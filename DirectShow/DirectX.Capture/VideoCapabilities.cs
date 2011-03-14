// ------------------------------------------------------------------
// DirectX.Capture
//
// History:
//	2003-Jan-24		BL		- created
//
// Copyright (c) 2003 Brian Low
// ------------------------------------------------------------------

using System;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using DirectShowLib;
using System.Collections.Generic;

namespace DirectX.Capture
{
  public struct VideoCapability
  {
    /// <summary> Native size of the incoming video signal. This is the largest signal the filter can digitize with every pixel remaining unique. Read-only. </summary>
    public Size InputSize;

    /// <summary> Minimum supported frame size. Read-only. </summary>
    public Size MinFrameSize;

    /// <summary> Maximum supported frame size. Read-only. </summary>
    public Size MaxFrameSize;

    /// <summary> Granularity of the output width. This value specifies the increments that are valid between MinFrameSize and MaxFrameSize. Read-only. </summary>
    public int FrameSizeGranularityX;

    /// <summary> Granularity of the output height. This value specifies the increments that are valid between MinFrameSize and MaxFrameSize. Read-only. </summary>
    public int FrameSizeGranularityY;

    /// <summary> Minimum supported frame rate. Read-only. </summary>
    public double MinFrameRate;

    /// <summary> Maximum supported frame rate. Read-only. </summary>
    public double MaxFrameRate;
  }

  /// <summary>
  ///  Capabilities of the video device such as 
  ///  min/max frame size and frame rate.
  /// </summary>
  public class VideoCapabilities
  {
    public List<VideoCapability> videoModes;
    // ----------------- Constructor ---------------------

    /// <summary> Retrieve capabilities of a video device </summary>
    internal VideoCapabilities(IAMStreamConfig videoStreamConfig)
    {
      if (videoStreamConfig == null)
        throw new ArgumentNullException("videoStreamConfig");

      this.videoModes = new List<VideoCapability>();

      AMMediaType mediaType = null;
      VideoStreamConfigCaps caps = null;
      IntPtr pCaps = IntPtr.Zero;
      try
      {
        // Ensure this device reports capabilities
        int c, size;
        int hr = videoStreamConfig.GetNumberOfCapabilities(out c, out size);
        if (hr != 0) Marshal.ThrowExceptionForHR(hr);
        if (c <= 0)
          throw new NotSupportedException("This video device does not report capabilities.");
        if (size > Marshal.SizeOf(typeof(VideoStreamConfigCaps)))
          throw new NotSupportedException("Unable to retrieve video device capabilities. This video device requires a larger VideoStreamConfigCaps structure.");

        for (int i = 0; i < c; i++)
        {
          // Alloc memory for structure
          pCaps = Marshal.AllocCoTaskMem(Marshal.SizeOf(typeof(VideoStreamConfigCaps)));

          // Retrieve first (and hopefully only) capabilities struct
          hr = videoStreamConfig.GetStreamCaps(i, out mediaType, pCaps);
          if (hr != 0) Marshal.ThrowExceptionForHR(hr);

          // Convert pointers to managed structures
          caps = (VideoStreamConfigCaps)Marshal.PtrToStructure(pCaps, typeof(VideoStreamConfigCaps));

          VideoCapability newCap = new VideoCapability();

          // Extract info
          newCap.InputSize = caps.InputSize;
          newCap.MinFrameSize = caps.MinOutputSize;
          newCap.MaxFrameSize = caps.MaxOutputSize;
          newCap.FrameSizeGranularityX = caps.OutputGranularityX;
          newCap.FrameSizeGranularityY = caps.OutputGranularityY;
          newCap.MinFrameRate = (double)10000000 / caps.MaxFrameInterval;
          newCap.MaxFrameRate = (double)10000000 / caps.MinFrameInterval;
          this.videoModes.Add(newCap);

          if (pCaps != IntPtr.Zero)
          {
            Marshal.FreeCoTaskMem(pCaps);
            pCaps = IntPtr.Zero;
          }
        }
      }
      finally
      {
        if (mediaType != null)
        {
          DsUtils.FreeAMMediaType(mediaType);
          mediaType = null;
        }
      }
    }
  }
}
