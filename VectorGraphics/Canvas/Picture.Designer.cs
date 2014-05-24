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
  using VectorGraphics.Controls.Timer;

  partial class Picture
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
      this.CustomDispose();

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
      this.tmrForeground = new MultimediaTimer(this.components);
      this.tmrBackground = new System.Windows.Forms.Timer(this.components);
      this.SuspendLayout();
      // 
      // tmrForeground
      // 
      this.tmrForeground.Period = 10;
      this.tmrForeground.SynchronizingObject = this;
      this.tmrForeground.Tick += new System.EventHandler(this.ForegroundTimerTick);
      // 
      // tmrBackground
      // 
      this.tmrBackground.Interval = 500;
      this.tmrBackground.Tick += new System.EventHandler(this.TmrBackgroundTick);
      // 
      // Picture
      // 
      this.BackColor = System.Drawing.Color.Black;
      this.Name = "Picture";
      this.ResumeLayout(false);

    }

    #endregion

    private MultimediaTimer tmrForeground;
    private System.Windows.Forms.Timer tmrBackground;
  }
}
