// <copyright file="SlideshowTreeNode.cs" company="FU Berlin">
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
  using System.Windows.Forms;
  using System.Xml;
  using System.Xml.Serialization;

  using Ogama.ExceptionHandling;

  using VectorGraphics.Elements;
  using VectorGraphics.Elements.ElementCollections;

  /// <summary>
  /// This class inherits <see cref="TreeNode"/> and implements <see cref="IXmlSerializable"/>.
  /// It extends the default <see cref="TreeNode"/> behavior with shuffling options
  /// and mainly the possibility to have a <see cref="Slide"/> contained in the node.
  /// </summary>
  /// <remarks>This is the solution to provide a <see cref="TreeView"/>
  /// in the user interface which in place also handles the arrangement and layout
  /// of the slideshow.</remarks>
  [Serializable]
  [XmlInclude(typeof(BrowserTreeNode))]
  public class SlideshowTreeNode : TreeNode, IXmlSerializable
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
    /// Indicates whether this slideshow containing slideshow sections
    /// should be shuffled before presentation.
    /// </summary>
    private bool randomize;

    /// <summary>
    /// Saves a slide if this <see cref="TreeNode"/> describes one.
    /// </summary>
    private Slide slide;

    /// <summary>
    /// Saves the number of items in this nodes subnodes,
    /// that should be used for presentation.
    /// </summary>
    private int numberOfItemsToUse;

    #endregion //FIELDS

    ///////////////////////////////////////////////////////////////////////////////
    // Construction and Initializing methods                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region CONSTRUCTION

    /// <summary>
    /// Initializes a new instance of the SlideshowTreeNode class.
    /// </summary>
    public SlideshowTreeNode()
    {
    }

    /// <summary>
    /// Initializes a new instance of the SlideshowTreeNode class by cloning
    /// the given <see cref="SlideshowTreeNode"/>
    /// </summary>
    /// <param name="newSlideshowTreeNode">The <see cref="SlideshowTreeNode"/> to clone.</param>
    public SlideshowTreeNode(SlideshowTreeNode newSlideshowTreeNode)
      : base(newSlideshowTreeNode.Text)
    {
      this.Name = newSlideshowTreeNode.Name;
      this.Tag = newSlideshowTreeNode.Tag;
      this.randomize = newSlideshowTreeNode.Randomize;
      this.numberOfItemsToUse = newSlideshowTreeNode.NumberOfItemsToUse;
      this.ImageKey = newSlideshowTreeNode.ImageKey;
      this.Slide = newSlideshowTreeNode.Slide;

      foreach (SlideshowTreeNode node in newSlideshowTreeNode.Nodes)
      {
        this.Nodes.Add((SlideshowTreeNode)node.Clone());
      }
    }

    /// <summary>
    /// Initializes a new instance of the SlideshowTreeNode class with the given name
    /// </summary>
    /// <param name="newName">The <see cref="string"/> with the new name 
    /// of this <see cref="SlideshowTreeNode"/>.</param>
    public SlideshowTreeNode(string newName)
      : base(newName)
    {
      this.ImageKey = "Slide";
    }

    #endregion //CONSTRUCTION

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Properties                                                       //
    ///////////////////////////////////////////////////////////////////////////////
    #region PROPERTIES

    /// <summary>
    /// Gets or sets a value indicating whether the irems contained in this node
    /// should be shuffled before presentation.
    /// </summary>
    /// <value>A <see cref="bool"/> that is <strong>true</strong>, if the
    /// contained objects should be shuffled, otherwise
    /// <strong>false.</strong></value>
    [Category("Shuffling")]
    [Description("Indicates whether the nodes items should be shuffled before presentation.")]
    public bool Randomize
    {
      get { return this.randomize; }
      set { this.randomize = value; }
    }

    /// <summary>
    /// Gets or sets a slide if this <see cref="TreeNode"/> describes one.
    /// </summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public Slide Slide
    {
      get { return this.slide; }
      set { this.slide = value; }
    }

    /// <summary>
    /// Gets or sets the number of items from this nodes subnode collection,
    /// that should be used for presentation.
    /// </summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public int NumberOfItemsToUse
    {
      get { return this.numberOfItemsToUse; }
      set { this.numberOfItemsToUse = value; }
    }

    #endregion //PROPERTIES

    ///////////////////////////////////////////////////////////////////////////////
    // Public methods                                                            //
    ///////////////////////////////////////////////////////////////////////////////
    #region PUBLICMETHODS

    /// <summary>
    /// This method checks if the given node is in the nodes collection
    /// or its child collections.
    /// </summary>
    /// <param name="nodeToCheck">The <see cref="TreeNode"/> to check if
    /// it is contained.</param>
    /// <returns><strong>True</strong>, if the given node is in this collection,
    /// otherwise <strong>false</strong>.</returns>
    public bool Contains(TreeNode nodeToCheck)
    {
      return this.ParseNodeForNode(this, nodeToCheck);
    }

    /// <summary>
    /// Implementation of <see cref="IXmlSerializable"/>.
    /// Converts this <see cref="Dictionary{TKey,TValue}"/>  into its XML representation. 
    /// </summary>
    /// <param name="writer">The <see cref="System.Xml.XmlWriter"/> stream 
    /// to which the <see cref="Dictionary{TKey,TValue}"/> is serialized.</param>
    public void WriteXml(System.Xml.XmlWriter writer)
    {
      this.SerializeNode(writer, this);
    }

    /// <summary>
    /// Implementation of <see cref="IXmlSerializable"/>.
    /// This property is reserved, apply the 
    /// <see cref="XmlSchemaProviderAttribute"/> to the class instead. 
    /// </summary>
    /// <returns>In this implementation it returns null, because we do not use it.</returns>
    public System.Xml.Schema.XmlSchema GetSchema()
    {
      return null;
    }

    /// <summary>
    /// Implementation of <see cref="IXmlSerializable"/>.
    /// Generates an <see cref="Dictionary{TKey,TValue}"/>  from its XML representation. 
    /// </summary>
    /// <param name="reader">The <see cref="System.Xml.XmlReader"/> stream from 
    /// which the dictionary is deserialized. </param>
    public void ReadXml(System.Xml.XmlReader reader)
    {
      bool wasEmpty = reader.IsEmptyElement;
      reader.Read();
      if (wasEmpty)
      {
        return;
      }

      while (reader.NodeType != XmlNodeType.EndElement)
      {
        this.DeserializeNode(reader, this);
      }

      reader.ReadEndElement();
    }

    /// <summary>
    /// This method interates through the slide contents of the given
    /// <see cref="SlideshowTreeNode"/> to set the appropriate slide
    /// icon in the treeview depending on the slide contents.
    /// </summary>
    /// <param name="newSlideshowTreeNode">The <see cref="SlideshowTreeNode"/>
    /// to set the <see cref="TreeNode.ImageKey"/> value for.</param>
    public void SetTreeNodeImageKey(SlideshowTreeNode newSlideshowTreeNode)
    {
      if (newSlideshowTreeNode is BrowserTreeNode)
      {
        newSlideshowTreeNode.ImageKey = "Browser";
        return;
      }

      if (newSlideshowTreeNode.Slide != null)
      {
        newSlideshowTreeNode.slide = (Slide)newSlideshowTreeNode.Slide.Clone();
        SlideType currentSlideType = SlideType.None;

        if (newSlideshowTreeNode.Slide.IsDesktopSlide)
        {
          newSlideshowTreeNode.ImageKey = "Desktop";
          return;
        }

        foreach (VGElement element in newSlideshowTreeNode.slide.VGStimuli)
        {
          if (element is VGEllipse || element is VGRectangle || element is VGLine || element is VGCursor || element is VGPolyline || element is VGSharp)
          {
            newSlideshowTreeNode.ImageKey = "Shapes";
            currentSlideType |= SlideType.Shapes;
          }
          else if (element is VGText || element is VGRichText)
          {
            newSlideshowTreeNode.ImageKey = "Instructions";
            currentSlideType |= SlideType.Instructions;
          }
          else if (element is VGImage)
          {
            newSlideshowTreeNode.ImageKey = "Images";
            currentSlideType |= SlideType.Images;
          }
          else if (element is VGSound)
          {
            newSlideshowTreeNode.ImageKey = "Sound";
            currentSlideType |= SlideType.Sound;
          }
        }

        foreach (VGElement element in newSlideshowTreeNode.slide.ActiveXStimuli)
        {
          if (element is VGFlash)
          {
            newSlideshowTreeNode.ImageKey = "Flash";
            currentSlideType |= SlideType.Flash;
          }
          else if (element is VGBrowser)
          {
            newSlideshowTreeNode.ImageKey = "Browser";
            currentSlideType |= SlideType.Browser;
          }
        }

        if (currentSlideType != SlideType.None)
        {
          if (currentSlideType != SlideType.Shapes &&
            currentSlideType != SlideType.Instructions &&
            currentSlideType != SlideType.Flash &&
            currentSlideType != SlideType.Browser &&
            currentSlideType != SlideType.Images &&
            currentSlideType != SlideType.Sound)
          {
            newSlideshowTreeNode.ImageKey = "MixedMedia";
          }
        }
        else
        {
          newSlideshowTreeNode.ImageKey = "Blank";
        }
      }
      else if (newSlideshowTreeNode.Tag != null && newSlideshowTreeNode.Tag.ToString() == "Trial")
      {
        newSlideshowTreeNode.ImageKey = "Trial";
      }
      else
      {
        newSlideshowTreeNode.ImageKey = "Folder";
      }
    }

    #endregion //PUBLICMETHODS

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
    // Inherited methods                                                         //
    ///////////////////////////////////////////////////////////////////////////////
    #region OVERRIDES

    /// <summary>
    /// Clones this trial object.
    /// </summary>
    /// <returns>A clone of the current <see cref="SlideshowTreeNode"/>.</returns>
    public override object Clone()
    {
      return new SlideshowTreeNode(this);
    }

    #endregion //OVERRIDES

    ///////////////////////////////////////////////////////////////////////////////
    // Methods for doing main class job                                          //
    ///////////////////////////////////////////////////////////////////////////////
    #region METHODS

    /// <summary>
    /// This method is a custom serialization with an <see cref="XmlSerializer"/>
    /// to write the contents of the <see cref="SlideshowTreeNode"/> that 
    /// are exposed in this override into an XML file.
    /// It serializes recursively.
    /// </summary>
    /// <param name="writer">The <see cref="XmlWriter"/> to use.</param>
    /// <param name="node">The <see cref="SlideshowTreeNode"/> to serialize.</param>
    public virtual void SerializeNode(XmlWriter writer, SlideshowTreeNode node)
    {
      var slideSerializer = new XmlSerializer(typeof(Slide));

      writer.WriteStartElement("SlideshowTreeNode");

      writer.WriteStartElement("Name");
      writer.WriteValue(node.Name);
      writer.WriteEndElement();
      writer.WriteStartElement("Randomize");
      writer.WriteValue(node.Randomize);
      writer.WriteEndElement();
      writer.WriteStartElement("NumberOfItemsToUse");
      writer.WriteValue(node.NumberOfItemsToUse);
      writer.WriteEndElement();
      if (node.Slide != null)
      {
        slideSerializer.Serialize(writer, node.Slide);
      }

      writer.WriteStartElement("Tag");
      if (node.Tag != null)
      {
        writer.WriteValue(node.Tag);
      }

      writer.WriteEndElement();
      writer.WriteStartElement("Text");
      writer.WriteValue(node.Text);
      writer.WriteEndElement();

      if (node.Nodes.Count > 0)
      {
        foreach (SlideshowTreeNode subNode in node.Nodes)
        {
          subNode.SerializeNode(writer, subNode);
        }
      }

      writer.WriteEndElement();
    }

    /// <summary>
    /// This method is a custom deserialization with an <see cref="XmlSerializer"/>
    /// to read the contents of the <see cref="SlideshowTreeNode"/> that 
    /// are exposed in this override.
    /// It deserializes recursively.
    /// </summary>
    /// <param name="reader">The <see cref="XmlReader"/> to use.</param>
    /// <param name="node">The <see cref="SlideshowTreeNode"/> to deserialize.</param>
    public virtual void DeserializeNode(XmlReader reader, SlideshowTreeNode node)
    {
      var slideSerializer = new XmlSerializer(typeof(Slide));

      // Check for older versions of Ogama 1.X 
      if (reader.Name == "Slides")
      {
        // A List<Slide> instead of SlideshowTreeNodes
        // in the slideshow node
        reader.ReadStartElement("Slides");
        this.ParseOgamaV1Slideshow(reader, slideSerializer);
        reader.ReadEndElement();
      }
      else if (reader.Name == "Slide")
      {
        // Directly the slides in the slideshow node
        this.ParseOgamaV1Slideshow(reader, slideSerializer);
      }
      else
      {
        // Ogama V2 format
        reader.ReadStartElement("SlideshowTreeNode");
        reader.ReadStartElement("Name");
        node.Name = reader.ReadString();
        reader.ReadEndElement();
        reader.ReadStartElement("Randomize");
        node.Randomize = reader.ReadContentAsBoolean();
        reader.ReadEndElement();
        reader.ReadStartElement("NumberOfItemsToUse");
        node.NumberOfItemsToUse = reader.ReadContentAsInt();
        reader.ReadEndElement();

        if (reader.Name == "Slide")
        {
          node.Slide = (Slide)slideSerializer.Deserialize(reader);
        }

        reader.ReadStartElement("Tag");
        if (reader.Value != string.Empty)
        {
          node.Tag = reader.ReadContentAsString();
          reader.ReadEndElement();
        }

        reader.ReadStartElement("Text");
        node.Text = reader.ReadContentAsString();
        reader.ReadEndElement();
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

        reader.ReadEndElement();

        reader.MoveToContent();
      }
    }

    /// <summary>
    /// This method parses the xml file for a slide layout of Ogama V1
    /// and updates them to the current format.
    /// </summary>
    /// <param name="reader">The <see cref="XmlReader"/> with the file to parse.</param>
    /// <param name="slideSerializer">The <see cref="XmlSerializer"/> for deserializing slide nodes.</param>
    protected void ParseOgamaV1Slideshow(XmlReader reader, XmlSerializer slideSerializer)
    {
      string message = "You are opening an OGAMA 1.X experiment." + Environment.NewLine + Environment.NewLine +
        "The experiment file along with the database need to be converted to the new Ogama 2 format." +
        Environment.NewLine +
        "PLEASE NOTE: Be sure to have a backup copy of the whole experiment along with the database before doing this conversion !" +
        " (Zip or copy the whole experiment folder.)" +
        Environment.NewLine +
        "The possibility is not ruled out, that the conversion fails and your experiment data " +
        "is lost." + Environment.NewLine + Environment.NewLine +
        "Can we start with the conversion now ?";
      DialogResult result = InformationDialog.Show("Upgrade experiment to new version ?", message, true, MessageBoxIcon.Question);
      switch (result)
      {
        case DialogResult.Cancel:
        case DialogResult.No:
          throw new WarningException("Upgrading cancelled due to user request");
        case DialogResult.Yes:
          break;
      }

      // Go through all slides in the serialized List<Slide>
      while (reader.Name == "Slide")
      {
        Slide actualSlide = (Slide)slideSerializer.Deserialize(reader);

        // Remove fill flag for images, because in V1 it was used
        // to indicate the drawing of the image, but in V2
        // It indicates a transparent fill over the image.
        foreach (VGElement element in actualSlide.VGStimuli)
        {
          if (element is VGImage)
          {
            element.ShapeDrawAction |= ~ShapeDrawAction.Fill;
          }

          if (element is VGText)
          {
            element.ShapeDrawAction |= ~ShapeDrawAction.Edge;
          }

          if ((int)element.ShapeDrawAction < 0)
          {
            element.ShapeDrawAction = ShapeDrawAction.None;
          }
        }

        var slideshow = this as Slideshow;

        // Add node
        var slideNode = new SlideshowTreeNode(actualSlide.Name);
        slideNode.Name = slideshow.GetUnusedNodeID();
        slideNode.Slide = actualSlide;
        slideshow.Nodes.Add(slideNode);
      }
    }

    /// <summary>
    /// This method recursively iterates the given <see cref="TreeNode"/> parent 
    /// for the second parameter <see cref="TreeNode"/> to be contained.
    /// </summary>
    /// <param name="parentNode">The <see cref="TreeNode"/> to search in.</param>
    /// <param name="nodeToCheck">The <see cref="TreeNode"/> to search for.</param>
    /// <returns><strong>True</strong>, if the given node is in this collection,
    /// otherwise <strong>false</strong>.</returns>
    private bool ParseNodeForNode(TreeNode parentNode, TreeNode nodeToCheck)
    {
      bool subNodesFound = false;
      foreach (TreeNode subNode in parentNode.Nodes)
      {
        if (subNode == nodeToCheck)
        {
          return true;
        }

        subNodesFound = this.ParseNodeForNode(subNode, nodeToCheck);
      }

      return subNodesFound;
    }

    #endregion //METHODS

    ///////////////////////////////////////////////////////////////////////////////
    // Small helping Methods                                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region HELPER
    #endregion //HELPER
  }
}
