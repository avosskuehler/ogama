using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using GTCommons.Enum;
using GTSettings;

namespace GTApplication.SettingsUI
{

    public partial class ComponentPlacingWindow : Window 
    {
        #region Variables

        private bool hasBeenMoved = false;
        public int numberOfIR = 0;
        private CameraIconUC cameraIcon;
        private int cameraIconWidth = 45;
        private int cameraIconHeight = 35;
        private double cameraIconLeft = 190;
        private double cameraIconTop = 210;
        private int irUCSize = 40;

        private Point irBelowLeft;
        private Point irBelowRight;
        private Point irAboveLeft;
        private Point irAboveRight;

        #endregion

        #region Constructor

        public ComponentPlacingWindow() 
        {
            InitializeComponent();
            Init();
        }

        #endregion


        #region Init

        private void Init()
        {
            dragCanvas.IsCalibrationCanvas = false; // enable left mouse drag/drop

            cameraIconLeft = dragCanvas.Width/2 - cameraIconWidth/2;
            cameraIconTop = dragCanvas.Height - cameraIconHeight*1.5;

            irBelowLeft = new Point(70, 205);
            irAboveLeft = new Point(70, 15);
            irBelowRight = new Point(320, 205);  
            irAboveRight = new Point(320, 15);

            comboBoxNumberIR.Items.Add(1);
            comboBoxNumberIR.Items.Add(2);
            comboBoxNumberIR.SelectionChanged += new SelectionChangedEventHandler(NumberIRChanged);

            AddCameraIcon(cameraIconTop, cameraIconLeft);
        }

        #endregion


        #region OnUIEvents

        private void NumberIRChanged(object sender, SelectionChangedEventArgs e)
        {
            labelSelectIR.Visibility = Visibility.Collapsed;
            labelDrag.Visibility = Visibility.Visible;

            ClearDragCanvas();

            numberOfIR = (int) comboBoxNumberIR.SelectedValue;

            if(numberOfIR >= 1)
            {
                IlluminationEllipseUC ir = new IlluminationEllipseUC(irUCSize,irUCSize);
                dragCanvas.Children.Add(ir);
                Canvas.SetTop(ir, irBelowLeft.Y);
                Canvas.SetLeft(ir, irBelowLeft.X);
            }

            if(numberOfIR >= 2)
            {
                IlluminationEllipseUC ir = new IlluminationEllipseUC(irUCSize,irUCSize);
                dragCanvas.Children.Add(ir);
                Canvas.SetTop(ir, irBelowRight.Y);
                Canvas.SetLeft(ir, irBelowRight.X);
            }
        }


        private void Save(object sender, RoutedEventArgs e)
        {
            GTSettings.Settings.Instance.Processing.NumberOfGlints = numberOfIR;

            bool isBelow = false;
            bool isAbove = false;

            foreach(UIElement ui in dragCanvas.Children)
            {
                if(ui.GetType() == typeof(IlluminationEllipseUC))
                {
                    double top = Canvas.GetTop(ui);
					double center = top + ui.RenderSize.Height / 2;

					if (center > GridDisplay.Margin.Top + GridDisplay.RenderSize.Height)
						isBelow = true;
					else if (center < GridDisplay.Margin.Top)
						isAbove = true;
                }
            }

			if (isAbove && isBelow)
				Settings.Instance.Processing.IRPlacement = IRPlacementEnum.BelowAndAbove;
			else if (isAbove)
                Settings.Instance.Processing.IRPlacement = IRPlacementEnum.Above;
			else if (isBelow)
                Settings.Instance.Processing.IRPlacement = IRPlacementEnum.Below;
			else
                Settings.Instance.Processing.IRPlacement = IRPlacementEnum.None;


            this.Close();

        }

        #endregion


        #region Misc helper methods

        private void AddCameraIcon(double top, double left) 
        {
            cameraIcon = new CameraIconUC();
            cameraIcon.Width = 45;
            cameraIcon.Height = 35;

            dragCanvas.Children.Add(cameraIcon);
            Canvas.SetTop(cameraIcon, top);
            Canvas.SetLeft(cameraIcon, left);
        }

        private void ClearDragCanvas()
        {
            cameraIconTop = Canvas.GetTop(cameraIcon);
            cameraIconLeft = Canvas.GetLeft(cameraIcon);

            dragCanvas.Children.Clear();
            AddCameraIcon(cameraIconTop, cameraIconLeft);
        }


        #endregion


        #region Misc UI stuff

        public bool HasBeenMoved
        {
            get { return hasBeenMoved; }
        }

        private void HideWindow(object sender, RoutedEventArgs e)
        {
            Visibility = Visibility.Collapsed;
        }

        private void DragWindow(object sender, MouseButtonEventArgs args)
        {
            try
            {
                hasBeenMoved = true;
                DragMove();
            }
            catch (Exception)
            {
            }
        }

        #endregion
    }
}
