namespace Ogama.Modules.Recording.ASL
{
#if ASL
    partial class aslSettingsDialog
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
        this.pictureBox1 = new System.Windows.Forms.PictureBox();
        this.ASLSettingToolTip = new System.Windows.Forms.ToolTip(this.components);
        this.btnBrowseLog = new System.Windows.Forms.Button();
        this.txtLogFile = new System.Windows.Forms.TextBox();
        this.bWriteLogFile = new System.Windows.Forms.CheckBox();
        this.lvLog = new System.Windows.Forms.ListView();
        this.btnConnect = new System.Windows.Forms.Button();
        this.btnDisconnect = new System.Windows.Forms.Button();
        this.timerUpdate = new System.Windows.Forms.Timer(this.components);
        this.btnRestoreDefaults = new System.Windows.Forms.Button();
        this.btnStartStopContinuous = new System.Windows.Forms.Button();
        this.btnGetRecord = new System.Windows.Forms.Button();
        this.btnBrowseConfig = new System.Windows.Forms.Button();
        this.txtConfigFile = new System.Windows.Forms.TextBox();
        this.label2 = new System.Windows.Forms.Label();
        this.gbReadOptions = new System.Windows.Forms.GroupBox();
        this.txtBaudRate = new System.Windows.Forms.TextBox();
        this.label3 = new System.Windows.Forms.Label();
        this.bStreaming = new System.Windows.Forms.CheckBox();
        this.bEyeHead = new System.Windows.Forms.CheckBox();
        this.cbPort = new System.Windows.Forms.ComboBox();
        this.label4 = new System.Windows.Forms.Label();
        this.cbWarning = new System.Windows.Forms.CheckBox();
        this.lblErrorStreamingMode = new System.Windows.Forms.Label();
        ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
        this.gbReadOptions.SuspendLayout();
        this.SuspendLayout();
        // 
        // pictureBox1
        // 
        this.pictureBox1.Image = global::Ogama.Properties.Resources.ASLlogo;
        this.pictureBox1.Location = new System.Drawing.Point(12, 12);
        this.pictureBox1.Name = "pictureBox1";
        this.pictureBox1.Size = new System.Drawing.Size(115, 69);
        this.pictureBox1.TabIndex = 0;
        this.pictureBox1.TabStop = false;
        // 
        // ASLSettingToolTip
        // 
        this.ASLSettingToolTip.AutomaticDelay = 250;
        this.ASLSettingToolTip.AutoPopDelay = 10000;
        this.ASLSettingToolTip.InitialDelay = 250;
        this.ASLSettingToolTip.ReshowDelay = 50;
        // 
        // btnBrowseLog
        // 
        this.btnBrowseLog.AutoSize = true;
        this.btnBrowseLog.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
        this.btnBrowseLog.Location = new System.Drawing.Point(384, 200);
        this.btnBrowseLog.Name = "btnBrowseLog";
        this.btnBrowseLog.Size = new System.Drawing.Size(26, 23);
        this.btnBrowseLog.TabIndex = 41;
        this.btnBrowseLog.Text = "...";
        this.btnBrowseLog.UseVisualStyleBackColor = true;
        this.btnBrowseLog.Click += new System.EventHandler(this.btnBrowseLog_Click);
        // 
        // txtLogFile
        // 
        this.txtLogFile.Location = new System.Drawing.Point(111, 203);
        this.txtLogFile.Name = "txtLogFile";
        this.txtLogFile.Size = new System.Drawing.Size(267, 20);
        this.txtLogFile.TabIndex = 40;
        // 
        // bWriteLogFile
        // 
        this.bWriteLogFile.AutoSize = true;
        this.bWriteLogFile.Location = new System.Drawing.Point(12, 205);
        this.bWriteLogFile.Name = "bWriteLogFile";
        this.bWriteLogFile.RightToLeft = System.Windows.Forms.RightToLeft.No;
        this.bWriteLogFile.Size = new System.Drawing.Size(93, 17);
        this.bWriteLogFile.TabIndex = 39;
        this.bWriteLogFile.Text = "Write to logfile";
        this.bWriteLogFile.UseVisualStyleBackColor = true;
        this.bWriteLogFile.CheckedChanged += new System.EventHandler(this.CheckedChanged);
        // 
        // lvLog
        // 
        this.lvLog.Location = new System.Drawing.Point(11, 236);
        this.lvLog.Name = "lvLog";
        this.lvLog.Size = new System.Drawing.Size(651, 219);
        this.lvLog.TabIndex = 38;
        this.lvLog.UseCompatibleStateImageBehavior = false;
        this.lvLog.View = System.Windows.Forms.View.Details;
        // 
        // btnConnect
        // 
        this.btnConnect.Location = new System.Drawing.Point(12, 164);
        this.btnConnect.Name = "btnConnect";
        this.btnConnect.Size = new System.Drawing.Size(73, 25);
        this.btnConnect.TabIndex = 37;
        this.btnConnect.Text = "Connect";
        this.btnConnect.UseVisualStyleBackColor = true;
        this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
        // 
        // btnDisconnect
        // 
        this.btnDisconnect.Location = new System.Drawing.Point(91, 164);
        this.btnDisconnect.Name = "btnDisconnect";
        this.btnDisconnect.Size = new System.Drawing.Size(73, 25);
        this.btnDisconnect.TabIndex = 36;
        this.btnDisconnect.Text = "Disconnect";
        this.btnDisconnect.UseVisualStyleBackColor = true;
        this.btnDisconnect.Click += new System.EventHandler(this.btnDisconnect_Click);
        // 
        // timerUpdate
        // 
        this.timerUpdate.Interval = 20;
        this.timerUpdate.Tick += new System.EventHandler(this.timerUpdate_Tick);
        // 
        // btnRestoreDefaults
        // 
        this.btnRestoreDefaults.AutoSize = true;
        this.btnRestoreDefaults.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
        this.btnRestoreDefaults.Location = new System.Drawing.Point(552, 16);
        this.btnRestoreDefaults.Name = "btnRestoreDefaults";
        this.btnRestoreDefaults.Size = new System.Drawing.Size(110, 23);
        this.btnRestoreDefaults.TabIndex = 34;
        this.btnRestoreDefaults.Text = "Restore Default File";
        this.btnRestoreDefaults.UseVisualStyleBackColor = true;
        this.btnRestoreDefaults.Click += new System.EventHandler(this.btnRestoreDefaults_Click);
        // 
        // btnStartStopContinuous
        // 
        this.btnStartStopContinuous.Location = new System.Drawing.Point(277, 164);
        this.btnStartStopContinuous.Name = "btnStartStopContinuous";
        this.btnStartStopContinuous.Size = new System.Drawing.Size(133, 25);
        this.btnStartStopContinuous.TabIndex = 33;
        this.btnStartStopContinuous.Text = "Start Continuous Read";
        this.btnStartStopContinuous.UseVisualStyleBackColor = true;
        this.btnStartStopContinuous.Click += new System.EventHandler(this.btnStartStopContinuous_Click);
        // 
        // btnGetRecord
        // 
        this.btnGetRecord.Location = new System.Drawing.Point(169, 164);
        this.btnGetRecord.Name = "btnGetRecord";
        this.btnGetRecord.Size = new System.Drawing.Size(102, 25);
        this.btnGetRecord.TabIndex = 32;
        this.btnGetRecord.Text = "Get Data Record";
        this.btnGetRecord.UseVisualStyleBackColor = true;
        this.btnGetRecord.Click += new System.EventHandler(this.btnGetRecord_Click);
        // 
        // btnBrowseConfig
        // 
        this.btnBrowseConfig.AutoSize = true;
        this.btnBrowseConfig.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
        this.btnBrowseConfig.Location = new System.Drawing.Point(520, 16);
        this.btnBrowseConfig.Name = "btnBrowseConfig";
        this.btnBrowseConfig.Size = new System.Drawing.Size(26, 23);
        this.btnBrowseConfig.TabIndex = 28;
        this.btnBrowseConfig.Text = "...";
        this.btnBrowseConfig.UseVisualStyleBackColor = true;
        this.btnBrowseConfig.Click += new System.EventHandler(this.btnBrowseConfig_Click);
        // 
        // txtConfigFile
        // 
        this.txtConfigFile.Location = new System.Drawing.Point(247, 18);
        this.txtConfigFile.Name = "txtConfigFile";
        this.txtConfigFile.Size = new System.Drawing.Size(267, 20);
        this.txtConfigFile.TabIndex = 27;
        // 
        // label2
        // 
        this.label2.Location = new System.Drawing.Point(132, 12);
        this.label2.Name = "label2";
        this.label2.Size = new System.Drawing.Size(109, 31);
        this.label2.TabIndex = 26;
        this.label2.Text = "Choose Eye Tracker configuration file";
        this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
        // 
        // gbReadOptions
        // 
        this.gbReadOptions.Controls.Add(this.txtBaudRate);
        this.gbReadOptions.Controls.Add(this.label3);
        this.gbReadOptions.Controls.Add(this.bStreaming);
        this.gbReadOptions.Controls.Add(this.bEyeHead);
        this.gbReadOptions.Controls.Add(this.cbPort);
        this.gbReadOptions.Controls.Add(this.label4);
        this.gbReadOptions.Enabled = false;
        this.gbReadOptions.Location = new System.Drawing.Point(11, 86);
        this.gbReadOptions.Name = "gbReadOptions";
        this.gbReadOptions.Size = new System.Drawing.Size(651, 53);
        this.gbReadOptions.TabIndex = 49;
        this.gbReadOptions.TabStop = false;
        this.gbReadOptions.Text = "Read options (Update when pressing the button <Connect>)";
        // 
        // txtBaudRate
        // 
        this.txtBaudRate.Location = new System.Drawing.Point(209, 19);
        this.txtBaudRate.Name = "txtBaudRate";
        this.txtBaudRate.ReadOnly = true;
        this.txtBaudRate.Size = new System.Drawing.Size(50, 20);
        this.txtBaudRate.TabIndex = 54;
        this.txtBaudRate.Text = "57600";
        // 
        // label3
        // 
        this.label3.AutoSize = true;
        this.label3.Location = new System.Drawing.Point(145, 23);
        this.label3.Name = "label3";
        this.label3.Size = new System.Drawing.Size(58, 13);
        this.label3.TabIndex = 53;
        this.label3.Text = "Baud Rate";
        // 
        // bStreaming
        // 
        this.bStreaming.AutoSize = true;
        this.bStreaming.Location = new System.Drawing.Point(275, 21);
        this.bStreaming.Name = "bStreaming";
        this.bStreaming.Size = new System.Drawing.Size(103, 17);
        this.bStreaming.TabIndex = 52;
        this.bStreaming.Text = "Streaming Mode";
        this.bStreaming.UseVisualStyleBackColor = true;
        this.bStreaming.CheckedChanged += new System.EventHandler(this.bStreaming_CheckedChanged);
        // 
        // bEyeHead
        // 
        this.bEyeHead.AutoSize = true;
        this.bEyeHead.Location = new System.Drawing.Point(385, 21);
        this.bEyeHead.Name = "bEyeHead";
        this.bEyeHead.Size = new System.Drawing.Size(123, 17);
        this.bEyeHead.TabIndex = 51;
        this.bEyeHead.Text = "EyeHead Integration";
        this.bEyeHead.UseVisualStyleBackColor = true;
        // 
        // cbPort
        // 
        this.cbPort.FormattingEnabled = true;
        this.cbPort.Items.AddRange(new object[] {
            "COM1",
            "COM2",
            "COM3",
            "COM4",
            "COM5",
            "COM6",
            "COM7",
            "COM8",
            "COM9",
            "COM10"});
        this.cbPort.Location = new System.Drawing.Point(69, 20);
        this.cbPort.Name = "cbPort";
        this.cbPort.Size = new System.Drawing.Size(68, 21);
        this.cbPort.TabIndex = 50;
        // 
        // label4
        // 
        this.label4.AutoSize = true;
        this.label4.Location = new System.Drawing.Point(8, 23);
        this.label4.Name = "label4";
        this.label4.Size = new System.Drawing.Size(55, 13);
        this.label4.TabIndex = 49;
        this.label4.Text = "Serial Port";
        // 
        // cbWarning
        // 
        this.cbWarning.AutoSize = true;
        this.cbWarning.Checked = true;
        this.cbWarning.CheckState = System.Windows.Forms.CheckState.Checked;
        this.cbWarning.Location = new System.Drawing.Point(135, 54);
        this.cbWarning.Name = "cbWarning";
        this.cbWarning.Size = new System.Drawing.Size(281, 17);
        this.cbWarning.TabIndex = 50;
        this.cbWarning.Text = "Display a warning when using default configuration file";
        this.cbWarning.UseVisualStyleBackColor = true;
        this.cbWarning.CheckedChanged += new System.EventHandler(this.cbWarning_CheckedChanged);
        // 
        // lblErrorStreamingMode
        // 
        this.lblErrorStreamingMode.AutoSize = true;
        this.lblErrorStreamingMode.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.lblErrorStreamingMode.ForeColor = System.Drawing.Color.Red;
        this.lblErrorStreamingMode.Location = new System.Drawing.Point(19, 142);
        this.lblErrorStreamingMode.Name = "lblErrorStreamingMode";
        this.lblErrorStreamingMode.Size = new System.Drawing.Size(179, 13);
        this.lblErrorStreamingMode.TabIndex = 51;
        this.lblErrorStreamingMode.Text = "Error : You must use streaming-mode";
        // 
        // aslSettingsDialog
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(670, 467);
        this.Controls.Add(this.lblErrorStreamingMode);
        this.Controls.Add(this.cbWarning);
        this.Controls.Add(this.gbReadOptions);
        this.Controls.Add(this.btnBrowseLog);
        this.Controls.Add(this.txtLogFile);
        this.Controls.Add(this.bWriteLogFile);
        this.Controls.Add(this.lvLog);
        this.Controls.Add(this.btnConnect);
        this.Controls.Add(this.btnDisconnect);
        this.Controls.Add(this.btnRestoreDefaults);
        this.Controls.Add(this.btnStartStopContinuous);
        this.Controls.Add(this.btnGetRecord);
        this.Controls.Add(this.btnBrowseConfig);
        this.Controls.Add(this.txtConfigFile);
        this.Controls.Add(this.label2);
        this.Controls.Add(this.pictureBox1);
        this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
        this.MaximizeBox = false;
        this.MinimizeBox = false;
        this.Name = "aslSettingsDialog";
        this.ShowIcon = false;
        this.ShowInTaskbar = false;
        this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
        this.Text = "Select and test ASL configuration file";
        this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
        ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
        this.gbReadOptions.ResumeLayout(false);
        this.gbReadOptions.PerformLayout();
        this.ResumeLayout(false);
        this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.PictureBox pictureBox1;
    private System.Windows.Forms.ToolTip ASLSettingToolTip;
    private System.Windows.Forms.Button btnBrowseLog;
    private System.Windows.Forms.TextBox txtLogFile;
    private System.Windows.Forms.CheckBox bWriteLogFile;
    private System.Windows.Forms.ListView lvLog;
    private System.Windows.Forms.Button btnConnect;
    private System.Windows.Forms.Button btnDisconnect;
    private System.Windows.Forms.Timer timerUpdate;
    private System.Windows.Forms.Button btnRestoreDefaults;
    private System.Windows.Forms.Button btnStartStopContinuous;
    private System.Windows.Forms.Button btnGetRecord;
    private System.Windows.Forms.Button btnBrowseConfig;
    private System.Windows.Forms.TextBox txtConfigFile;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.GroupBox gbReadOptions;
    private System.Windows.Forms.TextBox txtBaudRate;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.CheckBox bStreaming;
    private System.Windows.Forms.CheckBox bEyeHead;
    private System.Windows.Forms.ComboBox cbPort;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.CheckBox cbWarning;
    private System.Windows.Forms.Label lblErrorStreamingMode;
  }
#endif

}

