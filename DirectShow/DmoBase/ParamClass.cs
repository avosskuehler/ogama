// <copyright file="ParamClass.cs" company="FU Berlin">
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

  using DirectShowLib.DMO;

  /// <summary>
  /// This class holds all the information about the parameters
  /// for this DMO.  Using this class allows IMediaObjectImpl to
  /// automatically implement IMediaParamInfo and IMediaParams.  If
  /// your DMO has no parameters, you do not need to create an instance
  /// of this class.
  /// </summary>
  public class ParamClass
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
    /// Initializes a new instance of the ParamClass class.
    /// </summary>
    /// <param name="paramNumber">Number of params</param>
    /// <param name="timeFormats">A <see cref="TimeFormatFlags"/> flags.</param>
    public ParamClass(int paramNumber, TimeFormatFlags timeFormats)
    {
      this.CurrentTimeFormat = 0;
      this.TimeData = 0;

      // Make room for the timeformats
      int count = MPEnvelopes.CountBits((int)timeFormats);
      this.TimeFormats = new Guid[count];

      if ((timeFormats & TimeFormatFlags.Samples) > 0)
      {
        this.TimeFormats[--count] = MediaParamTimeFormat.Samples;
      }

      if ((timeFormats & TimeFormatFlags.Music) > 0)
      {
        this.TimeFormats[--count] = MediaParamTimeFormat.Music;
      }

      if ((timeFormats & TimeFormatFlags.Reference) > 0)
      {
        this.TimeFormats[--count] = MediaParamTimeFormat.Reference;
      }

      Debug.Assert(count == 0, "Unrecognized time format specified");

      // Make room for the parameter info
      this.ParamText = new string[paramNumber];
      this.Parms = new ParamInfo[paramNumber];
      this.Envelopes = new MPEnvelopes[paramNumber];
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

    /// <summary>
    /// Gets or sets the param returned to IMediaParamInfo::GetParamInfo
    /// </summary>
    public ParamInfo[] Parms { get; set; }

    /// <summary>
    /// Gets or sets the list of TimeFormats returned to IMediaParamInfo::GetSupportedTimeFormat
    /// </summary>
    public Guid[] TimeFormats { get; set; }

    /// <summary>
    /// Gets or sets the index into TimeFormats indicating the currently active format
    /// </summary>
    public int CurrentTimeFormat { get; set; }

    /// <summary>
    /// Gets or sets the TimeData for the current format (see IMediaParams::SetTimeFormat)
    /// </summary>
    public int TimeData { get; set; }

    /// <summary>
    /// Gets or sets the string text returned from IMediaParamInfo::GetParamText
    /// </summary>
    public string[] ParamText { get; set; }

    /// <summary>
    /// Gets or sets the envelopes holding the parameter info.  Note that even if parameter
    /// info isn't sent with IMediaParams::AddEnvelope (ie thru IMediaParams::SetParam),
    /// this implementation still creates and uses envelopes.  It just makes envelopes
    /// that span the entire media length.
    /// </summary>
    public MPEnvelopes[] Envelopes { get; set; }

    #endregion //PROPERTIES

    ///////////////////////////////////////////////////////////////////////////////
    // Public methods                                                            //
    ///////////////////////////////////////////////////////////////////////////////
    #region PUBLICMETHODS

    /// <summary>
    /// Defines a new param.
    /// </summary>
    /// <param name="paramNum">The zero based index of the param to set.</param>
    /// <param name="paramInfo">The <see cref="ParamInfo"/> for this parameter</param>
    /// <param name="text">The descriptive string.</param>
    public void DefineParam(int paramNum, ParamInfo paramInfo, string text)
    {
      Debug.Assert(paramNum < this.Parms.Length && paramNum >= 0, "Invalid parameter index");

      this.Parms[paramNum] = paramInfo;
      this.Envelopes[paramNum] = new MPEnvelopes(paramInfo.mpdNeutralValue, paramInfo.mopCaps, paramInfo.mpType, paramInfo.mpdMaxValue, paramInfo.mpdMaxValue);
      this.ParamText[paramNum] = text;
    }

    #endregion //PUBLICMETHODS

    ///////////////////////////////////////////////////////////////////////////////
    // Inherited methods                                                         //
    ///////////////////////////////////////////////////////////////////////////////
    #region OVERRIDES
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
