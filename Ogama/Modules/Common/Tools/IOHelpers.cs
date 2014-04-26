// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IOHelpers.cs" company="Freie Universität Berlin">
//   OGAMA - open gaze and mouse analyzer 
//   Copyright (C) 2014 Dr. Adrian Voßkühler  
//   Licensed under GPL V3
// </copyright>
// <author>Adrian Voßkühler</author>
// <email>adrian@ogama.net</email>
// <summary>
//   Class for import and export functionality.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Ogama.Modules.Common.Tools
{
  using System;
  using System.Diagnostics;
  using System.IO;
  using System.Linq;

  using Microsoft.Win32;

  /// <summary>
  ///   Class for import and export functionality.
  /// </summary>
  public sealed class IOHelpers
  {
    #region Public Methods and Operators

    /// <summary>
    /// Returns a Boolean value indicating whether an expression can be evaluated as a number.
    /// </summary>
    /// <param name="expression">
    /// object to test
    /// </param>
    /// <returns>
    /// <strong>True</strong> if given expression is a number,
    ///   otherwise <strong>false</strong>.
    /// </returns>
    public static bool IsNumeric(object expression)
    {
      // Variable to collect the Return value of the TryParse method.
      bool isNum;

      // Define variable to collect out parameter of the TryParse method. If the conversion fails, the out parameter is zero.
      double retNum;

      // The TryParse method converts a string in a specified style 
      // and culture-specific format to its double-precision floating point number equivalent.
      // The TryParse method does not generate an exception if the 
      // conversion fails. If the conversion passes, True is returned. 
      // If it does not, False is returned.
      isNum = double.TryParse(
        Convert.ToString(expression),
        System.Globalization.NumberStyles.Any,
        System.Globalization.NumberFormatInfo.InvariantInfo,
        out retNum);

      return isNum;
    }

    /// <summary>
    /// This method checks for the appearance of invalid
    ///   characters for filenames in the given string,
    ///   linke white space or slashes etc.
    /// </summary>
    /// <param name="filename">
    /// A <see cref="string"/> with the filename (without)
    ///   path to check for.
    /// </param>
    /// <returns>
    /// <strong>True</strong>, if filename is valid,
    ///   otherwise <strong>false</strong>.
    /// </returns>
    public static bool IsValidFilename(string filename)
    {
      // Get a list of invalid file characters.
      char[] invalidFileChars = Path.GetInvalidFileNameChars();

      return filename.IndexOfAny(invalidFileChars) < 0;
    }

    /// <summary>
    /// Searches a specified process
    /// </summary>
    /// <param name="name">Name of process, without extension .exe or .dll</param>
    /// <returns>True if process is running, false otherwise</returns>
    public static bool IsProcessOpen(string name)
    {
      return Process.GetProcesses().Any(clsProcess => clsProcess.ProcessName == name);
    }

    /// <summary>
    /// Searches a specified application in windows registries
    /// </summary>
    /// <param name="appName">Name of application</param>
    /// <returns>True if application installed, false otherwise</returns>
    public static bool IsApplicationInstalled(string appName)
    {
      // search in: CurrentUser
      var keyName = @"SOFTWARE";
      if (ExistsInSubKey(Registry.CurrentUser, keyName, "STARTMENU_REGISTRYNAME", appName))
      {
        return true;
      }

      // search in: LocalMachine_32            
      if (ExistsInSubKey(Registry.LocalMachine, keyName, "STARTMENU_REGISTRYNAME", appName))
      {
        return true;
      }

      // search in: LocalMachine_64
      keyName = @"SOFTWARE\Wow6432Node";
      if (ExistsInSubKey(Registry.LocalMachine, keyName, "STARTMENU_REGISTRYNAME", appName))
      {
        return true;
      }

      return false;
    }

    /// <summary>
    /// Find matching application's name with specified subkey's name in subkeys of a root registry directory
    /// </summary>
    /// <param name="root">Registry root</param>
    /// <param name="subKeyName">Searching root</param>
    /// <param name="attributeName">Subkey name to find</param>
    /// <param name="appName">Application name</param>
    /// <returns>True if we found matching subkey name, false otherwise</returns>
    private static bool ExistsInSubKey(RegistryKey root, string subKeyName, string attributeName, string appName)
    {
      using (var key = root.OpenSubKey(subKeyName))
      {
        if (key == null)
        {
          return false;
        }

        foreach (string kn in key.GetSubKeyNames())
        {
          RegistryKey subkey;
          using (subkey = key.OpenSubKey(kn))
          {
            if (subkey == null)
            {
              continue;
            }

            var displayName = subkey.GetValue(attributeName) as string;
            if (appName.Equals(displayName, StringComparison.OrdinalIgnoreCase))
            {
              return true;
            }
          }
        }
      }

      return false;
    }

    #endregion
  }
}