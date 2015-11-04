// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AOIModule.cs" company="Freie Universität Berlin">
//   OGAMA - open gaze and mouse analyzer 
//   Copyright (C) 2015 Dr. Adrian Voßkühler  
//   Licensed under GPL V3
// </copyright>
// <author>Adrian Voßkühler</author>
// <email>adrian@ogama.net</email>
// <summary>
//   Derived from <see cref="FormWithTrialSelection" />.
//   This form hosts the areas of interest module. (AOI)
//   This class handles the UI and the database connection for
//   the <see cref="AOIPicture" /> class, which is the main element
//   of this form.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Ogama.Modules.AOI
{
  using System;
  using System.Collections.Generic;
  using System.Data;
  using System.Drawing;
  using System.IO;
  using System.Windows.Forms;

  using Ogama.DataSet;
  using Ogama.ExceptionHandling;
  using Ogama.MainWindow;
  using Ogama.Modules.AOI.Dialogs;
  using Ogama.Modules.Common.FormTemplates;
  using Ogama.Modules.Common.SlideCollections;
  using Ogama.Modules.Common.Tools;
  using Ogama.Modules.Common.Types;
  using Ogama.Modules.ImportExport.AOIData;
  using Ogama.Modules.ImportExport.Common;
  using Ogama.Properties;

  using OgamaControls.Dialogs;

  using VectorGraphics.Elements;
  using VectorGraphics.Elements.ElementCollections;
  using VectorGraphics.Tools.CustomEventArgs;
  using VectorGraphics.Tools.CustomTypeConverter;

  /// <summary>
  ///   Derived from <see cref="FormWithTrialSelection" />.
  ///   This form hosts the areas of interest module. (AOI)
  ///   This class handles the UI and the database connection for
  ///   the <see cref="AOIPicture" /> class, which is the main element
  ///   of this form.
  /// </summary>
  /// <remarks>
  ///   This module is intended to define and display
  ///   areas of interest (AOI) on the given stimulus images.
  ///   The defined AOI then can be edited and copied in a data
  ///   grid view. There is also the possibility to import
  ///   a list of AOI from a text file. OGAMA knows ellipsoid,
  ///   rectangular and polylineal shapes for AOI.
  /// </remarks>
  public partial class AOIModule : FormWithTrialSelection
  {
    #region Constructors and Destructors

    /// <summary>
    ///   Initializes a new instance of the AOIModule class. Initializes bindings and UI.
    /// </summary>
    public AOIModule()
    {
      this.InitializeComponent();

      this.Picture = this.aoiPicture;
      this.TrialCombo = this.cbbTrial;
      this.ZoomTrackBar = this.trbZoom;

      this.InitializeDropDowns();
      this.InitializeDataBindings();
      this.InitializeCustomElements();
      this.InitAccelerators();
    }

    #endregion

    #region Public Methods and Operators

    /// <summary>
    ///   Uncheck shape creation buttons.
    /// </summary>
    public void ResetButtons()
    {
      this.btnNewRectangle.Checked = false;
      this.btnNewEllipse.Checked = false;
      this.btnNewPolyline.Checked = false;
      this.btnNewAOIGrid.Checked = false;
    }

    /// <summary>
    ///   Causes the controls bound to the BindingSources to reread all the
    ///   items in the list and refresh their displayed values.
    /// </summary>
    public override void ResetDataBindings()
    {
      base.ResetDataBindings();
      PopulateSubjectTreeView(this.trvSubjects);
    }

    /// <summary>
    ///   Updates database and mdf file.
    /// </summary>
    public void UpdateDatabase()
    {
      this.Cursor = Cursors.WaitCursor;
      ((MainForm)this.MdiParent).StatusLabel.Text = "Updating AOI database";
      try
      {
        this.dgvAOIs.EndEdit();
        Application.DoEvents();
        Document.ActiveDocument.DocDataSet.EnforceConstraints = false;
        Document.ActiveDocument.DocDataSet.AOIsAdapter.Update(Document.ActiveDocument.DocDataSet.AOIs);
        Document.ActiveDocument.DocDataSet.EnforceConstraints = true;
      }
      catch (Exception ex)
      {
        ExceptionMethods.HandleException(ex);
      }
      finally
      {
        this.Cursor = Cursors.Default;
        ((MainForm)this.MdiParent).StatusLabel.Text = "Ready.";
      }
    }

    #endregion

    #region Methods

    /// <summary>
    ///   Initializes accelerator keys. Binds to methods.
    /// </summary>
    protected override sealed void InitAccelerators()
    {
      base.InitAccelerators();
    }

    /// <summary>
    ///   This methods is used to initialize elements that are not
    ///   initialized in the designer.
    /// </summary>
    protected override sealed void InitializeCustomElements()
    {
      base.InitializeCustomElements();
      PopulateSubjectTreeView(this.trvSubjects);
      this.btnHelp.Click += this.btnHelp_Click;
      this.pnlCanvas.Resize += this.pnlCanvas_Resize;
    }

    /// <summary>
    ///   Initializes data bindings.
    ///   Is called whenever the database bindings are updated.
    /// </summary>
    protected override sealed void InitializeDataBindings()
    {
      base.InitializeDataBindings();

      // Set data grid view styles
      this.colID.DefaultCellStyle = this.ReadOnlyCellStyle;
      this.colTrialID.DefaultCellStyle = this.ReadOnlyCellStyle;
      this.colShapeNumPts.DefaultCellStyle = this.ReadOnlyCellStyle;
      this.colShapePts.DefaultCellStyle = this.ReadOnlyCellStyle;
      this.colShapeType.DefaultCellStyle = this.ReadOnlyCellStyle;
      this.colShapeGroup.DataSource = this.bsoShapeGroups;
    }

    /// <summary>
    ///   Initialize drop down controls.
    /// </summary>
    /// <remarks>
    ///   The toolstrip combo box does currently not know the
    ///   <see cref="ComboBox.SelectionChangeCommitted" /> event, so here we initialize it
    ///   from the
    ///   <see cref="ToolStripComboBox.ComboBox" /> member.
    /// </remarks>
    protected override sealed void InitializeDropDowns()
    {
      base.InitializeDropDowns();
    }

    /// <summary>
    ///   Reads dropdown settings and loads corresponding images and data from database.
    ///   Then notifys picture the changes.
    /// </summary>
    /// <returns><strong>True</strong>, if selection was successful, otherwise <strong>false</strong>.</returns>
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

          this.LoadTrialSlidesIntoTimeline(this.trialTimeLine);

          // Filter the items
          this.bsoAOIs.Filter = "(TrialID='" + trialID + "' AND SlideNr='" + this.trialTimeLine.HighlightedSlideIndex
                                + "')";

          this.UpdatePicture();

          // If transition panel is visible
          if (!this.spcStatisticsPicture.Panel1Collapsed)
          {
            this.UpdateStatistics();
          }
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

    /// <summary>
    /// The <see cref="MainForm.EditCopy"/> event handler for the
    ///   parent <see cref="Form"/> <see cref="MainForm"/>.
    ///   This method handles the edit copy event from main form
    ///   by either copying selected cells in data grid view or
    ///   rendering a copy of the displayed picture
    ///   to clipboard, depending on focus.
    /// </summary>
    /// <param name="sender">
    /// Source of the event.
    /// </param>
    /// <param name="e">
    /// An empty <see cref="EventArgs"/>
    /// </param>
    protected override void mainWindow_EditCopy(object sender, EventArgs e)
    {
      Form activeMdiChild = this.MdiParent.ActiveMdiChild;
      if (activeMdiChild != null && activeMdiChild.Name == this.Name)
      {
        try
        {
          if (this.dgvAOIs.Focused)
          {
            Clipboard.SetDataObject(this.dgvAOIs.GetClipboardContent());
            ((MainForm)this.MdiParent).StatusLabel.Text = "Table selection exported to clipboard.";
          }
          else
          {
            this.aoiPicture.OnCopy();
            ((MainForm)this.MdiParent).StatusLabel.Text = "Selection exported to clipboard.";
          }
        }
        catch (Exception ex)
        {
          ExceptionMethods.HandleException(ex);
        }
      }
    }

    /// <summary>
    /// The <see cref="MainForm.EditPaste"/> event handler for the
    ///   parent <see cref="Form"/> <see cref="MainForm"/>.
    ///   Paste clipboard text into data grid view if possible.
    /// </summary>
    /// <remarks>
    /// Clipboard text should be tabulator separated and with \n line endings
    /// </remarks>
    /// <param name="sender">
    /// Source of the event.
    /// </param>
    /// <param name="e">
    /// An empty <see cref="EventArgs"/>
    /// </param>
    protected override void mainWindow_EditPaste(object sender, EventArgs e)
    {
      var activeMdiChild = this.MdiParent.ActiveMdiChild;
      if (activeMdiChild != null && activeMdiChild.Name == this.Name)
      {
        try
        {
          this.aoiPicture.OnPaste();

          string clipboardText = Clipboard.GetText();

          // Check for multiple lines
          if (clipboardText.Contains("\n"))
          {
            string[] rows = clipboardText.Split('\n');
            foreach (string line in rows)
            {
              string trimmedLine = line.Replace('\r', ' ');
              trimmedLine = trimmedLine.TrimStart(null);
              string[] cells = trimmedLine.Split('\t');
              this.WriteNewLine(cells);
            }
          }
          else
          {
            // Single line or parts
            if (clipboardText.StartsWith("\t"))
            {
              clipboardText = clipboardText.TrimStart(null);
            }

            string[] items = clipboardText.Split('\t');

            // Check if selection matches clipboard size 
            // if it does, overwrite, else if enough data, write new line
            if (items.Length == this.dgvAOIs.Columns.Count)
            {
              this.WriteNewLine(items);
            }
            else
            {
              ((MainForm)this.MdiParent).StatusLabel.Text =
                "Clipboard content has wrong format, so it could not be inserted into the table.";
            }
          }

          this.ResetButtons();
          this.UpdatePicture();
        }
        catch (Exception ex)
        {
          ExceptionMethods.HandleException(ex);
        }
      }
    }

    /// <summary>
    /// The event handler for the
    ///   <see cref="VectorGraphics.Canvas.PictureModifiable.ShapeAdded"/> event from the
    ///   <see cref="VectorGraphics.Canvas.PictureModifiable"/> <see cref="aoiPicture"/>.
    ///   Updates database with new areas of interest shape given in the event arguments
    /// </summary>
    /// <param name="sender">
    /// Source of the event.
    /// </param>
    /// <param name="e">
    /// A <see cref="ShapeEventArgs"/> with the event data.
    /// </param>
    private void AOIPictureShapeAdded(object sender, ShapeEventArgs e)
    {
      // Skip if no data available
      if (this.cbbTrial.SelectedItem == null)
      {
        return;
      }

      string shapeName = e.Shape.Name;
      int shapePointCount = e.Shape.GetPointCount();
      string shapeType = e.Shape.GetType().ToString().Replace("VectorGraphics.Elements.VG", string.Empty);
      string strShapePoints = ObjectStringConverter.PointFListToString(e.Shape.GetPoints());
      int trialID = ((Trial)this.cbbTrial.SelectedItem).ID;

      // Add new Element to AOITable
      var aoi = new AOIData();
      aoi.ShapeName = shapeName;
      aoi.ShapeNumPts = shapePointCount;
      aoi.ShapePts = strShapePoints;
      aoi.ShapeType = (VGShapeType)Enum.Parse(typeof(VGShapeType), shapeType);
      aoi.TrialID = trialID;
      aoi.SlideNr = this.trialTimeLine.HighlightedSlideIndex;

      if (!Queries.WriteAOIDataToDataSet(aoi, null))
      {
        this.aoiPicture.DeleteShape(e.Shape);
      }

      this.ResetButtons();
    }

    /// <summary>
    /// The event handler for the
    ///   <see cref="VectorGraphics.Canvas.PictureModifiable.ShapeChanged"/> event from the
    ///   <see cref="VectorGraphics.Canvas.PictureModifiable"/> <see cref="aoiPicture"/>.
    ///   Updates database with modified areas of interest shape given in the event arguments
    /// </summary>
    /// <param name="sender">
    /// Source of the event.
    /// </param>
    /// <param name="e">
    /// A <see cref="ShapeEventArgs"/> with the event data.
    /// </param>
    private void AOIPictureShapeChanged(object sender, ShapeEventArgs e)
    {
      // Skip if no data available
      if (this.cbbTrial.SelectedItem == null)
      {
        return;
      }

      string shapeName = e.Shape.Name;
      int shapePointCount = e.Shape.GetPointCount();
      string shapeType = e.Shape.GetType().ToString().Replace("VectorGraphics.Elements.VG", string.Empty);
      string strShapePoints = ObjectStringConverter.PointFListToString(e.Shape.GetPoints());
      int trialID = ((Trial)this.cbbTrial.SelectedItem).ID;

      try
      {
        // Presuming the DataTable has a column named ShapeName.
        string expression = "ShapeName = '" + shapeName + "' AND TrialID = '" + trialID + "'";

        // Use the Select method to find all rows matching the filter.
        DataRow[] foundRows = Document.ActiveDocument.DocDataSet.AOIs.Select(expression);
        if (foundRows.Length > 0)
        {
          foreach (DataRow row in foundRows)
          {
            row["ShapeType"] = shapeType;
            row["ShapeNumPts"] = shapePointCount;
            row["ShapePts"] = strShapePoints;
          }
        }
      }
      catch (Exception ex)
      {
        ExceptionMethods.HandleException(ex);
      }

      this.ResetButtons();
    }

    /// <summary>
    /// The <see cref="VectorGraphics.Canvas.PictureModifiable.ShapeDeleted"/> event handler for the
    ///   <see cref="VectorGraphics.Canvas.PictureModifiable"/> <see cref="aoiPicture"/>.
    ///   Removes the corresponding row from the data grid view.
    /// </summary>
    /// <param name="sender">
    /// Source of the event.
    /// </param>
    /// <param name="e">
    /// A <see cref="ShapeEventArgs"/> with the event data.
    /// </param>
    private void AOIPictureShapeDeleted(object sender, ShapeEventArgs e)
    {
      try
      {
        string shapeName = e.Shape.Name;

        foreach (DataGridViewRow row in this.dgvAOIs.Rows)
        {
          if (row.Cells["colShapeName"].Value.ToString() == shapeName)
          {
            this.dgvAOIs.Rows.Remove(row);
            break;
          }
        }
      }
      catch (Exception ex)
      {
        ExceptionMethods.HandleException(ex);
      }
    }

    /// <summary>
    /// The <see cref="CheckBox.CheckedChanged"/> event handler for
    ///   the bubble style <see cref="CheckBox"/>s.
    ///   Notify the picture the new arrow style.
    /// </summary>
    /// <param name="sender">
    /// Source of the event.
    /// </param>
    /// <param name="e">
    /// An empty <see cref="EventArgs"/>
    /// </param>
    private void ArrowStyleCheckedChanged(object sender, EventArgs e)
    {
      this.UpdateStatisticProperties(
        this.aoiPicture.ArrowPen, 
        this.aoiPicture.ArrowBrush, 
        this.aoiPicture.ArrowFont, 
        this.aoiPicture.ArrowFontColor, 
        this.aoiPicture.ArrowTextAlignment, 
        VGStyleGroup.AOI_STATISTICS_ARROW);
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler for the
    ///   <see cref="Button"/> <see cref="btnSeekPreviousSlide"/>.
    ///   Activate previous slide in trial.
    /// </summary>
    /// <param name="sender">
    /// Source of the event.
    /// </param>
    /// <param name="e">
    /// An empty <see cref="EventArgs"/>
    /// </param>
    private void BtnSeekPreviousSlideClick(object sender, EventArgs e)
    {
      // Skip if there is no data
      if (this.CurrentTrial == null)
      {
        return;
      }

      this.trialTimeLine.HighlightNextSlide(false);
      int slideIndex = this.trialTimeLine.HighlightedSlideIndex;
      this.LoadSlide(this.CurrentTrial[slideIndex], ActiveXMode.BehindPicture);
      this.bsoAOIs.Filter = "(TrialID='" + this.CurrentTrial.ID + "' AND SlideNr='" + slideIndex + "')";
      this.UpdatePicture();
    }

    /// <summary>
    /// The <see cref="CheckBox.CheckedChanged"/> event handler for
    ///   the bubble style <see cref="CheckBox"/>s.
    ///   Notify the picture the new bubble style.
    /// </summary>
    /// <param name="sender">
    /// Source of the event.
    /// </param>
    /// <param name="e">
    /// An empty <see cref="EventArgs"/>
    /// </param>
    private void BubbleStyleCheckedChanged(object sender, EventArgs e)
    {
      this.UpdateStatisticProperties(
        this.aoiPicture.BubblePen, 
        this.aoiPicture.BubbleBrush, 
        this.aoiPicture.BubbleFont, 
        this.aoiPicture.BubbleFontColor, 
        this.aoiPicture.BubbleTextAlignment, 
        VGStyleGroup.AOI_STATISTICS_BUBBLE);
    }

    /// <summary>
    ///   This method shows a dialog that allows to specify the
    ///   name for a new shape group.
    ///   Afterwards it writes the group to the database.
    /// </summary>
    private void CallAddShapeGroup()
    {
      var dlg = new GroupNameDlg();
      if (dlg.ShowDialog() == DialogResult.OK)
      {
        SQLiteOgamaDataSet.ShapeGroupsRow workRowShapeGroups =
          Document.ActiveDocument.DocDataSet.ShapeGroups.NewShapeGroupsRow();
        workRowShapeGroups.ShapeGroup = dlg.GroupName;
        Document.ActiveDocument.DocDataSet.ShapeGroups.AddShapeGroupsRow(workRowShapeGroups);
        Document.ActiveDocument.DocDataSet.ShapeGroupsAdapter.Update(Document.ActiveDocument.DocDataSet.ShapeGroups);
        Document.ActiveDocument.DocDataSet.AcceptChanges();
      }
    }

    /// <summary>
    /// This method filters the fixations for selected subjects
    ///   and submits the data to the underlying picture.
    ///   Then it starts the pictures statistic calculation.
    /// </summary>
    /// <param name="trialID">
    /// An <see cref="Int32"/> with the trial ID
    /// </param>
    private void CalulateStatistics(int trialID)
    {
      var gazeFixations = new DataView(
        Document.ActiveDocument.DocDataSet.GazeFixationsAdapter.GetDataByTrialID(trialID));
      var mouseFixations =
        new DataView(Document.ActiveDocument.DocDataSet.MouseFixationsAdapter.GetDataByTrialID(trialID));

      var checkedSubjects = new List<string>();

      // Check if any subject is available
      if (this.trvSubjects.Nodes.Count == 0)
      {
        return;
      }

      foreach (TreeNode categoryNode in this.trvSubjects.Nodes)
      {
        foreach (TreeNode subjectNode in categoryNode.Nodes)
        {
          if (subjectNode.Checked)
          {
            checkedSubjects.Add(subjectNode.Text);
          }
        }
      }

      string filterString = string.Empty;
      if (checkedSubjects.Count > 0)
      {
        foreach (string subject in checkedSubjects)
        {
          filterString += "(SubjectName='" + subject + "') OR ";
        }

        filterString = filterString.Substring(0, filterString.Length - 4);
      }
      else
      {
        filterString = "(SubjectName='')";
      }

      gazeFixations.RowFilter = filterString;
      mouseFixations.RowFilter = filterString;

      this.aoiPicture.GazeFixationsView = gazeFixations;
      this.aoiPicture.MouseFixationsView = mouseFixations;

      this.aoiPicture.CalculateAOIStatistics(this.btnMouse.Checked ? SampleType.Mouse : SampleType.Gaze);
      this.ShowAOIStatistics();
    }

    /// <summary>
    ///   Deletes complete areas of interest table content in database with a SQL query.
    /// </summary>
    private void DeleteAOIinDatabase()
    {
      string queryString = "DELETE FROM AOIs;";
      Queries.ExecuteSQLCommand(queryString);
    }

    /// <summary>
    ///   This method parses the statistic settings ands
    ///   submits them to the picture to call the statistical
    ///   drawing function
    /// </summary>
    private void ShowAOIStatistics()
    {
      var visualizationMode = VisualizationModes.None;

      if (this.rdbGazeAverageFixationDuration.Checked)
      {
        visualizationMode = VisualizationModes.AverageFixationDuration;
      }
      else if (this.rdbGazeCompleteFixationTime.Checked)
      {
        visualizationMode = VisualizationModes.FixationTime;
      }
      else if (this.rdbGazeNumberOfFixations.Checked)
      {
        visualizationMode = VisualizationModes.NumberOfFixations;
      }

      if (this.rdbAbsoluteTransitions.Checked)
      {
        visualizationMode |= VisualizationModes.AbsoluteTransitions;
      }
      else if (this.rdbRelativeTransitions.Checked)
      {
        visualizationMode |= VisualizationModes.RelativeTransitions;
      }

      var sampleType = SampleType.None;
      if (this.btnEye.Checked)
      {
        sampleType |= SampleType.Gaze;
      }

      if (this.btnMouse.Checked)
      {
        sampleType |= SampleType.Mouse;
      }

      this.aoiPicture.DrawAOIStatistics(visualizationMode, sampleType);
    }

    /// <summary>
    ///   Updates AOI picture with new AOI values from the data grid view.
    /// </summary>
    private void UpdatePicture()
    {
      this.aoiPicture.LoadShapesFromDataGridView(this.dgvAOIs.Rows);
    }

    /// <summary>
    /// This method parses the statistic style settings
    ///   submits them to the picture to call the statistical
    ///   drawing function
    /// </summary>
    /// <param name="newPen">
    /// The <see cref="Pen"/> to use for the edge.
    /// </param>
    /// <param name="newBrush">
    /// The <see cref="Brush"/> to use for the fill.
    /// </param>
    /// <param name="newFont">
    /// The <see cref="Font"/> to use.
    /// </param>
    /// <param name="newFontColor">
    /// The <see cref="Color"/> to use for the font.
    /// </param>
    /// <param name="newTextAlignment">
    /// The <see cref="VGAlignment"/> to use.
    /// </param>
    /// <param name="styleGroup">
    /// The <see cref="VGStyleGroup"/> this style should belong to.
    /// </param>
    private void UpdateStatisticProperties(
      Pen newPen, 
      Brush newBrush, 
      Font newFont, 
      Color newFontColor, 
      VGAlignment newTextAlignment, 
      VGStyleGroup styleGroup)
    {
      var drawAction = ShapeDrawAction.None;

      switch (styleGroup)
      {
        case VGStyleGroup.AOI_STATISTICS_BUBBLE:
          if (this.chbBubblePen.Checked)
          {
            drawAction |= ShapeDrawAction.Edge;
          }

          if (this.chbBubbleFont.Checked)
          {
            drawAction |= ShapeDrawAction.Name;
          }

          if (this.chbBubbleBrush.Checked)
          {
            drawAction |= ShapeDrawAction.Fill;
          }

          break;
        case VGStyleGroup.AOI_STATISTICS_ARROW:
          if (this.chbArrowPen.Checked)
          {
            drawAction |= ShapeDrawAction.Edge;
          }

          if (this.chbArrowFont.Checked)
          {
            drawAction |= ShapeDrawAction.Name;
          }

          if (this.chbArrowBrush.Checked)
          {
            drawAction |= ShapeDrawAction.Fill;
          }

          break;
      }

      var ea = new ShapePropertiesChangedEventArgs(
        drawAction, 
        newPen, 
        newBrush, 
        newFont, 
        newFontColor, 
        string.Empty, 
        newTextAlignment);

      this.aoiPicture.ShapePropertiesChanged(styleGroup, ea);
    }

    /// <summary>
    ///   Updates AOI picture with new AOI statistics.
    /// </summary>
    private void UpdateStatistics()
    {
      this.CalulateStatistics(Document.ActiveDocument.SelectionState.TrialID);
    }

    /// <summary>
    /// Adds a new area of interest line to the database.
    /// </summary>
    /// <param name="cells">
    /// A <see cref="string"/> array with AOI columns.
    /// </param>
    private void WriteNewLine(string[] cells)
    {
      DataRow newRow = Document.ActiveDocument.DocDataSet.AOIs.NewRow();
      int largestValue = newRow.ItemArray.Length;
      if (cells.Length != newRow.ItemArray.Length)
      {
        return;
      }

      // Set Trial information to current trial
      newRow["TrialID"] = ((Trial)this.cbbTrial.SelectedItem).ID;
      newRow["SlideNr"] = this.trialTimeLine.HighlightedSlideIndex;

      // Set unique name if applicable
      string nameProposal = cells[3];
      foreach (DataGridViewRow row in this.dgvAOIs.Rows)
      {
        if (row.Cells["colShapeName"].Value.ToString() == nameProposal)
        {
          nameProposal += "Copy";
          break;
        }
      }

      newRow["ShapeName"] = nameProposal;

      for (int i = 4; i < largestValue; i++)
      {
        newRow[i] = cells[i];
      }

      try
      {
        Document.ActiveDocument.DocDataSet.AOIs.Rows.Add(newRow);
      }
      catch (Exception ex)
      {
        ExceptionMethods.HandleException(ex);
      }
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler for the
    ///   <see cref="Button"/> <see cref="btnAddShapeGroup"/>.
    ///   User selected the add shape group button,
    ///   so call the <see cref="CallAddShapeGroup()"/> method.
    /// </summary>
    /// <param name="sender">
    /// Source of the event
    /// </param>
    /// <param name="e">
    /// An empty <see cref="EventArgs"/>
    /// </param>
    private void BtnAddShapeGroupClick(object sender, EventArgs e)
    {
      this.CallAddShapeGroup();
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler for the
    ///   <see cref="Button"/> <see cref="btnArrowBrush"/>.
    ///   Shows a <see cref="BrushStyleDlg"/> for the arrow shapes.
    /// </summary>
    /// <param name="sender">
    /// Source of the event.
    /// </param>
    /// <param name="e">
    /// An empty <see cref="EventArgs"/>
    /// </param>
    private void BtnArrowBrushClick(object sender, EventArgs e)
    {
      Brush backupBrush = this.aoiPicture.BubbleBrush;

      var dlgArrowBrush = new BrushStyleDlg();
      dlgArrowBrush.Text = "Set arrow brush style...";
      dlgArrowBrush.Brush = this.aoiPicture.ArrowBrush;
      dlgArrowBrush.BrushStyleChanged += this.dlgArrowStyle_BrushStyleChanged;
      if (dlgArrowBrush.ShowDialog() == DialogResult.Cancel)
      {
        this.UpdateStatisticProperties(
          this.aoiPicture.ArrowPen, 
          backupBrush, 
          this.aoiPicture.ArrowFont, 
          this.aoiPicture.ArrowFontColor, 
          this.aoiPicture.ArrowTextAlignment, 
          VGStyleGroup.AOI_STATISTICS_ARROW);
      }
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler for the
    ///   <see cref="Button"/> <see cref="btnArrowFont"/>.
    ///   Shows a <see cref="FontStyleDlg"/> for the arrow shapes.
    /// </summary>
    /// <param name="sender">
    /// Source of the event.
    /// </param>
    /// <param name="e">
    /// An empty <see cref="EventArgs"/>
    /// </param>
    private void BtnArrowFontClick(object sender, EventArgs e)
    {
      Font backupFont = this.aoiPicture.ArrowFont;
      Color backupColor = this.aoiPicture.ArrowFontColor;
      VGAlignment backupAlignment = this.aoiPicture.ArrowTextAlignment;

      var dlgArrowFontStyle = new FontStyleDlg();
      dlgArrowFontStyle.Text = "Set arrow font style...";
      dlgArrowFontStyle.CurrentFont = this.aoiPicture.ArrowFont;
      dlgArrowFontStyle.CurrentFontColor = this.aoiPicture.ArrowFontColor;
      dlgArrowFontStyle.CurrentFontAlignment = this.aoiPicture.ArrowTextAlignment;

      dlgArrowFontStyle.FontStyleChanged += this.dlgArrowStyle_FontStyleChanged;
      if (dlgArrowFontStyle.ShowDialog() == DialogResult.Cancel)
      {
        this.UpdateStatisticProperties(
          this.aoiPicture.ArrowPen, 
          this.aoiPicture.ArrowBrush, 
          backupFont, 
          backupColor, 
          backupAlignment, 
          VGStyleGroup.AOI_STATISTICS_ARROW);
      }
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler for the
    ///   <see cref="Button"/> <see cref="btnArrowPen"/>.
    ///   Shows a <see cref="PenStyleDlg"/> for the arrow shapes.
    /// </summary>
    /// <param name="sender">
    /// Source of the event.
    /// </param>
    /// <param name="e">
    /// An empty <see cref="EventArgs"/>
    /// </param>
    private void BtnArrowPenClick(object sender, EventArgs e)
    {
      Pen backupPen = this.aoiPicture.ArrowPen;

      var dlgArrowStyle = new PenStyleDlg();
      dlgArrowStyle.Text = "Define arrow pen style...";
      dlgArrowStyle.Pen = this.aoiPicture.ArrowPen;
      dlgArrowStyle.PenChanged += this.dlgArrowStyle_PenChanged;

      if (dlgArrowStyle.ShowDialog() == DialogResult.Cancel)
      {
        this.UpdateStatisticProperties(
          backupPen, 
          this.aoiPicture.ArrowBrush, 
          this.aoiPicture.ArrowFont, 
          this.aoiPicture.ArrowFontColor, 
          this.aoiPicture.ArrowTextAlignment, 
          VGStyleGroup.AOI_STATISTICS_ARROW);
      }
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler for the
    ///   <see cref="Button"/> <see cref="btnBubbleBrush"/>.
    ///   Shows a <see cref="BrushStyleDlg"/> for the bubble shapes.
    /// </summary>
    /// <param name="sender">
    /// Source of the event.
    /// </param>
    /// <param name="e">
    /// An empty <see cref="EventArgs"/>
    /// </param>
    private void BtnBubbleBrushClick(object sender, EventArgs e)
    {
      Brush backupBrush = this.aoiPicture.BubbleBrush;

      var dlgBubbleBrush = new BrushStyleDlg();
      dlgBubbleBrush.Text = "Set bubble brush style...";
      dlgBubbleBrush.Brush = this.aoiPicture.BubbleBrush;
      dlgBubbleBrush.BrushStyleChanged += this.dlgBubbleStyle_BrushStyleChanged;
      if (dlgBubbleBrush.ShowDialog() == DialogResult.Cancel)
      {
        this.UpdateStatisticProperties(
          this.aoiPicture.BubblePen, 
          backupBrush, 
          this.aoiPicture.BubbleFont, 
          this.aoiPicture.BubbleFontColor, 
          this.aoiPicture.BubbleTextAlignment, 
          VGStyleGroup.AOI_STATISTICS_BUBBLE);
      }
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler for the
    ///   <see cref="Button"/> <see cref="btnBubbleFont"/>.
    ///   Shows a <see cref="FontStyleDlg"/> for the bubble shapes.
    /// </summary>
    /// <param name="sender">
    /// Source of the event.
    /// </param>
    /// <param name="e">
    /// An empty <see cref="EventArgs"/>
    /// </param>
    private void BtnBubbleFontClick(object sender, EventArgs e)
    {
      Font backupFont = this.aoiPicture.BubbleFont;
      Color backupColor = this.aoiPicture.BubbleFontColor;
      VGAlignment backupAlignment = this.aoiPicture.BubbleTextAlignment;

      var dlgBubbleFontStyle = new FontStyleDlg();
      dlgBubbleFontStyle.Text = "Set bubble font style...";
      dlgBubbleFontStyle.CurrentFont = this.aoiPicture.BubbleFont;
      dlgBubbleFontStyle.CurrentFontColor = this.aoiPicture.BubbleFontColor;
      dlgBubbleFontStyle.CurrentFontAlignment = this.aoiPicture.BubbleTextAlignment;

      dlgBubbleFontStyle.FontStyleChanged += this.dlgBubbleStyle_FontStyleChanged;
      if (dlgBubbleFontStyle.ShowDialog() == DialogResult.Cancel)
      {
        this.UpdateStatisticProperties(
          this.aoiPicture.BubblePen, 
          this.aoiPicture.BubbleBrush, 
          backupFont, 
          backupColor, 
          backupAlignment, 
          VGStyleGroup.AOI_STATISTICS_BUBBLE);
      }
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler for the
    ///   <see cref="Button"/> <see cref="btnBubblePen"/>.
    ///   Shows a <see cref="PenStyleDlg"/> for the bubble shapes.
    /// </summary>
    /// <param name="sender">
    /// Source of the event.
    /// </param>
    /// <param name="e">
    /// An empty <see cref="EventArgs"/>
    /// </param>
    private void BtnBubblePenClick(object sender, EventArgs e)
    {
      Pen backupPen = this.aoiPicture.BubblePen;

      var dlgBubbleStyle = new PenStyleDlg();
      dlgBubbleStyle.Text = "Define bubble pen style...";
      dlgBubbleStyle.Pen = this.aoiPicture.BubblePen;
      dlgBubbleStyle.PenChanged += this.dlgBubbleStyle_PenChanged;

      if (dlgBubbleStyle.ShowDialog() == DialogResult.Cancel)
      {
        this.UpdateStatisticProperties(
          backupPen, 
          this.aoiPicture.BubbleBrush, 
          this.aoiPicture.BubbleFont, 
          this.aoiPicture.BubbleFontColor, 
          this.aoiPicture.BubbleTextAlignment, 
          VGStyleGroup.AOI_STATISTICS_BUBBLE);
      }
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler for the
    ///   <see cref="Button"/> <see cref="btnDeleteAOIs"/>.
    ///   User selected to delete all areas of interest.
    ///   Ask for safety and then delete.
    /// </summary>
    /// <param name="sender">
    /// Source of the event.
    /// </param>
    /// <param name="e">
    /// An empty <see cref="EventArgs"/>
    /// </param>
    private void btnDeleteAOIs_Click(object sender, EventArgs e)
    {
      if (MessageBox.Show(
        "Do you really want to delete the whole areas of interest database ?", 
        Application.ProductName, 
        MessageBoxButtons.YesNo, 
        MessageBoxIcon.Warning) == DialogResult.Yes)
      {
        this.DeleteAOIinDatabase();
        Document.ActiveDocument.DocDataSet.AOIs.Clear();
      }
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler for the
    ///   <see cref="Button"/> <see cref="btnDeselectAllSubjects"/>.
    ///   User decided to deselect all subjects for calculation so
    ///   uncheck them in the check box list.
    /// </summary>
    /// <param name="sender">
    /// Source of the event.
    /// </param>
    /// <param name="e">
    /// An empty <see cref="EventArgs"/>
    /// </param>
    private void btnDeselectAllSubjects_Click(object sender, EventArgs e)
    {
      foreach (TreeNode categoryNode in this.trvSubjects.Nodes)
      {
        categoryNode.Checked = false;
      }
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler for the
    ///   <see cref="Button"/> <see cref="btnExportAOITable"/>.
    ///   User selected export table button. Opens <see cref="SaveFileDialog"/>
    ///   <see cref="sfdExport"/> and writes a text file with tab separated columns
    ///   from the whole aoi database.
    /// </summary>
    /// <param name="sender">
    /// Source of the event
    /// </param>
    /// <param name="e">
    /// An empty <see cref="EventArgs"/>
    /// </param>
    private void btnExportAOITable_Click(object sender, EventArgs e)
    {
      float screenArea = Document.ActiveDocument.ExperimentSettings.WidthStimulusScreen
                         * Document.ActiveDocument.ExperimentSettings.HeightStimulusScreen;

      // Ask for and open Logfiles
      if (this.sfdExport.ShowDialog() == DialogResult.OK)
      {
        this.Cursor = Cursors.WaitCursor;

        string filenameExport = this.sfdExport.FileName;
        using (var exportFile = new StreamWriter(filenameExport))
        {
          // Write Documentation
          exportFile.WriteLine("# File: " + Path.GetFileName(filenameExport));
          exportFile.WriteLine(
            "# Created: " + DateTime.Today.Date.ToLongDateString() + "," + DateTime.Now.ToLongTimeString());
          exportFile.WriteLine(
            "# with: " + Application.ProductName + " Version: "
            + Document.ActiveDocument.ExperimentSettings.OgamaVersion.ToString(3));
          exportFile.WriteLine("# Contents: Areas of interest table.");
          exportFile.WriteLine("# Applies to Projekt:" + Document.ActiveDocument.ExperimentSettings.Name);
          exportFile.WriteLine("#");

          var aois = Document.ActiveDocument.DocDataSet.AOIs;

          // Write Column Names
          foreach (DataColumn dataColumn in aois.Columns)
          {
            exportFile.Write(dataColumn.Caption);
            exportFile.Write("\t");
          }

          exportFile.Write("AOI Area Size %");
          exportFile.Write("\t");

          exportFile.WriteLine();

          foreach (SQLiteOgamaDataSet.AOIsRow aoiRow in aois.Rows)
          {
            exportFile.Write(aoiRow.ID);
            exportFile.Write("\t");
            exportFile.Write(aoiRow.TrialID);
            exportFile.Write("\t");
            exportFile.Write(aoiRow.SlideNr);
            exportFile.Write("\t");
            exportFile.Write(aoiRow.IsShapeNameNull() ? string.Empty : aoiRow.ShapeName);
            exportFile.Write("\t");
            exportFile.Write(aoiRow.IsShapeTypeNull() ? string.Empty : aoiRow.ShapeType);
            exportFile.Write("\t");
            exportFile.Write(aoiRow.IsShapeNumPtsNull() ? 0 : aoiRow.ShapeNumPts);
            exportFile.Write("\t");
            exportFile.Write(aoiRow.IsShapePtsNull() ? string.Empty : aoiRow.ShapePts);
            exportFile.Write("\t");
            exportFile.Write(aoiRow.IsShapeGroupNull() ? string.Empty : aoiRow.ShapeGroup);
            exportFile.Write("\t");
            exportFile.Write(aoiRow.IsShapeTypeNull() ? 0 : Statistics.Statistic.GetAOISize(aoiRow) / screenArea * 100);
            exportFile.Write("\t");
            exportFile.WriteLine();
          }
        }

        ((MainForm)this.MdiParent).StatusLabel.Text = "Areas of interest successfully exported.";
      }

      // Reset Cursor.
      this.Cursor = Cursors.Default;
    }

    /// <summary>
    /// The <see cref="ToolStripButton.CheckedChanged"/> event handler for the
    ///   <see cref="ToolStripButton"/> <see cref="btnEye"/>.
    ///   User selected to use gaze data for aoi statistic. Update mouse buttons checked state.
    /// </summary>
    /// <param name="sender">
    /// Source of the event.
    /// </param>
    /// <param name="e">
    /// An empty <see cref="EventArgs"/>
    /// </param>
    private void btnEye_CheckedChanged(object sender, EventArgs e)
    {
      if (this.btnEye.Checked && this.btnMouse.Checked)
      {
        this.btnMouse.Checked = false;
      }
      else if (!this.btnEye.Checked && !this.btnMouse.Checked)
      {
        this.btnMouse.Checked = true;
      }
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler for the
    ///   <see cref="Button"/> <see cref="btnReadAOITable"/>.
    ///   User selected import table button. Starts <see cref="ImportAOI"/> object
    ///   with import process.
    /// </summary>
    /// <param name="sender">
    /// Source of the event
    /// </param>
    /// <param name="e">
    /// An empty <see cref="EventArgs"/>
    /// </param>
    private void btnImportReadAOITable_Click(object sender, EventArgs e)
    {
      ImportAOI.Start();
      this.NewTrialSelected();
      ((MainForm)this.MdiParent).StatusLabel.Text = "File successfully imported.";
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler for the
    ///   <see cref="Button"/> <see cref="btnImportTargets"/>.
    ///   User selected import target areas of slideshow into
    ///   AOIs. So convert them into table entries.
    /// </summary>
    /// <param name="sender">
    /// Source of the event
    /// </param>
    /// <param name="e">
    /// An empty <see cref="EventArgs"/>
    /// </param>
    private void btnImportTargets_Click(object sender, EventArgs e)
    {
      if (MessageBox.Show("Would you like to convert the target areas of the slideshow into AOIs ?") == DialogResult.OK)
      {
        foreach (Trial trial in Document.ActiveDocument.ExperimentSettings.SlideShow.Trials)
        {
          for (int i = 0; i < trial.Count; i++)
          {
            Slide slide = trial[i];
            foreach (VGElement element in slide.TargetShapes)
            {
              string shapeName = element.Name;
              int shapePointCount = element.GetPointCount();
              string shapeType = element.GetType().ToString().Replace("VectorGraphics.Elements.VG", string.Empty);
              string strShapePoints = ObjectStringConverter.PointFListToString(element.GetPoints());

              var aoi = new AOIData();
              aoi.ShapeName = shapeName;
              aoi.ShapeNumPts = shapePointCount;
              aoi.ShapePts = strShapePoints;
              aoi.ShapeType = (VGShapeType)Enum.Parse(typeof(VGShapeType), shapeType);
              aoi.TrialID = trial.ID;
              aoi.SlideNr = i;

              Queries.WriteAOIDataToDataSet(aoi, null);
            }
          }
        }

        this.ResetButtons();
      }
    }

    /// <summary>
    /// The <see cref="ToolStripButton.CheckedChanged"/> event handler for the
    ///   <see cref="ToolStripButton"/> <see cref="btnMouse"/>.
    ///   User selected to use mouse data for aoi statistic. Update gaze buttons checked state.
    /// </summary>
    /// <param name="sender">
    /// Source of the event.
    /// </param>
    /// <param name="e">
    /// An empty <see cref="EventArgs"/>
    /// </param>
    private void btnMouse_CheckedChanged(object sender, EventArgs e)
    {
      if (this.btnEye.Checked && this.btnMouse.Checked)
      {
        this.btnEye.Checked = false;
      }
      else if (!this.btnEye.Checked && !this.btnMouse.Checked)
      {
        this.btnEye.Checked = true;
      }
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler for the
    ///   <see cref="Button"/> <see cref="btnNewAOIGrid"/>.
    ///   User selected new AOI grid button, so update other shape buttons
    ///   and raise dialog to specify grid.
    /// </summary>
    /// <param name="sender">
    /// Source of the event
    /// </param>
    /// <param name="e">
    /// An empty <see cref="EventArgs"/>
    /// </param>
    private void btnNewAOIGrid_Click(object sender, EventArgs e)
    {
      // Skip if no data available
      if (this.cbbTrial.SelectedItem == null)
      {
        return;
      }

      this.btnNewRectangle.Checked = false;
      this.btnNewEllipse.Checked = false;
      this.btnNewPolyline.Checked = false;
      this.btnNewAOIGrid.Checked = true;

      var gridDialog = new AddGridDlg();

      ////Bitmap screenShot = new Bitmap(Document.ActiveDocument.PresentationSize.Width, Document.ActiveDocument.PresentationSize.Height);
      Image screenshot2 = this.aoiPicture.RenderToImage();
      gridDialog.SlideImage = screenshot2;
      if (gridDialog.ShowDialog() == DialogResult.OK)
      {
        this.aoiPicture.CreateAOIGrid(gridDialog.PreviewDataGridView);
        this.UpdatePicture();
      }
      else
      {
        this.ResetButtons();
      }
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler for the
    ///   <see cref="Button"/> <see cref="btnNewEllipse"/>.
    ///   User selected new ellipse button, so update other shape buttons
    ///   and notify picture.
    /// </summary>
    /// <param name="sender">
    /// Source of the event
    /// </param>
    /// <param name="e">
    /// An empty <see cref="EventArgs"/>
    /// </param>
    private void btnNewEllipse_Click(object sender, EventArgs e)
    {
      // Skip if no data available
      if (this.cbbTrial.SelectedItem == null)
      {
        return;
      }

      this.btnNewRectangle.Checked = false;
      this.btnNewEllipse.Checked = true;
      this.btnNewPolyline.Checked = false;
      this.btnNewAOIGrid.Checked = false;
      this.aoiPicture.NewEllipseStart(
        ShapeDrawAction.NameAndEdge, 
        null, 
        null, 
        null, 
        Color.Empty, 
        VGStyleGroup.AOI_NORMAL, 
        string.Empty);
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler for the
    ///   <see cref="Button"/> <see cref="btnNewPolyline"/>.
    ///   User selected new polyline button, so update other shape buttons
    ///   and notify picture.
    /// </summary>
    /// <param name="sender">
    /// Source of the event
    /// </param>
    /// <param name="e">
    /// An empty <see cref="EventArgs"/>
    /// </param>
    private void btnNewPolyline_Click(object sender, EventArgs e)
    {
      // Skip if no data available
      if (this.cbbTrial.SelectedItem == null)
      {
        return;
      }

      this.btnNewRectangle.Checked = false;
      this.btnNewEllipse.Checked = false;
      this.btnNewPolyline.Checked = true;
      this.btnNewAOIGrid.Checked = false;
      this.aoiPicture.NewPolylineStart(
        ShapeDrawAction.NameAndEdge, 
        null, 
        null, 
        null, 
        Color.Empty, 
        VGStyleGroup.AOI_NORMAL, 
        string.Empty);
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler for the
    ///   <see cref="Button"/> <see cref="btnNewRectangle"/>.
    ///   User selected new rectangle button, so update other shape buttons
    ///   and notify picture.
    /// </summary>
    /// <param name="sender">
    /// Source of the event
    /// </param>
    /// <param name="e">
    /// An empty <see cref="EventArgs"/>
    /// </param>
    private void btnNewRectangle_Click(object sender, EventArgs e)
    {
      // Skip if no data available
      if (this.cbbTrial.SelectedItem == null)
      {
        return;
      }

      this.btnNewRectangle.Checked = true;
      this.btnNewEllipse.Checked = false;
      this.btnNewPolyline.Checked = false;
      this.btnNewAOIGrid.Checked = false;
      this.aoiPicture.NewRectangleStart(
        ShapeDrawAction.NameAndEdge, 
        null, 
        null, 
        null, 
        Color.Empty, 
        VGStyleGroup.AOI_NORMAL, 
        string.Empty);
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler for the
    ///   <see cref="Button"/> <see cref="btnRecalculateStatistics"/>.
    ///   User selected to recalculate the statistic so do it ;-).
    /// </summary>
    /// <param name="sender">
    /// Source of the event.
    /// </param>
    /// <param name="e">
    /// An empty <see cref="EventArgs"/>
    /// </param>
    private void btnRecalculateStatistics_Click(object sender, EventArgs e)
    {
      this.UpdateStatistics();
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler for the
    ///   <see cref="Button"/> <see cref="btnSaveAOI"/>.
    ///   User selected to immediately submit the AOI changes
    ///   to the database.
    /// </summary>
    /// <param name="sender">
    /// Source of the event.
    /// </param>
    /// <param name="e">
    /// An empty <see cref="EventArgs"/>
    /// </param>
    private void btnSaveAOI_Click(object sender, EventArgs e)
    {
      this.UpdateDatabase();
      Document.ActiveDocument.DocDataSet.AOIs.AcceptChanges();
      Document.ActiveDocument.DocDataSet.ShapeGroups.AcceptChanges();
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler for the
    ///   <see cref="Button"/> <see cref="btnSeekNextSlide"/>.
    ///   Activate next slide in trial.
    /// </summary>
    /// <param name="sender">
    /// Source of the event.
    /// </param>
    /// <param name="e">
    /// An empty <see cref="EventArgs"/>
    /// </param>
    private void btnSeekNextSlide_Click(object sender, EventArgs e)
    {
      // Skip if there is no data
      if (this.CurrentTrial == null)
      {
        return;
      }

      this.trialTimeLine.HighlightNextSlide(true);
      int slideIndex = this.trialTimeLine.HighlightedSlideIndex;
      this.LoadSlide(this.CurrentTrial[slideIndex], ActiveXMode.BehindPicture);
      this.bsoAOIs.Filter = "(TrialID='" + this.CurrentTrial.ID + "' AND SlideNr='" + slideIndex + "')";
      this.UpdatePicture();
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler for the
    ///   <see cref="Button"/> <see cref="btnSelectAllSubjects"/>.
    ///   User decided to select all subjects for calculation so
    ///   check them in the check box list.
    /// </summary>
    /// <param name="sender">
    /// Source of the event.
    /// </param>
    /// <param name="e">
    /// An empty <see cref="EventArgs"/>
    /// </param>
    private void btnSelectAllSubjects_Click(object sender, EventArgs e)
    {
      foreach (TreeNode categoryNode in this.trvSubjects.Nodes)
      {
        categoryNode.Checked = true;
      }
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler for the
    ///   <see cref="Button"/> <see cref="btnShowAOIStatistics"/>.
    ///   User selected to show or hide the statistic panel, so expand or
    ///   collapse the referring split container.
    /// </summary>
    /// <param name="sender">
    /// Source of the event.
    /// </param>
    /// <param name="e">
    /// An empty <see cref="EventArgs"/>
    /// </param>
    private void btnShowAOIStatistics_Click(object sender, EventArgs e)
    {
      this.spcStatisticsPicture.Panel1Collapsed = !this.btnShowAOIStatistics.Checked;
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler for the
    ///   <see cref="Button"/> <see cref="btnShowHideTable"/>.
    ///   User selected to show or hide the data grid view, so expand or
    ///   collapse the referring split container.
    /// </summary>
    /// <param name="sender">
    /// Source of the event.
    /// </param>
    /// <param name="e">
    /// An empty <see cref="EventArgs"/>
    /// </param>
    private void btnShowHideTable_Click(object sender, EventArgs e)
    {
      this.spcPictureTools.Panel2Collapsed = !this.btnShowHideTable.Checked;
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler for the
    ///   <see cref="Button"/> <see cref="btnStyleNormal"/>.
    ///   Shows a <see cref="PenAndFontStyleDlg"/> for the normal areas of interest.
    /// </summary>
    /// <param name="sender">
    /// Source of the event.
    /// </param>
    /// <param name="e">
    /// An empty <see cref="EventArgs"/>
    /// </param>
    private void btnStyleNormal_Click(object sender, EventArgs e)
    {
      Pen backupPen = this.aoiPicture.DefaultPen;
      Font backupFont = this.aoiPicture.DefaultFonts;
      var backupBrush = new SolidBrush(this.aoiPicture.DefaultFontColor);
      VGAlignment backupAlignment = this.aoiPicture.DefaultTextAlignment;

      var dlgDefaultStyle = new PenAndFontStyleDlg();
      dlgDefaultStyle.Text = "Set normal AOI pen and font style...";
      dlgDefaultStyle.Pen = this.aoiPicture.DefaultPen;
      dlgDefaultStyle.CustomFont = this.aoiPicture.DefaultFonts;
      dlgDefaultStyle.CustomFontBrush = new SolidBrush(this.aoiPicture.DefaultFontColor);
      dlgDefaultStyle.CustomFontTextAlignment = this.aoiPicture.DefaultTextAlignment;

      dlgDefaultStyle.PenChanged += this.dlgDefaultStyle_PenChanged;
      dlgDefaultStyle.FontStyleChanged += this.dlgDefaultStyle_FontStyleChanged;
      if (dlgDefaultStyle.ShowDialog() == DialogResult.Cancel)
      {
        var ea = new PenChangedEventArgs(backupPen, VGStyleGroup.AOI_NORMAL);
        var eaf = new FontChangedEventArgs(backupFont, backupBrush.Color, backupAlignment, VGStyleGroup.AOI_NORMAL);
        this.aoiPicture.PenChanged(this, ea);
        this.aoiPicture.FontStyleChanged(this, eaf);
      }
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler for the
    ///   <see cref="Button"/> <see cref="btnStyleSearchRect"/>.
    ///   Shows a <see cref="PenAndFontStyleDlg"/> for the search rect areas of interest.
    /// </summary>
    /// <param name="sender">
    /// Source of the event.
    /// </param>
    /// <param name="e">
    /// An empty <see cref="EventArgs"/>
    /// </param>
    private void btnStyleSearchRect_Click(object sender, EventArgs e)
    {
      Pen backupPen = this.aoiPicture.SearchRectPen;
      Font backupFont = this.aoiPicture.SearchRectFont;
      var backupBrush = new SolidBrush(this.aoiPicture.SearchRectFontColor);
      VGAlignment backupAlignment = this.aoiPicture.SearchRectTextAlignment;

      var dlgSearchRectStyle = new PenAndFontStyleDlg();
      dlgSearchRectStyle.Text = "Set search rectangle AOI pen and font style...";
      dlgSearchRectStyle.Pen = this.aoiPicture.SearchRectPen;
      dlgSearchRectStyle.CustomFont = this.aoiPicture.SearchRectFont;
      dlgSearchRectStyle.CustomFontBrush = new SolidBrush(this.aoiPicture.SearchRectFontColor);
      dlgSearchRectStyle.CustomFontTextAlignment = this.aoiPicture.SearchRectTextAlignment;

      dlgSearchRectStyle.PenChanged += this.dlgSearchRectStyle_PenChanged;
      dlgSearchRectStyle.FontStyleChanged += this.dlgSearchRectStyle_FontStyleChanged;
      if (dlgSearchRectStyle.ShowDialog() == DialogResult.Cancel)
      {
        var ea = new PenChangedEventArgs(backupPen, VGStyleGroup.AOI_SEARCHRECT);
        var eaf = new FontChangedEventArgs(backupFont, backupBrush.Color, backupAlignment, VGStyleGroup.AOI_SEARCHRECT);
        this.aoiPicture.PenChanged(this, ea);
        this.aoiPicture.FontStyleChanged(this, eaf);
      }
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler for the
    ///   <see cref="Button"/> <see cref="btnStyleTarget"/>.
    ///   Shows a <see cref="PenAndFontStyleDlg"/> for the target areas of interest.
    /// </summary>
    /// <param name="sender">
    /// Source of the event.
    /// </param>
    /// <param name="e">
    /// An empty <see cref="EventArgs"/>
    /// </param>
    private void btnStyleTarget_Click(object sender, EventArgs e)
    {
      Pen backupPen = this.aoiPicture.TargetPen;
      Font backupFont = this.aoiPicture.TargetFont;
      var backupBrush = new SolidBrush(this.aoiPicture.TargetFontColor);
      VGAlignment backupAlignment = this.aoiPicture.TargetTextAlignment;

      var dlgTargetStyle = new PenAndFontStyleDlg();
      dlgTargetStyle.Text = "Set target AOI pen and font style...";
      dlgTargetStyle.Pen = this.aoiPicture.TargetPen;
      dlgTargetStyle.CustomFont = this.aoiPicture.TargetFont;
      dlgTargetStyle.CustomFontBrush = new SolidBrush(this.aoiPicture.TargetFontColor);
      dlgTargetStyle.CustomFontTextAlignment = this.aoiPicture.TargetTextAlignment;

      dlgTargetStyle.PenChanged += this.dlgTargetStyle_PenChanged;
      dlgTargetStyle.FontStyleChanged += this.dlgTargetStyle_FontStyleChanged;
      if (dlgTargetStyle.ShowDialog() == DialogResult.Cancel)
      {
        var ea = new PenChangedEventArgs(backupPen, VGStyleGroup.AOI_TARGET);
        var eaf = new FontChangedEventArgs(backupFont, backupBrush.Color, backupAlignment, VGStyleGroup.AOI_TARGET);
        this.aoiPicture.PenChanged(this, ea);
        this.aoiPicture.FontStyleChanged(this, eaf);
      }
    }

    /// <summary>
    /// The <see cref="CheckBox.CheckedChanged"/> event handler for
    ///   the <see cref="CheckBox"/> <see cref="chbHideAOIDescription"/>.
    ///   Notify the picture to hide AOI names.
    /// </summary>
    /// <param name="sender">
    /// Source of the event.
    /// </param>
    /// <param name="e">
    /// An empty <see cref="EventArgs"/>
    /// </param>
    private void chbHideAOIDescription_CheckedChanged(object sender, EventArgs e)
    {
      this.aoiPicture.HideAOIDescription = this.chbHideAOIDescription.Checked;
      this.UpdatePicture();
      this.ShowAOIStatistics();
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler for the
    ///   <see cref="ContextMenu"/> <see cref="cmnuCopyToClipboard"/>
    ///   Context menu entry. Copies selected cells in the data grid view to
    ///   clipboard
    /// </summary>
    /// <param name="sender">
    /// Source of the event
    /// </param>
    /// <param name="e">
    /// An empty <see cref="EventArgs"/>
    /// </param>
    private void cmnuCopyToClipboard_Click(object sender, EventArgs e)
    {
      try
      {
        Clipboard.SetDataObject(this.dgvAOIs.GetClipboardContent());
      }
      catch (Exception ex)
      {
        ExceptionMethods.HandleException(ex);
      }

      ((MainForm)this.MdiParent).StatusLabel.Text = "Table selection exported to clipboard.";
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler for the
    ///   <see cref="ContextMenu"/> <see cref="cmnuPasteFromClipboard"/>
    ///   Context menu entry. Pastes cells from clipboard into data grid view.
    /// </summary>
    /// <param name="sender">
    /// Source of the event
    /// </param>
    /// <param name="e">
    /// An empty <see cref="EventArgs"/>
    /// </param>
    private void cmnuPasteFromClipboard_Click(object sender, EventArgs e)
    {
      this.mainWindow_EditPaste(sender, e);
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler for the
    ///   <see cref="ContextMenu"/> <see cref="cmnuSelectAll"/>
    ///   Context menu entry. Selects all rows in the data grid view.
    /// </summary>
    /// <param name="sender">
    /// Source of the event
    /// </param>
    /// <param name="e">
    /// An empty <see cref="EventArgs"/>
    /// </param>
    private void cmnuSelectAll_Click(object sender, EventArgs e)
    {
      this.dgvAOIs.SelectAll();
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler for the
    ///   <see cref="ContextMenu"/> <see cref="cmnuSelectElement"/>
    ///   Context menu entry. Selects the element in the current row in the picture
    /// </summary>
    /// <param name="sender">
    /// Source of the event
    /// </param>
    /// <param name="e">
    /// An empty <see cref="EventArgs"/>
    /// </param>
    private void cmnuSelectElement_Click(object sender, EventArgs e)
    {
      if (this.dgvAOIs.CurrentRow != null)
      {
        string nameOfShapeToSelect =
          this.dgvAOIs.Rows[this.dgvAOIs.CurrentRow.Index].Cells["colShapeName"].Value.ToString();
        this.aoiPicture.SelectShape(nameOfShapeToSelect);
      }
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler for the
    ///   <see cref="ContextMenu"/> <see cref="cmuAddShapeGroup"/>
    ///   Context menu entry. Adds a new shape group to the database.
    /// </summary>
    /// <param name="sender">
    /// Source of the event
    /// </param>
    /// <param name="e">
    /// An empty <see cref="EventArgs"/>
    /// </param>
    private void cmuAddShapeGroup_Click(object sender, EventArgs e)
    {
      this.CallAddShapeGroup();
    }

    /// <summary>
    /// The <see cref="DataGridView.DataError"/> event handler for the
    ///   <see cref="DataGridView"/> <see cref="dgvAOIs"/>.
    ///   Data error event occured in datagridview, so handle the error,
    ///   if there is no custom handling possible, raise global policy.
    /// </summary>
    /// <param name="sender">
    /// Source of the event.
    /// </param>
    /// <param name="e">
    /// A <see cref="DataGridViewDataErrorEventArgs"/> with the exception to handle.
    /// </param>
    private void dGVAOIs_DataError(object sender, DataGridViewDataErrorEventArgs e)
    {
      // Check for spaces as entries in the dropdown column,
      // which would have raised an ArgumentException in
      // the Dropdown target column
      if (this.dgvAOIs[e.ColumnIndex, e.RowIndex].Value.ToString().Trim() == string.Empty)
      {
        this.dgvAOIs[e.ColumnIndex, e.RowIndex].Value = string.Empty;
        e.Cancel = true;
      }
      else
      {
        HandleDataGridViewError(sender, e);
      }
    }

    /// <summary>
    /// The <see cref="DataGridView.UserDeletedRow"/> event handler for the
    ///   <see cref="DataGridView"/> <see cref="dgvAOIs"/>.
    ///   Updates database when user deleted row.
    /// </summary>
    /// <param name="sender">
    /// Source of the event.
    /// </param>
    /// <param name="e">
    /// The <see cref="DataGridViewRowEventArgs"/> with the event data.
    /// </param>
    private void dGVAOIs_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
    {
      // See http://forums.microsoft.com/MSDN/ShowPost.aspx?PostID=217938&SiteID=1
      this.UpdatePicture();
    }

    /// <summary>
    /// The <see cref="DataGridView.CellValueChanged"/> event handler for the
    ///   <see cref="DataGridView"/> <see cref="dgvAOIs"/>.
    ///   Updates canvas picture.
    /// </summary>
    /// <param name="sender">
    /// Source of the event.
    /// </param>
    /// <param name="e">
    /// The <see cref="DataGridViewCellEventArgs"/> with the event data.
    /// </param>
    private void dgvAOIs_CellValueChanged(object sender, DataGridViewCellEventArgs e)
    {
      this.UpdatePicture();
    }

    /// <summary>
    /// The <see cref="OgamaControls.Dialogs.BrushStyleDlg.BrushStyleChanged"/>
    ///   event handler for the <see cref="OgamaControls.Dialogs.BrushStyleDlg"/>.
    ///   Updates picture elements by calling the pictures brush changed event handler.
    /// </summary>
    /// <param name="sender">
    /// Source of the event
    /// </param>
    /// <param name="e">
    /// A <see cref="BrushChangedEventArgs"/> with brush to change.
    /// </param>
    private void dlgArrowStyle_BrushStyleChanged(object sender, BrushChangedEventArgs e)
    {
      this.UpdateStatisticProperties(
        this.aoiPicture.BubblePen, 
        e.Brush, 
        this.aoiPicture.BubbleFont, 
        this.aoiPicture.BubbleFontColor, 
        this.aoiPicture.BubbleTextAlignment, 
        VGStyleGroup.AOI_STATISTICS_ARROW);
    }

    /// <summary>
    /// The <see cref="OgamaControls.Dialogs.FontStyleDlg.FontStyleChanged"/>
    ///   event handler for the <see cref="OgamaControls.Dialogs.PenAndFontStyleDlg"/>.
    ///   Updates picture elements by calling the pictures font changed event handler.
    /// </summary>
    /// <param name="sender">
    /// Source of the event
    /// </param>
    /// <param name="e">
    /// A <see cref="FontChangedEventArgs"/> with group and font to change.
    /// </param>
    private void dlgArrowStyle_FontStyleChanged(object sender, FontChangedEventArgs e)
    {
      this.UpdateStatisticProperties(
        this.aoiPicture.ArrowPen, 
        this.aoiPicture.ArrowBrush, 
        e.Font, 
        e.FontColor, 
        e.FontAlignment, 
        VGStyleGroup.AOI_STATISTICS_ARROW);
    }

    /// <summary>
    /// The <see cref="OgamaControls.Dialogs.PenStyleDlg.PenChanged"/>
    ///   event handler for the <see cref="OgamaControls.Dialogs.PenAndFontStyleDlg"/>.
    ///   Updates pictures elements by calling the pictures pen changed event handler.
    /// </summary>
    /// <param name="sender">
    /// Source of the event
    /// </param>
    /// <param name="e">
    /// A <see cref="PenChangedEventArgs"/> with group and pen to change.
    /// </param>
    private void dlgArrowStyle_PenChanged(object sender, PenChangedEventArgs e)
    {
      this.UpdateStatisticProperties(
        e.Pen, 
        this.aoiPicture.ArrowBrush, 
        this.aoiPicture.ArrowFont, 
        this.aoiPicture.ArrowFontColor, 
        this.aoiPicture.ArrowTextAlignment, 
        VGStyleGroup.AOI_STATISTICS_ARROW);
    }

    /// <summary>
    /// The <see cref="OgamaControls.Dialogs.BrushStyleDlg.BrushStyleChanged"/>
    ///   event handler for the <see cref="OgamaControls.Dialogs.BrushStyleDlg"/>.
    ///   Updates picture elements by calling the pictures brush changed event handler.
    /// </summary>
    /// <param name="sender">
    /// Source of the event
    /// </param>
    /// <param name="e">
    /// A <see cref="BrushChangedEventArgs"/> with brush to change.
    /// </param>
    private void dlgBubbleStyle_BrushStyleChanged(object sender, BrushChangedEventArgs e)
    {
      this.UpdateStatisticProperties(
        this.aoiPicture.BubblePen, 
        e.Brush, 
        this.aoiPicture.BubbleFont, 
        this.aoiPicture.BubbleFontColor, 
        this.aoiPicture.BubbleTextAlignment, 
        VGStyleGroup.AOI_STATISTICS_BUBBLE);
    }

    /// <summary>
    /// The <see cref="OgamaControls.Dialogs.FontStyleDlg.FontStyleChanged"/>
    ///   event handler for the <see cref="OgamaControls.Dialogs.PenAndFontStyleDlg"/>.
    ///   Updates picture elements by calling the pictures font changed event handler.
    /// </summary>
    /// <param name="sender">
    /// Source of the event
    /// </param>
    /// <param name="e">
    /// A <see cref="FontChangedEventArgs"/> with group and font to change.
    /// </param>
    private void dlgBubbleStyle_FontStyleChanged(object sender, FontChangedEventArgs e)
    {
      this.UpdateStatisticProperties(
        this.aoiPicture.BubblePen, 
        this.aoiPicture.BubbleBrush, 
        e.Font, 
        e.FontColor, 
        e.FontAlignment, 
        VGStyleGroup.AOI_STATISTICS_BUBBLE);
    }

    /// <summary>
    /// The <see cref="OgamaControls.Dialogs.PenStyleDlg.PenChanged"/>
    ///   event handler for the <see cref="OgamaControls.Dialogs.PenAndFontStyleDlg"/>.
    ///   Updates pictures elements by calling the pictures pen changed event handler.
    /// </summary>
    /// <param name="sender">
    /// Source of the event
    /// </param>
    /// <param name="e">
    /// A <see cref="PenChangedEventArgs"/> with group and pen to change.
    /// </param>
    private void dlgBubbleStyle_PenChanged(object sender, PenChangedEventArgs e)
    {
      this.UpdateStatisticProperties(
        e.Pen, 
        this.aoiPicture.BubbleBrush, 
        this.aoiPicture.BubbleFont, 
        this.aoiPicture.BubbleFontColor, 
        this.aoiPicture.BubbleTextAlignment, 
        VGStyleGroup.AOI_STATISTICS_BUBBLE);
    }

    ///////////////////////////////////////////////////////////////////////////////
    // Eventhandler for Custom Defined Events                                    //
    ///////////////////////////////////////////////////////////////////////////////

    /// <summary>
    /// The <see cref="OgamaControls.Dialogs.PenAndFontStyleDlg.FontStyleChanged"/>
    ///   event handler for the <see cref="OgamaControls.Dialogs.PenAndFontStyleDlg"/>.
    ///   Updates picture elements by calling the pictures font changed event handler.
    /// </summary>
    /// <param name="sender">
    /// Source of the event
    /// </param>
    /// <param name="e">
    /// A <see cref="FontChangedEventArgs"/> with group and font to change.
    /// </param>
    private void dlgDefaultStyle_FontStyleChanged(object sender, FontChangedEventArgs e)
    {
      e.ElementGroup = VGStyleGroup.AOI_NORMAL;
      this.aoiPicture.FontStyleChanged(sender, e);
    }

    /// <summary>
    /// The <see cref="OgamaControls.Dialogs.PenAndFontStyleDlg.PenChanged"/>
    ///   event handler for the <see cref="OgamaControls.Dialogs.PenAndFontStyleDlg"/>.
    ///   Updates pictures elements by calling the pictures pen changed event handler.
    /// </summary>
    /// <param name="sender">
    /// Source of the event
    /// </param>
    /// <param name="e">
    /// A <see cref="PenChangedEventArgs"/> with group and pen to change.
    /// </param>
    private void dlgDefaultStyle_PenChanged(object sender, PenChangedEventArgs e)
    {
      e.ElementGroup = VGStyleGroup.AOI_NORMAL;
      this.aoiPicture.PenChanged(sender, e);
    }

    /// <summary>
    /// The <see cref="OgamaControls.Dialogs.PenAndFontStyleDlg.FontStyleChanged"/>
    ///   event handler for the <see cref="OgamaControls.Dialogs.PenAndFontStyleDlg"/>.
    ///   Updates picture elements by calling the pictures font changed event handler.
    /// </summary>
    /// <param name="sender">
    /// Source of the event
    /// </param>
    /// <param name="e">
    /// A <see cref="FontChangedEventArgs"/> with group and font to change.
    /// </param>
    private void dlgSearchRectStyle_FontStyleChanged(object sender, FontChangedEventArgs e)
    {
      e.ElementGroup = VGStyleGroup.AOI_SEARCHRECT;
      this.aoiPicture.FontStyleChanged(sender, e);
    }

    /// <summary>
    /// The <see cref="OgamaControls.Dialogs.PenAndFontStyleDlg.PenChanged"/>
    ///   event handler for the <see cref="OgamaControls.Dialogs.PenAndFontStyleDlg"/>
    ///   for the search rectangles.
    ///   Updates pictures elements by calling the pictures pen changed event handler.
    /// </summary>
    /// <param name="sender">
    /// Source of the event
    /// </param>
    /// <param name="e">
    /// A <see cref="PenChangedEventArgs"/> with group and pen to change.
    /// </param>
    private void dlgSearchRectStyle_PenChanged(object sender, PenChangedEventArgs e)
    {
      e.ElementGroup = VGStyleGroup.AOI_SEARCHRECT;
      this.aoiPicture.PenChanged(sender, e);
    }

    /// <summary>
    /// The <see cref="OgamaControls.Dialogs.PenAndFontStyleDlg.FontStyleChanged"/>
    ///   event handler for the <see cref="OgamaControls.Dialogs.PenAndFontStyleDlg"/>
    ///   for the targets.
    ///   Updates picture elements by calling the pictures font changed event handler.
    /// </summary>
    /// <param name="sender">
    /// Source of the event
    /// </param>
    /// <param name="e">
    /// A <see cref="FontChangedEventArgs"/> with group and font to change.
    /// </param>
    private void dlgTargetStyle_FontStyleChanged(object sender, FontChangedEventArgs e)
    {
      e.ElementGroup = VGStyleGroup.AOI_TARGET;
      this.aoiPicture.FontStyleChanged(sender, e);
    }

    /// <summary>
    /// The <see cref="OgamaControls.Dialogs.PenAndFontStyleDlg.PenChanged"/>
    ///   event handler for the <see cref="OgamaControls.Dialogs.PenAndFontStyleDlg"/>
    ///   for the default shapes.
    ///   Updates pictures elements by calling the pictures pen changed event handler.
    /// </summary>
    /// <param name="sender">
    /// Source of the event
    /// </param>
    /// <param name="e">
    /// A <see cref="PenChangedEventArgs"/> with group and pen to change.
    /// </param>
    private void dlgTargetStyle_PenChanged(object sender, PenChangedEventArgs e)
    {
      e.ElementGroup = VGStyleGroup.AOI_TARGET;
      this.aoiPicture.PenChanged(sender, e);
    }

    /// <summary>
    /// The <see cref="Form.FormClosing"/> event handler.
    ///   Updates database, when form is closed, to avoid lengthy updating during work.
    /// </summary>
    /// <param name="sender">
    /// Source of the event.
    /// </param>
    /// <param name="e">
    /// The <see cref="FormClosingEventArgs"/> with the event data.
    /// </param>
    private void frmAOIWindow_FormClosing(object sender, FormClosingEventArgs e)
    {
      if (Document.ActiveDocument.DocDataSet.HasChanges())
      {
        DialogResult result = InformationDialog.Show(
          "Would you like to commit the changes made to the database ?", 
          "Commit changes ?", 
          true, 
          MessageBoxIcon.Question);
        switch (result)
        {
          case DialogResult.Cancel:
            e.Cancel = true;
            break;
          case DialogResult.No:
            Document.ActiveDocument.DocDataSet.AOIs.RejectChanges();
            Document.ActiveDocument.DocDataSet.ShapeGroups.RejectChanges();
            break;
          case DialogResult.Yes:
            this.UpdateDatabase();
            Document.ActiveDocument.DocDataSet.AOIs.AcceptChanges();
            Document.ActiveDocument.DocDataSet.ShapeGroups.AcceptChanges();
            break;
        }

        ((MainForm)this.MdiParent).RefreshContextPanelSubjects();
      }

      this.aoiPicture.SaveStylesToApplicationSettings();
    }

    /// <summary>
    /// The <see cref="Form.Load"/> event handler.
    ///   Wires Mainform events and initializes vector graphics picture.
    /// </summary>
    /// <param name="sender">
    /// Source of the event.
    /// </param>
    /// <param name="e">
    /// An empty <see cref="EventArgs"/>
    /// </param>
    private void frmAOIWindow_Load(object sender, EventArgs e)
    {
      try
      {
        // Initialize picture
        this.aoiPicture.PresentationSize = Document.ActiveDocument.PresentationSize;
        this.aoiPicture.BubbleFactor = (float)this.nudBubbleFactor.Value;
        this.aoiPicture.ArrowFactor = (float)this.nudArrowFactor.Value;

        this.chbBubblePen.Checked = (this.aoiPicture.BubbleDrawAction & ShapeDrawAction.Edge) == ShapeDrawAction.Edge;
        this.chbBubbleBrush.Checked = (this.aoiPicture.BubbleDrawAction & ShapeDrawAction.Fill) == ShapeDrawAction.Fill;
        this.chbBubbleFont.Checked = (this.aoiPicture.BubbleDrawAction & ShapeDrawAction.Name) == ShapeDrawAction.Name;

        this.chbArrowPen.Checked = (this.aoiPicture.ArrowDrawAction & ShapeDrawAction.Edge) == ShapeDrawAction.Edge;
        this.chbArrowBrush.Checked = (this.aoiPicture.ArrowDrawAction & ShapeDrawAction.Fill) == ShapeDrawAction.Fill;
        this.chbArrowFont.Checked = (this.aoiPicture.ArrowDrawAction & ShapeDrawAction.Name) == ShapeDrawAction.Name;

        this.ResizeCanvas();

        // Show first stimulus picture with AOIs
        this.InitialDisplay();

        // Resize picture
        this.pnlPicture.Bounds = this.GetProportionalBounds(this.pnlCanvas);
      }
      catch (Exception ex)
      {
        ExceptionMethods.HandleException(ex);
      }
    }

    /// <summary>
    /// The <see cref="NumericUpDown.ValueChanged"/> event handler for
    ///   the <see cref="NumericUpDown"/> <see cref="nudArrowFactor"/>.
    ///   Updates the pictures arrow factor.
    /// </summary>
    /// <param name="sender">
    /// Source of the event.
    /// </param>
    /// <param name="e">
    /// An empty <see cref="EventArgs"/>
    /// </param>
    private void nudArrowFactor_ValueChanged(object sender, EventArgs e)
    {
      this.aoiPicture.ArrowFactor = (float)this.nudArrowFactor.Value;
      this.ShowAOIStatistics();
    }

    /// <summary>
    /// The <see cref="NumericUpDown.ValueChanged"/> event handler for
    ///   the <see cref="NumericUpDown"/> <see cref="nudBubbleFactor"/>.
    ///   Updates the pictures bubble factor.
    /// </summary>
    /// <param name="sender">
    /// Source of the event.
    /// </param>
    /// <param name="e">
    /// An empty <see cref="EventArgs"/>
    /// </param>
    private void nudBubbleFactor_ValueChanged(object sender, EventArgs e)
    {
      this.aoiPicture.BubbleFactor = (float)this.nudBubbleFactor.Value;
      this.ShowAOIStatistics();
    }

    /// <summary>
    /// The <see cref="RadioButton.CheckedChanged"/> event handler for
    ///   the <see cref="RadioButton"/>s.
    ///   Updates the selected statistic mode.
    /// </summary>
    /// <param name="sender">
    /// Source of the event.
    /// </param>
    /// <param name="e">
    /// An empty <see cref="EventArgs"/>
    /// </param>
    private void rdbStatistic_CheckedChanged(object sender, EventArgs e)
    {
      if (((RadioButton)sender).Checked)
      {
        this.ShowAOIStatistics();
      }
    }

    /// <summary>
    /// The <see cref="TreeView.AfterCheck"/> event handler for the
    ///   <see cref="TreeView"/> <see cref="trvSubjects"/>.
    ///   Checks or unchecks all subjects in the category node
    ///   that is clicked.
    /// </summary>
    /// <param name="sender">
    /// Source of the event.
    /// </param>
    /// <param name="e">
    /// An empty <see cref="EventArgs"/>
    /// </param>
    private void trvSubjects_AfterCheck(object sender, TreeViewEventArgs e)
    {
      if (e.Node.ImageKey == "Category")
      {
        foreach (TreeNode subjectNode in e.Node.Nodes)
        {
          subjectNode.Checked = e.Node.Checked;
        }
      }
    }

    #endregion
  }
}