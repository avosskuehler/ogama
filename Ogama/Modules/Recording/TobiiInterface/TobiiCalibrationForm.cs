// <copyright file="TobiiCalibrationForm.cs" company="FU Berlin">
// ******************************************************
// OGAMA - open gaze and mouse analyzer 
// Copyright (C) 2012 Adrian Voßkühler  
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
  using System;
  using System.Drawing;
  using System.Drawing.Drawing2D;
  using System.Windows.Forms;

  using Ogama.Modules.Common;
  using Ogama.Modules.Common.Tools;

  using Tobii.Eyetracking.Sdk;

  /// <summary>
  /// The calibration form.
  /// </summary>
  public partial class TobiiCalibrationForm : Form
  {
    #region Constants and Fields

    /// <summary>
    /// The pixel radius.
    /// </summary>
    private const int PixelRadius = 22;

    #endregion

    #region Constructors and Destructors

    /// <summary>
    /// Initializes a new instance of the <see cref="TobiiCalibrationForm"/> class.
    /// </summary>
    public TobiiCalibrationForm()
    {
      this.InitializeComponent();
    }

    #endregion

    #region Public Properties

    /// <summary>
    /// Gets or sets CalibrationPoint.
    /// </summary>
    public Point2D CalibrationPoint { get; set; }

    /// <summary>
    /// Gets or sets PointColor.
    /// </summary>
    public Color PointColor { get; set; }

    #endregion

    #region Public Methods

    /// <summary>
    /// The clear calibration point.
    /// </summary>
    public void ClearCalibrationPoint()
    {
      this.PointColor = Color.Transparent;
      this.Invalidate();
    }

    /// <summary>
    /// The draw calibration point.
    /// </summary>
    /// <param name="point">
    /// The point.
    /// </param>
    /// <param name="color">
    /// The color.
    /// </param>
    public void DrawCalibrationPoint(Point2D point, Color color)
    {
      this.CalibrationPoint = point;
      this.PointColor = color;
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

      // Draw calibration circle
      var monitorSize = PresentationScreen.GetPresentationResolution();
      var circleBounds = new Rectangle
        {
          X = (int)(monitorSize.Width * this.CalibrationPoint.X - PixelRadius),
          Y = (int)(monitorSize.Height * this.CalibrationPoint.Y - PixelRadius),
          Width = 2 * PixelRadius,
          Height = 2 * PixelRadius
        };

      var smallCircleBounds = new Rectangle
        {
          X = (int)(monitorSize.Width * this.CalibrationPoint.X - 1),
          Y = (int)(monitorSize.Height * this.CalibrationPoint.Y - 1),
          Width = 2,
          Height = 2
        };

      using (var brush = new SolidBrush(this.PointColor))
      {
        e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
        Console.WriteLine("Drawing circle " + circleBounds + " color " + this.PointColor);
        e.Graphics.FillEllipse(brush, circleBounds);

        brush.Color = this.PointColor == Color.Transparent ? Color.Transparent : Color.Black;
        e.Graphics.FillEllipse(brush, smallCircleBounds);
      }
    }

    #endregion
  }
}