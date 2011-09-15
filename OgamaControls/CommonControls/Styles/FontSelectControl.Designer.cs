using System.Windows.Forms;

namespace OgamaControls
{
  partial class FontSelectControl
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
      this.lblFontSize = new System.Windows.Forms.Label();
      this.lblFont = new System.Windows.Forms.Label();
      this.cbbFontFace = new System.Windows.Forms.ComboBox();
      this.cbbFontStyle = new System.Windows.Forms.ComboBox();
      this.label1 = new System.Windows.Forms.Label();
      this.fontColorSelectControl = new OgamaControls.ColorSelectControl();
      this.fontStyleArea = new OgamaControls.FontStyleArea();
      this.nudFontSize = new System.Windows.Forms.NumericUpDown();
      this.tabControl1 = new System.Windows.Forms.TabControl();
      this.tbpDefault = new System.Windows.Forms.TabPage();
      this.cbbAlignment = new System.Windows.Forms.ComboBox();
      this.label2 = new System.Windows.Forms.Label();
      this.tbpColor = new System.Windows.Forms.TabPage();
      this.splitContainer1 = new System.Windows.Forms.SplitContainer();
      ((System.ComponentModel.ISupportInitialize)(this.nudFontSize)).BeginInit();
      this.tabControl1.SuspendLayout();
      this.tbpDefault.SuspendLayout();
      this.tbpColor.SuspendLayout();
      this.splitContainer1.Panel1.SuspendLayout();
      this.splitContainer1.Panel2.SuspendLayout();
      this.splitContainer1.SuspendLayout();
      this.SuspendLayout();
      // 
      // lblFontSize
      // 
      this.lblFontSize.AutoSize = true;
      this.lblFontSize.Location = new System.Drawing.Point(7, 61);
      this.lblFontSize.Name = "lblFontSize";
      this.lblFontSize.Size = new System.Drawing.Size(49, 13);
      this.lblFontSize.TabIndex = 2;
      this.lblFontSize.Text = "Font si&ze";
      // 
      // lblFont
      // 
      this.lblFont.AutoSize = true;
      this.lblFont.Location = new System.Drawing.Point(7, 10);
      this.lblFont.Name = "lblFont";
      this.lblFont.Size = new System.Drawing.Size(55, 13);
      this.lblFont.TabIndex = 0;
      this.lblFont.Text = "&Font Face";
      // 
      // cbbFontFace
      // 
      this.cbbFontFace.Items.AddRange(new object[] {
            "Arial"});
      this.cbbFontFace.Location = new System.Drawing.Point(87, 7);
      this.cbbFontFace.MaxDropDownItems = 16;
      this.cbbFontFace.Name = "cbbFontFace";
      this.cbbFontFace.Size = new System.Drawing.Size(124, 21);
      this.cbbFontFace.TabIndex = 1;
      this.cbbFontFace.Text = "Arial";
      this.cbbFontFace.SelectedValueChanged += new System.EventHandler(this.OnFontFaceChanged);
      this.cbbFontFace.TextChanged += new System.EventHandler(this.OnFontFaceChanged);
      // 
      // cbbFontStyle
      // 
      this.cbbFontStyle.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
      this.cbbFontStyle.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
      this.cbbFontStyle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cbbFontStyle.Location = new System.Drawing.Point(87, 33);
      this.cbbFontStyle.Name = "cbbFontStyle";
      this.cbbFontStyle.Size = new System.Drawing.Size(124, 21);
      this.cbbFontStyle.TabIndex = 8;
      this.cbbFontStyle.SelectedValueChanged += new System.EventHandler(this.OnFontStyleTextChanged);
      this.cbbFontStyle.TextChanged += new System.EventHandler(this.OnFontStyleTextChanged);
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(7, 36);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(54, 13);
      this.label1.TabIndex = 9;
      this.label1.Text = "Font Style";
      // 
      // fontColorSelectControl
      // 
      this.fontColorSelectControl.AccessibleDescription = "Select a color to use";
      this.fontColorSelectControl.AccessibleName = "Select Color";
      this.fontColorSelectControl.Dock = System.Windows.Forms.DockStyle.Fill;
      this.fontColorSelectControl.Location = new System.Drawing.Point(3, 3);
      this.fontColorSelectControl.Name = "fontColorSelectControl";
      this.fontColorSelectControl.SelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(191)))));
      this.fontColorSelectControl.Size = new System.Drawing.Size(307, 129);
      this.fontColorSelectControl.TabIndex = 10;
      this.fontColorSelectControl.ColorChanged += new System.EventHandler<OgamaControls.ColorChangedEventArgs>(this.fontColorSelectControl_ColorChanged);
      // 
      // fontStyleArea
      // 
      this.fontStyleArea.BackColor = System.Drawing.Color.White;
      this.fontStyleArea.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.fontStyleArea.Dock = System.Windows.Forms.DockStyle.Fill;
      this.fontStyleArea.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World);
      this.fontStyleArea.FontAlignment = VectorGraphics.Elements.VGAlignment.Center;
      this.fontStyleArea.FontColor = System.Drawing.Color.Black;
      this.fontStyleArea.Location = new System.Drawing.Point(0, 0);
      this.fontStyleArea.Name = "fontStyleArea";
      this.fontStyleArea.SampleString = "Sample ...";
      this.fontStyleArea.Size = new System.Drawing.Size(321, 60);
      this.fontStyleArea.TabIndex = 11;
      // 
      // nudFontSize
      // 
      this.nudFontSize.Location = new System.Drawing.Point(87, 59);
      this.nudFontSize.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            0});
      this.nudFontSize.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.nudFontSize.Name = "nudFontSize";
      this.nudFontSize.Size = new System.Drawing.Size(124, 20);
      this.nudFontSize.TabIndex = 12;
      this.nudFontSize.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.nudFontSize.ValueChanged += new System.EventHandler(this.nudFontSize_ValueChanged);
      // 
      // tabControl1
      // 
      this.tabControl1.Controls.Add(this.tbpDefault);
      this.tabControl1.Controls.Add(this.tbpColor);
      this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tabControl1.Location = new System.Drawing.Point(0, 0);
      this.tabControl1.Name = "tabControl1";
      this.tabControl1.SelectedIndex = 0;
      this.tabControl1.Size = new System.Drawing.Size(321, 161);
      this.tabControl1.TabIndex = 13;
      // 
      // tbpDefault
      // 
      this.tbpDefault.Controls.Add(this.cbbAlignment);
      this.tbpDefault.Controls.Add(this.label2);
      this.tbpDefault.Controls.Add(this.cbbFontStyle);
      this.tbpDefault.Controls.Add(this.nudFontSize);
      this.tbpDefault.Controls.Add(this.cbbFontFace);
      this.tbpDefault.Controls.Add(this.lblFont);
      this.tbpDefault.Controls.Add(this.label1);
      this.tbpDefault.Controls.Add(this.lblFontSize);
      this.tbpDefault.Location = new System.Drawing.Point(4, 22);
      this.tbpDefault.Name = "tbpDefault";
      this.tbpDefault.Padding = new System.Windows.Forms.Padding(3);
      this.tbpDefault.Size = new System.Drawing.Size(313, 135);
      this.tbpDefault.TabIndex = 0;
      this.tbpDefault.Text = "Default";
      this.tbpDefault.UseVisualStyleBackColor = true;
      // 
      // cbbAlignment
      // 
      this.cbbAlignment.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
      this.cbbAlignment.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
      this.cbbAlignment.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cbbAlignment.Location = new System.Drawing.Point(87, 84);
      this.cbbAlignment.Name = "cbbAlignment";
      this.cbbAlignment.Size = new System.Drawing.Size(124, 21);
      this.cbbAlignment.TabIndex = 13;
      this.cbbAlignment.SelectedIndexChanged += new System.EventHandler(this.cbbAlignment_SelectedIndexChanged);
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(7, 87);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(77, 13);
      this.label2.TabIndex = 14;
      this.label2.Text = "Font Alignment";
      // 
      // tbpColor
      // 
      this.tbpColor.Controls.Add(this.fontColorSelectControl);
      this.tbpColor.Location = new System.Drawing.Point(4, 22);
      this.tbpColor.Name = "tbpColor";
      this.tbpColor.Padding = new System.Windows.Forms.Padding(3);
      this.tbpColor.Size = new System.Drawing.Size(313, 135);
      this.tbpColor.TabIndex = 1;
      this.tbpColor.Text = "Color";
      this.tbpColor.UseVisualStyleBackColor = true;
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
      this.splitContainer1.Panel1.Controls.Add(this.fontStyleArea);
      // 
      // splitContainer1.Panel2
      // 
      this.splitContainer1.Panel2.Controls.Add(this.tabControl1);
      this.splitContainer1.Size = new System.Drawing.Size(321, 225);
      this.splitContainer1.SplitterDistance = 60;
      this.splitContainer1.TabIndex = 14;
      // 
      // FontSelectControl
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.splitContainer1);
      this.Name = "FontSelectControl";
      this.Size = new System.Drawing.Size(321, 225);
      ((System.ComponentModel.ISupportInitialize)(this.nudFontSize)).EndInit();
      this.tabControl1.ResumeLayout(false);
      this.tbpDefault.ResumeLayout(false);
      this.tbpDefault.PerformLayout();
      this.tbpColor.ResumeLayout(false);
      this.splitContainer1.Panel1.ResumeLayout(false);
      this.splitContainer1.Panel2.ResumeLayout(false);
      this.splitContainer1.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    private System.Windows.Forms.ComboBox cbbFontFace;
    private System.Windows.Forms.Label lblFont;
    private System.Windows.Forms.Label lblFontSize;
    private ComboBox cbbFontStyle;
    private Label label1;
    private ColorSelectControl fontColorSelectControl;
    private FontStyleArea fontStyleArea;
    private NumericUpDown nudFontSize;
    private TabControl tabControl1;
    private TabPage tbpDefault;
    private TabPage tbpColor;
    private SplitContainer splitContainer1;

    #endregion
    private ComboBox cbbAlignment;
    private Label label2;
  }
}
