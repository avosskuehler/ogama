// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Eyes.cs" company="Freie Universität Berlin">
//   OGAMA - open gaze and mouse analyzer 
//   Copyright (C) 2015 Dr. Adrian Voßkühler  
//   Licensed under GPL V3
// </copyright>
// <author>Adrian Voßkühler</author>
// <email>adrian@ogama.net</email>
// <summary>
//   The eyes that are tracked
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Ogama.Modules.Recording.TrackerBase
{
  using System;

  /// <summary>
  ///   The eyes that are tracked
  /// </summary>
  public enum Eye
  {
    /// <summary>
    ///   No eye is beeing tracked
    /// </summary>
    None, 

    /// <summary>
    ///   Only the left eye is beeing tracked
    /// </summary>
    Left, 

    /// <summary>
    ///   Only the right eye is beeing tracked
    /// </summary>
    Right, 

    /// <summary>
    /// Both eyes are beeing tracked
    /// </summary>
    Both
  }
}