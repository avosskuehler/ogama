// <copyright file="TrialEvent.cs" company="FU Berlin">
// ******************************************************
// OGAMA - open gaze and mouse analyzer 
// Copyright (C) 2015 Dr. Adrian Voßkühler  
// ------------------------------------------------------------------------
// This program is free software; you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation; either version 2 of the License, or (at your option) any later version.
// This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
// You should have received a copy of the GNU General Public License along with this program; if not, write to the Free Software Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA 02111-1307 USA
// **************************************************************
// </copyright>
// <author>Adrian Voßkühler</author>
// <email>adrian@ogama.net</email>

namespace Ogama.Modules.Common.TrialEvents
{
  using System;

  /// <summary>
  /// This class encapsulates properties of trial events,
  /// like time, type and parameters of event.
  /// There are two implementations <see cref="MediaEvent"/> and
  /// <see cref="InputEvent"/>.
  /// </summary>
  [Serializable]
  public class TrialEvent
  {
    /// <summary>
    /// Saves the subject name the events trial belongs to.
    /// </summary>
    private string subjectName;

    /// <summary>
    /// Saves the trial sequence number the event belongs to.
    /// </summary>
    private int trialSequence;

    /// <summary>
    /// Saves the event id, that is used to link from the raw data to the event list.
    /// </summary>
    private int id;

    /// <summary>
    /// Saves the time with respect to the trials starting time this event occured.
    /// </summary>
    private long time;

    /// <summary>
    /// Saves an optional parameter description of this event.
    /// </summary>
    private string param;

    /// <summary>
    /// The <see cref="EventType"/> of this event.
    /// </summary>
    private EventType type;

    /// <summary>
    /// Gets or sets the subject name the events trial belongs to.
    /// </summary>
    public string SubjectName
    {
      get { return this.subjectName; }
      set { this.subjectName = value; }
    }

    /// <summary>
    /// Gets or sets the trial sequence number the event belongs to.
    /// </summary>
    public int TrialSequence
    {
      get { return this.trialSequence; }
      set { this.trialSequence = value; }
    }

    /// <summary>
    /// Gets or sets the <see cref="EventType"/> of this event.
    /// </summary>
    public EventType Type
    {
      get { return this.type; }
      set { this.type = value; }
    }

    /// <summary>
    /// Gets or sets the event id, that is used to link from the raw data to the event list.
    /// </summary>
    public int EventID
    {
      get { return this.id; }
      set { this.id = value; }
    }

    /// <summary>
    /// Gets or sets the time with respect to the trials starting time this event occured.
    /// </summary>
    public long Time
    {
      get { return this.time; }
      set { this.time = value; }
    }

    /// <summary>
    /// Gets or sets an optional parameter description of this event.
    /// </summary>
    public string Param
    {
      get { return this.param; }
      set { this.param = value; }
    }
  }
}
