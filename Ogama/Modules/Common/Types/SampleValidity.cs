// <copyright file="SampleValidity.cs" company="FU Berlin">
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

namespace Ogama.Modules.Common.Types
{
  using System;

  /// <summary>
  /// An enumeration of the validity of readable data sample types.
  /// </summary>
  [Flags]
  public enum SampleValidity
  {
    /// <summary>
    /// No validity defined.
    /// </summary>
    None = 0,

    /// <summary>
    /// The sampling data is valid.
    /// </summary>
    Valid = 1,

    /// <summary>
    /// The sampling data is empty (0,0).
    /// </summary>
    Empty = 2,

    /// <summary>
    /// The sampling data is not set.
    /// </summary>
    Null = 4,

    /// <summary>
    /// The samples are out of the presentation size.
    /// </summary>
    OutOfStimulus = 8,
  }
}
