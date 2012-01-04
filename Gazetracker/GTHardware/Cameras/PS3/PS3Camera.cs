using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Windows.Interop;
using System.Threading;
using System.Windows.Media.Imaging;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Media;
using Emgu.CV;
using Emgu.CV.Structure;
using System.IO;

namespace GTHardware.Cameras.PS3Eye
{
    #region Copyright notice 

    // This implementation relies on Codelaboratories SDK for the Sony Playstation Eye Camera.
    // The code used to capture the image is a slighly modfied version of the:
    // WPF C# CLEyeMulticamWPFTest Sample Application distributed with the CL SDK
    // 
    // All credits goes to Code Laboratories and Alex Popovich
    // http://codelaboratories.com
    //
    // Note: We are using a bare minimum of the functionality, potentially we could utilize 
    // calibrated stereo setups (original purpose of the SDK). This requires obtaining licences 
    // but offers interesting possibilities!


    #endregion

    #region Enums Camera Parameters

    // camera color mode
    public enum CLEyeCameraColorMode
    {
        CLEYE_MONO_PROCESSED,
        CLEYE_COLOR_PROCESSED,
        CLEYE_MONO_RAW,
        CLEYE_COLOR_RAW,
        CLEYE_BAYER_RAW
    };

    // camera resolution
    public enum CLEyeCameraResolution
    {
        CLEYE_QVGA,
        CLEYE_VGA
    };

    // camera parameters
    public enum CLEyeCameraParameter
    {
        // camera sensor parameters
        CLEYE_AUTO_GAIN,			// [false, true]
        CLEYE_GAIN,					// [0, 79]
        CLEYE_AUTO_EXPOSURE,		// [false, true]
        CLEYE_EXPOSURE,				// [0, 511]
        CLEYE_AUTO_WHITEBALANCE,	// [false, true]
        CLEYE_WHITEBALANCE_RED,		// [0, 255]
        CLEYE_WHITEBALANCE_GREEN,	// [0, 255]
        CLEYE_WHITEBALANCE_BLUE,	// [0, 255]
        // camera linear transform parameters
        CLEYE_HFLIP,				// [false, true]
        CLEYE_VFLIP,				// [false, true]
        CLEYE_HKEYSTONE,			// [-500, 500]
        CLEYE_VKEYSTONE,			// [-500, 500]
        CLEYE_XOFFSET,				// [-500, 500]
        CLEYE_YOFFSET,				// [-500, 500]
        CLEYE_ROTATION,				// [-500, 500]
        CLEYE_ZOOM,					// [-500, 500]
        // camera non-linear transform parameters
        CLEYE_LENSCORRECTION1,		// [-500, 500]
        CLEYE_LENSCORRECTION2,		// [-500, 500]
        CLEYE_LENSCORRECTION3,		// [-500, 500]
        CLEYE_LENSBRIGHTNESS		// [-500, 500]
    };
    
    #endregion
  
    public class PS3Camera : CameraBase, IDisposable
    {
        #region Driver imports (CLEyeMulticam)
        [DllImport("CLEyeMulticam.dll")]
        public static extern int CLEyeGetCameraCount();
        [DllImport("CLEyeMulticam.dll")]
        public static extern Guid CLEyeGetCameraUUID(int camId);
        [DllImport("CLEyeMulticam.dll")]
        public static extern IntPtr CLEyeCreateCamera(Guid camUUID, CLEyeCameraColorMode mode, CLEyeCameraResolution res, float frameRate);
        [DllImport("CLEyeMulticam.dll")]
        public static extern bool CLEyeDestroyCamera(IntPtr camera);
        [DllImport("CLEyeMulticam.dll")]
        public static extern bool CLEyeCameraStart(IntPtr camera);
        [DllImport("CLEyeMulticam.dll")]
        public static extern bool CLEyeCameraStop(IntPtr camera);
        [DllImport("CLEyeMulticam.dll")]
        public static extern bool CLEyeCameraLED(IntPtr camera, bool on);
        [DllImport("CLEyeMulticam.dll")]
        public static extern bool CLEyeSetCameraParameter(IntPtr camera, CLEyeCameraParameter param, int value);
        [DllImport("CLEyeMulticam.dll")]
        public static extern int CLEyeGetCameraParameter(IntPtr camera, CLEyeCameraParameter param);
        [DllImport("CLEyeMulticam.dll")]
        public static extern bool CLEyeCameraGetFrameDimensions(IntPtr camera, ref int width, ref int height);
        [DllImport("CLEyeMulticam.dll")]
        public static extern bool CLEyeCameraGetFrame(IntPtr camera, IntPtr pData, int waitTimeout);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern IntPtr CreateFileMapping(IntPtr hFile, IntPtr lpFileMappingAttributes, uint flProtect, uint dwMaximumSizeHigh, uint dwMaximumSizeLow, string lpName);
        [DllImport("kernel32.dll", SetLastError = true)]
        static extern IntPtr MapViewOfFile(IntPtr hFileMappingObject, uint dwDesiredAccess, uint dwFileOffsetHigh, uint dwFileOffsetLow, uint dwNumberOfBytesToMap);
        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool UnmapViewOfFile(IntPtr hMap);
        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool CloseHandle(IntPtr hHandle);
        #endregion

        #region Variables

        private IntPtr _map = IntPtr.Zero;
        private IntPtr _section = IntPtr.Zero;
        private IntPtr _camera = IntPtr.Zero;
        private int w = 0, h = 0;
        private bool _running;
        private Thread _workerThread;

        #endregion

        #region Get/Set properties

        public float Framerate{ get; set; }
        public CLEyeCameraColorMode ColorMode{ get; set; }
        public CLEyeCameraResolution Resolution{ get; set; }

        public bool AutoGain 
        {
            get
            {
                if (_camera == null)    return false;
                return CLEyeGetCameraParameter(_camera, CLEyeCameraParameter.CLEYE_AUTO_GAIN) != 0;
            }
            set
            {
                if (_camera == null)    return;
                CLEyeSetCameraParameter(_camera, CLEyeCameraParameter.CLEYE_AUTO_GAIN, value ? 1 : 0);
            }
        }
        public int Gain
        {
            get
            {
                if (_camera == null) return 0;
                return CLEyeGetCameraParameter(_camera, CLEyeCameraParameter.CLEYE_GAIN);
            }
            set
            {
                if (_camera == null) return;
                CLEyeSetCameraParameter(_camera, CLEyeCameraParameter.CLEYE_GAIN, value);
            }
        }
        public bool AutoExposure
        {
            get
            {
                if (_camera == null) return false;
                return CLEyeGetCameraParameter(_camera, CLEyeCameraParameter.CLEYE_AUTO_EXPOSURE) != 0;
            }
            set
            {
                if (_camera == null) return;
                CLEyeSetCameraParameter(_camera, CLEyeCameraParameter.CLEYE_AUTO_EXPOSURE, value ? 1 : 0);
            }
        }
        public int Exposure
        {
            get
            {
                if (_camera == null) return 0;
                return CLEyeGetCameraParameter(_camera, CLEyeCameraParameter.CLEYE_EXPOSURE);
            }
            set
            {
                if (_camera == null) return;
                CLEyeSetCameraParameter(_camera, CLEyeCameraParameter.CLEYE_EXPOSURE, value);
            }
        }
        public bool AutoWhiteBalance
        {
            get
            {
                if (_camera == null) return true;
                return CLEyeGetCameraParameter(_camera, CLEyeCameraParameter.CLEYE_AUTO_WHITEBALANCE) != 0;
            }
            set
            {
                if (_camera == null) return;
                CLEyeSetCameraParameter(_camera, CLEyeCameraParameter.CLEYE_AUTO_WHITEBALANCE, value ? 1 : 0);
            }
        }
        public int WhiteBalanceRed
        {
            get
            {
                if (_camera == null) return 0;
                return CLEyeGetCameraParameter(_camera, CLEyeCameraParameter.CLEYE_WHITEBALANCE_RED);
            }
            set
            {
                if (_camera == null) return;
                CLEyeSetCameraParameter(_camera, CLEyeCameraParameter.CLEYE_WHITEBALANCE_RED, value);
            }
        }
        public int WhiteBalanceGreen
        {
            get
            {
                if (_camera == null) return 0;
                return CLEyeGetCameraParameter(_camera, CLEyeCameraParameter.CLEYE_WHITEBALANCE_GREEN);
            }
            set
            {
                if (_camera == null) return;
                CLEyeSetCameraParameter(_camera, CLEyeCameraParameter.CLEYE_WHITEBALANCE_GREEN, value);
            }
        }
        public int WhiteBalanceBlue
        {
            get
            {
                if (_camera == null) return 0;
                return CLEyeGetCameraParameter(_camera, CLEyeCameraParameter.CLEYE_WHITEBALANCE_BLUE);
            }
            set
            {
                if (_camera == null) return;
                CLEyeSetCameraParameter(_camera, CLEyeCameraParameter.CLEYE_WHITEBALANCE_BLUE, value);
            }
        }
        public bool HorizontalFlip
        {
            get
            {
                if (_camera == null) return false;
                return CLEyeGetCameraParameter(_camera, CLEyeCameraParameter.CLEYE_HFLIP) != 0;
            }
            set
            {
                if (_camera == null) return;
                CLEyeSetCameraParameter(_camera, CLEyeCameraParameter.CLEYE_HFLIP, value ? 1 : 0);
            }
        }
        public bool VerticalFlip
        {
            get
            {
                if (_camera == null) return false;
                return CLEyeGetCameraParameter(_camera, CLEyeCameraParameter.CLEYE_VFLIP) != 0;
            }
            set
            {
                if (_camera == null) return;
                CLEyeSetCameraParameter(_camera, CLEyeCameraParameter.CLEYE_VFLIP, value ? 1 : 0);
            }
        }
        public int HorizontalKeystone
        {
            get
            {
                if (_camera == null) return 0;
                return CLEyeGetCameraParameter(_camera, CLEyeCameraParameter.CLEYE_HKEYSTONE);
            }
            set
            {
                if (_camera == null) return;
                CLEyeSetCameraParameter(_camera, CLEyeCameraParameter.CLEYE_HKEYSTONE, value);
            }
        }
        public int VerticalKeystone
        {
            get
            {
                if (_camera == null) return 0;
                return CLEyeGetCameraParameter(_camera, CLEyeCameraParameter.CLEYE_VKEYSTONE);
            }
            set
            {
                if (_camera == null) return;
                CLEyeSetCameraParameter(_camera, CLEyeCameraParameter.CLEYE_VKEYSTONE, value);
            }
        }
        public int XOffset
        {
            get
            {
                if (_camera == null) return 0;
                return CLEyeGetCameraParameter(_camera, CLEyeCameraParameter.CLEYE_XOFFSET);
            }
            set
            {
                if (_camera == null) return;
                CLEyeSetCameraParameter(_camera, CLEyeCameraParameter.CLEYE_XOFFSET, value);
            }
        }
        public int YOffset
        {
            get
            {
                if (_camera == null) return 0;
                return CLEyeGetCameraParameter(_camera, CLEyeCameraParameter.CLEYE_YOFFSET);
            }
            set
            {
                if (_camera == null) return;
                CLEyeSetCameraParameter(_camera, CLEyeCameraParameter.CLEYE_YOFFSET, value);
            }
        }
        public int Rotation
        {
            get
            {
                if (_camera == null) return 0;
                return CLEyeGetCameraParameter(_camera, CLEyeCameraParameter.CLEYE_ROTATION);
            }
            set
            {
                if (_camera == null) return;
                CLEyeSetCameraParameter(_camera, CLEyeCameraParameter.CLEYE_ROTATION, value);
            }
        }
        public int Zoom
        {
            get
            {
                if (_camera == null) return 0;
                return CLEyeGetCameraParameter(_camera, CLEyeCameraParameter.CLEYE_ZOOM);
            }
            set
            {
                if (_camera == null) return;
                CLEyeSetCameraParameter(_camera, CLEyeCameraParameter.CLEYE_ZOOM, value);
            }
        }
        public int LensCorrection1
        {
            get
            {
                if (_camera == null) return 0;
                return CLEyeGetCameraParameter(_camera, CLEyeCameraParameter.CLEYE_LENSCORRECTION1);
            }
            set
            {
                if (_camera == null) return;
                CLEyeSetCameraParameter(_camera, CLEyeCameraParameter.CLEYE_LENSCORRECTION1, value);
            }
        }
        public int LensCorrection2
        {
            get
            {
                if (_camera == null) return 0;
                return CLEyeGetCameraParameter(_camera, CLEyeCameraParameter.CLEYE_LENSCORRECTION2);
            }
            set
            {
                if (_camera == null) return;
                CLEyeSetCameraParameter(_camera, CLEyeCameraParameter.CLEYE_LENSCORRECTION2, value);
            }
        }
        public int LensCorrection3
        {
            get
            {
                if (_camera == null) return 0;
                return CLEyeGetCameraParameter(_camera, CLEyeCameraParameter.CLEYE_LENSCORRECTION3);
            }
            set
            {
                if (_camera == null) return;
                CLEyeSetCameraParameter(_camera, CLEyeCameraParameter.CLEYE_LENSCORRECTION3, value);
            }
        }
        public int LensBrightness
        {
            get
            {
                if (_camera == null) return 0;
                return CLEyeGetCameraParameter(_camera, CLEyeCameraParameter.CLEYE_LENSBRIGHTNESS);
            }
            set
            {
                if (_camera == null) return;
                CLEyeSetCameraParameter(_camera, CLEyeCameraParameter.CLEYE_LENSBRIGHTNESS, value);
            }
        }
        #endregion

        #region Static properties

        public static int CameraCount 
        { 
            get { return CLEyeGetCameraCount(); } 
        }

        public static Guid CameraUUID(int idx)
        { 
            return CLEyeGetCameraUUID(idx); 
        }
        #endregion

        #region Constructor/Destuctor/Dispose

        public PS3Camera()
        {
            Framerate = 75;
            ColorMode = CLEyeCameraColorMode.CLEYE_MONO_PROCESSED;
            Resolution = CLEyeCameraResolution.CLEYE_VGA;
        }

        ~PS3Camera()
        {
            // Finalizer calls Dispose(false)
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected void Dispose(bool disposing)
        {
            if (disposing)
            {
                // free managed resources
                Stop();
            }
            // free native resources if there are any.
            Destroy();
        }

    #endregion

        #region Public methods

        public bool Create(Guid cameraGuid)
        {
            // Create camera
            _camera = CLEyeCreateCamera(cameraGuid, ColorMode, Resolution, Framerate);
            
            if (_camera == IntPtr.Zero) 
                return false;
            
            // Get width and hight of the images
            CLEyeCameraGetFrameDimensions(_camera, ref w, ref h);
            uint imageSize = (uint)w * (uint)h;

            // create memory section and map
            _section = CreateFileMapping(new IntPtr(-1), IntPtr.Zero, 0x04, 0, imageSize, null);
            _map = MapViewOfFile(_section, 0xF001F, 0, 0, imageSize);

            return true;
        }

        public void Destroy()
        {
            if (_map != IntPtr.Zero)
            {
                UnmapViewOfFile(_map);
                _map = IntPtr.Zero;
            }
            if (_section != IntPtr.Zero)
            {
                CloseHandle(_section);
                _section = IntPtr.Zero;
            }
        }

        public override bool Start()
        {
            _running = true;
            _workerThread = new Thread(new ThreadStart(CaptureThread));
            _workerThread.Start();
            return true;
        }

        public override bool Stop()
        {
            if (!_running) return false;

            _running = false;
            _workerThread.Join(1000);
            return true;
        }

        #endregion

        #region Private methods

        private void CaptureThread()
        {
            CLEyeCameraStart(_camera);
            int i = 0;

            while (_running)
            {
                if (CLEyeCameraGetFrame(_camera, _map, 300))
                {
                    if (!_running)  break;

                    if (_map != IntPtr.Zero)
                        OnRaiseCustomEvent(new ImageEventArgs(new Emgu.CV.Image<Gray, byte>(w, h, w, _map)));

                    i++;
                }
            }
            CLEyeCameraStop(_camera);
            CLEyeDestroyCamera(_camera);
        }

        #endregion


        #region GT Properties

        public override int Width
        {
            get
            {
                return w;
            }
        }

        public override int Height
        {
            get
            {
                return h;
            }
        }

        public override int DefaultWidth
        {
            get { return Width; }
        }

        public override int DefaultHeight
        {
            get { return Height; }
        }

        public override int FPS
        {
            get { return (int)Framerate; }
        }

        public override bool IsSupportingROI
        {
            get
            {
                return false;
            }
            set
            {

            }
        }

        public override bool IsROISet
        {
            get { return false; }
        }

        public override bool IsSettingROI
        {
            get { return false; }
        }

        #endregion

        #region GT Methods

        public override bool Initialize()
        {
            bool success = this.Create(CameraUUID(0));

            return success; 
        }

        public override System.Drawing.Rectangle SetROI(System.Drawing.Rectangle newRoi)
        {
            // dont do anything
            return newRoi;
        }

        public override System.Drawing.Rectangle GetROI()
        {
            // return full image roi
            return new System.Drawing.Rectangle(0, 0, Width, Height);
        }

        public override void ClearROI()
        {
            // nothing to do
        }

        public override void Cleanup()
        {
            this.Stop();
            this.Destroy();
        }

        #endregion
    }
}
