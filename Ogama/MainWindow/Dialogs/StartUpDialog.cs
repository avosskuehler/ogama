// <copyright file="StartUpDialog.cs" company="FU Berlin">
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
  using System.Runtime.InteropServices;
  using System.Windows.Forms;

  using Ogama.Properties;

  /// <summary>
  /// The popup dialog <see cref="Form"/> is giving the user 
  /// the choice for selecting the project to begin with.
  /// Opening a recent project or creating a new one is easily done.
  /// </summary>
  public partial class StartUpDialog : Form
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
    /// A unvisible test flash com object.
    /// If initialization fails, OGAMA does not start,
    /// because all its flash com objects will fail.
    /// </summary>
    private AxShockwaveFlashObjects.AxShockwaveFlash flashTestObject;

    #endregion //FIELDS

    ///////////////////////////////////////////////////////////////////////////////
    // Construction and Initializing methods                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region CONSTRUCTION

    /// <summary>
    /// Initializes a new instance of the StartUpDialog class.
    /// </summary>
    public StartUpDialog()
    {
      this.InitializeComponent();
    }

    #endregion //CONSTRUCTION

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Properties                                                       //
    ///////////////////////////////////////////////////////////////////////////////
    #region PROPERTIES

    /// <summary>
    /// Gets the <see cref="ListBox.ObjectCollection"/> of the recent file list box.
    /// </summary>
    /// <value>A <see cref="ListBox.ObjectCollection"/> with the recent files.</value>
    public ListBox.ObjectCollection ListBoxItems
    {
      get { return this.lsbRecentProjects.Items; }
    }

    /// <summary>
    /// Gets the currently selected item from the recent file list.
    /// </summary>
    /// <value>A <see cref="string"/> with the selected recent file.</value>
    public string SelectedItem
    {
      get
      {
        if (this.lsbRecentProjects.SelectedItem != null)
        {
          return this.lsbRecentProjects.SelectedItem.ToString();
        }

        return string.Empty;
      }
    }

    /// <summary>
    /// Gets a value indicating whether the user wants to create a new project.
    /// </summary>
    /// <value>A <see cref="Boolean"/> indicating a new project creation.</value>
    public bool NewProject
    {
      get { return this.rdbNewProject.Checked; }
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
    /// The <see cref="Form.Load"/> event handler. 
    /// Selects first entry in recent files list.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void frmStartUpDlg_Load(object sender, EventArgs e)
    {
      this.lsbRecentProjects.SelectedIndex = 0;
      try
      {
        this.TryLoadingFlash();
      }
      catch (COMException)
      {
        FlashMissing errorWindow = new FlashMissing();
        errorWindow.ShowDialog();
        Application.Exit();
      }
    }

    /// <summary>
    /// The <see cref="RadioButton.CheckedChanged"/> event handler 
    /// for the <see cref="RadioButton"/> <see cref="rdbRecentProjects"/>.
    /// Checks or unchecks the <see cref="rdbNewProject"/>
    /// according to the state of this <see cref="rdbRecentProjects"/>.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void rdbRecentProjects_CheckedChanged(object sender, EventArgs e)
    {
      this.rdbNewProject.Checked = !this.rdbRecentProjects.Checked;
    }

    /// <summary>
    /// The <see cref="RadioButton.CheckedChanged"/> event handler 
    /// for the <see cref="RadioButton"/> <see cref="rdbNewProject"/>.
    /// Checks or unchecks the <see cref="rdbRecentProjects"/>
    /// according to the state of this <see cref="rdbNewProject"/>.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void rdbNewProject_CheckedChanged(object sender, EventArgs e)
    {
      this.rdbRecentProjects.Checked = !this.rdbNewProject.Checked;
    }

    /// <summary>
    /// The <see cref="ListBox.SelectedIndexChanged"/> event handler for
    /// the <see cref="ListBox"/> <see cref="lsbRecentProjects"/>.
    /// Activates recent project radio button, when recent file is
    /// selected in list.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void lsbRecentProjects_SelectedIndexChanged(object sender, EventArgs e)
    {
      this.rdbRecentProjects.Checked = true;
      this.rdbNewProject.Checked = false;
    }

    /// <summary>
    /// The <see cref="Control.MouseDoubleClick"/> event handler for
    /// the <see cref="ListBox"/> <see cref="lsbRecentProjects"/>.
    /// Selects recent project and loads clicked experiment 
    /// by returning <see cref="DialogResult.OK"/> when user double 
    /// clicked on a recent project.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void lsbRecentProjects_MouseDoubleClick(object sender, MouseEventArgs e)
    {
      this.rdbRecentProjects.Checked = true;
      this.rdbNewProject.Checked = false;
      this.DialogResult = DialogResult.OK;
      this.Close();
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler for
    /// the <see cref="Button"/> <see cref="btnClearRecentFilesList"/>.
    /// User clicked button to clear recent files list, so do it.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void btnClearRecentFilesList_Click(object sender, EventArgs e)
    {
      RecentFilesList.List.Clear();
      RecentFilesList.List.Delete();
      this.ListBoxItems.Clear();
      this.ListBoxItems.Add("search for other ...");
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
    /// This method tries to initialize a flash com object.
    /// If this is not possible, normally due to missing
    /// flash player installation, it throws a com exception.
    /// </summary>
    /// <exception cref="COMException">Thrown, when activeX com object initalization fails.</exception>
    private void TryLoadingFlash()
    {
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StartUpDialog));
      this.flashTestObject = new AxShockwaveFlashObjects.AxShockwaveFlash();

      ((System.ComponentModel.ISupportInitialize)this.flashTestObject).BeginInit();

      // axFlashTest
      this.flashTestObject.Enabled = true;
      this.flashTestObject.Location = new System.Drawing.Point(0, 61);
      this.flashTestObject.Name = "axFlashTest";
      this.flashTestObject.OcxState = (System.Windows.Forms.AxHost.State)resources.GetObject("axFlashTest.OcxState");
      this.flashTestObject.Size = new System.Drawing.Size(34, 32);
      this.flashTestObject.TabIndex = 1;
      ((System.ComponentModel.ISupportInitialize)this.flashTestObject).EndInit();

      try
      {
        this.dialogTop1.Controls.Add(this.flashTestObject);
      }
      catch (COMException)
      {
        this.dialogTop1.Controls.Remove(this.flashTestObject);
        throw;
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