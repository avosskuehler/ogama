// <copyright file="ITracker.cs" company="FU Berlin">
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

  /// <summary>
  /// This interface introduces a possibility to add new tracking hardware
  /// to the <see cref="RecordModule"/> <see cref="System.Windows.Forms.Form"/>.
  /// </summary>
  /// <remarks>For an example how to implement this interface have a look
  /// at the four existing implementations <see cref="MouseOnlyTracker"/>
  /// for the tracking of mouse data and Tobii.TobiiTracker for
  /// tracking with a Tobii (www.tobii.com) system, <see cref="AleaTracker"/> for
  /// tracking with a Alea Technologies (www.alea-technologies.com) system
  /// and  <see cref="SMITracker"/> for
  /// tracking with a SMI iViewX (www.smivision.com) system.
  /// Please also refer to the <see cref="RecordModule"/> source to add the
  /// new tracker to the user interface. Each tracker should have its own
  /// <see cref="System.Windows.Forms.TabPage"/> in the <see cref="RecordModule.tclEyetracker"/>.</remarks>
  public interface ITracker
  {
    /// <summary>
    /// An implementation of this event should
    /// send the new sampling data at each sampling time intervall.
    /// </summary>
    event GazeDataChangedEventHandler GazeDataChanged;

    /// <summary>
    /// Gets a value indicating whether the tracker is connected
    /// </summary>
    bool IsConnected { get; }

    /// <summary>
    /// An implementation of this method should do all 
    /// connection routines for the specific hardware, so that the
    /// system is ready for calibration.
    /// </summary>
    /// <returns><strong>True</strong> if succesful connected to tracker,
    /// otherwise <strong>false</strong>.</returns>
    bool Connect();

    /// <summary>
    /// An implementation of this method should do the calibration
    /// for the specific hardware, so that the
    /// system is ready for recording.
    /// </summary>
    /// <param name="isRecalibrating">whether to use recalibration or not.</param>
    /// <returns><strong>True</strong> if succesful calibrated,
    /// otherwise <strong>false</strong>.</returns>
    bool Calibrate(bool isRecalibrating);

    /// <summary>
    /// An implementation of this method should start the recording
    /// for the specific hardware, so that the
    /// system sends <see cref="GazeDataChanged"/> events.
    /// </summary>
    void Record();

    /// <summary>
    /// An implementation of this method should stop the recording
    /// for the specific hardware.
    /// </summary>
    void Stop();

    /// <summary>
    /// An implementation of this method should do a clean up
    /// for the specific hardware, so that the
    /// system is ready for shut down.
    /// </summary>
    void CleanUp();

    /// <summary>
    /// An implementation of this method should show a hardware 
    /// system specific dialog to change its settings like
    /// sampling rate or connection properties. It should also
    /// provide a xml serialization possibility of the settings,
    /// so that the user can store and backup system settings in
    /// a separate file. These settings should be implemented in
    /// a separate class and are stored in a special place of
    /// Ogamas directory structure.
    /// </summary>
    /// <remarks>Please have a look at the existing implemention
    /// of the tobii system in the namespace Tobii.</remarks>
    void ChangeSettings();
  }
}
