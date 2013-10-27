// <copyright file="PinDef.cs" company="FU Berlin">
// ****************************************************************************
// While the underlying libraries are covered by LGPL, this sample is released
// as public domain.  It is distributed in the hope that it will be useful, but
// WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY
// or FITNESS FOR A PARTICULAR PURPOSE.
// *****************************************************************************/
// </copyright>

namespace DmoBase
{
  using DirectShowLib;

  /// <summary>
  /// Info regarding a (input or output) pin
  /// </summary>
  public struct PinDef
  {
    /// <summary>
    /// Is pin type set ?
    /// </summary>
    public bool TypeSet;

    /// <summary>
    /// Is pin incomplete ?
    /// </summary>
    public bool Incomplete;

    /// <summary>
    /// The <see cref="AMMediaType"/> of the pin
    /// </summary>
    public AMMediaType CurrentMediaType;
  }
}
