// <copyright file="GazeDataChangedEventArgs.cs" company="FU Berlin">
// ******************************************************
// OGAMA - open gaze and mouse analyzer 
// Copyright (C) 2012 Adrian Voßkühler  
// ------------------------------------------------------------------------
// This program is free software; you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation; either version 2 of the License, or (at your option) any later version.
// This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
// You should have received a copy of the GNU General Public License along with this program; if not, write to the Free Software Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA 02111-1307 USA
// **************************************************************
// </copyright>
// <author>Adrian Voßkühler</author>
// <email>adrian@ogama.net</email>

namespace Ogama.Modules.Common.CustomEventArgs
{
  using System;

  using Ogama.Modules.Recording;

  /// <summary>
  /// Delegate. Handles gaze data changed event.
  /// </summary>
  /// <param name="sender">Source of the event.</param>
  /// <param name="e">A <see cref="GazeDataChangedEventArgs"/> with the new gaze data.</param>
  public delegate void GazeDataChangedEventHandler(object sender, GazeDataChangedEventArgs e);

  /// <summary>
  /// Derived from <see cref="System.EventArgs"/>
  /// Class that contains the data for the gaze data changed event. 
  /// </summary>
  public class GazeDataChangedEventArgs : EventArgs
  {
    ///////////////////////////////////////////////////////////////////////////////
    // Defining Variables, Enumerations, Events                                  //
    ///////////////////////////////////////////////////////////////////////////////
    #region FIELDS
    /// <summary>
    /// The new gaze data.
    /// </summary>
    private readonly GazeData gazedata;

    #endregion //FIELDS

    ///////////////////////////////////////////////////////////////////////////////
    // Construction and Initializing methods                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region CONSTRUCTION

    /// <summary>
    /// Initializes a new instance of the GazeDataChangedEventArgs class.
    /// </summary>
    /// <param name="newGazedata">The gaze data as a <see cref="GazeData"/> value.</param>
    public GazeDataChangedEventArgs(GazeData newGazedata)
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
    /// <value>The gaze data as a <see cref="GazeData"/> value.</value>
    public GazeData Gazedata
    {
      get { return this.gazedata; }
    }
    #endregion //PROPERTIES
  }
}
