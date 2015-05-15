// <copyright file="TrackStatusControl.cs" company="FU Berlin">
// ******************************************************
// OgamaClient for ITU GazeTracker
// Copyright (C) 2015 Dr. Adrian Voßkühler  
// ------------------------------------------------------------------------
// This program is free software; you can redistribute it and/or modify it 
// under the terms of the GNU General Public License as published by the 
// Free Software Foundation; either version 3 of the License, 
// or (at your option) any later version.
// This program is distributed in the hope that it will be useful, 
// but WITHOUT ANY WARRANTY; without even the implied warranty of 
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU 
// General Public License for more details.
// You should have received a copy of the GNU General Public License 
// along with this program; if not, see http://www.gnu.org/licenses/.
// **************************************************************
// </copyright>
// <author>Adrian Voßkühler</author>
// <email>adrian@ogama.net</email>

namespace GTOgamaClient.Controls
{
  using System;
  using System.ComponentModel;
  using System.Windows.Forms;

  using GTLibrary.Logging;

  /// <summary>
  /// A dialog which exposes a track status meter to the subject.
  /// For the ITU client this is the processed eye video in
  /// native resolution
  /// </summary>
  public partial class TrackStatusControl : Form
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
    /// Saves whether this dialog is connected to the tracker.
    /// </summary>
    private bool isConnected;

    #endregion //FIELDS

    ///////////////////////////////////////////////////////////////////////////////
    // Construction and Initializing methods                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region CONSTRUCTION

    /// <summary>
    /// Initializes a new instance of the TrackStatusControl class.
    /// </summary>
    public TrackStatusControl()
    {
      this.InitializeComponent();
    }

    #endregion //CONSTRUCTION

    ///////////////////////////////////////////////////////////////////////////////
    // Defining events, enums, delegates                                         //
    ///////////////////////////////////////////////////////////////////////////////
    #region EVENTS
    #endregion EVENTS

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Properties                                                       //
    ///////////////////////////////////////////////////////////////////////////////
    #region PROPERTIES

    /// <summary>
    /// Gets the <see cref="EyeVideoControl"/> of this form.
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public EyeVideoControl EyeVideoControl
    {
      get { return this.eyeVideoControl; }
    }

    #endregion //PROPERTIES

    ///////////////////////////////////////////////////////////////////////////////
    // Public methods                                                            //
    ///////////////////////////////////////////////////////////////////////////////
    #region PUBLICMETHODS

    /// <summary>
    /// Updates the size and description of this form
    /// with the video property values.
    /// </summary>
    /// <param name="imgWidth">An <see cref="Int32"/> with the native video width</param>
    /// <param name="imgHeight">An <see cref="Int32"/> with the native video height</param>
    /// <param name="fps">An <see cref="Int32"/> with the video framerate</param>
    public void SetSizeAndLabels(int imgWidth, int imgHeight, int fps)
    {
      // Size
      eyeVideoControl.VideoImageWidth = imgWidth;
      eyeVideoControl.VideoImageHeight = imgHeight;

      this.Width = imgWidth + eyeVideoControl.Margin.Left + eyeVideoControl.Margin.Right;
      this.Height = imgHeight + eyeVideoControl.Margin.Top +
        eyeVideoControl.Margin.Bottom + this.splitContainer1.Panel1.Height + 30;

      // Label
      //if (fps == 0)
      {
        this.Text = "Track status at " + imgWidth.ToString() + " x " +
          imgHeight.ToString();
      }
      //else
      //{
      //  this.Text = "Track status at " + imgWidth.ToString() + " x " +
      //    imgHeight.ToString() + " @ " + fps.ToString() + " frames per second";
      //}
    }

    #endregion //PUBLICMETHODS

    ///////////////////////////////////////////////////////////////////////////////
    // Inherited methods                                                         //
    ///////////////////////////////////////////////////////////////////////////////
    #region OVERRIDES
    #endregion //OVERRIDES

    ///////////////////////////////////////////////////////////////////////////////
    // Eventhandler                                                              //
    ///////////////////////////////////////////////////////////////////////////////
    #region EVENTHANDLER

    /// <summary>
    /// The <see cref="Form.Shown"/> event handler.
    /// Connects the track status control.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">A <see cref="FormClosingEventArgs"/> with the event data.</param>
    private void TrackStatusControl_Shown(object sender, EventArgs e)
    {
      this.Connect();
    }

    /// <summary>
    /// The <see cref="Form.FormClosing"/> event handler.
    /// Disconnects the track status control.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">A <see cref="FormClosingEventArgs"/> with the event data.</param>
    private void TrackStatus_FormClosing(object sender, FormClosingEventArgs e)
    {
      this.Disconnect();
    }

    #endregion //EVENTHANDLER

    ///////////////////////////////////////////////////////////////////////////////
    // Methods and Eventhandling for Background tasks                            //
    ///////////////////////////////////////////////////////////////////////////////
    #region THREAD
    #endregion //THREAD

    ///////////////////////////////////////////////////////////////////////////////
    // Methods for doing main class job                                          //
    ///////////////////////////////////////////////////////////////////////////////
    #region PRIVATEMETHODS

    /// <summary>
    /// This method tries to connect the track status object to
    /// the tracking system.
    /// </summary>
    private void Connect()
    {
      try
      {
        if (!this.isConnected)
        {
          this.eyeVideoControl.Start();
          this.isConnected = true;
        }
      }
      catch (Exception ex)
      {
        ErrorLogger.ProcessException(ex, false);

        // Don´t show an error message here
        this.Close();
      }
    }

    /// <summary>
    /// This method disconnects the track status object from the
    /// tracking system.
    /// </summary>
    private void Disconnect()
    {
      if (this.isConnected)
      {
        this.eyeVideoControl.Stop();
        this.isConnected = false;
      }
    }

    #endregion //PRIVATEMETHODS

    ///////////////////////////////////////////////////////////////////////////////
    // Small helping Methods                                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region HELPER
    #endregion //HELPER

    ///////////////////////////////////////////////////////////////////////////////
    // Small helping Methods                                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region HELPER
    #endregion //HELPER
  }
}