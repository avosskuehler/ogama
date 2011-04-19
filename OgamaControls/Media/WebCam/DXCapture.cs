using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Windows.Forms;
using DirectShowLib;
using System.Drawing;
using System.Runtime.InteropServices;
using GazeTrackingLibrary.Hardware;

namespace OgamaControls
{
  /// <summary>
  /// Capture class encapsulating simple video device capturing to file.
  /// Is used for the usercamera feature of Ogama
  /// </summary>
  public class DXCapture : IDisposable
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

#if DEBUG
    /// <summary>
    /// Special variable for debugging purposes 
    /// Cookie into the Running Object Table 
    /// </summary>
    private DsROTEntry rotCookie;
#endif

    /// <summary>
    /// DShow Filter: Graph builder 
    /// </summary>
    private IGraphBuilder graphBuilder;

    /// <summary>
    /// DShow Filter: Start/Stop the filter graph -> copy of graphBuilder
    /// </summary>
    private IMediaControl mediaControl;

    /// <summary>
    /// DShow Filter: Control preview window -> copy of graphBuilder
    /// </summary>
    private IVideoWindow videoWindow;

    /// <summary>
    ///  DShow Filter: building graphs for capturing video
    /// </summary>
    private ICaptureGraphBuilder2 captureGraphBuilder;

    /// <summary>
    /// DShow Filter: multiplexor (combine video and audio streams)
    /// </summary>
    private IBaseFilter muxFilter;

    /// <summary>
    /// DShow Filter: file writer
    /// </summary>
    private IFileSinkFilter2 fileWriterFilter;

    /// <summary>
    /// The <see cref="IReferenceClock"/> of the current graph
    /// </summary>
    private IReferenceClock referenceClock;

    /// <summary>
    /// DShow Filter: configure frame rate, size
    /// </summary>
    private IAMStreamConfig videoStreamConfig;

    /// <summary>
    /// The <see cref="CaptureDeviceProperties"/> containing the options
    /// for the DirectShow graph.
    /// </summary>
    private CaptureDeviceProperties captureDeviceProperties;

    #endregion //FIELDS

    ///////////////////////////////////////////////////////////////////////////////
    // Construction and Initializing methods                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region CONSTRUCTION

    /// <summary>
    /// Initializes an instance of the <see cref="DXCapture"/> class
    /// with the given <see cref="CaptureDeviceProperties"/>
    /// </summary>
    /// <param name="properties">A <see cref="CaptureDeviceProperties"/> containing the options
    /// for the DirectShow graph.</param>
    public DXCapture(CaptureDeviceProperties properties)
    {
      this.captureDeviceProperties = properties;
      this.CheckFramerateAndSize(
        this.captureDeviceProperties.VideoInputDevice,
        properties.FrameRate,
        properties.VideoSize);
      this.CreateGraph();
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

    ///// <summary> 
    /////  Sets the name of file to capture to. Initially set to
    /////  a valid temporary file.
    ///// </summary>		
    ///// <remarks>
    /////  If the file does not exist, it will be created. If it does 
    /////  exist, it will be overwritten. An overwritten file will 
    /////  not be shortened if the captured data is smaller than the 
    /////  original file. The file will be valid, it will just contain 
    /////  extra, unused, data after the audio/video data. 
    ///// <para>
    /////  This property cannot be changed while capturing or cued. </para>
    ///// </remarks> 
    //[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    //[Browsable(false)]
    //public string Filename
    //{
    //  get { return this.captureDeviceProperties.Filename; }
    //  set
    //  {
    //    this.captureDeviceProperties.Filename = value;
    //    if (fileWriterFilter == null)
    //    {
    //      this.CreateGraph();
    //    }

    //    if (fileWriterFilter != null)
    //    {
    //      string s;
    //      AMMediaType mt = new AMMediaType();
    //      int hr = fileWriterFilter.GetCurFile(out s, mt);
    //      DsError.ThrowExceptionForHR(hr);
    //      if (mt.formatSize > 0)
    //        Marshal.FreeCoTaskMem(mt.formatPtr);
    //      hr = fileWriterFilter.SetFileName(this.captureDeviceProperties.Filename, mt);
    //      DsError.ThrowExceptionForHR(hr);
    //    }
    //  }
    //}

    /// <summary>
    /// Gets a value indicating whether this capture class has a valid
    /// directshow graph.
    /// </summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public bool HasValidGraph { get; private set; }

    /// <summary>
    /// DShow Filter: selected video device
    /// </summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public IBaseFilter VideoDeviceFilter { get; private set; }

    /// <summary>
    /// DShow Filter: selected video compressor
    /// </summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public IBaseFilter VideoCompressorFilter { get; private set; }

    /// <summary>
    /// DShow Filter: selected video device
    /// </summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public IBaseFilter AudioDeviceFilter { get; private set; }

    /// <summary>
    /// DShow Filter: selected video compressor
    /// </summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public IBaseFilter AudioCompressorFilter { get; private set; }

    /// <summary>
    /// Gets or sets the <see cref="CaptureDeviceProperties"/> describing the
    /// directshow properties
    /// </summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public CaptureDeviceProperties CaptureDeviceProperties
    {
      get
      {
        return this.captureDeviceProperties;
      }
    }

    ///// <summary>
    /////  The capabilities of the video device.
    ///// </summary>
    //public VideoCapabilities VideoCaps { get; private set; }

    #endregion //PROPERTIES

    ///////////////////////////////////////////////////////////////////////////////
    // Public methods                                                            //
    ///////////////////////////////////////////////////////////////////////////////
    #region PUBLICMETHODS

    /// <summary>
    /// Start the graph if it is ready
    /// </summary>
    public void Start()
    {
      if (this.mediaControl != null)
      {
        // Start the filter graph: begin capturing
        int hr = mediaControl.Run();
        if (hr != 0) DsError.ThrowExceptionForHR(hr);
      }
    }

    /// <summary>
    /// Stop the graph if it is running
    /// </summary>
    public void Stop()
    {
      // If we have a preview running we should only stop the
      // capture stream. However, if we have a preview stream
      // we need to re-render the graph anyways because we 
      // need to get rid of the capture stream. To re-render
      // we need to stop the entire graph
      if (mediaControl != null)
      {
        //FilterState state;
        //int hr = mediaControl.GetState(1000, out state);
        int hr = mediaControl.Stop();
        if (hr != 0) DsError.ThrowExceptionForHR(hr);
        //string error = DsError.GetErrorText(hr);
      }
    }

    /// <summary>
    /// This method returns the time of the graphs
    /// <see cref="IReferenceClock"/>.
    /// </summary>
    /// <returns>The <see cref="Int64"/> value of the
    /// <see cref="IReferenceClock"/>.</returns>
    public long GetCurrentTime(out int dropped)
    {
      dropped = 0;
      long start = 0;
      long time = -1;
      if (this.referenceClock != null)
      {
        int hr = this.referenceClock.GetTime(out time);
        DsError.ThrowExceptionForHR(hr);
      }

      IAMDroppedFrames droppedFrames = this.videoStreamConfig as IAMDroppedFrames;
      if (droppedFrames != null)
      {
        int hr = droppedFrames.GetNumDropped(out dropped);
        DsError.ThrowExceptionForHR(hr);
      }

      IGraphConfig graphConfig = this.graphBuilder as IGraphConfig;
      if (graphConfig != null)
      {
        graphConfig.GetStartTime(out start);
      }

      return time - start;
    }

    /// <summary>
    /// Dispose the resources used by this capture class.
    /// Stops the graph first.
    /// </summary>
    public void Dispose()
    {
      // Stop the graph if it is running (ignore errors)
      this.Stop();

      // Free the preview window (ignore errors)
      if (videoWindow != null)
      {
        videoWindow.put_Visible(OABool.False);
        videoWindow.put_Owner(IntPtr.Zero);
        videoWindow = null;
      }

      // Remove the Resize event handler
      if (this.captureDeviceProperties.PreviewWindow != null)
      {
        this.captureDeviceProperties.PreviewWindow.Resize -= new EventHandler(onPreviewWindowResize);
      }

#if DEBUG
      // Remove graph from the ROT 
      if (rotCookie != null)
      {
        rotCookie.Dispose();
        rotCookie = null;
      }
#endif

      // Cleanup
      if (graphBuilder != null)
      {
        Marshal.ReleaseComObject(graphBuilder);
        graphBuilder = null;
      }

      if (captureGraphBuilder != null)
      {
        Marshal.ReleaseComObject(captureGraphBuilder);
        captureGraphBuilder = null;
      }

      if (muxFilter != null)
      {
        Marshal.ReleaseComObject(muxFilter);
        muxFilter = null;
      }

      if (fileWriterFilter != null)
      {
        Marshal.ReleaseComObject(fileWriterFilter);
        fileWriterFilter = null;
      }

      if (this.videoStreamConfig != null)
      {
        Marshal.ReleaseComObject(videoStreamConfig);
        videoStreamConfig = null;
      }

      if (this.referenceClock != null)
      {
        Marshal.ReleaseComObject(referenceClock);
        referenceClock = null;
      }

      if (this.VideoDeviceFilter != null)
      {
        Marshal.ReleaseComObject(VideoDeviceFilter);
        VideoDeviceFilter = null;
      }

      if (this.AudioDeviceFilter != null)
      {
        Marshal.ReleaseComObject(AudioDeviceFilter);
        AudioDeviceFilter = null;
      }

      if (this.VideoCompressorFilter != null)
      {
        Marshal.ReleaseComObject(VideoCompressorFilter);
        VideoCompressorFilter = null;
      }

      if (this.AudioCompressorFilter != null)
      {
        Marshal.ReleaseComObject(AudioCompressorFilter);
        AudioCompressorFilter = null;
      }

      // These are copies of graphBuilder
      mediaControl = null;
      videoWindow = null;

      this.HasValidGraph = false;

      // For unmanaged objects we haven't released explicitly
      GC.Collect();
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
    #region EVENTHANDLER

    /// <summary> Resize the preview when the PreviewWindow is resized </summary>
    protected void onPreviewWindowResize(object sender, EventArgs e)
    {
      if (this.videoWindow != null)
      {
        // Position video window in client rect of owner window
        Rectangle rc = this.captureDeviceProperties.PreviewWindow.ClientRectangle;
        this.videoWindow.SetWindowPosition(0, 0, rc.Right, rc.Bottom);
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
    ///  Create a new filter graph and add filters (devices, compressors, 
    ///  misc), but leave the filters unconnected. Call renderGraph()
    ///  to connect the filters.
    /// </summary>
    /// <returns>True if successful created the graph.</returns>
    protected bool CreateGraph()
    {
      int hr;

      try
      {
        // Garbage collect, ensure that previous filters are released
        GC.Collect();

        // Get the graphbuilder object
        this.graphBuilder = new FilterGraph() as IFilterGraph2;

        // Get a ICaptureGraphBuilder2 to help build the graph
        this.captureGraphBuilder = new CaptureGraphBuilder2() as ICaptureGraphBuilder2;

        // Link the CaptureGraphBuilder to the filter graph
        hr = this.captureGraphBuilder.SetFiltergraph(this.graphBuilder);
        DsError.ThrowExceptionForHR(hr);

#if DEBUG
        this.rotCookie = new DsROTEntry(this.graphBuilder);
#endif
        // Add the Video input device to the graph
        //// Add the video device
        //IBaseFilter videoSourceFilter;
        //hr = this.graphBuilder.AddSourceFilterForMoniker(this.captureDeviceProperties.VideoInputDevice.DirectshowDevice.Mon, null, "Video Source", out videoSourceFilter);
        //DsError.ThrowExceptionForHR(hr);
        //this.VideoDeviceFilter = videoSourceFilter;

        this.VideoDeviceFilter = DirectShowUtils.CreateFilter(
         FilterCategory.VideoInputDevice,
         this.captureDeviceProperties.VideoInputDevice.Name);
        if (this.VideoDeviceFilter != null)
        {
          hr = graphBuilder.AddFilter(this.VideoDeviceFilter, "Video Source");
          DsError.ThrowExceptionForHR(hr);
        }

        // Add the Audio input device to the graph
        this.AudioDeviceFilter = DirectShowUtils.CreateFilter(
         FilterCategory.AudioInputDevice,
         this.captureDeviceProperties.AudioInputDevice);
        if (this.AudioDeviceFilter != null)
        {
          hr = graphBuilder.AddFilter(this.AudioDeviceFilter, "Audio Source");
          DsError.ThrowExceptionForHR(hr);
        }

        // Retrieve the stream control interface for the video device
        // FindInterface will also add any required filters
        // (WDM devices in particular may need additional
        // upstream filters to function).

        DsGuid cat;
        DsGuid med;

        // Try looking for an interleaved media type
        object o;
        cat = PinCategory.Capture;
        med = MediaType.Interleaved;
        Guid iid = typeof(IAMStreamConfig).GUID;
        hr = captureGraphBuilder.FindInterface(cat, med, this.VideoDeviceFilter, iid, out o);

        if (hr != 0)
        {
          // If not found, try looking for a video media type
          med = MediaType.Video;
          hr = captureGraphBuilder.FindInterface(
            cat, med, this.VideoDeviceFilter, iid, out o);

          if (hr != 0)
            o = null;
        }

        videoStreamConfig = o as IAMStreamConfig;

        this.SetConfigParms(
          videoStreamConfig, 
          this.captureDeviceProperties.FrameRate, 
          this.captureDeviceProperties.VideoSize.Width,
          this.captureDeviceProperties.VideoSize.Height);

        // Render capture stream (only if necessary)
        if ((this.captureDeviceProperties.CaptureMode & CaptureMode.VideoCapture) == CaptureMode.VideoCapture
        || (this.captureDeviceProperties.CaptureMode & CaptureMode.AudioCapture) == CaptureMode.AudioCapture)
        {
          // Get the video compressor and add it to the filter graph
          // Create the filter for the selected video compressor
          this.VideoCompressorFilter = DirectShowUtils.CreateFilter(
            FilterCategory.VideoCompressorCategory,
            this.captureDeviceProperties.VideoCompressor);
          if (this.VideoCompressorFilter != null)
          {
            hr = this.graphBuilder.AddFilter(this.VideoCompressorFilter, "Video Compressor");
            DsError.ThrowExceptionForHR(hr);
          }

          // Get the video compressor and add it to the filter graph
          // Create the filter for the selected video compressor
          this.AudioCompressorFilter = DirectShowUtils.CreateFilter(
            FilterCategory.AudioCompressorCategory,
            this.captureDeviceProperties.AudioCompressor);
          if (this.AudioCompressorFilter != null)
          {
            hr = this.graphBuilder.AddFilter(this.AudioCompressorFilter, "Audio Compressor");
            DsError.ThrowExceptionForHR(hr);
          }

          // Ensure required properties set
          if (this.captureDeviceProperties.Filename == null || this.captureDeviceProperties.Filename == string.Empty)
          {
            throw new ArgumentException("The Filename property has not been set to a file.\n");
          }

          // Render the file writer portion of graph (mux -> file)
          hr = this.captureGraphBuilder.SetOutputFileName(
            MediaSubType.Avi,
            this.captureDeviceProperties.Filename,
            out this.muxFilter,
            out this.fileWriterFilter);
          DsError.ThrowExceptionForHR(hr);

          // Render video (video -> mux)
          if (this.VideoDeviceFilter != null &&
            ((this.captureDeviceProperties.CaptureMode & CaptureMode.VideoCapture) == CaptureMode.VideoCapture))
          {
            // Try interleaved first, because if the device supports it,
            // it's the only way to get audio as well as video
            cat = PinCategory.Capture;
            med = MediaType.Interleaved;
            hr = captureGraphBuilder.RenderStream(
              cat,
              med,
              this.VideoDeviceFilter,
              this.VideoCompressorFilter,
              muxFilter);

            // If interleaved fails try video
            if (hr < 0)
            {
              med = MediaType.Video;
              hr = captureGraphBuilder.RenderStream(
                cat,
                med,
                this.VideoDeviceFilter,
                this.VideoCompressorFilter,
                muxFilter);

              if (hr == -2147220969)
              {
                throw new ArgumentException("Video device is already in use");
              }

              DsError.ThrowExceptionForHR(hr);
            }

            hr = captureGraphBuilder.RenderStream(
              null,
              null,
              this.VideoDeviceFilter,
              this.VideoCompressorFilter,
              muxFilter);
          }

          // Render audio (audio -> mux)
          if (this.AudioDeviceFilter != null &&
            ((this.captureDeviceProperties.CaptureMode & CaptureMode.AudioCapture) == CaptureMode.AudioCapture))
          {
            cat = PinCategory.Capture;
            med = MediaType.Audio;
            hr = captureGraphBuilder.RenderStream(
              cat,
              med,
              this.AudioDeviceFilter,
              this.AudioCompressorFilter,
              muxFilter);

            DsError.ThrowExceptionForHR(hr);
          }
        }

        // Render preview stream (only if necessary)
        if ((this.captureDeviceProperties.CaptureMode & CaptureMode.VideoPreview) == CaptureMode.VideoPreview
        || (this.captureDeviceProperties.CaptureMode & CaptureMode.AudioPrehear) == CaptureMode.AudioPrehear)
        {
          if (this.VideoDeviceFilter != null &&
           ((this.captureDeviceProperties.CaptureMode & CaptureMode.VideoPreview) == CaptureMode.VideoPreview))
          {
            // Render preview (video -> renderer)
            hr = captureGraphBuilder.RenderStream(
              PinCategory.Preview,
              MediaType.Video,
              this.VideoDeviceFilter,
              null,
              null);
            string error = DsError.GetErrorText(hr);
            DsError.ThrowExceptionForHR(hr);
          }

          if (this.AudioDeviceFilter != null &&
            ((this.captureDeviceProperties.CaptureMode & CaptureMode.AudioPrehear) == CaptureMode.AudioPrehear))
          {
            // Render audio (audio -> renderer)
            hr = captureGraphBuilder.RenderStream(
             PinCategory.Preview,
             MediaType.Audio,
             this.AudioDeviceFilter,
             null,
             null);
            string error = DsError.GetErrorText(hr);
            DsError.ThrowExceptionForHR(hr);
          }
        }

        // Retrieve 
        RetreiveGraphReferenceClock();

        // Retreive the media control interface (for starting/stopping graph)
        this.mediaControl = (IMediaControl)this.graphBuilder;

        // Get the IVideoWindow interface
        this.videoWindow = (IVideoWindow)this.graphBuilder;
      }
      catch (Exception ex)
      {
        MessageBox.Show("Error in creating usercamera graph: " + ex.ToString());
        return false;
      }

      this.HasValidGraph = true;

      return true;
    }

    /// <summary>
    /// This method is called to show the preview of the video stream on
    /// the preselected this.captureDeviceProperties.PreviewWindow control.
    /// </summary>
    public void ShowPreviewWindow()
    {
      int hr;
      if (this.captureDeviceProperties.PreviewWindow != null)
      {
        // Set the video window to be a child of the main window
        hr = videoWindow.put_Owner(ThreadSafe.GetHandle(this.captureDeviceProperties.PreviewWindow));
        DsError.ThrowExceptionForHR(hr);

        // Set video window style
        hr = videoWindow.put_WindowStyle(WindowStyle.Child | WindowStyle.ClipChildren | WindowStyle.ClipSiblings);
        DsError.ThrowExceptionForHR(hr);

        // Position video window in client rect of owner window
        this.captureDeviceProperties.PreviewWindow.Resize += new EventHandler(onPreviewWindowResize);
        onPreviewWindowResize(this, null);

        // Make the video window visible, now that it is properly positioned
        hr = videoWindow.put_Visible(OABool.True);
        DsError.ThrowExceptionForHR(hr);

        this.Start();
      }
      //else
      //{
      //  if (videoWindow != null)
      //  {
      //    videoWindow.put_Visible(OABool.False);
      //    videoWindow.put_Owner(IntPtr.Zero);
      //    this.captureDeviceProperties.PreviewWindow.Resize -= new EventHandler(onPreviewWindowResize);
      //    videoWindow = null;
      //  }
      //}
    }

    /// <summary>
    /// Set the Framerate, and video size
    /// </summary>
    /// <param name="videoStreamConfig">The <see cref="IAMStreamConfig"/> of the capture device.</param>
    /// <param name="frameRate">The new framerate to be used.</param>
    /// <param name="width">The new video width to be used.</param>
    /// <param name="height">The new video height to be used.</param>
    private void SetConfigParms(
      IAMStreamConfig videoStreamConfig,
      int frameRate,
      int width,
      int height)
    {
      int hr;
      AMMediaType media = null;

      if (videoStreamConfig == null)
      {
        throw new ArgumentNullException("Error in DXCapture.SetConfigParams(). Failed to get IAMStreamConfig");
      }

      // Get the existing format block
      hr = videoStreamConfig.GetFormat(out media);
      DsError.ThrowExceptionForHR(hr);

      // copy out the videoinfoheader
      var v = new VideoInfoHeader();
      Marshal.PtrToStructure(media.formatPtr, v);

      // if overriding set values
      if (frameRate > 0)
        v.AvgTimePerFrame = 10000000 / frameRate;

      if (width > 0)
        v.BmiHeader.Width = width;

      if (height > 0)
        v.BmiHeader.Height = height;

      // Copy the media structure back
      Marshal.StructureToPtr(v, media.formatPtr, true);

      // Set the new format
      if (videoStreamConfig != null)
      {
        hr = videoStreamConfig.SetFormat(media);
        DsError.ThrowExceptionForHR(hr);
      }

      DsUtils.FreeAMMediaType(media);
      media = null;
    }

    private void CheckFramerateAndSize(CameraInfo camera, int newFrameRate, Size newFrameSize)
    {
      if (camera != null)
      {
        int minFramerate;
        int maxFramerate;
        int minWidth;
        int maxWidth;
        int minHeight;
        int maxHeight;

        this.GetMaxMinCaps(
          camera,
          out minFramerate,
          out maxFramerate,
          out minWidth,
          out maxWidth,
          out minHeight,
          out maxHeight);

        CamSizeFPS defaultMode = camera.SupportedSizesAndFPS[camera.SupportedSizesAndFPS.Count - 1];

        if (newFrameRate > minFramerate && newFrameRate < maxFramerate)
        {
          this.captureDeviceProperties.FrameRate = newFrameRate;
        }
        else
        {
          this.captureDeviceProperties.FrameRate = defaultMode.FPS;
        }

        if (newFrameSize.Width >= minWidth && newFrameSize.Width <= maxWidth &&
          newFrameSize.Height >= minHeight && newFrameSize.Height <= maxHeight)
        {
          this.captureDeviceProperties.VideoSize = newFrameSize;
        }
        else
        {
          this.captureDeviceProperties.VideoSize =
            new Size(defaultMode.Width, defaultMode.Height);
        }
      }
    }

    #endregion //PRIVATEMETHODS

    ///////////////////////////////////////////////////////////////////////////////
    // Small helping Methods                                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region HELPER

    private void RetreiveGraphReferenceClock()
    {
      this.graphBuilder.SetDefaultSyncSource();
      IMediaFilter filter = this.graphBuilder as IMediaFilter;
      int hr = filter.GetSyncSource(out referenceClock);
      DsError.ThrowExceptionForHR(hr);
    }

    /// <summary>
    /// Returns the maximum and minimum capabilities of the 
    /// given <see cref="CameraInfo"/> for FPS and size.
    /// </summary>
    /// <param name="camera">The <see cref="CameraInfo"/> to be checked for.</param>
    /// <param name="minFramerate">Out. The minimum available framerate of this device.</param>
    /// <param name="maxFramerate">Out. The maximum available framerate of this device.</param>
    /// <param name="minWidth">Out. The minimum available width of this device.</param>
    /// <param name="maxWidth">Out. The maximum available width of this device.</param>
    /// <param name="minHeight">Out. The minimum available height of this device.</param>
    /// <param name="maxHeight">Out. The maximum available height of this device.</param>
    public void GetMaxMinCaps(
      CameraInfo camera,
      out int minFramerate,
      out int maxFramerate,
      out int minWidth,
      out int maxWidth,
      out int minHeight,
      out int maxHeight)
    {
      minFramerate = int.MaxValue;
      maxFramerate = 0;
      minWidth = int.MaxValue;
      maxWidth = 0;
      minHeight = int.MaxValue;
      maxHeight = 0;

      if (camera.Name == "OgamaScreenCapture Filter")
      {
        minFramerate = 1;
        maxFramerate = 100;
        minWidth = 1;
        maxWidth = 3000;
        minHeight = 1;
        maxHeight = 3000;
        return;
      }

      foreach (CamSizeFPS cap in camera.SupportedSizesAndFPS)
      {
        if (cap.FPS < minFramerate)
        {
          minFramerate = (int)cap.FPS;
        }

        if (cap.FPS > maxFramerate)
        {
          maxFramerate = (int)cap.FPS;
        }

        if (cap.Width < minWidth)
        {
          minWidth = (int)cap.Width;
        }

        if (cap.Width > maxWidth)
        {
          maxWidth = (int)cap.Width;
        }

        if (cap.Height < minHeight)
        {
          minHeight = (int)cap.Height;
        }

        if (cap.Height > maxHeight)
        {
          maxHeight = (int)cap.Height;
        }
      }
    }

    #endregion //HELPER



  }
}
