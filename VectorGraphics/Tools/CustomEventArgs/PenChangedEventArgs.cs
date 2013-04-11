// <copyright file="PenChangedEventArgs.cs" company="FU Berlin">
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
  /// Class that contains the data for the PenChanged event. 
  /// Derives from <see cref="System.EventArgs"/>.
  /// </summary>
  public class PenChangedEventArgs : EventArgs
  {
    ///////////////////////////////////////////////////////////////////////////////
    // Defining Variables, Enumerations, Events                                  //
    ///////////////////////////////////////////////////////////////////////////////
    #region FIELDS

    /// <summary>
    /// This is the new created pen.
    /// </summary>
    private readonly Pen pen;

    /// <summary>
    /// This is the vector graphics
    /// group to which the new pen should be applied.
    /// </summary>
    private VGStyleGroup penGroup;

    #endregion FIELDS

    ///////////////////////////////////////////////////////////////////////////////
    // Construction and Initializing methods                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region CONSTRUCTION
    
    /// <summary>
    /// Initializes a new instance of the PenChangedEventArgs class.
    /// </summary>
    /// <param name="newPen">This is the new created pen.</param>
    /// <param name="newGroup">vector graphics
    /// group to which the new pen should be applied.</param>
    public PenChangedEventArgs(Pen newPen, VGStyleGroup newGroup)
    {
      this.pen = newPen;
      this.penGroup = newGroup;
    }

    #endregion //CONSTRUCTION

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Properties                                                       //
    ///////////////////////////////////////////////////////////////////////////////
    #region PROPERTIES

    /// <summary>
    /// Gets the Pen property. This is the new created pen.
    /// </summary>
    public Pen Pen
    {
      get { return this.pen; }
    }

    /// <summary>
    /// Gets or sets the PenGroup property. This is the vector graphics
    /// group to which the new pen should be applied.
    /// </summary>
    public VGStyleGroup ElementGroup
    {
      get { return this.penGroup; }
      set { this.penGroup = value; }
    }

    #endregion //PROPERTIES
  }
}
