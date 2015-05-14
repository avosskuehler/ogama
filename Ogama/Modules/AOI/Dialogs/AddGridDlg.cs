// <copyright file="AddGridDlg.cs" company="FU Berlin">
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
  using System;
  using System.Collections.Generic;
  using System.Drawing;
  using System.Windows.Forms;

  using Ogama.Modules.Scanpath;

  /// <summary>
  /// A pop up <see cref="Form"/>. Asks for the name of a newly defined shape group
  /// </summary>
  public partial class AddGridDlg : Form
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
    /// Initializes a new instance of the AddGridDlg class.
    /// </summary>
    public AddGridDlg()
    {
      this.InitializeComponent();
      this.CreateDataGridView();
    }

    #endregion //CONSTRUCTION

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Enumerations                                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region ENUMS
    #endregion ENUMS

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Properties                                                       //
    ///////////////////////////////////////////////////////////////////////////////
    #region PROPERTIES

    /// <summary>
    /// Gets the preview data grid view with columns and rows and cell names
    /// of the new AOI Grid.
    /// </summary>
    /// <value>A <see cref="DataGridView"/> with the new AOI grid.</value>
    public DataGridView PreviewDataGridView
    {
      get { return this.dgvGridPreview; }
    }

    /// <summary>
    /// Sets the slide background for the grid preview.
    /// </summary>
    public Image SlideImage
    {
      set { this.dgvGridPreview.BackgroundImage = value; }
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
    /// The <see cref="NumericUpDown.ValueChanged"/> event handler.
    /// Updates the preview data grid view.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/>.</param>
    private void nudParams_ValueChanged(object sender, EventArgs e)
    {
      this.CreateDataGridView();
    }

    /// <summary>
    /// The <see cref="Control.Resize"/> event handler for the preview
    /// data grid view that recalculates the preview.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/>.</param>
    private void dgvGridPreview_Resize(object sender, EventArgs e)
    {
      this.CreateDataGridView();
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
    #region PRIVATEMETHODS

    /// <summary>
    /// Performs the drawing of the preview data grid view
    /// including drawing of initial naming.
    /// </summary>
    private void CreateDataGridView()
    {
      string[] description = ScanpathsPicture.CurrentIdentifierList;
      if (description == null)
      {
        description = ScanpathsPicture.IdentifierList;
      }

      int numColumns = (int)this.nudColumns.Value;
      int numRows = (int)this.nudRows.Value;
      if (numRows * numColumns > 26)
      {
        description = ScanpathsPicture.IdentifierListLong;
      }

      if (numRows * numColumns > 676)
      {
        description = ScanpathsPicture.IdentifierListExtraLong;
      }

      this.dgvGridPreview.Columns.Clear();
      this.dgvGridPreview.Rows.Clear();
      for (int i = 0; i < numColumns; i++)
      {
        this.dgvGridPreview.Columns.Add("Column" + i.ToString(), string.Empty);
      }

      for (int i = 0; i < numRows; i++)
      {
        List<string> rowEntries = new List<string>();
        for (int j = 0; j < numColumns; j++)
        {
          rowEntries.Add(description[(i * numColumns) + j]);
        }

        this.dgvGridPreview.Rows.Add(rowEntries.ToArray());
      }

      // Adapt row height to fill preview.
      int newHeight = (int)(this.dgvGridPreview.Height / numRows);
      foreach (DataGridViewRow row in this.dgvGridPreview.Rows)
      {
        row.Height = newHeight;
      }
    }

    #endregion //PRIVATEMETHODS

    ///////////////////////////////////////////////////////////////////////////////
    // Small helping Methods                                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region HELPER
    #endregion //HELPER
  }
}