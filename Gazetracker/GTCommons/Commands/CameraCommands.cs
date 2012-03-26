using System.Windows;

namespace GTCommons.Commands
{
    public class CameraCommands : Window
    {
        public static readonly RoutedEvent CameraChangedEvent = EventManager.RegisterRoutedEvent("CameraChangedEvent",RoutingStrategy.Bubble,typeof (RoutedEventHandler),typeof (CameraCommands));

        public void CameraChange()
        {
            var args1 = new RoutedEventArgs();
            args1 = new RoutedEventArgs(CameraChangedEvent, this);
            RaiseEvent(args1);
        }

        public event RoutedEventHandler OnCameraChange
        {
            add { base.AddHandler(CameraChangedEvent, value); }
            remove { base.RemoveHandler(CameraChangedEvent, value); }
        }
    }
}