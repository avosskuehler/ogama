// <copyright file="DSRecord.cs" company="FU Berlin">
// ******************************************************
// OGAMA - open gaze and mouse analyzer 
// Copyright (C) 2013 Dr. Adrian Voßkühler  
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
  using System.Drawing;
  using System.Runtime.InteropServices;
  using System.Threading;
  using System.Windows.Forms;

  using DirectShowLib;

  using GTHardware.Cameras.DirectShow;

  using Ogama.ExceptionHandling;

  using OgamaControls;

  /// <summary>
  /// A class to construct a graph using the GenericSampleSourceFilter.
  /// Play a video into a window using the GenericSampleSourceFilter as the video source.
  /// </summary>
  public partial class DSRecord : IDisposable
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
    /// The class that retrieves the images
    /// </summary>
    private ImageHandler imageHandler;

    /// <summary>
    /// graph builder interfaces
    /// </summary>
    private IFilterGraph2 filterGraph;

    /// <summary>
    /// Another graph builder interface
    /// </summary>
    private IMediaControl mediaControl;

    /// <summary>
    /// The <see cref="IBaseFilter"/> for the video compressor.
    /// </summary>
    private IBaseFilter videoCompressor;

    /// <summary>
    /// The <see cref="IBaseFilter"/> for the audio compressor.
    /// </summary>
    private IBaseFilter audioCompressor;

    /// <summary>
    /// The <see cref="IVideoWindow"/> interface for the preview.
    /// </summary>
    private IVideoWindow videoWindow;

    /// <summary>
    /// A <see cref="VideoExportProperties"/> with the video properties.
    /// </summary>
    private VideoExportProperties videoExportProperties;

    /// <summary>
    /// Saves the <see cref="Control"/> of the preview window.
    /// </summary>
    private Control previewWindow;

#if DEBUG
    /// <summary>
    /// Allow you to "Connect to remote graph" from GraphEdit
    /// </summary>
    private DsROTEntry dsRot;
#endif

    #endregion //FIELDS

    ///////////////////////////////////////////////////////////////////////////////
    // Construction and Initializing methods                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region CONSTRUCTION

    /// <summary>
    /// Initializes a new instance of the DSRecord class.
    /// </summary>
    /// <param name="newImageHandler">An <see cref="ImageFromVectorGraphics"/>
    /// with the callback.</param>
    /// <param name="newVideoExportProperties">A <see cref="VideoExportProperties"/> with 
    /// the video export properties.</param>
    /// <param name="newPreviewWindow">A <see cref="Control"/> with 
    /// the preview panel.</param>
    public DSRecord(
      ImageFromVectorGraphics newImageHandler,
      VideoExportProperties newVideoExportProperties,
      Control newPreviewWindow)
    {
      try
      {
        // Set capture file name
        this.videoExportProperties = newVideoExportProperties;

        // set the image provider
        this.imageHandler = newImageHandler;

        if (this.videoExportProperties.OutputVideoProperties.VideoCompressor != string.Empty)
        {
          // Create the filter for the selected video compressor
          this.videoCompressor = DirectShowUtils.CreateFilter(
            FilterCategory.VideoCompressorCategory,
            this.videoExportProperties.OutputVideoProperties.VideoCompressor);
        }

        if (this.videoExportProperties.OutputVideoProperties.AudioCompressor != string.Empty)
        {
          // Create the filter for the selected video compressor
          this.audioCompressor = DirectShowUtils.CreateFilter(
            FilterCategory.AudioCompressorCategory,
            this.videoExportProperties.OutputVideoProperties.AudioCompressor);
        }

        // Set up preview window
        this.previewWindow = newPreviewWindow;

        // Set up the graph
        if (!this.SetupGraph())
        {
          throw new OperationCanceledException("The DirectShow graph could not be created,"
            + " try to use another video or audio compressor.");
        }
      }
      catch
      {
        this.Dispose();
        throw;
      }
    }

    /// <summary>
    /// Finalizes an instance of the DSRecord class.
    /// </summary>
    ~DSRecord()
    {
      this.CloseInterfaces();
    }

    #endregion //CONSTRUCTION

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Enumerations                                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region ENUMS

    /// <summary>
    /// Event called when the graph stops
    /// </summary>
    public event EventHandler Completed;

    #endregion ENUMS

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Properties                                                       //
    ///////////////////////////////////////////////////////////////////////////////
    #region PROPERTIES
    #endregion //PROPERTIES

    ///////////////////////////////////////////////////////////////////////////////
    // Public methods                                                            //
    ///////////////////////////////////////////////////////////////////////////////
    #region PUBLICMETHODS

    /// <summary>
    /// Disposes garbage and closes all open interfaces.
    /// </summary>
    public void Dispose()
    {
      GC.SuppressFinalize(this);
      this.CloseInterfaces();
    }

    /// <summary>
    /// Start playing
    /// </summary>
    public void Start()
    {
      if (this.mediaControl == null)
      {
        ExceptionMethods.ProcessErrorMessage("Could not start video export, please try again with other compressors.");
        return;
      }

      // Create a new thread to process events
      var t = new Thread(this.EventWait) { Name = "Media Event Thread" };
      t.Start();

      var hr = this.mediaControl.Run();
      DsError.ThrowExceptionForHR(hr);
    }

    /// <summary>
    /// Stop the capture graph.
    /// </summary>
    public void Stop()
    {
      int hr;

      // Don´t stop it, cause this hangs with the buffer callback that is never called again
      // hr = this.mediaControl.Stop();
      // DsError.ThrowExceptionForHR(hr);

      //// Send an event saying we are complete
      //if (this.Completed != null)
      //{
      //  var ca = new DESCompletedArgs(0);
      //  this.Completed(this, ca);
      //}

      // Just tell the event loop to be finished
      hr = ((IMediaEventSink)this.filterGraph).Notify(EventCode.UserAbort, IntPtr.Zero, IntPtr.Zero);
      DsError.ThrowExceptionForHR(hr);
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
    // Methods for doing main class job                                          //
    ///////////////////////////////////////////////////////////////////////////////
    #region PRIVATEMETHODS

    /// <summary>
    /// Build the filter graph
    /// </summary>
    /// <returns>True if succesful, otherwise false.</returns>
    private bool SetupGraph()
    {
      int hr;

      // Get the graphbuilder object
      this.filterGraph = new FilterGraph() as IFilterGraph2;

      // Get a ICaptureGraphBuilder2 to help build the graph
      ICaptureGraphBuilder2 captureGraph = (ICaptureGraphBuilder2)new CaptureGraphBuilder2();

      try
      {
        // Link the ICaptureGraphBuilder2 to the IFilterGraph2
        hr = captureGraph.SetFiltergraph(this.filterGraph);
        DsError.ThrowExceptionForHR(hr);

#if DEBUG
        // Allows you to view the graph with GraphEdit File/Connect
        this.dsRot = new DsROTEntry(this.filterGraph);
#endif

        // Our data source
        IBaseFilter bitmapSource = (IBaseFilter)new GenericSampleSourceFilter();

        try
        {
          // Get the pin from the filter so we can configure it
          IPin ipin = DsFindPin.ByDirection(bitmapSource, PinDirection.Output, 0);

          try
          {
            // Configure the pin using the provided BitmapInfo
            this.ConfigurePusher((IGenericSampleConfig)ipin);
          }
          catch (Exception ex)
          {
            ExceptionMethods.HandleException(ex);
            return false;
          }
          finally
          {
            Marshal.ReleaseComObject(ipin);
          }

          // Add the source filter to the graph
          hr = this.filterGraph.AddFilter(bitmapSource, "GenericSampleSourceFilter");
          Marshal.ThrowExceptionForHR(hr);

          IBaseFilter smartTee = new SmartTee() as IBaseFilter;

          // Add the smart tee filter to the graph
          hr = this.filterGraph.AddFilter(smartTee, "Smart Tee");
          Marshal.ThrowExceptionForHR(hr);

          // Add the Video compressor filter to the graph
          if (this.videoCompressor != null)
          {
            hr = this.filterGraph.AddFilter(this.videoCompressor, "video compressor filter");
            DsError.ThrowExceptionForHR(hr);
          }

          // Create the file writer part of the graph. 
          // SetOutputFileName does this for us, and returns the mux and sink
          IBaseFilter mux;
          IFileSinkFilter sink;
          hr = captureGraph.SetOutputFileName(
            MediaSubType.Avi,
            this.videoExportProperties.OutputVideoProperties.Filename,
            out mux,
            out sink);
          DsError.ThrowExceptionForHR(hr);

          // Connect the bitmap source output to the smart tee
          hr = captureGraph.RenderStream(
            null,
            null,
            bitmapSource,
            null,
            smartTee);
          DsError.ThrowExceptionForHR(hr);

          // Render the smart tee capture pin to the capture part including
          // compressor to the file muxer.
          hr = captureGraph.RenderStream(
            null,
            null,
            smartTee,
            this.videoCompressor,
            mux);
          DsError.ThrowExceptionForHR(hr);

          // Render the smart tee preview pin to the default video renderer
          hr = captureGraph.RenderStream(
           null,
           null,
           smartTee,
           null,
           null);
          DsError.ThrowExceptionForHR(hr);

          // Configure the Video Window
          this.videoWindow = this.filterGraph as IVideoWindow;
          this.ConfigureVideoWindow(this.videoWindow, this.previewWindow);
        }
        catch (Exception ex)
        {
          ExceptionMethods.HandleException(ex);
          return false;
        }
        finally
        {
          //Marshal.ReleaseComObject(bitmapSource);
        }

        // Grab some other interfaces
        this.mediaControl = this.filterGraph as IMediaControl;
      }
      catch (Exception ex)
      {
        ExceptionMethods.HandleException(ex);
        return false;
      }
      finally
      {
        Marshal.ReleaseComObject(captureGraph);
      }

      return true;
    }

    /// <summary>
    /// Configure the video window
    /// </summary>
    /// <param name="videoWindow">Interface of the video renderer</param>
    /// <param name="previewControl">Preview Control to draw into</param>
    private void ConfigureVideoWindow(IVideoWindow videoWindow, Control previewControl)
    {
      int hr;

      if (previewControl == null)
      {
        return;
      }

      // Set the output window
      hr = videoWindow.put_Owner(ThreadSafe.GetHandle(previewControl));
      if (hr >= 0) // If there is video
      {
        // Set the window style
        hr = videoWindow.put_WindowStyle(WindowStyle.Child | WindowStyle.ClipChildren | WindowStyle.ClipSiblings);
        DsError.ThrowExceptionForHR(hr);

        // Make the window visible
        hr = videoWindow.put_Visible(OABool.True);
        DsError.ThrowExceptionForHR(hr);

        // Position the playing location
        Rectangle rc = ThreadSafe.GetClientRectangle(previewControl);
        hr = videoWindow.SetWindowPosition(0, 0, rc.Right, rc.Bottom);
        DsError.ThrowExceptionForHR(hr);
      }
    }

    /// <summary>
    /// Configure the GenericSampleSourceFilter
    /// </summary>
    /// <param name="ips">The interface to the GenericSampleSourceFilter</param>
    private void ConfigurePusher(IGenericSampleConfig ips)
    {
      int hr;

      this.imageHandler.SetMediaType(ips);

      // Specify the callback routine to call with each sample
      hr = ips.SetBitmapCB(this.imageHandler);
      DsError.ThrowExceptionForHR(hr);
    }

    /// <summary>
    /// Shut down graph
    /// </summary>
    private void CloseInterfaces()
    {
      int hr;

      lock (this)
      {
        // Stop the graph
        if (this.mediaControl != null)
        {
          // Stop the graph
          // Don´t stop it, cause this hangs with the buffer callback that is never called again
          //hr = this.mediaControl.StopWhenReady();
          this.mediaControl = null;
        }

        // Free the preview window (ignore errors)
        if (this.videoWindow != null)
        {
          this.videoWindow.put_Visible(OABool.False);
          this.videoWindow.put_Owner(IntPtr.Zero);
          this.videoWindow = null;
        }

        if (this.imageHandler != null)
        {
          this.imageHandler.Dispose();
          this.imageHandler = null;
        }

        // Release the graph
        if (this.filterGraph != null)
        {
          hr = ((IMediaEventSink)this.filterGraph).Notify(EventCode.UserAbort, IntPtr.Zero, IntPtr.Zero);

          Marshal.ReleaseComObject(this.filterGraph);
          this.filterGraph = null;
        }
      }

#if DEBUG
      if (this.dsRot != null)
      {
        this.dsRot.Dispose();
        this.dsRot = null;
      }
#endif

      GC.Collect();
    }

    /// <summary>
    /// Called on a new thread to process events from the graph.  The thread
    /// exits when the graph finishes.
    /// </summary>
    private void EventWait()
    {
      // Returned when GetEvent is called but there are no events
      const int E_ABORT = unchecked((int)0x80004004);

      EventCode exitCode = 0;

      var mediaEvent = (IMediaEvent)this.filterGraph;

      do
      {
        // Read the event
        int hr;
        IntPtr p1;
        IntPtr p2;
        EventCode ec;
        for (hr = mediaEvent.GetEvent(out ec, out p1, out p2, 100);
            hr >= 0;
            hr = mediaEvent.GetEvent(out ec, out p1, out p2, 100))
        {
          // Debug.WriteLine(ec);
          switch (ec)
          {
            // If the clip is finished playing
            case EventCode.Complete:
            case EventCode.ErrorAbort:
            case EventCode.UserAbort:
              exitCode = ec;

              // Release any resources the message allocated
              hr = mediaEvent.FreeEventParams(ec, p1, p2);
              DsError.ThrowExceptionForHR(hr);
              break;

            default:
              // Release any resources the message allocated
              hr = mediaEvent.FreeEventParams(ec, p1, p2);
              DsError.ThrowExceptionForHR(hr);
              break;
          }
        }

        // If the error that exited the loop wasn't due to running out of events
        if (hr != E_ABORT)
        {
          DsError.ThrowExceptionForHR(hr);
        }
      }
      while (exitCode == 0);

      // Send an event saying we are complete
      if (this.Completed != null)
      {
        var ca = new DESCompletedArgs(exitCode);
        this.Completed(this, ca);
      }

      // Exit the thread
    }

    #endregion //PRIVATEMETHODS

    ///////////////////////////////////////////////////////////////////////////////
    // Small helping Methods                                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region HELPER
    #endregion //HELPER
  }
}
