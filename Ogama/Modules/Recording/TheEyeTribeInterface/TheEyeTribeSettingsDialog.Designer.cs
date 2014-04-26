
namespace Ogama.Modules.Recording.TheEyeTribeInterface
{
  partial class TheEyeTribeSettingsDialog
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
      this.rdb16PtsCalib = new System.Windows.Forms.RadioButton();
      this.rdb12PtsCalib = new System.Windows.Forms.RadioButton();
      this.rdb9PtsCalib = new System.Windows.Forms.RadioButton();
      this.groupBox1 = new System.Windows.Forms.GroupBox();
      this.pictureBox1 = new System.Windows.Forms.PictureBox();
      this.label1 = new System.Windows.Forms.Label();
      this.btnOK = new System.Windows.Forms.Button();
      this.btnCancel = new System.Windows.Forms.Button();
      this.groupBox4 = new System.Windows.Forms.GroupBox();
      this.label3 = new System.Windows.Forms.Label();
      this.label2 = new System.Windows.Forms.Label();
      this.clbBackColor = new OgamaControls.ColorButton(this.components);
      this.clbPointColor = new OgamaControls.ColorButton(this.components);
      this.groupBox3 = new System.Windows.Forms.GroupBox();
      this.label4 = new System.Windows.Forms.Label();
      this.nudDisplayTime = new System.Windows.Forms.NumericUpDown();
      this.chbDisplayHelp = new System.Windows.Forms.CheckBox();
      this.label5 = new System.Windows.Forms.Label();
      this.label6 = new System.Windows.Forms.Label();
      this.txbPort = new System.Windows.Forms.TextBox();
      this.txbServerAddress = new System.Windows.Forms.TextBox();
      this.tabControl1 = new System.Windows.Forms.TabControl();
      this.tabPage1 = new System.Windows.Forms.TabPage();
      this.cbbFramerate = new System.Windows.Forms.ComboBox();
      this.label7 = new System.Windows.Forms.Label();
      this.tabPage2 = new System.Windows.Forms.TabPage();
      this.label8 = new System.Windows.Forms.Label();
      this.cbbDeviceIndex = new System.Windows.Forms.ComboBox();
      this.groupBox1.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
      this.groupBox4.SuspendLayout();
      this.groupBox3.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.nudDisplayTime)).BeginInit();
      this.tabControl1.SuspendLayout();
      this.tabPage1.SuspendLayout();
      this.tabPage2.SuspendLayout();
      this.SuspendLayout();
      // 
      // rdb16PtsCalib
      // 
      this.rdb16PtsCalib.AutoSize = true;
      this.rdb16PtsCalib.Location = new System.Drawing.Point(6, 53);
      this.rdb16PtsCalib.Name = "rdb16PtsCalib";
      this.rdb16PtsCalib.Size = new System.Drawing.Size(68, 17);
      this.rdb16PtsCalib.TabIndex = 19;
      this.rdb16PtsCalib.Text = "&16 points";
      // 
      // rdb12PtsCalib
      // 
      this.rdb12PtsCalib.AutoSize = true;
      this.rdb12PtsCalib.Cursor = System.Windows.Forms.Cursors.Default;
      this.rdb12PtsCalib.Location = new System.Drawing.Point(6, 36);
      this.rdb12PtsCalib.Name = "rdb12PtsCalib";
      this.rdb12PtsCalib.Size = new System.Drawing.Size(68, 17);
      this.rdb12PtsCalib.TabIndex = 18;
      this.rdb12PtsCalib.Text = "&12 points";
      // 
      // rdb9PtsCalib
      // 
      this.rdb9PtsCalib.AutoSize = true;
      this.rdb9PtsCalib.Checked = true;
      this.rdb9PtsCalib.Location = new System.Drawing.Point(6, 19);
      this.rdb9PtsCalib.Name = "rdb9PtsCalib";
      this.rdb9PtsCalib.Size = new System.Drawing.Size(62, 17);
      this.rdb9PtsCalib.TabIndex = 17;
      this.rdb9PtsCalib.TabStop = true;
      this.rdb9PtsCalib.Text = "&9 points";
      // 
      // groupBox1
      // 
      this.groupBox1.Controls.Add(this.rdb16PtsCalib);
      this.groupBox1.Controls.Add(this.rdb9PtsCalib);
      this.groupBox1.Controls.Add(this.rdb12PtsCalib);
      this.groupBox1.Location = new System.Drawing.Point(6, 6);
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.Size = new System.Drawing.Size(100, 72);
      this.groupBox1.TabIndex = 20;
      this.groupBox1.TabStop = false;
      this.groupBox1.Text = "Number";
      // 
      // pictureBox1
      // 
      this.pictureBox1.Image = global::Ogama.Properties.Resources.TheEyeTribeFoto64;
      this.pictureBox1.Location = new System.Drawing.Point(12, 12);
      this.pictureBox1.Name = "pictureBox1";
      this.pictureBox1.Size = new System.Drawing.Size(64, 64);
      this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
      this.pictureBox1.TabIndex = 23;
      this.pictureBox1.TabStop = false;
      // 
      // label1
      // 
      this.label1.Location = new System.Drawing.Point(90, 17);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(168, 58);
      this.label1.TabIndex = 24;
      this.label1.Text = "Please specify the settings for the TheEyeTribe tracker.";
      // 
      // btnOK
      // 
      this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
      this.btnOK.Location = new System.Drawing.Point(105, 364);
      this.btnOK.Name = "btnOK";
      this.btnOK.Size = new System.Drawing.Size(75, 23);
      this.btnOK.TabIndex = 26;
      this.btnOK.Text = "&OK";
      this.btnOK.UseVisualStyleBackColor = true;
      // 
      // btnCancel
      // 
      this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.btnCancel.Location = new System.Drawing.Point(186, 364);
      this.btnCancel.Name = "btnCancel";
      this.btnCancel.Size = new System.Drawing.Size(75, 23);
      this.btnCancel.TabIndex = 26;
      this.btnCancel.Text = "&Cancel";
      this.btnCancel.UseVisualStyleBackColor = true;
      // 
      // groupBox4
      // 
      this.groupBox4.Controls.Add(this.label3);
      this.groupBox4.Controls.Add(this.label2);
      this.groupBox4.Controls.Add(this.clbBackColor);
      this.groupBox4.Controls.Add(this.clbPointColor);
      this.groupBox4.Location = new System.Drawing.Point(6, 85);
      this.groupBox4.Name = "groupBox4";
      this.groupBox4.Size = new System.Drawing.Size(214, 83);
      this.groupBox4.TabIndex = 32;
      this.groupBox4.TabStop = false;
      this.groupBox4.Text = "Colors";
      // 
      // label3
      // 
      this.label3.Location = new System.Drawing.Point(120, 16);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(78, 30);
      this.label3.TabIndex = 1;
      this.label3.Text = "Background color";
      // 
      // label2
      // 
      this.label2.Location = new System.Drawing.Point(3, 16);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(78, 30);
      this.label2.TabIndex = 1;
      this.label2.Text = "Calibration point color";
      // 
      // clbBackColor
      // 
      this.clbBackColor.AutoButtonString = "Automatic";
      this.clbBackColor.CurrentColor = System.Drawing.SystemColors.ControlLight;
      this.clbBackColor.Location = new System.Drawing.Point(123, 49);
      this.clbBackColor.Name = "clbBackColor";
      this.clbBackColor.Size = new System.Drawing.Size(75, 23);
      this.clbBackColor.TabIndex = 0;
      this.clbBackColor.UseVisualStyleBackColor = true;
      // 
      // clbPointColor
      // 
      this.clbPointColor.AutoButtonString = "Automatic";
      this.clbPointColor.CurrentColor = System.Drawing.Color.Red;
      this.clbPointColor.Location = new System.Drawing.Point(6, 49);
      this.clbPointColor.Name = "clbPointColor";
      this.clbPointColor.Size = new System.Drawing.Size(75, 23);
      this.clbPointColor.TabIndex = 0;
      this.clbPointColor.UseVisualStyleBackColor = true;
      // 
      // groupBox3
      // 
      this.groupBox3.Controls.Add(this.label4);
      this.groupBox3.Controls.Add(this.nudDisplayTime);
      this.groupBox3.Location = new System.Drawing.Point(112, 6);
      this.groupBox3.Name = "groupBox3";
      this.groupBox3.Size = new System.Drawing.Size(108, 73);
      this.groupBox3.TabIndex = 31;
      this.groupBox3.TabStop = false;
      this.groupBox3.Text = "Speed";
      // 
      // label4
      // 
      this.label4.Location = new System.Drawing.Point(14, 20);
      this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(78, 26);
      this.label4.TabIndex = 1;
      this.label4.Text = "Point display time in ms.";
      // 
      // nudDisplayTime
      // 
      this.nudDisplayTime.Increment = new decimal(new int[] {
            50,
            0,
            0,
            0});
      this.nudDisplayTime.Location = new System.Drawing.Point(17, 48);
      this.nudDisplayTime.Margin = new System.Windows.Forms.Padding(2);
      this.nudDisplayTime.Maximum = new decimal(new int[] {
            5000,
            0,
            0,
            0});
      this.nudDisplayTime.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            0});
      this.nudDisplayTime.Name = "nudDisplayTime";
      this.nudDisplayTime.Size = new System.Drawing.Size(75, 20);
      this.nudDisplayTime.TabIndex = 0;
      this.nudDisplayTime.Value = new decimal(new int[] {
            750,
            0,
            0,
            0});
      // 
      // chbDisplayHelp
      // 
      this.chbDisplayHelp.AutoSize = true;
      this.chbDisplayHelp.Location = new System.Drawing.Point(12, 174);
      this.chbDisplayHelp.Name = "chbDisplayHelp";
      this.chbDisplayHelp.Size = new System.Drawing.Size(86, 17);
      this.chbDisplayHelp.TabIndex = 31;
      this.chbDisplayHelp.Text = "Display help.";
      this.chbDisplayHelp.UseVisualStyleBackColor = true;
      // 
      // label5
      // 
      this.label5.AutoSize = true;
      this.label5.Location = new System.Drawing.Point(8, 9);
      this.label5.Name = "label5";
      this.label5.Size = new System.Drawing.Size(81, 13);
      this.label5.TabIndex = 36;
      this.label5.Text = "Server address:";
      // 
      // label6
      // 
      this.label6.AutoSize = true;
      this.label6.Location = new System.Drawing.Point(8, 36);
      this.label6.Name = "label6";
      this.label6.Size = new System.Drawing.Size(29, 13);
      this.label6.TabIndex = 35;
      this.label6.Text = "Port:";
      // 
      // txbPort
      // 
      this.txbPort.Location = new System.Drawing.Point(98, 33);
      this.txbPort.Name = "txbPort";
      this.txbPort.Size = new System.Drawing.Size(52, 20);
      this.txbPort.TabIndex = 33;
      this.txbPort.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxbPortKeyDown);
      // 
      // txbServerAddress
      // 
      this.txbServerAddress.Location = new System.Drawing.Point(98, 6);
      this.txbServerAddress.Name = "txbServerAddress";
      this.txbServerAddress.Size = new System.Drawing.Size(133, 20);
      this.txbServerAddress.TabIndex = 34;
      // 
      // tabControl1
      // 
      this.tabControl1.Controls.Add(this.tabPage1);
      this.tabControl1.Controls.Add(this.tabPage2);
      this.tabControl1.Location = new System.Drawing.Point(12, 91);
      this.tabControl1.Name = "tabControl1";
      this.tabControl1.SelectedIndex = 0;
      this.tabControl1.Size = new System.Drawing.Size(250, 267);
      this.tabControl1.TabIndex = 37;
      // 
      // tabPage1
      // 
      this.tabPage1.Controls.Add(this.cbbDeviceIndex);
      this.tabPage1.Controls.Add(this.cbbFramerate);
      this.tabPage1.Controls.Add(this.txbServerAddress);
      this.tabPage1.Controls.Add(this.label5);
      this.tabPage1.Controls.Add(this.label8);
      this.tabPage1.Controls.Add(this.txbPort);
      this.tabPage1.Controls.Add(this.label7);
      this.tabPage1.Controls.Add(this.label6);
      this.tabPage1.Location = new System.Drawing.Point(4, 22);
      this.tabPage1.Name = "tabPage1";
      this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
      this.tabPage1.Size = new System.Drawing.Size(242, 241);
      this.tabPage1.TabIndex = 0;
      this.tabPage1.Text = "General";
      this.tabPage1.UseVisualStyleBackColor = true;
      // 
      // cbbFramerate
      // 
      this.cbbFramerate.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
      this.cbbFramerate.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
      this.cbbFramerate.FormattingEnabled = true;
      this.cbbFramerate.Items.AddRange(new object[] {
            "30 Hz",
            "60 Hz"});
      this.cbbFramerate.Location = new System.Drawing.Point(98, 60);
      this.cbbFramerate.Name = "cbbFramerate";
      this.cbbFramerate.Size = new System.Drawing.Size(52, 21);
      this.cbbFramerate.Sorted = true;
      this.cbbFramerate.TabIndex = 37;
      this.cbbFramerate.Text = "60 Hz";
      // 
      // label7
      // 
      this.label7.AutoSize = true;
      this.label7.Location = new System.Drawing.Point(8, 63);
      this.label7.Name = "label7";
      this.label7.Size = new System.Drawing.Size(57, 13);
      this.label7.TabIndex = 35;
      this.label7.Text = "Framerate:";
      // 
      // tabPage2
      // 
      this.tabPage2.Controls.Add(this.groupBox1);
      this.tabPage2.Controls.Add(this.chbDisplayHelp);
      this.tabPage2.Controls.Add(this.groupBox3);
      this.tabPage2.Controls.Add(this.groupBox4);
      this.tabPage2.Location = new System.Drawing.Point(4, 22);
      this.tabPage2.Name = "tabPage2";
      this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
      this.tabPage2.Size = new System.Drawing.Size(242, 241);
      this.tabPage2.TabIndex = 1;
      this.tabPage2.Text = "Calibration";
      this.tabPage2.UseVisualStyleBackColor = true;
      // 
      // label8
      // 
      this.label8.AutoSize = true;
      this.label8.Location = new System.Drawing.Point(8, 90);
      this.label8.Name = "label8";
      this.label8.Size = new System.Drawing.Size(69, 13);
      this.label8.TabIndex = 35;
      this.label8.Text = "Device index";
      // 
      // cbbDeviceIndex
      // 
      this.cbbDeviceIndex.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
      this.cbbDeviceIndex.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
      this.cbbDeviceIndex.FormattingEnabled = true;
      this.cbbDeviceIndex.Items.AddRange(new object[] {
            "0",
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7"});
      this.cbbDeviceIndex.Location = new System.Drawing.Point(98, 88);
      this.cbbDeviceIndex.Name = "cbbDeviceIndex";
      this.cbbDeviceIndex.Size = new System.Drawing.Size(52, 21);
      this.cbbDeviceIndex.Sorted = true;
      this.cbbDeviceIndex.TabIndex = 37;
      this.cbbDeviceIndex.Text = "0";
      // 
      // TheEyeTribeSettingsDialog
      // 
      this.AcceptButton = this.btnOK;
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.CancelButton = this.btnCancel;
      this.ClientSize = new System.Drawing.Size(273, 399);
      this.Controls.Add(this.tabControl1);
      this.Controls.Add(this.btnCancel);
      this.Controls.Add(this.btnOK);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.pictureBox1);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "TheEyeTribeSettingsDialog";
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "Specify TheEyeTribe settings ...";
      this.groupBox1.ResumeLayout(false);
      this.groupBox1.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
      this.groupBox4.ResumeLayout(false);
      this.groupBox3.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.nudDisplayTime)).EndInit();
      this.tabControl1.ResumeLayout(false);
      this.tabPage1.ResumeLayout(false);
      this.tabPage1.PerformLayout();
      this.tabPage2.ResumeLayout(false);
      this.tabPage2.PerformLayout();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.RadioButton rdb16PtsCalib;
    private System.Windows.Forms.RadioButton rdb12PtsCalib;
    private System.Windows.Forms.RadioButton rdb9PtsCalib;
    private System.Windows.Forms.GroupBox groupBox1;
    private System.Windows.Forms.PictureBox pictureBox1;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Button btnOK;
    private System.Windows.Forms.Button btnCancel;
    private System.Windows.Forms.GroupBox groupBox4;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.Label label2;
    private OgamaControls.ColorButton clbBackColor;
    private OgamaControls.ColorButton clbPointColor;
    private System.Windows.Forms.GroupBox groupBox3;
    private System.Windows.Forms.CheckBox chbDisplayHelp;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.NumericUpDown nudDisplayTime;
    private System.Windows.Forms.Label label5;
    private System.Windows.Forms.Label label6;
    private System.Windows.Forms.TextBox txbPort;
    private System.Windows.Forms.TextBox txbServerAddress;
    private System.Windows.Forms.TabControl tabControl1;
    private System.Windows.Forms.TabPage tabPage1;
    private System.Windows.Forms.ComboBox cbbFramerate;
    private System.Windows.Forms.Label label7;
    private System.Windows.Forms.TabPage tabPage2;
    private System.Windows.Forms.ComboBox cbbDeviceIndex;
    private System.Windows.Forms.Label label8;
  }
}

