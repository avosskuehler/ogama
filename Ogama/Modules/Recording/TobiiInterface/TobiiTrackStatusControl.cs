// <copyright file="TobiiTrackStatusControl.cs" company="FU Berlin">
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

namespace Ogama.Modules.Recording.TobiiInterface
{
  using System.Collections.Generic;
  using System.Drawing;
  using System.Windows.Forms;

  using global::Tobii.Eyetracking.Sdk;

  /// <summary>
  /// The tobii track status control.
  /// </summary>
  public partial class TobiiTrackStatusControl : UserControl
  {
    #region Constants and Fields

    /// <summary>
    /// The brush.
    /// </summary>
    private readonly SolidBrush brush;

    /// <summary>
    /// The data history.
    /// </summary>
    private readonly Queue<GazeDataItem> dataHistory;

    /// <summary>
    /// The eye brush.
    /// </summary>
    private readonly SolidBrush eyeBrush;

    /// <summary>
    /// The bar height.
    /// </summary>
    private const int BarHeight = 25;

    /// <summary>
    /// The eye radius.
    /// </summary>
    private const int EyeRadius = 8;

    /// <summary>
    /// The history size.
    /// </summary>
    private const int HistorySize = 30;

    /// <summary>
    /// The left eye <see cref="Point3D"/>
    /// </summary>
    private Point3D leftEye;

    /// <summary>
    /// The left eyes validity.
    /// </summary>
    private int leftValidity;

    /// <summary>
    /// The right eye <see cref="Point3D"/>
    /// </summary>
    private Point3D rightEye;

    /// <summary>
    /// The right eyes validity.
    /// </summary>
    private int rightValidity;

    #endregion

    #region Constructors and Destructors

    /// <summary>
    /// Initializes a new instance of the <see cref="TobiiTrackStatusControl"/> class.
    /// </summary>
    public TobiiTrackStatusControl()
    {
      this.InitializeComponent();

      this.SetStyle(ControlStyles.UserPaint, true);
      this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
      this.SetStyle(ControlStyles.DoubleBuffer, true);

      this.dataHistory = new Queue<GazeDataItem>(HistorySize);

      this.brush = new SolidBrush(Color.Red);
      this.eyeBrush = new SolidBrush(Color.White);

      this.leftValidity = 4;
      this.rightValidity = 4;
    }

    #endregion

    #region Properties

    /// <summary>
    ///   Gets the current brush
    /// </summary>
    private SolidBrush Brush
    {
      get
      {
        if (this.leftValidity == 4 && this.rightValidity == 4)
        {
          this.brush.Color = Color.Red;
        }
        else if (this.leftValidity == 0 && this.rightValidity == 0)
        {
          this.brush.Color = Color.Lime;
        }
        else if (this.leftValidity == 2 && this.rightValidity == 2)
        {
          this.brush.Color = Color.Orange;
        }
        else
        {
          this.brush.Color = Color.Yellow;
        }

        return this.brush;
      }
    }

    #endregion

    #region Public Methods

    /// <summary>
    /// The clear.
    /// </summary>
    public void Clear()
    {
      this.dataHistory.Clear();
      this.leftValidity = 0;
      this.rightValidity = 0;
      this.leftEye = new Point3D();
      this.rightEye = new Point3D();

      this.Invalidate();
    }

    /// <summary>
    /// The on gaze data.
    /// </summary>
    /// <param name="gd">
    /// The gd.
    /// </param>
    public void OnGazeData(GazeDataItem gd)
    {
      // Add data to history
      this.dataHistory.Enqueue(gd);

      // Remove history item if necessary
      while (this.dataHistory.Count > HistorySize)
      {
        this.dataHistory.Dequeue();
      }

      this.leftValidity = gd.LeftValidity;
      this.rightValidity = gd.RightValidity;

      this.leftEye = gd.LeftEyePosition3DRelative;
      this.rightEye = gd.RightEyePosition3DRelative;

      this.Invalidate();
    }

    #endregion

    #region Methods

    /// <summary>
    /// The on paint.
    /// </summary>
    /// <param name="e">
    /// The e.
    /// </param>
    protected override void OnPaint(PaintEventArgs e)
    {
      base.OnPaint(e);

      // Compute status bar color
      this.brush.Color = this.ComputeStatusColor();

      // Draw bottom bar
      e.Graphics.FillRectangle(this.brush, new Rectangle(0, this.Height - BarHeight, this.Width, BarHeight));

      // Draw eyes
      if (this.leftValidity <= 2)
      {
        var r = new RectangleF(
          (float)((1.0 - this.leftEye.X) * this.Width - EyeRadius),
          (float)(this.leftEye.Y * this.Height - EyeRadius),
          2 * EyeRadius,
          2 * EyeRadius);
        e.Graphics.FillEllipse(this.eyeBrush, r);
      }

      if (this.rightValidity <= 2)
      {
        var r = new RectangleF(
          (float)((1 - this.rightEye.X) * this.Width - EyeRadius),
          (float)(this.rightEye.Y * this.Height - EyeRadius),
          2 * EyeRadius,
          2 * EyeRadius);
        e.Graphics.FillEllipse(this.eyeBrush, r);
      }
    }

    /// <summary>
    /// This method computes the status color.
    /// </summary>
    /// <returns>The <see cref="Color"/> indicating the tracking quality.</returns>
    private Color ComputeStatusColor()
    {
      if (!this.Enabled)
      {
        return Color.Gray;
      }

      int quality = 0;
      int count = 0;

      foreach (GazeDataItem item in this.dataHistory)
      {
        if (item.LeftValidity == 4 && item.RightValidity == 4)
        {
          quality += 0;
        }
        else if (item.LeftValidity == 0 && item.RightValidity == 0)
        {
          quality += 2;
        }
        else
        {
          quality++;
        }

        count++;
      }

      float q = count == 0 ? 0 : quality / (2F * count);

      if (q > 0.8)
      {
        return Color.Lime;
      }

      if (q < 0.1)
      {
        return Color.Red;
      }

      return Color.Red;
    }

    #endregion
  }
}