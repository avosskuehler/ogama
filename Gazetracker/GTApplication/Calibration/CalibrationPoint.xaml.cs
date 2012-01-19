using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;

namespace GTApplication.CalibrationUI
{
    /// <summary>
    /// Interaction logic for CalibrationPoint.xaml
    /// </summary>
    /// 
    public partial class CalibrationPoint : UserControl
    {
        #region Variables

        private double _diameter = -1;
        private double _duration = 1;
        private Point _mousePositionOnClick = new Point(0, 0);
        private int _number = -1;
        private SolidColorBrush _pointColor = new SolidColorBrush(Colors.White);
        private Storyboard scalePointSB;
        private Storyboard scalePointSBReverese;

        #endregion

        #region Events

        public static readonly RoutedEvent PointDisplayedEvent = EventManager.RegisterRoutedEvent(
            "PointDisplayedEvent", RoutingStrategy.Bubble, typeof (RoutedEventHandler), typeof (CalibrationPoint));

        public static readonly RoutedEvent PointClickEvent = EventManager.RegisterRoutedEvent("PointClickEvent",
                                                                                              RoutingStrategy.Bubble,
                                                                                              typeof (RoutedEventHandler
                                                                                                  ),
                                                                                              typeof (CalibrationPoint));

        #endregion

        #region Constructor

        public CalibrationPoint()
        {
            InitializeComponent();
            SetUpAnimations();
        }

        public CalibrationPoint(int pNumber, Point position, double diameter, SolidColorBrush pColor, double duration)
        {
            InitializeComponent();
            SetUpAnimations();

            Number = pNumber;
            Point = position;
            Diameter = diameter;
            PointColor = pColor;
            PointDuration = duration;
        }

        #endregion

        #region Event handlers

        public event RoutedEventHandler OnPointDisplayed
        {
            add { base.AddHandler(PointDisplayedEvent, value); }
            remove { base.RemoveHandler(PointDisplayedEvent, value); }
        }

        public event RoutedEventHandler OnClick
        {
            add { base.AddHandler(PointClickEvent, value); }
            remove { base.RemoveHandler(PointClickEvent, value); }
        }

        #endregion

        #region Public methods

        public void SetUpAnimations()
        {
            scalePointSB = FindResource("ScaleCalibrationPoint") as Storyboard;
            scalePointSBReverese = FindResource("ScaleCalibrationPointReverse") as Storyboard;
            if (scalePointSB != null) scalePointSB.Completed += scalePointSB_Completed;
        }

        public void RunScalePointAnimation()
        {
            scalePointSB.Begin(this);
        }

        public void RunScalePointAnimationReverse()
        {
            scalePointSBReverese.Begin(this);
        }

        #endregion

        #region Private metods

        private void scalePointSB_Completed(object sender, EventArgs e)
        {
            IsDisplayed = true;

            var args1 = new RoutedEventArgs();
            args1 = new RoutedEventArgs(PointDisplayedEvent, this);
            RaiseEvent(args1);
        }

        private void OnMouseRightDown(object sender, MouseButtonEventArgs e)
        {
            var args1 = new RoutedEventArgs();
            args1 = new RoutedEventArgs(PointClickEvent, this);
            RaiseEvent(args1);
        }

        #endregion

        #region Get/Set

        public static readonly DependencyProperty PointProperty =
            DependencyProperty.Register(
                "Point",
                typeof (Point),
                typeof (CalibrationPoint),
                new UIPropertyMetadata(null));

        public bool IsDisplayed { get; set; }

        public Point MousePositionOnClick
        {
            get { return _mousePositionOnClick; }
            set { _mousePositionOnClick = value; }
        }

        public Point Point
        {
            get { return (Point) GetValue(PointProperty); }
            set { SetValue(PointProperty, value); }
        }


        public SolidColorBrush PointColor
        {
            get { return _pointColor; }
            set
            {
                _pointColor = value;
                Target.Fill = _pointColor;
            }
        }

        public int Number
        {
            get { return _number; }
            set { _number = value; }
        }

        public double Diameter
        {
            get { return _diameter; }
            set
            {
                _diameter = value;
                Target.Width = value;
                Target.Height = value;
            }
        }

        public double PointDuration
        {
            get { return _duration; }
            set
            {
                _duration = value;
                scalePointSB.Duration = TimeSpan.FromMilliseconds(_duration);
                scalePointSBReverese.Duration = TimeSpan.FromMilliseconds(_duration);
            }
        }

        public bool IsRepositioned { get; set; }

        public void PointImage(BitmapImage img)
        {
            var myImageBrush = new ImageBrush(img);
            Target.Fill = myImageBrush;
        }

        #endregion
    }
}