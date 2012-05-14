using System;
using System.Diagnostics;

namespace GTLibrary.Log
{
    public class Performance
    {
        #region Variables

        private static Performance instance;
        private readonly Stopwatch stopWatch = new Stopwatch();
        private bool isEnabled = true;

        #endregion

        #region Constructor

        private Performance()
        {
        }

        #endregion

        #region Public methods

        public void Start()
        {
            if (!isEnabled)
                return;

            stopWatch.Reset();
            stopWatch.Start();

            Console.WriteLine(stopWatch.ElapsedMilliseconds + "ms. Start..");
        }

        public void Stamp(string msg)
        {
            if (isEnabled && stopWatch.IsRunning)
                Console.WriteLine(stopWatch.ElapsedMilliseconds + "ms. " + msg);
        }

        public void Stop()
        {
            if (!isEnabled)
                return;

            stopWatch.Stop();
            Console.WriteLine(stopWatch.ElapsedMilliseconds + "ms. Stop");
        }

        #endregion

        #region Get/Set

        public static Performance Now
        {
            get
            {
                if (instance == null)
                {
                    instance = new Performance();
                }

                return instance;
            }
        }

        public bool IsEnabled
        {
            get { return isEnabled; }
            set { isEnabled = value; }
        }

        #endregion
    }
}