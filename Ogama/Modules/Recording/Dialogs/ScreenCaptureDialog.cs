// <copyright file="ScreenCaptureDialog.cs" company="FU Berlin">
// ******************************************************
// OGAMA - open gaze and mouse analyzer 
// Copyright (C) 2013 Dr. Adrian Voßkühler  
// ------------------------------------------------------------------------
// This program is free software; you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation; either version 2 of the License, or (at your option) any later version.
// This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
// You should have received a copy of the GNU General Public License along with this program; if not, write to the Free Software Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA 02111-1307 USA
// **************************************************************
// </copyright>
// <author>Adrian Voßkühler</author>
// <email>adrian@ogama.net</email>

namespace Ogama.Modules.Recording.Dialogs
{
  using System;
  using System.Runtime.InteropServices;
  using System.Windows.Forms;

  using DirectShowLib;

  using GTHardware.Cameras.DirectShow;

  using OgamaControls;

  /// <summary>
  /// This dialog <see cref="Form"/> shows the options available
  /// for screen capturing using the Ogama Screen Capture filter.
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
    /// The array of <see cref="DsDevice"/> containing the available
    /// video compressors of the system.
    /// </summary>
    private DsDevice[] videoCompressors;

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

      int index = 0;
      this.videoCompressors = DsDevice.GetDevicesOfCat(FilterCategory.VideoCompressorCategory);
      for (int i = 0; i < this.videoCompressors.Length; i++)
      {
        DsDevice compressor = this.videoCompressors[i];
        this.cbbEncoderFilter.Items.Add(compressor.Name);
        if (compressor.Name.Contains("ffdshow"))
        {
          index = i;
        }
      }

      this.cbbEncoderFilter.SelectedIndex = index;
      this.nudFPS.Value = (decimal)Document.ActiveDocument.ExperimentSettings.ScreenCaptureFramerate;
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
    /// Gets or sets the selected video compressor filter
    /// </summary>
    public string VideoCompressor
    {
      get
      {
        return this.videoCompressors[this.cbbEncoderFilter.SelectedIndex].Name;
      }

      set
      {
        for (int i = 0; i < this.videoCompressors.Length; i++)
        {
          if (value == this.videoCompressors[i].Name)
          {
            this.cbbEncoderFilter.SelectedIndex = i;
            break;
          }
        }
      }
    }

    /// <summary>
    /// Gets or sets the frame rate.
    /// </summary>
    public int FrameRate
    {
      get { return (int)this.nudFPS.Value; }
      set { this.nudFPS.Value = (decimal)value; }
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
    /// The <see cref="Control.Click"/> event handler for the
    /// <see cref="Button"/> <see cref="btnEncoderProperties"/>.
    /// Shows the property pages of the selected filter.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void btnEncoderProperties_Click(object sender, EventArgs e)
    {
      // Create the filter for the selected video compressor
      IBaseFilter compressorFilter = DirectShowUtils.CreateFilter(
        FilterCategory.VideoCompressorCategory,
        this.cbbEncoderFilter.Text);
      if (compressorFilter != null)
      {
        DirectShowUtils.DisplayPropertyPage(this.Handle, compressorFilter);
        Marshal.ReleaseComObject(compressorFilter);
      }
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