using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

using GTSettings;

namespace GTLibrary.Calibration
{
  using GTLibrary.Detection.Glint;
  using GTLibrary.Utils;

  /// <summary>
    /// This class defines a calibration target, which will contain different information,
    /// such as its coordinates or a list with pupil centers obtained during calibration
    /// </summary>
    public class CalibrationTarget
    {
        #region Variables

        //Once we have calibrated, we can calculate the estimated gaze coordinates
        public double averageErrorLeft;
        public double averageErrorRight;
        public List<GTPoint> estimatedGazeCoordinatesLeft = new List<GTPoint>();
        public List<GTPoint> estimatedGazeCoordinatesRight = new List<GTPoint>();
        public List<GlintConfiguration> glintsLeft = new List<GlintConfiguration>();
        public List<GlintConfiguration> glintsRight = new List<GlintConfiguration>();

        public Point meanGazeCoordinatesLeft;
        public Point meanGazeCoordinatesRight;
        public List<GTPoint> pupilCentersLeft = new List<GTPoint>();
        public List<GTPoint> pupilCentersRight = new List<GTPoint>();

        public double stdDeviationGazeCoordinatesLeft;
        public double stdDeviationGazeCoordinatesRight;
        public Point targetCoordinates;
        public int targetNumber;

        #endregion

        #region Constructor

        public CalibrationTarget(int targetNumber, Point targetCoordinates)
        {
            this.targetNumber = targetNumber;
            this.targetCoordinates = targetCoordinates;
        }

        #endregion

        #region Public methods

        public void Clear()
        {
            pupilCentersLeft = new List<GTPoint>();
            pupilCentersRight = new List<GTPoint>();
            glintsLeft = new List<GlintConfiguration>();
            glintsRight = new List<GlintConfiguration>();
            estimatedGazeCoordinatesLeft = new List<GTPoint>();
            estimatedGazeCoordinatesRight = new List<GTPoint>();
        }


        /// <summary>
        /// This method calculates the average of the estimated gazed coordinates
        /// (once calibration is finished)
        /// </summary>
        public void CalculateAverageCoords()
        {
            var avgLeft = new GTPoint();
            var avgRight = new GTPoint();
            var varianceLeft = new GTPoint();
            var varianceRight = new GTPoint();

            // Left eye
            if (estimatedGazeCoordinatesLeft.Count > 0)
            {
                avgLeft = Operations.Mean(estimatedGazeCoordinatesLeft.ToArray());
                meanGazeCoordinatesLeft.X = (int) avgLeft.X;
                meanGazeCoordinatesLeft.Y = (int) avgLeft.Y;
                varianceLeft = Operations.Variance(estimatedGazeCoordinatesLeft.ToArray());
                stdDeviationGazeCoordinatesLeft = Math.Sqrt(varianceLeft.X + varianceLeft.Y);
            }
            else
            {
                meanGazeCoordinatesLeft.X = 0;
                meanGazeCoordinatesLeft.Y = 0;
                stdDeviationGazeCoordinatesLeft = 0;
            }

            // Right eye
            if (estimatedGazeCoordinatesRight.Count > 0)
            {
                avgRight = Operations.Mean(estimatedGazeCoordinatesRight.ToArray());
                meanGazeCoordinatesRight.X = (int) avgRight.X;
                meanGazeCoordinatesRight.Y = (int) avgRight.Y;
                varianceRight = Operations.Variance(estimatedGazeCoordinatesRight.ToArray());
                stdDeviationGazeCoordinatesRight = Math.Sqrt(varianceRight.X + varianceRight.Y);
            }
            else
            {
                meanGazeCoordinatesRight.X = 0;
                meanGazeCoordinatesRight.Y = 0;
                stdDeviationGazeCoordinatesRight = 0;
            }
        }

        #endregion

        #region Get methods (diffVectors etc.)

        /// <summary>
        /// The difference vectors between pupil center and glints average center
        /// </summary>
        public GTPoint[] DifferenceVectorLeft
        {
            get
            {
                var diffVector = new GTPoint[glintsLeft.Count];
                for (int i = 0; i < glintsLeft.Count; i++)
                {
                    diffVector[i] = new GTPoint(pupilCentersLeft[i].X - glintsLeft[i].AverageCenter.X,
                                                pupilCentersLeft[i].Y - glintsLeft[i].AverageCenter.Y);
                }
                return diffVector;
            }
        }

        public GTPoint[] DifferenceVectorRight
        {
            get
            {
                var diffVector = new GTPoint[glintsRight.Count];
                for (int i = 0; i < glintsRight.Count; i++)
                {
                    diffVector[i] = new GTPoint(pupilCentersRight[i].X - glintsRight[i].AverageCenter.X,
                                                pupilCentersRight[i].Y - glintsRight[i].AverageCenter.Y);
                }
                return diffVector;
            }
        }


        /// <summary>
        /// Average pupil center of all the samples taken for this calibration target
        /// </summary>
        public GTPoint AvgPupilCenterLeft
        {
            get { return Operations.Mean(pupilCentersLeft.ToArray()); }
        }

        public GTPoint AvgPupilCenterRight
        {
            get { return Operations.Mean(pupilCentersRight.ToArray()); }
        }

        /// <summary>
        /// Standard deviation of the pupil center of all the samples taken
        /// for this calibration target
        /// </summary>
        public GTPoint StdDevPupilCenterLeft
        {
            get { return Operations.StandardDeviation(pupilCentersLeft.ToArray()); }
        }

        public GTPoint StdDevPupilCenterRight
        {
            get { return Operations.StandardDeviation(pupilCentersRight.ToArray()); }
        }


        /// <summary>
        /// Average glint center of all the samples taken for this calibration target
        /// (if we're using more than one glint, this will take the average of all the
        /// glints in each image)
        /// </summary>
        public GTPoint AvgGlintCenterLeft
        {
            get
            {
                var glintCentersLeft = new GTPoint[glintsLeft.Count];
                for (int i = 0; i < glintsLeft.Count; i++)
                {
                    glintCentersLeft[i] = new GTPoint(glintsLeft[i].AverageCenter);
                }
                return Operations.Mean(glintCentersLeft.ToArray());
            }
        }

        public GTPoint AvgGlintCenterRight
        {
            get
            {
                var glintCentersRight = new GTPoint[glintsRight.Count];
                for (int i = 0; i < glintsRight.Count; i++)
                {
                    glintCentersRight[i] = new GTPoint(glintsRight[i].AverageCenter);
                }
                return Operations.Mean(glintCentersRight.ToArray());
            }
        }

        /// <summary>
        /// Standard deviation of the glint center of all the samples taken for this  target
        /// (if we're using more than one glint, in each image we calculate the average glint
        /// center, and then we calculate the standard deviation on all the images)
        /// </summary>
        public GTPoint StdDevGlintCenterLeft
        {
            get
            {
                var glintCenters = new GTPoint[glintsLeft.Count];
                for (int i = 0; i < glintsLeft.Count; i++)
                {
                    glintCenters[i] = new GTPoint(glintsLeft[i].AverageCenter);
                }
                return Operations.StandardDeviation(glintCenters.ToArray());
            }
        }

        public GTPoint StdDevGlintCenterRight
        {
            get
            {
                var glintCenters = new GTPoint[glintsRight.Count];
                for (int i = 0; i < glintsRight.Count; i++)
                {
                    glintCenters[i] = new GTPoint(glintsRight[i].AverageCenter);
                }
                return Operations.StandardDeviation(glintCenters.ToArray());
            }
        }


        public int NumImages
        {
            get
            {
                if (pupilCentersLeft.Count != 0)
                    return pupilCentersLeft.Count;

                else if (pupilCentersRight.Count != 0)
                    return pupilCentersRight.Count;

                else
                    return 0;
            }
        }

        public bool IsCorner
        {
            get
            {
                bool isCorner = false;
                if (Settings.Instance.Calibration.NumberOfPoints == 9 &&
                    (targetNumber == 1 || targetNumber == 3 || targetNumber == 9 || targetNumber == 7))
                {
                    isCorner = true;
                }
                if (Settings.Instance.Calibration.NumberOfPoints == 12 &&
                    (targetNumber == 1 || targetNumber == 4 || targetNumber == 12 || targetNumber == 9))
                {
                    isCorner = true;
                }
                if (Settings.Instance.Calibration.NumberOfPoints == 16 &&
                    (targetNumber == 1 || targetNumber == 4 || targetNumber == 16 || targetNumber == 13))
                {
                    isCorner = true;
                }
                return isCorner;
            }
        }

        #endregion
    }
}