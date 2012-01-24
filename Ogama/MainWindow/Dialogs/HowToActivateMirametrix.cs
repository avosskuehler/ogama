﻿// <copyright file="HowToActivateMirametrix.cs" company="FU Berlin">
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

namespace Ogama.MainWindow.Dialogs
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Linq;
    using System.Text;
    using System.Windows.Forms;

    /// <summary>
    /// /// A small popup <see cref="Form"/> for showing a dialog on how to
    /// install the mirametrix recording option.
    /// </summary>
    public partial class HowToActivateMirametrix : Form
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
        /// Initializes a new instance of the HowToActivateMirametrix class.
        /// </summary>
        public HowToActivateMirametrix()
        {
            this.InitializeComponent();
        }

        #endregion //CONSTRUCTION
        ///////////////////////////////////////////////////////////////////////////////
        // Defining Properties                                                       //
        ///////////////////////////////////////////////////////////////////////////////
        #region PROPERTIES
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
        /// The <see cref="Control.Click"/> event handler for
        /// the <see cref="PictureBox"/> <see cref="pcbTobiiLogo"/>.
        /// User clicked the tobii logo,
        /// so open tobii technologies website.
        /// </summary>
        /// <param name="sender">Source of the event.</param>
        /// <param name="e">An empty <see cref="EventArgs"/></param>
        private void pcbMirametrixLogo_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.mirametrix.com");
        }

        /// <summary>
        /// The <see cref="Control.Click"/> event handler for
        /// the <see cref="LinkLabel"/> <see cref="linkMirametrixSupportText"/>.
        /// User clicked the text "www.mirametrix.com/support",
        /// so open Mirametrix support website.
        /// </summary>
        /// <param name="sender">Source of the event</param>
        /// <param name="e">An empty</param>
        private void linkMirametrixSupportText_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.mirametrix.com/support");
        }

        /// <summary>
        /// The <see cref="Control.Click"/> event handler for
        /// the <see cref="LinkLabel"/> <see cref="linkMirametrixSupportText"/>.
        /// User clicked the text "www.mirametrix.com",
        /// so open Mirametrix technologies website.
        /// </summary>
        /// <param name="sender">Source of the event</param>
        /// <param name="e">An empty</param>
        private void linkMirametrixText_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.mirametrix.com");
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
        #endregion //METHODS

        ///////////////////////////////////////////////////////////////////////////////
        // Small helping Methods                                                     //
        ///////////////////////////////////////////////////////////////////////////////
        #region HELPER
        #endregion //HELPER
    }
}
