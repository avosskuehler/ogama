// <copyright file="FixationsModule.cs" company="FU Berlin">
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

namespace Ogama.Modules.Fixations
{
  using System;
  using System.Collections.Generic;
  using System.ComponentModel;
  using System.Data;
  using System.Drawing;
  using System.IO;
  using System.Windows.Forms;

  using Ogama.DataSet;
  using Ogama.ExceptionHandling;
  using Ogama.MainWindow;
  using Ogama.Modules.Common.Controls;
  using Ogama.Modules.Common.FormTemplates;
  using Ogama.Modules.Common.PictureTemplates;
  using Ogama.Modules.Common.Tools;
  using Ogama.Modules.Common.Types;
  using Ogama.Modules.ImportExport.Common;
  using Ogama.Modules.ImportExport.FixationData;
  using Ogama.Properties;
  using OgamaControls;
  using OgamaControls.Dialogs;

  using VectorGraphics.Elements;
  using VectorGraphics.Elements.ElementCollections;
  using VectorGraphics.Tools.CustomEventArgs;

  /// <summary>
  /// Derived from <see cref="FormWithSubjectAndTrialSelection"/>.
  /// This <see cref="Form"/> is the fixations module. 
  /// It handles the UI and the database connection for
  /// the <see cref="VectorGraphics.Canvas.Picture"/> class, which is the main element
  /// of this form.
  /// </summary>
  /// <remarks>This interface is intended to calculate and display the 
  /// gaze and mouse fixations made by the subjects. 
  /// There are several display modes and a data grid view
  /// which shows the calculated fixations.
  /// The calculation uses the fixation detection algorithms available from
  /// Company:   LC Technologies, Inc.
  ///            1483 Chain Bridge Road, Suite 104
  ///            McLean, VA 22101
  ///            (703) 385-7133
  /// that are ported to C# in the class <see cref="Fixation"/>.
  /// </remarks>
  public partial class FixationsModule : FormWithSubjectAndTrialSelection
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
    /// Stores the last value of the x drift correction.
    /// </summary>
    private int driftCorrectionLastXValue = 0;

    /// <summary>
    /// Stores the last value of the y drift correction.
    /// </summary>
    private int driftCorrectionLastYValue = 0;

    #endregion //FIELDS

    ///////////////////////////////////////////////////////////////////////////////
    // Construction and Initializing methods                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region CONSTRUCTION

    /// <summary>
    /// Initializes a new instance of the FixationsModule class.
    /// </summary>
    public FixationsModule()
    {
      // Init
      this.InitializeComponent();

      this.Picture = this.fixationsPicture;
      this.SubjectCombo = this.cbbSubject;
      this.TrialCombo = this.cbbTrial;
      this.ZoomTrackBar = this.trbZoom;

      this.InitializeDropDowns();
      this.InitializeCustomElements();
      this.InitAccelerators();
      this.InitializeDataBindings();
    }

    #endregion //CONSTRUCTION

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Properties                                                       //
    ///////////////////////////////////////////////////////////////////////////////
    #region PROPERTIES
    #endregion //PROPERTIES

    ///////////////////////////////////////////////////////////////////////////////
    // Public methods                                                            //
    ///////////////////////////////////////////////////////////////////////////////
    #region PUBLICMETHODS
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
      string gazeDisplayModeFromSettings = this.cbbGazeDisplayMode.Text;
      string mouseDisplayModeFromSettings = this.cbbMouseDisplayMode.Text;
      this.cbbGazeDisplayMode.Items.AddRange(Enum.GetNames(typeof(FixationDrawingMode)));
      this.cbbGazeDisplayMode.ComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
      this.cbbMouseDisplayMode.Items.AddRange(Enum.GetNames(typeof(FixationDrawingMode)));
      this.cbbMouseDisplayMode.ComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
      this.cbbGazeDisplayMode.SelectedItem = gazeDisplayModeFromSettings;
      this.cbbMouseDisplayMode.SelectedItem = mouseDisplayModeFromSettings;
      this.btnHelp.Click += new EventHandler(this.btnHelp_Click);
      this.pnlCanvas.Resize += new EventHandler(this.pnlCanvas_Resize);
    }

    /// <summary>
    /// Initializes data bindings. Mainly wires the assigned document dataset to the binding
    /// sources.
    /// </summary>
    protected override void InitializeDataBindings()
    {
      base.InitializeDataBindings();

      this.SetDataGridViewColumnsToReadOnlyStyle(this.dgvGazeFixations);
      this.SetDataGridViewColumnsToReadOnlyStyle(this.dgvMouseFixations);
    }

    /// <summary>
    /// Initializes accelerator keys. Binds to methods.
    /// </summary>
    protected override void InitAccelerators()
    {
      base.InitAccelerators();
      SetAccelerator(Keys.Escape, new AcceleratorAction(this.OnEscape));
    }

    /// <summary>
    /// Unregister custom events.
    /// </summary>
    protected override void CustomDispose()
    {
      base.CustomDispose();
      this.cbbGazeDisplayMode.ComboBox.SelectionChangeCommitted -= new EventHandler(this.cbbGazeDisplayMode_SelectionChangeCommitted);
      this.cbbMouseDisplayMode.ComboBox.SelectionChangeCommitted -= new EventHandler(this.cbbMouseDisplayMode_SelectionChangeCommitted);
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
      this.cbbGazeDisplayMode.ComboBox.SelectionChangeCommitted += new EventHandler(this.cbbGazeDisplayMode_SelectionChangeCommitted);
      this.cbbMouseDisplayMode.ComboBox.SelectionChangeCommitted += new EventHandler(this.cbbMouseDisplayMode_SelectionChangeCommitted);
    }

    /// <summary>
    /// Reads dropdown settings and loads corresponding images and data from database.
    /// Then notifys picture the changes.
    /// </summary>
    /// <returns><strong>True</strong> if trial could be succesfully loaded,
    /// otherwise <strong>false</strong>.</returns>
    protected override bool NewTrialSelected()
    {
      try
      {
        // Stop if no trial is selected.
        if (!Document.ActiveDocument.SelectionState.IsSet)
        {
          return false;
        }

        string subjectName = Document.ActiveDocument.SelectionState.SubjectName;
        int trialID = Document.ActiveDocument.SelectionState.TrialID;
        int trialSequence = Document.ActiveDocument.SelectionState.TrialSequence;

        // Switch to WaitCursor
        Cursor = Cursors.WaitCursor;

        // Read settings
        ExperimentSettings set = Document.ActiveDocument.ExperimentSettings;
        if (set != null)
        {
          int usercamID;

          // Reset Drift correction
          this.nudDriftXCorrection.Value = 0;
          this.driftCorrectionLastXValue = 0;
          this.nudDriftYCorrection.Value = 0;
          this.driftCorrectionLastYValue = 0;

          // Get the events for the current trial indexed by type
          this.TrialEvents = Queries.GetTrialEvents(subjectName, trialSequence, out usercamID);

          // Update time line with events
          this.trialTimeLine.TrialEvents = this.TrialEvents;
          this.trialTimeLine.HighlightFirstSlide();

          // Load trial stimulus into picture
          if (!LoadTrialStimulus(trialID))
          {
            this.LoadSlide(SlideNotFoundSlide, ActiveXMode.Off);
          }

          this.LoadAOIs(trialID);

          // Load the whole trial data
          if (!this.LoadFixations(subjectName, trialSequence))
          {
            return false;
          }

          this.FilterFixationsBySlide();
        }
      }
      catch (Exception ex)
      {
        ExceptionMethods.HandleException(ex);
      }
      finally
      {
        // Reset Cursor
        Cursor = Cursors.Default;
      }

      return true;
    }

    /// <summary>
    /// The <see cref="MainForm.EditCopy"/> event handler for the
    /// parent <see cref="Form"/> <see cref="MainForm"/>.
    /// This method handles the edit copy event from main form
    /// by either copying selected cells in data grid view or 
    /// rendering a copy of the displayed picture 
    /// to clipboard, depending on focus.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    protected override void mainWindow_EditCopy(object sender, EventArgs e)
    {
      if (this.MdiParent.ActiveMdiChild.Name == this.Name)
      {
        try
        {
          if (this.dgvGazeFixations.Focused)
          {
            Clipboard.SetDataObject(this.dgvGazeFixations.GetClipboardContent());
            ((MainForm)this.MdiParent).StatusLabel.Text = "Table selection exported to clipboard.";
          }
          else
          {
            Clipboard.SetImage(this.fixationsPicture.RenderToImage());
            ((MainForm)this.MdiParent).StatusLabel.Text = "Image exported to clipboard.";
          }
        }
        catch (Exception ex)
        {
          ExceptionMethods.HandleException(ex);
        }
      }
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
    /// Wires Mainform events and initializes vector graphics picture.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void FixationsModule_Load(object sender, EventArgs e)
    {
      try
      {
        this.Picture.PresentationSize = Document.ActiveDocument.PresentationSize;
        this.ResizeCanvas();

        this.nudGazeFixDiameterDiv.Value = (decimal)Document.ActiveDocument.ExperimentSettings.GazeDiameterDiv;
        this.fixationsPicture.GazeFixationsDiameterDivisor = Document.ActiveDocument.ExperimentSettings.GazeDiameterDiv;

        this.nudMouseFixDiameterDiv.Value = (decimal)Document.ActiveDocument.ExperimentSettings.MouseDiameterDiv;
        this.fixationsPicture.MouseFixationsDiameterDivisor = Document.ActiveDocument.ExperimentSettings.MouseDiameterDiv;

        this.spcGazeMouseTables.Panel2Collapsed = true;
        this.tosMouseDisplay.Visible = false;

        this.InitialDisplay();
      }
      catch (Exception ex)
      {
        ExceptionMethods.HandleException(ex);
      }
    }

    /// <summary>
    /// The <see cref="Form.FormClosing"/> event handler. 
    /// Updates the application settings with the modified pen and font styles.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">The <see cref="FormClosingEventArgs"/> with the event data.</param>
    private void frmFixations_FormClosing(object sender, FormClosingEventArgs e)
    {
      this.fixationsPicture.SaveStylesToApplicationSettings();
    }

    /// <summary>
    /// The <see cref="ComboBox.SelectionChangeCommitted"/> event handler for the
    /// <see cref="ComboBox"/> <see cref="cbbGazeDisplayMode"/>.
    /// User selected new gaze display mode from drop down list,
    /// so update pictures drawing mode flags and call recalculation.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void cbbGazeDisplayMode_SelectionChangeCommitted(object sender, EventArgs e)
    {
      this.fixationsPicture.GazeDrawingMode =
        (FixationDrawingMode)Enum.Parse(
        typeof(FixationDrawingMode),
        (string)this.cbbGazeDisplayMode.SelectedItem);
    }

    /// <summary>
    /// The <see cref="ComboBox.SelectionChangeCommitted"/> event handler for the
    /// <see cref="ComboBox"/> <see cref="cbbMouseDisplayMode"/>.
    /// User selected new mouse display mode from drop down list,
    /// so update pictures drawing mode flags and call recalculation.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void cbbMouseDisplayMode_SelectionChangeCommitted(object sender, EventArgs e)
    {
      this.fixationsPicture.MouseDrawingMode =
        (FixationDrawingMode)Enum.Parse(
        typeof(FixationDrawingMode),
        (string)this.cbbMouseDisplayMode.SelectedItem);
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler for the
    /// <see cref="Button"/> <see cref="btnGazeConnections"/>.
    /// Enables or disables the gaze connections display.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void btnGazeConnections_Click(object sender, EventArgs e)
    {
      this.fixationsPicture.GazeConnections = this.btnGazeConnections.Checked;
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler for the
    /// <see cref="Button"/> <see cref="btnGazeNumbers"/>.
    /// Enables or disables the gaze enumeration display.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void btnGazeNumbers_Click(object sender, EventArgs e)
    {
      this.fixationsPicture.GazeNumbers = this.btnGazeNumbers.Checked;
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler for the
    /// <see cref="Button"/> <see cref="btnMouseConnections"/>.
    /// Enables or disables the mouse connections display.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void btnMouseConnections_Click(object sender, EventArgs e)
    {
      this.fixationsPicture.MouseConnections = this.btnMouseConnections.Checked;
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler for the
    /// <see cref="Button"/> <see cref="btnMouseNumbers"/>.
    /// Enables or disables the mouse enumeration display.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void btnMouseNumbers_Click(object sender, EventArgs e)
    {
      this.fixationsPicture.MouseNumbers = this.btnMouseNumbers.Checked;
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler for the
    /// <see cref="Button"/> <see cref="btnCalculateFix"/>.
    /// User pressed calculate button. Starts fixation calculation 
    /// referring to parameter selections in a background worker thread.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void btnCalculateFix_Click(object sender, EventArgs e)
    {
      // Disable the Start button.
      this.btnCalculateFix.Enabled = false;

      this.Cursor = Cursors.WaitCursor;

      // unbind datagridview because it should not be updated while background worker is busy,
      // that would give a cross thread error. Is reset in backgroundWorker_RunWorkerCompleted 
      this.dgvGazeFixations.DataSource = null;
      this.dgvMouseFixations.DataSource = null;

      if (this.btnSelectedSubject.Checked)
      {
        string subject = this.cbbSubject.Text;
        ((MainForm)this.MdiParent).StatusLabel.Text = "Calculating subject: " + subject;

        int deleted = Document.ActiveDocument.DocDataSet.GazeFixationsAdapter.DeleteBySubject(subject);
        deleted = Document.ActiveDocument.DocDataSet.MouseFixationsAdapter.DeleteBySubject(subject);

        Document.ActiveDocument.DocDataSet.GazeFixationsAdapter.Fill(Document.ActiveDocument.DocDataSet.GazeFixations);
        Document.ActiveDocument.DocDataSet.MouseFixationsAdapter.Fill(Document.ActiveDocument.DocDataSet.MouseFixations);

        FixationCalculation calculationObject = new FixationCalculation();
        calculationObject.Worker.ProgressChanged += new ProgressChangedEventHandler(this.bgwCalcFixations_ProgressChanged);
        calculationObject.Worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(this.bgwCalcFixations_RunWorkerCompleted);
        calculationObject.CalculateSubjectsFixations(subject);
      }
      else if (this.btnAllSubjects.Checked)
      {
        int affectedRows = Document.ActiveDocument.DocDataSet.GazeFixationsAdapter.DeleteAll();
        affectedRows = Document.ActiveDocument.DocDataSet.MouseFixationsAdapter.DeleteAll();

        Document.ActiveDocument.DocDataSet.GazeFixationsAdapter.Fill(Document.ActiveDocument.DocDataSet.GazeFixations);
        Document.ActiveDocument.DocDataSet.MouseFixationsAdapter.Fill(Document.ActiveDocument.DocDataSet.MouseFixations);

        this.bgwCalcFixationsForAllSubjects.RunWorkerAsync();
      }

      this.Cursor = Cursors.Default;
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler for the
    /// <see cref="Button"/> <see cref="btnShowHideTables"/>.
    /// User selected to show or hide the data grid views, so expand or
    /// collapse the referring split containers.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void btnShowHideTables_Click(object sender, EventArgs e)
    {
      this.spcPictureTools.Panel2Collapsed = !this.btnShowHideTables.Checked;
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler for the
    /// <see cref="Button"/> <see cref="btnShowHideAOI"/>.
    /// User selected to show or hide the underlying areas of interest,
    /// so update the picture with the new settings.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void btnShowHideAOI_Click(object sender, EventArgs e)
    {
      // Load Data according to entrys in DropDowns
      this.NewTrialSelected();
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler for the
    /// <see cref="Button"/> <see cref="btnSeekNextSlide"/>.
    /// Activate next slide in trial.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void btnSeekNextSlide_Click(object sender, EventArgs e)
    {
      // Skip if no data available
      if (this.CurrentTrial == null)
      {
        return;
      }

      this.trialTimeLine.HighlightNextSlide(true);
      this.LoadSlide(this.CurrentTrial[this.trialTimeLine.HighlightedSlideIndex], ActiveXMode.Off);
      this.FilterFixationsBySlide();
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler for the
    /// <see cref="Button"/> <see cref="btnSeekPreviousSlide"/>.
    /// Activate previous slide in trial.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void btnSeekPreviousSlide_Click(object sender, EventArgs e)
    {
      // Skip if no data available
      if (this.CurrentTrial == null)
      {
        return;
      }

      this.trialTimeLine.HighlightNextSlide(false);
      this.LoadSlide(this.CurrentTrial[this.trialTimeLine.HighlightedSlideIndex], ActiveXMode.Off);
      this.FilterFixationsBySlide();
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler for the
    /// <see cref="Button"/> <see cref="btnEliminateData"/>.
    /// User pressed the "eliminate trial from analysis" button, 
    /// so update trial table with an "yes"
    /// in the eliminate column.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void btnEliminateData_Click(object sender, EventArgs e)
    {
      int currentTrialSequence = Document.ActiveDocument.SelectionState.TrialSequence;
      string activeSubject = Document.ActiveDocument.SelectionState.SubjectName;
      if (activeSubject != null)
      {
        OgamaDataSet.TrialsDataTable trialsTable = Document.ActiveDocument.DocDataSet.TrialsAdapter.GetDataBySubjectAndSequence(activeSubject, currentTrialSequence);
        Document.ActiveDocument.DocDataSet.TrialsAdapter.UpdateEliminateByID("yes", (long)trialsTable.Rows[0]["ID"]);
        Document.ActiveDocument.DocDataSet.TrialsAdapter.Fill(Document.ActiveDocument.DocDataSet.Trials);
      }
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler for the
    /// <see cref="Button"/> <see cref="btnShowOptions"/>.
    /// User pressed the "show fixation options" button,
    /// so call the experiment settings dialog
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void btnShowOptions_Click(object sender, EventArgs e)
    {
      MainForm.ShowExperimentSettingsDialog();
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler for the
    /// <see cref="Button"/> <see cref="btnEye"/>.
    /// Shows or hides the data grid view for the gaze data
    /// and displays or hides the gaze fixations.
    /// </summary>
    /// <remarks>If neither gaze nor mouse buttons would be
    /// active check the mouse button.</remarks>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void btnEye_Click(object sender, EventArgs e)
    {
      if (!this.btnEye.Checked && !this.btnMouse.Checked)
      {
        this.btnMouse.Checked = true;
      }

      this.ResetDriftCorrections();

      this.spcGazeMouseTables.Panel1Collapsed = !this.btnEye.Checked;
      this.tosGazeDisplay.Visible = this.btnEye.Checked;
      this.tosMouseDisplay.Visible = this.btnMouse.Checked;
      this.UpdatePicturesSampleType();
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler for the
    /// <see cref="Button"/> <see cref="btnMouse"/>.
    /// Shows or hides the data grid view for the gaze data
    /// and displays or hides the gaze fixations.
    /// </summary>
    /// <remarks>If neither gaze nor mouse buttons would be
    /// active check the gaze button.</remarks>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void btnMouse_Click(object sender, EventArgs e)
    {
      if (!this.btnEye.Checked && !this.btnMouse.Checked)
      {
        this.btnEye.Checked = true;
      }

      this.ResetDriftCorrections();

      this.spcGazeMouseTables.Panel2Collapsed = !this.btnMouse.Checked;
      this.tosMouseDisplay.Visible = this.btnMouse.Checked;
      this.tosGazeDisplay.Visible = this.btnEye.Checked;

      this.UpdatePicturesSampleType();
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler for the
    /// <see cref="Button"/> <see cref="btnSelectedSubject"/>.
    /// Updates state of "all subjects" button.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void btnSelectedSubject_Click(object sender, EventArgs e)
    {
      this.btnAllSubjects.Checked = !this.btnSelectedSubject.Checked;
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler for the
    /// <see cref="Button"/> <see cref="btnAllSubjects"/>.
    /// Updates state of "selected subject" button.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void btnAllSubjects_Click(object sender, EventArgs e)
    {
      this.btnSelectedSubject.Checked = !this.btnAllSubjects.Checked;
    }

    /// <summary>
    /// The <see cref="NumericUpDown.ValueChanged"/> event handler for the
    /// <see cref="NumericUpDown"/>s <see cref="nudDriftXCorrection"/> and
    /// <see cref="nudDriftYCorrection"/>.
    /// User selected new drift correction values, so update picture 
    /// by moving fixation values.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void nudDriftCorrection_ValueChanged(object sender, EventArgs e)
    {
      int relativeDriftXCorrection = (int)this.nudDriftXCorrection.Value - this.driftCorrectionLastXValue;
      int relativeDriftYCorrection = (int)this.nudDriftYCorrection.Value - this.driftCorrectionLastYValue;

      if (this.btnEye.Checked)
      {
        if (this.fixationsPicture.GazeFixations != null)
        {
          foreach (DataRow gazeFixationsRow in this.fixationsPicture.GazeFixations.Rows)
          {
            gazeFixationsRow["PosX"] = (double)gazeFixationsRow["PosX"] + relativeDriftXCorrection;
            gazeFixationsRow["PosY"] = (double)gazeFixationsRow["PosY"] + relativeDriftYCorrection;
          }
        }
      }

      if (this.btnMouse.Checked)
      {
        if (this.fixationsPicture.MouseFixations != null)
        {
          foreach (DataRow mouseFixationsRow in this.fixationsPicture.MouseFixations.Rows)
          {
            mouseFixationsRow["PosX"] = (double)mouseFixationsRow["PosX"] + relativeDriftXCorrection;
            mouseFixationsRow["PosY"] = (double)mouseFixationsRow["PosY"] + relativeDriftYCorrection;
          }
        }
      }

      this.driftCorrectionLastXValue = (int)this.nudDriftXCorrection.Value;
      this.driftCorrectionLastYValue = (int)this.nudDriftYCorrection.Value;

      this.fixationsPicture.DrawFixations(true);
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler for the
    /// <see cref="Button"/> <see cref="btnSaveDriftCorrection"/>.
    /// Updates the raw data with the new drift correction.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void btnSaveDriftCorrection_Click(object sender, EventArgs e)
    {
      this.Cursor = Cursors.WaitCursor;

      string subjectName = Document.ActiveDocument.SelectionState.SubjectName;
      int trialSequence = Document.ActiveDocument.SelectionState.TrialSequence;
      int absoluteDriftXCorrection = (int)this.nudDriftXCorrection.Value;
      int absoluteDriftYCorrection = (int)this.nudDriftYCorrection.Value;

      OgamaDataSet.RawdataDataTable rawData = Queries.GetRawDataBySubjectAndTrialSequence(subjectName, trialSequence);
      foreach (DataRow rawRow in rawData.Rows)
      {
        double gazePosX = rawRow.IsNull("GazePosX") ? 0 : (double)rawRow["GazePosX"];
        double gazePosY = rawRow.IsNull("GazePosX") ? 0 : (double)rawRow["GazePosY"];
        if (gazePosX != 0 && gazePosY != 0)
        {
          rawRow["GazePosX"] = (double)rawRow["GazePosX"] + absoluteDriftXCorrection;
          rawRow["GazePosY"] = (double)rawRow["GazePosY"] + absoluteDriftYCorrection;
        }
      }

      int affectedRows = Document.ActiveDocument.DocDataSet.GazeFixationsAdapter.Update((OgamaDataSet.GazeFixationsDataTable)this.fixationsPicture.GazeFixations);
      affectedRows = Queries.UpdateRawDataBySubject(rawData, subjectName);

      this.ResetDriftCorrections();
      this.Cursor = Cursors.Default;
    }

    /// <summary>
    /// The <see cref="NumericUpDown.ValueChanged"/> event handler for the
    /// <see cref="NumericUpDown"/> <see cref="nudGazeFixDiameterDiv"/>.
    /// User selected new divider for gaze fixation diameters,
    /// so set the <see cref="PictureWithFixations.GazeFixationsDiameterDivisor"/> property.    
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void nudGazeFixDiameterDiv_ValueChanged(object sender, EventArgs e)
    {
      this.fixationsPicture.GazeFixationsDiameterDivisor = (float)this.nudGazeFixDiameterDiv.Value;
    }

    /// <summary>
    /// The <see cref="NumericUpDown.ValueChanged"/> event handler for the
    /// <see cref="NumericUpDown"/> <see cref="nudMouseFixDiameterDiv"/>.
    /// User selected new divider for mouse fixation diameters,
    /// so set the <see cref="PictureWithFixations.MouseFixationsDiameterDivisor"/> property.    
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void nudMouseFixDiameterDiv_ValueChanged(object sender, EventArgs e)
    {
      this.fixationsPicture.MouseFixationsDiameterDivisor = (float)this.nudMouseFixDiameterDiv.Value;
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler for the
    /// <see cref="Button"/> <see cref="btnGazePenStyle"/>.
    /// Opens a <see cref="PenAndFontStyleDlg"/> and wires update events to
    /// member methods <see cref="OnPenChanged"/> and <see cref="OnFontStyleChanged"/>
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void btnGazePenStyle_Click(object sender, EventArgs e)
    {
      Pen backupPen = this.fixationsPicture.GazeFixationsPen;
      Font backupFont = this.fixationsPicture.GazeFixationsFont;
      SolidBrush backupBrush = new SolidBrush(this.fixationsPicture.GazeFixationsFontColor);
      PenAndFontStyleDlg dlgFixationStyle = new PenAndFontStyleDlg();
      dlgFixationStyle.Text = "Set gaze fixations pen and font style...";
      dlgFixationStyle.Pen = this.fixationsPicture.GazeFixationsPen;
      dlgFixationStyle.CustomFont = this.fixationsPicture.GazeFixationsFont;
      dlgFixationStyle.CustomFontBrush = new SolidBrush(this.fixationsPicture.GazeFixationsFontColor);
      dlgFixationStyle.PenChanged += new EventHandler<PenChangedEventArgs>(this.OnPenChanged);
      dlgFixationStyle.FontStyleChanged += new EventHandler<FontChangedEventArgs>(this.OnFontStyleChanged);

      if (dlgFixationStyle.ShowDialog() == DialogResult.Cancel)
      {
        PenChangedEventArgs ea = new PenChangedEventArgs(backupPen, VGStyleGroup.FIX_GAZE_ELEMENT);
        FontChangedEventArgs eaf = new FontChangedEventArgs(backupFont, backupBrush.Color, VGAlignment.Center, VGStyleGroup.FIX_GAZE_ELEMENT);
        this.fixationsPicture.PenChanged(this, ea);
        this.fixationsPicture.FontStyleChanged(this, eaf);
      }
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler for the
    /// <see cref="Button"/> <see cref="btnMousePenStyle"/>.
    /// Opens a <see cref="PenAndFontStyleDlg"/> and wires update events to
    /// member methods <see cref="OnPenChanged"/> and <see cref="OnFontStyleChanged"/>
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void btnMousePenStyle_Click(object sender, EventArgs e)
    {
      Pen backupPen = this.fixationsPicture.MouseFixationsPen;
      Font backupFont = this.fixationsPicture.MouseFixationsFont;
      SolidBrush backupBrush = new SolidBrush(this.fixationsPicture.MouseFixationsFontColor);
      PenAndFontStyleDlg dlgFixationStyle = new PenAndFontStyleDlg();
      dlgFixationStyle.Text = "Set mouse fixations pen and font style...";
      dlgFixationStyle.Pen = this.fixationsPicture.MouseFixationsPen;
      dlgFixationStyle.CustomFont = this.fixationsPicture.MouseFixationsFont;
      dlgFixationStyle.CustomFontBrush = new SolidBrush(this.fixationsPicture.MouseFixationsFontColor);
      dlgFixationStyle.PenChanged += new EventHandler<PenChangedEventArgs>(this.OnPenChanged);
      dlgFixationStyle.FontStyleChanged += new EventHandler<FontChangedEventArgs>(this.OnFontStyleChanged);

      if (dlgFixationStyle.ShowDialog() == DialogResult.Cancel)
      {
        PenChangedEventArgs ea = new PenChangedEventArgs(backupPen, VGStyleGroup.FIX_MOUSE_ELEMENT);
        FontChangedEventArgs eaf = new FontChangedEventArgs(backupFont, backupBrush.Color, VGAlignment.Center, VGStyleGroup.FIX_MOUSE_ELEMENT);
        this.fixationsPicture.PenChanged(this, ea);
        this.fixationsPicture.FontStyleChanged(this, eaf);
      }
    }

    /// <summary>
    /// The <see cref="OgamaControls.Dialogs.PenAndFontStyleDlg.PenChanged"/> 
    /// event handler for the <see cref="OgamaControls.Dialogs.PenAndFontStyleDlg"/>.
    /// Updates pictures elements by calling the pictures pen changed event handler.
    /// </summary>
    /// <param name="sender">Source of the event</param>
    /// <param name="e">A <see cref="PenChangedEventArgs"/> with group and pen to change.</param>
    private void OnPenChanged(object sender, PenChangedEventArgs e)
    {
      e.ElementGroup = VGStyleGroup.FIX_GAZE_ELEMENT;
      this.fixationsPicture.PenChanged(sender, e);
    }

    /// <summary>
    /// The <see cref="OgamaControls.Dialogs.PenAndFontStyleDlg.FontStyleChanged"/> 
    /// event handler for the <see cref="OgamaControls.Dialogs.PenAndFontStyleDlg"/>.
    /// Updates picture elements by calling the pictures font changed event handler.
    /// </summary>
    /// <param name="sender">Source of the event</param>
    /// <param name="e">A <see cref="FontChangedEventArgs"/> with group and font to change.</param>
    private void OnFontStyleChanged(object sender, FontChangedEventArgs e)
    {
      e.ElementGroup = VGStyleGroup.FIX_GAZE_ELEMENT;
      this.fixationsPicture.FontStyleChanged(sender, e);
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler for the 
    /// <see cref="ContextMenu"/> <see cref="cmnuSelectAll"/>
    /// Context menu entry. Selects all rows in the data grid view.
    /// </summary>
    /// <param name="sender">Source of the event</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void cmnuSelectAll_Click(object sender, EventArgs e)
    {
      this.dgvGazeFixations.SelectAll();
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler for the 
    /// <see cref="ContextMenu"/> <see cref="cmnuCopyToClipboard"/>
    /// Context menu entry. Copies selected cells in the data grid view to
    /// clipboard
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void cmnuCopyToClipboard_Click(object sender, EventArgs e)
    {
      try
      {
        Clipboard.SetDataObject(this.dgvGazeFixations.GetClipboardContent());
        ((MainForm)this.MdiParent).StatusLabel.Text = "Table selection exported to clipboard.";
      }
      catch (Exception ex)
      {
        ExceptionMethods.HandleException(ex);
      }
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler for the
    /// <see cref="Button"/> <see cref="btnExport"/>.
    /// Exports fixation table to given file.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void btnExport_Click(object sender, EventArgs e)
    {
      try
      {
        ExportOptionsDialog optionsDialog = new ExportOptionsDialog();
        if (optionsDialog.ShowDialog() == DialogResult.OK)
        {
          ExportOptions options = optionsDialog.ExportOptions;
          if (options.ExportGaze)
          {
            this.Export(options, SampleType.Gaze);
          }

          if (options.ExportMouse)
          {
            this.Export(options, SampleType.Mouse);
          }
        }

        ((MainForm)this.MdiParent).StatusLabel.Text = "Data successfully exported.";
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
    /// The <see cref="Control.Click"/> event handler for the
    /// <see cref="Button"/> <see cref="btnImport"/>.
    /// User selected import table button. Starts 
    /// <see cref="ImportFixations.Start()"/> 
    /// import assistant.
    /// </summary>
    /// <param name="sender">Source of the event</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void btnImport_Click(object sender, EventArgs e)
    {
      try
      {
        ImportFixations.Start();
        this.NewTrialSelected();
        ((MainForm)this.MdiParent).StatusLabel.Text = "File successfully imported.";
      }
      catch (Exception ex)
      {
        ExceptionMethods.HandleException(ex);
      }
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler for the
    /// <see cref="Button"/> <see cref="btnDeleteFixations"/>.
    /// User selected to delete all fixations for current user, so do it
    /// by using the table adapter.
    /// </summary>
    /// <param name="sender">Source of the event</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void btnDeleteFixations_Click(object sender, EventArgs e)
    {
      if (Document.ActiveDocument.SelectionState.SubjectName != null)
      {
        Document.ActiveDocument.DocDataSet.GazeFixationsAdapter.DeleteBySubject(Document.ActiveDocument.SelectionState.SubjectName);
        this.NewTrialSelected();
      }
    }

    #endregion //WINDOWSEVENTHANDLER

    ///////////////////////////////////////////////////////////////////////////////
    // Eventhandler for Custom Defined Events                                    //
    ///////////////////////////////////////////////////////////////////////////////
    #region CUSTOMEVENTHANDLER

    /// <summary>
    /// Cancels background threads when escape is pressed.
    /// </summary>
    private void OnEscape()
    {
      this.bgwCalcFixationsForAllSubjects.CancelAsync();
    }

    /// <summary>
    /// The <see cref="TimeLine.SectionStartValueChanged"/> event handler for the
    /// <see cref="TrialTimeLine"/> <see cref="trialTimeLine"/>.
    /// Sets <see cref="VectorGraphics.Canvas.Picture.SectionStartTime"/> and redraws fixations.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">A <see cref="TimeLine.PositionValueChangedEventArguments"/> with the event data.</param>
    private void TrialTimeLine_SectionStartValueChanged(object sender, TimeLine.PositionValueChangedEventArguments e)
    {
      this.fixationsPicture.SectionStartTime = e.Millisecond;
      this.fixationsPicture.DrawFixations(true);
    }

    /// <summary>
    /// The <see cref="TimeLine.SectionEndValueChanged"/> event handler for the
    /// <see cref="TrialTimeLine"/> <see cref="trialTimeLine"/>.
    /// Sets <see cref="VectorGraphics.Canvas.Picture.SectionEndTime"/> and redraws fixations.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">A <see cref="TimeLine.PositionValueChangedEventArguments"/> with the event data.</param>
    private void TrialTimeLine_SectionEndValueChanged(object sender, TimeLine.PositionValueChangedEventArguments e)
    {
      this.fixationsPicture.SectionEndTime = e.Millisecond;
      this.fixationsPicture.DrawFixations(true);
    }

    #endregion //CUSTOMEVENTHANDLER

    #endregion //EVENTS

    ///////////////////////////////////////////////////////////////////////////////
    // Methods and Eventhandling for Background tasks                            //
    ///////////////////////////////////////////////////////////////////////////////
    #region BACKGROUNDWORKER

    /// <summary>
    /// The <see cref="BackgroundWorker.DoWork"/> event handler for the
    /// <see cref="BackgroundWorker"/> <see cref="bgwCalcFixationsForAllSubjects"/>.
    /// Background worker thread working method for calculating
    /// the fixation data of all subjects in the subject list.
    /// </summary>
    /// <param name="sender">Source of the event</param>
    /// <param name="e">A <see cref="DoWorkEventArgs"/> with the event data.</param>
    private void bgwCalcFixationsForAllSubjects_DoWork(object sender, DoWorkEventArgs e)
    {
      // Get the BackgroundWorker that raised this event.
      BackgroundWorker worker = sender as BackgroundWorker;

      object[] arguments = (object[])e.Argument;

      this.CalculateFixForAllSubjects(worker, e);
    }

    /// <summary>
    /// The <see cref="BackgroundWorker.RunWorkerCompleted"/> event handler for the
    /// <see cref="BackgroundWorker"/> <see cref="bgwCalcFixationsForAllSubjects"/>.
    /// This event handler deals with the results of the
    /// background operation and updates the data grid view and picture.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">A <see cref="RunWorkerCompletedEventArgs"/> with the event data.</param>
    private void bgwCalcFixations_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
    {
      // First, handle the case where an exception was thrown.
      if (e.Error != null)
      {
        ExceptionMethods.HandleException(e.Error);
      }
      else if (e.Cancelled)
      {
        ((MainForm)this.MdiParent).StatusLabel.Text = "Status: Cancelled";
      }
      else
      {
        // Finally, handle the case where the operation succeeded.
        ((MainForm)this.MdiParent).StatusLabel.Text = "Ready";
      }

      Document.ActiveDocument.DocDataSet.AcceptChanges();

      ((MainForm)this.MdiParent).StatusProgressbar.Value = 0;

      // Enable the Start button.
      this.btnCalculateFix.Enabled = true;

      // Redraw UI
      this.NewTrialSelected();
    }

    /// <summary>
    /// The <see cref="BackgroundWorker.ProgressChanged"/> event handler for the
    /// <see cref="BackgroundWorker"/> <see cref="bgwCalcFixationsForAllSubjects"/>.
    /// This event handler updates the progress bar of the main form.
    /// </summary>
    /// <param name="sender">Source of the event</param>
    /// <param name="e">A <see cref="ProgressChangedEventArgs"/> with the event data</param>
    private void bgwCalcFixations_ProgressChanged(object sender, ProgressChangedEventArgs e)
    {
      ((MainForm)this.MdiParent).StatusProgressbar.Value = e.ProgressPercentage;
    }

    #endregion //BACKGROUNDWORKER

    ///////////////////////////////////////////////////////////////////////////////
    // Methods for doing main class job                                          //
    ///////////////////////////////////////////////////////////////////////////////
    #region METHODS

    /// <summary>
    /// This method exports the fixations of the given <see cref="SampleType"/>
    /// to the given File including AOI information.
    /// </summary>
    /// <param name="options">An <see cref="ExportOptions"/> with options for the file export.</param>
    /// <param name="type">The <see cref="SampleType"/> to be exported.</param>
    private void Export(ExportOptions options, SampleType type)
    {
      string tableName = string.Empty;
      string fileName = string.Empty;

      DataTable dataTable = new DataTable();
      switch (type)
      {
        case SampleType.Gaze:
          tableName = "GazeFixations";
          dataTable = Document.ActiveDocument.DocDataSet.GazeFixations;
          fileName = options.GazeFileName;
          break;
        case SampleType.Mouse:
          tableName = "MouseFixations";
          dataTable = Document.ActiveDocument.DocDataSet.MouseFixations;
          fileName = options.MouseFileName;
          break;
      }

      bool unknownSubjectFound = false;
      string missingSubjectName = string.Empty;

      using (StreamWriter exportFileWriter = new StreamWriter(fileName))
      {
        // Write Documentation
        exportFileWriter.WriteLine("# File: " + Path.GetFileName(fileName));
        exportFileWriter.WriteLine("# Created: " + DateTime.Today.Date.ToLongDateString() + "," + DateTime.Now.ToLongTimeString());
        exportFileWriter.WriteLine("# with: " + Application.ProductName + " Version: " + Document.ActiveDocument.ExperimentSettings.OgamaVersion.ToString(3));

        exportFileWriter.WriteLine("# Contents: " + tableName + " table.");
        exportFileWriter.WriteLine("# Applies to Projekt:" + Document.ActiveDocument.ExperimentSettings.Name);
        exportFileWriter.WriteLine("#");

        if (options.ExportFixations)
        {
          // Write Column Names
          foreach (DataColumn dataColumn in dataTable.Columns)
          {
            exportFileWriter.Write(dataColumn.Caption);
            exportFileWriter.Write("\t");
          }
        }
        else
        {
          // We should export saccades which need some other column descriptions
          exportFileWriter.Write("SubjectName");
          exportFileWriter.Write("\t");
          exportFileWriter.Write("TrialID");
          exportFileWriter.Write("\t");
          exportFileWriter.Write("TrialSequence");
          exportFileWriter.Write("\t");
          exportFileWriter.Write("CountInTrial");
          exportFileWriter.Write("\t");
          exportFileWriter.Write("StartTime");
          exportFileWriter.Write("\t");
          exportFileWriter.Write("Duration");
          exportFileWriter.Write("\t");
          exportFileWriter.Write("Distance");
          exportFileWriter.Write("\t");
          exportFileWriter.Write("Velocity");
          exportFileWriter.Write("\t");
          exportFileWriter.Write("Validity");
          exportFileWriter.Write("\t");
        }

        if (options.ExportSubjectDetails)
        {
          exportFileWriter.Write("SubjectCategory");
          exportFileWriter.Write("\t");
          exportFileWriter.Write("Age");
          exportFileWriter.Write("\t");
          exportFileWriter.Write("Sex");
          exportFileWriter.Write("\t");
          exportFileWriter.Write("Handedness");
          exportFileWriter.Write("\t");
          exportFileWriter.Write("Comments");
          exportFileWriter.Write("\t");

          DataTable customParams =
            Document.ActiveDocument.DocDataSet.ParamsAdapter.GetData();
          foreach (DataRow paramRow in customParams.Rows)
          {
            exportFileWriter.Write(paramRow["Param"]);
            exportFileWriter.Write("\t");
          }
        }

        if (options.ExportTrialDetails)
        {
          exportFileWriter.Write("Trial Name");
          exportFileWriter.Write("\t");
          exportFileWriter.Write("Trial Category");
          exportFileWriter.Write("\t");
          exportFileWriter.Write("SlideNr");
          exportFileWriter.Write("\t");
        }

        if (options.ExportAOIDetails)
        {
          if (options.ExportFixations)
          {
            exportFileWriter.Write("AOI");
            exportFileWriter.Write("\t");
            exportFileWriter.Write("AOI group");
            exportFileWriter.Write("\t");
          }
          else
          {
            exportFileWriter.Write("Saccade Target AOI");
            exportFileWriter.Write("\t");
            exportFileWriter.Write("Saccade Target AOI group");
            exportFileWriter.Write("\t");
          }
        }

        exportFileWriter.WriteLine();

        int trialID = -1;
        int lastTrialID = -1;

        string subjectName = string.Empty;
        string lastsubjectName = "LastSubjectKLSMA";

        DataView trialsAOIsView = new DataView(Document.ActiveDocument.DocDataSet.AOIs);
        SubjectsData subjectData = new SubjectsData();
        Dictionary<string, string> subjectParams = new Dictionary<string, string>();

        // Sort fixation data
        DataView fixationsView = new DataView(dataTable);
        fixationsView.Sort = "SubjectName, TrialSequence, CountInTrial ASC";

        VGElementCollection trialAOIs = new VGElementCollection();
        string category = string.Empty;
        string trialName = string.Empty;
        PointF lastFixationCenter = PointF.Empty;
        long lastFixationEndTime = 0;
        int lastFixationDuration = 0;
        int countInTrial = 0;
        int slideNr = 0;
        List<long> slideStartTimes = new List<long>();
        foreach (DataRowView dataRow in fixationsView)
        {
          trialID = (int)dataRow["TrialID"];

          // Skip trials that are not selected.
          if (!options.CheckedTrialIDs.Contains(trialID))
          {
            continue;
          }

          int trialSequence = (int)dataRow["TrialSequence"];
          long startTime = (long)dataRow["StartTime"];
          int length = (int)dataRow["Length"];
          float posX = Convert.ToSingle(dataRow["PosX"]);
          float posY = Convert.ToSingle(dataRow["PosY"]);
          PointF fixationCenter = new PointF(posX, posY);

          subjectName = dataRow["SubjectName"].ToString();
          if (!options.CheckedSubjects.Contains(subjectName))
          {
            continue;
          }

          if (options.ExportSubjectDetails)
          {
            if (subjectName != lastsubjectName)
            {
              subjectData = new SubjectsData();

              DataTable subjectsTable = Document.ActiveDocument.DocDataSet.SubjectsAdapter.GetDataBySubject(subjectName);
              if (subjectsTable.Rows.Count == 0)
              {
                unknownSubjectFound = true;
                missingSubjectName = subjectName;
                continue;
              }

              // Parse subject information
              if (!subjectsTable.Rows[0].IsNull("Age"))
              {
                subjectData.Age = (int)subjectsTable.Rows[0]["Age"];
              }

              subjectData.Category = subjectsTable.Rows[0]["Category"].ToString();
              subjectData.Comments = subjectsTable.Rows[0]["Comments"].ToString();
              subjectData.Handedness = subjectsTable.Rows[0]["Handedness"].ToString();
              subjectData.Sex = subjectsTable.Rows[0]["Sex"].ToString();

              // Parse custom subject information
              subjectParams.Clear();
              DataTable subjectParamsTable = Document.ActiveDocument.DocDataSet.SubjectParametersAdapter.GetDataBySubject(subjectName);
              foreach (DataRow paramRow in subjectParamsTable.Rows)
              {
                subjectParams.Add(paramRow["Param"].ToString(), paramRow["ParamValue"].ToString());
              }

              lastsubjectName = subjectName;
            }
          }

          if (trialID != lastTrialID)
          {
            slideStartTimes.Clear();
            DataTable trialEvents = Document.ActiveDocument.DocDataSet.TrialEventsAdapter.GetDataBySubjectNameTrialSequenceButOnlySlideChangeResponses(subjectName, trialSequence);
            foreach (DataRow slideEventRow in trialEvents.Rows)
            {
              slideStartTimes.Add((long)slideEventRow["EventTime"]);
            }

            countInTrial = 0;
            slideNr = 0;

            if (options.ExportTrialDetails || options.ExportAOIDetails)
            {
              DataTable trialTable = Document.ActiveDocument.DocDataSet.TrialsAdapter.GetDataBySubjectAndTrialID(subjectName, trialID);
              if (trialTable.Rows.Count > 0)
              {
                category = trialTable.Rows[0]["Category"].ToString();
                trialName = trialTable.Rows[0]["TrialName"].ToString();
              }

              string filter = "TrialID=" + trialID + string.Empty;
              trialsAOIsView.RowFilter = filter;
              lastTrialID = trialID;
              trialAOIs.Clear();

              foreach (DataRowView row in trialsAOIsView)
              {
                string strPtList = row["ShapePts"].ToString();
                string aoiType = row["ShapeType"].ToString();
                string aoiName = row["ShapeName"].ToString();
                string shapeGroup = row["ShapeGroup"].ToString();

                VGElement aoi = Queries.GetVGElementFromDatabase(aoiType, aoiName, shapeGroup, strPtList);
                trialAOIs.Add(aoi);
              }
            }
          }

          if (slideStartTimes.Count > (slideNr + 1) && startTime > slideStartTimes[slideNr])
          {
            slideNr++;
          }

          if (options.ExportFixations)
          {
            // Write fixation table content
            foreach (object cellValue in dataRow.Row.ItemArray)
            {
              exportFileWriter.Write(cellValue.ToString());
              exportFileWriter.Write("\t");
            }
          }
          else
          {
            if (countInTrial != 0)
            {
              // we should export saccades
              exportFileWriter.Write(subjectName);
              exportFileWriter.Write("\t");
              exportFileWriter.Write(trialID);
              exportFileWriter.Write("\t");
              exportFileWriter.Write(trialSequence);
              exportFileWriter.Write("\t");
              exportFileWriter.Write(countInTrial);
              exportFileWriter.Write("\t");
              exportFileWriter.Write(lastFixationEndTime);
              exportFileWriter.Write("\t");
              int duration = (int)(startTime - lastFixationEndTime);
              exportFileWriter.Write(duration);
              exportFileWriter.Write("\t");
              float distance = VGPolyline.Distance(lastFixationCenter, fixationCenter);
              exportFileWriter.Write(distance);
              exportFileWriter.Write("\t");
              exportFileWriter.Write(distance / duration);
              exportFileWriter.Write("\t");

              int validity = 0;
              if (duration > lastFixationDuration)
              {
                validity = -1;
              }

              exportFileWriter.Write(validity);
              exportFileWriter.Write("\t");
            }
          }

          if (options.ExportSubjectDetails)
          {
            if (options.ExportFixations || countInTrial != 0)
            {
              // Write subject information
              exportFileWriter.Write(subjectData.Category);
              exportFileWriter.Write("\t");
              exportFileWriter.Write(subjectData.Age);
              exportFileWriter.Write("\t");
              exportFileWriter.Write(subjectData.Sex);
              exportFileWriter.Write("\t");
              exportFileWriter.Write(subjectData.Handedness);
              exportFileWriter.Write("\t");
              exportFileWriter.Write(subjectData.Comments);
              exportFileWriter.Write("\t");

              foreach (string paramValue in subjectParams.Values)
              {
                exportFileWriter.Write(paramValue);
                exportFileWriter.Write("\t");
              }
            }
          }

          if (options.ExportTrialDetails)
          {
            if (options.ExportFixations || countInTrial != 0)
            {
              // Write trial information
              exportFileWriter.Write(trialName != string.Empty ? trialName : "NamelessTrial");
              exportFileWriter.Write("\t");
              exportFileWriter.Write(category);
              exportFileWriter.Write("\t");
              exportFileWriter.Write(slideNr);
              exportFileWriter.Write("\t");
            }
          }

          if (options.ExportAOIDetails)
          {
            if (options.ExportFixations || countInTrial != 0)
            {
              // Retrieve AOI position
              string hittedAOIName = string.Empty;
              string hittedAOIGroup = string.Empty;
              List<string[]> hittedAOIs = Statistics.Statistic.FixationHitsAOI(trialAOIs, dataRow);

              if (hittedAOIs.Count == 0)
              {
                hittedAOIName = "nowhere";
                hittedAOIGroup = "nowhere";
              }

              foreach (string[] aoi in hittedAOIs)
              {
                // Concatenate hitted AOIs
                hittedAOIName += aoi[0] + "#";
                hittedAOIGroup += aoi[1] + "#";
              }

              if (hittedAOIs.Count > 0)
              {
                hittedAOIName = hittedAOIName.Substring(0, hittedAOIName.Length - 1);
                hittedAOIGroup = hittedAOIGroup.Substring(0, hittedAOIGroup.Length - 1);
              }

              exportFileWriter.Write(hittedAOIName);
              exportFileWriter.Write("\t");
              exportFileWriter.Write(hittedAOIGroup);
              exportFileWriter.Write("\t");
            }
          }

          if (options.ExportFixations || countInTrial != 0)
          {
            // Write new line for next row.
            exportFileWriter.WriteLine();
          }

          countInTrial++;
          lastFixationCenter = fixationCenter;
          lastFixationEndTime = startTime + length;
          lastFixationDuration = length;
        }
      }

      if (unknownSubjectFound)
      {
        string message = "At least one fixation of an subject that is not in the database anymore " +
          "was found. Subject: '" + missingSubjectName + "'" + Environment.NewLine +
          "Please re-run a complete fixation calculation for all subjects.";
        ExceptionMethods.ProcessMessage("Old subject fixations found", message);
      }

      ExceptionMethods.ProcessMessage("Export succesful.", "Fixations were successfully exported to file");
    }

    /// <summary>
    /// This method loads the fixations of the given subject in the trial with the
    /// given sequence number from the database and submits them to the picture and
    /// datagridview to be displayed
    /// </summary>
    /// <param name="subjectName">A <see cref="String"/> with the subject name.</param>
    /// <param name="trialSequence">An <see cref="Int32"/> with the trials sequence number
    /// in the subjects recording.</param>
    /// <returns><strong>True</strong> if fixational data could be succesfully loaded,
    /// otherwise <strong>false</strong>.</returns>
    private bool LoadFixations(string subjectName, int trialSequence)
    {
      DataTable gazeFixTable = null;
      DataTable mouseFixTable = null;
      DataTable trialTable = null;

      trialTable = Document.ActiveDocument.DocDataSet.TrialsAdapter.GetDataBySubjectAndSequence(subjectName, trialSequence);

      // Only if there is one trial with the matching conditions
      if (trialTable != null && trialTable.Rows.Count == 1)
      {
        // Import raw Data
        gazeFixTable = Document.ActiveDocument.DocDataSet.GazeFixationsAdapter.GetDataBySubjectAndSequence(subjectName, trialSequence);
        mouseFixTable = Document.ActiveDocument.DocDataSet.MouseFixationsAdapter.GetDataBySubjectAndSequence(subjectName, trialSequence);

        // Update Replayslider
        int duration = (int)trialTable.Rows[0]["Duration"];
        this.trialTimeLine.Duration = duration;
        this.fixationsPicture.SectionEndTime = duration;
        this.trialTimeLine.ResetTimeLine();
      }
      else
      {
        ExceptionMethods.ProcessErrorMessage("The current selection seems not to be valid." +
        Environment.NewLine + "Please try again by selecting a trial from the drop down combo box." +
        "If this error occurs twice, there are maybe two trials with the same ID " + Environment.NewLine +
        "in the database, which is not allowed, try to correct it there.");
        return false;
      }

      if (gazeFixTable != null && gazeFixTable.Rows.Count == 0)
      {
        gazeFixTable = null;
      }

      if (mouseFixTable != null && mouseFixTable.Rows.Count == 0)
      {
        mouseFixTable = null;
      }

      this.fixationsPicture.GazeFixations = gazeFixTable;
      this.fixationsPicture.MouseFixations = mouseFixTable;

      this.UpdatePicturesSampleType();

      this.dgvGazeFixations.DataSource = gazeFixTable;
      this.dgvMouseFixations.DataSource = mouseFixTable;

      string status = string.Empty;
      if (gazeFixTable == null && mouseFixTable == null)
      {
        status += "No gaze or mouse fixations found for this stimulus image.";
      }
      else if (gazeFixTable == null)
      {
        status += "No gaze fixations found for this stimulus image.";
      }
      else if (mouseFixTable == null)
      {
        status += "No mouse fixations found for this stimulus image.";
      }
      else
      {
        status += "Ready ...";
      }

      ((MainForm)this.MdiParent).StatusLabel.Text = status;

      return true;
    }

    /// <summary>
    /// This method commits the currently selected <see cref="SampleType"/>
    /// to the underlying <see cref="VectorGraphics.Canvas.Picture"/>.
    /// </summary>
    private void UpdatePicturesSampleType()
    {
      SampleType sampleType = SampleType.None;
      if (this.btnEye.Checked)
      {
        sampleType |= SampleType.Gaze;
      }

      if (this.btnMouse.Checked)
      {
        sampleType |= SampleType.Mouse;
      }

      this.fixationsPicture.SampleTypeToDraw = sampleType;
    }

    /// <summary>
    /// This method submits the areas of interest of the given trial to 
    /// the underlying <see cref="VectorGraphics.Canvas.Picture"/>.
    /// </summary>
    /// <param name="trialID">The unique trial id of the trial whichs aois should
    /// be displayed.</param>
    private void LoadAOIs(int trialID)
    {
      // Send AOI Data to picture
      if (this.btnShowHideAOI.Checked)
      {
        this.fixationsPicture.AOITable = Document.ActiveDocument.DocDataSet.AOIsAdapter.GetDataByTrialID(trialID);
      }
      else
      {
        this.fixationsPicture.AOITable = null;
      }
    }

    /// <summary>
    /// Starts fixation calculation for every subject in list. 
    /// </summary>
    /// <param name="worker">background worker</param>
    /// <param name="e">do work event arguments</param>
    private void CalculateFixForAllSubjects(BackgroundWorker worker, DoWorkEventArgs e)
    {
      DataTable subjectsTable = Document.ActiveDocument.DocDataSet.SubjectsAdapter.GetData();
      foreach (DataRow subjectRow in subjectsTable.Rows)
      {
        string strSubject = (string)subjectRow["SubjectName"];
        try
        {
          ((MainForm)this.MdiParent).StatusLabel.Text = "Calculation Subject: " + strSubject;
        }
        catch (InvalidOperationException ex)
        {
          ExceptionMethods.HandleExceptionSilent(ex);
        }

        // Get trial data of current subject
        DataTable trialsTable = Document.ActiveDocument.DocDataSet.TrialsAdapter.GetDataBySubject(strSubject);

        FixationCalculation calculationObject = new FixationCalculation();
        calculationObject.CalcFixations(SampleType.Gaze, strSubject, trialsTable, worker, e);
        calculationObject.CalcFixations(SampleType.Mouse, strSubject, trialsTable, worker, e);

        if (e.Cancel)
        {
          break;
        }
      }
    }

    /// <summary>
    /// This method sets a row filter for the fixations depending on the currently
    /// highlighted slide.
    /// </summary>
    private void FilterFixationsBySlide()
    {
      long startTime = this.trialTimeLine.SlideEvents[this.trialTimeLine.HighlightedSlideIndex].Time;
      long endTime = this.trialTimeLine.SlideEvents.Count > this.trialTimeLine.HighlightedSlideIndex + 1 ? this.trialTimeLine.SlideEvents[this.trialTimeLine.HighlightedSlideIndex + 1].Time :
        this.trialTimeLine.SectionEndTime;
      if (this.fixationsPicture.GazeFixationsView != null)
      {
        this.fixationsPicture.GazeFixationsView.RowFilter = "StartTime>=" + startTime + " AND StartTime<" + endTime;
      }

      if (this.fixationsPicture.GazeFixationsView != null)
      {
        this.fixationsPicture.MouseFixationsView.RowFilter = "StartTime>=" + startTime + " AND StartTime<" + endTime;
      }

      this.fixationsPicture.DrawFixations(true);
    }

    #endregion //METHODS

    ///////////////////////////////////////////////////////////////////////////////
    // Small helping Methods                                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region HELPER

    /// <summary>
    /// This method resets the drift correction fields.
    /// </summary>
    private void ResetDriftCorrections()
    {
      // Reset Drift corrections
      this.driftCorrectionLastXValue = 0;
      this.nudDriftXCorrection.Value = 0;
      this.driftCorrectionLastYValue = 0;
      this.nudDriftYCorrection.Value = 0;
    }

    #endregion //HELPER
  }
}