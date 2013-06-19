// <copyright file="MainForm.Module.cs" company="FU Berlin">
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

namespace Ogama.MainWindow
{
  using System;
  using System.Collections.Generic;
  using System.Text;
  using System.Windows.Forms;
  using Ogama.Modules.Common;
  using Ogama.Modules.Common.FormTemplates;
  using Ogama.Modules.Scanpath;

  using OgamaControls;

  /// <summary>
  /// The MainWindow.Module.cs contains methods referring
  /// to the module creation.
  /// </summary>
  public partial class MainForm
  {
    #region ToolStripEventHandlers

    /// <summary>
    /// The <see cref="Control.Click"/> event handler
    /// for the <see cref="ToolStripButton"/> <see cref="btnRPL"/>
    /// Creates a new replay module or activates an existing one.
    /// </summary>
    /// <param name="sender">Source of the event</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void btnRPL_Click(object sender, EventArgs e)
    {
      this.CreateOrActivateReplayModule();
    }

    /// <summary>
    /// Creates a new replay module. If there is already one open
    /// it is brought to top.
    /// </summary>
    private void CreateOrActivateReplayModule()
    {
      if (Document.ActiveDocument != null)
      {
        bool found = false;
        foreach (Form form in this.MdiChildren)
        {
          if (form is Modules.Replay.ReplayModule)
          {
            form.Select();
            found = true;
            break;
          }
        }

        if (!found)
        {
          this.CreateReplayView();
        }
      }
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler
    /// for the <see cref="ToolStripButton"/> <see cref="btnATM"/>
    /// Creates a new attention map module or activates an existing one.
    /// </summary>
    /// <param name="sender">Source of the event</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void btnATM_Click(object sender, EventArgs e)
    {
      this.CreateOrActivateAttentionMapModule();
    }

    /// <summary>
    /// Creates a new attentionmap module. If there is already one open
    /// it is brought to top.
    /// </summary>
    private void CreateOrActivateAttentionMapModule()
    {
      if (Document.ActiveDocument != null)
      {
        bool found = false;
        foreach (Form form in this.MdiChildren)
        {
          if (form is Modules.AttentionMap.AttentionMapModule)
          {
            form.Select();
            found = true;
            break;
          }
        }

        if (!found)
        {
          this.CreateAttentionMapView();
        }
      }
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler
    /// for the <see cref="ToolStripButton"/> <see cref="btnDTB"/>
    /// Creates a new database module or activates an existing one.
    /// </summary>
    /// <param name="sender">Source of the event</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void btnDTB_Click(object sender, EventArgs e)
    {
      this.CreateOrActivateDatabaseModule();
    }

    /// <summary>
    /// Creates a new database module. If there is already one open
    /// it is brought to top.
    /// </summary>
    private void CreateOrActivateDatabaseModule()
    {
      if (Document.ActiveDocument != null)
      {
        bool found = false;
        foreach (Form form in this.MdiChildren)
        {
          if (form is Modules.Database.DatabaseModule)
          {
            form.Select();
            found = true;
            break;
          }
        }

        if (!found)
        {
          this.CreateDatabaseView();
        }
      }
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler
    /// for the <see cref="ToolStripButton"/> <see cref="btnFIX"/>
    /// Creates a new fixations module or activates an existing one.
    /// </summary>
    /// <param name="sender">Source of the event</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void btnFIX_Click(object sender, EventArgs e)
    {
      this.CreateOrActivateFixationModule();
    }

    /// <summary>
    /// Creates a new fixation module. If there is already one open
    /// it is brought to top.
    /// </summary>
    private void CreateOrActivateFixationModule()
    {
      if (Document.ActiveDocument != null)
      {
        bool found = false;
        foreach (Form form in this.MdiChildren)
        {
          if (form is Modules.Fixations.FixationsModule)
          {
            form.Select();
            found = true;
            break;
          }
        }

        if (!found)
        {
          this.CreateFixationsView();
        }
      }
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler
    /// for the <see cref="ToolStripButton"/> <see cref="btnSTA"/>
    /// Creates a new statistics module or activates an existing one.
    /// </summary>
    /// <param name="sender">Source of the event</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void btnSTA_Click(object sender, EventArgs e)
    {
      this.CreateOrActivateStatistikModule();
    }

    /// <summary>
    /// Creates a new statistik module. If there is already one open
    /// it is brought to top.
    /// </summary>
    private void CreateOrActivateStatistikModule()
    {
      if (Document.ActiveDocument != null)
      {
        bool found = false;
        foreach (Form form in this.MdiChildren)
        {
          if (form is Modules.Statistics.StatisticsModule)
          {
            form.Select();
            found = true;
            break;
          }
        }

        if (!found)
        {
          this.CreateStatisticsView();
        }
      }
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler
    /// for the <see cref="ToolStripButton"/> <see cref="btnAOI"/>
    /// Creates a new areas of interest module or activates an existing one.
    /// </summary>
    /// <param name="sender">Source of the event</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void btnAOI_Click(object sender, EventArgs e)
    {
      this.CreateOrActivateAreasOfInterestModule();
    }

    /// <summary>
    /// Creates a new AOI module. If there is already one open
    /// it is brought to top.
    /// </summary>
    private void CreateOrActivateAreasOfInterestModule()
    {
      if (Document.ActiveDocument != null)
      {
        bool found = false;
        foreach (Form form in this.MdiChildren)
        {
          if (form is Modules.AOI.AOIModule)
          {
            form.Select();
            found = true;
            break;
          }
        }

        if (!found)
        {
          this.CreateAOIView();
        }
      }
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler
    /// for the <see cref="ToolStripButton"/> <see cref="btnSAL"/>
    /// Creates a new saliency module or activates an existing one.
    /// </summary>
    /// <param name="sender">Source of the event</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void btnSAL_Click(object sender, EventArgs e)
    {
      this.CreateOrActivateSaliencyModule();
    }

    /// <summary>
    /// Creates a new saliency module. If there is already one open
    /// it is brought to top.
    /// </summary>
    private void CreateOrActivateSaliencyModule()
    {
      if (Document.ActiveDocument != null)
      {
        bool found = false;
        foreach (Form form in this.MdiChildren)
        {
          if (form is Modules.Saliency.SaliencyModule)
          {
            form.Select();
            found = true;
            break;
          }
        }

        if (!found)
        {
          this.CreateSaliencyView();
        }
      }
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler
    /// for the <see cref="ToolStripButton"/> <see cref="btnSCR"/>
    /// Creates a new stimuli design module or activates an existing one.
    /// </summary>
    /// <param name="sender">Source of the event</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void btnSCR_Click(object sender, EventArgs e)
    {
      this.CreateOrActivateStimuliDesignModule();
    }

    /// <summary>
    /// Creates a new stimuli design module. If there is already one open
    /// it is brought to top.
    /// </summary>
    private void CreateOrActivateStimuliDesignModule()
    {
      if (Document.ActiveDocument != null)
      {
        bool found = false;
        foreach (Form form in this.MdiChildren)
        {
          if (form is Modules.SlideshowDesign.SlideshowModule)
          {
            form.Select();
            found = true;
            break;
          }
        }

        if (!found)
        {
          this.CreateStimuliDesignView();
        }
      }
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler
    /// for the <see cref="ToolStripButton"/> <see cref="btnREC"/>
    /// Creates a new recording module or activates an existing one.
    /// </summary>
    /// <param name="sender">Source of the event</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void btnREC_Click(object sender, EventArgs e)
    {
      this.CreateOrActivateRecordModule();
    }

    /// <summary>
    /// Creates a new record module. If there is already one open
    /// it is brought to top.
    /// </summary>
    private void CreateOrActivateRecordModule()
    {
      if (Document.ActiveDocument != null)
      {
        bool found = false;
        foreach (Form form in this.MdiChildren)
        {
          if (form is Modules.Recording.RecordModule)
          {
            form.Select();
            found = true;
          }

          // Close slideshow module, because we need
          // exclusive access to the slideshow
          if (form is Modules.SlideshowDesign.SlideshowModule)
          {
            form.Close();
          }
        }

        if (!found)
        {
          this.CreateRecordingView();
        }
      }
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler
    /// for the <see cref="ToolStripButton"/> <see cref="btnSCA"/>
    /// Creates a new scanpath module or activates an existing one.
    /// </summary>
    /// <param name="sender">Source of the event</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void btnSCA_Click(object sender, EventArgs e)
    {
      this.CreateOrActivateScanpathModule();
    }

    /// <summary>
    /// Creates a new scanpath module. If there is already one open
    /// it is brought to top.
    /// </summary>
    private void CreateOrActivateScanpathModule()
    {
      if (Document.ActiveDocument != null)
      {
        bool found = false;
        foreach (Form form in this.MdiChildren)
        {
          if (form is ScanpathsModule)
          {
            form.Select();
            found = true;
            break;
          }
        }

        if (!found)
        {
          this.CreateScanpathsView();
        }
      }
    }

    #endregion //ToolStripEventHandlers

    /// <summary>
    /// The <see cref="Form.Activated"/> event handler for all modules.
    /// Updates help context panel, when new module is activated.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void module_Activated(object sender, EventArgs e)
    {
      FormWithInterface currentForm = (FormWithInterface)sender;
      this.contextPanel.HelpTabCaption = currentForm.Text.Replace(" Module", string.Empty);
      this.contextPanel.HelpTabLogo.Image = currentForm.Logo;
      if (currentForm.HelpRTF != null)
      {
        try
        {
          this.contextPanel.HelpTabRichTextBox.LoadFile(currentForm.HelpRTF);
          return;
        }
        catch (Exception)
        {
          // If no help file found use line below.
        }
      }

      this.contextPanel.HelpTabRichTextBox.Lines = new string[] { "Currently no help or hints available" };
    }

    /// <summary>
    /// The <see cref="Form.HelpButtonClicked"/> event handler for all modules.
    /// Brings help context window to front of the context panel, when
    /// Help button is pressed.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="hlpevent">A <see cref="HelpEventArgs"/> with the event data.</param>
    private void module_HelpButtonClicked(object sender, HelpEventArgs hlpevent)
    {
      if (!this.contextPanel.Visible)
      {
        this.contextPanel.Visible = true;
      }

      this.contextPanel.SelectHelpTab();
    }

    /// <summary>
    /// The <see cref="Form.FormClosing"/> event handler for all modules.
    /// Checks for modified Document, saves it if choosen and closes it.
    /// It is raised after the <see cref="Form.Closing"/> event which is handled
    /// in each module separately.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">A <see cref="FormClosingEventArgs"/> with the event data.</param>
    private void module_FormClosing(object sender, FormClosingEventArgs e)
    {
      if (!e.Cancel)
      {
        ((Form)sender).FormClosing -= new FormClosingEventHandler(this.module_FormClosing);
        ((Form)sender).Activated -= new EventHandler(this.module_Activated);
        ((Form)sender).HelpRequested -= new HelpEventHandler(this.module_HelpButtonClicked);
      }
    }

    #region ModuleCreation

    /// <summary>
    /// Creates and displays new database module. 
    /// Sets help information rtf and binds events.
    /// </summary>
    /// <returns><strong>True</strong>, if successful,
    /// otherwise <strong>false</strong>.</returns>
    private bool CreateDatabaseView()
    {
      Modules.Database.DatabaseModule objfrmLogDataSheet =
        new Modules.Database.DatabaseModule();

      if (objfrmLogDataSheet == null)
      {
        MessageBox.Show(
          "Couldn't create view",
            Application.ProductName,
            MessageBoxButtons.OK,
            MessageBoxIcon.Exclamation);
        return false;
      }
      else
      {
        objfrmLogDataSheet.MdiParent = this;
        objfrmLogDataSheet.HelpRTF = Application.StartupPath + @"\Help\DTB.rtf";
        objfrmLogDataSheet.FormClosing += new FormClosingEventHandler(this.module_FormClosing);
        objfrmLogDataSheet.Activated += new EventHandler(this.module_Activated);
        objfrmLogDataSheet.HelpRequested += new HelpEventHandler(this.module_HelpButtonClicked);
        objfrmLogDataSheet.Show();
        return true;
      }
    }

    /// <summary>
    /// Creates and displays new Replay module. 
    /// Sets help information rtf and binds events.
    /// </summary>
    /// <returns><strong>True</strong>, if successful,
    /// otherwise <strong>false</strong>.</returns>
    private bool CreateReplayView()
    {
      Ogama.Modules.Replay.ReplayModule objfrmReplay =
        new Ogama.Modules.Replay.ReplayModule();

      if (objfrmReplay == null)
      {
        MessageBox.Show(
          "Couldn't create view",
            Application.ProductName,
            MessageBoxButtons.OK,
            MessageBoxIcon.Exclamation);
        return false;
      }
      else
      {
        objfrmReplay.MdiParent = this;
        objfrmReplay.HelpRTF = Application.StartupPath + @"\Help\RPL.rtf";
        objfrmReplay.FormClosing += new FormClosingEventHandler(this.module_FormClosing);
        objfrmReplay.Activated += new EventHandler(this.module_Activated);
        objfrmReplay.HelpRequested += new HelpEventHandler(this.module_HelpButtonClicked);
        objfrmReplay.Show();
        return true;
      }
    }

    /// <summary>
    /// Creates and displays new attention map module. 
    /// Sets help information rtf and binds events.
    /// </summary>
    /// <returns><strong>True</strong>, if successful,
    /// otherwise <strong>false</strong>.</returns>
    private bool CreateAttentionMapView()
    {
      Ogama.Modules.AttentionMap.AttentionMapModule objfrmAttentionMap =
        new Ogama.Modules.AttentionMap.AttentionMapModule();

      if (objfrmAttentionMap == null)
      {
        MessageBox.Show(
          "Couldn't create view",
            Application.ProductName,
            MessageBoxButtons.OK,
            MessageBoxIcon.Exclamation);
        return false;
      }
      else
      {
        objfrmAttentionMap.MdiParent = this;
        objfrmAttentionMap.HelpRTF = Application.StartupPath + @"\Help\ATM.rtf";
        objfrmAttentionMap.FormClosing += new FormClosingEventHandler(this.module_FormClosing);
        objfrmAttentionMap.Activated += new EventHandler(this.module_Activated);
        objfrmAttentionMap.HelpRequested += new HelpEventHandler(this.module_HelpButtonClicked);
        objfrmAttentionMap.Show();
        return true;
      }
    }

    /// <summary>
    /// Creates and displays new areas of interest module. 
    /// Sets help information rtf and binds events.
    /// </summary>
    /// <returns><strong>True</strong>, if successful,
    /// otherwise <strong>false</strong>.</returns>
    private bool CreateAOIView()
    {
      Ogama.Modules.AOI.AOIModule objfrmAOI =
        new Ogama.Modules.AOI.AOIModule();

      if (objfrmAOI == null)
      {
        MessageBox.Show(
          "Couldn't create view",
            Application.ProductName,
            MessageBoxButtons.OK,
            MessageBoxIcon.Exclamation);
        return false;
      }
      else
      {
        objfrmAOI.MdiParent = this;
        objfrmAOI.HelpRTF = Application.StartupPath + @"\Help\AOI.rtf";
        objfrmAOI.FormClosing += new FormClosingEventHandler(this.module_FormClosing);
        objfrmAOI.Activated += new EventHandler(this.module_Activated);
        objfrmAOI.HelpRequested += new HelpEventHandler(this.module_HelpButtonClicked);
        objfrmAOI.Show();
        return true;
      }
    }

    /// <summary>
    /// Creates and displays new fixations module. 
    /// Sets help information rtf and binds events.
    /// </summary>
    /// <returns><strong>True</strong>, if successful,
    /// otherwise <strong>false</strong>.</returns>
    private bool CreateFixationsView()
    {
      Ogama.Modules.Fixations.FixationsModule objfrmFixations =
        new Ogama.Modules.Fixations.FixationsModule();

      if (objfrmFixations == null)
      {
        MessageBox.Show(
          "Couldn't create view",
            Application.ProductName,
            MessageBoxButtons.OK,
            MessageBoxIcon.Exclamation);
        return false;
      }
      else
      {
        objfrmFixations.MdiParent = this;
        objfrmFixations.HelpRTF = Application.StartupPath + @"\Help\FIX.rtf";
        objfrmFixations.FormClosing += new FormClosingEventHandler(this.module_FormClosing);
        objfrmFixations.Activated += new EventHandler(this.module_Activated);
        objfrmFixations.HelpRequested += new HelpEventHandler(this.module_HelpButtonClicked);
        objfrmFixations.Show();
        return true;
      }
    }

    /// <summary>
    /// Creates and displays new statistics module. 
    /// Sets help information rtf and binds events.
    /// </summary>
    /// <returns><strong>True</strong>, if successful,
    /// otherwise <strong>false</strong>.</returns>
    private bool CreateStatisticsView()
    {
      Ogama.Modules.Statistics.StatisticsModule objfrmStatistics =
        new Ogama.Modules.Statistics.StatisticsModule();

      if (objfrmStatistics == null)
      {
        MessageBox.Show(
          "Couldn't create view",
            Application.ProductName,
            MessageBoxButtons.OK,
            MessageBoxIcon.Exclamation);
        return false;
      }
      else
      {
        objfrmStatistics.MdiParent = this;
        objfrmStatistics.HelpRTF = Application.StartupPath + @"\Help\STA.rtf";
        objfrmStatistics.FormClosing += new FormClosingEventHandler(this.module_FormClosing);
        objfrmStatistics.Activated += new EventHandler(this.module_Activated);
        objfrmStatistics.HelpRequested += new HelpEventHandler(this.module_HelpButtonClicked);
        objfrmStatistics.Show();
        return true;
      }
    }

    /// <summary>
    /// Creates and displays new saliency module. 
    /// Sets help information rtf and binds events.
    /// </summary>
    /// <returns><strong>True</strong>, if successful,
    /// otherwise <strong>false</strong>.</returns>
    private bool CreateSaliencyView()
    {
      Ogama.Modules.Saliency.SaliencyModule objfrmSaliency =
        new Ogama.Modules.Saliency.SaliencyModule();

      if (objfrmSaliency == null)
      {
        MessageBox.Show(
          "Couldn't create view",
            Application.ProductName,
            MessageBoxButtons.OK,
            MessageBoxIcon.Exclamation);
        return false;
      }
      else
      {
        objfrmSaliency.MdiParent = this;
        objfrmSaliency.HelpRTF = Application.StartupPath + @"\Help\SAL.rtf";
        objfrmSaliency.FormClosing += new FormClosingEventHandler(this.module_FormClosing);
        objfrmSaliency.Activated += new EventHandler(this.module_Activated);
        objfrmSaliency.HelpRequested += new HelpEventHandler(this.module_HelpButtonClicked);
        objfrmSaliency.Show();
        return true;
      }
    }

    /// <summary>
    /// Creates and displays new stimuli creation module. 
    /// Sets help information rtf and binds events.
    /// </summary>
    /// <returns><strong>True</strong>, if successful,
    /// otherwise <strong>false</strong>.</returns>
    private bool CreateStimuliDesignView()
    {
      Ogama.Modules.SlideshowDesign.SlideshowModule objfrmStimuli =
        new Ogama.Modules.SlideshowDesign.SlideshowModule();

      if (objfrmStimuli == null)
      {
        MessageBox.Show(
          "Couldn't create view",
            Application.ProductName,
            MessageBoxButtons.OK,
            MessageBoxIcon.Exclamation);
        return false;
      }
      else
      {
        objfrmStimuli.MdiParent = this;
        objfrmStimuli.HelpRTF = Application.StartupPath + @"\Help\SCR.rtf";
        objfrmStimuli.FormClosing += new FormClosingEventHandler(this.module_FormClosing);
        objfrmStimuli.Activated += new EventHandler(this.module_Activated);
        objfrmStimuli.HelpRequested += new HelpEventHandler(this.module_HelpButtonClicked);
        objfrmStimuli.Show();
        return true;
      }
    }

    /// <summary>
    /// Creates and displays new recording module. 
    /// Sets help information rtf and binds events.
    /// </summary>
    /// <returns><strong>True</strong>, if successful,
    /// otherwise <strong>false</strong>.</returns>
    private bool CreateRecordingView()
    {
      Ogama.Modules.Recording.RecordModule objfrmRecord =
        new Ogama.Modules.Recording.RecordModule();

      if (objfrmRecord == null)
      {
        MessageBox.Show(
          "Couldn't create view",
            Application.ProductName,
            MessageBoxButtons.OK,
            MessageBoxIcon.Exclamation);
        return false;
      }
      else
      {
        objfrmRecord.MdiParent = this;
        objfrmRecord.HelpRTF = Application.StartupPath + @"\Help\REC.rtf";
        objfrmRecord.FormClosing += new FormClosingEventHandler(this.module_FormClosing);
        objfrmRecord.Activated += new EventHandler(this.module_Activated);
        objfrmRecord.HelpRequested += new HelpEventHandler(this.module_HelpButtonClicked);
        objfrmRecord.RecordingFinished += new EventHandler(this.objfrmRecord_RecordingFinished);
        objfrmRecord.Show();
        return true;
      }
    }

    /// <summary>
    /// Creates and displays new scanpaths module. 
    /// Sets help information rtf and binds events.
    /// </summary>
    /// <returns><strong>True</strong>, if successful,
    /// otherwise <strong>false</strong>.</returns>
    private bool CreateScanpathsView()
    {
      ScanpathsModule objfrmScanpaths =
        new ScanpathsModule();

      if (objfrmScanpaths == null)
      {
        MessageBox.Show(
          "Couldn't create view",
            Application.ProductName,
            MessageBoxButtons.OK,
            MessageBoxIcon.Exclamation);
        return false;
      }
      else
      {
        objfrmScanpaths.MdiParent = this;
        objfrmScanpaths.HelpRTF = Application.StartupPath + @"\Help\SCA.rtf";
        objfrmScanpaths.FormClosing += new FormClosingEventHandler(this.module_FormClosing);
        objfrmScanpaths.Activated += new EventHandler(this.module_Activated);
        objfrmScanpaths.HelpRequested += new HelpEventHandler(this.module_HelpButtonClicked);
        objfrmScanpaths.Show();
        return true;
      }
    }

    #endregion //ModuleCreation
  }
}
