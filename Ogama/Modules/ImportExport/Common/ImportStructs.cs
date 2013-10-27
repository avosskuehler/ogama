// <copyright file="ImportStructs.cs" company="FU Berlin">
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

namespace Ogama.Modules.ImportExport.Common
{
  using System;

  using Ogama.Modules.Common.TrialEvents;

  using VectorGraphics.Elements;

  /// <summary>
  /// List of possible import types.
  /// </summary>
  /// <remarks>Used to reuse <see cref="ImportImagesDialog"/> and
  /// <see cref="ImportTrialsDialog"/> for the fixations and rawdata import.</remarks>
  [Flags]
  public enum ImportTypes
  {
    /// <summary>
    /// The import file contains raw data rows.
    /// </summary>
    Rawdata = 1,

    /// <summary>
    /// The import file contains fixation rows.
    /// </summary>
    Fixations = 2,
  }

  /// <summary>
  /// List of import modes for stimuli image files.
  /// </summary>
  [Flags]
  public enum StimuliImportModes
  {
    /// <summary>
    /// Use iViewX message lines with stimuli files.
    /// </summary>
    /// <remarks>In the SMI iViewX logs one can insert MSG comment lines with the current
    /// stimulus bitmap filename through sending e.g. from presentation a
    /// tracker.send_command( "ET_BMP c:\\stimuli\\et_instruction.bmp")</remarks>
    UseiViewXMSG = 1,

    /// <summary>
    /// Use a table with a list of TrialID - Imagefile name assignments
    /// </summary>
    UseAssignmentTable = 2,

    /// <summary>
    /// Use a given column in the import file with file names.
    /// </summary>
    /// <remarks>This column has to have filenames without path.</remarks>
    UseImportColumn = 4,

    /// <summary>
    /// Search the import file for image endings 
    /// </summary>
    /// <remarks>This works when filenames have no spaces.</remarks>
    SearchForImageEnding = 8,
  }

  /// <summary>
  /// List of import modes for trial numbers.
  /// </summary>
  [Flags]
  public enum TrialSequenceImportModes
  {
    /// <summary>
    /// Use message lines to distinguish between trials
    /// </summary>
    /// <remarks>You can specify a trigger string for that mode.
    /// Every line that contains that trigger string starts a new trial.</remarks>
    UseMSGLines = 1,

    /// <summary>
    /// Use a table with a list of TrialID - Imagefile name assignments
    /// </summary>
    UseAssignmentTable = 2,

    /// <summary>
    /// Use a given column in the import file with increasing integers.
    /// </summary>
    /// <remarks>This column has to have an integer parsing possible format</remarks>
    UseImportColumn = 4,
  }

  /// <summary>
  /// List of import modes for AOI.
  /// </summary>
  [Flags]
  public enum AOIImportModes
  {
    /// <summary>
    /// Cast import file to simple rectangles.
    /// </summary>
    UseSimpleRectangles = 1,

    /// <summary>
    /// Use the ogama columns in the import file.
    /// </summary>
    UseOgamaColumns = 2,
  }

  /// <summary>
  /// List of import modes for fixations.
  /// </summary>
  [Flags]
  public enum FixationImportModes
  {
    /// <summary>
    /// Use a column with trial numbers
    /// </summary>
    UseTrialNumberColumn = 1,

    /// <summary>
    /// Use the lines that contain special quotations.
    /// </summary>
    UseMSGLines = 2,
  }

  /// <summary>
  /// This structure is used for serializing the settings of an import
  /// session to a xml file.
  /// </summary>
  [Serializable]
  public struct MergedSettings
  {
    /// <summary>
    /// The <see cref="ASCIISettings"/> part of the import settings.
    /// </summary>
    public ASCIISettings AsciiSetting;

    /// <summary>
    /// The <see cref="DetectionSettings"/> part of the import settings.
    /// </summary>
    public DetectionSettings DetectionSetting;
  }

  /// <summary>
  /// Subject data structure with fields that match the database columns.
  /// Used for saving subjects, before they are stored
  /// into the sql database.
  /// </summary>
  public struct SubjectsData
  {
    /// <summary>
    /// Subjects name
    /// </summary>
    public string SubjectName;

    /// <summary>
    /// Optional category for the subject
    /// </summary>
    /// <remarks>For example: politicians or nurses</remarks>
    public string Category;

    /// <summary>
    /// Age of subject
    /// </summary>
    public int? Age;

    /// <summary>
    /// Sex of subject.
    /// </summary>
    public string Sex;

    /// <summary>
    /// Handedness of subject.
    /// </summary>
    /// <remarks>For example: left handed or right handed</remarks>
    public string Handedness;

    /// <summary>
    /// Comments on subjects entry
    /// </summary>
    public string Comments;
  }

  /// <summary>
  /// Trial data structure with fields that match the database columns.
  /// Used for saving trials, before they are stored
  /// into the sql database.
  /// </summary>
  public struct TrialsData
  {
    /// <summary>
    /// Subjects name
    /// </summary>
    public string SubjectName;

    /// <summary>
    /// Unique integer to intentify the trial
    /// </summary>
    public int TrialID;

    /// <summary>
    /// Name of the trial
    /// </summary>
    public string TrialName;

    /// <summary>
    /// Unique integer to intentify the position in this trial
    /// </summary>
    public int TrialSequence;

    /// <summary>
    /// Starting time in milliseconds of trial from the beginning of the session
    /// </summary>
    public long TrialStartTime;

    /// <summary>
    /// Trial duration in milliseconds.
    /// </summary>
    public int Duration;

    /// <summary>
    /// Optional image category for the trial
    /// </summary>
    /// <remarks>For example: test stimuli, animal stimuli or human stimuli </remarks>
    public string Category;

    /// <summary>
    /// Save wheter this trial should be eliminated from analysis.
    /// </summary>
    public bool? EliminateData;
  }

  /// <summary>
  /// Trial event data structure with fields that match the database columns.
  /// Used for saving trial events, before they are stored
  /// into the sql database.
  /// </summary>
  public struct TrialEventsData
  {
    /// <summary>
    /// Subjects name
    /// </summary>
    public string SubjectName;

    /// <summary>
    /// Unique integer to intentify the position in this trial
    /// </summary>
    public int TrialSequence;

    /// <summary>
    /// Unique ID of the event.
    /// </summary>
    public int EventID;

    /// <summary>
    /// The time the event occured, measured in time
    /// relative to the trials beginning.
    /// </summary>
    public long EventTime;

    /// <summary>
    /// The type of the event, is a
    /// <see cref="InputEvent"/> or <see cref="MediaEvent"/>
    /// </summary>
    public string EventType;

    /// <summary>
    /// One of the <see cref="InputEventTask"/> or
    /// <see cref="MediaEventTask"/> members.
    /// </summary>
    public string EventTask;

    /// <summary>
    /// An optional parameter that describes the event.
    /// </summary>
    public string EventParam;
  }

  /// <summary>
  /// Raw data structure with fields that match the database columns
  /// Used for saving raw data, before they are stored
  /// into the sql database.
  /// </summary>
  public struct RawData
  {
    /// <summary>
    /// Time in milliseconds from the start of the trial
    /// </summary>
    public long Time;

    /// <summary>
    /// Subjects name
    /// </summary>
    public string SubjectName;

    /// <summary>
    /// Unique integer to intentify the trial
    /// </summary>
    public int TrialSequence;

    /// <summary>
    /// Optional image category for the trial
    /// </summary>
    /// <remarks>For example: test stimuli, animal stimuli or human stimuli </remarks>
    public string Category;

    /// <summary>
    /// X-diameter of pupil
    /// </summary>
    public float? PupilDiaX;

    /// <summary>
    /// y-diameter of pupil
    /// </summary>
    public float? PupilDiaY;

    /// <summary>
    /// x-coordinate of gaze position in screen coordinates
    /// </summary>
    public float? GazePosX;

    /// <summary>
    /// y-coordinate of gaze position in screen coordinates
    /// </summary>
    public float? GazePosY;

    /// <summary>
    /// x-coordinate of mouse position in screen coordinates
    /// </summary>
    public float? MousePosX;

    /// <summary>
    /// y-coordinate of mouse position in screen coordinates
    /// </summary>
    public float? MousePosY;

    /// <summary>
    /// The id of an event occured at the given time.
    /// </summary>
    public int? EventID;
  }

  /// <summary>
  /// AOI data structure with fields that match the database columns.
  /// Used for saving areas of interest, before they are stored
  /// into the sql database.
  /// </summary>
  public struct AOIData
  {
    /// <summary>
    /// TrialID the AOI refers to.
    /// </summary>
    public int TrialID;

    /// <summary>
    /// Number of the slide in this trial this AOI refers to.
    /// </summary>
    public int SlideNr;

    /// <summary>
    /// The name of the AOI
    /// </summary>
    public string ShapeName;

    /// <summary>
    /// The type of the shape.
    /// </summary>
    public VGShapeType ShapeType;

    /// <summary>
    /// Number of shape points.
    /// </summary>
    public int ShapeNumPts;

    /// <summary>
    /// The string representation of the point list.
    /// </summary>
    public string ShapePts;

    /// <summary>
    /// An optional target description.
    /// </summary>
    public string Group;
  }

  /// <summary>
  /// Fixation data structure with fields that match the database columns
  /// Used for saving fixations, before they are stored
  /// into the sql database.
  /// </summary>
  public struct FixationData
  {
    /// <summary>
    /// Subjects name
    /// </summary>
    public string SubjectName;

    /// <summary>
    /// Unique integer to intentify the trial
    /// </summary>
    public int TrialID;

    /// <summary>
    /// Unique integer to intentify the trial order in each presentation.
    /// </summary>
    public int TrialSequence;

    /// <summary>
    /// Unique integer to intentify the position of the fixation in the trial
    /// </summary>
    public int CountInTrial;

    /// <summary>
    /// Starting time of the fixation
    /// </summary>
    public long StartTime;

    /// <summary>
    /// Fixation Duration
    /// </summary>
    public int Length;

    /// <summary>
    /// x-coordinate of fixation position in screen coordinates
    /// </summary>
    public float? PosX;

    /// <summary>
    /// y-coordinate of fixation position in screen coordinates
    /// </summary>
    public float? PosY;
  }
}
