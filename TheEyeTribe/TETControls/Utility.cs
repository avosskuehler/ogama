using System;
using System.Drawing;
using System.Runtime.InteropServices;


namespace TETControls
{
    public class Utility
    {
        #region Variabels 

		private const float DPI_DEFAULT = 96f; // default system DIP setting
        private static Utility _instance;
        private Point sysDpi;
        private float scaleDpi;

        #endregion

		#region Enums

		public enum DeviceCap
		{
			/// <summary>
			/// Logical pixels inch in X
			/// </summary>
			LOGPIXELSX = 88,
			/// <summary>
			/// Logical pixels inch in Y
			/// </summary>
			LOGPIXELSY = 90
		}  

		#endregion

		#region Constructor

		private Utility()
        {
            sysDpi = GetSystemDpi();
			ScaleDpi = DPI_DEFAULT / sysDpi.X;
        }

        #endregion

        #region Get/Set

        public static Utility Instance
        {
            get { return _instance ?? (_instance = new Utility()); }
        }

        public Point SysDpi
        {
            get { return sysDpi; }
            set { sysDpi = value; }
        }

        public float ScaleDpi
        {
            get { return scaleDpi; }
            set { scaleDpi = value; }
        }

        #endregion

        #region Public methods

        public static Point GetSystemDpi()
        {
			Point result = new Point();
			IntPtr hDc = GetDC(IntPtr.Zero);
			result.X = GetDeviceCaps(hDc, (int)DeviceCap.LOGPIXELSX);
			result.Y = GetDeviceCaps(hDc, (int)DeviceCap.LOGPIXELSY);
			ReleaseDC(IntPtr.Zero, hDc);
			return result;
        }

        #endregion

        #region Private methods (DLL Imports)

        [DllImport("gdi32.dll")]
        private static extern int GetDeviceCaps(IntPtr hdc, int nIndex);

        [DllImport("user32.dll")]
        private static extern IntPtr GetDC(IntPtr hWnd);

        [DllImport("user32.dll")]
        private static extern int ReleaseDC(IntPtr hWnd, IntPtr hDc);

        #endregion
    }
}
