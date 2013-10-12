// <copyright file="FormWithSubjectAndTrialSelection.cs" company="FU Berlin">
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

namespace Ogama.Modules.Common.FormTemplates
{
  using System;
  using System.Collections.Generic;
  using System.ComponentModel;
  using System.Data;
  using System.Windows.Forms;

  using Ogama.ExceptionHandling;
  using Ogama.Modules.Common.SlideCollections;
  using Ogama.Modules.Common.Tools;

  /// <summary>
  /// Inherits <see cref="FormWithPicture"/>.
  /// This class extends the base class <see cref="FormWithPicture"/> to implement a 
  /// subject and trial selection common handling. It should be used for modules
  /// that display subject and trial specific data like replay or individual fixations.
  /// </summary>
  public class FormWithSubjectAndTrialSelection : FormWithPicture
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
    /// Saves the subject drop drown combo box.
    /// </summary>
    private ToolStripComboBox subjectCombo;

    /// <summary>
    /// Saves the trial drop down combo box.
    /// </summary>
    private ToolStripComboBox trialCombo;

    #endregion //FIELDS

    ///////////////////////////////////////////////////////////////////////////////
    // Construction and Initializing methods                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region CONSTRUCTION

    /// <summary>
    /// Initializes a new instance of the FormWithSubjectAndTrialSelection class.
    /// </summary>
    public FormWithSubjectAndTrialSelection()
    {
    }

    #endregion //CONSTRUCTION

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Properties                                                       //
    ///////////////////////////////////////////////////////////////////////////////
    #region PROPERTIES

    /// <summary>
    /// Gets or sets the subject drop drown combo box.
    /// </summary>
    /// <value>The <see cref="ToolStripComboBox"/> that is the subject drop drown combo box of the form.</value>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public ToolStripComboBox SubjectCombo
    {
      get { return this.subjectCombo; }
      set { this.subjectCombo = value; }
    }

    /// <summary>
    /// Gets or sets the trial drop drown combo box.
    /// </summary>
    /// <value>The <see cref="ToolStripComboBox"/> that is the trial drop drown combo box of the form.</value>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public ToolStripComboBox TrialCombo
    {
      get { return this.trialCombo; }
      set { this.trialCombo = value; }
    }

    #endregion //PROPERTIES

    ///////////////////////////////////////////////////////////////////////////////
    // Public methods                                                            //
    ///////////////////////////////////////////////////////////////////////////////
    #region PUBLICMETHODS

    /// <summary>
    /// This method sends the valid values from the given <see cref="ToolStripComboBox"/>es
    /// to the <see cref="Document"/>s <see cref="Document.SelectionState"/>.
    /// </summary>
    protected void UpdateDocumentSelectionState()
    {
      string subjectEntry = this.subjectCombo.Text;
      SequencedTrial trial = (SequencedTrial)this.trialCombo.SelectedItem;
      Document.ActiveDocument.SelectionState.Update(subjectEntry, trial.TrialID, trial.SequenceNumber, null);
    }

    /// <summary>
    /// Reads trial list for given subject and writes
    /// custom dropdownlist entrys in the form
    /// TrialNumber # TrialID
    /// </summary>
    protected void UpdateTrialDropdown()
    {
      this.trialCombo.Items.Clear();
      DataTable table = Document.ActiveDocument.DocDataSet.TrialsAdapter.GetDataBySubject(this.subjectCombo.Text);
      SequencedTrial? itemToSelect = null;
      if (table.Rows.Count >= 1)
      {
        foreach (DataRow row in table.Rows)
        {
          int trialSequence = (int)row["TrialSequence"];
          int trialID = (int)row["TrialID"];
          string trialName = row["TrialName"].ToString();

          SequencedTrial newEntry = new SequencedTrial();
          newEntry.SequenceNumber = trialSequence;
          newEntry.TrialID = trialID;
          newEntry.TrialName = trialName;

          if (Document.ActiveDocument.SelectionState.TrialID != 0)
          {
            if (Document.ActiveDocument.SelectionState.TrialID == trialID)
            {
              itemToSelect = newEntry;
              Document.ActiveDocument.SelectionState.Update(string.Empty, null, trialSequence, null);
            }
          }

          this.trialCombo.Items.Add(newEntry);
        }

        this.trialCombo.Sorted = true;
      }

      if (itemToSelect.HasValue)
      {
        if (this.trialCombo.Items.Count > 0)
        {
          this.trialCombo.SelectedItem = itemToSelect;
        }
      }
      else if (this.trialCombo.Items.Count > 0)
      {
        this.trialCombo.SelectedIndex = 0;
        this.UpdateDocumentSelectionState();
      }
    }

    #endregion //PUBLICMETHODS

    ///////////////////////////////////////////////////////////////////////////////
    // Inherited methods                                                         //
    ///////////////////////////////////////////////////////////////////////////////
    #region OVERRIDES

    /// <summary>
    /// Initialize drop down controls.
    /// </summary>
    /// <remarks>The toolstrip combo box does currently not know the
    /// <see cref="ComboBox.SelectionChangeCommitted"/> event, so here we initialize it
    /// from the <see cref="ToolStripComboBox.ComboBox"/> member.</remarks>
    protected override void InitializeDropDowns()
    {
      base.InitializeDropDowns();
      this.subjectCombo.ComboBox.SelectionChangeCommitted += new EventHandler(this.subjectCombo_SelectionChangeCommitted);
      this.trialCombo.ComboBox.SelectionChangeCommitted += new EventHandler(this.trialCombo_SelectionChangeCommitted);
    }

    /// <summary>
    /// Initializes data bindings. Mainly wires the assigned document dataset to the binding
    /// sources.
    /// </summary>
    protected override void InitializeDataBindings()
    {
      base.InitializeDataBindings();
      if (this.bsoSubjects != null)
      {
        ((System.ComponentModel.ISupportInitialize)this.bsoSubjects).BeginInit();
        this.subjectCombo.ComboBox.DataSource = this.bsoSubjects;
        this.subjectCombo.ComboBox.DisplayMember = "SubjectName";
        this.subjectCombo.AutoSize = true;
        ((System.ComponentModel.ISupportInitialize)this.bsoSubjects).EndInit();
      }

      this.trialCombo.ComboBox.DisplayMember = "SequencedName";
    }

    /// <summary>
    /// This method reads the first valid trial entries from the combo boxes
    /// and intializes the UI.
    /// </summary>
    protected virtual void InitialDisplay()
    {
      if (this.subjectCombo.Items.Count > 0)
      {
        this.subjectCombo.SelectedIndex = 0;
        this.UpdateTrialDropdown();
        this.ReadSelectionStateAndShowData();
      }
    }

    /// <summary>
    /// Unregister custom events.
    /// </summary>
    protected override void CustomDispose()
    {
      if (this.subjectCombo != null && this.trialCombo != null)
      {
        this.subjectCombo.ComboBox.SelectionChangeCommitted -= new EventHandler(this.subjectCombo_SelectionChangeCommitted);
        this.trialCombo.ComboBox.SelectionChangeCommitted -= new EventHandler(this.trialCombo_SelectionChangeCommitted);
      }

      base.CustomDispose();
    }

    /// <summary>
    /// Checks for the current documents selection state,
    /// that means: Subject and TrialID.
    /// Updates the drop down list, if subject has changed.
    /// Updates the Selection state.
    /// Loads the new data.
    /// </summary>
    protected override void ReadSelectionStateAndShowData()
    {
      try
      {
        if (Document.ActiveDocument.SelectionState.IsSet)
        {
          string subjectName = Document.ActiveDocument.SelectionState.SubjectName;
          if (this.subjectCombo.Text != subjectName)
          {
            this.subjectCombo.Text = subjectName;
            this.UpdateTrialDropdown();
          }

          // trial sequence order can change for randomized stimuli, 
          // so always use unique trial id
          int trialID = Document.ActiveDocument.SelectionState.TrialID;
          List<int> sequenceNumbers = Queries.GetSequencesBySubjectAndTrialID(subjectName, trialID);

          if (sequenceNumbers.Count > 0)
          {
            Document.ActiveDocument.SelectionState.Update(string.Empty, null, sequenceNumbers[0], null);
            foreach (SequencedTrial trial in this.trialCombo.Items)
            {
              if (trial.SequenceNumber == sequenceNumbers[0])
              {
                this.trialCombo.SelectedItem = trial;
                break;
              }
            }
          }
          else
          {
            string message = "This trial was not recorded for this subject, please choose another trial.";
            ExceptionMethods.ProcessMessage("Trial not available.", message);
            Document.ActiveDocument.SelectionState.Update(string.Empty, null, null, null);
          }

          this.NewTrialSelected();
        }
      }
      catch (Exception ex)
      {
        ExceptionMethods.HandleException(ex);
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
    /// The <see cref="ComboBox.SelectionChangeCommitted"/> event handler for the
    /// <see cref="ComboBox"/> <see cref="subjectCombo"/>.
    /// User selected new subject from drop down list, so update
    /// documents selection state and read new raw data from database.
    /// Also update trial list, because trial numbers could have changed.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void subjectCombo_SelectionChangeCommitted(object sender, EventArgs e)
    {
      Document.ActiveDocument.SelectionState.Update(this.subjectCombo.Text, null, null, null);
      this.UpdateTrialDropdown();
      this.NewTrialSelected();
    }

    /// <summary>
    /// The <see cref="ComboBox.SelectionChangeCommitted"/> event handler for the
    /// <see cref="ComboBox"/> <see cref="trialCombo"/>.
    /// User selected new trial from drop down list, so update
    /// documents selection state and read new raw data from database
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void trialCombo_SelectionChangeCommitted(object sender, EventArgs e)
    {
      this.UpdateDocumentSelectionState();
      this.NewTrialSelected();
    }

    #endregion //WINDOWSEVENTHANDLER

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
