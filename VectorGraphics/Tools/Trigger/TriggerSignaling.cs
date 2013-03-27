// <copyright file="TriggerSignaling.cs" company="FU Berlin">
// ******************************************************
// OGAMA - open gaze and mouse analyzer 
// Copyright (C) 2013 Dr. Adrian Voßkühler  
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
  /// <summary>
  /// This is a list of possible trigger signaling states
  /// </summary>
  public enum TriggerSignaling
  {
    /// <summary>
    /// No trigger should be send
    /// </summary>
    None,

    /// <summary>
    /// Trigger should be send at the start of the slide
    /// </summary>
    Enabled,

    /// <summary>
    /// Trigger should be send at the start of the slide,
    /// all other triggers are disabled.
    /// </summary>
    Override,
  }
}
