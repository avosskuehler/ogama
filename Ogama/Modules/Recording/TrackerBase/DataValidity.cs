// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DataValidity.cs" company="Freie Universität Berlin">
//   OGAMA - open gaze and mouse analyzer 
//   Copyright (C) 2015 Dr. Adrian Voßkühler  
//   Licensed under GPL V3
// </copyright>
// <summary>
//   The validity of gaze samples indicates how to rely on the gaze data
//   that is delivered by the tracking device.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Ogama.Modules.Recording.TrackerBase
{
  /// <summary>
  /// The validity of gaze samples indicates how to rely on the gaze data
  /// that is delivered by the tracking device.
  /// </summary>
  public enum Validity
  {
    /// <summary>
    /// Gaze data could not be calculated
    /// </summary>
    Missing,

    /// <summary>
    /// One cannot rely heavily on the gaze data estimation.
    /// </summary>
    Problematic,

    /// <summary>
    /// The gaze data is calculated without a problem.
    /// </summary>
    Good,
  }
}
