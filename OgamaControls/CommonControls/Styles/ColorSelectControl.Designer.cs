namespace OgamaControls
{
  partial class ColorSelectControl
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
      this.components = new System.ComponentModel.Container();
      this.label1 = new System.Windows.Forms.Label();
      this.label2 = new System.Windows.Forms.Label();
      this.lblSaturation = new System.Windows.Forms.Label();
      this.lblBrightness = new System.Windows.Forms.Label();
      this.wndScrollTranspareny = new System.Windows.Forms.HScrollBar();
      this.wndScrollSaturation = new System.Windows.Forms.HScrollBar();
      this.wndScrollBrightness = new System.Windows.Forms.HScrollBar();
      this.tmrUpdate = new System.Windows.Forms.Timer(this.components);
      this.nudA = new System.Windows.Forms.NumericUpDown();
      this.label3 = new System.Windows.Forms.Label();
      this.nudR = new System.Windows.Forms.NumericUpDown();
      this.nudG = new System.Windows.Forms.NumericUpDown();
      this.nudB = new System.Windows.Forms.NumericUpDown();
      this.btnGetARGB = new System.Windows.Forms.Button();
      this.btnSubmitARGB = new System.Windows.Forms.Button();
      this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
      this.panelHue = new OgamaControls.DoubleBufferPanel();
      this.panelSelectedColor = new OgamaControls.DoubleBufferPanel();
      this.btnRight = new OgamaControls.ArrowButton();
      this.btnLeft = new OgamaControls.ArrowButton();
      ((System.ComponentModel.ISupportInitialize)(this.nudA)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.nudR)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.nudG)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.nudB)).BeginInit();
      this.SuspendLayout();
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(3, 7);
      this.label1.Margin = new System.Windows.Forms.Padding(0);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(31, 13);
      this.label1.TabIndex = 18;
      this.label1.Text = "Color";
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(3, 82);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(72, 13);
      this.label2.TabIndex = 15;
      this.label2.Text = "Transparency";
      // 
      // lblSaturation
      // 
      this.lblSaturation.AutoSize = true;
      this.lblSaturation.Location = new System.Drawing.Point(3, 57);
      this.lblSaturation.Name = "lblSaturation";
      this.lblSaturation.Size = new System.Drawing.Size(55, 13);
      this.lblSaturation.TabIndex = 14;
      this.lblSaturation.Text = "Saturation";
      // 
      // lblBrightness
      // 
      this.lblBrightness.AutoSize = true;
      this.lblBrightness.Location = new System.Drawing.Point(3, 32);
      this.lblBrightness.Name = "lblBrightness";
      this.lblBrightness.Size = new System.Drawing.Size(56, 13);
      this.lblBrightness.TabIndex = 12;
      this.lblBrightness.Text = "Brightness";
      // 
      // wndScrollTranspareny
      // 
      this.wndScrollTranspareny.LargeChange = 100;
      this.wndScrollTranspareny.Location = new System.Drawing.Point(81, 80);
      this.wndScrollTranspareny.Maximum = 1099;
      this.wndScrollTranspareny.Name = "wndScrollTranspareny";
      this.wndScrollTranspareny.Size = new System.Drawing.Size(126, 17);
      this.wndScrollTranspareny.SmallChange = 5;
      this.wndScrollTranspareny.TabIndex = 17;
      this.wndScrollTranspareny.Scroll += new System.Windows.Forms.ScrollEventHandler(this.wndScrollTranspareny_Scroll);
      // 
      // wndScrollSaturation
      // 
      this.wndScrollSaturation.LargeChange = 100;
      this.wndScrollSaturation.Location = new System.Drawing.Point(81, 55);
      this.wndScrollSaturation.Maximum = 1099;
      this.wndScrollSaturation.Name = "wndScrollSaturation";
      this.wndScrollSaturation.Size = new System.Drawing.Size(126, 17);
      this.wndScrollSaturation.SmallChange = 5;
      this.wndScrollSaturation.TabIndex = 16;
      this.wndScrollSaturation.Scroll += new System.Windows.Forms.ScrollEventHandler(this.wndScrollSaturation_Scroll);
      // 
      // wndScrollBrightness
      // 
      this.wndScrollBrightness.LargeChange = 100;
      this.wndScrollBrightness.Location = new System.Drawing.Point(81, 30);
      this.wndScrollBrightness.Maximum = 1099;
      this.wndScrollBrightness.Name = "wndScrollBrightness";
      this.wndScrollBrightness.Size = new System.Drawing.Size(126, 17);
      this.wndScrollBrightness.SmallChange = 5;
      this.wndScrollBrightness.TabIndex = 13;
      this.wndScrollBrightness.Scroll += new System.Windows.Forms.ScrollEventHandler(this.wndScrollBrightness_Scroll);
      // 
      // tmrUpdate
      // 
      this.tmrUpdate.Tick += new System.EventHandler(this.Timer_Tick);
      // 
      // nudA
      // 
      this.nudA.Location = new System.Drawing.Point(49, 105);
      this.nudA.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
      this.nudA.Name = "nudA";
      this.nudA.Size = new System.Drawing.Size(38, 20);
      this.nudA.TabIndex = 21;
      this.toolTip1.SetToolTip(this.nudA, "0 = fully transparent");
      this.nudA.Value = new decimal(new int[] {
            255,
            0,
            0,
            0});
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Location = new System.Drawing.Point(3, 107);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(37, 13);
      this.label3.TabIndex = 22;
      this.label3.Text = "ARGB";
      // 
      // nudR
      // 
      this.nudR.Location = new System.Drawing.Point(89, 105);
      this.nudR.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
      this.nudR.Name = "nudR";
      this.nudR.Size = new System.Drawing.Size(38, 20);
      this.nudR.TabIndex = 21;
      this.toolTip1.SetToolTip(this.nudR, "Red");
      this.nudR.Value = new decimal(new int[] {
            255,
            0,
            0,
            0});
      // 
      // nudG
      // 
      this.nudG.Location = new System.Drawing.Point(129, 105);
      this.nudG.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
      this.nudG.Name = "nudG";
      this.nudG.Size = new System.Drawing.Size(38, 20);
      this.nudG.TabIndex = 21;
      this.toolTip1.SetToolTip(this.nudG, "Green");
      this.nudG.Value = new decimal(new int[] {
            255,
            0,
            0,
            0});
      // 
      // nudB
      // 
      this.nudB.Location = new System.Drawing.Point(169, 105);
      this.nudB.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
      this.nudB.Name = "nudB";
      this.nudB.Size = new System.Drawing.Size(38, 20);
      this.nudB.TabIndex = 21;
      this.toolTip1.SetToolTip(this.nudB, "Blue");
      this.nudB.Value = new decimal(new int[] {
            255,
            0,
            0,
            0});
      // 
      // btnGetARGB
      // 
      this.btnGetARGB.Image = global::OgamaControls.Properties.Resources.DownArrowHS;
      this.btnGetARGB.Location = new System.Drawing.Point(241, 103);
      this.btnGetARGB.Name = "btnGetARGB";
      this.btnGetARGB.Size = new System.Drawing.Size(22, 23);
      this.btnGetARGB.TabIndex = 23;
      this.toolTip1.SetToolTip(this.btnGetARGB, "Retrieve ARGB values for color.");
      this.btnGetARGB.UseVisualStyleBackColor = true;
      this.btnGetARGB.Click += new System.EventHandler(this.btnGetARGB_Click);
      // 
      // btnSubmitARGB
      // 
      this.btnSubmitARGB.Image = global::OgamaControls.Properties.Resources.TopArrowHS;
      this.btnSubmitARGB.Location = new System.Drawing.Point(213, 103);
      this.btnSubmitARGB.Name = "btnSubmitARGB";
      this.btnSubmitARGB.Size = new System.Drawing.Size(22, 23);
      this.btnSubmitARGB.TabIndex = 23;
      this.toolTip1.SetToolTip(this.btnSubmitARGB, "Submit ARGB value to control.");
      this.btnSubmitARGB.UseVisualStyleBackColor = true;
      this.btnSubmitARGB.Click += new System.EventHandler(this.btnSubmitARGB_Click);
      // 
      // panelHue
      // 
      this.panelHue.Location = new System.Drawing.Point(98, 3);
      this.panelHue.Name = "panelHue";
      this.panelHue.Size = new System.Drawing.Size(92, 22);
      this.panelHue.TabIndex = 20;
      this.panelHue.Paint += new System.Windows.Forms.PaintEventHandler(this.panelHue_Paint);
      this.panelHue.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panelHue_MouseMove);
      this.panelHue.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panelHue_MouseDown);
      // 
      // panelSelectedColor
      // 
      this.panelSelectedColor.Location = new System.Drawing.Point(210, 5);
      this.panelSelectedColor.Name = "panelSelectedColor";
      this.panelSelectedColor.Size = new System.Drawing.Size(92, 92);
      this.panelSelectedColor.TabIndex = 19;
      this.toolTip1.SetToolTip(this.panelSelectedColor, "Click to show default color select dialog.");
      this.panelSelectedColor.Paint += new System.Windows.Forms.PaintEventHandler(this.panelSelectedColor_Paint);
      this.panelSelectedColor.MouseUp += new System.Windows.Forms.MouseEventHandler(this.panelSelectedColor_MouseUp);
      // 
      // btnRight
      // 
      this.btnRight.Direction = OgamaControls.ArrowButtonDirection.Right;
      this.btnRight.Location = new System.Drawing.Point(190, 5);
      this.btnRight.Margin = new System.Windows.Forms.Padding(0);
      this.btnRight.Name = "btnRight";
      this.btnRight.Size = new System.Drawing.Size(17, 17);
      this.btnRight.TabIndex = 11;
      this.btnRight.Click += new System.EventHandler(this.btnRight_Click);
      this.btnRight.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnRight_MouseDown);
      this.btnRight.MouseUp += new System.Windows.Forms.MouseEventHandler(this.OnMouseButtonUp);
      // 
      // btnLeft
      // 
      this.btnLeft.Direction = OgamaControls.ArrowButtonDirection.Left;
      this.btnLeft.Location = new System.Drawing.Point(81, 5);
      this.btnLeft.Margin = new System.Windows.Forms.Padding(0);
      this.btnLeft.Name = "btnLeft";
      this.btnLeft.Size = new System.Drawing.Size(17, 17);
      this.btnLeft.TabIndex = 10;
      this.btnLeft.Click += new System.EventHandler(this.btnLeft_Click);
      this.btnLeft.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnLeft_MouseDown);
      this.btnLeft.MouseUp += new System.Windows.Forms.MouseEventHandler(this.OnMouseButtonUp);
      // 
      // ColorSelectControl
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.btnGetARGB);
      this.Controls.Add(this.btnSubmitARGB);
      this.Controls.Add(this.label3);
      this.Controls.Add(this.nudB);
      this.Controls.Add(this.nudG);
      this.Controls.Add(this.nudR);
      this.Controls.Add(this.nudA);
      this.Controls.Add(this.panelHue);
      this.Controls.Add(this.panelSelectedColor);
      this.Controls.Add(this.btnRight);
      this.Controls.Add(this.btnLeft);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.lblSaturation);
      this.Controls.Add(this.lblBrightness);
      this.Controls.Add(this.wndScrollTranspareny);
      this.Controls.Add(this.wndScrollSaturation);
      this.Controls.Add(this.wndScrollBrightness);
      this.Name = "ColorSelectControl";
      this.Size = new System.Drawing.Size(310, 135);
      this.Load += new System.EventHandler(this.ColorSelectControl_Load);
      this.Paint += new System.Windows.Forms.PaintEventHandler(this.ColorSelectControl_Paint);
      ((System.ComponentModel.ISupportInitialize)(this.nudA)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.nudR)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.nudG)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.nudB)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private DoubleBufferPanel panelHue;
    private DoubleBufferPanel panelSelectedColor;
    private ArrowButton btnRight;
    private ArrowButton btnLeft;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Label lblSaturation;
    private System.Windows.Forms.Label lblBrightness;
    private System.Windows.Forms.HScrollBar wndScrollTranspareny;
    private System.Windows.Forms.HScrollBar wndScrollSaturation;
    private System.Windows.Forms.HScrollBar wndScrollBrightness;
    private System.Windows.Forms.Timer tmrUpdate;
    private System.Windows.Forms.NumericUpDown nudA;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.NumericUpDown nudR;
    private System.Windows.Forms.NumericUpDown nudG;
    private System.Windows.Forms.NumericUpDown nudB;
    private System.Windows.Forms.Button btnSubmitARGB;
    private System.Windows.Forms.Button btnGetARGB;
    private System.Windows.Forms.ToolTip toolTip1;
  }
}
