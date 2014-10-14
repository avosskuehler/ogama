// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SelectTracker.cs" company="Freie Universität Berlin">
//   OGAMA - open gaze and mouse analyzer 
//   Copyright (C) 2014 Dr. Adrian Voßkühler  
//   Licensed under GPL V3
// </copyright>
// <author>Adrian Voßkühler</author>
// <email>adrian@ogama.net</email>
// <summary>
//   A small popup <see cref="Form" /> for showing a dialog
//   to select the available recording devices.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Ogama.Modules.Recording.Dialogs
{
  using System;
  using System.Drawing;
  using System.Windows.Forms;

  using Ogama.ExceptionHandling;
  using Ogama.Modules.Recording.AleaInterface;
  using Ogama.Modules.Recording.ASLInterface;
  using Ogama.Modules.Recording.EyeTechInterface;
  using Ogama.Modules.Recording.GazegroupInterface;
  using Ogama.Modules.Recording.GazepointInterface;
  using Ogama.Modules.Recording.HaythamInterface;
  using Ogama.Modules.Recording.MirametrixInterface;
  using Ogama.Modules.Recording.MouseOnlyInterface;
  using Ogama.Modules.Recording.SmartEyeInterface;
  using Ogama.Modules.Recording.SMIInterface.iViewX;
  using Ogama.Modules.Recording.SMIInterface.RedM;
  using Ogama.Modules.Recording.TheEyeTribeInterface;
  using Ogama.Modules.Recording.TobiiInterface;
  using Ogama.Modules.Recording.TrackerBase;

  /// <summary>
  ///   A small popup <see cref="Form" /> for showing a dialog
  ///   to select the available recording devices.
  /// </summary>
  public partial class SelectTracker : Form
  {
    #region Fields

    /// <summary>
    ///   Provides an <see cref="Timer" /> which updates the tracker status
    ///   of the connected devices every second.
    /// </summary>
    private readonly Timer eyetrackerUpdateTimer;

    #endregion

    #region Constructors and Destructors

    /// <summary>
    ///   Initializes a new instance of the SelectTracker class.
    /// </summary>
    public SelectTracker()
    {
      this.InitializeComponent();
      this.eyetrackerUpdateTimer = new Timer { Interval = 1000, Enabled = true };
      this.eyetrackerUpdateTimer.Tick += this.EyetrackerUpdateTimerTick;
    }

    #endregion

    #region Delegates

    /// <summary>
    /// Support for IsAvailable delegate
    /// </summary>
    /// <param name="error">The error occured during tracker status check.</param>
    /// <returns>A Tracker Status</returns>
    private delegate TrackerStatus IsAvailable(out string error);

    #endregion

    #region Public Properties

    /// <summary>
    ///   Gets the selected available hardware devices.
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

        if (this.chbSMIiViewX.Checked)
        {
          returnValue |= HardwareTracker.SMIiViewX;
        }

        if (this.chbSMIRedM.Checked)
        {
          returnValue |= HardwareTracker.SMIRedM;
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

        if (this.chbHaytham.Checked)
        {
          returnValue |= HardwareTracker.Haytham;
        }

        if (this.chbTheEyeTribe.Checked)
        {
          returnValue |= HardwareTracker.TheEyeTribe;
        }

        if (this.chbSmartEye.Checked)
        {
          returnValue |= HardwareTracker.SmartEye;
        }

        return returnValue;
      }
    }

    #endregion

    #region Methods

    /// <summary>
    /// Updates the connected tracker status.
    /// </summary>
    /// <param name="sender">
    /// Source of the event.
    /// </param>
    /// <param name="e">
    /// An empty <see cref="EventArgs"/>
    /// </param>
    private void EyetrackerUpdateTimerTick(object sender, EventArgs e)
    {
      this.UpdateTrackerStatus();
    }

    /// <summary>
    /// GRPs the asl enter.
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    private void GrpASLEnter(object sender, EventArgs e)
    {
      this.UpdateInfobar(AslTracker.IsAvailable, Properties.Resources.ASLFoto64, Properties.Resources.ASLLogo);
    }

    /// <summary>
    /// Update the info bar on hovering over the control.
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    private void GrpAleaEnter(object sender, EventArgs e)
    {
      this.UpdateInfobar(AleaTracker.IsAvailable, Properties.Resources.AleaFoto64, Properties.Resources.AleaLogo);
    }

    /// <summary>
    /// Update the info bar on hovering over the control.
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    private void GrpEyetechEnter(object sender, EventArgs e)
    {
      this.UpdateInfobar(
        EyeTechTracker.IsAvailable,
        Properties.Resources.EyetechFoto64,
        Properties.Resources.EyeTechLogo);
    }

    /// <summary>
    /// Update the info bar on hovering over the control.
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    private void GrpGazepointEnter(object sender, EventArgs e)
    {
      this.UpdateInfobar(
        GazepointTracker.IsAvailable,
        Properties.Resources.GazepointFoto64,
        Properties.Resources.GazepointLogo);
    }

    /// <summary>
    /// Update the info bar on hovering over the control.
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    private void GrpGazetrackerDirectEnter(object sender, EventArgs e)
    {
      this.UpdateInfobar(
        GazetrackerDirectClientTracker.IsAvailable,
        Properties.Resources.ITUDirectFoto64,
        Properties.Resources.ITU_Logo);
    }

    /// <summary>
    /// Update the info bar on hovering over the control.
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    private void GrpGazetrackerNetworkEnter(object sender, EventArgs e)
    {
      this.UpdateInfobar(
        GazetrackerIPClientTracker.IsAvailable,
        Properties.Resources.ITUNetworkFoto64,
        Properties.Resources.ITU_Logo);
    }

    /// <summary>
    /// Update the info bar on hovering over the control.
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    private void GrpHaythamEnter(object sender, EventArgs e)
    {
      this.UpdateInfobar(
        HaythamTracker.IsAvailable,
        Properties.Resources.Haytham64Foto,
        Properties.Resources.HaythamLogo);
    }

    /// <summary>
    /// Update the info bar on hovering over the control.
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    private void GrpMirametrixEnter(object sender, EventArgs e)
    {
      this.UpdateInfobar(
        MirametrixTracker.IsAvailable,
        Properties.Resources.MirametrixFoto64,
        Properties.Resources.MirametrixLogo);
    }

    /// <summary>
    /// Update the info bar on hovering over the control.
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    private void GrpMouseOnlyEnter(object sender, EventArgs e)
    {
      this.UpdateInfobar(MouseOnlyTracker.IsAvailable, Properties.Resources.Mouse, Properties.Resources.Mouse);
    }

    /// <summary>
    /// Update the info bar on hovering over the control.
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    private void GrpSmartEyeEnter(object sender, System.EventArgs e)
    {
      this.UpdateInfobar(SmartEyeTracker.IsAvailable, Properties.Resources.SmartEyeAuroraFoto64, Properties.Resources.SmartEyeLogo);
    }

    /// <summary>
    /// Update the info bar on hovering over the control.
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    private void GrpSMIRedMEnter(object sender, EventArgs e)
    {
      this.UpdateInfobar(SMIRedMTracker.IsAvailable, Properties.Resources.SMIRedMFoto64, Properties.Resources.SMILogo);
    }

    /// <summary>
    /// Update the info bar on hovering over the control.
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    private void GrpSmIiViewXEnter(object sender, EventArgs e)
    {
      this.UpdateInfobar(
        SMIiViewXTracker.IsAvailable,
        Properties.Resources.SMIiViewXFoto64,
        Properties.Resources.SMILogo);
    }

    /// <summary>
    /// Update the info bar on hovering over the control.
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    private void GrpTobiiEnter(object sender, EventArgs e)
    {
      this.UpdateInfobar(TobiiTracker.IsAvailable, Properties.Resources.TobiiPhoto64, Properties.Resources.TobiiLogo);
    }

    /// <summary>
    /// Update the info bar on hovering over the control.
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    private void GrpTheEyeTribeEnter(object sender, EventArgs e)
    {
      this.UpdateInfobar(TheEyeTribeTracker.IsAvailable, Properties.Resources.TheEyeTribeFoto64, Properties.Resources.TheEyeTribeLogo);
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler for
    ///   the <see cref="PictureBox"/>
    ///   User clicked the haytham logo, so open haytham website.
    /// </summary>
    /// <param name="sender">
    /// Source of the event.
    /// </param>
    /// <param name="e">
    /// An empty <see cref="EventArgs"/>
    /// </param>
    private void PcHaythamClick(object sender, EventArgs e)
    {
      System.Diagnostics.Process.Start("http://www.itu.dk/research/eye/");
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler for
    ///   the <see cref="PictureBox"/> <see cref="pcbAlea"/>.
    ///   User clicked the alea logo,
    ///   so open alea technologies website.
    /// </summary>
    /// <param name="sender">
    /// Source of the event.
    /// </param>
    /// <param name="e">
    /// An empty <see cref="EventArgs"/>
    /// </param>
    private void PcbAleaClick(object sender, EventArgs e)
    {
      System.Diagnostics.Process.Start("http://www.alea-technologies.com");
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler for
    ///   the <see cref="PictureBox"/> <see cref="pcbASL"/>.
    ///   User clicked the ASL logo,
    ///   so open senso motoric instruments website.
    /// </summary>
    /// <param name="sender">
    /// Source of the event.
    /// </param>
    /// <param name="e">
    /// An empty <see cref="EventArgs"/>
    /// </param>
    private void PcbAslClick(object sender, EventArgs e)
    {
      System.Diagnostics.Process.Start("http://asleyetracking.com/site/");
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler for
    ///   the <see cref="PictureBox"/> <see cref="pcbEyetech"/>.
    ///   User clicked the Eye Tech logo,
    ///   so open eye tech website.
    /// </summary>
    /// <param name="sender">
    /// Source of the event.
    /// </param>
    /// <param name="e">
    /// An empty <see cref="EventArgs"/>
    /// </param>
    private void PcbEyetechClick(object sender, EventArgs e)
    {
      System.Diagnostics.Process.Start("https://www.eyetechds.com");
    }

    /// <summary>
    /// The event handler for the User clicked the gazepoint logo, so open mirametrix website
    /// </summary>
    /// <param name="sender">
    /// Source of the event.
    /// </param>
    /// <param name="e">
    /// An empty <see cref="EventArgs"/>
    /// </param>
    private void PcbGazepointClick(object sender, EventArgs e)
    {
      System.Diagnostics.Process.Start("http://www.gazept.com");
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler for
    ///   the <see cref="PictureBox"/>
    ///   User clicked the ITU logo,
    ///   so open GAZE GROUP website.
    /// </summary>
    /// <param name="sender">
    /// Source of the event.
    /// </param>
    /// <param name="e">
    /// An empty <see cref="EventArgs"/>
    /// </param>
    private void PcbITUClick(object sender, EventArgs e)
    {
      System.Diagnostics.Process.Start("http://www.gazegroup.org/downloads/23-gazetracker");
    }

    /// <summary>
    /// The event handler for the User clicked the mirametrix logo, so open mirametrix website
    /// </summary>
    /// <param name="sender">
    /// Source of the event.
    /// </param>
    /// <param name="e">
    /// An empty <see cref="EventArgs"/>
    /// </param>
    private void PcbMirametrixClick(object sender, EventArgs e)
    {
      System.Diagnostics.Process.Start("http://www.mirametrix.com");
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler for
    /// the <see cref="PictureBox"/>
    /// User clicked the Smart Eye logo,
    /// so open Smart Eye website.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void PcbSmartEyeClick(object sender, EventArgs e)
    {
      System.Diagnostics.Process.Start("http://www.smarteye.se");
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler for
    ///   the <see cref="PictureBox"/> <see cref="pcbSMIiViewX"/>.
    ///   User clicked the SMI logo,
    ///   so open senso motoric instruments website.
    /// </summary>
    /// <param name="sender">
    /// Source of the event.
    /// </param>
    /// <param name="e">
    /// An empty <see cref="EventArgs"/>
    /// </param>
    private void PcbSMIClick(object sender, EventArgs e)
    {
      System.Diagnostics.Process.Start("http://www.smivision.com");
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler for
    ///   the <see cref="PictureBox"/> <see cref="pcbTobii"/>.
    ///   User clicked the tobii logo,
    ///   so open tobii technologies website.
    /// </summary>
    /// <param name="sender">
    /// Source of the event.
    /// </param>
    /// <param name="e">
    /// An empty <see cref="EventArgs"/>
    /// </param>
    private void PcbTobiiClick(object sender, EventArgs e)
    {
      System.Diagnostics.Process.Start("http://www.tobii.com");
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler for
    ///   the <see cref="PictureBox"/> <see cref="pcbTheEyeTribe"/>.
    ///   User clicked the eye tribe logo,
    ///   so open eye tribe website.
    /// </summary>
    /// <param name="sender"> Source of the event. </param>
    /// <param name="e"> An empty <see cref="EventArgs"/> </param>
    private void PcbTheEyeTribeClick(object sender, EventArgs e)
    {
      System.Diagnostics.Process.Start("http://theeyetribe.com");
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler
    ///   for the <see cref="PictureBox"/> <see cref="pcbHelpAlea"/>
    ///   Displays instructions to activate alea recording.
    /// </summary>
    /// <param name="sender">
    /// Source of the event
    /// </param>
    /// <param name="e">
    /// An empty <see cref="EventArgs"/>
    /// </param>
    private void PcbHelpAleaClick(object sender, EventArgs e)
    {
      var objActivateAlea = new HowToActivateAlea();
      objActivateAlea.ShowDialog();
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler
    ///   for the <see cref="PictureBox"/> <see cref="pcbHelpASL"/>
    ///   Displays instructions to activate ASL recording.
    /// </summary>
    /// <param name="sender">
    /// Source of the event
    /// </param>
    /// <param name="e">
    /// An empty <see cref="EventArgs"/>
    /// </param>
    private void PcbHelpAslClick(object sender, EventArgs e)
    {
      var objActivateAsl = new HowToActivateAsl();
      objActivateAsl.ShowDialog();
    }

    /// <summary>
    /// The event handler for the User clicked the gazepoint help
    /// </summary>
    /// <param name="sender">
    /// Source of the event.
    /// </param>
    /// <param name="e">
    /// An empty <see cref="EventArgs"/>
    /// </param>
    private void PcbHelpGazepointClick(object sender, EventArgs e)
    {
      var objActivateGazepoint = new HowToActivateGazepoint();
      objActivateGazepoint.ShowDialog();
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler
    ///   for the <see cref="PictureBox"/> <see cref="pcbHelpGazetrackerIPClient"/>
    ///   Displays instructions to enable gaze tracking with the GazeTracker and a webcam.
    /// </summary>
    /// <param name="sender">
    /// Source of the event
    /// </param>
    /// <param name="e">
    /// An empty <see cref="EventArgs"/>
    /// </param>
    private void PcbHelpGazetrackerClientClick(object sender, EventArgs e)
    {
      var objActivateITU = new HowToActivateGazetracker();
      objActivateITU.ShowDialog();
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler
    ///   for the <see cref="PictureBox"/> <see cref="pcbHelpGazetrackerDirectClient"/>
    ///   Displays instructions to enable gaze tracking with the GazeTracker and a webcam.
    /// </summary>
    /// <param name="sender">
    /// Source of the event
    /// </param>
    /// <param name="e">
    /// An empty <see cref="EventArgs"/>
    /// </param>
    private void PcbHelpGazetrackerDirectClientClick(object sender, EventArgs e)
    {
      var objHowToActivateGazetracker = new HowToActivateGazetracker();
      objHowToActivateGazetracker.ShowDialog();
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler
    ///   for the <see cref="PictureBox"/> <see cref="pcbHelpHaytham"/>
    ///   Displays instructions to enable gaze tracking with the Haytham and a webcam.
    /// </summary>
    /// <param name="sender">
    /// Source of the event
    /// </param>
    /// <param name="e">
    /// An empty <see cref="EventArgs"/>
    /// </param>
    private void PcbHelpHaythamClick(object sender, EventArgs e)
    {
      var objActivateHaytham = new HowToActivateHaytham();
      objActivateHaytham.ShowDialog();
    }

    /// <summary>
    /// The event handler for the User clicked the mirametrix logo, so open mirametrix website
    /// </summary>
    /// <param name="sender">
    /// Source of the event.
    /// </param>
    /// <param name="e">
    /// An empty <see cref="EventArgs"/>
    /// </param>
    private void PcbHelpMirametrixClick(object sender, EventArgs e)
    {
      var objActivateMirametrix = new HowToActivateMirametrix();
      objActivateMirametrix.ShowDialog();
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler
    /// for the <see cref="PictureBox"/> <see cref="pcbHelpSMIRedM"/>
    /// Displays instructions to activate SMI RedM recording.
    /// </summary>
    /// <param name="sender">Source of the event</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void PcbHelpSmartEyeClick(object sender, EventArgs e)
    {
      var objActivateSmartEye = new HowToActivateSmartEye();
      objActivateSmartEye.ShowDialog();
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler
    ///   for the <see cref="PictureBox"/> <see cref="pcbHelpSMIRedM"/>
    ///   Displays instructions to activate SMI RedM recording.
    /// </summary>
    /// <param name="sender">
    /// Source of the event
    /// </param>
    /// <param name="e">
    /// An empty <see cref="EventArgs"/>
    /// </param>
    private void PcbHelpSMIRedMClick(object sender, EventArgs e)
    {
      var objActivateSMI = new HowToActivateSMIRedM();
      objActivateSMI.ShowDialog();
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler
    ///   for the <see cref="PictureBox"/> <see cref="pcbHelpSMI"/>
    ///   Displays instructions to activate SMI iViewX recording.
    /// </summary>
    /// <param name="sender">
    /// Source of the event
    /// </param>
    /// <param name="e">
    /// An empty <see cref="EventArgs"/>
    /// </param>
    private void PcbHelpSmIiViewXClick(object sender, EventArgs e)
    {
      var objActivateSMI = new HowToActivateSMIiViewX();
      objActivateSMI.ShowDialog();
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler
    ///   for the <see cref="PictureBox"/> <see cref="pcbHelpTobii"/>
    ///   Displays instructions to activate tobii recording.
    /// </summary>
    /// <param name="sender">
    /// Source of the event
    /// </param>
    /// <param name="e">
    /// An empty <see cref="EventArgs"/>
    /// </param>
    private void PcbHelpTobiiClick(object sender, EventArgs e)
    {
      var objActivateTobii = new HowToActivateTobii();
      objActivateTobii.ShowDialog();
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler
    ///   for the <see cref="PictureBox"/> <see cref="pcbHelpTheEyeTribe"/>
    ///   Displays instructions to activate the eye tribe recording.
    /// </summary>
    /// <param name="sender"> Source of the event </param>
    /// <param name="e"> An empty <see cref="EventArgs"/> </param>
    private void PcbHelpTheEyeTribeClick(object sender, EventArgs e)
    {
      var objActivateTheEyeTribe = new HowToActivateTheEyeTribe();
      objActivateTheEyeTribe.ShowDialog();
    }

    /// <summary>
    /// The <see cref="Form.FormClosing"/> event handler
    ///   which checks for a valid tracking device selection
    ///   and otherwise raises a warning message.
    /// </summary>
    /// <param name="sender">
    /// Source of the event.
    /// </param>
    /// <param name="e">
    /// The <see cref="FormClosingEventArgs"/>
    ///   with the event data.
    /// </param>
    private void SelectTrackerFormClosing(object sender, FormClosingEventArgs e)
    {
      if (this.SelectedTracker == HardwareTracker.None && this.DialogResult == DialogResult.OK)
      {
        e.Cancel = true;
        string message = "At least one tracking device should be selected." + Environment.NewLine
                         + "If no gaze tracking hardware is available " + "you can use the mouse only tracker.";
        ExceptionMethods.ProcessMessage("Please select a device ...", message);
      }
      else
      {
        this.eyetrackerUpdateTimer.Stop();
      }
    }

    /// <summary>
    /// <see cref="Form.Load"/> event handler.
    ///   Checks for available tracker interfaces.
    /// </summary>
    /// <param name="sender">
    /// Source of the event.
    /// </param>
    /// <param name="e">
    /// An empty <see cref="EventArgs"/>
    /// </param>
    private void SelectTrackerLoad(object sender, EventArgs e)
    {
      this.UpdateTrackerStatus();
    }

    /// <summary>
    /// Updates the status of the given tracking group box.
    /// </summary>
    /// <param name="availableMethod">
    /// The IsAvailable method of the tracker.
    /// </param>
    /// <param name="checkBox">
    /// The checkbox to select the tracker.
    /// </param>
    /// <param name="pcbStatus">
    /// The tracker status image picture box.
    /// </param>
    /// <param name="lblStatus">
    /// The tracker status label.
    /// </param>
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

    /// <summary>
    ///   Updates the status of the ASL tracking devices
    /// </summary>
    private void UpdateASLStatus()
    {
      this.SetStatus(AslTracker.IsAvailable, this.chbASL, this.pcbASLStatus, this.lblASLStatus);
    }

    /// <summary>
    ///   Updates the status of the Alea tracking devices
    /// </summary>
    private void UpdateAleaTrackStatus()
    {
      this.SetStatus(AleaTracker.IsAvailable, this.chbAlea, this.pcbAleaStatus, this.lblAleaStatus);
    }

    /// <summary>
    ///   Updates the status of the Eyetech tracking devices
    /// </summary>
    private void UpdateEyeTechStatus()
    {
      this.SetStatus(EyeTechTracker.IsAvailable, this.chbEyetech, this.pcbEyetechStatus, this.lblEyetechStatus);
    }

    /// <summary>
    ///   Updates the status of the Gazepoint tracking devices
    /// </summary>
    private void UpdateGazepointStatus()
    {
      this.SetStatus(GazepointTracker.IsAvailable, this.chbGazepoint, this.pcbGazepointStatus, this.lblGazepointStatus);
    }

    /// <summary>
    ///   Updates the status of the gazetracker direct client devices.
    /// </summary>
    private void UpdateGazetrackerDirectClientStatus()
    {
      this.SetStatus(
        GazetrackerDirectClientTracker.IsAvailable,
        this.chbGazetrackerDirectClient,
        this.pcbGazetrackerDirectStatus,
        this.lblGazetrackerDirectStatus);
    }

    /// <summary>
    ///   Updates the status of the gazetracker network client devices.
    /// </summary>
    private void UpdateGazetrackerNetworkClientStatus()
    {
      this.SetStatus(
        GazetrackerIPClientTracker.IsAvailable,
        this.chbGazetrackerIPClient,
        this.pcbGazetrackerNetworkStatus,
        this.lblGazetrackerNetworkStatus);
    }

    /// <summary>
    ///   Updates the status of the haytham client.
    /// </summary>
    private void UpdateHaythamTrackStatus()
    {
      this.SetStatus(HaythamTracker.IsAvailable, this.chbHaytham, this.pcbHaythamStatus, this.lblHaythamStatus);
    }

    /// <summary>
    /// This method updates the infobar with the given tracker information.
    /// </summary>
    /// <param name="availableMethod">
    /// The IsAvailable method of the tracker.
    /// </param>
    /// <param name="image">
    /// The bitmap of the tracker
    /// </param>
    /// <param name="logo">
    /// The bitmap of the logo
    /// </param>
    private void UpdateInfobar(IsAvailable availableMethod, Image image, Image logo)
    {
      this.pcbSelectedTrackerLogo.Image = logo;
      this.pcbSelectedTrackerImage.Image = image;
      string error;
      availableMethod(out error);
      this.lblSelectedStatus.Text = error;
    }

    /// <summary>
    ///   Updates the status of the Mirametrix tracking devices
    /// </summary>
    private void UpdateMirametrixStatus()
    {
      this.SetStatus(
        MirametrixTracker.IsAvailable,
        this.chbMirametrix,
        this.pcbMirametrixStatus,
        this.lblMirametrixStatus);
    }

    /// <summary>
    ///   Updates the status of the mouse device.
    /// </summary>
    private void UpdateMouseStatus()
    {
      this.SetStatus(MouseOnlyTracker.IsAvailable, this.chbMouseOnly, this.pcbMouseStatus, this.lblMouseStatus);
    }

    /// <summary>
    /// Updates the status of the Smart Eye tracking devices
    /// </summary>
    private void UpdateSmartEyeStatus()
    {
      this.SetStatus(SmartEyeTracker.IsAvailable, this.chbSmartEye, this.pcbSmartEyeStatus, this.lblSmartEyeStatus);
    }

    /// <summary>
    ///   Updates the status of the SMI RedM tracking devices
    /// </summary>
    private void UpdateSMIRedMTrackStatus()
    {
      this.SetStatus(SMIRedMTracker.IsAvailable, this.chbSMIRedM, this.pcbSMIRedMStatus, this.lblSMIRedMStatus);
    }

    /// <summary>
    ///   Updates the status of the SMI iViewX tracking devices
    /// </summary>
    private void UpdateSmIiViewXTrackStatus()
    {
      this.SetStatus(SMIiViewXTracker.IsAvailable, this.chbSMIiViewX, this.pcbSMIiVIewXStatus, this.lblSMIiViewXStatus);
    }

    /// <summary>
    ///   Updates the status of the Tobii tracking devices
    /// </summary>
    private void UpdateTobiiStatus()
    {
      this.SetStatus(TobiiTracker.IsAvailable, this.chbTobii, this.pcbTobiiStatus, this.lblTobiiStatus);
    }

    /// <summary>
    ///   Updates the status of the eye tribe tracking devices
    /// </summary>
    private void UpdateTheEyeTribeStatus()
    {
      this.SetStatus(TheEyeTribeTracker.IsAvailable, this.chbTheEyeTribe, this.pcbTheEyeTribeStatus, this.lblTheEyeTribeStatus);
    }

    /// <summary>
    ///   Updates the status of all connected devices.
    /// </summary>
    private void UpdateTrackerStatus()
    {
      this.UpdateMouseStatus();
      this.UpdateGazetrackerDirectClientStatus();
      this.UpdateGazetrackerNetworkClientStatus();
      this.UpdateAleaTrackStatus();
      this.UpdateTobiiStatus();
      this.UpdateASLStatus();
      this.UpdateMirametrixStatus();
      this.UpdateGazepointStatus();
      this.UpdateEyeTechStatus();
      this.UpdateSmIiViewXTrackStatus();
      this.UpdateSMIRedMTrackStatus();
      this.UpdateHaythamTrackStatus();
      this.UpdateTheEyeTribeStatus();
      this.UpdateSmartEyeStatus();
    }

    #endregion
  }
}