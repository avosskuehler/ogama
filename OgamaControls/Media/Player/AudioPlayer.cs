using System;
using System.Windows.Forms;
using DirectShowLib;
using System.Runtime.InteropServices;

namespace OgamaControls
{
  /// <summary>
  /// This class encapsulates an DirectShow interface
  /// to play back audio files.
  /// </summary>
  public partial class AudioPlayer
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

    private const int VolumeFull = 0;
    private const int VolumeSilence = -10000;

    private IGraphBuilder filterGraph = null;
    private IMediaControl mediaControl = null;
    private IBasicAudio basicAudio = null;
    private IMediaSeeking mediaSeeking = null;

    private string filename = string.Empty;
    private int currentVolume = VolumeFull;
    private PlayState currentState = PlayState.Stopped;
    private double currentPlaybackRate = 1.0;

    //private bool threadCompleted;

    /// <summary>
    /// Called when the graph has finished running.
    /// </summary>
    /// <remarks>
    /// The <see cref="CompletedArgs"/>
    /// contains the result of running the graph (Completed, UserAborted,
    /// out of disk space, etc.)
    /// This code will be a member of DirectShowLib.EventCode.  Typically it 
    /// will be EventCode.Complete, EventCode.ErrorAbort or EventCode.UserAbort.
    /// </remarks>
    public event EventHandler Completed = null;

#if DEBUG
    private DsROTEntry rot = null;
#endif

    #endregion //FIELDS

    ///////////////////////////////////////////////////////////////////////////////
    // Construction and Initializing methods                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region CONSTRUCTION

    /// <summary>
    /// Initializes a new instance of the AudioPlayer class.
    /// </summary>
    public AudioPlayer()
    {
      //threadCompleted = true;
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
    public string AudioFile
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
        }
      }
    }

    /// <summary>
    /// Gets the duration in milliseconds of the current loaded audiofile.
    /// </summary>
    /// <value>An <see cref="Int64"/> with the files duration in milliseconds.</value>
    public long Duration
    {
      get
      {
        long duration = -1;
        if (this.mediaSeeking != null)
        {
          int hr = this.mediaSeeking.GetDuration(out duration);
          DsError.ThrowExceptionForHR(hr);
        }
        duration = (long)(duration / 10000f);
        return duration;
      }
    }

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
    public void LoadAudioFile(string filename)
    {
      this.CloseClip();
      this.OpenClip(filename);
      if (this.mediaControl != null)
      {
        this.mediaControl.Stop();
        this.currentState = PlayState.Stopped;
      }
    }

    /// <summary>
    /// This method adds a new file to the output channel that is played
    /// in parallel to existing files in the direct show graph.
    /// </summary>
    /// <param name="filename">A <see cref="string"/> with the full filename of
    /// the audio file to play.</param>
    public void AddAudioChannel(string filename)
    {
      if (this.filterGraph == null)
      {
        LoadAudioFile(filename);
      }
      else
      {
        if (this.mediaControl != null)
        {
          int hr = 0;
          if (this.PlayState == PlayState.Running)
          {
            this.mediaControl.Pause();
          }
          hr = this.filterGraph.RenderFile(filename, null);
          if (this.PlayState == PlayState.Running)
          {
            this.mediaControl.Run();
          }
          DsError.ThrowExceptionForHR(hr);
        }
      }
    }

    /// <summary>
    /// This method closes a currently loaded movie.
    /// </summary>
    public void CloseAudioFile()
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
    public void Seek(Int64? timeToStart, Int64? timeToStop)
    {
      try
      {
        if (this.mediaSeeking != null)
        {
          int hr = 0;
          if (timeToStart.HasValue)
          {
            hr = this.mediaSeeking.SetPositions(timeToStart.Value*10000, AMSeekingSeekingFlags.AbsolutePositioning,
              null, AMSeekingSeekingFlags.NoPositioning);
            DsError.ThrowExceptionForHR(hr);
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
    /// If no movie is loaded before vie <see cref="LoadAudioFile(string)"/>
    /// this method does nothing.
    /// </summary>
    public void Play()
    {
      if (this.mediaControl != null)
      {
        this.currentState = PlayState.Running;

        //// Get the event handle the graph will use to signal
        //// when events occur
        //hr = mediaEvent.GetEventHandle(out hEvent);
        //DsError.ThrowExceptionForHR(hr);

        //// Wrap the graph event with a ManualResetEvent
        //manualResetEvent = new ManualResetEvent(false);
        //manualResetEvent.SafeWaitHandle = new Microsoft.Win32.SafeHandles.SafeWaitHandle(hEvent, true);

      // Avoid double threads
        //if (threadCompleted)
        //{
        //  // Create a new thread to wait for events
        //  Thread eventThread = new Thread(new ThreadStart(this.EventWait));
        //  eventThread.Name = "Media Event Thread";
        //  eventThread.Start();

        //  threadCompleted = false;
        //}
        ////BackgroundWorker eventListener = new BackgroundWorker();
        ////eventListener.WorkerSupportsCancellation = true;
        ////eventListener.RunWorkerCompleted += new RunWorkerCompletedEventHandler(eventListener_RunWorkerCompleted);
        ////eventListener.DoWork += new DoWorkEventHandler(EventWait);
        ////eventListener.RunWorkerAsync();
        //this.Completed += new EventHandler(AudioPlayer_Completed);

        // Run the graph to play the media file
        int hr = this.mediaControl.Run();
        DsError.ThrowExceptionForHR(hr);

      }
    }

    /// <summary>
    /// This method stops a currently running movie.
    /// </summary>
    public void Stop()
    {
      if (this.mediaControl != null)
      {
        // Run the graph to play the media file
        int hr = this.mediaControl.StopWhenReady();
        DsError.ThrowExceptionForHR(hr);

        //// End event thread.
        //hr = ((IMediaEventSink)this.filterGraph).Notify(EventCode.UserAbort, IntPtr.Zero, IntPtr.Zero);
        //DsError.ThrowExceptionForHR(hr);

        //this.Completed -= new EventHandler(AudioPlayer_Completed);

        ChangeState(PlayState.Stopped);
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
    #region PRIVATEMETHODS

    private void OpenClip(string filename)
    {
      try
      {
        this.filename = filename;

        // If no filename is specified, show file open dialog
        if (this.filename == string.Empty)
        {
          this.filename = GetClipFileName();
          if (this.filename == string.Empty)
            return;
        }

        if (!System.IO.File.Exists(this.filename))
        {
          return;
        }

        // Reset status variables
        this.currentState = PlayState.Stopped;
        this.currentVolume = VolumeFull;

        // Start playing the media file
        InitGraph(this.filename);
      }
      catch
      {
        CloseClip();
      }
    }
    
    private void InitGraph(string filename)
    {
      int hr = 0;
      //IntPtr hEvent;

      if (filename == string.Empty)
        return;

      this.filterGraph = new FilterGraph() as IFilterGraph2;

#if DEBUG
      //rot = new DsROTEntry(this.filterGraph);
#endif

      // Have the graph builder construct its the appropriate graph automatically
      hr = this.filterGraph.RenderFile(filename, null);
      DsError.ThrowExceptionForHR(hr);
      
      // QueryInterface for DirectShow interfaces
      this.mediaControl = this.filterGraph as IMediaControl;
      this.mediaSeeking = this.filterGraph as IMediaSeeking;
      //this.mediaEvent = this.graphBuilder as IMediaEvent;

      // Query for audio interfaces, which may not be relevant for video-only files
      this.basicAudio = this.filterGraph as IBasicAudio;

      // Complete window initialization
      this.currentPlaybackRate = 1.0;
    }

    void AudioPlayer_Completed(object sender, EventArgs e)
    {
      MethodInvoker closingDelegate =
        new MethodInvoker(this.CloseInterfaces);
      closingDelegate.BeginInvoke(null, null);
      this.Completed -= new EventHandler(AudioPlayer_Completed);
    }

    //void eventListener_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
    //{
    //  this.CloseAudioFile();
    //}

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

      IMediaEvent pEvent = (IMediaEvent)this.filterGraph;

      do
      {
        // Read the event
        for (
            hr = pEvent.GetEvent(out ec, out p1, out p2, 100);
            hr >= 0 && this.currentState == PlayState.Running;
            hr = pEvent.GetEvent(out ec, out p1, out p2, 100)
            )
        {
          switch (ec)
          {
            // If all files finished playing
            case EventCode.Complete:
            case EventCode.ErrorAbort:
            case EventCode.UserAbort:
              ChangeState(PlayState.Exiting);
              exitCode = ec;
              break;
          }

          // Release any resources the message allocated
          hr = pEvent.FreeEventParams(ec, p1, p2);
          DsError.ThrowExceptionForHR(hr);

        }

        // If the error that exited the loop wasn't due to running out of events
        if (hr != E_ABORT)
        {
          DsError.ThrowExceptionForHR(hr);
        }
      } while (this.currentState == PlayState.Running);

      // If the user cancelled
      if (this.currentState == PlayState.Cancelling)
      {
        // Stop the graph, send an appropriate exit code
        hr = this.mediaControl.Stop();
        exitCode = EventCode.UserAbort;
      }


      // Send an event saying we are complete
      if (exitCode == EventCode.Complete && Completed != null)
      {
        CompletedArgs ca = new CompletedArgs(exitCode);
        Completed(this, ca);
      }
      //threadCompleted = true;
    } // Exit the thread


    /// <summary>Reports the event code that exited the graph.
    /// </summary>
    /// <remarks>Signals that all files have been rendered</remarks>
    public class CompletedArgs : System.EventArgs
    {
      /// <summary>The result of the rendering</summary>
      /// <remarks>
      /// This code will be a member of DirectShowLib.EventCode.  Typically it 
      /// will be EventCode.Complete, EventCode.ErrorAbort or EventCode.UserAbort.
      /// </remarks>
      public EventCode Result;

      /// <summary>
      /// Used to construct an instace of the class.
      /// </summary>
      /// <param name="ec"></param>
      internal CompletedArgs(EventCode ec)
      {
        Result = ec;
      }
    }

    /// <summary>
    /// Helps deal with potential conflicts between the eventing thread
    /// and client calls to Cancel()
    /// </summary>
    /// <param name="newState">State to change to</param>
    private void ChangeState(PlayState newState)
    {
      lock (this)
      {
        //if (m_State == PlayState..GraphStarted)
        //{
          this.currentState = newState;
        //}
      }
    }

    ///// <summary>
    ///// Wait for events to happen.  This approach uses waiting on an event handle.
    ///// The nice thing about doing it this way is that you aren't in the windows message
    ///// loop, and don't have to worry about re-entrency or taking too long.  Plus, being
    ///// in a class as we are, we don't have access to the message loop.
    ///// Alternately, you can receive your events as windows messages.  See
    ///// IMediaEventEx.SetNotifyWindow.
    ///// </summary>
    ///// <remarks>We catch the EC_COMPLETE event in its default implementation,
    ///// that means it is only called when all running streams are finished. 
    ///// MSDN says:
    ///// 
    ///// EC_COMPLETE
    ///// All data from a particular stream has been rendered.
    ///// Parameters
    ///// lParam1 
    ///// (HRESULT) Status of the stream on completion. If no errors occurred during playback, the value is S_OK. 
    ///// lParam2 
    ///// (IUnknown*) Zero, or a pointer to the renderer's IBaseFilter interface. 
    ///// Default Action
    ///// By default, the filter graph manager does not forward this event to the application. However, after all the streams in the graph report EC_COMPLETE, the filter graph manager posts a separate EC_COMPLETE event to the application.
    ///// If the default action is disabled for this event, the application receives all of the EC_COMPLETE events from the renderers.
    ///// Remarks
    ///// A renderer filter sends this event when it receives an end-of-stream notice. (End-of-stream is signaled through the IPin::EndOfStream method.) The filter sends exactly one EC_COMPLETE event for each stream. The filter must process any pending samples before it sends the event. Stopping a renderer resets any end-of-stream state that was cached.
    ///// Filters set the lParam2 parameter to an IBaseFilter pointer. If the default action is enabled, the filter graph manager sets this parameter to zero.
    ///// </remarks>
    //private void EventWait()
    //{
    //  // Returned when GetEvent is called but there are no events
    //  const int E_ABORT = unchecked((int)0x80004004);

    //  int hr;
    //  IntPtr p1, p2;
    //  EventCode ec;

    //  do
    //  {
    //    // Wait for an event
    //    manualResetEvent.WaitOne(10, false);

    //    // Avoid contention for m_State
    //    //lock (this)
    //    {
    //      // If we are not shutting down
    //      if (this.currentState != PlayState.Exiting&&this.mediaEvent!=null)
    //      {
    //        // Read the event
    //        for (
    //            hr = this.mediaEvent.GetEvent(out ec, out p1, out p2, 0);
    //            hr >= 0;
    //            hr = this.mediaEvent.GetEvent(out ec, out p1, out p2, 0)
    //            )
    //        {
    //          // Write the event name to the debug window
    //          //Console.WriteLine(ec.ToString());

    //          // Release any resources the message allocated
    //          hr = this.mediaEvent.FreeEventParams(ec, p1, p2);
    //          DsError.ThrowExceptionForHR(hr);

    //          // If the clip is finished playing
    //          if (ec == EventCode.Complete)
    //          {
    //            // Call Stop() to set state
    //            Stop();

    //            // Throw event
    //            if (StopPlay != null)
    //            {
    //              StopPlay(this);
    //            }
    //            //this.currentState = PlayState.Exiting;
    //            break;
    //          }
    //        }

    //        // If the error that exited the loop wasn't due to running out of events
    //        if (hr != E_ABORT)
    //        {
    //          DsError.ThrowExceptionForHR(hr);
    //        }
    //      }
    //      else
    //      {
    //        // We are shutting down
    //        break;
    //      }
    //    }
    //  } while (true);//this.currentState != PlayState.Exiting);
    //}

    private void CloseClip()
    {
      Stop();

      // Free DirectShow interfaces
      CloseInterfaces();

      // Clear file name to allow selection of new file with open dialog
      this.filename = string.Empty;

      // No current media state
      this.currentState = PlayState.None;
    }

    private void CloseInterfaces()
    {
      try
      {
        lock (this)
        {
#if DEBUG
          if (rot != null)
          {
            rot.Dispose();
            rot = null;
          }
#endif

          //ChangeState(PlayState.Exiting);

          //// Release the thread (if the thread was started)
          //if (this.currentState != PlayState.Exiting)
          //{
          //  this.currentState = PlayState.Exiting;
          //  if (this.manualResetEvent != null)
          //  {
          //    manualResetEvent.Set();
          //    manualResetEvent.SafeWaitHandle.Dispose();
          //    manualResetEvent.Close();
          //    manualResetEvent = null;
          //  }
          //}
          //if (eventThread != null)
          //{
          //  eventThread.Abort();
          //}


          // Release and zero DirectShow interfaces
          if (this.mediaSeeking != null)
            this.mediaSeeking = null;
          //if (this.mediaEvent != null)
          //  this.mediaEvent = null;
          if (this.mediaControl != null)
            this.mediaControl = null;
          if (this.basicAudio != null)
            this.basicAudio = null;

          if (this.filterGraph != null)
          {
            //// End event thread.
            //int hr = ((IMediaEventSink)this.filterGraph).Notify(EventCode.UserAbort, IntPtr.Zero, IntPtr.Zero);
            //DsError.ThrowExceptionForHR(hr);
            ////EventCode code;
            ////hr = ((IMediaEvent)this.filterGraph).WaitForCompletion(0, out code);
            ////DsError.ThrowExceptionForHR(hr);
            //while (!threadCompleted)
            //{
            //  Thread.Sleep(2);
            //}
            Marshal.ReleaseComObject(this.filterGraph);
          }
          this.filterGraph = null;

          GC.Collect();
        }
      }
      catch (Exception ex)
      {
        MessageBox.Show(ex.Message, "Exception occured");
      }
    }

    /*
     * Media Related methods
     */

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
    }

    private int ToggleMute()
    {
      int hr = 0;

      if ((this.filterGraph == null) || (this.basicAudio == null))
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
      }

      return hr;
    }

    private string GetClipFileName()
    {
      OpenFileDialog openFileDialog=new OpenFileDialog();
      openFileDialog.FileName = "*.*";
      openFileDialog.Filter = "Music files|*.mp3;*.wav;*.wma|MP3 - files|*.mp3|WAV - files|*.wav|Windows Media Files (wma)|*.wma|All files|*.*";
      openFileDialog.FilterIndex = 0;

      if (openFileDialog.ShowDialog() == DialogResult.OK)
      {
        return openFileDialog.FileName;
      }
      else
        return string.Empty;
    }

    #endregion //PRIVATEMETHODS


    ///////////////////////////////////////////////////////////////////////////////
    // Small helping Methods                                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region HELPER
    #endregion //HELPER

  }
}
