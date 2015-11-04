// <copyright file="VGEllipse.cs" company="FU Berlin">
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
  using System.Drawing.Drawing2D;
  using System.Text;

  using VectorGraphics.Tools.CustomTypeConverter;

  /// <summary>
  /// Inherited from <see cref="VGElement"/>. 
  /// An xml serializable class that is a vector graphics ellipse,
  /// that can have a bounding line, a fill and a name.
  /// </summary>
  [Serializable]
  public class VGEllipse : VGElement
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
    /// that means all outside the bounds of the ellipse is filled with the
    /// brush.
    /// </summary>
    private bool inverted;

    #endregion //FIELDS

    ///////////////////////////////////////////////////////////////////////////////
    // Construction and Initializing methods                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region CONSTRUCTION

    /// <summary>
    /// Initializes a new instance of the VGEllipse class.
    /// </summary>
    /// <param name="newShapeDrawAction">Drawing action: Edge, Fill, Both</param>
    /// <param name="newPen">Pen for edges</param>
    /// <param name="newBrush">Brush for fills</param>
    /// <param name="newBounds">bounding rectangle</param>
    /// <param name="newStyleGroup">Group Enumeration, <see cref="VGStyleGroup"/></param>
    /// <param name="newName">Name of Element</param>
    /// <param name="newElementGroup">Element group description</param>
    public VGEllipse(
      ShapeDrawAction newShapeDrawAction,
      Pen newPen,
      Brush newBrush,
      RectangleF newBounds,
      VGStyleGroup newStyleGroup,
      string newName,
      string newElementGroup)
      : base(newShapeDrawAction, newPen, newBrush, newBounds, newStyleGroup, newName, newElementGroup)
    {
    }

    /// <summary>
    /// Initializes a new instance of the VGEllipse class.
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
    public VGEllipse(
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
    /// Initializes a new instance of the VGEllipse class.
    /// </summary>
    /// <param name="newShapeDrawAction">Drawing action: Edge, Fill, Both</param>
    /// <param name="newPen">Pen for edges</param>
    public VGEllipse(ShapeDrawAction newShapeDrawAction, Pen newPen)
      : base(newShapeDrawAction, newPen)
    {
    }

    /// <summary>
    /// Initializes a new instance of the VGEllipse class.
    /// </summary>
    /// <param name="newShapeDrawAction">Drawing action: Edge, Fill, Both</param>
    /// <param name="newPen">Pen for edges</param>
    /// <param name="newStyleGroup">Group Enumeration, <see cref="VGStyleGroup"/></param>
    /// <param name="newName">Name of Element</param>
    /// <param name="newElementGroup">Element group description</param>
    public VGEllipse(
      ShapeDrawAction newShapeDrawAction,
      Pen newPen,
      VGStyleGroup newStyleGroup,
      string newName,
      string newElementGroup)
      : base(newShapeDrawAction, newPen, newStyleGroup, newName, newElementGroup)
    {
    }

    /// <summary>
    /// Initializes a new instance of the VGEllipse class.
    /// </summary>
    /// <param name="newShapeDrawAction">Drawing action: Edge, Fill, Both</param>
    /// <param name="newBrush">Brush for fills</param>
    public VGEllipse(ShapeDrawAction newShapeDrawAction, Brush newBrush)
      : base(newShapeDrawAction, newBrush)
    {
    }

    /// <summary>
    /// Initializes a new instance of the VGEllipse class.
    /// </summary>
    /// <param name="newShapeDrawAction">Drawing action: Edge, Fill, Both</param>
    /// <param name="newBrush">Brush for fills</param>
    /// <param name="newStyleGroup">Group Enumeration, <see cref="VGStyleGroup"/></param>
    /// <param name="newName">Name of Element</param>
    /// <param name="newElementGroup">Element group description</param>
    public VGEllipse(
      ShapeDrawAction newShapeDrawAction,
      Brush newBrush,
      VGStyleGroup newStyleGroup,
      string newName,
      string newElementGroup)
      : base(newShapeDrawAction, newBrush, newStyleGroup, newName, newElementGroup)
    {
    }

    /// <summary>
    /// Initializes a new instance of the VGEllipse class.
    /// </summary>
    /// <param name="newShapeDrawAction">Drawing action: Edge, Fill, Both</param>
    /// <param name="newPen">Pen for edges</param>
    /// <param name="newBounds">bounding rectangle</param>
    public VGEllipse(ShapeDrawAction newShapeDrawAction, Pen newPen, RectangleF newBounds)
      : base(newShapeDrawAction, newPen, newBounds)
    {
    }

    /// <summary>
    /// Initializes a new instance of the VGEllipse class.
    /// </summary>
    /// <param name="newShapeDrawAction">Drawing action: Edge, Fill, Both</param>
    /// <param name="newPen">Pen for edges</param>
    /// <param name="newBounds">bounding rectangle</param>
    /// <param name="newStyleGroup">Group Enumeration, <see cref="VGStyleGroup"/></param>
    /// <param name="newName">Name of Element</param>
    /// <param name="newElementGroup">Element group description</param>
    public VGEllipse(
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
    /// Initializes a new instance of the VGEllipse class.
    /// </summary>
    /// <param name="newShapeDrawAction">Drawing action: Edge, Fill, Both</param>
    /// <param name="newPen">Pen for edges</param>
    /// <param name="newFont">Font for text</param>
    /// <param name="newFontColor">Color for text</param>
    /// <param name="newBounds">bounding rectangle</param>
    /// <param name="newStyleGroup">Group Enumeration, <see cref="VGStyleGroup"/></param>
    /// <param name="newName">Name of Element</param>
    /// <param name="newElementGroup">Element group description</param>
    public VGEllipse(
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
    /// Initializes a new instance of the VGEllipse class.
    /// </summary>
    /// <param name="newShapeDrawAction">Drawing action: Edge, Fill, Both</param>
    /// <param name="newBrush">Brush for fills</param>
    /// <param name="newBounds">bounding rectangle</param>
    public VGEllipse(ShapeDrawAction newShapeDrawAction, Brush newBrush, RectangleF newBounds)
      : base(newShapeDrawAction, newBrush, newBounds)
    {
    }

    /// <summary>
    /// Initializes a new instance of the VGEllipse class.
    /// </summary>
    /// <param name="newShapeDrawAction">Drawing action: Edge, Fill, Both</param>
    /// <param name="newBrush">Brush for fills</param>
    /// <param name="newBounds">bounding rectangle</param>
    /// <param name="newStyleGroup">Group Enumeration, <see cref="VGStyleGroup"/></param>
    /// <param name="newName">Name of Element</param>
    /// <param name="newElementGroup">Element group description</param>
    public VGEllipse(
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
    /// Initializes a new instance of the VGEllipse class.
    /// </summary>
    /// <param name="newShapeDrawAction">Drawing action: Edge, Fill, Both</param>
    /// <param name="newBrush">Brush for fills</param>
    /// <param name="newFont">Font for text</param>
    /// <param name="newFontColor">Color for text</param>
    /// <param name="newBounds">bounding rectangle</param>
    /// <param name="newStyleGroup">Group Enumeration, <see cref="VGStyleGroup"/></param>
    /// <param name="newName">Name of Element</param>
    /// <param name="newElementGroup">Element group description</param>
    public VGEllipse(
      ShapeDrawAction newShapeDrawAction,
      Brush newBrush,
      Font newFont,
      Color newFontColor,
      RectangleF newBounds,
      VGStyleGroup newStyleGroup,
      string newName,
      string newElementGroup)
      : base(newShapeDrawAction, newBrush, newFont, newFontColor, newBounds, newStyleGroup, newName, newElementGroup)
    {
    }

    /// <summary>
    /// Prevents a default instance of the VGEllipse class from being created.
    /// Parameterless constructor. Used for serialization.
    /// </summary>
    private VGEllipse()
    {
    }

    /// <summary>
    /// Initializes a new instance of the VGEllipse class.
    /// Clone Constructor. Creates new ellipse element that is
    /// identical to the given VGEllipse.
    /// </summary>
    /// <param name="ellipse"><see cref="VGEllipse"/> to clone</param>
    private VGEllipse(VGEllipse ellipse)
      : base(
      ellipse.ShapeDrawAction,
      ellipse.Pen,
      ellipse.Brush,
      ellipse.Font,
      ellipse.FontColor,
      ellipse.Bounds,
      ellipse.StyleGroup,
      ellipse.Name,
      ellipse.ElementGroup,
      ellipse.Sound)
    {
      this.inverted = ellipse.Inverted;
    }

    #endregion //CONSTRUCTION

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Properties                                                       //
    ///////////////////////////////////////////////////////////////////////////////
    #region PROPERTIES

    /// <summary>
    /// Gets or sets a value indicating whether this ellipse should be drawn
    /// inverted, that is the fill fills the region outside this ellipse.
    /// </summary>
    public bool Inverted
    {
      get { return this.inverted; }
      set { this.inverted = value; }
    }

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

    /// <summary>
    /// Overridden <see cref="VGElement.Draw(Graphics)"/>.  
    /// Draws the ellipse to the given graphics context.
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
            GraphicsPath path = new GraphicsPath();
            path.AddEllipse(this.Bounds);
            Region region = new Region();
            region.MakeEmpty();
            region.Union(graphics.Clip);
            region.Exclude(path);
            graphics.FillRegion(this.Brush, region);
            region.Dispose();
          }
          else
          {
            graphics.FillEllipse(Brush, innerBounds);
          }
        }
      }

      // Draw ellipse
      if ((ShapeDrawAction & ShapeDrawAction.Edge) == ShapeDrawAction.Edge)
      {
        RectangleF halfInlineRect = this.Bounds;
        halfInlineRect.Inflate(-this.Pen.Width / 2, -this.Pen.Width / 2);

        graphics.DrawEllipse(Pen, halfInlineRect);
      }

      // Draw name and selection frame if applicable
      base.Draw(graphics);
    }

    /// <summary>
    /// Overridden <see cref="VGElement.Contains(PointF)"/>. 
    /// Detects if given point is in ellipse region.
    /// </summary>
    /// <param name="pt">Check Point</param>
    /// <returns><strong>True</strong> if point is in ellipse, otherwise <strong>false</strong></returns>
    public override bool Contains(PointF pt)
    {
      RectangleF bigBounds = this.Bounds;
      if (this.IsInEditMode)
      {
        bigBounds.Inflate(GrabHandle.HANDLESIZE, GrabHandle.HANDLESIZE);
        return bigBounds.Contains(pt);
      }

      GraphicsPath ellipsePath = new GraphicsPath();
      ellipsePath.AddEllipse(bigBounds);

      return ellipsePath.IsVisible(pt);
    }

    /// <summary>
    /// Overridden <see cref="VGElement.Contains(PointF, Int32)"/>. 
    /// Detects if given point is in element region widened by the tolerance value.
    /// </summary>
    /// <param name="pt">Point to check.</param>
    /// <param name="tolerance">An <see cref="int"/> tolerance value for 
    /// widening areas of interest to get a better hit rate in pixel.</param>
    /// <returns><strong>True</strong> if point is in region, otherwise <strong>false</strong></returns>
    public override bool Contains(PointF pt, int tolerance)
    {
      RectangleF bigBounds = this.Bounds;
      bigBounds.Inflate(tolerance, tolerance);

      GraphicsPath ellipsePath = new GraphicsPath();
      ellipsePath.AddEllipse(bigBounds);

      return ellipsePath.IsVisible(pt);
    }

    /// <summary>
    /// Overridden <see cref="Object.ToString()"/> method.
    /// Returns the <see cref="VGEllipse"/> properties as a human readable string.
    /// </summary>
    /// <returns>A <see cref="string"/> that represents this <see cref="VGEllipse"/>.</returns>
    public override string ToString()
    {
      StringBuilder sb = new StringBuilder();
      sb.Append("VGEllipse, Name: ");
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
    /// Returns the main <see cref="VGEllipse"/> properties as a human readable string.
    /// </summary>
    /// <returns>A <see cref="string"/> that represents the 
    /// current <see cref="VGEllipse"/> in short form with its main properties.</returns>
    public override string ToShortString()
    {
      StringBuilder sb = new StringBuilder();
      sb.Append("Ellipse ");
      sb.Append(this.Name);
      sb.Append(", Draw: ");
      sb.Append(this.ShapeDrawAction.ToString());
      return sb.ToString();
    }

    /// <summary>
    /// Overridden <see cref="VGElement.CloneCore()"/> method. 
    /// Creates a excact copy of given ellipse
    /// </summary>
    /// <returns>Excact copy of this ellipse</returns>
    protected override VGElement CloneCore()
    {
      return new VGEllipse(this);
    }

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
