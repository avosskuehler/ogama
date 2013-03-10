// <copyright file="VisualizationModes.cs" company="FU Berlin">
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

namespace Ogama.Modules.AOI
{
  using System;

  /// <summary>
  /// Drawing modes for aoi module
  /// </summary>
  [Flags]
  public enum VisualizationModes
  {
    /// <summary>
    /// No visualization mode specified.
    /// </summary>
    None = 0,

    /// <summary>
    /// Visualize sum of all fixation times
    /// </summary>
    FixationTime = 1,

    /// <summary>
    /// Visualize the number of fixations
    /// </summary>
    NumberOfFixations = 2,

    /// <summary>
    /// Visualize the average fixation duration
    /// </summary>
    AverageFixationDuration = 4,

    /// <summary>
    /// Visualize transitions between AOIs in percentage values of current trial.
    /// </summary>
    RelativeTransitions = 8,

    /// <summary>
    /// Visualize transitions between AOIs in absolute transition values.
    /// </summary>
    AbsoluteTransitions = 16,
  }
}
