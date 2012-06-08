using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace GazeTrackerUI.Commands
{
    public class CameraCommands : Window
    {
        public static readonly RoutedEvent CameraChangedEvent = EventManager.RegisterRoutedEvent("CameraChangedEvent", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(CameraCommands));

        public CameraCommands()
        {
        }

        public void CameraChange()
        {
            RoutedEventArgs args1 = new RoutedEventArgs();
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
