// <copyright file="VideoPropertiesDialog.cs" company="FU Berlin">
// ******************************************************
// OGAMA - open gaze and mouse analyzer 
// Copyright (C) 2015 Dr. Adrian Voßkühler  
// ------------------------------------------------------------------------
// This program is free software; you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation; either version 2 of the License, or (at your option) any later version.
// This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
// You should have received a copy of the GNU General Public License along with this program; if not, write to the Free Software Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA 02111-1307 USA
// **************************************************************
// </copyright>
// <author>Adrian Voßkühler</author>
// <email>adrian@ogama.net</email>

namespace Ogama.Modules.Replay.Video
{
  using System;
  using System.Collections.Generic;
  using System.Drawing;
  using System.IO;
  using System.Runtime.InteropServices;
  using System.Windows.Forms;

  using DirectShowLib;

  using GTHardware.Cameras.DirectShow;

  using Ogama.Modules.Common.FormTemplates;
  using Ogama.Modules.Common.SlideCollections;

  using OgamaControls;

  using VectorGraphics.Elements;

  /// <summary>
  /// Popup <see cref="Form"/>. Asks for the properties of the new video stream.
  /// </summary>
  public partial class VideoPropertiesDialog : FormWithPicture
  {
    ///////////////////////////////////////////////////////////////////////////////
    // Defining Constants                                                        //
    ///////////////////////////////////////////////////////////////////////////////
    #region CONSTANTS

    /// <summary>
    /// This static member defines a video size for the video
    /// export of 800x600 pixel.
    /// </summary>
    private Size defaultVideoSize1 = new Size(800, 600);

    /// <summary>
    /// This static member defines a video size for the video
    /// export of 768x576 pixel.
    /// </summary>
    private Size defaultVideoSize2 = new Size(768, 576);

    /// <summary>
    /// This static member defines a video size for the video
    /// export of 640x480 pixel.
    /// </summary>
    private Size defaultVideoSize3 = new Size(640, 480);

    /// <summary>
    /// This static member defines a video size for the video
    /// export of 320x240 pixel.
    /// </summary>
    private Size defaultVideoSize4 = new Size(320, 240);

    /// <summary>
    /// This static member defines a video size for the video
    /// export of 160x120 pixel.
    /// </summary>
    private Size defaultVideoSize5 = new Size(160, 120);

    #endregion //CONSTANTS

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Variables, Enumerations, Events                                  //
    ///////////////////////////////////////////////////////////////////////////////
    #region FIELDS

    /// <summary>
    /// The native aspect ratio of the gaze video
    /// </summary>
    private float aspectRatioGazeVideo;

    /// <summary>
    /// The native aspect ratio of the user video
    /// </summary>
    private float aspectRatioUserVideo;

    /// <summary>
    /// Indicates the availability of the user video.
    /// </summary>
    private bool isUserVideoAvailable;

    /// <summary>
    /// Flag, to be set when updating fields without
    /// updating value changed handler.
    /// </summary>
    private bool isInitializing;

    /// <summary>
    /// The <see cref="Size"/> of the output video stream
    /// </summary>
    private Size outputVideoSize;

    /// <summary>
    /// The <see cref="VGImage"/> with a preview of content
    /// and position of the gaze video.
    /// </summary>
    private VGImage gazeVideoRect;

    /// <summary>
    /// The <see cref="VGImage"/> with a preview of content
    /// and position of the user video.
    /// </summary>
    private VGImage userVideoRect;

    /// <summary>
    /// The <see cref="VideoExportProperties"/> to be designed in this form.
    /// </summary>
    private VideoExportProperties videoExportProperties;

    #endregion //FIELDS

    ///////////////////////////////////////////////////////////////////////////////
    // Construction and Initializing methods                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region CONSTRUCTION

    /// <summary>
    /// Initializes a new instance of the VideoPropertiesDialog class.
    /// </summary>
    public VideoPropertiesDialog()
    {
      this.InitializeComponent();
      this.Picture = this.picPreview;
      this.isInitializing = true;
      this.InitializeCustomElements();
    }

    #endregion //CONSTRUCTION

    ///////////////////////////////////////////////////////////////////////////////
    // Defining events, enums, delegates                                         //
    ///////////////////////////////////////////////////////////////////////////////
    #region EVENTS
    #endregion EVENTS

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Properties                                                       //
    ///////////////////////////////////////////////////////////////////////////////
    #region PROPERTIES

    /// <summary>
    /// Gets or sets the <see cref="VideoExportProperties"/> to be modified in this dialog.
    /// </summary>
    public VideoExportProperties VideoExportProperties
    {
      get
      {
        this.videoExportProperties.OutputVideoColor = this.clbBackground.CurrentColor;

        this.videoExportProperties.OutputVideoProperties = this.GetVideoProperties();

        this.videoExportProperties.GazeVideoProperties.IsStreamRendered = true;
        this.videoExportProperties.GazeVideoProperties.StreamAlpha = (float)this.nudGazeVideoAlpha.Value;
        this.videoExportProperties.GazeVideoProperties.StreamPosition = new RectangleF(
          (float)this.nudGazeVideoLeft.Value / this.outputVideoSize.Width,
          (float)this.nudGazeVideoTop.Value / this.outputVideoSize.Height,
          (float)this.nudGazeVideoWidth.Value / this.outputVideoSize.Width,
          (float)this.nudGazeVideoHeight.Value / this.outputVideoSize.Height);

        if (this.chbUserVideoVisible.Checked)
        {
          this.videoExportProperties.UserVideoProperties.IsStreamRendered = true;
          this.videoExportProperties.UserVideoProperties.StreamAlpha = (float)this.nudUserVideoAlpha.Value;
          this.videoExportProperties.UserVideoProperties.StreamPosition = new RectangleF(
            (float)this.nudUserVideoLeft.Value / this.outputVideoSize.Width,
            (float)this.nudUserVideoTop.Value / this.outputVideoSize.Height,
            (float)this.nudUserVideoWidth.Value / this.outputVideoSize.Width,
            (float)this.nudUserVideoHeight.Value / this.outputVideoSize.Height);
        }
        else
        {
          this.videoExportProperties.UserVideoProperties.IsStreamRendered = false;
        }

        return this.videoExportProperties;
      }

      set
      {
        this.videoExportProperties = value;
      }
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

    /// <summary>
    /// This methods is used to initialize elements that are not
    /// initialized in the designer.
    /// </summary>
    protected override void InitializeCustomElements()
    {
      base.InitializeCustomElements();
      this.pnlCanvas.Resize += new EventHandler(this.pnlCanvas_Resize);

      foreach (DsDevice filter in DsDevice.GetDevicesOfCat(FilterCategory.VideoCompressorCategory))
      {
        this.cbbVideoCompressor.Items.Add(filter.Name);
      }

      if (this.cbbVideoCompressor.Items.Count > 0)
      {
        this.cbbVideoCompressor.SelectedIndex = 0;
        int counter = 0;
        foreach (string item in this.cbbVideoCompressor.Items)
        {
          if (item.Contains("Microsoft Video 1"))
          {
            this.cbbVideoCompressor.SelectedIndex = counter;
            break;
          }

          counter++;
        }
      }
      else
      {
        this.cbbVideoCompressor.Text = "No video compressors found";
      }

      foreach (DsDevice filter in DsDevice.GetDevicesOfCat(FilterCategory.AudioCompressorCategory))
      {
        this.cbbAudioCompressor.Items.Add(filter.Name);
      }

      if (this.cbbAudioCompressor.Items.Count > 0)
      {
        this.cbbAudioCompressor.SelectedIndex = 0;
        int counter = 0;
        foreach (string item in this.cbbAudioCompressor.Items)
        {
          if (item == "PCM")
          {
            this.cbbAudioCompressor.SelectedIndex = counter;
            break;
          }

          counter++;
        }
      }
      else
      {
        this.cbbAudioCompressor.Text = "No audio compressors found";
      }

      this.outputVideoSize = new Size(
        Document.ActiveDocument.ExperimentSettings.WidthStimulusScreen,
        Document.ActiveDocument.ExperimentSettings.HeightStimulusScreen);
    }

    #endregion //OVERRIDES

    ///////////////////////////////////////////////////////////////////////////////
    // Eventhandler                                                              //
    ///////////////////////////////////////////////////////////////////////////////
    #region EVENTHANDLER

    /// <summary>
    /// The <see cref="Form.Load"/> event handler that initializes the UI.
    /// </summary>
    /// <param name="sender">Source of the event</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void VideoPropertiesDialog_Load(object sender, EventArgs e)
    {
      // Intialize picture
      this.picPreview.PresentationSize = this.outputVideoSize;
      this.ResizeCanvas();

      this.gazeVideoRect = new VGImage(this.videoExportProperties.GazeVideoProperties.StreamScreenshot, ImageLayout.Stretch, this.outputVideoSize);
      this.gazeVideoRect.Name = this.videoExportProperties.GazeVideoProperties.StreamName;
      this.picPreview.Elements.Add(this.gazeVideoRect);

      if (File.Exists(this.videoExportProperties.UserVideoProperties.StreamFilename))
      {
        this.userVideoRect = new VGImage(this.videoExportProperties.UserVideoProperties.StreamScreenshot, ImageLayout.Stretch, this.outputVideoSize);
        this.userVideoRect.Name = this.videoExportProperties.UserVideoProperties.StreamName;
        this.picPreview.Elements.Add(this.userVideoRect);
        this.isUserVideoAvailable = true;
      }
      else
      {
        this.grbUserVideoOptions.Visible = false;
        this.chbUserVideoVisible.Checked = false;
        this.isUserVideoAvailable = false;
        this.grpGazeMouseOptions.Visible = false;
        this.chbUserVideoVisible.Visible = false;
        this.label13.Visible = false;
      }

      this.PopulateDefaultProperties();

      this.UpdatePositionNumerics();
      this.isInitializing = false;
      this.UpdatePreview();
    }

    /// <summary>
    /// Event handler for value changed events which updates the preview panel.
    /// </summary>
    /// <param name="sender">Source of the event</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void GazeMouseVideo_ValueChanged(object sender, EventArgs e)
    {
      this.UpdatePreview();
    }

    /// <summary>
    /// The <see cref="NumericUpDown.ValueChanged"/> event handler
    /// which updates the preview and check for correct aspect ratio.
    /// </summary>
    /// <param name="sender">Source of the event</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void nudGazeVideoWidth_ValueChanged(object sender, EventArgs e)
    {
      if (this.chbGazeVideoKeepAspect.Checked && !this.isInitializing)
      {
        this.isInitializing = true;
        this.nudGazeVideoHeight.Value = (int)(this.nudGazeVideoWidth.Value / (decimal)this.aspectRatioGazeVideo);
        this.isInitializing = false;
      }

      this.UpdatePreview();
    }

    /// <summary>
    /// The <see cref="NumericUpDown.ValueChanged"/> event handler
    /// which updates the preview and check for correct aspect ratio.
    /// </summary>
    /// <param name="sender">Source of the event</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void nudGazeVideoHeight_ValueChanged(object sender, EventArgs e)
    {
      if (this.chbGazeVideoKeepAspect.Checked && !this.isInitializing)
      {
        this.isInitializing = true;
        this.nudGazeVideoWidth.Value = (int)(this.nudGazeVideoHeight.Value * (decimal)this.aspectRatioGazeVideo);
        this.isInitializing = false;
      }

      this.UpdatePreview();
    }

    /// <summary>
    /// The <see cref="NumericUpDown.ValueChanged"/> event handler
    /// which updates the preview and check for correct aspect ratio.
    /// </summary>
    /// <param name="sender">Source of the event</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void nudUserVideoWidth_ValueChanged(object sender, EventArgs e)
    {
      if (this.chbUserVideoKeepAspect.Checked && this.isUserVideoAvailable && !this.isInitializing)
      {
        this.isInitializing = true;
        this.nudUserVideoHeight.Value = (int)(this.nudUserVideoWidth.Value / (decimal)this.aspectRatioUserVideo);
        this.isInitializing = false;
      }

      this.UpdatePreview();
    }

    /// <summary>
    /// The <see cref="NumericUpDown.ValueChanged"/> event handler
    /// which updates the preview and check for correct aspect ratio.
    /// </summary>
    /// <param name="sender">Source of the event</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void nudUserVideoHeight_ValueChanged(object sender, EventArgs e)
    {
      if (this.chbUserVideoKeepAspect.Checked && this.isUserVideoAvailable && !this.isInitializing)
      {
        this.isInitializing = true;
        this.nudUserVideoWidth.Value = (int)(this.nudUserVideoHeight.Value * (decimal)this.aspectRatioUserVideo);
        this.isInitializing = false;
      }

      this.UpdatePreview();
    }

    /// <summary>
    /// The <see cref="CheckBox.CheckedChanged"/> event handler
    /// wfor the user video visible checkbox, changing the layout of the form.
    /// </summary>
    /// <param name="sender">Source of the event</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void chbUserVideoVisible_CheckedChanged(object sender, EventArgs e)
    {
      this.grbUserVideoOptions.Visible = this.chbUserVideoVisible.Checked;
      this.grpGazeMouseOptions.Visible = this.chbUserVideoVisible.Checked;
      this.UpdatePreview();

      if (!this.chbUserVideoVisible.Checked)
      {
        this.isInitializing = true;
        this.UpdatePositionNumerics();
        this.isInitializing = false;
        this.UpdatePreview();
      }
    }

    /// <summary>
    /// The <see cref="ComboBox.SelectedIndexChanged"/> event handler
    /// which updates the position and size numerics of both streams.
    /// </summary>
    /// <param name="sender">Source of the event</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void cbbVideoSize_SelectedIndexChanged(object sender, EventArgs e)
    {
      this.isInitializing = true;
      if (this.cbbVideoSize.SelectedItem != null)
      {
        string[] sizes = this.cbbVideoSize.SelectedItem.ToString().Split('x');
        this.outputVideoSize = new Size(Convert.ToInt32(sizes[0]), Convert.ToInt32(sizes[1]));
      }

      // Reintialize picture
      this.picPreview.PresentationSize = this.outputVideoSize;
      this.ResizeCanvas();

      this.UpdatePositionNumerics();
      this.isInitializing = false;
      this.UpdatePreview();
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler for the video
    /// compressor options button which shows the property page for
    /// the selected compressor.
    /// </summary>
    /// <param name="sender">Source of the event</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void btnVideoCompressorProperties_Click(object sender, EventArgs e)
    {
      // Create the filter for the selected video compressor
      IBaseFilter compressorFilter = DirectShowUtils.CreateFilter(
        FilterCategory.VideoCompressorCategory,
        this.cbbVideoCompressor.Text);

      // Show property page
      if (compressorFilter != null)
      {
        DirectShowUtils.DisplayPropertyPage(this.Handle, compressorFilter);
        Marshal.ReleaseComObject(compressorFilter);
      }
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler for the audio
    /// compressor options button which shows the property page for
    /// the selected compressor.
    /// </summary>
    /// <param name="sender">Source of the event</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void btnAudioCompressorProperties_Click(object sender, EventArgs e)
    {
      // Create the filter for the selected video compressor
      IBaseFilter compressorFilter = DirectShowUtils.CreateFilter(
        FilterCategory.AudioCompressorCategory,
        this.cbbAudioCompressor.Text);

      // Show property page
      if (compressorFilter != null)
      {
        DirectShowUtils.DisplayPropertyPage(this.Handle, compressorFilter);
        Marshal.ReleaseComObject(compressorFilter);
      }
    }

    #endregion //EVENTHANDLER

    ///////////////////////////////////////////////////////////////////////////////
    // Methods and Eventhandling for Background tasks                            //
    ///////////////////////////////////////////////////////////////////////////////
    #region THREAD
    #endregion //THREAD

    ///////////////////////////////////////////////////////////////////////////////
    // Methods for doing main class job                                          //
    ///////////////////////////////////////////////////////////////////////////////
    #region PRIVATEMETHODS

    /// <summary>
    /// This method fills the <see cref="CaptureDeviceProperties"/> according to the current selections.
    /// </summary>
    /// <returns>The <see cref="CaptureDeviceProperties"/> according to the current selections.</returns>
    private CaptureDeviceProperties GetVideoProperties()
    {
      CaptureDeviceProperties currentProperties = new CaptureDeviceProperties();

      if (this.cbbVideoCompressor.SelectedItem != null)
      {
        currentProperties.VideoCompressor = this.cbbVideoCompressor.SelectedItem.ToString();
      }

      if (this.cbbAudioCompressor.SelectedItem != null)
      {
        currentProperties.AudioCompressor = this.cbbAudioCompressor.SelectedItem.ToString();
      }

      if (this.cbbVideoSize.SelectedItem != null)
      {
        string[] sizes = this.cbbVideoSize.SelectedItem.ToString().Split('x');
        currentProperties.VideoSize = new Size(Convert.ToInt32(sizes[0]), Convert.ToInt32(sizes[1]));
      }

      currentProperties.FrameRate = (int)this.nudFrameRate.Value;

      return currentProperties;
    }

    /// <summary>
    /// Populates the video sizes combo with available sizes.
    /// </summary>
    private void PopulateDefaultProperties()
    {
      // Erase old items
      this.cbbVideoSize.Items.Clear();

      List<Size> defaultSizes = new List<Size>();
      defaultSizes.Add(this.defaultVideoSize1);
      defaultSizes.Add(this.defaultVideoSize2);
      defaultSizes.Add(this.defaultVideoSize3);
      defaultSizes.Add(this.defaultVideoSize4);
      defaultSizes.Add(this.defaultVideoSize5);

      if (!defaultSizes.Contains(this.outputVideoSize))
      {
        defaultSizes.Add(this.outputVideoSize);
      }

      Trial exportTrial =
        Document.ActiveDocument.ExperimentSettings.SlideShow.GetTrialByID(
        Document.ActiveDocument.SelectionState.TrialID);

      Size maxSize = exportTrial[0].StimulusSize;
      if (!defaultSizes.Contains(maxSize))
      {
        defaultSizes.Add(maxSize);
      }

      foreach (Size entry in defaultSizes)
      {
        this.cbbVideoSize.Items.Add(entry.Width.ToString() + " x " + entry.Height.ToString());
      }

      this.cbbVideoSize.SelectedIndex = 0;
    }

    /// <summary>
    /// Updates the max and values of the position and size numeric up and downs
    /// for both video streams.
    /// </summary>
    private void UpdatePositionNumerics()
    {
      this.aspectRatioGazeVideo = (float)this.outputVideoSize.Width / this.outputVideoSize.Height;

      this.nudGazeVideoHeight.Maximum = this.outputVideoSize.Height;
      this.nudGazeVideoWidth.Maximum = this.outputVideoSize.Width;
      this.nudGazeVideoLeft.Maximum = this.outputVideoSize.Width;
      this.nudGazeVideoTop.Maximum = this.outputVideoSize.Height;
      this.nudGazeVideoHeight.Value = this.outputVideoSize.Height;
      this.nudGazeVideoWidth.Value = this.outputVideoSize.Width;

      if (this.isUserVideoAvailable)
      {
        Size userVideoStreamSize = this.videoExportProperties.UserVideoProperties.StreamSize;
        this.aspectRatioUserVideo = (float)userVideoStreamSize.Width / userVideoStreamSize.Height;
        this.nudUserVideoHeight.Maximum = userVideoStreamSize.Height;
        this.nudUserVideoWidth.Maximum = userVideoStreamSize.Width;
        this.nudUserVideoLeft.Maximum = this.outputVideoSize.Width;
        this.nudUserVideoTop.Maximum = this.outputVideoSize.Height;
        this.nudUserVideoHeight.Value = Math.Min(userVideoStreamSize.Height, this.outputVideoSize.Height);
        this.nudUserVideoWidth.Value = Math.Min(userVideoStreamSize.Width, this.outputVideoSize.Width);
      }
    }

    /// <summary>
    /// This method updates the preview images with new position and size of
    /// both streams.
    /// </summary>
    private void UpdatePreview()
    {
      if (!this.isInitializing)
      {
        this.isInitializing = true;
        this.Picture.BackColor = this.clbBackground.CurrentColor;

        // Update preview
        this.gazeVideoRect.Location = new PointF((float)this.nudGazeVideoLeft.Value, (float)this.nudGazeVideoTop.Value);
        this.gazeVideoRect.Size = new SizeF((float)this.nudGazeVideoWidth.Value, (float)this.nudGazeVideoHeight.Value);
        this.gazeVideoRect.Alpha = (float)this.nudGazeVideoAlpha.Value;

        if (this.isUserVideoAvailable)
        {
          this.userVideoRect.Visible = this.chbUserVideoVisible.Checked;
          this.userVideoRect.Location = new PointF((float)this.nudUserVideoLeft.Value, (float)this.nudUserVideoTop.Value);
          this.userVideoRect.Size = new SizeF((float)this.nudUserVideoWidth.Value, (float)this.nudUserVideoHeight.Value);
          this.userVideoRect.Alpha = (float)this.nudUserVideoAlpha.Value;
        }

        // Redraw picture.
        this.Picture.DrawForeground(true);
        this.isInitializing = false;
      }
    }

    #endregion //PRIVATEMETHODS

    ///////////////////////////////////////////////////////////////////////////////
    // Small helping Methods                                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region HELPER
    #endregion //HELPER
  }
}