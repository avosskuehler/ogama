using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using GazeTrackingLibrary.Settings;
using GazeTrackingLibrary.Utils;

namespace GazeTrackerUI.Settings
{
	/// <summary>
	/// Interaction logic for CalibrationSettingsWindow.xaml
	/// </summary>
	public partial class CalibrationSettingsWindow : Window
	{
        double screenWidth = 0;
        double screenHeight = 0;
        private bool hasBeenMoved = false;

		public CalibrationSettingsWindow()
		{
			this.InitializeComponent();

            double screenWidth = GazeTracker.Tools.TrackingScreen.TrackingScreenWidth;
            double screenHeight = GazeTracker.Tools.TrackingScreen.TrackingScreenHeight;

            calibrationControl.NumberOfPoints = GTSettings.Current.CalibrationSettings.NumberOfPoints;
            calibrationControl.ColorBackground = GTSettings.Current.CalibrationSettings.BackgroundColor;
            calibrationControl.ColorPoints = GTSettings.Current.CalibrationSettings.PointColor;
            calibrationControl.PointDiameter = 20;

            calibrationControl.GeneratePositions(calibrationControl.Width, calibrationControl.Height);
            calibrationControl.ShowAllPoints();
		}

        private void CalibrationDurationShow(object sender, MouseButtonEventArgs args)
        {
            GridCalibrationDurations.Visibility = Visibility.Visible;
        }

        private void CalibrationDurationHide(object sender, RoutedEventArgs e)
        {
            GridCalibrationDurations.Visibility = Visibility.Collapsed;
        }

        #region Misc UI stuff

        private void HideWindow(object sender, RoutedEventArgs e)
        {
            //SaveSettings();
            this.Visibility = Visibility.Collapsed;
        }

        private void DragWindow(object sender, MouseButtonEventArgs args)
        {
            try
            {
                hasBeenMoved = true;
                DragMove();
            }
            catch (Exception ex)
            { }
        }

        public bool HasBeenMoved
        {
            get { return hasBeenMoved; }
        }

        #endregion
    }
}