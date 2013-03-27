// <copyright file="AttentionMapPicture.cs" company="FU Berlin">
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

namespace Ogama.Modules.AttentionMap
{
  using System;
  using System.Collections.Generic;
  using System.ComponentModel;
  using System.Data;
  using System.Drawing;
  using System.Drawing.Drawing2D;
  using System.Drawing.Imaging;
  using System.Text;
  using System.Windows.Forms;

  using Ogama.Modules.Common;
  using OgamaControls;

  using VectorGraphics;
  using VectorGraphics.Canvas;
  using VectorGraphics.Elements;

  /// <summary>
  /// Derived from <see cref="Picture"/>. 
  /// Used to display vector graphic elements of the Attention Map Interface.
  /// </summary>
  public partial class AttentionMapPicture : Picture
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

    /// <summary>
    /// Should hold data to calculate attention map. Normally a fixation table.
    /// </summary>
    private DataTable tableAttentionMap;

    /// <summary>
    /// Holds the current active gradient for the attention map.
    /// </summary>
    private Gradient gradient;

    /// <summary>
    /// Bitmap to fill with gradient to grab single color values.
    /// </summary>
    private PaletteBitmap colorMap;

    /// <summary>
    /// Bitmap to fill with heat map to overlay on background.
    /// </summary>
    private PaletteBitmap heatMap;

    #endregion //FIELDS

    ///////////////////////////////////////////////////////////////////////////////
    // Construction and Initializing methods                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region CONSTRUCTION

    /// <summary>
    /// Initializes a new instance of the AttentionMapPicture class.
    /// </summary>
    public AttentionMapPicture()
      : base()
    {
      this.InitializeComponent();
      this.InitializeElements();
    }

    #endregion //CONSTRUCTION

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Properties                                                       //
    ///////////////////////////////////////////////////////////////////////////////
    #region PROPERTIES

    /// <summary>
    /// Sets table holding the fixations data.
    /// </summary>
    /// <value>A <see cref="DataTable"/> with fixation rows.</value>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public DataTable AttentionMapTable
    {
      set
      {
        this.tableAttentionMap = value;
        ResetPicture();
      }
    }

    /// <summary>
    /// Sets the new generated gradient for the attention map.
    /// </summary>
    /// <value>A <see cref="Gradient"/> to use for the attention maps.</value>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Gradient Gradient
    {
      set
      {
        this.gradient = value;

        Bitmap colorMapBitmap = new Bitmap(
          this.colorMap.Width,
          this.colorMap.Height,
          PixelFormat.Format32bppArgb);

        // Cache the gradient by painting it onto a bitmap
        using (Graphics bitmapGraphics = Graphics.FromImage(colorMapBitmap))
        {
          Rectangle bmpRct = new Rectangle(0, 0, this.colorMap.Width, this.colorMap.Height);
          this.gradient.PaintGradientWithDirectionOverride(
            bitmapGraphics,
            bmpRct,
            LinearGradientMode.Horizontal);
        }

        if (this.colorMap != null)
        {
          this.colorMap.Dispose();
        }

        this.colorMap = new PaletteBitmap(colorMapBitmap);
      }
    }

    #endregion //PROPERTIES

    ///////////////////////////////////////////////////////////////////////////////
    // Public methods                                                            //
    ///////////////////////////////////////////////////////////////////////////////
    #region PUBLICMETHODS

    /// <summary>
    /// Performs calculation according to user interface settings
    /// with the weightened fixations using the length for multiplying the kernel.
    /// </summary>
    /// <param name="worker">background worker</param>
    /// <param name="e">Background worker event arguments</param>
    public void CalculateWeightedAttentionMap(BackgroundWorker worker, DoWorkEventArgs e)
    {
      this.DrawAttentionMap(true, worker, e);
    }

    /// <summary>
    /// Performs calculation according to user interface settings
    /// ignoring length informations.
    /// </summary>
    /// <param name="worker">background worker</param>
    /// <param name="e">Background worker event arguments</param>
    public void CalculateUnweightedAttentionMap(BackgroundWorker worker, DoWorkEventArgs e)
    {
      this.DrawAttentionMap(false, worker, e);
    }

    #endregion //PUBLICMETHODS

    ///////////////////////////////////////////////////////////////////////////////
    // Inherited methods                                                         //
    ///////////////////////////////////////////////////////////////////////////////
    #region OVERRIDES

    /// <summary>
    /// Overridden <see cref="Picture.InitalizeOverlayGraphics"/>. 
    /// Creates transparent bitmap for drawing and the corresponding graphics
    /// with correct transformation matrix.
    /// Updates heat map size with the presentation size.
    /// </summary>
    protected override void InitalizeOverlayGraphics()
    {
      base.InitalizeOverlayGraphics();
      if (this.heatMap.Width != this.PresentationSize.Width ||
         this.heatMap.Height != this.PresentationSize.Height)
      {
        this.CreateHeatMapBitmap(this.PresentationSize);
      }
    }

    /// <summary>
    /// Overridden. Frees resources of objects that are not disposed
    /// by the designer, mainly private objects.
    /// Is called during the call to <see cref="Control.Dispose(Boolean)"/>.
    /// </summary>
    protected override void CustomDispose()
    {
      if (this.tableAttentionMap != null)
      {
        this.tableAttentionMap.Dispose();
      }

      this.colorMap.Dispose();
      this.heatMap.Dispose();
      base.CustomDispose();
    }

    #endregion //OVERRIDES

    ///////////////////////////////////////////////////////////////////////////////
    // Methods for doing main class job                                          //
    ///////////////////////////////////////////////////////////////////////////////
    #region METHODS

    /// <summary>
    /// Initializes non designer fields.
    /// </summary>
    private void InitializeElements()
    {
      Bitmap colorMapBitmap = new Bitmap(AttentionMaps.NUMCOLORS, 1, PixelFormat.Format32bppArgb);
      this.colorMap = new PaletteBitmap(colorMapBitmap);

      this.CreateHeatMapBitmap(new Size(100, 100));
    }

    /// <summary>
    /// Performs the attention map calculation.
    /// </summary>
    /// <param name="weightened"><strong>True</strong>, if length of
    /// fixations should weighten the fixations, otherwise <strong>false</strong></param>
    /// <param name="worker">background worker</param>
    /// <param name="e">Background worker event arguments</param>
    private void DrawAttentionMap(bool weightened, BackgroundWorker worker, DoWorkEventArgs e)
    {
      this.Elements.Clear();

      // Read Eyetracker settings
      int eyeMonX = this.PresentationSize.Width;
      int eyeMonY = this.PresentationSize.Height;

      // init DistributionArray
      float[,] distributionArray = new float[eyeMonX, eyeMonY];

      // Calculate gaussian kernel for each entry in Fixationtable and add it to Distributionarray
      int counterRows = 0;
      foreach (DataRow row in this.tableAttentionMap.Rows)
      {
        float posX = Convert.ToSingle(row["PosX"]);
        float posY = Convert.ToSingle(row["PosY"]);
        float[,] kernelMultiplied = null;
        if (weightened)
        {
          // 1 ms = 1 kernelMaximum
          float factor = (int)row["Length"];
          kernelMultiplied = AttentionMaps.MultiplyKernel(factor, AttentionMaps.KernelSize);
        }
        else
        {
          // 1 Fixation = 1 kernelMaximum
          kernelMultiplied = AttentionMaps.DefaultKernel;
        }

        AttentionMaps.AddKernelToArray(distributionArray, (int)posX, (int)posY, eyeMonX, eyeMonY, AttentionMaps.KernelSize, kernelMultiplied);

        if (worker.CancellationPending)
        {
          e.Cancel = true;
          break;
        }
        else
        {
          // Report progress as a percentage of the total task.
          int percentComplete = Convert.ToInt32(Convert.ToSingle(counterRows) / this.tableAttentionMap.Rows.Count * 100);
          worker.ReportProgress(percentComplete, "Calculating Subject:" + row["SubjectName"].ToString());
        }

        counterRows++;
      }

      ////this.valueForMaxColor = 20;
      AttentionMaps.RescaleArray(distributionArray, -1);

      Bitmap heatMapBitmap = AttentionMaps.CreateHeatMap(
        this.heatMap,
        this.colorMap,
        new Size(eyeMonX, eyeMonY),
        distributionArray);
      VGImage newImage = new VGImage(heatMapBitmap, ImageLayout.Center, new Size(eyeMonX, eyeMonY));
      Elements.Add(newImage);
      heatMapBitmap.Dispose();

      this.DrawForeground(true);
    }

    /// <summary>
    ///  Creates a newly sized heatmap template to be filled 
    ///  with the data
    /// </summary>
    /// <param name="stimulusSize">A <see cref="Size"/> containing the new stimulus size.</param>
    private void CreateHeatMapBitmap(Size stimulusSize)
    {
      if (this.heatMap != null)
      {
        this.heatMap.Dispose();
        this.heatMap = null;
      }

      if (this.heatMap == null ||
        this.heatMap.Width != stimulusSize.Width ||
        this.heatMap.Height != stimulusSize.Height)
      {
        Bitmap heatMapBitmap = new Bitmap(stimulusSize.Width, stimulusSize.Height, PixelFormat.Format32bppArgb);
        this.heatMap = new PaletteBitmap(heatMapBitmap);
        heatMapBitmap.Dispose();
      }
    }

    #endregion //METHODS

    ///////////////////////////////////////////////////////////////////////////////
    // Small helping Methods                                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region HELPER
    #endregion //HELPER
  }
}
