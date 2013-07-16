using System;
using System.Drawing;
using System.Windows;
using System.Windows.Controls;

namespace GTApplication.TrackerViewer
{
    public partial class TrackBoxUC : UserControl
    {
        private int videoHeight = 1024;
        private int videoWidth = 1280; // default, will use tracker.VideoWidth on call

        public TrackBoxUC()
        {
            InitializeComponent();
        }

        public void UpdateROI(Rectangle roi)
        {
            if (Visibility == Visibility.Collapsed)
                return;

            if (roi.X == 0 || roi.Y == 0)
                rectangleROI.Visibility = Visibility.Collapsed;
            else
                rectangleROI.Visibility = Visibility.Visible;

            roi = ScaleROI(roi, videoWidth, videoHeight);
            rectangleROI.Width = roi.Width;
            rectangleROI.Height = roi.Height;
            Canvas.SetTop(rectangleROI, roi.Y);
            Canvas.SetLeft(rectangleROI, roi.X);
        }

        private Rectangle ScaleROI(Rectangle roi, int videoWidth, int videoHeight)
        {
            double diffX = canvasCamera.ActualWidth/videoWidth;
            double diffY = canvasCamera.ActualHeight/videoHeight;

            roi.Width = Convert.ToInt32(roi.Width*diffX);
            roi.Height = Convert.ToInt32(roi.Height*diffY);
            roi.X = Convert.ToInt32(roi.X*diffX);
            roi.Y = Convert.ToInt32(roi.Y*diffY);

            return roi;
        }

        #region Get/Set

        public int VideoWidth
        {
            get { return videoWidth; }
            set { videoWidth = value; }
        }

        public int VideoHeight
        {
            get { return videoHeight; }
            set { videoHeight = value; }
        }

        #endregion
    }
}