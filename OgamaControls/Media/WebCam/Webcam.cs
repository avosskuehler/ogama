using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DirectShowLib;
using System.Diagnostics;

namespace OgamaControls
{
  using GTHardware.Cameras.DirectShow;

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

    ///// <summary>
    ///// The <see cref="Filters"/> registered in the system, containing
    ///// Video, AudioDevices and Video, Audiocompressors
    ///// </summary>
    //private Filters filters;

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

    ///// <summary>
    ///// An optional <see cref="Control"/> in which the preview
    ///// window should be shown.
    ///// </summary>
    //private Control previewWindow;

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
      //this.previewWindow = this;
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
      //this.previewWindow = previewWindow;
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

    ///// <summary>
    ///// Sets the filename of the current capture.
    ///// </summary>
    //[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    //[Browsable(false)]
    //public string Filename
    //{
    //  set
    //  {
    //    if (this.dxCapture != null)
    //    {
    //      this.dxCapture.Filename = value;
    //    }
    //  }
    //}


    #endregion //PROPERTIES

    ///////////////////////////////////////////////////////////////////////////////
    // Public methods                                                            //
    ///////////////////////////////////////////////////////////////////////////////
    #region PUBLICMETHODS

    /// <summary>
    /// Gets or sets a value indicating whether this
    /// webcam should preview its video stream or not.
    /// </summary>
    public void Preview()
    {
      this.dxCapture.ShowPreviewWindow();
    }

    /// <summary>
    /// Releases the resources used by the DXcapture object
    /// </summary>
    public void DisposeDxCapture()
    {
      if (this.dxCapture != null)
      {
        this.dxCapture.Dispose();
        this.dxCapture = null;
      }
    }

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
      //string oldFile = this.properties.Filename;

      //string temp = System.IO.Path.GetTempFileName();
      //this.properties.Filename = temp;

      if (this.dxCapture == null)
      {
        this.InitializeWebcam(this.properties);
      }

      //if (this.dxCapture == null)
      //{
      //  this.properties.Filename = oldFile;
      //  return false;
      //}

      //try
      //{
      //  this.dxCapture.Start();
      //}
      //catch (Exception)
      //{
      //  this.properties.Filename = oldFile;
      //  return false;
      //}
      //finally
      //{
      //  this.dxCapture.Stop();
      //}

      if (!this.dxCapture.HasValidGraph)
      {
        //this.properties.Filename = oldFile;
        return false;
      }

      //this.properties.Filename = oldFile;

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
        this.dxCapture.ShowPreviewWindow();
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
    public void ShowConfigureDialog(bool restartCamera)
    {
      if (this.dxCapture == null)
      {
        return;
      }

      WebcamPropertiesDlg dialog = new WebcamPropertiesDlg(this.dxCapture);
      //dialog.Properties = this.properties;
      //dialog.ShouldPreview = true;

      if (dialog.ShowDialog() == DialogResult.OK)
      {
        // Update properties
        // Validation check is within dialog
        this.properties = dialog.Properties;
        this.properties.PreviewWindow = this;
      }

      if (restartCamera)
      {
        // Restart webcam
        this.InitializeWebcam(this.properties);
        this.RunGraph();
      }
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

      if (this.dxCapture.VideoDeviceFilter != null)
      {
        DirectShowUtils.DisplayPropertyPage(this.Handle, this.dxCapture.VideoDeviceFilter);
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

      if (this.dxCapture.VideoCompressorFilter != null)
      {
        DirectShowUtils.DisplayPropertyPage(this.Handle, this.dxCapture.VideoCompressorFilter);
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

      if (this.dxCapture.AudioDeviceFilter != null)
      {
        DirectShowUtils.DisplayPropertyPage(this.Handle, this.dxCapture.AudioDeviceFilter);
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

      if (this.dxCapture.AudioCompressorFilter != null)
      {
        DirectShowUtils.DisplayPropertyPage(this.Handle, this.dxCapture.AudioCompressorFilter);
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
      InitializeCustomComponents();
      //this.InitializeWebcam(this.properties);
    }

    /// <summary>
    /// The <see cref="ToolStripItem.Click"/> event handler
    /// for the <see cref="ContextMenu"/> <see cref="cmuProperties"/>.
    /// Calls the <see cref="ShowConfigureDialog(bool)"/> that shows
    /// a configuration dialog.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void cmuProperties_Click(object sender, EventArgs e)
    {
      this.ShowConfigureDialog(true);
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
      this.properties = new CaptureDeviceProperties();
      //this.stopWatch = new Stopwatch();
      if (DirectShowDevices.Instance.Cameras.Count > 0)
      {
        this.properties.VideoInputDevice = DirectShowDevices.Instance.Cameras[0];
      }

      DsDevice[] videoCompressors = DsDevice.GetDevicesOfCat(FilterCategory.VideoCompressorCategory);
      foreach (DsDevice compressor in videoCompressors)
      {
        if (compressor.Name.Contains("Microsoft Video 1"))
        {
          this.properties.VideoCompressor = compressor.Name;
          break;
        }
      }

      DsDevice[] audioInputDevices = DsDevice.GetDevicesOfCat(FilterCategory.AudioInputDevice);
      if (audioInputDevices.Length > 0)
      {
        this.properties.AudioInputDevice = audioInputDevices[0].Name;
      }

      DsDevice[] audioCompressors = DsDevice.GetDevicesOfCat(FilterCategory.AudioCompressorCategory);
      foreach (DsDevice compressor in audioCompressors)
      {
        if (compressor.Name == "PCM")
        {
          this.properties.AudioCompressor = compressor.Name;
          break;
        }
      }

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

      if (captureProperties.VideoInputDevice.Name == "OgamaScreenCapture Filter")
      {
        // We dont want to initialize a webcam with the
        // ScreenCapture Filter
        return;
      }

      if (this.dxCapture != null)
      {
        this.dxCapture.Dispose();
      }

      // If this control is attached use itself for previewing
      // otherwise use the given preview window
      if (this.Parent != null)
      {
        captureProperties.PreviewWindow = this;
      }

      this.dxCapture = new DXCapture(captureProperties);

      // Check the current valid CaptureMode
      //CaptureMode available = CaptureMode.None;
      //if ((captureProperties.CaptureMode & CaptureMode.Video) == CaptureMode.VideoPreview)
      //{
      //  available |= CaptureMode.Video;
      //}

      //if (audioDevice != null)
      //{
      //  available |= CaptureMode.Audio;
      //}

      this.OnWebcamAvailable(new CaptureModeEventArgs(captureProperties.CaptureMode));
    }

    #endregion //PRIVATEMETHODS

    ///////////////////////////////////////////////////////////////////////////////
    // Small helping Methods                                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region HELPER
    #endregion //HELPER

  }
}
