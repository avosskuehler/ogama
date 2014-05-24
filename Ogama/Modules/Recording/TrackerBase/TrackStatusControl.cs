// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TrackStatusControl.cs" company="Freie Universität Berlin">
//   OGAMA - open gaze and mouse analyzer 
//   Copyright (C) 2014 Dr. Adrian Voßkühler  
//   Licensed under GPL V3
// </copyright>
// <author>Adrian Voßkühler</author>
// <email>adrian@ogama.net</email>
// <summary>
//   The tobii track status control.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Ogama.Modules.Recording.TrackerBase
{
  using System;
  using System.Collections.Generic;
  using System.Drawing;
  using System.Windows.Forms;
  using System.Windows.Media;

  using Ogama.Modules.AttentionMap;

  using Color = System.Drawing.Color;
  using LinearGradientBrush = System.Drawing.Drawing2D.LinearGradientBrush;

  /// <summary>
  ///   The tobii track status control.
  /// </summary>
  public partial class TrackStatusControl : UserControl
  {
    #region Constants

    /// <summary>
    ///   The bar height.
    /// </summary>
    private const int Barheight = 25;

    /// <summary>
    ///   The eye radius.
    /// </summary>
    private const int Eyeradius = 8;

    /// <summary>
    ///   The history size.
    /// </summary>
    private const int Historysize = 30;

    #endregion

    #region Fields

    /// <summary>
    ///   The brush.
    /// </summary>
    private readonly SolidBrush brush;

    /// <summary>
    ///   The data history.
    /// </summary>
    private readonly Queue<TrackStatusData> dataHistory;

    /// <summary>
    ///   The eye brush.
    /// </summary>
    private readonly SolidBrush eyeBrush;

    /// <summary>
    ///   The newest <see cref="TrackStatusData" />
    /// </summary>
    private TrackStatusData currentTrackStatus;

    private GradientStopCollection statusGradient;

    #endregion

    #region Constructors and Destructors

    /// <summary>
    ///   Initializes a new instance of the <see cref="TrackStatusControl" /> class.
    /// </summary>
    public TrackStatusControl()
    {
      this.InitializeComponent();

      this.SetStyle(ControlStyles.UserPaint, true);
      this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
      this.SetStyle(ControlStyles.DoubleBuffer, true);

      statusGradient = new GradientStopCollection(3);
      statusGradient.Add(new GradientStop(Colors.Red, 0));
      statusGradient.Add(new GradientStop(Colors.Lime, 1));

      this.dataHistory = new Queue<TrackStatusData>(Historysize);

      this.brush = new SolidBrush(Color.Red);
      this.eyeBrush = new SolidBrush(Color.White);
    }

    #endregion

    #region Properties

    #endregion

    #region Public Methods and Operators

    /// <summary>
    ///   Clears the track status.
    /// </summary>
    public void Clear()
    {
      this.dataHistory.Clear();
      this.currentTrackStatus = new TrackStatusData();
      this.Invalidate();
    }

    /// <summary>
    /// Called when new track status data is received
    /// </summary>
    /// <param name="data"> The track status data to be displayed in this control. </param>
    public void OnTrackStatusData(TrackStatusData data)
    {
      // Add data to history
      this.dataHistory.Enqueue(data);

      // Remove history item if necessary
      while (this.dataHistory.Count > Historysize)
      {
        this.dataHistory.Dequeue();
      }

      this.currentTrackStatus = data;

      this.Invalidate();
    }

    #endregion

    #region Methods

    /// <summary>
    /// Raises the <see cref="E:System.Windows.Forms.Control.Paint" /> event.
    /// Paints the control representing the track status of one or both eyes.
    /// </summary>
    /// <param name="e">A <see cref="T:System.Windows.Forms.PaintEventArgs" /> that contains the event data.</param>
    protected override void OnPaint(PaintEventArgs e)
    {
      base.OnPaint(e);

      // Compute status bar color
      this.brush.Color = this.ComputeStatusColor();

      // Draw bottom bar
      e.Graphics.FillRectangle(this.brush, new Rectangle(0, this.Height - Barheight, this.Width, Barheight));

      // Draw eyes
      switch (this.currentTrackStatus.TrackedEyes)
      {
        case Eye.None:
          break;
        case Eye.Left:
          this.DrawLeftEye(e);
          break;
        case Eye.Right:
          this.DrawRightEye(e);
          break;
        case Eye.Both:
          this.DrawLeftEye(e);
          this.DrawRightEye(e);
          break;
      }
    }

    /// <summary>
    /// Draws the left eye.
    /// </summary>
    /// <param name="e">The <see cref="PaintEventArgs"/> instance containing the event data.</param>
    private void DrawLeftEye(PaintEventArgs e)
    {
      if (this.currentTrackStatus.LeftEyeValidity >= Validity.Problematic)
      {
        var r = new RectangleF(
          (float)((1.0 - this.currentTrackStatus.LeftEyePosition.X) * this.Width - Eyeradius),
          (float)(this.currentTrackStatus.LeftEyePosition.Y * (this.Height - Barheight) - Eyeradius),
          2 * Eyeradius,
          2 * Eyeradius);
        e.Graphics.FillEllipse(this.eyeBrush, r);
      }
    }

    /// <summary>
    /// Draws the right eye.
    /// </summary>
    /// <param name="e">The <see cref="PaintEventArgs"/> instance containing the event data.</param>
    private void DrawRightEye(PaintEventArgs e)
    {
      if (this.currentTrackStatus.RightEyeValidity >= Validity.Problematic)
      {
        var r = new RectangleF(
          (float)((1.0 - this.currentTrackStatus.RightEyePosition.X) * this.Width - Eyeradius),
          (float)(this.currentTrackStatus.RightEyePosition.Y * (this.Height - Barheight) - Eyeradius),
          2 * Eyeradius,
          2 * Eyeradius);
        e.Graphics.FillEllipse(this.eyeBrush, r);
      }
    }

    /// <summary>
    ///   This method computes the status color.
    /// </summary>
    /// <returns>The <see cref="Color" /> indicating the tracking quality.</returns>
    private Color ComputeStatusColor()
    {
      if (!this.Enabled)
      {
        return Color.Gray;
      }

      int quality = 0;
      int count = 0;

      var historyClone = new TrackStatusData[Historysize];

      this.dataHistory.CopyTo(historyClone, 0);

      foreach (TrackStatusData item in historyClone)
      {
        switch (item.TrackedEyes)
        {
          case Eye.None:
            break;
          case Eye.Left:
            if (item.LeftEyeValidity == Validity.Good)
            {
              quality += 2;
            }
            else if (item.LeftEyeValidity == Validity.Problematic)
            {
              quality++;
            }

            break;
          case Eye.Right:
            if (item.RightEyeValidity == Validity.Good)
            {
              quality += 2;
            }
            else if (item.RightEyeValidity == Validity.Problematic)
            {
              quality++;
            }

            break;
          case Eye.Both:
            if (item.LeftEyeValidity == Validity.Good && item.RightEyeValidity == Validity.Good)
            {
              quality += 2;
            }
            else if (item.LeftEyeValidity == Validity.Problematic && item.RightEyeValidity == Validity.Problematic)
            {
              quality++;
            }

            break;
        }

        count++;
      }

      float q = count == 0 ? 0 : quality / (2F * count);

      return this.statusGradient.GetRelativeColor(q);
    }

    #endregion
  }
}