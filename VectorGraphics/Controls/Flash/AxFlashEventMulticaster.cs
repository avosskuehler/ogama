// <copyright file="AxFlashEventMulticaster.cs" company="FU Berlin">
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

namespace VectorGraphics.Controls.Flash
{
  using System;
  using System.Runtime.InteropServices;

  using ShockwaveFlashObjects;

  /// <summary>
  /// The class for the <see cref="_IShockwaveFlashEvents"/>.
  /// </summary>
  [ClassInterface(ClassInterfaceType.None)]
  public class AxFlashEventMulticaster : _IShockwaveFlashEvents
  {
    /// <summary>
    /// The <see cref="AxFlash"/> parent for the events.
    /// </summary>
    private AxFlash parent;

    /// <summary>
    /// Initializes a new instance of the AxFlashEventMulticaster class.
    /// </summary>
    /// <param name="parent">The <see cref="AxFlash"/> parent for the events.</param>
    public AxFlashEventMulticaster(AxFlash parent)
    {
      this.parent = parent;
    }

    /// <summary>
    /// Raises the OnReadyStateChange event.
    /// </summary>
    /// <param name="newState">An <see cref="Int32"/> with the new state.</param>
    public virtual void OnReadyStateChange(int newState)
    {
      AxFlashReadyStateChangeEvent onreadystatechangeEvent = new AxFlashReadyStateChangeEvent(newState);
      this.parent.RaiseOnOnReadyStateChange(this.parent, onreadystatechangeEvent);
    }

    /// <summary>
    /// Raises the OnProgress event.
    /// </summary>
    /// <param name="percentDone">An <see cref="Int32"/> with the percentage done.</param>
    public virtual void OnProgress(int percentDone)
    {
      AxFlashProgressEvent onprogressEvent = new AxFlashProgressEvent(percentDone);
      this.parent.RaiseOnOnProgress(this.parent, onprogressEvent);
    }

    /// <summary>
    /// Raises the FSCommand event.
    /// </summary>
    /// <param name="command">A <see cref="String"/> with the command to be sent.</param>
    /// <param name="args">A <see cref="String"/> with the comman arguments.</param>
    public virtual void FSCommand(string command, string args)
    {
      AxFlashFSCommandEvent fscommandEvent = new AxFlashFSCommandEvent(command, args);
      this.parent.RaiseOnFSCommand(this.parent, fscommandEvent);
    }

    /// <summary>
    /// Raises the FlashCall event.
    /// </summary>
    /// <param name="request">A <see cref="String"/> with the flash request , xml formatted.</param>
    public virtual void FlashCall(string request)
    {
      AxFlashCallEvent flashcallEvent = new AxFlashCallEvent(request);
      this.parent.RaiseOnFlashCall(this.parent, flashcallEvent);
    }
  }
}
