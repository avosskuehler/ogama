// <copyright file="FormWithTrialSelection.cs" company="FU Berlin">
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
  using System.ComponentModel;
  using System.Windows.Forms;

  using Ogama.ExceptionHandling;
  using Ogama.Modules.Common.SlideCollections;

  /// <summary>
  /// Inherits <see cref="FormWithPicture"/>.
  /// This class extends the base class <see cref="FormWithPicture"/> to implement a 
  /// trial selection common handling that is subject comprehensive. It should be used for modules
  /// that display trial specific data that is not subject specific e.g. AOI definition,
  /// attention maps.
  /// </summary>
  public class FormWithTrialSelection : FormWithPicture
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
    /// Saves the trial drop down combo box.
    /// </summary>
    private ToolStripComboBox trialCombo;

    #endregion //FIELDS

    ///////////////////////////////////////////////////////////////////////////////
    // Construction and Initializing methods                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region CONSTRUCTION

    /// <summary>
    /// Initializes a new instance of the FormWithTrialSelection class.
    /// </summary>
    public FormWithTrialSelection()
    {
    }

    #endregion //CONSTRUCTION

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Properties                                                       //
    ///////////////////////////////////////////////////////////////////////////////
    #region PROPERTIES

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
      Trial trial = this.trialCombo.SelectedItem as Trial;
      Document.ActiveDocument.SelectionState.Update(
        string.Empty,
        trial.ID,
        null,
        null);
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

      this.trialCombo.ComboBox.SelectionChangeCommitted += new EventHandler(this.trialCombo_SelectionChangeCommitted);
    }

    /// <summary>
    /// Initializes data bindings. Mainly wires the assigned document dataset to the binding
    /// sources.
    /// </summary>
    protected override void InitializeDataBindings()
    {
      base.InitializeDataBindings();
      this.trialCombo.Items.AddRange(Document.ActiveDocument.ExperimentSettings.SlideShow.Trials.ToArray());
      this.trialCombo.ComboBox.DisplayMember = "Name";
      this.trialCombo.ComboBox.ValueMember = "ID";
      this.trialCombo.AutoSize = true;
    }

    /// <summary>
    /// This method reads the first valid trial entries from the combo boxes
    /// and intializes the UI.
    /// </summary>
    protected virtual void InitialDisplay()
    {
      if (this.trialCombo.Items.Count > 0)
      {
        this.trialCombo.SelectedIndex = 0;
        this.UpdateDocumentSelectionState();
        this.ReadSelectionStateAndShowData();
      }
    }

    /// <summary>
    /// Unregister custom events.
    /// </summary>
    protected override void CustomDispose()
    {
      base.CustomDispose();
      if (this.trialCombo != null)
      {
        this.trialCombo.ComboBox.SelectionChangeCommitted -= new EventHandler(this.trialCombo_SelectionChangeCommitted);
      }
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
        // trial sequence order can change for randomized stimuli, 
        // so always use unique trial id
        int trialID = Document.ActiveDocument.SelectionState.TrialID;

        Trial newTrial = Document.ActiveDocument.ExperimentSettings.SlideShow.GetTrialByID(trialID);
        Trial itemToSelect = this.trialCombo.SelectedItem as Trial;

        if (newTrial == null)
        {
          return;
        }

        foreach (Trial item in this.trialCombo.Items)
        {
          if (item.ID == newTrial.ID)
          {
            itemToSelect = item;
            break;
          }
        }

        // if trial ID in selection state was empty, 
        // the first trial in the combo box was selected, so update
        // trial id of selection state
        Document.ActiveDocument.SelectionState.Update(string.Empty, itemToSelect.ID, null, null);
        this.trialCombo.SelectedItem = itemToSelect;

        // Load Data according to entrys in DropDowns
        this.NewTrialSelected();
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
