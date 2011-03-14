namespace OgamaControls
{
  partial class PositionButton
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
      this.SuspendLayout();
      // 
      // PositionButton
      // 
      this.MinimumSize = new System.Drawing.Size(80, 22);
      this.Size = new System.Drawing.Size(80, 22);
      this.TextAlign = System.Drawing.ContentAlignment.TopLeft;
      this.Paint += new System.Windows.Forms.PaintEventHandler(this.PositionButton_Paint);
      this.Click += new System.EventHandler(this.PositionButton_Click);
      this.ResumeLayout(false);

    }

    #endregion
  }
}
