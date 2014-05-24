using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Interop;
using Emgu.CV;
using Emgu.CV.Structure;

using log4net;

namespace GTHardware.Cameras.Thorlabs
{
  using System.Windows;

  using GTCommons;

  using Point = System.Drawing.Point;
  using Size = System.Drawing.Size;

  public class ThorlabCamera : CameraBase
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

    public static readonly GTHardware.Camera.DeviceTypeEnum deviceType = GTHardware.Camera.DeviceTypeEnum.Thorlabs;

    private ThorlabDevice m_uc480;
    private ThorlabSettings settings;

    private Window messageDummyWindow;
    private HwndSource hwndSource;
    private IntPtr hwnd = (IntPtr)0;

    private string defaultParametersFile = "uc480.ini";
    private int cpuSpeedMHz = 0; // Limit ROI fps if cpu is too slow
    private int maxFPS = 250;
    private bool isSupportingROI = true;
    //private int lastfps = 0;
    //private bool hasTriedTwice = false;

    private int defaultWidth = 1280;
    private int defaultHeight = 1024;
    private int width = 1280;
    private int height = 1024;
    private int pitch;
    private int stride;
    private int bits;

    private const int IMAGE_COUNT = 1;
    private readonly string parametersFileStr;
    private bool isImageFormatSet;
    private UC480IMAGE[] m_Uc480Images;
    //private bool m_bDrawing = false;
    private bool m_bLive;

    private readonly BackgroundWorker workerInit = new BackgroundWorker();
    private BackgroundWorker workerGetImage = new BackgroundWorker();

    private struct UC480IMAGE
    {
      public int MemID;
      public int nSeqNum;
      public IntPtr pMemory;
    }

    #endregion


    #region Events

    #region Delegates

    //public delegate void FrameHandler();
    public delegate void ConnectedHandler();
    public delegate void DisconnectedHandler();

    #endregion

    //public event FrameHandler OnNewImage;
    public event ConnectedHandler OnConnect;
    public event DisconnectedHandler OnDisconnect;

    #endregion


    #region Constructor

    public ThorlabCamera()
    {
      // hwndSource must be set to addhook to messageloop for thorlab messages
      this.messageDummyWindow = new Window();
      this.messageDummyWindow.Width = 1;
      this.messageDummyWindow.Height = 1;
      this.messageDummyWindow.Left = 2000;
      this.messageDummyWindow.Show();

      var wih = new WindowInteropHelper(this.messageDummyWindow);
      this.hwndSource = HwndSource.FromHwnd(wih.Handle);
      if (this.hwndSource != null)
      {
        this.hwnd = this.hwndSource.Handle;
      }
      else
      {
        throw new Exception("HwndSource of Thorlabs=null");
      }

      //var curProc = Process.GetCurrentProcess();
      //this.hwndSource = HwndSource.FromHwnd(curProc.MainWindowHandle);
      //this.hwnd = this.hwndSource.Handle;

      // Settings file
      // string dir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase);
      var dir = GTPath.GetLocalApplicationDataPath();
      var dirInfo = new DirectoryInfo(new Uri(dir).LocalPath);
      this.parametersFileStr = dirInfo.FullName + Path.DirectorySeparatorChar + this.defaultParametersFile;

      this.m_uc480 = new ThorlabDevice();
      this.settings = new ThorlabSettings(this.m_uc480);

      this.Name = "Thorlabs UC480";

      this.DetermineMaxFPS();
    }

    #endregion


    #region Get/Set

    public ThorlabDevice Camera
    {
      get { return m_uc480; }
    }

    public ThorlabSettings Settings
    {
      get { return settings; }
      set { settings = value; }
    }

    public bool IsInitialized
    {
      get
      {
        if (m_uc480 == null)
          return false;
        else
          return true;
      }
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
      get { return defaultWidth; }
    }

    public override int DefaultHeight
    {
      get { return defaultHeight; }
    }

    //private double lastFPS = 0;

    public override int FPS
    {
      get
      {
        double fps = 0;
        m_uc480.GetFramesPerSecond(ref fps);

        return Convert.ToInt32(Math.Round(fps, 0));
      }
    }

    public override bool IsSupportingROI
    {
      get { return isSupportingROI; }
      set { isSupportingROI = value; }
    }

    public override bool IsROISet
    {
      get { return settings.IsROISet; }
    }

    public override bool IsSettingROI
    {
      get { return settings.IsSettingROI; }
    }

    public string GetDeviceName()
    {
      var camInfo = new ThorlabDevice.CAMINFO();

      if (m_uc480.GetCameraInfo(ref camInfo) == ThorlabDevice.IS_SUCCESS)
        return camInfo.id + "\t" + camInfo.Version + "\t" + camInfo.SerNo + "\t" + camInfo.Date;
      else
        return "UC480";
    }

    #endregion


    #region Static method isConnected()


    public static bool IsConnected()
    {
      bool isConnected = false;
      log.Debug("IsConnected: Attempting to retrieve list of connected UC480 cameras...");

      try
      {
        int number = 0;
        ThorlabDevice.GetNumberOfCameras(ref number);
        if (number > 0)
        {
          return true;
        }

        return false;

        //ThorlabDevice.UC480_CAMERA_LIST camList = new ThorlabDevice.UC480_CAMERA_LIST();
        //int list = tempCam.GetCameraList(ref camList);

        //// if camera count is larger than 0 there is a UC camera connected..
        //if (camList.dwCount > 0)
        //{
        //  log.Info("IsConnected: Detected " + camList.dwCount.ToString() + " UC480 camera(s) - ThorlabCamera.IsConnected will return 'true' - Thorlabs camera detected");
        //  isConnected = true;
        //}
        //else
        //{
        //  log.Info("IsConnected: Detected no UC480 camera(s) - ThorlabCamera.IsConnected will return 'false', i.e. No Thorlabs detected");
        //}

        //tempCam = null; // it's not started or anything, just a reference for the method call above
      }
      catch (Exception ex)
      {
        log.Warn("IsConnected: Exception occurred whilst trying to detect Thorlabs camera (" + ex.Message + "), IsConnected will return 'false', i.e. No Thorlabs detected. Stack trace is: " + ex.StackTrace);
      }

      return isConnected;
    }

    #endregion


    #region Inititialize and start camera

    public override bool Start()
    {
      // Must have the hwnd to start camera
      while (hwnd.ToInt32() == 0)
        Thread.Sleep(50);

      hwndSource.AddHook(WndProc);

      workerInit.DoWork += workerInit_DoWork;
      workerInit.RunWorkerCompleted += this.workerInit_RunWorkerCompleted;
      workerInit.RunWorkerAsync(hwndSource);

      return true; ///m_bLive;
    }

    public override bool Initialize()
    {
      // The initialization has been pushed to the Start() method as the camera needs the Hwnd window process pointer for messaging.

      //workerInit.DoWork += workerInit_DoWork;
      //workerInit.RunWorkerAsync(hwndSource);
      return true;
    }

    private void workerInit_DoWork(object sender, DoWorkEventArgs e)
    {
      InitCamera();
    }

    void workerInit_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
    {
      // Start live capture
      if (m_uc480.CaptureVideo(ThorlabDevice.IS_DONT_WAIT) == ThorlabDevice.IS_SUCCESS) // alt. IS_WAIT
        m_bLive = true;
      else
        m_bLive = false;

      workerGetImage = new BackgroundWorker();
      workerGetImage.WorkerReportsProgress = false;
      workerGetImage.WorkerSupportsCancellation = false;
      workerGetImage.DoWork += workerGetImage_DoWork;
    }

    private bool InitCamera()
    {
      m_uc480 = new ThorlabDevice();
      settings = new ThorlabSettings(m_uc480);

      m_Uc480Images = new UC480IMAGE[IMAGE_COUNT];

      if (m_uc480.InitCamera(1, hwnd.ToInt32()) != ThorlabDevice.IS_SUCCESS)
      {
        Console.Out.WriteLine("Thorlabs/UC480 Camera init failed");
        return false;
      }

      // Load camera configuration file (if you change settings in the DLL/Native, 
      // first build (Ctrl-Shift+B) so that it gets copied into the Bin folder)
      if (m_uc480.LoadParameters(parametersFileStr) != ThorlabDevice.IS_SUCCESS)
        Console.Out.WriteLine("UC480 Unable to load camera configuration file.");

      // Allocate memory for images
      AllocateImageMemory(settings.ROI.Width, settings.ROI.Height);

      // Set monochrome
      m_uc480.SetColorMode(ThorlabDevice.IS_SET_CM_Y8);
      //m_uc480.SetColorMode(ThorlabDevice.IS_SET_CM_BAYER);

      // enables on new frame event
      m_uc480.EnableMessage(ThorlabDevice.IS_FRAME, hwnd.ToInt32());
      m_uc480.EnableMessage(ThorlabDevice.IS_DEVICE_RECONNECTED, hwnd.ToInt32());
      m_uc480.EnableMessage(ThorlabDevice.IS_DEVICE_REMOVAL, hwnd.ToInt32());
      m_uc480.EnableMessage(ThorlabDevice.IS_DEVICE_REMOVED, hwnd.ToInt32());
      m_uc480.EnableMessage(ThorlabDevice.IS_DEVICE_RECONNECTED, hwnd.ToInt32());

      return m_bLive;
    }

    private void AllocateImageMemory(int w, int h)
    {
      m_uc480.ClearSequence();

      for (int i = 0; i < IMAGE_COUNT; i++)
      {
        // alloc memory
        m_uc480.AllocImageMem(w, h, 8, ref m_Uc480Images[i].pMemory, ref m_Uc480Images[i].MemID);
        // add our memory to the sequence
        m_uc480.AddToSequence(m_Uc480Images[i].pMemory, m_Uc480Images[i].MemID);
        // set sequence number
        m_Uc480Images[i].nSeqNum = i + 1;
      }
    }


    #endregion


    #region Stop and cleanup

    public override bool Stop()
    {
      m_uc480.StopLiveVideo(0);
      m_uc480.ExitCamera();
      return true;
    }

    public override void Cleanup()
    {
      m_uc480.StopLiveVideo(0);

      //for (int i = 0; i < IMAGE_COUNT; i++)
      //    Marshal.FreeCoTaskMem(m_Uc480Images[i].pMemory);

      m_uc480.ExitCamera();
    }

    #endregion


    #region New image (by WndProc msg)

    private delegate Image<Gray, byte> GrabImageDelegate();

    //private GrabImageDelegate dm;
    //private IAsyncResult result;

    public IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
    {
      this.hwnd = hwnd;

      // Listen for operating system messages
      switch (msg)
      {
        // Got a camera message
        case ThorlabDevice.IS_UC480_MESSAGE:

          switch (wParam.ToInt32())
          {
            case ThorlabDevice.IS_FRAME:
              {
                // loosing some frames due to busy worker, tried pooling but gets filled on heavy processing.. solution?

                //if (IsROISet && settings.FramesSinceROIChange > 5)
                //{
                //    // Running on the same thread is fast (capture rate) but lags when classifier is running..
                //    OnRaiseCustomEvent(new ImageEventArgs(GrabImage()));
                //}
                //else
                //{
                // BgWorker doesn't lag when running classifiers but takes more resources, losing 10% fps
                if (!workerGetImage.IsBusy)
                  workerGetImage.RunWorkerAsync(wParam.ToInt32().ToString());
                //}
              }
              break;

            case ThorlabDevice.IS_NEW_DEVICE:
              if (OnConnect != null)
                OnConnect();
              Console.Out.WriteLine("Thorlabs new device");
              break;

            case ThorlabDevice.IS_DEVICE_REMOVAL:
              if (OnDisconnect != null)
                OnDisconnect();
              Console.Out.WriteLine("Thorlabs disconnected");
              break;

            case ThorlabDevice.IS_DEVICE_RECONNECTED:
              if (OnConnect != null)
                OnConnect();
              Console.Out.WriteLine("Thorlabs re-connected");
              break;
          }

          handled = true;
          break;
      }
      return hwnd;
    }

    private void workerGetImage_DoWork(object sender, DoWorkEventArgs e)
    {
      OnRaiseCustomEvent(new ImageEventArgs(GrabImage()));
    }


    private Image<Gray, byte> GrabImage()
    {
      settings.FramesSinceROIChange++;
      //m_bDrawing = true;

      // draw Instance memory if a camera is opened
      if (m_uc480.IsOpen() && m_bLive == true)
      {
        int num = 0;
        IntPtr pMem = new IntPtr();
        IntPtr pLast = new IntPtr();

        // Get pointers to image
        m_uc480.GetActSeqBuf(ref num, ref pMem, ref pLast);

        // Pointer to image is null, return
        if (pLast.ToInt32() == 0)
        {
          //m_bDrawing = false;
          return new Image<Gray, byte>(1, 1);
        }

        // Get id's
        int nLastID = GetImageID(pLast);
        int nLastNum = GetImageNum(pLast);

        // Lock camera image memory
        m_uc480.LockSeqBuf(nLastNum, pLast);

        // Set image format
        if (isImageFormatSet == false)
        {
          m_uc480.InquireImageMem(pLast, nLastID, ref width, ref height, ref bits, ref pitch);
          stride = width;

          if (stride % 4 != 0)
            stride += (4 - (stride % 4));

          isImageFormatSet = false;
        }

        // Toggle ROI based on image size
        if (height != defaultHeight && width != defaultWidth)
        {
          settings.IsSettingROI = false;
          settings.IsROISet = true;
        }

        // Unlock camera image memory
        m_uc480.UnlockSeqBuf(nLastNum, pLast);

        // Create and return new image using the pointer pLast
        return new Image<Gray, byte>(settings.ROI.Width, settings.ROI.Height, stride, pLast);
      }
      return new Image<Gray, byte>(1, 1);
    }


    #endregion


    #region GetImageID/Number

    private int GetImageID(IntPtr pBuffer)
    {
      // get image id for a given memory
      if (!m_uc480.IsOpen())
        return 0;

      int i = 0;
      for (i = 0; i < IMAGE_COUNT; i++)
        if (m_Uc480Images[i].pMemory == pBuffer)
          return m_Uc480Images[i].MemID;
      return 0;
    }

    private int GetImageNum(IntPtr pBuffer)
    {
      // get number of sequence for a given memory
      if (!m_uc480.IsOpen())
        return 0;

      int i = 0;
      for (i = 0; i < IMAGE_COUNT; i++)
        if (m_Uc480Images[i].pMemory == pBuffer)
          return m_Uc480Images[i].nSeqNum;

      return 0;
    }

    #endregion


    #region FPS - Capture speed

    private void SetFPS(int fps)
    {
      double actualFPS = 0;

      // Limit FPS based on CPU speed
      if (fps > maxFPS)
        fps = maxFPS;

      if (m_uc480.SetFrameRate(fps, ref actualFPS) != ThorlabDevice.IS_SUCCESS)
        Console.Out.WriteLine("UC480 failed to set fps.");
    }

    private void DetermineMaxFPS()
    {
      maxFPS = 500; // default high value, might not be able to reach this no matter how fast cpu (camera limited)

      cpuSpeedMHz = CheckCPUSpeed();

      if (cpuSpeedMHz < 2400)
        maxFPS = 160;
      if (cpuSpeedMHz < 2000)
        maxFPS = 120;
      if (cpuSpeedMHz < 1800)
        maxFPS = 100;

      //// Check for weaker Atoms
      //if(processorNameString != null && processorNameString.Length > 5)
      //    if(processorNameString.ToLower().Contains("atom"))
      //       maxFPS = 50; // whats the capability of Intel Atoms? 
    }

    //private string processorNameString;

    private int CheckCPUSpeed()
    {
      try
      {
        Microsoft.Win32.RegistryKey registrykeyHKLM = Microsoft.Win32.Registry.LocalMachine;
        string keyPath = @"HARDWARE\DESCRIPTION\System\CentralProcessor\0";
        Microsoft.Win32.RegistryKey registrykeyCPU = registrykeyHKLM.OpenSubKey(keyPath, false);
        string MHz = registrykeyCPU.GetValue("~MHz").ToString();
        string processorNameString = (string)registrykeyCPU.GetValue("ProcessorNameString");
        registrykeyCPU.Close();
        registrykeyHKLM.Close();
        return (Convert.ToInt32(MHz));
      }
      catch (Exception)
      {
        return 0;
      }
    }

    #endregion


    #region ROI - Set/Clear

    public override Rectangle SetROI(Rectangle newRoi)
    {
      return settings.SetROI(newRoi);
    }

    public override Rectangle GetROI()
    {
      return settings.GetROI();
    }

    public override void ClearROI()
    {
      settings.SetROI(new Rectangle(new Point(0, 0), new Size(1280, 1024)));

    }

    #endregion

  }
}