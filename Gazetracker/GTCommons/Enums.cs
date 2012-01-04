// <copyright file="PresentationScreen.cs" company="ITU">
// ******************************************************
// GazeTrackingLibrary for ITU GazeTracker
// Copyright (C) 2010 Martin Tall  
// ------------------------------------------------------------------------
// This program is free software; you can redistribute it and/or modify it 
// under the terms of the GNU General Public License as published by the 
// Free Software Foundation; either version 3 of the License, 
// or (at your option) any later version.
// This program is distributed in the hope that it will be useful, 
// but WITHOUT ANY WARRANTY; without even the implied warranty of 
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU 
// General Public License for more details.
// You should have received a copy of the GNU General Public License 
// along with this program; if not, see http://www.gnu.org/licenses/.
// **************************************************************
// </copyright>
// <author>Adrian Voßkühler</author>
// <email>adrian.vosskuehler@fu-berlin.de</email>
// <modifiedby>Martin Tall</modifiedby>

namespace GTCommons.Enum
{
    /// <summary>
    /// Tracking method, Headmounted or Remote
    /// </summary>
    public enum TrackingMethodEnum
    {
        Headmounted = 1,
        RemoteBinocular = 2,
        RemoteMonocular = 3
    }

    /// <summary>
    /// Binocular or monocular tracking (one or two eyes)
    /// </summary>
    public enum TrackingModeEnum
    {
        Monocular = 1,
        Binocular = 2,
    }

    /// <summary>
    /// Monitor enumeration. Primary or Secondary
    /// </summary>
    public enum Monitor
    {
        /// <summary>
        /// Primary Monitor
        /// </summary>
        Primary,

        /// <summary>
        /// Secondary Montor
        /// </summary>
        Secondary
    }

    /// <summary>
    /// Video mode output, Normal or Processed
    /// </summary>
    public enum VideoModeEnum
    {
        RawNoTracking = 0,
        Normal = 1,
        Processed = 2,
    }

    public enum VideoRenderEnum
    {
        CPU = 0,
        GPU = 1,
    }

    public enum IRPlacementEnum
    {
        Below = 1,
        Above = 2,
        BelowAndAbove = 3,
        None = 4,
    }

    public enum CalibrationAlignmentEnum
    {
        Center = 1,
        Left = 2,
        Right = 3,
        Top = 4,
        Bottom = 5,
    }

    public enum EyeEnum
    {
        Left = 1,
        Right = 2,
        False = 3,
    }

    public enum LogLevel
    {
        Essential = 1,
        Full = 2,
    }

    public enum TrackDBFilter
    {
        All = 1,

        EyeNoneFound = 2,
        EyeLeftFound = 3,
        EyeRightFound = 4,
        EyeBothFound = 5,

        PupilNoneFound = 5,
        PupilLeftFound = 6,
        PupilRightFound = 7,
        PupilBothFound = 8,

        GlintNoneFound = 5,
        GlintLeftFound = 6,
        GlintRightFound = 7,
        GlintBothFound = 8,
    }

    public enum RecalibrationTypeEnum
    {
        None = 0,
        Offset = 1,
        Continuous = 2,
        Full = 3,
    }

    public enum CalibrationTypeEnum
    {
        PupilOnly = 0,
        DiffVector = 1,
        Homography = 2,
        HomographyPoly = 3,
    }
}