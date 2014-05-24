// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EnumExtensions.cs" company="Freie Universität Berlin">
//   OGAMA - open gaze and mouse analyzer 
//   Copyright (C) 2014 Dr. Adrian Voßkühler  
//   Licensed under GPL V3
// </copyright>
// <author>Adrian Voßkühler</author>
// <email>adrian@ogama.net</email>
// <summary>
//   Defines the EnumExtensions type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Ogama.Modules.Common.Types
{
  using System;
  using System.Collections.Generic;
  using System.ComponentModel;
  using System.Linq;
  using System.Reflection;

  /// <summary>
  /// Extensions for enums to check for flags etc.
  /// </summary>
  public static class EnumExtensions
  {
    #region Public Methods and Operators

    /// <summary>
    /// Clears the flags.
    /// </summary>
    /// <typeparam name="T">The enum type</typeparam>
    /// <param name="value">
    /// The value.
    /// </param>
    /// <param name="flags">
    /// The flags.
    /// </param>
    /// <returns>
    /// The Flag.
    /// </returns>
    public static T ClearFlags<T>(this T value, T flags) where T : struct 
    {
      return value.SetFlags(flags, false);
    }

    /// <summary>
    /// Combines the flags.
    /// </summary>
    /// <typeparam name="T">The enum type</typeparam>
    /// <param name="flags">
    /// The flags.
    /// </param>
    /// <returns>
    /// The Flag result
    /// </returns>
    public static T CombineFlags<T>(this IEnumerable<T> flags) where T : struct
    {
      CheckIsEnum<T>(true);
      long lValue = 0;
      foreach (T flag in flags)
      {
        long lFlag = Convert.ToInt64(flag);
        lValue |= lFlag;
      }

      return (T)Enum.ToObject(typeof(T), lValue);
    }

    /// <summary>
    /// Gets the description.
    /// </summary>
    /// <typeparam name="T">The enum type</typeparam>
    /// <param name="value">
    /// The value.
    /// </param>
    /// <returns>
    /// The description for the flags.
    /// </returns>
    public static string GetDescription<T>(this T value) where T : struct
    {
      CheckIsEnum<T>(false);
      string name = Enum.GetName(typeof(T), value);
      if (name != null)
      {
        FieldInfo field = typeof(T).GetField(name);
        if (field != null)
        {
          var attr = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) as DescriptionAttribute;
          if (attr != null)
          {
            return attr.Description;
          }
        }
      }

      return null;
    }

    /// <summary>
    /// Gets the flags.
    /// </summary>
    /// <typeparam name="T">The enum type</typeparam>
    /// <param name="value">
    /// The value.
    /// </param>
    /// <returns>
    /// The enumeration of flags
    /// </returns>
    public static IEnumerable<T> GetFlags<T>(this T value) where T : struct
    {
      CheckIsEnum<T>(true);
      foreach (T flag in Enum.GetValues(typeof(T)).Cast<T>())
      {
        if (value.IsFlagSet(flag))
        {
          yield return flag;
        }
      }
    }

    /// <summary>
    /// Determines whether [is flag set] [the specified value].
    /// </summary>
    /// <typeparam name="T">The enum type</typeparam>
    /// <param name="value">
    /// The value.
    /// </param>
    /// <param name="flag">
    /// The flag.
    /// </param>
    /// <returns>
    /// The <see cref="bool"/>.
    /// </returns>
    public static bool IsFlagSet<T>(this T value, T flag) where T : struct
    {
      CheckIsEnum<T>(true);
      long lValue = Convert.ToInt64(value);
      long lFlag = Convert.ToInt64(flag);
      return (lValue & lFlag) != 0;
    }

    /// <summary>
    /// Sets the flags.
    /// </summary>
    /// <typeparam name="T">The enum type</typeparam>
    /// <param name="value">
    /// The value.
    /// </param>
    /// <param name="flags">
    /// The flags.
    /// </param>
    /// <param name="on">
    /// if set to <c>true</c> [on].
    /// </param>
    /// <returns>
    /// The flag
    /// </returns>
    public static T SetFlags<T>(this T value, T flags, bool on) where T : struct
    {
      CheckIsEnum<T>(true);
      long lValue = Convert.ToInt64(value);
      long lFlag = Convert.ToInt64(flags);
      if (on)
      {
        lValue |= lFlag;
      }
      else
      {
        lValue &= ~lFlag;
      }

      return (T)Enum.ToObject(typeof(T), lValue);
    }

    /// <summary>
    /// Sets the flags.
    /// </summary>
    /// <typeparam name="T">The enum type</typeparam>
    /// <param name="value">
    /// The value.
    /// </param>
    /// <param name="flags">
    /// The flags.
    /// </param>
    /// <returns>
    /// The flags
    /// </returns>
    public static T SetFlags<T>(this T value, T flags) where T : struct
    {
      return value.SetFlags(flags, true);
    }

    #endregion

    #region Methods

    /// <summary>
    /// Checks the is enum.
    /// </summary>
    /// <typeparam name="T">The enum type</typeparam>
    /// <param name="withFlags">
    /// if set to <c>true</c> [with flags].
    /// </param>
    /// <exception cref="System.ArgumentException">
    /// </exception>
    private static void CheckIsEnum<T>(bool withFlags)
    {
      if (!typeof(T).IsEnum)
      {
        throw new ArgumentException(string.Format("Type '{0}' is not an enum", typeof(T).FullName));
      }

      if (withFlags && !Attribute.IsDefined(typeof(T), typeof(FlagsAttribute)))
      {
        throw new ArgumentException(string.Format("Type '{0}' doesn't have the 'Flags' attribute", typeof(T).FullName));
      }
    }

    #endregion
  }
}