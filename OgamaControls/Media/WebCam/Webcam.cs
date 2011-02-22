using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DirectX.Capture;
using DirectShowLib;
using System.Diagnostics;

namespace OgamaControls
{
  /// <summary>
  /// This class encapsulates a webcam <see cref="UserControl"/>.
  /// It can be used to show a preview of any capture device on the
  /// system in itself or a custom provided <see cref="Control"/>.
  /// It can also capture video and audio stream into a file with
  /// default or custom devices and compressors.
  /// </summary>
  public partial class Webcam : UserControl
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
    /// The <see cref="Filters"/> registered in the system, containing
    /// Video, AudioDevices and Video, Audiocompressors
    /// </summary>
    private Filters filters;

    /// <summary>
    /// The <see cref="DXCapture"/> object used to capture the webcams
    /// video stream along with audio stream.
    /// </summary>
    private DXCapture dxCapture;

    /// <summary>
    /// The <see cref="CaptureDeviceProperties"/> with the currently
    /// used filters for the capturing
    /// </summary>
    private CaptureDeviceProperties properties;

    /// <summary>
    /// An optional <see cref="Control"/> in which the preview
    /// window should be shown.
    /// </summary>
    private Control previewWindow;

    ///// <summary>
    ///// A precise timer for getting local time stamps.
    ///// </summary>
    //private Stopwatch stopWatch;

    ///// <summary>
    ///// Saves the first time value of the reference clock after running the graph.
    ///// </summary>
    //private long webcamStarttime;

    #endregion //FIELDS

    ///////////////////////////////////////////////////////////////////////////////
    // Construction and Initializing methods                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region CONSTRUCTION

    /// <summary>
    /// Initializes a new instance of the ReplayModule class
    /// with the default preview window.
    /// </summary>
    public Webcam()
    {
      InitializeComponent();
      InitializeCustomComponents();
      this.previewWindow = this;
    }

    /// <summary>
    /// Initializes a new instance of the ReplayModule class 
    /// with a custom preview window.
    /// </summary>
    /// <param name="previewWindow">A <see cref="Control"/> in which the preview
    /// window should be shown.</param>
    public Webcam(Control previewWindow)
    {
      InitializeComponent();
      InitializeCustomComponents();
      this.previewWindow = previewWindow;
    }

    #endregion //CONSTRUCTION

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Enumerations                                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region ENUMS

    /// <summary>
    /// This event is raised whenever the webcam is initialized with
    /// new capture settings and contains a <see cref="CaptureMode"/>
    /// in its event handler.
    /// </summary>
    public event CaptureModeEventHandler WebcamAvailable;

    #endregion ENUMS

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Properties                                                       //
    ///////////////////////////////////////////////////////////////////////////////
    #region PROPERTIES

    /// <summary>
    /// Gets the underlying <see cref="DXCapture"/>
    /// object.
    /// </summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public DXCapture DirectXCapture
    {
      get { return this.dxCapture; }
    }

    /// <summary>
    /// Gets or sets a <see cref="CaptureDeviceProperties"/>
    /// with the properties of the webcam.
    /// </summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public CaptureDeviceProperties Properties
    {
      get { return this.properties; }
      set
      {
        this.properties = value;
        InitializeWebcam(this.properties);
      }
    }

    /// <summary>
    /// Sets the filename of the current capture.
    /// </summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public string Filename
    {
      set
      {
        if (this.dxCapture != null)
        {
          this.dxCapture.Filename = value;
        }
      }
    }

    /// <summary>
    /// Gets or sets a value indicating whether this
    /// webcam should preview its video stream or not.
    /// </summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public bool Preview
    {
      set
      {
        if (value)
        {
          try
          {
            if (this.dxCapture != null)
            {
              this.dxCapture.PreviewWindow = this.previewWindow;
            }
          }
          catch (Exception)
          {
          }
        }
        else
        {
          try
          {
            if (this.dxCapture != null)
            {
              this.dxCapture.PreviewWindow = null;
            }
          }
          catch (Exception)
          {
          }
        }
      }
    }

    #endregion //PROPERTIES

    ///////////////////////////////////////////////////////////////////////////////
    // Public methods                                                            //
    ///////////////////////////////////////////////////////////////////////////////
    #region PUBLICMETHODS

    /// <summary>
    /// This method checks for valid capture settings
    /// and returns <strong>true</strong> if the current
    /// <see cref="CaptureDeviceProperties"/> can be used.
    /// </summary>
    /// <returns><strong>True</strong> if the current
    /// <see cref="CaptureDeviceProperties"/> can be used for capturing,
    /// otherwise <strong>false</strong>.</returns>
    public bool TestCapture()
    {
      if (this.dxCapture == null)
      {
        return false;
      }

      string temp = System.IO.Path.GetTempFileName();
      this.dxCapture.Filename = temp;

      try
      {
        this.dxCapture.Start();
      }
      catch (Exception)
      {
        return false;
      }
      finally
      {
        this.dxCapture.Stop();
      }

      if (!this.dxCapture.HasValidGraph)
      {
        return false;
      }

      return true;
    }

    /// <summary>
    /// This methods starts the capturing of the webcam stream
    /// into the given filename if one is specified.
    /// It also starts the internal timer to receive timestamps.
    /// </summary>
    public void RunGraph()
    {
      if (this.dxCapture == null)
      {
        return;
      }

      if (this.dxCapture.HasValidGraph)
      {
        this.dxCapture.Start();
      }
    }

    /// <summary>
    /// This methods stops the capturing of the webcam stream
    /// and writes the file on the disk.
    /// It also stops the internal timer for receiving timestamps.
    /// </summary>
    public void StopCapture()
    {
      if (this.dxCapture == null)
      {
        return;
      }

      this.dxCapture.Stop();
    }

    /// <summary>
    /// This method shows a <see cref="WebcamPropertiesDlg"/>
    /// to modify the current <see cref="CaptureDeviceProperties"/>
    /// and afterwards reinitializes the webcam.
    /// </summary>
    public void ShowConfigureDialog()
    {
      if (this.dxCapture == null)
      {
        return;
      }

      try
      {
        this.dxCapture.PreviewWindow = null;
      }
      catch (Exception)
      {
      }

      WebcamPropertiesDlg dialog = new WebcamPropertiesDlg();
      dialog.Properties = this.properties;
      dialog.ShouldPreview = true;

      if (dialog.ShowDialog() == DialogResult.OK)
      {
        // Update properties
        // Validation check is within dialog
        this.properties = dialog.Properties;
      }

      // Restart webcam
      this.InitializeWebcam(this.properties);
    }

    /// <summary>
    /// This method returns the <see cref="Stopwatch.ElapsedMilliseconds"/> value,
    /// that is the time from the start of the capturing in millisecons.
    /// </summary>
    /// <returns>The <see cref="Stopwatch.ElapsedMilliseconds"/> in
    /// milliseconds and -1, if stopwatch is not running.</returns>
    public long GetCurrentTime()
    {
      if (this.dxCapture == null)
      {
        return -1;
      }

      // Retrieve current time from capture graph and
      // number of dropped frames
      int droppedFrames;
      long currentTime = this.dxCapture.GetCurrentTime(out droppedFrames);
      int shrinkValue = (int)((1000f / this.properties.FrameRate) * (droppedFrames - 1));

      // Convert from 100-Nanosecond units to milliseconds and subtract
      // missing time from lost samples
      long returnValue = (long)(currentTime / 10000f) - shrinkValue;
      return returnValue;
    }

    /// <summary>
    /// This method displays the property pages for the current video device.
    /// </summary>
    public void ShowVideoDeviceConfigureDialog()
    {
      if (this.dxCapture == null)
      {
        return;
      }

      if (this.dxCapture.VideoDevice.baseFilter != null)
      {
        DirectShowUtils.DisplayPropertyPage(this.Handle, this.dxCapture.VideoDevice.baseFilter);
      }
    }

    /// <summary>
    /// This method displays the property pages for the current video compressor.
    /// </summary>
    public void ShowVideoCompressorConfigureDialog()
    {
      if (this.dxCapture == null)
      {
        return;
      }

      if (this.dxCapture.VideoCompressor.baseFilter != null)
      {
        DirectShowUtils.DisplayPropertyPage(this.Handle, this.dxCapture.VideoCompressor.baseFilter);
      }
    }

    /// <summary>
    /// This method displays the property pages for the current audio device.
    /// </summary>
    public void ShowAudioDeviceConfigureDialog()
    {
      if (this.dxCapture == null)
      {
        return;
      }

      if (this.dxCapture.AudioDevice.baseFilter != null)
      {
        DirectShowUtils.DisplayPropertyPage(this.Handle, this.dxCapture.AudioDevice.baseFilter);
      }
    }

    /// <summary>
    /// This method displays the property pages for the current audio compressor.
    /// </summary>
    public void ShowAudioCompressorConfigureDialog()
    {
      if (this.dxCapture == null)
      {
        return;
      }

      if (this.dxCapture.AudioCompressor.baseFilter != null)
      {
        DirectShowUtils.DisplayPropertyPage(this.Handle, this.dxCapture.AudioCompressor.baseFilter);
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
    /// The <see cref="Form.Load"/> event handler. 
    /// Initializes the webcam with the current
    /// <see cref="CaptureDeviceProperties"/>
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void WebCam2_Load(object sender, EventArgs e)
    {
      this.InitializeWebcam(this.properties);
    }

    /// <summary>
    /// The <see cref="ToolStripItem.Click"/> event handler
    /// for the <see cref="ContextMenu"/> <see cref="cmuProperties"/>.
    /// Calls the <see cref="ShowConfigureDialog()"/> that shows
    /// a configuration dialog.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void cmuProperties_Click(object sender, EventArgs e)
    {
      this.ShowConfigureDialog();
    }

    #endregion //WINDOWSEVENTHANDLER

    ///////////////////////////////////////////////////////////////////////////////
    // Eventhandler for Custom Defined Events                                    //
    ///////////////////////////////////////////////////////////////////////////////
    #region CUSTOMEVENTHANDLER

    /// <summary>
    /// This method raises the <see cref="WebcamAvailable"/> 
    /// event by invoking the delegates.
    /// It should be called when the webcam is initialized.
    /// </summary>
    /// <remarks>The <see cref="CaptureModeEventArgs"/> contain
    /// the new initalized <see cref="CaptureMode"/></remarks>
    /// <param name="e">A <see cref="CaptureModeEventArgs"/></param>.
    private void OnWebcamAvailable(CaptureModeEventArgs e)
    {
      if (this.WebcamAvailable != null)
      {
        this.WebcamAvailable(this, e);
      }
    }

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
    /// This method initializes the webcam with default values
    /// for video and audio capturing.
    /// </summary>
    private void InitializeCustomComponents()
    {
      this.filters = new Filters();
      this.properties = new CaptureDeviceProperties();
      //this.stopWatch = new Stopwatch();

      if (this.filters.VideoInputDevices.Count > 0)
      {
        this.properties.VideoInputDevice = this.filters.VideoInputDevices[0].Name;
      }

      this.properties.VideoCompressor = "ffdshow";

      if (this.filters.AudioInputDevices.Count > 0)
      {
        this.properties.AudioInputDevice = this.filters.AudioInputDevices[0].Name;
      }

      this.properties.AudioCompressor = "PCM";

      this.properties.FrameRate = 10;
      this.properties.VideoSize = new Size(320, 240);
      this.properties.CaptureMode = CaptureMode.AudioVideoPreview;
    }

    /// <summary>
    /// This method creates a new <see cref="DXCapture"/>
    /// with the given <see cref="CaptureDeviceProperties"/>
    /// </summary>
    /// <param name="captureProperties">A <see cref="CaptureDeviceProperties"/>
    /// to use for creating the underlying <see cref="DXCapture"/></param>
    private void InitializeWebcam(CaptureDeviceProperties captureProperties)
    {
      if (this.DesignMode)
      {
        return;
      }

      if (captureProperties == null)
      {
        // We dont want to initialize a webcam
        return;
      }

      if (this.dxCapture != null)
      {
        this.dxCapture.Dispose();
      }

      if (captureProperties.VideoInputDevice == "OgamaScreenCapture Filter")
      {
        // We dont want to initialize a webcam with the
        // ScreenCapture Filter
        return;
      }

      Filter videoDevice = null;
      Filter videoCompressor = null;
      Filter audioDevice = null;
      Filter audioCompressor = null;

      foreach (Filter inputFilter in this.filters.VideoInputDevices)
      {
        if (inputFilter.Name == captureProperties.VideoInputDevice)
        {
          videoDevice = inputFilter;
          break;
        }
      }

      foreach (Filter compressorFilter in this.filters.VideoCompressors)
      {
        if (compressorFilter.Name == captureProperties.VideoCompressor)
        {
          videoCompressor = compressorFilter;
          break;
        }
      }

      foreach (Filter inputFilter in this.filters.AudioInputDevices)
      {
        if (inputFilter.Name == captureProperties.AudioInputDevice)
        {
          audioDevice = inputFilter;
          break;
        }
      }

      foreach (Filter compressorFilter in this.filters.AudioCompressors)
      {
        if (compressorFilter.Name == captureProperties.AudioCompressor)
        {
          audioCompressor = compressorFilter;
          break;
        }
      }

      if (videoDevice != null || audioDevice != null)
      {
        this.dxCapture = new DXCapture(
          videoDevice,
          audioDevice,
          videoCompressor,
          audioCompressor,
          captureProperties.FrameRate,
          captureProperties.VideoSize,
          captureProperties.CaptureMode);

        this.dxCapture.Filename = captureProperties.Filename;
        try
        {
          if ((captureProperties.CaptureMode & CaptureMode.VideoPreview) == CaptureMode.VideoPreview)
          {
            this.dxCapture.PreviewWindow = this.previewWindow;
          }
        }
        catch (Exception)
        {
        }
      }

      // Check the current valid CaptureMode
      CaptureMode available = CaptureMode.None;
      if (videoDevice != null)
      {
        available |= CaptureMode.Video;
      }

      if (audioDevice != null)
      {
        available |= CaptureMode.Audio;
      }

      this.OnWebcamAvailable(new CaptureModeEventArgs(available));
    }

    #endregion //PRIVATEMETHODS

    ///////////////////////////////////////////////////////////////////////////////
    // Small helping Methods                                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region HELPER
    #endregion //HELPER
  }
}
