// <copyright file="HowToActivateAsl.cs" company="FU Berlin">
// ******************************************************
// OGAMA - open gaze and mouse analyzer 
// Copyright (C) 2013 Dr. Adrian Voßkühler  
// ------------------------------------------------------------------------
// This program is free software; you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation; either version 2 of the License, or (at your option) any later version.
// This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
// You should have received a copy of the GNU General Public License along with this program; if not, write to the Free Software Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA 02111-1307 USA
// **************************************************************
// </copyright>
// <author></author>
// <email></email>

namespace Ogama.Modules.Recording.ASLInterface
{
  using System;
  using System.Windows.Forms;

  // for GetLang

    /// <summary>
    /// A small popup <see cref="Form"/> for showing a dialog on how to
    /// install the ASL recording option.
    /// </summary>
    public partial class HowToActivateAsl : Form
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
        /// Initializes a new instance of the <see cref="HowToActivateAsl"/> class.
        /// </summary>
        public HowToActivateAsl()
        {
            // call the Windows Form Designer generated method
            this.InitializeComponent();

            // call the additional local initialize method 
            this.CustomInitialize();
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
        /// the <see cref="PictureBox"/> <see cref="pcbAslLogo"/>.
        /// User clicked the ASL logo, so open ASL website.
        /// </summary>
        /// <param name="sender">Source of the event.</param>
        /// <param name="e">An empty <see cref="EventArgs"/></param>
        private void pcbAslLogo_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://asleyetracking.com/Site/");
        }

        /// <summary>    
        /// User clicked link, so open ASL website.
        /// </summary>
        /// <param name="sender">Source of the event.</param>
        /// <param name="e">An empty <see cref="EventArgs"/></param>
        private void llbAslWebsite_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://asleyetracking.com/Site/");
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
        /// Sets up Asl system settings
        /// </summary>
        private void CustomInitialize()
        {
            this.Text = "HowTo : Activate Asl recording...";
            this.label1.Text = "To enable the recording with Asl Eye Tracker please perform the following steps :";
            this.label3.Text = "1. Purchase an ASL model 5000 Eye Tracker control unit and his softwares." + Environment.NewLine +
                "2. Make all good connections between this control unit, the Interface PC, the camera, the two monitors and this Display PC." + Environment.NewLine +
                "3. Install the softwares on the Interface PC." + Environment.NewLine + Environment.NewLine +
                "(Points of gaze information come from the Eye Tracker Controller via a serial connection)" + Environment.NewLine +
                "4. Install the ASLSerialOutLib2.dll library in C:\\Program Files\\ASL Eye Tracker\\LL\\ on this Display Computer" + Environment.NewLine +
                "5. and register it by this command C:\\WINDOWS\\system32\\regsvr32.exe C:\\Program Files\\ASL Eye Tracker\\DLL\\ASLSerialOutLib2.dll" + Environment.NewLine +
                "6. Start the recording module." + Environment.NewLine +
                "7. Click the \"select tracking device\" button and check the Asl checkbox."
                + Environment.NewLine + Environment.NewLine +
                "If you followed these steps, a new tab called \"Asl\" will appear in the recording module.";
            this.llbAslWebsite.Text = "ASL website";
            this.btnOK.Text = "&OK";
        }

        #endregion //METHODS

        ///////////////////////////////////////////////////////////////////////////////
        // Small helping Methods                                                     //
        ///////////////////////////////////////////////////////////////////////////////
        #region HELPER
        #endregion //HELPER
    }
}
