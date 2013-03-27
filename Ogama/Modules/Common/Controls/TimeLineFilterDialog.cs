// <copyright file="TimeLineFilterDialog.cs" company="FU Berlin">
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

namespace Ogama.Modules.Common.Controls
{
  using System.Collections.Generic;
  using System.Windows.Forms;

  using Ogama.Modules.Common.TrialEvents;

  /// <summary>
  /// This <see cref="Form"/> shows a selection dialog to choose
  /// which <see cref="TrialEvent"/>s should be displayed in the 
  /// <see cref="TrialTimeLine"/>.
  /// </summary>
  public partial class TimeLineFilterDialog : Form
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
    /// Initializes a new instance of the TimeLineFilterDialog class.
    /// </summary>
    public TimeLineFilterDialog()
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
    /// Gets or sets the <see cref="List{String}"/>
    /// with the names of the events that should not be shown.
    /// </summary>
    public List<string> FilterString
    {
      get
      {
        List<string> filter = new List<string>();
        if (!this.chbMouseDown.Checked)
        {
          filter.Add("MouseDown");
        }

        if (!this.chbMouseUp.Checked)
        {
          filter.Add("MouseUp");
        }

        if (!this.chbKeys.Checked)
        {
          filter.Add("Key");
        }

        if (!this.chbFlash.Checked)
        {
          filter.Add("Flash");
        }

        if (!this.chbSound.Checked)
        {
          filter.Add("Sound");
        }

        if (!this.chbScroll.Checked)
        {
          filter.Add("Scroll");
        }

        return filter;
      }

      set
      {
        if (!value.Contains("MouseDown"))
        {
          this.chbMouseDown.Checked = true;
        }

        if (!value.Contains("MouseUp"))
        {
          this.chbMouseUp.Checked = true;
        }

        if (!value.Contains("Key"))
        {
          this.chbKeys.Checked = true;
        }

        if (!value.Contains("Flash"))
        {
          this.chbFlash.Checked = true;
        }

        if (!value.Contains("Sound"))
        {
          this.chbSound.Checked = true;
        }

        if (!value.Contains("Scroll"))
        {
          this.chbScroll.Checked = true;
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