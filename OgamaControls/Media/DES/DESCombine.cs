using DirectShowLib.DES;
using DirectShowLib;
using System.Runtime.InteropServices;
using System;
using System.Windows.Forms;
using System.Threading;
using System.IO;

namespace OgamaControls.Media
{
  /// <summary>
  /// Takes a combination of video or audio files of various formats and stitches them together 
  /// sequentially.  The result can be output to one of several formats including a video window, 
  /// or an AVI file.
  /// </summary>
  /// <remarks>The normal sequence when using this class would be:
  /// <list type="number">
  /// <item>Call the constructor</item>
  /// <item>Call the Add*File methods to specify the video/audio files to combine</item>
  /// <item>Call RenderTo* to specify where to render the output</item>
  /// <item>Call StartRendering to begin the rendering</item>
  /// </list>
  /// <para></para>
  /// <para>
  /// When calling the RenderTo* methods, a callback routine can be registered so that each audio or video
  /// frame will be passed to the CB routine.</para>
  /// </remarks>
  public class DESCombine : IDisposable
  {
    #region Declarations

    /// <summary>
    /// Number of MilliSeconds in a second.
    /// </summary>
    /// <remarks>
    /// This constant may be useful for calculations
    /// </remarks>
    public const long MILLISECONDS = (1000);            // 10 ^ 3

    /// <summary>
    /// Number of NanoSeconds in a second.
    /// </summary>
    /// <remarks>
    /// This constant may be useful for calculations
    /// </remarks>
    public const long NANOSECONDS = (1000000000);       // 10 ^ 9

    /// <summary>
    /// Number of 100NS in a second.
    /// </summary>
    /// <remarks>
    /// To convert from seconds to 100NS 
    /// units (used by most DES function), multiply the seconds by UNITS.
    /// </remarks>
    public const long UNITS = (NANOSECONDS / 100);      // 10 ^ 7

    /// <summary>
    /// Event code indicating a video file has finished being processed
    /// </summary>
    internal const EventCode EC_VideoFileComplete = (EventCode)0x8000;

    /// <summary>
    /// Event code indicating an audio file has finished being processed
    /// </summary>
    internal const EventCode EC_AudioFileComplete = (EventCode)0x8001;

    private bool threadCompleted;

    /// <summary>
    /// Used to indicate the state of the class
    /// </summary>
    private enum ClassState
    {
      Constructed,
      FilesAdded,
      RenderSelected,
      GraphStarted,
      Paused,
      Running,
      Stopped,
      GraphCompleting,
      GraphCompleted,
      Cancelling,
      Cancelled
    }

    #endregion

    #region Data Members

    // How much of the process of rendering the class has been completed
    volatile private ClassState m_State;

    /// <summary>
    /// Status of the video timeline
    /// </summary>
    private MediaGroup m_Video;

    /// <summary>
    /// Status of the audio timeline
    /// </summary>
    private MediaGroup m_Audio;

    /// <summary>
    /// Pointer to the timeline containing the audio and video tracks
    /// </summary>
    private IAMTimeline m_pTimeline;

    /// <summary>
    /// IGraphBuilder object for the timeline
    /// </summary>
    private IGraphBuilder m_pGraph;

    /// <summary>
    /// Media control interface from m_pGraph
    /// </summary>
    private IMediaControl m_pControl;

    /// <summary>
    /// The engine to process the timeline (can't be released
    /// until the graph processing is complete)
    /// </summary>
    private IRenderEngine m_pRenderEngine;

#if DEBUG
    /// <summary>
    /// Used when adding the graph to the Running Object Table.
    /// This allows the graph to be inspected from GraphEdit using
    /// File/Connect
    /// </summary>
    private DsROTEntry m_rot;
#endif

    #endregion

    private bool supportAudio;

    private bool supportVideo;

    /// <summary>
    /// Construct the class
    /// </summary>
    /// <remarks>
    /// The input files do not need to have the same height/width/fps.  DES will
    /// automatically convert them all to the values specified by the constructor.
    /// </remarks>
    /// <param name="FPS">Frames per second (commonly 15 or 30)</param>
    /// <param name="BitCount">Color depth: 16, 24 or 32</param>
    /// <param name="Width">Frame width (commonly 320, 640, etc)</param>
    /// <param name="Height">Frame height (commonly 240, 480, etc)</param>
    /// <param name="audio">support only audio files</param>
    /// <param name="video">support only video files</param>
    public DESCombine(double FPS, short BitCount, int Width, int Height, bool audio, bool video)
    {
      threadCompleted = true;

      // Initialize the data members
      m_State = ClassState.Constructed;

      // Create the timeline
      m_pTimeline = (IAMTimeline)new AMTimeline();

      // Set the frames per second
      int hr = m_pTimeline.SetDefaultFPS(FPS);
      DESError.ThrowExceptionForHR(hr);

      supportAudio = audio;
      supportVideo = video;

      if (supportVideo)
      {
        // Init the video group
        m_Video = new MediaGroup(GetVideoMediaType(BitCount, Width, Height), m_pTimeline, FPS);
      }

      if (supportAudio)
      {
        // Init the audio group
        m_Audio = new MediaGroup(GetAudioMediaType(), m_pTimeline, FPS);
      }

      this.ThreadFinished += new EventHandler(DESCombine_ThreadFinished);
    }

    private void DESCombine_ThreadFinished(object sender, EventArgs e)
    {
      this.StopRendering();
      this.Dispose();
      if (this.Completed != null)
      {
        this.Completed(this, e);
      }
    }

    /// <exclude></exclude>
    ~DESCombine()
    {
      Dispose();
    }

    /// <summary>
    /// Add a file with both video and audio to the timeline.
    /// </summary>
    /// <remarks>
    /// If a file contains both audio and video, this method can be used
    /// to add the file to both groups at the same time.
    /// </remarks>
    /// <para>Video files can be of different 
    /// lengths, frame sizes, color depths, aspect ratios, frame rates, etc.  DES 
    /// converts them all to the values specified in the constructor.</para>
    /// 
    /// <param name="sFileName">A/V file path+name</param>
    /// <param name="lStart">Start position within the file specified in 100ns units</param>
    /// <param name="lStop">End position within the file specified in 100ns units 
    /// or -1 to add entire file</param>
    public void AddAVFile(
        string sFileName,
        long lStart,
        long lStop)
    {
      if (m_State > ClassState.FilesAdded)
      {
        throw new Exception("Can't add files since rendering method already selected");
      }

      if (!File.Exists(sFileName))
      {
        throw new FileNotFoundException("Can´t add file because its not found at the provided location.");
      }

      if (!supportVideo)
      {
        throw new NotSupportedException("Can´t add files because this player is initialized without video support.");
      }
      if (!supportAudio)
      {
        throw new NotSupportedException("Can´t add files because this player is initialized without audio support.");
      }

      m_Video.Add(sFileName, lStart, lStop);
      m_Audio.Add(sFileName, lStart, lStop);

      // At least one file has now been added
      m_State = ClassState.FilesAdded;

    }

    /// <summary>
    /// Add the video portion of a file to the timeline.
    /// </summary>
    /// <remarks>
    /// If a file contains both audio and video, this method will only add the video portion.
    /// </remarks>
    /// <para>Video files can be of different 
    /// lengths, frame sizes, color depths, aspect ratios, frame rates, etc.  DES 
    /// converts them all to the values specified in the constructor.</para>
    /// 
    /// <param name="sFileName">Video file path+name</param>
    /// <param name="lStart">Start position within the file specified in 100ns units</param>
    /// <param name="lStop">End position within the file specified in 100ns units 
    /// or -1 to add entire file</param>
    public void AddVideoFile(
        string sFileName,
        long lStart,
        long lStop)
    {
      if (m_State > ClassState.FilesAdded)
      {
        throw new Exception("Can't add files since rendering method already selected");
      }
      if (!supportVideo)
      {
        throw new NotSupportedException("Can´t add files because this player is initialized without video support.");
      }

      m_Video.Add(sFileName, lStart, lStop);

      // At least one file has now been added
      m_State = ClassState.FilesAdded;

    }

    /// <summary>
    /// Add the audio portion of a file to the timeline.
    /// </summary>
    /// <remarks>
    /// If a file contains both audio and video, this method will only add the audio portion.
    /// </remarks>
    /// <param name="sFileName">Audio file path+name</param>
    /// <param name="lStart">Start position within the file specified in 100ns units</param>
    /// <param name="lStop">End position within the file specified in 100ns units 
    /// or -1 to go to the end of the file</param>
    public void AddAudioFile(
        string sFileName,
        long lStart,
        long lStop)
    {
      if (m_State > ClassState.FilesAdded)
      {
        throw new Exception("Can't add files since rendering method already selected");
      }
      if (!supportAudio)
      {
        throw new NotSupportedException("Can´t add files because this player is initialized without audio support.");
      }

      m_Audio.Add(sFileName, lStart, lStop);

      // At least one file has now been added
      m_State = ClassState.FilesAdded;
    }

    /// <summary>
    /// 
    /// </summary>
    public void RemoveAudioFiles()
    {
      if (m_Audio != null && m_Audio.Count > 0)
      {
        m_Audio.RemoveTracks();
      }
      m_State = ClassState.Constructed;
    }

    /// <summary>
    /// Add the audio portion of a file to the timeline.
    /// </summary>
    /// <remarks>
    /// If a file contains both audio and video, this method will only add the audio portion.
    /// </remarks>
    /// <param name="sFileName">Audio file path+name</param>
    /// <param name="lStart">Start position within the file specified in 100ns units</param>
    public void AddAudioFile(
        string sFileName,
        long lStart)
    {
      if (m_State > ClassState.FilesAdded)
      {
        throw new Exception("Can't add files since rendering method already selected");
      }
      if (!supportAudio)
      {
        throw new NotSupportedException("Can´t add files because this player is initialized without audio support.");
      }
      m_Audio.Add(sFileName, lStart);

      // At least one file has now been added
      m_State = ClassState.FilesAdded;

    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="pAudioCallback"></param>
    public void RenderAudioToOutput(IDESCombineCB pAudioCallback)
    {
      RenderToWindow(IntPtr.Zero, null, pAudioCallback, false, true);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="pAudioCallback"></param>
    public void RenderAudioToFile(IDESCombineCB pAudioCallback)
    {

    }

    /// <summary>
    /// Configure the graph to output the results to a video window.
    /// </summary>
    /// <remarks>
    /// The callback routines are invoked once for each sample.  This allows for additional processing to
    /// be performed on the video or audio buffers.
    /// </remarks>
    /// <param name="hWnd">Window handle to render to, or IntPtr.Zero to render to its own window</param>
    /// <param name="pVideoCallback">Callback routine to be called for each video frame or null for no callback</param>
    /// <param name="pAudioCallback">Callback routine to be called for each audio frame or null for no callback</param>
    /// <param name="video">Render only video</param>
    /// <param name="audio">Render only audio</param>
    private void RenderToWindow(IntPtr hWnd, IDESCombineCB pVideoCallback, IDESCombineCB pAudioCallback, bool video, bool audio)
    {
      int hr;
      IPin pPin;
      IVideoWindow pVidWindow;
      IAMTimelineObj pGroup;

      // Perform initialization common to all render routines
      RenderCommon();

      // Contains useful routines for creating the graph
      ICaptureGraphBuilder2 icgb = (ICaptureGraphBuilder2)new CaptureGraphBuilder2();

      try
      {
        hr = icgb.SetFiltergraph(m_pGraph);
        DESError.ThrowExceptionForHR(hr);

        int NumGroups;
        hr = m_pTimeline.GetGroupCount(out NumGroups);
        DESError.ThrowExceptionForHR(hr);

        // Walk the groups.  For DESCombine, there is one group that 
        // contains all the video, and a second group for the audio.
        for (int i = 0; i < NumGroups; i++)
        {
          hr = m_pTimeline.GetGroup(out pGroup, i);
          DESError.ThrowExceptionForHR(hr);

          try
          {
            // Inform the graph we will be previewing (rather than writing to disk)
            IAMTimelineGroup pTLGroup = (IAMTimelineGroup)pGroup;
            hr = pTLGroup.SetPreviewMode(true);
            DESError.ThrowExceptionForHR(hr);
          }
          finally
          {
            // Release the group
            Marshal.ReleaseComObject(pGroup);
          }

          // Get the IPin for the current group
          hr = m_pRenderEngine.GetGroupOutputPin(i, out pPin);
          DESError.ThrowExceptionForHR(hr);

          try
          {
            // If this is the video pin
            if (video && IsVideo(pPin))
            {
              // Get a video renderer
              IBaseFilter ibfVideoRenderer = (IBaseFilter)new VideoRenderer();

              try
              {
                // Create a sample grabber, add it to the graph and connect it all up
                AVCallback mcb = new AVCallback(m_Video, pVideoCallback, (IMediaEventSink)m_pGraph, EC_VideoFileComplete);
                RenderWindowHelper(icgb, mcb, "Video", pPin, ibfVideoRenderer);
              }
              finally
              {
                Marshal.ReleaseComObject(ibfVideoRenderer);
              }
            }
            else if (audio)
            {
              // Get an audio renderer
              IBaseFilter ibfAudioRenderer = (IBaseFilter)new AudioRender();

              try
              {
                // Create a sample grabber, add it to the graph and connect it all up
                AVCallback mcb = new AVCallback(m_Audio, pAudioCallback, (IMediaEventSink)m_pGraph, EC_AudioFileComplete);
                RenderWindowHelper(icgb, mcb, "Audio", pPin, ibfAudioRenderer);
              }
              finally
              {
                Marshal.ReleaseComObject(ibfAudioRenderer);
              }
            }
          }
          finally
          {
            Marshal.ReleaseComObject(pPin);
          }
        }

        if (video)
        {

          // Configure the video window
          pVidWindow = (IVideoWindow)m_pGraph;

          // If a window handle was supplied, use it
          if (hWnd != IntPtr.Zero)
          {
            hr = pVidWindow.put_Owner(hWnd);
            DESError.ThrowExceptionForHR(hr);
          }
          else
          {
            // Use our own window

            hr = pVidWindow.put_Caption("Video Rendering Window");
            DESError.ThrowExceptionForHR(hr);

            // since no user interaction is allowed, remove
            // system menu and maximize/minimize buttons
            WindowStyle lStyle = 0;
            hr = pVidWindow.get_WindowStyle(out lStyle);
            DESError.ThrowExceptionForHR(hr);

            lStyle &= ~(WindowStyle.MinimizeBox | WindowStyle.MaximizeBox | WindowStyle.SysMenu);
            hr = pVidWindow.put_WindowStyle(lStyle);
            DESError.ThrowExceptionForHR(hr);
          }
        }
      }
      finally
      {
        Marshal.ReleaseComObject(icgb);
      }
    }

    /// <summary>
    /// Configure the graph to output the results to an AVI file.
    /// </summary>
    /// <param name="sOutputFile">File name for output (must not be null)</param>
    /// <param name="ibfVideoCompressor">IBaseFilter of a video compressor to use (or null for none).  
    /// Note that <b><i>no</i></b> configuration of this compressor is done by this method.  It merely adds it
    /// to the graph in the appropriate place.  Also, the pointer is not invalidated, so any configuration
    /// of the compressor that needs to be done after being added to the graph can still be done.</param>
    /// <param name="ibfAudioCompressor">IBaseFilter of an audio compressor to use (or null for none).
    /// Note that <b><i>no</i></b> configuration of this compressor is done by this method.  It merely adds it
    /// to the graph in the appropriate place.  Also, the pointer is not invalidated, so any configuration
    /// of the compressor that needs to be done after being added to the graph can still be done.</param>
    /// <param name="pVideoCallback">Callback routine to be called for each video frame or null for no callback</param>
    /// <param name="pAudioCallback">Callback routine to be called for each audio frame or null for no callback</param>
    /// <remarks>
    /// The callback routines are invoked once for each sample.  This allows for additional processing to
    /// be performed on the video or audio buffers.
    /// </remarks>
    public void RenderToAVI(
        string sOutputFile,
        IBaseFilter ibfVideoCompressor,
        IBaseFilter ibfAudioCompressor,
        IDESCombineCB pVideoCallback,
        IDESCombineCB pAudioCallback)
    {
      int hr;
      IPin pPin;

      if (sOutputFile == null)
      {
        throw new Exception("Output file name cannot be null");
      }

      // Perform initialization common to all render routines
      RenderCommon();

      // Contains useful routines for creating the graph
      ICaptureGraphBuilder2 icgb = (ICaptureGraphBuilder2)new CaptureGraphBuilder2();

      try
      {
        hr = icgb.SetFiltergraph(m_pGraph);
        DESError.ThrowExceptionForHR(hr);

        // Create the file writer
        IBaseFilter pMux;
        IFileSinkFilter pFilter;
        hr = icgb.SetOutputFileName(MediaSubType.Avi, sOutputFile, out pMux, out pFilter);
        DESError.ThrowExceptionForHR(hr);

        // We don't need this, so let it go
        Marshal.ReleaseComObject(pFilter);

        try
        {
          int NumGroups;
          hr = m_pTimeline.GetGroupCount(out NumGroups);
          DESError.ThrowExceptionForHR(hr);

          // Walk the groups.  For this class, there is one group that 
          // contains all the video, and a second group for the audio.
          for (int i = 0; i < NumGroups; i++)
          {
            IAMTimelineObj pGroup;

            hr = m_pTimeline.GetGroup(out pGroup, i);
            DESError.ThrowExceptionForHR(hr);

            try
            {
              // Inform the graph we will be writing to disk (rather than previewing)
              IAMTimelineGroup pTLGroup = (IAMTimelineGroup)pGroup;
              hr = pTLGroup.SetPreviewMode(false);
              DESError.ThrowExceptionForHR(hr);
            }
            finally
            {
              Marshal.ReleaseComObject(pGroup);
            }

            // Get the IPin for the current group
            hr = m_pRenderEngine.GetGroupOutputPin(i, out pPin);
            DESError.ThrowExceptionForHR(hr);

            try
            {
              if (IsVideo(pPin))
              {
                // Create a sample grabber, add it to the graph and connect it all up
                AVCallback mcb = new AVCallback(m_Video, pVideoCallback, (IMediaEventSink)m_pGraph, EC_VideoFileComplete);
                RenderHelper(icgb, mcb, "Video", pPin, ibfVideoCompressor, pMux);
              }
              else
              {
                // Create a sample grabber, add it to the graph and connect it all up
                AVCallback mcb = new AVCallback(m_Audio, pAudioCallback, (IMediaEventSink)m_pGraph, EC_AudioFileComplete);
                RenderHelper(icgb, mcb, "Audio", pPin, ibfAudioCompressor, pMux);
              }
            }
            finally
            {
              Marshal.ReleaseComObject(pPin);
            }
          }
        }
        finally
        {
          Marshal.ReleaseComObject(pMux);
        }
      }
      finally
      {
        Marshal.ReleaseComObject(icgb);
      }
    }


    /// <summary>
    /// Begins rendering and returns immediately.
    /// </summary>
    /// <remarks>
    /// Final status is sent as a <see cref="DESCombine.Completed"/> event.
    /// </remarks>
    public void StartRendering()
    {
      int hr;

      if (m_State < ClassState.RenderSelected)
      {
        throw new Exception("Render method not selected");
      }

      m_State = ClassState.GraphStarted;

      m_pControl = (IMediaControl)m_pGraph;

      // Avoid double threads
      if (threadCompleted)
      {
        // Create a new thread to process events
        Thread t;
        t = new Thread(new ThreadStart(EventWait));
        t.Name = "Media Event Thread";
        t.Start();

        threadCompleted = false;
      }

      hr = m_pControl.Run();
      DESError.ThrowExceptionForHR(hr);
    }

    /// <summary>
    /// Pauses / unpauses clip according to state.
    /// </summary>
    public void PauseRendering()
    {
      if (this.m_pControl == null)
        return;

      // Toggle play/pause behavior
      if ((this.m_State == ClassState.Paused) || (this.m_State == ClassState.Stopped))
      {
        if (this.m_pControl.Run() >= 0)
          this.m_State = ClassState.Running;
      }
      else
      {
        if (this.m_pControl.Pause() >= 0)
          this.m_State = ClassState.Paused;
      }
    }

    /// <summary>
    /// This method stops a currently running movie.
    /// </summary>
    public void StopRendering()
    {
      if (this.m_pControl != null)
      {
        // Stop the graph from playing the media file
        int hr = this.m_pControl.Stop();//StopWhenReady();
        DsError.ThrowExceptionForHR(hr);

        this.m_State = ClassState.Stopped;
      }
    }

    /// <summary>
    /// 
    /// </summary>
    public void StopWhenReady()
    {
      if (this.m_pControl != null)
      {
        // Stop the graph from playing the media file
        int hr = this.m_pControl.StopWhenReady();
        DsError.ThrowExceptionForHR(hr);

        this.m_State = ClassState.Stopped;
      }
    }

    /// <summary>
    /// This method seeks the movie to the given time position.
    /// The time has to be in units of the <see cref="IReferenceClock"/>.
    /// By default that are 100-nanosecond units.
    /// </summary>
    /// <param name="timeToStart">An <see cref="Int64"/> with the absolute start time to seek, 
    /// or null if the start time should not be set</param>
    public void Seek(Int64? timeToStart)
    {
      try
      {
        if (this.m_pGraph != null)
        {
          int hr = 0;
          if (timeToStart.HasValue)
          {
            //this.m_pControl.Pause();
            //FilterState state;
            //this.m_pControl.GetState(50, out state);
            var mediaSeeking = this.m_pGraph as IMediaSeeking;

            if (mediaSeeking == null)
            {
              return;
            }

            long currentPosition;

            hr = mediaSeeking.GetCurrentPosition(out currentPosition);
            DsError.ThrowExceptionForHR(hr);

            var newPosition = timeToStart.Value * 10000;
            if (currentPosition != newPosition)
            {
              hr = mediaSeeking.SetPositions(
                newPosition,
                AMSeekingSeekingFlags.AbsolutePositioning,
                null,
                AMSeekingSeekingFlags.NoPositioning);
              DsError.ThrowExceptionForHR(hr);
            }

            //m_pControl->Run();
            //m_pControl->GetState(1000, NULL);

            //m_pControl->Stop();
            //m_pControl->GetState(1000, NULL);

          }
        }
      }
      catch (Exception ex)
      {
        MessageBox.Show("Could not seek movie because: " + ex.Message);
      }
    }

    /// <summary>
    /// Returns the length of the timeline in <see cref="DESCombine.UNITS"/>.
    /// </summary>
    /// <remarks>
    /// This will return the longer of the video time line or the audio time line.
    /// </remarks>
    public long MediaLength
    {
      get
      {
        long len = 0;
        if (m_pTimeline != null)
        {
          int hr = m_pTimeline.GetDuration(out len);
          DESError.ThrowExceptionForHR(hr);
        }

        return len;
      }
    }

    /// <summary>
    /// Returns the length of the video group in <see cref="DESCombine.UNITS"/>.
    /// </summary>
    /// <remarks>
    /// This will return the length of the video group.
    /// </remarks>
    public long VideoLength
    {
      get
      {
        return m_Video.Length;
      }
    }

    /// <summary>
    /// Returns the length of the audio group in <see cref="DESCombine.UNITS"/>.
    /// </summary>
    /// <remarks>
    /// This will return the length of the audio group.
    /// </remarks>
    public long AudioLength
    {
      get
      {
        return m_Audio.Length;
      }
    }

    /// <summary>
    /// Sets the current playback rate of the timelime.
    /// </summary>
    /// <value>A <see cref="double"/> with the playback rate. Must not be zero.</value>
    public double PlaybackRate
    {
      set
      {
        // If the IMediaSeeking interface exists, use it to set rate
        if (this.m_pGraph != null && value != 0)
        {
          int hr = ((IMediaSeeking)this.m_pGraph).SetRate(value);
          DsError.ThrowExceptionForHR(hr);
        }
      }
    }

    /// <summary>
    /// Used to signal that the graph should be cancelled.
    /// </summary>
    /// <remarks>
    /// Only has meaning if called after <see cref="DESCombine.StartRendering"/>.
    /// </remarks>
    public void Cancel()
    {
      if (m_State < ClassState.GraphStarted)
      {
        return;
        //throw new Exception("Graph not yet started");
      }

#if DEBUG
      if (m_rot != null)
      {
        m_rot.Dispose();
        m_rot = null;
      }
#endif
      if (m_State < ClassState.GraphCompleting)
      {
        ChangeState(ClassState.Cancelling);
      }
    }

    /// <summary>
    /// Returns an XML description of the capture graph (as seen by DES).
    /// </summary>
    /// <remarks>
    /// This method can only be called after one
    /// of the render functions has been called.  Might be useful for debugging.  It is also possible (even easy) 
    /// to build the DES part of a graph from an XML file, however that functionality has not been implemented
    /// in this version of the library.
    /// </remarks>
    /// <returns>string containing XML</returns>
    public string GetXML()
    {
      if (m_State < ClassState.RenderSelected)
      {
        throw new Exception("No render method has been selected");
      }

      IXml2Dex pXML;
      string sRet;
      int hr;

      pXML = (IXml2Dex)new Xml2Dex();

      try
      {
        hr = pXML.WriteXML(m_pTimeline, out sRet);
        DESError.ThrowExceptionForHR(hr);
      }
      finally
      {
        Marshal.ReleaseComObject(pXML);
      }

      return sRet;
    }

    /// <summary>
    /// Called when the graph has finished running.
    /// </summary>
    /// <remarks>
    /// The <see cref="DESCompletedArgs"/>
    /// contains the result of running the graph (Completed, UserAborted,
    /// out of disk space, etc.)
    /// This code will be a member of DirectShowLib.EventCode.  Typically it 
    /// will be EventCode.Complete, EventCode.ErrorAbort or EventCode.UserAbort.
    /// </remarks>
    public event EventHandler Completed = null;

    /// <summary>
    /// Called when the graph has finished running.
    /// </summary>
    /// <remarks>
    /// The <see cref="DESCompletedArgs"/>
    /// contains the result of running the graph (Completed, UserAborted,
    /// out of disk space, etc.)
    /// This code will be a member of DirectShowLib.EventCode.  Typically it 
    /// will be EventCode.Complete, EventCode.ErrorAbort or EventCode.UserAbort.
    /// </remarks>
    private event EventHandler ThreadFinished = null;

    /// <summary>
    /// Called when a file has finished processing.
    /// </summary>
    /// <remarks>
    /// The <see cref="FileCompletedArgs"/> contains the file name and type.
    /// Note that if RenderToWindow() is chosen, dropped frames can result in missed notifications.  This
    /// should not affect other RenderTo* methods.
    /// </remarks>
    public event EventHandler FileCompleted = null;

    /// <summary>
    /// Helps deal with potential conflicts between the eventing thread
    /// and client calls to Cancel()
    /// </summary>
    /// <param name="newState">State to change to</param>
    private void ChangeState(ClassState newState)
    {
      lock (this)
      {
        //if (m_State == ClassState.GraphStarted)
        {
          m_State = newState;
        }
      }
    }

    /// <summary>
    /// Called on a new thread to process events from the graph.  The thread
    /// exits when the graph finishes.  Cancelling is done here.
    /// </summary>
    private void EventWait()
    {
      // Returned when GetEvent is called but there are no events
      const int E_ABORT = unchecked((int)0x80004004);

      int hr;
      IntPtr p1, p2;
      EventCode ec;
      EventCode exitCode = 0;

      IMediaEvent pEvent = (IMediaEvent)m_pGraph;

      do
      {
        // Read the event
        for (
            hr = pEvent.GetEvent(out ec, out p1, out p2, 100);
            hr >= 0 && m_State < ClassState.GraphCompleted;
            hr = pEvent.GetEvent(out ec, out p1, out p2, 100)
            )
        {
          switch (ec)
          {
            case EventCode.UserAbort:
              ChangeState(ClassState.Cancelling);
              exitCode = ec;
              // Release any resources the message allocated
              hr = pEvent.FreeEventParams(ec, p1, p2);
              DESError.ThrowExceptionForHR(hr);

              break;
            // If the clip is finished playing
            case EventCode.Complete:
            case EventCode.ErrorAbort:
              ChangeState(ClassState.GraphCompleting);
              exitCode = ec;

              // Release any resources the message allocated
              hr = pEvent.FreeEventParams(ec, p1, p2);
              DESError.ThrowExceptionForHR(hr);
              break;

            // Walked past the end of a video file, send an event
            case EC_VideoFileComplete:
              if (FileCompleted != null)
              {
                MediaFile mf = m_Video.File(p1.ToInt32());
                FileCompletedArgs ca = new FileCompletedArgs(mf.FileName, FileCompletedArgs.FileType.Video);
                FileCompleted(this, ca);
              }
              break;

            // Walked past the end of a video file, send an event
            case EC_AudioFileComplete:
              if (FileCompleted != null)
              {
                MediaFile mf = m_Audio.File(p1.ToInt32());
                FileCompletedArgs ca = new FileCompletedArgs(mf.FileName, FileCompletedArgs.FileType.Audio);
                FileCompleted(this, ca);
              }
              break;

            default:
              // Release any resources the message allocated
              hr = pEvent.FreeEventParams(ec, p1, p2);
              DESError.ThrowExceptionForHR(hr);
              break;
          }
        }

        // If the error that exited the loop wasn't due to running out of events
        if (hr != E_ABORT)
        {
          DESError.ThrowExceptionForHR(hr);
        }
        //FilterState fs;
        //m_pControl.GetState(200, out fs);
        ////DESError.ThrowExceptionForHR(hr);

      } while (m_State < ClassState.GraphCompleting);

      // If the user cancelled
      if (m_State == ClassState.Cancelling && m_pControl != null)
      {
        // Stop the graph, send an appropriate exit code
        hr = m_pControl.Stop();
        exitCode = EventCode.UserAbort;
      }

      if (m_State == ClassState.GraphCompleting)
      {
        m_State = ClassState.GraphCompleted;
      }
      else
      {
        m_State = ClassState.Cancelled;
      }

      threadCompleted = true;

      // Send an event saying we are complete
      if (this.ThreadFinished != null)
      {
        DESCompletedArgs ca = new DESCompletedArgs(exitCode);
        this.ThreadFinished(this, ca);
      }
    } // Exit the thread

    /// <summary>
    /// Create a video media type from a few parameters
    /// </summary>
    /// <param name="BitCount">Bits per pixel (16, 24, 32)</param>
    /// <param name="Width">Video width</param>
    /// <param name="Height">Video height</param>
    /// <returns>The constructed AMMediaType</returns>
    private AMMediaType GetVideoMediaType(short BitCount, int Width, int Height)
    {
      Guid mediaSubType;
      AMMediaType VideoGroupType = new AMMediaType();

      // Calculate the SubType from the Bit count
      switch (BitCount)
      {
        case 16:
          mediaSubType = MediaSubType.RGB555;
          break;
        case 24:
          mediaSubType = MediaSubType.RGB24;
          break;
        case 32:
          mediaSubType = MediaSubType.RGB32;
          break;
        default:
          throw new Exception("Unrecognized bit format");
      }

      VideoGroupType.majorType = MediaType.Video;
      VideoGroupType.subType = mediaSubType;
      VideoGroupType.formatType = FormatType.VideoInfo;
      VideoGroupType.fixedSizeSamples = true;

      VideoGroupType.formatSize = Marshal.SizeOf(typeof(VideoInfoHeader));
      VideoInfoHeader vif = new VideoInfoHeader();
      vif.BmiHeader = new BitmapInfoHeader();

      // The HEADER macro returns the BITMAPINFO within the VIDEOINFOHEADER
      vif.BmiHeader.Size = Marshal.SizeOf(typeof(BitmapInfoHeader));
      vif.BmiHeader.Compression = 0;
      vif.BmiHeader.BitCount = BitCount;
      vif.BmiHeader.Width = Width;
      vif.BmiHeader.Height = Height;
      vif.BmiHeader.Planes = 1;

      int iSampleSize = vif.BmiHeader.Width * vif.BmiHeader.Height * (vif.BmiHeader.BitCount / 8);
      vif.BmiHeader.ImageSize = iSampleSize;
      VideoGroupType.sampleSize = iSampleSize;
      VideoGroupType.formatPtr = Marshal.AllocCoTaskMem(Marshal.SizeOf(vif));

      Marshal.StructureToPtr(vif, VideoGroupType.formatPtr, false);

      return VideoGroupType;
    }

    /// <summary>
    /// Create an audio media type
    /// </summary>
    /// <returns>The constructed media type</returns>
    private AMMediaType GetAudioMediaType()
    {
      AMMediaType AudioGroupType = new AMMediaType();
      AudioGroupType.majorType = MediaType.Audio;

      return AudioGroupType;
    }

    /// <summary>
    /// Called from RenderTo* routines to perform common initialization
    /// </summary>
    private void RenderCommon()
    {
      int hr;

      if (m_State >= ClassState.RenderSelected)
      {
        throw new Exception("Graph rendering has already been selected");
      }

      if (m_State < ClassState.FilesAdded)
      {
        throw new Exception("No files added to render.");
      }

      m_State = ClassState.RenderSelected;

      // create the render engine
      m_pRenderEngine = (IRenderEngine)new RenderEngine();

      // tell the render engine about the timeline it should use
      hr = m_pRenderEngine.SetTimelineObject(m_pTimeline);
      DESError.ThrowExceptionForHR(hr);

      // connect up the front end
      hr = m_pRenderEngine.ConnectFrontEnd();
      DESError.ThrowExceptionForHR(hr);

      // Get the filtergraph - used all over the place
      hr = m_pRenderEngine.GetFilterGraph(out m_pGraph);
      DESError.ThrowExceptionForHR(hr);

#if DEBUG
      // Allow the graph to be connected to from GraphEdit
      m_rot = new DsROTEntry(m_pGraph);
#endif
    }

    /// <summary>
    /// Determine whether a specified pin is audio or video
    /// </summary>
    /// <param name="pPin">Pin to check</param>
    /// <returns>True if pin is video</returns>
    private bool IsVideo(IPin pPin)
    {
      int hr;
      bool bRet = false;
      AMMediaType[] pmt = new AMMediaType[1];
      IEnumMediaTypes ppEnum;

      // Walk the MediaTypes for the pin
      hr = pPin.EnumMediaTypes(out ppEnum);
      DESError.ThrowExceptionForHR(hr);

      try
      {
        // Just read the first one
        hr = ppEnum.Next(1, pmt, IntPtr.Zero);
        DESError.ThrowExceptionForHR(hr);

        bRet = pmt[0].majorType == MediaType.Video;
      }
      finally
      {
        Marshal.ReleaseComObject(ppEnum);
      }
      DsUtils.FreeAMMediaType(pmt[0]);

      return bRet;
    }

    /// <summary>
    /// Called from RenderWindow to add the renderer to the graph, create a sample grabber, add it 
    /// to the graph and connect it all up
    /// </summary>
    /// <param name="icgb">ICaptureGraphBuilder2 to use</param>
    /// <param name="pCallback">ICaptureGraphBuilder2 to use</param>
    /// <param name="sType">string to use in creating filter graph object descriptions</param>
    /// <param name="pPin">Pin to connect from</param>
    /// <param name="ibfRenderer">Renderer to add</param>
    private void RenderWindowHelper(ICaptureGraphBuilder2 icgb, AVCallback pCallback, string sType, IPin pPin, IBaseFilter ibfRenderer)
    {
      int hr;

      // Add the renderer to the graph
      hr = m_pGraph.AddFilter(ibfRenderer, sType + " Renderer");
      DESError.ThrowExceptionForHR(hr);

      // Do everything else
      RenderHelper(icgb, pCallback, sType, pPin, null, ibfRenderer);
    }

    /// <summary>
    /// Common routine used by RenderTo*  
    /// </summary>
    /// <param name="icgb">ICaptureGraphBuilder2 to use</param>
    /// <param name="pCallback">Callback to use (or null)</param>
    /// <param name="sType">string to use in creating filter graph object descriptions</param>
    /// <param name="pPin">Pin to connect from</param>
    /// <param name="ibfCompressor">Compressor to use, or null for none</param>
    /// <param name="pOutput">Endpoint (renderer or file writer) to connect to</param>
    private void RenderHelper(ICaptureGraphBuilder2 icgb, AVCallback pCallback, string sType, IPin pPin, IBaseFilter ibfCompressor, IBaseFilter pOutput)
    {
      int hr;
      IBaseFilter ibfSampleGrabber = null;

      try
      {
        // If no callback was provided, don't create a samplegrabber
        if (pCallback != null)
        {
          ISampleGrabber isg = (ISampleGrabber)new SampleGrabber();
          ibfSampleGrabber = (IBaseFilter)isg;

          hr = isg.SetCallback(pCallback, 1);
          DESError.ThrowExceptionForHR(hr);

          hr = m_pGraph.AddFilter(ibfSampleGrabber, sType + " sample grabber");
          DESError.ThrowExceptionForHR(hr);
        }

        // If a compressor was provided, add it to the graph and connect it up
        if (ibfCompressor != null)
        {
          // Connect the pin.
          hr = m_pGraph.AddFilter(ibfCompressor, sType + " Compressor");
          DESError.ThrowExceptionForHR(hr);

          hr = icgb.RenderStream(null, null, pPin, ibfSampleGrabber, ibfCompressor);
          DESError.ThrowExceptionForHR(hr);

          // Connect the pin.
          hr = icgb.RenderStream(null, null, ibfCompressor, null, pOutput);
          DESError.ThrowExceptionForHR(hr);
        }
        else
        {
          // Just connect the SampleGrabber (if any)
          hr = icgb.RenderStream(null, null, pPin, ibfSampleGrabber, pOutput);
          DESError.ThrowExceptionForHR(hr);
        }
      }
      finally
      {
        if (ibfSampleGrabber != null)
        {
          Marshal.ReleaseComObject(ibfSampleGrabber);
        }
      }
    }

    /// <summary>
    /// Release resources used by the class.
    /// </summary>
    /// <remarks>May fire events, so do not call from Form.Dispose().</remarks>
    public void Dispose()
    {
      GC.SuppressFinalize(this);

      this.ThreadFinished -= new EventHandler(DESCombine_ThreadFinished);

      if (m_Video != null)
      {
        m_Video.Dispose();
        m_Video = null;
      }
      if (m_Audio != null)
      {
        m_Audio.Dispose();
        m_Audio = null;
      }

      if (m_pTimeline != null)
      {
        Marshal.ReleaseComObject(m_pTimeline);
        m_pTimeline = null;
      }

      if (m_pRenderEngine != null)
      {
        Marshal.ReleaseComObject(m_pRenderEngine);
        m_pRenderEngine = null;
      }

#if DEBUG
      if (m_rot != null)
      {
        m_rot.Dispose();
        m_rot = null;
      }
#endif
      if (m_pControl != null)
      {
        m_pControl.Stop();
        m_pControl = null;
      }

      if (m_pGraph != null)
      {
        // End event thread.
        int hr = ((IMediaEventSink)this.m_pGraph).Notify(EventCode.UserAbort, IntPtr.Zero, IntPtr.Zero);
        DsError.ThrowExceptionForHR(hr);

        while (!threadCompleted)
        {
          Thread.Sleep(2);
        }

        Marshal.ReleaseComObject(m_pGraph);
        m_pGraph = null;
      }

      // No point in calling us from the Finalizer, we're already gone
      GC.SuppressFinalize(this);
    }

  }
}