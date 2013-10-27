// <copyright file="VGShapeType.cs" company="FU Berlin">
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
  /// Type of selected shape to be drawn or created.
  /// Can be None, Rectangle, Ellipse, Polyline, Text, Image, Cursor, Line.
  /// </summary>
  public enum VGShapeType
  {
    /// <summary>
    /// Undefined shape type.
    /// </summary>
    None,

    /// <summary>
    /// Rectangular shape.
    /// </summary>
    Rectangle,

    /// <summary>
    /// Ellipsoid shape.
    /// </summary>
    Ellipse,

    /// <summary>
    /// Polyline shape.
    /// </summary>
    Polyline,

    /// <summary>
    /// Textual shape/object.
    /// </summary>
    Text,

    /// <summary>
    /// Image shape/object.
    /// </summary>
    Image,

    /// <summary>
    /// Cursor shape/object.
    /// </summary>
    Cursor,

    /// <summary>
    /// Line shape.
    /// </summary>
    Line,
  }
}
