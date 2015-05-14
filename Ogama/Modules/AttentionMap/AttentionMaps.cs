// <copyright file="AttentionMaps.cs" company="FU Berlin">
// ******************************************************
// OGAMA - open gaze and mouse analyzer 
// Copyright (C) 2015 Dr. Adrian Voßkühler  
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
  using System.Drawing.Drawing2D;
  using System.Drawing.Imaging;

  using OgamaControls;

  using VectorGraphics.Tools;

  /// <summary>
  /// This class is designed to calculate gaussian distributions of the
  /// fixational data and transform them into transparent heat maps 
  /// with a gradient color map.
  /// </summary>
  public class AttentionMaps
  {
    ///////////////////////////////////////////////////////////////////////////////
    // Defining Constants                                                        //
    ///////////////////////////////////////////////////////////////////////////////
    #region CONSTANTS

    /// <summary>
    /// Number of colors used for gradient representation
    /// </summary>
    public const int NUMCOLORS = 2000;

    #endregion //CONSTANTS

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Variables, Enumerations, Events                                  //
    ///////////////////////////////////////////////////////////////////////////////
    #region FIELDS
    /// <summary>
    /// Predifined traffic light gradient
    /// </summary>
    private static Gradient trafficLight;

    /// <summary>
    /// Predifined rainbow gradient
    /// </summary>
    private static Gradient rainbow;

    /// <summary>
    /// Predifined mask gradient
    /// </summary>
    private static Gradient blackMask;

    /// <summary>
    /// Predifined custom gradient
    /// </summary>
    private static Gradient customGradient;

    /// <summary>
    /// Array for standard kernel calculation.
    /// </summary>
    private static float[,] defaultKernel;

    /// <summary>
    /// Standard deviation for gaussian kernel in pixel.
    /// </summary>
    private static double sigma = 40;

    /// <summary>
    /// 2D Gaussian distribution size in pixel.
    /// </summary>
    private static int kernelSize = 201;

    /// <summary>
    /// The maximum at µ=0 for the current sigma.
    /// </summary>
    private static double kernelMaximum = 0.01d;

    #endregion //FIELDS

    ///////////////////////////////////////////////////////////////////////////////
    // Construction and Initializing methods                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region CONSTRUCTION

    /// <summary>
    /// Initializes static members of the AttentionMaps class.
    /// </summary>
    static AttentionMaps()
    {
      KernelSize = 201;
      defaultKernel = Kernel2D(kernelSize, sigma * sigma);
      kernelMaximum = Function2D(0, 0, sigma * sigma);
      InitializeGradients();
    }

    #endregion //CONSTRUCTION

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Properties                                                       //
    ///////////////////////////////////////////////////////////////////////////////
    #region PROPERTIES

    /// <summary>
    /// Gets gradient with traffic light (red,yellow,green) colors.
    /// </summary>
    public static Gradient TrafficLight
    {
      get { return trafficLight; }
    }

    /// <summary>
    /// Gets gradient with rainbow colors.
    /// </summary>
    public static Gradient Rainbow
    {
      get { return rainbow; }
    }

    /// <summary>
    /// Gets gradient with masking black. (Alpha values blended)
    /// </summary>
    public static Gradient BlackMask
    {
      get { return blackMask; }
    }

    /// <summary>
    /// Gets custom gradient
    /// </summary>
    public static Gradient Custom
    {
      get { return customGradient; }
    }

    /// <summary>
    /// Gets or sets 2D Gaussian distribution size in pixel.
    /// </summary>
    public static int KernelSize
    {
      get
      {
        return kernelSize;
      }

      set
      {
        kernelSize = value;
        sigma = kernelSize / 6f;
        defaultKernel = Kernel2D(kernelSize, sigma * sigma);
        kernelMaximum = Function2D(0, 0, sigma * sigma);
      }
    }

    /// <summary>
    /// Gets a copy of the default kernel.
    /// </summary>
    public static float[,] DefaultKernel
    {
      get { return (float[,])defaultKernel.Clone(); }
    }

    #endregion //PROPERTIES

    ///////////////////////////////////////////////////////////////////////////////
    // Public methods                                                            //
    ///////////////////////////////////////////////////////////////////////////////
    #region PUBLICMETHODS

    /// <summary>
    /// Apply gaussion distribution as filter for output image.
    /// </summary>
    /// <param name="srcImg">Image to apply filter on.</param>
    /// <param name="size">kernel size</param>
    /// <param name="distributionArray">summarized distribution array</param>
    /// <returns>modified background image</returns>
    /// <remarks>Multiplies every pixel of the <c>srcImg</c> with the
    /// corresponding factor in the <c>DistributionArray</c>.
    /// It is an adaption of the two source filters of the 
    /// AForge Image Processing Library from andrew.kirillov@gmail.com.</remarks>
    internal static Bitmap Apply(Bitmap srcImg, int size, float[,] distributionArray)
    {
      // get source image size
      int width = srcImg.Width;
      int height = srcImg.Height;

      PixelFormat fmt = (srcImg.PixelFormat == PixelFormat.Format8bppIndexed) ?
        PixelFormat.Format8bppIndexed : PixelFormat.Format24bppRgb;

      // lock source bitmap data
      BitmapData srcData = srcImg.LockBits(
        new Rectangle(0, 0, width, height),
        ImageLockMode.ReadOnly,
        fmt);

      // create new image
      Bitmap dstImg = (fmt == PixelFormat.Format8bppIndexed) ?
        Images.CreateGrayscaleImage(width, height) :
        new Bitmap(width, height, fmt);

      // lock destination bitmap data
      BitmapData dstData = dstImg.LockBits(
        new Rectangle(0, 0, width, height),
        ImageLockMode.ReadWrite,
        fmt);

      int stride = srcData.Stride;
      int offset = stride - ((fmt == PixelFormat.Format8bppIndexed) ? width : width * 3);

      float k = 0;
      int radius = size >> 1;
      long r, g, b;

      // do the job
      unsafe
      {
        byte* src = (byte*)srcData.Scan0.ToPointer();
        byte* dst = (byte*)dstData.Scan0.ToPointer();
        byte* p;

        if (fmt == PixelFormat.Format8bppIndexed)
        {
          // Grayscale image

          // for each line
          for (int y = 0; y < height; y++)
          {
            // for each pixel
            for (int x = 0; x < width; x++, src++, dst++)
            {
              g = 0;
              k = distributionArray[x, y];
              p = &src[0];

              g = (long)(k * p[0]);

              *dst = (g > 255) ? (byte)255 : ((g < 0) ? (byte)0 : (byte)g);
            }

            src += offset;
            dst += offset;
          }
        }
        else
        {
          // RGB image

          // for each line
          for (int y = 0; y < height; y++)
          {
            // for each pixel
            for (int x = 0; x < width; x++, src += 3, dst += 3)
            {
              r = g = b = 0;
              k = distributionArray[x, y];
              p = &src[0];

              r = (long)(k * p[Images.RGBR]);
              g = (long)(k * p[Images.RGBG]);
              b = (long)(k * p[Images.RGBB]);

              dst[Images.RGBR] = (r > 255) ? (byte)255 : ((r < 0) ? (byte)0 : (byte)r);
              dst[Images.RGBG] = (g > 255) ? (byte)255 : ((g < 0) ? (byte)0 : (byte)g);
              dst[Images.RGBB] = (b > 255) ? (byte)255 : ((b < 0) ? (byte)0 : (byte)b);
            }

            src += offset;
            dst += offset;
          }
        }
      }

      // unlock both images
      dstImg.UnlockBits(dstData);
      srcImg.UnlockBits(srcData);

      return dstImg;
    }

    /// <summary>
    /// Adds a sized gaussian kernel to an existing distribution array of size width,heigth
    /// at position PosX,PosY
    /// </summary>
    /// <param name="distributionArray">existing gaussian distribution sum array</param>
    /// <param name="posX">X-position of kernel to add</param>
    /// <param name="posY">Y-position of kernel to add</param>
    /// <param name="width">width of distribution array</param>
    /// <param name="height">height of distribution array</param>
    /// <param name="size">size of kernel</param>
    /// <param name="kernel">float array with gaussian kernel</param>
    internal static void AddKernelToArray(
      float[,] distributionArray,
      int posX,
      int posY,
      int width,
      int height,
      int size,
      float[,] kernel)
    {
      int i, j, tx, ty, ir, jr;
      int radius = size >> 1;

      int x = posX;
      int y = posY;

      // for each kernel row
      for (i = 0; i < size; i++)
      {
        ir = i - radius;
        ty = y + ir;

        // skip row
        if (ty < 0)
        {
          continue;
        }

        // break Height
        if (ty >= height)
        {
          break;
        }

        // for each kernel column
        for (j = 0; j < size; j++)
        {
          jr = j - radius;
          tx = x + jr;

          // skip column
          if (tx < 0)
          {
            continue;
          }

          if (tx < width)
          {
            distributionArray[tx, ty] += kernel[i, j];
          }
        }
      }
    }

    /// <summary>
    /// Rescale the given float array to a maximal value of NUMCOLORS.
    /// </summary>
    /// <param name="array">array to scale</param>
    /// <param name="valueForMaxColor">An <see cref="Int32"/> with the value for the
    /// maxmimal color.</param>
    internal static void RescaleArray(float[,] array, int valueForMaxColor)
    {
      float max = 0;

      if (valueForMaxColor < 0)
      {
        // Determine Current Max Value in Array
        foreach (float value in array)
        {
          if (value > max)
          {
            max = value;
          }
        }
      }
      else
      {
        max = (float)(valueForMaxColor * kernelMaximum);
      }

      // Rescale to Maximal NUMCOLORs value
      for (int i = 0; i < array.GetLength(0); i++)
      {
        for (int j = 0; j < array.GetLength(1); j++)
        {
          if (array[i, j] > max)
          {
            array[i, j] = max;
          }
          else
          {
            array[i, j] *= (NUMCOLORS - 1) / max;
          }
        }
      }
    }

    /// <summary>
    /// Multiply given kernel with a scaling factor.
    /// </summary>
    /// <param name="factor">Factor to multiply.</param>
    /// <param name="size">The kernel size.</param>
    /// <returns>A 2D <see cref="Single"/> array with the newly resized kernel array.</returns>
    internal static float[,] MultiplyKernel(float factor, int size)
    {
      // kernel
      float[,] kernel = new float[size, size];
      kernel = (float[,])defaultKernel.Clone();

      for (int i = 0; i < size; i++)
      {
        for (int j = 0; j < size; j++)
        {
          kernel[i, j] *= factor;
        }
      }

      return kernel;
    }

    /// <summary>
    /// This static method creates an distribution array of values between 0-1
    /// from the brightness of the given b/w image.
    /// </summary>
    /// <param name="distributionArray">An 2D array of <see cref="Single"/>
    /// to be filled with the distribution values.</param>
    /// <param name="channelFilename">The filename with the b/w image.</param>
    /// <param name="stimulusSize">Contains the real size of the stimulus,
    /// which can be greater than presentation size in webpages.</param>
    internal static void CreateDistributionArrayFromBWImage(
      ref float[,] distributionArray, 
      string channelFilename, 
      Size stimulusSize)
    {
      if (!System.IO.File.Exists(channelFilename))
      {
        throw new System.IO.FileNotFoundException();
      }

      Bitmap srcImage = (Bitmap)Image.FromFile(channelFilename);

      // get image size
      int width = srcImage.Width;
      int height = srcImage.Height;

      // create new image
      Bitmap dstImage = new Bitmap(stimulusSize.Width, stimulusSize.Height, srcImage.PixelFormat);

      using (Graphics grfx = Graphics.FromImage(dstImage))
      {
        grfx.InterpolationMode = InterpolationMode.HighQualityBicubic;

        // necessary setting for proper work with image borders
        grfx.PixelOffsetMode = PixelOffsetMode.HighQuality;

        grfx.DrawImage(srcImage, 0, 0, stimulusSize.Width, stimulusSize.Height);
      }

      PaletteBitmap output = new PaletteBitmap(dstImage);

      // for each line
      for (int y = 0; y < stimulusSize.Height; y++)
      {
        // for each pixel
        for (int x = 0; x < stimulusSize.Width; x++)
        {
          distributionArray[x, y] = output.GetPixel(x, y).GetBrightness();
        }
      }
    }

    /// <summary>
    /// This static method creates a heatmap
    /// from the brightness of the given b/w image where the colors are
    /// set using the given gradient colored bitmap.
    /// </summary>
    /// <param name="heatMapBmp">A <see cref="PaletteBitmap"/> thats
    /// contents should be overwritten with new heatmap</param>
    /// <param name="gradientBmp">A <see cref="PaletteBitmap"/> which is gradient
    /// colored.</param>
    /// <param name="size">The <see cref="Size"/> of the heatmap.</param>
    /// <param name="channelFilename">The filename with the b/w image.</param>
    /// <returns>A <see cref="Bitmap"/> with the new heat map</returns>
    internal static Bitmap CreateHeatMapFromBWImage(
      PaletteBitmap heatMapBmp,
      PaletteBitmap gradientBmp,
      Size size,
      string channelFilename)
    {
      // get heat map size
      int newWidth = size.Width;
      int newHeight = size.Height;

      if (!System.IO.File.Exists(channelFilename))
      {
        throw new System.IO.FileNotFoundException();
      }

      // Rescale source image to heat map size.
      Bitmap srcImage = (Bitmap)Images.GetImageOfFile(channelFilename);
      Bitmap rescaledSrcImage = Images.RescaleImage(new Size(newWidth, newHeight), srcImage, true);

      PaletteBitmap output = new PaletteBitmap(rescaledSrcImage);

      // for each line
      for (int y = 0; y < newHeight; y++)
      {
        // for each pixel
        for (int x = 0; x < newWidth; x++)
        {
          heatMapBmp.SetPixel(
            x,
            y,
            gradientBmp.GetPixel((int)(output.GetPixel(x, y).GetBrightness() * (NUMCOLORS - 1)), 0));
        }
      }

      return heatMapBmp.Image;
    }

    /// <summary>
    /// Returns a bitmap with a gradient colored DistributionArray with the given size.
    /// </summary>
    /// <param name="heatMapBmp">Bitmap to save the heatmap to.</param>
    /// <param name="gradientBmp">Bitmap with gradient colors</param>
    /// <param name="size">new bitmap size</param>
    /// <param name="distributionArray">fixation distributions array</param>
    /// <returns>heat map bitmap</returns>
    internal static Bitmap CreateHeatMap(
      PaletteBitmap heatMapBmp,
      PaletteBitmap gradientBmp,
      Size size,
      float[,] distributionArray)
    {
      // get image size
      int width = size.Width;
      int height = size.Height;

      // for each line
      for (int y = 0; y < height; y++)
      {
        // for each pixel
        for (int x = 0; x < width; x++)
        {
          heatMapBmp.SetPixel(x, y, gradientBmp.GetPixel((int)distributionArray[x, y], 0));
        }
      }

      return (Bitmap)heatMapBmp.Image.Clone();
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
    /// Initialize gradients and the subject list.
    /// </summary>
    private static void InitializeGradients()
    {
      customGradient = new Gradient();
      ColorBlend customBlend = new ColorBlend(5);
      customBlend.Colors = new Color[4] 
      { 
        Color.FromArgb(0, 255, 255, 255), 
        Color.FromArgb(0, 255, 255, 255),
        Color.FromArgb(153, 0, 255, 0),
        Color.FromArgb(153, 255, 0, 0)
      };
      customBlend.Positions = new float[4] { 0.0f, 0.3f, 0.85f, 1.0f };
      customGradient.ColorBlend = customBlend;
      customGradient.GradientDirection = LinearGradientMode.Horizontal;

      blackMask = new Gradient(Color.FromArgb(217, 0, 0, 0), Color.FromArgb(0, 255, 255, 255));
      blackMask.GradientDirection = LinearGradientMode.Horizontal;

      trafficLight = new Gradient();
      ColorBlend trafficLightBlend = new ColorBlend(5);
      trafficLightBlend.Colors = new Color[5] 
      { 
        Color.FromArgb(0, 255, 255, 255), 
        Color.FromArgb(0, 255, 255, 255),
        Color.FromArgb(153, 0, 255, 0),
        Color.FromArgb(153, 255, 255, 0),
        Color.FromArgb(153, 255, 0, 0)
      };
      trafficLightBlend.Positions = new float[5] { 0.0f, 0.3f, 0.6f, 0.85f, 1.0f };
      trafficLight.ColorBlend = trafficLightBlend;
      trafficLight.GradientDirection = LinearGradientMode.Horizontal;

      rainbow = new Gradient();
      ColorBlend rainbowBlend = new ColorBlend(8);
      rainbowBlend.Colors = new Color[8] 
      { 
        Color.FromArgb(153, 0, 0, 0), 
        Color.FromArgb(153, 128, 0, 128),
        Color.FromArgb(153, 0, 0, 255),
        Color.FromArgb(153, 0, 255, 255),
        Color.FromArgb(153, 0, 255, 0),
        Color.FromArgb(153, 255, 255, 0),
        Color.FromArgb(153, 255, 128, 0),
        Color.FromArgb(153, 255, 0, 0)
      };
      rainbowBlend.Positions = new float[8] { 0.0f, 0.1f, 0.25f, 0.4f, 0.65f, 0.75f, 0.93f, 1.0f };
      rainbow.ColorBlend = rainbowBlend;
      rainbow.GradientDirection = LinearGradientMode.Horizontal;
    }

    /// <summary>
    /// Calculates the two dimensional gaussian function
    /// at a given point.
    /// </summary>
    /// <param name="x">x value for evaluating the function.</param>
    /// <param name="y">y value for evaluating the function.</param>
    /// <param name="sqrSigma">The squared sigma of the gaussian function.</param>
    /// <returns>A <see cref="Single"/> with the functions value.</returns>
    private static double Function2D(double x, double y, double sqrSigma)
    {
      return Math.Exp(((x * x) + (y * y)) / (-2 * sqrSigma)) / (2 * Math.PI * sqrSigma);
    }

    /// <summary>
    /// Calculates the 2-D Gaussian kernel of given size.
    /// </summary>
    /// <param name="size">The size of the kernel in pixel.</param>
    /// <param name="sqrSigma">The squared sigma of the gaussian function.</param>
    /// <returns>A 2D <see cref="Single"/> array with the function values.</returns>
    private static float[,] Kernel2D(int size, double sqrSigma)
    {
      // check for evem size and for out of range
      if (((size % 2) == 0) || (size < 3) || (size > 401))
      {
        throw new ArgumentException();
      }

      // radius
      int r = size / 2;

      // double maxFunc = Function2D(0,0,sqrSigma);//1 / (Math.Sqrt(2 * Math.PI) * sigma);
      // kernel
      float[,] kernel = new float[size, size];

      // compute kernel
      for (int y = -r, i = 0; i < size; y++, i++)
      {
        for (int x = -r, j = 0; j < size; x++, j++)
        {
          kernel[i, j] = (float)Function2D(x, y, sqrSigma);
        }
      }

      return kernel;
    }

    #endregion //METHODS

    ///////////////////////////////////////////////////////////////////////////////
    // Small helping Methods                                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region HELPER
    #endregion //HELPER
  }
}
