using System;
using System.Collections.Generic;
using System.Linq;
using Emgu.CV;

using GTCommons.Enum;
using GTSettings;

namespace GTLibrary.Calibration
{
  using GTLibrary.Detection.BlobAnalysis;
  using GTLibrary.Detection.Glint;
  using GTLibrary.Utils;

  #region Pupil-only calibration

    /// <summary>
    /// Calibration of polynomial with pupil center only
    /// </summary>
    public class CalibPupil : CalibMethod
    {
        #region Constructor

        public CalibPupil()
        {
            //Initialization
            IsCalibrated = false;
            CalibrationTargets = new List<CalibrationTarget>();
        }

        #endregion

        #region Calibrate

        public override bool Calibrate()
        {
            if (NumImages == 0)
            {
                //throw new ArgumentException("numImages=0 in Calibrate()");
                return false;
            }

            try
            {
                CalibrationDataLeft = new CalibrationData();
                CalibrationDataRight = new CalibrationData();

                var targets = new Matrix<double>(NumImages, 3);
                var designMatrixLeft = new Matrix<double>(NumImages, 6);
                var designMatrixRight = new Matrix<double>(NumImages, 6);

                var rowLeft = new double[6];
                var rowRight = new double[6];

                int k = 0;

                foreach (CalibrationTarget ct in CalibrationTargets)
                {
                    for (int j = 0; j < ct.NumImages; j++)
                    {
                        targets[k, 0] = ct.targetCoordinates.X;
                        targets[k, 1] = ct.targetCoordinates.Y;

                        double xLeft = ct.pupilCentersLeft[j].X;
                        double yLeft = ct.pupilCentersLeft[j].Y;

                        rowLeft[0] = 1;
                        rowLeft[1] = xLeft;
                        rowLeft[2] = yLeft;
                        rowLeft[3] = xLeft*yLeft;
                        rowLeft[4] = xLeft*xLeft;
                        rowLeft[5] = yLeft*yLeft;

                        for (int r = 0; r < 6; r++)
                        {
                            designMatrixLeft[k, r] = rowLeft[r];
                        }

                        if (Settings.Instance.Processing.TrackingMode == TrackingModeEnum.Binocular)
                        {
                            double xRight = ct.pupilCentersRight[j].X;
                            double yRight = ct.pupilCentersRight[j].Y;

                            rowRight[0] = 1;
                            rowRight[1] = xRight;
                            rowRight[2] = yRight;
                            rowRight[3] = xRight*yRight;
                            rowRight[4] = xRight*xRight;
                            rowRight[5] = yRight*yRight;

                            for (int r = 0; r < 6; r++)
                            {
                                designMatrixRight[k, r] = rowRight[r];
                            }
                        }
                        k++;
                    }
                }

                CalibrationDataLeft.CoeffsX = new Matrix<double>(6, 1);
                CalibrationDataLeft.CoeffsY = new Matrix<double>(6, 1);
                CalibrationDataLeft.CoeffsX = Operations.SolveLeastSquares(designMatrixLeft, targets.GetCol(0));
                CalibrationDataLeft.CoeffsY = Operations.SolveLeastSquares(designMatrixLeft, targets.GetCol(1));

                if (Settings.Instance.Processing.TrackingMode == TrackingModeEnum.Binocular)
                {
                    CalibrationDataRight.CoeffsX = new Matrix<double>(6, 1);
                    CalibrationDataRight.CoeffsY = new Matrix<double>(6, 1);
                    CalibrationDataRight.CoeffsX = Operations.SolveLeastSquares(designMatrixRight, targets.GetCol(0));
                    CalibrationDataRight.CoeffsY = Operations.SolveLeastSquares(designMatrixRight, targets.GetCol(1));
                }

                // For each image we calculate the estimated gaze coordinates
                foreach (CalibrationTarget ct in CalibrationTargets)
                {
                    // We might be recalibrating so clear estGazeCoords first
                    ct.estimatedGazeCoordinatesLeft.Clear();
                    ct.estimatedGazeCoordinatesRight.Clear();

                    for (int j = 0; j < ct.NumImages; j++)
                    {
                        PupilCenterLeft = ct.pupilCentersLeft[j];
                        ct.estimatedGazeCoordinatesLeft.Add(GetGazeCoordinates(EyeEnum.Left));

                        if (Settings.Instance.Processing.TrackingMode == TrackingModeEnum.Binocular)
                        {
                            PupilCenterRight = ct.pupilCentersRight[j];
                            ct.estimatedGazeCoordinatesRight.Add(GetGazeCoordinates(EyeEnum.Right));
                        }
                    }

                    ct.CalculateAverageCoords();
                    ct.averageErrorLeft = Operations.Distance(ct.meanGazeCoordinatesLeft, ct.targetCoordinates);

                    if (Settings.Instance.Processing.TrackingMode == TrackingModeEnum.Binocular)
                        ct.averageErrorRight = Operations.Distance(ct.meanGazeCoordinatesRight, ct.targetCoordinates);
                }

                //calibrated = true;
                CalibrationDataLeft.Calibrated = true;
                CalculateAverageErrorLeft();
                CalculateDegreesLeft();

                if (Settings.Instance.Processing.TrackingMode == TrackingModeEnum.Binocular)
                {
                    CalibrationDataRight.Calibrated = true;
                    CalculateAverageErrorRight();
                    CalculateDegreesRight();
                }
            }
            catch (Exception)
            {
                //IsCalibrated = false;
                return true; // what to do here
            }

            IsCalibrated = true;
            return IsCalibrated;

            //OnCalibrationComplete(EventArgs.Empty); // Raise event
        }

        #endregion

        #region Get Gaze Coordinates

        public override GTPoint GetGazeCoordinates(TrackData trackData, EyeEnum eye)
        {
            var row = new Matrix<double>(6, 1);
            var screenCoordinates = new Matrix<double>(2, 1);

            var gazedPoint = new GTPoint();
            double x, y;

            if (eye == EyeEnum.Left)
            {
                x = trackData.PupilDataLeft.Center.X;
                y = trackData.PupilDataLeft.Center.Y;
            }
            else
            {
                x = trackData.PupilDataRight.Center.X;
                y = trackData.PupilDataRight.Center.Y;
            }

            row[0, 0] = 1;
            row[1, 0] = x;
            row[2, 0] = y;
            row[3, 0] = x*y;
            row[4, 0] = x*x;
            row[5, 0] = y*y;

            if (eye == EyeEnum.Left)
            {
                gazedPoint.X = CvInvoke.cvDotProduct(row.Ptr, CalibrationDataLeft.CoeffsX.Ptr);
                gazedPoint.Y = CvInvoke.cvDotProduct(row.Ptr, CalibrationDataLeft.CoeffsY.Ptr);
            }
            else
            {
                gazedPoint.X = CvInvoke.cvDotProduct(row.Ptr, CalibrationDataRight.CoeffsX.Ptr);
                gazedPoint.Y = CvInvoke.cvDotProduct(row.Ptr, CalibrationDataRight.CoeffsY.Ptr);
            }

            return gazedPoint;
        }

        public GTPoint GetGazeCoordinates(EyeEnum eye)
        {
            var row = new Matrix<double>(6, 1);
            var screenCoordinates = new Matrix<double>(2, 1);

            var gazedPoint = new GTPoint();
            double x, y;

            if (eye == EyeEnum.Left)
            {
                x = PupilCenterLeft.X;
                y = PupilCenterLeft.Y;
            }
            else
            {
                x = PupilCenterRight.X;
                y = PupilCenterRight.Y;
            }

            row[0, 0] = 1;
            row[1, 0] = x;
            row[2, 0] = y;
            row[3, 0] = x*y;
            row[4, 0] = x*x;
            row[5, 0] = y*y;

            if (eye == EyeEnum.Left)
            {
                gazedPoint.X = CvInvoke.cvDotProduct(row.Ptr, CalibrationDataLeft.CoeffsX.Ptr);
                gazedPoint.Y = CvInvoke.cvDotProduct(row.Ptr, CalibrationDataLeft.CoeffsY.Ptr);
            }
            else
            {
                gazedPoint.X = CvInvoke.cvDotProduct(row.Ptr, CalibrationDataRight.CoeffsX.Ptr);
                gazedPoint.Y = CvInvoke.cvDotProduct(row.Ptr, CalibrationDataRight.CoeffsY.Ptr);
            }

            return gazedPoint;
        }

        #endregion
    }

    #endregion

    #region Pupil-glint(s) calibration

    /// <summary>
    /// Calibration of a polynomial where we use the normalized pupil center.
    /// We normalize using the average coordinates of the glints stored in glintConfig.
    /// </summary>
    public class CalibPolynomial : CalibMethod
    {
        private int numOutliersRemovedLeft;
        private int numOutliersRemovedRight;

        #region Constructor

        public CalibPolynomial()
        {
            IsCalibrated = false;
            CalibrationTargets = new List<CalibrationTarget>();
        }

        #endregion

        #region Calibrate

        public override bool Calibrate()
        {
            if (numOutliersRemovedLeft == 0 && numOutliersRemovedRight == 0)
                RemoveOutliers(); // Only works sometimes, tried fixing it..

            if (NumImages == 0)
            {
                //throw new ArgumentException("numImages=0 in Calibrate()");
                IsCalibrated = false;
                return false;
            }

            #region Initialize variabels

            CalibrationDataLeft = new CalibrationData();
            CalibrationDataRight = new CalibrationData();


            var targets = new Matrix<double>(NumImages, 3);
            var designMatrixLeft = new Matrix<double>(NumImages, 6);
            var designMatrixRight = new Matrix<double>(NumImages, 6);

            var rowLeft = new double[6];
            var rowRight = new double[6];

            int k = 0;

            #endregion

            #region Build matrices

            foreach (CalibrationTarget ct in CalibrationTargets)
            {
                for (int j = 0; j < ct.NumImages; j++)
                {
                    #region Left

                    if (j < ct.pupilCentersLeft.Count && j < ct.glintsLeft.Count)
                    {
                        GTPoint pupilCenterLeft = ct.pupilCentersLeft.ElementAt(j);
                        GlintConfiguration glintsLeft = ct.glintsLeft.ElementAt(j);

                        if (pupilCenterLeft != null && glintsLeft != null && glintsLeft.Count > 0)
                        {
                            targets[k, 0] = ct.targetCoordinates.X;
                            targets[k, 1] = ct.targetCoordinates.Y;

                            double xLeft = pupilCenterLeft.X - glintsLeft.AverageCenter.X;
                            double yLeft = pupilCenterLeft.Y - glintsLeft.AverageCenter.Y;

                            rowLeft[0] = 1;
                            rowLeft[1] = xLeft;
                            rowLeft[2] = yLeft;
                            rowLeft[3] = xLeft*yLeft;
                            rowLeft[4] = xLeft*xLeft;
                            rowLeft[5] = yLeft*yLeft;

                            for (int r = 0; r < 6; r++)
                                designMatrixLeft[k, r] = rowLeft[r];
                        }
                    }

                    #endregion

                    #region Right

                    if (Settings.Instance.Processing.TrackingMode == TrackingModeEnum.Binocular)
                    {
                        if (ct.pupilCentersRight.Count - 1 > j && ct.glintsRight.Count - 1 > j)
                        {
                            GTPoint pupilCenterRight = ct.pupilCentersRight.ElementAt(j);
                            GlintConfiguration glintsRight = ct.glintsRight.ElementAt(j);

                            if (pupilCenterRight != null && glintsRight != null && glintsRight.Count > 0)
                            {
                                double xRight = pupilCenterRight.X - glintsRight.AverageCenter.X;
                                double yRight = pupilCenterRight.Y - glintsRight.AverageCenter.Y;

                                rowRight[0] = 1;
                                rowRight[1] = xRight;
                                rowRight[2] = yRight;
                                rowRight[3] = xRight*yRight;
                                rowRight[4] = xRight*xRight;
                                rowRight[5] = yRight*yRight;

                                for (int r = 0; r < 6; r++)
                                {
                                    designMatrixRight[k, r] = rowRight[r];
                                }
                            }
                        }
                    }

                    #endregion

                    k++;
                }
            }

            #endregion

            #region SolveLeastSquares

            CalibrationDataLeft.CoeffsX = new Matrix<double>(6, 1);
            CalibrationDataLeft.CoeffsY = new Matrix<double>(6, 1);
            CalibrationDataLeft.CoeffsX = Operations.SolveLeastSquares(designMatrixLeft, targets.GetCol(0));
            CalibrationDataLeft.CoeffsY = Operations.SolveLeastSquares(designMatrixLeft, targets.GetCol(1));

            if (Settings.Instance.Processing.TrackingMode == TrackingModeEnum.Binocular)
            {
                CalibrationDataRight.CoeffsX = new Matrix<double>(6, 1);
                CalibrationDataRight.CoeffsY = new Matrix<double>(6, 1);
                CalibrationDataRight.CoeffsX = Operations.SolveLeastSquares(designMatrixRight, targets.GetCol(0));
                CalibrationDataRight.CoeffsY = Operations.SolveLeastSquares(designMatrixRight, targets.GetCol(1));
            }

            #endregion

            #region Calculated est. gaze coordinates (per image)

            // For each image we calculate the estimated gaze coordinates
            foreach (CalibrationTarget ct in CalibrationTargets)
            {
                // We might be recalibrating so clear estGazeCoords first
                ct.estimatedGazeCoordinatesLeft.Clear();
                ct.estimatedGazeCoordinatesRight.Clear();

                for (int j = 0; j < ct.NumImages; j++)
                {
                    #region Left

                    if (ct.pupilCentersLeft.Count - 1 >= j && ct.glintsLeft.Count - 1 >= j)
                    {
                        var pupilCenterLeft = new GTPoint(0, 0);
                        var glintConfigLeft = new GlintConfiguration(new Blobs());

                        if (ct.pupilCentersLeft.ElementAt(j) != null)
                            pupilCenterLeft = ct.pupilCentersLeft[j];

                        if (ct.glintsLeft.ElementAt(j) != null)
                            glintConfigLeft = ct.glintsLeft[j];

                        if (pupilCenterLeft.Y != 0)
                            ct.estimatedGazeCoordinatesLeft.Add(GetGazeCoordinates(EyeEnum.Left, pupilCenterLeft,
                                                                                   glintConfigLeft));
                    }

                    #endregion

                    #region Right

                    if (Settings.Instance.Processing.TrackingMode == TrackingModeEnum.Binocular)
                    {
                        if (ct.pupilCentersRight.Count - 1 > j && ct.glintsRight.Count - 1 > j)
                        {
                            var pupilCenterRight = new GTPoint(0, 0);
                            var glintConfigRight = new GlintConfiguration(new Blobs());

                            if (ct.pupilCentersRight.ElementAt(j) != null)
                                pupilCenterRight = ct.pupilCentersRight[j];

                            if (ct.glintsRight.ElementAt(j) != null)
                                glintConfigRight = ct.glintsRight[j];

                            if (pupilCenterRight.Y != 0)
                                ct.estimatedGazeCoordinatesRight.Add(GetGazeCoordinates(EyeEnum.Right, pupilCenterRight,
                                                                                        glintConfigRight));
                        }
                    }

                    #endregion
                }

                ct.CalculateAverageCoords();
                ct.averageErrorLeft = Operations.Distance(ct.meanGazeCoordinatesLeft, ct.targetCoordinates);

                if (Settings.Instance.Processing.TrackingMode == TrackingModeEnum.Binocular)
                    ct.averageErrorRight = Operations.Distance(ct.meanGazeCoordinatesRight, ct.targetCoordinates);
            }

            CalibrationDataLeft.Calibrated = true;
            CalculateAverageErrorLeft();
            CalculateDegreesLeft();

            if (Settings.Instance.Processing.TrackingMode == TrackingModeEnum.Binocular)
            {
                CalibrationDataRight.Calibrated = true;
                CalculateAverageErrorRight();
                CalculateDegreesRight();
            }

            #endregion

            IsCalibrated = true;
            return IsCalibrated;
        }

        private void RemoveOutliers()
        {
            var meanLeft = new GTPoint();
            var stddevLeft = new GTPoint();
            var meanRight = new GTPoint();
            var stddevRight = new GTPoint();

            numOutliersRemovedLeft = 0;
            numOutliersRemovedRight = 0;

            foreach (CalibrationTarget ct in CalibrationTargets)
            {
                #region Calculate mean and std

                // Left
                if (ct.DifferenceVectorLeft != null)
                {
                    meanLeft = Operations.Mean(ct.DifferenceVectorLeft);
                    stddevLeft = Operations.StandardDeviation(ct.DifferenceVectorLeft);
                }

                // Right
                if (Settings.Instance.Processing.TrackingMode == TrackingModeEnum.Binocular)
                {
                    if (ct.DifferenceVectorRight != null)
                    {
                        meanRight = Operations.Mean(ct.DifferenceVectorRight);
                        stddevRight = Operations.StandardDeviation(ct.DifferenceVectorRight);
                    }
                }

                #endregion

                try
                {
                    for (int i = 0; i < ct.NumImages - 1; i++)
                    {
                        // remove left
                        if (ct.DifferenceVectorLeft != null && i <= ct.DifferenceVectorLeft.Length)
                            if (Math.Abs(ct.DifferenceVectorLeft[i].X - meanLeft.X) > stddevLeft.X ||
                                Math.Abs(ct.DifferenceVectorLeft[i].Y - meanLeft.Y) > stddevLeft.Y)
                            {
                                if (ct.pupilCentersLeft.Count <= i)
                                    ct.pupilCentersLeft.RemoveAt(i - numOutliersRemovedLeft);

                                if (ct.glintsLeft.Count <= i)
                                    ct.glintsLeft.RemoveAt(i - numOutliersRemovedLeft);

                                numOutliersRemovedLeft++;
                            }

                        // remove right (if binocular)                      
                        if (Settings.Instance.Processing.TrackingMode == TrackingModeEnum.Binocular)
                        {
                            if (ct.DifferenceVectorRight != null && i <= ct.DifferenceVectorRight.Length)
                                if (Math.Abs(ct.DifferenceVectorRight[i].X - meanRight.X) > stddevRight.X ||
                                    Math.Abs(ct.DifferenceVectorRight[i].Y - meanRight.Y) > stddevRight.Y)
                                {
                                    if (ct.pupilCentersRight.Count <= i)
                                        ct.pupilCentersRight.RemoveAt(i - numOutliersRemovedRight);

                                    if (ct.glintsRight.Count <= i)
                                        ct.glintsRight.RemoveAt(i - numOutliersRemovedRight);

                                    numOutliersRemovedRight++;
                                }
                        }
                        //Console.WriteLine("{0} outliers removed out of a total of {1}, Old std: {2}, {3}, New std: {4}, {5}",
                        //    numOutliersRemovedLeft, ct.NumImages, stddevLeft.X, stddevLeft.Y, Operations.StandardDeviation(ct.DifferenceVectorLeft).X,
                        //    Operations.StandardDeviation(ct.DifferenceVectorLeft).Y);
                    }
                }
                catch (Exception ex)
                {
                    Console.Out.WriteLine("Calibration.cs, error while removing outlier eye. Message: " + ex.Message);
                }
            }
        }

        #endregion

        #region Get Gaze Coordinates

        public override GTPoint GetGazeCoordinates(TrackData trackData, EyeEnum eye)
        {
            var row = new Matrix<double>(6, 1);
            var screenCoordinates = new Matrix<double>(2, 1);

            var gazedPoint = new GTPoint();
            
            try
            {
                double X = 0;
                double Y = 0;

                switch (eye)
                {
                    case EyeEnum.Left:
                        X = trackData.PupilDataLeft.Center.X - trackData.GlintDataLeft.Glints.AverageCenter.X;
                        Y = trackData.PupilDataLeft.Center.Y - trackData.GlintDataLeft.Glints.AverageCenter.Y;
                        break;
                    default:
                        X = trackData.PupilDataRight.Center.X - trackData.GlintDataRight.Glints.AverageCenter.X;
                        Y = trackData.PupilDataRight.Center.Y - trackData.GlintDataRight.Glints.AverageCenter.Y;
                        break;
                }

                row[0, 0] = 1;
                row[1, 0] = X;
                row[2, 0] = Y;
                row[3, 0] = X*Y;
                row[4, 0] = X*X;
                row[5, 0] = Y*Y;

                if (eye == EyeEnum.Left)
                {
                    if (CalibrationDataLeft != null && CalibrationDataLeft.CoeffsX != null)
                    {
                        gazedPoint.X = CvInvoke.cvDotProduct(row.Ptr, CalibrationDataLeft.CoeffsX.Ptr);
                        gazedPoint.Y = CvInvoke.cvDotProduct(row.Ptr, CalibrationDataLeft.CoeffsY.Ptr);
                    }
                }
                else
                {
                    if (CalibrationDataRight != null && CalibrationDataRight.CoeffsX != null)
                    {
                        gazedPoint.X = CvInvoke.cvDotProduct(row.Ptr, CalibrationDataRight.CoeffsX.Ptr);
                        gazedPoint.Y = CvInvoke.cvDotProduct(row.Ptr, CalibrationDataRight.CoeffsY.Ptr);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine("Calibration.cs, exception in GetGazeCoordinates(), message: " + ex.Message);
            }

            return gazedPoint;
        }

        public GTPoint GetGazeCoordinates(EyeEnum eye, GTPoint pupilCenter, GlintConfiguration glintConfig)
        {
            var row = new Matrix<double>(6, 1);
            var screenCoordinates = new Matrix<double>(2, 1);

            var gazedPoint = new GTPoint();
            double X, Y;

            try
            {
                X = pupilCenter.X - glintConfig.AverageCenter.X;
                Y = pupilCenter.Y - glintConfig.AverageCenter.Y;

                row[0, 0] = 1;
                row[1, 0] = X;
                row[2, 0] = Y;
                row[3, 0] = X*Y;
                row[4, 0] = X*X;
                row[5, 0] = Y*Y;

                if (eye == EyeEnum.Left)
                {
                    gazedPoint.X = CvInvoke.cvDotProduct(row.Ptr, CalibrationDataLeft.CoeffsX.Ptr);
                    gazedPoint.Y = CvInvoke.cvDotProduct(row.Ptr, CalibrationDataLeft.CoeffsY.Ptr);
                }
                else
                {
                    gazedPoint.X = CvInvoke.cvDotProduct(row.Ptr, CalibrationDataRight.CoeffsX.Ptr);
                    gazedPoint.Y = CvInvoke.cvDotProduct(row.Ptr, CalibrationDataRight.CoeffsY.Ptr);
                }
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine("Calibration.cs, exception in GetGazeCoordinates(), message: " + ex.Message);
            }

            return gazedPoint;
        }

        #endregion
    }

    #endregion
}