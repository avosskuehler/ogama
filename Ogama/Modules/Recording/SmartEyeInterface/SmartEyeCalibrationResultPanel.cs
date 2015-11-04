// <copyright file="SmartEyeCalibrationResultPanel.cs" company="Smart Eye AB">
// ******************************************************
// OGAMA - open gaze and mouse analyzer 
// Copyright (C) 2015 Dr. Adrian Voßkühler  
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
  using System.Collections.Generic;
  using System.Drawing;
  using System.Windows.Forms;

  /// <summary>
  /// The SmartEye calibration result panel.
  /// </summary>
  public partial class SmartEyeCalibrationResultPanel : Control
  {
    ///////////////////////////////////////////////////////////////////////////////
    // Defining Variables, Enumerations, Events                                  //
    ///////////////////////////////////////////////////////////////////////////////
    #region FIELDS

    /// <summary>
    /// The circle radius.
    /// </summary>
    private const int CircleRadius = 5;

    /// <summary>
    /// The sample circle radius.
    /// </summary>
    private const int SampleCircleRadius = 1;

    /// <summary>
    /// The padding ratio.
    /// </summary>
    private const float PaddingRatio = 0.07F;

    /// <summary>
    /// The calibration points.
    /// </summary>
    private readonly List<PointF> calibrationPoints;

    /// <summary>
    /// The ratio for displaying the calibration samples.
    /// </summary>
    private double degreeToPixelRatio = 40.0;

    /// <summary>
    /// The calibration data.
    /// </summary>
    private List<CalibrationResult> calibrationData;

    #endregion FIELDS

    ///////////////////////////////////////////////////////////////////////////////
    // Construction and Initializing methods                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region CONSTRUCTION

    /// <summary>
    /// Initializes a new instance of the <see cref="SmartEyeCalibrationResultPanel"/> class.
    /// </summary>
    public SmartEyeCalibrationResultPanel()
    {
      this.InitializeComponent();

      this.calibrationPoints = new List<PointF>();

      this.degreeToPixelRatio = 2;
    }

    #endregion CONSTRUCTION

    ///////////////////////////////////////////////////////////////////////////////
    // Methods for doing main class job                                          //
    ///////////////////////////////////////////////////////////////////////////////
    #region METHODS

    /// <summary>
    /// The initialize.
    /// </summary>
    /// <param name="newCalibrationData">
    /// The calibration data.
    /// </param>
    public void Initialize(List<CalibrationResult> newCalibrationData)
    {
      this.calibrationData = newCalibrationData;
      this.ExtractCalibrationPoints();

      this.Invalidate();
    }

    /// <summary>
    /// The initialize.
    /// </summary>
    /// <param name="newCalibrationData">
    /// The calibration data.
    /// </param>
    /// <param name="degreeToPixelFactor">
    /// The degree to pixel factor for the left and right samples.
    /// </param>
    public void Initialize(List<CalibrationResult> newCalibrationData, double degreeToPixelFactor)
    {
      this.calibrationData = newCalibrationData;
      this.ExtractCalibrationPoints();

      this.degreeToPixelRatio = degreeToPixelFactor;

      this.Invalidate();
    }

    ///////////////////////////////////////////////////////////////////////////////
    // Inherited methods                                                         //
    ///////////////////////////////////////////////////////////////////////////////
    #region OVERRIDES

    /// <summary>
    /// The on paint.
    /// </summary>
    /// <param name="pe">
    /// The paint event.
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
          foreach (CalibrationResult result in this.calibrationData)
          {
            foreach (var leftSample in result.SamplesLeft)
            {
              pen.Color = Color.Green;

              PointF p = this.PixelPointFromNormalizedPoint(new PointF((float)result.Target.X, (float)result.Target.Y));
              p.X += (float)(leftSample.X * this.degreeToPixelRatio);
              p.Y += (float)(leftSample.Y * this.degreeToPixelRatio);
              Rectangle r = new Rectangle((int)p.X - SampleCircleRadius, (int)p.Y - SampleCircleRadius, SampleCircleRadius * 2, SampleCircleRadius * 2);

              pe.Graphics.DrawEllipse(pen, r);
            }

            foreach (var rightSample in result.SamplesRight)
            {
              pen.Color = Color.Blue;

              PointF p = this.PixelPointFromNormalizedPoint(new PointF((float)result.Target.X, (float)result.Target.Y));
              p.X += (float)(rightSample.X * this.degreeToPixelRatio);
              p.Y += (float)(rightSample.Y * this.degreeToPixelRatio);
              Rectangle r = new Rectangle((int)p.X - SampleCircleRadius, (int)p.Y - SampleCircleRadius, SampleCircleRadius * 2, SampleCircleRadius * 2);

              pe.Graphics.DrawEllipse(pen, r);
            }
          }
        }
      }
    }

    #endregion OVERRIDES

    ///////////////////////////////////////////////////////////////////////////////
    // Small helping Methods                                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region HELPER

    /// <summary>
    /// The extract calibration points.
    /// </summary>
    private void ExtractCalibrationPoints()
    {
      this.calibrationPoints.Clear();

      foreach (var result in this.calibrationData)
      {
        var p = new PointF((float)result.Target.X, (float)result.Target.Y);

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

    #endregion HELPER
    #endregion METHODS
  }
}