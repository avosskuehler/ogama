using System;
using System.Runtime.InteropServices;
using System.Security;

namespace OgamaControls.Media
{
  /// <summary>
  /// 
  /// </summary>
  [ComImport, Guid("FA10746C-9B63-4b6c-BC49-FC300EA5F256")]
  public class EnhancedVideoRenderer
  {
  }

  /// <summary>
  /// 
  /// </summary>
  [ComImport, SuppressUnmanagedCodeSecurity,
  InterfaceType(ComInterfaceType.InterfaceIsIUnknown),
  Guid("83E91E85-82C1-4ea7-801D-85DC50B75086")]
  public interface IEVRFilterConfig
  {
    /// <summary>
    /// 
    /// </summary>
    /// <param name="dwMaxStreams"></param>
    /// <returns></returns>
    [PreserveSig]
    int SetNumberOfStreams(int dwMaxStreams);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="pdwMaxStreams"></param>
    [PreserveSig]
    void GetNumberOfStreams(out int pdwMaxStreams);
  }

  /// <summary>
  /// 
  /// </summary>
  [ComImport, SuppressUnmanagedCodeSecurity,
  Guid("DFDFD197-A9CA-43D8-B341-6AF3503792CD"),
  InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  public interface IMFVideoRenderer
  {
    /// <summary>
    /// 
    /// </summary>
    /// <param name="pVideoMixer"></param>
    /// <param name="pVideoPresenter"></param>
    /// <returns></returns>
    [PreserveSig]
    int InitializeRenderer([In, MarshalAs(UnmanagedType.Interface)] object pVideoMixer, [In, MarshalAs(UnmanagedType.Interface)] IMFVideoPresenter pVideoPresenter);
  }

  /// <summary>
  /// 
  /// </summary>
  [ComImport, SuppressUnmanagedCodeSecurity,
  InterfaceType(ComInterfaceType.InterfaceIsIUnknown),
  Guid("F6696E82-74F7-4F3D-A178-8A5E09C3659F")]
  public interface IMFClockStateSink
  {
    /// <summary>
    /// 
    /// </summary>
    /// <param name="hnsSystemTime"></param>
    /// <param name="llClockStartOffset"></param>
    /// <returns></returns>
    [PreserveSig]
    int OnClockStart([In] long hnsSystemTime, [In] long llClockStartOffset);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="hnsSystemTime"></param>
    /// <returns></returns>
    [PreserveSig]
    int OnClockStop([In] long hnsSystemTime);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="hnsSystemTime"></param>
    /// <returns></returns>
    [PreserveSig]
    int OnClockPause([In] long hnsSystemTime);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="hnsSystemTime"></param>
    /// <returns></returns>
    [PreserveSig]
    int OnClockRestart([In] long hnsSystemTime);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="hnsSystemTime"></param>
    /// <param name="flRate"></param>
    /// <returns></returns>
    [PreserveSig]
    int OnClockSetRate([In] long hnsSystemTime, [In] float flRate);
  }

  /// <summary>
  /// 
  /// </summary>
  [ComImport, SuppressUnmanagedCodeSecurity,
  InterfaceType(ComInterfaceType.InterfaceIsIUnknown),
  Guid("29AFF080-182A-4A5D-AF3B-448F3A6346CB")]
  public interface IMFVideoPresenter : IMFClockStateSink
  {
    #region IMFClockStateSink
    /// <summary>
    /// 
    /// </summary>
    /// <param name="hnsSystemTime"></param>
    /// <param name="llClockStartOffset"></param>
    [PreserveSig]
    new void OnClockStart([In] long hnsSystemTime, [In] long llClockStartOffset);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="hnsSystemTime"></param>
    [PreserveSig]
    new void OnClockStop([In] long hnsSystemTime);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="hnsSystemTime"></param>
    [PreserveSig]
    new void OnClockPause([In] long hnsSystemTime);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="hnsSystemTime"></param>
    [PreserveSig]
    new void OnClockRestart([In] long hnsSystemTime);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="hnsSystemTime"></param>
    /// <param name="flRate"></param>
    [PreserveSig]
    new void OnClockSetRate([In] long hnsSystemTime, [In] float flRate);
    #endregion

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    [PreserveSig]
    int ProcessMessage();

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    [PreserveSig]
    int GetCurrentMediaType();
  }

  /// <summary>
  /// 
  /// </summary>
  [ComImport, SuppressUnmanagedCodeSecurity,
  Guid("A490B1E4-AB84-4D31-A1B2-181E03B1077A"),
  InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  public interface IMFVideoDisplayControl
  {
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    [PreserveSig]
    int GetNativeVideoSize(/* not impl */);

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    [PreserveSig]
    int GetIdealVideoSize(/* not impl */);

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    [PreserveSig]
    int SetVideoPosition(/* not impl */);

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    [PreserveSig]
    int GetVideoPosition(/* not impl */);

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    [PreserveSig]
    int SetAspectRatioMode(/* not impl */);

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    [PreserveSig]
    int GetAspectRatioMode(/* not impl */);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="hwndVideo"></param>
    /// <returns></returns>
    [PreserveSig]
    int SetVideoWindow([In] IntPtr hwndVideo);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="phwndVideo"></param>
    /// <returns></returns>
    [PreserveSig]
    int GetVideoWindow(out IntPtr phwndVideo);
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    [PreserveSig]
    int RepaintVideo();
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    [PreserveSig]
    int GetCurrentImage(/* not impl */);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="Clr"></param>
    /// <returns></returns>
    [PreserveSig]
    int SetBorderColor([In] int Clr);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="pClr"></param>
    /// <returns></returns>
    [PreserveSig]
    int GetBorderColor(out int pClr);
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    [PreserveSig]
    int SetRenderingPrefs(/* not impl */);
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    [PreserveSig]
    int GetRenderingPrefs(/* not impl */);
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    [PreserveSig]
    int SetFullscreen(/* not impl */);
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    [PreserveSig]
    int GetFullscreen(/* not impl */);
  }

}
