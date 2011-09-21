// <copyright file="ITUClient.cs" company="FU Berlin">
// ******************************************************
// OGAMA - open gaze and mouse analyzer 
// Copyright (C) 2010 Adrian Voßkühler  
// ------------------------------------------------------------------------
// This program is free software; you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation; either version 2 of the License, or (at your option) any later version.
// This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
// You should have received a copy of the GNU General Public License along with this program; if not, write to the Free Software Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA 02111-1307 USA
// **************************************************************
// </copyright>
// <author>Adrian Voßkühler</author>
// <email>adrian.vosskuehler@fu-berlin.de</email>

namespace Ogama.Modules.Recording.ITU
{
  using System;
  using System.Net;
  using System.Threading;
  using GazeTrackerClient;
  using Ogama.ExceptionHandling;
  using Ogama.Modules.Common;

  using OgamaControls;

  using GazeData = Ogama.Modules.Recording.GazeData;

  /// <summary>
  /// This class is the abstraction layer for the communication
  /// via UDP with the SMI iViewX eyetracker.
  /// </summary>
  public class ITUClient
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

    /// <summary>
    /// The client which hosts 
    /// the udp connection to the iViewX host computer.
    /// </summary>
    private readonly Client client;

    /// <summary>
    /// The thread which listens to packets received from
    /// the udp connection to the iViewX host computer.
    /// </summary>
    private Thread clientReceiveThread;

    /// <summary>
    /// Saves the sample time of the last received gaze sample-
    /// </summary>
    private long lastTime;

    ///// <summary>
    ///// Flag indicating to stop the <see cref="clientReceiveThread"/>.
    ///// </summary>
    //// private bool stopListenThread;

    #endregion //FIELDS

    ///////////////////////////////////////////////////////////////////////////////
    // Construction and Initializing methods                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region CONSTRUCTION

    /// <summary>
    /// Initializes a new instance of the ITUClient class.
    /// </summary>
    public ITUClient()
    {
      this.client = new Client { IPAddress = IPAddress.Parse("127.0.0.1"), PortReceive = 6666, PortSend = 5555 };
      this.client.OnClientConnectionChanged += this.OnClientConnectionChanged;
      this.client.Calibration.OnEnd += this.CalibrationOnEnd;

      this.lastTime = 0;
    }

    /// <summary>
    /// Finalizes an instance of the ITUClient class
    /// </summary>
    ~ITUClient()
    {
      if (this.client == null)
      {
        return;
      }

      this.client.Disconnect();
    }

    #endregion //CONSTRUCTION

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Enumerations                                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region ENUMS

    /// <summary>
    /// Event. Raised, when new gaze data is available.
    /// </summary>
    public event GazeDataChangedEventHandler GazeDataAvailable;

    /// <summary>
    /// Event. Raised, when calibration has finished.
    /// </summary>
    public event EventHandler CalibrationFinished;

    /// <summary>
    /// Event. Raised whenever an error occured in the ITU client.
    /// </summary>
    public event StringEventHandler ErrorOccured;

    #endregion ENUMS

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Properties                                                       //
    ///////////////////////////////////////////////////////////////////////////////
    #region PROPERTIES

    /// <summary>
    /// Gets a value indicating whether the connection of the smi client object is available
    /// </summary>
    public bool IsConnected
    {
      get { return this.client.IsRunning; }
    }

    /// <summary>
    /// Gets a value indicating whether the smi client object is tracking.
    /// </summary>
    public bool IsTracking { get; private set; }

    #endregion //PROPERTIES

    ///////////////////////////////////////////////////////////////////////////////
    // Public methods                                                            //
    ///////////////////////////////////////////////////////////////////////////////
    #region PUBLICMETHODS

    /// <summary>
    /// Configures the SMI iViewX System.
    /// </summary>
    public void Configure()
    {
    }

    /// <summary>
    /// Connects the udp client to the iViewX Server.
    /// </summary>
    /// <returns>Returns true if succesful, otherwise false.</returns>
    public bool Connect()
    {
      try
      {
        if (!this.client.IsRunning)
        {
          this.clientReceiveThread = new Thread(this.StartClientReceiveThread);
          this.clientReceiveThread.Start();
          return true;
        }
      }
      catch (Exception ex)
      {
        ExceptionMethods.HandleExceptionSilent(ex);
      }

      return false;
    }

    /// <summary>
    /// Disconnects the udp client connection.
    /// </summary>
    public void Disconnect()
    {
      try
      {
        this.clientReceiveThread.Abort();
        this.client.Disconnect();
      }
      catch (Exception ex)
      {
        ExceptionMethods.HandleExceptionSilent(ex);
      }
    }

    /// <summary>
    /// Starts the data streaming by sending the ET_STR command to
    /// iViewX
    /// </summary>
    public void StartStreaming()
    {
      this.client.Stream.StreamStart();
      this.IsTracking = true;
    }

    /// <summary>
    /// Starts the data streaming by sending the ET_EST command to
    /// iViewX
    /// </summary>
    public void StopStreaming()
    {
      this.client.Stream.StreamStop();
      this.IsTracking = false;
    }

    /// <summary>
    /// Returns the sample time of the last received sample.
    /// </summary>
    /// <returns>A <see cref="Int64"/> with the sample time of the last received sample.</returns>
    public long GetCurrentTime()
    {
      return this.lastTime;
    }

    /// <summary>
    /// This method performs a unrandomized nine point calibration.
    /// </summary>
    public void Calibrate()
    {
      this.client.Calibration.Start();
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
    #region EVENTS

    ///////////////////////////////////////////////////////////////////////////////
    // Eventhandler for UI, Menu, Buttons, Toolbars etc.                         //
    ///////////////////////////////////////////////////////////////////////////////
    #region WINDOWSEVENTHANDLER
    #endregion //WINDOWSEVENTHANDLER

    ///////////////////////////////////////////////////////////////////////////////
    // Eventhandler for Custom Defined Events                                    //
    ///////////////////////////////////////////////////////////////////////////////
    #region CUSTOMEVENTHANDLER

    private void CalibrationOnEnd(int quality)
    {
      this.OnCalibrationFinished(EventArgs.Empty);
    }

    private void OnClientConnectionChanged(bool isConnected)
    {
      if (isConnected)
      {
        this.OnErrorOccured(new StringEventArgs("Connected"));
      }
      else
      {
        this.OnErrorOccured(new StringEventArgs("Not connected"));
      }
    }

    /// <summary>
    /// Raised when new gaze data is available.
    /// </summary>
    /// <param name="e"><see cref="GazeDataChangedEventArgs"/> event arguments</param>.
    private void OnGazeDataAvailable(GazeDataChangedEventArgs e)
    {
      if (this.GazeDataAvailable != null)
      {
        this.GazeDataAvailable(this, e);
      }
    }

    /// <summary>
    /// Raised when CalibrationFinished
    /// </summary>
    /// <param name="e">Empty <see cref="EventArgs"/> event arguments</param>.
    private void OnCalibrationFinished(EventArgs e)
    {
      if (this.CalibrationFinished != null)
      {
        this.CalibrationFinished(this, e);
      }
    }

    /// <summary>
    /// Raised when ErrorOccured
    /// </summary>
    /// <param name="e">A <see cref="StringEventArgs"/> with the event data.</param>.
    private void OnErrorOccured(StringEventArgs e)
    {
      if (this.ErrorOccured != null)
      {
        this.ErrorOccured(this, e);
      }
    }

    #endregion //CUSTOMEVENTHANDLER

    #endregion //EVENTS

    ///////////////////////////////////////////////////////////////////////////////
    // Methods and Eventhandling for Background tasks                            //
    ///////////////////////////////////////////////////////////////////////////////
    #region BACKGROUNDWORKER

    private void StartClientReceiveThread()
    {
      try
      {
        this.client.GazeData.OnGazeData += this.GazeData_OnGazeData;
        this.client.Connect();
      }
      catch (Exception ex)
      {
        this.OnErrorOccured(new StringEventArgs(ex.Message));
      }
    }

    private void GazeData_OnGazeData(GazeTrackerClient.GazeData gazeData)
    {
      var ogamaGazeData = new GazeData
        {
          //// Calculate values between 0..1
          //GazePosX = e.Gazedata.GazePosX / this.resolutionX,
          //GazePosY = e.Gazedata.GazePosY / this.resolutionY
          GazePosX = (float)gazeData.GazePositionX,
          GazePosY = (float)gazeData.GazePositionY,
          PupilDiaX = gazeData.PupilDiameterLeft,
          PupilDiaY = gazeData.PupilDiameterRight,
          Time = gazeData.TimeStamp
        };

      this.lastTime = gazeData.TimeStamp;
      this.OnGazeDataAvailable(new GazeDataChangedEventArgs(ogamaGazeData));
    }

    #endregion //BACKGROUNDWORKER

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

    internal void ChangeSettings()
    {
      throw new NotImplementedException();
    }
  }
}
