// <copyright file="VideoImageControl.xaml.cs" company="ITU">
// ******************************************************
// GazeTrackingLibrary for ITU GazeTracker
// Copyright (C) 2010 Martin Tall  
// ------------------------------------------------------------------------
// This program is free software; you can redistribute it and/or modify it 
// under the terms of the GNU General Public License as published by the 
// Free Software Foundation; either version 3 of the License, 
// or (at your option) any later version.
// This program is distributed in the hope that it will be useful, 
// but WITHOUT ANY WARRANTY; without even the implied warranty of 
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU 
// General Public License for more details.
// You should have received a copy of the GNU General Public License 
// along with this program; if not, see http://www.gnu.org/licenses/.
// **************************************************************
// </copyright>
// <author>Martin Tall</author>
// <email>info@martintall.com</email>

using System;
using System.Windows;
using System.Windows.Forms;
using Emgu.CV.UI;
using GTLibrary;
using GTLibrary.Logging;
using GTCommons.Enum;
using GTSettings;

namespace GTApplication.TrackerViewer
{
    public partial class VideoImageControl
    {
        #region CONSTANTS

        private const int DefaultVideoImageWidth = 380;
        private const int DefaultVideoImageHeight = 200;

        #endregion //CONSTANTS

        #region Variables

        private VideoImageOverlay overlay;

        #endregion //FIELDS

        #region Constructor

        public VideoImageControl()
        {
            InitializeComponent();

            VideoImageHeight = DefaultVideoImageHeight;
            VideoImageWidth = DefaultVideoImageWidth;

            pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;

            this.LayoutUpdated += VideoImageControl_LayoutUpdated;
            this.pictureBox.MouseEnter += pictureBox_MouseEnter;
            this.pictureBox.MouseLeave += pictureBox_MouseLeave;
        }

        void pictureBox_MouseLeave(object sender, EventArgs e) 
        {
            if(overlay != null)
               overlay.GridPerformanceCounters.Visibility = Visibility.Collapsed;
        }

        void pictureBox_MouseEnter(object sender, EventArgs e) 
        {
            if(overlay != null)
               overlay.GridPerformanceCounters.Visibility = Visibility.Visible;
        }



        void VideoImageControl_LayoutUpdated(object sender, EventArgs e) 
        {
            if(overlay != null && this.IsVisible)
            {
                try
                {
                    overlay.Width = this.Width;
                    overlay.Height = this.Height;
                    overlay.Top = this.PointToScreen(new Point(0, 0)).Y;
                    overlay.Left = this.PointToScreen(new Point(0, 0)).X;
                }
                catch (Exception ex) 
                {
                    Console.Out.WriteLine("VideoImageControl: " + ex.Message);
                }
            }
        }

        #endregion //CONSTRUCTION

        #region Properties

        /// <summary>
        /// Gets the CV image box.
        /// </summary>
        /// <value>The CV image box.</value>
        public ImageBox CVImageBox
        {
            get { return pictureBox; }
        }

        /// <summary>
        /// Sets the width of the video image.
        /// </summary>
        /// <value>The width of the video image.</value>
        public int VideoImageWidth
        {
            set { pictureBox.Width = value; }
            get { return pictureBox.Width; }
        }

        /// <summary>
        /// Sets the height of the video image.
        /// </summary>
        /// <value>The height of the video image.</value>
        public int VideoImageHeight
        {
            set { pictureBox.Height = value; }
            get { return pictureBox.Height; }
        }

        public VideoImageOverlay Overlay
        {
            get { return this.overlay; }
        }

        public bool VideoOverlayTopMost
        {
            set
            {
                if(overlay != null)
                    overlay.Topmost = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is native resolution.
        /// </summary>
        /// <value>
        /// <c>true</c> if running at native resolution; otherwise, <c>false</c>.
        /// </value>
        //public bool IsNativeResolution
        //{
        //    get { return this.isNativeResolution; }
        //    set { this.isNativeResolution = value; }
        //}

        #endregion //PROPERTIES

        #region Public methods

        public void Start()
        {
            Tracker.Instance.OnProcessedFrame += Tracker_FrameCaptureComplete;

            Settings.Instance.Visualization.IsDrawing = true;

            if(overlay == null)
            {
                overlay = new VideoImageOverlay();
                overlay.Width = this.VideoImageWidth;
                overlay.Height = this.VideoImageHeight;
                overlay.Top = PointToScreen(new Point(0, 0)).Y;
                overlay.Left = PointToScreen(new Point(0, 0)).X;
                overlay.Show();
                overlay.Topmost = true;
            }
        }


        public void Stop(bool stopAtServer)
        {
            // Unregister event listner for processed frame
            Tracker.Instance.OnProcessedFrame -= Tracker_FrameCaptureComplete;

            // Turn of visualization (copying of newframe to visualization class, saves cpu and memory)
            if(stopAtServer)
              Settings.Instance.Visualization.IsDrawing = false;

            this.Dispatcher.BeginInvoke(new MethodInvoker(delegate { if (overlay == null) return; overlay.Topmost = false; }));
        }


        #endregion

        #region Eventhandler

        int drawImageOnCounter = 1;
        int imageCounter = 0;
        int fps = 0;

        private void Tracker_FrameCaptureComplete(object sender, EventArgs args)
        {
            // Don't draw while calibrating to obtain maximum images
            if (Tracker.Instance.IsCalibrating)
                return;

            #region Skipping to conserve CPU on high framerates

            if (GTHardware.Camera.Instance.DeviceType != GTHardware.Camera.DeviceTypeEnum.DirectShow)
            {
                imageCounter++;

                // If fps changed by more than +-10, determine new skipping
                if (fps < GTHardware.Camera.Instance.Device.FPS - 10 || fps > GTHardware.Camera.Instance.Device.FPS + 10)
                {
                    fps = GTHardware.Camera.Instance.Device.FPS;

                    if (fps < 30)
                        drawImageOnCounter = 1;
                    else
                        drawImageOnCounter = Convert.ToInt32(fps/24); // Target visualization @ 24 fps
                }

                if (imageCounter < drawImageOnCounter)
                    return;

                imageCounter = 0;
            }

            #endregion

            try
            {
                if (pictureBox.InvokeRequired)
                    pictureBox.BeginInvoke(new MethodInvoker(UpdateImage));
                else
                   UpdateImage();
            }
            catch (Exception ex)
            {
                ErrorLogger.ProcessException(ex, false);
            }
        }
        #endregion

        #region Private methods

        private void UpdateImage() 
        {
            pictureBox.Image = null;

             if (Settings.Instance.Visualization.VideoMode == VideoModeEnum.Processed)
                 pictureBox.Image = Tracker.Instance.GetProcessedImage();
             else
                 pictureBox.Image = Tracker.Instance.GetGrayImage();

             if (overlay != null)
                 overlay.performanceCountersUC.Update(Tracker.Instance.FPSVideo, Tracker.Instance.FPSTracking);
        }

        #endregion


    }
}