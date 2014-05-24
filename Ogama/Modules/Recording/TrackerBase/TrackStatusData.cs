// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TrackStatusData.cs" company="Freie Universität Berlin">
//   OGAMA - open gaze and mouse analyzer 
//   Copyright (C) 2014 Dr. Adrian Voßkühler  
//   Licensed under GPL V3
// </copyright>
// <author>Adrian Voßkühler</author>
// <email>adrian@ogama.net</email>
// <summary>
//   The track status data can store eye position and data validity for each eye
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Ogama.Modules.Recording.TrackerBase
{
  using System.Windows.Media.Media3D;

  /// <summary>
  ///   The track status data can store eye position and data validity for each eye
  /// </summary>
  public struct TrackStatusData
  {
    #region Fields

    /// <summary>
    ///   The left eye position in 3D space
    /// </summary>
    public Vector3D LeftEyePosition;

    /// <summary>
    ///   How much we can to rely on this values.
    /// </summary>
    public Validity LeftEyeValidity;

    /// <summary>
    ///   The right eye position in 3D space
    /// </summary>
    public Vector3D RightEyePosition;

    /// <summary>
    ///   How much we can to rely on this values.
    /// </summary>
    public Validity RightEyeValidity;

    /// <summary>
    ///   The eyes that are tracked.
    /// </summary>
    public Eye TrackedEyes;

    #endregion
  }
}