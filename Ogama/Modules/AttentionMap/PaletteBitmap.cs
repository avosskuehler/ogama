// <copyright file="PaletteBitmap.cs" company="FU Berlin">
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
  using System.Drawing;
  using System.Drawing.Imaging;

  /// <summary>
  /// Class for creating color arrays from bitmaps to get
  /// a fast access to GetPixel and SetPixel function.
  /// </summary>
  public class PaletteBitmap : IDisposable
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
    /// Current bitmaps byte array.
    /// </summary>
    private byte[] imageData;

    /// <summary>
    /// Array of bitmap colors
    /// </summary>
    private Color[,] colorArray;

    /// <summary>
    /// width of bitmap
    /// </summary>
    private int width;

    /// <summary>
    /// Height of bitmap
    /// </summary>
    private int height;

    /// <summary>
    /// Current working bitmap
    /// </summary>
    private Bitmap workingImage;

    /// <summary>
    /// Rectangle of bitmap size
    /// </summary>
    private Rectangle rect;

    /// <summary>
    /// Flag. True if bitmap was modified
    /// </summary>
    private bool modified;

    /// <summary>
    /// Current bitmaps stride*height
    /// </summary>
    private int bytes;

    /// <summary>
    /// Current bitmaps stride
    /// </summary>
    private int stride;

    /// <summary>
    /// Current bitmaps pixel format
    /// </summary>
    private PixelFormat pixelFormat;

    /// <summary>
    /// Current bitmaps color palette
    /// </summary>
    private ColorPalette colorPalette;

    #endregion //FIELDS

    ///////////////////////////////////////////////////////////////////////////////
    // Construction and Initializing methods                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region CONSTRUCTION

    /// <summary>
    /// Initializes a new instance of the PaletteBitmap class.
    /// Initializes Color array for each pixel.
    /// </summary>
    /// <param name="bld">Bitmap to clone</param>
    public PaletteBitmap(Bitmap bld)
    {
      this.workingImage = (Bitmap)bld.Clone();
      this.SetValues();
    }

    /// <summary>
    /// Finalizes an instance of the PaletteBitmap class. 
    /// Disposes bitmap.
    /// </summary>
    ~PaletteBitmap()
    {
      this.Dispose();
    }

    #endregion //CONSTRUCTION

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Properties                                                       //
    ///////////////////////////////////////////////////////////////////////////////
    #region PROPERTIES

    /// <summary>
    /// Gets Width of bitmap.
    /// </summary>
    public int Width
    {
      get { return this.width; }
    }

    /// <summary>
    /// Gets bitmaps height.
    /// </summary>
    public int Height
    {
      get { return this.height; }
    }

    /// <summary>
    /// Gets or sets a new bitmap image
    /// </summary>
    public Bitmap Image
    {
      get
      {
        if (!this.modified)
        {
          return this.workingImage;
        }

        switch (this.pixelFormat)
        {
          case System.Drawing.Imaging.PixelFormat.Format32bppArgb:
            return this.ReturnFormat32BppArgb();
          case System.Drawing.Imaging.PixelFormat.Format24bppRgb:
            return this.ReturnFormat24BppRgb();
          case System.Drawing.Imaging.PixelFormat.Format8bppIndexed:
            // ReturnFormat8BppIndexed();
            break;
          case System.Drawing.Imaging.PixelFormat.Format4bppIndexed:
            // ReturnFormat4BppIndexed();
            break;
          case System.Drawing.Imaging.PixelFormat.Format1bppIndexed:
            // ReturnFormat1BppIndexed();
            break;
        }

        return null;
      }

      set
      {
        if (this.workingImage != null)
        {
          this.workingImage.Dispose();
        }

        this.workingImage = (Bitmap)value.Clone();
        this.SetValues();
      }
    }

    /// <summary>
    /// Gets array of bitmap colors.
    /// </summary>
    public Color[,] ColorArray
    {
      get { return this.colorArray; }
    }

    #endregion //PROPERTIES

    ///////////////////////////////////////////////////////////////////////////////
    // Public methods                                                            //
    ///////////////////////////////////////////////////////////////////////////////
    #region PUBLICMETHODS

    /// <summary>
    /// Implemented <see cref="IDisposable.Dispose()"/>
    /// releases all used resources.
    /// </summary>
    public void Dispose()
    {
      if (this.workingImage != null)
      {
        this.workingImage.Dispose();
        this.workingImage = null;
      }
    }

    /// <summary>
    /// Gets color of pixel at column x and line y 
    /// </summary>
    /// <param name="x">column value</param>
    /// <param name="y">line value</param>
    /// <returns>ARGB Color value</returns>
    public Color GetPixel(int x, int y)
    {
      if (x >= 0 && y >= 0)
      {
        return this.colorArray[x, y];
      }
      else
      {
        return this.colorArray[0, 0];
      }
    }

    /// <summary>
    /// Sets ARGB Color value at given position
    /// </summary>
    /// <param name="x">column value</param>
    /// <param name="y">line value</param>
    /// <param name="col">ARGB Color value</param>
    public void SetPixel(int x, int y, Color col)
    {
      this.colorArray[x, y] = col;
      this.modified = true;
    }

    /// <summary>
    /// Applies color array modifications to new bitmap
    /// </summary>
    /// <returns>Modified bitmap</returns>
    public Bitmap ReturnFormat24BppRgb()
    {
      for (int y = 0; y < this.height; y++)
      {
        for (int x = 0; x < this.width; x++)
        {
          this.imageData[y * this.stride + x * 3 + 2] = this.colorArray[x, y].R;
          this.imageData[y * this.stride + x * 3 + 1] = this.colorArray[x, y].G;
          this.imageData[y * this.stride + x * 3] = this.colorArray[x, y].B;
        }
      }

      System.Drawing.Imaging.BitmapData bmpData =
         this.workingImage.LockBits(
         this.rect,
         System.Drawing.Imaging.ImageLockMode.WriteOnly,
         this.workingImage.PixelFormat);
      IntPtr ptr = bmpData.Scan0;
      System.Runtime.InteropServices.Marshal.Copy(this.imageData, 0, ptr, this.bytes);
      this.workingImage.UnlockBits(bmpData);
      this.modified = false;
      return this.workingImage;
    }

    /// <summary>
    /// Applies color array modifications to new bitmap
    /// </summary>
    /// <returns>Modified bitmap</returns>
    public Bitmap ReturnFormat32BppArgb()
    {
      for (int y = 0; y < this.height; y++)
      {
        for (int x = 0; x < this.width; x++)
        {
          this.imageData[y * this.stride + x * 4 + 3] = this.colorArray[x, y].A;
          this.imageData[y * this.stride + x * 4 + 2] = this.colorArray[x, y].R;
          this.imageData[y * this.stride + x * 4 + 1] = this.colorArray[x, y].G;
          this.imageData[y * this.stride + x * 4] = this.colorArray[x, y].B;
        }
      }

      System.Drawing.Imaging.BitmapData bmpData =
         this.workingImage.LockBits(
         this.rect,
         System.Drawing.Imaging.ImageLockMode.WriteOnly,
         this.workingImage.PixelFormat);
      IntPtr ptr = bmpData.Scan0;
      System.Runtime.InteropServices.Marshal.Copy(this.imageData, 0, ptr, this.bytes);
      this.workingImage.UnlockBits(bmpData);
      this.modified = false;
      return this.workingImage;
    }

    #endregion //PUBLICMETHODS

    ///////////////////////////////////////////////////////////////////////////////
    // Eventhandler                                                              //
    ///////////////////////////////////////////////////////////////////////////////
    #region EVENTS

    ///////////////////////////////////////////////////////////////////////////////
    // Eventhandler for UI, Menu, Buttons, Toolbars etc.                         //
    ///////////////////////////////////////////////////////////////////////////////
    #region WINDOWSEVENTHANDLER
    #endregion //WINDOWSEVENTHANDLER

    ///////////////////////////////////////////////////////////////////////////////
    // Eventhandler for Custom Defined Events                                    //
    ///////////////////////////////////////////////////////////////////////////////
    #region CUSTOMEVENTHANDLER
    #endregion //CUSTOMEVENTHANDLER

    #endregion //EVENTS

    ///////////////////////////////////////////////////////////////////////////////
    // Methods and Eventhandling for Background tasks                            //
    ///////////////////////////////////////////////////////////////////////////////
    #region BACKGROUNDWORKER
    #endregion //BACKGROUNDWORKER

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
    /// Initializes new Bitmap and the color array.
    /// </summary>
    private void SetValues()
    {
      this.colorPalette = this.workingImage.Palette;
      this.pixelFormat = this.workingImage.PixelFormat;
      this.width = this.workingImage.Width;
      this.height = this.workingImage.Height;
      this.rect = new Rectangle(0, 0, this.width, this.height);
      BitmapData bmpData =
          this.workingImage.LockBits(
          this.rect,
          System.Drawing.Imaging.ImageLockMode.ReadOnly,
          this.workingImage.PixelFormat);

      IntPtr ptr = bmpData.Scan0;
      this.stride = bmpData.Stride;
      this.bytes = this.stride * this.height;
      this.imageData = new byte[this.bytes];
      System.Runtime.InteropServices.Marshal.Copy(ptr, this.imageData, 0, this.bytes);
      this.workingImage.UnlockBits(bmpData);
      this.colorArray = new Color[this.width, this.height];
      switch (this.pixelFormat)
      {
        case System.Drawing.Imaging.PixelFormat.Format32bppArgb:
          this.Format32BppArgb();
          break;
        case System.Drawing.Imaging.PixelFormat.Format24bppRgb:
          this.Format24BppRgb();
          break;
        case System.Drawing.Imaging.PixelFormat.Format8bppIndexed:
          this.Format8BppIndexed();
          break;
        case System.Drawing.Imaging.PixelFormat.Format4bppIndexed:
          this.Format4BppIndexed();
          break;
        case System.Drawing.Imaging.PixelFormat.Format1bppIndexed:
          this.Format1BppIndexed();
          break;
      }

      this.modified = false;
    }

    #endregion //METHODS

    ///////////////////////////////////////////////////////////////////////////////
    // Small helping Methods                                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region HELPER

    /// <summary>
    /// Formats current bitmap to 32BppArgb
    /// </summary>
    private void Format32BppArgb()
    {
      for (int y = 0; y < this.height; y++)
      {
        for (int x = 0; x < this.width; x++)
        {
          this.colorArray[x, y] = Color.FromArgb(
            this.imageData[y * this.stride + x * 4 + 3],
            this.imageData[y * this.stride + x * 4 + 2],
            this.imageData[y * this.stride + x * 4 + 1],
            this.imageData[y * this.stride + x * 4]);
        }
      }
    }

    /// <summary>
    /// Formats current bitmap to 24BppRgb.
    /// </summary>
    private void Format24BppRgb()
    {
      for (int y = 0; y < this.height; y++)
      {
        for (int x = 0; x < this.width; x++)
        {
          this.colorArray[x, y] = Color.FromArgb(
            this.imageData[y * this.stride + x * 3 + 2],
            this.imageData[y * this.stride + x * 3 + 1],
            this.imageData[y * this.stride + x * 3]);
        }
      }
    }

    /// <summary>
    /// Formats current bitmap to 8BppIndexed.
    /// </summary>
    private void Format8BppIndexed()
    {
      for (int y = 0; y < this.height; y++)
      {
        for (int x = 0; x < this.width; x++)
        {
          this.colorArray[x, y] = this.colorPalette.Entries[this.imageData[y * this.stride + x]];
        }
      }
    }

    /// <summary>
    /// Formats current bitmap to 4BppIndexed.
    /// </summary>
    private void Format4BppIndexed()
    {
      for (int y = 0; y < this.height; y++)
      {
        for (int x = 0; x < this.width; x++)
        {
          if (x % 2 == 0)
          {
            this.colorArray[x, y] = this.colorPalette.Entries[this.LowByte(this.imageData[y * this.stride + x / 2])];
          }
          else
          {
            this.colorArray[x, y] = this.colorPalette.Entries[this.HighByte(this.imageData[y * this.stride + x / 2])];
          }
        }
      }
    }

    /// <summary>
    /// Formats current bitmap to 1BppIndexed.
    /// </summary>
    private void Format1BppIndexed()
    {
      int rest = this.width % 8;
      byte bits;
      int x, y;
      for (y = 0; y < this.height; y++)
      {
        for (x = 0; x < this.width - 8; x += 8)
        {
          bits = this.imageData[y * this.stride + x / 8];
          this.colorArray[x, y] = this.colorPalette.Entries[(bits & 128) / 128];
          this.colorArray[x + 1, y] = this.colorPalette.Entries[(bits & 64) / 64];
          this.colorArray[x + 2, y] = this.colorPalette.Entries[(bits & 32) / 32];
          this.colorArray[x + 3, y] = this.colorPalette.Entries[(bits & 16) / 16];
          this.colorArray[x + 4, y] = this.colorPalette.Entries[(bits & 8) / 8];
          this.colorArray[x + 5, y] = this.colorPalette.Entries[(bits & 4) / 4];
          this.colorArray[x + 6, y] = this.colorPalette.Entries[(bits & 2) / 2];
          this.colorArray[x + 7, y] = this.colorPalette.Entries[bits & 1];
        }

        bits = this.imageData[y * this.stride + x / 8];
        int teiler = 128;
        for (int i = 0; i < rest; i++)
        {
          this.colorArray[x + i, y] = this.colorPalette.Entries[(bits & teiler) / teiler];
          teiler /= 2;
        }
      }
    }

    /// <summary>
    /// Returns high byte of given byte.
    /// </summary>
    /// <param name="zahl">byte value</param>
    /// <returns>integer with higher part of given byte</returns>
    private int HighByte(byte zahl)
    {
      return zahl >> 4;
    }

    /// <summary>
    /// Returns low byte of given byte.
    /// </summary>
    /// <param name="zahl">byte  value</param>
    /// <returns>integer with lower part of given byte</returns>
    private int LowByte(byte zahl)
    {
      return zahl & 15;
    }

    #endregion //HELPER
  }
}
