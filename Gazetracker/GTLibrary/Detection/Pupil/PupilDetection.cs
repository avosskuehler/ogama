using System;
using System.Linq;
using Emgu.CV;
using Emgu.CV.Structure;

using GTCommons.Enum;
using GTSettings;

namespace GTLibrary.Detection.Pupil
{
  using GTLibrary.Detection.BlobAnalysis;
  using GTLibrary.Utils;

  public class PupilDetection
    {
        #region Variables

        private readonly EyeEnum eye;
        private BlobDetector blobDetector;
        private bool foundPupil;
        private PupilData pupilData;

        #endregion

        #region Delegates

        public delegate void AutoTunedEventHandler(bool success);

        #endregion

        #region Constructor

        public PupilDetection(EyeEnum eye)
        {
            Init();
            pupilData.Eye = eye;
            this.eye = eye;
        }

        public PupilDetection()
        {
            Init();
        }

        private void Init()
        {
            blobDetector = new BlobDetector();
            pupilData = new PupilData();
        }

        #endregion

        #region Detect a pupil

        public bool DetectPupil(Image<Gray, byte> inputImage, TrackData trackData)
        {
            foundPupil = false;
            var initialLocation = new GTPoint(inputImage.Width/2, inputImage.Height/2);

            if (eye == EyeEnum.Left)
                PupilGrayLevel = Settings.Instance.Processing.PupilThresholdLeft;
            else
                PupilGrayLevel = Settings.Instance.Processing.PupilThresholdRight;

            MinPupilSize = Settings.Instance.Processing.PupilSizeMinimum;
            MaxPupilSize = Settings.Instance.Processing.PupilSizeMaximum;

            var min = (int) Math.Round(Math.PI*Math.Pow(MinPupilSize, 2));
            var max = (int) Math.Round(Math.PI*Math.Pow(MaxPupilSize, 2));

            Blobs blobs = blobDetector.DetectBlobs(inputImage, PupilGrayLevel, min, max, false);

            #region Autotuning data store

            if (eye == EyeEnum.Left)
            {
                trackData.UnfilteredBlobCountLeft = blobs.Count;
                trackData.UnfilteredTotalBlobAreaLeft = blobs.TotalArea;
            }
            else
            {
                trackData.UnfilteredBlobCountRight = blobs.Count;
                trackData.UnfilteredTotalBlobAreaRight = blobs.TotalArea;
            }

            #endregion

            //if (blobDetector.IsFiltering == false)
            //    blobs.FilterByArea(10, (int)blobs.TotalArea);

            //Console.WriteLine("Average fullness: {0}", blobs.AverageFullness);

            blobs.EliminateExteriorBlobs();

            if (blobDetector.IsFiltering == false)
                blobs.FilterByArea(min, max);

            if (blobs.Count > 1)
                blobs.FilterByDistance(initialLocation);

            // New, filter by fullness
            //blobs.FilterByFullness(0.40);

            if (blobs.Count > 0)
            {
                //blobDetector.blobCounter.ExtractBlobsImage(inputImage.ToBitmap(), blobs.BlobDir.ElementAt(0).Value, false);
                pupilData.Blob = blobs.BlobDir.ElementAt(0).Value;

                if (pupilData.Blob != null)
                {
                    foundPupil = true;
                    pupilData.Center = new GTPoint(pupilData.Blob.CenterOfGravity.X, pupilData.Blob.CenterOfGravity.Y);

                    // We save the values of the gray level in the corners of rectangle around the pupil blob (which are on the iris)
                    // Javier, the array on EmguGray is [y,x] not [x,y]
                    int x = pupilData.Blob.Rectangle.X;
                    int y = pupilData.Blob.Rectangle.Y;
                    int w = pupilData.Blob.Rectangle.Width;
                    int h = pupilData.Blob.Rectangle.Height;

                    pupilData.GrayCorners[0] = (int) inputImage[y, x].Intensity;
                    pupilData.GrayCorners[1] = (int) inputImage[y, x + w - 1].Intensity;
                    pupilData.GrayCorners[2] = (int) inputImage[y + h - 1, x].Intensity;
                    pupilData.GrayCorners[3] = (int) inputImage[y + h - 1, x + w - 1].Intensity;
                }
            }
            else
                foundPupil = false;

            return foundPupil;
        }

        #endregion

        #region Detect a pupil given a GlintConfiguration structure

        /// <summary>
        /// Detect the pupil in a grayscale image. The blob closest to the glints will
        /// be considered to be the pupil. To do: check for eccentricity
        /// </summary>
        /// <param name="inputImage">Input image in grayscale</param>
        /// <param name="pupilGrayLevel">Gray level used to threshold the image</param>
        /// <param name="minPupilSize">Minimum pupil size allowed</param>
        /// <param name="maxPupilSize">Maximum pupil size allowed</param>
        /// <param name="glints">Glints detected in the image, passed as a GlintConfiguration
        /// class</param>
        /// <returns></returns>
        //public bool DetectPupil(Image<Gray, byte> inputImage, int pupilGrayLevel,
        //    int minPupilSize, int maxPupilSize, GlintConfiguration glints)
        //{
        //    bool errorPupil = true;
        //    List<Blob> filteredBlobs = new List<Blob>();

        //if(eye == EyeEnum.Left)
        //    this.pupilGrayLevel = Settings.Instance.Processing.PupilThresholdLeft;
        //else
        //    this.pupilGrayLevel = Settings.Instance.Processing.PupilThresholdRight;
        //    this.minPupilSize = minPupilSize;
        //    this.maxPupilSize = maxPupilSize;

        //    blobResult = new BlobResult(inputImage, pupilGrayLevel, true);

        //    blobResult.FilterByArea((int)(Math.PI * Math.Pow(minPupilSize, 2)), (int)(Math.PI * Math.Pow(maxPupilSize, 2)));


        //    if (blobResult.detectedBlobs.Length > 0)
        //    {
        //        pupilData.Blob = blobResult.FilterByDistance(glints.averageCenter);

        //        if (pupilData.Blob != null)
        //            errorPupil = false;
        //        else
        //            errorPupil = true;

        //    }

        //    else
        //        errorPupil = true;

        //    if (blobResult != null)
        //        blobResult.ClearBlobResult();

        //    return errorPupil;
        //}

        #endregion

        #region Detect two pupils

        /// <summary>
        /// Detect two pupils
        /// </summary>
        /// <param name="inputImage">Input image in grayscale</param>
        /// <returns></returns>
        public bool DetectTwoPupils(Image<Gray, byte> inputImage, TrackData trackData)
        {
            foundPupil = false;
            var initialLocation = new GTPoint(inputImage.Width/2, inputImage.Height/2);

            PupilGrayLevel = Settings.Instance.Processing.PupilThreshold;
            MinPupilSize = Settings.Instance.Processing.PupilSizeMinimum;
            MaxPupilSize = Settings.Instance.Processing.PupilSizeMaximum;

            double min = Math.PI*Math.Pow(MinPupilSize, 2);
            double max = Math.PI*Math.Pow(MaxPupilSize, 2);

            Blobs blobs = blobDetector.DetectBlobs(inputImage, PupilGrayLevel, (int) min, (int) max, false);
            blobs.EliminateExteriorBlobs();

            if (blobDetector.IsFiltering == false)
                blobs.FilterByArea((int) min, (int) max);

            // We have 2 or more candidates
            if (blobs.Count > 1)
            {
                pupilData.Blob = blobs.FilterByDistance(initialLocation);

                if (pupilData.Blob != null)
                {
                    foundPupil = true;
                    pupilData.Center = new GTPoint(pupilData.Blob.CenterOfGravity.X, pupilData.Blob.CenterOfGravity.Y);
                }
            }
            else
                foundPupil = false;

            return foundPupil;
        }

        #endregion

        #region Getters and setters

        public PupilData PupilData
        {
            get { return pupilData; }
            set { pupilData = value; }
        }

        public bool FoundPupil
        {
            get { return foundPupil; }
        }

        public int PupilGrayLevel { get; set; }

        public int MinPupilSize { get; set; }

        public int MaxPupilSize { get; set; }


        //public int numRays
        //{
        //    get { return m_numRays; }
        //    set { m_numRays = value; }
        //}
        //public int numSamplingPoints
        //{
        //    get { return m_numSamplingPoints; }
        //    set { m_numSamplingPoints = value; }
        //}
        //public double maxDistForRansac
        //{
        //    get { return m_maxDistForRansac; }
        //    set { m_maxDistForRansac = value; }
        //}
        //public int numInliers
        //{
        //    get { return m_numInliers; }
        //    set { m_numInliers = value; }
        //}

        #endregion

        //private int m_numRays;
        //private int m_numSamplingPoints;	
        //private double m_maxDistForRansac;
    }
}