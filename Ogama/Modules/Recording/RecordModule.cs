// <copyright file="RecordModule.cs" company="FU Berlin">
// ******************************************************
// OGAMA - open gaze and mouse analyzer 
// Copyright (C) 2010 Adrian Voßkühler  
// ------------------------------------------------------------------------
// This program is free software; you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation; either version 2 of the License, or (at your option) any later version.
// This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
// You should have received a copy of the GNU General Public License along with this program; if not, write to the Free Software Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA 02111-1307 USA
// **************************************************************
// </copyright>
// <author>Adrian Voßkühler</author>
// <email>adrian.vosskuehler@fu-berlin.de</email>

namespace Ogama.Modules.Recording
{
  using System;
  using System.Collections.Generic;
  using System.ComponentModel;
  using System.Data;
  using System.Diagnostics;
  using System.Drawing;
  using System.IO;
  using System.Threading;
  using System.Windows.Forms;
  using DirectX.Capture;

  using Ogama.DataSet;
  using Ogama.ExceptionHandling;
  using Ogama.MainWindow;
  using Ogama.Modules.Common;
  using Ogama.Modules.Fixations;
  using Ogama.Modules.ImportExport;
  using Ogama.Modules.Recording.Alea;
  using Ogama.Modules.Recording.MouseOnly;
  using Ogama.Modules.Recording.SMI;
  using Ogama.Modules.Recording.Tobii;
  using Ogama.Properties;
  using OgamaControls;
  using VectorGraphics.Elements;
  using VectorGraphics.StopConditions;
  using VectorGraphics.Tools;
  using VectorGraphics.Triggers;

  /// <summary>
  /// Derived from <see cref="FormWithPicture"/>.
  /// This <see cref="Form"/> is the recording module of OGAMA.
  /// It is intended to receive the sampling data from the
  /// <see cref="ITracker"/> interfaces and stores them after succesful
  /// presentation of the slideshow via the <see cref="PresenterModule"/>
  /// into OGAMAs database.
  /// </summary>
  public partial class RecordModule : FormWithPicture
  {
    ///////////////////////////////////////////////////////////////////////////////
    // Defining Constants                                                        //
    ///////////////////////////////////////////////////////////////////////////////
    #region CONSTANTS

    /// <summary>
    /// This value sets the maximal distance for two points
    /// that are considered to be near in pixel.
    /// </summary>
    private const int MAXDISTANCENEAR = 30;

    /// <summary>
    /// Number of tables that are used to bulk copy the data into the
    /// database from the dataset.
    /// </summary>
    private const int NUMWRITINGTHREADS = 5;

    /// <summary>
    /// When the received samples exceed the following limit after trial 
    /// has changed, then the data is written to the database,
    /// freeing memory.
    /// </summary>
    private const int MINSAMPLESFORWRITINGTODATABASE = 1000;

    /// <summary>
    /// A value that indicates how much samples are ignored during
    /// refreshing the mouse and gaze cursors.
    /// </summary>
    private static int reducingCount = 3;

    /// <summary>
    /// This is a slide that shows a short recording instruction
    /// when starting the module.
    /// </summary>
    private static Slide defaultSlide = new Slide(
      "Background",
      Color.Black,
      Images.CreateRecordInstructionImage(Document.ActiveDocument.ExperimentSettings.WidthStimulusScreen, Document.ActiveDocument.ExperimentSettings.HeightStimulusScreen),
      new StopConditionCollection(),
      null,
      string.Empty,
      Document.ActiveDocument.PresentationSize);

    #endregion //CONSTANTS

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Variables, Enumerations, Events                                  //
    ///////////////////////////////////////////////////////////////////////////////
    #region FIELDS

    /// <summary>
    /// An optional trigger that can be send for each slide additionally to the 
    /// triggers that can be defined for each slide separately.
    /// </summary>
    private Trigger generalTrigger;

    /// <summary>
    /// This counter is to get the current slide.
    /// </summary>
    private volatile int slideCounter;

    /// <summary>
    /// Saves the trial count.
    /// </summary>
    private volatile int trialCounter;

    /// <summary>
    /// local lock for GetTime method
    /// </summary>
    private object timeLock;

    /// <summary>
    /// Save last time stamp
    /// </summary>
    private long lastTimeStamp;

    /// <summary>
    /// This field counts the number of gaze samples that are near.
    /// It is used for smoothing the gaze cursor when the live viewer
    /// is in <see cref="smoothing"/> mode.
    /// </summary>
    private int numberOfSamplesInRegion;

    /// <summary>
    /// This is the sum of all sample values within a region.
    /// It is used for smoothing the gaze cursor when the live viewer
    /// is in <see cref="smoothing"/> mode.
    /// </summary>
    private PointF sumOfSamplesInRegion;

    /// <summary>
    /// This field saves the gaze cursor center in presentation
    /// screen coordinates.
    /// </summary>
    private PointF currentGazeCursorCenter;

    /// <summary>
    /// This field saves the mouse cursor center in presentation
    /// screen coordinates.
    /// </summary>
    private PointF currentMouseCursorCenter;

    /// <summary>
    /// This flag indicates a smoothing of the 
    /// gaze cursor in the live viewer.
    /// </summary>
    private bool smoothing;

    /// <summary>
    /// This flag is for debug purposes only and forces the 
    /// panelViewer which is a copy of the presentation screen to
    /// update even if it is hidden under the presentation screen.
    /// </summary>
    private bool forcePanelViewerUpdate;

    /// <summary>
    /// Saves the trials that are currently presented.
    /// </summary>
    private TrialCollection trials;

    /// <summary>
    /// Saves the current shown slide.
    /// </summary>
    private Slide currentSlide;

    /// <summary>
    /// The <see cref="Thread"/> in which the presentation
    /// is executed.
    /// </summary>
    private Thread presentationThread;

    /// <summary>
    /// Saves the x-Resolution of the presentation screen.
    /// </summary>
    private int xResolution;

    /// <summary>
    /// Saves the y-Resolution of the presentation screen.
    /// </summary>
    private int yResolution;

    /// <summary>
    /// Saves the x-Position of the presentation screen.
    /// </summary>
    private int xPosition;

    /// <summary>
    /// Saves the y-Position of the presentation screen.
    /// </summary>
    private int yPosition;

    /// <summary>
    /// Saves the current identifier for the current trial.
    /// </summary>
    private Trial currentTrial;

    /// <summary>
    /// Saves the current identifier for the preceding trial.
    /// </summary>
    private Trial precedingTrial;

    /// <summary>
    /// Saves the current trials starting time in milliseconds.
    /// </summary>
    private long currentTrialStarttime;

    /// <summary>
    /// Saves the current trials video starting time in milliseconds.
    /// </summary>
    private long currentTrialVideoStartTime;

    /// <summary>
    /// Saves the recordings starting time.
    /// </summary>
    private long recordingStarttime;

    /// <summary>
    /// Saves the precise time when the slide or trial counter
    /// were changed, that means a new slide is shown
    /// on the presenter.
    /// </summary>
    private long counterChangedTime;

    /// <summary>
    /// Saves the trials of the current recording.
    /// </summary>
    private List<TrialsData> trialDataList;

    /// <summary>
    /// Saves the trial events of the current recording.
    /// </summary>
    private List<TrialEvent> trialEventList;

    /// <summary>
    /// Saves the sampling data of the current recording.
    /// </summary>
    private List<RawData>[] rawDataLists;

    /// <summary>
    /// A flag whether this system has a secondary screen.
    /// </summary>
    private bool systemHasSecondaryScreen;

    /// <summary>
    /// Stores the currently used <see cref="ITracker"/>.
    /// </summary>
    private Tracker currentTracker;

    /// <summary>
    /// Contains the available resp. choosen <see cref="Tracker"/>
    /// derived tracking interfaces.
    /// </summary>
    private Dictionary<HardwareTracker, Tracker> trackerInterfaces;

    /// <summary>
    /// The raw data table to write the samples to.
    /// </summary>
    private OgamaDataSet.RawdataDataTable subjectRawDataTable;

    /// <summary>
    /// Holds the currently used index of the raw data list in use.
    /// </summary>
    private volatile int listCounter;

    /// <summary>
    /// The delegate for the call of the 
    /// panel viewer update from another thread.
    /// </summary>
    private UpdateLiveView delegateNewSlideAvailable;

    /// <summary>
    /// Saves the <see cref="ScreenCaptureProperties"/>
    /// for the screen recorder of the presentation module.
    /// </summary>
    private ScreenCaptureProperties screenCaptureProperties;

    /// <summary>
    /// A diagnostic <see cref="Stopwatch"/> for debugging purposes.
    /// </summary>
    private Stopwatch watch;

    /// <summary>
    /// A <see cref="Stopwatch"/> for counting the time left during recording.
    /// </summary>
    private Stopwatch recordTimerWatch;

    /// <summary>
    /// This member hosts the form for the presenter module.
    /// </summary>
    private PresenterModule presenterForm;

    /// <summary>
    /// Flag, indicating a running recording.
    /// </summary>
    private bool recordingBusy;

    #endregion //FIELDS

    ///////////////////////////////////////////////////////////////////////////////
    // Construction and Initializing methods                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region CONSTRUCTION

    /// <summary>
    /// Initializes a new instance of the RecordModule class.
    /// </summary>
    public RecordModule()
    {
      this.InitializeComponent();
      this.InitAccelerators();

      this.Picture = this.recordPicture;
      this.InitializeCustomElements();
    }

    #endregion //CONSTRUCTION

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Enumerations                                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region ENUMS

    /// <summary>
    /// This delegate is to call the get time method of
    /// this interface.
    /// </summary>
    /// <returns>A <see cref="Int64"/> with the timestamp of
    /// the last retreived tracker sample.</returns>
    public delegate long GetTimeDelegate();

    /// <summary>
    /// The delegate declaration for the call of the 
    /// panel viewer update from another thread.
    /// </summary>
    private delegate void UpdateLiveView();

    /// <summary>
    /// This delegate enables asynchronous calls for setting
    /// a string property.
    /// </summary>
    /// <param name="stringToSet">A <see cref="String"/> for the string property to set.</param>
    private delegate void SetStringInvoker(string stringToSet);

    /// <summary>
    /// Event. Raised when a new recording finished succesfully.
    /// </summary>
    public event EventHandler RecordingFinished;

    /// <summary>
    /// Event. Raised when new raw data row is available.
    /// </summary>
    private event NewRawDataAvailableEventHandler NewRawDataAvailable;

    #endregion ENUMS

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Properties                                                       //
    ///////////////////////////////////////////////////////////////////////////////
    #region PROPERTIES

    /// <summary>
    /// Gets the form of the presenter module if presentation is running-
    /// </summary>
    public PresenterModule Presenter
    {
      get { return this.presenterForm; }
    }

    #endregion //PROPERTIES

    ///////////////////////////////////////////////////////////////////////////////
    // Public methods                                                            //
    ///////////////////////////////////////////////////////////////////////////////
    #region PUBLICMETHODS

    /// <summary>
    /// This method test the presentation screen for equal size with the size
    /// specified in the experiment settings and returns true if succesful.
    /// </summary>
    /// <returns><strong>True</strong>, if presentation screen size is correct,
    /// otherwise <strong>false</strong>.</returns>
    public static bool CheckForCorrectPresentationScreenResolution()
    {
      Size presentationScreenWorkingArea = PresentationScreen.GetPresentationResolution();
      int presentationWidth = Document.ActiveDocument.ExperimentSettings.WidthStimulusScreen;
      int presentationHeight = Document.ActiveDocument.ExperimentSettings.HeightStimulusScreen;
      if (presentationScreenWorkingArea.Width != presentationWidth ||
        presentationScreenWorkingArea.Height != presentationHeight)
      {
        string message = "The resolution of the presentation screen: " + presentationScreenWorkingArea.Width.ToString() +
          "x" + presentationScreenWorkingArea.Height.ToString() + " is different from the size " +
          "specified in the experiment settings: " + presentationWidth.ToString() + "x" +
          presentationHeight.ToString() + "." + Environment.NewLine +
          "Please change the resolution of the presentation screen to fit the resolution specified in the experiment settings." + Environment.NewLine
          + "This can be done in the display settings of the windows control panel." + Environment.NewLine +
          "In some setups you may also need to hide the taskbars and other windows.";

        ExceptionMethods.ProcessMessage(
          "Incorrect presentation screen resolution.",
          message);

        return false;
      }

      // Check for missing secondary screen and switch presentation monitor
      // if applicable.
      if (Settings.Default.PresentationScreenMonitor == "Secondary"
        && !SecondaryScreen.SystemHasSecondaryScreen())
      {
        Settings.Default.PresentationScreenMonitor = "Primary";
        Settings.Default.Save();
      }

      return true;
    }

    /// <summary>
    /// This method polls the current cursor centers for the gaze
    /// and the mouse cursor and writes them into the parameters.
    /// </summary>
    /// <param name="gazeCenter">Out. A <see cref="PointF"/> with the new gaze center location.</param>
    /// <param name="mouseCenter">Out. A <see cref="PointF"/> with the new mouse center location.</param>
    public void GetCurrentCursorPositions(out PointF gazeCenter, out PointF mouseCenter)
    {
      lock (this)
      {
        gazeCenter = this.currentGazeCursorCenter;
        mouseCenter = this.currentMouseCursorCenter;
      }

      // Send cursor positions to video overlay DMO
      // during capturing of live stream
      if (this.presenterForm != null)
      {
        if (this.presenterForm.CurrentScreenCapture != null)
        {
          this.presenterForm.CurrentScreenCapture.UpdateDMOParams(
            Point.Round(gazeCenter),
            Point.Round(mouseCenter));
        }
      }
    }

    /// <summary>
    /// This method calls the <see cref="ITracker.Record()"/> method after
    /// initializing the recordings start time and succesful start
    /// of the presentation.
    /// </summary>
    /// <returns><strong>True</strong> if starting was succesfull, otherwise
    /// <strong>false</strong>.</returns>
    public bool StartRecording()
    {
      // Check for valid presentation thread
      if (this.presentationThread == null || !this.presentationThread.IsAlive)
      {
        this.Cursor = Cursors.WaitCursor;

        // create new raw data table for subject
        this.subjectRawDataTable = new OgamaDataSet.RawdataDataTable();
        this.subjectRawDataTable.TableName = this.currentTracker.Subject.SubjectName + "Rawdata";

        // Start presentation in a separate thread,
        if (this.StartPresentation())
        {
          this.Cursor = Cursors.Default;
          return true;
        }
      }
      else
      {
        InformationDialog.Show(
          "Recording is active",
          "The tracker is currently recording. Please stop the current recording first. By typing ESC for example.",
          false,
          MessageBoxIcon.Warning);
      }

      this.Cursor = Cursors.Default;
      return false;
    }

    /// <summary>
    /// The <see cref="ITracker.GazeDataChanged"/> event handler.
    /// Calculates the new sampling data from the trackers format.
    /// Updates the live viewers cursors and send a
    /// <see cref="NewRawDataAvailable"/> message to write the
    /// data into the database.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">A <see cref="GazeDataChangedEventArgs"/> with the event data.</param>
    public void ITracker_GazeDataChanged(object sender, GazeDataChangedEventArgs e)
    {
      // Save current timestamp
      lock (this.timeLock)
      {
        this.lastTimeStamp = e.Gazedata.Time;
      }

      if (this.currentTrial == null)
      {
        return;
      }

      // Create a new RawData object
      RawData newRawData = new RawData();

      // Write name
      newRawData.SubjectName = this.currentTracker.Subject.SubjectName;

      // Write the sampling values from the ITracker interface
      newRawData.GazePosX = e.Gazedata.GazePosX;
      newRawData.GazePosY = e.Gazedata.GazePosY;

      // The GazePos data is in values from 0 to 1
      // so scale it to SCREEN COORDINATES
      if (newRawData.GazePosX != null)
      {
        newRawData.GazePosX = newRawData.GazePosX * this.xResolution;
      }

      if (newRawData.GazePosY != null)
      {
        newRawData.GazePosY = newRawData.GazePosY * this.yResolution;
      }

      newRawData.PupilDiaX = e.Gazedata.PupilDiaX;
      newRawData.PupilDiaY = e.Gazedata.PupilDiaY;

      long gazeTime = e.Gazedata.Time - this.recordingStarttime;
      newRawData.Time = gazeTime;
      newRawData.TrialSequence = this.trialCounter;

      // Retrieve mouse position, this is already in SCREEN COORDINATES
      PointF? newMousePos = this.GetMousePosition();
      if (this.currentSlide.MouseCursorVisible && newMousePos != null)
      {
        newRawData.MousePosX = newMousePos.Value.X;
        newRawData.MousePosY = newMousePos.Value.Y;
      }

      // Update gaze cursor with reduced update rate
      // if (_counter % REDUCING_COUNT == 0)
      if (newRawData.GazePosX != null && newRawData.GazePosY != null)
      {
        PointF newCursorPos = new PointF(
          newRawData.GazePosX.Value,
          newRawData.GazePosY.Value);

        if (this.smoothing)
        {
          if (this.PointsArNear(newCursorPos, this.currentGazeCursorCenter))
          {
            this.numberOfSamplesInRegion++;
            this.sumOfSamplesInRegion.X += newCursorPos.X;
            this.sumOfSamplesInRegion.Y += newCursorPos.Y;
            this.currentGazeCursorCenter.X = this.sumOfSamplesInRegion.X / this.numberOfSamplesInRegion;
            this.currentGazeCursorCenter.Y = this.sumOfSamplesInRegion.Y / this.numberOfSamplesInRegion;
          }
          else
          {
            this.numberOfSamplesInRegion = 0;
            this.sumOfSamplesInRegion = PointF.Empty;
            this.currentGazeCursorCenter = newCursorPos;
          }
        }
        else
        {
          this.currentGazeCursorCenter = newCursorPos;
        }
      }

      // Update mouse cursor with reduced update rate
      // if (_counter % REDUCING_COUNT == 0)
      {
        if (this.systemHasSecondaryScreen || this.forcePanelViewerUpdate)
        {
          if (newRawData.MousePosX.HasValue && newRawData.MousePosY.HasValue)
          {
            this.currentMouseCursorCenter = new PointF(
              newRawData.MousePosX.Value,
              newRawData.MousePosY.Value);
          }
        }
      }

      this.OnNewRawDataAvailable(new NewRawDataAvailableEventArgs(newRawData));
    }

    #endregion //PUBLICMETHODS

    ///////////////////////////////////////////////////////////////////////////////
    // Inherited methods                                                         //
    ///////////////////////////////////////////////////////////////////////////////
    #region OVERRIDES

    /// <summary>
    /// This methods is used to initialize elements that are not
    /// initialized in the designer.
    /// </summary>
    protected override void InitializeCustomElements()
    {
      base.InitializeCustomElements();

      this.btnHelp.Click += new EventHandler(this.btnHelp_Click);
      this.pnlCanvas.Resize += new EventHandler(this.pnlCanvas_Resize);

      // Sets the counter value for updating mouse and gaze cursor.
      // Should give a frame rate of about 20 Hz
      // So if GazeSamping rate is 60 Hz to get 20 Hz it should be set to 3.
      reducingCount = Document.ActiveDocument.ExperimentSettings.GazeSamplingRate / 30;

      this.xResolution = Document.ActiveDocument.ExperimentSettings.WidthStimulusScreen;
      this.yResolution = Document.ActiveDocument.ExperimentSettings.HeightStimulusScreen;
      this.xPosition = PresentationScreen.GetPresentationBounds().X;
      this.yPosition = PresentationScreen.GetPresentationBounds().Y;

      this.systemHasSecondaryScreen = SecondaryScreen.SystemHasSecondaryScreen();

      this.delegateNewSlideAvailable = new UpdateLiveView(this.NewSlideAvailable);

      this.forcePanelViewerUpdate = true;

      this.trialDataList = new List<TrialsData>();
      this.trialEventList = new List<TrialEvent>();

      this.slideCounter = 0;
      this.trialCounter = -1;
      this.recordingStarttime = -5;
      this.timeLock = new object();
      this.lastTimeStamp = -1;

      this.watch = new Stopwatch();
      this.recordTimerWatch = new Stopwatch();
      ////this.watch.Start();

      this.generalTrigger = new Trigger();
      this.generalTrigger.OutputDevice = TriggerOutputDevices.LPT;
      this.generalTrigger.Signaling = TriggerSignaling.None;
      this.generalTrigger.SignalingTime = 10;
      this.generalTrigger.Value = 255;
      this.generalTrigger.PortAddress = 0x0378;

      // Take primary monitor
      int monitorIndex = 0;
      if (PresentationScreen.GetPresentationScreen() != Screen.PrimaryScreen)
      {
        // otherwise take the secondary sceen.
        monitorIndex = 1;
      }

      this.screenCaptureProperties = new ScreenCaptureProperties(
        "OgamaScreenCapture Filter",
        string.Empty,
        "ffdshow Video Codec",
        string.Empty,
        10,
        Document.ActiveDocument.PresentationSize,
        string.Empty,
        CaptureMode.Video,
        monitorIndex,
        this.recordPicture);

      this.rawDataLists = new List<RawData>[NUMWRITINGTHREADS];
      for (int i = 0; i < NUMWRITINGTHREADS; i++)
      {
        this.rawDataLists[i] = new List<RawData>();
      }

      if (SecondaryScreen.SystemHasSecondaryScreen())
      {
        if (Settings.Default.PresentationScreenMonitor == "Primary")
        {
          this.btnPrimary.Checked = true;
          this.btnSecondary.Checked = false;
        }
        else
        {
          this.btnPrimary.Checked = false;
          this.btnSecondary.Checked = true;
        }
      }
      else
      {
        this.btnPrimary.Visible = false;
        this.btnSecondary.Visible = false;
      }

      this.trackerInterfaces = new Dictionary<HardwareTracker, Tracker>();
    }

    /// <summary>
    /// Overridden <see cref="FormWithPicture.InitAccelerators()"/>.
    /// Sets Enter Key for start of recording.
    /// </summary>
    protected override void InitAccelerators()
    {
      base.InitAccelerators();
      this.SetAccelerator(Keys.Enter, new AcceleratorAction(this.OnPressEnter));
    }

    #endregion //OVERRIDES

    ///////////////////////////////////////////////////////////////////////////////
    // Eventhandler                                                              //
    ///////////////////////////////////////////////////////////////////////////////
    #region EVENTS

    ///////////////////////////////////////////////////////////////////////////////
    // Eventhandler for UI, Menu, Buttons, Toolbars etc.                         //
    ///////////////////////////////////////////////////////////////////////////////
    #region WINDOWSEVENTHANDLER

    /// <summary>
    /// <see cref="Form.Load"/> event handler. 
    /// Wires Mainform events and initializes tracker interfaces.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void RecordModule_Load(object sender, EventArgs e)
    {
      this.smoothing = false;

      // Intialize replay picture
      this.recordPicture.OwningForm = this;
      this.recordPicture.PresentationSize = Document.ActiveDocument.PresentationSize;
      this.ResizePicture();

      // Disable Usercam button, if there is no available webcam
      // except Ogama Screen Capture which should not be used.
      if (this.webcamPreview.DirectXCapture == null)
      {
        this.btnUsercam.Enabled = false;
        this.spcPanelUserCam.Panel2Collapsed = true;
      }

      this.InitializeScreenCapture();
      this.CreateTrackerInterfaces();

      this.NewSlideAvailable();
    }

    /// <summary>
    /// The <see cref="Form.FormClosing"/> event handler.
    /// Cleans up interfaces and checks for running presentations.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">A <see cref="FormClosingEventArgs"/> with the event data.</param>
    private void RecordModule_FormClosing(object sender, FormClosingEventArgs e)
    {
      if (this.presentationThread != null && this.presentationThread.IsAlive)
      {
        e.Cancel = true;
        InformationDialog.Show(
          "Presentation is active.",
          "The presentation is currently active. Please stop it first using the ESC key for example.",
          false,
          MessageBoxIcon.Information);
      }
      else
      {
        this.DisposeTrackerInterfaces();
      }
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler for the
    /// <see cref="Button"/> <see cref="btnPrimary"/>.
    /// Updates the presentation screen.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/>.</param>
    private void btnPrimary_Click(object sender, EventArgs e)
    {
      this.btnSecondary.Checked = !this.btnPrimary.Checked;
      this.SubmitPresentationScreenToSettings();
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler for the
    /// <see cref="Button"/> <see cref="btnSecondary"/>.
    /// Updates the presentation screen.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/>.</param>
    private void btnSecondary_Click(object sender, EventArgs e)
    {
      this.btnPrimary.Checked = !this.btnSecondary.Checked;
      this.SubmitPresentationScreenToSettings();
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler for the
    /// <see cref="Button"/> <see cref="btnWebcamSettings"/>.
    /// Calls the <see cref="Webcam.ShowConfigureDialog()"/> method
    /// for webcam specific settings modification.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/>.</param>
    private void btnWebcamSettings_Click(object sender, EventArgs e)
    {
      this.webcamPreview.ShowConfigureDialog();
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler for the
    /// <see cref="Button"/> <see cref="btnTriggerSettings"/>.
    /// Shows a <see cref="TriggerDialog"/> to specify a general trigger.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/>.</param>
    private void btnTriggerSettings_Click(object sender, EventArgs e)
    {
      TriggerDialog dlg = new TriggerDialog();
      dlg.TriggerSignal = this.generalTrigger;
      if (dlg.ShowDialog() == DialogResult.OK)
      {
        this.generalTrigger = dlg.TriggerSignal;
      }
    }

    /// <summary>
    /// The <see cref="TabControl.SelectedIndexChanged"/> event handler for the
    /// <see cref="TabControl"/> <see cref="tclEyetracker"/>.
    /// Switches the used <see cref="ITracker"/> according to selected tab.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/>.</param>
    private void tclEyetracker_SelectedIndexChanged(object sender, EventArgs e)
    {
      this.SwitchCurrentTracker();
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler for the
    /// <see cref="Button"/> <see cref="btnTrackerSettings"/>.
    /// Calls the <see cref="ITracker.ChangeSettings()"/> method
    /// for interface specific settings modification.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/>.</param>
    private void btnTrackerSettings_Click(object sender, EventArgs e)
    {
      if (this.currentTracker != null)
      {
        this.currentTracker.ChangeSettings();
      }
      else
      {
        string message = "Please choose a tracker system before changing settings, "
        + "by selecting a tracker tab at the left side of the module";
        ExceptionMethods.ProcessMessage("No tracker selected", message);
      }
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler for the
    /// <see cref="Button"/> <see cref="btnScreenCaptureSettings"/>.
    /// Shows a <see cref="ScreenCaptureDialog"/> to control
    /// the screen capture settings during recording of flash stimuli.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/>.</param>
    private void btnScreenCaptureSettings_Click(object sender, EventArgs e)
    {
      this.ShowScreenCaptureDialog();
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler for the
    /// <see cref="Button"/> <see cref="btnSelectTracker"/>.
    /// Shows the tracking device selection dialog and then
    /// calls the <see cref="CreateTrackerInterfaces()"/> method
    /// to create the tracking interfaces for the selected trackers.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/>.</param>
    private void btnSelectTracker_Click(object sender, EventArgs e)
    {
      HardwareTracker tracker = this.ShowTrackerSelectionDialog();
      if (tracker != HardwareTracker.None)
      {
        Properties.Settings.Default.ActivatedHardwareTracker = tracker.ToString();
        Properties.Settings.Default.Save();
        this.CreateTrackerInterfaces();
      }
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler for the
    /// <see cref="Button"/> <see cref="btnNoDeviceTabSelectTracker"/>.
    /// Shows the tracking device selection dialog and then
    /// calls the <see cref="CreateTrackerInterfaces()"/> method
    /// to create the tracking interfaces for the selected trackers.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/>.</param>
    private void btnNoDeviceTabSelectTracker_Click(object sender, EventArgs e)
    {
      HardwareTracker tracker = this.ShowTrackerSelectionDialog();
      if (tracker != HardwareTracker.None)
      {
        Properties.Settings.Default.ActivatedHardwareTracker = tracker.ToString();
        Properties.Settings.Default.Save();
        this.CreateTrackerInterfaces();
      }
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler for the
    /// <see cref="Button"/> <see cref="btnUsercam"/>.
    /// Shows or hides the webcam control.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/>.</param>
    private void btnUsercam_Click(object sender, EventArgs e)
    {
      if (this.btnUsercam.Checked)
      {
        // Check for available camera
        if (!this.webcamPreview.TestCapture())
        {
          this.btnUsercam.Checked = false;
          this.btnUsercam.Enabled = false;
          string message = "It seems that your system has no webcam plugged in, " +
            "so user camera recording is not available at the moment. " +
            Environment.NewLine +
            "Try again after plugging in a camera or change the capture device.";
          ExceptionMethods.ProcessMessage("No webcam found", message);
        }
        else
        {
          this.btnUsercam.Enabled = true;
        }
      }
      else
      {
        this.chbRecordVideo.Checked = false;
        this.chbRecordAudio.Checked = false;
      }

      this.webcamPreview.Preview = this.btnUsercam.Checked;
      this.spcPanelUserCam.Panel2Collapsed = !this.btnUsercam.Checked;
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler for the
    /// <see cref="Button"/> <see cref="btnSmoothing"/>.
    /// Switches the smoothing mode of the gaze cursor.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/>.</param>
    private void btnSmoothing_Click(object sender, EventArgs e)
    {
      this.smoothing = this.btnSmoothing.Checked;
    }

    #endregion //WINDOWSEVENTHANDLER

    ///////////////////////////////////////////////////////////////////////////////
    // Eventhandler for Custom Defined Events                                    //
    ///////////////////////////////////////////////////////////////////////////////
    #region CUSTOMEVENTHANDLER

    #region Presentation

    /// <summary>
    /// The <see cref="PresenterModule.TrialEventOccured"/> event handler.
    /// Stores the new trial event into the list.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">A <see cref="TrialEventOccuredEventArgs"/> with the event data.</param>
    private void objPresenter_TrialEventOccured(object sender, TrialEventOccuredEventArgs e)
    {
      lock (this)
      {
        long time = this.GetCurrentTime() - this.recordingStarttime - this.currentTrialStarttime;

        // Store trial event event
        e.TrialEvent.EventID = this.trialEventList.Count;
        e.TrialEvent.Time = time;
        e.TrialEvent.TrialSequence = this.trialCounter;
        e.TrialEvent.SubjectName = this.currentTracker.Subject.SubjectName;

        if (this.trialCounter >= 0)
        {
          this.trialEventList.Add(e.TrialEvent);
          if (e.TrialEvent.Type != EventType.Marker)
          {
            this.WriteTrialEventToRawData(e.TrialEvent);
          }
        }
      }
    }

    /// <summary>
    /// The <see cref="PresenterModule.CounterChanged"/> event handler.
    /// Indicates a slide change in the presenter, so update trial and slide
    /// counter and the live viewer.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">A <see cref="CounterChangedEventArgs"/> with the event data.</param>
    private void objPresenter_CounterChanged(object sender, CounterChangedEventArgs e)
    {
      this.counterChangedTime = this.GetCurrentTime();

      if (this.counterChangedTime < 0)
      {
        throw new ArgumentException("Tracking had not been started");
      }

      if (this.recordingStarttime == -5)
      {
        this.recordingStarttime = this.counterChangedTime;
        this.recordTimerWatch.Start();
        this.tmrRecordClock.Start();

        // Start update timer of control panel viewer
        this.Picture.StartAnimation();
      }

      lock (this)
      {
        // Don´t use presenters trialCounter
        // Because of using links between trials.
        this.trialCounter++;
        this.slideCounter = e.SlideCounter;

        // Set current trial
        if (e.TrialID != -5)
        {
          this.currentTrial = this.trials[this.trials.GetIndexOfTrialByID(e.TrialID)];
        }
        else
        {
          this.currentTrial = null;
        }

        // Set current slide
        if (this.currentTrial != null)
        {
          this.currentSlide = this.currentTrial[this.slideCounter];
        }
        else
        {
          this.currentSlide = null;
        }
      }
    }

    /// <summary>
    /// The <see cref="PresenterModule.TrialEventOccured"/> event handler.
    /// Stores the new trial event into the list.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">A <see cref="TrialEventOccuredEventArgs"/> with the event data.</param>
    private void objPresenter_SlideChanged(object sender, SlideChangedEventArgs e)
    {
      long time = this.counterChangedTime - this.recordingStarttime - this.currentTrialStarttime;

      // Store slide changed event
      MediaEvent slideChangedEvent = new MediaEvent();
      slideChangedEvent.EventID = this.trialEventList.Count;
      slideChangedEvent.Param = e.SlideCounter + "#" + e.NextSlide.Name;
      slideChangedEvent.Task = MediaEventTask.Show;
      slideChangedEvent.Time = time;
      slideChangedEvent.Type = EventType.Slide;
      slideChangedEvent.SubjectName = this.currentTracker.Subject.SubjectName;
      slideChangedEvent.TrialSequence = this.trialCounter;

      if (this.trialCounter >= 0)
      {
        this.trialEventList.Add(slideChangedEvent);
        this.WriteTrialEventToRawData(slideChangedEvent);
      }

      // Store subjects response event
      InputEvent inputEvent = new InputEvent();
      inputEvent.EventID = this.trialEventList.Count;
      if (e.Response != null)
      {
        inputEvent.Param = e.Response.ToString();
      }

      inputEvent.SubjectName = this.currentTracker.Subject.SubjectName;
      inputEvent.Task = InputEventTask.SlideChange;
      inputEvent.Time = time;
      inputEvent.TrialSequence = this.trialCounter;
      inputEvent.Type = EventType.Response;

      if (this.trialCounter >= 0)
      {
        this.trialEventList.Add(inputEvent);
        this.WriteTrialEventToRawData(slideChangedEvent);
      }

      this.Invoke(this.delegateNewSlideAvailable);
    }

    /// <summary>
    /// The <see cref="PresenterModule.TrialChanged"/> event handler.
    /// Stores the trial information into the database
    /// and updates the live viewer with the new slide.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">A <see cref="TrialChangedEventArgs"/> with the event data.</param>
    private void objPresenter_TrialChanged(object sender, TrialChangedEventArgs e)
    {
      // Set time critical values
      long currentTime = this.counterChangedTime;

      if (e.FinishedTrial.Name != "DummyTrial")
      {
        // Update current trial
        this.precedingTrial = e.FinishedTrial;

        // When rawData list exceeds sample limit or this was the last trial
        // write the samples into the database
        if (this.rawDataLists[this.listCounter].Count > MINSAMPLESFORWRITINGTODATABASE
          || e.NextTrial == null)
        {
          // Stop recording if this was the last trial or cancelled
          if (e.NextTrial == null)
          {
            // Stop tracking
            this.currentTracker.Stop();

            // Give the presentation thread time to close.
            Application.DoEvents();
          }

          // switch to next raw data list for writing
          lock (this)
          {
            // Save copy to dataset table in new thread
            AsyncHelper.FireAndForget(
              new WaitCallback(this.StoreRecordsInDataSetTable),
              new DataToTable(this.rawDataLists[this.listCounter], this.subjectRawDataTable));

            this.listCounter++;
            if (this.listCounter == NUMWRITINGTHREADS)
            {
              this.listCounter = 0;
            }
          }
        }

        // Write new trial information
        TrialsData trialData = new TrialsData();
        trialData.SubjectName = this.currentTracker.Subject.SubjectName;
        trialData.TrialName = this.precedingTrial.Name;
        trialData.TrialSequence = this.trialCounter - 1;
        trialData.TrialID = this.precedingTrial.ID;
        trialData.Category = e.Category;
        trialData.TrialStartTime = this.currentTrialStarttime;
        trialData.Duration = (int)(currentTime - this.recordingStarttime - this.currentTrialStarttime);

        if (this.trialCounter > 0)
        {
          this.trialDataList.Add(trialData);
        }

        // Store usercam start event if applicable
        if (this.chbRecordAudio.Checked || this.chbRecordVideo.Checked)
        {
          MediaEvent usercamVideoEvent = new MediaEvent();
          usercamVideoEvent.EventID = this.trialEventList.Count;
          usercamVideoEvent.Param = this.currentTrialVideoStartTime.ToString();
          usercamVideoEvent.Task = MediaEventTask.Start;
          usercamVideoEvent.Time = 0;
          usercamVideoEvent.Type = EventType.Usercam;
          usercamVideoEvent.SubjectName = this.currentTracker.Subject.SubjectName;
          usercamVideoEvent.TrialSequence = this.trialCounter - 1;
          if (this.trialCounter > 0)
          {
            this.trialEventList.Add(usercamVideoEvent);
          }
        }

        // Store subjects response event
        InputEvent inputEvent = new InputEvent();
        inputEvent.EventID = this.trialEventList.Count;
        if (e.Response != null)
        {
          inputEvent.Param = e.Response.ToString();
        }

        inputEvent.SubjectName = this.currentTracker.Subject.SubjectName;
        inputEvent.Task = InputEventTask.SlideChange;
        inputEvent.Time = trialData.Duration;
        inputEvent.TrialSequence = this.trialCounter - 1;
        inputEvent.Type = EventType.Response;

        if (this.trialCounter >= 0)
        {
          this.trialEventList.Add(inputEvent);
        }
      }

      // Update new trial
      this.currentTrialStarttime = currentTime - this.recordingStarttime;

      if (this.chbRecordAudio.Checked || this.chbRecordVideo.Checked)
      {
        this.currentTrialVideoStartTime = e.WebcamTime;
      }

      Thread updateLiveViewerThread = new Thread(new ThreadStart(this.NewSlideAvailable));
      updateLiveViewerThread.SetApartmentState(ApartmentState.STA);
      updateLiveViewerThread.Start();
    }

    /// <summary>
    /// This static method performs the writing of the copied raw data list into the
    /// table.
    /// </summary>
    /// <param name="stateInfo">An <see cref="Object"/> with the thread param which is 
    /// a <see cref="List{RawData}"/></param>
    private void StoreRecordsInDataSetTable(object stateInfo)
    {
      DataToTable data = (DataToTable)stateInfo;

      if (!Queries.SaveDataToTable(data.RawDataList, data.RawDataTable))
      {
        throw new DataException("The new raw data could not be written into the dataset.");
      }

      data.RawDataList.Clear();
    }

    /// <summary>
    /// The <see cref="PresenterModule.PresentationDone"/> event handler.
    /// Resets the recording module and ask for saving the new
    /// sampling data.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/>.</param>
    private void objPresenter_PresentationDone(object sender, EventArgs e)
    {
      try
      {
        // Wait for writing of raw data finishes
        bool writingFinished = false;
        while (!writingFinished)
        {
          bool dataFound = false;
          foreach (List<RawData> rawData in this.rawDataLists)
          {
            if (rawData.Count > 0)
            {
              Application.DoEvents();
              Thread.Sleep(500);
              dataFound = true;
              break;
            }
          }

          if (dataFound)
          {
            writingFinished = false;
          }
          else
          {
            writingFinished = true;
          }
        }

        // Resets the interface
        this.ResetRecordInterface();

        // Show the question for saving the new sampling data on the 
        // controllers screen
        AskSaveDataDialog saveDlg = new AskSaveDataDialog();
        Rectangle controllerBounds = PresentationScreen.GetControllerWorkingArea();
        Point centerControllerScreen = new Point(
        controllerBounds.Width / 2,
        controllerBounds.Height / 2);
        Cursor.Position = centerControllerScreen;
        saveDlg.Location =
        new Point(
        controllerBounds.Width / 2 - saveDlg.Width / 2,
        controllerBounds.Height / 2 - saveDlg.Height / 2);

        if (saveDlg.ShowDialog() == DialogResult.OK)
        {
          this.SaveRecordingIntoTablesAndDB();
        }
        else
        {
          Queries.DeleteRawDataTableInDB(this.currentTracker.Subject.SubjectName);
        }

        this.trialEventList.Clear();
        this.trialDataList.Clear();

        // Reset recording flag
        this.recordingBusy = false;
      }
      catch (Exception ex)
      {
        ExceptionMethods.HandleException(ex);
      }
    }

    #endregion //Presentation

    /// <summary>
    /// Raises <see cref="NewRawDataAvailable"/> event by invoking the delegates.
    /// </summary>
    /// <param name="e">A <see cref="TrialChangedEventArgs"/> with the event data.</param>
    private void OnNewRawDataAvailable(NewRawDataAvailableEventArgs e)
    {
      if (this.NewRawDataAvailable != null)
      {
        this.NewRawDataAvailable(this, e);
      }
    }

    /// <summary>
    /// Raises <see cref="RecordingFinished"/> event by invoking the delegates.
    /// </summary>
    /// <param name="e">An empty <see cref="EventArgs"/>.</param>
    private void OnRecordingFinished(EventArgs e)
    {
      if (this.RecordingFinished != null)
      {
        this.RecordingFinished(this, e);
      }
    }

    /// <summary>
    /// The <see cref="RecordModule.NewRawDataAvailable"/> event handler.
    /// Adds the sent <see cref="RawData"/> struct to the <see cref="rawDataLists"/>.
    /// </summary>
    /// <remarks>After succesfull recording this list will be written into
    /// the database.
    /// Because the access to this list is not allowed to be simultaneous from
    /// different methods, this event is introduced.
    /// It is called from each method that writes raw data
    /// rows (response entries or gaze data entries)</remarks>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">A <see cref="NewRawDataAvailableEventArgs"/> with the event data.</param>
    private void RecordModule_NewRawDataAvailable(object sender, NewRawDataAvailableEventArgs e)
    {
      // Write row into table
      try
      {
        // The beginloaddata and endloaddata methods are called to
        // workaround the follwing error message :
        // "DataTable internal index is corrupted: '5'"
        // Which occured some times during adding.
        // This workaround is 
        //  from http://connect.microsoft.com/VisualStudio/feedback/ViewFeedback.aspx?FeedbackID=98279
        // _subjectRawDataTable.BeginLoadData();
        // _subjectRawDataTable.AddRawdataRow(e.RawdataRow);
        // _subjectRawDataTable.EndLoadData();
        // This above approach didn´t even work, so finally I
        // came over with the following solution:
        // Writing all the data into a large list
        // which after trial is finished is written into the database
        if (e.RawData.TrialSequence >= 0)
        {
          this.rawDataLists[this.listCounter].Add(e.RawData);
        }
      }
      catch (Exception ex)
      {
        ExceptionMethods.HandleExceptionSilent(ex);
      }
    }

    /// <summary>
    /// The <see cref="OgamaControls.Webcam.WebcamAvailable"/> event handler.
    /// Updates the UI with the available modes for the usercam.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">A <see cref="CaptureModeEventArgs"/> with the event data.</param>
    private void webcamPreview_WebcamAvailable(object sender, CaptureModeEventArgs e)
    {
      // Show user cam by default
      this.spcPanelUserCam.Panel2Collapsed = false;

      // Disable recording check boxes by default
      this.chbRecordVideo.Enabled = false;
      this.chbRecordAudio.Enabled = false;

      // Update checkboxes with respect to available mode
      if ((e.Param & CaptureMode.VideoCapture) == CaptureMode.VideoCapture)
      {
        this.chbRecordVideo.Enabled = true;
      }

      if ((e.Param & CaptureMode.AudioCapture) == CaptureMode.AudioCapture)
      {
        this.chbRecordAudio.Enabled = true;
      }
    }

    /// <summary>
    /// The <see cref="System.Windows.Forms.Timer.Tick"/> event handler for the timer that checks every 500 ms
    /// for the end of the presentation thread.
    /// This is used to reinialize the usercam after it has been released from the 
    /// presentation thread, due to cross-thread calls this could not be done
    /// directly on the shutdown of the presentation thread.
    /// </summary>
    /// <param name="sender">Sender of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void tmrWaitForPresentationEnd_Tick(object sender, EventArgs e)
    {
      if (!this.recordingBusy)
      {
        // Restart usercam preview
        if (this.btnUsercam.Checked)
        {
          this.webcamPreview.Preview = true;
        }

        this.OnRecordingFinished(new StringEventArgs(string.Empty));

        Cursor.Current = Cursors.Default;

        this.tmrWaitForPresentationEnd.Stop();
      }
    }

    /// <summary>
    /// The <see cref="System.Windows.Forms.Timer.Tick"/> event handler for the timer that 
    /// updates the recording timer.
    /// </summary>
    /// <param name="sender">Sender of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void tmrRecordClock_Tick(object sender, EventArgs e)
    {
      this.ThreadSafeUpdateLabel(this.recordTimerWatch.Elapsed.ToString());
    }

    #endregion //CUSTOMEVENTHANDLER

    #endregion //EVENTS

    ///////////////////////////////////////////////////////////////////////////////
    // Methods and Eventhandling for Background tasks                            //
    ///////////////////////////////////////////////////////////////////////////////
    #region BACKGROUNDWORKER

    /// <summary>
    /// The presentations <see cref="Thread"/> DoWork event handler.
    /// Initializes the <see cref="PresenterModule"/> for presentation and
    /// wires the events.
    /// </summary>
    /// <param name="data">A <see cref="object"/> with the list of slides.</param>
    private void PresentationThread_DoWork(object data)
    {
      // Create presentation form
      this.presenterForm = new PresenterModule();

      // Cast thread data.
      List<object> threadParams = (List<object>)data;
      TrialCollection trials = threadParams[0] as TrialCollection;
      Trigger trigger = threadParams[1] as Trigger;
      bool enableTrigger = (bool)threadParams[2];
      ScreenCaptureProperties screenCaptureProperties = (ScreenCaptureProperties)threadParams[3];
      CaptureDeviceProperties userCameraProperties = (CaptureDeviceProperties)threadParams[4];
      Control userCameraPreviewWindow = (Control)threadParams[5];
      GetTimeDelegate getTimeMethod = this.GetCurrentTime;

      // Set triggering state
      this.presenterForm.EnableTrigger = enableTrigger;

      // Set general trigger
      this.presenterForm.GeneralTrigger = trigger;

      // Set slide list of presenter
      this.presenterForm.TrialList = trials;

      // Set screen capture device properties
      this.presenterForm.ScreenCaptureProperties = screenCaptureProperties;

      // Set preview window for user camera
      this.presenterForm.UserCameraPreviewWindow = userCameraPreviewWindow;

      // Set user camera properties
      this.presenterForm.UserCameraProperties = userCameraProperties;

      // sets the get time method
      this.presenterForm.GetTimeMethod = getTimeMethod;

      // Wire presenter events
      this.presenterForm.CounterChanged += new CounterChangedEventHandler(this.objPresenter_CounterChanged);
      this.presenterForm.SlideChanged += new SlideChangedEventHandler(this.objPresenter_SlideChanged);
      this.presenterForm.TrialChanged += new TrialChangedEventHandler(this.objPresenter_TrialChanged);
      this.presenterForm.TrialEventOccured += new TrialEventOccuredEventHandler(this.objPresenter_TrialEventOccured);
      this.presenterForm.PresentationDone += new EventHandler(this.objPresenter_PresentationDone);

      // Wire this forms event
      this.NewRawDataAvailable += new NewRawDataAvailableEventHandler(this.RecordModule_NewRawDataAvailable);

      // Captures input to presentation form.
      this.presenterForm.Capture = true;

      // Show presenter form, that starts presentation.
      this.presenterForm.ShowDialog();

      // Presentation is done, so unplug event handlers.
      this.presenterForm.CounterChanged -= new CounterChangedEventHandler(this.objPresenter_CounterChanged);
      this.presenterForm.SlideChanged -= new SlideChangedEventHandler(this.objPresenter_SlideChanged);
      this.presenterForm.TrialChanged -= new TrialChangedEventHandler(this.objPresenter_TrialChanged);
      this.presenterForm.TrialEventOccured -= new TrialEventOccuredEventHandler(this.objPresenter_TrialEventOccured);
      this.presenterForm.PresentationDone -= new EventHandler(this.objPresenter_PresentationDone);

      // Delete presentation form
      this.presenterForm.Dispose();
      this.presenterForm = null;

      this.NewRawDataAvailable -= new NewRawDataAvailableEventHandler(this.RecordModule_NewRawDataAvailable);
    }

    /// <summary>
    /// The <see cref="BackgroundWorker.DoWork"/> event handler for
    /// the <see cref="BackgroundWorker"/> <see cref="bgwSaveSplash"/>.
    /// Shows the <see cref="SavingSplash"/> form with a splash screen
    /// wait dialog.
    /// </summary>
    /// <param name="sender">Source of the event</param>
    /// <param name="e">A <see cref="DoWorkEventArgs"/> with the event data.</param>
    private void bgwSaveSplash_DoWork(object sender, DoWorkEventArgs e)
    {
      // Create splash object
      SavingSplash newSplash = new SavingSplash();

      // Get the BackgroundWorker that raised this event.
      BackgroundWorker worker = sender as BackgroundWorker;
      newSplash.Worker = worker;

      // Get the description to be shown in the headline.
      string description = e.Argument as string;
      newSplash.Description = description;

      // Show dialog
      newSplash.ShowDialog();
    }

    #endregion //BACKGROUNDWORKER

    ///////////////////////////////////////////////////////////////////////////////
    // Methods for doing main class job                                          //
    ///////////////////////////////////////////////////////////////////////////////
    #region METHODS

    /// <summary>
    /// This method checks if there are any unsaved changes to the current slideshow.
    /// </summary>
    /// <returns>True if the slideshow has unsaved changes, otherwise false.</returns>
    private static bool CheckForModifiedSlideshow()
    {
      if (Document.ActiveDocument.ExperimentSettings.SlideShow.IsModified)
      {
        string message = "You´re current slideshow has unsaved changes." +
          Environment.NewLine + "Please switch to the Slideshow design module and save them before starting a recording.";

        ExceptionMethods.ProcessMessage("Please save the modified slideshow first.", message);
        return true;
      }

      return false;
    }

    /// <summary>
    /// Key event handler for the Enter key.
    /// Starts recording if UI is set.
    /// </summary>
    private void OnPressEnter()
    {
      if (this.currentTracker != null)
      {
        this.currentTracker.StartRecording();
      }
    }

    /// <summary>
    /// This method shows a <see cref="ScreenCaptureDialog"/> to enable customized settings
    /// of the screen capturing.
    /// </summary>
    private void ShowScreenCaptureDialog()
    {
      ScreenCaptureDialog dialog = new ScreenCaptureDialog();

      // Initialize dialog with defaults
      dialog.VideoCompressor = this.screenCaptureProperties.VideoCompressor;
      dialog.FrameRate = Document.ActiveDocument.ExperimentSettings.ScreenCaptureFramerate;
      if (dialog.ShowDialog() == DialogResult.OK)
      {
        this.screenCaptureProperties.VideoCompressor = dialog.VideoCompressor;
        this.screenCaptureProperties.FrameRate = dialog.FrameRate;
        Document.ActiveDocument.ExperimentSettings.ScreenCaptureFramerate = dialog.FrameRate;
      }
    }

    /// <summary>
    /// Creates the tracking interfaces for the selected
    /// tracking devices. If none is selected raises
    /// a <see cref="ShowTrackerSelectionDialog()"/>.
    /// </summary>
    private void CreateTrackerInterfaces()
    {
      // Release existing tracking devices
      this.DisposeTrackerInterfaces();

      // Reset tab control
      this.tclEyetracker.TabPages.Clear();
      this.tclEyetracker.TabPages.Add(this.tbpTobii);
      this.tclEyetracker.TabPages.Add(this.tbpAlea);
      this.tclEyetracker.TabPages.Add(this.tbpSMI);
      this.tclEyetracker.TabPages.Add(this.tbpMouseOnly);
      this.tclEyetracker.TabPages.Add(this.tbpITU);
      this.tclEyetracker.TabPages.Add(this.tbpITUPS3);

      // Read activated tracker value from the application settings
      string activatedTracker = Properties.Settings.Default.ActivatedHardwareTracker;
      HardwareTracker tracker = (HardwareTracker)Enum.Parse(typeof(HardwareTracker), activatedTracker);

      // Show dialog if no tracking device is selected.
      if (tracker == HardwareTracker.None)
      {
        tracker = this.ShowTrackerSelectionDialog();
        Properties.Settings.Default.ActivatedHardwareTracker = tracker.ToString();
        Properties.Settings.Default.Save();
      }

      this.tclEyetracker.SuspendLayout();

      if (tracker == (tracker | HardwareTracker.MouseOnly))
      {
        // Create mouse only tracker
        MouseOnlyTracker newMouseOnly = new MouseOnlyTracker(
          this,
          null,
          this.btnMouseOnlySubject,
          null,
          this.btnMouseOnlyRecord,
          this.txbMouseOnlySubjectName);

        this.trackerInterfaces.Add(HardwareTracker.MouseOnly, newMouseOnly);
      }
      else
      {
        if (this.tclEyetracker.TabPages.Contains(this.tbpMouseOnly))
        {
          this.tclEyetracker.TabPages.Remove(this.tbpMouseOnly);
        }
      }

      if (tracker == (tracker | HardwareTracker.Alea))
      {
        // Create alea tracker
        AleaTracker newAlea = new AleaTracker(
          ref this.labelCalibrationResult,
          this.tbpAlea,
          this,
          this.spcAleaControls,
          this.spcAleaTrackStatus.Panel1,
          this.spcAleaCalibPlot.Panel1,
          this.btnAleaShowOnPresentationScreen,
          this.btnAleaAcceptCalibration,
          this.btnAleaRecalibrate,
          this.btnAleaConnect,
          this.btnAleaSubjectName,
          this.btnAleaCalibrate,
          this.btnAleaRecord,
          this.txbAleaSubjectName);

        this.trackerInterfaces.Add(HardwareTracker.Alea, newAlea);
      }
      else
      {
        if (this.tclEyetracker.TabPages.Contains(this.tbpAlea))
        {
          this.tclEyetracker.TabPages.Remove(this.tbpAlea);
        }
      }

#if TOBII
      if (tracker == (tracker | HardwareTracker.Tobii))
      {
        // Create mouse only tracker
        TobiiTracker newTobii = new TobiiTracker(
          this,
          this.spcTobiiControls,
          this.spcTobiiTrackStatus.Panel1,
          this.spcTobiiCalibPlot.Panel1,
          this.btnTobiiShowOnPresentationScreen,
          this.btnTobiiAcceptCalibration,
          this.btnTobiiRecalibrate,
          this.btnTobiiConnect,
          this.btnTobiiSubjectName,
          this.btnTobiiCalibrate,
          this.btnTobiiRecord,
          this.txbTobiiSubjectName);

        this.trackerInterfaces.Add(HardwareTracker.Tobii, newTobii);
      }
      else
      {
        if (this.tclEyetracker.TabPages.Contains(this.tbpTobii))
        {
          this.tclEyetracker.TabPages.Remove(this.tbpTobii);
        }
      }
#else
      if (this.tclEyetracker.TabPages.Contains(this.tbpTobii))
      {
          this.tclEyetracker.TabPages.Remove(this.tbpTobii);
      }
#endif

      if (tracker == (tracker | HardwareTracker.SMI))
      {
        // Create mouse only tracker
        SMITracker newSMI = new SMITracker(
          this,
          this.btnSMIConnect,
          this.btnSMISubjectName,
          this.btnSMICalibrate,
          this.btnSMIRecord,
          this.txbSMISubjectName);

        this.trackerInterfaces.Add(HardwareTracker.SMI, newSMI);
      }
      else
      {
        if (this.tclEyetracker.TabPages.Contains(this.tbpSMI))
        {
          this.tclEyetracker.TabPages.Remove(this.tbpSMI);
        }
      }

      if (tracker == (tracker | HardwareTracker.ITU))
      {
        // Create ITU GazeTracker
        ITUGazeTracker.ITUGazeTrackerBase newITU = new ITUGazeTracker.ITUDefaultGazeTracker(
          this,
          this.spcITUControls,
          this.spcITUTrackStatus.Panel1,
          this.spcITUCalibPlot.Panel1,
          this.btnITUShowOnPresentationScreen,
          this.btnITUAcceptCalibration,
          this.btnITURecalibrate,
          this.btnITUConnect,
          this.btnITUCamera,
          this.btnITUAdjust,
          this.btnITUSubjectName,
          this.btnITUCalibrate,
          this.btnITURecord,
          this.txbITUSubjectName);

        this.trackerInterfaces.Add(HardwareTracker.ITU, newITU);
      }
      else
      {
        if (this.tclEyetracker.TabPages.Contains(this.tbpITU))
        {
          this.tclEyetracker.TabPages.Remove(this.tbpITU);
        }
      }

      if (tracker == (tracker | HardwareTracker.ITUPS3))
      {
        // Create ITU PS3 GazeTracker
        ITUGazeTracker.ITUPS3GazeTracker newITUPs3 = new ITUGazeTracker.ITUPS3GazeTracker(
          this,
          this.spcITUPS3Controls,
          this.spcITUPS3TrackStatus.Panel1,
          this.spcITUPS3CalibPlot.Panel1,
          this.btnITUPS3ShowOnPresentationScreen,
          this.btnITUPS3AcceptCalibration,
          this.btnITUPS3Recalibrate,
          this.btnITUPS3Connect,
          this.btnITUPS3Camera,
          this.btnITUPS3Adjust,
          this.btnITUPS3SubjectName,
          this.btnITUPS3Calibrate,
          this.btnITUPS3Record,
          this.txbITUPS3SubjectName);

        this.trackerInterfaces.Add(HardwareTracker.ITUPS3, newITUPs3);
      }
      else
      {
        if (this.tclEyetracker.TabPages.Contains(this.tbpITUPS3))
        {
          this.tclEyetracker.TabPages.Remove(this.tbpITUPS3);
        }
      }

      if (tracker == HardwareTracker.None)
      {
        this.tclEyetracker.TabPages.Add(this.tbpNoDevice);
      }

      this.tclEyetracker.ResumeLayout();

      this.SwitchCurrentTracker();
    }

    /// <summary>
    /// This method frees the resources used by the tracker interfaces
    /// by calling the <see cref="ITracker.CleanUp()"/> methods.
    /// </summary>
    private void DisposeTrackerInterfaces()
    {
      foreach (Tracker tracker in this.trackerInterfaces.Values)
      {
        tracker.Dispose();
      }

      this.trackerInterfaces.Clear();
    }

    /// <summary>
    /// This method shows a <see cref="SelectTracker"/>
    /// form to enable the user to select the available trackers.
    /// </summary>
    /// <returns>A <see cref="HardwareTracker"/> flags
    /// with the enabled tracking devices.</returns>
    private HardwareTracker ShowTrackerSelectionDialog()
    {
      SelectTracker dialog = new SelectTracker();
      if (dialog.ShowDialog() == DialogResult.OK)
      {
        return dialog.SelectedTracker;
      }

      return HardwareTracker.None;
    }

    /// <summary>
    /// This method initializes the presentation thread
    /// and raises an error if no slides are defined.
    /// </summary>
    /// <returns><strong>True</strong>, if presentation could be started,
    /// otherwise <strong>false</strong>.</returns>
    private bool StartPresentation()
    {
      if (CheckForModifiedSlideshow())
      {
        return false;
      }

      if (!CheckForCorrectPresentationScreenResolution())
      {
        return false;
      }

      this.InitializeScreenCapture();
      this.lblRecordedTime.Text = "00:00:00";
      this.xPosition = PresentationScreen.GetPresentationBounds().X;
      this.yPosition = PresentationScreen.GetPresentationBounds().Y;

      // Reread slideshow
      Slideshow slideshow = Document.ActiveDocument.ExperimentSettings.SlideShow;

      if (slideshow != null && slideshow.Slides.Count > 0)
      {
        ((MainForm)this.MdiParent).StatusLabel.Text = "Recording...";

        // Create a newly randomized trial list.
        this.trials = slideshow.GetRandomizedTrials();

        // Create a hardcopy of the trials.
        TrialCollection copyOfTrials = (TrialCollection)this.trials.Clone();

        // CaptureMode for usercam
        CaptureMode mode = CaptureMode.None;

        string userVideoFilename = Path.Combine(
          Document.ActiveDocument.ExperimentSettings.ThumbsPath,
          Document.ActiveDocument.SelectionState.SubjectName + ".avi");

        if (this.btnUsercam.Checked)
        {
          mode |= CaptureMode.VideoPreview;
        }

        if (this.chbRecordAudio.Checked)
        {
          mode |= CaptureMode.Audio;

          // Only set filname when recording is choosen
          this.webcamPreview.Properties.Filename = userVideoFilename;
        }

        if (this.chbRecordVideo.Checked)
        {
          mode |= CaptureMode.Video;

          // Only set filname when recording is choosen
          this.webcamPreview.Properties.Filename = userVideoFilename;
        }

        this.webcamPreview.Properties.CaptureMode = mode;

        List<object> threadParameters = new List<object>();
        threadParameters.Add(copyOfTrials);
        threadParameters.Add(this.generalTrigger);
        threadParameters.Add(this.btnTrigger.Checked);
        threadParameters.Add(this.screenCaptureProperties);
        threadParameters.Add(this.webcamPreview.Properties);
        threadParameters.Add(this.webcamPreview.DirectXCapture == null ? null : this.webcamPreview.DirectXCapture.PreviewWindow);

        // Stop webcam
        this.webcamPreview.Preview = false;

        // Start recording
        this.currentTrialVideoStartTime = 0;
        this.currentTracker.Record();

        // Wait for first sample to be received
        while (this.GetCurrentTime() < 0)
        {
          Application.DoEvents();
        }

        // Set recording flag
        this.recordingBusy = true;

        // Initialize presentation thread
        this.presentationThread = new Thread(new ParameterizedThreadStart(this.PresentationThread_DoWork));
        this.presentationThread.SetApartmentState(ApartmentState.STA);
        this.presentationThread.Start(threadParameters);

        // Start presentation has ended timer
        this.tmrWaitForPresentationEnd.Start();

        // Update live viewer and slide counter
        this.NewSlideAvailable();
      }
      else
      {
        string message = "Could not start presentation, because there are no slides defined." +
          Environment.NewLine + "Please use the Slides Design Interface";
        InformationDialog.Show("No slides available", message, false, MessageBoxIcon.Warning);

        return false;
      }

      return true;
    }

    /// <summary>
    /// This method updates the live viewer of the presentation module.
    /// It sets the <see cref="currentSlide"/> according to <see cref="trialCounter"/>,
    /// loads flash objects and invalidates itself.
    /// </summary>
    /// <remarks>If the <see cref="trialCounter"/> is 0 the panel 
    /// will show the <see cref="defaultSlide"/> which has a recording instruction,
    /// if it is 1 the initialization slide is shown.</remarks>
    private void NewSlideAvailable()
    {
      if (this.currentTrial != null)
      {
        // Set current slide
        this.currentSlide = this.currentTrial[this.slideCounter];
      }
      else
      {
        this.currentSlide = defaultSlide;
      }

      if (this.systemHasSecondaryScreen || this.forcePanelViewerUpdate)
      {
        // Load Slide into picture
        this.LoadSlide(this.currentSlide, ActiveXMode.Off);

        // Show or hide duplicated mouse cursor
        this.recordPicture.MouseCursorVisible = this.currentSlide.MouseCursorVisible;
      }
    }

    /// <summary>
    /// This method writes the data that is written in the lists during
    /// recording to OGAMAs dataset.
    /// If this could be succesfully done the whole new data is
    /// written to the database (.mdf).
    /// </summary>
    private void SaveRecordingIntoTablesAndDB()
    {
      try
      {
        // Set wait cursor until UI is reset
        Cursor.Current = Cursors.WaitCursor;

        string subject = this.currentTracker.Subject.SubjectName;

        this.bgwSaveSplash.RunWorkerAsync("Saving to database ...");

        // Creates an empty raw data table in the mdf database
        Queries.CreateRawDataTableInDB(subject);

        // Write RawDataTable into File with Bulk Statement
        Queries.WriteRawDataWithBulkStatement(subject, this.subjectRawDataTable);

        // Save subject information to dataset
        if (!Queries.WriteSubjectToDataSet(this.currentTracker.Subject))
        {
          throw new DataException("The new subject information could not be written into the dataset.");
        }

        // Save trial information to dataset
        if (!Queries.WriteTrialsDataListToDataSet(this.trialDataList))
        {
          throw new DataException("The new trials table could not be written into the dataset.");
        }

        // Save trial event information to dataset
        if (!Queries.WriteTrialEventsToDataSet(this.trialEventList))
        {
          throw new DataException("The new trials table could not be written into the dataset.");
        }

        Document.ActiveDocument.DocDataSet.EnforceConstraints = false;

        // Update subjects and trials table in the mdf database
        int affectedRows = Document.ActiveDocument.DocDataSet.TrialEventsAdapter.Update(Document.ActiveDocument.DocDataSet.TrialEvents);
        affectedRows = Document.ActiveDocument.DocDataSet.TrialsAdapter.Update(Document.ActiveDocument.DocDataSet.Trials);
        affectedRows = Document.ActiveDocument.DocDataSet.SubjectsAdapter.Update(Document.ActiveDocument.DocDataSet.Subjects);

        Document.ActiveDocument.DocDataSet.AcceptChanges();
        Document.ActiveDocument.DocDataSet.CreateRawDataAdapters();

        Document.ActiveDocument.DocDataSet.EnforceConstraints = true;

        // Get trial data of current subject
        DataTable trialsTable = Document.ActiveDocument.DocDataSet.TrialsAdapter.GetDataBySubject(subject);

        // Hide splash
        this.bgwSaveSplash.CancelAsync();

        // Show new splash new info
        this.bgwCalcFixations.RunWorkerAsync("Calculating fixations ...");
        Application.DoEvents();

        // Calculate fixations
        FixationCalculation calculationObject = new FixationCalculation();
        calculationObject.CalcFixations(SampleType.Gaze, subject, trialsTable, null, null);
        calculationObject.CalcFixations(SampleType.Mouse, subject, trialsTable, null, null);

        this.bgwCalcFixations.CancelAsync();

        // Show the success information on the 
        // controllers screen
        SavingSuccessDialog successDlg = new SavingSuccessDialog();
        Rectangle controllerBounds = PresentationScreen.GetControllerWorkingArea();
        successDlg.Location =
          new Point(
          controllerBounds.Width / 2 - successDlg.Width / 2,
          controllerBounds.Height / 2 - successDlg.Height / 2);
        successDlg.ShowDialog();
      }
      catch (Exception ex)
      {
        ExceptionMethods.HandleException(ex);

        // Hide saving splash.
        if (this.bgwSaveSplash.IsBusy)
        {
          this.bgwSaveSplash.CancelAsync();
        }

        // Hide fixation calculation splash.
        if (this.bgwCalcFixations.IsBusy)
        {
          this.bgwCalcFixations.CancelAsync();
        }

        // CleanUp
        // First reject changes (remove trial and subject table modifications)
        Document.ActiveDocument.DocDataSet.RejectChanges();

        Document.ActiveDocument.DocDataSet.EnforceConstraints = false;

        int affectedRows = Document.ActiveDocument.DocDataSet.SubjectsAdapter.Update(Document.ActiveDocument.DocDataSet.Subjects);
        affectedRows = Document.ActiveDocument.DocDataSet.TrialsAdapter.Update(Document.ActiveDocument.DocDataSet.Trials);
        affectedRows = Document.ActiveDocument.DocDataSet.TrialEventsAdapter.Update(Document.ActiveDocument.DocDataSet.TrialEvents);

        // Remove eventually added raw data table in dataset
        if (Document.ActiveDocument.DocDataSet.Tables.Contains(this.currentTracker.Subject.SubjectName + "Rawdata"))
        {
          Document.ActiveDocument.DocDataSet.Tables.Remove(this.currentTracker.Subject.SubjectName + "Rawdata");
        }

        Document.ActiveDocument.DocDataSet.EnforceConstraints = true;

        // Remove raw data table in database file (.mdf)
        Queries.DeleteRawDataTableInDB(this.currentTracker.Subject.SubjectName);

        string filename = Path.Combine(Document.ActiveDocument.ExperimentSettings.ThumbsPath, this.currentTracker.Subject.SubjectName + ".avi");
        if (File.Exists(filename))
        {
          File.Delete(filename);
        }
      }
      finally
      {
        // Free resources
        this.trialEventList.Clear();
        this.trialDataList.Clear();
        this.subjectRawDataTable.Dispose();
      }
    }

    /// <summary>
    /// This method resets the recording module by calling
    /// <see cref="ITracker.Stop()"/> for the current tracker.
    /// </summary>
    private void ResetRecordInterface()
    {
      this.currentTracker.Stop();

      ((MainForm)this.MdiParent).StatusLabel.Text = "Ready...";
      this.currentSlide = null;

      // Reset counters
      this.trialCounter = -1;
      this.slideCounter = 0;
      this.recordingStarttime = -5;
      this.lastTimeStamp = -1;

      this.currentTrial = null;
      this.currentSlide = defaultSlide;

      this.recordTimerWatch.Reset();
      this.tmrRecordClock.Stop();
      this.tmrRecordClock.Dispose();

      this.tmrRecordClock = new System.Windows.Forms.Timer();
      this.tmrRecordClock.Interval = 1000;
      this.tmrRecordClock.Tick += new EventHandler(this.tmrRecordClock_Tick);

      // Stop updating viewer
      this.Picture.StopAnimation();
      this.Picture.ResetPicture();

      // Redraw panel
      this.NewSlideAvailable();
    }

    /// <summary>
    /// This method sets the capture mode according to flash content.
    /// </summary>
    private void InitializeScreenCapture()
    {
      // Only needed if there is a flash movie.
      if (!Document.ActiveDocument.ExperimentSettings.SlideShow.HasFlashContent())
      {
        this.screenCaptureProperties.CaptureMode = CaptureMode.None;
        return;
      }
      else
      {
        this.screenCaptureProperties.CaptureMode = CaptureMode.Video;
      }
    }

    /// <summary>
    /// This method reads the currently selected tracker tab
    /// and sets the <see cref="currentTracker"/> to the 
    /// referring value.
    /// </summary>
    private void SwitchCurrentTracker()
    {
      if (this.tclEyetracker.SelectedTab == null)
      {
        return;
      }

      switch (this.tclEyetracker.SelectedTab.Name)
      {
#if TOBII
        case "tbpTobii":
          if (this.trackerInterfaces.ContainsKey(HardwareTracker.Tobii))
          {
            this.currentTracker = this.trackerInterfaces[HardwareTracker.Tobii];
          }

          break;
#endif
        case "tbpAlea":
          if (this.trackerInterfaces.ContainsKey(HardwareTracker.Alea))
          {
            this.currentTracker = this.trackerInterfaces[HardwareTracker.Alea];
          }

          break;
        case "tbpMouseOnly":
          if (this.trackerInterfaces.ContainsKey(HardwareTracker.MouseOnly))
          {
            this.currentTracker = this.trackerInterfaces[HardwareTracker.MouseOnly];
          }

          break;
        case "tbpSMI":
          if (this.trackerInterfaces.ContainsKey(HardwareTracker.SMI))
          {
            this.currentTracker = this.trackerInterfaces[HardwareTracker.SMI];
          }

          break;
        case "tbpITU":
          if (this.trackerInterfaces.ContainsKey(HardwareTracker.ITU))
          {
            this.currentTracker = this.trackerInterfaces[HardwareTracker.ITU];
          }

          break;
        case "tbpITUPS3":
          if (this.trackerInterfaces.ContainsKey(HardwareTracker.ITUPS3))
          {
            this.currentTracker = this.trackerInterfaces[HardwareTracker.ITUPS3];
          }

          break;
      }
    }

    #endregion //METHODS

    ///////////////////////////////////////////////////////////////////////////////
    // Small helping Methods                                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region HELPER

    /// <summary>
    /// Method to return the current timing of the tracking system.
    /// </summary>
    /// <returns>A <see cref="Int64"/> with the time in milliseconds.</returns>
    private long GetCurrentTime()
    {
      // set lock for lastTimeStamp value
      lock (this.timeLock)
      {
        return this.lastTimeStamp;
      }
    }

    /// <summary>
    /// This method updates the current application settings
    /// with the newly selected presentation screen monitor.
    /// </summary>
    private void SubmitPresentationScreenToSettings()
    {
      if (this.btnPrimary.Checked)
      {
        Settings.Default.PresentationScreenMonitor = "Primary";
      }
      else
      {
        Settings.Default.PresentationScreenMonitor = "Secondary";
      }

      Settings.Default.Save();
      this.InitializeScreenCapture();
    }

    /// <summary>
    /// This method is a quick check whether two points are near.
    /// It uses the <see cref="MAXDISTANCENEAR"/> constant to check.
    /// </summary>
    /// <param name="firstPt">First point</param>
    /// <param name="secondPt">Second Point</param>
    /// <returns><strong>True</strong> if points are near, otherwise <strong>false</strong>.</returns>
    private bool PointsArNear(PointF firstPt, PointF secondPt)
    {
      if (Math.Abs(firstPt.X - secondPt.X) < MAXDISTANCENEAR)
      {
        if (Math.Abs(firstPt.Y - secondPt.Y) < MAXDISTANCENEAR)
        {
          return true;
        }
      }

      return false;
    }

    /// <summary>
    /// This method creates a new <see cref="RawData"/> with the
    /// event id and sends it to be recorded into the dataset
    /// by a call of <see cref="NewRawDataAvailable"/>.
    /// </summary>
    /// <param name="inputEvent">A <see cref="TrialEvent"/> with the 
    /// event data.</param>
    private void WriteTrialEventToRawData(TrialEvent inputEvent)
    {
      // Create a new RawData object
      RawData newRawData = new RawData();

      // Fill its members with current values.
      newRawData.SubjectName = this.currentTracker.Subject.SubjectName;
      newRawData.TrialSequence = this.trialCounter;
      newRawData.Time = inputEvent.Time + this.currentTrialStarttime;
      newRawData.EventID = inputEvent.EventID;

      // Raise the NewRawDataAvailable event with the new data
      // to submit its contents to the database.
      this.OnNewRawDataAvailable(new NewRawDataAvailableEventArgs(newRawData));
    }

    /// <summary>
    /// Returns the current mouse cursor position,
    /// or null if mouse is out of the bounds of the presentation screen.
    /// </summary>
    /// <returns>A <see cref="PointF"/> with the current mouse cursor position,
    /// or <strong>null</strong> if the mouse is out of presentation screen.</returns>
    private PointF? GetMousePosition()
    {
      PointF newMousePos = new PointF();

      // Get Mouse State.
      Point mousePos = Control.MousePosition;
      newMousePos.X = (float)(mousePos.X - this.xPosition);
      newMousePos.Y = (float)(mousePos.Y - this.yPosition);

      return newMousePos;
    }

    /// <summary>
    /// This method updates the label with the recording timer
    /// in a thread safe way.
    /// </summary>
    /// <param name="stringToSet">A <see cref="String"/> with the new recording time.</param>
    private void ThreadSafeUpdateLabel(string stringToSet)
    {
      if (this.lblRecordedTime.InvokeRequired)
      {
        this.lblRecordedTime.Invoke(new SetStringInvoker(this.ThreadSafeUpdateLabel), stringToSet);
      }
      else
      {
        this.lblRecordedTime.Text = stringToSet;
      }
    }

    #endregion //HELPER
  }
}
