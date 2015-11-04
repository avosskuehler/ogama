// <copyright file="MouseStopCondition.cs" company="FU Berlin">
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

namespace VectorGraphics.StopConditions
{
  using System;
  using System.ComponentModel;
  using System.Drawing;
  using System.Text;
  using System.Windows.Forms;

  using VectorGraphics.Tools.CustomTypeConverter;

  /// <summary>
  /// Derived from <see cref="StopCondition"/>.
  /// This class stores a <see cref="MouseStopCondition.StopMouseButton"/>.
  /// When this is pressed the display of the slide should be ended.
  /// </summary>
  [Serializable]
  public class MouseStopCondition : InputStopCondition
  {
    ///////////////////////////////////////////////////////////////////////////////
    // Defining Constants                                                        //
    ///////////////////////////////////////////////////////////////////////////////
    #region CONSTANTS

    /// <summary>
    /// A <see cref="MouseButtons"/> with the default value for
    /// the <see cref="MouseStopCondition.StopMouseButton"/> property.
    /// </summary>
    private const MouseButtons DEFAULTBUTTON = MouseButtons.None;

    #endregion //CONSTANTS

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Variables, Enumerations, Events                                  //
    ///////////////////////////////////////////////////////////////////////////////
    #region FIELDS

    /// <summary>
    /// Saves the <see cref="MouseButtons"/> to listen for.
    /// </summary>
    private MouseButtons stopButton;

    /// <summary>
    /// Saves the click location of the mouse button.
    /// </summary>
    private Point clickLocation;

    /// <summary>
    /// Saves the name of the target shape that has to be clicked
    /// to be valid.
    /// </summary>
    private string target;

    #endregion //FIELDS

    ///////////////////////////////////////////////////////////////////////////////
    // Construction and Initializing methods                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region CONSTRUCTION

    /// <summary>
    /// Initializes a new instance of the MouseStopCondition class.
    /// </summary>
    public MouseStopCondition()
    {
      this.clickLocation = Point.Empty;
      this.target = string.Empty;
      this.stopButton = MouseButtons.None;
    }

    /// <summary>
    /// Initializes a new instance of the MouseStopCondition class.
    /// Clone Constructor.
    /// </summary>
    /// <param name="cloneCondition">The <see cref="MouseStopCondition"/>
    /// to be cloned.</param>
    public MouseStopCondition(MouseStopCondition cloneCondition)
      : base(cloneCondition)
    {
      this.stopButton = cloneCondition.StopMouseButton;
      this.target = cloneCondition.Target;
      this.clickLocation = cloneCondition.clickLocation;
    }

    /// <summary>
    /// Initializes a new instance of the MouseStopCondition class.
    /// </summary>
    /// <param name="newButton">A <see cref="MouseButtons"/> with the button
    /// that should end the slide display.</param>
    /// <param name="anyMouseButton"><strong>True</strong>, if any mouse button can
    /// end the slide display, otherwise <strong>false</strong>.</param>
    /// <param name="newTarget">A <see cref="string"/> with an optional target shape name
    /// that has to be clicked.</param>
    /// <param name="newLinkTrialID">A nullable <see cref="int"/> with an optional trial ID
    /// to which this condition should link on click.</param>
    /// <param name="newClickLocation">The <see cref="Point"/> of the mouse click location
    /// in screen coordinates.</param>
    public MouseStopCondition(
      MouseButtons newButton,
      bool anyMouseButton,
      string newTarget,
      int? newLinkTrialID,
      Point newClickLocation)
    {
      this.stopButton = newButton;
      this.CanBeAnyInputOfThisType = anyMouseButton;
      this.target = newTarget;
      this.TrialID = newLinkTrialID;
      this.clickLocation = newClickLocation;
    }

    #endregion //CONSTRUCTION

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Properties                                                       //
    ///////////////////////////////////////////////////////////////////////////////
    #region PROPERTIES

    /// <summary>
    /// Gets or sets the <see cref="MouseButtons"/> that ends the slide display.
    /// </summary>
    /// <value>A <see cref="MouseButtons"/> that ends the slide display.</value>
    [Category("General")]
    [Description("The mouse button that can end the slide display.")]
    public MouseButtons StopMouseButton
    {
      get
      {
        return this.stopButton;
      }

      set
      {
        this.stopButton = value;
        if (this.stopButton != MouseButtons.None)
        {
          this.CanBeAnyInputOfThisType = false;
        }
      }
    }

    /// <summary>
    /// Gets or sets the click location of the mouse button.
    /// </summary>
    /// <value>A <see cref="Point"/> with the location of the mouse click in screen coordinates.</value>
    [Category("General")]
    [Description("The position of the mouse click.")]
    public Point ClickLocation
    {
      get { return this.clickLocation; }
      set { this.clickLocation = value; }
    }

    /// <summary>
    /// Gets or sets the name of the target shape that has to be clicked
    /// to be valid.
    /// </summary>
    /// <value>A <see cref="string"/> with the name of an optional target shape.</value>
    [Category("General")]
    [Description("The name of an optional target shape that has to be clicked.")]
    public string Target
    {
      get { return this.target; }
      set { this.target = value; }
    }

    #endregion //PROPERTIES

    ///////////////////////////////////////////////////////////////////////////////
    // Inherited methods                                                         //
    ///////////////////////////////////////////////////////////////////////////////
    #region OVERRIDES

    /// <summary>
    /// Overridden <see cref="Object.ToString()"/> method.
    /// Returns a <see cref="string"/> that represents the current <see cref="MouseStopCondition"/>. 
    /// </summary>
    /// <returns>A <see cref="string"/> that represents the current <see cref="MouseStopCondition"/></returns>
    public override string ToString()
    {
      StringBuilder sb = new StringBuilder();
      sb.Append("Mouse: ");
      if (this.CanBeAnyInputOfThisType)
      {
        sb.Append(" any mouse button");
      }
      else
      {
        sb.Append(this.stopButton.ToString());
      }

      sb.Append(" ");

      if (this.clickLocation != Point.Empty)
      {
        sb.Append(ObjectStringConverter.PointToString(this.clickLocation));
      }

      if (this.target != string.Empty)
      {
        if (this.target != "Any")
        {
          sb.Append(", at target: ");
          sb.Append(this.target);
        }
        else
        {
          sb.Append(", at any target");
        }
      }

      if (this.IsCorrectResponse.HasValue)
      {
        if (this.IsCorrectResponse.Value)
        {
          sb.Append(". Correct response to the slide");
        }
        else
        {
          sb.Append(". Wrong response to the slide");
        }
      }

      if (this.TrialID.HasValue)
      {
        sb.Append("- Links to Trial ID #");
        sb.Append(this.TrialID.Value);
      }

      return sb.ToString();
    }

    /// <summary>
    /// Determines whether two <see cref="MouseStopCondition"/> instances are equal.
    /// </summary>
    /// <param name="obj">The <see cref="Object"/> to compare with the current 
    /// <see cref="MouseStopCondition"/>.</param>
    /// <returns><strong>True</strong> if the specified Object is equal 
    /// to the current <see cref="MouseStopCondition"/>; otherwise, <strong>false</strong>. </returns>
    public override bool Equals(object obj)
    {
      if (obj is MouseStopCondition)
      {
        MouseStopCondition msc = (MouseStopCondition)obj;
        if (msc.StopMouseButton == this.StopMouseButton &&
          msc.CanBeAnyInputOfThisType == this.CanBeAnyInputOfThisType)
        {
          if (msc.Target == this.Target)
          {
            return true;
          }
        }
      }

      return false;
    }

    /// <summary>
    /// Serves as a hash function for a particular type. 
    /// <strong>GetHashCode</strong> is suitable for use in hashing algorithms 
    /// and data structures like a hash table. 
    /// It needs to be overridden when <see cref="Object.Equals(object)"/>
    /// is overriden.
    /// </summary>
    /// <returns>A hash code for the current <see cref="MouseStopCondition"/>. </returns>
    public override int GetHashCode()
    {
      return this.stopButton.GetHashCode() ^ this.CanBeAnyInputOfThisType.GetHashCode()
        ^ this.target.GetHashCode();
    }

    /// <summary>
    /// Reset the current mouse stop condition to
    /// default values
    /// </summary>
    public override void ResetValue()
    {
      base.ResetValue();
      this.stopButton = DEFAULTBUTTON;
      this.target = string.Empty;
      this.clickLocation = Point.Empty;
    }

    /// <summary>
    /// Overridden <see cref="StopCondition.Clone()"/> method.
    /// Returns a <see cref="StopCondition"/> that is
    /// an exact copy of this <see cref="MouseStopCondition"/>. 
    /// </summary>
    /// <returns>A <see cref="StopCondition"/> that is
    /// an exact copy of this <see cref="MouseStopCondition"/>. </returns>
    protected override StopCondition CloneCore()
    {
      return new MouseStopCondition(this);
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
    #region METHODS
    #endregion //METHODS

    ///////////////////////////////////////////////////////////////////////////////
    // Small helping Methods                                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region HELPER
    #endregion //HELPER
  }
}