// <copyright file="VGSharp.cs" company="FU Berlin">
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

namespace VectorGraphics.Elements
{
  using System;
  using System.Collections.Generic;
  using System.ComponentModel;
  using System.Drawing;
  using System.Drawing.Drawing2D;
  using System.Text;
  using System.Windows.Forms;
  using System.Xml.Serialization;

  using VectorGraphics.Tools;
  using VectorGraphics.Tools.CustomTypeConverter;

  /// <summary>
  /// Inherited from <see cref="VGElement"/>. Creates new vector graphics sharp.
  /// </summary>
  [Serializable]
  public class VGSharp : VGElement
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
    #endregion //FIELDS

    ///////////////////////////////////////////////////////////////////////////////
    // Construction and Initializing methods                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region CONSTRUCTION

    /// <summary>
    /// Initializes a new instance of the VGSharp class.
    /// </summary>
    /// <param name="newShapeDrawAction">Drawing action: Edge, Fill, Both</param>
    /// <param name="newPen">Pen for edges</param>
    public VGSharp(ShapeDrawAction newShapeDrawAction, Pen newPen)
      : base(newShapeDrawAction, newPen)
    {
      this.Bounds = Rectangle.Empty;
    }

    /// <summary>
    /// Initializes a new instance of the VGSharp class.
    /// </summary>
    /// <param name="newShapeDrawAction">Drawing action: Edge, Fill, Both</param>
    /// <param name="newPen">Pen for edges</param>
    /// <param name="newBounds">bounding rectangle</param>
    public VGSharp(ShapeDrawAction newShapeDrawAction, Pen newPen, RectangleF newBounds)
      : base(newShapeDrawAction, newPen, newBounds)
    {
    }

    /// <summary>
    /// Initializes a new instance of the VGSharp class.
    /// </summary>
    /// <param name="newShapeDrawAction">Drawing action: Edge, Fill, Both</param>
    /// <param name="newPen">Pen for edges</param>
    /// <param name="newBounds">bounding rectangle</param>
    /// <param name="newStyleGroup">Group Enumeration, <see cref="VGStyleGroup"/></param>
    /// <param name="newName">Name of Element</param>
    /// <param name="newElementGroup">Element group description</param>
    public VGSharp(
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
    /// Initializes a new instance of the VGSharp class.
    /// </summary>
    /// <param name="newShapeDrawAction">Drawing action: Edge, Fill, Both</param>
    /// <param name="newPen">Pen for edges</param>
    /// <param name="newFont">Font for text</param>
    /// <param name="newFontColor">Color for text</param>
    /// <param name="newBounds">bounding rectangle</param>
    /// <param name="newStyleGroup">Group Enumeration, <see cref="VGStyleGroup"/></param>
    /// <param name="newName">Name of Element</param>
    /// <param name="newElementGroup">Element group description</param>
    public VGSharp(
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
    /// Initializes a new instance of the VGSharp class.
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
    public VGSharp(
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
    /// Initializes a new instance of the VGSharp class.
    /// </summary>
    /// <param name="newShapeDrawAction">Drawing action: Edge, Fill, Both</param>
    /// <param name="newPen">Pen for edges</param>
    /// <param name="newStyleGroup">Group Enumeration, <see cref="VGStyleGroup"/></param>
    /// <param name="newName">Name of Element</param>
    /// <param name="newElementGroup">Element group description</param>
    public VGSharp(
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
    /// Initializes a new instance of the VGSharp class.
    /// </summary>
    /// <param name="newShapeDrawAction">Drawing action: Edge, Fill, Both</param>
    /// <param name="newBrush">Brush for fills</param>
    /// <param name="newBounds">bounding rectangle</param>
    public VGSharp(ShapeDrawAction newShapeDrawAction, Brush newBrush, RectangleF newBounds)
      : base(newShapeDrawAction, newBrush, newBounds)
    {
    }

    /// <summary>
    /// Initializes a new instance of the VGSharp class.
    /// </summary>
    /// <param name="newShapeDrawAction">Drawing action: Edge, Fill, Both</param>
    /// <param name="newBrush">Brush for fills</param>
    /// <param name="newBounds">bounding rectangle</param>
    /// <param name="newStyleGroup">Group Enumeration, <see cref="VGStyleGroup"/></param>
    /// <param name="newName">Name of Element</param>
    /// <param name="newElementGroup">Element group description</param>
    public VGSharp(
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
    /// Prevents a default instance of the VGSharp class from being created.
    /// Parameterless constructor. Used for serialization.
    /// </summary>
    private VGSharp()
    {
    }

    /// <summary>
    /// Initializes a new instance of the VGSharp class.
    /// Clone Constructor. Creates new sharp that is
    /// identical to the given <see cref="VGSharp"/>
    /// </summary>
    /// <param name="sharp">Sharp to clone</param>
    private VGSharp(VGSharp sharp)
      : base(
      sharp.ShapeDrawAction,
      sharp.Pen,
      sharp.Brush,
      sharp.Font,
      sharp.FontColor,
      sharp.Bounds,
      sharp.StyleGroup,
      sharp.Name,
      sharp.ElementGroup,
      sharp.Sound)
    {
    }

    #endregion //CONSTRUCTION

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Properties                                                       //
    ///////////////////////////////////////////////////////////////////////////////
    #region PROPERTIES
    #endregion //PROPERTIES

    ///////////////////////////////////////////////////////////////////////////////
    // Public methods                                                            //
    ///////////////////////////////////////////////////////////////////////////////
    #region PUBLICMETHODS
    #endregion //PUBLICMETHODS

    ///////////////////////////////////////////////////////////////////////////////
    // Inherited methods                                                         //
    ///////////////////////////////////////////////////////////////////////////////
    #region OVERRIDES

    /// <summary>
    /// Overridden <see cref="VGElement.Draw(Graphics)"/> method. 
    /// Draws the sharp to the given graphics context.
    /// </summary>
    /// <param name="graphics">Graphics context to draw to</param>
    /// <exception cref="ArgumentNullException">Thrown, when graphics object is null.</exception>
    public override void Draw(Graphics graphics)
    {
      if (graphics == null)
      {
        throw new ArgumentNullException("Graphics object should not be null.");
      }

      float halfPenWidth = this.Pen.Width / 2;
      if ((ShapeDrawAction & ShapeDrawAction.Fill) == ShapeDrawAction.Fill)
      {
        graphics.FillRectangle(this.Brush, this.Bounds);
      }

      if ((ShapeDrawAction & ShapeDrawAction.Edge) == ShapeDrawAction.Edge)
      {
        float middleX = this.Bounds.X + this.Bounds.Width / 2 - halfPenWidth;

        graphics.DrawLine(
          this.Pen,
          new PointF(middleX, this.Bounds.Top),
          new PointF(middleX, this.Bounds.Bottom));

        float middleY = this.Bounds.Y + this.Bounds.Height / 2 - halfPenWidth;

        graphics.DrawLine(
          this.Pen,
          new PointF(this.Bounds.Left, middleY),
          new PointF(this.Bounds.Right, middleY));
      }

      // Draw name and selection frame if applicable
      base.Draw(graphics);
    }

    /// <summary>
    /// Overridden <see cref="Object.ToString()"/> method.
    /// Returns the <see cref="VGSharp"/> properties as a human readable string.
    /// </summary>
    /// <returns>A <see cref="string"/> that represents this <see cref="VGSharp"/>.</returns>
    public override string ToString()
    {
      StringBuilder sb = new StringBuilder();
      sb.Append("VGSharp, Name: ");
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
    /// Returns the main <see cref="VGSharp"/> properties as a human readable string.
    /// </summary>
    /// <returns>A <see cref="string"/> that represents the 
    /// current <see cref="VGSharp"/> in short form with its main properties.</returns>
    public override string ToShortString()
    {
      StringBuilder sb = new StringBuilder();
      sb.Append("Sharp ");
      sb.Append(this.Name);
      sb.Append(", Draw: ");
      sb.Append(this.ShapeDrawAction.ToString());
      return sb.ToString();
    }

    /// <summary>
    /// Overridden <see cref="VGElement.CloneCore()"/> method. 
    /// Creates a excact copy of given sharp
    /// </summary>
    /// <returns>Excact copy of this sharp</returns>
    protected override VGElement CloneCore()
    {
      return new VGSharp(this);
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
