// <copyright file="TrialChangedEventArgs.cs" company="FU Berlin">
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

  using Ogama.DataSet;
  using Ogama.Modules.Common.SlideCollections;

  using VectorGraphics.StopConditions;

  /// <summary>
  /// Delegate. Handles trial changed event.
  /// </summary>
  /// <param name="sender">Source of the event.</param>
  /// <param name="e">A <see cref="TrialChangedEventArgs"/> with event arguments</param>
  public delegate void TrialChangedEventHandler(object sender, TrialChangedEventArgs e);

  /// <summary>
  /// Derived from <see cref="System.EventArgs"/>
  /// Class that contains the data for the trial changed event.
  /// </summary>
  public class TrialChangedEventArgs : EventArgs
  {
    ///////////////////////////////////////////////////////////////////////////////
    // Defining Variables, Enumerations, Events                                  //
    ///////////////////////////////////////////////////////////////////////////////
    #region FIELDS

    /// <summary>
    ///  Saves the name of the finished trial.
    /// </summary>
    private readonly Trial finishedTrial;

    /// <summary>
    ///  Saves the name of the next trial.
    /// </summary>
    private readonly Trial nextTrial;

    /// <summary>
    /// The response that ended the trial as a <see cref="StopCondition"/> value.
    /// </summary>
    private readonly StopCondition response;

    /// <summary>
    /// Saves the string with the slides category. For more information
    /// see property <see cref="Category"/>.
    /// </summary>
    private readonly string category;

    /// <summary>
    /// This member saves the index of the current visible trial
    /// in the current trial list.
    /// </summary>
    private readonly int trialCounter;

    /// <summary>
    /// This member saves the time of the webcams internal <see cref="System.Diagnostics.Stopwatch"/>
    /// </summary>
    private readonly long webcamTime;

    #endregion //FIELDS

    ///////////////////////////////////////////////////////////////////////////////
    // Construction and Initializing methods                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region CONSTRUCTION

    /// <summary>
    /// Initializes a new instance of the TrialChangedEventArgs class.
    /// </summary>
    /// <param name="newFinishedTrial">Finished trial.</param>
    /// <param name="newNextTrial">Next trial.</param>
    /// <param name="newResponse">The response of type <see cref="StopCondition"/>
    /// that ended the trial.</param>
    /// <param name="newCategory">A <see cref="string"/> with the
    /// slides category.</param>
    /// <param name="newTrialCounter">The index of the current visible trial
    /// in the current trial list.</param>
    /// <param name="newWebcamTime">The time in milliseconds of the begin of the 
    /// webcams video section.</param>
    public TrialChangedEventArgs(
      Trial newFinishedTrial,
      Trial newNextTrial,
      StopCondition newResponse,
      string newCategory,
      int newTrialCounter,
      long newWebcamTime)
    {
      this.finishedTrial = newFinishedTrial;
      this.nextTrial = newNextTrial;
      this.response = newResponse;
      this.category = newCategory;
      this.trialCounter = newTrialCounter;
      this.webcamTime = newWebcamTime;
    }

    #endregion //CONSTRUCTION

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Properties                                                       //
    ///////////////////////////////////////////////////////////////////////////////
    #region PROPERTIES

    /// <summary>
    /// Gets the finished trial.
    /// </summary>
    /// <value>A <see cref="Trial"/> with the finished trial.</value>
    public Trial FinishedTrial
    {
      get { return this.finishedTrial; }
    }

    /// <summary>
    /// Gets the next trial.
    /// </summary>
    /// <value>A <see cref="Trial"/> with the next trial.</value>
    public Trial NextTrial
    {
      get { return this.nextTrial; }
    }

    /// <summary>
    /// Gets the current response.
    /// </summary>
    /// <value>The response as a <see cref="StopCondition"/> value.</value>
    public StopCondition Response
    {
      get { return this.response; }
    }

    /// <summary>
    /// Gets a string with the stimulus category.
    /// This is used for distinction between different slide types.
    /// </summary>
    /// <value>A <see cref="string"/> with this slides category.</value>
    /// <remarks>Can be for example "BlankSlide" or "MultipleChoiceQuestion" etc.
    /// This field is stored in the <see cref="SQLiteOgamaDataSet.TrialsDataTable"/> 
    /// and can be used in the statistics module 
    /// <see cref="Modules.Statistics.StatisticsModule"/></remarks>
    public string Category
    {
      get { return this.category; }
    }

    /// <summary>
    /// Gets the index of the current visible trial
    /// in the current trial list.
    /// </summary>
    public int TrialCounter
    {
      get { return this.trialCounter; }
    }
    
    /// <summary>
    /// Gets the time of the webcams internal <see cref="System.Diagnostics.Stopwatch"/>
    /// </summary>
    public long WebcamTime
    {
      get { return this.webcamTime; }
    }

    #endregion //PROPERTIES
  }
}
