// <copyright file="SmartEyeSetting.cs" company="Smart Eye AB">
// ******************************************************
// OGAMA - open gaze and mouse analyzer 
// Copyright (C) 2015 Dr. Adrian Voßkühler  
// ------------------------------------------------------------------------
// This program is free software; you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation; either version 2 of the License, or (at your option) any later version.
// This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
// You should have received a copy of the GNU General Public License along with this program; if not, write to the Free Software Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA 02111-1307 USA
// **************************************************************
// </copyright>
// <author>Kathrin Scheil</author>
// <email>kathrin.scheil@smarteye.se</email>

namespace Ogama.Modules.Recording.SmartEyeInterface
{
  using System;
  using System.Collections.Generic;
  using System.Drawing;
  using System.Linq;
  using System.Text;
  using System.Xml.Serialization;
  using VectorGraphics.Tools.CustomTypeConverter;

  /// <summary>
  /// Class to save settings for the Smart Eye eye tracking system.
  /// It is XML serializable and can be stored in a file via 
  /// the <see cref="XmlSerializer"/> class.
  /// <example>XmlSerializer serializer = new XmlSerializer(typeof(SmartEyeSetting));</example>
  /// </summary>
  [Serializable]
  public class SmartEyeSetting
  {
    ///////////////////////////////////////////////////////////////////////////////
    // Defining Variables, Enumerations, Events                                  //
    ///////////////////////////////////////////////////////////////////////////////
    #region FIELDS

    /// <summary>
    /// Saves the server address of the Smart Eye tracking system.
    /// </summary>
    private string smartEyeServerAddress;

    /// <summary>
    /// Saves the port for the RPC server address of the Smart Eye tracking system.
    /// </summary>
    private int smartEyeRPCPort;

    /// <summary>
    /// Saves the port for the address of the computer with OGAMA running.
    /// </summary>
    private int ogamaPort;

    /// <summary>
    /// Saves the number of the calibration points.
    /// </summary>
    private int numCalibPoints;

    /// <summary>
    /// Saves the size for the calibration points.
    /// </summary>
    private int calibPointSize;

    /// <summary>
    /// Saves the speed of the calibration points.
    /// </summary>
    private int calibPointSpeed;

    /// <summary>
    /// Saves the color for the calibration points.
    /// </summary>
    private Color calibPointColor;

    /// <summary>
    /// Saves the background color of the calibration procedure.
    /// </summary>
    private Color calibBackgroundColor;

    /// <summary>
    /// Saves the if the calibration points should be shown in randomized order.
    /// </summary>
    private bool randomizeCalibPointOrder;

    /// <summary>
    /// Saves the quality threshold for recorded data.
    /// </summary>
    private float qualityThreshold;

    /// <summary>
    /// Saves the if the message boxes should be hidden.
    /// </summary>
    private bool silentMode;

    #endregion //FIELDS

    ///////////////////////////////////////////////////////////////////////////////
    // Construction and Initializing methods                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region CONSTRUCTION

    /// <summary>
    /// Initializes a new instance of the SmartEyeSetting class.
    /// </summary>
    public SmartEyeSetting()
    {
      this.SmartEyeServerAddress = "127.0.0.1";
      this.SmartEyeRPCPort = 8100;
      this.OgamaPort = 5556;
      this.NumCalibPoints = 5;
      this.CalibPointSize = 22;
      this.CalibPointSpeed = 2;
      this.CalibPointColor = Color.Blue;
      this.CalibBackgroundColor = SystemColors.ControlLight;
      this.RandomizeCalibPointOrder = true;
      this.QualityThreshold = 0.5f;
      this.SilentMode = false;
    }

    #endregion //CONSTRUCTION

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Properties                                                       //
    ///////////////////////////////////////////////////////////////////////////////
    #region PROPERTIES

    /// <summary>
    /// Gets or sets the Smart Eye Server IP host address.
    /// </summary>
    /// <value>A <see cref="String"/> with the server address.</value>
    public string SmartEyeServerAddress
    {
      get { return this.smartEyeServerAddress; }
      set { this.smartEyeServerAddress = value; }
    }

    /// <summary>
    /// Gets or sets the Smart Eye Server RPC port number.
    /// </summary>
    /// <value>A <see cref="Int32"/> with the RPC port number, default is 8100.</value>
    public int SmartEyeRPCPort
    {
      get { return this.smartEyeRPCPort; }
      set { this.smartEyeRPCPort = value; }
    }

    /// <summary>
    /// Gets or sets the OGAMA port number.
    /// </summary>
    /// <value>A <see cref="Int32"/> with the port number, default is 5556.</value>
    public int OgamaPort
    {
      get { return this.ogamaPort; }
      set { this.ogamaPort = value; }
    }

    /// <summary>
    /// Gets or sets the number of calibration points.
    /// </summary>
    /// <value>An int value which can be 3, 5 or 9.</value>
    public int NumCalibPoints
    {
      get { return this.numCalibPoints; }
      set { this.numCalibPoints = value; }
    }

    /// <summary>
    /// Gets or sets the size for the calibration points.
    /// </summary>
    /// <value>An int value which can be 11, 22 or 44 pixels.</value>
    public int CalibPointSize
    {
      get { return this.calibPointSize; }
      set { this.calibPointSize = value; }
    }

    /// <summary>
    /// Gets or sets the size for the calibration points.
    /// </summary>
    /// <value>An int value which can be 1, 2 or 3.</value>
    public int CalibPointSpeed
    {
      get { return this.calibPointSpeed; }
      set { this.calibPointSpeed = value; }
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
    /// Gets or sets respectively serializes and deserializes the <see cref="CalibPointColor"/> to XML,
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

    /// <summary>
    /// Gets or sets a value indicating whether the calibration points should be shown in randomized order.
    /// </summary>
    /// <value>A bool value which can be true or false.</value>
    public bool RandomizeCalibPointOrder
    {
      get { return this.randomizeCalibPointOrder; }
      set { this.randomizeCalibPointOrder = value; }
    }

    /// <summary>
    /// Gets or sets the quality threshold for recording gaze data.
    /// </summary>
    /// <value>A float value which should range from 0-1.</value>
    public float QualityThreshold
    {
      get { return this.qualityThreshold; }
      set { this.qualityThreshold = value; }
    }

    /// <summary>
    /// Gets or sets a value indicating whether message boxes shoud be hidden.
    /// </summary>
    /// <value>A bool value which can be true or false.</value>
    public bool SilentMode
    {
      get { return this.silentMode; }
      set { this.silentMode = value; }
    }

    #endregion //PROPERTIES
  }
}
