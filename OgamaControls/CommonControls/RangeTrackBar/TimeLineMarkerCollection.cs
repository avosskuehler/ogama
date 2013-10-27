using System.Collections.Generic;

namespace OgamaControls
{
  /// <summary>
  /// Collection of TimeLineMarkers.
  /// </summary>
  public class TimeLineMarkerCollection : List<TimeLineMarker>
  {
    /// <summary>
    /// Sorts the TimeLineMarker in the collection by their TimeLineMarker.Time value
    /// </summary>
    public new void Sort()
    {
      TimeLineMarkerComparer cp = new TimeLineMarkerComparer();
      base.Sort(cp);
    }
  }

}