// <copyright file="CaptureMode.cs" company="FU Berlin">
// Copyright (c) 2008 All Rights Reserved
// </copyright>
// <author>Adrian Voßkühler</author>
// <email>adrian@ogama.net</email>
namespace OgamaControls
{
  using System;

  /// <summary>
  /// The <strong>CaptureMode</strong> enumeration lists
  /// the available modes for capturing audio and video
  /// </summary>
  [Flags]
  public enum CaptureMode
  {
    /// <summary>
    /// No capturing at all.
    /// </summary>
    None = 0,

    /// <summary>
    /// Only preview of video stream.
    /// </summary>
    VideoPreview = 1,

    /// <summary>
    /// Capture the video stream.
    /// </summary>
    VideoCapture = 2,

    /// <summary>
    /// Preview and Capture the video stream
    /// </summary>
    Video = 3,

    /// <summary>
    /// Preview (Prehear) the audio stream.
    /// </summary>
    AudioPrehear = 4,

    /// <summary>
    /// Capture the audio stream.
    /// </summary>
    AudioCapture = 8,

    /// <summary>
    /// Prehear and capture the audio stream.
    /// </summary>
    Audio = 12,

    /// <summary>
    /// Preview video and audio stream.
    /// </summary>
    AudioVideoPreview = 5,

    /// <summary>
    /// Capture both video and audio stream into a file.
    /// </summary>
    AudioVideoCapture = 10,

    /// <summary>
    /// Preview and capture audio and video stream.
    /// </summary>
    AudioVideo = 15,
  }
}