using System;
using System.Collections.Generic;
using GTSettings;

namespace GTLibrary.EyeMovement
{
  using GTLibrary.Utils;

  /// <summary>
    /// This class implements the methods to detect the type of 
    /// eye movement (fixation or saccade)
    /// </summary>
    public class Classifier
    {
        #region EyeMovementStateEnum enum

        public enum EyeMovementStateEnum
        {
            NoFixation = 0,
            Fixation = 1,
        }

        #endregion

        #region Variables

        #region Calculate Velocity

        private double angularVelocity; //angular velocity between 2 consecutive samples
        private double distDegrees; //distance converted to degrees of visual angle
        private double distMm; //distance converted to mm
        private double distPixels; //distance in pixels (i.e. error on screen)
        private double timeElapsed; //time elapsed between 2 consecutive images

        #endregion

        private readonly List<GTPoint> recentPoints = new List<GTPoint>();
        private readonly List<double> velocities = new List<double>();

        private EyeMovementStateEnum eyeMovementState = EyeMovementStateEnum.NoFixation;

        private long previousTime;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor. Initializes the eye movement detector with default parameters
        /// </summary>
        public Classifier()
        {
            // This should be in an XML file (or in the UI)
            //maxWindowSize = 10;
            //windowSize = 2;
            //distUserToScreen = 60; // In cm
            //maxAngularSpeed = 50; // In degrees per second
            //maxDispersion = 150;
        }


        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="windowSize">Window size in samples (recommended value: 5 to 10)</param>
        /// <param name="maxAngularSpeed">Maximum angular speed allowed to consider
        /// a fixation. Angular speeds above this value will be considered a saccade</param>
        public Classifier(int maxWindowSize, int maxAngularSpeed)
        {
            Settings.Instance.EyeMovement.MaxWindowSize = maxWindowSize;
            Settings.Instance.EyeMovement.MaxAngularSpeed = maxAngularSpeed;
        }

        #endregion

        #region Get/Set properties

        public EyeMovementStateEnum EyeMovementState
        {
            get { return eyeMovementState; }
        }

        #endregion

        #region Public methods

        /// <summary>
        /// This method calculates the type of eye movement given a new point
        /// </summary>
        /// <param name="newPoint">New point</param>
        public void CalculateEyeMovement(GTPoint newPoint)
        {
            long time = DateTime.UtcNow.Ticks/TimeSpan.TicksPerMillisecond;
            AddNewPoint(newPoint, time);

            if (recentPoints.Count > 1)
            {
                if (Operations.GetMaxDistanceOnWindow(recentPoints) < Settings.Instance.EyeMovement.MaxDispersion)
                {
                    CalculateVelocity();
                    if (velocities[velocities.Count - 1] > Settings.Instance.EyeMovement.MaxAngularSpeed)
                        eyeMovementState = EyeMovementStateEnum.NoFixation;
                    else
                        eyeMovementState = EyeMovementStateEnum.Fixation;
                }
                else
                    eyeMovementState = EyeMovementStateEnum.NoFixation;
            }
            else
                eyeMovementState = EyeMovementStateEnum.NoFixation;

            if (eyeMovementState == EyeMovementStateEnum.NoFixation)
            {
                if (TrackDB.Instance.GetLastSample().EyeMovement == EyeMovementStateEnum.Fixation)
                {
                    recentPoints.Clear();
                    Settings.Instance.EyeMovement.WindowSize = 2;
                    AddNewPoint(newPoint, time);
                }
            }
            else
                Settings.Instance.EyeMovement.WindowSize = Math.Min(Settings.Instance.EyeMovement.WindowSize,
                                                                     Settings.Instance.EyeMovement.MaxWindowSize);
        }

        #endregion

        #region Private methods

        private void AddNewVelocity(double velocity)
        {
            if (velocities.Count == Settings.Instance.EyeMovement.WindowSize)
                velocities.RemoveAt(0);

            velocities.Add(velocity);
        }


        private void AddNewPoint(GTPoint newPoint, long time)
        {
            if (recentPoints.Count >= Settings.Instance.EyeMovement.WindowSize)
                recentPoints.RemoveAt(0);

            recentPoints.Add(newPoint);

            timeElapsed = time - previousTime;
            timeElapsed = timeElapsed/1000;
            previousTime = time;
        }

        /// <summary>
        /// Calculate angular velocity
        /// </summary>
        private void CalculateVelocity()
        {
            var newPoint = new GTPoint(recentPoints[recentPoints.Count - 1]);
            var oldPoint = new GTPoint(recentPoints[recentPoints.Count - 2]);
            distPixels = Operations.Distance(newPoint, oldPoint);

            distMm = ConvertPixToMm(distPixels);

            distDegrees = Math.Atan(distMm/10/Settings.Instance.EyeMovement.DistanceUserToScreen);
            distDegrees = distDegrees*180/Math.PI;

            angularVelocity = distDegrees/timeElapsed;

            AddNewVelocity(angularVelocity);
        }


        private static double ConvertPixToMm(double pixels)
        {
            return pixels*ScreenParameters.PrimarySize.Width/ScreenParameters.PrimaryResolution.Width;
        }

        #endregion
    }
}