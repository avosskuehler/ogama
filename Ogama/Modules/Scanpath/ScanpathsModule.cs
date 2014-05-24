// <copyright file="ScanpathsModule.cs" company="FU Berlin">
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

namespace Ogama.Modules.Scanpath
{
  using System;
  using System.Collections.Generic;
  using System.Data;
  using System.Drawing;
  using System.IO;
  using System.Windows.Forms;

  using Ogama.ExceptionHandling;
  using Ogama.MainWindow;
  using Ogama.Modules.Common.Controls;
  using Ogama.Modules.Common.FormTemplates;
  using Ogama.Modules.Common.PictureTemplates;
  using Ogama.Modules.Common.SlideCollections;
  using Ogama.Modules.Common.Types;
  using Ogama.Modules.Scanpath.Colorization;
  using Ogama.Properties;

  using OgamaControls;
  using OgamaControls.Dialogs;

  using VectorGraphics.Elements;
  using VectorGraphics.Tools.CustomEventArgs;

  /// <summary>
  /// Derived from <see cref="FormWithTrialSelection"/>.
  /// This <see cref="Form"/> is the scanpath module. 
  /// It handles the UI and the database connection for
  /// the <see cref="ScanpathsPicture"/> class, which is the main element
  /// of this form.
  /// </summary>
  /// <remarks>This interface is intended to visualize different scanpaths
  /// of the subjects that can be grouped and compared.
  /// Also it implements the Levensthein string edit distance calculation
  /// for numeric comparsion of paths.
  /// </remarks>
  public partial class ScanpathsModule : FormWithTrialSelection
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
    /// Saves the currently selected subjects in the treeview.
    /// </summary>
    private SortedDictionary<string, ScanpathProperties> selectedSubjects;

    /// <summary>
    /// Saves the current gaze color to subject assignment.
    /// </summary>
    private ColorizationParameters gazeColorParams;

    /// <summary>
    /// Saves the current mouse color to subject assignment.
    /// </summary>
    private ColorizationParameters mouseColorParams;

    /// <summary>
    /// Flag. Indicates whether Levensthein string distances
    /// should be calculated or not.
    /// </summary>
    private bool calcDistances;

    /// <summary>
    /// Flag indicating whether to show levenshtein distances between each subject
    /// or between groups of subjects.
    /// </summary>
    private bool aggregateDistancesByCategory;

    #endregion //FIELDS

    ///////////////////////////////////////////////////////////////////////////////
    // Construction and Initializing methods                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region CONSTRUCTION

    /// <summary>
    /// Initializes a new instance of the ScanpathsModule class.
    /// </summary>
    public ScanpathsModule()
    {
      // Init
      this.InitializeComponent();

      this.Picture = this.scanpathsPicture;
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
    /// Causes the controls bound to the BindingSources to reread all the 
    /// items in the list and refresh their displayed values. 
    /// </summary>
    public override void ResetDataBindings()
    {
      base.ResetDataBindings();
      this.InitializeSubjectListAndColors();
    }

    /// <summary>
    /// This methods is used to initialize elements that are not
    /// initialized in the designer.
    /// </summary>
    protected override void InitializeCustomElements()
    {
      base.InitializeCustomElements();
      this.InitializeSubjectListAndColors();
      string gazeDisplayModeFromSettings = this.cbbGazeDisplayMode.Text;
      string mouseDisplayModeFromSettings = this.cbbMouseDisplayMode.Text;
      this.cbbGazeDisplayMode.Items.AddRange(Enum.GetNames(typeof(FixationDrawingMode)));
      this.cbbMouseDisplayMode.Items.AddRange(Enum.GetNames(typeof(FixationDrawingMode)));
      this.cbbGazeDisplayMode.SelectedItem = gazeDisplayModeFromSettings;
      this.cbbMouseDisplayMode.SelectedItem = mouseDisplayModeFromSettings;
      this.aggregateDistancesByCategory = false;
      this.btnHelp.Click += new EventHandler(this.btnHelp_Click);
      this.pnlCanvas.Resize += new EventHandler(this.pnlCanvas_Resize);
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
    /// <returns><strong>True</strong> if trial could be successfully loaded,
    /// otherwise <strong>false</strong></returns>
    protected override bool NewTrialSelected()
    {
      try
      {
        // Read current selection state
        int trialID = Document.ActiveDocument.SelectionState.TrialID;

        // Switch to WaitCursor
        this.Cursor = Cursors.WaitCursor;

        // Read settings
        ExperimentSettings set = Document.ActiveDocument.ExperimentSettings;
        if (set != null)
        {
          // Load trial stimulus into picture
          if (!this.LoadTrialStimulus(trialID))
          {
            return false;
          }

          // Load the whole trial data
          if (!this.LoadRawData(trialID))
          {
            return false;
          }

          this.ReReadSelectedSubjects();
        }

        // Reset data state label
        ((MainForm)this.MdiParent).StatusRightLabel.Text = string.Empty;
      }
      catch (Exception ex)
      {
        ExceptionMethods.HandleException(ex);
      }
      finally
      {
        // Reset Cursor
        this.Cursor = Cursors.Default;
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
    /// <see cref="Form.Load"/> event handler. 
    /// Wires Mainform events and initializes vector graphics picture.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void frmFixations_Load(object sender, EventArgs e)
    {
      try
      {
        this.Cursor = Cursors.WaitCursor;

        this.cbbGazeDisplayMode.Text = Properties.Settings.Default.GazeFixationsDrawingMode;
        this.cbbMouseDisplayMode.Text = Properties.Settings.Default.MouseFixationsDrawingMode;

        this.nudGazeFixDiameterDiv.Value = (decimal)Document.ActiveDocument.ExperimentSettings.GazeDiameterDiv;
        this.scanpathsPicture.GazeFixationsDiameterDivisor = Document.ActiveDocument.ExperimentSettings.GazeDiameterDiv;

        this.nudMouseFixDiameterDiv.Value = (decimal)Document.ActiveDocument.ExperimentSettings.MouseDiameterDiv;
        this.scanpathsPicture.MouseFixationsDiameterDivisor = Document.ActiveDocument.ExperimentSettings.MouseDiameterDiv;
        this.scanpathsPicture.GridFactor = (int)this.nudGridFactor.Value;
        this.scanpathsPicture.Progress += new ProgressEventHandler(this.scanpathsPicture_Progress);

        this.scanpathsPicture.PresentationSize = Document.ActiveDocument.PresentationSize;
        this.ResizeCanvas();

        // Hide Levensthein toolstrip 
        this.tosLevenshtein.Visible = false;

        // Hide levenshtein distance table.
        this.spcPicTable.Panel2Collapsed = true;

        // Hide timeline
        this.tscPictureTimeline.BottomToolStripPanelVisible = false;

        this.tosGazeDisplay.Visible = this.btnEye.Checked;
        this.tosMouseDisplay.Visible = this.btnMouse.Checked;
        this.UpdateGrid();

        // Show first stimulus picture
        this.InitialDisplay();

        this.pnlPicture.Bounds = this.GetProportionalBounds(this.pnlCanvas);

        this.Cursor = Cursors.Default;
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
      this.scanpathsPicture.SaveStylesToApplicationSettings();
      this.SaveColorization();
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler for
    /// the <see cref="Button"/> <see cref="btnGrid"/>.
    /// Changes the grid drawing mode and switches the levensthein distance
    /// basis by recalculating the scanpath strings.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void btnGrid_Click(object sender, EventArgs e)
    {
      if (this.btnGrid.Checked && this.btnAOI.Checked)
      {
        this.btnAOI.Checked = false;
      }

      this.UpdateGrid();
      this.ReReadSelectedSubjects();
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler for the
    /// <see cref="Button"/> <see cref="btnAOI"/>.
    /// Changes the grid drawing mode and switches the levensthein distance
    /// basis by recalculating the scanpath strings.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void btnAOI_Click(object sender, EventArgs e)
    {
      if (this.btnGrid.Checked && this.btnAOI.Checked)
      {
        this.btnGrid.Checked = false;
      }

      this.UpdateGrid();
      this.ReReadSelectedSubjects();
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler for
    /// the <see cref="Button"/> <see cref="btnRemoveSubsequentHits"/>.
    /// Changes the flag indicating whether to remove subsequent fixations 
    /// in the same grid element when calculating the edit distances.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void btnRemoveSubsequentHits_Click(object sender, EventArgs e)
    {
      this.scanpathsPicture.IgnoreSubsequentFixations = this.btnRemoveSubsequentHits.Checked;
      this.ReReadSelectedSubjects();
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler for
    /// the <see cref="Button"/> <see cref="btnGroupLevenshteinOutput"/>.
    /// Changes the flag indicating whether to aggregate 
    /// levenshtein distances by groups and invokes recalculation of distances.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void btnGroupLevenshteinOutput_Click(object sender, EventArgs e)
    {
      this.aggregateDistancesByCategory = this.btnGroupLevenshteinOutput.Checked;
      this.ReReadSelectedSubjects();
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
      this.scanpathsPicture.GazeDrawingMode = (FixationDrawingMode)Enum.Parse(
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
      this.scanpathsPicture.MouseDrawingMode = (FixationDrawingMode)Enum.Parse(
        typeof(FixationDrawingMode),
        (string)this.cbbMouseDisplayMode.SelectedItem);
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
      this.scanpathsPicture.GazeFixationsDiameterDivisor = (float)this.nudGazeFixDiameterDiv.Value;
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
      this.scanpathsPicture.MouseFixationsDiameterDivisor = (float)this.nudMouseFixDiameterDiv.Value;
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
      ColorDefinitionDialog dlg = new ColorDefinitionDialog(this.scanpathsPicture.GazeDrawingMode);
      dlg.SubjectNodes = this.trvSubjects.Nodes;
      dlg.ColorParams = this.gazeColorParams;

      if (dlg.ShowDialog() == DialogResult.OK)
      {
        this.gazeColorParams = dlg.ColorParams;
        this.trvSubjects.Refresh();
        this.ReReadSelectedSubjects();
        this.scanpathsPicture.Subjects = this.selectedSubjects;

        // Drawing properties changed, so redraw picture
        this.scanpathsPicture.DrawFixations(true);
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
      ColorDefinitionDialog dlg = new ColorDefinitionDialog(this.scanpathsPicture.MouseDrawingMode);
      dlg.SubjectNodes = this.trvSubjects.Nodes;
      dlg.ColorParams = this.mouseColorParams;

      if (dlg.ShowDialog() == DialogResult.OK)
      {
        this.mouseColorParams = dlg.ColorParams;
        this.trvSubjects.Refresh();
        this.ReReadSelectedSubjects();
        this.scanpathsPicture.Subjects = this.selectedSubjects;

        // Drawing properties changed, so redraw picture
        this.scanpathsPicture.DrawFixations(true);
      }
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

      this.tosGazeDisplay.Visible = this.btnEye.Checked;
      this.tosMouseDisplay.Visible = this.btnMouse.Checked;
      this.NewTrialSelected();
      this.trvSubjects.Invalidate();
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

      this.tosGazeDisplay.Visible = this.btnEye.Checked;
      this.tosMouseDisplay.Visible = this.btnMouse.Checked;
      this.NewTrialSelected();
      this.trvSubjects.Invalidate();
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
      this.scanpathsPicture.GazeConnections = this.btnGazeConnections.Checked;
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
      this.scanpathsPicture.GazeNumbers = this.btnGazeNumbers.Checked;
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
      this.scanpathsPicture.MouseConnections = this.btnMouseConnections.Checked;
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
      this.scanpathsPicture.MouseNumbers = this.btnMouseNumbers.Checked;
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler for the
    /// <see cref="Button"/> <see cref="btnExport"/>.
    /// Raise the export dialog and exports the referring table.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void btnExport_Click(object sender, EventArgs e)
    {
      ExportDialog dlg = new ExportDialog();
      if (dlg.ShowDialog() == DialogResult.OK)
      {
        if (this.sfdSimiliarity.ShowDialog() == DialogResult.OK)
        {
          string filename = this.sfdSimiliarity.FileName;
          using (StreamWriter exportFileWriter = new StreamWriter(filename))
          {
            // Write Documentation
            exportFileWriter.WriteLine("# File: " + Path.GetFileName(filename));
            exportFileWriter.WriteLine("# Created: " + DateTime.Today.Date.ToLongDateString() + "," + DateTime.Now.ToLongTimeString());
            exportFileWriter.WriteLine("# with: " + Application.ProductName + " Version: " + Document.ActiveDocument.ExperimentSettings.OgamaVersion.ToString(3));
            exportFileWriter.WriteLine("# Contents: Similarity Measurements of scanpaths.");
            exportFileWriter.WriteLine("# Applies to Projekt:" + Document.ActiveDocument.ExperimentSettings.Name);
            exportFileWriter.WriteLine("#");

            if (dlg.ExportSimilarityForAllTrials)
            {
              DataGridView usedGridView = null;
              if (dlg.ExportLociSimilarity)
              {
                exportFileWriter.WriteLine("# The following is the table of loci similarity measurements of the scanpaths");
                exportFileWriter.WriteLine("#");
                usedGridView = this.dgvLociSimilarity;
              }

              if (dlg.ExportSequenceSimilarity)
              {
                exportFileWriter.WriteLine("# The following is the table of sequence similarity measurements of the scanpaths");
                exportFileWriter.WriteLine("#");
                usedGridView = this.dgvSequenceSimilarity;
              }

              if (usedGridView == null)
              {
                return;
              }

              // Write Column Names
              List<string> columnNames = new List<string>();
              for (int i = 0; i < usedGridView.Columns.Count; i++)
              {
                // Skip first column
                if (i == 0)
                {
                  continue;
                }

                if (!this.aggregateDistancesByCategory && i > usedGridView.Columns.Count - 3)
                {
                  continue;
                }

                columnNames.Add(usedGridView.Columns[i].HeaderText);
              }

              exportFileWriter.Write("TrialID\t");
              exportFileWriter.Write("TrialName\t");

              for (int i = 0; i < columnNames.Count; i++)
              {
                for (int j = 0; j < columnNames.Count; j++)
                {
                  // Skip entries below the diagonal
                  if (i > j)
                  {
                    continue;
                  }

                  string newColumnName = columnNames[i] + "-" + columnNames[j] + "\t";
                  exportFileWriter.Write(newColumnName);
                }
              }

              exportFileWriter.WriteLine();

              List<Trial> trials = Document.ActiveDocument.ExperimentSettings.SlideShow.Trials;

              foreach (Trial trialRow in trials)
              {
                exportFileWriter.Write(trialRow.ID.ToString() + "\t");
                exportFileWriter.Write(trialRow.Name + "\t");

                // Load the trial data
                this.LoadFixationsIntoPicture(trialRow.ID);

                // Send AOI Data to picture
                this.scanpathsPicture.AOITable =
                  Document.ActiveDocument.DocDataSet.AOIsAdapter.GetDataByTrialID(trialRow.ID);

                // Looks for subjects and calculates the distances
                this.ReReadSelectedSubjects();

                // Now that the datagrids where filled with the distances
                // parse through the table

                // Write Data To File
                for (int i = 0; i < usedGridView.Rows.Count; i++)
                {
                  for (int j = 0; j < usedGridView.Columns.Count; j++)
                  {
                    // Skip first column
                    if (j == 0)
                    {
                      continue;
                    }

                    // Skip entries below the diagonal
                    if (i >= j)
                    {
                      continue;
                    }

                    if (!this.aggregateDistancesByCategory && j > usedGridView.Columns.Count - 3)
                    {
                      continue;
                    }

                    exportFileWriter.Write(usedGridView[j, i].Value);
                    exportFileWriter.Write("\t");
                  }
                }

                exportFileWriter.WriteLine();
              }
            }
            else
            {
              if (dlg.ExportLociSimilarity)
              {
                exportFileWriter.WriteLine("# The following part is the loci similarity of the scanpaths");
                exportFileWriter.WriteLine("#");
                this.WriteDataGridToFile(this.dgvLociSimilarity, exportFileWriter);
              }

              if (dlg.ExportSequenceSimilarity)
              {
                exportFileWriter.WriteLine("# The following part is the sequence similarity of the scanpaths");
                exportFileWriter.WriteLine("#");
                this.WriteDataGridToFile(this.dgvSequenceSimilarity, exportFileWriter);
              }
            }
          }
        }
      }
    }

    /// <summary>
    /// The <see cref="TreeView.AfterCheck"/> event handler for the
    /// <see cref="TreeView"/> <see cref="trvSubjects"/>.
    /// Checks or unchecks all subjects in the category node
    /// that is clicked.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void trvSubjects_AfterCheck(object sender, TreeViewEventArgs e)
    {
      // The code only executes if the user caused the checked state to change.
      if (e.Action != TreeViewAction.Unknown)
      {
        this.trvSubjects.BeginUpdate();
        if (e.Node.Nodes.Count > 0)
        {
          /* Calls the CheckAllChildNodes method, passing in the current 
          Checked value of the TreeNode whose checked state changed. */
          this.CheckAllChildNodes(e.Node, e.Node.Checked);
        }

        if (e.Node.ImageKey == "Category")
        {
          foreach (TreeNode subjectNode in e.Node.Nodes)
          {
            string name = subjectNode.Text;
            this.AddSubjectToList(e.Node, name);
          }
        }
        else
        {
          string name = e.Node.Text;
          this.AddSubjectToList(e.Node, name);
        }

        this.scanpathsPicture.Subjects = this.selectedSubjects;

        // Recalculate distances
        this.RefillDistancesTable();

        // Drawing properties changed, so redraw picture
        this.scanpathsPicture.DrawFixations(true);

        this.trvSubjects.EndUpdate();
      }
    }

    /// <summary>
    /// The <see cref="TreeView.DrawNode"/> event handler for the
    /// <see cref="TreeView"/> <see cref="trvSubjects"/>.
    /// Draws the subjects entrys with a color rectangle.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">A <see cref="DrawTreeNodeEventArgs"/> with the event data.</param>
    private void trvSubjects_DrawNode(object sender, DrawTreeNodeEventArgs e)
    {
      e.DrawDefault = true;

      Rectangle gazeColorBounds = e.Bounds;
      gazeColorBounds.Offset(gazeColorBounds.Width, 0);

      Rectangle mouseColorBounds = e.Bounds;
      if (this.btnEye.Checked && this.btnMouse.Checked)
      {
        mouseColorBounds.Offset(mouseColorBounds.Width + 22, 0);
      }
      else if (!this.btnEye.Checked)
      {
        mouseColorBounds.Offset(mouseColorBounds.Width, 0);
      }

      if (this.btnEye.Checked)
      {
        ColorDefinitionDialog.DrawNodes(e, this.gazeColorParams, gazeColorBounds);
      }

      if (this.btnMouse.Checked)
      {
        ColorDefinitionDialog.DrawNodes(e, this.mouseColorParams, mouseColorBounds);
      }
    }

    /// <summary>
    /// The <see cref="NumericUpDown.ValueChanged"/> event handler for the
    /// <see cref="NumericUpDown"/> <see cref="nudGridFactor"/>.
    /// Updates the grid division factor and redraws the fixations including the grid.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void nudGridFactor_ValueChanged(object sender, EventArgs e)
    {
      this.scanpathsPicture.GridFactor = (int)this.nudGridFactor.Value;

      this.scanpathsPicture.DrawFixations(true);
      this.ReReadSelectedSubjects();
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler for the
    /// <see cref="Button"/> <see cref="btnLevenstheinToolbar"/>.
    /// Enables or disables the levensthein edit distance calculation tools.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void btnLevenstheinToolbar_Click(object sender, EventArgs e)
    {
      if (this.btnLevenstheinToolbar.Checked)
      {
        this.calcDistances = true;
        this.tosLevenshtein.Visible = true;
        this.spcPicTable.Panel2Collapsed = false;
        this.UpdateGrid();
        this.ReReadSelectedSubjects();
      }
      else
      {
        this.calcDistances = false;
        this.tosLevenshtein.Visible = false;
        this.spcPicTable.Panel2Collapsed = true;
        this.scanpathsPicture.GridBasis = GridBase.None;
        this.scanpathsPicture.DrawFixations(true);
      }
    }

    /// <summary>
    /// The <see cref="ToolStripButton.CheckedChanged"/> event handler for the
    /// <see cref="ToolStripButton"/>s <see cref="btnLociSimilarity"/> and
    /// <see cref="btnSequenceSimilarity"/>.
    /// Enables or disables the corresponding similarity calculation.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void btnSimilarity_CheckedChanged(object sender, EventArgs e)
    {
      if (!this.btnLociSimilarity.Checked && !this.btnSequenceSimilarity.Checked)
      {
        this.btnSequenceSimilarity.Checked = true;
      }

      this.spcTables.Panel1Collapsed = !this.btnLociSimilarity.Checked;
      this.spcTables.Panel2Collapsed = !this.btnSequenceSimilarity.Checked;
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
      if (this.trialTimeLine.SectionEndTime == this.trialTimeLine.CurrentTime)
      {
        this.ResetControls();
      }

      if (this.trialTimeLine.CurrentTime == this.trialTimeLine.SectionStartTime)
      {
        this.scanpathsPicture.ResetPicture();
      }

      // Paranoia check for empty trials.
      if (this.trialTimeLine.SectionEndTime == this.trialTimeLine.SectionStartTime)
      {
        return;
      }

      // Disable the Start and rewind button
      this.btnStart.Enabled = false;
      this.btnRewind.Enabled = false;

      // Enable the stop button while 
      // the operation runs.
      this.btnStop.Enabled = true;

      // Update pictures properties
      this.scanpathsPicture.AnimationStartTime =
        DateTime.Now.AddMilliseconds(-this.trialTimeLine.CurrentTime).Ticks;

      // Start the timer updating operation.
      this.scanpathsPicture.StartAnimation();
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler for the
    /// <see cref="Button"/> <see cref="btnStop"/>.
    /// User pressed stop button. So stop animation method.
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
    /// <see cref="Button"/> <see cref="btnTimeLine"/>.
    /// Enables or disables replay tools for this module.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void btnTimeLine_Click(object sender, EventArgs e)
    {
      this.nudTimeSpan.Visible = this.btnTimeLine.Checked;
      this.tscPictureTimeline.BottomToolStripPanelVisible = this.btnTimeLine.Checked;
      this.scanpathsPicture.ResetTimeSpan(this.btnTimeLine.Checked);
      this.ResetControls();
      this.scanpathsPicture.DrawFixations(true);
    }

    #endregion //WINDOWSEVENTHANDLER

    ///////////////////////////////////////////////////////////////////////////////
    // Eventhandler for Custom Defined Events                                    //
    ///////////////////////////////////////////////////////////////////////////////
    #region CUSTOMEVENTHANDLER

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
      this.scanpathsPicture.PenChanged(sender, e);
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
      this.scanpathsPicture.FontStyleChanged(sender, e);
    }

    /// <summary>
    /// The <see cref="VectorGraphics.Canvas.Picture.Progress"/> event handler for
    /// the <see cref="VectorGraphics.Canvas.Picture"/> <see cref="scanpathsPicture"/>.
    /// This method updates time slider caret and stops
    /// the animation if the trial has finished.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">A <see cref="ProgressEventArgs"/>with time and status</param>
    private void scanpathsPicture_Progress(object sender, ProgressEventArgs e)
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
      }
    }

    /// <summary>
    /// The <see cref="TimeLine.SectionStartValueChanged"/> event handler for the
    /// <see cref="TrialTimeLine"/> <see cref="trialTimeLine"/>.
    /// Sets <see cref="VectorGraphics.Canvas.Picture.SectionStartTime"/> and redraws fixations.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">A <see cref="RangeTrackBar.PositionValueChangedEventArguments"/> with the event data.</param>
    private void TrialTimeLine_SectionStartValueChanged(object sender, TimeLine.PositionValueChangedEventArguments e)
    {
      this.scanpathsPicture.SectionStartTime = e.Millisecond;
    }

    /// <summary>
    /// The <see cref="TimeLine.SectionEndValueChanged"/> event handler for the
    /// <see cref="TrialTimeLine"/> <see cref="trialTimeLine"/>.
    /// Sets <see cref="VectorGraphics.Canvas.Picture.SectionEndTime"/> and redraws fixations.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">A <see cref="RangeTrackBar.PositionValueChangedEventArguments"/> with the event data.</param>
    private void TrialTimeLine_SectionEndValueChanged(object sender, TimeLine.PositionValueChangedEventArguments e)
    {
      this.scanpathsPicture.SectionEndTime = e.Millisecond;
    }

    /// <summary>
    /// The <see cref="TimeLine.CaretValueChanged"/> event handler for the
    /// <see cref="TrialTimeLine"/> <see cref="trialTimeLine"/>.
    /// Updates picture.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">A <see cref="RangeTrackBar.PositionValueChangedEventArguments"/> with the event data.</param>
    private void TrialTimeLine_CaretValueChanged(object sender, TimeLine.PositionValueChangedEventArguments e)
    {
      // Draw a portion of the time scale around the caret calue
      this.scanpathsPicture.DrawTimeSection(e.Millisecond, (int)this.nudTimeSpan.Value);
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
    /// Initialize the subject list with custom coloring.
    /// </summary>
    private void InitializeSubjectListAndColors()
    {
      this.selectedSubjects = new SortedDictionary<string, ScanpathProperties>();

      List<Color> gazeColorTable = new List<Color>();
      List<Color> mouseColorTable = new List<Color>();
      Font gazeFont = new Font("Arial", 28f);
      Font mouseFont = new Font("Arial", 28f);

      ColorizationStyle gazeStyle = new ColorizationStyle(
        new Pen(Color.Yellow),
        new Pen(Color.Yellow),
        gazeFont,
        Color.Yellow);

      ColorizationStyle mouseStyle = new ColorizationStyle(
        new Pen(Color.LightYellow),
        new Pen(Color.LightYellow),
        mouseFont,
        Color.LightYellow);

      DataView subjectsView = new DataView(Document.ActiveDocument.DocDataSet.Subjects);
      subjectsView.Sort = "SubjectName ASC";

      // Check for saved params in the experiment settings
      // if they are empty or not as much as subjects in
      // the current database, refresh all params.
      bool refreshGazeColorParams = false;
      if (Document.ActiveDocument.ExperimentSettings.GazeColorParams != null
        && Document.ActiveDocument.ExperimentSettings.GazeColorParams.SubjectStyles.Count != subjectsView.Count)
      {
        refreshGazeColorParams = true;
      }

      if (!refreshGazeColorParams)
      {
        this.gazeColorParams = (ColorizationParameters)Document.ActiveDocument.ExperimentSettings.GazeColorParams.Clone();
      }
      else
      {
        this.gazeColorParams = new ColorizationParameters();
        this.gazeColorParams.ColorizationMode = ColorizationModes.Category;

        gazeColorTable.Add(Color.Red);
        gazeColorTable.Add(Color.Blue);
        gazeColorTable.Add(Color.Brown);
        gazeColorTable.Add(Color.Green);
        gazeColorTable.Add(Color.Cyan);
        gazeColorTable.Add(Color.Orange);
        gazeColorTable.Add(Color.Coral);

        this.gazeColorParams.CategoryStyles.Add("no category", gazeStyle);
      }

      // Check for saved params in the experiment settings
      // if they are empty or not as much as subjects in
      // the current database, refresh all params.
      bool refreshMouseColorParams = false;
      if (Document.ActiveDocument.ExperimentSettings.MouseColorParams != null
        && Document.ActiveDocument.ExperimentSettings.MouseColorParams.SubjectStyles.Count != subjectsView.Count)
      {
        refreshMouseColorParams = true;
      }

      if (!refreshMouseColorParams)
      {
        this.mouseColorParams = (ColorizationParameters)Document.ActiveDocument.ExperimentSettings.MouseColorParams.Clone();
      }
      else
      {
        this.mouseColorParams = new ColorizationParameters();
        this.mouseColorParams.ColorizationMode = ColorizationModes.Category;

        mouseColorTable.Add(Color.Tomato);
        mouseColorTable.Add(Color.LightBlue);
        mouseColorTable.Add(Color.BurlyWood);
        mouseColorTable.Add(Color.LightGoldenrodYellow);
        mouseColorTable.Add(Color.LightCyan);
        mouseColorTable.Add(Color.LightSalmon);
        mouseColorTable.Add(Color.LightCoral);

        this.mouseColorParams.CategoryStyles.Add("no category", mouseStyle);
      }

      this.trvSubjects.BeginUpdate();
      this.trvSubjects.Nodes.Clear();

      // Populates a TreeView control with subject category nodes. 
      int categoryCounter = 0;
      foreach (DataRowView row in subjectsView)
      {
        string category = row["Category"].ToString();
        if (category == string.Empty || category == " ")
        {
          category = "no category";
        }

        if (!this.trvSubjects.Nodes.ContainsKey(category))
        {
          this.trvSubjects.Nodes.Add(category, category, "Category");

          if (refreshGazeColorParams)
          {
            Color gazeColor = gazeColorTable[categoryCounter];
            gazeStyle = new ColorizationStyle(new Pen(gazeColor), new Pen(gazeColor), gazeFont, gazeColor);
            if (!this.gazeColorParams.CategoryStyles.ContainsKey(category))
            {
              this.gazeColorParams.CategoryStyles.Add(category, gazeStyle);
            }
          }

          if (refreshMouseColorParams)
          {
            Color mouseColor = mouseColorTable[categoryCounter];
            mouseStyle = new ColorizationStyle(new Pen(mouseColor), new Pen(mouseColor), mouseFont, mouseColor);
            if (!this.mouseColorParams.CategoryStyles.ContainsKey(category))
            {
              this.mouseColorParams.CategoryStyles.Add(category, mouseStyle);
            }
          }

          categoryCounter++;
          if (categoryCounter > gazeColorTable.Count)
          {
            categoryCounter = 0;
          }
        }
      }

      // Populates a TreeView control with subject nodes. 
      foreach (DataRowView row in subjectsView)
      {
        string category = row["Category"].ToString();
        if (category == string.Empty || category == " ")
        {
          category = "no category";
        }

        string subjectName = row["SubjectName"].ToString();
        this.trvSubjects.Nodes[category].Nodes.Add(subjectName, subjectName, "Subject", "Subject");
      }

      this.trvSubjects.ExpandAll();

      if (refreshGazeColorParams)
      {
        this.gazeColorParams.SubjectStyles.Clear();
        foreach (TreeNode categoryNode in this.trvSubjects.Nodes)
        {
          foreach (TreeNode subjectNode in categoryNode.Nodes)
          {
            this.gazeColorParams.SubjectStyles.Add(
              subjectNode.Text,
              this.gazeColorParams.CategoryStyles[categoryNode.Text]);
          }
        }
      }

      if (refreshMouseColorParams)
      {
        this.mouseColorParams.SubjectStyles.Clear();
        foreach (TreeNode categoryNode in this.trvSubjects.Nodes)
        {
          foreach (TreeNode subjectNode in categoryNode.Nodes)
          {
            this.mouseColorParams.SubjectStyles.Add(
              subjectNode.Text,
              this.mouseColorParams.CategoryStyles[categoryNode.Text]);
          }
        }
      }

      this.trvSubjects.EndUpdate();
      foreach (TreeNode categoryNode in this.trvSubjects.Nodes)
      {
        categoryNode.Checked = true;
        this.CheckAllChildNodes(categoryNode, true);
      }

      this.ReReadSelectedSubjects();
    }

    /// <summary>
    /// This method loads the raw data for the trial 
    /// given by id and submits it to the underlying picture.
    /// </summary>
    /// <param name="trialID">An <see cref="Int32"/> with the trial id to 
    /// be selected.</param>
    /// <returns><strong>True</strong> if raw data could be successfully found and loaded,
    /// otherwise <strong>false</strong>.</returns>
    private bool LoadRawData(int trialID)
    {
      SampleType sampleType = this.LoadFixationsIntoPicture(trialID);

      // Send AOI Data to picture
      this.scanpathsPicture.AOITable =
        Document.ActiveDocument.DocDataSet.AOIsAdapter.GetDataByTrialID(trialID);

      // Notify the drawing type 
      this.scanpathsPicture.SampleTypeToDraw = sampleType;

      // Notify the subjects that should be drawn
      this.scanpathsPicture.Subjects = this.selectedSubjects;

      if ((sampleType == (sampleType | SampleType.Gaze) &&
        Document.ActiveDocument.DocDataSet.GazeFixations.Count > 0)
        ||
        (sampleType == (sampleType | SampleType.Mouse) &&
        Document.ActiveDocument.DocDataSet.MouseFixations.Count > 0))
      {
        // Update Timeslider
        object objMaxGazeStartTime = Document.ActiveDocument.DocDataSet.GazeFixationsAdapter.GetMaxStartTimeOfTrial(trialID);
        int maxGazeStartTime = Convert.ToInt32(objMaxGazeStartTime);
        object objMaxMouseStartTime = Document.ActiveDocument.DocDataSet.MouseFixationsAdapter.GetMaxStartTimeOfTrial(trialID);
        int maxMouseStartTime = Convert.ToInt32(objMaxMouseStartTime);
        int maxTime = Math.Max(maxGazeStartTime, maxMouseStartTime);
        this.trialTimeLine.Duration = maxTime;
        this.scanpathsPicture.SectionEndTime = maxTime;
        this.scanpathsPicture.ResetTimeSpan(false);
        this.trialTimeLine.ResetTimeLine();

        // There is something to draw, so call DrawFixations()
        this.scanpathsPicture.DrawFixations(true);
      }
      else
      {
        return false;
      }

      return true;
    }

    /// <summary>
    /// This method retrieves the fixations for the current selected
    /// SampleType from the database and sends them to the scanpath picture.
    /// It returns the parsed <see cref="SampleType"/>
    /// </summary>
    /// <param name="trialID">An <see cref="Int32"/> with the trial ID.</param>
    /// <returns>The <see cref="SampleType"/> to be used.</returns>
    private SampleType LoadFixationsIntoPicture(int trialID)
    {
      // Set sample type flags and load fixation tables from database
      // into picture fields.
      SampleType sampleType = SampleType.None;
      if (this.btnEye.Checked)
      {
        sampleType |= SampleType.Gaze;
        this.scanpathsPicture.GazeFixations =
          Document.ActiveDocument.DocDataSet.GazeFixationsAdapter.GetDataByTrialID(trialID);
      }

      if (this.btnMouse.Checked)
      {
        sampleType |= SampleType.Mouse;
        this.scanpathsPicture.MouseFixations =
         Document.ActiveDocument.DocDataSet.MouseFixationsAdapter.GetDataByTrialID(trialID);
      }

      return sampleType;
    }

    /// <summary>
    /// This method writes the whole data from the given <see cref="DataGridView"/>
    /// into the given <see cref="StreamWriter"/> object.
    /// </summary>
    /// <param name="dgv">A <see cref="DataGridView"/> which contents should be exported.</param>
    /// <param name="sw">Ref. A <see cref="StreamWriter"/> that should be written to.</param>
    private void WriteDataGridToFile(DataGridView dgv, StreamWriter sw)
    {
      // Write Column Names
      foreach (DataGridViewColumn dataColumn in dgv.Columns)
      {
        sw.Write(dataColumn.HeaderText);
        sw.Write("\t");
      }

      sw.WriteLine();

      // Write Data To File
      foreach (DataGridViewRow dataRow in dgv.Rows)
      {
        foreach (DataGridViewCell cell in dataRow.Cells)
        {
          sw.Write(cell.Value);
          sw.Write("\t");
        }

        sw.WriteLine();
      }
    }

    /// <summary>
    /// This method calculates the edit distances for all selected subjects
    /// scanpath with the current settings and populates the data grid views
    /// with the Loci and Sequence similarity matrices.
    /// </summary>
    private void RefillDistancesTable()
    {
      // Clear Existing Rows and Columns
      this.dgvLociSimilarity.Rows.Clear();
      this.dgvLociSimilarity.Columns.Clear();
      this.dgvSequenceSimilarity.Rows.Clear();
      this.dgvSequenceSimilarity.Columns.Clear();

      if (!this.calcDistances)
      {
        return;
      }

      // Skip if no data available
      if (this.scanpathsPicture.GazeFixations == null)
      {
        return;
      }

      if (this.aggregateDistancesByCategory)
      {
        var categories = new List<string>();

        // Create loci similarity matrix
        int rows = this.selectedSubjects.Count;
        Array lociSimilarities = new DistanceEntry[rows, rows];
        Array globalSimilarities = new DistanceEntry[rows, rows];

        DataTable fixations = this.scanpathsPicture.GazeFixations;
        var fixationsView = new DataView(
           fixations,
          string.Empty,
          "CountInTrial",
          DataViewRowState.CurrentRows);

        DataTable mainTable = Document.ActiveDocument.DocDataSet.SubjectsAdapter.GetData();

        foreach (DataRow row in mainTable.Rows)
        {
          string entry = row["Category"].ToString();
          if (entry == string.Empty || entry == " ")
          {
            entry = "no category";
          }

          if (!categories.Contains(entry))
          {
            categories.Add(entry);
          }
        }

        int rowCounter = 0;
        foreach (KeyValuePair<string, ScanpathProperties> subjectRowKVP in this.selectedSubjects)
        {
          int columnCounter = 0;
          ////DataView fixationsOfS = new DataView(
          ////  fixations,
          ////  "(SubjectName='" + subjectRowKVP.Value.SubjectName + "')",
          //// "CountInTrial",
          //// DataViewRowState.CurrentRows);

          foreach (KeyValuePair<string, ScanpathProperties> subjectColumnKVP in this.selectedSubjects)
          {
            ////DataView fixationsOfT = new DataView(
            ////  fixations,
            ////  "(SubjectName='" + subjectColumnKVP.Value.SubjectName + "')",
            //// "CountInTrial",
            //// DataViewRowState.CurrentRows);

            ////float malsburgSimilarity = EditDistance.MalsburgDistance(sFixations, tFixations, 0.5f);
            List<string> rowStringList = GetStringListForExtendedString(subjectRowKVP.Value.ScanpathString);
            List<string> columnStringList = GetStringListForExtendedString(subjectColumnKVP.Value.ScanpathString);
            float localSimilarity = EditDistance.CharacterSimilarityExtended(
               rowStringList,
               columnStringList) * 100;
            float globalSimilarity = EditDistance.SequenceSimilarityExtended(
               rowStringList,
               columnStringList) * 100;

            lociSimilarities.SetValue(
              new DistanceEntry(
              categories.IndexOf(subjectRowKVP.Value.SubjectCategory),
              categories.IndexOf(subjectColumnKVP.Value.SubjectCategory),
              localSimilarity),
              rowCounter,
              columnCounter);
            globalSimilarities.SetValue(
              new DistanceEntry(
              categories.IndexOf(subjectRowKVP.Value.SubjectCategory),
              categories.IndexOf(subjectColumnKVP.Value.SubjectCategory),
              globalSimilarity),
              rowCounter,
              columnCounter);
            columnCounter++;
          }

          rowCounter++;
        }

        int mergedRows = categories.Count;
        Array mergedLociSimilarities = new CategoryEntry[mergedRows, mergedRows];
        Array mergedGlobalSimilarities = new CategoryEntry[mergedRows, mergedRows];

        // Merge categories
        for (int i = 0; i <= lociSimilarities.GetUpperBound(0); i++)
        {
          for (int j = 0; j <= lociSimilarities.GetUpperBound(1); j++)
          {
            // Only take values above the diagonal
            if (i >= j)
            {
              continue;
            }

            DistanceEntry currentLociValue = (DistanceEntry)lociSimilarities.GetValue(i, j);
            CategoryEntry currentCategoryValue = (CategoryEntry)mergedLociSimilarities.GetValue(currentLociValue.RowCategory, currentLociValue.ColumnCategory);
            currentCategoryValue.Divisor++;
            CategoryEntry newCategoryValue = new CategoryEntry(currentCategoryValue.Divisor, currentLociValue.DistanceValue + currentCategoryValue.DistanceValue);
            mergedLociSimilarities.SetValue(newCategoryValue, currentLociValue.RowCategory, currentLociValue.ColumnCategory);
            mergedLociSimilarities.SetValue(newCategoryValue, currentLociValue.ColumnCategory, currentLociValue.RowCategory);

            DistanceEntry currentGlobalValue = (DistanceEntry)globalSimilarities.GetValue(i, j);
            CategoryEntry currentGlobalCategoryValue = (CategoryEntry)mergedGlobalSimilarities.GetValue(currentGlobalValue.RowCategory, currentGlobalValue.ColumnCategory);
            currentGlobalCategoryValue.Divisor++;
            CategoryEntry newGlobalCategoryValue = new CategoryEntry(currentGlobalCategoryValue.Divisor, currentGlobalValue.DistanceValue + currentGlobalCategoryValue.DistanceValue);
            mergedGlobalSimilarities.SetValue(newGlobalCategoryValue, currentGlobalValue.RowCategory, currentGlobalValue.ColumnCategory);
            mergedGlobalSimilarities.SetValue(newGlobalCategoryValue, currentGlobalValue.ColumnCategory, currentGlobalValue.RowCategory);
          }
        }

        this.dgvLociSimilarity.Columns.Add("columnLociSubjects", "Loci Similarity");
        this.dgvSequenceSimilarity.Columns.Add("columnSequenceSubjects", "Sequence Similarity");
        foreach (string category in categories)
        {
          this.dgvLociSimilarity.Columns.Add(category, category);
          this.dgvSequenceSimilarity.Columns.Add(category, category);
        }

        // Ouput category distances
        for (int i = 0; i <= mergedLociSimilarities.GetUpperBound(0); i++)
        {
          var lociSimilarity = new List<object>();
          var sequenceSimilarity = new List<object>();
          for (int j = 0; j <= mergedLociSimilarities.GetUpperBound(1); j++)
          {
            CategoryEntry currentCategoryValue = (CategoryEntry)mergedLociSimilarities.GetValue(i, j);
            lociSimilarity.Add(currentCategoryValue.DistanceValue / currentCategoryValue.Divisor);

            CategoryEntry currentGlobalCategoryValue = (CategoryEntry)mergedGlobalSimilarities.GetValue(i, j);
            sequenceSimilarity.Add(currentGlobalCategoryValue.DistanceValue / currentGlobalCategoryValue.Divisor);
          }

          var lociColumns = new List<object> { categories[i] };
          lociColumns.AddRange(lociSimilarity.ToArray());
          this.dgvLociSimilarity.Rows.Add(lociColumns.ToArray());

          var sequenceColumns = new List<object> { categories[i] };
          sequenceColumns.AddRange(sequenceSimilarity.ToArray());
          this.dgvSequenceSimilarity.Rows.Add(sequenceColumns.ToArray());
        }
      }
      else
      {
        this.dgvLociSimilarity.Columns.Add("columnLociSubjects", "Loci Similarity");
        this.dgvSequenceSimilarity.Columns.Add("columnSequenceSubjects", "Sequence Similarity");
        foreach (string subject in this.selectedSubjects.Keys)
        {
          this.dgvLociSimilarity.Columns.Add(subject, subject);
          this.dgvSequenceSimilarity.Columns.Add(subject, subject);
        }

        this.dgvLociSimilarity.Columns.Add("columnLociString", "Scanpath string");
        this.dgvSequenceSimilarity.Columns.Add("columnSequenceString", "Scanpath string");
        this.dgvLociSimilarity.Columns.Add("columnLociSubjectGroup", "Subject Group");
        this.dgvSequenceSimilarity.Columns.Add("columnSequenceSubjectGroup", "Subject Group");

        DataTable fixations = this.scanpathsPicture.GazeFixations;
        ////DataView fixationsView = new DataView(
        ////   fixations,
        ////  string.Empty,
        ////  "CountInTrial",
        ////  DataViewRowState.CurrentRows);

        foreach (KeyValuePair<string, ScanpathProperties> subjectRowKVP in this.selectedSubjects)
        {
          var lociSimilarity = new List<object>();
          var sequenceSimilarity = new List<object>();
          ////DataView fixationsOfS = new DataView(
          ////  fixations,
          ////  "(SubjectName='" + subjectRowKVP.Value.SubjectName + "')",
          //// "CountInTrial",
          //// DataViewRowState.CurrentRows);

          foreach (KeyValuePair<string, ScanpathProperties> subjectColumnKVP in this.selectedSubjects)
          {
            ////DataView fixationsOfT = new DataView(
            ////  fixations,
            ////  "(SubjectName='" + subjectColumnKVP.Value.SubjectName + "')",
            //// "CountInTrial",
            //// DataViewRowState.CurrentRows);

            ////float malsburgSimilarity = EditDistance.MalsburgDistance(sFixations, tFixations, 0.5f);
            List<string> rowStringList = GetStringListForExtendedString(subjectRowKVP.Value.ScanpathString);
            List<string> columnStringList = GetStringListForExtendedString(subjectColumnKVP.Value.ScanpathString);
            float localSimilarity = EditDistance.CharacterSimilarityExtended(
              rowStringList,
              columnStringList) * 100;
            float globalSimilarity = EditDistance.SequenceSimilarityExtended(
              rowStringList,
              columnStringList) * 100;

            lociSimilarity.Add(localSimilarity.ToString("N0") + " %");
            sequenceSimilarity.Add(globalSimilarity.ToString("N0") + " %");
          }

          var lociColumns = new List<object>();
          lociColumns.Add(subjectRowKVP.Value.SubjectName);
          lociColumns.AddRange(lociSimilarity.ToArray());
          lociColumns.Add(subjectRowKVP.Value.ScanpathString);
          lociColumns.Add(subjectRowKVP.Value.SubjectCategory);
          this.dgvLociSimilarity.Rows.Add(lociColumns.ToArray());

          var sequenceColumns = new List<object>();
          sequenceColumns.Add(subjectRowKVP.Value.SubjectName);
          sequenceColumns.AddRange(sequenceSimilarity.ToArray());
          sequenceColumns.Add(subjectRowKVP.Value.ScanpathString);
          sequenceColumns.Add(subjectRowKVP.Value.SubjectCategory);
          this.dgvSequenceSimilarity.Rows.Add(sequenceColumns.ToArray());
        }
      }

      this.dgvLociSimilarity.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.DisplayedCells);
      this.dgvSequenceSimilarity.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.DisplayedCells);
    }

    /// <summary>
    /// Returns a <see cref="List{String}"/> that contain the string parts of the extended
    /// levenshtein string.
    /// </summary>
    /// <param name="extendedString">A <see cref="String"/> with the extended string.</param>
    /// <returns>A <see cref="List{String}"/> to be used in the extended distance calculation.</returns>
    private static List<string> GetStringListForExtendedString(string extendedString)
    {
      var rowString = new List<string>();

      int identifierCount = ScanpathsPicture.CurrentIdentifierList.Length;
      int modulo = 1;
      if (identifierCount > 26)
      {
        modulo = 2;
      }

      if (identifierCount > 676)
      {
        modulo = 3;
      }

      for (int i = 0; i < extendedString.Length; i += modulo)
      {
        rowString.Add(extendedString.Substring(i, modulo));
      }

      return rowString;
    }

    /// <summary>
    /// This method adds the subject described by the <see cref="TreeViewEventArgs"/>
    /// and the given name to the list of selected subjects.
    /// </summary>
    /// <param name="node">An <see cref="TreeNode"/> with the selected 
    /// subject</param>
    /// <param name="name">A <see cref="String"/> with the subject name.</param>
    private void AddSubjectToList(TreeNode node, string name)
    {
      if (node.Checked)
      {
        if (!this.selectedSubjects.ContainsKey(name))
        {
          string scanpathString = string.Empty;

          if (this.calcDistances)
          {
            scanpathString = this.scanpathsPicture.CalcPathString(name);
          }

          ScanpathProperties props = new ScanpathProperties(
            name,
            node.Level == 0 ? node.Text : node.Parent.Text,
            this.gazeColorParams.SubjectStyles[name],
            this.mouseColorParams.SubjectStyles[name],
            scanpathString);

          this.selectedSubjects.Add(name, props);
        }
      }
      else
      {
        if (this.selectedSubjects.ContainsKey(name))
        {
          this.selectedSubjects.Remove(name);
        }
      }
    }

    /// <summary>
    /// This method reads the selected subjects from the <see cref="TreeView"/>
    /// with the current color from ColorizationParameters.SubjectColors into the 
    /// <see cref="selectedSubjects"/>field and recalculates the distances if applicable.
    /// </summary>
    private void ReReadSelectedSubjects()
    {
      this.selectedSubjects.Clear();
      foreach (TreeNode categoryNode in this.trvSubjects.Nodes)
      {
        foreach (TreeNode subjectNode in categoryNode.Nodes)
        {
          if (subjectNode.Checked)
          {
            string name = subjectNode.Text;
            if (!this.selectedSubjects.ContainsKey(name))
            {
              string scanpathString = string.Empty;

              if (this.calcDistances)
              {
                scanpathString = this.scanpathsPicture.CalcPathString(name);
              }

              ScanpathProperties props = new ScanpathProperties(
                name,
                categoryNode.Text,
                this.gazeColorParams.SubjectStyles[name],
                this.mouseColorParams.SubjectStyles[name],
                scanpathString);

              this.selectedSubjects.Add(name, props);
            }
          }
        }
      }

      this.RefillDistancesTable();
    }

    /// <summary>
    /// This method checks the UI for the grid settings
    /// and updates the <see cref="VectorGraphics.Canvas.Picture"/> with the new <see cref="GridBase"/>
    /// enumeration to be used in the levensthein edit distance calculation.
    /// </summary>
    private void UpdateGrid()
    {
      if (this.btnLevenstheinToolbar.Checked)
      {
        if (this.btnGrid.Checked)
        {
          this.scanpathsPicture.GridBasis = GridBase.Rectangular;
        }
        else if (this.btnAOI.Checked)
        {
          this.scanpathsPicture.GridBasis = GridBase.AOIs;
        }
        else
        {
          this.scanpathsPicture.GridBasis = GridBase.None;
        }
      }
      else
      {
        this.scanpathsPicture.GridBasis = GridBase.None;
      }

      this.scanpathsPicture.DrawFixations(true);
    }

    /// <summary>
    /// This method stops the animation in the <see cref="VectorGraphics.Canvas.Picture"/>
    /// and reenables the correct control buttons.
    /// </summary>
    private void StopPlaying()
    {
      // Stop update timer
      this.scanpathsPicture.StopAnimation();

      // Activate and deactivate buttons
      this.btnStart.Enabled = true;
      this.btnStop.Enabled = false;
      this.btnRewind.Enabled = true;
    }

    /// <summary>
    /// Resets UI to start conditions by enabling start button and resetting picture.
    /// </summary>
    private void ResetControls()
    {
      this.scanpathsPicture.StopAnimation();

      ((MainForm)this.MdiParent).StatusLabel.Text = "Ready";
      ((MainForm)this.MdiParent).StatusProgressbar.Value = 0;
      ((MainForm)this.MdiParent).StatusRightLabel.Text = string.Empty;

      this.trialTimeLine.ResetTimeLine();
      this.scanpathsPicture.ResetPicture();
    }

    /// <summary>
    /// This method updates the current <see cref="ColorizationParameters"/>
    /// for gaze and mouse drawing in the Document.ActiveDocument.ExperimentSettings
    /// </summary>
    private void SaveColorization()
    {
      if (Document.ActiveDocument.ExperimentSettings.GazeColorParams != this.gazeColorParams)
      {
        Document.ActiveDocument.ExperimentSettings.GazeColorParams = (ColorizationParameters)this.gazeColorParams.Clone();
      }

      if (Document.ActiveDocument.ExperimentSettings.MouseColorParams != this.mouseColorParams)
      {
        Document.ActiveDocument.ExperimentSettings.MouseColorParams = (ColorizationParameters)this.mouseColorParams.Clone();
      }

      Document.ActiveDocument.Modified = true;
    }

    #endregion //METHODS

    ///////////////////////////////////////////////////////////////////////////////
    // Small helping Methods                                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region HELPER

    /// <summary>
    /// Updates all child tree nodes recursively.
    /// </summary>
    /// <param name="treeNode">The <see cref="TreeNode"/> for which all
    /// childs should be checked or unchecked.</param>
    /// <param name="nodeChecked"><strong>True</strong>, if child nodes
    /// should be checked, otherwise <strong>false</strong>.</param>
    private void CheckAllChildNodes(TreeNode treeNode, bool nodeChecked)
    {
      foreach (TreeNode node in treeNode.Nodes)
      {
        node.Checked = nodeChecked;
        if (node.Nodes.Count > 0)
        {
          // If the current node has child nodes, call the CheckAllChildsNodes method recursively.
          this.CheckAllChildNodes(node, nodeChecked);
        }
      }
    }

    #endregion //HELPER

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Enumerations                                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region ENUMS

    /// <summary>
    /// This structure is used to save a levenshtein distance of two categories.
    /// This is a matrix entry.
    /// </summary>
    private struct DistanceEntry
    {
      /// <summary>
      /// The category of the row in the distance matrix.
      /// </summary>
      public int RowCategory;

      /// <summary>
      /// The category of the column in the distance matrix.
      /// </summary>
      public int ColumnCategory;

      /// <summary>
      /// The distance value at the matrix position.
      /// </summary>
      public float DistanceValue;

      /// <summary>
      /// Initializes a new instance of the DistanceEntry struct.
      /// </summary>
      /// <param name="newRowCategory">The category of the row in the distance matrix.</param>
      /// <param name="newColumnCategory">The category of the column in the distance matrix.</param>
      /// <param name="newValue">The distance value at the matrix position.</param>
      public DistanceEntry(int newRowCategory, int newColumnCategory, float newValue)
      {
        this.RowCategory = newRowCategory >= newColumnCategory ? newColumnCategory : newRowCategory;
        this.ColumnCategory = newRowCategory >= newColumnCategory ? newRowCategory : newColumnCategory;
        this.DistanceValue = newValue;
      }
    }

    /// <summary>
    /// This structure is used to save a distance value 
    /// for a category along with its divisor.
    /// (Used to sum up categorial distances)
    /// </summary>
    private struct CategoryEntry
    {
      /// <summary>
      /// The current divisor number
      /// </summary>
      public int Divisor;

      /// <summary>
      /// The current distance value.
      /// </summary>
      public float DistanceValue;

      /// <summary>
      /// Initializes a new instance of the CategoryEntry struct.
      /// </summary>
      /// <param name="newDivisor">The new divisor number</param>
      /// <param name="newValue">The new distance value.</param>
      public CategoryEntry(int newDivisor, float newValue)
      {
        this.Divisor = newDivisor;
        this.DistanceValue = newValue;
      }
    }

    #endregion //ENUMS
  }
}