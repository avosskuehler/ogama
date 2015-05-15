// <copyright file="GazepointSetting.cs" company="Gazepoint">
// ******************************************************
// OGAMA - open gaze and mouse analyzer 
// Copyright (C) 2015 Dr. Adrian Voßkühler  
// ------------------------------------------------------------------------
// This program is free software; you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation; either version 2 of the License, or (at your option) any later version.
// This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
// You should have received a copy of the GNU General Public License along with this program; if not, write to the Free Software Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA 02111-1307 USA
// **************************************************************
// </copyright>
// <author>Herval Yatchou</author>
// <email>herval.yatchou@tandemlaunchtech.com</email>
// <modifiedby>Andras Pattantyus, andras@gazept.com</modifiedby>

namespace Ogama.Modules.Recording.GazepointInterface
{
  using System;
  using System.Xml.Serialization;

  /// <summary>
  /// Class to save settings for the Gazepoint eye tracking system.
  /// It is XML serializable and can be stored in a file via 
  /// the <see cref="XmlSerializer"/> class.
  /// <example>XmlSerializer serializer = new XmlSerializer(typeof(GazepointSetting));</example>
  /// </summary>
  [Serializable]
  public class GazepointSetting
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
    /// Saves the server address of the Gazepoint tracking system.
    /// </summary>
    private string memServerAddress;

    /// <summary>
    /// Saves the port for the server address of the Gazepoint tracking system.
    /// </summary>
    private int memServerPort;

    /// <summary>
    /// Saves the type of the calibration (manual=0 or automatic=1).
    /// </summary>
    private int memCalibrationType;

    /// <summary>
    /// Saves the calibration speed in seconds.
    /// </summary>
    private double memCalibPointSpeed;

    /// <summary>
    /// Specify if the calibration must be done faster or not.
    /// </summary>
    private bool memFastCalibration;

    /// <summary>
    /// Specify if the calibration windows must be hidden after calibration done.
    /// </summary>
    private bool memHideCalibWin;

    /// <summary>
    /// Specify the number of connected tracker.
    /// </summary>
    private int memConnectedTrackerNum;

    #endregion //FIELDS

    ///////////////////////////////////////////////////////////////////////////////
    // Construction and Initializing methods                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region CONSTRUCTION

    /// <summary>
    /// Initializes a new instance of the GazepointSetting class.
    /// </summary>
    public GazepointSetting()
    {
      this.memServerAddress = "127.0.0.1";
      this.memServerPort = 4242;
      this.memCalibrationType = 0;
      this.memFastCalibration = false;
      this.memHideCalibWin = false;
      this.memCalibPointSpeed = 2.0;
      this.memConnectedTrackerNum = 1;
    }

    #endregion //CONSTRUCTION

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Properties                                                       //
    ///////////////////////////////////////////////////////////////////////////////
    #region PROPERTIES

    /// <summary>
    /// Gets or sets the tracker IP host address.
    /// </summary>
    /// <value>A <see cref="string"/> with the server address.</value>
    /// <remarks>This can be detected via a ping to the serial number in the command line.</remarks>
    public string ServerAddress
    {
      get
      {
        return this.memServerAddress;
      }

      set
      {
        this.memServerAddress = value;
      }
    }

    /// <summary>
    /// Gets or sets the tracker IP port number.
    /// </summary>
    /// <value>A <see cref="int"/> with the port number, default is 4242.</value>
    public int ServerPort
    {
      get
      {
        return this.memServerPort;
      }

      set
      {
        this.memServerPort = value;
      }
    }

    /// <summary>
    /// Gets or sets the type of the calibration.
    /// </summary>
    /// <value>A <see cref="Int32"/> which is manual (0) or automatic (1).</value>
    public int CalibrationType
    {
      get
      {
        return this.memCalibrationType;
      }

      set
      {
        this.memCalibrationType = value;
      }
    }

    /// <summary>
    /// Gets or sets the speed of the Calibration point.
    /// </summary>
    /// <value>A <see cref="Int32"/> which is between fast and slow.</value>
    public double CalibPointSpeed
    {
      get
      {
        return this.memCalibPointSpeed;
      }

      set
      {
        this.memCalibPointSpeed = value;
      }
    }

    /// <summary>
    /// Gets or sets a value indicating whether we use fast calibration.
    /// </summary>
    /// <value>A <see cref="Int32"/> which is between fast and slow.</value>
    public bool IsCalibFast
    {
      get
      {
        return this.memFastCalibration;
      }

      set
      {
        this.memFastCalibration = value;
      }
    }

    /// <summary>
    /// Gets or sets a value indicating whether we use fast calibration.
    /// </summary>
    /// <value>A <see cref="Int32"/> which is between fast and slow.</value>
    public bool HideCalibWindow
    {
      get
      {
        return this.memHideCalibWin;
      }

      set
      {
        this.memHideCalibWin = value;
      }
    }

    /// <summary>
    /// Gets or sets the speed of the Calibration point.
    /// </summary>
    /// <value>A <see cref="Int32"/> which is between fast and slow.</value>
    public int ConnectedTrackerNum
    {
      get
      {
        return this.memConnectedTrackerNum;
      }

      set
      {
        this.memConnectedTrackerNum = value;
      }
    }

    #endregion //PROPERTIES

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
    #endregion //CUSTOMEVENTHANDLER

    #endregion //EVENTS

    ///////////////////////////////////////////////////////////////////////////////
    // Methods and Eventhandling for Background tasks                            //
    ///////////////////////////////////////////////////////////////////////////////
    #region BACKGROUNDWORKER
    #endregion //BACKGROUNDWORKER

    ///////////////////////////////////////////////////////////////////////////////
    // Inherited methods                                                         //
    ///////////////////////////////////////////////////////////////////////////////
    #region OVERRIDES
    #endregion //OVERRIDES

    ///////////////////////////////////////////////////////////////////////////////
    // Methods for doing main class job                                          //
    ///////////////////////////////////////////////////////////////////////////////
    #region METHODS
    #endregion //METHODS

    ///////////////////////////////////////////////////////////////////////////////
    // Small helping Methods                                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region HELPER
    #endregion //HELPER
  }
}