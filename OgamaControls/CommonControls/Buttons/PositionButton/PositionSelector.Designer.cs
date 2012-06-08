namespace OgamaControls
{
  /// <summary>
  /// 
  /// </summary>
  partial class PositionSelector
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
      this.cursorText = new System.Windows.Forms.Label();
      this.SuspendLayout();
      // 
      // cursorText
      // 
      this.cursorText.BackColor = System.Drawing.Color.Transparent;
      this.cursorText.Location = new System.Drawing.Point(72, 21);
      this.cursorText.Margin = new System.Windows.Forms.Padding(0);
      this.cursorText.Name = "cursorText";
      this.cursorText.Size = new System.Drawing.Size(56, 14);
      this.cursorText.TabIndex = 1;
      this.cursorText.Text = "Instruction";
      this.cursorText.Visible = false;
      // 
      // PositionSelector
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.BackColor = System.Drawing.SystemColors.ControlLight;
      this.ClientSize = new System.Drawing.Size(160, 120);
      this.ControlBox = false;
      this.Controls.Add(this.cursorText);
      this.DoubleBuffered = true;
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "PositionSelector";
      this.Opacity = 0.6;
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.TopMost = true;
      this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.PositionSelector_MouseUp);
      this.Paint += new System.Windows.Forms.PaintEventHandler(this.PositionSelector_Paint);
      this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PositionSelector_MouseDown);
      this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.PositionSelector_MouseMove);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Label cursorText;


  }
}