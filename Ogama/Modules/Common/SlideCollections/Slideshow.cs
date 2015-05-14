// <copyright file="Slideshow.cs" company="FU Berlin">
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

namespace Ogama.Modules.Common.SlideCollections
{
  using System;
  using System.Collections.Generic;
  using System.ComponentModel;
  using System.Drawing;
  using System.Windows.Forms;
  using System.Xml;
  using System.Xml.Serialization;

  using Ogama.Modules.Common.Tools;
  using Ogama.Modules.Common.Types;
  using Ogama.Modules.SlideshowDesign.Shuffling;

  using VectorGraphics.Elements;
  using VectorGraphics.Elements.ElementCollections;
  using VectorGraphics.Tools;

  /// <summary>
  /// The xml serializable slideshow class which inherits <see cref="SlideshowTreeNode"/>
  /// to add randomizing methods for all nodes contained in the root node.
  /// It also adds a <see cref="CustomShuffling"/> property to the slideshow.
  /// </summary>
  [Serializable]
  [XmlInclude(typeof(BrowserTreeNode))]
  public class Slideshow : SlideshowTreeNode
  {
    ///////////////////// //////////////////////////////////////////////////////////
    // Defining Constants                                                        //
    ///////////////////////////////////////////////////////////////////////////////
    #region CONSTANTS
    #endregion //CONSTANTS

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Variables, Enumerations, Events                                  //
    ///////////////////////////////////////////////////////////////////////////////
    #region FIELDS

    /// <summary>
    /// The custom shuffling of the slideshow.
    /// </summary>
    private CustomShuffling shuffling;

    /// <summary>
    /// Indicates unsaved modifications to the slideshow.
    /// </summary>
    private bool isModified;

    #endregion //FIELDS

    ///////////////////////////////////////////////////////////////////////////////
    // Construction and Initializing methods                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region CONSTRUCTION

    /// <summary>
    /// Initializes a new instance of the Slideshow class.
    /// </summary>
    public Slideshow()
    {
      this.Name = "-1";
      this.Text = "Slideshow";
      this.shuffling = new CustomShuffling();
    }

    /// <summary>
    /// Initializes a new instance of the Slideshow class by cloneing the given <see cref="Slideshow"/>
    /// </summary>
    /// <param name="slideshow">A <see cref="Slideshow"/> to be cloned.</param>
    public Slideshow(Slideshow slideshow)
    {
      this.Name = slideshow.Name;
      this.Text = slideshow.Text;
      this.ImageKey = slideshow.ImageKey;
      this.shuffling = slideshow.Shuffling;
      foreach (SlideshowTreeNode node in slideshow.Nodes)
      {
        this.Nodes.Add((SlideshowTreeNode)node.Clone());
      }
    }

    #endregion //CONSTRUCTION

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Enumerations                                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region ENUMS

    /// <summary>
    /// This is the method delegate for the GetUnusedNodeID method,
    /// </summary>
    /// <returns>A <see cref="string"/> with a unique node id.</returns>
    public delegate string GetUnusedNodeIDDelegate();

    #endregion ENUMS

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Properties                                                       //
    ///////////////////////////////////////////////////////////////////////////////
    #region PROPERTIES

    /// <summary>
    /// Gets the list of <see cref="Slide"/>s of this slideshow.
    /// </summary>
    /// <value>A <see cref="NameObjectList{Slide}"/> with the 
    /// <see cref="Slide"/>s of this slideshow.</value>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    [XmlIgnore]
    public NameObjectList<Slide> Slides
    {
      get { return this.GetSlides(); }
    }

    /// <summary>
    /// Gets the list of trials of this slideshow.
    /// </summary>
    /// <value>A <see cref="NameObjectList{Trial}"/> with the 
    /// trials of this slideshow.</value>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    [XmlIgnore]
    public TrialCollection Trials
    {
      get { return this.GetTrials(); }
    }

    /// <summary>
    /// Gets or sets the <see cref="CustomShuffling"/> for this slideshow.
    /// </summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public CustomShuffling Shuffling
    {
      get { return this.shuffling; }
      set { this.shuffling = value; }
    }

    /// <summary>
    /// Gets or sets a value indicating whether this slideshow has unsaved changes.
    /// </summary>
    public bool IsModified
    {
      get { return this.isModified; }
      set { this.isModified = value; }
    }

    #endregion //PROPERTIES

    ///////////////////////////////////////////////////////////////////////////////
    // Public methods                                                            //
    ///////////////////////////////////////////////////////////////////////////////
    #region PUBLICMETHODS

    /// <summary>
    /// This method draws a dice bitmap at the given position
    /// on the given graphics object.
    /// </summary>
    /// <param name="g">The <see cref="Graphics"/> to draw to.</param>
    /// <param name="position">The <see cref="Point"/> location of the top left corner.</param>
    public static void DrawDice(Graphics g, Point position)
    {
      // Draw the other bits of information
      Image diceBitmap = Properties.Resources.DiceHS16;
      g.DrawImage(diceBitmap, position.X, position.Y, diceBitmap.Width, diceBitmap.Height);
    }

    /// <summary>
    /// This method draws a disabled bitmap at the given position
    /// on the given graphics object.
    /// </summary>
    /// <param name="g">The <see cref="Graphics"/> to draw to.</param>
    /// <param name="position">The <see cref="Point"/> location of the top left corner.</param>
    public static void DrawDisabled(Graphics g, Point position)
    {
      Image disabledBitmap = Properties.Resources.Disabled16;
      g.DrawImage(disabledBitmap, position.X, position.Y, disabledBitmap.Width, disabledBitmap.Height);
    }

    /// <summary>
    /// This method draws a trial bitmap at the given position
    /// on the given graphics object.
    /// </summary>
    /// <param name="g">The <see cref="Graphics"/> to draw to.</param>
    /// <param name="position">The <see cref="Point"/> location of the top left corner.</param>
    public static void DrawTrialIcon(Graphics g, Point position)
    {
      // Draw the other bits of information
      Image trialBitmap = Properties.Resources.OrgChartHS;
      g.DrawImage(trialBitmap, position.X, position.Y, trialBitmap.Width, trialBitmap.Height);
    }

    /// <summary>
    /// This method iterates the child nodes of the given <see cref="SlideshowTreeNode"/> to
    /// find the <see cref="SlideshowTreeNode"/> with the given name using the
    /// <see cref="TreeNode.Name"/> or <see cref="TreeNode.Text"/> property 
    /// of the <see cref="TreeNode"/> as indicated in the third paramter.
    /// </summary>
    /// <param name="name">The <see cref="String"/> with the name or text to search for</param>
    /// <param name="rootNode">The base <see cref="SlideshowTreeNode"/> to start the search from.</param>
    /// <param name="useNameProperty"><strong>True</strong>, if the <see cref="TreeNode.Name"/>
    /// property should be used for checking, <strong>false</strong> if the <see cref="TreeNode.Text"/>
    /// property should be used.</param>
    /// <returns>The <see cref="SlideshowTreeNode"/> that matches the search criteria or null if
    /// no node was found.</returns>
    public static SlideshowTreeNode IterateTreeNodes(string name, SlideshowTreeNode rootNode, bool useNameProperty)
    {
      SlideshowTreeNode returnTreeNode;
      foreach (SlideshowTreeNode childNode in rootNode.Nodes)
      {
        if (CheckNameOrText(childNode, name, useNameProperty))
        {
          return childNode;
        }
        else
        {
          if (childNode.Nodes.Count > 0)
          {
            returnTreeNode = IterateTreeNodes(name, childNode, useNameProperty);
            if (returnTreeNode != null)
            {
              return returnTreeNode;
            }
          }
        }
      }

      return null;
    }

    /// <summary>
    /// This method returns an unused node id for a new inserted node.
    /// </summary>
    /// <returns>An unused node id as a <see cref="String"/> to be used
    /// for a new inserted node <see cref="TreeNode.Name"/>.</returns>
    public string GetUnusedNodeID()
    {
      List<string> names = new List<string>();
      this.GetNodeNames(this, ref names);

      int count = names.Count;
      string proposal = count.ToString();

      while (names.Contains(proposal))
      {
        count++;
        proposal = count.ToString();
      }

      return proposal;
    }

    /// <summary>
    /// This method sets new ids for all nodes in the tree by using the 
    /// given start id.
    /// </summary>
    /// <param name="firstUnusedNodeID">A <see cref="String"/> with the first unused ID.</param>
    public void SetNewNodeIDs(string firstUnusedNodeID)
    {
      int nodeID = Convert.ToInt32(firstUnusedNodeID);
      this.Name = firstUnusedNodeID;
      nodeID++;
      this.SetNewIDsToAllNodes(this, ref nodeID);
    }

    /// <summary>
    /// This method sets the modified status of all slides in the slideshow.
    /// </summary>
    public void SetModifiedToAllSlides()
    {
      this.SetModifiedToAllSlideNodes(this);
    }

    /// <summary>
    /// Parses the slideshow for a slide with flash movie 
    /// and returns true if there is at least one.
    /// </summary>
    /// <returns><strong>True</strong> if the slideshow contains one flash
    /// movie, otherwise <strong>false</strong>.</returns>
    public bool HasScreenCaptureContent()
    {
      foreach (Trial trial in this.GetTrials())
      {
        if (trial.HasActiveXContent || trial.HasDesktopRecordingContent)
        {
          return true;
        }
      }

      return false;
    }

    /// <summary>
    /// This method parses all slides and updates the path values
    /// for all resources located on the disk.
    /// This method is needed when the Experiments base folder is moved to another
    /// location or computer.
    /// </summary>
    /// <param name="newResourcesPath">The Document.ActiveDocument.ExperimentSettings.SlideResourcesPath</param>
    public void UpdateExperimentPathOfResources(string newResourcesPath)
    {
      this.ParseNodeForSlideUpdate(this, newResourcesPath);
    }

    /// <summary>
    /// This method returns a list with the trial names of all trials
    /// in the slideshow.
    /// </summary>
    /// <returns>A <see cref="List{String}"/> with the trial names.</returns>
    public List<string> GetTrialNames()
    {
      List<string> names = new List<string>();
      foreach (Trial trial in this.Trials)
      {
        names.Add(trial.Name);
      }

      return names;
    }

    /// <summary>
    /// This method iterates the slideshow recursively to find the
    /// trial with the given ID.
    /// </summary>
    /// <param name="trialID">An <see cref="Int32"/> with the trial id to search for.</param>
    /// <returns>The <see cref="Trial"/> that matches the trial id.</returns>
    public Trial GetTrialByID(int trialID)
    {
      SlideshowTreeNode trialNode = IterateTreeNodes(trialID.ToString(), this, true);
      if (trialNode != null)
      {
        Trial trial = new Trial(trialNode.Text, trialID);
        if (trialNode.Slide != null)
        {
          if (trialNode.Parent.Tag != null && trialNode.Parent.Tag.ToString() == "Trial")
          {
            trial = new Trial(trialNode.Parent.Text, Convert.ToInt32(trialNode.Parent.Name));
            foreach (SlideshowTreeNode slideNode in trialNode.Parent.Nodes)
            {
              trial.Add((Slide)slideNode.Slide.Clone());
            }
          }
          else
          {
            trial.Add((Slide)trialNode.Slide.Clone());
          }
        }
        else if (trialNode.Nodes.Count > 0 &&
          (trialNode.Tag != null && trialNode.Tag.ToString() == "Trial"))
        {
          foreach (SlideshowTreeNode slideNode in trialNode.Nodes)
          {
            trial.Add((Slide)slideNode.Slide.Clone());
          }
        }

        return trial;
      }

      return null;
    }

    /// <summary>
    /// This method iterates the slideshow recursively to find the
    /// <see cref="SlideshowTreeNode"/> with the given ID.
    /// </summary>
    /// <param name="trialID">An <see cref="Int32"/> with the trial id to search for.</param>
    /// <returns>The <see cref="SlideshowTreeNode"/> that matches the trial id.</returns>
    public SlideshowTreeNode GetNodeByID(int trialID)
    {
      return IterateTreeNodes(trialID.ToString(), this, true);
    }

    /// <summary>
    /// Returns the current <see cref="TrialCollection"/> that was randomized
    /// according to the shuffling settings defined.
    /// </summary>
    /// <returns>The current <see cref="TrialCollection"/> that was randomized
    /// according to the shuffling settings defined.</returns>
    public TrialCollection GetRandomizedTrials()
    {
      List<object> nodeCollection = this.ParseNodeForRandomizedTrials(this);
      var trials = new TrialCollection();
      this.GetTrialsFromObjectCollection(nodeCollection, ref trials);
      return trials;
    }

    /// <summary>
    /// This method iterates the tree recursively to fill the list with the 
    /// names of the nodes.
    /// </summary>
    /// <param name="node">The <see cref="TreeNode"/> to start parsing.</param>
    /// <param name="names">Ref. The output <see cref="List{String}"/> with all used names.</param>
    public void GetNodeNames(TreeNode node, ref List<string> names)
    {
      foreach (TreeNode subNode in node.Nodes)
      {
        if (!names.Contains(subNode.Name))
        {
          names.Add(subNode.Name);
        }

        this.GetNodeNames(subNode, ref names);
      }
    }

    #endregion //PUBLICMETHODS

    ///////////////////////////////////////////////////////////////////////////////
    // Inherited methods                                                         //
    ///////////////////////////////////////////////////////////////////////////////
    #region OVERRIDES

    /// <summary>
    /// Clones this slideshow object.
    /// </summary>
    /// <returns>A clone of the current <see cref="Slideshow"/>.</returns>
    public override object Clone()
    {
      return new Slideshow(this);
    }

    /// <summary>
    /// This method is a custom serialization with an <see cref="XmlSerializer"/>
    /// to write the contents of the <see cref="Slideshow"/> that 
    /// are exposed in this override into an XML file.
    /// It serializes recursively.
    /// </summary>
    /// <param name="writer">The <see cref="XmlWriter"/> to use.</param>
    /// <param name="node">The <see cref="SlideshowTreeNode"/> to serialize.</param>
    public override void SerializeNode(XmlWriter writer, SlideshowTreeNode node)
    {
      writer.WriteStartElement("IsModified");
      writer.WriteValue(this.IsModified);
      writer.WriteEndElement();

      var shuffleSerializer = new XmlSerializer(typeof(CustomShuffling));

      if (this.Shuffling != null)
      {
        shuffleSerializer.Serialize(writer, this.Shuffling);
      }

      if (node.Nodes.Count > 0)
      {
        foreach (SlideshowTreeNode subNode in node.Nodes)
        {
          subNode.SerializeNode(writer, subNode);
        }
      }
    }

    /// <summary>
    /// This method is a custom deserialization with an <see cref="XmlSerializer"/>
    /// to read the contents of the <see cref="Slideshow"/> that 
    /// are exposed in this override. It deserializes recursively.
    /// </summary>
    /// <param name="reader">The <see cref="XmlReader"/> to use.</param>
    /// <param name="node">The <see cref="SlideshowTreeNode"/> to deserialize.</param>
    public override void DeserializeNode(XmlReader reader, SlideshowTreeNode node)
    {
      var shuffleSerializer = new XmlSerializer(typeof(CustomShuffling));

      // Check for Versions < Ogama 4.3
      if (reader.Name == "SlideshowTreeNode")
      {
        base.DeserializeNode(reader, node);
        return;
      }

      reader.ReadStartElement("IsModified");
      this.IsModified = reader.ReadContentAsBoolean();
      reader.ReadEndElement();

      if (reader.Name == "CustomShuffling")
      {
        this.shuffling = (CustomShuffling)shuffleSerializer.Deserialize(reader);
      }


      while ((reader.Name == "SlideshowTreeNode" && reader.NodeType == XmlNodeType.Element) ||
        (reader.Name == "BrowserTreeNode" && reader.NodeType == XmlNodeType.Element))
      {
        if (reader.Name == "SlideshowTreeNode")
        {
          var newNode = new SlideshowTreeNode();
          newNode.DeserializeNode(reader, newNode);
          this.SetTreeNodeImageKey(newNode);
          node.Nodes.Add(newNode);
        }
        else if (reader.Name == "BrowserTreeNode")
        {
          var newNode = new BrowserTreeNode();
          newNode.DeserializeNode(reader, newNode);
          this.SetTreeNodeImageKey(newNode);
          node.Nodes.Add(newNode);
        }
      }
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
    /// This method updates the given element with the new resources path
    /// </summary>
    /// <param name="newResourcesPath">The new resource path</param>
    /// <param name="element">The <see cref="VGElement"/> to update.</param>
    private static void UpdateElement(string newResourcesPath, VGElement element)
    {
      // check all element for sounds
      if (element.Sound == null)
      {
        element.Sound = new AudioFile();
      }

      if (element.Sound.Filename != null)
      {
        if (element.Sound.Filename.Contains(@"\"))
        {
          element.Sound.Filename = System.IO.Path.GetFileName(element.Sound.Filename);
        }

        element.Sound.Filepath = newResourcesPath;
      }

      // check file based elements
      if (element is VGScrollImage)
      {
        var scrollImage = (VGScrollImage)element;
        if (scrollImage.Filepath != newResourcesPath)
        {
          scrollImage.Filename = System.IO.Path.GetFileName(scrollImage.Filename);
          scrollImage.Filepath = newResourcesPath;
          scrollImage.Canvas = Document.ActiveDocument.PresentationSize;
        }

        scrollImage.CreateInternalImage();
      }
      else if (element is VGImage)
      {
        VGImage image = (VGImage)element;
        if (image.Filepath != newResourcesPath)
        {
          image.Filename = System.IO.Path.GetFileName(image.Filename);
          image.Filepath = newResourcesPath;
          image.Canvas = Document.ActiveDocument.PresentationSize;
          image.CreateInternalImage();
        }
      }
      else if (element is VGFlash)
      {
        VGFlash flash = (VGFlash)element;
        if (flash.Filepath != newResourcesPath)
        {
          flash.Filename = System.IO.Path.GetFileName(flash.Filename);
          flash.Filepath = newResourcesPath;
        }
      }
    }

    /// <summary>
    /// This method returns true if the given node has the name or text given in the second parameter.
    /// </summary>
    /// <param name="node">The <see cref="TreeNode"/> to check.</param>
    /// <param name="name">A <see cref="String"/> with the name to search.</param>
    /// <param name="useNameProperty"><strong>True</strong>, if the <see cref="TreeNode.Name"/>
    /// property should be used for checking, <strong>false</strong> if the <see cref="TreeNode.Text"/>
    /// property should be used.</param>
    /// <returns><strong>True</strong>, if the node matches the search criteria, otherwise
    /// <strong>false</strong>.</returns>
    private static bool CheckNameOrText(TreeNode node, string name, bool useNameProperty)
    {
      if (useNameProperty)
      {
        if (node.Name == name)
        {
          return true;
        }
        else
        {
          return false;
        }
      }
      else
      {
        if (node.Text == name)
        {
          return true;
        }
        else
        {
          return false;
        }
      }
    }

    /// <summary>
    /// This method returns a sorted <see cref="List{Slide}"/> with the whole
    /// slideshow.
    /// </summary>
    /// <returns>A sorted <see cref="List{Slide}"/> with all slides from the whole
    /// slideshow.</returns>
    private NameObjectList<Slide> GetSlides()
    {
      NameObjectList<Slide> slides = new NameObjectList<Slide>();
      this.ParseNodeForSlides(ref slides, this);
      return slides;
    }

    /// <summary>
    /// This method returns a <see cref="TrialCollection"/> of all trials contained
    /// in this slideshow.
    /// </summary>
    /// <returns>A <see cref="TrialCollection"/> of all trials contained
    /// in this slideshow.</returns>
    private TrialCollection GetTrials()
    {
      TrialCollection trials = new TrialCollection();
      this.ParseNodeForTrials(ref trials, this);
      return trials;
    }

    /// <summary>
    /// This method iterates recursively through the tree to update the
    /// resource path of the slide elements found.
    /// </summary>
    /// <param name="node">The <see cref="TreeNode"/> to parse</param>
    /// <param name="newResourcesPath">A <see cref="String"/> with the new path to the resources.</param>
    private void ParseNodeForSlideUpdate(TreeNode node, string newResourcesPath)
    {
      // Check all child nodes
      foreach (TreeNode subNode in node.Nodes)
      {
        // Only if the node contains a slide
        Slide slide = ((SlideshowTreeNode)subNode).Slide;
        if (slide != null)
        {
          // Update background sound resource path
          if (slide.BackgroundSound != null)
          {
            if (slide.BackgroundSound.Filename.Contains(@"\"))
            {
              slide.BackgroundSound.Filename =
                System.IO.Path.GetFileName(slide.BackgroundSound.Filename);
            }

            slide.BackgroundSound.Filepath = newResourcesPath;
          }

          // Update element resource path
          foreach (VGElement element in slide.VGStimuli)
          {
            UpdateElement(newResourcesPath, element);
          }

          foreach (VGElement element in slide.ActiveXStimuli)
          {
            UpdateElement(newResourcesPath, element);
          }

          slide.PresentationSize = Document.ActiveDocument.PresentationSize;
        }

        // Iterate recursively
        this.ParseNodeForSlideUpdate(subNode, newResourcesPath);
      }
    }

    /// <summary>
    /// This method iterates recursively through the tree to return only the slides
    /// contained in the tree.
    /// </summary>
    /// <param name="slides">Ref. The output <see cref="NameObjectList{Slide}"/> of slides.</param>
    /// <param name="node">The <see cref="TreeNode"/> to parse.</param>
    private void ParseNodeForSlides(ref NameObjectList<Slide> slides, TreeNode node)
    {
      // Check all child nodes
      foreach (TreeNode subNode in node.Nodes)
      {
        // If a slide is contained, add it to the list
        Slide slide = ((SlideshowTreeNode)subNode).Slide;
        if (slide != null)
        {
          slides.Add(slide.Name, slide);
        }

        // Iterate recursively
        this.ParseNodeForSlides(ref slides, subNode);
      }
    }

    /// <summary>
    /// This method iterates recursively through the tree to return only the trials
    /// contained in the tree.
    /// </summary>
    /// <param name="trials">Ref. The output <see cref="TrialCollection"/> of trials.</param>
    /// <param name="node">The <see cref="TreeNode"/> to parse.</param>
    private void ParseNodeForTrials(ref TrialCollection trials, TreeNode node)
    {
      Trial trial = null;

      // Add trial if this node is marked to be a trial
      if (node.Tag != null && node.Tag.ToString() == "Trial")
      {
        trial = new Trial(node.Text, GetIdOfNode(node));
        trials.Add(trial);
      }

      foreach (TreeNode subNode in node.Nodes)
      {
        SlideshowTreeNode subSlideNode = subNode as SlideshowTreeNode;
        Slide slide = subSlideNode.Slide;
        if (slide != null)
        {
          // By default use a new trial for each slide
          trial = new Trial(subNode.Text, GetIdOfNode(subNode));

          // If parent is already marked as trial use this
          // instead.
          if (subNode.Parent != null && subNode.Parent.Tag != null && subNode.Parent.Tag.ToString() == "Trial")
          {
            trial = trials[trials.Count - 1];
          }

          // Add slide to correct trial
          trial.Add(slide);
        }

        // Iterate through the tree.
        this.ParseNodeForTrials(ref trials, subNode);

        // If a trial was found, add it to the list.
        if (trial != null && !trials.Contains(trial))
        {
          trials.Add(trial);
        }
      }
    }

    /// <summary>
    /// This method iterates recursively through the tree and resets the node ids.
    /// </summary>
    /// <param name="node">The <see cref="TreeNode"/> to parse.</param>
    /// <param name="unusedIDCounter">Ref. The increasing counter for unused IDs.</param>
    private void SetNewIDsToAllNodes(TreeNode node, ref int unusedIDCounter)
    {
      // Check all child nodes
      foreach (TreeNode subNode in node.Nodes)
      {
        subNode.Name = unusedIDCounter.ToString();
        unusedIDCounter++;

        // Iterate recursively
        this.SetNewIDsToAllNodes(subNode, ref unusedIDCounter);
      }
    }

    /// <summary>
    /// This method iterates recursively through the tree to set the modified flags
    /// in all slide nodes.
    /// </summary>
    /// <param name="node">The <see cref="TreeNode"/> to parse.</param>
    private void SetModifiedToAllSlideNodes(TreeNode node)
    {
      // Check all child nodes
      foreach (TreeNode subNode in node.Nodes)
      {
        Slide slide = ((SlideshowTreeNode)subNode).Slide;
        if (slide != null)
        {
          slide.Modified = true;
        }

        // Iterate recursively
        this.SetModifiedToAllSlideNodes(subNode);
      }
    }

    /// <summary>
    /// This method iterates recursively through the tree to return a
    /// shuffled trial collection using the <see cref="CustomShuffling"/>.
    /// </summary>
    /// <param name="node">The <see cref="TreeNode"/> to parse.</param>
    /// <returns>A <see cref="List{Object}"/> with the shuffled trials separated in
    /// shuffle groups.</returns>
    private List<object> ParseNodeForCustomShuffledTrials(TreeNode node)
    {
      // Get sections
      var sections = new List<object>();
      foreach (TreeNode subNode in node.Nodes)
      {
        var nodeCollection = this.ParseNodeForRandomizedTrials(subNode);
        var trials = new TrialCollection();
        this.GetTrialsFromObjectCollection(nodeCollection, ref trials);

        // Convert to List<object>
        var trialsObjectCollection = new List<object>();
        trialsObjectCollection.AddRange(trials.ToArray());

        // Shuffle sections if applicable
        if (this.shuffling.ShuffleSectionItems)
        {
          trialsObjectCollection = (List<object>)CollectionUtils<object>.Shuffle(trialsObjectCollection);
        }

        sections.Add(trialsObjectCollection);
      }

      // Create groups
      var groups = new List<object>();
      int index = 0;
      var finish = false;
      do
      {
        var group = new List<object>();
        for (int i = 0; i < sections.Count; i++)
        {
          var coll = (List<object>)sections[i];
          if (coll.Count == index)
          {
            finish = true;
            break;
          }

          for (int j = 0; j < this.shuffling.NumItemsOfSectionInGroup; j++)
          {
            group.Add(coll[index + j]);
          }
        }

        if (this.shuffling.ShuffleGroupItems)
        {
          group = (List<object>)CollectionUtils<object>.Shuffle(group);
        }

        if (!finish)
        {
          groups.Add(group);
        }

        index += this.shuffling.NumItemsOfSectionInGroup;
      }
      while (!finish);

      // Shuffle groups
      if (this.shuffling.ShuffleGroups)
      {
        groups = (List<object>)CollectionUtils<object>.Shuffle(groups);
      }

      return groups;
    }

    /// <summary>
    /// This method iterates recursively through the tree to return a
    /// shuffled trial collection.
    /// </summary>
    /// <param name="node">The <see cref="TreeNode"/> to parse.</param>
    /// <returns>A <see cref="List{Object}"/> with the shuffled trials separated in
    /// shuffle groups.</returns>
    private List<object> ParseNodeForRandomizedTrials(TreeNode node)
    {
      var items = new List<object>();

      var parentNode = node as SlideshowTreeNode;
      if (this.shuffling.UseThisCustomShuffling)
      {
        if (Convert.ToInt32(parentNode.Name) == this.shuffling.ShuffleSectionsParentNodeID)
        {
          return this.ParseNodeForCustomShuffledTrials(node);
        }
      }

      if (parentNode.Tag != null && parentNode.Tag.ToString() == "Trial")
      {
        var trial = new Trial(parentNode.Text, GetIdOfNode(parentNode));
        foreach (SlideshowTreeNode subNode in parentNode.Nodes)
        {
          var slide = subNode.Slide;
          if (slide != null && !slide.IsDisabled)
          {
            // if we have a preslide fixation trial add the slides before
            var preSlideTrialID = slide.IdOfPreSlideFixationTrial;
            if (preSlideTrialID != -1)
            {
              var preSlideNode = this.GetNodeByID(preSlideTrialID);
              trial.AddRange(this.GetPreSlideTrialSlides(preSlideNode));
            }

            trial.Add(slide);
          }
        }

        if (trial.Count > 0)
        {
          items.Add(trial);
        }
      }
      else
      {
        foreach (TreeNode subNode in node.Nodes)
        {
          var subCollectionNode = subNode as SlideshowTreeNode;
          if (subCollectionNode is BrowserTreeNode)
          {
            var trial = new Trial(subNode.Text, GetIdOfNode(subNode));
            var browserSlide = this.CreateBrowserSlide(subCollectionNode as BrowserTreeNode);
            if (!browserSlide.IsDisabled)
            {
              // if we have a preslide fixation trial add this trial before
              var preSlideTrialID = browserSlide.IdOfPreSlideFixationTrial;
              if (preSlideTrialID != -1)
              {
                var preSlideNode = this.GetNodeByID(preSlideTrialID);
                var preTrial = new Trial(preSlideNode.Text, preSlideTrialID);
                preTrial.AddRange(this.GetPreSlideTrialSlides(preSlideNode));
                items.Add(preTrial);
              }

              trial.Add(browserSlide);
              items.Add(trial);
            }
          }
          else
          {
            var slide = subCollectionNode.Slide;
            if (slide != null && !slide.IsDisabled)
            {
              var trial = new Trial(subNode.Text, GetIdOfNode(subNode));

              // if we have a preslide fixation trial add this trial before
              var preSlideTrialID = slide.IdOfPreSlideFixationTrial;
              if (preSlideTrialID != -1)
              {
                var preSlideNode = this.GetNodeByID(preSlideTrialID);
                var preTrial = new Trial(preSlideNode.Text, preSlideTrialID);
                preTrial.AddRange(this.GetPreSlideTrialSlides(preSlideNode));
                items.Add(preTrial);
              }

              trial.Add(slide);
              items.Add(trial);
            }
            else
            {
              items.Add(this.ParseNodeForRandomizedTrials(subNode));
            }
          }
        }

        if (parentNode.Randomize)
        {
          items = (List<object>)CollectionUtils<object>.Shuffle(items);

          if (parentNode.NumberOfItemsToUse != 0)
          {
            items.RemoveRange(parentNode.NumberOfItemsToUse, items.Count - parentNode.NumberOfItemsToUse);
          }
        }
      }

      return items;
    }

    /// <summary>
    /// This method returns a list of slides from the trial node that is selected
    /// to be shown before the slide.
    /// </summary>
    /// <param name="preSlideNode">The pre slide node.</param>
    /// <returns>The slide list.</returns>
    private List<Slide> GetPreSlideTrialSlides(SlideshowTreeNode preSlideNode)
    {
      var slides = new List<Slide>();
      if (preSlideNode.Tag != null && preSlideNode.Tag.ToString() == "Trial")
      {
        foreach (SlideshowTreeNode subNode in preSlideNode.Nodes)
        {
          var slide = subNode.Slide;
          if (slide != null)
          {
            slides.Add(slide);
          }
        }
      }
      else
      {
        slides.Add(preSlideNode.Slide);
      }

      return slides;
    }

    /// <summary>
    /// This method creates a <see cref="Slide"/> containing the<see cref="VGBrowser"/>
    /// that can be used to display the website described in the 
    /// given <see cref="BrowserTreeNode"/>
    /// </summary>
    /// <param name="browserSlide">The <see cref="BrowserTreeNode"/> to be converted into a 
    /// presentation slide.</param>
    /// <returns>A <see cref="Slide"/> containing the <see cref="VGBrowser"/>
    /// that displays the web site.</returns>
    private Slide CreateBrowserSlide(BrowserTreeNode browserSlide)
    {
      Slide slide = (Slide)browserSlide.Slide.Clone();

      VGBrowser browser = new VGBrowser(
        ShapeDrawAction.None,
        browserSlide.OriginURL,
        browserSlide.BrowseDepth,
        Pens.Black,
        Brushes.Black,
        SystemFonts.DefaultFont,
        Color.Black,
        PointF.Empty,
        Document.ActiveDocument.PresentationSize,
        VGStyleGroup.ACTIVEX,
        browserSlide.Name,
        string.Empty);
      slide.ActiveXStimuli.Add(browser);
      return slide;
    }

    /// <summary>
    /// This method iterates the given shuffled object collection recursively
    /// to return the one dimensional <see cref="TrialCollection"/> that is used
    /// during presentation.
    /// </summary>
    /// <param name="collection">A <see cref="List{Object}"/> with the shuffled trials separated in
    /// shuffle groups.</param>
    /// <param name="trials">Ref. The output <see cref="TrialCollection"/>.</param>
    private void GetTrialsFromObjectCollection(List<object> collection, ref TrialCollection trials)
    {
      foreach (object item in collection)
      {
        if (item is Trial)
        {
          Trial trial = item as Trial;
          trials.Add(trial);
        }
        else if (item is List<object>)
        {
          this.GetTrialsFromObjectCollection((List<object>)item, ref trials);
        }
      }
    }

    #endregion //METHODS

    ///////////////////////////////////////////////////////////////////////////////
    // Small helping Methods                                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region HELPER

    /// <summary>
    /// This method returns the <see cref="Int32"/> converted id of the node,
    /// that is written in the <see cref="TreeNode.Name"/> property in the format
    /// "NodeXXX"
    /// </summary>
    /// <param name="node">The <see cref="TreeNode"/> thats id should be returned.</param>
    /// <returns>The <see cref="Int32"/> converted id of the given node</returns>
    public static int GetIdOfNode(TreeNode node)
    {
      int id = -1;
      string name = node.Name;
      name = name.Replace("Node", string.Empty);
      if (int.TryParse(name, out id))
      {
        return id;
      }

      return id;
    }

    #endregion //HELPER

  }
}
