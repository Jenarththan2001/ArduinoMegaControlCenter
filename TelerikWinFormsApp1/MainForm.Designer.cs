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
            Telerik.WinControls.UI.TableViewDefinition tableViewDefinition2 = new Telerik.WinControls.UI.TableViewDefinition();
            this.lblTimer = new Telerik.WinControls.UI.RadLabel();
            this.gridLeaderboard = new Telerik.WinControls.UI.RadGridView();
            this.btnSetupForm = new Telerik.WinControls.UI.RadButton();
            this.btnCheckForm = new Telerik.WinControls.UI.RadButton();
            this.btnResetTimer = new Telerik.WinControls.UI.RadButton();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.btnExportPDF = new Telerik.WinControls.UI.RadButton();
            this.radThemeManager1 = new Telerik.WinControls.RadThemeManager();
            this.telerikMetroTheme1 = new Telerik.WinControls.Themes.TelerikMetroTheme();
            ((System.ComponentModel.ISupportInitialize)(this.lblTimer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLeaderboard)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLeaderboard.MasterTemplate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSetupForm)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnCheckForm)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnResetTimer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnExportPDF)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTimer
            // 
            this.lblTimer.Font = new System.Drawing.Font("DS-Digital", 100F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.lblTimer.ForeColor = System.Drawing.Color.DodgerBlue;
            this.lblTimer.Location = new System.Drawing.Point(493, 3);
            this.lblTimer.Name = "lblTimer";
            this.lblTimer.Size = new System.Drawing.Size(421, 150);
            this.lblTimer.TabIndex = 0;
            this.lblTimer.Text = "00.000";
            this.lblTimer.ThemeName = "ControlDefault";
            // 
            // gridLeaderboard
            // 
            this.gridLeaderboard.AutoScroll = true;
            this.gridLeaderboard.AutoSizeRows = true;
            this.gridLeaderboard.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridLeaderboard.Location = new System.Drawing.Point(47, 137);
            // 
            // 
            // 
            this.gridLeaderboard.MasterTemplate.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill;
            this.gridLeaderboard.MasterTemplate.ViewDefinition = tableViewDefinition2;
            this.gridLeaderboard.Name = "gridLeaderboard";
            this.gridLeaderboard.Size = new System.Drawing.Size(1287, 444);
            this.gridLeaderboard.TabIndex = 1;
            this.gridLeaderboard.ThemeName = "ControlDefault";
            // 
            // btnSetupForm
            // 
            this.btnSetupForm.Location = new System.Drawing.Point(1041, 687);
            this.btnSetupForm.Name = "btnSetupForm";
            this.btnSetupForm.Size = new System.Drawing.Size(137, 30);
            this.btnSetupForm.TabIndex = 2;
            this.btnSetupForm.Text = "Go to Setup";
            this.btnSetupForm.ThemeName = "ControlDefault";
            this.btnSetupForm.Click += new System.EventHandler(this.btnSetupForm_Click);
            // 
            // btnCheckForm
            // 
            this.btnCheckForm.Location = new System.Drawing.Point(1197, 687);
            this.btnCheckForm.Name = "btnCheckForm";
            this.btnCheckForm.Size = new System.Drawing.Size(137, 30);
            this.btnCheckForm.TabIndex = 3;
            this.btnCheckForm.Text = "Go to Check";
            this.btnCheckForm.ThemeName = "ControlDefault";
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
            this.btnResetTimer.ThemeName = "ControlDefault";
            // 
            // btnExportPDF
            // 
            this.btnExportPDF.Location = new System.Drawing.Point(885, 687);
            this.btnExportPDF.Name = "btnExportPDF";
            this.btnExportPDF.Size = new System.Drawing.Size(137, 30);
            this.btnExportPDF.TabIndex = 3;
            this.btnExportPDF.Text = "Export as PDF";
            this.btnExportPDF.ThemeName = "ControlDefault";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1408, 729);
            this.Controls.Add(this.btnExportPDF);
            this.Controls.Add(this.btnResetTimer);
            this.Controls.Add(this.btnCheckForm);
            this.Controls.Add(this.btnSetupForm);
            this.Controls.Add(this.gridLeaderboard);
            this.Controls.Add(this.lblTimer);
            this.Name = "MainForm";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.Text = "Quiz Manager";
            this.ThemeName = "ControlDefault";
            ((System.ComponentModel.ISupportInitialize)(this.lblTimer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLeaderboard.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLeaderboard)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSetupForm)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnCheckForm)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnResetTimer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnExportPDF)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Telerik.WinControls.UI.RadLabel lblTimer;
        private Telerik.WinControls.UI.RadGridView gridLeaderboard;
        private Telerik.WinControls.UI.RadButton btnSetupForm;
        private Telerik.WinControls.UI.RadButton btnCheckForm;
        private Telerik.WinControls.UI.RadButton btnResetTimer;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private Telerik.WinControls.UI.RadButton btnExportPDF;
        private Telerik.WinControls.RadThemeManager radThemeManager1;
        private Telerik.WinControls.Themes.TelerikMetroTheme telerikMetroTheme1;
    }
}
