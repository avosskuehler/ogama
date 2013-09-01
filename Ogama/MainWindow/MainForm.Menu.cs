// <copyright file="MainForm.Menu.cs" company="FU Berlin">
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

namespace Ogama.MainWindow
{
  using System;
  using System.Collections.Generic;
  using System.IO;
  using System.Text;
  using System.Windows.Forms;

  using Ogama.ExceptionHandling;
  using Ogama.MainWindow.Dialogs;
  using Ogama.Modules.ImportExport.RawData;
  using Ogama.Modules.Scanpath;
  using Ogama.Properties;

  /// <summary>
  /// The MainWindow.Menu.cs contains the event handler
  /// for the menu bar at the top of the main window.
  /// </summary>
  public partial class MainForm
  {
    /// <summary>
    /// This method opens experiment settings dialog and saves changes to Document.ActiveDocument.ExperimentSettings
    /// </summary>
    public static void ShowExperimentSettingsDialog()
    {
      if (Document.ActiveDocument != null)
      {
        ExperimentSettingsDialog popUpDlg = new ExperimentSettingsDialog();

        // Update dialog with settings not needed because it reads the current
        // settings of the Document singleton by itself
        // show dialog
        if (popUpDlg.ShowDialog() == DialogResult.OK)
        {
          // Read dialogs changes
          Document.ActiveDocument.ExperimentSettings = popUpDlg.ExperimentSettings;
          Document.ActiveDocument.Modified = true;
        }
      }
    }

    ////////////////////////////////////////////////////////////////////////
    // EventHandlers for UI Menu selections                                //
    ////////////////////////////////////////////////////////////////////////
    #region MenuEventHandlers

    #region mnuFile

    ////////////////////////////////////////////////////////////////////////
    // EventHandlers for File Menu                                         //
    ////////////////////////////////////////////////////////////////////////

    /// <summary>
    /// The <see cref="ToolStripDropDownItem.DropDownOpening"/> event handler
    /// for the <see cref="ToolStripMenuItem"/> <see cref="mnuFile"/>
    /// Initializes Menuitems when drop down.
    /// </summary>
    /// <param name="sender">Source of the event</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void mnuFile_DropDownOpening(object sender, EventArgs e)
    {
      this.mnuFileNewExperiment.Enabled = Document.ActiveDocument == null;
      this.mnuFileOpenExperiment.Enabled = Document.ActiveDocument == null;
      this.mnuFileCloseExperiment.Enabled = Document.ActiveDocument != null;
      this.mnuFileSaveExperiment.Enabled = Document.ActiveDocument != null;

      if (Document.ActiveDocument != null)
      {
        this.mnuFileSaveExperiment.Enabled = Document.ActiveDocument.Modified;
      }

      this.mnuFileSaveExperimentAs.Enabled = Document.ActiveDocument != null;

      // Disable menu item if MRU list is empty
      if (RecentFilesList.List.Count == 0)
      {
        this.mnuFileRecentFiles.Enabled = false;
      }
      else
      {
        this.mnuFileRecentFiles.Enabled = true;
      }

      this.mnuFileClearRecentFileList.Enabled = this.mnuFileRecentFiles.Enabled;

      // Enable when You implement a printing method
      this.mnuFilePrint.Enabled = false;
      this.mnuFilePrintPreview.Enabled = false;
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler
    /// for the <see cref="ToolStripMenuItem"/> <see cref="mnuFileNewExperiment"/>
    /// Creates new experiment through creating new database and
    /// asks for stimulus path and screen size.
    /// </summary>
    /// <param name="sender">Source of the event</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void mnuFileNewExperiment_Click(object sender, EventArgs e)
    {
      if (Document.ActiveDocument == null)
      {
        // Show new experiment storage dialog.
        NewExperiment objfrmNewExperimentDlg = new NewExperiment();
        if (objfrmNewExperimentDlg.ShowDialog() != DialogResult.OK)
        {
          return;
        }

        this.Cursor = Cursors.WaitCursor;

        string newExperimentFolder = Path.Combine(
          objfrmNewExperimentDlg.ParentFolder,
          objfrmNewExperimentDlg.ExperimentName);

        string newExperimentName = objfrmNewExperimentDlg.ExperimentName;
        string newExperimentFilename = Path.Combine(newExperimentFolder, newExperimentName + ".oga");

        Document.ActiveDocument = new Document();
        ExperimentSettings newSettings = new ExperimentSettings();

        // Show new experiment properties dialog.
        ExperimentSettingsDialog popUpDlg = new ExperimentSettingsDialog();
        if (popUpDlg.ShowDialog() == DialogResult.OK)
        {
          newSettings = popUpDlg.ExperimentSettings;
          newSettings.DocumentPath = newExperimentFolder;
          newSettings.Name = newExperimentName;
          newSettings.UpdateVersion();
        }
        else
        {
          return;
        }

        // Show loading splash screen
        this.bgwLoad.RunWorkerAsync();
        Application.DoEvents();

        Directory.CreateDirectory(newExperimentFolder);
        Directory.CreateDirectory(newSettings.ThumbsPath);
        Directory.CreateDirectory(newSettings.DatabasePath);
        Directory.CreateDirectory(newSettings.SlideResourcesPath);

        Document.ActiveDocument.ExperimentSettings = newSettings;

        string mdfSource = Application.StartupPath + "\\DataSet\\OgamaDatabaseTemplate.mdf";
        string ldfSource = Application.StartupPath + "\\DataSet\\OgamaDatabaseTemplate_log.ldf";
        if (File.Exists(mdfSource))
        {
          string mdfDestination = newSettings.DatabaseMDFFile;
          string ldfDestination = mdfDestination.Replace(".mdf", "_log.ldf");

          bool overwrite = false;
          bool skip = false;
          if (File.Exists(mdfDestination))
          {
            // Hide loading splash screen
            this.bgwLoad.CancelAsync();

            if (MessageBox.Show(
              "Overwrite existing Database File " + Environment.NewLine + mdfDestination,
              Application.ProductName,
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question) == DialogResult.Yes)
            {
              overwrite = true;
            }
            else
            {
              skip = true;
            }

            // Show loading splash screen again
            this.bgwLoad.RunWorkerAsync();
            Application.DoEvents();
          }

          try
          {
            if (!skip)
            {
              // Always delete ldf file
              if (File.Exists(ldfDestination))
              {
                File.Delete(ldfDestination);
              }

              File.Copy(mdfSource, mdfDestination, overwrite);
              File.Copy(ldfSource, ldfDestination, overwrite);
            }
          }
          catch (ArgumentException ex)
          {
            ExceptionMethods.ProcessErrorMessage(ex.Message);
          }
          catch (IOException ex)
          {
            ExceptionMethods.ProcessErrorMessage(ex.Message);
          }
          catch (NotSupportedException ex)
          {
            ExceptionMethods.ProcessErrorMessage(ex.Message);
          }
          catch (UnauthorizedAccessException ex)
          {
            ExceptionMethods.ProcessErrorMessage(ex.Message);
          }
          catch (Exception ex)
          {
            ExceptionMethods.HandleException(ex);
          }

          if (!Document.ActiveDocument.SaveDocument(newExperimentFilename, this.bgwLoad))
          {
            ExceptionMethods.ProcessErrorMessage("Couldn't create document");
            this.ChangeMenuItems(false);
            if (Document.ActiveDocument != null)
            {
              this.CleanUpDocument();
            }
          }
          else
          {
            if (!Document.ActiveDocument.LoadSQLData(this.bgwLoad))
            {
              ExceptionMethods.ProcessErrorMessage("Couldn't create document, because database loading failed.");
              this.ChangeMenuItems(false);
              if (Document.ActiveDocument != null)
              {
                this.CleanUpDocument();
              }
            }
            else
            {
              this.contextPanel.Init();
              RecentFilesList.List.Add(newExperimentFilename);
            }

            // Hide loading splash screen
            this.bgwLoad.CancelAsync();

            if (Document.ActiveDocument != null)
            {
              this.ShowTaskChooseDialog();
              this.ChangeMenuItems(true);
            }
          }
        }
        else
        {
          // DataBaseSourceFile does not exist in the expected directory
          string message = "Could not find DataBaseTemplate File:" + Environment.NewLine;
          message += mdfSource;
          message += Environment.NewLine + "Please make sure the File OgamaDatabaseTemplate.mdf (supplied with the application installation) is in the directory listed before and try again.";

          ExceptionMethods.ProcessErrorMessage(message);
        }

        // Hide loading splash screen
        this.bgwLoad.CancelAsync();
      }
      else
      {
        // mDocument.ActiveDocument!=null
        string message = "Couldn't create new experiment, because there is one already open." +
          Environment.NewLine + "Please close it using File -> Close Experiment.";
        ExceptionMethods.ProcessErrorMessage(message);
      }

      this.Cursor = Cursors.Default;
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler
    /// for the <see cref="ToolStripMenuItem"/> <see cref="mnuFileOpenExperiment"/>
    /// Opens existing experiment and displays task choose dialog.
    /// </summary>
    /// <param name="sender">Source of the event</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void mnuFileOpenExperiment_Click(object sender, EventArgs e)
    {
      this.OpenExperiment();
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler
    /// for the <see cref="ToolStripMenuItem"/> <see cref="mnuFileCloseExperiment"/>
    /// Closes all views and releases Document.
    /// </summary>
    /// <param name="sender">Source of the event</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void mnuFileCloseExperiment_Click(object sender, EventArgs e)
    {
      if (Document.ActiveDocument != null)
      {
        RecentFilesList.List.Add(Document.ActiveDocument.ExperimentSettings.DocumentFilename);

        // Close all views.
        foreach (Form view in this.MdiChildren)
        {
          view.Close();
        }

        // Save Document
        this.SaveAndDisposeDocument();
      }
      else
      {
        string message = "Couldn't close experiment, because it seems to me that there is none open.";
        ExceptionMethods.ProcessErrorMessage(message);
      }
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler
    /// for the <see cref="ToolStripMenuItem"/> <see cref="mnuFileSaveExperiment"/>
    /// Saves experiment settings in .oga file
    /// </summary>
    /// <param name="sender">Source of the event</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void mnuFileSaveExperiment_Click(object sender, EventArgs e)
    {
      this.SaveExperimentToFile();
    }

    /// <summary>
    /// This method checks for updates on the slideshow and
    /// afterwards saves all changes of the experiment to file.
    /// </summary>
    private void SaveExperimentToFile()
    {
      // Display Wait Cursor
      this.Cursor = Cursors.WaitCursor;

      if (Document.ActiveDocument != null && Document.ActiveDocument.ExperimentSettings != null)
      {
        this.StatusLabel.Text = "Saving experiment to file ...";

        // Check for modified slideshow in the slideshow module
        foreach (Form form in this.MdiChildren)
        {
          if (form is Modules.SlideshowDesign.SlideshowModule)
          {
            (form as Modules.SlideshowDesign.SlideshowModule).SaveSlideshowToDocument();
          }
        }

        // Save Document to disk
        if (!Document.ActiveDocument.SaveDocument(Document.ActiveDocument.ExperimentSettings.DocumentFilename, null))
        {
          ExceptionMethods.ProcessErrorMessage("Couldn't save experiment.");
        }
      }

      // Reset Cursor
      this.Cursor = Cursors.Default;
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler
    /// for the <see cref="ToolStripMenuItem"/> <see cref="mnuFileSaveExperimentAs"/>
    /// Saves experiment setting in new .oga file and duplicates database.
    /// </summary>
    /// <remarks>This function currently does not work and the menu
    /// entry is disabled in the <see cref="MainForm"/> designer because 
    /// I don´t now how to detach
    /// the mdf database file before duplicating it.
    /// If it is not detached while copying an exception is thrown, because the file is in use.
    /// </remarks>
    /// <param name="sender">Source of the event</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void mnuFileSaveExperimentAs_Click(object sender, EventArgs e)
    {
      ////if (Document.ActiveDocument != null)
      ////{
      ////  if (sfdExperiment.ShowDialog() != DialogResult.OK)
      ////  {
      ////    return;
      ////  }
      ////  ExperimentSettings newSettings = new ExperimentSettings();

      ////  string fileName=sfdExperiment.FileName;
      ////  string oldExpTitle=Document.ActiveDocument.ExperimentSettings.Name;
      ////  string newExpTitle = Path.GetFileNameWithoutExtension(fileName);
      ////  string oldExpPath = Document.ActiveDocument.ExperimentSettings.SettingsPath;
      ////  string newExpPath = Path.GetDirectoryName(fileName) + Path.DirectorySeparatorChar;

      ////  mnuFileCloseExperiment_Click(this, EventArgs.Empty);

      ////  string oldFileThumbs = oldExpPath + oldExpTitle + "Images.thb";
      ////  string newFileThumbs = newExpPath + newExpTitle + "Images.thb";
      ////  string oldFileThumbsList = oldExpPath + oldExpTitle + "Images.thl";
      ////  string newFileThumbsList = newExpPath + newExpTitle + "Images.thl";
      ////  string oldFileDatabaseMDF = oldExpPath + oldExpTitle + ".mdf";
      ////  string newFileDatabaseMDF = newExpPath + newExpTitle + ".mdf";
      ////  string oldFileDatabaseLDF = oldExpPath + oldExpTitle + "_log.ldf";
      ////  string newFileDatabaseLDF = newExpPath + newExpTitle + "_log.ldf";

      ////  DialogResult answer;
      ////  if (File.Exists(oldFileThumbs))
      ////  {
      ////    if (File.Exists(newFileThumbs))
      ////    {
      ////      answer=MessageBox.Show("The destination thumbs file '"+ 
      ////        Path.GetFileName(newFileThumbs)+ 
      ////        "' exists already. Would you like to overwrite it ? ",
      ////        Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
      ////      if (answer == DialogResult.Yes)
      ////        File.Copy(oldFileThumbs, newFileThumbs, true);
      ////    }
      ////    else
      ////      File.Copy(oldFileThumbs, newFileThumbs, true);
      ////  }
      ////  if (File.Exists(oldFileThumbsList))
      ////  {
      ////    if (File.Exists(newFileThumbsList))
      ////    {
      ////      answer = MessageBox.Show("The destination thumbs list file '" +
      ////        Path.GetFileName(newFileThumbsList) +
      ////        "' exists already. Would you like to overwrite it ? ",
      ////        Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
      ////      if (answer == DialogResult.Yes)
      ////        File.Copy(oldFileThumbsList, newFileThumbsList, true);
      ////    }
      ////    else
      ////      File.Copy(oldFileThumbsList, newFileThumbsList, true);
      ////  }
      ////  if (File.Exists(oldFileDatabaseMDF))
      ////  {
      ////    if (File.Exists(newFileDatabaseMDF))
      ////    {
      ////      answer = MessageBox.Show("The destination database MDF file '" +
      ////        Path.GetFileName(newFileDatabaseMDF) +
      ////        "' exists already. Would you like to overwrite it ? ",
      ////        Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
      ////      if (answer==DialogResult.Yes) 
      ////        File.Copy(oldFileDatabaseMDF, newFileDatabaseMDF,true);
      ////    }
      ////    else
      ////      File.Copy(oldFileDatabaseMDF, newFileDatabaseMDF, true);
      ////  }
      ////  if (File.Exists(oldFileDatabaseLDF))
      ////  {
      ////    if (File.Exists(newFileDatabaseMDF))
      ////    {
      ////      answer = MessageBox.Show("The destination database LDF file '" +
      ////        Path.GetFileName(newFileDatabaseMDF) +
      ////        "' exists already. Would you like to overwrite it ? ",
      ////        Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
      ////      if (answer == DialogResult.Yes)
      ////        File.Copy(oldFileDatabaseLDF, newFileDatabaseLDF,true);
      ////    }
      ////    else
      ////      File.Copy(oldFileDatabaseLDF, newFileDatabaseLDF, true);
      ////  }

      ////  Document newdocument = new Document();
      ////  newdocument.ExperimentSettings = newSettings;
      ////  newdocument.SaveDocument(fileName);
      ////  Document.ActiveDocument = newdocument;
      ////  if (newdocument == null)
      ////  {
      ////    MessageBox.Show("Couldn't create Document",
      ////        Application.ProductName,
      ////        MessageBoxButtons.OK,
      ////        MessageBoxIcon.Exclamation);
      ////  }
      ////  else
      ////  {
      ////    foreach (FormWithAccellerators view in MdiChildren)
      ////    {
      ////      view.Document = newdocument;
      ////    }
      ////    RecentFilesList.List.Add(sfdExperiment.FileName);
      ////  }
      ////}
      ////else
      ////// mDocument.ActiveDocument==null
      ////{
      ////  MessageBox.Show("Couldn't save experiment, because it seems to me that there is none open.",
      ////      Application.ProductName,
      ////      MessageBoxButtons.OK,
      ////      MessageBoxIcon.Stop);
      ////}
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler
    /// for the <see cref="ToolStripMenuItem"/> <see cref="mnuFileExit"/>
    /// Exits application and asks for saving experiment settings during
    /// <see cref="Form.FormClosing"/>, if they are modifed.
    /// </summary>
    /// <param name="sender">Source of the event</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void mnuFileExit_Click(object sender, EventArgs e)
    {
      // CleanUp is performed in Form_Closing
      this.Close();
    }

    /// <summary>
    /// The <see cref="ToolStripDropDownItem.DropDownOpening"/> event handler
    /// for the <see cref="ToolStripMenuItem"/> <see cref="mnuFileRecentFiles"/>
    /// Updates recent file list menu when opening.
    /// </summary>
    /// <param name="sender">Source of the event</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void mnuFileRecentFiles_DropDownOpening(object sender, EventArgs e)
    {
      // remove all childs
      if (this.mnuFileRecentFiles.HasDropDownItems)
      {
        this.mnuFileRecentFiles.DropDownItems.Clear();
      }

      // Disable menu item if MRU list is empty
      if (RecentFilesList.List.Count == 0)
      {
        this.mnuFileRecentFiles.Enabled = false;
        this.mnuFileClearRecentFileList.Enabled = false;
        return;
      }

      // enable menu item and add child items
      this.mnuFileRecentFiles.Enabled = true;

      ToolStripMenuItem item;

      System.Collections.Specialized.StringEnumerator myEnumerator =
        RecentFilesList.List.GetEnumerator();

      while (myEnumerator.MoveNext())
      {
        if ((string)myEnumerator.Current != string.Empty)
        {
          string fileName = (string)myEnumerator.Current;
          item = new ToolStripMenuItem(RecentFilesList.List.GetDisplayName(fileName));
          item.ToolTipText = fileName;

          // subscribe to item's Click event
          item.Click += new EventHandler(this.OnMRUClicked);

          this.mnuFileRecentFiles.DropDownItems.Add(item);
        }
      }
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler
    /// for the <see cref="ToolStripMenuItem"/> <see cref="mnuFileClearRecentFileList"/>
    /// Clears all recent files in the list.
    /// </summary>
    /// <param name="sender">Source of the event</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void mnuFileClearRecentFileList_Click(object sender, EventArgs e)
    {
      RecentFilesList.List.Clear();
      RecentFilesList.List.Delete();
      this.mnuFileRecentFiles.Enabled = false;
      this.mnuFileClearRecentFileList.Enabled = false;
    }

    #endregion //mnuFile

    #region mnuEdit

    ////////////////////////////////////////////////////////////////////////
    // EventHandlers for Edit Menu                                         //
    ////////////////////////////////////////////////////////////////////////

    /// <summary>
    /// The <see cref="Control.Click"/> event handler
    /// for the <see cref="ToolStripMenuItem"/> <see cref="mnuEditSaveImage"/>
    /// Make a screenshot of current module picture into a file by
    /// invoking <see cref="MainForm.EditSaveImage"/>
    /// </summary>
    /// <param name="sender">Source of the event</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void mnuEditSaveImage_Click(object sender, EventArgs e)
    {
      this.OnEditSaveImage(e);
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler
    /// for the <see cref="ToolStripMenuItem"/> <see cref="mnuEditCopy"/>
    /// Copys data of module to the clipboard by
    /// invoking <see cref="MainForm.EditCopy"/>
    /// The data transferred depends on the module.
    /// </summary>
    /// <param name="sender">Source of the event</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void mnuEditCopy_Click(object sender, EventArgs e)
    {
      this.OnEditCopy(e);
    }

    #endregion //mnuEdit

    #region mnuViews

    ////////////////////////////////////////////////////////////////////////
    // EventHandlers for DataViews Menu                                    //
    ////////////////////////////////////////////////////////////////////////

    /// <summary>
    /// The <see cref="ToolStripDropDownItem.DropDownOpening"/> event handler
    /// for the <see cref="ToolStripMenuItem"/> <see cref="mnuFileRecentFiles"/>
    /// Updates menu items when opening.
    /// </summary>
    /// <param name="sender">Source of the event</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void mnuViews_DropDownOpening(object sender, EventArgs e)
    {
      this.mnuViewsCloseChild.Enabled = this.ActiveMdiChild != null;
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler
    /// for the <see cref="ToolStripMenuItem"/> <see cref="mnuViewsNewDatabase"/>
    /// Creates a new <see cref="Modules.Database.DatabaseModule"/>.
    /// </summary>
    /// <param name="sender">Source of the event</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void mnuViewsNewDatabase_Click(object sender, EventArgs e)
    {
      this.CreateOrActivateDatabaseModule();
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler
    /// for the <see cref="ToolStripMenuItem"/> <see cref="mnuViewsNewReplay"/>
    /// Creates a new <see cref="Modules.Replay.ReplayModule"/> module
    /// </summary>
    /// <param name="sender">Source of the event</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void mnuViewsNewReplay_Click(object sender, EventArgs e)
    {
      this.CreateOrActivateReplayModule();
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler
    /// for the <see cref="ToolStripMenuItem"/> <see cref="mnuViewsNewAttentionMap"/>
    /// Creates a new <see cref="Modules.AttentionMap.AttentionMapModule"/>.
    /// </summary>
    /// <param name="sender">Source of the event</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void mnuViewsNewAttentionMap_Click(object sender, EventArgs e)
    {
      this.CreateOrActivateAttentionMapModule();
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler
    /// for the <see cref="ToolStripMenuItem"/> <see cref="mnuViewsNewAOI"/>
    /// Creates a new <see cref="Modules.AOI.AOIModule"/>.
    /// </summary>
    /// <param name="sender">Source of the event</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void mnuViewsNewAOI_Click(object sender, EventArgs e)
    {
      this.CreateOrActivateAreasOfInterestModule();
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler
    /// for the <see cref="ToolStripMenuItem"/> <see cref="mnuViewsNewFixations"/>
    /// Creates a new <see cref="Modules.Fixations.FixationsModule"/> module.
    /// </summary>
    /// <param name="sender">Source of the event</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void mnuViewsNewFixations_Click(object sender, EventArgs e)
    {
      this.CreateOrActivateFixationModule();
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler
    /// for the <see cref="ToolStripMenuItem"/> <see cref="mnuViewsNewStatistics"/>
    /// Creates a new <see cref="Modules.Statistics.StatisticsModule"/> module.
    /// </summary>
    /// <param name="sender">Source of the event</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void mnuViewsNewStatistics_Click(object sender, EventArgs e)
    {
      this.CreateOrActivateStatistikModule();
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler
    /// for the <see cref="ToolStripMenuItem"/> <see cref="mnuViewsNewSaliency"/>
    /// Creates a new <see cref="Modules.Saliency.SaliencyModule"/>.
    /// </summary>
    /// <param name="sender">Source of the event</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void mnuViewsNewSaliency_Click(object sender, EventArgs e)
    {
      this.CreateOrActivateSaliencyModule();
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler
    /// for the <see cref="ToolStripMenuItem"/> <see cref="mnuViewsNewStimuliCreation"/>
    /// Creates a new <see cref="Modules.SlideshowDesign.SlideshowModule"/>.
    /// </summary>
    /// <param name="sender">Source of the event</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void mnuViewsNewStimuliCreation_Click(object sender, EventArgs e)
    {
      this.CreateOrActivateStimuliDesignModule();
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler
    /// for the <see cref="ToolStripMenuItem"/> <see cref="mnuViewsNewRecording"/>
    /// Creates a new <see cref="Modules.Recording.RecordModule"/>.
    /// </summary>
    /// <param name="sender">Source of the event</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void mnuViewsNewRecording_Click(object sender, EventArgs e)
    {
      this.CreateOrActivateRecordModule();
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler
    /// for the <see cref="ToolStripMenuItem"/> <see cref="mnuViewsNewScanpaths"/>.
    /// Creates a new <see cref="ScanpathsModule"/> module.
    /// </summary>
    /// <param name="sender">Source of the event</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void mnuViewsNewScanpaths_Click(object sender, EventArgs e)
    {
      this.CreateOrActivateScanpathModule();
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler
    /// for the <see cref="ToolStripMenuItem"/> <see cref="mnuViewsCloseChild"/>
    /// Closes current active view.
    /// <remarks>In the view_closing event, it will be checked whether
    /// this is the last view, so then the Document will
    /// also be closed.</remarks>
    /// </summary>
    /// <param name="sender">Source of the event</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void mnuViewsCloseChild_Click(object sender, EventArgs e)
    {
      if (this.ActiveMdiChild != null)
      {
        this.ActiveMdiChild.Close();
      }
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler
    /// for the <see cref="ToolStripMenuItem"/> <see cref="mnuViewsStatusBar"/>
    /// Switches visibility of the statusbar.
    /// </summary>
    /// <param name="sender">Source of the event</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void mnuViewsStatusBar_Click(object sender, EventArgs e)
    {
      this.stsMain.Visible = !this.stsMain.Visible;
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler
    /// for the <see cref="ToolStripMenuItem"/> <see cref="mnuViewsContextPanel"/>
    /// Switch visibility of the context panel with
    /// help tab, subject tab and image browser
    /// </summary>
    /// <param name="sender">Source of the event</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void mnuViewsContextPanel_Click(object sender, EventArgs e)
    {
      this.contextPanel.Visible = !this.contextPanel.Visible;
    }

    #endregion //mnuDataViews

    #region mnuTools

    ////////////////////////////////////////////////////////////////////////
    // EventHandlers for Tools Menu                                        //
    ////////////////////////////////////////////////////////////////////////

    /// <summary>
    /// The <see cref="Control.Click"/> event handler
    /// for the <see cref="ToolStripMenuItem"/> <see cref="mnuToolsExperimentSettings"/>
    /// Opens experiment settings dialog and saves changes to Document.ActiveDocument.ExperimentSettings
    /// </summary>
    /// <param name="sender">Source of the event</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void mnuToolsExperimentSettings_Click(object sender, EventArgs e)
    {
      ShowExperimentSettingsDialog();
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler
    /// for the <see cref="ToolStripMenuItem"/> <see cref="mnuToolsRecalculateStimuliThumbs"/>
    /// Recreates new stimuli thumbs for the context panel.
    /// </summary>
    /// <param name="sender">Source of the event</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void mnuToolsRecalculateStimuliThumbs_Click(object sender, EventArgs e)
    {
      this.Cursor = Cursors.WaitCursor;
      this.contextPanel.RepopulateImageListTab();
      this.contextPanel.RepopulateThumbsListTab();
      this.Cursor = Cursors.Default;
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler
    /// for the <see cref="ToolStripMenuItem"/> <see cref="mnuToolsOptions"/>
    /// Opens application settings dialog.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void mnuToolsOptions_Click(object sender, EventArgs e)
    {
      Options optionsDlg = new Options();
      if (optionsDlg.ShowDialog() == DialogResult.OK)
      {
        Properties.Settings.Default.Save();
        this.OnOptionsChanged(EventArgs.Empty);
      }
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler
    /// for the <see cref="ToolStripMenuItem"/> <see cref="mnuToolsImport"/>
    /// Starts import assistent.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void mnuToolsImport_Click(object sender, EventArgs e)
    {
      ImportRawData.Start(this);
      this.RefreshContextPanelSubjects();
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler
    /// for the <see cref="ToolStripMenuItem"/> <see cref="mnuToolsDatabaseConnection"/>
    /// Starts dialog for manually editing database connection string.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void mnuToolsDatabaseConnection_Click(object sender, EventArgs e)
    {
      SQLConnectionDialog connectionDialog = new SQLConnectionDialog();
      connectionDialog.ConnectionString = Document.ActiveDocument.ExperimentSettings.DatabaseConnectionString;
      if (connectionDialog.ShowDialog() == DialogResult.OK)
      {
        Document.ActiveDocument.ExperimentSettings.CustomConnectionString =
          connectionDialog.ConnectionString;
        Document.ActiveDocument.Modified = true;
      }
    }

    #endregion //mnuTools

    #region mnuWindow

    ////////////////////////////////////////////////////////////////////////
    // EventHandlers for Window Menu                                       //
    ////////////////////////////////////////////////////////////////////////

    /// <summary>
    /// The <see cref="ToolStripDropDownItem.DropDownOpening"/> event handler
    /// for the <see cref="ToolStripMenuItem"/> <see cref="mnuWindow"/>
    /// Updates menu states from window menu drop downs.
    /// </summary>
    /// <param name="sender">Source of the event</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void mnuWindow_DropDownOpening(object sender, EventArgs e)
    {
      this.mnuWindowCascade.Enabled = this.MdiChildren.Length != 0;
      this.mnuWindowTileHorizontal.Enabled = this.MdiChildren.Length != 0;
      this.mnuWindowTileVertical.Enabled = this.MdiChildren.Length != 0;
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler
    /// for the <see cref="ToolStripMenuItem"/> <see cref="mnuWindowCascade"/>
    /// All MDI child windows are cascaded within the client region of the MDI parent form. 
    /// </summary>
    /// <param name="sender">Source of the event</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void mnuWindowCascade_Click(object sender, EventArgs e)
    {
      this.LayoutMdi(MdiLayout.Cascade);
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler
    /// for the <see cref="ToolStripMenuItem"/> <see cref="mnuWindowTileVertical"/>
    /// All MDI child windows are tiled vertically within the client region of the MDI parent form. 
    /// </summary>
    /// <param name="sender">Source of the event</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void mnuWindowTileVertical_Click(object sender, EventArgs e)
    {
      this.LayoutMdi(MdiLayout.TileVertical);
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler
    /// for the <see cref="ToolStripMenuItem"/> <see cref="mnuWindowTileHorizontal"/>
    /// All MDI child windows are tiled horizontally within the client region of the MDI parent form. 
    /// </summary>
    /// <param name="sender">Source of the event</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void mnuWindowTileHorizontal_Click(object sender, EventArgs e)
    {
      this.LayoutMdi(MdiLayout.TileHorizontal);
    }

    #endregion //mnuWindow

    #region mnuHelp

    ////////////////////////////////////////////////////////////////////////
    // EventHandlers for Help Menu                                         //
    ////////////////////////////////////////////////////////////////////////

    /// <summary>
    /// The <see cref="ToolStripDropDownItem.DropDownOpening"/> event handler
    /// for the <see cref="ToolStripMenuItem"/> <see cref="mnuHelp"/>
    /// Updates menu states for help menu drop downs.
    /// </summary>
    /// <param name="sender">Source of the event</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void mnuHelp_DropDownOpening(object sender, EventArgs e)
    {
      this.mnuHelpSource.Visible = this.contextPanel.Visible;
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler
    /// for the <see cref="ToolStripMenuItem"/> <see cref="mnuHelpSource"/>
    /// Displays CHM file with source code documentation.
    /// </summary>
    /// <remarks>The source code documentation is automatically
    /// generated from the xml statements written in in the source
    /// code itself with the help of sandcastle builder.</remarks>
    /// <param name="sender">Source of the event</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void mnuHelpSource_Click(object sender, EventArgs e)
    {
      Help.ShowHelp(this, Application.StartupPath + @"\Help\OgamaSourceDocumentation.chm");
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler
    /// for the <see cref="ToolStripMenuItem"/> <see cref="mnuHelpContents"/>
    /// Displays context help in the context panel help tab.
    /// </summary>
    /// <param name="sender">Source of the event</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void mnuHelpContents_Click(object sender, EventArgs e)
    {
      if (!this.contextPanel.Visible)
      {
        this.contextPanel.Visible = true;
      }

      this.contextPanel.SelectHelpTab();
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler
    /// for the <see cref="ToolStripMenuItem"/> <see cref="mnuHelpAbout"/>
    /// Displays about box.
    /// </summary>
    /// <param name="sender">Source of the event</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void mnuHelpAbout_Click(object sender, EventArgs e)
    {
      AboutBox objAboutBox = new AboutBox();
      objAboutBox.ShowDialog();
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler
    /// for the <see cref="ToolStripMenuItem"/> <see cref="mnuHelpHowToTobii"/>
    /// Displays instructions to activate tobii recording.
    /// </summary>
    /// <param name="sender">Source of the event</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void mnuHelpHowToTobii_Click(object sender, EventArgs e)
    {
      HowToActivateTobii objActivateTobii = new HowToActivateTobii();
      objActivateTobii.ShowDialog();
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler
    /// for the <see cref="ToolStripMenuItem"/> <see cref="mnuHelpHowToMirametrix"/>
    /// Displays instructions to activate mirametrix recording.
    /// </summary>
    /// <param name="sender">Source of the event</param>
    /// <param name="e">An empty</param>
    private void mnuHelpHowToMirametrix_Click(object sender, EventArgs e)
    {
        HowToActivateMirametrix objActivateMirametrix = new HowToActivateMirametrix();
        objActivateMirametrix.ShowDialog();
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler
    /// for the <see cref="ToolStripMenuItem"/> <see cref="mnuHelpHowToAsl"/>
    /// Displays instructions to activate ASL recording.
    /// </summary>
    /// <param name="sender">Source of the event</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void mnuHelpHowToAsl_Click(object sender, EventArgs e)
    {
      HowToActivateAsl objActivateAsl = new HowToActivateAsl();
      objActivateAsl.ShowDialog();
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler
    /// for the <see cref="ToolStripMenuItem"/> <see cref="mnuHelpHowToAlea"/>
    /// Displays instructions to activate alea recording.
    /// </summary>
    /// <param name="sender">Source of the event</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void mnuHelpHowToAlea_Click(object sender, EventArgs e)
    {
      HowToActivateAlea objActivateAlea = new HowToActivateAlea();
      objActivateAlea.ShowDialog();
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler
    /// for the <see cref="ToolStripMenuItem"/> <see cref="mnuHelpHowToSMI"/>
    /// Displays instructions to activate SMI iViewX recording.
    /// </summary>
    /// <param name="sender">Source of the event</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void mnuHelpHowToSMI_Click(object sender, EventArgs e)
    {
      HowToActivateSMI objActivateSMI = new HowToActivateSMI();
      objActivateSMI.ShowDialog();
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler
    /// for the <see cref="ToolStripMenuItem"/> <see cref="mnuHelpHowToITU"/>
    /// Displays instructions to enable gaze tracking with the ITU
    /// GazeTracker and a webcam.
    /// </summary>
    /// <param name="sender">Source of the event</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void mnuHelpHowToITU_Click(object sender, EventArgs e)
    {
      HowToActivateGazetracker objActivateGazetracker = new HowToActivateGazetracker();
      objActivateGazetracker.ShowDialog();
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler
    /// for the <see cref="ToolStripMenuItem"/> <see cref="mnuHelpCheckForUpdates"/>
    /// Checks the web for updates by parsing the version.txt file
    /// from ogamas web site, comparing it with assembly version
    /// of installed file.
    /// </summary>
    /// <param name="sender">Source of the event</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void mnuHelpCheckForUpdates_Click(object sender, EventArgs e)
    {
      CheckForUpdates updateDlg = new CheckForUpdates();
      updateDlg.ShowDialog();
    }

    #endregion //mnuHelp

    #endregion //MenuEventHandlers
  }
}
