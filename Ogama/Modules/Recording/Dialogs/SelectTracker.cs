// <copyright file="SelectTracker.cs" company="FU Berlin">
// ******************************************************
// OGAMA - open gaze and mouse analyzer 
// Copyright (C) 2010 Adrian Voßkühler  
// ------------------------------------------------------------------------
// This program is free software; you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation; either version 2 of the License, or (at your option) any later version.
// This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
// You should have received a copy of the GNU General Public License along with this program; if not, write to the Free Software Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA 02111-1307 USA
// **************************************************************
// </copyright>
// <author>Adrian Voßkühler</author>
// <email>adrian.vosskuehler@fu-berlin.de</email>

namespace Ogama.Modules.Recording
{
  using System;
  using System.Collections.Generic;
  using System.ComponentModel;
  using System.Data;
  using System.Drawing;
  using System.Text;
  using System.Windows.Forms;
  using Ogama.ExceptionHandling;
  using Ogama.MainWindow;

  /// <summary>
  /// A small popup <see cref="Form"/> for showing a dialog 
  /// to select the available recording devices.
  /// </summary>
  public partial class SelectTracker : Form
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
    /// Initializes a new instance of the SelectTracker class.
    /// </summary>
    public SelectTracker()
    {
      this.InitializeComponent();
    }

    #endregion //CONSTRUCTION

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Properties                                                       //
    ///////////////////////////////////////////////////////////////////////////////
    #region PROPERTIES

    /// <summary>
    /// Gets the selected available hardware devices.
    /// </summary>
    public HardwareTracker SelectedTracker
    {
      get
      {
        HardwareTracker returnValue = HardwareTracker.None;
        if (this.chbAlea.Checked)
        {
          returnValue |= HardwareTracker.Alea;
        }

        if (this.chbMouseOnly.Checked)
        {
          returnValue |= HardwareTracker.MouseOnly;
        }

        if (this.chbTobii.Checked)
        {
          returnValue |= HardwareTracker.Tobii;
        }

        if (this.chbSMI.Checked)
        {
          returnValue |= HardwareTracker.SMI;
        }

        if (this.chbITU.Checked)
        {
          returnValue |= HardwareTracker.ITU;
        }

        if (this.chbItuPS3.Checked)
        {
          returnValue |= HardwareTracker.ITUPS3;
        }

        return returnValue;
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
    /// <see cref="Form.Load"/> event handler. 
    /// Checks for available tracker interfaces.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void SelectTracker_Load(object sender, EventArgs e)
    {
      MessageBox.Show("ITUGazeTrackerBase");
      string error;

      string ituDefaultText = "The ITU GazeTracker application which uses a webcam as " +
        "an eye tracker and can be used in both remote and head-mounted setup."
        + Environment.NewLine;

      if (!Ogama.Modules.Recording.ITUGazeTracker.ITUGazeTrackerBase.IsAvailable(out error))
      {
        this.chbITU.Enabled = false;
        this.chbITU.Checked = false;
        this.chbITU.Text = ituDefaultText +
          "Status: " + error;
      }
      else
      {
        this.chbITU.Text = ituDefaultText +
          "Status: GazeTracker found a camera device.";
      }

      MessageBox.Show("ITUPS3GazeTracker");
      string ituPS3DefaultText = "The ITU GazeTracker application with the PlayStationEye3 camera."
        + Environment.NewLine;

      if (!Ogama.Modules.Recording.ITUGazeTracker.ITUPS3GazeTracker.IsAvailable(out error))
      {
        this.chbItuPS3.Enabled = false;
        this.chbItuPS3.Checked = false;
        this.chbItuPS3.Text = ituPS3DefaultText +
          "Status: " + error;
      }
      else
      {
        this.chbItuPS3.Text = ituPS3DefaultText +
          "Status: GazeTracker found a PlayStation3 Eye device.";
      }

      MessageBox.Show("AleaTracker");
      string aleaDefaultText = "The alea technologies IG-30 Pro Eyetracking-System. " +
          "Needs to have Intelligaze Software 1.2 to be installed." + Environment.NewLine;
      if (!Ogama.Modules.Recording.Alea.AleaTracker.IsAvailable(out error))
      {
        this.chbAlea.Enabled = false;
        this.chbAlea.Checked = false;
        this.pcbAlea.Enabled = false;
        this.chbAlea.Text = aleaDefaultText +
          "Status: " + error;
      }
      else
      {
        this.chbAlea.Text = aleaDefaultText +
          "Status: Intelligaze found.";
      }

      MessageBox.Show("TobiiTracker");
      string tobiiDefaultText = "The Tobii technologies T60,T120,X120 gaze tracker series." +
          "Needs to have a purchased Tobii SDK to be installed on the computer." + Environment.NewLine;
#if TOBII
      if (!Ogama.Modules.Recording.Tobii.TobiiTracker.IsAvailable(out error))
      {
        this.chbTobii.Enabled = false;
        this.chbTobii.Checked = false;
        this.pcbTobii.Enabled = false;
        this.chbTobii.Text = tobiiDefaultText +
          "Status: " + error;
      }
      else
      {
        this.chbTobii.Text = tobiiDefaultText +
          "Status: Tobii SDK found.";
      }
#else
      this.chbTobii.Enabled = false;
      this.chbTobii.Checked = false;
      this.pcbTobii.Enabled = false;
      this.chbTobii.Text = tobiiDefaultText +
        "Status: This version of OGAMA has no Tobii support (TOBII compiler flag not set)."
        + Environment.NewLine;
#endif
    }

    /// <summary>
    /// The <see cref="Form.FormClosing"/> event handler
    /// which checks for a valid tracking device selection
    /// and otherwise raises a warning message.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">The <see cref="FormClosingEventArgs"/>
    /// with the event data.</param>
    private void SelectTracker_FormClosing(object sender, FormClosingEventArgs e)
    {
      if (this.SelectedTracker == HardwareTracker.None && this.DialogResult == DialogResult.OK)
      {
        e.Cancel = true;
        string message = "At least one tracking device should be selected." +
          Environment.NewLine + "If no gaze tracking hardware is available " +
          "you can use the mouse only tracker.";
        ExceptionMethods.ProcessMessage("Please select a device ...", message);
      }
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler for
    /// the <see cref="PictureBox"/> <see cref="pcbTobii"/>.
    /// User clicked the tobii logo,
    /// so open tobii technologies website.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void pcbTobii_Click(object sender, EventArgs e)
    {
      System.Diagnostics.Process.Start("http://www.tobii.com");
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler for
    /// the <see cref="PictureBox"/> <see cref="pcbAlea"/>.
    /// User clicked the alea logo,
    /// so open alea technologies website.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void pcbAlea_Click(object sender, EventArgs e)
    {
      System.Diagnostics.Process.Start("http://www.alea-technologies.com");
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler for
    /// the <see cref="PictureBox"/> <see cref="pcbSMI"/>.
    /// User clicked the SMI logo,
    /// so open senso motoric instruments website.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void pcbSMI_Click(object sender, EventArgs e)
    {
      System.Diagnostics.Process.Start("http://www.smivision.com");
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler for
    /// the <see cref="PictureBox"/> <see cref="pcbITU"/>.
    /// User clicked the ITU logo,
    /// so open GAZE GROUP website.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void pcbITU_Click(object sender, EventArgs e)
    {
      System.Diagnostics.Process.Start("http://www.gazegroup.org/downloads/23-gazetracker");
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler for
    /// the <see cref="PictureBox"/> <see cref="pcbPS3"/>.
    /// User clicked the PS3 logo,
    /// so open eyewriter and codelaboratories website.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void pcbPS3_Click(object sender, EventArgs e)
    {
      System.Diagnostics.Process.Start("http://www.eyewriter.org");
      System.Diagnostics.Process.Start("http://codelaboratories.com");
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler
    /// for the <see cref="PictureBox"/> <see cref="pcbHelpTobii"/>
    /// Displays instructions to activate tobii recording.
    /// </summary>
    /// <param name="sender">Source of the event</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void pcbHelpTobii_Click(object sender, EventArgs e)
    {
      HowToActivateTobii objActivateTobii = new HowToActivateTobii();
      objActivateTobii.ShowDialog();
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler
    /// for the <see cref="PictureBox"/> <see cref="pcbHelpAlea"/>
    /// Displays instructions to activate alea recording.
    /// </summary>
    /// <param name="sender">Source of the event</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void pcbHelpAlea_Click(object sender, EventArgs e)
    {
      HowToActivateAlea objActivateAlea = new HowToActivateAlea();
      objActivateAlea.ShowDialog();
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler
    /// for the <see cref="PictureBox"/> <see cref="pcbHelpSMI"/>
    /// Displays instructions to activate SMI iViewX recording.
    /// </summary>
    /// <param name="sender">Source of the event</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void pcbHelpSMI_Click(object sender, EventArgs e)
    {
      HowToActivateSMI objActivateSMI = new HowToActivateSMI();
      objActivateSMI.ShowDialog();
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler
    /// for the <see cref="PictureBox"/> <see cref="pcbHelpITUPS3"/>
    /// Displays instructions to enable gaze tracking with the ITU
    /// GazeTracker and the PlayStation3 Eye camera.
    /// </summary>
    /// <param name="sender">Source of the event</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void pcbHelpITUPS3_Click(object sender, EventArgs e)
    {
      HowToActivateITUPS3 objActivateITUPS3 = new HowToActivateITUPS3();
      objActivateITUPS3.ShowDialog();
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler
    /// for the <see cref="PictureBox"/> <see cref="pcbHelpITU"/>
    /// Displays instructions to enable gaze tracking with the ITU
    /// GazeTracker and a webcam.
    /// </summary>
    /// <param name="sender">Source of the event</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void pcbHelpITU_Click(object sender, EventArgs e)
    {
      HowToActivateITU objActivateITU = new HowToActivateITU();
      objActivateITU.ShowDialog();
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