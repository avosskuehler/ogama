using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

using GTLibrary.Calibration;
using GTLibrary.Logging;
using GTLibrary.Utils;

using GTCommons.Enum;
using GTSettings;

namespace GTApplication.CalibrationUI
{
  using GTApplication.Tools;

  #region Includes



    #endregion

    public partial class CalibrationControl : UserControl
    {
        #region Variables

        private double _acceleration = 0.4;
        private double _avgSumStdDev;
        private SolidColorBrush _colorBackground;
        private SolidColorBrush _colorPoints;
        private double _deacceleration = 0.6;
        private double _pointDuration = 1000;
        private double _pointTransitionDuration = 1000;
        private bool _randomOrder = true;
        private bool calibrationCompleted;
        private List<CalibrationPoint> calibrationPoints = new List<CalibrationPoint>();
        private CalibrationPoint currentPoint;
        private CalibrationPoint fakePoint;
        private int indexInstancePoint;
        private bool isRecalibratingPoint;
        private bool isRunning;
        private CalibrationPoint nextPoint;
        private int numberOfPoints = 9;

        private double pointDiameter = 45;

        #endregion

        #region Events

        public static readonly RoutedEvent CalibrationStartEvent = EventManager.RegisterRoutedEvent("CalibrationStartEvent", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(CalibrationControl));
        public static readonly RoutedEvent CalibrationEndEvent = EventManager.RegisterRoutedEvent("CalibrationEndEvent", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(CalibrationControl));
        public static readonly RoutedEvent CalibrationAbortEvent = EventManager.RegisterRoutedEvent("CalibrationAbortEvent", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(CalibrationControl));
        public static readonly RoutedEvent PointStartEvent = EventManager.RegisterRoutedEvent("PointStartEvent", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(CalibrationControl));
        public static readonly RoutedEvent PointStopEvent = EventManager.RegisterRoutedEvent("PointStopEvent", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(CalibrationControl));

        #endregion

        #region Constructor

        public CalibrationControl()
        {
            InitializeComponent();
            //CanvasCalibration.OnUIElementMoved += CanvasCalibration_OnUIElementMoved;
        }

        #endregion

        #region Set/Get

        public new double Width
        {
            set { this.CanvasCalibration.Width = value; }
            get { return this.CanvasCalibration.Width; }
        }

        public new double Height
        {
            set { this.CanvasCalibration.Height = value; }
            get { return this.CanvasCalibration.Height; }
        }

        public int NumberOfPoints
        {
            get { return numberOfPoints; }
            set { numberOfPoints = value; }
        }

        public List<CalibrationPoint> CalibrationPoints
        {
            get { return calibrationPoints; }
            set { calibrationPoints = value; }
        }

        public bool RandomOrder
        {
            get { return _randomOrder; }
            set { _randomOrder = value; }
        }

        public CalibrationPoint CurrentPoint
        {
            get { return currentPoint; }
        }

        public bool IsRecalibratingPoint
        {
            get { return isRecalibratingPoint; }
            set { isRecalibratingPoint = value; }
        }

        public SolidColorBrush ColorPoints
        {
            get { return _colorPoints; }
            set { _colorPoints = value; }
        }


        public double PointDiameter
        {
            get { return pointDiameter; }
            set { pointDiameter = value; }
        }

        public SolidColorBrush ColorBackground
        {
            get { return _colorBackground; }
            set
            {
                _colorBackground = value;
                CanvasCalibration.Background = _colorBackground;
            }
        }

        public double PointDuration
        {
            get { return _pointDuration; }
            set { _pointDuration = value; }
        }

        public double PointTransitionDuration
        {
            get { return _pointTransitionDuration; }
            set { _pointTransitionDuration = value; }
        }

        public double Acceleration
        {
            get { return _acceleration; }
            set { _acceleration = value / 10; }
        }

        public double Deacceleration
        {
            get { return _deacceleration; }
            set { _deacceleration = value / 10; }
        }


        public bool UseInfantGraphics { get; set; }


        public double AvgSumStdDev
        {
            get { return _avgSumStdDev; }
            set { _avgSumStdDev = value; }
        }

        public double AvgDistFromTargets { get; set; }

        #endregion

        #region Public methods

        public void GeneratePositions(double areaWidth, double areaHeight)
        {
            int pointNumber = 1;
            int columnCount = 4; // default 12 points (indx starting at 1-4, eg. 4x3)
            int rowCount = 3; // default 12 points (indx starting at 1-3, eg. 4x3)

			switch (numberOfPoints)
			{
				case 9:
					columnCount = 3;
					rowCount = 3;
					break;
				case 12:
					columnCount = 4;
					rowCount = 3;
					break;
				case 16:
					columnCount = 4;
					rowCount = 4;
					break;
				default:
					columnCount = 3;
					rowCount = 3;
					break;
			}

            double rowSpacer = (areaHeight - pointDiameter) / (rowCount - 1);
            double columnSpacer = (areaWidth - pointDiameter) / (columnCount - 1);

            //if (UseInfantGraphics)
            //    calibrationPointDiameter = calibrationPointDiameter * 2;

            //rowSpacer = rowSpacer - pointDiameter / 2;
            //columnSpacer = columnSpacer - pointDiameter / 2;

            // Adjust for 0-based array loop
            rowCount -= 1;
            columnCount -= 1;

            for (int row = 0; row <= rowCount; row++)
            {
                for (int column = 0; column <= columnCount; column++)
                {
                    var pos = new Point(column * columnSpacer, row * rowSpacer);

                    pos.Y = pos.Y + pointDiameter / 2;
                    // Compensate for top/left positioning, eg. 0,0 is top/left when it should be 25,25 (center of point)
                    pos.X = pos.X + pointDiameter / 2;

                    var calPoint = new CalibrationPoint(
                        pointNumber,
                        pos,
                        pointDiameter,
                        _colorPoints,
                        PointDuration);

                    if (UseInfantGraphics)
                        SetRandomImage(calPoint);

                    calibrationPoints.Add(calPoint);
                    pointNumber++;
                }
            }
        }

        public void Start()
        {
            Reset();

            GeneratePositions(this.Width, this.Height);

            if (RandomOrder)
                RandomizeCalibrationPoints();

            if (currentPoint == null)
                currentPoint = GetNextPoint();

            Cursor = Cursors.None; // Redisplay after calibration

            StartWithFirstFakePoint(new Point(0, 0), currentPoint.Point);
        }

        public void Stop()
        {
            isRunning = false;

            if (calibrationCompleted) return;
            //GTCommands.Instance.Calibration.Abort();

            // Raise calibration abort event
            var args1 = new RoutedEventArgs();
            args1 = new RoutedEventArgs(CalibrationAbortEvent, this);
            RaiseEvent(args1);
        }

        public void Reset()
        {
            calibrationCompleted = false;
            CanvasCalibration.Children.Clear();
            currentPoint = null;
            nextPoint = null;
            indexInstancePoint = -1;
            calibrationPoints = new List<CalibrationPoint>();
            _avgSumStdDev = 0;
        }

        public void ShowAllPoints()
        {
            // Loop through Calpoints in the UserControl (eg. this)
            foreach (CalibrationPoint t in calibrationPoints)
            {
                CalibrationPoint calPoint = CopyPoint(t);
                calPoint.Visibility = Visibility.Visible;

                CanvasCalibration.Children.Add(calPoint);
                Canvas.SetTop(calPoint, calPoint.Point.Y - calPoint.Diameter / 2);
                Canvas.SetLeft(calPoint, calPoint.Point.X - calPoint.Diameter / 2);
                Panel.SetZIndex(calPoint, 1);
            }
        }

        #endregion

        #region FakePoint (display one point to grab attention but no events are sent to the eye tracker)

        private void StartWithFirstFakePoint(Point start, Point end)
        {
            fakePoint = new CalibrationPoint(
                -1,
                TrackingScreen.TrackingScreenCenter,
                pointDiameter,
                ColorPoints,
                1000);

            // Add point to display canvas
            CanvasCalibration.Children.Add(fakePoint);
            Canvas.SetTop(fakePoint, fakePoint.Point.Y - pointDiameter / 2);
            Canvas.SetLeft(fakePoint, fakePoint.Point.X - pointDiameter / 2);

            fakePoint.RunScalePointAnimation();
            fakePoint.OnPointDisplayed += fakePoint_PointDisplayedAction;
        }

        private void fakePoint_PointDisplayedAction(object sender, RoutedEventArgs e)
        {
            // Animate from previous fake point to new real point

            var dbX = new DoubleAnimation();
            dbX.From = fakePoint.Point.X - pointDiameter / 2;
            dbX.To = currentPoint.Point.X - pointDiameter / 2;
            dbX.Duration = TimeSpan.FromMilliseconds(PointTransitionDuration);

            dbX.AccelerationRatio = 0.4;
            dbX.DecelerationRatio = 0.4;

            var dbY = new DoubleAnimation();
            dbY.From = fakePoint.Point.Y - pointDiameter / 2;
            dbY.To = currentPoint.Point.Y - pointDiameter / 2;
            dbY.Duration = TimeSpan.FromMilliseconds(PointTransitionDuration);

            dbY.AccelerationRatio = 0.4;
            dbY.DecelerationRatio = 0.4;

            dbY.Completed += fakePoint_Completed;

            // Start animations
            fakePoint.BeginAnimation(Canvas.LeftProperty, dbX);
            fakePoint.BeginAnimation(Canvas.TopProperty, dbY);
            fakePoint.RunScalePointAnimationReverse(); // At the same time, zoom out again..
        }


        private void fakePoint_Completed(object sender, EventArgs e)
        {
            fakePoint.Visibility = Visibility.Collapsed;

            // Raise calibration start event

            RoutedEventArgs args1 = new RoutedEventArgs();
            args1 = new RoutedEventArgs(CalibrationStartEvent, this);
            RaiseEvent(args1);

            isRunning = true;

            // Starts the main calibration loop..
            DisplayPoints();
        }

        #endregion

        #region Calibration procedure, Animations and Transitions

        private void DisplayPoints()
        {
            if (!isRunning) return;

            // Add point to display canvas
            CanvasCalibration.Children.Add(currentPoint);
            Canvas.SetTop(currentPoint, currentPoint.Point.Y - pointDiameter / 2);
            Canvas.SetLeft(currentPoint, currentPoint.Point.X - pointDiameter / 2);

            // When point shrunk, signal Eye tracker, eg. EyeTrackerPointEnd.
            currentPoint.OnPointDisplayed += PointStop;

            // When point displayed, animate transition to next point (if any)
            currentPoint.OnPointDisplayed += AnimateBetweenPoints;

            // Signal eye tracker, new point starting
            //GTCommands.Instance.Calibration.PointStart(InstancePoint.Number, InstancePoint.Point);
            EyeTrackerPointStart(currentPoint);

            // Starts scale animation (to fixate)
            currentPoint.RunScalePointAnimation();
        }

        private void AnimateBetweenPoints(object sender, RoutedEventArgs e)
        {
            // Get next point (to animate between)
            nextPoint = GetNextPoint();

            if (nextPoint == null)
            {
                // All points displayed, raise event
                calibrationCompleted = true;
                var args1 = new RoutedEventArgs();
                args1 = new RoutedEventArgs(CalibrationEndEvent, this);
                RaiseEvent(args1);
            }
            else
            {
                // Animate from previous to new point
                var calPointX = new DoubleAnimation();
                calPointX.From = currentPoint.Point.X - pointDiameter / 2;
                calPointX.To = nextPoint.Point.X - pointDiameter / 2;
                calPointX.Duration = TimeSpan.FromMilliseconds(PointTransitionDuration);
                calPointX.AccelerationRatio = _acceleration;
                calPointX.DecelerationRatio = _deacceleration;

                var calPointY = new DoubleAnimation();
                calPointY.From = currentPoint.Point.Y - pointDiameter / 2;
                calPointY.To = nextPoint.Point.Y - pointDiameter / 2;
                calPointY.Duration = TimeSpan.FromMilliseconds(PointTransitionDuration);
                calPointY.AccelerationRatio = _acceleration;
                calPointY.DecelerationRatio = _deacceleration;

                calPointX.Completed += AnimateBetweenPoints_Completed;

                // Start animations
                currentPoint.BeginAnimation(Canvas.LeftProperty, calPointX);
                currentPoint.BeginAnimation(Canvas.TopProperty, calPointY);
                currentPoint.RunScalePointAnimationReverse(); // At the same time, zoom out again..
            }
        }

        private void AnimateBetweenPoints_Completed(object sender, EventArgs e)
        {
            // Hide last point, make a switch to the next point
            currentPoint.Visibility = Visibility.Collapsed;

            currentPoint = nextPoint;
            currentPoint.Visibility = Visibility.Visible;

            // Get next point and continue..
            DisplayPoints();
        }

        #endregion


        #region Visualize Calibration Results & recalibrate single point

        public BitmapSource VisualizeCalibrationResults(List<CalibrationTarget> calibTargets)
        {
            CanvasCalibration.Children.Clear();

            // Draw also to Drawing Visual
            DrawingVisual drawingVisual = new DrawingVisual();
            DrawingContext drawingContext = drawingVisual.RenderOpen();

            // Fill with background color
            drawingContext.DrawRectangle(
                this.CanvasCalibration.Background, 
                null, 
                new Rect(0, 0, this.ActualWidth, this.ActualHeight));

            // Redisplay cursor
            Cursor = Cursors.Arrow;

            double sumStdDeviation = 0;
            double sumDistFromTargetsX = 0;
            double sumDistFromTargetsY = 0;

            // Loop through Calpoints in the UserControl
            foreach (CalibrationPoint t in calibrationPoints)
            {
                CalibrationPoint calPoint = CopyPoint(t);

                #region Draw tracker data for calPoint

                foreach (CalibrationTarget trackerPoint in calibTargets)
                {
                    // If target number is the same, create visuals for calibration point
                    if (trackerPoint.targetNumber.Equals(calPoint.Number))
                    {
                        #region Draw raw estimated gaze positions

                        foreach (GTPoint gpLeft in trackerPoint.estimatedGazeCoordinatesLeft)
                        {
                            // compensate for relative to full screen coordinates
                            Point p = AdjustPointToPartial(new Point(gpLeft.X, gpLeft.Y));
                            Ellipse g = new Ellipse { Width = 3, Height = 3, Fill = new SolidColorBrush(Colors.Red) };
                            CanvasCalibration.Children.Add(g);
                            Canvas.SetTop(g, p.Y);
                            Canvas.SetLeft(g, p.X);

                            // Draw also to Drawing Visual
                            drawingContext.DrawEllipse(Brushes.Red, null, p, 3, 3);
                        }

                        foreach (GTPoint gpRight in trackerPoint.estimatedGazeCoordinatesRight)
                        {
                            Point p = AdjustPointToPartial(new Point(gpRight.X, gpRight.Y));
                            Ellipse g = new Ellipse { Width = 3, Height = 3, Fill = new SolidColorBrush(Colors.Blue) };
                            CanvasCalibration.Children.Add(g);
                            Canvas.SetTop(g, p.Y);
                            Canvas.SetLeft(g, p.X);

                            // Draw also to Drawing Visual
                            drawingContext.DrawEllipse(Brushes.Blue, null, p, 3, 3);
                        }

                        #endregion

                        #region Draw Standard deviation indicator

                        CalibrationFeedbackPoint feedback = new CalibrationFeedbackPoint();
                        double stdDevGazeCoordinates = trackerPoint.stdDeviationGazeCoordinatesLeft;

                        // binocular
                        if (trackerPoint.stdDeviationGazeCoordinatesRight != 0)
                        {
                            stdDevGazeCoordinates += trackerPoint.stdDeviationGazeCoordinatesRight;
                            stdDevGazeCoordinates = stdDevGazeCoordinates / 2;
                        }

                        feedback.Width = stdDevGazeCoordinates;
                        feedback.Height = stdDevGazeCoordinates;

                        if (feedback.Width > 80)
                            feedback.EllipseBackground.Fill = new SolidColorBrush(Colors.Red);
                        else
                            feedback.EllipseBackground.Fill = new SolidColorBrush(Colors.Green);

                        CanvasCalibration.Children.Add(feedback);

                        #endregion


                        #region Average mean gaze

                        Point avgMeanGazeCoordinates = new Point(trackerPoint.meanGazeCoordinatesLeft.X, trackerPoint.meanGazeCoordinatesLeft.Y);

                        //// binocular
                        //if (trackerPoint.meanGazeCoordinatesRight.X != 0)
                        //{
                        //    avgMeanGazeCoordinates.Y += trackerPoint.meanGazeCoordinatesRight.Y;
                        //    avgMeanGazeCoordinates.X += trackerPoint.meanGazeCoordinatesRight.X;

                        //    avgMeanGazeCoordinates.X = avgMeanGazeCoordinates.X/2;
                        //    avgMeanGazeCoordinates.Y = avgMeanGazeCoordinates.Y/2;
                        //}

                        // Position avgMean feedback, compensate for relative to full screen coordinates
                        Point adjustedAvgMeanGazePoint = AdjustPointToPartial(avgMeanGazeCoordinates);
                        Canvas.SetTop(feedback, adjustedAvgMeanGazePoint.Y);
                        Canvas.SetLeft(feedback, adjustedAvgMeanGazePoint.X);
                        Panel.SetZIndex(feedback, 2);

                        // Draw also to Drawing Visual
                        drawingContext.DrawEllipse(
                            feedback.EllipseBackground.Fill,
                            null,
                            adjustedAvgMeanGazePoint,
                            stdDevGazeCoordinates,
                            stdDevGazeCoordinates);


                        #region Connect with line if more than 80 px away

                        // When point is more than 80px. away connect it with a dotted line to center
                        if (trackerPoint.meanGazeCoordinatesLeft.Y >= calPoint.Point.Y + 80 ||
                            trackerPoint.meanGazeCoordinatesLeft.Y <= calPoint.Point.Y + 80)
                        {
                            // Start line
                            var myLine = new Line { X1 = calPoint.Point.X, Y1 = calPoint.Point.Y };

                            // End line
                            if (!double.IsNaN(feedback.Width))
                                myLine.X2 = adjustedAvgMeanGazePoint.X + feedback.Width / 2;

                            if (!double.IsNaN(feedback.Height))
                                myLine.Y2 = adjustedAvgMeanGazePoint.Y + feedback.Height / 2;

                            myLine.Stroke = Brushes.LightSteelBlue;

                            var dashes = new DoubleCollection { 2, 2 };
                            myLine.StrokeDashArray = dashes;
                            myLine.StrokeDashCap = PenLineCap.Round;

                            CanvasCalibration.Children.Add(myLine);

                            // Draw also to Drawing Visual
                            Pen linePen = new Pen();
                            linePen.Brush = myLine.Stroke;
                            linePen.DashStyle = DashStyles.Dash;
                            linePen.DashCap = PenLineCap.Round;

                            drawingContext.DrawLine(
                                linePen,
                                new Point(myLine.X1, myLine.Y1),
                                new Point(myLine.X2, myLine.Y2));
                        }

                        #endregion

                        #endregion

                        #region Calculate sum std dev

                        sumDistFromTargetsY += avgMeanGazeCoordinates.Y - (calPoint.Point.Y - calPoint.Diameter / 2);
                        sumDistFromTargetsX += avgMeanGazeCoordinates.X - (calPoint.Point.X - calPoint.Diameter / 2);

                        double sumStdDevAvg = trackerPoint.stdDeviationGazeCoordinatesLeft + trackerPoint.stdDeviationGazeCoordinatesRight;
                        sumStdDevAvg = sumStdDevAvg / 2;
                        sumStdDeviation += sumStdDevAvg;

                        #endregion
                    }
                }

                #endregion

                #region Draw calibration points (last to stay on top)

                calPoint.Visibility = Visibility.Visible;
                calPoint.Opacity = 0.5;
                CanvasCalibration.Children.Add(calPoint);
                Canvas.SetTop(calPoint, calPoint.Point.Y - calPoint.Diameter / 2);
                Canvas.SetLeft(calPoint, calPoint.Point.X - calPoint.Diameter / 2);
                Panel.SetZIndex(calPoint, 99);

                // Draw also to Drawing Visual
                SolidColorBrush whiteBrush = new SolidColorBrush();
                whiteBrush.Color = Color.FromArgb(122, 255, 255, 255);
                drawingContext.DrawEllipse(
                   whiteBrush,
                    null,
                    calPoint.Point,
                    calPoint.Diameter / 2,
                    calPoint.Diameter / 2);
                drawingContext.DrawEllipse(
                   null,
                    new Pen(Brushes.Black, 1f),
                    calPoint.Point,
                    8,
                    8);

                // Enable recalibration 
                calPoint.OnClick += RecalibratePoint;

                #endregion
            }

            // Generate values for the 1-5 star ratings-control from quality feeback
            AvgSumStdDev = sumStdDeviation / calibTargets.Count;
            AvgDistFromTargets = ((sumDistFromTargetsY + sumDistFromTargetsX) / 2) / calibTargets.Count;

            // Now perform the rendering of the Visual to a bitmap
            drawingContext.Close();
            var bmp = new RenderTargetBitmap((int)ActualWidth, (int)ActualHeight, 96, 96, PixelFormats.Pbgra32);
            bmp.Render(drawingVisual);
           
            return bmp;
        }

        public void RecalibratePoint(object sender, RoutedEventArgs e)
        {
            isRecalibratingPoint = true;
            CanvasCalibration.Children.Clear();
            Cursor = Cursors.None; // Redisplay after calibration

            // Add single point
            var calPoint = sender as CalibrationPoint;
            CanvasCalibration.Children.Add(calPoint);

            if (calPoint != null)
            {
                Canvas.SetTop(calPoint, calPoint.Point.Y - calPoint.Diameter / 2);
                Canvas.SetLeft(calPoint, calPoint.Point.X - calPoint.Diameter / 2);

                // Run animation, when done the isRecalibratingPoint will trigger tracker.CalibrationEnd -> calculate coords..
                calPoint.OnPointDisplayed += PointStop;
                calPoint.RunScalePointAnimation();
                EyeTrackerPointStart(calPoint);
            }
        }

        #endregion

        #region Events and Eye Tracker Signals

        private void PointStop(object sender, RoutedEventArgs e)
        {
            EyeTrackerPointStop(sender as CalibrationPoint);
        }

        private void EyeTrackerPointStart(CalibrationPoint calPoint)
        {
            currentPoint = calPoint;
            //GTCommands.Instance.CalibrationPointStart(InstancePoint.Number, InstancePoint.Point);

            var args1 = new RoutedEventArgs();
            args1 = new RoutedEventArgs(PointStartEvent, calPoint);
            RaiseEvent(args1);
        }

        private void EyeTrackerPointStop(CalibrationPoint calPoint)
        {
            currentPoint = calPoint;
            var args1 = new RoutedEventArgs();
            args1 = new RoutedEventArgs(PointStopEvent, calPoint);
            RaiseEvent(args1);
        }


        public event RoutedEventHandler OnPointStart
        {
            add { AddHandler(PointStartEvent, value); }
            remove { RemoveHandler(PointStartEvent, value); }
        }

        public event RoutedEventHandler OnPointStop
        {
            add { AddHandler(PointStopEvent, value); }
            remove { RemoveHandler(PointStopEvent, value); }
        }

        public event RoutedEventHandler OnCalibrationStart
        {
            add { AddHandler(CalibrationStartEvent, value); }
            remove { RemoveHandler(CalibrationStartEvent, value); }
        }

        public event RoutedEventHandler OnCalibrationEnd
        {
            add { AddHandler(CalibrationEndEvent, value); }
            remove { RemoveHandler(CalibrationEndEvent, value); }
        }

        public event RoutedEventHandler OnCalibrationAbort
        {
            add { AddHandler(CalibrationAbortEvent, value); }
            remove { RemoveHandler(CalibrationAbortEvent, value); }
        }

        #endregion

        #region Calibration Points

        private static CalibrationPoint CopyPoint(CalibrationPoint point)
        {
            var newPoint = new CalibrationPoint
                               {
                                   Number = point.Number,
                                   Point = point.Point,
                                   PointColor = point.PointColor,
                                   Diameter = point.Diameter,
                                   PointDuration = point.PointDuration
                               };
            return newPoint;
        }

        private CalibrationPoint GetPointAt(int indexPosition)
        {
            return calibrationPoints[indexPosition];
        }

        private CalibrationPoint GetNextPoint()
        {
            try
            {
                indexInstancePoint++; // starts at -1

                if (indexInstancePoint < NumberOfPoints)
                    return calibrationPoints[indexInstancePoint];
                else
                    return null;

            }
            catch (Exception ex)
            {
                ErrorLogger.ProcessException(ex, false);
                return null;
            }
        }


        private void RandomizeCalibrationPoints()
        {
            var randomizedList = new List<CalibrationPoint>();
            var rnd = new Random();
            while (calibrationPoints.Count != 0)
            {
                int index = rnd.Next(0, calibrationPoints.Count);
                randomizedList.Add(calibrationPoints[index]);
                calibrationPoints.RemoveAt(index);
            }

            calibrationPoints = randomizedList;
        }

        bool errorLoadingImages = false;

        private void SetRandomImage(CalibrationPoint point)
        {
            if (errorLoadingImages)
                return;

            try
            {
                var imageList = new ArrayList();
                var di = new DirectoryInfo(Directory.GetCurrentDirectory() + "\\InfantGraphics\\");

                foreach (FileInfo file in di.GetFiles())
                {
                    imageList.Add(file.FullName);
                }

                var random = new Random();
                int num = random.Next(imageList.Count - 1);

                var img = new BitmapImage(new Uri(imageList[num].ToString()));
                point.PointImage(img);
            }
            catch (Exception)
            {
                errorLoadingImages = true;
                MessageBox.Show("Could not locate the \"InfantGraphics\" folder, please ensure that it exists in the application directory.");
            }
        }

        #endregion

        #region Convert point to absolute screen position

        public Point AdjustPointFromPartialCalibration(Point point)
        {
            // Check for zero
            if (Settings.Instance.Calibration.AreaWidth == 0 || Settings.Instance.Calibration.AreaHeight == 0)
                return point;

            // Check for full screen, primary
            if (Settings.Instance.Calibration.TrackingMonitor == Monitor.Primary &&
               Settings.Instance.Calibration.AreaWidth == ScreenParameters.PrimaryResolution.Width &&
               Settings.Instance.Calibration.AreaHeight == ScreenParameters.PrimaryResolution.Height)
                return point;

            // Check for full screen, secondary
            if (Settings.Instance.Calibration.TrackingMonitor == Monitor.Secondary &&
               Settings.Instance.Calibration.AreaWidth == ScreenParameters.SecondaryResolution.Width &&
               Settings.Instance.Calibration.AreaHeight == ScreenParameters.SecondaryResolution.Height)
                return point;

            // Partial calibration, scale
            Point p = new Point(point.X, point.Y);
            double adjustX = TrackingScreen.TrackingScreenWidth / 2 - (Settings.Instance.Calibration.AreaWidth / 2);
            double adjustY = TrackingScreen.TrackingScreenHeight / 2 - (Settings.Instance.Calibration.AreaHeight / 2);

            p.X += adjustX;
            p.Y += adjustY;

            return p;
        }

        public Point AdjustPointToPartial(Point point)
        {
            // Check for zero
            if (Settings.Instance.Calibration.AreaWidth == 0 || Settings.Instance.Calibration.AreaHeight == 0)
                return point;

            // Check for full screen, primary
            if (Settings.Instance.Calibration.TrackingMonitor == Monitor.Primary &&
               Settings.Instance.Calibration.AreaWidth == ScreenParameters.PrimaryResolution.Width &&
               Settings.Instance.Calibration.AreaHeight == ScreenParameters.PrimaryResolution.Height)
                return point;

            // Check for full screen, secondary
            if (Settings.Instance.Calibration.TrackingMonitor == Monitor.Secondary &&
               Settings.Instance.Calibration.AreaWidth == ScreenParameters.SecondaryResolution.Width &&
               Settings.Instance.Calibration.AreaHeight == ScreenParameters.SecondaryResolution.Height)
                return point;

            // Partial calibration, scale
            Point p = new Point(point.X, point.Y);
            double adjustX = TrackingScreen.TrackingScreenWidth / 2 - (Settings.Instance.Calibration.AreaWidth / 2);
            double adjustY = TrackingScreen.TrackingScreenHeight / 2 - (Settings.Instance.Calibration.AreaHeight / 2);

            p.X -= adjustX;
            p.Y -= adjustY;

            return p;
        }

        #endregion

        #region DragDrop Canvas

        private void CanvasCalibration_OnUIElementMoved(object sender, RoutedEventArgs e)
        {
            var replacePoint = e.OriginalSource as CalibrationPoint;
            var oldPoint = new CalibrationPoint(99, new Point(), 0, new SolidColorBrush(), 1);

            int index = 0;
            int foundIndex = 0;

            foreach (CalibrationPoint c in calibrationPoints)
            {
                if (replacePoint != null)
                    if (c.Number == replacePoint.Number)
                        foundIndex = index;

                index++;
            }

            calibrationPoints[foundIndex] = replacePoint;
            RecalibratePoint(calibrationPoints[foundIndex], new RoutedEventArgs());
        }

        #endregion

    }
}