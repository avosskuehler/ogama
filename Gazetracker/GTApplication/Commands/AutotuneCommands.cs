using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace GazeTrackerUI.Commands
{
    public class AutotuneCommands: Window
    {
        public static readonly RoutedEvent AutotuneEvent = EventManager.RegisterRoutedEvent("AutotuneEvent", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(AutotuneCommands));

        public AutotuneCommands()
        {
        }

        public void Autotune()
        {
            RoutedEventArgs args1 = new RoutedEventArgs();
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
