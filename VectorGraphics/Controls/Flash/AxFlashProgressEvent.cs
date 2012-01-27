// <copyright file="AxFlashProgressEvent.cs" company="FU Berlin">
// ******************************************************
// OGAMA - open gaze and mouse analyzer 
// Copyright (C) 2012 Adrian Voßkühler  
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
  /// Delegate for the OnProgress event.
  /// </summary>
  /// <param name="sender">The sender of the event.</param>
  /// <param name="e">An <see cref="AxFlashProgressEvent"/> with the event data.</param>
  public delegate void AxFlashProgressEventHandler(object sender, AxFlashProgressEvent e);

  /// <summary>
  /// The event arguments for the <see cref="AxFlash.OnProgress"/> event.
  /// </summary>
  public class AxFlashProgressEvent
  {
    /// <summary>
    /// Initializes a new instance of the AxFlashProgressEvent class.
    /// </summary>
    /// <param name="percentDone">An <see cref="Int32"/> with the percentDone.</param>
    public AxFlashProgressEvent(int percentDone)
    {
      this.PercentDone = percentDone;
    }

    /// <summary>
    /// Gets or sets an <see cref="Int32"/> with the percent done.
    /// </summary>
    public int PercentDone { get; set; }
  }
}
