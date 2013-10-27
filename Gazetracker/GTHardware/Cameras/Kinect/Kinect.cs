using System;
using System.Drawing;
using System.IO;
using System.Threading;
using Emgu.CV;
using Emgu.CV.Structure;

namespace GTHardware.Cameras.Kinect
{
  using GTCommons;

  /// <summary>
  /// Disabled cause it uses old OpenNI without x64 support
  /// Please udate to OpenNI2 !
  /// </summary>
  public class KinectCamera : CameraBase
  {

    #region Variables

    public static readonly GTHardware.Camera.DeviceTypeEnum deviceType = GTHardware.Camera.DeviceTypeEnum.Kinect;

    private static readonly string configFile = @"kinect.xml";

    //private Context context;
    //private IRGenerator ir;
    private Thread readerThread;
    private bool shouldRun;
    private Image<Gray, UInt16> grayImage;

    private Rectangle roi;
    private bool isSupportingROI = false;
    //private bool isConnected = false;
    private bool isRoiSet = false;
    private int fps = 0;
    private int defaultWidth;
    private int defaultHeight;
    private int width = 0;
    private int height = 0;

    #endregion

    #region Events

    #region Delegates

    public delegate void FrameCapHandler();

    #endregion

    public event FrameCapHandler FrameCaptureComplete;

    #endregion

    #region Constructor

    public KinectCamera()
    {
    }

    #endregion

    #region Public static IsConnected()

    public static bool IsConnected()
    {
      bool isConnected = false;

      try
      {
        // string dir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase);
        var dir = GTPath.GetLocalApplicationDataPath();
        DirectoryInfo dirInfo = new DirectoryInfo(new Uri(dir).LocalPath);
        //Context ctx = new Context(dirInfo.FullName + "\\" + configFile);
        //ProductionNode irNode = ctx.FindExistingNode(NodeType.IR) as IRGenerator;

        //if (irNode != null)
        //  isConnected = true;

        //irNode.Dispose();
        //ctx.Dispose();

      }
      catch (Exception)
      {
        isConnected = false;
      }

      return isConnected;
    }



    #endregion

    #region Get/Set

    public Image<Gray, byte> VideoImage
    {
      get
      {
        Image<Gray, byte> img2 = grayImage.Copy().Convert<Gray, byte>();
        return img2;
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

    public override int FPS
    {
      get { return fps; }
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


    #endregion

    #region Public methods

    public override bool Initialize()
    {
      // string dir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase);
      var dir = GTPath.GetLocalApplicationDataPath();
      DirectoryInfo dirInfo = new DirectoryInfo(new Uri(dir).LocalPath);
      //this.context = new Context(dirInfo.FullName + Path.DirectorySeparatorChar + configFile);

      //this.ir = context.FindExistingNode(NodeType.IR) as IRGenerator;

      //if (this.ir != null)
      //  return true;
      //else
        return false;
    }

    public override bool Start()
    {
      bool success;
      try
      {
        this.shouldRun = true;
        this.readerThread = new Thread(ReaderThread);
        this.readerThread.Start();
        success = true;
      }
      catch (Exception)
      {
        success = false;
      }

      return success;
    }

    public override bool Stop()
    {
      try
      {
        this.shouldRun = false;
        this.readerThread.Join();
      }
      catch (Exception)
      {
        return false;
      }

      return true;
    }

    public override void Cleanup()
    {
      this.Stop();
      //this.ir.StopGenerating();
      //this.ir.Dispose();
    }

    public override Rectangle SetROI(Rectangle newRoi)
    {
      //Cropping croppingROI = new Cropping();
      //roi.bEnabled = true;
      //roi.nXOffset = 1;
      //roi.nXSize = 640;
      //roi.nYOffset = 1;
      //roi.nYSize = 480;

      //ir.GetCroppingCap().SetCropping(ref roi);

      roi = newRoi;
      return roi;
    }

    public override Rectangle GetROI()
    {
      return roi;
    }

    public override void ClearROI()
    {
      //Cropping croppingROI = new Cropping();
      //roi.bEnabled = true;
      //roi.nXOffset = 1;
      //roi.nXSize = 640;
      //roi.nYOffset = 1;
      //roi.nYSize = 480;

      //ir.GetCroppingCap().SetCropping(ref roi);
    }


    #endregion

    #region Private methods

    private unsafe void ReaderThread()
    {
      //IRMetaData irMD = new IRMetaData();

      //while (this.shouldRun)
      //{
      //  try
      //  {
      //    this.context.WaitOneUpdateAll(this.ir);
      //  }
      //  catch (Exception)
      //  {
      //  }

      //  this.ir.GetMetaData(irMD);
      //  MapData<ushort> irMap = this.ir.GetIRMap();

      //  if (isRoiSet == false)
      //  {
      //    defaultWidth = irMap.XRes;
      //    defaultHeight = irMap.YRes;
      //  }

      //  int w = irMap.XRes;
      //  int h = irMap.YRes;

      //  lock (this)
      //  {
      //    int stride = w * 2;

      //    if (stride % 2 != 0)
      //      stride += (2 - (stride % 2));


      //    if (roi.Size.Width != 0)
      //    {
      //      Emgu.CV.Image<Gray, UInt16> tmp = new Emgu.CV.Image<Gray, UInt16>(irMap.XRes, irMap.YRes, stride, ir.GetIRMapPtr());
      //      tmp.ROI = new Rectangle(roi.X, roi.Y, roi.Width, roi.Height);
      //      grayImage = tmp.Copy();
      //    }
      //    else
      //    {
      //      grayImage = new Emgu.CV.Image<Gray, UInt16>(w, h, stride, ir.GetIRMapPtr());
      //    }
      //  }

      //  if (FrameCaptureComplete != null)
      //    FrameCaptureComplete();
      //}
    }

    #endregion

  }
}
