// <copyright file="BitmapEventArgs.cs" company="FU Berlin">
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

namespace OgamaControls
{
  using System;
  using System.Collections.Generic;
  using System.ComponentModel;
  using System.Drawing;
  using System.Text;

  /// <summary>
  /// Delegate. Handles bitmap occured event.
  /// </summary>
  /// <param name="sender">Source of the event.</param>
  /// <param name="e">A <see cref="BitmapEventArgs"/> with the bitmap</param>
  public delegate void BitmapEventHandler(object sender, BitmapEventArgs e);

  /// <summary>
  /// Derived from <see cref="System.EventArgs"/>
  /// Class that contains the data for the TrialEventOccured event. Derives from System.EventArgs.
  /// </summary>
  public class BitmapEventArgs : EventArgs
  {
    ///////////////////////////////////////////////////////////////////////////////
    // Defining Variables, Enumerations, Events                                  //
    ///////////////////////////////////////////////////////////////////////////////
    #region FIELDS
    /// <summary>
    /// The <see cref="Bitmap"/> to be send.
    /// </summary>
    private readonly Bitmap bitmap;

    #endregion //FIELDS

    ///////////////////////////////////////////////////////////////////////////////
    // Construction and Initializing methods                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region CONSTRUCTION

    /// <summary>
    /// Initializes a new instance of the BitmapEventArgs class.
    /// </summary>
    /// <param name="newBitmap">A new available <see cref="Bitmap"/>.</param>
    public BitmapEventArgs(Bitmap newBitmap)
    {
      this.bitmap = newBitmap;
    }

    #endregion //CONSTRUCTION

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Properties                                                       //
    ///////////////////////////////////////////////////////////////////////////////
    #region PROPERTIES

    /// <summary>
    /// Gets the <see cref="Bitmap"/>.
    /// </summary>
    /// <value>A new <see cref="Bitmap"/>.</value>
    public Bitmap Bitmap
    {
      get { return this.bitmap; }
    }

    #endregion //PROPERTIES
  }
}
