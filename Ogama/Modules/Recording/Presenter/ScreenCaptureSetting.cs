// <copyright file="ScreenCaptureSetting.cs" company="FU Berlin">
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

namespace Ogama.Modules.Recording
{
  using System;
  using System.Drawing;
  using System.IO;
  using System.Windows.Forms;
  using System.Xml;
  using System.Xml.Serialization;
  using Ogama.ExceptionHandling;
  using Ogama.Modules.Common;

  /// <summary>
  /// Class to save settings for the VH screen capture driver.
  /// It is XML serializable and can be stored in a file via 
  /// the <see cref="XmlSerializer"/> class.
  /// <example>XmlSerializer serializer = new XmlSerializer(typeof(ScreenCaptureSetting));</example>
  /// </summary>
  [Serializable]
  public class ScreenCaptureSetting : IXmlSerializable
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
    /// Gets or sets the frame time (framerate = 10000000 / frametime, 
    /// where 10000000 units = 1 sec, 100ns time format standard for DirectShow).
    /// </summary>
    private int sampleTime;

    /// <summary>
    /// Gets or sets the mouseMagnitude used with Track Mouse feature, 
    /// value range in 0 .. 1000 (means 0% .. 100.0%). 0 means 
    /// the mouse will be always with red rectangle center, 
    /// 1000 means the red rectangle will move when mouse out 
    /// of red rectangle, intermediate values creates intermediate 
    /// non-visible edge, which moves red rectange when mouse out of that edge)
    /// </summary>
    private int mouseMagnitude;

    /// <summary>
    /// Gets or sets the rectangle to capture. Works when trackwnd and trackscr disabled.
    /// </summary>
    private Rectangle trackRectangle;

    /// <summary>
    /// Gets or sets a value indicating whether to align output width and height to be multiple of 4.
    /// </summary>
    private bool align;

    /// <summary>
    /// Gets or sets a value indicating whether to track for 
    /// screen (if you change screen resolution, it must send stream size notify)
    /// </summary>
    private bool trackscr;

    /// <summary>
    /// Gets or sets a value indicating whether to track for window
    /// </summary>
    private bool trackwnd;

    /// <summary>
    /// Gets or sets a value indicating whether to the capture mouse
    /// </summary>
    private bool showmouse;

    /// <summary>
    /// Gets or sets a value indicating whether to capture mouse clicks
    /// </summary>
    private bool showclicks;

    /// <summary>
    /// Gets or sets a value indicating whether to capture 
    /// layered windows (which can be not captured, but cursor will flashing)
    /// </summary>
    private bool caplayered;

    /// <summary>
    /// Gets or sets a value indicating whether to follow capture 
    /// area for mouse (when capture area greater stream size).
    /// Works when resize disabled.
    /// </summary>
    private bool trackmouse;

    /// <summary>
    /// Gets or sets a value indicating whether to optimize capture
    /// from screen, capture only when detect changes in GUI.
    /// </summary>
    private bool optcapture;

    /// <summary>
    /// Gets or sets a value indicating whether optimize frames sending 
    /// (it will send next frame if
    /// detect changes in GUI. if disabled, VHScrCap
    /// will still provide selected FPS)
    /// </summary>
    private bool optdeliver;

    /// <summary>
    /// Gets or sets a value indicating whether to register graph in ROT (Running Object Table, see MSDN).
    /// You can connect to registered remote graph with graphedit MS tool.
    /// </summary>
    private bool regrot;

    /// <summary>
    /// Gets or sets a value indicating whether to limit 
    /// VHScrCap running instances to 1 for each process (if enabled).
    /// </summary>
    private bool limitinst;

    #endregion //FIELDS

    ///////////////////////////////////////////////////////////////////////////////
    // Construction and Initializing methods                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region CONSTRUCTION

    /// <summary>
    /// Initializes a new instance of the ScreenCaptureSetting class.
    /// </summary>
    public ScreenCaptureSetting()
    {
      this.sampleTime = 1000000;
      this.mouseMagnitude = 1000;
      this.trackRectangle = PresentationScreen.GetPresentationBounds();
      this.align = true;
      this.trackscr = false;
      this.trackwnd = false;
      this.showmouse = false;
      this.showclicks = false;
      this.caplayered = false;
      this.trackmouse = false;
      this.optcapture = true;
      this.optdeliver = false;
      this.regrot = false;
      this.limitinst = false;
    }

    #endregion //CONSTRUCTION

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Properties                                                       //
    ///////////////////////////////////////////////////////////////////////////////
    #region PROPERTIES

    /// <summary>
    /// Gets or sets the frame time (framerate = 10000000 / frametime, 
    /// where 10000000 units = 1 sec, 100ns time format standard for DirectShow).
    /// </summary>
    /// <value>A <see cref="Int32"/> with the sample time.</value>
    public int SampleTime
    {
      get { return this.sampleTime; }
      set { this.sampleTime = value; }
    }

    /// <summary>
    /// Gets or sets the mouseMagnitude used with Track Mouse feature, 
    /// value range in 0 .. 1000 (means 0% .. 100.0%). 0 means 
    /// the mouse will be always with red rectangle center, 
    /// 1000 means the red rectangle will move when mouse out 
    /// of red rectangle, intermediate values creates intermediate 
    /// non-visible edge, which moves red rectange when mouse out of that edge)
    /// </summary>
    /// <value>A <see cref="int"/> with the mouse magnitude.</value>
    public int MouseMagnitude
    {
      get { return this.mouseMagnitude; }
      set { this.mouseMagnitude = value; }
    }

    /// <summary>
    /// Gets or sets the rectangle to capture. 
    /// Works when trackwnd and trackscr disabled.
    /// </summary>
    /// <value>A <see cref="Rectangle"/> with the screen rectangle
    /// to be captured.</value>
    public Rectangle TrackRectangle
    {
      get { return this.trackRectangle; }
      set { this.trackRectangle = value; }
    }

    /// <summary>
    ///  Gets or sets a value indicating whether to align 
    ///  output width and height to be multiple of 4.
    /// </summary>
    /// <value>A <see cref="bool"/> which is true,
    /// if output size should be aligned by multiples of 4.</value>
    public bool Align
    {
      get { return this.align; }
      set { this.align = value; }
    }

    /// <summary>
    /// Gets or sets a value indicating whether to 
    /// track for screen (if you change screen resolution, it must send stream size notify)
    /// </summary>
    /// <value>A <see cref="bool"/> which is true when the whole
    /// screen should be captured.</value>
    public bool Trackscr
    {
      get { return this.trackscr; }
      set { this.trackscr = value; }
    }

    /// <summary>
    /// Gets or sets a value indicating whether to track 
    /// a specific window
    /// </summary>
    /// <value>A <see cref="bool"/> which is true when 
    /// a specific window should be captured.</value>
    public bool Trackwnd
    {
      get { return this.trackwnd; }
      set { this.trackwnd = value; }
    }

    /// <summary>
    /// Gets or sets a value indicating whether to capture the  mouse
    /// cursor.
    /// </summary>
    /// <value>A <see cref="bool"/> which is true when 
    /// the mouse cursor should be visible in the screen video.</value>
    public bool Showmouse
    {
      get { return this.showmouse; }
      set { this.showmouse = value; }
    }

    /// <summary>
    /// Gets or sets a value indicating whether to capture mouse clicks.
    /// </summary>
    /// <value>A <see cref="bool"/> which is true when 
    /// the mouse clicks should be visible in the screen video.</value>
    public bool Showclicks
    {
      get { return this.showclicks; }
      set { this.showclicks = value; }
    }

    /// <summary>
    /// Gets or sets a value indicating whether to capture 
    /// layered windows (which can be not captured, but cursor will flashing)
    /// </summary>
    /// <value>A <see cref="bool"/> which is true when 
    /// the the video should capture layered windows.</value>
    public bool Caplayered
    {
      get { return this.caplayered; }
      set { this.caplayered = value; }
    }

    /// <summary>
    /// Gets or sets a value indicating whether to follow capture 
    /// area for mouse (when capture area greater stream size).
    /// Works when resize is disabled.
    /// </summary>
    /// <value>A <see cref="bool"/> which is true when 
    /// the capturing should follow the mouse cursor.</value>
    public bool Trackmouse
    {
      get { return this.trackmouse; }
      set { this.trackmouse = value; }
    }

    /// <summary>
    /// Gets or sets a value indicating whether to optimize capture
    /// from screen, capture only when detect changes in GUI.
    /// </summary>
    /// <value>A <see cref="bool"/> which is true when 
    /// the capturing saves CPU load by checking for GUI changes.</value>
    public bool Optcapture
    {
      get { return this.optcapture; }
      set { this.optcapture = value; }
    }

    /// <summary>
    /// Gets or sets a value indicating whether optimize frames sending 
    /// (it will send next frame if
    /// detect changes in GUI. if disabled, VHScrCap
    /// will still provide selected FPS)
    /// </summary>
    /// <value>A <see cref="bool"/> which is true when 
    /// the capturing should stream frames only when 
    /// GUI changes are detected.</value>
    public bool Optdeliver
    {
      get { return this.optdeliver; }
      set { this.optdeliver = value; }
    }

    /// <summary>
    /// Gets or sets a value indicating whether to register graph in ROT (Running Object Table, see MSDN).
    /// You can connect to registered remote graph with graphedit MS tool.
    /// </summary>
    /// <value>A <see cref="bool"/> which is true when 
    /// the capture graph should be registered in the ROT.</value>
    public bool Regrot
    {
      get { return this.regrot; }
      set { this.regrot = value; }
    }

    /// <summary>
    /// Gets or sets a value indicating whether to limit VHScrCap 
    /// running instances to 1 for each process (if enabled).
    /// </summary>
    /// <value>A <see cref="bool"/> which is true when 
    /// VHScrCap should have only 1 running instance.</value>
    public bool Limitinst
    {
      get { return this.limitinst; }
      set { this.limitinst = value; }
    }

    #endregion //PROPERTIES

    ///////////////////////////////////////////////////////////////////////////////
    // Public methods                                                            //
    ///////////////////////////////////////////////////////////////////////////////
    #region PUBLICMETHODS

    /// <summary>
    /// Deserializes the <see cref="ScreenCaptureSetting"/> from the given xml file.
    /// </summary>
    /// <param name="filePath">Full file path to the xml settings file.</param>
    public void Deserialize(string filePath)
    {
      try
      {
        // A FileStream is needed to read the XML document.
        FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);
        XmlTextReader reader = new XmlTextReader(fs);

        // Use the ReadXml method to restore the object's state with
        // data from the XML document.
        this.ReadXml(reader);

        reader.Close();

        fs.Close();
      }
      catch (Exception ex)
      {
        InformationDialog.Show(
          "Error occured",
          "Deserialization of ScreenCaptureSetting failed with the following message: " + Environment.NewLine + ex.Message,
          false,
          MessageBoxIcon.Error);
      }
    }

    /// <summary>
    /// Serializes the <see cref="ScreenCaptureSetting"/> into the given file in a xml structure.
    /// </summary>
    /// <param name="filePath">Full file path to the xml settings file.</param>
    public void Serialize(string filePath)
    {
      // Serialize the ScreenCaptureSetting, and close the XmlTextWriter.
      try
      {
        // Encode with iso-8859-1
        XmlTextWriter writer = new XmlTextWriter(filePath, System.Text.Encoding.GetEncoding(1252));

        // Write the XML declaration. 
        writer.WriteStartDocument();

        // Use indentation for readability.
        writer.Formatting = Formatting.Indented;
        writer.Indentation = 4;

        this.WriteXml(writer);

        writer.Close();
      }
      catch (Exception ex)
      {
        InformationDialog.Show(
          "Error occured",
          "Serialization of ScreenCaptureSetting failed with the following message: " + Environment.NewLine + ex.Message,
          false,
          MessageBoxIcon.Error);
      }
    }

    /// <summary>
    /// Implementation of <see cref="IXmlSerializable"/>.
    /// This property is reserved, apply the 
    /// <see cref="XmlSchemaProviderAttribute"/> to the class instead. 
    /// </summary>
    /// <returns>In this implementation it returns null, because we do not use it.</returns>
    public System.Xml.Schema.XmlSchema GetSchema()
    {
      return null;
    }

    /// <summary>
    /// Implementation of <see cref="IXmlSerializable"/>.
    /// Generates an <see cref="ScreenCaptureSetting"/> from its XML representation. 
    /// </summary>
    /// <param name="reader">The <see cref="System.Xml.XmlReader"/> stream from 
    /// which the <see cref="ScreenCaptureSetting"/> is deserialized. </param>
    public void ReadXml(System.Xml.XmlReader reader)
    {
      reader.ReadStartElement("configuration");

      // Read "params" attributes
      reader.MoveToContent();
      reader.MoveToFirstAttribute();
      this.sampleTime = Convert.ToInt32(reader.GetAttribute("sampletime"));

      // Skip event attribute
      reader.MoveToNextAttribute();
      reader.MoveToNextAttribute();
      this.mouseMagnitude = Convert.ToInt32(reader.GetAttribute("mousemagnitude"));

      // Jump back to params node
      reader.MoveToElement();
      reader.ReadStartElement("params");

      // Read "rect" attributes
      reader.MoveToContent();
      reader.MoveToFirstAttribute();
      this.trackRectangle.X = Convert.ToInt32(reader.GetAttribute("left"));
      reader.MoveToNextAttribute();
      this.trackRectangle.Y = Convert.ToInt32(reader.GetAttribute("top"));
      reader.MoveToNextAttribute();
      int right = Convert.ToInt32(reader.GetAttribute("right"));
      reader.MoveToNextAttribute();
      int bottom = Convert.ToInt32(reader.GetAttribute("bottom"));
      this.trackRectangle.Width = right - this.trackRectangle.X;
      this.trackRectangle.Height = bottom - this.trackRectangle.Y;

      // Jump back to rect node
      reader.MoveToElement();
      reader.ReadStartElement("rect");
      reader.MoveToContent();

      // Read "flags" attributes
      reader.MoveToFirstAttribute();
      this.align = Convert.ToInt32(reader.GetAttribute("align")) == 0 ? false : true;
      reader.MoveToNextAttribute();
      this.trackwnd = Convert.ToInt32(reader.GetAttribute("trackwnd")) == 0 ? false : true;
      reader.MoveToNextAttribute();
      this.trackscr = Convert.ToInt32(reader.GetAttribute("trackscr")) == 0 ? false : true;
      reader.MoveToNextAttribute();
      this.showmouse = Convert.ToInt32(reader.GetAttribute("showmouse")) == 0 ? false : true;
      reader.MoveToNextAttribute();
      this.showclicks = Convert.ToInt32(reader.GetAttribute("showclicks")) == 0 ? false : true;
      reader.MoveToNextAttribute();
      this.caplayered = Convert.ToInt32(reader.GetAttribute("caplayered")) == 0 ? false : true;
      reader.MoveToNextAttribute();
      this.trackmouse = Convert.ToInt32(reader.GetAttribute("trackmouse")) == 0 ? false : true;
      reader.MoveToNextAttribute();
      this.optcapture = Convert.ToInt32(reader.GetAttribute("optcapture")) == 0 ? false : true;
      reader.MoveToNextAttribute();
      this.optdeliver = Convert.ToInt32(reader.GetAttribute("optdeliver")) == 0 ? false : true;
      reader.MoveToNextAttribute();
      this.regrot = Convert.ToInt32(reader.GetAttribute("regrot")) == 0 ? false : true;
      reader.MoveToNextAttribute();
      this.limitinst = Convert.ToInt32(reader.GetAttribute("limitinst")) == 0 ? false : true;

      // Jump back to flags node
      reader.MoveToElement();
      reader.ReadStartElement("flags");

      reader.ReadEndElement(); // Params
      reader.ReadEndElement(); // configuration
    }

    /// <summary>
    /// Implementation of <see cref="IXmlSerializable"/>.
    /// Converts this <see cref="ScreenCaptureSetting"/> into its XML representation. 
    /// </summary>
    /// <param name="writer">The <see cref="System.Xml.XmlWriter"/> stream 
    /// to which the <see cref="ScreenCaptureSetting"/> is serialized.</param>
    public void WriteXml(System.Xml.XmlWriter writer)
    {
      writer.WriteStartElement("configuration");

      writer.WriteStartElement("params");
      writer.WriteAttributeString("sampletime", this.sampleTime.ToString());
      writer.WriteAttributeString("event", string.Empty);
      writer.WriteAttributeString("mousemagnitude", this.mouseMagnitude.ToString());

      writer.WriteStartElement("rect");
      writer.WriteAttributeString("left", this.trackRectangle.Left.ToString());
      writer.WriteAttributeString("top", this.trackRectangle.Top.ToString());
      writer.WriteAttributeString("right", this.trackRectangle.Right.ToString());
      writer.WriteAttributeString("bottom", this.trackRectangle.Bottom.ToString());
      writer.WriteEndElement();

      writer.WriteStartElement("flags");
      writer.WriteAttributeString("align", this.align ? "1" : "0");
      writer.WriteAttributeString("trackwnd", this.trackwnd ? "1" : "0");
      writer.WriteAttributeString("trackscr", this.trackscr ? "1" : "0");
      writer.WriteAttributeString("showmouse", this.showmouse ? "1" : "0");
      writer.WriteAttributeString("showclicks", this.showclicks ? "1" : "0");
      writer.WriteAttributeString("caplayered", this.caplayered ? "1" : "0");
      writer.WriteAttributeString("trackmouse", this.trackmouse ? "1" : "0");
      writer.WriteAttributeString("optcapture", this.optcapture ? "1" : "0");
      writer.WriteAttributeString("optdeliver", this.optdeliver ? "1" : "0");
      writer.WriteAttributeString("regrot", this.regrot ? "1" : "0");
      writer.WriteAttributeString("limitinst", this.limitinst ? "1" : "0");
      writer.WriteEndElement();

      writer.WriteEndElement(); // Params

      writer.WriteEndElement(); // configuration
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
    #region METHODS
    #endregion //METHODS

    ///////////////////////////////////////////////////////////////////////////////
    // Small helping Methods                                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region HELPER
    #endregion //HELPER
  }
}

