// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HaythamStatus.cs" company="Freie Universität Berlin">
//   OGAMA - open gaze and mouse analyzer 
//   Copyright (C) 2014 Dr. Adrian Voßkühler  
//   Licensed under GPL V3
// </copyright>
// <author>Adrian Voßkühler</author>
// <email>adrian@ogama.net</email>
// <summary>
//   The different status values the connection to the haytham client
//   application can have.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Ogama.Modules.Recording.HaythamInterface
{
  using System;

  /// <summary>
  ///   The different status values the connection to the haytham client
  ///   application can have.
  /// </summary>
  [Flags]
  public enum HaythamStatus
  {
    /// <summary>
    ///   Indicates the calibration state of the tcp client.
    /// </summary>
    IsCalibrated = 1,

    /// <summary>
    ///   Indicates the connections status of the tcp client.
    /// </summary>
    IsConnected = 2,

    /// <summary>
    ///   Indicates the streaming state of the tcp client.
    /// </summary>
    IsStreaming = 4
  }
}