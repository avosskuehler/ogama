// <copyright file="SmartEyeTrackStatus.cs" company="Smart Eye AB">
// ******************************************************
// OGAMA - open gaze and mouse analyzer 
// Copyright (C) 2014 Dr. Adrian Voßkühler  
// ------------------------------------------------------------------------
// This program is free software; you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation; either version 2 of the License, or (at your option) any later version.
// This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
// You should have received a copy of the GNU General Public License along with this program; if not, write to the Free Software Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA 02111-1307 USA
// **************************************************************
// </copyright>
// <author>Kathrin Scheil</author>
// <email>kathrin.scheil@smarteye.se</email>

namespace Ogama.Modules.Recording.SmartEyeInterface
{
  using System;
  using System.Drawing;
  using System.Windows.Forms;

  /// <summary>
  /// A dialog which exposes a track status meter and live image to the subject.
  /// </summary>
  public partial class SmartEyeTrackStatus : Form
  {
    ///////////////////////////////////////////////////////////////////////////////
    // Construction and Initializing methods                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region CONSTRUCTION

    /// <summary>
    /// Initializes a new instance of the <see cref="SmartEyeTrackStatus"/> class.
    /// </summary>
    public SmartEyeTrackStatus()
    {
      this.InitializeComponent();
    }

    #endregion CONSTRUCTION

    ///////////////////////////////////////////////////////////////////////////////
    // Eventhandler                                                              //
    ///////////////////////////////////////////////////////////////////////////////
    #region EVENTS

    /// <summary>
    /// Updates the status control with the incoming gaze data.
    /// </summary>
    /// <param name="gd">The GazeDataItem with the current
    /// gaze data.</param>
    public void Update(SmartEyeGazeData gd)
    {
      this.smartEyeTrackStatusControl.OnGazeData(gd);
    }

    /// <summary>
    /// Update the displayed live image
    /// </summary>
    /// <param name="bmp">The current live image</param>
    public void UpdateLiveImage(Bitmap bmp)
    {
      this.smartEyeTrackStatusControl.OnLiveImage(bmp);
    }

    #endregion EVENTS
  }
}
