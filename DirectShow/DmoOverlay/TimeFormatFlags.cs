// <copyright file="TimeFormatFlags.cs" company="FU Berlin">
// ****************************************************************************
// While the underlying libraries are covered by LGPL, this sample is released
// as public domain.  It is distributed in the hope that it will be useful, but
// WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY
// or FITNESS FOR A PARTICULAR PURPOSE.
// *****************************************************************************/
// </copyright>

namespace MediaObjectTemplate
{
  using System;

  /// <summary>
  /// Used by the IMediaObjectImpl constructor to specify which timeformats are supported
  /// </summary>
  [Flags]
  public enum TimeFormatFlags
  {
    /// <summary>
    /// Used only when the DMO has no parameters
    /// </summary>
    None = 0,

    /// <summary>
    /// Reference time, in 100-nanosecond units.  All DMOs should support this format.
    /// </summary>
    Reference = 1,

    /// <summary>
    /// Music time, in parts per quarter note.
    /// </summary>
    Music = 2,

    /// <summary>
    /// Samples per second.
    /// </summary>
    Samples = 4,
  }
}
