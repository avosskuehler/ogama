namespace Ogama.Modules.SlideshowDesign.DesignModule.StimuliDialogs
{
  using Ogama.Modules.Common.Controls;

  partial class TextDialog
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
      if (disposing && (this.components != null))
      {
        this.components.Dispose();
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TextDialog));
      this.btnOK = new System.Windows.Forms.Button();
      this.btnCancel = new System.Windows.Forms.Button();
      this.splitContainer1 = new System.Windows.Forms.SplitContainer();
      this.splitContainer2 = new System.Windows.Forms.SplitContainer();
      this.splitContainer3 = new System.Windows.Forms.SplitContainer();
      this.toolStrip1 = new System.Windows.Forms.ToolStrip();
      this.cbbFontFace = new System.Windows.Forms.ToolStripComboBox();
      this.nudFontSize = new OgamaControls.ToolStripNumericUpDown();
      this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
      this.btnAlignLeft = new System.Windows.Forms.ToolStripButton();
      this.btnAlignCenter = new System.Windows.Forms.ToolStripButton();
      this.btnAlignRight = new System.Windows.Forms.ToolStripButton();
      this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
      this.btnBold = new System.Windows.Forms.ToolStripButton();
      this.btnItalic = new System.Windows.Forms.ToolStripButton();
      this.btnUnderline = new System.Windows.Forms.ToolStripButton();
      this.btnStrikeout = new System.Windows.Forms.ToolStripButton();
      this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
      this.nudLineSpacing = new OgamaControls.ToolStripNumericUpDown();
      this.txbInstruction = new System.Windows.Forms.TextBox();
      this.groupBox2 = new System.Windows.Forms.GroupBox();
      this.clcTextColor = new OgamaControls.ColorSelectControl();
      this.grpBorder = new System.Windows.Forms.GroupBox();
      this.pbcBorder = new OgamaControls.PenAndBrushControl();
      this.groupBox1 = new System.Windows.Forms.GroupBox();
      this.audioControl = new OgamaControls.AudioControl();
      this.btnHelp = new System.Windows.Forms.Button();
      this.panel1 = new System.Windows.Forms.Panel();
      this.dialogTop1 = new DialogTop();
      this.nudPadding = new OgamaControls.ToolStripNumericUpDown();
      this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
      this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
      this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
      this.splitContainer1.Panel1.SuspendLayout();
      this.splitContainer1.Panel2.SuspendLayout();
      this.splitContainer1.SuspendLayout();
      this.splitContainer2.Panel1.SuspendLayout();
      this.splitContainer2.Panel2.SuspendLayout();
      this.splitContainer2.SuspendLayout();
      this.splitContainer3.Panel1.SuspendLayout();
      this.splitContainer3.Panel2.SuspendLayout();
      this.splitContainer3.SuspendLayout();
      this.toolStrip1.SuspendLayout();
      this.groupBox2.SuspendLayout();
      this.grpBorder.SuspendLayout();
      this.groupBox1.SuspendLayout();
      this.panel1.SuspendLayout();
      this.SuspendLayout();
      // 
      // btnOK
      // 
      this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
      this.btnOK.Location = new System.Drawing.Point(705, 3);
      this.btnOK.Name = "btnOK";
      this.btnOK.Size = new System.Drawing.Size(65, 25);
      this.btnOK.TabIndex = 21;
      this.btnOK.Text = "OK";
      this.btnOK.UseVisualStyleBackColor = true;
      this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
      // 
      // btnCancel
      // 
      this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.btnCancel.Location = new System.Drawing.Point(782, 3);
      this.btnCancel.Name = "btnCancel";
      this.btnCancel.Size = new System.Drawing.Size(65, 25);
      this.btnCancel.TabIndex = 20;
      this.btnCancel.Text = "Cancel";
      this.btnCancel.UseVisualStyleBackColor = true;
      // 
      // splitContainer1
      // 
      this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
      this.splitContainer1.IsSplitterFixed = true;
      this.splitContainer1.Location = new System.Drawing.Point(3, 3);
      this.splitContainer1.Name = "splitContainer1";
      this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
      // 
      // splitContainer1.Panel1
      // 
      this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
      // 
      // splitContainer1.Panel2
      // 
      this.splitContainer1.Panel2.Controls.Add(this.btnHelp);
      this.splitContainer1.Panel2.Controls.Add(this.btnOK);
      this.splitContainer1.Panel2.Controls.Add(this.btnCancel);
      this.splitContainer1.Size = new System.Drawing.Size(850, 416);
      this.splitContainer1.SplitterDistance = 382;
      this.splitContainer1.TabIndex = 23;
      // 
      // splitContainer2
      // 
      this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
      this.splitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
      this.splitContainer2.IsSplitterFixed = true;
      this.splitContainer2.Location = new System.Drawing.Point(0, 0);
      this.splitContainer2.Name = "splitContainer2";
      this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
      // 
      // splitContainer2.Panel1
      // 
      this.splitContainer2.Panel1.Controls.Add(this.splitContainer3);
      // 
      // splitContainer2.Panel2
      // 
      this.splitContainer2.Panel2.Controls.Add(this.groupBox2);
      this.splitContainer2.Panel2.Controls.Add(this.grpBorder);
      this.splitContainer2.Panel2.Controls.Add(this.groupBox1);
      this.splitContainer2.Panel2MinSize = 150;
      this.splitContainer2.Size = new System.Drawing.Size(850, 382);
      this.splitContainer2.SplitterDistance = 228;
      this.splitContainer2.TabIndex = 1;
      // 
      // splitContainer3
      // 
      this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
      this.splitContainer3.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
      this.splitContainer3.IsSplitterFixed = true;
      this.splitContainer3.Location = new System.Drawing.Point(0, 0);
      this.splitContainer3.Name = "splitContainer3";
      this.splitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal;
      // 
      // splitContainer3.Panel1
      // 
      this.splitContainer3.Panel1.Controls.Add(this.toolStrip1);
      // 
      // splitContainer3.Panel2
      // 
      this.splitContainer3.Panel2.Controls.Add(this.txbInstruction);
      this.splitContainer3.Size = new System.Drawing.Size(850, 228);
      this.splitContainer3.SplitterDistance = 25;
      this.splitContainer3.TabIndex = 1;
      // 
      // toolStrip1
      // 
      this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cbbFontFace,
            this.nudFontSize,
            this.toolStripSeparator1,
            this.btnAlignLeft,
            this.btnAlignCenter,
            this.btnAlignRight,
            this.toolStripSeparator2,
            this.btnBold,
            this.btnItalic,
            this.btnUnderline,
            this.btnStrikeout,
            this.toolStripSeparator3,
            this.toolStripLabel1,
            this.nudLineSpacing,
            this.toolStripSeparator4,
            this.toolStripLabel2,
            this.nudPadding});
      this.toolStrip1.Location = new System.Drawing.Point(0, 0);
      this.toolStrip1.Name = "toolStrip1";
      this.toolStrip1.Size = new System.Drawing.Size(850, 26);
      this.toolStrip1.TabIndex = 0;
      this.toolStrip1.Text = "toolStrip1";
      // 
      // cbbFontFace
      // 
      this.cbbFontFace.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cbbFontFace.Name = "cbbFontFace";
      this.cbbFontFace.Size = new System.Drawing.Size(121, 26);
      this.cbbFontFace.SelectedIndexChanged += new System.EventHandler(this.cbbFontFace_SelectedIndexChanged);
      // 
      // nudFontSize
      // 
      this.nudFontSize.DecimalPlaces = 0;
      this.nudFontSize.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.nudFontSize.Maximum = new decimal(new int[] {
            100,
            0,
            0,
            0});
      this.nudFontSize.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.nudFontSize.Name = "nudFontSize";
      this.nudFontSize.Size = new System.Drawing.Size(41, 23);
      this.nudFontSize.Text = "1";
      this.nudFontSize.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.nudFontSize.Click += new System.EventHandler(this.nudFontSize_Click);
      // 
      // toolStripSeparator1
      // 
      this.toolStripSeparator1.Name = "toolStripSeparator1";
      this.toolStripSeparator1.Size = new System.Drawing.Size(6, 26);
      // 
      // btnAlignLeft
      // 
      this.btnAlignLeft.Checked = true;
      this.btnAlignLeft.CheckState = System.Windows.Forms.CheckState.Checked;
      this.btnAlignLeft.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.btnAlignLeft.Image = global::Ogama.Properties.Resources.AlignTableCellMiddleLeftHS;
      this.btnAlignLeft.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnAlignLeft.Name = "btnAlignLeft";
      this.btnAlignLeft.Size = new System.Drawing.Size(23, 23);
      this.btnAlignLeft.Text = "Align left";
      this.btnAlignLeft.Click += new System.EventHandler(this.btnAlignLeft_Click);
      // 
      // btnAlignCenter
      // 
      this.btnAlignCenter.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.btnAlignCenter.Image = ((System.Drawing.Image)(resources.GetObject("btnAlignCenter.Image")));
      this.btnAlignCenter.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnAlignCenter.Name = "btnAlignCenter";
      this.btnAlignCenter.Size = new System.Drawing.Size(23, 23);
      this.btnAlignCenter.Text = "Align center";
      this.btnAlignCenter.Click += new System.EventHandler(this.btnAlignCenter_Click);
      // 
      // btnAlignRight
      // 
      this.btnAlignRight.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.btnAlignRight.Image = global::Ogama.Properties.Resources.AlignTableCellMiddleRightHS;
      this.btnAlignRight.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnAlignRight.Name = "btnAlignRight";
      this.btnAlignRight.Size = new System.Drawing.Size(23, 23);
      this.btnAlignRight.Text = "Align right";
      this.btnAlignRight.Click += new System.EventHandler(this.btnAlignRight_Click);
      // 
      // toolStripSeparator2
      // 
      this.toolStripSeparator2.Name = "toolStripSeparator2";
      this.toolStripSeparator2.Size = new System.Drawing.Size(6, 26);
      // 
      // btnBold
      // 
      this.btnBold.CheckOnClick = true;
      this.btnBold.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.btnBold.Image = global::Ogama.Properties.Resources.boldhs;
      this.btnBold.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnBold.Name = "btnBold";
      this.btnBold.Size = new System.Drawing.Size(23, 23);
      this.btnBold.Text = "Bold";
      this.btnBold.Click += new System.EventHandler(this.btnBold_Click);
      // 
      // btnItalic
      // 
      this.btnItalic.CheckOnClick = true;
      this.btnItalic.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.btnItalic.Image = global::Ogama.Properties.Resources.ItalicHS;
      this.btnItalic.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnItalic.Name = "btnItalic";
      this.btnItalic.Size = new System.Drawing.Size(23, 23);
      this.btnItalic.Text = "Italic";
      this.btnItalic.Click += new System.EventHandler(this.btnItalic_Click);
      // 
      // btnUnderline
      // 
      this.btnUnderline.CheckOnClick = true;
      this.btnUnderline.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.btnUnderline.Image = global::Ogama.Properties.Resources.underlineHS;
      this.btnUnderline.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnUnderline.Name = "btnUnderline";
      this.btnUnderline.Size = new System.Drawing.Size(23, 23);
      this.btnUnderline.Text = "Underline";
      this.btnUnderline.Click += new System.EventHandler(this.btnUnderline_Click);
      // 
      // btnStrikeout
      // 
      this.btnStrikeout.CheckOnClick = true;
      this.btnStrikeout.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.btnStrikeout.Image = global::Ogama.Properties.Resources.StrikeoutHS;
      this.btnStrikeout.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnStrikeout.Name = "btnStrikeout";
      this.btnStrikeout.Size = new System.Drawing.Size(23, 23);
      this.btnStrikeout.Text = "Strikeout";
      this.btnStrikeout.Click += new System.EventHandler(this.btnStrikeout_Click);
      // 
      // toolStripSeparator3
      // 
      this.toolStripSeparator3.Name = "toolStripSeparator3";
      this.toolStripSeparator3.Size = new System.Drawing.Size(6, 26);
      // 
      // nudLineSpacing
      // 
      this.nudLineSpacing.DecimalPlaces = 1;
      this.nudLineSpacing.Increment = new decimal(new int[] {
            5,
            0,
            0,
            65536});
      this.nudLineSpacing.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
      this.nudLineSpacing.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
      this.nudLineSpacing.Name = "nudLineSpacing";
      this.nudLineSpacing.Size = new System.Drawing.Size(44, 23);
      this.nudLineSpacing.Text = "1,0";
      this.nudLineSpacing.ToolTipText = "Please specify the line spacing";
      this.nudLineSpacing.Value = new decimal(new int[] {
            10,
            0,
            0,
            65536});
      // 
      // txbInstruction
      // 
      this.txbInstruction.Dock = System.Windows.Forms.DockStyle.Fill;
      this.txbInstruction.Location = new System.Drawing.Point(0, 0);
      this.txbInstruction.Multiline = true;
      this.txbInstruction.Name = "txbInstruction";
      this.txbInstruction.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
      this.txbInstruction.Size = new System.Drawing.Size(850, 199);
      this.txbInstruction.TabIndex = 0;
      // 
      // groupBox2
      // 
      this.groupBox2.Controls.Add(this.clcTextColor);
      this.groupBox2.Dock = System.Windows.Forms.DockStyle.Left;
      this.groupBox2.Location = new System.Drawing.Point(0, 0);
      this.groupBox2.Name = "groupBox2";
      this.groupBox2.Size = new System.Drawing.Size(319, 150);
      this.groupBox2.TabIndex = 3;
      this.groupBox2.TabStop = false;
      this.groupBox2.Text = "TextColor";
      // 
      // clcTextColor
      // 
      this.clcTextColor.Dock = System.Windows.Forms.DockStyle.Fill;
      this.clcTextColor.Location = new System.Drawing.Point(3, 16);
      this.clcTextColor.Name = "clcTextColor";
      this.clcTextColor.SelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(47)))), ((int)(((byte)(191)))));
      this.clcTextColor.Size = new System.Drawing.Size(313, 131);
      this.clcTextColor.TabIndex = 2;
      this.clcTextColor.ColorChanged += new System.EventHandler<OgamaControls.ColorChangedEventArgs>(this.clcTextColor_ColorChanged);
      // 
      // grpBorder
      // 
      this.grpBorder.Controls.Add(this.pbcBorder);
      this.grpBorder.Location = new System.Drawing.Point(326, 0);
      this.grpBorder.Name = "grpBorder";
      this.grpBorder.Size = new System.Drawing.Size(259, 150);
      this.grpBorder.TabIndex = 0;
      this.grpBorder.TabStop = false;
      this.grpBorder.Text = "Border and fill style ...";
      // 
      // pbcBorder
      // 
      this.pbcBorder.Dock = System.Windows.Forms.DockStyle.Left;
      this.pbcBorder.DrawAction = VectorGraphics.Elements.ShapeDrawAction.None;
      this.pbcBorder.Location = new System.Drawing.Point(3, 16);
      this.pbcBorder.Name = "pbcBorder";
      this.pbcBorder.NewFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.pbcBorder.NewFontColor = System.Drawing.SystemColors.GrayText;
      this.pbcBorder.NewName = "";
      this.pbcBorder.NewTextAlignment = VectorGraphics.Elements.VGAlignment.None;
      this.pbcBorder.Size = new System.Drawing.Size(249, 131);
      this.pbcBorder.TabIndex = 0;
      // 
      // groupBox1
      // 
      this.groupBox1.Controls.Add(this.audioControl);
      this.groupBox1.Dock = System.Windows.Forms.DockStyle.Right;
      this.groupBox1.Location = new System.Drawing.Point(592, 0);
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.Size = new System.Drawing.Size(258, 150);
      this.groupBox1.TabIndex = 1;
      this.groupBox1.TabStop = false;
      this.groupBox1.Text = "Audio properties";
      // 
      // audioControl
      // 
      this.audioControl.Dock = System.Windows.Forms.DockStyle.Fill;
      this.audioControl.Location = new System.Drawing.Point(3, 16);
      this.audioControl.Name = "audioControl";
      this.audioControl.ShouldPlay = false;
      this.audioControl.Size = new System.Drawing.Size(252, 131);
      this.audioControl.TabIndex = 0;
      // 
      // btnHelp
      // 
      this.btnHelp.Image = global::Ogama.Properties.Resources.HelpBmp;
      this.btnHelp.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.btnHelp.Location = new System.Drawing.Point(3, 3);
      this.btnHelp.Name = "btnHelp";
      this.btnHelp.Size = new System.Drawing.Size(25, 25);
      this.btnHelp.TabIndex = 23;
      this.btnHelp.UseVisualStyleBackColor = true;
      this.btnHelp.Click += new System.EventHandler(this.btnHelp_Click);
      // 
      // panel1
      // 
      this.panel1.Controls.Add(this.splitContainer1);
      this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.panel1.Location = new System.Drawing.Point(0, 60);
      this.panel1.Name = "panel1";
      this.panel1.Padding = new System.Windows.Forms.Padding(3);
      this.panel1.Size = new System.Drawing.Size(856, 422);
      this.panel1.TabIndex = 19;
      // 
      // dialogTop1
      // 
      this.dialogTop1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(237)))), ((int)(((byte)(237)))));
      this.dialogTop1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("dialogTop1.BackgroundImage")));
      this.dialogTop1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
      this.dialogTop1.Description = "Please enter the new instruction, and define its appearance";
      this.dialogTop1.Dock = System.Windows.Forms.DockStyle.Top;
      this.dialogTop1.Location = new System.Drawing.Point(0, 0);
      this.dialogTop1.Logo = global::Ogama.Properties.Resources.textdoc;
      this.dialogTop1.Name = "dialogTop1";
      this.dialogTop1.Size = new System.Drawing.Size(856, 60);
      this.dialogTop1.TabIndex = 22;
      // 
      // nudPadding
      // 
      this.nudPadding.DecimalPlaces = 0;
      this.nudPadding.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.nudPadding.Maximum = new decimal(new int[] {
            100,
            0,
            0,
            0});
      this.nudPadding.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
      this.nudPadding.Name = "nudPadding";
      this.nudPadding.Size = new System.Drawing.Size(41, 23);
      this.nudPadding.Text = "6";
      this.nudPadding.Value = new decimal(new int[] {
            6,
            0,
            0,
            0});
      // 
      // toolStripLabel1
      // 
      this.toolStripLabel1.Name = "toolStripLabel1";
      this.toolStripLabel1.Size = new System.Drawing.Size(74, 23);
      this.toolStripLabel1.Text = "Line Spacing";
      // 
      // toolStripSeparator4
      // 
      this.toolStripSeparator4.Name = "toolStripSeparator4";
      this.toolStripSeparator4.Size = new System.Drawing.Size(6, 26);
      // 
      // toolStripLabel2
      // 
      this.toolStripLabel2.Name = "toolStripLabel2";
      this.toolStripLabel2.Size = new System.Drawing.Size(51, 23);
      this.toolStripLabel2.Text = "Padding";
      // 
      // TextDialog
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.CancelButton = this.btnCancel;
      this.ClientSize = new System.Drawing.Size(856, 482);
      this.Controls.Add(this.panel1);
      this.Controls.Add(this.dialogTop1);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.Name = "TextDialog";
      this.ShowIcon = false;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "Add new instruction ...";
      this.Shown += new System.EventHandler(this.TextDialog_Shown);
      this.splitContainer1.Panel1.ResumeLayout(false);
      this.splitContainer1.Panel2.ResumeLayout(false);
      this.splitContainer1.ResumeLayout(false);
      this.splitContainer2.Panel1.ResumeLayout(false);
      this.splitContainer2.Panel2.ResumeLayout(false);
      this.splitContainer2.ResumeLayout(false);
      this.splitContainer3.Panel1.ResumeLayout(false);
      this.splitContainer3.Panel1.PerformLayout();
      this.splitContainer3.Panel2.ResumeLayout(false);
      this.splitContainer3.Panel2.PerformLayout();
      this.splitContainer3.ResumeLayout(false);
      this.toolStrip1.ResumeLayout(false);
      this.toolStrip1.PerformLayout();
      this.groupBox2.ResumeLayout(false);
      this.grpBorder.ResumeLayout(false);
      this.groupBox1.ResumeLayout(false);
      this.panel1.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Button btnOK;
    private System.Windows.Forms.Button btnCancel;
    private DialogTop dialogTop1;
    private System.Windows.Forms.SplitContainer splitContainer1;
    private System.Windows.Forms.Panel panel1;
    private System.Windows.Forms.Button btnHelp;
    private System.Windows.Forms.SplitContainer splitContainer2;
    private System.Windows.Forms.GroupBox grpBorder;
    private OgamaControls.PenAndBrushControl pbcBorder;
    private System.Windows.Forms.GroupBox groupBox1;
    private OgamaControls.AudioControl audioControl;
    private System.Windows.Forms.SplitContainer splitContainer3;
    private System.Windows.Forms.TextBox txbInstruction;
    private System.Windows.Forms.ToolStrip toolStrip1;
    private System.Windows.Forms.ToolStripButton btnAlignLeft;
    private System.Windows.Forms.ToolStripButton btnAlignCenter;
    private System.Windows.Forms.ToolStripButton btnAlignRight;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
    private OgamaControls.ColorSelectControl clcTextColor;
    private System.Windows.Forms.GroupBox groupBox2;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
    private System.Windows.Forms.ToolStripButton btnItalic;
    private System.Windows.Forms.ToolStripButton btnBold;
    private System.Windows.Forms.ToolStripButton btnStrikeout;
    private System.Windows.Forms.ToolStripButton btnUnderline;
    private System.Windows.Forms.ToolStripComboBox cbbFontFace;
    private OgamaControls.ToolStripNumericUpDown nudFontSize;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
    private OgamaControls.ToolStripNumericUpDown nudLineSpacing;
    private OgamaControls.ToolStripNumericUpDown nudPadding;
    private System.Windows.Forms.ToolStripLabel toolStripLabel1;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
    private System.Windows.Forms.ToolStripLabel toolStripLabel2;
  }
}