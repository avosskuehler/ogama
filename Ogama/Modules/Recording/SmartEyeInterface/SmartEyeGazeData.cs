// <copyright file="SmartEyeGazeData.cs" company="Smart Eye AB">
// ******************************************************
// OGAMA - open gaze and mouse analyzer 
// Copyright (C) 2014 Dr. Adrian Voßkühler  
// ------------------------------------------------------------------------
// This program is free software; you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation; either version 2 of the License, or (at your option) any later version.
// This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
// You should have received a copy of the GNU General Public License along with this program; if not, write to the Free Software Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA 02111-1307 USA
// **************************************************************
// </copyright>
// <author>Kathrin Scheil</author>
// <email>kathrin.scheil@smarteye.se</email>

namespace Ogama.Modules.Recording.SmartEyeInterface
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Text;

  /// <summary>
  /// Gaze data structure with fields that match the database columns
  /// that correspond to gaze data, plus additional fields for tracking quality
  /// </summary>
  public struct SmartEyeGazeData
  {
    /// <summary>
    /// Time in milliseconds
    /// </summary>
    public long Time;

    /// <summary>
    /// Time in 100 ns FILETIME as an absolute time since January 1, 1601 (UTC)
    /// </summary>
    public long RealTime;

    /// <summary>
    /// x-diameter of pupil
    /// </summary>
    public float? PupilDiaX;

    /// <summary>
    /// y-diameter of pupil
    /// </summary>
    public float? PupilDiaY;

    /// <summary>
    /// x-coordinate of gaze position in values ranging between 0..1
    /// </summary>
    /// <remarks>0 means left margin of presentation screen,
    /// 1 means right margin of presentation screen.</remarks>
    public float? GazePosX;

    /// <summary>
    /// y-coordinate of gaze position in values ranging between 0..1
    /// </summary>
    /// <remarks>0 means top margin of presentation screen,
    /// 1 means bottom margin of presentation screen.</remarks>
    public float? GazePosY;

    /// <summary>
    /// Gaze tracking quality
    /// </summary>
    public double? GazeQuality;

    /// <summary>
    /// Head tracking quality
    /// </summary>
    public double? HeadQuality;
  }
}
