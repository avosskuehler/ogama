// <copyright file="InformationDialog.cs" company="FU Berlin">
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

namespace Ogama.ExceptionHandling
{
  using System;
  using System.Drawing;
  using System.Windows.Forms;

  /// <summary>
  /// A small popup <see cref="Form"/> for showing a convinient ogama styled message.
  /// </summary>
  public partial class InformationDialog : Form
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
    /// Initializes a new instance of the InformationDialog class.
    /// </summary>
    /// <param name="title">A <see cref="String"/> with the dialogs title.</param>
    /// <param name="message">A <see cref="String"/> with th message to show.</param>
    /// <param name="isYesNoCancel"><strong>True</strong>, if dialog should show
    /// three buttins YesNoCancel, <strong>false</strong> when only has an OK button.</param>
    /// <param name="icon">The <see cref="MessageBoxIcon"/> to show.</param>
    public InformationDialog(string title, string message, bool isYesNoCancel, MessageBoxIcon icon)
    {
      this.InitializeComponent();

      this.dialogTop1.Description = title;
      this.txbMessage.Text = message;

      if (isYesNoCancel)
      {
        this.spcYesNoCancelOK.Panel2Collapsed = true;
        this.AcceptButton = this.btnYes;
      }
      else
      {
        this.spcYesNoCancelOK.Panel1Collapsed = true;
        this.AcceptButton = this.btnOK;
      }

      switch (icon)
      {
        case MessageBoxIcon.Error:
          this.Icon = SystemIcons.Error;
          break;
        case MessageBoxIcon.Information:
          this.Icon = SystemIcons.Information;
          break;
        case MessageBoxIcon.None:
          this.Icon = null;
          break;
        case MessageBoxIcon.Question:
          this.Icon = SystemIcons.Question;
          break;
        case MessageBoxIcon.Warning:
          this.Icon = SystemIcons.Warning;
          break;
      }
    }

    #endregion //CONSTRUCTION

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Properties                                                       //
    ///////////////////////////////////////////////////////////////////////////////
    #region PROPERTIES
    #endregion //PROPERTIES

    ///////////////////////////////////////////////////////////////////////////////
    // Eventhandler                                                              //
    ///////////////////////////////////////////////////////////////////////////////
    #region EVENTS

    ///////////////////////////////////////////////////////////////////////////////
    // Eventhandler for UI, Menu, Buttons, Toolbars etc.                         //
    ///////////////////////////////////////////////////////////////////////////////
    #region WINDOWSEVENTHANDLER
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

    /// <summary>
    /// Displays an ogama styled message box with specified text, caption, buttons, and icon. 
    /// </summary>
    /// <param name="caption">The text to display in the title of the message box. </param>
    /// <param name="message">The text to display in the message box.</param>
    /// <param name="isYesNoCancel"><strong>True</strong> when yes no cancel buttons should be shown,
    /// otherwise <strong>false</strong> only OK button is shown.</param>
    /// <param name="icon">One of the <see cref="MessageBoxIcon"/> values that specifies which icon to display in the message box.</param>
    /// <returns>One of the <see cref="DialogResult"/> values.</returns>
    public static DialogResult Show(string caption, string message, bool isYesNoCancel, MessageBoxIcon icon)
    {
      var dlg = new InformationDialog(caption, message, isYesNoCancel, icon);
      return dlg.ShowDialog();
    }

    #endregion //METHODS

    ///////////////////////////////////////////////////////////////////////////////
    // Small helping Methods                                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region HELPER
    #endregion //HELPER
  }
}