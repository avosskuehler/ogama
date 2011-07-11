﻿// <copyright file="NavigatedStopCondition.cs" company="FU Berlin">
// ******************************************************
// OGAMA - open gaze and mouse analyzer 
// Copyright (C) 2010 Adrian Voßkühler  
// ------------------------------------------------------------------------
// This program is free software; you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation; either version 2 of the License, or (at your option) any later version.
// This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
// You should have received a copy of the GNU General Public License along with this program; if not, write to the Free Software Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA 02111-1307 USA
// **************************************************************
// </copyright>
// <author>Adrian Voßkühler</author>
// <email>adrian.vosskuehler@fu-berlin.de</email>

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
  /// This class stores a url of the navigated link
  /// </summary>
  [Serializable]
  public class NavigatedStopCondition : InputStopCondition
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
    /// The <see cref="Uri"/> to navigate to.
    /// </summary>
    private Uri link;

    #endregion //FIELDS

    ///////////////////////////////////////////////////////////////////////////////
    // Construction and Initializing methods                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region CONSTRUCTION

    /// <summary>
    /// Initializes a new instance of the KeyStopCondition class.
    /// </summary>
    public NavigatedStopCondition()
    {
      this.link = new Uri("about:blank");
    }

    /// <summary>
    /// Initializes a new instance of the NavigatedStopCondition class.
    /// Clone Constructor.
    /// </summary>
    /// <param name="cloneCondition">The <see cref="NavigatedStopCondition"/>
    /// to be cloned.</param>
    public NavigatedStopCondition(NavigatedStopCondition cloneCondition)
      : base(cloneCondition)
    {
      this.link = cloneCondition.Link;
    }

    /// <summary>
    /// Initializes a new instance of the NavigatedStopCondition class.
    /// </summary>
    /// <param name="newTargetUri">A <see cref="Uri"/> with the
    /// link to be navigated to.</param>
    public NavigatedStopCondition(Uri newTargetUri)
    {
      this.link = newTargetUri;
    }

    #endregion //CONSTRUCTION

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Properties                                                       //
    ///////////////////////////////////////////////////////////////////////////////
    #region PROPERTIES

    /// <summary>
    /// Gets or sets the link to navigate to.
    /// </summary>
    /// <value> The <see cref="Uri"/> that has been navigated to.</value>
    [Category("General")]
    [Description("The Url that has been navigated to.")]
    public Uri Link
    {
      get { return this.link; }
      set { this.link = value; }
    }

    #endregion //PROPERTIES

    ///////////////////////////////////////////////////////////////////////////////
    // Inherited methods                                                         //
    ///////////////////////////////////////////////////////////////////////////////
    #region OVERRIDES

    /// <summary>
    /// Overridden <see cref="Object.ToString()"/> method.
    /// Returns a <see cref="string"/> that represents 
    /// the current <see cref="NavigatedStopCondition"/>. 
    /// </summary>
    /// <returns>A <see cref="string"/> that represents 
    /// the current <see cref="NavigatedStopCondition"/></returns>
    public override string ToString()
    {
      StringBuilder sb = new StringBuilder();
      sb.Append("Uri: ");
      string text = this.link.ToString();
      sb.Append(text.Substring(0, text.Length > 12 ? Math.Max(12, text.Length - 1) : text.Length));
      return sb.ToString();
    }

    /// <summary>
    /// Determines whether two <see cref="NavigatedStopCondition"/> instances are equal.
    /// </summary>
    /// <param name="obj">The <see cref="Object"/> to compare with the current 
    /// <see cref="NavigatedStopCondition"/>.</param>
    /// <returns><strong>True</strong> if the specified Object is equal 
    /// to the current <see cref="KeyStopCondition"/>; otherwise, <strong>false</strong>. </returns>
    public override bool Equals(object obj)
    {
      if (obj is NavigatedStopCondition)
      {
        NavigatedStopCondition nsc = (NavigatedStopCondition)obj;
        if (nsc.link == this.Link)
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
    /// <returns>A hash code for the current <see cref="NavigatedStopCondition"/>. </returns>
    public override int GetHashCode()
    {
      return this.link.GetHashCode();
    }

    /// <summary>
    /// Reset the current navigated stop condition to
    /// default values.
    /// </summary>
    public override void ResetValue()
    {
      base.ResetValue();
      this.link = new Uri("about:blank");
    }

    /// <summary>
    /// Overridden <see cref="StopCondition.Clone()"/> method.
    /// Returns a <see cref="StopCondition"/> that is
    /// an exact copy of this <see cref="NavigatedStopCondition"/>. 
    /// </summary>
    /// <returns>A <see cref="StopCondition"/> that is
    /// an exact copy of this <see cref="NavigatedStopCondition"/>. </returns>
    protected override StopCondition CloneCore()
    {
      return new NavigatedStopCondition(this);
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
