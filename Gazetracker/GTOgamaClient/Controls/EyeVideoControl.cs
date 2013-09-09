// <copyright file="EyeVideoControl.cs" company="Freie Universität Berlin">        
// OGAMA - open gaze and mouse analyzer
// Copyright (C) 2011 Dr. Adrian Voßkühler
// -----------------------------------------------------------------------
// This program is free software; you can redistribute it and/or modify 
// it under the terms of the GNU General Public License as published  
// by the Free Software Foundation; either version 2 of the License, or 
// (at your option) any later version. This program is distributed in the 
// hope that it will be useful, but WITHOUT ANY WARRANTY; without 
// even the implied warranty of MERCHANTABILITY or FITNESS FOR A
// PARTICULAR PURPOSE. 
// See the GNU General Public License for more details.
// ***********************************************************************
// </copyright>
// <author>Adrian Voßkühler</author>
// <email>adrian@ogama.net</email>

namespace GTOgamaClient.Controls
{
  using System;
  using System.ComponentModel;
  using System.Diagnostics;
  using System.Windows.Forms;

  using Emgu.CV.UI;

  using GTLibrary;
  using GTLibrary.Logging;

  using GTCommons.Enum;

  using GTSettings;

  using Camera = GTHardware.Camera;

  /// <summary>
  /// This WinForms <see cref="UserControl"/> is created to be integrated
  ///   into OGAMAs recording module displaying the current track status,
  ///   resp. the processed eye video of the ITU GazeTracker
  /// </summary>
  public partial class EyeVideoControl : UserControl
  {
    ///////////////////////////////////////////////////////////////////////////////
    // Defining Constants                                                        //
    ///////////////////////////////////////////////////////////////////////////////
    #region Constants and Fields

    /// <summary>
    ///   The default video image height.
    /// </summary>
    private const int DefaultVideoImageHeight = 180;

    /// <summary>
    ///   The default video image width.
    /// </summary>
    private const int DefaultVideoImageWidth = 240;

    /// <summary>
    ///   The cpu load.
    /// </summary>
    private double cpuLoad;

    /// <summary>
    ///   counter for CPU reduction
    /// </summary>
    private int drawImageOnCounter;

    /// <summary>
    ///   current displayed frames per second
    /// </summary>
    private int fps;

    /// <summary>
    ///   second counter for CPU reduction
    /// </summary>
    private int imageCounter;

    /// <summary>
    ///   Flag, indicating whether this control should display
    ///   the video in native resolution (don´t resize to 
    ///   fit image control)
    /// </summary>
    private bool isNativeResolution;

    /// <summary>
    ///   Indicates whether this control is beeing updated
    /// </summary>
    private bool isRendering;

    ///// <summary>
    /////   The mem load.
    ///// </summary>
    //private double memLoad;

    /// <summary>
    ///   The pc cpu.
    /// </summary>
    private PerformanceCounter cpuPerformance;

    ///// <summary>
    /////   The pc mem.
    ///// </summary>
    //private PerformanceCounter memoryPerformance;

    /// <summary>
    ///   The process.
    /// </summary>
    private Process process;

    /// <summary>
    ///   The sample counter.
    /// </summary>
    private long sampleCounter;

    /// <summary>
    /// Stops updating if set
    /// </summary>
    private bool interruptImmediately;

    #endregion

    ///////////////////////////////////////////////////////////////////////////////
    // Construction and Initializing methods                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region Constructors and Destructors

    /// <summary>
    ///   Initializes a new instance of the EyeVideoControl class.
    /// </summary>
    public EyeVideoControl()
    {
      this.InitializeComponent();
      this.VideoImageHeight = DefaultVideoImageHeight;
      this.VideoImageWidth = DefaultVideoImageWidth;
      this.isNativeResolution = false;
      this.isRendering = false;
      this.drawImageOnCounter = 1;
      this.imageCounter = 0;
      this.fps = 0;
    }

    /// <summary>
    /// Finalizes an instance of the EyeVideoControl class.
    /// </summary>
    ~EyeVideoControl()
    {
      this.Stop();
    }

    #endregion

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Properties                                                       //
    ///////////////////////////////////////////////////////////////////////////////
    #region Public Properties

    /// <summary>
    ///   Gets the OpenCV image control that displays the image.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public ImageBox CVImageBox
    {
      get
      {
        return this.pictureBox;
      }
    }

    /// <summary>
    ///   Gets or sets a value indicating whether this control should display
    ///   the video in native resolution (don´t resize to 
    ///   fit image control)
    /// </summary>
    public bool IsNativeResolution
    {
      get
      {
        return this.isNativeResolution;
      }

      set
      {
        this.isNativeResolution = value;
      }
    }

    /// <summary>
    ///   Sets the height of the video image.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public int VideoImageHeight
    {
      set
      {
        this.pictureBox.Height = value;
      }
    }

    /// <summary>
    ///   Sets the width of the video image.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public int VideoImageWidth
    {
      set
      {
        this.pictureBox.Width = value;
      }
    }

    #endregion

    ///////////////////////////////////////////////////////////////////////////////
    // Public methods                                                            //
    ///////////////////////////////////////////////////////////////////////////////
    #region Public Methods

    /// <summary>
    /// Starts updating the image by subscribing to
    ///   the trackers FrameCaptureComplete event.
    /// </summary>
    public void Start()
    {
      this.interruptImmediately = false;

      if (!this.isRendering)
      {
        Tracker.Instance.OnProcessedFrame += this.Tracker_FrameCaptureComplete;
        this.isRendering = true;
      }
    }

    /// <summary>
    /// Stops updating the image by unsubscribing 
    ///   from the trackers FrameCaptureComplete event.
    /// </summary>
    public void Stop()
    {
      if (this.isRendering)
      {
        Tracker.Instance.OnProcessedFrame -= this.Tracker_FrameCaptureComplete;
        this.isRendering = false;
      }
    }

    /// <summary>
    /// This method displays the initial ogama client image in the eye video control.
    /// </summary>
    public void ShowInitialImage()
    {
      this.Stop();
      this.interruptImmediately = true;
      var initialImage = new Emgu.CV.Image<Emgu.CV.Structure.Bgr, byte>(Properties.Resources.StartUp);
      this.pictureBox.Image = initialImage.Resize(
        this.CVImageBox.Width,
        this.CVImageBox.Height,
        Emgu.CV.CvEnum.INTER.CV_INTER_AREA);

      this.pictureBox.Refresh();
    }

    #endregion

    #region Methods

    /// <summary>
    /// Gets the current CPU load.
    /// </summary>
    /// <param name="trackingFPS">
    /// The tracking fps.
    /// </param>
    /// <returns>The current cpu load.</returns>
    private double GetCPULoad(double trackingFPS)
    {
      this.process = Process.GetCurrentProcess();

      if (this.cpuPerformance == null)
      {
        this.cpuPerformance = new PerformanceCounter("Process", "% Processor Time", this.process.ProcessName);
        this.cpuPerformance.NextValue();
        //this.memoryPerformance = new PerformanceCounter("Memory", "Available MBytes");
        //this.memoryPerformance.NextValue();
      }

      this.sampleCounter++;

      // Get CPU time (once per second)
      if (this.sampleCounter > trackingFPS)
      {
        this.cpuLoad = this.cpuPerformance.NextValue();
        //this.memLoad = this.process.PrivateMemorySize64 / 1024 / 1024; // Kb/Mb.
        this.sampleCounter = 0;
      }

      return Math.Round(this.cpuLoad / Environment.ProcessorCount, 0);
    }

    /// <summary>
    /// The event handler for the FrameCaptureComplete
    /// event which updates the OpenCV image control with the new frame
    /// according to the current display settings.
    /// </summary>
    /// <param name="sender">
    /// The sender.
    /// </param>
    /// <param name="args">
    /// The args.
    /// </param>
    private void Tracker_FrameCaptureComplete(object sender, EventArgs args)
    {
      // Don't draw while calibrating to obtain maximum images
      if (Tracker.Instance.IsCalibrating)
      {
        return;
      }

      if (Camera.Instance.DeviceType != Camera.DeviceTypeEnum.DirectShow)
      {
        this.imageCounter++;

        // If fps changed by more than +-10, determine new skipping
        if (this.fps < Camera.Instance.Device.FPS - 10 || this.fps > Camera.Instance.Device.FPS + 10)
        {
          this.fps = Camera.Instance.Device.FPS;

          if (this.fps < 30)
          {
            this.drawImageOnCounter = 1;
          }
          else
          {
            this.drawImageOnCounter = Convert.ToInt32(this.fps / 24); // Target visualization @ 24 fps
          }
        }

        if (this.imageCounter < this.drawImageOnCounter)
        {
          return;
        }

        this.imageCounter = 0;
      }

      try
      {
        if (this.pictureBox.InvokeRequired)
        {
          this.pictureBox.BeginInvoke(new MethodInvoker(this.UpdateImage));
        }
        else
        {
          this.UpdateImage();
        }
      }
      catch (Exception ex)
      {
        ErrorLogger.ProcessException(ex, false);
      }
    }

    /// <summary>
    /// The update image.
    /// </summary>
    private void UpdateImage()
    {
      if (this.interruptImmediately)
      {
        return;
      }

      this.pictureBox.Image = null;

      if (this.isNativeResolution)
      {
        if (Settings.Instance.Visualization.VideoMode == VideoModeEnum.Processed)
        {
          this.pictureBox.Image = Tracker.Instance.GetProcessedImage();
        }
        else
        {
          this.pictureBox.Image = Tracker.Instance.GetGrayImage();
        }
      }
      else
      {
        if (Settings.Instance.Visualization.VideoMode == VideoModeEnum.Processed)
        {
          this.pictureBox.Image = Tracker.Instance.GetProcessedImage(this.pictureBox.Width, this.pictureBox.Height);
        }
        else
        {
          this.pictureBox.Image = Tracker.Instance.GetGrayImage(this.pictureBox.Width, this.pictureBox.Height);
        }
      }

      this.UpdatePerformance();
    }

    /// <summary>
    /// The update performance.
    /// </summary>
    private void UpdatePerformance()
    {
      var videoFPS = Tracker.Instance.FPSVideo;
      var trackingFPS = Tracker.Instance.FPSTracking;

      // Set labels
      this.lblFPS.Text = videoFPS.ToString("N0") + "/" + trackingFPS.ToString("N0");
      this.lblCPU.Text = this.GetCPULoad(trackingFPS).ToString("N0") + "%";
      //this.lblMem.Text = this.memLoad.ToString("N0") + "Mb";
    }

    /// <summary>
    /// The rdb mode_ checked changed.
    /// </summary>
    /// <param name="sender">
    /// The sender.
    /// </param>
    /// <param name="e">
    /// The e.
    /// </param>
    private void RdbModeCheckedChanged(object sender, EventArgs e)
    {
      if (this.rdbNormal.Checked)
      {
        Settings.Instance.Visualization.VideoMode = VideoModeEnum.Normal;
      }
      else if (this.rdbProcessed.Checked)
      {
        Settings.Instance.Visualization.VideoMode = VideoModeEnum.Processed;
      }
      else if (this.rdbRaw.Checked)
      {
        Settings.Instance.Visualization.VideoMode = VideoModeEnum.RawNoTracking;
      }
    }

    #endregion
  }
}