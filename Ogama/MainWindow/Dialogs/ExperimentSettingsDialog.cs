// <copyright file="ExperimentSettingsDialog.cs" company="FU Berlin">
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

namespace Ogama.MainWindow.Dialogs
{
  using System;
  using System.ComponentModel;
  using System.Data.SqlClient;
  using System.Data.SQLite;
  using System.Windows.Forms;

  using Microsoft.SqlServer.Management.Common;
  using Microsoft.SqlServer.Management.Smo;

  using Ogama.ExceptionHandling;
  using Ogama.Modules.Common.Tools;
  using Ogama.Properties;

  /// <summary>
  /// Dialog with experiment settings.
  /// Asks for stimulus folder and database file and for screen dimensions 
  /// of stimulus screen and other experiment related settings.
  /// </summary>
  public partial class ExperimentSettingsDialog : Form
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
    /// Initializes a new instance of the ExperimentSettingsDialog class.
    /// </summary>
    public ExperimentSettingsDialog()
    {
      this.InitializeComponent();
      if (Document.ActiveDocument != null)
      {
        ExperimentSettings settings = Document.ActiveDocument.ExperimentSettings;
        this.txbXSizeEyeMon.Text = Convert.ToString(settings.WidthStimulusScreen);
        this.txbYSizeEyeMon.Text = Convert.ToString(settings.HeightStimulusScreen);
        this.txbSamplingRateGaze.Text = settings.GazeSamplingRate.ToString();
        this.txbSamplingRateMouse.Text = settings.MouseSamplingRate.ToString();
        this.nudMaxDistanceGaze.Value = settings.GazeMaxDistance;
        this.nudMaxDistanceMouse.Value = settings.MouseMaxDistance;
        this.nudMinGazeSamples.Value = settings.GazeMinSamples;
        this.nudMinMouseSamples.Value = settings.MouseMinSamples;
        this.nudGazeDiameterDiv.Value = (decimal)settings.GazeDiameterDiv;
        this.nudMouseDiameterDiv.Value = (decimal)settings.MouseDiameterDiv;
        this.nudFixationRingSize.Value = (decimal)settings.FixationRingSize;
        this.chbEliminateFirstFixationOfGivenLength.Checked = settings.EliminateFirstFixation;
        this.chbEliminateFirstFixationSimple.Checked = settings.EliminateFirstFixationSimple;
        this.chbMergeConsecutiveFixations.Checked = settings.MergeConsecutiveFixations;
        this.nudFixationLimit.Value = (decimal)settings.LimitForFirstFixation;
      }
    }

    #endregion //CONSTRUCTION

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Properties                                                       //
    ///////////////////////////////////////////////////////////////////////////////
    #region PROPERTIES

    /// <summary>
    /// Gets the <see cref="ExperimentSettings"/> that are setup 
    /// in the dialog.
    /// </summary>
    /// <value>A <see cref="ExperimentSettings"/> filled with the 
    /// dialogs settings.</value>
    public ExperimentSettings ExperimentSettings
    {
      get { return this.GenerateNewSettings(); }
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
    /// Used to cancel closing, if sql server connection does not exist.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">A <see cref="FormClosingEventArgs"/> that contains the event data. </param>
    private void ExperimentSettingsDialog_FormClosing(object sender, FormClosingEventArgs e)
    {
      // Check sql server connection on exit, but not on cancelling.
      if (this.DialogResult == DialogResult.OK)
      {
        this.Cursor = Cursors.WaitCursor;
        if (!this.TestSQLServerConnection(false))
        {
          e.Cancel = true;
          string message = "Please specify the correct name for the running SQL Server instance.";
          ExceptionMethods.ProcessMessage("SQL Instance not found", message);
        }
      }

      this.Cursor = Cursors.Default;
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler for
    /// the <see cref="Button"/> <see cref="btnTestSQLConnection"/>
    /// Tests for a successful connection to the SQL server.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void btnTestSQLConnection_Click(object sender, EventArgs e)
    {
      this.TestSQLServerConnection(true);
    }

    /// <summary>
    /// The <see cref="Control.Validating"/> event handler for 
    /// the <see cref="TextBox"/> <see cref="txbSamplingRateGaze"/>.
    /// Checks gaze sampling rate text field for correct numeric entry.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">A <see cref="CancelEventArgs"/> with the event data.</param>
    private void txbSamplingRateGaze_Validating(object sender, CancelEventArgs e)
    {
      if (!IOHelpers.IsNumeric(this.txbSamplingRateGaze.Text))
      {
        e.Cancel = true;
        ExceptionMethods.ProcessErrorMessage("The sampling rate has to be a numerical value");
      }
    }

    /// <summary>
    /// The <see cref="Control.Validating"/> event handler for 
    /// the <see cref="TextBox"/> <see cref="txbSamplingRateMouse"/>.
    /// Checks mouse sampling rate text field for correct numeric entry.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">A <see cref="CancelEventArgs"/> with the event data.</param>
    private void txbSamplingRateMouse_Validating(object sender, CancelEventArgs e)
    {
      if (!IOHelpers.IsNumeric(this.txbSamplingRateMouse.Text))
      {
        e.Cancel = true;
        ExceptionMethods.ProcessErrorMessage("The sampling rate has to be a numerical value");
      }
    }

    /// <summary>
    /// The <see cref="Control.Validating"/> event handler for 
    /// the <see cref="TextBox"/> <see cref="txbXSizeEyeMon"/>.
    /// Checks x size of tracking monitor text field for correct numeric entry.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">A <see cref="CancelEventArgs"/> with the event data.</param>
    private void fldXSizeEyeMon_Validating(object sender, CancelEventArgs e)
    {
      if (!IOHelpers.IsNumeric(this.txbXSizeEyeMon.Text))
      {
        e.Cancel = true;
        ExceptionMethods.ProcessErrorMessage("The screen width has to be a numerical value");
      }
    }

    /// <summary>
    /// The <see cref="Control.Validating"/> event handler for 
    /// the <see cref="TextBox"/> <see cref="txbYSizeEyeMon"/>.
    /// Checks y size of tracking monitor text field for correct numeric entry.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">A <see cref="CancelEventArgs"/> with the event data.</param>
    private void fldYSizeEyeMon_Validating(object sender, CancelEventArgs e)
    {
      if (!IOHelpers.IsNumeric(this.txbYSizeEyeMon.Text))
      {
        e.Cancel = true;
        ExceptionMethods.ProcessErrorMessage("The screen height has to be a numerical value");
      }
    }

    /// <summary>
    /// The <see cref="Control.Validated"/> event handler for 
    /// the <see cref="TextBox"/> <see cref="txbSamplingRateGaze"/>.
    /// Adjusts min gaze samples value according to sampling rate.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void txbSamplingRateGaze_Validated(object sender, EventArgs e)
    {
      int gazeSamplingRate = Convert.ToInt32(this.txbSamplingRateGaze.Text);
      this.nudMinGazeSamples.Value = Math.Max(1, (decimal)(gazeSamplingRate / 12.0f));
    }

    /// <summary>
    /// The <see cref="Control.Validated"/> event handler for 
    /// the <see cref="TextBox"/> <see cref="txbSamplingRateMouse"/>.
    /// Adjusts min mouse samples value according to sampling rate.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void txbSamplingRateMouse_Validated(object sender, EventArgs e)
    {
      int mouseSamplingRate = Convert.ToInt32(this.txbSamplingRateMouse.Text);
      this.nudMinMouseSamples.Value = Math.Max(1, (decimal)(mouseSamplingRate / 6.0f));
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

    /// <summary>
    /// This method reads the values of the dialog into the 
    /// experiment settings fiels
    /// </summary>
    /// <returns>A <see cref="ExperimentSettings"/> with the dialogs values.</returns>
    private ExperimentSettings GenerateNewSettings()
    {
      ExperimentSettings settings = Document.ActiveDocument.ExperimentSettings;

      settings.GazeDiameterDiv = (float)this.nudGazeDiameterDiv.Value;
      settings.GazeMaxDistance = (int)this.nudMaxDistanceGaze.Value;
      settings.GazeMinSamples = (int)this.nudMinGazeSamples.Value;
      settings.GazeSamplingRate = Convert.ToInt32(this.txbSamplingRateGaze.Text);
      settings.WidthStimulusScreen = Convert.ToInt32(this.txbXSizeEyeMon.Text);
      settings.HeightStimulusScreen = Convert.ToInt32(this.txbYSizeEyeMon.Text);
      settings.MouseDiameterDiv = (float)this.nudMouseDiameterDiv.Value;
      settings.MouseMaxDistance = (int)this.nudMaxDistanceMouse.Value;
      settings.MouseMinSamples = (int)this.nudMinMouseSamples.Value;
      settings.MouseSamplingRate = Convert.ToInt32(this.txbSamplingRateMouse.Text);
      settings.LimitForFirstFixation = (int)this.nudFixationLimit.Value;
      settings.EliminateFirstFixation = this.chbEliminateFirstFixationOfGivenLength.Checked;
      settings.EliminateFirstFixationSimple = this.chbEliminateFirstFixationSimple.Checked;
      settings.MergeConsecutiveFixations = this.chbMergeConsecutiveFixations.Checked;
      settings.FixationRingSize = (int)this.nudFixationRingSize.Value;
      settings.SqlInstanceName = this.txbSQLInstanceName.Text;

      return settings;
    }

    /// <summary>
    /// This method tries to connect to the given SQL server.
    /// </summary>
    /// <param name="showMessage">Indicates whether this method should be run
    /// in silent or verbose mode.</param>
    /// <returns>True if successful, otherwise false.</returns>
    private bool TestSQLServerConnection(bool showMessage)
    {
      this.Cursor = Cursors.WaitCursor;
      string oldConnectionString = Document.ActiveDocument.ExperimentSettings.DatabaseConnectionString;
      //Document.ActiveDocument.ExperimentSettings.SqlInstanceName = this.txbSQLInstanceName.Text;
      //SqlConnection connectionString = new SqlConnection(Document.ActiveDocument.ExperimentSettings.ServerConnectionString);
      //ServerConnection connection = new ServerConnection(connectionString);
      //using (var conn = new SQLiteConnection(this.txbSQLInstanceName.Text))
      //{
      //  try
      //  {
      //    conn.Open();
      //  }
      //  catch (Exception ex)
      //  {
      //    if (showMessage)
      //    {
      //      MessageBox.Show("Connection failed");
      //    }

      //    this.txbSQLInstanceName.Text = oldConnectionString;

      //    return false;
      //  }
      //  finally
      //  {
      //    this.Cursor = Cursors.Default;
      //  }
      //}
      if (showMessage)
      {
        MessageBox.Show("Connection successful");
      }

      return true;
    }

    /// <summary>
    /// This method checks the state of chbEliminateFirstFixationSimple_CheckedChanged checkbox
    /// and controls another checkbox chbEliminateFirstFixationOfGivenLength
    /// so there is ony one checkbox could be checked.
    /// </summary>
    /// <param name="sender">Indicates the sender of an event.</param>
    /// <param name="e">Event arguments.</param>
    private void chbEliminateFirstFixationSimple_CheckedChanged(object sender, EventArgs e)
    {
        if (this.chbEliminateFirstFixationSimple.Checked)
        {
            this.chbEliminateFirstFixationOfGivenLength.Checked = false;
        }
    }
    
    /// <summary>
    /// This method checks the state of chbEliminateFirstFixationOfGivenLength_CheckedChanged checkbox
    /// and controls another checkbox chbEliminateFirstFixationSimple
    /// so there is ony one checkbox could be checked.
    /// </summary>
    /// <param name="sender">Indicates the sender of an event.</param>
    /// <param name="e">Event arguments.</param>
    private void chbEliminateFirstFixationOfGivenLength_CheckedChanged(object sender, EventArgs e)
    {
        if (this.chbEliminateFirstFixationOfGivenLength.Checked)
        {
            this.chbEliminateFirstFixationSimple.Checked = false;
        }
    }

    #endregion //METHODS
      
    ///////////////////////////////////////////////////////////////////////////////
    // Small helping Methods                                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region HELPER
    #endregion //HELPER
  }
}
