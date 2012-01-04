using System;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using GTCommons.Events;

namespace GTCommons.Commands
{
    public class CalibrationCommands : Window
    {
        #region RoutedEventHandlers

        public event RoutedEventHandler OnStart
        {
            add { base.AddHandler(StartEvent, value); }
            remove { base.RemoveHandler(StartEvent, value); }
        }

        public event RoutedEventHandler OnRunning
        {
            add { base.AddHandler(RunningEvent, value); }
            remove { base.RemoveHandler(RunningEvent, value); }
        }

        public event RoutedEventHandler OnEnd
        {
            add { base.AddHandler(EndEvent, value); }
            remove { base.RemoveHandler(EndEvent, value); }
        }

        public event RoutedEventHandler OnAbort
        {
            add { base.AddHandler(AbortEvent, value); }
            remove { base.RemoveHandler(AbortEvent, value); }
        }

        public event RoutedEventHandler OnAccepted
        {
            add { base.AddHandler(AcceptEvent, value); }
            remove { base.RemoveHandler(AcceptEvent, value); }
        }

        public event RoutedEventHandler OnPointStart
        {
            add { AddHandler(PointStartEvent, value); }
            remove { RemoveHandler(PointStartEvent, value); }
        }

        //public event CalibrationPointEventArgs.CalibrationPointEventHandler OnPointStart
        //{
        //    add { this.AddHandler(PointStartEvent, value); }
        //    remove { this.RemoveHandler(PointStartEvent, value); }
        //}

        public event CalibrationPointEventArgs.CalibrationPointEventHandler OnPointEnd
        {
            add { AddHandler(PointEndEvent, value); }
            remove { RemoveHandler(PointEndEvent, value); }
        }

        public event CalibrationResultEventArgs.CalibrationResultEventHandler OnResult
        {
            add { AddHandler(ResultEvent, value); }
            remove { RemoveHandler(ResultEvent, value); }
        }

        #endregion

        #region Public methods

        public void Start()
        {
            Dispatcher.BeginInvoke
                (
                    DispatcherPriority.Normal,
                    new Action
                        (
                        delegate
                        {
                            var args1 = new RoutedEventArgs();
                            args1 = new RoutedEventArgs(StartEvent, this);
                            RaiseEvent(args1);
                        }));
        }

        public void CalibrationRunning()
        {
            var args1 = new RoutedEventArgs();
            args1 = new RoutedEventArgs(RunningEvent, this);
            RaiseEvent(args1);
        }

        public void End()
        {
            var args1 = new RoutedEventArgs();
            args1 = new RoutedEventArgs(EndEvent, this);
            RaiseEvent(args1);
        }

        public void Abort()
        {
            Dispatcher.BeginInvoke
                (
                    DispatcherPriority.Normal,
                    new Action
                        (
                        delegate
                        {
                            var args1 = new RoutedEventArgs();
                            args1 = new RoutedEventArgs(AbortEvent, this);
                            RaiseEvent(args1);
                        }));
        }

        public void Accept()
        {
            Dispatcher.BeginInvoke
                (
                    DispatcherPriority.Normal,
                    new Action
                        (
                        delegate
                          {
                            var args1 = new RoutedEventArgs(AcceptEvent, this);
                            RaiseEvent(args1);
                          }));
        }

        public void ExportResults(BitmapSource calibrationResult, int qualityRating)
        {
            try
            {
              var args = new CalibrationResultEventArgs(calibrationResult, qualityRating)
                { RoutedEvent = ResultEvent, Source = this };
              RaiseEvent(args);
            }
            catch (Exception ex)
            {
                Console.WriteLine("CalibrationCommands, could not raise ExportResults, Message: " + ex.Message);
            }
        }

        public void PointStart(int number, Point point)
        {
            try
            {
                var args = new CalibrationPointEventArgs(number, point);
                RaiseEvent(args);
            }
            catch (Exception ex)
            {
                Console.WriteLine("CalibrationCommands, could not raise PointStart, Message: " + ex.Message);
            }
        }

        public void PointEnd(int number, Point point)
        {
            try
            {
                var args = new CalibrationPointEventArgs(number, point);
                RaiseEvent(args);
            }
            catch (Exception)
            {
                //ErrorLogger.WriteLine("CalibrationCommands, could not raise PointEnd, Message: " + ex.Message);
            }
        }

        #endregion

        public static readonly RoutedEvent StartEvent = EventManager.RegisterRoutedEvent("StartEvent", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(CalibrationCommands));
        public static readonly RoutedEvent RunningEvent = EventManager.RegisterRoutedEvent("RunningEvent", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(CalibrationCommands));
        public static readonly RoutedEvent EndEvent = EventManager.RegisterRoutedEvent("EndEvent", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(CalibrationCommands));
        public static readonly RoutedEvent AbortEvent = EventManager.RegisterRoutedEvent("AbortEvent", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(CalibrationCommands));
        public static readonly RoutedEvent PointStartEvent = EventManager.RegisterRoutedEvent("PointStartEvent", RoutingStrategy.Bubble, typeof(CalibrationPointEventArgs.CalibrationPointEventHandler), typeof(CalibrationCommands));
        public static readonly RoutedEvent PointEndEvent = EventManager.RegisterRoutedEvent("PointEndEvent", RoutingStrategy.Bubble, typeof(CalibrationPointEventArgs.CalibrationPointEventHandler), typeof(CalibrationCommands));
        public static readonly RoutedEvent ResultEvent = EventManager.RegisterRoutedEvent("ResultEvent", RoutingStrategy.Bubble, typeof(CalibrationResultEventArgs.CalibrationResultEventHandler), typeof(CalibrationCommands));
        public static readonly RoutedEvent AcceptEvent = EventManager.RegisterRoutedEvent("AcceptEvent", RoutingStrategy.Bubble, typeof(CalibrationResultEventArgs.CalibrationResultEventHandler), typeof(CalibrationCommands));
    }
}