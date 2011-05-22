using System.Windows.Forms;

namespace OgamaControls
{
  partial class PenSelectControl
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
      this.lblPenSize = new System.Windows.Forms.Label();
      this.lblPenStyle = new System.Windows.Forms.Label();
      this.cbbPenStyle = new System.Windows.Forms.ComboBox();
      this.penStyleArea = new OgamaControls.PenStyleArea();
      this.chbLeftCap = new System.Windows.Forms.CheckBox();
      this.nudPenSize = new System.Windows.Forms.NumericUpDown();
      this.tabControl1 = new System.Windows.Forms.TabControl();
      this.tabPage1 = new System.Windows.Forms.TabPage();
      this.tabPage2 = new System.Windows.Forms.TabPage();
      this.penColorSelectControl = new OgamaControls.ColorSelectControl();
      this.tabPage3 = new System.Windows.Forms.TabPage();
      this.lblRightCapHeight = new System.Windows.Forms.Label();
      this.lblRightCapWidth = new System.Windows.Forms.Label();
      this.nudRightCapHeight = new System.Windows.Forms.NumericUpDown();
      this.nudRightCapWidth = new System.Windows.Forms.NumericUpDown();
      this.lblLeftCapHeight = new System.Windows.Forms.Label();
      this.lblLeftCapWidth = new System.Windows.Forms.Label();
      this.nudLeftCapHeight = new System.Windows.Forms.NumericUpDown();
      this.cbbRightCapStyle = new System.Windows.Forms.ComboBox();
      this.nudLeftCapWidth = new System.Windows.Forms.NumericUpDown();
      this.chbRightCap = new System.Windows.Forms.CheckBox();
      this.cbbLeftCapStyle = new System.Windows.Forms.ComboBox();
      this.splitContainer1 = new System.Windows.Forms.SplitContainer();
      ((System.ComponentModel.ISupportInitialize)(this.nudPenSize)).BeginInit();
      this.tabControl1.SuspendLayout();
      this.tabPage1.SuspendLayout();
      this.tabPage2.SuspendLayout();
      this.tabPage3.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.nudRightCapHeight)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.nudRightCapWidth)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.nudLeftCapHeight)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.nudLeftCapWidth)).BeginInit();
      this.splitContainer1.Panel1.SuspendLayout();
      this.splitContainer1.Panel2.SuspendLayout();
      this.splitContainer1.SuspendLayout();
      this.SuspendLayout();
      // 
      // lblPenSize
      // 
      this.lblPenSize.AutoSize = true;
      this.lblPenSize.Location = new System.Drawing.Point(7, 37);
      this.lblPenSize.Name = "lblPenSize";
      this.lblPenSize.Size = new System.Drawing.Size(49, 13);
      this.lblPenSize.TabIndex = 2;
      this.lblPenSize.Text = "Pen Size";
      // 
      // lblPenStyle
      // 
      this.lblPenStyle.AutoSize = true;
      this.lblPenStyle.Location = new System.Drawing.Point(7, 13);
      this.lblPenStyle.Margin = new System.Windows.Forms.Padding(0);
      this.lblPenStyle.Name = "lblPenStyle";
      this.lblPenStyle.Size = new System.Drawing.Size(52, 13);
      this.lblPenStyle.TabIndex = 7;
      this.lblPenStyle.Text = "Pen Style";
      // 
      // cbbPenStyle
      // 
      this.cbbPenStyle.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
      this.cbbPenStyle.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
      this.cbbPenStyle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cbbPenStyle.FormattingEnabled = true;
      this.cbbPenStyle.Location = new System.Drawing.Point(69, 10);
      this.cbbPenStyle.Name = "cbbPenStyle";
      this.cbbPenStyle.Size = new System.Drawing.Size(127, 21);
      this.cbbPenStyle.TabIndex = 9;
      this.cbbPenStyle.SelectionChangeCommitted += new System.EventHandler(this.cbbPenStyle_SelectionChangeCommitted);
      // 
      // penStyleArea
      // 
      this.penStyleArea.BackColor = System.Drawing.Color.White;
      this.penStyleArea.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.penStyleArea.Dock = System.Windows.Forms.DockStyle.Fill;
      this.penStyleArea.Location = new System.Drawing.Point(0, 0);
      this.penStyleArea.Name = "penStyleArea";
      this.penStyleArea.PenColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
      this.penStyleArea.PenDashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
      this.penStyleArea.PenSize = 1F;
      this.penStyleArea.Size = new System.Drawing.Size(319, 60);
      this.penStyleArea.TabIndex = 12;
      // 
      // chbLeftCap
      // 
      this.chbLeftCap.AutoSize = true;
      this.chbLeftCap.Location = new System.Drawing.Point(9, 6);
      this.chbLeftCap.Name = "chbLeftCap";
      this.chbLeftCap.Size = new System.Drawing.Size(65, 17);
      this.chbLeftCap.TabIndex = 13;
      this.chbLeftCap.Text = "Left cap";
      this.chbLeftCap.UseVisualStyleBackColor = true;
      this.chbLeftCap.CheckedChanged += new System.EventHandler(this.chbLeftCap_CheckedChanged);
      // 
      // nudPenSize
      // 
      this.nudPenSize.DecimalPlaces = 1;
      this.nudPenSize.Increment = new decimal(new int[] {
            5,
            0,
            0,
            65536});
      this.nudPenSize.Location = new System.Drawing.Point(69, 35);
      this.nudPenSize.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.nudPenSize.Name = "nudPenSize";
      this.nudPenSize.Size = new System.Drawing.Size(127, 20);
      this.nudPenSize.TabIndex = 14;
      this.nudPenSize.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.nudPenSize.ValueChanged += new System.EventHandler(this.nudPenSize_ValueChanged);
      // 
      // tabControl1
      // 
      this.tabControl1.Controls.Add(this.tabPage1);
      this.tabControl1.Controls.Add(this.tabPage2);
      this.tabControl1.Controls.Add(this.tabPage3);
      this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tabControl1.Location = new System.Drawing.Point(0, 0);
      this.tabControl1.Name = "tabControl1";
      this.tabControl1.SelectedIndex = 0;
      this.tabControl1.Size = new System.Drawing.Size(319, 163);
      this.tabControl1.TabIndex = 15;
      // 
      // tabPage1
      // 
      this.tabPage1.Controls.Add(this.nudPenSize);
      this.tabPage1.Controls.Add(this.lblPenSize);
      this.tabPage1.Controls.Add(this.lblPenStyle);
      this.tabPage1.Controls.Add(this.cbbPenStyle);
      this.tabPage1.Location = new System.Drawing.Point(4, 22);
      this.tabPage1.Name = "tabPage1";
      this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
      this.tabPage1.Size = new System.Drawing.Size(311, 137);
      this.tabPage1.TabIndex = 0;
      this.tabPage1.Text = "Default";
      this.tabPage1.UseVisualStyleBackColor = true;
      // 
      // tabPage2
      // 
      this.tabPage2.Controls.Add(this.penColorSelectControl);
      this.tabPage2.Location = new System.Drawing.Point(4, 22);
      this.tabPage2.Name = "tabPage2";
      this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
      this.tabPage2.Size = new System.Drawing.Size(311, 137);
      this.tabPage2.TabIndex = 1;
      this.tabPage2.Text = "Color";
      this.tabPage2.UseVisualStyleBackColor = true;
      // 
      // penColorSelectControl
      // 
      this.penColorSelectControl.AccessibleDescription = "Select a color to use";
      this.penColorSelectControl.AccessibleName = "Select Color";
      this.penColorSelectControl.Dock = System.Windows.Forms.DockStyle.Fill;
      this.penColorSelectControl.Location = new System.Drawing.Point(3, 3);
      this.penColorSelectControl.Name = "penColorSelectControl";
      this.penColorSelectControl.SelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(42)))), ((int)(((byte)(191)))));
      this.penColorSelectControl.Size = new System.Drawing.Size(305, 131);
      this.penColorSelectControl.TabIndex = 11;
      this.penColorSelectControl.ColorChanged += new System.EventHandler<OgamaControls.ColorChangedEventArgs>(this.penColorSelectControl_ColorChanged);
      // 
      // tabPage3
      // 
      this.tabPage3.Controls.Add(this.lblRightCapHeight);
      this.tabPage3.Controls.Add(this.lblRightCapWidth);
      this.tabPage3.Controls.Add(this.nudRightCapHeight);
      this.tabPage3.Controls.Add(this.nudRightCapWidth);
      this.tabPage3.Controls.Add(this.lblLeftCapHeight);
      this.tabPage3.Controls.Add(this.lblLeftCapWidth);
      this.tabPage3.Controls.Add(this.nudLeftCapHeight);
      this.tabPage3.Controls.Add(this.cbbRightCapStyle);
      this.tabPage3.Controls.Add(this.nudLeftCapWidth);
      this.tabPage3.Controls.Add(this.chbRightCap);
      this.tabPage3.Controls.Add(this.cbbLeftCapStyle);
      this.tabPage3.Controls.Add(this.chbLeftCap);
      this.tabPage3.Location = new System.Drawing.Point(4, 22);
      this.tabPage3.Name = "tabPage3";
      this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
      this.tabPage3.Size = new System.Drawing.Size(311, 137);
      this.tabPage3.TabIndex = 2;
      this.tabPage3.Text = "Line Caps";
      this.tabPage3.UseVisualStyleBackColor = true;
      // 
      // lblRightCapHeight
      // 
      this.lblRightCapHeight.AutoSize = true;
      this.lblRightCapHeight.Location = new System.Drawing.Point(121, 82);
      this.lblRightCapHeight.Name = "lblRightCapHeight";
      this.lblRightCapHeight.Size = new System.Drawing.Size(38, 13);
      this.lblRightCapHeight.TabIndex = 19;
      this.lblRightCapHeight.Text = "Height";
      // 
      // lblRightCapWidth
      // 
      this.lblRightCapWidth.AutoSize = true;
      this.lblRightCapWidth.Location = new System.Drawing.Point(121, 59);
      this.lblRightCapWidth.Name = "lblRightCapWidth";
      this.lblRightCapWidth.Size = new System.Drawing.Size(35, 13);
      this.lblRightCapWidth.TabIndex = 20;
      this.lblRightCapWidth.Text = "Width";
      // 
      // nudRightCapHeight
      // 
      this.nudRightCapHeight.Location = new System.Drawing.Point(172, 80);
      this.nudRightCapHeight.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.nudRightCapHeight.Name = "nudRightCapHeight";
      this.nudRightCapHeight.Size = new System.Drawing.Size(52, 20);
      this.nudRightCapHeight.TabIndex = 17;
      this.nudRightCapHeight.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.nudRightCapHeight.ValueChanged += new System.EventHandler(this.nudRightCapHeight_ValueChanged);
      // 
      // nudRightCapWidth
      // 
      this.nudRightCapWidth.Location = new System.Drawing.Point(172, 57);
      this.nudRightCapWidth.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.nudRightCapWidth.Name = "nudRightCapWidth";
      this.nudRightCapWidth.Size = new System.Drawing.Size(52, 20);
      this.nudRightCapWidth.TabIndex = 18;
      this.nudRightCapWidth.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.nudRightCapWidth.ValueChanged += new System.EventHandler(this.nudRightCapWidth_ValueChanged);
      // 
      // lblLeftCapHeight
      // 
      this.lblLeftCapHeight.AutoSize = true;
      this.lblLeftCapHeight.Location = new System.Drawing.Point(6, 82);
      this.lblLeftCapHeight.Name = "lblLeftCapHeight";
      this.lblLeftCapHeight.Size = new System.Drawing.Size(38, 13);
      this.lblLeftCapHeight.TabIndex = 16;
      this.lblLeftCapHeight.Text = "Height";
      // 
      // lblLeftCapWidth
      // 
      this.lblLeftCapWidth.AutoSize = true;
      this.lblLeftCapWidth.Location = new System.Drawing.Point(6, 59);
      this.lblLeftCapWidth.Name = "lblLeftCapWidth";
      this.lblLeftCapWidth.Size = new System.Drawing.Size(35, 13);
      this.lblLeftCapWidth.TabIndex = 16;
      this.lblLeftCapWidth.Text = "Width";
      // 
      // nudLeftCapHeight
      // 
      this.nudLeftCapHeight.Location = new System.Drawing.Point(57, 80);
      this.nudLeftCapHeight.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.nudLeftCapHeight.Name = "nudLeftCapHeight";
      this.nudLeftCapHeight.Size = new System.Drawing.Size(52, 20);
      this.nudLeftCapHeight.TabIndex = 15;
      this.nudLeftCapHeight.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.nudLeftCapHeight.ValueChanged += new System.EventHandler(this.nudLeftCapHeight_ValueChanged);
      // 
      // cbbRightCapStyle
      // 
      this.cbbRightCapStyle.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
      this.cbbRightCapStyle.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
      this.cbbRightCapStyle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cbbRightCapStyle.FormattingEnabled = true;
      this.cbbRightCapStyle.Location = new System.Drawing.Point(124, 29);
      this.cbbRightCapStyle.Name = "cbbRightCapStyle";
      this.cbbRightCapStyle.Size = new System.Drawing.Size(100, 21);
      this.cbbRightCapStyle.TabIndex = 14;
      this.cbbRightCapStyle.SelectionChangeCommitted += new System.EventHandler(this.cbbRightCapStyle_SelectionChangeCommitted);
      // 
      // nudLeftCapWidth
      // 
      this.nudLeftCapWidth.Location = new System.Drawing.Point(57, 57);
      this.nudLeftCapWidth.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.nudLeftCapWidth.Name = "nudLeftCapWidth";
      this.nudLeftCapWidth.Size = new System.Drawing.Size(52, 20);
      this.nudLeftCapWidth.TabIndex = 15;
      this.nudLeftCapWidth.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.nudLeftCapWidth.ValueChanged += new System.EventHandler(this.nudLeftCapWidth_ValueChanged);
      // 
      // chbRightCap
      // 
      this.chbRightCap.AutoSize = true;
      this.chbRightCap.Location = new System.Drawing.Point(124, 6);
      this.chbRightCap.Name = "chbRightCap";
      this.chbRightCap.Size = new System.Drawing.Size(72, 17);
      this.chbRightCap.TabIndex = 13;
      this.chbRightCap.Text = "Right cap";
      this.chbRightCap.UseVisualStyleBackColor = true;
      this.chbRightCap.CheckedChanged += new System.EventHandler(this.chbRightCap_CheckedChanged);
      // 
      // cbbLeftCapStyle
      // 
      this.cbbLeftCapStyle.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
      this.cbbLeftCapStyle.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
      this.cbbLeftCapStyle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cbbLeftCapStyle.FormattingEnabled = true;
      this.cbbLeftCapStyle.Location = new System.Drawing.Point(9, 29);
      this.cbbLeftCapStyle.Name = "cbbLeftCapStyle";
      this.cbbLeftCapStyle.Size = new System.Drawing.Size(100, 21);
      this.cbbLeftCapStyle.TabIndex = 14;
      this.cbbLeftCapStyle.SelectionChangeCommitted += new System.EventHandler(this.cbbLeftCapStyle_SelectionChangeCommitted);
      // 
      // splitContainer1
      // 
      this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
      this.splitContainer1.IsSplitterFixed = true;
      this.splitContainer1.Location = new System.Drawing.Point(0, 0);
      this.splitContainer1.Name = "splitContainer1";
      this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
      // 
      // splitContainer1.Panel1
      // 
      this.splitContainer1.Panel1.Controls.Add(this.penStyleArea);
      // 
      // splitContainer1.Panel2
      // 
      this.splitContainer1.Panel2.Controls.Add(this.tabControl1);
      this.splitContainer1.Size = new System.Drawing.Size(319, 227);
      this.splitContainer1.SplitterDistance = 60;
      this.splitContainer1.TabIndex = 16;
      // 
      // PenSelectControl
      // 
      this.AccessibleDescription = "Select a color to use";
      this.AccessibleName = "Select Color";
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.splitContainer1);
      this.Margin = new System.Windows.Forms.Padding(5);
      this.Name = "PenSelectControl";
      this.Size = new System.Drawing.Size(319, 227);
      ((System.ComponentModel.ISupportInitialize)(this.nudPenSize)).EndInit();
      this.tabControl1.ResumeLayout(false);
      this.tabPage1.ResumeLayout(false);
      this.tabPage1.PerformLayout();
      this.tabPage2.ResumeLayout(false);
      this.tabPage3.ResumeLayout(false);
      this.tabPage3.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.nudRightCapHeight)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.nudRightCapWidth)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.nudLeftCapHeight)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.nudLeftCapWidth)).EndInit();
      this.splitContainer1.Panel1.ResumeLayout(false);
      this.splitContainer1.Panel2.ResumeLayout(false);
      this.splitContainer1.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    private System.Windows.Forms.Label lblPenSize;
    private Label lblPenStyle;
    private ComboBox cbbPenStyle;
    private PenStyleArea penStyleArea;
    private CheckBox chbLeftCap;
    private NumericUpDown nudPenSize;
    private TabControl tabControl1;
    private TabPage tabPage1;
    private TabPage tabPage2;
    private ColorSelectControl penColorSelectControl;
    private TabPage tabPage3;
    private SplitContainer splitContainer1;
    private Label lblLeftCapHeight;
    private Label lblLeftCapWidth;
    private NumericUpDown nudLeftCapHeight;
    private ComboBox cbbRightCapStyle;
    private NumericUpDown nudLeftCapWidth;
    private CheckBox chbRightCap;
    private ComboBox cbbLeftCapStyle;

    #endregion
    private Label lblRightCapHeight;
    private Label lblRightCapWidth;
    private NumericUpDown nudRightCapHeight;
    private NumericUpDown nudRightCapWidth;
  }
}
