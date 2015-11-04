// <copyright file="DmoOverlay.cs" company="FU Berlin">
// ****************************************************************************
// While the underlying libraries are covered by LGPL, this sample is released
// as public domain.  It is distributed in the hope that it will be useful, but
// WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY
// or FITNESS FOR A PARTICULAR PURPOSE.
// *****************************************************************************/
// </copyright>

namespace DmoOverlay
{
  using System;
  using System.Diagnostics;
  using System.Drawing;
  using System.Drawing.Imaging;
  using System.Runtime.InteropServices;

  using DirectShowLib;
  using DirectShowLib.DMO;

  using DmoBase;

  /// <summary>
  /// This COM-Visible class is a DMO (digital media object)
  /// that can be inserted into a direct show filter graph.
  /// Its purpose is to render a mouse and gaze cursor over
  /// the video stream at the positions given in the parameters.
  /// </summary>
  [ComVisible(true),
  Guid("DFF225E1-36D2-4b77-A8CD-907A80B0A698"),
  ClassInterface(ClassInterfaceType.None)]
  public class DmoOverlay : IMediaObjectImpl
  {
    ///////////////////////////////////////////////////////////////////////////////
    // Defining Constants                                                        //
    ///////////////////////////////////////////////////////////////////////////////
    #region CONSTANTS

    /// <summary>
    /// The category of this DMO. That is a <see cref="DMOCategory.VideoEffect"/>
    /// </summary>
    private static readonly Guid DMOCat = DMOCategory.VideoEffect;

    /// <summary>
    /// Number of parameters of this DMO
    /// </summary>
    private const int NumParams = 4;

    /// <summary>
    /// The name of this DMO to be saved into the registry
    /// </summary>
    private const string DMOName = "DmoOverlay";

    /// <summary>
    /// The input pin count of this DMO.
    /// </summary>
    private const int InputPinCount = 1;

    /// <summary>
    /// The output pin count of this DMO.
    /// </summary>
    private const int OutputPinCount = 1;

    /// <summary>
    /// A value for the maximum time.
    /// </summary>
    private const long MaxTime = long.MaxValue;

    /// <summary>
    /// Index of alpha component of a byte pointer
    /// </summary>
    private const short RGBA = 3;

    /// <summary>
    /// Index of red component of a byte pointer
    /// </summary>
    private const short RGBR = 2;

    /// <summary>
    /// Index of green component of a byte pointer
    /// </summary>
    private const short RGBG = 1;

    /// <summary>
    /// Index of blue component of a byte pointer
    /// </summary>
    private const short RGBB = 0;

    #endregion //CONSTANTS

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Variables, Enumerations, Events                                  //
    ///////////////////////////////////////////////////////////////////////////////
    #region FIELDS

    /// <summary>
    /// The width of the video stream
    /// </summary>
    private int streamWidth;

    /// <summary>
    /// The height of the video stream
    /// </summary>
    private int streamHeight;

    /// <summary>
    /// The stride of the video stream
    /// </summary>
    private int streamStride;

    /// <summary>
    /// The bits per pixel of the video stream
    /// </summary>
    private int streamBBP;

    /// <summary>
    /// The time stamp of the current processed video buffer
    /// </summary>
    private long bufferTimeStamp;

    /// <summary>
    /// The time length of the current processed video buffer
    /// </summary>
    private long bufferTimeLength;

    /// <summary>
    /// The <see cref="IMediaBuffer"/> for the current processed video buffer
    /// </summary>
    private IMediaBuffer buffer;

    /// <summary>
    /// The pointer to the current processed video buffer
    /// </summary>
    private IntPtr bufferPointer;

    /// <summary>
    /// The byte count of the current processed video buffer
    /// </summary>
    private int bufferByteCount;

    /// <summary>
    /// The <see cref="DMOOutputDataBufferFlags"/> of the current processed video buffer
    /// </summary>
    private DMOOutputDataBufferFlags bufferFlags;

    /// <summary>
    /// The <see cref="Size"/> of the mouse cursor overlay bitmap.
    /// </summary>
    private Size mouseCursorSize;

    /// <summary>
    /// The <see cref="BitmapData"/> of the mouse cursor overlay bitmap.
    /// </summary>
    private BitmapData mouseCursorData;

    /// <summary>
    /// The <see cref="Size"/> of the gaze cursor overlay bitmap.
    /// </summary>
    private Size gazeCursorSize;

    /// <summary>
    /// The <see cref="BitmapData"/> of the gaze cursor overlay bitmap.
    /// </summary>
    private BitmapData gazeCursorData;

    /// <summary>
    /// An array of <see cref="byte"/> that contain the argb bytes of
    /// the gaze cursor overlay bitmap.
    /// </summary>
    private byte[] gazeCursorArgbValues;

    /// <summary>
    /// An array of <see cref="byte"/> that contain the argb bytes of
    /// the mouse cursor overlay bitmap.
    /// </summary>
    private byte[] mouseCursorArgbValues;

    #endregion //FIELDS

    ///////////////////////////////////////////////////////////////////////////////
    // Construction and Initializing methods                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region CONSTRUCTION

    /// <summary>
    /// Initializes a new instance of the DmoOverlay class.  
    /// The parameters to the base class
    /// describe the number of input and output streams, which
    /// DirectShow calls Pins, followed by the number of parameters
    /// this class supports (can be zero), and the timeformat of those
    /// parameters (should include ParamClass.TimeFormatFlags.Reference
    /// if NumParameters > 0).
    /// </summary>
    public DmoOverlay()
      : base(InputPinCount, OutputPinCount, NumParams, TimeFormatFlags.Reference)
    {
      // Initialize the data members
      this.streamWidth = 0;
      this.streamHeight = 0;
      this.streamStride = 0;
      this.streamBBP = 0;
      this.bufferTimeStamp = 0;
      this.bufferTimeLength = 0;
      this.bufferByteCount = 0;
      this.bufferFlags = 0;
      this.bufferPointer = IntPtr.Zero;
      this.buffer = null;

      // Start describing the parameters this DMO supports.  Building this
      // structure (painful as it is) will allow the base class to automatically
      // support IMediaParamInfo & IMediaParams, which allow clients to find
      // out what parameters you support, and to set them.

      // See the MSDN
      // docs for MP_PARAMINFO for a description of the other parameters
      ParamInfo gazeX = new ParamInfo();
      gazeX.mopCaps = MPCaps.Jump;
      gazeX.mpdMinValue.vInt = 0;
      gazeX.mpdMaxValue.vInt = 2000;
      gazeX.mpdNeutralValue.vInt = 1;
      gazeX.mpType = MPType.INT;
      gazeX.szLabel = "GazeX";
      gazeX.szUnitText = "Pixel";

      ParamInfo gazeY = new ParamInfo();
      gazeY.mopCaps = MPCaps.Jump;
      gazeY.mpdMinValue.vInt = 0;
      gazeY.mpdMaxValue.vInt = 2000;
      gazeY.mpdNeutralValue.vInt = 1;
      gazeY.mpType = MPType.INT;
      gazeY.szLabel = "GazeY";
      gazeY.szUnitText = "Pixel";

      ParamInfo mouseX = new ParamInfo();
      mouseX.mopCaps = MPCaps.Jump;
      mouseX.mpdMinValue.vInt = 0;
      mouseX.mpdMaxValue.vInt = 2000;
      mouseX.mpdNeutralValue.vInt = 1;
      mouseX.mpType = MPType.INT;
      mouseX.szLabel = "MouseX";
      mouseX.szUnitText = "Pixel";

      ParamInfo mouseY = new ParamInfo();
      mouseY.mopCaps = MPCaps.Jump;
      mouseY.mpdMinValue.vInt = 0;
      mouseY.mpdMaxValue.vInt = 2000;
      mouseY.mpdNeutralValue.vInt = 1;
      mouseY.mpType = MPType.INT;
      mouseY.szLabel = "MouseY";
      mouseY.szUnitText = "Pixel";

      // Parameter #0, using the struct, and a format string (described in MSDN
      // under IMediaParamInfo::GetParamText).  Note that when marshaling strings,
      // .NET will add another \0
      ParamDefine(0, gazeX, "GazeX\0Pixel\0");
      ParamDefine(1, gazeY, "GazeY\0Pixel\0");
      ParamDefine(2, mouseX, "MouseX\0Pixel\0");
      ParamDefine(3, mouseY, "MouseY\0Pixel\0");

      // Initialize the buffers for the gaze and mouse cursor overlay bitmaps.
      Bitmap circleBitmap = (Bitmap)Properties.Resources.Circle;
      Rectangle circleRect = new Rectangle(0, 0, circleBitmap.Width, circleBitmap.Height);
      this.gazeCursorSize = circleRect.Size;
      this.gazeCursorData = circleBitmap.LockBits(circleRect, ImageLockMode.ReadOnly, circleBitmap.PixelFormat);

      // Get the address of the first line.
      IntPtr gazeCursorScan0Pointer = this.gazeCursorData.Scan0;

      // Declare an array to hold the bytes of the bitmap.
      int gazeCursorBytes = this.gazeCursorData.Stride * this.gazeCursorData.Height;
      this.gazeCursorArgbValues = new byte[gazeCursorBytes];

      // Copy the RGB values into the array.
      Marshal.Copy(gazeCursorScan0Pointer, this.gazeCursorArgbValues, 0, gazeCursorBytes);

      Bitmap arrowBitmap = Properties.Resources.Arrow;
      Rectangle cursorRect = new Rectangle(0, 0, arrowBitmap.Width, arrowBitmap.Height);
      this.mouseCursorSize = cursorRect.Size;
      this.mouseCursorData = arrowBitmap.LockBits(cursorRect, ImageLockMode.ReadOnly, arrowBitmap.PixelFormat);

      // Get the address of the first line.
      IntPtr mouseCursorScan0Pointer = this.mouseCursorData.Scan0;

      // Declare an array to hold the bytes of the bitmap.
      int mouseCursorBytes = this.mouseCursorData.Stride * this.mouseCursorData.Height;
      this.mouseCursorArgbValues = new byte[mouseCursorBytes];

      // Copy the RGB values into the array.
      Marshal.Copy(mouseCursorScan0Pointer, this.mouseCursorArgbValues, 0, mouseCursorBytes);
    }

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
    /// Given a specific AMMediaType, we are asked if we support it
    /// </summary>
    /// <param name="inputStreamIndex">Stream number</param>
    /// <param name="pmt">The AMMediaType to check</param>
    /// <returns>S_OK if it is supported, DMOResults.E_InvalidType if not</returns>
    protected override int InternalCheckInputType(int inputStreamIndex, AMMediaType pmt)
    {
      int hr;

      // Check the format is defined
      if (pmt.majorType == MediaType.Video &&
          (pmt.subType == MediaSubType.RGB24 || pmt.subType == MediaSubType.RGB32) &&
          pmt.formatType == FormatType.VideoInfo &&
          pmt.formatPtr != IntPtr.Zero)
      {
        hr = SOK;
      }
      else
      {
        hr = DMOResults.E_InvalidType;
      }

      return hr;
    }

    /// <summary>
    /// Given a specific AMMediaType, we are asked if we support it
    /// </summary>
    /// <param name="outputStreamIndex">Stream number</param>
    /// <param name="pmt">The AMMediaType to check</param>
    /// <returns>S_OK if it is supported, DMOResults.E_InvalidType if not</returns>
    protected override int InternalCheckOutputType(int outputStreamIndex, AMMediaType pmt)
    {
      int hr;

      AMMediaType pIn = InputType(0);

      // We don't support anything until after our input pin is set
      if (pIn != null)
      {
        // Our output type must be the same as the input type
        if (TypesMatch(pmt, pIn))
        {
          hr = SOK;
        }
        else
        {
          hr = DMOResults.E_InvalidType;
        }
      }
      else
      {
        hr = DMOResults.E_InvalidType;
      }

      return hr;
    }

    /// <summary>
    /// Get the list of supported types.  Note this it is NOT required that any types be returned here.
    /// If no types are returned, connectors just try media types (InternalCheckInputType) until we
    /// accept one.
    /// </summary>
    /// <param name="inputStreamIndex">Stream number</param>
    /// <param name="typeIndex">Index into the array of media types we support</param>
    /// <param name="pmt">Out the <see cref="AMMediaType"/>.</param>
    /// <returns>DMOResults.E_NoMoreItems if out of range, else S_OK</returns>
    protected override int InternalGetInputType(int inputStreamIndex, int typeIndex, out AMMediaType pmt)
    {
      int hr;

      switch (typeIndex)
      {
        case 0:
          pmt = new AMMediaType();
          pmt.majorType = MediaType.Video;
          pmt.subType = MediaSubType.RGB32;
          pmt.formatType = FormatType.VideoInfo;
          hr = SOK;
          break;

        case 1:
          pmt = new AMMediaType();
          pmt.majorType = MediaType.Video;
          pmt.subType = MediaSubType.RGB24;
          pmt.formatType = FormatType.VideoInfo;
          hr = SOK;
          break;

        default:
          pmt = null;
          hr = DMOResults.E_NoMoreItems;
          break;
      }

      return hr;
    }

    /// <summary>
    /// What size (and alignment) do we require of our output buffer?
    /// </summary>
    /// <param name="outputStreamIndex">Stream number</param>
    /// <param name="pcbSize">returns the buffer size needed</param>
    /// <param name="pcbAlignment">Returns the alignment needed (don't use zero!)</param>
    /// <returns>Returns always S_OK</returns>
    protected override int InternalGetOutputSizeInfo(int outputStreamIndex, out int pcbSize, out int pcbAlignment)
    {
      pcbAlignment = 1;
      AMMediaType pmt = OutputType(0);

      VideoInfoHeader v = new VideoInfoHeader();
      Marshal.PtrToStructure(pmt.formatPtr, v);

      pcbSize = v.BmiHeader.ImageSize;

      return SOK;
    }

    /// <summary>
    /// Flush releases all input buffers without processing them
    /// </summary>
    /// <returns>Returns always S_OK</returns>
    protected override int InternalFlush()
    {
      InternalDiscontinuity(0);

      // Release buffers
      this.ReleaseInputBuffs();
      this.bufferTimeStamp = 0;

      return SOK;
    }

    /// <summary>
    /// Our chance to allocate any storage we may need
    /// </summary>
    /// <returns>Returns always S_OK</returns>
    protected override int InternalAllocateStreamingResources()
    {
      // Reinitialize variables
      InternalDiscontinuity(0);

      AMMediaType pmt = InputType(0);
      VideoInfoHeader v = new VideoInfoHeader();

      Marshal.PtrToStructure(pmt.formatPtr, v);
      this.streamWidth = v.BmiHeader.Width;
      this.streamHeight = v.BmiHeader.Height;
      this.streamBBP = v.BmiHeader.BitCount / 8;
      this.streamStride = v.BmiHeader.Width * this.streamBBP;

      return SOK;
    }

    /// <summary>
    /// Accept the input buffers to be processed.  You'll want to read
    /// the MSDN docs on this one.  One point worth noting is that DMO
    /// doesn't require that one complete block be passed at a time.
    /// Picture a case where raw data is being read from a file, and your
    /// DMO is the first to process it.  The chunk of data you receive
    /// might represent one image, 5 images, half an image, etc.  Likewise,
    /// your input could contain both video and audio that you are splitting
    /// into two output streams.
    /// That helps explain some of the parameters you see here and in
    /// InternalProcessOutput.
    /// Note that while DMO doesn't insist on it, for this sample, we
    /// specifically request that only complete buffers be provided.
    /// </summary>
    /// <param name="inputStreamIndex">Stream Index</param>
    /// <param name="mediaBuffer">Interface that holds the input data</param>
    /// <param name="flags">Flags to control input processing</param>
    /// <param name="timestamp">Timestamp of the sample</param>
    /// <param name="timelength">Duration of the sample</param>
    /// <returns>S_FALSE if there is no output, S_OK otherwise</returns>
    protected override int InternalProcessInput(
        int inputStreamIndex,
        [In] IMediaBuffer mediaBuffer,
        DMOInputDataBuffer flags,
        long timestamp,
        long timelength)
    {
      // Check state - if we already have a buffer, we shouldn't be getting another
      Debug.Assert(this.buffer == null, "We already have a buffer, we shouldn't be getting another");

      int hr = mediaBuffer.GetBufferAndLength(out this.bufferPointer, out this.bufferByteCount);
      if (hr >= 0)
      {
        // Ignore zero length buffers
        if (this.bufferByteCount > 0)
        {
          this.buffer = mediaBuffer;

          // Cast the input flags to become output flags
          this.bufferFlags = (DMOOutputDataBufferFlags)flags;

          // If there is a time, store it
          if (0 == (flags & DMOInputDataBuffer.Time))
          {
            this.bufferTimeStamp = MaxTime;
          }
          else
          {
            this.bufferTimeStamp = timestamp;
          }

          // If there is a TimeLength, store it
          if (0 == (flags & DMOInputDataBuffer.TimeLength))
          {
            this.bufferTimeLength = -1;
          }
          else
          {
            this.bufferTimeLength = timelength;
          }

          hr = SOK;
        }
        else
        {
          this.ReleaseInputBuffs();
          hr = SFALSE;
        }
      }

      return hr;
    }

    /// <summary>
    /// Given output buffers, process the input buffers into the output buffers.
    /// </summary>
    /// <param name="flags">A <see cref="DMOProcessOutput"/> Flags</param>
    /// <param name="outputBufferCount">Number of buffers (will be one per output stream)</param>
    /// <param name="outputBufferPointers">The buffers</param>
    /// <param name="pdwStatus">Reserved: 0</param>
    /// <returns>S_FALSE if there is no output, S_OK otherwise</returns>
    protected override int InternalProcessOutput(
        DMOProcessOutput flags,
        int outputBufferCount,
        [In, Out] DMOOutputDataBuffer[] outputBufferPointers,
        out int pdwStatus)
    {
      // Check buffer
      IntPtr outputPointer;
      int outputByteCount;
      int currentByteCount;
      int hr = SOK;

      pdwStatus = 0;

      // No input buffers to process
      if (this.buffer != null)
      {
        if (outputBufferPointers[0].pBuffer != null)
        {
          // Get a pointer to the output buffer
          hr = outputBufferPointers[0].pBuffer.GetBufferAndLength(out outputPointer, out currentByteCount);
          if (hr >= 0)
          {
            hr = outputBufferPointers[0].pBuffer.GetMaxLength(out outputByteCount);
            if (hr >= 0)
            {
              // Make sure we have room
              if (outputByteCount >= currentByteCount + OutputType(0).sampleSize)
              {
                // Get the mode for the current timecode
                MPData gazeX = ParamCalcValueForTime(0, this.bufferTimeStamp);
                MPData gazeY = ParamCalcValueForTime(1, this.bufferTimeStamp);
                MPData mouseX = ParamCalcValueForTime(2, this.bufferTimeStamp);
                MPData mouseY = ParamCalcValueForTime(3, this.bufferTimeStamp);

                // Process from input to output according to the mode
                this.DoOverlay(
                  (IntPtr)(outputPointer.ToInt32() + currentByteCount),
                  this.bufferByteCount,
                  this.bufferPointer,
                  this.streamBBP,
                  new Point(gazeX.vInt, gazeY.vInt),
                  new Point(mouseX.vInt, mouseY.vInt));

                // Keep the flags & time info from the input
                outputBufferPointers[0].dwStatus = this.bufferFlags;
                outputBufferPointers[0].rtTimelength = this.bufferTimeLength;
                outputBufferPointers[0].rtTimestamp = this.bufferTimeStamp;

                // Release the buffer.  Since we are always processing one buffer at
                // a time, we always release on completion.  If our input might be
                // more than one buffer, we would only release the input when it had
                // be complete processed.
                this.ReleaseInputBuffs();

                // Say we've filled the buffer
                hr = outputBufferPointers[0].pBuffer.SetLength(outputByteCount);
              }
              else
              {
                hr = EINVALIDARG;
              }
            }
          }
        }
        else
        {
          // No output buffer provided.  Happens in the DMO Wrapper if one of
          // the output pins is not connected.
          outputBufferPointers[0].dwStatus = this.bufferFlags;
          outputBufferPointers[0].rtTimelength = this.bufferTimeLength;
          outputBufferPointers[0].rtTimestamp = this.bufferTimeStamp;

          // Release the buffer.  Since we are always processing one buffer at
          // a time, we always release on completion.  If our input might be
          // more than one buffer, we would only release the input when it had
          // be complete processed.
          this.ReleaseInputBuffs();
        }
      }
      else
      {
        hr = SFALSE;
      }

      return hr;
    }

    /// <summary>
    /// Are we able to accept more input at this time?
    /// </summary>
    /// <param name="inputStreamIndex">Stream number</param>
    /// <returns>S_OK if we can, else S_FALSE</returns>
    protected override int InternalAcceptingInput(int inputStreamIndex)
    {
      return this.buffer == null ? SOK : SFALSE;
    }

    /// <summary>
    /// This method returns the current buffers time stamp.
    /// </summary>
    /// <returns>A <see cref="Int64"/> with the current buffers time stamp.</returns>
    protected override long InternalGetCurrentTime()
    {
      return this.bufferTimeStamp;
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

    /// <summary>
    /// The CopyMemory function copies a block of memory from one location to another. 
    /// </summary>
    /// <param name="destination">Pointer to the starting address of the copied block's destination.</param>
    /// <param name="source">Pointer to the starting address of the block of memory to copy.</param>
    /// <param name="length">Size of the block of memory to copy, in bytes.</param>
    [DllImport("Kernel32.dll", EntryPoint = "RtlMoveMemory")]
    private static extern void CopyMemory(IntPtr destination, IntPtr source, [MarshalAs(UnmanagedType.U4)] int length);

    /// <summary>
    /// Register the DMO in the registry.  Called by regasm.
    /// </summary>
    /// <param name="t">The <see cref="Type"/> to register.</param>
    [ComRegisterFunctionAttribute]
    private static void DoRegister(Type t)
    {
      // Tell what media types you are able to support.  This allows
      // the smart connect capability of DS to avoid loading your DMO
      // if it can't handle the stream type.

      // Note that you don't have to register any (but I recommend
      // you do).  Also, you don't have to provide a subtype (use
      // Guid.Empty).  You can negotiate this at run time in
      // InternalCheckInputType.
      DMOPartialMediatype[] inPin = new DMOPartialMediatype[2];
      inPin[0] = new DMOPartialMediatype();
      inPin[0].type = MediaType.Video;
      inPin[0].subtype = MediaSubType.RGB24;

      inPin[1] = new DMOPartialMediatype();
      inPin[1].type = MediaType.Video;
      inPin[1].subtype = MediaSubType.RGB32;

      DMOPartialMediatype[] outPin = new DMOPartialMediatype[2];
      outPin[0] = new DMOPartialMediatype();
      outPin[0].type = MediaType.Video;
      outPin[0].subtype = MediaSubType.RGB24;

      outPin[1] = new DMOPartialMediatype();
      outPin[1].type = MediaType.Video;
      outPin[1].subtype = MediaSubType.RGB32;

      int hr = DMOUtils.DMORegister(
          DMOName,
          typeof(DmoOverlay).GUID,
          DMOCat,
          DMORegisterFlags.None,
          inPin.Length,
          inPin,
          outPin.Length,
          outPin);
    }

    /// <summary>
    /// Removes the DMO from the registry.
    /// </summary>
    /// <param name="t">The <see cref="Type"/> to unregister</param>
    [ComUnregisterFunctionAttribute]
    private static void UnregisterFunction(Type t)
    {
      int hr = DMOUtils.DMOUnregister(typeof(DmoOverlay).GUID, DMOCat);
      DMOError.ThrowExceptionForHR(hr);
    }

    /// <summary>
    /// Release all info for the most recent input buffer
    /// </summary>
    private void ReleaseInputBuffs()
    {
      if (this.buffer != null)
      {
        Marshal.ReleaseComObject(this.buffer);
        this.buffer = null;
      }

      this.bufferPointer = IntPtr.Zero;
      this.bufferByteCount = 0;
      this.bufferFlags = 0;

      // I specifically DON'T release the TimeStamp so we can keep track of where we are
    }

    /// <summary>
    /// Perform the requested overlay.
    /// This method is the core of this class.
    /// It copies the input buffer to the output buffer and then overlays
    /// the gaze and mouse cursor at the given positions.
    /// </summary>
    /// <param name="outputDataPointer">Pointer to the output buffer</param>
    /// <param name="inputByteCount">Size of input buffer</param>
    /// <param name="inputDataPointer">Pointer to input buffer</param>
    /// <param name="bytesPerPixel">Bytes (not bits) per pixel</param>
    /// <param name="gazePosition">A <see cref="Point"/> with the gaze position in
    /// video pixel coordinates.</param>
    /// <param name="mousePosition">A <see cref="Point"/> with the mouse position in
    /// video pixel coordinates.</param>
    private void DoOverlay(
      IntPtr outputDataPointer,
      int inputByteCount,
      IntPtr inputDataPointer,
      int bytesPerPixel,
      Point gazePosition,
      Point mousePosition)
    {
      // Copy input to output
      CopyMemory(outputDataPointer, inputDataPointer, this.streamStride * this.streamHeight);

      this.DoCursorOverlay(
        outputDataPointer,
        inputDataPointer,
        bytesPerPixel,
        mousePosition,
        this.mouseCursorSize,
        this.mouseCursorData.Stride,
        this.mouseCursorArgbValues,
        false);

      // Copy output to input
      CopyMemory(inputDataPointer, outputDataPointer, this.streamStride * this.streamHeight);

      this.DoCursorOverlay(
        outputDataPointer,
        inputDataPointer,
        bytesPerPixel,
        gazePosition,
        this.gazeCursorSize,
        this.gazeCursorData.Stride,
        this.gazeCursorArgbValues,
        true);
    }

    /// <summary>
    /// Perform the requested overlay for the given cursor.
    /// </summary>
    /// <param name="outputDataPointer">Pointer to the output buffer</param>
    /// <param name="inputDataPointer">Pointer to input buffer</param>
    /// <param name="bytesPerPixel">Bytes (not bits) per pixel</param>
    /// <param name="cursorPosition">A <see cref="Point"/> with the cursor position in
    /// video pixel coordinates.</param>
    /// <param name="cursorSize">A <see cref="Size"/> with the size of the cursor.</param>
    /// <param name="cursorStride">The cursors bitmap stride.</param>
    /// <param name="cursorData">The byte array with the cursors data.</param>
    /// <param name="center">True if cursor should be centered at cursor position.</param>
    private unsafe void DoCursorOverlay(
      IntPtr outputDataPointer,
      IntPtr inputDataPointer,
      int bytesPerPixel,
      Point cursorPosition,
      Size cursorSize,
      int cursorStride,
      byte[] cursorData,
      bool center)
    {
      byte* p;
      long r, g, b;

      // Swap coordinates
      int gazeY = this.streamHeight - cursorPosition.Y;
      int gazeX = cursorPosition.X;
      int gazeCursorByteCounter = 0;

      // Truncate cursor to movie bounds
      if (gazeY < 0 || gazeY > this.streamHeight)
      {
        return;
      }

      if (gazeX < 0 || gazeX > this.streamWidth)
      {
        return;
      }

      if (center)
      {
        // Center gaze circle to gaze location
        gazeY += cursorSize.Height / 2;
        gazeX -= cursorSize.Width / 2;
      }

      // Calculate y-Position in video stream to start
      // applying the overlay
      int gazeStartY = gazeY - cursorSize.Height;
      int gazeStopY = gazeY;

      // Crop overlay to stream bounds by checking for values 
      // out of the stream in y-Direction
      if (gazeStartY < 0)
      {
        // Hop gazeStartY strides into the overlay because
        // we need to crop some of the gazecursors circle
        gazeCursorByteCounter = -gazeStartY * cursorStride;
        gazeStartY = 0;
      }

      if (gazeStopY > this.streamHeight)
      {
        gazeStopY = this.streamHeight;
      }

      // Set the start and end positions of the cursor overlay
      // in the cursors byte data
      int gazeStartX = 0;
      int gazeStopX = cursorSize.Width;

      // Crop overlay to stream bounds by checking for values 
      // out of the stream in x-Direction
      if (gazeX < 0)
      {
        gazeStartX = -gazeX;
      }

      if (gazeX + cursorSize.Width > this.streamWidth)
      {
        gazeStopX = Math.Min(this.streamWidth - gazeX, cursorSize.Width);
      }

      // Calculate the factor that is used to jump
      // into the correct row of the video stream
      int rowStart = gazeX < 0 ? 0 : gazeX;

      // For each column in the area of the gaze cursor
      // overlay rectangle
      for (int y = gazeStartY; y < gazeStopY; y++)
      {
        // Calculate the read/write positions
        // of the video stream at the area
        int src = y * this.streamStride;
        int dst = y * this.streamStride;

        // Jump to the correct column
        byte* sourcePointer = (byte*)(src + inputDataPointer.ToInt32());
        byte* destinationPointer = (byte*)(dst + outputDataPointer.ToInt32());

        // Move to correct x-Position in the column
        // were the gaze rectangle overlay is
        // placed
        if (bytesPerPixel == 4)
        {
          sourcePointer += 4 * rowStart;
          destinationPointer += 4 * rowStart;
        }
        else
        {
          sourcePointer += 3 * rowStart;
          destinationPointer += 3 * rowStart;
        }

        // If applicable move the byte counter
        // to the start of the truncated
        // gaze cursor
        gazeCursorByteCounter += gazeStartX * 4;

        // For each pixel in the column
        // starting at the correct position
        for (int x = gazeStartX; x < gazeStopX; x++)
        {
          // Get the source bytes
          p = &sourcePointer[0];

          // calculate the transparency of the gaze cursor
          float transparency = cursorData[gazeCursorByteCounter + RGBA] / 256f;

          // merge source and gaze cursor overlay using
          // the gaze cursors transparency
          r = (long)((byte)(transparency * cursorData[gazeCursorByteCounter + RGBR])
            + ((1 - transparency) * p[RGBR]));
          g = (long)((byte)(transparency * cursorData[gazeCursorByteCounter + RGBG])
            + ((1 - transparency) * p[RGBG]));
          b = (long)((byte)(transparency * cursorData[gazeCursorByteCounter + RGBB])
            + ((1 - transparency) * p[RGBB]));

          // Set the rgb values for the output
          destinationPointer[RGBR] = (byte)r;
          destinationPointer[RGBG] = (byte)g;
          destinationPointer[RGBB] = (byte)b;

          // Move the byte counter for the ARGB gaze cursor
          gazeCursorByteCounter += 4;

          // Move source and output pointer according
          // to bbp
          if (bytesPerPixel == 4)
          {
            destinationPointer[RGBA] = p[RGBA];

            sourcePointer += 4;
            destinationPointer += 4;
          }
          else
          {
            sourcePointer += 3;
            destinationPointer += 3;
          }
        }

        // If applicable move the byte counter
        // to the end of the truncated
        // gaze cursor
        gazeCursorByteCounter += (cursorSize.Width - gazeStopX) * 4;
      }
    }

    #endregion //PRIVATEMETHODS

    ///////////////////////////////////////////////////////////////////////////////
    // Small helping Methods                                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region HELPER
    #endregion //HELPER
  }
}
