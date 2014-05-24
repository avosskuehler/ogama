// <copyright file="CaptureModeEventArgs.cs" company="FU Berlin">
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

namespace OgamaControls
{
  using System;

  /// <summary>
  /// Delegate. Handles capture mode changed event.
  /// </summary>
  /// <param name="sender">Source of the event.</param>
  /// <param name="e">A <see cref="CaptureModeEventArgs"/>
  /// with <see cref="CaptureMode"/> to send. </param>
  public delegate void CaptureModeEventHandler(object sender, CaptureModeEventArgs e);

  /// <summary>
  /// Derived from <see cref="System.EventArgs"/>
  /// Class that contains a <see cref="CaptureMode"/> in its arguments
  /// </summary>
  public class CaptureModeEventArgs : EventArgs
  {
    ///////////////////////////////////////////////////////////////////////////////
    // Defining Variables, Enumerations, Events                                  //
    ///////////////////////////////////////////////////////////////////////////////
    #region FIELDS
    /// <summary>
    /// A custom CaptureMode to send.
    /// </summary>
    private readonly CaptureMode parameter;

    #endregion //FIELDS

    ///////////////////////////////////////////////////////////////////////////////
    // Construction and Initializing methods                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region CONSTRUCTION

    /// <summary>
    /// Initializes a new instance of the CaptureModeEventArgs class.
    /// </summary>
    /// <param name="newParam">A <see cref="CaptureMode"/> with the CaptureMode to send.</param>
    public CaptureModeEventArgs(CaptureMode newParam)
    {
      this.parameter = newParam;
    }

    #endregion //CONSTRUCTION

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Properties                                                       //
    ///////////////////////////////////////////////////////////////////////////////
    #region PROPERTIES

    /// <summary>
    /// Gets the custom CaptureMode.
    /// </summary>
    /// <value>A <see cref="CaptureMode"/> to send to the listener.</value>
    public CaptureMode Param
    {
      get { return this.parameter; }
    }

    #endregion //PROPERTIES
  }
}
