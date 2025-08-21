// SetupForm.cs
using System;
using System.Linq;
using System.Windows.Forms;
using Telerik.WinControls.UI;

namespace TelerikWinFormsApp1
{
    public partial class SetupForm : Telerik.WinControls.UI.RadForm
    {
        public SetupForm()
        {
            InitializeComponent();
            HookEvents();
            LoadDefaultsIntoUi();
        }

        private void HookEvents()
        {
            btnApplySettings.Click += btnApplySettings_Click;
            btnStartQuiz.Click += btnStartQuiz_Click;

            cmbNumSchools.SelectedIndexChanged += (s, e) => RefreshSchoolTextBoxes();
            cmbNumOptions.SelectedIndexChanged += (s, e) => { /* nothing visual to toggle */ };
        }

        private void LoadDefaultsIntoUi()
        {
            // duration
            nudDurationSec.Minimum = 0;
            nudDurationSec.Maximum = 3600;
            nudDurationSec.Value = QuizConfig.QuestionDurationSec;

            // schools 2..5
            cmbNumSchools.Items.Clear();
            foreach (var n in new[] { 2, 3, 4, 5 })
                cmbNumSchools.Items.Add(n.ToString());
            cmbNumSchools.SelectedItem = cmbNumSchools.Items
                .FirstOrDefault(it => it.Text == QuizConfig.NumSchools.ToString()) ?? cmbNumSchools.Items.Last();

            // options 3..5
            cmbNumOptions.Items.Clear();
            foreach (var n in new[] { 3, 4, 5 })
                cmbNumOptions.Items.Add(n.ToString());
            cmbNumOptions.SelectedItem = cmbNumOptions.Items
                .FirstOrDefault(it => it.Text == QuizConfig.NumOptions.ToString()) ?? cmbNumOptions.Items.Last();

            // names
            txtSchoolName1.Text = QuizConfig.SchoolNames[0];
            txtSchoolName2.Text = QuizConfig.SchoolNames[1];
            txtSchoolName3.Text = QuizConfig.SchoolNames[2];
            txtSchoolName4.Text = QuizConfig.SchoolNames[3];
            txtSchoolName5.Text = QuizConfig.SchoolNames[4];

            RefreshSchoolTextBoxes();

            // you can only Start after Apply (so Start knows settings are valid)
            btnStartQuiz.Enabled = false;
        }

        private void RefreshSchoolTextBoxes()
        {
            int nSchools = ParseSelected(cmbNumSchools, 5);
            var boxes = new[] { txtSchoolName1, txtSchoolName2, txtSchoolName3, txtSchoolName4, txtSchoolName5 };

            for (int i = 0; i < boxes.Length; i++)
            {
                boxes[i].Enabled = i < nSchools;
                boxes[i].ReadOnly = !(i < nSchools);
            }
        }

        private static int ParseSelected(RadDropDownList ddl, int fallback)
            => int.TryParse(ddl.SelectedItem?.Text, out var n) ? n : fallback;

        private void btnApplySettings_Click(object sender, EventArgs e)
        {
            // read UI
            int duration = (int)nudDurationSec.Value;
            int schools = ParseSelected(cmbNumSchools, 5); // clamp handled in QuizConfig
            int options = ParseSelected(cmbNumOptions, 5);

            // collect names
            var names = new[]
            {
                txtSchoolName1.Text, txtSchoolName2.Text, txtSchoolName3.Text,
                txtSchoolName4.Text, txtSchoolName5.Text
            };

            // apply to config
            QuizConfig.QuestionDurationSec = Math.Max(0, duration);
            QuizConfig.NumSchools = schools;
            QuizConfig.NumOptions = options;
            QuizConfig.SetSchoolNames(names);

            // tiny validation note
            if (QuizConfig.NumSchools < 2 || QuizConfig.NumSchools > 5)
            {
                MessageBox.Show(this, "Number of schools must be 2..5.", "Settings",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (QuizConfig.NumOptions < 3 || QuizConfig.NumOptions > 5)
            {
                MessageBox.Show(this, "Number of options must be 3..5.", "Settings",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            // Persist to disk so it survives app restarts
            QuizConfig.Save();
            // done
            btnStartQuiz.Enabled = true;
            MessageBox.Show(this, "Settings applied. You can now Start Quiz.", "Settings",
                MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void btnStartQuiz_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }


    }
}
