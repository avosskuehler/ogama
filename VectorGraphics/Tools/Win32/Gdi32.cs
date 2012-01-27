// <copyright file="Gdi32.cs" company="FU Berlin">
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

namespace VectorGraphics.Tools.Win32
{
  using System;
  using System.Runtime.InteropServices;

  /// <summary>
  /// Helper class containing Gdi32.dll API functions
  /// </summary>
  public class Gdi32
  {
    ///////////////////////////////////////////////////////////////////////////////
    // Defining Constants                                                        //
    ///////////////////////////////////////////////////////////////////////////////
    #region CONSTANTS
    #endregion //CONSTANTS

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Enumerations                                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region ENUMS

    /// <summary>
    /// Enumeration for the raster operations used in BitBlt.
    /// In C++ these are actually #define. But to use these
    /// constants with C#, a new enumeration type is defined.
    /// These codes define how the color data for the 
    /// source rectangle is to be combined with the color 
    /// data for the destination rectangle to achieve the final color.
    /// Copies the source rectangle directly to the destination rectangle.
    /// </summary>
    public enum TernaryRasterOperations
    {
      /// <summary>
      /// Copies the source rectangle directly to the destination rectangle. 
      /// </summary>
      SRCCOPY = 0x00CC0020, // dest = source

      /// <summary>
      /// Combines the colors of the source and destination rectangles by using the Boolean OR operator. 
      /// </summary>
      SRCPAINT = 0x00EE0086, // dest = source OR dest

      /// <summary>
      /// Combines the colors of the source and destination rectangles by using the Boolean AND operator. 
      /// </summary>
      SRCAND = 0x008800C6, // dest = source AND dest

      /// <summary>
      /// Combines the colors of the source and destination rectangles by using the Boolean XOR operator. 
      /// </summary>
      SRCINVERT = 0x00660046, // dest = source XOR dest

      /// <summary>
      /// Combines the inverted colors of the destination rectangle with the colors of the source rectangle by using the Boolean AND operator. 
      /// </summary>
      SRCERASE = 0x00440328, // dest = source AND (NOT dest)

      /// <summary>
      /// Copies the inverted source rectangle to the destination. 
      /// </summary>
      NOTSRCCOPY = 0x00330008, // dest = (NOT source)

      /// <summary>
      /// Combines the colors of the source and destination rectangles by using the Boolean OR operator and then inverts the resultant color. 
      /// </summary>
      NOTSRCERASE = 0x001100A6, // dest = (NOT src) AND (NOT dest)

      /// <summary>
      /// Merges the colors of the source rectangle with the brush currently selected in hdcDest, by using the Boolean AND operator. 
      /// </summary>
      MERGECOPY = 0x00C000CA, // dest = (source AND pattern)

      /// <summary>
      /// Merges the colors of the inverted source rectangle with the colors of the destination rectangle by using the Boolean OR operator. 
      /// </summary>
      MERGEPAINT = 0x00BB0226, // dest = (NOT source) OR dest

      /// <summary>
      /// Copies the brush currently selected in hdcDest, into the destination bitmap. 
      /// </summary>
      PATCOPY = 0x00F00021, // dest = pattern

      /// <summary>
      /// Combines the colors of the brush currently selected in hdcDest, with the colors of the inverted source rectangle by using the Boolean OR operator. The result of this operation is combined with the colors of the destination rectangle by using the Boolean OR operator. 
      /// </summary>
      PATPAINT = 0x00FB0A09, // dest = DPSnoo

      /// <summary>
      /// Combines the colors of the brush currently selected in hdcDest, with the colors of the destination rectangle by using the Boolean XOR operator. 
      /// </summary>
      PATINVERT = 0x005A0049, // dest = pattern XOR dest

      /// <summary>
      /// Inverts the destination rectangle. 
      /// </summary>
      DSTINVERT = 0x00550009, // dest = (NOT dest)

      /// <summary>
      /// Fills the destination rectangle using the color associated with index 0 in the physical palette. (This color is black for the default physical palette.) 
      /// </summary>
      BLACKNESS = 0x00000042, // dest = BLACK

      /// <summary>
      /// Fills the destination rectangle using the color associated with index 1 in the physical palette. (This color is white for the default physical palette.) 
      /// </summary>
      WHITENESS = 0x00FF0062 // dest = WHITE
    }

    /// <summary>
    /// Device-specific information.
    /// </summary>
    public enum DeviceCap : int
    {
      /// <summary>
      /// Device driver version
      /// </summary>
      DRIVERVERSION = 0,

      /// <summary>
      /// Device classification
      /// </summary>
      TECHNOLOGY = 2,

      /// <summary>
      /// Horizontal size in millimeters
      /// </summary>
      HORZSIZE = 4,

      /// <summary>
      /// Vertical size in millimeters
      /// </summary>
      VERTSIZE = 6,

      /// <summary>
      /// Horizontal width in pixels
      /// </summary>
      HORZRES = 8,

      /// <summary>
      /// Vertical height in pixels
      /// </summary>
      VERTRES = 10,

      /// <summary>
      /// Number of bits per pixel
      /// </summary>
      BITSPIXEL = 12,

      /// <summary>
      /// Number of planes
      /// </summary>
      PLANES = 14,

      /// <summary>
      /// Number of brushes the device has
      /// </summary>
      NUMBRUSHES = 16,

      /// <summary>
      /// Number of pens the device has
      /// </summary>
      NUMPENS = 18,

      /// <summary>
      /// Number of markers the device has
      /// </summary>
      NUMMARKERS = 20,

      /// <summary>
      /// Number of fonts the device has
      /// </summary>
      NUMFONTS = 22,

      /// <summary>
      /// Number of colors the device supports
      /// </summary>
      NUMCOLORS = 24,

      /// <summary>
      /// Size required for device descriptor
      /// </summary>
      PDEVICESIZE = 26,

      /// <summary>
      /// Curve capabilities
      /// </summary>
      CURVECAPS = 28,

      /// <summary>
      /// Line capabilities
      /// </summary>
      LINECAPS = 30,

      /// <summary>
      /// Polygonal capabilities
      /// </summary>
      POLYGONALCAPS = 32,

      /// <summary>
      /// Text capabilities
      /// </summary>
      TEXTCAPS = 34,

      /// <summary>
      /// Clipping capabilities
      /// </summary>
      CLIPCAPS = 36,

      /// <summary>
      /// Bitblt capabilities
      /// </summary>
      RASTERCAPS = 38,

      /// <summary>
      /// Length of the X leg
      /// </summary>
      ASPECTX = 40,

      /// <summary>
      /// Length of the Y leg
      /// </summary>
      ASPECTY = 42,

      /// <summary>
      /// Length of the hypotenuse
      /// </summary>
      ASPECTXY = 44,

      /// <summary>
      /// Shading and Blending caps
      /// </summary>
      SHADEBLENDCAPS = 45,

      /// <summary>
      /// Logical pixels inch in X
      /// </summary>
      LOGPIXELSX = 88,

      /// <summary>
      /// Logical pixels inch in Y
      /// </summary>
      LOGPIXELSY = 90,

      /// <summary>
      /// Number of entries in physical palette
      /// </summary>
      SIZEPALETTE = 104,

      /// <summary>
      /// Number of reserved entries in palette
      /// </summary>
      NUMRESERVED = 106,

      /// <summary>
      /// Actual color resolution
      /// </summary>
      COLORRES = 108,

      // Printing related DeviceCaps. These replace the appropriate Escapes

      /// <summary>
      /// Physical Width in device units
      /// </summary>
      PHYSICALWIDTH = 110,

      /// <summary>
      /// Physical Height in device units
      /// </summary>
      PHYSICALHEIGHT = 111,

      /// <summary>
      /// Physical Printable Area x margin
      /// </summary>
      PHYSICALOFFSETX = 112,

      /// <summary>
      /// Physical Printable Area y margin
      /// </summary>
      PHYSICALOFFSETY = 113,

      /// <summary>
      /// Scaling factor x
      /// </summary>
      SCALINGFACTORX = 114,

      /// <summary>
      /// Scaling factor y
      /// </summary>
      SCALINGFACTORY = 115,

      /// <summary>
      /// Current vertical refresh rate of the display device (for displays only) in Hz
      /// </summary>
      VREFRESH = 116,

      /// <summary>
      /// Horizontal width of entire desktop in pixels
      /// </summary>
      DESKTOPVERTRES = 117,

      /// <summary>
      /// Vertical height of entire desktop in pixels
      /// </summary>
      DESKTOPHORZRES = 118,

      /// <summary>
      /// Preferred blt alignment
      /// </summary>
      BLTALIGNMENT = 119
    }

    #endregion ENUMS

    ///////////////////////////////////////////////////////////////////////////////
    // Public methods                                                            //
    ///////////////////////////////////////////////////////////////////////////////
    #region PUBLICMETHODS

    /// <summary>
    /// The DeleteObject function deletes a logical pen, brush, 
    /// font, bitmap, region, or palette, freeing all system 
    /// resources associated with the object. After the object 
    /// is deleted, the specified handle is no longer valid. 
    /// </summary>
    /// <param name="objectHandle">[in] Handle to a logical pen, brush, font, bitmap, region, or palette.</param>
    /// <returns>If the function succeeds, the return value is nonzero.
    /// If the specified handle is not valid or is currently selected into 
    /// a DC, the return value is zero.</returns>
    [DllImport("gdi32.dll")]
    public static extern bool DeleteObject(IntPtr objectHandle);

    /// <summary>
    /// The CreateDIBSection function creates a DIB that 
    /// applications can write to directly. The function gives 
    /// you a pointer to the location of the bitmap bit values. 
    /// You can supply a handle to a file-mapping object that 
    /// the function will use to create the bitmap, or you can 
    /// let the system allocate the memory for the bitmap.
    /// </summary>
    /// <param name="hdc">[in] Handle to a device context. If 
    /// the value of iUsage is DIB_PAL_COLORS, the function uses 
    /// this device context's logical palette to initialize the DIB colors.</param>
    /// <param name="bmi">[in] Pointer to a <see cref="BITMAPINFO"/> structure that specifies 
    /// various attributes of the DIB, including the bitmap dimensions and colors.</param>
    /// <param name="usage">[in] Specifies the type of data contained in the
    /// bmiColors array member of the BITMAPINFO structure pointed to 
    /// by pbmi (either logical palette indexes or literal RGB values). </param>
    /// <param name="bits">[out] Pointer to a variable that receives 
    /// a pointer to the location of the DIB bit values.</param>
    /// <param name="sectionHandle">[in] Handle to a file-mapping object that the 
    /// function will use to create the DIB. This parameter can be NULL.</param>
    /// <param name="offset">[in] Specifies the offset from the beginning
    /// of the file-mapping object referenced by hSection where storage 
    /// for the bitmap bit values is to begin. This value is ignored if 
    /// hSection is NULL. The bitmap bit values are aligned on doubleword
    /// boundaries, so dwOffset must be a multiple of the size of a DWORD.</param>
    /// <returns>If the function succeeds, the return value is a handle 
    /// to the newly created DIB, and *ppvBits points to the bitmap bit values.
    /// If the function fails, the return value is NULL, and *ppvBits is NULL.</returns>
    [DllImport("gdi32.dll")]
    public static extern IntPtr CreateDIBSection(
      IntPtr hdc,
      ref BITMAPINFO bmi,
      uint usage,
      out IntPtr bits,
      IntPtr sectionHandle,
      int offset);

    /// <summary>
    /// The CreateCompatibleDC function creates a memory 
    /// device context (DC) compatible with the specified device.
    /// </summary>
    /// <param name="hdc">[in] Handle to an existing DC. 
    /// If this handle is NULL, the function creates a memory DC 
    /// compatible with the application's current screen.</param>
    /// <returns>If the function succeeds, the return value is 
    /// the handle to a memory DC.
    /// If the function fails, the return value is NULL.</returns>
    [DllImport("gdi32.dll")]
    public static extern IntPtr CreateCompatibleDC(IntPtr hdc);

    /// <summary>
    /// The DeleteDC function deletes the specified device context (DC).
    /// </summary>
    /// <param name="hdc">[in] Handle to the device context. </param>
    /// <returns>If the function succeeds, the return value is nonzero.
    /// If the function fails, the return value is zero.</returns>
    [DllImport("gdi32.dll")]
    public static extern int DeleteDC(IntPtr hdc);

    /// <summary>
    /// The SelectObject function selects an object into the 
    /// specified device context (DC). The new object replaces 
    /// the previous object of the same type. 
    /// </summary>
    /// <param name="hdc">[in] Handle to the DC.</param>
    /// <param name="hgdiobj">[in] Handle to the object to be selected.</param>
    /// <returns>If the selected object is not a region and the function succeeds, 
    /// the return value is a handle to the object being replaced. 
    /// If the selected object is a region and the function succeeds,
    /// the return value is one of the following values.</returns>
    [DllImport("gdi32.dll")]
    public static extern IntPtr SelectObject(IntPtr hdc, IntPtr hgdiobj);

    /// <summary>
    /// The BitBlt function performs a bit-block transfer of the color data 
    /// corresponding to a rectangle of pixels from the specified 
    /// source device context into a destination device context. 
    /// </summary>
    /// <param name="objectHandle">[in] Handle to the destination device context.</param>
    /// <param name="destX">[in] Specifies the x-coordinate, in logical units,
    /// of the upper-left corner of the destination rectangle. </param>
    /// <param name="destY">[in] Specifies the y-coordinate, in logical units, 
    /// of the upper-left corner of the destination rectangle. </param>
    /// <param name="width">[in] Specifies the width, in logical units, of the 
    /// source and destination rectangles.</param>
    /// <param name="height">[in] Specifies the height, in logical units, of the 
    /// source and destination rectangles.</param>
    /// <param name="objectSourceHandle">[in] Handle to the source device context.</param>
    /// <param name="srcX">[in] Specifies the x-coordinate, in logical units, 
    /// of the upper-left corner of the source rectangle.</param>
    /// <param name="srcY">[in] Specifies the y-coordinate, in logical units, 
    /// of the upper-left corner of the source rectangle.</param>
    /// <param name="rop">[in] Specifies a raster-operation code. These codes define how 
    /// the color data for the source rectangle is to be combined with the 
    /// color data for the destination rectangle to achieve the final color. </param>
    /// <returns>If the function succeeds, the return value is nonzero.
    /// If the function fails, the return value is zero. </returns>
    [DllImport("gdi32.dll")]
    public static extern bool BitBlt(
      IntPtr objectHandle,
      int destX,
      int destY,
      int width,
      int height,
      IntPtr objectSourceHandle,
      int srcX,
      int srcY,
      TernaryRasterOperations rop);

    /// <summary>
    /// The StretchBlt function copies a bitmap from a source rectangle 
    /// into a destination rectangle, stretching or compressing the bitmap 
    /// to fit the dimensions of the destination rectangle, 
    /// if necessary. The system stretches or compresses the bitmap 
    /// according to the stretching mode currently set in the destination device context. 
    /// </summary>
    /// <param name="destHDC">[in] Handle to the destination device context.</param>
    /// <param name="originDestX">[in] Specifies the x-coordinate, in logical units, of the upper-left corner of the destination rectangle.</param>
    /// <param name="originDestY">[in] Specifies the y-coordinate, in logical units, of the upper-left corner of the destination rectangle.</param>
    /// <param name="widthDest">[in] Specifies the width, in logical units, of the destination rectangle.</param>
    /// <param name="heightDest">[in] Specifies the height, in logical units, of the destination rectangle.</param>
    /// <param name="srcHDC">[in] Handle to the source device context.</param>
    /// <param name="originSrcX">[in] Specifies the x-coordinate, in logical units, of the upper-left corner of the source rectangle.</param>
    /// <param name="originSrcY">[in] Specifies the y-coordinate, in logical units, of the upper-left corner of the source rectangle.</param>
    /// <param name="widthSrc">[in] Specifies the width, in logical units, of the source rectangle.</param>
    /// <param name="heightSrc">[in] Specifies the height, in logical units, of the source rectangle.</param>
    /// <param name="rop">[in] Specifies the raster operation to be performed. Raster operation codes 
    /// define how the system combines colors in output operations that involve 
    /// a brush, a source bitmap, and a destination bitmap.</param>
    /// <returns>If the function succeeds, the return value is nonzero.
    /// If the function fails, the return value is zero.</returns>
    [DllImport("gdi32.dll")]
    public static extern bool StretchBlt(
      IntPtr destHDC,
      int originDestX,
      int originDestY,
      int widthDest,
      int heightDest,
      IntPtr srcHDC,
      int originSrcX,
      int originSrcY,
      int widthSrc,
      int heightSrc,
      TernaryRasterOperations rop);

    /// <summary>
    /// The CreateCompatibleBitmap function creates a bitmap compatible 
    /// with the device that is associated with the specified device context. 
    /// </summary>
    /// <param name="hDC">[in] Handle to a device context. </param>
    /// <param name="width">[in] Specifies the bitmap width, in pixels. </param>
    /// <param name="height">[in] Specifies the bitmap height, in pixels. </param>
    /// <returns>If the function succeeds, the return value is a handle 
    /// to the compatible bitmap (DDB).
    /// If the function fails, the return value is NULL.</returns>
    [DllImport("gdi32.dll")]
    public static extern IntPtr CreateCompatibleBitmap(
      IntPtr hDC,
      int width,
      int height);

    /// <summary>
    /// The GetDeviceCaps function retrieves device-specific 
    /// information for the specified device. 
    /// </summary>
    /// <param name="hdc">[in] Handle to the DC.</param>
    /// <param name="index">[in] Specifies the item to return. 
    /// This parameter can be one of the following values. </param>
    /// <returns>The return value specifies the value of the desired item.</returns>
    [DllImport("gdi32.dll")]
    public static extern int GetDeviceCaps(IntPtr hdc, DeviceCap index);

    #endregion //PUBLICMETHODS

    ///////////////////////////////////////////////////////////////////////////////
    // Structs                                                                   //
    ///////////////////////////////////////////////////////////////////////////////
    #region STRUCTS

    /// <summary>
    /// The BITMAPINFO structure defines the dimensions and color 
    /// information for a Windows device-independent bitmap (DIB). 
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct BITMAPINFO
    {
      /// <summary>
      /// Specifies the number of bytes required by the structure.
      /// </summary>
      public uint BiSize;

      /// <summary>
      /// Specifies the width of the bitmap, in pixels.
      /// </summary>
      public int BiWidth;

      /// <summary>
      /// Specifies the height of the bitmap, in pixels. 
      /// If biHeight is positive, the bitmap is a bottom-up 
      /// DIB and its origin is the lower-left corner. 
      /// If biHeight is negative, the bitmap is a top-down 
      /// DIB and its origin is the upper-left corner. 
      /// If biHeight is negative, indicating a top-down DIB,
      /// biCompression must be either BI_RGB or BI_BITFIELDS. 
      /// Top-down DIBs cannot be compressed.
      /// </summary>
      public int BiHeight;

      /// <summary>
      /// Specifies the number of planes for the target device. 
      /// This value must be set to 1.
      /// </summary>
      public short BiPlanes;

      /// <summary>
      /// Specifies the number of bits-per-pixel. 
      /// The biBitCount member of the 
      /// structure determines the number of bits that 
      /// define each pixel and the maximum number of colors 
      /// in the bitmap.
      /// </summary>
      public short BiBitCount;

      /// <summary>
      /// Specifies the type of compression for a compressed 
      /// bottom-up bitmap (top-down DIBs cannot be compressed). 
      /// </summary>
      public uint BiCompression;

      /// <summary>
      /// Specifies the size, in bytes, of the image. 
      /// This may be set to zero for BI_RGB bitmaps.
      /// </summary>
      public uint BiSizeImage;

      /// <summary>
      /// Specifies the horizontal resolution, in pixels-per-meter, 
      /// of the target device for the bitmap. An application 
      /// can use this value to select a bitmap from a resource 
      /// group that best matches the characteristics of the current device.
      /// </summary>
      public int BiXPelsPerMeter;

      /// <summary>
      /// Specifies the vertical resolution, 
      /// in pixels-per-meter, of the target device for the bitmap.
      /// </summary>
      public int BiYPelsPerMeter;

      /// <summary>
      /// Specifies the number of color indexes in the color 
      /// table that are actually used by the bitmap. If 
      /// this value is zero, the bitmap uses the maximum number 
      /// of colors corresponding to the value of the biBitCount
      /// member for the compression mode specified by biCompression. 
      /// If biClrUsed is nonzero and the biBitCount member is less
      /// than 16, the biClrUsed member specifies the actual number
      /// of colors the graphics engine or device driver accesses. 
      /// If biBitCount is 16 or greater, the biClrUsed member specifies 
      /// the size of the color table used to optimize performance of 
      /// the system color palettes. If biBitCount equals 16 or 32, 
      /// the optimal color palette starts immediately following the 
      /// three DWORD masks. 
      /// When the bitmap array immediately follows the <see cref="BITMAPINFO"/> 
      /// structure, it is a packed bitmap. Packed bitmaps are referenced 
      /// by a single pointer. Packed bitmaps require that the biClrUsed 
      /// member must be either zero or the actual size of the color table.
      /// </summary>
      public uint BiClrUsed;

      /// <summary>
      /// Specifies the number of color indexes that are required for 
      /// displaying the bitmap. If this value is zero, all colors are required.
      /// </summary>
      public uint BiClrImportant;

      /// <summary>
      /// Specifies an array of RGBQUAD or DWORD data types 
      /// that define the colors in the bitmap. 
      /// </summary>
      [MarshalAs(UnmanagedType.ByValArray, SizeConst = 256)]
      public uint[] Cols;
    }

    #endregion //STRUCTS
  }
}
