using System;
using System.Drawing;
using System.Runtime.InteropServices;

namespace GTLibrary.Utils
{
    public class ScreenParameters
    {
        private static Size primaryResolution;
        private static Size primarySize;

        private static Size secondaryResolution;
        private static Size secondarySize;

        public static Size PrimaryResolution
        {
            get
            {
                GetPrimary();
                return primaryResolution;
            }
        }

        public static Size PrimarySize
        {
            get
            {
                GetPrimary();
                return primarySize;
            }
        }

        public static Size SecondaryResolution
        {
            get
            {
                GetSecondary();
                return secondaryResolution;
            }
        }

        public static Size SecondarySize
        {
            get
            {
                GetSecondary();
                return secondarySize;
            }
        }

        [DllImport("user32.dll")]
        private static extern IntPtr GetDC(IntPtr hwnd);

        [DllImport("gdi32.dll")]
        private static extern bool DeleteDC(IntPtr hdc);

        [DllImport("gdi32.dll")]
        private static extern int GetDeviceCaps(IntPtr hdc, DeviceCap nIndex);

        //[DllImport("user32.dll")]
        //static extern bool EnumDisplayMonitors(IntPtr hdc, IntPtr lprcClip,
        //   string lpfnEnum, IntPtr dwData);

        [DllImport("gdi32.dll")]
        private static extern IntPtr CreateDC(string lpszDriver, string lpszDevice,
                                              string lpszOutput, IntPtr lpInitData);


        private static void GetPrimary()
        {
            // Don't call the dll if we've already got the values *expensive*
            if (primaryResolution.Width != 0)
                return;

            using (Graphics.FromHdc(GetDC(IntPtr.Zero)))
            {
                var hdc = new IntPtr();
                //hdc = g.GetHdc();

                hdc = CreateDC("DISPLAY", null, null, IntPtr.Zero); //Get handle first monitor
                //hdc = CreateDC(("\\\\.\\DISPLAY2"), null, null, IntPtr.Zero);	//Get handle second monitor
                if (hdc != IntPtr.Zero)
                {
                    primaryResolution.Width = GetDeviceCaps(hdc, DeviceCap.HORZRES);
                    primaryResolution.Height = GetDeviceCaps(hdc, DeviceCap.VERTRES);
                    primarySize.Width =
                        (int) Math.Round((double) primaryResolution.Width/GetDeviceCaps(hdc, DeviceCap.LOGPIXELSX)*2.54)*
                        10;
                    primarySize.Height =
                        (int)
                        Math.Round((double) primaryResolution.Height/GetDeviceCaps(hdc, DeviceCap.LOGPIXELSY)*2.54)*10;
                }
                DeleteDC(hdc);
            }
        }

        private static void GetSecondary()
        {
            if (secondaryResolution.Width != 0)
                return;

            using (Graphics g = Graphics.FromHdc(GetDC(IntPtr.Zero)))
            {
                var hdc = new IntPtr();
                //hdc = g.GetHdc();

                //hdc = CreateDC("DISPLAY", null, null, IntPtr.Zero);	//Get handle first monitor
                hdc = CreateDC(("\\\\.\\DISPLAY2"), null, null, IntPtr.Zero); //Get handle second monitor
                if (hdc != IntPtr.Zero)
                {
                    secondarySize.Width = GetDeviceCaps(hdc, DeviceCap.HORZSIZE);
                    secondarySize.Height = GetDeviceCaps(hdc, DeviceCap.VERTSIZE);
                    secondaryResolution.Width = GetDeviceCaps(hdc, DeviceCap.HORZRES);
                    secondaryResolution.Height = GetDeviceCaps(hdc, DeviceCap.VERTRES);
                }
                DeleteDC(hdc);
            }
        }

        #region Nested type: DeviceCap

        private enum DeviceCap
        {
            /// <summary>
            /// Device driver version
            /// </summary>
            DRIVERVERSION = 0,
            /// <summary>
            /// Device classification
            /// </summary>
            TECHNOLOGY = 2,
            /// <summary>
            /// Horizontal size in millimeters
            /// </summary>
            HORZSIZE = 4,
            /// <summary>
            /// Vertical size in millimeters
            /// </summary>
            VERTSIZE = 6,
            /// <summary>
            /// Horizontal width in pixels
            /// </summary>
            HORZRES = 8,
            /// <summary>
            /// Vertical height in pixels
            /// </summary>
            VERTRES = 10,
            /// <summary>
            /// Number of bits per pixel
            /// </summary>
            BITSPIXEL = 12,
            /// <summary>
            /// Number of planes
            /// </summary>
            PLANES = 14,
            /// <summary>
            /// Number of brushes the device has
            /// </summary>
            NUMBRUSHES = 16,
            /// <summary>
            /// Number of pens the device has
            /// </summary>
            NUMPENS = 18,
            /// <summary>
            /// Number of markers the device has
            /// </summary>
            NUMMARKERS = 20,
            /// <summary>
            /// Number of fonts the device has
            /// </summary>
            NUMFONTS = 22,
            /// <summary>
            /// Number of colors the device supports
            /// </summary>
            NUMCOLORS = 24,
            /// <summary>
            /// Size required for device descriptor
            /// </summary>
            PDEVICESIZE = 26,
            /// <summary>
            /// Curve capabilities
            /// </summary>
            CURVECAPS = 28,
            /// <summary>
            /// Line capabilities
            /// </summary>
            LINECAPS = 30,
            /// <summary>
            /// Polygonal capabilities
            /// </summary>
            POLYGONALCAPS = 32,
            /// <summary>
            /// Text capabilities
            /// </summary>
            TEXTCAPS = 34,
            /// <summary>
            /// Clipping capabilities
            /// </summary>
            CLIPCAPS = 36,
            /// <summary>
            /// Bitblt capabilities
            /// </summary>
            RASTERCAPS = 38,
            /// <summary>
            /// Length of the X leg
            /// </summary>
            ASPECTX = 40,
            /// <summary>
            /// Length of the Y leg
            /// </summary>
            ASPECTY = 42,
            /// <summary>
            /// Length of the hypotenuse
            /// </summary>
            ASPECTXY = 44,
            /// <summary>
            /// Shading and Blending caps
            /// </summary>
            SHADEBLENDCAPS = 45,

            /// <summary>
            /// Logical pixels inch in X
            /// </summary>
            LOGPIXELSX = 88,
            /// <summary>
            /// Logical pixels inch in Y
            /// </summary>
            LOGPIXELSY = 90,

            /// <summary>
            /// Number of entries in physical palette
            /// </summary>
            SIZEPALETTE = 104,
            /// <summary>
            /// Number of reserved entries in palette
            /// </summary>
            NUMRESERVED = 106,
            /// <summary>
            /// Actual color resolution
            /// </summary>
            COLORRES = 108,

            // Printing related DeviceCaps. These replace the appropriate Escapes
            /// <summary>
            /// Physical Width in device units
            /// </summary>
            PHYSICALWIDTH = 110,
            /// <summary>
            /// Physical Height in device units
            /// </summary>
            PHYSICALHEIGHT = 111,
            /// <summary>
            /// Physical Printable Area x margin
            /// </summary>
            PHYSICALOFFSETX = 112,
            /// <summary>
            /// Physical Printable Area y margin
            /// </summary>
            PHYSICALOFFSETY = 113,
            /// <summary>
            /// Scaling factor x
            /// </summary>
            SCALINGFACTORX = 114,
            /// <summary>
            /// Scaling factor y
            /// </summary>
            SCALINGFACTORY = 115,

            /// <summary>
            /// Instance vertical refresh rate of the display device (for displays only) in Hz
            /// </summary>
            VREFRESH = 116,
            /// <summary>
            /// Horizontal width of entire desktop in pixels
            /// </summary>
            DESKTOPVERTRES = 117,
            /// <summary>
            /// Vertical height of entire desktop in pixels
            /// </summary>
            DESKTOPHORZRES = 118,
            /// <summary>
            /// Preferred blt alignment
            /// </summary>
            BLTALIGNMENT = 119
        }

        #endregion
    }
}