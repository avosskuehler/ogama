using System;
using System.Drawing;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;

using GTSettings;

namespace GTLibrary.Detection.Eyes
{
  using GTLibrary.Log;
  using GTLibrary.Logging;

  public class EyesTracker
    {
        #region Variables

        private HaarCascade haarCascade;
        private bool foundEyes;
        private bool isReady;
        private Rectangle roiEyes;

        private Point startingLeftEyePointOptimized = new Point(0, 0);
        private Point startingPointSearchEyes = new Point(0, 0);

        #endregion

        #region Construction

        public EyesTracker()
        {
            roiEyes = new Rectangle(new Point(0, 0), new Size(0, 0));
            try
            {
                Settings.Instance.Eyestracker.OnHaarCascadeLoaded += EyestrackerSettings_OnHaarCascadeLoaded;

                if (haarCascade == null)
                    haarCascade = new HaarCascade(GTSettings.Settings.Instance.Eyestracker.HaarCascadePath);
            }
            catch (Exception ex)
            {
                ErrorLogger.ProcessException(ex, true);
            }
        }

        #endregion

        #region Public methods

        public bool DetectEyes(Image<Gray, byte> input, TrackData trackData)
        {
            if (input == null)
                return false;

            return DoEyesRegionExtraction(input, trackData);
        }

        #endregion

        #region Private methods

        private void EyestrackerSettings_OnHaarCascadeLoaded(bool success)
        {
            if (success)
                isReady = true;
            else
                isReady = false;
        }

        private bool DoEyesRegionExtraction(Image<Gray, Byte> input, TrackData trackData)
        {
            // We assume there's only one face in the video
            MCvAvgComp[][] facesDetected = input.DetectHaarCascade(
                haarCascade,
                Settings.Instance.Eyestracker.ScaleFactor,
                2, //Min. neighbours, higher value reduces false detection
                HAAR_DETECTION_TYPE.FIND_BIGGEST_OBJECT,
                Settings.Instance.Eyestracker.SizeMin);

            if (facesDetected[0].Length == 1)
            {
                MCvAvgComp face = facesDetected[0][0];

                if (face.rect.X != 0 && face.rect.Width != 0)
                {
                    if (face.rect.Height < 100)
                        return false;

                    roiEyes = face.rect;
                    // Add some margin
                    //roiEyes.Y = Convert.ToInt32(roiEyes.Y * 0.90);
                    roiEyes.X = Convert.ToInt32(roiEyes.X*0.85);
                    roiEyes.Height = Convert.ToInt32(roiEyes.Height*1.2);
                    roiEyes.Width = Convert.ToInt32(roiEyes.Width*1.4);
                    foundEyes = true;
                    trackData.EyesROI = roiEyes;
                }
            }
            else
            {
                foundEyes = false;
                roiEyes = new Rectangle(new Point(0, 0), new Size(0, 0));
            }

            Performance.Now.Stamp("Eyes X:" + roiEyes.X + " Y:" + roiEyes.Y + " W:" + roiEyes.Width + " H:" +
                                  roiEyes.Height);

            return foundEyes;
        }


        // Old code below, used to be in EyesROIManager

        public Rectangle GetEstimatedEyeROI()
        {
            return new Rectangle(new Point(0, 0), new Size(0, 0));
            // This code is for estimating a eyes region when using the full frontal face classifier
            //

            //if (CameraControl.Instance.IsROISet)
            //    return new Rectangle(0, 0, CameraControl.Instance.ROI.Width, CameraControl.Instance.ROI.Height);

            //Rectangle face = this.GetAvgROI();
            //Rectangle eyes = this.InstanceROI;

            // This code is for estimating a eyes region when using the full frontal face classifier
            //
            //Rectangle estEyeROI = new Rectangle();
            //estEyeROI.Y = Convert.ToInt32(face.Top - 125); // + (face.Height * 2.8 / 11));
            //estEyeROI.X = face.X;
            ////estEyeROI.Size =  new Size(face.Width, (face.Height * 2 / 9));
            //estEyeROI.Size = new Size(face.Width, face.Height); //Camera.CameraControl.Instance.Capture.Height/6);

            //return estEyeROI;
        }

        #endregion

        #region Get/Set

        public bool IsReady
        {
            get { return isReady; }
        }


        public Rectangle EyesROI
        {
            get { return roiEyes; }
        }

        #endregion
    }
}