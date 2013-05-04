// <copyright file="ImageHandler.cs" company="FU Berlin">
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

namespace Ogama.Modules.Replay.Video
{
  using System;
  using System.Runtime.InteropServices;

  using DirectShowLib;

  /// <summary>
  /// A generic class to support easily changing between my different sources of data.
  /// </summary>
  /// <remarks>Note: You DON'T have to use this class, or anything like it. 
  /// The key is the SampleCallback routine.  How/where you get your bitmaps is ENTIRELY up to you.  
  /// Having SampleCallback call members of this class was just
  /// the approach I used to isolate the data handling.</remarks>
  public abstract class ImageHandler : IDisposable, IGenericSampleCB
  {
    ///////////////////////////////////////////////////////////////////////////////
    // Defining Constants                                                        //
    ///////////////////////////////////////////////////////////////////////////////
    #region CONSTANTS

    /// <summary>
    /// 100 ns - used by a number of DS methods
    /// </summary>
    protected const long UNIT = 10000000;

    #endregion //CONSTANTS

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Variables, Enumerations, Events                                  //
    ///////////////////////////////////////////////////////////////////////////////
    #region FIELDS

    /// <summary>
    /// Number of callbacks that returned a positive result
    /// </summary>
    private int frameNumber;

    #endregion //FIELDS

    ///////////////////////////////////////////////////////////////////////////////
    // Construction and Initializing methods                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region CONSTRUCTION
    #endregion //CONSTRUCTION

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Enumerations                                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region ENUMS
    #endregion ENUMS

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Properties                                                       //
    ///////////////////////////////////////////////////////////////////////////////
    #region PROPERTIES

    /// <summary>
    /// Gets or sets the number of callbacks that returned a positive result
    /// </summary>
    protected int FrameNumber
    {
      get { return this.frameNumber; }
      set { this.frameNumber = value; }
    }
    
    #endregion //PROPERTIES

    ///////////////////////////////////////////////////////////////////////////////
    // Public methods                                                            //
    ///////////////////////////////////////////////////////////////////////////////
    #region PUBLICMETHODS
    #endregion //PUBLICMETHODS

    ///////////////////////////////////////////////////////////////////////////////
    // Inherited methods                                                         //
    ///////////////////////////////////////////////////////////////////////////////
    #region OVERRIDES

    /// <summary>
    /// Dispose. Does nothing.
    /// </summary>
    public virtual void Dispose()
    {
    }

    /// <summary>
    /// Set the media type on the IGenericSampleConfig
    /// </summary>
    /// <param name="psc">The <see cref="IGenericSampleConfig"/></param>
    public abstract void SetMediaType(IGenericSampleConfig psc);

    /// <summary>
    /// Populate the data buffer.  In this class we retreive the new bitmap from
    /// the imageRenderer callback.
    /// </summary>
    /// <param name="frameNumber">Frame number</param>
    /// <param name="pointer">A pointer to the memory to populate with the bitmap data</param>
    /// <param name="size">Size of the bitmap in bytes.</param>
    /// <param name="sizeRead">Out. Size read.</param>
    /// <returns>0 on success and 1 on end of stream</returns>
    public abstract int GetImage(int frameNumber, IntPtr pointer, int size, out int sizeRead);

    /// <summary>
    /// Calculate the timestamps based on the frame number and 
    /// the frames per second
    /// </summary>
    /// <param name="sample">The <see cref="IMediaSample"/> to be timed.</param>
    /// <returns>0 on success and negative values for errors</returns>
    public virtual int SetTimeStamps(IMediaSample sample)
    {
      return 0;
    }

    /// <summary>
    /// Called by the GenericSampleSourceFilter.  This routine populates the MediaSample.
    /// </summary>
    /// <param name="sample">Pointer to a sample</param>
    /// <returns>0 = success, 1 = end of stream, negative values for errors</returns>
    public virtual int SampleCallback(IMediaSample sample)
    {
      int hr;
      IntPtr dataPointer;

      try
      {
        // Get the buffer into which we will copy the data
        hr = sample.GetPointer(out dataPointer);
        if (hr >= 0)
        {
          // Set TRUE on every sample for uncompressed frames
          hr = sample.SetSyncPoint(true);
          if (hr >= 0)
          {
            // Find out the amount of space in the buffer
            int callbackData = sample.GetSize();

            hr = this.SetTimeStamps(sample);
            if (hr >= 0)
            {
              int read;

              // Get copy the data into the sample
              hr = this.GetImage(this.frameNumber, dataPointer, callbackData, out read);
              
              // 1 == End of stream
              if (hr == 0) 
              {
                sample.SetActualDataLength(read);

                // progress the frame number for next time
                this.frameNumber++;
              }
            }
          }
        }
      }
      finally
      {
        // Release our pointer the the media sample.  THIS IS ESSENTIAL!  If
        // you don't do this, the graph will stop after about 2 samples.
        Marshal.ReleaseComObject(sample);
      }

      return hr;
    }

    #endregion //OVERRIDES

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
    // Methods for doing main class job                                          //
    ///////////////////////////////////////////////////////////////////////////////
    #region PRIVATEMETHODS
    #endregion //PRIVATEMETHODS

    ///////////////////////////////////////////////////////////////////////////////
    // Small helping Methods                                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region HELPER
    #endregion //HELPER
  }
}