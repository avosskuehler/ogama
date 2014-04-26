using System;

namespace TETControls.Calibration
{
	public delegate void CalibrationFadeInFormHandler(object sender, EventArgs e);

	public class CalibrationFadeInArgs : EventArgs
	{
		private readonly bool _eventInfo;

		public CalibrationFadeInArgs(bool ready)
		{
			_eventInfo = ready;
		}

		public bool GetInfo()
		{
			return _eventInfo;
		}
	}
}



