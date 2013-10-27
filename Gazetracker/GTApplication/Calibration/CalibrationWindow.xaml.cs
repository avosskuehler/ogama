using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

using GTLibrary;
using GTLibrary.Logging;
using GTLibrary.Utils;
using GTCommons;
using System.IO;
using GTCommons.Enum;
using GTSettings;

namespace GTApplication.CalibrationUI
{
  using GTApplication.Tools;

  public partial class CalibrationWindow : Window
  {

    #region Variables

    private static CalibrationWindow instance;
    private readonly VisualGazePoint visualPoint;
    //private double margin = 100;
    public bool ExportCalibrationResults { get; set; }

    #endregion


    #region Constructor

    public CalibrationWindow()
    {
      //this.DataContext = TrackingScreen.TrackingScreenBounds;
      InitializeComponent();
      visualPoint = new VisualGazePoint();

      // Get tracking-screen size
      Left = TrackingScreen.TrackingScreenLeft;
      Top = TrackingScreen.TrackingScreenTop;
      Width = TrackingScreen.TrackingScreenWidth;
      Height = TrackingScreen.TrackingScreenHeight;

      ExportCalibrationResults = false;

      // Hide menues and stuff
      calibrationMenu.Visibility = Visibility.Collapsed;
      sharingUC.Visibility = Visibility.Collapsed;

      calibrationMenu.OnAccept += AcceptCalibration;
      calibrationMenu.OnShare += ShareData;
      calibrationMenu.OnRecalibrate += RedoCalibration;
      calibrationMenu.OnToggleCrosshair += ToggleCrosshair;
      calibrationMenu.OnToggleSmoothing += ToggleSmoothing;
      calibrationMenu.OnAccuracyParamsChange += AccuracyParamsChange;

      sharingUC.OnDataSent += sharingUC_OnDataSent;

      KeyDown += Calibration_KeyDown;
    }

    #endregion


    #region Public Start/Stop/Reset/Recalibrate

    public void Start()
    {
      CanvasRoot.Width = Width; // Tracking screen width
      CanvasRoot.Height = Height;

      // Partial calibration if not zero or not full screen, set active calibration area by applying margin to the control. 
      if (GTSettings.Settings.Instance.Calibration.AreaWidth != 0 &&
          Settings.Instance.Calibration.AreaHeight != 0 &&
          Settings.Instance.Calibration.AreaWidth != Width &&
          Settings.Instance.Calibration.AreaHeight != Height)
      {
        calibrationControl.Width = Settings.Instance.Calibration.AreaWidth;
        calibrationControl.Height = Settings.Instance.Calibration.AreaHeight;

        // Align the calibration control (center)
        if (Settings.Instance.Calibration.CalibrationAlignment == CalibrationAlignmentEnum.Center)
        {
          Canvas.SetTop(calibrationControl, Height / 2 - (calibrationControl.Height / 2));
          Canvas.SetLeft(calibrationControl, Width / 2 - (calibrationControl.Width / 2));
        }
        else
        {
          // Align left/right, top, bottom
          switch (Settings.Instance.Calibration.CalibrationAlignment)
          {
            case CalibrationAlignmentEnum.Top:
              Canvas.SetTop(calibrationControl, 0);
              Canvas.SetLeft(calibrationControl, Width / 2 - (calibrationControl.Width / 2));
              break;
            case CalibrationAlignmentEnum.Bottom:
              Canvas.SetTop(calibrationControl, CanvasRoot.Height - calibrationControl.Height);
              Canvas.SetLeft(calibrationControl, Width / 2 - (calibrationControl.Width / 2));
              break;
            case CalibrationAlignmentEnum.Left:
              Canvas.SetTop(calibrationControl, Height / 2 - (calibrationControl.Height / 2));
              Canvas.SetLeft(calibrationControl, 0);
              break;
            case CalibrationAlignmentEnum.Right:
              Canvas.SetTop(calibrationControl, Height / 2 - (calibrationControl.Height / 2));
              Canvas.SetLeft(calibrationControl, CanvasRoot.Width - calibrationControl.Width);
              break;
          }
        }
      }
      else
      {
        calibrationControl.Width = Width;
        calibrationControl.Height = Height;
        Canvas.SetTop(calibrationControl, 0);
        Canvas.SetLeft(calibrationControl, 0);
      }

      // Initialize calibration control and settings
      calibrationControl.NumberOfPoints = Settings.Instance.Calibration.NumberOfPoints;
      calibrationControl.RandomOrder = Settings.Instance.Calibration.RandomizePointOrder;
      calibrationControl.ColorPoints = Settings.Instance.Calibration.PointColor;
      calibrationControl.ColorBackground = Settings.Instance.Calibration.BackgroundColor;
      calibrationControl.PointDuration = Settings.Instance.Calibration.PointDuration;
      calibrationControl.PointTransitionDuration = Settings.Instance.Calibration.PointTransitionDuration;
      calibrationControl.PointDiameter = Settings.Instance.Calibration.PointDiameter;
      calibrationControl.Acceleration = Settings.Instance.Calibration.Acceleration;
      calibrationControl.Deacceleration = Settings.Instance.Calibration.Deacceleration;
      calibrationControl.UseInfantGraphics = Settings.Instance.Calibration.UseInfantGraphics;



      // Register for events
      calibrationControl.OnCalibrationStart += calibrationControl_Start;
      calibrationControl.OnPointStart += calibrationControl_PointStart;
      calibrationControl.OnPointStop += calibrationControl_PointEnd;
      calibrationControl.OnCalibrationEnd += calibrationControl_End;

      this.BringIntoView();
      // Start calibration procedure
      calibrationControl.Start();
    }

    public void Stop()
    {
      calibrationControl.Stop();
    }

    public void Reset()
    {
      instance = new CalibrationWindow();
    }

    public void Recalibrate()
    {
      calibrationControl.CanvasCalibration.Background = calibrationControl.ColorBackground;
      calibrationMenu.Visibility = Visibility.Collapsed;
      calibrationControl.Reset();
      calibrationControl.Start();
    }

    #endregion


    #region OnEvents

    #region OnEvents from CalibrationControl / Tracker

    private void calibrationControl_Start(object sender, RoutedEventArgs e)
    {
      // Notify tracker that calibration starts
      Tracker.Instance.CalibrationStart();
    }

    private void calibrationControl_PointStart(object sender, RoutedEventArgs e)
    {
      var control = sender as CalibrationControl;
      // Notify tracker that a point is displayed (start sampling)

      if (control != null)
        Tracker.Instance.CalibrationPointStart(control.CurrentPoint.Number, calibrationControl.AdjustPointFromPartialCalibration(calibrationControl.CurrentPoint.Point)); // Convert point to absolute screen pos.
    }

    private void calibrationControl_PointEnd(object sender, RoutedEventArgs e)
    {
      var control = sender as CalibrationControl;

      //// Notify tracker that a point has been displayed (stop sampling)
      if (control == null) return;

      Tracker.Instance.CalibrationPointEnd();

      if (!control.IsRecalibratingPoint) return;

      calibrationControl_End(null, null);
      control.IsRecalibratingPoint = false;
    }

    private void calibrationControl_End(object sender, RoutedEventArgs e)
    {
      try
      {
        // Notify tracker that calibration has ended, it will raise an event when calculations are done
        Tracker.Instance.OnCalibrationComplete += Tracker_OnCalibrationCompleted;
        Tracker.Instance.CalibrationEnd();
      }
      catch (Exception ex)
      {
        ErrorLogger.ProcessException(ex, false);
      }
    }

    private void Tracker_OnCalibrationCompleted(object sender, EventArgs e)
    {
      //if (!tracker.Calibration.IsCalibrated)
      //{
      //    MessageWindow msgWin = new MessageWindow();
      //    msgWin.Text = "Calibration unsuccessful. Not enough images could be captured during the calibration. Try to calibrate again.";
      //    msgWin.Show();
      //    msgWin.Closed += new EventHandler(errorMsgWin_Closed);
      //    return;
      //}

      if (calibrationMenu.CheckBoxVisualFeedback.IsChecked.Value)
        visualPoint.Visibility = Visibility.Visible;

      // Unregister event
      Tracker.Instance.OnCalibrationComplete -= Tracker_OnCalibrationCompleted;

      // Draw feedback on calibration points and the tracker.calibrationTargets overlaid
      BitmapSource calibrationResult =
          calibrationControl.VisualizeCalibrationResults(Tracker.Instance.Calibration.CalibMethod.CalibrationTargets);

      // Generate indicator of calibration quality (1-5 star)
      calibrationMenu.GenerateQualityIndicator(Tracker.Instance.Calibration.CalibMethod.Degrees);
      calibrationMenu.SetAccuracy(
          Tracker.Instance.Calibration.CalibMethod.DegreesLeft,
          Tracker.Instance.Calibration.CalibMethod.DegreesRight);

      if (ExportCalibrationResults)
      {
        // Send event
        GTCommands.Instance.Calibration.ExportResults(calibrationResult, calibrationMenu.ratingCalibrationQuality.RatingValue);
      }
      else
      {
        // Show menu to accept or recalibrate
        calibrationMenu.Visibility = Visibility.Visible;

        if (calibrationMenu.CheckBoxVisualFeedback.IsChecked.Value)
        {
          CanvasRoot.Children.Remove(visualPoint);
          ToggleCrosshair(null, null);
        }
      }
    }

    #endregion


    #region Events - StopOnEscape or ErrorMsgWin Close

    private void Calibration_KeyDown(object sender, KeyEventArgs e)
    {
      // Exit on Escape-key
      if (e.Key.Equals(Key.Escape))
      {
        calibrationControl.Stop();
        Tracker.Instance.CalibrationAbort();
        Close();

      }
    }

    private void errorMsgWin_Closed(object sender, EventArgs e)
    {
      Close();
    }

    #endregion


    #region Calibration menu events (accept/redo/share)

    private void RedoCalibration(object sender, RoutedEventArgs e)
    {
      visualPoint.Visibility = Visibility.Collapsed;
      Recalibrate();
    }

    private void AcceptCalibration(object sender, RoutedEventArgs e)
    {
      // Hide UI
      calibrationMenu.Visibility = Visibility.Collapsed;
      this.visualPoint.Visibility = Visibility.Collapsed;

      // Grab screenshot
      string savePath =
          GTPath.GetLocalApplicationDataPath()
          + Path.DirectorySeparatorChar
          + "Calibration"
          + Path.DirectorySeparatorChar
          + Tracker.Instance.Calibration.ID
          + Path.DirectorySeparatorChar
          + "calibrationScreen.png";
      SaveImage(savePath, CanvasRoot.GetScreenShot(1));

      // Dump Instance settings into the calibration folder
      string settingsFile = Settings.Instance.GetLatestConfigurationFile();

      // On first start there is no latest configuration file
      if (settingsFile != string.Empty)
      {
        FileInfo fileSettings = new FileInfo(Settings.Instance.FileSettings.SettingsDirectory +
          Path.DirectorySeparatorChar + settingsFile);

        try
        {
          File.Copy(fileSettings.FullName, Tracker.Instance.Calibration.DataFolder + "\\" + fileSettings.Name);
        }
        catch (Exception ex)
        {
          Console.Out.WriteLine("Error dumping calibration data in CalibrationWindow.AcceptCalibration(), message: " + ex.Message);
        }
      }

      // Trigger global calibration accept (will hide this)
      GTCommands.Instance.Calibration.Accept();
    }

    private void ShareData(object sender, RoutedEventArgs e)
    {
      calibrationMenu.Visibility = Visibility.Collapsed;
      sharingUC.SendData(Tracker.Instance, CanvasRoot.GetScreenShot(1));
      sharingUC.Visibility = Visibility.Visible;
    }

    private static void SaveImage(string file, BitmapSource bmpScr)
    {
      if (file == "") return;

      try
      {
        FileStream fileStream = new FileStream(file, FileMode.Create);

        JpegBitmapEncoder encoder = new JpegBitmapEncoder();
        encoder.Frames.Add(BitmapFrame.Create(bmpScr));
        encoder.QualityLevel = 100;
        encoder.Save(fileStream);
        fileStream.Close();
        fileStream.Dispose();
      }
      catch (Exception e)
      {
        Console.WriteLine("Exception: " + e.Message);
      }
    }
    private void sharingUC_OnDataSent(object sender, RoutedEventArgs e)
    {
      sharingUC.Visibility = Visibility.Collapsed;
      calibrationMenu.Visibility = Visibility.Visible;
    }

    #endregion


    #endregion


    #region Crosshair - Visual feedback on gaze position

    private void ToggleCrosshair(object sender, RoutedEventArgs e)
    {
      if (calibrationMenu.CheckBoxVisualFeedback.IsChecked.Value)
      {
        RegisterForGazeDataEvent();

        // Add crosshair (visual feedback indicator) to canvas
        CanvasRoot.Children.Add(visualPoint);

        Canvas.SetTop(visualPoint, 0);
        Canvas.SetLeft(visualPoint, 0);
        Panel.SetZIndex(visualPoint, 3);
      }
      else
      {
        UnregisterForGazeDataEvent();
        CanvasRoot.Children.Remove(visualPoint);
      }
    }

    private void ToggleSmoothing(object sender, RoutedEventArgs e)
    {
      if (calibrationMenu.CheckBoxSmooth.IsChecked.Value)
        Settings.Instance.Processing.EyeMouseSmooth = true;
      else
        Settings.Instance.Processing.EyeMouseSmooth = false;

      UnregisterForGazeDataEvent();
      RegisterForGazeDataEvent();
    }

    private void AccuracyParamsChange(object sender, RoutedEventArgs e)
    {
      if (calibrationMenu.DistanceFromScreen != Settings.Instance.Calibration.DistanceFromScreen)
      {
        Settings.Instance.Calibration.DistanceFromScreen = calibrationMenu.DistanceFromScreen;
        double left = Tracker.Instance.Calibration.CalibMethod.CalculateDegreesLeft();  // recalculate
        double right = Tracker.Instance.Calibration.CalibMethod.CalculateDegreesRight(); // recalculate
        calibrationMenu.SetAccuracy(left, right);
      }
    }


    private void RegisterForGazeDataEvent()
    {
      if (Settings.Instance.Processing.EyeMouseSmooth)
        Tracker.Instance.GazeDataSmoothed.GazeDataChanged += GazeDataRaw_OnNewGazeData;
      else
        Tracker.Instance.GazeDataRaw.GazeDataChanged += GazeDataRaw_OnNewGazeData;
    }

    private void UnregisterForGazeDataEvent()
    {
      try
      {
        Tracker.Instance.GazeDataSmoothed.GazeDataChanged -= GazeDataRaw_OnNewGazeData;
        Tracker.Instance.GazeDataRaw.GazeDataChanged -= GazeDataRaw_OnNewGazeData;
      }
      catch (Exception)
      {
      }
    }

    private void GazeDataRaw_OnNewGazeData(double x, double y)
    {
      // On new data move the cross-hair
      CanvasRoot.Dispatcher.BeginInvoke
          (
              DispatcherPriority.Normal,
              new Action
                  (
                  delegate
                  {
                    if (x < 0 || y < 0) return;
                    Canvas.SetTop(visualPoint, y - visualPoint.Height / 2);
                    Canvas.SetLeft(visualPoint, x - visualPoint.Width / 2);
                  }
                  )
          );
    }

    #endregion


    #region Set/Get

    public static CalibrationWindow Instance
    {
      get
      {
        if (instance == null)
        {
          instance = new CalibrationWindow();
        }

        return instance;
      }
    }

    public CalibrationPoint CurrentPoint
    {
      get { return calibrationControl.CurrentPoint; }
    }

    public GTGazeData GazeDataRaw { get; set; }

    #endregion

  }
}