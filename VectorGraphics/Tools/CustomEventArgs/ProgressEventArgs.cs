// <copyright file="ProgressEventArgs.cs" company="FU Berlin">
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

namespace VectorGraphics.Tools.CustomEventArgs
{
  using System;

  /// <summary>
  /// Delegate. Handles progress event
  /// </summary>
  /// <param name="sender">Source of the event.</param>
  /// <param name="e">A <see cref="ProgressEventArgs"/> with progress data.</param>
  public delegate void ProgressEventHandler(object sender, ProgressEventArgs e);

  /// <summary>
  /// Derived from <see cref="System.EventArgs"/>
  /// Class that contains the data for the progress event. 
  /// </summary>
  public class ProgressEventArgs : EventArgs
  {
    ///////////////////////////////////////////////////////////////////////////////
    // Defining Variables, Enumerations, Events                                  //
    ///////////////////////////////////////////////////////////////////////////////
    #region FIELDS

    /// <summary>
    /// Flag. True, if backgroundworker has finished work.
    /// </summary>
    private readonly bool isFinished;

    /// <summary>
    /// Percentage of task completition.
    /// </summary>
    private readonly int percentComplete;

    /// <summary>
    /// string containing current timing position in ms.
    /// </summary>
    private readonly int currentMillisecond;

    #endregion //FIELDS

    ///////////////////////////////////////////////////////////////////////////////
    // Construction and Initializing methods                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region CONSTRUCTION

    /// <summary>
    /// Initializes a new instance of the ProgressEventArgs class.
    /// </summary>
    /// <param name="newIsFinished">Flag. True, if backgroundworker has finished work.</param>
    /// <param name="newPercentComplete">Percentage of task completition.</param>
    /// <param name="newCurrentMillisecond">An <see cref="int"/> 
    /// containing the current timing position in ms.</param>
    public ProgressEventArgs(bool newIsFinished, int newPercentComplete, int newCurrentMillisecond)
    {
      this.isFinished = newIsFinished;
      this.percentComplete = newPercentComplete;
      this.currentMillisecond = newCurrentMillisecond;
    }

    #endregion //CONSTRUCTION

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Properties                                                       //
    ///////////////////////////////////////////////////////////////////////////////
    #region PROPERTIES

    /// <summary>
    /// Gets progress value in percent. 
    /// </summary>
    /// <value>An <see cref="int"/> with the total
    /// percentage of finished rows when the progress event 
    /// is generated.</value>
    public int ProgressInPercent
    {
      get { return this.percentComplete; }
    }

    /// <summary>
    /// Gets a value indicating whether the background worker 
    /// has finished when the progress event is generated
    /// </summary>
    /// <value>A <see cref="Boolean"/> that is <strong>true</strong>,
    /// when the background worker has finished when the progress event is generated,
    /// otherwise <strong>false</strong>.
    /// </value>
    public bool Finished
    {
      get { return this.isFinished; }
    }

    /// <summary>
    /// Gets  a string containing current timing position in ms.
    /// </summary>
    /// <value>An <see cref="int"/> with the
    /// timing position in ms when the progress event 
    /// is generated.</value>
    public int Millisecond
    {
      get { return this.currentMillisecond; }
    }

    #endregion //PROPERTIES
  }
}
