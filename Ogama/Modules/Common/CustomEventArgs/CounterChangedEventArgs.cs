// <copyright file="CounterChangedEventArgs.cs" company="FU Berlin">
// ******************************************************
// OGAMA - open gaze and mouse analyzer 
// Copyright (C) 2013 Dr. Adrian Voßkühler  
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

  /// <summary>
  /// Delegate. Handles counter changed event.
  /// </summary>
  /// <param name="sender">Source of the event.</param>
  /// <param name="e">A <see cref="CounterChangedEventArgs"/> with event arguments</param>
  public delegate void CounterChangedEventHandler(object sender, CounterChangedEventArgs e);

  /// <summary>
  /// Derived from <see cref="System.EventArgs"/>
  /// Class that contains the data for the counter changed event.
  /// </summary>
  public class CounterChangedEventArgs : EventArgs
  {
    ///////////////////////////////////////////////////////////////////////////////
    // Defining Variables, Enumerations, Events                                  //
    ///////////////////////////////////////////////////////////////////////////////
    #region FIELDS

    /// <summary>
    /// This member saves the index of the current visible slide
    /// in the current trial.
    /// </summary>
    private readonly int slideCounter;

    /// <summary>
    /// This member saves the ID of the current visible trial
    /// in the current slideshow.
    /// </summary>
    private readonly int trialID;

    #endregion //FIELDS

    ///////////////////////////////////////////////////////////////////////////////
    // Construction and Initializing methods                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region CONSTRUCTION

    /// <summary>
    /// Initializes a new instance of the CounterChangedEventArgs class.
    /// </summary>
    /// <param name="newTrialID">The ID of the current visible trial
    /// in the current slideshow.</param>
    /// <param name="newSlideCounter">The index of the current visible slide
    /// in the current trial.</param>
    public CounterChangedEventArgs(int newTrialID, int newSlideCounter)
    {
      this.trialID = newTrialID;
      this.slideCounter = newSlideCounter;
    }

    #endregion //CONSTRUCTION

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Properties                                                       //
    ///////////////////////////////////////////////////////////////////////////////
    #region PROPERTIES

    /// <summary>
    /// Gets the ID of the current visible trial
    /// in the current slideshow.
    /// </summary>
    public int TrialID
    {
      get { return this.trialID; }
    }

    /// <summary>
    /// Gets the index of the current visible slide
    /// in the current trial.
    /// </summary>
    public int SlideCounter
    {
      get { return this.slideCounter; }
    }

    #endregion //PROPERTIES
  }
}
