// <copyright file="VGAlignment.cs" company="FU Berlin">
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
  /// <summary>
  /// This enumeration specifies how to align 
  /// name of elements.
  /// </summary>
  public enum VGAlignment
  {
    /// <summary>
    /// No alignment
    /// </summary>
    None,

    /// <summary>
    /// Draw at the top of the bounds.
    /// </summary>
    Top,

    /// <summary>
    /// Draw at the left side of the bounds.
    /// </summary>
    Left,

    /// <summary>
    /// Draw at the right side of the bounds.
    /// </summary>
    Right,

    /// <summary>
    /// Draw at the bottom of the bounds.
    /// </summary>
    Bottom,

    /// <summary>
    /// Draw at the center of the bounds.
    /// </summary>
    Center,
  }
}