// <copyright file="AxFlashReadyStateChangeEvent.cs" company="FU Berlin">
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
  /// Delegate for the OnReadyStateChange event.
  /// </summary>
  /// <param name="sender">The sender of the event.</param>
  /// <param name="e">An <see cref="AxFlashReadyStateChangeEvent"/> with the event data.</param>
  public delegate void AxFlashReadyStateChangeEventHandler(object sender, AxFlashReadyStateChangeEvent e);

  /// <summary>
  /// The event arguments for the <see cref="AxFlash.OnReadyStateChange"/> event.
  /// </summary>
  public class AxFlashReadyStateChangeEvent
  {
    /// <summary>
    /// Initializes a new instance of the AxFlashReadyStateChangeEvent class.
    /// </summary>
    /// <param name="newState">An <see cref="Int32"/> with the new state.</param>
    public AxFlashReadyStateChangeEvent(int newState)
    {
      this.NewState = newState;
    }

    /// <summary>
    /// Gets or sets an <see cref="Int32"/> with the new state.
    /// </summary>
    public int NewState { get; set; }
  }
}
