using System;
using System.Windows;
using GTLibrary.Logging;
using UserControl=System.Windows.Controls.UserControl;

namespace GTApplication.CalibrationUI
{
    #region Using

    

    #endregion

    public partial class CalibrationMenuUC : UserControl
    {

        #region Events

        public static readonly RoutedEvent RecalibrateEvent = EventManager.RegisterRoutedEvent("RecalibrateEvent",RoutingStrategy.Bubble, typeof (RoutedEventHandler), typeof (CalibrationMenuUC));
        public static readonly RoutedEvent AcceptEvent = EventManager.RegisterRoutedEvent("AcceptEvent", RoutingStrategy.Bubble, typeof (RoutedEventHandler), typeof (CalibrationMenuUC));
        public static readonly RoutedEvent ShareEvent = EventManager.RegisterRoutedEvent("ShareEvent", RoutingStrategy.Bubble, typeof (RoutedEventHandler), typeof (CalibrationMenuUC));
        public static readonly RoutedEvent ToggleSmoothingEvent = EventManager.RegisterRoutedEvent("ToggleSmoothingEvent", RoutingStrategy.Bubble, typeof (RoutedEventHandler), typeof (CalibrationMenuUC));
        public static readonly RoutedEvent ToggleCrosshairEvent = EventManager.RegisterRoutedEvent("ToggleCrosshairEvent", RoutingStrategy.Bubble, typeof (RoutedEventHandler), typeof (CalibrationMenuUC));
        public static readonly RoutedEvent AccuracyParamChangeEvent = EventManager.RegisterRoutedEvent("AccuracyParamChangeEvent", RoutingStrategy.Bubble, typeof (RoutedEventHandler), typeof (CalibrationMenuUC));

        #endregion


        #region Constructor

        public CalibrationMenuUC()
        {
            InitializeComponent();

            CheckBoxSmooth.Checked += ToggleSmoothing;
            CheckBoxSmooth.Unchecked += ToggleSmoothing;

            CheckBoxVisualFeedback.Checked += CrosshairToggle;
            CheckBoxVisualFeedback.Unchecked += CrosshairToggle;

            LabelAccuracy.MouseDown += new System.Windows.Input.MouseButtonEventHandler(LabelAccuracy_MouseDown);
            TextBoxDistanceFromScreen.KeyDown += new System.Windows.Input.KeyEventHandler(TextBoxDistanceFromScreen_KeyDown);
        }


        #endregion


        #region Eventhandlers

        public event RoutedEventHandler OnRecalibrate
        {
            add { base.AddHandler(RecalibrateEvent, value); }
            remove { base.RemoveHandler(RecalibrateEvent, value); }
        }

        public event RoutedEventHandler OnAccept
        {
            add { base.AddHandler(AcceptEvent, value); }
            remove { base.RemoveHandler(AcceptEvent, value); }
        }

        public event RoutedEventHandler OnShare
        {
            add { base.AddHandler(ShareEvent, value); }
            remove { base.RemoveHandler(ShareEvent, value); }
        }

        public event RoutedEventHandler OnToggleSmoothing
        {
            add { base.AddHandler(ToggleSmoothingEvent, value); }
            remove { base.RemoveHandler(ToggleSmoothingEvent, value); }
        }

        public event RoutedEventHandler OnToggleCrosshair
        {
            add { base.AddHandler(ToggleCrosshairEvent, value); }
            remove { base.RemoveHandler(ToggleCrosshairEvent, value); }
        }

        public event RoutedEventHandler OnAccuracyParamsChange
        {
            add { base.AddHandler(AccuracyParamChangeEvent, value); }
            remove { base.RemoveHandler(AccuracyParamChangeEvent, value); }
        }

        #endregion

        #region Get/Set

        public int DistanceFromScreen
        {
            get { return Convert.ToInt32(TextBoxDistanceFromScreen.Text);}
            set { TextBoxDistanceFromScreen.Text = value.ToString(); }
        }

        #endregion


        #region Public methods

        public void GenerateQualityIndicator(double accuracy)
        {
			int stars = 0;

			if (accuracy < 0.5)
				stars = 5;
			else if (accuracy > 0.5 && accuracy < 1)
				stars = 4;
			else if (accuracy > 1 && accuracy < 1.5)
				stars = 3;
			else if (accuracy > 1.5 && accuracy < 2)
				stars = 2;
			else if (accuracy > 2)
				stars = 1;

			try
			{
				ratingCalibrationQuality.RatingValue = stars;
			}
			catch (Exception ex)
			{
				ErrorLogger.ProcessException(ex, false);
			}
		}

        public void SetAccuracy(double left, double right)
        {
          if (right != 0)
            LabelAccuracyValues.Content = "left " + left.ToString().Substring(0, 3) + " right " + right.ToString().Substring(0, 3);
          else if (left != 0)
            LabelAccuracyValues.Content = left.ToString().Substring(0, 3);
          else
            LabelAccuracyValues.Content = "not available";
        }


        #endregion


        #region Private methods (on menu clicks)

        private void AcceptCalibration(object sender, RoutedEventArgs e)
        {
            RoutedEventArgs args1 = new RoutedEventArgs();
            args1 = new RoutedEventArgs(AcceptEvent, new RoutedEventArgs());
            RaiseEvent(args1);
        }

        private void RedoCalibration(object sender, RoutedEventArgs e)
        {
            RoutedEventArgs args1 = new RoutedEventArgs();
            args1 = new RoutedEventArgs(RecalibrateEvent, new RoutedEventArgs());
            RaiseEvent(args1);
        }

        private void ShareData(object sender, RoutedEventArgs e)
        {
            RoutedEventArgs args1 = new RoutedEventArgs();
            args1 = new RoutedEventArgs(ShareEvent, new RoutedEventArgs());
            RaiseEvent(args1);
        }

        private void CrosshairToggle(object sender, RoutedEventArgs e)
        {
            RoutedEventArgs args1 = new RoutedEventArgs();
            args1 = new RoutedEventArgs(ToggleCrosshairEvent, new RoutedEventArgs());
            RaiseEvent(args1);
        }

        private void ToggleSmoothing(object sender, RoutedEventArgs e)
        {
            RoutedEventArgs args1 = new RoutedEventArgs();
            args1 = new RoutedEventArgs(ToggleSmoothingEvent, new RoutedEventArgs());
            RaiseEvent(args1);
        }

        private void LabelAccuracy_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e) 
        {
            GridAccuracyParams.Visibility = Visibility.Visible;
        }

        private void CloseAccuracyParamGrid(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            GridAccuracyParams.Visibility = Visibility.Collapsed;
        }

        private void TextBoxDistanceFromScreen_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            switch (e.Key)
            {
                case System.Windows.Input.Key.Enter:
                    AccuracyParamSet(null, null);
                    break;
                case System.Windows.Input.Key.Escape:
                    GridAccuracyParams.Visibility = Visibility.Collapsed;
                    break;
            }
        }

        private void AccuracyParamSet(object sender, RoutedEventArgs e)
        {
            RoutedEventArgs args1 = new RoutedEventArgs();
            args1 = new RoutedEventArgs(AccuracyParamChangeEvent, new RoutedEventArgs());
            RaiseEvent(args1);
            GridAccuracyParams.Visibility = Visibility.Collapsed;
        }

        #endregion

    }
}