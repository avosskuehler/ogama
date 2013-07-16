// <copyright file="SlideType.cs" company="FU Berlin">
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

namespace Ogama.Modules.Common.SlideCollections
{
  using System;

  /// <summary>
  /// This enumeration describes the different available slide types
  /// that are shown with an individual slide type image.
  /// </summary>
  [Flags]
  public enum SlideType
  {
    /// <summary>
    /// No slide type is set.
    /// </summary>
    None = 0,

    /// <summary>
    /// Slides containing only rectangles, ellipses, sharps, polylines etc.
    /// </summary>
    Shapes = 1,

    /// <summary>
    /// Slides containing only images.
    /// </summary>
    Images = 2,

    /// <summary>
    /// Slides containing only flash movies (swf files).
    /// </summary>
    Flash = 4,

    /// <summary>
    /// Slides containing only sound stimuli.
    /// </summary>
    Sound = 8,

    /// <summary>
    /// Slides containing only textual stimuli (RTF or default).
    /// </summary>
    Instructions = 16,

    /// <summary>
    /// Slides containing only an internet browser.
    /// </summary>
    Browser = 32,

    /// <summary>
    /// Slides used to capture the desktop
    /// </summary>
    Desktop = 64,
  }
}