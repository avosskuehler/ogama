// <copyright file="TrialSequenceChangedEventArgs.cs" company="FU Berlin">
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

  /// <summary>
  /// Delegate. Handles trial sequence changed event.
  /// </summary>
  /// <param name="sender">Source of the event.</param>
  /// <param name="e">A <see cref="TrialSequenceChangedEventArgs"/>
  /// with the new trial sequence number.</param>
  public delegate void TrialSequenceChangedEventHandler(object sender, TrialSequenceChangedEventArgs e);

  /// <summary>
  /// Derived from <see cref="System.EventArgs"/>
  /// Class that contains the data for the trial sequence changed event. Derives from System.EventArgs.
  /// </summary>
  public class TrialSequenceChangedEventArgs : EventArgs
  {
    ///////////////////////////////////////////////////////////////////////////////
    // Defining Variables, Enumerations, Events                                  //
    ///////////////////////////////////////////////////////////////////////////////
    #region FIELDS

    /// <summary>
    /// The unique trial sequence number.
    /// </summary>
    private readonly int trialSequence;

    #endregion //FIELDS

    ///////////////////////////////////////////////////////////////////////////////
    // Construction and Initializing methods                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region CONSTRUCTION

    /// <summary>
    /// Initializes a new instance of the TrialSequenceChangedEventArgs class.
    /// </summary>
    /// <param name="trialSequence">An <see cref="int"/> with the new sequence number.</param>
    public TrialSequenceChangedEventArgs(int trialSequence)
    {
      this.trialSequence = trialSequence;
    }

    #endregion //CONSTRUCTION

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Properties                                                       //
    ///////////////////////////////////////////////////////////////////////////////
    #region PROPERTIES

    /// <summary>
    /// Gets the trial sequence.
    /// </summary>
    /// <value>An <see cref="int"/> with the new sequence number.</value>
    public int TrialSequence
    {
      get { return this.trialSequence; }
    }

    #endregion //PROPERTIES
  }
}
