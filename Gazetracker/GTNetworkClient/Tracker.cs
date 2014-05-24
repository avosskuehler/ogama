namespace GTNetworkClient
{
    internal class Tracker
    {
        #region Delegates

        public delegate void EyeTrackerParametersHandler(int eyeType, int parameterType, int value);

        public delegate void SampleRateHandler(int sampleRate);

        public delegate void TrackerStatusHandler(string status);

        #endregion

        //public event TrackerStatusHandler OnTrackerStatus;

        //public event EyeTrackerParametersHandler OnEyeTrackerParameters;

        //public event SampleRateHandler OnSampleRate;

        public void ExtractDataAndRaiseEvent(string data)
        {
        }
    }
}