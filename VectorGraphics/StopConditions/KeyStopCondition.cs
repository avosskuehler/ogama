// <copyright file="KeyStopCondition.cs" company="FU Berlin">
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

namespace VectorGraphics.StopConditions
{
  using System;
  using System.Collections;
  using System.Collections.Generic;
  using System.ComponentModel;
  using System.Text;
  using System.Windows.Forms;
  using System.Xml.Serialization;

  /// <summary>
  /// Derived from <see cref="StopCondition"/>.
  /// This class stores a <see cref="KeyStopCondition.StopKey"/>.
  /// When this is pressed the display of the slide should be ended.
  /// </summary>
  [Serializable]
  public class KeyStopCondition : InputStopCondition
  {
    ///////////////////////////////////////////////////////////////////////////////
    // Defining Constants                                                        //
    ///////////////////////////////////////////////////////////////////////////////
    #region CONSTANTS

    /// <summary>
    /// A <see cref="Keys"/> with the default value for
    /// the <see cref="KeyStopCondition.StopKey"/> property.
    /// </summary>
    private const Keys DEFAULTKEY = Keys.None;

    #endregion //CONSTANTS

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Variables, Enumerations, Events                                  //
    ///////////////////////////////////////////////////////////////////////////////
    #region FIELDS

    /// <summary>
    /// The <see cref="Keys"/> to listen for.
    /// </summary>
    private Keys stopKey;

    #endregion //FIELDS

    ///////////////////////////////////////////////////////////////////////////////
    // Construction and Initializing methods                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region CONSTRUCTION

    /// <summary>
    /// Initializes a new instance of the KeyStopCondition class.
    /// </summary>
    public KeyStopCondition()
    {
      this.stopKey = Keys.None;
    }

    /// <summary>
    /// Initializes a new instance of the KeyStopCondition class.
    /// Clone Constructor.
    /// </summary>
    /// <param name="cloneCondition">The <see cref="KeyStopCondition"/>
    /// to be cloned.</param>
    public KeyStopCondition(KeyStopCondition cloneCondition)
      : base(cloneCondition)
    {
      this.stopKey = cloneCondition.StopKey;
    }

    /// <summary>
    /// Initializes a new instance of the KeyStopCondition class.
    /// </summary>
    /// <param name="key">A <see cref="Keys"/> with the key 
    /// which should end the slide display.</param>
    /// <param name="anyKey"><strong>True</strong>, if any key can
    /// end the slide display, otherwise <strong>false</strong>.</param>
    /// <param name="newLinkTrialID">A nullable <see cref="int"/> with an optional trial ID
    /// to which this condition should link on click.</param>
    public KeyStopCondition(Keys key, bool anyKey, int? newLinkTrialID)
    {
      this.stopKey = key;
      this.CanBeAnyInputOfThisType = anyKey;
      this.TrialID = newLinkTrialID;
    }

    #endregion //CONSTRUCTION

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Properties                                                       //
    ///////////////////////////////////////////////////////////////////////////////
    #region PROPERTIES

    /// <summary>
    /// Gets or sets the key that should be pressed for stopping the slide display.
    /// </summary>
    /// <value> The <see cref="Keys"/> to listen for.</value>
    [Category("General")]
    [Description("The key that should be pressed for stopping the slide display.")]
    public Keys StopKey
    {
      get { return this.stopKey; }
      set { this.stopKey = value; }
    }

    #endregion //PROPERTIES

    ///////////////////////////////////////////////////////////////////////////////
    // Inherited methods                                                         //
    ///////////////////////////////////////////////////////////////////////////////
    #region OVERRIDES

    /// <summary>
    /// Overridden <see cref="Object.ToString()"/> method.
    /// Returns a <see cref="string"/> that represents 
    /// the current <see cref="KeyStopCondition"/>. 
    /// </summary>
    /// <returns>A <see cref="string"/> that represents 
    /// the current <see cref="KeyStopCondition"/></returns>
    public override string ToString()
    {
      StringBuilder sb = new StringBuilder();
      sb.Append("Key: ");
      if (this.CanBeAnyInputOfThisType)
      {
        sb.Append(" any key");
      }
      else
      {
        sb.Append(this.stopKey.ToString());
      }

      if (IsCorrectResponse.HasValue)
      {
        if (IsCorrectResponse.Value)
        {
          sb.Append(". Correct response to the slide");
        }
        else
        {
          sb.Append(". Wrong response to the slide");
        }
      }

      if (this.TrialID.HasValue)
      {
        sb.Append("- Links to Trial ID #");
        sb.Append(this.TrialID.Value);
      }

      return sb.ToString();
    }

    /// <summary>
    /// Determines whether two <see cref="KeyStopCondition"/> instances are equal.
    /// </summary>
    /// <param name="obj">The <see cref="Object"/> to compare with the current 
    /// <see cref="KeyStopCondition"/>.</param>
    /// <returns><strong>True</strong> if the specified Object is equal 
    /// to the current <see cref="KeyStopCondition"/>; otherwise, <strong>false</strong>. </returns>
    public override bool Equals(object obj)
    {
      if (obj is KeyStopCondition)
      {
        KeyStopCondition ksc = (KeyStopCondition)obj;
        if (ksc.stopKey == this.StopKey &&
          ksc.CanBeAnyInputOfThisType == this.CanBeAnyInputOfThisType)
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
    /// <returns>A hash code for the current <see cref="KeyStopCondition"/>. </returns>
    public override int GetHashCode()
    {
      return this.stopKey.GetHashCode() ^ this.CanBeAnyInputOfThisType.GetHashCode()
        ^ this.TrialID.GetHashCode();
    }

    /// <summary>
    /// Reset the current key stop condition to
    /// default values.
    /// </summary>
    public override void ResetValue()
    {
      base.ResetValue();
      this.stopKey = DEFAULTKEY;
    }

    /// <summary>
    /// Overridden <see cref="StopCondition.Clone()"/> method.
    /// Returns a <see cref="StopCondition"/> that is
    /// an exact copy of this <see cref="KeyStopCondition"/>. 
    /// </summary>
    /// <returns>A <see cref="StopCondition"/> that is
    /// an exact copy of this <see cref="KeyStopCondition"/>. </returns>
    protected override StopCondition CloneCore()
    {
      return new KeyStopCondition(this);
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
