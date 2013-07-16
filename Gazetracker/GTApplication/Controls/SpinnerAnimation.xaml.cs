using System;
using System.IO;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GazeTrackerUI.Controls
{
	public partial class SpinnerAnimation
	{

        // Adapted from http://social.msdn.microsoft.com/Forums/en-US/wpf/thread/0875ebf8-bb77-45ea-a929-d40743a3bf03/
        // Credits go to jrboddie @ http://dotupdate.wordpress.com/ 

		public SpinnerAnimation()
		{
			this.InitializeComponent();
            Start();

		}

        public void Start()
        {
            drawCanvas();
            canvas2.Visibility = Visibility.Visible;
            DoubleAnimation a = new DoubleAnimation();
            a.From = 0;
            a.To = 360;
            a.RepeatBehavior = RepeatBehavior.Forever;
            a.SpeedRatio = 1;
            spin.BeginAnimation(RotateTransform.AngleProperty, a);
        }

        public void Stop()
        {
            canvas2.Visibility = Visibility.Collapsed;
        }


        //Spinner draw

        void drawCanvas()
        {

            for (int i = 0; i < 12; i++)
            {
                Line line = new Line()
                {
                    X1 = 25,
                    X2 = 25,
                    Y1 = 0,
                    Y2 = 10,

                    StrokeThickness = 5,
                    Stroke = Brushes.Gray,
                    Width = 50,
                    Height = 50
                };

                line.VerticalAlignment = VerticalAlignment.Center;
                line.HorizontalAlignment = HorizontalAlignment.Center;
                line.RenderTransformOrigin = new Point(.5, .5);
                line.RenderTransform = new RotateTransform(i * 30);
                line.Opacity = (double)i / 12;
                canvas1.Children.Add(line);
            }

        }




	}
}