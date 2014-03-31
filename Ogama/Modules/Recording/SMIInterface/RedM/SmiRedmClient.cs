namespace Ogama.Modules.Recording.SMIInterface.RedM
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Text;

  using Ogama.Modules.Common.CustomEventArgs;
  using Ogama.Modules.Recording.TrackerBase;

  /// <summary>
  /// This class encapsulates the connection to a SMI red-m hardware device.
  /// </summary>
  public class SMIRedMClient : SMIRedMWrapperListener, ISMIClient
  {
    /// <summary>
    /// The smi wrapper
    /// </summary>
    private SMIRedMWrapper smiWrapper;

    /// <summary>
    /// Gets or sets a value indicating whether this instance is tracking.
    /// </summary>
    /// <value>
    /// <c>true</c> if this instance is tracking; otherwise, <c>false</c>.
    /// </value>
    public bool IsTracking { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether this instance is connected.
    /// </summary>
    /// <value>
    /// <c>true</c> if this instance is connected; otherwise, <c>false</c>.
    /// </value>
    public bool IsConnected { get; set; }

    /// <summary>
    /// The last time
    /// </summary>
    private long lastTime;

    /// <summary>
    /// The screen width
    /// </summary>
    private int screenWidth;

    /// <summary>
    /// The screen height
    /// </summary>
    private int screenHeight;

    /// <summary>
    /// Event. Raised, when new gaze data is available.
    /// </summary>
    public event GazeDataChangedEventHandler GazeDataChanged;

    /// <summary>
    /// Event. Raised, when calibration has finished.
    /// </summary>
    public event EventHandler CalibrationFinished;

    /// <summary>
    /// Initializes a new instance of the <see cref="SMIRedMClient"/> class.
    /// </summary>
    public SMIRedMClient()
    {
      this.init();
    }

    /// <summary>
    /// constructor for unit testing
    /// </summary>
    /// <param name="isConnected">if set to <c>true</c> [is connected].</param>
    /// <param name="screenWidth">Width of the screen.</param>
    /// <param name="screenHeight">Height of the screen.</param>
    public SMIRedMClient(bool isConnected, int screenWidth, int screenHeight)
    {
      this.IsConnected = isConnected;
      this.screenWidth = screenWidth;
      this.screenHeight = screenHeight;
    }

    /// <summary>
    /// Finalizes an instance of the <see cref="SMIRedMClient"/> class.
    /// </summary>
    ~SMIRedMClient()
    {
      if (this.smiWrapper != null)
      {
        this.smiWrapper.disconnecting();
      }
    }

    /// <summary>
    /// Initializes this instance.
    /// </summary>
    public void init()
    {
      this.smiWrapper = new SMIRedMWrapper();
      this.smiWrapper.initialize();
      this.smiWrapper.register(this);
      this.IsTracking = false;
      this.IsConnected = false;
      this.lastTime = 0;
      Document activeDocument = Document.ActiveDocument;
      if (activeDocument != null)
      {
        this.screenHeight = activeDocument.PresentationSize.Height;
        this.screenWidth = activeDocument.PresentationSize.Width;
      }
    }

    /// <summary>
    /// Gets or sets the <see cref="SMISetting" /> to be used within this client.
    /// </summary>
    /// <value>
    /// The settings.
    /// </value>
    public SMISetting Settings { get; set; }

    /// <summary>
    /// Configures this instance.
    /// </summary>
    public void Configure()
    {

    }

    /// <summary>
    /// Connects this instance.
    /// </summary>
    public void Connect()
    {
      try
      {
        this.smiWrapper.receiveip = this.Settings.SMIServerAddress;
        this.smiWrapper.receiveport = this.Settings.OGAMAServerPort;
        this.smiWrapper.sendip = this.Settings.SMIServerAddress;
        this.smiWrapper.sendport = this.Settings.SMIServerPort;

        this.smiWrapper.connect();

        this.IsConnected = true;
      }
      catch (Exception e)
      {
        Ogama.ExceptionHandling.ExceptionMethods.HandleExceptionSilent(e);
      }
    }

    /// <summary>
    /// Disconnects this instance.
    /// </summary>
    public void Disconnect()
    {
      try
      {
        this.smiWrapper.disconnecting();
        this.IsConnected = false;
      }
      catch (Exception ex)
      {
        Ogama.ExceptionHandling.ExceptionMethods.HandleExceptionSilent(ex);
      }
    }

    /// <summary>
    /// Starts the streaming.
    /// </summary>
    public void StartStreaming()
    {

    }

    /// <summary>
    /// Stops the streaming.
    /// </summary>
    public void StopStreaming()
    {

    }

    /// <summary>
    /// Starts the tracking.
    /// </summary>
    public void StartTracking()
    {
      this.smiWrapper.startrecording();
      this.IsTracking = true;
    }

    /// <summary>
    /// Stops the tracking.
    /// </summary>
    public void StopTracking()
    {
      this.smiWrapper.stoprecording();
      this.IsTracking = false;
    }

    /// <summary>
    /// Gets the time stamp.
    /// </summary>
    /// <returns></returns>
    public long GetTimeStamp()
    {
      return this.lastTime;
    }

    /// <summary>
    /// Calibrates this instance.
    /// </summary>
    public void Calibrate()
    {
      int calibrationPoints = 9;
      int display = 1;
      bool isPrimaryScreen = Ogama.Modules.Common.Tools.PresentationScreen.GetPresentationScreen().Primary;
      if (isPrimaryScreen)
      {
        display = 0;
      }

      var pointSize = 20;
      switch (this.Settings.CalibPointSize)
      {
        case CalibrationPointSize.Small:
          pointSize = 10;
          break;
        case CalibrationPointSize.Medium:
          pointSize = 20;
          break;
        case CalibrationPointSize.Large:
          pointSize = 40;
          break;
      }

      this.smiWrapper.calibrate(calibrationPoints, display, pointSize);

      this.smiWrapper.validate();

      this.smiWrapper.getaccuracy();

      this.OnCalibrationFinished(new EventArgs());

    }


    /// <summary>
    /// Extracts the tracker data.
    /// </summary>
    /// <param name="data">The data.</param>
    /// <returns></returns>
    public GazeData ExtractTrackerData(EyeTrackingController.SampleStruct data)
    {
      GazeData result = new GazeData();

      float gazePosXLeft = Convert.ToSingle(data.leftEye.gazeX);
      float gazePosXRight = Convert.ToSingle(data.rightEye.gazeX);
      float gazePosYLeft = Convert.ToSingle(data.leftEye.gazeY);
      float gazePosYRight = Convert.ToSingle(data.rightEye.gazeY);

      result.GazePosX = (gazePosXLeft + gazePosXRight) / 2;
      result.GazePosY = (gazePosYLeft + gazePosYRight) / 2;

      long MICROSECONDS = 1000;
      result.Time = (data.timestamp / MICROSECONDS);
      this.lastTime = result.Time;

      result.PupilDiaX = Convert.ToSingle(data.leftEye.diam);
      result.PupilDiaY = Convert.ToSingle(data.rightEye.diam);

      return result;
    }

    /// <summary>
    /// Raised when new gaze data is available.
    /// </summary>
    /// <param name="e"><see cref="GazeDataChangedEventArgs" /> event arguments</param>
    /// .
    private void OnGazeDataChanged(GazeDataChangedEventArgs e)
    {
      if (this.GazeDataChanged != null)
      {
        this.GazeDataChanged(this, e);
      }
    }

    /// <summary>
    /// Raises the <see cref="E:CalibrationFinished" /> event.
    /// </summary>
    /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    private void OnCalibrationFinished(EventArgs e)
    {
      if (this.CalibrationFinished == null)
      {
        return;
      }
      this.CalibrationFinished(this, e);
    }

    /// <summary>
    /// The lock
    /// </summary>
    private Object LOCK = new Object();

    /// <summary>
    /// Ons the sample data.
    /// </summary>
    /// <param name="data">The data.</param>
    public void onSampleData(EyeTrackingController.SampleStruct data)
    {
      lock (LOCK)
      {
        if (!IsTracking)
        {
          return;
        }
        if (this.GazeDataChanged == null)
        {
          return;
        }
        GazeData gazeData = ExtractTrackerData(data);

        GazeDataChangedEventArgs eventArgs = new GazeDataChangedEventArgs(gazeData);

        this.OnGazeDataChanged(eventArgs);
      }
    }
  }
}
