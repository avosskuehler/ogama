// <copyright file="CustomShuffleDialog.cs" company="FU Berlin">
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

namespace Ogama.Modules.SlideshowDesign.Shuffling
{
  using System;
  using System.Windows.Forms;

  using Ogama.Modules.Common.SlideCollections;

  /// <summary>
  /// This dialog is intended to create a custom shuffling for
  /// the slideshow that is cannot be defined via the standard methods.
  /// Be careful using this, it is not very error safe at the moment.
  /// </summary>
  public partial class CustomShuffleDialog : Form
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
    /// The id of the selected node in the slideshow.
    /// </summary>
    private int selectedNodeID;

    #endregion //FIELDS

    ///////////////////////////////////////////////////////////////////////////////
    // Construction and Initializing methods                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region CONSTRUCTION

    /// <summary>
    /// Initializes a new instance of the CustomShuffleDialog class.
    /// </summary>
    public CustomShuffleDialog()
    {
      this.InitializeComponent();
    }

    #endregion //CONSTRUCTION

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Enumerations                                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region ENUMS
    #endregion ENUMS

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Properties                                                       //
    ///////////////////////////////////////////////////////////////////////////////
    #region PROPERTIES

    /// <summary>
    /// Gets or sets the <see cref="CustomShuffling"/> that is describes in this dialog.
    /// </summary>
    public CustomShuffling Shuffling
    {
      get { return this.GetShuffling(); }
      set { this.PopulateShuffling(value); }
    }

    /// <summary>
    /// Sets the <see cref="Slideshow"/> for which
    /// the shuffling is to be defined.
    /// </summary>
    public Slideshow Slideshow
    {
      set { this.PopulateTreeview(value); }
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
    /// The <see cref="RadioButton.CheckedChanged"/> for the 
    /// <see cref="RadioButton"/> <see cref="rdbEnableShuffling"/>.
    /// Shows or hides the properties for the shuffling when shuffling is enabled or disabled.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void rdbEnableShuffling_CheckedChanged(object sender, EventArgs e)
    {
      this.grpProperties.Visible = this.rdbEnableShuffling.Checked;
    }

    /// <summary>
    /// The <see cref="TreeView.AfterSelect"/> event handler for the
    /// <see cref="TreeView"/> <see cref="trvSlideshow"/>.
    /// Sets the parent node id from the selected node.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void trvSlideshow_AfterSelect(object sender, TreeViewEventArgs e)
    {
      this.txbParentNodeName.Text = e.Node.Text;
      this.selectedNodeID = Convert.ToInt32(e.Node.Name);
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
    #region PRIVATEMETHODS

    /// <summary>
    /// This method populates the dialogs <see cref="TreeView"/>
    /// with the given slideshow.
    /// </summary>
    /// <param name="slideshow">The <see cref="Slideshow"/> to be used.</param>
    private void PopulateTreeview(Slideshow slideshow)
    {
      Slideshow clone = (Slideshow)slideshow.Clone();
      clone.Remove();
      this.trvSlideshow.BeginUpdate();
      this.trvSlideshow.Nodes.Clear();
      this.trvSlideshow.Nodes.Add(clone);
      this.trvSlideshow.EndUpdate();
      this.trvSlideshow.ExpandAll();
    }

    /// <summary>
    /// This method populates the dialog  with the given <see cref="CustomShuffling"/>.
    /// </summary>
    /// <param name="shuffling">The <see cref="CustomShuffling"/>
    /// that is designed in this dialog.</param>
    private void PopulateShuffling(CustomShuffling shuffling)
    {
      this.chbShuffleGroups.Checked = shuffling.ShuffleGroups;
      this.chbShuffleInsideGroups.Checked = shuffling.ShuffleGroupItems;
      this.chbShuffleSections.Checked = shuffling.ShuffleSectionItems;
      this.nudCountSectionItemsInGroup.Value = shuffling.NumItemsOfSectionInGroup;
      this.rdbEnableShuffling.Checked = shuffling.UseThisCustomShuffling;
    }

    /// <summary>
    /// This method returns the <see cref="CustomShuffling"/> that is
    /// defined in this dialog.
    /// </summary>
    /// <returns>The <see cref="CustomShuffling"/> that is
    /// defined in this dialog.</returns>
    private CustomShuffling GetShuffling()
    {
      CustomShuffling newShuffling = new CustomShuffling(
        this.selectedNodeID,
        this.chbShuffleSections.Checked,
        (int)this.nudCountSectionItemsInGroup.Value,
        this.chbShuffleGroups.Checked,
        this.chbShuffleInsideGroups.Checked);

      newShuffling.UseThisCustomShuffling = this.rdbEnableShuffling.Checked;
      return newShuffling;
    }

    #endregion //PRIVATEMETHODS

    ///////////////////////////////////////////////////////////////////////////////
    // Small helping Methods                                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region HELPER
    #endregion //HELPER
  }
}