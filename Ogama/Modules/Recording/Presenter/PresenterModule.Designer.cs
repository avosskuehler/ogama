namespace Ogama.Modules.Recording.Presenter
{
  partial class PresenterModule
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
      this.panelOne = new OgamaControls.BufferedGraphicsRenderPanel();
      this.panelTwo = new OgamaControls.BufferedGraphicsRenderPanel();
      this.SuspendLayout();
      // 
      // panelOne
      // 
      this.panelOne.BackColor = System.Drawing.Color.Gold;
      this.panelOne.Dock = System.Windows.Forms.DockStyle.Fill;
      this.panelOne.Location = new System.Drawing.Point(0, 0);
      this.panelOne.Name = "panelOne";
      this.panelOne.Size = new System.Drawing.Size(640, 480);
      this.panelOne.TabIndex = 0;
      this.panelOne.MouseDown += new System.Windows.Forms.MouseEventHandler(this.FrmPresenterMouseDown);
      this.panelOne.MouseUp += new System.Windows.Forms.MouseEventHandler(this.FrmPresenterMouseUp);
      // 
      // panelTwo
      // 
      this.panelTwo.BackColor = System.Drawing.Color.Green;
      this.panelTwo.Dock = System.Windows.Forms.DockStyle.Fill;
      this.panelTwo.Location = new System.Drawing.Point(0, 0);
      this.panelTwo.Name = "panelTwo";
      this.panelTwo.Size = new System.Drawing.Size(640, 480);
      this.panelTwo.TabIndex = 0;
      this.panelTwo.MouseDown += new System.Windows.Forms.MouseEventHandler(this.FrmPresenterMouseDown);
      this.panelTwo.MouseUp += new System.Windows.Forms.MouseEventHandler(this.FrmPresenterMouseUp);
      // 
      // PresenterModule
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
      this.ClientSize = new System.Drawing.Size(640, 480);
      this.Controls.Add(this.panelOne);
      this.Controls.Add(this.panelTwo);
      this.DoubleBuffered = true;
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
      this.Name = "PresenterModule";
      this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
      this.Text = "frmPresenter";
      this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmPresenterFormClosing);
      this.Load += new System.EventHandler(this.PresenterModuleLoad);
      this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmPresenterKeyDown);
      this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.FrmPresenterMouseDown);
      this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.FrmPresenterMouseUp);
      this.ResumeLayout(false);

    }

    #endregion

    private OgamaControls.BufferedGraphicsRenderPanel panelOne;
    private OgamaControls.BufferedGraphicsRenderPanel panelTwo;

  }
}