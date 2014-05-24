// <copyright file="DetectionManager.cs" company="Gazegroup">
// ******************************************************
// GazeTrackingLibrary for ITU GazeTracker
// Copyright (C) 2010 GazeGroup
// ------------------------------------------------------------------------
// This program is free software; you can redistribute it and/or modify it 
// under the terms of the GNU General Public License as published by the 
// Free Software Foundation; either version 3 of the License, 
// or (at your option) any later version.
// This program is distributed in the hope that it will be useful, 
// but WITHOUT ANY WARRANTY; without even the implied warranty of 
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU 
// General Public License for more details.
// You should have received a copy of the GNU General Public License 
// along with this program; if not, see http://www.gnu.org/licenses/.
// **************************************************************
// </copyright>
// <author>Martin Tall</author>
// <email>info@martintall.com</email>
// <modifiedby>Alastair Jeremy</modifiedby>

using System;
using System.Drawing;
using System.Reflection;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;

using GTCommons.Enum;
using GTSettings;

using log4net;

namespace GTLibrary.Detection.Eye
{
  using GTLibrary.Logging;

  public class Eyetracker
    {
        #region Fields

        /// <summary>
        /// Logger for log4net logging
        /// </summary>
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Indicator that can be used for high speed DEBUG level logging. Note that using this flag will prevent automated reload
        /// of log4net configuration for that log statement during program operation
        /// </summary>
        private static readonly bool isDebugEnabled = log.IsDebugEnabled;

        #endregion

        #region Variables

        private HaarCascade haarCascade;
        private bool foundLeft;
        private bool foundRight;
        private bool isReady;

        #endregion

        #region Constructor

        public Eyetracker()
        {
            try
            {
                if (haarCascade == null)
                    haarCascade = new HaarCascade(Settings.Instance.Eyetracker.HaarCascadePath);

                isReady = true;
            }
            catch (Exception ex)
            {
                ErrorLogger.ProcessException(ex, true);
            }
        }

        #endregion

        #region Events

        #region Delegates

        public delegate void AutoTunedEventHandler(bool success);

        #endregion

        //public event AutoTunedEventHandler OnAutoTuneCompleted;

        #endregion

        #region Public methods

        public bool DetectEyes(Image<Gray, byte> gray, TrackData trackData)
        {
            bool eyesFound = DoEyeRegionExtraction(gray, trackData);
            return eyesFound;
        }

        #endregion

        #region Private methods

        private bool DoEyeRegionExtraction(Image<Gray, byte> gray, TrackData trackData)
        {
            foundLeft = false;
            foundRight = false;

            // Run classifier 
            MCvAvgComp[][] eyesDetected = gray.DetectHaarCascade(
                haarCascade,
                Settings.Instance.Eyetracker.ScaleFactor,
                2, //Min Neighbours (how many nodes should return overlapping hits, eg. reduce false detections)
                HAAR_DETECTION_TYPE.DO_CANNY_PRUNING,
                Settings.Instance.Eyetracker.SizeMin);


            // No eyes detected return false
            if (eyesDetected == null || eyesDetected[0] != null && eyesDetected[0].Length == 0)
            {
                if (isDebugEnabled)
                    log.Debug("DoEyeRegionExtraction: No eyes detected from DetectHaarCascade(...) call, returning false");

                return false;
            }

            if (Settings.Instance.Processing.TrackingMode == TrackingModeEnum.Monocular)
            {
                if (eyesDetected[0].Length == 0)
                {
                    if (isDebugEnabled)
                        log.Debug("DoEyeRegionExtraction: Monocular mode returning false because no eyesDetected");

                    return false;
                }

                trackData.LeftROI = eyesDetected[0][0].rect;
                foundLeft = true;
                return true;
            }

            if (Settings.Instance.Processing.TrackingMode == TrackingModeEnum.Binocular)
            {
                // Binocular but didnt find both eyes
                if (eyesDetected[0].Length < 2)
                {
                    if (isDebugEnabled)
                    {
                        log.Debug(
                            "DoEyeRegionExtraction: Binocular mode returning false because eyesDetected[0].Length=" +
                            eyesDetected[0].Length + " which is less than 2");
                    }
                    return false;
                }

                var r1 = new Rectangle();
                var r2 = new Rectangle();

                int minSize = Settings.Instance.Eyetracker.SizeMin.Width;
                int maxSize = Settings.Instance.Eyetracker.SizeMax.Width;

                // more than two, exclude false hit (nose?)
                if (eyesDetected[0].Length > 2)
                {
                    if (isDebugEnabled)
                        log.Debug("DoEyeRegionExtraction: Binocular mode found " + eyesDetected[0].Length + " eyes");

                    // More than two hits, get the best fit by vertial alignment and distances
                    MCvAvgComp[] sortedEyesArray = AlignedROIs(eyesDetected[0]);
                    r1 = sortedEyesArray[0].rect;
                    r2 = sortedEyesArray[1].rect;
                }
                else
                {
                    if (isDebugEnabled)
                        log.Debug("DoEyeRegionExtraction: Binocular mode found exactly two eyes");

                    r1 = eyesDetected[0][0].rect;
                    r2 = eyesDetected[0][1].rect;
                }

                // Assign left/right eye
                if (r1.X < r2.X)
                {
                    trackData.LeftROI = r1;
                    trackData.RightROI = r2;
                }
                else
                {
                    trackData.LeftROI = r2;
                    trackData.RightROI = r1;
                }
                return true;
            }

            return false;

            //Log.Performance.Now.Stamp("Eye: Left X:" + trackData.LeftROI.X + " Y:" + trackData.LeftROI.Y + " W:" + trackData.LeftROI.Width + " H:" + trackData.LeftROI.Height + " Right X:" + trackData.RightROI.X + " Y:" + trackData.RightROI.Y + " W:" + trackData.RightROI.Width + " H:" + trackData.RightROI.Height);
        }

        /// <summary>
        /// This method returns the 2 ROIs that are more "aligned", i.e. with the
        /// lowest difference in the Y coordinate
        /// It calculates the distances between all the ROIs and selects the appropriate pair
        /// </summary>
        /// <param name="ROIs"></param>
        /// <returns></returns>
        private MCvAvgComp[] AlignedROIs(MCvAvgComp[] ROIs)
        {
            MCvAvgComp[] alignedROIs = new MCvAvgComp[2];
            int N = ROIs.Length;
            Matrix<int> distancesY = new Matrix<int>(N, N);
            distancesY = distancesY.Add(100000);
            double minimum;
            double maximum;
            Point minimumLocation;
            Point maximumLocation;

            for (int i = 0; i < N; i++)
            {
                for (int j = i + 1; j < N; j++)
                {
                    // If both rectangles do not intersect, we add their distance to the matrix
                    // MT: Min distance of 2 x roi.width
                    if (ROIs[j].rect.IntersectsWith(ROIs[i].rect) == false && Math.Abs(ROIs[j].rect.X - ROIs[i].rect.X) > ROIs[j].rect.Width*2.5)
                        distancesY[i, j] = Math.Abs(ROIs[j].rect.Y - ROIs[i].rect.Y);
                }
            }

            distancesY.MinMax(out minimum, out maximum, out minimumLocation, out maximumLocation);

            alignedROIs[0] = ROIs[minimumLocation.X];
            alignedROIs[1] = ROIs[minimumLocation.Y];

            return alignedROIs;
        }

        #endregion

        #region Get/Set

        public bool IsReady
        {
            get { return isReady; }
        }

        public bool FoundLeft
        {
            get { return foundLeft; }
        }

        public bool FoundRight
        {
            get { return foundRight; }
        }

        #endregion
    }
}