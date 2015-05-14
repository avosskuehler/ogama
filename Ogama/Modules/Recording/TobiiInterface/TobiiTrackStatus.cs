// <copyright file="TobiiTrackStatus.cs" company="FU Berlin">
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

namespace Ogama.Modules.Recording.TobiiInterface
{
  using System;
  using System.Windows.Forms;

  using Tobii.Eyetracking.Sdk;

  /// <summary>
  /// A dialog which exposes a track status meter to the subject.
  /// </summary>
  public partial class TobiiTrackStatus : Form
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

    /////// <summary>
    /////// Saves the current active <see cref="TobiiSetting"/> to use.
    /////// </summary>
    ////private TobiiSetting tobiiSettings;

    /////// <summary>
    /////// Saves whether this dialog is connected to the tracker.
    /////// </summary>
    ////private bool isConnected;

    #endregion //FIELDS

    ///////////////////////////////////////////////////////////////////////////////
    // Construction and Initializing methods                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region CONSTRUCTION

    /// <summary>
    /// Initializes a new instance of the TobiiTrackStatus class.
    /// </summary>
    /// <param name="setting">A <see cref="TobiiSetting"/> to use for
    /// connection.</param>
    public TobiiTrackStatus(TobiiSetting setting)
    {
      this.InitializeComponent();

      // this.tobiiSettings = setting;
      // this.Connect();
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

    /// <summary>
    /// Updates the status control with the incoming gaze data.
    /// </summary>
    /// <param name="gd">The <see cref="GazeDataItem"/> with the current
    /// gaze data.</param>
    public void Update(GazeDataItem gd)
    {
      this.tobiiTrackStatusControl.OnGazeData(gd);
    }

    ///////////////////////////////////////////////////////////////////////////////
    // Eventhandler for UI, Menu, Buttons, Toolbars etc.                         //
    ///////////////////////////////////////////////////////////////////////////////
    #region WINDOWSEVENTHANDLER

    /// <summary>
    /// The <see cref="Form.FormClosing"/> event handler.
    /// Disconnects the track status activeX control.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">A <see cref="FormClosingEventArgs"/> with the event data.</param>
    private void TobiiTrackStatusFormClosing(object sender, FormClosingEventArgs e)
    {
      ////if (this.isConnected)
      ////{
      ////  this.Disconnect();
      ////}
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
    /// This method tries to connect the track status object to
    /// the tracking system.
    /// </summary>
    private void Connect()
    {
      try
      {
        ////this.tetTrackStatusSubject = (ITetTrackStatus)this.axTobiiTrackStatus.GetOcx();

        ////// Connect to the TET server if necessary
        ////if (!this.tetTrackStatusSubject.IsConnected)
        ////{
        ////  this.tetTrackStatusSubject.Connect(
        ////    this.tobiiSettings.connectedTrackerName,
        ////    this.tobiiSettings.TetServerPort);
        ////}

        ////// Start the track status meter
        ////if (!this.tetTrackStatusSubject.IsTracking)
        ////{
        ////  this.tetTrackStatusSubject.Start();
        ////}

        ////this.isConnected = true;
      }
      catch (Exception)
      {
        // Don´t show an error message here
        this.Close();
      }
    }

    #endregion //METHODS

    ///////////////////////////////////////////////////////////////////////////////
    // Small helping Methods                                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region HELPER
    #endregion //HELPER
  }
}
