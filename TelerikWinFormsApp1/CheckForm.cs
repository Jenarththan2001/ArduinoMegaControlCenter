using System;
using System.IO.Ports;
using System.Linq;
using System.Windows.Forms;
using System.Diagnostics;   // Debug.WriteLine
using System.Data;          // for DataTable / DataRow
using System.Drawing;       // for Color
using System.IO;            // for File
using Telerik.WinControls;

namespace TelerikWinFormsApp1
{
    public partial class CheckForm : Telerik.WinControls.UI.RadForm
    {
        // ===== Serial receive line buffering =====
        private readonly System.Text.StringBuilder _rxBuffer = new System.Text.StringBuilder(256);

        SerialPort serialPort = new SerialPort();
        string selectedPort = "";

        // Baud options shown in cmbBaudRate
        private readonly int[] _baudRates = { 9600, 19200, 38400, 57600, 115200, 230400, 460800, 921600 };

        // Log list cap (FIFO)
        private const int MaxLogItems = 1000;

        // ===== 5×5 matrix (School1..5 × A..E) =====
        private readonly char[] _colsAE = { 'A', 'B', 'C', 'D', 'E' };
        private readonly string[] _schools = { "School1", "School2", "School3", "School4", "School5" };
        private DataTable _matrix;

        // If true → only one cell per school can be ON at a time
        private const bool ExclusivePerSchool = false;

        // ===== Reset echo await timer to detect missing echo =====
        private readonly Timer _resetAwaitTimer = new Timer { Interval = 2000 }; // 2s

        // ----- BUZZER test helpers -----
        private readonly Timer _buzzerCooldownTimer = new Timer { Interval = 600 }; // 0.6s lockout after click
        private const string BUZZER_CMD = "BUZZ\n";   // match your Arduino command (fallback: "BEEP\n")

        // ===== Team LED state (1-based: 1..5) =====
        private readonly bool[] _ledOn = new bool[6]; // index 0 unused

        private static readonly Color LedOnColor = Color.FromArgb(46, 160, 67);
        private static readonly Color LedOffColor = Color.FromArgb(64, 64, 64);

        public CheckForm()
        {
            InitializeComponent();
            HookEvents();
        }

        private void HookEvents()
        {
            // Form
            this.Load += CheckForm_Load;

            // Buttons
            btnConnect.Click += btnConnect_Click;
            btnDisconnect.Click += btnDisconnect_Click;
            btnRefreshPorts.Click += btnRefreshPorts_Click;   // single hookup
            btnClearLogs.Click += btnClearLogs_Click;
            btnTeam1Led.Click += (s, e) => ToggleLed(1);
            btnTeam2Led.Click += (s, e) => ToggleLed(2);
            btnTeam3Led.Click += (s, e) => ToggleLed(3);
            btnTeam4Led.Click += (s, e) => ToggleLed(4);
            btnTeam5Led.Click += (s, e) => ToggleLed(5);
            btnSaveLogs.Click += btnSaveLogs_Click;

            // Reset button & grid handlers
            btnReset.Click += btnReset_Click;
            gridButtons.ViewCellFormatting += gridButtons_ViewCellFormatting;
            gridButtons.CellDoubleClick += gridButtons_CellDoubleClick;

            // Baud dropdown (optional logs)
            cmbBaudRate.SelectedIndexChanged += (s, e) =>
            {
                if (cmbBaudRate.SelectedItem != null)
                    Debug.WriteLine($"Baud changed → {cmbBaudRate.SelectedItem}");
            };

            // Live monitor toggle feedback
            chkLiveMonitor.ToggleStateChanged += (s, e) =>
            {
                Debug.WriteLine($"Live monitor → {chkLiveMonitor.Checked}");
                AppendLog($"[Live Monitor: {(chkLiveMonitor.Checked ? "ON" : "OFF")}]");
            };

            // Test Mode note
            chkTestMode.ToggleStateChanged += (s, e) =>
            {
                AppendLog($"[Test Mode: {(chkTestMode.Checked ? "ON" : "OFF")}]");
            };

            // Reset echo timer
            _resetAwaitTimer.Tick += (s, e) =>
            {
                _resetAwaitTimer.Stop();
                lblResetButtonStatus.Text = "Operator Reset: NO ECHO";
                lblResetButtonStatus.ForeColor = Color.OrangeRed;
                AppendLog("[WARN] RESET sent but no echo received");
            };

            // Buzzer
            btnBuzzerCheck.Click += btnBuzzerCheck_Click;

            // buzzer cooldown re-enable
            _buzzerCooldownTimer.Tick += (s, e) =>
            {
                _buzzerCooldownTimer.Stop();
                btnBuzzerCheck.Enabled = true;
                btnBuzzerCheck.Text = "Test Buzzer";
            };
        }

        // ======== CORE: refresh ports & log ========
        private void RefreshPortsAndLog()
        {
            Debug.WriteLine("Refresh ports (core)");
            var before = cmbComPorts.SelectedItem as string;

            RefreshCOMPorts();

            var ports = SerialPort.GetPortNames().OrderBy(p => p).ToArray();
            if (ports.Length == 0)
            {
                AppendLog("[Ports] No COM ports found.");
            }
            else
            {
                AppendLog("[Ports] " + string.Join(", ", ports));
                // try to restore previous selection if present
                if (!string.IsNullOrWhiteSpace(before) && ports.Contains(before))
                    cmbComPorts.SelectedItem = before;
            }
        }

        // The ONLY event handler for btnRefreshPorts
        private void btnRefreshPorts_Click(object sender, EventArgs e)
        {
            Debug.WriteLine("btnRefreshPorts clicked");
            RefreshPortsAndLog();
        }

        // ======== Buzzer test ========
        private void btnBuzzerCheck_Click(object sender, EventArgs e)
        {
            if (!serialPort.IsOpen)
            {
                RadMessageBox.Show(this, "COM port is not open.", "Serial",
                    MessageBoxButtons.OK, RadMessageIcon.Info);
                return;
            }

            try
            {
                // UI feedback + anti-spam
                btnBuzzerCheck.Enabled = false;
                btnBuzzerCheck.Text = "Buzzing…";

                // send the test buzz command
                serialPort.Write(BUZZER_CMD);
                AppendLog("[BUZZ command sent]");

                // optional: if Arduino replies with something (e.g., "BUZZ=OK"),
                // it will flow through SerialPort_DataReceived → AppendLog.
                _buzzerCooldownTimer.Stop();
                _buzzerCooldownTimer.Start();
            }
            catch (Exception ex)
            {
                btnBuzzerCheck.Enabled = true;
                btnBuzzerCheck.Text = "Test Buzzer";
                AppendLog($"[ERROR sending BUZZ: {ex.Message}]");
            }
        }

        private void CheckForm_Load(object sender, EventArgs e)
        {
            // Populate baud list
            cmbBaudRate.Items.Clear();
            foreach (var b in _baudRates) cmbBaudRate.Items.Add(b.ToString());
            if (cmbBaudRate.Items.Count > 0) cmbBaudRate.SelectedItem = "9600";

            RefreshCOMPorts();
            chkLiveMonitor.Checked = true;
            SetUiConnected(false);

            // Build the 5×5 matrix
            InitGridMatrix();

            // Defaults for status strip + device info
            lblComStatus.Text = "COM: Disconnected";
            lblComStatus.ForeColor = Color.DarkRed;
            lblComStatus.AccessibleName = "COM Status";
            lblComStatus.AccessibleDescription = "COM: Disconnected";

            lblLastMsg.Text = "Last Msg: -";
            lblLastMsg.AccessibleName = "Last Message";
            lblLastMsg.AccessibleDescription = "Last Msg: -";

            lblTimestamp.Text = "Time: —";
            lblTimestamp.AccessibleName = "Time";
            lblTimestamp.AccessibleDescription = "Time: —";


            // Default reset label: NOT PRESSED
            SetResetPressed(false);

            // LED buttons initial paint
            for (int t = 1; t <= 5; t++) UpdateLedButtonUI(t);
        }

        // ---------- UI state ----------
        private void SetUiConnected(bool connected)
        {
            btnConnect.Enabled = !connected;
            btnDisconnect.Enabled = connected;
            cmbComPorts.Enabled = !connected;
            cmbBaudRate.Enabled = !connected;   // lock baud while connected

            lblConnectionStatus.Text = connected ? "Connected" : "Disconnected";
            lblConnectionStatus.ForeColor = connected ? Color.DarkGreen
                                                      : Color.DarkRed;

            UpdateComStatus(connected);
        }

        // ---------- Status strip helpers ----------
        private void UpdateComStatus(bool connected)
        {
            var text = connected ? "COM: Connected" : "COM: Disconnected";
            lblComStatus.Text = text;
            lblComStatus.ForeColor = connected ? Color.DarkGreen : Color.DarkRed;
            lblComStatus.AccessibleName = "COM Status";
            lblComStatus.AccessibleDescription = text;

            var t = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            lblTimestamp.Text = $"Time: {t}";
            lblTimestamp.AccessibleName = "Time";
            lblTimestamp.AccessibleDescription = $"Last update at {t}";
        }

        private void NoteLastMessage(string msg)
        {
            var trimmed = Truncate(msg, 120);
            lblLastMsg.Text = $"Last Msg: {trimmed}";
            lblLastMsg.AccessibleName = "Last Message";
            lblLastMsg.AccessibleDescription = $"Last Msg: {trimmed}";

            var t = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            lblTimestamp.Text = $"Time: {t}";
            lblTimestamp.AccessibleDescription = $"Last message at {t}";
        }

        private static string Truncate(string s, int max)
        {
            if (string.IsNullOrEmpty(s)) return "-";
            return (s.Length <= max) ? s : s.Substring(0, max) + "…";
        }



        // ---------- Populate cmbComPorts ----------
        private void RefreshCOMPorts()
        {
            var ports = SerialPort.GetPortNames().OrderBy(p => p).ToArray();
            var prev = cmbComPorts.SelectedItem as string;

            cmbComPorts.Items.Clear();
            cmbComPorts.Items.AddRange(ports);

            if (ports.Length == 0)
            {
                Debug.WriteLine("No COM ports found.");
                cmbComPorts.Text = string.Empty;
                return;
            }
            else
            {
                Debug.WriteLine("Found COM ports: " + string.Join(", ", ports));
            }

            if (!string.IsNullOrWhiteSpace(prev) && ports.Contains(prev))
                cmbComPorts.SelectedItem = prev;
            else
                cmbComPorts.SelectedIndex = 0;
        }

        // Helper: read baud from combo (with fallback + logs)
        private int GetSelectedBaudOrFallback()
        {
            if (cmbBaudRate.SelectedItem == null)
            {
                Debug.WriteLine("No baud selected; defaulting to 9600");
                return 9600;
            }

            if (!int.TryParse(cmbBaudRate.SelectedItem.ToString(), out int baud))
            {
                Debug.WriteLine($"Invalid baud '{cmbBaudRate.SelectedItem}', defaulting to 9600");
                return 9600;
            }

            return baud;
        }

        // ---------- Connect ----------
        private void btnConnect_Click(object sender, EventArgs e)
        {
            Debug.WriteLine("btnConnect clicked");

            if (serialPort.IsOpen)
            {
                Debug.WriteLine("Port already open; ignoring.");
                return;
            }

            if (cmbComPorts.SelectedItem == null)
            {
                Debug.WriteLine("No COM port selected.");
                RadMessageBox.Show(this, "Select a COM port first.", "Serial",
                    MessageBoxButtons.OK, RadMessageIcon.Info);
                return;
            }

            selectedPort = cmbComPorts.SelectedItem.ToString();
            int baud = GetSelectedBaudOrFallback();

            serialPort.PortName = selectedPort;
            serialPort.BaudRate = baud;
            serialPort.Parity = Parity.None;
            serialPort.DataBits = 8;
            serialPort.StopBits = StopBits.One;
            serialPort.Handshake = Handshake.None;
            serialPort.NewLine = "\n";
            serialPort.ReadTimeout = 1000;
            serialPort.WriteTimeout = 2000;

            try
            {
                Debug.WriteLine($"Attempting to open {selectedPort} @ {baud} baud");
                serialPort.Open();

                // hook RX after open
                serialPort.DataReceived -= SerialPort_DataReceived;
                serialPort.DataReceived += SerialPort_DataReceived;

                Debug.WriteLine($"SUCCESS: Opened {selectedPort} @ {baud}");
                AppendLog($"[Connected {selectedPort} @ {baud}]");
                SetUiConnected(true);

            }
            catch (Exception ex)
            {
                Debug.WriteLine($"ERROR: Failed to open {selectedPort}\n{ex}");
                AppendLog($"[ERROR opening {selectedPort}: {ex.Message}]");
                SetUiConnected(false);
                RadMessageBox.Show(this,
                    $"Failed to open {selectedPort}.\n\n{ex.Message}",
                    "Serial Error", MessageBoxButtons.OK, RadMessageIcon.Error);
            }
        }

        // ---------- Disconnect ----------
        private void btnDisconnect_Click(object sender, EventArgs e)
        {
            Debug.WriteLine("btnDisconnect clicked");
            try
            {
                if (serialPort.IsOpen)
                {
                    serialPort.DataReceived -= SerialPort_DataReceived;
                    serialPort.Close();
                    Debug.WriteLine($"Closed port {selectedPort}.");
                    AppendLog($"[Disconnected {selectedPort}]");
                }
                else
                {
                    Debug.WriteLine("Port was not open.");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"ERROR while closing {selectedPort}\n{ex}");
                AppendLog($"[ERROR closing {selectedPort}: {ex.Message}]");
            }

            SetUiConnected(false);
        }

        // ---------- Data RX → logs + matrix ----------
        private void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                string chunk = serialPort.ReadExisting();
                if (string.IsNullOrEmpty(chunk)) return;

                // Normalize CRLF -> LF and accumulate
                lock (_rxBuffer)
                {
                    _rxBuffer.Append(chunk.Replace("\r", ""));

                    int nl;
                    while ((nl = _rxBuffer.ToString().IndexOf('\n')) >= 0)
                    {
                        string line = _rxBuffer.ToString(0, nl).Trim();
                        _rxBuffer.Remove(0, nl + 1);

                        if (line.Length == 0) continue;

                        BeginInvoke(new Action(() =>
                        {
                            if (chkLiveMonitor.Checked)
                                AppendLog(line);

                            // status strip last message
                            NoteLastMessage(line);

                            // update the 5×5 table and reset label
                            ProcessMatrixLine(line);

                            Debug.WriteLine($"RX: {line}");
                        }));
                    }
                }
            }
            catch (Exception ex)
            {
                BeginInvoke(new Action(() =>
                {
                    AppendLog($"[RX ERROR: {ex.Message}]");
                }));
                Debug.WriteLine($"RX ERROR: {ex}");
            }
        }

        // ---------- Clear logs ----------
        private void btnClearLogs_Click(object sender, EventArgs e)
        {
            Debug.WriteLine("btnClearLogs clicked");
            lstLogs.Items.Clear();
            AppendLog("[Logs cleared]");
        }

        // ---------- Log helper ----------
        private void AppendLog(string text)
        {
            if (lstLogs.Items.Count >= MaxLogItems)
                lstLogs.Items.RemoveAt(0);

            lstLogs.Items.Add(text);

            var listElement = lstLogs.ListElement;
            if (listElement != null && lstLogs.Items.Count > 0)
                listElement.ScrollToItem(lstLogs.Items[lstLogs.Items.Count - 1]);
        }

        // ======== MATRIX (School × A..E) ========
        private void InitGridMatrix()
        {
            _matrix = new DataTable();

            _matrix.Columns.Add("School", typeof(string));
            foreach (var c in _colsAE) _matrix.Columns.Add(c.ToString(), typeof(bool));

            foreach (var s in _schools)
            {
                var row = _matrix.NewRow();
                row["School"] = s;
                foreach (var c in _colsAE) row[c.ToString()] = false;
                _matrix.Rows.Add(row);
            }

            gridButtons.DataSource = _matrix;

            var mt = gridButtons.MasterTemplate;
            mt.AllowAddNewRow = false;
            mt.AllowDeleteRow = false;
            mt.AllowEditRow = false;

            if (mt.Columns["School"] != null)
            {
                mt.Columns["School"].Width = 110;
                mt.Columns["School"].ReadOnly = true;
            }
            foreach (var c in _colsAE)
            {
                var col = mt.Columns[c.ToString()];
                if (col != null)
                {
                    col.Width = 70;
                    col.ReadOnly = true;
                }
            }
        }

        // Color A..E cells based on bool value
        private void gridButtons_ViewCellFormatting(object sender, Telerik.WinControls.UI.CellFormattingEventArgs e)
        {
            var colName = e.CellElement.ColumnInfo?.Name;
            if (string.IsNullOrEmpty(colName) || colName == "School") return;

            bool on = false;
            try
            {
                var val = e.CellElement.Value;
                if (val is bool b) on = b;
                else if (val != null) bool.TryParse(val.ToString(), out on);
            }
            catch { }

            e.CellElement.DrawFill = true;
            e.CellElement.NumberOfColors = 1;
            e.CellElement.BackColor = on ? Color.FromArgb(46, 160, 67) : Color.FromArgb(64, 64, 64);
            e.CellElement.ForeColor = Color.White;
        }

        // Double-click to toggle in Test Mode
        private void gridButtons_CellDoubleClick(object sender, Telerik.WinControls.UI.GridViewCellEventArgs e)
        {
            if (!chkTestMode.Checked) return;
            if (e.RowIndex < 0) return;

            var col = e.Column?.Name;
            if (string.IsNullOrEmpty(col) || col == "School") return;

            bool cur = Convert.ToBoolean(e.Row.Cells[col].Value);
            if (ExclusivePerSchool)
            {
                foreach (var c in _colsAE) e.Row.Cells[c.ToString()].Value = false;
                e.Row.Cells[col].Value = true;
            }
            else
            {
                e.Row.Cells[col].Value = !cur;
            }
            gridButtons.Refresh();
        }

        // Interpret incoming serial and set cells / reset label (supports OPR:1/0)
        private void ProcessMatrixLine(string line)
        {
            if (string.IsNullOrWhiteSpace(line)) return;
            line = line.Trim();

            // ===== Operator Reset via OPR:1 / OPR:0 =====
            if (line.StartsWith("OPR:", StringComparison.OrdinalIgnoreCase))
            {
                var parts = line.Split(':');
                if (parts.Length >= 2)
                {
                    var v = parts[1].Trim();
                    if (v == "1")
                    {
                        _resetAwaitTimer.Stop();
                        SetResetPressed(true, "OPR");
                    }
                    else if (v == "0")
                    {
                        _resetAwaitTimer.Stop();
                        SetResetPressed(false, "OPR");
                    }
                }
                return;
            }

            // ===== RESET echo =====
            if (line.Equals("RESET", StringComparison.OrdinalIgnoreCase))
            {
                _resetAwaitTimer.Stop();       // got echo, stop waiting
                ClearMatrix();
                SetResetPressed(true, "echo"); // pressed (from echo)
                return;
            }

            // Optional variants: show NOT PRESSED without clearing
            if (line.Equals("RESET_RELEASED", StringComparison.OrdinalIgnoreCase) ||
                line.Equals("RESET OFF", StringComparison.OrdinalIgnoreCase) ||
                line.Equals("RESET=0", StringComparison.OrdinalIgnoreCase))
            {
                _resetAwaitTimer.Stop();
                SetResetPressed(false, "released");
                return;
            }

            // ===== Button matrix (Sx:O[:...]) =====
            var p = line.Split(':');
            if (p.Length < 2) return;
            if (!p[0].StartsWith("S", StringComparison.OrdinalIgnoreCase)) return;

            if (!int.TryParse(p[0].Substring(1), out int team)) return;
            if (team < 1 || team > _schools.Length) return;

            string opt = p[1].Trim().ToUpperInvariant();
            if (opt.Length != 1) return;
            char c = opt[0];
            if (!_colsAE.Contains(c)) return;

            int rowIdx = team - 1;
            var colName = c.ToString();

            if (ExclusivePerSchool)
            {
                foreach (var col in _colsAE)
                    _matrix.Rows[rowIdx][col.ToString()] = false;
            }

            _matrix.Rows[rowIdx][colName] = true;
            gridButtons.Refresh();
        }

        private void ClearMatrix()
        {
            foreach (DataRow r in _matrix.Rows)
                foreach (var c in _colsAE)
                    r[c.ToString()] = false;

            gridButtons.Refresh();
        }

        // Send RESET to Arduino (expects Arduino to echo "RESET")
        private void btnReset_Click(object sender, EventArgs e)
        {
            if (!serialPort.IsOpen)
            {
                RadMessageBox.Show(this, "COM port is not open.", "Serial",
                    MessageBoxButtons.OK, RadMessageIcon.Info);
                return;
            }

            try
            {
                serialPort.Write("RESET\n");

                // Show waiting state until echo arrives or timer times out
                lblResetButtonStatus.Text = "Operator Reset: WAITING…";
                lblResetButtonStatus.ForeColor = Color.DarkGoldenrod;
                AppendLog("[RESET sent]");

                _resetAwaitTimer.Stop();
                _resetAwaitTimer.Start(); // if no echo in 2s, we show NO ECHO
            }
            catch (Exception ex)
            {
                AppendLog($"[ERROR sending RESET: {ex.Message}]");
            }
        }

        // Click → toggle state and send command
        private void ToggleLed(int team)
        {
            if (team < 1 || team > 5) return;

            var newState = !_ledOn[team];

            if (!serialPort.IsOpen)
            {
                RadMessageBox.Show(this, "COM port is not open.", "Serial",
                    MessageBoxButtons.OK, RadMessageIcon.Info);
                return;
            }

            SendLed(team, newState);
            // Optimistic UI (will also sync if Arduino echoes back)
            _ledOn[team] = newState;
            UpdateLedButtonUI(team);
        }

        // Send LED<n>:1 or LED<n>:0 to Arduino
        private void SendLed(int team, bool on)
        {
            try
            {
                string cmd = $"LED{team}:{(on ? 1 : 0)}\n";
                serialPort.Write(cmd);
                AppendLog($"[LED] Sent {cmd.Trim()}");
            }
            catch (Exception ex)
            {
                AppendLog($"[ERROR sending LED{team}: {ex.Message}]");
            }
        }

        // If Arduino notifies us (echo) we sync here
        private void SetLedStateFromDevice(int team, bool on)
        {
            if (team < 1 || team > 5) return;
            _ledOn[team] = on;
            UpdateLedButtonUI(team);
            AppendLog($"[LED] Team {team} → {(on ? "ON" : "OFF")} (from device)");
        }

        // Paint the RadButton for the given team (Telerik-friendly)
        private void UpdateLedButtonUI(int team)
        {
            var btn = GetLedButton(team);
            if (btn == null) return;

            bool on = _ledOn[team];
            var color = on ? LedOnColor : LedOffColor;

            btn.Text = $"LED {team}: {(on ? "ON" : "OFF")}";
            btn.ForeColor = Color.White;

            // Prefer the fill primitive if available (theme-safe)
            var fill = btn.ButtonElement?.ButtonFillElement; // Telerik.WinControls.Primitives.FillPrimitive
            if (fill != null)
            {
                fill.GradientStyle = Telerik.WinControls.GradientStyles.Solid;
                fill.BackColor = color;
            }
            else
            {
                // Fallback in older/odd themes
                btn.BackColor = color;
            }

            // keep text readable
            btn.ButtonElement.ForeColor = Color.White;
        }

        // Map team → button
        private Telerik.WinControls.UI.RadButton GetLedButton(int team)
        {
            switch (team)
            {
                case 1: return btnTeam1Led;
                case 2: return btnTeam2Led;
                case 3: return btnTeam3Led;
                case 4: return btnTeam4Led;
                case 5: return btnTeam5Led;
                default: return null;
            }
        }
                // Helper: set/reset label
        private void SetResetPressed(bool pressed, string source = "")
        {
            if (pressed)
            {
                lblResetButtonStatus.Text = "Operator Reset: PRESSED" + (string.IsNullOrEmpty(source) ? "" : $" ({source})");
                lblResetButtonStatus.ForeColor = Color.DarkRed;
            }
            else
            {
                lblResetButtonStatus.Text = "Operator Reset: NOT PRESSED";
                lblResetButtonStatus.ForeColor = Color.DarkGreen;
            }
        }
        // ===== Save logs (plain text) =====
        private void btnSaveLogs_Click(object sender, EventArgs e)
        {
            if (lstLogs.Items.Count == 0)
            {
                RadMessageBox.Show(this, "No logs to save.", "Save Logs",
                    MessageBoxButtons.OK, RadMessageIcon.Info);
                return;
            }

            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
                sfd.Title = "Save Logs";
                sfd.FileName = "logs.txt";

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        var lines = lstLogs.Items.Select(i => i.Text).ToArray();
                        File.WriteAllLines(sfd.FileName, lines);
                        AppendLog($"[Logs saved → {sfd.FileName}]");
                    }
                    catch (Exception ex)
                    {
                        AppendLog($"[ERROR saving logs: {ex.Message}]");
                    }
                }
            }
        }
    }
}
