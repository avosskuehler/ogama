// <copyright file="Tracker.cs" company="ITU">
// ******************************************************
// GazeTrackingLibrary for ITU GazeTracker
// Copyright (C) 2010 Martin Tall. All rights reserved. 
// ------------------------------------------------------------------------
// We have a dual licence, open source (GPLv3) for individuals - licence for commercial ventures.
// You may not use or distribute any part of this software in a commercial product. Contact us to arrange a licence. 
// We accept no responsibility or liability.
// </copyright>
// <author>Martin Tall</author>
// <email>info@martintall.com</email>

using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Threading;
using GTNetworkClient;

using GTLibrary;
using GTLibrary.Logging;
using GTLibrary.Utils;
using GTCommons;
using GTCommons.Enum;
using Application = System.Windows.Application;
using KeyEventArgs = System.Windows.Input.KeyEventArgs;
using Point = System.Windows.Point;
using Settings = GTSettings.Settings;

namespace GTApplication
{
  using GTApplication.CalibrationUI;
  using GTApplication.Network;
  using GTApplication.SettingsUI;
  using GTApplication.Tools;
  using GTApplication.TrackerViewer;

  #region Includes

  // System classes

  // GazeTracker classes

  //using GazeTrackingLibrary.Illumination;

  #endregion


  public partial class GTApplicationMainWindow
  {

    #region Variables

    private readonly CrosshairDriver crosshairDriver = new CrosshairDriver();
    private readonly MouseDriver mouseDriver = new MouseDriver();
    private readonly TCPIPServer tcpipServer = new TCPIPServer();
    private Process clientUIProc;
    private bool isRunning;
    private MessageWindow msgWindow;
    // private Tracker tracker;

    #endregion


    #region Constructor / Init methods

    public GTApplicationMainWindow()
    {
      // Little fix for colorschema (must run before initializing)
      ComboBoxBackgroundColorFix.Initialize();

      // Register for special error messages
      ErrorLogger.TrackerError += tracker_OnTrackerError;

      this.ContentRendered += new EventHandler(GTApplicationMainWindow_ContentRendered);

      InitializeComponent();

    }

    private void GTApplicationMainWindow_ContentRendered(object sender, EventArgs e)
    {
      // Load GTSettings
      Settings.Instance.LoadLatestConfiguration();

      // Camera initialization and start frame grabbing
      if (GTHardware.Camera.Instance.DeviceType != GTHardware.Camera.DeviceTypeEnum.None)
      {
        // If DirectShow camera, init using saved settings
        if (GTHardware.Camera.Instance.DeviceType == GTHardware.Camera.DeviceTypeEnum.DirectShow)
          GTHardware.Camera.Instance.SetDirectShowCamera(GTSettings.Settings.Instance.Camera.DeviceNumber, GTSettings.Settings.Instance.Camera.DeviceMode);
        else
          GTHardware.Camera.Instance.Device.Initialize();

        GTHardware.Camera.Instance.Device.Start();
      }
      else
      {
        // No camera detected, display message and quit
        ShowMessageNoCamera();
        //this.Close();
        //return;
      }

      // Create Tracker
      //tracker = new Tracker(GTCommands.Instance); // Hook up commands and events to tracker

      SettingsWindow.Instance.Title = "SettingsWindow"; // Just touch it..


      // Video preview window (tracker draws visualization)
      videoImageControl.Start();

      // Events
      RegisterEventListners();

      // Start servers
      tcpipServer.Start();
      Tracker.Instance.Server.IsEnabled = GTSettings.Settings.Instance.Network.UDPServerEnabled;


      // Register event listners for incoming TCP/IP commands
      RegisterForIncomingServerRequests();

      menuBarIcons.IsSettingsVisible = false;

      // Show window
      Show();

      // Set this process to real-time priority
      Process.GetCurrentProcess().PriorityClass = ProcessPriorityClass.High;
    }

    #endregion


    private void StartStop(object sender, RoutedEventArgs e)
    {
      if (Tracker.Instance.Calibration.IsCalibrated == false)
      {
        msgWindow = new MessageWindow("You need to calibrate before starting");
        msgWindow.Show();
        return;
      }

      // Starting
      if (!isRunning)
      {
        #region EyeMouse

        // Start eye mouse (register listner for gazedata events)
        if (Settings.Instance.Processing.EyeMouseEnabled)
        {
          if (Settings.Instance.Processing.EyeMouseSmooth)
            Tracker.Instance.GazeDataSmoothed.GazeDataChanged += mouseDriver.Move;
          else
            Tracker.Instance.GazeDataRaw.GazeDataChanged += mouseDriver.Move;
        }

        #endregion

        #region Crosshair

        if (Settings.Instance.Processing.EyeCrosshairEnabled)
        {
          crosshairDriver.Show();

          if (Settings.Instance.Processing.EyeMouseSmooth)
            Tracker.Instance.GazeDataSmoothed.GazeDataChanged += crosshairDriver.Move;
          else
            Tracker.Instance.GazeDataRaw.GazeDataChanged += crosshairDriver.Move;
        }

        #endregion

        #region TCPIP Server

        // Start UDP data server (if enabled)
        Tracker.Instance.Server.IsEnabled = GTSettings.Settings.Instance.Network.UDPServerEnabled;

        // Start logging (if enabled)
        Tracker.Instance.LogData.IsEnabled = GTSettings.Settings.Instance.FileSettings.LoggingEnabled;

        #endregion

        BtnStartStop.Label = "Stop";

        //if(Settings.Instance.Processing.EyeMouseEnabled)
        //    BtnStartStop.ActivationMethod = "Dwell";


        isRunning = true;
      }

          // Stopping
      else
      {
        #region EyeMouse

        // Stop eye mouse (unregister events)
        if (Settings.Instance.Processing.EyeMouseEnabled)
        {
          if (Settings.Instance.Processing.EyeMouseSmooth)
            Tracker.Instance.GazeDataSmoothed.GazeDataChanged -= mouseDriver.Move;
          else
            Tracker.Instance.GazeDataRaw.GazeDataChanged -= mouseDriver.Move;
        }

        #endregion

        #region Crosshair

        if (Settings.Instance.Processing.EyeCrosshairEnabled)
        {
          if (Settings.Instance.Processing.EyeMouseSmooth)
            Tracker.Instance.GazeDataSmoothed.GazeDataChanged -= crosshairDriver.Move;
          else
            Tracker.Instance.GazeDataRaw.GazeDataChanged -= crosshairDriver.Move;

          crosshairDriver.Hide();
        }

        #endregion

        #region TCPIP Server

        // Stop UDP data server (if enabled)
        if (Tracker.Instance.Server.IsEnabled)
          Tracker.Instance.Server.IsEnabled = false;

        if (Tracker.Instance.LogData.IsEnabled)
          Tracker.Instance.LogData.IsEnabled = false; // Will stop and close filestream

        #endregion

        BtnStartStop.Label = "Start";

        //if(Settings.Instance.Processing.EyeMouseEnabled)
        //    BtnStartStop.ActivationMethod = "Mouse";

        isRunning = false;
      }
    }


    #region Events

    private void RegisterEventListners()
    {
      #region Settings

      GTCommands.Instance.Settings.OnSettings += OnSettings;

      #endregion

      #region Camera

      GTCommands.Instance.Camera.OnCameraChange += OnCameraChanged;

      #endregion

      #region TrackerViewer

      GTCommands.Instance.TrackerViewer.OnVideoDetach += OnVideoDetach;
      GTCommands.Instance.TrackerViewer.OnTrackBoxShow += OnTrackBoxShow;
      GTCommands.Instance.TrackerViewer.OnTrackBoxHide += OnTrackBoxHide;

      #endregion

      #region Calibration

      GTCommands.Instance.Calibration.OnAccepted += OnCalibrationAccepted;
      GTCommands.Instance.Calibration.OnStart += OnCalibrationStart;
      GTCommands.Instance.Calibration.OnRunning += OnCalibrationRunning;

      GTCommands.Instance.Calibration.OnPointStart += OnPointStart;
      //GTCommands.Instance.Calibration.OnPointStart += new GTCommons.Events.CalibrationPointEventArgs.CalibrationPointEventHandler(OnPointStart);
      //GTCommands.Instance.Calibration.OnPointStart += new GTApplication.Calibration.Events.CalibrationPointEventArgs.CalibrationPointEventHandler(OnPointStart);

      GTCommands.Instance.Calibration.OnAbort += OnCalibrationAbort;
      GTCommands.Instance.Calibration.OnEnd += OnCalibrationEnd;

      #endregion

      #region Misc

      GTCommands.Instance.OnNetworkClient += OnNetworkClient;

      #endregion

      #region This window

      ExpanderVisualization.Expanded += new RoutedEventHandler(ExpanderVisualization_Expanded);
      ExpanderVisualization.Collapsed += new RoutedEventHandler(ExpanderVisualization_Collapsed);
      Activated += Window1_Activated;
      Deactivated += Window1_Deactivated;

      KeyDown += KeyDownAction;

      #endregion
    }

    private void ExpanderVisualization_Collapsed(object sender, RoutedEventArgs e)
    {
      videoImageControl.Overlay.GridVisualization.Visibility = Visibility.Collapsed;
    }

    private void ExpanderVisualization_Expanded(object sender, RoutedEventArgs e)
    {
      videoImageControl.Overlay.GridVisualization.Visibility = Visibility.Visible;
    }


    private void tracker_OnTrackerError(string message)
    {
      msgWindow = new MessageWindow { Text = message };
      msgWindow.Show();
    }

    private void KeyDownAction(object sender, KeyEventArgs e)
    {
      switch (e.Key)
      {
        case Key.Escape:
          if (isRunning)
            StartStop(null, null);
          break;

        case Key.C:
          GTCommands.Instance.Calibration.Start();
          break;

        case Key.S:
          if (!isRunning)
            StartStop(null, null);
          break;
      }
    }

    #endregion


    #region On GTCommands -> actions

    #region Settings

    private void ShowSetupWindow(object sender, RoutedEventArgs e)
    {
      //if (SettingsWindow.Instance.Visibility != Visibility.Collapsed) return;
      SettingsWindow.Instance.Visibility = Visibility.Visible;
      SettingsWindow.Instance.Focus();

      Settings.Instance.Visualization.VideoMode = VideoModeEnum.Processed;

      if (SettingsWindow.Instance.HasBeenMoved != false) return;
      SettingsWindow.Instance.Left = Left + Width + 5;
      SettingsWindow.Instance.Top = Top;
    }

    private static void OnSettings(object sender, RoutedEventArgs e)
    {
      SettingsWindow.Instance.Visibility = Visibility.Visible;
    }

    #endregion

    #region TrackerViewer

    private void OnVideoDetach(object sender, RoutedEventArgs e)
    {
      int width = 0;
      int height = 0;

      VideoViewer.Instance.SetSizeAndLabels();

      // If ROI has been set display at twice the image size
      if (GTHardware.Camera.Instance.Device != null && GTHardware.Camera.Instance.Device.IsROISet)
      {
        width = GTHardware.Camera.Instance.ROI.Width * 2;
        height = GTHardware.Camera.Instance.ROI.Height * 2;
      }
      else
      {
        width = GTHardware.Camera.Instance.Width;
        height = GTHardware.Camera.Instance.Height;
      }

      int posX = Convert.ToInt32(Left - width - 5);
      int posY = Convert.ToInt32(Top);

      this.videoImageControl.VideoOverlayTopMost = false;

      VideoViewer.Instance.ShowWindow(width, height);
    }

    private void OnTrackBoxShow(object sender, RoutedEventArgs e)
    {
      //if (trackBoxUC.Visibility == Visibility.Collapsed)
      //    trackBoxUC.Visibility = Visibility.Visible;
    }

    private void OnTrackBoxHide(object sender, RoutedEventArgs e)
    {
      //if (trackBoxUC.Visibility == Visibility.Visible)
      //    trackBoxUC.Visibility = Visibility.Collapsed;
    }

    private void OnROIChange(Rectangle newROI)
    {
      //Dispatcher.Invoke
      //    (
      //        DispatcherPriority.ApplicationIdle,
      //        new Action
      //            (
      //            delegate { trackBoxUC.UpdateROI(newROI); }
      //            )
      //    );
    }

    #endregion

    #region Calibration

    private void Calibrate(object sender, RoutedEventArgs e)
    {
      GTCommands.Instance.Calibration.Start();
      videoImageControl.VideoOverlayTopMost = false;
    }

    private void OnCalibrationStart(object sender, RoutedEventArgs e)
    {
      CalibrationWindow.Instance.Reset();
      CalibrationWindow.Instance.Show();
      CalibrationWindow.Instance.Start();
    }

    private void OnCalibrationRunning(object sender, RoutedEventArgs e)
    {
      Tracker.Instance.CalibrationStart();
    }

    private void OnCalibrationAccepted(object sender, RoutedEventArgs e)
    {
      CalibrationWindow.Instance.Close();
      WindowState = WindowState.Normal;
      BtnStartStop.IsEnabled = true;
      BtnCalibrate.Label = "Recalibrate";
      this.videoImageControl.VideoOverlayTopMost = true;

      Tracker.Instance.CalibrationAccepted();
    }

    private void OnPointStart(object sender, RoutedEventArgs e)
    {
      var control = sender as CalibrationControl;
      if (control != null) Tracker.Instance.CalibrationPointStart(control.CurrentPoint.Number, control.CurrentPoint.Point);
      e.Handled = true;
    }

    //private void OnPointStart(object sender, GTApplication.Calibration.Events.CalibrationPointEventArgs e)
    //{
    //    tracker.CalibrationPointStart(e.Number, e.Point);
    //    e.Handled = true;
    //}

    //private void OnPointEnd(object sender, CalibrationPointEventArgs e)
    //{
    //    tracker.CalibrationPointEnd(e.Number, e.Point);
    //    e.Handled = true;
    //}

    private void OnCalibrationAbort(object sender, RoutedEventArgs e)
    {
      CalibrationWindow.Instance.Stop();
      Tracker.Instance.CalibrationAbort();
    }

    private void OnCalibrationEnd(object sender, RoutedEventArgs e)
    {
      Tracker.Instance.CalibrationEnd();
    }


    #endregion

    #endregion


    #region Camera/Video viewing

    private void OnCameraChanged(object sender, RoutedEventArgs e)
    {
      Tracker.Instance.SetCamera(Settings.Instance.Camera.DeviceNumber, Settings.Instance.Camera.DeviceMode);

      Point oldWinPos = new Point(VideoViewer.Instance.Top, VideoViewer.Instance.Left);
      VideoViewer.Instance.Width = Tracker.Instance.VideoWidth + videoImageControl.Margin.Left + videoImageControl.Margin.Right;
      VideoViewer.Instance.Height = Tracker.Instance.VideoHeight + videoImageControl.Margin.Top + videoImageControl.Margin.Bottom;
    }


    private void ShowMessageNoCamera()
    {
      msgWindow = new MessageWindow();
      msgWindow.Text = "The GazeTracker was unable to connect a camera. \n" +
                       "Make sure that the device is connected and that the device drivers are installed. " +
                       "Verified configurations can be found in our forum located at http://forum.gazegroup.org";
      msgWindow.Show();
      ErrorLogger.WriteLine("Fatal error on startup, could not connect to a camera.");
      msgWindow.Closed += new EventHandler(msgWindowNoCamera_Closed);
    }

    private void msgWindowNoCamera_Closed(object sender, EventArgs e)
    {
      //Environment.Exit(Environment.ExitCode);
    }

    #endregion


    #region Server

    private void RegisterForIncomingServerRequests()
    {
      //tcpipServer.OnCalibrationStart += new TCPIPServer.CalibrationStartHandler(tcpipServer_OnCalibrationStart);
      //tcpipServer.OnCalibrationAbort += new TCPIPServer.CalibrationAbortHandler(tcpipServer_OnCalibrationAbort);
      //tcpipServer.OnCalibrationParameters += tcpipServer_OnCalibrationParameters;

      /*Henriks testing area - updating the gaze tracker calibration point-by-point*/
      //tcpipServer.OnCalibrationFeedbackPoint += new TCPIPServer.CalibrationFeedbackPointHandler(tcpipServer_OnCalibrationFeedbackPoint);
      //tcpipServer.onCalibrationUpdateMethod += new TCPIPServer.CalibrationUpdateMethod(tcpipServer_onCalibrationUpdateMethod);
      /* end testing area */

      //tcpipServer.OnDataStreamStart += tcpipServer_OnDataStreamStart;
      //tcpipServer.OnDataStreamStop += tcpipServer_OnDataStreamStop;

      //tcpipServer.OnLogStart += tcpipServer_OnLogStart;
      //tcpipServer.OnLogStop += tcpipServer_OnLogStop;
      //tcpipServer.OnLogWriteLine += tcpipServer_OnLogWriteLine;
      //tcpipServer.OnLogPathSet += tcpipServer_OnLogPathSet;
      //tcpipServer.OnLogPathGet += tcpipServer_OnLogPathGet;

      //tcpipServer.OnCameraSettings += new TCPIPServer.CameraSettingsHandler(tcpipServer_OnCameraSettings);

      //tcpipServer.OnUIMinimize += tcpipServer_OnUIMinimize;
      //tcpipServer.OnUIRestore += tcpipServer_OnUIRestore;
      //tcpipServer.OnUISettings += tcpipServer_OnUISettings;
    }


    #region Stream

    private void tcpipServer_OnDataStreamStart()
    {
      Tracker.Instance.Server.IsStreamingGazeData = true;
    }

    private void tcpipServer_OnDataStreamStop()
    {
      Tracker.Instance.Server.IsStreamingGazeData = false;
    }

    #endregion

    #region Calibration

    private void tcpipServer_OnCalibrationParameters(CalibrationParameters calParams)
    {
      // Todo: Should be sent from the tracker after applying settings everywhere..
      Tracker.Instance.Server.SendMessage(Commands.CalibrationParameters, calParams.ParametersAsString);
    }

    private void tcpipServer_OnCalibrationStart()
    {
      CalibrationWindow.Instance.Dispatcher.Invoke
          (
              DispatcherPriority.ApplicationIdle,
              new Action
                  (
                  delegate { Calibrate(null, null); }
                  )
          );
    }

    private void tcpipServer_OnCalibrationAbort()
    {
      CalibrationWindow.Instance.Dispatcher.Invoke
          (
              DispatcherPriority.ApplicationIdle,
              new Action
                  (
                  delegate
                  {
                    try
                    {
                      CalibrationWindow.Instance.Stop();
                      CalibrationWindow.Instance.Close();
                      Tracker.Instance.CalibrationAbort();
                    }
                    catch (Exception ex)
                    {
                      ErrorLogger.WriteLine(
                          "GTApplicationMainWindow.cs, error in tcpipServer_OnCalibrationAbort. Message: " +
                          ex.Message);
                    }
                  }
                  )
          );
    }


    private void tcpipServer_OnCalibrationFeedbackPoint(long time, int packagenumber, int targetX, int targetY,
                                                        int gazeX, int gazeY, float distance, int acquisitionTime)
    {
      CalibrationWindow.Instance.Dispatcher.Invoke
          (
              DispatcherPriority.ApplicationIdle,
              new Action
                  (
                  delegate
                  {
                    //pass info from the dedicated interface to the tracker class
                    var target = new System.Drawing.Point(targetX, targetY);
                    var gaze = new GTPoint(gazeX, gazeY);
                    //tracker.SaveRecalibInfo(time, packagenumber, target, gaze);

                    /* outputting the data in a local class */
                    string del = " ";
                    string msg = DateTime.Now.Ticks + del
                                 + time + del
                                 + packagenumber + del
                                 + targetX + del
                                 + targetX + del
                                 + gazeX + del
                                 + gazeY + del
                                 + distance + del
                                 + acquisitionTime;
                    Output.Instance.appendToFile(msg);
                  }
                  )
          );
    }

    void tcpipServer_onCalibrationUpdateMethod(int method)
    {
      CalibrationWindow.Instance.Dispatcher.Invoke(
          DispatcherPriority.ApplicationIdle,
          new Action(delegate { Settings.Instance.Calibration.RecalibrationType = (RecalibrationTypeEnum)method; }));
    }


    #endregion

    #region Log

    private void tcpipServer_OnLogStart()
    {
      Dispatcher.Invoke
          (
              DispatcherPriority.ApplicationIdle,
              new Action(delegate
             {
               try
               {
                 Tracker.Instance.LogData.IsEnabled = true;
               }
               catch (Exception ex)
               {
                 ErrorLogger.ProcessException(ex, false);
               }
             }));
    }


    private void tcpipServer_OnLogStop()
    {
      Dispatcher.Invoke
          (
              DispatcherPriority.ApplicationIdle,
              new Action(delegate
             {
               try
               {
                 Tracker.Instance.LogData.IsEnabled = false;
               }
               catch (Exception ex)
               {
                 ErrorLogger.ProcessException(ex, false);
               }
             }));
    }


    private void tcpipServer_OnLogPathGet()
    {
      Dispatcher.Invoke
          (
              DispatcherPriority.ApplicationIdle,
              new Action(delegate
                     {
                       try
                       {
                         Tracker.Instance.Server.SendMessage(Commands.LogPathGet + " " + Tracker.Instance.LogData.LogFilePath);
                       }
                       catch (Exception ex)
                       {
                         ErrorLogger.ProcessException(ex, false);
                       }
                     }));
    }

    private void tcpipServer_OnLogPathSet(string path)
    {
      Dispatcher.Invoke
          (
              DispatcherPriority.ApplicationIdle,
              new Action(delegate
                 {
                   try
                   {
                     Tracker.Instance.LogData.LogFilePath = path;
                   }
                   catch (Exception ex)
                   {
                     ErrorLogger.ProcessException(ex, false);
                   }
                 }));
    }

    private void tcpipServer_OnLogWriteLine(string line)
    {
      Dispatcher.Invoke
          (
              DispatcherPriority.ApplicationIdle,
              new Action(delegate
             {
               try
               {
                 Tracker.Instance.LogData.WriteLine(line);
                 Tracker.Instance.Server.SendMessage(Commands.LogWriteLine + " " + line);
               }
               catch (Exception ex)
               {
                 ErrorLogger.ProcessException(ex, false);
               }
             }));
    }

    #endregion

    #region Camera

    //void tcpipServer_OnCameraSettings(CameraSettings camSettings)
    //{
    //    this.Dispatcher.Invoke
    //    (
    //    DispatcherPriority.ApplicationIdle, new Action(delegate()
    //    {
    //        try
    //        {
    //            tracker.Camera.CameraSettings = camSettings;
    //            MessageBox.Show("CamSettings!");
    //        }
    //        catch (Exception ex)
    //        { Console.Out.WriteLine("Could not apply CameraSettings via Network call" + ex.Message); }
    //    }));

    //}

    #endregion

    #region U.I

    private void tcpipServer_OnUISettings()
    {
      Dispatcher.Invoke
          (
              DispatcherPriority.ApplicationIdle, new Action(
                 delegate
                 {
                   try
                   {
                     SettingsWindow.Instance.Visibility = Visibility.Visible;
                     SettingsWindow.Instance.WindowState = WindowState.Normal;
                   }
                   catch (Exception ex)
                   {
                     ErrorLogger.ProcessException(ex, false);
                   }
                 }));
    }

    private void tcpipServer_OnUIRestore()
    {
      Dispatcher.Invoke
          (
              DispatcherPriority.ApplicationIdle, new Action(
                  delegate
                  {
                    try
                    {
                      WindowState = WindowState.Normal;
                      SettingsWindow.Instance.WindowState = WindowState.Normal;
                    }
                    catch (Exception ex)
                    {
                      ErrorLogger.ProcessException(ex, false);
                    }
                  }));
    }

    private void tcpipServer_OnUIMinimize()
    {
      Dispatcher.Invoke
          (
              DispatcherPriority.ApplicationIdle, new Action(
                  delegate
                  {
                    try
                    {
                      WindowState = WindowState.Minimized;
                      SettingsWindow.Instance.WindowState =
                          WindowState.Minimized;
                    }
                    catch (Exception ex)
                    {
                      ErrorLogger.ProcessException(ex, false);
                    }
                  }));
    }


    #endregion

    #endregion


    #region NetworkClient

    private void OnNetworkClient(object sender, RoutedEventArgs e)
    {
      NetworkClientLaunch();
    }

    private void NetworkClientLaunch()
    {
      if (Settings.Instance.Network.ClientUIPath == "" ||
          !File.Exists(Settings.Instance.Network.ClientUIPath))
      {
        var ofd = new OpenFileDialog();
        ofd.Title = "Select the GazeTrackerClientUI executable file";
        ofd.Multiselect = false;
        ofd.Filter = "Executables (*.exe)|*.exe*;*.xml|All Files|*.*";

        if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
        {
          string[] filePath = ofd.FileNames;
          Settings.Instance.Network.ClientUIPath = filePath[0];
        }
        else
        {
        }
      }

      if (!File.Exists(Settings.Instance.Network.ClientUIPath)) return;
      var psi = new ProcessStartInfo(Settings.Instance.Network.ClientUIPath);
      clientUIProc = new Process { StartInfo = psi };
      clientUIProc.Start();
    }

    #endregion


    #region Minimize/Activate/Close main app window

    private void AppMinimize(object sender, MouseButtonEventArgs e)
    {
      // If video is detached (seperate window), stop updating images and close the window)
      if (VideoViewer.Instance.WindowState.Equals(WindowState.Normal))
      {
        VideoViewer.Instance.videoImageControl.Stop(true);
        VideoViewer.Instance.WindowState = WindowState.Minimized;
      }

      // Stop updating images in small preview box
      this.videoImageControl.Stop(true);

      // Minimize settings window
      SettingsWindow.Instance.WindowState = WindowState.Minimized;

      // Mimimize the application window
      WindowState = WindowState.Minimized;
    }

    private void AppClose(object sender, MouseButtonEventArgs e)
    {
      // Save settings 
      SettingsWindow.Instance.SaveSettings();
      //CameraSettingsWindow.Instance.Close(); //is already closed - will force the class to reinitialize only to be closed again

      // Kill the ClientUI process (if initiated)
      if (clientUIProc != null && clientUIProc.HasExited == false)
        clientUIProc.Kill();

      // Cleaup tracker & release camera
      Tracker.Instance.Cleanup();

      // Close all windows (including Visibility.Collapsed & Hidden)
      for (int i = 0; i < Application.Current.Windows.Count; i++)
        Application.Current.Windows[i].Close();

      // Null tracker..
      //Tracker.Instance = null;

      // Force exit, now dammit!
      Environment.Exit(Environment.ExitCode);
    }

    private void Window1_Activated(object sender, EventArgs e)
    {
      if (Visibility == Visibility.Visible && Settings.Instance.Visualization.IsDrawing == false)
        this.videoImageControl.Start();

      SettingsWindow.Instance.WindowState = WindowState.Normal;
      SettingsWindow.Instance.Focus();
      this.Focus();


      videoImageControl.VideoOverlayTopMost = true;
    }

    private void Window1_Deactivated(object sender, EventArgs e)
    {
      if (WindowState.Equals(WindowState.Minimized))
        videoImageControl.Stop(true);

      videoImageControl.VideoOverlayTopMost = false;
    }

    #endregion


    #region DragWindow

    private void DragWindow(object sender, MouseButtonEventArgs args)
    {
      DragMove();
    }

    #endregion
  }
}