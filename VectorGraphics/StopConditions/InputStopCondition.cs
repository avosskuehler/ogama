// <copyright file="InputStopCondition.cs" company="FU Berlin">
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

namespace VectorGraphics.StopConditions
{
  using System;
  using System.ComponentModel;

  /// <summary>
  /// Derived from <see cref="StopCondition"/>.
  /// This class is the base class for input stop conditions like MouseInput or KeyInput.
  /// </summary>
  [Serializable]
  public abstract class InputStopCondition : StopCondition
  {
    ///////////////////////////////////////////////////////////////////////////////
    // Defining Constants                                                        //
    ///////////////////////////////////////////////////////////////////////////////
    #region CONSTANTS

    /// <summary>
    /// A <see cref="Boolean"/> with the default value for
    /// the <see cref="InputStopCondition.CanBeAnyInputOfThisType"/> property.
    /// </summary>
    private const bool DEFAULTANYKEY = false;

    #endregion //CONSTANTS

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Variables, Enumerations, Events                                  //
    ///////////////////////////////////////////////////////////////////////////////
    #region FIELDS

    /// <summary>
    /// Saves the value, if any input of child class type is valid.
    /// </summary>
    private bool anyInputOfThisType;

    /// <summary>
    /// Saves the name of the trial that this condition links to
    /// or is null, if this is not a link.
    /// </summary>
    private int? trialID;

    #endregion //FIELDS

    ///////////////////////////////////////////////////////////////////////////////
    // Construction and Initializing methods                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region CONSTRUCTION

    /// <summary>
    /// Initializes a new instance of the InputStopCondition class.
    /// </summary>
    public InputStopCondition()
    {
      this.anyInputOfThisType = false;
      this.trialID = null;
    }

    /// <summary>
    /// Initializes a new instance of the InputStopCondition class.
    /// Clone Constructor. Creates new <see cref="InputStopCondition"/> that is
    /// identical to the given <see cref="InputStopCondition"/>.
    /// </summary>
    /// <param name="newInputStopCondition">The <see cref="InputStopCondition"/> to clone.</param>
    public InputStopCondition(InputStopCondition newInputStopCondition)
      : base(newInputStopCondition.IsCorrectResponse)
    {
      this.anyInputOfThisType = newInputStopCondition.CanBeAnyInputOfThisType;
      this.trialID = newInputStopCondition.TrialID;
    }

    /// <summary>
    /// Initializes a new instance of the InputStopCondition class.
    /// </summary>
    /// <param name="newCanBeAnyInput"><strong>True</strong>, if any mouse button can
    /// end the slide display, otherwise <strong>false</strong>.</param>
    /// <param name="newTrialID">A nullable <see cref="int"/> with the trial
    /// ID this stop condition links to or null if this is not a link.</param>
    public InputStopCondition(bool newCanBeAnyInput, int? newTrialID)
    {
      this.anyInputOfThisType = newCanBeAnyInput;
      this.trialID = newTrialID;
    }

    #endregion //CONSTRUCTION

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Properties                                                       //
    ///////////////////////////////////////////////////////////////////////////////
    #region PROPERTIES

    /// <summary>
    /// Gets or sets a value indicating whether any mousebutton or key can end the slide display.
    /// </summary>
    /// <value>A <see cref="Boolean"/> which is true, 
    /// if any mousebutton or key can end the slide display.</value>
    [Category("General")]
    [Description("A flag, if any mouse button or key can end the slide display.")]
    public bool CanBeAnyInputOfThisType
    {
      get { return this.anyInputOfThisType; }
      set { this.anyInputOfThisType = value; }
    }

    /// <summary>
    /// Gets or sets the trial ID this condition links to.
    /// </summary>
    /// <value>A nullable <see cref="int"/> which is the trial ID of 
    /// the trial this condition links to or null if there is no link.</value>
    [Category("General")]
    [Description("The trial ID this condition links to.")]
    public int? TrialID
    {
      get { return this.trialID; }
      set { this.trialID = value; }
    }

    #endregion //PROPERTIES

    ///////////////////////////////////////////////////////////////////////////////
    // Public methods                                                            //
    ///////////////////////////////////////////////////////////////////////////////
    #region PUBLICMETHODS
    #endregion //PUBLICMETHODS

    ///////////////////////////////////////////////////////////////////////////////
    // Inherited methods                                                         //
    ///////////////////////////////////////////////////////////////////////////////
    #region OVERRIDES

    /// <summary>
    /// Reset the current mouse stop condition to
    /// default values
    /// </summary>
    public override void ResetValue()
    {
      this.anyInputOfThisType = DEFAULTANYKEY;
      this.trialID = null;
      this.IsCorrectResponse = null;
    }

    #endregion //OVERRIDES

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
    // Methods for doing main class job                                          //
    ///////////////////////////////////////////////////////////////////////////////
    #region METHODS
    #endregion //METHODS

    ///////////////////////////////////////////////////////////////////////////////
    // Small helping Methods                                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region HELPER
    #endregion //HELPER
  }
}