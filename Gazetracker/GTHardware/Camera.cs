using System;
using System.Linq;

using GTHardware.Cameras.DirectShow;
//using GTHardware.Cameras.Thorlabs;
using GTHardware.Cameras.Kinect;

namespace GTHardware
{
  public class Camera
  {

    #region Variables

    private static Camera instance;
    private CameraBase camera;
    //private HwndSource hwndSource;
    private readonly DeviceTypeEnum deviceType;

    public enum DeviceTypeEnum
    {
      None = 0,
      DirectShow = 1,
      Thorlabs = 2,
      Kinect = 4,
      FileStream = 8
    }

    #endregion

    #region Constructor / Init

    private Camera()
    {
      // Check connected device
      deviceType = GetConnectedDevice();

      // Create camera subclass object, inherits from CameraBase
      switch (deviceType)
      {
        //case DeviceTypeEnum.Thorlabs:
        //  camera = new ThorlabCamera(); // needs hwndSource to work, set by this.HwndSource property
        //  break;
        //case DeviceTypeEnum.PS3Eye:
        //  camera = new PS3Camera();
        //  break;
        case DeviceTypeEnum.Kinect:
          camera = new KinectCamera();
          break;
        case DeviceTypeEnum.DirectShow:
          camera = new DirectShowCamera();
          break;

      }
    }

    public static DeviceTypeEnum GetConnectedDevice()
    {
      // In GUI, this will error message to be shown and then exits app
      DeviceTypeEnum connectedDevice = DeviceTypeEnum.None;

      // Check which cameras are connected, 
      // Set the deviceType which is used to initialize the camera object

      //if (ThorlabCamera.IsConnected())
      //{
      //  connectedDevice = DeviceTypeEnum.Thorlabs;
      //}
      //else if (!DisablePs3Camera)
      //{
      //  try
      //  {
      //    if (PS3Camera.CameraCount > 0)
      //    {
      //      connectedDevice = DeviceTypeEnum.PS3Eye;
      //    }
      //  }
      //  catch (Exception)
      //  {
      //    DisablePs3Camera = true;
      //  }
      //}
      //else if (KinectCamera.IsConnected())
      //    deviceType = DeviceTypeEnum.Kinect;

      if (connectedDevice == DeviceTypeEnum.None && DirectShowDevices.Instance.Cameras.Any())
      {
        // Fallback to DirectShow drivers
        connectedDevice = DeviceTypeEnum.DirectShow;
      }

      return connectedDevice;
    }

    #endregion

    #region Get/Set

    public static Camera Instance
    {
      get
      {
        return instance ?? (instance = new Camera());
      }
    }

    public CameraBase Device
    {
      get { return camera; }
    }

    public DeviceTypeEnum DeviceType
    {
      get { return deviceType; }
    }

    public int Width
    {
      get
      {

        if (camera == null)
          return 0;
        return camera.Width;
      }
    }

    public int Height
    {
      get
      {
        if (camera == null)
          return 0;
        return camera.Height;
      }
    }

    public int DefaultWidth
    {
      get
      {
        if (camera == null)
          return 0;
        return camera.DefaultWidth;
      }
    }

    public int DefaultHeight
    {
      get
      {
        if (camera == null)
          return 0;
        return camera.DefaultHeight;
      }
    }


    public int FPS
    {
      get
      {
        if (camera == null)
          return 0;
        return camera.FPS;
      }
    }

    public System.Drawing.Rectangle ROI
    {
      get
      {
        if (camera == null)
          return new System.Drawing.Rectangle(0, 0, 0, 0);

        return camera.GetROI();
      }
    }

    #endregion

    #region Public methods

    public void SetDirectShowCamera(int newDeviceNumber, int newDeviceMode)
    {
      try
      {
        bool initializeNew = true;
        DirectShowCamera localCam; // we store new camera here and then transfer it to this.camera on success

        // If no device mode was select use default
        if (newDeviceMode == -1)
          newDeviceMode = 0;

        // Get Instance camera and mode
        if (camera != null && camera is DirectShowCamera)
        {
          var dCam = camera as DirectShowCamera;

          // Store Instance mode to reset if new mode fails
          var instanceDeviceNumber = dCam.DeviceNumber;
          var instanceDeviceMode = dCam.DeviceMode;

          // Dont init the same device & mode twice..
          if (instanceDeviceNumber == newDeviceNumber
              && instanceDeviceMode == newDeviceMode
              && dCam.HasValidGraph)
            initializeNew = false;
        }


        // Specific deviceMode (e.g. 800x600 @ 30fps)
        if (camera == null || newDeviceMode > -1)
        {
          if (initializeNew)
          {
            if (camera != null)
            {
              camera.Stop();
              camera.Cleanup();
            }
          }
          else
          {
            // No changes, just return
            return;
          }
        }

        // Specific configuration
        localCam = new DirectShowCamera(
            DirectShowDevices.Instance.Cameras[newDeviceNumber].DirectshowDevice,
            newDeviceNumber,
            newDeviceMode,
            DirectShowDevices.Instance.Cameras[newDeviceNumber].SupportedSizesAndFPS[newDeviceMode].FPS,
            DirectShowDevices.Instance.Cameras[newDeviceNumber].SupportedSizesAndFPS[newDeviceMode].Width,
            DirectShowDevices.Instance.Cameras[newDeviceNumber].SupportedSizesAndFPS[newDeviceMode].Height);

        //  If last set camera failed, try default mode
        if (localCam.HasValidGraph == false) // No specific deviceMode
        {
          localCam.Cleanup();
          localCam = new DirectShowCamera(
              DirectShowDevices.Instance.Cameras[newDeviceNumber].DirectshowDevice,
              newDeviceNumber,
              0,
              0,
              0,
              0);
        }

        //  If this is already occupied by another application and there
        //  are more than one camera connected, try the next one.
        if (localCam.HasValidGraph == false && DirectShowDevices.Instance.Cameras.Count() > 1)
        {
          localCam.Cleanup();
          localCam = new DirectShowCamera(
              DirectShowDevices.Instance.Cameras[1].DirectshowDevice, 1, 0, 0, 0, 0);
        }

        // Finally, if running set this.camera to localCam
        if (localCam.HasValidGraph)
        {
          localCam.IsSupportingROI = false;

          // Switch
          camera = localCam;
          camera.Initialize();
          camera.Start();

          // Trigger global command, signal that a new camera has been started 
          // Listners for OnImage event has to re-subscribe
          GTCommons.GTCommands.Instance.Camera.CameraChange();
        }
        else
        {
          // Failed, start previous camera/mode again
          camera.Start();
        }
      }
      catch (Exception)
      {
        //ErrorLogger.ProcessException(ex, false);
      }
    }


    #endregion

  }
}
