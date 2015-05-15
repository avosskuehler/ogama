// <copyright file="TimeStopCondition.cs" company="FU Berlin">
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

namespace VectorGraphics.StopConditions
{
  using System;
  using System.ComponentModel;
  using System.Text;

  /// <summary>
  /// Derived from <see cref="StopCondition"/>.
  /// This class stores a <see cref="TimeStopCondition.Duration"/> after 
  /// which the display of the slide is ended.
  /// </summary>
  [Serializable]
  public class TimeStopCondition : StopCondition
  {
    ///////////////////////////////////////////////////////////////////////////////
    // Defining Constants                                                        //
    ///////////////////////////////////////////////////////////////////////////////
    #region CONSTANTS

    /// <summary>
    /// The default value for the duration in milliseconds.
    /// </summary>
    private const int DEFAULTDURATION = 1000;

    #endregion //CONSTANTS

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Variables, Enumerations, Events                                  //
    ///////////////////////////////////////////////////////////////////////////////
    #region FIELDS

    /// <summary>
    /// Saves the slide duration value in milliseconds.
    /// </summary>
    private int duration;

    #endregion //FIELDS

    ///////////////////////////////////////////////////////////////////////////////
    // Construction and Initializing methods                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region CONSTRUCTION

    /// <summary>
    /// Initializes a new instance of the TimeStopCondition class.
    /// </summary>
    public TimeStopCondition()
    {
    }

    /// <summary>
    /// Initializes a new instance of the TimeStopCondition class.
    /// </summary>
    /// <param name="newDuration">An <see cref="int"/> with the 
    /// length of the trial display time in milliseconds.</param>
    public TimeStopCondition(int newDuration)
    {
      this.duration = newDuration;
    }

    #endregion //CONSTRUCTION

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Properties                                                       //
    ///////////////////////////////////////////////////////////////////////////////
    #region PROPERTIES

    /// <summary>
    /// Gets or sets the length of the trial display time in milliseconds.
    /// </summary>
    /// <value>A <see cref="int"/> with the trial display time.</value>
    [Category("General")]
    [Description("The duration of the trial with this slide in milliseconds.")]
    public int Duration
    {
      get { return this.duration; }
      set { this.duration = value; }
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

    /// <summary>
    /// Overridden <see cref="Object.ToString()"/> method.
    /// Returns a <see cref="string"/> that represents the current <see cref="TimeStopCondition"/>. 
    /// </summary>
    /// <returns>A <see cref="string"/> that represents the current <see cref="TimeStopCondition"/></returns>
    public override string ToString()
    {
      StringBuilder sb = new StringBuilder();
      sb.Append("Time: ");
      sb.Append(this.duration.ToString());
      sb.Append(" ms");
      if (this.IsCorrectResponse.HasValue)
      {
        if (this.IsCorrectResponse.Value)
        {
          sb.Append(". Correct response to the slide.");
        }
        else
        {
          sb.Append(". Wrong response to the slide.");
        }
      }

      return sb.ToString();
    }

    /// <summary>
    /// Determines whether two <see cref="TimeStopCondition"/> instances are equal.
    /// </summary>
    /// <param name="obj">The <see cref="Object"/> to compare with the current 
    /// <see cref="TimeStopCondition"/>.</param>
    /// <returns><strong>True</strong> if the specified Object is equal 
    /// to the current <see cref="TimeStopCondition"/>; otherwise, <strong>false</strong>. </returns>
    public override bool Equals(object obj)
    {
      if (obj is TimeStopCondition)
      {
        TimeStopCondition tsc = (TimeStopCondition)obj;
        if (tsc.Duration == this.Duration)
        {
          return true;
        }
      }

      return false;
    }

    /// <summary>
    /// Serves as a hash function for a particular type. 
    /// <strong>GetHashCode</strong> is suitable for use in hashing algorithms 
    /// and data structures like a hash table. 
    /// It needs to be overridden when <see cref="Object.Equals(object)"/>
    /// is overriden.
    /// </summary>
    /// <returns>An <see cref="int"/> with the
    /// hash code for the current <see cref="TimeStopCondition"/>. </returns>
    public override int GetHashCode()
    {
      return this.duration.GetHashCode();
    }

    /// <summary>
    /// Reset the current time stop condition to
    /// default duration value.
    /// </summary>
    public override void ResetValue()
    {
      this.duration = DEFAULTDURATION;
    }

    /// <summary>
    /// Overridden <see cref="StopCondition.Clone()"/> method.
    /// Returns a <see cref="StopCondition"/> that is
    /// an exact copy of this <see cref="TimeStopCondition"/>. 
    /// </summary>
    /// <returns>A <see cref="StopCondition"/> that is
    /// an exact copy of this <see cref="TimeStopCondition"/>. </returns>
    protected override StopCondition CloneCore()
    {
      return new TimeStopCondition(this.duration);
    }

    #endregion //OVERRIDES

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
