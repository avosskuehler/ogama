﻿// <copyright file="VGElementCollectionConverter.cs" company="FU Berlin">
// ******************************************************
// OGAMA - open gaze and mouse analyzer 
// Copyright (C) 2010 Adrian Voßkühler  
// ------------------------------------------------------------------------
// This program is free software; you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation; either version 2 of the License, or (at your option) any later version.
// This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
// You should have received a copy of the GNU General Public License along with this program; if not, write to the Free Software Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA 02111-1307 USA
// **************************************************************
// </copyright>
// <author>Adrian Voßkühler</author>
// <email>adrian.vosskuehler@fu-berlin.de</email>

namespace VectorGraphics.Tools.CustomTypeConverter
{
  using System;
  using System.ComponentModel;
  using System.Globalization;

  using VectorGraphics.Elements;
  using VectorGraphics.Elements.ElementCollections;

  /// <summary>
  /// Derived from <see cref="ExpandableObjectConverter"/>.
  /// Provides a type converter to convert <see cref="VGElementCollection"/> objects
  /// to strings.
  /// </summary>
  /// <remarks>It is implemented to have the human readable description
  /// "Vector graphic elements" for the <see cref="VGElementCollection"/> 
  /// at the right side of the 
  /// <see cref="System.Windows.Forms.PropertyGrid"/> row.</remarks>
  public class VGElementCollectionConverter : ExpandableObjectConverter
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
      if (destinationType == typeof(string) && value is VGElementCollection)
      {
        // Return standard description.
        return "Vector graphic elements.";
      }

      return base.ConvertTo(context, culture, value, destinationType);
    }
  }
}
