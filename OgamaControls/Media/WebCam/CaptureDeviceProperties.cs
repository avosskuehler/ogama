// <copyright file="CaptureDeviceProperties.cs" company="FU Berlin">
// Copyright (c) 2008 All Rights Reserved
// </copyright>
// <author>Adrian Voßkühler</author>
// <email>adrian@ogama.net</email>
namespace OgamaControls
{
  using System;
  using System.Drawing;
  using System.Windows.Forms;

  using GTHardware.Cameras.DirectShow;

  /// <summary>
  /// This class holds the properties of a capture device
  /// like a webcam or a screen recorder.
  /// </summary>
  public class CaptureDeviceProperties : ICloneable
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
    /// Saves the the friendly name of the video input device to use.
    /// </summary>
    private CameraInfo videoInputDevice;

    /// <summary>
    /// Saves the friendly name of the audio input device to use.
    /// </summary>
    private string audioInputDevice;

    /// <summary>
    /// Saves the friendly name of the video compressor to use.
    /// </summary>
    private string videoCompressor;

    /// <summary>
    /// Saves the friendly name of the audio compressor to use.
    /// </summary>
    private string audioCompressor;

    /// <summary>
    /// Saves the frame rate for the video stream.
    /// </summary>
    private int frameRate;

    /// <summary>
    /// Saves the size of the video stream.
    /// </summary>
    private Size videoSize;

    /// <summary>
    /// Saves the filename for the video recording.
    /// </summary>
    private string filename;

    /// <summary>
    /// Saves the <see cref="CaptureMode"/> for the device.
    /// </summary>
    private CaptureMode captureMode;

    /// <summary>
    /// Saves the <see cref="Control"/> for the preview window.
    /// Null if it should not be previewed.
    /// </summary>
    private Control previewWindow;

    #endregion //FIELDS

    ///////////////////////////////////////////////////////////////////////////////
    // Construction and Initializing methods                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region CONSTRUCTION

    /// <summary>
    /// Initializes a new instance of the CaptureDeviceProperties class.
    /// </summary>
    public CaptureDeviceProperties()
    {
    }

    /// <summary>
    /// Initializes a new instance of the CaptureDeviceProperties class.
    /// </summary>
    /// <param name="newVideoInputDevice">The <see cref="CameraInfo"/> with the video input device to use.</param>
    /// <param name="newAudioInputDevice">The friendly name of the audio input device to use.</param>
    /// <param name="newVideoCompressor">The friendly name of the video compressor to use.</param>
    /// <param name="newAudioCompressor">The friendly name of the audio compressor to use.</param>
    /// <param name="newFrameRate">The frame rate for the video stream.</param>
    /// <param name="newVideoSize">The size of the video stream.</param>
    /// <param name="newFilename">The filename for the video recording.</param>
    /// <param name="newCaptureMode">The <see cref="CaptureMode"/> flags for the recording.</param>
    /// <param name="newPreviewWindow">The <see cref="Control"/> where the preview
    /// should be shown or null, if no preview should be shown.</param>
    public CaptureDeviceProperties(
      CameraInfo newVideoInputDevice,
      string newAudioInputDevice,
      string newVideoCompressor,
      string newAudioCompressor,
      int newFrameRate,
      Size newVideoSize,
      string newFilename,
      CaptureMode newCaptureMode,
      Control newPreviewWindow)
    {
      this.videoInputDevice = newVideoInputDevice;
      this.videoCompressor = newVideoCompressor;
      this.audioInputDevice = newAudioInputDevice;
      this.audioCompressor = newAudioCompressor;
      this.frameRate = newFrameRate;
      this.videoSize = newVideoSize;
      this.filename = newFilename;
      this.captureMode = newCaptureMode;
      this.previewWindow = newPreviewWindow;
    }

    /// <summary>
    /// Initializes a new instance of the WebcamProperties class.
    /// </summary>
    /// <param name="newVideoInputDevice">The friendly name of the video input device to use.</param>
    /// <param name="newAudioInputDevice">The friendly name of the audio input device to use.</param>
    /// <param name="newVideoCompressor">The friendly name of the video compressor to use.</param>
    /// <param name="newAudioCompressor">The friendly name of the audio compressor to use.</param>
    /// <param name="newFrameRate">The frame rate for the video stream.</param>
    /// <param name="newVideoSize">The size of the video stream.</param>
    /// <param name="newFilename">The filename for the video recording.</param>
    /// <param name="newCaptureMode">The <see cref="CaptureMode"/> flags for the recording.</param>
    /// <param name="newPreviewWindow">The <see cref="Control"/> where the preview
    /// should be shown or null, if no preview should be shown.</param>
    public CaptureDeviceProperties(
      string newVideoInputDevice,
      string newAudioInputDevice,
      string newVideoCompressor,
      string newAudioCompressor,
      int newFrameRate,
      Size newVideoSize,
      string newFilename,
      CaptureMode newCaptureMode,
      Control newPreviewWindow)
   {
      this.videoInputDevice = new CameraInfo();
      this.videoInputDevice.Name = newVideoInputDevice;
      this.videoCompressor = newVideoCompressor;
      this.audioInputDevice = newAudioInputDevice;
      this.audioCompressor = newAudioCompressor;
      this.frameRate = newFrameRate;
      this.videoSize = newVideoSize;
      this.filename = newFilename;
      this.captureMode = newCaptureMode;
      this.previewWindow = newPreviewWindow;
   }

    #endregion //CONSTRUCTION

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Properties                                                       //
    ///////////////////////////////////////////////////////////////////////////////
    #region PROPERTIES

    /// <summary>
    /// Gets or sets the friendly name of the video input device to use.
    /// </summary>
    /// <value>A <see cref="string"/> with the name of the video input device as
    /// been enumerated via DirectShow.</value>
    public CameraInfo VideoInputDevice
    {
      get { return this.videoInputDevice; }
      set { this.videoInputDevice = value; }
    }

    /// <summary>
    /// Gets or sets the friendly name of the audio input device to use.
    /// </summary>
    /// <value>A <see cref="string"/> with the name of the audio input device as
    /// been enumerated via DirectShow.</value>
    public string AudioInputDevice
    {
      get { return this.audioInputDevice; }
      set { this.audioInputDevice = value; }
    }

    /// <summary>
    /// Gets or sets the friendly name of the video compressor to use.
    /// </summary>
    /// <value>A <see cref="string"/> with the name of the video compressor as
    /// been enumerated via DirectShow.</value>
    public string VideoCompressor
    {
      get { return this.videoCompressor; }
      set { this.videoCompressor = value; }
    }

    /// <summary>
    /// Gets or sets the friendly name of the audio compressor to use.
    /// </summary>
    /// <value>A <see cref="string"/> with the name of the audio compressor as
    /// been enumerated via DirectShow.</value>
    public string AudioCompressor
    {
      get { return this.audioCompressor; }
      set { this.audioCompressor = value; }
    }

    /// <summary>
    /// Gets or sets the frame rate for the video stream.
    /// </summary>
    /// <value>A <see cref="int"/> with the new frame rate for the video stream.</value>
    public int FrameRate
    {
      get { return this.frameRate; }
      set { this.frameRate = value; }
    }

    /// <summary>
    /// Gets or sets the size of the video stream.
    /// </summary>
    /// <value>A <see cref="Size"/> with the new size of the video stream.</value>
    public Size VideoSize
    {
      get { return this.videoSize; }
      set { this.videoSize = value; }
    }

    /// <summary>
    /// Gets or sets the filename for the video recording.
    /// </summary>
    /// <value>A <see cref="string"/> with the new filename for the video recording.</value>
    public string Filename
    {
      get { return this.filename; }
      set { this.filename = value; }
    }

    /// <summary>
    /// Gets or sets the <see cref="CaptureMode"/> for the device.
    /// </summary>
    public CaptureMode CaptureMode
    {
      get { return this.captureMode; }
      set { this.captureMode = value; }
    }

    /// <summary>
    /// Gets or sets the <see cref="Control"/> which hosts the preview window
    /// </summary>
    public Control PreviewWindow
    {
      get { return this.previewWindow; }
      set { this.previewWindow = value; }
    }

    #endregion //PROPERTIES

    /// <summary>
    /// This method returns an exact copy of this <see cref="CaptureDeviceProperties"/>
    /// </summary>
    /// <returns>An duplicated <see cref="object"/> of type <see cref="CaptureDeviceProperties"/></returns>
    public object Clone()
    {
      return new CaptureDeviceProperties(
        this.videoInputDevice,
        this.audioInputDevice,
        this.videoCompressor,
        this.audioCompressor,
        this.frameRate,
        this.videoSize,
        this.filename,
        this.captureMode,
        this.previewWindow);
    }

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