// <copyright file="WebcamPropertiesDlg.cs" company="FU Berlin">
// Copyright (c) 2008 All Rights Reserved
// </copyright>
// <author>Adrian Voßkühler</author>
// <email>adrian@ogama.net</email>

namespace OgamaControls
{
  using System.Windows.Forms;

  /// <summary>
  /// This <see cref="Form"/> encapsulates an dialog for getting, setting
  /// and previewing the properties of a webcam including audio input.
  /// </summary>
  public partial class WebcamPropertiesDlg : Form // , ISampleGrabberCB
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
    #endregion //FIELDS

    ///////////////////////////////////////////////////////////////////////////////
    // Construction and Initializing methods                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region CONSTRUCTION

    /// <summary>
    /// Initializes a new instance of the WebcamPropertiesDlg class.
    /// </summary>
    public WebcamPropertiesDlg(DXCapture capture)
    {
      InitializeComponent();
      this.dsVideoProperties.DxCapture = capture;
    }

    #endregion //CONSTRUCTION

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Properties                                                       //
    ///////////////////////////////////////////////////////////////////////////////
    #region PROPERTIES

    /// <summary>
    /// Gets or sets the current <see cref="CaptureDeviceProperties"/>.
    /// </summary>
    public CaptureDeviceProperties Properties
    {
      get
      {
        return this.dsVideoProperties.Properties;
      }
      //set
      //{
      //  this.dsVideoProperties.Properties = value;
      //}
    }

    ///// <summary>
    ///// Sets a value indicating whether the underlying <see cref="DSVideoProperties"/>
    ///// should be initialized for capture mode, that means should preview the video capture.
    ///// </summary>
    //public bool ShouldPreview
    //{
    //  set
    //  {
    //    this.dsVideoProperties.ShouldPreview = value;
    //  }
    //}

    #endregion //PROPERTIES

    ///////////////////////////////////////////////////////////////////////////////
    // Public methods                                                            //
    ///////////////////////////////////////////////////////////////////////////////
    #region PUBLICMETHODS
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
    #region PRIVATEMETHODS
    #endregion //METHODS

    ///////////////////////////////////////////////////////////////////////////////
    // Small helping Methods                                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region HELPER
    #endregion //HELPER

    ////#region ISampleGrabberCB Members

    /////// <summary>
    /////// The BufferCB method is a callback method that receives a pointer to the sample buffer.
    /////// </summary>
    /////// <param name="SampleTime">Starting time of the sample, in seconds.</param>
    /////// <param name="pBuffer">Pointer to a buffer that contains the sample data.</param>
    /////// <param name="BufferLen">Length of the buffer pointed to by pBuffer, in bytes.</param>
    /////// <returns>Returns S_OK if successful, or an HRESULT error code otherwise.</returns>
    ////public int BufferCB(double SampleTime, IntPtr pBuffer, int BufferLen)
    ////{
    ////  byte[] buffer = new byte[BufferLen];

    ////  Marshal.Copy(pBuffer, buffer, 0, BufferLen);
    ////  uint nSamples = (uint)BufferLen / 2;

    ////  //pgbLevel.Value = (short)buffer[0]/255*100;

    ////  //// adjust FFT Sample to next power 2 but fill data with silence
    ////  //uint pow2Samples = FFT.NextPowerOfTwo(nSamples);

    ////  //double[] RealIn = new double[pow2Samples];
    ////  //double[] RealOut = new double[pow2Samples];
    ////  //double[] ImagOut = new double[pow2Samples];
    ////  //double[] AmplOut = new double[pow2Samples];

    ////  //for (int n = 0; n < BufferLen; n += 2)
    ////  //{
    ////  //  RealIn[n / 2] = Convert.ToDouble((short)buffer[n]); //left;
    ////  //  //Samples[n + 1]; //right;
    ////  //}

    ////  //if (pow2Samples != nSamples)
    ////  //{
    ////  //  double dsilence = 0.0;
    ////  //  for (uint ii = nSamples; ii < pow2Samples; ii++)
    ////  //  {
    ////  //    RealIn[ii] = dsilence;
    ////  //  }
    ////  //  nSamples = pow2Samples;
    ////  //}

    ////  //FFT.Compute(nSamples, RealIn, null, RealOut, ImagOut, false);
    ////  //FFT.Norm(nSamples, RealOut, ImagOut, AmplOut);

    ////  //double maxAmpl = (256 * 256);

    ////  //int SamplesPerSecond = 44100;
    ////  //const int NUM_FREQUENCY = 19;
    ////  //int[] METER_FREQUENCY = new int[NUM_FREQUENCY] { 30, 60, 80, 90, 100, 150, 200, 330, 480, 660, 880, 1000, 1500, 2000, 3000, 5000, 8000, 12000, 16000 };
    ////  //int[] meterData = new int[NUM_FREQUENCY];

    ////  //// update meter
    ////  //int centerFreq = (SamplesPerSecond / 2);
    ////  //for (int i = 0; i < NUM_FREQUENCY; ++i)
    ////  //{
    ////  //  if (METER_FREQUENCY[i] > centerFreq)
    ////  //    meterData[i] = 0;
    ////  //  else
    ////  //  {
    ////  //    int indice = (int)(METER_FREQUENCY[i] * nSamples / SamplesPerSecond);
    ////  //    int metervalue = (int)(20.0 * Math.Log10(AmplOut[indice] / maxAmpl));
    ////  //    meterData[i] = metervalue;
    ////  //  }
    ////  //}

    ////  ////OnAudioSamplesReceived(new FFTEventArgs(meterData));
    ////  //pmcAudio.SetData(meterData, 0, NUM_FREQUENCY);

    ////  return 0;
    ////}

    /////// <summary>
    /////// The SampleCB method is a callback method that receives a pointer to the media sample.
    /////// But we don´t use it because we use <see cref="BufferCB(double, IntPtr, int)"/>.
    /////// </summary>
    /////// <param name="SampleTime">Starting time of the sample, in seconds.</param>
    /////// <param name="pSample">Pointer to the <see cref="IMediaSample"/> interface of the sample.</param>
    /////// <returns>Returns S_OK if successful, or an HRESULT error code otherwise.</returns>
    ////public int SampleCB(double SampleTime, IMediaSample pSample)
    ////{
    ////  //AMMediaType media=new AMMediaType();
    ////  //int hr=pSample.(out media);
    ////  //DsError.ThrowExceptionForHR(hr);
    ////  //WaveFormatEx waveFormat = new WaveFormatEx();
    ////  //Marshal.PtrToStructure(media.formatPtr,waveFormat);

    ////  return 0;
    ////}
    ////#endregion
  }
}