// <copyright file="StringLogicalComparer.cs" company="FU Berlin">
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
  using System;

  /// <summary>
  /// This class emulates StrCmpLogicalW, but not fully,
  /// which means it sorts file names logically not by string,
  /// which includes 1,2,3,4,...,10,11 not 1,10,100,2,3.
  /// It emulates the file name sorting of the windows explorer.
  /// </summary>
  public class StringLogicalComparer
  {
    /// <summary>
    /// This static method performs a string comparsion that
    /// is a file name sort.
    /// </summary>
    /// <param name="s1">The first <see cref="String"/> to compare.</param>
    /// <param name="s2">The second <see cref="String"/> to compare.</param>
    /// <returns>A signed integer that indicates the relative values of x and y
    /// Less than zero if x is less than y. 
    /// Zero if x equals y. 
    /// Greater than zero if x is greater than y.
    /// </returns>
    public static int Compare(string s1, string s2)
    {
      // get rid of special cases
      if ((s1 == null) && (s2 == null))
      {
        return 0;
      }

      if (s1 == null)
      {
        return -1;
      }

      if (s2 == null)
      {
        return 1;
      }

      if (s1.Equals(string.Empty) && s2.Equals(string.Empty))
      {
        return 0;
      }

      if (s1.Equals(string.Empty))
      {
        return -1;
      }

      if (s2.Equals(string.Empty))
      {
        return -1;
      }

      // WE style, special case
      bool sp1 = char.IsLetterOrDigit(s1, 0);
      bool sp2 = char.IsLetterOrDigit(s2, 0);
      if (sp1 && !sp2)
      {
        return 1;
      }

      if (!sp1 && sp2)
      {
        return -1;
      }

      int i1 = 0, i2 = 0; // current index
      int r = 0; // temp result
      while (true)
      {
        bool c1 = char.IsDigit(s1, i1);
        bool c2 = char.IsDigit(s2, i2);
        if (!c1 && !c2)
        {
          bool letter1 = char.IsLetter(s1, i1);
          bool letter2 = char.IsLetter(s2, i2);
          if ((letter1 && letter2) || (!letter1 && !letter2))
          {
            if (letter1 && letter2)
            {
              r = char.ToLower(s1[i1]).CompareTo(char.ToLower(s2[i2]));
            }
            else
            {
              r = s1[i1].CompareTo(s2[i2]);
            }

            if (r != 0)
            {
              return r;
            }
          }
          else if (!letter1 && letter2)
          {
            return -1;
          }
          else if (letter1 && !letter2)
          {
            return 1;
          }
        }
        else if (c1 && c2)
        {
          r = CompareNum(s1, ref i1, s2, ref i2);
          if (r != 0)
          {
            return r;
          }
        }
        else if (c1)
        {
          return -1;
        }
        else if (c2)
        {
          return 1;
        }

        i1++;
        i2++;

        if ((i1 >= s1.Length) && (i2 >= s2.Length))
        {
          return 0;
        }

        if (i1 >= s1.Length)
        {
          return -1;
        }

        if (i2 >= s2.Length)
        {
          return -1;
        }
      }
    }

    /// <summary>
    /// This method compares two strings with numbers.
    /// </summary>
    /// <param name="s1">The first string</param>
    /// <param name="i1">Ref. The index of the first string to start</param>
    /// <param name="s2">The second string</param>
    /// <param name="i2">Ref. The index of the second string to start</param>
    /// <returns>A signed integer that indicates the relative values of x and y
    /// Less than zero if x is less than y. 
    /// Zero if x equals y. 
    /// Greater than zero if x is greater than y.
    /// </returns>
    private static int CompareNum(string s1, ref int i1, string s2, ref int i2)
    {
      int nonZeroStart1 = i1, nonZeroStart2 = i2; // nz = non zero
      int end1 = i1, end2 = i2;

      ScanNumEnd(s1, i1, ref end1, ref nonZeroStart1);
      ScanNumEnd(s2, i2, ref end2, ref nonZeroStart2);
      int start1 = i1;
      i1 = end1 - 1;
      int start2 = i2; 
      i2 = end2 - 1;

      int nonZeroLength1 = end1 - nonZeroStart1;
      int nonZeroLength2 = end2 - nonZeroStart2;

      if (nonZeroLength1 < nonZeroLength2)
      {
        return -1;
      }
      else if (nonZeroLength1 > nonZeroLength2)
      {
        return 1;
      }

      for (int j1 = nonZeroStart1, j2 = nonZeroStart2; j1 <= i1; j1++, j2++)
      {
        int r = s1[j1].CompareTo(s2[j2]);
        if (r != 0)
        {
          return r;
        }
      }

      // the nz parts are equal
      int length1 = end1 - start1;
      int length2 = end2 - start2;
      if (length1 == length2)
      {
        return 0;
      }

      if (length1 > length2)
      {
        return -1;
      }

      return 1;
    }

    /// <summary>
    /// This method scans the given string for the numeric.
    /// </summary>
    /// <param name="s">The string to parse</param>
    /// <param name="start">The start index to search</param>
    /// <param name="end">Ref. The end index of the number.</param>
    /// <param name="nonZeroStart">Ref. The start index of the number.</param>
    private static void ScanNumEnd(string s, int start, ref int end, ref int nonZeroStart)
    {
      nonZeroStart = start;
      end = start;
      bool countZeros = true;
      while (char.IsDigit(s, end))
      {
        if (countZeros && s[end].Equals('0'))
        {
          nonZeroStart++;
        }
        else
        {
          countZeros = false;
        }

        end++;

        if (end >= s.Length)
        {
          break;
        }
      }
    }
  }
}
