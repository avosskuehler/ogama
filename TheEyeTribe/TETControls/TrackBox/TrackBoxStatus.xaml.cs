using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Media;
using System.Windows.Threading;
using TETCSharpClient.Data;
using TETCSharpClient;
using Image = System.Windows.Controls.Image;
using Size = System.Windows.Size;

namespace TETControls.TrackBox
{
  public partial class TrackBoxStatus : IGazeListener, ITrackerStateListener
  {
    #region Structs

    // trackbox struct
    private struct TrackBoxObject
    {
      public PointF Left;
      public PointF Right;
      public EyeCount LeftValidity;
      public EyeCount RightValidity;
    }

    #endregion

    #region Enums

    private enum EyeCount { Zero = 0, One, Two }

    #endregion

    #region Variables

    private const int UI_UPDATE_FREQUENCY = 60; // Hz
    private const int MAX_BAD_SAMPLES = 3;
    private const int HISTORY_SIZE = 20;
    private const float ACCEPTABLE_QUALITY = 0.2f;

    private readonly object Locker = new object();
    private readonly Queue<TrackBoxObject> TrackBoxHistory = new Queue<TrackBoxObject>(HISTORY_SIZE);
    private TrackBoxObject LatestGoodSample;
    private TrackBoxObject CurrentTrackboxObj;
    private double LatestAngle;
    private double LatestNormalizedDistance;
    private double EyeScale;
    private float CurrentTrackingQuality;
    private int BadSuccessiveSamples;
    private Size ControlSize;

    private TransformGroup LeftTransGroup;
    private RotateTransform LeftTransRotation;
    private TranslateTransform LeftTransTranslation;
    private ScaleTransform LeftTransScale;
    private TransformGroup RightTransGroup;
    private RotateTransform RightTransRotation;
    private TranslateTransform RightTransTranslation;
    private ScaleTransform RightTransScale;

    #endregion

    #region Get/Set

    public double GridOpacity
    {
      get { return GridImages.Opacity; }
      set { GridImages.Opacity = value; }
    }

    public Visibility GridBackgroundImageVisibility
    {
      get { return GridImageBackground.Visibility; }
      set { GridImageBackground.Visibility = value; }
    }

    #endregion

    #region Constructor

    public TrackBoxStatus()
    {
      InitializeComponent();

      DispatcherTimer UIUpdateTimer = new DispatcherTimer();
      UIUpdateTimer.Tick += UIUpdateTimerTick;
      UIUpdateTimer.Interval = new TimeSpan(0, 0, 0, 0, 1000 / UI_UPDATE_FREQUENCY);
      UIUpdateTimer.Start();

      Loaded += (sender, args) =>
      {
        var size = new Size(Width, Height);
        if (double.IsNaN(size.Width))
          size = new Size(ActualWidth, ActualHeight);
        ControlSize = size;

        // Normalize eyes and 'no-tracking image' - based on the designHeight of the user control
        EyeScale = ControlSize.Height / 1000;
        status_no_tracking.Width = ControlSize.Height / 3;
        status_no_tracking.Height = ControlSize.Height / 3;

        // Initialize the transformation variables for left and right eye
        LeftTransRotation = new RotateTransform();
        LeftTransTranslation = new TranslateTransform();
        LeftTransScale = new ScaleTransform();
        LeftTransGroup = new TransformGroup();
        LeftTransGroup.Children.Add(LeftTransRotation);
        LeftTransGroup.Children.Add(LeftTransScale);
        LeftTransGroup.Children.Add(LeftTransTranslation);

        RightTransRotation = new RotateTransform();
        RightTransTranslation = new TranslateTransform();
        RightTransScale = new ScaleTransform();
        RightTransGroup = new TransformGroup();
        RightTransGroup.Children.Add(RightTransRotation);
        RightTransGroup.Children.Add(RightTransScale);
        RightTransGroup.Children.Add(RightTransTranslation);
      };
    }

    public void Connect()
    {
      GazeManager.Instance.AddGazeListener(this);
      GazeManager.Instance.AddTrackerStateListener(this);
      this.OnTrackerStateChanged(GazeManager.Instance.Trackerstate);
    }

    public void Disconnect()
    {
      GazeManager.Instance.RemoveGazeListener(this);
      GazeManager.Instance.RemoveTrackerStateListener(this);
      this.OnTrackerStateChanged(GazeManager.Instance.Trackerstate);
      this.CurrentTrackingQuality = 0;
      this.DoVisibility();
    }

    #endregion

    #region Public Methods

    public void OnScreenStatesChanged(int screenIndex, int screenResolutionWidth, int screenResolutionHeight, float screenPhysicalWidth, float screenPhysicalHeight)
    { }

    public void OnGazeUpdate(GazeData gazeData)
    {
      ProcessSample(gazeData);
      AnalyzeSamples();
    }

    public void OnTrackerStateChanged(GazeManager.TrackerState trackerState)
    {
      if (labelDeviceConnected.Dispatcher.Thread != Thread.CurrentThread)
      {
        this.Dispatcher.BeginInvoke(new MethodInvoker(() => OnTrackerStateChanged(trackerState)));
        return;
      }

      gridContent.Visibility = Visibility.Hidden;

      switch (trackerState)
      {
        case GazeManager.TrackerState.TRACKER_CONNECTED:
          labelDeviceConnected.Content = "";
          gridContent.Visibility = Visibility.Visible;
          break;
        case GazeManager.TrackerState.TRACKER_CONNECTED_NOUSB3:
          labelDeviceConnected.Content = "Device connected to a USB2.0 port";
          break;
        case GazeManager.TrackerState.TRACKER_CONNECTED_BADFW:
          labelDeviceConnected.Content = "A firmware updated is required.";
          break;
        case GazeManager.TrackerState.TRACKER_NOT_CONNECTED:
          labelDeviceConnected.Content = "Device not connected.";
          break;
        case GazeManager.TrackerState.TRACKER_CONNECTED_NOSTREAM:
          labelDeviceConnected.Content = "No data coming out of the sensor.";
          break;
      }
    }

    #endregion

    #region Private UI Methods

    private void UIUpdateTimerTick(object sender, EventArgs e)
    {
      if (gridContent.Visibility != Visibility.Visible)
        return;

      lock (Locker)
      {
        // Do background opacity
        status_quality.Opacity = CurrentTrackingQuality;
        status_quality_inverted.Opacity = 1f - CurrentTrackingQuality;

        // Determine what should visible and update the eye positions if needed
        if (DoVisibility())
        {
          UpdateEyes();
        }
      }
    }

    private bool DoVisibility()
    {
      if (CurrentTrackingQuality <= ACCEPTABLE_QUALITY)
      {
        status_no_tracking.Visibility = Visibility.Visible;
        eye_left.Visibility = Visibility.Collapsed;
        eye_right.Visibility = Visibility.Collapsed;
        return false;
      }
      status_no_tracking.Visibility = Visibility.Collapsed;
      return true;
    }

    private void UpdateEyes()
    {
      // Update each eye with their respective transformation group
      UpdateEye(eye_left, CurrentTrackboxObj.Left, CurrentTrackboxObj.LeftValidity, LeftTransGroup);
      UpdateEye(eye_right, CurrentTrackboxObj.Right, CurrentTrackboxObj.RightValidity, RightTransGroup);
    }

    private void UpdateEye(Image eye, PointF pos, EyeCount validity, TransformGroup transformation)
    {
      if (pos != PointF.Empty && validity <= EyeCount.Two)
      {
        eye.Visibility = Visibility.Visible;
        var scale = EyeScale + DoEyeSizeDiff() * EyeScale;
        var x = pos.X * ControlSize.Width;
        var y = pos.Y * ControlSize.Height;
        ((RotateTransform)transformation.Children[0]).Angle = LatestAngle;
        ((ScaleTransform)transformation.Children[1]).ScaleX = scale;
        ((ScaleTransform)transformation.Children[1]).ScaleY = scale;
        ((TranslateTransform)transformation.Children[2]).X = x - (eye.ActualWidth) / 2;
        ((TranslateTransform)transformation.Children[2]).Y = y - (eye.ActualHeight) / 2;
        eye.RenderTransform = transformation;
      }
      else
      {
        eye.Visibility = Visibility.Collapsed;
      }
    }

    #endregion

    #region Private Methods

    private void ProcessSample(GazeData gazeData)
    {
      var right = PointF.Empty;
      var left = PointF.Empty;

      if ((gazeData.State & GazeData.STATE_TRACKING_EYES) != 0 || (gazeData.State & GazeData.STATE_TRACKING_PRESENCE) != 0)
      {
        if (gazeData.LeftEye.PupilCenterCoordinates.X != 0 && gazeData.LeftEye.PupilCenterCoordinates.Y != 0)
        {
          left.X = (float)gazeData.LeftEye.PupilCenterCoordinates.X;
          left.Y = (float)gazeData.LeftEye.PupilCenterCoordinates.Y;
        }

        if (gazeData.RightEye.PupilCenterCoordinates.X != 0 && gazeData.RightEye.PupilCenterCoordinates.Y != 0)
        {
          right.X = (float)gazeData.RightEye.PupilCenterCoordinates.X;
          right.Y = (float)gazeData.RightEye.PupilCenterCoordinates.Y;
        }
      }

      // create a new trackbox sample and enqueue it
      CurrentTrackboxObj = new TrackBoxObject
      {
        Left = left,
        Right = right,
        LeftValidity = left != PointF.Empty ? EyeCount.One : EyeCount.Zero,
        RightValidity = right != PointF.Empty ? EyeCount.One : EyeCount.Zero
      };
      EnqueueTrackBoxObject(CurrentTrackboxObj);
    }

    private void AnalyzeSamples()
    {
      lock (Locker)
      {
        CurrentTrackingQuality = GetStatus();
        var quality = VisibleEyesCount(CurrentTrackboxObj);
        if (quality == EyeCount.One || quality == EyeCount.Two)
        {
          BadSuccessiveSamples = 0;
          LatestGoodSample = CurrentTrackboxObj;

          // calculate eye angle if both eyes are visible
          if (quality == EyeCount.Two)
          {
            var dx = CurrentTrackboxObj.Right.X - CurrentTrackboxObj.Left.X;
            var dy = CurrentTrackboxObj.Right.Y - CurrentTrackboxObj.Left.Y;
            LatestNormalizedDistance = Math.Sqrt(Math.Pow(dx, 2) + Math.Pow(dy, 2));
            LatestAngle = ((180 / Math.PI * Math.Atan2((dy) * ControlSize.Height, (dx) * ControlSize.Width)));
          }
        }
        else
        {
          // we are forgiving with a couple of bad samples
          BadSuccessiveSamples++;
          if (BadSuccessiveSamples < MAX_BAD_SAMPLES)
          {
            CurrentTrackboxObj = LatestGoodSample;
          }
        }
      }
    }

    private double DoEyeSizeDiff()
    {
      // Linear scale - normalised with the overall eye scale
      const double b = 0.15; // magic number (gestimated normalized distance between eyes)
      const double a = 1;
      return ((LatestNormalizedDistance - b) / b) * a;
    }

    private void EnqueueTrackBoxObject(TrackBoxObject tbo)
    {
      TrackBoxHistory.Enqueue(tbo);
      while (TrackBoxHistory.Count > HISTORY_SIZE)
      {
        TrackBoxHistory.Dequeue();
      }
    }

    private float GetStatus()
    {
      // Get the overall tracking quality from our TrackBoxHistory
      var totalQuality = 0;
      var count = 0;

      var clone = new TrackBoxObject[HISTORY_SIZE];
      this.TrackBoxHistory.CopyTo(clone, 0);

      foreach (var item in clone)
      {
        totalQuality += (int)VisibleEyesCount(item);
        count++;
      }

      return totalQuality == (int)EyeCount.Zero ? totalQuality : totalQuality / ((float)EyeCount.Two * count);
    }

    private static EyeCount VisibleEyesCount(TrackBoxObject tbi)
    {
      // Get the quality of a single frame based on eye count
      if (tbi.LeftValidity == EyeCount.Zero && tbi.RightValidity == EyeCount.Zero)
      {
        // both eyes are gone
        return EyeCount.Zero;
      }

      if (tbi.LeftValidity == EyeCount.One && tbi.RightValidity == EyeCount.One)
      {
        // two eyes are found
        return EyeCount.Two;
      }

      // only left or right eye is showing
      return EyeCount.One;
    }
    #endregion
  }
}