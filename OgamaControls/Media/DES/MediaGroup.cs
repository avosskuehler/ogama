using System;

using DirectShowLib.DES;
using DirectShowLib;
using System.Runtime.InteropServices;
using System.Collections;

namespace OgamaControls.Media
{
  /// <summary>
  /// Class containing information about the timeline groups (one
  /// for audio, one for video)
  /// </summary>
  internal class MediaGroup : IDisposable
  {
    #region Members

    /// <summary>
    /// Used to store the current length
    /// </summary>
    private long m_Length;

    /// <summary>
    /// An array holding the names and durations of the files to be processed
    /// </summary>
    private ArrayList m_Files;

    ///// <summary>
    ///// Pointer to the track
    ///// </summary>
    //private IAMTimelineTrack m_Track;

    /// <summary>
    /// FPS of the group
    /// </summary>
    private double m_FPS;

    /// <summary>
    /// Timeline to add items to
    /// </summary>
    private IAMTimeline m_pTimeline;

    private IAMTimelineGroup m_pGroup;

    #endregion

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="mType">Media type of the new group</param>
    /// <param name="pTimeline">Timeline to use for the group</param>
    /// <param name="fps">FPS for the group</param>
    public MediaGroup(AMMediaType mType, IAMTimeline pTimeline, double fps)
    {
      int hr;
      IAMTimelineObj pGroupObj;

      m_Length = 0;
      m_Files = new ArrayList();
      m_FPS = fps;
      m_pTimeline = pTimeline;

      // make the root group/composition
      hr = m_pTimeline.CreateEmptyNode(out pGroupObj, TimelineMajorType.Group);
      DESError.ThrowExceptionForHR(hr);

      try
      {
        m_pGroup = (IAMTimelineGroup)pGroupObj;

        // Set the media type we just created
        hr = m_pGroup.SetMediaType(mType);
        DESError.ThrowExceptionForHR(hr);
        DsUtils.FreeAMMediaType(mType);

        // add the video group to the timeline
        hr = m_pTimeline.AddGroup(pGroupObj);
        DESError.ThrowExceptionForHR(hr);
      }
      finally
      {
        //Marshal.ReleaseComObject(pGroupObj);
      }
      //Marshal.ReleaseComObject(pTrack1Obj);  // Released as m_VideoTrack in dispose
    }


    /// <summary>
    /// Returns the indexed file
    /// </summary>
    /// <param name="x">Zero based index into list of files in group</param>
    /// <returns>The specified MediaFile</returns>
    public MediaFile File(int x)
    {
      if (m_Files.Count > 0)
      {
        return (MediaFile)m_Files[x];
      }

      return null;
    }

    /// <summary>
    /// The count of files in the group
    /// </summary>
    /// <returns>The count</returns>
    public int Count
    {
      get
      {
        return m_Files.Count;
      }
    }

    /// <summary>
    /// Returns the length of the group in <see cref="DESCombine.UNITS"/>.
    /// </summary>
    public long Length
    {
      get
      {
        return m_Length;
      }
    }

    /// <summary>
    /// 
    /// </summary>
    public void RemoveTracks()
    {
      IAMTimelineComp pRootComp = (IAMTimelineComp)m_pGroup;

      IAMTimelineObj track;
      while (pRootComp.GetNextVTrack(null, out track) == 0)
      {
        track.RemoveAll();
        Marshal.ReleaseComObject(track);
      }
      m_Files.Clear();
      m_Length = 0;
    }

    /// <summary>
    /// Add a file to the group
    /// </summary>
    /// <param name="sName">File name+path to add</param>
    /// <param name="lStart">Start point in destination timeline in UNITS</param>
    public void Add(string sName, long lStart)
    {
      int hr;
      long lLength;
      long lEnd;

      // Create a mediafile object to hold the file
      MediaFile mf = new MediaFile(sName);

      // Add it to the list of files
      m_Files.Add(mf);

      // Find the real file length
      lLength = mf.Length;

      lEnd = lStart + lLength;

      // create the timeline source object
      IAMTimelineObj pSource1Obj;
      hr = m_pTimeline.CreateEmptyNode(out pSource1Obj, TimelineMajorType.Source);
      DESError.ThrowExceptionForHR(hr);

      // Create a track for the source
      IAMTimelineObj pTrack1Obj;
      hr = m_pTimeline.CreateEmptyNode(out pTrack1Obj, TimelineMajorType.Track);
      DESError.ThrowExceptionForHR(hr);

      try
      {
        // Set start and stop time of the file in the target timeline
        hr = pSource1Obj.SetStartStop(lStart, lEnd);
        DESError.ThrowExceptionForHR(hr);

        IAMTimelineSrc pSource1Src = (IAMTimelineSrc)pSource1Obj;

        // Set the file name
        hr = pSource1Src.SetMediaName(mf.FileName);
        DESError.ThrowExceptionForHR(hr);

        // Set the start/end in the source file itself.
        hr = pSource1Src.SetMediaTimes(0, lLength);
        DESError.ThrowExceptionForHR(hr);

        // tell the composition about the track
        IAMTimelineComp pRootComp = (IAMTimelineComp)m_pGroup;
        hr = pRootComp.VTrackInsBefore(pTrack1Obj, -1);
        DESError.ThrowExceptionForHR(hr);

        IAMTimelineTrack pTrack = (IAMTimelineTrack)pTrack1Obj;

        // Connect the track to the source
        hr = pTrack.SrcAdd(pSource1Obj);
        DESError.ThrowExceptionForHR(hr);

        // Set the times, get back the times adjusted to fit the frame rate
        hr = pSource1Obj.FixTimes(ref lStart, ref lEnd);
        DESError.ThrowExceptionForHR(hr);

        long lBegin = 0;

        // Set the times, get back the times adjusted to fit the frame rate
        hr = pSource1Src.FixMediaTimes(ref lBegin, ref lLength);
        DESError.ThrowExceptionForHR(hr);

        // Calculate the last frame number for the file
        double d1 = (lLength - lBegin);
        double d2 = (DESCombine.UNITS / m_FPS);
        double d3 = d1 / d2;
        int d4 = (int)Math.Round(d3);

        // Update the MediaFile (used to see when we've walked past
        // the end of a file)
        mf.LengthInFrames = d4;
      }
      finally
      {
        Marshal.ReleaseComObject(pSource1Obj);
        Marshal.ReleaseComObject(pTrack1Obj);
      }

      if (lEnd > m_Length)
      {
        m_Length = lEnd;
      }
    }

    /// <summary>
    /// Add a file to the group
    /// </summary>
    /// <param name="sName">File name+path to add</param>
    /// <param name="lStart">Start point in source file in UNITS</param>
    /// <param name="lEnd">End point in source file in UNITS or -1 to add entire file</param>
    public void Add(string sName, long lStart, long lEnd)
    {
      int hr;
      long lLength;

      // Create a mediafile object to hold the file
      MediaFile mf = new MediaFile(sName);

      // Add it to the list of files
      m_Files.Add(mf);

      // If the endpoint is -1, find the real file length
      if (lEnd < 0)
      {
        lEnd = mf.Length;
      }
      lLength = lEnd - lStart;

      // create the timeline source object
      IAMTimelineObj pSource1Obj;
      hr = m_pTimeline.CreateEmptyNode(out pSource1Obj, TimelineMajorType.Source);
      DESError.ThrowExceptionForHR(hr);

      // Create track
      IAMTimelineObj pTrack1Obj;
      hr = m_pTimeline.CreateEmptyNode(out pTrack1Obj, TimelineMajorType.Track);
      DESError.ThrowExceptionForHR(hr);

      try
      {
        // Set start and stop time of the file in the target timeline
        hr = pSource1Obj.SetStartStop(m_Length, lLength + m_Length);
        DESError.ThrowExceptionForHR(hr);

        IAMTimelineSrc pSource1Src = (IAMTimelineSrc)pSource1Obj;

        // Set the file name
        hr = pSource1Src.SetMediaName(mf.FileName);
        DESError.ThrowExceptionForHR(hr);

        // Set the start/end
        hr = pSource1Src.SetMediaTimes(lStart, lEnd);
        DESError.ThrowExceptionForHR(hr);


        // tell the composition about the track
        IAMTimelineComp pRootComp = (IAMTimelineComp)m_pGroup;
        hr = pRootComp.VTrackInsBefore(pTrack1Obj, -1);
        DESError.ThrowExceptionForHR(hr);

        IAMTimelineTrack pTrack = (IAMTimelineTrack)pTrack1Obj;

        // Connect the track to the source
        hr = pTrack.SrcAdd(pSource1Obj);
        DESError.ThrowExceptionForHR(hr);

        // Set the times, get back the times adjusted to fit the frame rate
        hr = pSource1Src.FixMediaTimes(ref lStart, ref lEnd);
        DESError.ThrowExceptionForHR(hr);

        // Calculate the last frame number for the file
        double d1 = (lEnd - lStart);
        double d2 = (DESCombine.UNITS / m_FPS);
        double d3 = d1 / d2;
        int d4 = (int)Math.Round(d3);

        // Update the MediaFile (used to see when we've walked past
        // the end of a file)
        mf.LengthInFrames = d4;
      }
      finally
      {
        Marshal.ReleaseComObject(pSource1Obj);
        Marshal.ReleaseComObject(pTrack1Obj);
      }

      m_Length += lLength;
    }

    #region IDisposable Members

    /// <summary>
    /// Release everything
    /// </summary>
    public void Dispose()
    {
      if (m_pGroup != null)
      {
        Marshal.ReleaseComObject(m_pGroup);
        m_pGroup = null;
      }
      m_Files = null;
      m_Length = 0;
      GC.SuppressFinalize(this);
    }

    #endregion
  }
}
