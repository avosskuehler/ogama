using System;
using System.Windows.Forms;
using TETCSharpClient;
using TETCSharpClient.Data;


namespace TETControls.Cursor
{
	public class CursorControl : IGazeListener
    {
        #region Get/Set

        public bool Enabled { get; set; }
		public bool Smooth { get; set; }
		public Screen ActiveScreen { get; set; }

        #endregion

        #region Constuctor

        public CursorControl() : this(Screen.PrimaryScreen, false, false) 
        { }

		public CursorControl(Screen screen, bool enabled, bool smooth)
        {
            GazeManager.Instance.AddGazeListener(this);
			ActiveScreen = screen;
			Enabled = enabled;
		    Smooth = smooth;
        }

        #endregion

        #region Public interface methods

        public void OnGazeUpdate(GazeData gazeData)
        {
			if (!Enabled) return;

            // start or stop tracking lost animation
	        if ((gazeData.State & GazeData.STATE_TRACKING_GAZE) == 0 &&
	            (gazeData.State & GazeData.STATE_TRACKING_PRESENCE) == 0) return;
	        
			// tracking coordinates
			var x = ActiveScreen.Bounds.X;
	        var y = ActiveScreen.Bounds.Y;
	        var gX = Smooth ? gazeData.SmoothedCoordinates.X : gazeData.RawCoordinates.X;
			var gY = Smooth ? gazeData.SmoothedCoordinates.Y : gazeData.RawCoordinates.Y;
			var screenX = (int)Math.Round(x + gX, 0);
			var screenY = (int)Math.Round(y + gY, 0);

			// return in case of 0,0 
			if (screenX == 0 && screenY == 0) return;

			NativeMethods.SetCursorPos(screenX, screenY);
        }

        #endregion

        public class NativeMethods  
        {  
            [System.Runtime.InteropServices.DllImportAttribute("user32.dll", EntryPoint = "SetCursorPos")]  
            [return: System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.Bool)]  

            public static extern bool SetCursorPos(int x, int y);  
        } 
    }
}