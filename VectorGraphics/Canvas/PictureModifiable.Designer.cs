// <copyright file="CaptureMode.cs" company="FU Berlin">
// ******************************************************
// OGAMA - open gaze and mouse analyzer 
// Copyright (C) 2013 Dr. Adrian Voßkühler  
// ------------------------------------------------------------------------
// This program is free software; you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation; either version 2 of the License, or (at your option) any later version.
// This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
// You should have received a copy of the GNU General Public License along with this program; if not, write to the Free Software Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA 02111-1307 USA
// **************************************************************
// </copyright>
// <author>Adrian Voßkühler</author>
// <email>adrian@ogama.net</email>

namespace VectorGraphics.Canvas
{
  partial class PictureModifiable
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
      this.cmuPicture = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.cmuToFront = new System.Windows.Forms.ToolStripMenuItem();
      this.cmuForward = new System.Windows.Forms.ToolStripMenuItem();
      this.cmuBackward = new System.Windows.Forms.ToolStripMenuItem();
      this.cmuToBack = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
      this.cmuDelete = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
      this.cmuAlignCenter = new System.Windows.Forms.ToolStripMenuItem();
      this.cmuAlignLeft = new System.Windows.Forms.ToolStripMenuItem();
      this.cmuAlignRight = new System.Windows.Forms.ToolStripMenuItem();
      this.cmuAlignTop = new System.Windows.Forms.ToolStripMenuItem();
      this.cmuAlignBottom = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
      this.cmuSizeFullScreen = new System.Windows.Forms.ToolStripMenuItem();
      this.cmuPicture.SuspendLayout();
      this.SuspendLayout();
      // 
      // cmuPicture
      // 
      this.cmuPicture.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmuToFront,
            this.cmuForward,
            this.cmuBackward,
            this.cmuToBack,
            this.toolStripSeparator1,
            this.cmuDelete,
            this.toolStripSeparator2,
            this.cmuAlignCenter,
            this.cmuAlignLeft,
            this.cmuAlignRight,
            this.cmuAlignTop,
            this.cmuAlignBottom,
            this.toolStripSeparator3,
            this.cmuSizeFullScreen});
      this.cmuPicture.Name = "cmuPicture";
      this.cmuPicture.Size = new System.Drawing.Size(203, 286);
      // 
      // cmuToFront
      // 
      this.cmuToFront.Name = "cmuToFront";
      this.cmuToFront.Size = new System.Drawing.Size(202, 22);
      this.cmuToFront.Text = "Bring to front    Pg-Up";
      this.cmuToFront.Click += new System.EventHandler(this.cmuToFront_Click);
      // 
      // cmuForward
      // 
      this.cmuForward.Name = "cmuForward";
      this.cmuForward.Size = new System.Drawing.Size(202, 22);
      this.cmuForward.Text = "Bring forward    +";
      this.cmuForward.Click += new System.EventHandler(this.cmuForward_Click);
      // 
      // cmuBackward
      // 
      this.cmuBackward.Name = "cmuBackward";
      this.cmuBackward.Size = new System.Drawing.Size(202, 22);
      this.cmuBackward.Text = "Send backward     -";
      this.cmuBackward.Click += new System.EventHandler(this.cmuBackward_Click);
      // 
      // cmuToBack
      // 
      this.cmuToBack.Name = "cmuToBack";
      this.cmuToBack.Size = new System.Drawing.Size(202, 22);
      this.cmuToBack.Text = "Send to back    Pg-Down";
      this.cmuToBack.Click += new System.EventHandler(this.cmuToBack_Click);
      // 
      // toolStripSeparator1
      // 
      this.toolStripSeparator1.Name = "toolStripSeparator1";
      this.toolStripSeparator1.Size = new System.Drawing.Size(199, 6);
      // 
      // cmuDelete
      // 
      this.cmuDelete.Name = "cmuDelete";
      this.cmuDelete.ShortcutKeys = System.Windows.Forms.Keys.Delete;
      this.cmuDelete.Size = new System.Drawing.Size(202, 22);
      this.cmuDelete.Text = "Delete";
      this.cmuDelete.Click += new System.EventHandler(this.cmuDelete_Click);
      // 
      // toolStripSeparator2
      // 
      this.toolStripSeparator2.Name = "toolStripSeparator2";
      this.toolStripSeparator2.Size = new System.Drawing.Size(199, 6);
      // 
      // cmuAlignCenter
      // 
      this.cmuAlignCenter.Name = "cmuAlignCenter";
      this.cmuAlignCenter.Size = new System.Drawing.Size(202, 22);
      this.cmuAlignCenter.Text = "Align center";
      this.cmuAlignCenter.Click += new System.EventHandler(this.cmuAlignCenter_Click);
      // 
      // cmuAlignLeft
      // 
      this.cmuAlignLeft.Name = "cmuAlignLeft";
      this.cmuAlignLeft.Size = new System.Drawing.Size(202, 22);
      this.cmuAlignLeft.Text = "Dock left";
      this.cmuAlignLeft.Click += new System.EventHandler(this.cmuAlignLeft_Click);
      // 
      // cmuAlignRight
      // 
      this.cmuAlignRight.Name = "cmuAlignRight";
      this.cmuAlignRight.Size = new System.Drawing.Size(202, 22);
      this.cmuAlignRight.Text = "Dock right";
      this.cmuAlignRight.Click += new System.EventHandler(this.cmuAlignRight_Click);
      // 
      // cmuAlignTop
      // 
      this.cmuAlignTop.Name = "cmuAlignTop";
      this.cmuAlignTop.Size = new System.Drawing.Size(202, 22);
      this.cmuAlignTop.Text = "Dock top";
      this.cmuAlignTop.Click += new System.EventHandler(this.cmuAlignTop_Click);
      // 
      // cmuAlignBottom
      // 
      this.cmuAlignBottom.Name = "cmuAlignBottom";
      this.cmuAlignBottom.Size = new System.Drawing.Size(202, 22);
      this.cmuAlignBottom.Text = "Dock bottom";
      this.cmuAlignBottom.Click += new System.EventHandler(this.cmuAlignBottom_Click);
      // 
      // toolStripSeparator3
      // 
      this.toolStripSeparator3.Name = "toolStripSeparator3";
      this.toolStripSeparator3.Size = new System.Drawing.Size(199, 6);
      // 
      // cmuSizeFullScreen
      // 
      this.cmuSizeFullScreen.Name = "cmuSizeFullScreen";
      this.cmuSizeFullScreen.Size = new System.Drawing.Size(202, 22);
      this.cmuSizeFullScreen.Text = "Size full screen";
      this.cmuSizeFullScreen.Click += new System.EventHandler(this.cmuSizeFullScreen_Click);
      // 
      // PictureModifiable
      // 
      this.BackColor = System.Drawing.Color.Black;
      this.ContextMenuStrip = this.cmuPicture;
      this.Name = "PictureModifiable";
      this.Resize += new System.EventHandler(this.PictureModifiable_Resize);
      this.cmuPicture.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.ContextMenuStrip cmuPicture;
    private System.Windows.Forms.ToolStripMenuItem cmuToFront;
    private System.Windows.Forms.ToolStripMenuItem cmuForward;
    private System.Windows.Forms.ToolStripMenuItem cmuBackward;
    private System.Windows.Forms.ToolStripMenuItem cmuToBack;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
    private System.Windows.Forms.ToolStripMenuItem cmuDelete;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
    private System.Windows.Forms.ToolStripMenuItem cmuAlignLeft;
    private System.Windows.Forms.ToolStripMenuItem cmuAlignRight;
    private System.Windows.Forms.ToolStripMenuItem cmuAlignTop;
    private System.Windows.Forms.ToolStripMenuItem cmuAlignBottom;
    private System.Windows.Forms.ToolStripMenuItem cmuAlignCenter;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
    private System.Windows.Forms.ToolStripMenuItem cmuSizeFullScreen;
  }
}
