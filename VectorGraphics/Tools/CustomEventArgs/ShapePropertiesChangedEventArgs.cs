// <copyright file="ShapePropertiesChangedEventArgs.cs" company="FU Berlin">
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

namespace VectorGraphics.Tools.CustomEventArgs
{
  using System;
  using System.Drawing;

  using VectorGraphics.Elements;

  /// <summary>
  /// Class that contains the data for the ShapePropertiesChanged event.
  /// Derives from <see cref="System.EventArgs"/>.
  /// </summary>
  public class ShapePropertiesChangedEventArgs : EventArgs
  {
    ///////////////////////////////////////////////////////////////////////////////
    // Defining Variables, Enumerations, Events                                  //
    ///////////////////////////////////////////////////////////////////////////////
    #region FIELDS

    /// <summary>
    /// New <see cref="ShapeDrawAction"/> that should be used.
    /// </summary>
    private readonly ShapeDrawAction drawAction;

    /// <summary>
    /// New <see cref="Pen"/> for the element.
    /// </summary>
    private readonly Pen pen;

    /// <summary>
    /// New <see cref="Brush"/> for the element.
    /// </summary>
    private readonly Brush brush;

    /// <summary>
    /// New <see cref="Font"/> for the element.
    /// </summary>
    private readonly Font font;

    /// <summary>
    /// New font <see cref="Color"/> for the element.
    /// </summary>
    private readonly Color fontColor;

    /// <summary>
    /// New elements name as a <see cref="String"/>.
    /// </summary>
    private readonly string name;

    /// <summary>
    /// New elements <see cref="VGAlignment"/>.
    /// </summary>
    private readonly VGAlignment newFontAlignment;
    
    #endregion FIELDS

    ///////////////////////////////////////////////////////////////////////////////
    // Construction and Initializing methods                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region CONSTRUCTION

    /// <summary>
    /// Initializes a new instance of the ShapePropertiesChangedEventArgs class.
    /// </summary>
    /// <param name="newDrawAction">New <see cref="ShapeDrawAction"/> that should be used.</param>
    /// <param name="newPen">new <see cref="Pen"/> for the element.</param>
    /// <param name="newBrush">new <see cref="Brush"/> for the element.</param>
    /// <param name="newFont">new <see cref="Font"/> for the element.</param>
    /// <param name="newFontColor">new font <see cref="Color"/> for the element.</param>
    /// <param name="newName">new elements name.</param>
    /// <param name="newAlignment">new elements <see cref="VGAlignment"/>.</param>
    public ShapePropertiesChangedEventArgs(
      ShapeDrawAction newDrawAction,
      Pen newPen,
      Brush newBrush,
      Font newFont,
      Color newFontColor,
      string newName,
      VGAlignment newAlignment)
    {
      this.drawAction = newDrawAction;
      this.pen = newPen;
      this.brush = newBrush;
      this.font = newFont;
      this.fontColor = newFontColor;
      this.name = newName;
      this.newFontAlignment = newAlignment;
    }

    #endregion //CONSTRUCTION

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Properties                                                       //
    ///////////////////////////////////////////////////////////////////////////////
    #region PROPERTIES

    /// <summary>
    /// Gets the name.
    /// </summary>
    /// <value>The newly defined name.</value>
    public string NewName
    {
      get { return this.name; }
    }

    /// <summary>
    /// Gets the <see cref="Font"/>.
    /// </summary>
    /// <value>The newly defined <see cref="Font"/>.</value>
    public Font NewFont
    {
      get { return this.font; }
    }

    /// <summary>
    /// Gets the fonts <see cref="Color"/>.
    /// </summary>
    /// <value>The newly defined <see cref="Color"/> for the font.</value>
    public Color NewFontColor
    {
      get { return this.fontColor; }
    }

    /// <summary>
    /// Gets the Pen property. This is the new created pen.
    /// </summary>
    public Pen Pen
    {
      get { return this.pen; }
    }

    /// <summary>
    /// Gets the Brush property. This is the new created brush.
    /// </summary>
    public Brush Brush
    {
      get { return this.brush; }
    }

    /// <summary>
    /// Gets the ShapeDrawAction property. This can be
    /// None, Edge, Fill or Both.
    /// </summary>
    public ShapeDrawAction ShapeDrawAction
    {
      get { return this.drawAction; }
    }

    /// <summary>
    /// Gets the <see cref="VGAlignment"/> property. 
    /// </summary>
    public VGAlignment TextAlignment
    {
      get { return this.newFontAlignment; }
    }

    #endregion //PROPERTIES
  }
}
