// <copyright file="StimuliTypes.cs" company="FU Berlin">
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

namespace Ogama.Modules.SlideshowDesign
{
  using System;
  using System.Collections.Generic;
  using System.Text;

  /// <summary>
  /// Type of stimulus objects that can be created or displayed.
  /// Can be None, Shape, Instruction, Image, Flash.
  /// </summary>
  public enum StimuliTypes
  {
    /// <summary>
    /// Undefined stimulus.
    /// </summary>
    None,

    /// <summary>
    /// An empty stimulus.
    /// </summary>
    Blank,

    /// <summary>
    /// Shape stimuli like <see cref="VectorGraphics.Elements.VGPolyline"/>, <see cref="VectorGraphics.Elements.VGRectangle"/>
    /// </summary>
    Shape,

    /// <summary>
    /// Instructional stimuli (textual stimuli).
    /// </summary>
    Instruction,

    /// <summary>
    /// Textual stimuli based on RTF format
    /// </summary>
    RTFInstruction,

    /// <summary>
    /// Image stimuli, like pictures.
    /// </summary>
    Image,

    /// <summary>
    /// Macromedia Flash stimuli.
    /// </summary>
    Flash,

    /// <summary>
    /// Browser based stimuli.
    /// </summary>
    Browser,

    /// <summary>
    /// The stimulus is the desktop.
    /// </summary>
    Desktop,

    /// <summary>
    /// Different types of stimuli
    /// </summary>
    Mixed,
  }
}
