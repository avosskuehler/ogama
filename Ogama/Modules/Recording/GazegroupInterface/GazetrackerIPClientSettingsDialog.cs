// <copyright file="GazetrackerIPClientSettingsDialog.cs" company="FU Berlin">
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
 
namespace Ogama.Modules.Recording.GazegroupInterface
{
  using System;
  using System.Windows.Forms;

  /// <summary>
  /// Popup form to specify settings for the Gazetracker system which uses
  /// the udp connection not the ogama client.
  /// </summary>
  public partial class GazetrackerIPClientSettingsDialog : Form
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
    /// Saves the current <see cref="GazetrackerIPClientSetting"/>.
    /// </summary>
    private GazetrackerIPClientSetting gazetrackerIPClientSetting;
                
    #endregion //FIELDS

    ///////////////////////////////////////////////////////////////////////////////
    // Construction and Initializing methods                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region CONSTRUCTION

    /// <summary>
    /// Initializes a new instance of the GazetrackerIPClientSettingsDialog class.
    /// </summary>
    public GazetrackerIPClientSettingsDialog()
    {
      this.InitializeComponent();
    }

    #endregion //CONSTRUCTION

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Properties                                                       //
    ///////////////////////////////////////////////////////////////////////////////
    #region PROPERTIES

    /// <summary>
    /// Gets or sets the <see cref="GazetrackerIPClientSetting"/> class.
    /// </summary>
    /// <value>A <see cref="GazetrackerIPClientSetting"/> with the current settings.</value>
    public GazetrackerIPClientSetting GazetrackerIPClientSetting
    {
      get
      {
        return this.gazetrackerIPClientSetting;
      }

      set
      {
        this.gazetrackerIPClientSetting = value;
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
    /// The <see cref="Control.TextChanged"/> event handler for
    /// the <see cref="TextBox"/> <see cref="txbServerAddress"/>.
    /// Updates current alea settings with the new server address.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void TxbServerAddressTextChanged(object sender, EventArgs e)
    {
      this.gazetrackerIPClientSetting.GazeDataServerIPAddress = this.txbServerAddress.Text;
    }

    /// <summary>
    /// The <see cref="Control.TextChanged"/> event handler for
    /// the <see cref="TextBox"/> <see cref="txbServerPort"/>.
    /// Updates current alea settings with the new server port.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void TxbServerPortTextChanged(object sender, EventArgs e)
    {
      this.gazetrackerIPClientSetting.GazeDataServerPort = Convert.ToInt32(this.txbServerPort.Text);         
    }

    /// <summary>
    /// The <see cref="Control.TextChanged"/> event handler for
    /// the <see cref="TextBox"/> <see cref="txbClientAddress"/>.
    /// Updates current alea settings with the new client address.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void TxbClientAddressTextChanged(object sender, EventArgs e)
    {
      this.gazetrackerIPClientSetting.CommandServerIPAddress = this.txbClientAddress.Text;
    }

    /// <summary>
    /// The <see cref="Control.TextChanged"/> event handler for
    /// the <see cref="TextBox"/> <see cref="txbClientPort"/>.
    /// Updates current alea settings with the new client port.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void TxbClientPortTextChanged(object sender, EventArgs e)
    {
      this.gazetrackerIPClientSetting.CommandServerPort = Convert.ToInt32(this.txbClientPort.Text);
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
    /// Updates the forms UI with the new settings from the 
    /// <see cref="GazetrackerIPClientSetting"/> member.
    /// </summary>
    private void RealizeNewSettings()
    {
      this.txbServerAddress.Text = this.gazetrackerIPClientSetting.GazeDataServerIPAddress;
      this.txbServerPort.Text = this.gazetrackerIPClientSetting.GazeDataServerPort.ToString();
      this.txbClientAddress.Text = this.gazetrackerIPClientSetting.CommandServerIPAddress;
      this.txbClientPort.Text = this.gazetrackerIPClientSetting.CommandServerPort.ToString();
    }

    #endregion //METHODS

    ///////////////////////////////////////////////////////////////////////////////
    // Small helping Methods                                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region HELPER
    #endregion //HELPER
  }
}