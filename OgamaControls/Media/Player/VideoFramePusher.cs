using System;
using System.Collections.Generic;
using System.Text;
using DirectShowLib;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Threading;
using System.Windows.Forms;

namespace OgamaControls
{
  /// <summary>
  /// This class inherits <see cref="ISampleGrabberCB"/> and <see cref="IDisposable"/>
  /// to provide a sample callback function that raises an event whenever
  /// a new video frame is available.
  /// Connect to <see cref="VideoFrameAvailable"/> to retreive the bitmaps of 
  /// every frame.
  /// </summary>
  public class VideoFramePusher : ISampleGrabberCB, IDisposable
  {
    private IGraphBuilder graphBuilder;
    private IMediaControl mediaControl;
    private IMediaSeeking mediaSeeking;
    private IVideoFrameStep frameStep;
    private ISampleGrabber sampleGrabber;
    private IBaseFilter sampleGrabberFilter;
    private IBaseFilter nullrenderer;
    private IMediaEvent mediaEvent;

    private double currentPlaybackRate = 1.0;
    /// <summary> Dimensions of the image, calculated once in constructor. </summary>
    private int m_videoWidth;
    private int m_videoHeight;
    private int m_stride;
    private IntPtr m_handle = IntPtr.Zero;
    //private byte[] m_bitmapDataArray;
    private PlayState currentState = PlayState.Stopped;
    private Bitmap workingBitmap;

    /// <summary>
    /// This event is raised whenever a new video frame is available.
    /// </summary>
    public event BitmapEventHandler VideoFrameAvailable;
    private bool callbackDone;


#if DEBUG
    // Allow you to "Connect to remote graph" from GraphEdit
    DsROTEntry m_rot = null;
#endif

    /// <summary>
    /// Gets or sets the <see cref="Bitmap"/> of the current captured image.
    /// </summary>
    public Bitmap CapturedImage
    {
      get { return this.workingBitmap; }
      set { this.workingBitmap = value; }
    }

    /// <summary>
    /// Gets or sets the current playback rate of the video.
    /// </summary>
    /// <value>A <see cref="double"/> with the playback rate. Must not be zero.</value>
    public double PlaybackRate
    {
      get
      {
        return this.currentPlaybackRate;
      }
      set
      {
        int hr = 0;

        // If the IMediaSeeking interface exists, use it to set rate
        if (this.mediaSeeking != null && value != 0)
        {
          hr = this.mediaSeeking.SetRate(value);
          DsError.ThrowExceptionForHR(hr);

          this.currentPlaybackRate = value;
        }
      }
    }

    /// <summary>
    /// Initializes a new instance of the VideoFramePusher class.
    /// </summary>
    public VideoFramePusher()
    {
    }

    /// <summary> release everything. </summary>
    public void Dispose()
    {
      CloseInterfaces();
    }

    /// <summary>
    /// Destructor
    /// </summary>
    ~VideoFramePusher()
    {
      CloseInterfaces();
    }

    /// <summary>
    /// Starts the capturing.
    /// </summary>
    public void Start()
    {
      int hr = mediaControl.Run();
      this.currentState = PlayState.Running;
      DsError.ThrowExceptionForHR(hr);
    }

    /// <summary>
    /// Stops the capturing
    /// </summary>
    public void Stop()
    {
      int hr = mediaControl.Stop();
      this.currentState = PlayState.Stopped;
      DsError.ThrowExceptionForHR(hr);
    }

    /// <summary>
    /// Pauses / unpauses clip according to state.
    /// </summary>
    public void Pause()
    {
      if (this.mediaControl == null)
        return;

      // Toggle play/pause behavior
      if ((this.currentState == PlayState.Paused) || (this.currentState == PlayState.Stopped))
      {
        if (this.mediaControl.Run() >= 0)
          this.currentState = PlayState.Running;
      }
      else
      {
        if (this.mediaControl.Pause() >= 0)
          this.currentState = PlayState.Paused;
      }
    }

    // Some video renderers support stepping media frame by frame with the
    // IVideoFrameStep interface.  See the interface documentation for more
    // details on frame stepping.
    private bool GetFrameStepInterface()
    {
      int hr = 0;

      // Get the frame step interface, if supported
      IVideoFrameStep frameStepTest = this.graphBuilder as IVideoFrameStep;

      // Check if this decoder can step
      hr = frameStepTest.CanStep(0, null);
      if (hr == 0)
      {
        this.frameStep = frameStepTest;
        return true;
      }
      else
      {
        this.frameStep = null;
        return false;
      }
    }

    /// <summary> build the capture graph for grabber. </summary>
    private void SetupGraph(string FileName)
    {
      int hr;

      // Get the graphbuilder object
      this.graphBuilder = new FilterGraph() as IGraphBuilder;
      this.mediaControl = this.graphBuilder as IMediaControl;
      this.mediaSeeking = this.graphBuilder as IMediaSeeking;
      this.mediaEvent = this.graphBuilder as IMediaEvent;

      try
      {
        // Get the SampleGrabber interface
        this.sampleGrabber = new SampleGrabber() as ISampleGrabber;
        this.sampleGrabberFilter = sampleGrabber as IBaseFilter;

        ConfigureSampleGrabber(sampleGrabber);

        // Add the frame grabber to the graph
        hr = graphBuilder.AddFilter(sampleGrabberFilter, "Ds.NET Sample Grabber");
        DsError.ThrowExceptionForHR(hr);

        IBaseFilter aviSplitter = new AviSplitter() as IBaseFilter;

        // Add the aviSplitter to the graph
        hr = graphBuilder.AddFilter(aviSplitter, "Splitter");
        DsError.ThrowExceptionForHR(hr);

        // Have the graph builder construct its appropriate graph automatically
        hr = this.graphBuilder.RenderFile(FileName, null);
        DsError.ThrowExceptionForHR(hr);

#if DEBUG
        m_rot = new DsROTEntry(graphBuilder);
#endif

        // Remove the video renderer filter
        IBaseFilter defaultVideoRenderer = null;
        graphBuilder.FindFilterByName("Video Renderer", out defaultVideoRenderer);
        graphBuilder.RemoveFilter(defaultVideoRenderer);

        // Disconnect anything that is connected
        // to the output of the sample grabber
        IPin iPinSampleGrabberOut = DsFindPin.ByDirection(sampleGrabberFilter, PinDirection.Output, 0);
        IPin iPinVideoIn;
        hr = iPinSampleGrabberOut.ConnectedTo(out iPinVideoIn);

        if (hr == 0)
        {
          // Disconnect the sample grabber output from the attached filters
          hr = iPinVideoIn.Disconnect();
          DsError.ThrowExceptionForHR(hr);

          hr = iPinSampleGrabberOut.Disconnect();
          DsError.ThrowExceptionForHR(hr);
        }
        else
        {
          // Try other way round because automatic renderer could not build
          // graph including the sample grabber
          IPin iPinAVISplitterOut = DsFindPin.ByDirection(aviSplitter, PinDirection.Output, 0);
          IPin iPinAVISplitterIn;
          hr = iPinAVISplitterOut.ConnectedTo(out iPinAVISplitterIn);
          DsError.ThrowExceptionForHR(hr);

          hr = iPinAVISplitterOut.Disconnect();
          DsError.ThrowExceptionForHR(hr);

          hr = iPinAVISplitterIn.Disconnect();
          DsError.ThrowExceptionForHR(hr);

          // Connect the avi splitter output to sample grabber
          IPin iPinSampleGrabberIn = DsFindPin.ByDirection(sampleGrabberFilter, PinDirection.Input, 0);
          hr = graphBuilder.Connect(iPinAVISplitterOut, iPinSampleGrabberIn);
          DsError.ThrowExceptionForHR(hr);
        }

        // Add the null renderer to the graph
        nullrenderer = new NullRenderer() as IBaseFilter;
        hr = graphBuilder.AddFilter(nullrenderer, "Null renderer");
        DsError.ThrowExceptionForHR(hr);

        // Get the input pin of the null renderer
        IPin iPinNullRendererIn = DsFindPin.ByDirection(nullrenderer, PinDirection.Input, 0);

        // Connect the sample grabber to the null renderer
        hr = graphBuilder.Connect(iPinSampleGrabberOut, iPinNullRendererIn);
        DsError.ThrowExceptionForHR(hr);

        // Read and cache the image sizes
        SaveSizeInfo(sampleGrabber);

        this.GetFrameStepInterface();
      }
      finally
      {
      }
    }

    /// <summary> Read and store the properties </summary>
    private void SaveSizeInfo(ISampleGrabber sampGrabber)
    {
      int hr;

      // Get the media type from the SampleGrabber
      AMMediaType media = new AMMediaType();
      hr = sampGrabber.GetConnectedMediaType(media);
      DsError.ThrowExceptionForHR(hr);

      if ((media.formatType != FormatType.VideoInfo) || (media.formatPtr == IntPtr.Zero))
      {
        throw new NotSupportedException("Unknown Grabber Media Format");
      }

      // Grab the size info
      VideoInfoHeader videoInfoHeader = (VideoInfoHeader)Marshal.PtrToStructure(media.formatPtr, typeof(VideoInfoHeader));
      m_videoWidth = videoInfoHeader.BmiHeader.Width;
      m_videoHeight = videoInfoHeader.BmiHeader.Height;
      m_stride = m_videoWidth * (videoInfoHeader.BmiHeader.BitCount / 8);

      //m_bitmapDataArray = new byte[videoInfoHeader.BmiHeader.ImageSize];
      m_handle = Marshal.AllocCoTaskMem(m_stride * m_videoHeight);

      DsUtils.FreeAMMediaType(media);
      media = null;
    }

    /// <summary> Set the options on the sample grabber </summary>
    private void ConfigureSampleGrabber(ISampleGrabber sampGrabber)
    {
      AMMediaType media;
      int hr;

      // Set the media type to Video/RBG24
      media = new AMMediaType();
      media.majorType = MediaType.Video;
      media.subType = MediaSubType.RGB24;
      media.formatType = FormatType.VideoInfo;
      hr = sampGrabber.SetMediaType(media);
      DsError.ThrowExceptionForHR(hr);

      DsUtils.FreeAMMediaType(media);
      media = null;

      // Choose to call BufferCB instead of SampleCB
      hr = sampGrabber.SetCallback(this, 1);
      DsError.ThrowExceptionForHR(hr);

      //hr = this.sampleGrabber.SetBufferSamples(true);
      //DsError.ThrowExceptionForHR(hr);

      //hr = this.sampleGrabber.SetOneShot(true);
      //DsError.ThrowExceptionForHR(hr);
    }

    /// <summary> Shut down capture </summary>
    private void CloseInterfaces()
    {
      int hr;
      try
      {
        if (m_handle != IntPtr.Zero)
        {
          Marshal.FreeCoTaskMem(m_handle);
          m_handle = IntPtr.Zero;
        }
      }
      catch (Exception)
      {
      }

      try
      {
        if (mediaControl != null)
        {
          // Stop the graph
          hr = mediaControl.Stop();
          mediaControl = null;
        }
      }
      catch (Exception ex)
      {
        Debug.WriteLine(ex);
      }

#if DEBUG
      if (m_rot != null)
      {
        m_rot.Dispose();
      }
#endif

      if (this.mediaSeeking != null)
        this.mediaSeeking = null;
      if (this.frameStep != null)
        this.frameStep = null;

      if (this.sampleGrabber != null)
      {
        Marshal.ReleaseComObject(this.sampleGrabber);
        this.sampleGrabber = null;
      }

      if (nullrenderer != null)
      {
        Marshal.ReleaseComObject(nullrenderer);
        nullrenderer = null;
      }

      if (graphBuilder != null)
      {
        Marshal.ReleaseComObject(graphBuilder);
        graphBuilder = null;
      }

      GC.Collect();
    }

    /// <summary>
    /// This method seeks the movie to the given position
    /// and waits for the buffer callback function to 
    /// be done, then returns the new bitmap of the
    /// video at the seek position
    /// </summary>
    /// <param name="ms">A timestamp in milliseconds.</param>
    /// <returns>A <see cref="Bitmap"/> with the snapshot
    /// of the video at the given millisecond.</returns>
    public Bitmap ShowSampleAtMS(long ms)
    {
      if (this.sampleGrabber != null)
      {
        callbackDone = false;
        this.SeekMovie(ms);
        this.mediaControl.Pause();

        do
        {
          Application.DoEvents();
        } while (!callbackDone);
      }

      return this.workingBitmap;
    }

    /// <summary> sample callback, NOT USED. </summary>
    int ISampleGrabberCB.SampleCB(double SampleTime, IMediaSample pSample)
    {
      Marshal.ReleaseComObject(pSample);
      return 0;
    }

    /// <summary>
    /// This method raises the <see cref="VideoFrameAvailable"/> 
    /// event by invoking the delegates.
    /// </summary>
    /// <param name="e">A <see cref="BitmapEventArgs"/> with the event data.</param>.
    private void OnVideoFrameAvailable(BitmapEventArgs e)
    {
      if (this.VideoFrameAvailable != null)
      {
        this.VideoFrameAvailable(this, e);
      }
    }

    /// <summary> buffer callback, COULD BE FROM FOREIGN THREAD. </summary>
    int ISampleGrabberCB.BufferCB(double SampleTime, IntPtr pBuffer, int BufferLen)
    {
      try
      {
        Kernel32.CopyMemory(m_handle, pBuffer, m_stride * m_videoHeight);

        // The next operation flips the video 
        int handle = (int)m_handle;
        handle += (m_videoHeight - 1) * m_stride;
        this.workingBitmap = new Bitmap(m_videoWidth, m_videoHeight, -m_stride, PixelFormat.Format24bppRgb, (IntPtr)handle);

        //Launch the event
        OnVideoFrameAvailable(new BitmapEventArgs(this.workingBitmap));

        callbackDone = true;
      }
      catch (ThreadInterruptedException e)
      {
        MessageBox.Show(e.Message);
      }
      catch (Exception we)
      {
        MessageBox.Show(we.Message);
      }

      //Marshal.Copy(pBuffer, m_bitmapDataArray, 0, BufferLen);

      //try
      //{
      //  GCHandle handle = GCHandle.Alloc(m_bitmapDataArray, GCHandleType.Pinned);
      //  int scan0 = (int)handle.AddrOfPinnedObject();
      //  scan0 += (m_videoHeight - 1) * m_stride;
      //  Bitmap b = new Bitmap(m_videoWidth, m_videoHeight, -m_stride, PixelFormat.Format24bppRgb, (IntPtr)scan0);
      //  handle.Free();

      //  //Launch the event
      //  OnVideoFrameAvailable(new BitmapEventArgs(b));
      //}
      //catch (ThreadInterruptedException e)
      //{
      //  MessageBox.Show(e.Message);
      //}
      //catch (Exception we)
      //{
      //  MessageBox.Show(we.Message);
      //}

      return 0;
    }

    /// <summary>
    /// This method seeks the movie to the given time position.
    /// The time has to be in units of the <see cref="IReferenceClock"/>.
    /// By default that are 100-nanosecond units.
    /// </summary>
    /// <param name="timeToStart">An <see cref="Int64"/> with the absolute start time to seek</param>
    public void SeekMovie(Int64 timeToStart)
    {
      try
      {
        if (this.mediaSeeking != null)
        {
          int hr = 0;

          hr = this.mediaSeeking.SetPositions(
            timeToStart * 10000, AMSeekingSeekingFlags.AbsolutePositioning, null, AMSeekingSeekingFlags.NoPositioning);
          DsError.ThrowExceptionForHR(hr);

          hr = this.mediaControl.StopWhenReady();
          DsError.ThrowExceptionForHR(hr);
          //if (this.mediaControl != null)
          //{
          //  FilterState state = FilterState.Stopped;
          //  int timeout = 100;
          //  hr = this.mediaControl.GetState(timeout, out state);
          //  DsError.ThrowExceptionForHR(hr);
          //  this.StepOneFrame();
          //}
        }
      }
      catch (Exception ex)
      {
        MessageBox.Show("Could not seek movie because: " + ex.Message);
      }
    }

    private int StepOneFrame()
    {
      int hr = 0;

      // If the Frame Stepping interface exists, use it to step one frame
      if (this.frameStep != null)
      {
        // The graph must be paused for frame stepping to work
        if (this.currentState != PlayState.Paused)
        {
          this.Pause();
        }

        // Step the requested number of frames, if supported
        hr = this.frameStep.Step(1, null);
      }

      return hr;
    }

    /// <summary>
    /// Loads the given movie into the graph.
    /// </summary>
    /// <param name="videoFile">A <see cref="String"/> with full path to the
    /// video file.</param>
    public void LoadMovie(string videoFile)
    {
      if (this.graphBuilder != null)
      {
        this.Dispose();
      }

      try
      {
        // Set up the capture graph
        SetupGraph(videoFile);
        int hr = this.mediaControl.StopWhenReady();
        DsError.ThrowExceptionForHR(hr);
      }
      catch
      {
        Dispose();
        throw;
      }
    }
  }
}
