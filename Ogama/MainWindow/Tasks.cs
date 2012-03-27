// <copyright file="Tasks.cs" company="FU Berlin">
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

namespace Ogama.MainWindow
{
  using System;
  using System.Collections.Generic;
  using System.Text;

  /// <summary>
  /// A task that can be performed in OGAMA.
  /// Used to create the referring module after loading the experiment.
  /// </summary>
  public enum Tasks
  {
    /// <summary>
    /// Shows the <see cref="Ogama.Modules.Replay.ReplayModule"/> module.
    /// </summary>
    Analyze,

    /// <summary>
    /// Shows the <see cref="Ogama.Modules.Database.DatabaseModule"/> and starts the import process.
    /// </summary>
    Import,

    /// <summary>
    /// Shows the <see cref="Ogama.Modules.SlideshowDesign.SlideshowModule"/>.
    /// </summary>
    Design,

    /// <summary>
    /// Shows the <see cref="Ogama.Modules.Recording.RecordModule"/> module.
    /// </summary>
    Record,
  }
}
