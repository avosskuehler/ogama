// <copyright file="AOIStatistic.cs" company="FU Berlin">
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

namespace Ogama.Modules.AOI
{
  using System;
  using System.Collections.Generic;

  /// <summary>
  /// This structure saves some basic information of fixation
  /// statistic for a single Area of Interest.
  /// </summary>
  public class AOIStatistic
  {
    ///////////////////////////////////////////////////////////////////////////////
    // Defining Constants                                                        //
    ///////////////////////////////////////////////////////////////////////////////
    #region CONSTANTS
    #endregion //CONSTANTS

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Variables, Enumerations, Events                                  //
    ///////////////////////////////////////////////////////////////////////////////
    #region FIELDS

    /// <summary>
    /// The name of the aoi the statistic belongs to
    /// </summary>
    private string aoiName;

    /// <summary>
    /// The number of fixations at this aoi.
    /// </summary>
    private int fixationCount;

    /// <summary>
    /// The sum of all fixation durations at the aoi in ms.
    /// </summary>
    private double sumOfTimeOfAllFixationDurations;

    /// <summary>
    /// The fixation duration mean in ms on the aoi.
    /// </summary>
    private double fixationDurationMean;

    /// <summary>
    /// The fixation duration median ms on the aoi.
    /// </summary>
    private double fixationDurationMedian;

    /// <summary>
    /// The saccade duration median in ms on the aoi.
    /// </summary>
    private double saccadeDuration;

    /// <summary>
    /// The saccade length median in ms on the aoi.
    /// </summary>
    private double saccadeLength;

    /// <summary>
    /// The saccade velocity median in ms on the aoi.
    /// </summary>
    private double saccadeVelocity;

    /// <summary>
    /// The dictionary of times the AOI was hitted for the n-th time.
    /// </summary>
    private Dictionary<int, long> hitTimes;

    /// <summary>
    /// The start time of the fixation that was inside the given
    /// aoicollection after it was outside for the first time.
    /// </summary>
    private long firstHitTimeAfterBeeingOutside;

    #endregion //FIELDS

    ///////////////////////////////////////////////////////////////////////////////
    // Construction and Initializing methods                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region CONSTRUCTION

    /// <summary>
    /// Initializes a new instance of the AOIStatistic class.
    /// </summary>
    public AOIStatistic()
    {
      this.hitTimes = new Dictionary<int, long>();
    }

    #endregion //CONSTRUCTION

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Enumerations                                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region ENUMS
    #endregion ENUMS

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Properties                                                       //
    ///////////////////////////////////////////////////////////////////////////////
    #region PROPERTIES

    /// <summary>
    /// Gets or sets The name of the aoi the statistic belongs to
    /// </summary>
    public string AoiName
    {
      get { return this.aoiName; }
      set { this.aoiName = value; }
    }

    /// <summary>
    /// Gets or sets the number of fixations at this aoi.
    /// </summary>
    public int FixationCount
    {
      get { return this.fixationCount; }
      set { this.fixationCount = value; }
    }

    /// <summary>
    /// Gets or sets the sum of all fixation times at the aoi in ms.
    /// </summary>
    public double SumOfTimeOfAllFixations
    {
      get { return this.sumOfTimeOfAllFixationDurations; }
      set { this.sumOfTimeOfAllFixationDurations = value; }
    }

    /// <summary>
    /// Gets or sets the fixation duration mean in ms on the aoi.
    /// </summary>
    public double FixationDurationMean
    {
      get { return this.fixationDurationMean; }
      set { this.fixationDurationMean = value; }
    }

    /// <summary>
    /// Gets or sets the fixation duration median in ms on the aoi.
    /// </summary>
    public double FixationDurationMedian
    {
      get { return this.fixationDurationMedian; }
      set { this.fixationDurationMedian = value; }
    }

    /// <summary>
    /// Gets or sets the saccade duration mean in ms on the aoi.
    /// </summary>
    public double SaccadeDuration
    {
      get { return this.saccadeDuration; }
      set { this.saccadeDuration = value; }
    }

    /// <summary>
    /// Gets or sets the saccade length mean in ms on the aoi.
    /// </summary>
    public double SaccadeLength
    {
      get { return this.saccadeLength; }
      set { this.saccadeLength = value; }
    }

    /// <summary>
    /// Gets or sets the saccade velocity mean in ms on the aoi.
    /// </summary>
    public double SaccadeVelocity
    {
      get { return this.saccadeVelocity; }
      set { this.saccadeVelocity = value; }
    }

    /// <summary>
    /// Gets or sets the dictionary of times the AOI was hitted for the n-th time.
    /// </summary>
    public Dictionary<int, long> HitTimes
    {
      get { return this.hitTimes; }
      set { this.hitTimes = value; }
    }

    /// <summary>
    /// Gets or sets the start time of the fixation that was inside the given
    /// aoicollection after it was outside for the first time.
    /// </summary>
    public long FirstHitTimeAfterBeeingOutside
    {
      get { return this.firstHitTimeAfterBeeingOutside; }
      set { this.firstHitTimeAfterBeeingOutside = value; }
    }

    #endregion //PROPERTIES

    ///////////////////////////////////////////////////////////////////////////////
    // Public methods                                                            //
    ///////////////////////////////////////////////////////////////////////////////
    #region PUBLICMETHODS
    #endregion //PUBLICMETHODS

    ///////////////////////////////////////////////////////////////////////////////
    // Inherited methods                                                         //
    ///////////////////////////////////////////////////////////////////////////////
    #region OVERRIDES
    #endregion //OVERRIDES

    ///////////////////////////////////////////////////////////////////////////////
    // Eventhandler                                                              //
    ///////////////////////////////////////////////////////////////////////////////
    #region EVENTS

    ///////////////////////////////////////////////////////////////////////////////
    // Eventhandler for UI, Menu, Buttons, Toolbars etc.                         //
    ///////////////////////////////////////////////////////////////////////////////
    #region WINDOWSEVENTHANDLER
    #endregion //WINDOWSEVENTHANDLER

    ///////////////////////////////////////////////////////////////////////////////
    // Eventhandler for Custom Defined Events                                    //
    ///////////////////////////////////////////////////////////////////////////////
    #region CUSTOMEVENTHANDLER
    #endregion //CUSTOMEVENTHANDLER

    #endregion //EVENTS

    ///////////////////////////////////////////////////////////////////////////////
    // Methods and Eventhandling for Background tasks                            //
    ///////////////////////////////////////////////////////////////////////////////
    #region BACKGROUNDWORKER
    #endregion //BACKGROUNDWORKER

    ///////////////////////////////////////////////////////////////////////////////
    // Methods for doing main class job                                          //
    ///////////////////////////////////////////////////////////////////////////////
    #region PRIVATEMETHODS
    #endregion //PRIVATEMETHODS

    ///////////////////////////////////////////////////////////////////////////////
    // Small helping Methods                                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region HELPER
    #endregion //HELPER
  }
}
