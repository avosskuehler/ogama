// <copyright file="SlideshowModule.Buttons.cs" company="FU Berlin">
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

namespace Ogama.Modules.SlideshowDesign
{
  using System;
  using System.Collections.Generic;
  using System.IO;
  using System.Windows.Forms;
  using System.Xml.Serialization;

  using Ogama.ExceptionHandling;
  using Ogama.MainWindow;
  using Ogama.Modules.Common.SlideCollections;
  using Ogama.Modules.Recording;
  using Ogama.Modules.Recording.Presenter;
  using Ogama.Modules.SlideshowDesign.DesignModule;
  using Ogama.Modules.SlideshowDesign.DesignModule.StimuliDialogs;
  using Ogama.Modules.SlideshowDesign.Import;
  using Ogama.Modules.SlideshowDesign.Shuffling;

  using OgamaControls;
  using VectorGraphics.Elements.ElementCollections;

  /// <summary>
  /// The SlideshowModule.Buttons.cs contains methods referring
  /// to the <see cref="ToolStrip"/> buttons at the top of the module.
  /// </summary>
  public partial class SlideshowModule
  {
    /// <summary>
    /// This method updates the experiments document with the designed slideshow
    /// of this module, but does not itself writes the slideshow to file.
    /// This is done via MainModule File-Save.
    /// </summary>
    public void SaveSlideshowToDocument()
    {
      this.Cursor = Cursors.WaitCursor;

      if (!this.MdiParent.IsDisposed)
      {
        Document.ActiveDocument.ExperimentSettings.SlideShow = this.slideshow;

        ((MainForm)this.MdiParent).StatusLabel.Text = "Refreshing context panel ...";
        ((MainForm)this.MdiParent).RefreshContextPanelImageTabs();
        ((MainForm)this.MdiParent).StatusLabel.Text = "Ready ...";
        ((MainForm)this.MdiParent).StatusProgressbar.Value = 0;

        Document.ActiveDocument.Modified = true;
        Document.ActiveDocument.ExperimentSettings.SlideShow.IsModified = false;
      }

      this.Cursor = Cursors.Default;
    }

    ///////////////////////////////////////////////////////////////////////////////
    // Eventhandler for UI, Menu, Buttons, Toolbars etc.                         //
    ///////////////////////////////////////////////////////////////////////////////
    #region WINDOWSEVENTHANDLER

    /// <summary>
    /// The <see cref="Control.Click"/> event handler for the
    /// <see cref="Button"/> <see cref="btnImport"/>.
    /// Raises the method to open a slideshow from a file.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void btnImport_Click(object sender, EventArgs e)
    {
      if (this.ofdSlideshow.ShowDialog() == DialogResult.OK)
      {
        switch (this.OpenSlideshowFromFile(this.ofdSlideshow.FileName))
        {
          case DialogResult.Abort:
            ExceptionMethods.ProcessErrorMessage("Could not open the slideshow.");
            break;
          case DialogResult.Cancel:
            ExceptionMethods.ProcessMessage("Cancelled", "The import of the slideshow was cancelled.");
            break;
          case DialogResult.OK:
            ExceptionMethods.ProcessMessage("Finished", "The import of the slideshow was successfull.");
            break;
        }
      }
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler for the
    /// <see cref="Button"/> <see cref="btnExport"/>.
    /// Raises the method to save the slides into a slideshow file.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void btnExport_Click(object sender, EventArgs e)
    {
      if (this.sfdSlideshow.ShowDialog() == DialogResult.OK)
      {
        switch (this.SaveSlideshowToFile(this.sfdSlideshow.FileName))
        {
          case DialogResult.Abort:
            ExceptionMethods.ProcessErrorMessage("Could not export the slideshow.");
            break;
          case DialogResult.Cancel:
            ExceptionMethods.ProcessMessage("Cancelled", "The export of the slideshow was cancelled.");
            break;
          case DialogResult.OK:
            ExceptionMethods.ProcessMessage("Finished", "The export of the slideshow was successfull.");
            break;
        }
      }
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler for the
    /// <see cref="Button"/> <see cref="btnSaveSlideshow"/>.
    /// Raises the method to save the slides to the experiment settings.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void btnSaveSlideshow_Click(object sender, EventArgs e)
    {
      this.SaveSlideshowToDocument();
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler for the
    /// <see cref="Button"/> <see cref="btnInstruction"/>.
    /// Raises the <see cref="OpenStimulusDesignerForm(SlideDesignModule,string)"/>
    /// form with adapted properties for instructional stimuli.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void btnInstruction_Click(object sender, EventArgs e)
    {
      SlideDesignModule newInstruction = new SlideDesignModule(StimuliTypes.Instruction);
      newInstruction.Text = "Create new instruction slide ...";
      newInstruction.Icon = Properties.Resources.Instruction;
      newInstruction.Description = "Instruction stimuli can be used to present a message or a multiple choice question to the subject.";
      newInstruction.SlideName = this.GetUnusedSlideName();
      this.OpenStimulusDesignerForm(newInstruction, string.Empty);
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler for the
    /// <see cref="Button"/> <see cref="btnDesktop"/>.
    /// Raises the <see cref="OpenStimulusDesignerForm(SlideDesignModule,string)"/>
    /// form with adapted properties for desktop stimuli.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void btnDesktop_Click(object sender, EventArgs e)
    {
      var newDesktop = new DesktopDialog { SlideName = this.GetUnusedSlideName() };
      this.OpenDesktopDesignerForm(newDesktop, string.Empty);
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler for the
    /// <see cref="Button"/> <see cref="btnRtfInstruction"/>.
    /// Raises the <see cref="OpenStimulusDesignerForm(SlideDesignModule,string)"/>
    /// form with adapted properties for instructional RTF formatted stimuli.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void btnRtfInstruction_Click(object sender, EventArgs e)
    {
      SlideDesignModule newInstruction = new SlideDesignModule(StimuliTypes.RTFInstruction);
      newInstruction.Text = "Create new rich text formatted instruction slide ...";
      newInstruction.Icon = Properties.Resources.Instruction;
      newInstruction.Description = "Rich text instruction stimuli can be used to present a message with different fonts and colors.";
      newInstruction.SlideName = this.GetUnusedSlideName();
      this.OpenStimulusDesignerForm(newInstruction, string.Empty);
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler for the
    /// <see cref="Button"/> <see cref="btnImage"/>.
    /// Raises the <see cref="OpenStimulusDesignerForm(SlideDesignModule,string)"/>
    /// form with adapted properties for image stimuli.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void btnImage_Click(object sender, EventArgs e)
    {
      SlideDesignModule newImages = new SlideDesignModule(StimuliTypes.Image);
      newImages.Text = "Create new images slide ...";
      newImages.Icon = Properties.Resources.Images;
      newImages.Description = "Image stimuli can be used to present one or more images to the subject.";
      newImages.SlideName = this.GetUnusedSlideName();
      this.OpenStimulusDesignerForm(newImages, string.Empty);
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler for the
    /// <see cref="Button"/> <see cref="btnShapes"/>.
    /// Raises the <see cref="OpenStimulusDesignerForm(SlideDesignModule,string)"/>
    /// form with adapted properties for shape stimuli.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void btnShapes_Click(object sender, EventArgs e)
    {
      SlideDesignModule newShapes = new SlideDesignModule(StimuliTypes.Shape);
      newShapes.Text = "Create new shapes slide ...";
      newShapes.Icon = Properties.Resources.Shape;
      newShapes.Description = "Shape stimuli can be used to present different shapes to the subject.";
      newShapes.SlideName = this.GetUnusedSlideName();
      this.OpenStimulusDesignerForm(newShapes, string.Empty);
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler for the
    /// <see cref="Button"/> <see cref="btnMixed"/>.
    /// Raises the <see cref="OpenStimulusDesignerForm(SlideDesignModule,string)"/>
    /// form with adapted properties for mixed stimuli.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void btnMixed_Click(object sender, EventArgs e)
    {
      SlideDesignModule newMixed = new SlideDesignModule(StimuliTypes.None);
      newMixed.Text = "Create new mixed stimuli slide ...";
      newMixed.Icon = Properties.Resources.Images;
      newMixed.Description = "Mixed text, image and vector graphic stimuli can be used to " +
        "present images along with a caption or multiple choice questions with images as answers " +
        "and a rectangle around the question.";
      newMixed.SlideName = this.GetUnusedSlideName();
      this.OpenStimulusDesignerForm(newMixed, string.Empty);
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler for the
    /// <see cref="Button"/> <see cref="btnFlash"/>.
    /// Raises the <see cref="OpenStimulusDesignerForm(SlideDesignModule,string)"/>
    /// form with adapted properties for flash stimuli.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void btnFlash_Click(object sender, EventArgs e)
    {
      SlideDesignModule newFlash = new SlideDesignModule(StimuliTypes.Flash);
      newFlash.Text = "Create new flash movie slide ...";
      newFlash.Icon = Properties.Resources.FlashPlayerIcon;
      newFlash.Description = "Flash movie stimuli are used to present a shockwave flash object (.swf - file).";
      newFlash.SlideName = this.GetUnusedSlideName();
      this.OpenStimulusDesignerForm(newFlash, string.Empty);
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler for the
    /// <see cref="Button"/> <see cref="btnInternet"/>.
    /// Raises the <see cref="OpenStimulusDesignerForm(SlideDesignModule,string)"/>
    /// form with adapted properties for browser stimuli.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void btnInternet_Click(object sender, EventArgs e)
    {
      this.OpenBrowserDesignerForm(null);
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler for the
    /// <see cref="Button"/> <see cref="btnBlank"/>.
    /// Raises the <see cref="OpenStimulusDesignerForm(SlideDesignModule,string)"/>
    /// form with adapted properties for blank slides.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void btnBlank_Click(object sender, EventArgs e)
    {
      SlideDesignModule newBlank = new SlideDesignModule(StimuliTypes.Blank);
      newBlank.Text = "Create new blank slide ...";
      newBlank.Icon = Properties.Resources.Blank;
      newBlank.Description = "Blank slides can be used to fill in a pause or just simple colored slides.";
      newBlank.SlideName = this.GetUnusedSlideName();
      this.OpenStimulusDesignerForm(newBlank, string.Empty);
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler for the
    /// <see cref="Button"/> <see cref="btnAddFolder"/>.
    /// Inserts an empty folder for slides in the treeview.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void btnAddFolder_Click(object sender, EventArgs e)
    {
      SlideshowTreeNode folderNode = new SlideshowTreeNode("SlideFolder");
      folderNode.ImageKey = "Folder";
      folderNode.Name = this.slideshow.GetUnusedNodeID();

      // Get root node for insertion
      SlideshowTreeNode firstNode = this.trvSlideshow.Nodes[0] as SlideshowTreeNode;

      // If there is a selected node use this instead
      NodesCollection selectedNodes = this.trvSlideshow.SelectedNodes;
      if (selectedNodes.Count > 0)
      {
        firstNode = selectedNodes[0] as SlideshowTreeNode;
        this.trvSlideshow.SelectedNodes.Clear();
      }

      // Add to node, if it is a slide node add to parent instead
      if (firstNode.Slide != null)
      {
        firstNode.Parent.Nodes.Add(folderNode);
      }
      else
      {
        firstNode.Nodes.Add(folderNode);
      }

      // Select added node.
      this.trvSlideshow.SelectedNodes.Add(folderNode);

      this.UpdateListView(this.trvSlideshow.SelectedNodes);
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler for the
    /// <see cref="Button"/> <see cref="btnPreviewSlideshow"/>.
    /// Starts a <see cref="PresenterModule"/> with the given slides.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void btnPreviewSlideshow_Click(object sender, EventArgs e)
    {
      if (!RecordModule.CheckForCorrectPresentationScreenResolution())
      {
        return;
      }

      PresenterModule objPresenter = new PresenterModule();

      // Create a newly randomized trial list.
      TrialCollection trials = this.slideshow.GetRandomizedTrials();

      // Create a hardcopy of the trials.
      TrialCollection copyOfTrials = (TrialCollection)trials.Clone();

      // Set slide list of presenter
      objPresenter.TrialList = copyOfTrials;

      // Show presenter form, that starts presentation.
      objPresenter.ShowDialog();
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler for the
    /// <see cref="Button"/> <see cref="btnIndexDown"/>.
    /// Increases the index of all selected nodes if possible.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void btnIndexDown_Click(object sender, EventArgs e)
    {
      NodesCollection selectedNodes = this.trvSlideshow.SelectedNodes;
      if (selectedNodes.Count != 1)
      {
        return;
      }

      TreeNode node = selectedNodes[0];

      // Skip if the node is the root node
      if (node.Parent == null)
      {
        return;
      }

      int currentIndex = node.Index;

      // Skip if item is already at last index
      if (currentIndex == node.Parent.Nodes.Count - 1)
      {
        return;
      }

      TreeNode parent = node.Parent;
      node.Remove();
      parent.Nodes.Insert(currentIndex + 1, node);

      this.UpdateListView(this.trvSlideshow.SelectedNodes);
      this.SlideShowModified();
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler for the
    /// <see cref="Button"/> <see cref="btnIndexUp"/>.
    /// Decreases the index of all selected nodes if possible.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void btnIndexUp_Click(object sender, EventArgs e)
    {
      NodesCollection selectedNodes = this.trvSlideshow.SelectedNodes;
      if (selectedNodes.Count != 1)
      {
        return;
      }

      TreeNode node = selectedNodes[0];

      // Skip if the node is the root node
      if (node.Parent == null)
      {
        return;
      }

      int currentIndex = node.Index;

      // Skip if item is already at first index
      if (currentIndex == 0)
      {
        return;
      }

      TreeNode parent = node.Parent;
      node.Remove();
      parent.Nodes.Insert(currentIndex - 1, node);

      this.UpdateListView(this.trvSlideshow.SelectedNodes);
      this.SlideShowModified();
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler for the
    /// <see cref="Button"/> <see cref="btnLevelUp"/>.
    /// Move the selected nodes one level up if possible.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void btnLevelUp_Click(object sender, EventArgs e)
    {
      NodesCollection nodes = this.trvSlideshow.SelectedNodes;
      this.MoveNodesLevelUp(nodes, false);
      this.lsvDetails.Invalidate();
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler for the
    /// <see cref="Button"/> <see cref="btnLevelDown"/>.
    /// Move the selected nodes one level down if possible.
    /// Create a new parent node to add the selection, if there
    /// is no child node to move the selected nodes to.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void btnLevelDown_Click(object sender, EventArgs e)
    {
      NodesCollection nodes = this.trvSlideshow.SelectedNodes;
      nodes.Sort();
      if (nodes.Count > 0)
      {
        TreeNode firstNode = nodes[0];

        // Skip if the node is the root node or at level 1
        if (firstNode.Parent == null || firstNode.Parent.Parent == null)
        {
          return;
        }

        int insertionIndex = firstNode.Index;

        TreeNode newParent = firstNode.Parent.Parent;
        for (int i = nodes.Count - 1; i >= 0; i--)
        {
          TreeNode collectionNode = nodes[i];
          collectionNode.Remove();
          newParent.Nodes.Insert(insertionIndex, collectionNode);
        }
      }

      this.UpdateListView(this.trvSlideshow.SelectedNodes);
      this.SlideShowModified();
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler for the
    /// <see cref="Button"/> <see cref="btnCustomShuffling"/>.
    /// Opens a <see cref="CustomShuffleDialog"/> to enable
    /// and define a <see cref="CustomShuffling"/> for
    /// the slideshow.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void btnCustomShuffling_Click(object sender, EventArgs e)
    {
      CustomShuffleDialog shuffleDlg = new CustomShuffleDialog();
      shuffleDlg.Slideshow = this.slideshow;
      shuffleDlg.Shuffling = this.slideshow.Shuffling;
      if (shuffleDlg.ShowDialog() == DialogResult.OK)
      {
        this.slideshow.Shuffling = shuffleDlg.Shuffling;
      }
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler for the
    /// <see cref="Button"/> <see cref="btnImportFolderContent"/>.
    /// Opens a <see cref="FolderContentSlideImportDialog"/> to batch import
    /// a list of stimuli in a folder as slides into the current slideshow.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void btnImportFolderContent_Click(object sender, EventArgs e)
    {
      FolderContentSlideImportDialog importDlg = new FolderContentSlideImportDialog();
      if (importDlg.ShowDialog() == DialogResult.OK)
      {
        List<Slide> newSlides = importDlg.Slides;
        foreach (Slide slide in newSlides)
        {
          this.AddSlide(slide);
        }
      }
    }

    #endregion //WINDOWSEVENTHANDLER

    ///////////////////////////////////////////////////////////////////////////////
    // Methods for doing main class job                                          //
    ///////////////////////////////////////////////////////////////////////////////
    #region PRIVATEMETHODS

    /// <summary>
    /// Saves current list of slides into the experiment settings xml file.
    /// Also calculates slide bitmaps for all modified slides and copies
    /// them into the slides folders.
    /// </summary>
    /// <param name="silent"><strong>True</strong>, if it should be saved
    /// without a question.</param>
    private void SaveToExperimentSettings(bool silent)
    {
      if (Document.ActiveDocument != null && Document.ActiveDocument.ExperimentSettings != null)
      {
        if (Document.ActiveDocument.ExperimentSettings.SlideShow.IsModified)
        {
          bool doIt = true;
          if (!silent)
          {
            doIt = MessageBox.Show(
              "The slideshow has changed, would you like to store it to the experiments file ?",
              Application.ProductName,
              MessageBoxButtons.YesNo,
              MessageBoxIcon.Question) == DialogResult.Yes;
          }

          if (doIt)
          {
            this.Cursor = Cursors.WaitCursor;
            Document.ActiveDocument.ExperimentSettings.SlideShow = this.slideshow;
            ((MainForm)this.MdiParent).StatusLabel.Text = "Saving slideshow to file ...";
            if (!Document.ActiveDocument.SaveSettingsToFile(Document.ActiveDocument.ExperimentSettings.DocumentFilename))
            {
              ExceptionMethods.ProcessErrorMessage("Couldn't save slideshow to experiment settings.");
            }
          }
        }
      }
    }

    /// <summary>
    /// Saves the current slides collection to a OGAMA slideshow file.
    /// </summary>
    /// <param name="filePath">Path to slideshow file.</param>
    /// <returns><strong>True</strong> if successful, 
    /// otherwise <strong>false</strong>.</returns>
    private DialogResult SaveSlideshowToFile(string filePath)
    {
      try
      {
        if (this.trvSlideshow.Nodes[0].Nodes.Count == 0)
        {
          return DialogResult.Cancel;
        }

        using (TextWriter writer = new StreamWriter(filePath))
        {
          string message = "Would you like to export the whole slideshow ?" +
            Environment.NewLine + "Otherwise only the selected tree node will be exported";
          DialogResult result = InformationDialog.Show("Save whole slideshow ?", message, true, MessageBoxIcon.Question);

          switch (result)
          {
            case DialogResult.Cancel:
              return DialogResult.Cancel;
            case DialogResult.No:
              // Create an instance of the XmlSerializer class;
              // specify the type of object to serialize 
              XmlSerializer serializer = new XmlSerializer(typeof(SlideshowTreeNode));
              SlideshowTreeNode currentNode = this.slideshow;
              if (this.trvSlideshow.SelectedNodes.Count > 0)
              {
                currentNode = (SlideshowTreeNode)this.trvSlideshow.SelectedNodes[0];
              }

              // Serialize the Nodes
              serializer.Serialize(writer, currentNode);
              break;
            case DialogResult.Yes:
              // Create an instance of the XmlSerializer class;
              // specify the type of object to serialize 
              serializer = new XmlSerializer(typeof(Slideshow));
              Slideshow currentSlideshow = this.slideshow;

              // Serialize the Nodes
              serializer.Serialize(writer, currentSlideshow);
              return DialogResult.OK;
          }
        }
      }
      catch (Exception ex)
      {
        ExceptionMethods.HandleException(ex);

        return DialogResult.Abort;
      }

      return DialogResult.OK;
    }

    /// <summary>
    /// Reads an OGAMA slideshow file.
    /// </summary>
    /// <param name="filePath">Path to slideshow file.</param>
    /// <returns><strong>True</strong> if successful, 
    /// otherwise <strong>false</strong>.</returns>
    private DialogResult OpenSlideshowFromFile(string filePath)
    {
      try
      {
        Slideshow newSlideshow = null;

        using (FileStream fs = new FileStream(filePath, FileMode.Open))
        {
          // Create an instance of the XmlSerializer class;
          // specify the type of object to be deserialized 
          XmlSerializer serializer = new XmlSerializer(typeof(Slideshow));

          /* Use the Deserialize method to restore the object's state with
          data from the XML document. */
          newSlideshow = (Slideshow)serializer.Deserialize(fs);
          newSlideshow.SetModifiedToAllSlides();
        }

        if (this.trvSlideshow.Nodes[0].Nodes.Count > 0)
        {
          string message = "Do you want to delete the existing slideshow items" +
            "before importing ?" + Environment.NewLine +
            "Otherwise the imported slides will be inserted at the current selected node.";
          DialogResult result =
            InformationDialog.Show("Delete existing slideshow ?", message, true, MessageBoxIcon.Information);
          switch (result)
          {
            case DialogResult.Cancel:
              return DialogResult.Cancel;
            case DialogResult.No:
              newSlideshow.SetNewNodeIDs(this.slideshow.GetUnusedNodeID());
              if (this.trvSlideshow.SelectedNodes.Count > 0)
              {
                this.trvSlideshow.SelectedNodes[0].Nodes.Add(newSlideshow);
              }
              else
              {
                this.trvSlideshow.Nodes[0].Nodes.Add(newSlideshow);
              }

              break;
            case DialogResult.Yes:
              this.trvSlideshow.Nodes[0].Nodes.Clear();
              this.lsvDetails.ClearObjects();
              this.slideshow = newSlideshow;
              this.PopulateTreeView(this.slideshow);
              break;
          }
        }
        else
        {
          this.slideshow = newSlideshow;
          this.PopulateTreeView(this.slideshow);
        }

        this.SlideShowModified();
        return DialogResult.OK;
      }
      catch (Exception ex)
      {
        ExceptionMethods.HandleException(ex);

        return DialogResult.Abort;
      }
    }

    /// <summary>
    /// This method moves the given nodes on level up.
    /// They can be marked as a trial using the second parameter.
    /// </summary>
    /// <param name="nodes">A <see cref="NodesCollection"/> with the <see cref="TreeView"/> nodes.</param>
    /// <param name="markAsTrial"><strong>True</strong> if the nodes in the collection
    /// are referred to be one trial in the presentation.</param>
    private void MoveNodesLevelUp(NodesCollection nodes, bool markAsTrial)
    {
      nodes.Sort();
      if (nodes.Count > 0)
      {
        TreeNode firstNode = nodes[0];

        // Skip if the node is the root node
        if (firstNode.Parent == null)
        {
          return;
        }

        int insertionIndex = firstNode.Index;

        string parentNodeName = this.slideshow.GetUnusedNodeID();
        SlideshowTreeNode newParent = new SlideshowTreeNode(parentNodeName);
        newParent.Name = parentNodeName;
        if (markAsTrial)
        {
          newParent.Tag = "Trial";
        }

        newParent.SetTreeNodeImageKey(newParent);

        firstNode.Parent.Nodes.Insert(insertionIndex, newParent);
        foreach (TreeNode collectionNode in nodes)
        {
          collectionNode.Remove();
          newParent.Nodes.Add(collectionNode);
        }
      }

      this.UpdateListView(this.trvSlideshow.SelectedNodes);
      this.SlideShowModified();
    }

    #endregion //PRIVATEMETHODS
  }
}
