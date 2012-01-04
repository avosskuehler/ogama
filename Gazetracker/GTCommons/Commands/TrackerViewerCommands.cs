using System.Windows;

namespace GTCommons.Commands
{
    public class TrackerViewerCommands : Window
    {
        public static readonly RoutedEvent VideoDetachEvent = EventManager.RegisterRoutedEvent("DetachEvent", RoutingStrategy.Bubble, typeof ( RoutedEventHandler), typeof ( TrackerViewerCommands));
        public static readonly RoutedEvent VideoCloseEvent = EventManager.RegisterRoutedEvent("CloseEvent",RoutingStrategy.Bubble, typeof (RoutedEventHandler), typeof ( TrackerViewerCommands));
        public static readonly RoutedEvent TrackBoxShowEvent = EventManager.RegisterRoutedEvent("TrackBoxShowEvent", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof( TrackerViewerCommands));
        public static readonly RoutedEvent TrackBoxHideEvent = EventManager.RegisterRoutedEvent("TrackBoxHideEvent", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(TrackerViewerCommands));


        public void VideoDetach()
        {
            var args1 = new RoutedEventArgs();
            args1 = new RoutedEventArgs(VideoDetachEvent, this);
            RaiseEvent(args1);
        }

        public void VideoCloseViewer()
        {
            var args1 = new RoutedEventArgs();
            args1 = new RoutedEventArgs(VideoCloseEvent, this);
            RaiseEvent(args1);
        }

        public void TrackBoxShow()
        {
            var args1 = new RoutedEventArgs();
            args1 = new RoutedEventArgs(TrackBoxShowEvent, this);
            RaiseEvent(args1);
        }

        public void TrackBoxHide()
        {
            var args1 = new RoutedEventArgs();
            args1 = new RoutedEventArgs(TrackBoxHideEvent, this);
            RaiseEvent(args1);
        }


        public event RoutedEventHandler OnVideoDetach
        {
            add { base.AddHandler(VideoDetachEvent, value); }
            remove { base.RemoveHandler(VideoDetachEvent, value); }
        }

        public event RoutedEventHandler OnVideoClose
        {
            add { base.AddHandler(VideoCloseEvent, value); }
            remove { base.RemoveHandler(VideoCloseEvent, value); }
        }

        public event RoutedEventHandler OnTrackBoxShow
        {
            add { base.AddHandler(TrackBoxShowEvent, value); }
            remove { base.RemoveHandler(TrackBoxShowEvent, value); }
        }

        public event RoutedEventHandler OnTrackBoxHide
        {
            add { base.AddHandler(TrackBoxHideEvent, value); }
            remove { base.RemoveHandler(TrackBoxHideEvent, value); }
        }
    }
}