// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PresenterModule.cs" company="Freie Universität Berlin">
//   OGAMA - open gaze and mouse analyzer 
//   Copyright (C) 2014 Dr. Adrian Voßkühler  
//   Licensed under GPL V3
// </copyright>
// <author>Adrian Voßkühler</author>
// <email>adrian@ogama.net</email>
// <summary>
//   A <see cref="Form" /> that is used for stimuli presentation.
//   This class presents the stimuli created by the stimulus creation form
//   and stored in the <see cref="Document" /> <see cref="Properties.ExperimentSettings" /> member.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Ogama.Modules.Recording.Presenter
{
  using System;
  using System.Collections.Generic;
  using System.ComponentModel;
  using System.Diagnostics;
  using System.Drawing;
  using System.Globalization;
  using System.IO;
  using System.Runtime.InteropServices;
  using System.Threading;
  using System.Windows.Forms;

  using mshtml;

  using Ogama.ExceptionHandling;
  using Ogama.Modules.Common.CustomEventArgs;
  using Ogama.Modules.Common.SlideCollections;
  using Ogama.Modules.Common.Tools;
  using Ogama.Modules.Common.TrialEvents;
  using Ogama.Modules.SlideshowDesign.DesignModule.StimuliDialogs;

  using OgamaControls;

  using SHDocVw;

  using VectorGraphics.Controls;
  using VectorGraphics.Controls.Flash;
  using VectorGraphics.Controls.Timer;
  using VectorGraphics.Elements;
  using VectorGraphics.Elements.ElementCollections;
  using VectorGraphics.StopConditions;
  using VectorGraphics.Tools;
  using VectorGraphics.Tools.Trigger;

  using WebBrowser = System.Windows.Forms.WebBrowser;

  /// <summary>
  ///   A <see cref="Form" /> that is used for stimuli presentation.
  ///   This class presents the stimuli created by the stimulus creation form
  ///   and stored in the <see cref="Document" /> <see cref="Properties.ExperimentSettings" /> member.
  /// </summary>
  public partial class PresenterModule : Form
  {
    #region Constants

    /// <summary>
    ///   Specify the minimum interval in milliseconds that should elapse before the next key
    ///   press is accepted and forwarded to the stop condition test.
    /// </summary>
    private const int MINIMUMKEYPRESSINTERVALLMS = 50;

    #endregion

    #region Fields

    /// <summary>
    ///   The main <see cref="BufferedGraphicsContext" /> which is used
    ///   to allocate the <see cref="BufferedGraphics" /> for the
    ///   <see cref="SlidePresentationContainer" />s.
    /// </summary>
    private readonly BufferedGraphicsContext context;

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Variables, Enumerations, Events                                  //
    ///////////////////////////////////////////////////////////////////////////////

    /// <summary>
    ///   The keyboard callback method that is called during desktop recording.
    /// </summary>
    private readonly MessageHook.LowLevelProc keyboardCallback;

    /// <summary>
    ///   The mouse callback method that is called during desktop recording.
    /// </summary>
    private readonly MessageHook.LowLevelProc mouseCallback;

    /// <summary>
    ///   The <see cref="NumberFormatInfo" /> to be used for capturing
    ///   mouse coordinates into the database.
    /// </summary>
    private readonly NumberFormatInfo nfi;

    /// <summary>
    ///   The first of the both available <see cref="SlidePresentationContainer" />
    ///   that are used to prepare and present slides on time.
    /// </summary>
    private readonly SlidePresentationContainer preparedSlideOne;

    /// <summary>
    ///   The second of the both available <see cref="SlidePresentationContainer" />
    ///   that are used to prepare and present slides on time.
    /// </summary>
    private readonly SlidePresentationContainer preparedSlideTwo;

    /// <summary>
    ///   The web browser_ navigating_ lock.
    /// </summary>
    private readonly object webBrowserNavigatingLock = new object();

    /// <summary>
    ///   Indicates the closing of the form.
    /// </summary>
    private bool closing;

    /// <summary>
    ///   Contains the <see cref="BrowserTreeNode" /> for the base webpage, where
    ///   navigation started, to be able to add new pages to the slideshow
    ///   tree at the correct position.
    /// </summary>
    private BrowserTreeNode currentBrowserTreeNode;

    /// <summary>
    ///   Saves the currently pressed key
    /// </summary>
    private Keys currentKey;

    /// <summary>
    ///   Saves the currently pressed mouse button.
    /// </summary>
    private MouseButtons currentMousebutton;

    /// <summary>
    ///   Indicates sending of triggers.
    ///   This value is valid for the slide triggers and the general trigger.
    /// </summary>
    private bool enableTrigger;

    /// <summary>
    ///   An optional trigger that can be send for each slide additionally to the
    ///   triggers that can be defined for each slide separately.
    /// </summary>
    private Trigger generalTrigger;

    /// <summary>
    ///   The <see cref="RecordModule.GetTimeDelegate" /> which
    ///   can be called to retrieve the current sample time
    ///   from the recorder.
    /// </summary>
    private RecordModule.GetTimeDelegate getTimeMethod;

    /// <summary>
    ///   Flag that is true if the cursor should be hidden.
    /// </summary>
    private bool hiddenCursor;

    /// <summary>
    ///   Indicates, whether the current web browser control has been clicked
    ///   with the mouse. Used to determine the first valid navigated
    ///   event after the mouse click, to avoid double trial notifications
    ///   on multiple frame loading (firing a navigated event on each
    ///   frame)
    /// </summary>
    private bool isWebbrowserClicked;

    /// <summary>
    ///   The ID of the keyboard hook that is inserted in the application chain
    ///   during desktop recording.
    /// </summary>
    private IntPtr keyboardHookID = IntPtr.Zero;

    /// <summary>
    ///   Stores the scroll position of the last scroll event during
    ///   web browsing to prevent sending double scroll trial events with same data.
    /// </summary>
    private Point lastScrollEventPosition;

    /// <summary>
    ///   The maximal allowed number of links that can be clicked
    ///   during presentation of the web browser slide.
    /// </summary>
    private int maxBrowseDepth;

    /// <summary>
    ///   The ID of the mouse hook that is inserted in the application chain
    ///   during desktop recording.
    /// </summary>
    private IntPtr mouseHookID = IntPtr.Zero;

    /// <summary>
    ///   Counts the number of links clicked during web browsing.
    ///   Is used to stop browsing, after maximal allowed
    ///   browse depth is reached.
    /// </summary>
    private int numberOfTimesNavigated;

    /// <summary>
    ///   Saves the bounds of the presentation screen for
    ///   quick access.
    /// </summary>
    private Rectangle presentationBounds;

    /// <summary>
    ///   This <see cref="ScreenCaptureProperties" /> contain the
    ///   screen capture properties.
    /// </summary>
    private ScreenCaptureProperties screenCaptureProperties;

    /// <summary>
    ///   This member indicates the currently shown <see cref="SlidePresentationContainer" />
    ///   that can be preparedSlideOne or preparedSlideTwo
    /// </summary>
    private ShownContainer shownContainer;

    /// <summary>
    ///   Contains the currently shown <see cref="SlidePresentationContainer" />
    ///   that is either preparedSlideOne or preparedSlideTwo.
    /// </summary>
    private SlidePresentationContainer shownSlideContainer;

    /// <summary>
    ///   The slide counter.
    /// </summary>
    private int slideCounter;

    /// <summary>
    ///   The trial counter.
    /// </summary>
    private int trialCounter;

    /// <summary>
    ///   Saves the list of trials to display
    /// </summary>
    private TrialCollection trials;

    /// <summary>
    ///   Save the number of trials added to the slideshow during presentation.
    ///   This occurs only during web browsing.
    /// </summary>
    private int trialsAdded;

    /// <summary>
    ///   A <see cref="Webcam" /> that controls the user camera
    ///   if it is used.
    /// </summary>
    private Webcam userCamera;

    /// <summary>
    ///   A <see cref="CaptureDeviceProperties" /> describing
    ///   the webcam properties (capture filter, compressor, etc)
    /// </summary>
    private CaptureDeviceProperties userCameraProperties;

    /// <summary>
    ///   A precise timer.
    /// </summary>
    private Stopwatch watch;

    #endregion

    #region Constructors and Destructors

    /// <summary>
    ///   Initializes a new instance of the PresenterModule class.
    /// </summary>
    public PresenterModule()
    {
      this.InitializeComponent();

      this.nfi = new CultureInfo("en-US", false).NumberFormat;
      this.nfi.NumberGroupSeparator = string.Empty;

      // Retrieves the BufferedGraphicsContext for the 
      // current application domain.
      this.context = BufferedGraphicsManager.Current;

      // Sets the maximum size for the primary graphics buffer
      // of the buffered graphics context for the application
      // domain.  Any allocation requests for a buffer larger 
      // than this will create a temporary buffered graphics 
      // context to host the graphics buffer.
      this.context.MaximumBuffer = new Size(this.Width + 1, this.Height + 1);

      this.preparedSlideOne = new SlidePresentationContainer { ContainerControl = this.panelOne };

      // Allocates a graphics buffer the size of this form
      // using the pixel format of the Graphics created by 
      // the Form.CreateGraphics() method, which returns a 
      // Graphics object that matches the pixel format of the form.
      int width = Document.ActiveDocument.ExperimentSettings.WidthStimulusScreen;
      int height = Document.ActiveDocument.ExperimentSettings.HeightStimulusScreen;

      this.preparedSlideOne.DrawingSurface = this.context.Allocate(
        this.panelOne.CreateGraphics(),
        new Rectangle(0, 0, width, height));
      this.panelOne.DrawingSurface = this.preparedSlideOne.DrawingSurface;

      this.preparedSlideTwo = new SlidePresentationContainer();
      this.preparedSlideTwo.ContainerControl = this.panelTwo;
      this.preparedSlideTwo.DrawingSurface = this.context.Allocate(
        this.panelTwo.CreateGraphics(),
        new Rectangle(0, 0, width, height));
      this.panelTwo.DrawingSurface = this.preparedSlideTwo.DrawingSurface;

      this.keyboardCallback = this.KeyboardHookCallback;
      this.mouseCallback = this.MouseHookCallback;
    }

    #endregion

    #region Delegates

    /// <summary>
    ///   The delegate for the call to SendTrigger from a thread pool thread.
    /// </summary>
    /// <param name="slideContainer">
    ///   The <see cref="SlidePresentationContainer" />
    ///   to send the trigger for.
    /// </param>
    private delegate void SendTriggerDelegate(SlidePresentationContainer slideContainer);

    #endregion

    #region Public Events

    /// <summary>
    ///   Event. Raised when slide or trial counter has changed.
    ///   Used for immediate calls.
    /// </summary>
    public event CounterChangedEventHandler CounterChanged;

    /// <summary>
    ///   Event. Raised when presentation has finished or is aborted.
    ///   Used for asynchronous calls.
    /// </summary>
    public event EventHandler PresentationDone;

    /// <summary>
    ///   Event. Raised when slide has changed.
    ///   Used for asynchronous calls.
    /// </summary>
    public event SlideChangedEventHandler SlideChanged;

    /// <summary>
    ///   Event. Raised when trial has changed.
    ///   Used for asynchronous calls.
    /// </summary>
    public event TrialChangedEventHandler TrialChanged;

    /// <summary>
    ///   Event. Raised when trial event occurred.
    ///   Used for asynchronous calls.
    /// </summary>
    public event TrialEventOccuredEventHandler TrialEventOccured;

    #endregion

    #region Enums

    /// <summary>
    ///   This enumeration describes which <see cref="SlidePresentationContainer" />
    ///   is currently being displayed. Can be None, One or Two.
    /// </summary>
    private enum ShownContainer
    {
      /// <summary>
      ///   No container is shown.
      /// </summary>
      None,

      /// <summary>
      ///   The container preparedSlideOne is shown.
      /// </summary>
      One,

      /// <summary>
      ///   The container preparedSlideTwo is shown.
      /// </summary>
      Two,
    }

    #endregion

    #region Public Properties

    /// <summary>
    ///   Gets the <see cref="TrialCollection" /> of the recorder trials
    ///   which is a clone of the trials collection of this presenter,
    ///   along with the already existing sub trials of the browser trials.
    ///   This collection is being extended during recording,
    ///   if a subject navigates to a never visited page.
    /// </summary>
    public static TrialCollection TrialsIncludingNavigatedWebpages { get; private set; }

    /// <summary>
    ///   Gets the <see cref="DSScreenCapture" /> with the current
    ///   screen capture object or null if none is
    ///   capturing.
    /// </summary>
    public DSScreenCapture CurrentScreenCapture
    {
      get
      {
        switch (this.shownContainer)
        {
          case ShownContainer.One:
            if (this.preparedSlideOne.ScreenCapture != null && this.preparedSlideOne.ScreenCapture.IsRunning)
            {
              return this.preparedSlideOne.ScreenCapture;
            }

            break;
          case ShownContainer.Two:
            if (this.preparedSlideTwo.ScreenCapture != null && this.preparedSlideTwo.ScreenCapture.IsRunning)
            {
              return this.preparedSlideTwo.ScreenCapture;
            }

            break;
        }

        return null;
      }
    }

    /// <summary>
    ///   Sets a value indicating whether to send triggers.
    ///   This value is valid for the slide triggers and the general trigger.
    /// </summary>
    public bool EnableTrigger
    {
      set
      {
        this.enableTrigger = value;
      }
    }

    /// <summary>
    ///   Sets an optional trigger that can be send for each slide additionally to the
    ///   triggers that can be defined for each slide separately.
    /// </summary>
    public Trigger GeneralTrigger
    {
      set
      {
        this.generalTrigger = value;
      }
    }

    /// <summary>
    ///   Sets the  <see cref="RecordModule.GetTimeDelegate" /> which
    ///   can be called to retrieve the current sample time
    ///   from the recorder.
    /// </summary>
    public RecordModule.GetTimeDelegate GetTimeMethod
    {
      set
      {
        this.getTimeMethod = value;
      }
    }

    /// <summary>
    ///   Sets the properties of the screen capture
    ///   device.
    /// </summary>
    public ScreenCaptureProperties ScreenCaptureProperties
    {
      set
      {
        this.screenCaptureProperties = value;
      }
    }

    /// <summary>
    ///   Sets the list of trials that should be presented.
    /// </summary>
    /// <value>A <see cref="TrialCollection" /> with the slides to present.</value>
    public TrialCollection TrialList
    {
      set
      {
        this.trials = value;
      }
    }

    /// <summary>
    ///   Sets the <see cref="Webcam" /> user camera.
    /// </summary>
    public CaptureDeviceProperties UserCameraProperties
    {
      set
      {
        this.userCameraProperties = value;
      }
    }

    #endregion

    #region Public Methods and Operators

    /// <summary>
    /// This method stops the timer, raises the <see cref="PresentationDone"/>
    ///   event and ends the presentation by closing this form.
    /// </summary>
    /// <param name="sendBreakTrigger">
    /// <strong>True</strong>
    ///   if this call to EndPresentation is due to a break of
    ///   the presentation via ESC or from record module.
    /// </param>
    public void EndPresentation(bool sendBreakTrigger)
    {
      // Disable the low level keyboard and mouse hooks
      this.UnhookMessageFilter();

      this.closing = true;

      if (sendBreakTrigger)
      {
        long webcamTime = this.userCamera != null ? this.userCamera.GetCurrentTime() : -1;

        this.OnCounterChanged(new CounterChangedEventArgs(-5, this.slideCounter));

        this.OnTrialChanged(
          new TrialChangedEventArgs(
            this.shownSlideContainer.Trial,
            null,
            new KeyStopCondition(Keys.Escape, false, null),
            this.shownSlideContainer.Slide.Category,
            -1,
            webcamTime));
      }

      if (this.hiddenCursor)
      {
        Cursor.Show();
      }

      this.trials.Clear();

      this.DisposeSlideContainer(this.preparedSlideOne);
      this.DisposeSlideContainer(this.preparedSlideTwo);

      this.preparedSlideOne.DrawingSurface.Dispose();
      this.preparedSlideTwo.DrawingSurface.Dispose();

      if (this.preparedSlideOne.ScreenCapture != null)
      {
        this.preparedSlideOne.ScreenCapture.Dispose();
      }

      if (this.preparedSlideOne.ScreenCapture != null)
      {
        this.preparedSlideTwo.ScreenCapture.Dispose();
      }

      if (this.userCamera != null)
      {
        if (this.userCamera.Properties.CaptureMode != CaptureMode.None)
        {
          // Stop UserCamera
          this.userCamera.StopCapture();
        }

        this.userCamera.Dispose();
      }

      this.OnPresentationDone(EventArgs.Empty);

      try
      {
        this.context.Invalidate();
      }
      catch (Exception ex)
      {
        ExceptionMethods.HandleExceptionSilent(ex);
      }

      if (this.InvokeRequired)
      {
        MethodInvoker closeMethod = this.Close;
        this.Invoke(closeMethod);
        return;
      }

      this.Close();
    }

    #endregion

    #region Methods

    /// <summary>
    /// Overridden <see cref="ProcessCmdKey(ref Message,Keys)"/> method.
    ///   Captures all pressed keys including
    ///   Alt, Ctrl, Space, Esc that are normally not raised as KeyDown in a form.
    /// </summary>
    /// <param name="msg">
    /// This parameter contains the Windows Message, such as WM_KEYDOWN
    /// </param>
    /// <param name="keyData">
    /// The keyData parameter contains the key code of the key that was pressed. If CTRL or ALT was also
    ///   pressed, the keyData parameter contains the ModifierKey information.
    /// </param>
    /// <returns>
    /// True if Key should be processed ?
    /// </returns>
    protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
    {
      const int WM_KEYDOWN = 0x100;
      const int WM_SYSKEYDOWN = 0x104;

      if (((msg.Msg == WM_KEYDOWN) || (msg.Msg == WM_SYSKEYDOWN)) && (!this.closing))
      {
        long eventTime = -1;
        if (this.getTimeMethod != null)
        {
          eventTime = this.getTimeMethod();
        }

        if (this.watch.ElapsedMilliseconds > MINIMUMKEYPRESSINTERVALLMS)
        {
          // Check for markers
          if (keyData == Keys.F12)
          {
            // Store marker event
            var keyEvent = new MediaEvent { Type = EventType.Marker, Task = MediaEventTask.None, Param = string.Empty };
            this.OnTrialEventOccured(new TrialEventOccuredEventArgs(keyEvent, eventTime));
          }
          else
          {
            this.currentKey = keyData;

            if (!this.CheckforSlideChange(false))
            {
              // Store key event only when slide has not changed
              // otherwise the event will be stored during
              // trialchanged event.
              var keyEvent = new InputEvent { Type = EventType.Key, Task = InputEventTask.Down };
              var ksc = new KeyStopCondition(keyData, false, null);
              keyEvent.Param = ksc.ToString();
              this.OnTrialEventOccured(new TrialEventOccuredEventArgs(keyEvent, eventTime));
            }

            this.watch.Reset();
            this.watch.Start();
          }
        }
      }

      return base.ProcessCmdKey(ref msg, keyData);
    }

    /// <summary>
    /// This method parses the given <see cref="VGElement"/>
    ///   for sound files and fills them in the <see cref="OgamaControls.AudioPlayer"/>.
    ///   If they should be played on click, they are stored in the
    ///   <see cref="SlidePresentationContainer.ElementsWithAudioOnClick"/> list.
    /// </summary>
    /// <param name="slideContainer">
    /// The <see cref="SlidePresentationContainer"/>
    ///   this element belongs to.
    /// </param>
    /// <param name="element">
    /// The <see cref="VGElement"/> to search for audio content
    /// </param>
    private static void ParseElementForAudio(SlidePresentationContainer slideContainer, VGElement element)
    {
      if (element.Sound != null && element.Sound.ShouldPlay)
      {
        if (!element.Sound.ShowOnClick)
        {
          slideContainer.AudioPlayer.AddAudioChannel(element.Sound.FullFilename);
        }
        else
        {
          slideContainer.ElementsWithAudioOnClick.Add(element);
        }
      }
    }

    /// <summary>
    /// This method iterates recursively through the <see cref="HtmlWindowCollection"/>
    ///   of a web browser Document to attach the mouse down event for all
    ///   frames, if there are multiple.
    /// </summary>
    /// <param name="htmlWindows">
    /// The first <see cref="HtmlWindowCollection"/>
    ///   to start parsing. You get it from browser.Document.Window.Frames
    /// </param>
    private void AttachEventHandlerForFrames(HtmlWindowCollection htmlWindows)
    {
      foreach (HtmlWindow window in htmlWindows)
      {
        window.Scroll -= this.WebBrowserScroll;
        window.Scroll += this.WebBrowserScroll;
        try
        {
          if (window.Document != null)
          {
            window.Document.MouseDown -= this.WebBrowserMouseDown;
            window.Document.MouseDown += this.WebBrowserMouseDown;
            if (window.Document.Window != null)
            {
              this.AttachEventHandlerForFrames(window.Document.Window.Frames);
            }
          }
        }
        catch (UnauthorizedAccessException ex)
        {
          //ExceptionMethods.HandleExceptionSilent(ex);
        }
      }
    }

    /// <summary>
    /// This method checks the <see cref="Slide.Links"/> collection,
    ///   if the given response is in it, if so it sets the newTrialID
    ///   ouput parameter, otherwise it will be -1.
    /// </summary>
    /// <param name="slideContainer">
    /// The <see cref="SlidePresentationContainer"/> for which to check the links for.
    /// </param>
    /// <param name="response">
    /// A <see cref="StopCondition"/> with the current response.
    /// </param>
    /// <param name="isLink">
    /// Out. <strong>True</strong> if this response is a link, otherwise <strong>false</strong>.
    /// </param>
    /// <param name="newTrialID">
    /// Out. An <see cref="Int32"/> with the new trial ID to link to.
    /// </param>
    private void CheckLinks(
      SlidePresentationContainer slideContainer,
      StopCondition response,
      out bool isLink,
      out int newTrialID)
    {
      isLink = false;
      newTrialID = -1;
      foreach (StopCondition condition in slideContainer.Slide.Links)
      {
        if (condition is MouseStopCondition && response is MouseStopCondition)
        {
          var linkMsc = (MouseStopCondition)condition;
          var responseMsc = (MouseStopCondition)response;
          if (linkMsc.StopMouseButton == responseMsc.StopMouseButton && linkMsc.Target == responseMsc.Target)
          {
            if (linkMsc.TrialID != null)
            {
              newTrialID = linkMsc.TrialID.Value;
            }
            isLink = true;
            break;
          }
        }
        else if (condition is KeyStopCondition && response is KeyStopCondition)
        {
          var linkKsc = (KeyStopCondition)condition;
          var responseKsc = (KeyStopCondition)response;
          if (linkKsc.StopKey == responseKsc.StopKey)
          {
            if (linkKsc.TrialID != null)
            {
              newTrialID = linkKsc.TrialID.Value;
            }
            isLink = true;
            break;
          }
        }
      }
    }

    /// <summary>
    /// This method steps through each of the stop conditions of the current slide.
    ///   If any of them matches the current state, check for
    ///   response correctness and set bChangeStimulus=true;
    /// </summary>
    /// <param name="slideContainer">
    /// The <see cref="SlidePresentationContainer"/> for which the slide should be parsed.
    /// </param>
    /// <param name="changeSlide">
    /// Out. <strong>True</strong> if new slide should be shown.
    /// </param>
    /// <param name="response">
    /// Out. The <see cref="StopCondition"/> that ended the slide.
    /// </param>
    private void CheckResponses(
      SlidePresentationContainer slideContainer,
      out bool changeSlide,
      out StopCondition response)
    {
      changeSlide = false;
      response = null;

      foreach (StopCondition condition in slideContainer.Slide.StopConditions)
      {
        if (condition is MouseStopCondition)
        {
          var msc = (MouseStopCondition)condition;
          if ((msc.CanBeAnyInputOfThisType && this.currentMousebutton != MouseButtons.None)
              || (this.currentMousebutton == msc.StopMouseButton))
          {
            foreach (VGElement shape in slideContainer.Slide.TargetShapes)
            {
              if (shape.Contains(this.PointToClient(MousePosition)))
              {
                response = new MouseStopCondition(msc.StopMouseButton, false, shape.Name, null, MousePosition);
                if (msc.Target != string.Empty && (shape.Name == msc.Target || msc.Target == "Any"))
                {
                  changeSlide = true;
                }

                break;
              }
            }

            if (msc.Target == string.Empty)
            {
              changeSlide = true;
              if (response == null)
              {
                response = new MouseStopCondition(msc.StopMouseButton, false, string.Empty, null, MousePosition);
              }
            }

            if (changeSlide)
            {
              // Check testing condition if specified.
              foreach (StopCondition correctCondition in slideContainer.Slide.CorrectResponses)
              {
                if (msc.Equals(correctCondition))
                {
                  response.IsCorrectResponse = true;
                  break;
                }

                response.IsCorrectResponse = false;
              }

              this.currentMousebutton = MouseButtons.None;
            }
          }
        }
        else if (condition is KeyStopCondition)
        {
          var ksc = (KeyStopCondition)condition;
          if ((ksc.CanBeAnyInputOfThisType && this.currentKey != Keys.None) || (this.currentKey == ksc.StopKey))
          {
            changeSlide = true;
            response = new KeyStopCondition(ksc.StopKey, false, null);

            // Check testing condition if specified.
            if (slideContainer.Slide.CorrectResponses != null)
            {
              // Check testing condition if specified.
              foreach (StopCondition correctCondition in slideContainer.Slide.CorrectResponses)
              {
                if (ksc.Equals(correctCondition))
                {
                  response.IsCorrectResponse = true;
                  break;
                }

                response.IsCorrectResponse = false;
              }
            }

            this.currentKey = Keys.None;
            break;
          }
        }
      }
    }

    /// <summary>
    /// This method scans the <see cref="SlidePresentationContainer.ElementsWithAudioOnClick"/>
    ///   for an element that was clicked and plays it if it was the case.
    /// </summary>
    /// <param name="slideContainer">
    /// The <see cref="SlidePresentationContainer"/> to search for audio files.
    /// </param>
    /// <param name="point">
    /// A <see cref="Point"/> with the click location.
    /// </param>
    /// <param name="eventTime">
    /// A <see cref="Int64"/> with the timestamp of the mouse click.
    /// </param>
    private void CheckforAudioStimulusOnClick(SlidePresentationContainer slideContainer, Point point, long eventTime)
    {
      foreach (VGElement element in slideContainer.ElementsWithAudioOnClick)
      {
        if (element.Contains(point))
        {
          slideContainer.AudioPlayer.AddAudioChannel(element.Sound.FullFilename);
          if (slideContainer.AudioPlayer.PlayState != PlayState.Running)
          {
            slideContainer.AudioPlayer.Play();
          }

          var soundEvent = new MediaEvent();
          soundEvent.Type = EventType.Audio;
          soundEvent.Task = MediaEventTask.Start;
          soundEvent.Param = Path.GetFileName(element.Sound.Filename);
          this.OnTrialEventOccured(new TrialEventOccuredEventArgs(soundEvent, eventTime));

          break;
        }
      }
    }

    /// <summary>
    /// This method checks current response and timers for
    ///   a value that indicates a slide change.
    ///   It also aborts the presentation if the ESC key was pressed.
    /// </summary>
    /// <param name="timeOver">
    /// This flag indicates whether this
    ///   method is called from the slide timer tick event, so the
    ///   slide should definetely be changed.
    /// </param>
    /// <returns>
    /// <strong>True</strong> if slide should be changed or presentation
    ///   is done, otherwise <strong>false</strong>.
    /// </returns>
    private bool CheckforSlideChange(bool timeOver)
    {
      try
      {
        // Abort presentation, if ESC is pressed.
        if (this.currentKey == Keys.Escape)
        {
          this.EndPresentation(true);
          return true;
        }

        bool changeSlide = false;
        StopCondition response = null;
        if (!timeOver)
        {
          this.CheckResponses(this.shownSlideContainer, out changeSlide, out response);
        }
        else
        {
          // if this method is called with timeover = true
          // the time condition is met, so change slide
          // without another test.
          changeSlide = true;
        }

        // go to next slide.
        if (changeSlide)
        {
          bool isLink = false;
          int newTrialID = -1;
          if (response != null)
          {
            this.CheckLinks(this.shownSlideContainer, response, out isLink, out newTrialID);
          }

          bool trialChange = false;
          bool slideChange = false;

          Trial lastTrial = this.shownSlideContainer.Trial;

          if (isLink)
          {
            int newTrialIndex = this.trials.GetIndexOfTrialByID(newTrialID);
            this.slideCounter = 0;
            this.trialCounter = newTrialIndex;
            trialChange = true;

            // Immediately prepare the link slide
            // because it could not be prepared in the 
            // default background preparation thread
            this.PrepareSpecificSlide(this.trialCounter, this.slideCounter);
          }
          else if (this.shownSlideContainer.Trial.IndexOf(this.shownSlideContainer.Slide)
                   == this.shownSlideContainer.Trial.Count - 1)
          {
            // If trial consist of multiple slides then this was the last one,
            // Reset slide counter
            this.slideCounter = 0;

            // Increase trial counter
            this.trialCounter++;
            trialChange = true;
          }
          else
          {
            // Increase slide counter
            this.slideCounter++;
            slideChange = true;
          }

          // If there is a screen capturing in progress
          // finish it in a new thread to let it
          // be saved on disk
          this.StopScreenCapturing(trialChange);

          // Disable the low level keyboard and mouse hooks
          this.UnhookMessageFilter();

          // Finished trial ID 
          int trialID = -5;

          // Switch to new slide/trial
          if (this.trialCounter < this.trials.Count)
          {
            // Change the shown container which has
            // already a prepared slide in it
            this.PresentPreparedSlide();

            // Invoke the preparation of the next slide
            // in the trial list if there is one
            this.PrepareNextSlideAsynchronously(trialChange);

            trialID = this.shownSlideContainer.Trial.ID;
          }
          else
          {
            // this was the last trial, so setup next trial to null
            this.shownSlideContainer.Trial = null;
          }

          // Save webcam time
          long webcamTime = this.userCamera != null ? this.userCamera.GetCurrentTime() : -1;

          // Send trigger in background thread
          AsyncHelper.FireAndForget(new SendTriggerDelegate(this.SendTrigger), this.shownSlideContainer);

          // Send counter update in synchronous call
          //var trial = this.shownSlideContainer.Trial;
          //if (trial != null)
          {
            // Always update counter
            this.OnCounterChanged(new CounterChangedEventArgs(trialID, this.slideCounter));
          }

          // Changes the mouse position and occurence according
          // to slide properties
          this.SetupMouse(this.shownSlideContainer);

          // Send slide or trial change events in asynchronous calls.
          if (trialChange)
          {
            // TO DO : Currently joined slides will not have their own category
            // if displayed in a single trial.
            this.OnTrialChanged(
              new TrialChangedEventArgs(
                lastTrial,
                this.shownSlideContainer.Trial,
                response,
                this.shownSlideContainer.Slide.Category,
                this.trialCounter,
                webcamTime));
          }
          else if (slideChange)
          {
            // There are more slides in this trial, so only
            // raise OnSlideChanged
            this.OnSlideChanged(new SlideChangedEventArgs(lastTrial[this.slideCounter], response, this.slideCounter));
          }

          // Check for last trial
          if (this.trialCounter == this.trials.Count)
          {
            this.EndPresentation(false);
          }
          else
          {
            this.PlaySlideContainer(this.shownSlideContainer);
          }

          return true;
        }

        return false;
      }
      catch (Exception ex)
      {
        ExceptionMethods.ProcessUnhandledException(ex);
        return false;
      }
    }

    /// <summary>
    /// This method releases the resources used in the given
    ///   <see cref="SlidePresentationContainer"/> but not all.
    ///   Some items should not be disposed because they are reused for
    ///   the next slide.
    /// </summary>
    /// <param name="slideContainer">
    /// The <see cref="SlidePresentationContainer"/>
    ///   to be disposed and prepared for next use.
    /// </param>
    private void DisposeSlideContainer(SlidePresentationContainer slideContainer)
    {
      // Explicitely dispose flash objects 
      // otherwise we will get an exception from 
      // the MDA reportAvOnComRelease
      if (slideContainer.ContainerControl.Controls.Count > 0)
      {
        foreach (Control ctrl in slideContainer.ContainerControl.Controls)
        {
          if (ctrl is AxFlashControl)
          {
            if (ctrl.InvokeRequired)
            {
              MethodInvoker ctrlDisposeDelegate = ctrl.Dispose;
              ctrl.Invoke(ctrlDisposeDelegate);
            }
            else
            {
              ctrl.Dispose();
            }
          }
          else if (ctrl is WebBrowser)
          {
            if (ctrl.InvokeRequired)
            {
              MethodInvoker ctrlDisposeDelegate = ctrl.Dispose;
              ctrl.Invoke(ctrlDisposeDelegate);
            }
          }
        }
      }

      foreach (VGElement element in slideContainer.Slide.ActiveXStimuli)
      {
        if (element is VGFlash)
        {
          var flash = element as VGFlash;
          flash.SendMessagesToParent(false);
        }
        else if (element is VGBrowser)
        {
          var browser = element as VGBrowser;
          browser.SendMessagesToParent(false);
        }
      }

      if (this.InvokeRequired)
      {
        MethodInvoker controlsClearMethod = slideContainer.ContainerControl.Controls.Clear;
        this.Invoke(controlsClearMethod);
      }
      else
      {
        slideContainer.ContainerControl.Controls.Clear();
      }

      slideContainer.Slide.Dispose();

      // Stop audio playback and release player
      slideContainer.AudioPlayer.CloseAudioFile();
      slideContainer.ElementsWithAudioOnClick.Clear();

      // Stop current running timer.
      if (slideContainer.Timer.IsRunning)
      {
        slideContainer.Timer.Stop();
      }
    }

    /// <summary>
    /// This method invokes the method to draw the given slide onto
    ///   the given graphics asynchronously.
    ///   That allows calling from a separate thread.
    /// </summary>
    /// <param name="slide">
    /// A <see cref="Slide"/> that should be drawn.
    /// </param>
    /// <param name="g">
    /// A <see cref="Graphics"/> on which the slide should be drawn.
    /// </param>
    private void DrawSlideAsyncMethod(Slide slide, Graphics g)
    {
      this.Invoke(new Slide.AsyncDrawSlideMethodCaller(Slide.DrawSlideAsync), slide, g);
    }

    /// <summary>
    /// This method asynchronously calls the drawing method of the slide of
    ///   the given container to the given slidecontainers drawing surface.
    /// </summary>
    /// <param name="slideToDraw">
    /// The <see cref="SlidePresentationContainer"/>
    ///   whichs slide should be drawn to its buffer.
    /// </param>
    private void DrawToBuffer(SlidePresentationContainer slideToDraw)
    {
      if (slideToDraw.Slide.PresentationSize == Size.Empty)
      {
        slideToDraw.Slide.PresentationSize = Document.ActiveDocument.PresentationSize;
      }

      // Draw slides contents.
      this.DrawSlideAsyncMethod(slideToDraw.Slide, slideToDraw.DrawingSurface.Graphics);
    }

    /// <summary>
    ///   This method creates the initialization trial.
    /// </summary>
    private void InitializeFirstTrial()
    {
      var coll = new StopConditionCollection();
      var stc = new TimeStopCondition(5000);
      coll.Add(stc);

      this.preparedSlideOne.Slide = new Slide(
        "OgamaDummyStartTrial6gsj2",
        Color.Gray,
        Images.CreateRecordInstructionImage(
          Document.ActiveDocument.ExperimentSettings.WidthStimulusScreen,
          Document.ActiveDocument.ExperimentSettings.HeightStimulusScreen),
        coll,
        new StopConditionCollection(),
        string.Empty,
        Document.ActiveDocument.PresentationSize);
      var wait = new VGText(
        ShapeDrawAction.None,
        "Initializing ...",
        new Font("Verdana", 40f),
        Color.WhiteSmoke,
        HorizontalAlignment.Center,
        1,
        6,
        Pens.Red,
        Brushes.Red,
        SystemFonts.MenuFont,
        Color.Black,
        new RectangleF(100, 100, 400, 200),
        VGStyleGroup.None,
        "Text",
        string.Empty,
        null);
      this.preparedSlideOne.Slide.VGStimuli.Add(wait);

      // Reset the cursor position to initial location if applicable
      var newPoint = new Point(
        this.presentationBounds.Left + this.presentationBounds.Width / 2,
        this.presentationBounds.Top + this.presentationBounds.Height / 2);
      Cursor.Position = newPoint;

      Cursor.Hide();
      this.hiddenCursor = true;

      // Prepare preparation slide 
      this.preparedSlideOne.Trial = new Trial("DummyTrial", -1);
      this.preparedSlideOne.Trial.Add(this.preparedSlideOne.Slide);
      this.preparedSlideOne.Timer.Period = 2000;
      this.preparedSlideOne.Timer.Mode = TimerMode.OneShot;
      this.preparedSlideOne.Timer.SynchronizingObject = this;
      this.preparedSlideOne.Timer.Tick += this.TimerTick;

      this.DrawToBuffer(this.preparedSlideOne);
      this.PresentPreparedSlide();

      // Prepare first slide of trial list
      this.preparedSlideTwo.Trial = this.trials[0];
      this.preparedSlideTwo.Slide = this.trials[0][0];
      this.preparedSlideTwo.Timer.Period = 200;
      this.preparedSlideTwo.Timer.Mode = TimerMode.OneShot;
      this.preparedSlideTwo.Timer.SynchronizingObject = this;
      this.preparedSlideTwo.Timer.Tick += this.TimerTick;
      this.InitializeNextSlide(this.preparedSlideTwo);
      this.DrawToBuffer(this.preparedSlideTwo);
      this.PrepareScreenCapture(-1);
    }

    /// <summary>
    /// This method initializes the next slide in the given
    ///   <see cref="SlidePresentationContainer"/>.
    ///   That is setting a time stop condition timer,
    ///   setup audio replay, and load flash objects.
    ///   Because this can last a significant amount of time
    ///   it should be done in a background thread.
    /// </summary>
    /// <param name="slideContainer">
    /// The <see cref="SlidePresentationContainer"/>
    ///   that should be initialized.
    /// </param>
    private void InitializeNextSlide(SlidePresentationContainer slideContainer)
    {
      try
      {
        // Reset Timer to 1ms period indicating that it should not be used during
        // call to PlaySlideContainer
        slideContainer.Timer.Period = 1;

        // Reset the timer according to stop condition if applicable.
        foreach (StopCondition condition in slideContainer.Slide.StopConditions)
        {
          if (condition is TimeStopCondition)
          {
            var timeCondition = (TimeStopCondition)condition;
            slideContainer.Timer.Period = timeCondition.Duration;
            break;
          }
        }

        // Search for audio files
        this.ParseElementsForAudio(slideContainer);

        // Load background sound
        if (slideContainer.Slide.BackgroundSound != null)
        {
          if (slideContainer.Slide.BackgroundSound.ShouldPlay)
          {
            slideContainer.AudioPlayer.AddAudioChannel(slideContainer.Slide.BackgroundSound.FullFilename);
          }
        }

        // Check for flash stimuli and load them into the
        // flashObject activeX control.
        foreach (VGElement element in slideContainer.Slide.ActiveXStimuli)
        {
          if (element is VGFlash)
          {
            var flash = element as VGFlash;
            flash.InitializeOnControl(slideContainer.ContainerControl, true, new System.Drawing.Drawing2D.Matrix());
          }
          else if (element is VGBrowser)
          {
            var browser = element as VGBrowser;
            browser.InitializeOnControl(slideContainer.ContainerControl, true);
            browser.WebBrowser.Navigate(browser.BrowserURL);
            this.numberOfTimesNavigated = 0;
            this.maxBrowseDepth = browser.BrowseDepth;
            this.currentBrowserTreeNode =
              (BrowserTreeNode)Document.ActiveDocument.ExperimentSettings.SlideShow.GetNodeByID(slideContainer.Trial.ID);
          }
        }
      }
      catch (Exception ex)
      {
        ExceptionMethods.HandleException(ex);
      }
    }

    /// <summary>
    /// This method initializes the user canera on first start of the
    ///   presentation.
    /// </summary>
    /// <param name="value">
    /// A <see cref="CaptureDeviceProperties"/>
    ///   with the device and compressor to be used for the webcam.
    /// </param>
    private void InitializeUserCamera(CaptureDeviceProperties value)
    {
      if (value == null)
      {
        // No need to initalize
        return;
      }

      this.userCamera = new Webcam();
      this.userCamera.Properties = value;
    }

    /// <summary>
    /// This method is an implementation of the hook callback for low level
    ///   keyboard message hooks.
    /// </summary>
    /// <param name="ncode">
    /// A code the hook procedure uses to determine how
    ///   to process the message. If nCode is less than zero, the hook procedure
    ///   must pass the message to the CallNextHookEx function without further processing
    ///   and should return the value returned by CallNextHookEx
    /// </param>
    /// <param name="wparam">
    /// The identifier of the mouse message. This parameter can be one
    ///   of the following messages: WM_LBUTTONDOWN, WM_LBUTTONUP, WM_MOUSEMOVE,
    ///   WM_MOUSEWHEEL, WM_MOUSEHWHEEL, WM_RBUTTONDOWN, or WM_RBUTTONUP.
    /// </param>
    /// <param name="lparam">
    /// A pointer to an MSLLHOOKSTRUCT structure.
    /// </param>
    /// <returns>
    /// If nCode is less than zero, the hook procedure must return the value returned by CallNextHookEx.
    ///   If nCode is greater than or equal to zero, and the hook procedure did
    ///   not process the message, it is highly recommended that you call
    ///   CallNextHookEx and return the value it returns; otherwise,
    ///   other applications that have installed WH_MOUSE_LL hooks will not
    ///   receive hook notifications and may behave incorrectly as a result.
    ///   If the hook procedure processed the message, it may return a nonzero #
    ///   value to prevent the system from passing the message to the rest of the
    ///   hook chain or the target window procedure.
    /// </returns>
    private IntPtr KeyboardHookCallback(int ncode, IntPtr wparam, IntPtr lparam)
    {
      var msg = new Message { WParam = wparam, LParam = lparam, Msg = (int)wparam };
      int keyCode = Marshal.ReadInt32(lparam);
      this.ProcessCmdKey(ref msg, (Keys)keyCode);

      return MessageHook.CallNextHookEx(this.keyboardHookID, ncode, wparam, lparam);
    }

    ///////////////////////////////////////////////////////////////////////////////
    // Eventhandler                                                              //
    ///////////////////////////////////////////////////////////////////////////////

    ///////////////////////////////////////////////////////////////////////////////
    // Eventhandler for UI, Menu, Buttons, Toolbars etc.                         //
    ///////////////////////////////////////////////////////////////////////////////

    /// <summary>
    /// An application-defined ocallback function used with the
    ///   SetWindowsHookEx function. The system call this function every time a
    ///   new mouse input event is about to be posted into a thread input queue.
    /// </summary>
    /// <param name="ncode">
    /// A code the hook procedure uses to determine how
    ///   to process the message. If nCode is less than zero, the hook procedure
    ///   must pass the message to the CallNextHookEx function without further processing
    ///   and should return the value returned by CallNextHookEx
    /// </param>
    /// <param name="wparam">
    /// The identifier of the mouse message. This parameter can be one
    ///   of the following messages: WM_LBUTTONDOWN, WM_LBUTTONUP, WM_MOUSEMOVE,
    ///   WM_MOUSEWHEEL, WM_MOUSEHWHEEL, WM_RBUTTONDOWN, or WM_RBUTTONUP.
    /// </param>
    /// <param name="lparam">
    /// A pointer to an MSLLHOOKSTRUCT structure.
    /// </param>
    /// <returns>
    /// If nCode is less than zero, the hook procedure must return the value returned by CallNextHookEx.
    ///   If nCode is greater than or equal to zero, and the hook procedure did
    ///   not process the message, it is highly recommended that you call
    ///   CallNextHookEx and return the value it returns; otherwise,
    ///   other applications that have installed WH_MOUSE_LL hooks will not
    ///   receive hook notifications and may behave incorrectly as a result.
    ///   If the hook procedure processed the message, it may return a nonzero #
    ///   value to prevent the system from passing the message to the rest of the
    ///   hook chain or the target window procedure.
    /// </returns>
    private IntPtr MouseHookCallback(int ncode, IntPtr wparam, IntPtr lparam)
    {
      if (ncode >= 0)
      {
        bool skip = false;
        bool isButtonDown = false;
        var button = MouseButtons.None;
        switch ((MessageHook.MouseMessages)wparam)
        {
          case MessageHook.MouseMessages.WM_LBUTTONDOWN:
            button = MouseButtons.Left;
            isButtonDown = true;
            break;
          case MessageHook.MouseMessages.WM_LBUTTONUP:
            button = MouseButtons.Left;
            break;
          case MessageHook.MouseMessages.WM_MOUSEMOVE:
          case MessageHook.MouseMessages.WM_MOUSEWHEEL:
            skip = true;
            break;
          case MessageHook.MouseMessages.WM_RBUTTONDOWN:
            button = MouseButtons.Right;
            isButtonDown = true;
            break;
          case MessageHook.MouseMessages.WM_RBUTTONUP:
            button = MouseButtons.Right;
            break;
        }

        if (!skip)
        {
          var hookStruct =
            (MessageHook.MSLLHOOKSTRUCT)Marshal.PtrToStructure(lparam, typeof(MessageHook.MSLLHOOKSTRUCT));
          var mea = new MouseEventArgs(button, 1, hookStruct.Point.X, hookStruct.Point.Y, 0);
          if (isButtonDown)
          {
            this.FrmPresenterMouseDown(this, mea);
          }
          else
          {
            this.FrmPresenterMouseUp(this, mea);
          }
        }
      }

      return MessageHook.CallNextHookEx(this.mouseHookID, ncode, wparam, lparam);
    }

    /// <summary>
    /// This method is the DoWork event handler for the thread
    ///   that navigates the webbrowser mirror form to the navigated page of
    ///   the presenter thread.
    /// </summary>
    /// <param name="data">
    /// An <see cref="Object"/> with the
    ///   thread parameters, which are in this case of type
    ///   <see cref="WebBrowserNavigatingEventArgs"/>.
    /// </param>
    private void NavigateMirror(object data)
    {
      var parameters = (object[])data;

      //WebsiteScreenshot.Instance.Navigate((WebBrowserNavigatingEventArgs)data);
      WebsiteScreenshot.DoScreenshot((WebBrowserNavigatingEventArgs)parameters[0], (string)parameters[1]);
    }

    /// <summary>
    /// This method raises the <see cref="CounterChanged"/>
    ///   event by invoking the delegates.
    ///   It should be called when the current slide ot trial index has changed.
    /// </summary>
    /// <param name="e">
    /// A <see cref="CounterChangedEventArgs"/> with the event data.
    /// </param>
    /// .
    private void OnCounterChanged(CounterChangedEventArgs e)
    {
      if (this.CounterChanged != null)
      {
        // This is the only place we should not use asynchrous calls.
        this.CounterChanged(this, e);
      }
    }

    /// <summary>
    /// This method raises the <see cref="PresentationDone"/>
    ///   event by invoking the delegates.
    ///   It should be called when the presentation has finished.
    /// </summary>
    /// <param name="e">
    /// An empty <see cref="EventArgs"/>
    /// </param>
    /// .
    /// <remarks>
    /// We just fire the <see cref="PresentationDone"/> event and don´t wait
    ///   for finish, because otherwise the last slide will stay visible until the whole
    ///   writing to database is done.
    /// </remarks>
    private void OnPresentationDone(EventArgs e)
    {
      if (this.PresentationDone != null)
      {
        AsyncHelper.FireAndForget(this.PresentationDone, this, e);
      }
    }

    /// <summary>
    /// This method raises the <see cref="SlideChanged"/>
    ///   event by invoking the delegates.
    ///   It should be called when the current slide has changed.
    /// </summary>
    /// <param name="e">
    /// A <see cref="SlideChangedEventArgs"/> with the event data.
    /// </param>
    /// .
    private void OnSlideChanged(SlideChangedEventArgs e)
    {
      if (this.SlideChanged != null)
      {
        AsyncHelper.FireAndForget(this.SlideChanged, this, e);
      }
    }

    /// <summary>
    /// This method raises the <see cref="TrialChanged"/>
    ///   event by invoking the delegates.
    ///   It should be called when the current trial has changed.
    /// </summary>
    /// <param name="e">
    /// A <see cref="TrialChangedEventArgs"/> with the event data.
    /// </param>
    /// .
    private void OnTrialChanged(TrialChangedEventArgs e)
    {
      if (this.TrialChanged != null)
      {
        AsyncHelper.FireAndForget(this.TrialChanged, this, e);
      }
    }

    /// <summary>
    /// This method raises the <see cref="TrialEventOccured"/>
    ///   event by invoking the delegates.
    ///   It should be called whenever an event occured in a trial.
    /// </summary>
    /// <param name="e">
    /// A <see cref="TrialEventOccuredEventArgs"/> with the event data.
    /// </param>
    /// .
    private void OnTrialEventOccured(TrialEventOccuredEventArgs e)
    {
      if (this.TrialEventOccured != null)
      {
        this.TrialEventOccured(this, e);

        // AsyncHelper.FireAndForget(this.TrialEventOccured, this, e);
      }
    }

    /// <summary>
    /// This method parses the <see cref="Slide.VGStimuli"/>
    ///   for elements with sound files and fills them in the <see cref="OgamaControls.AudioPlayer"/>.
    ///   If they should be played on click, they are stored in the
    ///   <see cref="SlidePresentationContainer.ElementsWithAudioOnClick"/> list.
    /// </summary>
    /// <param name="slideContainer">
    /// The <see cref="SlidePresentationContainer"/>
    ///   to pe parsed for audio content that should be prepared for replay.
    /// </param>
    private void ParseElementsForAudio(SlidePresentationContainer slideContainer)
    {
      slideContainer.ElementsWithAudioOnClick.Clear();
      foreach (VGElement element in slideContainer.Slide.VGStimuli)
      {
        ParseElementForAudio(slideContainer, element);
      }

      foreach (VGElement element in slideContainer.Slide.ActiveXStimuli)
      {
        ParseElementForAudio(slideContainer, element);
      }
    }

    /// <summary>
    /// This method is called whenever the slide has been swapped to the
    ///   foreground and its replay of audio streams and
    ///   optional screen capturing should be started.
    /// </summary>
    /// <param name="slideContainer">
    /// The <see cref="SlidePresentationContainer"/>
    ///   that should be initiated for replay.
    /// </param>
    private void PlaySlideContainer(SlidePresentationContainer slideContainer)
    {
      if (slideContainer.AudioPlayer.PlayState != PlayState.Running)
      {
        slideContainer.AudioPlayer.Play();
      }

      int countFlash = 0;

      if (this.shownSlideContainer.Slide.HasActiveXContent)
      {
        foreach (VGElement element in slideContainer.Slide.ActiveXStimuli)
        {
          if (element is VGFlash)
          {
            var flash = element as VGFlash;
            flash.SendMessagesToParent(true);
            flash.Play();
            countFlash++;
          }
          else if (element is VGBrowser)
          {
            var browser = element as VGBrowser;

            if (browser.WebBrowser.IsDisposed)
            {
              throw new ObjectDisposedException("Browser of new slide is already disposed...");
            }

            browser.SendMessagesToParent(true);
            if (browser.WebBrowser.InvokeRequired)
            {
              Application.DoEvents();
            }

            while (browser.WebBrowser.ReadyState != WebBrowserReadyState.Complete)
            {
              Application.DoEvents();
              if (this.closing)
              {
                return;
              }
            }

            // Set input focus to the webbrowser, otherwise the 
            // mouse will not act like a webbrowser mouse showing a
            // hand on a link etc.
            browser.WebBrowser.Focus();
            browser.WebBrowser.NewWindow += this.WebBrowser_NewWindow;
            browser.WebBrowser.DocumentCompleted += this.WebBrowserDocumentCompleted;
            browser.WebBrowser.Navigating += this.WebBrowserNavigating;
            if (browser.WebBrowser.Document != null)
            {
              if (browser.WebBrowser.Document.Window != null)
              {
                browser.WebBrowser.Document.Window.Scroll += this.WebBrowserScroll;
              }
              browser.WebBrowser.Document.MouseDown += this.WebBrowserMouseDown;
            }
          }
        }
      }

      // Just do screen capturing on flash content or desktop recording
      if (countFlash > 0 || this.shownSlideContainer.Slide.IsDesktopSlide)
      {
        switch (this.shownContainer)
        {
          case ShownContainer.One:
            if (this.preparedSlideOne.ScreenCapture != null)
            {
              this.preparedSlideOne.ScreenCapture.Start();
            }

            break;
          case ShownContainer.Two:
            if (this.preparedSlideTwo.ScreenCapture != null)
            {
              this.preparedSlideTwo.ScreenCapture.Start();
            }

            break;
        }
      }
    }

    /// <summary>
    /// This method is the DoWork event handler for the thread
    ///   that preparates the next slide to be displayed.
    /// </summary>
    /// <param name="data">
    /// An <see cref="Object"/> with the
    ///   thread parameters.
    /// </param>
    private void PreparationThreadDoWork(object data)
    {
      var threadParams = (List<object>)data;

      var localTrialCounter = (int)threadParams[0];
      var localSlideCounter = (int)threadParams[1];

      Trial shownTrial = this.trials[localTrialCounter];
      Slide shownSlide = shownTrial[localSlideCounter];

      if (shownTrial.IndexOf(shownSlide) == shownTrial.Count - 1)
      {
        // If trial consist of multiple slides then this was the last one,
        // Reset slide counter
        localSlideCounter = 0;

        // Increase trial counter
        localTrialCounter++;
      }
      else
      {
        // Increase slide counter
        localSlideCounter++;
      }

      if (localTrialCounter < this.trials.Count)
      {
        this.PrepareSpecificSlide(localTrialCounter, localSlideCounter);
      }
    }

    /// <summary>
    /// This method invokes the preparation of the next slides.
    /// </summary>
    /// <param name="trialChange">
    /// True, if the trial has changed,
    ///   false if it should go on with the next slide in the trial.
    /// </param>
    private void PrepareNextSlideAsynchronously(bool trialChange)
    {
      // Initialize preparation thread
      var threadParameters = new List<object>();
      threadParameters.Add(this.trialCounter);
      threadParameters.Add(this.slideCounter);

      var preparationThread = new Thread(this.PreparationThreadDoWork);
      preparationThread.SetApartmentState(ApartmentState.STA);
      preparationThread.Start(threadParameters);

      if (trialChange)
      {
        // AsyncHelper.FireAndForget(new PrepareScreenCaptureDelegate(this.PrepareScreenCapture), this.trialCounter);
        this.PrepareScreenCapture(this.trialCounter);
      }
    }

    /// <summary>
    /// This method scans the next trial for flash content to initialize
    ///   the corresponding screen capture device.
    /// </summary>
    /// <param name="shownTrialCounter">
    /// The zero-based counter of the
    ///   current shown trial.
    /// </param>
    private void PrepareScreenCapture(int shownTrialCounter)
    {
      int nextTrialCounter = shownTrialCounter;
      nextTrialCounter++;

      if (nextTrialCounter >= this.trials.Count)
      {
        return;
      }

      Trial nextTrial = this.trials[nextTrialCounter];

      if (nextTrial.HasActiveXContent || nextTrial[0].IsDesktopSlide)
      {
        string filename = Document.ActiveDocument.SelectionState.SubjectName + "-" + nextTrialCounter + ".avi";
        filename = Path.Combine(Document.ActiveDocument.ExperimentSettings.ThumbsPath, filename);
        switch (this.shownContainer)
        {
          case ShownContainer.One:

            // Prepare screen capturing
            if (this.preparedSlideTwo.ScreenCapture != null)
            {
              this.preparedSlideTwo.ScreenCapture.Filename = filename;
            }

            break;
          case ShownContainer.Two:

            // Prepare screen capturing
            if (this.preparedSlideOne.ScreenCapture != null)
            {
              this.preparedSlideOne.ScreenCapture.Filename = filename;
            }

            break;
        }
      }
    }

    /// <summary>
    /// This method updates the slide container that is
    ///   the next to be displayed with a new slide.
    /// </summary>
    /// <param name="prepareTrialCounter">
    /// The zero-based index of the trial
    ///   in the trials list.
    /// </param>
    /// <param name="prepareSlideCounter">
    /// The zero-based index of the slide
    ///   in the trials slide list.
    /// </param>
    private void PrepareSpecificSlide(int prepareTrialCounter, int prepareSlideCounter)
    {
      switch (this.shownContainer)
      {
        case ShownContainer.One:
          this.DisposeSlideContainer(this.preparedSlideTwo);
          this.preparedSlideTwo.Trial = this.trials[prepareTrialCounter];
          this.preparedSlideTwo.Slide = this.preparedSlideTwo.Trial[prepareSlideCounter];
          this.InitializeNextSlide(this.preparedSlideTwo);
          this.DrawToBuffer(this.preparedSlideTwo);
          break;
        case ShownContainer.Two:
          this.DisposeSlideContainer(this.preparedSlideOne);
          this.preparedSlideOne.Trial = this.trials[prepareTrialCounter];
          this.preparedSlideOne.Slide = this.preparedSlideOne.Trial[prepareSlideCounter];
          this.InitializeNextSlide(this.preparedSlideOne);
          this.DrawToBuffer(this.preparedSlideOne);
          break;
      }
    }

    /// <summary>
    ///   This method swaps the slide buffers by swapping the
    ///   position of the controls panelOne and panelTwo and
    ///   updating the shownSlide.
    /// </summary>
    private void PresentPreparedSlide()
    {
      // Detach scroll event listeners
      if (this.shownSlideContainer != null)
      {
        foreach (VGElement element in this.shownSlideContainer.Slide.ActiveXStimuli)
        {
          if (element is VGBrowser)
          {
            var browser = element as VGBrowser;
            try
            {
              browser.WebBrowser.NewWindow -= this.WebBrowser_NewWindow;
              if (browser.WebBrowser.Document != null)
              {
                if (browser.WebBrowser.Document.Window != null)
                {
                  browser.WebBrowser.Document.Window.Scroll -= this.WebBrowserScroll;
                }
                browser.WebBrowser.DocumentCompleted -= this.WebBrowserDocumentCompleted;
                browser.WebBrowser.Navigating -= this.WebBrowserNavigating;
                browser.WebBrowser.Document.MouseDown -= this.WebBrowserMouseDown;
              }
            }
            catch (NullReferenceException ex)
            {
              ExceptionMethods.HandleExceptionSilent(ex);
            }
          }
        }
      }

      switch (this.shownContainer)
      {
        case ShownContainer.One:
          this.Controls.SetChildIndex(this.panelTwo, 0);
          this.shownSlideContainer = this.preparedSlideTwo;
          this.shownContainer = ShownContainer.Two;
          break;
        case ShownContainer.None:
        case ShownContainer.Two:
          this.Controls.SetChildIndex(this.panelOne, 0);
          this.shownSlideContainer = this.preparedSlideOne;
          this.shownContainer = ShownContainer.One;
          break;
      }

      // Care for desktop recording slides
      var slidePresentationContainer = this.shownSlideContainer;
      if (slidePresentationContainer != null && slidePresentationContainer.Slide.IsDesktopSlide)
      {
        this.WindowState = FormWindowState.Minimized;
        this.keyboardHookID = MessageHook.SetKeyboardHook(this.keyboardCallback);
        this.mouseHookID = MessageHook.SetMouseHook(this.mouseCallback);
      }
      else
      {
        this.WindowState = FormWindowState.Maximized;
      }

      // Reset response fields
      this.currentMousebutton = MouseButtons.None;
      this.currentKey = Keys.None;

      // Need a refresh here because otherwise the video
      // capture will be started before the screen has been redrawn
      // and it would start with a frame of the old
      // slide.
      this.Refresh();

      if (this.shownSlideContainer.Timer.Period > 1)
      {
        this.shownSlideContainer.Timer.Start();
      }
    }

    /// <summary>
    /// The <see cref="Form.Load"/> event handler.
    ///   Initializes the form on the presentation screen,
    ///   then initializes flash stimulus handling,
    ///   starts the timing control and then
    ///   loads first stimulus.
    /// </summary>
    /// <param name="sender">
    /// Source of the event.
    /// </param>
    /// <param name="e">
    /// An empty <see cref="EventArgs"/>
    /// </param>
    private void PresenterModuleLoad(object sender, EventArgs e)
    {
      try
      {
        if (this.trials == null || this.trials.Count == 0)
        {
          this.Close();
          return;
        }

        // Add already navigated browser slide subnode to the trial collection
        TrialsIncludingNavigatedWebpages = (TrialCollection)this.trials.Clone();
        foreach (Trial trial in this.trials)
        {
          if (trial[0].IsBrowserSlide)
          {
            SlideshowTreeNode browserNode = Document.ActiveDocument.ExperimentSettings.SlideShow.GetNodeByID(trial.ID);
            foreach (object visitedPageNode in browserNode.Nodes)
            {
              // Also add all visited page trials for this browser slide
              // this can be only be one step deep
              var subCollectionNode = visitedPageNode as SlideshowTreeNode;
              if (subCollectionNode != null)
              {
                Slide slide = subCollectionNode.Slide;
                if (slide == null || slide.IsDisabled)
                {
                  continue;
                }
                var subtrial = new Trial(subCollectionNode.Text, Slideshow.GetIdOfNode(subCollectionNode));
                subtrial.Add(slide);
                TrialsIncludingNavigatedWebpages.Add(subtrial);
              }
            }
          }
        }

        // Show presentation on secondary screen if possible,
        // otherwise maximise on primary screen
        PresentationScreen.PutFormOnPresentationScreen(this, true);

        // Some slides may have a hidden cursor.
        this.hiddenCursor = false;

        // Initializes the timer
        this.watch = new Stopwatch();
        this.watch.Start();

        // Start UserCamera
        if (this.userCameraProperties != null)
        {
          this.InitializeUserCamera(this.userCameraProperties);
          if (this.userCamera != null && this.userCamera.Properties.CaptureMode != CaptureMode.None)
          {
            AsyncHelper.FireAsync(new MethodInvoker(this.userCamera.RunGraph));
          }
        }

        if (this.screenCaptureProperties != null && this.screenCaptureProperties.CaptureMode != CaptureMode.None)
        {
          this.preparedSlideOne.InitializeScreenCapture(this.screenCaptureProperties);
          this.preparedSlideTwo.InitializeScreenCapture(this.screenCaptureProperties);
        }

        this.presentationBounds = PresentationScreen.GetPresentationWorkingArea();

        // Loads first slide, or closes form if there are no slides to display.
        this.trialCounter = -1;
        this.InitializeFirstTrial();
      }
      catch (Exception ex)
      {
        ExceptionMethods.HandleException(ex);

        this.Close();
      }
    }

    /// <summary>
    /// This method sends trigger signals to the ports if
    ///   triggering is enabled in the module.
    ///   First it sends the general trigger, and the the slide trigger, if
    ///   it is enabled.
    /// </summary>
    /// <param name="slideContainer">
    /// The <see cref="SlidePresentationContainer"/> that should send the slide trigger.
    /// </param>
    /// <remarks>
    /// This method is called in a separate thread, because
    ///   the signaling time could be long lasting.
    /// </remarks>
    private void SendTrigger(SlidePresentationContainer slideContainer)
    {
      try
      {
        if (this.enableTrigger)
        {
          // Send general trigger if applicable
          if (this.generalTrigger.Signaling != TriggerSignaling.None)
          {
            this.generalTrigger.Send();
          }

          // Send Slide trigger if applicable
          if (this.generalTrigger.Signaling != TriggerSignaling.Override)
          {
            if (slideContainer.Slide.TriggerSignal.Signaling == TriggerSignaling.Enabled)
            {
              slideContainer.Slide.TriggerSignal.Send();
            }
          }
        }
      }
      catch (Exception ex)
      {
        MessageBox.Show(ex.Message);
      }
    }

    /// <summary>
    /// This method setups the mouse cursor of the new slide.
    ///   Its sets the position and visibility of the cursor.
    /// </summary>
    /// <param name="slideContainer">
    /// The <see cref="SlidePresentationContainer"/>
    ///   that should be initialized.
    /// </param>
    private void SetupMouse(SlidePresentationContainer slideContainer)
    {
      // Show or hide mouse cursor at specified position.
      if (slideContainer.Slide.MouseCursorVisible)
      {
        // Reset the cursor position to initial location if applicable
        if (slideContainer.Slide.ForceMousePositionChange)
        {
          var newPoint = new Point(
            slideContainer.Slide.MouseInitialPosition.X + this.presentationBounds.Left,
            slideContainer.Slide.MouseInitialPosition.Y + this.presentationBounds.Top);
          Cursor.Position = newPoint;
        }

        if (this.hiddenCursor)
        {
          Cursor.Show();
          this.hiddenCursor = false;
        }
      }
      else
      {
        if (!this.hiddenCursor)
        {
          Cursor.Hide();
          this.hiddenCursor = true;
        }
      }
    }

    /// <summary>
    /// This method invokes the stop of current running
    ///   screen capturings if applicable.
    /// </summary>
    /// <param name="trialChange">
    /// True, if the trial has changed,
    ///   false if it should go on with the next slide in the trial.
    /// </param>
    private void StopScreenCapturing(bool trialChange)
    {
      switch (this.shownContainer)
      {
        case ShownContainer.One:

          // Stop capturing video
          if (this.preparedSlideOne.ScreenCapture != null && trialChange
              && this.preparedSlideOne.ScreenCapture.IsRunning)
          {
            this.preparedSlideOne.ScreenCapture.Stop();

            ////AsyncHelper.FireAndForget(new MethodInvoker(this.preparedSlideOne.ScreenCapture.StopAll));
          }

          break;
        case ShownContainer.Two:

          // Stop capturing video
          if (this.preparedSlideTwo.ScreenCapture != null && trialChange
              && this.preparedSlideTwo.ScreenCapture.IsRunning)
          {
            this.preparedSlideTwo.ScreenCapture.Stop();

            ////AsyncHelper.FireAndForget(new MethodInvoker(this.preparedSlideTwo.ScreenCapture.StopAll));
          }

          break;
      }
    }

    /// <summary>
    ///   This method unhooks the low level keyboard and mouse message filters
    ///   if there are any.
    /// </summary>
    private void UnhookMessageFilter()
    {
      if (this.keyboardHookID != IntPtr.Zero)
      {
        MessageHook.UnhookWindowsHookEx(this.keyboardHookID);
        this.keyboardHookID = IntPtr.Zero;
      }

      if (this.mouseHookID != IntPtr.Zero)
      {
        MessageHook.UnhookWindowsHookEx(this.mouseHookID);
        this.mouseHookID = IntPtr.Zero;
      }
    }

    /// <summary>
    /// The <see cref="WebBrowser.DocumentCompleted"/> event handler which updates the scroll
    ///   and mouse down subscriptions recursively for all documents
    /// </summary>
    /// <param name="sender">
    /// Source of the event
    /// </param>
    /// <param name="e">
    /// A <see cref="WebBrowserDocumentCompletedEventArgs"/> with the event data
    /// </param>
    private void WebBrowserDocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
    {
      // Get WebBrowser object
      var browser = sender as WebBrowser;

      // Reattach scroll and mouse down events for each Document even in frames
      // otherwise we will not receive mouse down event on subframes.
      if (browser == null)
      {
        return;
      }

      if (browser.Document == null)
      {
        return;
      }

      browser.Document.MouseDown -= this.WebBrowserMouseDown;
      browser.Document.MouseDown += this.WebBrowserMouseDown;

      if (browser.Document.Window != null)
      {
        browser.Document.Window.Scroll -= this.WebBrowserScroll;
        browser.Document.Window.Scroll += this.WebBrowserScroll;
        this.AttachEventHandlerForFrames(browser.Document.Window.Frames);
      }
    }

    /// <summary>
    /// The <see cref="Control.MouseDown"/> event handler which sends
    ///   a mouse down event.
    /// </summary>
    /// <param name="sender">
    /// Source of the event
    /// </param>
    /// <param name="e">
    /// A <see cref="HtmlElementEventArgs"/> with the event data
    /// </param>
    private void WebBrowserMouseDown(object sender, HtmlElementEventArgs e)
    {
      // Set flag for navigated event
      this.isWebbrowserClicked = true;

      long eventTime = -1;
      if (this.getTimeMethod != null)
      {
        eventTime = this.getTimeMethod();
      }

      var browserControl = sender as HtmlDocument;
      if (browserControl != null)
      {
        var htmlElement = browserControl.GetElementsByTagName("HTML")[0];
        var bodyElement = browserControl.Body;

        int scrollTop = htmlElement.ScrollTop > bodyElement.ScrollTop ? htmlElement.ScrollTop : bodyElement.ScrollTop;
        int scrollLeft = htmlElement.ScrollLeft > bodyElement.ScrollLeft ? htmlElement.ScrollLeft : bodyElement.ScrollLeft;
        var mouseEvent = new InputEvent();
        mouseEvent.Type = EventType.Mouse;
        mouseEvent.Task = InputEventTask.Down;
        var msc = new MouseStopCondition(
          e.MouseButtonsPressed,
          false,
          string.Empty,
          null,
          new Point(e.ClientMousePosition.X + scrollLeft, e.ClientMousePosition.Y + scrollTop));
        mouseEvent.Param = msc.ToString();

        this.OnTrialEventOccured(new TrialEventOccuredEventArgs(mouseEvent, eventTime));
      }
    }

    /// <summary>
    /// The <see cref="WebBrowser.Navigating"/> event handler which reactivates the
    ///   scroll event subscribtion and updates the slideshow with the new trial along
    ///   making a new screenshot of the new Document if this site has never been visited before.
    /// </summary>
    /// <param name="sender">
    /// Source of the event
    /// </param>
    /// <param name="e">
    /// A <see cref="WebBrowserNavigatingEventArgs"/> with the event data
    /// </param>
    private void WebBrowserNavigating(object sender, WebBrowserNavigatingEventArgs e)
    {
      lock (this.webBrowserNavigatingLock)
      {
        this.WebBrowserNavigatingLock(sender, e);
      }
    }

    /// <summary>
    /// The web browser_ navigating lock.
    /// </summary>
    /// <param name="sender">
    /// The sender.
    /// </param>
    /// <param name="e">
    /// The e.
    /// </param>
    private void WebBrowserNavigatingLock(object sender, WebBrowserNavigatingEventArgs e)
    {
      try
      {
        // Get WebBrowser object
        var browser = sender as WebBrowser;

        // Check if website has frames which leads to multiple
        // calls to navigated for each frame
        if (this.isWebbrowserClicked)
        {
          // Reset flag to avoid multiple trial notifications on frame loading
          this.isWebbrowserClicked = false;

          // Disable navigation, if max browse depth is reached
          this.numberOfTimesNavigated++;
          if (this.numberOfTimesNavigated >= this.maxBrowseDepth)
          {
            if (browser != null)
            {
              browser.AllowNavigation = false;
            }
          }

          // Get slideshow
          Slideshow documentsSlideshow = Document.ActiveDocument.ExperimentSettings.SlideShow;

          // Make screenshot of newly navigated web page in 
          // separate thread, if it is not already there.
          string screenshotFilename = BrowserDialog.GetFilenameFromUrl(e.Url);
          //WebsiteScreenshot.Instance.ScreenshotFilename = screenshotFilename;

          if (!this.currentBrowserTreeNode.UrlToID.ContainsKey(screenshotFilename))
          {
            // This webpage has never been visited before, so get a new id
            // and create slide and slideshownode for the slideshow
            int newTrialId = Convert.ToInt32(documentsSlideshow.GetUnusedNodeID());
            this.currentBrowserTreeNode.UrlToID.Add(screenshotFilename, newTrialId);

            // Create new trial
            string newName = Path.GetFileNameWithoutExtension(screenshotFilename);
            var newWebpageTrial = new Trial(newName, newTrialId);

            // Create VGScrollImageSlide
            var newWebpageSlide = (Slide)this.shownSlideContainer.Slide.Clone();
            newWebpageSlide.Modified = true;
            if (!newWebpageSlide.IsThumbNull)
            {
              newWebpageSlide.Thumb.Dispose();
              newWebpageSlide.Thumb = null;
            }

            newWebpageSlide.Name = newName;
            newWebpageSlide.ActiveXStimuli.Clear();
            newWebpageSlide.VGStimuli.Clear();

            var baseUrlScreenshot = new VGScrollImage(
              ShapeDrawAction.None,
              Pens.Transparent,
              Brushes.Black,
              SystemFonts.DefaultFont,
              Color.Black,
              Path.GetFileName(screenshotFilename),
              Document.ActiveDocument.ExperimentSettings.SlideResourcesPath,
              ImageLayout.None,
              1f,
              Document.ActiveDocument.PresentationSize,
              VGStyleGroup.None,
              newWebpageSlide.Name,
              string.Empty);

            newWebpageSlide.VGStimuli.Add(baseUrlScreenshot);

            // Finish creating new trial
            newWebpageTrial.Add(newWebpageSlide);

            // Update trial lists with new trial
            TrialsIncludingNavigatedWebpages.Add(newWebpageTrial);

            // Now update slideshow with new trial
            // Create node
            var slideNode = new SlideshowTreeNode(newName);
            slideNode.Name = newTrialId.ToString(CultureInfo.InvariantCulture);
            slideNode.Slide = newWebpageSlide;

            // Add node to slideshow at browser tree node subgroup
            this.currentBrowserTreeNode.Nodes.Add(slideNode);
            documentsSlideshow.IsModified = true;
            Document.ActiveDocument.Modified = true;
          }

          if (this.getTimeMethod != null)
          {
            this.getTimeMethod();
          }

          // Get the corrent trial ID
          // use the existing, if we have already a screenshot of this url
          int trialID = this.currentBrowserTreeNode.UrlToID[screenshotFilename];
          int trialIndex = TrialsIncludingNavigatedWebpages.GetIndexOfTrialByID(trialID);
          Trial trial = TrialsIncludingNavigatedWebpages[trialIndex];

          // Reset slide counter
          // but do not increase trial counter, otherwise
          // we would have wrong counting in CheckForSlideChange
          // instead note the trials added
          this.slideCounter = 0;
          this.trialsAdded++;

          // Send counter changed event to increase trial sequence
          // and update recorders control view
          this.OnCounterChanged(new CounterChangedEventArgs(trialID, this.slideCounter));

          long webcamTime = this.userCamera != null ? this.userCamera.GetCurrentTime() : -1;

          var navigatedCondition = new NavigatedStopCondition(e.Url);
          this.OnTrialChanged(
            new TrialChangedEventArgs(
              this.shownSlideContainer.Trial,
              trial,
              navigatedCondition,
              "Webpage",
              this.trialCounter + this.trialsAdded,
              webcamTime));

          // Update shown slide container
          this.shownSlideContainer.Trial = trial;
          this.shownSlideContainer.Slide = trial[0];

          var fullFile = Path.Combine(Document.ActiveDocument.ExperimentSettings.SlideResourcesPath, screenshotFilename);
          if (!File.Exists(fullFile))
          {
            // Create screenshot on Document completed event using
            // the filename stated above
            var navigateThread = new Thread(this.NavigateMirror);
            navigateThread.SetApartmentState(ApartmentState.STA);
            var parameters = new object[2];
            parameters[0] = e;
            parameters[1] = fullFile;
            navigateThread.Start(parameters);
          }
        }
      }
      catch (Exception ex)
      {
        ExceptionMethods.HandleException(ex);
      }
    }

    /// <summary>
    /// The <see cref="WebBrowser.NewWindow"/> event handler which disables
    ///   popups.
    /// </summary>
    /// <param name="sender">
    /// Source of the event
    /// </param>
    /// <param name="e">
    /// A <see cref="CancelEventArgs"/> with the event data
    /// </param>
    private void WebBrowser_NewWindow(object sender, CancelEventArgs e)
    {
      // Disable popups.
      e.Cancel = true;
    }

    /// <summary>
    /// The Scroll event handler of the web Document in which the scroll
    ///   position is sent to the recorder as a trial event.
    /// </summary>
    /// <param name="sender">
    /// Source of the event.
    /// </param>
    /// <param name="e">
    /// A <see cref="HtmlElementEventArgs"/> with the event data.
    /// </param>
    private void WebBrowserScroll(object sender, HtmlElementEventArgs e)
    {
      var browserControl = sender as HtmlWindow;
      if (browserControl == null)
      {
        return;
      }

      if (browserControl.Document == null)
      {
        return;
      }

      var htmlElement = browserControl.Document.GetElementsByTagName("HTML")[0];
      var bodyElement = browserControl.Document.Body;

      int scrollTop = htmlElement.ScrollTop > bodyElement.ScrollTop ? htmlElement.ScrollTop : bodyElement.ScrollTop;
      int scrollLeft = htmlElement.ScrollLeft > bodyElement.ScrollLeft ? htmlElement.ScrollLeft : bodyElement.ScrollLeft;

      // Avoid double scroll events.
      var newScrollPosition = new Point(scrollLeft, scrollTop);
      if (!this.lastScrollEventPosition.Equals(newScrollPosition))
      {
        this.lastScrollEventPosition = newScrollPosition;

        long eventTime = -1;
        if (this.getTimeMethod != null)
        {
          eventTime = this.getTimeMethod();
        }

        // Store marker event
        var scrollEvent = new MediaEvent();
        scrollEvent.Type = EventType.Scroll;
        scrollEvent.Task = MediaEventTask.Seek;
        scrollEvent.Param = scrollLeft.ToString("N0", this.nfi) + ";" + scrollTop.ToString("N0", this.nfi);
        this.OnTrialEventOccured(new TrialEventOccuredEventArgs(scrollEvent, eventTime));
      }
    }

    /// <summary>
    /// The <see cref="Form.FormClosing"/> event handler.
    ///   Stops the stopwatch timer.
    /// </summary>
    /// <param name="sender">
    /// Source of the event.
    /// </param>
    /// <param name="e">
    /// A <see cref="FormClosingEventArgs"/> with the event data.
    /// </param>
    private void FrmPresenterFormClosing(object sender, FormClosingEventArgs e)
    {
      this.watch.Stop();

      //// Cleanup website screenshot creator
      //if (WebsiteScreenshot.HasBeenUsed)
      //{
      //  WebsiteScreenshot.Instance.Dispose();
      //}
    }

    /// <summary>
    /// The <see cref="Control.KeyDown"/> event handler.
    ///   The receiving of key press events has moved to
    ///   <see cref="ProcessCmdKey"/> because this method
    ///   allows logging of modifier keys also.
    /// </summary>
    /// <param name="sender">
    /// Source of the event.
    /// </param>
    /// <param name="e">
    /// A <see cref="KeyEventArgs"/> with the event data.
    /// </param>
    private void FrmPresenterKeyDown(object sender, KeyEventArgs e)
    {
      // _currentKey = (e.KeyData & e.KeyCode);
      // This has moved to ProcessCmdKey
    }

    /// <summary>
    /// The <see cref="Control.MouseDown"/> event handler.
    ///   Sets the <see cref="currentMousebutton"/> and raises
    ///   the <see cref="TrialEventOccured"/> event.
    /// </summary>
    /// <remarks>
    /// If the mouse button is a response that indicates
    ///   a slide change, do it.
    /// </remarks>
    /// <param name="sender">
    /// Source of the event.
    /// </param>
    /// <param name="e">
    /// A <see cref="MouseEventArgs"/> with the event data.
    /// </param>
    private void FrmPresenterMouseDown(object sender, MouseEventArgs e)
    {
      this.currentMousebutton = e.Button;
      long eventTime = -1;
      if (this.getTimeMethod != null)
      {
        eventTime = this.getTimeMethod();
      }

      if (!this.CheckforSlideChange(false))
      {
        var mouseEvent = new InputEvent();
        mouseEvent.Type = EventType.Mouse;
        mouseEvent.Task = InputEventTask.Down;
        var msc = new MouseStopCondition(e.Button, false, string.Empty, null, e.Location);
        mouseEvent.Param = msc.ToString();
        this.OnTrialEventOccured(new TrialEventOccuredEventArgs(mouseEvent, eventTime));

        this.CheckforAudioStimulusOnClick(this.shownSlideContainer, e.Location, eventTime);
      }
    }

    /// <summary>
    /// The <see cref="Control.MouseUp"/> event handler.
    ///   Resets the <see cref="currentMousebutton"/> to
    ///   <see cref="MouseButtons.None"/>.
    /// </summary>
    /// <param name="sender">
    /// Source of the event.
    /// </param>
    /// <param name="e">
    /// A <see cref="MouseEventArgs"/> with the event data.
    /// </param>
    private void FrmPresenterMouseUp(object sender, MouseEventArgs e)
    {
      this.currentMousebutton = MouseButtons.None;
      long eventTime = -1;
      if (this.getTimeMethod != null)
      {
        eventTime = this.getTimeMethod();
      }

      var mouseEvent = new InputEvent();
      mouseEvent.Type = EventType.Mouse;
      mouseEvent.Task = InputEventTask.Up;
      var msc = new MouseStopCondition(e.Button, false, string.Empty, null, e.Location);
      mouseEvent.Param = msc.ToString();
      this.OnTrialEventOccured(new TrialEventOccuredEventArgs(mouseEvent, eventTime));
    }

    ///////////////////////////////////////////////////////////////////////////////
    // Eventhandler for Custom Defined Events                                    //
    ///////////////////////////////////////////////////////////////////////////////

    /// <summary>
    /// The <see cref="MultimediaTimer.Tick"/> event handler for the
    ///   <see cref="MultimediaTimer"/>.
    ///   Is triggered whenever it is time to change the displayed slide.
    /// </summary>
    /// <param name="sender">
    /// Source of the event.
    /// </param>
    /// <param name="e">
    /// An empty <see cref="EventArgs"/>
    /// </param>
    private void TimerTick(object sender, EventArgs e)
    {
      this.CheckforSlideChange(true);
    }

    #endregion
  }
}