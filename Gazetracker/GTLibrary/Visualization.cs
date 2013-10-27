using System;
using System.Drawing;
using Emgu.CV;
using Emgu.CV.Structure;

using GTCommons.Enum;
using GTSettings;

namespace GTLibrary
{
  using GTLibrary.Detection.Glint;
  using GTLibrary.Log;
  using GTLibrary.Utils;

  public class Visualization
    {
        #region Variables

        private Image<Gray, byte> gray;
        private Image<Bgr, byte> processed;
        private TrackData trackData;
        private int height;
        private int width;

        #endregion

        #region Public methods (Visualize)

        #region Get/Set

        public Image<Gray, byte> Gray
        {
            get { return gray; }
            set { gray = value; }
        }

        public Image<Bgr, byte> Processed
        {
            get { return processed; }
            set { processed = value; }
        }

        public TrackData TrackData
        {
            set { trackData = value; }
        }

        #endregion

        #region Visualize-On-Demand

        public void VisualizeOnDemand()
        {
            Visualize(trackData, gray);
        }

        public Image<Gray, byte> VisualizeOnDemandGray()
        {
            if (gray != null)
            {
                Visualize(trackData);
                return gray;
            }
            else
                return new Image<Gray, byte>(1, 1);
        }

        public Image<Bgr, byte> VisualizeOnDemandProcessed()
        {
            if (gray != null)
            {
                Visualize(trackData);
                return processed;
            }
            else
                return new Image<Bgr, byte>(1, 1);
        }

        #endregion


        public void Visualize(TrackData trackData, Image<Gray, byte> image)
        {
            gray = image;
            Visualize(trackData);
        }

        public void Visualize(TrackData trackData)
        {
            if (gray == null)
                return;

            if (Settings.Instance.Visualization.VideoMode == VideoModeEnum.RawNoTracking)
                return; // no further actions

            #region Paint processed

            if (Settings.Instance.Visualization.VideoMode == VideoModeEnum.Processed)
            {
                processed = gray.Convert<Bgr, byte>();
                width = processed.Width;
                height = processed.Height;

                #region Draw threshold pupil

                if (Settings.Instance.Visualization.DrawPupil)
                {
                    // Left
                    if (trackData.LeftROI.Y > 0) //roi
                        ThresholdColorizePupil(
                            trackData.LeftROI,
                            Settings.Instance.Processing.PupilThresholdLeft,
                            Settings.Instance.Visualization.PupilThresholdColor);

                    // Right
                    if (trackData.RightROI.Y > 0) //roi
                        ThresholdColorizePupil(
                            trackData.RightROI,
                            Settings.Instance.Processing.PupilThresholdRight,
                            Settings.Instance.Visualization.PupilThresholdColor);

                    if (trackData.LeftROI.Y == 0 && trackData.RightROI.Y == 0) //full image
                        ThresholdColorizePupilFullImage();
                }

                #endregion

                #region Draw glints glints

                if (Settings.Instance.Processing.TrackingGlints)
                {
                    if (trackData.LeftROI.Y > 0) //roi
                        ThresholdColorizeGlints(
                            trackData.LeftROI,
                            Settings.Instance.Processing.GlintThresholdLeft,
                            Settings.Instance.Visualization.GlintThresholdColor);

                    if (trackData.RightROI.Y > 0) //roi
                        ThresholdColorizeGlints(trackData.RightROI,
                                                Settings.Instance.Processing.GlintThresholdRight,
                                                Settings.Instance.Visualization.GlintThresholdColor);

                    if (trackData.LeftROI.Y == 0 && trackData.RightROI.Y == 0) //full image
                        ThresholdColorizeGlintsFullImage();
                }

                #endregion
            }

            #endregion

            #region Draw roi, pupil, glint crosses etc.

            // Eye ROI
            if (Settings.Instance.Visualization.DrawEyesROI && trackData.EyesROI.Width != 0)
                DrawEyesROI(trackData.EyesROI);

            if (Settings.Instance.Visualization.DrawEyeROI)
            {
                if (trackData.LeftROI.Width != 0)
                    DrawEyeROI(trackData.LeftROI);

                if (trackData.RightROI.Width != 0)
                    DrawEyeROI(trackData.RightROI);
            }

            // Pupil
            if (Settings.Instance.Visualization.DrawPupil)
            {
                if (trackData.PupilDataLeft.Center.X != 0)
                    DrawPupil(trackData.PupilDataLeft.Center.ToPoint(), Settings.Instance.Processing.PupilSizeMaximum*2);

                if (trackData.PupilDataRight.Center.X != 0)
                    DrawPupil(trackData.PupilDataRight.Center.ToPoint(), Settings.Instance.Processing.PupilSizeMaximum*2);
            }

            // Glint
            if (Settings.Instance.Processing.TrackingGlints)
            {
                if (trackData.GlintDataLeft.Glints != null && trackData.GlintDataLeft.Glints.Count != 0 &&
                    trackData.GlintDataLeft.Glints.Centers[0].X != 0)
                    DrawGlints(trackData.GlintDataLeft.Glints, Settings.Instance.Processing.GlintSizeMaximum/2);

                if (trackData.GlintDataRight.Glints != null && trackData.GlintDataRight.Glints.Count != 0 &&
                    trackData.GlintDataRight.Glints.Centers[0].X != 0)
                    DrawGlints(trackData.GlintDataRight.Glints, Settings.Instance.Processing.GlintSizeMaximum/2);
            }

            #endregion

            Performance.Now.Stamp("Visualized");
        }

        #endregion


        #region Private paint thresholds

        #region Threshold sub-roi

        private void ThresholdColorizePupil(Rectangle roi, int threshold, Color color)
        {
            // Check roi setting procedures.. for now return
            if (Operations.IsWithinBounds(roi, gray.ROI) == false)
                return;

            // Create thresholded images
            gray.ROI = roi;
            Image<Gray, byte> pupilImage = gray.ThresholdBinaryInv(new Gray(threshold), new Gray(255));
            gray.ROI = Rectangle.Empty;

            // Pixel data is stored on a zero based array, reduce roi size by one
            if(roi.Width > 0)
                roi.Width -= 1;
            if(roi.Height > 0)
                roi.Height -= 1;

            // Set local variabels to avoid calling properties, expensive on "glintImage.Data[,,,]"
            int w = pupilImage.Width;
            int h = pupilImage.Height;
            byte[,,] dataPupil = pupilImage.Data;
            byte[,,] dataProcessed = processed.Data;

            // Replace pixles in the processed color image with values from the sub-roi pixels that are not 0
            int x, y;

            for (y = 0; y < h; y++)
            {
                for (x = 0; x < w; x++)
                {
                    if (dataPupil[y, x, 0] != 0)
                    {
                        // This is the "fast" way of doin' it
                        dataProcessed[y + roi.Y, x + roi.X, 0] = color.B;
                        dataProcessed[y + roi.Y, x + roi.X, 1] = color.G;
                        dataProcessed[y + roi.Y, x + roi.X, 2] = color.R;
                    }
                }
            }
            // Now that it has been painted in the "full" image we dispose
            pupilImage.Dispose();
        }

        private void ThresholdColorizeGlints(Rectangle roi, int threshold, Color color)
        {
            // Check roi setting procedures.. for now return
            if (Operations.IsWithinBounds(roi, gray.ROI) == false)
                return;

            // Create thresholded images
            gray.ROI = roi;
            Image<Gray, byte> glintImage = gray.ThresholdBinaryInv(new Gray(threshold), new Gray(255));
            gray.ROI = Rectangle.Empty;

            // Pixel data is stored on a zero based array, reduce roi size by one
            if(roi.Width > 0)
                roi.Width -= 1;
            if(roi.Height > 0)
                roi.Height -= 1;

            // Set local variabels to avoid calling properties, expensive on "glintImage.Data[,,,]"
            int w = glintImage.Width;
            int h = glintImage.Height;
            byte[,,] dataPupil = glintImage.Data;
            byte[,,] dataProcessed = processed.Data;

            // Replace pixles in the processed color image with values from the sub-roi pixels that are not 0
            int x, y;

            for (x = 0; x < w; x++)
            {
                for (y = 0; y < h; y++)
                {
                    if (dataPupil[y, x, 0] == 0)
                    {
                        dataProcessed[y + roi.Y, x + roi.X, 0] = color.B;
                        dataProcessed[y + roi.Y, x + roi.X, 1] = color.G;
                        dataProcessed[y + roi.Y, x + roi.X, 2] = color.R;
                    }
                }
            }
            // Now that it has been painted in the "full" image we dispose
            glintImage.Dispose();
        }

        #endregion

        #region Threshold whole image

        private void ThresholdColorizePupilFullImage()
        {
            // Threshold (whole image)
            Image<Gray, byte> pupilImage = gray.ThresholdBinaryInv(new Gray(Settings.Instance.Processing.PupilThreshold), new Gray(255));

            // Convert thresholded to color and add
            Image<Bgr, byte> pupilThresholdImage = new Image<Bgr, byte>(width, height, new Bgr(Settings.Instance.Visualization.PupilThresholdColor));
            pupilThresholdImage = pupilThresholdImage.And(pupilImage.Convert<Bgr, byte>());
            processed = processed.Add(pupilThresholdImage);

            pupilThresholdImage.Dispose();
            pupilImage.Dispose();
        }

        private void ThresholdColorizeGlintsFullImage()
        {
            // Threshold (whole image)
            Image<Gray, byte> glintImage = gray.ThresholdBinary(new Gray(Settings.Instance.Processing.GlintThreshold), new Gray(255));

            // Convert thresholded to color and add
            // Negate the selected color (unary minus) otherwise yellow becomes blue, green = red etc. 
            Color c = Settings.Instance.Visualization.GlintThresholdColor;
            Color glintNegated = Color.FromArgb(c.A, 255 - c.R, 255 - c.G, 255 - c.B);

            Image<Bgr, byte> glintThresholdImage = new Image<Bgr, byte>(width, height, new Bgr(glintNegated));
            glintThresholdImage = glintThresholdImage.And(glintImage.Convert<Bgr, byte>());
            processed = processed.Sub(glintThresholdImage);

            glintThresholdImage.Dispose();
            glintImage.Dispose();
        }

        #endregion

        #endregion

        #region Private draw methods (eye, pupil, glint)

        private void DrawEyesROI(Rectangle eyesROI)
        {
            if (eyesROI != null)
            {
                switch (Settings.Instance.Visualization.VideoMode)
                {
                    case VideoModeEnum.Normal:
                        if (Operations.IsWithinBounds(eyesROI, gray.ROI))
                            gray.Draw(eyesROI, new Gray(255), 1);
                        break;

                    case VideoModeEnum.Processed:
                        if (Operations.IsWithinBounds(eyesROI, gray.ROI))
                            processed.Draw(eyesROI, new Bgr(Color.White), 1);
                        break;
                }
            }
        }

        private void DrawEyeROI(Rectangle rectangle)
        {
            switch (Settings.Instance.Visualization.VideoMode)
            {
                case VideoModeEnum.Normal:
                    if(Operations.IsWithinBounds(rectangle, gray.ROI))
                       gray.Draw(rectangle, new Gray(Settings.Instance.Visualization.EyeROIGray), 1);
                    break;

                case VideoModeEnum.Processed:
                    if (Operations.IsWithinBounds(rectangle, processed.ROI))
                        processed.Draw(rectangle, new Bgr(Settings.Instance.Visualization.EyeROIColor), 1);
                    break;
            }
        }

        private void DrawPupil(Point pupilCenter, int lineLenght)
        {
            switch (Settings.Instance.Visualization.VideoMode)
            {
                case VideoModeEnum.Normal:
                    gray.Draw(new Cross2DF(pupilCenter, lineLenght, lineLenght), new Gray(Settings.Instance.Visualization.PupilCrossGray), 1);
                    break;

                case VideoModeEnum.Processed:
                    processed.Draw(new Cross2DF(pupilCenter, lineLenght, lineLenght), new Bgr(Settings.Instance.Visualization.PupilCrossColor), 1);
                    break;
            }
        }

        private void DrawGlints(GlintConfiguration glints, int lineLenght)
        {
            if (glints != null)
            {
                for (int i = 0; i < glints.Count; i++)
                {
                    Point gcPoint = new Point(Convert.ToInt32(glints.Centers[i].X), Convert.ToInt32(glints.Centers[i].Y));

                    switch (Settings.Instance.Visualization.VideoMode)
                    {
                        case VideoModeEnum.Normal:
                            gray.Draw(new Cross2DF(gcPoint, lineLenght, lineLenght), new Gray(Settings.Instance.Visualization.GlintCrossGray), 1);
                            break;

                        case VideoModeEnum.Processed:
                            processed.Draw(new Cross2DF(gcPoint, lineLenght, lineLenght), new Bgr(Settings.Instance.Visualization.GlintCrossColor), 1);
                            break;
                    }
                }
            }
        }

        #endregion

        #region Private Draw-shapes methods (rectangle, cross) - Not used anymore (reducing method calls)

        /// <summary>
        /// This method draws a cross at the given position.
        /// </summary>
        /// <param name="image">The input image.</param>
        /// <param name="point">The position of the cross.</param>
        /// <param name="size">The size of the cross.</param>
        /// <param name="color">The color of the cross.</param>
        /// <param name="thickness">The thickness of the cross.</param>
        private static void DrawCross(Image<Bgr, byte> image, Point point, int size, Color color, int thickness)
        {
            if (image == null)
                return;

            image.Draw(new Cross2DF(point, size, size), new Bgr(color), thickness);
        }

        /// <summary>
        /// This method draws a cross at the given position.
        /// </summary>
        /// <param name="image">The input image.</param>
        /// <param name="point">The position of the cross.</param>
        /// <param name="size">The size of the cross.</param>
        /// <param name="color">The color of the cross.</param>
        /// <param name="thickness">The thickness of the cross.</param>
        private static void DrawCross(Image<Gray, byte> image, Point point, int size, int grayScale, int thickness)
        {
            if (image == null)
                return;

            image.Draw(new Cross2DF(point, size, size), new Gray(grayScale), thickness);
        }

        /// <summary>
        /// This method draws a circle at the given position.
        /// </summary>
        /// <param name="image">The input image.</param>
        /// <param name="circle">The position of the circle.</param>
        /// <param name="color">The color of the cross.</param>
        /// <param name="thickness">The thickness of the cross.</param>
        private static void DrawCircle(Image<Gray, byte> image, CircleF circle, int grayScale, int thickness)
        {
            //image.Draw(circle, new Gray(grayScale), thickness);
        }

        private static void DrawCircle(Image<Bgr, byte> image, CircleF circle, Color color, int thickness)
        {
            //image.Draw(circle, new Bgr(color), thickness);
        }

        private static void DrawRectangle(Image<Bgr, byte> image, Rectangle rectangle, Color color, int thickness)
        {
            if (image == null)
                return;

            if (rectangle.Width > 0 && rectangle.Height > 0)
                image.Draw(rectangle, new Bgr(color), thickness);
        }

        private static void DrawRectangle(Image<Gray, byte> image, Rectangle rectangle, int grayScale, int thickness)
        {
            if (image == null)
                return;

            if (rectangle.Width > 0 && rectangle.Height > 0)
                image.Draw(rectangle, new Gray(grayScale), thickness);
        }

        #endregion
    }
}