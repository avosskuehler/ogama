// <copyright file="VideoExportProperties.cs" company="FU Berlin">
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

namespace Ogama.Modules.Replay.Video
{
  using System.Drawing;

  using OgamaControls;

  /// <summary>
  /// This class encapsulates all properties needed to
  /// export gaze and mouse along with user video 
  /// into a new video.
  /// </summary>
  public class VideoExportProperties
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
    /// Initializes a new instance of the VideoExportProperties class.
    /// </summary>
    public VideoExportProperties()
    {
      this.OutputVideoColor = Color.White;
      this.OutputVideoProperties = new CaptureDeviceProperties();

      this.GazeVideoProperties = new VideoStreamProperties();
      this.GazeVideoProperties.IsStreamRendered = false;

      this.UserVideoProperties = new VideoStreamProperties();
      this.UserVideoProperties.IsStreamRendered = false;
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
    /// Gets or sets the background color for the video.
    /// </summary>
    public Color OutputVideoColor { get; set; }

    /// <summary>
    /// Gets or sets a filename with path to a temporary file location
    /// in which to store the recompressed user video, if applicable.
    /// </summary>
    public string UserVideoTempFile { get; set; }

    /// <summary>
    /// Gets or sets the <see cref="CaptureDeviceProperties"/> for the
    /// output video stream.
    /// </summary>
    public CaptureDeviceProperties OutputVideoProperties { get; set; }

    /// <summary>
    /// Gets or sets the <see cref="VideoStreamProperties"/> for the 
    /// gaze and mouse video.
    /// </summary>
    public VideoStreamProperties GazeVideoProperties { get; set; }

    /// <summary>
    /// Gets or sets the <see cref="VideoStreamProperties"/> for the 
    /// user video.
    /// </summary>
    public VideoStreamProperties UserVideoProperties { get; set; }

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
