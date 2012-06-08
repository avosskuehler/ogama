using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace OgamaControls
{
  /// <summary>
  /// Collection of selected nodes.
  /// </summary>
  public class TimeLineEventCollection : List<TimeLineEvent>
  {
    /// <summary>
    /// Sorts the TimeLineEvent in the collection by their TimeLineEvent.Time value
    /// </summary>
    public new void Sort()
    {
      TimeLineEventComparer cp = new TimeLineEventComparer();
      base.Sort(cp);
    }
  }

}