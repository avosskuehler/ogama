// <copyright file="GazepointTrackStatus.cs" company="FU Berlin">
// ******************************************************
// OGAMA - open gaze and mouse analyzer 
// Copyright (C) 2013 Dr. Adrian Voßkühler  
// ------------------------------------------------------------------------
// This program is free software; you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation; either version 2 of the License, or (at your option) any later version.
// This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
// You should have received a copy of the GNU General Public License along with this program; if not, write to the Free Software Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA 02111-1307 USA
// **************************************************************
// </copyright>
// <author>Herval Yatchou</author>
// <email>herval.yatchou@tandemlaunchtech.com</email>
// <modifiedby>Andras Pattantyus, andras@gazept.com</modifiedby>

namespace Ogama.Modules.Recording.GazepointInterface
{
  using System.Drawing;
  using System.Windows.Forms;

  /// <summary>
  /// A dialog which exposes a track status meter to the subject.
  /// </summary>
  public partial class GazepointTrackStatus : Form
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
    /// Drawing area for eye displaying
    /// </summary>
    private Bitmap memDrawArea;

    /// <summary>
    /// Drawing area for position accuracy displaying
    /// </summary>
    private Bitmap memAccuracyDrawArea;

    /// <summary>
    /// Left eye position x
    /// </summary>
    private float memLeftEyePosX;

    /// <summary>
    /// Left eye position y
    /// </summary>
    private float memLeftEyePosY;

    /// <summary>
    /// Right eye position x
    /// </summary>
    private float memRightEyePosX;

    /// <summary>
    /// Right eye position y
    /// </summary>
    private float memRightEyePosY;

    /// <summary>
    /// Precision cursor position
    /// </summary>
    private float memCursorPos;

    /// <summary>
    /// Hardcoded distance between the two circles representing eyes
    /// </summary>
    private float memDistanceBetweenEyes;

    /// <summary>
    /// Height of each colored section in the precision bar
    /// </summary>
    private float memSingleColorAccuracyBarHeight;

    /// <summary>
    /// Width of each colored section in the precision bar
    /// </summary>
    private float memSingleColorAccuracyBarWidth;

    /// <summary>
    /// Maximum/minimum distance (in percent) tolered from the eye tracker
    /// </summary>
    private float memMaxDistanceToleredFromTracker;

    #endregion //FIELDS

    ///////////////////////////////////////////////////////////////////////////////
    // Construction and Initializing methods                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region CONSTRUCTION

    /// <summary>
    /// Initializes a new instance of the GazepointTrackStatus class.
    /// </summary>
    public GazepointTrackStatus()
    {
      this.InitializeComponent();
      this.memDrawArea = new Bitmap(this.pbTrackStatusGazepoint.Width, this.pbTrackStatusGazepoint.Height);
      this.pbTrackStatusGazepoint.Image = this.memDrawArea;
      this.memAccuracyDrawArea = new Bitmap(this.pbAccuracy.Width, this.pbAccuracy.Height);
      this.pbAccuracy.Image = this.memAccuracyDrawArea;
      this.memLeftEyePosX = 0;
      this.memLeftEyePosY = 0;
      this.memRightEyePosX = 0;
      this.memRightEyePosY = 0;

      // At a perfect position from tracker, memCursor must be 1
      // memMaxDistanceToleredFromTracker is in percent, the maximum distance available from the perfect position
      // An available position is 1 +/- memMaxDistanceToleredFromTracker
      // In fact this represent extremities of accuracy bar
      this.memCursorPos = 0;
      this.memMaxDistanceToleredFromTracker = 1.0f;

      // This is the half distance between the center of the two circles representing eyes
      this.memDistanceBetweenEyes = 10;

      this.memSingleColorAccuracyBarHeight = (float)(this.pbAccuracy.Height / 3);
      this.memSingleColorAccuracyBarWidth = (float)this.pbAccuracy.Width;
    }

    /// <summary>
    /// Initialize new values to refresh displaying
    /// </summary>
    /// <param name="gazePosX">X position of gaze</param>
    /// <param name="gazePosY">y position of gaze</param>
    /// <param name="posAccuracy">Accuracy of user position from tracker</param>
    public void UpdateStatus(float gazePosX, float gazePosY, float posAccuracy)
    {
      ////this.GazepointTrackStatusControl.UpdateStatus(gazePosX, gazePosY, posAccuracy);
      this.memLeftEyePosX = (gazePosX * this.pbTrackStatusGazepoint.Width) - this.memDistanceBetweenEyes;
      this.memLeftEyePosY = gazePosY * this.pbTrackStatusGazepoint.Height;
      this.memRightEyePosX = (gazePosX * this.pbTrackStatusGazepoint.Width) + this.memDistanceBetweenEyes;
      this.memRightEyePosY = gazePosY * this.pbTrackStatusGazepoint.Height;
      this.memCursorPos = 1 + (posAccuracy - 1) / this.memMaxDistanceToleredFromTracker;
    }

    /// <summary>
    /// Event to initializes settings when loading
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty</param>
    private void GazepointTrackStatus_Load(object sender, System.EventArgs e)
    {
      Graphics drawer;
      drawer = Graphics.FromImage(this.memDrawArea);
      System.Drawing.Pen mypen = new System.Drawing.Pen(System.Drawing.Brushes.Black);
      drawer.Clear(System.Drawing.Color.Black);
      drawer = Graphics.FromImage(this.memAccuracyDrawArea);
      drawer.Clear(System.Drawing.Color.Black);
      drawer.Dispose();
    }

    /// <summary>
    /// The event handler to refresh the trackstatus window <see cref="PictureBox"/>s.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty</param>
    private void pbTrackStatusGazepoint_Paint(object sender, PaintEventArgs e)
    {
      Graphics drawer;
      drawer = Graphics.FromImage(this.memDrawArea);
      SolidBrush mypen = new SolidBrush(System.Drawing.Color.White);
      drawer.Clear(System.Drawing.Color.Black);
      drawer.FillEllipse(mypen, this.memLeftEyePosX, this.memLeftEyePosY, 10, 10);
      drawer.FillEllipse(mypen, this.memRightEyePosX, this.memRightEyePosY, 10, 10);
      this.pbTrackStatusGazepoint.Image = this.memDrawArea;

      drawer = Graphics.FromImage(this.memAccuracyDrawArea);
      drawer.Clear(System.Drawing.Color.Black);

      mypen.Color = System.Drawing.Color.Red;
      drawer.FillRectangle(mypen, 0, 0, this.memSingleColorAccuracyBarWidth, this.memSingleColorAccuracyBarHeight);

      mypen.Color = System.Drawing.Color.Green;
      drawer.FillRectangle(mypen, 0, this.memSingleColorAccuracyBarHeight, this.memSingleColorAccuracyBarWidth, this.memSingleColorAccuracyBarHeight);

      mypen.Color = System.Drawing.Color.Blue;
      drawer.FillRectangle(mypen, 0, (2 * this.memSingleColorAccuracyBarHeight), this.memSingleColorAccuracyBarWidth, this.memSingleColorAccuracyBarHeight);

      mypen.Color = System.Drawing.Color.Yellow;
      drawer.FillEllipse(mypen, 0, (float)(this.pbAccuracy.Height / 2) * this.memCursorPos, (float)this.pbAccuracy.Width, (float)this.pbAccuracy.Width / 2);

      this.pbAccuracy.Image = this.memAccuracyDrawArea;
      drawer.Dispose();
    }

    #endregion //CONSTRUCTION

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Properties                                                       //
    ///////////////////////////////////////////////////////////////////////////////
    #region PROPERTIES
    #endregion //PROPERTIES

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
    // Inherited methods                                                         //
    ///////////////////////////////////////////////////////////////////////////////
    #region OVERRIDES
    #endregion //OVERRIDES

    ///////////////////////////////////////////////////////////////////////////////
    // Methods for doing main class job                                          //
    ///////////////////////////////////////////////////////////////////////////////
    #region METHODS
    #endregion //METHODS

    ///////////////////////////////////////////////////////////////////////////////
    // Small helping Methods                                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region HELPER
    #endregion //HELPER
  }
}
