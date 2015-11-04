// <copyright file="StringEnum.cs" company="alea technologies">
// ******************************************************
// OGAMA - open gaze and mouse analyzer 
// Copyright (C) 2015 Dr. Adrian Voßkühler  
// ------------------------------------------------------------------------
// This program is free software; you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation; either version 2 of the License, or (at your option) any later version.
// This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
// You should have received a copy of the GNU General Public License along with this program; if not, write to the Free Software Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA 02111-1307 USA
// **************************************************************
// </copyright>
// <author>Martin Werner</author>
// <email>martin.werner@alea-technologies.de</email>

namespace Ogama.Modules.Common.Types
{
  using System;
  using System.Collections;
  using System.Reflection;

  /// <summary>
  /// Helper class for working with 'extended' enums using <see cref="StringValueAttribute"/> attributes.
  /// </summary>
  public class StringEnum
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
    /// The <see cref="Hashtable"/> with the string values.
    /// </summary>
    private static Hashtable stringValues = new Hashtable();

    /// <summary>
    /// The underlying enum type for this instance.
    /// </summary>
    private Type enumType;

    #endregion //FIELDS

    ///////////////////////////////////////////////////////////////////////////////
    // Construction and Initializing methods                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region CONSTRUCTION

    /// <summary>
    /// Initializes a new instance of the <see cref="StringEnum"/> class.
    /// </summary>
    /// <param name="enumType">Enum type.</param>
    public StringEnum(Type enumType)
    {
      if (!enumType.IsEnum)
      {
        throw new ArgumentException(string.Format("Supplied type must be an Enum.  Type was {0}", enumType.ToString()));
      }

      this.enumType = enumType;
    }

    #endregion //CONSTRUCTION

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Properties                                                       //
    ///////////////////////////////////////////////////////////////////////////////
    #region PROPERTIES

    /// <summary>
    /// Gets the underlying enum type for this instance.
    /// </summary>
    /// <value></value>
    public Type EnumType
    {
        get { return this.enumType; }
    }

    #endregion //PROPERTIES

    ///////////////////////////////////////////////////////////////////////////////
    // Public methods                                                            //
    ///////////////////////////////////////////////////////////////////////////////
    #region PUBLICMETHODS

    /// <summary>
    /// Gets a string value for a particular enum value.
    /// </summary>
    /// <param name="value">Enumeration Value.</param>
    /// <returns>String Value associated via a <see cref="StringValueAttribute"/> attribute, or null if not found.</returns>
    public static string GetStringValue(Enum value)
    {
      string output = null;
      Type type = value.GetType();

      if (stringValues.ContainsKey(value))
      {
        output = (stringValues[value] as StringValueAttribute).Value;
      }
      else
      {
        // Look for our 'StringValueAttribute' in the field's custom attributes
        FieldInfo fi = type.GetField(value.ToString());
        StringValueAttribute[] attrs = fi.GetCustomAttributes(typeof(StringValueAttribute), false) as StringValueAttribute[];
        if (attrs.Length > 0)
        {
          stringValues.Add(value, attrs[0]);
          output = attrs[0].Value;
        }
      }

      return output;
    }

    /// <summary>
    /// Parses the supplied enum and string value to find an associated enum value (case sensitive).
    /// </summary>
    /// <param name="type">Type of Enumeration.</param>
    /// <param name="stringValue">String value.</param>
    /// <returns>Enum value associated with the string value, or null if not found.</returns>
    public static object Parse(Type type, string stringValue)
    {
      return Parse(type, stringValue, false);
    }

    /// <summary>
    /// Parses the supplied enum and string value to find an associated enum value.
    /// </summary>
    /// <param name="type">Type of Enumeration.</param>
    /// <param name="stringValue">String value.</param>
    /// <param name="ignoreCase">Denotes whether to conduct a case-insensitive match on the supplied string value</param>
    /// <returns>Enum value associated with the string value, or null if not found.</returns>
    public static object Parse(Type type, string stringValue, bool ignoreCase)
    {
      object output = null;
      string enumStringValue = null;

      if (!type.IsEnum)
      {
        throw new ArgumentException(string.Format("Supplied type must be an Enum.  Type was {0}", type.ToString()));
      }

      // Look for our string value associated with fields in this enum
      foreach (FieldInfo fi in type.GetFields())
      {
        // Check for our custom attribute
        StringValueAttribute[] attrs = fi.GetCustomAttributes(typeof(StringValueAttribute), false) as StringValueAttribute[];
        if (attrs.Length > 0)
        {
          enumStringValue = attrs[0].Value;
        }

        // Check for equality then select actual enum value.
        if (string.Compare(enumStringValue, stringValue, ignoreCase) == 0)
        {
          output = Enum.Parse(type, fi.Name);
          break;
        }
      }

      return output;
    }

    /// <summary>
    /// Return the existence of the given string value within the enum.
    /// </summary>
    /// <param name="enumType">Type of enum</param>
    /// <param name="stringValue">String value.</param>
    /// <returns>Existence of the string value</returns>
    public static bool IsStringDefined(Type enumType, string stringValue)
    {
      return Parse(enumType, stringValue) != null;
    }

    /// <summary>
    /// Return the existence of the given string value within the enum.
    /// </summary>    
    /// <param name="enumType">Type of enum</param>
    /// <param name="stringValue">String value.</param>
    /// <param name="ignoreCase">Denotes whether to conduct a case-insensitive match on the supplied string value</param>
    /// <returns>Existence of the string value</returns>
    public static bool IsStringDefined(Type enumType, string stringValue, bool ignoreCase)
    {
      return Parse(enumType, stringValue, ignoreCase) != null;
    }

    /// <summary>
    /// Gets the string value associated with the given enum value.
    /// </summary>
    /// <param name="valueName">Name of the enum value.</param>
    /// <returns>String Value</returns>
    public string GetStringValue(string valueName)
    {
      Enum enumType;
      string stringValue = null;
      try
      {
        enumType = (Enum)Enum.Parse(this.enumType, valueName);
        stringValue = GetStringValue(enumType);
      }
      catch (Exception) 
      {
        // Swallow!
      }

      return stringValue;
    }

    /// <summary>
    /// Gets the string values associated with the enum.
    /// </summary>
    /// <returns>String value array</returns>
    public Array GetStringValues()
    {
      ArrayList values = new ArrayList();

      // Look for our string value associated with fields in this enum
      foreach (FieldInfo fi in this.enumType.GetFields())
      {
        // Check for our custom attribute
        StringValueAttribute[] attrs = fi.GetCustomAttributes(typeof(StringValueAttribute), false) as StringValueAttribute[];
        if (attrs.Length > 0)
        {
          values.Add(attrs[0].Value);
        }
      }

      return values.ToArray();
    }

    /// <summary>
    /// Gets the values as a 'bindable' list datasource.
    /// </summary>
    /// <returns>IList for data binding</returns>
    public IList GetListValues()
    {
      Type underlyingType = Enum.GetUnderlyingType(this.enumType);
      ArrayList values = new ArrayList();

      // Look for our string value associated with fields in this enum
      foreach (FieldInfo fi in this.enumType.GetFields())
      {
        // Check for our custom attribute
        StringValueAttribute[] attrs = fi.GetCustomAttributes(typeof(StringValueAttribute), false) as StringValueAttribute[];
        if (attrs.Length > 0)
        {
          values.Add(new DictionaryEntry(Convert.ChangeType(Enum.Parse(this.enumType, fi.Name), underlyingType), attrs[0].Value));
        }
      }

      return values;
    }

    /// <summary>
    /// Return the existence of the given string value within the enum.
    /// </summary>
    /// <param name="stringValue">String value.</param>
    /// <returns>Existence of the string value</returns>
    public bool IsStringDefined(string stringValue)
    {
      return Parse(this.enumType, stringValue) != null;
    }

    /// <summary>
    /// Return the existence of the given string value within the enum.
    /// </summary>
    /// <param name="stringValue">String value.</param>
    /// <param name="ignoreCase">Denotes whether to conduct a case-insensitive match on the supplied string value</param>
    /// <returns>Existence of the string value</returns>
    public bool IsStringDefined(string stringValue, bool ignoreCase)
    {
      return Parse(this.enumType, stringValue, ignoreCase) != null;
    }

    #endregion //PUBLICMETHODS

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
    // Inherited methods                                                         //
    ///////////////////////////////////////////////////////////////////////////////
    #region OVERRIDES
    #endregion //OVERRIDES

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