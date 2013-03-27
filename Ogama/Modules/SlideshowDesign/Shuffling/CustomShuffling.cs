// <copyright file="CustomShuffling.cs" company="FU Berlin">
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

  /// <summary>
  /// This class defines a specialized custom shuffling for the slideshow
  /// which cannot be achieved using ogamas standard shuffling options.
  /// </summary>
  /// <remarks>Imagine a slideshow with a root node that contains four child nodes.
  /// Each child nodes contains a list of 30 trials of the same type.
  /// No you want to build trial groups of four slides:
  /// one of each of the four sections, but randomized.
  /// That can be done with custom shuffling and will give you 30 groups each with four trials.</remarks>
  [Serializable]
  public class CustomShuffling
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
    /// Indicates the use of this shuffling.
    /// </summary>
    private bool useThisCustomShuffling;

    /// <summary>
    /// Indicates the parent node ID for the groups of slides that should be rearranged.
    /// </summary>
    private int shuffleSectionsParentNodeID;

    /// <summary>
    /// Indicates whether to shuffle the items in a section,
    /// before combining them to groups.
    /// </summary>
    private bool shuffleSectionItems;

    /// <summary>
    /// The number of items of a section in each group.
    /// </summary>
    private int numItemsOfSectionInGroup;

    /// <summary>
    /// Indicates whether to shuffle the newly created groups before presentation.
    /// </summary>
    private bool shuffleGroups;

    /// <summary>
    /// Indicates whether to shuffle the items in a group.
    /// </summary>
    private bool shuffleGroupItems;

    #endregion //FIELDS

    ///////////////////////////////////////////////////////////////////////////////
    // Construction and Initializing methods                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region CONSTRUCTION

    /// <summary>
    /// Initializes a new instance of the CustomShuffling class.
    /// </summary>
    public CustomShuffling()
    {
      this.shuffleSectionsParentNodeID = 1;
      this.shuffleSectionItems = true;
      this.numItemsOfSectionInGroup = 1;
      this.shuffleGroups = true;
      this.useThisCustomShuffling = false;
    }

    /// <summary>
    /// Initializes a new instance of the CustomShuffling class.
    /// </summary>
    /// <param name="newShuffleSectionsParentNodeID">Indicates the parent node ID for the groups of slides that should be rearranged.</param>
    /// <param name="newShuffleSectionItems">Indicates whether to shuffle the items in a section,
    /// before combining them to groups.</param>
    /// <param name="newNumItemsOfSectionInGroup">The number of items of a section in each group.</param>
    /// <param name="newShuffleGroups">Indicates whether to shuffle the newly created groups before presentation.</param>
    /// <param name="newShuffleGroupItems">Indicates whether to shuffle the items in a group.</param>
    public CustomShuffling(
      int newShuffleSectionsParentNodeID,
      bool newShuffleSectionItems,
      int newNumItemsOfSectionInGroup,
      bool newShuffleGroups,
      bool newShuffleGroupItems)
    {
      this.shuffleSectionsParentNodeID = newShuffleSectionsParentNodeID;
      this.shuffleSectionItems = newShuffleSectionItems;
      this.numItemsOfSectionInGroup = newNumItemsOfSectionInGroup;
      this.shuffleGroups = newShuffleGroups;
      this.shuffleGroupItems = newShuffleGroupItems;
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
    /// Gets or sets a value indicating whether this shuffling should be used.
    /// </summary>
    public bool UseThisCustomShuffling
    {
      get { return this.useThisCustomShuffling; }
      set { this.useThisCustomShuffling = value; }
    }

    /// <summary>
    /// Gets the parent node ID for the groups of slides that should be rearranged.
    /// </summary>
    public int ShuffleSectionsParentNodeID
    {
      get { return this.shuffleSectionsParentNodeID; }
    }

    /// <summary>
    /// Gets a value indicating whether to shuffle the items in a section,
    /// before combining them to groups.
    /// </summary>
    public bool ShuffleSectionItems
    {
      get { return this.shuffleSectionItems; }
    }

    /// <summary>
    /// Gets the number of items of a section in each group.
    /// </summary>
    public int NumItemsOfSectionInGroup
    {
      get { return this.numItemsOfSectionInGroup; }
    }

    /// <summary>
    /// Gets a value indicating whether to shuffle the newly created groups before presentation.
    /// </summary>
    public bool ShuffleGroups
    {
      get { return this.shuffleGroups; }
    }

    /// <summary>
    /// Gets a value indicating whether to shuffle the items in a group.
    /// </summary>
    public bool ShuffleGroupItems
    {
      get { return this.shuffleGroupItems; }
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
    #endregion //PRIVATEMETHODS

    ///////////////////////////////////////////////////////////////////////////////
    // Small helping Methods                                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region HELPER
    #endregion //HELPER
  }
}
