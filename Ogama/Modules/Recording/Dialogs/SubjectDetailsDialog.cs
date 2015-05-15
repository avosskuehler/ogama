// <copyright file="SubjectDetailsDialog.cs" company="FU Berlin">
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

namespace Ogama.Modules.Recording.Dialogs
{
  using System;
  using System.Windows.Forms;

  using Ogama.Modules.Common.Tools;

  /// <summary>
  /// A pop up dialog <see cref="Form"/> for requesting subject 
  /// information during recording.
  /// </summary>
  public partial class SubjectDetailsDialog : Form
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
    /// Initializes a new instance of the SubjectDetailsDialog class.
    /// </summary>
    public SubjectDetailsDialog()
    {
      this.InitializeComponent();
      this.cbbSex.SelectedIndex = 0;
      this.cbbHandedness.SelectedIndex = 0;
    }

    #endregion //CONSTRUCTION

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Properties                                                       //
    ///////////////////////////////////////////////////////////////////////////////
    #region PROPERTIES

    /// <summary>
    /// Gets or sets subject name as entered in text box.
    /// </summary>
    /// <value>A <see cref="string"/> with the subject name.</value>
    public string SubjectName
    {
      get
      {
        return this.txbSubjectName.Text;
      }

      set
      {
        string subjectName = value;
        int counter = 1;
        while (!Queries.ValidateSubjectName(ref subjectName, true))
        {
          if (subjectName.Contains("Subject"))
          {
            if (int.TryParse(subjectName.Replace("Subject", string.Empty), out counter))
            {
              counter++;
              subjectName = "Subject" + counter.ToString();
            }
          }
          else
          {
            subjectName = "Subject1";
          }
        }

        this.txbSubjectName.Text = subjectName;
      }
    }

    /// <summary>
    /// Gets the category as entered in the text box.
    /// </summary>
    /// <value>A <see cref="string"/> with the subjects category
    /// or <strong>null</strong> if the category is not specified.</value>
    public string Category
    {
      get
      {
        if (this.cbbCategory.Text != "not specified")
        {
          return this.cbbCategory.Text;
        }
        else
        {
          return null;
        }
      }
    }

    /// <summary>
    /// Gets the age of the subject as entered in the text box.
    /// </summary>
    /// <value>A <see cref="int"/> with the subjects age
    /// or <strong>null</strong> if the age is not specified.</value>
    public int? Age
    {
      get
      {
        int age;
        if (int.TryParse(this.txbAge.Text, out age))
        {
          return age;
        }
        else
        {
          return null;
        }
      }
    }

    /// <summary>
    /// Gets the sex of the subject as entered in the text box.
    /// </summary>
    /// <value>A <see cref="string"/> with the subjects sex
    /// or <strong>null</strong> if the sex is not specified.</value>
    public string Sex
    {
      get
      {
        if (this.cbbSex.Text != "not specified")
        {
          return this.cbbSex.Text;
        }
        else
        {
          return null;
        }
      }
    }

    /// <summary>
    /// Gets the handedness of the subject as entered in the text box.
    /// </summary>
    /// <value>A <see cref="string"/> with the subjects handedness
    /// or <strong>null</strong> if the handedness is not specified.</value>
    public string Handedness
    {
      get
      {
        if (this.cbbHandedness.Text != "not specified")
        {
          return this.cbbHandedness.Text;
        }
        else
        {
          return null;
        }
      }
    }

    /// <summary>
    /// Gets the comments for the subject as entered in the text box.
    /// </summary>
    /// <value>A <see cref="string"/> with the subjects comments
    /// or <strong>null</strong> if the comments section is empty.</value>
    public string Comments
    {
      get
      {
        if (this.txbComments.Text != string.Empty)
        {
          return this.txbComments.Text;
        }
        else
        {
          return null;
        }
      }
    }

    /// <summary>
    /// Gets the param1 of the subject as entered in the text box.
    /// </summary>
    /// <value>A <see cref="string"/> with the subjects param1
    /// or <strong>null</strong> if the param1 is not specified.</value>
    public string Param1
    {
      get
      {
        if (this.txbParam1.Text != string.Empty)
        {
          return this.txbParam1.Text;
        }
        else
        {
          return null;
        }
      }
    }

    /// <summary>
    /// Gets the param2 of the subject as entered in the text box.
    /// </summary>
    /// <value>A <see cref="string"/> with the subjects param2
    /// or <strong>null</strong> if the param2 is not specified.</value>
    public string Param2
    {
      get
      {
        if (this.txbParam2.Text != string.Empty)
        {
          return this.txbParam2.Text;
        }
        else
        {
          return null;
        }
      }
    }

    /// <summary>
    /// Gets the param3 of the subject as entered in the text box.
    /// </summary>
    /// <value>A <see cref="string"/> with the subjects param3
    /// or <strong>null</strong> if the param3 is not specified.</value>
    public string Param3
    {
      get
      {
        if (this.txbParam3.Text != string.Empty)
        {
          return this.txbParam3.Text;
        }
        else
        {
          return null;
        }
      }
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
    /// Cancels closing if the dialogs state is <see cref="DialogResult.Retry"/>
    /// </summary>
    /// <remarks>This is for example when the subject name is inacceptable.</remarks>
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
    /// Ensures valid subject names.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void btnOK_Click(object sender, EventArgs e)
    {
      string subjectName = this.txbSubjectName.Text.Trim();
      if (!Queries.ValidateSubjectName(ref subjectName, false))
      {
        this.DialogResult = DialogResult.Retry;
      }

      this.txbSubjectName.Text = subjectName;
    }

    /// <summary>
    /// The <see cref="Control.KeyDown"/> event handler for the
    /// <see cref="TextBox"/> <see cref="txbAge"/>.
    /// Supresses letter entries to get valid ages.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">A <see cref="KeyEventArgs"/> with the event data.</param>
    private void txbAge_KeyDown(object sender, KeyEventArgs e)
    {
      if (char.IsLetter((char)e.KeyCode))
      {
        e.SuppressKeyPress = true;
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