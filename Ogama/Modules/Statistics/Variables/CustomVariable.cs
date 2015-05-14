// <copyright file="CustomVariable.cs" company="FU Berlin">
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

namespace Ogama.Modules.Statistics.Variables
{
  using System.Text;
  using System.Windows.Forms;

  /// <summary>
  /// This enumeration defines custom variable calculation
  /// possibilities.
  /// </summary>
  public enum ParamTypes
  {
    /// <summary>
    /// Calculates the complete fixation time at a AOI or AOI group.
    /// </summary>
    CompleteTime,

    /// <summary>
    /// Calculates the time until a specific fixation on a AOI or AOI group.
    /// </summary>
    TimeUntil,

    /// <summary>
    /// Calculate the number of fixations in a specific AOI or AOI group.
    /// </summary>
    NumberOf,

    /// <summary>
    /// Calculate the average fixation duration mean in a specific AOI or AOI group.
    /// </summary>
    FixationDurationMean,

    /// <summary>
    /// Calculate the median of the fixation durations in a specific AOI or AOI group.
    /// </summary>
    FixationDurationMedian,

    /// <summary>
    /// Calculate the number of mouse clicks in a specific AOI or AOI group.
    /// </summary>
    Clicks,

    /// <summary>
    /// Calculate regressions in a specific AOI or AOI group or over all.
    /// </summary>
    Regressions,

    /// <summary>
    /// Calculate average saccade duration in a specific AOI or AOI group.
    /// </summary>
    SaccadeDuration,

    /// <summary>
    /// Calculate saccade length by using the distance between consecutive fixation centers in a specific AOI or AOI group.
    /// </summary>
    SaccadeLength,

    /// <summary>
    /// Calculate saccade velocity by dividing saccade length by saccade time in a specific AOI or AOI group.
    /// </summary>
    SaccadeVelocity,
  }

  /// <summary>
  /// This structure is used to define a custom variable that can be calculated
  /// via the statistics interface
  /// </summary>
  public struct CustomVariable
  {
    /// <summary>
    /// The type of calculation method to be used.
    /// </summary>
    public ParamTypes ParamType;

    /// <summary>
    /// The name of the column in which the value should be written.
    /// </summary>
    public string ColumnName;

    /// <summary>
    /// The name of the AOI or AOI group to detect the fixations for.
    /// </summary>
    public string AOIName;

    /// <summary>
    /// Flag. True if the string in the property <see cref="AOIName"/>
    /// specifies an AOI group, otherwise false (specifying an single AOI)
    /// </summary>
    public bool IsAOIGroup;

    /// <summary>
    /// Optional when using <see cref="ParamTypes.TimeUntil"/>.
    /// The number of fixation (first, second, third) which one is interested in.
    /// Or when using <see cref="ParamTypes.Regressions"/> the line height
    /// </summary>
    public int Number;

    /// <summary>
    /// Optional when using <see cref="ParamTypes.Clicks"/>.
    /// The <see cref="string"/> that is the trigger in the database
    /// for mouse clicks that should be checked for
    /// clicks at the given AOI or AOI group.
    /// </summary>
    public MouseButtons MouseButton;

    /// <summary>
    /// Initializes a new instance of the CustomVariable struct.
    /// </summary>
    /// <param name="paramType">The type of calculation method to be used.</param>
    /// <param name="name">The name of the column in which the value should be written.</param>
    /// <param name="aoiName">The name of the AOI or AOI Group to detect the fixations for.</param>
    /// <param name="isAOIGroup">True if the string in the property aoiName
    /// specifies an AOI group, otherwise false (specifying an single AOI)</param>
    /// <param name="number">Optional when using <see cref="ParamTypes.TimeUntil"/>.
    /// The number of fixation (first, second, third) which one is interested in.</param>
    /// <param name="button">Optional when using <see cref="ParamTypes.Clicks"/>.
    /// The <see cref="MouseButtons"/> that should be checked for
    /// clicks at the given AOI or AOI group.
    /// </param>
    public CustomVariable(
      ParamTypes paramType,
      string name,
      string aoiName,
      bool isAOIGroup,
      int number,
      MouseButtons button)
    {
      this.ParamType = paramType;
      this.ColumnName = name;
      this.AOIName = aoiName;
      this.IsAOIGroup = isAOIGroup;
      this.Number = number;
      this.MouseButton = button;
    }

    /// <summary>
    /// Overridden. This method returns a human readable representation of
    /// this custom variable.
    /// </summary>
    /// <returns>A <see cref="string"/> representation of this custom variable.</returns>
    public override string ToString()
    {
      StringBuilder sb = new StringBuilder();
      switch (this.ParamType)
      {
        case ParamTypes.CompleteTime:
          sb.Append("Complete fixation time at AOI");
          sb.Append(this.IsAOIGroup ? " Group: " : ":");
          sb.Append(this.AOIName);
          sb.Append(" (ms)");
          break;
        case ParamTypes.TimeUntil:
          sb.Append("Time until ");
          sb.Append(this.Number);
          sb.Append(" fixation in AOI");
          sb.Append(this.IsAOIGroup ? " Group: " : ":");
          sb.Append(this.AOIName);
          sb.Append(" (ms)");
          break;
        case ParamTypes.NumberOf:
          sb.Append("Number of fixations at AOI");
          sb.Append(this.IsAOIGroup ? " Group: " : ":");
          sb.Append(this.AOIName);
          break;
        case ParamTypes.FixationDurationMean:
          sb.Append("Fixation duration mean at AOI");
          sb.Append(this.IsAOIGroup ? " Group: " : ":");
          sb.Append(this.AOIName);
          break;
        case ParamTypes.FixationDurationMedian:
          sb.Append("Fixation duration median at AOI");
          sb.Append(this.IsAOIGroup ? " Group: " : ":");
          sb.Append(this.AOIName);
          break;
        case ParamTypes.Clicks:
          sb.Append("Clicks of button: '");
          sb.Append(this.MouseButton);
          sb.Append("' at AOI");
          sb.Append(this.IsAOIGroup ? " Group: " : ":");
          sb.Append(this.AOIName);
          break;
        case ParamTypes.Regressions:
          sb.Append("Number of regressions ");
          if (this.AOIName != string.Empty)
          {
            sb.Append("in AOI");
            sb.Append(this.IsAOIGroup ? " Group: " : ":");
            sb.Append(this.AOIName);
          }

          break;
        case ParamTypes.SaccadeDuration:
          sb.Append("Saccade duration mean at AOI");
          sb.Append(this.IsAOIGroup ? " Group: " : ":");
          sb.Append(this.AOIName);
          break;
        case ParamTypes.SaccadeLength:
          sb.Append("Saccade distance mean at AOI");
          sb.Append(this.IsAOIGroup ? " Group: " : ":");
          sb.Append(this.AOIName);
          break;
        case ParamTypes.SaccadeVelocity:
          sb.Append("Saccade velocity mean at AOI");
          sb.Append(this.IsAOIGroup ? " Group: " : ":");
          sb.Append(this.AOIName);
          break;
      }

      return sb.ToString();
    }

    /// <summary>
    /// Overridden. This method returns a human readable representation of
    /// this custom variable.
    /// </summary>
    /// <returns>A <see cref="string"/> representation of this custom variable.</returns>
    public string ReturnValues()
    {
      StringBuilder sb = new StringBuilder();
      switch (this.ParamType)
      {
        case ParamTypes.CompleteTime:
          sb.AppendLine("A value of -1 means: ");
          sb.AppendLine("this AOI/AOI group was not defined.");
          break;
        case ParamTypes.TimeUntil:
          sb.AppendLine("A value of -1 means:");
          sb.AppendLine("gaze/mouse was never a " + this.Number.ToString() + ". time over this AOI");
          sb.AppendLine("A value of -2 means: ");
          sb.AppendLine("no such AOI/AOI Group is defined");
          break;
        case ParamTypes.NumberOf:
          sb.AppendLine("A value of -1 means: ");
          sb.AppendLine("this AOI/AOI group was not defined.");
          break;
        case ParamTypes.FixationDurationMean:
          sb.AppendLine("A value of -1 means: ");
          sb.AppendLine("gaze/mouse was never over this AOI");
          sb.AppendLine("A value of -2 means: ");
          sb.AppendLine("this AOI/AOI group was not defined.");
          break;
        case ParamTypes.FixationDurationMedian:
          sb.AppendLine("A value of -1 means: ");
          sb.AppendLine("gaze/mouse was never over this AOI");
          sb.AppendLine("A value of -2 means: ");
          sb.AppendLine("this AOI/AOI group was not defined.");
          break;
        case ParamTypes.Clicks:
          sb.AppendLine("A value if -1 means: ");
          sb.AppendLine("mouse was never over AOI/ AOI group ");
          sb.AppendLine("A value of -2 means: ");
          sb.AppendLine("this AOIs was not definded in this trial ");
          sb.AppendLine("A value of -3 means: ");
          sb.AppendLine("no clicks of given type at all");
          break;
        case ParamTypes.Regressions:
          sb.AppendLine("A value of 0 means: ");
          sb.AppendLine("no regressions found");
          sb.AppendLine("A value if -1 means: ");
          sb.AppendLine("gaze/mouse was never over AOI/ AOI group ");
          sb.AppendLine("A value of -2 means: ");
          sb.AppendLine("this AOIs was not definded in this trial ");
          break;
        case ParamTypes.SaccadeDuration:
          sb.AppendLine("A value of -1 means: ");
          sb.AppendLine("gaze/mouse was never over this AOI");
          sb.AppendLine("A value of -2 means: ");
          sb.AppendLine("this AOI/AOI group was not defined.");
          break;
        case ParamTypes.SaccadeLength:
          sb.AppendLine("A value of -1 means: ");
          sb.AppendLine("gaze/mouse was never over this AOI");
          sb.AppendLine("A value of -2 means: ");
          sb.AppendLine("this AOI/AOI group was not defined.");
          break;
        case ParamTypes.SaccadeVelocity:
          sb.AppendLine("A value of -1 means: ");
          sb.AppendLine("gaze/mouse was never over this AOI");
          sb.AppendLine("A value of -2 means: ");
          sb.AppendLine("this AOI/AOI group was not defined.");
          break;
      }

      return sb.ToString();
    }
  }
}
