using System;
using Emgu.CV;
using Emgu.CV.Structure;

namespace GTLibrary.Detection.Glint
{
  using GTLibrary.Detection.BlobAnalysis;
  using GTLibrary.Utils;

  public partial class GlintDetection
    {
        #region Detect single glint

        #region Detect glints within a specific area range

        /// <summary>
        /// Detect glints in a grayscale image.
        /// This method just thresholds the image with the given gray level and
        /// selects the blobs that are within the given minimum and maximum sizes
        /// </summary>
        /// <param name="inputImage">Input image in grayscale</param>
        /// <param name="glintThreshold">Gray level to threshold the image</param>
        /// <param name="minGlintSize">Minimum glint size allowed (radius in pixels)</param>
        /// <param name="maxGlintSize">Maximum glint size allowed (radius in pixels)</param>
        /// <returns>False if glint(s) detected, true otherwise</returns>
        public bool DetectGlints(Image<Gray, byte> inputImage, int glintThreshold, int minGlintSize, int maxGlintSize)
        {
            bool errorGlints = true;

            GlintThreshold = glintThreshold;
            MinGlintSize = minGlintSize;
            MaxGlintSize = maxGlintSize;

            var min = (int) Math.Round(Math.PI*Math.Pow(minGlintSize, 2));
            var max = (int) Math.Round(Math.PI*Math.Pow(maxGlintSize, 2));

            // We get the blobs in the input image given the threshold
            blobs = blobDetector.DetectBlobs(inputImage, glintThreshold, min, max, true);
            glintData.Glints.UnfilteredCount = blobs.Count;
            glintData.Glints.UnfilteredTotalArea = blobs.TotalArea;

            // Filter out exterior blobs
            blobs.EliminateExteriorBlobs();

            //Filter out the undesired glints by size
            if (blobDetector.IsFiltering == false)
                blobs.FilterByArea(min, max);

            // We store all the glints in the "glints" structure
            glintData.Glints = new GlintConfiguration(blobs.Count);
            glintData.Glints.Blobs = blobs;

            // store blobcount for autotune
            glintData.Glints.UnfilteredCount = blobs.Count;
            glintData.Glints.UnfilteredTotalArea = blobs.TotalArea;

            int count = 0;
            foreach (Blob b in blobs.BlobList)
            {
                glintData.Glints.Centers[count] = new GTPoint(b.CenterOfGravity.X, b.CenterOfGravity.Y);
                count++;
            }


            if (glintData.Glints.Count > 0)
                errorGlints = false;

            return errorGlints;
        }

        #endregion

        #region Detect the glint closest to a specified pair of coordinates

        /// <summary>
        /// Detect glints in a grayscale image.
        /// This method will select the blob closest to coordinates of initialLocation
        /// </summary>
        /// <param name="inputImage">Input image in grayscale</param>
        /// <param name="glintThreshold">Gray level to threshold the image</param>
        /// <param name="minGlintSize">Minimum glint size allowed (radius in pixels)</param>
        /// <param name="maxGlintSize">Maximum glint size allowed (radius in pixels)</param>
        /// <param name="initialLocation">Select the glint closest to this parameter</param>
        /// <returns>True if glint detected, true otherwise</returns>
        public bool DetectGlint(Image<Gray, byte> inputImage, int glintThreshold,
                                int minGlintSize, int maxGlintSize, GTPoint initialLocation)
        {
            bool foundGlints = false;

            GlintThreshold = glintThreshold;
            MinGlintSize = minGlintSize;
            MaxGlintSize = maxGlintSize;

            var min = (int) Math.Round(Math.PI*Math.Pow(minGlintSize, 2));
            var max = (int) Math.Round(Math.PI*Math.Pow(maxGlintSize, 2));

            // We get the blobs in the input image given the threshold
            blobs = blobDetector.DetectBlobs(inputImage, glintThreshold, min, max, true);

            // store blobcount for autotune
            glintData.Glints.UnfilteredCount = blobs.Count;
            glintData.Glints.UnfilteredTotalArea = blobs.TotalArea;

            // Filter out exterior blobs
            if (blobs.Count > 1)
                blobs.EliminateExteriorBlobs();

            if (blobDetector.IsFiltering == false) // Not using AForger filtering
                blobs.FilterByArea(min, max);

            if (blobs.Count > 1)
                blobs.FilterByDistance(initialLocation);

            if (blobs.Count > 0)
            {
                glintData.Glints = new GlintConfiguration(blobs);
                foundGlints = true;
            }

            return foundGlints;
        }

        #endregion

        #region Detect the glint closest to and below a specified pair of coordinates 

        /// <summary>
        /// Detect glints in a grayscale image.
        /// This method will select the blob closest to coordinates of initialLocation 
        /// that has a higher y coordinate (i.e., corresponds to having the light source
        /// below the screen)
        /// </summary>
        /// <param name="inputImage">Input image in grayscale</param>
        /// <param name="glintThreshold">Gray level to threshold the image</param>
        /// <param name="minGlintSize">Minimum glint size allowed (radius in pixels)</param>
        /// <param name="maxGlintSize">Maximum glint size allowed (radius in pixels)</param>
        /// <param name="initialLocation">Select the glint closest to this parameter</param>
        /// <returns>True if glint detected, true otherwise</returns>
        public bool DetectGlintBelow(Image<Gray, byte> inputImage, int glintThreshold,
                                     int minGlintSize, int maxGlintSize, GTPoint initialLocation)
        {
            bool foundGlints = false;

            GlintThreshold = glintThreshold;
            MinGlintSize = minGlintSize;
            MaxGlintSize = maxGlintSize;

            var min = (int) Math.Round(Math.PI*Math.Pow(minGlintSize, 2));
            var max = (int) Math.Round(Math.PI*Math.Pow(maxGlintSize, 2));

            // We get the blobs in the input image given the threshold
            blobs = blobDetector.DetectBlobs(inputImage, glintThreshold, min, max, true);

            // store blobcount for autotune
            glintData.Glints.UnfilteredCount = blobs.Count;
            glintData.Glints.UnfilteredTotalArea = blobs.TotalArea;

            // Filter out exterior blobs
            if (blobs.Count > 1)
                blobs.EliminateExteriorBlobs();

            if (blobDetector.IsFiltering == false) // Not using AForger filtering
                blobs.FilterByArea(min, max);

            // Eliminate blobs above initialLocation (pupil center)
            blobs.FilterByLocation(initialLocation, 1, -1);

            if (blobs.Count > 1)
                blobs.FilterByDistance(initialLocation);

            if (blobs.Count > 0)
            {
                glintData.Glints = new GlintConfiguration(blobs);
                foundGlints = true;
            }

            return foundGlints;
        }

        #endregion

        #region Detect the glint closest to and above a specified pair of coordinates

        /// <summary>
        /// Detect glints in a grayscale image.
        /// This method will select the blob closest to coordinates of initialLocation 
        /// that has a lower y coordinate (i.e., corresponds to having the light source
        /// above the screen)
        /// </summary>
        /// <param name="inputImage">Input image in grayscale</param>
        /// <param name="glintThreshold">Gray level to threshold the image</param>
        /// <param name="minGlintSize">Minimum glint size allowed (radius in pixels)</param>
        /// <param name="maxGlintSize">Maximum glint size allowed (radius in pixels)</param>
        /// <param name="initialLocation">Select the glint closest to this parameter</param>
        /// <returns>True if glint detected, true otherwise</returns>
        public bool DetectGlintAbove(Image<Gray, byte> inputImage, int glintThreshold,
                                     int minGlintSize, int maxGlintSize, GTPoint initialLocation)
        {
            bool foundGlints = false;

            GlintThreshold = glintThreshold;
            MinGlintSize = minGlintSize;
            MaxGlintSize = maxGlintSize;

            var min = (int) Math.Round(Math.PI*Math.Pow(minGlintSize, 2));
            var max = (int) Math.Round(Math.PI*Math.Pow(maxGlintSize, 2));

            // We get the blobs in the input image given the threshold
            blobs = blobDetector.DetectBlobs(inputImage, glintThreshold, min, max, true);

            // store blobcount for autotune
            glintData.Glints.UnfilteredCount = blobs.Count;
            glintData.Glints.UnfilteredTotalArea = blobs.TotalArea;

            // Filter out exterior blobs
            if (blobs.Count > 1)
                blobs.EliminateExteriorBlobs();

            if (blobDetector.IsFiltering == false) // Not using AForger filtering
                blobs.FilterByArea(min, max);

            // Eliminate blobs below initialLocation (pupil center)
            blobs.FilterByLocation(initialLocation, 1, 1);

            if (blobs.Count > 1)
                blobs.FilterByDistance(initialLocation);

            if (blobs.Count > 0)
            {
                glintData.Glints = new GlintConfiguration(blobs);
                foundGlints = true;
            }

            return foundGlints;
        }

        #endregion

        #endregion
    }
}