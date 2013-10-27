using System;

using DirectShowLib.DES;
using System.Runtime.InteropServices;

namespace OgamaControls.Media
{
  /// <summary>
  /// This class wraps a file to be used by DESCombine.  In addition to
  /// holding the name, it also retrieves the media duration.
  /// </summary>
  internal class MediaFile
  {
    #region Data members

    // File name from the constructor
    private string m_FileName;

    // Actual media length (in 100NS) as reported by IMediaDet
    private long m_RealLength;

    // Amount of the real length to use when rendering
    private long m_UsingLength;

    // UsingLength reported in # of frames (only available
    // after the file has been added to the timeline)
    private int m_LengthInFrames;

    #endregion

    /// <summary>
    /// Constructor takes a file path+name
    /// </summary>
    /// <param name="s">File path+name</param>
    public MediaFile(string s)
    {
      m_FileName = s;
      m_RealLength = GetLength();
      m_UsingLength = m_RealLength;
      m_LengthInFrames = -1;
    }

    /// <summary>
    /// Return the length of the media file
    /// </summary>
    /// <returns>Length in 100NS</returns>
    private long GetLength()
    {
      int hr;
      double d;
      long i;

      IMediaDet imd = (IMediaDet)new MediaDet();

      // Set the name
      hr = imd.put_Filename(m_FileName);
      DESError.ThrowExceptionForHR(hr);

      // Read from stream zero
      hr = imd.put_CurrentStream(0);
      DESError.ThrowExceptionForHR(hr);

      // Get the length in seconds
      hr = imd.get_StreamLength(out d);
      DESError.ThrowExceptionForHR(hr);

      Marshal.ReleaseComObject(imd);

      // Convert to UNITS
      i = (long)(d * DESCombine.UNITS);
      return i;
    }

    /// <summary>
    /// Return or set the length of the media file.  When setting the
    /// value, you can set it from zero to the duration of the media file.
    /// If the duration of the media file is zero (jpg, bmp, etc), you can
    /// set the duration to any time.  Useful for intro, credits, etc.
    /// </summary>
    public long Length
    {
      get
      {
        return m_UsingLength;
      }
      set
      {
        if ((value < m_RealLength || m_RealLength == 0) && value >= 0)
        {
          m_UsingLength = value;
        }
        else
        {
          throw new Exception("Invalid length specified");
        }
      }
    }

    /// <summary>
    /// Return the file name
    /// </summary>
    public string FileName
    {
      get
      {
        return m_FileName;
      }
    }

    public int LengthInFrames
    {
      set
      {
        m_LengthInFrames = value;
      }
      get
      {
        return m_LengthInFrames;
      }
    }
  }
}
