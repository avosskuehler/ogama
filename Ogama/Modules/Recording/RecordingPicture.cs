// <copyright file="RecordingPicture.cs" company="FU Berlin">
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

namespace Ogama.Modules.Recording
{
  using System;
  using System.ComponentModel;
  using System.Drawing;
  using System.Windows.Forms;
  using Ogama.ExceptionHandling;
  using VectorGraphics.Canvas;
  using VectorGraphics.Elements;

  /// <summary>
  /// Derived from <see cref="Picture"/>. 
  /// Used to display vector graphic elements of the recording Interface.
  /// </summary>
  public partial class RecordingPicture : Picture
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
    /// Pen for gaze cursor
    /// </summary>
    private Pen penGazeCursor;

    /// <summary>
    /// Pen for mouse cursor
    /// </summary>
    private Pen penMouseCursor;

    /// <summary>
    /// Shape of gaze cursor
    /// </summary>
    private VGCursor gazeCursor;

    /// <summary>
    /// Shape of mouse cursor
    /// </summary>
    private VGCursor mouseCursor;

    #endregion //FIELDS

    ///////////////////////////////////////////////////////////////////////////////
    // Construction and Initializing methods                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region CONSTRUCTION

    /// <summary>
    /// Initializes a new instance of the RecordingPicture class.
    /// </summary>
    public RecordingPicture()
      : base()
    {
      this.InitializeComponent();
      this.InitializeElements();
    }

    #endregion //CONSTRUCTION

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Properties                                                       //
    ///////////////////////////////////////////////////////////////////////////////
    #region PROPERTIES

    /// <summary>
    /// Gets or sets gaze cursor
    /// </summary>
    /// <value>A <see cref="VGCursor"/> that is used to visualize the gaze position.</value>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public VGCursor GazeCursor
    {
      get { return this.gazeCursor; }
      set { this.gazeCursor = value; }
    }

    /// <summary>
    /// Gets or sets mouse cursor
    /// </summary>
    /// <value>A <see cref="VGCursor"/> that is used to visualize the mouse position</value>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public VGCursor MouseCursor
    {
      get { return this.mouseCursor; }
      set { this.mouseCursor = value; }
    }

    /// <summary>
    /// Sets a value indicating whether the mouse cursor is visible or not.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool MouseCursorVisible
    {
      set { this.mouseCursor.Visible = value; }
    }

    #endregion //PROPERTIES

    ///////////////////////////////////////////////////////////////////////////////
    // Inherited methods                                                         //
    ///////////////////////////////////////////////////////////////////////////////
    #region OVERRIDES

    /// <summary>
    /// Overriden animation start method, to check for added
    /// gaze and mouse cursor on begin.
    /// </summary>
    public override void StartAnimation()
    {
      if (this.Elements.Count == 0)
      {
        this.Elements.Add(this.mouseCursor);
        this.Elements.Add(this.gazeCursor);
      }

      base.StartAnimation();
    }

    /// <summary>
    /// Overridden. Removes all Elements from Element List and clears picture.
    /// </summary>
    public override void ResetPicture()
    {
      base.ResetPicture();
      this.Invalidate();
    }

    /// <summary>
    /// Overridden. Frees resources of objects that are not disposed
    /// by the designer, mainly private objects.
    /// Is called during the call to <see cref="Control.Dispose(Boolean)"/>.
    /// </summary>
    protected override void CustomDispose()
    {
      this.penGazeCursor.Dispose();
      this.penMouseCursor.Dispose();
      this.gazeCursor.Dispose();
      this.mouseCursor.Dispose();

      base.CustomDispose();
    }

    /// <summary>
    /// Overridden. Calls picture update method. Invoked from Picture Animation timer tick method.
    /// </summary>
    /// <param name="sender">sending frame, normally base picture class timer</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    /// <remarks>Starts updating the readed samples for the timespan that
    /// is over since the last update.</remarks>
    protected override void ForegroundTimerTick(object sender, EventArgs e)
    {
      // Retreive new cursor positions.
      PointF gazeCenter;
      PointF mouseCenter;
      ((RecordModule)this.OwningForm).GetCurrentCursorPositions(out gazeCenter, out mouseCenter);

      this.gazeCursor.Center = gazeCenter;
      this.mouseCursor.Center = mouseCenter;

      base.ForegroundTimerTick(sender, e);
    }

    #endregion //OVERRIDES

    ///////////////////////////////////////////////////////////////////////////////
    // Eventhandler                                                              //
    ///////////////////////////////////////////////////////////////////////////////
    #region EVENTS

    ///////////////////////////////////////////////////////////////////////////////
    // Eventhandler for Custom Defined Events                                    //
    ///////////////////////////////////////////////////////////////////////////////
    #region CUSTOMEVENTHANDLER
    #endregion //CUSTOMEVENTHANDLER

    #endregion //EVENTS

    ///////////////////////////////////////////////////////////////////////////////
    // Methods for doing main class job                                          //
    ///////////////////////////////////////////////////////////////////////////////
    #region METHODS

    /// <summary>
    /// Initializes standard values of drawing elements
    /// </summary>
    private void InitializeElements()
    {
      try
      {
        this.penGazeCursor = new Pen(Properties.Settings.Default.GazeCursorColor, Properties.Settings.Default.GazeCursorWidth);
        this.penGazeCursor.DashStyle = Properties.Settings.Default.GazeCursorStyle;

        this.penMouseCursor = new Pen(Properties.Settings.Default.MouseCursorColor, Properties.Settings.Default.MouseCursorWidth);
        this.penMouseCursor.DashStyle = Properties.Settings.Default.MouseCursorStyle;

        float gazeCursorSize = (float)Properties.Settings.Default.GazeCursorSize;
        VGCursor.DrawingCursors gazeCursorType = (VGCursor.DrawingCursors)Enum.Parse(
          typeof(VGCursor.DrawingCursors), Properties.Settings.Default.GazeCursorType);
        this.gazeCursor = new VGCursor(this.penGazeCursor, gazeCursorType, gazeCursorSize, VGStyleGroup.RPL_PEN_GAZE_CURSOR);

        float mouseCursorSize = (float)Properties.Settings.Default.MouseCursorSize;
        VGCursor.DrawingCursors mouseCursorType = (VGCursor.DrawingCursors)Enum.Parse(
          typeof(VGCursor.DrawingCursors), Properties.Settings.Default.MouseCursorType);
        this.mouseCursor = new VGCursor(this.penMouseCursor, mouseCursorType, mouseCursorSize, VGStyleGroup.RPL_PEN_MOUSE_CURSOR);
      }
      catch (Exception ex)
      {
        ExceptionMethods.HandleException(ex);
      }
    }

    #endregion //METHODS

    ///////////////////////////////////////////////////////////////////////////////
    // Small helping Methods                                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region HELPER
    #endregion //HELPER
  }
}
