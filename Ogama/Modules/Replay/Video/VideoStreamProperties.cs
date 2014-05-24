// <copyright file="VideoStreamProperties.cs" company="FU Berlin">
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

namespace Ogama.Modules.Replay.Video
{
  using System.Drawing;

  /// <summary>
  /// This class encapsulates properties of a video stream that
  /// is used for video output of gaze mouse and user videos.
  /// </summary>
  public class VideoStreamProperties
  {
    /// <summary>
    /// Gets or sets the name of the stream.
    /// </summary>
    public string StreamName { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether this stream should
    /// be rendered (visible) or not.
    /// </summary>
    public bool IsStreamRendered { get; set; }

    /// <summary>
    /// Gets or sets the alpha (transparency) value for the stream
    /// </summary>
    public float StreamAlpha { get; set; }

    /// <summary>
    /// Gets or sets the position of the stream 
    /// positioned to the output size in values 
    /// ranging from 0.0 to 1.0-
    /// </summary>
    public RectangleF StreamPosition { get; set; }

    /// <summary>
    /// Gets or sets the time in milliseconds this stream should start.
    /// </summary>
    public long StreamStartTime { get; set; }

    /// <summary>
    /// Gets or sets the time in milliseconds this stream should be stopped.
    /// </summary>
    public long StreamEndTime { get; set; }

    /// <summary>
    /// Gets or sets a <see cref="Bitmap"/> with a screenshot of the first
    /// image of the stream.
    /// </summary>
    public Bitmap StreamScreenshot { get; set; }

    /// <summary>
    /// Gets or sets the streams <see cref="Size"/>.
    /// </summary>
    public Size StreamSize { get; set; }

    /// <summary>
    /// Gets or sets the filename with path to the video 
    /// file of this stream (if there is any)
    /// </summary>
    public string StreamFilename { get; set; }
  }
}
