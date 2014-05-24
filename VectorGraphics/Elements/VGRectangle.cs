// <copyright file="VGRectangle.cs" company="FU Berlin">
// ******************************************************
// OGAMA - open gaze and mouse analyzer 
// Copyright (C) 2013 Dr. Adrian Voßkühler  
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
  using System.Drawing.Drawing2D;
  using System.Text;

  using VectorGraphics.Tools.CustomTypeConverter;

  /// <summary>
  /// Inherited from <see cref="VGElement"/>.
  /// Creates new vector graphics rectangle.
  /// </summary>
  [Serializable]
  public class VGRectangle : VGElement
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
    /// This indicates whether this shape should be drawn inverted,
    /// that means all outside the bounds of the rectangle is filled with the
    /// brush.
    /// </summary>
    private bool inverted;

    #endregion //FIELDS

    ///////////////////////////////////////////////////////////////////////////////
    // Construction and Initializing methods                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region CONSTRUCTION

    /// <summary>
    /// Initializes a new instance of the VGRectangle class.
    /// </summary>
    /// <param name="newShapeDrawAction">Drawing action: Edge, Fill, Both</param>
    /// <param name="newPen">Pen for edges</param>
    public VGRectangle(ShapeDrawAction newShapeDrawAction, Pen newPen)
      : base(newShapeDrawAction, newPen)
    {
      this.Bounds = Rectangle.Empty;
    }

    /// <summary>
    /// Initializes a new instance of the VGRectangle class.
    /// </summary>
    /// <param name="newShapeDrawAction">Drawing action: Edge, Fill, Both</param>
    /// <param name="newPen">Pen for edges</param>
    /// <param name="newBounds">bounding rectangle</param>
    public VGRectangle(ShapeDrawAction newShapeDrawAction, Pen newPen, RectangleF newBounds)
      : base(newShapeDrawAction, newPen, newBounds)
    {
    }

    /// <summary>
    /// Initializes a new instance of the VGRectangle class.
    /// </summary>
    /// <param name="newShapeDrawAction">Drawing action: Edge, Fill, Both</param>
    /// <param name="newPen">Pen for edges</param>
    /// <param name="newBounds">bounding rectangle</param>
    /// <param name="newStyleGroup">Group Enumeration, <see cref="VGStyleGroup"/></param>
    /// <param name="newName">Name of Element</param>
    /// <param name="newElementGroup">Element group description</param>
    public VGRectangle(
      ShapeDrawAction newShapeDrawAction,
      Pen newPen,
      RectangleF newBounds,
      VGStyleGroup newStyleGroup,
      string newName,
      string newElementGroup)
      : base(newShapeDrawAction, newPen, newBounds, newStyleGroup, newName, newElementGroup)
    {
    }

    /// <summary>
    /// Initializes a new instance of the VGRectangle class.
    /// </summary>
    /// <param name="newShapeDrawAction">Drawing action: Edge, Fill, Both</param>
    /// <param name="newPen">Pen for edges</param>
    /// <param name="newFont">Font for text</param>
    /// <param name="newFontColor">Color for text</param>
    /// <param name="newBounds">bounding rectangle</param>
    /// <param name="newStyleGroup">Group Enumeration, <see cref="VGStyleGroup"/></param>
    /// <param name="newName">Name of Element</param>
    /// <param name="newElementGroup">Element group description</param>
    public VGRectangle(
      ShapeDrawAction newShapeDrawAction,
      Pen newPen,
      Font newFont,
      Color newFontColor,
      RectangleF newBounds,
      VGStyleGroup newStyleGroup,
      string newName,
      string newElementGroup)
      : base(newShapeDrawAction, newPen, newFont, newFontColor, newBounds, newStyleGroup, newName, newElementGroup)
    {
    }

    /// <summary>
    /// Initializes a new instance of the VGRectangle class.
    /// </summary>
    /// <param name="newShapeDrawAction">Drawing action: Edge, Fill, Both</param>
    /// <param name="newPen">Pen for edges</param>
    /// <param name="newBrush">Brush for fills</param>
    /// <param name="newFont">Font for text</param>
    /// <param name="newFontColor">Color for text</param>
    /// <param name="newBounds">bounding rectangle</param>
    /// <param name="newStyleGroup">Group Enumeration, <see cref="VGStyleGroup"/></param>
    /// <param name="newName">Name of Element</param>
    /// <param name="newElementGroup">Element group description</param>
    public VGRectangle(
      ShapeDrawAction newShapeDrawAction,
      Pen newPen,
      Brush newBrush,
      Font newFont,
      Color newFontColor,
      RectangleF newBounds,
      VGStyleGroup newStyleGroup,
      string newName,
      string newElementGroup)
      : base(
      newShapeDrawAction,
      newPen,
      newBrush,
      newFont,
      newFontColor,
      newBounds,
      newStyleGroup,
      newName,
      newElementGroup,
      null)
    {
    }

    /// <summary>
    /// Initializes a new instance of the VGRectangle class.
    /// </summary>
    /// <param name="newShapeDrawAction">Drawing action: Edge, Fill, Both</param>
    /// <param name="newPen">Pen for edges</param>
    /// <param name="newStyleGroup">Group Enumeration, <see cref="VGStyleGroup"/></param>
    /// <param name="newName">Name of Element</param>
    /// <param name="newElementGroup">Element group description</param>
    public VGRectangle(
      ShapeDrawAction newShapeDrawAction,
      Pen newPen,
      VGStyleGroup newStyleGroup,
      string newName,
      string newElementGroup)
      : base(newShapeDrawAction, newPen, newStyleGroup, newName, newElementGroup)
    {
      this.Bounds = Rectangle.Empty;
    }

    /// <summary>
    /// Initializes a new instance of the VGRectangle class.
    /// </summary>
    /// <param name="newShapeDrawAction">Drawing action: Edge, Fill, Both</param>
    /// <param name="newBrush">Brush for fills</param>
    /// <param name="newBounds">bounding rectangle</param>
    public VGRectangle(ShapeDrawAction newShapeDrawAction, Brush newBrush, RectangleF newBounds)
      : base(newShapeDrawAction, newBrush, newBounds)
    {
    }

    /// <summary>
    /// Initializes a new instance of the VGRectangle class.
    /// </summary>
    /// <param name="newShapeDrawAction">Drawing action: Edge, Fill, Both</param>
    /// <param name="newBrush">Brush for fills</param>
    /// <param name="newBounds">bounding rectangle</param>
    /// <param name="newStyleGroup">Group Enumeration, <see cref="VGStyleGroup"/></param>
    /// <param name="newName">Name of Element</param>
    /// <param name="newElementGroup">Element group description</param>
    public VGRectangle(
      ShapeDrawAction newShapeDrawAction,
      Brush newBrush,
      RectangleF newBounds,
      VGStyleGroup newStyleGroup,
      string newName,
      string newElementGroup)
      : base(newShapeDrawAction, newBrush, newBounds, newStyleGroup, newName, newElementGroup)
    {
    }

    /// <summary>
    /// Prevents a default instance of the VGRectangle class from being created.
    /// Parameterless constructor. Used for serialization.
    /// </summary>
    private VGRectangle()
    {
    }

    /// <summary>
    /// Initializes a new instance of the VGRectangle class.
    /// Clone Constructor. Creates new rectangle that is
    /// identical to the given <see cref="VGRectangle"/>
    /// </summary>
    /// <param name="newBounds">Rectangle to clone</param>
    private VGRectangle(VGRectangle newBounds)
      : base(
      newBounds.ShapeDrawAction,
      newBounds.Pen,
      newBounds.Brush,
      newBounds.Font,
      newBounds.FontColor,
      newBounds.Bounds,
      newBounds.StyleGroup,
      newBounds.Name,
      newBounds.ElementGroup,
      newBounds.Sound)
    {
    }

    #endregion //CONSTRUCTION

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Properties                                                       //
    ///////////////////////////////////////////////////////////////////////////////
    #region PROPERTIES

    /// <summary>
    /// Gets or sets a value indicating whether this rectangle should be drawn
    /// inverted, that is the fill fills the region outside this rectangle.
    /// </summary>
    public bool Inverted
    {
      get { return this.inverted; }
      set { this.inverted = value; }
    }

    #endregion //PROPERTIES

    ///////////////////////////////////////////////////////////////////////////////
    // Public methods                                                            //
    ///////////////////////////////////////////////////////////////////////////////
    #region PUBLICMETHODS

    /// <summary>
    /// This static method rounds a given rectangle into a graphics path that
    /// represents the rounded rectangle with the given radius
    /// </summary>
    /// <param name="newBounds">A <see cref="Rectangle"/> with the bounds of the rounded rectangle</param>
    /// <param name="radius">A <see cref="Single"/> with the radius of the corners.</param>
    /// <returns>A <see cref="GraphicsPath"/> with the rounded rectangle.</returns>
    public static GraphicsPath GetRoundedRect(Rectangle newBounds, float radius)
    {
      GraphicsPath gp = new GraphicsPath();

      gp.AddLine(newBounds.X + radius, newBounds.Y, newBounds.X + newBounds.Width - (radius * 2), newBounds.Y);
      gp.AddArc(newBounds.X + newBounds.Width - (radius * 2), newBounds.Y, radius * 2, radius * 2, 270, 90);
      gp.AddLine(newBounds.X + newBounds.Width, newBounds.Y + radius, newBounds.X + newBounds.Width, newBounds.Y + newBounds.Height - (radius * 2));
      gp.AddArc(newBounds.X + newBounds.Width - (radius * 2), newBounds.Y + newBounds.Height - (radius * 2), radius * 2, radius * 2, 0, 90);
      gp.AddLine(newBounds.X + newBounds.Width - (radius * 2), newBounds.Y + newBounds.Height, newBounds.X + radius, newBounds.Y + newBounds.Height);
      gp.AddArc(newBounds.X, newBounds.Y + newBounds.Height - (radius * 2), radius * 2, radius * 2, 90, 90);
      gp.AddLine(newBounds.X, newBounds.Y + newBounds.Height - (radius * 2), newBounds.X, newBounds.Y + radius);
      gp.AddArc(newBounds.X, newBounds.Y, radius * 2, radius * 2, 180, 90);
      gp.CloseFigure();

      return gp;
    }

    #endregion //PUBLICMETHODS

    ///////////////////////////////////////////////////////////////////////////////
    // Inherited methods                                                         //
    ///////////////////////////////////////////////////////////////////////////////
    #region OVERRIDES

    /// <summary>
    /// Overridden <see cref="VGElement.Draw(Graphics)"/> method. 
    /// Draws the rectangle to the given graphics context.
    /// </summary>
    /// <param name="graphics">Graphics context to draw to</param>
    /// <exception cref="ArgumentNullException">Thrown, when graphics object is null.</exception>
    public override void Draw(Graphics graphics)
    {
      if (graphics == null)
      {
        throw new ArgumentNullException("Graphics object should not be null.");
      }

      RectangleF innerBounds = this.Bounds;
      if ((ShapeDrawAction & ShapeDrawAction.Edge) == ShapeDrawAction.Edge)
      {
        innerBounds.Inflate(-this.Pen.Width, -this.Pen.Width);
      }

      if ((ShapeDrawAction & ShapeDrawAction.Fill) == ShapeDrawAction.Fill)
      {
        if (this.Brush != null)
        {
          if (this.inverted)
          {
            Region region = new Region();
            region.MakeEmpty();
            region.Union(graphics.Clip);
            region.Exclude(this.Bounds);
            graphics.FillRegion(this.Brush, region);
            region.Dispose();
          }
          else
          {
            graphics.FillRectangle(Brush, innerBounds);
          }
        }
      }

      // Draw rectangle
      if ((ShapeDrawAction & ShapeDrawAction.Edge) == ShapeDrawAction.Edge)
      {
        RectangleF halfInlineRect = this.Bounds;
        halfInlineRect.Inflate(-this.Pen.Width / 2, -this.Pen.Width / 2);

        graphics.DrawRectangle(Pen, halfInlineRect.X, halfInlineRect.Y, halfInlineRect.Width, halfInlineRect.Height);
      }

      // Draw name and selection frame if applicable
      base.Draw(graphics);
    }

    /// <summary>
    /// Overridden <see cref="Object.ToString()"/> method.
    /// Returns the <see cref="VGRectangle"/> properties as a human readable string.
    /// </summary>
    /// <returns>A <see cref="string"/> that represents this <see cref="VGRectangle"/>.</returns>
    public override string ToString()
    {
      StringBuilder sb = new StringBuilder();
      sb.Append("VGRectangle, Name: ");
      sb.Append(this.Name);
      sb.Append(" ; ShapeDrawAction: ");
      sb.Append(this.ShapeDrawAction.ToString());
      sb.Append(" ; Pen: ");
      sb.Append(ObjectStringConverter.PenToString(this.Pen));
      sb.Append(" ; Brush: ");
      sb.Append(ObjectStringConverter.BrushToString(this.Brush));
      sb.Append(" ; Group: ");
      sb.Append(this.StyleGroup.ToString());
      sb.Append(" ; Bounds: ");
      sb.Append(this.Bounds.ToString());
      return sb.ToString();
    }

    /// <summary>
    /// Overridden <see cref="VGElement.ToShortString()"/> method.
    /// Returns the main <see cref="VGRectangle"/> properties as a human readable string.
    /// </summary>
    /// <returns>A <see cref="string"/> that represents the 
    /// current <see cref="VGRectangle"/> in short form with its main properties.</returns>
    public override string ToShortString()
    {
      StringBuilder sb = new StringBuilder();
      sb.Append("Rectangle ");
      sb.Append(this.Name);
      sb.Append(", Draw: ");
      sb.Append(this.ShapeDrawAction.ToString());
      return sb.ToString();
    }

    /// <summary>
    /// Overridden <see cref="VGElement.CloneCore()"/> method. 
    /// Creates a excact copy of given rectangle
    /// </summary>
    /// <returns>Excact copy of this rectangle</returns>
    protected override VGElement CloneCore()
    {
      return new VGRectangle(this);
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
    #region METHODS
    #endregion //METHODS

    ///////////////////////////////////////////////////////////////////////////////
    // Small helping Methods                                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region HELPER
    #endregion //HELPER
  }
}
