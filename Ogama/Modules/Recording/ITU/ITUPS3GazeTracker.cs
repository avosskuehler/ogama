// <copyright file="ITUPS3GazeTracker.cs" company="FU Berlin">
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

namespace Ogama.Modules.Recording.ITUGazeTracker
{
  using System;
  using System.Runtime.InteropServices;
  using System.Windows.Forms;

  /// <summary>
  /// This class implements the <see cref="TrackerWithStatusControls"/> class
  /// to represent an OGAMA known eyetracker.
  /// It encapsulates the ITU GazeTracker http://www.gazegroup.org 
  /// </summary>
  public class ITUPS3GazeTracker : ITUGazeTrackerBase
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
    /// Initializes a new instance of the ITUPS3GazeTracker class.
    /// Initializes COM objects and other.
    /// </summary>
    /// <param name="owningRecordModule">The <see cref="RecordModule"/>
    /// form wich host the recorder.</param>
    /// <param name="trackerTrackerControlsContainer">The <see cref="SplitContainer"/>
    /// control which contains two <see cref="SplitContainer"/>s with
    /// track status and calibration plot controls and buttons.</param>
    /// <param name="trackerTrackStatusPanel">The <see cref="Panel"/>
    /// which should contain the track status object.</param>
    /// <param name="trackerCalibrationResultPanel">The <see cref="Panel"/>
    /// which should contain the calibration result object.</param>
    /// <param name="trackerShowOnSecondaryScreenButton">The <see cref="Button"/>
    /// named "ShowOnPresentationScreenButton" at the tab page of the ITUPS3GazeTracker device.</param>
    /// <param name="trackerAcceptButton">The <see cref="Button"/>
    /// named "Accept" at the tab page of the ITUPS3GazeTracker device.</param>
    /// <param name="trackerRecalibrateButton">The <see cref="Button"/>
    /// named "Recalibrate" at the tab page of the ITUPS3GazeTracker device.</param>
    /// <param name="trackerConnectButton">The <see cref="Button"/>
    /// named "Connect" at the tab page of the ITUPS3GazeTracker device.</param>
    /// <param name="trackerAdjustButton">The <see cref="Button"/>
    /// named "Adjust" at the tab page of the device.</param>
    /// <param name="trackerSubjectButton">The <see cref="Button"/>
    /// named "Subject" at the tab page of the ITUPS3GazeTracker device.</param>
    /// <param name="trackerCalibrateButton">The <see cref="Button"/>
    /// named "Calibrate" at the tab page of the ITUPS3GazeTracker device.</param>
    /// <param name="trackerRecordButton">The <see cref="Button"/>
    /// named "Record" at the tab page of the ITUPS3GazeTracker device.</param>
    /// <param name="trackerSubjectNameTextBox">The <see cref="TextBox"/>
    /// which should contain the subject name at the tab page of the ITUPS3GazeTracker device.</param>
    public ITUPS3GazeTracker(
      RecordModule owningRecordModule,
      SplitContainer trackerTrackerControlsContainer,
      Panel trackerTrackStatusPanel,
      Panel trackerCalibrationResultPanel,
      Button trackerShowOnSecondaryScreenButton,
      Button trackerAcceptButton,
      Button trackerRecalibrateButton,
      Button trackerConnectButton,
      Button trackerAdjustButton,
      Button trackerSubjectButton,
      Button trackerCalibrateButton,
      Button trackerRecordButton,
      TextBox trackerSubjectNameTextBox)
      : base(
      owningRecordModule,
      trackerTrackerControlsContainer,
      trackerTrackStatusPanel,
      trackerCalibrationResultPanel,
      trackerShowOnSecondaryScreenButton,
      trackerAcceptButton,
      trackerRecalibrateButton,
      trackerConnectButton,
      trackerAdjustButton,
      trackerSubjectButton,
      trackerCalibrateButton,
      trackerRecordButton,
      trackerSubjectNameTextBox)
    {
      this.ItuClient = new OgamaClient.PS3GazeTrackerAPI();

      // Call the initialize methods of derived classes
      this.Initialize();
    }

    #endregion //CONSTRUCTION

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Enumerations                                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region ENUMS
    #endregion ENUMS

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Properties                                                       //
    ///////////////////////////////////////////////////////////////////////////////
    #region PROPERTIES
    #endregion //PROPERTIES

    ///////////////////////////////////////////////////////////////////////////////
    // Public methods                                                            //
    ///////////////////////////////////////////////////////////////////////////////
    #region PUBLICMETHODS

    /// <summary>
    /// Checks if the ITU GazeTracker has a camera available.
    /// </summary>
    /// <param name="errorMessage">Out. A <see cref="String"/> with an error message.</param>
    /// <returns><strong>True</strong>, if ITU Gazetracker with an camera
    /// is available in the system, otherwise <strong>false</strong></returns>
    public static new bool IsAvailable(out string errorMessage)
    {
      errorMessage = string.Empty;

      try
      {
        int numberOfPS3Devices = CLEyeGetCameraCount();

        if (numberOfPS3Devices == 0)
        {
          errorMessage = "There were no PlayStation3 cameras found. " + Environment.NewLine +
            "Make sure that the device is connected and that the device drivers are installed. " +
            "The driver can be installed from http://codelaboratories.com";
          return false;
        }
      }
      catch (Exception)
      {
        errorMessage = "Initialization of the ITU PS3 driver failed.";
        return false;
      }

      return true;
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
    // Methods for doing main class job                                          //
    ///////////////////////////////////////////////////////////////////////////////
    #region METHODS
    #endregion //METHODS

    ///////////////////////////////////////////////////////////////////////////////
    // Small helping Methods                                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region HELPER

    [DllImport("CLEyeMulticam.dll")]
    private static extern int CLEyeGetCameraCount();

    #endregion //HELPER
  }
}
