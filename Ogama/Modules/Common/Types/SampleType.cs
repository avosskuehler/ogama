// <copyright file="SampleType.cs" company="FU Berlin">
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
  /// An enumeration of the readable data sample types.
  /// Currently these are gaze or mouse samples.
  /// </summary>
  [Flags]
  public enum SampleType
  {
    /// <summary>
    /// No sampling type defined.
    /// </summary>
    None = 0,

    /// <summary>
    /// The sampling data is from gaze movements.
    /// </summary>
    Gaze = 1,

    /// <summary>
    /// The sampling data is from mouse movements.
    /// </summary>
    Mouse = 2,

    /// <summary>
    /// There are both sampling data available, gaze and mouse.
    /// </summary>
    Both = 3,
  }
}
