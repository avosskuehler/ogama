using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ogama.Modules.Recording.SMIInterface.RedM
{
  /// <summary>
  /// 
  /// </summary>
  public interface SmiRedmWrapperListener
  {
    /// <summary>
    /// Ons the sample data.
    /// </summary>
    /// <param name="data">The data.</param>
    void onSampleData(EyeTrackingController.SampleStruct data);
  }
}
