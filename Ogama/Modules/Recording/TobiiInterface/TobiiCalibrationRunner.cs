// <copyright file="TobiiCalibrationRunner.cs" company="FU Berlin">
// ******************************************************
// OGAMA - open gaze and mouse analyzer 
// Copyright (C) 2015 Dr. Adrian Voßkühler  
// ------------------------------------------------------------------------
// This program is free software; you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation; either version 2 of the License, or (at your option) any later version.
// This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
// You should have received a copy of the GNU General Public License along with this program; if not, write to the Free Software Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA 02111-1307 USA
// **************************************************************
// </copyright>
// <author>Adrian Voßkühler</author>
// <email>adrian@ogama.net</email>

namespace Ogama.Modules.Recording.TobiiInterface
{
  using System;
  using System.Collections.Generic;
  using System.Drawing;
  using System.Windows.Forms;

  using Ogama.Modules.Common.Tools;

  using Tobii.Eyetracking.Sdk;

  /// <summary>
  /// The tobii calibration runner.
  /// </summary>
  public class TobiiCalibrationRunner
  {
    #region Constants and Fields

    /// <summary>
    /// The sleep timer.
    /// </summary>
    private readonly Timer sleepTimer;

    /// <summary>
    /// The tobii calibration form.
    /// </summary>
    private readonly TobiiCalibrationForm tobiiCalibrationForm;

    /// <summary>
    /// The calibration points.
    /// </summary>
    private Queue<Point2D> calibrationPoints;

    /// <summary>
    /// The calibration result.
    /// </summary>
    private Calibration calibrationResult;

    /// <summary>
    /// The tracker.
    /// </summary>
    private IEyetracker tracker;

    #endregion

    #region Constructors and Destructors

    /// <summary>
    /// Initializes a new instance of the <see cref="TobiiCalibrationRunner"/> class.
    /// </summary>
    public TobiiCalibrationRunner()
    {
      this.sleepTimer = new Timer { Interval = 1000 };
      this.sleepTimer.Tick += this.HandleTimerTick;

      this.tobiiCalibrationForm = new TobiiCalibrationForm();
      this.tobiiCalibrationForm.Load += this.TobiiCalibrationFormLoaded;
    }

    #endregion

    #region Public Methods

    /// <summary>
    /// The method runs the calibration of the tobii device.
    /// </summary>
    /// <param name="givenTracker">The <see cref="IEyetracker"/> tracker</param>
    /// <returns>A <see cref="Calibration"/> object</returns>
    public Calibration RunCalibration(IEyetracker givenTracker)
    {
      this.CreatePointList();

      this.tracker = givenTracker;
      this.tracker.ConnectionError += this.HandleConnectionError;

      // Inform the eyetracker that we want to run a calibration
      this.tracker.StartCalibration();

      this.tobiiCalibrationForm.ClearCalibrationPoint();
      PresentationScreen.PutFormOnPresentationScreen(this.tobiiCalibrationForm, true);
      this.tobiiCalibrationForm.ShowDialog();

      // Inform the eyetracker that we have finished 
      // the calibration routine
      this.tracker.StopCalibration();

      return this.calibrationResult;
    }

    #endregion

    #region Methods

    /// <summary>
    /// The abort calibration.
    /// </summary>
    private void AbortCalibration()
    {
      this.calibrationResult = null;
      this.sleepTimer.Stop();
      this.tobiiCalibrationForm.Close();
    }

    /// <summary>
    /// The compute completed.
    /// </summary>
    /// <param name="sender">
    /// The sender.
    /// </param>
    /// <param name="e">
    /// The e.
    /// </param>
    private void ComputeCompleted(object sender, AsyncCompletedEventArgs<Empty> e)
    {
      this.tobiiCalibrationForm.Close();

      if (e.Error != null)
      {
        this.calibrationResult = null;
      }
      else
      {
        this.calibrationResult = this.tracker.GetCalibration();
      }
    }

    /// <summary>
    /// The create point list.
    /// </summary>
    private void CreatePointList()
    {
      this.calibrationPoints = new Queue<Point2D>();
      this.calibrationPoints.Enqueue(new Point2D(0.1, 0.1));
      this.calibrationPoints.Enqueue(new Point2D(0.5, 0.5));
      this.calibrationPoints.Enqueue(new Point2D(0.9, 0.1));
      this.calibrationPoints.Enqueue(new Point2D(0.9, 0.9));
      this.calibrationPoints.Enqueue(new Point2D(0.1, 0.9));
    }

    /// <summary>
    /// The handle connection error.
    /// </summary>
    /// <param name="sender">
    /// The sender.
    /// </param>
    /// <param name="e">
    /// The e.
    /// </param>
    private void HandleConnectionError(object sender, ConnectionErrorEventArgs e)
    {
      // Abort calibration if the connection fails
      this.AbortCalibration();
    }

    /// <summary>
    /// The handle timer tick.
    /// </summary>
    /// <param name="sender">
    /// The sender.
    /// </param>
    /// <param name="e">
    /// The e.
    /// </param>
    private void HandleTimerTick(object sender, EventArgs e)
    {
      this.sleepTimer.Stop();
      var point = this.tobiiCalibrationForm.CalibrationPoint;
      this.tracker.AddCalibrationPointAsync(point, this.PointCompleted);
    }

    /// <summary>
    /// The point completed.
    /// </summary>
    /// <param name="sender">
    /// The sender.
    /// </param>
    /// <param name="e">
    /// The e.
    /// </param>
    private void PointCompleted(object sender, AsyncCompletedEventArgs<Empty> e)
    {
      this.tobiiCalibrationForm.ClearCalibrationPoint();

      this.StartNextOrFinish();
    }

    /// <summary>
    /// The start next or finish.
    /// </summary>
    private void StartNextOrFinish()
    {
      if (this.calibrationPoints.Count > 0)
      {
        var point = this.calibrationPoints.Dequeue();
        this.tobiiCalibrationForm.DrawCalibrationPoint(point, Color.Yellow);
        this.sleepTimer.Start();
      }
      else
      {
        // Use the async version of ComputeCalibration since
        // this call takes some time
        this.tracker.ComputeCalibrationAsync(this.ComputeCompleted);
      }
    }

    /// <summary>
    /// The tobii calibration form loaded.
    /// </summary>
    /// <param name="sender">
    /// The sender.
    /// </param>
    /// <param name="e">
    /// The e.
    /// </param>
    private void TobiiCalibrationFormLoaded(object sender, EventArgs e)
    {
      this.StartNextOrFinish();
    }

    #endregion
  }
}