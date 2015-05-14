// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TheEyeTribeCalibrationPoints.cs" company="Freie Universität Berlin">
//   OGAMA - open gaze and mouse analyzer 
//   Copyright (C) 2015 Dr. Adrian Voßkühler  
//   Licensed under GPL V3
// </copyright>
// <author>Adrian Voßkühler</author>
// <email>adrian@ogama.net</email>
// <summary>
//   The different calibration point count values
//   of the eye tribe tracker
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Ogama.Modules.Recording.TheEyeTribeInterface
{
  /// <summary>
  ///   The different calibration point count values
  ///   of the eye tribe tracker
  /// </summary>
  public enum TheEyeTribeCalibrationPoints
  {
    /// <summary>
    ///   Indicates that the calibration should use 9 points
    /// </summary>
    Nine, 

    /// <summary>
    ///   Indicates that the calibration should use 12 points
    /// </summary>
    Twelve, 

    /// <summary>
    ///   Indicates that the calibration should use 16 points
    /// </summary>
    Sixteen
  }
}