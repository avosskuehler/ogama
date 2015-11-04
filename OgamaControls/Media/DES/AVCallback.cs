using System;

using DirectShowLib;
using System.Runtime.InteropServices;

namespace OgamaControls.Media
{
  /// <summary>
  /// Class used by both audio and video callback
  /// </summary>
  internal class AVCallback : ISampleGrabberCB
  {
    #region Data members

    /// <summary>
    /// Client callback routine
    /// </summary>
    protected IDESCombineCB m_pCallback;

    // The list of files
    protected MediaGroup m_Group;

    // The event sink (used to notify on end of file)
    protected IMediaEventSink m_pEventSink;

    // The event code to be used for end of file
    protected EventCode m_ec;

    // Holds the index into m_Files we are currently processing
    protected int m_iCurFile;

    // Which frame number we are currently processing
    protected int m_iCurFrame;

    // Maximum frame number for the current file
    protected int m_iMaxFrame;

    // File name of the currently processing file
    protected string m_CurFileName;

    #endregion

    /// <summary>
    /// 
    /// </summary>
    /// <param name="pGroup">Timeline group info</param>
    /// <param name="pCallback">Client callback</param>
    /// <param name="pEventSink">Event sync to call on file complete</param>
    /// <param name="ec">Event code to send on file completion</param>
    public AVCallback(
        MediaGroup pGroup,
        IDESCombineCB pCallback,
        IMediaEventSink pEventSink,
        EventCode ec
        )
    {
      m_pCallback = pCallback;
      m_Group = pGroup;
      m_pEventSink = pEventSink;
      m_ec = ec;

      m_iCurFrame = 0;
      m_iCurFile = 0;
      MediaFile mf = m_Group.File(m_iCurFile);
      if (mf != null)
      {
        m_CurFileName = mf.FileName;
        m_iMaxFrame = mf.LengthInFrames;
      }
      else
      {
        m_CurFileName = null;
        m_iMaxFrame = int.MaxValue;
      }
    }


    // ISampleGrabberCB methods
    public int SampleCB(double SampleTime, IMediaSample pSample)
    {
      Marshal.ReleaseComObject(pSample);
      return 0;
    }

    public int BufferCB(double SampleTime, System.IntPtr pBuffer, int BufferLen)
    {
      // Call the client
      int iRet;

      if (m_pCallback != null)
      {
        iRet = m_pCallback.BufferCB(m_CurFileName, SampleTime, pBuffer, BufferLen);
      }
      else
      {
        iRet = 0;
      }

      m_iCurFrame++;

      // Have we finished the current file?
      if (m_iCurFrame >= m_iMaxFrame)
      {
        // Send the notification
        int hr = m_pEventSink.Notify(m_ec, new IntPtr(m_iCurFile), new IntPtr(m_iCurFrame));

        // Find the next file
        m_iCurFile++;
        if (m_iCurFile < m_Group.Count)
        {
          MediaFile mf = m_Group.File(m_iCurFile);
          m_CurFileName = mf.FileName;
          m_iMaxFrame += mf.LengthInFrames;
        }
        else
        {
          // A failsafe
          m_iMaxFrame = int.MaxValue;
        }
      }

      return iRet;
    }
  }
}