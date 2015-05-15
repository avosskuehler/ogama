// <copyright file="RecentFileList.cs" company="FU Berlin">
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

namespace Ogama.Properties
{
  using System.Collections.Specialized;
  using System.IO;
  using System.Text;

  /// <summary>
  /// Derived from <see cref="StringCollection"/>.
  /// Class to handle a string collection with the last recently used files.
  /// Its of a singleton class type, to always have one unique complete list,
  /// if you call <code>RecentFilesList.List</code>
  /// </summary>
  public class RecentFilesList : StringCollection
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
    /// The static member, that holds the recent files list.
    /// </summary>
    private static RecentFilesList recentFiles;

    /// <summary>
    /// Maximum number of items in recent files list.
    /// </summary>
    private static decimal maxNumItems;

    /// <summary>
    /// The application settings of the main program,
    /// where the property RecentFiles has the StringCollection.
    /// </summary>
    private Ogama.Properties.Settings appSettings;

    /// <summary>
    /// maximum length of file name for display in recent file list
    /// </summary>
    private int maxLengthDisplay = 40;

    #endregion //FIELDS

    ///////////////////////////////////////////////////////////////////////////////
    // Construction and Initializing methods                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region CONSTRUCTION

    /// <summary>
    /// Prevents a default instance of the RecentFilesList class from being created.
    /// Initializes Recentfiles list by reading application settings.
    /// </summary>
    private RecentFilesList()
    {
      this.appSettings = new Ogama.Properties.Settings();
      maxNumItems = this.appSettings.NumberOfRecentFiles;
      this.Load();
    }

    #endregion //CONSTRUCTION

    ///////////////////////////////////////////////////////////////////////////////
    // Defining Properties                                                       //
    ///////////////////////////////////////////////////////////////////////////////
    #region PROPERTIES

    /// <summary>
    /// Gets the recent files list.
    /// </summary>
    /// <value>A <see cref="RecentFilesList"/> with the recent files.</value>
    public static RecentFilesList List
    {
      get
      {
        if (recentFiles == null)
        {
          recentFiles = new RecentFilesList();
        }

        return recentFiles;
      }
    }

    #endregion //PROPERTIES

    ///////////////////////////////////////////////////////////////////////////////
    // Public methods                                                            //
    ///////////////////////////////////////////////////////////////////////////////
    #region PUBLICMETHODS

    /// <summary>
    /// Stores current recent files list into application settings,
    /// "|" separated.
    /// </summary>
    public void Save()
    {
      StringBuilder stringBuilder = new StringBuilder();
      bool first = true;
      foreach (string file in this)
      {
        if (first)
        {
          first = false;
        }
        else
        {
          stringBuilder.Append('|');
        }

        stringBuilder.Append(file);
      }

      this.appSettings.RecentFileList = stringBuilder.ToString();
      this.appSettings.Save();
    }

    /// <summary>
    /// Adds new filename to recent files list and saves list to application settings.
    /// </summary>
    /// <param name="file">A <see cref="string"/> with full path and
    /// filename to recent file</param>
    public new void Add(string file)
    {
      int fileIndex = this.FindFile(file);

      if (fileIndex < 0)
      {
        this.Insert(0, file);

        while (this.Count > maxNumItems)
        {
          this.RemoveAt(this.Count - 1);
        }
      }
      else
      {
        this.RemoveAt(fileIndex);
        this.Insert(0, file);
      }

      this.Save();
    }

    /// <summary>
    /// Removes filename from recent files list and saves list to application settings.
    /// </summary>
    /// <param name="file">A <see cref="string"/> with full path and
    /// filename to recent file</param>
    public new void Remove(string file)
    {
      int fileIndex = this.FindFile(file);

      if (fileIndex < 0)
      {
        this.Insert(0, file);

        while (this.Count > maxNumItems)
        {
          this.RemoveAt(this.Count - 1);
        }
      }
      else
      {
        this.RemoveAt(fileIndex);
      }

      this.Save();
    }

    /// <summary>
    /// Deletes the recent files list in application settings.
    /// </summary>
    public void Delete()
    {
      this.appSettings.RecentFileList = string.Empty;
      this.appSettings.Save();
    }

    /// <summary>
    /// Get display file name from full name.
    /// </summary>
    /// <param name="fullName">Full file name</param>
    /// <returns>A <see cref="string"/> with a short display name</returns>
    public string GetDisplayName(string fullName)
    {
      // if file is in current directory, show only file name
      FileInfo fileInfo = new FileInfo(fullName);

      // keep current directory in the time of initialization
      string currentDirectory = Directory.GetCurrentDirectory();

      if (fileInfo.DirectoryName == currentDirectory)
      {
        return fileInfo.ToString().Substring(0, fileInfo.ToString().Length > this.maxLengthDisplay ? this.maxLengthDisplay : fileInfo.ToString().Length);
      }

      string filename = Path.GetFileName(fullName);
      if (filename.Length > this.maxLengthDisplay)
      {
        filename = filename.Substring(0, this.maxLengthDisplay);
      }

      return filename;
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
    /// <summary>
    /// Loads "|" separated values from application settings into StringCollection base class.
    /// </summary>
    private void Load()
    {
      string listEntry = this.appSettings.RecentFileList;
      if (listEntry != null)
      {
        string[] files = listEntry.Split(new char[] { '|' });
        foreach (string file in files)
        {
          base.Add(file);
        }
      }
    }

    /// <summary>
    /// Get index in recent file list from given file path.
    /// </summary>
    /// <param name="file">Full path name of search file.</param>
    /// <returns>Index in file list, if not found -1.</returns>
    private int FindFile(string file)
    {
      string fileLower = file.ToLower();
      for (int index = 0; index < this.Count; index++)
      {
        if (fileLower == this[index].ToLower())
        {
          return index;
        }
      }

      return -1;
    }

    #endregion //METHODS

    ///////////////////////////////////////////////////////////////////////////////
    // Small helping Methods                                                     //
    ///////////////////////////////////////////////////////////////////////////////
    #region HELPER
    #endregion //HELPER
  }
}