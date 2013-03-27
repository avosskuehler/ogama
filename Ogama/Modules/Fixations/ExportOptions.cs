// <copyright file="ExportOptions.cs" company="FU Berlin">
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
  using System;
  using System.Collections.Generic;
  using System.Text;

  /// <summary>
  /// This class encapsulates properties of how to
  /// export fixations or saccades from the fixations module.
  /// </summary>
  public struct ExportOptions
  {
    /// <summary>
    /// Indicates whether to export the gaze data.
    /// </summary>
    public bool ExportGaze;

    /// <summary>
    /// Indicates whether to export the mouse data.
    /// </summary>
    public bool ExportMouse;

    /// <summary>
    /// Indicates whether to export the fixations or saccades.
    /// </summary>
    public bool ExportFixations;

    /// <summary>
    /// Indicates whether to export subject details
    /// </summary>
    public bool ExportSubjectDetails;

    /// <summary>
    /// Indicates whether to export trial name and category.
    /// </summary>
    public bool ExportTrialDetails;

    /// <summary>
    /// Indicates whether to export AOI hits and group.
    /// </summary>
    public bool ExportAOIDetails;

    /// <summary>
    /// The file with full path to the gaze export file.
    /// </summary>
    public string GazeFileName;

    /// <summary>
    /// The file with full path to the mouse export file.
    /// </summary>
    public string MouseFileName;

    /// <summary>
    /// The list of subjects that should be included in the export.
    /// </summary>
    public List<string> CheckedSubjects;

    /// <summary>
    /// The list of trials that should be included in the export.
    /// </summary>
    public List<int> CheckedTrialIDs;
  }
}
