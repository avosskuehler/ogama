namespace OgamaControls
{
  partial class TriggerControl
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

    #region Component Designer generated code

    /// <summary> 
    /// Required method for Designer support - do not modify 
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
			this.nudTriggerValue = new System.Windows.Forms.NumericUpDown();
			this.label24 = new System.Windows.Forms.Label();
			this.cbbTriggerDevice = new System.Windows.Forms.ComboBox();
			this.label23 = new System.Windows.Forms.Label();
			this.label22 = new System.Windows.Forms.Label();
			this.chbSendTrigger = new System.Windows.Forms.CheckBox();
			this.label1 = new System.Windows.Forms.Label();
			this.nudTriggerSignalTime = new System.Windows.Forms.NumericUpDown();
			this.txbPortAddress = new System.Windows.Forms.TextBox();
			((System.ComponentModel.ISupportInitialize)(this.nudTriggerValue)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.nudTriggerSignalTime)).BeginInit();
			this.SuspendLayout();
			// 
			// nudTriggerValue
			// 
			this.nudTriggerValue.Location = new System.Drawing.Point(114, 32);
			this.nudTriggerValue.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
			this.nudTriggerValue.Name = "nudTriggerValue";
			this.nudTriggerValue.Size = new System.Drawing.Size(74, 20);
			this.nudTriggerValue.TabIndex = 40;
			this.nudTriggerValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.nudTriggerValue.Value = new decimal(new int[] {
            255,
            0,
            0,
            0});
			// 
			// label24
			// 
			this.label24.AutoSize = true;
			this.label24.Location = new System.Drawing.Point(3, 34);
			this.label24.Name = "label24";
			this.label24.Size = new System.Drawing.Size(98, 13);
			this.label24.TabIndex = 37;
			this.label24.Text = "Trigger value (8 bit)";
			// 
			// cbbTriggerDevice
			// 
			this.cbbTriggerDevice.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbbTriggerDevice.FormattingEnabled = true;
			this.cbbTriggerDevice.Location = new System.Drawing.Point(114, 58);
			this.cbbTriggerDevice.Name = "cbbTriggerDevice";
			this.cbbTriggerDevice.Size = new System.Drawing.Size(74, 21);
			this.cbbTriggerDevice.TabIndex = 38;
			// 
			// label23
			// 
			this.label23.AutoSize = true;
			this.label23.Location = new System.Drawing.Point(3, 88);
			this.label23.Name = "label23";
			this.label23.Size = new System.Drawing.Size(93, 13);
			this.label23.TabIndex = 36;
			this.label23.Text = "Port Address (hex)";
			// 
			// label22
			// 
			this.label22.AutoSize = true;
			this.label22.Location = new System.Drawing.Point(3, 61);
			this.label22.Name = "label22";
			this.label22.Size = new System.Drawing.Size(108, 13);
			this.label22.TabIndex = 35;
			this.label22.Text = "Trigger output device";
			// 
			// chbSendTrigger
			// 
			this.chbSendTrigger.AutoSize = true;
			this.chbSendTrigger.Location = new System.Drawing.Point(3, 3);
			this.chbSendTrigger.Name = "chbSendTrigger";
			this.chbSendTrigger.Size = new System.Drawing.Size(83, 17);
			this.chbSendTrigger.TabIndex = 42;
			this.chbSendTrigger.Text = "Send trigger";
			this.chbSendTrigger.UseVisualStyleBackColor = true;
			this.chbSendTrigger.CheckedChanged += new System.EventHandler(this.chbSendTrigger_CheckedChanged);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(3, 115);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(94, 13);
			this.label1.TabIndex = 36;
			this.label1.Text = "Signaling time (ms)";
			// 
			// nudTriggerSignalTime
			// 
			this.nudTriggerSignalTime.Location = new System.Drawing.Point(114, 111);
			this.nudTriggerSignalTime.Maximum = new decimal(new int[] {
            5000,
            0,
            0,
            0});
			this.nudTriggerSignalTime.Name = "nudTriggerSignalTime";
			this.nudTriggerSignalTime.Size = new System.Drawing.Size(74, 20);
			this.nudTriggerSignalTime.TabIndex = 41;
			this.nudTriggerSignalTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// txbPortAddress
			// 
			this.txbPortAddress.Enabled = false;
			this.txbPortAddress.Location = new System.Drawing.Point(114, 85);
			this.txbPortAddress.Name = "txbPortAddress";
			this.txbPortAddress.Size = new System.Drawing.Size(74, 20);
			this.txbPortAddress.TabIndex = 43;
			// 
			// TriggerControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.txbPortAddress);
			this.Controls.Add(this.chbSendTrigger);
			this.Controls.Add(this.nudTriggerValue);
			this.Controls.Add(this.nudTriggerSignalTime);
			this.Controls.Add(this.label24);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.cbbTriggerDevice);
			this.Controls.Add(this.label23);
			this.Controls.Add(this.label22);
			this.Name = "TriggerControl";
			this.Size = new System.Drawing.Size(194, 147);
			((System.ComponentModel.ISupportInitialize)(this.nudTriggerValue)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.nudTriggerSignalTime)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

    }

    #endregion

		private System.Windows.Forms.NumericUpDown nudTriggerValue;
    private System.Windows.Forms.Label label24;
    private System.Windows.Forms.ComboBox cbbTriggerDevice;
    private System.Windows.Forms.Label label23;
    private System.Windows.Forms.Label label22;
    private System.Windows.Forms.CheckBox chbSendTrigger;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.NumericUpDown nudTriggerSignalTime;
		private System.Windows.Forms.TextBox txbPortAddress;
  }
}
