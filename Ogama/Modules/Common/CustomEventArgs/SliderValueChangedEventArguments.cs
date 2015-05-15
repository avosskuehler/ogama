// <copyright file="SliderValueChangedEventArguments.cs" company="FU Berlin">
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
  /// Derived from <see cref="System.EventArgs"/>
  /// Class that contains the data for the slider value changed event.
  /// </summary>
  public class SliderValueChangedEventArguments : EventArgs
  {
    ///////////////////////////////////////////////////////////////////////////////
    // Defining Variables, Enumerations, Events                                  //
    ///////////////////////////////////////////////////////////////////////////////
    #region FIELDS

    /// <summary>
    /// Saves the current timing position in ms.
    /// </summary>
    private readonly int currentMillisecond;

    #endregion //FIELDS

    ///////////////////////////////////////////////////////////////////////////////
    // Construction and Initializing methods                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region CONSTRUCTION

    /// <summary>
    /// Initializes a new instance of the SliderValueChangedEventArguments class.
    /// </summary>
    /// <param name="newCurrentMillisecond">An <see cref="int"/> containing 
    /// current timing position in ms.</param>
    public SliderValueChangedEventArguments(int newCurrentMillisecond)
    {
      this.currentMillisecond = newCurrentMillisecond;
    }

    #endregion //CONSTRUCTION

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Properties                                                       //
    ///////////////////////////////////////////////////////////////////////////////
    #region PROPERTIES

    /// <summary>
    /// Gets a string containing current timing position in ms
    /// </summary>
    /// <value>An <see cref="int"/> with the
    /// timing position in ms when the slider value changed event 
    /// is generated.</value>
    public int Millisecond
    {
      get { return this.currentMillisecond; }
    }

    #endregion //PROPERTIES
  }
}
