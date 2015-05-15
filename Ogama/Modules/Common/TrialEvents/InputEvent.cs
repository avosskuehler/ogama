// <copyright file="InputEvent.cs" company="FU Berlin">
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

namespace Ogama.Modules.Common.TrialEvents
{
  using System;

  /// <summary>
  /// Derives from <see cref="TrialEvent"/>.
  /// Adds specific <see cref="InputEventTask"/> to the event properties.
  /// </summary>
  [Serializable]
  public class InputEvent : TrialEvent
  {
    /// <summary>
    /// Saves the <see cref="InputEventTask"/> of this <see cref="InputEvent"/>
    /// </summary>
    private InputEventTask task;

    /// <summary>
    /// Gets or sets the <see cref="InputEventTask"/> of this <see cref="InputEvent"/>.
    /// </summary>
    public InputEventTask Task
    {
      get { return this.task; }
      set { this.task = value; }
    }
  }
}
