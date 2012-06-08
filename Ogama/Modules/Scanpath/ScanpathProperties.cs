// <copyright file="ScanpathProperties.cs" company="FU Berlin">
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

namespace Ogama.Modules.Scanpath
{
  using System;

  using Ogama.Modules.Scanpath.Colorization;

  /// <summary>
  /// This structure is designed to save the visualization style of 
  /// a subjects fixations.
  /// </summary>
  public struct ScanpathProperties
  {
    /// <summary>
    /// The name of the subject.
    /// </summary>
    public string SubjectName;

    /// <summary>
    /// The category of the subject.
    /// </summary>
    public string SubjectCategory;

    /// <summary>
    /// The <see cref="ColorizationStyle"/> for the gaze drawings.
    /// </summary>
    public ColorizationStyle GazeStyle;

    /// <summary>
    /// The <see cref="ColorizationStyle"/> for the mouse drawings.
    /// </summary>
    public ColorizationStyle MouseStyle;

    /// <summary>
    /// The <see cref="String"/> with the scanpath characters.
    /// </summary>
    public string ScanpathString;

    /// <summary>
    /// Initializes a new instance of the ScanpathProperties struct.
    /// </summary>
    /// <param name="newSubjectName">The name of the subject.</param>
    /// <param name="newSubjectCategory">The category of the subject.</param>
    /// <param name="newGazeStyle">The <see cref="ColorizationStyle"/> for the gaze drawings.</param>
    /// <param name="newMouseStyle">The <see cref="ColorizationStyle"/> for the mouse drawings.</param>
    /// <param name="newScanpathString">The <see cref="String"/> with the scanpath characters.</param>
    public ScanpathProperties(
      string newSubjectName,
      string newSubjectCategory,
      ColorizationStyle newGazeStyle,
      ColorizationStyle newMouseStyle,
      string newScanpathString)
    {
      this.SubjectName = newSubjectName;
      this.SubjectCategory = newSubjectCategory;
      this.GazeStyle = newGazeStyle;
      this.MouseStyle = newMouseStyle;
      this.ScanpathString = newScanpathString;
    }
  }
}