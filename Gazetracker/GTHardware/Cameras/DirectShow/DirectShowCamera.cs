// <copyright file="Capture.cs" company="ITU">
// ******************************************************
// GazeTrackingLibrary for ITU GazeTracker
// Copyright (C) 2010 Martin Tall  
// ------------------------------------------------------------------------
// This program is free software; you can redistribute it and/or modify it 
// under the terms of the GNU General Public License as published by the 
// Free Software Foundation; either version 3 of the License, 
// or (at your option) any later version.
// This program is distributed in the hope that it will be useful, 
// but WITHOUT ANY WARRANTY; without even the implied warranty of 
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU 
// General Public License for more details.
// You should have received a copy of the GNU General Public License 
// along with this program; if not, see http://www.gnu.org/licenses/.
// **************************************************************
// </copyright>
// <author>Martin Tall</author>
// <email>tall@stanford.edu</email>
// <modifiedby>Adrian Voßkühler</modifiedby>

using System;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Threading;
using DirectShowLib;
using Emgu.CV;
using Emgu.CV.Structure;

//using GazeTrackingLibrary.Logging;
//using GazeTrackingLibrary.Settings;

namespace GTHardware.Cameras.DirectShow
{
  using System.Windows;

  using Point = System.Drawing.Point;
  using Size = System.Drawing.Size;

  /// <summary>
  /// This is the main class for the DirectShow interop.
  /// It creates a graph that pushes video frames from a Video Input Device
  /// through the filter chain to a SampleGrabber, from which the
  /// frames can be catched and send into the processing tree of
  /// the application.
  /// </summary>
  public class DirectShowCamera : CameraBase, ISampleGrabberCB
  {

    #region Variables

    public static readonly GTHardware.Camera.DeviceTypeEnum deviceType = GTHardware.Camera.DeviceTypeEnum.DirectShow;

    private DsDevice device;
    private bool isRunning;
    //private bool skipFrameMode;
    private int deviceNumber;
    private int deviceMode;

    /// <summary>
    /// The IAMVideoControl interface controls certain video capture operations 
    /// such as enumerating available frame rates and image orientation.
    /// </summary>
    private IAMVideoControl videoControl;

    /// <summary>
    /// The IAMCameraControl interface controls camera settings such as zoom, 
    /// pan, aperture adjustment, or shutter speed. To obtain this interface, 
    /// query the filter that controls the camera. 
    /// </summary>
    private IAMCameraControl cameraControl;

    /// <summary>
    /// The IAMVideoProcAmp interface adjusts the qualities of an incoming 
    /// video signal, such as brightness, contrast, hue, saturation, gamma, and sharpness.
    /// </summary>
    private IAMVideoProcAmp videoProcAmp;

    /// <summary>
    /// The IAMStreamConfig interface sets the output format on certain capture 
    /// and compression filters, for both audio and video. Applications can use 
    /// this interface to set format properties, such as the output dimensions and 
    /// frame rate (for video) or the sample rate and number of channels (for audio).
    /// </summary>
    private IAMStreamConfig videoStreamConfig;

    /// <summary>
    /// This interface provides methods that enable an application to build a filter graph. 
    /// The Filter Graph Manager implements this interface.
    /// </summary>
    private IFilterGraph2 graphBuilder;

    /// <summary>
    /// The IMediaControl interface provides methods for controlling the 
    /// flow of data through the filter graph. It includes methods for running, pausing, and stopping the graph. 
    /// </summary>

    private IMediaControl mediaControl;
    private ISampleGrabber sampGrabber;
    private IBaseFilter capFilter;
    private ICaptureGraphBuilder2 capGraph;
    private bool isSupportingROI = false;
    private bool isRoiSet = false;
    private Rectangle roi;
    private int width;
    private int height;
    private int stride;
    private int bufferLength;
    private int fps;
    private bool hasValidGraph;
    private IntPtr section = IntPtr.Zero;
    private IntPtr map = IntPtr.Zero;
    private Image<Bgr, byte> videoImage;
    private Stopwatch stopwatch;

#if DEBUG
    /// <summary>
    /// Helps showing capture graph in GraphBuilder
    /// </summary>
    private DsROTEntry rotEntry;
#endif

    #endregion //FIELDS

    #region Constructor

    /// <summary>
    /// Initializes a new instance of the Capture class.
    /// Use capture device zero, default frame rate and size
    /// </summary>
    public DirectShowCamera()
    {
      Initialize();
      DsDevice[] capDevices = DsDevice.GetDevicesOfCat(FilterCategory.VideoInputDevice);
      this.deviceNumber = 0;
      this.deviceMode = 0;

      if (capDevices.Length > 0)
        NewCamera(capDevices[deviceNumber], deviceMode, 0, 0); //force default values
    }


    /// <summary> 
    /// Initializes a new instance of the Capture class.
    /// Use specified capture device, default frame rate and size
    /// </summary>
    /// <param name="device">The <see cref="DsDevice"/> to use.</param>
    public DirectShowCamera(DsDevice device, int deviceNumber)
    {
      Initialize();
      this.deviceNumber = deviceNumber;
      this.deviceMode = 0;
      NewCamera(device, deviceMode, 0, 0);
    }

    /// <summary> 
    /// Initializes a new instance of the Capture class.
    /// Use specified capture device, specified frame rate and default size
    /// </summary>
    /// <param name="device">The <see cref="DsDevice"/> to use.</param>
    /// <param name="frameRate">The framerate for the capturing.</param>
    public DirectShowCamera(DsDevice device, int deviceNumber, int frameRate)
    {
      Initialize();
      this.deviceNumber = deviceNumber;
      this.deviceMode = 0;
      NewCamera(device, frameRate, 0, 0);
    }

    /// <summary> 
    /// Initializes a new instance of the Capture class.
    /// Use specified capture device, specified frame rate and size
    /// </summary>
    /// <param name="device">The <see cref="DsDevice"/> to use.</param>
    /// <param name="frameRate">The framerate for the capturing.</param>
    /// <param name="width">The width of the video stream.</param>
    /// <param name="height">The height of the video stream.</param>
    public DirectShowCamera(DsDevice device, int number, int mode, int frameRate, int width, int height)
    {
      Initialize();
      this.deviceNumber = number;
      this.deviceMode = mode;
      NewCamera(device, frameRate, width, height);
    }

    #endregion //CONSTRUCTION

    #region Events/Delegate

    public delegate void FrameCapHandler();

    //public event FrameCapHandler FrameCaptureComplete;

    #endregion EVENTS

    #region Get/Set

    /// <summary>
    /// Gets the debug purpose performance clock that is in time with
    /// the capture graph.
    /// </summary>
    public Stopwatch PerformanceClock
    {
      get { return stopwatch; }
    }

    public int DeviceNumber
    {
      get { return deviceNumber; }
    }

    public int DeviceMode
    {
      get { return deviceMode; }
    }

    public override bool IsSupportingROI
    {
      get { return isSupportingROI; }
      set { isSupportingROI = value; }
    }

    public override bool IsROISet
    {
      get { return isRoiSet; }
    }

    public override bool IsSettingROI
    {
      get { return false; }
    }


    public Rectangle ROI
    {
      get { return roi; }
      set { roi = value; }
    }

    public override int Width
    {
      get { return width; }
    }

    public override int Height
    {
      get { return height; }
    }

    public override int DefaultWidth
    {
      get { return width; }
    }

    public override int DefaultHeight
    {
      get { return height; }
    }

    public override int FPS
    {
      get { return fps; }
    }

    /// <summary>
    /// Gets a value indicating whether the graph is 
    /// succesfully rendered.
    /// </summary>
    public bool HasValidGraph
    {
      get { return hasValidGraph; }
    }

    public string DeviceName
    {
      get
      {
        if (device != null)
          return device.Name;
        else
          return "DirectShowDevice";
      }
    }

    #endregion //PROPERTIES

    #region Public methods

    public override bool Initialize()
    {
      stopwatch = new Stopwatch();
      return true;
    }

    public override bool Start()
    {
      if (isRunning || mediaControl == null) return false;

      int hr = mediaControl.Run();

      if (hr != 0)
      {
        //ErrorLogger.WriteLine("Error while starting camera. Message: " + DsError.GetErrorText(hr));
        isRunning = false;
      }

      // Set roi to full frame
      //this.ROI = new System.Drawing.Rectangle(new Point(0, 0), new Size(videoWidth, videoHeight));
      isRunning = true;

      return isRunning;
    }

    public override bool Stop()
    {
      if (!isRunning) return false;

      int hr = mediaControl.Pause();
      if (hr != 0)
      {
        //ErrorLogger.WriteLine("Error while pausing camera. Message: " + DsError.GetErrorText(hr));
      }

      isRunning = false;
      return isRunning;
    }

    public override Rectangle SetROI(Rectangle newRoi)
    {
      return new Rectangle(0, 0, width, height);
    }

    public override Rectangle GetROI()
    {
      return new Rectangle(0, 0, width, height);
    }

    public override void ClearROI()
    {

    }

    public override void Cleanup()
    {
      try
      {
        // To stop the capture filter before stopping the media control
        // seems to solve the problem described in the next comment.
        // sancta simplicitas...
        if (capFilter != null)
        {
          capFilter.Stop();
        }

        // The stop or stopwhenready methods sometimes hang ... 
        // This is a multithreading issue but I don´t solved it yet
        // But stopping is needed, otherwise the video device
        // is not disposed fast enough (due to GC) so at next initialization
        // with other params the video device seems to be in 
        // use and the GraphBuilder render mehtod fails.
        if (mediaControl != null)
        {
          // This hangs when closing the GT
          int hr = mediaControl.Stop();
        }

        isRunning = false;
      }
      catch (Exception)
      {
        //ErrorLogger.ProcessException(ex, false);
      }

      if (capFilter != null)
      {
        Marshal.ReleaseComObject(capFilter);
        capFilter = null;
        cameraControl = null;
        videoControl = null;
        videoStreamConfig = null;
      }

      if (videoProcAmp != null)
      {
        Marshal.ReleaseComObject(videoProcAmp);
        videoProcAmp = null;
      }

      if (sampGrabber != null)
      {
        Marshal.ReleaseComObject(sampGrabber);
        sampGrabber = null;
      }

      if (graphBuilder != null)
      {
        Marshal.ReleaseComObject(graphBuilder);
        graphBuilder = null;
        mediaControl = null;
        hasValidGraph = false;
      }

      if (capGraph != null)
      {
        Marshal.ReleaseComObject(capGraph);
        capGraph = null;
      }

      if (map != IntPtr.Zero)
      {
        UnmapViewOfFile(map);
        map = IntPtr.Zero;
      }

      if (section != IntPtr.Zero)
      {
        CloseHandle(section);
        section = IntPtr.Zero;
      }
#if DEBUG
      if (this.rotEntry != null)
      {
        // This hangs when closing the GT
        this.rotEntry.Dispose();
      }
#endif
    }


    /// <summary>
    /// This method creates a new graph for the given capture device and
    /// properties.
    /// </summary>
    /// <param name="device">The index of the new capture device.</param>
    /// <param name="frameRate">The framerate to use.</param>
    /// <param name="width">The width to use.</param>
    /// <param name="height">The height to use.</param>
    public void NewCamera(DsDevice device, int frameRate, int width, int height)
    {
      this.device = device;
      this.Cleanup();

      stopwatch.Start();

      roi = new Rectangle(new Point(0, 0), new Size(width, height));

      try
      {
        // Set up the capture graph
        if (SetupGraph(device, frameRate, width, height))
        {
          isRunning = false;
          SetVideoProcMinMaxRanges();
          SetCameraControlMinMaxRanges();
        }
        else
        {
          hasValidGraph = false;
          return;
        }
      }
      catch
      {
        Cleanup();
        //ErrorLogger.WriteLine("Error in Camera.Capture(), Could not initialize graphs");
        hasValidGraph = false;
        return;
      }

      hasValidGraph = true;
    }


    /// <summary> 
    /// The <see cref="ISampleGrabberCB.SampleCB{Double,IMediaSample}"/> sample callback method.
    /// NOT USED.
    /// </summary>
    /// <param name="sampleTime">Starting time of the sample, in seconds.</param>
    /// <param name="sample">Pointer to the IMediaSample interface of the sample.</param>
    /// <returns>Returns S_OK if successful, or an HRESULT error code otherwise.</returns>
    public int SampleCB(double sampleTime, IMediaSample sample)
    {
      Marshal.ReleaseComObject(sample);
      return 0;
    }

    /// <summary> 
    /// The <see cref="ISampleGrabberCB.BufferCB{Double,IntPtr, Int32}"/> buffer callback method.
    /// Gets called whenever a new frame arrives down the stream in the SampleGrabber.
    /// Updates the memory mapping of the OpenCV image and raises the 
    /// <see cref="FrameCaptureComplete"/> event.
    /// </summary>
    /// <param name="sampleTime">Starting time of the sample, in seconds.</param>
    /// <param name="buffer">Pointer to a buffer that contains the sample data.</param>
    /// <param name="bufferLength">Length of the buffer pointed to by pBuffer, in bytes.</param>
    /// <returns>Returns S_OK if successful, or an HRESULT error code otherwise.</returns>
    public int BufferCB(double sampleTime, IntPtr buffer, int bufferLength)
    {
      if (buffer != IntPtr.Zero)
      {
        // Check mapping if it is not already released and the buffer is running
        if (map != IntPtr.Zero)
        {
          // This is fast and lasts less than 1 millisecond.
          CopyMemory(map, buffer, bufferLength);

          // reset roi on image and use roi (rectagle) upon get
          videoImage.ROI = new Rectangle();

          try
          {
            // Send new image to processing thread
            OnRaiseCustomEvent(new ImageEventArgs(videoImage.Convert<Gray, byte>()));
          }
          catch (ThreadInterruptedException)
          {
            //ErrorLogger.ProcessException(e, false);
          }
          catch (Exception)
          {
            //ErrorLogger.ProcessException(we, false);
          }
        }
      }

      return 0;
    }

    // Remove ?
    private Image<Gray, byte> VideoImage
    {
      get
      {
        if (roi.Width != 0)
        {
          //videoImage.ROI = roi;
          Image<Gray, byte> gray = videoImage.Copy().Convert<Gray, byte>();
          return gray;
        }
        else
          return videoImage.Convert<Gray, byte>();
      }
    }

    #endregion //PUBLICMETHODS

    #region Eventhandlers OnPropChange etc.

    /// <summary>
    /// The event handler for the <see cref="OnCameraControlPropertyChanged"/> event.
    /// Updates the video capture device with new brightness, contrast, etc.
    /// </summary>
    /// <param name="property">The <see cref="VideoProcAmpProperty"/> to be changed</param>
    /// <param name="value">The new value for the property</param>
    public void OnVideoProcAmpPropertyChanged(VideoProcAmpProperty property, int value)
    {
      if (videoProcAmp == null)
        return;

      int min, max, steppingDelta, defaultValue;
      VideoProcAmpFlags flags;

      try
      {
        videoProcAmp.GetRange(property, out min, out max, out steppingDelta, out defaultValue, out flags);

        if (value >= min && value <= max)
          videoProcAmp.Set(property, value, flags);
      }
      catch (Exception)
      {
        //ErrorLogger.ProcessException(ex, false);
      }
    }

    /// <summary>
    /// The event handler for the <see cref="OnVideoProcAmpPropertyChanged"/> event.
    /// Updates the video capture device with new zoom, pan, etc.
    /// </summary>
    /// <param name="property">The <see cref="CameraControlProperty"/> to be changed</param>
    /// <param name="value">The new value for the property</param>
    public void OnCameraControlPropertyChanged(CameraControlProperty property, int value)
    {
      if (cameraControl == null)
        return;

      // Todo: Disabled focus as it turns on autofocus
      if (property.Equals(CameraControlProperty.Focus))
      {
        return;
      }

      int min, max, steppingDelta, defaultValue;
      CameraControlFlags flags;
      try
      {
        cameraControl.GetRange(property, out min, out max, out steppingDelta, out defaultValue, out flags);

        if (value >= min && value <= max)
          cameraControl.Set(property, value, flags);
      }
      catch (Exception)
      {
        //ErrorLogger.ProcessException(ex, false);
      }
    }

    /// <summary>
    /// The event handler for the <see cref="OnVideoControlFlagsChanged"/> event.
    /// Updates the video capture device with new video control properties.
    /// </summary>
    /// <remarks> This method has been disabled, because it was easier to flip the incoming image
    /// with the CV image flip in ImageProcessing.cs.
    /// The direct show flipping didn't work with some webcams, e.g. the PlayStationEye3 cam or an HP Laptop Webcam</remarks>
    /// <param name="property">The <see cref="VideoControlFlags"/> to be changed</param>
    /// <param name="value">The new value for the property</param>
    private void OnVideoControlFlagsChanged(VideoControlFlags property, int value)
    {
      ////if (this.graphBuilder == null)
      ////{
      ////  return;
      ////}

      ////if (videoControl == null)
      ////  return;

      ////VideoControlFlags pCapsFlags;

      ////IPin pPin = DsFindPin.ByCategory(capFilter, PinCategory.Capture, 0);
      ////int hr = videoControl.GetCaps(pPin, out pCapsFlags);

      ////if (hr != 0)
      ////  ErrorLogger.WriteLine("Error: videoControl.GetCaps in Camera.Capture. Message: " + DsError.GetErrorText(hr));

      ////hr = videoControl.GetMode(pPin, out pCapsFlags);

      ////if (hr != 0)
      ////  ErrorLogger.WriteLine("Error while getting mode in videoControl.GetMode in Camera.Capture. Message: " + DsError.GetErrorText(hr));

      ////if (value == 0)
      ////{
      ////  if ((pCapsFlags&VideoControlFlags.FlipVertical)==VideoControlFlags.FlipVertical)
      ////  {
      ////    pCapsFlags |= ~VideoControlFlags.FlipVertical;
      ////  }
      ////}
      ////else
      ////{
      ////  pCapsFlags |= VideoControlFlags.FlipVertical;
      ////}

      ////hr = videoControl.SetMode(pPin, pCapsFlags);

      ////if (hr != 0)
      ////  ErrorLogger.WriteLine("Error while getting mode in videoControl.SetMode in Camera.Capture. Message: " + DsError.GetErrorText(hr));
    }

    #endregion //EVENTHANDLER

    #region Private methods

    /// <summary>
    /// Copies a block of memory from one location to another.
    /// </summary>
    /// <param name="destination">A pointer to the starting address of the copied block's destination.</param>
    /// <param name="source">A pointer to the starting address of the block of memory to copy</param>
    /// <param name="length">The size of the block of memory to copy, in bytes.</param>
    [DllImport("Kernel32.dll", EntryPoint = "RtlMoveMemory")]
    private static extern void CopyMemory(IntPtr destination, IntPtr source, int length);

    /// <summary>
    /// Creates or opens a named or unnamed file mapping object for a specified file.
    /// </summary>
    /// <param name="file">A handle to the file from which to create a file mapping object.</param>
    /// <param name="fileMappingAttributes">A pointer to a SECURITY_ATTRIBUTES structure that determines whether a returned handle can be inherited by child processes.</param>
    /// <param name="protect">The protection for the file view, when the file is mapped.</param>
    /// <param name="maximumSizeHigh">The high-order DWORD of the maximum size of the file mapping object.</param>
    /// <param name="maximumSizeLow">The low-order DWORD of the maximum size of the file mapping object.</param>
    /// <param name="name">The name of the file mapping object.</param>
    /// <returns>If the function succeeds, the return value is a handle to the file mapping object.
    /// If the object exists before the function call, the function returns a handle to the existing object
    /// (with its Instance size, not the specified size), and GetLastError returns ERROR_ALREADY_EXISTS. 
    /// If the function fails, the return value is NULL. To get extended error information, call GetLastError.</returns>
    [DllImport("kernel32.dll", SetLastError = true)]
    private static extern IntPtr CreateFileMapping(IntPtr file, IntPtr fileMappingAttributes, uint protect,
                                                   uint maximumSizeHigh, uint maximumSizeLow, string name);

    /// <summary>
    /// Maps a view of a file mapping into the address space of a calling process.
    /// </summary>
    /// <param name="fileMappingObject">A handle to a file mapping object. The CreateFileMapping and OpenFileMapping functions return this handle.</param>
    /// <param name="desiredAccess">The type of access to a file mapping object, which ensures the protection of the pages.</param>
    /// <param name="fileOffsetHigh">A high-order DWORD of the file offset where the view begins.</param>
    /// <param name="fileOffsetLow">A low-order DWORD of the file offset where the view is to begin. 
    /// The combination of the high and low offsets must specify an offset within the file mapping. 
    /// They must also match the memory allocation granularity of the system. That is, 
    /// the offset must be a multiple of the allocation granularity. </param>
    /// <param name="numberOfBytesToMap">The number of bytes of a file mapping to map to the view.</param>
    /// <returns>If the function succeeds, the return value is the starting address of the mapped view.
    /// If the function fails, the return value is NULL.</returns>
    [DllImport("kernel32.dll", SetLastError = true)]
    private static extern IntPtr MapViewOfFile(IntPtr fileMappingObject, uint desiredAccess, uint fileOffsetHigh,
                                               uint fileOffsetLow, uint numberOfBytesToMap);

    /// <summary>
    /// Unmaps a mapped view of a file from the calling process's address space.
    /// </summary>
    /// <param name="map">A pointer to the base address of the mapped view of a file that is to be unmapped. </param>
    /// <returns>If the function succeeds, the return value is nonzero, and all dirty pages within the specified range are written "lazily" to disk.
    /// If the function fails, the return value is zero. </returns>
    [DllImport("kernel32.dll", SetLastError = true)]
    private static extern bool UnmapViewOfFile(IntPtr map);

    /// <summary>
    /// Closes an open object handle.
    /// </summary>
    /// <param name="handle">A valid handle to an open object.</param>
    /// <returns>If the function succeeds, the return value is nonzero.
    /// If the function fails, the return value is zero.</returns>
    [DllImport("kernel32.dll", SetLastError = true)]
    private static extern bool CloseHandle(IntPtr handle);

    /// <summary>
    /// Connects to the property changed events of the camera settings.
    /// </summary>
    //private void Initialize()
    //{
    //    //Settings.Instance.Camera.OnCameraControlPropertyChanged += OnCameraControlPropertyChanged;
    //    //Settings.Instance.Camera.OnVideoProcAmpPropertyChanged += OnVideoProcAmpPropertyChanged;
    //    //Settings.Instance.Camera.OnVideoControlFlagsChanged += OnVideoControlFlagsChanged;

    //    //stopwatch = new Stopwatch();
    //}

    /// <summary>
    /// Build the capture graph for grabber. 
    /// </summary>
    /// <param name="dev">The index of the new capture device.</param>
    /// <param name="frameRate">The framerate to use.</param>
    /// <param name="width">The width to use.</param>
    /// <param name="height">The height to use.</param>
    /// <returns>True, if succesfull, otherwise false.</returns>
    private bool SetupGraph(DsDevice dev, int frameRate, int width, int height)
    {
      int hr;
      fps = frameRate; // Not measured, only to expose FPS externally 
      cameraControl = null;
      capFilter = null;

      // Get the graphbuilder object
      graphBuilder = (IFilterGraph2)new FilterGraph();
      mediaControl = graphBuilder as IMediaControl;

      try
      {
        // Create the ICaptureGraphBuilder2
        capGraph = (ICaptureGraphBuilder2)new CaptureGraphBuilder2();

        // Create the SampleGrabber interface
        sampGrabber = (ISampleGrabber)new SampleGrabber();

        // Start building the graph
        hr = capGraph.SetFiltergraph(graphBuilder);
        //if (hr != 0)
        //    ErrorLogger.WriteLine("Error in capGraph.SetFiltergraph. Could not build graph. Message: " +
        //                          DsError.GetErrorText(hr));

#if DEBUG
        this.rotEntry = new DsROTEntry(this.graphBuilder);
#endif

        this.capFilter = CreateFilter(
       FilterCategory.VideoInputDevice,
       dev.Name);
        if (this.capFilter != null)
        {
          hr = graphBuilder.AddFilter(this.capFilter, "Video Source");
          DsError.ThrowExceptionForHR(hr);
        }

        //// Add the video device
        //hr = graphBuilder.AddSourceFilterForMoniker(dev.Mon, null, "Video input", out capFilter);
        //if (hr != 0)
        //    ErrorLogger.WriteLine(
        //        "Error in m_graphBuilder.AddSourceFilterForMoniker(). Could not add source filter. Message: " +
        //        DsError.GetErrorText(hr));

        var baseGrabFlt = (IBaseFilter)sampGrabber;

        ConfigureSampleGrabber(sampGrabber);

        // Add the frame grabber to the graph
        hr = graphBuilder.AddFilter(baseGrabFlt, "Ds.NET Grabber");

        //if (hr != 0)
        //    ErrorLogger.WriteLine("Error in m_graphBuilder.AddFilter(). Could not add filter. Message: " +
        //                          DsError.GetErrorText(hr));

        // turn on the infrared leds ONLY FOR THE GENIUS WEBCAM
        /*
        if (!defaultMode)
        {
            m_icc = capFilter as IAMCameraControl;
            CameraControlFlags CamFlags = new CameraControlFlags();
            int pMin, pMax, pStep, pDefault;

            hr = m_icc.GetRange(CameraControlProperty.Focus, out pMin, out pMax, out pStep, out pDefault, out CamFlags);
            m_icc.Set(CameraControlProperty.Focus, pMax, CameraControlFlags.None);
        }
        */


        //IBaseFilter smartTee = new SmartTee() as IBaseFilter;

        //// Add the smart tee filter to the graph
        //hr = this.graphBuilder.AddFilter(smartTee, "Smart Tee");
        //Marshal.ThrowExceptionForHR(hr);

        // Connect the video source output to the smart tee
        //hr = capGraph.RenderStream(null, null, capFilter, null, smartTee);

        hr = capGraph.RenderStream(PinCategory.Capture, MediaType.Video, capFilter, null, baseGrabFlt);
        var errorText = DsError.GetErrorText(hr);

        cameraControl = capFilter as IAMCameraControl;

        // Set videoProcAmp
        object obj;
        var iid_IBaseFilter = new Guid("56a86895-0ad4-11ce-b03a-0020af0ba770");
        DirectShowDevices.Instance.Cameras[deviceNumber].DirectshowDevice.Mon.BindToObject(
            null,
            null,
            ref iid_IBaseFilter,
            out obj);

        videoProcAmp = obj as IAMVideoProcAmp;

        // If any of the default config items are set
        if (frameRate + height + width > 0)
          SetConfigParms(capGraph, capFilter, frameRate, width, height);

        // Check for succesful rendering, if this failed the class cannot be used, so dispose the resources and return false.
        if (hr < 0)
        {
          Cleanup();
          return false;
        }
        else
        {
          // Otherwise update the SampleGrabber.
          SaveSizeInfo(sampGrabber);
          hr = sampGrabber.SetBufferSamples(false);

          if (hr == 0)
          {
            hr = sampGrabber.SetOneShot(false);
            hr = sampGrabber.SetCallback(this, 1);
          }

          //if (hr < 0)
          //    ErrorLogger.WriteLine("Could not set callback function (SetupGraph) in Camera.Capture()");
        }
      }
      catch (Exception)
      {
        //ErrorLogger.ProcessException(ex, false);

        Cleanup();
        return false;
      }

      return true;
    }

    /// <summary>
    /// Enumerates all filters of the selected category and returns the IBaseFilter for the 
    /// filter described in friendlyname
    /// </summary>
    /// <param name="category">Category of the filter</param>
    /// <param name="friendlyname">Friendly name of the filter</param>
    /// <returns>IBaseFilter for the device</returns>
    public static IBaseFilter CreateFilter(Guid category, string friendlyname)
    {
      object source = null;
      try
      {
        Guid iid = typeof(IBaseFilter).GUID;
        foreach (DsDevice device in DsDevice.GetDevicesOfCat(category))
        {
          if (device.Name.CompareTo(friendlyname) == 0)
          {
            device.Mon.BindToObject(null, null, ref iid, out source);
            break;
          }
        }
      }
      catch (Exception ex)
      {
        MessageBox.Show(ex.Message);
      }

      return (IBaseFilter)source;
    }

    /// <summary>
    /// Configure the sample grabber with default Video RGB24 mode.
    /// </summary>
    /// <param name="sampGrabber">The <see cref="ISampleGrabber"/> to be configured.</param>
    private void ConfigureSampleGrabber(ISampleGrabber sampGrabber)
    {
      AMMediaType media;
      int hr;

      // Set the media type to Video/RBG24
      media = new AMMediaType();
      media.majorType = MediaType.Video;
      media.subType = MediaSubType.RGB24;
      media.formatType = FormatType.VideoInfo;

      hr = this.sampGrabber.SetMediaType(media);

      //if (hr != 0)
      //    ErrorLogger.WriteLine("Could not ConfigureSampleGrabber in Camera.Capture. Message: " +
      //                          DsError.GetErrorText(hr));

      DsUtils.FreeAMMediaType(media);
      media = null;

      // Configure the samplegrabber
      hr = this.sampGrabber.SetCallback(this, 1);

      //if (hr != 0)
      //    ErrorLogger.WriteLine("Could not set callback method for sampleGrabber in Camera.Capture. Message: " +
      //                          DsError.GetErrorText(hr));
    }

    /// <summary>
    /// Set the Framerate, and video size
    /// </summary>
    /// <param name="capGraph">The <see cref="ICaptureGraphBuilder2"/> interface.</param>
    /// <param name="capFilter">The <see cref="IBaseFilter"/> of the capture device.</param>
    /// <param name="frameRate">The new framerate to be used.</param>
    /// <param name="width">The new video width to be used.</param>
    /// <param name="height">The new video height to be used.</param>
    private void SetConfigParms(ICaptureGraphBuilder2 capGraph, IBaseFilter capFilter, int frameRate, int width,
                                int height)
    {
      int hr;
      object o;
      AMMediaType media = null;

      // Find the stream config interface
      hr = this.capGraph.FindInterface(PinCategory.Capture, MediaType.Video, capFilter,
                                       typeof(IAMStreamConfig).GUID, out o);

      videoControl = capFilter as IAMVideoControl;
      videoStreamConfig = o as IAMStreamConfig;

      //if (videoStreamConfig == null)
      //    ErrorLogger.WriteLine("Error in Capture.SetConfigParams(). Failed to get IAMStreamConfig");

      // Get the existing format block
      if (videoStreamConfig != null) hr = videoStreamConfig.GetFormat(out media);

      //if (hr != 0)
      //    ErrorLogger.WriteLine("Could not SetConfigParms in Camera.Capture. Message: " + DsError.GetErrorText(hr));

      // copy out the videoinfoheader
      var v = new VideoInfoHeader();
      Marshal.PtrToStructure(media.formatPtr, v);

      // if overriding set values
      if (frameRate > 0)
      {
        v.AvgTimePerFrame = 10000000 / frameRate;
        this.fps = frameRate;
      }
      else
      {
        this.fps = (int)(10000000 / v.AvgTimePerFrame);
      }

      if (width > 0)
        v.BmiHeader.Width = width;

      if (height > 0)
        v.BmiHeader.Height = height;

      // Copy the media structure back
      Marshal.StructureToPtr(v, media.formatPtr, true);

      // Set the new format
      if (videoStreamConfig != null) hr = videoStreamConfig.SetFormat(media);
      //if (hr != 0)
      //    ErrorLogger.WriteLine(
      //        "Error while setting new camera format (videoStreamConfig) in Camera.Capture. Message: " +
      //        DsError.GetErrorText(hr));

      DsUtils.FreeAMMediaType(media);
      media = null;
    }

    /// <summary>
    /// Saves the video properties of the SampleGrabber into member fields
    /// and creates a file mapping for the captured frames.
    /// </summary>
    /// <param name="sampGrabber">The <see cref="ISampleGrabber"/>
    /// from which to retreive the sample information.</param>
    private void SaveSizeInfo(ISampleGrabber sampGrabber)
    {
      int hr;

      // Get the media type from the SampleGrabber
      var media = new AMMediaType();
      hr = sampGrabber.GetConnectedMediaType(media);

      //if (hr != 0)
      //{
      //    ErrorLogger.WriteLine("Could not SaveSizeInfo in Camera.Capture. Message: " + DsError.GetErrorText(hr));
      //}

      //if ((media.formatType != FormatType.VideoInfo) || (media.formatPtr == IntPtr.Zero))
      //{
      //    ErrorLogger.WriteLine("Error in Camera.Capture. Unknown Grabber Media Format");
      //}

      // Grab the size info
      var videoInfoHeader = (VideoInfoHeader)Marshal.PtrToStructure(media.formatPtr, typeof(VideoInfoHeader));
      width = videoInfoHeader.BmiHeader.Width;
      height = videoInfoHeader.BmiHeader.Height;
      stride = width * (videoInfoHeader.BmiHeader.BitCount / 8);
      this.fps = (int)(10000000 / videoInfoHeader.AvgTimePerFrame);

      bufferLength = width * height * 3; // RGB24 = 3 bytes

      // create memory section and map for the OpenCV Image.
      section = CreateFileMapping(new IntPtr(-1), IntPtr.Zero, 0x04, 0, (uint)bufferLength, null);
      map = MapViewOfFile(section, 0xF001F, 0, 0, (uint)bufferLength);
      videoImage = new Image<Bgr, byte>(width, height, stride, map);

      DsUtils.FreeAMMediaType(media);
      media = null;
    }

    /// <summary>
    /// This method is used to set minimum and maximum values for the
    /// <see cref="VideoProcAmpProperty"/>s of the capture device.
    /// </summary>
    private void SetVideoProcMinMaxRanges()
    {
      if (videoProcAmp == null)
      {
        return;
      }

      int min, max, steppingDelta, defaultValue;
      VideoProcAmpFlags flags;

      foreach (VideoProcAmpProperty item in Enum.GetValues(typeof(VideoProcAmpProperty)))
      {
        switch (item)
        {
          case VideoProcAmpProperty.Brightness:
            videoProcAmp.GetRange(VideoProcAmpProperty.Brightness, out min, out max, out steppingDelta,
                                  out defaultValue, out flags);
            //Settings.Instance.Camera.BrightnessMinimum = min;
            //Settings.Instance.Camera.BrightnessMaximum = max;
            //Settings.Instance.Camera.BrightnessStepping = steppingDelta;
            //Settings.Instance.Camera.BrightnessDefault = defaultValue;
            break;

          case VideoProcAmpProperty.Saturation:
            videoProcAmp.GetRange(VideoProcAmpProperty.Saturation, out min, out max, out steppingDelta,
                                  out defaultValue, out flags);
            //Settings.Instance.Camera.SaturationMinimum = min;
            //Settings.Instance.Camera.SaturationMaximum = max;
            //Settings.Instance.Camera.SaturationStepping = steppingDelta;
            //Settings.Instance.Camera.SaturationDefault = defaultValue;
            break;

          case VideoProcAmpProperty.Sharpness:
            videoProcAmp.GetRange(VideoProcAmpProperty.Sharpness, out min, out max, out steppingDelta,
                                  out defaultValue, out flags);
            //Settings.Instance.Camera.SharpnessMinimum = min;
            //Settings.Instance.Camera.SharpnessMaximum = max;
            //Settings.Instance.Camera.SharpnessStepping = steppingDelta;
            //Settings.Instance.Camera.SharpnessDefault = defaultValue;
            break;

          case VideoProcAmpProperty.Contrast:
            videoProcAmp.GetRange(VideoProcAmpProperty.Contrast, out min, out max, out steppingDelta,
                                  out defaultValue, out flags);
            //Settings.Instance.Camera.ContrastMinimum = min;
            //Settings.Instance.Camera.ContrastMaximum = max;
            //Settings.Instance.Camera.ContrastStepping = steppingDelta;
            //Settings.Instance.Camera.ContrastDefault = defaultValue;
            break;

          case VideoProcAmpProperty.ColorEnable:
            // videoProcAmp.GetRange(VideoProcAmpProperty.ColorEnable, out min, out max, out steppingDelta, out defaultValue, out flags);
            break;

          case VideoProcAmpProperty.Gain:
            // videoProcAmp.GetRange(VideoProcAmpProperty.Gain, out min, out max, out steppingDelta, out defaultValue, out flags);
            break;

          case VideoProcAmpProperty.Gamma:
            // videoProcAmp.GetRange(VideoProcAmpProperty.Gamma, out min, out max, out steppingDelta, out defaultValue, out flags);
            break;

          case VideoProcAmpProperty.Hue:
            // videoProcAmp.GetRange(VideoProcAmpProperty.Hue, out min, out max, out steppingDelta, out defaultValue, out flags);
            break;

          case VideoProcAmpProperty.WhiteBalance:
            // videoProcAmp.GetRange(VideoProcAmpProperty.WhiteBalance, out min, out max, out steppingDelta, out defaultValue, out flags);
            break;

          case VideoProcAmpProperty.BacklightCompensation:
            // videoProcAmp.GetRange(VideoProcAmpProperty.BacklightCompensation, out min, out max, out steppingDelta, out defaultValue, out flags);
            break;
        }
      }
    }

    /// <summary>
    /// This method is used to set minimum and maximum values for the
    /// <see cref="CameraControlProperty"/>s of the capture device.
    /// </summary>
    private void SetCameraControlMinMaxRanges()
    {
      if (cameraControl == null)
      {
        return;
      }

      int min, max, steppingDelta, defaultValue;
      CameraControlFlags flags;

      foreach (CameraControlProperty item in Enum.GetValues(typeof(CameraControlProperty)))
      {
        switch (item)
        {
          case CameraControlProperty.Exposure:
            cameraControl.GetRange(CameraControlProperty.Exposure, out min, out max, out steppingDelta,
                                   out defaultValue, out flags);
            //Settings.Instance.Camera.ExposureMinimum = min;
            //Settings.Instance.Camera.ExposureMaximum = max;
            //Settings.Instance.Camera.ExposureStepping = steppingDelta;
            //Settings.Instance.Camera.ExposureDefault = defaultValue;
            break;
          case CameraControlProperty.Focus:
            cameraControl.GetRange(CameraControlProperty.Focus, out min, out max, out steppingDelta,
                                   out defaultValue, out flags);
            //Settings.Instance.Camera.FocusMinimum = min;
            //Settings.Instance.Camera.FocusMaximum = max;
            //Settings.Instance.Camera.FocusStepping = steppingDelta;
            //Settings.Instance.Camera.FocusDefault = defaultValue;
            break;
          case CameraControlProperty.Zoom:
            cameraControl.GetRange(CameraControlProperty.Focus, out min, out max, out steppingDelta,
                                   out defaultValue, out flags);
            //Settings.Instance.Camera.ZoomMinimum = min;
            //Settings.Instance.Camera.ZoomMaximum = max;
            //Settings.Instance.Camera.ZoomStepping = steppingDelta;
            //Settings.Instance.Camera.ZoomDefault = defaultValue;
            break;
          case CameraControlProperty.Iris:
            break;
          case CameraControlProperty.Pan:
            break;
          case CameraControlProperty.Roll:
            break;
          case CameraControlProperty.Tilt:
            break;
        }
      }
    }

    #endregion //PRIVATEMETHODS

  }
}