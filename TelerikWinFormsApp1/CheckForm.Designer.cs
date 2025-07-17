namespace TelerikWinFormsApp1
{
    partial class CheckForm
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
            Telerik.WinControls.UI.TableViewDefinition tableViewDefinition1 = new Telerik.WinControls.UI.TableViewDefinition();
            this.radLabel1 = new Telerik.WinControls.UI.RadLabel();
            this.lblComPort = new Telerik.WinControls.UI.RadLabel();
            this.cmbComPorts = new System.Windows.Forms.ComboBox();
            this.btnConnect = new Telerik.WinControls.UI.RadButton();
            this.btnDisconnect = new Telerik.WinControls.UI.RadButton();
            this.lblConnectionStatus = new Telerik.WinControls.UI.RadLabel();
            this.lstLogs = new Telerik.WinControls.UI.RadListControl();
            this.gridButtons = new Telerik.WinControls.UI.RadGridView();
            this.btnClearLogs = new Telerik.WinControls.UI.RadButton();
            this.btnReset = new Telerik.WinControls.UI.RadButton();
            this.btnBuzzerCheck = new Telerik.WinControls.UI.RadButton();
            this.lblResetButtonStatus = new Telerik.WinControls.UI.RadLabel();
            this.btnTeam1Led = new Telerik.WinControls.UI.RadButton();
            this.btnTeam2Led = new Telerik.WinControls.UI.RadButton();
            this.btnTeam3Led = new Telerik.WinControls.UI.RadButton();
            this.btnTeam4Led = new Telerik.WinControls.UI.RadButton();
            this.btnTeam5Led = new Telerik.WinControls.UI.RadButton();
            this.chkTestMode = new Telerik.WinControls.UI.RadCheckBox();
            this.chkLiveMonitor = new Telerik.WinControls.UI.RadCheckBox();
            this.btnRefreshPorts = new Telerik.WinControls.UI.RadButton();
            this.btnSaveLogs = new Telerik.WinControls.UI.RadButton();
            this.statusStrip = new Telerik.WinControls.UI.RadStatusStrip();
            this.lblComStatus = new Telerik.WinControls.UI.RadLabelElement();
            this.lblLastMsg = new Telerik.WinControls.UI.RadLabelElement();
            this.lblTimestamp = new Telerik.WinControls.UI.RadLabelElement();
            this.txtDeviceInfo = new Telerik.WinControls.UI.RadLabelElement();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblComPort)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnConnect)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnDisconnect)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblConnectionStatus)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lstLogs)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridButtons)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridButtons.MasterTemplate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnClearLogs)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnReset)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnBuzzerCheck)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblResetButtonStatus)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnTeam1Led)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnTeam2Led)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnTeam3Led)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnTeam4Led)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnTeam5Led)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkTestMode)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkLiveMonitor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnRefreshPorts)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveLogs)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.statusStrip)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // radLabel1
            // 
            this.radLabel1.Location = new System.Drawing.Point(43, 27);
            this.radLabel1.Name = "radLabel1";
            this.radLabel1.Size = new System.Drawing.Size(55, 18);
            this.radLabel1.TabIndex = 0;
            this.radLabel1.Text = "radLabel1";
            // 
            // lblComPort
            // 
            this.lblComPort.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblComPort.Location = new System.Drawing.Point(26, 30);
            this.lblComPort.Name = "lblComPort";
            this.lblComPort.Size = new System.Drawing.Size(107, 21);
            this.lblComPort.TabIndex = 1;
            this.lblComPort.Text = "Select COM Port";
            // 
            // cmbComPorts
            // 
            this.cmbComPorts.FormattingEnabled = true;
            this.cmbComPorts.Location = new System.Drawing.Point(139, 30);
            this.cmbComPorts.Name = "cmbComPorts";
            this.cmbComPorts.Size = new System.Drawing.Size(151, 24);
            this.cmbComPorts.TabIndex = 2;
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(313, 27);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(137, 30);
            this.btnConnect.TabIndex = 3;
            this.btnConnect.Text = "Connect";
            // 
            // btnDisconnect
            // 
            this.btnDisconnect.Enabled = false;
            this.btnDisconnect.Location = new System.Drawing.Point(467, 27);
            this.btnDisconnect.Name = "btnDisconnect";
            this.btnDisconnect.Size = new System.Drawing.Size(137, 30);
            this.btnDisconnect.TabIndex = 4;
            this.btnDisconnect.Text = "Disconnect";
            // 
            // lblConnectionStatus
            // 
            this.lblConnectionStatus.Location = new System.Drawing.Point(624, 33);
            this.lblConnectionStatus.Name = "lblConnectionStatus";
            this.lblConnectionStatus.Size = new System.Drawing.Size(74, 18);
            this.lblConnectionStatus.TabIndex = 5;
            this.lblConnectionStatus.Text = "Disconnected";
            // 
            // lstLogs
            // 
            this.lstLogs.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lstLogs.ItemHeight = 24;
            this.lstLogs.Location = new System.Drawing.Point(26, 84);
            this.lstLogs.Name = "lstLogs";
            this.lstLogs.Size = new System.Drawing.Size(745, 82);
            this.lstLogs.TabIndex = 6;
            // 
            // gridButtons
            // 
            this.gridButtons.Location = new System.Drawing.Point(26, 219);
            // 
            // 
            // 
            this.gridButtons.MasterTemplate.ViewDefinition = tableViewDefinition1;
            this.gridButtons.Name = "gridButtons";
            this.gridButtons.Size = new System.Drawing.Size(745, 198);
            this.gridButtons.TabIndex = 7;
            // 
            // btnClearLogs
            // 
            this.btnClearLogs.Location = new System.Drawing.Point(634, 172);
            this.btnClearLogs.Name = "btnClearLogs";
            this.btnClearLogs.Size = new System.Drawing.Size(137, 30);
            this.btnClearLogs.TabIndex = 8;
            this.btnClearLogs.Text = "Clear Logs";
            // 
            // btnReset
            // 
            this.btnReset.Location = new System.Drawing.Point(634, 423);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(137, 30);
            this.btnReset.TabIndex = 9;
            this.btnReset.Text = "Send RESET";
            // 
            // btnBuzzerCheck
            // 
            this.btnBuzzerCheck.Location = new System.Drawing.Point(26, 481);
            this.btnBuzzerCheck.Name = "btnBuzzerCheck";
            this.btnBuzzerCheck.Size = new System.Drawing.Size(137, 30);
            this.btnBuzzerCheck.TabIndex = 12;
            this.btnBuzzerCheck.Text = "Test Buzzer";
            // 
            // lblResetButtonStatus
            // 
            this.lblResetButtonStatus.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblResetButtonStatus.Location = new System.Drawing.Point(266, 425);
            this.lblResetButtonStatus.Name = "lblResetButtonStatus";
            this.lblResetButtonStatus.Size = new System.Drawing.Size(172, 19);
            this.lblResetButtonStatus.TabIndex = 13;
            this.lblResetButtonStatus.Text = "Operator Reset: NOT PRESSED";
            this.lblResetButtonStatus.Click += new System.EventHandler(this.lblResetButtonStatus_Click);
            // 
            // btnTeam1Led
            // 
            this.btnTeam1Led.Location = new System.Drawing.Point(26, 536);
            this.btnTeam1Led.Name = "btnTeam1Led";
            this.btnTeam1Led.Size = new System.Drawing.Size(137, 30);
            this.btnTeam1Led.TabIndex = 0;
            this.btnTeam1Led.Text = "LED 1";
            // 
            // btnTeam2Led
            // 
            this.btnTeam2Led.Location = new System.Drawing.Point(181, 536);
            this.btnTeam2Led.Name = "btnTeam2Led";
            this.btnTeam2Led.Size = new System.Drawing.Size(137, 30);
            this.btnTeam2Led.TabIndex = 1;
            this.btnTeam2Led.Text = "LED 2";
            // 
            // btnTeam3Led
            // 
            this.btnTeam3Led.Location = new System.Drawing.Point(324, 536);
            this.btnTeam3Led.Name = "btnTeam3Led";
            this.btnTeam3Led.Size = new System.Drawing.Size(137, 30);
            this.btnTeam3Led.TabIndex = 2;
            this.btnTeam3Led.Text = "LED 3";
            // 
            // btnTeam4Led
            // 
            this.btnTeam4Led.Location = new System.Drawing.Point(479, 536);
            this.btnTeam4Led.Name = "btnTeam4Led";
            this.btnTeam4Led.Size = new System.Drawing.Size(137, 30);
            this.btnTeam4Led.TabIndex = 3;
            this.btnTeam4Led.Text = "LED 4";
            // 
            // btnTeam5Led
            // 
            this.btnTeam5Led.Location = new System.Drawing.Point(634, 536);
            this.btnTeam5Led.Name = "btnTeam5Led";
            this.btnTeam5Led.Size = new System.Drawing.Size(137, 30);
            this.btnTeam5Led.TabIndex = 4;
            this.btnTeam5Led.Text = "LED 5";
            // 
            // chkTestMode
            // 
            this.chkTestMode.Location = new System.Drawing.Point(26, 426);
            this.chkTestMode.Name = "chkTestMode";
            this.chkTestMode.Size = new System.Drawing.Size(149, 18);
            this.chkTestMode.TabIndex = 1;
            this.chkTestMode.Text = "Enable Button Test Mode";
            // 
            // chkLiveMonitor
            // 
            this.chkLiveMonitor.Location = new System.Drawing.Point(27, 172);
            this.chkLiveMonitor.Name = "chkLiveMonitor";
            this.chkLiveMonitor.Size = new System.Drawing.Size(86, 18);
            this.chkLiveMonitor.TabIndex = 2;
            this.chkLiveMonitor.Text = "Live Monitor";
            // 
            // btnRefreshPorts
            // 
            this.btnRefreshPorts.Location = new System.Drawing.Point(181, 481);
            this.btnRefreshPorts.Name = "btnRefreshPorts";
            this.btnRefreshPorts.Size = new System.Drawing.Size(137, 30);
            this.btnRefreshPorts.TabIndex = 13;
            this.btnRefreshPorts.Text = "Refresh COM Ports";
            // 
            // btnSaveLogs
            // 
            this.btnSaveLogs.Location = new System.Drawing.Point(324, 481);
            this.btnSaveLogs.Name = "btnSaveLogs";
            this.btnSaveLogs.Size = new System.Drawing.Size(137, 30);
            this.btnSaveLogs.TabIndex = 14;
            this.btnSaveLogs.Text = "Save Logs";
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new Telerik.WinControls.RadItem[] {
            this.lblComStatus,
            this.lblLastMsg,
            this.lblTimestamp,
            this.txtDeviceInfo});
            this.statusStrip.Location = new System.Drawing.Point(0, 586);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(807, 24);
            this.statusStrip.TabIndex = 15;
            // 
            // lblComStatus
            // 
            this.lblComStatus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblComStatus.Name = "lblComStatus";
            this.statusStrip.SetSpring(this.lblComStatus, false);
            this.lblComStatus.Text = "COM: Disconnected";
            this.lblComStatus.TextWrap = true;
            // 
            // lblLastMsg
            // 
            this.lblLastMsg.Name = "lblLastMsg";
            this.statusStrip.SetSpring(this.lblLastMsg, false);
            this.lblLastMsg.Text = "Last Msg: —";
            this.lblLastMsg.TextWrap = true;
            // 
            // lblTimestamp
            // 
            this.lblTimestamp.Name = "lblTimestamp";
            this.statusStrip.SetSpring(this.lblTimestamp, false);
            this.lblTimestamp.Text = "Time: —";
            this.lblTimestamp.TextWrap = true;
            // 
            // txtDeviceInfo
            // 
            this.txtDeviceInfo.Name = "txtDeviceInfo";
            this.statusStrip.SetSpring(this.txtDeviceInfo, false);
            this.txtDeviceInfo.Text = "Device info will appear here...";
            this.txtDeviceInfo.TextWrap = true;
            // 
            // CheckForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(807, 610);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.btnSaveLogs);
            this.Controls.Add(this.btnRefreshPorts);
            this.Controls.Add(this.chkLiveMonitor);
            this.Controls.Add(this.chkTestMode);
            this.Controls.Add(this.btnTeam5Led);
            this.Controls.Add(this.btnTeam4Led);
            this.Controls.Add(this.btnTeam3Led);
            this.Controls.Add(this.btnTeam2Led);
            this.Controls.Add(this.btnTeam1Led);
            this.Controls.Add(this.lblResetButtonStatus);
            this.Controls.Add(this.btnBuzzerCheck);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.btnClearLogs);
            this.Controls.Add(this.gridButtons);
            this.Controls.Add(this.lstLogs);
            this.Controls.Add(this.lblConnectionStatus);
            this.Controls.Add(this.btnDisconnect);
            this.Controls.Add(this.btnConnect);
            this.Controls.Add(this.cmbComPorts);
            this.Controls.Add(this.lblComPort);
            this.Controls.Add(this.radLabel1);
            this.Name = "CheckForm";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.Text = "Check Form";
            this.Load += new System.EventHandler(this.CheckForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblComPort)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnConnect)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnDisconnect)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblConnectionStatus)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lstLogs)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridButtons.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridButtons)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnClearLogs)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnReset)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnBuzzerCheck)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblResetButtonStatus)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnTeam1Led)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnTeam2Led)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnTeam3Led)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnTeam4Led)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnTeam5Led)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkTestMode)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkLiveMonitor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnRefreshPorts)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveLogs)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.statusStrip)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Telerik.WinControls.UI.RadLabel radLabel1;
        private Telerik.WinControls.UI.RadLabel lblComPort;
        private System.Windows.Forms.ComboBox cmbComPorts;
        private Telerik.WinControls.UI.RadButton btnConnect;
        private Telerik.WinControls.UI.RadButton btnDisconnect;
        private Telerik.WinControls.UI.RadLabel lblConnectionStatus;
        private Telerik.WinControls.UI.RadListControl lstLogs;
        private Telerik.WinControls.UI.RadButton btnClearLogs;
        private Telerik.WinControls.UI.RadGridView gridButtons;
        private Telerik.WinControls.UI.RadButton btnReset;
        private Telerik.WinControls.UI.RadButton btnBuzzerCheck;
        private Telerik.WinControls.UI.RadLabel lblResetButtonStatus;
        private Telerik.WinControls.UI.RadButton btnTeam2Led;
        private Telerik.WinControls.UI.RadButton btnTeam1Led;
        private Telerik.WinControls.UI.RadButton btnTeam3Led;
        private Telerik.WinControls.UI.RadButton btnTeam4Led;
        private Telerik.WinControls.UI.RadButton btnTeam5Led;
        private Telerik.WinControls.UI.RadCheckBox chkTestMode;
        private Telerik.WinControls.UI.RadCheckBox chkLiveMonitor;
        private Telerik.WinControls.UI.RadButton btnRefreshPorts;
        private Telerik.WinControls.UI.RadButton btnSaveLogs;
        private Telerik.WinControls.UI.RadStatusStrip statusStrip;
        private Telerik.WinControls.UI.RadLabelElement lblComStatus;
        private Telerik.WinControls.UI.RadLabelElement lblLastMsg;
        private Telerik.WinControls.UI.RadLabelElement lblTimestamp;
        private Telerik.WinControls.UI.RadLabelElement txtDeviceInfo;
    }
}
