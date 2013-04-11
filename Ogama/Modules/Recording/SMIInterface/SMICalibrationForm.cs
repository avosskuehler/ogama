// <copyright file="SMICalibrationForm.cs" company="FU Berlin">
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

namespace Ogama.Modules.Recording.SMIInterface
{
  using System.Collections.Generic;
  using System.Drawing;
  using System.Windows.Forms;

  /// <summary>
  /// This <see cref="Form"/> is used to show calibration points in
  /// a top most and presentation fullscreen window with the given
  /// color, backgroundcolor and size.
  /// </summary>
  public partial class SMICalibrationForm : Form
  {
    ///////////////////////////////////////////////////////////////////////////////
    // Defining Constants                                                        //
    ///////////////////////////////////////////////////////////////////////////////
    #region CONSTANTS
    #endregion //CONSTANTS

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Variables, Enumerations, Events                                  //
    ///////////////////////////////////////////////////////////////////////////////
    #region FIELDS

    /// <summary>
    /// This is the diameter value for the calibration points.
    /// </summary>
    private int usedPointSize;

    /// <summary>
    /// The <see cref="Color"/> for the calibration
    /// points.
    /// </summary>
    private Color calibPointColor;

    /// <summary>
    /// The <see cref="Point"/> with the current shown 
    /// calibration points location.
    /// </summary>
    private Point currentPointLocation;

    /// <summary>
    /// The <see cref="List{Point}"/> with the calibration points.
    /// </summary>
    private List<Point> calibrationPoints;

    #endregion //FIELDS

    ///////////////////////////////////////////////////////////////////////////////
    // Construction and Initializing methods                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region CONSTRUCTION

    /// <summary>
    /// Initializes a new instance of the SMICalibrationForm class.
    /// </summary>
    public SMICalibrationForm()
    {
      this.InitializeComponent();
      this.currentPointLocation = Point.Empty;
      this.calibrationPoints = new List<Point>();
      this.usedPointSize = 20;
    }

    #endregion //CONSTRUCTION

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Enumerations                                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region ENUMS
    #endregion ENUMS

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Properties                                                       //
    ///////////////////////////////////////////////////////////////////////////////
    #region PROPERTIES

    /// <summary>
    /// Gets or sets the <see cref="List{Point}"/>
    /// with the calibration point locations.
    /// </summary>
    public List<Point> CalibrationPoints
    {
      get { return this.calibrationPoints; }
      set { this.calibrationPoints = value; }
    }

    /// <summary>
    /// Sets the <see cref="Color"/> for
    /// the calibration points.
    /// </summary>
    public Color CalibPointColor
    {
      set { this.calibPointColor = value; }
    }

    /// <summary>
    /// Sets the <see cref="CalibrationPointSize"/>
    /// for the calibration points.
    /// </summary>
    public CalibrationPointSize CalibPointSize
    {
      set
      {
        switch (value)
        {
          case CalibrationPointSize.Small:
            this.usedPointSize = 20;
            break;
          default:
          case CalibrationPointSize.Medium:
            this.usedPointSize = 40;
            break;
          case CalibrationPointSize.Large:
            this.usedPointSize = 80;
            break;
        }
      }
    }

    #endregion //PROPERTIES

    ///////////////////////////////////////////////////////////////////////////////
    // Public methods                                                            //
    ///////////////////////////////////////////////////////////////////////////////
    #region PUBLICMETHODS

    /// <summary>
    /// This method changes the shown calibration point index to
    /// the new value. This is NOT zero-based (1-based)
    /// </summary>
    /// <param name="calibrationPointIndex">A (1-based) index of the new point
    /// to be shown.</param>
    public void ShowCalibrationPoint(int calibrationPointIndex)
    {
      if (this.calibrationPoints.Count >= calibrationPointIndex)
      {
        this.currentPointLocation = this.calibrationPoints[calibrationPointIndex - 1];
        this.Invalidate();
      }
    }

    /// <summary>
    /// This method is a thread safe version for the <see cref="Form.Close()"/>
    /// method.
    /// </summary>
    public void ThreadSafeClose()
    {
      if (this.InvokeRequired)
      {
        this.Invoke(new MethodInvoker(this.Close));
        return;
      }

      this.Close();
    }

    #endregion //PUBLICMETHODS

    ///////////////////////////////////////////////////////////////////////////////
    // Inherited methods                                                         //
    ///////////////////////////////////////////////////////////////////////////////
    #region OVERRIDES

    /// <summary>
    /// The <see cref="Control.Paint"/> event handler for this <see cref="Form"/>.
    /// Paints the current calibration point.
    /// </summary>
    /// <param name="e">A <see cref="PaintEventArgs"/> with the event data.</param>
    protected override void OnPaint(PaintEventArgs e)
    {
      base.OnPaint(e);
      SolidBrush brush = new SolidBrush(this.calibPointColor);

      // Draw bounding circle
      e.Graphics.FillEllipse(
        brush,
        this.currentPointLocation.X - this.usedPointSize / 2,
        this.currentPointLocation.Y - this.usedPointSize / 2,
        this.usedPointSize,
        this.usedPointSize);

      // Draw center point
      e.Graphics.FillEllipse(
        Brushes.Black,
        this.currentPointLocation.X - 1,
        this.currentPointLocation.Y - 1,
        2,
        2);
    }

    #endregion //OVERRIDES

    ///////////////////////////////////////////////////////////////////////////////
    // Eventhandler                                                              //
    ///////////////////////////////////////////////////////////////////////////////
    #region EVENTS

    ///////////////////////////////////////////////////////////////////////////////
    // Eventhandler for UI, Menu, Buttons, Toolbars etc.                         //
    ///////////////////////////////////////////////////////////////////////////////
    #region WINDOWSEVENTHANDLER
    #endregion //WINDOWSEVENTHANDLER

    ///////////////////////////////////////////////////////////////////////////////
    // Eventhandler for Custom Defined Events                                    //
    ///////////////////////////////////////////////////////////////////////////////
    #region CUSTOMEVENTHANDLER
    #endregion //CUSTOMEVENTHANDLER

    #endregion //EVENTS

    ///////////////////////////////////////////////////////////////////////////////
    // Methods and Eventhandling for Background tasks                            //
    ///////////////////////////////////////////////////////////////////////////////
    #region BACKGROUNDWORKER
    #endregion //BACKGROUNDWORKER

    ///////////////////////////////////////////////////////////////////////////////
    // Methods for doing main class job                                          //
    ///////////////////////////////////////////////////////////////////////////////
    #region PRIVATEMETHODS
    #endregion //PRIVATEMETHODS

    ///////////////////////////////////////////////////////////////////////////////
    // Small helping Methods                                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region HELPER
    #endregion //HELPER
  }
}
