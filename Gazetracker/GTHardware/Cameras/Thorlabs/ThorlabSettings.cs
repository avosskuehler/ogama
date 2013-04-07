using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;

namespace GTHardware.Cameras.Thorlabs
{
    // http://www.thorlabs.com/Thorcat/19400/19418-D05.pdf

    public class ThorlabSettings : INotifyPropertyChanged
    {
        private ThorlabDevice camera;

        #region Variables 

        // Image size
        //private int roiStartXAbsolute = 1;
        //private int roiStartYAbsolute = 1;

        private Rectangle roi = new Rectangle(0, 0, 1280, 1024); // default
        private bool isRoiSet = false;
        private bool isSettingRoi = false;
        private long framesSinceROIChange = 0;

        private int binning = 0;
        private int subsampling = 0;

        // Timing
        private int pixelclock = 43;
        private double framerate = 25.015329;
        private double framerateMax = 200;
        private bool autoFramerateControl = false; // 0
        private bool autoFramerateSensorControl = false;
        private double exposure = 39.938233;

        // Parameters
        private int colorMode = 6;
        private int brightness = 100;
        private int contrast = 215;
        private double gamma = 1.0;
        private double hardwareGamma = 0;
        private int blackLevelMode = 0;
        private int blackLevelOffset = 0;
        private int hotPixelMode = 2;
        private int hotPixelThreshold = 0;
        private int globalShutter = 7;

        // Gain
        private int gainMaster = 1;
        private bool gainBoost = false;

        // Processing
        private int edgeEnhancement = 0;  // 0 = off, 1 =strong, 2=weak
        private int ropEffect = 0;
        private int whitebalance = 0;
        private bool colorCorrection = false;
        private double colorCcorrectionFactor = 1.000000;
        private int colorCorrectionSatU = 100;
        private int colorCorrectionSatV = 100;
        private int bayerConversion = 1;

        // Auto features
        private bool brightnessExposureControl = true; // 1
        private bool brightnessGainControl = true; //1
        private bool brightnessExposureSensorControl = false;
        private bool brightnessGainSensorControl = false;
        private bool brightnessControlOnce = false;
        private double brightnessReference = 55;
        private int brightnessSpeed = 50;
        private int brightnessMaxGain = 100;
        private double brightnessMaxExposure = 39.938233;
        private int brightnessAoiLeft = 290;
        private int brightnessAoiTop = 94;
        private int brightnessAoiWidth = 824;
        private int brightnessAoiHeight = 850;
        private int brightnessHysteresis = 2;
        private int brightnessSkipFrames=4;

        private bool autoWBControl = false;
        private int autoWBOffsetR = 0;
        private int autoWBOffsetB = 0;
        private int autoWBGainMin = 0;
        private int autoWBGainMax = 100;
        private int autoWBSpeed = 50;
        private int autoWBAoiLeft = 0;
        private int autoWBAoiTop = 0;
        private int autoWBAoiWidth = 1280;
        private int autoWBAoiHeight = 1024;
        private bool autoWBOnce = false;
        private int autoWBHysteresis = 2;
        private int autoWBSkipFrames = 4;

        #endregion


        #region Events 

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion


        #region Constructor

        public ThorlabSettings()
        {
            
        }

        public ThorlabSettings(ThorlabDevice cam)
        {
            camera = cam;
        }

        #endregion


        #region Get/Set

        public ThorlabDevice Camera 
        {
            set { camera = value; }
        }

        public Rectangle ROI
        {
            get { return roi; }
            //    set
            //    {
            //       // Must call function SetROI(Rectangle)
            //    }
        }

        public bool IsROISet
        {
            get { return isRoiSet; }
            set { isRoiSet = value; }
        }

        public bool IsSettingROI
        {
            get { return isSettingRoi; }
            set { isSettingRoi = value; }
        }

        public long FramesSinceROIChange
        {
            get { return framesSinceROIChange;  }
            set { framesSinceROIChange = value; }
        }

        public int Binning
        {
            get { return binning; }
            set { binning = value; }
        }

        public int Subsampling
        {
            get { return subsampling; }
            set { subsampling = value; }
        }

        public int Pixelclock
        {
            get { return pixelclock; }
            set { pixelclock = value; }
        }


        #region Framerate 

        public double Framerate
        {
            get
            {
                camera.GetFramesPerSecond(ref framerate);
                return framerate;
            }
            set
            {
                double actualFPS = 0;

                if(framerate < framerateMax)
                   framerate = value;
                else
                    framerate = framerateMax;

                if (camera.SetFrameRate(framerate, ref actualFPS) != ThorlabDevice.IS_SUCCESS)
                    Console.Out.WriteLine("UC480, set FPS: " + framerate + " Got: " + actualFPS);
            }
        }

        public double FramerateMax
        {
            get { return framerateMax; }
            set { framerateMax = value; }
        }

        public bool AutoFramerateControl
        {
            get { return autoFramerateControl; }
            set
            {
                autoFramerateControl = value;
                double input = 0;
                double output = 0;

                if(autoFramerateControl)
                {
                    input = 1;
                }

                camera.SetAutoParameter(ThorlabDevice.IS_SET_ENABLE_AUTO_FRAMERATE, ref input, ref output);
                Console.Out.WriteLine("Enable auto framerate : " + input + " " + output);
            }
        }

        public bool AutoFramerateSensorControl
        {
            get { return autoFramerateSensorControl; }
            set { autoFramerateSensorControl = value; }
        }

        #endregion


        public double Exposure
        {
            get { return exposure; }
            set { exposure = value; }
        }

        public int ColorMode
        {
            get { return colorMode; }
            set
            {
                colorMode = value;
                camera.SetColorMode(colorMode);
            }
        }

        public int Brightness
        {
            get { return brightness; }
            set { brightness = value; }
        }

        public int Contrast
        {
            get { return contrast; }
            set { contrast = value; }
        }

        public double Gamma
        {
            get { return gamma; }
            set 
            { 
                int ret = camera.SetGamma((int)value);

                if (ret == ThorlabDevice.IS_SUCCESS)
                {
                    gamma = value;
                    Console.Out.WriteLine("Gamma: " + gamma);
                }

            }
        }

        public double HardwareGamma
        {
            get { return hardwareGamma; }
            set { hardwareGamma = value; }
        }

        public int BlackLevelMode
        {
            get { return blackLevelMode; }
            set { blackLevelMode = value; }
        }

        public int BlackLevelOffset
        {
            get { return blackLevelOffset; }
            set { blackLevelOffset = value; }
        }

        public int HotPixelMode
        {
            get { return hotPixelMode; }
            set { hotPixelMode = value; }
        }

        public int HotPixelThreshold
        {
            get { return hotPixelThreshold; }
            set { hotPixelThreshold = value; }
        }

        public int GlobalShutter
        {
            get { return globalShutter; }
            set { globalShutter = value; }
        }

        public int GainMaster
        {
            get { return gainMaster; }
            set { 

                gainMaster = value;
                int ret = camera.SetHardwareGain(gainMaster, gainMaster, gainMaster, gainMaster);

                if(ret == ThorlabDevice.IS_SUCCESS)
                    Console.Out.WriteLine("GainMaster: " + gainMaster);
            }
        }

        public bool GainBoost
        {
            get { return gainBoost; }
            set
            {
                gainBoost = value;
                int ret;

                if(gainBoost)
                    ret = camera.SetGainBoost(ThorlabDevice.IS_SET_GAINBOOST_ON);
                 else
                    ret = camera.SetGainBoost(ThorlabDevice.IS_SET_GAINBOOST_OFF);

                if (ret == ThorlabDevice.IS_SUCCESS)
                   Console.Out.WriteLine("GainBoost: " + gainBoost);
            }
        }

        public int EdgeEnhancement
        {
            get { return edgeEnhancement; }
            set
            {
                int ret = 0;

                //IS_EDGE_EN_DISABLE = 0;
                //IS_EDGE_EN_STRONG = 1;
                //IS_EDGE_EN_WEAK = 2;


                if(value == 1)
                   ret = camera.SetEdgeEnhancement(ThorlabDevice.IS_EDGE_EN_STRONG);
                else if(value == 2)
                   ret = camera.SetEdgeEnhancement(ThorlabDevice.IS_EDGE_EN_WEAK);
                else
                   ret = camera.SetEdgeEnhancement(ThorlabDevice.IS_EDGE_EN_DISABLE);

                if (ret == ThorlabDevice.IS_SUCCESS)
                {
                    edgeEnhancement = value;
                    Console.Out.WriteLine("Set EdgeEnhancement: " + value);
                }
                else
                {
                    Console.Out.WriteLine("Set EdgeEnhancement failed.");
                }
            }
        }

        public int RopEffect
        {
            get { return ropEffect; }
            set { ropEffect = value; }
        }

        public int Whitebalance
        {
            get { return whitebalance; }
            set { whitebalance = value; }
        }


        #region Color correction 

        public bool ColorCorrection
        {
            get { return colorCorrection; }
            set { colorCorrection = value; }
        }

        public double ColorCorrectionFactor
        {
            get { return colorCcorrectionFactor; }
            set { colorCcorrectionFactor = value; }
        }

        public int ColorCorrectionSatU
        {
            get { return colorCorrectionSatU; }
            set { colorCorrectionSatU = value; }
        }

        public int ColorCorrectionSatV
        {
            get { return colorCorrectionSatV; }
            set { colorCorrectionSatV = value; }
        }

        #endregion

        public int BayerConversion
        {
            get { return bayerConversion; }
            set { bayerConversion = value; }
        }


        #region Auto Brightness

        public bool AutoBrightnessExposureControl
        {
            get { return brightnessExposureControl; }
            set { brightnessExposureControl = value; }

        }

        public bool AutoBrightnessGainControl
        {
            get { return brightnessGainControl; }
            set
            {
                double input = 0;
                double output = 0;
                brightnessGainControl = value;

                if(brightnessGainControl)
                {
                    input = 1;
                }

                camera.SetAutoParameter(ThorlabDevice.IS_SET_ENABLE_AUTO_GAIN, ref input, ref output);
                Console.Out.WriteLine("Enable auto gain : " + input + " " + output);
            }
        }

        public bool AutoBrightnessExposureSensorControl
        {
            get { return brightnessExposureSensorControl; }
            set
            {
               brightnessExposureSensorControl = value;
            }
        }

        public bool AutoBrightnessGainSensorControl
        {
            get { return brightnessGainSensorControl; }
            set
            {

                double input = 0;
                double output = 0;
                brightnessGainSensorControl = value;

                if(brightnessGainSensorControl)
                {
                    input = 1;
                }
                camera.SetAutoParameter(ThorlabDevice.IS_SET_ENABLE_AUTO_SENSOR_GAIN, ref input, ref output);
                Console.Out.WriteLine("Enable auto sensor gain control : " + input + " " + output);
            }
        }

        public bool BrightnessControlOnce
        {
            get { return brightnessControlOnce; }
            set { brightnessControlOnce = value; }
        }

        double brightnessReferenceInitialValue = 0;

        public double BrightnessReference
        {
            get { return brightnessReference; }
            set {

                // First time, store orginal value so that we can reset when exiting ROI
                if (brightnessReferenceInitialValue == 0)
                {
                    double i = 0; // output stored here
                    double o = 0;
                    camera.SetAutoParameter(ThorlabDevice.IS_GET_AUTO_REFERENCE, ref i, ref o);
                    brightnessReferenceInitialValue = i;
                }

                if (value == 0)
                    return;

                brightnessReference = value;
                double input = brightnessReference;
                double output = 0;

                if (camera.SetAutoParameter(ThorlabDevice.IS_SET_AUTO_REFERENCE, ref input, ref output) == ThorlabDevice.IS_SUCCESS)
                    Console.Out.WriteLine("BrightnessReference " + input + " " + output);
                else
                    Console.Out.WriteLine("BrightnessReference " + input + " failed"); 
            }

        }

        public int BrightnessSpeed
        {
            get { return brightnessSpeed; }
            set
            {
                brightnessSpeed = value;
                double input = brightnessSpeed;
                double output = 0;

                camera.SetAutoParameter(ThorlabDevice.IS_SET_AUTO_SPEED, ref input, ref output);
                Console.Out.WriteLine("BrightnessSpeed : " + input + " " + output);
            }
        }

        public int BrightnessMaxGain
        {
            get { 
                return brightnessMaxGain; 
            }
            set 
            {
                brightnessMaxGain = value;
                double input = brightnessMaxGain;
                double output = 0;
                camera.SetAutoParameter(ThorlabDevice.IS_SET_AUTO_GAIN_MAX, ref input, ref output);
                Console.Out.WriteLine("Auto-Brightness Max Gain: " + input + " " + output);
            }

        }

        public double BrightnessMaxExposure
        {
            get { return brightnessMaxExposure; }
            set { brightnessMaxExposure = value; }
        }

        #region Brightness AOI

        public int BrightnessAoiLeft
        {
            get { return brightnessAoiLeft; }
            set { 
                brightnessAoiLeft = value;
                camera.SetAOIAutoBrightness(brightnessAoiLeft, brightnessAoiTop, brightnessAoiWidth, brightnessAoiHeight);
            }
        }

        public int BrightnessAoiTop
        {
            get { return brightnessAoiTop; }
            set { 
                brightnessAoiTop = value;
                camera.SetAOIAutoBrightness(brightnessAoiLeft, brightnessAoiTop, brightnessAoiWidth, brightnessAoiHeight);
            }
        }

        public int BrightnessAoiWidth
        {
            get { return brightnessAoiWidth; }
            set { 
                brightnessAoiWidth = value;
                camera.SetAOIAutoBrightness(brightnessAoiLeft, brightnessAoiTop, brightnessAoiWidth, brightnessAoiHeight);
            }
        }

        public int BrightnessAoiHeight
        {
            get { return brightnessAoiHeight; }
            set { 
                brightnessAoiHeight = value;
                camera.SetAOIAutoBrightness(brightnessAoiLeft, brightnessAoiTop, brightnessAoiWidth, brightnessAoiHeight);
            }
        }

        #endregion

        public int BrightnessHysteresis
        {
            get { return brightnessHysteresis; }
            set { brightnessHysteresis = value; }
        }

        public int BrightnessSkipFrames
        {
            get { return brightnessSkipFrames; }
            set { brightnessSkipFrames = value; }
        }

        #endregion


        #region Auto White Balance

        public bool AutoWbControl
        {
            get { return autoWBControl; }
            set { autoWBControl = value; }
        }

        public int AutoWbOffsetR
        {
            get { return autoWBOffsetR; }
            set { autoWBOffsetR = value; }
        }

        public int AutoWbOffsetB
        {
            get { return autoWBOffsetB; }
            set { autoWBOffsetB = value; }
        }

        public int AutoWbGainMin
        {
            get { return autoWBGainMin; }
            set { autoWBGainMin = value; }
        }

        public int AutoWbGainMax
        {
            get { return autoWBGainMax; }
            set { autoWBGainMax = value; }
        }

        public int AutoWbSpeed
        {
            get { return autoWBSpeed; }
            set { autoWBSpeed = value; }
        }

        public int AutoWbAoiLeft
        {
            get { return autoWBAoiLeft; }
            set { autoWBAoiLeft = value; }
        }

        public int AutoWbAoiTop
        {
            get { return autoWBAoiTop; }
            set { autoWBAoiTop = value; }
        }

        public int AutoWbAoiWidth
        {
            get { return autoWBAoiWidth; }
            set { autoWBAoiWidth = value; }
        }

        public int AutoWbAoiHeight
        {
            get { return autoWBAoiHeight; }
            set { autoWBAoiHeight = value; }
        }

        public bool AutoWbOnce
        {
            get { return autoWBOnce; }
            set { autoWBOnce = value; }
        }

        public int AutoWbHysteresis
        {
            get { return autoWBHysteresis; }
            set { autoWBHysteresis = value; }
        }

        public int AutoWbSkipFrames
        {
            get { return autoWBSkipFrames; }
            set { autoWBSkipFrames = value; }
        }

        #endregion


        #endregion


        #region EVENTHANDLER

        private void OnPropertyChanged(string parameter)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(parameter));
            }
        }

        #endregion //EVENTHANDLER


        #region Public methods

        public bool ReadFile(string filepath)
        {
            // todo
            return true;
        }

        public void ReadAndApplyNativeIni(string filepath)
        {
            IniFile ini = new IniFile(filepath);

            #region Sample file
            /*

            [Versions]
            uc480.dll=3.50.0010
            uc480.sys=3.50.0010
            uc480_boot.sys=3.50.0010

            [Sensor]
            Sensor=DCC1545M

            [Image size]
            Start X=0
            Start Y=0
            Start X absolute=1
            Start Y absolute=1
            Width=1280
            Height=1024
            Binning=0
            Subsampling=0

            [Timing]
            Pixelclock=40
            Framerate=23.270074
            Exposure=7.969950

            [Selected Converter]
            IS_SET_CM_RGB32=1
            IS_SET_CM_RGB24=1
            IS_SET_CM_RGB16=1
            IS_SET_CM_RGB15=1
            IS_SET_CM_Y8=1
            IS_SET_CM_RGB8=1
            IS_SET_CM_BAYER=8
            IS_SET_CM_UYVY=1
            IS_SET_CM_UYVY_MONO=1
            IS_SET_CM_UYVY_BAYER=1
            IS_CM_CBYCRY_PACKED=8
            IS_SET_CM_RGBY=8
            IS_SET_CM_RGB30=8
            IS_SET_CM_Y12=8
            IS_SET_CM_BAYER12=8
            IS_SET_CM_Y16=8
            IS_SET_CM_BAYER16=8
            IS_CM_RGBA8_PACKED=1
            IS_CM_RGB8_PACKED=1
            IS_CM_RGBY8_PACKED=8
            IS_CM_RGB10V2_PACKED=8


            [Parameters]
            Colormode=6
            Brightness=65
            Contrast=215
            Gamma=1.000000
            Hardware Gamma=0
            Blacklevel Mode=0
            Blacklevel Offset=0
            Hotpixel Mode=2
            Hotpixel Threshold=0
            GlobalShutter=7

            [Gain]
            Master=50
            Red=0
            Green=0
            Blue=0
            GainBoost=0

            [Processing]
            EdgeEnhancement=0
            RopEffect=0
            Whitebalance=0
            Whitebalance Red=1.000000
            Whitebalance Green=1.000000
            Whitebalance Blue=1.000000
            Color correction=0
            Color_correction_factor=1.000000
            Color_correction_satU=100
            Color_correction_satV=100
            Bayer Conversion=1

            [Auto features]
            Auto Framerate control=0
            Brightness exposure control=0
            Brightness gain control=0
            Auto Framerate Sensor control=0
            Brightness exposure Sensor control=0
            Brightness gain Sensor control=0
            Brightness control once=0
            Brightness reference=128
            Brightness speed=50
            Brightness max gain=100
            Brightness max exposure=42.933600
            Brightness Aoi Left=0
            Brightness Aoi Top=0
            Brightness Aoi Width=1280
            Brightness Aoi Height=1024
            Brightness Hysteresis=2
            Brightness Skip Frames=4
            Auto WB control=0
            Auto WB offsetR=0
            Auto WB offsetB=0
            Auto WB gainMin=0
            Auto WB gainMax=100
            Auto WB speed=50
            Auto WB Aoi Left=0
            Auto WB Aoi Top=0
            Auto WB Aoi Width=1280
            Auto WB Aoi Height=1024
            Auto WB Once=0
            Auto WB Hysteresis=2
            Auto WB Skip Frames=4

            [Trigger and Flash]
            Trigger delay=0
            Flash strobe=0
            Flash delay=0
            Flash duration=0

            */
            #endregion

            // Timing
            Pixelclock = Convert.ToInt32(ini.IniReadValue("Timing", "Pixelclock"));
            Framerate = Convert.ToDouble(ini.IniReadValue("Timing", "Framerate"));
            Exposure = Convert.ToDouble(ini.IniReadValue("Timing", "Exposure"));

            // Gain
            GainMaster = Convert.ToInt32(ini.IniReadValue("Gain", "Master"));

            int gBoost = Convert.ToInt32(ini.IniReadValue("Gain", "GainBoost"));
            if (gBoost == 1)
                GainBoost = true;
            else
                GainBoost = false;

            //Auto features

            int autFrameRate = Convert.ToInt32(ini.IniReadValue("Auto features", "Auto Framerate control"));
            if (autFrameRate == 1)
                AutoFramerateControl = true;
            else
                AutoFramerateControl = false;

            int briGainControl = Convert.ToInt32(ini.IniReadValue("Auto features", "Brightness gain control"));
            if (briGainControl == 0)
                AutoBrightnessGainControl = false;
            else
                AutoBrightnessGainControl = true;

            BrightnessMaxGain = Convert.ToInt32(ini.IniReadValue("Auto features", "Brightness max gain"));

            BrightnessReference = Convert.ToInt32(ini.IniReadValue("Auto features", "Brightness reference"));
            BrightnessSpeed = Convert.ToInt32(ini.IniReadValue("Auto features", "Brightness speed"));
            brightnessAoiLeft = Convert.ToInt32(ini.IniReadValue("Auto features", "Brightness Aoi Left"));
            brightnessAoiTop = Convert.ToInt32(ini.IniReadValue("Auto features", "Brightness Aoi Top"));
            brightnessAoiWidth = Convert.ToInt32(ini.IniReadValue("Auto features", "Brightness Aoi Width"));
            brightnessAoiHeight = Convert.ToInt32(ini.IniReadValue("Auto features", "Brightness Aoi Height"));

        }

        public bool WriteFile(string filepath)
        {
            // to-do
            return true;
        }

        #region ROI methods

        public Rectangle SetROI(Rectangle newRoi)
        {
            //// Out of bounds
            if (newRoi.X > 1280 || newRoi.X < 0 ||
                newRoi.Y > 1024 || newRoi.Y < 0 ||
                newRoi.Width > 1280 || newRoi.Width < 0 ||
                newRoi.Height > 1024 || newRoi.Height < 0)
                return newRoi;

            //// Same size & position
            if (roi.X == newRoi.X && roi.Y == newRoi.Y && roi.Width == newRoi.Width && roi.Height == newRoi.Height)
                return newRoi;

            framesSinceROIChange = 0;

            bool success = false;

            // Make sure roi format conforms to sensor limitations
            newRoi = AdjustROI(newRoi);


            //if (newRoi.Width == roi.Width && newRoi.Height == roi.Height)
            //{
            //    if (newRoi.X != roi.X || newRoi.Y != roi.Y)
            //    {
            //        camera.SetImagePos(newRoi.X, newRoi.Y);
            //        return newRoi;
            //    }
            //}


            if (camera.SetAOI(newRoi.X, newRoi.Y, newRoi.Width, newRoi.Height) == ThorlabDevice.IS_SUCCESS)
            {
                success = true;

                if (newRoi.Width != 1280 && newRoi.Width != roi.Width && isRoiSet == false)
                {
                    GainBoost = true;
                    //AutoBrightnessGainControl = true;
                    //GainMaster = 1;
                    BrightnessReference += 55;
                    //EdgeEnhancement = 2;
                }
                isRoiSet = success;
            }
            else
            {
                return roi;
            }

            // Clear roi
            if (success && newRoi.Width == 1280)
            {
                isRoiSet = false; // we did a clear roi
                GainBoost = false;
                BrightnessReference = brightnessReferenceInitialValue;
                //EdgeEnhancement = 0;
                //AutoBrightnessGainControl = false;
                //GainMaster = 0;
               // Gamma = 1;

            }

            // Store new roi
            this.roi = newRoi;

            //Console.Out.WriteLine("SetRoi, w:" + newRoi.Width + " h:" + newRoi.Height + " x:" + newRoi.X + " y:" + newRoi.Y + " IsSet:" + isRoiSet);

            return roi;
        }

        public Rectangle GetROI()
        {
            return roi;
        }

        public Rectangle AdjustROI(Rectangle ROI)
        {
            Rectangle adjustedROI = new Rectangle();
            adjustedROI = ROI;

            int reminder = 0;

            Math.DivRem(ROI.X, 4, out reminder);
            if (reminder != 0)
                adjustedROI.X -= reminder;

            reminder = 0;

            Math.DivRem(ROI.Width, 4, out reminder);
            if (reminder != 0)
                adjustedROI.X -= reminder;

            reminder = 0;

            Math.DivRem(ROI.Height, 2, out reminder);
            if (reminder != 0)
                adjustedROI.X -= reminder;

            reminder = 0;
            
            Math.DivRem(ROI.Y, 2, out reminder);
            if (reminder != 0)
                adjustedROI.Y -= reminder;

            reminder = 0;

            return adjustedROI;
        }

        #endregion

        public void ApplyDefaults() 
        {
            //GainBoost = false;
            //AutoBrightnessGainControl = false;
            //AutoFramerateControl = true;
            //BrightnessSpeed = 50;
        }



        #endregion



    }

}
