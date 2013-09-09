// <copyright file="MultimediaTimer.cs" company="FU Berlin">
// ******************************************************
// OGAMA - open gaze and mouse analyzer 
// Copyright (C) 2013 Dr. Adrian Voßkühler  
// ------------------------------------------------------------------------
// This program is free software; you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation; either version 2 of the License, or (at your option) any later version.
// This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
// You should have received a copy of the GNU General Public License along with this program; if not, write to the Free Software Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA 02111-1307 USA
// **************************************************************
// </copyright>
// <author>Adrian Voßkühler</author>
// <email>adrian@ogama.net</email>

#region License

/* Copyright (c) 2006 Leslie Sanford
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy 
 * of this software and associated documentation files (the "Software"), to 
 * deal in the Software without restriction, including without limitation the 
 * rights to use, copy, modify, merge, publish, distribute, sublicense, and/or 
 * sell copies of the Software, and to permit persons to whom the Software is 
 * furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in 
 * all copies or substantial portions of the Software. 
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR 
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, 
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE 
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER 
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, 
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN 
 * THE SOFTWARE.
 */

#endregion

#region Contact

/*
 * Leslie Sanford
 * Email: jabberdabber@hotmail.com
 */

#endregion

namespace VectorGraphics.Controls.Timer
{
  using System;
  using System.ComponentModel;
  using System.Diagnostics;
  using System.Drawing;
  using System.Runtime.InteropServices;
  using System.Windows.Forms;

  /// <summary>
  /// Represents the Windows multimedia timer.
  /// </summary>
  [ToolboxBitmap(typeof(Timer))]
  public sealed class MultimediaTimer : IComponent
  {
    ///////////////////////////////////////////////////////////////////////////////
    // Defining Constants                                                        //
    ///////////////////////////////////////////////////////////////////////////////
    #region CONSTANTS

    /// <summary>
    /// Indicates that the operation was successful.
    /// </summary>
    private const int TIMERRNOERROR = 0;

    #endregion //CONSTANTS

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Variables, Enumerations, Events                                  //
    ///////////////////////////////////////////////////////////////////////////////
    #region FIELDS

    /// <summary>
    /// Multimedia timer capabilities.
    /// </summary>
    private static TimerCaps caps;

    /// <summary>
    /// Timer identifier.
    /// </summary>
    private int timerID;

    /// <summary>
    /// Timer mode.
    /// </summary>
    private volatile TimerMode mode;

    /// <summary>
    /// Period between timer events in milliseconds.
    /// </summary>
    private volatile int period;

    /// <summary>
    /// Timer resolution in milliseconds.
    /// </summary>
    private volatile int resolution;

    /// <summary>
    /// Called by Windows when a timer periodic event occurs.
    /// </summary>
    private TimeProc timeProcPeriodic;

    /// <summary>
    /// Called by Windows when a timer one shot event occurs.
    /// </summary>
    private TimeProc timeProcOneShot;

    /// <summary>
    /// Represents the method that raises the Tick event.
    /// </summary>
    private EventRaiser tickRaiser;

    /// <summary>
    /// Indicates whether or not the timer is running.
    /// </summary>
    private bool running = false;

    /// <summary>
    /// Indicates whether or not the timer has been disposed.
    /// </summary>
    private volatile bool disposed = false;

    /// <summary>
    /// The ISynchronizeInvoke object to use for marshaling events.
    /// </summary>
    private ISynchronizeInvoke synchronizingObject = null;

    /// <summary>
    /// For implementing IComponent.
    /// </summary>
    private ISite site = null;

    #endregion //FIELDS

    ///////////////////////////////////////////////////////////////////////////////
    // Construction and Initializing methods                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region CONSTRUCTION

    /// <summary>
    /// Initializes static members of the MultimediaTimer class.
    /// </summary>
    static MultimediaTimer()
    {
      // Get multimedia timer capabilities.
      timeGetDevCaps(ref caps, Marshal.SizeOf(caps));
    }

    /// <summary>
    /// Initializes a new instance of the MultimediaTimer class with the specified IContainer.
    /// </summary>
    /// <param name="container">
    /// The IContainer to which the Timer will add itself.
    /// </param>
    public MultimediaTimer(IContainer container)
    {
      // Required for Windows.Forms Class Composition Designer support
      container.Add(this);

      this.Initialize();
    }

    /// <summary>
    /// Initializes a new instance of the MultimediaTimer class.
    /// </summary>
    public MultimediaTimer()
    {
      this.Initialize();
    }

    /// <summary>
    /// Finalizes an instance of the MultimediaTimer class.
    /// </summary>
    ~MultimediaTimer()
    {
      if (this.IsRunning)
      {
        // Stop and destroy timer.
        timeKillEvent(this.timerID);
      }
    }

    #endregion //CONSTRUCTION

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Enumerations                                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region ENUMS

    /// <summary>
    /// Represents the method that is called by Windows when a timer event occurs.
    /// </summary>
    /// <param name="id">Timer identifier</param>
    /// <param name="msg">Message to send</param>
    /// <param name="user">User parameter</param>
    /// <param name="param1">Parameter One</param>
    /// <param name="param2">Parameter Two</param>
    private delegate void TimeProc(int id, int msg, int user, int param1, int param2);

    /// <summary>
    /// Represents methods that raise events.
    /// </summary>
    /// <param name="e">An <see cref="EventArgs"/> parameter.</param>
    private delegate void EventRaiser(EventArgs e);

    /// <summary>
    /// Occurs when the Timer has started;
    /// </summary>
    public event EventHandler Started;

    /// <summary>
    /// Occurs when the Timer has stopped;
    /// </summary>
    public event EventHandler Stopped;

    /// <summary>
    /// Occurs when the time period has elapsed.
    /// </summary>
    public event EventHandler Tick;

    /// <summary>
    /// Occurs when the timer is disposed.
    /// </summary>
    public event EventHandler Disposed;

    #endregion ENUMS

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Properties                                                       //
    ///////////////////////////////////////////////////////////////////////////////
    #region PROPERTIES

    /// <summary>
    /// Gets the timer capabilities.
    /// </summary>
    public static TimerCaps Capabilities
    {
      get { return caps; }
    }

    /// <summary>
    /// Gets or sets the object used to marshal event-handler calls.
    /// </summary>
    public ISynchronizeInvoke SynchronizingObject
    {
      get
      {
        if (this.disposed)
        {
          throw new ObjectDisposedException("Timer");
        }

        return this.synchronizingObject;
      }

      set
      {
        if (this.disposed)
        {
          throw new ObjectDisposedException("Timer");
        }

        this.synchronizingObject = value;
      }
    }

    /// <summary>
    /// Gets or sets the time between Tick events.
    /// </summary>
    /// <exception cref="ObjectDisposedException">
    /// If the timer has already been disposed.
    /// </exception>   
    public int Period
    {
      get
      {
        if (this.disposed)
        {
          throw new ObjectDisposedException("Timer");
        }

        return this.period;
      }

      set
      {
        if (this.disposed)
        {
          throw new ObjectDisposedException("Timer");
        }
        else if (value < Capabilities.PeriodMin || value > Capabilities.PeriodMax)
        {
          throw new ArgumentOutOfRangeException(
            "Period", 
            value,
            "Multimedia Timer period out of range.");
        }

        this.period = value;

        if (this.IsRunning)
        {
          this.Stop();
          this.Start();
        }
      }
    }

    /// <summary>
    /// Gets or sets the timer resolution.
    /// </summary>
    /// <exception cref="ObjectDisposedException">
    /// If the timer has already been disposed.
    /// </exception>        
    /// <remarks>
    /// The resolution is in milliseconds. The resolution increases 
    /// with smaller values; a resolution of 0 indicates periodic events 
    /// should occur with the greatest possible accuracy. To reduce system 
    /// overhead, however, you should use the maximum value appropriate 
    /// for your application.
    /// </remarks>
    public int Resolution
    {
      get
      {
        if (this.disposed)
        {
          throw new ObjectDisposedException("Timer");
        }

        return this.resolution;
      }

      set
      {
        if (this.disposed)
        {
          throw new ObjectDisposedException("Timer");
        }
        else if (value < 0)
        {
          throw new ArgumentOutOfRangeException(
            "Resolution", 
            value,
            "Multimedia timer resolution out of range.");
        }

        this.resolution = value;

        if (this.IsRunning)
        {
          this.Stop();
          this.Start();
        }
      }
    }

    /// <summary>
    /// Gets or sets the timer mode.
    /// </summary>
    /// <exception cref="ObjectDisposedException">
    /// If the timer has already been disposed.
    /// </exception>
    public TimerMode Mode
    {
      get
      {
        if (this.disposed)
        {
          throw new ObjectDisposedException("Timer");
        }

        return this.mode;
      }

      set
      {
        if (this.disposed)
        {
          throw new ObjectDisposedException("Timer");
        }

        this.mode = value;

        if (this.IsRunning)
        {
          this.Stop();
          this.Start();
        }
      }
    }

    /// <summary>
    /// Gets a value indicating whether the Timer is running.
    /// </summary>
    public bool IsRunning
    {
      get { return this.running; }
    }

    /// <summary>
    /// Gets or sets the Components <see cref="ISite"/>
    /// </summary>
    public ISite Site
    {
      get { return this.site; }
      set { this.site = value; }
    }

    #endregion //PROPERTIES

    ///////////////////////////////////////////////////////////////////////////////
    // Public methods                                                            //
    ///////////////////////////////////////////////////////////////////////////////
    #region PUBLICMETHODS

    /// <summary>
    /// Frees timer resources.
    /// </summary>
    public void Dispose()
    {
      if (this.disposed)
      {
        return;
      }

      if (this.IsRunning)
      {
        this.Stop();
      }

      this.disposed = true;

      this.OnDisposed(EventArgs.Empty);
    }

    /// <summary>
    /// Starts the timer.
    /// </summary>
    /// <exception cref="ObjectDisposedException">
    /// The timer has already been disposed.
    /// </exception>
    /// <exception cref="TimerStartException">
    /// The timer failed to start.
    /// </exception>
    public void Start()
    {
      if (this.disposed)
      {
        throw new ObjectDisposedException("Timer");
      }

      if (this.IsRunning)
      {
        return;
      }

      // If the periodic event callback should be used.
      if (this.Mode == TimerMode.Periodic)
      {
        // Create and start timer.
        this.timerID = timeSetEvent(this.Period, this.Resolution, this.timeProcPeriodic, 0, (int)this.Mode);
      }
      else
      {
        // Else the one shot event callback should be used.
        // Create and start timer.
        this.timerID = timeSetEvent(this.Period, this.Resolution, this.timeProcOneShot, 0, (int)this.Mode);
      }

      // If the timer was created successfully.
      if (this.timerID != 0)
      {
        this.running = true;

        if (this.SynchronizingObject != null && this.SynchronizingObject.InvokeRequired)
        {
          this.SynchronizingObject.BeginInvoke(
              new EventRaiser(this.OnStarted),
              new object[] { EventArgs.Empty });
        }
        else
        {
          this.OnStarted(EventArgs.Empty);
        }
      }
      else
      {
        throw new TimerStartException("Unable to start multimedia Timer.");
      }
    }

    /// <summary>
    /// Stops timer.
    /// </summary>
    /// <exception cref="ObjectDisposedException">
    /// If the timer has already been disposed.
    /// </exception>
    public void Stop()
    {
      if (this.disposed)
      {
        throw new ObjectDisposedException("Timer");
      }

      if (!this.running)
      {
        return;
      }

      // Stop and destroy timer.
      int result = timeKillEvent(this.timerID);

      Debug.Assert(result == TIMERRNOERROR, "TIMER not existent");

      this.running = false;

      if (this.SynchronizingObject != null && this.SynchronizingObject.InvokeRequired)
      {
        this.SynchronizingObject.BeginInvoke(
            new EventRaiser(this.OnStopped),
            new object[] { EventArgs.Empty });
      }
      else
      {
        this.OnStopped(EventArgs.Empty);
      }
    }

    #endregion //PUBLICMETHODS

    ///////////////////////////////////////////////////////////////////////////////
    // Inherited methods                                                         //
    ///////////////////////////////////////////////////////////////////////////////
    #region OVERRIDES

    /// <summary>
    /// The timeGetDevCaps function queries the timer device to determine its resolution.
    /// </summary>
    /// <param name="caps">A TIMECAPS structure. This structure is filled with 
    /// information about the resolution of the timer device.</param>
    /// <param name="sizeOfTimerCaps">Size, in bytes, of the TIMECAPS structure.</param>
    /// <returns>Returns TIMERR_NOERROR if successful or TIMERR_STRUCT if it 
    /// fails to return the timer device capabilities.</returns>
    [DllImport("winmm.dll")]
    private static extern int timeGetDevCaps(
      ref TimerCaps caps,
      int sizeOfTimerCaps);

    /// <summary>
    /// The timeSetEvent function starts a specified timer event. 
    /// The multimedia timer runs in its own thread. After the event is activated, 
    /// it calls the specified callback function or sets or pulses the specified event object.
    /// </summary>
    /// <param name="delay">Event delay, in milliseconds. If this value is not in the 
    /// range of the minimum and maximum event delays supported by the timer, 
    /// the function returns an error.</param>
    /// <param name="resolution">Resolution of the timer event, in milliseconds. 
    /// The resolution increases with smaller values; a resolution of 0 indicates
    /// periodic events should occur with the greatest possible accuracy. 
    /// To reduce system overhead, however, you should use the maximum value 
    /// appropriate for your application.</param>
    /// <param name="proc">Pointer to a callback function that is called 
    /// once upon expiration of a single event or periodically upon expiration 
    /// of periodic events. If fuEvent specifies the TIME_CALLBACK_EVENT_SET 
    /// or TIME_CALLBACK_EVENT_PULSE flag, then the lpTimeProc parameter is interpreted 
    /// as a handle to an event object. The event will be set or pulsed upon completion 
    /// of a single event or periodically upon completion of periodic events. For any 
    /// other value of fuEvent, the lpTimeProc parameter is interpreted as a function
    /// pointer with the following signature: void (CALLBACK)(UINT uTimerID, UINT uMsg, 
    /// DWORD_PTR dwUser, DWORD_PTR dw1, DWORD_PTR dw2);</param>
    /// <param name="user">User-supplied callback data.</param>
    /// <param name="mode">Timer event type.</param>
    /// <returns>Returns an identifier for the timer event if successful 
    /// or an error otherwise. This function returns NULL if it fails and 
    /// the timer event was not created. (This identifier is also passed to the callback function.)</returns>
    [DllImport("winmm.dll")]
    private static extern int timeSetEvent(
      int delay,
      int resolution,
      TimeProc proc,
      int user,
      int mode);

    /// <summary>
    /// The timeKillEvent function cancels a specified timer event.
    /// </summary>
    /// <param name="id">Identifier of the timer event to cancel. 
    /// This identifier was returned by the timeSetEvent function 
    /// when the timer event was set up.</param>
    /// <returns>Returns TIMERR_NOERROR if successful or MMSYSERR_INVALPARAM 
    /// if the specified timer event does not exist.</returns>
    [DllImport("winmm.dll")]
    private static extern int timeKillEvent(int id);

    #endregion //OVERRIDES

    ///////////////////////////////////////////////////////////////////////////////
    // Eventhandler                                                              //
    ///////////////////////////////////////////////////////////////////////////////
    #region EVENTS

    ///////////////////////////////////////////////////////////////////////////////
    // Eventhandler for UI, Menu, Buttons, Toolbars etc.                         //
    ///////////////////////////////////////////////////////////////////////////////
    #region WINDOWSEVENTHANDLER
    #endregion //WINDOWSEVENTHANDLER

    ///////////////////////////////////////////////////////////////////////////////
    // Eventhandler for Custom Defined Events                                    //
    ///////////////////////////////////////////////////////////////////////////////
    #region CUSTOMEVENTHANDLER

    /// <summary>
    /// Callback method called by the Win32 multimedia timer when a timer
    /// periodic event occurs.
    /// </summary>
    /// <param name="id">Timer identifier</param>
    /// <param name="msg">Message to send</param>
    /// <param name="user">User parameter</param>
    /// <param name="param1">Parameter One</param>
    /// <param name="param2">Parameter Two</param>
    private void TimerPeriodicEventCallback(int id, int msg, int user, int param1, int param2)
    {
      if (this.synchronizingObject != null)
      {
        this.synchronizingObject.BeginInvoke(this.tickRaiser, new object[] { EventArgs.Empty });
      }
      else
      {
        this.OnTick(EventArgs.Empty);
      }
    }

    /// <summary>
    /// Callback method called by the Win32 multimedia timer when a timer
    /// one shot event occurs.
    /// </summary>
    /// <param name="id">Timer identifier</param>
    /// <param name="msg">Message to send</param>
    /// <param name="user">User parameter</param>
    /// <param name="param1">Parameter One</param>
    /// <param name="param2">Parameter Two</param>
    private void TimerOneShotEventCallback(int id, int msg, int user, int param1, int param2)
    {
      if (this.synchronizingObject != null)
      {
        this.synchronizingObject.BeginInvoke(this.tickRaiser, new object[] { EventArgs.Empty });
        this.Stop();
      }
      else
      {
        this.OnTick(EventArgs.Empty);
        this.Stop();
      }
    }

    /// <summary>
    /// Raises the Disposed event.
    /// </summary>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void OnDisposed(EventArgs e)
    {
      EventHandler handler = this.Disposed;

      if (handler != null)
      {
        handler(this, e);
      }
    }

    /// <summary>
    /// Raises the Started event.
    /// </summary>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void OnStarted(EventArgs e)
    {
      EventHandler handler = this.Started;

      if (handler != null)
      {
        handler(this, e);
      }
    }

    /// <summary>
    /// Raises the Stopped event.
    /// </summary>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void OnStopped(EventArgs e)
    {
      EventHandler handler = this.Stopped;

      if (handler != null)
      {
        handler(this, e);
      }
    }

    /// <summary>
    /// Raises the Tick event.
    /// </summary>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void OnTick(EventArgs e)
    {
      EventHandler handler = this.Tick;

      if (handler != null)
      {
        handler(this, e);
      }
    }

    #endregion //CUSTOMEVENTHANDLER

    #endregion //EVENTS

    ///////////////////////////////////////////////////////////////////////////////
    // Methods and Eventhandling for Background tasks                            //
    ///////////////////////////////////////////////////////////////////////////////
    #region BACKGROUNDWORKER
    #endregion //BACKGROUNDWORKER

    ///////////////////////////////////////////////////////////////////////////////
    // Methods for doing main class job                                          //
    ///////////////////////////////////////////////////////////////////////////////
    #region PRIVATEMETHODS

    /// <summary>
    /// Initialize timer with default values.
    /// </summary>
    private void Initialize()
    {
      this.mode = TimerMode.Periodic;
      this.period = Capabilities.PeriodMin;
      this.resolution = 1;

      this.running = false;

      this.timeProcPeriodic = new TimeProc(this.TimerPeriodicEventCallback);
      this.timeProcOneShot = new TimeProc(this.TimerOneShotEventCallback);
      this.tickRaiser = new EventRaiser(this.OnTick);
    }

    #endregion //PRIVATEMETHODS

    ///////////////////////////////////////////////////////////////////////////////
    // Small helping Methods                                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region HELPER
    #endregion //HELPER
  }
}
