// <copyright file="ImageFromVectorGraphics.cs" company="FU Berlin">
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

namespace Ogama.Modules.Replay.Video
{
  using System;
  using System.Drawing;
  using System.Drawing.Imaging;
  using System.Runtime.InteropServices;

  using DirectShowLib;

  using OgamaControls;

  using VectorGraphics.Tools.CustomEventArgs;

  /// <summary>
  /// Class to provide image data for the direct show video.
  /// This class just generates bitmaps from the vector graphics canvas
  /// and fills them into the video.
  /// </summary>
  public class ImageFromVectorGraphics : ImageHandler
  {
    ///////////////////////////////////////////////////////////////////////////////
    // Defining Constants                                                        //
    ///////////////////////////////////////////////////////////////////////////////
    #region CONSTANTS

    /// <summary>
    /// How many frames to show the bitmap in.  Using 1 will return a new
    /// image for each frame.  Setting it to 5 would show the same image
    /// in 5 frames, etc.
    /// </summary>
    private const int DIV = 1;

    /// <summary>
    /// How many bits per pixel for each frame.
    /// </summary>
    private const int BPP = 32;

    #endregion //CONSTANTS

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Variables, Enumerations, Events                                  //
    ///////////////////////////////////////////////////////////////////////////////
    #region FIELDS

    /// <summary>
    /// The <see cref="Size"/> of the video.
    /// </summary>
    private Size videoSize;

    /// <summary>
    /// Number of frames per second
    /// </summary>
    private long framesPerSecond;

    /// <summary>
    /// Contains the IntPtr to the raw data
    /// </summary>
    private BitmapData bitmapVGData;

    /// <summary>
    /// The bitmap for the video.
    /// </summary>
    private Bitmap bitmapVG;

    /// <summary>
    /// The section start time for the video export in units
    /// of the trial.
    /// </summary>
    private long sectionStartTime;

    /// <summary>
    /// The section end time for the video export in units
    /// of the trial.
    /// </summary>
    private long sectionEndTime;

    /// <summary>
    /// Milliseconds time span between frames.
    /// </summary>
    private int frameTimeSpan;

    /// <summary>
    /// The starttime for the new image in samples.
    /// </summary>
    private int newStartTimeInSamples;

    /// <summary>
    /// The <see cref="VideoFramePusher"/> that contains the
    /// trial video.
    /// </summary>
    private VideoFramePusher videoFramePusher;

    /// <summary>
    /// The delegate method for the render frame method
    /// </summary>
    private Ogama.Modules.Replay.ReplayPicture.RenderFrameHandler imageRenderer;

    #endregion //FIELDS

    ///////////////////////////////////////////////////////////////////////////////
    // Construction and Initializing methods                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region CONSTRUCTION

    /// <summary>
    /// Initializes a new instance of the ImageFromVectorGraphics class.
    /// </summary>
    /// <param name="newVideoProperties">A <see cref="CaptureDeviceProperties"/> with the properties to use for this object.</param>
    /// <param name="newSectionStartTime">A <see cref="long"/> with the start time of the video section.</param>
    /// <param name="newSectionEndTime">A <see cref="long"/> with the end time of the video section.</param>
    /// <param name="newVideoFramePusher">A <see cref="VideoFramePusher"/> that populates the image with a background video frame.</param>
    /// <param name="newImageRenderer">The <see cref="Ogama.Modules.Replay.ReplayPicture.RenderFrameHandler"/> that is the delegate to get the images from.</param>
    public ImageFromVectorGraphics(
      CaptureDeviceProperties newVideoProperties,
      long newSectionStartTime,
      long newSectionEndTime,
      VideoFramePusher newVideoFramePusher,
      Ogama.Modules.Replay.ReplayPicture.RenderFrameHandler newImageRenderer)
    {
      int fps = newVideoProperties.FrameRate;
      this.framesPerSecond = UNIT / fps;
      this.frameTimeSpan = (int)(1000f / fps);
      this.videoSize = newVideoProperties.VideoSize;
      this.sectionStartTime = newSectionStartTime;
      this.sectionEndTime = newSectionEndTime;
      this.videoFramePusher = newVideoFramePusher;
      this.imageRenderer = newImageRenderer;
    }

    #endregion //CONSTRUCTION

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Enumerations                                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region ENUMS

    /// <summary>
    /// This event occurs, whenever a new frame has arrived, it tells
    /// the listener the percentage of the progress.
    /// </summary>
    public event EventHandler<ProgressEventArgs> Progress;

    /// <summary>
    /// This event occurs, whenever there is no more data available 
    /// from the picture
    /// </summary>
    public event EventHandler<EventArgs> Finished;

    #endregion ENUMS

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Properties                                                       //
    ///////////////////////////////////////////////////////////////////////////////
    #region PROPERTIES
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
    /// Overridden. Sets the media type on the IGenericSampleConfig
    /// </summary>
    /// <param name="psc">The <see cref="IGenericSampleConfig"/></param>
    public override void SetMediaType(IGenericSampleConfig psc)
    {
      BitmapInfoHeader bmi = new BitmapInfoHeader();

      // Build a BitmapInfo struct using the parms from the file
      bmi.Size = Marshal.SizeOf(typeof(BitmapInfoHeader));
      bmi.Width = this.videoSize.Width;
      bmi.Height = this.videoSize.Height;
      bmi.Planes = 1;
      bmi.BitCount = BPP;
      bmi.Compression = 0;
      bmi.ImageSize = (bmi.BitCount / 8) * bmi.Width * bmi.Height;
      bmi.XPelsPerMeter = 0;
      bmi.YPelsPerMeter = 0;
      bmi.ClrUsed = 0;
      bmi.ClrImportant = 0;

      int hr = psc.SetMediaTypeFromBitmap(bmi, this.framesPerSecond);
      DsError.ThrowExceptionForHR(hr);
    }

    /// <summary>
    /// Populate the data buffer.  In this class we retreive the new bitmap from
    /// the imageRenderer callback.
    /// </summary>
    /// <param name="frameNumber">Frame number</param>
    /// <param name="pointer">A pointer to the memory to populate with the bitmap data</param>
    /// <param name="size">Size of the bitmap in bytes.</param>
    /// <param name="sizeRead">Out. Size read.</param>
    /// <returns>0 on success and 1 on end of stream</returns>
    public override int GetImage(int frameNumber, IntPtr pointer, int size, out int sizeRead)
    {
      int hr = 0;

      if (frameNumber % DIV == 0)
      {
        int currentTime = (int)(this.sectionStartTime + frameNumber * this.frameTimeSpan);
        if (currentTime < this.sectionEndTime)
        {
          long sampleTime = frameNumber == 0 ? this.newStartTimeInSamples : currentTime + this.frameTimeSpan;

          // Get image from the video
          if (this.videoFramePusher != null)
          {
            this.videoFramePusher.ShowSampleAtMS(sampleTime);
          }

          var reachedEnd = false;

          // Get image from the Canvas
          Bitmap bmp = this.imageRenderer(
           ref this.newStartTimeInSamples,
           sampleTime,
           this.videoSize,
           ref reachedEnd);

          // By default do a vertical flip for the image
          bmp.RotateFlip(RotateFlipType.RotateNoneFlipY);

          Rectangle r = new Rectangle(0, 0, bmp.Width, bmp.Height);


          // Store the pointers
          this.bitmapVG = bmp;
          this.bitmapVGData = this.bitmapVG.LockBits(r, ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);

          // Only do the copy if we have a place to put the data
          if (pointer != IntPtr.Zero)
          {
            // Copy from the bmd to the MediaSample
            Kernel32.CopyMemory(pointer, this.bitmapVGData.Scan0, size);
          }

          // Release the previous image
          if (this.bitmapVGData != null)
          {
            this.bitmapVG.UnlockBits(this.bitmapVGData);
            this.bitmapVG.Dispose();
          }

          if (this.Progress != null)
          {
            int percentComplete = (int)((float)currentTime / (this.sectionEndTime - this.sectionStartTime) * 100);
            this.Progress(this, new ProgressEventArgs(false, percentComplete, currentTime));
          }

          if (reachedEnd)
          {
            if (this.Finished != null)
            {
              this.Finished(this, EventArgs.Empty);
            }

            sizeRead = size;

            return 1; // End of stream
          }

          if (sampleTime > this.sectionEndTime - 2 * this.frameTimeSpan)
          {
            //// Console.WriteLine("Finish=true");
            if (this.Finished != null)
            {
              this.Finished(this, EventArgs.Empty);
            }

            hr = 1; // End of stream
          }
        }
        else
        {
          //// Console.WriteLine("Finish=true");
          if (this.Finished != null)
          {
            this.Finished(this, EventArgs.Empty);
          }

          hr = 1; // End of stream
        }
      }

      sizeRead = size;

      return hr;
    }

    /// <summary>
    /// Calculate the timestamps based on the frame number and the frames per second.
    /// </summary>
    /// <param name="sample">The <see cref="IMediaSample"/> to be timed.</param>
    /// <returns>0 = success, negative values for errors</returns>
    public override int SetTimeStamps(IMediaSample sample)
    {
      // Calculate the start/end times based on the current frame number
      // and frame rate
      DsLong start = new DsLong(this.FrameNumber * this.framesPerSecond);
      DsLong stop = new DsLong(start + this.framesPerSecond);

      // Set the times into the sample
      int hr = sample.SetTime(start, stop);

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