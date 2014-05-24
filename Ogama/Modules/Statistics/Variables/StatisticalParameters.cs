// <copyright file="StatisticalParameters.cs" company="FU Berlin">
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

namespace Ogama.Modules.Statistics.Variables
{
  using System;

  /// <summary>
  /// Flags. Selection values for subject parameters.
  /// </summary>
  [Flags]
  public enum SubjectParams
  {
    /// <summary>
    /// No subject variable specified.
    /// </summary>
    None = 0,

    /// <summary>
    /// The subjects name.
    /// </summary>
    SubjectName = 1,

    /// <summary>
    /// The subjects category.
    /// </summary>
    Category = 2,

    /// <summary>
    /// The subjects age.
    /// </summary>
    Age = 4,

    /// <summary>
    /// The subjects sex.
    /// </summary>
    Sex = 8,

    /// <summary>
    /// Standard subject parameters, which are name, category, age and sex
    /// </summary>
    Standard = 15,

    /// <summary>
    /// The handedness of the subject.
    /// </summary>
    Handedness = 16,

    /// <summary>
    /// The comments section for the subjects,
    /// </summary>
    Comments = 32,

    /// <summary>
    /// The custom variables for the subjects,
    /// </summary>
    Custom = 64,

    /// <summary>
    /// All possible subject variables.
    /// </summary>
    All = 127
  }

  /// <summary>
  /// Flags. Selection values for trial parameters.
  /// </summary>
  [Flags]
  public enum TrialParams
  {
    /// <summary>
    /// Do not calculate trial parameters 
    /// </summary>
    None = 0,

    /// <summary>
    /// The unique trial id.
    /// </summary>
    TrialID = 1,

    /// <summary>
    /// The trial sequence number of the trial in the subjects presentation.
    /// </summary>
    Sequence = 2,

    /// <summary>
    /// The name of the trial
    /// </summary>
    Name = 4,

    /// <summary>
    /// Trial duration in milliseconds
    /// </summary>
    Duration = 8,

    /// <summary>
    /// optional stimulus category, e.g. testing, practice, award
    /// </summary>
    Category = 16,

    /// <summary>
    /// Default parameters id, name, sequence, duration, category
    /// </summary>
    Standard = 31,

    /// <summary>
    /// Area of all areas of interest, with the "Target" category,
    /// in percent from screen size A value of -1 means:
    /// no "Target" AOI defined
    /// </summary>
    Targetsize = 32,

    /// <summary>
    /// Area of all areas of interest,
    /// except those marked as "SearchRect",
    /// in percent from screen size
    /// A value of -1 means:
    /// no AOIs defined
    /// A value of -2 means:
    /// no AOIs except "SearchRect" defined.
    /// </summary>
    AOISize = 64,

    /// <summary>
    /// A string with the event that stopped the trial (e.g. Mouse: Left)
    /// </summary>
    Response = 128,

    /// <summary>
    /// Did the subject decided correct ?
    /// 1 : correct answered
    /// 0 : wrong answered
    /// -1 : no testing conditions specified
    /// </summary>
    ResponseCorrectness = 256,

    /// <summary>
    /// Count of gaze samples, that have a value of (0,0).
    /// A value of 0  means:
    /// No gaze data is lost through missing 
    /// detection or blinks.
    /// </summary>
    Dataloss = 512,

    /// <summary>
    /// As Dataloss, but in counts per second
    /// </summary>
    DatalossPC = 1024,

    /// <summary>
    /// Count of valid gaze samples, that were out of screen area.
    /// </summary>
    OutOfMonitor = 2048,

    /// <summary>
    /// As OutOfMonitor, but in counts per second
    /// </summary>
    OutOfMonitorPC = 4096,

    /// <summary>
    /// During the revision this trial seemed to be not 
    /// reliable so it should been eliminated from further analysis.
    /// value = 0, if trial tables column "EliminateData"
    /// is null, otherwise value=1
    /// </summary>
    Elimination = 8192,

    /// <summary>
    /// Trial start time from the log files timing
    /// </summary>
    Starttime = 16384,

    /// <summary>
    /// If the response was a mouse click, 
    /// on which AOI and AOI group (if they were defined)
    /// does the subject has clicked.
    /// A value of -1 means:
    /// Response was not a mouse response.
    /// A value of -2 means:
    /// No AOIs defined.
    /// A value of -3 means:
    /// Mouse response was not at a AOI.
    /// </summary>
    ResponseAOI = 32768,

    /// <summary>
    /// All trial parameters.
    /// </summary>
    All = 65535,
  }

  /// <summary>
  /// Flags. Selection values for gaze parameters.
  /// </summary>
  [Flags]
  public enum GazeParams
  {
    /// <summary>
    /// Do not calculate gaze variables.
    /// </summary>
    None = 0,

    /// <summary>
    /// Number of fixations during each trial.
    /// </summary>
    Fixations = 1,

    /// <summary>
    /// as foresaid but per second
    /// </summary>
    FixationsPS = 2,

    /// <summary>
    /// Number of fixations during each trial (Count and per second)
    /// </summary>
    FixationCountBoth = 3,

    /// <summary>
    /// Sum of all fixation durations divided by fixation count.
    /// A value of -1 means: No fixation at all.
    /// </summary>
    FixationDurationMean = 4,

    /// <summary>
    /// Median of fixation durations.
    /// A value of -1 means: No fixation at all.
    /// </summary>
    /// <remarks>see http://www.statcan.gc.ca/edu/power-pouvoir/ch11/median-mediane/5214872-eng.htm for a description.</remarks>
    FixationDurationMedian = 8,

    /// <summary>
    /// Sum of fixation times divided by trial duration.
    /// (OK. This is not every time a real fixation - saccade ratio, but ...) 
    /// </summary>
    FixationSaccadeRatio = 16,

    /// <summary>
    /// Mean of all lengths of the straight line connections between consecutive fixations.
    /// </summary>
    AverageSaccadeLength = 32,

    /// <summary>
    /// Average saccade velocity calculated as mean of all saccades length/timespan values.
    /// </summary>
    AverageSaccadeVelocity = 64,

    /// <summary>
    /// Sum of the lengths of the straight line connections between consecutive 
    /// fixations.
    /// A value of -1 means:
    /// no gaze data 
    /// </summary>
    Pathlength = 128,

    /// <summary>
    /// As Pathlength but per second
    /// </summary>
    PathlengthPS = 256,

    /// <summary>
    /// Time until subjects first fixation at the area of interest quoted
    /// in AOI interface as "Searchrect"
    /// A value of 0  means:
    /// gaze was from the beginning on in this AOI
    /// A value of -1 means: 
    /// gaze was never over this AOI
    /// A value of -2 means: 
    /// no "Searchrect" AOI is defined
    /// </summary>
    TimeToFirstFixInSearchRect = 512,

    /// <summary>
    /// Time until subjects first fixation 
    /// at the area of interest quoted
    /// in AOI interface as "Target"
    /// A value of 0  means:
    /// gaze was from the beginning on in the AOI
    /// A value of -1 means: 
    /// gaze was never over this AOI
    /// A value of -2 means: no "Target" AOI is defined
    /// </summary>
    TimeToFirstFixAtTarget = 1024,

    /// <summary>
    /// Time until subjects second fixation 
    /// at the area of interest quoted
    /// in AOI interface as "Target"
    /// A value of -1 means: 
    /// gaze was never a second time over this AOI
    /// A value of -2 means: 
    /// no "Target" AOI is defined
    /// </summary>
    TimeToSecondFixAtTarget = 2048,

    /// <summary>
    /// Number of fixations for each trial
    /// until first mouse click.
    /// A value of -1 means:
    /// No left or right mouse click
    /// in this trial.
    /// </summary>
    FixationsUntilFirstMouseClick = 4096,

    /// <summary>
    /// Sum of fixation time at the area of interest quoted
    /// in AOI interface as "Target"
    /// A value of -1 means: 
    /// no "Target" AOI defined.
    /// </summary>
    TimeAtTarget = 8192,

    /// <summary>
    /// A custom variable specified with <see cref="GazeVariable"/>
    /// </summary>
    Custom = 16384,

    /// <summary>
    /// All gaze variables.
    /// </summary>
    All = 32767,
  }

  /// <summary>
  /// Flags. Selection values for mouse parameters.
  /// </summary>
  [Flags]
  public enum MouseParams
  {
    /// <summary>
    /// Do not calculate mouse variables.
    /// </summary>
    None = 0,

    /// <summary>
    /// Number of fixations during each trial.
    /// </summary>
    Fixations = 1,

    /// <summary>
    /// as foresaid but per second
    /// </summary>
    FixationsPS = 2,

    /// <summary>
    /// Number of fixations during each trial (Count and per second)
    /// </summary>
    FixationCountBoth = 3,

    /// <summary>
    /// Sum of all fixation durations divided by fixation count.
    /// A value of -1 means: No fixation at all.
    /// </summary>
    FixationDurationMean = 4,

    /// <summary>
    /// Median of fixation durations.
    /// A value of -1 means: No fixation at all.
    /// </summary>
    /// <remarks>see http://www.statcan.gc.ca/edu/power-pouvoir/ch11/median-mediane/5214872-eng.htm for a description.</remarks>
    FixationDurationMedian = 8,

    /// <summary>
    /// Sum of fixation times divided by trial duration.
    /// (OK. This is not every time a real fixation - saccade ratio, but ...) 
    /// </summary>
    FixationSaccadeRatio = 16,

    /// <summary>
    /// Mean of all lengths of the straight line connections between consecutive fixations.
    /// </summary>
    AverageSaccadeLength = 32,

    /// <summary>
    /// Average saccade velocity calculated as mean of all saccades length/timespan values.
    /// </summary>
    AverageSaccadeVelocity = 64,

    /// <summary>
    /// Length of the mouse path polyline in pixels.
    /// A value of -1 means:
    /// no mouse data 
    /// </summary>
    Pathlength = 128,

    /// <summary>
    /// Length of the mouse path polyline in pixels per second.
    /// </summary>
    PathlengthPS = 256,

    /// <summary>
    /// Time until subjects mouse position is 
    /// the first time at the area of interest quoted
    /// in AOI interface as "Searchrect"
    /// A value of 0  means:
    /// mouse position was from the beginning on in the AOI
    /// A value of -1 means: 
    /// mouse position was never over this AOI
    /// A value of -2 means: 
    /// no "Searchrect" AOI is defined
    /// </summary>
    TimeToFirstFixInSearchRect = 512,

    /// <summary>
    /// Time until subjects first fixation 
    /// at the area of interest quoted
    /// in AOI interface as "Target"
    /// A value of 0  means:
    /// gaze was from the beginning on in the AOI
    /// A value of -1 means: 
    /// gaze was never over this AOI
    /// A value of -2 means: no "Target" AOI is defined
    /// </summary>
    TimeToFirstFixAtTarget = 1024,

    /// <summary>
    /// Time until subjects second fixation 
    /// at the area of interest quoted
    /// in AOI interface as "Target"
    /// A value of -1 means: 
    /// gaze was never a second time over this AOI
    /// A value of -2 means: 
    /// no "Target" AOI is defined
    /// </summary>
    TimeToSecondFixAtTarget = 2048,

    /// <summary>
    /// Absolute number of left mouse clicks per trial.
    /// </summary>
    LeftClicks = 4096,

    /// <summary>
    /// As LeftClicks but per second
    /// </summary>
    LeftClicksPS = 8192,

    /// <summary>
    /// Absolute number of right mouse clicks per trial.
    /// </summary>
    RightClicks = 16384,

    /// <summary>
    /// As RightClicks but per second
    /// </summary>
    RightClicksPS = 32768,

    /// <summary>
    /// Time until subjects first mouse click.
    /// A value of -1 means: 
    /// mouse was never clicked
    /// </summary>
    TimeToFirstClick = 65536,

    /// <summary>
    /// Number of fixations for each trial
    /// until first mouse click.
    /// A value of -1 means:
    /// No left or right mouse click
    /// in this trial.
    /// </summary>
    FixationsUntilFirstMouseClick = 131072,

    /// <summary>
    /// Average distance of simultaneous samples
    /// between gaze and mouse position. 
    /// That is the sum of all phythagoras distances between
    /// a valid gaze and mouse point times the number of
    /// possible distance measurements.
    /// A value of -1  means:
    /// No distance could be calculated for various reasons
    /// </summary>
    AverageDistanceToGaze = 262144,

    /// <summary>
    /// Sum of fixation time at the area of interest quoted
    /// in AOI interface as "Target"
    /// A value of -1 means: 
    /// no "Target" AOI defined.
    /// </summary>
    TimeAtTarget = 524288,

    /// <summary>
    /// A custom variable specified with <see cref="MouseVariable"/>
    /// </summary>
    Custom = 1048576,

    /// <summary>
    /// All mouse variables.
    /// </summary>
    All = 2097151,
  }
}