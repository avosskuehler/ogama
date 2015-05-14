// <copyright file="DataToTable.cs" company="FU Berlin">
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

namespace Ogama.Modules.Recording
{
  using System.Collections.Generic;

  using Ogama.DataSet;
  using Ogama.Modules.ImportExport.Common;

  /// <summary>
  /// This stores a set of raw data along with the table where the data
  /// should be written to.
  /// </summary>
  public struct DataToTable
  {
    /// <summary>
    /// A <see cref="List{RawData}"/> with the new data.
    /// </summary>
    public RawData[] RawDataList;

    /// <summary>
    /// A <see cref="SQLiteOgamaDataSet.RawdataDataTable"/> of the current subject
    /// where to write the data to.
    /// </summary>
    public SQLiteOgamaDataSet.RawdataDataTable RawDataTable;

    /// <summary>
    /// Initializes a new instance of the DataToTable structure.
    /// </summary>
    /// <param name="newRawData">A <see cref="List{RawData}"/> with the new data.</param>
    /// <param name="newRawDataTable">A <see cref="SQLiteOgamaDataSet.RawdataDataTable"/> of the current subject
    /// where to write the data to.</param>
    public DataToTable(List<RawData> newRawData, SQLiteOgamaDataSet.RawdataDataTable newRawDataTable)
    {
      this.RawDataList = newRawData.ToArray();
      this.RawDataTable = newRawDataTable;
    }
  }
}
