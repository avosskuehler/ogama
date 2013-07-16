// <copyright file="EventType.cs" company="FU Berlin">
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

namespace Ogama.Modules.Common.TrialEvents
{
  /// <summary>
  /// This enumeration describes the possible event types that occur
  /// during an ogama presentation.
  /// This can be input types like mouse and key inputs
  /// or media types like image, flash, audio, video and usercam events.
  /// </summary>
  public enum EventType
  {
    /// <summary>
    /// No event at all.
    /// </summary>
    None,

    /// <summary>
    /// Mouse events.
    /// </summary>
    Mouse,

    /// <summary>
    /// Key events.
    /// </summary>
    Key,

    /// <summary>
    /// Slide events.
    /// </summary>
    Slide,

    /// <summary>
    /// Shockwave flash movie events.
    /// </summary>
    Flash,

    /// <summary>
    /// Audio events.
    /// </summary>
    Audio,

    /// <summary>
    /// Video events.
    /// </summary>
    Video,

    /// <summary>
    /// Usercam events.
    /// </summary>
    Usercam,

    /// <summary>
    /// Slide responses.
    /// </summary>
    Response,

    /// <summary>
    /// A time marker
    /// </summary>
    Marker,

    /// <summary>
    /// Scroll event marker for browser pages
    /// </summary>
    Scroll,

    /// <summary>
    /// Webpgage event marker for browser pages
    /// </summary>
    Webpage,
  }
}
