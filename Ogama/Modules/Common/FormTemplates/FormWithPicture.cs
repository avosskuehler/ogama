// <copyright file="FormWithPicture.cs" company="FU Berlin">
// ******************************************************
// OGAMA - open gaze and mouse analyzer 
// Copyright (C) 2015 Dr. Adrian Voßkühler  
// ------------------------------------------------------------------------
// This program is free software; you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation; either version 2 of the License, or (at your option) any later version.
// This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
// You should have received a copy of the GNU General Public License along with this program; if not, write to the Free Software Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA 02111-1307 USA
// **************************************************************
// </copyright>
// <author>Adrian Voßkühler</author>
// <email>adrian@ogama.net</email>

namespace Ogama.Modules.Common.FormTemplates
{
  using System;
  using System.Collections.Generic;
  using System.ComponentModel;
  using System.Drawing;
  using System.IO;
  using System.Windows.Forms;

  using Ogama.ExceptionHandling;
  using Ogama.MainWindow;
  using Ogama.Modules.Common.Controls;
  using Ogama.Modules.Common.SlideCollections;
  using Ogama.Modules.Common.TrialEvents;
  using Ogama.Modules.Common.Types;

  using OgamaControls;
  using OgamaControls.Media;

  using VectorGraphics.Canvas;
  using VectorGraphics.Elements;
  using VectorGraphics.Elements.ElementCollections;
  using VectorGraphics.Tools;

  /// <summary>
  /// Inherits <see cref="FormWithInterface"/>.
  /// This class is the base class for all modules that contain a
  /// <see cref="Picture"/> element with <see cref="VGElement"/> drawing
  /// elements. It contains common methods like <see cref="LoadSlide(Slide,ActiveXMode)"/>
  /// to populate the <see cref="Picture"/>.
  /// </summary>
  public partial class FormWithPicture : FormWithInterface
  {
    ///////////////////////////////////////////////////////////////////////////////
    // Defining Constants                                                        //
    ///////////////////////////////////////////////////////////////////////////////
    #region CONSTANTS

    /// <summary>
    /// This is a slide that shows a slide not found instruction
    /// when starting the module.
    /// </summary>
    private static Slide slideNotFoundSlide = new Slide(
      "Background",
      Color.White,
      Images.CreateNotFoundImage(Document.ActiveDocument != null ? Document.ActiveDocument.PresentationSize : new Size(1024, 768), "Slide"),
      new VectorGraphics.StopConditions.StopConditionCollection(),
      null,
      string.Empty,
      Document.ActiveDocument != null ? Document.ActiveDocument.PresentationSize : new Size(1024, 768));

    #endregion //CONSTANTS

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Variables, Enumerations, Events                                  //
    ///////////////////////////////////////////////////////////////////////////////
    #region FIELDS

    /// <summary>
    /// Holds canvas picture. Has to be set during intialization.
    /// </summary>
    private Picture picture;

    /// <summary>
    /// This member saves the current visited trial with its contained
    /// slides.
    /// </summary>
    private Trial currentTrial;

    /// <summary>
    /// The <see cref="DESAudioPlayer"/> that is used to stimuli play audio files.
    /// </summary>
    private DESAudioPlayer audioPlayer;

    /// <summary>
    /// A <see cref="SortedList{Int32, TrialEvent}"/> that 
    /// is filled with the events for the current trial.
    /// Its contents can be accessed via the EventID.
    /// </summary>
    private SortedList<int, TrialEvent> trialEvents;

    /// <summary>
    /// Saves the zoom trackBar.
    /// </summary>
    private ToolStripTrackBar zoomTrackBar;

    #endregion //FIELDS

    ///////////////////////////////////////////////////////////////////////////////
    // Construction and Initializing methods                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region CONSTRUCTION

    /// <summary>
    /// Initializes a new instance of the FormWithPicture class.
    /// </summary>
    public FormWithPicture()
    {
      this.audioPlayer = new DESAudioPlayer();
    }

    #endregion //CONSTRUCTION

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Enumerations                                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region ENUMS

    /// <summary>
    /// The delegate for the thread safe call to a point set method.
    /// </summary>
    /// <param name="newValue">The new <see cref="Point"/> to set.</param>
    private delegate void SetPointCallback(Point newValue);

    /// <summary>
    /// The delegate for the thread safe call to a size set method.
    /// </summary>
    /// <param name="newValue">The new <see cref="Size"/> to set.</param>
    private delegate void SetSizeCallback(Size newValue);

    /// <summary>
    /// The delegate for the thread safe call to a rectangle set method.
    /// </summary>
    /// <param name="newValue">The new <see cref="Rectangle"/> to set.</param>
    private delegate void SetRectangleCallback(Rectangle newValue);

    #endregion ENUMS

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Properties                                                       //
    ///////////////////////////////////////////////////////////////////////////////
    #region PROPERTIES

    /// <summary>
    /// Gets or sets a <see cref="SortedList{Int32, TrialEvent}"/> that 
    /// is filled with the events for the current trial.
    /// Its contents can be accessed via the EventID.
    /// </summary>
    /// <value>The SortedList that contains all events for the current trial.</value>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public SortedList<int, TrialEvent> TrialEvents
    {
      get { return this.trialEvents; }
      set { this.trialEvents = value; }
    }

    /// <summary>
    /// Gets or sets the canvas picture of this form.
    /// </summary>
    /// <value>The <see cref="Picture"/> that is the canvas of the form.</value>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Picture Picture
    {
      get { return this.picture; }
      set { this.picture = value; }
    }

    /// <summary>
    /// Gets or sets the zoom track bar.
    /// </summary>
    /// <value>The <see cref="ToolStripTrackBar"/> that is zoom track bar of the form.</value>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public ToolStripTrackBar ZoomTrackBar
    {
      get { return this.zoomTrackBar; }
      set { this.zoomTrackBar = value; }
    }

    /// <summary>
    /// Gets or sets the current visited trial with its contained
    /// slides.
    /// </summary>
    /// <value>The current visited <see cref="Trial"/> with its contained
    /// slides..</value>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Trial CurrentTrial
    {
      get { return this.currentTrial; }
      set { this.currentTrial = value; }
    }

    /// <summary>
    /// Gets or sets the <see cref="DESAudioPlayer"/> that is used to stimuli play audio files.
    /// </summary>
    /// <value>The <see cref="DESAudioPlayer"/> that is used to stimuli play audio files.</value>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public DESAudioPlayer Player
    {
      get { return this.audioPlayer; }
      set { this.audioPlayer = value; }
    }

    /// <summary>
    /// Gets a slide that shows a slide not found instruction
    /// when starting the module.
    /// </summary>
    protected static Slide SlideNotFoundSlide
    {
      get { return slideNotFoundSlide; }
    }

    #endregion //PROPERTIES

    ///////////////////////////////////////////////////////////////////////////////
    // Public methods                                                            //
    ///////////////////////////////////////////////////////////////////////////////
    #region PUBLICMETHODS

    /// <summary>
    /// Gets a <see cref="Rectangle"/> with the bounds that are
    /// centered to the given Panel and are proportional sized 
    /// to the experiments stimulus screen.
    /// </summary>
    /// <param name="canvas">A <see cref="Control"/> in which the rectangle should be
    /// sized</param>
    /// <returns>A <see cref="Rectangle"/> that is proportional sized.</returns>
    protected Rectangle GetProportionalBounds(Control canvas)
    {
      // if (Document.ActiveDocument != null)
      // {
      //  int width = (int)Document.ActiveDocument.ExperimentSettings.WidthStimulusScreen;
      //  int height = (int)Document.ActiveDocument.ExperimentSettings.HeightStimulusScreen;
      //  int canvasWidth = this.Picture.Parent.Parent.Width;
      //  int canvasHeight = this.Picture.Parent.Parent.Width;
      //  float screenRatio = width / (float)height;
      //  if (canvasHeight * screenRatio > canvasWidth)
      //  {
      //    // Width is correct
      //    dstRect.X = 0;
      //    dstRect.Width = canvas.Width;
      //    float newHeight = (float)canvas.Width / width * height;
      //    dstRect.Y = (int)(canvas.Height / 2f - newHeight / 2);
      //    dstRect.Height = (int)newHeight;
      //  }
      //  else
      //  {
      //    // Height is correct
      //    dstRect.Y = 0;
      //    dstRect.Height = canvas.Height;
      //    float newWidth = (float)canvas.Height / height * width;
      //    dstRect.X = (int)(canvas.Width / 2f - newWidth / 2);
      //    dstRect.Width = (int)newWidth;
      //  }
      // }
      Rectangle dstRect = new Rectangle();
      dstRect.X = (int)Math.Max(0, (canvas.Width / 2f - this.Picture.Width / 2));
      dstRect.Width = this.Picture.Width;
      dstRect.Y = (int)(int)Math.Max(0, (canvas.Height / 2f - this.Picture.Height / 2));
      dstRect.Height = this.Picture.Height;

      return dstRect;
    }

    #endregion //PUBLICMETHODS

    ///////////////////////////////////////////////////////////////////////////////
    // Inherited methods                                                         //
    ///////////////////////////////////////////////////////////////////////////////
    #region OVERRIDES

    /// <summary>
    /// Overridden. Frees resources of objects that are not disposed
    /// by the designer, mainly private objects.
    /// Is called during the call to <see cref="Control.Dispose(Boolean)"/>.
    /// </summary>
    protected override void CustomDispose()
    {
      if (this.audioPlayer != null)
      {
        this.audioPlayer.Close();
      }

      if (this.zoomTrackBar != null)
      {
        this.zoomTrackBar.ValueChanged -= new EventHandler(this.zoomTrackBar_ValueChanged);
        this.zoomTrackBar.Click -= new EventHandler(this.zoomTrackBar_Click);
      }

      base.CustomDispose();
    }

    /// <summary>
    /// Initializes accelerator keys. Binds to methods.
    /// </summary>
    protected override void InitAccelerators()
    {
      base.InitAccelerators();
      this.SetAccelerator(Keys.F, new AcceleratorAction(this.OnPressF));
    }

    /// <summary>
    /// Overridden. Initializes zoom trackbar event handler.
    /// </summary>
    protected override void InitializeCustomElements()
    {
      base.InitializeCustomElements();
      if (this.zoomTrackBar != null)
      {
        this.zoomTrackBar.ValueChanged += new EventHandler(this.zoomTrackBar_ValueChanged);
        this.zoomTrackBar.Click += new EventHandler(this.zoomTrackBar_Click);
      }
    }

    /// <summary>
    /// Virtual empty method that should be overwritten with code
    /// that loads data from the database and updates the canvas with the new contents.
    /// </summary>
    /// <returns><strong>True</strong> if successful, otherwise <strong>false</strong>.</returns>
    protected virtual bool NewTrialSelected()
    {
      return false;
    }

    /// <summary>
    /// Overrides should check for the current documents selection state
    /// that means: Subject and TrialID and load new data.
    /// </summary>
    protected virtual void ReadSelectionStateAndShowData()
    {
    }

    /// <summary>
    /// The <see cref="MainForm.OptionsChanged"/> event handler for the
    /// parent <see cref="Form"/> <see cref="MainForm"/>.
    /// This method reinitializes the picture with the new experiment
    /// settings.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    protected override void mainWindow_OptionsChanged(object sender, EventArgs e)
    {
      this.picture.ResetPicture();
    }

    /// <summary>
    /// The <see cref="MainForm.StimulusChanged"/> event handler for the
    /// parent <see cref="Form"/> <see cref="MainForm"/>.
    /// This method handles the stimulus changed event from the main form
    /// and updates the form and the picture.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    protected override void mainWindow_StimulusChanged(object sender, EventArgs e)
    {
      try
      {
        if (this.MdiParent != null && this.MdiParent.ActiveMdiChild.Name == this.Name)
        {
          this.ReadSelectionStateAndShowData();
        }
      }
      catch (Exception ex)
      {
        string message = "This stimulus could not be displayed." + Environment.NewLine +
          "Maybe its not in the current subjects trial list." + Environment.NewLine +
          ex.Message;
        ExceptionMethods.ProcessErrorMessage(message);
      }
    }

    /// <summary>
    /// The <see cref="MainForm.EditCopy"/> event handler for the
    /// parent <see cref="Form"/> <see cref="MainForm"/>.
    /// This method handles the edit copy event from main form
    /// by  rendering a copy of the displayed picture 
    /// to clipboard.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    protected override void mainWindow_EditCopy(object sender, EventArgs e)
    {
      if (this.MdiParent != null && this.MdiParent.ActiveMdiChild.Name == this.Name)
      {
        Clipboard.SetImage(this.picture.RenderToImage());
        ((MainForm)this.MdiParent).StatusLabel.Text = "Image exported to clipboard.";
      }
    }

    /// <summary>
    /// The <see cref="MainForm.EditPaste"/> event handler for the
    /// parent <see cref="Form"/> <see cref="MainForm"/>.
    /// No default implementation. Should be implemented in overrides.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    protected override void mainWindow_EditPaste(object sender, EventArgs e)
    {
    }

    /// <summary>
    /// The <see cref="MainForm.EditSaveImage"/> event handler for the
    /// parent <see cref="Form"/> <see cref="MainForm"/>.
    /// This method handles the edit save image event from main form
    /// by rendering a copy of the diplayed picture onto a file format
    /// asked through a dialog.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    protected override void mainWindow_EditSaveImage(object sender, EventArgs e)
    {
      if (this.MdiParent.ActiveMdiChild.Name == this.Name)
      {
        if (Images.ExportImageToFile(this.picture.RenderToImage()))
        {
          ((MainForm)this.MdiParent).StatusLabel.Text = "Image successfully saved";
        }
      }
    }

    /// <summary>
    /// Overridden <see cref="Form.OnResize(EventArgs)"/>. 
    /// Starts the autozoom procedure for the picture.
    /// </summary>
    /// <param name="e">Empty event args</param>
    protected override void OnResize(EventArgs e)
    {
      base.OnResize(e);
      this.AutoZoomPicture();
    }

    /// <summary>
    /// Overridden <see cref="Form.OnLoad(EventArgs)"/>. 
    /// Starts the autozoom procedure for the picture.
    /// </summary>
    /// <param name="e">Empty event args</param>
    protected override void OnLoad(EventArgs e)
    {
      base.OnLoad(e);
      this.AutoZoomPicture();
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
    #endregion //WINDOWSEVENTHANDLER

    ///////////////////////////////////////////////////////////////////////////////
    // Eventhandler for Custom Defined Events                                    //
    ///////////////////////////////////////////////////////////////////////////////
    #region CUSTOMEVENTHANDLER

    /// <summary>
    /// The <see cref="Control.Resize"/> event handler for the
    /// picture.
    /// When this panel is resized, the <see cref="Picture"/> has to be rescaled.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    protected void pnlCanvas_Resize(object sender, EventArgs e)
    {
      this.ResizeCanvas();
    }

    /// <summary>
    /// Hides or shows picture. Only for debug purposes.
    /// </summary>
    protected void OnPressF()
    {
      this.picture.Visible = !this.picture.Visible;
    }

    /// <summary>
    /// The <see cref="TrackBar.ValueChanged"/> event handler
    /// for the zoom track bar control. Calls resizing of the picture.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    protected void zoomTrackBar_ValueChanged(object sender, EventArgs e)
    {
      float zoomfactor = this.zoomTrackBar.Value / 50f;
      this.ZoomPicture(zoomfactor);
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler
    /// for the zoom track bar control. Calls the autozoom procedure
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    protected void zoomTrackBar_Click(object sender, EventArgs e)
    {
      if (e is MouseEventArgs)
      {
        MouseEventArgs args = e as MouseEventArgs;
        if (args.Button == MouseButtons.Right)
        {
          this.AutoZoomPicture();
        }
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
    #region METHODS

    /// <summary>
    /// This method loads the <see cref="Trial"/> with the given ID
    /// into the <see cref="Picture"/> by displaying its first <see cref="Slide"/>
    /// if there is any.
    /// </summary>
    /// <param name="trialID">An <see cref="Int32"/> with the trial ID.</param>
    /// <returns><strong>True</strong> if successful, otherwise <strong>false</strong>.</returns>
    protected bool LoadTrialStimulus(int trialID)
    {
      this.currentTrial = Document.ActiveDocument.ExperimentSettings.SlideShow.GetTrialByID(trialID);
      if (this.currentTrial == null)
      {
        return false;
      }

      Slide initialSlide = this.currentTrial[0];

      this.LoadSlide(initialSlide, ActiveXMode.BehindPicture);

      ((MainForm)this.MdiParent).StatusLabel.Text = "Ready ...";

      return true;
    }

    /// <summary>
    /// This method checks the current trial for the contained
    /// slides and if there is more than one it populates
    /// the given <see cref="TrialTimeLine"/> with <see cref="TrialEvents"/>.
    /// </summary>
    /// <param name="timeline">A <see cref="TrialTimeLine"/> to display the trial sections.</param>
    protected void LoadTrialSlidesIntoTimeline(TrialTimeLine timeline)
    {
      // Update time line with events
      int slideCount = this.CurrentTrial.Count;
      if (slideCount > 1)
      {
        timeline.Duration = slideCount * 1000;
        this.Picture.SectionEndTime = slideCount * 1000;

        SortedList<int, TrialEvent> trialEvents = new SortedList<int, TrialEvent>();
        for (int i = 0; i < slideCount; i++)
        {
          Slide slide = this.CurrentTrial[i];
          TrialEvent slideEvent = new TrialEvent();
          slideEvent.Type = EventType.Slide;
          int time = i * 1000;
          slideEvent.Time = time;
          if (i > 0)
          {
            trialEvents.Add(time, slideEvent);
          }
        }

        timeline.TrialEvents = trialEvents;
        timeline.HighlightFirstSlide();
      }
      else
      {
        timeline.RemoveEvents();
      }
    }

    /// <summary>
    /// This method populates the <see cref="DESAudioPlayer"/> with the
    /// audio files that are called in the current trial at the
    /// correct starting times, so that they could be played and seeked along with the
    /// replay visualization.
    /// </summary>
    /// <param name="trialEvents">The complete <see cref="SortedList{Int32, TrialEvent}"/>
    /// of trial events to be parsed for audio stimuli.</param>
    /// <returns><strong>True</strong> if successful, otherwise <strong>false</strong>.</returns>
    protected bool LoadAudioStimuli(SortedList<int, TrialEvent> trialEvents)
    {
      try
      {
        if (this.currentTrial == null)
        {
          return false;
        }

        // Initialize player
        if (this.audioPlayer != null)
        {
          this.audioPlayer.Close();
          this.audioPlayer = null;
        }

        List<long> slideStartTimes = new List<long>();
        slideStartTimes.Add(0);
        List<TrialEvent> audioEvents = new List<TrialEvent>();

        foreach (TrialEvent trialEvent in trialEvents.Values)
        {
          if (trialEvent.Type == EventType.Slide)
          {
            slideStartTimes.Add(trialEvent.Time);
          }

          if (trialEvent.Type == EventType.Audio)
          {
            audioEvents.Add(trialEvent);
          }
        }

        for (int i = 0; i < this.currentTrial.Count; i++)
        {
          Slide slide = this.currentTrial[i];

          // Background sound
          if (slide.BackgroundSound != null && slide.BackgroundSound.ShouldPlay)
          {
            // Create only if needed
            if (this.audioPlayer == null)
            {
              this.audioPlayer = new DESAudioPlayer();
            }

            this.audioPlayer.AddSoundAtPosition(slide.BackgroundSound.FullFilename, slideStartTimes[i]);
          }

          // Parse elements for audio
          foreach (VGElement element in slide.VGStimuli)
          {
            if (element.Sound != null && element.Sound.ShouldPlay && !element.Sound.ShowOnClick)
            {
              // Create only if needed
              if (this.audioPlayer == null)
              {
                this.audioPlayer = new DESAudioPlayer();
              }

              this.audioPlayer.AddSoundAtPosition(element.Sound.FullFilename, slideStartTimes[i]);
            }
          }
        }

        // Parse elements for audio
        foreach (TrialEvent audioEvent in audioEvents)
        {
          string filename = Path.Combine(
            Document.ActiveDocument.ExperimentSettings.SlideResourcesPath,
            Path.GetFileName(Path.GetFileName(audioEvent.Param)));

          // Create only if needed
          if (this.audioPlayer == null)
          {
            this.audioPlayer = new DESAudioPlayer();
          }

          this.audioPlayer.AddSoundAtPosition(filename, audioEvent.Time);
        }

        // Initialize audioplayer for replay on time.
        if (this.audioPlayer != null)
        {
          this.audioPlayer.Initialize();
        }
      }
      catch (Exception ex)
      {
        ExceptionMethods.HandleException(ex);

        return false;
      }

      return true;
    }

    /// <summary>
    /// This method loads the given slide content into the background of the
    /// <see cref="Picture"/>. If applicable any flash objects are
    /// initialized
    /// </summary>
    /// <param name="slide">The new <see cref="Slide"/> to be displayed in the 
    /// background of the <see cref="Picture"/>.</param>
    /// <param name="activeXMode">The <see cref="ActiveXMode"/> that should
    /// be used to display slide and activeX controls. Use this
    /// to display them on top or in the background.</param>
    protected void LoadSlide(Slide slide, ActiveXMode activeXMode)
    {
      // This releases the resources of the used slide
      // including explicit disposing of flash objects
      this.Picture.ResetBackground();

      if (slide == null)
      {
        return;
      }

      // Set presentation size
      slide.PresentationSize = Document.ActiveDocument.PresentationSize;

      if (slide.StimulusSize != Size.Empty)
      {
        this.Picture.StimulusSize = slide.StimulusSize;
      }
      else
      {
        this.Picture.StimulusSize = slide.PresentationSize;
      }

      this.Picture.PresentationSize = slide.PresentationSize;

      Slide slideCopy = (Slide)slide.Clone();
      switch (activeXMode)
      {
        case ActiveXMode.Off:
          // set Pictures new background slide
          this.Picture.BgSlide = slideCopy;
          break;
        default:
        case ActiveXMode.BehindPicture:
          // set Pictures new background slide
          this.Picture.BgSlide = slideCopy;

          foreach (VGElement element in slideCopy.ActiveXStimuli)
          {
            if (element is VGFlash)
            {
              VGFlash flash = element as VGFlash;
              flash.InitializeOnControl(this.Picture.Parent, false, this.Picture.StimulusToScreen);
            }
            else if (element is VGBrowser)
            {
              // Don´t show browser control, use screenshot of whole website instead.
              // VGBrowser browser = element as VGBrowser;
              // browser.InitializeOnControl(this.Picture.Parent, false);
            }
          }

          break;
        case ActiveXMode.OnTop:
          // set Pictures new background slide
          this.Picture.BgSlide = slideCopy;

          foreach (VGElement element in slideCopy.ActiveXStimuli)
          {
            if (element is VGFlash)
            {
              VGFlash flash = element as VGFlash;
              flash.InitializeOnControl(this.Picture, false, this.Picture.StimulusToScreen);
            }
            else if (element is VGBrowser)
            {
              VGBrowser browser = element as VGBrowser;
              browser.InitializeOnControl(this.Picture, false);
            }
          }

          break;
        case ActiveXMode.Video:
          // Don´t use Slide
          this.Picture.BgSlide = null;
          break;
      }

      // Set autozoom, because websites could have changed in size
      this.AutoZoomPicture();

      // Redraw picture
      this.Picture.Invalidate();
    }

    /// <summary>
    /// The <see cref="Control.Resize"/> event handler for the
    /// <see cref="Panel"/> <code>pnlCanvas</code> which is implemented in 
    /// all implementations of this class.
    /// When this panel is resized, the <see cref="Picture"/> has to be rescaled.
    /// </summary>
    /// <exception cref="ArgumentNullException">Thrown, when canvas of picture (which is the parent of its parent) is null.</exception>
    protected void ResizeCanvas()
    {
      if (this.Picture.Parent.Parent == null)
      {
        throw new ArgumentNullException("Canvas Panel should not be null calling ResizePicture");
      }

      if (this.Picture != null && this.Picture.Parent != null)
      {
        this.ThreadSafeSetBounds(this.GetProportionalBounds(this.Picture.Parent.Parent));
        this.Picture.Parent.Invalidate();
      }
    }

    /// <summary>
    /// This method performs an auto zoom procedure to fit the
    /// picture into the bounds of the canvas, without scoll bars.
    /// </summary>
    protected void AutoZoomPicture()
    {
      if (Document.ActiveDocument != null && this.Picture != null)
      {
        int width = (int)Document.ActiveDocument.ExperimentSettings.WidthStimulusScreen;
        int height = (int)Document.ActiveDocument.ExperimentSettings.HeightStimulusScreen;

        // Care of side scrollbars... (-17)
        int canvasWidth = this.Picture.Parent.Parent.Width - 17;
        int canvasHeight = this.Picture.Parent.Parent.Height - 17;
        float screenRatio = width / (float)height;

        float zoomFactor = 1;

        // TODO: This has been left out, cause lead to wrong scalings on VGScrollImages...
        //if (canvasHeight * screenRatio > canvasWidth)
        //{
        //  // Width is correct
        //  zoomFactor = canvasWidth / (float)width;
        //}
        //else
        //{
          // Height is correct
          zoomFactor = canvasHeight / (float)height;
        //}

        // Paranoia Check
        if (zoomFactor == 0)
        {
          zoomFactor = 1;
        }

        this.ZoomPicture(zoomFactor);
      }
    }

    /// <summary>
    /// Zooms the picture canvas with the given factor.
    /// </summary>
    /// <param name="zoomfactor">A <see cref="Single"/>
    /// with the zoom factor, can be from 0.1 to 2.</param>
    protected void ZoomPicture(float zoomfactor)
    {
      // Reset scroll position
      this.ThreadSafeSetAutoScrollPosition(Point.Empty);

      this.Picture.ZoomFactor = zoomfactor;
      Size presentationSize = this.Picture.PresentationSize;
      if (this.Picture.StimulusSize != Size.Empty)
      {
        presentationSize = this.Picture.StimulusSize;
      }

      this.ThreadSafeSetSize(new Size(
        (int)(presentationSize.Width * zoomfactor),
        (int)(presentationSize.Height * zoomfactor)));
      this.ResizeCanvas();

      // Update track bar
      if (this.zoomTrackBar != null && !this.zoomTrackBar.TrackBar.InvokeRequired)
      {
        this.zoomTrackBar.SendValueChangedEvents = false;
        this.zoomTrackBar.Value = (int)Math.Max(1, (zoomfactor * 50));
        this.zoomTrackBar.SendValueChangedEvents = true;
      }
    }

    #endregion //METHODS

    ///////////////////////////////////////////////////////////////////////////////
    // Small helping Methods                                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region HELPER

    /// <summary>
    /// Thread safe set of the bounds of the pictures parent of this form.
    /// </summary>
    /// <param name="newBounds">A <see cref="Rectangle"/> with the new bounds.</param>
    protected void ThreadSafeSetBounds(Rectangle newBounds)
    {
      if (this.Picture == null || this.Picture.Parent == null)
      {
        return;
      }

      if (this.Picture.Parent.InvokeRequired)
      {
        SetRectangleCallback d = new SetRectangleCallback(this.ThreadSafeSetBounds);
        this.Picture.Parent.Invoke(d, new object[] { newBounds });
      }
      else
      {
        this.Picture.Parent.Bounds = newBounds;
      }
    }

    /// <summary>
    /// Thread safe set of the size of the picture of this form.
    /// </summary>
    /// <param name="newSize">A <see cref="Size"/> with the new size.</param>
    protected void ThreadSafeSetSize(Size newSize)
    {
      if (this.Picture.InvokeRequired)
      {
        SetSizeCallback d = new SetSizeCallback(this.ThreadSafeSetSize);
        this.Picture.Invoke(d, new object[] { newSize });
      }
      else
      {
        this.Picture.Size = newSize;
      }
    }

    /// <summary>
    /// Thread safe set of the autoscrollposition of the the scroll panel.
    /// </summary>
    /// <param name="scrollPosition">A <see cref="Point"/> with the new scroll position.</param>
    protected void ThreadSafeSetAutoScrollPosition(Point scrollPosition)
    {
      if (this.Picture.Parent.Parent == null)
      {
        return;
      }

      ScrollableControl scrollPanel = this.Picture.Parent.Parent as ScrollableControl;
      if (scrollPanel == null)
      {
        return;
      }

      if (scrollPanel.InvokeRequired)
      {
        SetPointCallback d = new SetPointCallback(this.ThreadSafeSetAutoScrollPosition);
        scrollPanel.Invoke(d, new object[] { scrollPosition });
      }
      else
      {
        scrollPanel.AutoScrollPosition = scrollPosition;
      }
    }

    /// <summary>
    /// This method converts the coordinates of a specific point in screen coordinates
    /// to the client coordinates of the given controls bounds.
    /// </summary>
    /// <param name="clickLocation">The <see cref="Point"/> in screen coordinates of the presentation screen.</param>
    /// <param name="controlBounds">The <see cref="RectangleF"/> with the bounds
    /// in screen coordinates of the control that should receive the click.</param>
    /// <returns>A <see cref="Point"/> in client coordinates of the given controls bounds.</returns>
    protected Point GetTransformedFlashClickLocation(Point clickLocation, RectangleF controlBounds)
    {
      PointF relativeLocation = clickLocation;
      try
      {
        relativeLocation.X -= controlBounds.X;
        relativeLocation.Y -= controlBounds.Y;

        PointF[] mousePts = { relativeLocation };
        this.Picture.StimulusToScreen.TransformPoints(mousePts);
        Point scaledLocation = Point.Round(mousePts[0]);
        return scaledLocation;
      }
      catch (Exception)
      {
        // TODO: Omit Object is in use elsewhere exception (StimulusToScreen)
        return Point.Round(relativeLocation);
      }
    }

    #endregion //HELPER
  }
}
