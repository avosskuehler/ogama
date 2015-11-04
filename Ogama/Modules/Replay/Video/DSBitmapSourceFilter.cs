// <copyright file="DSBitmapSourceFilter.cs" company="FU Berlin">
// ******************************************************
// OGAMA - open gaze and mouse analyzer 
// Copyright (C) 2015 Dr. Adrian Voßkühler  
// ------------------------------------------------------------------------
// This program is free software; you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation; either version 2 of the License, or (at your option) any later version.
// This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
// You should have received a copy of the GNU General Public License along with this program; if not, write to the Free Software Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA 02111-1307 USA
// **************************************************************
// </copyright>
// <author>Adrian Voßkühler</author>
// <email>adrian@ogama.net</email>

namespace Ogama.Modules.Replay.Video
{
  using System;
  using System.Runtime.InteropServices;

  using DirectShowLib;

  /// <summary>
  /// ComImport for IGenericSampleCB.
  /// </summary>
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown),
  Guid("33B9EE57-1067-45fa-B12D-C37517F09FC0")]
  public interface IGenericSampleCB
  {
    /// <summary>
    /// Called by the GenericSampleSourceFilter.  This routine populates the MediaSample.
    /// </summary>
    /// <param name="sample">Pointer to a sample</param>
    /// <returns>0 = success, 1 = end of stream, negative values for errors</returns>
    [PreserveSig]
    int SampleCallback(IMediaSample sample);
  }

  /// <summary>
  /// ComImport for IGenericSampleConfig.
  /// </summary>
  [Guid("CE50FFF9-1BA8-4788-8131-BDE7D4FFC27F"),
  InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  public interface IGenericSampleConfig
  {
    /// <summary>
    /// SetMediaTypeFromBitmap: Populate the media type from a passed in bitmap.  
    /// Note that we need the FramesPerSecond to build the VIDEOINFOHEADER.  This
    /// is the entry point for IGenericSampleConfig::SetMediaTypeFromBitmap
    /// </summary>
    /// <param name="bmi">A <see cref="BitmapInfoHeader"/> with the media type</param>
    /// <param name="fps">A <see cref="Int64"/> with the frames per second.</param>
    /// <returns>0 = success, 1 = end of stream, negative values for errors</returns>
    [PreserveSig]
    int SetMediaTypeFromBitmap(BitmapInfoHeader bmi, long fps);

    /// <summary>
    /// The SetMediaType method specifies the media type for the connection 
    /// on the input pin of the Sample Grabber.
    /// </summary>
    /// <param name="amt">The <see cref="AMMediaType"/> structure specifies the required media type. 
    /// It is not necessary to set all the structure members.</param>
    /// <returns>0 = success, 1 = end of stream, negative values for errors</returns>
    [PreserveSig]
    int SetMediaType([MarshalAs(UnmanagedType.LPStruct)] AMMediaType amt);

    /// <summary>
    /// SetMediaTypeEx: Populate the media type from a completely 
    /// constructed media type.  
    /// If you aren't using a media type that I know how to get 
    /// the buffer size from, use this method and provide 
    /// it explicitly.  This is the entry point for 
    /// IGenericSampleConfig::SetMediaTypeEx
    /// </summary>
    /// <param name="amt">The <see cref="AMMediaType"/> to use.</param>
    /// <param name="bufferSize">An <see cref="Int32"/> with the buffer size.</param>
    /// <returns>0 = success, 1 = end of stream, negative values for errors.</returns>
    [PreserveSig]
    int SetMediaTypeEx([MarshalAs(UnmanagedType.LPStruct)] AMMediaType amt, int bufferSize);

    /// <summary>
    /// SetBitmapCB: Set the callback.  
    /// You must call one of the SetMediaType* methods first.  Notice
    /// that there is no check to ensure the CB wasn't already set.  While
    /// I haven't tried it, I suspect you could change this even while
    /// the graph is running.  Why you would want to is a more difficult question.
    /// </summary>
    /// <param name="pfn">An <see cref="IGenericSampleCB"/> function</param>
    /// <returns>0 = success, 1 = end of stream, negative values for errors</returns>
    [PreserveSig]
    int SetBitmapCB(IGenericSampleCB pfn);
  }

  /// <summary>
  /// ComImport for GenericSampleSourceFilter.
  /// </summary>
  [ComImport, Guid("6F7BCF72-D0C2-4449-BE0E-B12F580D056D")]
  public class GenericSampleSourceFilter
  {
  }
}
