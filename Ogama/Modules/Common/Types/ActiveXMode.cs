// <copyright file="ActiveXMode.cs" company="FU Berlin">
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

namespace Ogama.Modules.Common.Types
{
  /// <summary>
  /// This enumeration indicates the mode in which to display
  /// ActiveX controls of slides in a picture.
  /// </summary>
  public enum ActiveXMode
  {
    /// <summary>
    /// The ActiveX objects are not initialized
    /// and shown at all.
    /// </summary>
    Off = 0,

    /// <summary>
    /// The ActiveX objects are initialized on the background
    /// of the picture to be drawn on the surface via a call
    /// to IViewObject.Draw.
    /// </summary>
    BehindPicture = 1,

    /// <summary>
    /// The ActiveX objects are shown on top of the picture
    /// to be used as they orginially were.
    /// </summary>
    OnTop = 2,

    /// <summary>
    /// The ActiveX objects and slide are not shown,
    /// instead a video of the recording should be used.
    /// </summary>
    Video = 4,
  }
}
