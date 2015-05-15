// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TheEyeTribeSetting.cs" company="Freie Universität Berlin">
//   OGAMA - open gaze and mouse analyzer 
//   Copyright (C) 2015 Dr. Adrian Voßkühler  
//   Licensed under GPL V3
// </copyright>
// <author>Adrian Voßkühler</author>
// <email>adrian@ogama.net</email>
// <summary>
//   Class to save settings for the TheEyeTribe eye tracking system.
//   It is XML serializable and can be stored in a file via
//   the <see cref="XmlSerializer" /> class.
//   <example>XmlSerializer serializer = new XmlSerializer(typeof(TheEyeTribeSetting));</example>
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Ogama.Modules.Recording.TheEyeTribeInterface
{
  using System;
  using System.Drawing;
  using System.Xml.Serialization;

  using VectorGraphics.Tools.CustomTypeConverter;

  /// <summary>
  ///   Class to save settings for the  eye tracking system.
  ///   It is XML serializable and can be stored in a file via
  ///   the <see cref="XmlSerializer" /> class.
  ///   <example>XmlSerializer serializer = new XmlSerializer(typeof(TheEyeTribeSetting));</example>
  /// </summary>
  [Serializable]
  public class TheEyeTribeSetting
  {
    #region Constructors and Destructors

    /// <summary>
    ///   Initializes a new instance of the TheEyeTribeSetting class.
    /// </summary>
    public TheEyeTribeSetting()
    {
      this.CalibrationPointCount = TheEyeTribeCalibrationPoints.Nine;
      this.ServerIPAddress = "localhost";
      this.ServerPort = 6555;
      this.PointDisplayTime = 750;
      this.CalibrationPointColor = Color.Red;
      this.CalibrationBackgroundColor = SystemColors.ControlLight;
      this.DisplayHelp = false;
      this.Framerate = 60;
      this.DeviceIndex = 0;
    }

    #endregion

    #region Public Properties

    /// <summary>
    ///   Gets the parameters for customized EyeTribe.exe start
    /// </summary>
    [XmlIgnore]
    public string ServerStartParams
    {
      get
      {
        return string.Format("--device={0} --framerate={1} --port={2}", this.DeviceIndex, this.Framerate, this.ServerPort);
      }
    }

    /// <summary>
    ///   Gets or sets the data server IP address.
    /// </summary>
    /// <value>A <see cref="string" /> with the server address, default is localhost</value>
    public string ServerIPAddress { get; set; }

    /// <summary>
    /// Gets or sets the Server IP port number.
    /// </summary>
    /// <value>A <see cref="Int32"/> with the port number, default is 6555.</value>
    public int ServerPort { get; set; }

    /// <summary>
    /// Gets or sets the Framerate for the tracker
    /// </summary>
    /// <value>A <see cref="Int32"/> with the frame rate, currently supported values are 30 and 60.</value>
    public int Framerate { get; set; }

    /// <summary>
    /// Gets or sets the device index for the tracking device to use
    /// </summary>
    /// <value>A <see cref="Int32"/> with the zero based device index, allowed range is from 0-7.</value>
    public int DeviceIndex { get; set; }

    /// <summary>
    /// Gets or sets the display time for the calibration points in milliseconds
    /// </summary>
    /// <value>A <see cref="Int32"/> with the display time in milliseconds, default is 750.</value>
    public int PointDisplayTime { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the calibration control should
    /// display help.
    /// </summary>
    public bool DisplayHelp { get; set; }

    /// <summary>
    ///   Gets or sets the number of calibration points that are shown.
    /// </summary>
    /// <remarks>9 are standard, 12/16 gives higher precision.</remarks>
    /// <value>A <see cref="TheEyeTribeCalibrationPoints" /> with the number of points.</value>
    public TheEyeTribeCalibrationPoints CalibrationPointCount { get; set; }

    /// <summary>
    /// Gets or sets the color of the circles marking the calibration points.
    /// The default value is red.
    /// </summary>
    /// <value>A <see cref="Color"/> for the calibration points,
    /// which is converted into a OLE_COLOR Structure.</value>
    [XmlIgnoreAttribute]
    public Color CalibrationPointColor { get; set; }

    /// <summary>
    /// Gets or sets the <see cref="SerializedCalibrationPointColor"/>.
    /// Serializes and deserializes the <see cref="CalibrationPointColor"/> to XML,
    /// because XMLSerializer can not serialize <see cref="Color"/> values.
    /// </summary>
    public string SerializedCalibrationPointColor
    {
      get { return ObjectStringConverter.ColorToHtmlAlpha(this.CalibrationPointColor); }
      set { this.CalibrationPointColor = ObjectStringConverter.HtmlAlphaToColor(value); }
    }

    /// <summary>
    /// Gets or sets the color of the calibration window background.
    /// </summary>
    /// <value>A <see cref="Color"/> for the background,
    /// which is converted into a OLE_COLOR Structure.</value>
    [XmlIgnoreAttribute]
    public Color CalibrationBackgroundColor { get; set; }

    /// <summary>
    /// Gets or sets the <see cref="SerializedCalibrationBackgroundColor"/>.
    /// Serializes and deserializes the <see cref="CalibrationBackgroundColor"/> to XML,
    /// because XMLSerializer can not serialize <see cref="Color"/> values.
    /// </summary>
    public string SerializedCalibrationBackgroundColor
    {
      get { return ObjectStringConverter.ColorToHtmlAlpha(this.CalibrationBackgroundColor); }
      set { this.CalibrationBackgroundColor = ObjectStringConverter.HtmlAlphaToColor(value); }
    }


    #endregion
  }
}