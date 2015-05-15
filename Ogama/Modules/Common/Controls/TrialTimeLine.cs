// <copyright file="TrialTimeLine.cs" company="FU Berlin">
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

namespace Ogama.Modules.Common.Controls
{
  using System.Collections.Generic;
  using System.ComponentModel;
  using System.Drawing;
  using System.Windows.Forms;

  using Ogama.ExceptionHandling;
  using Ogama.Modules.Common.TrialEvents;

  using OgamaControls;

  /// <summary>
  /// This class inherits <see cref="TimeLine"/> and extends it to
  /// be used in the Ogama trial display mode which subsequences
  /// the timeline by trial events.
  /// </summary>
  public partial class TrialTimeLine : TimeLine
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
    /// This is the zero based index of the slide in the given trial
    /// that is currently active (to be highlighted).
    /// </summary>
    private int highlightedSlideIndex;

    /// <summary>
    /// This holds the <see cref="List{TimeLineEvent}"/> with
    /// the slide events for the current trial.
    /// </summary>
    private List<TimeLineEvent> slideEvents;

    #endregion //FIELDS

    ///////////////////////////////////////////////////////////////////////////////
    // Construction and Initializing methods                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region CONSTRUCTION

    /// <summary>
    /// Initializes a new instance of the TrialTimeLine class.
    /// </summary>
    public TrialTimeLine()
    {
      this.InitializeComponent();
      this.slideEvents = new List<TimeLineEvent>();
      this.highlightedSlideIndex = 0;
    }

    /// <summary>
    /// Initializes a new instance of the TrialTimeLine class.
    /// </summary>
    /// <param name="container">The <see cref="IContainer"/> that is the
    /// container for this element.</param>
    public TrialTimeLine(IContainer container)
    {
      container.Add(this);
      this.InitializeComponent();
      this.slideEvents = new List<TimeLineEvent>();
      this.highlightedSlideIndex = 0;
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
    /// Gets the zero based index of the current highlighted slide in the trial.
    /// </summary>
    public int HighlightedSlideIndex
    {
      get { return this.highlightedSlideIndex; }
    }

    /// <summary>
    /// Sets the <see cref="SortedList{Int32, TrialEvent}"/> of trial
    /// events that should ne displayed in the time line.
    /// </summary>
    public SortedList<int, TrialEvent> TrialEvents
    {
      set
      {
        this.SubmitTrialEventsToTimeline(value);
      }
    }

    /// <summary>
    /// Gets the <see cref="List{TimeLineEvent}"/> of slide
    /// events this time line contains.
    /// </summary>
    public List<TimeLineEvent> SlideEvents
    {
      get
      {
        return this.slideEvents;
      }
    }

    #endregion //PROPERTIES

    ///////////////////////////////////////////////////////////////////////////////
    // Public methods                                                            //
    ///////////////////////////////////////////////////////////////////////////////
    #region PUBLICMETHODS

    /// <summary>
    /// This method sets the first slide of this trial to be visible
    /// and highlighted.
    /// </summary>
    internal void HighlightFirstSlide()
    {
      if (this.slideEvents.Count > 1)
      {
        this.HighlightedTimeLineRange = new TimeLineRange(0, this.slideEvents[1].Time);
      }
      else
      {
        this.HighlightedTimeLineRange = new TimeLineRange(0, 0);
      }

      this.highlightedSlideIndex = 0;
      this.Invalidate();
    }

    /// <summary>
    /// This method sets the next slide of this trial to be visible
    /// and highlighted. The parameter indicates the direction.
    /// </summary>
    /// <param name="forward"><strong>True</strong> if the next slide should be
    /// selected and <strong>false</strong> if the preceding slide should
    /// be selected.</param>
    internal void HighlightNextSlide(bool forward)
    {
      if (this.slideEvents.Count > 0)
      {
        if (forward)
        {
          if (this.highlightedSlideIndex < this.slideEvents.Count - 1)
          {
            int newStartTime = this.slideEvents[this.highlightedSlideIndex + 1].Time;
            int newEndTime = this.Duration;
            if (this.highlightedSlideIndex < this.slideEvents.Count - 2)
            {
              newEndTime = this.slideEvents[this.highlightedSlideIndex + 2].Time;
            }

            this.highlightedSlideIndex++;
            this.HighlightedTimeLineRange = new TimeLineRange(newStartTime, newEndTime);
          }
        }
        else
        {
          if (this.highlightedSlideIndex > 0)
          {
            int newStartTime = this.slideEvents[this.highlightedSlideIndex - 1].Time;
            int newEndTime = this.Duration;
            if (this.highlightedSlideIndex < this.slideEvents.Count)
            {
              newEndTime = this.slideEvents[this.highlightedSlideIndex].Time;
            }

            this.highlightedSlideIndex--;
            this.HighlightedTimeLineRange = new TimeLineRange(newStartTime, newEndTime);
          }
        }
      }
      else
      {
        this.HighlightedTimeLineRange = new TimeLineRange(0, 0);
        this.highlightedSlideIndex = 0;
      }

      this.Invalidate();
    }

    /// <summary>
    /// This method empties the list of events shown by this timeline.
    /// </summary>
    internal void RemoveEvents()
    {
      this.slideEvents.Clear();
      this.HighlightedTimeLineRange = new TimeLineRange(0, 0);
      this.highlightedSlideIndex = 0;
      this.TimeLineEvents.Clear();
      this.Invalidate();
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

    /// <summary>
    /// The <see cref="Control.MouseDown"/> event handler for
    /// the <see cref="TrialTimeLine"/>.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">A <see cref="MouseEventArgs"/> with the event data.</param>
    private void TrialTimeLine_MouseDown(object sender, MouseEventArgs e)
    {
      if (e.Button == MouseButtons.Right)
      {
        TimeLineFilterDialog dialog = new TimeLineFilterDialog();
        dialog.FilterString = this.EventFilterList;
        if (dialog.ShowDialog() == DialogResult.OK)
        {
          this.EventFilterList = dialog.FilterString;
          this.Invalidate();
        }
      }
    }

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
    /// Converts the given <see cref="SortedList{Int32,TrialEvent}"/> to 
    /// <see cref="TimeLineEvent"/> that are displayed in the timeline.
    /// </summary>
    /// <param name="trialEvents">A <see cref="SortedList{Int32,TrialEvent}"/> 
    /// with the trial events to be shown.</param>
    private void SubmitTrialEventsToTimeline(SortedList<int, TrialEvent> trialEvents)
    {
      this.TimeLineEvents.Clear();
      this.TimeLineMarkers.Clear();
      this.slideEvents.Clear();
      this.slideEvents.Add(new TimeLineEvent(0, "Slide", 2f, Color.Blue, TimeLinePosition.Above));

      if (trialEvents.Count > 30 && !this.EventFilterList.Contains("MouseUp"))
      {
        this.EventFilterList.Add("Scroll");
        this.EventFilterList.Add("MouseUp");
        this.EventFilterList.Add("MouseDown");
        this.EventFilterList.Add("KeyDown");
        this.EventFilterList.Add("KeyUp");
        string message = "To provide acceptable performance OGAMA now hides the mouse and scroll events in the time line " +
          " at the bottom of the module. You may reactivate them by right-clicking on this timeline.";
        ExceptionMethods.ProcessMessage("Lots of events in that trial ...", message);
      }

      foreach (TrialEvent trialEvent in trialEvents.Values)
      {
        TimeLineEvent newTimeLineEvent = new TimeLineEvent();
        newTimeLineEvent.Time = (int)trialEvent.Time;
        switch (trialEvent.Type)
        {
          case EventType.Mouse:
            newTimeLineEvent.Position = TimeLinePosition.Below;
            newTimeLineEvent.StrokeWidth = 1f;
            newTimeLineEvent.StrokeColor = Color.Yellow;
            if (((InputEvent)trialEvent).Task == InputEventTask.Up)
            {
              newTimeLineEvent.ImageKey = "MouseUp";
            }
            else if (((InputEvent)trialEvent).Task == InputEventTask.Down)
            {
              newTimeLineEvent.ImageKey = "MouseDown";
            }

            this.TimeLineEvents.Add(newTimeLineEvent);
            break;
          case EventType.Key:
            newTimeLineEvent.Position = TimeLinePosition.Below;
            newTimeLineEvent.StrokeWidth = 1f;
            newTimeLineEvent.StrokeColor = Color.YellowGreen;
            newTimeLineEvent.ImageKey = "Key";
            this.TimeLineEvents.Add(newTimeLineEvent);
            break;
          case EventType.Slide:
            newTimeLineEvent.Position = TimeLinePosition.Above;
            newTimeLineEvent.StrokeWidth = 2f;
            newTimeLineEvent.StrokeColor = Color.Blue;
            newTimeLineEvent.ImageKey = "Slide";
            this.TimeLineEvents.Add(newTimeLineEvent);
            this.slideEvents.Add(newTimeLineEvent);
            break;
          case EventType.Flash:
            newTimeLineEvent.Position = TimeLinePosition.Above;
            newTimeLineEvent.StrokeWidth = 2f;
            newTimeLineEvent.StrokeColor = Color.Magenta;
            newTimeLineEvent.ImageKey = "Flash";
            this.TimeLineEvents.Add(newTimeLineEvent);
            break;
          case EventType.Audio:
            newTimeLineEvent.Position = TimeLinePosition.Below;
            newTimeLineEvent.StrokeWidth = 1f;
            newTimeLineEvent.StrokeColor = Color.Orange;
            newTimeLineEvent.ImageKey = "Sound";
            this.TimeLineEvents.Add(newTimeLineEvent);
            break;
          case EventType.Video:
            break;
          case EventType.Webpage:
            break;
          case EventType.Usercam:
            break;
          case EventType.Response:
            newTimeLineEvent.Position = TimeLinePosition.Above;
            newTimeLineEvent.StrokeWidth = 2f;
            newTimeLineEvent.StrokeColor = Color.Blue;
            newTimeLineEvent.ImageKey = "Slide";
            this.TimeLineEvents.Add(newTimeLineEvent);
            break;
          case EventType.Marker:
            TimeLineMarker newTimeLineMarker = new TimeLineMarker();
            newTimeLineMarker.Time = (int)trialEvent.Time;
            newTimeLineMarker.EventID = (int)trialEvent.EventID;
            newTimeLineMarker.MarkerColor = Color.Yellow;
            this.TimeLineMarkers.Add(newTimeLineMarker);
            break;
          case EventType.Scroll:
            newTimeLineEvent.Position = TimeLinePosition.Above;
            newTimeLineEvent.StrokeWidth = 0f;
            newTimeLineEvent.StrokeColor = Color.Transparent;
            newTimeLineEvent.ImageKey = "Scroll";
            this.TimeLineEvents.Add(newTimeLineEvent);
            break;
        }
      }

      this.Invalidate();
    }

    #endregion //PRIVATEMETHODS

    ///////////////////////////////////////////////////////////////////////////////
    // Small helping Methods                                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region HELPER
    #endregion //HELPER
  }
}
