// <copyright file="PointFConverter.cs" company="FU Berlin">
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

namespace VectorGraphics.Tools.CustomTypeConverter
{
  using System;
  using System.ComponentModel;
  using System.Drawing;
  using System.Globalization;

  /// <summary>
  /// Derived from <see cref="ExpandableObjectConverter"/>.
  /// Provides a type converter to convert expandable objects 
  /// to and from the <see cref="PointF"/> structure
  /// </summary>
  public class PointFConverter : ExpandableObjectConverter
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
    /// the object to the specified type <see cref="PointF"/>. 
    /// </summary>
    /// <param name="context">An <see cref="ITypeDescriptorContext"/> that 
    /// provides a format context.</param>
    /// <param name="destinationType">A <see cref="Type"/> that represents 
    /// the type you want to convert to.</param>
    /// <returns><strong>True</strong> if this converter can 
    /// perform the conversion; otherwise, <strong>false</strong>. </returns>
    public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
    {
      if (destinationType == typeof(PointF))
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
    /// <returns>An <strong>Object</strong> that represents the converted value.</returns>
    public override object ConvertFrom(
      ITypeDescriptorContext context,
      CultureInfo culture,
      object value)
    {
      if (value is string)
      {
        string[] v = ((string)value).Split(new char[] { ';' });
        return new PointF(float.Parse(v[0]), float.Parse(v[1]));
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
    /// <returns>An <strong>Object</strong> that represents the converted value.</returns>
    public override object ConvertTo(
      ITypeDescriptorContext context,
      CultureInfo culture, 
      object value, 
      Type destinationType)
    {
      if (destinationType == typeof(string) && value is PointF)
      {
        return ((PointF)value).X + ";" + ((PointF)value).Y;
      }

      return base.ConvertTo(context, culture, value, destinationType);
    }
  }
}
