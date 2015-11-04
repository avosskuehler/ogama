// <copyright file="GroupNameDlg.cs" company="FU Berlin">
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

namespace Ogama.Modules.AOI.Dialogs
{
  using System.Data;
  using System.Windows.Forms;

  using Ogama.ExceptionHandling;

  /// <summary>
  /// A pop up <see cref="Form"/>. Asks for the name of a newly defined shape group
  /// </summary>
  public partial class GroupNameDlg : Form
  {
    ///////////////////////////////////////////////////////////////////////////////
    // Construction and Initializing methods                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region CONSTRUCTION

    /// <summary>
    /// Initializes a new instance of the GroupNameDlg class.
    /// </summary>
    public GroupNameDlg()
    {
      this.InitializeComponent();
    }

    #endregion //CONSTRUCTION
    ///////////////////////////////////////////////////////////////////////////////
    // Defining Properties                                                       //
    ///////////////////////////////////////////////////////////////////////////////
    #region PROPERTIES

    /// <summary>
    /// Gets group name written in textbox field.
    /// </summary>
    /// <value>A <see cref="string"/> with the new group name.</value>
    public string GroupName
    {
      get { return this.txbGroupName.Text; }
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
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">A <see cref="FormClosingEventArgs"/> with the event data.</param>
    private void GroupNameDlg_FormClosing(object sender, FormClosingEventArgs e)
    {
      DataTable groupsTable =
         Document.ActiveDocument.DocDataSet.ShapeGroupsAdapter.GetDataByGroup(this.GroupName);
      if (groupsTable.Rows.Count > 0 && this.DialogResult == DialogResult.OK)
      {
        string message = "A group with this name is already present in the " +
          "database. This name has to be unique.";
        ExceptionMethods.ProcessMessage("Please change the group name", message);
        e.Cancel = true;
      }
    }

    #endregion //WINDOWSEVENTHANDLER

    ///////////////////////////////////////////////////////////////////////////////
    // Eventhandler for Custom Defined Events                                    //
    ///////////////////////////////////////////////////////////////////////////////
    #region CUSTOMEVENTHANDLER
    #endregion //CUSTOMEVENTHANDLER

    #endregion //EVENTS
  }
}