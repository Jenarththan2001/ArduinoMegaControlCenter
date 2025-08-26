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
            this.components = new System.ComponentModel.Container();
            Telerik.WinControls.UI.TableViewDefinition tableViewDefinition1 = new Telerik.WinControls.UI.TableViewDefinition();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.lblTimer = new Telerik.WinControls.UI.RadLabel();
            this.gridLeaderboard = new Telerik.WinControls.UI.RadGridView();
            this.btnSetupForm = new Telerik.WinControls.UI.RadButton();
            this.btnCheckForm = new Telerik.WinControls.UI.RadButton();
            this.btnResetTimer = new Telerik.WinControls.UI.RadButton();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.btnExportCSV = new Telerik.WinControls.UI.RadButton();
            this.radThemeManager1 = new Telerik.WinControls.RadThemeManager();
            this.telerikMetroTheme1 = new Telerik.WinControls.Themes.TelerikMetroTheme();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.fluentTheme1 = new Telerik.WinControls.Themes.FluentTheme();
            this.fluentTheme2 = new Telerik.WinControls.Themes.FluentTheme();
            this.telerikMetroBlueTheme1 = new Telerik.WinControls.Themes.TelerikMetroBlueTheme();
            ((System.ComponentModel.ISupportInitialize)(this.lblTimer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLeaderboard)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLeaderboard.MasterTemplate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSetupForm)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnCheckForm)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnResetTimer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnExportCSV)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTimer
            // 
            this.lblTimer.Font = new System.Drawing.Font("DS-Digital", 120F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTimer.ForeColor = System.Drawing.Color.DodgerBlue;
            this.lblTimer.Location = new System.Drawing.Point(546, 209);
            this.lblTimer.Name = "lblTimer";
            this.lblTimer.Size = new System.Drawing.Size(505, 180);
            this.lblTimer.TabIndex = 0;
            this.lblTimer.Text = "00.000";
            this.lblTimer.ThemeName = "TelerikMetroBlue";
            // 
            // gridLeaderboard
            // 
            this.gridLeaderboard.AutoSizeRows = true;
            this.gridLeaderboard.Cursor = System.Windows.Forms.Cursors.Default;
            this.gridLeaderboard.Font = new System.Drawing.Font("Segoe UI", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridLeaderboard.Location = new System.Drawing.Point(53, 391);
            // 
            // 
            // 
            this.gridLeaderboard.MasterTemplate.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill;
            this.gridLeaderboard.MasterTemplate.ViewDefinition = tableViewDefinition1;
            this.gridLeaderboard.Name = "gridLeaderboard";
            this.gridLeaderboard.ReadOnly = true;
            this.gridLeaderboard.Size = new System.Drawing.Size(1382, 222);
            this.gridLeaderboard.TabIndex = 1;
            this.gridLeaderboard.ThemeName = "TelerikMetroBlue";
            // 
            // btnSetupForm
            // 
            this.btnSetupForm.Location = new System.Drawing.Point(1095, 708);
            this.btnSetupForm.Name = "btnSetupForm";
            this.btnSetupForm.Size = new System.Drawing.Size(137, 30);
            this.btnSetupForm.TabIndex = 2;
            this.btnSetupForm.Text = "Go to Setup";
            this.btnSetupForm.ThemeName = "TelerikMetroBlue";
            this.btnSetupForm.Click += new System.EventHandler(this.btnSetupForm_Click);
            // 
            // btnCheckForm
            // 
            this.btnCheckForm.Location = new System.Drawing.Point(1251, 708);
            this.btnCheckForm.Name = "btnCheckForm";
            this.btnCheckForm.Size = new System.Drawing.Size(137, 30);
            this.btnCheckForm.TabIndex = 3;
            this.btnCheckForm.Text = "Go to Check";
            this.btnCheckForm.ThemeName = "TelerikMetroBlue";
            this.btnCheckForm.Click += new System.EventHandler(this.btnCheckForm_Click);
            // 
            // btnResetTimer
            // 
            this.btnResetTimer.Font = new System.Drawing.Font("Segoe UI", 19.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnResetTimer.Location = new System.Drawing.Point(609, 619);
            this.btnResetTimer.Name = "btnResetTimer";
            this.btnResetTimer.Size = new System.Drawing.Size(287, 82);
            this.btnResetTimer.TabIndex = 4;
            this.btnResetTimer.Text = "Reset Timer ";
            this.btnResetTimer.ThemeName = "TelerikMetroBlue";
            // 
            // btnExportCSV
            // 
            this.btnExportCSV.Location = new System.Drawing.Point(939, 708);
            this.btnExportCSV.Name = "btnExportCSV";
            this.btnExportCSV.Size = new System.Drawing.Size(137, 30);
            this.btnExportCSV.TabIndex = 3;
            this.btnExportCSV.Text = "Export as CSV";
            this.btnExportCSV.ThemeName = "TelerikMetroBlue";
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(365, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(711, 206);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 5;
            this.pictureBox1.TabStop = false;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1447, 754);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.btnExportCSV);
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
            this.ThemeName = "TelerikMetroBlue";
            ((System.ComponentModel.ISupportInitialize)(this.lblTimer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLeaderboard.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLeaderboard)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSetupForm)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnCheckForm)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnResetTimer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnExportCSV)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
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
        private Telerik.WinControls.UI.RadButton btnExportCSV;
        private Telerik.WinControls.RadThemeManager radThemeManager1;
        private Telerik.WinControls.Themes.TelerikMetroTheme telerikMetroTheme1;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private Telerik.WinControls.Themes.FluentTheme fluentTheme1;
        private Telerik.WinControls.Themes.FluentTheme fluentTheme2;
        private Telerik.WinControls.Themes.TelerikMetroBlueTheme telerikMetroBlueTheme1;
    }
}
