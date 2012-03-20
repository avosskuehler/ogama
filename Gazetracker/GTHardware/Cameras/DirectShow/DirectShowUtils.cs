using DirectShowLib;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace GTHardware.Cameras.DirectShow
{
  using System.Windows;

  using Size = System.Drawing.Size;

  /// <summary>
  /// This class encapsulates often needed static methods for direct show 
  /// interfaces like enumeration of devices and filters.
  /// </summary>
  public class DirectShowUtils
  {
    /// <summary>
    /// Enumerates all filters of the selected category and returns the IBaseFilter for the 
    /// filter described in friendlyname
    /// </summary>
    /// <param name="category">Category of the filter</param>
    /// <param name="friendlyname">Friendly name of the filter</param>
    /// <returns>IBaseFilter for the device</returns>
    public static IBaseFilter CreateFilter(Guid category, string friendlyname)
    {
      object source = null;
      try
      {
        Guid iid = typeof(IBaseFilter).GUID;
        foreach (DsDevice device in DsDevice.GetDevicesOfCat(category))
        {
          if (device.Name.CompareTo(friendlyname) == 0)
          {
            device.Mon.BindToObject(null, null, ref iid, out source);
            break;
          }
        }
      }
      catch (Exception ex)
      {
        MessageBox.Show(ex.Message);
      }

      return (IBaseFilter)source;
    }

    /// <summary>
    /// Displays a property page for a filter
    /// </summary>
    /// <param name="parentHandle">An <see cref="IntPtr"/> with the handle for the parent window of the dialog.</param>
    /// <param name="dev">The <see cref="IBaseFilter"/> for which to display a property page.</param>
    public static void DisplayPropertyPage(IntPtr parentHandle, IBaseFilter dev)
    {
      // Get the ISpecifyPropertyPages for the filter
      ISpecifyPropertyPages properties = dev as ISpecifyPropertyPages;
      int hr = 0;

      if (properties == null)
      {
        // If the filter doesn't implement ISpecifyPropertyPages, try displaying IAMVfwCompressDialogs instead!
        IAMVfwCompressDialogs compressDialog = dev as IAMVfwCompressDialogs;
        if (compressDialog != null)
        {
          hr = compressDialog.ShowDialog(VfwCompressDialogs.Config, IntPtr.Zero);
          DsError.ThrowExceptionForHR(hr);
        }

        return;
      }

      // Get the name of the filter from the FilterInfo struct
      FilterInfo filterInfo;
      hr = dev.QueryFilterInfo(out filterInfo);
      DsError.ThrowExceptionForHR(hr);

      // Get the propertypages from the property bag
      DsCAUUID captureGUID;
      hr = properties.GetPages(out captureGUID);
      DsError.ThrowExceptionForHR(hr);

      // Check for property pages on the output pin
      IPin pin = DsFindPin.ByDirection(dev, PinDirection.Output, 0);
      ISpecifyPropertyPages properties2 = pin as ISpecifyPropertyPages;
      if (properties2 != null)
      {
        DsCAUUID captureGUID2;
        hr = properties2.GetPages(out captureGUID2);
        DsError.ThrowExceptionForHR(hr);

        if (captureGUID2.cElems > 0)
        {
          int guidSize = Marshal.SizeOf(typeof(Guid));

          // Create a new buffer to hold all the GUIDs
          IntPtr p1 = Marshal.AllocCoTaskMem((captureGUID.cElems + captureGUID2.cElems) * guidSize);

          // Copy over the pages from the Filter
          for (int x = 0; x < captureGUID.cElems * guidSize; x++)
          {
            Marshal.WriteByte(p1, x, Marshal.ReadByte(captureGUID.pElems, x));
          }

          // Add the pages from the pin
          for (int x = 0; x < captureGUID2.cElems * guidSize; x++)
          {
            Marshal.WriteByte(p1, x + (captureGUID.cElems * guidSize), Marshal.ReadByte(captureGUID2.pElems, x));
          }

          // Release the old memory
          Marshal.FreeCoTaskMem(captureGUID.pElems);
          Marshal.FreeCoTaskMem(captureGUID2.pElems);

          // Reset caGUID to include both
          captureGUID.pElems = p1;
          captureGUID.cElems += captureGUID2.cElems;
        }
      }

      // Create and display the OlePropertyFrame
      object device = (object)dev;
      hr = Oleaut32.OleCreatePropertyFrame(parentHandle, 0, 0, filterInfo.achName, 1, ref device, captureGUID.cElems, captureGUID.pElems, 0, 0, IntPtr.Zero);
      DsError.ThrowExceptionForHR(hr);

      // Release COM objects
      Marshal.FreeCoTaskMem(captureGUID.pElems);
      Marshal.ReleaseComObject(properties);
      if (filterInfo.pGraph != null)
      {
        Marshal.ReleaseComObject(filterInfo.pGraph);
      }
    }

    /// <summary>
    /// Sets framerate and video size for the given <see cref="IBaseFilter"/>
    /// </summary>
    /// <param name="capGraph">A <see cref="ICaptureGraphBuilder2"/> with the capture graph.</param>
    /// <param name="capFilter">A <see cref="IBaseFilter"/> with the video device filter.</param>
    /// <param name="newFrameRate">An <see cref="int"/> with the new framerate.</param>
    /// <param name="newWidth">An <see cref="int"/> with the new video width.</param>
    /// <param name="newHeight">An <see cref="int"/> with the new video height.</param>
    public static void SetVideoConfigParms(
      ICaptureGraphBuilder2 capGraph,
      IBaseFilter capFilter,
      int newFrameRate,
      int newWidth,
      int newHeight)
    {
      int hr;
      object o;
      AMMediaType media;

      // Find the stream config interface
      hr = capGraph.FindInterface(
          PinCategory.Capture,
          MediaType.Video,
          capFilter,
          typeof(IAMStreamConfig).GUID,
          out o);

      IAMStreamConfig videoStreamConfig = o as IAMStreamConfig;
      if (videoStreamConfig == null)
      {
        throw new Exception("Failed to get IAMStreamConfig");
      }

      // Get the existing format block
      hr = videoStreamConfig.GetFormat(out media);
      DsError.ThrowExceptionForHR(hr);

      // copy out the videoinfoheader
      VideoInfoHeader v = new VideoInfoHeader();
      Marshal.PtrToStructure(media.formatPtr, v);

      // if overriding the framerate, set the frame rate
      if (newFrameRate > 0)
      {
        v.AvgTimePerFrame = 10000000 / newFrameRate;
      }

      // if overriding the width, set the width
      if (newWidth > 0)
      {
        v.BmiHeader.Width = newWidth;
      }

      // if overriding the Height, set the Height
      if (newHeight > 0)
      {
        v.BmiHeader.Height = newHeight;
      }

      // Copy the media structure back
      Marshal.StructureToPtr(v, media.formatPtr, false);

      // Set the new format
      hr = videoStreamConfig.SetFormat(media);
      DsError.ThrowExceptionForHR(hr);

      DsUtils.FreeAMMediaType(media);
      media = null;
    }

    /// <summary>
    /// This method returns the capabilities of the current selected video device.
    /// That is a list of video sizes and a list of frame rates.
    /// </summary>
    /// <param name="videoDevice">[in] A <see cref="IBaseFilter"/> thats properties should be received.</param>
    /// <param name="videoSizes">[out] A <see cref="List{Size}"/> with valid video sizes.</param>
    /// <param name="frameRates">[out] A <see cref="List{Int32}"/> with valid frame rates.</param>
    /// <returns><strong>True</strong>, if parsing was successfull, otherwise <strong>false</strong></returns>
    public static bool GetVideoCaps(IBaseFilter videoDevice, out List<Size> videoSizes, out List<int> frameRates)
    {
      int hr;
      object o;
      int pinCount;
      int pinSize;
      videoSizes = new List<Size>();
      frameRates = new List<int>();

      if (videoDevice == null)
      {
        return false;
      }

      // Create the Graph
      IGraphBuilder localGraphBuilder = (IGraphBuilder)new FilterGraph();

      // Create the Capture Graph Builder
      ICaptureGraphBuilder2 captureGraphBuilder = null;
      captureGraphBuilder = (ICaptureGraphBuilder2)new CaptureGraphBuilder2();

      try
      {
        // Attach the filter graph to the capture graph
        hr = captureGraphBuilder.SetFiltergraph(localGraphBuilder);
        DsError.ThrowExceptionForHR(hr);

        // Add the Video input device to the graph
        hr = localGraphBuilder.AddFilter(videoDevice, "video source filter");
        DsError.ThrowExceptionForHR(hr);

        // Find the stream config interface
        hr = captureGraphBuilder.FindInterface(
            PinCategory.Capture, MediaType.Video, videoDevice, typeof(IAMStreamConfig).GUID, out o);
        DsError.ThrowExceptionForHR(hr);

        IAMStreamConfig videoStreamConfig = o as IAMStreamConfig;
        if (videoStreamConfig == null)
        {
          throw new Exception("Failed to get IAMStreamConfig");
        }

        hr = videoStreamConfig.GetNumberOfCapabilities(out pinCount, out pinSize);
        DsError.ThrowExceptionForHR(hr);

        AMMediaType media;

        // copy out the videoinfoheader
        VideoStreamConfigCaps caps = new VideoStreamConfigCaps();
        IntPtr capsPtr = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(VideoStreamConfigCaps)));
        Marshal.StructureToPtr(caps, capsPtr, false);
        for (int i = 0; i < pinCount; i++)
        {
          hr = videoStreamConfig.GetStreamCaps(i, out media, capsPtr);
          DsError.ThrowExceptionForHR(hr);
          Marshal.PtrToStructure(capsPtr, caps);

          // Get valid framerates
          int maxRate = (int)(10000000f / caps.MinFrameInterval);
          int minRate = (int)(10000000f / caps.MaxFrameInterval);

          // Paranoia check for wrong intialized web cams
          // which don´t use nano second units, instead using real frame rates
          if (caps.MinFrameInterval < 100)
          {
            minRate = (int)caps.MinFrameInterval;
            maxRate = (int)caps.MaxFrameInterval;
          }

          for (int j = minRate; j <= maxRate; j++)
          {
            if (!frameRates.Contains(j))
            {
              frameRates.Add(j);
            }
          }

          // Get valid video sizes
          if (caps.MinOutputSize != caps.MaxOutputSize && caps.OutputGranularityX != 0)
          {
            int count = (caps.MaxOutputSize.Width - caps.MinOutputSize.Width) / caps.OutputGranularityX;
            for (int j = 0; j <= count; j++)
            {
              Size newSize = caps.MinOutputSize;
              newSize.Width += caps.OutputGranularityX * j;
              newSize.Height += caps.OutputGranularityY * j;
              if (!videoSizes.Contains(newSize))
              {
                videoSizes.Add(newSize);
              }
            }
          }
          else
          {
            if (!videoSizes.Contains(caps.MinOutputSize))
            {
              videoSizes.Add(caps.MinOutputSize);
            }
          }

          DsUtils.FreeAMMediaType(media);
        }
        Marshal.FreeHGlobal(capsPtr);
      }
      catch (Exception ex)
      {
        MessageBox.Show(ex.Message);
        return false;
      }
      finally
      {
        if (localGraphBuilder != null)
        {
          Marshal.ReleaseComObject(localGraphBuilder); 
          localGraphBuilder = null;
        } 
        
        if (captureGraphBuilder != null)
        {
          Marshal.ReleaseComObject(captureGraphBuilder); 
          captureGraphBuilder = null;
        }
      }

      return true;
    }

    /// <summary>
    /// Returns a <see cref="List{String}"/> with the friendly names of the video devices
    /// attached to the system. 
    /// </summary>
    /// <returns>A <see cref="List{String}"/> with the friendly names of the video devices.</returns>
    public static List<string> GetVideoInputDevices()
    {
      List<string> devices = new List<string>();

      // enumerate Video Input Devices
      foreach (DsDevice ds in DsDevice.GetDevicesOfCat(FilterCategory.VideoInputDevice))
      {
        devices.Add(ds.Name);
      }

      return devices;
    }

    /// <summary>
    /// Returns a <see cref="List{String}"/> with the friendly names of the audio devices
    /// attached to the system. 
    /// </summary>
    /// <returns>A <see cref="List{String}"/> with the friendly names of the audio devices.</returns>
    public static List<string> GetAudioInputDevices()
    {
      List<string> devices = new List<string>();

      // enumerate Audio Input Devices
      foreach (DsDevice ds in DsDevice.GetDevicesOfCat(FilterCategory.AudioInputDevice))
      {
        devices.Add(ds.Name);
      }

      return devices;
    }

    /// <summary>
    /// Returns a <see cref="List{String}"/> with the friendly names of the video compressors
    /// of the system. 
    /// </summary>
    /// <returns>A <see cref="List{String}"/> with the friendly names of the video compressors.</returns>
    public static List<string> GetVideoCompressors()
    {
      List<string> compressors = new List<string>();

      // enumerate Video Compressor filters 
      foreach (DsDevice ds in DsDevice.GetDevicesOfCat(FilterCategory.VideoCompressorCategory))
      {
        compressors.Add(ds.Name);
      }

      return compressors;
    }

    /// <summary>
    /// Returns a <see cref="List{String}"/> with the friendly names of the audio compressors
    /// of the system. 
    /// </summary>
    /// <returns>A <see cref="List{String}"/> with the friendly names of the audio compressors.</returns>
    public static List<string> GetAudioCompressors()
    {
      List<string> compressors = new List<string>();

      // enumerate Audio Compressor filters 
      foreach (DsDevice ds in DsDevice.GetDevicesOfCat(FilterCategory.AudioCompressorCategory))
      {
        compressors.Add(ds.Name);
      }

      return compressors;
    }
  }

  /// <summary>
  /// This static class exposes methods from the oleaut32.dll to C#.
  /// </summary>
  public static class Oleaut32
  {
    /// <summary>
    /// Invokes a new property frame, that is, a property sheet dialog box, whose parent is hwndOwner, where the dialog is positioned at the point (x,y) in the parent window and has the caption lpszCaption.
    /// </summary>
    /// <param name="hwndOwner">[in] Parent window of the resulting property sheet dialog box.</param>
    /// <param name="x">[in] Reserved. Horizontal position for the dialog box relative to hwndOwner.</param>
    /// <param name="y">[in] Reserved. Vertical position for the dialog box relative to hwndOwner.</param>
    /// <param name="lpszCaption">[in] Pointer to the string used for the caption of the dialog box.</param>
    /// <param name="cObjects">[in] Number of object pointers passed in lplpUnk.</param>
    /// <param name="ppUnk">[in] An array of IUnknown pointers on the objects for 
    /// which this property sheet is being invoked. The number of elements in the 
    /// array is specified by cObjects. These pointers are passed to each property 
    /// page through IPropertyPage::SetObjects.</param>
    /// <param name="cPages">[in] Number of property pages specified in lpPageCIsID.</param>
    /// <param name="lpPageClsID">[in] Array of size cPages containing the CLSIDs 
    /// of each property page to display in the property sheet.</param>
    /// <param name="lcid">[in] Locale identifier to use for the property sheet. 
    /// Property pages can retrieve this identifier through IPropertyPageSite::GetLocaleID.</param>
    /// <param name="dwReserved">[in] Reserved for future use; must be zero.</param>
    /// <param name="lpvReserved">[in] Reserved for future use; must be NULL.</param>
    /// <returns>This function supports the standard return values E_INVALIDARG, 
    /// E_OUTOFMEMORY, and E_UNEXPECTED, as well as the following: 
    /// S_OK The dialog box was invoked and operated successfully.
    /// E_POINTER The address in lpszCaption, lplpUnk, or lpPageCIsID is not valid. 
    /// For example, any one of them may be NULL.</returns>
    [DllImport(@"oleaut32.dll")]
    public static extern int OleCreatePropertyFrame(
      IntPtr hwndOwner,
      int x,
      int y,
      [MarshalAs(UnmanagedType.LPWStr)] string lpszCaption,
      int cObjects,
      [MarshalAs(UnmanagedType.Interface, ArraySubType = UnmanagedType.IUnknown)] 
			ref object ppUnk,
      int cPages,
      IntPtr lpPageClsID,
      int lcid,
      int dwReserved,
      IntPtr lpvReserved);
  }
}