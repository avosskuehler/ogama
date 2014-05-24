using System;
using System.Collections.Generic;
using GTSettings;

namespace GTLibrary.EyeMovementDetection
{
  using GTLibrary.Utils;

  /// <summary>
    /// Interface for smoothing algorithms
    /// </summary>
    public interface IGazeSmoother
    {
        GTPoint Smooth(GTPoint newPoint);
        void Stop();
    }

    /// <summary>
    /// This does not do anything at all, it just returns the newest point
    /// </summary>
    public class IdentitySmoother : IGazeSmoother
    {
        #region Public methods

        public GTPoint Smooth(GTPoint newPoint)
        {
            return newPoint;
        }

        public void Stop()
        {
        }

        #endregion
    }

    /// <summary>
    /// Average window smoothing algorithm. It calculates the smoothed
    /// coordinates as the average in the last N samples
    /// </summary>
    public class AverageWindow : IGazeSmoother
    {
        #region Variables

        private readonly List<GTPoint> data;
        private readonly int numberOfSamples;

        #endregion

        #region Constructor 

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="numOfSamples">Number of samples we consider 
        /// (i.e. window size)</param>
        public AverageWindow(int numOfSamples)
        {
            data = new List<GTPoint>();
            numberOfSamples = numOfSamples;
        }

        #endregion

        #region Public methods 

        /// <summary>
        /// Smooth method. Call this method when we get a new
        /// sample and it will return the smoothed coordinates
        /// </summary>
        /// <param name="newPoint">New gazed point</param>
        /// <returns>Smoothed point</returns>
        public GTPoint Smooth(GTPoint newPoint)
        {
            GTPoint smoothedPoint;

            if (data.Count < numberOfSamples)
                data.Add(newPoint);
            else
            {
                data.RemoveAt(0);
                data.Add(newPoint);
            }

            smoothedPoint = Operations.Mean(data.ToArray());

            return smoothedPoint;
        }

        /// <summary>
        /// Stop smoothing. Call this method when we detect a saccade.
        /// </summary>
        public void Stop()
        {
            data.Clear();
        }

        #endregion
    }


    /// <summary>
    /// Exponential smoothing. It gives more weight to the most recent
    /// samples. This makes the cursor more respondant to small eye movements
    /// during a fixation
    /// </summary>
    public class ExponentialSmoother : IGazeSmoother
    {
        #region Variables

        private readonly List<GTPoint> data;

        private readonly double mean;
        private readonly int numberOfSamples;
        private const double maxDistance = 150;
        private double stddev;

        #endregion

        #region Constructor 

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="numOfSamples">Window size. Recommended value: 10 samples</param>
        /// <param name="mean">Mean of the exponential. Recommended value: 0</param>
        /// <param name="stddev">Standard deviation. Recommended value: 5</param>
        public ExponentialSmoother(int numOfSamples, double mean, double stddev)
        {
            data = new List<GTPoint>();
            numberOfSamples = numOfSamples;

            this.mean = mean;
            this.stddev = stddev;
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Smooth method. Call this method when we get a new
        /// sample and it will return the smoothed coordinates
        /// </summary>
        /// <param name="newPoint">New gazed point</param>
        /// <returns>Smoothed point</returns>
        /// 
        public GTPoint Smooth(GTPoint newPoint)
        {
            GTPoint smoothedPoint;

            stddev = (int) Math.Ceiling(Settings.Instance.EyeMovement.SmoothLevel/5.0);

            if (data.Count < numberOfSamples)
                data.Add(newPoint);
            else
            {
                data.RemoveAt(0);
                data.Add(newPoint);
            }


            //Javier: this has been incorporated in the eye movement detection, where it belongs
            //if (GetMaxDistanceOnWindow() > maxDistance)
            //    smoothedPoint = newPoint;
            //else
            //{
            var sum = new GTPoint(0, 0);
            double sumWeights = 0;
            double weight;

            for (int i = 0; i < data.Count; i++)
            {
                weight = GetGaussWeight(data.Count - i - 1, mean, stddev);
                sum.X += data[i].X*weight;
                sum.Y += data[i].Y*weight;

                sumWeights += weight;
            }

            smoothedPoint = new GTPoint(sum.X/sumWeights, sum.Y/sumWeights);
            //}

            return smoothedPoint;
        }

        /// <summary>
        /// Stop smoothing. Call this method when we detect a saccade.
        /// </summary>
        public void Stop()
        {
            data.Clear();
        }

        #endregion

        #region Private methods

        private static double GetGaussWeight(double x, double mean, double stddev)
        {
            double scale = 1/(Math.Sqrt(2*Math.PI)*stddev)*Math.Exp(-Math.Pow(0 - mean, 2)/(2*Math.Pow(stddev, 2)));
            return (1/(Math.Sqrt(2*Math.PI)*stddev*scale)*Math.Exp(-Math.Pow(-x - mean, 2)/(2*Math.Pow(stddev, 2))));
        }

        private double GetMaxDistanceOnWindow()
        {
            GTPoint centroid = Operations.Mean(data.ToArray());

            double maxDist = 0;
            double dist = 0;

            for (int i = 0; i < data.Count; i++)
            {
                dist = Operations.Distance(centroid, data[i]);
                if (dist > maxDistance)
                    maxDist = dist;
            }

            return maxDist;
        }

        #endregion
    }
}