// <copyright file="CompletedEventArgs.cs" company="FU Berlin">
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

  using DirectShowLib;

  /// <summary>
  /// Derived from <see cref="System.EventArgs"/>
  /// Class that contains a <see cref="EventCode"/> in its arguments.
  /// </summary>
  public class CompletedEventArgs : EventArgs
  {
    ///////////////////////////////////////////////////////////////////////////////
    // Defining Variables, Enumerations, Events                                  //
    ///////////////////////////////////////////////////////////////////////////////
    #region FIELDS

    /// <summary>The result of the rendering</summary>
    /// <remarks>
    /// This code will be a member of DirectShowLib.EventCode.  Typically it 
    /// will be EventCode.Complete, EventCode.ErrorAbort or EventCode.UserAbort.
    /// </remarks>
    private readonly EventCode result;

    #endregion //FIELDS

    ///////////////////////////////////////////////////////////////////////////////
    // Construction and Initializing methods                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region CONSTRUCTION

    /// <summary>
    /// Initializes a new instance of the CompletedEventArgs class.
    /// </summary>
    /// <param name="ec">The <see cref="EventCode"/> that completed the task.</param>
    internal CompletedEventArgs(EventCode ec)
    {
      this.result = ec;
    }

    #endregion //CONSTRUCTION

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Properties                                                       //
    ///////////////////////////////////////////////////////////////////////////////
    #region PROPERTIES

    /// <summary>
    /// Gets the result of the rendering
    /// </summary>
    /// <remarks>
    /// This code will be a member of <see cref="DirectShowLib.EventCode"/>.  Typically it 
    /// will be EventCode.Complete, EventCode.ErrorAbort or EventCode.UserAbort.
    /// </remarks>
    /// <value>A <see cref="EventCode"/> that completed the task.</value>
    public EventCode Result
    {
      get { return this.result; }
    }

    #endregion //PROPERTIES
  }
}
