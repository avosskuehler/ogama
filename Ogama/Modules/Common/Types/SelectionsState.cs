// <copyright file="SelectionsState.cs" company="FU Berlin">
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

namespace Ogama.Modules.Common.Types
{
  using System;

  /// <summary>
  /// Class that saves the entries of the current trial:
  /// subject, trial name and trial section time
  /// </summary>
  /// <remarks>When a new interface form is opened, this class, which member is stored in
  /// the Document class is asked for the current selections to show always
  /// the recent stimulus image and subject data.</remarks>
  public class SelectionsState
  {
    ///////////////////////////////////////////////////////////////////////////////
    // Defining Constants                                                        //
    ///////////////////////////////////////////////////////////////////////////////
    #region CONSTANTS
    #endregion //CONSTANTS

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Variables, Enumerations, Events                                  //
    ///////////////////////////////////////////////////////////////////////////////
    #region FIELDS

    /// <summary>
    /// Flag. <strong>True</strong>, if a selection state is set,
    /// otherwise <strong>false</strong>.
    /// </summary>
    private bool stateIsSet;

    /// <summary>
    /// Saves the current trials ID.
    /// </summary>
    private int trialID;

    /// <summary>
    /// Saves the current trials sequence value.
    /// </summary>
    private int trialSequence;

    /// <summary>
    /// Saves the current trials section start time.
    /// </summary>
    private int trialSectionTime;

    /// <summary>
    /// Saves the current subject name.
    /// </summary>
    private string subjectName;

    #endregion //FIELDS

    ///////////////////////////////////////////////////////////////////////////////
    // Construction and Initializing methods                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region CONSTRUCTION

    /// <summary>
    /// Initializes a new instance of the SelectionsState class.
    /// </summary>
    public SelectionsState()
    {
      this.stateIsSet = false;
    }

    #endregion //CONSTRUCTION

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Properties                                                       //
    ///////////////////////////////////////////////////////////////////////////////
    #region PROPERTIES

    /// <summary>
    /// Gets last selected SubjectName for the currently active Document
    /// </summary>
    /// <value>A <see cref="string"/> with currently selected subject.</value>
    public string SubjectName
    {
      get { return this.subjectName; }
    }

    /// <summary>
    /// Gets last selected trial ID for the currently active Document
    /// </summary>
    /// <value>A <see cref="int"/> with currently selected trials ID.</value>
    public int TrialID
    {
      get { return this.trialID; }
    }

    /// <summary>
    /// Gets last selected trial sequence for the currently active Document
    /// </summary>
    /// <value>A <see cref="int"/> with currently selected trials sequence number.</value>
    public int TrialSequence
    {
      get { return this.trialSequence; }
    }

    /// <summary>
    /// Gets last selected section time for the currently active Document
    /// </summary>
    /// <value>An <see cref="int"/> with the trials current section time.</value>
    public int TrialSectionTime
    {
      get { return this.trialSectionTime; }
    }

    /// <summary>
    /// Gets a value indicating whether the members of this class having valid entries.
    /// </summary>
    /// <value>A <see cref="Boolean"/> that indicates wheter the selection
    /// state is set or not.</value>
    public bool IsSet
    {
      get { return this.stateIsSet; }
    }

    #endregion //PROPERTIES

    ///////////////////////////////////////////////////////////////////////////////
    // Eventhandler                                                              //
    ///////////////////////////////////////////////////////////////////////////////
    #region EVENTS

    ///////////////////////////////////////////////////////////////////////////////
    // Eventhandler for UI, Menu, Buttons, Toolbars etc.                         //
    ///////////////////////////////////////////////////////////////////////////////
    #region WINDOWSEVENTHANDLER
    #endregion //WINDOWSEVENTHANDLER

    ///////////////////////////////////////////////////////////////////////////////
    // Eventhandler for Custom Defined Events                                    //
    ///////////////////////////////////////////////////////////////////////////////
    #region CUSTOMEVENTHANDLER
    #endregion //CUSTOMEVENTHANDLER

    #endregion //EVENTS

    ///////////////////////////////////////////////////////////////////////////////
    // Methods and Eventhandling for Background tasks                            //
    ///////////////////////////////////////////////////////////////////////////////
    #region BACKGROUNDWORKER
    #endregion //BACKGROUNDWORKER

    ///////////////////////////////////////////////////////////////////////////////
    // Inherited methods                                                         //
    ///////////////////////////////////////////////////////////////////////////////
    #region OVERRIDES
    #endregion //OVERRIDES

    ///////////////////////////////////////////////////////////////////////////////
    // Methods for doing main class job                                          //
    ///////////////////////////////////////////////////////////////////////////////
    #region METHODS

    /// <summary>
    /// Updates SelectionState member with given drop down combo box entries.
    /// </summary>
    /// <param name="newSubjectName">A <see cref="string"/> with the selected subject name.</param>
    /// <param name="newTrialID">An <see cref="int"/> with the selected trials unique identifier.</param>
    /// <param name="newTrialSequence">An <see cref="int"/> with the selected trials unique sequence number.</param>
    /// <param name="newTrialSectionTime">A nullable <see cref="int"/> with the start 
    /// time of a subsection of this trial.</param>
    public void Update(string newSubjectName, int? newTrialID, int? newTrialSequence, int? newTrialSectionTime)
    {
      if (newSubjectName != null && newSubjectName != string.Empty)
      {
        this.subjectName = newSubjectName;
      }

      if (newTrialID.HasValue)
      {
        this.trialID = newTrialID.Value;
      }

      if (newTrialSequence.HasValue)
      {
        this.trialSequence = newTrialSequence.Value;
      }

      if (newTrialSectionTime.HasValue)
      {
        this.trialSectionTime = newTrialSectionTime.Value;
      }

      if (this.subjectName != null && this.trialSequence != -1)
      {
        this.stateIsSet = true;
      }

      if (newSubjectName == string.Empty && newTrialID == null && newTrialSequence == null)
      {
        this.stateIsSet = false;
      }
    }

    #endregion //METHODS

    ///////////////////////////////////////////////////////////////////////////////
    // Small helping Methods                                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region HELPER
    #endregion //HELPER
  }
}
