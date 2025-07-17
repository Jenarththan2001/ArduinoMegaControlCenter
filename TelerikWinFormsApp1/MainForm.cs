using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls;

namespace TelerikWinFormsApp1
{
    public partial class MainForm : Telerik.WinControls.UI.RadForm
    {
        public MainForm()
        {
            InitializeComponent();
        }

        // Navigate to Setup Form
        private void btnSetupForm_Click(object sender, EventArgs e)
        {
            // Check if SetupForm exists in memory
            SetupForm setupForm = new SetupForm();
            setupForm.Show();
            
        }

        // Navigate to Check Form
        private void btnCheckForm_Click(object sender, EventArgs e)
        {
            CheckForm checkForm = new CheckForm();
            checkForm.Show();
            
        }
    }
}
