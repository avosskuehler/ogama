﻿namespace Ogama.Modules.Recording.Presenter
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
      // panel1
      // 
      this.panelOne.BackColor = System.Drawing.Color.Gold;
      this.panelOne.Dock = System.Windows.Forms.DockStyle.Fill;
      this.panelOne.Location = new System.Drawing.Point(0, 0);
      this.panelOne.Name = "panel1";
      this.panelOne.Size = new System.Drawing.Size(640, 480);
      this.panelOne.TabIndex = 0;
      this.panelOne.MouseDown += new System.Windows.Forms.MouseEventHandler(this.frmPresenter_MouseDown);
      this.panelOne.MouseUp += new System.Windows.Forms.MouseEventHandler(this.frmPresenter_MouseUp);
      // 
      // panel2
      // 
      this.panelTwo.BackColor = System.Drawing.Color.Green;
      this.panelTwo.Dock = System.Windows.Forms.DockStyle.Fill;
      this.panelTwo.Location = new System.Drawing.Point(0, 0);
      this.panelTwo.Name = "panel2";
      this.panelTwo.Size = new System.Drawing.Size(640, 480);
      this.panelTwo.TabIndex = 0;
      this.panelTwo.MouseDown += new System.Windows.Forms.MouseEventHandler(this.frmPresenter_MouseDown);
      this.panelTwo.MouseUp += new System.Windows.Forms.MouseEventHandler(this.frmPresenter_MouseUp);
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
      this.Load += new System.EventHandler(this.PresenterModule_Load);
      this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.frmPresenter_MouseUp);
      this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.frmPresenter_MouseDown);
      this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmPresenter_FormClosing);
      this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmPresenter_KeyDown);
      this.ResumeLayout(false);

    }

    #endregion

    private OgamaControls.BufferedGraphicsRenderPanel panelOne;
    private OgamaControls.BufferedGraphicsRenderPanel panelTwo;

  }
}