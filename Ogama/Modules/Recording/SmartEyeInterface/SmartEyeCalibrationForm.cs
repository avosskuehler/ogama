// <copyright file="SmartEyeCalibrationForm.cs" company="Smart Eye AB">
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
  using System;
  using System.Drawing;
  using System.Drawing.Drawing2D;
  using System.Windows.Forms;

  using Ogama.Modules.Common.Tools;

  using SmartEye.Geometry;

  /// <summary>
  /// The calibration form.
  /// </summary>
  public partial class SmartEyeCalibrationForm : Form
  {
    ///////////////////////////////////////////////////////////////////////////////
    // Construction and Initializing methods                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region CONSTRUCTION

    /// <summary>
    /// Initializes a new instance of the <see cref="SmartEyeCalibrationForm"/> class.
    /// </summary>
    public SmartEyeCalibrationForm()
    {
      this.InitializeComponent();
      this.CalibrationPoint = new Point2D(0, 0);
      this.PointColor = Color.Transparent;
    }

    #endregion CONSTRUCTION

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Properties                                                       //
    ///////////////////////////////////////////////////////////////////////////////
    #region PROPERTIES

    /// <summary>
    /// Gets or sets CalibrationPoint.
    /// </summary>
    public Point2D CalibrationPoint { get; set; }

    /// <summary>
    /// Gets or sets PointColor.
    /// </summary>
    public Color PointColor { get; set; }

    /// <summary>
    /// Gets or sets PointColor.
    /// </summary>
    public int PointSize { get; set; }

    /// <summary>
    /// Gets or sets the Message.
    /// </summary>
    public string Message { get; set; }

    /// <summary>
    /// Gets or sets MessageColor.
    /// </summary>
    public Color MessageColor { get; set; }

    #endregion PROPERTIES

    ///////////////////////////////////////////////////////////////////////////////
    // Methods for doing main class job                                          //
    ///////////////////////////////////////////////////////////////////////////////
    #region METHODS

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
    /// <param name="size">
    /// The size.
    /// </param>
    public void DrawCalibrationPoint(Point2D point, Color color, int size)
    {
      this.CalibrationPoint = point;
      this.PointColor = color;
      this.PointSize = size;
      this.Refresh();
    }

    /// <summary>
    /// Show a message
    /// </summary>
    /// <param name="message">The message</param>
    /// <param name="color">The color</param>
    public void ShowMessage(string message, Color color)
    {
      this.Message = message;
      this.MessageColor = color;
      this.Refresh();
    }

    /// <summary>
    /// Clear the shown message
    /// </summary>
    public void ClearMessage()
    {
      this.Message = null;
      this.Refresh();
    }

    /// <summary>
    /// The on paint.
    /// </summary>
    /// <param name="e">
    /// The e.
    /// </param>
    protected override void OnPaint(PaintEventArgs e)
    {
      base.OnPaint(e);

      if (this.Message != null)
      {
        var x = PresentationScreen.GetPresentationResolution().Width / 2;
        var y = PresentationScreen.GetPresentationResolution().Height / 2;

        Font stringFont = new Font("Helvetica", 24);

        // Measure string.
        SizeF stringSize = new SizeF();
        stringSize = e.Graphics.MeasureString(this.Message, stringFont);
        
        // Draw string to screen.
        e.Graphics.DrawString(this.Message, stringFont, new SolidBrush(this.MessageColor), new PointF(x - stringSize.Width / 2, y - stringSize.Height / 2));
      }

      // Draw calibration circle
      if (this.CalibrationPoint == null)
      {
        return;
      }

      var monitorSize = PresentationScreen.GetPresentationResolution();
      var circleBounds = new Rectangle
        {
          X = (int)(monitorSize.Width * this.CalibrationPoint.X - this.PointSize),
          Y = (int)(monitorSize.Height * this.CalibrationPoint.Y - this.PointSize),
          Width = 2 * this.PointSize,
          Height = 2 * this.PointSize
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

    /// <summary>
    /// Hide cursor when entering with mouse.
    /// </summary>
    /// <param name="sender">The sender</param>
    /// <param name="e">The event</param>
    private void SmartEyeCalibrationForm_MouseEnter(object sender, EventArgs e)
    {
      Cursor.Hide();
    }

    /// <summary>
    /// Show cursor when leaving with mouse.
    /// </summary>
    /// <param name="sender">The sender</param>
    /// <param name="e">The event</param>
    private void SmartEyeCalibrationForm_MouseLeave(object sender, EventArgs e)
    {
      Cursor.Show();
    }

    /// <summary>
    /// Show mouse cursor when closing the form
    /// </summary>
    /// <param name="sender">The sender</param>
    /// <param name="e">The event</param>
    private void SmartEyeCalibrationForm_FormClosed(object sender, FormClosedEventArgs e)
    {
      Cursor.Show();
    }

    #endregion METHODS
  }
}