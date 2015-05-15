// <copyright file="TobiiCalibrationResultPanel.cs" company="FU Berlin">
// ******************************************************
// OGAMA - open gaze and mouse analyzer 
// Copyright (C) 2015 Dr. Adrian Voßkühler  
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

  using Tobii.Eyetracking.Sdk;

  /// <summary>
  /// The tobii calibration result panel.
  /// </summary>
  public partial class TobiiCalibrationResultPanel : Control
  {
    #region Constants and Fields

    /// <summary>
    /// The circle radius.
    /// </summary>
    private const int CircleRadius = 5;

    /// <summary>
    /// The padding ratio.
    /// </summary>
    private const float PaddingRatio = 0.07F;

    /// <summary>
    /// The _calibration points.
    /// </summary>
    private readonly List<PointF> calibrationPoints;

    /// <summary>
    /// The _calibration data.
    /// </summary>
    private List<CalibrationPlotItem> calibrationData;

    #endregion

    #region Constructors and Destructors

    /// <summary>
    /// Initializes a new instance of the <see cref="TobiiCalibrationResultPanel"/> class.
    /// </summary>
    public TobiiCalibrationResultPanel()
    {
      this.InitializeComponent();

      this.calibrationPoints = new List<PointF>();
    }

    #endregion

    #region Public Methods

    /// <summary>
    /// The initialize.
    /// </summary>
    /// <param name="newCalibrationData">
    /// The calibration data.
    /// </param>
    public void Initialize(List<CalibrationPlotItem> newCalibrationData)
    {
      this.calibrationData = newCalibrationData;
      this.ExtractCalibrationPoints();

      this.Invalidate();
    }

    #endregion

    #region Methods

    /// <summary>
    /// The on paint.
    /// </summary>
    /// <param name="pe">
    /// The pe.
    /// </param>
    protected override void OnPaint(PaintEventArgs pe)
    {
      base.OnPaint(pe);

      if (this.calibrationData != null && this.calibrationPoints != null)
      {
        using (var pen = new Pen(Color.DarkGray))
        {
          // Draw calibration points
          pen.Color = Color.DarkGray;
          foreach (PointF calibrationPoint in this.calibrationPoints)
          {
            Rectangle r = this.GetCalibrationCircleBounds(calibrationPoint, CircleRadius);
            pe.Graphics.DrawEllipse(pen, r);
          }

          // Draw bounds
          pen.Color = Color.LightGray;
          Rectangle canvasBounds = this.GetCanvasBounds();
          pe.Graphics.DrawRectangle(pen, canvasBounds);

          // Draw errors
          foreach (var plotItem in this.calibrationData)
          {
            if (plotItem.ValidityLeft == 1)
            {
              pen.Color = Color.Red;

              Point p1 = this.PixelPointFromNormalizedPoint(new PointF(plotItem.TrueX, plotItem.TrueY));
              Point p2 = this.PixelPointFromNormalizedPoint(new PointF(plotItem.MapLeftX, plotItem.MapLeftY));

              pe.Graphics.DrawLine(pen, p1, p2);
            }

            if (plotItem.ValidityRight == 1)
            {
              pen.Color = Color.Lime;

              Point p1 = this.PixelPointFromNormalizedPoint(new PointF(plotItem.TrueX, plotItem.TrueY));
              Point p2 = this.PixelPointFromNormalizedPoint(new PointF(plotItem.MapRightX, plotItem.MapRightY));

              pe.Graphics.DrawLine(pen, p1, p2);
            }
          }
        }
      }
    }

    /// <summary>
    /// The extract calibration points.
    /// </summary>
    private void ExtractCalibrationPoints()
    {
      this.calibrationPoints.Clear();

      foreach (var plotItem in this.calibrationData)
      {
        var p = new PointF(plotItem.TrueX, plotItem.TrueY);

        if (!this.calibrationPoints.Contains(p))
        {
          this.calibrationPoints.Add(p);
        }
      }
    }

    /// <summary>
    /// The get calibration circle bounds.
    /// </summary>
    /// <param name="center">
    /// The center.
    /// </param>
    /// <param name="radius">
    /// The radius.
    /// </param>
    /// <returns>A <see cref="Rectangle"/> with the bounds of the calibration circle.</returns>
    private Rectangle GetCalibrationCircleBounds(PointF center, int radius)
    {
      Point pixelCenter = this.PixelPointFromNormalizedPoint(center);
      int d = 2 * radius;

      return new Rectangle(pixelCenter.X - radius, pixelCenter.Y - radius, d, d);
    }

    /// <summary>
    /// The get canvas bounds.
    /// </summary>
    /// <returns>A <see cref="Rectangle"/> with the bounds of the canvas.</returns>
    private Rectangle GetCanvasBounds()
    {
      Point upperLeft = this.PixelPointFromNormalizedPoint(new PointF(0F, 0F));
      Point lowerRight = this.PixelPointFromNormalizedPoint(new PointF(1F, 1F));

      var bounds = new Rectangle
        { 
          Location = upperLeft, 
          Width = lowerRight.X - upperLeft.X, 
          Height = lowerRight.Y - upperLeft.Y 
        };

      return bounds;
    }

    /// <summary>
    /// The pixel point from normalized point.
    /// </summary>
    /// <param name="normalizedPoint">
    /// The normalized point.
    /// </param>
    /// <returns>Returns the normalized point in pixel coordinates.</returns>
    private Point PixelPointFromNormalizedPoint(PointF normalizedPoint)
    {
      var xPadding = (int)(PaddingRatio * this.Width);
      var yPadding = (int)(PaddingRatio * this.Height);

      int canvasWidth = this.Width - 2 * xPadding;
      int canvasHeight = this.Height - 2 * yPadding;

      var pixelPoint = new Point
        {
          X = xPadding + (int)(normalizedPoint.X * canvasWidth),
          Y = yPadding + (int)(normalizedPoint.Y * canvasHeight)
        };

      return pixelPoint;
    }

    #endregion
  }
}