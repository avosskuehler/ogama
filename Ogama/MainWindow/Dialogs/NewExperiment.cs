// <copyright file="NewExperiment.cs" company="FU Berlin">
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

namespace Ogama.MainWindow.Dialogs
{
  using System;
  using System.Collections.Generic;
  using System.IO;
  using System.Windows.Forms;

  using Ogama.ExceptionHandling;

  /// <summary>
  /// A popup dialog <see cref="Form"/> asking for parent folder and 
  /// name of the new experiment to create.
  /// </summary>
  public partial class NewExperiment : Form
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
    #endregion //FIELDS

    ///////////////////////////////////////////////////////////////////////////////
    // Construction and Initializing methods                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region CONSTRUCTION

    /// <summary>
    /// Initializes a new instance of the NewExperiment class.
    /// </summary>
    public NewExperiment()
    {
      this.InitializeComponent();

      var path = Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.Personal),
        "OgamaExperiments");

      // Check for existing default experiment
      var experiments = Directory.GetDirectories(path);
      var experimentList = new List<string>();
      experimentList.AddRange(experiments);
      var counter = 1;
      var defaultName = "Experiment" + counter;
      while (experimentList.Contains(Path.Combine(path, defaultName)))
      {
        counter++;
        defaultName = "Experiment" + counter;
      }

      this.txbExperimentName.Text = defaultName;
      this.txbParentFolder.Text = path;
    }

    #endregion //CONSTRUCTION

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Properties                                                       //
    ///////////////////////////////////////////////////////////////////////////////
    #region PROPERTIES

    /// <summary>
    /// Gets the string with the full path to the parent folder of the new experiment.
    /// </summary>
    /// <value>A <see cref="string"/> with the parent folder path.</value>
    public string ParentFolder
    {
      get { return this.txbParentFolder.Text; }
    }

    /// <summary>
    /// Gets the string with the name for the new experiment.
    /// </summary>
    /// <value>A <see cref="string"/> with the experiment name.</value>
    public string ExperimentName
    {
      get { return this.txbExperimentName.Text; }
    }

    #endregion //PROPERTIES

    ///////////////////////////////////////////////////////////////////////////////
    // Eventhandler                                                              //
    ///////////////////////////////////////////////////////////////////////////////
    #region EVENTS

    ///////////////////////////////////////////////////////////////////////////////
    // Eventhandler for UI, Menu, Buttons, Toolbars etc.                         //
    ///////////////////////////////////////////////////////////////////////////////
    #region WINDOWSEVENTHANDLER

    /// <summary>
    /// <see cref="Form.FormClosing"/> event handler. Occurs before the form is closed. 
    /// Used to cancel closing, if path does not exist.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">A <see cref="FormClosingEventArgs"/> that contains the event data. </param>
    private void frmNewExperimentDlg_FormClosing(object sender, FormClosingEventArgs e)
    {
      if (this.DialogResult == DialogResult.OK)
      {
        if (!Directory.Exists(this.txbParentFolder.Text))
        {
          string message = "The given path :" +
            Environment.NewLine + this.txbParentFolder.Text + Environment.NewLine +
            "does not exist, would you like to create it ? "
            + Environment.NewLine
            + "You have to specify a valid path or create a new one";

          DialogResult result = InformationDialog.Show(
            "Create Path ?",
            message,
            true,
            MessageBoxIcon.Question);
          if (result == DialogResult.Yes)
          {
            Directory.CreateDirectory(this.txbParentFolder.Text);
          }
          else
          {
            e.Cancel = true;
            return;
          }
        }

        string newExperimentFolder = Path.Combine(this.txbParentFolder.Text, this.txbExperimentName.Text);
        string newExperimentName = this.txbExperimentName.Text;
        string newExperimentFilename = Path.Combine(newExperimentFolder, newExperimentName + ".oga");

        if (File.Exists(newExperimentFilename))
        {
          string message = "This experiment already exists. Please choose another name";
          ExceptionMethods.ProcessMessage("Duplicate name", message);
          e.Cancel = true;
        }
      }
    }

    /// <summary>
    /// The <see cref="Control.OnClick"/> event handler for the 
    /// <see cref="btnOpenFolder"/> <see cref="Button"/>.
    /// Opens the <see cref="fbdExperiment"/> <see cref="FolderBrowserDialog"/>
    /// for parent folder selection.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void btnOpenFolder_Click(object sender, EventArgs e)
    {
      this.fbdExperiment.SelectedPath = this.txbParentFolder.Text;
      if (this.fbdExperiment.ShowDialog() == DialogResult.OK)
      {
        this.txbParentFolder.Text = this.fbdExperiment.SelectedPath;
      }
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
    // Inherited methods                                                         //
    ///////////////////////////////////////////////////////////////////////////////
    #region OVERRIDES
    #endregion //OVERRIDES

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