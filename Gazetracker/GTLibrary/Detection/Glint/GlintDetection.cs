using System;
using Emgu.CV;
using Emgu.CV.Structure;

using GTCommons.Enum;
using GTSettings;

namespace GTLibrary.Detection.Glint
{
  using GTLibrary.Detection.BlobAnalysis;
  using GTLibrary.Utils;

  public partial class GlintDetection
    {
        #region Variables

        private readonly BlobDetector blobDetector;
        public Blobs blobResult;
        private Blobs blobs;
        public Blobs candidateGlints;
        private EyeEnum eye;
        private GlintData glintData;

        #endregion

        #region Constructor

        public GlintDetection(EyeEnum eye)
        {
            this.eye = eye;
            glintData = new GlintData();
            blobDetector = new BlobDetector();
        }

        #endregion

        #region Get/Set

        public GlintData GlintData
        {
            get { return glintData; }
            set { glintData = value; }
        }

        public EyeEnum Eye
        {
            get { return eye; }
            set { eye = value; }
        }

        public int GlintThreshold { get; set; }

        public int AngleValue { get; set; }

        public int MinGlintSize { get; set; }

        public int MaxGlintSize { get; set; }

        public GTPoint InitialLocation { get; set; }

        public int MinDistBetweenGlints { get; set; }

        public int MaxDistBetweenGlints { get; set; }

        #endregion

        /// <summary>
        /// Detect glint(s) main method (moved from ImageProcessing)
        /// </summary>
        /// <returns>True if glints detected, false otherwise</returns>
        public bool DetectGlints(Image<Gray, byte> gray, GTPoint pupilCenter)
        {
            bool glintsDetected = false;
            int threshold = Settings.Instance.Processing.GlintThreshold; // default for both eyes

            // Treshold to apply, seperate for each eye.
            if (eye == EyeEnum.Left)
                threshold = Settings.Instance.Processing.GlintThresholdLeft;
            else
                threshold = Settings.Instance.Processing.GlintThresholdRight;

            MinDistBetweenGlints = (int) Math.Floor(0.03*gray.Width);
            MaxDistBetweenGlints = (int) Math.Ceiling(0.5*gray.Width);


            switch (Settings.Instance.Processing.IRPlacement)
            {
                case IRPlacementEnum.Above:

                    if (Settings.Instance.Processing.NumberOfGlints == 1)
                    {
                        glintsDetected = DetectGlintAbove(
                            gray,
                            threshold,
                            Settings.Instance.Processing.GlintSizeMinimum,
                            Settings.Instance.Processing.GlintSizeMaximum,
                            pupilCenter);
                    }
                    else
                    {
                        glintsDetected = DetectTwoGlintsAbove(
                            gray,
                            threshold,
                            Settings.Instance.Processing.GlintSizeMinimum,
                            Settings.Instance.Processing.GlintSizeMaximum,
                            pupilCenter);
                    }
                    break;


                case IRPlacementEnum.Below:

                    if (Settings.Instance.Processing.NumberOfGlints == 1)
                    {
                        glintsDetected = DetectGlintBelow(
                            gray,
                            threshold,
                            Settings.Instance.Processing.GlintSizeMinimum,
                            Settings.Instance.Processing.GlintSizeMaximum,
                            pupilCenter);
                    }
                    else
                    {
                        glintsDetected = DetectTwoGlintsBelow(
                            gray,
                            threshold,
                            Settings.Instance.Processing.GlintSizeMinimum,
                            Settings.Instance.Processing.GlintSizeMaximum,
                            pupilCenter);
                    }
                    break;

                case IRPlacementEnum.None:

                    if (Settings.Instance.Processing.NumberOfGlints == 1)
                    {
                        glintsDetected = DetectGlint(
                            gray,
                            threshold,
                            Settings.Instance.Processing.GlintSizeMinimum,
                            Settings.Instance.Processing.GlintSizeMaximum,
                            pupilCenter);
                    }
                    else if (Settings.Instance.Processing.NumberOfGlints == 2)
                    {
                        glintsDetected = DetectTwoGlints(
                            gray,
                            threshold,
                            Settings.Instance.Processing.GlintSizeMinimum,
                            Settings.Instance.Processing.GlintSizeMaximum,
                            pupilCenter);
                    }
                    break;

                case IRPlacementEnum.BelowAndAbove:

                    if (Settings.Instance.Processing.NumberOfGlints == 2)
                    {
                        glintsDetected = DetectTwoGlints(
                            gray,
                            threshold,
                            Settings.Instance.Processing.GlintSizeMinimum,
                            Settings.Instance.Processing.GlintSizeMaximum,
                            pupilCenter);
                    }
                    break;
            }
            //Performance.Now.Stamp("Glint detected");

            return glintsDetected;
        }
    }
}