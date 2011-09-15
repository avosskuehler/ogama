using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Tobii.Eyetracking.Sdk;
using Point = System.Drawing.Point;

namespace Ogama.Modules.Recording.TobiiDevice
{
  public partial class TobiiCalibrationResultPanel : Control
  {

    private const float PaddingRatio = 0.07F;
    private const int CircleRadius = 5;

    private List<CalibrationPlotItem> _calibrationData;
    private EyeOption _eyeOption;

    private readonly List<PointF> _calibrationPoints;

    public TobiiCalibrationResultPanel()
    {
      InitializeComponent();

      _calibrationPoints = new List<PointF>();
    }

    [Browsable(true)]
    public EyeOption EyeOption
    {
      get { return _eyeOption; }
      set { _eyeOption = value; }
    }

    public void Initialize(List<CalibrationPlotItem> calibrationData)
    {
      _calibrationData = calibrationData;
      ExtractCalibrationPoints();

      Invalidate();
    }


    protected override void OnPaint(PaintEventArgs pe)
    {
      base.OnPaint(pe);

      if (_calibrationData != null && _calibrationPoints != null)
      {
        using (Pen pen = new Pen(Color.DarkGray))
        {
          // Draw calibration points
          pen.Color = Color.DarkGray;
          foreach (PointF calibrationPoint in _calibrationPoints)
          {
            Rectangle r = GetCalibrationCircleBounds(calibrationPoint, CircleRadius);
            pe.Graphics.DrawEllipse(pen, r);
          }

          // Draw bounds
          pen.Color = Color.LightGray;
          Rectangle canvasBounds = GetCanvasBounds();
          pe.Graphics.DrawRectangle(pen, canvasBounds);

          // Draw errors
          foreach (var plotItem in _calibrationData)
          {

            if ((_eyeOption & EyeOption.Left) == EyeOption.Left)
            {
              if (plotItem.ValidityLeft == 1)
              {
                pen.Color = Color.Red;

                Point p1 = PixelPointFromNormalizedPoint(new PointF(plotItem.TrueX, plotItem.TrueY));
                Point p2 = PixelPointFromNormalizedPoint(new PointF(plotItem.MapLeftX, plotItem.MapLeftY));

                pe.Graphics.DrawLine(pen, p1, p2);
              }
            }

            if ((_eyeOption & EyeOption.Right) == EyeOption.Right)
            {
              if (plotItem.ValidityRight == 1)
              {
                pen.Color = Color.Lime;

                Point p1 = PixelPointFromNormalizedPoint(new PointF(plotItem.TrueX, plotItem.TrueY));
                Point p2 = PixelPointFromNormalizedPoint(new PointF(plotItem.MapRightX, plotItem.MapRightY));

                pe.Graphics.DrawLine(pen, p1, p2);
              }
            }
          }
        }
      }
    }

    private void ExtractCalibrationPoints()
    {
      _calibrationPoints.Clear();

      foreach (var plotItem in _calibrationData)
      {
        PointF p = new PointF(plotItem.TrueX, plotItem.TrueY);

        if (!_calibrationPoints.Contains(p))
        {
          _calibrationPoints.Add(p);
        }
      }
    }


    private Rectangle GetCalibrationCircleBounds(PointF center, int radius)
    {
      Point pixelCenter = PixelPointFromNormalizedPoint(center);
      int d = 2 * radius;

      return new Rectangle(pixelCenter.X - radius, pixelCenter.Y - radius, d, d);
    }

    private Point PixelPointFromNormalizedPoint(PointF normalizedPoint)
    {
      int xPadding = (int)(PaddingRatio * Width);
      int yPadding = (int)(PaddingRatio * Height);

      int canvasWidth = Width - 2 * xPadding;
      int canvasHeight = Height - 2 * yPadding;

      Point pixelPoint = new Point();
      pixelPoint.X = xPadding + (int)(normalizedPoint.X * canvasWidth);
      pixelPoint.Y = yPadding + (int)(normalizedPoint.Y * canvasHeight);

      return pixelPoint;
    }

    private Rectangle GetCanvasBounds()
    {
      Point upperLeft = PixelPointFromNormalizedPoint(new PointF(0F, 0F));
      Point lowerRight = PixelPointFromNormalizedPoint(new PointF(1F, 1F));

      Rectangle bounds = new Rectangle();

      bounds.Location = upperLeft;
      bounds.Width = lowerRight.X - upperLeft.X;
      bounds.Height = lowerRight.Y - upperLeft.Y;

      return bounds;
    }
  }

  [Flags]
  public enum EyeOption
  {
    Left = 1,
    Right = 2
  }
}