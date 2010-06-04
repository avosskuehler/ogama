// <copyright file="ScreenCaptureDialog.cs" company="FU Berlin">
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
  using DirectX.Capture;
  using Ogama.ExceptionHandling;
  using VHScrCapLib;

  /// <summary>
  /// This dialog <see cref="Form"/> shows the options available
  /// for screen capturing using the Hmelyoff labs VHSrcCap filter.
  /// </summary>
  public partial class ScreenCaptureDialog : Form
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
    /// The <see cref="Filters"/> class containing valid direct show filters.
    /// </summary>
    private Filters filters;

    /// <summary>
    /// The frame rate to be used.
    /// </summary>
    private int frameRate;

    /// <summary>
    /// The <see cref="Size"/> of the captured video frames.
    /// </summary>
    private Size frameSize;

    #endregion //FIELDS

    ///////////////////////////////////////////////////////////////////////////////
    // Construction and Initializing methods                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region CONSTRUCTION

    /// <summary>
    /// Initializes a new instance of the ScreenCaptureDialog class.
    /// </summary>
    public ScreenCaptureDialog()
    {
      this.InitializeComponent();
      this.filters = new Filters();

      this.cbbCaptureFilter.Items.Add("None");
      int index = 0;
      this.cbbCaptureFilter.SelectedIndex = 0;
      for (int i = 0; i < this.filters.VideoInputDevices.Count; i++)
      {
        Filter inputFilter = this.filters.VideoInputDevices[i];
        this.cbbCaptureFilter.Items.Add(inputFilter.Name);
        if (inputFilter.Name.StartsWith("VHScr"))
        {
          index = i;
          this.cbbCaptureFilter.SelectedIndex = index + 1;
          break;
        }
      }

      index = 0;
      for (int i = 0; i < this.filters.VideoCompressors.Count; i++)
      {
        Filter compressor = this.filters.VideoCompressors[i];
        this.cbbEncoderFilter.Items.Add(compressor.Name);
        if (compressor.Name.Contains("ffdshow"))
        {
          index = i;
        }
      }

      this.cbbEncoderFilter.SelectedIndex = index;
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
    /// Gets or sets the selected video device <see cref="Filter"/>
    /// </summary>
    public string VideoDevice
    {
      get
      {
        int index = this.cbbCaptureFilter.SelectedIndex - 1;
        if (index >= 0)
        {
          return this.filters.VideoInputDevices[index].Name;
        }

        return null;
      }

      set
      {
        for (int i = 0; i < this.filters.VideoInputDevices.Count; i++)
        {
          if (value == this.filters.VideoInputDevices[i].Name)
          {
            this.cbbCaptureFilter.SelectedIndex = i + 1;
            break;
          }
        }
      }
    }

    /// <summary>
    /// Gets or sets the selected video compressor <see cref="Filter"/>
    /// </summary>
    public string VideoCompressor
    {
      get
      {
        return this.filters.VideoCompressors[this.cbbEncoderFilter.SelectedIndex].Name;
      }

      set
      {
        for (int i = 0; i < this.filters.VideoCompressors.Count; i++)
        {
          if (value == this.filters.VideoCompressors[i].Name)
          {
            this.cbbEncoderFilter.SelectedIndex = i;
            break;
          }
        }
      }
    }

    /// <summary>
    /// Gets the choosen frame rate.
    /// </summary>
    public int FrameRate
    {
      get { return this.frameRate; }
    }

    /// <summary>
    /// Gets the <see cref="Size"/> of the capture frame.
    /// </summary>
    public Size FrameSize
    {
      get { return this.frameSize; }
    }

    #endregion //PROPERTIES

    ///////////////////////////////////////////////////////////////////////////////
    // Public methods                                                            //
    ///////////////////////////////////////////////////////////////////////////////
    #region PUBLICMETHODS

    /// <summary>
    /// This method parses the Hmelyoff VHScrCap Properties.
    /// </summary>
    /// <param name="frameRate">Out. An <see cref="Int32"/> with the framerate.</param>
    /// <param name="frameSize">Out. An <see cref="Size"/> with the video frame size.</param>
    private static void GetVHScrCapProperties(out int frameRate, out Size frameSize)
    {
      IVHScrCap screenCapture = new VHScrCapClass();

      // Preset outs
      frameRate = 1;
      frameSize = Size.Empty;

      // Parse VHScrCaps
      XmlDocument doc = new XmlDocument();
      doc.LoadXml(screenCapture.sc_props);
      try
      {
        XmlNode ndparams = doc.SelectSingleNode("//params");
        XmlNode ndrect = doc.SelectSingleNode("//params/rect");

        // ndparams
        double fps = Convert.ToDouble(Convert.ToInt64(
            10000000000.0 / Convert.ToDouble(ndparams.Attributes.GetNamedItem("sampletime").Value))) / 1000.0;
        frameRate = Convert.ToInt32(fps);
        int top = Convert.ToInt32(ndrect.Attributes.GetNamedItem("top").Value);
        int left = Convert.ToInt32(ndrect.Attributes.GetNamedItem("left").Value);
        int bottom = Convert.ToInt32(ndrect.Attributes.GetNamedItem("bottom").Value);
        int right = Convert.ToInt32(ndrect.Attributes.GetNamedItem("right").Value);
        frameSize = new Size(right - left, bottom - top);
      }
      catch (Exception ex)
      {
        ExceptionMethods.HandleException(ex);
      }
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

    /// <summary>
    /// The <see cref="Control.Click"/> event handler for the
    /// <see cref="Button"/> <see cref="btnScreenCaptureProperties"/>.
    /// Shows the property pages of the selected filter.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void btnScreenCaptureProperties_Click(object sender, EventArgs e)
    {
      VHSettingsDialog dialog = new VHSettingsDialog();
      if (dialog.ShowDialog() == DialogResult.OK)
      {
        string xmlFile = Path.Combine(Properties.Settings.Default.VHScreenCaptureSettingsPath, "ogamasc.xml");
        dialog.ScreenCaptureSetting.Serialize(xmlFile);
      }
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler for the
    /// <see cref="Button"/> <see cref="btnEncoderProperties"/>.
    /// Shows the property pages of the selected filter.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void btnEncoderProperties_Click(object sender, EventArgs e)
    {
      int index = this.cbbCaptureFilter.SelectedIndex - 1;
      if (index >= 0)
      {
        Filter videoDevice = this.filters.VideoInputDevices[index];
        Filter compressor = this.filters.VideoCompressors[this.cbbEncoderFilter.SelectedIndex];
        DirectX.Capture.DXCapture capture = new DXCapture(videoDevice, null);
        capture.VideoCompressor = compressor;
        if (capture.PropertyPages.Count > 1)
        {
          capture.PropertyPages[1].Show(this);
        }

        capture.Dispose();
      }
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler for the
    /// <see cref="Button"/> <see cref="btnOK"/>.
    /// Queries current settings of VHScrCap filter.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void btnOK_Click(object sender, EventArgs e)
    {
      GetVHScrCapProperties(out this.frameRate, out this.frameSize);
    }

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