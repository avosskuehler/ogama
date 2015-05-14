// <copyright file="SequencedTrial.cs" company="FU Berlin">
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

namespace Ogama.Modules.Common.SlideCollections
{
  using System;

  /// <summary>
  /// This structure is used to describe a trial by its sequence number, name and id.
  /// It can be used to populate data bound combo boxes.
  /// </summary>
  public struct SequencedTrial
  {
    /// <summary>
    /// An <see cref="Int32"/> with the sequence order number of the trial
    /// in the subjects trial list.
    /// </summary>
    public int SequenceNumber;

    /// <summary>
    /// A <see cref="String"/> with the name of the trial.
    /// </summary>
    public string TrialName;

    /// <summary>
    /// The unique <see cref="Int32"/> trial ID.
    /// </summary>
    public int TrialID;

    /// <summary>
    /// Gets a formatted description of the current trial in the format:
    /// "SequenceNumber # TrialName (ID:TrialID)"
    /// </summary>
    /// <value>A <see cref="String"/> with the formatted description of the trial.</value>
    public string SequencedName
    {
      get
      {
        string cbbEntry = this.SequenceNumber.ToString();
        if (cbbEntry.Length == 1)
        {
          cbbEntry = "0" + cbbEntry;
        }

        if (cbbEntry.Length == 2)
        {
          cbbEntry = "0" + cbbEntry;
        }

        string idEntry = this.TrialID.ToString();
        if (idEntry.Length == 1)
        {
          idEntry = "0" + idEntry;
        }

        if (idEntry.Length == 2)
        {
          idEntry = "0" + idEntry;
        }

        cbbEntry += " # " + this.TrialName + " (ID:" + idEntry + ")";
        return cbbEntry;
      }
    }
  }
}
