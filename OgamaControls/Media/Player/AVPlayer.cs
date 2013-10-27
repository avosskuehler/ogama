using System;
using System.Drawing;
using System.Windows.Forms;
using DirectShowLib;
using System.Runtime.InteropServices;
using System.Drawing.Imaging;

namespace OgamaControls
{
  /// <summary>
  /// This <see cref="UserControl"/> encapsulates an DirectShow interface
  /// to play back videos.
  /// </summary>
  public partial class AVPlayer : UserControl
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

    private const int WMGraphNotify = 0x0400 + 13;
    private const int VolumeFull = 0;
    private const int VolumeSilence = -10000;

    private IGraphBuilder graphBuilder = null;
    private ICaptureGraphBuilder2 captureGraphBuilder = null;
    private IMediaControl mediaControl = null;
    //private IMediaEventEx mediaEventEx = null;
    private IMediaPosition mediaPosition = null;
    //private IVideoWindow videoWindow = null;
    private IBasicAudio basicAudio = null;
    private IBasicVideo basicVideo = null;
    private IMediaSeeking mediaSeeking = null;
    private IVideoFrameStep frameStep = null;
    private VideoMixingRenderer9 vmr9 = null;
    private IVMRFilterConfig9 vmrConfig = null;
    private IVMRWindowlessControl9 windowLessControl = null;

    private string filename = string.Empty;
    private bool isAudioOnly = false;
    //private bool isFullScreen = false;
    private int currentVolume = VolumeFull;
    private PlayState currentState = PlayState.Stopped;
    private double currentPlaybackRate = 1.0;

    private IntPtr hDrain = IntPtr.Zero;

#if DEBUG
    private DsROTEntry rot = null;
#endif

    #endregion //FIELDS

    ///////////////////////////////////////////////////////////////////////////////
    // Construction and Initializing methods                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region CONSTRUCTION

    /// <summary>
    /// Initializes a new instance of the AVPlayer class.
    /// </summary>
    public AVPlayer()
    {
      InitializeComponent();
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
    /// Gets the current <see cref="PlayState"/>.
    /// </summary>
    /// <value>The current <see cref="PlayState"/> of the movie</value>
    public PlayState PlayState
    {
      get { return this.currentState; }
    }

    /// <summary>
    /// Gets the filename with path to the currently loaded movie file.
    /// </summary>
    /// <value>A <see cref="string"/> with the currently loaded movie file.</value>
    public string MovieFile
    {
      get { return this.filename; }
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
          UpdateToolTip();
        }
      }
    }

    /// <summary>
    /// Gets the native <see cref="Size"/> of the video stream.
    /// </summary>
    public Size VideoSize
    {
      get
      {
        Size videoSize = new Size();
        if (this.vmr9 != null)
        {
          int width;
          int height;
          int aspectRatioX;
          int aspectRatioY;
          int hr = this.windowLessControl.GetNativeVideoSize(out width, out height, out aspectRatioX, out aspectRatioY);
          DsError.ThrowExceptionForHR(hr);
          videoSize.Width = width;
          videoSize.Height = height;
        }

        return videoSize;
      }
    }

    /// <summary>
    /// Gets a <see cref="Bitmap"/> with a screenshot of
    /// the current video renderer output.
    /// </summary>
    public Bitmap Screenshot
    {
      get
      {
        Bitmap retBmp = null;
        int hr = 0;
        IntPtr iDIB = IntPtr.Zero;

        if (this.vmr9 != null)
        {
          IVMRWindowlessControl9 imageCaputureInterface = this.vmr9 as IVMRWindowlessControl9;

          while (iDIB == IntPtr.Zero)
          {
            hr = imageCaputureInterface.GetCurrentImage(out iDIB);
          }

          BitmapInfoHeader bih = (BitmapInfoHeader)Marshal.PtrToStructure(iDIB, typeof(BitmapInfoHeader));

          int width = bih.Width;
          int height = bih.Height;
          int stride = width * (bih.BitCount / 8);
          PixelFormat pixelFormat = PixelFormat.Format24bppRgb;

          switch (bih.BitCount)
          {
            case 24: pixelFormat = PixelFormat.Format24bppRgb; break;
            case 32: pixelFormat = PixelFormat.Format32bppRgb; break;
            case 48: pixelFormat = PixelFormat.Format48bppRgb; break;
            default: throw new Exception("Unknown BitCount");
          }

          retBmp = new Bitmap(width, height, stride, pixelFormat, iDIB);
          retBmp.RotateFlip(RotateFlipType.RotateNoneFlipY);

          if (iDIB != null && iDIB != IntPtr.Zero)
          {
            Marshal.FreeCoTaskMem(iDIB);
            iDIB = IntPtr.Zero;
          }
        }

        return retBmp;
      }
    }

    //public Bitmap Screenshot
    //{
    //  get
    //  {
    //    if (this.basicVideo != null)
    //    {
    //      int bufferSize = 0;
    //      int hr = this.basicVideo.GetCurrentImage(ref bufferSize, IntPtr.Zero);
    //      DsError.ThrowExceptionForHR(hr);

    //      IntPtr bitmapPointer = Marshal.AllocHGlobal(bufferSize);

    //      hr = this.basicVideo.GetCurrentImage(ref bufferSize, bitmapPointer);
    //      DsError.ThrowExceptionForHR(hr);

    //      Size videoSize = this.VideoSize;
    //      int stride = -4 * videoSize.Width;
    //      PixelFormat format = PixelFormat.Format32bppRgb;
    //      IntPtr scan0 = (IntPtr)(((long)bitmapPointer));// + (bufferSize + stride));

    //      Bitmap bmp = new Bitmap(videoSize.Width, videoSize.Height, stride, format, bitmapPointer);
    //      bmp.Save(@"c:\Dumps\test.png");
    //      Marshal.FreeHGlobal(bitmapPointer);
    //      return bmp;
    //    }

    //    return null;
    //  }
    //}


    #endregion //PROPERTIES

    ///////////////////////////////////////////////////////////////////////////////
    // Public methods                                                            //
    ///////////////////////////////////////////////////////////////////////////////
    #region PUBLICMETHODS

    /// <summary>
    /// This method loads a movie given by the parameter into
    /// this control and displays its first frame.
    /// If the filename is empty a file dialog is shown.
    /// </summary>
    /// <param name="filename">A <see cref="string"/> with the full filename of
    /// the movie to play.</param>
    /// <remarks>If another movie is already loaded it is closed before
    /// the new movie is loaded.</remarks>
    public void LoadMovie(string filename)
    {
      this.CloseClip();
      this.OpenClip(filename);
      if (this.mediaControl != null)
      {
        this.mediaControl.Stop();
        this.currentState = PlayState.Stopped;
      }
      this.StepOneFrame();
    }

    /// <summary>
    /// This method closes a currently loaded movie.
    /// </summary>
    public void CloseMovie()
    {
      this.CloseClip();
    }

    /// <summary>
    /// This method seeks the movie to the given time position.
    /// The time has to be in units of the <see cref="IReferenceClock"/>.
    /// By default that are 100-nanosecond units.
    /// </summary>
    /// <param name="timeToStart">An <see cref="Int64"/> with the absolute start time to seek, 
    /// or null if the start time should not be set</param>
    /// <param name="timeToStop">An <see cref="Int64"/> with the absolute time at which the play should stop,
    /// or null if the stop time should not be set.</param>
    /// <remarks>If the timeToStop parameter is null, the stop time is set to the duration
    /// of the video.</remarks>
    public void SeekMovie(Int64? timeToStart, Int64? timeToStop)
    {
      try
      {
        if (this.mediaSeeking != null)
        {
          int hr = 0;
          if (timeToStart.HasValue)
          {
            hr = this.mediaSeeking.SetPositions(timeToStart.Value * 10000, AMSeekingSeekingFlags.AbsolutePositioning,
              null, AMSeekingSeekingFlags.NoPositioning);
            DsError.ThrowExceptionForHR(hr);
          }

          if (this.mediaControl != null)
          {
            FilterState state = FilterState.Stopped;
            int timeout = 100;
            hr = this.mediaControl.GetState(timeout, out state);
            DsError.ThrowExceptionForHR(hr);
            this.StepOneFrame();
          }
        }
      }
      catch (Exception ex)
      {
        MessageBox.Show("Could not seek movie because: " + ex.Message);
      }
    }

    /// <summary>
    /// This method starts the replay of an already loaded movie.
    /// If no movie is loaded before vie <see cref="LoadMovie(string)"/>
    /// this method does nothing.
    /// </summary>
    public void PlayMovie()
    {
      if (this.mediaControl != null)
      {
        // Run the graph to play the media file
        int hr = this.mediaControl.Run();
        DsError.ThrowExceptionForHR(hr);

        this.currentState = PlayState.Running;
      }
    }

    /// <summary>
    /// This method stops a currently running movie.
    /// </summary>
    public void StopMovie()
    {
      if (this.mediaControl != null)
      {
        // Run the graph to play the media file
        int hr = this.mediaControl.StopWhenReady();
        DsError.ThrowExceptionForHR(hr);

        this.currentState = PlayState.Stopped;
      }
    }

    #endregion //PUBLICMETHODS

    ///////////////////////////////////////////////////////////////////////////////
    // Eventhandler                                                              //
    ///////////////////////////////////////////////////////////////////////////////
    #region EVENTS

    ///////////////////////////////////////////////////////////////////////////////
    // Eventhandler for UI, Menu, Buttons, Toolbars etc.                         //
    ///////////////////////////////////////////////////////////////////////////////
    #region WINDOWSEVENTHANDLER

    private void AVPlayer_KeyDown(object sender, KeyEventArgs e)
    {
      //switch (e.KeyCode)
      //{
      //  case Keys.Space:
      //    {
      //      StepOneFrame();
      //      break;
      //    }
      //  case Keys.Left:
      //    {
      //      ModifyRate(-0.25);
      //      break;
      //    }
      //  case Keys.Right:
      //    {
      //      ModifyRate(+0.25);
      //      break;
      //    }
      //  case Keys.Down:
      //    {
      //      SetRate(1.0);
      //      break;
      //    }
      //  case Keys.P:
      //    {
      //      PauseClip();
      //      break;
      //    }
      //  case Keys.S:
      //    {
      //      StopClip();
      //      break;
      //    }
      //  case Keys.M:
      //    {
      //      ToggleMute();
      //      break;
      //    }
      //  case Keys.F:
      //  case Keys.Return:
      //    {
      //      ToggleFullScreen();
      //      break;
      //    }
      //  case Keys.Escape:
      //    {
      //      if (this.isFullScreen)
      //        ToggleFullScreen();
      //      else
      //        CloseClip();
      //      break;
      //    }
      //  case Keys.F12 | Keys.Q | Keys.X:
      //    {
      //      CloseClip();
      //      break;
      //    }
      //}
    }

    private void AVPlayer_Resize(object sender, EventArgs e)
    {
      // Resize the video preview window to match owner window size
      if (this.windowLessControl != null)
      {
        this.windowLessControl.SetVideoPosition(null, new DsRect(0, 0, this.ClientSize.Width, this.ClientSize.Height));
      }

      //// Resize the video preview window to match owner window size
      //if (this.videoWindow != null)
      //{
      //  this.videoWindow.SetWindowPosition(0, 0, this.Width, this.Height);
      //}
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
    // Inherited methods                                                         //
    ///////////////////////////////////////////////////////////////////////////////
    #region OVERRIDES

    ///// <summary>
    ///// Processes Windows messages.
    ///// </summary>
    ///// <param name="m">Ref. The Windows <see cref="Message"/> to process.</param>
    //protected override void WndProc(ref Message m)
    //{
    //  //switch (m.Msg)
    //  //{
    //  //  case WMGraphNotify:
    //  //    {
    //  //      HandleGraphEvent();
    //  //      break;
    //  //    }
    //  //}

    //  //// Pass this message to the video window for notification of system changes
    //  //if (this.videoWindow != null)
    //  //  this.videoWindow.NotifyOwnerMessage(m.HWnd, m.Msg, m.WParam, m.LParam);

    //  base.WndProc(ref m);
    //}

    #endregion //OVERRIDES

    ///////////////////////////////////////////////////////////////////////////////
    // Methods for doing main class job                                          //
    ///////////////////////////////////////////////////////////////////////////////
    #region PRIVATEMETHODS

    private void OpenClip(string filename)
    {
      try
      {
        this.filename = filename;

        // If no filename is specified, show file open dialog
        if (this.filename == string.Empty)
        {
          UpdateToolTip();

          this.filename = GetClipFileName();
          if (this.filename == string.Empty)
            return;
        }

        // Reset status variables
        this.currentState = PlayState.Stopped;
        this.currentVolume = VolumeFull;

        // Start playing the media file
        LoadMovieInWindow(this.filename);
      }
      catch
      {
        CloseClip();
      }
    }

    private void LoadMovieInWindow(string filename)
    {
      int hr = 0;

      if (filename == string.Empty)
        return;

      this.graphBuilder = (IGraphBuilder)new FilterGraph();
      this.captureGraphBuilder = (ICaptureGraphBuilder2)new CaptureGraphBuilder2();
      this.vmr9 = new VideoMixingRenderer9();
      this.vmrConfig = this.vmr9 as IVMRFilterConfig9;

      // Attach the filter graph to the capture graph
      hr = this.captureGraphBuilder.SetFiltergraph(this.graphBuilder);
      DsError.ThrowExceptionForHR(hr);

      hr = this.graphBuilder.AddFilter(vmr9 as IBaseFilter, "VideoMixingRenderer9");
      DsError.ThrowExceptionForHR(hr);

      hr = this.vmrConfig.SetRenderingMode(VMR9Mode.Windowless);
      DsError.ThrowExceptionForHR(hr);
      this.windowLessControl = this.vmr9 as IVMRWindowlessControl9;
      this.windowLessControl.SetVideoClippingWindow(this.Handle);
      this.windowLessControl.SetVideoPosition(null, new DsRect(0, 0, this.ClientSize.Width, this.ClientSize.Height));

      IBaseFilter fileSourceFilter;
      hr = this.graphBuilder.AddSourceFilter(filename, "WebCamSource", out fileSourceFilter);
      DsError.ThrowExceptionForHR(hr);

      hr = this.captureGraphBuilder.RenderStream(null, null, fileSourceFilter, null, vmr9 as IBaseFilter);
      DsError.ThrowExceptionForHR(hr);

      //// Have the graph builder construct its the appropriate graph automatically
      //hr = this.graphBuilder.RenderFile(filename, null);
      //DsError.ThrowExceptionForHR(hr);

      // QueryInterface for DirectShow interfaces
      this.mediaControl = (IMediaControl)this.graphBuilder;
      //this.mediaEventEx = (IMediaEventEx)this.graphBuilder;
      this.mediaSeeking = (IMediaSeeking)this.graphBuilder;
      //this.mediaPosition = (IMediaPosition)this.graphBuilder;

      // Query for video interfaces, which may not be relevant for audio files
      ////this.videoWindow = this.graphBuilder as IVideoWindow;
      this.basicVideo = this.graphBuilder as IBasicVideo;

      // Query for audio interfaces, which may not be relevant for video-only files
      this.basicAudio = this.graphBuilder as IBasicAudio;

      // Is this an audio-only file (no video component)?
      CheckVisibility();

      //// Have the graph signal event via window callbacks for performance
      //hr = this.mediaEventEx.SetNotifyWindow(this.Handle, WMGraphNotify, IntPtr.Zero);
      //DsError.ThrowExceptionForHR(hr);

      if (!this.isAudioOnly)
      {
        this.windowLessControl = this.vmr9 as IVMRWindowlessControl9;
        this.windowLessControl.SetVideoClippingWindow(this.Handle);
        this.windowLessControl.SetVideoPosition(null, new DsRect(0, 0, this.ClientSize.Width, this.ClientSize.Height));

        //hr = InitVideoWindow();
        //DsError.ThrowExceptionForHR(hr);

        GetFrameStepInterface();
      }

      // Complete window initialization
      //this.isFullScreen = false;
      this.currentPlaybackRate = 1.0;
      //UpdateToolTip();

#if DEBUG
      rot = new DsROTEntry(this.graphBuilder);
#endif
    }

    private void CloseClip()
    {
      int hr = 0;

      // Stop media playback
      if (this.mediaControl != null)
        hr = this.mediaControl.Stop();

      // Clear global flags
      this.currentState = PlayState.Stopped;
      this.isAudioOnly = true;
      //this.isFullScreen = false;

      // Free DirectShow interfaces
      CloseInterfaces();

      // Clear file name to allow selection of new file with open dialog
      this.filename = string.Empty;

      // No current media state
      this.currentState = PlayState.Init;

      UpdateToolTip();
    }

    //private int InitVideoWindow()
    //{
    //  // Set the owner of the videoWindow to an IntPtr of some sort 
    //  // (the Handle of any control - could be a form / button etc.)
    //  int hr = this.videoWindow.put_Owner(this.Handle);
    //  DsError.ThrowExceptionForHR(hr);

    //  // Set the style of the video window
    //  hr = this.videoWindow.put_WindowStyle(WindowStyle.Child | WindowStyle.ClipSiblings | WindowStyle.ClipChildren);
    //  DsError.ThrowExceptionForHR(hr);

    //  // Position video window in client rect
    //  hr = this.videoWindow.SetWindowPosition(0, 0, this.Width, this.Height);
    //  DsError.ThrowExceptionForHR(hr);

    //  // Make the video window visible
    //  hr = this.videoWindow.put_Visible(OABool.True);
    //  DsError.ThrowExceptionForHR(hr);

    //  return hr;
    //}

    private void CheckVisibility()
    {
      //int hr = 0;
      //OABool lVisible;

      if (this.basicVideo == null)
      {
        // Audio-only files have no video interfaces.  This might also
        // be a file whose video component uses an unknown video codec.
        this.isAudioOnly = true;
        return;
      }
      else
      {
        // Clear the global flag
        this.isAudioOnly = false;
      }
    }

    //
    // Some video renderers support stepping media frame by frame with the
    // IVideoFrameStep interface.  See the interface documentation for more
    // details on frame stepping.
    //
    private bool GetFrameStepInterface()
    {
      int hr = 0;

      IVideoFrameStep frameStepTest = null;

      // Get the frame step interface, if supported
      frameStepTest = (IVideoFrameStep)this.graphBuilder;

      // Check if this decoder can step
      hr = frameStepTest.CanStep(0, null);
      if (hr == 0)
      {
        this.frameStep = frameStepTest;
        return true;
      }
      else
      {
        // BUG 1560263 found by husakm (thanks)...
        // Marshal.ReleaseComObject(frameStepTest);
        this.frameStep = null;
        return false;
      }
    }

    private void CloseInterfaces()
    {
      //int hr = 0;

      try
      {
        lock (this)
        {
          //// Relinquish ownership (IMPORTANT!) after hiding video window
          //if (!this.isAudioOnly && this.videoWindow != null)
          //{
          //  hr = this.videoWindow.put_Visible(OABool.False);
          //  DsError.ThrowExceptionForHR(hr);
          //  hr = this.videoWindow.put_Owner(IntPtr.Zero);
          //  DsError.ThrowExceptionForHR(hr);
          //}

          //if (this.mediaEventEx != null)
          //{
          //  hr = this.mediaEventEx.SetNotifyWindow(IntPtr.Zero, 0, IntPtr.Zero);
          //  DsError.ThrowExceptionForHR(hr);
          //}

#if DEBUG
          if (rot != null)
          {
            rot.Dispose();
            rot = null;
          }
#endif
          // Release and zero DirectShow interfaces
          //if (this.mediaEventEx != null)
          //  this.mediaEventEx = null;
          if (this.mediaSeeking != null)
            this.mediaSeeking = null;
          if (this.mediaPosition != null)
            this.mediaPosition = null;
          if (this.mediaControl != null)
            this.mediaControl = null;
          if (this.basicAudio != null)
            this.basicAudio = null;
          if (this.basicVideo != null)
            this.basicVideo = null;
          //if (this.videoWindow != null)
          //  this.videoWindow = null;
          if (this.frameStep != null)
            this.frameStep = null;
          if (this.graphBuilder != null)
            Marshal.ReleaseComObject(this.graphBuilder);
          this.graphBuilder = null;

          GC.Collect();
        }
      }
      catch (Exception ex)
      {
        MessageBox.Show(ex.StackTrace, ex.Message);
      }
    }

    /*
     * Media Related methods
     */

    /// <summary>
    /// Pauses / unpauses clip according to state.
    /// </summary>
    public void PauseMovie()
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

      UpdateToolTip();
    }

    private void StopClip()
    {
      int hr = 0;
      DsLong pos = new DsLong(0);

      if ((this.mediaControl == null) || (this.mediaSeeking == null))
        return;

      // Stop and reset postion to beginning
      if ((this.currentState == PlayState.Paused) || (this.currentState == PlayState.Running))
      {
        hr = this.mediaControl.Stop();
        this.currentState = PlayState.Stopped;

        // Seek to the beginning
        hr = this.mediaSeeking.SetPositions(pos, AMSeekingSeekingFlags.AbsolutePositioning, null, AMSeekingSeekingFlags.NoPositioning);

        // Display the first frame to indicate the reset condition
        hr = this.mediaControl.Pause();
      }
      UpdateToolTip();
    }

    private int ToggleMute()
    {
      int hr = 0;

      if ((this.graphBuilder == null) || (this.basicAudio == null))
        return 0;

      // Read current volume
      hr = this.basicAudio.get_Volume(out this.currentVolume);
      if (hr == -1) //E_NOTIMPL
      {
        // Fail quietly if this is a video-only media file
        return 0;
      }
      else if (hr < 0)
      {
        return hr;
      }

      // Switch volume levels
      if (this.currentVolume == VolumeFull)
        this.currentVolume = VolumeSilence;
      else
        this.currentVolume = VolumeFull;

      // Set new volume
      hr = this.basicAudio.put_Volume(this.currentVolume);

      UpdateToolTip();
      return hr;
    }

    private int ToggleFullScreen()
    {
      int hr = 0;
      //OABool lMode;

      //// Don't bother with full-screen for audio-only files
      //if ((this.isAudioOnly) || (this.videoWindow == null))
      //  return 0;

      //// Read current state
      //hr = this.videoWindow.get_FullScreenMode(out lMode);
      //DsError.ThrowExceptionForHR(hr);

      //if (lMode == OABool.False)
      //{
      //  // Save current message drain
      //  hr = this.videoWindow.get_MessageDrain(out hDrain);
      //  DsError.ThrowExceptionForHR(hr);

      //  // Set message drain to application main window
      //  hr = this.videoWindow.put_MessageDrain(this.Handle);
      //  DsError.ThrowExceptionForHR(hr);

      //  // Switch to full-screen mode
      //  lMode = OABool.True;
      //  hr = this.videoWindow.put_FullScreenMode(lMode);
      //  DsError.ThrowExceptionForHR(hr);
      //  //this.isFullScreen = true;
      //}
      //else
      //{
      //  // Switch back to windowed mode
      //  lMode = OABool.False;
      //  hr = this.videoWindow.put_FullScreenMode(lMode);
      //  DsError.ThrowExceptionForHR(hr);

      //  // Undo change of message drain
      //  hr = this.videoWindow.put_MessageDrain(hDrain);
      //  DsError.ThrowExceptionForHR(hr);

      //  // Reset video window
      //  hr = this.videoWindow.SetWindowForeground(OABool.True);
      //  DsError.ThrowExceptionForHR(hr);

      //  // Reclaim keyboard focus for player application
      //  //this.Focus();
      //  //this.isFullScreen = false;
      //}

      return hr;
    }

    private int StepOneFrame()
    {
      int hr = 0;

      // If the Frame Stepping interface exists, use it to step one frame
      if (this.frameStep != null)
      {
        // The graph must be paused for frame stepping to work
        if (this.currentState != PlayState.Paused)
          PauseMovie();

        // Step the requested number of frames, if supported
        hr = this.frameStep.Step(1, null);
      }

      return hr;
    }

    private int StepFrames(int nFramesToStep)
    {
      int hr = 0;

      // If the Frame Stepping interface exists, use it to step frames
      if (this.frameStep != null)
      {
        // The renderer may not support frame stepping for more than one
        // frame at a time, so check for support.  S_OK indicates that the
        // renderer can step nFramesToStep successfully.
        hr = this.frameStep.CanStep(nFramesToStep, null);
        if (hr == 0)
        {
          // The graph must be paused for frame stepping to work
          if (this.currentState != PlayState.Paused)
            PauseMovie();

          // Step the requested number of frames, if supported
          hr = this.frameStep.Step(nFramesToStep, null);
        }
      }

      return hr;
    }

    private int ModifyRate(double dRateAdjust)
    {
      int hr = 0;
      double dRate;

      // If the IMediaSeeking interface exists, use it to set rate
      if ((this.mediaSeeking != null) && (dRateAdjust != 0.0))
      {
        hr = this.mediaSeeking.GetRate(out dRate);
        DsError.ThrowExceptionForHR(hr);

        // Add current rate to adjustment value
        double dNewRate = dRate + dRateAdjust;
        hr = this.mediaSeeking.SetRate(dNewRate);
        DsError.ThrowExceptionForHR(hr);

        // Save global rate
        this.currentPlaybackRate = dNewRate;
        UpdateToolTip();
      }

      return hr;
    }



    //private void HandleGraphEvent()
    //{
    //  int hr = 0;
    //  EventCode evCode;
    //  IntPtr evParam1, evParam2;

    //  // Make sure that we don't access the media event interface
    //  // after it has already been released.
    //  if (this.mediaEventEx == null)
    //    return;

    //  // Process all queued events
    //  while (this.mediaEventEx.GetEvent(out evCode, out evParam1, out evParam2, 0) == 0)
    //  {
    //    // Free memory associated with callback, since we're not using it
    //    hr = this.mediaEventEx.FreeEventParams(evCode, evParam1, evParam2);

    //    // If this is the end of the clip, reset to beginning
    //    if (evCode == EventCode.Complete)
    //    {
    //      DsLong pos = new DsLong(0);
    //      // Reset to first frame of movie
    //      hr = this.mediaSeeking.SetPositions(pos, AMSeekingSeekingFlags.AbsolutePositioning,
    //        null, AMSeekingSeekingFlags.NoPositioning);
    //      if (hr < 0)
    //      {
    //        // Some custom filters (like the Windows CE MIDI filter)
    //        // may not implement seeking interfaces (IMediaSeeking)
    //        // to allow seeking to the start.  In that case, just stop
    //        // and restart for the same effect.  This should not be
    //        // necessary in most cases.
    //        hr = this.mediaControl.Stop();
    //        hr = this.mediaControl.Run();
    //      }
    //    }
    //  }
    //}

    private string GetClipFileName()
    {
      if (openFileDialog1.ShowDialog() == DialogResult.OK)
      {
        return openFileDialog1.FileName;
      }
      else
        return string.Empty;
    }

    private void UpdateToolTip()
    {
      // If no file is loaded, just show the application title
      if (this.filename == string.Empty)
      {
        this.toolTip.SetToolTip(this, "PlayWnd Media Player");
      }
      else
      {
        string media = (isAudioOnly) ? "Audio" : "Video";
        string muted = (currentVolume == VolumeSilence) ? "Mute" : "";
        string paused = (currentState == PlayState.Paused) ? "Paused" : "";

        this.toolTip.SetToolTip(
          this,
          string.Format("{0} [{1}] {2}{3}",
          System.IO.Path.GetFileName(this.filename),
          media,
          muted,
          paused));
      }
    }

    private void CheckSizeMenu(MenuItem item)
    {
      //menuFileSizeHalf.Checked = false;
      //menuFileSizeThreeQuarter.Checked = false;
      //menuFileSizeNormal.Checked = false;
      //menuFileSizeDouble.Checked = false;

      item.Checked = true;
    }

    private void cmuAudio_Click(object sender, EventArgs e)
    {
      if (cmuAudio.Checked)
      {
        this.EnableAudio(true);
      }
      else
      {
        this.EnableAudio(false);
      }
    }

    //private void EnablePlaybackMenu(bool bEnable, MediaType nMediaType)
    //{
    //  // Enable/disable menu items related to playback (pause, stop, mute)
    //  menuFilePause.Enabled = bEnable;
    //  menuFileStop.Enabled = bEnable;
    //  menuFileMute.Enabled = bEnable;
    //  menuRateIncrease.Enabled = bEnable;
    //  menuRateDecrease.Enabled = bEnable;
    //  menuRateNormal.Enabled = bEnable;
    //  menuRateHalf.Enabled = bEnable;
    //  menuRateDouble.Enabled = bEnable;

    //  // Enable/disable menu items related to video size
    //  bool isVideo = (nMediaType == MediaType.Video) ? true : false;

    //  menuSingleStep.Enabled = isVideo;
    //  menuFileSizeHalf.Enabled = isVideo;
    //  menuFileSizeDouble.Enabled = isVideo;
    //  menuFileSizeNormal.Enabled = isVideo;
    //  menuFileSizeThreeQuarter.Enabled = isVideo;
    //  menuFileFullScreen.Enabled = isVideo;
    //}

    #endregion //PRIVATEMETHODS


    ///////////////////////////////////////////////////////////////////////////////
    // Small helping Methods                                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region HELPER

    private void EnableAudio(bool enable)
    {
      try
      {
        if (this.isAudioOnly)
        {
          return;
        }

        if (this.mediaControl == null)
        {
          return;
        }

        //Need to stop replay, otherwise removing of filters will crash
        this.mediaControl.Stop();

        int hr = 0;
        bool isAudio = false;

        if (!enable)
        {
          // All audio filters should be removed,
          // so iterate through all filters, find each with a audio pin
          // and remove it from the graph

          IEnumFilters enumFilters = null;

          if (graphBuilder == null)
            throw new ArgumentNullException("graphBuilder");

          hr = graphBuilder.EnumFilters(out enumFilters);
          if (hr == 0)
          {
            IBaseFilter[] filters = new IBaseFilter[1];

            while (enumFilters.Next(filters.Length, filters, IntPtr.Zero) == 0)
            {
              FilterInfo filterInfo;

              hr = filters[0].QueryFilterInfo(out filterInfo);
              if (hr == 0)
              {
                if (filterInfo.pGraph != null)
                  Marshal.ReleaseComObject(filterInfo.pGraph);

                IEnumPins pins;
                filters[0].EnumPins(out pins);
                IPin[] pin = new IPin[1];
                while (pins.Next(1, pin, IntPtr.Zero) == 0)
                {
                  AMMediaType media = new AMMediaType();
                  pin[0].ConnectionMediaType(media);
                  if (media.majorType == MediaType.Audio)
                  {
                    isAudio = true;
                    break;
                  }
                }
                if (isAudio)
                {
                  hr = this.graphBuilder.RemoveFilter(filters[0]);
                  DsError.ThrowExceptionForHR(hr);
                }
              }
            }

            if (filters[0] != null)
            {
              Marshal.ReleaseComObject(filters[0]);
            }
          }
          Marshal.ReleaseComObject(enumFilters);
        }
        else
        {
          //Recreate whole replay graph, disposing any old values first.
          LoadMovie(this.filename);
        }
      }
      catch (Exception ex)
      {
        MessageBox.Show(ex.Message);
      }
    }

    #endregion //HELPER
  }
}
