using System.Windows;

namespace GTCommons.Commands
{
    public class SettingsCommands : Window
    {
        public static readonly RoutedEvent SettingsEvent = EventManager.RegisterRoutedEvent("SettingsEvent",RoutingStrategy.Bubble, typeof (RoutedEventHandler), typeof (SettingsCommands));
        public static readonly RoutedEvent AutotuneSettingsEvent = EventManager.RegisterRoutedEvent("AutotuneSettingsEvent", RoutingStrategy.Bubble, typeof (RoutedEventHandler), typeof (SettingsCommands));
        public static readonly RoutedEvent CameraSettingsEvent = EventManager.RegisterRoutedEvent("CameraSettingsEvent", RoutingStrategy.Bubble, typeof (RoutedEventHandler), typeof (SettingsCommands));
        public static readonly RoutedEvent VideoDetachEvent = EventManager.RegisterRoutedEvent("VideoDetachEvent", RoutingStrategy.Bubble, typeof (RoutedEventHandler), typeof (SettingsCommands));
        public static readonly RoutedEvent NetworkClientEvent = EventManager.RegisterRoutedEvent("NetworkClientEvent", RoutingStrategy.Bubble, typeof (RoutedEventHandler),typeof (SettingsCommands));
        public static readonly RoutedEvent TrackQualityEvent = EventManager.RegisterRoutedEvent("TrackStatsEvent", RoutingStrategy.Bubble, typeof (RoutedEventHandler),typeof (SettingsCommands));
        public static readonly RoutedEvent VisualizationSettingsEvent = EventManager.RegisterRoutedEvent("VisualizationSettingsEvent", RoutingStrategy.Bubble, typeof (RoutedEventHandler), typeof (SettingsCommands));
        public static readonly RoutedEvent HeadtrackerSettingsEvent = EventManager.RegisterRoutedEvent("HeadtrackerSettingsEvent", RoutingStrategy.Bubble, typeof (RoutedEventHandler), typeof (SettingsCommands));
        public static readonly RoutedEvent EyetrackerSettingsEvent = EventManager.RegisterRoutedEvent("EyetrackerSettingsEvent", RoutingStrategy.Bubble, typeof (RoutedEventHandler), typeof (SettingsCommands));

        #region Public method for executing / rasining events

        public void Settings()
        {
            var args1 = new RoutedEventArgs();
            args1 = new RoutedEventArgs(SettingsEvent, this);
            RaiseEvent(args1);
        }

        public void AutotuneSettings()
        {
            var args1 = new RoutedEventArgs();
            args1 = new RoutedEventArgs(AutotuneSettingsEvent, this);
            RaiseEvent(args1);
        }

        public void CameraSettings()
        {
            var args1 = new RoutedEventArgs();
            args1 = new RoutedEventArgs(CameraSettingsEvent, this);
            RaiseEvent(args1);
        }

        public void VisualizationSettings()
        {
            var args1 = new RoutedEventArgs();
            args1 = new RoutedEventArgs(VisualizationSettingsEvent, this);
            RaiseEvent(args1);
        }

        public void HeadtrackerSettings()
        {
            var args1 = new RoutedEventArgs();
            args1 = new RoutedEventArgs(HeadtrackerSettingsEvent, this);
            RaiseEvent(args1);
        }

        public void EyetrackerSettings()
        {
            var args1 = new RoutedEventArgs();
            args1 = new RoutedEventArgs(EyetrackerSettingsEvent, this);
            RaiseEvent(args1);
        }

        #endregion

        #region RoutedEventsHandlers

        public event RoutedEventHandler OnSettings
        {
            add { base.AddHandler(SettingsEvent, value); }
            remove { base.RemoveHandler(SettingsEvent, value); }
        }

        public event RoutedEventHandler OnAutoTuneSettings
        {
            add { base.AddHandler(AutotuneSettingsEvent, value); }
            remove { base.RemoveHandler(AutotuneSettingsEvent, value); }
        }

        public event RoutedEventHandler OnCameraSettings
        {
            add { base.AddHandler(CameraSettingsEvent, value); }
            remove { base.RemoveHandler(CameraSettingsEvent, value); }
        }

        public event RoutedEventHandler OnVisualizationSettings
        {
            add { base.AddHandler(VisualizationSettingsEvent, value); }
            remove { base.RemoveHandler(VisualizationSettingsEvent, value); }
        }

        public event RoutedEventHandler OnHeadtrackerSettings
        {
            add { base.AddHandler(HeadtrackerSettingsEvent, value); }
            remove { base.RemoveHandler(HeadtrackerSettingsEvent, value); }
        }

        public event RoutedEventHandler OnEyetrackerSettings
        {
            add { base.AddHandler(EyetrackerSettingsEvent, value); }
            remove { base.RemoveHandler(EyetrackerSettingsEvent, value); }
        }

        #endregion
    }
}
