namespace OgamaControls
{
  partial class Webcam
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
      if (this.dxCapture != null)
      {
        this.dxCapture.Dispose();
      }

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
      this.cmu = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.cmuProperties = new System.Windows.Forms.ToolStripMenuItem();
      this.cmu.SuspendLayout();
      this.SuspendLayout();
      // 
      // cmu
      // 
      this.cmu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmuProperties});
      this.cmu.Name = "cmu";
      this.cmu.Size = new System.Drawing.Size(140, 26);
      // 
      // cmuProperties
      // 
      this.cmuProperties.Name = "cmuProperties";
      this.cmuProperties.Size = new System.Drawing.Size(139, 22);
      this.cmuProperties.Text = "Properties ...";
      this.cmuProperties.Click += new System.EventHandler(this.cmuProperties_Click);
      // 
      // WebCam2
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ContextMenuStrip = this.cmu;
      this.Name = "WebCam2";
      this.Load += new System.EventHandler(this.WebCam2_Load);
      this.cmu.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.ContextMenuStrip cmu;
    private System.Windows.Forms.ToolStripMenuItem cmuProperties;
  }
}
