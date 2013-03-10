namespace GazeTrackingLibrary
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Emgu.CV;
    using Emgu.CV.Structure;
    using GazeTrackingLibrary.Settings;
    using GazeTrackingLibrary.Utils;
    using System.Drawing;
    using GazeTrackingLibrary.GlintDetection;

    class TrackerVisualization
    {
        #region FIELDS

        /// <summary>
        /// Holds the to gray converted video frame.
        /// </summary>
        private Image<Gray, byte> gray;

        /// <summary>
        /// Holds the processed video frame with pupil and glint overlay
        /// </summary>
        private Image<Bgr, byte> processed;

        /// <summary>
        /// EyeROI Image
        /// </summary>
        private Image<Gray, byte> eyeROIImage;

        /// <summary>
        /// Holds the pupil blob detection frame.
        /// </summary>
        private Image<Gray, byte> pupilImage;

        /// <summary>
        /// Holds the glint blob detection frame.
        /// </summary>
        private Image<Gray, byte> glintImage;

        /// <summary>
        /// Image to paint pupil on the processed image
        /// </summary>
        private Image<Bgr, byte> green;

        /// <summary>
        /// Images to paint glints on the processed image
        /// </summary>
        private Image<Bgr, byte> red;

        private GTPoint pupilCenter;
        private GTPoint glintCenter;
        private GlintConfiguration glintsDetected;

        private List<Rectangle> eyeROIs = new List<Rectangle>();
        private Rectangle eyeROIAvg = new Rectangle();

        private bool foundGlints = false;
        private bool foundPupil = false;

        private System.Drawing.Color pupilCrossColor = System.Drawing.Color.Black;
        private System.Drawing.Color pupilCrossGray = System.Drawing.Color.Black;
        private System.Drawing.Color pupilCircleMinColor = System.Drawing.Color.Blue;
        private System.Drawing.Color pupilCircleMaxColor = System.Drawing.Color.Green;

        private System.Drawing.Color glintCircleMinColor = System.Drawing.Color.Blue;
        private System.Drawing.Color glintCircleMaxColor = System.Drawing.Color.Green;

        private System.Drawing.Color glintCrossColor = System.Drawing.Color.LightBlue;
        private System.Drawing.Color glintCrossGray = System.Drawing.Color.LightGray;

        private System.Drawing.Color pupilThresholdColor = System.Drawing.Color.LimeGreen;
        private System.Drawing.Color glintThresholdColor = System.Drawing.Color.DarkRed;


        #endregion

 
        #region Public methods (Visualize)

        /// <summary>
        /// Draws the results from the imageprocessing procedures
        /// </summary>
        public void Visualize(Image<Emgu.CV.Structure.Gray, byte> image)
        {
            this.gray = image;

            Rectangle roi = new System.Drawing.Rectangle(this.gray.ROI.Location, this.gray.ROI.Size);
            this.gray.ROI = new System.Drawing.Rectangle();

            if (GTSettings.Current.ProcessingSettings.IsVideoModeProcessed)
            {
                this.PaintProcessedImage();
            }

            // Always draw pupil graphics
            //DrawPupil(roi);
            DrawPupil();

            // Only draw glint graphics if tracking mode is not headmounted
            if (GTSettings.Current.ProcessingSettings.TrackingMethod != TrackingMethodEnum.Headmounted)
            {
                if (FoundGlints)
                    DrawGlints(roi);
            }

			//if (GTSettings.Current.ProcessingSettings.TrackingMethod == TrackingMethodEnum.RemoteEyeTracking)
			//{
                if(eyeROIAvg.Width != 0)
                   DrawEyeROI();
			//}
        }

        #endregion


        #region Private draw methods

        private void DrawEyeROI()
        {
            if (eyeROIAvg.X == 0 && eyeROIAvg.Y == 0)
                return;

            switch (GTSettings.Current.ProcessingSettings.VideoMode)
            {
                case VideoModeEnum.Normal:
                    //foreach (Rectangle rect in eyeROIs)
                    //{
                    //    this.DrawRectangle(this.gray, rect, new Gray(200), 1);
                    //}
                    this.DrawRectangle(this.gray, eyeROIAvg, new Gray(50), 1);
                    break;

                case VideoModeEnum.Processed:
                    //foreach (Rectangle rect in eyeROIs)
                    //{
                    //    this.DrawRectangle(this.processed, rect, Color.DarkOrange, 1);
                    //}
                    this.DrawRectangle(this.processed, eyeROIAvg, Color.DarkRed, 1);
                    break;
            }
        }

        private void DrawPupil()
        {
            DrawPupil(new Rectangle(new Point(0, 0), gray.Size));
        }

        private void DrawPupil(Rectangle roi)
        {
            if (FoundPupil)
            {
                CircleF pCircleMin = new CircleF(this.pupilCenter.ToPoint(), GTSettings.Current.ProcessingSettings.PupilSizeMinimum);
                CircleF pCircleMax = new CircleF(this.pupilCenter.ToPoint(), GTSettings.Current.ProcessingSettings.PupilSizeMaximum);

                switch (GTSettings.Current.ProcessingSettings.VideoMode)
                {
                    case VideoModeEnum.Normal:
                        this.DrawCross(this.gray, this.pupilCenter.ToPoint(), 800, pupilCrossGray, 1);
                        this.DrawCircle(this.gray, pCircleMin, new Gray(155), 1);
                        this.DrawCircle(this.gray, pCircleMax, new Gray(155), 1);
                        break;

                    case VideoModeEnum.Processed:
                        this.DrawCross(this.processed, this.pupilCenter.ToPoint(), 800, System.Drawing.Color.DarkGreen, 1);
                        this.DrawCircle(this.processed, pCircleMin, pupilCircleMinColor, 1);
                        this.DrawCircle(this.processed, pCircleMax, pupilCircleMaxColor, 1);
                        break;
                }
            }
        }

        private void DrawGlints()
        {
            DrawGlints(new Rectangle(new Point(0, 0), gray.Size));
        }

        private void DrawGlints(Rectangle roi)
        {
            for (int i = 0; i < this.glintsDetected.numGlints; i++)
            {
                double gcX = this.glintsDetected.centers[i].X + roi.Left;
                double gcY = this.glintsDetected.centers[i].Y + roi.Top;
                Point gcPoint = new Point(Convert.ToInt32(gcX), Convert.ToInt32(gcY));

                this.glintsDetected.centers[i].X = gcX;
                this.glintsDetected.centers[i].Y = gcY;

                CircleF gCircleMin = new CircleF(gcPoint, GTSettings.Current.ProcessingSettings.GlintSizeMinimum);
                CircleF gCircleMax = new CircleF(gcPoint, GTSettings.Current.ProcessingSettings.GlintSizeMaximum);

                switch (GTSettings.Current.ProcessingSettings.VideoMode)
                {
                    case VideoModeEnum.Normal:
                        this.DrawCross(this.gray, gcPoint, 800, glintCrossGray, 1);
                        this.DrawCircle(this.gray, gCircleMin, new Gray(155), 1);
                        this.DrawCircle(this.gray, gCircleMax, new Gray(155), 1);
                        break;

                    case VideoModeEnum.Processed:
                        this.DrawCross(this.processed, gcPoint, 800, glintCrossColor, 1);
                        this.DrawCircle(this.processed, gCircleMin, glintCircleMinColor, 1);
                        this.DrawCircle(this.processed, gCircleMax, glintCircleMaxColor, 1);
                        break;
                }
            }
        }

        /// <summary>
        /// This method overlays pupil and glint blobs over the original image.
        /// </summary>
        private void PaintProcessedImage()
        {
            int width = this.processed.Width;
            int height = this.processed.Height;

            // Draw pupil
            this.green = new Image<Bgr, byte>(width, height, new Bgr(this.pupilThresholdColor));
            this.green = this.green.And(this.pupilImage.Convert<Bgr, byte>());
            this.processed = this.processed.Add(this.green);

            // Draw glints
            if (GTSettings.Current.ProcessingSettings.TrackingMethod != TrackingMethodEnum.Headmounted)
            {
                this.red = new Image<Bgr, byte>(width, height, new Bgr(glintThresholdColor));
                this.red = this.red.And(this.glintImage.Convert<Bgr, byte>());
                this.processed = this.processed.Sub(this.red);
            }
        }

        #endregion


        #region Draw-shapes methods

        /// <summary>
        /// This method draws a cross at the given position.
        /// </summary>
        /// <param name="image">The input image.</param>
        /// <param name="point">The position of the cross.</param>
        /// <param name="size">The size of the cross.</param>
        /// <param name="color">The color of the cross.</param>
        /// <param name="thickness">The thickness of the cross.</param>
        private void DrawCross(Image<Bgr, byte> image, Point point, int size, System.Drawing.Color color, int thickness)
        {
            // Convert to System.Drawing.Point until EMGU updates its library
            System.Drawing.Point p1 = new System.Drawing.Point(point.X - (int)(size / 2), point.Y);
            System.Drawing.Point p2 = new System.Drawing.Point(point.X + (int)(size / 2), point.Y);

            image.Draw(new LineSegment2D(p1, p2), new Bgr(color), thickness);

            System.Drawing.Point p3 = new System.Drawing.Point(point.X, point.Y - (int)(size / 2));
            System.Drawing.Point p4 = new System.Drawing.Point(point.X, point.Y + (int)(size / 2));
            
            image.Draw(new LineSegment2D(p3, p4),  new Bgr(color), thickness);
        }

        /// <summary>
        /// This method draws a cross at the given position.
        /// </summary>
        /// <param name="image">The input image.</param>
        /// <param name="point">The position of the cross.</param>
        /// <param name="size">The size of the cross.</param>
        /// <param name="color">The color of the cross.</param>
        /// <param name="thickness">The thickness of the cross.</param>
        private void DrawCross(Image<Gray, byte> image, Point point, int size, System.Drawing.Color color, int thickness)
        {
            // Convert to System.Drawing.Point until EMGU updates its library
            System.Drawing.Point p1 = new System.Drawing.Point((int)point.X - (int)(size / 2), (int)point.Y);
            System.Drawing.Point p2 = new System.Drawing.Point((int)point.X + (int)(size / 2), (int)point.Y);

            image.Draw(new LineSegment2D(p1, p2), new Gray(color.GetBrightness()), thickness);

            System.Drawing.Point p3 = new System.Drawing.Point((int)point.X, (int)point.Y - (int)(size / 2));
            System.Drawing.Point p4 = new System.Drawing.Point((int)point.X, (int)point.Y + (int)(size / 2));
            
            image.Draw(new LineSegment2D(p3, p4), new Gray(color.GetBrightness()), thickness);
        }

        /// <summary>
        /// This method draws a circle at the given position.
        /// </summary>
        /// <param name="image">The input image.</param>
        /// <param name="circle">The position of the circle.</param>
        /// <param name="color">The color of the cross.</param>
        /// <param name="thickness">The thickness of the cross.</param>


        private void DrawCircle(Image<Gray, byte> image, CircleF circle, Gray gray, int thickness)
        {
            image.Draw(circle, gray, thickness);
        }

        private void DrawCircle(Image<Bgr, byte> image, CircleF circle, Color color, int thickness)
        {
            image.Draw(circle, new Bgr(color), thickness);
        }

        private void DrawRectangle(Image<Bgr, byte> image, Rectangle rectangle, Color color, int thickness)
        {
            image.Draw(rectangle, new Bgr(color), thickness);
        }

        private void DrawRectangle(Image<Gray, byte> image, Rectangle rectangle, Gray gray, int thickness)
        {
            image.Draw(rectangle, gray, thickness);
        }

        #endregion


        #region Get/Set

        public Image<Bgr, byte> Processed
        {
            get { return processed; }
            set { processed = value; }
        }

        public Image<Gray, byte> Gray
        {
            get { return gray; }
            set { gray = value; }
        }

        public Image<Gray, byte> PupilImage
        {
            get { return pupilImage; }
            set { pupilImage = value; }
        }

        public Image<Gray, byte> GlintImage
        {
            get { return glintImage; }
            set { glintImage = value; }
        }

        public GTPoint PupilCenter
        {
            get { return pupilCenter; }
            set { pupilCenter = value; }
        }

        public GTPoint GlintCenter
        {
            get { return glintCenter; }
            set { glintCenter = value; }
        }

        public GlintConfiguration GlintsDetected
        {
            get { return glintsDetected; }
            set { glintsDetected = value; }
        }

        public bool FoundPupil
        {
            get { return foundPupil; }
            set { foundPupil = value; }
        }

        public bool FoundGlints
        {
            get { return foundGlints; }
            set { foundGlints = value; }
        }

        public Rectangle EyeROIAvg
        {
            get { return eyeROIAvg; }
            set { eyeROIAvg = value; }
        }

        public List<Rectangle> EyeROIs
        {
            get { return eyeROIs; }
            set { eyeROIs = value; }
        }

        public System.Drawing.Color PupilThresholdColor
        {
            get { return pupilThresholdColor; }
            set { pupilThresholdColor = value; }
        }

        public System.Drawing.Color GlintThresholdColor
        {
            get { return GlintThresholdColor; }
            set { GlintThresholdColor = value; }
        }

        #endregion
 
    }
}
