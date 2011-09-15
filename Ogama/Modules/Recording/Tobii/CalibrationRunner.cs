using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Tobii.Eyetracking.Sdk;

namespace Ogama.Modules.Recording.TobiiDevice
{
  using Ogama.Modules.Common;

  public class TobiiCalibrationRunner
  {
    private readonly CalibrationForm calibrationForm;
    private readonly Timer sleepTimer;
    private IEyetracker tracker;
    private Queue<Point2D> calibrationPoints;
    private Calibration calibrationResult;

    public TobiiCalibrationRunner()
    {
      this.sleepTimer = new Timer { Interval = 1000 };
      this.sleepTimer.Tick += this.HandleTimerTick;

      this.calibrationForm = new CalibrationForm();
      this.calibrationForm.Load += this.CalibrationFormLoaded;
    }

    public Calibration RunCalibration(IEyetracker tracker)
    {
      CreatePointList();

      this.tracker = tracker;
      this.tracker.ConnectionError += HandleConnectionError;

      // Inform the eyetracker that we want to run a calibration
      this.tracker.StartCalibration();

      this.calibrationForm.ClearCalibrationPoint();
      PresentationScreen.PutFormOnPresentationScreen(this.calibrationForm, true);
      this.calibrationForm.ShowDialog();

      // Inform the eyetracker that we have finished 
      // the calibration routine
      this.tracker.StopCalibration();

      return this.calibrationResult;
    }


    private void StartNextOrFinish()
    {
      if (this.calibrationPoints.Count > 0)
      {
        var point = this.calibrationPoints.Dequeue();
        this.calibrationForm.DrawCalibrationPoint(point, Color.Yellow);
        this.sleepTimer.Start();
      }
      else
      {
        // Use the async version of ComputeCalibration since
        // this call takes some time
        this.tracker.ComputeCalibrationAsync(ComputeCompleted);
      }
    }


    private void HandleTimerTick(object sender, EventArgs e)
    {
      this.sleepTimer.Stop();
      var point = this.calibrationForm.CalibrationPoint;
      this.tracker.AddCalibrationPointAsync(point, PointCompleted);
    }

    private void PointCompleted(object sender, AsyncCompletedEventArgs<Empty> e)
    {
      this.calibrationForm.ClearCalibrationPoint();

      StartNextOrFinish();
    }

    private void ComputeCompleted(object sender, AsyncCompletedEventArgs<Empty> e)
    {
      this.calibrationForm.Close();

      if (e.Error != null)
      {
        this.calibrationResult = null;
      }
      else
      {
        this.calibrationResult = this.tracker.GetCalibration();
      }

    }

    private void CreatePointList()
    {
      this.calibrationPoints = new Queue<Point2D>();
      this.calibrationPoints.Enqueue(new Point2D(0.1, 0.1));
      this.calibrationPoints.Enqueue(new Point2D(0.5, 0.5));
      this.calibrationPoints.Enqueue(new Point2D(0.9, 0.1));
      this.calibrationPoints.Enqueue(new Point2D(0.9, 0.9));
      this.calibrationPoints.Enqueue(new Point2D(0.1, 0.9));
    }

    private void CalibrationFormLoaded(object sender, EventArgs e)
    {
      StartNextOrFinish();
    }

    private void HandleConnectionError(object sender, ConnectionErrorEventArgs e)
    {
      // Abort calibration if the connection fails
      AbortCalibration();
    }

    private void AbortCalibration()
    {
      this.calibrationResult = null;
      this.sleepTimer.Stop();
      this.calibrationForm.Close();
    }
  }
}
