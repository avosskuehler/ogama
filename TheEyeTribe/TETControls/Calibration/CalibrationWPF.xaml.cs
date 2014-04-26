using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using TETCSharpClient.Data;
using HorizontalAlignment = System.Windows.HorizontalAlignment;
using KeyEventArgs = System.Windows.Input.KeyEventArgs;
using MouseEventArgs = System.Windows.Input.MouseEventArgs;


namespace TETControls.Calibration
{
	public partial class CalibrationWpf
	{
		#region Variables & Events

		private const double FADE_IN_TIME = 2.0; //sec
		private const double FADE_OUT_TIME = 0.5; //sec
		private readonly DoubleAnimation animateOut;
		private Screen screen;
        private readonly CalibrationPointWpf calibrationPointWpf;

		#endregion

        #region Events

        public delegate void AbortCalibrationHandler();
        public event AbortCalibrationHandler OnCalibrationAborted;
        public event CalibrationFadeInFormHandler OnFadeInDone;

        #endregion

		#region Constructor

		public CalibrationWpf(Screen current)
		{
			InitializeComponent();
			screen = current;

			// Create the calibration target
			calibrationPointWpf = new CalibrationPointWpf(new Size(screen.Bounds.Width, screen.Bounds.Height));
			CalibrationCanvas.Children.Add(calibrationPointWpf);

			Opacity = 0;
			IsAborted = false;

            // Create the animation-out object and close form when completet
			animateOut = new DoubleAnimation(0, TimeSpan.FromSeconds(FADE_OUT_TIME))
			{
				From = 1.0,
				To = 0.0,
				AutoReverse = false
			};

			animateOut.Completed += delegate
			{
				Close();
			};
		}

        #endregion

		#region Get/Set

        public Size CalibrationAreaSize
	    {
            get { return new Size(ActiveArea.Width, ActiveArea.Height); }
            set
            {
                ActiveArea.Width = value.Width;
                ActiveArea.Height = value.Height;
            }
	    }

		public bool IsAborted { get; set; }

        public Visibility HelpVisbility 
        {
            get { return CalibrationHelp.Visibility; }
            set { CalibrationHelp.Visibility = value; }
        }

	    public HorizontalAlignment AlignmentHorizontal
	    {
	        get { return ActiveArea.HorizontalAlignment; }
            set { ActiveArea.HorizontalAlignment = value; }
	    }
        
	    public VerticalAlignment AlignmentVertical
	    {
	        get { return ActiveArea.VerticalAlignment; }
            set { ActiveArea.VerticalAlignment = value; }
	    }

	    public SolidColorBrush BackgroundColor
	    {
            get { return CalibrationCanvas.Background as SolidColorBrush; }
            set { CalibrationCanvas.Background = value; }
	    }

	    public SolidColorBrush PointColor
	    {
            get { return calibrationPointWpf.PointColor; }
            set { calibrationPointWpf.PointColor = value; }
	    }

        public int PointDisplayTimeMs
        {
            get { return calibrationPointWpf.AnimationTimeMilliseconds; }
            set { calibrationPointWpf.AnimationTimeMilliseconds = value; }
        }

		#endregion

		#region Public methods

		public void DrawCalibrationPoint(Point2D current)
		{
			Dispatcher.BeginInvoke(new Action(() => PlaceCalibrationTarget(current)));
		}

		public void AnimateCalibrationPoint()
		{
			Dispatcher.BeginInvoke(new Action(calibrationPointWpf.StartAnimate));
		}

		public void CloseWindow()
		{
			Dispatcher.BeginInvoke(new Action(() => BeginAnimation(OpacityProperty, animateOut)));
		}

		#endregion

		#region Private methods

        private void WindowContentRendered(object sender, EventArgs e)
		{
            FitToWindow();

            // Adjust for DPI scaling
			ActiveArea.Width = (int)(Utility.Instance.ScaleDpi * (ActiveArea.Width == 0 ? screen.Bounds.Width : ActiveArea.Width));
			ActiveArea.Height = (int)(Utility.Instance.ScaleDpi * (ActiveArea.Height == 0 ? screen.Bounds.Height : ActiveArea.Height));

			System.Windows.Forms.Cursor.Hide();

			// run the fade-in animation and signal when done
			Show();

			var anim = new DoubleAnimation(0, TimeSpan.FromSeconds(FADE_IN_TIME))
			{
				From = 0.0,
				To = 1.0,
				AutoReverse = false
			};

            anim.Completed += delegate
            {
                // remove the info message and notify the calibration runner that we are ready to start
                CalibrationMessage.Visibility = Visibility.Hidden;

                if (OnFadeInDone != null)
                    OnFadeInDone(this, new CalibrationFadeInArgs(true));
            };

			BeginAnimation(OpacityProperty, anim);
		}

		private void FitToWindow()
		{
			Width = screen.Bounds.Width * Utility.Instance.ScaleDpi;
			Height = screen.Bounds.Height * Utility.Instance.ScaleDpi;
			Top = screen.Bounds.Y * Utility.Instance.ScaleDpi;
            Left = screen.Bounds.X * Utility.Instance.ScaleDpi;
		}

        private void PlaceCalibrationTarget(Point2D current)
        {
			var x = (float)Math.Round(Utility.Instance.ScaleDpi * current.X, 0);
			var y = (float)Math.Round(Utility.Instance.ScaleDpi * current.Y, 0);
            Canvas.SetLeft(CalibrationCanvas.Children[0], x);
            Canvas.SetTop(CalibrationCanvas.Children[0], y);
        }

		private void WindowClosed(object sender, EventArgs e)
		{
			System.Windows.Forms.Cursor.Show();
		}

		private void MouseDbClick(object sender, MouseEventArgs e)
		{
			Abort();
		}

		private void KeyUpDetected(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Escape)
				Abort();
		}

		private void Abort()
		{
			IsAborted = true;

			if (OnCalibrationAborted != null)
				OnCalibrationAborted();
		}

		#endregion
    }
}
