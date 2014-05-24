// <copyright file="ISMIClient.cs" company="FU Berlin">
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
  using Ogama.Modules.Common.CustomEventArgs;
  using Ogama.Modules.Recording.AleaInterface;
  using Ogama.Modules.Recording.MouseOnlyInterface;
  using Ogama.Modules.Recording.SMIInterface;
  using System;

  /// <summary>
  /// This interface encapsulates the structure of an smi client
  /// </summary>
  public interface ISMIClient
  {
    /// <summary>
    /// An implementation of this event should
    /// send the new sampling data at each sampling time intervall.
    /// </summary>
    event GazeDataChangedEventHandler GazeDataChanged;

    /// <summary>
    /// An implementation of this event should
    /// trigger on calibration end.
    /// </summary>
    event EventHandler CalibrationFinished;

    /// <summary>
    /// Gets a value indicating whether the tracker is connected
    /// </summary>
    bool IsConnected { get; }

    /// <summary>
    /// Gets a value indicating whether the tracker is tracking.
    /// </summary>
    bool IsTracking { get; }

    /// <summary>
    /// Gets or sets the <see cref="SMISetting" /> to be used within this client.
    /// </summary>
    /// <value>
    /// The settings.
    /// </value>
    SMISetting Settings { get; set; }

    /// <summary>
    /// An implementation of this method should do all 
    /// connection routines for the specific hardware, so that the
    /// system is ready for calibration.
    /// </summary>
    void Connect();

    /// <summary>
    /// An implementation of this method should disconnect the client connection.
    /// </summary>
    void Disconnect();

    /// <summary>
    /// An implementation of this method should return the sample time of the last received sample.
    /// </summary>
    /// <returns>A <see cref="Int64"/> with the sample time of the last received sample.</returns>
    long GetTimeStamp();

    /// <summary>
    /// An implementation of this method should do the calibration
    /// for the specific hardware, so that the system is ready for recording.
    /// </summary>
    void Calibrate();

    /// <summary>
    /// An implementation of this method should start the tracking.
    /// </summary>
    void StartTracking();

    /// <summary>
    /// An implementation of this method should stop the tracking.
    /// </summary>
    void StopTracking();
  }
}
