// <copyright file="MPEnvelopes.cs" company="FU Berlin">
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
  using System.Collections;
  using System.Diagnostics;

  using DirectShowLib.DMO;

  /// <summary>
  /// The idea of envelopes (as I understand them) is to allow a parameter value
  /// to be applied to a range within the stream.  Generally speaking, there is a
  /// start time and stop time, a parameter start value and a parameter end value.  
  /// So, you could say "The volume should go from 0% to 100% over the first 4 
  /// seconds."  There is also a Curve parameter that controls how quickly the 
  /// parameter value should change: Linear, Square, Sine, Jump, etc.
  /// <para>
  /// Also note, the docs for IMediaParams::FlushEnvelope make no sense to me.
  /// Instead of trying to make sense of the crazyness they have written, I
  /// have done something simpler.  If you flush a range, that range gets set
  /// back to the NeutralVal for the parameter.
  /// </para>
  /// <para>
  /// The assumption (based on comments in IMediaParams::SetTimeFormat) is that
  /// the times specified to the envelope are the same format as will be specified
  /// to ProcessInput.  If you are changing the format, call flush.
  /// </para>
  /// </summary>
  public class MPEnvelopes
  {
    ///////////////////////////////////////////////////////////////////////////////
    // Defining Constants                                                        //
    ///////////////////////////////////////////////////////////////////////////////
    #region CONSTANTS

    /// <summary>
    /// Max time value
    /// </summary>
    protected const long MaxTime = 0x7FFFFFFFFFFFFFFF;

    /// <summary>
    /// COM return code indicating success
    /// </summary>
    protected const int SOk = 0;

    /// <summary>
    /// COM return code indicating invalid argument specified
    /// </summary>
    protected const int EINVALIDARG = unchecked((int)0x80070057);

    #endregion //CONSTANTS

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Variables, Enumerations, Events                                  //
    ///////////////////////////////////////////////////////////////////////////////
    #region FIELDS

    /// <summary>
    /// An <see cref="ArrayList"/> with the envelope
    /// </summary>
    private ArrayList envelope;

    /// <summary>
    /// An <see cref="MPData"/> with the default value
    /// </summary>
    private MPData defaultValue;

    /// <summary>
    /// The valid capabilities
    /// </summary>
    private int validCaps;

    /// <summary>
    /// The <see cref="MPType"/> of the envelope.
    /// </summary>
    private MPType dataType;

    /// <summary>
    /// The <see cref="MPData"/> with the minimal value for the envelope.
    /// </summary>
    private MPData minValue;

    /// <summary>
    /// The <see cref="MPData"/> with the maximal value for the envelope.
    /// </summary>
    private MPData maxValue;

    #endregion //FIELDS

    ///////////////////////////////////////////////////////////////////////////////
    // Construction and Initializing methods                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region CONSTRUCTION

    /// <summary>
    /// Initializes a new instance of the MPEnvelopes class.
    /// </summary>
    /// <param name="newDefaultValue">An <see cref="MPData"/> with the default value</param>
    /// <param name="newValidCaps">The valid capabilities</param>
    /// <param name="newMPType">The <see cref="MPType"/> of the envelope.</param>
    /// <param name="newMinValue">The <see cref="MPData"/> with the minimal value for the envelope.</param>
    /// <param name="newMaxValue">The <see cref="MPData"/> with the maximal value for the envelope.</param>
    public MPEnvelopes(
      MPData newDefaultValue,
      MPCaps newValidCaps,
      MPType newMPType,
      MPData newMinValue,
      MPData newMaxValue)
    {
      // Store the neutralValue, min/max value range, data type and supported curve types
      this.defaultValue = newDefaultValue;
      this.validCaps = (int)newValidCaps;
      this.dataType = newMPType;
      this.minValue = newMinValue;
      this.maxValue = newMaxValue;

      // Create an array to hold the segments (size = 3 was chosen arbitrarily)
      this.envelope = new ArrayList(3);

      // Create one segment that spans all time containing the default values
      MPEnvelopeSegment e = new MPEnvelopeSegment();
      e.flags = MPFlags.BeginNeutralVal;
      e.iCurve = MPCaps.Jump;
      e.rtStart = 0;
      e.rtEnd = MaxTime;
      e.valStart = this.defaultValue;
      e.valEnd = this.defaultValue;

      this.envelope.Add(e);
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

    /// <summary>
    /// Add a segment to the envelope.  If this segment overlaps other segments,
    /// the other segments are deleted or shortened, and this segment is inserted
    /// </summary>
    /// <param name="newSegment">The segment to add</param>
    /// <returns>The HRESULT of the method.</returns>
    public int AddSegment(MPEnvelopeSegment newSegment)
    {
      int hr;
      int y;
      MPEnvelopeSegment oldSegment;

      hr = this.ValidateEnvelopeSegment(newSegment);
      if (hr >= 0)
      {
        // Find the first record to modify.  There is always
        // at least one record, and ValidateEnvelopeSegment ensures
        // that the start time is less than the endtime of the last
        // record (MAX_TIME)
        y = 0;

        do
        {
          oldSegment = (MPEnvelopeSegment)this.envelope[y];

          if (newSegment.rtStart <= oldSegment.rtEnd)
          {
            break;
          }

          y++;
        }
        while (true);

        // Process the flags on mNew
        this.UpdateSegment(y, ref newSegment);

        // Depending on how the new segment overlaps, adjust the
        // other segments and add it
        if (newSegment.rtStart <= oldSegment.rtStart)
        {
          if (newSegment.rtEnd >= oldSegment.rtEnd)
          {
            // Existing segment is completely replaced
            this.envelope.RemoveAt(y);
            this.envelope.Insert(y, newSegment);

            // Subsequent records may need to be deleted/adjusted
            if (newSegment.rtEnd > oldSegment.rtEnd)
            {
              this.DeleteRest(y + 1, newSegment.rtEnd);
            }
          }
          else
          {
            // Existing segment is shortened from the left
            oldSegment.rtStart = newSegment.rtEnd + 1;
            this.envelope[y] = oldSegment;
            this.envelope.Insert(y, newSegment);
          }
        }
        else
        {
          if (newSegment.rtEnd >= oldSegment.rtEnd)
          {
            // Existing segment is truncated
            oldSegment.rtEnd = newSegment.rtStart - 1;
            this.envelope[y] = oldSegment;
            this.envelope.Insert(y + 1, newSegment);

            if (newSegment.rtEnd > oldSegment.rtEnd)
            {
              this.DeleteRest(y + 2, newSegment.rtEnd);
            }
          }
          else
          {
            // Split a segment
            MPEnvelopeSegment appendSegment = oldSegment;
            appendSegment.rtStart = newSegment.rtEnd + 1;

            oldSegment.rtEnd = newSegment.rtStart - 1;
            this.envelope[y] = oldSegment;

            this.envelope.Insert(y + 1, newSegment);
            this.envelope.Insert(y + 2, appendSegment);
          }
        }
      }

      return hr;
    }

    /// <summary>
    /// Returns the Envelope the applies to a specific time.  Since there is a
    /// default segment that covers all possible times, this will always return 
    /// a value.
    /// </summary>
    /// <param name="rt">Time to check</param>
    /// <returns>The envelope at that segment</returns>
    public MPEnvelopeSegment FindEnvelopeForTime(long rt)
    {
      int x = 0;
      MPEnvelopeSegment returnSegment;

      do
      {
        returnSegment = (MPEnvelopeSegment)this.envelope[x++];
      }
      while (rt > returnSegment.rtEnd);

      return returnSegment;
    }

    /// <summary>
    /// Calculate the parameter value at a specified time
    /// <para></para>
    /// While there are routines written for all the curve types, I'm not enough
    /// of a math whiz to feel comfortable that I got it right.  I stole the code
    /// from elsewhere and converted it to c#, so there's a chance I messed up.
    /// </summary>
    /// <param name="rt">Time at which to calculate</param>
    /// <returns>MPData value for that time based in the specified Curve</returns>
    public MPData CalcValueForTime(long rt)
    {
      long ir, ic;
      float p;
      MPData ret;
      MPEnvelopeSegment m = this.FindEnvelopeForTime(rt);

      switch (m.iCurve)
      {
        case MPCaps.Jump:

          // Not quite sure how I want to do this.  Consider an envelope
          // that goes from time 0 to 10, and value 55 to 99.  Obviously
          // at time 0, you return 55.  At times 1 thru 9, I assume you 
          // also return 55 (although one could argue they should return
          // 99).  At time 10, you return 99.

          // If you never have a timestamp that exactly equals 10, you
          // would never get the new value.  Seems odd.  Well, that's
          // how I've written it, anyway.
          if (rt < m.rtEnd)
          {
            ret = m.valStart;
          }
          else
          {
            ret = m.valEnd;
          }

          break;

        case MPCaps.Linear:
          ir = m.rtEnd - m.rtStart;
          ic = rt - m.rtStart;
          p = ic / ir;
          ret = new MPData();

          if (this.dataType == MPType.FLOAT)
          {
            ret.vFloat = (m.valEnd.vFloat - m.valStart.vFloat) * p;
          }
          else
          {
            ret.vInt = (int)((m.valEnd.vInt - m.valStart.vInt) * p);
          }

          break;

        case MPCaps.InvSquare:
          ir = m.rtEnd - m.rtStart;
          ic = rt - m.rtStart;
          p = ic / ir;
          p = (float)Math.Sqrt(p);
          ret = new MPData();

          if (this.dataType == MPType.FLOAT)
          {
            ret.vFloat = (m.valEnd.vFloat - m.valStart.vFloat) * p;
          }
          else
          {
            ret.vInt = (int)((m.valEnd.vInt - m.valStart.vInt) * p);
          }

          break;

        case MPCaps.Sine:
          ir = m.rtEnd - m.rtStart;
          ic = rt - m.rtStart;
          p = ic / ir;
          p = (float)((Math.Sin((p * Math.PI) - (Math.PI / 2)) + 1) / 2);
          ret = new MPData();

          if (this.dataType == MPType.FLOAT)
          {
            ret.vFloat = (m.valEnd.vFloat - m.valStart.vFloat) * p;
          }
          else
          {
            ret.vInt = (int)((m.valEnd.vInt - m.valStart.vInt) * p);
          }

          break;

        case MPCaps.Square:
          ir = m.rtEnd - m.rtStart;
          ic = rt - m.rtStart;
          p = ic / ir;
          p = p * p;
          ret = new MPData();

          if (this.dataType == MPType.FLOAT)
          {
            ret.vFloat = (m.valEnd.vFloat - m.valStart.vFloat) * p;
          }
          else
          {
            ret.vInt = (int)((m.valEnd.vInt - m.valStart.vInt) * p);
          }

          break;

        default:
          Debug.Assert(false, "Invalid flag!");
          ret = new MPData();
          break;
      }

      return ret;
    }

    /// <summary>
    /// Make sure the envelope parameters are valid
    /// </summary>
    /// <param name="m">Envelope segment to check</param>
    /// <returns>E_INVALIDARG if parameters are incorrect, else S_OK</returns>
    public int ValidateEnvelopeSegment(MPEnvelopeSegment m)
    {
      int hr;

      // The start time cannot be MAX_TIME
      // The end time must be >= start time
      // The iCurve must be one of the recognized formats
      // The iCurve must be one of the supported formats
      // The iCurve can only be one format
      // The flags must be one of the recognized formats
      // The flags can only be one value
      if (m.rtStart == MaxTime ||
          m.rtEnd < m.rtStart ||
          (m.iCurve & ~(MPCaps.InvSquare | MPCaps.Jump | MPCaps.Linear | MPCaps.Sine | MPCaps.Square)) > 0 ||
          ((int)m.iCurve & ~this.validCaps) > 0 ||
          CountBits((int)m.iCurve) > 1 ||
          (m.flags & (~(MPFlags.BeginCurrentVal | MPFlags.BeginNeutralVal | MPFlags.Standard))) > 0 ||
          CountBits((int)m.flags) > 1)
      {
        hr = EINVALIDARG;
      }
      else
      {
        hr = SOk;
      }

      return hr;
    }

#if DEBUG
    /// <summary>
    /// Debug utility to dump an envelope
    /// </summary>
    public void DumpEnvelope()
    {
      int c = this.envelope.Count;
      Debug.WriteLine("----------------------------");

      for (int x = 0; x < c; x++)
      {
        MPEnvelopeSegment oldSegment = (MPEnvelopeSegment)this.envelope[x];

        Debug.WriteLine(string.Format("{0}: {1}-{2} {3}-{4}", x, oldSegment.rtStart, oldSegment.rtEnd, oldSegment.valStart.vInt, oldSegment.valEnd.vInt));
      }
    }
#endif

    /// <summary>
    /// Count how many bits are set in an flag
    /// </summary>
    /// <param name="i">bitmap to check</param>
    /// <returns>Count of the number of bits set</returns>
    internal static int CountBits(int i)
    {
      int returnValue = 0;

      while (i > 0)
      {
        if ((i & 1) > 0)
        {
          returnValue++;
        }

        i >>= 1;
      }

      return returnValue;
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

    /// <summary>
    /// Utility function called from AddEnvelope.  When adding an envelope segment,
    /// it could happen that the new segment overlaps multiple existing segments.  
    /// This routine walks starting at the specified segment.  It either deletes
    /// the segment or adjusts it until it reaches the ending time of the new
    /// segment.
    /// </summary>
    /// <param name="z">Starting segment</param>
    /// <param name="end">Ending segment</param>
    private void DeleteRest(int z, long end)
    {
      // Don't walk past the end of the array
      while (z < this.envelope.Count)
      {
        MPEnvelopeSegment oldSegment = (MPEnvelopeSegment)this.envelope[z];

        // Have we found the end time of the current segment?
        if (oldSegment.rtEnd <= end)
        {
          // Nope.  Delete this segment.
          this.envelope.RemoveAt(z);
        }
        else
        {
          // Yes, this is the end.  Split the old segment
          oldSegment.rtStart = end + 1;
          this.envelope[z] = oldSegment;
          break;
        }
      }
    }

    /// <summary>
    /// See if the specified parameter value falls within the allowable range
    /// </summary>
    /// <param name="m">Value to check</param>
    /// <returns>true if the parameter value is valid</returns>
    private bool CheckRange(MPData m)
    {
      bool returnValue;

      switch (this.dataType)
      {
        case MPType.BOOL:
        case MPType.ENUM:
        case MPType.INT:
          returnValue = m.vInt >= this.minValue.vInt && m.vInt <= this.maxValue.vInt;
          break;

        case MPType.FLOAT:
          returnValue = m.vFloat >= this.minValue.vFloat && m.vFloat <= this.maxValue.vFloat;
          break;

        default:
          Debug.Assert(false, "Invalid Type");
          returnValue = false;
          break;
      }

      return returnValue;
    }

    /// <summary>
    /// Updates the given <see cref="MPEnvelopeSegment"/>
    /// with the new value
    /// </summary>
    /// <param name="y">The new value</param>
    /// <param name="m">Ref. The <see cref="MPEnvelopeSegment"/>
    /// to be updated.</param>
    private void UpdateSegment(int y, ref MPEnvelopeSegment m)
    {
      if ((m.flags & MPFlags.BeginNeutralVal) > 0)
      {
        m.valStart = this.defaultValue;
      }
      else if ((m.flags & MPFlags.BeginCurrentVal) > 0)
      {
        if (y > 0)
        {
          MPEnvelopeSegment oldSegment = (MPEnvelopeSegment)this.envelope[y - 1];
          m.valStart = oldSegment.valEnd;
        }
        else
        {
          m.valStart = this.defaultValue;
        }
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
