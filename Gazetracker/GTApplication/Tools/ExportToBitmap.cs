// <copyright file="ExportToBitmap.cs" company="ITU">
// ******************************************************
// GazeTrackingLibrary for ITU GazeTracker
// Copyright (C) 2010 Martin Tall  
// ------------------------------------------------------------------------
// This program is free software; you can redistribute it and/or modify it 
// under the terms of the GNU General Public License as published by the 
// Free Software Foundation; either version 3 of the License, 
// or (at your option) any later version.
// This program is distributed in the hope that it will be useful, 
// but WITHOUT ANY WARRANTY; without even the implied warranty of 
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU 
// General Public License for more details.
// You should have received a copy of the GNU General Public License 
// along with this program; if not, see http://www.gnu.org/licenses/.
// **************************************************************
// </copyright>
// <author>Adrian Voßkühler</author>
// <email>adrian@ogama.net</email>
// <modifiedby>Martin Tall</modifiedby>

using System;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media.Imaging;

namespace GTApplication.Tools
{
    /// <summary>
    /// This class provides methods to convert between <see cref="System.Drawing.Bitmap"/>
    /// and <see cref="BitmapSource"/>
    /// </summary>
    public class ExportToBitmap
    {
        #region CONSTANTS

        #endregion //CONSTANTS

        #region FIELDS

        #endregion //FIELDS

        #region CONSTRUCTION

        #endregion //CONSTRUCTION

        #region EVENTS

        #endregion EVENTS

        #region PROPERTIES

        #endregion //PROPERTIES

        #region PUBLICMETHODS

        /// <summary>
        /// Converts a given <see cref="BitmapSource"/> into a <see cref="System.Drawing.Bitmap"/>
        /// </summary>
        /// <param name="bitmapsource">The <see cref="BitmapSource"/> to be converted.</param>
        /// <returns>An exact copy of the <see cref="BitmapSource"/> as a <see cref="System.Drawing.Bitmap"/></returns>
        public static Bitmap BitmapFromSource(BitmapSource bitmapsource)
        {
            Bitmap bitmap;
            using (var outStream = new MemoryStream())
            {
                // from System.Media.BitmapImage to System.Drawing.Bitmap 
                BitmapEncoder enc = new BmpBitmapEncoder();
                enc.Frames.Add(BitmapFrame.Create(bitmapsource));
                enc.Save(outStream);
                bitmap = new Bitmap(outStream);
                //bitmap.Save(@"c:\result.jpg");
            }

            return bitmap;
        }

        [DllImport("gdi32.dll", EntryPoint = "DeleteObject")]
        public static extern IntPtr DeleteObject(IntPtr hDc);


        /// <summary>
        /// Converts a given <see cref="System.Drawing.Bitmap"/> into a <see cref="BitmapSource"/>
        /// </summary>
        /// <param name="bitmap">The <see cref="System.Drawing.Bitmap"/> to be converted.</param>
        /// <returns>An exact copy of the <see cref="System.Drawing.Bitmap"/> as a <see cref="BitmapSource"/></returns>
        public static BitmapSource BitmapToSource(Bitmap bitmap)
        {
            IntPtr bitmapPointer = bitmap.GetHbitmap();
            BitmapSizeOptions sizeOptions = BitmapSizeOptions.FromEmptyOptions();
            BitmapSource destination = Imaging.CreateBitmapSourceFromHBitmap(bitmapPointer, IntPtr.Zero, Int32Rect.Empty, sizeOptions);
            destination.Freeze();

            // Must delete int pointer to avoid memory leak
            DeleteObject(bitmapPointer);

            return destination;
        }

        #endregion //PUBLICMETHODS

        #region OVERRIDES

        #endregion //OVERRIDES

        #region EVENTHANDLER

        #endregion //EVENTHANDLER

        #region THREAD

        #endregion //THREAD

        #region PRIVATEMETHODS

        #endregion //PRIVATEMETHODS

        #region HELPER

        #endregion //HELPER

        ///////////////////////////////////////////////////////////////////////////////
        // Defining Constants                                                        //
        ///////////////////////////////////////////////////////////////////////////////

        ///////////////////////////////////////////////////////////////////////////////
        // Defining Variables, Enumerations, Events                                  //
        ///////////////////////////////////////////////////////////////////////////////

        ///////////////////////////////////////////////////////////////////////////////
        // Construction and Initializing methods                                     //
        ///////////////////////////////////////////////////////////////////////////////

        ///////////////////////////////////////////////////////////////////////////////
        // Defining events, enums, delegates                                         //
        ///////////////////////////////////////////////////////////////////////////////

        ///////////////////////////////////////////////////////////////////////////////
        // Defining Properties                                                       //
        ///////////////////////////////////////////////////////////////////////////////

        ///////////////////////////////////////////////////////////////////////////////
        // Public methods                                                            //
        ///////////////////////////////////////////////////////////////////////////////

        ///////////////////////////////////////////////////////////////////////////////
        // Inherited methods                                                         //
        ///////////////////////////////////////////////////////////////////////////////

        ///////////////////////////////////////////////////////////////////////////////
        // Eventhandler                                                              //
        ///////////////////////////////////////////////////////////////////////////////

        ///////////////////////////////////////////////////////////////////////////////
        // Methods and Eventhandling for Background tasks                            //
        ///////////////////////////////////////////////////////////////////////////////

        ///////////////////////////////////////////////////////////////////////////////
        // Methods for doing main class job                                          //
        ///////////////////////////////////////////////////////////////////////////////

        ///////////////////////////////////////////////////////////////////////////////
        // Small helping Methods                                                     //
        ///////////////////////////////////////////////////////////////////////////////
    }
}