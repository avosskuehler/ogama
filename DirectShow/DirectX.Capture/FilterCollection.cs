// ------------------------------------------------------------------
// DirectX.Capture
//
// History:
//	2003-Jan-24		BL		- created
//
// Copyright (c) 2003 Brian Low
// ------------------------------------------------------------------

using System;
using System.Collections;
using System.Runtime.InteropServices;
using DirectShowLib;

namespace DirectX.Capture
{
  /// <summary>
  ///	 A collection of Filter objects (DirectShow filters).
  ///	 This is used by the <see cref="Capture"/> class to provide
  ///	 lists of capture devices and compression filters. This class
  ///	 cannot be created directly.
  /// </summary>
  public class FilterCollection : CollectionBase
  {
    /// <summary> Populate the collection with a list of filters from a particular category. </summary>
    internal FilterCollection(Guid category)
    {
      getFilters(category);
    }

    /// <summary> Populate the InnerList with a list of filters from a particular category </summary>
    protected void getFilters(Guid category)
    {
      try
      {
        DsDevice[] devices = DsDevice.GetDevicesOfCat(category);
        foreach (DsDevice device in devices)
        {
          // Add the filter
          Filter filter = new Filter(device.Mon);
          InnerList.Add(filter);
        }

        // Sort
        InnerList.Sort();
      }
      catch
      {
      }
    }

    /// <summary> Get the filter at the specified index. </summary>
    public Filter this[int index]
    {
      get { return ((Filter)InnerList[index]); }
    }
  }
}
