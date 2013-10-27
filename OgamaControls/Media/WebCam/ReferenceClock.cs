namespace OgamaControls.Media.WebCam
{
  using System;
  using System.Runtime.InteropServices;
  using System.ComponentModel;
  using System.Threading;

  using DirectShowLib;

  /// <summary>
  /// This class implements an DirectShow <see cref="IReferenceClock"/>
  /// with the QueryPerformanceCounter and QueryPerformanceFrequency
  /// methods of the Kernel32.dll.
  /// </summary>
  public class CustomReferenceClock : IReferenceClock
  {
    #region Private Members

    [DllImport("Kernel32.dll")]
    private static extern bool QueryPerformanceCounter(out ulong lpPerformanceCount);

    [DllImport("Kernel32.dll", SetLastError = true)]
    private static extern bool QueryPerformanceFrequency(out ulong lpFrequency);

    [DllImport("Kernel32.DLL", CharSet = CharSet.Auto)]
    private static extern IntPtr OpenEvent(uint dwDesiredAccess, bool bInheritHandle, string lpName);

    [DllImport("Kernel32.DLL")]
    private static extern bool SetEvent(IntPtr hEvent);

    private bool _init = false;

    private ulong _lastTime;
    private ulong _freq;

    private long _currentTime;

    private static double _playRate;

    private const long TIME_FACTOR = 10000000;

    #endregion

    #region Constructors

    static CustomReferenceClock()
    {
      _playRate = 0d;
    }

    /// <summary>
    /// Initializes a new instance of the CustomReferenceClock class.
    /// </summary>
    public CustomReferenceClock()
    {
      if (QueryPerformanceFrequency(out _freq) == false)
      {
        throw new Win32Exception(); // timer not supported
      }

      _playRate = 0d;
      _init = false;
    }

    #endregion

    #region Properties

    /// <summary>
    /// Gets or sets the replay rate of the clock.
    /// </summary>
    public static double PlayRate
    {
      get
      {
        return _playRate;
      }
      set
      {
        if (value < 0)
          _playRate = 0;
        else
          _playRate = value;
      }
    }

    #endregion

    #region Get Private Time

    private long GetPrivateTime()
    {
      ulong time;

      if (_init == false)
      {
        _init = true;

        QueryPerformanceCounter(out time);

        _lastTime = time;
        _currentTime = 0;
      }
      else
      {
        QueryPerformanceCounter(out time);

        long timeDiff = (long)(time - _lastTime);

        _lastTime = time;

        double elapsedSeconds = (double)(timeDiff) / (double)_freq;

        elapsedSeconds = elapsedSeconds * PlayRate;

        _currentTime += (long)(elapsedSeconds * TIME_FACTOR);
      }

      return _currentTime;
    }

    #endregion

    #region IReferenceClock Members

    /// <summary>
    /// Returns the current time.
    /// </summary>
    /// <param name="pTime">Out. An <see cref="Int64"/> with the clocks time.</param>
    /// <returns>Always S_OK</returns>
    public int GetTime(out long pTime)
    {
      pTime = this.GetPrivateTime();

      return 0;
    }

    /// <summary>
    /// The AdviseTime method creates a one-shot advise request.
    /// </summary>
    /// <param name="baseTime">Base reference time, in 100-nanosecond units.</param>
    /// <param name="streamTime">Stream offset time, in 100-nanosecond units.</param>
    /// <param name="hEvent">Handle to an event, created by the caller.</param>
    /// <param name="pdwAdviseCookie">Pointer to a variable that receives 
    /// an identifier for the advise request.</param>
    /// <returns>Returns an HRESULT value. Possible values include the following.
    /// S_OK Success. 
    /// E_INVALIDARG Invalid time values. 
    /// E_OUTOFMEMORY Failure. 
    /// E_POINTER NULL pointer argument.</returns>
    /// <remarks>This method creates a one-shot advise request for the reference 
    /// time baseTime + streamTime. The sum must be greater than zero and 
    /// less than MAX_TIME, or the method returns E_INVALIDARG. 
    /// At the requested time, the clock signals the event specified in the hEvent parameter.
    /// To cancel the notification before the time is reached, call 
    /// the Unadvise method and pass the pdwAdviseToken value 
    /// returned from this call. After the notification has occurred, 
    /// the clock automatically clears it, so it is not necessary to call 
    /// Unadvise. However, it is not an error to do so.</remarks>
    public int AdviseTime(long baseTime, long streamTime, System.IntPtr hEvent, out int pdwAdviseCookie)
    {
      pdwAdviseCookie = 0;

      long refTime = baseTime + streamTime;

      if (PlayRate == 0d)
      {
        SetEvent(hEvent);
        return 0;
      }

      while (true)
      {
        long time = this.GetPrivateTime();

        if (time >= refTime)
        {
          SetEvent(hEvent);
          break;
        }

        Thread.Sleep(2);
      }

      return 0;
    }

    /// <summary>
    /// Not implemented. 
    /// The Unadvise method removes a pending advise request.
    /// </summary>
    /// <param name="dwAdviseCookie">Identifier of the request to remove. 
    /// Use the value returned by IReferenceClock::AdviseTime or 
    /// IReferenceClock::AdvisePeriodic in the pdwAdviseToken parameter.</param>
    /// <returns>Returns an HRESULT value. Possible values include the following.
    /// S_FALSE Not found. S_OK Success.</returns>
    public int Unadvise(int dwAdviseCookie)
    {
      // TODO: Add RefrenceClock.Unadvise implementation
      return 0;
    }

    /// <summary>
    /// Not implemented. 
    /// The AdvisePeriodic method creates a periodic advise request.
    /// </summary>
    /// <param name="startTime">Time of the first notification, 
    /// in 100-nanosecond units. Must be greater than zero and less than MAX_TIME</param>
    /// <param name="periodTime">Time between notifications, in 100-nanosecond units. Must be greater than zero</param>
    /// <param name="hSemaphore">Handle to a semaphore, created by the caller. </param>
    /// <param name="pdwAdviseCookie">Pointer to a variable that receives an identifier for the advise request.</param>
    /// <returns>Returns an HRESULT value. Possible values include the following.
    /// S_OK Success. 
    /// E_INVALIDARG Invalid time values. 
    /// E_OUTOFMEMORY Failure. 
    /// E_POINTER NULL pointer argument.</returns>
    public int AdvisePeriodic(long startTime, long periodTime, System.IntPtr hSemaphore, out int pdwAdviseCookie)
    {
      // TODO: Add RefrenceClock.AdvisePeriodic implementation
      pdwAdviseCookie = 0;
      return 0;
    }

    #endregion
  }
}