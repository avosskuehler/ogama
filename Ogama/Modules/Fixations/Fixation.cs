// <copyright file="Fixation.cs" company="FU Berlin">
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

namespace Ogama.Modules.Fixations
{
  using Ogama.Modules.Common.Types;

  /// <summary>
  /// This class encapsulates properties of how to
  /// export fixations or saccades from the fixations module.
  /// </summary>
  public struct Fixation
  {
    /// <summary>
    /// The <see cref="SampleType"/> of the fixation.
    /// Can be gaze or mouse.
    /// </summary>
    public SampleType SampleType;

    /// <summary>
    /// The name of the subject this fixation belongs to.
    /// </summary>
    public string SubjectName;

    /// <summary>
    /// The trial id this fixation belongs to.
    /// </summary>
    public int TrialID;

    /// <summary>
    /// The trial sequence this fixation belongs to.
    /// </summary>
    public int TrialSequence;

    /// <summary>
    /// The one-based index of this fixation in the trial.
    /// </summary>
    public int CountInTrial;

    /// <summary>
    /// The starting time with respect to the beginning of the trial in ms.
    /// </summary>
    public long StartTime;

    /// <summary>
    /// The fixation duration.
    /// </summary>
    public long Length;

    /// <summary>
    /// The x-coordinate in pixel of the fixation center.
    /// </summary>
    public float PosX;

    /// <summary>
    /// The y-coordinate in pixel of the fixation center.
    /// </summary>
    public float PosY;
  }
}
