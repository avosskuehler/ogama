// <copyright file="AslSetting.cs" company="FU Berlin">
// ******************************************************
// OGAMA - open gaze and mouse analyzer 
// Copyright (C) 2015 Dr. Adrian Voßkühler  
// ------------------------------------------------------------------------
// This program is free software; you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation; either version 2 of the License, or (at your option) any later version.
// This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
// You should have received a copy of the GNU General Public License along with this program; if not, write to the Free Software Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA 02111-1307 USA
// **************************************************************
// </copyright>
// <author>University Toulouse 2 - CLLE-LTC UMR5263</author>
// <email>virginie.feraud@univ-tlse2.fr</email>

namespace Ogama.Modules.Recording.ASLInterface
{
  using System;
  using System.Xml.Serialization;

  /// <summary>
  /// Class to save settings for the eye tracking system.
  /// It is XML serializable and can be stored in a file via 
  /// the <see cref="XmlSerializer"/> class.
  /// <example>XmlSerializer serializer = new XmlSerializer(typeof(AslSetting));</example>
  /// </summary>   
  [Serializable]
  public class AslSetting
  {
    ///////////////////////////////////////////////////////////////////////////////
    // Defining Constants                                                        //
    ///////////////////////////////////////////////////////////////////////////////
    #region CONSTANTS
    #endregion //CONSTANTS

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Enumerations                                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region ENUMS
    #endregion // ENUMS

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Variables, Enumerations, Events                                  //
    ///////////////////////////////////////////////////////////////////////////////
    #region FIELDS

    /// <summary>
    /// Saves the asl system optic type setting.
    /// </summary>
    private int systemType;

    /// <summary>
    /// Saves the asl pupil mode setting.
    /// <value>A <see cref="bool"/> which can be false (BRIGHT) or true (DARK).</value>
    /// </summary>
    private bool darkPupil;

    /// <summary>
    /// Saves the asl unit setting.
    /// <value>A <see cref="bool"/> which can be false (ENGLISH) or true (METRIC).</value>
    /// </summary>
    private bool useMetricSystem;

    /// <summary>
    /// Saves the point number of the test map which is used to calibrate the eye tracker.
    /// <value>A <see cref="bool"/> which can be true (NINE POINTS)
    /// or false (SEVENTEEN POINTS).</value>
    /// </summary>
    private bool pointsEyeCalibration;

    /// <summary>
    /// Saves the serial data output format type for the standard 8-byte output buffer.
    /// <value>A <see cref="bool"/> which can be false (on demand mode)
    /// or true (streaming mode).</value>
    /// </summary>
    private bool streamingMode;

    /// <summary>
    /// Saves the eye camera update rate setting.
    /// <value>An <see cref="int"/> which can be 50, 60 (default), 120, 240
    /// or 360 Hertz.</value>
    /// </summary>
    private int eyeCameraSpeed;

    /// <summary>
    /// Saves the serial port COM number setting.
    /// <value>An <see cref="EInterfacePort"/> which is between 1 (default) and 9
    /// for COM1 to COM9</value>
    /// </summary>
    private int interfacePort;

    /// <summary>
    /// Saves the serial out baud rate setting.
    /// <value>An <see cref="int"/> which can be 1200, 2400, 4800, 9600, 14400, 19200,
    /// 28800, 38400, 57600 (default) or 115 200 bauds.</value>
    /// </summary>
    private int portBaudRate;

    /// <summary>
    /// Saves the limit in milliseconds for the eye blinking.
    /// <remarks>It's usually shorter than 200 ms.</remarks> 
    /// </summary>
    private short maxBlink;

    #endregion //FIELDS
    ///////////////////////////////////////////////////////////////////////////////
    // Construction and Initializing methods                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region CONSTRUCTION

    /// <summary>
    /// Initializes a new instance of the AslSetting class with Default parameters.
    /// </summary>
    public AslSetting()
    {
      this.SystemType = 1;
      this.DarkPupil = false;
      this.UseMetricSystem = false;
      this.PointsEyeCalibration = true;
      this.StreamingMode = true;
      this.EyeCameraSpeed = Document.ActiveDocument.ExperimentSettings.GazeSamplingRate;
      this.InterfacePort = (int)EInterfacePort.COM1;
      this.PortBaudRate = 57600;  // Default value
      this.MaxBlink = 2000;       // Default value
    }

    #endregion //CONSTRUCTION
    ///////////////////////////////////////////////////////////////////////////////
    // Defining Properties                                                       //
    ///////////////////////////////////////////////////////////////////////////////
    #region PROPERTIES

    /// <summary>
    /// Gets or sets the system optic type setting.
    /// </summary>
    public int SystemType
    {
      get { return this.systemType; }
      set { this.systemType = value; }
    }

    /// <summary>
    /// Gets or sets a value indicating whether the asl pupil setting mode is Bright or Dark.
    /// <strong>False</strong>, for BRIGHT or <strong>True</strong>, for DARK.
    /// </summary>
    public bool DarkPupil
    {
      get { return this.darkPupil; }
      set { this.darkPupil = value; }
    }

    /// <summary>
    /// Gets or sets a value indicating whether the asl unit setting is ENGLISH or METRIC.
    /// <strong>False</strong>, the unit setting is ENGLISH. 
    /// <strong>True</strong>, the unit setting is METRIC.
    /// </summary>
    public bool UseMetricSystem
    {
      get { return this.useMetricSystem; }
      set { this.useMetricSystem = value; }
    }

    /// <summary>
    /// Gets or sets a value indicating whether the point number of the test map (used to calibrate the eye
    /// tracker system) is NINE or SEVENTEEN.
    /// <strong>False</strong> for SEVENTEEN POINTS.
    /// <strong>True</strong> for NINE POINTS.
    /// </summary>
    public bool PointsEyeCalibration
    {
      get { return this.pointsEyeCalibration; }
      set { this.pointsEyeCalibration = value; }
    }

    /// <summary>
    /// Gets or sets a value indicating whether the serial data output format type for the standard 8-byte output
    /// buffer is ON DEMAND MODE or STREAMING MODE.
    /// <strong>False</strong>, for ON DEMAND MODE.
    /// <strong>True</strong>, for STREAMING MODE.
    /// </summary>
    public bool StreamingMode
    {
      get { return this.streamingMode; }
      set { this.streamingMode = value; }
    }

    /// <summary>
    /// Gets or sets the eye camera update rate setting.
    /// <value>An <see cref="int"/> which can be 50, 60 (default), 120, 240
    /// or 360 Hertz.</value>
    /// </summary>
    public int EyeCameraSpeed
    {
      get { return this.eyeCameraSpeed; }
      set { this.eyeCameraSpeed = value; }
    }

    /// <summary>
    /// Gets or sets the serial port number setting.
    /// <value>An <see cref="EInterfacePort"/> which is between 1 (default) and 9
    /// for COM1 to COM9</value>
    /// </summary>
    public int InterfacePort
    {
      get { return this.interfacePort; }
      set { this.interfacePort = value; }
    }

    /// <summary>
    /// Gets or sets the serial out baud rate setting.
    /// <value>An <see cref="int"/> which can be 1200, 2400, 4800, 9600, 14400, 19200,
    /// 28800, 38400, 57600 (default) or 115 200 bauds.</value>
    /// </summary>
    public int PortBaudRate
    {
      get { return this.portBaudRate; }
      set { this.portBaudRate = value; }
    }

    /// <summary>
    /// Gets or sets the limit in milliseconds for the eye blinking.
    /// </summary>
    public short MaxBlink
    {
      get { return this.maxBlink; }
      set { this.maxBlink = value; }
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