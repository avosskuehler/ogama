// <copyright file="Levenshtein.cs" company="FU Berlin">
// ******************************************************
// OGAMA - open gaze and mouse analyzer 
// Copyright (C) 2012 Adrian Voßkühler  
// ------------------------------------------------------------------------
// This program is free software; you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation; either version 2 of the License, or (at your option) any later version.
// This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
// You should have received a copy of the GNU General Public License along with this program; if not, write to the Free Software Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA 02111-1307 USA
// **************************************************************
// </copyright>
// <author>Adrian Voßkühler</author>
// <email>adrian@ogama.net</email>

namespace Ogama.Modules.Scanpath
{
  using System;
  using System.Collections.Generic;
  using System.Data;

  /// <summary>
  /// This static class is used to calculate the edit distance
  /// between two string in numerous methods.
  /// </summary>
  /// <remarks>Refer to Cetin Sert
  /// http://www.codeproject.com/cs/algorithms/Levenshtein.asp</remarks>
  public static class EditDistance
  {
    /// <summary>
    /// Compute Levenshtein distance
    /// GNU C version ported to C#
    /// Memory efficient version
    /// Cetin Sert, PHP Dev Team, Free Software Foundation
    /// http://www.koders.com/c/fid95F7F5D4792831FB74EB61BCD353ECD6DC38A794.aspx
    /// </summary>
    /// <param name="s">The first <see cref="String"/> that should be compared.</param>
    /// <param name="t">The second <see cref="String"/> that should be compared.</param>
    /// <returns>
    /// Levenshtein edit distance if that is not greater than 65535; otherwise -1
    /// </returns>
    public static unsafe int GNULevenshtein(string s, string t)
    {
      fixed (char* fps = s)
      fixed (char* fpt = t)
      {
        char* ps = fps;
        char* pt = fpt;
        int i, j, n;
        int l1, l2;
        char* p1, p2, tmp;

        /* skip equal start sequence, if any */
        if (s.Length >= t.Length)
        {
          while (*ps == *pt)
          {
            /* if we already used up one string,
             * then the result is the length of the other */
            if (*ps == '\0')
            {
              break;
            }

            ps++;
            pt++;
          }
        }
        else // sl < tl
        {
          while (*ps == *pt)
          {
            /* if we already used up one string,
             * then the result is the length of the other */
            if (*pt == '\0')
            {
              break;
            }

            ps++;
            pt++;
          }
        }

        /* length count #1*/
        l1 = s.Length - (int)(ps - fps);
        l2 = t.Length - (int)(pt - fpt);

        /* if we already used up one string, then
         the result is the length of the other */
        if (*ps == '\0')
        {
          return l2;
        }

        if (*pt == '\0')
        {
          return l1;
        }

        /* length count #2*/
        ps += l1;
        pt += l2;

        /* cut of equal tail sequence, if any */
        while (*--ps == *--pt)
        {
          l1--;
          l2--;
        }

        /* reset pointers, adjust length */
        ps -= l1++;
        pt -= l2++;

        /* possible dist to great? */

        // if ((l1 - l2 >= 0 ? l1 - l2 : -(l1 - l2)) >= char.MaxValue) return -1;
        if (Math.Abs(l1 - l2) >= char.MaxValue)
        {
          return -1;
        }

        /* swap if l2 longer than l1 */
        if (l1 < l2)
        {
          tmp = ps;
          ps = pt;
          pt = tmp;

          l1 ^= l2;
          l2 ^= l1;
          l1 ^= l2;
        }

        /* fill initial row */
        n = (*ps != *pt) ? 1 : 0;
        char* r = stackalloc char[l1 * 2];
        for (i = 0, p1 = r; i < l1; i++, *p1++ = (char)n++, p1++)
        {
          /*empty*/
        }

        /* calc. rowwise */
        for (j = 1; j < l2; j++)
        {
          /* initFileBasedDatabase pointers and col#0 */
          p1 = r + ((j & 1) == 0 ? 1 : 0);
          p2 = r + (j & 1);
          n = *p1 + 1;

          *p2++ = (char)n;
          p2++;
          pt++;

          /* foreach column */
          for (i = 1; i < l1; i++)
          {
            if (*p1 < n)
            {
              n = *p1 + (*(ps + i) != *pt ? 1 : 0);
            }

            /* replace cheaper than delete? */

            p1++;
            if (*++p1 < n)
            {
              n = *p1 + 1; /* insert cheaper then insert ? */
            }

            *p2++ = (char)n++; /* update field and cost for next col's delete */
            p2++;
          }
        }

        /* return result */
        return n - 1;
      }
    }

    /// <summary>
    /// Compute Levenshtein distance
    /// Single Dimensional array vector unsafe version
    /// Memory efficient version
    /// Cetin Sert, Sten Hjelmqvist
    /// http://www.codeproject.com/cs/algorithms/Levenshtein.asp
    /// </summary>
    /// <param name="s">The first <see cref="String"/> that should be compared.</param>
    /// <param name="t">The second <see cref="String"/> that should be compared.</param>
    /// <returns>Levenshtein edit distance</returns>
    public static unsafe int UnsafeVectorLevenshtein(string s, string t)
    {
      fixed (char* ps = s)
      fixed (char* pt = t)
      {
        int n = s.Length;       // length of s
        int m = t.Length;       // length of t
        int cost;               // cost

        // Step 1
        if (n == 0)
        {
          return m;
        }

        if (m == 0)
        {
          return n;
        }

        // Create the two vectors
        int* v0 = stackalloc int[n + 1];
        int* v1 = stackalloc int[n + 1];
        int* vTmp;

        // Step 2
        // Initialize the first vector
        for (int i = 1; i <= n; i++)
        {
          v0[i] = i;
        }

        // Step 3
        // For each column - unsafe
        for (int j = 1; j <= m; j++)
        {
          v1[0] = j;

          // Step 4
          for (int i = 1; i <= n; i++)
          {
            // Step 5
            cost = (ps[i - 1] == pt[j - 1]) ? 0 : 1;

            // Step 6
            int m_min = v0[i] + 1;
            int b = v1[i - 1] + 1;
            int c = v0[i - 1] + cost;

            if (b < m_min)
            {
              m_min = b;
            }

            if (c < m_min)
            {
              m_min = c;
            }

            v1[i] = m_min;
          }

          // Swap the vectors
          vTmp = v0;
          v0 = v1;
          v1 = vTmp;
        }

        // Step 7
        return v0[n];
      }
    }

    /// <summary>
    /// Compute Levenshtein distance
    /// Single Dimensional array vector version
    /// Memory efficient version
    /// Sten Hjelmqvist
    /// http://www.codeproject.com/cs/algorithms/Levenshtein.asp
    /// </summary>
    /// <param name="s">The first <see cref="String"/> that should be compared.</param>
    /// <param name="t">The second <see cref="String"/> that should be compared.</param>
    /// <returns>Levenshtein edit distance</returns>
    public static int VectorLevenshtein(string s, string t)
    {
      int n = s.Length;       // length of s
      int m = t.Length;       // length of t
      int cost;               // cost

      // Step 1
      if (n == 0)
      {
        return m;
      }

      if (m == 0)
      {
        return n;
      }

      // Create the two vectors
      int[] v0 = new int[n + 1];
      int[] v1 = new int[n + 1];
      int[] vTmp;

      // Step 2
      // Initialize the first vector
      for (int i = 1; i <= n; i++)
      {
        v0[i] = i;
      }

      // Step 3
      // Fore each column
      for (int j = 1; j <= m; j++)
      {
        // Set the 0'th element to the column number
        v1[0] = j;

        // Step 4
        // Fore each row
        for (int i = 1; i <= n; i++)
        {
          // Step 5
          cost = (s[i - 1] == t[j - 1]) ? 0 : 1;

          // Step 6
          // Find minimum
          int m_min = v0[i] + 1;
          int b = v1[i - 1] + 1;
          int c = v0[i - 1] + cost;

          if (b < m_min)
          {
            m_min = b;
          }

          if (c < m_min)
          {
            m_min = c;
          }

          v1[i] = m_min;
        }

        // Swap the vectors
        vTmp = v0;
        v0 = v1;
        v1 = vTmp;
      }

      // Step 7
      return v0[n];
    }

    /// <summary>
    /// Compute Levenshtein distance
    /// 2 Dimensional array matrix version
    /// </summary>
    /// <param name="s">The first <see cref="String"/> that should be compared.</param>
    /// <param name="t">The second <see cref="String"/> that should be compared.</param>
    /// <returns>Levenshtein edit distance</returns>
    public static int MatrixLevenshtein(string s, string t)
    {
      int[,] matrix;          // matrix
      int n = s.Length;       // length of s
      int m = t.Length;       // length of t
      int cost;               // cost

      // Step 1
      if (n == 0)
      {
        return m;
      }

      if (m == 0)
      {
        return n;
      }

      // Create matirx
      matrix = new int[n + 1, m + 1];

      // Step 2
      // Initialize
      for (int i = 0; i <= n; i++)
      {
        matrix[i, 0] = i;
      }

      for (int j = 0; j <= m; j++)
      {
        matrix[0, j] = j;
      }

      // Step 3
      for (int i = 1; i <= n; i++)
      {
        // Step 4
        for (int j = 1; j <= m; j++)
        {
          // Step 5
          if (s[i - 1] == t[j - 1])
          {
            cost = 0;
          }
          else
          {
            cost = 1;
          }

          // Step 6
          // Find minimum
          int min = matrix[i - 1, j] + 1;
          int b = matrix[i, j - 1] + 1;
          int c = matrix[i - 1, j - 1] + cost;

          if (b < min)
          {
            min = b;
          }

          if (c < min)
          {
            min = c;
          }

          matrix[i, j] = min;
        }
      }

      // Step 7
      return matrix[n, m];
    }

    /// <summary>
    /// Compute Levenshtein distance
    /// 2 Dimensional array matrix version
    /// </summary>
    /// <param name="s">The first <see cref="String"/> that should be compared.</param>
    /// <param name="t">The second <see cref="String"/> that should be compared.</param>
    /// <returns>Levenshtein edit distance</returns>
    public static int MatrixLevenshteinExtended(List<string> s, List<string> t)
    {
      int[,] matrix;          // matrix
      int n = s.Count;       // length of s
      int m = t.Count;       // length of t
      int cost;               // cost

      // Step 1
      if (n == 0)
      {
        return m;
      }

      if (m == 0)
      {
        return n;
      }

      // Create matirx
      matrix = new int[n + 1, m + 1];

      // Step 2
      // Initialize
      for (int i = 0; i <= n; i++)
      {
        matrix[i, 0] = i;
      }

      for (int j = 0; j <= m; j++)
      {
        matrix[0, j] = j;
      }

      // Step 3
      for (int i = 1; i <= n; i++)
      {
        // Step 4
        for (int j = 1; j <= m; j++)
        {
          // Step 5
          if (s[i - 1] == t[j - 1])
          {
            cost = 0;
          }
          else
          {
            cost = 1;
          }

          // Step 6
          // Find minimum
          int min = matrix[i - 1, j] + 1;
          int b = matrix[i, j - 1] + 1;
          int c = matrix[i - 1, j - 1] + cost;

          if (b < min)
          {
            min = b;
          }

          if (c < min)
          {
            min = c;
          }

          matrix[i, j] = min;
        }
      }

      // Step 7
      return matrix[n, m];
    }

    /// <summary>
    /// Compute Levenshtein distance
    /// Jagged array matrix version
    /// </summary>
    /// <param name="s">The first <see cref="String"/> that should be compared.</param>
    /// <param name="t">The second <see cref="String"/> that should be compared.</param>
    /// <returns>Levenshtein edit distance</returns>
    public static int JaggedLevenshtein(string s, string t)
    {
      int[][] matrix;         // matrix
      int n = s.Length;       // length of s
      int m = t.Length;       // length of t
      int cost;               // cost

      // Step 1
      if (n == 0)
      {
        return m;
      }

      if (m == 0)
      {
        return n;
      }

      // Create matirx
      matrix = new int[n + 1][];

      // Step 2
      // Initialize
      for (int i = 0; i <= n; i++)
      {
        matrix[i] = new int[m + 1];
        matrix[i][0] = i;
      }

      for (int j = 0; j <= m; j++)
      {
        matrix[0][j] = j;
      }

      // Step 3
      for (int i = 1; i <= n; i++)
      {
        // Step 4
        for (int j = 1; j <= m; j++)
        {
          // Step 5
          if (s[i - 1] == t[j - 1])
          {
            cost = 0;
          }
          else
          {
            cost = 1;
          }

          // Step 6
          // Find minimum
          int min = matrix[i - 1][j] + 1;
          int b = matrix[i][j - 1] + 1;
          int c = matrix[i - 1][j - 1] + cost;

          if (b < min)
          {
            min = b;
          }

          if (c < min)
          {
            min = c;
          }

          matrix[i][j] = min;
        }
      }

      // Step 7
      return matrix[n][m];
    }

    /// <summary>
    /// Compute Levenshtein distance
    /// Version implemented from iComp.
    /// </summary>
    /// <param name="a">The first <see cref="String"/> that should be compared.</param>
    /// <param name="b">The second <see cref="String"/> that should be compared.</param>
    /// <returns>Levenshtein edit distance</returns>
    /// <remarks>This method is from iComp (Heminghous)</remarks>
    public static int LevenshteiniComp(string a, string b)
    {
      int n, m, i, j, cost;
      n = a.Length;
      m = b.Length;
      int[,] d = new int[n + 1, m + 1];

      if (n == 0)
      {
        return m;
      }

      if (m == 0)
      {
        return n;
      }

      for (i = 0; i <= n; i++)
      {
        d[i, 0] = i;
      }

      for (j = 0; j <= m; j++)
      {
        d[0, j] = j;
      }

      for (i = 1; i <= n; i++)
      {
        for (j = 1; j <= m; j++)
        {
          cost = (a[i - 1] == b[j - 1]) ? 0 : 1;
          d[i, j] = Min(d[i - 1, j] + 1, d[i, j - 1] + 1, d[i - 1, j - 1] + cost);
        }
      }

      return d[n, m];
    }

    /// <summary>
    /// This method checks the number of matches of character similarities.
    /// </summary>
    /// <param name="a">The first <see cref="String"/> that should be compared.</param>
    /// <param name="b">The second <see cref="String"/> that should be compared.</param>
    /// <returns>The number of matches of character similarities.</returns>
    /// <remarks>This method is from iComp (Heminghous)</remarks>
    public static int Match(string a, string b)
    {
      int count = 0;
      for (int i = 0; i < a.Length; ++i)
      {
        if (a.IndexOf(a[i]) == i && b.IndexOf(a[i]) != -1)
        {
          count++;
        }
      }

      return count;
    }

    /// <summary>
    /// This method calculates the number of similar characters in the given string. 
    /// </summary>
    /// <param name="a">A <see cref="String"/> to check.</param>
    /// <returns>The number of distinct characters.</returns>
    /// <remarks>This method is from iComp (Heminghous)</remarks>
    public static int Distinct(string a)
    {
      int count = 0;
      for (int i = 0; i < a.Length; ++i)
      {
        if (a.IndexOf(a[i]) == i)
        {
          count++;
        }
      }

      return count;
    }

    /// <summary>
    /// Returns the character similarity of two strings,
    /// that means the percent of characters that are concordant in both strings.
    /// </summary>
    /// <remarks>This is the version implemented in iComp (Heminghous)</remarks>
    /// <param name="a">The first <see cref="String"/> that should be compared.</param>
    /// <param name="b">The second <see cref="String"/> that should be compared.</param>
    /// <returns>The character similarity of two strings in percent</returns>
    public static float CharacterSimilarityIComp(string a, string b)
    {
      SwapStrings(ref a, ref b);
      return (float)Match(a, b) / Math.Max(Distinct(a), Distinct(b));
    }

    /// <summary>
    /// Returns the character similarity of two strings,
    /// that means the percent of characters that are concordant in both strings.
    /// </summary>
    /// <param name="s">The first <see cref="String"/> that should be compared.</param>
    /// <param name="t">The second <see cref="String"/> that should be compared.</param>
    /// <returns>The character similarity of two strings in percent.</returns>
    public static float CharacterSimilarity(string s, string t)
    {
      float similarity = 0f;

      SwapStrings(ref s, ref t);

      List<char> theTList = new List<char>();
      theTList.AddRange(t.ToCharArray());
      theTList = RemoveDuplicateSections<char>(theTList);

      List<char> theSList = new List<char>();
      theSList.AddRange(s.ToCharArray());
      theSList = RemoveDuplicateSections<char>(theSList);

      int numberOfCharactersInT = theTList.Count;
      int numberOfCharactersInS = theSList.Count;

      int maxNumber = Math.Max(numberOfCharactersInT, numberOfCharactersInS);
      int concordancies = 0;
      foreach (char character in theSList)
      {
        if (theTList.Contains(character))
        {
          concordancies++;
          theTList.Remove(character);
        }
      }

      similarity = (float)concordancies / (maxNumber > 0 ? maxNumber : 1);

      return similarity;
    }

    /// <summary>
    /// Returns the character similarity of two strings,
    /// that means the percent of characters that are concordant in both strings.
    /// </summary>
    /// <param name="s">The first <see cref="String"/> that should be compared.</param>
    /// <param name="t">The second <see cref="String"/> that should be compared.</param>
    /// <returns>The character similarity of two strings in percent.</returns>
    public static float CharacterSimilarityExtended(List<string> s, List<string> t)
    {
      float similarity = 0f;

      SwapStringsExtended(ref s, ref t);

      List<string> theTList = new List<string>(t);
      theTList = RemoveDuplicateSections<string>(theTList);

      List<string> theSList = new List<string>(s);
      theSList = RemoveDuplicateSections<string>(theSList);

      int numberOfCharactersInT = theTList.Count;
      int numberOfCharactersInS = theSList.Count;

      int maxNumber = Math.Max(numberOfCharactersInT, numberOfCharactersInS);
      int concordancies = 0;
      foreach (string subString in theSList)
      {
        if (theTList.Contains(subString))
        {
          concordancies++;
          theTList.Remove(subString);
        }
      }

      similarity = (float)concordancies / (maxNumber > 0 ? maxNumber : 1);

      return similarity;
    }

    /// <summary>
    /// Returns the sequence similarity of two strings,
    /// that means the percent of character sequences that are concordant in both strings.
    /// </summary>
    /// <param name="s">The first <see cref="String"/> that should be compared.</param>
    /// <param name="t">The second <see cref="String"/> that should be compared.</param>
    /// <returns>The sequence similarity of two strings in percent.</returns>
    public static float SequenceSimilarity(string s, string t)
    {
      if (s.Length == 0 || t.Length == 0)
      {
        return 0;
      }

      SwapStrings(ref s, ref t);

      return (float)(1.0 - (float)MatrixLevenshtein(s, t) / s.Length);
    }

    /// <summary>
    /// Returns the sequence similarity of two strings,
    /// that means the percent of character sequences that are concordant in both strings.
    /// </summary>
    /// <param name="s">The first <see cref="String"/> that should be compared.</param>
    /// <param name="t">The second <see cref="String"/> that should be compared.</param>
    /// <returns>The sequence similarity of two strings in percent.</returns>
    public static float SequenceSimilarityExtended(List<string> s, List<string> t)
    {
      if (s.Count == 0 || t.Count == 0)
      {
        return 0;
      }

      SwapStringsExtended(ref s, ref t);

      return (float)(1.0 - (float)MatrixLevenshteinExtended(s, t) / s.Count);
    }

    /// <summary>
    /// This method calculates the minimum levenshtein distance,
    /// when the first string is ring shifted (shift the first character
    /// at the end and so on)
    /// </summary>
    /// <param name="s">The first <see cref="String"/> that should be compared.</param>
    /// <param name="t">The second <see cref="String"/> that should be compared.</param>
    /// <returns>The minimum levenshtein distance.</returns>
    public static int MinimumRotatedLevensthein(string s, string t)
    {
      int minimum = s.Length;
      string temp = s;
      if (s.Length > t.Length)
      {
        temp = s;
        s = t;
        t = temp;
      }

      for (int i = 0; i < s.Length; i++)
      {
        char firstCharacter = s[0];
        s = s.Remove(0, 1);
        s += firstCharacter;
        int distance = GNULevenshtein(s, t);
        if (distance < minimum)
        {
          minimum = distance;
        }
      }

      return minimum;
    }

    /// <summary>
    /// Computes the similarity of two scanpaths s and t. Fixation sites are
    /// defined using x and y coordinates.
    /// </summary>
    /// <param name="fixationsOfS">A <see cref="DataView"/> with the fixations of the first path.</param>
    /// <param name="fixationsOfT">A <see cref="DataView"/> with the fixations of the second path.</param>
    /// <param name="modulator">For an explanation of modulator see the poster.</param>
    /// <returns>The similarity of two scanpaths s and t</returns>
    /// <remarks> Die Koordinate sind in Graden des Gesichtsfelds angegeben,
    /// Pixel-Koordinaten vom Tracker müssen also vorher projeziert werden.
    /// </remarks>
    public static float MalsburgDistance(
      DataView fixationsOfS,
      DataView fixationsOfT,
      float modulator)
    {
      // Number of fixations equals string length
      int n = fixationsOfS.Count;
      int m = fixationsOfT.Count;

      // Creates and initializes a new two-dimensional Array of type double.
      double[,] d = new double[n + 1, m + 1];

      // Iterate fixations
      for (int i = 0; i < n; i++)
      {
        DataRow fixationRowOfS = fixationsOfS[i].Row;
        float sX = !fixationRowOfS.IsNull("PosX") ? Convert.ToSingle(fixationRowOfS["PosX"]) : 0;
        float sY = !fixationRowOfS.IsNull("PosY") ? Convert.ToSingle(fixationRowOfS["PosY"]) : 0;
        int durationOfS = (int)fixationRowOfS["Length"];

        for (int j = 0; j < m; j++)
        {
          DataRow fixationRowOfT = fixationsOfT[j].Row;
          float tX = !fixationRowOfT.IsNull("PosX") ? Convert.ToSingle(fixationRowOfT["PosX"]) : 0;
          float tY = !fixationRowOfT.IsNull("PosY") ? Convert.ToSingle(fixationRowOfT["PosY"]) : 0;
          int durationOfT = (int)fixationRowOfT["Length"];

          ////* Great circle distance using the Vincenty formula for better
          //// * precision: */
          ////double d_lon = sFixations[i].PosX.Value - tFixations[j].PosX.Value;
          ////double cos_d_lon = Math.Cos(d_lon);
          ////double sin_d_lon = Math.Sin(d_lon);
          ////double cos_slat = Math.Cos(sFixations[i].PosY.Value);
          ////double cos_tlat = Math.Cos(tFixations[j].PosY.Value);
          ////double sin_slat = Math.Sin(sFixations[i].PosY.Value);
          ////double sin_tlat = Math.Sin(tFixations[j].PosY.Value);

          ////double distance = Math.Atan2(Math.Sqrt(Math.Pow(cos_tlat * Math.Sin(d_lon), 2)
          ////               + Math.Pow(cos_slat * sin_tlat - sin_slat * cos_tlat * cos_d_lon, 2)),
          ////          sin_slat * sin_tlat + cos_slat * cos_tlat * cos_d_lon);

          ////distance *= 180 / Math.PI;

          double distance = Math.Sqrt(Math.Pow(tX - sX, 2) + Math.Pow(tY - sY, 2));

          /* cortical magnification */
          distance = Math.Pow(modulator, distance);

          /* cost for substituting */
          double cost = Math.Abs((durationOfT - durationOfS) * distance) +
                (durationOfT + durationOfS) * (1.0 - distance);

          /* selection of edit operation */
          d[i + 1, j + 1] = Math.Min(
            d[i, j + 1] + durationOfS,
            Math.Min(d[i + 1, j] + durationOfT, d[i, j] + cost));
        }
      }

      float result = (float)d[n, m];
      result /= n + m;

      return result;
    }

    /// <summary>
    /// Swap strings if s is shorter than t.
    /// </summary>
    /// <param name="s">Ref. The first <see cref="String"/></param>
    /// <param name="t">Ref. The second <see cref="String"/></param>
    private static void SwapStrings(ref string s, ref string t)
    {
      // Swap strings if s is shorter than t.
      string temp = s;
      if (s.Length < t.Length)
      {
        temp = s;
        s = t;
        t = temp;
      }
    }

    /// <summary>
    /// Swap strings if s is shorter than t. Used for lists of strings.
    /// </summary>
    /// <param name="s">Ref. The first <see cref="List{String}"/></param>
    /// <param name="t">Ref. The second <see cref="List{String}"/></param>
    private static void SwapStringsExtended(ref List<string> s, ref List<string> t)
    {
      // Swap strings if s is shorter than t.
      List<string> temp = s;
      if (s.Count < t.Count)
      {
        temp = s;
        s = t;
        t = temp;
      }
    }

    /// <summary>
    /// This static method removes duplicate sections in the given list of sections.
    /// </summary>
    /// <typeparam name="T">The type of the list items.</typeparam>
    /// <param name="sections">A List of sections to be removed
    /// from duplicates.</param>
    /// <returns>The cleaned List</returns>
    private static List<T> RemoveDuplicateSections<T>(List<T> sections)
    {
      List<T> finalList = new List<T>();
      foreach (T currValue in sections)
      {
        if (!finalList.Contains(currValue))
        {
          finalList.Add(currValue);
        }
      }

      return finalList;
    }

    /// <summary>
    /// This method compares three <see cref="Int32"/>and returns the minimum.
    /// </summary>
    /// <param name="a">An <see cref="Int32"/> to be compared with b,c.</param>
    /// <param name="b">An <see cref="Int32"/> to be compared with a,c.</param>
    /// <param name="c">An <see cref="Int32"/> to be compared with a,b.</param>
    /// <returns>The minimum under all three parameters.</returns>
    private static int Min(int a, int b, int c)
    {
      return (a < b) ? ((a < c) ? a : c) : ((b < c) ? b : c);
    }

    /// <summary>
    /// Compute Damerau - Levenshtein distance
    /// </summary>
    /// <param name="src">The first <see cref="String"/> that should be compared.</param>
    /// <param name="dest">The second <see cref="String"/> that should be compared.</param>
    /// <returns>Damerau Levenshtein edit distance</returns>
    private static int DamerauLevenshteinDistance(string src, string dest)
    {
      int[,] d = new int[src.Length + 1, dest.Length + 1];
      int i, j, cost;
      char[] str1 = src.ToCharArray();
      char[] str2 = dest.ToCharArray();

      for (i = 0; i <= str1.Length; i++)
      {
        d[i, 0] = i;
      }

      for (j = 0; j <= str2.Length; j++)
      {
        d[0, j] = j;
      }

      for (i = 1; i <= str1.Length; i++)
      {
        for (j = 1; j <= str2.Length; j++)
        {
          if (str1[i - 1] == str2[j - 1])
          {
            cost = 0;
          }
          else
          {
            cost = 1;
          }

          // Deletion, Insertion, Substitution
          d[i, j] = Math.Min(d[i - 1, j] + 1, Math.Min(d[i, j - 1] + 1, d[i - 1, j - 1] + cost));

          if ((i > 1) && (j > 1) && (str1[i - 1] == str2[j - 2]) && (str1[i - 2] == str2[j - 1]))
          {
            d[i, j] = Math.Min(d[i, j], d[i - 2, j - 2] + cost);
          }
        }
      }

      return d[str1.Length, str2.Length];
    }

    ////Java code for Mannan linear distance
    ////public double mannanDistance(int x, int y)
    //////*****************************
    ////// Compute Mannan distance
    //////*****************************
    //////based on Mannan et al (1995)
    ////{
    ////double d[ ][ ]; // matrix
    ////Point spA[ ]; // first scanpath (an array of points)
    ////Point spB[ ]; // second scanpath
    ////int n; // length of first
    ////int m; // length of second
    ////int i; // iterates through first
    ////int j; // iterates through second
    ////double d1i = 0;
    //////the sum of squared distances from the ith fixation
    ////in the 1st scanpath to its nearest neighbour
    ////double d2j = 0;
    //////the sum of squared distances from the jth fixation
    ////in the 2nd scanpath to its nearest neighbour
    ////double md; // the normalized, mean linear distance
    //////make a matrix of the distance between each of the
    ////fixations
    //////the lowest in each column/row gives the nearest neighbour
    ////distance
    ////d=new double [n][m];
    ////for(i=0;i<n;i++)
    ////{
    ////for(j=0;j<m;j++)
    ////{
    //////get the distance
    ////313
    ////d[i][j] = spA[i].distance( spB[j] );
    ////}
    ////}
    ////double lowest;
    ////for(i=0;i<n;i++)
    ////{
    ////lowest=100000;
    ////for(j=0;j<m;j++)
    ////{
    ////if (d[i][j]<lowest)
    ////{
    ////lowest=d[i][j];
    ////}
    ////}
    //////sum the squared distances from A to B
    ////d1i=d1i+(lowest*lowest);
    ////}
    ////for(j=0;j<m;j++)
    ////{
    ////lowest=100000;
    ////for(i=0;i<n;i++)
    ////{
    ////if (d[i][j]<lowest)
    ////{
    ////lowest=d[i][j];
    ////}
    ////}
    //////sum the squared distances from B to A
    ////d2j=d2j+(lowest*lowest);
    ////}
    //////multiply the sum of squares by the length of each scanpath
    ////double dsquared = (n*d2j)+(m*d1i);
    //////normalise over the display size
    ////dsquared = dsquared / ((2*n*m)*((x*x)+(y*y)));
    ////md = Math.sqrt(dsquared);
    ////return md;
    ////}
  }
}