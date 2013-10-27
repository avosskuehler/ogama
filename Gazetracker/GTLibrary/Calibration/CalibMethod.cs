using System;
using System.Collections.Generic;
using System.Drawing;

using GTCommons.Enum;
using GTSettings;

namespace GTLibrary.Calibration
{
  using GTLibrary.Detection.Glint;
  using GTLibrary.Utils;

  public abstract class CalibMethod
    {
        #region Variables

        private int averageErrorLeft;
        private int averageErrorRight;
        private List<CalibrationTarget> calibTargets;
        private double degreesLeft;
        private double degreesRight;

        #endregion

        #region Get/Set

        public GTPoint PupilCenterLeft { get; set; }

        public GTPoint PupilCenterRight { get; set; }

        public GlintConfiguration GlintConfigLeft { get; set; }

        public GlintConfiguration GlintConfigRight { get; set; }

        public bool IsCalibrated { get; set; }

        public int InstanceTargetNumber { get; set; }

        public List<CalibrationTarget> CalibrationTargets
        {
            get { return calibTargets; }
            set { calibTargets = value; }
        }

        public CalibrationData CalibrationDataLeft { get; set; }

        public CalibrationData CalibrationDataRight { get; set; }

        public int NumberOfTargets
        {
            get { return calibTargets.Count; }
        }

        public int NumImages
        {
            // Total number of images (i.e. sum of all the images grabbed
            // for each target)
            get { return GetTotalNumberOfImages(); }
        }

        public double AverageError
        {
            get
            {
                if (Settings.Instance.Processing.TrackingMode == TrackingModeEnum.Binocular)
                    return averageErrorLeft + averageErrorRight/2;
                else
                    return averageErrorLeft;
            }
        }

        public double AverageErrorLeft
        {
            get { return averageErrorLeft; }
        }

        public double AverageErrorRight
        {
            get { return averageErrorRight; }
        }

        public double DegreesLeft
        {
            get { return degreesLeft; }
        }

        public double DegreesRight
        {
            get { return degreesRight; }
        }

        public double Degrees
        {
            get
            {
                if (Settings.Instance.Processing.TrackingMode == TrackingModeEnum.Binocular)
                    return degreesLeft + degreesRight/2;
                else
                    return degreesLeft;
            }
        }

        #endregion

        #region Public methods

        public CalibrationTarget GetTarget(int targetNumber)
        {
            CalibrationTarget ctFound = null;

            foreach (CalibrationTarget ct in CalibrationTargets)
            {
                if (ct.targetNumber == targetNumber)
                    ctFound = ct;
            }

            return ctFound;
        }

        public void AddTarget(int targetNumber, Point targetCoordinates)
        {
            CalibrationTarget ctFound = GetTarget(targetNumber);

            if (ctFound == null)
                calibTargets.Add(new CalibrationTarget(targetNumber, targetCoordinates));
            else
            {
                //We're resampling this point, clear out
                IsCalibrated = false;
                ctFound.Clear();
            }
        }

        #endregion

        #region Public abstract methods (to be derived)

        public abstract bool Calibrate();
        public abstract GTPoint GetGazeCoordinates(TrackData trackData, EyeEnum eye);

        #endregion

        #region Private/Protected methods

        private int GetTotalNumberOfImages()
        {
            int numImages = 0;

            foreach (CalibrationTarget ct in CalibrationTargets)
                numImages += ct.NumImages;

            //Console.Out.WriteLine("Calibration, num images: " + numImages);
            return numImages;
        }

        protected void CalculateAverageErrorLeft()
        {
            if (!CalibrationDataLeft.Calibrated)
                averageErrorLeft = 0;
            else
            {
                averageErrorLeft = 0;
                double totalError = 0;

                foreach (CalibrationTarget ct in CalibrationTargets)
                    totalError += ct.averageErrorLeft;

                averageErrorLeft = Convert.ToInt32(Math.Round(totalError/calibTargets.Count));
            }
        }

        protected void CalculateAverageErrorRight()
        {
            if (!CalibrationDataRight.Calibrated)
                averageErrorRight = 0;
            else
            {
                averageErrorRight = 0;
                double totalError = 0;

                foreach (CalibrationTarget ct in CalibrationTargets)
                    totalError += ct.averageErrorRight;

                averageErrorRight = Convert.ToInt32(Math.Round(totalError/calibTargets.Count));
            }
        }

        public double CalculateDegreesLeft()
        {
            if (!CalibrationDataLeft.Calibrated)
                degreesLeft = 0;
            else
            {
                double averageErrorMM = ConvertPixToMm(averageErrorLeft);
                degreesLeft = 180 * Math.Atan(averageErrorMM / Settings.Instance.Calibration.DistanceFromScreen) / Math.PI;
            }
            return degreesLeft;
        }

        public double CalculateDegreesRight()
        {
            if (!CalibrationDataRight.Calibrated)
                degreesRight = 0;
            else
            {
                double averageErrorMM = ConvertPixToMm(averageErrorRight);
                degreesRight = 180 * Math.Atan(averageErrorMM / Settings.Instance.Calibration.DistanceFromScreen) / Math.PI;
            }
            return degreesRight;
        }

        private static double ConvertPixToMm(double pixels)
        {
            return pixels*ScreenParameters.PrimarySize.Width/ScreenParameters.PrimaryResolution.Width;
        }

        #endregion
    }
}