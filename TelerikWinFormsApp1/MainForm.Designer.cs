namespace TelerikWinFormsApp1
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            Telerik.WinControls.UI.TableViewDefinition tableViewDefinition3 = new Telerik.WinControls.UI.TableViewDefinition();
            this.radLabel1 = new Telerik.WinControls.UI.RadLabel();
            this.gridLeaderboard = new Telerik.WinControls.UI.RadGridView();
            this.btnSetupForm = new Telerik.WinControls.UI.RadButton();
            this.btnCheckForm = new Telerik.WinControls.UI.RadButton();
            this.btnResetTimer = new Telerik.WinControls.UI.RadButton();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.chkEnableBuzzer = new Telerik.WinControls.UI.RadCheckBox();
            this.radCheckBox2 = new Telerik.WinControls.UI.RadCheckBox();
            this.btnExportPDF = new Telerik.WinControls.UI.RadButton();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLeaderboard)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLeaderboard.MasterTemplate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSetupForm)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnCheckForm)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnResetTimer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkEnableBuzzer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radCheckBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnExportPDF)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // radLabel1
            // 
            this.radLabel1.Font = new System.Drawing.Font("DS-Digital", 100F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.radLabel1.ForeColor = System.Drawing.Color.RoyalBlue;
            this.radLabel1.Location = new System.Drawing.Point(493, 3);
            this.radLabel1.Name = "radLabel1";
            this.radLabel1.Size = new System.Drawing.Size(421, 150);
            this.radLabel1.TabIndex = 0;
            this.radLabel1.Text = "00.000";
            // 
            // gridLeaderboard
            // 
            this.gridLeaderboard.Location = new System.Drawing.Point(47, 137);
            // 
            // 
            // 
            this.gridLeaderboard.MasterTemplate.ViewDefinition = tableViewDefinition3;
            this.gridLeaderboard.Name = "gridLeaderboard";
            this.gridLeaderboard.Size = new System.Drawing.Size(1287, 444);
            this.gridLeaderboard.TabIndex = 1;
            // 
            // btnSetupForm
            // 
            this.btnSetupForm.Location = new System.Drawing.Point(1041, 687);
            this.btnSetupForm.Name = "btnSetupForm";
            this.btnSetupForm.Size = new System.Drawing.Size(137, 30);
            this.btnSetupForm.TabIndex = 2;
            this.btnSetupForm.Text = "Go to Setup";
            this.btnSetupForm.Click += new System.EventHandler(this.btnSetupForm_Click);
            // 
            // btnCheckForm
            // 
            this.btnCheckForm.Location = new System.Drawing.Point(1197, 687);
            this.btnCheckForm.Name = "btnCheckForm";
            this.btnCheckForm.Size = new System.Drawing.Size(137, 30);
            this.btnCheckForm.TabIndex = 3;
            this.btnCheckForm.Text = "Go to Check";
            this.btnCheckForm.Click += new System.EventHandler(this.btnCheckForm_Click);
            // 
            // btnResetTimer
            // 
            this.btnResetTimer.Font = new System.Drawing.Font("Segoe UI", 19.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnResetTimer.Location = new System.Drawing.Point(554, 587);
            this.btnResetTimer.Name = "btnResetTimer";
            this.btnResetTimer.Size = new System.Drawing.Size(287, 82);
            this.btnResetTimer.TabIndex = 4;
            this.btnResetTimer.Text = "Reset Timer ";
            // 
            // chkEnableBuzzer
            // 
            this.chkEnableBuzzer.Location = new System.Drawing.Point(1078, 587);
            this.chkEnableBuzzer.Name = "chkEnableBuzzer";
            this.chkEnableBuzzer.Size = new System.Drawing.Size(80, 18);
            this.chkEnableBuzzer.TabIndex = 5;
            this.chkEnableBuzzer.Text = "Play buzzer";
            // 
            // radCheckBox2
            // 
            this.radCheckBox2.Location = new System.Drawing.Point(1078, 611);
            this.radCheckBox2.Name = "radCheckBox2";
            this.radCheckBox2.Size = new System.Drawing.Size(256, 18);
            this.radCheckBox2.TabIndex = 6;
            this.radCheckBox2.Text = "Save leaderboard automatically (timestamped)";
            // 
            // btnExportPDF
            // 
            this.btnExportPDF.Location = new System.Drawing.Point(885, 687);
            this.btnExportPDF.Name = "btnExportPDF";
            this.btnExportPDF.Size = new System.Drawing.Size(137, 30);
            this.btnExportPDF.TabIndex = 3;
            this.btnExportPDF.Text = "Export as PDF";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1408, 729);
            this.Controls.Add(this.btnExportPDF);
            this.Controls.Add(this.radCheckBox2);
            this.Controls.Add(this.chkEnableBuzzer);
            this.Controls.Add(this.btnResetTimer);
            this.Controls.Add(this.btnCheckForm);
            this.Controls.Add(this.btnSetupForm);
            this.Controls.Add(this.gridLeaderboard);
            this.Controls.Add(this.radLabel1);
            this.Name = "MainForm";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.Text = "Qiuz Manager";
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLeaderboard.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLeaderboard)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSetupForm)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnCheckForm)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnResetTimer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkEnableBuzzer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radCheckBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnExportPDF)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Telerik.WinControls.UI.RadLabel radLabel1;
        private Telerik.WinControls.UI.RadGridView gridLeaderboard;
        private Telerik.WinControls.UI.RadButton btnSetupForm;
        private Telerik.WinControls.UI.RadButton btnCheckForm;
        private Telerik.WinControls.UI.RadButton btnResetTimer;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private Telerik.WinControls.UI.RadCheckBox chkEnableBuzzer;
        private Telerik.WinControls.UI.RadCheckBox radCheckBox2;
        private Telerik.WinControls.UI.RadButton btnExportPDF;
    }
}
