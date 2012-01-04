using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using GazeTrackerUI.Calibration.Events;
using GazeTrackerUI.Calibration;
using GazeTrackingLibrary.Logging;

namespace GazeTrackerUI.Commands
{
    public class CalibrationCommands : Window
    {
        public static readonly RoutedEvent StartEvent = EventManager.RegisterRoutedEvent("StartEvent", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(CalibrationCommands));
        public static readonly RoutedEvent RunningEvent = EventManager.RegisterRoutedEvent("RunningEvent", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(CalibrationCommands));
        public static readonly RoutedEvent EndEvent = EventManager.RegisterRoutedEvent("EndEvent", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(CalibrationCommands));
        public static readonly RoutedEvent AbortEvent = EventManager.RegisterRoutedEvent("AbortEvent", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(CalibrationCommands));
        public static readonly RoutedEvent PointStartEvent = EventManager.RegisterRoutedEvent("PointStartEvent", RoutingStrategy.Bubble, typeof(CalibrationPointEventArgs.CalibrationPointEventHandler), typeof(CalibrationCommands));
        public static readonly RoutedEvent PointEndEvent = EventManager.RegisterRoutedEvent("PointEndEvent", RoutingStrategy.Bubble, typeof(CalibrationPointEventArgs.CalibrationPointEventHandler), typeof(CalibrationCommands));
        public static readonly RoutedEvent ResultEvent = EventManager.RegisterRoutedEvent("ResultEvent", RoutingStrategy.Bubble, typeof(CalibrationResultEventHandler), typeof(CalibrationCommands));

        //public delegate void CalibrationPointStartEventHandler(object sender, CalibrationPointEventArgs e);
        //public delegate void CalibrationPointEndEventHandler(object sender, CalibrationPointEventArgs e);
        public delegate void CalibrationResultEventHandler(object sender, CalibrationResultEventArgs e);


        public CalibrationCommands()
        {
        }

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

        public event CalibrationPointEventArgs.CalibrationPointEventHandler OnPointStart
        {
            add { this.AddHandler(PointStartEvent, value); }
            remove { this.RemoveHandler(PointStartEvent, value); }
        }

        public event CalibrationPointEventArgs.CalibrationPointEventHandler OnPointEnd
        {
            add { this.AddHandler(PointEndEvent, value); }
            remove { this.RemoveHandler(PointEndEvent, value); }
        }

        #endregion


        #region Public methods

        public void Start()
        {
            RoutedEventArgs args1 = new RoutedEventArgs();
            args1 = new RoutedEventArgs(StartEvent, this);
            RaiseEvent(args1);
        }

        public void CalibrationRunning()
        {
            RoutedEventArgs args1 = new RoutedEventArgs();
            args1 = new RoutedEventArgs(RunningEvent, this);
            RaiseEvent(args1);
        }

        public void End()
        {
            RoutedEventArgs args1 = new RoutedEventArgs();
            args1 = new RoutedEventArgs(EndEvent, this);
            RaiseEvent(args1);
        }

        public void Abort()
        {
            RoutedEventArgs args1 = new RoutedEventArgs();
            args1 = new RoutedEventArgs(AbortEvent, this);
            RaiseEvent(args1);
        }

        public void Accept()
        {
            throw new NotImplementedException();
        }

        public void ExportResults(System.Windows.Media.Imaging.BitmapSource calibrationResult, int qualityRating)
        {
            CalibrationResultEventArgs args = new CalibrationResultEventArgs(calibrationResult, qualityRating);
            RaiseEvent(args);
        }

        public void PointStart(CalibrationPoint calPoint)
        {
            try
            {
                CalibrationPointEventArgs args = new CalibrationPointEventArgs(calPoint.Number, calPoint.Point);
                RaiseEvent(args);
            }
            catch (Exception ex)
            {
                ErrorLogger.WriteLine("CalibrationCommands, could not raise PointStart, Message: " + ex.Message);
            }
        }

        public void PointEnd(CalibrationPoint calPoint)
        {
            try
            {
                CalibrationPointEventArgs args = new CalibrationPointEventArgs(calPoint.Number, calPoint.Point);
                RaiseEvent(args);
            }
            catch (Exception ex)
            {
                ErrorLogger.WriteLine("CalibrationCommands, could not raise PointEnd, Message: " + ex.Message);
            }
        }

        #endregion



    }
}
