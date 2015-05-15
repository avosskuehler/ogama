// <copyright file="ScreenCaptureProperties.cs" company="alea technologies">
// ******************************************************
// OGAMA - open gaze and mouse analyzer 
// Copyright (C) 2015 Dr. Adrian Voßkühler  
// ------------------------------------------------------------------------
// This program is free software; you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation; either version 2 of the License, or (at your option) any later version.
// This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
// You should have received a copy of the GNU General Public License along with this program; if not, write to the Free Software Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA 02111-1307 USA
// **************************************************************
// </copyright>
// <author>Adrian Voßkühler</author>
// <email>adrian@ogama.net</email>

namespace Ogama.Modules.Recording.Presenter
{
  using System.Drawing;
  using System.Windows.Forms;

  using OgamaControls;

  /// <summary>
  /// This class extends <see cref="CaptureDeviceProperties"/>
  /// with the monitor index and the preview window that are used
  /// during screen capturing.
  /// </summary>
  public class ScreenCaptureProperties : CaptureDeviceProperties
  {
    ///////////////////////////////////////////////////////////////////////////////
    // Defining Constants                                                        //
    ///////////////////////////////////////////////////////////////////////////////
    #region CONSTANTS
    #endregion //CONSTANTS

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Variables, Enumerations, Events                                  //
    ///////////////////////////////////////////////////////////////////////////////
    #region FIELDS
    #endregion //FIELDS

    ///////////////////////////////////////////////////////////////////////////////
    // Construction and Initializing methods                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region CONSTRUCTION

    /// <summary>
    /// Initializes a new instance of the ScreenCaptureProperties class.
    /// </summary>
    /// <param name="newVideoInputDevice">The friendly name of the video input device to use.</param>
    /// <param name="newAudioInputDevice">The friendly name of the audio input device to use.</param>
    /// <param name="newVideoCompressor">The friendly name of the video compressor to use.</param>
    /// <param name="newAudioCompressor">The friendly name of the audio compressor to use.</param>
    /// <param name="newFrameRate">The frame rate for the video stream.</param>
    /// <param name="newVideoSize">The size of the video stream.</param>
    /// <param name="newFilename">The filename for the video recording.</param>
    /// <param name="newCaptureMode">The <see cref="CaptureMode"/> flags for the recording.</param>
    /// <param name="newMonitorIndex">The zero-based index of the monitor whichs surface
    /// should be captured.</param>
    /// <param name="newPreviewWindow">The <see cref="Control"/> on which the preview
    /// stream of the DirectShow graph should be shown.</param>
    public ScreenCaptureProperties(
      string newVideoInputDevice,
      string newAudioInputDevice,
      string newVideoCompressor,
      string newAudioCompressor,
      int newFrameRate,
      Size newVideoSize,
      string newFilename,
      CaptureMode newCaptureMode,
      int newMonitorIndex,
      Control newPreviewWindow)
      : base(
      newVideoInputDevice,
      newAudioInputDevice,
      newVideoCompressor,
      newAudioCompressor,
      newFrameRate,
      newVideoSize,
      newFilename,
      newCaptureMode,
      newPreviewWindow)
    {
      this.PreviewWindow = newPreviewWindow;
      this.MonitorIndex = newMonitorIndex;
    }

    #endregion //CONSTRUCTION

    ///////////////////////////////////////////////////////////////////////////////
    // Defining events, enums, delegates                                         //
    ///////////////////////////////////////////////////////////////////////////////
    #region EVENTS
    #endregion EVENTS

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Properties                                                       //
    ///////////////////////////////////////////////////////////////////////////////
    #region PROPERTIES

    /// <summary>
    /// Gets or sets the zero-based index of the monitor whichs surface
    /// should be captured.
    /// </summary>
    public int MonitorIndex { get; set; }

    #endregion //PROPERTIES

    ///////////////////////////////////////////////////////////////////////////////
    // Public methods                                                            //
    ///////////////////////////////////////////////////////////////////////////////
    #region PUBLICMETHODS
    #endregion //PUBLICMETHODS

    ///////////////////////////////////////////////////////////////////////////////
    // Inherited methods                                                         //
    ///////////////////////////////////////////////////////////////////////////////
    #region OVERRIDES
    #endregion //OVERRIDES

    ///////////////////////////////////////////////////////////////////////////////
    // Eventhandler                                                              //
    ///////////////////////////////////////////////////////////////////////////////
    #region EVENTHANDLER
    #endregion //EVENTHANDLER

    ///////////////////////////////////////////////////////////////////////////////
    // Methods and Eventhandling for Background tasks                            //
    ///////////////////////////////////////////////////////////////////////////////
    #region THREAD
    #endregion //THREAD

    ///////////////////////////////////////////////////////////////////////////////
    // Methods for doing main class job                                          //
    ///////////////////////////////////////////////////////////////////////////////
    #region PRIVATEMETHODS
    #endregion //PRIVATEMETHODS

    ///////////////////////////////////////////////////////////////////////////////
    // Small helping Methods                                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region HELPER
    #endregion //HELPER
  }
}
