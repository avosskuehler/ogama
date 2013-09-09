// <copyright file="NewRawDataAvailableEventArgs.cs" company="FU Berlin">
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

  using Ogama.Modules.ImportExport.Common;

  /// <summary>
  /// Delegate. Handles new raw data available event.
  /// </summary>
  /// <param name="sender">Source of the event.</param>
  /// <param name="e">A <see cref="NewRawDataAvailableEventArgs"/> with the raw sampling data.</param>
  public delegate void NewRawDataAvailableEventHandler(object sender, NewRawDataAvailableEventArgs e);

  /// <summary>
  /// Derived from <see cref="System.EventArgs"/>
  /// Class that contains the data for the new raw data available event. 
  /// </summary>
  public class NewRawDataAvailableEventArgs : EventArgs
  {
    ///////////////////////////////////////////////////////////////////////////////
    // Defining Variables, Enumerations, Events                                  //
    ///////////////////////////////////////////////////////////////////////////////
    #region FIELDS
    /// <summary>
    /// The new raw data row.
    /// </summary>
    private readonly RawData rawData;

    #endregion //FIELDS

    ///////////////////////////////////////////////////////////////////////////////
    // Construction and Initializing methods                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region CONSTRUCTION

    /// <summary>
    /// Initializes a new instance of the NewRawDataAvailableEventArgs class.
    /// </summary>
    /// <param name="newRawData">The raw data row as a 
    /// <see cref="DataSet.OgamaDataSet.RawdataRow"/> value.</param>
    public NewRawDataAvailableEventArgs(RawData newRawData)
    {
      this.rawData = newRawData;
    }

    #endregion //CONSTRUCTION

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Properties                                                       //
    ///////////////////////////////////////////////////////////////////////////////
    #region PROPERTIES
    /// <summary>
    /// Gets the new raw data.
    /// </summary>
    /// <value>The raw data as a <see cref="DataSet.OgamaDataSet.RawdataRow"/> value.</value>
    public RawData RawData
    {
      get { return this.rawData; }
    }
    #endregion //PROPERTIES
  }
}
