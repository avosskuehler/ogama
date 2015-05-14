// <copyright file="GrabHandle.cs" company="FU Berlin">
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

namespace VectorGraphics.Elements
{
  using System;
  using System.Drawing;
  using System.Windows.Forms;
  using System.Xml.Serialization;

  /// <summary>
  /// This class <strong>GrabHandle</strong> encapsulates a single grab handle,
  /// which is a rectangular shape at the corners of a vector graphic element,
  /// that indicates the possibility of moving its corners.
  /// </summary>
  [Serializable]
  public class GrabHandle
  {
    ///////////////////////////////////////////////////////////////////////////////
    // Defining Constants                                                        //
    ///////////////////////////////////////////////////////////////////////////////
    #region CONSTANTS

    /// <summary>
    /// Size of grab handles when in edit mode. Used for resizing shapes.
    /// </summary>
    private static int handleSize = 12;

    #endregion //CONSTANTS

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Variables, Enumerations, Events                                  //
    ///////////////////////////////////////////////////////////////////////////////
    #region FIELDS

    /// <summary>
    /// Saves the <see cref="HandlePosition"/> of the grab handle,
    /// resp. its position in the owning shape.
    /// </summary>
    private HandlePosition handlePosition;

    /// <summary>
    /// Saves the size of the grab handle.
    /// </summary>
    private Size size;

    /// <summary>
    /// Saves the location of the grab handle in experiment coordinates.
    /// </summary>
    private PointF location;

    /// <summary>
    /// Saves the cursor, that will be shown when the mouse is over this grab handle.
    /// </summary>
    private Cursor cursor;

    #endregion //FIELDS

    ///////////////////////////////////////////////////////////////////////////////
    // Construction and Initializing methods                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region CONSTRUCTION

    /// <summary>
    /// Initializes a new instance of the GrabHandle class.
    /// </summary>
    /// <param name="newLocation">new grab handle location</param>
    /// <param name="newCursor">new grab handle mouse over cursor</param>
    /// <param name="newHandlePosition">Position indicator</param>
    public GrabHandle(Point newLocation, Cursor newCursor, HandlePosition newHandlePosition)
    {
      this.size = new Size(HANDLESIZE, HANDLESIZE);
      this.location = newLocation;
      this.cursor = newCursor;
      this.handlePosition = newHandlePosition;
    }

    /// <summary>
    /// Initializes a new instance of the GrabHandle class.
    /// </summary>
    /// <param name="newLocation">new grab handle location</param>
    /// <param name="newCursor">new grab handle mouse over cursor</param>
    /// <param name="newHandlePosition">Position indicator</param>
    public GrabHandle(PointF newLocation, Cursor newCursor, HandlePosition newHandlePosition)
    {
      this.size = new Size(HANDLESIZE, HANDLESIZE);
      this.location = newLocation;
      this.cursor = newCursor;
      this.handlePosition = newHandlePosition;
    }

    /// <summary>
    /// Prevents a default instance of the GrabHandle class from being created.
    /// Parameterless Constructor. Needed for Serialization.
    /// </summary>
    private GrabHandle()
    {
    }

    #endregion //CONSTRUCTION

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Enumerations                                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region ENUMS

    /// <summary>
    /// Grab handle resize modes.
    /// </summary>
    public enum HandlePosition
    {
      /// <summary>
      /// Top center resize handle position
      /// </summary>
      Top,

      /// <summary>
      /// Bottom center resize handle position
      /// </summary>
      Down,

      /// <summary>
      /// Left center resize handle position
      /// </summary>
      Left,

      /// <summary>
      /// Right center resize handle position
      /// </summary>
      Right,

      /// <summary>
      /// Middle resize handle position
      /// </summary>
      Center,

      /// <summary>
      /// TopLeft resize handle position
      /// </summary>
      TopLeft,

      /// <summary>
      /// TopRight resize handle position
      /// </summary>
      TopRight,

      /// <summary>
      /// BottomLeft resize handle position
      /// </summary>
      BottomLeft,

      /// <summary>
      /// BottomRight resize handle position
      /// </summary>
      BottomRight,
    }

    #endregion ENUMS

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Properties                                                       //
    ///////////////////////////////////////////////////////////////////////////////
    #region PROPERTIES

    /// <summary>
    /// Gets the size of grab handles when in edit mode. Used for resizing shapes.
    /// </summary>
    public static int HANDLESIZE
    {
      get { return handleSize; }
    }

    /// <summary>
    /// Gets or sets the position of the grab handle 
    /// in relation to the owning shape.
    /// </summary>
    /// <value>A <see cref="HandlePosition"/> which indicates
    /// the position of the grab handle.</value>
    public HandlePosition GrabHandlePosition
    {
      get { return this.handlePosition; }
      set { this.handlePosition = value; }
    }

    /// <summary>
    /// Gets or sets the size of the grab handle
    /// </summary>
    /// <value>A <see cref="Size"/> with the size of the grab handle.</value>
    public Size Size
    {
      get { return this.size; }
      set { this.size = value; }
    }

    /// <summary>
    /// Gets or sets location of the grab handle
    /// </summary>
    /// <value>A <see cref="PointF"/> with the top left position
    /// of the grab handle.</value>
    public PointF Location
    {
      get { return this.location; }
      set { this.location = value; }
    }

    /// <summary>
    /// Gets or sets center of the grab handle
    /// </summary>
    /// <value>A <see cref="PointF"/> with the center of the grab handle.</value>
    public PointF Center
    {
      get
      {
        return new PointF(this.location.X + this.size.Width / 2, this.location.Y + this.size.Height / 2);
      }

      set
      {
        this.location = new PointF(value.X - this.size.Width / 2, value.Y - this.size.Height / 2);
      }
    }

    /// <summary>
    /// Gets or sets the cursor to show when the mouse is over the grab handle.
    /// </summary>
    /// <value>A <see cref="Cursor"/> that is show when the mouse
    /// is over the grab handle.</value>
    [XmlIgnoreAttribute]
    public Cursor Cursor
    {
      get { return this.cursor; }
      set { this.cursor = value; }
    }

    #endregion //PROPERTIES

    ///////////////////////////////////////////////////////////////////////////////
    // Public methods                                                            //
    ///////////////////////////////////////////////////////////////////////////////
    #region PUBLICMETHODS

    /// <summary>
    /// Draws this grab handle to the given graphics context
    /// </summary>
    /// <param name="g">A <see cref="Graphics"/> to draw the grab handle to.</param>
    public void Draw(Graphics g)
    {
      if (this.handlePosition == HandlePosition.Center)
      {
        RectangleF centerPoint = new RectangleF(this.Location, this.Size);
        g.FillEllipse(new SolidBrush(Color.White), centerPoint);
        g.DrawEllipse(new Pen(Color.Black), centerPoint);
      }
      else
      {
        ControlPaint.DrawGrabHandle(
          g,
          new Rectangle((int)this.location.X, (int)this.location.Y, this.size.Width, this.size.Height), 
          true, 
          true);
      }
    }

    /// <summary>
    /// Detects if given point is in grab handle bounded area.
    /// </summary>
    /// <param name="point">Point to check</param>
    /// <returns><strong>True</strong>, if given point is inside the bounds of the grab handle,
    /// otherwise <strong>false</strong>.</returns>
    public bool Contains(Point point)
    {
      RectangleF currentBounds = new RectangleF(this.location, this.size);
      return currentBounds.Contains(point);
    }

    #endregion //PUBLICMETHODS
    
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
