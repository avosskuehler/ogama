// <copyright file="MainForm.cs" company="FU Berlin">
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

namespace Ogama.MainWindow
{
  using System;
  using System.Collections.Generic;
  using System.ComponentModel;
  using System.IO;
  using System.Windows.Forms;

  using Ogama.ExceptionHandling;
  using Ogama.MainWindow.Dialogs;
  using Ogama.Modules.Common.FormTemplates;
  using Ogama.Modules.ImportExport.RawData;
  using Ogama.Modules.Recording.TobiiInterface;
  using Ogama.Properties;

  /// <summary>
  /// The main frame <see cref="Form"/>. Is the MDI Parent. 
  /// Derived from <see cref="FormWithAccellerators"/>.
  /// Owns menu, toolbar and status bar and the message handler therefore.
  /// </summary>
  public partial class MainForm : FormWithAccellerators
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
    /// Filename of an ogama project to load when user double clicked
    /// in explorer on project to open OGAMA.
    /// </summary>
    private string commandLineFileName;

    /// <summary>
    /// Flag. Indicates loading project from Splash dialog or command line
    /// </summary>
    private bool loadFileFromCommandLine;

    #endregion //FIELDS

    ///////////////////////////////////////////////////////////////////////////////
    // Construction and Initializing methods                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region CONSTRUCTION

    /// <summary>
    /// Initializes a new instance of the MainForm class.
    /// </summary>
    /// <param name="fileName">A <see cref="string"/> with an optional filename
    /// from the command line to load.</param>
    public MainForm(string fileName)
    {
      if (fileName == string.Empty)
      {
        this.loadFileFromCommandLine = false;
      }
      else
      {
        this.loadFileFromCommandLine = true;
        this.commandLineFileName = fileName;
      }

      this.InitializeComponent();
      this.InitAccelerators();

      try
      {
        TobiiTracker.StaticInitialize();
      }
      catch (Exception ex)
      {
        ExceptionMethods.ProcessErrorMessage(
          "The tobii SDK could not be initialized, the tobii record interface will not be"
          + "available. Please install apple bonjour, if this is a module load error."
          + ex.Message);
      }
    }

    #endregion //CONSTRUCTION

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Enumerations                                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region ENUMS

    /// <summary>
    /// Event. Is raised, when the application settings are changed in the
    /// options dialog. Updates the pictures.
    /// </summary>
    public event EventHandler OptionsChanged;

    /// <summary>
    /// Event. Should be raised, when a new stimulus bitmap is selected.
    /// Updates all views with new stimulus.
    /// </summary>
    public event EventHandler StimulusChanged;

    /// <summary>
    /// Event. Fires edit copy command to listeners.
    /// </summary>
    public event EventHandler EditCopy;

    /// <summary>
    /// Event. Fires edit paste command to listeners.
    /// </summary>
    public event EventHandler EditPaste;

    /// <summary>
    /// Event. Fires edit cut command to listeners.
    /// </summary>
    public event EventHandler EditCut;

    /// <summary>
    /// Event. Fires save image command to listeners.
    /// </summary>
    public event EventHandler EditSaveImage;

    #endregion ENUMS

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Properties                                                       //
    ///////////////////////////////////////////////////////////////////////////////
    #region PROPERTIES

    /// <summary>
    /// Gets main form right align status label.
    /// </summary>
    /// <value>The <see cref="ToolStripStatusLabel"/> right status label of the main form.</value>
    public ToolStripStatusLabel StatusRightLabel
    {
      get { return this.lblDataStates; }
    }

    /// <summary>
    /// Gets main form status label.
    /// </summary>
    /// <value>The <see cref="ToolStripStatusLabel"/> left status label of the main form.</value>
    public ToolStripStatusLabel StatusLabel
    {
      get { return this.lblStatus; }
    }

    /// <summary>
    /// Gets main form progress bar.
    /// </summary>
    /// <value>The <see cref="ToolStripProgressBar"/> of the main form.</value>
    public ToolStripProgressBar StatusProgressbar
    {
      get { return this.prbStatus; }
    }

    /// <summary>
    /// Gets the <see cref="ContextPanel"/>.
    /// </summary>
    /// <value>The <see cref="ContextPanel"/> user control.</value>
    public ContextPanel.ContextPanel ContextPanel
    {
      get { return this.contextPanel; }
    }

    #endregion //PROPERTIES

    ///////////////////////////////////////////////////////////////////////////////
    // Public methods                                                            //
    ///////////////////////////////////////////////////////////////////////////////
    #region PUBLICMETHODS

    /// <summary>
    /// Reinitializes Context Panels subject tab
    /// </summary>
    /// <remarks>Called, when database changed.</remarks>
    public void RefreshContextPanelSubjects()
    {
      this.contextPanel.RepopulateSubjectTab();
    }

    /// <summary>
    /// Recreates the whole thumbs and image lists for the context panel.
    /// </summary>
    /// <remarks>Should be called, when stimuli are changed.</remarks>
    public void RefreshContextPanelImageTabs()
    {
      this.contextPanel.RepopulateThumbsListTab();
      this.contextPanel.RepopulateImageListTab();
    }

    /// <summary>
    /// The public OnStimulusChanged method raises the event by invoking 
    /// the delegates. The sender is always this, the current instance 
    /// of the class.
    /// </summary>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    public virtual void OnStimulusChanged(EventArgs e)
    {
      // Invokes the delegates. 
      if (this.StimulusChanged != null)
      {
        this.StimulusChanged(this, e);
      }
    }

    #endregion //PUBLICMETHODS

    ///////////////////////////////////////////////////////////////////////////////
    // Inherited methods                                                         //
    ///////////////////////////////////////////////////////////////////////////////
    #region OVERRIDES

    /// <summary>
    /// Initializes accelerator keys.
    /// </summary>
    protected override void InitAccelerators()
    {
      this.SetAccelerator(Keys.Control | Keys.C, new AcceleratorAction(this.CopyToClipboard));
      this.SetAccelerator(Keys.Control | Keys.V, new AcceleratorAction(this.PasteFromClipboard));
      this.SetAccelerator(Keys.Control | Keys.X, new AcceleratorAction(this.CutToClipboard));
      this.SetAccelerator(Keys.Control | Keys.I, new AcceleratorAction(this.SaveImage));
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
    /// The <see cref="Form.OnLoad"/> event handler. 
    /// Checks for command line file, otherwise show start up screen.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void MainForm_Load(object sender, EventArgs e)
    {
      if (this.loadFileFromCommandLine)
      {
        // after successful loading enable main menu items
        if (this.OpenDocument(this.commandLineFileName))
        {
          this.ChangeMenuItems(true);
        }
        else
        {
          string message = "Project file given in the command line during start could not be openend."
          + Environment.NewLine + "Now trying to load from standard dialog.";
          ExceptionMethods.ProcessErrorMessage(message);
          Document.ActiveDocument = null;
          this.LoadProjectWithStartUpScreen();
        }
      }
      else
      {
        this.LoadProjectWithStartUpScreen();
      }
    }

    /// <summary>
    /// The <see cref="Form.FormClosing"/> event handler.
    /// Saves application settings when closing form.
    /// Asks for saving <see cref="Properties.ExperimentSettings"/>
    /// if modified.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">A <see cref="FormClosingEventArgs"/> with the event data.</param>
    private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
    {
      if (!e.Cancel)
      {
        if (Document.ActiveDocument != null && !this.SaveAndDisposeDocument())
        {
          // Saving has been cancelled
          e.Cancel = true;
        }
      }
    }

    #endregion //WINDOWSEVENTHANDLER

    ///////////////////////////////////////////////////////////////////////////////
    // Eventhandler for Custom Defined Events                                    //
    ///////////////////////////////////////////////////////////////////////////////
    #region CUSTOMEVENTHANDLER

    /// <summary>
    /// The OnOptionsChanged method raises the event by invoking 
    /// the delegates. The sender is always this, the current instance 
    /// of the class.
    /// </summary>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void OnOptionsChanged(EventArgs e)
    {
      // Invokes the delegates. 
      if (this.OptionsChanged != null)
      {
        this.OptionsChanged(this, e);
      }
    }

    /// <summary>
    /// The protected OnEditCopy method raises the event by 
    /// invoking the delegates. The sender is always this, the current instance 
    /// of the class.
    /// </summary>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void OnEditCopy(EventArgs e)
    {
      // Invokes the delegates. 
      if (this.EditCopy != null)
      {
        this.EditCopy(this, e);
      }
    }

    /// <summary>
    /// The protected OnEditPaste method raises the event by invoking 
    /// the delegates. The sender is always this, the current instance 
    /// of the class.
    /// </summary>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void OnEditPaste(EventArgs e)
    {
      // Invokes the delegates. 
      if (this.EditPaste != null)
      {
        this.EditPaste(this, e);
      }
    }

    /// <summary>
    /// The protected OnEditCut method raises the event by invoking 
    /// the delegates. The sender is always this, the current instance 
    /// of the class.
    /// </summary>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void OnEditCut(EventArgs e)
    {
      // Invokes the delegates. 
      if (this.EditCut != null)
      {
        this.EditCut(this, e);
      }
    }

    /// <summary>
    /// The protected OnEditSaveImage method raises the event by invoking 
    /// the delegates. The sender is always this, the current instance 
    /// of the class.
    /// </summary>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void OnEditSaveImage(EventArgs e)
    {
      // Invokes the delegates. 
      if (this.EditSaveImage != null)
      {
        this.EditSaveImage(this, e);
      }
    }

    /// <summary>
    /// MRU menu item is clicked - call owner's OpenMRUFile function
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void OnMRUClicked(object sender, EventArgs e)
    {
      string s;

      try
      {
        // cast sender object to MenuItem
        ToolStripMenuItem item = (ToolStripMenuItem)sender;

        if (item != null)
        {
          // Get file name from list using item index
          s = item.ToolTipText;

          // call owner's OpenMRUFile function
          if (s.Length > 0)
          {
            this.OpenMRUFile(s);
          }
        }
      }
      catch (Exception ex)
      {
        MessageBox.Show(
          "Exception in OnMRUClicked: " + ex.Message,
            Application.ProductName,
            MessageBoxButtons.OK,
            MessageBoxIcon.Stop);
      }
    }

    /// <summary>
    /// The <see cref="Modules.Recording.RecordModule.RecordingFinished"/> event handler for
    /// the module <see cref="Modules.Recording.RecordModule"/>.
    /// Updates modules that need to now about the new data.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void objfrmRecord_RecordingFinished(object sender, EventArgs e)
    {
      if (this.contextPanel.Visible)
      {
        this.contextPanel.RepopulateSubjectTab();
      }

      // Refreshes the binding sources
      foreach (Form view in this.MdiChildren)
      {
        if (view is FormWithInterface)
        {
          ((FormWithInterface)view).ResetDataBindings();
        }
      }
    }

    #endregion //CUSTOMEVENTHANDLER

    #endregion //EVENTS

    ///////////////////////////////////////////////////////////////////////////////
    // Methods and Eventhandling for Background tasks                            //
    ///////////////////////////////////////////////////////////////////////////////
    #region BACKGROUNDWORKER

    /// <summary>
    /// The <see cref="BackgroundWorker.DoWork"/> event handler for
    /// the <see cref="BackgroundWorker"/> <see cref="bgwLoad"/>.
    /// Shows the <see cref="LoadDocSplash"/> form with a splash screen
    /// wait dialog.
    /// </summary>
    /// <param name="sender">Source of the event</param>
    /// <param name="e">A <see cref="DoWorkEventArgs"/> with the event data.</param>
    private void bgwLoad_DoWork(object sender, DoWorkEventArgs e)
    {
      // Get the BackgroundWorker that raised this event.
      BackgroundWorker worker = sender as BackgroundWorker;

      LoadDocSplash newSplash = new LoadDocSplash();
      newSplash.Worker = worker;
      newSplash.ShowDialog();
    }

    #endregion //BACKGROUNDWORKER

    ///////////////////////////////////////////////////////////////////////////////
    // Methods for doing main class job                                          //
    ///////////////////////////////////////////////////////////////////////////////
    #region METHODS

    /// <summary>
    /// Destruction of Document
    /// </summary>
    private void CleanUpDocument()
    {
      Document.ActiveDocument.CleanUp();
      Document.ActiveDocument = null;
      this.contextPanel.Clear();
      this.ChangeMenuItems(false);
    }

    /// <summary>
    /// This method shows an dialog to ask for saving settings,
    /// and returns true if successful and false, if saving has been cancelled.
    /// </summary>
    /// <returns><strong>True</strong> if successful and <strong>false</strong>,
    /// if saving has been cancelled.</returns>
    private bool SaveAndDisposeDocument()
    {
      Properties.Settings.Default.Save();

      if (Document.ActiveDocument != null)
      {
        if (Document.ActiveDocument.Modified)
        {
          string message = "This document has been modified. "
            + Environment.NewLine +
            "Do you want to save ?";
          switch (InformationDialog.Show("Save document ?", message, true, MessageBoxIcon.Question))
          {
            case DialogResult.Yes:
              this.SaveExperimentToFile();
              break;
            case DialogResult.No:
              break;
            case DialogResult.Cancel:
              return false;
          }
        }

        RecentFilesList.List.Add(Document.ActiveDocument.ExperimentSettings.DocumentFilename);

        // CleanUpDocument
        this.CleanUpDocument();
      }

      return true;
    }

    /// <summary>
    /// Checks for existing document and starts open file dialog if there is none
    /// Otherwise throw error and return false.
    /// </summary>
    /// <returns><strong>True</strong>, if opening was successful,
    /// otherwise <strong>false</strong>.</returns>
    private bool OpenExperiment()
    {
      if (Document.ActiveDocument == null)
      {
        if (this.ofdExperiment.ShowDialog() == DialogResult.OK)
        {
          if (this.OpenDocument(this.ofdExperiment.FileName))
          {
            this.ChangeMenuItems(true);
          }

          return true;
        }

        this.ChangeMenuItems(false);
        return false;
      }

      string message = "Couldn't open experiment, because there is one already open." +
                       Environment.NewLine + "Please close it using File -> Close Experiment.";
      ExceptionMethods.ProcessErrorMessage(message);
      return false;
    }

    /// <summary>
    /// Opens document and initializes context panel.
    /// When loading finished shows <see cref="StartTask"/> dialog.
    /// </summary>
    /// <param name="filename">A <see cref="string"/> with 
    /// full path to experiment settings file</param>
    /// <returns><strong>True</strong> if opening was successful,
    /// otherwise <strong>false</strong>.</returns>
    private bool OpenDocument(string filename)
    {
      if (File.Exists(filename))
      {
        string fileLower = filename.ToLower();
        Document.ActiveDocument = new Document();

        if (this.bgwLoad.IsBusy)
        {
          return false;
        }

        this.bgwLoad.RunWorkerAsync();

        Application.DoEvents();

        if (Document.ActiveDocument.LoadDocument(fileLower, true, this.bgwLoad))
        {
          this.contextPanel.Init();
          this.bgwLoad.CancelAsync();
          if (this.ShowTaskChooseDialog())
          {
            return true;
          }
        }
        else
        {
          this.bgwLoad.CancelAsync();
          ExceptionMethods.ProcessMessage("Experiment not opened.", "OGAMA could/should not open the document.");
          if (Document.ActiveDocument != null)
          {
            this.CleanUpDocument();
          }

          return false;
        }
      }
      else
      {
        string message = "Experiment file could not be found." +
          Environment.NewLine + "It will be removed from the recent files list.";
        ExceptionMethods.ProcessErrorMessage(message);

        // Remove file from list.
        RecentFilesList.List.Remove(filename);
      }

      return false;
    }

    /// <summary>
    /// This LoadProjectWithStartUpScreen method shows a <see cref="StartUpDialog"/>
    /// dialog for experiment selection or creation.
    /// </summary>
    private void LoadProjectWithStartUpScreen()
    {
      try
      {
        StartUpDialog newStartUpDlg = new StartUpDialog();

        // Dictionary to remember full path from recent files list
        // but show onlay file names
        Dictionary<string, string> recentFilesWithPath = new Dictionary<string, string>();

      ShowStartDialog:
        recentFilesWithPath.Clear();
        newStartUpDlg.ListBoxItems.Clear();

        // Add recent files to dialogs recent experiments list box
        foreach (string entry in RecentFilesList.List)
        {
          if (entry != string.Empty)
          {
            string fileName = Path.GetFileName(entry);
            if (recentFilesWithPath.ContainsKey(fileName))
            {
              fileName += "(other path)";
            }

            recentFilesWithPath.Add(fileName, entry);
            if (!newStartUpDlg.ListBoxItems.Contains(fileName))
            {
              newStartUpDlg.ListBoxItems.Add(fileName);
            }
          }
        }

        newStartUpDlg.ListBoxItems.Add("search for other ...");

        if (newStartUpDlg.ShowDialog() == DialogResult.OK)
        {
          // Hide Dialog
          newStartUpDlg.Hide();
          this.Refresh();

          // Is new project or recent project ?
          bool isNewProject = newStartUpDlg.NewProject;
          if (isNewProject)
          {
            this.mnuFileNewExperiment_Click(null, EventArgs.Empty);
          }
          else
          {
            string selectedItem = string.Empty;

            // When user deselected any entry, use the project search mask.
            if (newStartUpDlg.SelectedItem == string.Empty)
            {
              selectedItem = "search for other ...";
            }
            else
            {
              selectedItem = newStartUpDlg.SelectedItem;
            }

            // searched experiment is not in recent files list,
            // so start with standard open file dialog
            if (selectedItem == "search for other ...")
            {
              if (!this.OpenExperiment())
              {
                goto ShowStartDialog;
              }
            }
            else
            {
              // after successful loading enable main menu items
              if (this.OpenDocument(recentFilesWithPath[selectedItem]))
              {
                this.ChangeMenuItems(true);
              }
              else
              {
                goto ShowStartDialog;
              }
            }
          }
        }
        else if (Document.ActiveDocument != null)
        {
          this.CleanUpDocument();
        }
      }
      catch (Exception ex)
      {
        if (Document.ActiveDocument != null)
        {
          this.CleanUpDocument();
        }

        ExceptionMethods.HandleException(ex);
      }
    }

    /// <summary>
    /// Shows a dialog with a question which task should be started.
    /// Referring to the answer the corresponding module is started.
    /// </summary>
    /// <returns><strong>True</strong> if selection was done, otherwise
    /// user clicked cancel and return <strong>false</strong>.</returns>
    private bool ShowTaskChooseDialog()
    {
      var objWhatToDo = new StartTask();
      if (objWhatToDo.ShowDialog() == DialogResult.OK)
      {
        switch (objWhatToDo.Task)
        {
          case Tasks.Replay:
            this.CreateReplayView();
            break;
          case Tasks.Scanpaths:
            this.CreateScanpathsView();
            break;
          case Tasks.Statistics:
            this.CreateStatisticsView();
            break;
          case Tasks.AOIs:
            this.CreateAOIView();
            break;
          case Tasks.AttentionMaps:
            this.CreateAttentionMapView();
            break;
          case Tasks.Fixations:
            this.CreateFixationsView();
            break;
          case Tasks.Import:
            if (this.CreateDatabaseView())
            {
              ImportRawData.Start(this);
            }

            break;
          case Tasks.Design:
            this.CreateStimuliDesignView();
            break;
          case Tasks.Record:
            this.CreateRecordingView();
            break;
        }

        return true;
      }

      return false;
    }

    /// <summary>
    /// Open file clicked from recent file list.
    /// </summary>
    /// <param name="fileName">A <see cref="string"/> with the 
    /// file name of the recent file to open.</param>
    private void OpenMRUFile(string fileName)
    {
      if (Document.ActiveDocument != null)
      {
        this.mnuFileCloseExperiment_Click(null, null);
      }

      if (this.OpenDocument(fileName))
      {
        this.ChangeMenuItems(true);
      }
    }

    #endregion //METHODS

    ///////////////////////////////////////////////////////////////////////////////
    // Small helping Methods                                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region HELPER

    /// <summary>
    /// Raises copy to clipboard event
    /// </summary>
    private void CopyToClipboard()
    {
      this.OnEditCopy(EventArgs.Empty);
    }

    /// <summary>
    /// Raises paste from clipboard event
    /// </summary>
    private void PasteFromClipboard()
    {
      this.OnEditPaste(EventArgs.Empty);
    }

    /// <summary>
    /// Raises cut to clipboard event
    /// </summary>
    private void CutToClipboard()
    {
      this.OnEditCut(EventArgs.Empty);
    }

    /// <summary>
    /// Raises save image to clipboard event
    /// </summary>
    private void SaveImage()
    {
      this.OnEditSaveImage(EventArgs.Empty);
    }

    /// <summary>
    /// Disables or enables menu items that are not used when no
    /// document is open.
    /// </summary>
    /// <param name="isEnabled"><strong>True</strong>, if items should be enabled,
    /// otherwise <strong>false</strong>.</param>
    private void ChangeMenuItems(bool isEnabled)
    {
      this.mnuEdit.Enabled = isEnabled;
      this.mnuViews.Enabled = isEnabled;
      this.mnuTools.Enabled = isEnabled;
    }

    #endregion //HELPER
  }
}
