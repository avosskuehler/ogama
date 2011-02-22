// <copyright file="VideoStream.cs" company="FU Berlin">
// ****************************************************************************
// While the underlying libraries are covered by LGPL, this sample is released
// as public domain.  It is distributed in the hope that it will be useful, but
// WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY
// or FITNESS FOR A PARTICULAR PURPOSE.
// *****************************************************************************/
// </copyright>

namespace DmoMixer
{
  using System;
  using System.Drawing;
  using DirectShowLib.DMO;

  /// <summary>
  /// Encapsulates the properties of a video stream.
  /// </summary>
  public class VideoStream
  {
    ///////////////////////////////////////////////////////////////////////////////
    // Defining Constants                                                        //
    ///////////////////////////////////////////////////////////////////////////////
    #region CONSTANTS
    #endregion //CONSTANTS

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Variables, Enumerations, Events                                  //
    ///////////////////////////////////////////////////////////////////////////////
    #region FIELDS
    #endregion //FIELDS

    ///////////////////////////////////////////////////////////////////////////////
    // Construction and Initializing methods                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region CONSTRUCTION

    /// <summary>
    /// Initializes a new instance of the VideoStream class.
    /// </summary>
    public VideoStream()
    {
      this.StreamWidth = 0;
      this.StreamHeight = 0;
      this.StreamStride = 0;
      this.StreamBBP = 0;
      this.BufferTimeStamp = 0;
      this.BufferTimeLength = 0;
      this.BufferByteCount = 0;
      this.BufferPointer = IntPtr.Zero;
      this.Buffer = null;
      this.Position = new RectangleF(0, 0, 1, 1);
      this.Alpha = 0;
      this.FlipY = false;
    }

    #endregion //CONSTRUCTION

    ///////////////////////////////////////////////////////////////////////////////
    // Defining events, enums, delegates                                         //
    ///////////////////////////////////////////////////////////////////////////////
    #region EVENTS
    #endregion EVENTS

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Properties                                                       //
    ///////////////////////////////////////////////////////////////////////////////
    #region PROPERTIES

    /// <summary>
    /// Gets or sets the width of the video stream
    /// </summary>
    public int StreamWidth { get; set; }

    /// <summary>
    /// Gets or sets the height of the video stream
    /// </summary>
    public int StreamHeight { get; set; }

    /// <summary>
    /// Gets or sets the stride of the video stream
    /// </summary>
    public int StreamStride { get; set; }

    /// <summary>
    /// Gets or sets the bits per pixel of the video stream
    /// </summary>
    public int StreamBBP { get; set; }

    /// <summary>
    /// Gets or sets the time stamp of the current processed video buffer
    /// </summary>
    public long BufferTimeStamp { get; set; }

    /// <summary>
    /// Gets or sets the time length of the current processed video buffer
    /// </summary>
    public long BufferTimeLength { get; set; }

    /// <summary>
    /// Gets or sets the <see cref="IMediaBuffer"/> for the current processed video buffer
    /// </summary>
    public IMediaBuffer Buffer { get; set; }

    /// <summary>
    /// Gets or sets the pointer to the current processed video buffer
    /// </summary>
    public IntPtr BufferPointer { get; set; }

    /// <summary>
    /// Gets or sets the byte count of the current processed video buffer
    /// </summary>
    public int BufferByteCount { get; set; }

    /// <summary>
    /// Gets or sets the position of the video stream in relation to
    /// the output stream size (0-1f)
    /// </summary>
    public RectangleF Position { get; set; }

    /// <summary>
    /// Gets or sets the alpha value of the video stream.
    /// </summary>
    public float Alpha { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether to flip the video stream vertically
    /// </summary>
    public bool FlipY { get; set; }

    #endregion //PROPERTIES

    ///////////////////////////////////////////////////////////////////////////////
    // Public methods                                                            //
    ///////////////////////////////////////////////////////////////////////////////
    #region PUBLICMETHODS
    #endregion //PUBLICMETHODS

    ///////////////////////////////////////////////////////////////////////////////
    // Inherited methods                                                         //
    ///////////////////////////////////////////////////////////////////////////////
    #region OVERRIDES
    #endregion //OVERRIDES

    ///////////////////////////////////////////////////////////////////////////////
    // Eventhandler                                                              //
    ///////////////////////////////////////////////////////////////////////////////
    #region EVENTHANDLER
    #endregion //EVENTHANDLER

    ///////////////////////////////////////////////////////////////////////////////
    // Methods and Eventhandling for Background tasks                            //
    ///////////////////////////////////////////////////////////////////////////////
    #region THREAD
    #endregion //THREAD

    ///////////////////////////////////////////////////////////////////////////////
    // Methods for doing main class job                                          //
    ///////////////////////////////////////////////////////////////////////////////
    #region PRIVATEMETHODS
    #endregion //PRIVATEMETHODS

    ///////////////////////////////////////////////////////////////////////////////
    // Small helping Methods                                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region HELPER
    #endregion //HELPER
  }
}
