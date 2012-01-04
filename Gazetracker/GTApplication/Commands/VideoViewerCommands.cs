using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace GazeTrackerUI.Commands
{
    public class VideoViewerCommands : Window
    {
        public static readonly RoutedEvent DetachEvent = EventManager.RegisterRoutedEvent("DetachEvent", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(VideoViewerCommands));
        public static readonly RoutedEvent CloseEvent = EventManager.RegisterRoutedEvent("CloseEvent", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(VideoViewerCommands));

        public VideoViewerCommands()
        {
        }


        public void Detach()
        {
            RoutedEventArgs args1 = new RoutedEventArgs();
            args1 = new RoutedEventArgs(DetachEvent, this);
            RaiseEvent(args1);
        }

        public void CloseViewer()
        {
            RoutedEventArgs args1 = new RoutedEventArgs();
            args1 = new RoutedEventArgs(CloseEvent, this);
            RaiseEvent(args1);
        }


        public event RoutedEventHandler OnDetach
        {
            add { base.AddHandler(DetachEvent, value); }
            remove { base.RemoveHandler(DetachEvent, value); }
        }

        public event RoutedEventHandler OnClose
        {
            add { base.AddHandler(CloseEvent, value); }
            remove { base.RemoveHandler(CloseEvent, value); }
        }

    }
}
