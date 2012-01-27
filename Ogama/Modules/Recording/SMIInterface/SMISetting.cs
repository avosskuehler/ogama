// <copyright file="SMISetting.cs" company="FU Berlin">
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

namespace Ogama.Modules.Recording.SMIInterface
{
  using System;
  using System.Drawing;
  using System.Xml.Serialization;

  using VectorGraphics.Tools.CustomTypeConverter;

  /// <summary>
  /// Class to save settings for the SMI iViewX eye tracking system.
  /// It is XML serializable and can be stored in a file via 
  /// the <see cref="XmlSerializer"/> class.
  /// <example>XmlSerializer serializer = new XmlSerializer(typeof(SMISetting));</example>
  /// </summary>
  [Serializable]
  public class SMISetting
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
    /// Saves the server adress of the SMI tracking system.
    /// </summary>
    private string smiServerAddress;

    /// <summary>
    /// Saves the port for the server adress of the SMI tracking system.
    /// </summary>
    private int smiServerPort;

    /// <summary>
    /// Saves the port for the adress of the computer with OGAMA is running.
    /// </summary>
    private int ogamaServerPort;

    /// <summary>
    /// Saves the <see cref="AvailableEye"/>
    /// </summary>
    private AvailableEye availableEye;

    /// <summary>
    /// Saves the size for the calibration points.
    /// </summary>
    private CalibrationPointSize calibPointSize;

    /// <summary>
    /// Saves the color for the calibration points.
    /// </summary>
    private Color calibPointColor;

    /// <summary>
    /// Saves the background color of the calibration procedure.
    /// </summary>
    private Color calibBackgroundColor;

    #endregion //FIELDS

    ///////////////////////////////////////////////////////////////////////////////
    // Construction and Initializing methods                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region CONSTRUCTION

    /// <summary>
    /// Initializes a new instance of the SMISetting class.
    /// </summary>
    public SMISetting()
    {
      this.smiServerAddress = "127.0.0.1";
      this.smiServerPort = 4444;
      this.ogamaServerPort = 5555;
      this.availableEye = AvailableEye.Both;
      this.calibPointSize = CalibrationPointSize.Medium;
      this.calibPointColor = Color.Red;
      this.calibBackgroundColor = SystemColors.ControlLight;
    }

    #endregion //CONSTRUCTION

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Enumerations                                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region ENUMS
    #endregion ENUMS

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Properties                                                       //
    ///////////////////////////////////////////////////////////////////////////////
    #region PROPERTIES

    /// <summary>
    /// Gets or sets the SMI Server IP host address.
    /// </summary>
    /// <value>A <see cref="String"/> with the server address.</value>
    public string SMIServerAddress
    {
      get { return this.smiServerAddress; }
      set { this.smiServerAddress = value; }
    }

    /// <summary>
    /// Gets or sets the SMI Server IP port number.
    /// </summary>
    /// <value>A <see cref="Int32"/> with the UDP port number, default is 4444.</value>
    public int SMIServerPort
    {
      get { return this.smiServerPort; }
      set { this.smiServerPort = value; }
    }

    /// <summary>
    /// Gets or sets the OGAMA Server IP port number.
    /// </summary>
    /// <value>A <see cref="Int32"/> with the UDP port number, default is 5555.</value>
    public int OGAMAServerPort
    {
      get { return this.ogamaServerPort; }
      set { this.ogamaServerPort = value; }
    }

    /// <summary>
    /// Gets or sets the <see cref="AvailableEye"/> that should be recorded
    /// </summary>
    /// <value>A <see cref="AvailableEye"/> which can be Left, Right or Both.</value>
    public AvailableEye AvailableEye
    {
      get { return this.availableEye; }
      set { this.availableEye = value; }
    }

    /// <summary>
    /// Gets or sets the size for the calibration points.
    /// </summary>
    /// <value>A <see cref="CalibrationPointSize"/> which can be Small, Medium or Large.</value>
    public CalibrationPointSize CalibPointSize
    {
      get { return this.calibPointSize; }
      set { this.calibPointSize = value; }
    }

    /// <summary>
    /// Gets or sets the color of the circles marking the calibration points.
    /// The default value is red.
    /// </summary>
    /// <value>A <see cref="Color"/> for the calibration points,
    /// which is converted into a OLE_COLOR Structure.</value>
    [XmlIgnoreAttribute]
    public Color CalibPointColor
    {
      get { return this.calibPointColor; }
      set { this.calibPointColor = value; }
    }

    /// <summary>
    /// Gets or sets resp. serializes and deserializes the <see cref="CalibPointColor"/> to XML,
    /// because XMLSerializer can not serialize <see cref="Color"/> values.
    /// </summary>
    public string SerializedCalibPointColor
    {
      get { return ObjectStringConverter.ColorToHtmlAlpha(this.calibPointColor); }
      set { this.calibPointColor = ObjectStringConverter.HtmlAlphaToColor(value); }
    }

    /// <summary>
    /// Gets or sets the color of the calibration window background.
    /// </summary>
    /// <value>A <see cref="Color"/> for the background,
    /// which is converted into a OLE_COLOR Structure.</value>
    [XmlIgnoreAttribute]
    public Color CalibBackgroundColor
    {
      get { return this.calibBackgroundColor; }
      set { this.calibBackgroundColor = value; }
    }

    /// <summary>
    /// Gets or sets reps. serializes and deserializes the <see cref="CalibBackgroundColor"/> to XML,
    /// because XMLSerializer can not serialize <see cref="Color"/> values.
    /// </summary>
    public string SerializedCalibBackgroundColor
    {
      get { return ObjectStringConverter.ColorToHtmlAlpha(this.calibBackgroundColor); }
      set { this.calibBackgroundColor = ObjectStringConverter.HtmlAlphaToColor(value); }
    }

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