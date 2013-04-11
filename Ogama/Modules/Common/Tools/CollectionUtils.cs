// <copyright file="CollectionUtils.cs" company="FU Berlin">
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

namespace Ogama.Modules.Common.Tools
{
  using System;
  using System.Collections.Generic;

  /// <summary>
  /// This class encapsulates a shuffling method for 
  /// collections.
  /// </summary>
  /// <typeparam name="T">Generic type parameter of the collection.</typeparam>
  public class CollectionUtils<T>
  {
    /// <summary>
    /// This static member initializes a random generator
    /// with the current time.
    /// </summary>
    private static Random rng = new Random();

    /// <summary>
    /// This method shuffles the given collection randomly.
    /// </summary>
    /// <param name="collection">The collection to shuffle.</param>
    /// <returns>The shuffled collection.</returns>
    public static ICollection<T> Shuffle(ICollection<T> collection)
    {
      T[] a = new T[collection.Count];
      collection.CopyTo(a, 0);
      byte[] b = new byte[a.Length];
      rng.NextBytes(b);
      Array.Sort(b, a);
      return new List<T>(a);
    }
  }
}