// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NewRawDataAvailableEventArgs.cs" company="Freie Universität Berlin">
//   OGAMA - open gaze and mouse analyzer 
//   Copyright (C) 2014 Dr. Adrian Voßkühler  
//   Licensed under GPL V3
// </copyright>
// <author>Adrian Voßkühler</author>
// <email>adrian@ogama.net</email>
// <summary>
//   Delegate. Handles new raw data available event.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Ogama.Modules.Common.CustomEventArgs
{
  using System;

  using Ogama.Modules.ImportExport.Common;

  /// <summary>
  ///   Delegate. Handles new raw data available event.
  /// </summary>
  /// <param name="sender">Source of the event.</param>
  /// <param name="e">A <see cref="NewRawDataAvailableEventArgs" /> with the raw sampling data.</param>
  public delegate void NewRawDataAvailableEventHandler(object sender, NewRawDataAvailableEventArgs e);

  /// <summary>
  ///   Derived from <see cref="System.EventArgs" />
  ///   Class that contains the data for the new raw data available event.
  /// </summary>
  public class NewRawDataAvailableEventArgs : EventArgs
  {
    #region Constructors and Destructors

    /// <summary>
    /// Initializes a new instance of the NewRawDataAvailableEventArgs class.
    /// </summary>
    /// <param name="newRawData">
    /// The raw data row as a
    ///   <see cref="DataSet.OgamaDataSet.RawdataRow"/> value.
    /// </param>
    public NewRawDataAvailableEventArgs(RawData newRawData)
    {
      this.RawData = newRawData;
    }

    #endregion

    #region Public Properties

    /// <summary>
    ///   Gets the new raw data.
    /// </summary>
    /// <value>The raw data as a <see cref="DataSet.OgamaDataSet.RawdataRow" /> value.</value>
    public RawData RawData { get; private set; }

    #endregion
  }
}