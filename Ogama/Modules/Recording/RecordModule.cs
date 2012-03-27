// <copyright file="RecordModule.cs" company="FU Berlin">
// ******************************************************
// OGAMA - open gaze and mouse analyzer 
// Copyright (C) 2012 Adrian Voßkühler  
// ------------------------------------------------------------------------
// This program is free software; you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation; either version 2 of the License, or (at your option) any later version.
// This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
// You should have received a copy of the GNU General Public License along with this program; if not, write to the Free Software Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA 02111-1307 USA
// **************************************************************
// </copyright>
// <author>Adrian Voßkühler</author>
// <email>adrian@ogama.net</email>

namespace Ogama.Modules.Recording
{
  using System;
  using System.Collections.Generic;
  using System.ComponentModel;
  using System.Data;
  using System.Diagnostics;
  using System.Drawing;
  using System.IO;
  using System.Linq;
  using System.Threading;
  using System.Windows.Forms;
  using GTCommons.Events;
  using Ogama.DataSet;
  using Ogama.ExceptionHandling;
  using Ogama.MainWindow;
  using Ogama.Modules.Common;
  using Ogama.Modules.Common.CustomEventArgs;
  using Ogama.Modules.Common.FormTemplates;
  using Ogama.Modules.Common.SlideCollections;
  using Ogama.Modules.Common.Tools;
  using Ogama.Modules.Common.TrialEvents;
  using Ogama.Modules.Common.Types;
  using Ogama.Modules.Fixations;
  using Ogama.Modules.ImportExport.Common;
  using Ogama.Modules.Recording.AleaInterface;
  using Ogama.Modules.Recording.ASLInterface;
  using Ogama.Modules.Recording.Dialogs;
  using Ogama.Modules.Recording.GazegroupInterface;
  using Ogama.Modules.Recording.MirametrixInterface;
  using Ogama.Modules.Recording.MouseOnlyInterface;
  using Ogama.Modules.Recording.Presenter;
  using Ogama.Modules.Recording.SMIInterface;
  using Ogama.Modules.Recording.TobiiInterface;
  using Ogama.Modules.Recording.TrackerBase;
  using Ogama.Properties;
  using OgamaControls;
  using VectorGraphics.Elements;
  using VectorGraphics.Elements.ElementCollections;
  using VectorGraphics.StopConditions;
  using VectorGraphics.Tools;
  using VectorGraphics.Tools.Trigger;

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
    /// This is a slide that shows a short recording instruction
    /// when starting the module.
    /// </summary>
    private static readonly Slide DefaultSlide = new Slide(
      "Background",
      Color.Black,
      Images.CreateRecordInstructionImage(Document.ActiveDocument.ExperimentSettings.WidthStimulusScreen, Document.ActiveDocument.ExperimentSettings.HeightStimulusScreen),
      new StopConditionCollection(),
      null,
      string.Empty,
      Document.ActiveDocument.PresentationSize);

    /// <summary>
    /// This value sets the maximal distance for two points
    /// that are considered to be near in pixel.
    /// </summary>
    private const int Maxdistancenear = 30;

    /// <summary>
    /// Number of tables that are used to bulk copy the data into the
    /// database from the dataset.
    /// </summary>
    private const int Numwritingthreads = 5;

    /// <summary>
    /// When the received samples exceed the following limit after trial 
    /// has changed, then the data is written to the database,
    /// freeing memory.
    /// </summary>
    private const int Minsamplesforwritingtodatabase = 1000;

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
    private volatile int trialSequenceCounter;

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
    /// Saves the x-Offset for scrolled pages.
    /// </summary>
    private int xScrollOffset;

    /// <summary>
    /// Saves the y-Offset for scrolled pages.
    /// </summary>
    private int yScrollOffset;

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

      this.Picture = this.recordPicture;
      this.ZoomTrackBar = this.trbZoom;

      this.InitAccelerators();
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
    /// Gets the form of the presenter module if presentation is running.
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
        this.subjectRawDataTable = new OgamaDataSet.RawdataDataTable
          {
            TableName = this.currentTracker.Subject.SubjectName + "Rawdata"
          };

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
    public void TrackerGazeDataChanged(object sender, GazeDataChangedEventArgs e)
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
      var newRawData = new RawData
        {
          SubjectName = this.currentTracker.Subject.SubjectName,
          GazePosX = e.Gazedata.GazePosX,
          GazePosY = e.Gazedata.GazePosY
        };

      // The GazePos data is in values from 0 to 1
      // so scale it to SCREEN COORDINATES
      // and add optional scroll offset
      if (newRawData.GazePosX != null)
      {
        newRawData.GazePosX = newRawData.GazePosX * this.xResolution + this.xScrollOffset;
      }

      if (newRawData.GazePosY != null)
      {
        newRawData.GazePosY = newRawData.GazePosY * this.yResolution + this.yScrollOffset;
      }

      newRawData.PupilDiaX = e.Gazedata.PupilDiaX;
      newRawData.PupilDiaY = e.Gazedata.PupilDiaY;

      var gazeTime = e.Gazedata.Time - this.recordingStarttime;
      newRawData.Time = gazeTime;
      newRawData.TrialSequence = this.trialSequenceCounter;

      // Retrieve mouse position, this is already in SCREEN COORDINATES
      var newMousePos = this.GetMousePosition();
      if (this.currentSlide.MouseCursorVisible && newMousePos != null)
      {
        newRawData.MousePosX = newMousePos.Value.X + this.xScrollOffset;
        newRawData.MousePosY = newMousePos.Value.Y + this.yScrollOffset;
      }

      // Update gaze cursor with reduced update rate
      // if (_counter % REDUCING_COUNT == 0)
      if (newRawData.GazePosX != null && newRawData.GazePosY != null)
      {
        var newCursorPos = new PointF(
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
    protected override sealed void InitializeCustomElements()
    {
      base.InitializeCustomElements();

      this.btnHelp.Click += this.btnHelp_Click;
      this.pnlCanvas.Resize += this.pnlCanvas_Resize;

      // Sets the counter value for updating mouse and gaze cursor.
      // Should give a frame rate of about 20 Hz
      // So if GazeSamping rate is 60 Hz to get 20 Hz it should be set to 3.
      this.xResolution = Document.ActiveDocument.ExperimentSettings.WidthStimulusScreen;
      this.yResolution = Document.ActiveDocument.ExperimentSettings.HeightStimulusScreen;
      this.xPosition = PresentationScreen.GetPresentationBounds().X;
      this.yPosition = PresentationScreen.GetPresentationBounds().Y;

      this.systemHasSecondaryScreen = SecondaryScreen.SystemHasSecondaryScreen();

      this.delegateNewSlideAvailable = this.NewSlideAvailable;

      this.trialDataList = new List<TrialsData>();
      this.trialEventList = new List<TrialEvent>();

      this.slideCounter = 0;
      this.trialSequenceCounter = -1;
      this.recordingStarttime = -5;
      this.timeLock = new object();
      this.lastTimeStamp = -1;

      this.watch = new Stopwatch();
      this.recordTimerWatch = new Stopwatch();
      ////this.watch.Start();

      this.generalTrigger = new Trigger
        {
          OutputDevice = TriggerOutputDevices.LPT,
          Signaling = TriggerSignaling.None,
          SignalingTime = 10,
          Value = 255,
          PortAddress = 0x0378
        };

      // Take primary monitor
      var monitorIndex = 0;
      if (PresentationScreen.GetPresentationScreen() != Screen.PrimaryScreen)
      {
        // otherwise take the secondary sceen.
        monitorIndex = 1;
      }

      this.screenCaptureProperties = new ScreenCaptureProperties(
        "OgamaScreenCapture Filter",
        string.Empty,
        "ffdshow video encoder",
        string.Empty,
        10,
        Document.ActiveDocument.PresentationSize,
        string.Empty,
        CaptureMode.Video,
        monitorIndex,
        this.recordPicture);

      this.rawDataLists = new List<RawData>[Numwritingthreads];
      for (var i = 0; i < Numwritingthreads; i++)
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
    protected override sealed void InitAccelerators()
    {
      base.InitAccelerators();
      this.SetAccelerator(Keys.Enter, this.OnPressEnter);
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
    private void RecordModuleLoad(object sender, EventArgs e)
    {
      this.smoothing = false;

      // Intialize replay picture
      this.recordPicture.OwningForm = this;
      this.recordPicture.PresentationSize = Document.ActiveDocument.PresentationSize;
      this.ResizeCanvas();

      this.InitializeScreenCapture();
      this.CreateTrackerInterfaces();

      // Use panel update always at start
      this.forcePanelViewerUpdate = true;

      this.NewSlideAvailable();

      // Reset the panel update, only used in debugging mode
      // this.forcePanelViewerUpdate = true;
      this.forcePanelViewerUpdate = false;

      // Uncheck Usercam button, if there is no available webcam
      // except Ogama Screen Capture which should not be used.
      if (this.webcamPreview.DirectXCapture == null)
      {
        this.btnUsercam.Checked = false;
        this.spcPanelUserCam.Panel2Collapsed = true;
      }
    }

    /// <summary>
    /// The <see cref="Form.FormClosing"/> event handler.
    /// Cleans up interfaces and checks for running presentations.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">A <see cref="FormClosingEventArgs"/> with the event data.</param>
    private void RecordModuleFormClosing(object sender, FormClosingEventArgs e)
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
    private void BtnPrimaryClick(object sender, EventArgs e)
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
    private void BtnSecondaryClick(object sender, EventArgs e)
    {
      this.btnPrimary.Checked = !this.btnSecondary.Checked;
      this.SubmitPresentationScreenToSettings();
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler for the
    /// <see cref="Button"/> <see cref="btnWebcamSettings"/>.
    /// Calls the <see cref="Webcam.ShowConfigureDialog(bool)"/> method
    /// for webcam specific settings modification.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/>.</param>
    private void BtnWebcamSettingsClick(object sender, EventArgs e)
    {
      this.webcamPreview.ShowConfigureDialog(this.btnUsercam.Checked);
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler for the
    /// <see cref="Button"/> <see cref="btnTriggerSettings"/>.
    /// Shows a <see cref="TriggerDialog"/> to specify a general trigger.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/>.</param>
    private void BtnTriggerSettingsClick(object sender, EventArgs e)
    {
      using (var dlg = new TriggerDialog())
      {
        dlg.TriggerSignal = this.generalTrigger;
        if (dlg.ShowDialog() == DialogResult.OK)
        {
          this.generalTrigger = dlg.TriggerSignal;
        }
      }
    }

    /// <summary>
    /// The <see cref="TabControl.SelectedIndexChanged"/> event handler for the
    /// <see cref="TabControl"/> <see cref="tclEyetracker"/>.
    /// Switches the used <see cref="ITracker"/> according to selected tab.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/>.</param>
    private void TclEyetrackerSelectedIndexChanged(object sender, EventArgs e)
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
    private void BtnTrackerSettingsClick(object sender, EventArgs e)
    {
      if (this.currentTracker != null)
      {
        this.currentTracker.ChangeSettings();
      }
      else
      {
        const string Message = "Please choose a tracker system before changing settings, "
                               + "by selecting a tracker tab at the left side of the module";
        ExceptionMethods.ProcessMessage("No tracker selected", Message);
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
    private void BtnScreenCaptureSettingsClick(object sender, EventArgs e)
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
    private void BtnSelectTrackerClick(object sender, EventArgs e)
    {
      var tracker = this.ShowTrackerSelectionDialog();
      switch (tracker)
      {
        case HardwareTracker.None:
          return;
      }

      Settings.Default.ActivatedHardwareTracker = tracker.ToString();
      Settings.Default.Save();
      this.CreateTrackerInterfaces();
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
    private void BtnNoDeviceTabSelectTrackerClick(object sender, EventArgs e)
    {
      var tracker = this.ShowTrackerSelectionDialog();
      switch (tracker)
      {
        case HardwareTracker.None:
          return;
      }

      Settings.Default.ActivatedHardwareTracker = tracker.ToString();
      Settings.Default.Save();
      this.CreateTrackerInterfaces();
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler for the
    /// <see cref="Button"/> <see cref="btnUsercam"/>.
    /// Shows or hides the webcam control.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/>.</param>
    private void BtnUsercamClick(object sender, EventArgs e)
    {
      if (this.btnUsercam.Checked)
      {
        // Check for available camera
        if (!this.webcamPreview.TestCapture())
        {
          this.btnUsercam.Checked = false;
          var message = "It seems that your system has no webcam plugged in, " +
            "so user camera recording is not available at the moment. " +
            Environment.NewLine +
            "Try again after plugging in a camera or change the capture device.";
          ExceptionMethods.ProcessMessage("No webcam found", message);
        }
        else
        {
          this.webcamPreview.Preview();
        }
      }
      else
      {
        this.chbRecordVideo.Checked = false;
        this.chbRecordAudio.Checked = false;
        if (this.webcamPreview.DirectXCapture != null)
        {
          this.webcamPreview.DisposeDxCapture();
        }
      }

      this.spcPanelUserCam.Panel2Collapsed = !this.btnUsercam.Checked;
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler for the
    /// <see cref="Button"/> <see cref="btnSmoothing"/>.
    /// Switches the smoothing mode of the gaze cursor.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/>.</param>
    private void BtnSmoothingClick(object sender, EventArgs e)
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
    private void ObjPresenterTrialEventOccured(object sender, TrialEventOccuredEventArgs e)
    {
      // Save scroll offsets for raw data transformation
      if (e.TrialEvent.Type == EventType.Scroll)
      {
        var scrollOffsets = e.TrialEvent.Param.Split(';');
        lock (this)
        {
          this.xScrollOffset = Convert.ToInt32(scrollOffsets[0]);
          this.yScrollOffset = Convert.ToInt32(scrollOffsets[1]);
        }

        var newScrollPosition = new Point(this.xScrollOffset, this.yScrollOffset);
        this.ThreadSafeSetAutoScrollPosition(newScrollPosition);
      }

      var time = e.EventTime - this.recordingStarttime - this.currentTrialStarttime;

      // Store trial event event
      e.TrialEvent.EventID = this.trialEventList.Count;
      e.TrialEvent.Time = time;
      e.TrialEvent.TrialSequence = this.trialSequenceCounter;
      e.TrialEvent.SubjectName = this.currentTracker.Subject.SubjectName;

      if (this.trialSequenceCounter < 0)
      {
        return;
      }

      this.trialEventList.Add(e.TrialEvent);
      if (e.TrialEvent.Type != EventType.Marker)
      {
        this.WriteTrialEventToRawData(e.TrialEvent);
      }
    }

    /// <summary>
    /// The <see cref="PresenterModule.CounterChanged"/> event handler.
    /// Indicates a slide change in the presenter, so update trial and slide
    /// counter and the live viewer.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">A <see cref="CounterChangedEventArgs"/> with the event data.</param>
    private void ObjPresenterCounterChanged(object sender, CounterChangedEventArgs e)
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
        this.slideCounter = e.SlideCounter;

        // Don´t use presenters trialCounter
        // Because of using links between trials.
        // and care of trials with multiple slides
        // don´t increase trial sequence counter on this.
        if (this.slideCounter == 0)
        {
          this.trialSequenceCounter++;
        }

        // Set current trial
        this.currentTrial = e.TrialID != -5 ? this.trials[this.trials.GetIndexOfTrialByID(e.TrialID)] : null;

        // Set current slide
        this.currentSlide = this.currentTrial != null ? this.currentTrial[this.slideCounter] : null;
      }
    }

    /// <summary>
    /// The <see cref="PresenterModule.TrialEventOccured"/> event handler.
    /// Stores the new trial event into the list.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">A <see cref="TrialEventOccuredEventArgs"/> with the event data.</param>
    private void ObjPresenterSlideChanged(object sender, SlideChangedEventArgs e)
    {
      var time = this.counterChangedTime - this.recordingStarttime - this.currentTrialStarttime;

      // Store slide changed event
      var slideChangedEvent = new MediaEvent
        {
          EventID = this.trialEventList.Count,
          Param = e.SlideCounter + "#" + e.NextSlide.Name,
          Task = MediaEventTask.Show,
          Time = time,
          Type = EventType.Slide,
          SubjectName = this.currentTracker.Subject.SubjectName,
          TrialSequence = this.trialSequenceCounter
        };

      if (this.trialSequenceCounter >= 0)
      {
        this.trialEventList.Add(slideChangedEvent);
        this.WriteTrialEventToRawData(slideChangedEvent);
      }

      // Store subjects response event
      var inputEvent = new InputEvent { EventID = this.trialEventList.Count };
      if (e.Response != null)
      {
        inputEvent.Param = e.Response.ToString();
      }

      inputEvent.SubjectName = this.currentTracker.Subject.SubjectName;
      inputEvent.Task = InputEventTask.SlideChange;
      inputEvent.Time = time;
      inputEvent.TrialSequence = this.trialSequenceCounter;
      inputEvent.Type = EventType.Response;

      if (this.trialSequenceCounter >= 0)
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
    private void ObjPresenterTrialChanged(object sender, TrialChangedEventArgs e)
    {
      // Set time critical values
      long currentTime = this.counterChangedTime;
      this.xScrollOffset = 0;
      this.yScrollOffset = 0;

      if (e.FinishedTrial.Name != "DummyTrial")
      {
        // Update current trial
        this.precedingTrial = e.FinishedTrial;

        // When rawData list exceeds sample limit or this was the last trial
        // write the samples into the database
        if (this.rawDataLists[this.listCounter].Count > Minsamplesforwritingtodatabase
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
              new WaitCallback(StoreRecordsInDataSetTable),
              new DataToTable(this.rawDataLists[this.listCounter], this.subjectRawDataTable));

            this.listCounter++;
            if (this.listCounter == Numwritingthreads)
            {
              this.listCounter = 0;
            }
          }
        }

        // Write new trial information
        var trialData = new TrialsData
          {
            SubjectName = this.currentTracker.Subject.SubjectName,
            TrialName = this.precedingTrial.Name,
            TrialSequence = this.trialSequenceCounter - 1,
            TrialID = this.precedingTrial.ID,
            Category = this.precedingTrial[0].Category,
            TrialStartTime = this.currentTrialStarttime,
            Duration = (int)(currentTime - this.recordingStarttime - this.currentTrialStarttime)
          };

        if (this.trialSequenceCounter > 0)
        {
          this.trialDataList.Add(trialData);
        }

        // Store usercam start event if applicable
        if (this.chbRecordAudio.Checked || this.chbRecordVideo.Checked)
        {
          var usercamVideoEvent = new MediaEvent
            {
              EventID = this.trialEventList.Count,
              Param = this.currentTrialVideoStartTime.ToString(),
              Task = MediaEventTask.Start,
              Time = 0,
              Type = EventType.Usercam,
              SubjectName = this.currentTracker.Subject.SubjectName,
              TrialSequence = this.trialSequenceCounter - 1
            };

          if (this.trialSequenceCounter > 0)
          {
            this.trialEventList.Add(usercamVideoEvent);
          }
        }

        // Store subjects response event
        var inputEvent = new InputEvent
          {
            EventID = this.trialEventList.Count,
            SubjectName = this.currentTracker.Subject.SubjectName,
            Task = InputEventTask.SlideChange,
            Time = trialData.Duration,
            TrialSequence = this.trialSequenceCounter - 1,
            Type = EventType.Response
          };

        if (e.Response != null)
        {
          inputEvent.Param = e.Response.ToString();
        }

        if (this.trialSequenceCounter >= 0)
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

      // Update recorder modules viewer
      var updateLiveViewerThread = new Thread(this.NewSlideAvailable);
      updateLiveViewerThread.SetApartmentState(ApartmentState.STA);
      updateLiveViewerThread.Start();
    }

    /// <summary>
    /// This static method performs the writing of the copied raw data list into the
    /// table.
    /// </summary>
    /// <param name="stateInfo">An <see cref="Object"/> with the thread param which is 
    /// a <see cref="List{RawData}"/></param>
    private static void StoreRecordsInDataSetTable(object stateInfo)
    {
      var data = (DataToTable)stateInfo;

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
    private void ObjPresenterPresentationDone(object sender, EventArgs e)
    {
      try
      {
        // Wait for writing of raw data finishes
        var writingFinished = false;
        while (!writingFinished)
        {
          var dataFound = false;
          if (this.rawDataLists.Any(rawData => rawData.Count > 0))
          {
            Application.DoEvents();
            Thread.Sleep(500);
            dataFound = true;
          }

          if (!dataFound)
          {
            writingFinished = true;
          }
        }

        // Resets the interface
        this.ResetRecordInterface();

        // Show the question for saving the new sampling data on the 
        // controllers screen
        var saveDlg = new AskSaveDataDialog();
        var controllerBounds = PresentationScreen.GetControllerWorkingArea();
        var centerControllerScreen = new Point(
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
    private void RecordModuleNewRawDataAvailable(object sender, NewRawDataAvailableEventArgs e)
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
    private void WebcamPreviewWebcamAvailable(object sender, CaptureModeEventArgs e)
    {
      // Show user cam by default
      this.spcPanelUserCam.Panel2Collapsed = false;
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
    private void TmrWaitForPresentationEndTick(object sender, EventArgs e)
    {
      if (!this.recordingBusy)
      {
        // Restart usercam preview
        if (this.btnUsercam.Checked)
        {
          CaptureDeviceProperties currentProperties = this.webcamPreview.Properties;
          currentProperties.Filename = string.Empty;
          currentProperties.CaptureMode = CaptureMode.AudioVideoPreview;
          this.webcamPreview.Properties = currentProperties;
          this.webcamPreview.RunGraph();
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
    private void TmrRecordClockTick(object sender, EventArgs e)
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
    private void PresentationThreadDoWork(object data)
    {
      // Create presentation form
      this.presenterForm = new PresenterModule();

      // Cast thread data.
      var threadParams = (List<object>)data;
      var trialCollection = threadParams[0] as TrialCollection;
      var trigger = threadParams[1] as Trigger;
      var enableTrigger = (bool)threadParams[2];
      var captureProperties = (ScreenCaptureProperties)threadParams[3];

      CaptureDeviceProperties userCameraProperties = null;

      if (threadParams.Count > 4)
      {
        userCameraProperties = (CaptureDeviceProperties)threadParams[4];
      }

      GetTimeDelegate getTimeMethod = this.GetCurrentTime;

      // Set triggering state
      this.presenterForm.EnableTrigger = enableTrigger;

      // Set general trigger
      this.presenterForm.GeneralTrigger = trigger;

      // Set slide list of presenter
      this.presenterForm.TrialList = trialCollection;

      // Set screen capture device properties
      this.presenterForm.ScreenCaptureProperties = captureProperties;

      // Set user camera properties
      this.presenterForm.UserCameraProperties = userCameraProperties;

      // sets the get time method
      this.presenterForm.GetTimeMethod = getTimeMethod;

      // Wire presenter events
      this.presenterForm.CounterChanged += this.ObjPresenterCounterChanged;
      this.presenterForm.SlideChanged += this.ObjPresenterSlideChanged;
      this.presenterForm.TrialChanged += this.ObjPresenterTrialChanged;
      this.presenterForm.TrialEventOccured += this.ObjPresenterTrialEventOccured;
      this.presenterForm.PresentationDone += this.ObjPresenterPresentationDone;

      // Wire this forms event
      this.NewRawDataAvailable += this.RecordModuleNewRawDataAvailable;

      // Captures input to presentation form.
      this.presenterForm.Capture = true;

      // Show presenter form, that starts presentation.
      this.presenterForm.ShowDialog();

      // Presentation is done, so unplug event handlers.
      this.presenterForm.CounterChanged -= this.ObjPresenterCounterChanged;
      this.presenterForm.SlideChanged -= this.ObjPresenterSlideChanged;
      this.presenterForm.TrialChanged -= this.ObjPresenterTrialChanged;
      this.presenterForm.TrialEventOccured -= this.ObjPresenterTrialEventOccured;
      this.presenterForm.PresentationDone -= this.ObjPresenterPresentationDone;

      // Delete presentation form
      this.presenterForm.Dispose();
      this.presenterForm = null;

      this.NewRawDataAvailable -= this.RecordModuleNewRawDataAvailable;
    }

    /// <summary>
    /// The <see cref="BackgroundWorker.DoWork"/> event handler for
    /// the <see cref="BackgroundWorker"/> <see cref="bgwSaveSplash"/>.
    /// Shows the <see cref="SavingSplash"/> form with a splash screen
    /// wait dialog.
    /// </summary>
    /// <param name="sender">Source of the event</param>
    /// <param name="e">A <see cref="DoWorkEventArgs"/> with the event data.</param>
    private void BgwSaveSplashDoWork(object sender, DoWorkEventArgs e)
    {
      // Create splash object
      var newSplash = new SavingSplash();

      // Get the BackgroundWorker that raised this event.
      var worker = sender as BackgroundWorker;
      newSplash.Worker = worker;

      // Get the description to be shown in the headline.
      var description = e.Argument as string;
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
      var dialog = new ScreenCaptureDialog
        {
          VideoCompressor = this.screenCaptureProperties.VideoCompressor
        };

      // Initialize dialog with defaults
      if (Document.ActiveDocument.ExperimentSettings.ScreenCaptureFramerate < 5)
      {
        Document.ActiveDocument.ExperimentSettings.ScreenCaptureFramerate = 10;
        Document.ActiveDocument.Modified = true;
      }

      dialog.FrameRate = Document.ActiveDocument.ExperimentSettings.ScreenCaptureFramerate;
      if (dialog.ShowDialog() == DialogResult.OK)
      {
        this.screenCaptureProperties.VideoCompressor = dialog.VideoCompressor;
        this.screenCaptureProperties.FrameRate = dialog.FrameRate;
        Document.ActiveDocument.ExperimentSettings.ScreenCaptureFramerate = dialog.FrameRate;
        Document.ActiveDocument.Modified = true;
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
      this.tclEyetracker.TabPages.Add(this.tbpMirametrix);
      this.tclEyetracker.TabPages.Add(this.tbpTobii);
      this.tclEyetracker.TabPages.Add(this.tbpAlea);
      this.tclEyetracker.TabPages.Add(this.tbpSMI);
      this.tclEyetracker.TabPages.Add(this.tbpAsl);
      this.tclEyetracker.TabPages.Add(this.tbpMouseOnly);
      this.tclEyetracker.TabPages.Add(this.tbpGazetrackerIPClient);
      this.tclEyetracker.TabPages.Add(this.tbpGazetrackerDirectClient);

      // Read activated tracker value from the application settings
      var activatedTracker = Settings.Default.ActivatedHardwareTracker;
      var tracker = (HardwareTracker)Enum.Parse(typeof(HardwareTracker), activatedTracker);

      // Show dialog if no tracking device is selected.
      if (tracker == HardwareTracker.None)
      {
        tracker = this.ShowTrackerSelectionDialog();
        Settings.Default.ActivatedHardwareTracker = tracker.ToString();
        Settings.Default.Save();
      }

      this.tclEyetracker.SuspendLayout();

      if (tracker == (tracker | HardwareTracker.MouseOnly))
      {
        // Create mouse only tracker
        var newMouseOnly = new MouseOnlyTracker(
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
        var newAlea = new AleaTracker(
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

      if (tracker == (tracker | HardwareTracker.Tobii))
      {
        // Create tobii tracker
        var newTobii = new TobiiTracker(
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

      if (tracker == (tracker | HardwareTracker.Mirametrix))
      {
        // Create Mirametrix tracker
        var newMirametrix = new MirametrixTracker(
            ref this.labelCalibrationResultMirametrix,
            this.tbpMirametrix,
            this,
            this.spcMirametrixControls,
            this.spcMirametrixTrackStatus.Panel1,
            this.spcMirametrixCalibPlot.Panel1,
            this.btnMirametrixShowOnPresentationScreen,
            this.btnMirametrixAcceptCalibration,
            this.btnMirametrixRecalibrate,
            this.btnMirametrixConnect,
            this.btnMirametrixSubjectName,
            this.btnMirametrixCalibrate,
            this.btnMirametrixRecord,
            this.txbMirametrixSubjectName);

        this.trackerInterfaces.Add(HardwareTracker.Mirametrix, newMirametrix);
      }
      else
      {
        if (this.tclEyetracker.TabPages.Contains(this.tbpMirametrix))
        {
          this.tclEyetracker.TabPages.Remove(this.tbpMirametrix);
        }
      }

      if (tracker == (tracker | HardwareTracker.SMI))
      {
        // Create mouse only tracker
        var newSMI = new SMITracker(
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

      if (tracker == (tracker | HardwareTracker.ASL))
      {
        // Create ASL tracker
        var newAsl = new AslTracker(
              this,
              this.btnAslConnect,
              this.btnAslSubjectName,
              this.btnAslCalibrate,
              this.btnAslRecord,
              this.txbAslSubjectName);
        this.trackerInterfaces.Add(HardwareTracker.ASL, newAsl);
      }
      else
      {
        if (this.tclEyetracker.TabPages.Contains(this.tbpAsl))
        {
          this.tclEyetracker.TabPages.Remove(this.tbpAsl);
        }
      }

      if (tracker == (tracker | HardwareTracker.GazetrackerIPClient))
      {
        // Create ITU GazeTracker
        var newGazetrackerIPClient = new GazetrackerIPClientTracker(
          this,
          this.txbGazetrackerIPStatus,
          this.btnGazetrackerIPLaunch,
          this.btnGazetrackerIPConnect,
          this.btnGazetrackerIPSubject,
          this.btnGazetrackerIPRecord,
          this.txbGazetrackerIPSubject);

        this.trackerInterfaces.Add(HardwareTracker.GazetrackerIPClient, newGazetrackerIPClient);

        // Disable Usercam button by default,
        // because gazetracker often uses the first connected camera device
        // which would otherwise used by the usercam
        this.btnUsercam.Checked = false;
        this.spcPanelUserCam.Panel2Collapsed = true;
      }
      else
      {
        if (this.tclEyetracker.TabPages.Contains(this.tbpGazetrackerIPClient))
        {
          this.tclEyetracker.TabPages.Remove(this.tbpGazetrackerIPClient);
        }
      }

      if (tracker == (tracker | HardwareTracker.GazetrackerDirectClient))
      {
        // Create ITU GazeTracker
        var newGazetrackerDirectClient = new GazetrackerDirectClientTracker(
          this,
          this.eyeVideoControlGazetracker,
          this.btnGazetrackerDirectClientShowOnPresentationScreen,
          this.btnGazetrackerDirectClientConnect,
          this.btnGazetrackerDirectClientAdjust,
          this.btnGazetrackerDirectClientSubject,
          this.btnGazetrackerDirectClientCalibrate,
          this.btnGazetrackerDirectClientRecord,
          this.txbGazetrackerDirectClientSubject);

        this.trackerInterfaces.Add(HardwareTracker.GazetrackerDirectClient, newGazetrackerDirectClient);

        // Disable Usercam button by default,
        // because gazetracker often uses the first connected camera device
        // which would otherwise used by the usercam
        this.btnUsercam.Checked = false;
        this.spcPanelUserCam.Panel2Collapsed = true;
      }
      else
      {
        if (this.tclEyetracker.TabPages.Contains(this.tbpGazetrackerDirectClient))
        {
          this.tclEyetracker.TabPages.Remove(this.tbpGazetrackerDirectClient);
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
      var dialog = new SelectTracker();
      return dialog.ShowDialog() == DialogResult.OK ? dialog.SelectedTracker : HardwareTracker.None;
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

        // Dispose webcam to be recreated in presentation thread
        // with old preview window
        this.webcamPreview.DisposeDxCapture();

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

        var threadParameters = new List<object>
          {
            this.trials, 
            this.generalTrigger, 
            this.btnTrigger.Checked, 
            this.screenCaptureProperties
          };

        if (this.btnUsercam.Checked)
        {
          threadParameters.Add(this.webcamPreview.Properties);
        }

        // Start recording
        this.currentTrialVideoStartTime = 0;
        this.currentTracker.Record();

        var stopwatch = new Stopwatch();
        stopwatch.Start();

        // Wait for first sample to be received
        while (this.GetCurrentTime() < 0)
        {
          Application.DoEvents();
          if (stopwatch.ElapsedMilliseconds > 5000)
          {
            var message = "Could not start recording, because we received no gaze samples from the tracking device during 5 seconds." +
              Environment.NewLine + "Please be sure your tracker is up and running and collecting gaze data, resp. is correct calibrated.";
            InformationDialog.Show("No incoming tracker data", message, false, MessageBoxIcon.Warning);
            stopwatch.Stop();

            return false;
          }
        }

        stopwatch.Stop();

        // Set recording flag
        this.recordingBusy = true;

        // Initialize presentation thread
        this.presentationThread = new Thread(this.PresentationThreadDoWork);
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
    /// It sets the <see cref="currentSlide"/> according to <see cref="currentTrial"/>
    /// </summary>
    private void NewSlideAvailable()
    {
      if (this.currentTrial == null)
      {
        this.currentSlide = DefaultSlide;
      }

      if (this.systemHasSecondaryScreen || this.forcePanelViewerUpdate)
      {
        // Wait for finished screenshot of newly navigated page
        // before updating recorder viewer,
        // otherwise we would have a blank screen
        if (this.currentSlide.VGStimuli.Count > 0)
        {
          if (this.currentSlide.VGStimuli[0] is VGScrollImage)
          {
            var webpageScreenshot = (VGScrollImage)this.currentSlide.VGStimuli[0];
            while (!File.Exists(webpageScreenshot.FullFilename))
            {
              Application.DoEvents();
            }

            // When the file exists it must be ensured, that it is released
            // by its creator...
            int attempts = 0;

            // Loop allow multiple attempts
            while (true)
            {
              try
              {
                FileStream fs = File.OpenRead(webpageScreenshot.FullFilename);

                // If we get here, the File.Open succeeded, so break out of the loop
                fs.Dispose();
                break;
              }
              catch (IOException)
              {
                // IOExcception is thrown if the file is in use by another process.
                // Check the numbere of attempts to ensure no infinite loop
                attempts++;
                if (attempts > 20)
                {
                  // Too many attempts,cannot Open File, break
                  MessageBox.Show("Wait Time did not was enough");
                  break;
                }

                // Sleep before making another attempt
                Thread.Sleep(200);
              }
            }

            webpageScreenshot.CreateInternalImage();
          }
        }

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
        var calculationObject = new FixationCalculation();
        calculationObject.CalcFixations(SampleType.Gaze, subject, trialsTable, null, null);
        calculationObject.CalcFixations(SampleType.Mouse, subject, trialsTable, null, null);

        this.bgwCalcFixations.CancelAsync();

        // Show the success information on the 
        // controllers screen
        var successDlg = new SavingSuccessDialog();
        var controllerBounds = PresentationScreen.GetControllerWorkingArea();
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
      this.trialSequenceCounter = -1;
      this.slideCounter = 0;
      this.recordingStarttime = -5;
      this.lastTimeStamp = -1;

      this.currentTrial = null;
      this.currentSlide = DefaultSlide;

      this.recordTimerWatch.Reset();
      this.tmrRecordClock.Stop();
      this.tmrRecordClock.Dispose();

      this.tmrRecordClock = new System.Windows.Forms.Timer { Interval = 1000 };
      this.tmrRecordClock.Tick += this.TmrRecordClockTick;

      // Stop updating viewer
      this.Picture.StopAnimation();
      this.Picture.ResetPicture();

      // Redraw panel
      this.NewSlideAvailable();

      // this.forcePanelViewerUpdate = true;
      this.forcePanelViewerUpdate = false;
    }

    /// <summary>
    /// This method sets the capture mode according to flash content.
    /// </summary>
    private void InitializeScreenCapture()
    {
      // Only needed if there is a flash movie.
      if (!Document.ActiveDocument.ExperimentSettings.SlideShow.HasScreenCaptureContent())
      {
        this.screenCaptureProperties.CaptureMode = CaptureMode.None;
        return;
      }

      this.screenCaptureProperties.CaptureMode = CaptureMode.Video;
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
        case "tbpMirametrix":
          if (this.trackerInterfaces.ContainsKey(HardwareTracker.Mirametrix))
          {
            this.currentTracker = this.trackerInterfaces[HardwareTracker.Mirametrix];
          }

          break;
        case "tbpTobii":
          if (this.trackerInterfaces.ContainsKey(HardwareTracker.Tobii))
          {
            this.currentTracker = this.trackerInterfaces[HardwareTracker.Tobii];
          }

          break;
        case "tbpAsl":
          if (this.trackerInterfaces.ContainsKey(HardwareTracker.ASL))
          {
            this.currentTracker = this.trackerInterfaces[HardwareTracker.ASL];
          }

          break;
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
        case "tbpGazetrackerDirectClient":
          if (this.trackerInterfaces.ContainsKey(HardwareTracker.GazetrackerDirectClient))
          {
            this.currentTracker = this.trackerInterfaces[HardwareTracker.GazetrackerDirectClient];
          }

          break;
        case "tbpGazetrackerIPClient":
          if (this.trackerInterfaces.ContainsKey(HardwareTracker.GazetrackerIPClient))
          {
            this.currentTracker = this.trackerInterfaces[HardwareTracker.GazetrackerIPClient];
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
      Settings.Default.PresentationScreenMonitor = this.btnPrimary.Checked ? "Primary" : "Secondary";
      Settings.Default.Save();
      this.InitializeScreenCapture();
    }

    /// <summary>
    /// This method is a quick check whether two points are near.
    /// It uses the <see cref="Maxdistancenear"/> constant to check.
    /// </summary>
    /// <param name="firstPt">First point</param>
    /// <param name="secondPt">Second Point</param>
    /// <returns><strong>True</strong> if points are near, otherwise <strong>false</strong>.</returns>
    private bool PointsArNear(PointF firstPt, PointF secondPt)
    {
      if (Math.Abs(firstPt.X - secondPt.X) < Maxdistancenear)
      {
        if (Math.Abs(firstPt.Y - secondPt.Y) < Maxdistancenear)
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
      // Fill its members with current values.
      var newRawData = new RawData
        {
          SubjectName = this.currentTracker.Subject.SubjectName,
          TrialSequence = this.trialSequenceCounter,
          Time = inputEvent.Time + this.currentTrialStarttime,
          EventID = inputEvent.EventID
        };

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
      var newMousePos = new PointF();

      // Get Mouse State.
      var mousePos = MousePosition;
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
