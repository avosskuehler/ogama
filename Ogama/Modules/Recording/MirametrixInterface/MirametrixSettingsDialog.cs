// <copyright file="MirametrixSettingsDialog.cs" company="Mirametrix">
// ******************************************************
// OGAMA - open gaze and mouse analyzer 
// Copyright (C) 2010 Adrian Voßkühler  
// ------------------------------------------------------------------------
// This program is free software; you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation; either version 2 of the License, or (at your option) any later version.
// This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
// You should have received a copy of the GNU General Public License along with this program; if not, write to the Free Software Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA 02111-1307 USA
// **************************************************************
// </copyright>
// <author>Herval Yatchou</author>
// <email>herval.yatchou@tandemlaunchtech.com</email>

namespace Ogama.Modules.Recording.MirametrixInterface
{
    using System;
    using System.Windows.Forms;

    /// <summary>
    /// Popup form to specify settings for the Mirametrix system.
    /// </summary>
    public partial class MirametrixSettingsDialog : Form
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
        /// Saves the current <see cref="MirametrixSetting"/>.
        /// </summary>
        private MirametrixSetting _mirametrixSettings;

        #endregion //FIELDS

        ///////////////////////////////////////////////////////////////////////////////
        // Construction and Initializing methods                                     //
        ///////////////////////////////////////////////////////////////////////////////
        #region CONSTRUCTION

        /// <summary>
        /// Initializes a new instance of the MirametrixSettingsDialog class.
        /// </summary>
        public MirametrixSettingsDialog()
        {
            this.InitializeComponent();
        }

        #endregion //CONSTRUCTION

        ///////////////////////////////////////////////////////////////////////////////
        // Defining Properties                                                       //
        ///////////////////////////////////////////////////////////////////////////////
        #region PROPERTIES

        /// <summary>
        /// Gets or sets the <see cref="MirametrixSetting"/> class.
        /// </summary>
        /// <value>A <see cref="MirametrixSetting"/> with the current settings.</value>
        public MirametrixSetting MirametrixSetting
        {
            get
            {
                return this._mirametrixSettings;
            }

            set
            {
                this._mirametrixSettings = value;
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
        /// <see cref=MirametrixSetting"/> member.
        /// </summary>
        private void RealizeNewSettings()
        {
        }
        #endregion //METHODS

        ///////////////////////////////////////////////////////////////////////////////
        // Small helping Methods                                                     //
        ///////////////////////////////////////////////////////////////////////////////
        #region HELPER
        #endregion //HELPER
    }
}