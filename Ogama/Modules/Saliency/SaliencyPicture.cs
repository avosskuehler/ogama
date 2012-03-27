// <copyright file="SaliencyPicture.cs" company="FU Berlin">
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

namespace Ogama.Modules.Saliency
{
  using System;
  using System.Collections.Generic;
  using System.ComponentModel;
  using System.Data;
  using System.Diagnostics;
  using System.Drawing;
  using System.Drawing.Drawing2D;
  using System.Drawing.Imaging;
  using System.Text;
  using System.Windows.Forms;

  using Ogama.ExceptionHandling;
  using Ogama.Modules.AttentionMap;
  using Ogama.Modules.Common;
  using Ogama.Modules.Common.PictureTemplates;
  using Ogama.Modules.Fixations;
  using Ogama.Modules.ImportExport;
  using OgamaControls;
  using VectorGraphics;
  using VectorGraphics.Elements;

  /// <summary>
  /// Derived from <see cref="PictureWithFixations"/>. 
  /// Used to display vector graphic elements of the Saliency Module.
  /// </summary>
  public partial class SaliencyPicture : PictureWithFixations
  {
    ///////////////////////////////////////////////////////////////////////////////
    // Defining Constants                                                        //
    ///////////////////////////////////////////////////////////////////////////////
    #region CONSTANTS
    #endregion //CONSTANTS

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Variables, Enumerations, Events                                  //
    ///////////////////////////////////////////////////////////////////////////////
    #region FIELDS
    #endregion //FIELDS

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Properties                                                       //
    ///////////////////////////////////////////////////////////////////////////////
    #region PROPERTIES
    #endregion //PROPERTIES

    ///////////////////////////////////////////////////////////////////////////////
    // Construction and Initializing methods                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region CONSTRUCTION

    /// <summary>
    /// Initializes a new instance of the SaliencyPicture class.
    /// </summary>
    public SaliencyPicture()
      : base()
    {
      this.InitializeComponent();
    }

    #endregion //CONSTRUCTION

    ///////////////////////////////////////////////////////////////////////////////
    // Eventhandler                                                              //
    ///////////////////////////////////////////////////////////////////////////////
    #region EVENTS

    ///////////////////////////////////////////////////////////////////////////////
    // Eventhandler for Custom Defined Events                                    //
    ///////////////////////////////////////////////////////////////////////////////
    #region CUSTOMEVENTHANDLER
    #endregion //CUSTOMEVENTHANDLER

    #endregion //EVENTS

    ///////////////////////////////////////////////////////////////////////////////
    // Inherited methods                                                         //
    ///////////////////////////////////////////////////////////////////////////////
    #region OVERRIDES
    #endregion //OVERRIDES

    ///////////////////////////////////////////////////////////////////////////////
    // Methods for doing main class job                                          //
    ///////////////////////////////////////////////////////////////////////////////
    #region METHODS

    /// <summary>
    /// This method calculates an heat map out of the 
    /// given channel file and overlays it on the
    /// background.
    /// </summary>
    /// <param name="channelFilename">A <see cref="string"/> with
    /// filename and full path of the channel image to use.</param>
    public void VisualizeChannelMapOverlay(string channelFilename)
    {
      Bitmap heatMap = AttentionMaps.CreateHeatMapFromBWImage(
        this.HeatMap,
        this.ColorMap,
        this.PresentationSize,
        channelFilename);
      VGImage newImage = new VGImage(heatMap, ImageLayout.Center, this.PresentationSize);

      this.Elements.Clear();
      this.Elements.Add(newImage);
      this.DrawFixationsForCurrentSubject();
      this.DrawForeground(true);
    }

    #endregion //METHODS

    ///////////////////////////////////////////////////////////////////////////////
    // Small helping Methods                                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region HELPER
    #endregion //HELPER
  }
}
