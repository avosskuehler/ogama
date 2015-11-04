using System.Collections.Generic;

namespace OgamaControls
{
  /// <summary>
  /// Class. Implements the manual sorting of TimeLineMarker by index.
  /// </summary>
  public class TimeLineMarkerComparer : IComparer<TimeLineMarker>
  {
    /// <summary>
    /// Compares two objects and returns a value indicating 
    /// whether one is less than, equal to, or greater than the other.
    /// </summary>
    /// <param name="x">The first <see cref="object"/> to compare. </param>
    /// <param name="y">The second <see cref="object"/> to compare. </param>
    /// <returns>"Less than zero" if x is less than y. 
    /// "Zero" if x equals y. and "Greater than zero", if
    /// x is greater than y.</returns>
    public int Compare(TimeLineMarker x, TimeLineMarker y)
    {
      int xIndex = ((TimeLineMarker)x).Time;
      int yIndex = ((TimeLineMarker)y).Time;

      if (xIndex == yIndex)
      {
        return 0;
      }
      else if (xIndex > yIndex)
      {
        return 1;
      }
      else
      {
        return -1;
      }
    }
  }
}
