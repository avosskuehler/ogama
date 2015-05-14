// <copyright file="MouseOnlySettingsDlg.cs" company="FU Berlin">
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

namespace Ogama.Modules.Recording.MouseOnlyInterface
{
  using System;
  using System.Windows.Forms;

  /// <summary>
  /// A Popup <see cref="Form"/> to specify settings for capturing only mouse
  /// movements.
  /// </summary>
  public partial class MouseOnlySettingsDlg : Form
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
    /// Saves the used <see cref="MouseOnlySetting"/>.
    /// </summary>
    private MouseOnlySetting mouseOnlySettings;

    #endregion //FIELDS

    ///////////////////////////////////////////////////////////////////////////////
    // Construction and Initializing methods                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region CONSTRUCTION

    /// <summary>
    /// Initializes a new instance of the MouseOnlySettingsDlg class.
    /// </summary>
    public MouseOnlySettingsDlg()
    {
      this.InitializeComponent();
    }

    #endregion //CONSTRUCTION

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Properties                                                       //
    ///////////////////////////////////////////////////////////////////////////////
    #region PROPERTIES

    /// <summary>
    /// Gets or sets the used <see cref="MouseOnlySetting"/> class.
    /// </summary>
    /// <value>A <see cref="MouseOnlySetting"/> with the settings for
    /// the mouse tracker.</value>
    public MouseOnlySetting MouseOnlySettings
    {
      get
      {
        return this.mouseOnlySettings;
      }

      set
      {
        this.mouseOnlySettings = value;
        this.RealizeNewSettings();
      }
    }

    #endregion //PROPERTIES

    ///////////////////////////////////////////////////////////////////////////////
    // Eventhandler                                                              //
    ///////////////////////////////////////////////////////////////////////////////
    #region EVENTS

    ///////////////////////////////////////////////////////////////////////////////
    // Eventhandler for UI, Menu, Buttons, Toolbars etc.                         //
    ///////////////////////////////////////////////////////////////////////////////
    #region WINDOWSEVENTHANDLER

    /// <summary>
    /// The <see cref="ComboBox.SelectedIndexChanged"/> event handler for
    /// the <see cref="ComboBox"/> <see cref="cbbSampleRate"/>.
    /// Reads the new sampling rate and updates the <see cref="MouseOnlySetting"/>
    /// member.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/>.</param>
    private void cbbSampleRate_SelectedIndexChanged(object sender, EventArgs e)
    {
      string rate = this.cbbSampleRate.SelectedItem.ToString();
      rate = rate.Replace("Hz", string.Empty);
      rate = rate.Trim();
      this.mouseOnlySettings.SampleRate = Convert.ToInt32(rate);
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
    // Inherited methods                                                         //
    ///////////////////////////////////////////////////////////////////////////////
    #region OVERRIDES
    #endregion //OVERRIDES

    ///////////////////////////////////////////////////////////////////////////////
    // Methods for doing main class job                                          //
    ///////////////////////////////////////////////////////////////////////////////
    #region METHODS

    /// <summary>
    /// Updates forms UI with new mouse only settings,
    /// which is only the sampling rate.
    /// </summary>
    private void RealizeNewSettings()
    {
      string rate = this.mouseOnlySettings.SampleRate.ToString().Trim();
      rate += " Hz";
      this.cbbSampleRate.SelectedItem = rate;
    }

    #endregion //METHODS

    ///////////////////////////////////////////////////////////////////////////////
    // Small helping Methods                                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region HELPER
    #endregion //HELPER
  }
}