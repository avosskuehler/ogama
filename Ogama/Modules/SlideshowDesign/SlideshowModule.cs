// <copyright file="SlideshowModule.cs" company="FU Berlin">
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

namespace Ogama.Modules.SlideshowDesign
{
  using System;
  using System.Collections;
  using System.Collections.Generic;
  using System.Collections.Specialized;
  using System.ComponentModel;
  using System.Data;
  using System.Drawing;
  using System.Drawing.Drawing2D;
  using System.IO;
  using System.Text;
  using System.Threading;
  using System.Windows.Forms;
  using System.Xml;
  using System.Xml.Serialization;

  using Ogama.ExceptionHandling;
  using Ogama.MainWindow;
  using Ogama.Modules.Common;
  using Ogama.Modules.Common.FormTemplates;
  using Ogama.Modules.Common.SlideCollections;
  using Ogama.Modules.Common.Types;
  using Ogama.Modules.Recording;
  using Ogama.Modules.SlideshowDesign.DesignModule;
  using Ogama.Modules.SlideshowDesign.DesignModule.StimuliDialogs;
  using Ogama.Properties;
  using OgamaControls;
  using VectorGraphics;
  using VectorGraphics.Elements;
  using VectorGraphics.Elements.ElementCollections;

  /// <summary>
  /// Derived from <see cref="FormWithPicture"/>.
  /// This <see cref="Form"/> hosts the slideshow module.
  /// This is used for the design of a slideshow presentation.
  /// </summary>
  /// <remarks>This interface is intended to supply tools for
  /// creating different slide shows that are suitable for OGAMA.
  /// Different types of slides are available.
  /// Instructional, pictures, graphical elements, flash stimuli and blank slides.</remarks>
  public partial class SlideshowModule : FormWithPicture
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
    /// Saves the <see cref="Slideshow"/> that is designed in this interface.
    /// </summary>
    private Slideshow slideshow;

    /// <summary>
    /// A <see cref="ToolTip"/> to show the stop condition.
    /// </summary>
    private ToolTip toolTip;

    /// <summary>
    /// Saves the last selected node.
    /// </summary>
    private TreeNode selectedNode;

    #endregion //FIELDS

    ///////////////////////////////////////////////////////////////////////////////
    // Construction and Initializing methods                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region CONSTRUCTION

    /// <summary>
    /// Initializes a new instance of the SlideshowModule class.
    /// </summary>
    public SlideshowModule()
    {
      this.InitializeComponent();
      this.Picture = this.slidePreviewPicture;
      this.InitializeCustomElements();
    }

    #endregion //CONSTRUCTION

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Properties                                                       //
    ///////////////////////////////////////////////////////////////////////////////
    #region PROPERTIES

    /// <summary>
    /// Gets the sorted list of <see cref="Slide"/>s in this form.
    /// </summary>
    public NameObjectList<Slide> Slides
    {
      get { return this.slideshow.Slides; }
    }

    /// <summary>
    /// Gets or sets the current selected TreeViewNode, can be selected in the TreeView
    /// or the ListView.
    /// </summary>
    public TreeNode SelectedNode
    {
      get
      {
        return this.selectedNode;
      }

      set
      {
        this.selectedNode = value;
        this.prgSlides.SelectedObject = value;

        if (value != null)
        {
          SlideshowTreeNode node = value as SlideshowTreeNode;
          if (node.Slide != null)
          {
            this.LoadSlide(node.Slide, ActiveXMode.BehindPicture);
          }
        }
      }
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

    /// <summary>
    /// Initialize custom elements
    /// </summary>
    protected override void InitializeCustomElements()
    {
      base.InitializeCustomElements();
      this.slideshow = new Slideshow();
      this.toolTip = new ToolTip();
      this.toolTip.ShowAlways = true;

      // Hide PropertyGrid
      this.spcPropertiesPreview.Panel1Collapsed = true;

      this.btnHelp.Click += new EventHandler(this.btnHelp_Click);
      this.pnlCanvas.Resize += new EventHandler(this.pnlCanvas_Resize);
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
    /// The <see cref="Form.Load"/> event handler.
    /// Initializes this form with the slides from the experiment settings.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void frmStimuli_Load(object sender, EventArgs e)
    {
      this.slideshow = Document.ActiveDocument.ExperimentSettings.SlideShow;

      // Populate the tree view with the current slideshow.
      this.PopulateTreeView(this.slideshow);

      // Set detail views initial formatting.
      this.InitializeDetailListView();

      // Intialize slide preview picture
      this.slidePreviewPicture.OwningForm = this;
      this.slidePreviewPicture.PresentationSize = Document.ActiveDocument.PresentationSize;
      this.ResizeCanvas();

      // Select root node
      this.trvSlideshow.SelectedNodes.Clear();
      this.trvSlideshow.SelectedNodes.Add(this.trvSlideshow.Nodes[0]);
    }

    /// <summary>
    /// The <see cref="Form.FormClosing"/> event handler.
    /// Saves the slides to the experiment settings.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An <see cref="FormClosingEventArgs"/> with the event arguments.</param>
    private void frmStimuli_FormClosing(object sender, FormClosingEventArgs e)
    {
      // Remove the slideshow from the TreeView.
      this.slideshow.Remove();

      this.SaveToExperimentSettings(false);
    }

    /// <summary>
    /// The <see cref="PropertyGrid.PropertyValueChanged"/> for the
    /// <see cref="PropertyGrid"/> <see cref="prgSlides"/>.
    /// Sets modified flag.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An <see cref="PropertyValueChangedEventArgs"/> with the event arguments.</param>
    private void prgSlides_PropertyValueChanged(object sender, PropertyValueChangedEventArgs e)
    {
      this.SlideShowModified();
    }

    /// <summary>
    /// The <see cref="Control.DoubleClick"/> event handler.
    /// Opens the clicked slide.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void slidePreviewPicture_DoubleClick(object sender, EventArgs e)
    {
      if (this.selectedNode != null)
      {
        this.OpenSlideDesignForm((SlideshowTreeNode)this.selectedNode, ((SlideshowTreeNode)this.selectedNode).Slide);
      }
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
    #region METHODS

    /// <summary>
    /// This method opens the given slide in a new <see cref="SlideDesignModule"/> form
    /// for modification.
    /// </summary>
    /// <param name="treeNode">The <see cref="SlideshowTreeNode"/> that indicates the slide.</param>
    /// <param name="currentSlide">The <see cref="Slide"/> to be edited.</param>
    private void OpenSlideDesignForm(SlideshowTreeNode treeNode, Slide currentSlide)
    {
      SlideDesignModule newStimulusDesignForm = new SlideDesignModule(StimuliTypes.None);
      newStimulusDesignForm.Slide = (Slide)currentSlide.Clone();
      this.OpenStimulusDesignerForm(newStimulusDesignForm, treeNode.Name);
    }

    /// <summary>
    /// This method opens the given slide in a new <see cref="SlideDesignModule"/> form
    /// for modification.
    /// </summary>
    /// <param name="treeNode">The <see cref="SlideshowTreeNode"/> that indicates the slide.</param>
    /// <param name="currentSlide">The <see cref="Slide"/> to be edited.</param>
    private void OpenDesktopDesignForm(SlideshowTreeNode treeNode, Slide currentSlide)
    {
      DesktopDialog newDesktopDesignForm = new DesktopDialog();
      newDesktopDesignForm.Slide = (Slide)currentSlide.Clone();
      this.OpenDesktopDesignerForm(newDesktopDesignForm, treeNode.Name);
    }

    /// <summary>
    /// This method renames all slides with the given name.
    /// </summary>
    /// <param name="oldName">A <see cref="String"/> to be renamed.</param>
    /// <param name="newName">The new <see cref="String"/> name.</param>
    private void RenameSlide(string oldName, string newName)
    {
      // Update TreeView.
      TreeNode slideNode = this.GetTreeNodeByName(oldName);
      slideNode.Text = newName;

      this.UpdateListView(this.trvSlideshow.SelectedNodes);
    }

    /// <summary>
    /// This method adds the given <see cref="Slide"/> at the
    /// current treeview position.
    /// </summary>
    /// <param name="newSlide">The new <see cref="Slide"/> to be added to the slideshow.</param>
    private void AddSlide(Slide newSlide)
    {
      // Add node
      SlideshowTreeNode slideNode = new SlideshowTreeNode(newSlide.Name);
      slideNode.Name = this.slideshow.GetUnusedNodeID();
      slideNode.Slide = newSlide;
      ((SlideshowTreeNode)slideNode).SetTreeNodeImageKey((SlideshowTreeNode)slideNode);

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
        firstNode.Parent.Nodes.Add(slideNode);
      }
      else
      {
        firstNode.Nodes.Add(slideNode);
      }

      // Select added node.
      this.trvSlideshow.SelectedNodes.Add(slideNode);

      this.UpdateListView(this.trvSlideshow.SelectedNodes);
    }

    /// <summary>
    /// This method adds the given <see cref="BrowserTreeNode"/> at the
    /// current treeview position.
    /// </summary>
    /// <param name="newBrowserSlideNode">The new <see cref="BrowserTreeNode"/> to be added to the slideshow.</param>
    private void AddBrowserSlide(BrowserTreeNode newBrowserSlideNode)
    {
      // Add node
      string newID = this.slideshow.GetUnusedNodeID();
      newBrowserSlideNode.Name = newID;
      newBrowserSlideNode.UrlToID.Add(newBrowserSlideNode.OriginURL, Convert.ToInt32(newID));

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
        firstNode.Parent.Nodes.Add(newBrowserSlideNode);
      }
      else
      {
        firstNode.Nodes.Add(newBrowserSlideNode);
      }

      // Select added node.
      this.trvSlideshow.SelectedNodes.Add(newBrowserSlideNode);

      this.UpdateListView(this.trvSlideshow.SelectedNodes);
    }

    /// <summary>
    /// This method replaces the Slide at the node with the given nodeID
    /// with the new given <see cref="Slide"/>.
    /// </summary>
    /// <param name="newSlide">The new <see cref="Slide"/> that replaces the old one.</param>
    /// <param name="nodeID">A <see cref="String"/> with the node ID of the <see cref="TreeView"/>
    /// node to be replaced.</param>
    private void OverwriteSlide(Slide newSlide, string nodeID)
    {
      // Update node
      TreeNode slideNode = this.GetTreeNodeByID(nodeID);

      if (slideNode != null)
      {
        slideNode.Text = newSlide.Name;
        ((SlideshowTreeNode)slideNode).Slide = newSlide;
        ((SlideshowTreeNode)slideNode).SetTreeNodeImageKey((SlideshowTreeNode)slideNode);
        this.UpdateListView(this.selectedNode);
        this.lsvDetails.SelectObject(slideNode);
      }
      else
      {
        ExceptionMethods.ProcessErrorMessage("No suitable slide found for overriding, so adding it");
        this.AddSlide(newSlide);
      }
    }

    /// <summary>
    /// Opens a <see cref="SlideDesignModule"/> form, waits for succesful
    /// design and updates slideshow with the designed <see cref="Slide"/>.
    /// </summary>
    /// <param name="newDesignForm">A <see cref="SlideDesignModule"/> with the design form to display.</param>
    /// <param name="nodeID">Contains the node ID (which is the Node.Name property) of the node that is 
    /// modified or "" if this should be a new slide.</param>
    private void OpenStimulusDesignerForm(SlideDesignModule newDesignForm, string nodeID)
    {
      string oldSlidename = newDesignForm.SlideName;

      if (newDesignForm.ShowDialog() == DialogResult.OK)
      {
        Slide newSlide = newDesignForm.Slide;
        string newSlidename = newSlide.Name;
        if (nodeID != string.Empty)
        {
          this.OverwriteSlide(newSlide, nodeID);
        }
        else
        {
          this.AddSlide(newSlide);
        }

        this.SlideShowModified();
      }
    }

    /// <summary>
    /// Opens a <see cref="DesktopDialog"/> form, waits for succesful
    /// design and updates slideshow with the designed <see cref="Slide"/>.
    /// </summary>
    /// <param name="newDesignForm">A <see cref="DesktopDialog"/> with the desktop design form to display.</param>
    /// <param name="nodeID">Contains the node ID (which is the Node.Name property) of the node that is 
    /// modified or "" if this should be a new slide.</param>
    private void OpenDesktopDesignerForm(DesktopDialog newDesignForm, string nodeID)
    {
      if (newDesignForm.ShowDialog() == DialogResult.OK)
      {
        Slide newSlide = newDesignForm.Slide;
        if (nodeID != string.Empty)
        {
          this.OverwriteSlide(newSlide, nodeID);
        }
        else
        {
          this.AddSlide(newSlide);
        }

        this.SlideShowModified();
      }
    }

    /// <summary>
    /// Opens a <see cref="BrowserDialog"/> form, waits for succesful
    /// design and updates slideshow with the designed <see cref="BrowserTreeNode"/>.
    /// </summary>
    /// <param name="node">Contains the node that is 
    /// modified or null if this should be a new slide.</param>
    private void OpenBrowserDesignerForm(BrowserTreeNode node)
    {
      BrowserDialog dlg = new BrowserDialog();
      if (node != null)
      {
        dlg.BrowserNode = node;
      }

      if (dlg.ShowDialog() == DialogResult.OK)
      {
        BrowserTreeNode newNode = dlg.BrowserNode;
        if (node != null)
        {
          newNode.UrlToID.Clear();
          newNode.UrlToID.Add(newNode.OriginURL, Convert.ToInt32(node.Name));
          node = newNode;
        }
        else
        {
          this.AddBrowserSlide(newNode);
        }

        this.SlideShowModified();
      }
    }

    #endregion //METHODS

    ///////////////////////////////////////////////////////////////////////////////
    // Small helping Methods                                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region HELPER

    /// <summary>
    /// This method returns an unused unique slide name for a new <see cref="Slide"/>
    /// </summary>
    /// <returns>A <see cref="String"/> with an unique slide name 
    /// for a new <see cref="Slide"/></returns>
    private string GetUnusedSlideName()
    {
      NameObjectList<Slide> slides = this.slideshow.Slides;
      int count = slides.Count;
      string proposal = "Slide" + count.ToString();

      while (slides.Contains(proposal))
      {
        count++;
        proposal = "Slide" + count.ToString();
      }

      return proposal;
    }

    /// <summary>
    /// This method sets the modified flag.
    /// </summary>
    private void SlideShowModified()
    {
      Document.ActiveDocument.Modified = true;
      Document.ActiveDocument.ExperimentSettings.SlideShow.IsModified = true;
    }

    #endregion //HELPER
  }
}
