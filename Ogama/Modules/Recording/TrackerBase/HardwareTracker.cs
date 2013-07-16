// <copyright file="HardwareTracker.cs" company="FU Berlin">
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

namespace Ogama.Modules.Recording.TrackerBase
{
  using System;

  /// <summary>
  /// Enumeration of optional installed tracking
  /// devices that can be used in OGAMAs recording module.
  /// </summary>
  [Flags]
  public enum HardwareTracker
  {
    /// <summary>
    /// No hardware tracker available
    /// </summary>
    None = 0,

    /// <summary>
    /// MouseOnly tracker available
    /// </summary>
    MouseOnly = 1,

    /// <summary>
    /// Tobii T60,T120,X60 gaze tracker available
    /// </summary>
    Tobii = 2,

    /// <summary>
    /// Alea IG-30 professional gaze tracker available
    /// </summary>
    Alea = 4,

    /// <summary>
    /// The Senso motoric instruments iViewX System based tracking.
    /// </summary>
    SMI = 8,

    /// <summary>
    /// The Gazegroup GazeTracker using the ogama client.
    /// </summary>
    GazetrackerDirectClient = 16,

    /// <summary>
    /// The Gazegroup GazeTracker using an IP connection.
    /// </summary>
    GazetrackerIPClient = 32,

    /// <summary>
    /// Applied Science Laboratories (ASL) professional gaze tracker
    /// </summary>
    ASL = 64,

    /// <summary>
    /// Mirametrix S2 Tracker available
    /// </summary>
    Mirametrix = 128,

    /// <summary>
    /// The EyeTech gazetracker using quicklink
    /// </summary>
    EyeTech = 256,

  }
}
