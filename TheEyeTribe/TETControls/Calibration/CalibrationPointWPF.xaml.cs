using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace TETControls.Calibration
{
    public partial class CalibrationPointWpf
    {
        #region Variables

        private Storyboard _sb;
		private const double PctTarget = 0.07;
        private int animationTimeMs = 1000;
        private int animationDurationMs;
        private int pulseCount = 1;
        private double minScale = 0.5;
        private double maxScale = 1;
        private int currentPulseCount;

        #endregion

        #region Events

        public delegate void AnimationDoneHandler();
        public event AnimationDoneHandler OnAnimationDone;

        #endregion

        #region Get/Set

        public SolidColorBrush PointColor { get; set; }

        public int AnimationTimeMilliseconds
        {
            get { return animationTimeMs; } 

            set 
            { 
                animationTimeMs = value;

                // due to autoreverse - divide with PulseCount and half the animation time
                animationDurationMs = animationTimeMs / 2 / pulseCount;
            }
        }

        #endregion

        #region Constructor

		public CalibrationPointWpf(Size screenSize)
        {
            InitializeComponent();
            AnimationTimeMilliseconds = 1000; // set default
			Loaded += (s, e) => SetupCalibrationPoint(screenSize);
        }

        #endregion

        #region Public methods

        public void StartAnimate()
        {
            currentPulseCount = 0;
            _sb.Begin();
        }

        #endregion

        #region Private methods

		private void SetupCalibrationPoint(Size screenSize)
        {
            TargetCenter.Fill = PointColor;

			// set the size of the calibration target
			var monitorSize = screenSize;
			var targetSize = monitorSize.Height * PctTarget;
			var scaleToFit = targetSize / TargetCenter.Width;
			Target.Margin = new Thickness(-targetSize / 2, -targetSize / 2, 0.0, 0.0);

			var trScl = new ScaleTransform(scaleToFit, scaleToFit);
			var trGrp = new TransformGroup();
			trGrp.Children.Add(trScl);
			Target.RenderTransform = trGrp;

			// setup the storyboard for the animation part
            _sb = new Storyboard();
            var myScaleTransform = new ScaleTransform ();
            var myTransformGroup = new TransformGroup();
            myTransformGroup.Children.Add(myScaleTransform);
            TargetCenter.RenderTransform = myTransformGroup;

            var scaleAnimationX = new DoubleAnimation(
                maxScale,
                minScale,
                new Duration(TimeSpan.FromMilliseconds(animationDurationMs)))
					{AutoReverse = true};

			Storyboard.SetTarget(scaleAnimationX, TargetCenter);
			Storyboard.SetTargetProperty(scaleAnimationX, new PropertyPath("RenderTransform.Children[0].(ScaleTransform.ScaleX)"));

            var scaleAnimationY = new DoubleAnimation(
                  maxScale,
                  minScale,
                  new Duration(TimeSpan.FromMilliseconds(animationDurationMs)))
                    {AutoReverse = true};

            Storyboard.SetTarget(scaleAnimationY, TargetCenter);
            Storyboard.SetTargetProperty(scaleAnimationY, new PropertyPath("RenderTransform.Children[0].(ScaleTransform.ScaleY)"));

			_sb.Children.Add(scaleAnimationX);
			_sb.Children.Add(scaleAnimationY);

			_sb.Completed += (s, e) =>
			{
			    _sb.Stop();
			    AnimationCompleted();
			};
        }

        private void AnimationCompleted()
        {
            currentPulseCount++;

			if (currentPulseCount < pulseCount)
			{
				_sb.Begin();
			}
			else
			{
				if (OnAnimationDone != null)
					OnAnimationDone();
			}
        }

        #endregion
    }
}

