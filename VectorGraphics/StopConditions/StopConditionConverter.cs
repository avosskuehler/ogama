// <copyright file="StopConditionConverter.cs" company="FU Berlin">
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
  using System.Globalization;
  using System.Windows.Forms;

  using VectorGraphics.Tools.CustomTypeConverter;

  /// <summary>
  /// Derived from <see cref="ExpandableObjectConverter"/>.
  /// Provides a type converter to convert expandable objects 
  /// to and from the <see cref="StopCondition"/> object.
  /// </summary>
  public class StopConditionConverter : ExpandableObjectConverter
  {
    /// <summary>
    /// Overridden <see cref="TypeConverter.CanConvertFrom(ITypeDescriptorContext,Type)"/>
    /// Returns whether this converter can convert an object 
    /// of string to the type of this converter.
    /// </summary>
    /// <param name="context">An <see cref="ITypeDescriptorContext"/> that 
    /// provides a format context.</param>
    /// <param name="sourceType">A <see cref="Type"/> that represents 
    /// the type you want to convert from.</param>
    /// <returns><strong>True</strong> if this converter can 
    /// perform the conversion; otherwise, <strong>false</strong>. </returns>
    public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
    {
      if (sourceType == typeof(string))
      {
        return true;
      }

      return base.CanConvertFrom(context, sourceType);
    }

    /// <summary>
    /// Overridden <see cref="TypeConverter.CanConvertTo(ITypeDescriptorContext,Type)"/>
    /// Returns whether this converter can convert 
    /// the object to the specified type <see cref="StopCondition"/>. 
    /// </summary>
    /// <param name="context">An <see cref="ITypeDescriptorContext"/> that 
    /// provides a format context.</param>
    /// <param name="destinationType">A <see cref="Type"/> that represents 
    /// the type you want to convert to.</param>
    /// <returns><strong>True</strong> if this converter can 
    /// perform the conversion; otherwise, <strong>false</strong>. </returns>
    public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
    {
      if (destinationType == typeof(StopCondition))
      {
        return true;
      }

      return base.CanConvertTo(context, destinationType);
    }

    /// <summary>
    /// Overridden <see cref="TypeConverter.ConvertFrom(ITypeDescriptorContext,CultureInfo,object)"/>
    /// Converts the given value to the type of this converter.</summary>
    /// <param name="context">An <see cref="ITypeDescriptorContext"/> that 
    /// provides a format context.</param>
    /// <param name="culture">The <see cref="CultureInfo"/> to use as the current culture.</param>
    /// <param name="value">The <see cref="Object"/> to convert.</param>
    /// <returns>An <strong>Object</strong> that represents the converted value.
    /// In this implementation that can be any <see cref="StopCondition"/></returns>
    public override object ConvertFrom(
      ITypeDescriptorContext context,
      CultureInfo culture,
      object value)
    {
      if (value is string)
      {
        try
        {
          if (value.ToString() == string.Empty)
          {
            return null;
          }

          // Old Ogama V1.1 versions supplied "None" for the Response when there was no response
          // Convert them to null.
          if (value.ToString() == "None")
          {
            return null;
          }

          // Old Ogama V1.1 versions supplied "Left" for a left mouse response
          if (value.ToString() == "Left")
          {
            return new MouseStopCondition(MouseButtons.Left, false, string.Empty, null, Point.Empty);
          }

          // Old Ogama V1.1 versions supplied "Right" for a right mouse response
          if (value.ToString() == "Right")
          {
            return new MouseStopCondition(MouseButtons.Right, false, string.Empty, null, Point.Empty);
          }

          string s = (string)value;
          int colon = s.IndexOf(':');
          int point = s.IndexOf('.');
          int target = s.IndexOf("target");
          int parenthesisOpen = s.IndexOf('(');
          int parenthesisClose = s.IndexOf(')');

          if (colon != -1)
          {
            StopCondition stopCondition = null;

            string type = s.Substring(0, colon).Trim();
            if (type == "Time")
            {
              string duration = s.Substring(colon + 1, s.Length - colon - 3).Trim();
              stopCondition = new TimeStopCondition(Convert.ToInt32(duration));
            }
            else if (type == "Key")
            {
              string key = s.Substring(colon + 1, point > -1 ? point - colon - 1 : s.Length - colon - 1).Trim();
              if (key == "any key")
              {
                stopCondition = new KeyStopCondition(Keys.None, false, null);
              }
              else
              {
                stopCondition = new KeyStopCondition((Keys)Enum.Parse(typeof(Keys), key), false, null);
              }
            }
            else if (type.Contains("Mouse"))
            {
              string button = MouseButtons.None.ToString();
              Point location = Point.Empty;

              // Older versions (Ogama 1.X) did not write the location in parenthesises.
              if (parenthesisOpen == -1)
              {
                button = s.Substring(colon + 1).Trim();
                int space = button.IndexOf(" ");
                if (space != -1)
                {
                  button = button.Substring(0, space);
                }

                // Do not set location because it is not known
              }
              else
              {
                button = s.Substring(colon + 1, parenthesisOpen - colon - 1).Trim();
                location = ObjectStringConverter.StringToPoint(s.Substring(parenthesisOpen, parenthesisClose - parenthesisOpen));
              }

              stopCondition = new MouseStopCondition(
                (MouseButtons)Enum.Parse(typeof(MouseButtons), button), false, string.Empty, null, location);

              if (button == "any mouse button")
              {
                ((MouseStopCondition)stopCondition).CanBeAnyInputOfThisType = true;
              }

              if (target != -1)
              {
                int colon2 = button.IndexOf(':');
                if (colon2 != -1)
                {
                  string targetName = button.Substring(colon2).Trim();
                  ((MouseStopCondition)stopCondition).Target = targetName;
                }
                else
                {
                  ((MouseStopCondition)stopCondition).Target = "Any";
                }
              }
            }
            else if (type == "http")
            {
              stopCondition = new NavigatedStopCondition(new Uri(s));
            }

            // Parse correct answer.
            if (s.Contains("."))
            {
              if (s.Contains("Correct"))
              {
                stopCondition.IsCorrectResponse = true;
              }
              else
              {
                stopCondition.IsCorrectResponse = false;
              }
            }

            // Parse trial ID of links.
            if (s.Contains("-"))
            {
              int sharp = s.IndexOf('#');
              if (sharp != -1)
              {
                string trialIDString = s.Substring(sharp).Trim();
                int trialID = 0;
                if (int.TryParse(trialIDString, out trialID))
                {
                  ((InputStopCondition)stopCondition).TrialID = trialID;
                }
              }
            }

            return stopCondition;
          }
          else if (value.ToString().Contains("Left"))
          {
            Point location = ObjectStringConverter.StringToPoint(s.Substring(parenthesisOpen, parenthesisClose - parenthesisOpen));
            MouseStopCondition stopCondition = new MouseStopCondition(MouseButtons.Left, false, string.Empty, null, location);
            return stopCondition;
          }
          else if (value.ToString().Contains("Right"))
          {
            Point location = ObjectStringConverter.StringToPoint(s.Substring(parenthesisOpen, parenthesisClose - parenthesisOpen));
            MouseStopCondition stopCondition = new MouseStopCondition(MouseButtons.Right, false, string.Empty, null, location);
            return stopCondition;
          }
        }
        catch
        {
          throw new ArgumentException(
              " '" + (string)value + "' could not be converted to StopCondition type.");
        }
      }

      return base.ConvertFrom(context, culture, value);
    }

    /// <summary>
    /// Overridden <see cref="TypeConverter.ConvertTo(ITypeDescriptorContext,CultureInfo,object,Type)"/>
    /// </summary>
    /// <param name="context">An <see cref="ITypeDescriptorContext"/> that 
    /// provides a format context.</param>
    /// <param name="culture">A <see cref="CultureInfo"/>. If a null reference 
    /// is passed, the current culture is assumed.</param>
    /// <param name="value">The <see cref="Object"/> to convert.</param>
    /// <param name="destinationType">The <see cref="Type"/> to convert the 
    /// <strong>value</strong> parameter to.</param>
    /// <returns>An <strong>Object</strong> that represents the converted value.
    /// In this implementation that is a <see cref="string"/>.</returns>
    public override object ConvertTo(
      ITypeDescriptorContext context,
      CultureInfo culture,
      object value,
      Type destinationType)
    {
      if (destinationType == typeof(string) && value is StopCondition)
      {
        StopCondition stopCondition = (StopCondition)value;
        return stopCondition.ToString();
      }

      return base.ConvertTo(context, culture, value, destinationType);
    }
  }
}
