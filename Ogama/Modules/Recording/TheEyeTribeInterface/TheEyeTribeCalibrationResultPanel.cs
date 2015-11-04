// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TheEyeTribeCalibrationResultPanel.cs" company="Freie Universität Berlin">
//   OGAMA - open gaze and mouse analyzer 
//   Copyright (C) 2015 Dr. Adrian Voßkühler  
//   Licensed under GPL V3
// </copyright>
// <author>Adrian Voßkühler</author>
// <email>adrian@ogama.net</email>
// <summary>
//   The theEyeTribe calibration result panel.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Ogama.Modules.Recording.TheEyeTribeInterface
{
  using System.Collections.Generic;
  using System.Drawing;
  using System.Windows.Forms;

  using TETCSharpClient.Data;

  /// <summary>
  ///   The theEyeTribe calibration result panel.
  /// </summary>
  public partial class TheEyeTribeCalibrationResultPanel : Control
  {
    #region Constants

    /// <summary>
    ///   The circle radius.
    /// </summary>
    private const int CircleRadius = 8;

    /// <summary>
    ///   The padding ratio.
    /// </summary>
    private const float PaddingRatio = 0.07F;

    /// <summary>
    /// The padding at the bottom of the control to display
    /// the calibration result text
    /// </summary>
    private const int CalibrationTextPadding = 20;

    #endregion

    #region Fields

    /// <summary>
    ///   The calibration points.
    /// </summary>
    private readonly List<PointF> calibrationPoints;

    /// <summary>
    ///   The calibration result.
    /// </summary>
    private CalibrationResult calibrationResult;

    #endregion

    #region Constructors and Destructors

    /// <summary>
    ///   Initializes a new instance of the <see cref="TheEyeTribeCalibrationResultPanel" /> class.
    /// </summary>
    public TheEyeTribeCalibrationResultPanel()
    {
      this.InitializeComponent();
      this.calibrationPoints = new List<PointF>();
    }

    #endregion

    #region Public Methods and Operators

    /// <summary>
    /// This method initializes the control with the given
    /// calibration result.
    /// </summary>
    /// <param name="newCalibrationResult">
    /// The calibration result with information about the calibration 
    /// </param>
    public void Initialize(CalibrationResult newCalibrationResult)
    {
      this.calibrationResult = newCalibrationResult;
      this.ExtractCalibrationPoints();

      this.Invalidate();
    }

    #endregion

    #region Methods

    /// <summary>
    /// Paints the background of the control.
    /// </summary>
    /// <param name="pevent">A <see cref="T:System.Windows.Forms.PaintEventArgs" /> that contains information about the control to paint.</param>
    protected override void OnPaintBackground(PaintEventArgs pevent)
    {
      base.OnPaintBackground(pevent);

      pevent.Graphics.Clear(SystemColors.ControlLightLight);
    }

    /// <summary>
    /// Raises the <see cref="E:Paint" /> event.
    /// Draws the calibration points and the deviation from the 
    /// calibration result.
    /// </summary>
    /// <param name="pe">The <see cref="PaintEventArgs"/> instance 
    /// containing the event data.</param>
    protected override void OnPaint(PaintEventArgs pe)
    {
      base.OnPaint(pe);

      if (this.calibrationResult != null && this.calibrationPoints != null)
      {
        using (var pen = new Pen(Color.DarkGray))
        {
          // Draw errors
          foreach (CalibrationPoint plotItem in this.calibrationResult.Calibpoints)
          {
            // STATE_NO_DATA = 0
            // STATE_RESAMPLE = 1
            // STATE_OK = 2
            if (plotItem.State == 2)
            {
              pen.Color = Color.Green;
              pen.Width = 2;
              Point p1 = this.GetScaledPoint(
                  new PointF((float)plotItem.Coordinates.X, (float)plotItem.Coordinates.Y));
              Point p2 = this.GetScaledPoint(
                  new PointF((float)plotItem.MeanEstimatedCoords.X, (float)plotItem.MeanEstimatedCoords.Y));

              pe.Graphics.DrawLine(pen, p1, p2);

              var r = this.GetCalibrationCircleBounds(new PointF((float)plotItem.Coordinates.X, (float)plotItem.Coordinates.Y), CircleRadius);
              pe.Graphics.FillPie(this.RatingColor(plotItem.Accuracy.Left), r, 90, 180);
              pe.Graphics.FillPie(this.RatingColor(plotItem.Accuracy.Right), r, 270, 180);
            }

            if (plotItem.State == 0 || plotItem.State == 1)
            {
              var r = this.GetCalibrationCircleBounds(new PointF((float)plotItem.Coordinates.X, (float)plotItem.Coordinates.Y), CircleRadius - 1);

              pe.Graphics.FillEllipse(new SolidBrush(Color.Red), r);
            }
          }

          // Draw calibration points
          pen.Color = Color.DarkGray;
          pen.Width = 1;
          foreach (PointF calibrationPoint in this.calibrationPoints)
          {
            Rectangle r = this.GetCalibrationCircleBounds(calibrationPoint, CircleRadius);
            pe.Graphics.DrawEllipse(pen, r);
          }

          // Draw bounds
          pen.Color = Color.LightGray;
          Rectangle canvasBounds = this.GetCanvasBounds();
          pe.Graphics.DrawRectangle(pen, canvasBounds);
        }

        pe.Graphics.DrawString(this.RatingFunction(this.calibrationResult), SystemFonts.StatusFont, Brushes.Black, this.Width / 2 - 45, this.Height - CalibrationTextPadding);
      }
    }

    /// <summary>
    /// Returns a brush with a color that represents the given accuracy.
    /// </summary>
    /// <param name="accuracy">The accuracy to be coded in color.</param>
    /// <returns>A brush with a color that represents the given accuracy.</returns>
    private SolidBrush RatingColor(double accuracy)
    {
      if (accuracy < 0.5)
      {
        return new SolidBrush(Color.FromArgb(180, Color.Green));
      }

      if (accuracy < 0.7)
      {
        return new SolidBrush(Color.FromArgb(180, Color.Yellow));
      }

      if (accuracy < 1)
      {
        return new SolidBrush(Color.FromArgb(180, Color.Orange));
      }

      if (accuracy < 1.5)
      {
        return new SolidBrush(Color.FromArgb(180, Color.Red));
      }

      return new SolidBrush(Color.FromArgb(180, Color.Transparent));
    }

    /// <summary>
    /// Returns a string with a description that represents the given overall accuracy.
    /// </summary>
    /// <param name="result">The calibration result to encode.</param>
    /// <returns>A string with a description that represents the given overall accuracy.</returns>
    private string RatingFunction(CalibrationResult result)
    {
      var accuracy = result.AverageErrorDegree;

      if (accuracy < 0.5)
      {
        return "Quality: Perfect";
      }

      if (accuracy < 0.7)
      {
        return "Quality: Good";
      }

      if (accuracy < 1)
      {
        return "Quality: Moderate";
      }

      if (accuracy < 1.5)
      {
        return "Quality: Poor";
      }

      return "Quality: Redo";
    }

    /// <summary>
    ///   Extracts the calibration points from the result.
    /// </summary>
    private void ExtractCalibrationPoints()
    {
      this.calibrationPoints.Clear();

      foreach (CalibrationPoint plotItem in this.calibrationResult.Calibpoints)
      {
        var p = new PointF((float)plotItem.Coordinates.X, (float)plotItem.Coordinates.Y);

        if (!this.calibrationPoints.Contains(p))
        {
          this.calibrationPoints.Add(p);
        }
      }
    }

    /// <summary>
    /// Gets calibration circle bounds.
    /// </summary>
    /// <param name="center">The center of the calibration point</param>
    /// <param name="radius"> The radius. </param>
    /// <returns> A <see cref="Rectangle"/> with the bounds of the calibration circle. </returns>
    private Rectangle GetCalibrationCircleBounds(PointF center, int radius)
    {
      Point pixelCenter = this.GetScaledPoint(center);
      int d = 2 * radius;

      return new Rectangle(pixelCenter.X - radius, pixelCenter.Y - radius, d, d);
    }

    /// <summary>
    ///   The get canvas bounds.
    /// </summary>
    /// <returns>A <see cref="Rectangle" /> with the bounds of the canvas.</returns>
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
    /// Gets the scaled point.
    /// </summary>
    /// <param name="originalPixelPoint">The original pixel point.</param>
    /// <returns>A <see cref="Point"/> scaled to calibration panels size.</returns>
    private Point GetScaledPoint(PointF originalPixelPoint)
    {
      var presentationScreenSize = Document.ActiveDocument.PresentationSize;
      var scaledX = originalPixelPoint.X / presentationScreenSize.Width;
      var scaledY = originalPixelPoint.Y / presentationScreenSize.Height;

      var normalizedPoint = new PointF(scaledX, scaledY);
      return this.PixelPointFromNormalizedPoint(normalizedPoint);
    }

    /// <summary>
    /// The pixel point from normalized point.
    /// </summary>
    /// <param name="normalizedPoint">
    /// The normalized point.
    /// </param>
    /// <returns>
    /// Returns the normalized point in pixel coordinates.
    /// </returns>
    private Point PixelPointFromNormalizedPoint(PointF normalizedPoint)
    {
      var xPadding = (int)(PaddingRatio * this.Width);
      var yPadding = (int)(PaddingRatio * this.Height);

      int canvasWidth = this.Width - 2 * xPadding;
      int canvasHeight = this.Height - 2 * yPadding - CalibrationTextPadding;

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