// <copyright file="GTGazeData.cs" company="ITU">
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
// <author>Martin Tall</author>
// <email>info@martintall.com</email>
// <modifiedby>Adrian Voßkühler</modifiedby>

using System;
using System.Windows;

namespace GTLibrary.Utils
{
    /// <summary>
    /// This class contains the data for a gaze position.
    /// </summary>
    public class GTGazeData
    {
        #region Varibles

        private double gazePosX;

        private double gazePosXLeft;
        private double gazePosXRight;
        private double gazePosY;
        private double gazePosYLeft;
        private double gazePosYRight;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the GTGazeData class.
        /// The coordinates are initalized with zero.
        /// </summary>
        public GTGazeData()
        {
            GazePositionX = 0;
            GazePositionY = 0;
        }

        /// <summary>
        /// Initializes a new instance of the GTGazeData class.
        /// The coordinates are initalized with the given coordinates.
        /// </summary>
        /// <param name="x">A <see cref="double"/> with the new x coordinate</param>
        /// <param name="y">A <see cref="double"/> with the new y coordinate</param>
        public GTGazeData(double x, double y)
        {
            GazePositionX = x;
            GazePositionY = y;
        }

        /// <summary>
        /// Initializes a new instance of the GTGazeData class.
        /// The coordinates are initalized with teh given point.
        /// </summary>
        /// <param name="p">A <see cref="Point"/> with the new gaze coordinates.</param>
        public GTGazeData(Point p)
        {
            GazePositionX = p.X;
            GazePositionY = p.Y;
        }

        #endregion //CONSTRUCTION

        #region Events

        #region Delegates

        /// <summary>
        /// The delegate for the <see cref="GazeDataChanged"/> event.
        /// </summary>
        /// <param name="x">A <see cref="double"/> with the x coordinate.</param>
        /// <param name="y">A <see cref="double"/> with the y coordinate.</param>
        public delegate void GazeDataChangedEventHandler(double x, double y);

        #endregion

        /// <summary>
        /// The event that is raised whenever this class has got new gaze data
        /// coordinates.
        /// </summary>
        public event GazeDataChangedEventHandler GazeDataChanged;

        #endregion EVENTS

        #region Get/Set

        public double GazePositionX
        {
            get { return GetGazeDataValues().X; }

            set { gazePosXLeft = value; }
        }

        public double GazePositionXLeft
        {
            get { return gazePosXLeft; }
            set { gazePosXLeft = value; }
        }

        public double GazePositionXRight
        {
            get { return gazePosXRight; }
            set { gazePosXRight = value; }
        }


        /// <summary>
        /// Gets or sets the y coodinate of the gaze position.
        /// </summary>
        public double GazePositionY
        {
            get { return GetGazeDataValues().Y; }

            set { gazePosYLeft = value; }
        }

        public double GazePositionYLeft
        {
            get { return gazePosYLeft; }
            set { gazePosYLeft = value; }
        }

        public double GazePositionYRight
        {
            get { return gazePosYRight; }
            set { gazePosYRight = value; }
        }

        #endregion //PROPERTIES

        #region Public methods

        /// <summary>
        /// Sets new gaze coordinates.
        /// </summary>
        /// <param name="x">A <see cref="double"/> with the new x coordinate.</param>
        /// <param name="y">A <see cref="double"/> with the new y coordinate.</param>
        public void Set(double x, double y)
        {
            gazePosX = x;
            gazePosY = y;

            RaiseChangedEvent();
        }

        public void Set(double xLeft, double yLeft, double xRight, double yRight)
        {
            gazePosXLeft = xLeft;
            gazePosXRight = xRight;
            gazePosYLeft = yLeft;
            gazePosYRight = yRight;

            RaiseChangedEvent();
        }

        /// <summary>
        /// Gets the Instance gaze position as a <see cref="System.Drawing.Point"/>
        /// </summary>
        /// <returns>The Instance gaze position as a <see cref="System.Drawing.Point"/></returns>
        public System.Drawing.Point ToPoint()
        {
            Point p = GetGazeDataValues();
            return new System.Drawing.Point(Convert.ToInt32(p.X), Convert.ToInt32(p.Y));
        }

        #endregion //PUBLICMETHODS

        #region Eventhandlers

        /// <summary>
        /// This method raises the GazeDataChanged event.
        /// </summary>
        private void RaiseChangedEvent()
        {
            if (GazeDataChanged != null)
            {
                Point gazePos = GetGazeDataValues(); // gets avg values if binocular
                GazeDataChanged(gazePos.X, gazePos.Y);
            }
        }

        #endregion

        #region Private methods

        private Point GetGazeDataValues()
        {
            // Left eye is always default
            double y = gazePosYLeft;
            double x = gazePosXLeft;

            // Binocular, add and divide
            if (gazePosYRight != 0 && gazePosXRight != 0)
            {
                y += gazePosYRight;
                x += gazePosXRight;

                if (gazePosXLeft != 0 && gazePosYLeft != 0)
                {
                    y = y/2;
                    x = x/2;
                }
            }

            //if (y > 0 && x > 0)
            return new Point(x, y);
            //else
            //    return new Point(0, 0);
        }

        #endregion
    }
}