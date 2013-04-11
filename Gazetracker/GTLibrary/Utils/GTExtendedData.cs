// <copyright file="GTExtendedData.cs" company="ITU">
// ******************************************************
// GazeTrackingLibrary for ITU GazeTracker
// Copyright (C) 2010 Martin Tall  
// ------------------------------------------------------------------------
// This program is free software; you can redistribute it and/or modify it 
// under the terms of the GNU General Public License as published by the 
// Free Software Foundation; either version 3 of the License, 
// or (at your option) any later version.
// This program is distributed in the hope that it will be useful, 
// but WITHOUT ANY WARRANTY; without even the implied warranty of 
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU 
// General Public License for more details.
// You should have received a copy of the GNU General Public License 
// along with this program; if not, see http://www.gnu.org/licenses/.
// **************************************************************
// </copyright>
// <author>Adrian Voßkühler</author>
// <email>adrian@ogama.net</email>

namespace GTLibrary.Utils
{
  /// <summary>
  /// This class contains the data for a gaze position.
  /// </summary>
  public class GTExtendedData
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
    /// Initializes a new instance of the GTExtendedData class.
    /// The coordinates are initalized with zero, the time with -1.
    /// </summary>
    public GTExtendedData()
    {
      this.TimeStamp = -1;
      this.GazePositionX = 0;
      this.GazePositionY = 0;
      this.PupilDiameterLeft = 0;
      this.PupilDiameterRight = 0;
    }

    #endregion //CONSTRUCTION

    ///////////////////////////////////////////////////////////////////////////////
    // Defining events, enums, delegates                                         //
    ///////////////////////////////////////////////////////////////////////////////
    #region EVENTS

    /// <summary>
    /// The delegate for the <see cref="GTExtendedDataChanged"/> event.
    /// </summary>
    /// <param name="data">A <see cref="GTExtendedData"/> with the new data.</param>
    public delegate void GTExtendedDataChangedEventHandler(GTExtendedData data);

    /// <summary>
    /// The event that is raised whenever this class has got new data
    /// </summary>
    public event GTExtendedDataChangedEventHandler GTExtendedDataChanged;

    #endregion EVENTS

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Properties                                                       //
    ///////////////////////////////////////////////////////////////////////////////
    #region PROPERTIES

    /// <summary>
    /// Gets the time stamp of the gaze sample.
    /// </summary>
    public long TimeStamp { get; private set; }

    /// <summary>
    /// Gets the x position of the gaze sample.
    /// </summary>
    public double GazePositionX { get; private set; }

    /// <summary>
    /// Gets the y position of the gaze sample.
    /// </summary>
    public double GazePositionY { get; private set; }

    /// <summary>
    /// Gets the left eyes pupil diameter.
    /// </summary>
    public double PupilDiameterLeft { get; private set; }

    /// <summary>
    /// Gets the right eyes pupil diameter.
    /// </summary>
    public double PupilDiameterRight { get; private set; }

    #endregion //PROPERTIES

    ///////////////////////////////////////////////////////////////////////////////
    // Public methods                                                            //
    ///////////////////////////////////////////////////////////////////////////////
    #region PUBLICMETHODS

    /// <summary>
    /// Sets new gaze coordinates.
    /// </summary>
    /// <param name="timeStamp">A <see cref="long"/> with the new time stamp in milliseconds.</param>
    /// <param name="x">A <see cref="double"/> with the new gaze x coordinate.</param>
    /// <param name="y">A <see cref="double"/> with the new gaze y coordinate.</param>
    /// <param name="diameterLeft">A <see cref="double"/> with the new left pupil diameter.</param>
    /// <param name="diameterRight">A <see cref="double"/> with the new right pupil diameter.</param>
    public void Set(long timeStamp, double x, double y, double diameterLeft, double diameterRight)
    {
      this.TimeStamp = timeStamp;
      this.GazePositionX = x;
      this.GazePositionY = y;
      this.PupilDiameterLeft = diameterLeft;
      this.PupilDiameterRight = diameterRight;

      this.RaiseChangedEvent();
    }

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

    /// <summary>
    /// This method raises the <see cref="GTExtendedDataChanged"/> event.
    /// </summary>
    private void RaiseChangedEvent()
    {
      if (this.GTExtendedDataChanged != null)
      {
        this.GTExtendedDataChanged(this);
      }
    }

    #endregion //PRIVATEMETHODS

    ///////////////////////////////////////////////////////////////////////////////
    // Small helping Methods                                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region HELPER
    #endregion //HELPER
  }
}