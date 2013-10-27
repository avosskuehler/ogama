// <copyright file="LoopState.cs" company="FU Berlin">
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

namespace Ogama.Modules.Replay
{
using System;
using System.Drawing;

  /// <summary>
  /// Structure which saves state of replay loop in between refreshing cycles
  /// that are given from timer.
  /// </summary>
  public struct LoopState
  {
    /// <summary>
    /// Flag, whether sample data is out of monitor surface
    /// </summary>
    /// <remarks>That is detected in correspondance to the settings 
    /// <see cref="Properties.ExperimentSettings.WidthStimulusScreen"/> and 
    /// <see cref="Properties.ExperimentSettings.HeightStimulusScreen"/>.</remarks>
    public bool IsOutOfMonitor;

    /// <summary>
    /// Flag, true if samples have no data.
    /// </summary>
    public bool IsBlink;

    /// <summary>
    /// Point of last gaze fixation cenetr
    /// </summary>
    public PointF GazeLastFixCenter;

    /// <summary>
    /// Point of last mouse fixation center
    /// </summary>
    public PointF MouseLastFixCenter;

    /// <summary>
    /// counts the rows in the sampling data
    /// </summary>
    public int RowCounter;

    /// <summary>
    /// Begin of trial in ms
    /// </summary>
    /// <remarks>Time column entry of first sample</remarks>
    public long TrialStartTimeInMS;

    /// <summary>
    /// Processor time, when drawing startet.
    /// </summary>
    public DateTime ProcessBeginningTime;
  }
}
