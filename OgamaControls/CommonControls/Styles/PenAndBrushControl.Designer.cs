namespace OgamaControls
{
  partial class PenAndBrushControl
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
      this.chbDrawEdge = new System.Windows.Forms.CheckBox();
      this.chbDrawFill = new System.Windows.Forms.CheckBox();
      this.btnBrushStyle = new System.Windows.Forms.Button();
      this.btnPenStyle = new System.Windows.Forms.Button();
      this.chbDrawName = new System.Windows.Forms.CheckBox();
      this.txbName = new System.Windows.Forms.TextBox();
      this.btnFont = new System.Windows.Forms.Button();
      this.bsaFillstyle = new OgamaControls.BrushStyleArea();
      this.psaLinestyle = new OgamaControls.PenStyleArea();
      this.SuspendLayout();
      // 
      // chbDrawEdge
      // 
      this.chbDrawEdge.AutoSize = true;
      this.chbDrawEdge.Checked = true;
      this.chbDrawEdge.CheckState = System.Windows.Forms.CheckState.Checked;
      this.chbDrawEdge.Location = new System.Drawing.Point(3, 33);
      this.chbDrawEdge.Name = "chbDrawEdge";
      this.chbDrawEdge.Size = new System.Drawing.Size(78, 17);
      this.chbDrawEdge.TabIndex = 32;
      this.chbDrawEdge.Text = "Draw edge";
      this.chbDrawEdge.UseVisualStyleBackColor = true;
      this.chbDrawEdge.CheckedChanged += new System.EventHandler(this.chbDrawEdge_CheckedChanged);
      // 
      // chbDrawFill
      // 
      this.chbDrawFill.AutoSize = true;
      this.chbDrawFill.Location = new System.Drawing.Point(3, 63);
      this.chbDrawFill.Name = "chbDrawFill";
      this.chbDrawFill.Size = new System.Drawing.Size(72, 17);
      this.chbDrawFill.TabIndex = 31;
      this.chbDrawFill.Text = "Fill interior";
      this.chbDrawFill.UseVisualStyleBackColor = true;
      this.chbDrawFill.CheckedChanged += new System.EventHandler(this.chbDrawFill_CheckedChanged);
      // 
      // btnBrushStyle
      // 
      this.btnBrushStyle.AutoSize = true;
      this.btnBrushStyle.Image = global::OgamaControls.Properties.Resources.RecolorPictureHS;
      this.btnBrushStyle.Location = new System.Drawing.Point(218, 58);
      this.btnBrushStyle.Name = "btnBrushStyle";
      this.btnBrushStyle.Size = new System.Drawing.Size(25, 25);
      this.btnBrushStyle.TabIndex = 29;
      this.btnBrushStyle.UseVisualStyleBackColor = true;
      this.btnBrushStyle.Click += new System.EventHandler(this.btnBrushStyle_Click);
      // 
      // btnPenStyle
      // 
      this.btnPenStyle.AutoSize = true;
      this.btnPenStyle.Image = global::OgamaControls.Properties.Resources.LineColorHS;
      this.btnPenStyle.Location = new System.Drawing.Point(218, 29);
      this.btnPenStyle.Name = "btnPenStyle";
      this.btnPenStyle.Size = new System.Drawing.Size(25, 25);
      this.btnPenStyle.TabIndex = 28;
      this.btnPenStyle.UseVisualStyleBackColor = true;
      this.btnPenStyle.Click += new System.EventHandler(this.btnPenStyle_Click);
      // 
      // chbDrawName
      // 
      this.chbDrawName.AutoSize = true;
      this.chbDrawName.Location = new System.Drawing.Point(3, 5);
      this.chbDrawName.Name = "chbDrawName";
      this.chbDrawName.Size = new System.Drawing.Size(84, 18);
      this.chbDrawName.TabIndex = 33;
      this.chbDrawName.Text = " Draw name";
      this.chbDrawName.UseCompatibleTextRendering = true;
      this.chbDrawName.UseVisualStyleBackColor = true;
      this.chbDrawName.CheckedChanged += new System.EventHandler(this.chbDrawName_CheckedChanged);
      // 
      // txbName
      // 
      this.txbName.Location = new System.Drawing.Point(83, 4);
      this.txbName.Name = "txbName";
      this.txbName.Size = new System.Drawing.Size(127, 20);
      this.txbName.TabIndex = 34;
      this.txbName.TextChanged += new System.EventHandler(this.txbName_TextChanged);
      this.txbName.Enter += new System.EventHandler(this.txbName_Enter);
      // 
      // btnFont
      // 
      this.btnFont.AutoSize = true;
      this.btnFont.Image = global::OgamaControls.Properties.Resources.FontHS;
      this.btnFont.Location = new System.Drawing.Point(218, 0);
      this.btnFont.Name = "btnFont";
      this.btnFont.Size = new System.Drawing.Size(25, 25);
      this.btnFont.TabIndex = 28;
      this.btnFont.UseVisualStyleBackColor = true;
      this.btnFont.Click += new System.EventHandler(this.btnFont_Click);
      // 
      // bsaFillstyle
      // 
      this.bsaFillstyle.BackColor = System.Drawing.Color.WhiteSmoke;
      this.bsaFillstyle.Location = new System.Drawing.Point(83, 58);
      this.bsaFillstyle.Margin = new System.Windows.Forms.Padding(0);
      this.bsaFillstyle.Name = "bsaFillstyle";
      this.bsaFillstyle.Size = new System.Drawing.Size(127, 25);
      this.bsaFillstyle.TabIndex = 30;
      // 
      // psaLinestyle
      // 
      this.psaLinestyle.BackColor = System.Drawing.Color.White;
      this.psaLinestyle.Location = new System.Drawing.Point(83, 29);
      this.psaLinestyle.Name = "psaLinestyle";
      this.psaLinestyle.PenColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
      this.psaLinestyle.PenDashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
      this.psaLinestyle.PenSize = 1F;
      this.psaLinestyle.Size = new System.Drawing.Size(127, 25);
      this.psaLinestyle.TabIndex = 27;
      // 
      // PenAndBrushControl
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.txbName);
      this.Controls.Add(this.chbDrawName);
      this.Controls.Add(this.btnBrushStyle);
      this.Controls.Add(this.btnFont);
      this.Controls.Add(this.btnPenStyle);
      this.Controls.Add(this.chbDrawEdge);
      this.Controls.Add(this.chbDrawFill);
      this.Controls.Add(this.bsaFillstyle);
      this.Controls.Add(this.psaLinestyle);
      this.Name = "PenAndBrushControl";
      this.Size = new System.Drawing.Size(246, 85);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Button btnBrushStyle;
    private System.Windows.Forms.Button btnPenStyle;
    private System.Windows.Forms.CheckBox chbDrawEdge;
    private System.Windows.Forms.CheckBox chbDrawFill;
    private BrushStyleArea bsaFillstyle;
    private PenStyleArea psaLinestyle;
    private System.Windows.Forms.CheckBox chbDrawName;
    private System.Windows.Forms.TextBox txbName;
    private System.Windows.Forms.Button btnFont;
  }
}
