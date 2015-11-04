using System;
using System.Drawing;
using AForge.Video;
using AForge.Video.DirectShow;
using Emgu.CV;
using Emgu.CV.Structure;
using log4net;

namespace GTHardware.Cameras.FileStream
{
  public class FileStreamCamera : CameraBase
  {
    #region Logging Setup

    /// <summary>
    /// Logger for log4net logging
    /// </summary>
    private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    /// <summary>
    /// Indicator that can be used for high speed DEBUG level logging. Note that using this flag will prevent automated reload
    /// of log4net configuration for that log statement during program operation
    /// </summary>
    private static readonly bool isDebugEnabled = log.IsDebugEnabled;

    #endregion

    #region Variables

    public static readonly GTHardware.Camera.DeviceTypeEnum deviceType = GTHardware.Camera.DeviceTypeEnum.FileStream;

    private Rectangle _roi;
    private bool isSupportingROI = true;
    private bool isSettingRoi = false;
    private bool isRoiSet = false;

    private static object InstanceImageDispose = new object();
    private Image<Gray, byte> InstanceGray;

    private int defaultWidth = 1280;
    private int defaultHeight = 1024;
    private int width = 1280;
    private int height = 1024;
    private string videoFilename = "";
    private AsyncVideoSource videoSource; // Implements IVideoSource

    #endregion

    #region Constructor

    public FileStreamCamera(string videoFilename, bool skipFramesIfBusy)
    {
      ClearROI();

      this.videoFilename = videoFilename;
      videoSource = new AsyncVideoSource(new FileVideoSource(videoFilename));
      videoSource.SkipFramesIfBusy = skipFramesIfBusy;

      if (videoSource != null)
      {
        videoSource.NewFrame += new NewFrameEventHandler(videoSource_NewFrame);
        videoSource.VideoSourceError += new VideoSourceErrorEventHandler(videoSource_VideoSourceError);
        videoSource.PlayingFinished += new PlayingFinishedEventHandler(videoSource_PlayingFinished);
      }
    }

    #endregion


    #region Get/Set

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
      get { return defaultWidth; }
    }

    public override int DefaultHeight
    {
      get { return defaultHeight; }
    }

    public override int FPS
    {
      get
      {
        double fps = 0;
        // TODO
        // m_uc480.GetFramesPerSecond(ref fps);
        return Convert.ToInt32(fps);
      }
    }


    public Image<Gray, byte> Image
    {
      get
      {
        Image<Gray, byte> returnImage = InstanceGray.Copy();
        return returnImage;
      }
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
      get { return isSettingRoi; }
    }

    public string GetDeviceName()
    {
      return "FileStreamCamera: " + videoFilename;
    }

    #endregion


    #region Inititialize and start camera

    public override bool Start()
    {
      videoSource.Start();
      return true;
    }

    public override bool Initialize()
    {
      // The initialization has been pushed to the Start() method as the camera needs the Hwnd window process pointer for messaging.
      return true;
    }

    #endregion


    #region Stop and cleanup

    public override bool Stop()
    {
      videoSource.Stop();
      return true;
    }

    public override void Cleanup()
    {
      videoSource = null;
    }

    #endregion

    #region ROI - Set/Clear

    public override Rectangle SetROI(Rectangle newRoi)
    {
      _roi = newRoi;
      return newRoi;
    }

    public override Rectangle GetROI()
    {
      return _roi;
    }

    public override void ClearROI()
    {
      SetROI(new Rectangle(new Point(0, 0), new Size(defaultHeight, defaultWidth)));
    }

    #endregion

    #region Underlying video feed events

    void videoSource_NewFrame(object sender, AForge.Video.NewFrameEventArgs eventArgs)
    {
      log.Debug("videoSource_NewFrame: Received a frame from the File video source");

      lock (InstanceImageDispose)
      {
        // log.Debug("videoSource_NewFrame: Considering disposal of previous Images");
        if (InstanceGray != null)
        {
          // log.Debug("videoSource_NewFrame: Disposing of previous Gray frame");
          InstanceGray.Dispose();
          InstanceGray = null;
        }
        else
        {
          // log.Debug("videoSource_NewFrame: No InstanceGray exists, no need to Dispose of it");
        }
      }

      log.Debug("videoSource_NewFrame: Converting to a new Gray");
      // InstanceGray = new Image<Gray, byte>((Bitmap)eventArgs.Frame.Clone());
      InstanceGray = new Image<Gray, byte>((Bitmap)eventArgs.Frame);
      log.Debug("videoSource_NewFrame: Finished converting to a new Gray");

      // notify about the new frame
      log.Debug("videoSource_NewFrame: Built Raising NewFrame event");
      ImageEventArgs imageEventArgs = new ImageEventArgs(InstanceGray);
      OnRaiseCustomEvent(imageEventArgs);
      log.Debug("videoSource_NewFrame: Event complete");
    }

    void videoSource_PlayingFinished(object sender, ReasonToFinishPlaying reason)
    {
      log.Info("videoSource_PlayingFinished: Event occurred");
    }

    void videoSource_VideoSourceError(object sender, VideoSourceErrorEventArgs eventArgs)
    {
      log.Warn("videoSource_VideoSourceError: Event occurred");
    }

    #endregion

  }

}