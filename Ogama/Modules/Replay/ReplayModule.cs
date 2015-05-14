// <copyright file="ReplayModule.cs" company="FU Berlin">
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

namespace Ogama.Modules.Replay
{
  using System;
  using System.Collections.Generic;
  using System.Data;
  using System.Drawing;
  using System.IO;
  using System.Runtime.InteropServices;
  using System.Threading;
  using System.Windows.Forms;
  using DirectShowLib;

  using GTHardware.Cameras.DirectShow;

  using Ogama.DataSet;
  using Ogama.ExceptionHandling;
  using Ogama.MainWindow;
  using Ogama.MainWindow.ContextPanel;
  using Ogama.Modules.Common.Controls;
  using Ogama.Modules.Common.CustomEventArgs;
  using Ogama.Modules.Common.FormTemplates;
  using Ogama.Modules.Common.Tools;
  using Ogama.Modules.Common.TrialEvents;
  using Ogama.Modules.Common.Types;
  using Ogama.Modules.Replay.Video;
  using Ogama.Properties;
  using OgamaControls;
  using OgamaControls.Dialogs;
  using OgamaControls.Media;

  using VectorGraphics.Elements;
  using VectorGraphics.Elements.ElementCollections;
  using VectorGraphics.Tools;
  using VectorGraphics.Tools.CustomEventArgs;

  /// <summary>
  /// Derived from <see cref="FormWithSubjectAndTrialSelection"/>.
  /// This <see cref="Form"/> hosts the replay module.
  /// It handles the user interface and the database connection for
  /// the <see cref="ReplayPicture"/>, which is the main element
  /// of this form.
  /// </summary>
  /// <remarks>This interface is intended to replay the data that is
  /// logged during the experiment. A lot of settings can be made, 
  /// concerning the visualization of the data.
  /// Also a video export is implemented.</remarks>
  public partial class ReplayModule : FormWithSubjectAndTrialSelection
  {
    ///////////////////////////////////////////////////////////////////////////////
    // Defining Constants                                                        //
    ///////////////////////////////////////////////////////////////////////////////
    #region CONSTANTS
    #endregion //CONSTANTS

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Variables, Enumerations, Events                                  //
    ///////////////////////////////////////////////////////////////////////////////
    #region FIELDS

    /// <summary>
    /// Indicates that the replay module is currently replaying the
    /// complete experiment, not only the selected trial.
    /// </summary>
    private bool isInContinuousMode;

    /// <summary>
    /// This member save the current viewed time of the trial 
    /// in milliseconds.
    /// </summary>
    private int currentTrialTime;

    /// <summary>
    /// This member saves the current trials video start time.
    /// </summary>
    private long userVideoStartTime;

    /// <summary>
    /// The <see cref="AVPlayer"/> of the <see cref="ContextPanel"/>
    /// that is used to show the user cam video.
    /// </summary>
    private AVPlayer usercamVideoPlayer;

    /// <summary>
    /// The <see cref="VideoFramePusher"/> to send the video frames from
    /// the recording video.
    /// </summary>
    private VideoFramePusher videoFramePusher;

    /// <summary>
    /// Indicates the background update of a trial video instead of
    /// a slide display.
    /// </summary>
    private bool isUsingTrialVideo;

    /// <summary>
    /// A <see cref="DSRecord"/> used to create the avi movie via direct show.
    /// </summary>
    private DSRecord directShowInterface;

    /// <summary>
    /// A <see cref="DSRecordWithDMO"/> used to create the avi movie via direct show
    /// with an overlaid user video.
    /// </summary>
    private DSRecordWithDMO directShowInterfaceWithWebcam;

    /// <summary>
    /// The video export splash window with preview.
    /// </summary>
    private SaveVideoSplash newSplash;

    /// <summary>
    /// The <see cref="VideoExportProperties"/> to be used in the AVI
    /// export section.
    /// </summary>
    private VideoExportProperties videoExportProperties;

    /// <summary>
    /// This class performs the cut of the user video to the correct
    /// length of the selected avi export movie.
    /// </summary>
    private DESCombine desCombine;

    #endregion //FIELDS

    ///////////////////////////////////////////////////////////////////////////////
    // Construction and Initializing methods                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region CONSTRUCTION

    /// <summary>
    /// Initializes a new instance of the ReplayModule class.
    /// </summary>
    public ReplayModule()
    {
      this.InitializeComponent();

      this.Picture = this.replayPicture;
      this.SubjectCombo = this.cbbSubject;
      this.TrialCombo = this.cbbTrial;
      this.ZoomTrackBar = this.trbZoom;

      this.InitializeDropDowns();
      this.InitializeDataBindings();
      this.InitAccelerators();
      this.InitializeCustomElements();
    }

    #endregion //CONSTRUCTION

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Properties                                                       //
    ///////////////////////////////////////////////////////////////////////////////
    #region PROPERTIES

    /// <summary>
    /// Gets the current data state label of the status bar of the main MDI from.
    /// </summary>
    /// <value>A <see cref="ToolStripStatusLabel"/> with the state label of 
    /// the main form.</value>
    public ToolStripStatusLabel LabelDataStates
    {
      get { return ((MainForm)this.MdiParent).StatusRightLabel; }
    }

    /// <summary>
    /// Gets the <see cref="VideoFramePusher"/> to send the video frames from
    /// the recording video.
    /// </summary>
    public VideoFramePusher VideoFramePusher
    {
      get { return this.videoFramePusher; }
    }

    #endregion //PROPERTIES

    ///////////////////////////////////////////////////////////////////////////////
    // Public methods                                                            //
    ///////////////////////////////////////////////////////////////////////////////
    #region PUBLICMETHODS

    /// <summary>
    /// Record is requested from <see cref="ReplayModule"/>, 
    /// so start the video recording with the given settings.
    /// Cancel operation if the video stream could not be created.
    /// </summary>
    public void Record()
    {
      try
      {
        if (!this.GetVideoProperties(out this.videoExportProperties))
        {
          return;
        }

        this.Cursor = Cursors.WaitCursor;

        // If we have no DirectShow class open
        if (this.directShowInterfaceWithWebcam == null)
        {
          try
          {
            // Create and show splash
            this.newSplash = new SaveVideoSplash(this);
            this.newSplash.Show();

            if (File.Exists(this.videoExportProperties.UserVideoProperties.StreamFilename) &&
              this.videoExportProperties.UserVideoProperties.IsStreamRendered)
            {
              // The RenderGazeAndUserVideo() method is called in the 
              // desCombine_Completed(object sender, EventArgs e) method.
              this.CreatePartialUserVideo(this.videoExportProperties);
            }
            else
            {
              this.RenderGazeVideo();
            }
          }
          catch (COMException ce)
          {
            this.newSplash.Close();
            string message = "Video export failed during creating the DirectShow Interface with the following message: "
              + Environment.NewLine
              + ce.Message + Environment.NewLine
              + "Try to choose another compressor.";
            ExceptionMethods.ProcessErrorMessage(message);
          }
        }
        else
        {
          this.directShowInterfaceWithWebcam.Stop();
        }
      }
      catch (Exception ex)
      {
        ExceptionMethods.HandleException(ex);
      }
      finally
      {
        this.Cursor = Cursors.Default;
      }
    }

    /// <summary>
    /// Cancels replay or video export
    /// </summary>
    public void OnEscape()
    {
      // If we have no DirectShow class open
      if (this.directShowInterfaceWithWebcam == null)
      {
        this.StopPlaying();
      }
      else
      {
        this.directShowInterfaceWithWebcam.Stop();
      }

      // If we have no DirectShow class open
      if (this.directShowInterface == null)
      {
        this.StopPlaying();
      }
      else
      {
        this.directShowInterface.Stop();
      }
    }

    #endregion //PUBLICMETHODS

    ///////////////////////////////////////////////////////////////////////////////
    // Inherited methods                                                         //
    ///////////////////////////////////////////////////////////////////////////////
    #region OVERRIDES

    /// <summary>
    /// Initialize custom elements
    /// </summary>
    protected override void InitializeCustomElements()
    {
      base.InitializeCustomElements();
      this.btnHelp.Click += new EventHandler(this.btnHelp_Click);
      this.pnlCanvas.Resize += new EventHandler(this.pnlCanvas_Resize);
      this.videoFramePusher = new VideoFramePusher();
      this.videoFramePusher.VideoFrameAvailable += new BitmapEventHandler(this.videoFramePusher_VideoFrameAvailable);
    }

    /// <summary>
    /// Unregister custom events.
    /// </summary>
    protected override void CustomDispose()
    {
      if (this.videoFramePusher != null)
      {
        this.videoFramePusher.VideoFrameAvailable -= new BitmapEventHandler(this.videoFramePusher_VideoFrameAvailable);
        this.videoFramePusher.Dispose();
      }

      // DO not dispose the usercam, because its just a copy of the context panels UserCam.
      // this.usercamVideoPlayer.Dispose();
      if (this.directShowInterface != null)
      {
        this.directShowInterface.Stop();
        this.directShowInterface.Dispose();
        this.directShowInterface = null;
      }

      base.CustomDispose();
      this.cbbSpeed.ComboBox.SelectionChangeCommitted -= new EventHandler(this.cbbSpeed_SelectionChangeCommitted);
    }

    /// <summary>
    /// Initialize drop down controls.
    /// </summary>
    /// <remarks>The toolstrip combo box does currently not know the
    /// <see cref="ComboBox.SelectionChangeCommitted"/> event, so here we initialize it
    /// from the <see cref="ToolStripComboBox.ComboBox"/> member.</remarks>
    protected override void InitializeDropDowns()
    {
      base.InitializeDropDowns();
      this.cbbSpeed.ComboBox.SelectionChangeCommitted += new EventHandler(this.cbbSpeed_SelectionChangeCommitted);
      this.cbbNumFixationsToShow.Text = Properties.Settings.Default.MaxNumberFixations.ToString();
    }

    /// <summary>
    /// Loads images and data from database.
    /// Then updates picture with the changes.
    /// </summary>
    /// <returns><strong>True</strong> if trial could be successfully loaded,
    /// otherwise <strong>false</strong></returns>
    protected override bool NewTrialSelected()
    {
      try
      {
        // Stop if no trial is selected.
        if (!Document.ActiveDocument.SelectionState.IsSet)
        {
          return false;
        }

        // Read current selection state
        string subjectName = Document.ActiveDocument.SelectionState.SubjectName;
        int trialID = Document.ActiveDocument.SelectionState.TrialID;
        int trialSequence = Document.ActiveDocument.SelectionState.TrialSequence;

        // Switch to WaitCursor
        this.Cursor = Cursors.WaitCursor;

        // Read settings
        ExperimentSettings set = Document.ActiveDocument.ExperimentSettings;
        if (set != null)
        {
          int usercamID;

          this.LoadTrialEvents(subjectName, trialSequence, out usercamID);

          // Load the user camera
          this.LoadUsercam(subjectName, usercamID);

          // Load an video for the trial
          this.LoadTrialVideo(subjectName, trialSequence);

          // Load trial stimulus into picture
          if (!this.LoadTrialStimulus(trialID))
          {
            this.LoadSlide(SlideNotFoundSlide, ActiveXMode.Off);
          }

          // if video is available reset the slide
          if (this.isUsingTrialVideo)
          {
            this.Picture.ResetBackground();
          }

          // Load trial stimulus into picture
          this.LoadAudioStimuli(this.TrialEvents);

          // Load the whole trial data
          if (!this.LoadRawData(subjectName, trialSequence))
          {
            return false;
          }

          // Initializes usercam, video etc. with currently selected replay speed.
          this.SetupReplaySpeed();

          this.replayPicture.InitDrawingElements();
        }

        // Reset data state label
        ((MainForm)this.MdiParent).StatusRightLabel.Text = string.Empty;
      }
      catch (Exception ex)
      {
        ExceptionMethods.HandleException(ex);

        return false;
      }
      finally
      {
        // Reset Cursor
        Cursor = Cursors.Default;
      }

      return true;
    }

    /// <summary>
    /// Initializes accelerator keys: ESC
    /// </summary>
    protected override void InitAccelerators()
    {
      base.InitAccelerators();
      this.SetAccelerator(Keys.Escape, new AcceleratorAction(this.OnEscape));
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
    /// The <see cref="Form.FormClosing"/> event handler. 
    /// Updates the application settings with the current pen styles.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">The <see cref="FormClosingEventArgs"/> with the event data.</param>
    private void ReplayModule_FormClosing(object sender, FormClosingEventArgs e)
    {
      this.replayPicture.SaveStylesToApplicationSettings();
    }

    /// <summary>
    /// The <see cref="Form.Load"/> event handler. 
    /// Wires Mainform events and initializes vector graphics picture.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void ReplayModule_Load(object sender, EventArgs e)
    {
      try
      {
        // Intialize replay picture
        this.replayPicture.OwningForm = this;
        this.replayPicture.PresentationSize = Document.ActiveDocument.PresentationSize;

        // Read the current fixation calculation settings
        // from the experiment settings.
        this.ReadFixationCalculationSettings();

        this.ResizeCanvas();

        this.nudGazeFixDiameterDiv.Value = (decimal)Document.ActiveDocument.ExperimentSettings.GazeDiameterDiv;
        this.nudMouseFixDiameterDiv.Value = (decimal)Document.ActiveDocument.ExperimentSettings.MouseDiameterDiv;

        this.usercamVideoPlayer = ((MainForm)this.MdiParent).ContextPanel.AVPlayer;

        this.InitialDisplay();
      }
      catch (Exception ex)
      {
        ExceptionMethods.HandleException(ex);
      }
    }

    #region GazeStyleButtonsEventHandler
    /// <summary>
    /// The <see cref="Control.Click"/> event handler for the
    /// <see cref="Button"/> <see cref="btnGazeCursorTyp"/>.
    /// Shows gaze cursor selection dialog.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void btnGazeCursorTyp_Click(object sender, EventArgs e)
    {
      VGCursor backupShape = this.replayPicture.GazeCursor;
      CursorStyleDlg dlgGazeCursorTyp = new CursorStyleDlg();
      dlgGazeCursorTyp.CursorStyleChanged += new EventHandler<ShapeEventArgs>(this.dlgGazeCursorTyp_ShapeChanged);
      dlgGazeCursorTyp.Text = "Set gaze cursor shape...";
      dlgGazeCursorTyp.DrawingCursor = this.replayPicture.GazeCursor;
      dlgGazeCursorTyp.Icon = Properties.Resources.RPLCursorTypIcon;
      if (dlgGazeCursorTyp.ShowDialog() == DialogResult.Cancel)
      {
        ShapeEventArgs ea = new ShapeEventArgs(backupShape);
        ea.Shape.StyleGroup = VGStyleGroup.RPL_PEN_GAZE_CURSOR;
        this.replayPicture.DrawingCursorChanged(this, ea);
      }
    }

    /// <summary>
    /// The <see cref="VectorGraphics.Canvas.PictureModifiable.ShapeChanged"/> event handler for the
    /// <see cref="VectorGraphics.Canvas.PictureModifiable"/> <see cref="replayPicture"/>.
    /// Updates picture by invoking the pictures drawing cursor changed event handler.
    /// </summary>
    /// <param name="sender">Source of the event</param>
    /// <param name="e">A <see cref="ShapeEventArgs"/> with group and shape to switch.</param>
    private void dlgGazeCursorTyp_ShapeChanged(object sender, ShapeEventArgs e)
    {
      e.Shape.StyleGroup = VGStyleGroup.RPL_PEN_GAZE_CURSOR;
      this.replayPicture.DrawingCursorChanged(sender, e);
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler for the
    /// <see cref="Button"/> <see cref="btnGazeCursorPenStyle"/>.
    /// Shows pen style selection dialog for the gaze cursor.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void btnGazeCursorPenStyle_Click(object sender, EventArgs e)
    {
      Pen backupPen = this.replayPicture.PenGazeCursor;
      PenStyleDlg dlgGazeCursorStyle = new PenStyleDlg();
      dlgGazeCursorStyle.PenChanged += new EventHandler<PenChangedEventArgs>(this.dlgGazeCursorPenStyle_PenChanged);
      dlgGazeCursorStyle.Text = "Set gaze cursor pen style...";
      dlgGazeCursorStyle.Pen = this.replayPicture.PenGazeCursor;
      dlgGazeCursorStyle.Icon = Properties.Resources.RPLCursorPenIcon;
      if (dlgGazeCursorStyle.ShowDialog() == DialogResult.Cancel)
      {
        PenChangedEventArgs ea = new PenChangedEventArgs(backupPen, VGStyleGroup.RPL_PEN_GAZE_CURSOR);
        this.replayPicture.PenChanged(this, ea);
      }
    }

    /// <summary>
    /// The <see cref="OgamaControls.Dialogs.PenStyleDlg.PenChanged"/> 
    /// event handler for the <see cref="OgamaControls.Dialogs.PenStyleDlg"/>.
    /// Updates picture by invoking the pictures pen changed event handler.
    /// </summary>
    /// <param name="sender">Source of the event</param>
    /// <param name="e">A <see cref="PenChangedEventArgs"/> with group and pen to switch.</param>
    private void dlgGazeCursorPenStyle_PenChanged(object sender, PenChangedEventArgs e)
    {
      e.ElementGroup = VGStyleGroup.RPL_PEN_GAZE_CURSOR;
      this.replayPicture.PenChanged(sender, e);
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler for the
    /// <see cref="Button"/> <see cref="btnGazePathPenStyle"/>.
    /// Shows pen style selection dialog for the gaze path.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void btnGazePathPenStyle_Click(object sender, EventArgs e)
    {
      Pen backupPen = this.replayPicture.PenGazePath;
      PenStyleDlg dlgGazePathStyle = new PenStyleDlg();
      dlgGazePathStyle.Text = "Set gaze path pen style...";
      dlgGazePathStyle.Pen = this.replayPicture.PenGazePath;
      dlgGazePathStyle.Icon = Properties.Resources.RPLPathPenIcon;
      dlgGazePathStyle.PenChanged += new EventHandler<PenChangedEventArgs>(this.dlgGazePathStyle_PenChanged);
      if (dlgGazePathStyle.ShowDialog() == DialogResult.Cancel)
      {
        PenChangedEventArgs ea = new PenChangedEventArgs(backupPen, VGStyleGroup.RPL_PEN_GAZE_PATH);
        this.replayPicture.PenChanged(this, ea);
      }
    }

    /// <summary>
    /// The <see cref="OgamaControls.Dialogs.PenStyleDlg.PenChanged"/> 
    /// event handler for the <see cref="OgamaControls.Dialogs.PenStyleDlg"/>.
    /// Updates picture by invoking the pictures pen changed event handler.
    /// </summary>
    /// <param name="sender">Source of the event</param>
    /// <param name="e">A <see cref="PenChangedEventArgs"/> with group and pen to switch.</param>
    private void dlgGazePathStyle_PenChanged(object sender, PenChangedEventArgs e)
    {
      e.ElementGroup = VGStyleGroup.RPL_PEN_GAZE_PATH;
      this.replayPicture.PenChanged(sender, e);
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler for the
    /// <see cref="Button"/> <see cref="btnGazeFixPenStyle"/>.
    /// Shows pen style selection dialog for the gaze fixations.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void btnGazeFixPenStyle_Click(object sender, EventArgs e)
    {
      Pen backupPen = this.replayPicture.PenGazeFixation;
      PenStyleDlg dlgGazeFixStyle = new PenStyleDlg();
      dlgGazeFixStyle.PenChanged += new EventHandler<PenChangedEventArgs>(this.dlgGazeFixStyle_PenChanged);
      dlgGazeFixStyle.Text = "Set gaze fixations pen style...";
      dlgGazeFixStyle.Pen = this.replayPicture.PenGazeFixation;
      dlgGazeFixStyle.Icon = Properties.Resources.RPLFixPenIcon;
      if (dlgGazeFixStyle.ShowDialog() == DialogResult.Cancel)
      {
        PenChangedEventArgs ea = new PenChangedEventArgs(backupPen, VGStyleGroup.RPL_PEN_GAZE_FIX);
        this.replayPicture.PenChanged(this, ea);
      }
    }

    /// <summary>
    /// The <see cref="OgamaControls.Dialogs.PenStyleDlg.PenChanged"/> 
    /// event handler for the <see cref="OgamaControls.Dialogs.PenStyleDlg"/>.
    /// Updates picture by invoking the pictures pen changed event handler.
    /// </summary>
    /// <param name="sender">Source of the event</param>
    /// <param name="e">A <see cref="PenChangedEventArgs"/> with group and pen to switch.</param>
    private void dlgGazeFixStyle_PenChanged(object sender, PenChangedEventArgs e)
    {
      e.ElementGroup = VGStyleGroup.RPL_PEN_GAZE_FIX;
      this.replayPicture.PenChanged(sender, e);
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler for the
    /// <see cref="Button"/> <see cref="btnGazeFixConPenStyle"/>.
    /// Shows pen style selection dialog for the gaze fixation connections.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void btnGazeFixConPenStyle_Click(object sender, EventArgs e)
    {
      Pen backupPen = this.replayPicture.PenGazeFixationConnection;
      PenStyleDlg dlgGazeFixConStyle = new PenStyleDlg();
      dlgGazeFixConStyle.PenChanged += new EventHandler<PenChangedEventArgs>(this.dlgGazeFixConStyle_PenChanged);
      dlgGazeFixConStyle.Text = "Set gaze fixation connections pen style...";
      dlgGazeFixConStyle.Pen = this.replayPicture.PenGazeFixationConnection;
      dlgGazeFixConStyle.Icon = Properties.Resources.RPLFixConPenIcon;
      if (dlgGazeFixConStyle.ShowDialog() == DialogResult.Cancel)
      {
        PenChangedEventArgs ea = new PenChangedEventArgs(backupPen, VGStyleGroup.RPL_PEN_GAZE_FIXCON);
        this.replayPicture.PenChanged(this, ea);
      }
    }

    /// <summary>
    /// The <see cref="OgamaControls.Dialogs.PenStyleDlg.PenChanged"/> 
    /// event handler for the <see cref="OgamaControls.Dialogs.PenStyleDlg"/>.
    /// Updates picture by invoking the pictures pen changed event handler.
    /// </summary>
    /// <param name="sender">Source of the event</param>
    /// <param name="e">A <see cref="PenChangedEventArgs"/> with group and pen to switch.</param>
    private void dlgGazeFixConStyle_PenChanged(object sender, PenChangedEventArgs e)
    {
      e.ElementGroup = VGStyleGroup.RPL_PEN_GAZE_FIXCON;
      this.replayPicture.PenChanged(sender, e);
    }

    #endregion //GazeStyleButtonsEventHandler

    #region MouseStyleButtonsEventHandler
    /// <summary>
    /// The <see cref="Control.Click"/> event handler for the
    /// <see cref="Button"/> <see cref="btnMouseCursorTyp"/>.
    /// Shows mouse cursor selection dialog.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void btnMouseCursorTyp_Click(object sender, EventArgs e)
    {
      VGCursor backupShape = this.replayPicture.MouseCursor;
      CursorStyleDlg dlgMouseCursorTyp = new CursorStyleDlg();
      dlgMouseCursorTyp.CursorStyleChanged += new EventHandler<ShapeEventArgs>(this.dlgMouseCursorTyp_ShapeChanged);
      dlgMouseCursorTyp.Text = "Set mouse cursor shape...";
      dlgMouseCursorTyp.DrawingCursor = this.replayPicture.MouseCursor;
      dlgMouseCursorTyp.Icon = Properties.Resources.RPLCursorTypIcon;
      if (dlgMouseCursorTyp.ShowDialog() == DialogResult.Cancel)
      {
        ShapeEventArgs ea = new ShapeEventArgs(backupShape);
        ea.Shape.StyleGroup = VGStyleGroup.RPL_PEN_MOUSE_CURSOR;
        this.replayPicture.DrawingCursorChanged(this, ea);
      }
    }

    /// <summary>
    /// The <see cref="OgamaControls.Dialogs.PenStyleDlg.PenChanged"/> 
    /// event handler for the <see cref="OgamaControls.Dialogs.PenStyleDlg"/>.
    /// Updates picture by invoking the pictures pen changed event handler.
    /// </summary>
    /// <param name="sender">Source of the event</param>
    /// <param name="e">A <see cref="PenChangedEventArgs"/> with group and pen to switch.</param>
    private void dlgMouseCursorTyp_ShapeChanged(object sender, ShapeEventArgs e)
    {
      e.Shape.StyleGroup = VGStyleGroup.RPL_PEN_MOUSE_CURSOR;
      this.replayPicture.DrawingCursorChanged(sender, e);
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler for the
    /// <see cref="Button"/> <see cref="btnMouseCursorPenStyle"/>.
    /// Shows pen style selection dialog for the mouse cursor.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void btnMouseCursorPenStyle_Click(object sender, EventArgs e)
    {
      Pen backupPen = this.replayPicture.PenMouseCursor;
      PenStyleDlg dlgMouseCursorStyle = new PenStyleDlg();
      dlgMouseCursorStyle.PenChanged += new EventHandler<PenChangedEventArgs>(this.dlgMouseCursorPenStyle_PenChanged);
      dlgMouseCursorStyle.Text = "Set mouse cursor pen style...";
      dlgMouseCursorStyle.Pen = this.replayPicture.PenMouseCursor;
      dlgMouseCursorStyle.Icon = Properties.Resources.RPLCursorPenIcon;
      if (dlgMouseCursorStyle.ShowDialog() == DialogResult.Cancel)
      {
        PenChangedEventArgs ea = new PenChangedEventArgs(backupPen, VGStyleGroup.RPL_PEN_MOUSE_CURSOR);
        this.replayPicture.PenChanged(this, ea);
      }
    }

    /// <summary>
    /// The <see cref="OgamaControls.Dialogs.PenStyleDlg.PenChanged"/> 
    /// event handler for the <see cref="OgamaControls.Dialogs.PenStyleDlg"/>.
    /// Updates picture by invoking the pictures pen changed event handler.
    /// </summary>
    /// <param name="sender">Source of the event</param>
    /// <param name="e">A <see cref="PenChangedEventArgs"/> with group and pen to switch.</param>
    private void dlgMouseCursorPenStyle_PenChanged(object sender, PenChangedEventArgs e)
    {
      e.ElementGroup = VGStyleGroup.RPL_PEN_MOUSE_CURSOR;
      this.replayPicture.PenChanged(sender, e);
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler for the
    /// <see cref="Button"/> <see cref="btnMousePathPenStyle"/>.
    /// Shows pen style selection dialog for the mouse path.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void btnMousePathPenStyle_Click(object sender, EventArgs e)
    {
      Pen backupPen = this.replayPicture.PenMousePath;
      PenStyleDlg dlgMousePathStyle = new PenStyleDlg();
      dlgMousePathStyle.PenChanged += new EventHandler<PenChangedEventArgs>(this.dlgMousePathStyle_PenChanged);
      dlgMousePathStyle.Text = "Set mouse path pen style...";
      dlgMousePathStyle.Pen = this.replayPicture.PenMousePath;
      dlgMousePathStyle.Icon = Properties.Resources.RPLPathPenIcon;
      if (dlgMousePathStyle.ShowDialog() == DialogResult.Cancel)
      {
        PenChangedEventArgs ea = new PenChangedEventArgs(backupPen, VGStyleGroup.RPL_PEN_MOUSE_PATH);
        this.replayPicture.PenChanged(this, ea);
      }
    }

    /// <summary>
    /// The <see cref="OgamaControls.Dialogs.PenStyleDlg.PenChanged"/> 
    /// event handler for the <see cref="OgamaControls.Dialogs.PenStyleDlg"/>.
    /// Updates picture by invoking the pictures pen changed event handler.
    /// </summary>
    /// <param name="sender">Source of the event</param>
    /// <param name="e">A <see cref="PenChangedEventArgs"/> with group and pen to switch.</param>
    private void dlgMousePathStyle_PenChanged(object sender, PenChangedEventArgs e)
    {
      e.ElementGroup = VGStyleGroup.RPL_PEN_MOUSE_PATH;
      this.replayPicture.PenChanged(sender, e);
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler for the
    /// <see cref="Button"/> <see cref="btnMouseFixPenStyle"/>.
    /// Shows pen style selection dialog for the mouse fixations.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void btnMouseFixPenStyle_Click(object sender, EventArgs e)
    {
      Pen backupPen = this.replayPicture.PenMouseFixation;
      PenStyleDlg dlgMouseFixStyle = new PenStyleDlg();
      dlgMouseFixStyle.PenChanged += new EventHandler<PenChangedEventArgs>(this.dlgMouseFixStyle_PenChanged);
      dlgMouseFixStyle.Text = "Set mouse fixations pen style...";
      dlgMouseFixStyle.Pen = this.replayPicture.PenMouseFixation;
      dlgMouseFixStyle.Icon = Properties.Resources.RPLFixPenIcon;
      if (dlgMouseFixStyle.ShowDialog() == DialogResult.Cancel)
      {
        PenChangedEventArgs ea = new PenChangedEventArgs(backupPen, VGStyleGroup.RPL_PEN_MOUSE_FIX);
        this.replayPicture.PenChanged(this, ea);
      }
    }

    /// <summary>
    /// The <see cref="OgamaControls.Dialogs.PenStyleDlg.PenChanged"/> 
    /// event handler for the <see cref="OgamaControls.Dialogs.PenStyleDlg"/>.
    /// Updates picture by invoking the pictures pen changed event handler.
    /// </summary>
    /// <param name="sender">Source of the event</param>
    /// <param name="e">A <see cref="PenChangedEventArgs"/> with group and pen to switch.</param>
    private void dlgMouseFixStyle_PenChanged(object sender, PenChangedEventArgs e)
    {
      e.ElementGroup = VGStyleGroup.RPL_PEN_MOUSE_FIX;
      this.replayPicture.PenChanged(sender, e);
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler for the
    /// <see cref="Button"/> <see cref="btnMouseFixConPenStyle"/>.
    /// Shows pen style selection dialog for the mouse fixation connections.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void btnMouseFixConPenStyle_Click(object sender, EventArgs e)
    {
      Pen backupPen = this.replayPicture.PenMouseFixationConnection;
      PenStyleDlg dlgMouseFixConStyle = new PenStyleDlg();
      dlgMouseFixConStyle.PenChanged += new EventHandler<PenChangedEventArgs>(this.dlgMouseFixConStyle_PenChanged);
      dlgMouseFixConStyle.Text = "Set mouse fixation connections pen style...";
      dlgMouseFixConStyle.Pen = this.replayPicture.PenMouseFixationConnection;
      dlgMouseFixConStyle.Icon = Properties.Resources.RPLFixConPenIcon;
      if (dlgMouseFixConStyle.ShowDialog() == DialogResult.Cancel)
      {
        PenChangedEventArgs ea = new PenChangedEventArgs(backupPen, VGStyleGroup.RPL_PEN_MOUSE_FIXCON);
        this.replayPicture.PenChanged(this, ea);
      }
    }

    /// <summary>
    /// The <see cref="OgamaControls.Dialogs.PenStyleDlg.PenChanged"/> 
    /// event handler for the <see cref="OgamaControls.Dialogs.PenStyleDlg"/>.
    /// Updates picture by invoking the pictures pen changed event handler.
    /// </summary>
    /// <param name="sender">Source of the event</param>
    /// <param name="e">A <see cref="PenChangedEventArgs"/> with group and pen to switch.</param>
    private void dlgMouseFixConStyle_PenChanged(object sender, PenChangedEventArgs e)
    {
      e.ElementGroup = VGStyleGroup.RPL_PEN_MOUSE_FIXCON;
      this.replayPicture.PenChanged(sender, e);
    }

    #endregion //MouseStyleButtonsEventHandler

    #region GazeModeButtonsEventHandler
    /// <summary>
    /// The <see cref="Control.Click"/> event handler for the
    /// <see cref="Button"/> <see cref="btnGazeModeCursor"/>.
    /// Sets or resets pictures <see cref="ReplayPicture.GazeDrawingMode"/> 
    /// <see cref="ReplayDrawingModes.Cursor"/>
    /// flag depending on buttons <see cref="ToolStripButton.Checked"/> state.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void btnGazeModeCursor_Click(object sender, EventArgs e)
    {
      if (this.btnGazeModeCursor.Checked)
      {
        this.replayPicture.GazeDrawingMode |= ReplayDrawingModes.Cursor;
      }
      else
      {
        this.replayPicture.GazeDrawingMode &= ~ReplayDrawingModes.Cursor;
      }

      this.RedrawPicture();
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler for the
    /// <see cref="Button"/> <see cref="btnGazeModePath"/>.
    /// Sets or resets pictures <see cref="ReplayPicture.GazeDrawingMode"/> 
    /// <see cref="ReplayDrawingModes.Path"/>
    /// flag depending on buttons <see cref="ToolStripButton.Checked"/> state.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void btnGazeModePath_Click(object sender, EventArgs e)
    {
      if (this.btnGazeModePath.Checked)
      {
        this.replayPicture.GazeDrawingMode |= ReplayDrawingModes.Path;
      }
      else
      {
        this.replayPicture.GazeDrawingMode &= ~ReplayDrawingModes.Path;
      }

      this.RedrawPicture();
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler for the
    /// <see cref="Button"/> <see cref="btnGazeModeFix"/>.
    /// Sets or resets pictures <see cref="ReplayPicture.GazeDrawingMode"/> 
    /// <see cref="ReplayDrawingModes.Fixations"/>
    /// flag depending on buttons <see cref="ToolStripButton.Checked"/> state.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void btnGazeModeFix_Click(object sender, EventArgs e)
    {
      if (this.btnGazeModeFix.Checked)
      {
        this.replayPicture.GazeDrawingMode |= ReplayDrawingModes.Fixations;
      }
      else
      {
        this.replayPicture.GazeDrawingMode &= ~ReplayDrawingModes.Fixations;
      }

      this.RedrawPicture();
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler for the
    /// <see cref="Button"/> <see cref="btnGazeModeFixCon"/>.
    /// Sets or resets pictures <see cref="ReplayPicture.GazeDrawingMode"/> 
    /// <see cref="ReplayDrawingModes.FixationConnections"/>
    /// flag depending on buttons <see cref="ToolStripButton.Checked"/> state.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void btnGazeModeFixCon_Click(object sender, EventArgs e)
    {
      if (this.btnGazeModeFixCon.Checked)
      {
        this.replayPicture.GazeDrawingMode |= ReplayDrawingModes.FixationConnections;
      }
      else
      {
        this.replayPicture.GazeDrawingMode &= ~ReplayDrawingModes.FixationConnections;
      }

      this.RedrawPicture();
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler for the
    /// <see cref="Button"/> <see cref="btnGazeModeSpot"/>.
    /// Sets or resets pictures <see cref="ReplayPicture.GazeDrawingMode"/> 
    /// <see cref="ReplayDrawingModes.Spotlight"/>
    /// flag depending on buttons <see cref="ToolStripButton.Checked"/> state.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void btnGazeModeSpot_Click(object sender, EventArgs e)
    {
      if (this.btnGazeModeSpot.Checked)
      {
        this.replayPicture.GazeDrawingMode |= ReplayDrawingModes.Spotlight;
      }
      else
      {
        this.replayPicture.GazeDrawingMode &= ~ReplayDrawingModes.Spotlight;
      }

      this.RedrawPicture();
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler for the
    /// <see cref="Button"/> <see cref="btnGazeCutPath"/>.
    /// Sets or resets pictures <see cref="ReplayPicture.GazeDiscreteLength"/> 
    /// flag depending on buttons <see cref="ToolStripButton.Checked"/> state.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void btnGazeCutPath_Click(object sender, EventArgs e)
    {
      this.replayPicture.GazeDiscreteLength = this.btnGazeCutPath.Checked;
      this.RedrawPicture();
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler for the
    /// <see cref="Button"/> <see cref="btnGazeBlinks"/>.
    /// Sets or resets pictures <see cref="ReplayPicture.Blinks"/> 
    /// flag depending on buttons <see cref="ToolStripButton.Checked"/> state.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void btnGazeBlinks_Click(object sender, EventArgs e)
    {
      this.replayPicture.Blinks = this.btnGazeBlinks.Checked;
    }

    #endregion //GazeModeButtonsEventHandler

    #region MouseModeButtonsEventHandler

    /// <summary>
    /// The <see cref="Control.Click"/> event handler for the
    /// <see cref="Button"/> <see cref="btnMouseModeCursor"/>.
    /// Sets or resets pictures <see cref="ReplayPicture.MouseDrawingMode"/> 
    /// <see cref="ReplayDrawingModes.Cursor"/>
    /// flag depending on buttons <see cref="ToolStripButton.Checked"/> state.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void btnMouseModeCursor_Click(object sender, EventArgs e)
    {
      if (this.btnMouseModeCursor.Checked)
      {
        this.replayPicture.MouseDrawingMode |= ReplayDrawingModes.Cursor;
      }
      else
      {
        this.replayPicture.MouseDrawingMode &= ~ReplayDrawingModes.Cursor;
      }

      this.RedrawPicture();
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler for the
    /// <see cref="Button"/> <see cref="btnMouseModePath"/>.
    /// Sets or resets pictures <see cref="ReplayPicture.MouseDrawingMode"/> 
    /// <see cref="ReplayDrawingModes.Path"/>
    /// flag depending on buttons <see cref="ToolStripButton.Checked"/> state.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void btnMouseModePath_Click(object sender, EventArgs e)
    {
      if (this.btnMouseModePath.Checked)
      {
        this.replayPicture.MouseDrawingMode |= ReplayDrawingModes.Path;
      }
      else
      {
        this.replayPicture.MouseDrawingMode &= ~ReplayDrawingModes.Path;
      }

      this.RedrawPicture();
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler for the
    /// <see cref="Button"/> <see cref="btnMouseModeFix"/>.
    /// Sets or resets pictures <see cref="ReplayPicture.MouseDrawingMode"/> 
    /// <see cref="ReplayDrawingModes.Fixations"/>
    /// flag depending on buttons <see cref="ToolStripButton.Checked"/> state.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void btnMouseModeFix_Click(object sender, EventArgs e)
    {
      if (this.btnMouseModeFix.Checked)
      {
        this.replayPicture.MouseDrawingMode |= ReplayDrawingModes.Fixations;
      }
      else
      {
        this.replayPicture.MouseDrawingMode &= ~ReplayDrawingModes.Fixations;
      }

      this.RedrawPicture();
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler for the
    /// <see cref="Button"/> <see cref="btnMouseModeFixCon"/>.
    /// Sets or resets pictures <see cref="ReplayPicture.MouseDrawingMode"/> 
    /// <see cref="ReplayDrawingModes.FixationConnections"/>
    /// flag depending on buttons <see cref="ToolStripButton.Checked"/> state.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void btnMouseModeFixCon_Click(object sender, EventArgs e)
    {
      if (this.btnMouseModeFixCon.Checked)
      {
        this.replayPicture.MouseDrawingMode |= ReplayDrawingModes.FixationConnections;
      }
      else
      {
        this.replayPicture.MouseDrawingMode &= ~ReplayDrawingModes.FixationConnections;
      }

      this.RedrawPicture();
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler for the
    /// <see cref="Button"/> <see cref="btnMouseModeSpot"/>.
    /// Sets or resets pictures <see cref="ReplayPicture.MouseDrawingMode"/> 
    /// <see cref="ReplayDrawingModes.Spotlight"/>
    /// flag depending on buttons <see cref="ToolStripButton.Checked"/> state.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void btnMouseModeSpot_Click(object sender, EventArgs e)
    {
      if (this.btnMouseModeSpot.Checked)
      {
        this.replayPicture.MouseDrawingMode |= ReplayDrawingModes.Spotlight;
      }
      else
      {
        this.replayPicture.MouseDrawingMode &= ~ReplayDrawingModes.Spotlight;
      }

      this.RedrawPicture();
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler for the
    /// <see cref="Button"/> <see cref="btnMouseModeClicks"/>.
    /// Sets or resets pictures <see cref="ReplayPicture.MouseDrawingMode"/> 
    /// <see cref="ReplayDrawingModes.Clicks"/>
    /// flag depending on buttons <see cref="ToolStripButton.Checked"/> state.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void btnMouseModeClicks_Click(object sender, EventArgs e)
    {
      if (this.btnMouseModeClicks.Checked)
      {
        this.replayPicture.MouseDrawingMode |= ReplayDrawingModes.Clicks;
      }
      else
      {
        this.replayPicture.MouseDrawingMode &= ~ReplayDrawingModes.Clicks;
      }

      this.RedrawPicture();
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler for the
    /// <see cref="Button"/> <see cref="btnMouseCutPath"/>.
    /// Sets or resets pictures <see cref="ReplayPicture.MouseDiscreteLength"/>
    /// flag depending on buttons <see cref="ToolStripButton.Checked"/> state.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void btnMouseCutPath_Click(object sender, EventArgs e)
    {
      this.replayPicture.MouseDiscreteLength = this.btnMouseCutPath.Checked;
      this.RedrawPicture();
    }

    #endregion //MouseModeButtonsEventHandler

    /// <summary>
    /// The <see cref="Control.Click"/> event handler for the
    /// <see cref="Button"/> <see cref="btnShowVisiblePartOfScreen"/>.
    /// Shows or hides the rectangle indicating the visible part of the screen.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void btnShowVisiblePartOfScreen_Click(object sender, EventArgs e)
    {
      this.replayPicture.ShowVisiblePartOfScreen = this.btnShowVisiblePartOfScreen.Checked;
      this.RedrawPicture();
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler for the
    /// <see cref="Button"/> <see cref="btnShowUsercam"/>.
    /// Shows or hides the user cam in the context panel.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void btnShowUsercam_Click(object sender, EventArgs e)
    {
      ((MainForm)this.MdiParent).ContextPanel.IsUsercamVisible = this.btnShowUsercam.Checked;
      if (!this.btnShowUsercam.Checked)
      {
        // Close user video
        this.usercamVideoPlayer.CloseMovie();
      }
      else
      {
        // Show user video if exists.
        this.SetVideoStartTime(Document.ActiveDocument.SelectionState.SubjectName, this.userVideoStartTime);
      }
    }

    /// <summary>
    /// The <see cref="ComboBox.SelectedIndexChanged"/> event handler for the
    /// <see cref="ComboBox"/> <see cref="cbbNumFixationsToShow"/>.
    /// User selected new number of fixations to show, when
    /// replay interface is in reduce data mode, so update pictures 
    /// <see cref="ReplayPicture.NumFixToShow"/> member.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void cbbNumFixationsToShow_SelectedIndexChanged(object sender, EventArgs e)
    {
      try
      {
        int numFixToShow = Convert.ToInt32(this.cbbNumFixationsToShow.Text);
        Properties.Settings.Default.MaxNumberFixations = numFixToShow;
        this.replayPicture.NumFixToShow = numFixToShow;
        this.RedrawPicture();
      }
      catch (Exception ex)
      {
        ExceptionMethods.HandleException(ex);
      }
    }

    /// <summary>
    /// The <see cref="ComboBox.SelectionChangeCommitted"/> event handler for the
    /// <see cref="ComboBox"/> <see cref="cbbTrial"/>.
    /// User selected new playing speed, so update pictures 
    /// <see cref="ReplayPicture.Speed"/> member.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void cbbSpeed_SelectionChangeCommitted(object sender, EventArgs e)
    {
      this.SetupReplaySpeed();
    }

    /// <summary>
    /// The <see cref="NumericUpDown.ValueChanged"/> event handler for the
    /// <see cref="NumericUpDown"/> <see cref="nudGazeFixDiameterDiv"/>.
    /// User selected new divider for gaze fixation diameters,
    /// so set the <see cref="ReplayPicture.GazeFixationsDiameterDivisior"/> property.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void nudGazeFixDivameterDiv_ValueChanged(object sender, EventArgs e)
    {
      this.replayPicture.GazeFixationsDiameterDivisior = (float)this.nudGazeFixDiameterDiv.Value;
      Document.ActiveDocument.ExperimentSettings.GazeDiameterDiv = (float)this.nudGazeFixDiameterDiv.Value;
      this.RedrawPicture();
    }

    /// <summary>
    /// The <see cref="NumericUpDown.ValueChanged"/> event handler for the
    /// <see cref="NumericUpDown"/> <see cref="nudGazeFixDiameterDiv"/>.
    /// User selected new divider for mouse fixation diameters,
    /// so set the <see cref="ReplayPicture.MouseFixationsDiameterDivisor"/> property.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void nudMouseFixDiameterDiv_ValueChanged(object sender, EventArgs e)
    {
      this.replayPicture.MouseFixationsDiameterDivisor = (float)this.nudMouseFixDiameterDiv.Value;
      Document.ActiveDocument.ExperimentSettings.MouseDiameterDiv = (float)this.nudMouseFixDiameterDiv.Value;
      this.RedrawPicture();
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler for the
    /// <see cref="Button"/> <see cref="btnStart"/>.
    /// User pressed Playbutton, so start picture animation, and modify UI
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void btnStart_Click(object sender, EventArgs e)
    {
      // Read the current fixation calculation settings
      // from the experiment settings.
      this.ReadFixationCalculationSettings();

      if (this.trialTimeLine.CurrentTime > this.trialTimeLine.SectionEndTime - 15)
      {
        this.ResetControls();
      }

      // Paranoia check for empty trials.
      if (this.trialTimeLine.SectionEndTime == this.trialTimeLine.SectionStartTime)
      {
        return;
      }

      if (this.replayPicture.InitDrawingElements())
      {
        // Disable the ComboBoxes control until 
        // the replay operation is done.
        this.cbbSubject.Enabled = false;
        this.cbbTrial.Enabled = false;

        // Disable the Start and rewind button
        this.btnStart.Enabled = false;
        this.btnStartContinous.Enabled = false;
        this.btnRewind.Enabled = false;

        // Enable the Cancel button while 
        // the operation runs.
        this.btnStop.Enabled = true;
        this.btnPause.Enabled = true;

        // Update pictures properties
        this.replayPicture.ReplayPosition = this.currentTrialTime;

        // Start the timer updating operation.
        this.replayPicture.StartAnimation();

        if (this.isUsingTrialVideo)
        {
          this.videoFramePusher.Start();
          // AsyncHelper.FireAndForget(new MethodInvoker(this.videoFramePusher.Start));
        }

        // Start sound
        if (this.btnEnableAudio.Checked)
        {
          if (this.Player != null)
          {
            this.Player.Play();
          }
        }

        // Update the video positions and start replay.
        if (this.btnShowUsercam.Checked && ((MainForm)this.MdiParent).ContextPanel.Visible)
        {
          AsyncHelper.FireAndForget(new MethodInvoker(this.usercamVideoPlayer.PlayMovie));
        }
      }
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler for the
    /// <see cref="Button"/> <see cref="btnStartContinous"/>.
    /// User pressed Playbutton for continuous mode, 
    /// so start picture animation, modify UI and call new trials if
    /// trial ended.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void btnStartContinous_Click(object sender, EventArgs e)
    {
      // Set continuous mode
      this.isInContinuousMode = true;

      // Populate the picture with the whole subjects raw data...
      this.ReadWholeSubjectsData();

      // Read the current fixation calculation settings
      // from the experiment settings.
      this.ReadFixationCalculationSettings();

      if (this.trialTimeLine.CurrentTime > this.trialTimeLine.SectionEndTime - 15)
      {
        this.ResetControls();
      }

      // Paranoia check for empty trials.
      if (this.trialTimeLine.SectionEndTime == this.trialTimeLine.SectionStartTime)
      {
        return;
      }

      // Disable the ComboBoxes control until 
      // the replay operation is done.
      this.cbbSubject.Enabled = false;
      this.cbbTrial.Enabled = false;

      // Disable the Start and rewind button
      this.btnStart.Enabled = false;
      this.btnStartContinous.Enabled = false;
      this.btnRewind.Enabled = false;

      // Enable the Cancel button while 
      // the operation runs.
      this.btnStop.Enabled = true;
      this.btnPause.Enabled = true;

      // Update pictures properties
      this.replayPicture.ReplayPosition = this.currentTrialTime;

      // Start the timer updating operation.
      this.replayPicture.StartAnimation();

      if (this.isUsingTrialVideo)
      {
        this.videoFramePusher.Start();
        //         AsyncHelper.FireAndForget(new MethodInvoker(this.videoFramePusher.Start));
      }

      // Start sound
      if (this.btnEnableAudio.Checked)
      {
        if (this.Player != null)
        {
          this.Player.Play();
        }
      }

      // Update the video positions and start replay.
      if (this.btnShowUsercam.Checked && ((MainForm)this.MdiParent).ContextPanel.Visible)
      {
        AsyncHelper.FireAndForget(new MethodInvoker(this.usercamVideoPlayer.PlayMovie));
      }
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler for the
    /// <see cref="Button"/> <see cref="btnPause"/>.
    /// User pressed pause button, so halt or restart picture animation.
    /// <remarks>Updates pictures <see cref="ReplayPicture.PauseTime"/>
    /// and <see cref="ReplayPicture.PauseTimeStart"/> timing values.</remarks>
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void btnPause_Click(object sender, EventArgs e)
    {
      if (this.btnPause.Checked)
      {
        this.replayPicture.StopAnimation();
        this.replayPicture.PauseTimeStart = DateTime.Now;
      }
      else
      {
        this.replayPicture.PauseTime += DateTime.Now - this.replayPicture.PauseTimeStart;
        this.replayPicture.StartAnimation();
      }

      if (this.btnShowUsercam.Checked)
      {
        this.usercamVideoPlayer.PauseMovie();
      }

      if (this.isUsingTrialVideo)
      {
        this.videoFramePusher.Pause();
      }

      if (this.btnEnableAudio.Checked)
      {
        if (this.Player != null)
        {
          this.Player.Pause();
        }
      }
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler for the
    /// <see cref="Button"/> <see cref="btnStop"/>.
    /// User pressed stop button. So raise <see cref="StopPlaying()"/> method.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void btnStop_Click(object sender, EventArgs e)
    {
      this.StopPlaying();
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler for the
    /// <see cref="Button"/> <see cref="btnRewind"/>.
    /// User pressed rewind button, so reset user interface.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void btnRewind_Click(object sender, EventArgs e)
    {
      this.ResetControls();
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler for the
    /// <see cref="Button"/> <see cref="btnSeekToNextEvent"/>.
    /// User pressed seek to next event button, so stop playing,
    /// seek audio, video and picture to next event position.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void btnSeekToNextEvent_Click(object sender, EventArgs e)
    {
      this.trialTimeLine.MoveToNextTimeLineEvent(true);
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler for the
    /// <see cref="Button"/> <see cref="btnSeekToPreviousEvent"/>.
    /// User pressed seek to previous event button, so stop playing,
    /// seek audio, video and picture to previous event position.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void btnSeekToPreviousEvent_Click(object sender, EventArgs e)
    {
      this.trialTimeLine.MoveToNextTimeLineEvent(false);
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler for the
    /// <see cref="Button"/> <see cref="btnSeekToNextMarker"/>.
    /// User pressed seek to next marker button, so stop playing,
    /// seek audio, video and picture to next marker position.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void btnSeekToNextMarker_Click(object sender, EventArgs e)
    {
      this.trialTimeLine.MoveToNextMarker(true);
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler for the
    /// <see cref="Button"/> <see cref="btnSeekToPreviousMarker"/>.
    /// User pressed seek to previous marker button, so stop playing,
    /// seek audio, video and picture to previous marker position.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void btnSeekToPreviousMarker_Click(object sender, EventArgs e)
    {
      this.trialTimeLine.MoveToNextMarker(false);
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler for the
    /// <see cref="Button"/> <see cref="btnAddMarker"/>.
    /// User pressed add marker event button,
    /// so add a trial event at the current time.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void btnAddMarker_Click(object sender, EventArgs e)
    {
      // Skip if there is no data
      if (Document.ActiveDocument.SelectionState.SubjectName == null)
      {
        return;
      }

      long time = this.currentTrialTime;

      MediaEvent markerEvent = new MediaEvent();
      int? maxID = Document.ActiveDocument.DocDataSet.TrialEventsAdapter.GetMaxEventID();
      markerEvent.EventID = maxID.HasValue ? maxID.Value + 1 : 0;
      markerEvent.Param = string.Empty;
      markerEvent.SubjectName = Document.ActiveDocument.SelectionState.SubjectName;
      markerEvent.Task = MediaEventTask.None;
      markerEvent.Time = time;
      markerEvent.TrialSequence = Document.ActiveDocument.SelectionState.TrialSequence;
      markerEvent.Type = EventType.Marker;

      // Insert Data in TrialEvents Table
      SQLiteOgamaDataSet.TrialEventsRow workTrialEventRow;
      workTrialEventRow = Document.ActiveDocument.DocDataSet.TrialEvents.NewTrialEventsRow();
      workTrialEventRow.EventID = markerEvent.EventID;
      workTrialEventRow.EventParam = markerEvent.Param;
      workTrialEventRow.SubjectName = markerEvent.SubjectName;
      workTrialEventRow.EventTime = markerEvent.Time;
      workTrialEventRow.TrialSequence = markerEvent.TrialSequence;
      workTrialEventRow.EventTask = markerEvent.Task.ToString();
      workTrialEventRow.EventType = markerEvent.Type.ToString();
      Document.ActiveDocument.DocDataSet.TrialEvents.AddTrialEventsRow(workTrialEventRow);

      // Update Database
      int affectedRows =
        Document.ActiveDocument.DocDataSet.TrialEventsAdapter.Update(Document.ActiveDocument.DocDataSet.TrialEvents);

      this.TrialEvents.Add(markerEvent.EventID, markerEvent);
      this.trialTimeLine.TrialEvents = this.TrialEvents;
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler for the
    /// <see cref="Button"/> <see cref="btnRecord"/>.
    /// User pressed record button, so start the video recording
    /// by asking for filename, then asking for the video size
    /// and compression and start rendering to frames.
    /// </summary>
    /// <remarks>This video calculation is done in a separate
    /// thread using a directshow interface. 
    /// During export the replay picture is prevented from beeing
    /// repainted, because the export uses the same video resources.</remarks>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void btnRecord_Click(object sender, EventArgs e)
    {
      this.Record();
    }

    #endregion //WINDOWSEVENTHANDLER

    ///////////////////////////////////////////////////////////////////////////////
    // Eventhandler for Custom Defined Events                                    //
    ///////////////////////////////////////////////////////////////////////////////
    #region CUSTOMEVENTHANDLER

    /// <summary>
    /// The event handler for the completed event of the DES class.
    /// </summary>
    /// <param name="sender">Source of the event</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void desCombine_Completed(object sender, EventArgs e)
    {
      try
      {
        if (this.InvokeRequired)
        {
          this.Invoke(new MethodInvoker(this.RenderGazeAndUserVideo));
        }
        else
        {
          this.RenderGazeAndUserVideo();
        }
      }
      catch (Exception ex)
      {
        ExceptionMethods.HandleException(ex);
      }
    }

    /// <summary>
    /// The event handler for the Progress event of the image handler.
    /// Notifies the splash with the current progress percentage.
    /// </summary>
    /// <param name="sender">Source of the event</param>
    /// <param name="e">A <see cref="ProgressEventArgs"/> with the progress percentage.</param>
    private void imageHandler_Progress(object sender, ProgressEventArgs e)
    {
      this.newSplash.ProgressPercentage = e.ProgressInPercent;
    }

    /// <summary>
    /// The <see cref="TimeLine.CaretValueChanged"/> event handler
    /// for the <see cref="TrialTimeLine"/> <see cref="trialTimeLine"/>.
    /// User moved the caret in the time slider to a new position,
    /// so render all graphic content up to the new millisecond time.
    /// </summary>
    /// <param name="sender">Source of the event</param>
    /// <param name="e">A <see cref="TimeLine.PositionValueChangedEventArguments"/> 
    /// with the new millisecond.</param>
    private void TimeSlider_CaretValueChanged(
      object sender,
      TimeLine.PositionValueChangedEventArguments e)
    {
      this.replayPicture.RenderTimeRangeInMS(this.currentTrialTime, e.Millisecond - 1);
      this.replayPicture.DrawForeground(false);
      this.currentTrialTime = e.Millisecond;

      if (this.btnShowUsercam.Checked)
      {
        this.usercamVideoPlayer.SeekMovie(this.userVideoStartTime + e.Millisecond, null);
      }

      if (this.isUsingTrialVideo)
      {
        this.videoFramePusher.SeekMovie(e.Millisecond);
      }

      if (this.btnEnableAudio.Checked)
      {
        if (this.Player != null)
        {
          this.Player.Seek(e.Millisecond);
        }
      }

      this.Refresh();
    }

    /// <summary>
    /// The <see cref="TimeLine.CaretMovingStarted"/> event handler for the 
    /// <see cref="TrialTimeLine"/> <see cref="TimeLine"/>
    /// Sets the flag indicating that flash remote control should be stopped
    /// during moving and stops the replay.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void TimeSlider_CaretMovingStarted(object sender, EventArgs e)
    {
      this.StopPlaying();
    }

    /// <summary>
    /// The <see cref="TimeLine.MarkerPositionChanged"/> event handler for the 
    /// <see cref="TrialTimeLine"/> <see cref="TimeLine"/>
    /// Updates the trial events member of this class
    /// and the database entry with the new time.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An <see cref="TimeLine.MarkerPositionChangedEventArguments"/></param>
    private void TimeSlider_MarkerPositionChanged(object sender, TimeLine.MarkerPositionChangedEventArguments e)
    {
      if (this.TrialEvents.ContainsKey(e.MarkerEventID))
      {
        this.TrialEvents[e.MarkerEventID].Time = e.NewTime;
        this.TrialEvents[e.MarkerEventID].Param = "Modified";
        this.UpdateMarkerPositionsInDatabase();
      }
    }

    /// <summary>
    /// The <see cref="TimeLine.MarkerDeleted"/> event handler for the 
    /// <see cref="TrialTimeLine"/> <see cref="TimeLine"/>
    /// Deletes the given marker form the list and the database.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An <see cref="TimeLine.MarkerPositionChangedEventArguments"/></param>
    private void TimeSlider_MarkerDeleted(object sender, TimeLine.MarkerPositionChangedEventArguments e)
    {
      if (this.TrialEvents.ContainsKey(e.MarkerEventID))
      {
        Queries.DeleteMarkerEventByID(this.TrialEvents[e.MarkerEventID]);
        this.TrialEvents.Remove(e.MarkerEventID);
      }
    }

    /// <summary>
    /// The <see cref="VectorGraphics.Canvas.Picture.Progress"/> event handler for
    /// the <see cref="ReplayPicture"/>.
    /// This method updates time slider caret and stops
    /// the animation if the trial has finished.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">A <see cref="ProgressEventArgs"/>with time and status</param>
    private void replayPicture_Progress(object sender, ProgressEventArgs e)
    {
      // Check for end of trial.
      if (e.Finished || e.Millisecond >= this.trialTimeLine.SectionEndTime)
      {
        this.StopPlaying();
        this.trialTimeLine.CurrentTime = this.trialTimeLine.SectionEndTime;
      }
      else if (e.Millisecond != 0)
      {
        this.trialTimeLine.CurrentTime = e.Millisecond;
        this.currentTrialTime = e.Millisecond;
      }
    }

    /// <summary>
    /// The <see cref="ReplayPicture.TrialEventIDFound"/> event handler for
    /// the <see cref="VectorGraphics.Canvas.Picture"/> <see cref="replayPicture"/>.
    /// Updates the picture with the occured event if applicable.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">A <see cref="TrialEventIDFoundEventArgs"/> with the event data.</param>
    private void replayPicture_TrialEventIDFound(object sender, TrialEventIDFoundEventArgs e)
    {
      try
      {
        if (!this.TrialEvents.ContainsKey(e.EventID))
        {
          return;
        }

        TrialEvent occuredEvent = this.TrialEvents[e.EventID];
        string parameter = occuredEvent.Param;
        switch (occuredEvent.Type)
        {
          case EventType.Webpage:
            break;
          case EventType.Response:
            break;
          case EventType.Mouse:
            break;
          case EventType.Key:
            break;
          case EventType.Slide:
            int indexOfHash = parameter.IndexOf('#');
            int slideCounter = Convert.ToInt32(parameter.Substring(0, indexOfHash));
            Slide newSlide = this.CurrentTrial[slideCounter];
            if (this.replayPicture.BgSlide != newSlide)
            {
              this.LoadSlide(newSlide, ActiveXMode.Off);
            }

            break;
          case EventType.Flash:
            break;
          case EventType.Audio:
            break;
          case EventType.Video:
            break;
          case EventType.Usercam:
            break;

          case EventType.Scroll:
            // Update scroll position
            string[] scrollOffsets = occuredEvent.Param.Split(';');
            Point newScrollPosition = new Point(
              Convert.ToInt32(scrollOffsets[0]),
              Convert.ToInt32(scrollOffsets[1]));

            this.ThreadSafeSetAutoScrollPosition(newScrollPosition);

            break;
        }
      }
      catch (Exception ex)
      {
        ExceptionMethods.HandleExceptionSilent(ex);
      }
    }

    /// <summary>
    /// The <see cref="ReplayPicture.TrialSequenceChanged"/> event handler for
    /// the <see cref="VectorGraphics.Canvas.Picture"/> <see cref="replayPicture"/>.
    /// Updates the picture with the new trials slide, if we are in continous mode,
    /// that means replaying the whole experiment.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">A <see cref="TrialSequenceChangedEventArgs"/> with the event data.</param>
    private void replayPicture_TrialSequenceChanged(object sender, TrialSequenceChangedEventArgs e)
    {
      // Load new trial stimulus during replay if we are in continous mode
      if (this.isInContinuousMode)
      {
        string subjectName = Document.ActiveDocument.SelectionState.SubjectName;

        this.StopTrialVideoAndTrialAudio();

        // Load an video for the trial
        this.LoadTrialVideo(subjectName, e.TrialSequence);

        // Get the trial id for the current subject and sequence
        int trialID = Queries.GetTrialIDForSequence(subjectName, e.TrialSequence);

        // Load trial stimulus into picture
        if (!this.LoadTrialStimulus(trialID))
        {
          this.Picture.ResetBackground();
          this.LoadSlide(SlideNotFoundSlide, ActiveXMode.Off);
        }

        // if video is available reset the slide
        if (this.isUsingTrialVideo)
        {
          this.Picture.ResetBackground();
          this.videoFramePusher.Start();
          //           AsyncHelper.FireAndForget(new MethodInvoker(this.videoFramePusher.Start));
        }

        // Load trial stimulus into picture
        this.LoadAudioStimuli(this.FilterTrialEvents(this.TrialEvents, e.TrialSequence));

        // Start sound
        if (this.btnEnableAudio.Checked)
        {
          if (this.Player != null)
          {
            this.Player.Play();
          }
        }
      }
    }

    /// <summary>
    /// The <see cref="OgamaControls.VideoFramePusher.VideoFrameAvailable"/> event handler.
    /// Updates the pictures background with the new video frame.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">A <see cref="BitmapEventArgs"/> with the new bitmap.</param>
    private void videoFramePusher_VideoFrameAvailable(object sender, BitmapEventArgs e)
    {
      this.Picture.BackgroundImage = e.Bitmap;
    }

    #endregion //CUSTOMEVENTHANDLER

    #endregion //EVENTS

    ///////////////////////////////////////////////////////////////////////////////
    // Methods and Eventhandling for Background tasks                            //
    ///////////////////////////////////////////////////////////////////////////////
    #region BACKGROUNDWORKER

    /// <summary>
    /// The DoWork event handler for the
    /// <see cref="Thread"/> VideoThread
    /// Background worker thread working method for calculating
    /// the video frames according to replay settings.
    /// </summary>
    /// <param name="data">An <see cref="object"/> with the threads parameters.</param>
    private void VideoThread_DoWork(object data)
    {
      this.Record();
    }

    /// <summary>
    /// The <see cref="DSRecord.Completed"/> event handler for the video
    /// export.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/>.</param>
    private void directShowInterface_Completed(object sender, EventArgs e)
    {
      try
      {
        if (this.directShowInterface != null)
        {
          this.directShowInterface.Completed -= this.directShowInterface_Completed;
          this.directShowInterface.Dispose();
          this.directShowInterface = null;
        }

        this.ShowVideoExportResult();
      }
      catch (Exception ex)
      {
        ExceptionMethods.HandleException(ex);
      }
    }

    /// <summary>
    /// The <see cref="DSRecordWithDMO.Completed"/> event handler for the video
    /// export.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/>.</param>
    private void directShowInterfaceWithWebcam_Completed(object sender, EventArgs e)
    {
      if (this.directShowInterfaceWithWebcam != null)
      {
        this.directShowInterfaceWithWebcam.Completed -= this.directShowInterfaceWithWebcam_Completed;
        this.directShowInterfaceWithWebcam.Dispose();
        this.directShowInterfaceWithWebcam = null;
      }

      if (this.desCombine != null)
      {
        this.desCombine.Dispose();
      }

      this.ShowVideoExportResult();
    }

    #endregion //BACKGROUNDWORKER

    ///////////////////////////////////////////////////////////////////////////////
    // Methods for doing main class job                                          //
    ///////////////////////////////////////////////////////////////////////////////
    #region METHODS

    /// <summary>
    /// This methods loads the whole raw data for the current subject
    /// to display a smooth replay for the whole experiment only updating
    /// the trial slides.
    /// </summary>
    /// <returns><strong>True</strong> if data could be successfully loaded,
    /// otherwise <strong>false</strong></returns>
    private bool ReadWholeSubjectsData()
    {
      try
      {
        // Stop if no trial is selected.
        if (!Document.ActiveDocument.SelectionState.IsSet)
        {
          return false;
        }

        // Read current selection state
        string subjectName = Document.ActiveDocument.SelectionState.SubjectName;
        int trialID = Document.ActiveDocument.SelectionState.TrialID;
        int trialSequence = Document.ActiveDocument.SelectionState.TrialSequence;

        // Switch to WaitCursor
        this.Cursor = Cursors.WaitCursor;

        // Read settings
        ExperimentSettings set = Document.ActiveDocument.ExperimentSettings;
        if (set != null)
        {
          int usercamID;

          this.LoadTrialEvents(subjectName, out usercamID);

          // Load the user camera
          this.LoadUsercam(subjectName, usercamID);

          // Load an video for the trial
          this.LoadTrialVideo(subjectName, trialSequence);

          // Load trial stimulus into picture
          if (!this.LoadTrialStimulus(trialID))
          {
            this.Picture.ResetBackground();
            this.LoadSlide(SlideNotFoundSlide, ActiveXMode.Off);
          }

          // if video is available reset the slide
          if (this.isUsingTrialVideo)
          {
            this.Picture.ResetBackground();
          }

          // Load trial stimulus into picture
          this.LoadAudioStimuli(this.TrialEvents);

          // Load the whole trial data
          if (!this.LoadRawData(subjectName))
          {
            return false;
          }

          // Initializes usercam, video etc. with currently selected replay speed.
          this.SetupReplaySpeed();

          this.replayPicture.InitDrawingElements();
        }

        // Reset data state label
        ((MainForm)this.MdiParent).StatusRightLabel.Text = string.Empty;
      }
      catch (Exception ex)
      {
        ExceptionMethods.HandleException(ex);
        return false;
      }
      finally
      {
        // Reset Cursor
        Cursor = Cursors.Default;
      }

      return true;
    }

    /// <summary>
    /// This method is called when the video export has finished an pops up
    /// the media player with the newly created video and 
    /// reinitializtes the picture of the module.
    /// </summary>
    private void ShowVideoExportResult()
    {
      try
      {
        this.replayPicture.EndUpdate();
        ThreadSafe.Close(this.newSplash);
        this.videoFramePusher.SeekMovie(0);

        // Update pictures properties
        if (this.InvokeRequired)
        {
          this.Invoke(new MethodInvoker(this.ResetControls));
        }
        else
        {
          this.ResetControls();
        }

        // Run video
        if (File.Exists(this.videoExportProperties.OutputVideoProperties.Filename))
        {
          System.Diagnostics.Process.Start(this.videoExportProperties.OutputVideoProperties.Filename);
        }
      }
      catch (Exception ex)
      {
        ExceptionMethods.HandleException(ex);
      }
    }

    /// <summary>
    /// This method cuts the given user video to the
    /// appropriate length by copying the selected part to
    /// a new temporary file with the new output properties
    /// </summary>
    /// <param name="videoExportProperties">The <see cref="VideoExportProperties"/>
    /// to use during export.</param>
    private void CreatePartialUserVideo(VideoExportProperties videoExportProperties)
    {
      this.newSplash.IsPreviewVisible = false;
      this.newSplash.Header = "Preparing User Video";

      string userVideoFile = this.videoExportProperties.UserVideoProperties.StreamFilename;

      MediaInfo videoHeader = new MediaInfo();
      videoHeader.Open(userVideoFile);
      bool hasVideo = videoHeader.Get(StreamKind.Video, 0, "ID") != string.Empty;
      bool hasAudio = videoHeader.Get(StreamKind.Audio, 0, "ID") != string.Empty;
      videoHeader.Close();

      // Init DESCombine with audio and/or video support depending on
      // what the user video contains.
      // audio is "", if there is no audio stream
      this.desCombine = new DESCombine(
        this.videoExportProperties.OutputVideoProperties.FrameRate,
        32,
        this.videoExportProperties.UserVideoProperties.StreamSize.Width,
        this.videoExportProperties.UserVideoProperties.StreamSize.Height,
        hasAudio,
        hasVideo);

      if (hasVideo && hasAudio)
      {
        this.desCombine.AddAVFile(
          this.videoExportProperties.UserVideoProperties.StreamFilename,
          this.videoExportProperties.UserVideoProperties.StreamStartTime * 10000,
          this.videoExportProperties.UserVideoProperties.StreamEndTime * 10000);
      }
      else if (hasVideo)
      {
        this.desCombine.AddVideoFile(
          this.videoExportProperties.UserVideoProperties.StreamFilename,
          this.videoExportProperties.UserVideoProperties.StreamStartTime * 10000,
          this.videoExportProperties.UserVideoProperties.StreamEndTime * 10000);
      }
      else if (hasAudio)
      {
        this.desCombine.AddAudioFile(
          this.videoExportProperties.UserVideoProperties.StreamFilename,
          this.videoExportProperties.UserVideoProperties.StreamStartTime * 10000,
          this.videoExportProperties.UserVideoProperties.StreamEndTime * 10000);
      }

      IBaseFilter videoCompressor = null;
      if (this.videoExportProperties.OutputVideoProperties.VideoCompressor != string.Empty)
      {
        // Create the filter for the selected video compressor
        videoCompressor = DirectShowUtils.CreateFilter(
          FilterCategory.VideoCompressorCategory,
          this.videoExportProperties.OutputVideoProperties.VideoCompressor);
      }

      IBaseFilter audioCompressor = null;
      if (this.videoExportProperties.OutputVideoProperties.AudioCompressor != string.Empty)
      {
        // Create the filter for the selected video compressor
        audioCompressor = DirectShowUtils.CreateFilter(
          FilterCategory.AudioCompressorCategory,
          this.videoExportProperties.OutputVideoProperties.AudioCompressor);
      }

      this.desCombine.RenderToAVI(
        this.videoExportProperties.UserVideoTempFile,
        videoCompressor,
        audioCompressor,
        null,
        null);

      this.desCombine.Completed += new EventHandler(this.desCombine_Completed);
      this.desCombine.StartRendering();
    }

    /// <summary>
    /// This method renders the gaze and truncated user video
    /// with the mixer dmo into a new video file.
    /// </summary>
    private void RenderGazeAndUserVideo()
    {
      this.newSplash.Header = "Rendering gaze and user video";
      this.newSplash.IsPreviewVisible = true;

      ImageFromVectorGraphics imageHandler =
        new ImageFromVectorGraphics(
        this.videoExportProperties.OutputVideoProperties,
        this.videoExportProperties.GazeVideoProperties.StreamStartTime,
        this.videoExportProperties.GazeVideoProperties.StreamEndTime,
        this.videoFramePusher,
        this.replayPicture.RenderFrame);
      imageHandler.Progress += this.imageHandler_Progress;
      imageHandler.Finished += this.imageHandler_Finished;

      // Setup usercam video filename.
      this.directShowInterfaceWithWebcam = new DSRecordWithDMO(
        imageHandler,
        this.videoExportProperties,
        this.newSplash.PreviewWindow);

      this.directShowInterfaceWithWebcam.Completed += this.directShowInterfaceWithWebcam_Completed;

      this.replayPicture.ResetPicture();

      if (this.replayPicture.InitDrawingElements())
      {
        // Update pictures properties
        this.replayPicture.ReplayPosition = this.currentTrialTime;
        this.replayPicture.BeginUpdate();

        this.Cursor = Cursors.Default;

        this.directShowInterfaceWithWebcam.Start();
      }
    }

    /// <summary>
    /// This method renders the picture content into an avi file.
    /// </summary>
    private void RenderGazeVideo()
    {
      this.newSplash.Header = "Rendering gaze and mouse video";
      this.newSplash.IsPreviewVisible = true;

      var imageHandler =
        new ImageFromVectorGraphics(
        this.videoExportProperties.OutputVideoProperties,
        this.videoExportProperties.GazeVideoProperties.StreamStartTime,
        this.videoExportProperties.GazeVideoProperties.StreamEndTime,
        this.videoFramePusher,
        this.replayPicture.RenderFrame);
      imageHandler.Progress += this.imageHandler_Progress;
      imageHandler.Finished += this.imageHandler_Finished;
      // Setup usercam video filename.
      this.directShowInterface = new DSRecord(
        imageHandler,
        this.videoExportProperties,
        this.newSplash.PreviewWindow);

      this.directShowInterface.Completed += this.directShowInterface_Completed;

      this.replayPicture.ResetPicture();

      if (this.replayPicture.InitDrawingElements())
      {
        // Update pictures properties
        this.replayPicture.ReplayPosition = this.currentTrialTime;
        this.replayPicture.BeginUpdate();

        this.Cursor = Cursors.Default;

        this.directShowInterface.Start();
      }
    }

    /// <summary>
    /// This event handler ensures the correct close and disposal of
    /// the video export once the image handler has no data anymore.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void imageHandler_Finished(object sender, EventArgs e)
    {
      this.OnEscape();
    }

    /// <summary>
    /// This method sets the currently selected replay speed to
    /// the picture, usercam, replayvideo and audioplayer.
    /// </summary>
    private void SetupReplaySpeed()
    {
      int speedValue = this.cbbSpeed.SelectedIndex;
      float speed = 1;
      switch (speedValue)
      {
        case 0: speed = 0.1f;
          break;
        case 1: speed = 0.2f;
          break;
        case 2: speed = 0.33f;
          break;
        case 3: speed = 0.5f;
          break;
        case 4: speed = 1f;
          break;
        case 5: speed = 2f;
          break;
        case 6: speed = 3f;
          break;
        case 7: speed = 5f;
          break;
        case 8: speed = 10f;
          break;
        default: speed = 1f;
          break;
      }

      bool successful = true;

      try
      {
        if (this.btnShowUsercam.Checked)
        {
          this.usercamVideoPlayer.PlaybackRate = speed;
        }

        if (this.isUsingTrialVideo)
        {
          this.videoFramePusher.PlaybackRate = speed;
        }

        if (this.btnEnableAudio.Checked)
        {
          if (this.Player != null)
          {
            this.Player.PlaybackRate = speed;
          }
        }
      }
      catch (System.Runtime.InteropServices.COMException ex)
      {
        ExceptionMethods.ProcessErrorMessage("The playback rate could not be changed. " +
          Environment.NewLine + "The following exception occured: "
          + Environment.NewLine + ex.Message
          + Environment.NewLine +
          "Try to disable the audio stream of the video player in the context panel, by using its context menu and disable the audio playback of this interface by clicking on the sound button. Unfortunately most of the audio stream formats don´t support other replay speed than 1.");

        this.cbbSpeed.SelectedIndex = 4;
        successful = false;
      }

      if (successful)
      {
        this.replayPicture.Speed = speed;
      }
    }

    /// <summary>
    /// This method loads the trial events from the database into the timeline.
    /// </summary>
    /// <param name="subjectName">A <see cref="String"/> with the subject name.</param>
    /// <param name="trialSequence">An <see cref="Int32"/> with the trial sequence number</param>
    /// <param name="usercamID">Out. An <see cref="Int32"/> with an optional usercamID event.</param>
    private void LoadTrialEvents(string subjectName, int trialSequence, out int usercamID)
    {
      // Get the events for the current trial indexed by type
      this.TrialEvents = Queries.GetTrialEvents(
        subjectName,
        trialSequence,
        out usercamID);

      // Update time line with events
      this.trialTimeLine.TrialEvents = this.TrialEvents;
    }

    /// <summary>
    /// This method loads the trial events from the database into the timeline
    /// for the whole subjects data.
    /// </summary>
    /// <param name="subjectName">A <see cref="String"/> with the subject name.</param>
    /// <param name="usercamID">Out. An <see cref="Int32"/> with an optional usercamID event.</param>
    private void LoadTrialEvents(string subjectName, out int usercamID)
    {
      // Get the events for the current trial indexed by type
      this.TrialEvents = Queries.GetTrialEvents(
        subjectName,
        out usercamID);

      // Update time line with events
      this.trialTimeLine.TrialEvents = this.TrialEvents;
    }

    /// <summary>
    /// This method asks the user for a video filename and raises
    /// a <see cref="VideoPropertiesDialog"/> to define the video
    /// export properties.
    /// </summary>
    /// <param name="videoExportProperties">Out. A <see cref="VideoExportProperties"/>
    /// that is generated in this method.</param>
    /// <returns><strong>True</strong> if the user selected valid properties,
    /// otherwise <strong>false</strong>.</returns>
    private bool GetVideoProperties(out VideoExportProperties videoExportProperties)
    {
      videoExportProperties = new VideoExportProperties();

      if (this.sfdVideo.ShowDialog() == DialogResult.OK)
      {
        videoExportProperties.UserVideoTempFile = Path.GetTempFileName();

        videoExportProperties.UserVideoProperties.StreamFilename = this.usercamVideoPlayer.MovieFile;
        videoExportProperties.UserVideoProperties.StreamSize = this.usercamVideoPlayer.VideoSize;
        videoExportProperties.UserVideoProperties.StreamScreenshot =
          this.usercamVideoPlayer.Screenshot;
        videoExportProperties.UserVideoProperties.StreamName = "Uservideo";
        videoExportProperties.UserVideoProperties.StreamStartTime = this.userVideoStartTime + this.trialTimeLine.SectionStartTime;
        videoExportProperties.UserVideoProperties.StreamEndTime = this.userVideoStartTime + this.trialTimeLine.SectionEndTime;

        videoExportProperties.GazeVideoProperties.StreamScreenshot =
          (Bitmap)this.Picture.RenderToImage();
        videoExportProperties.GazeVideoProperties.StreamName = "Gaze video";
        videoExportProperties.GazeVideoProperties.StreamStartTime = this.trialTimeLine.SectionStartTime;
        videoExportProperties.GazeVideoProperties.StreamEndTime = this.trialTimeLine.SectionEndTime;
        videoExportProperties.GazeVideoProperties.StreamSize = videoExportProperties.GazeVideoProperties.StreamScreenshot.Size;

        VideoPropertiesDialog videoPropertiesDlg = new VideoPropertiesDialog();
        videoPropertiesDlg.VideoExportProperties = videoExportProperties;

        if (videoPropertiesDlg.ShowDialog() == DialogResult.OK)
        {
          videoExportProperties = videoPropertiesDlg.VideoExportProperties;

          // Save filename in member.
          videoExportProperties.OutputVideoProperties.Filename = this.sfdVideo.FileName;
          return true;
        }
      }

      return false;
    }

    /// <summary>
    /// This method redraws the <see cref="ReplayPicture"/> with the updated
    /// UI settings.
    /// </summary>
    private void RedrawPicture()
    {
      this.replayPicture.ResetPicture();

      // if (this.currentTrialTime > 0)
      if (this.replayPicture.ReplayDataTable != null)
      {
        this.replayPicture.InitDrawingElements();
        this.replayPicture.RenderUpToGivenTime(this.currentTrialTime);
        this.replayPicture.DrawForeground(false);
      }
    }

    /// <summary>
    /// This method loads the raw data for the given subject and trial 
    /// given by sequence and submits it to the underlying picture.
    /// </summary>
    /// <param name="subjectName">A <see cref="String"/> with the subject name.</param>
    /// <param name="trialSequence">An <see cref="Int32"/> with the trial sequence to 
    /// be selected.</param>
    /// <returns><strong>True</strong> if raw data could be successfully found and loaded,
    /// otherwise <strong>false</strong>.</returns>
    private bool LoadRawData(string subjectName, int trialSequence)
    {
      // Load raw data
      DataTable rawDataTable = null;
      DataTable trialTable = null;

      trialTable = Document.ActiveDocument.DocDataSet.TrialsAdapter.GetDataBySubjectAndSequence(subjectName, trialSequence);

      // Only if there is one trial with the matching conditions
      if (trialTable != null && trialTable.Rows.Count == 1)
      {
        DataRow row = trialTable.Rows[0];
        int duration = (int)row["Duration"];

        // Import raw Data
        rawDataTable = Queries.GetRawDataBySubjectAndTrialSequence(subjectName, trialSequence);

        // Update Replayslider
        this.trialTimeLine.Duration = duration;
        this.currentTrialTime = 0;
        this.trialTimeLine.ResetTimeLine();
      }
      else
      {
        ExceptionMethods.ProcessErrorMessage("The current selection seems not to be valid." +
        Environment.NewLine + "Please try again by selecting a trial from the drop down combo box." +
        "If this error occurs twice, there are maybe two trials with the same ID " + Environment.NewLine +
        "in the database, which is not allowed, try to correct it there.");
      }

      if (rawDataTable != null)
      {
        this.replayPicture.ReplayDataTable = rawDataTable;
        return true;
      }

      return false;
    }

    /// <summary>
    /// This method loads the raw data for the given subject 
    /// and submits it to the underlying picture.
    /// </summary>
    /// <param name="subjectName">A <see cref="String"/> with the subject name.</param>
    /// <returns><strong>True</strong> if raw data could be successfully found and loaded,
    /// otherwise <strong>false</strong>.</returns>
    private bool LoadRawData(string subjectName)
    {
      // Load raw data
      DataTable rawDataTable = Queries.GetRawDataBySubject(subjectName);

      if (rawDataTable.Rows.Count == 0)
      {
        return false;
      }

      long firstSamplesTime = Convert.ToInt64(rawDataTable.Rows[0]["Time"]);
      long lastSamplesTime = Convert.ToInt64(rawDataTable.Rows[rawDataTable.Rows.Count - 1]["Time"]);
      int duration = (int)(lastSamplesTime - firstSamplesTime);

      // Update Replayslider
      this.trialTimeLine.Duration = duration;
      this.currentTrialTime = 0;
      this.trialTimeLine.ResetTimeLine();

      if (rawDataTable != null)
      {
        this.replayPicture.ReplayDataTable = rawDataTable;
        return true;
      }

      return false;
    }

    /// <summary>
    /// This method loads the user camera video into the viewer,
    /// if there is any and seeks it to the start tine of the current trial.
    /// </summary>
    /// <param name="subjectName">A <see cref="String"/> with the subjects name.</param>
    /// <param name="trialSequence">An <see cref="Int32"/> with the trial sequence.</param>
    /// <returns>Always <strong>true</strong>.</returns>
    private bool LoadTrialVideo(string subjectName, int trialSequence)
    {
      string videoFile = Path.Combine(
        Document.ActiveDocument.ExperimentSettings.ThumbsPath,
        subjectName + "-" + trialSequence.ToString() + ".avi");

      if (File.Exists(videoFile))
      {
        // Trial video available
        this.videoFramePusher.LoadMovie(videoFile);
        this.videoFramePusher.SeekMovie(0);
        this.isUsingTrialVideo = true;
      }
      else
      {
        // No video available
        this.isUsingTrialVideo = false;
      }

      return true;
    }

    /// <summary>
    /// This method loads the user camera video into the viewer,
    /// if there is any and seeks it to the start tine of the current trial.
    /// </summary>
    /// <param name="subjectName">A <see cref="String"/> with the subjects name.</param>
    /// <param name="usercamID">An <see cref="Int32"/> with the event id 
    /// of the usercamera start event.</param>
    /// <returns>Always <strong>true</strong>.</returns>
    private bool LoadUsercam(string subjectName, int usercamID)
    {
      if (usercamID == -1 )//|| !this.btnShowUsercam.Checked)
      {
        // No usercam available
        this.usercamVideoPlayer.CloseMovie();
        ((MainForm)this.MdiParent).ContextPanel.IsUsercamVisible = false;
        this.btnShowUsercam.Checked = false;

        return true;
      }

      TrialEvent cameraEvent = this.TrialEvents[usercamID];
      this.userVideoStartTime = Convert.ToInt32(cameraEvent.Param);
      this.SetVideoStartTime(subjectName, this.userVideoStartTime);
      return true;
    }

    /// <summary>
    /// This method loads an user camera video into the player and
    /// seeks to its start time.
    /// </summary>
    /// <param name="subject">A <see cref="String"/> with the subjects name.</param>
    /// <param name="videoStartTime">An <see cref="Int64"/> with the start time
    /// of the current trial in video time units.</param>
    private void SetVideoStartTime(string subject, long videoStartTime)
    {
      string videoFile = Path.Combine(Document.ActiveDocument.ExperimentSettings.ThumbsPath, subject + ".avi");
      if (File.Exists(videoFile))
      {
        ((MainForm)this.MdiParent).ContextPanel.IsUsercamVisible = true;
        if (this.usercamVideoPlayer.MovieFile != videoFile)
        {
          this.usercamVideoPlayer.LoadMovie(videoFile);
        }

        this.usercamVideoPlayer.SeekMovie(videoStartTime, null);
        this.btnShowUsercam.Checked = true;
      }
    }

    /// <summary>
    /// Stop picture animation and reset UI
    /// </summary>
    private void StopPlaying()
    {
      // Reset continuous mode
      this.isInContinuousMode = false;

      // Stop update timer
      this.replayPicture.StopAnimation();

      if (this.btnShowUsercam.Checked && this.usercamVideoPlayer.PlayState == OgamaControls.PlayState.Running)
      {
        this.usercamVideoPlayer.StopMovie();
      }

      this.StopTrialVideoAndTrialAudio();

      // Enable the ComboBoxes
      this.cbbSubject.Enabled = true;
      this.cbbTrial.Enabled = true;

      // Activate and deactivate buttons
      this.btnStart.Enabled = true;
      this.btnStartContinous.Enabled = true;
      this.btnStop.Enabled = false;
      this.btnRewind.Enabled = true;
      this.btnPause.Enabled = false;
      this.btnPause.Checked = false;
    }

    /// <summary>
    /// Stops current trial video and audio player.
    /// </summary>
    private void StopTrialVideoAndTrialAudio()
    {
      if (this.isUsingTrialVideo)
      {
        this.videoFramePusher.Stop();
      }

      if (this.btnEnableAudio.Checked)
      {
        if (this.Player != null)
        {
          this.Player.Stop();
        }
      }
    }

    /// <summary>
    /// This method reads the fixation calculation settings 
    /// from the current <see cref="ExperimentSettings"/>
    /// and updates the <see cref="ReplayPicture"/>.
    /// </summary>
    private void ReadFixationCalculationSettings()
    {
      this.replayPicture.InitializeSettings();
    }

    /// <summary>
    /// Resets UI to start conditions by enabling start button and resetting picture.
    /// </summary>
    private void ResetControls()
    {
      this.StopPlaying();

      // Skip if there is no data
      if (this.CurrentTrial == null)
      {
        return;
      }

      // Load first slide of multiple slides on trial
      if (this.CurrentTrial.Count > 1)
      {
        Slide newSlide = this.CurrentTrial[0];
        if (this.replayPicture.BgSlide != newSlide)
        {
          this.LoadSlide(newSlide, ActiveXMode.Off);
        }
      }

      ((MainForm)this.MdiParent).StatusLabel.Text = "Ready";
      ((MainForm)this.MdiParent).StatusProgressbar.Value = 0;
      ((MainForm)this.MdiParent).StatusRightLabel.Text = string.Empty;

      this.trialTimeLine.ResetTimeLine();
      this.currentTrialTime = this.trialTimeLine.CurrentTime;

      if (this.btnShowUsercam.Checked)
      {
        this.SetVideoStartTime(this.cbbSubject.Text, this.userVideoStartTime);
      }
      else
      {
        this.RemoveUserVideo();
      }

      if (this.isUsingTrialVideo)
      {
        this.videoFramePusher.SeekMovie(0);
      }

      if (this.btnEnableAudio.Checked)
      {
        if (this.Player != null)
        {
          this.Player.Seek(0);
        }
      }

      this.replayPicture.ResetPicture();
      this.replayPicture.InitDrawingElements();
    }

    /// <summary>
    /// This method unloads the usercamera and hides the control.
    /// </summary>
    private void RemoveUserVideo()
    {
      ((MainForm)this.MdiParent).ContextPanel.IsUsercamVisible = false;
      this.usercamVideoPlayer.CloseMovie();
      this.btnShowUsercam.Checked = false;
    }

    #endregion //METHODS

    ///////////////////////////////////////////////////////////////////////////////
    // Small helping Methods                                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region HELPER

    /// <summary>
    /// This method updates the time value of the modified marker in 
    /// the trial events table of the database.
    /// </summary>
    private void UpdateMarkerPositionsInDatabase()
    {
      foreach (TrialEvent trialEvent in this.TrialEvents.Values)
      {
        if (trialEvent.Type != EventType.Marker)
        {
          continue;
        }

        if (trialEvent.Param == "Modified")
        {
          trialEvent.Param = string.Empty;
          Queries.UpdateMarkerEventByID(trialEvent);
        }
      }
    }

    /// <summary>
    /// Filters the list of trial events to return only the events
    /// occured in the given trial sequence of the current subject.
    /// </summary>
    /// <param name="sortedList">A list of all trial events of the whole experiment
    /// for this subject.</param>
    /// <param name="trialSequence">The trial sequence filter.</param>
    /// <returns>The filtered list of trial events.</returns>
    private SortedList<int, TrialEvent> FilterTrialEvents(
      SortedList<int, TrialEvent> sortedList,
      int trialSequence)
    {
      SortedList<int, TrialEvent> returnList = new SortedList<int, TrialEvent>();
      foreach (KeyValuePair<int, TrialEvent> trialEvent in sortedList)
      {
        if (trialEvent.Value.TrialSequence == trialSequence)
        {
          returnList.Add(trialEvent.Value.EventID, trialEvent.Value);
        }
      }

      return returnList;
    }

    #endregion //HELPER
  }
}