// <copyright file="NumericComparer.cs" company="FU Berlin">
// ******************************************************
// OGAMA - open gaze and mouse analyzer 
// Copyright (C) 2015 Dr. Adrian Voßkühler  
// ------------------------------------------------------------------------
// This program is free software; you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation; either version 2 of the License, or (at your option) any later version.
// This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
// You should have received a copy of the GNU General Public License along with this program; if not, write to the Free Software Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA 02111-1307 USA
// **************************************************************
// </copyright>
// <author>Vasian Cepa 2005</author>
// <modifiedby>Adrian Voßkühler</modifiedby>
// <email>adrian@ogama.net</email>

namespace Ogama.Modules.Common.Tools
{
  using System.Collections;
  using System.IO;

  /// <summary>
  /// This class implements <see cref="IComparer"/> for a numeric file name comparison.
  /// </summary>
  public class NumericComparer : IComparer
  {
    /// <summary>
    /// Performs a case-sensitive comparison of two objects of the same type and returns a 
    /// value indicating whether one is less than, equal to, or greater than the other.
    /// </summary>
    /// <param name="x">The first object to compare. </param>
    /// <param name="y">The second object to compare. </param>
    /// <returns>A signed integer that indicates the relative values of x and y
    /// Less than zero if x is less than y. 
    /// Zero if x equals y. 
    /// Greater than zero if x is greater than y.
    /// </returns>
    public int Compare(object x, object y)
    {
      if ((x is string) && (y is string))
      {
        return StringLogicalComparer.Compare((string)x, (string)y);
      }

      if ((x is FileInfo) && (y is FileInfo))
      {
        return StringLogicalComparer.Compare(((FileInfo)x).Name, ((FileInfo)y).Name);
      }

      return -1;
    }
  }
}