// <copyright file="GTSettings.cs" company="ITU">
// ******************************************************
// GazeTrackingLibrary for ITU GazeTracker
// Copyright (C) 2010 Martin Tall  
// ------------------------------------------------------------------------
// This program is free software; you can redistribute it and/or modify it 
// under the terms of the GNU General Public License as published by the 
// Free Software Foundation; either version 3 of the License, 
// or (at your option) any later version.
// This program is distributed in the hope that it will be useful, 
// but WITHOUT ANY WARRANTY; without even the implied warranty of 
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU 
// General Public License for more details.
// You should have received a copy of the GNU General Public License 
// along with this program; if not, see http://www.gnu.org/licenses/.
// **************************************************************
// </copyright>
// <author>Martin Tall</author>
// <email>tall@stanford.edu</email>
// <modifiedby>Adrian Voßkühler</modifiedby>

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using DirectShowLib;
using GTHardware.Cameras.DirectShow;
//using GazeTrackingLibrary.Logging;

namespace GTHardware.Cameras.DirectShow
{
  /// <summary>
  /// This singleton class provides the available video capture devices
  /// of the system.
  /// </summary>
  public class DirectShowDevices
  {
    #region FIELDS

    /// <summary>
    /// The static instance of the singleton class.
    /// </summary>
    private static readonly DirectShowDevices instance = new DirectShowDevices();

    private List<CameraInfo> cameraDevices;

    private const int maxFps = 10000;
    private const int maxHeight = 10000;
    private const int maxWidth = 10000;
    private const int minFps = 1;
    private int minHeight = 0;
    private int minWidth = 0;
    private IntPtr pscc;

    #endregion //FIELDS

    #region CONSTRUCTION

    /// <summary>
    /// Prevents a default instance of the Devices class from being created.
    /// </summary>
    private DirectShowDevices()
    {
      cameraDevices = GetDevices();
    }

    #endregion //CONSTRUCTION

    #region PROPERTIES

    /// <summary>
    /// Gets the singleton instance of the devices class
    /// that contains the Instance devices.
    /// </summary>
    public static DirectShowDevices Instance
    {
      get { return instance; }
    }

    /// <summary>
    /// Gets the list of available capture devices
    /// </summary>
    public List<CameraInfo> Cameras
    {
      get { return cameraDevices; }
    }

    #endregion //PROPERTIES


    #region PRIVATEMETHODS

    /// <summary>
    /// Iterates through the list of available VideoInputDevice
    /// and fills CameraInfo if the device is not in use by another application.
    /// </summary>
    /// <returns>A <see cref="List{CameraInfo}"/> with the usable video capture devices.</returns>
    private List<CameraInfo> GetDevices()
    {
      cameraDevices = new List<CameraInfo>();

      foreach (DsDevice device in DsDevice.GetDevicesOfCat(FilterCategory.VideoInputDevice))
      {
        CameraInfo deviceInfo = Caps(device);

        if (deviceInfo != null)
        {
          deviceInfo.DirectshowDevice = device;
          cameraDevices.Add(deviceInfo);
        }
      }

      return cameraDevices;
    }

    /// <summary>
    /// Returns the <see cref="CameraInfo"/> for the given <see cref="DsDevice"/>.
    /// </summary>
    /// <param name="dev">A <see cref="DsDevice"/> to parse name and capabilities for.</param>
    /// <returns>The <see cref="CameraInfo"/> for the given device.</returns>
    private CameraInfo Caps(DsDevice dev)
    {
      var camerainfo = new CameraInfo();

      // Get the graphbuilder object
      var graphBuilder = (IFilterGraph2)new FilterGraph();

      // Get the ICaptureGraphBuilder2
      var capGraph = (ICaptureGraphBuilder2)new CaptureGraphBuilder2();

      IBaseFilter capFilter = null;

      try
      {
        int hr = capGraph.SetFiltergraph(graphBuilder);
        DsError.ThrowExceptionForHR(hr);

        // Add the video device
        hr = graphBuilder.AddSourceFilterForMoniker(dev.Mon, null, "Video input", out capFilter);
        //        DsError.ThrowExceptionForHR(hr);

        if (hr != 0)
        {
          Console.WriteLine("Error in m_graphBuilder.AddSourceFilterForMoniker(). Could not add source filter. Message: " + DsError.GetErrorText(hr));
          return null;
        }

        //hr = m_graphBuilder.AddFilter(capFilter, "Ds.NET Video Capture Device");
        //DsError.ThrowExceptionForHR(hr);

        object o = null;
        DsGuid cat = PinCategory.Capture;
        DsGuid type = MediaType.Interleaved;
        DsGuid iid = typeof(IAMStreamConfig).GUID;

        // Check if Video capture filter is in use
        hr = capGraph.RenderStream(cat, MediaType.Video, capFilter, null, null);
        if (hr != 0)
        {
          return null;
        }

        //hr = capGraph.FindInterface(PinCategory.Capture, MediaType.Interleaved, capFilter, typeof(IAMStreamConfig).GUID, out o);
        //if (hr != 0)
        //{
        hr = capGraph.FindInterface(PinCategory.Capture, MediaType.Video, capFilter,
                                    typeof(IAMStreamConfig).GUID, out o);
        DsError.ThrowExceptionForHR(hr);
        //}

        var videoStreamConfig = o as IAMStreamConfig;

        int iCount = 0;
        int iSize = 0;

        try
        {
          if (videoStreamConfig != null) videoStreamConfig.GetNumberOfCapabilities(out iCount, out iSize);
        }
        catch (Exception)
        {
          //ErrorLogger.ProcessException(ex, false);
          return null;
        }

        pscc = Marshal.AllocCoTaskMem(Marshal.SizeOf(typeof(VideoStreamConfigCaps)));

        camerainfo.Name = dev.Name;
        camerainfo.DirectshowDevice = dev;

        for (int i = 0; i < iCount; i++)     
        {
          VideoStreamConfigCaps scc;

          try
          {
            AMMediaType curMedType;
            if (videoStreamConfig != null) hr = videoStreamConfig.GetStreamCaps(i, out curMedType, pscc);
            Marshal.ThrowExceptionForHR(hr);
            scc = (VideoStreamConfigCaps)Marshal.PtrToStructure(pscc, typeof(VideoStreamConfigCaps));


            var CSF = new CamSizeFPS();
            CSF.FPS = (int)(10000000 / scc.MinFrameInterval);
            CSF.Height = scc.InputSize.Height;
            CSF.Width = scc.InputSize.Width;

            if (!InSizeFpsList(camerainfo.SupportedSizesAndFPS, CSF))
              if (ParametersOK(CSF))
                camerainfo.SupportedSizesAndFPS.Add(CSF);
          }
          catch (Exception)
          {
            //ErrorLogger.ProcessException(ex, false);
          }
        }

        Marshal.FreeCoTaskMem(pscc);
      }
      finally
      {
        if (graphBuilder != null)
        {
          Marshal.ReleaseComObject(graphBuilder);
        }
        if (capFilter != null)
        {
          Marshal.ReleaseComObject(capFilter);
        }
        if (capGraph != null)
        {
          Marshal.ReleaseComObject(capGraph);
        }
      }

      return camerainfo;
    }

    #endregion //PRIVATEMETHODS

    #region HELPER

    /// <summary>
    /// loop through the list of the differnet sizes and FPS
    /// </summary>
    /// <param name="lst"></param>
    /// <param name="instance"></param>
    /// <returns></returns>
    private static bool InSizeFpsList(List<CamSizeFPS> lst, CamSizeFPS instance)
    {
      foreach (CamSizeFPS c in lst)
        if (c.CompareTo(instance) == 0)
          return true;
      return false;
    }

    private bool ParametersOK(CamSizeFPS instance)
    {
      if (instance.FPS < minFps || instance.FPS > maxFps)
        return false;

      if (instance.Width < minWidth || instance.Width > maxWidth)
        return false;

      if (instance.Height < minHeight || instance.Height > maxHeight)
        return false;

      return true;
    }

    private int SupportedResolution(int deviceNumber, int width, int height)
    {
      int highestfps = 0;
      foreach (CamSizeFPS csf in cameraDevices[deviceNumber].SupportedSizesAndFPS)
      {
        if (csf.Height == height && csf.Width == width)
        {
          if (csf.FPS > highestfps)
            highestfps = csf.FPS;
        }
      }
      return highestfps;
    }

    private CameraInfo GetFirstAvailableCamera()
    {
      foreach (CameraInfo ci in cameraDevices)
      {
        if (ci.DirectshowDevice != null)
          return ci;
      }
      return null;
    }

    #endregion //HELPER
  }
}