// MediaInfoDLL - All info about media files, for DLL
// Copyright (C) 2002-2009 Jerome Martinez, Zen@MediaArea.net
//
// This library is free software; you can redistribute it and/or
// modify it under the terms of the GNU Lesser General Public
// License as published by the Free Software Foundation; either
// version 2.1 of the License, or (at your option) any later version.
//
// This library is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
// Lesser General Public License for more details.
//
// MediaInfoDLL - All info about media files, for DLL
// Copyright (C) 2002-2009 Jerome Martinez, Zen@MediaArea.net
//
// This library is free software; you can redistribute it and/or
// modify it under the terms of the GNU Lesser General Public
// License as published by the Free Software Foundation; either
// version 2.1 of the License, or (at your option) any later version.
//
// This library is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
// Lesser General Public License for more details.
//
// You should have received a copy of the GNU Lesser General Public
// License along with this library; if not, write to the Free Software
// Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA  02111-1307  USA
//
//+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
//+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
//
// Microsoft Visual C# wrapper for MediaInfo Library
// See MediaInfo.h for help
//
// To make it working, you must put MediaInfo.Dll
// in the executable folder
//
//+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

using System;
using System.Runtime.InteropServices;

namespace OgamaControls.Media
{
  /// <summary>
  /// Describes the kind of media stream
  /// </summary>
  public enum StreamKind
  {
    /// <summary>
    /// 
    /// </summary>
    General,
    /// <summary>
    /// 
    /// </summary>
    Video,
    /// <summary>
    /// 
    /// </summary>
    Audio,
    /// <summary>
    /// 
    /// </summary>
    Text,
    /// <summary>
    /// 
    /// </summary>
    Chapters,
    /// <summary>
    /// 
    /// </summary>
    Image
  }
  /// <summary>
  /// 
  /// </summary>
  public enum InfoKind
  {
    /// <summary>
    /// 
    /// </summary>
    Name,
    /// <summary>
    /// 
    /// </summary>
    Text,
    /// <summary>
    /// 
    /// </summary>
    Measure,
    /// <summary>
    /// 
    /// </summary>
    Options,
    /// <summary>
    /// 
    /// </summary>
    NameText,
    /// <summary>
    /// 
    /// </summary>
    MeasureText,
    /// <summary>
    /// 
    /// </summary>
    Info,
    /// <summary>
    /// 
    /// </summary>
    HowTo
  }

  /// <summary>
  /// 
  /// </summary>
  public enum InfoOptions
  {
    /// <summary>
    /// 
    /// </summary>
    ShowInInform,
    /// <summary>
    /// 
    /// </summary>
    Support,
    /// <summary>
    /// 
    /// </summary>
    ShowInSupported,
    /// <summary>
    /// 
    /// </summary>
    TypeOfValue
  }

  /// <summary>
  /// 
  /// </summary>
  public enum InfoFileOptions
  {
    /// <summary>
    /// 
    /// </summary>
    FileOption_Nothing = 0x00,
    /// <summary>
    /// 
    /// </summary>
    FileOption_Recursive = 0x01,
    /// <summary>
    /// 
    /// </summary>
    FileOption_CloseAll = 0x02,
    /// <summary>
    /// 
    /// </summary>
    FileOption_Max = 0x04
  };


  /// <summary>
  /// 
  /// </summary>
  public class MediaInfo
  {
    //Import of DLL functions. DO NOT USE until you know what you do (MediaInfo DLL do NOT use CoTaskMemAlloc to allocate memory)  
    [DllImport("MediaInfo.dll")]
    private static extern IntPtr MediaInfo_New();
    [DllImport("MediaInfo.dll")]
    private static extern void MediaInfo_Delete(IntPtr Handle);
    [DllImport("MediaInfo.dll")]
    private static extern IntPtr MediaInfo_Open(IntPtr Handle, [MarshalAs(UnmanagedType.LPWStr)] string FileName);
    [DllImport("MediaInfo.dll")]
    private static extern IntPtr MediaInfoA_Open(IntPtr Handle, IntPtr FileName);
    [DllImport("MediaInfo.dll")]
    private static extern IntPtr MediaInfo_Open_Buffer_Init(IntPtr Handle, Int64 File_Size, Int64 File_Offset);
    [DllImport("MediaInfo.dll")]
    private static extern IntPtr MediaInfoA_Open(IntPtr Handle, Int64 File_Size, Int64 File_Offset);
    [DllImport("MediaInfo.dll")]
    private static extern IntPtr MediaInfo_Open_Buffer_Continue(IntPtr Handle, IntPtr Buffer, IntPtr Buffer_Size);
    [DllImport("MediaInfo.dll")]
    private static extern IntPtr MediaInfoA_Open_Buffer_Continue(IntPtr Handle, Int64 File_Size, byte[] Buffer, IntPtr Buffer_Size);
    [DllImport("MediaInfo.dll")]
    private static extern Int64 MediaInfo_Open_Buffer_Continue_GoTo_Get(IntPtr Handle);
    [DllImport("MediaInfo.dll")]
    private static extern Int64 MediaInfoA_Open_Buffer_Continue_GoTo_Get(IntPtr Handle);
    [DllImport("MediaInfo.dll")]
    private static extern IntPtr MediaInfo_Open_Buffer_Finalize(IntPtr Handle);
    [DllImport("MediaInfo.dll")]
    private static extern IntPtr MediaInfoA_Open_Buffer_Finalize(IntPtr Handle);
    [DllImport("MediaInfo.dll")]
    private static extern void MediaInfo_Close(IntPtr Handle);
    [DllImport("MediaInfo.dll")]
    private static extern IntPtr MediaInfo_Inform(IntPtr Handle, IntPtr Reserved);
    [DllImport("MediaInfo.dll")]
    private static extern IntPtr MediaInfoA_Inform(IntPtr Handle, IntPtr Reserved);
    [DllImport("MediaInfo.dll")]
    private static extern IntPtr MediaInfo_GetI(IntPtr Handle, IntPtr StreamKind, IntPtr StreamNumber, IntPtr Parameter, IntPtr KindOfInfo);
    [DllImport("MediaInfo.dll")]
    private static extern IntPtr MediaInfoA_GetI(IntPtr Handle, IntPtr StreamKind, IntPtr StreamNumber, IntPtr Parameter, IntPtr KindOfInfo);
    [DllImport("MediaInfo.dll")]
    private static extern IntPtr MediaInfo_Get(IntPtr Handle, IntPtr StreamKind, IntPtr StreamNumber, [MarshalAs(UnmanagedType.LPWStr)] string Parameter, IntPtr KindOfInfo, IntPtr KindOfSearch);
    [DllImport("MediaInfo.dll")]
    private static extern IntPtr MediaInfoA_Get(IntPtr Handle, IntPtr StreamKind, IntPtr StreamNumber, IntPtr Parameter, IntPtr KindOfInfo, IntPtr KindOfSearch);
    [DllImport("MediaInfo.dll")]
    private static extern IntPtr MediaInfo_Option(IntPtr Handle, [MarshalAs(UnmanagedType.LPWStr)] string Option, [MarshalAs(UnmanagedType.LPWStr)] string Value);
    [DllImport("MediaInfo.dll")]
    private static extern IntPtr MediaInfoA_Option(IntPtr Handle, IntPtr Option, IntPtr Value);
    [DllImport("MediaInfo.dll")]
    private static extern IntPtr MediaInfo_State_Get(IntPtr Handle);
    [DllImport("MediaInfo.dll")]
    private static extern IntPtr MediaInfo_Count_Get(IntPtr Handle, IntPtr StreamKind, IntPtr StreamNumber);

    //MediaInfo class
    /// <summary>
    /// 
    /// </summary>
    public MediaInfo()
    {
      Handle = MediaInfo_New();
      if (Environment.OSVersion.ToString().IndexOf("Windows") == -1)
        MustUseAnsi = true;
      else
        MustUseAnsi = false;
    }
    /// <summary>
    /// 
    /// </summary>
    ~MediaInfo() { MediaInfo_Delete(Handle); }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="FileName"></param>
    /// <returns></returns>
    public int Open(String FileName)
    {
      if (MustUseAnsi)
      {
        IntPtr FileName_Ptr = Marshal.StringToHGlobalAnsi(FileName);
        int ToReturn = (int)MediaInfoA_Open(Handle, FileName_Ptr);
        Marshal.FreeHGlobal(FileName_Ptr);
        return ToReturn;
      }
      else
        return (int)MediaInfo_Open(Handle, FileName);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="File_Size"></param>
    /// <param name="File_Offset"></param>
    /// <returns></returns>
    public int Open_Buffer_Init(Int64 File_Size, Int64 File_Offset)
    {
      return (int)MediaInfo_Open_Buffer_Init(Handle, File_Size, File_Offset);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="Buffer"></param>
    /// <param name="Buffer_Size"></param>
    /// <returns></returns>
    public int Open_Buffer_Continue(IntPtr Buffer, IntPtr Buffer_Size)
    {
      return (int)MediaInfo_Open_Buffer_Continue(Handle, Buffer, Buffer_Size);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public Int64 Open_Buffer_Continue_GoTo_Get()
    {
      return (int)MediaInfo_Open_Buffer_Continue_GoTo_Get(Handle);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public int Open_Buffer_Finalize()
    {
      return (int)MediaInfo_Open_Buffer_Finalize(Handle);
    }

    /// <summary>
    /// 
    /// </summary>
    public void Close() { MediaInfo_Close(Handle); }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public String Inform()
    {
      if (MustUseAnsi)
        return Marshal.PtrToStringAnsi(MediaInfoA_Inform(Handle, (IntPtr)0));
      else
        return Marshal.PtrToStringUni(MediaInfo_Inform(Handle, (IntPtr)0));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="StreamKind"></param>
    /// <param name="StreamNumber"></param>
    /// <param name="Parameter"></param>
    /// <param name="KindOfInfo"></param>
    /// <param name="KindOfSearch"></param>
    /// <returns></returns>
    public String Get(StreamKind StreamKind, int StreamNumber, String Parameter, InfoKind KindOfInfo, InfoKind KindOfSearch)
    {
      if (MustUseAnsi)
      {
        IntPtr Parameter_Ptr = Marshal.StringToHGlobalAnsi(Parameter);
        String ToReturn = Marshal.PtrToStringAnsi(MediaInfoA_Get(Handle, (IntPtr)StreamKind, (IntPtr)StreamNumber, Parameter_Ptr, (IntPtr)KindOfInfo, (IntPtr)KindOfSearch));
        Marshal.FreeHGlobal(Parameter_Ptr);
        return ToReturn;
      }
      else
        return Marshal.PtrToStringUni(MediaInfo_Get(Handle, (IntPtr)StreamKind, (IntPtr)StreamNumber, Parameter, (IntPtr)KindOfInfo, (IntPtr)KindOfSearch));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="StreamKind"></param>
    /// <param name="StreamNumber"></param>
    /// <param name="Parameter"></param>
    /// <param name="KindOfInfo"></param>
    /// <returns></returns>
    public String Get(StreamKind StreamKind, int StreamNumber, int Parameter, InfoKind KindOfInfo)
    {
      if (MustUseAnsi)
        return Marshal.PtrToStringAnsi(MediaInfoA_GetI(Handle, (IntPtr)StreamKind, (IntPtr)StreamNumber, (IntPtr)Parameter, (IntPtr)KindOfInfo));
      else
        return Marshal.PtrToStringUni(MediaInfo_GetI(Handle, (IntPtr)StreamKind, (IntPtr)StreamNumber, (IntPtr)Parameter, (IntPtr)KindOfInfo));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="Option"></param>
    /// <param name="Value"></param>
    /// <returns></returns>
    public String Option(String Option, String Value)
    {
      if (MustUseAnsi)
      {
        IntPtr Option_Ptr = Marshal.StringToHGlobalAnsi(Option);
        IntPtr Value_Ptr = Marshal.StringToHGlobalAnsi(Value);
        String ToReturn = Marshal.PtrToStringAnsi(MediaInfoA_Option(Handle, Option_Ptr, Value_Ptr));
        Marshal.FreeHGlobal(Option_Ptr);
        Marshal.FreeHGlobal(Value_Ptr);
        return ToReturn;
      }
      else
        return Marshal.PtrToStringUni(MediaInfo_Option(Handle, Option, Value));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public int State_Get() { return (int)MediaInfo_State_Get(Handle); }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="StreamKind"></param>
    /// <param name="StreamNumber"></param>
    /// <returns></returns>
    public int Count_Get(StreamKind StreamKind, int StreamNumber) { return (int)MediaInfo_Count_Get(Handle, (IntPtr)StreamKind, (IntPtr)StreamNumber); }

    private IntPtr Handle;
    private bool MustUseAnsi;

    //Default values, if you know how to set default values in C#, say me
    /// <summary>
    /// 
    /// </summary>
    /// <param name="StreamKind"></param>
    /// <param name="StreamNumber"></param>
    /// <param name="Parameter"></param>
    /// <param name="KindOfInfo"></param>
    /// <returns></returns>
    public String Get(StreamKind StreamKind, int StreamNumber, String Parameter, InfoKind KindOfInfo) { return Get(StreamKind, StreamNumber, Parameter, KindOfInfo, InfoKind.Name); }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="StreamKind"></param>
    /// <param name="StreamNumber"></param>
    /// <param name="Parameter"></param>
    /// <returns></returns>
    public String Get(StreamKind StreamKind, int StreamNumber, String Parameter) { return Get(StreamKind, StreamNumber, Parameter, InfoKind.Text, InfoKind.Name); }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="StreamKind"></param>
    /// <param name="StreamNumber"></param>
    /// <param name="Parameter"></param>
    /// <returns></returns>
    public String Get(StreamKind StreamKind, int StreamNumber, int Parameter) { return Get(StreamKind, StreamNumber, Parameter, InfoKind.Text); }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="Option_"></param>
    /// <returns></returns>
    public String Option(String Option_) { return Option(Option_, ""); }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="StreamKind"></param>
    /// <returns></returns>
    public int Count_Get(StreamKind StreamKind) { return Count_Get(StreamKind, -1); }
  }

  /// <summary>
  /// 
  /// </summary>
  public class MediaInfoList
  {
    //Import of DLL functions. DO NOT USE until you know what you do (MediaInfo DLL do NOT use CoTaskMemAlloc to allocate memory)  
    [DllImport("MediaInfo.dll")]
    private static extern IntPtr MediaInfoList_New();
    [DllImport("MediaInfo.dll")]
    private static extern void MediaInfoList_Delete(IntPtr Handle);
    [DllImport("MediaInfo.dll")]
    private static extern IntPtr MediaInfoList_Open(IntPtr Handle, [MarshalAs(UnmanagedType.LPWStr)] string FileName, IntPtr Options);
    [DllImport("MediaInfo.dll")]
    private static extern void MediaInfoList_Close(IntPtr Handle, IntPtr FilePos);
    [DllImport("MediaInfo.dll")]
    private static extern IntPtr MediaInfoList_Inform(IntPtr Handle, IntPtr FilePos, IntPtr Reserved);
    [DllImport("MediaInfo.dll")]
    private static extern IntPtr MediaInfoList_GetI(IntPtr Handle, IntPtr FilePos, IntPtr StreamKind, IntPtr StreamNumber, IntPtr Parameter, IntPtr KindOfInfo);
    [DllImport("MediaInfo.dll")]
    private static extern IntPtr MediaInfoList_Get(IntPtr Handle, IntPtr FilePos, IntPtr StreamKind, IntPtr StreamNumber, [MarshalAs(UnmanagedType.LPWStr)] string Parameter, IntPtr KindOfInfo, IntPtr KindOfSearch);
    [DllImport("MediaInfo.dll")]
    private static extern IntPtr MediaInfoList_Option(IntPtr Handle, [MarshalAs(UnmanagedType.LPWStr)] string Option, [MarshalAs(UnmanagedType.LPWStr)] string Value);
    [DllImport("MediaInfo.dll")]
    private static extern IntPtr MediaInfoList_State_Get(IntPtr Handle);
    [DllImport("MediaInfo.dll")]
    private static extern IntPtr MediaInfoList_Count_Get(IntPtr Handle, IntPtr FilePos, IntPtr StreamKind, IntPtr StreamNumber);

    //MediaInfo class

    /// <summary>
    /// 
    /// </summary>
    public MediaInfoList() { Handle = MediaInfoList_New(); }

    /// <summary>
    /// 
    /// </summary>
    ~MediaInfoList() { MediaInfoList_Delete(Handle); }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="FileName"></param>
    /// <param name="Options"></param>
    /// <returns></returns>
    public int Open(String FileName, InfoFileOptions Options) { return (int)MediaInfoList_Open(Handle, FileName, (IntPtr)Options); }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="FilePos"></param>
    public void Close(int FilePos) { MediaInfoList_Close(Handle, (IntPtr)FilePos); }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="FilePos"></param>
    /// <returns></returns>
    public String Inform(int FilePos) { return Marshal.PtrToStringUni(MediaInfoList_Inform(Handle, (IntPtr)FilePos, (IntPtr)0)); }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="FilePos"></param>
    /// <param name="StreamKind"></param>
    /// <param name="StreamNumber"></param>
    /// <param name="Parameter"></param>
    /// <param name="KindOfInfo"></param>
    /// <param name="KindOfSearch"></param>
    /// <returns></returns>
    public String Get(int FilePos, StreamKind StreamKind, int StreamNumber, String Parameter, InfoKind KindOfInfo, InfoKind KindOfSearch) { return Marshal.PtrToStringUni(MediaInfoList_Get(Handle, (IntPtr)FilePos, (IntPtr)StreamKind, (IntPtr)StreamNumber, Parameter, (IntPtr)KindOfInfo, (IntPtr)KindOfSearch)); }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="FilePos"></param>
    /// <param name="StreamKind"></param>
    /// <param name="StreamNumber"></param>
    /// <param name="Parameter"></param>
    /// <param name="KindOfInfo"></param>
    /// <returns></returns>
    public String Get(int FilePos, StreamKind StreamKind, int StreamNumber, int Parameter, InfoKind KindOfInfo) { return Marshal.PtrToStringUni(MediaInfoList_GetI(Handle, (IntPtr)FilePos, (IntPtr)StreamKind, (IntPtr)StreamNumber, (IntPtr)Parameter, (IntPtr)KindOfInfo)); }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="Option"></param>
    /// <param name="Value"></param>
    /// <returns></returns>
    public String Option(String Option, String Value) { return Marshal.PtrToStringUni(MediaInfoList_Option(Handle, Option, Value)); }
    
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public int State_Get() { return (int)MediaInfoList_State_Get(Handle); }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="FilePos"></param>
    /// <param name="StreamKind"></param>
    /// <param name="StreamNumber"></param>
    /// <returns></returns>
    public int Count_Get(int FilePos, StreamKind StreamKind, int StreamNumber) { return (int)MediaInfoList_Count_Get(Handle, (IntPtr)FilePos, (IntPtr)StreamKind, (IntPtr)StreamNumber); }
    private IntPtr Handle;

    //Default values, if you know how to set default values in C#, say me

    /// <summary>
    /// 
    /// </summary>
    /// <param name="FileName"></param>
    public void Open(String FileName) { Open(FileName, 0); }

    /// <summary>
    /// 
    /// </summary>
    public void Close() { Close(-1); }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="FilePos"></param>
    /// <param name="StreamKind"></param>
    /// <param name="StreamNumber"></param>
    /// <param name="Parameter"></param>
    /// <param name="KindOfInfo"></param>
    /// <returns></returns>
    public String Get(int FilePos, StreamKind StreamKind, int StreamNumber, String Parameter, InfoKind KindOfInfo) { return Get(FilePos, StreamKind, StreamNumber, Parameter, KindOfInfo, InfoKind.Name); }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="FilePos"></param>
    /// <param name="StreamKind"></param>
    /// <param name="StreamNumber"></param>
    /// <param name="Parameter"></param>
    /// <returns></returns>
    public String Get(int FilePos, StreamKind StreamKind, int StreamNumber, String Parameter) { return Get(FilePos, StreamKind, StreamNumber, Parameter, InfoKind.Text, InfoKind.Name); }
   
    /// <summary>
    /// 
    /// </summary>
    /// <param name="FilePos"></param>
    /// <param name="StreamKind"></param>
    /// <param name="StreamNumber"></param>
    /// <param name="Parameter"></param>
    /// <returns></returns>
    public String Get(int FilePos, StreamKind StreamKind, int StreamNumber, int Parameter) { return Get(FilePos, StreamKind, StreamNumber, Parameter, InfoKind.Text); }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="Option_"></param>
    /// <returns></returns>
    public String Option(String Option_) { return Option(Option_, ""); }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="FilePos"></param>
    /// <param name="StreamKind"></param>
    /// <returns></returns>
    public int Count_Get(int FilePos, StreamKind StreamKind) { return Count_Get(FilePos, StreamKind, -1); }
  }

} //NameSpace

