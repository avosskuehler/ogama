// <copyright file="GazetrackerIPClientStatus.cs" company="FU Berlin">
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

namespace Ogama.Modules.Recording.GazegroupInterface
{
  /// <summary>
  /// The different status values the connection to the gazetracker client
  /// application can have.
  /// </summary>
  public struct GazetrackerIPClientStatus
  {
    /// <summary>
    /// Indicates the connections status of the ip client.
    /// </summary>
    public bool IsConnected;

    /// <summary>
    /// Indicates the streaming state of the ip client.
    /// </summary>
    public bool IsStreaming;

    /// <summary>
    /// Indicates the calibration state of the ip client.
    /// </summary>
    public bool IsCalibrated;

    /// <summary>
    /// Resets all flags to false.
    /// </summary>
    public void Reset()
    {
      this.IsCalibrated = false;
      this.IsConnected = false;
      this.IsStreaming = false;
    }
  }
}
