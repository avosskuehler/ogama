// <copyright file="AxFlashFSCommandEvent.cs" company="FU Berlin">
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

  /// <summary>
  /// Delegate for the FSCommand event.
  /// </summary>
  /// <param name="sender">The sender of the event.</param>
  /// <param name="e">An <see cref="AxFlashFSCommandEvent"/> with the event data.</param>
  public delegate void AxFlashFSCommandEventHandler(object sender, AxFlashFSCommandEvent e);

  /// <summary>
  /// The event arguments for the <see cref="AxFlash.FSCommand"/> event.
  /// </summary>
  public class AxFlashFSCommandEvent
  {
    /// <summary>
    /// Initializes a new instance of the AxFlashFSCommandEvent class.
    /// </summary>
    /// <param name="command">A <see cref="String"/> with the command to be sent.</param>
    /// <param name="args">A <see cref="String"/> with the command arguments.</param>
    public AxFlashFSCommandEvent(string command, string args)
    {
      this.Command = command;
      this.Arguments = args;
    }

    /// <summary>
    /// Gets or sets a <see cref="String"/> with the command to be sent.
    /// </summary>
    public string Command { get; set; }

    /// <summary>
    /// Gets or sets a <see cref="String"/> with the command arguments.
    /// </summary>
    public string Arguments { get; set; }
  }
}
