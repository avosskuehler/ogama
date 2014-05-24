// <copyright file="AttentionMapModule.cs" company="FU Berlin">
// ******************************************************
// OGAMA - open gaze and mouse analyzer 
// Copyright (C) 2013 Dr. Adrian Voßkühler  
// ------------------------------------------------------------------------
// This program is free software; you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation; either version 2 of the License, or (at your option) any later version.
// This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
// You should have received a copy of the GNU General Public License along with this program; if not, write to the Free Software Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA 02111-1307 USA
// **************************************************************
// </copyright>
// <author>Adrian Voßkühler</author>
// <email>adrian@ogama.net</email>

namespace Ogama.Modules.AttentionMap
{
  using System;
  using System.Collections.Generic;
  using System.ComponentModel;
  using System.Data;
  using System.Drawing;
  using System.Windows.Forms;

  using Ogama.ExceptionHandling;
  using Ogama.MainWindow;
  using Ogama.Modules.Common.FormTemplates;
  using Ogama.Modules.Common.Tools;
  using Ogama.Modules.Common.TrialEvents;
  using Ogama.Modules.Common.Types;
  using Ogama.Properties;
  using OgamaControls;

  using VectorGraphics.StopConditions;

  /// <summary>
  /// Derived from <see cref="FormWithTrialSelection"/>.
  /// This <see cref="Form"/> is the class for the attention map module. 
  /// This class handles the UI and the database connection for
  /// the <see cref="AttentionMapPicture"/> class, which is the main element
  /// of this form.
  /// </summary>
  /// <remarks>This module is intended to calculate gaussian distributions of the
  /// fixational data and overlay it on the output stimulus image, so that
  /// you can see a landscape of visited and unvisited locations on the stimulus.
  /// You can choose the subjects to include in the calculation and whether 
  /// the calculation should be based only on a special or all fixations.</remarks>
  public partial class AttentionMapModule : FormWithTrialSelection
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
    /// Initializes a new instance of the AttentionMapModule class.
    /// </summary>
    public AttentionMapModule()
    {
      this.InitializeComponent();

      this.Picture = this.attentionMapPicture;
      this.TrialCombo = this.cbbTrial;
      this.ZoomTrackBar = this.trbZoom;

      this.InitializeDropDowns();
      this.InitializeDataBindings();
      this.InitAccelerators();
      this.InitializeCustomElements();
    }

    #endregion //CONSTRUCTION

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
    /// Causes the controls bound to the BindingSources to reread all the 
    /// items in the list and refresh their displayed values. 
    /// </summary>
    public override void ResetDataBindings()
    {
      base.ResetDataBindings();
      PopulateSubjectTreeView(this.trvSubjects);
    }

    /// <summary>
    /// Initializes accelerator keys. Binds to methods.
    /// </summary>
    protected override void InitAccelerators()
    {
      base.InitAccelerators();
      SetAccelerator(Keys.Escape, new AcceleratorAction(this.OnEscape));
    }

    /// <summary>
    /// This methods is used to initialize elements that are not
    /// initialized in the designer.
    /// </summary>
    protected override void InitializeCustomElements()
    {
      base.InitializeCustomElements();
      PopulateSubjectTreeView(this.trvSubjects);
      this.btnHelp.Click += new EventHandler(this.btnHelp_Click);
      this.pnlCanvas.Resize += new EventHandler(this.pnlCanvas_Resize);
    }

    /// <summary>
    /// Reads dropdown settings and loads corresponding images and data from database.
    /// Then notifys picture the changes.
    /// </summary>
    /// <returns><strong>True</strong> if selection was successful, otherwise <strong>false</strong>.
    /// </returns>
    protected override bool NewTrialSelected()
    {
      try
      {
        // Reset picture
        this.attentionMapPicture.ResetPicture();

        // Read current selection state
        int trialID = Document.ActiveDocument.SelectionState.TrialID;

        // Switch to WaitCursor
        Cursor = Cursors.WaitCursor;

        // Read settings
        ExperimentSettings set = Document.ActiveDocument.ExperimentSettings;
        if (set != null)
        {
          // Load trial stimulus into picture
          if (!this.LoadTrialStimulus(trialID))
          {
            return false;
          }

          this.LoadTrialSlidesIntoTimeline(this.trialTimeLine);
        }

        // Reset data state label
        ((MainForm)this.MdiParent).StatusRightLabel.Text = string.Empty;
      }
      catch (Exception ex)
      {
        ExceptionMethods.HandleException(ex);
      }
      finally
      {
        // Reset Cursor
        Cursor = Cursors.Default;
      }

      return true;
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

    /// <summary>
    /// <see cref="Form.Load"/> event handler. 
    /// Wires Mainform events and initializes vector graphics picture.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void AttentionMapModule_Load(object sender, EventArgs e)
    {
      try
      {
        this.attentionMapPicture.Gradient = AttentionMaps.TrafficLight;
        this.gradientControl.Gradient = AttentionMaps.TrafficLight;

        // Hide custom gradient panel
        this.spcPictureGradient.Panel2Collapsed = true;

        this.attentionMapPicture.PresentationSize = Document.ActiveDocument.PresentationSize;
        this.ResizeCanvas();

        this.InitialDisplay();
      }
      catch (Exception ex)
      {
        ExceptionMethods.HandleException(ex);
      }
    }

    /// <summary>
    /// The <see cref="TreeView.AfterCheck"/> event handler for the
    /// <see cref="TreeView"/> <see cref="trvSubjects"/>.
    /// Checks or unchecks all subjects in the category node
    /// that is clicked.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void trvSubjects_AfterCheck(object sender, TreeViewEventArgs e)
    {
      if (e.Node.ImageKey == "Category")
      {
        foreach (TreeNode subjectNode in e.Node.Nodes)
        {
          subjectNode.Checked = e.Node.Checked;
        }
      }
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler for the
    /// <see cref="Button"/> <see cref="btnAllFix"/>.
    /// User pressed "calculate all fixations" button.
    /// So deactivate "calculate first fixation" button.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void btnAllFix_Click(object sender, EventArgs e)
    {
      this.btnSpecialFix.Checked = !this.btnAllFix.Checked;
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler for the
    /// <see cref="Button"/> <see cref="btnSpecialFix"/>.
    /// User pressed "calculate first fixation" button.
    /// So deactivate "calculate all fixations" button.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void btnSpecialFix_Click(object sender, EventArgs e)
    {
      this.btnAllFix.Checked = !this.btnSpecialFix.Checked;
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler for the
    /// <see cref="Button"/> <see cref="btnEye"/>.
    /// Switches the calculation basis to gaze fixation data.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void btnEye_Click(object sender, EventArgs e)
    {
      if (this.btnEye.Checked)
      {
        this.btnMouse.Checked = false;
        this.btnMouseClicks.Checked = false;
        this.btnWeight.Enabled = true;
      }
      else
      {
        this.btnMouse.Checked = true;
        this.btnMouseClicks.Checked = false;
        this.btnWeight.Enabled = true;
      }
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler for the
    /// <see cref="Button"/> <see cref="btnMouse"/>.
    /// Switches the calculation basis to mouse fixation data.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void btnMouse_Click(object sender, EventArgs e)
    {
      if (this.btnMouse.Checked)
      {
        this.btnEye.Checked = false;
        this.btnMouseClicks.Checked = false;
        this.btnWeight.Enabled = true;
      }
      else
      {
        this.btnEye.Checked = true;
        this.btnMouseClicks.Checked = false;
        this.btnWeight.Enabled = true;
      }
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler for the
    /// <see cref="Button"/> <see cref="btnMouseClicks"/>.
    /// Switches the calculation basis to mouse click data.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void btnMouseClicks_Click(object sender, EventArgs e)
    {
      if (this.btnMouseClicks.Checked)
      {
        this.btnEye.Checked = false;
        this.btnMouse.Checked = false;
        this.btnWeight.Enabled = false;
        this.btnWeight.Checked = false;
      }
      else
      {
        this.btnEye.Checked = true;
        this.btnMouse.Checked = false;
        this.btnWeight.Enabled = true;
      }
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler for the
    /// <see cref="Button"/> <see cref="btnSeekNextSlide"/>.
    /// Activate next slide in trial.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void btnSeekNextSlide_Click(object sender, EventArgs e)
    {
      // Skip if there is no data
      if (this.CurrentTrial == null)
      {
        return;
      }

      this.trialTimeLine.HighlightNextSlide(true);
      int slideIndex = this.trialTimeLine.HighlightedSlideIndex;
      this.LoadSlide(this.CurrentTrial[slideIndex], ActiveXMode.BehindPicture);
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler for the
    /// <see cref="Button"/> <see cref="btnSeekPreviousSlide"/>.
    /// Activate previous slide in trial.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void btnSeekPreviousSlide_Click(object sender, EventArgs e)
    {
      // Skip if there is no data
      if (this.CurrentTrial == null)
      {
        return;
      }

      this.trialTimeLine.HighlightNextSlide(false);
      int slideIndex = this.trialTimeLine.HighlightedSlideIndex;
      this.LoadSlide(this.CurrentTrial[slideIndex], ActiveXMode.BehindPicture);
    }

    /// <summary>
    /// The <see cref="NumericUpDown.ValueChanged"/> event handler for the
    /// <see cref="NumericUpDown"/> <see cref="nudKernelSize"/>.
    /// Resizes the gaussian kernel of the <see cref="AttentionMaps"/> class.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void nudKernelSize_ValueChanged(object sender, EventArgs e)
    {
      int size = (int)this.nudKernelSize.Value;

      // Only odd numbers are allowed
      if ((size % 2) == 0)
      {
        size += 1;
        this.nudKernelSize.Value = (decimal)size;
      }

      AttentionMaps.KernelSize = size;
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler for the
    /// <see cref="Button"/> <see cref="btnStartCalculation"/>.
    /// User pressed "Start calculation" button. So run the 
    /// backgroundworker to do the job if it is not busy.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void btnStartCalculation_Click(object sender, EventArgs e)
    {
      if (!this.bgwCalcMap.IsBusy)
      {
        this.StartCalculation();
      }
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler for the
    /// <see cref="ToolStripMenuItem"/> <see cref="mnuShowGradientBuilder"/>.
    /// User switched visibility of gradient control, so
    /// collapse or expand its splitcontainer panel.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void mnuShowGradientBuilder_Click(object sender, EventArgs e)
    {
      this.spcPictureGradient.Panel2Collapsed = !this.mnuShowGradientBuilder.Checked;
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler for the
    /// <see cref="ToolStripMenuItem"/> <see cref="mnuTrafficLight"/>.
    /// User selected traffic light predefined gradient from gradient drop down. 
    /// So update attention map pictures gradient.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void mnuTrafficLight_Click(object sender, EventArgs e)
    {
      this.attentionMapPicture.Gradient = AttentionMaps.TrafficLight;
      this.gradientControl.Gradient = AttentionMaps.TrafficLight;
      if (this.mnuTrafficLight.Checked)
      {
        this.mnuCustomGradient.Checked = false;
        this.mnuRainbow.Checked = false;
        this.mnuTrafficLight.Checked = false;
        this.mnuBlackMask.Checked = true;
      }
      else
      {
        this.mnuCustomGradient.Checked = false;
        this.mnuRainbow.Checked = false;
        this.mnuTrafficLight.Checked = true;
        this.mnuBlackMask.Checked = false;
      }
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler for the
    /// <see cref="ToolStripMenuItem"/> <see cref="mnuRainbow"/>.
    /// User selected rainbow predefined gradient from gradient drop down. 
    /// So update attention map pictures gradient.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void mnuRainbow_Click(object sender, EventArgs e)
    {
      this.attentionMapPicture.Gradient = AttentionMaps.Rainbow;
      this.gradientControl.Gradient = AttentionMaps.Rainbow;
      if (this.mnuRainbow.Checked)
      {
        this.mnuCustomGradient.Checked = false;
        this.mnuRainbow.Checked = false;
        this.mnuTrafficLight.Checked = true;
        this.mnuBlackMask.Checked = false;
      }
      else
      {
        this.mnuCustomGradient.Checked = false;
        this.mnuRainbow.Checked = true;
        this.mnuTrafficLight.Checked = false;
        this.mnuBlackMask.Checked = false;
      }
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler for the
    /// <see cref="ToolStripMenuItem"/> <see cref="mnuBlackMask"/>.
    /// User selected black mask predefined gradient from gradient drop down. 
    /// So update attention map pictures gradient.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void mnuBlackMask_Click(object sender, EventArgs e)
    {
      this.attentionMapPicture.Gradient = AttentionMaps.BlackMask;
      this.gradientControl.Gradient = AttentionMaps.BlackMask;
      if (this.mnuBlackMask.Checked)
      {
        this.mnuCustomGradient.Checked = false;
        this.mnuRainbow.Checked = false;
        this.mnuTrafficLight.Checked = true;
        this.mnuBlackMask.Checked = false;
      }
      else
      {
        this.mnuCustomGradient.Checked = false;
        this.mnuRainbow.Checked = false;
        this.mnuTrafficLight.Checked = false;
        this.mnuBlackMask.Checked = true;
      }
    }

    /// <summary>
    /// The <see cref="Control.Click"/> event handler for the
    /// <see cref="ToolStripMenuItem"/> <see cref="mnuCustomGradient"/>.
    /// User selected custom gradient from gradient drop down. 
    /// So show gradient builder.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void mnuCustomGradient_Click(object sender, EventArgs e)
    {
      this.spcPictureGradient.Panel2Collapsed = false;
      this.mnuShowGradientBuilder.Checked = true;
      this.attentionMapPicture.Gradient = AttentionMaps.Custom;
      this.gradientControl.Gradient = AttentionMaps.Custom;
      if (this.mnuCustomGradient.Checked)
      {
        this.mnuCustomGradient.Checked = false;
        this.mnuRainbow.Checked = false;
        this.mnuTrafficLight.Checked = true;
        this.mnuBlackMask.Checked = false;
      }
      else
      {
        this.mnuCustomGradient.Checked = true;
        this.mnuRainbow.Checked = false;
        this.mnuTrafficLight.Checked = false;
        this.mnuBlackMask.Checked = false;
      }
    }

    /// <summary>
    /// The <see cref="Control.Paint"/> event handler for the
    /// <see cref="ToolStripMenuItem"/> <see cref="mnuTrafficLight"/>.
    /// Fills background of menu entry with the traffic light
    /// predefined gradient.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">The <see cref="PaintEventArgs"/> with the event data.</param>
    private void mnuTrafficLight_Paint(object sender, PaintEventArgs e)
    {
      Rectangle paintRect = e.ClipRectangle;
      paintRect.X += 25;
      AttentionMaps.TrafficLight.FillRectangle(e.Graphics, paintRect);
    }

    /// <summary>
    /// The <see cref="Control.Paint"/> event handler for the
    /// <see cref="ToolStripMenuItem"/> <see cref="mnuRainbow"/>.
    /// Fills background of menu entry with the rainbow 
    /// predefined gradient.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">The <see cref="PaintEventArgs"/> with the event data.</param>
    private void mnuRainbow_Paint(object sender, PaintEventArgs e)
    {
      Rectangle paintRect = e.ClipRectangle;
      paintRect.X += 25;
      AttentionMaps.Rainbow.FillRectangle(e.Graphics, paintRect);
    }

    /// <summary>
    /// The <see cref="Control.Paint"/> event handler for the
    /// <see cref="ToolStripMenuItem"/> <see cref="mnuBlackMask"/>.
    /// Fills background of menu entry with the black mask 
    /// predefined gradient.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">The <see cref="PaintEventArgs"/> with the event data.</param>
    private void mnuBlackMask_Paint(object sender, PaintEventArgs e)
    {
      Rectangle paintRect = e.ClipRectangle;
      paintRect.X += 25;
      AttentionMaps.BlackMask.FillRectangle(e.Graphics, paintRect);
    }

    /// <summary>
    /// The <see cref="Control.Paint"/> event handler for the
    /// <see cref="ToolStripMenuItem"/> <see cref="mnuCustomGradient"/>.
    /// Fills background of menu entry with the custom gradient.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">The <see cref="PaintEventArgs"/> with the event data.</param>
    private void mnuCustomGradient_Paint(object sender, PaintEventArgs e)
    {
      Rectangle paintRect = e.ClipRectangle;
      paintRect.X += 25;
      AttentionMaps.Custom.FillRectangle(e.Graphics, paintRect);
    }

    #endregion //WINDOWSEVENTHANDLER

    ///////////////////////////////////////////////////////////////////////////////
    // Eventhandler for Custom Defined Events                                    //
    ///////////////////////////////////////////////////////////////////////////////
    #region CUSTOMEVENTHANDLER

    /// <summary>
    /// Cancels background thread when escape key is pressed.
    /// </summary>
    private void OnEscape()
    {
      this.bgwCalcMap.CancelAsync();
    }

    /// <summary>
    /// The <see cref="GradientTypeEditorUI.GradientChanged"/> event handler for the
    /// <see cref="GradientTypeEditorUI"/> <see cref="gradientControl"/>.
    /// This method handles the gradient changed event from the gradient control
    /// by resetting the pictures gradient property.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">An empty <see cref="EventArgs"/></param>
    private void gradientControl_GradientChanged(object sender, EventArgs e)
    {
      this.attentionMapPicture.Gradient = this.gradientControl.Gradient;
    }

    #endregion //CUSTOMEVENTHANDLER

    #endregion //EVENTS

    ///////////////////////////////////////////////////////////////////////////////
    // Methods and Eventhandling for Background tasks                            //
    ///////////////////////////////////////////////////////////////////////////////
    #region BACKGROUNDWORKER

    /// <summary>
    /// The <see cref="BackgroundWorker.DoWork"/> event handler for the
    /// <see cref="BackgroundWorker"/> <see cref="bgwCalcMap"/>.
    /// Background worker thread working method for calculating
    /// the attention map according to settings in UI.
    /// </summary>
    /// <param name="sender">Source of the event</param>
    /// <param name="e">A <see cref="DoWorkEventArgs"/> with the event data.</param>
    private void bgwCalcMap_DoWork(object sender, DoWorkEventArgs e)
    {
      // Get the BackgroundWorker that raised this event.
      BackgroundWorker worker = sender as BackgroundWorker;
      bool weighted = (bool)e.Argument;

      // Start Calculation
      if (weighted)
      {
        this.attentionMapPicture.CalculateWeightedAttentionMap(worker, e);
      }
      else
      {
        this.attentionMapPicture.CalculateUnweightedAttentionMap(worker, e);
      }
    }

    /// <summary>
    /// The <see cref="BackgroundWorker.RunWorkerCompleted"/> event handler for the
    /// <see cref="BackgroundWorker"/> <see cref="bgwCalcMap"/>.
    /// This event handler deals with the results of the
    /// background operation and updates the UI.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">A <see cref="RunWorkerCompletedEventArgs"/> with the event data.</param>
    private void bgwCalcMap_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
    {
      // First, handle the case where an exception was thrown.
      if (e.Error != null)
      {
        ExceptionMethods.HandleException(e.Error);
      }
      else if (e.Cancelled)
      {
        ((MainForm)this.MdiParent).StatusLabel.Text = "Status: Attentionmap calculation cancelled.";
      }
      else
      {
        // Finally, handle the case where the operation succeeded.
        ((MainForm)this.MdiParent).StatusLabel.Text = "Ready";
      }

      ((MainForm)this.MdiParent).StatusProgressbar.Value = 0;

      // Enable the Start button.
      this.btnStartCalculation.Enabled = true;

      // Redraw Image
      this.Refresh();
    }

    /// <summary>
    /// The <see cref="BackgroundWorker.ProgressChanged"/> event handler for the
    /// <see cref="BackgroundWorker"/> <see cref="bgwCalcMap"/>.
    /// This event handler updates the progress bar of the main form.
    /// </summary>
    /// <param name="sender">Source of the event</param>
    /// <param name="e">A <see cref="ProgressChangedEventArgs"/> with the event data</param>
    private void bgwCalcMap_ProgressChanged(object sender, ProgressChangedEventArgs e)
    {
      ((MainForm)this.MdiParent).StatusLabel.Text = (string)e.UserState;
      ((MainForm)this.MdiParent).StatusProgressbar.Value = e.ProgressPercentage;
    }

    #endregion //BACKGROUNDWORKER

    ///////////////////////////////////////////////////////////////////////////////
    // Methods for doing main class job                                          //
    ///////////////////////////////////////////////////////////////////////////////
    #region METHODS

    /// <summary>
    /// Calculates attention map according to settings in UI.
    /// </summary>
    /// <remarks>Updates data table of picture and starts background working thread.</remarks>
    private void StartCalculation()
    {
      try
      {
        this.btnStartCalculation.Enabled = false;

        // Read ComboBoxes
        int trialID = Document.ActiveDocument.SelectionState.TrialID;
        int trialSequence = Document.ActiveDocument.SelectionState.TrialSequence;

        // Check if any subject is selected
        if (this.trvSubjects.Nodes.Count == 0)
        {
          this.btnStartCalculation.Enabled = true;
          return;
        }

        List<string> checkedSubjects = GetCheckedSubjects(this.trvSubjects);

        if (checkedSubjects.Count > 0)
        {
          DataTable table = null;

          if (this.btnMouseClicks.Checked)
          {
            table = this.GetFilteredMouseClicks(trialID, checkedSubjects);
          }
          else
          {
            // btnEye or btnMouse is checked
            table = this.GetFilteredFixations(trialID, checkedSubjects);
          }

          if (table.Rows.Count == 0)
          {
            string message = "For this settings no data is available." +
              Environment.NewLine +
              "If using fixations to be the base for calculation, make sure, that you calculated the fixations in the fixation module." +
              Environment.NewLine + "Otherwise (when using mouse clicks based attention maps) there occured no mouse clicks in this trial.";

            InformationDialog.Show(
              "No data available",
              message,
              false,
              MessageBoxIcon.Information);
            this.btnStartCalculation.Enabled = true;
          }
          else
          {
            this.attentionMapPicture.AttentionMapTable = table;

            // Start Calculation in background
            this.bgwCalcMap.RunWorkerAsync(this.btnWeight.Checked);
          }
        }
        else
        {
          this.btnStartCalculation.Enabled = true;
        }
      }
      catch (Exception ex)
      {
        this.btnStartCalculation.Enabled = true;
        ExceptionMethods.HandleException(ex);
      }
    }

    /// <summary>
    /// This method returns the mouse clicks for the list of subjects
    /// on the trial with the given trial id.
    /// </summary>
    /// <param name="trialID">An <see cref="Int32"/> with the trial id.</param>
    /// <param name="checkedSubjects">A <see cref="List{String}"/> with the subject names.</param>
    /// <returns>A <see cref="Ogama.DataSet.OgamaDataSet.MouseFixationsDataTable"/>
    /// with the mouse clicks.</returns>
    private DataTable GetFilteredMouseClicks(int trialID, List<string> checkedSubjects)
    {
      Ogama.DataSet.OgamaDataSet.MouseFixationsDataTable clickTable =
        new Ogama.DataSet.OgamaDataSet.MouseFixationsDataTable();

      foreach (string entry in checkedSubjects)
      {
        // Ignore trials marked to be eliminated in analysis.
        DataTable currentTrial =
          Document.ActiveDocument.DocDataSet.TrialsAdapter.GetDataBySubjectAndTrialID(entry, trialID);
        if (currentTrial.Rows.Count > 0)
        {
          if (currentTrial.Rows[0]["EliminateData"] != null && currentTrial.Rows[0]["EliminateData"].ToString() != string.Empty)
          {
            continue;
          }
          else
          {
            int trialSequence = (int)currentTrial.Rows[0]["TrialSequence"];
            DataTable trialEvents =
              Document.ActiveDocument.DocDataSet.TrialEventsAdapter.GetDataBySubjectAndSequence(entry, trialSequence);

            foreach (DataRow trialEvent in trialEvents.Rows)
            {
              string typeString = trialEvent["EventType"].ToString();
              EventType type = EventType.None;
              try
              {
                type = (EventType)Enum.Parse(typeof(EventType), typeString, true);
              }
              catch (ArgumentException)
              {
                continue;
              }

              if (type == EventType.Mouse)
              {
                string taskString = trialEvent["EventTask"].ToString();
                string paramString = trialEvent["EventParam"].ToString();

                MouseStopCondition msc = (MouseStopCondition)TypeDescriptor.GetConverter(typeof(StopCondition)).ConvertFrom(paramString);
                InputEventTask task = (InputEventTask)Enum.Parse(typeof(InputEventTask), taskString, true);
                switch (task)
                {
                  case InputEventTask.Down:
                    Ogama.DataSet.OgamaDataSet.MouseFixationsRow newRow =
                      clickTable.NewMouseFixationsRow();
                    newRow.PosX = msc.ClickLocation.X;
                    newRow.PosY = msc.ClickLocation.Y;
                    newRow.SubjectName = entry;
                    newRow.TrialID = trialID;
                    newRow.TrialSequence = trialSequence;
                    clickTable.AddMouseFixationsRow(newRow);
                    break;
                }
              }
              else if (type == EventType.Response)
              {
                string taskString = trialEvent["EventTask"].ToString();
                string paramString = trialEvent["EventParam"].ToString();

                StopCondition sc = (StopCondition)TypeDescriptor.GetConverter(typeof(StopCondition)).ConvertFrom(paramString);
                if (sc is MouseStopCondition)
                {
                  MouseStopCondition msc = sc as MouseStopCondition;
                  InputEventTask task = (InputEventTask)Enum.Parse(typeof(InputEventTask), taskString, true);
                  Ogama.DataSet.OgamaDataSet.MouseFixationsRow newRow =
                    clickTable.NewMouseFixationsRow();
                  newRow.PosX = msc.ClickLocation.X;
                  newRow.PosY = msc.ClickLocation.Y;
                  newRow.SubjectName = entry;
                  newRow.TrialID = trialID;
                  newRow.TrialSequence = trialSequence;
                  clickTable.AddMouseFixationsRow(newRow);
                  break;
                }
              }
            }
          }
        }
      }

      return clickTable;
    }

    /// <summary>
    /// This method returns a <see cref="DataTable"/> with the fixations at the given
    /// trial ID of the given subjects.
    /// </summary>
    /// <param name="trialID">An <see cref="Int32"/> with the trial id.</param>
    /// <param name="checkedSubjects">A <see cref="List{String}"/> with the subject names.</param>
    /// <returns>A <see cref="DataTable"/> filtered gaze or mouse fixations.</returns>
    private DataTable GetFilteredFixations(int trialID, List<string> checkedSubjects)
    {
      // Generate WhereStatement for Fixationselection from database
      string searchCondition = " (TrialID = " + trialID + ") ";
      string whereStatement = string.Empty;
      foreach (string entry in checkedSubjects)
      {
        // Ignore trials marked to be eliminated in analysis.
        DataTable currentTrial =
          Document.ActiveDocument.DocDataSet.TrialsAdapter.GetDataBySubjectAndTrialID(entry, trialID);
        if (currentTrial.Rows.Count > 0 && (currentTrial.Rows[0]["EliminateData"] != null && currentTrial.Rows[0]["EliminateData"].ToString() != string.Empty))
        {
          continue;
        }

        whereStatement += "((SubjectName = '" + entry + "') AND " + searchCondition + ") ";
        if (this.btnSpecialFix.Checked)
        {
          whereStatement = whereStatement.Remove(whereStatement.Length - 2);
          whereStatement += "AND (CountInTrial=" + this.nudFixNumber.Value.ToString() + ")) ";
        }

        whereStatement += "OR ";
      }

      if (whereStatement == string.Empty)
      {
        string message = "The data from the selected subjects is marked to be eliminated from analysis." +
          "So nothing will be displayed.";
        ExceptionMethods.ProcessMessage("No data available for this settings ...", message);
        this.btnStartCalculation.Enabled = true;
        return null;
      }

      whereStatement = whereStatement.Substring(0, whereStatement.Length - 4);

      // Load FixationData according to selections
      DataTable table = null;
      if (this.btnEye.Checked)
      {
        table = Queries.GetGazeFixDataByWhereStatement(whereStatement);
      }
      else if (this.btnMouse.Checked)
      {
        table = Queries.GetMouseFixDataByWhereStatement(whereStatement);
      }

      return table;
    }

    #endregion //METHODS

    ///////////////////////////////////////////////////////////////////////////////
    // Small helping Methods                                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region HELPER

    /// <summary>
    /// Returns a value indicating whether the specified 
    /// TreeNode has checked child nodes.
    /// </summary>
    /// <param name="node">Tree node to test for checked items</param>
    /// <returns>True if this node has checked child nodes</returns>
    private bool HasCheckedChildNodes(TreeNode node)
    {
      if (node.Nodes.Count == 0)
      {
        return false;
      }

      foreach (TreeNode childNode in node.Nodes)
      {
        if (childNode.Checked)
        {
          return true;
        }

        // Recursively check the children of the current child node.
        if (this.HasCheckedChildNodes(childNode))
        {
          return true;
        }
      }

      return false;
    }

    #endregion //HELPER
  }
}