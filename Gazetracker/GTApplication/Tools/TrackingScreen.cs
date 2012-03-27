// <copyright file="TrackingScreen.cs" company="ITU">
// ******************************************************
// GazeTrackingLibrary for ITU GazeTracker
// Copyright (C) 2010 Martin Tall  
// ------------------------------------------------------------------------
// This program is free software; you can redistribute it and/or modify it 
// under the terms of the GNU General Public License as published by the 
// Free Software Foundation; either version 3 of the License, 
// or (at your option) any later version.
// This program is distributed in the hope that it will be useful, 
// but WITHOUT ANY WARRANTY; without even the implied warranty of 
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU 
// General Public License for more details.
// You should have received a copy of the GNU General Public License 
// along with this program; if not, see http://www.gnu.org/licenses/.
// **************************************************************
// </copyright>
// <author>Adrian Voßkühler</author>
// <email>adrian@ogama.net</email>

using System.Drawing;
using System.Windows.Forms;
using GTCommons.Enum;
using Point = System.Windows.Point;
using GTSettings;

namespace GTApplication.Tools
{
    /// <summary>
    /// Static helper class for getting presentation screens and bounds.
    /// </summary>
    public class TrackingScreen
    {
        #region PROPERTIES

        /// <summary>
        /// Gets the bounds of the tracking screen.
        /// </summary>
        public static Rectangle TrackingScreenBounds
        {
            get { return GetTrackingScreenBounds(); }
        }

        /// <summary>
        /// Gets the top coordinate of the tracking screen.
        /// </summary>
        public static double TrackingScreenTop
        {
            get { return GetTrackingScreenBounds().Top; }
        }

        /// <summary>
        /// Gets the left coordinate of the tracking screen.
        /// </summary>
        public static double TrackingScreenLeft
        {
            get { return GetTrackingScreenBounds().Left; }
        }

        /// <summary>
        /// Gets the width of the tracking screen.
        /// </summary>
        public static double TrackingScreenWidth
        {
            get { return GetTrackingScreenResolution().Width; }
        }

        /// <summary>
        /// Gets the height of the tracking screen.
        /// </summary>
        public static double TrackingScreenHeight
        {
            get { return GetTrackingScreenResolution().Height; }
        }

        /// <summary>
        /// Gets the center of the tracking screen.
        /// </summary>
        public static Point TrackingScreenCenter
        {
            get
            {
                var centerPoint = new Point();
                centerPoint.X = TrackingScreenWidth/2;
                centerPoint.Y = TrackingScreenHeight/2;
                return centerPoint;
            }
        }

        #endregion //PROPERTIES

        #region PUBLICMETHODS

        /// <summary>
        /// Gets the working area of the presentation screen.
        /// In multiple screen environments the monitor is defined in Ogama Options
        /// </summary>
        /// <returns>The working area size of the presentation screen.</returns>
        private static Rectangle GetTrackingScreenWorkingArea()
        {
            return GetTrackingScreen().WorkingArea;
        }

        /// <summary>
        /// Gets the working area of the controller screen.
        /// </summary>
        /// <returns>The working area size of the controller screen.</returns>
        private static Rectangle GetControllerWorkingArea()
        {
            return GetControllerScreen().WorkingArea;
        }

        /// <summary>
        /// Gets the resolution of the presentation screen.
        /// </summary>
        /// <returns>A <see cref="Size"/> with the resolution of the presentation screen.</returns>
        private static Size GetTrackingScreenResolution()
        {
            return GetTrackingScreenBounds().Size;
        }

        /// <summary>
        /// Gets the bounding area of the presentation screen.
        /// </summary>
        /// <returns>A <see cref="Rectangle"/> with the bounds of the presentation screen.</returns>
        private static Rectangle GetTrackingScreenBounds()
        {
            return GetTrackingScreen().Bounds;
        }

        /// <summary>
        /// Gets the presentation screen in single and multiple screen environments.
        /// In multiple screen environments the method returns the screen you specify in Ogama Options.
        /// </summary>
        /// <returns>screen which is specified in Ogama Options if exists, otherwise primary.</returns>
        private static Screen GetTrackingScreen()
        {
            // Get the monitor screen       
            if (Settings.Instance.Calibration.TrackingMonitor == Monitor.Secondary && SystemHasSecondaryScreen())
            {
                return GetSecondaryScreen();
            }
            else
            {
                return Screen.PrimaryScreen;
            }
        }

        /// <summary>
        /// Gets the controller screen in single and multiple screen environments.
        /// </summary>
        /// <returns>The <see cref="Screen"/> which is not
        /// the presentation screen in dual monitor setups,
        /// otherwise, otherwise primary screen.</returns>
        private static Screen GetControllerScreen()
        {
            // Return the controller monitor screen       
            if (Settings.Instance.Calibration.TrackingMonitor == Monitor.Secondary || !SystemHasSecondaryScreen())
            {
                return Screen.PrimaryScreen;
            }
            else
            {
                return GetSecondaryScreen();
            }
        }

        #endregion //PUBLICMETHODS


        #region HELPER

        /// <summary>
        /// Checks the Instance system for a (at least) secondary screen
        /// </summary>
        /// <returns>True, if there is more than one screen in the Screen.AllScreens list</returns>
        private static bool SystemHasSecondaryScreen()
        {
            return Screen.AllScreens.Length > 1;
        }

        /// <summary>
        /// Gets the secondary screen if there is one. Otherwise return null.
        /// </summary>
        /// <returns>secondary screen or null</returns>
        private static Screen GetSecondaryScreen()
        {
            if (Screen.AllScreens.Length == 1)
            {
                return null;
            }

            foreach (Screen screen in Screen.AllScreens)
            {
                if (screen.Primary == false)
                {
                    return screen;
                }
            }

            return null;
        }

        #endregion //HELPER
    }
}