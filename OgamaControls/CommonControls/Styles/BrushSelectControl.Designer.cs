namespace OgamaControls
{
  partial class BrushSelectControl
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
      this.tbcBrushType = new System.Windows.Forms.TabControl();
      this.tbpSolid = new System.Windows.Forms.TabPage();
      this.clcSolid = new OgamaControls.ColorSelectControl();
      this.tbpTexture = new System.Windows.Forms.TabPage();
      this.txbImageFile = new System.Windows.Forms.TextBox();
      this.label1 = new System.Windows.Forms.Label();
      this.cbbWrapMode = new System.Windows.Forms.ComboBox();
      this.btnImageFile = new System.Windows.Forms.Button();
      this.tbpHatch = new System.Windows.Forms.TabPage();
      this.clbBackground = new OgamaControls.ColorButton(this.components);
      this.clbForeground = new OgamaControls.ColorButton(this.components);
      this.label2 = new System.Windows.Forms.Label();
      this.cbbHatchStyle = new System.Windows.Forms.ComboBox();
      this.bsaPreview = new OgamaControls.BrushStyleArea();
      this.ofdImage = new System.Windows.Forms.OpenFileDialog();
      this.splitContainer1 = new System.Windows.Forms.SplitContainer();
      this.tbcBrushType.SuspendLayout();
      this.tbpSolid.SuspendLayout();
      this.tbpTexture.SuspendLayout();
      this.tbpHatch.SuspendLayout();
      this.splitContainer1.Panel1.SuspendLayout();
      this.splitContainer1.Panel2.SuspendLayout();
      this.splitContainer1.SuspendLayout();
      this.SuspendLayout();
      // 
      // tbcBrushType
      // 
      this.tbcBrushType.Controls.Add(this.tbpSolid);
      this.tbcBrushType.Controls.Add(this.tbpTexture);
      this.tbcBrushType.Controls.Add(this.tbpHatch);
      this.tbcBrushType.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tbcBrushType.Location = new System.Drawing.Point(0, 0);
      this.tbcBrushType.Name = "tbcBrushType";
      this.tbcBrushType.SelectedIndex = 0;
      this.tbcBrushType.Size = new System.Drawing.Size(317, 162);
      this.tbcBrushType.TabIndex = 6;
      this.tbcBrushType.SelectedIndexChanged += new System.EventHandler(this.tbcBrushType_SelectedIndexChanged);
      // 
      // tbpSolid
      // 
      this.tbpSolid.Controls.Add(this.clcSolid);
      this.tbpSolid.Location = new System.Drawing.Point(4, 22);
      this.tbpSolid.Name = "tbpSolid";
      this.tbpSolid.Padding = new System.Windows.Forms.Padding(3);
      this.tbpSolid.Size = new System.Drawing.Size(309, 136);
      this.tbpSolid.TabIndex = 0;
      this.tbpSolid.Text = "Solid";
      this.tbpSolid.UseVisualStyleBackColor = true;
      // 
      // clcSolid
      // 
      this.clcSolid.AccessibleDescription = "Select a color to use";
      this.clcSolid.AccessibleName = "Select Color";
      this.clcSolid.Dock = System.Windows.Forms.DockStyle.Fill;
      this.clcSolid.Location = new System.Drawing.Point(3, 3);
      this.clcSolid.Name = "clcSolid";
      this.clcSolid.SelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(191)))));
      this.clcSolid.Size = new System.Drawing.Size(303, 130);
      this.clcSolid.TabIndex = 2;
      this.clcSolid.ColorChanged += new System.EventHandler<OgamaControls.ColorChangedEventArgs>(this.clcSolid_ColorChanged);
      // 
      // tbpTexture
      // 
      this.tbpTexture.Controls.Add(this.txbImageFile);
      this.tbpTexture.Controls.Add(this.label1);
      this.tbpTexture.Controls.Add(this.cbbWrapMode);
      this.tbpTexture.Controls.Add(this.btnImageFile);
      this.tbpTexture.Location = new System.Drawing.Point(4, 22);
      this.tbpTexture.Name = "tbpTexture";
      this.tbpTexture.Padding = new System.Windows.Forms.Padding(3);
      this.tbpTexture.Size = new System.Drawing.Size(309, 136);
      this.tbpTexture.TabIndex = 1;
      this.tbpTexture.Text = "Texture";
      this.tbpTexture.UseVisualStyleBackColor = true;
      // 
      // txbImageFile
      // 
      this.txbImageFile.Location = new System.Drawing.Point(40, 11);
      this.txbImageFile.Name = "txbImageFile";
      this.txbImageFile.ReadOnly = true;
      this.txbImageFile.Size = new System.Drawing.Size(161, 20);
      this.txbImageFile.TabIndex = 5;
      this.txbImageFile.TextChanged += new System.EventHandler(this.txbImageFile_TextChanged);
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(6, 37);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(59, 13);
      this.label1.TabIndex = 4;
      this.label1.Text = "wrap mode";
      // 
      // cbbWrapMode
      // 
      this.cbbWrapMode.FormattingEnabled = true;
      this.cbbWrapMode.Location = new System.Drawing.Point(83, 34);
      this.cbbWrapMode.Name = "cbbWrapMode";
      this.cbbWrapMode.Size = new System.Drawing.Size(82, 21);
      this.cbbWrapMode.TabIndex = 3;
      this.cbbWrapMode.SelectionChangeCommitted += new System.EventHandler(this.cbbWrapMode_SelectionChangeCommitted);
      // 
      // btnImageFile
      // 
      this.btnImageFile.Image = global::OgamaControls.Properties.Resources.openHS;
      this.btnImageFile.Location = new System.Drawing.Point(6, 6);
      this.btnImageFile.Name = "btnImageFile";
      this.btnImageFile.Size = new System.Drawing.Size(28, 28);
      this.btnImageFile.TabIndex = 1;
      this.btnImageFile.UseVisualStyleBackColor = true;
      this.btnImageFile.Click += new System.EventHandler(this.btnImageFile_Click);
      // 
      // tbpHatch
      // 
      this.tbpHatch.Controls.Add(this.clbBackground);
      this.tbpHatch.Controls.Add(this.clbForeground);
      this.tbpHatch.Controls.Add(this.label2);
      this.tbpHatch.Controls.Add(this.cbbHatchStyle);
      this.tbpHatch.Location = new System.Drawing.Point(4, 22);
      this.tbpHatch.Name = "tbpHatch";
      this.tbpHatch.Padding = new System.Windows.Forms.Padding(3);
      this.tbpHatch.Size = new System.Drawing.Size(309, 136);
      this.tbpHatch.TabIndex = 2;
      this.tbpHatch.Text = "Hatch";
      this.tbpHatch.UseVisualStyleBackColor = true;
      // 
      // clbBackground
      // 
      this.clbBackground.AutoButtonString = "Automatic";
      this.clbBackground.CurrentColor = System.Drawing.Color.Transparent;
      this.clbBackground.Location = new System.Drawing.Point(95, 33);
      this.clbBackground.Name = "clbBackground";
      this.clbBackground.Size = new System.Drawing.Size(79, 26);
      this.clbBackground.TabIndex = 6;
      this.clbBackground.UseVisualStyleBackColor = true;
      this.clbBackground.ColorChanged += new System.EventHandler(this.clbBackground_ColorChanged);
      // 
      // clbForeground
      // 
      this.clbForeground.AutoButtonString = "Automatic";
      this.clbForeground.CurrentColor = System.Drawing.Color.Transparent;
      this.clbForeground.Location = new System.Drawing.Point(10, 33);
      this.clbForeground.Name = "clbForeground";
      this.clbForeground.Size = new System.Drawing.Size(79, 26);
      this.clbForeground.TabIndex = 5;
      this.clbForeground.UseVisualStyleBackColor = true;
      this.clbForeground.ColorChanged += new System.EventHandler(this.clbForeground_ColorChanged);
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(3, 6);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(60, 13);
      this.label2.TabIndex = 4;
      this.label2.Text = "Hatch style";
      // 
      // cbbHatchStyle
      // 
      this.cbbHatchStyle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cbbHatchStyle.FormattingEnabled = true;
      this.cbbHatchStyle.Location = new System.Drawing.Point(81, 6);
      this.cbbHatchStyle.Name = "cbbHatchStyle";
      this.cbbHatchStyle.Size = new System.Drawing.Size(105, 21);
      this.cbbHatchStyle.TabIndex = 3;
      this.cbbHatchStyle.SelectionChangeCommitted += new System.EventHandler(this.cbbHatchStyle_SelectionChangeCommitted);
      // 
      // bsaPreview
      // 
      this.bsaPreview.BackColor = System.Drawing.Color.WhiteSmoke;
      this.bsaPreview.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.bsaPreview.Dock = System.Windows.Forms.DockStyle.Fill;
      this.bsaPreview.Location = new System.Drawing.Point(0, 0);
      this.bsaPreview.Margin = new System.Windows.Forms.Padding(0);
      this.bsaPreview.Name = "bsaPreview";
      this.bsaPreview.Size = new System.Drawing.Size(317, 70);
      this.bsaPreview.TabIndex = 2;
      // 
      // ofdImage
      // 
      this.ofdImage.Filter = "Images (*.BMP;*.JPG;*.GIF)|*.BMP;*.JPG;*.GIF|All files (*.*)|*.*";
      this.ofdImage.Title = "Please select an image for the texture brush ...";
      // 
      // splitContainer1
      // 
      this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
      this.splitContainer1.Location = new System.Drawing.Point(0, 0);
      this.splitContainer1.Name = "splitContainer1";
      this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
      // 
      // splitContainer1.Panel1
      // 
      this.splitContainer1.Panel1.Controls.Add(this.bsaPreview);
      // 
      // splitContainer1.Panel2
      // 
      this.splitContainer1.Panel2.Controls.Add(this.tbcBrushType);
      this.splitContainer1.Size = new System.Drawing.Size(317, 236);
      this.splitContainer1.SplitterDistance = 70;
      this.splitContainer1.TabIndex = 7;
      // 
      // BrushSelectControl
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.splitContainer1);
      this.Name = "BrushSelectControl";
      this.Size = new System.Drawing.Size(317, 236);
      this.tbcBrushType.ResumeLayout(false);
      this.tbpSolid.ResumeLayout(false);
      this.tbpTexture.ResumeLayout(false);
      this.tbpTexture.PerformLayout();
      this.tbpHatch.ResumeLayout(false);
      this.tbpHatch.PerformLayout();
      this.splitContainer1.Panel1.ResumeLayout(false);
      this.splitContainer1.Panel2.ResumeLayout(false);
      this.splitContainer1.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion

    private BrushStyleArea bsaPreview;
    private System.Windows.Forms.TabControl tbcBrushType;
    private System.Windows.Forms.TabPage tbpSolid;
    private System.Windows.Forms.TabPage tbpTexture;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.ComboBox cbbWrapMode;
    private System.Windows.Forms.Button btnImageFile;
    private ColorButton clbBackground;
    private ColorButton clbForeground;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.ComboBox cbbHatchStyle;
    private System.Windows.Forms.TextBox txbImageFile;
    private System.Windows.Forms.OpenFileDialog ofdImage;
    private System.Windows.Forms.TabPage tbpHatch;
    private ColorSelectControl clcSolid;
    private System.Windows.Forms.SplitContainer splitContainer1;
  }
}
