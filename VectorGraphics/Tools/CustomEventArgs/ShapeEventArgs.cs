// <copyright file="ShapeEventArgs.cs" company="FU Berlin">
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

namespace VectorGraphics.Tools.CustomEventArgs
{
  using System;

  using VectorGraphics.Elements;

  /// <summary>
  /// Delegate. Handles shape events.
  /// </summary>
  /// <param name="sender">Source of the event.</param>
  /// <param name="e">A <see cref="ShapeEventArgs"/> with the <see cref="VGElement"/> shape.</param>
  public delegate void ShapeEventHandler(object sender, ShapeEventArgs e);

  /// <summary>
  /// Class that contains the data for shape events. Derives from <see cref="System.EventArgs"/>.
  /// </summary>
  public class ShapeEventArgs : EventArgs
  {
    ///////////////////////////////////////////////////////////////////////////////
    // Defining Variables, Enumerations, Events                                  //
    ///////////////////////////////////////////////////////////////////////////////
    #region FIELDS

    /// <summary>
    /// Saves the <see cref="VGElement"/> that is sent within
    /// this <see cref="ShapeEventArgs"/>.
    /// </summary>
    private readonly VGElement shape;

    #endregion //FIELDS

    ///////////////////////////////////////////////////////////////////////////////
    // Construction and Initializing methods                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region CONSTRUCTION

    /// <summary>
    /// Initializes a new instance of the ShapeEventArgs class.
    /// </summary>
    /// <param name="shape">The <see cref="VGElement"/> to store.</param>
    public ShapeEventArgs(VGElement shape)
    {
      this.shape = shape;
    }

    #endregion //CONSTRUCTION

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Properties                                                       //
    ///////////////////////////////////////////////////////////////////////////////
    #region PROPERTIES

    /// <summary>
    /// Gets the whole original shape object
    /// </summary>
    public VGElement Shape
    {
      get { return this.shape; }
    }

    #endregion //PROPERTIES
  }
}
