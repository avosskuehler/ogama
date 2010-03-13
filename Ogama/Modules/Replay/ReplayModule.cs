// <copyright file="ReplayModule.cs" company="FU Berlin">
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

namespace Ogama.Modules.Replay
{
  using System;
  using System.Collections.Generic;
  using System.ComponentModel;
  using System.Data;
  using System.Drawing;
  using System.Drawing.Drawing2D;
  using System.Drawing.Imaging;
  using System.IO;
  using System.Runtime.InteropServices;
  using System.Text;
  using System.Threading;
  using System.Windows.Forms;

  using DirectX.Capture;
  using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;

  using Ogama.ExceptionHandling;
  using Ogama.MainWindow;
  using Ogama.Modules.Common;
  using Ogama.Modules.SlideshowDesign;
  using Ogama.Properties;
  using OgamaControls;
  using OgamaControls.Dialogs;

  using VectorGraphics.CustomEventArgs;
  using VectorGraphics.Elements;
  using VectorGraphics.StopConditions;

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
    /// This member save the current viewed time of the trial 
    /// in milliseconds.
    /// </summary>
    private int currentTrialTime;

    /// <summary>
    /// This member saves the current trials video start time.
    /// </summary>
    private long currentVideoStartTime;

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
    /// The filename for the video export.
    /// </summary>
    private string fileName;

    /// <summary>
    /// A <see cref="CaptureDeviceProperties"/> with the video export properties
    /// </summary>
    private CaptureDeviceProperties videoProperties;

    /// <summary>
    /// A <see cref="DSRecord"/> used to create the avi movie via direct show.
    /// </summary>
    private DSRecord directShowInterface;

    /// <summary>
    /// The video export splash window with preview.
    /// </summary>
    private SaveVideoSplash newSplash;

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
    /// <param name="sectionStartTime">A <see cref="Int64"/> with the 
    /// section starting time (beginning of export).</param>
    /// <param name="sectionEndTime">A <see cref="Int64"/> with the 
    /// section ending time (end of export).</param>
    public void Record(long sectionStartTime, long sectionEndTime)
    {
      if (!this.GetVideoProperties())
      {
        return;
      }

      this.Cursor = Cursors.WaitCursor;

      // If we have no DirectShow class open
      if (this.directShowInterface == null)
      {
        try
        {
          // Create and show splash
          this.newSplash = new SaveVideoSplash();
          this.newSplash.Show();

          ImageFromVectorGraphics imageHandler =
            new ImageFromVectorGraphics(
            this.videoProperties,
            sectionStartTime,
            sectionEndTime,
            this.videoFramePusher,
            this.replayPicture.RenderFrame);
          this.directShowInterface = new DSRecord(imageHandler, this.fileName, this.videoProperties, this.newSplash.PreviewWindow);
          this.directShowInterface.Completed += new EventHandler(this.directShowInterface_Completed);

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
        this.directShowInterface.Stop();
      }

      this.Cursor = Cursors.Default;
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
    /// <returns><strong>True</strong> if trial could be succesfully loaded,
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

          // Load trial stimulus into picture, if no video is available
          if (!this.isUsingTrialVideo)
          {
            if (!this.LoadTrialStimulus(trialID))
            {
              this.Picture.ResetBackground();
              this.LoadSlide(SlideNotFoundSlide, ActiveXMode.Off);
            }
          }
          else
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
        bool rethrow = ExceptionPolicy.HandleException(ex, "Global Policy");
        if (rethrow)
        {
          throw;
        }

        return false;
      }
      finally
      {
        // Reset Cursor
        Cursor = Cursors.Default;
      }

      return true;
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

        this.ResizePicture();

        this.nudGazeFixDiameterDiv.Value = (decimal)Document.ActiveDocument.ExperimentSettings.GazeDiameterDiv;
        this.nudMouseFixDiameterDiv.Value = (decimal)Document.ActiveDocument.ExperimentSettings.MouseDiameterDiv;

        this.usercamVideoPlayer = ((MainForm)this.MdiParent).ContextPanel.AVPlayer;

        this.InitialDisplay();
      }
      catch (Exception ex)
      {
        bool rethrow = ExceptionPolicy.HandleException(ex, "Global Policy");
        if (rethrow)
        {
          throw;
        }
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
        this.SetVideoStartTime(Document.ActiveDocument.SelectionState.SubjectName, this.currentVideoStartTime);
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
        bool rethrow = ExceptionPolicy.HandleException(ex, "Global Policy");
        if (rethrow)
        {
          throw;
        }
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
          AsyncHelper.FireAndForget(new MethodInvoker(this.videoFramePusher.Start));
        }

        // Start sound
        if (this.btnEnableAudio.Checked)
        {
          this.Player.Play();
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
        this.Player.Pause();
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
      Ogama.DataSet.OgamaDataSet.TrialEventsRow workTrialEventRow;
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
      this.Record(this.trialTimeLine.SectionStartTime, this.trialTimeLine.SectionEndTime);
    }

    #endregion //WINDOWSEVENTHANDLER

    ///////////////////////////////////////////////////////////////////////////////
    // Eventhandler for Custom Defined Events                                    //
    ///////////////////////////////////////////////////////////////////////////////
    #region CUSTOMEVENTHANDLER

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
        this.usercamVideoPlayer.SeekMovie(this.currentVideoStartTime + e.Millisecond, null);
      }

      if (this.isUsingTrialVideo)
      {
        this.videoFramePusher.SeekMovie(e.Millisecond);
      }

      if (this.btnEnableAudio.Checked)
      {
        this.Player.Seek(e.Millisecond);
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
        TrialEvent occuredEvent = this.TrialEvents[e.EventID];
        string parameter = occuredEvent.Param;
        switch (occuredEvent.Type)
        {
          case EventType.Mouse:
            break;
          case EventType.Key:
            break;
          case EventType.Slide:
            int indexOfHash = parameter.IndexOf('#');
            int slideCounter = Convert.ToInt32(parameter.Substring(0, indexOfHash));
            Slide newSlide = this.CurrentTrial[slideCounter];
            if (this.replayPicture.BGSlide != newSlide)
            {
              this.LoadSlide(newSlide, ActiveXMode.Video);
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
          case EventType.Response:
            break;
        }
      }
      catch (Exception ex)
      {
        bool rethrow = ExceptionPolicy.HandleException(ex, "Log Only Policy");
        if (rethrow)
        {
          throw;
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
      this.Record(this.trialTimeLine.SectionStartTime, this.trialTimeLine.SectionEndTime);
    }

    /// <summary>
    /// The <see cref="DSRecord.Completed"/> event handler for the video
    /// export.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/>.</param>
    private void directShowInterface_Completed(object sender, EventArgs e)
    {
      if (this.directShowInterface != null)
      {
        this.directShowInterface.Completed -= new EventHandler(this.directShowInterface_Completed);
        this.directShowInterface.Dispose();
        this.directShowInterface = null;
      }

      this.replayPicture.EndUpdate();
      ThreadSafe.Close(this.newSplash);
      this.videoFramePusher.SeekMovie(0);

      string message = "The video export has been completed succesfully.";
      ExceptionMethods.ProcessMessage("Export finished.", message);
    }

    #endregion //BACKGROUNDWORKER

    ///////////////////////////////////////////////////////////////////////////////
    // Methods for doing main class job                                          //
    ///////////////////////////////////////////////////////////////////////////////
    #region METHODS

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

      bool succesful = true;

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
          this.Player.PlaybackRate = speed;
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
        succesful = false;
      }

      if (succesful)
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
    /// This method asks the user for a video filename and raises
    /// a <see cref="VideoPropertiesDialog"/> to define the video
    /// export properties.
    /// </summary>
    /// <returns><strong>True</strong> if the user selected valid properties,
    /// otherwise <strong>false</strong>.</returns>
    private bool GetVideoProperties()
    {
      if (this.sfdVideo.ShowDialog() == DialogResult.OK)
      {
        // Save filename in member.
        this.fileName = this.sfdVideo.FileName;

        this.Cursor = Cursors.WaitCursor;

        VideoPropertiesDialog videoPropertiesDlg = new VideoPropertiesDialog();

        List<Size> videoSizes = videoPropertiesDlg.VideoStreamSizes;
        videoSizes.Add(new Size(
          Document.ActiveDocument.ExperimentSettings.WidthStimulusScreen,
          Document.ActiveDocument.ExperimentSettings.HeightStimulusScreen));
        videoPropertiesDlg.VideoStreamSizes = videoSizes;

        this.Cursor = Cursors.Default;
        if (videoPropertiesDlg.ShowDialog() == DialogResult.OK)
        {
          this.videoProperties = videoPropertiesDlg.Properties;
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
      if (this.currentTrialTime > 0)
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
    /// <returns><strong>True</strong> if raw data could be succesfully found and loaded,
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
      if (usercamID == -1 || !this.btnShowUsercam.Checked)
      {
        // No usercam available
        this.usercamVideoPlayer.CloseMovie();

        return true;
      }

      TrialEvent cameraEvent = this.TrialEvents[usercamID];
      this.currentVideoStartTime = Convert.ToInt32(cameraEvent.Param);
      this.SetVideoStartTime(subjectName, this.currentVideoStartTime);
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
      // Stop update timer
      this.replayPicture.StopAnimation();

      if (this.btnShowUsercam.Checked && this.usercamVideoPlayer.PlayState == OgamaControls.PlayState.Running)
      {
        this.usercamVideoPlayer.StopMovie();
      }

      if (this.isUsingTrialVideo)
      {
        this.videoFramePusher.Stop();
      }

      if (this.btnEnableAudio.Checked)
      {
        this.Player.Stop();
      }

      // Enable the ComboBoxes
      this.cbbSubject.Enabled = true;
      this.cbbTrial.Enabled = true;

      // Activate and deactivate buttons
      this.btnStart.Enabled = true;
      this.btnStop.Enabled = false;
      this.btnRewind.Enabled = true;
      this.btnPause.Enabled = false;
      this.btnPause.Checked = false;
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

      ((MainForm)this.MdiParent).StatusLabel.Text = "Ready";
      ((MainForm)this.MdiParent).StatusProgressbar.Value = 0;
      ((MainForm)this.MdiParent).StatusRightLabel.Text = string.Empty;

      this.trialTimeLine.ResetTimeLine();
      this.currentTrialTime = this.trialTimeLine.CurrentTime;

      if (this.btnShowUsercam.Checked)
      {
        this.SetVideoStartTime(this.cbbSubject.Text, this.currentVideoStartTime);
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
        this.Player.Seek(0);
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

    #endregion //HELPER
  }
}