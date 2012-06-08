// <copyright file="FontChangedEventArgs.cs" company="FU Berlin">
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
  /// Class that contains the data for the FontChanged event. 
  /// Derives from <see cref="System.EventArgs"/>.
  /// </summary>
  public class FontChangedEventArgs : EventArgs
  {
    ///////////////////////////////////////////////////////////////////////////////
    // Defining Variables, Enumerations, Events                                  //
    ///////////////////////////////////////////////////////////////////////////////
    #region FIELDS

    /// <summary>
    /// The newly created <see cref="Font"/>
    /// </summary>
    private readonly Font font;

    /// <summary>
    /// The <see cref="Color"/> for the newly created font.
    /// </summary>
    private readonly Color fontColor;

    /// <summary>
    /// The <see cref="VGAlignment"/> for the newly created font.
    /// </summary>
    private readonly VGAlignment fontAlignment;

    /// <summary>
    /// This is the vector graphics
    /// group to which the new font should be applied.
    /// </summary>
    private VGStyleGroup elementGroup;

    #endregion FIELDS

    ///////////////////////////////////////////////////////////////////////////////
    // Construction and Initializing methods                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region CONSTRUCTION

    /// <summary>
    /// Initializes a new instance of the FontChangedEventArgs class.
    /// </summary>
    /// <param name="newFont">The newly created <see cref="Font"/></param>
    /// <param name="newColor">The <see cref="Color"/> for the newly created font.</param>
    /// <param name="newAlignment">The <see cref="VGAlignment"/> for the newly created font.</param>
    /// <param name="newGroup">vector graphics
    /// group to which the new font should be applied.</param>
    public FontChangedEventArgs(Font newFont, Color newColor, VGAlignment newAlignment, VGStyleGroup newGroup)
    {
      this.font = newFont;
      this.fontColor = newColor;
      this.fontAlignment = newAlignment;
      this.elementGroup = newGroup;
    }

    #endregion //CONSTRUCTION

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Properties                                                       //
    ///////////////////////////////////////////////////////////////////////////////
    #region PROPERTIES

    /// <summary>
    /// Gets the Font property. This is the new created font.
    /// </summary>
    public Font Font
    {
      get { return this.font; }
    }

    /// <summary>
    /// Gets the Color property. This is the new created font color.
    /// </summary>
    public Color FontColor
    {
      get { return this.fontColor; }
    }

    /// <summary>
    /// Gets the FontAlignment property. This is the new created fonts text alignment value.
    /// </summary>
    public VGAlignment FontAlignment
    {
      get { return this.fontAlignment; }
    }

    /// <summary>
    /// Gets or sets the ElementGroup property. This is the vector graphics
    /// group to which the new font should be applied.
    /// </summary>
    public VGStyleGroup ElementGroup
    {
      get { return this.elementGroup; }
      set { this.elementGroup = value; }
    }

    #endregion //PROPERTIES
  }
}
