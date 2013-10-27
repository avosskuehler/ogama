// <copyright file="SelectTracker.cs" company="FU Berlin">
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

namespace Ogama.Modules.Recording.Dialogs
{
  using System;
  using System.Drawing;
  using System.Windows.Forms;

  using Ogama.ExceptionHandling;
  using Ogama.MainWindow.Dialogs;
  using Ogama.Modules.Recording.AleaInterface;
  using Ogama.Modules.Recording.ASLInterface;
  using Ogama.Modules.Recording.EyeTechInterface;
  using Ogama.Modules.Recording.GazegroupInterface;
  using Ogama.Modules.Recording.MirametrixInterface;
  using Ogama.Modules.Recording.GazepointInterface;
  using Ogama.Modules.Recording.SMIInterface;
  using Ogama.Modules.Recording.TobiiInterface;
  using Ogama.Modules.Recording.TrackerBase;

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

    /// <summary>
    /// Provides an <see cref="Timer"/> which updates the tracker status
    /// of the connected devices every second.
    /// </summary>
    private readonly Timer eyetrackerUpdateTimer;

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
      this.eyetrackerUpdateTimer = new Timer { Interval = 1000, Enabled = true };
      this.eyetrackerUpdateTimer.Tick += this.EyetrackerUpdateTimerTick;
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
        var returnValue = HardwareTracker.None;
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

        if (this.chbGazetrackerIPClient.Checked)
        {
          returnValue |= HardwareTracker.GazetrackerIPClient;
        }

        if (this.chbGazetrackerDirectClient.Checked)
        {
          returnValue |= HardwareTracker.GazetrackerDirectClient;
        }

        if (this.chbASL.Checked)
        {
          returnValue |= HardwareTracker.ASL; // = 64
        }

        if (this.chbMirametrix.Checked)
        {
          returnValue |= HardwareTracker.Mirametrix;
        }

        if (this.chbGazepoint.Checked)
        {
          returnValue |= HardwareTracker.Gazepoint;
        }

        if (this.chbEyetech.Checked)
        {
          returnValue |= HardwareTracker.EyeTech;
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
    private void SelectTrackerLoad(object sender, EventArgs e)
    {
      this.UpdateTrackerStatus();
    }

    /// <summary>
    /// Updates the connected tracker status.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void EyetrackerUpdateTimerTick(object sender, EventArgs e)
    {
      this.UpdateTrackerStatus();
    }

    /// <summary>
    /// The <see cref="Form.FormClosing"/> event handler
    /// which checks for a valid tracking device selection
    /// and otherwise raises a warning message.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">The <see cref="FormClosingEventArgs"/>
    /// with the event data.</param>
    private void SelectTrackerFormClosing(object sender, FormClosingEventArgs e)
    {
      if (this.SelectedTracker == HardwareTracker.None && this.DialogResult == DialogResult.OK)
      {
        e.Cancel = true;
        string message = "At least one tracking device should be selected." +
          Environment.NewLine + "If no gaze tracking hardware is available " +
          "you can use the mouse only tracker.";
        ExceptionMethods.ProcessMessage("Please select a device ...", message);
      }
      else
      {
        this.eyetrackerUpdateTimer.Stop();
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
    private void PcbTobiiClick(object sender, EventArgs e)
    {
      System.Diagnostics.Process.Start("http://www.tobii.com");
    }

    /// <summary>
    /// The event handler for the User clicked the mirametrix logo, so open mirametrix website
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void PcbMirametrixClick(object sender, EventArgs e)
    {
      System.Diagnostics.Process.Start("http://www.mirametrix.com");
    }

    /// <summary>
    /// The event handler for the User clicked the gazepoint logo, so open mirametrix website
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void PcbGazepointClick(object sender, EventArgs e)
    {
      System.Diagnostics.Process.Start("http://www.gazept.com");
    }


    /// <summary>
    /// The <see cref="Control.Click"/> event handler for
    /// the <see cref="PictureBox"/> <see cref="pcbAlea"/>.
    /// User clicked the alea logo,
    /// so open alea technologies website.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void PcbAleaClick(object sender, EventArgs e)
    {
      System.Diagnostics.Process.Start("http://www.alea-technologies.com");
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler for
    /// the <see cref="PictureBox"/> <see cref="pcbEyetech"/>.
    /// User clicked the Eye Tech logo,
    /// so open eye tech website.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void PcbEyetechClick(object sender, EventArgs e)
    {
      System.Diagnostics.Process.Start("https://www.eyetechds.com");
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler for
    /// the <see cref="PictureBox"/> <see cref="pcbSMI"/>.
    /// User clicked the SMI logo,
    /// so open senso motoric instruments website.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void PcbSMIClick(object sender, EventArgs e)
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
    private void PcbITUClick(object sender, EventArgs e)
    {
      System.Diagnostics.Process.Start("http://www.gazegroup.org/downloads/23-gazetracker");
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler
    /// for the <see cref="PictureBox"/> <see cref="pcbHelpTobii"/>
    /// Displays instructions to activate tobii recording.
    /// </summary>
    /// <param name="sender">Source of the event</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void PcbHelpTobiiClick(object sender, EventArgs e)
    {
      var objActivateTobii = new HowToActivateTobii();
      objActivateTobii.ShowDialog();
    }

    /// <summary>
    /// The event handler for the User clicked the mirametrix logo, so open mirametrix website
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void PcbHelpMirametrixClick(object sender, EventArgs e)
    {
      var objActivateMirametrix = new HowToActivateMirametrix();
      objActivateMirametrix.ShowDialog();
    }

    /// <summary>
    /// The event handler for the User clicked the gazepoint help
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void PcbHelpGazepointClick(object sender, EventArgs e)
    {
      var objActivateGazepoint = new HowToActivateGazepoint();
      objActivateGazepoint.ShowDialog();
    }


    /// <summary>
    /// The <see cref="Control.Click"/> event handler for
    /// the <see cref="PictureBox"/> <see cref="pcbASL"/>.
    /// User clicked the ASL logo,
    /// so open senso motoric instruments website.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void PcbAslClick(object sender, EventArgs e)
    {
      System.Diagnostics.Process.Start("http://asleyetracking.com/site/");
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler
    /// for the <see cref="PictureBox"/> <see cref="pcbHelpAlea"/>
    /// Displays instructions to activate alea recording.
    /// </summary>
    /// <param name="sender">Source of the event</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void PcbHelpAleaClick(object sender, EventArgs e)
    {
      var objActivateAlea = new HowToActivateAlea();
      objActivateAlea.ShowDialog();
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler
    /// for the <see cref="PictureBox"/> <see cref="pcbHelpSMI"/>
    /// Displays instructions to activate SMI iViewX recording.
    /// </summary>
    /// <param name="sender">Source of the event</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void PcbHelpSMIClick(object sender, EventArgs e)
    {
      var objActivateSMI = new HowToActivateSMI();
      objActivateSMI.ShowDialog();
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler
    /// for the <see cref="PictureBox"/> <see cref="pcbHelpASL"/>
    /// Displays instructions to activate ASL recording.
    /// </summary>
    /// <param name="sender">Source of the event</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void PcbHelpAslClick(object sender, EventArgs e)
    {
      var objActivateAsl = new HowToActivateAsl();
      objActivateAsl.ShowDialog();
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler
    /// for the <see cref="PictureBox"/> <see cref="pcbHelpGazetrackerIPClient"/>
    /// Displays instructions to enable gaze tracking with the GazeTracker and a webcam.
    /// </summary>
    /// <param name="sender">Source of the event</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void PcbHelpGazetrackerClientClick(object sender, EventArgs e)
    {
      var objActivateITU = new HowToActivateGazetracker();
      objActivateITU.ShowDialog();
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler
    /// for the <see cref="PictureBox"/> <see cref="pcbHelpGazetrackerDirectClient"/>
    /// Displays instructions to enable gaze tracking with the GazeTracker and a webcam.
    /// </summary>
    /// <param name="sender">Source of the event</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void PcbHelpGazetrackerDirectClientClick(object sender, EventArgs e)
    {
      var objHowToActivateGazetracker = new HowToActivateGazetracker();
      objHowToActivateGazetracker.ShowDialog();
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
    /// Updates the status of all connected devices.
    /// </summary>
    private void UpdateTrackerStatus()
    {
      this.UpdateGazetrackerDirectClientStatus();
      this.UpdateAleaTrackStatus();
      this.UpdateTobiiStatus();
      this.UpdateASLStatus();
      this.UpdateMirametrixStatus();
      this.UpdateGazepointStatus();
      this.UpdateEyeTechStatus();
      this.UpdateSMITrackStatus();
    }

    /// <summary>
    /// Support for IsAvailable delegate
    /// </summary>
    private delegate TrackerStatus IsAvailable(out string error);

    /// <summary>
    /// Updates the status of the Tobii tracking devices
    /// </summary>
    private void UpdateTobiiStatus()
    {
      this.SetStatus(TobiiTracker.IsAvailable, this.chbTobii, this.pcbTobiiStatus, this.lblTobiiStatus);
    }

    /// <summary>
    /// Updates the status of the Mirametrix tracking devices
    /// </summary>
    private void UpdateMirametrixStatus()
    {
      this.SetStatus(MirametrixTracker.IsAvailable, this.chbMirametrix, this.pcbMirametrixStatus, this.lblMirametrixStatus);
    }

    /// <summary>
    /// Updates the status of the Gazepoint tracking devices
    /// </summary>

    private void UpdateGazepointStatus()
    {
      this.SetStatus(GazepointTracker.IsAvailable, this.chbGazepoint, this.pcbGazepointStatus, this.lblGazepointStatus);
    }

    /// <summary>
    /// Updates the status of the Eyetech tracking devices
    /// </summary>
    private void UpdateEyeTechStatus()
    {
      this.SetStatus(EyeTechTracker.IsAvailable, this.chbEyetech, this.pcbEyetechStatus, this.lblEyetechStatus);
    }

    /// <summary>
    /// Updates the status of the ASL tracking devices
    /// </summary>
    private void UpdateASLStatus()
    {
      this.SetStatus(AslTracker.IsAvailable, this.chbASL, this.pcbASLStatus, this.lblASLStatus);
    }

    /// <summary>
    /// Updates the status of the Alea tracking devices
    /// </summary>
    private void UpdateAleaTrackStatus()
    {
      this.SetStatus(AleaTracker.IsAvailable, this.chbAlea, this.pcbAleaStatus, this.lblAleaStatus);
    }

    /// <summary>
    /// Updates the status of the SMI tracking devices
    /// </summary>
    private void UpdateSMITrackStatus()
    {
      this.SetStatus(SMITracker.IsAvailable, this.chbSMI, this.pcbSMIStatus, this.lblSMIStatus);
    }

    /// <summary>
    /// Updates the status of the gazetracker direct client devices.
    /// </summary>
    private void UpdateGazetrackerDirectClientStatus()
    {
      string error;

      switch (GazetrackerDirectClientTracker.IsAvailable(out error))
      {
        case TrackerStatus.Available:
          this.chbGazetrackerDirectClient.Enabled = true;
          break;
        case TrackerStatus.NotAvailable:
        case TrackerStatus.Undetermined:
        case TrackerStatus.None:
          this.chbGazetrackerDirectClient.Enabled = false;
          this.chbGazetrackerDirectClient.Checked = false;
          break;
      }
    }

    #endregion //METHODS

    private void GrpTobiiEnter(object sender, EventArgs e)
    {
      this.UpdateInfobar(TobiiTracker.IsAvailable, Properties.Resources.TobiiPhoto64, Properties.Resources.TobiiLogo);
    }

    private void GrpAleaEnter(object sender, EventArgs e)
    {
      this.UpdateInfobar(AleaTracker.IsAvailable, Properties.Resources.AleaFoto64, Properties.Resources.AleaLogo);
    }

    private void GrpSMIEnter(object sender, EventArgs e)
    {
      this.UpdateInfobar(SMITracker.IsAvailable, Properties.Resources.SMIFoto64, Properties.Resources.SMILogo);
    }

    private void GrpASLEnter(object sender, EventArgs e)
    {
      this.UpdateInfobar(AslTracker.IsAvailable, Properties.Resources.ASLFoto64, Properties.Resources.ASLLogo);
    }

    private void GrpMirametrixEnter(object sender, EventArgs e)
    {
      this.UpdateInfobar(MirametrixTracker.IsAvailable, Properties.Resources.MirametrixFoto64, Properties.Resources.MirametrixLogo);
    }

    private void GrpEyetechEnter(object sender, EventArgs e)
    {
      this.UpdateInfobar(EyeTechTracker.IsAvailable, Properties.Resources.EyetechFoto64, Properties.Resources.EyeTechLogo);
    }

    private void GrpGazepointEnter(object sender, EventArgs e)
    {
      this.UpdateInfobar(GazepointTracker.IsAvailable, Properties.Resources.GazepointFoto64, Properties.Resources.GazepointLogo);
    }

    ///////////////////////////////////////////////////////////////////////////////
    // Small helping Methods                                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region HELPER

    /// <summary>
    /// This method updates the infobar with the given tracker information.
    /// </summary>
    /// <param name="availableMethod">The IsAvailable method of the tracker.</param>
    /// <param name="image">The bitmap of the tracker</param>
    /// <param name="logo">The bitmap of the logo</param>
    private void UpdateInfobar(
      IsAvailable availableMethod,
      Image image,
      Image logo)
    {
      this.pcbSelectedTrackerLogo.Image = logo;
      this.pcbSelectedTrackerImage.Image = image;
      string error;
      availableMethod(out error);
      this.lblSelectedStatus.Text = error;
    }

    /// <summary>
    /// Updates the status of the given tracking group box.
    /// </summary>
    /// <param name="availableMethod">The IsAvailable method of the tracker.</param>
    /// <param name="checkBox">The checkbox to select the tracker.</param>
    /// <param name="pcbStatus">The tracker status image picture box.</param>
    /// <param name="lblStatus">The tracker status label.</param>
    private void SetStatus(IsAvailable availableMethod, CheckBox checkBox, PictureBox pcbStatus, Label lblStatus)
    {
      string error;
      switch (availableMethod(out error))
      {
        case TrackerStatus.Available:
          checkBox.Enabled = true;
          checkBox.Enabled = true;
          pcbStatus.Image = Properties.Resources.Valid16;
          this.toolTip1.SetToolTip(pcbStatus, "Tracker is available for recording");
          lblStatus.Text = "Available";
          break;
        case TrackerStatus.None:
        case TrackerStatus.NotAvailable:
          checkBox.Enabled = false;
          checkBox.Checked = false;
          pcbStatus.Image = Properties.Resources.Error16;
          this.toolTip1.SetToolTip(pcbStatus, "Tracker is not available for recording");
          lblStatus.Text = "Not Available";
          break;
        case TrackerStatus.Undetermined:
          checkBox.Enabled = true;
          pcbStatus.Image = Properties.Resources.Warning16;
          this.toolTip1.SetToolTip(pcbStatus, "Tracker status cannot be resolved");
          lblStatus.Text = "Undetermined";
          break;
      }
    }

    #endregion //HELPER


  }
}