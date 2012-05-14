// <copyright file="FixationDetection.cs" company="FU Berlin">
// ******************************************************
// OGAMA - open gaze and mouse analyzer 
// Copyright (C) 2012 Adrian Voßkühler  
// ------------------------------------------------------------------------
// This program is free software; you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation; either version 2 of the License, or (at your option) any later version.
// This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
// You should have received a copy of the GNU General Public License along with this program; if not, write to the Free Software Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA 02111-1307 USA
// **************************************************************
// Program Name:    Eye Fixation Analysis Functions
// Company:         LC Technologies, Inc.
//                  1483 Chain Bridge Road, Suite 104
//                  McLean, VA 22101
//                  (703) 385-7133
// 
// Makers of the Eyegaze System, additional information about LC
// Technologies and its products may be found at http://www.eyegaze.com
// 
//  * ported to C# and added time estimation support 
//  * by Adrian Voßkühler, Freie Universität Berlin.
// 
// </copyright>
// <author>Adrian Voßkühler</author>
// <email>adrian@ogama.net</email>

namespace Ogama.Modules.Common.Tools
{
  using System;
  using System.Windows.Forms;

  /// <summary>
  /// State of current eye motion. Can be moving, fixation, completed or error.
  /// </summary>
  public enum EyeMotionState
  {
    /// <summary>
    /// When the eye is in motion, ie during saccades
    /// </summary>
    MOVING = 0,

    /// <summary>
    /// When the eye is still, ie during fixations
    /// </summary>
    FIXATING = 1,

    /// <summary>
    /// Upon the detected completion of a fixation
    /// </summary>
    FIXATION_COMPLETED = 2,

    /// <summary>
    /// Detection algorithm failed
    /// </summary>
    ERROR = 3,
  }

  /// <summary>
  /// Class for calculating fixation data.
  /// </summary>
  /// <remarks>
  /// It is based on the fixation calculation source code from LC technologies with
  /// permission from Dixon Cleveland.
  /// <para>Please refer to</para>
  /// <para>Program Name:    Eye Fixation Analysis Functions
  /// Company:         LC Technologies, Inc.
  ///                  1483 Chain Bridge Road, Suite 104
  ///                  McLean, VA 22101
  ///                  (703) 385-7133
  /// </para>
  /// <para>
  /// Makers of the Eyegaze System, additional information about LC
  /// Technologies and its products may be found at http://www.eyegaze.com</para>
  /// </remarks>
  public class FixationDetection
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

    /// <summary>
    /// Length of the delay line in DetectFixation method -- should be greater than
    /// minimum_fix_samples.
    /// </summary>
    private static int ringsize;

    /// <summary>
    /// number of times this function has been called since it was initialized 
    /// (30ths or 60ths of a second, depending on eyetracking sample rate)
    /// </summary>
    private long callCount;

    /// <summary>
    /// number of successive samples with no eye found
    /// </summary>
    private int numberOfNoEyeFound;

    /* DATA ON PREVIOUS FIXATION                */

    /// <summary>
    /// count that the previous fixation ended
    /// </summary>
    private long prevFixEndCount;

    /* DATA ON PRESENT FIXATION                 */

    /// <summary>
    /// call count that the fixation starts 
    /// </summary>
    private long presFixStartCount;

    /// <summary>
    /// time fixation starts
    /// </summary>
    private long timePresFixStarttime;

    /// <summary>
    /// call count that the fixation ends
    /// </summary>
    private long presFixEndCount;

    /// <summary>
    /// time fixation ends    
    /// </summary>
    private long timePresFixEndtime;

    /// <summary>
    /// number of samples in the present fix
    /// </summary>
    private int presFixSamples;

    /// <summary>
    /// summations for calculation of average
    /// </summary>
    private float presXFixSum;

    /// <summary>
    /// fixation position
    /// </summary>
    private float presYFixSum;

    /// <summary>
    /// average x-coordinate of the eye fixation point (user selected units)
    /// </summary>
    private float presXFix;

    /// <summary>
    /// average y-coordinate of the eye fixation point (user selected units)
    /// </summary>
    private float presYFix;

    /// <summary>
    /// number of samples outside the fixation
    /// </summary>
    private int presNumberOut;

    /// <summary>
    /// difference between gazepoint and fixation (x, y, and radius) 
    /// </summary>
    private float presDifference;

    /* DATA ON NEW FIXATION                     */

    /// <summary>
    /// Time in ms the fixation has startet
    /// </summary>
    private long newFixStartTime;

    /// <summary>
    /// call count that the new fixation starts
    /// </summary>
    private long newFixStartCount;

    /// <summary>
    /// Time in ms the fixation has ended
    /// </summary>
    private long newFixEndTime;

    /// <summary>
    /// call count that the new fixation ends
    /// </summary>
    private long newFixEndCount;

    /// <summary>
    /// number of samples in the fixation
    /// </summary>
    private int numberNewFixSamples;

    /// <summary>
    /// summations for the FIR filter calculations of the eye motion 
    /// </summary>
    private float xNewFixSum;

    /// <summary>
    /// summations for the FIR filter calculations of the eye motion 
    /// </summary>
    private float yNewFixSum;

    /// <summary>
    /// x-coordinate of average coordinate of the eye fixation point (user selected units)
    /// </summary>
    private float xNewFix;

    /// <summary>
    /// y-coordinate of average coordinate of the eye fixation point (user selected units)
    /// </summary>
    private float yNewFix;

    /// <summary>
    /// difference between gazepoint and fixation (x, y, and radius)
    /// </summary>
    private float newDifference;

    /* RING BUFFERS STORING PAST VALUES:        */

    /// <summary>
    /// Array of <c>RING_SIZE</c> containing: timings of gaze samples
    /// </summary>
    private long[] timeGazeRing;

    /// <summary>
    /// Array of <c>RING_SIZE</c> containing: x-coordinates of gaze samples
    /// </summary>
    private float[] xGazeRing;

    /// <summary>
    /// Array of <c>RING_SIZE</c> containing: y-coordinates of gaze samples
    /// </summary>
    private float[] yGazeRing;

    /// <summary>
    /// Array of <c>RING_SIZE</c> containing: gaze found flags
    /// </summary>
    private bool[] gazeFoundRing;

    /// <summary>
    /// Array of <c>RING_SIZE</c> containing: eye motion states
    /// </summary>
    /// <seealso cref="EyeMotionState"/>
    private EyeMotionState[] eyeMotionState;

    /// <summary>
    /// Array of <c>RING_SIZE</c> containing: x-coordinates of fixation centers
    /// </summary>
    private float[] xFixRing;

    /// <summary>
    /// Array of <c>RING_SIZE</c> containing: y-coordinates of fixation centers
    /// </summary>
    private float[] yFixRing;

    /// <summary>
    /// Array of <c>RING_SIZE</c> containing: deviation of gaze from fixation
    /// </summary>
    private float[] gazeDeviationRing;

    /// <summary>
    /// Array of <c>RING_SIZE</c> containing: saccade duration
    /// </summary>
    private int[] sacDurationRingSamples;

    /// <summary>
    /// Array of <c>RING_SIZE</c> containing: fixation duration in samples
    /// </summary>
    private int[] fixDurationRingSamples;

    /// <summary>
    /// Array of <c>RING_SIZE</c> containing: fixation duration in milliseconds
    /// </summary>
    private long[] fixDurationRingMilliseconds;

    /// <summary>
    /// Array of <c>RING_SIZE</c> containing: start time of fixation in milliseconds
    /// </summary>
    private long[] fixStarttimeMilliseconds;

    /// <summary>
    /// ring index of the present gaze sample
    /// </summary>
    private int ringIndex;

    /// <summary>
    /// ring index of the gaze sample taken minimum_fix_samples ago  
    /// </summary>
    private int ringIndexDelay;

    #endregion //FIELDS

    ///////////////////////////////////////////////////////////////////////////////
    // Construction and Initializing methods                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region CONSTRUCTION

    /// <summary>
    /// This function clears any previous, present and new fixations, and it 
    /// initializes DetectFixation's internal ring buffers of prior
    /// gazepoint data.  <see cref="InitFixation(int)"/> should be called prior to a sequence  
    /// of calls to <c>DetectFixation()</c>.                       
    /// </summary>
    /// <param name="minimum_fix_samples">minimum number of gaze samples that can be considered a fixation
    /// <remarks>Note: if the input value is less than 3, the function sets it to 3</remarks></param>
    public void InitFixation(int minimum_fix_samples)
    {
      /* Make sure the minimum fix time is at least 3 samples. */
      if (minimum_fix_samples < 3)
      {
        minimum_fix_samples = 3;
      }

      // Length of the delay line.
      // Note: should be greater than minimum_fix_samples,
      // if the input value is less than minimum_fix_samples,
      // the function sets it to minimum_fix_samples+1
      ringsize = Document.ActiveDocument.ExperimentSettings.FixationRingSize;
      /* Make sure the ring size is large enough to handle the delay.             */
      if (minimum_fix_samples >= ringsize)
      {
        ringsize = minimum_fix_samples + 1;
      }

      this.timeGazeRing = new long[ringsize];
      this.xGazeRing = new float[ringsize];
      this.yGazeRing = new float[ringsize];
      this.gazeFoundRing = new bool[ringsize];
      this.eyeMotionState = new EyeMotionState[ringsize];
      this.xFixRing = new float[ringsize];
      this.yFixRing = new float[ringsize];
      this.gazeDeviationRing = new float[ringsize];
      this.sacDurationRingSamples = new int[ringsize];
      this.fixDurationRingSamples = new int[ringsize];
      this.fixDurationRingMilliseconds = new long[ringsize];
      this.fixStarttimeMilliseconds = new long[ringsize];

      /* Initialize the internal ring buffer.                                     */
      for (this.ringIndex = 0; this.ringIndex < ringsize; this.ringIndex++)
      {
        this.timeGazeRing[this.ringIndex] = 0;
        this.xGazeRing[this.ringIndex] = 0.0F;
        this.yGazeRing[this.ringIndex] = 0.0F;
        this.gazeFoundRing[this.ringIndex] = false;
        this.eyeMotionState[this.ringIndex] = EyeMotionState.MOVING;
        this.xFixRing[this.ringIndex] = 0.0F;
        this.yFixRing[this.ringIndex] = 0.0F;
        this.gazeDeviationRing[this.ringIndex] = -0.1F;
        this.sacDurationRingSamples[this.ringIndex] = 0;
        this.fixDurationRingSamples[this.ringIndex] = 0;
        this.fixDurationRingMilliseconds[this.ringIndex] = 0;
        this.fixStarttimeMilliseconds[this.ringIndex] = 0;
      }

      this.ringIndex = 0;
      this.ringIndexDelay = ringsize - minimum_fix_samples;

      /* Set the call count to zero, and initialize the previous fixation end     */
      /* count so the first saccade duration is a legitimate count.               */
      this.callCount = 0;
      this.prevFixEndCount = 0;

      /* Reset the present fixation data.                                         */
      this.ResetPresFixation();

      /* Reset the new fixation data.                                             */
      this.ResetNewFixation();

      /* Initialize the number of successive samples with no eye found.           */
      this.numberOfNoEyeFound = 0;
    }

    #endregion //CONSTRUCTION

    ///////////////////////////////////////////////////////////////////////////////
    // Methods for doing main class job                                          //
    ///////////////////////////////////////////////////////////////////////////////
    #region METHODS

    /// <summary>
    /// This function converts a series of uniformly-sampled (raw) gaze       
    /// points into a series of variable-duration saccades and fixations.</summary>
    /// <remarks>
    /// Fixation analysis may be performed in real time or after the fact.  To   
    /// allow eye fixation analysis during real-time eyegaze data collection,    
    /// the function is designed to be called once per sample.  When the eye     
    /// is in motion, ie during saccades, the function returns 0 (MOVING).       
    /// When the eye is still, ie during fixations, the function returns 1       
    /// (FIXATING).  Upon the detected completion of a fixation, the function    
    /// returns 2 (FIXATION_COMPLETED) and produces:                             
    ///   a) the time duration of the saccade between the last and present       
    ///      eye fixation (eyegaze samples)                                      
    ///   b) the time duration of the present, just completed fixation           
    ///      (eyegaze samples)                                                   
    ///   c) the average x and y coordinates of the eye fixation                 
    ///      (in user defined units of x_gaze and y_gaze)                        
    /// Note: Although this function is intended to work in "real time", there   
    /// is a delay of minimum_fix_samples in the filter which detects the        
    /// motion/fixation condition of the eye.                                    
    /// <para>PRINCIPLE OF OPERATION</para>
    ///    This function detects fixations by looking for sequences of gaze-     
    /// point measurements that remain relatively constant.  If a new gazepoint  
    /// lies within a circular region around the running average of an on-going  
    /// fixation, the fixation is extended to include the new gazepoint.         
    /// (The radius of the acceptance circle is user specified by setting the    
    /// value of the function argument gaze_deviation_threshold.)                
    ///    To accommodate noisy eyegaze measurements, a gazepoint that exceeds   
    /// the deviation threshold is included in an on-going fixation if the       
    /// subsequent gazepoint returns to a position within the threshold.         
    ///    If a gazepoint is not found, during a blink for example, a fixation   
    /// is extended if a) the next legitimate gazepoint measurement falls within 
    /// the acceptance circle, and b) there are less than minimum_fix_samples    
    /// of successive missed gazepoints.  Otherwise, the previous fixation       
    /// is considered to end at the last good gazepoint measurement.             
    /// <para>NITS OF MEASURE</para>
    ///    The gaze position/direction may be expressed in any units (e.g.       
    /// millimeters, pixels, or radians), but the filter threshold must be       
    /// expressed in the same units.                                             
    /// <para>INITIALIZING THE FUNCTION </para>   
    ///    Prior to analyzing a sequence of gazepoint data, the InitFixation     
    /// function should be called to clear any previous, present and new         
    /// fixations and to initialize the ring buffers of prior gazepoint data.    
    /// <para>PROGRAM NOTES</para>
    /// For purposes of describing an ongoing sequence of fixations, fixations   
    /// in this program are referred to as "previous", "present", and "new".     
    /// The present fixation is the one that is going on right now, or, if a     
    /// new fixation has just started, the present fixation is the one that      
    /// just finished.  The previous fixation is the one immediatly preceeding   
    /// the present one, and a new fixation is the one immediately following     
    /// the present one.  Once the present fixation is declared to be completed, 
    /// the present fixation becomes the previous one, the new fixation becomes  
    /// the present one, and there is not yet a new fixation.</remarks>
    /// <param name="gazepoint_found">flag indicating whether or not the image processing algo
    /// detected the eye and computed a valid gazepoint (TRUE/FALSE)</param>
    /// <param name="time_gaze">Current sample timing value in ms.</param>
    /// <param name="x_gaze">x-coordinate of present gazepoint (user specified units)</param>
    /// <param name="y_gaze">y-coordinate of present gazepoint (user specified units)</param>
    /// <param name="gaze_deviation_threshold">distance that a gazepoint may vary from the average fixation
    /// point and still be considered part of the fixation (user specified units)</param>
    /// <param name="minimum_fix_samples">minimum number of gaze samples that can be considered a
    /// fixation. Note: if the input value is less than 3, the function sets it to 3</param>
    /// <param name="ptr_gazepoint_found_delayed">Delayed Gazepoint data with fixation annotations: 
    /// sample gazepoint-found flag, min_fix_samples ago</param>
    /// <param name="ptr_x_gaze_delayed">sample gazepoint x-coordinates min_fix_samples ago</param>
    /// <param name="ptr_y_gaze_delayed">sample gazepoint y-coordinates min_fix_samples ago</param>
    /// <param name="ptr_gaze_deviation_delayed">deviation of the gaze from the present fixation,
    /// min_fix_samples ago</param>
    /// <param name="ptr_x_fix_delayed">x-coordinate of fixation point as estimated, min_fix_samples ago</param>
    /// <param name="ptr_y_fix_delayed">y-coordinate of fixation point as estimated, min_fix_samples ago</param>
    /// <param name="ptr_saccade_duration_delayed">duration of the saccade preceeding the preset fixation
    /// (samples)</param>
    /// <param name="ptr_fix_start_time">samling time of fixation begin</param>
    /// <param name="ptr_fix_duration_delayed_milliseconds"> duration of the present fixation in ms</param>
    /// <param name="ptr_fix_duration_delayed_samples">duration of the present fixation (samples)</param>
    /// <returns>MOVING 0   The eye was in motion min_fix_samples ago. 
    /// FIXATING 1   The eye was fixating min_fix_samples ago.
    /// FIXATION_COMPLETED 2   A completed fixation has just been detected, 
    /// the fixation ended min_fix_samples ago.</returns>
    public EyeMotionState DetectFixation(
           bool gazepoint_found,
           long time_gaze,
           float x_gaze,
           float y_gaze,
           float gaze_deviation_threshold,
           int minimum_fix_samples,
           out bool ptr_gazepoint_found_delayed,
           out float ptr_x_gaze_delayed,
           out float ptr_y_gaze_delayed,
           out float ptr_gaze_deviation_delayed,
           out float ptr_x_fix_delayed,
           out float ptr_y_fix_delayed,
           out int ptr_saccade_duration_delayed,
           out long ptr_fix_start_time,
           out long ptr_fix_duration_delayed_milliseconds,
           out int ptr_fix_duration_delayed_samples)
    {
      /* Make sure the minimum fix time is at least 3 samples.                    */
      if (minimum_fix_samples < 3)
      {
        minimum_fix_samples = 3;
      }

      /* Make sure the ring size is large enough to handle the delay.             */
      if (minimum_fix_samples >= ringsize)
      {
        MessageBox.Show(
       "minimum_fix_samples >= RING_SIZE",
       Application.ProductName,
       MessageBoxButtons.OK,
       MessageBoxIcon.Stop);

        ptr_x_gaze_delayed = 0.0F;
        ptr_y_gaze_delayed = 0.0F;
        ptr_gazepoint_found_delayed = false;
        ptr_x_fix_delayed = 0.0F;
        ptr_y_fix_delayed = 0.0F;
        ptr_gaze_deviation_delayed = -0.1F;
        ptr_saccade_duration_delayed = 0;
        ptr_fix_duration_delayed_samples = 0;
        ptr_fix_duration_delayed_milliseconds = 0;
        ptr_fix_start_time = 0;

        return EyeMotionState.ERROR;
      }

      /* Increment the call count, the ring index, and the delayed ring index.    */
      this.callCount++;
      this.ringIndex++;
      if (this.ringIndex >= ringsize)
      {
        this.ringIndex = 0;
      }

      this.ringIndexDelay = this.ringIndex - minimum_fix_samples;
      if (this.ringIndexDelay < 0)
      {
        this.ringIndexDelay += ringsize;
      }

      /* Update the storage rings.   */
      this.timeGazeRing[this.ringIndex] = time_gaze;
      this.xGazeRing[this.ringIndex] = x_gaze;
      this.yGazeRing[this.ringIndex] = y_gaze;
      this.gazeFoundRing[this.ringIndex] = gazepoint_found;

      /* Initially assume the eye is moving.                                      */
      /* Note: These values are updated during the processing of this and         */
      /* subsequent gazepoints.                                                   */
      this.eyeMotionState[this.ringIndex] = EyeMotionState.MOVING;
      this.xFixRing[this.ringIndex] = -0.0F;
      this.yFixRing[this.ringIndex] = -0.0F;
      this.gazeDeviationRing[this.ringIndex] = -0.1F;
      this.sacDurationRingSamples[this.ringIndex] = 0;
      this.fixDurationRingSamples[this.ringIndex] = 0;
      this.fixDurationRingMilliseconds[this.ringIndex] = 0;
      this.fixStarttimeMilliseconds[this.ringIndex] = 0;
      /*- - - - - - - - - - - - - Process Tracked Eye  - - - - - - - - - - - - - -*/

      /* A1 If the eye's gazepoint was successfully measured this sample,         */
      if (gazepoint_found == true)
      {
        /*    The number of successive no-tracks is zero.                           */
        this.numberOfNoEyeFound = 0;

        /* B1 If there is a present fixation,                                       */
        if (this.presFixSamples > 0)
        {
          /*       Compute the deviation of the gazepoint from the present fixation.  */
          this.CalcGazeDeviationFromPresFix(x_gaze, y_gaze);

          /*   C1  If the gazepoint is within the present fixation region,            */
          if (this.presDifference <= gaze_deviation_threshold)
          {
            /*          Restore any previous gazepoints that were temporarily left      */
            /*          out of the fixation.                                            */
            this.RestoreOutPoints();

            /*          Update the present fixation hypothesis, and check if there      */
            /*          are enough samples to declare that the eye is fixating.         */
            this.UpdatePresFixation(time_gaze, x_gaze, y_gaze, minimum_fix_samples);
          }

/*   C2  Otherwise, if the point is outside the present fixation region,    */
          else   /* if (fPresDr > gaze_deviation_threshold) */
          {
            /*          Increment the number of gazepoint samples outside the           */
            /*          present fix.                                                    */
            this.presNumberOut++;

            /*      D1  If the present fixation is finished, i.e. if there have         */
            /*          been minimum_fix_samples since the gazepoint last matched       */
            /*          the present fixation, and the present fixation is long          */
            /*          enough to count as a real fixation,                             */
            if (((int)(this.callCount - this.presFixEndCount) >=
                 minimum_fix_samples) &&
                (this.presFixSamples >= minimum_fix_samples))
            {
              /*             Declare the present fixation to be completed, move the       */
              /*             present fixation to the prior, move the new fixation to      */
              /*             the present, and check if the new (now present) fixation     */
              /*             has enough points for the eye to be declared to be fixating. */
              this.DeclareCompletedFixation(minimum_fix_samples);

              /*             Compute the deviation of the gazepoint from the now          */
              /*             present fixation.                                            */
              this.CalcGazeDeviationFromPresFix(x_gaze, y_gaze);

              /*         E1  If the gazepoint is within the now present fixation region,  */
              if (this.presDifference <= gaze_deviation_threshold)
              {
                /*                Update the present fixation data, and check if there      */
                /*                are enough samples to declare that the eye is fixating.   */
                this.UpdatePresFixation(time_gaze, x_gaze, y_gaze, minimum_fix_samples);
              }

/*         E2  Otherwise, if the gazepoint is outside the now present       */
              /*             fixation,                                                    */
              else   /* if (fPresDr > gaze_deviation_threshold) */
              {
                /*                Start a new fixation at the gazepoint.                    */
                this.StartNewFixAtGazepoint(time_gaze, x_gaze, y_gaze);
              }
            }

/*      D2  Otherwise, if the present fixation is not finished,             */
            else
            {
              /*         F1  If there is a new fixation hypothesis,                       */
              if (this.numberNewFixSamples > 0)
              {
                /*                Compute the deviation of the gazepoint from the new       */
                /*                fixation.                                                 */
                this.CalcGazeDeviationFromNewFix(x_gaze, y_gaze);

                /*            G1  If the new point falls within the new fix,                */
                if (this.newDifference <= gaze_deviation_threshold)
                {
                  /*                   Update the new fixation hypothesis.                    */
                  this.UpdateNewFixation(time_gaze, x_gaze, y_gaze);

                  /*               H.  If there are now enough points in the new fix          */
                  /*                   to declare it a real fix,                              */
                  if (this.numberNewFixSamples == minimum_fix_samples)
                  {
                    /*                      Drop the present fixation data, move the new        */
                    /*                      new fixation into the present fixation, and see     */
                    /*                      if the new (now present) fixation has enough        */
                    /*                      points to declare the eye to be fixating.           */
                    this.MoveNewFixToPresFix(minimum_fix_samples);
                  }
                }

/*            G2  Otherwise, if the point is outside the new fix,           */
                else   /* if (new_dr <= gaze_deviation_threshold) */
                {
                  /*                   Start the new fixation at the new gazepoint.           */
                  this.StartNewFixAtGazepoint(time_gaze, x_gaze, y_gaze);
                }
              }

/*         F2  Otherwise, If there is not a new fix,                        */
              else   /* if (n_new_fix_counts == 0) */
              {
                /*                Start the new fixation at the gazepoint.                  */
                this.StartNewFixAtGazepoint(time_gaze, x_gaze, y_gaze);
              }
            }
          }
        }

/* B2 Otherwise, if there is not a present fixation,                        */
        else  /* if (iNPresFixSamples = 0) */
        {
          /*       Start the present fixation at the gazepoint and reset the          */
          /*       new fixation.                                                      */
          this.StartPresFixAtGazepoint(time_gaze, x_gaze, y_gaze);
        }
      }

 /*- - - - - - - - - - - - - Process Untracked Eye  - - - - - - - - - - - - -*/

 /* A2 Otherwise, if the eye's gazepoint was not successfully measured       */
      /* this sample,                                                             */
      else   /*  if (gazepoint_found == FALSE)  */
      {
        /*    Increment the number of successive samples with no eye found.         */
        this.numberOfNoEyeFound++;

        /*I   If it has been min-fix-samples since the last sample in the           */
        /*    present fixation,                                                     */
        if ((int)(this.callCount - this.presFixEndCount) >= minimum_fix_samples)
        {
          /*   J   If there had been a fixation prior to loosing track of the eye,    */
          if (this.presFixSamples >= minimum_fix_samples)
          {
            /*          Declare the present fixation to be completed, move the          */
            /*          present fixation to the prior, move the new fixation to         */
            /*          the present, and check if the new (now present) fixation        */
            /*          has enough points for the eye to be declared to be fixating.    */
            this.DeclareCompletedFixation(minimum_fix_samples);
          }

          /*       Reset the present fixation data.                                   */
          this.ResetPresFixation();
        }
      }

      /*---------------------------- Pass Data Back ------------------------------*/

      /* Pass the delayed gazepoint data, with the relevant saccade/fixation      */
      /* data, back to the calling function.                                      */
      ptr_x_gaze_delayed = this.xGazeRing[this.ringIndexDelay];
      ptr_y_gaze_delayed = this.yGazeRing[this.ringIndexDelay];
      ptr_gazepoint_found_delayed = this.gazeFoundRing[this.ringIndexDelay];
      ptr_x_fix_delayed = this.xFixRing[this.ringIndexDelay];
      ptr_y_fix_delayed = this.yFixRing[this.ringIndexDelay];
      ptr_gaze_deviation_delayed = this.gazeDeviationRing[this.ringIndexDelay];
      ptr_saccade_duration_delayed = this.sacDurationRingSamples[this.ringIndexDelay];
      ptr_fix_duration_delayed_samples = this.fixDurationRingSamples[this.ringIndexDelay];
      ptr_fix_duration_delayed_milliseconds = this.fixDurationRingMilliseconds[this.ringIndexDelay];
      ptr_fix_start_time = this.fixStarttimeMilliseconds[this.ringIndexDelay];

      /* Return the eye motion/fixation state for the delayed point.              */
      return this.eyeMotionState[this.ringIndexDelay];
    }

    /// <summary>
    /// This function resets the present fixation, i.e. declares it nonexistent.
    /// </summary>
    private void ResetPresFixation()
    {
      this.timePresFixEndtime = 0;
      this.timePresFixStarttime = 0;
      this.presFixStartCount = 0;
      this.presFixEndCount = 0;
      this.presFixSamples = 0;
      this.presXFixSum = 0.0F;
      this.presYFixSum = 0.0F;
      this.presXFix = 0.0F;
      this.presYFix = 0.0F;
      this.presNumberOut = 0;
    }

    /// <summary>
    /// This function resets the new fixation, i.e. declares it nonexistent.
    /// </summary>
    private void ResetNewFixation()
    {
      this.newFixEndTime = 0;
      this.newFixStartTime = 0;
      this.newFixStartCount = 0;
      this.newFixEndCount = 0;
      this.numberNewFixSamples = 0;
      this.xNewFixSum = 0.0F;
      this.yNewFixSum = 0.0F;
      this.xNewFix = 0.0F;
      this.yNewFix = 0.0F;
    }

    /// <summary>
    /// This function starts the present fixation at the argument gazepoint 
    /// and makes sure there is no new fixation hypothesis.
    /// </summary>
    /// <param name="time_gaze">time of gaze sample in ms</param>
    /// <param name="x_gaze">x gaze sample</param>
    /// <param name="y_gaze">y gaze sample</param>
    private void StartPresFixAtGazepoint(long time_gaze, float x_gaze, float y_gaze)
    {
      /* Start the present fixation at the argument gazepoint.                    */
      this.presFixSamples = 1;
      this.presXFixSum = x_gaze;
      this.presYFixSum = y_gaze;
      this.presXFix = x_gaze;
      this.presYFix = y_gaze;
      this.presFixStartCount = this.callCount;
      this.presFixEndCount = this.callCount;
      this.timePresFixEndtime = time_gaze;
      this.timePresFixStarttime = time_gaze;
      this.presNumberOut = 0;

      /* Make sure there is no new fixation.                                      */
      this.ResetNewFixation();
    }

    /// <summary>
    /// This function starts the new fixation at the argument gazepoint.
    /// </summary>
    /// <param name="time_gaze">time of gaze sample in ms</param>
    /// <param name="x_gaze">x gaze sample</param>
    /// <param name="y_gaze">y gaze sample</param>
    private void StartNewFixAtGazepoint(long time_gaze, float x_gaze, float y_gaze)
    {
      this.numberNewFixSamples = 1;
      this.xNewFixSum = x_gaze;
      this.yNewFixSum = y_gaze;
      this.xNewFix = x_gaze;
      this.yNewFix = y_gaze;
      this.newFixStartCount = this.callCount;
      this.newFixEndCount = this.callCount;
      this.newFixStartTime = time_gaze;
      this.newFixEndTime = time_gaze;
    }

    /// <summary>
    /// This function updates the present fixation with the argument gazepoint,
    /// checks if there are enough samples to declare that the eye is now 
    /// fixating, and makes sure there is no hypothesis for a new fixation.
    /// </summary>
    /// <param name="time_gaze">time of gaze sample in ms</param>
    /// <param name="x_gaze">x gaze sample</param>
    /// <param name="y_gaze">y gaze sample</param>
    /// <param name="minimum_fix_samples">Minimum number of samples that constitute a fixation.
    /// Should depend on sampling rate.</param>
    private void UpdatePresFixation(long time_gaze, float x_gaze, float y_gaze, int minimum_fix_samples)
    {
      /* Update the present fixation with the argument gazepoint.                 */
      this.presFixSamples++;
      this.presXFixSum += x_gaze;
      this.presYFixSum += y_gaze;
      this.presXFix = this.presXFixSum / this.presFixSamples;
      this.presYFix = this.presYFixSum / this.presFixSamples;
      this.presFixEndCount = this.callCount;
      this.presNumberOut = 0;
      this.timePresFixEndtime = time_gaze;

      /* Check if there are enough samples in the present fixation hypothesis     */
      /* to declare that the eye is fixating.                                     */
      this.CheckIfFixating(minimum_fix_samples);

      /* There is no hypothesis for a new fixation.                               */
      this.ResetNewFixation();
    }

    /// <summary>
    /// This function updates the new fixation with the argument gazepoint.
    /// </summary>
    /// <param name="time_gaze">time of gaze sample in ms</param>
    /// <param name="x_gaze">x gaze sample</param>
    /// <param name="y_gaze">y gaze sample</param>
    private void UpdateNewFixation(long time_gaze, float x_gaze, float y_gaze)
    {
      /* Update the new fixation with the argument gazepoint.                     */
      this.numberNewFixSamples++;
      this.xNewFixSum += x_gaze;
      this.yNewFixSum += y_gaze;
      this.xNewFix = this.xNewFixSum / this.numberNewFixSamples;
      this.yNewFix = this.yNewFixSum / this.numberNewFixSamples;
      this.newFixEndCount = this.callCount;
      this.newFixEndTime = time_gaze;
    }

    /// <summary>
    /// This function calculates the deviation of the gazepoint from the 
    /// present fixation location. 
    /// </summary>
    /// <param name="x_gaze">x gaze sample</param>
    /// <param name="y_gaze">y gaze sample</param>
    private void CalcGazeDeviationFromPresFix(float x_gaze, float y_gaze)
    {
      float dx, dy;                   /* horizontal and vertical deviations    */

      dx = x_gaze - this.presXFix;
      dy = y_gaze - this.presYFix;
      this.presDifference = (float)Math.Sqrt(dx * dx + dy * dy);

      /* Put the deviation in the ring buffer for future reference.               */
      this.gazeDeviationRing[this.ringIndex] = this.presDifference;
    }

    /// <summary>
    /// This function calculate the deviation of the gazepoint from the new
    /// fixation location.
    /// </summary>
    /// <param name="x_gaze">x gaze sample</param>
    /// <param name="y_gaze">y gaze sample</param>
    private void CalcGazeDeviationFromNewFix(float x_gaze, float y_gaze)
    {
      float dx, dy;                   /* horizontal and vertical deviations    */

      dx = x_gaze - this.xNewFix;
      dy = y_gaze - this.yNewFix;
      this.newDifference = (float)Math.Sqrt(dx * dx + dy * dy);
    }

    /// <summary>
    /// This function checks to see whether there are enough samples in the
    /// presently hypothesized fixation to declare that the eye is fixating 
    /// yet, and if there is a true fixation going on, it updates the ring
    /// buffers to reflect the fixation.
    /// </summary>
    /// <param name="minimum_fix_samples">Minimum number of samples that constitute a fixation.
    /// Should depend on sampling rate.</param>
    private void CheckIfFixating(int minimum_fix_samples)
    {
      int i, ii;                  /* dummy ring indices                       */

      /* If there are enough samples for a fixation,                              */
      if (this.presFixSamples >= minimum_fix_samples)
      {
        /*    Declare the eye to be fixating.  Go back through the last             */
        /*    minimum_fix_samples entries of the ring buffer making sure that all   */
        /*    samples from the present fixation are marked as fixating, and set     */
        /*    the entries with the newest estimate of the fixation location.        */
        for (i = 0; i < minimum_fix_samples; i++)
        {
          ii = this.ringIndex - i;
          if (ii < 0)
          {
            ii += ringsize;
          }

          this.eyeMotionState[ii] = EyeMotionState.FIXATING;
          this.xFixRing[ii] = this.presXFix;
          this.yFixRing[ii] = this.presYFix;

          this.sacDurationRingSamples[ii] = (int)(this.presFixStartCount -
                                        this.prevFixEndCount - 1);
          this.fixDurationRingSamples[ii] = (int)(this.presFixEndCount -
                                        this.presFixStartCount + 1 - i);
          this.fixDurationRingMilliseconds[ii] = this.timePresFixEndtime - this.timePresFixStarttime;
          this.fixStarttimeMilliseconds[ii] = this.timePresFixStarttime;
        }
      }
    }

    /// <summary>
    /// This function copies the new fixation data into the present fixation,
    /// and resets the new fixation.
    /// </summary>
    /// <param name="minimum_fix_samples">Minimum number of samples that constitute a fixation.
    /// Should depend on sampling rate.</param>
    private void MoveNewFixToPresFix(int minimum_fix_samples)
    {
      /* Move the new fixation to the present fixation.                           */
      this.presFixSamples = this.numberNewFixSamples;
      this.presXFixSum = this.xNewFixSum;
      this.presYFixSum = this.yNewFixSum;
      this.presXFix = this.xNewFix;
      this.presYFix = this.yNewFix;
      this.presFixStartCount = this.newFixStartCount;
      this.presFixEndCount = this.newFixEndCount;
      this.timePresFixEndtime = this.newFixEndTime;
      this.timePresFixStarttime = this.newFixStartTime;
      this.presNumberOut = 0;

      /* Reset the new fixation.                                                  */
      this.ResetNewFixation();

      /* Check if there are enough samples in the new (now present) fixation to   */
      /* declare that the eye is fixating.                                        */
      this.CheckIfFixating(minimum_fix_samples);
    }

    /// <summary>
    /// This function:
    /// a) declares the present fixation to be completed,
    /// b) moves the present fixation to the prior fixation, 
    /// c) moves the new fixation, if any, to the present fixation, and
    /// reset the new fixation
    /// </summary>
    /// <param name="minimum_fix_samples">Minimum number of samples that constitute a fixation.
    /// Should depend on sampling rate.</param>
    private void DeclareCompletedFixation(int minimum_fix_samples)
    {
      /* Declare the present fixation to be completed.                            */
      this.eyeMotionState[this.ringIndexDelay] = EyeMotionState.FIXATION_COMPLETED;

      /* Move the present fixation to the previous fixation.  This saves the      */
      /* end time of the present fixation for later computation of the saccade    */
      /* period between this and the next fixation.                               */
      this.prevFixEndCount = this.presFixEndCount;

      /* Move the new fixation data, if any, to the present fixation, reset       */
      /* the new fixation, and check if there are enough samples in the new       */
      /* (now present) fixation to declare that the eye is fixating.              */
      this.MoveNewFixToPresFix(minimum_fix_samples);
    }

    /// <summary>
    /// This function restores any previous gazepoints that were left out of 
    /// the fixation and are now known to be part of the present fixation. 
    /// </summary>
    private void RestoreOutPoints()
    {
      int i, ii;                  /* dummy ring indices                       */

      /* If there were some previous points that temporarily went out of the      */
      /* fixation region,                                                         */
      if (this.presNumberOut > 0)
      {
        /*    Undo the hypothesis that they were outside the fixation and declare   */
        /*    them now to be part of the fix. */

        // INSERTED IF
        // sometimes there where more points outside of the fixation 
        // than stored in the RING_SIZE BUFFER, skip the first of them.
        if (this.presNumberOut > ringsize)
        {
          this.presNumberOut = ringsize;
        }

        // END INSERTED IF
        for (i = 1; i <= this.presNumberOut; i++)
        {
          ii = this.ringIndex - i;
          if (ii < 0)
          {
            ii += ringsize;
          }

          if (ii >= 0)
          {
            if (this.gazeFoundRing[ii] == true)
            {
              this.presFixSamples++;
              this.presXFixSum += this.xGazeRing[ii];
              this.presYFixSum += this.yGazeRing[ii];
              this.eyeMotionState[ii] = EyeMotionState.FIXATING;
            }
          }
          else
          {
            throw new ArgumentOutOfRangeException();
          }

          // Console.WriteLine("Something not correct in the fixation detection");
        }

        /*    Set the number of "out" points to be zero.                            */
        this.presNumberOut = 0;
      }
    }
    #endregion //METHODS
  }
}
