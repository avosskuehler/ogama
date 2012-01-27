// <copyright file="BrowserTreeNode.cs" company="FU Berlin">
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

namespace Ogama.Modules.Common.SlideCollections
{
  using System;
  using System.Collections.Generic;
  using System.Windows.Forms;
  using System.Xml;
  using System.Xml.Serialization;

  using Ogama.Modules.Common.Types;

  using VectorGraphics.Elements.ElementCollections;

  /// <summary>
  /// The xml serializable browser slide class which inherits <see cref="SlideshowTreeNode"/>
  /// to add web browsing slide capabilities
  /// </summary>
  [Serializable]
  public class BrowserTreeNode : SlideshowTreeNode
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
    /// Initializes a new instance of the BrowserTreeNode class.
    /// </summary>
    public BrowserTreeNode()
    {
      this.Name = "-1";
      this.Text = "Webpage";
      this.Category = "Webpage";
      this.OriginURL = "about:blank";
      this.ImageKey = "Browser";
      this.BrowseDepth = 0;
      this.UrlToID = new XMLSerializableDictionary<string, int>();
    }

    /// <summary>
    /// Initializes a new instance of the BrowserTreeNode class 
    /// by cloneing the given <see cref="BrowserTreeNode"/>
    /// </summary>
    /// <param name="browserTreeNode">A <see cref="BrowserTreeNode"/> to be cloned.</param>
    public BrowserTreeNode(BrowserTreeNode browserTreeNode)
    {
      this.Name = browserTreeNode.Name;
      this.Category = browserTreeNode.Category;
      this.Text = browserTreeNode.Text;
      this.BrowseDepth = browserTreeNode.BrowseDepth;
      this.ImageKey = browserTreeNode.ImageKey;
      this.UrlToID = browserTreeNode.UrlToID;
      foreach (SlideshowTreeNode node in browserTreeNode.Nodes)
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
    /// Gets or sets the url to id assignments of the subnodes of
    /// this browser tree node
    /// </summary>
    public XMLSerializableDictionary<string, int> UrlToID { get; set; }

    /// <summary>
    /// Gets or sets the <see cref="OriginURL"/> for this browser slide.
    /// </summary>
    public string OriginURL { get; set; }

    /// <summary>
    /// Gets or sets the <see cref="Category"/> for this browser slide.
    /// </summary>
    public string Category { get; set; }

    /// <summary>
    /// Gets or sets the browse depth for this browser slide.
    /// </summary>
    public int BrowseDepth { get; set; }

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
    /// Clones this BrowserSlide object.
    /// </summary>
    /// <returns>A clone of the current <see cref="BrowserTreeNode"/>.</returns>
    public override object Clone()
    {
      return new BrowserTreeNode(this);
    }

    /// <summary>
    /// This method is a custom deserialization with an <see cref="XmlSerializer"/>
    /// to read the contents of the <see cref="SlideshowTreeNode"/> that 
    /// are exposed in this override.
    /// It deserializes recursively.
    /// </summary>
    /// <param name="reader">The <see cref="XmlReader"/> to use.</param>
    /// <param name="node">The <see cref="SlideshowTreeNode"/> to deserialize.</param>
    protected override void DeserializeNode(XmlReader reader, SlideshowTreeNode node)
    {
      XmlSerializer slideSerializer = new XmlSerializer(typeof(Slide));
      XmlSerializer dictionarySerializer = new XmlSerializer(typeof(XMLSerializableDictionary<string, int>));
      BrowserTreeNode browserTreeNode = node as BrowserTreeNode;

      reader.ReadStartElement("BrowserTreeNode");
      reader.ReadStartElement("Name");
      browserTreeNode.Name = reader.ReadString();
      reader.ReadEndElement();
      if (reader.Name == "XMLSerializableDictionaryOfStringInt32")
      {
        browserTreeNode.UrlToID = (XMLSerializableDictionary<string, int>)dictionarySerializer.Deserialize(reader);
      }

      reader.ReadStartElement("OriginURL");
      browserTreeNode.OriginURL = reader.ReadString();
      reader.ReadEndElement();
      reader.ReadStartElement("Category");
      browserTreeNode.Category = reader.ReadString();
      reader.ReadEndElement();
      reader.ReadStartElement("BrowseDepth");
      browserTreeNode.BrowseDepth = reader.ReadContentAsInt();
      reader.ReadEndElement();
      reader.ReadStartElement("Randomize");
      browserTreeNode.Randomize = reader.ReadContentAsBoolean();
      reader.ReadEndElement();
      reader.ReadStartElement("NumberOfItemsToUse");
      browserTreeNode.NumberOfItemsToUse = reader.ReadContentAsInt();
      reader.ReadEndElement();

      if (reader.Name == "Slide")
      {
        browserTreeNode.Slide = (Slide)slideSerializer.Deserialize(reader);
      }

      reader.ReadStartElement("Tag");
      if (reader.Value != string.Empty)
      {
        browserTreeNode.Tag = reader.ReadContentAsString();
        reader.ReadEndElement();
      }

      reader.ReadStartElement("Text");
      node.Text = reader.ReadContentAsString();
      reader.ReadEndElement();
      while (reader.Name == "SlideshowTreeNode" && reader.NodeType == XmlNodeType.Element)
      {
        SlideshowTreeNode newNode = new SlideshowTreeNode();
        base.DeserializeNode(reader, newNode);
        this.SetTreeNodeImageKey(newNode);
        node.Nodes.Add(newNode);
      }

      reader.ReadEndElement();

      reader.MoveToContent();
    }

    /// <summary>
    /// This method is a custom serialization with an <see cref="XmlSerializer"/>
    /// to write the contents of the <see cref="BrowserTreeNode"/> that 
    /// are exposed in this override into an XML file.
    /// It serializes recursively.
    /// </summary>
    /// <param name="writer">The <see cref="XmlWriter"/> to use.</param>
    /// <param name="node">The <see cref="SlideshowTreeNode"/> to serialize.</param>
    protected override void SerializeNode(XmlWriter writer, SlideshowTreeNode node)
    {
      XmlSerializer slideSerializer = new XmlSerializer(typeof(Slide));
      XmlSerializer dictionarySerializer = new XmlSerializer(typeof(XMLSerializableDictionary<string, int>));
      BrowserTreeNode browserTreeNode = node as BrowserTreeNode;

      writer.WriteStartElement("BrowserTreeNode");

      writer.WriteStartElement("Name");
      writer.WriteValue(browserTreeNode.Name);
      writer.WriteEndElement();

      dictionarySerializer.Serialize(writer, browserTreeNode.UrlToID);

      writer.WriteStartElement("OriginURL");
      writer.WriteValue(browserTreeNode.OriginURL);
      writer.WriteEndElement();
      writer.WriteStartElement("Category");
      writer.WriteValue(browserTreeNode.Category);
      writer.WriteEndElement();
      writer.WriteStartElement("BrowseDepth");
      writer.WriteValue(browserTreeNode.BrowseDepth);
      writer.WriteEndElement();
      writer.WriteStartElement("Randomize");
      writer.WriteValue(browserTreeNode.Randomize);
      writer.WriteEndElement();
      writer.WriteStartElement("NumberOfItemsToUse");
      writer.WriteValue(browserTreeNode.NumberOfItemsToUse);
      writer.WriteEndElement();
      if (browserTreeNode.Slide != null)
      {
        slideSerializer.Serialize(writer, browserTreeNode.Slide);
      }

      writer.WriteStartElement("Tag");
      if (browserTreeNode.Tag != null)
      {
        writer.WriteValue(browserTreeNode.Tag);
      }

      writer.WriteEndElement();
      writer.WriteStartElement("Text");
      writer.WriteValue(browserTreeNode.Text);
      writer.WriteEndElement();

      if (browserTreeNode.Nodes.Count > 0)
      {
        foreach (SlideshowTreeNode subNode in browserTreeNode.Nodes)
        {
          // Use base serializer for subnodes, because
          // they are basically scroll image slides
          base.SerializeNode(writer, subNode);
        }
      }

      writer.WriteEndElement();
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
        trial = new Trial(node.Text, Slideshow.GetIdOfNode(node));
        trials.Add(trial);
      }

      foreach (TreeNode subNode in node.Nodes)
      {
        SlideshowTreeNode subSlideNode = subNode as SlideshowTreeNode;
        Slide slide = subSlideNode.Slide;
        if (slide != null)
        {
          // By default use a new trial for each slide
          trial = new Trial(subNode.Text, Slideshow.GetIdOfNode(subNode));

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

    #endregion //METHODS

    ///////////////////////////////////////////////////////////////////////////////
    // Small helping Methods                                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region HELPER
    #endregion //HELPER
  }
}
