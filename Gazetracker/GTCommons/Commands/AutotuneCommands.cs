using System.Windows;

namespace GTCommons.Commands
{
    public class AutotuneCommands : Window
    {
        public static readonly RoutedEvent AutotuneEvent = EventManager.RegisterRoutedEvent("AutotuneEvent",
                                                                                            RoutingStrategy.Bubble,
                                                                                            typeof (RoutedEventHandler),
                                                                                            typeof (AutotuneCommands));

        public void Autotune()
        {
            var args1 = new RoutedEventArgs();
            args1 = new RoutedEventArgs(AutotuneEvent, this);
            RaiseEvent(args1);
        }

        public event RoutedEventHandler OnAutoTune
        {
            add { base.AddHandler(AutotuneEvent, value); }
            remove { base.RemoveHandler(AutotuneEvent, value); }
        }
    }
}