using System;
using System.ComponentModel;
using System.IO;                 // <-- for File.WriteAllText
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using Telerik.WinControls;       // RadMessageBox, RadMessageIcon
using Telerik.WinControls.Data;
using Telerik.WinControls.UI;

namespace TelerikWinFormsApp1
{
    public partial class MainForm : Telerik.WinControls.UI.RadForm
    {
        // ===== Singleton (optional; useful if other forms ever need to call in) =====
        public static MainForm Instance { get; private set; }

        // ===== Serial =====
        private readonly SerialPort _serial = new SerialPort();
        private readonly StringBuilder _rxBuf = new StringBuilder(256);
        private readonly Timer _reconnectTimer = new Timer { Interval = 5000 }; // retry every 5s

        // ===== Question countdown =====
        private readonly Timer _questionTimer = new Timer { Interval = 1000 }; // 1s tick
        private DateTime? _questionStartUtc = null;
        private int DurationSec => Math.Max(0, QuizConfig.QuestionDurationSec);

        // Track whether a team (1..5) has already answered this question
        private readonly bool[] _answered = new bool[6]; // index 0 unused

        // ===== Session history for export =====
        private int _questionIndex = 0;

        private class AnswerRow
        {
            public int No { get; set; }             // Filled at export time (for CSV)
            public int Question { get; set; }       // 1,2,3...
            public string School { get; set; }      // School name
            public string Option { get; set; }      // "A".."E"
            public string Time { get; set; }        // "mm:ss.mmm"
            public DateTime Utc { get; set; }       // When captured
            public double ElapsedMs { get; set; }   // For sort
        }

        private readonly BindingList<AnswerRow> _history = new BindingList<AnswerRow>();

        // Grid columns (live leaderboard)
        private const string COL_SCHOOL = "School";
        private const string COL_OPTION = "Option";
        private const string COL_TIME = "Time";
        private const string COL_MS = "ElapsedMs"; // hidden numeric for sort

        public MainForm()
        {
            InitializeComponent();
            Instance = this;
            HookEvents();
        }

        private void HookEvents()
        {
            this.Load += MainForm_Load;
            this.FormClosed += (s, e) => SafeCloseSerial();

            // Countdown
            _questionTimer.Tick += (s, e) => OnTimerTick();

            // Reset button
            btnResetTimer.Click += (s, e) =>
            {
                ClearAllAnswers();
                StartQuestionNow();
            };

            // Export (CSV via this handler)
            btnExportPDF.Click += btnExportPDF_Click;

            // Serial reconnect attempts
            _reconnectTimer.Tick += (s, e) => TryOpenFirstPort();

            // Serial received
            _serial.DataReceived += Serial_DataReceived;

        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            BuildLeaderboardGrid();
            UpdateTimerLabel(DurationSec, running: false);

            // Minimal serial setup (9600 baud, first available port)
            _serial.BaudRate = 9600;
            _serial.Parity = Parity.None;
            _serial.DataBits = 8;
            _serial.StopBits = StopBits.One;
            _serial.Handshake = Handshake.None;
            _serial.NewLine = "\n";
            _serial.ReadTimeout = 1000;
            _serial.WriteTimeout = 2000;

            TryOpenFirstPort();
        }

        // =========================
        // Serial helpers
        // =========================
        private void TryOpenFirstPort()
        {
            if (_serial.IsOpen) return;

            try
            {
                var name = SerialPort.GetPortNames().OrderBy(p => p).FirstOrDefault();
                if (string.IsNullOrEmpty(name))
                {
                    _reconnectTimer.Start(); // no ports—try again later
                    return;
                }

                _serial.PortName = name;
                _serial.Open();
                _reconnectTimer.Stop();
            }
            catch
            {
                // Couldn’t open—retry later
                _reconnectTimer.Start();
            }
        }

        private void SafeCloseSerial()
        {
            try
            {
                if (_serial.IsOpen)
                {
                    _serial.DataReceived -= Serial_DataReceived;
                    _serial.Close();
                }
            }
            catch { /* ignore */ }
        }

        private void Serial_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                string chunk = _serial.ReadExisting();
                if (string.IsNullOrEmpty(chunk)) return;

                lock (_rxBuf)
                {
                    _rxBuf.Append(chunk.Replace("\r", ""));

                    int nl;
                    while ((nl = _rxBuf.ToString().IndexOf('\n')) >= 0)
                    {
                        string line = _rxBuf.ToString(0, nl).Trim();
                        _rxBuf.Remove(0, nl + 1);

                        if (line.Length == 0) continue;

                        // Bounce to UI thread
                        BeginInvoke(new Action(() => ProcessLine(line)));
                    }
                }
            }
            catch
            {
                // on read error, we’ll try reconnecting
                BeginInvoke(new Action(() =>
                {
                    SafeCloseSerial();
                    _reconnectTimer.Start();
                }));
            }
        }

        // =========================
        // Parsing & business logic
        // =========================
        private void ProcessLine(string line)
        {
            // Supported:
            // RESET            -> start new question window (clears answers)
            // OPR:1            -> same as RESET (start window)
            // OPR:0            -> ignore for timer
            // Sx:O             -> e.g., S2:B  (first answer only per team)

            if (string.IsNullOrWhiteSpace(line)) return;
            line = line.Trim();

            // RESET / OPR:1
            if (line.Equals("RESET", StringComparison.OrdinalIgnoreCase) ||
                line.Equals("OPR:1", StringComparison.OrdinalIgnoreCase))
            {
                ClearAllAnswers();
                StartQuestionNow();
                return;
            }

            // Ignore OPR:0
            if (line.Equals("OPR:0", StringComparison.OrdinalIgnoreCase))
                return;

            // Sx:O
            var parts = line.Split(':');
            if (parts.Length >= 2 && parts[0].StartsWith("S", StringComparison.OrdinalIgnoreCase))
            {
                if (int.TryParse(parts[0].Substring(1), out int team))
                {
                    char opt = parts[1].Trim().Length > 0 ? char.ToUpperInvariant(parts[1].Trim()[0]) : '\0';
                    if (TeamIsValid(team) && OptionIsValid(opt))
                        RegisterFirstAnswer(team, opt);
                }
            }
        }

        private bool TeamIsValid(int team)
        {
            int n = Math.Max(2, Math.Min(5, QuizConfig.NumSchools));
            return team >= 1 && team <= n;
        }

        private bool OptionIsValid(char option)
        {
            if (option < 'A' || option > 'Z') return false;
            int n = Math.Max(3, Math.Min(5, QuizConfig.NumOptions));
            char last = (char)('A' + n - 1);
            return option >= 'A' && option <= last;
        }

        private string TeamName(int teamIndex)
        {
            int n = Math.Max(2, Math.Min(5, QuizConfig.NumSchools));
            if (teamIndex < 1 || teamIndex > n) return $"School{teamIndex}";
            return QuizConfig.SchoolNames[teamIndex - 1];
        }

        // =========================
        // Leaderboard (answers only)
        // =========================
        private void BuildLeaderboardGrid()
        {
            var mt = gridLeaderboard.MasterTemplate;
            gridLeaderboard.AutoGenerateColumns = false;

            mt.AllowAddNewRow = false;
            mt.AllowDeleteRow = false;
            mt.AllowEditRow = false;
            mt.AllowColumnChooser = false;
            mt.EnableGrouping = false;
            mt.EnableSorting = true;

            mt.Columns.Clear();

            mt.Columns.Add(new GridViewTextBoxColumn(COL_SCHOOL)
            {
                HeaderText = "School",
                Width = 220,
                ReadOnly = true
            });

            mt.Columns.Add(new GridViewTextBoxColumn(COL_OPTION)
            {
                HeaderText = "Option",
                Width = 90,
                ReadOnly = true,
                TextAlignment = ContentAlignment.MiddleCenter
            });

            mt.Columns.Add(new GridViewTextBoxColumn(COL_TIME)
            {
                HeaderText = "Time",
                Width = 120,
                ReadOnly = true
            });

            mt.Columns.Add(new GridViewDecimalColumn(COL_MS)
            {
                IsVisible = false,
                ReadOnly = true
            });

            gridLeaderboard.Rows.Clear();

            // Sort fastest first (lowest ms), then school name
            mt.SortDescriptors.Clear();
            mt.SortDescriptors.Add(new SortDescriptor(COL_MS, ListSortDirection.Ascending));
            mt.SortDescriptors.Add(new SortDescriptor(COL_SCHOOL, ListSortDirection.Ascending));
        }

        private static string FormatSpan(TimeSpan ts)
        {
            // mm:ss.mmm
            return $"{(int)ts.TotalMinutes:00}:{ts.Seconds:00}.{ts.Milliseconds:000}";
        }

        /// <summary>
        /// Locks in ONLY the first answer per team and displays + stores it.
        /// </summary>
        private void RegisterFirstAnswer(int teamIndex, char option)
        {
            if (!TeamIsValid(teamIndex)) return;
            if (!OptionIsValid(option)) return;
            if (_answered[teamIndex]) return; // already answered

            // If timer hasn’t started (defensive), start it now
            if (_questionStartUtc == null)
                StartQuestionNow();

            var elapsed = DateTime.UtcNow - _questionStartUtc.Value;
            double ms = Math.Max(0, elapsed.TotalMilliseconds);

            // Live grid row
            var row = gridLeaderboard.Rows.AddNew();
            row.Cells[COL_SCHOOL].Value = TeamName(teamIndex);
            row.Cells[COL_OPTION].Value = option.ToString();
            row.Cells[COL_TIME].Value = FormatSpan(elapsed);
            row.Cells[COL_MS].Value = ms;

            _answered[teamIndex] = true;

            // Persist for export
            _history.Add(new AnswerRow
            {
                Question = _questionIndex == 0 ? 1 : _questionIndex, // safety
                School = TeamName(teamIndex),
                Option = option.ToString(),
                Time = FormatSpan(elapsed),
                Utc = DateTime.UtcNow,
                ElapsedMs = ms
            });

            // Resort (fastest first)
            var mt = gridLeaderboard.MasterTemplate;
            mt.SortDescriptors.Clear();
            mt.SortDescriptors.Add(new SortDescriptor(COL_MS, ListSortDirection.Ascending));
            mt.SortDescriptors.Add(new SortDescriptor(COL_SCHOOL, ListSortDirection.Ascending));
        }

        // =========================
        // Timer
        // =========================
        private void StartQuestionNow()
        {
            _questionIndex++; // new question #
            _questionStartUtc = DateTime.UtcNow;
            _questionTimer.Start();
            UpdateTimerLabel(DurationSec, running: true);
        }

        private void OnTimerTick()
        {
            if (_questionStartUtc == null)
            {
                _questionTimer.Stop();
                UpdateTimerLabel(DurationSec, running: false);
                return;
            }

            var elapsed = DateTime.UtcNow - _questionStartUtc.Value;
            int remaining = DurationSec - (int)Math.Floor(elapsed.TotalSeconds);
            if (remaining <= 0)
            {
                remaining = 0;
                _questionTimer.Stop();
            }
            UpdateTimerLabel(remaining, running: remaining > 0);
        }

        private void UpdateTimerLabel(int remainingSec, bool running)
        {
            int m = remainingSec / 60;
            int s = remainingSec % 60;
            lblTimer.Text = $"{m:00}:{s:00}";

            if (!running)
            {
                lblTimer.ForeColor = Color.Blue;
                lblTimer.Font = new Font(lblTimer.Font, FontStyle.Regular);
            }
            else if (remainingSec == 0)
            {
                lblTimer.ForeColor = Color.Red;
                lblTimer.Font = new Font(lblTimer.Font, FontStyle.Bold);
            }
            else
            {
                lblTimer.ForeColor = Color.Blue;
                lblTimer.Font = new Font(lblTimer.Font, FontStyle.Regular);
            }
        }

        private void ClearAllAnswers()
        {
            for (int i = 1; i <= 5; i++) _answered[i] = false;
            gridLeaderboard.Rows.Clear();
            _questionStartUtc = null;
            UpdateTimerLabel(DurationSec, running: false);
        }

        // =========================
        // Export CSV (all questions)
        // =========================
        private void btnExportPDF_Click(object sender, EventArgs e)
        {
            // Reuse existing button, but export CSV instead of PDF
            if (_history.Count == 0)
            {
                RadMessageBox.Show(this, "Nothing to export yet.", "Export CSV",
                    MessageBoxButtons.OK, RadMessageIcon.Info);
                return;
            }

            using (var sfd = new SaveFileDialog())
            {
                sfd.Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*";
                sfd.Title = "Export Quiz Results (CSV)";
                sfd.FileName = $"QuizResults_{DateTime.Now:yyyyMMdd_HHmmss}.csv";

                if (sfd.ShowDialog(this) != DialogResult.OK) return;

                try
                {
                    var sb = new StringBuilder();

                    // header
                    sb.AppendLine("No,Question,School,Option,Time,UTC,ElapsedMs");

                    // rows: sorted by Question then ElapsedMs
                    int no = 1;
                    foreach (var h in _history.OrderBy(x => x.Question).ThenBy(x => x.ElapsedMs))
                    {
                        sb.Append(Csv(no++)); sb.Append(',');
                        sb.Append(Csv(h.Question)); sb.Append(',');
                        sb.Append(Csv(h.School)); sb.Append(',');
                        sb.Append(Csv(h.Option)); sb.Append(',');
                        sb.Append(Csv(h.Time)); sb.Append(',');
                        sb.Append(Csv(h.Utc.ToString("yyyy-MM-dd HH:mm:ss.fff 'UTC'"))); sb.Append(',');
                        sb.Append(Csv(h.ElapsedMs));
                        sb.AppendLine();
                    }

                    File.WriteAllText(sfd.FileName, sb.ToString(), Encoding.UTF8);

                    RadMessageBox.Show(this, "Exported CSV successfully.", "Export CSV",
                        MessageBoxButtons.OK, RadMessageIcon.Info);
                }
                catch (Exception ex)
                {
                    RadMessageBox.Show(this, "Failed to export CSV.\n\n" + ex.Message, "Export CSV",
                        MessageBoxButtons.OK, RadMessageIcon.Error);
                }
            }
        }

        // CSV escape helper
        private static string Csv(object value)
        {
            if (value == null) return "";
            var s = Convert.ToString(value);
            bool mustQuote = s.Contains(",") || s.Contains("\"") || s.Contains("\n") || s.Contains("\r");
            if (mustQuote)
                s = "\"" + s.Replace("\"", "\"\"") + "\"";
            return s;
        }

        // =========================================
        // Keep these to satisfy Designer event hookups.
        // =========================================
        private void btnSetupForm_Click(object sender, EventArgs e)
        {
            using (var setup = new SetupForm())
            {
                if (setup.ShowDialog(this) == DialogResult.OK)
                {
                    // Rebuild view in case settings changed
                    BuildLeaderboardGrid();
                    // Don't autostart timer here
                    UpdateTimerLabel(Math.Max(0, QuizConfig.QuestionDurationSec), running: false);
                }
            }
        }

        private void btnCheckForm_Click(object sender, EventArgs e)
        {
            var check = new CheckForm();
            check.Show(this);
        }
    }
}
