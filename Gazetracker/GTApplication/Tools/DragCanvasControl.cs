using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace GTApplication.Tools
{
  using GTApplication.CalibrationUI;

  #region Includes

    

    #endregion

    public class DragCanvasControl : Canvas
    {
        public static readonly RoutedEvent UIElementMovedEvent = EventManager.RegisterRoutedEvent("CalibrationPointMovedEvent", RoutingStrategy.Bubble, typeof (RoutedEventHandler), typeof (DragCanvasControl));

        private bool isDragging;
        private bool isCalibrationCanvas = true; // switches mouse button and disables events (if used outside calibration control)
        private UIElement selectedElement;
        private Point selectedElementOrigins;
        private Point startPoint;

        static DragCanvasControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof (DragCanvasControl), new FrameworkPropertyMetadata(typeof (DragCanvasControl)));
        }


        public event RoutedEventHandler OnUIElementMoved
        {
            add { base.AddHandler(UIElementMovedEvent, value); }
            remove { base.RemoveHandler(UIElementMovedEvent, value); }
        }

        protected override void OnPreviewMouseRightButtonDown(MouseButtonEventArgs e)
        {
            base.OnPreviewMouseLeftButtonDown(e);

            if (e.Source == this) return;
            if (!isDragging)
            {
                startPoint = e.GetPosition(this);
                selectedElement = e.Source as UIElement;
                CaptureMouse();

                isDragging = true;

                selectedElementOrigins = new Point(GetLeft(selectedElement), GetTop(selectedElement));
            }
            e.Handled = true;
        }

        protected override void OnPreviewMouseRightButtonUp(MouseButtonEventArgs e)
        {
            base.OnPreviewMouseLeftButtonUp(e);

            if (!IsMouseCaptured) return;
            isDragging = false;
            ReleaseMouseCapture();

            if (selectedElement is CalibrationPoint)
            {
                var calPoint = selectedElement as CalibrationPoint;
                calPoint.IsRepositioned = true;
                calPoint.Point = new Point(e.GetPosition(this).X - calPoint.ActualWidth/2,
                                           e.GetPosition(this).Y + calPoint.ActualHeight/2);

                var args1 = new RoutedEventArgs();
                args1 = new RoutedEventArgs(UIElementMovedEvent, calPoint);
                RaiseEvent(args1);
            }

            e.Handled = true;
        }

        protected override void OnPreviewMouseMove(MouseEventArgs e)
        {
            base.OnPreviewMouseMove(e);

            if (!IsMouseCaptured) return;
            if (isDragging)
            {
                Point InstancePosition = e.GetPosition(this);

                double elementLeft = (InstancePosition.X - startPoint.X) + selectedElementOrigins.X;
                double elementTop = (InstancePosition.Y - startPoint.Y) + selectedElementOrigins.Y;

                SetLeft(selectedElement, elementLeft);
                SetTop(selectedElement, elementTop);
            }
        }


        
        protected override void OnPreviewMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            // Enables left button drag/drop (used outside of the calibration control)
            if(isCalibrationCanvas == false)
                OnPreviewMouseRightButtonDown(e);
        }

        protected override void OnPreviewMouseLeftButtonUp(MouseButtonEventArgs e)
        {
             // Enables left button drag/drop (used outside of the calibration control)
             if(isCalibrationCanvas == false)
                OnPreviewMouseRightButtonUp(e);
        }


        public bool IsCalibrationCanvas
        {
            get { return isCalibrationCanvas; }
            set { isCalibrationCanvas = value; }
        }
    }
}