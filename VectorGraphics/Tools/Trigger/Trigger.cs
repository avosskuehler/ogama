// <copyright file="Trigger.cs" company="FU Berlin">
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

namespace VectorGraphics.Tools.Trigger
{
  using System;

  /// <summary>
  /// Class to save settings for triggers.
  /// </summary>
  [Serializable]
  public class Trigger
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
    /// The trigger signaling time to be sent to the <see cref="TriggerOutputDevices"/>
    /// </summary>
    private TriggerSignaling signaling;

    /// <summary>
    /// The <see cref="TriggerOutputDevices"/> to send the trigger to.
    /// </summary>
    private TriggerOutputDevices outputDevice;

    /// <summary>
    /// The trigger value to be send.
    /// </summary>
    private int signalValue;

    /// <summary>
    /// The time the trigger signal is been send in milliseconds
    /// </summary>
    private int signalingTime;

    /// <summary>
    /// The port address of the lpt device to send to
    /// </summary>
    private int portAddress;

    #endregion //FIELDS

    ///////////////////////////////////////////////////////////////////////////////
    // Construction and Initializing methods                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region CONSTRUCTION

    /// <summary>
    /// Initializes a new instance of the Trigger class.
    /// </summary>
    public Trigger()
    {
      this.signaling = TriggerSignaling.None;
      this.outputDevice = TriggerOutputDevices.LPT;
      this.signalingTime = 40;
      this.signalValue = 255;
      this.portAddress = 0x0378;
    }

    /// <summary>
    /// Initializes a new instance of the Trigger class.
    /// </summary>
    /// <param name="newSignaling">The <see cref="TriggerSignaling"/> enumeration</param>
    /// <param name="newTriggerOutputDevice">The <see cref="TriggerOutputDevices"/> to send the trigger to.</param>
    /// <param name="newSignalingTime">An <see cref="int"/> with the time in milliseconds the trigger level should be set.</param>
    /// <param name="newValue">An 8-bit <see cref="int"/> (0-255) that indicates the value to be sent.</param>
    /// <param name="newPortAddress">An <see cref="int"/> with the port address to send the trigger to.</param>
    public Trigger(
      TriggerSignaling newSignaling,
      TriggerOutputDevices newTriggerOutputDevice,
      int newSignalingTime,
      int newValue,
      int newPortAddress)
    {
      this.signaling = newSignaling;
      this.outputDevice = newTriggerOutputDevice;
      this.signalingTime = newSignalingTime;
      this.signalValue = newValue;
      this.portAddress = newPortAddress;
    }

    #endregion //CONSTRUCTION

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Properties                                                       //
    ///////////////////////////////////////////////////////////////////////////////
    #region PROPERTIES

    /// <summary>
    /// Gets or sets the trigger signaling time to be sent to the <see cref="TriggerOutputDevices"/>
    /// </summary>
    public TriggerSignaling Signaling
    {
      get { return this.signaling; }
      set { this.signaling = value; }
    }

    /// <summary>
    /// Gets or sets the <see cref="TriggerOutputDevices"/> to send the trigger to.
    /// </summary>
    /// <value>The <see cref="TriggerOutputDevices"/> to send the trigger to.</value>
    public TriggerOutputDevices OutputDevice
    {
      get { return this.outputDevice; }
      set { this.outputDevice = value; }
    }

    /// <summary>
    /// Gets or sets the trigger value to be send.
    /// </summary>
    public int Value
    {
      get { return this.signalValue; }
      set { this.signalValue = value; }
    }

    /// <summary>
    /// Gets or sets the time the trigger signal is been send in milliseconds
    /// </summary>
    public int SignalingTime
    {
      get { return this.signalingTime; }
      set { this.signalingTime = value; }
    }

    /// <summary>
    /// Gets or sets the port address of the lpt device to send to
    /// </summary>
    public int PortAddress
    {
      get { return this.portAddress; }
      set { this.portAddress = value; }
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
    /// This method sends the trigger to
    /// the given <see cref="TriggerOutputDevices"/>.
    /// </summary>
    public void Send()
    {
      switch (this.outputDevice)
      {
        ////case TriggerOutputDevices.COM1:
        ////  break;
        ////case TriggerOutputDevices.COM2:
        ////  break;
        ////case TriggerOutputDevices.COM3:
        ////  break;
        ////case TriggerOutputDevices.COM4:
        ////  break;
        ////case TriggerOutputDevices.COM5:
        ////  break;
        ////case TriggerOutputDevices.COM6:
        ////  break;
        ////case TriggerOutputDevices.COM7:
        ////  break;
        ////case TriggerOutputDevices.COM8:
        ////  break;
        case TriggerOutputDevices.LPT:
          this.SendTriggerToLPT();
          break;
        ////case TriggerOutputDevices.LPT2:
        ////  break;
      }
    }

    /// <summary>
    /// This method uses the inpout32.dll to send the trigger to the lpt port 
    /// with the given address.
    /// </summary>
    private void SendTriggerToLPT()
    {
      // 0xDC00, 0xD800, default: 0x0378
      ParallelPort.SendTriggerToLPT(this.portAddress, this.signalingTime, this.signalValue);

      // The following method does not work, closing fails.
      // ParallelPort.SendTriggerToLPT1ViaCreateFile(this.signalingTime, this.signalValue);
    }

    #endregion //METHODS

    ///////////////////////////////////////////////////////////////////////////////
    // Small helping Methods                                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region HELPER
    #endregion //HELPER
  }
}
