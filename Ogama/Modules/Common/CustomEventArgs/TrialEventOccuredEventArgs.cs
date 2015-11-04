// <copyright file="TrialEventOccuredEventArgs.cs" company="FU Berlin">
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

namespace Ogama.Modules.Common.CustomEventArgs
{
  using System;

  using Ogama.Modules.Common.TrialEvents;

  /// <summary>
  /// Delegate. Handles trial event occured event.
  /// </summary>
  /// <param name="sender">Source of the event.</param>
  /// <param name="e">A <see cref="TrialEventOccuredEventArgs"/> with the trial event</param>
  public delegate void TrialEventOccuredEventHandler(object sender, TrialEventOccuredEventArgs e);

  /// <summary>
  /// Derived from <see cref="System.EventArgs"/>
  /// Class that contains the data for the TrialEventOccured event. Derives from System.EventArgs.
  /// </summary>
  public class TrialEventOccuredEventArgs : EventArgs
  {
    ///////////////////////////////////////////////////////////////////////////////
    // Defining Variables, Enumerations, Events                                  //
    ///////////////////////////////////////////////////////////////////////////////
    #region FIELDS

    /// <summary>
    /// The <see cref="TrialEvent"/> to be reported.
    /// </summary>
    private readonly TrialEvent trialEvent;

    /// <summary>
    /// The <see cref="Int64"/> with the time the event occured
    /// measured in sample time.
    /// </summary>
    private readonly long eventTime;

    #endregion //FIELDS

    ///////////////////////////////////////////////////////////////////////////////
    // Construction and Initializing methods                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region CONSTRUCTION

    /// <summary>
    /// Initializes a new instance of the TrialEventOccuredEventArgs class.
    /// </summary>
    /// <param name="newTrialEvent">A <see cref="TrialEvent"/> with 
    /// the occured trial event and params.</param>
    /// <param name="newEventTime">A <see cref="Int64"/> with the time the event occured
    /// measured in sample time.</param>
    public TrialEventOccuredEventArgs(TrialEvent newTrialEvent, long newEventTime)
    {
      this.trialEvent = newTrialEvent;
      this.eventTime = newEventTime;
    }

    #endregion //CONSTRUCTION

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Properties                                                       //
    ///////////////////////////////////////////////////////////////////////////////
    #region PROPERTIES

    /// <summary>
    /// Gets the <see cref="TrialEvent"/>.
    /// </summary>
    /// <value>A <see cref="TrialEvent"/> with the event data.</value>
    public TrialEvent TrialEvent
    {
      get { return this.trialEvent; }
    }

    /// <summary>
    /// Gets the event time.
    /// </summary>
    /// <value>A <see cref="Int64"/> with the event time value.</value>
    public long EventTime
    {
      get { return this.eventTime; }
    }

    #endregion //PROPERTIES
  }
}
