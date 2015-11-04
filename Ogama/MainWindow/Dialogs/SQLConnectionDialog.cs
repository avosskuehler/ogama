// <copyright file="SQLConnectionDialog.cs" company="FU Berlin">
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

namespace Ogama.MainWindow.Dialogs
{
  using System;
  using System.Data.SqlClient;
  using System.Windows.Forms;

  using Microsoft.Data.ConnectionUI;

  /// <summary>
  /// A dialog <see cref="Form"/> that is identical to that used
  /// from Visual Studio SDK for generating a SQL connection string.
  /// </summary>
  public partial class SQLConnectionDialog : Form
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
    /// Saves the <see cref="SqlConnectionProperties"/> for the
    /// custom connection.
    /// </summary>
    private SqlConnectionProperties connectionProperties;

    #endregion //FIELDS

    ///////////////////////////////////////////////////////////////////////////////
    // Construction and Initializing methods                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region CONSTRUCTION

    /// <summary>
    /// Initializes a new instance of the SQLConnectionDialog class.
    /// </summary>
    public SQLConnectionDialog()
    {
      this.InitializeComponent();
      this.connectionProperties = new SqlConnectionProperties();
      this.sqlConnectionUIControl.Initialize(this.connectionProperties);
    }

    #endregion //CONSTRUCTION

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Properties                                                       //
    ///////////////////////////////////////////////////////////////////////////////
    #region PROPERTIES

    /// <summary>
    /// Gets or sets the underlying connection string.
    /// </summary>
    /// <value>A <see cref="string"/> with the connection string
    /// that is represented by the dialog.</value>
    public string ConnectionString
    {
      get
      {
        return this.connectionProperties.ConnectionStringBuilder.ConnectionString;
      }

      set
      {
        this.connectionProperties.ConnectionStringBuilder.ConnectionString = value;
        this.sqlConnectionUIControl.LoadProperties();
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
    /// The <see cref="Control.Click"/> event handler for the
    /// <see cref="Button"/> <see cref="btnTestConnection"/>.
    /// Tries to establish a connection to the given SQL server
    /// with the current dialogs properties.
    /// Displays an message for success or failure.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void btnTestConnection_Click(object sender, EventArgs e)
    {
      using (SqlConnection conn = new SqlConnection(this.connectionProperties.ConnectionStringBuilder.ConnectionString))
      {
        try
        {
          conn.Open();
          MessageBox.Show("Test Connection Succeeded.");
        }
        catch (Exception ex)
        {
          MessageBox.Show("Test Connection Failed. Error message is: "
              + Environment.NewLine + ex.Message);
        }
        finally
        {
          try
          {
            conn.Close();
          }
          catch (Exception)
          {
          }
        }
      }
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler for the
    /// <see cref="Button"/> <see cref="btnAdvanced"/>.
    /// Opens a property dialog for the <see cref="SqlConnectionProperties"/>.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void btnAdvanced_Click(object sender, EventArgs e)
    {
      PropertyDialog dialog = new PropertyDialog();
      dialog.SelectedObject = this.connectionProperties;
      if (dialog.ShowDialog() == DialogResult.OK)
      {
        this.connectionProperties = (SqlConnectionProperties)dialog.SelectedObject;
      }
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler for the
    /// <see cref="Button"/> <see cref="btnDefaultConnection"/>.
    /// Resets the connection to default by emptying the custom
    /// conneciton string property of the <see cref="Document.ExperimentSettings"/>.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void btnDefaultConnection_Click(object sender, EventArgs e)
    {
      Document.ActiveDocument.ExperimentSettings.ResetConnectionStringToDefault();
      this.ConnectionString = Document.ActiveDocument.ExperimentSettings.DatabaseConnectionString;
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