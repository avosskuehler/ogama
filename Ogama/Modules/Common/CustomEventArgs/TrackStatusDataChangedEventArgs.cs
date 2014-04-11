// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TrackStatusDataChangedEventArgs.cs" company="Freie Universit�t Berlin">
//   OGAMA - open gaze and mouse analyzer 
//   Copyright (C) 2014 Dr. Adrian Vo�k�hler  
//   Licensed under GPL V3
// </copyright>
// <author>Adrian Vo�k�hler</author>
// <email>adrian@ogama.net</email>
// <summary>
//   Delegate. Handles track status changed event.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Ogama.Modules.Common.CustomEventArgs
{
  using System;

  using Ogama.Modules.Recording.TrackerBase;

  /// <summary>
  ///   Delegate. Handles track status changed event.
  /// </summary>
  /// <param name="sender">Source of the event.</param>
  /// <param name="e">A <see cref="TrackStatusDataChangedEventArgs" /> with the new track status.</param>
  public delegate void TrackStatusDataChangedEventHandler(object sender, TrackStatusDataChangedEventArgs e);

  /// <summary>
  ///   Derived from <see cref="System.EventArgs" />
  ///   Class that contains the data for the gaze data changed event.
  /// </summary>
  public class TrackStatusDataChangedEventArgs : EventArgs
  {
    #region Constructors and Destructors

    /// <summary>
    /// Initializes a new instance of the TrackStatusDataChangedEventArgs class.
    /// </summary>
    /// <param name="newTrackStatusData">
    /// The track status data as a <see cref="TrackStatusData"/> value.
    /// </param>
    public TrackStatusDataChangedEventArgs(TrackStatusData newTrackStatusData)
    {
      this.TrackStatusData = newTrackStatusData;
    }

    #endregion

    #region Public Properties

    /// <summary>
    ///   Gets the new track status data.
    /// </summary>
    /// <value>The track status data as a <see cref="TrackStatusData" /> value.</value>
    public TrackStatusData TrackStatusData { get; private set; }

    #endregion
  }
}