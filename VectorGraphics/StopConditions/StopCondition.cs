// <copyright file="StopCondition.cs" company="FU Berlin">
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
  using System.Xml.Serialization;

  /// <summary>
  /// Abstract base class for all stop conditions.
  /// <remarks>A <strong>StopCondition</strong> is a pair of
  /// a type and a value for which the slide should beeing stop displayed.
  /// For example A <see cref="TimeStopCondition"/> has a property 
  /// <see cref="TimeStopCondition.Duration"/> that means, the slide is stopped,
  /// after this time has left.
  /// There are three different Stop conditions available:
  /// <see cref="TimeStopCondition"/>, <see cref="KeyStopCondition"/>,
  /// <see cref="MouseStopCondition"/>.</remarks>
  /// </summary>
  [Serializable]
  [XmlInclude(typeof(TimeStopCondition)),
   XmlInclude(typeof(KeyStopCondition)),
   XmlInclude(typeof(MouseStopCondition))]
  [TypeConverter(typeof(StopConditionConverter))]
  public abstract class StopCondition : ICloneable
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
    /// Saves a value indication if this condition was a correct response to
    /// the slide. Only applies, when correct responses are defined,
    /// otherwise this is null.
    /// </summary>
    private bool? isCorrectResponse;

    #endregion //FIELDS

    ///////////////////////////////////////////////////////////////////////////////
    // Construction and Initializing methods                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region CONSTRUCTION

    /// <summary>
    /// Initializes a new instance of the StopCondition class.
    /// </summary>
    public StopCondition()
    {
      this.isCorrectResponse = null;
    }

    /// <summary>
    /// Initializes a new instance of the StopCondition class.
    /// </summary>
    /// <param name="newIsCorrectResponse">A <see cref="Boolean"/> indicating if this is a correct response.
    /// This value can be null.</param>
    public StopCondition(bool? newIsCorrectResponse)
    {
      this.isCorrectResponse = newIsCorrectResponse;
    }

    #endregion //CONSTRUCTION

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Properties                                                       //
    ///////////////////////////////////////////////////////////////////////////////
    #region PROPERTIES

    /// <summary>
    /// Gets or sets a value indicating whether this condition was a correct response to
    /// the slide. Only applies, when correct responses are defined,
    /// otherwise this is null.
    /// </summary>
    public bool? IsCorrectResponse
    {
      get { return this.isCorrectResponse; }
      set { this.isCorrectResponse = value; }
    }

    #endregion //PROPERTIES

    ///////////////////////////////////////////////////////////////////////////////
    // Public methods                                                            //
    ///////////////////////////////////////////////////////////////////////////////
    #region PUBLICMETHODS

    /// <summary>
    /// Creates a excact copy of given <see cref="StopCondition"/>
    /// </summary>
    /// <returns>Excact copy of this <see cref="StopCondition"/></returns>
    public object Clone()
    {
      return this.CloneCore();
    }

    #endregion //PUBLICMETHODS

    ///////////////////////////////////////////////////////////////////////////////
    // Inherited methods                                                         //
    ///////////////////////////////////////////////////////////////////////////////
    #region OVERRIDES

    /// <summary>
    /// An implementation should reset the condition 
    /// to default values.
    /// </summary>
    public abstract void ResetValue();

    /// <summary>
    /// Should create a excact copy of the given stop condition
    /// </summary>
    /// <returns>Excact copy of this stop condition</returns>
    protected abstract StopCondition CloneCore();

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
