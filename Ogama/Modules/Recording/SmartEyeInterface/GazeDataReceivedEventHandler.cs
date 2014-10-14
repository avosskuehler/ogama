// <copyright file="GazeDataReceivedEventHandler.cs" company="Smart Eye AB">
// ******************************************************
// OGAMA - open gaze and mouse analyzer 
// Copyright (C) 2014 Dr. Adrian Voßkühler  
// ------------------------------------------------------------------------
// This program is free software; you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation; either version 2 of the License, or (at your option) any later version.
// This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
// You should have received a copy of the GNU General Public License along with this program; if not, write to the Free Software Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA 02111-1307 USA
// **************************************************************
// </copyright>
// <author>Kathrin Scheil</author>
// <email>kathrin.scheil@smarteye.se</email>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ogama.Modules.Recording.SmartEyeInterface
{
  /// <summary>
  /// Delegate. Handles Smart Eye gaze data received event.
  /// </summary>
  /// <param name="sender">Source of the event.</param>
  /// <param name="e">A <see cref="GazeDataReceivedEventArgs"/> with the new gaze data.</param>
  public delegate void GazeDataReceivedEventHandler(object sender, GazeDataReceivedEventArgs e);

  /// <summary>
  /// Derived from <see cref="System.EventArgs"/>
  /// Class that contains the data for the gaze data changed event. 
  /// </summary>
  public class GazeDataReceivedEventArgs : EventArgs
  {
    ///////////////////////////////////////////////////////////////////////////////
    // Defining Variables, Enumerations, Events                                  //
    ///////////////////////////////////////////////////////////////////////////////
    #region FIELDS
    /// <summary>
    /// The new gaze data.
    /// </summary>
    private readonly SmartEyeGazeData gazedata;

    #endregion //FIELDS

    ///////////////////////////////////////////////////////////////////////////////
    // Construction and Initializing methods                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region CONSTRUCTION

    /// <summary>
    /// Initializes a new instance of the GazeDataChangedEventArgs class.
    /// </summary>
    /// <param name="newGazedata">The gaze data as a <see cref="SmartEyeGazeData"/> value.</param>
    public GazeDataReceivedEventArgs(SmartEyeGazeData newGazedata)
    {
      this.gazedata = newGazedata;
    }

    #endregion //CONSTRUCTION

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Properties                                                       //
    ///////////////////////////////////////////////////////////////////////////////
    #region PROPERTIES
    /// <summary>
    /// Gets the new gaze data.
    /// </summary>
    /// <value>The gaze data as a <see cref="SmartEyeGazeData"/> value.</value>
    public SmartEyeGazeData Gazedata
    {
      get { return this.gazedata; }
    }
    #endregion //PROPERTIES
  }
}
