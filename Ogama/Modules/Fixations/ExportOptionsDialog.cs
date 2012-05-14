// <copyright file="ExportOptionsDialog.cs" company="FU Berlin">
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

namespace Ogama.Modules.Fixations
{
  using System;
  using System.Data;
  using System.Windows.Forms;

  using Ogama.Modules.Common;
  using Ogama.Modules.Common.FormTemplates;
  using Ogama.Modules.Statistics;

  /// <summary>
  /// A pop up dialog <see cref="FormWithInterface"/> to ask user what to 
  /// export from the fixation table.
  /// </summary>
  public partial class ExportOptionsDialog : FormWithInterface
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
    /// The field with the options created in this dialog.
    /// </summary>
    private ExportOptions exportOptions;

    #endregion //FIELDS

    ///////////////////////////////////////////////////////////////////////////////
    // Construction and Initializing methods                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region CONSTRUCTION

    /// <summary>
    /// Initializes a new instance of the ExportOptionsDialog class.
    /// </summary>
    public ExportOptionsDialog()
    {
      this.InitializeComponent();

      this.InitAccelerators();
      this.InitializeDataBindings();
      this.InitializeCustomElements();
    }

    #endregion //CONSTRUCTION

    ///////////////////////////////////////////////////////////////////////////////
    // Defining events, enums, delegates                                         //
    ///////////////////////////////////////////////////////////////////////////////
    #region EVENTS
    #endregion EVENTS

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Properties                                                       //
    ///////////////////////////////////////////////////////////////////////////////
    #region PROPERTIES

    /// <summary>
    /// Gets or sets the <see cref="ExportOptions"/> defined in this dialog.
    /// </summary>
    /// <value>A <see cref="ExportOptions"/> with the options for the export of
    /// the fixations.</value>
    public ExportOptions ExportOptions
    {
      get { return this.exportOptions; }
      set { this.exportOptions = value; }
    }

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
    /// Overridden. This method is used to initialize elements that are not
    /// initialized in the designer.
    /// </summary>
    protected override void InitializeCustomElements()
    {
      base.InitializeCustomElements();
      this.exportOptions = new ExportOptions();
      PopulateSubjectTreeView(this.trvSubjects);

      DataTable table = Document.ActiveDocument.DocDataSet.TrialsAdapter.GetData();
      StatisticsModule.FillTreeView(this.trvTrialsDefault, table);
    }

    #endregion //OVERRIDES

    ///////////////////////////////////////////////////////////////////////////////
    // Eventhandler                                                              //
    ///////////////////////////////////////////////////////////////////////////////
    #region EVENTHANDLER

    /// <summary>
    /// The <see cref="Control.Click"/> event handler for the
    /// <see cref="Button"/> <see cref="btnOK"/>.
    /// </summary>
    /// <param name="sender">The sender of the event</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void btnOK_Click(object sender, EventArgs e)
    {
      if (this.chbGaze.Checked)
      {
        this.sfdExport.Title = "Please specify gaze fixations export file name ...";
        if (this.sfdExport.ShowDialog() == DialogResult.OK)
        {
          this.exportOptions.GazeFileName = this.sfdExport.FileName;
          this.exportOptions.ExportGaze = true;
        }
      }

      if (this.chbMouse.Checked)
      {
        this.sfdExport.Title = "Please specify mouse fixations export file name ...";
        if (this.sfdExport.ShowDialog() == DialogResult.OK)
        {
          this.exportOptions.MouseFileName = this.sfdExport.FileName;
          this.exportOptions.ExportMouse = true;
        }
      }

      if (this.chbSubjectDetail.Checked)
      {
        this.exportOptions.ExportSubjectDetails = true;
      }

      if (this.chbTrialDetail.Checked)
      {
        this.exportOptions.ExportTrialDetails = true;
      }

      if (this.chbAOIInfo.Checked)
      {
        this.exportOptions.ExportAOIDetails = true;
      }

      if (this.rdbFixations.Checked)
      {
        this.exportOptions.ExportFixations = true;
      }
      else
      {
        this.exportOptions.ExportFixations = false;
      }

      this.exportOptions.CheckedSubjects = GetCheckedSubjects(this.trvSubjects);
      this.exportOptions.CheckedTrialIDs = StatisticsModule.GetSelectedTrials(this.trvTrialsDefault);
    }

    /// <summary>
    /// The <see cref="CheckBox.CheckedChanged"/> event handler for the
    /// <see cref="CheckBox"/> <see cref="chbGaze"/> and <see cref="chbMouse"/>.
    /// Be sure to have at least one check box selected.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void chbGazeMouse_CheckedChanged(object sender, EventArgs e)
    {
      if (!this.chbGaze.Checked && !this.chbMouse.Checked)
      {
        this.chbGaze.Checked = true;
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
      if (e.Node.ImageKey == "Category")
      {
        foreach (TreeNode subjectNode in e.Node.Nodes)
        {
          subjectNode.Checked = e.Node.Checked;
        }
      }
    }

    /// <summary>
    /// The <see cref="TreeView.AfterCheck"/> event handler for the
    /// <see cref="TreeView"/> <see cref="trvTrialsDefault"/>.
    /// Checks or unchecks all trials in the category node
    /// that is clicked.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void trvTrialsDefault_AfterCheck(object sender, TreeViewEventArgs e)
    {
      // Category Level
      if (e.Node.Level == 0)
      {
        foreach (TreeNode stimulusNode in e.Node.Nodes)
        {
          stimulusNode.Checked = e.Node.Checked;
        }
      }
    }

    #endregion //EVENTHANDLER

    ///////////////////////////////////////////////////////////////////////////////
    // Methods and Eventhandling for Background tasks                            //
    ///////////////////////////////////////////////////////////////////////////////
    #region THREAD
    #endregion //THREAD

    ///////////////////////////////////////////////////////////////////////////////
    // Methods for doing main class job                                          //
    ///////////////////////////////////////////////////////////////////////////////
    #region PRIVATEMETHODS
    #endregion //PRIVATEMETHODS

    ///////////////////////////////////////////////////////////////////////////////
    // Small helping Methods                                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region HELPER
    #endregion //HELPER
  }
}