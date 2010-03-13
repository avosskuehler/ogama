// <copyright file="VHSettingsDialog.cs" company="FU Berlin">
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
  using System.Collections.Generic;
  using System.ComponentModel;
  using System.Data;
  using System.Drawing;
  using System.Text;
  using System.Windows.Forms;
  using System.Xml;
  using VHScrCapLib;

  /// <summary>
  /// This dialog <see cref="Form"/> is used to specify custom settings
  /// for the Hmelyoff VHScrCap direct show screen capture filter.
  /// </summary>
  /// <remarks>Have a look at the VHToolkit
  /// Copyright (c) 2004-2009 Vladimir Hmelyoff vh@hmelyoff.com
  /// </remarks>
  public partial class VHSettingsDialog : Form
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
    /// Initializes a new instance of the VHSettingsDialog class.
    /// </summary>
    public VHSettingsDialog()
    {
      this.InitializeComponent();
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
    /// Gets the <see cref="ScreenCaptureSetting"/> that is designed
    /// within this dialog.
    /// </summary>
    public ScreenCaptureSetting ScreenCaptureSetting
    {
      get { return this.GetScreenCaptureSetting(); }
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

    /// <summary>
    /// <see cref="Form.Load"/> event handler. 
    /// Initializes first display by reading the current settings
    /// of the filter
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void VHSettingsDialog_Load(object sender, EventArgs e)
    {
      this.QuerySettings();
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

    /// <summary>
    /// This method parses the current settings of the dialog
    /// into a new <see cref="ScreenCaptureSetting"/>.
    /// </summary>
    /// <returns>A <see cref="ScreenCaptureSetting"/> representing
    /// the status of the dialog.</returns>
    private ScreenCaptureSetting GetScreenCaptureSetting()
    {
      ScreenCaptureSetting setting = new ScreenCaptureSetting();
      setting.Align = this.chbAlign.Checked;
      setting.Caplayered = this.chbCapLayeredWindows.Checked;
      setting.Optcapture = this.chbOptCapture.Checked;
      setting.Optdeliver = this.chbOptDeliver.Checked;
      setting.Regrot = this.chbOptDeliver.Checked;
      int sampleTime = (int)(10000000000.0d / (double)(this.nudFPS.Value * 1000));
      setting.SampleTime = sampleTime;
      setting.Showclicks = this.chbShowclicks.Checked;
      setting.Showmouse = this.chbShowmouse.Checked;
      int left = Convert.ToInt32(this.txbLeft.Text);
      int top = Convert.ToInt32(this.txbTop.Text);
      int width = Convert.ToInt32(this.txbWidth.Text);
      int height = Convert.ToInt32(this.txbHeight.Text);
      setting.TrackRectangle = new Rectangle(left, top, width, height);

      return setting;
    }

    /// <summary>
    /// This method reads the current settings from a new <see cref="VHScrCapClass"/>
    /// and populates the dialog with the values.
    /// </summary>
    private void QuerySettings()
    {
      IVHScrCap screenCaptureFilter = new VHScrCapClass();
      XmlDocument doc = new XmlDocument();
      doc.LoadXml(screenCaptureFilter.sc_props);

      XmlNode ndparams = doc.SelectSingleNode("//params");
      XmlNode ndwindow = doc.SelectSingleNode("//params/window");
      XmlNode ndrect = doc.SelectSingleNode("//params/rect");
      XmlNode ndoutput = doc.SelectSingleNode("//params/output");
      XmlNode ndborder = doc.SelectSingleNode("//params/border");
      XmlNode ndflags = doc.SelectSingleNode("//params/flags");

      // ndparams
      double sampleTime = Convert.ToDouble(ndparams.Attributes.GetNamedItem("sampletime").Value);
      double fps = Convert.ToDouble(Convert.ToInt64(10000000000.0 / sampleTime) / 1000.0);
      this.nudFPS.Value = (decimal)fps;

      // ndrect
      this.txbLeft.Text = ndrect.Attributes.GetNamedItem("left").Value;
      this.txbTop.Text = ndrect.Attributes.GetNamedItem("top").Value;
      this.txbWidth.Text = Convert.ToString(
          Convert.ToInt32(ndrect.Attributes.GetNamedItem("right").Value) -
          Convert.ToInt32(ndrect.Attributes.GetNamedItem("left").Value));
      this.txbHeight.Text = Convert.ToString(
          Convert.ToInt32(ndrect.Attributes.GetNamedItem("bottom").Value) -
          Convert.ToInt32(ndrect.Attributes.GetNamedItem("top").Value));

      // ndflags
      this.chbAlign.Checked = Convert.ToByte(ndflags.Attributes.GetNamedItem("align").Value) != 0;
      this.chbShowmouse.Checked = Convert.ToByte(ndflags.Attributes.GetNamedItem("showmouse").Value) != 0;
      this.chbShowclicks.Checked = Convert.ToByte(ndflags.Attributes.GetNamedItem("showclicks").Value) != 0;
      this.chbCapLayeredWindows.Checked = Convert.ToByte(ndflags.Attributes.GetNamedItem("caplayered").Value) != 0;
      this.chbOptCapture.Checked = Convert.ToByte(ndflags.Attributes.GetNamedItem("optcapture").Value) != 0;
      this.chbOptDeliver.Checked = Convert.ToByte(ndflags.Attributes.GetNamedItem("optdeliver").Value) != 0;
      this.chbOptDeliver.Checked = Convert.ToByte(ndflags.Attributes.GetNamedItem("regrot").Value) != 0;

      this.chbShowclicks.Enabled = this.chbShowmouse.Checked;
      this.chbOptDeliver.Enabled = this.chbOptCapture.Checked;
    }

    #endregion //PRIVATEMETHODS

    ///////////////////////////////////////////////////////////////////////////////
    // Small helping Methods                                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region HELPER
    #endregion //HELPER
  }
}