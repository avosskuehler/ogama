// <copyright file="IMediaObjectImpl.cs" company="FU Berlin">
// ****************************************************************************
// While the underlying libraries are covered by LGPL, this sample is released
// as public domain.  It is distributed in the hope that it will be useful, but
// WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY
// or FITNESS FOR A PARTICULAR PURPOSE.
// *****************************************************************************/
// </copyright>

namespace DmoBase
{
  using System;
  using System.Diagnostics;
  using System.Runtime.InteropServices;

  using DirectShowLib;
  using DirectShowLib.DMO;

  /// <summary>
  /// This abstract class can be used to implement a DMO in .NET.
  /// </summary>
  /// <remarks>
  /// <para>Before attempting to use this class, read the MSDN docs on DMOs!  In 
  /// particular read about IMediaObject, IMediaParamInfo, IMediaParams, 
  /// and the DMO Wrapper Filter (if you are using DirectShow graphs).</para>
  /// <para>When you read the MSDN docs about creating a DMO, they refer to a template that
  /// you can use to make things easier.  That template served as the inspiration for this 
  /// class.  To create a DMO, you can just create a class that implements this abstract class, 
  /// write code for the abstract methods, and you should be good to go.</para>
  /// <para>Here is a more detailed description of the steps you need to take.  Note that you can
  /// look at the sample code for examples of these steps.</para>
  /// <para>1) Other than ripping out the rather lame logging, you shouldn't need to change
  /// any code in IMediaObjectImpl.cs.  It is the initial entry point for all the
  /// IMediaObject interfaces.  It performs parameter checking, makes sure the call
  /// is appropriate for the current state, etc.  As needed it will make calls to the
  /// abstract and virtual methods of the class.</para>
  /// <para>2) Create a class which implements the abstract IMediaObjectImpl class:</para>
  /// <code>
  ///     [ComVisible(true), Guid("7EF28FD7-E88F-45bb-9CDD-8A62956F2D75"),
  ///     ClassInterface(ClassInterfaceType.None)]
  ///     public class DmoFlip : IMediaObjectImpl
  /// </code>
  /// <para>3) Generate your own guid so the samples won't interfere with your code:
  /// If you are running Dev Studio, go to Tools/Create Guid, choose "Registry 
  /// Format", click "Copy", then paste into your code.</para>
  /// <para>4) Create the constructor for your class.  It must not take any parameters:</para>
  /// <code>
  ///    public DmoFlip() : base(InputPinCount, OutputPinCount, ParamCount, TimeFormatFlags.Reference)
  /// </code>
  /// <para>If you are planning to use this DMO with the DirectShow DMO Wrapper Filter, note 
  /// that (up to and including DX v9.0) InputPinCount must be 1, and OutputPinCount must 
  /// be > 0.  The ParamCount is the number of parameters your DMO supports, and can be zero.  
  /// In general, you should use TimeFormatFlags.Reference for the last paramter.</para>
  /// <para>5) Register the parameters your DMO supports using <see cref="IMediaObjectImpl.ParamDefine"/>.  
  /// This must be done in the constructor (unless you have no parameters).  Doing this allows you to support 
  /// IMediaParamInfo and IMediaParams.  You will also need to use <see cref="IMediaObjectImpl.ParamCalcValueForTime"/> 
  /// to find out what parameter value you should use at any given point during the stream.  
  /// See the docs for these two methods for details.</para>
  /// <para>6) Create the COM register/unregister methods:</para>
  /// <code>
  ///     [ComRegisterFunctionAttribute]
  ///     static private void DoRegister(Type t)
  ///     [ComUnregisterFunctionAttribute]
  ///     static private void UnregisterFunction(Type t)
  /// </code>
  /// <para>These tell the OS about your DMO.  If you are distributing your code, you 
  /// will need to make sure they get called during installation (read about RegAsm 
  /// in the .NET docs).  At a minimum, you will need to call DMORegister to register
  /// your DMO.  See the sample for how this is done.</para>
  /// <para><b>WARNING:</b> If you use the "Register for COM Interop" compiler switch, the
  /// compiler will attempt to register DirectShowLib.dll as well as your DMO.
  /// Since DirectShowLib has no registration to perform, this generates an error.
  /// That is why the sample uses pre/post build events to perform the registration.  You
  /// may need to adjust this command for your particular installation.</para>
  /// <para>7) Do everything else.  There are 7 abstract methods for which you must 
  /// write code.  These methods are listed in the IMediaObjectImpl Methods page in 
  /// the <b>Protected Instance Methods</b> section. These methods will contain the information 
  /// specific to your DMO, and describe what type of data you are willing to process, and 
  /// perform the actual processing.  Note that since the abstract class has verified the 
  /// parameters, you do not need to re-check them in your implementation.  See the 
  /// descriptions for each method and the sample for details about what each of these 
  /// methods must do.</para>
  /// <para>You may also need to override some of the 11 virtual methods if their default
  /// implementation doesn't match your specific needs.  See the documentation for each of 
  /// these specific methods for details.</para>
  /// <hr></hr>
  /// <para>If you aren't already knowledgeable about COM and writing multi-threaded apps, 
  /// this is probably a good time to do a little research.  You may have multiple 
  /// instances of your DMO running in the same process, in multiple processes, 
  /// called on different threads, etc.</para>
  /// <para>As a simple example of the things you should be thinking of, the logging in 
  /// (debug builds of) IMediaObjectImpl.cs opens the file as non-sharable.
  /// However, if two applications try to instantiate your DMO, the second will fail, 
  /// solely due to not being able to open the log file.  Probably not the desired 
  /// behavior (told you the logging was lame).</para>
  /// </remarks>
  public abstract class IMediaObjectImpl : IMediaObject, IMediaParamInfo, IMediaParams
  {
    ///////////////////////////////////////////////////////////////////////////////
    // Defining Constants                                                        //
    ///////////////////////////////////////////////////////////////////////////////
    #region CONSTANTS

    /// <summary>
    /// Used in IMediaParams to specify that an envelope change should
    /// be applied to all parameters.
    /// </summary>
    protected const int ALLPARAMS = -1;

    /// <summary>
    /// COM return code indicating success
    /// </summary>
    protected const int SOK = 0;

    /// <summary>
    /// COM return code indicating partial success
    /// </summary>
    protected const int SFALSE = 1;

    /// <summary>
    /// COM return code indicating method not supported
    /// </summary>
    protected const int ENOTIMPL = unchecked((int)0x80004001);

    /// <summary>
    /// COM return code indicating invalid pointer provided
    /// </summary>
    protected const int EPOINTER = unchecked((int)0x80004003);

    /// <summary>
    /// COM return code indicating invalid argument specified
    /// </summary>
    protected const int EINVALIDARG = unchecked((int)0x80070057);

    /// <summary>
    /// COM return code indicating a method called at an unexpected time
    /// </summary>
    protected const int EUNEXPECTED = unchecked((int)0x8000FFFF);

    #endregion //CONSTANTS

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Variables, Enumerations, Events                                  //
    ///////////////////////////////////////////////////////////////////////////////
    #region FIELDS

    /// <summary>
    /// Keeps a count of the number of instances of this class have
    /// been instantiated.  Only used by logging.
    /// </summary>
    private static int instanceCount = 0;

    /// <summary>
    /// Status info about the input pins
    /// </summary>
    private PinDef[] inputInfo;

    /// <summary>
    /// Status info about the output pins
    /// </summary>
    private PinDef[] outputInfo;

    /// <summary>
    /// Have the types been set for all the input/output pins
    /// </summary>
    private bool typesSet;

    /// <summary>
    /// All buffers are empty
    /// </summary>
    private bool flushed;

    /// <summary>
    /// Has AllocateStreamingResources been successfully been called
    /// </summary>
    private bool resourcesAllocated;

    /// <summary>
    /// Count of the input pins
    /// </summary>
    private int numInputs;

    /// <summary>
    /// Count of the output pins
    /// </summary>
    private int numOutputs;

    /// <summary>
    /// Struct to support IMediaParamInfo and IMediaParams
    /// </summary>
    private ParamClass paramsClass;

    #endregion //FIELDS

    ///////////////////////////////////////////////////////////////////////////////
    // Construction and Initializing methods                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region CONSTRUCTION

    /// <summary>
    /// Initializes a new instance of the IMediaObjectImpl class.
    /// </summary>
    /// <param name="inputs">Number of input streams</param>
    /// <param name="outputs">Number of output streams</param>
    /// <param name="numberOfParameters">Number of parameters</param>
    /// <param name="timeFormats">What time formats the parameters support</param>
    /// <remarks>
    /// This constructor will be called from the constructor of the class that implements 
    /// IMediaObjectImpl. See <see cref="IMediaObjectImpl"/> for a step by step description 
    /// of the process.
    /// </remarks>
    protected IMediaObjectImpl(int inputs, int outputs, int numberOfParameters, TimeFormatFlags timeFormats)
    {
      instanceCount++;

      this.numInputs = inputs;
      this.numOutputs = outputs;
      this.typesSet = false;
      this.flushed = true;
      this.resourcesAllocated = false;
      this.inputInfo = new PinDef[this.numInputs];
      this.outputInfo = new PinDef[this.numOutputs];

      // Protect ourselves from incorrectly written children
      this.paramsClass = new ParamClass(numberOfParameters, timeFormats);
    }

    /// <summary>
    /// Finalizes an instance of the IMediaObjectImpl class
    /// </summary>
    ~IMediaObjectImpl()
    {
      int currentType;

      for (currentType = 0; currentType < this.numInputs; currentType++)
      {
        if (this.InputTypeSet(currentType))
        {
          DsUtils.FreeAMMediaType(this.inputInfo[currentType].CurrentMediaType);
        }
      }

      for (currentType = 0; currentType < this.numOutputs; currentType++)
      {
        if (this.OutputTypeSet(currentType))
        {
          DsUtils.FreeAMMediaType(this.outputInfo[currentType].CurrentMediaType);
        }
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

    #region IMediaObject methods

    /// <summary>
    /// COM entry point for IMediaObject.GetStreamCount
    /// </summary>
    /// <param name="pulNumberOfInputStreams">Out. Number of input streams.</param>
    /// <param name="pulNumberOfOutputStreams">Out. Number of output streams.</param>
    /// <returns>A HRESULT value.</returns>
    /// <remarks>
    /// There should be no need to modify or override this method.  It will call the
    /// abstract and virtual methods to perform its work.
    /// </remarks>
    public int GetStreamCount(out int pulNumberOfInputStreams, out int pulNumberOfOutputStreams)
    {
      int hr;

      try
      {
        // Avoid multi-threaded access issues
        lock (this)
        {
          // Return the number of input/output streams
          pulNumberOfInputStreams = this.numInputs;
          pulNumberOfOutputStreams = this.numOutputs;

          hr = SOK;
        }
      }
      catch (Exception e)
      {
        // Generic handling of all exceptions.  While .NET will turn exceptions into
        // HRESULTS "automatically", I prefer to have some place I can set a breakpoint.
        hr = this.CatFail(e);

        // Have to have these to make the compiler happy.  Should be safe since
        // GetStreamCount is doc'ed not to accept NULLs.
        pulNumberOfInputStreams = 0;
        pulNumberOfOutputStreams = 0;
      }

      return hr;
    }

    /// <summary>
    /// COM entry point for IMediaObject.GetInputStreamInfo
    /// </summary>
    /// <param name="streamIndex">The index of the stream.</param>
    /// <param name="pflags">A <see cref="DMOInputStreamInfo"/>
    /// with the stream flags.</param>
    /// <returns>A HRESULT value.</returns>
    /// <remarks>
    /// There should be no need to modify or override this method.  It will call the
    /// abstract and virtual methods to perform its work.
    /// </remarks>
    public int GetInputStreamInfo(int streamIndex, out DMOInputStreamInfo pflags)
    {
      int hr;

      try
      {
        // Avoid multi-threaded access issues
        lock (this)
        {
          pflags = 0;

          // Validate the stream number
          if (streamIndex < this.numInputs && streamIndex >= 0)
          {
            // Call the internal function to get the value
            hr = this.InternalGetInputStreamInfo(streamIndex, out pflags);

            // Validate the value returned by the internal function
            Debug.Assert(
              0 == (pflags & ~(DMOInputStreamInfo.WholeSamples |
                DMOInputStreamInfo.SingleSamplePerBuffer |
                DMOInputStreamInfo.FixedSampleSize |
                DMOInputStreamInfo.HoldsBuffers)),
                "Wrong value");
          }
          else
          {
            hr = DMOResults.E_InvalidStreamIndex;
          }
        }
      }
      catch (Exception e)
      {
        // Generic handling of all exceptions.  While .NET will turn exceptions into
        // HRESULTS "automatically", I prefer to have some place I can set a breakpoint.
        hr = this.CatFail(e);

        // Have to have this to make the compiler happy.
        pflags = 0;
      }

      return hr;
    }

    /// <summary>
    /// COM entry point for IMediaObject.GetOutputStreamInfo
    /// </summary>
    /// <param name="streamIndex">The index of the stream.</param>
    /// <param name="pflags">A <see cref="DMOInputStreamInfo"/>
    /// with the stream flags.</param>
    /// <returns>A HRESULT value.</returns>
    /// <remarks>
    /// There should be no need to modify or override this method.  It will call the
    /// abstract and virtual methods to perform its work.
    /// </remarks>
    public int GetOutputStreamInfo(int streamIndex, out DMOOutputStreamInfo pflags)
    {
      int hr;

      try
      {
        // Avoid multi-threaded access issues
        lock (this)
        {
          pflags = 0;

          // Validate the stream number
          if (streamIndex < this.numOutputs && streamIndex >= 0)
          {
            // Call the internal function to get the value
            hr = this.InternalGetOutputStreamInfo(streamIndex, out pflags);

            // Validate the value returned by the internal function
            Debug.Assert(
              0 == (pflags & ~(DMOOutputStreamInfo.WholeSamples |
                DMOOutputStreamInfo.SingleSamplePerBuffer |
                DMOOutputStreamInfo.FixedSampleSize |
                DMOOutputStreamInfo.Discardable |
                DMOOutputStreamInfo.Optional)),
                "Wring value");
          }
          else
          {
            hr = DMOResults.E_InvalidStreamIndex;
          }
        }
      }
      catch (Exception e)
      {
        // Generic handling of all exceptions.  While .NET will turn exceptions into
        // HRESULTS "automatically", I prefer to have some place I can set a breakpoint.
        hr = this.CatFail(e);

        // Have to have this to make the compiler happy.
        pflags = 0;
      }

      return hr;
    }

    /// <summary>
    /// COM entry point for IMediaObject.GetInputType
    /// </summary>
    /// <param name="streamIndex">The index of the stream.</param>
    /// <param name="typeIndex">The index of the type</param>
    /// <param name="pmt">A <see cref="AMMediaType"/> value.</param>
    /// <returns>A HRESULT value.</returns>
    /// <remarks>
    /// There should be no need to modify or override this method.  It will call the
    /// abstract and virtual methods to perform its work.
    /// </remarks>
    public int GetInputType(int streamIndex, int typeIndex, AMMediaType pmt)
    {
      int hr;

      try
      {
        // Avoid multi-threaded access issues
        lock (this)
        {
          // Validate the stream number
          if (streamIndex < this.numInputs && streamIndex >= 0)
          {
            AMMediaType pmt2;

            // Call the internal method to get the MediaType
            hr = this.InternalGetInputType(streamIndex, typeIndex, out pmt2);

            // If InternalGetInputType returned a value, and if the caller
            // provided a place for it, copy it over
            if (hr >= 0 && pmt != null)
            {
              DMOUtils.MoCopyMediaType(pmt, pmt2);
            }
          }
          else
          {
            hr = DMOResults.E_InvalidStreamIndex;
          }
        }
      }
      catch (Exception e)
      {
        // Generic handling of all exceptions.  While .NET will turn exceptions into
        // HRESULTS "automatically", I prefer to have some place I can set a breakpoint.
        hr = this.CatFail(e);
      }

      return hr;
    }

    /// <summary>
    /// COM entry point for IMediaObject.GetOutputType
    /// </summary>
    /// <param name="streamIndex">The index of the stream.</param>
    /// <param name="typeIndex">The index of the type</param>
    /// <param name="pmt">A <see cref="AMMediaType"/> value.</param>
    /// <returns>A HRESULT value.</returns>
    /// <remarks>
    /// There should be no need to modify or override this method.  It will call the
    /// abstract and virtual methods to perform its work.
    /// </remarks>
    public int GetOutputType(int streamIndex, int typeIndex, AMMediaType pmt)
    {
      int hr;

      try
      {
        // Avoid multi-threaded access issues
        lock (this)
        {
          // Validate the stream number
          if (streamIndex < this.numOutputs && streamIndex >= 0)
          {
            AMMediaType pmt2;

            hr = this.InternalGetOutputType(streamIndex, typeIndex, out pmt2);

            // If InternalGetInputType returned a value, and if the caller
            // provided a place for it, copy it over
            if (hr >= 0)
            {
              if (pmt != null)
              {
                DMOUtils.MoCopyMediaType(pmt, pmt2);
              }
            }
          }
          else
          {
            hr = DMOResults.E_InvalidStreamIndex;
          }
        }
      }
      catch (Exception e)
      {
        // Generic handling of all exceptions.  While .NET will turn exceptions into
        // HRESULTS "automatically", I prefer to have some place I can set a breakpoint.
        hr = this.CatFail(e);
      }

      return hr;
    }

    /// <summary>
    /// COM entry point for IMediaObject.GetInputCurrentType
    /// </summary>
    /// <param name="streamIndex">The index of the stream.</param>
    /// <param name="pmt">A <see cref="AMMediaType"/> value.</param>
    /// <returns>A HRESULT value.</returns>
    /// <remarks>
    /// There should be no need to modify or override this method.  It will call the
    /// abstract and virtual methods to perform its work.
    /// </remarks>
    public int GetInputCurrentType(int streamIndex, AMMediaType pmt)
    {
      int hr;

      try
      {
        // Avoid multi-threaded access issues
        lock (this)
        {
          pmt = null;

          // Validate the stream number
          if (streamIndex < this.numInputs && streamIndex >= 0)
          {
            // If there *is* a current type
            if (this.InputTypeSet(streamIndex))
            {
              pmt = MoCloneMediaType(this.inputInfo[streamIndex].CurrentMediaType);
              hr = SOK;
            }
            else
            {
              hr = DMOResults.E_TypeNotSet;
            }
          }
          else
          {
            hr = DMOResults.E_InvalidStreamIndex;
          }
        }
      }
      catch (Exception e)
      {
        // Generic handling of all exceptions.  While .NET will turn exceptions into
        // HRESULTS "automatically", I prefer to have some place I can set a breakpoint.
        hr = this.CatFail(e);

        // Have to have this to make the compiler happy.
        pmt = null;
      }

      return hr;
    }

    /// <summary>
    /// COM entry point for IMediaObject.GetOutputCurrentType
    /// </summary>
    /// <param name="streamIndex">The index of the stream.</param>
    /// <param name="pmt">A <see cref="AMMediaType"/> value.</param>
    /// <returns>A HRESULT value.</returns>
    /// <remarks>
    /// There should be no need to modify or override this method.  It will call the
    /// abstract and virtual methods to perform its work.
    /// </remarks>
    public int GetOutputCurrentType(int streamIndex, AMMediaType pmt)
    {
      int hr;

      try
      {
        // Avoid multi-threaded access issues
        lock (this)
        {
          pmt = null;

          // Validate the stream number
          if (streamIndex < this.numOutputs && streamIndex >= 0)
          {
            // If there *is* a current type
            if (this.OutputTypeSet(streamIndex))
            {
              pmt = MoCloneMediaType(this.outputInfo[streamIndex].CurrentMediaType);
              hr = SOK;
            }
            else
            {
              hr = DMOResults.E_TypeNotSet;
            }
          }
          else
          {
            hr = DMOResults.E_InvalidStreamIndex;
          }
        }
      }
      catch (Exception e)
      {
        // Generic handling of all exceptions.  While .NET will turn exceptions into
        // HRESULTS "automatically", I prefer to have some place I can set a breakpoint.
        hr = this.CatFail(e);

        // Have to have this to make the compiler happy.
        pmt = null;
      }

      return hr;
    }

    /// <summary>
    /// COM entry point for IMediaObject.GetInputSizeInfo
    /// </summary>
    /// <param name="streamIndex">Zero-based index of an input stream on the DMO.</param>
    /// <param name="pulSize">Out. Pointer to a variable that receives the minimum size of an input buffer for this stream, in bytes.</param>
    /// <param name="pcbMaxLookahead">Out. Pointer to a variable
    /// that receives the maximum amount of data that the DMO 
    /// will hold for lookahead, in bytes. If the DMO does not 
    /// perform lookahead on the stream, the value is zero.</param>
    /// <param name="pulAlignment">Out. Pointer to a variable 
    /// that receives the required buffer alignment, in bytes. 
    /// If the input stream has no alignment requirement, the value is 1.</param>
    /// <returns>A HRESULT value.</returns>
    /// <remarks>
    /// There should be no need to modify or override this method.  It will call the
    /// abstract and virtual methods to perform its work.
    /// </remarks>
    public int GetInputSizeInfo(int streamIndex, out int pulSize, out int pcbMaxLookahead, out int pulAlignment)
    {
      int hr;

      try
      {
        // Avoid multi-threaded access issues
        lock (this)
        {
          pulSize = 0;
          pcbMaxLookahead = 0;
          pulAlignment = 0;

          // Validate the stream number
          if (streamIndex < this.numInputs && streamIndex >= 0)
          {
            // If there is a type set (otherwise, how can we report a size for it?)
            if (this.InputTypeSet(streamIndex))
            {
              hr = this.InternalGetInputSizeInfo(streamIndex, out pulSize, out pcbMaxLookahead, out pulAlignment);
            }
            else
            {
              hr = DMOResults.E_TypeNotSet;
            }
          }
          else
          {
            hr = DMOResults.E_InvalidStreamIndex;
          }
        }
      }
      catch (Exception e)
      {
        // Generic handling of all exceptions.  While .NET will turn exceptions into
        // HRESULTS "automatically", I prefer to have some place I can set a breakpoint.
        hr = this.CatFail(e);

        // Have to have this to make the compiler happy.
        pulSize = 0;
        pcbMaxLookahead = 0;
        pulAlignment = 0;
      }

      return hr;
    }

    /// <summary>
    /// COM entry point for IMediaObject.GetOutputSizeInfo
    /// </summary>
    /// <param name="streamIndex">Zero-based index of an input stream on the DMO.</param>
    /// <param name="pulSize">Out. Pointer to a variable that receives the minimum size of an input buffer for this stream, in bytes.</param>
    /// <param name="pulAlignment">Out. Pointer to a variable 
    /// that receives the required buffer alignment, in bytes. 
    /// If the input stream has no alignment requirement, the value is 1.</param>
    /// <returns>A HRESULT value.</returns>
    /// <remarks>
    /// There should be no need to modify or override this method.  It will call the
    /// abstract and virtual methods to perform its work.
    /// </remarks>
    public int GetOutputSizeInfo(int streamIndex, out int pulSize, out int pulAlignment)
    {
      int hr;

      try
      {
        // Avoid multi-threaded access issues
        lock (this)
        {
          pulSize = 0;
          pulAlignment = 0;

          // Validate the stream number
          if (streamIndex < this.numOutputs && streamIndex >= 0)
          {
            // If there is a type set (otherwise, how can we report a size for it?)
            if (this.typesSet && this.OutputTypeSet(streamIndex))
            {
              hr = this.InternalGetOutputSizeInfo(streamIndex, out pulSize, out pulAlignment);
            }
            else
            {
              hr = DMOResults.E_TypeNotSet;
            }
          }
          else
          {
            hr = DMOResults.E_InvalidStreamIndex;
          }
        }
      }
      catch (Exception e)
      {
        // Generic handling of all exceptions.  While .NET will turn exceptions into
        // HRESULTS "automatically", I prefer to have some place I can set a breakpoint.
        hr = this.CatFail(e);

        // Have to have this to make the compiler happy.
        pulSize = 0;
        pulAlignment = 0;
      }

      return hr;
    }

    /// <summary>
    /// COM entry point for IMediaObject.SetInputType
    /// </summary>
    /// <param name="streamIndex">Zero-based index of an input stream on the DMO.</param>
    /// <param name="pmt">Pointer to a <see cref="AMMediaType"/> structure that 
    /// specifies the media type.</param>
    /// <param name="flags">Bitwise combination of zero or more flags 
    /// from the DMO_SET_TYPE_FLAGS enumeration.</param>
    /// <returns>A HRESULT value.</returns>
    /// <remarks>
    /// There should be no need to modify or override this method.  It will call the
    /// abstract and virtual methods to perform its work.
    /// </remarks>
    public int SetInputType(int streamIndex, AMMediaType pmt, DMOSetType flags)
    {
      int hr;

      try
      {
        // Avoid multi-threaded access issues
        lock (this)
        {
          // Validate the stream number
          if (streamIndex >= this.numInputs || streamIndex < 0)
          {
            return DMOResults.E_InvalidStreamIndex;
          }

          // Validate the flags
          if ((flags & ~(DMOSetType.Clear | DMOSetType.TestOnly)) > 0)
          {
            return EINVALIDARG;
          }

          // If they requested to clear the current type
          if ((flags & DMOSetType.Clear) > 0)
          {
            DsUtils.FreeAMMediaType(this.inputInfo[streamIndex].CurrentMediaType);
            this.inputInfo[streamIndex].TypeSet = false;

            // Re-check to see if all the non-optional streams still have types
            if (!this.CheckAllTypesSet())
            {
              // We aren't in a runnable state anymore, flush things
              this.Flush();
              this.FreeStreamingResources();
            }

            hr = SOK;
          }
          else
          {
            if (null != pmt)
            {
              // Check to see if the type is already set
              if (!this.InputTypeSet(streamIndex))
              {
                // Check to see if we support the requested format
                hr = this.InternalCheckInputType(streamIndex, pmt);
                if (hr >= 0)
                {
                  hr = SOK;

                  // If we weren't just being tested, actually set the type
                  if ((flags & DMOSetType.TestOnly) == 0)
                  {
                    // Free any previous mediatype
                    if (this.InputTypeSet(streamIndex))
                    {
                      DsUtils.FreeAMMediaType(this.inputInfo[streamIndex].CurrentMediaType);
                    }

                    this.inputInfo[streamIndex].CurrentMediaType = MoCloneMediaType(pmt);
                    this.inputInfo[streamIndex].TypeSet = true;

                    // Re-check to see if all the non-optional streams still have types
                    this.CheckAllTypesSet();
                  }
                }
              }
              else
              {
                // Type is set, so reject any type that's not identical
                if (TypesMatch(pmt, this.InputType(streamIndex)))
                {
                  hr = SOK;
                }
                else
                {
                  hr = DMOResults.E_InvalidType;
                }
              }
            }
            else
            {
              hr = EPOINTER;
            }
          }
        }
      }
      catch (Exception e)
      {
        // Generic handling of all exceptions.  While .NET will turn exceptions into
        // HRESULTS "automatically", I prefer to have some place I can set a breakpoint.
        hr = this.CatFail(e);
      }

      return hr;
    }

    /// <summary>
    /// COM entry point for IMediaObject.SetOutputType
    /// </summary>
    /// <param name="streamIndex">Zero-based index of an input stream on the DMO.</param>
    /// <param name="pmt">Pointer to a <see cref="AMMediaType"/> structure that 
    /// specifies the media type.</param>
    /// <param name="flags">Bitwise combination of zero or more flags 
    /// from the DMO_SET_TYPE_FLAGS enumeration.</param>
    /// <returns>A HRESULT value.</returns>
    /// <remarks>
    /// There should be no need to modify or override this method.  It will call the
    /// abstract and virtual methods to perform its work.
    /// </remarks>
    public int SetOutputType(int streamIndex, AMMediaType pmt, DMOSetType flags)
    {
      int hr = SOK;

      try
      {
        // Avoid multi-threaded access issues
        lock (this)
        {
          // Validate the stream number
          if (streamIndex >= this.numOutputs || streamIndex < 0)
          {
            return DMOResults.E_InvalidStreamIndex;
          }

          // Validate the flags
          if ((flags & ~(DMOSetType.Clear | DMOSetType.TestOnly)) > 0)
          {
            return EINVALIDARG;
          }

          // If they requested to clear the current type
          if ((flags & DMOSetType.Clear) > 0)
          {
            DsUtils.FreeAMMediaType(this.outputInfo[streamIndex].CurrentMediaType);
            this.outputInfo[streamIndex].TypeSet = false;

            // Re-check to see if all the non-optional streams still have types
            if (!this.CheckAllTypesSet())
            {
              // We aren't in a runnable state anymore, flush things
              this.Flush();
              this.FreeStreamingResources();
            }

            hr = SOK;
          }
          else
          {
            if (null != pmt)
            {
              // Check if the type is already set
              if (!this.OutputTypeSet(streamIndex))
              {
                // Check to see if we support the requested format
                hr = this.InternalCheckOutputType(streamIndex, pmt);
                if (hr >= 0)
                {
                  hr = SOK;

                  // If we weren't just being tested, actually set the type
                  if ((flags & DMOSetType.TestOnly) == 0)
                  {
                    // Free any previous mediatype
                    if (this.OutputTypeSet(streamIndex))
                    {
                      DsUtils.FreeAMMediaType(this.outputInfo[streamIndex].CurrentMediaType);
                    }

                    this.outputInfo[streamIndex].CurrentMediaType = MoCloneMediaType(pmt);
                    this.outputInfo[streamIndex].TypeSet = true;

                    // Re-check to see if all the non-optional streams still have types
                    this.CheckAllTypesSet();
                  }
                }
              }
              else
              {
                // Type is set, so reject any type that's not identical
                if (!TypesMatch(pmt, this.OutputType(streamIndex)))
                {
                  hr = DMOResults.E_InvalidType;
                }
                else
                {
                  hr = SOK;
                }
              }
            }
            else
            {
              hr = EPOINTER;
            }
          }
        }
      }
      catch (Exception e)
      {
        // Generic handling of all exceptions.  While .NET will turn exceptions into
        // HRESULTS "automatically", I prefer to have some place I can set a breakpoint.
        hr = this.CatFail(e);
      }

      return hr;
    }

    /// <summary>
    /// COM entry point for IMediaObject.GetInputStatus
    /// </summary>
    /// <param name="streamIndex">Zero-based index of an input stream on the DMO.</param>
    /// <param name="pdwStatus">Out. Pointer to a variable that 
    /// receives either zero or DMO_INPUT_STATUSF_ACCEPT_DATA.</param>
    /// <returns>A HRESULT value.</returns>
    /// <remarks>
    /// There should be no need to modify or override this method.  It will call the
    /// abstract and virtual methods to perform its work.
    /// </remarks>
    public int GetInputStatus(int streamIndex, out DMOInputStatusFlags pdwStatus)
    {
      int hr;

      try
      {
        // Avoid multi-threaded access issues
        lock (this)
        {
          pdwStatus = DMOInputStatusFlags.None;

          // Validate the stream number
          if (streamIndex < this.numInputs && streamIndex >= 0)
          {
            if (this.typesSet)
            {
              hr = this.InternalAcceptingInput(streamIndex);
              if (hr == SOK)
              {
                pdwStatus |= DMOInputStatusFlags.AcceptData;
              }
              else if (hr == SFALSE)
              {
                // Not an error, we just aren't accepting input right now
                hr = SOK;
              }
            }
            else
            {
              hr = DMOResults.E_TypeNotSet;
            }
          }
          else
          {
            hr = DMOResults.E_InvalidStreamIndex;
          }
        }
      }
      catch (Exception e)
      {
        // Generic handling of all exceptions.  While .NET will turn exceptions into
        // HRESULTS "automatically", I prefer to have some place I can set a breakpoint.
        hr = this.CatFail(e);

        // Have to have this to make the compiler happy.
        pdwStatus = DMOInputStatusFlags.None;
      }

      return hr;
    }

    /// <summary>
    /// COM entry point for IMediaObject.GetInputMaxLatency
    /// </summary>
    /// <param name="streamIndex">Zero-based index of an input stream on the DMO.</param>
    /// <param name="platency">Out. Pointer to a variable that receives the maximum latency</param>
    /// <returns>A HRESULT value.</returns>
    /// <remarks>
    /// There should be no need to modify or override this method.  It will call the
    /// abstract and virtual methods to perform its work.
    /// </remarks>
    public int GetInputMaxLatency(int streamIndex, out long platency)
    {
      int hr;

      try
      {
        // Avoid multi-threaded access issues
        lock (this)
        {
          platency = 0;

          // Validate the stream number
          if (streamIndex < this.numInputs && streamIndex >= 0)
          {
            hr = this.InternalGetInputMaxLatency(streamIndex, out platency);
          }
          else
          {
            hr = DMOResults.E_InvalidStreamIndex;
          }
        }
      }
      catch (Exception e)
      {
        // Generic handling of all exceptions.  While .NET will turn exceptions into
        // HRESULTS "automatically", I prefer to have some place I can set a breakpoint.
        hr = this.CatFail(e);

        // Have to have this to make the compiler happy.
        platency = 0;
      }

      return hr;
    }

    /// <summary>
    /// COM entry point for IMediaObject.SetInputMaxLatency
    /// </summary>
    /// <param name="streamIndex">Zero-based index of an input stream on the DMO.</param>
    /// <param name="latency">Pointer to a variable that receives the maximum latency</param>
    /// <returns>A HRESULT value.</returns>
    /// <remarks>
    /// There should be no need to modify or override this method.  It will call the
    /// abstract and virtual methods to perform its work.
    /// </remarks>
    public int SetInputMaxLatency(int streamIndex, long latency)
    {
      int hr;

      try
      {
        // Avoid multi-threaded access issues
        lock (this)
        {
          // Validate the stream number
          if (streamIndex < this.numInputs && streamIndex >= 0)
          {
            hr = this.InternalSetInputMaxLatency(streamIndex, latency);
          }
          else
          {
            hr = DMOResults.E_InvalidStreamIndex;
          }
        }
      }
      catch (Exception e)
      {
        // Generic handling of all exceptions.  While .NET will turn exceptions into
        // HRESULTS "automatically", I prefer to have some place I can set a breakpoint.
        hr = this.CatFail(e);
      }

      return hr;
    }

    /// <summary>
    /// COM entry point for IMediaObject.Discontinuity
    /// </summary>
    /// <param name="streamIndex">Zero-based index of an input stream on the DMO.</param>
    /// <returns>A HRESULT value.</returns>
    /// <remarks>
    /// There should be no need to modify or override this method.  It will call the
    /// abstract and virtual methods to perform its work.
    /// </remarks>
    public int Discontinuity(int streamIndex)
    {
      int hr;

      try
      {
        // Avoid multi-threaded access issues
        lock (this)
        {
          // Validate the stream number
          if (streamIndex < this.numInputs && streamIndex >= 0)
          {
            if (this.typesSet)
            {
              // If we are accepting input, tell the internal
              // functions about the Discontinuity.
              hr = this.InternalAcceptingInput(streamIndex);
              if (SOK == hr)
              {
                hr = this.InternalDiscontinuity(streamIndex);
              }
              else
              {
                // No one cares, just return an error
                hr = DMOResults.E_NotAccepting;
              }
            }
            else
            {
              hr = DMOResults.E_TypeNotSet;
            }
          }
          else
          {
            hr = DMOResults.E_InvalidStreamIndex;
          }
        }
      }
      catch (Exception e)
      {
        // Generic handling of all exceptions.  While .NET will turn exceptions into
        // HRESULTS "automatically", I prefer to have some place I can set a breakpoint.
        hr = this.CatFail(e);
      }

      return hr;
    }

    /// <summary>
    /// COM entry point for IMediaObject.Flush
    /// </summary>
    /// <returns>A HRESULT value.</returns>
    /// <remarks>
    /// There should be no need to modify or override this method.  It will call the
    /// abstract and virtual methods to perform its work.
    /// </remarks>
    public int Flush()
    {
      int hr;

      try
      {
        // Avoid multi-threaded access issues
        lock (this)
        {
          // Only call the internal flush if there is something to flush
          if (this.typesSet && !this.flushed)
          {
            hr = this.InternalFlush();
          }
          else
          {
            hr = SOK;
          }

          this.flushed = true;
        }
      }
      catch (Exception e)
      {
        // Generic handling of all exceptions.  While .NET will turn exceptions into
        // HRESULTS "automatically", I prefer to have some place I can set a breakpoint.
        hr = this.CatFail(e);
      }

      return hr;
    }

    /// <summary>
    /// COM entry point for IMediaObject.AllocateStreamingResources
    /// </summary>
    /// <returns>A HRESULT value.</returns>
    /// <remarks>
    /// There should be no need to modify or override this method.  It will call the
    /// abstract and virtual methods to perform its work.
    /// </remarks>
    public int AllocateStreamingResources()
    {
      int hr;

      try
      {
        // Avoid multi-threaded access issues
        lock (this)
        {
          // Don't allocate resources until all streams have a type
          if (this.typesSet)
          {
            // Only need to call it once
            if (!this.resourcesAllocated)
            {
              hr = this.InternalAllocateStreamingResources();
              if (hr >= 0)
              {
                this.resourcesAllocated = true;
              }
            }
            else
            {
              hr = SOK;
            }
          }
          else
          {
            hr = DMOResults.E_TypeNotSet;
          }
        }
      }
      catch (Exception e)
      {
        // Generic handling of all exceptions.  While .NET will turn exceptions into
        // HRESULTS "automatically", I prefer to have some place I can set a breakpoint.
        hr = this.CatFail(e);
      }

      return hr;
    }

    /// <summary>
    /// COM entry point for IMediaObject.FreeStreamingResources
    /// </summary>
    /// <returns>A HRESULT value.</returns>
    /// <remarks>
    /// There should be no need to modify or override this method.  It will call the
    /// abstract and virtual methods to perform its work.
    /// </remarks>
    public int FreeStreamingResources()
    {
      int hr;

      try
      {
        // Avoid multi-threaded access issues
        lock (this)
        {
          if (this.resourcesAllocated)
          {
            this.resourcesAllocated = false;
            this.InternalFlush();
            hr = this.InternalFreeStreamingResources();
            GC.Collect();
          }
          else
          {
            hr = SOK;
          }
        }
      }
      catch (Exception e)
      {
        // Generic handling of all exceptions.  While .NET will turn exceptions into
        // HRESULTS "automatically", I prefer to have some place I can set a breakpoint.
        hr = this.CatFail(e);
      }

      return hr;
    }

    /// <summary>
    /// COM entry point for IMediaObject.ProcessInput
    /// </summary>
    /// <param name="streamIndex">Zero-based index of an input stream on the DMO.</param>
    /// <param name="bufferVal">Pointer to the buffer's IMediaBuffer interface.</param>
    /// <param name="flags">Bitwise combination of zero or 
    /// more flags from the DMO_INPUT_DATA_BUFFER_FLAGS enumeration.</param>
    /// <param name="timestamp">Time stamp that 
    /// specifies the start time of the data in the buffer. 
    /// If the buffer has a valid time stamp, set the DMO_INPUT_DATA_BUFFERF_TIME 
    /// flag in the flags parameter. Otherwise, the DMO ignores this value.</param>
    /// <param name="timelength">Reference time specifying the duration 
    /// of the data in the buffer. If this value is valid, set 
    /// the DMO_INPUT_DATA_BUFFERF_TIMELENGTH flag in the flags
    /// parameter. Otherwise, the DMO ignores this value.</param>
    /// <returns>A HRESULT value.</returns>
    /// <remarks>
    /// There should be no need to modify or override this method.  It will call the
    /// abstract and virtual methods to perform its work.
    /// </remarks>
    public int ProcessInput(
        int streamIndex,
        IMediaBuffer bufferVal,
        DMOInputDataBuffer flags,
        long timestamp,
        long timelength)
    {
      int hr;

      try
      {
        // Avoid multi-threaded access issues
        lock (this)
        {
          if (bufferVal != null)
          {
            // Validate the stream number
            if (streamIndex < this.numInputs && streamIndex >= 0)
            {
              // Validate flags
              if ((flags & ~(DMOInputDataBuffer.SyncPoint |
                  DMOInputDataBuffer.Time |
                  DMOInputDataBuffer.TimeLength)) == 0)
              {
                // Make sure all streams have media types set and resources are allocated
                hr = this.AllocateStreamingResources();
                if (hr >= 0)
                {
                  // If we aren't accepting input, forget it
                  if (this.InternalAcceptingInput(streamIndex) == SOK)
                  {
                    this.flushed = false;

                    hr = this.InternalProcessInput(
                        streamIndex,
                        bufferVal,
                        flags,
                        timestamp,
                        timelength);
                  }
                  else
                  {
                    hr = DMOResults.E_NotAccepting;
                  }
                }
              }
              else
              {
                hr = EINVALIDARG;
              }
            }
            else
            {
              hr = DMOResults.E_InvalidStreamIndex;
            }
          }
          else
          {
            hr = EPOINTER;
          }
        }
      }
      catch (Exception e)
      {
        // Generic handling of all exceptions.  While .NET will turn exceptions into
        // HRESULTS "automatically", I prefer to have some place I can set a breakpoint.
        hr = this.CatFail(e);
      }

      return hr;
    }

    /// <summary>
    /// COM entry point for IMediaObject.ProcessOutput
    /// </summary>
    /// <param name="flags">Bitwise combination of zero
    /// or more flags from the DMO_PROCESS_OUTPUT_FLAGS enumeration.</param>
    /// <param name="outputBufferCount">Number of output buffers.</param>
    /// <param name="outputBuffers">Pointer to an array of DMO_OUTPUT_DATA_BUFFER 
    /// structures containing the output buffers. 
    /// Specify the size of the array in the cOutputBufferCount parameter.</param>
    /// <param name="pdwStatus">Out. Pointer to a variable that
    /// receives a reserved value (zero). The application should ignore this value.</param>
    /// <returns>A HRESULT value.</returns>
    /// <remarks>
    /// There should be no need to modify or override this method.  It will call the
    /// abstract and virtual methods to perform its work.
    /// </remarks>
    public int ProcessOutput(
        DMOProcessOutput flags,
        int outputBufferCount,
        DMOOutputDataBuffer[] outputBuffers,
        out int pdwStatus)
    {
      int hr;

      try
      {
        // Avoid multi-threaded access issues
        lock (this)
        {
          pdwStatus = 0;

          // The number of buffers needs to exactly equal the number of streams
          if (outputBufferCount == this.numOutputs && ((flags & ~DMOProcessOutput.DiscardWhenNoBuffer) == 0))
          {
            // If there are output streams, outputBuffers can't be null
            if (this.numOutputs > 0 || outputBuffers != null)
            {
              hr = this.AllocateStreamingResources();
              if (hr >= 0)
              {
                // Init the status flags to zero
                int dw;
                for (dw = 0; dw < this.numOutputs; dw++)
                {
                  outputBuffers[dw].dwStatus = DMOOutputDataBufferFlags.None;
                }

                // Fill the buffers
                hr = this.InternalProcessOutput(
                    flags,
                    outputBufferCount,
                    outputBuffers,
                    out pdwStatus);

                // remember the DMO's incomplete status
                for (dw = 0; dw < this.numOutputs; dw++)
                {
                  if ((outputBuffers[dw].dwStatus & DMOOutputDataBufferFlags.InComplete) > 0)
                  {
                    this.outputInfo[dw].Incomplete = true;
                  }
                  else
                  {
                    this.outputInfo[dw].Incomplete = false;
                  }
                }
              }
            }
            else
            {
              hr = EPOINTER;
            }
          }
          else
          {
            hr = EINVALIDARG;
          }
        }
      }
      catch (Exception e)
      {
        // Generic handling of all exceptions.  While .NET will turn exceptions into
        // HRESULTS "automatically", I prefer to have some place I can set a breakpoint.
        hr = this.CatFail(e);

        // Have to have this to make the compiler happy.
        pdwStatus = 0;
      }

      return hr;
    }

    /// <summary>
    /// COM entry point for IMediaObject.Lock
    /// </summary>
    /// <param name="lockValue">Value that specifies whether to acquire 
    /// or release the lock. If the value is non-zero, 
    /// a lock is acquired. If the value is zero, the lock is released.</param>
    /// <returns>A HRESULT value.</returns>
    /// <remarks>
    /// There should be no need to modify or override this method.  It will call the
    /// abstract and virtual methods to perform its work.
    /// </remarks>
    public int Lock(bool lockValue)
    {
      // Lock is doc'ed to limit access to multiple threads at the same time.  We
      // are doing that with the lock(this) in each entry point, so this is redundant.
      return SOK;
    }

    #endregion

    #region IMediaParamInfo Members

    /// <summary>
    /// COM entry point for IMediaParamInfo.GetParamCount
    /// </summary>
    /// <param name="pdwParams">[out] Pointer to a variable that receives the number of parameters.</param>
    /// <returns>Returns an HRESULT value.</returns>
    /// <remarks>
    /// There should be no need to modify or override this method.  See 
    /// <see cref="IMediaObjectImpl.ParamDefine"/> and <see cref="IMediaObjectImpl.ParamCalcValueForTime"/>
    /// for details about how this works.
    /// </remarks>
    public int GetParamCount(out int pdwParams)
    {
      int hr;

      try
      {
        lock (this)
        {
          pdwParams = this.paramsClass.Parms.Length;
          hr = SOK;
        }
      }
      catch (Exception e)
      {
        // Generic handling of all exceptions.  While .NET will turn exceptions into
        // HRESULTS "automatically", I prefer to have some place I can set a breakpoint.
        hr = this.CatFail(e);

        // Have to have this to make the compiler happy.
        pdwParams = 0;
      }

      return hr;
    }

    /// <summary>
    /// COM entry point for IMediaParamInfo.GetSupportedTimeFormat
    /// </summary>
    /// <param name="formatIndex">[in] Index of the time format to retrieve.</param>
    /// <param name="pguidTimeFormat">[out] Pointer to a variable that 
    /// receives a time format GUID</param>
    /// <returns>Returns an HRESULT value.</returns>
    /// <remarks>
    /// There should be no need to modify or override this method.  See 
    /// <see cref="IMediaObjectImpl.ParamDefine"/> and <see cref="IMediaObjectImpl.ParamCalcValueForTime"/>
    /// for details about how this works.
    /// </remarks>
    public int GetSupportedTimeFormat(int formatIndex, out Guid pguidTimeFormat)
    {
      int hr;

      try
      {
        lock (this)
        {
          // If the index is in range, return the specified timeformat
          if (formatIndex < this.paramsClass.TimeFormats.Length)
          {
            pguidTimeFormat = this.paramsClass.TimeFormats[formatIndex];
            hr = SOK;
          }
          else
          {
            pguidTimeFormat = Guid.Empty;
            hr = EINVALIDARG;
          }
        }
      }
      catch (Exception e)
      {
        // Generic handling of all exceptions.  While .NET will turn exceptions into
        // HRESULTS "automatically", I prefer to have some place I can set a breakpoint.
        hr = this.CatFail(e);

        // Have to have this to make the compiler happy.
        pguidTimeFormat = Guid.Empty;
      }

      return hr;
    }

    /// <summary>
    /// COM entry point for IMediaParamInfo.GetParamInfo
    /// </summary>
    /// <param name="paramIndex">[in] Zero-based index of the parameter.</param>
    /// <param name="info">[out] Pointer to an <see cref="ParamInfo"/> structure
    /// that is filled with the parameter information.</param>
    /// <returns>Returns an HRESULT value.</returns>
    /// <remarks>
    /// There should be no need to modify or override this method.  See 
    /// <see cref="IMediaObjectImpl.ParamDefine"/> and <see cref="IMediaObjectImpl.ParamCalcValueForTime"/>
    /// for details about how this works.
    /// </remarks>
    public int GetParamInfo(int paramIndex, out ParamInfo info)
    {
      int hr;

      try
      {
        lock (this)
        {
          // If the parameter is in range, return the ParamInfo
          if (paramIndex < this.paramsClass.Parms.Length)
          {
            info = this.paramsClass.Parms[paramIndex];
            hr = SOK;
          }
          else
          {
            info = new ParamInfo();
            hr = EINVALIDARG;
          }
        }
      }
      catch (Exception e)
      {
        // Generic handling of all exceptions.  While .NET will turn exceptions into
        // HRESULTS "automatically", I prefer to have some place I can set a breakpoint.
        hr = this.CatFail(e);

        // Have to have this to make the compiler happy.
        info = new ParamInfo();
      }

      return hr;
    }

    /// <summary>
    /// COM entry point for IMediaParamInfo.GetCurrentTimeFormat
    /// </summary>
    /// <param name="pguidTimeFormat">[out] Pointer to a variable that receives a time format GUID.</param>
    /// <param name="timeData">[out] Pointer to a variable that receives 
    /// an MP_TIMEDATA value specifying the unit of measure for the new format.</param>
    /// <returns>Returns an HRESULT value.</returns>
    /// <remarks>
    /// There should be no need to modify or override this method.  See 
    /// <see cref="IMediaObjectImpl.ParamDefine"/> and <see cref="IMediaObjectImpl.ParamCalcValueForTime"/>
    /// for details about how this works.
    /// </remarks>
    public int GetCurrentTimeFormat(out Guid pguidTimeFormat, out int timeData)
    {
      int hr;

      try
      {
        lock (this)
        {
          // Return the current time format from the array
          pguidTimeFormat = this.paramsClass.TimeFormats[this.paramsClass.CurrentTimeFormat];
          timeData = this.paramsClass.TimeData;

          hr = SOK;
        }
      }
      catch (Exception e)
      {
        // Generic handling of all exceptions.  While .NET will turn exceptions into
        // HRESULTS "automatically", I prefer to have some place I can set a breakpoint.
        hr = this.CatFail(e);

        // Have to have this to make the compiler happy.
        pguidTimeFormat = Guid.Empty;
        timeData = 0;
      }

      return hr;
    }

    /// <summary>
    /// COM entry point for IMediaParamInfo.GetParamText
    /// </summary>
    /// <param name="paramIndex">[in] Zero-based index of the parameter.</param>
    /// <param name="ppwchText">[out] Address of a variable that 
    /// receives a pointer to a series of Unicode™ strings.</param>
    /// <returns>Returns an HRESULT value.</returns>
    /// <remarks>
    /// There should be no need to modify or override this method.  See 
    /// <see cref="IMediaObjectImpl.ParamDefine"/> and <see cref="IMediaObjectImpl.ParamCalcValueForTime"/>
    /// for details about how this works.
    /// </remarks>
    public int GetParamText(int paramIndex, out IntPtr ppwchText)
    {
      int hr;

      try
      {
        lock (this)
        {
          // If the parameter is in range, return the text
          if (paramIndex < this.paramsClass.ParamText.Length)
          {
            ppwchText = Marshal.StringToCoTaskMemUni(this.paramsClass.ParamText[paramIndex]);
            hr = SOK;
          }
          else
          {
            ppwchText = IntPtr.Zero;
            hr = EINVALIDARG;
          }
        }
      }
      catch (Exception e)
      {
        // Generic handling of all exceptions.  While .NET will turn exceptions into
        // HRESULTS "automatically", I prefer to have some place I can set a breakpoint.
        hr = this.CatFail(e);

        // Have to have this to make the compiler happy.
        ppwchText = IntPtr.Zero;
      }

      return hr;
    }

    /// <summary>
    /// COM entry point for IMediaParamInfo.GetNumTimeFormats
    /// </summary>
    /// <param name="pdwNumTimeFormats">[out] Pointer to a variable that 
    /// receives the number of supported time formats.</param>
    /// <returns>Returns an HRESULT value.</returns>
    /// <remarks>
    /// There should be no need to modify or override this method.  See 
    /// <see cref="IMediaObjectImpl.ParamDefine"/> and <see cref="IMediaObjectImpl.ParamCalcValueForTime"/>
    /// for details about how this works.
    /// </remarks>
    public int GetNumTimeFormats(out int pdwNumTimeFormats)
    {
      int hr;

      try
      {
        lock (this)
        {
          pdwNumTimeFormats = this.paramsClass.TimeFormats.Length;
          hr = SOK;
        }
      }
      catch (Exception e)
      {
        // Generic handling of all exceptions.  While .NET will turn exceptions into
        // HRESULTS "automatically", I prefer to have some place I can set a breakpoint.
        hr = this.CatFail(e);

        // Have to have this to make the compiler happy.
        pdwNumTimeFormats = 0;
      }

      return hr;
    }

    #endregion

    #region IMediaParams Members

    /// <summary>
    /// COM entry point for IMediaParams.SetTimeFormat
    /// </summary>
    /// <param name="guidTimeFormat">[in] Time format GUID that specifies the time format.</param>
    /// <param name="timedata">[in] Value of type MP_TIMEDATA that 
    /// specifies the unit of measure for the new format.</param>
    /// <returns>Returns an HRESULT value.</returns>
    /// <remarks>
    /// There should be no need to modify or override this method.  See 
    /// <see cref="IMediaObjectImpl.ParamDefine"/> and <see cref="IMediaObjectImpl.ParamCalcValueForTime"/>
    /// for details about how this works.
    /// </remarks>
    public int SetTimeFormat(Guid guidTimeFormat, int timedata)
    {
      int hr = EINVALIDARG;

      try
      {
        lock (this)
        {
          // Scan the array of timeformats looking for a match
          for (int x = 0; x < this.paramsClass.TimeFormats.Length; x++)
          {
            if (guidTimeFormat == this.paramsClass.TimeFormats[x])
            {
              // Found one, grab the index & store the TimeData
              this.paramsClass.CurrentTimeFormat = x;
              this.paramsClass.TimeData = timedata;
              hr = SOK;
              break;
            }
          }
        }
      }
      catch (Exception e)
      {
        // Generic handling of all exceptions.  While .NET will turn exceptions into
        // HRESULTS "automatically", I prefer to have some place I can set a breakpoint.
        hr = this.CatFail(e);
      }

      return hr;
    }

    /// <summary>
    /// COM entry point for IMediaParams.SetParam
    /// </summary>
    /// <param name="paramIndex">[in] Zero-based index of the parameter, 
    /// or DWORD_ALLPARAMS to apply the value to every parameter.</param>
    /// <param name="value">[in] New value of the parameter.</param>
    /// <returns>Returns an HRESULT value.</returns>
    /// <remarks>
    /// There should be no need to modify or override this method.  See 
    /// <see cref="IMediaObjectImpl.ParamDefine"/> and <see cref="IMediaObjectImpl.ParamCalcValueForTime"/>
    /// for details about how this works.
    /// </remarks>
    public int SetParam(int paramIndex, MPData value)
    {
      int hr;

      try
      {
        lock (this)
        {
          // Check to see if the index is in range
          if (paramIndex < this.paramsClass.Parms.Length && paramIndex >= 0)
          {
            // Make an envelope that spans from current to end of stream
            MPEnvelopeSegment m = new MPEnvelopeSegment();

            m.flags = MPFlags.Standard;
            m.iCurve = MPCaps.Jump;
            m.rtStart = 0;
            m.rtEnd = long.MaxValue;
            m.valStart = value;
            m.valEnd = value;

            // Add the struct to the envelope
            hr = this.paramsClass.Envelopes[paramIndex].AddSegment(m);
          }
          else
          {
            hr = EINVALIDARG;
          }
        }
      }
      catch (Exception e)
      {
        // Generic handling of all exceptions.  While .NET will turn exceptions into
        // HRESULTS "automatically", I prefer to have some place I can set a breakpoint.
        hr = this.CatFail(e);
      }

      return hr;
    }

    /// <summary>
    /// COM entry point for IMediaParams.AddEnvelope
    /// </summary>
    /// <param name="paramIndex">[in] Zero-based index of the parameter, 
    /// or DWORD_ALLPARAMS to apply the value to every parameter.</param>
    /// <param name="segments">[in] Number of segments in the envelope.</param>
    /// <param name="envelopeSegments">[in] Pointer to an array 
    /// of <see cref="MPEnvelopeSegment"/> structures that define the 
    /// envelope segments. The size of the array is given in the cPoints parameter.</param>
    /// <returns>Returns an HRESULT value.</returns>
    /// <remarks>
    /// There should be no need to modify or override this method.  See 
    /// <see cref="IMediaObjectImpl.ParamDefine"/> and <see cref="IMediaObjectImpl.ParamCalcValueForTime"/>
    /// for details about how this works.
    /// </remarks>
    public int AddEnvelope(int paramIndex, int segments, MPEnvelopeSegment[] envelopeSegments)
    {
      int hr;

      try
      {
        lock (this)
        {
          hr = SOK;

          if (paramIndex == ALLPARAMS)
          {
            // Add all the envelopes to all the parameters
            for (int y = 0; y < this.paramsClass.Parms.Length && hr >= 0; y++)
            {
              for (int x = 0; x < segments && hr >= 0; x++)
              {
                // Add the struct to the envelope
                hr = this.paramsClass.Envelopes[y].AddSegment(envelopeSegments[x]);
              }
            }
          }
          else if (paramIndex < this.paramsClass.Parms.Length && paramIndex >= 0)
          {
            // Add all the envelopes to the specified parameter
            for (int x = 0; x < segments && hr >= 0; x++)
            {
              // Add the struct to the envelope
              hr = this.paramsClass.Envelopes[paramIndex].AddSegment(envelopeSegments[x]);
            }
          }
          else
          {
            hr = EINVALIDARG;
          }
        }
      }
      catch (Exception e)
      {
        // Generic handling of all exceptions.  While .NET will turn exceptions into
        // HRESULTS "automatically", I prefer to have some place I can set a breakpoint.
        hr = this.CatFail(e);
      }

      return hr;
    }

    /// <summary>
    /// COM entry point for IMediaParams.GetParam
    /// </summary>
    /// <param name="paramIndex">[in] Zero-based index of the parameter, 
    /// or DWORD_ALLPARAMS to apply the value to every parameter.</param>
    /// <param name="dataValue">[out] Pointer to a variable of type
    /// MPData that receives the parameter value.</param>
    /// <returns>Returns an HRESULT value.</returns>
    /// <remarks>
    /// There should be no need to modify or override this method.  See 
    /// <see cref="IMediaObjectImpl.ParamDefine"/> and <see cref="IMediaObjectImpl.ParamCalcValueForTime"/>
    /// for details about how this works.
    /// </remarks>
    public int GetParam(int paramIndex, out MPData dataValue)
    {
      int hr;

      try
      {
        lock (this)
        {
          // If the requested parameter is within range
          if (paramIndex < this.paramsClass.Parms.Length && paramIndex >= 0)
          {
            // Read the value
            dataValue = this.paramsClass.Envelopes[paramIndex].CalcValueForTime(this.InternalGetCurrentTime());
            hr = SOK;
          }
          else
          {
            dataValue = new MPData();
            hr = EINVALIDARG;
          }
        }
      }
      catch (Exception e)
      {
        // Generic handling of all exceptions.  While .NET will turn exceptions into
        // HRESULTS "automatically", I prefer to have some place I can set a breakpoint.
        hr = this.CatFail(e);

        // Have to have this to make the compiler happy.
        dataValue = new MPData();
      }

      return hr;
    }

    /// <summary>
    /// COM entry point for IMediaParams.FlushEnvelope
    /// </summary>
    /// <param name="paramIndex">[in] Zero-based index of the parameter,
    /// or DWORD_ALLPARAMS to flush envelope data from every parameter.</param>
    /// <param name="refTimeStart">[in] Start time of the envelope data to flush.</param>
    /// <param name="refTimeEnd">[in] End time of the envelope data to flush.</param>
    /// <returns>Returns an HRESULT value.</returns>
    /// <remarks>
    /// There should be no need to modify or override this method.  See 
    /// <see cref="IMediaObjectImpl.ParamDefine"/> and <see cref="IMediaObjectImpl.ParamCalcValueForTime"/>
    /// for details about how this works.
    /// </remarks>
    public int FlushEnvelope(int paramIndex, long refTimeStart, long refTimeEnd)
    {
      int hr;

      try
      {
        lock (this)
        {
          // If the time range makes sense
          if ((refTimeStart >= 0) && (refTimeEnd >= refTimeStart))
          {
            if (paramIndex == ALLPARAMS)
            {
              hr = SOK;

              // Apply the flush to all parameters
              for (int x = 0; x < this.paramsClass.Parms.Length && hr >= 0; x++)
              {
                MPEnvelopeSegment m = new MPEnvelopeSegment();

                m.flags = MPFlags.Standard;
                m.iCurve = MPCaps.Jump;
                m.rtStart = refTimeStart;
                m.rtEnd = refTimeEnd;
                m.valStart = this.paramsClass.Parms[x].mpdNeutralValue;
                m.valEnd = this.paramsClass.Parms[x].mpdNeutralValue;

                hr = this.paramsClass.Envelopes[paramIndex].AddSegment(m);
              }
            }
            else if (paramIndex < this.paramsClass.Parms.Length)
            {
              // Apply the flush to the specified parameter
              MPEnvelopeSegment m = new MPEnvelopeSegment();

              m.flags = MPFlags.Standard;
              m.iCurve = MPCaps.Jump;
              m.rtStart = refTimeStart;
              m.rtEnd = refTimeEnd;
              m.valStart = this.paramsClass.Parms[paramIndex].mpdNeutralValue;
              m.valEnd = this.paramsClass.Parms[paramIndex].mpdNeutralValue;

              hr = this.paramsClass.Envelopes[paramIndex].AddSegment(m);
            }
            else
            {
              hr = EINVALIDARG;
            }
          }
          else
          {
            hr = EINVALIDARG;
          }
        }
      }
      catch (Exception e)
      {
        // Generic handling of all exceptions.  While .NET will turn exceptions into
        // HRESULTS "automatically", I prefer to have some place I can set a breakpoint.
        hr = this.CatFail(e);
      }

      return hr;
    }

    #endregion

    #endregion //PUBLICMETHODS

    ///////////////////////////////////////////////////////////////////////////////
    // Inherited methods                                                         //
    ///////////////////////////////////////////////////////////////////////////////
    #region OVERRIDES

    /// <summary>
    /// Make a clone of a media type
    /// </summary>
    /// <param name="pmt1">The AMMediaType to clone</param>
    /// <returns>Returns the clone</returns>
    /// <remarks>
    /// Note that like all AMMediaTypes, the clone must be released
    /// with DsUtils.FreeAMMediaType when it is no longer needed.
    /// </remarks>
    protected static AMMediaType MoCloneMediaType(AMMediaType pmt1)
    {
      AMMediaType returnValue = new AMMediaType();
      DMOUtils.MoCopyMediaType(returnValue, pmt1);

      return returnValue;
    }

    /// <summary>
    /// Check to see if two Media Types are exactly the same
    /// </summary>
    /// <param name="pmt1">First Media type to compare</param>
    /// <param name="pmt2">Second Media type to compare</param>
    /// <returns>true if types are identical, else false</returns>
    protected static bool TypesMatch(AMMediaType pmt1, AMMediaType pmt2)
    {
      if (pmt1.majorType == pmt2.majorType &&
          pmt1.subType == pmt2.subType &&
          pmt1.sampleSize == pmt2.sampleSize &&
          pmt1.formatType == pmt2.formatType &&
          pmt1.formatSize == pmt2.formatSize &&
          pmt1.formatSize == CompareMemory(pmt1.formatPtr, pmt2.formatPtr, pmt1.formatSize))
      {
        return true;
      }
      else
      {
        return false;
      }
    }

    #region Abstract methods

    /// <summary>
    /// (Abstract) Determine whether the input stream supports a specific media type
    /// </summary>
    /// <param name="inputStreamIndex">Input stream number</param>
    /// <param name="pmt">The media type to check</param>
    /// <returns>S_OK if the specified stream supports the specified media type, 
    /// else DMOResults.E_InvalidType</returns>
    /// <remarks>
    /// This method is called by the abstract class.  The implementor should check the
    /// properties of the AMMediaType to ensure that if a sample of the specified type
    /// is sent, it will be able to process it.
    /// </remarks>
    protected abstract int InternalCheckInputType(int inputStreamIndex, AMMediaType pmt);

    /// <summary>
    /// (Abstract) Determine whether the output stream supports a specific media type
    /// </summary>
    /// <param name="outputStreamIndex">Output stream number</param>
    /// <param name="pmt">The media type to check</param>
    /// <returns>S_OK if the specified stream supports the specified media type, 
    /// else DMOResults.E_InvalidType</returns>
    /// <remarks>
    /// This method is called by the abstract class.  The implementor should check the
    /// properties of the AMMediaType to ensure that if requested, it can produce an
    /// output sample of the specified type.
    /// </remarks>
    protected abstract int InternalCheckOutputType(int outputStreamIndex, AMMediaType pmt);

    /// <summary>
    /// (Abstract) Determine the requirements for the output stream
    /// </summary>
    /// <param name="outputStreamIndex">Output stream number</param>
    /// <param name="pcbSize">The minimum size of an output buffer for this stream, in bytes</param>
    /// <param name="pcbAlignment">The required buffer alignment, in bytes. If the output stream 
    /// has no alignment requirement, the value is 1.</param>
    /// <returns>S_OK to indicate successful operation.</returns>
    /// <remarks>
    /// This method is called by the abstract class.  You should never return zero for the alignment.
    /// </remarks>
    protected abstract int InternalGetOutputSizeInfo(int outputStreamIndex, out int pcbSize, out int pcbAlignment);

    /// <summary>
    /// (Abstract) Called to flush all pending processing
    /// </summary>
    /// <returns>S_OK to indicate successful operation</returns>
    /// <remarks>
    /// This method is called by the abstract class.  In response, the implementor should discard
    /// any pending input buffers.
    /// </remarks>
    protected abstract int InternalFlush();

    /// <summary>
    /// (Abstract) Accept input buffers to be processed
    /// </summary>
    /// <param name="inputStreamIndex">Input stream number</param>
    /// <param name="bufferVal">Input buffer to process</param>
    /// <param name="flags">Processing flags</param>
    /// <param name="timestamp">Timestamp of sample(s)</param>
    /// <param name="timelength">Length of sample(s)</param>
    /// <returns>S_OK if the operation completes successfully, S_FALSE if there
    /// is no input to process.</returns>
    /// <remarks>
    /// This method is called by the abstract class.  It passes the actual data to be process to the 
    /// implementor.  Commonly, the implementor stores these values, waiting for the call to 
    /// InternalProcessOutput (which contains the buffers into which the results are to be stored), at
    /// which point they are released.
    /// </remarks>
    protected abstract int InternalProcessInput(int inputStreamIndex, IMediaBuffer bufferVal, DMOInputDataBuffer flags, long timestamp, long timelength);

    /// <summary>
    /// (Abstract) Process the input buffers from a previous call to InternalProcessInput into the provided output buffers
    /// </summary>
    /// <param name="flags">Flags controlling the operation</param>
    /// <param name="outputBufferCount">The number of buffers provided (one per output stream)</param>
    /// <param name="outputBuffers">The output buffer into which the data is processed</param>
    /// <param name="pdwStatus">Zero output</param>
    /// <returns>S_FALSE if there is no output, S_OK for successful operation.</returns>
    /// <remarks>
    /// This method is called by the abstract class.  It passes the output buffers to the implementor.
    /// Typically, this is when the actual work is done, processing the input buffers into the output
    /// buffers.
    /// </remarks>
    protected abstract int InternalProcessOutput(
      DMOProcessOutput flags,
      int outputBufferCount,
      DMOOutputDataBuffer[] outputBuffers,
      out int pdwStatus);

    /// <summary>
    /// (Abstract) Report whether more input buffers can be accepted
    /// </summary>
    /// <param name="inputStreamIndex">Input stream number</param>
    /// <returns>S_OK if the implementor is ready to accept an input buffer, else S_FALSE</returns>
    /// <remarks>
    /// This method is called by the abstract class.  If the implementor has room for another input buffer, it
    /// should return S_OK.  It is perfectly acceptable for a DMO to only accept one input buffer at a time, and
    /// to return S_FALSE until InternalProcessOutput has been called to process the buffer.
    /// </remarks>
    protected abstract int InternalAcceptingInput(int inputStreamIndex);

    #endregion

    #region Virtual Methods

    /// <summary>
    /// (Virtual) Returns the current time in the media stream
    /// </summary>
    /// <returns>The current time in the media stream</returns>
    /// <remarks>
    /// Typically, this function should be overridden to return the most recent timestamp
    /// from the last call to InternalProcessInput.  It is used to support IMediaParams.GetParam.
    /// The default implementation assumes the stream has no time stamps or that the stream is 
    /// stopped.
    /// </remarks>
    protected virtual long InternalGetCurrentTime()
    {
      return 0;
    }

    /// <summary>
    /// (Virtual) Allows stream resources to be allocated
    /// </summary>
    /// <returns>S_OK for successful operation</returns>
    /// <remarks>
    /// Allows the implementor to allocate any resources necessary for performing the processing.  
    /// The default implementation assumes we don't need to allocate any addition resources to 
    /// perform the processing.
    /// </remarks>
    protected virtual int InternalAllocateStreamingResources()
    {
      return SOK;
    }

    /// <summary>
    /// (Virtual) Allows stream resources to be released
    /// </summary>
    /// <returns>S_OK for successful operation</returns>
    /// <remarks>
    /// Allows the implementor to release any resources used for performing the processing.  
    /// The default implementation assumes we don't need to release any addition resources.
    /// </remarks>
    protected virtual int InternalFreeStreamingResources()
    {
      return SOK;
    }

    /// <summary>
    /// (Virtual) Controls information about how input buffers are formatted
    /// </summary>
    /// <param name="inputStreamIndex">Input stream number</param>
    /// <param name="pflags">Flags specifying how input buffers need to be formatted</param>
    /// <returns>S_OK for successful completion</returns>
    /// <remarks>
    /// Allows the implementor to specify flags controlling the format of input buffers.  The default
    /// implementation return FixedSampleSize | SingleSamplePerBuffer | WholeSamples
    /// </remarks>
    protected virtual int InternalGetInputStreamInfo(int inputStreamIndex, out DMOInputStreamInfo pflags)
    {
      pflags = DMOInputStreamInfo.FixedSampleSize |
          DMOInputStreamInfo.SingleSamplePerBuffer |
          DMOInputStreamInfo.WholeSamples;

      return SOK;
    }

    /// <summary>
    /// (Virtual) Controls information about how output buffers are formatted
    /// </summary>
    /// <param name="outputStreamIndex">Output stream number</param>
    /// <param name="pflags">Flags specifying how output buffers need to be formatted</param>
    /// <returns>S_OK for successful completion</returns>
    /// <remarks>
    /// Allows the implementor to specify flags controlling the format of output buffers.  The default
    /// implementation returns WholeSamples | SingleSamplePerBuffer | FixedSampleSize
    /// </remarks>
    protected virtual int InternalGetOutputStreamInfo(
      int outputStreamIndex,
      out DMOOutputStreamInfo pflags)
    {
      pflags = DMOOutputStreamInfo.WholeSamples |
          DMOOutputStreamInfo.SingleSamplePerBuffer |
          DMOOutputStreamInfo.FixedSampleSize;

      return SOK;
    }

    /// <summary>
    /// (Virtual) Retrieves a preferred media type for a specified input stream
    /// </summary>
    /// <param name="inputStreamIndex">Input stream number</param>
    /// <param name="typeIndex">Index into the array of supported media types</param>
    /// <param name="pmt">The media type</param>
    /// <returns>DMOResults.E_NoMoreItems if out of range or S_OK for successful completion</returns>
    /// <remarks>
    /// If the implementor supports returning a collection of supported media types, it should override
    /// this method.  The default implementation assumes we don't enumerate our supported types.  The
    /// app calling this DMO should just try setting something and see if it works.
    /// </remarks>
    protected virtual int InternalGetInputType(int inputStreamIndex, int typeIndex, out AMMediaType pmt)
    {
      pmt = null;
      return DMOResults.E_NoMoreItems;
    }

    /// <summary>
    /// (Virtual) Retrieves a preferred media type for a specified output stream
    /// </summary>
    /// <param name="outputStreamIndex">Output stream number</param>
    /// <param name="typeIndex">Index into the array of supported media types</param>
    /// <param name="pmt">The media type</param>
    /// <returns>DMOResults.E_NoMoreItems if out of range or S_OK for successful completion</returns>
    /// <remarks>
    /// If the implementor supports returning a collection of supported media types, it should override
    /// this method.  The default implementation assumes our output type is the same as our input type.
    /// </remarks>
    protected virtual int InternalGetOutputType(int outputStreamIndex, int typeIndex, out AMMediaType pmt)
    {
      int hr;

      if (this.InputTypeSet(outputStreamIndex))
      {
        if (typeIndex == 0)
        {
          pmt = this.InputType(outputStreamIndex);
          hr = SOK;
        }
        else
        {
          pmt = null;
          hr = DMOResults.E_NoMoreItems;
        }
      }
      else
      {
        pmt = null;
        hr = DMOResults.E_TypeNotSet;
      }

      return hr;
    }

    /// <summary>
    /// (Virtual) Retrieves the buffer requirements for a specified input stream
    /// </summary>
    /// <param name="inputStreamIndex">Input stream number</param>
    /// <param name="pcbSize">Minimum size of an input buffer for this stream, in bytes</param>
    /// <param name="pcbMaxLookahead">Maximum amount of data that the DMO will hold for lookahead, in bytes</param>
    /// <param name="pcbAlignment">the required buffer alignment, in bytes. If the input stream has no alignment requirement, the value is 1.</param>
    /// <returns>S_OK for successful operation</returns>
    /// <remarks>
    /// The implementator could override this method to specify different values.  The default 
    /// implementation reports that we can accept any alignment, hold no lookahead buffers, and the
    /// input buffer must be at least 1 byte long.  
    /// </remarks>
    protected virtual int InternalGetInputSizeInfo(int inputStreamIndex, out int pcbSize, out int pcbMaxLookahead, out int pcbAlignment)
    {
      pcbSize = 1;
      pcbMaxLookahead = 0;
      pcbAlignment = 1;

      return SOK;
    }

    /// <summary>
    /// (Virtual) Retrieves the maximum latency on a specified input stream
    /// </summary>
    /// <param name="inputStreamIndex">Input stream number</param>
    /// <param name="prtMaxLatency">Latency value</param>
    /// <returns>S_OK for successful completion</returns>
    /// <remarks>
    /// The latency is the difference between a time stamp on the input stream and the corresponding time 
    /// stamp on the output stream. The maximum latency is the largest possible difference in the time stamps.
    /// The default implementation returns E_NOTIMPL indicating the DMO doesn't support reporting latency.
    /// </remarks>
    protected virtual int InternalGetInputMaxLatency(int inputStreamIndex, out long prtMaxLatency)
    {
      prtMaxLatency = 0;

      return ENOTIMPL;
    }

    /// <summary>
    /// (Virtual) Set the maximum latency on a specified input stream
    /// </summary>
    /// <param name="inputStreamIndex">Input stream number</param>
    /// <param name="maxLatency">Maximum latency</param>
    /// <returns>S_OK for successful operation</returns>
    /// <remarks>
    /// The default implementation returns E_NOTIMPL indicating the DMO doesn't support reporting latency.
    /// </remarks>
    protected virtual int InternalSetInputMaxLatency(int inputStreamIndex, long maxLatency)
    {
      return ENOTIMPL;
    }

    /// <summary>
    /// (Virtual) Called to notify of a stream discontinuity
    /// </summary>
    /// <param name="inputStreamIndex">Input stream number</param>
    /// <returns>S_OK for successful operation</returns>
    /// <remarks>
    /// The default implementation assumes no special processing is required for discontinuities.
    /// </remarks>
    protected virtual int InternalDiscontinuity(int inputStreamIndex)
    {
      return SOK;
    }

    #endregion

    /// <summary>
    /// Create a definition for a parameter that is accessible thru IMediaParamInfo
    /// and IMediaParams.
    /// </summary>
    /// <param name="paramNum">Zero based parameter number to set the definition for</param>
    /// <param name="p">Populated ParamInfo struct</param>
    /// <param name="text">Format string (described in MSDN under IMediaParamInfo::GetParamText)</param>
    /// <remarks>
    /// This method should be called from the constructor of the class that implements IMediaObjectImpl.  It
    /// defines a single parameter that can be set on the DMO.  You must call it once for each of the
    /// parameters defined in the call to the IMediaObjectImpl constructor.  This allows for automatic
    /// support of the IMediaParamInfo and IMediaParams methods.  See the 
    /// <see cref="IMediaObjectImpl.ParamCalcValueForTime"/> for additional details.
    /// </remarks>
    protected void ParamDefine(int paramNum, ParamInfo p, string text)
    {
      this.paramsClass.DefineParam(paramNum, p, text);
    }

    /// <summary>
    /// Given a parameter number and a time, return the parameter value at that time.
    /// </summary>
    /// <param name="paramNumber">Zero based parameter number</param>
    /// <param name="timestamp">Time value</param>
    /// <returns>Calculated value for the specified time</returns>
    /// <remarks>
    /// Parameters for DMO can be set in one of two ways.  IMediaParams.SetParam can
    /// be used to set a parameter to a specific value.  It is useful for setting values
    /// that aren't intended to change over time.  There is also IMediaParams.AddEnvelope.  This
    /// method can be use for things that change over time.  For example, consider a parameter
    /// for adjusting the audio volume.  You might want to be able to have the volume go from 0%
    /// to 150% over the first x seconds.  
    /// <para>You can easily support both by using this method.
    /// As you prepare to process buffers, take the timestamp that applies to that buffer, and 
    /// call this method to get the desired value for that parameter at that that time.
    /// </para>
    /// </remarks>
    protected MPData ParamCalcValueForTime(int paramNumber, long timestamp)
    {
      return this.paramsClass.Envelopes[paramNumber].CalcValueForTime(timestamp);
    }

    /// <summary>
    /// Check whether the media type is set for the specified input stream
    /// </summary>
    /// <param name="inputStreamIndex">Zero based stream number to check</param>
    /// <returns>true if the stream type is set</returns>
    protected bool InputTypeSet(int inputStreamIndex)
    {
      Debug.Assert(inputStreamIndex < this.numInputs, "Wrong number of streams");
      return this.inputInfo[inputStreamIndex].TypeSet;
    }

    /// <summary>
    /// Check whether the media type is set for the specified output stream
    /// </summary>
    /// <param name="outputStreamIndex">Zero based stream number to check</param>
    /// <returns>true if the stream type is set</returns>
    protected bool OutputTypeSet(int outputStreamIndex)
    {
      Debug.Assert(outputStreamIndex < this.numOutputs, "Wrong number of streams");
      return this.outputInfo[outputStreamIndex].TypeSet;
    }

    /// <summary>
    /// Get the AMMediaType for the specified Input stream
    /// </summary>
    /// <param name="inputStreamIndex">The stream to get the media type for</param>
    /// <returns>The media type for the stream, or null if not set</returns>
    /// <remarks>
    /// The abstract class will call <see cref="IMediaObjectImpl.InternalCheckInputType"/> to see
    /// whether a given media type is supported.  To see what media type was actually set, the
    /// derived class can call this method.
    /// </remarks>
    protected AMMediaType InputType(int inputStreamIndex)
    {
      if (!this.InputTypeSet(inputStreamIndex))
      {
        return null;
      }

      return this.inputInfo[inputStreamIndex].CurrentMediaType;
    }

    /// <summary>
    /// Get the AMMediaType for the specified Output stream
    /// </summary>
    /// <param name="outputStreamIndex">The stream to get the media type for</param>
    /// <returns>The media type for the stream, or null if not set</returns>
    /// <remarks>
    /// The abstract class will call <see cref="IMediaObjectImpl.InternalCheckOutputType"/> to see
    /// whether a given media type is supported.  To see what media type was actually set, the
    /// derived class can call this method.
    /// </remarks>
    protected AMMediaType OutputType(int outputStreamIndex)
    {
      if (!this.OutputTypeSet(outputStreamIndex))
      {
        return null;
      }

      return this.outputInfo[outputStreamIndex].CurrentMediaType;
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
    /// Compare two blocks of memory to see if they are identical
    /// </summary>
    /// <param name="destination">Pointer to first block</param>
    /// <param name="source">Pointer to second block</param>
    /// <param name="length">Number of bytes to compare</param>
    /// <returns>The number of bytes that compare as equal. If all bytes compare as equal, the input Length is returned.</returns>
    [DllImport("NTDll.dll", EntryPoint = "RtlCompareMemory")]
    private static extern int CompareMemory(
      IntPtr destination,
      IntPtr source,
      [MarshalAs(UnmanagedType.U4)] int length);

    /// <summary>
    /// Set m_fTypesSet by making sure types are set for all input and non-optional output streams.
    /// </summary>
    /// <returns>true if all types are set</returns>
    private bool CheckAllTypesSet()
    {
      DMOOutputStreamInfo flags;
      int dw;
      int hr;

      this.typesSet = false;
      for (dw = 0; dw < this.numInputs; dw++)
      {
        if (!this.InputTypeSet(dw))
        {
          return false;
        }
      }

      for (dw = 0; dw < this.numOutputs; dw++)
      {
        if (!this.OutputTypeSet(dw))
        {
          // Check if it's optional
          hr = this.InternalGetOutputStreamInfo(dw, out flags);

          Debug.Assert(
            0 == (flags & ~(DMOOutputStreamInfo.WholeSamples |
              DMOOutputStreamInfo.SingleSamplePerBuffer |
              DMOOutputStreamInfo.FixedSampleSize |
              DMOOutputStreamInfo.Discardable |
              DMOOutputStreamInfo.Optional)),
              "Wrong value");

          if ((flags & DMOOutputStreamInfo.Optional) > 0)
          {
            return false;
          }
        }
      }

      this.typesSet = true;

      return true;
    }

    /// <summary>
    /// Handle thrown exceptions in a consistent way.
    /// </summary>
    /// <param name="e">The exception that was thrown</param>
    /// <returns>HRESULT to return to COM</returns>
    private int CatFail(Exception e)
    {
      int hr = Marshal.GetHRForException(e);
      if (hr >= 0)
      {
        hr = EUNEXPECTED;
      }

      return hr;
    }

    #endregion //PRIVATEMETHODS

    ///////////////////////////////////////////////////////////////////////////////
    // Small helping Methods                                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region HELPER
    #endregion //HELPER
  }
}
