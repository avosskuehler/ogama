// <copyright file="VGStyleGroup.cs" company="FU Berlin">
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

  /// <summary>
  /// List of vector graphics groups, that are used in Ogamas pictures
  /// </summary>
  [Flags]
  public enum VGStyleGroup
  {
    /// <summary>
    /// No Group specified.
    /// </summary>
    None = 0,

    /// <summary>
    /// Areas of interest module. Standard elements.
    /// </summary>
    AOI_NORMAL = 1,

    /// <summary>
    /// Areas of interest module. Target elements.
    /// </summary>
    AOI_TARGET = 2,

    /// <summary>
    /// Areas of interest module. Search rectangle elements.
    /// </summary>
    AOI_SEARCHRECT = 4,

    /// <summary>
    /// Replay Module. Pen for gaze cursor.
    /// </summary>
    RPL_PEN_GAZE_CURSOR = 8,

    /// <summary>
    /// Replay Module. Pen for gaze path.
    /// </summary>
    RPL_PEN_GAZE_PATH = 16,

    /// <summary>
    /// Replay Module. Pen for fixation circles.
    /// </summary>
    RPL_PEN_GAZE_FIX = 32,

    /// <summary>
    /// Replay Module. Pen for fixation connections.
    /// </summary>
    RPL_PEN_GAZE_FIXCON = 64,

    /// <summary>
    /// Replay Module. Pen for lines that show nodata connections. e.g. blinks.
    /// </summary>
    RPL_PEN_GAZE_NODATA = 128,

    /// <summary>
    /// Replay Module. Pen for mouse cursor.
    /// </summary>
    RPL_PEN_MOUSE_CURSOR = 256,

    /// <summary>
    /// Replay Module. Pen for mouse path.
    /// </summary>
    RPL_PEN_MOUSE_PATH = 512,

    /// <summary>
    /// Replay Module. Pen for mouse fixations.
    /// </summary>
    RPL_PEN_MOUSE_FIX = 1024,

    /// <summary>
    /// Replay Module. Pen for mouse fixation connections.
    /// </summary>
    RPL_PEN_MOUSE_FIXCON = 2048,

    /// <summary>
    /// Replay module. Click elements.
    /// </summary>
    RPL_MOUSE_CLICK = 4096,

    /// <summary>
    /// Fixations Module. Standard gaze elements.
    /// </summary>
    FIX_GAZE_ELEMENT = 8192,

    /// <summary>
    /// Fixations Module. Standard mouse elements.
    /// </summary>
    FIX_MOUSE_ELEMENT = 16384,

    /// <summary>
    /// Scanpaths Module. Grid rectangle elements.
    /// </summary>
    SCA_GRID_RECTANGLE = 32768,

    /// <summary>
    /// Scanpaths Module. AOI elements.
    /// </summary>
    SCA_GRID_AOI = 65536,

    /// <summary>
    /// AOI module. Statistical bubbles.
    /// </summary>
    AOI_STATISTICS_BUBBLE = 131072,

    /// <summary>
    /// AOI module. Statistical transitions.
    /// </summary>
    AOI_STATISTICS_ARROW = 262144,

    /// <summary>
    /// All modules. ActiveX elements.
    /// </summary>
    ACTIVEX = 524288,
  }
}