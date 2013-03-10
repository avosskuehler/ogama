// <copyright file="BrushChangedEventArgs.cs" company="FU Berlin">
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
  /// Class that contains the data for the BrushChanged event. Derives from <see cref="System.EventArgs"/>.
  /// </summary>
  public class BrushChangedEventArgs : EventArgs
  {
    ///////////////////////////////////////////////////////////////////////////////
    // Defining Variables, Enumerations, Events                                  //
    ///////////////////////////////////////////////////////////////////////////////
    #region FIELDS

    /// <summary>
    /// The new <see cref="Brush"/>.
    /// </summary>
    private readonly Brush brush;

    /// <summary>
    /// The <see cref="VGStyleGroup"/> of the new <see cref="Brush"/>
    /// </summary>
    private VGStyleGroup brushGroup;

    #endregion FIELDS

    ///////////////////////////////////////////////////////////////////////////////
    // Construction and Initializing methods                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region CONSTRUCTION

    /// <summary>
    /// Initializes a new instance of the BrushChangedEventArgs class.
    /// </summary>
    /// <param name="newBrush">The newly created <see cref="Brush"/>.</param>
    /// <param name="newGroup">vector graphics
    /// group to which the new brush should be applied.</param>
    public BrushChangedEventArgs(Brush newBrush, VGStyleGroup newGroup)
    {
      this.brush = newBrush;
      this.brushGroup = newGroup;
    }

    #endregion //CONSTRUCTION

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Properties                                                       //
    ///////////////////////////////////////////////////////////////////////////////
    #region PROPERTIES

    /// <summary>
    /// Gets the Brush property. This is the new created brush.
    /// </summary>
    public Brush Brush
    {
      get { return this.brush; }
    }

    /// <summary>
    /// Gets or sets the ElementGroup property. This is the vector graphics
    /// group to which the new brush should be applied.
    /// </summary>
    public VGStyleGroup ElementGroup
    {
      get { return this.brushGroup; }
      set { this.brushGroup = value; }
    }

    #endregion //PROPERTIES
  }
}
