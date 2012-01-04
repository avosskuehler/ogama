using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace GTApplication.Tools
{
    public partial class CrosshairDriver : Window
    {
        public CrosshairDriver()
        {
            InitializeComponent();
            CanvasRoot.IsHitTestVisible = false;
            Left = 0;
            Top = 0;
            Topmost = true;
        }

        public void Move(Point p)
        {
            // On new data move the cross-hair
            CanvasRoot.Dispatcher.BeginInvoke
                (
                    DispatcherPriority.Normal,
                    new Action
                        (
                        delegate
                            {
								if (p.X <= 0)
									Canvas.SetLeft(visualGazePoint, 0 - visualGazePoint.ActualWidth / 2);
								else
									Canvas.SetLeft(visualGazePoint, p.X - visualGazePoint.ActualWidth / 2);
									
								if(p.Y <= 0)
									Canvas.SetTop(visualGazePoint, 0 - visualGazePoint.ActualHeight / 2);
								else
									Canvas.SetTop(visualGazePoint, p.Y - visualGazePoint.ActualHeight / 2);
                            }
                        )
                );
        }

        public void Move(double x, double y)
        {
            // On new data move the cross-hair
            CanvasRoot.Dispatcher.BeginInvoke
                (
                    DispatcherPriority.ApplicationIdle,
                    new Action
                        (
                        delegate
                            {
								if (x <= 0)
									Canvas.SetLeft(visualGazePoint, 0 - visualGazePoint.ActualWidth / 2);
								else
									Canvas.SetLeft(visualGazePoint, x - visualGazePoint.ActualWidth / 2);

								if (y <= 0)
									Canvas.SetTop(visualGazePoint, 0 - visualGazePoint.ActualHeight / 2);
								else
									Canvas.SetTop(visualGazePoint, y - visualGazePoint.ActualHeight / 2);
							}
                        )
                );
        }
    }
}