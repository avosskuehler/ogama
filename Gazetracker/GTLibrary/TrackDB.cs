using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Emgu.CV;
using Emgu.CV.CvEnum;

using GTCommons.Enum;

namespace GTLibrary
{
  using GTLibrary.Utils;

  public class TrackDB
    {
        #region Variables 

        private static TrackDB instance;
        private readonly int logSize = 50;
        private Queue<TrackData> db;

        // Tracking FPS
        private double fpsTracking;
        private int imagesReceivedSinceCounterStart;
        private DateTime timerStartTime;

        #endregion

        #region Constructor

        private TrackDB()
        {
            db = new Queue<TrackData>(logSize);
        }

        private TrackDB(int logSize)
        {
            this.logSize = logSize;
            db = new Queue<TrackData>(logSize);
        }

        public void AddSample(TrackData t)
        {
            if (db.Count > logSize)
                db.Dequeue();

            db.Enqueue(t);

            if (t.LeftROI.Y == t.RightROI.Y && t.LeftROI.X == t.RightROI.X)
                if (t.LeftROI.Y != 0 && t.RightROI.X != 0)
                    Console.Out.WriteLine("Same..");

            //Log.Performance.Now.Stamp("TrackDB Add");
        }

        #endregion

        #region ROI estimation methods

        public TrackData GetLastSample()
        {
            return db.Count == 0 ? new TrackData() : db.Last();
        }

        #endregion

        #region Variance calculations

        public double GetVariancePupilArea(EyeEnum eye)
        {
            var list = new List<double>();

            foreach (TrackData td in db)
            {
                if (eye == EyeEnum.Left && td.PupilDataLeft != null && td.PupilDataLeft.Blob != null)
                    list.Add(td.PupilDataLeft.Blob.Area);

                if (eye == EyeEnum.Right && td.PupilDataRight != null && td.PupilDataRight.Blob != null)
                    list.Add(td.PupilDataRight.Blob.Area);
            }

            if (list.Count != 0)
                return Operations.Variance(list.ToArray());
            else
                return 9999;
        }

        public double GetVariancePupilFullness(EyeEnum eye)
        {
            var list = new List<double>();

            foreach (TrackData td in db)
            {
                if (eye == EyeEnum.Left && td.PupilDataLeft.Blob != null)
                    list.Add(td.PupilDataLeft.Blob.Fullness);

                if (eye == EyeEnum.Right && td.PupilDataRight.Blob != null)
                    list.Add(td.PupilDataRight.Blob.Fullness);
            }

            if (list.Count != 0)
                return Operations.Variance(list.ToArray());
            else
                return 9999;
        }

        public double GetSTDPupilFullness(EyeEnum eye)
        {
            var list = new List<double>();

            foreach (TrackData td in db)
            {
                if (eye == EyeEnum.Left && td.PupilDataLeft.Blob != null)
                    list.Add(td.PupilDataLeft.Blob.Fullness);

                if (eye == EyeEnum.Right && td.PupilDataRight.Blob != null)
                    list.Add(td.PupilDataRight.Blob.Fullness);
            }

            if (list.Count != 0)
                return Operations.StandardDeviation(list.ToArray());
            else
                return 9999;
        }

        public double GetMeanPupilFullness(EyeEnum eye)
        {
            var list = new List<double>();

            foreach (TrackData td in db)
            {
                if (eye == EyeEnum.Left && td.PupilDataLeft.Blob != null)
                    list.Add(td.PupilDataLeft.Blob.Fullness);

                if (eye == EyeEnum.Right && td.PupilDataRight.Blob != null)
                    list.Add(td.PupilDataRight.Blob.Fullness);
            }

            if (list.Count != 0)
                return Operations.Mean(list.ToArray());
            else
                return 9999;
        }

        #endregion

        #region Slopes calculations

        public PointF GetSlopePupilFullness(EyeEnum eye, int numSamples)
        {
            TrackData[] tds = db.ToArray();
            var points = new PointF[numSamples];

            var direction = new PointF();
            var pointOnLine = new PointF();

            for (int i = tds.Length - 1; i >= tds.Length - numSamples; i--)
            {
                if (eye == EyeEnum.Left && tds[i].PupilDataLeft.Blob != null)
                {
                    points[tds.Length - i - 1] = new PointF(i, (float) tds[i].PupilDataLeft.Blob.Fullness);
                }
            }

            PointCollection.Line2DFitting(points, DIST_TYPE.CV_DIST_L2, out direction, out pointOnLine);

            return direction;
        }

        #endregion

        #region Hit ratio calculations

        public double CalculatePupilHitRatio(EyeEnum eye, int numberOfSample)
        {
            int misses = 0;

            foreach (TrackData t in db)
            {
                if (eye == EyeEnum.Left && t.PupilDataLeft.Center.X == 0)
                    misses++;
                else if (eye == EyeEnum.Right && t.PupilDataRight.Center.X == 0)
                    misses++;
            }

            return (double) misses/db.Count*100;
        }

        public double CalculateGlintHitRatio(EyeEnum eye)
        {
            int misses = 0;

            foreach (TrackData t in db)
            {
                if (eye == EyeEnum.Left && t.GlintDataLeft.Glints.Count == 0)
                    misses++;
                else if (eye == EyeEnum.Right && t.GlintDataRight.Glints.Count == 0)
                    misses++;
            }

            return (double) misses/db.Count*100;
        }

        #endregion

        #region Public Get/Search samples

        public Rectangle GetLastEyeROI(EyeEnum eye, Size imgSize)
        {
            TrackData[] tds = db.ToArray();

            for (int i = tds.Length - 1; i >= 0; i--)
            {
                switch (eye)
                {
                    case EyeEnum.Left:

                        //if (tds[i].LeftROI.Width != 0 &&
                        //    Operations.IsWithinBounds(tds[i].LeftROI,
                        //                              new Rectangle(0, 0, imgSize.Width, imgSize.Height)))
                            // Eye roi not set
                            if (tds[i].LeftROI.X == 0 || tds[i].LeftROI.Y == 0)
                                continue;

                            // Last sample roi not set 
                            if (tds[i].CameraROI.Y == 0 || tds[i].CameraROI.X == 0)
                                if (Operations.IsWithinBounds(tds[i].LeftROI, new Rectangle(0, 0, imgSize.Width, imgSize.Height)))
                                    return tds[i].LeftROI;

                            // Camera roi changed
                            if (i > 1) // we have samples 
                             if(tds[i - 1].CameraROI.Y != 0 && tds[i - 1].CameraROI.X != 0) // prior sample has camera roi
                               if(tds[i].CameraROI.Y != tds[i - 1].CameraROI.Y || tds[i].CameraROI.X != tds[i - 1].CameraROI.X) // they differ 
                               {
                                int x = 0;
                                int y = 0;

                                // Position between last and n-1 differs, roi has been recenterd
                                if (tds[i - 1].CameraROI.Y != 0)
                                {
                                    x = tds[i].LeftROI.X;
                                    y = imgSize.Height / 2 - tds[i].LeftROI.Height / 2;
                                }

                                if (tds[i - 1].CameraROI.X != 0)
                                {
                                    x = tds[i].LeftROI.X;
                                    x -= tds[i].LeftROI.X - tds[i - 1].LeftROI.X;
                                }

                                if (x != 0 || y != 0)
                                    return new Rectangle(x, y, tds[i].LeftROI.Width, tds[i].LeftROI.Height);
                            }
                            else if(Operations.IsWithinBounds(tds[i].LeftROI, new Rectangle(0, 0, imgSize.Width, imgSize.Height)))
                            {
                                return tds[i].LeftROI;
                            }
                        //}
                        break;

                    case EyeEnum.Right:

                        //if (tds[i].RightROI.Width != 0 && Operations.IsWithinBounds(tds[i].RightROI, new Rectangle(0, 0, imgSize.Width, imgSize.Height)))
                        //{
                            // Eye roi not set
                            if (tds[i].RightROI.X == 0 || tds[i].RightROI.Y == 0)
                                continue;

                            // Last sample roi not set 
                            if (tds[i].CameraROI.Y == 0 || tds[i].CameraROI.X == 0)
                                if (Operations.IsWithinBounds(tds[i].RightROI, new Rectangle(0, 0, imgSize.Width, imgSize.Height)))
                                    return tds[i].RightROI;

                            // Camera roi changed
                            if (i > 1) // we have samples 
                                if (tds[i - 1].CameraROI.Y != 0 && tds[i - 1].CameraROI.X != 0) // prior sample has camera roi
                                    if (tds[i].CameraROI.Y != tds[i - 1].CameraROI.Y || tds[i].CameraROI.X != tds[i - 1].CameraROI.X) // they differ 
                                    {


                                int x = 0;
                                int y = 0;

                                // Position between last and n-1 differs, roi has been recenterd
                                if (tds[i - 1].CameraROI.Y != 0)
                                {
                                    x = tds[i].RightROI.X;
                                    y = imgSize.Height/2 - tds[i].RightROI.Height/2;
                                }

                                if (tds[i - 1].CameraROI.X != 0)
                                {
                                    x = tds[i].RightROI.X;
                                    x -= tds[i].RightROI.X - tds[i - 1].RightROI.X;
                                }

                                if(x != 0 || y != 0)
                                   return new Rectangle(x, y, tds[i].RightROI.Width, tds[i].RightROI.Height);
                            }
                            else if(Operations.IsWithinBounds(tds[i].RightROI, new Rectangle(0, 0, imgSize.Width, imgSize.Height)))
                            {
                                return tds[i].RightROI;
                            }
                        //}
                        //return new Rectangle(0, 0, imgSize.Width, imgSize.Height);
                        break;
                }
            }


            //if (eye == EyeEnum.Left && tds[i].LeftROI.Width != 0 && 
                //    tds[i].LeftROI.Width <= imgSize.Width &&
                //    tds[i].LeftROI.Height <= imgSize.Height)
                //    return tds[i].LeftROI;

                //if (eye == EyeEnum.Right && tds[i].RightROI.Width != 0 &&
                //    tds[i].RightROI.Width <= imgSize.Width &&
                //    tds[i].RightROI.Height <= imgSize.Height)
                //    return tds[i].RightROI;

            return new Rectangle(0, 0, imgSize.Width, imgSize.Height);
        }


        public IEnumerable<TrackData> GetBySamples(int numberOfSamples, TrackDBFilter filterOptions)
        {
            switch (filterOptions)
            {
                case TrackDBFilter.All:
                    return db.ToList();

                case TrackDBFilter.EyeBothFound:

                    IEnumerable<TrackData> qBothEyesFound =
                        from td in db
                        where td.LeftROI.Width != 0 && td.RightROI.Width != 0
                        select td;

                    return qBothEyesFound;

                case TrackDBFilter.EyeLeftFound:

                    IEnumerable<TrackData> qLeftEyeFound =
                        from td in db
                        where td.LeftROI.Width != 0
                        select td;

                    return qLeftEyeFound;

                case TrackDBFilter.EyeRightFound:

                    IEnumerable<TrackData> qRightEyeFound =
                        from td in db
                        where td.RightROI.Width != 0
                        select td;

                    return qRightEyeFound;
            }

            return db.ToList();
        }

        public List<TrackData> GetByTime(int milliseconds, TrackDBFilter filterOptions)
        {
            return db.ToList();
        }

        #endregion

        #region Get/Set

        public static TrackDB Instance
        {
            get { return instance ?? (instance = new TrackDB()); }
        }

        public Queue<TrackData> Data
        {
            get { return db; }
            set { db = value; }
        }

        public double TrackingFPS
        {
            get { return fpsTracking; }
        }

        #endregion

        internal void CalculateFPS()
        {
            TimeSpan ts = DateTime.Now.Subtract(timerStartTime);

            if (ts.TotalMilliseconds < 1000)
            {
                imagesReceivedSinceCounterStart++;
            }
            else
            {
                fpsTracking = imagesReceivedSinceCounterStart;
                timerStartTime = DateTime.Now;
                imagesReceivedSinceCounterStart = 0;
            }
        }
    }
}