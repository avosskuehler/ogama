// <copyright file="FixationsModule.cs" company="FU Berlin">
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

namespace Ogama.Modules.Charts
{
  using System;
  using System.Collections.Generic;
  using System.Drawing;
  using System.IO;
  using System.Windows.Forms;

  using Ogama.ExceptionHandling;
  using Ogama.MainWindow;
  using Ogama.Modules.Common.FormTemplates;
  using Ogama.Modules.Common.Tools;
  using Ogama.Modules.Common.Types;
  using Ogama.Properties;

  using OxyPlot;
  using OxyPlot.Axes;
  using OxyPlot.Series;
  using OxyPlot.WindowsForms;

  /// <summary>
  /// Derived from <see cref="FormWithSubjectAndTrialSelection"/>.
  /// This <see cref="Form"/> is the chart module. 
  /// </summary>
  public partial class ChartModule : FormWithSubjectAndTrialSelection
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

    private PlotView Plot;

    private List<DataPoint> dataPoints;

    #endregion //FIELDS

    ///////////////////////////////////////////////////////////////////////////////
    // Construction and Initializing methods                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region CONSTRUCTION

    /// <summary>
    /// Initializes a new instance of the FixationsModule class.
    /// </summary>
    public ChartModule()
    {
      // Init
      this.InitializeComponent();

      this.SubjectCombo = this.cbbSubject;
      this.TrialCombo = this.cbbTrial;

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
      this.btnHelp.Click += new EventHandler(this.btnHelp_Click);

      this.Plot = new PlotView();
      this.Plot.Model = new PlotModel();
      this.Plot.Dock = DockStyle.Fill;
      this.spcChartOptions.Panel1.Controls.Add(this.Plot);

      this.Plot.Model.PlotType = PlotType.XY;
      this.Plot.Model.Background = OxyColors.White;
      this.Plot.Model.TextColor = OxyColors.Black;

      // Create Line series
      this.dataPoints=new List<DataPoint>();
      this.dataPoints.Add(new DataPoint(2, 7));
      this.dataPoints.Add(new DataPoint(7, 9));
      this.dataPoints.Add(new DataPoint(9, 4));

      var s1 = new LineSeries { Title = "LineSeries", StrokeThickness = 1 };
      s1.Points.AddRange(this.dataPoints);

      // add Series and Axis to plot model
      this.Plot.Model.Series.Add(s1);
    }

    /// <summary>
    /// Reads dropdown settings and loads corresponding images and data from database.
    /// Then notifys picture the changes.
    /// </summary>
    /// <returns><strong>True</strong> if trial could be successfully loaded,
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
          using (var memStream = new MemoryStream())
          {
            var pngExporter = new PngExporter();
            pngExporter.Export(this.Plot.ActualModel, memStream);
            var fromStream = Image.FromStream(memStream);
            Clipboard.SetImage(fromStream);
          }
          ((MainForm)this.MdiParent).StatusLabel.Text = "Image exported to clipboard.";
        }
        catch (Exception ex)
        {
          ExceptionMethods.HandleException(ex);
        }
      }
    }

    /// <summary>
    /// Initializes accelerator keys. Binds to methods.
    /// </summary>
    protected override void InitAccelerators()
    {
      base.InitAccelerators();
      this.SetAccelerator(Keys.F, null);
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
    private void ChartModule_Load(object sender, EventArgs e)
    {
      try
      {
        this.InitialDisplay();
      }
      catch (Exception ex)
      {
        ExceptionMethods.HandleException(ex);
      }
    }

    #endregion //WINDOWSEVENTHANDLER

    private void txbDiagramTitle_TextChanged(object sender, EventArgs e)
    {
      this.Plot.ActualModel.Title = this.txbDiagramTitle.Text;
      this.Plot.Refresh();
    }

    private void txbLegend_TextChanged(object sender, EventArgs e)
    {
      this.Plot.ActualModel.LegendTitle = this.txbLegend.Text;
      this.Plot.Refresh();
    }

    private void chbShowLegend_CheckedChanged(object sender, EventArgs e)
    {
      this.Plot.ActualModel.IsLegendVisible = this.chbShowLegend.Checked;
      this.Plot.Refresh();
    }

    private void txbSeriesTitle_TextChanged(object sender, EventArgs e)
    {
      this.Plot.ActualModel.Series[0].Title = this.txbSeriesTitle.Text;
      this.Plot.Refresh();
    }

    private void rdbLegendPosition_CheckedChanged(object sender, EventArgs e)
    {
      this.Plot.ActualModel.LegendPlacement = this.rdbLegendPositionInside.Checked
                                                ? LegendPlacement.Inside
                                                : LegendPlacement.Outside;
      this.Plot.Refresh();
    }

    private void rdbChartType_CheckedChanged(object sender, EventArgs e)
    {

      if (rdbChartTypeArea.Checked)
      {
      var s1 = new AreaSeries { Title = this.txbDiagramTitle.Text, StrokeThickness = 1 };
      s1.Points.AddRange(this.dataPoints);
      }
      //else if (rdbChartTypeBubble.Checked)
      //{
      //  var s1 = new BarSeries { Title = this.txbDiagramTitle.Text, StrokeThickness = 1 };
      //  s1.Points.AddRange(this.dataPoints);
      //}
      //else if (rdbChartTypeColumn.Checked)
      //{
      //  var s1 = new ColumnSeries { Title = this.txbDiagramTitle.Text, StrokeThickness = 1 };
      //  s1.Points.AddRange(this.dataPoints);
      //}
      else if (rdbChartTypeLine.Checked)
      {
        var s1 = new LineSeries { Title = this.txbDiagramTitle.Text, StrokeThickness = 1 };
        s1.Points.AddRange(this.dataPoints);
      }
      //else if (rdbChartTypePie.Checked)
      //{
      //  var s1 = new PieSeries { Title = this.txbDiagramTitle.Text, StrokeThickness = 1 };
      //  s1.Points.AddRange(this.dataPoints);
      //}
      //else if (rdbChartTypeScatter.Checked)
      //{
      //  var s1 = new ScatterSeries { Title = this.txbDiagramTitle.Text};
      //  s1.Points.AddRange(this.dataPoints);
      //}
    }

    ///////////////////////////////////////////////////////////////////////////////
    // Eventhandler for Custom Defined Events                                    //
    ///////////////////////////////////////////////////////////////////////////////
    #region CUSTOMEVENTHANDLER
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

    #endregion //METHODS

    ///////////////////////////////////////////////////////////////////////////////
    // Small helping Methods                                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region HELPER
    #endregion //HELPER
  }
}