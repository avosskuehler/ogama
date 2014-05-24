using System.Collections.Generic;
using System.Drawing;

using GTCommons.Enum;
using GTSettings;

namespace GTLibrary.Calibration
{
  using GTLibrary.Utils;

  public class Recalibration
    {
        #region Delegates

        public delegate void RecalibrationAvailableHandler();

        #endregion

        #region Variables

        private readonly Point[] corners = new Point[4];
        private readonly int numTargetsForRecalib;


        public Calibration calibration;

        public List<GTPoint> gazeCoordinates;
        private int numCorners = 4;

        private int numRecalibTargets;

        public bool recalibrating;
        public List<Point> targetCoordinates;

        #endregion

        public Recalibration()
        {
            numTargetsForRecalib = 30;
            targetCoordinates = new List<Point>(numTargetsForRecalib);
            gazeCoordinates = new List<GTPoint>(numTargetsForRecalib);
            numRecalibTargets = 0;

            calibration = new Calibration();
            //if (Settings.Instance.Processing.TrackingGlints)
            //    calibration = new CalibPolynomial();
            //else
            //    calibration = new CalibPupil();
        }


        public int NumRecalibTargets
        {
            get { return numRecalibTargets; }
            set
            {
                numRecalibTargets = value;
                if (numRecalibTargets >= numTargetsForRecalib)
                {
                    switch (Settings.Instance.Calibration.RecalibrationType)
                    {
                        case RecalibrationTypeEnum.Full:
                            FullRecalibration();
                            break;
                        case RecalibrationTypeEnum.Continuous:
                            ContinuousRecalibration();
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        public int NumTargetsForRecalib
        {
            get { return numTargetsForRecalib; }
        }

        public void StartRecalibration(Calibration calib)
        {
            CopyCorners(calib);
            recalibrating = true;
        }

        public event RecalibrationAvailableHandler RecalibrationAvailable;

        public void OnRecalibrationAvailable()
        {
            RecalibrationAvailable();
        }


        public void FullRecalibration()
        {
            //double previousError = ErrorPreviousCalib();

            calibration.Calibrate();

            OnRecalibrationAvailable();

            #region Clear calibration data

            //if (Settings.Instance.Processing.TrackingGlints)
            //    calibration = new CalibPolynomial();
            //else
            //    calibration = new CalibPupil();
            //gazeCoordinates.Clear();
            //targetCoordinates.Clear();
            //numRecalibTargets = 0;

            #endregion
        }

        public void ContinuousRecalibration()
        {
            calibration.Calibrate();
            OnRecalibrationAvailable();

            //gazeCoordinates.RemoveAt(0);
            //targetCoordinates.RemoveAt(0);
            //calibration.CalibrationTargets.RemoveAt(0);

            for (int i = 0; i < numCorners; i++)
            {
                if (
                    Operations.Distance(
                        calibration.CalibMethod.CalibrationTargets[NumRecalibTargets - 1].targetCoordinates, corners[i]) <
                    150)
                {
                    calibration.CalibMethod.CalibrationTargets.RemoveAt(i);
                    calibration.CalibMethod.CalibrationTargets.Insert(i,
                                                                      calibration.CalibMethod.CalibrationTargets[
                                                                          NumRecalibTargets - 2]);
                }
            }

            if (calibration.CalibMethod.CalibrationTargets.Count == numTargetsForRecalib)
            {
                gazeCoordinates.RemoveAt(4);
                calibration.CalibMethod.CalibrationTargets.RemoveAt(4);
            }

            numRecalibTargets--;
        }

        public void RecalibrateOffset(GTPoint gazeCoords, Point targetCoords)
        {
            double distanceX = gazeCoords.X - targetCoords.X;
            double distanceY = gazeCoords.Y - targetCoords.Y;

            calibration.CalibMethod.CalibrationDataLeft.CoeffsX[0, 0] -= distanceX/2;
            calibration.CalibMethod.CalibrationDataLeft.CoeffsY[0, 0] -= distanceY/2;

            if (GTSettings.Settings.Instance.Processing.TrackingMode == TrackingModeEnum.Binocular)
            {
                calibration.CalibMethod.CalibrationDataRight.CoeffsX[0, 0] += distanceX/2;
                calibration.CalibMethod.CalibrationDataRight.CoeffsY[0, 0] += distanceY/2;
            }

            OnRecalibrationAvailable();
        }

        private double ErrorPreviousCalib()
        {
            double totalError = 0;

            for (int i = 0; i < targetCoordinates.Count; i++)
            {
                totalError = totalError + Operations.Distance(targetCoordinates[i], gazeCoordinates[i]);
            }

            return totalError/targetCoordinates.Count;
        }

        public void CopyCorners(Calibration calib)
        {
            for (int i = 0; i < calibration.CalibMethod.CalibrationTargets.Count; i++)
            {
                if (calibration.CalibMethod.CalibrationTargets[i].IsCorner)
                {
                    corners[numRecalibTargets] = calibration.CalibMethod.CalibrationTargets[i].targetCoordinates;

                    calibration.CalibMethod.CalibrationTargets.Add(
                        new CalibrationTarget(numRecalibTargets,
                                              calibration.CalibMethod.
                                              CalibrationTargets[i].
                                              targetCoordinates));

                    for (int j = 0; j < calibration.CalibMethod.CalibrationTargets[i].NumImages; j++)
                    {
                        calibration.CalibMethod.CalibrationTargets[numRecalibTargets].pupilCentersLeft.Add(
                            calibration.CalibMethod.CalibrationTargets[i].pupilCentersLeft[j]);

                        calibration.CalibMethod.CalibrationTargets[numRecalibTargets].pupilCentersRight.Add(
                            calibration.CalibMethod.CalibrationTargets[i].pupilCentersRight[j]);

                        if (Settings.Instance.Processing.TrackingGlints)
                        {
                            calibration.CalibMethod.CalibrationTargets[numRecalibTargets].glintsLeft.Add(
                                calibration.CalibMethod.CalibrationTargets[i].glintsLeft[j]);

                            calibration.CalibMethod.CalibrationTargets[numRecalibTargets].glintsRight.Add(
                                calibration.CalibMethod.CalibrationTargets[i].glintsRight[j]);
                        }
                    }
                    numRecalibTargets++;
                }
            }
        }
    }
}