// <copyright file="ResponseChangedEventArgs.cs" company="FU Berlin">
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

namespace Ogama.Modules.Common.CustomEventArgs
{
  using System;

  using VectorGraphics.StopConditions;

  /// <summary>
  /// Delegate. Handles response changed event.
  /// </summary>
  /// <param name="sender">Source of the event.</param>
  /// <param name="e">A <see cref="ResponseChangedEventArgs"/> with the response
  /// as a <see cref="StopCondition"/> type.</param>
  public delegate void ResponseChangedEventHandler(object sender, ResponseChangedEventArgs e);

  /// <summary>
  /// Derived from <see cref="System.EventArgs"/>
  /// Class that contains the data for the response changed event. 
  /// </summary>
  public class ResponseChangedEventArgs : EventArgs
  {
    ///////////////////////////////////////////////////////////////////////////////
    // Defining Variables, Enumerations, Events                                  //
    ///////////////////////////////////////////////////////////////////////////////
    #region FIELDS
    /// <summary>
    /// The response as a <see cref="StopCondition"/> value.
    /// </summary>
    private readonly StopCondition response;

    #endregion //FIELDS

    ///////////////////////////////////////////////////////////////////////////////
    // Construction and Initializing methods                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region CONSTRUCTION

    /// <summary>
    /// Initializes a new instance of the ResponseChangedEventArgs class.
    /// </summary>
    /// <param name="newResponse">The response of type <see cref="StopCondition"/></param>
    public ResponseChangedEventArgs(StopCondition newResponse)
    {
      this.response = newResponse;
    }

    #endregion //CONSTRUCTION

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Properties                                                       //
    ///////////////////////////////////////////////////////////////////////////////
    #region PROPERTIES
    /// <summary>
    /// Gets the current response.
    /// </summary>
    /// <value>The response as a <see cref="StopCondition"/> value.</value>
    public StopCondition Response
    {
      get { return this.response; }
    }
    #endregion //PROPERTIES
  }
}
