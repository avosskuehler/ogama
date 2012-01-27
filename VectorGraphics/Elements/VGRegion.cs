// <copyright file="VGRegion.cs" company="FU Berlin">
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
  using System.Drawing;
  using System.Drawing.Drawing2D;
  using System.Text;
  using System.Windows.Forms;

  using VectorGraphics.Tools;
  using VectorGraphics.Tools.CustomTypeConverter;

  /// <summary>
  /// Inherited from <see cref="VGElement"/>. 
  /// A serializable class that is a vector graphics region,
  /// that can have a bounding line, a fill and a name.
  /// </summary>
  [Serializable]
  public class VGRegion : VGElement
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
    /// Indicates wheter this region should be drawn inverted.
    /// </summary>
    private bool inverted;

    /// <summary>
    /// Saves the drawing region of this VGRegion.
    /// </summary>
    private Region region;

    #endregion //FIELDS

    ///////////////////////////////////////////////////////////////////////////////
    // Construction and Initializing methods                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region CONSTRUCTION

    /// <summary>
    /// Initializes a new instance of the VGRegion class.
    /// </summary>
    /// <param name="newShapeDrawAction">Drawing action: Edge, Fill, Both</param>
    /// <param name="newPen">Pen for edges</param>
    /// <param name="newBrush">Brush for fills</param>
    /// <param name="newBounds">bounding rectangle</param>
    /// <param name="newStyleGroup">Group Enumeration, <see cref="VGStyleGroup"/></param>
    /// <param name="newName">Name of Element</param>
    /// <param name="newElementGroup">Element group description</param>
    public VGRegion(
      ShapeDrawAction newShapeDrawAction,
      Pen newPen,
      Brush newBrush,
      RectangleF newBounds,
      VGStyleGroup newStyleGroup,
      string newName,
      string newElementGroup)
      : base(newShapeDrawAction, newPen, newBrush, newBounds, newStyleGroup, newName, newElementGroup)
    {
      this.region = new Region();
      this.region.MakeEmpty();
      this.region.Union(newBounds);
    }

    /// <summary>
    /// Initializes a new instance of the VGRegion class.
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
    public VGRegion(
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
      this.region = new Region();
      this.region.MakeEmpty();
      this.region.Union(newBounds);
    }

    /// <summary>
    /// Initializes a new instance of the VGRegion class.
    /// </summary>
    /// <param name="newShapeDrawAction">Drawing action: Edge, Fill, Both</param>
    /// <param name="newPen">Pen for edges</param>
    public VGRegion(ShapeDrawAction newShapeDrawAction, Pen newPen)
      : base(newShapeDrawAction, newPen)
    {
      this.region = new Region();
      this.region.MakeEmpty();
    }

    /// <summary>
    /// Initializes a new instance of the VGRegion class.
    /// </summary>
    /// <param name="newShapeDrawAction">Drawing action: Edge, Fill, Both</param>
    /// <param name="newPen">Pen for edges</param>
    /// <param name="newStyleGroup">Group Enumeration, <see cref="VGStyleGroup"/></param>
    /// <param name="newName">Name of Element</param>
    /// <param name="newElementGroup">Element group description</param>
    public VGRegion(
      ShapeDrawAction newShapeDrawAction,
      Pen newPen,
      VGStyleGroup newStyleGroup,
      string newName,
      string newElementGroup)
      : base(newShapeDrawAction, newPen, newStyleGroup, newName, newElementGroup)
    {
      this.region = new Region();
      this.region.MakeEmpty();
    }

    /// <summary>
    /// Initializes a new instance of the VGRegion class.
    /// </summary>
    /// <param name="newShapeDrawAction">Drawing action: Edge, Fill, Both</param>
    /// <param name="newBrush">Brush for fills</param>
    public VGRegion(ShapeDrawAction newShapeDrawAction, Brush newBrush)
      : base(newShapeDrawAction, newBrush)
    {
      this.region = new Region();
      this.region.MakeEmpty();
    }

    /// <summary>
    /// Initializes a new instance of the VGRegion class.
    /// </summary>
    /// <param name="newShapeDrawAction">Drawing action: Edge, Fill, Both</param>
    /// <param name="newBrush">Brush for fills</param>
    /// <param name="newStyleGroup">Group Enumeration, <see cref="VGStyleGroup"/></param>
    /// <param name="newName">Name of Element</param>
    /// <param name="newElementGroup">Element group description</param>
    public VGRegion(
      ShapeDrawAction newShapeDrawAction,
      Brush newBrush,
      VGStyleGroup newStyleGroup,
      string newName,
      string newElementGroup)
      : base(newShapeDrawAction, newBrush, newStyleGroup, newName, newElementGroup)
    {
      this.region = new Region();
      this.region.MakeEmpty();
    }

    /// <summary>
    /// Initializes a new instance of the VGRegion class.
    /// </summary>
    /// <param name="newShapeDrawAction">Drawing action: Edge, Fill, Both</param>
    /// <param name="newPen">Pen for edges</param>
    /// <param name="newBounds">bounding rectangle</param>
    public VGRegion(ShapeDrawAction newShapeDrawAction, Pen newPen, RectangleF newBounds)
      : base(newShapeDrawAction, newPen, newBounds)
    {
      this.region = new Region();
      this.region.MakeEmpty();
      this.region.Union(newBounds);
    }

    /// <summary>
    /// Initializes a new instance of the VGRegion class.
    /// </summary>
    /// <param name="newShapeDrawAction">Drawing action: Edge, Fill, Both</param>
    /// <param name="newPen">Pen for edges</param>
    /// <param name="newBounds">bounding rectangle</param>
    /// <param name="newStyleGroup">Group Enumeration, <see cref="VGStyleGroup"/></param>
    /// <param name="newName">Name of Element</param>
    /// <param name="newElementGroup">Element group description</param>
    public VGRegion(
      ShapeDrawAction newShapeDrawAction,
      Pen newPen,
      RectangleF newBounds,
      VGStyleGroup newStyleGroup,
      string newName,
      string newElementGroup)
      : base(newShapeDrawAction, newPen, newBounds, newStyleGroup, newName, newElementGroup)
    {
      this.region = new Region();
      this.region.MakeEmpty();
      this.region.Union(newBounds);
    }

    /// <summary>
    /// Initializes a new instance of the VGRegion class.
    /// </summary>
    /// <param name="newShapeDrawAction">Drawing action: Edge, Fill, Both</param>
    /// <param name="newPen">Pen for edges</param>
    /// <param name="newFont">Font for text</param>
    /// <param name="newFontColor">Color for text</param>
    /// <param name="newBounds">bounding rectangle</param>
    /// <param name="newStyleGroup">Group Enumeration, <see cref="VGStyleGroup"/></param>
    /// <param name="newName">Name of Element</param>
    /// <param name="newElementGroup">Element group description</param>
    public VGRegion(
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
      this.region = new Region();
      this.region.MakeEmpty();
      this.region.Union(newBounds);
    }

    /// <summary>
    /// Initializes a new instance of the VGRegion class.
    /// </summary>
    /// <param name="newShapeDrawAction">Drawing action: Edge, Fill, Both</param>
    /// <param name="newBrush">Brush for fills</param>
    /// <param name="newBounds">bounding rectangle</param>
    public VGRegion(ShapeDrawAction newShapeDrawAction, Brush newBrush, RectangleF newBounds)
      : base(newShapeDrawAction, newBrush, newBounds)
    {
      this.region = new Region();
      this.region.MakeEmpty();
      this.region.Union(newBounds);
    }

    /// <summary>
    /// Initializes a new instance of the VGRegion class.
    /// </summary>
    /// <param name="newShapeDrawAction">Drawing action: Edge, Fill, Both</param>
    /// <param name="newBrush">Brush for fills</param>
    /// <param name="newBounds">bounding rectangle</param>
    /// <param name="newStyleGroup">Group Enumeration, <see cref="VGStyleGroup"/></param>
    /// <param name="newName">Name of Element</param>
    /// <param name="newElementGroup">Element group description</param>
    public VGRegion(
      ShapeDrawAction newShapeDrawAction,
      Brush newBrush,
      RectangleF newBounds,
      VGStyleGroup newStyleGroup,
      string newName,
      string newElementGroup)
      : base(newShapeDrawAction, newBrush, newBounds, newStyleGroup, newName, newElementGroup)
    {
      this.region = new Region();
      this.region.MakeEmpty();
      this.region.Union(newBounds);
    }

    /// <summary>
    /// Initializes a new instance of the VGRegion class.
    /// </summary>
    /// <param name="newShapeDrawAction">Drawing action: Edge, Fill, Both</param>
    /// <param name="newBrush">Brush for fills</param>
    /// <param name="newFont">Font for text</param>
    /// <param name="newFontColor">Color for text</param>
    /// <param name="newBounds">bounding rectangle</param>
    /// <param name="newStyleGroup">Group Enumeration, <see cref="VGStyleGroup"/></param>
    /// <param name="newName">Name of Element</param>
    /// <param name="newElementGroup">Element group description</param>
    public VGRegion(
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
      this.region = new Region();
      this.region.MakeEmpty();
      this.region.Union(newBounds);
    }

    /// <summary>
    /// Prevents a default instance of the VGRegion class from being created.
    /// Parameterless constructor. Used for serialization.
    /// </summary>
    private VGRegion()
    {
      this.region = new Region();
      this.region.MakeEmpty();
    }

    /// <summary>
    /// Initializes a new instance of the VGRegion class.
    /// Clone Constructor. Creates new region element that is
    /// identical to the given VGRegion.
    /// </summary>
    /// <param name="regionToClone"><see cref="VGRegion"/> to clone</param>
    private VGRegion(VGRegion regionToClone)
      : base(
      regionToClone.ShapeDrawAction,
      regionToClone.Pen,
      regionToClone.Brush,
      regionToClone.Font,
      regionToClone.FontColor,
      regionToClone.Bounds,
      regionToClone.StyleGroup,
      regionToClone.Name,
      regionToClone.ElementGroup,
      regionToClone.Sound)
    {
      this.region = (Region)regionToClone.Region.Clone();
      this.inverted = regionToClone.Inverted;
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

    /// <summary>
    /// Gets or sets the drawing region represented by a <see cref="Region"/> of this VGRegion.
    /// </summary>
    public Region Region
    {
      get { return this.region; }
      set { this.region = value; }
    }

    /// <summary>
    /// Gets or sets the bounding rectangle for this <see cref="VGRegion"/>.
    /// </summary>
    public override RectangleF Bounds
    {
      get
      {
        return base.Bounds;
      }

      set
      {
        RectangleF currentBounds = this.Bounds;
        Matrix transform = new Matrix();
        transform.Scale(value.Width / currentBounds.Width, value.Height / currentBounds.Height);
        this.region.Transform(transform);
      }
    }

    #endregion //PROPERTIES

    ///////////////////////////////////////////////////////////////////////////////
    // Public methods                                                            //
    ///////////////////////////////////////////////////////////////////////////////
    #region PUBLICMETHODS

    /// <summary>
    /// This method adds an ellipse to the region.
    /// </summary>
    /// <param name="ellipseBounds">A <see cref="RectangleF"/> with the 
    /// ellipse bounds.</param>
    public void AddEllipse(RectangleF ellipseBounds)
    {
      GraphicsPath ellipsePath = new GraphicsPath();
      ellipsePath.AddEllipse(ellipseBounds);
      this.region.Union(ellipsePath);
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

      if ((ShapeDrawAction & ShapeDrawAction.Fill) == ShapeDrawAction.Fill)
      {
        if (this.Brush != null)
        {
          if (this.inverted)
          {
            Region drawRegion = new Region();
            drawRegion.MakeEmpty();
            drawRegion.Union(graphics.Clip);
            drawRegion.Exclude(this.region);
            graphics.FillRegion(this.Brush, drawRegion);
          }
          else
          {
            graphics.FillRegion(this.Brush, this.region);
          }
        }
      }

      // Draw name and selection frame if applicable
      base.Draw(graphics);
    }

    /// <summary>
    /// Overridden <see cref="Object.ToString()"/> method.
    /// Returns the <see cref="VGRegion"/> properties as a human readable string.
    /// </summary>
    /// <returns>A <see cref="string"/> that represents this <see cref="VGRegion"/>.</returns>
    public override string ToString()
    {
      StringBuilder sb = new StringBuilder();
      sb.Append("VGRegion, Name: ");
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
      sb.Append("Region ");
      sb.Append(this.Name);
      sb.Append(", Draw: ");
      sb.Append(this.ShapeDrawAction.ToString());
      return sb.ToString();
    }

    /// <summary>
    /// Overridden <see cref="VGElement.CloneCore()"/> method. 
    /// Creates a excact copy of given region
    /// </summary>
    /// <returns>Excact copy of this region</returns>
    protected override VGElement CloneCore()
    {
      return new VGRegion(this);
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
