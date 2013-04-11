// <copyright file="StopConditionCollectionConverter.cs" company="FU Berlin">
// ******************************************************
// OGAMA - open gaze and mouse analyzer 
// Copyright (C) 2012 Adrian Voßkühler  
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
  using System.Collections.Generic;
  using System.ComponentModel;
  using System.Drawing;
  using System.Globalization;
  using System.IO;
  using System.Text;
  using System.Windows.Forms;

  /// <summary>
  /// Derived from <see cref="ExpandableObjectConverter"/>.
  /// Provides a type converter to convert <see cref="StopConditionCollection"/> objects
  /// to strings.
  /// </summary>
  /// <remarks>It is implemented to have the human readable description
  /// "Slides stop conditions." for the <see cref="StopConditionCollection"/> 
  /// at the right side of the 
  /// <see cref="System.Windows.Forms.PropertyGrid"/> row.</remarks>
  public class StopConditionCollectionConverter : ExpandableObjectConverter
  {
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
    /// In this implementation that is just a list description.</returns>
    public override object ConvertTo(
      ITypeDescriptorContext context,
      CultureInfo culture, 
      object value, 
      Type destinationType)
    {
      if (destinationType == typeof(string) && value is StopConditionCollection)
      {
        // Return a descriptive string.
        return ((StopConditionCollection)value).ToString();
      }

      return base.ConvertTo(context, culture, value, destinationType);
    }
  }
}
