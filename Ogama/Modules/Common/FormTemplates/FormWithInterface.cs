// <copyright file="FormWithInterface.cs" company="FU Berlin">
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

namespace Ogama.Modules.Common.FormTemplates
{
  using System;
  using System.Collections.Generic;
  using System.Data;
  using System.Drawing;
  using System.Windows.Forms;

  using Ogama.MainWindow;

  /// <summary>
  /// Inherits <see cref="FormWithAccellerators"/>.
  /// This is the base class for all modules and provides
  /// methods and members for standard jobs and database connection.
  /// </summary>
  public partial class FormWithInterface : FormWithAccellerators
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
    /// Filename of rich text format helpfile for this form.
    /// </summary>
    /// <remarks>Will be displayed in the context frame of the main window.</remarks>
    private string helpRTF;

    /// <summary>
    /// Bitmap with the logo or icon of the form.
    /// </summary>
    /// <remarks>Is used in the context frame of the main window for the help section.</remarks>
    private Bitmap logoBitmap;

    #endregion //FIELDS

    ///////////////////////////////////////////////////////////////////////////////
    // Construction and Initializing methods                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region CONSTRUCTION

    /// <summary>
    /// Initializes a new instance of the FormWithInterface class.
    /// Initializes read only style.
    /// </summary>
    public FormWithInterface()
    {
      this.InitializeComponent();
    }

    #endregion //CONSTRUCTION

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Properties                                                       //
    ///////////////////////////////////////////////////////////////////////////////
    #region PROPERTIES

    /// <summary>
    /// Gets or sets rich text file name with help information
    /// </summary>
    /// <value>A <see cref="string"/> with the rtf formatted help text.</value>
    public string HelpRTF
    {
      get { return this.helpRTF; }
      set { this.helpRTF = value; }
    }

    /// <summary>
    /// Gets or sets Logo bitmap for the form to display in the help context window.
    /// </summary>
    /// <value>A <see cref="Bitmap"/> with the logo to use for the help context window.</value>
    public Bitmap Logo
    {
      get { return this.logoBitmap; }
      set { this.logoBitmap = value; }
    }

    #endregion //PROPERTIES

    ///////////////////////////////////////////////////////////////////////////////
    // Inherited methods                                                         //
    ///////////////////////////////////////////////////////////////////////////////
    #region OVERRIDES

    /// <summary>
    /// Causes the controls bound to the BindingSources to reread all the 
    /// items in the list and refresh their displayed values. 
    /// </summary>
    public virtual void ResetDataBindings()
    {
      if (this.InvokeRequired)
      {
        MethodInvoker resetMethod = new MethodInvoker(this.ResetDataBindings);
        this.Invoke(resetMethod);
        return;
      }

      this.bsoSubjects.ResetBindings(false);
      this.bsoSubjectParameters.ResetBindings(false);
      this.bsoParams.ResetBindings(false);
      this.bsoTrials.ResetBindings(false);
      this.bsoTrialEvents.ResetBindings(false);
      this.bsoGazeFixations.ResetBindings(false);
      this.bsoMouseFixations.ResetBindings(false);
      this.bsoAOIs.ResetBindings(false);
      this.bsoShapeGroups.ResetBindings(false);

      this.bsoFKSubjectsSubjectParameters.ResetBindings(false);
      this.bsoFKSubjectsTrials.ResetBindings(false);
      this.bsoFKTrialsEvents.ResetBindings(false);
      this.bsoTrialsGazeFixations.ResetBindings(false);
      this.bsoTrialsMouseFixations.ResetBindings(false);
      this.bsoTrialsAOIs.ResetBindings(false);
    }

    /// <summary>
    /// Initialize drop down controls.
    /// </summary>
    /// <remarks>The toolstrip combo box does currently not know the
    /// <see cref="ComboBox.SelectionChangeCommitted"/> event, so here we initialize it
    /// from the <see cref="ToolStripComboBox.ComboBox"/> member.</remarks>
    protected virtual void InitializeDropDowns()
    {
    }

    /// <summary>
    /// Unregister custom events.
    /// </summary>
    protected virtual void CustomDispose()
    {
      if (this.MdiParent != null)
      {
        ((MainForm)this.MdiParent).StimulusChanged -= new EventHandler(this.mainWindow_StimulusChanged);
        ((MainForm)this.MdiParent).EditCopy -= new EventHandler(this.mainWindow_EditCopy);
        ((MainForm)this.MdiParent).EditPaste -= new EventHandler(this.mainWindow_EditPaste);
        ((MainForm)this.MdiParent).EditSaveImage -= new EventHandler(this.mainWindow_EditSaveImage);
        ((MainForm)this.MdiParent).OptionsChanged -= new EventHandler(this.mainWindow_OptionsChanged);
      }
    }

    /// <summary>
    /// This method initializes the standard subjects and foreign key
    /// subject-trial <see cref="BindingSource"/>.
    /// Any overrides should initializes additional data bindings,
    /// like setting <see cref="DataGridView"/> sources to the <see cref="BindingSource"/>s.
    /// </summary>
    protected virtual void InitializeDataBindings()
    {
      if (Document.ActiveDocument != null)
      {
        ((System.ComponentModel.ISupportInitialize)this.bsoSubjects).BeginInit();
        ((System.ComponentModel.ISupportInitialize)this.bsoSubjectParameters).BeginInit();
        ((System.ComponentModel.ISupportInitialize)this.bsoParams).BeginInit();
        ((System.ComponentModel.ISupportInitialize)this.bsoTrials).BeginInit();
        ((System.ComponentModel.ISupportInitialize)this.bsoTrialEvents).BeginInit();
        ((System.ComponentModel.ISupportInitialize)this.bsoGazeFixations).BeginInit();
        ((System.ComponentModel.ISupportInitialize)this.bsoMouseFixations).BeginInit();
        ((System.ComponentModel.ISupportInitialize)this.bsoAOIs).BeginInit();
        ((System.ComponentModel.ISupportInitialize)this.bsoShapeGroups).BeginInit();

        ((System.ComponentModel.ISupportInitialize)this.bsoFKSubjectsSubjectParameters).BeginInit();
        ((System.ComponentModel.ISupportInitialize)this.bsoFKSubjectsTrials).BeginInit();
        ((System.ComponentModel.ISupportInitialize)this.bsoFKTrialsEvents).BeginInit();
        ((System.ComponentModel.ISupportInitialize)this.bsoTrialsGazeFixations).BeginInit();
        ((System.ComponentModel.ISupportInitialize)this.bsoTrialsMouseFixations).BeginInit();
        ((System.ComponentModel.ISupportInitialize)this.bsoTrialsAOIs).BeginInit();

        this.bsoSubjects.DataMember = "Subjects";
        this.bsoSubjects.DataSource = Document.ActiveDocument.DocDataSet;

        this.bsoSubjectParameters.DataMember = "SubjectParameters";
        this.bsoSubjectParameters.DataSource = Document.ActiveDocument.DocDataSet;

        this.bsoParams.DataMember = "Params";
        this.bsoParams.DataSource = Document.ActiveDocument.DocDataSet;

        this.bsoTrials.DataMember = "Trials";
        this.bsoTrials.DataSource = Document.ActiveDocument.DocDataSet;

        this.bsoTrialEvents.DataMember = "TrialEvents";
        this.bsoTrialEvents.DataSource = Document.ActiveDocument.DocDataSet;

        this.bsoGazeFixations.DataMember = "GazeFixations";
        this.bsoGazeFixations.DataSource = Document.ActiveDocument.DocDataSet;

        this.bsoMouseFixations.DataMember = "MouseFixations";
        this.bsoMouseFixations.DataSource = Document.ActiveDocument.DocDataSet;

        this.bsoAOIs.DataMember = "AOIs";
        this.bsoAOIs.DataSource = Document.ActiveDocument.DocDataSet;

        this.bsoShapeGroups.DataMember = "ShapeGroups";
        this.bsoShapeGroups.DataSource = Document.ActiveDocument.DocDataSet;

        this.bsoFKSubjectsSubjectParameters.DataMember = "FK_Subjects_SubjectParameters";
        this.bsoFKSubjectsSubjectParameters.DataSource = this.bsoSubjects;

        this.bsoFKSubjectsTrials.DataMember = "FK_Subjects_Trials";
        this.bsoFKSubjectsTrials.DataSource = this.bsoSubjects;

        this.bsoFKTrialsEvents.DataMember = "FK_Trials_Events";
        this.bsoFKTrialsEvents.DataSource = this.bsoFKSubjectsTrials;

        this.bsoTrialsGazeFixations.DataMember = "Trials_GazeFixations";
        this.bsoTrialsGazeFixations.DataSource = this.bsoFKSubjectsTrials;

        this.bsoTrialsMouseFixations.DataMember = "Trials_MouseFixations";
        this.bsoTrialsMouseFixations.DataSource = this.bsoFKSubjectsTrials;

        this.bsoTrialsAOIs.DataMember = "Trials_AOIs";
        this.bsoTrialsAOIs.DataSource = this.bsoFKSubjectsTrials;

        ((System.ComponentModel.ISupportInitialize)this.bsoSubjects).EndInit();
        ((System.ComponentModel.ISupportInitialize)this.bsoSubjectParameters).EndInit();
        ((System.ComponentModel.ISupportInitialize)this.bsoParams).EndInit();
        ((System.ComponentModel.ISupportInitialize)this.bsoTrials).EndInit();
        ((System.ComponentModel.ISupportInitialize)this.bsoTrialEvents).EndInit();
        ((System.ComponentModel.ISupportInitialize)this.bsoGazeFixations).EndInit();
        ((System.ComponentModel.ISupportInitialize)this.bsoMouseFixations).EndInit();
        ((System.ComponentModel.ISupportInitialize)this.bsoAOIs).EndInit();
        ((System.ComponentModel.ISupportInitialize)this.bsoShapeGroups).EndInit();

        ((System.ComponentModel.ISupportInitialize)this.bsoFKSubjectsSubjectParameters).EndInit();
        ((System.ComponentModel.ISupportInitialize)this.bsoFKSubjectsTrials).EndInit();
        ((System.ComponentModel.ISupportInitialize)this.bsoFKTrialsEvents).EndInit();
        ((System.ComponentModel.ISupportInitialize)this.bsoTrialsGazeFixations).EndInit();
        ((System.ComponentModel.ISupportInitialize)this.bsoTrialsMouseFixations).EndInit();
        ((System.ComponentModel.ISupportInitialize)this.bsoTrialsAOIs).EndInit();
      }
    }

    /// <summary>
    /// Overrides should intitalizes custom events and properties that are not
    /// initialized in the designer.
    /// </summary>
    protected virtual void InitializeCustomElements()
    {
    }

    /// <summary>
    /// This method populates the given subject treeview with
    /// the subjects from the subjects table
    /// </summary>
    /// <param name="treeView">The <see cref="TreeView"/> to 
    /// be populated with the subjects.</param>
    public static void PopulateSubjectTreeView(TreeView treeView)
    {
      treeView.BeginUpdate();
      treeView.Nodes.Clear();

      DataView subjectsView = new DataView(Document.ActiveDocument.DocDataSet.Subjects);
      subjectsView.Sort = "SubjectName ASC";

      // Populates a TreeView control with subject category nodes. 
      foreach (DataRowView row in subjectsView)
      {
        string entry = row["Category"].ToString();
        if (entry == string.Empty || entry == " ")
        {
          entry = "no category";
        }

        if (!treeView.Nodes.ContainsKey(entry))
        {
          treeView.Nodes.Add(entry, entry, "Category");
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
        treeView.Nodes[category].Nodes.Add(subjectName, subjectName, "Subject", "Subject");
      }

      treeView.ExpandAll();

      foreach (TreeNode categoryNode in treeView.Nodes)
      {
        categoryNode.Checked = true;
      }

      treeView.EndUpdate();
    }

    /// <summary>
    /// Returns a list of the text properties of the checked nodes
    /// in the given <see cref="TreeView"/>
    /// </summary>
    /// <param name="treeView">The <see cref="TreeView"/> that is 
    /// populated with subjects.</param>
    /// <returns>A <see cref="List{String}"/> with the 
    /// checked subjects names of the given treeview.</returns>
    public static List<string> GetCheckedSubjects(TreeView treeView)
    {
      List<string> checkedSubjects = new List<string>();

      foreach (TreeNode categoryNode in treeView.Nodes)
      {
        foreach (TreeNode subjectNode in categoryNode.Nodes)
        {
          if (subjectNode.Checked)
          {
            checkedSubjects.Add(subjectNode.Text);
          }
        }
      }

      return checkedSubjects;
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
    /// The <see cref="Control.Click"/> event handler for the
    /// <see cref="Button"/> btnHelp, that has to be in each derived form.
    /// User pressed help button, so bring help tab in context panel to front
    /// by calling <see cref="Control.OnHelpRequested(HelpEventArgs)"/> which raises the 
    /// <see cref="Control.HelpRequested"/> event.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    protected void btnHelp_Click(object sender, EventArgs e)
    {
      this.RaiseHelp();
    }

    #endregion //WINDOWSEVENTHANDLER

    ///////////////////////////////////////////////////////////////////////////////
    // Eventhandler for Custom Defined Events                                    //
    ///////////////////////////////////////////////////////////////////////////////
    #region CUSTOMEVENTHANDLER

    /// <summary>
    /// The <see cref="MainForm.OptionsChanged"/> event handler.
    /// This method reinitializes the picture with the new experiment
    /// settings.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    protected virtual void mainWindow_OptionsChanged(object sender, EventArgs e)
    {
    }

    /// <summary>
    /// The <see cref="MainForm.StimulusChanged"/> event handler for the
    /// parent <see cref="Form"/> <see cref="MainForm"/>.
    /// This method handles the stimulus changed event from the main form
    /// and updates the form and the picture.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    protected virtual void mainWindow_StimulusChanged(object sender, EventArgs e)
    {
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
    protected virtual void mainWindow_EditCopy(object sender, EventArgs e)
    {
    }

    /// <summary>
    /// The <see cref="MainForm.EditPaste"/> event handler for the
    /// parent <see cref="Form"/> <see cref="MainForm"/>.
    /// No default implementation. Should be implemented in overrides.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    protected virtual void mainWindow_EditPaste(object sender, EventArgs e)
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
    protected virtual void mainWindow_EditSaveImage(object sender, EventArgs e)
    {
    }

    /// <summary>
    /// User pressed help button, so bring help tab in context panel to front
    /// by calling <see cref="Control.OnHelpRequested(HelpEventArgs)"/> which raises the 
    /// <see cref="Control.HelpRequested"/> event.
    /// </summary>
    protected void RaiseHelp()
    {
      this.OnHelpRequested(new HelpEventArgs(new Point(0, 0)));
    }

    /// <summary>
    /// The <see cref="Form.Load"/> event handler.
    /// Wires cut, copy paste events.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void FormWithPicture_Load(object sender, EventArgs e)
    {
      if (this.MdiParent != null)
      {
        // Wires the OnStimulusChanged method to the StimulusChanged event.
        ((MainForm)this.MdiParent).StimulusChanged += new EventHandler(this.mainWindow_StimulusChanged);

        // Wires the OnEditCopy method to the EditCopy event.
        ((MainForm)this.MdiParent).EditCopy += new EventHandler(this.mainWindow_EditCopy);

        // Wires the OnEditPaste method to the EditPaste event.
        ((MainForm)this.MdiParent).EditPaste += new EventHandler(this.mainWindow_EditPaste);

        // Wires the OnEditSaveImage method to the EditSaveImage event.
        ((MainForm)this.MdiParent).EditSaveImage += new EventHandler(this.mainWindow_EditSaveImage);

        // Wires the OnEditSaveImage method to the EditSaveImage event. 
        ((MainForm)this.MdiParent).OptionsChanged += new EventHandler(this.mainWindow_OptionsChanged);
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
    #endregion //METHODS

    ///////////////////////////////////////////////////////////////////////////////
    // Small helping Methods                                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region HELPER
    #endregion //HELPER
  }
}
