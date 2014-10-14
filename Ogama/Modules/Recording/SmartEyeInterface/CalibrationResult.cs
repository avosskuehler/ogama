// <copyright file="CalibrationResult.cs" company="Smart Eye AB">
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

using SmartEye.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ogama.Modules.Recording.SmartEyeInterface
{
  /// <summary>
  /// Calibration result class with fields that describe the calibration statistics
  /// </summary>
  public class CalibrationResult
  {
    ///////////////////////////////////////////////////////////////////////////////
    // Defining Properties                                                       //
    ///////////////////////////////////////////////////////////////////////////////
    #region PROPERTIES

    /// <summary>
    /// Id of the calibration point
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Target screen point of the calibration point
    /// </summary>
    public Point2D Target { get; set; }

    /// <summary>
    /// Left samples of the calibration point
    /// </summary>
    public List<Point2D> SamplesLeft { get; set; }

    /// <summary>
    /// Right samples of the calibration point
    /// </summary>
    public List<Point2D> SamplesRight { get; set; }

    /// <summary>
    /// Intersections of the gaze sampples with the screen
    /// </summary>
    public List<Point2D> Intersections { get; set; }

    /// <summary>
    /// Accurracy of the samples for the calibration point
    /// </summary>
    public double[] Accuracy { get; set; }

    /// <summary>
    /// Precision of the samples for the calibration point
    /// </summary>
    public double[] Precision { get; set; }

    #endregion PROPERTIES

    ///////////////////////////////////////////////////////////////////////////////
    // Construction and Initializing methods                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region CONSTRUCTION

    /// <summary>
    /// Initializes a new instance of the CalibrationResult class.
    /// </summary>
    public CalibrationResult()
    {
    }

    /// <summary>
    /// Initializes a new instance of the CalibrationResult class.
    /// </summary>
    /// <param name="target">The calibration target as a <see cref="Point2D"/> value.</param>
    /// <param name="id">The calibration ID as a <see cref="int"/> value.</param>
    public CalibrationResult(Point2D target, int id)
    {
      Target = target;
      Id = id;
    }

    #endregion CONSTRUCTION
  }
}
