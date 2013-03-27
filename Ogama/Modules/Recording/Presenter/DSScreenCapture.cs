// <copyright file="DSScreenCapture.cs" company="alea technologies">
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

namespace Ogama.Modules.Recording.Presenter
{
  using System;
  using System.Drawing;
  using System.IO;
  using System.Runtime.InteropServices;
  using System.Windows.Forms;

  using DirectShowLib;
  using DirectShowLib.DMO;

  using GTHardware.Cameras.DirectShow;

  using Ogama.ExceptionHandling;
  using Ogama.Modules.Common.Tools;

  using OgamaControls;

  /// <summary>
  /// This class is used to create a DirectShow graph which 
  /// has the OgamaCapture device as a source, which is split
  /// with a Smart Tee into a capture stream which is rendered to a file
  /// and a preview stream which is shown on the recorder
  /// module preview added with a gaze and mouse overlay dmo filter.
  /// </summary>
  public class DSScreenCapture
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
    /// Property Backer: Name of file to capture to
    /// </summary>
    private string filename;

    /// <summary>
    /// Temporary file allocated much space to write avi file to
    /// </summary>
    private string tempFilename;

    /// <summary>
    /// The frame rate of the screen capture graph.
    /// </summary>
    private int frameRate;

    /// <summary>
    /// The zero-based index of the monitor (in dual monitor setups)
    /// to be captured.
    /// </summary>
    private int monitorIndex;

    /// <summary>
    ///  Property Backer: Owner control for preview
    /// </summary>
    private Control previewWindow;

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
    /// The smart tee filter which introduces a preview to the screen capture stream.
    /// </summary>
    private IBaseFilter smartTeeFilter;

    /// <summary>
    /// DShow Filter: selected video device
    /// </summary>
    private IBaseFilter screenCaptureFilter;

    /// <summary>
    /// DShow Filter: selected video compressor
    /// </summary>
    private IBaseFilter videoCompressorFilter;

    /// <summary>
    /// DShow Filter: multiplexor (combine video and audio streams)
    /// </summary>
    private IBaseFilter muxFilter;

    /// <summary>
    /// DShow Filter: file writer
    /// </summary>
    private IFileSinkFilter fileWriterFilter;

    /// <summary>
    /// The human readable string for the video compressor.
    /// </summary>
    private string videoCompressorName;

    /// <summary>
    /// The dmo filter which performs the gaze and mouse overlay
    /// </summary>
    private IBaseFilter dmoFilter;

    /// <summary>
    /// The <see cref="IDMOWrapperFilter"/> which hosts the gaze and mouse
    /// overlay dmo.
    /// </summary>
    private IDMOWrapperFilter dmoWrapperFilter;

    /// <summary>
    /// Contains the position coordinates for the gaze and mouse overlay dmo.
    /// </summary>
    private IMediaParams dmoParams;

    /// <summary>
    /// The <see cref="MPData"/> for the x position of the gaze
    /// </summary>
    private MPData gazeX;

    /// <summary>
    /// The <see cref="MPData"/> for the y position of the gaze
    /// </summary>
    private MPData gazeY;

    /// <summary>
    /// The <see cref="MPData"/> for the x position of the mouse
    /// </summary>
    private MPData mouseX;

    /// <summary>
    /// The <see cref="MPData"/> for the y position of the mouse
    /// </summary>
    private MPData mouseY;

    #endregion //FIELDS

    ///////////////////////////////////////////////////////////////////////////////
    // Construction and Initializing methods                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region CONSTRUCTION

    /// <summary>
    /// Initializes a new instance of the <see cref="DSScreenCapture"/> class.
    /// </summary>
    /// <param name="videoCompressor">A <see cref="String"/> with the friendly
    /// name of the video compressor to be used in the file capture stream.</param>
    /// <param name="newFrameRate">An <see cref="Int32"/> with the framerate
    /// to use for capturing.</param>
    /// <param name="monitorIndex">The zero based index of the monitor screen
    /// to be captured.</param>
    public DSScreenCapture(
      string videoCompressor,
      int newFrameRate,
      int monitorIndex)
    {
      this.videoCompressorName = videoCompressor;
      this.frameRate = newFrameRate;
      this.monitorIndex = monitorIndex;
      this.tempFilename = this.GetTempAVIFilename();
      this.createGraph();
    }

    /// <summary> 
    /// Finalizes an instance of the DSScreenCapture class. Dispose of resources.
    /// </summary>
    ~DSScreenCapture()
    {
      this.Dispose();
    }

    #endregion //CONSTRUCTION

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Enumerations                                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region ENUMS

    /// <summary>
    /// This interface import is for the IOgamaScreenCapture which
    /// has the properties to modify the screen capture filter.
    /// </summary>
    [ComImport, System.Security.SuppressUnmanagedCodeSecurity,
    Guid("E86F68BE-BEC5-4d7b-9CB9-6954ACE63C87"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IOgamaScreenCapture
    {
      /// <summary>
      /// Gets the monitor index set in the screen capture filter.
      /// </summary>
      /// <param name="index">Out. The zero-based index
      /// of the monitor whichs surface should be captured.</param>
      /// <returns>An HRESULT value for success or fail.</returns>
      [PreserveSig]
      int get_Monitor([Out] out int index);

      /// <summary>
      /// Sets the monitor index to use in the screen capture filter.
      /// </summary>
      /// <param name="index">In. The zero-based index
      /// of the monitor whichs surface should be captured.</param>
      /// <returns>An HRESULT value for success or fail.</returns>
      [PreserveSig]
      int set_Monitor([In] int index);

      /// <summary>
      /// Gets the framerate of the screen capture filter.
      /// </summary>
      /// <param name="framerate">Out. The framerate in frames per second
      /// that the ogama screen capture filter should produce screen captures.
      /// Is valid from 1 to 30.</param>
      /// <returns>An HRESULT value for success or fail.</returns>
      [PreserveSig]
      int get_Framerate([Out] out int framerate);

      /// <summary>
      /// Sets the framerate of the screen capture filter.
      /// </summary>
      /// <param name="framerate">In. The framerate in frames per second
      /// that the ogama screen capture filter should produce screen captures.
      /// Is valid from 1 to 30.</param>
      /// <returns>An HRESULT value for success or fail.</returns>
      [PreserveSig]
      int set_Framerate([In] int framerate);
    }

    #endregion ENUMS

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Properties                                                       //
    ///////////////////////////////////////////////////////////////////////////////
    #region PROPERTIES

    /// <summary>
    /// Sets the <see cref="Control"/> which should show the preview
    /// stream of the underlying graph. 
    /// Setting it will not make it visible, the window
    /// will automatically be set visible on start of the media control.
    /// </summary>
    public Control PreviewWindow
    {
      set
      {
        if (SecondaryScreen.SystemHasSecondaryScreen())
        {
          this.previewWindow = value;

          if (this.previewWindow != null)
          {
            // Get the IVideoWindow interface
            this.videoWindow = (IVideoWindow)this.graphBuilder;

            // Position video window in client rect of owner window
            this.previewWindow.Resize += new EventHandler(this.onPreviewWindowResize);
            this.onPreviewWindowResize(this, null);
          }
        }
      }
    }

    /// <summary>
    /// Sets the filename for the filewriter part of the capture graph.
    /// </summary>
    public string Filename
    {
      set
      {
        this.Stop();

        this.filename = value;
        this.tempFilename = this.GetTempAVIFilename();

        if (this.fileWriterFilter != null)
        {
          AMMediaType mediaType = new AMMediaType();
          string oldFile;
          int hr = this.fileWriterFilter.GetCurFile(out oldFile, mediaType);
          DsError.ThrowExceptionForHR(hr);

          hr = this.fileWriterFilter.SetFileName(this.tempFilename, mediaType);
          DsError.ThrowExceptionForHR(hr);
        }
      }
    }

    /// <summary>
    /// Gets a value indicating whether the graph is running.
    /// </summary>
    public bool IsRunning { get; private set; }

    #endregion //PROPERTIES

    ///////////////////////////////////////////////////////////////////////////////
    // Public methods                                                            //
    ///////////////////////////////////////////////////////////////////////////////
    #region PUBLICMETHODS

    /// <summary> Begin capturing. </summary>
    public void Start()
    {
      if (this.mediaControl != null)
      {
        // Start the filter graph: begin capturing
        int hr = this.mediaControl.Run();
        DsError.ThrowExceptionForHR(hr);

        this.IsRunning = true;
      }

      if (this.videoWindow != null)
      {
        // Set owner
        int hr = this.videoWindow.put_Owner(ThreadSafe.GetHandle(this.previewWindow));
        DsError.ThrowExceptionForHR(hr);

        // Set video window style
        hr = this.videoWindow.put_WindowStyle(WindowStyle.Child | WindowStyle.ClipChildren | WindowStyle.ClipSiblings);
        DsError.ThrowExceptionForHR(hr);

        // Set visible
        hr = this.videoWindow.put_Visible(OABool.True);
        DsError.ThrowExceptionForHR(hr);
      }
    }

    /// <summary> 
    ///  Stop the current capture capture. If there is no
    ///  current capture, this method will succeed.
    /// </summary>
    public void Stop()
    {
      int hr = 0;

      if (this.mediaControl != null)
      {
        this.mediaControl.Stop();
      }

      if (this.videoWindow != null)
      {
        hr = this.videoWindow.put_Visible(OABool.False);
        DsError.ThrowExceptionForHR(hr);

        hr = this.videoWindow.put_Owner(IntPtr.Zero);
        DsError.ThrowExceptionForHR(hr);
      }

      if (this.IsRunning)
      {
        hr = this.captureGraphBuilder.CopyCaptureFile(this.tempFilename, this.filename, false, null);
        DsError.ThrowExceptionForHR(hr);
      }

      this.IsRunning = false;
    }

    /// <summary> 
    ///  Calls Stop, releases all references. If a capture is in progress
    ///  it will be stopped, but the CaptureComplete event will NOT fire.
    /// </summary>
    public void Dispose()
    {
      try
      {
#if DEBUG
        // Remove graph from the ROT 
        if (this.rotCookie != null)
        {
          this.rotCookie.Dispose();
          this.rotCookie = null;
        }
#endif

        // Free the preview window (ignore errors)
        if (this.videoWindow != null)
        {
          this.videoWindow.put_Visible(OABool.False);
          this.videoWindow.put_Owner(IntPtr.Zero);
          this.videoWindow = null;
        }

        // Remove the Resize event handler
        if (this.previewWindow != null)
        {
          this.previewWindow.Resize -= new EventHandler(this.onPreviewWindowResize);
        }

        // Cleanup
        if (this.graphBuilder != null)
        {
          Marshal.ReleaseComObject(this.graphBuilder);
          this.graphBuilder = null;

          // These are copies of graphBuilder
          this.mediaControl = null;
          this.videoWindow = null;
        }

        if (this.captureGraphBuilder != null)
        {
          Marshal.ReleaseComObject(this.captureGraphBuilder);
          this.captureGraphBuilder = null;
        }

        if (this.muxFilter != null)
        {
          Marshal.ReleaseComObject(this.muxFilter);
          this.muxFilter = null;
        }

        if (this.fileWriterFilter != null)
        {
          Marshal.ReleaseComObject(this.fileWriterFilter);
          this.fileWriterFilter = null;
        }

        if (this.screenCaptureFilter != null)
        {
          Marshal.ReleaseComObject(this.screenCaptureFilter);
          this.screenCaptureFilter = null;
        }

        if (this.videoCompressorFilter != null)
        {
          Marshal.ReleaseComObject(this.videoCompressorFilter);
          this.videoCompressorFilter = null;
        }

        if (this.dmoFilter != null)
        {
          Marshal.ReleaseComObject(this.dmoFilter);
          this.dmoFilter = null;
        }

        if (this.smartTeeFilter != null)
        {
          Marshal.ReleaseComObject(this.smartTeeFilter);
          this.smartTeeFilter = null;
        }

        // For unmanaged objects we haven't released explicitly
        GC.Collect();
      }
      catch (Exception ex)
      {
        ExceptionMethods.HandleExceptionSilent(ex);
      }
    }

    /// <summary>
    /// Updates the GazeOverlay DMO filter parameters with the new positions
    /// of the gaze and mouse locations.
    /// </summary>
    /// <param name="gazeLocation">A <see cref="Point"/> with the new gaze location.</param>
    /// <param name="mouseLocation">A <see cref="Point"/> with the new mouse location.</param>
    public void UpdateDMOParams(Point gazeLocation, Point mouseLocation)
    {
      if (this.dmoParams == null || this.mediaControl == null)
      {
        return;
      }

      int hr;

      this.gazeX.vInt = gazeLocation.X;
      hr = this.dmoParams.SetParam(0, this.gazeX);
      DMOError.ThrowExceptionForHR(hr);

      this.gazeY.vInt = gazeLocation.Y;
      hr = this.dmoParams.SetParam(1, this.gazeY);
      DMOError.ThrowExceptionForHR(hr);

      this.mouseX.vInt = mouseLocation.X;
      hr = this.dmoParams.SetParam(2, this.mouseX);
      DMOError.ThrowExceptionForHR(hr);

      this.mouseY.vInt = mouseLocation.Y;
      hr = this.dmoParams.SetParam(3, this.mouseY);
      DMOError.ThrowExceptionForHR(hr);
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
    ///  Create a new filter graph and add filters (devices, compressors, 
    ///  misc), but leave the filters unconnected. Call renderGraph()
    ///  to connect the filters.
    /// </summary>
    /// <returns>True if successful created the graph.</returns>
    protected bool createGraph()
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
        // Get the ogama screen capture device and add it to the filter graph
        this.screenCaptureFilter = DirectShowUtils.CreateFilter(
          FilterCategory.VideoInputDevice, "OgamaCapture");

        hr = this.graphBuilder.AddFilter(this.screenCaptureFilter, "OgamaCapture");
        DsError.ThrowExceptionForHR(hr);

        // Query the interface for the screen capture Filter
        var ogamaFilter = this.screenCaptureFilter as IOgamaScreenCapture;

        hr = ogamaFilter.set_Monitor(this.monitorIndex);
        DsError.ThrowExceptionForHR(hr);

        hr = ogamaFilter.set_Framerate(this.frameRate);
        DsError.ThrowExceptionForHR(hr);

        this.smartTeeFilter = new SmartTee() as IBaseFilter;
        hr = this.graphBuilder.AddFilter(this.smartTeeFilter, "Smart Tee");
        DsError.ThrowExceptionForHR(hr);

        if (SecondaryScreen.SystemHasSecondaryScreen())
        {
          // Add a DMO Wrapper Filter
          this.dmoFilter = (IBaseFilter)new DMOWrapperFilter();
          this.dmoWrapperFilter = (IDMOWrapperFilter)this.dmoFilter;

          // But it is more useful to show how to scan for the DMO
          Guid g = this.FindGuid("DmoOverlay", DMOCategory.VideoEffect);

          hr = this.dmoWrapperFilter.Init(g, DMOCategory.VideoEffect);
          DMOError.ThrowExceptionForHR(hr);

          this.SetDMOParams(this.dmoFilter);

          // Add it to the Graph
          hr = this.graphBuilder.AddFilter(this.dmoFilter, "DMO Filter");
          DsError.ThrowExceptionForHR(hr);
        }

        // Get the video compressor and add it to the filter graph
        // Create the filter for the selected video compressor
        this.videoCompressorFilter = DirectShowUtils.CreateFilter(
          FilterCategory.VideoCompressorCategory,
          this.videoCompressorName);
        hr = this.graphBuilder.AddFilter(this.videoCompressorFilter, "Video Compressor");
        DsError.ThrowExceptionForHR(hr);

        // Render the file writer portion of graph (mux -> file)
        hr = this.captureGraphBuilder.SetOutputFileName(
          MediaSubType.Avi,
          this.tempFilename,
          out this.muxFilter,
          out this.fileWriterFilter);
        DsError.ThrowExceptionForHR(hr);

        //// Disable overwrite
        //// hr = this.fileWriterFilter.SetMode(AMFileSinkFlags.None);
        //// DsError.ThrowExceptionForHR(hr);

        hr = this.captureGraphBuilder.AllocCapFile(this.tempFilename, 10000000);
        DsError.ThrowExceptionForHR(hr);

        if (SecondaryScreen.SystemHasSecondaryScreen())
        {
          hr = this.captureGraphBuilder.RenderStream(
            null,
            null,
            this.screenCaptureFilter,
            null,
            this.smartTeeFilter);
          DsError.ThrowExceptionForHR(hr);

          hr = this.captureGraphBuilder.RenderStream(
            null,
            null,
            this.smartTeeFilter,
            this.videoCompressorFilter,
            this.muxFilter);
          DsError.ThrowExceptionForHR(hr);

          hr = this.captureGraphBuilder.RenderStream(
            null,
            null,
            this.smartTeeFilter,
            null,
            this.dmoFilter);
          DsError.ThrowExceptionForHR(hr);

          hr = this.captureGraphBuilder.RenderStream(
            null,
            null,
            this.dmoFilter,
            null,
            null);
          DsError.ThrowExceptionForHR(hr);
        }
        else
        {
          hr = this.captureGraphBuilder.RenderStream(
            null,
            null,
            this.screenCaptureFilter,
            this.videoCompressorFilter,
            this.muxFilter);
          DsError.ThrowExceptionForHR(hr);
        }

        // IMediaFilter filter = this.graphBuilder as IMediaFilter;
        // IReferenceClock clock;
        // filter.GetSyncSource(out clock);
        hr = this.graphBuilder.SetDefaultSyncSource();
        DsError.ThrowExceptionForHR(hr);

        // Retreive the media control interface (for starting/stopping graph)
        this.mediaControl = (IMediaControl)this.graphBuilder;
      }
      catch (Exception ex)
      {
        ExceptionMethods.HandleException(ex);
        return false;
      }

      return true;
    }

    /// <summary> 
    /// Resize the preview when the PreviewWindow is resized 
    /// </summary>
    /// <param name="sender">Source of the event</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    protected void onPreviewWindowResize(object sender, EventArgs e)
    {
      if (this.videoWindow != null)
      {
        // Position video window in client rect of owner window
        Rectangle rc = this.previewWindow.ClientRectangle;
        this.videoWindow.SetWindowPosition(0, 0, rc.Right, rc.Bottom);
      }
    }

    #endregion //PRIVATEMETHODS

    ///////////////////////////////////////////////////////////////////////////////
    // Small helping Methods                                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region HELPER

    /// <summary>
    /// Returns a temporary zero length file with .avi extension.
    /// </summary>
    /// <returns>The filename for a temporary zero length file with .avi extension.</returns>
    private string GetTempAVIFilename()
    {
      string tmpFile = Path.GetTempFileName();
      tmpFile = tmpFile.Replace(".tmp", ".avi");
      return tmpFile;
    }

    /// <summary>
    /// Finds the <see cref="Guid"/> for the given 
    /// category and object
    /// </summary>
    /// <param name="gn">A string containing the name of the DMO.</param>
    /// <param name="cat">A <see cref="Guid"/> with the dmo category.</param>
    /// <returns>The <see cref="Guid"/> of the found DMO.</returns>
    private Guid FindGuid(string gn, Guid cat)
    {
      int hr;

      IEnumDMO dmoEnum;
      Guid[] g2 = new Guid[1];
      string[] sn = new string[1];

      hr = DMOUtils.DMOEnum(cat, 0, 0, null, 0, null, out dmoEnum);
      DMOError.ThrowExceptionForHR(hr);

      try
      {
        do
        {
          hr = dmoEnum.Next(1, g2, sn, IntPtr.Zero);
        }
        while (hr == 0 && sn[0] != gn);

        // Handle any serious errors
        DMOError.ThrowExceptionForHR(hr);

        if (hr != 0)
        {
          throw new Exception("Cannot find " + gn);
        }
      }
      finally
      {
        Marshal.ReleaseComObject(dmoEnum);
      }

      return g2[0];
    }

    /// <summary>
    /// Creates the parameter connection for the gaze and mouse location
    /// properties of the dmo filter.
    /// </summary>
    /// <param name="dmoWrapperFilter">The <see cref="IBaseFilter"/> interface
    /// of the dmo wrapper filter.</param>
    private void SetDMOParams(IBaseFilter dmoWrapperFilter)
    {
      if (dmoWrapperFilter == null)
      {
        return;
      }

      int hr;
      this.dmoParams = dmoWrapperFilter as IMediaParams;

      this.gazeX = new MPData();
      this.gazeX.vInt = 0;
      hr = this.dmoParams.SetParam(0, this.gazeX);
      DMOError.ThrowExceptionForHR(hr);

      this.gazeY = new MPData();
      this.gazeY.vInt = 0;
      hr = this.dmoParams.SetParam(1, this.gazeY);
      DMOError.ThrowExceptionForHR(hr);

      this.mouseX = new MPData();
      this.mouseX.vInt = 0;
      hr = this.dmoParams.SetParam(2, this.mouseX);
      DMOError.ThrowExceptionForHR(hr);

      this.mouseY = new MPData();
      this.mouseY.vInt = 0;
      hr = this.dmoParams.SetParam(3, this.mouseY);
      DMOError.ThrowExceptionForHR(hr);
    }

    #endregion //HELPER
  }
}
