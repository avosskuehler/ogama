// <copyright file="AleaSetting.cs" company="alea technologies">
// ******************************************************
// OGAMA - open gaze and mouse analyzer 
// Copyright (C) 2013 Dr. Adrian Voßkühler  
// ------------------------------------------------------------------------
// This program is free software; you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation; either version 2 of the License, or (at your option) any later version.
// This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
// You should have received a copy of the GNU General Public License along with this program; if not, write to the Free Software Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA 02111-1307 USA
// **************************************************************
// </copyright>
// <author>Martin Werner</author>
// <email>martin.werner@alea-technologies.de</email>

namespace Ogama.Modules.Recording.AleaInterface
{
  using System;
  using System.Drawing;
  using System.Xml.Serialization;

  using Alea.Api;

  using VectorGraphics.Tools.CustomTypeConverter;

  /// <summary>
  /// Class to save settings for the alea eye tracking system.
  /// It is XML serializable and can be stored in a file via 
  /// the <see cref="XmlSerializer"/> class.
  /// <example>XmlSerializer serializer = new XmlSerializer(typeof(AleaSetting));</example>
  /// </summary>
  [Serializable]
  public class AleaSetting
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
    /// Gets or sets the Server IP host address.
    /// </summary>
    private string serverAddress;

    /// <summary>
    /// Gets or sets the Server IP port number.
    /// </summary>
    private int serverPort;

    /// <summary>
    /// Gets or sets the Client IP address.
    /// </summary>
    private string clientAddress;

    /// <summary>
    /// Gets or sets the Client IP port number.
    /// </summary>
    private int clientPort;

    /// <summary>
    /// Gets or sets the number of calibration points that are shown.
    /// </summary>
    private int numCalibPoint;

    /// <summary>
    /// Gets or sets the calibration area
    /// </summary>
    private int calibArea;

    /// <summary>
    /// Gets or sets 
    /// </summary>
    private int eye;

    /// <summary>
    /// Gets or sets a value indicating whether calibration point order should be randomized or not.
    /// </summary>
    private bool randomizeCalibPointOrder;

    /// <summary>
    /// Gets or sets a value indicating whether bad calibration points should be skipped or not.
    /// </summary>
    private bool skipBadPoints;

    /// <summary>
    /// Gets or sets a value indicating wheter an audio feedback should be played after each calibration point.
    /// </summary>
    private bool playAudioFeedback;

    /// <summary>
    /// Gets or sets the speed of the Calibration point.
    /// </summary>
    private bool slowMode;

    /// <summary>
    /// Gets or sets the color of the circles marking the calibration points.
    /// The default value is red.
    /// </summary>
    private Color calibPointColor;

    /// <summary>
    /// Gets or sets the color of the calibration window background.
    /// </summary>
    private Color calibBackgroundColor;

    #endregion //FIELDS

    ///////////////////////////////////////////////////////////////////////////////
    // Construction and Initializing methods                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region CONSTRUCTION

    /// <summary>
    /// Initializes a new instance of the AleaSetting class.
    /// </summary>
    public AleaSetting()
    {
      this.serverAddress = "127.0.0.1";
      this.serverPort = 27412;
      this.clientAddress = "127.0.0.1";
      this.clientPort = 27413;
      this.numCalibPoint = 9;
      this.calibArea = (int)PointLocationEnum.Full;
      this.eye = (int)EyeTypeEnum.Binocular;
      this.randomizeCalibPointOrder = false;
      this.skipBadPoints = true;
      this.playAudioFeedback = true;
      this.slowMode = false;
      this.calibPointColor = Color.Blue;
      this.calibBackgroundColor = SystemColors.ControlLight;
    }

    #endregion //CONSTRUCTION

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Properties                                                       //
    ///////////////////////////////////////////////////////////////////////////////
    #region PROPERTIES

    /// <summary>
    /// Gets or sets the Server IP host address.
    /// </summary>
    /// <value>A <see cref="string"/> with the server address.</value>
    /// <remarks>This can be detected via a ping to the serial number in the command line.</remarks>
    public string ServerAddress
    {
      get { return this.serverAddress; }
      set { this.serverAddress = value; }
    }

    /// <summary>
    /// Gets or sets the Server IP port number.
    /// </summary>
    /// <value>A <see cref="int"/> with the port number, default is 27412.</value>
    public int ServerPort
    {
      get { return this.serverPort; }
      set { this.serverPort = value; }
    }

    /// <summary>
    /// Gets or sets the Client IP address.
    /// </summary>
    /// <value>A <see cref="string"/> with the client address.</value>
    public string ClientAddress
    {
      get { return this.clientAddress; }
      set { this.clientAddress = value; }
    }

    /// <summary>
    /// Gets or sets the Client IP port number.
    /// </summary>
    /// <value>A <see cref="int"/> with the port number, default is 27413.</value>
    public int ClientPort
    {
      get { return this.clientPort; }
      set { this.clientPort = value; }
    }

    /// <summary>
    /// Gets or sets the number of calibration points that are shown.
    /// </summary>
    /// <remarks>9 are standard, 16 gives higher precision, 1 are for uncooperative subjects.</remarks>
    /// <value>A <see cref="int"/> with the number of points, can be  (1,5,9,16).</value>
    public int NumCalibPoint
    {
      get { return this.numCalibPoint; }
      set { this.numCalibPoint = value; }
    }

    /// <summary>
    /// Gets or sets the calibration area
    /// </summary>
    /// <value>A <see cref="PointLocationEnum"/>. Can be (0,1,2)</value>
    public int CalibArea
    {
      get { return this.calibArea; }
      set { this.calibArea = value; }
    }

    /// <summary>
    /// Gets or sets the eye index.
    /// </summary>
    public int Eye
    {
      get { return this.eye; }
      set { this.eye = value; }
    }

    /// <summary>
    /// Gets or sets a value indicating whether calibration point order should be randomized or not.
    /// </summary>
    /// <value>A <see cref="Boolean"/> indicating the randomizing of the calibration points.</value>
    public bool RandomizeCalibPointOrder
    {
      get { return this.randomizeCalibPointOrder; }
      set { this.randomizeCalibPointOrder = value; }
    }

    /// <summary>
    /// Gets or sets a value indicating whether bad calibration points should be skipped or not.
    /// </summary>
    public bool SkipBadPoints
    {
      get { return this.skipBadPoints; }
      set { this.skipBadPoints = value; }
    }

    /// <summary>
    /// Gets or sets a value indicating whether an audio feedback should be played after each calibration point.
    /// </summary>
    public bool PlayAudioFeedback
    {
      get { return this.playAudioFeedback; }
      set { this.playAudioFeedback = value; }
    }

    /// <summary>
    /// Gets or sets a value indicating whether the speed of the Calibration point during calibration is slow or fast.
    /// </summary>
    public bool SlowMode
    {
      get { return this.slowMode; }
      set { this.slowMode = value; }
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
    /// Gets or sets the <see cref="SerializedCalibPointColor"/>.
    /// Serializes and deserializes the <see cref="CalibPointColor"/> to XML,
    /// because XMLSerializer can not serialize <see cref="Color"/> values.
    /// </summary>
    public string SerializedCalibPointColor
    {
      get { return ObjectStringConverter.ColorToHtmlAlpha(this.CalibPointColor); }
      set { this.CalibPointColor = ObjectStringConverter.HtmlAlphaToColor(value); }
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
    /// Gets or sets the <see cref="SerializedCalibBackgroundColor"/>.
    /// Serializes and deserializes the <see cref="CalibBackgroundColor"/> to XML,
    /// because XMLSerializer can not serialize <see cref="Color"/> values.
    /// </summary>
    public string SerializedCalibBackgroundColor
    {
      get { return ObjectStringConverter.ColorToHtmlAlpha(this.CalibBackgroundColor); }
      set { this.CalibBackgroundColor = ObjectStringConverter.HtmlAlphaToColor(value); }
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