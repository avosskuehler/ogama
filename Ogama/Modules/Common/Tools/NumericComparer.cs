// (c) Vasian Cepa 2005
// Version 2

using System.Collections;
using System.IO;

namespace Ogama.Modules.Common.Tools
{
  /// <summary>
  /// This class implements <see cref="IComparer"/> for a numeric file name comparison.
  /// </summary>
  public class NumericComparer : IComparer
  {
    /// <summary>
    /// Performs a case-sensitive comparison of two objects of the same type and returns a 
    /// value indicating whether one is less than, equal to, or greater than the other.
    /// </summary>
    /// <param name="x">The first object to compare. </param>
    /// <param name="y">The second object to compare. </param>
    /// <returns>A signed integer that indicates the relative values of x and y
    /// Less than zero if x is less than y. 
    /// Zero if x equals y. 
    /// Greater than zero if x is greater than y.
    /// </returns>
    public int Compare(object x, object y)
    {
      if ((x is string) && (y is string))
      {
        return StringLogicalComparer.Compare((string)x, (string)y);
      }

      if ((x is FileInfo) && (y is FileInfo))
      {
        return StringLogicalComparer.Compare(((FileInfo)x).Name, ((FileInfo)y).Name);
      }

      return -1;
    }
  }

}