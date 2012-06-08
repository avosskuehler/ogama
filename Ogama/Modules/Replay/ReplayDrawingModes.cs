// <copyright file="ReplayDrawingModes.cs" company="FU Berlin">
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

namespace Ogama.Modules.Replay
{
  using System;

  /// <summary>
  /// Drawing Modes for playback module.
  /// </summary>
  [Flags]
  public enum ReplayDrawingModes
  {
    /// <summary>
    /// No drawing mode specified
    /// </summary>
    None = 0,

    /// <summary>
    /// Draw a cursor.
    /// </summary>
    Cursor = 1,

    /// <summary>
    /// Draw a polyline consisting of the sample points.
    /// </summary>
    Path = 2,

    /// <summary>
    /// Draw fixation circles.
    /// </summary>
    Fixations = 4,

    /// <summary>
    /// Draw straight lines between fixation circles.
    /// </summary>
    FixationConnections = 8,

    /// <summary>
    /// Draw circle of original image over grayed image.
    /// </summary>
    Spotlight = 16,

    /// <summary>
    /// Draws mouse clicks.
    /// </summary>
    Clicks = 32,
  }
}
