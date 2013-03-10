// <copyright file="DmoMixer.cs" company="FU Berlin">
// ****************************************************************************
// While the underlying libraries are covered by LGPL, this sample is released
// as public domain.  It is distributed in the hope that it will be useful, but
// WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY
// or FITNESS FOR A PARTICULAR PURPOSE.
// *****************************************************************************/
// </copyright>

namespace DmoMixer
{
  using System;
  using System.Diagnostics;
  using System.Drawing;
  using System.Runtime.InteropServices;
  using DirectShowLib;
  using DirectShowLib.DMO;
  using DmoBase;

  /// <summary>
  /// This COM-Visible class is a DMO (digital media object)
  /// that can be inserted into a direct show filter graph.
  /// Its purpose is to mix two video streams into one output stream
  /// </summary>
  [ComVisible(true),
  Guid("7686580F-2C96-4dd5-B11B-FAACA183E376"),
  ClassInterface(ClassInterfaceType.None)]
  public class DmoMixer : IMediaObjectImpl
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
    private const int NumParams = 11;

    /// <summary>
    /// The name of this DMO to be saved into the registry
    /// </summary>
    private const string DMOName = "DmoMixer";

    /// <summary>
    /// The input pin count of this DMO.
    /// </summary>
    private const int InputPinCount = 2;

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
    /// The array of incoming <see cref="VideoStream"/>s.
    /// </summary>
    private VideoStream[] inputStreams;

    /// <summary>
    /// The output <see cref="VideoStream"/>
    /// </summary>
    private VideoStream outputStream;

    /// <summary>
    /// The <see cref="DMOOutputDataBufferFlags"/> of the current processed video buffer
    /// </summary>
    private DMOOutputDataBufferFlags bufferFlags;

    #endregion //FIELDS

    ///////////////////////////////////////////////////////////////////////////////
    // Construction and Initializing methods                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region CONSTRUCTION

    /// <summary>
    /// Initializes a new instance of the DmoMixer class.  
    /// The parameters to the base class
    /// describe the number of input and output streams, which
    /// DirectShow calls Pins, followed by the number of parameters
    /// this class supports (can be zero), and the timeformat of those
    /// parameters (should include ParamClass.TimeFormatFlags.Reference
    /// if NumParameters > 0).
    /// </summary>
    public DmoMixer()
      : base(InputPinCount, OutputPinCount, NumParams, TimeFormatFlags.Reference)
    {
      // Initialize the data members
      this.bufferFlags = 0;
      this.inputStreams = new VideoStream[InputPinCount];

      for (int i = 0; i < InputPinCount; i++)
      {
        this.inputStreams[i] = new VideoStream();
      }

      this.outputStream = new VideoStream();

      // Start describing the parameters this DMO supports.  Building this
      // structure (painful as it is) will allow the base class to automatically
      // support IMediaParamInfo & IMediaParams, which allow clients to find
      // out what parameters you support, and to set them.

      // See the MSDN
      // docs for MP_PARAMINFO for a description of the other parameters
      ParamInfo backgroundColor = new ParamInfo();
      backgroundColor.mopCaps = MPCaps.Jump;
      backgroundColor.mpdMinValue.vInt = int.MinValue;
      backgroundColor.mpdMaxValue.vInt = int.MaxValue;
      backgroundColor.mpdNeutralValue.vInt = 0;
      backgroundColor.mpType = MPType.INT;
      backgroundColor.szLabel = "BackgroundColor";
      backgroundColor.szUnitText = "Color";

      ParamDefine(0, backgroundColor, "BackgroundColor\0Color\0");

      for (int i = 0; i < InputPinCount; i++)
      {
        ParamInfo streamLeft = new ParamInfo();
        streamLeft.mopCaps = MPCaps.Jump;
        streamLeft.mpdMinValue.vFloat = 0;
        streamLeft.mpdMaxValue.vFloat = 1;
        streamLeft.mpdNeutralValue.vFloat = 0;
        streamLeft.mpType = MPType.FLOAT;
        streamLeft.szLabel = "Stream" + i.ToString() + "Left";
        streamLeft.szUnitText = "Position";

        ParamInfo streamTop = new ParamInfo();
        streamTop.mopCaps = MPCaps.Jump;
        streamTop.mpdMinValue.vFloat = 0;
        streamTop.mpdMaxValue.vFloat = 1;
        streamTop.mpdNeutralValue.vFloat = 0;
        streamTop.mpType = MPType.FLOAT;
        streamTop.szLabel = "Stream" + i.ToString() + "Top";
        streamTop.szUnitText = "Position";

        ParamInfo streamWidth = new ParamInfo();
        streamWidth.mopCaps = MPCaps.Jump;
        streamWidth.mpdMinValue.vFloat = 0;
        streamWidth.mpdMaxValue.vFloat = 1;
        streamWidth.mpdNeutralValue.vFloat = 1;
        streamWidth.mpType = MPType.FLOAT;
        streamWidth.szLabel = "Stream" + i.ToString() + "Width";
        streamWidth.szUnitText = "Position";

        ParamInfo streamHeight = new ParamInfo();
        streamHeight.mopCaps = MPCaps.Jump;
        streamHeight.mpdMinValue.vFloat = 0;
        streamHeight.mpdMaxValue.vFloat = 1;
        streamHeight.mpdNeutralValue.vFloat = 1;
        streamHeight.mpType = MPType.FLOAT;
        streamHeight.szLabel = "Stream" + i.ToString() + "Height";
        streamHeight.szUnitText = "Position";

        ParamInfo streamAlpha = new ParamInfo();
        streamAlpha.mopCaps = MPCaps.Jump;
        streamAlpha.mpdMinValue.vFloat = 0;
        streamAlpha.mpdMaxValue.vFloat = 1;
        streamAlpha.mpdNeutralValue.vFloat = 1;
        streamAlpha.mpType = MPType.FLOAT;
        streamAlpha.szLabel = "Stream" + i.ToString() + "Alpha";
        streamAlpha.szUnitText = "Alpha";

        ParamDefine((i * 5) + 1, streamLeft, "Stream" + i.ToString() + "Left\0Position\0");
        ParamDefine((i * 5) + 2, streamTop, "Stream" + i.ToString() + "Top\0Position\0");
        ParamDefine((i * 5) + 3, streamWidth, "Stream" + i.ToString() + "Width\0Position\0");
        ParamDefine((i * 5) + 4, streamHeight, "Stream" + i.ToString() + "Height\0Position\0");
        ParamDefine((i * 5) + 5, streamAlpha, "Stream" + i.ToString() + "Alpha\0Alpha\0");
      }
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
      for (int i = 0; i < InputPinCount; i++)
      {
        InternalDiscontinuity(i);

        // Release buffers
        this.ReleaseInputBuffs(i);
      }

      for (int i = 0; i < InputPinCount; i++)
      {
        this.inputStreams[i].BufferTimeStamp = 0;
      }

      return SOK;
    }

    /// <summary>
    /// Our chance to allocate any storage we may need
    /// </summary>
    /// <returns>Returns always S_OK</returns>
    protected override int InternalAllocateStreamingResources()
    {
      // Reinitialize variables
      for (int i = 0; i < InputPinCount; i++)
      {
        InternalDiscontinuity(i);
      }

      for (int i = 0; i < InputPinCount; i++)
      {
        AMMediaType mediaType = InputType(i);
        VideoInfoHeader videoInfoHeader = new VideoInfoHeader();
        Marshal.PtrToStructure(mediaType.formatPtr, videoInfoHeader);
        this.inputStreams[i].StreamWidth = videoInfoHeader.BmiHeader.Width;
        this.inputStreams[i].StreamHeight = videoInfoHeader.BmiHeader.Height;
        this.inputStreams[i].StreamBBP = videoInfoHeader.BmiHeader.BitCount / 8;
        this.inputStreams[i].StreamStride = videoInfoHeader.BmiHeader.Width * this.inputStreams[i].StreamBBP;
        this.inputStreams[i].BufferTimeStamp = 0;
      }

      AMMediaType outputMediaType = OutputType(0);
      VideoInfoHeader outputVideoInfoHeader = new VideoInfoHeader();
      Marshal.PtrToStructure(outputMediaType.formatPtr, outputVideoInfoHeader);
      this.outputStream.StreamWidth = outputVideoInfoHeader.BmiHeader.Width;
      this.outputStream.StreamHeight = outputVideoInfoHeader.BmiHeader.Height;
      this.outputStream.StreamBBP = outputVideoInfoHeader.BmiHeader.BitCount / 8;
      this.outputStream.StreamStride = outputVideoInfoHeader.BmiHeader.Width * this.outputStream.StreamBBP;
      this.outputStream.BufferTimeStamp = 0;

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
      Debug.Assert(this.inputStreams[inputStreamIndex].Buffer == null, "We already have a buffer, we shouldn't be getting another");

      IntPtr bufferPointer;
      int bufferByteCount;

      int hr = mediaBuffer.GetBufferAndLength(out bufferPointer, out bufferByteCount);
      this.inputStreams[inputStreamIndex].BufferPointer = bufferPointer;
      this.inputStreams[inputStreamIndex].BufferByteCount = bufferByteCount;

      if (hr >= 0)
      {
        // Ignore zero length buffers
        if (this.inputStreams[inputStreamIndex].BufferByteCount > 0)
        {
          this.inputStreams[inputStreamIndex].Buffer = mediaBuffer;

          // Cast the input flags to become output flags
          this.bufferFlags = (DMOOutputDataBufferFlags)flags;

          // If there is a time, store it
          if (0 == (flags & DMOInputDataBuffer.Time))
          {
            this.inputStreams[inputStreamIndex].BufferTimeStamp = MaxTime;
          }
          else
          {
            this.inputStreams[inputStreamIndex].BufferTimeStamp = timestamp;
          }

          // If there is a TimeLength, store it
          if (0 == (flags & DMOInputDataBuffer.TimeLength))
          {
            this.inputStreams[inputStreamIndex].BufferTimeLength = -1;
          }
          else
          {
            this.inputStreams[inputStreamIndex].BufferTimeLength = timelength;
          }

          hr = SOK;
        }
        else
        {
          this.ReleaseInputBuffs(inputStreamIndex);
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

      // Check for no input buffers to process
      for (int i = 0; i < InputPinCount; i++)
      {
        if (this.inputStreams[i].Buffer == null)
        {
          return SFALSE;
        }
      }

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
              long timeStamp = this.inputStreams[0].BufferTimeStamp;

              MPData backgroundColor = ParamCalcValueForTime(0, timeStamp);

              // Get the mode for the current timecode
              for (int i = 0; i < InputPinCount; i++)
              {
                MPData streamLeft = ParamCalcValueForTime((i * 5) + 1, timeStamp);
                MPData streamTop = ParamCalcValueForTime((i * 5) + 2, timeStamp);
                MPData streamWidth = ParamCalcValueForTime((i * 5) + 3, timeStamp);
                MPData streamHeight = ParamCalcValueForTime((i * 5) + 4, timeStamp);
                MPData streamAlpha = ParamCalcValueForTime((i * 5) + 5, timeStamp);
                RectangleF streamPosition = new RectangleF(
                  streamLeft.vFloat,
                  streamTop.vFloat,
                  streamWidth.vFloat,
                  streamHeight.vFloat);
                this.inputStreams[i].Alpha = streamAlpha.vFloat;
                this.inputStreams[i].Position = streamPosition;
              }

              // Process from input to output according to the mode
              this.DoOverlay(
                (IntPtr)(outputPointer.ToInt32() + currentByteCount),
                outputByteCount,
                this.inputStreams,
                backgroundColor.vInt);

              // Keep the flags & time info from the input
              outputBufferPointers[0].dwStatus = this.bufferFlags;
              outputBufferPointers[0].rtTimelength = this.inputStreams[0].BufferTimeLength;
              outputBufferPointers[0].rtTimestamp = timeStamp;

              // Release the buffer.  Since we are always processing one buffer at
              // a time, we always release on completion.  If our input might be
              // more than one buffer, we would only release the input when it had
              // be complete processed.
              this.ReleaseAllInputBuffers();

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
        outputBufferPointers[0].rtTimelength = this.inputStreams[0].BufferTimeLength;
        outputBufferPointers[0].rtTimestamp = this.inputStreams[0].BufferTimeStamp;

        // Release the buffer.  Since we are always processing one buffer at
        // a time, we always release on completion.  If our input might be
        // more than one buffer, we would only release the input when it had
        // be complete processed.
        this.ReleaseAllInputBuffers();
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
      return this.inputStreams[inputStreamIndex].Buffer == null ? SOK : SFALSE;
    }

    /// <summary>
    /// This method returns the current buffers time stamp.
    /// </summary>
    /// <returns>A <see cref="Int64"/> with the current buffers time stamp.</returns>
    protected override long InternalGetCurrentTime()
    {
      return this.inputStreams[0].BufferTimeStamp;
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
          typeof(DmoMixer).GUID,
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
      int hr = DMOUtils.DMOUnregister(typeof(DmoMixer).GUID, DMOCat);
      DMOError.ThrowExceptionForHR(hr);
    }

    /// <summary>
    /// Release all info for all input buffers
    /// </summary>
    private void ReleaseAllInputBuffers()
    {
      for (int i = 0; i < InputPinCount; i++)
      {
        this.ReleaseInputBuffs(i);
      }
    }

    /// <summary>
    /// Release all info for the given input buffer
    /// </summary>
    /// <param name="inputStreamIndex">Index of the input stream to be released.</param>
    private void ReleaseInputBuffs(int inputStreamIndex)
    {
      if (this.inputStreams[inputStreamIndex].Buffer != null)
      {
        Marshal.ReleaseComObject(this.inputStreams[inputStreamIndex].Buffer);
        this.inputStreams[inputStreamIndex].Buffer = null;
      }

      this.inputStreams[inputStreamIndex].BufferPointer = IntPtr.Zero;
      this.inputStreams[inputStreamIndex].BufferByteCount = 0;
      this.bufferFlags = 0;

      // I specifically DON'T release the TimeStamp so we can keep track of where we are
    }

    /// <summary>
    /// This method fills the output buffer with the video background and
    /// calls the overlay method for each stream.
    /// </summary>
    /// <param name="outputDataPointer">Pointer to the output data stream</param>
    /// <param name="outputByteCount">number of bytes in the output stream</param>
    /// <param name="videoStreams">The array of <see cref="VideoStream"/>s of the input pins.</param>
    /// <param name="backgroundColor">An AARRGGBB int with the background color.</param>
    private unsafe void DoOverlay(IntPtr outputDataPointer, int outputByteCount, VideoStream[] videoStreams, int backgroundColor)
    {
      // Fill area with background color
      Color background = Color.FromArgb(backgroundColor);
      byte* outputPointer = (byte*)outputDataPointer.ToInt32();
      for (int i = 0; i < Math.Abs(this.outputStream.StreamHeight * this.outputStream.StreamWidth); i++)
      {
        if (this.outputStream.StreamBBP == 4)
        {
          outputPointer[RGBA] = background.A;
          outputPointer[RGBR] = background.R;
          outputPointer[RGBG] = background.G;
          outputPointer[RGBB] = background.B;
          outputPointer += 4;
        }
        else
        {
          outputPointer[RGBR] = background.R;
          outputPointer[RGBG] = background.G;
          outputPointer[RGBB] = background.B;
          outputPointer += 3;
        }
      }

      // Overlay each incoming video stream
      for (int j = 0; j < this.inputStreams.Length; j++)
      {
        this.OverlayVideoStream(outputDataPointer, videoStreams[j]);
      }
    }

    /// <summary>
    /// Core method to overlay the given video stream with its
    /// properties on the output stream (given by the pointer).
    /// </summary>
    /// <param name="outputDataPointer">Pointer to the output data stream</param>
    /// <param name="videoStream">The <see cref="VideoStream"/> to overlay on the output.</param>
    private unsafe void OverlayVideoStream(IntPtr outputDataPointer, VideoStream videoStream)
    {
      RectangleF streamPosition = videoStream.Position;

      var streamWidth = (int)Math.Abs(streamPosition.Width * this.outputStream.StreamWidth);
      var streamHeight = (int)Math.Abs(streamPosition.Height * this.outputStream.StreamHeight);

      var resizeStream = (streamWidth != Math.Abs(videoStream.StreamWidth) || streamHeight != Math.Abs(videoStream.StreamHeight));

      if (videoStream.FlipY)
      {
        var top = 1 - streamPosition.Height - streamPosition.Top;
        streamPosition.Location = new PointF(streamPosition.Left, top);
      }

      var streamLeft = (int)Math.Abs(streamPosition.Left * this.outputStream.StreamWidth);
      var streamTop = (int)Math.Abs(streamPosition.Top * this.outputStream.StreamHeight);

      var resizedStream = videoStream;

      if (resizeStream)
      {
        resizedStream = this.ResizeBicubic(videoStream, new Size(streamWidth, streamHeight));
      }

      byte* p;
      long r, g, b;

      // For each column in the area of the gaze cursor
      // overlay rectangle
      for (int y = 0; y < Math.Abs(resizedStream.StreamHeight); y++)
      {
        // Calculate the read/write positions
        // of the video stream at the area
        int src = 0;
        if (videoStream.FlipY)
        {
          src = y * resizedStream.StreamStride;
        }
        else
        {
          src = (resizedStream.StreamHeight - y - 1) * resizedStream.StreamStride;
        }

        int dst = (streamTop + y) * this.outputStream.StreamStride;

        // Jump to the correct column
        byte* sourcePointer = (byte*)(src + resizedStream.BufferPointer.ToInt32());
        byte* destinationPointer = (byte*)(dst + outputDataPointer.ToInt32());

        // Move to correct x-Position in the column
        // were the video overlay is placed
        destinationPointer += resizedStream.StreamBBP * streamLeft;

        // For each pixel in the column
        // starting at the correct position
        for (int x = 0; x < resizedStream.StreamWidth; x++)
        {
          // Get the ouput bytes
          p = &destinationPointer[0];

          // calculate the transparency of the gaze cursor
          float transparency = resizedStream.Alpha;

          // merge source and gaze cursor overlay using
          // the gaze cursors transparency
          r = (long)((byte)(transparency * sourcePointer[RGBR])
            + ((1 - transparency) * p[RGBR]));
          g = (long)((byte)(transparency * sourcePointer[RGBG])
            + ((1 - transparency) * p[RGBG]));
          b = (long)((byte)(transparency * sourcePointer[RGBB])
            + ((1 - transparency) * p[RGBB]));

          // Set the rgb values for the output
          destinationPointer[RGBR] = (byte)r;
          destinationPointer[RGBG] = (byte)g;
          destinationPointer[RGBB] = (byte)b;

          // Move source and output pointer according
          // to bbp
          sourcePointer += resizedStream.StreamBBP;
          destinationPointer += resizedStream.StreamBBP;
        }
      }

      if (resizeStream)
      {
        // Free resources of resized stream
        Marshal.FreeHGlobal(resizedStream.BufferPointer);
      }
    }

    /// <summary>
    /// Resize the incoming videostream to the new stream size.
    /// </summary>
    /// <param name="sourceStream">Source video stream.</param>
    /// <param name="newStreamSize">New stream size.</param>
    /// <returns>The resized and newly allocated <see cref="VideoStream"/>.</returns>
    /// <remarks>The stream pointer has to be released after use with a call
    /// to ReleaseHGlobal(IntPtr).</remarks>
    private unsafe VideoStream ResizeBicubic(VideoStream sourceStream, Size newStreamSize)
    {
      // get image sizes
      int width = Math.Abs(sourceStream.StreamWidth);
      int height = Math.Abs(sourceStream.StreamHeight);
      int newWidth = newStreamSize.Width;
      int newHeight = newStreamSize.Height;

      VideoStream resizedStream = new VideoStream();
      resizedStream.StreamBBP = sourceStream.StreamBBP;
      resizedStream.StreamHeight = newHeight;
      resizedStream.StreamWidth = newWidth;
      resizedStream.StreamStride = newWidth * sourceStream.StreamBBP;
      resizedStream.Position = sourceStream.Position;
      resizedStream.Alpha = sourceStream.Alpha;
      resizedStream.Buffer = null;
      resizedStream.BufferTimeLength = sourceStream.BufferTimeLength;
      resizedStream.BufferTimeStamp = sourceStream.BufferTimeLength;

      // allocate memory for the image
      resizedStream.BufferPointer = Marshal.AllocHGlobal(resizedStream.StreamStride * newHeight);

      int pixelSize = sourceStream.StreamBBP;
      int srcStride = sourceStream.StreamStride;
      int dstOffset = resizedStream.StreamStride - (pixelSize * newWidth);
      double factorX = (double)width / newWidth;
      double factorY = (double)height / newHeight;

      // do the job
      byte* src = (byte*)sourceStream.BufferPointer;
      byte* dst = (byte*)resizedStream.BufferPointer;

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

      // RGB
      for (int y = 0; y < newHeight; y++)
      {
        // Y coordinates
        oy = (double)((y * factorY) - 0.5f);
        oy1 = (int)oy;
        dy = oy - (double)oy1;

        for (int x = 0; x < newWidth; x++, dst += pixelSize)
        {
          // X coordinates
          ox = (double)((x * factorX) - 0.5f);
          ox1 = (int)ox;
          dx = ox - (double)ox1;

          // initial pixel value
          r = g = b = 0;

          for (int n = -1; n < 3; n++)
          {
            // get Y cooefficient
            k1 = this.BiCubicKernel(dy - (double)n);

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
              k2 = k1 * this.BiCubicKernel((double)m - dx);

              ox2 = ox1 + m;
              if (ox2 < 0)
              {
                ox2 = 0;
              }

              if (ox2 > xmax)
              {
                ox2 = xmax;
              }

              // get pixel of original image
              p = src + (oy2 * srcStride) + (ox2 * pixelSize);

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

      return resizedStream;
    }

    #endregion //PRIVATEMETHODS

    ///////////////////////////////////////////////////////////////////////////////
    // Small helping Methods                                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region HELPER

    /// <summary>
    /// Bicubic kernel. From AForge.
    /// </summary>
    /// <param name="x">X value of the bicubic kernel.</param>
    /// <returns>Bicubic cooefficient.</returns>
    private double BiCubicKernel(double x)
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
