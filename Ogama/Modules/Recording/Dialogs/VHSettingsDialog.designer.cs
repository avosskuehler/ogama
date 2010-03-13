namespace Ogama.Modules.Recording
{
  partial class VHSettingsDialog
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VHSettingsDialog));
      this.label1 = new System.Windows.Forms.Label();
      this.label2 = new System.Windows.Forms.Label();
      this.txbLeft = new System.Windows.Forms.TextBox();
      this.txbTop = new System.Windows.Forms.TextBox();
      this.txbHeight = new System.Windows.Forms.TextBox();
      this.txbWidth = new System.Windows.Forms.TextBox();
      this.label3 = new System.Windows.Forms.Label();
      this.label4 = new System.Windows.Forms.Label();
      this.chbAlign = new System.Windows.Forms.CheckBox();
      this.chbShowmouse = new System.Windows.Forms.CheckBox();
      this.chbShowclicks = new System.Windows.Forms.CheckBox();
      this.chbCapLayeredWindows = new System.Windows.Forms.CheckBox();
      this.chbOptCapture = new System.Windows.Forms.CheckBox();
      this.chbOptDeliver = new System.Windows.Forms.CheckBox();
      this.chbRegROT = new System.Windows.Forms.CheckBox();
      this.groupBox1 = new System.Windows.Forms.GroupBox();
      this.btnOK = new System.Windows.Forms.Button();
      this.btnCancel = new System.Windows.Forms.Button();
      this.splitContainer1 = new System.Windows.Forms.SplitContainer();
      this.dialogTop1 = new Ogama.Modules.Common.DialogTop();
      this.groupBox3 = new System.Windows.Forms.GroupBox();
      this.groupBox2 = new System.Windows.Forms.GroupBox();
      this.label5 = new System.Windows.Forms.Label();
      this.nudFPS = new System.Windows.Forms.NumericUpDown();
      this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
      this.groupBox1.SuspendLayout();
      this.splitContainer1.Panel1.SuspendLayout();
      this.splitContainer1.Panel2.SuspendLayout();
      this.splitContainer1.SuspendLayout();
      this.groupBox3.SuspendLayout();
      this.groupBox2.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.nudFPS)).BeginInit();
      this.SuspendLayout();
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(9, 22);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(28, 13);
      this.label1.TabIndex = 0;
      this.label1.Text = "Left:";
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(8, 48);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(29, 13);
      this.label2.TabIndex = 0;
      this.label2.Text = "Top:";
      // 
      // txbLeft
      // 
      this.txbLeft.Location = new System.Drawing.Point(43, 19);
      this.txbLeft.Name = "txbLeft";
      this.txbLeft.Size = new System.Drawing.Size(70, 20);
      this.txbLeft.TabIndex = 1;
      // 
      // txbTop
      // 
      this.txbTop.Location = new System.Drawing.Point(43, 45);
      this.txbTop.Name = "txbTop";
      this.txbTop.Size = new System.Drawing.Size(70, 20);
      this.txbTop.TabIndex = 2;
      // 
      // txbHeight
      // 
      this.txbHeight.Location = new System.Drawing.Point(160, 45);
      this.txbHeight.Name = "txbHeight";
      this.txbHeight.Size = new System.Drawing.Size(70, 20);
      this.txbHeight.TabIndex = 4;
      // 
      // txbWidth
      // 
      this.txbWidth.Location = new System.Drawing.Point(160, 19);
      this.txbWidth.Name = "txbWidth";
      this.txbWidth.Size = new System.Drawing.Size(70, 20);
      this.txbWidth.TabIndex = 3;
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Location = new System.Drawing.Point(116, 48);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(41, 13);
      this.label3.TabIndex = 2;
      this.label3.Text = "Height:";
      // 
      // label4
      // 
      this.label4.AutoSize = true;
      this.label4.Location = new System.Drawing.Point(116, 22);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(38, 13);
      this.label4.TabIndex = 3;
      this.label4.Text = "Width:";
      // 
      // chbAlign
      // 
      this.chbAlign.AutoSize = true;
      this.chbAlign.Location = new System.Drawing.Point(6, 19);
      this.chbAlign.Name = "chbAlign";
      this.chbAlign.Size = new System.Drawing.Size(78, 17);
      this.chbAlign.TabIndex = 10;
      this.chbAlign.Text = "Align video";
      this.toolTip1.SetToolTip(this.chbAlign, "Align output width and height to be multiple of 4.");
      this.chbAlign.UseVisualStyleBackColor = true;
      // 
      // chbShowmouse
      // 
      this.chbShowmouse.AutoSize = true;
      this.chbShowmouse.Location = new System.Drawing.Point(6, 42);
      this.chbShowmouse.Name = "chbShowmouse";
      this.chbShowmouse.Size = new System.Drawing.Size(87, 17);
      this.chbShowmouse.TabIndex = 11;
      this.chbShowmouse.Text = "Show mouse";
      this.toolTip1.SetToolTip(this.chbShowmouse, "Capture mouse cursor.");
      this.chbShowmouse.UseVisualStyleBackColor = true;
      // 
      // chbShowclicks
      // 
      this.chbShowclicks.AutoSize = true;
      this.chbShowclicks.Location = new System.Drawing.Point(24, 61);
      this.chbShowclicks.Name = "chbShowclicks";
      this.chbShowclicks.Size = new System.Drawing.Size(83, 17);
      this.chbShowclicks.TabIndex = 12;
      this.chbShowclicks.Text = "Show clicks";
      this.toolTip1.SetToolTip(this.chbShowclicks, "Capture clicks of mouse cursor.");
      this.chbShowclicks.UseVisualStyleBackColor = true;
      // 
      // chbCapLayeredWindows
      // 
      this.chbCapLayeredWindows.AutoSize = true;
      this.chbCapLayeredWindows.Location = new System.Drawing.Point(6, 84);
      this.chbCapLayeredWindows.Name = "chbCapLayeredWindows";
      this.chbCapLayeredWindows.Size = new System.Drawing.Size(144, 17);
      this.chbCapLayeredWindows.TabIndex = 13;
      this.chbCapLayeredWindows.Text = "Capture layered windows";
      this.toolTip1.SetToolTip(this.chbCapLayeredWindows, "Capture layered windows.");
      this.chbCapLayeredWindows.UseVisualStyleBackColor = true;
      // 
      // chbOptCapture
      // 
      this.chbOptCapture.AutoSize = true;
      this.chbOptCapture.Location = new System.Drawing.Point(6, 107);
      this.chbOptCapture.Name = "chbOptCapture";
      this.chbOptCapture.Size = new System.Drawing.Size(140, 17);
      this.chbOptCapture.TabIndex = 14;
      this.chbOptCapture.Text = "Optimize screen capture";
      this.toolTip1.SetToolTip(this.chbOptCapture, "Optimize capture from screen so that it is \r\nonly captured when GUI changes are d" +
              "etected.\r\n(Saves CPU load)");
      this.chbOptCapture.UseVisualStyleBackColor = true;
      // 
      // chbOptDeliver
      // 
      this.chbOptDeliver.AutoSize = true;
      this.chbOptDeliver.Location = new System.Drawing.Point(24, 127);
      this.chbOptDeliver.Name = "chbOptDeliver";
      this.chbOptDeliver.Size = new System.Drawing.Size(135, 17);
      this.chbOptDeliver.TabIndex = 15;
      this.chbOptDeliver.Text = "Optimize screen deliver";
      this.toolTip1.SetToolTip(this.chbOptDeliver, "Optimize frames sending (it will send next frame if\r\nchanges in GUI are detected)" +
              ". \r\nIf disabled, it will provide the selected FPS.");
      this.chbOptDeliver.UseVisualStyleBackColor = true;
      // 
      // chbRegROT
      // 
      this.chbRegROT.AutoSize = true;
      this.chbRegROT.Location = new System.Drawing.Point(6, 150);
      this.chbRegROT.Name = "chbRegROT";
      this.chbRegROT.Size = new System.Drawing.Size(102, 17);
      this.chbRegROT.TabIndex = 23;
      this.chbRegROT.Text = "Register in ROT";
      this.toolTip1.SetToolTip(this.chbRegROT, "Registers graph in ROT \r\n(Running Object Table, see MSDN).");
      this.chbRegROT.UseVisualStyleBackColor = true;
      // 
      // groupBox1
      // 
      this.groupBox1.Controls.Add(this.txbWidth);
      this.groupBox1.Controls.Add(this.label1);
      this.groupBox1.Controls.Add(this.label2);
      this.groupBox1.Controls.Add(this.txbLeft);
      this.groupBox1.Controls.Add(this.txbTop);
      this.groupBox1.Controls.Add(this.label4);
      this.groupBox1.Controls.Add(this.label3);
      this.groupBox1.Controls.Add(this.txbHeight);
      this.groupBox1.Location = new System.Drawing.Point(12, 5);
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.Size = new System.Drawing.Size(240, 73);
      this.groupBox1.TabIndex = 24;
      this.groupBox1.TabStop = false;
      this.groupBox1.Text = "Area to be tracked";
      this.toolTip1.SetToolTip(this.groupBox1, resources.GetString("groupBox1.ToolTip"));
      // 
      // btnOK
      // 
      this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
      this.btnOK.Location = new System.Drawing.Point(178, 268);
      this.btnOK.Name = "btnOK";
      this.btnOK.Size = new System.Drawing.Size(80, 23);
      this.btnOK.TabIndex = 25;
      this.btnOK.Text = "&OK";
      this.btnOK.UseVisualStyleBackColor = true;
      // 
      // btnCancel
      // 
      this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.btnCancel.Location = new System.Drawing.Point(264, 268);
      this.btnCancel.Name = "btnCancel";
      this.btnCancel.Size = new System.Drawing.Size(80, 23);
      this.btnCancel.TabIndex = 25;
      this.btnCancel.Text = "&Cancel";
      this.btnCancel.UseVisualStyleBackColor = true;
      // 
      // splitContainer1
      // 
      this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
      this.splitContainer1.IsSplitterFixed = true;
      this.splitContainer1.Location = new System.Drawing.Point(0, 0);
      this.splitContainer1.Name = "splitContainer1";
      this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
      // 
      // splitContainer1.Panel1
      // 
      this.splitContainer1.Panel1.Controls.Add(this.dialogTop1);
      // 
      // splitContainer1.Panel2
      // 
      this.splitContainer1.Panel2.Controls.Add(this.groupBox3);
      this.splitContainer1.Panel2.Controls.Add(this.groupBox2);
      this.splitContainer1.Panel2.Controls.Add(this.btnOK);
      this.splitContainer1.Panel2.Controls.Add(this.btnCancel);
      this.splitContainer1.Panel2.Controls.Add(this.groupBox1);
      this.splitContainer1.Size = new System.Drawing.Size(356, 364);
      this.splitContainer1.SplitterDistance = 60;
      this.splitContainer1.SplitterWidth = 1;
      this.splitContainer1.TabIndex = 26;
      // 
      // dialogTop1
      // 
      this.dialogTop1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(237)))), ((int)(((byte)(237)))));
      this.dialogTop1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("dialogTop1.BackgroundImage")));
      this.dialogTop1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
      this.dialogTop1.Description = "Please specify custom settings for the screen capturing";
      this.dialogTop1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.dialogTop1.Location = new System.Drawing.Point(0, 0);
      this.dialogTop1.Logo = ((System.Drawing.Image)(resources.GetObject("dialogTop1.Logo")));
      this.dialogTop1.Name = "dialogTop1";
      this.dialogTop1.Size = new System.Drawing.Size(356, 60);
      this.dialogTop1.TabIndex = 0;
      // 
      // groupBox3
      // 
      this.groupBox3.Controls.Add(this.chbAlign);
      this.groupBox3.Controls.Add(this.chbShowclicks);
      this.groupBox3.Controls.Add(this.chbRegROT);
      this.groupBox3.Controls.Add(this.chbShowmouse);
      this.groupBox3.Controls.Add(this.chbOptDeliver);
      this.groupBox3.Controls.Add(this.chbCapLayeredWindows);
      this.groupBox3.Controls.Add(this.chbOptCapture);
      this.groupBox3.Location = new System.Drawing.Point(10, 84);
      this.groupBox3.Name = "groupBox3";
      this.groupBox3.Size = new System.Drawing.Size(242, 178);
      this.groupBox3.TabIndex = 28;
      this.groupBox3.TabStop = false;
      this.groupBox3.Text = "Other options";
      // 
      // groupBox2
      // 
      this.groupBox2.Controls.Add(this.label5);
      this.groupBox2.Controls.Add(this.nudFPS);
      this.groupBox2.Location = new System.Drawing.Point(258, 5);
      this.groupBox2.Name = "groupBox2";
      this.groupBox2.Size = new System.Drawing.Size(86, 73);
      this.groupBox2.TabIndex = 27;
      this.groupBox2.TabStop = false;
      this.groupBox2.Text = "Framerate";
      // 
      // label5
      // 
      this.label5.AutoSize = true;
      this.label5.Location = new System.Drawing.Point(59, 22);
      this.label5.Name = "label5";
      this.label5.Size = new System.Drawing.Size(21, 13);
      this.label5.TabIndex = 27;
      this.label5.Text = "fps";
      // 
      // nudFPS
      // 
      this.nudFPS.Location = new System.Drawing.Point(6, 19);
      this.nudFPS.Maximum = new decimal(new int[] {
            50,
            0,
            0,
            0});
      this.nudFPS.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.nudFPS.Name = "nudFPS";
      this.nudFPS.Size = new System.Drawing.Size(47, 20);
      this.nudFPS.TabIndex = 26;
      this.nudFPS.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      this.toolTip1.SetToolTip(this.nudFPS, "The frames per second that should be captured.\r\nPlease note: More than 10 fps are" +
              " very CPU demanding\r\nand may disturb the recording (control the CPU monitor).");
      this.nudFPS.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
      // 
      // VHSettingsDialog
      // 
      this.AcceptButton = this.btnOK;
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.CancelButton = this.btnCancel;
      this.ClientSize = new System.Drawing.Size(356, 364);
      this.Controls.Add(this.splitContainer1);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.MaximizeBox = false;
      this.Name = "VHSettingsDialog";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "Screen Capture settings ...";
      this.Load += new System.EventHandler(this.VHSettingsDialog_Load);
      this.groupBox1.ResumeLayout(false);
      this.groupBox1.PerformLayout();
      this.splitContainer1.Panel1.ResumeLayout(false);
      this.splitContainer1.Panel2.ResumeLayout(false);
      this.splitContainer1.ResumeLayout(false);
      this.groupBox3.ResumeLayout(false);
      this.groupBox3.PerformLayout();
      this.groupBox2.ResumeLayout(false);
      this.groupBox2.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.nudFPS)).EndInit();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.TextBox txbLeft;
    private System.Windows.Forms.TextBox txbTop;
    private System.Windows.Forms.TextBox txbHeight;
    private System.Windows.Forms.TextBox txbWidth;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.CheckBox chbAlign;
    private System.Windows.Forms.CheckBox chbShowmouse;
    private System.Windows.Forms.CheckBox chbShowclicks;
    private System.Windows.Forms.CheckBox chbCapLayeredWindows;
    private System.Windows.Forms.CheckBox chbOptCapture;
    private System.Windows.Forms.CheckBox chbOptDeliver;
    private System.Windows.Forms.CheckBox chbRegROT;
    private System.Windows.Forms.GroupBox groupBox1;
    private System.Windows.Forms.Button btnOK;
    private System.Windows.Forms.Button btnCancel;
    private System.Windows.Forms.SplitContainer splitContainer1;
    private Ogama.Modules.Common.DialogTop dialogTop1;
    private System.Windows.Forms.ToolTip toolTip1;
    private System.Windows.Forms.NumericUpDown nudFPS;
    private System.Windows.Forms.GroupBox groupBox3;
    private System.Windows.Forms.GroupBox groupBox2;
    private System.Windows.Forms.Label label5;
  }
}

