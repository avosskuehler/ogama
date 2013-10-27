// <copyright file="Tracker.cs" company="ITU">
// ******************************************************
// GazeTrackingLibrary for ITU GazeTracker
// Copyright (C) 2010 Javier San Agustin  
// ------------------------------------------------------------------------
// This software is distributed under a dual-licence structure.
//
// For profit usage:
// If you use the software in a way that generates any form of compensation or profit 
// you must obtain a licence to do so. This will allow you to use the codebase without 
// sharing your own source code. Any redistribution the software or parts of it 
// requires a mutual agreement. Contact Javier San Agustin (javier@itu.dk)
// or Martin Tall (m@martintall.com) for more information. 
//
// For educational and non-profit usage:
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
//
// **************************************************************
// </copyright>
// <author>Javier San Agustin</author>
// <email>javier@itu.dk</email>
// <modifiedby>Martin Tall</modifiedby>
// <modifiedby>Adrian Voßkühler</modifiedby>
// <modifiedby>Alastair Jeremy</modifiedby>

using System;
using System.ComponentModel;
using System.Threading;
using System.Windows;
using System.Windows.Threading;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;

using GTCommons.Enum;
using GTSettings;

using GTCommons;

using log4net;
using GTHardware.Cameras.DirectShow;

namespace GTLibrary
{
  using GTLibrary.Calibration;
  using GTLibrary.Detection;
  using GTLibrary.EyeMovement;
  using GTLibrary.EyeMovementDetection;
  using GTLibrary.Log;
  using GTLibrary.Logging;
  using GTLibrary.Network;
  using GTLibrary.Utils;

  public class Tracker
    {
        #region Variables & events

        /// <summary>
        /// Logger for log4net logging
        /// </summary>
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        /// <summary>
        /// Indicator that can be used for high speed DEBUG level logging. Note that using this flag will prevent automated reload
        /// of log4net configuration for that log statement during program operation
        /// </summary>
        private static readonly bool isDebugEnabled = log.IsDebugEnabled;

        private static Tracker instance;
        private readonly DetectionManager detectionManager;
        private readonly ExponentialSmoother exponentialSmoother;
        private readonly GTGazeData gazeDataRaw;
        private readonly GTGazeData gazeDataSmoothed;
        private readonly GTExtendedData gazeDazaExtended;

        private readonly Logger logGaze;
        private readonly UDPServer server;

        /// <summary>
        /// This timerCalibration is used to wait 300 milliseconds before starting 
        /// grabbing images when a new calibration target starts
        /// // Changed to 100ms. are we doing this because of overshooting? /martin
        /// </summary>
        private readonly DispatcherTimer timerCalibrationDelay;

        private readonly Visualization visualization;
        private Calibration.Calibration calibration;
        private int calibrationDelayMilliseconds = 100;
        private GTCommands commands;

        private Classifier eyeMovement;
        private double fpsTracking;
        private int imagesReceivedSinceCounterStart;
        private long imgCounter;
        private bool isCalibrating;
        private bool processingDone;
        //private bool recalibrate;
        private Recalibration recalibration;

        private DateTime timerStartTime;
        private TrackData trackData;

        #endregion //FIELDS

        #region Events

        /// <summary>
        /// This event is raised, whenever the tracker has received
        /// and processed a new video capture frame.
        /// </summary>
        public event EventHandler OnProcessedFrame;

        /// <summary>
        /// This event is raised, whenever the tracker has calculated the calibration
        /// </summary>
        public event EventHandler OnCalibrationComplete;

        #endregion

        #region Constructor

        private Tracker(GTCommands commands)
        {
            log.Info("Constructing Tracker...");
            this.commands = commands;
            detectionManager = new DetectionManager();
            calibration = new Calibration.Calibration();
            recalibration = new Recalibration();
            eyeMovement = new Classifier();
            exponentialSmoother = new ExponentialSmoother(Settings.Instance.EyeMovement.SmoothNumberOfSamples, 0, (int)Math.Ceiling(Settings.Instance.EyeMovement.SmoothLevel / 5.0));
            visualization = new Visualization();
            server = new UDPServer();
            gazeDataRaw = new GTGazeData();
            gazeDataSmoothed = new GTGazeData();
            gazeDazaExtended = new GTExtendedData();
            processingDone = true;
            timerCalibrationDelay = new DispatcherTimer();
            timerCalibrationDelay.Interval = TimeSpan.FromMilliseconds(calibrationDelayMilliseconds);
            timerCalibrationDelay.Tick += TimerCalibrationTick;
            logGaze = new Logger();
            logGaze.Server = server; // Used to send messages back to client (log start/stop etc.)
            log.Info( "Looking up LoggingEnabled in FileSettings" );
            logGaze.IsEnabled = Settings.Instance.FileSettings.LoggingEnabled;

            log.Info( "Setting RecalibrationAvailableHandler" );
            recalibration.RecalibrationAvailable += new Recalibration.RecalibrationAvailableHandler( recalibration_RecalibrationAvailable );
            //recalibration.OnRecalibrationAvailable +=new Recalibration.RecalibrationAvailable(recalibration_OnRecalibrationAvailable);
            Settings.Instance.Processing.PropertyChanged += ProcessingSettingsPropertyChanged;
            timerCalibrationDelay.Tick += TimerCalibrationTick;

            if ( GTHardware.Camera.Instance.Device != null )
            {
                log.Info( "Setting GTHardware device OnImage EventHandler" );
                GTHardware.Camera.Instance.Device.OnImage += new EventHandler<GTHardware.Cameras.ImageEventArgs>( Device_OnImage );
            }

            log.Info("Completed constructing Tracker");
        }

        #endregion //CONSTRUCTION

        #region Get/set properties & methods

        public static Tracker Instance
        {
            get { return instance ?? (instance = new Tracker(GTCommands.Instance)); }
        }

        public GTCommands Commands
        {
            set { commands = value; }
        }

        public int VideoWidth
        {
            get { return GTHardware.Camera.Instance.Width; }
        }

        public int VideoHeight
        {
            get { return GTHardware.Camera.Instance.Height; }
        }

        public int FPSVideo
        {
            get { return GTHardware.Camera.Instance.FPS; }
        }

        public double FPSTracking
        {
            get { return fpsTracking; }
        }

        public DetectionManager ImageProcessing
        {
            get { return detectionManager; }
        }

        public UDPServer Server
        {
            get { return server; }
        }

        public Logger LogData
        {
            get { return logGaze; }
        }

        public Calibration.Calibration Calibration
        {
            get { return calibration; }
        }

        public bool IsCalibrating
        {
            get { return isCalibrating; }
        }

        public GTGazeData GazeDataRaw
        {
            get { return gazeDataRaw; }
        }

        public GTGazeData GazeDataSmoothed
        {
            get { return gazeDataSmoothed; }
        }

        public GTExtendedData GTExtendedData
        {
            get { return gazeDazaExtended; }
        }

        protected long ImgCounter
        {
            get { return imgCounter; }
            set { imgCounter = value; }
        }

        protected bool ProcessingDone
        {
            get { return processingDone; }
        }

        #region Get images

        public Image<Gray, byte> GetGrayImage()
        {
            return visualization.VisualizeOnDemandGray();
        }

        public IImage GetGrayImage(int width, int height)
        {
            return visualization.VisualizeOnDemandGray().Resize(width, height, INTER.CV_INTER_LINEAR);
        }

        public IImage GetProcessedImage()
        {
            return visualization.VisualizeOnDemandProcessed();
        }

        public IImage GetProcessedImage(int width, int height)
        {
            return visualization.VisualizeOnDemandProcessed().Resize(width, height, INTER.CV_INTER_LINEAR);
        }

        #endregion

        #endregion //PROPERTIES

        #region Public methods

        public void SetCamera(int deviceNumber, int deviceMode)
        {
            GTHardware.Camera.Instance.SetDirectShowCamera(deviceNumber, deviceMode);
            GTHardware.Camera.Instance.Device.OnImage += new EventHandler<GTHardware.Cameras.ImageEventArgs>(Device_OnImage);
            GTHardware.Camera.Instance.Device.Start();

            // Bing camera settings to device (brightness etc.). 
            // Sliders are bound to CameraSettings, here we bind them to the camera interface  
            if (GTHardware.Camera.Instance.Device != null && GTHardware.Camera.Instance.DeviceType == GTHardware.Camera.DeviceTypeEnum.DirectShow)
            {
                DirectShowCamera dShowCam = (DirectShowCamera)GTHardware.Camera.Instance.Device;
                Settings.Instance.Camera.OnVideoProcAmpPropertyChanged += dShowCam.OnVideoProcAmpPropertyChanged;
                Settings.Instance.Camera.OnCameraControlPropertyChanged += dShowCam.OnCameraControlPropertyChanged;
            }
        }

        #region Calibration

        public void CalibrationStart()
        {
            //if (Settings.Instance.Processing.TrackingGlints)
            //{
            //    if (Settings.Instance.Processing.NumberOfGlints == 4)
            //        calibration = new CalibHomography();
            //    else
            //        calibration = new CalibPolynomial();
            //}
            //else
            //{
            //    calibration = new CalibPupil();
            //}

            calibration = new Calibration.Calibration();

            //server.SendMessage(GTCommands.Instance.Calibration, this.calibration.numTargets.ToString());
        }

        public void CalibrationEnd()
        {
            try
            {
                bool success = calibration.Calibrate();
                trackData.CalibrationDataLeft = calibration.CalibMethod.CalibrationDataLeft;
                trackData.CalibrationDataRight = calibration.CalibMethod.CalibrationDataRight;
                server.SendMessage(GTNetworkClient.Commands.CalibrationEnd, 5); // todo: should be quality
            }
            catch (Exception ex)
            {
                ErrorLogger.ProcessException(ex, false);
            }

            //this.calibration.ExportToFile();

            // Raise event to UI
            if (OnCalibrationComplete != null)
            {
                OnCalibrationComplete(this, new EventArgs());
            }
        }

        public void CalibrationAccepted()
        {
            // Save calibration data to /bin/Calibration/ID/
            calibration.ExportToFile();
        }

        public void CalibrationAbort()
        {
            timerCalibrationDelay.Stop();
            isCalibrating = false;
            server.SendMessage(GTNetworkClient.Commands.CalibrationAbort);
        }

        public void CalibrationPointStart(int id, Point coords)
        {
            var targetCoordinates = new System.Drawing.Point((int)coords.X, (int)coords.Y);

            calibration.CalibMethod.AddTarget(id, targetCoordinates);
            calibration.CalibMethod.InstanceTargetNumber = id;

            // We wait a little before starting grabbing images
            timerCalibrationDelay.Start();
            server.SendMessage(GTNetworkClient.Commands.CalibrationPointChange, "x:" + coords.X + " y:" + coords.Y + " ");
        }

        public void CalibrationPointEnd()
        {
            timerCalibrationDelay.Stop();
            isCalibrating = false;
        }

        #region Recalibration methods

        //public void SaveRecalibInfo(long rawTimeOnClient, int packagenumber, System.Drawing.Point targetCoords,
        //                            GTPoint gazeCoords)
        //{
        //    if (!calibration.calibration.IsCalibrated)
        //        return;

        //    int package = packagenumber; //the package number 

        //    switch (Settings.Instance.Calibration.RecalibrationType)
        //    {
        //        case RecalibrationTypeEnum.Offset:
        //            recalibration.calibration = Calibration;
        //            recalibration.RecalibrateOffset(gazeCoords, targetCoords);
        //            break;

        //        case RecalibrationTypeEnum.None:
        //            break;

        //        default:
        //            if (!recalibration.recalibrating)
        //            {
        //                recalibration.StartRecalibration(calibration);
        //            }

        //            int InstanceTarget = recalibration.NumRecalibTargets;
        //            recalibration.calibration.CalibrationTargets.Add(new CalibrationTarget(InstanceTarget, targetCoords));

        //            recalibration.gazeCoordinates.Add(gazeCoords);

        //            recalibration.calibration.CalibrationTargets[InstanceTarget].pupilCentersLeft.Add(trackData.PupilDataLeft.Center);
        //            recalibration.calibration.CalibrationTargets[InstanceTarget].pupilCentersRight.Add(trackData.PupilDataRight.Center);

        //            if (Settings.Instance.Processing.TrackingGlints)
        //            {
        //                recalibration.calibration.CalibrationTargets[InstanceTarget].glintsLeft.Add(trackData.GlintDataLeft.Glints);
        //                recalibration.calibration.CalibrationTargets[InstanceTarget].glintsRight.Add(trackData.GlintDataRight.Glints);
        //            }

        //            recalibration.NumRecalibTargets++;
        //            break;
        //    }
        //}

        #endregion

        #endregion // Calibration

        #region Autotune

        public void AutoTune()
        {
        }

        #endregion

        #endregion //Public methods

        #region Overrides

        public virtual void Cleanup()
        {
            if (server.IsEnabled)
                server.IsEnabled = false;

            if (logGaze.IsEnabled)
                logGaze.IsEnabled = false;

            if (GTHardware.Camera.Instance.Device != null)
            {
                GTHardware.Camera.Instance.Device.Cleanup();
            }
        }

        #endregion //OVERRIDES

        #region Eventhandlers

        /// <summary>
        /// Resets the wait before calibration begins timer and sets the calibrating flag
        /// to now pop up the images down to the calibration routines.
        /// </summary>
        /// <param name="sender">Source of the event</param>
        /// <param name="e">An empty <see cref="EventArgs"/></param>
        private void TimerCalibrationTick(object sender, EventArgs e)
        {
            timerCalibrationDelay.Stop();
            isCalibrating = true;
        }

        private void ProcessingSettingsPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "TrackingMethod")
            {
                if (GTHardware.Camera.Instance.Device != null)
                    GTHardware.Camera.Instance.Device.ClearROI();

                detectionManager.Clear();
            }
        }

        #endregion //EVENTHANDLER

        #region Private methods

        #region FrameCaptureCompleted & Processing completed

        /// <summary>
        /// The main event handler that is called whenever the camera capture class
        /// has an new eye video image available.
        /// Starts to process the frame trying to find pupil and glints.
        /// </summary>
        /// <param name="newVideoFrame">The <see cref="Image{TColor,TDepth}.CV.Structure.Bgr, byte}"/>
        /// image with the new frame.</param>

        private void Device_OnImage(object sender, GTHardware.Cameras.ImageEventArgs e)
        {
            imgCounter++;
            processingDone = false;
            //bool processingOk;

            Performance.Now.IsEnabled = false;
            Performance.Now.Start(); // Stop output by setting IsEnabled = false or Stop()

            // TrackData object stores all information on pupil centers, glints etc.
            trackData = new TrackData();
            trackData.TimeStamp = DateTime.UtcNow.Ticks / TimeSpan.TicksPerMillisecond;
            trackData.FrameNumber = imgCounter;

            // Keep reference to image in local variable
            Image<Gray, byte> gray = e.Image;

            // Flip image here, directshow flipping is not supported by every device
            if (Settings.Instance.Camera.FlipImage)
                gray = gray.Flip(FLIP.VERTICAL);

            // Tracking disabled, if visible just set gray image in visulization and return
            if (Settings.Instance.Visualization.VideoMode == VideoModeEnum.RawNoTracking)
            {
                Performance.Now.Stop();

                if (Settings.Instance.Visualization.IsDrawing)
                {
                    visualization.Gray = gray;
                    visualization.TrackData = trackData; // not sure if there is anything to visualize here..
                    CalculateFPS();
                    RaiseFrameProcessingCompletedEvent(true);
                }
                return;
            }

            try
            {
                // Process image, find features, main entry point to processing chain
                trackData.ProcessingOk = detectionManager.ProcessImage(gray, trackData);

                if (trackData.ProcessingOk)
                {
                    if (calibration.CalibMethod.IsCalibrated)
                    {
                        CalculateGazeCoordinates(trackData);

                        if (Settings.Instance.FileSettings.LoggingEnabled)
                            logGaze.LogData(trackData);
                    }
                    else
                    {
                        if (isCalibrating)
                            SaveCalibInfo(trackData);

                        // Really log uncalibrated data? For pupil size?
                        //if (Settings.Instance.FileSettings.LoggingEnabled)
                        //    logGaze.LogData(trackData);
                    }
                }
                else
                {
                    if (Settings.Instance.FileSettings.LoggingEnabled)
                        logGaze.LogData(trackData);
                }
            }
            catch (Exception)
            {
                trackData.ProcessingOk = false;
            }

            // Sends values via the UDP server directly
            if (server.IsStreamingGazeData)
            {
                //if (server.SendSmoothedData)
                //    server.SendGazeData(gazeDataSmoothed.GazePositionX, gazeDataSmoothed.GazePositionY,
                //        trackData.PupilDataLeft.Diameter);
                //else
                // Send avg. value
                server.SendGazeData(gazeDataRaw.GazePositionX, gazeDataRaw.GazePositionY, trackData.PupilDataLeft.Diameter);

                //server.SendTrackData(trackData);
            }

            Autotune.Instance.Tune();

            // Set data for visualization
            if (Settings.Instance.Visualization.IsDrawing && isCalibrating == false)
            {
                // Drawn on-demand by calling GetProcessed or GetOriginalImage 
                visualization.Gray = gray.Copy();
                visualization.TrackData = trackData;
            }

            // Recenter camera ROI
            detectionManager.CameraCenterROI(trackData, gray.Size);

            // Store camera roi position
            trackData.CameraROI = GTHardware.Camera.Instance.ROI;

            // Add sample to database
            TrackDB.Instance.AddSample(trackData.Copy());

            // Calculate the frames per second we're tracking at
            CalculateFPS();

            // Stop performance timer
            Performance.Now.Stop();

            // Raise FrameCaptureComplete event (UI listens for updating video stream)
            RaiseFrameProcessingCompletedEvent(trackData.ProcessingOk);
        }

        private void RaiseFrameProcessingCompletedEvent(bool processingOk)
        {
            try
            {
                if (OnProcessedFrame != null)
                    OnProcessedFrame(this, EventArgs.Empty);
            }
            catch (ThreadInterruptedException e)
            {
                string message = "An error occured in Tracker.cs (ThreadInterruptedException). Message: " + e.Message;
                ErrorLogger.RaiseGazeTrackerMessage(message);
            }
            catch (Exception we)
            {
                ErrorLogger.ProcessException(we, false);
            }

            processingDone = true;
        }

        #endregion

        #region Calibration and gaze coordinate calculateion

        private void CalculateGazeCoordinates(TrackData td)
        {
            GTPoint gazedCoordinatesLeft;
            GTPoint gazedCoordinatesRight = new GTPoint();
            GTPoint smoothedCoordinates;

            #region Monocular/Left eye

            calibration.PupilCenterLeft = trackData.PupilDataLeft.Center;

            if (Settings.Instance.Processing.TrackingGlints)
                calibration.GlintConfigLeft = td.GlintDataLeft.Glints;

            gazedCoordinatesLeft = calibration.GetGazeCoordinates(td, EyeEnum.Left);

            #endregion

            #region Binocular/Right eye

            if (Settings.Instance.Processing.TrackingMode == TrackingModeEnum.Binocular)
            {
                calibration.PupilCenterRight = td.PupilDataRight.Center;

                if (Settings.Instance.Processing.TrackingGlints)
                    calibration.GlintConfigRight = td.GlintDataRight.Glints;

                gazedCoordinatesRight = calibration.GetGazeCoordinates(td, EyeEnum.Right);
            }

            #endregion

            #region Smoothing/Eye movement state

            if (Settings.Instance.Processing.EyeMouseSmooth)
            {
                var p = new GTPoint(gazedCoordinatesLeft.X, gazedCoordinatesLeft.Y);

                if (Settings.Instance.Processing.TrackingMode == TrackingModeEnum.Binocular)
                {
                    if (gazedCoordinatesRight.Y != 0 && gazedCoordinatesRight.X != 0)
                    {
                        p.X += gazedCoordinatesRight.X;
                        p.Y += gazedCoordinatesRight.Y;
                        p.X = p.X / 2;
                        p.Y = p.Y / 2;
                    }
                }

                this.eyeMovement.CalculateEyeMovement(p);

                smoothedCoordinates = exponentialSmoother.Smooth(p);


                //if (this.eyeMovement.EyeMovementState == Classifier.EyeMovementStateEnum.Fixation)
                //    smoothedCoordinates = exponentialSmoother.Smooth(p);
                //else
                //{
                //    smoothedCoordinates = p;
                //    this.exponentialSmoother.Stop();
                //}
                trackData.EyeMovement = this.eyeMovement.EyeMovementState;
                gazeDataSmoothed.Set(smoothedCoordinates.X, smoothedCoordinates.Y, smoothedCoordinates.X, smoothedCoordinates.Y);
            }

            #endregion

            #region Set values, raise events

            // trigger OnGazeData events
            this.gazeDataRaw.Set(
              gazedCoordinatesLeft.X,
              gazedCoordinatesLeft.Y,
              gazedCoordinatesRight.X,
              gazedCoordinatesRight.Y);

            this.trackData.GazeDataRaw = this.gazeDataRaw;
            this.trackData.GazeDataSmoothed = this.gazeDataSmoothed;

            // Trigger OnExtendedGazeData events
            this.gazeDazaExtended.Set(
                this.trackData.TimeStamp,
                this.gazeDataRaw.GazePositionX,
                this.gazeDataRaw.GazePositionY,
                this.trackData.PupilDataLeft.Diameter,
                this.trackData.PupilDataRight.Diameter);

            #endregion
        }

        private void SaveCalibInfo(TrackData td)
        {
            CalibrationTarget ct = calibration.CalibMethod.GetTarget(calibration.InstanceTargetNumber);

            ct.pupilCentersLeft.Add(td.PupilDataLeft.Center);
            ct.pupilCentersRight.Add(td.PupilDataRight.Center);

            if (Settings.Instance.Processing.TrackingGlints)
            {
                ct.glintsLeft.Add(td.GlintDataLeft.Glints);
                ct.glintsRight.Add(td.GlintDataRight.Glints);
            }

            // Important: Only print if really needed for debugging, you'll only receive 1/10 of the samples..

            //foreach (CalibrationTarget ctOutput in this.calibration.CalibrationTargets) 
            //{
            //    foreach (GTPoint pLeft in ctOutput.pupilCentersLeft)
            //        Console.Out.WriteLine("Target " + calibration.InstanceTargetNumber + " PupilCenterLeft:" + pLeft.X + " " + pLeft.Y);
            //    foreach (GTPoint pRight in ctOutput.pupilCentersRight)
            //        Console.Out.WriteLine("Target " + calibration.InstanceTargetNumber + " PupilCenterRight:" + pRight.X + " " + pRight.Y);
            //}

            Performance.Now.Stamp("SaveCalibInfo");
        }

        private void recalibration_RecalibrationAvailable()
        {
            calibration.CalibMethod.CalibrationDataLeft.CoeffsX = new Matrix<double>(recalibration.calibration.CalibMethod.CalibrationDataLeft.CoeffsX.Data);
            calibration.CalibMethod.CalibrationDataLeft.CoeffsY = new Matrix<double>(recalibration.calibration.CalibMethod.CalibrationDataLeft.CoeffsY.Data);
        }

        #endregion

        private void CalculateFPS()
        {
            if (Settings.Instance.Visualization.VideoMode == VideoModeEnum.RawNoTracking)
            {
                fpsTracking = 0;
                return;
            }

            TimeSpan ts = DateTime.Now.Subtract(timerStartTime);

            if (ts.TotalMilliseconds < 1000)
            {
                imagesReceivedSinceCounterStart++;
            }
            else
            {
                fpsTracking = imagesReceivedSinceCounterStart;
                timerStartTime = DateTime.Now;
                imagesReceivedSinceCounterStart = 0;
            }
        }

        #endregion
    }
}