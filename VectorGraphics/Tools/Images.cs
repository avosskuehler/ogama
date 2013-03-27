// <copyright file="Images.cs" company="FU Berlin">
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

namespace VectorGraphics.Tools
{
  using System;
  using System.Collections.Generic;
  using System.Drawing;
  using System.Drawing.Drawing2D;
  using System.Drawing.Imaging;
  using System.IO;
  using System.Text;
  using System.Windows.Forms;

  using VectorGraphics.Tools.Win32;

  /// <summary>
  /// A class for creating gray scale images and other image related stuff.
  /// </summary>
  /// <remarks>Some of the code in this class 
  /// SetGrayscalePalette and CreateGrayscaleImage
  /// is a part from the great:
  /// <para></para>
  /// AForge Image Processing Library
  /// <para></para>
  /// Copyright © Andrew Kirillov, 2005-2006
  /// andrew.kirillov@gmail.com.
  /// <para></para>
  /// Some others are adopted from the MSDN VB.NET GDI+ Images Sample</remarks>
  public class Images
  {
    ///////////////////////////////////////////////////////////////////////////////
    // Defining Constants                                                        //
    ///////////////////////////////////////////////////////////////////////////////
    #region CONSTANTS

    /// <summary>
    /// Index of red component
    /// </summary>
    public const short RGBR = 2;

    /// <summary>
    /// Index of green component
    /// </summary>
    public const short RGBG = 1;

    /// <summary>
    /// Index of blue component
    /// </summary>
    public const short RGBB = 0;

    /// <summary>
    /// Specifies the type of compression for a compressed 
    /// bottom-up bitmap (top-down DIBs cannot be compressed).
    /// In this case 0 means uncompressed.
    /// </summary>
    private const uint BIRGB = 0;

    /// <summary>
    /// Type of data contained in the
    /// bmiColors array member of the BITMAPINFO
    /// </summary>
    private const uint DIBRGBCOLORS = 0;

    #endregion //CONSTANTS

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Variables, Enumerations, Events                                  //
    ///////////////////////////////////////////////////////////////////////////////
    #region FIELDS
    #endregion //FIELDS

    ///////////////////////////////////////////////////////////////////////////////
    // Construction and Initializing methods                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region CONSTRUCTION
    #endregion //CONSTRUCTION

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Properties                                                       //
    ///////////////////////////////////////////////////////////////////////////////
    #region PROPERTIES
    #endregion //PROPERTIES

    ///////////////////////////////////////////////////////////////////////////////
    // Public methods                                                            //
    ///////////////////////////////////////////////////////////////////////////////
    #region PUBLICMETHODS

    /// <summary>
    /// Thumbnail creation callback.
    /// Currently not used
    /// </summary>
    /// <returns>always false</returns>
    public static bool ThumbnailCallback()
    {
      return false;
    }

    /// <summary>
    /// This method creates a <see cref="Bitmap"/> of given size 
    /// with the custom background color.
    /// </summary>
    /// <param name="size">The <see cref="Size"/> of the image.</param>
    /// <param name="whatWasNotFound">A <see cref="String"/> indicating the thing that was not found.</param>
    /// <returns>A <see cref="Bitmap"/> of given size 
    /// with gray background color.</returns>
    public static Bitmap CreateNotFoundImage(Size size, string whatWasNotFound)
    {
      Bitmap image = null;
      try
      {
        // create new image
        image = new Bitmap(size.Width, size.Height);
        using (Graphics graphics = Graphics.FromImage(image))
        {
          graphics.Clear(Color.Gray);
          SizeF textArea = graphics.MeasureString(whatWasNotFound + " not found", new Font("Verdana", 24));
          PointF topLeft = new PointF(x: size.Width / 2 - textArea.Width / 2, y: size.Height / 2 - textArea.Height / 2);
          graphics.DrawString(whatWasNotFound + " not found", new Font("Verdana", 24), Brushes.Red, topLeft);
        }
      }
      catch (Exception ex)
      {
        VGExceptionMethods.HandleExceptionSilent(ex);
      }

      return image;
    }

    /// <summary>
    /// This method creates a <see cref="Bitmap"/> of given size 
    /// with recording instructions.
    /// </summary>
    /// <param name="width">The width of the image.</param>
    /// <param name="height">The height of the image.</param>
    /// <returns>A <see cref="Bitmap"/> of given size 
    /// with recording instructions.</returns>
    public static Bitmap CreateRecordInstructionImage(int width, int height)
    {
      // create new image
      Bitmap image = new Bitmap(width, height, PixelFormat.Format32bppArgb);
      Graphics graphics = Graphics.FromImage(image);
      graphics.Clear(Color.Gray);
      Point center = new Point(
        width / 2 - Properties.Resources.RecordingBackground.Width / 2,
        height / 2 - Properties.Resources.RecordingBackground.Height / 2);
      graphics.DrawImage(Properties.Resources.RecordingBackground, center);

      return image;
    }

    ///////////////////////////////////////////////////////////////////////////////
    // Not all methods are used, because of performance problems.                //
    // Now the fastest method is used which is CopyToBpp                          //
    ///////////////////////////////////////////////////////////////////////////////
    #region ConvertToGray

    /// <summary>
    /// Returns a grayscaled <see cref="Bitmap"/> of given color image.
    /// </summary>
    /// <param name="original">A colored <see cref="Image"/> to convert to grayscale.</param>
    /// <returns>A <see cref="Bitmap"/> with a grayscale image of the given image.</returns>
    public static Bitmap GrayBackground(Image original)
    {
      Bitmap cloneCore = (Bitmap)original.Clone();
      Bitmap returnBmp = CopyToBpp(cloneCore, 8);
      return returnBmp;
    }

    /// <summary>
    /// Set palette of the image to grayscale.
    /// </summary>
    /// <param name="image">Image to convert.</param>
    /// <remarks>The method initializes palette of
    /// <see cref="System.Drawing.Imaging.PixelFormat">Format8bppIndexed</see>
    /// image with 256 gradients of gray color.</remarks>
    public static void SetGrayscalePalette(Bitmap image)
    {
      // check pixel format
      if (image.PixelFormat != PixelFormat.Format8bppIndexed)
      {
        throw new ArgumentException("Wrong pixel format, should be Format8bppIndexed");
      }

      // get palette
      ColorPalette cp = image.Palette;

      // init palette
      for (int i = 0; i < 256; i++)
      {
        cp.Entries[i] = Color.FromArgb(i, i, i);
      }

      // set palette back
      image.Palette = cp;
    }

    /// <summary>
    /// Converts the given image to grayscale by using a <see cref="ColorMatrix"/>.
    /// </summary>
    /// <param name="currentImage">image to convert to grayscale</param>
    /// <param name="brightness">A <see cref="Single"/> with the 
    /// absolute brightness value. Can be from 0f up to +1.0f.</param>
    /// <returns><strong>True</strong> if succeeded, otherwise <strong>false</strong>.</returns>
    /// <exception cref="ArgumentException">Thrown, when brightness value is less than zero.</exception>
    /// <exception cref="ArgumentNullException">Thrown, when image is null.</exception>
    public static bool ConvertToGrayScale(Image currentImage, float brightness)
    {
      if (brightness < 0)
      {
        string message = "Brightness values should be greater zero, otherwise the Bitmap will be black";
        throw new ArgumentException(message);
      }

      if (currentImage == null)
      {
        throw new ArgumentNullException("Image to convert to grayscale is null");
      }

      // Gilles Khouzams colour corrected grayscale shear
      float[][] array = new float[][]
        {
          new float[] { 0.299f * brightness, 0.299f * brightness, 0.299f * brightness, 0, 0 }, 
          new float[] { 0.587f * brightness, 0.587f * brightness, 0.587f * brightness, 0, 0 }, 
          new float[] { 0.114f * brightness, 0.114f * brightness, 0.114f * brightness, 0, 0 }, 
          new float[] { 0, 0, 0, 1, 0 }, 
          new float[] { 0, 0, 0, 0, 1 }
        };
      ColorMatrix colorMatrix = new ColorMatrix(array);
      /*
       // Jacob Grass 
    ColorMatrix cm = new ColorMatrix(new float[][]{   new float[]{0.5f,0.5f,0.5f,0,0},
                                 new float[]{0.5f,0.5f,0.5f,0,0},
                                 new float[]{0.5f,0.5f,0.5f,0,0},
                                 new float[]{0,0,0,1,0,0},
                                 new float[]{0,0,0,0,1,0},
                                 new float[]{0,0,0,0,0,1}});
     */

      return DrawAdjustedImage(currentImage, colorMatrix);
    }

    /// <summary>
    /// Adjust given images brightness by given value with 
    /// a color matrix.
    /// </summary>
    /// <param name="currentImage">image to adjust</param>
    /// <param name="brightness">A <see cref="Single"/> with the 
    /// relative brightness value. Can be from -1.0f um to +1.0f.</param>
    /// <returns><strong>True</strong> if succeeded, otherwise <strong>false</strong>.</returns>
    public static bool AdjustBrightness(Image currentImage, float brightness)
    {
      float[][] array = new float[][]
        {
          new float[] { 1f, brightness, brightness, 0, 0 }, 
          new float[] { brightness, 1f, brightness, 0, 0 }, 
          new float[] { brightness, brightness, 1f, 0, 0 }, 
          new float[] { 0, 0, 0, 1f, 0 }, 
          new float[] { 0, 0, 0, 0, 1f }
        };

      ColorMatrix colorMatrix = new ColorMatrix(array);

      return DrawAdjustedImage(currentImage, colorMatrix);
    }

    /// <summary>
    /// Create and initialize grayscale image. 
    /// </summary>
    /// <param name="width">Image width</param>
    /// <param name="height">Image height</param>
    /// <returns>Returns the created grayscale image</returns>
    /// <remarks>The methods create new grayscale image and initializes its palette.
    /// Grayscale image is represented as <see cref="System.Drawing.Imaging.PixelFormat"/>
    /// Format8bppIndexed image with palette initialized
    /// to 256 gradients of gray color</remarks>
    public static Bitmap CreateGrayscaleImage(int width, int height)
    {
      // create new image
      Bitmap image = new Bitmap(width, height, PixelFormat.Format8bppIndexed);

      // set palette to grayscale
      SetGrayscalePalette(image);

      // return new image
      return image;
    }

    /// <summary>
    /// Copies a bitmap into a 1bpp/8bpp bitmap of the same dimensions, very fast.
    /// </summary>
    /// <param name="bitmap">A <see cref="Bitmap"/> with the output bitmap.</param>
    /// <param name="bitPerPixel">1 or 8, target bits per pixel.</param>
    /// <returns>A <see cref="Bitmap"/> with a converted copy of the bitmap.</returns>
    public static Bitmap CopyToBpp(Bitmap bitmap, int bitPerPixel)
    {
      if (bitPerPixel != 1 && bitPerPixel != 8)
      {
        throw new System.ArgumentException("1 or 8", "bpp");
      }

      // Plan: built into Windows GDI is the ability to convert
      // bitmaps from one format to another. Most of the time, this
      // job is actually done by the graphics hardware accelerator card
      // and so is extremely fast. The rest of the time, the job is done by
      // very fast native code.
      // We will call into this GDI functionality from C#. Our plan:
      // (1) Convert our Bitmap into a GDI hbitmap (ie. copy unmanaged->managed)
      // (2) Create a GDI monochrome hbitmap
      // (3) Use GDI "BitBlt" function to copy from hbitmap into monochrome (as above)
      // (4) Convert the monochrone hbitmap into a Bitmap (ie. copy unmanaged->managed)
      int w = bitmap.Width, h = bitmap.Height;
      IntPtr hbm = bitmap.GetHbitmap(); // this is step (1)

      // Step (2): create the monochrome bitmap.
      // "BITMAPINFO" is an interop-struct which we define below.
      // In GDI terms, it's a BITMAPHEADERINFO followed by an array of two RGBQUADs
      Gdi32.BITMAPINFO bmi = new Gdi32.BITMAPINFO();
      bmi.BiSize = 40;  // the size of the BITMAPHEADERINFO struct
      bmi.BiWidth = w;
      bmi.BiHeight = h;
      bmi.BiPlanes = 1; // "planes" are confusing. We always use just 1. Read MSDN for more info.
      bmi.BiBitCount = (short)bitPerPixel; // ie. 1bpp or 8bpp
      bmi.BiCompression = BIRGB; // ie. the pixels in our RGBQUAD table are stored as RGBs, not palette indexes
      bmi.BiSizeImage = (uint)(((w + 7) & 0xFFFFFFF8) * h / 8);
      bmi.BiXPelsPerMeter = 1000000; // not really important
      bmi.BiYPelsPerMeter = 1000000; // not really important

      // Now for the colour table.
      uint ncols = (uint)1 << bitPerPixel; // 2 colours for 1bpp; 256 colours for 8bpp
      bmi.BiClrUsed = ncols;
      bmi.BiClrImportant = ncols;
      bmi.Cols = new uint[256]; // The structure always has fixed size 256, even if we end up using fewer colours
      if (bitPerPixel == 1)
      {
        bmi.Cols[0] = MAKERGB(0, 0, 0);
        bmi.Cols[1] = MAKERGB(255, 255, 255);
      }
      else
      {
        for (int i = 0; i < 180; i++)
        {
          bmi.Cols[i] = MAKERGB(i, i, i);
        }
      }

      // For 8bpp we've created an palette with just grayscale colours.
      // You can set up any palette you want here. Here are some possibilities:
      // greyscale: for (int i=0; i<256; i++) bmi.cols[i]=MAKERGB(i,i,i);
      // rainbow: bmi.biClrUsed=216; bmi.biClrImportant=216; int[] colv=new int[6]{0,51,102,153,204,255};
      //          for (int i=0; i<216; i++) bmi.cols[i]=MAKERGB(colv[i/36],colv[(i/6)%6],colv[i%6]);
      // optimal: a difficult topic: http://en.wikipedia.org/wiki/Color_quantization
      // 
      // Now create the indexed bitmap "hbm0"
      IntPtr bits0; // not used for our purposes. It returns a pointer to the raw bits that make up the bitmap.
      IntPtr hbm0 = Gdi32.CreateDIBSection(
        IntPtr.Zero,
        ref bmi,
        DIBRGBCOLORS,
        out bits0,
        IntPtr.Zero,
        0);

      // Step (3): use GDI's BitBlt function to copy from output hbitmap into monocrhome bitmap
      // GDI programming is kind of confusing... nb. The GDI equivalent of "Graphics" is called a "DC".
      IntPtr sdc = User32.GetDC(IntPtr.Zero);       // First we obtain the DC for the screen

      // Next, create a DC for the output hbitmap
      IntPtr hdc = Gdi32.CreateCompatibleDC(sdc);
      Gdi32.SelectObject(hdc, hbm);

      // and create a DC for the monochrome hbitmap
      IntPtr hdc0 = Gdi32.CreateCompatibleDC(sdc);
      Gdi32.SelectObject(hdc0, hbm0);

      // Now we can do the BitBlt:
      Gdi32.BitBlt(hdc0, 0, 0, w, h, hdc, 0, 0, Gdi32.TernaryRasterOperations.SRCCOPY);

      // Step (4): convert this monochrome hbitmap back into a Bitmap:
      Bitmap b0 = Bitmap.FromHbitmap(hbm0);

      // Finally some cleanup.
      Gdi32.DeleteDC(hdc);
      Gdi32.DeleteDC(hdc0);
      User32.ReleaseDC(IntPtr.Zero, sdc);
      Gdi32.DeleteObject(hbm);
      Gdi32.DeleteObject(hbm0);

      return b0;
    }

    #endregion //ConvertToGray

    /// <summary>
    /// Saves given image into a file. Filename is requested via <see cref="SaveFileDialog"/>.
    /// </summary>
    /// <param name="image">Image to save to disk</param>
    /// <returns><strong>True</strong> if succeeded, otherwise <strong>false</strong>.</returns>
    public static bool ExportImageToFile(Image image)
    {
      SaveFileDialog dlg = new SaveFileDialog();
      dlg.Title = "Please enter filename for image...";
      dlg.InitialDirectory = Environment.SpecialFolder.MyDocuments.ToString();
      dlg.Filter = "JPEG Format - jpg|*.jpg|Bitmap Format - bmp|*.bmp|Graphics Interchange Format - gif|*.gif|Portable Networks Graphic - png|*.png|Tag Image File Format - tif|*.tif|Windows MetaFile Format - wmf|*.wmf";
      dlg.FileName = "*.jpg";
      dlg.AddExtension = true;
      if (dlg.ShowDialog() == DialogResult.OK)
      {
        ImageFormat format;
        switch (dlg.FilterIndex)
        {
          case 1:
            format = ImageFormat.Jpeg;
            break;
          case 2:
            format = ImageFormat.Bmp;
            break;
          case 3:
            format = ImageFormat.Gif;
            break;
          case 4:
            format = ImageFormat.Png;
            break;
          case 5:
            format = ImageFormat.Tiff;
            break;
          case 6:
            format = ImageFormat.Wmf;
            break;
          default:
            format = ImageFormat.Jpeg;
            break;
        }

        try
        {
          image.Save(dlg.FileName, format);
        }
        catch (Exception ex)
        {
          VGExceptionMethods.HandleException(ex);

          return false;
        }
      }

      return true;
    }

    #region Scale

    /// <summary>
    /// Rescale the given image.
    /// </summary>
    /// <param name="sourceData">Source image data</param>
    /// <param name="destinationData">Destination image data</param>
    public static unsafe void RescaleImage(BitmapData sourceData, BitmapData destinationData)
    {
      // get source image size
      int width = sourceData.Width;
      int height = sourceData.Height;

      // get destination image size
      int newWidth = destinationData.Width;
      int newHeight = destinationData.Height;

      int pixelSize = (sourceData.PixelFormat == PixelFormat.Format8bppIndexed) ? 1 : 3;
      int srcStride = sourceData.Stride;
      int dstOffset = destinationData.Stride - pixelSize * newWidth;
      double factorX = (double)width / newWidth;
      double factorY = (double)height / newHeight;

      // do the job
      byte* src = (byte*)sourceData.Scan0.ToPointer();
      byte* dst = (byte*)destinationData.Scan0.ToPointer();

      // coordinates of source points and cooefficiens
      double ox, oy, dx, dy, k1, k2;
      int ox1, oy1, ox2, oy2;

      // destination pixel values
      double r, g, b;

      // width and height decreased by 1
      int ymax = height - 1;
      int xmax = width - 1;

      // temporary pointer
      byte* p;

      // check pixel format
      if (destinationData.PixelFormat == PixelFormat.Format8bppIndexed)
      {
        // grayscale
        for (int y = 0; y < newHeight; y++)
        {
          // Y coordinates
          oy = (double)y * factorY - 0.5;
          oy1 = (int)oy;
          dy = oy - (double)oy1;

          for (int x = 0; x < newWidth; x++, dst++)
          {
            // X coordinates
            ox = (double)x * factorX - 0.5f;
            ox1 = (int)ox;
            dx = ox - (double)ox1;

            // initial pixel value
            g = 0;

            for (int n = -1; n < 3; n++)
            {
              // get Y cooefficient
              k1 = BiCubicKernel(dy - (double)n);

              oy2 = oy1 + n;
              if (oy2 < 0)
              {
                oy2 = 0;
              }

              if (oy2 > ymax)
              {
                oy2 = ymax;
              }

              for (int m = -1; m < 3; m++)
              {
                // get X cooefficient
                k2 = k1 * BiCubicKernel((double)m - dx);

                ox2 = ox1 + m;
                if (ox2 < 0)
                {
                  ox2 = 0;
                }

                if (ox2 > xmax)
                {
                  ox2 = xmax;
                }

                g += k2 * src[oy2 * srcStride + ox2];
              }
            }

            *dst = (byte)g;
          }

          dst += dstOffset;
        }
      }
      else
      {
        // RGB
        for (int y = 0; y < newHeight; y++)
        {
          // Y coordinates
          oy = (double)y * factorY - 0.5f;
          oy1 = (int)oy;
          dy = oy - (double)oy1;

          for (int x = 0; x < newWidth; x++, dst += 3)
          {
            // X coordinates
            ox = (double)x * factorX - 0.5f;
            ox1 = (int)ox;
            dx = ox - (double)ox1;

            // initial pixel value
            r = g = b = 0;

            for (int n = -1; n < 3; n++)
            {
              // get Y cooefficient
              k1 = BiCubicKernel(dy - (double)n);

              oy2 = oy1 + n;
              if (oy2 < 0)
              {
                oy2 = 0;
              }

              if (oy2 > ymax)
              {
                oy2 = ymax;
              }

              for (int m = -1; m < 3; m++)
              {
                // get X cooefficient
                k2 = k1 * BiCubicKernel((double)m - dx);

                ox2 = ox1 + m;
                if (ox2 < 0)
                {
                  ox2 = 0;
                }

                if (ox2 > xmax)
                {
                  ox2 = xmax;
                }

                // get pixel of output image
                p = src + oy2 * srcStride + ox2 * 3;

                r += k2 * p[RGBR];
                g += k2 * p[RGBG];
                b += k2 * p[RGBB];
              }
            }

            dst[RGBR] = (byte)r;
            dst[RGBG] = (byte)g;
            dst[RGBB] = (byte)b;
          }

          dst += dstOffset;
        }
      }
    }

    /// <summary>
    /// This static method returns a bitmap that is a scaled version of
    /// the given bitmap to the newly given size.
    /// </summary>
    /// <param name="newSize">A <see cref="Size"/> for the scaled bitmap.</param>
    /// <param name="srcBitmap">The source <see cref="Bitmap"/> to scale.</param>
    /// <param name="inHighQuality"><strong>True</strong>, if rescaling should
    /// be performed in high quality (lasts longer).</param>
    /// <returns>A <see cref="Bitmap"/> with the rescaled image.</returns>
    public static Bitmap RescaleImage(Size newSize, Bitmap srcBitmap, bool inHighQuality)
    {
      // create heatmap sized image (rescaled source to heatmap size)
      Bitmap dstImage = new Bitmap(newSize.Width, newSize.Height, srcBitmap.PixelFormat);

      // Create graphics for rescaling
      using (Graphics grfx = Graphics.FromImage(dstImage))
      {
        grfx.InterpolationMode = inHighQuality ? InterpolationMode.HighQualityBicubic : InterpolationMode.Default;
        grfx.PixelOffsetMode = inHighQuality ? PixelOffsetMode.HighQuality : PixelOffsetMode.Default;

        // Draw rescaled bw source to heat map sized destination
        grfx.DrawImage(srcBitmap, 0, 0, newSize.Width, newSize.Height);
      }

      return dstImage;
    }

    #endregion //Scale

    /// <summary>
    /// This method returns a bitmap copy of the image in the given file.
    /// It is used to avoid the blocking of the png files when shown in
    /// the listview.
    /// </summary>
    /// <param name="filename">A <see cref="string"/> with the filename with path.</param>
    /// <returns>An <see cref="Image"/> with a <see cref="Bitmap"/> that 
    /// is the copy of the given image file.</returns>
    public static Image GetImageOfFile(string filename)
    {
      Image firstImage = null;
      using (FileStream fs = File.OpenRead(filename))
      {
        Image original = Image.FromStream(fs);
        firstImage = new Bitmap(original);
      }

      return firstImage;
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
    /// Applies a color Matrix to a given Image.
    /// E.g. transforms image to grayscale.
    /// </summary>
    /// <param name="currentImage">image to apply color matrix to</param>
    /// <param name="colorMatrix">color matrix to apply</param>
    /// <returns><strong>True</strong> if succeeded, otherwise <strong>false</strong>.</returns>
    private static bool DrawAdjustedImage(Image currentImage, ColorMatrix colorMatrix)
    {
      Bitmap bmp = new Bitmap(currentImage);
      Rectangle rc = new Rectangle(0, 0, currentImage.Width, currentImage.Height);
      Graphics graphicsObject = Graphics.FromImage(currentImage);
      ImageAttributes imgattr = new ImageAttributes();
      try
      {
        imgattr.SetColorMatrix(colorMatrix);
        graphicsObject.DrawImage(
          bmp,
          rc,
          0,
          0,
          currentImage.Width,
          currentImage.Height,
          GraphicsUnit.Pixel,
          imgattr);

        return true;
      }
      catch (Exception ex)
      {
        VGExceptionMethods.HandleException(ex);

        return false;
      }
      finally
      {
        imgattr.Dispose();
        bmp.Dispose();
        graphicsObject.Dispose();
      }
    }

    #endregion //METHODS

    ///////////////////////////////////////////////////////////////////////////////
    // Small helping Methods                                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region HELPER

    /// <summary>
    /// This method creates a <see cref="uint"/> representation of the
    /// given r,g,b values.
    /// </summary>
    /// <param name="r">A <see cref="int"/> with the red portion.</param>
    /// <param name="g">A <see cref="int"/> with the green portion.</param>
    /// <param name="b">A <see cref="int"/> with the blue portion.</param>
    /// <returns>a <see cref="uint"/> representation of the
    /// given r,g,b values.</returns>
    private static uint MAKERGB(int r, int g, int b)
    {
      return ((uint)(b & 255)) | ((uint)((r & 255) << 8)) | ((uint)((g & 255) << 16));
    }

    /// <summary>
    /// This method calculates the bicubic kernel for the given value.
    /// </summary>
    /// <param name="x">The X value</param>
    /// <returns>Bicubic coefficient</returns>
    private static double BiCubicKernel(double x)
    {
      if (x > 2.0)
      {
        return 0.0;
      }

      double a, b, c, d;
      double xm1 = x - 1.0;
      double xp1 = x + 1.0;
      double xp2 = x + 2.0;

      a = (xp2 <= 0.0) ? 0.0 : xp2 * xp2 * xp2;
      b = (xp1 <= 0.0) ? 0.0 : xp1 * xp1 * xp1;
      c = (x <= 0.0) ? 0.0 : x * x * x;
      d = (xm1 <= 0.0) ? 0.0 : xm1 * xm1 * xm1;

      return 0.16666666666666666667 * (a - (4.0 * b) + (6.0 * c) - (4.0 * d));
    }

    #endregion //HELPER
  }
}
