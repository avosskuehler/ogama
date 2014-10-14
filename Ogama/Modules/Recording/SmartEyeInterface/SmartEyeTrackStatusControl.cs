// <copyright file="SmartEyeTrackStatusControl.cs" company="Smart Eye AB">
// ******************************************************
// OGAMA - open gaze and mouse analyzer 
// Copyright (C) 2014 Dr. Adrian Voßkühler  
// ------------------------------------------------------------------------
// This program is free software; you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation; either version 2 of the License, or (at your option) any later version.
// This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
// You should have received a copy of the GNU General Public License along with this program; if not, write to the Free Software Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA 02111-1307 USA
// **************************************************************
// </copyright>
// <author>Kathrin Scheil</author>
// <email>kathrin.scheil@smarteye.se</email>

namespace Ogama.Modules.Recording.SmartEyeInterface
{
  using System;
  using System.Collections.Generic;
  using System.Diagnostics;
  using System.Drawing;
  using System.IO;
  using System.Windows.Forms;
  using System.Windows.Media.Imaging;
  using SmartEye.Geometry;

  /// <summary>
  /// The SmartEye track status control.
  /// </summary>
  public partial class SmartEyeTrackStatusControl : UserControl
  {
    ///////////////////////////////////////////////////////////////////////////////
    // Defining Variables, Enumerations, Events                                  //
    ///////////////////////////////////////////////////////////////////////////////
    #region FIELDS

    /// <summary>
    /// The brush.
    /// </summary>
    private readonly SolidBrush brush;

    /// <summary>
    /// The head brush.
    /// </summary>
    private readonly SolidBrush headBrush;

    /// <summary>
    /// The data history.
    /// </summary>
    private SmartEyeGazeData gazeData;

    #endregion FIELDS

    ///////////////////////////////////////////////////////////////////////////////
    // Construction and Initializing methods                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region CONSTRUCTION

    /// <summary>
    /// Initializes a new instance of the <see cref="SmartEyeTrackStatusControl"/> class.
    /// </summary>
    public SmartEyeTrackStatusControl()
    {
      this.InitializeComponent();

      this.SetStyle(ControlStyles.UserPaint, true);
      this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
      this.SetStyle(ControlStyles.DoubleBuffer, true);

      this.gazeData = new SmartEyeGazeData();

      this.brush = new SolidBrush(Color.Transparent);
      this.headBrush = new SolidBrush(Color.Transparent);

      this.liveImagePictureBox.Paint += this.LiveImagePictureBoxOnPaint;
    }

    #endregion CONSTRUCTION

    ///////////////////////////////////////////////////////////////////////////////
    // Methods for doing main class job                                          //
    ///////////////////////////////////////////////////////////////////////////////
    #region METHODS

    /// <summary>
    /// The clear.
    /// </summary>
    public void Clear()
    {
      this.gazeData = new SmartEyeGazeData();

      this.Invalidate();
    }

    ///////////////////////////////////////////////////////////////////////////////
    // Eventhandler                                                              //
    ///////////////////////////////////////////////////////////////////////////////
    #region EVENTS

    /// <summary>
    /// The on gaze data.
    /// </summary>
    /// <param name="gd">
    /// The gaze data.
    /// </param>
    public void OnGazeData(SmartEyeGazeData gd)
    {
      this.gazeData = gd;

      if (gd.GazeQuality != null)
      {
        this.brush.Color = this.ComputeStatusColor(gd.GazeQuality);
        this.headBrush.Color = this.ComputeStatusColor(gd.HeadQuality);
      }

      this.Invalidate();
    }

    /// <summary>
    /// The on live image.
    /// </summary>
    /// <param name="bmp">
    /// The image data.
    /// </param>
    public void OnLiveImage(Bitmap bmp)
    {
      liveImagePictureBox.Image = bmp;

      this.Invalidate();
    }

    /// <summary>
    /// The additional on paint of the live image picture box to get dimensions right.
    /// </summary>
    /// <param name="sender">The sender</param>
    /// <param name="e">The event</param>
    private void LiveImagePictureBoxOnPaint(object sender, PaintEventArgs e)
    {
      if (this.gazeData.GazeQuality == null)
      {
        return;
      }

      // Draw frame - TODO: positioned to DR120 setup, should be fitted to Aurora
      var left = this.Left + this.Width / 6;
      var width = this.Width / 2;
      var top = this.Top + this.Height / 20;
      var height = (int)Math.Floor(this.Height * 0.8);

      if (this.liveImagePictureBox.Image != null)
      {
        // image and container dimensions
        int w_i = this.liveImagePictureBox.Image.Width;
        int h_i = this.liveImagePictureBox.Image.Height;
        int w_c = this.liveImagePictureBox.Width;
        int h_c = this.liveImagePictureBox.Height;

        float imageRatio = w_i / (float)h_i; // image W:H ratio
        float containerRatio = w_c / (float)h_c; // container W:H ratio

        if (imageRatio >= containerRatio)
        {
          // horizontal image
          float scaleFactor = w_c / (float)w_i;
          float scaledHeight = h_i * scaleFactor;

          // calculate gap between top of container and top of image
          float filler = Math.Abs(h_c - scaledHeight) / 2;

          top = (int)Math.Floor(this.Top + (this.Height - filler * 2) / 20 + filler);
          height = (int)Math.Floor((this.Height - filler * 2) * 0.8);
        }
        else
        {
          // vertical image
          float scaleFactor = h_c / (float)h_i;
          float scaledWidth = w_i * scaleFactor;
          float filler = Math.Abs(w_c - scaledWidth) / 2;

          left = (int)Math.Floor(this.Left + (this.Width - filler * 2) / 6 + filler);
          width = (int)Math.Floor((this.Width - filler * 2) / 2);
        }
      }

      e.Graphics.DrawRectangle(new Pen(this.headBrush), left, top, width, height);

      // Draw gaze quality indicator
      e.Graphics.FillEllipse(this.brush, this.Left + 5, this.Top + 5, 12, 12);
    }

    #endregion EVENTS

    ///////////////////////////////////////////////////////////////////////////////
    // Small helping Methods                                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region HELPER

    /// <summary>
    /// This method computes the status color.
    /// </summary>
    /// <param name="quality">The tracking quality</param>
    /// <returns>The <see cref="Color"/> indicating the tracking quality.</returns>
    private Color ComputeStatusColor(double? quality)
    {
      if (!this.Enabled)
      {
        return Color.Gray;
      }

      if (quality > 0.2)
      {
        return Color.Lime;
      }
      else
      {
        return Color.Red;
      }
    }

    #endregion HELPER
    #endregion METHODS
  }
}