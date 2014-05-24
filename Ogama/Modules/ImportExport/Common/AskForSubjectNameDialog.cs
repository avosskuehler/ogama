// <copyright file="AskForSubjectNameDialog.cs" company="FU Berlin">
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

namespace Ogama.Modules.ImportExport.Common
{
  using System;
  using System.Windows.Forms;

  using Ogama.Modules.Common.Tools;

  /// <summary>
  /// A pop up dialog <see cref="Form"/> for requesting a subject name during import.
  /// </summary>
  public partial class AskForSubjectNameDialog : Form
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
    /// Saves a flag, whether this dialog should return <see cref="DialogResult.OK"/>,
    /// if dialogs <see cref="SubjectName"/> property is an existing subjects
    /// or a new subject.
    /// </summary>
    private bool returnOkIfSubjectIsInDatabase;

    #endregion //FIELDS

    ///////////////////////////////////////////////////////////////////////////////
    // Construction and Initializing methods                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region CONSTRUCTION

    /// <summary>
    /// Initializes a new instance of the AskForSubjectNameDialog class.
    /// </summary>
    /// <param name="doReturnOkIfSubjectIsInDatabase"><strong>True</strong>
    /// if dialog should return true, if subjects was found in the database,
    /// otherwise <strong>false</strong>.</param>
    public AskForSubjectNameDialog(bool doReturnOkIfSubjectIsInDatabase)
    {
      this.InitializeComponent();
      this.returnOkIfSubjectIsInDatabase = doReturnOkIfSubjectIsInDatabase;
    }

    #endregion //CONSTRUCTION

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Properties                                                       //
    ///////////////////////////////////////////////////////////////////////////////
    #region PROPERTIES

    /// <summary>
    /// Gets or sets the subject name as entered in Textbox
    /// </summary>
    /// <value>A <see cref="string"/> with the new subject name.</value>
    public string SubjectName
    {
      get { return this.txbSubjectName.Text; }
      set { this.txbSubjectName.Text = value; }
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
    /// The <see cref="Form.FormClosing"/> event handler. 
    /// If something is incorrect, the dialog result is 
    /// set to <see cref="DialogResult.Retry"/>. 
    /// When trying to close the form and this state is set
    /// closing will be cancelled.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">A <see cref="FormClosingEventArgs"/> with the event data.</param>
    private void frmAskForSubjectName_FormClosing(object sender, FormClosingEventArgs e)
    {
      if (this.DialogResult == DialogResult.Retry)
      {
        e.Cancel = true;
      }
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler for the
    /// <see cref="Button"/> <see cref="btnOK"/>.
    /// Checks for valid SubjectName by calling 
    /// <see cref="Queries.ValidateSubjectName"/>
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void btnOK_Click(object sender, EventArgs e)
    {
      string subjectName = this.txbSubjectName.Text.Trim();

      if (this.returnOkIfSubjectIsInDatabase)
      {
        // Retry if subject name does not exist.
        if (!Queries.IsSubjectNameInDatabase(subjectName))
        {
          this.DialogResult = DialogResult.Retry;
        }
      }
      else
      {
        // Retry if subject name is existing or invalid.
        if (!Queries.ValidateSubjectName(ref subjectName, false))
        {
          this.DialogResult = DialogResult.Retry;
        }
      }

      this.txbSubjectName.Text = subjectName;
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