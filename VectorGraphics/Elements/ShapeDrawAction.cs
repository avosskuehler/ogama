// <copyright file="ShapeDrawAction.cs" company="FU Berlin">
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

  /// <summary>
  /// This enumeration specifies what to draw from
  /// a vector graphic element: edge, fill, name or all.
  /// </summary>
  [Flags]
  public enum ShapeDrawAction
  {
    /// <summary>
    /// Nothing to draw...
    /// </summary>
    None = 0,

    /// <summary>
    /// Draw only edges of graphic object
    /// </summary>
    Edge = 1,

    /// <summary>
    /// Only fill the interior of the object with defined brush.
    /// </summary>
    Fill = 2,

    /// <summary>
    /// Draw both edge and fill.
    /// </summary>
    EdgeAndFill = 3,

    /// <summary>
    /// Draw the name of the element at the center.
    /// </summary>
    Name = 4,

    /// <summary>
    /// Draw the name and the edge
    /// </summary>
    NameAndEdge = 5,

    /// <summary>
    /// Draw the name and the fill
    /// </summary>
    NameAndFill = 6,

    /// <summary>
    /// Draw the name the edge and the fill
    /// </summary>
    NameEdgeFill = 7,
  }
}