// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AOIDataSettings.cs" company="Freie Universität Berlin">
//   OGAMA - open gaze and mouse analyzer 
//   Copyright (C) 2014 Dr. Adrian Voßkühler  
//   Licensed under GPL V3
// </copyright>
// <author>Adrian Voßkühler</author>
// <email>adrian@ogama.net</email>
// <summary>
//   List of settings that will be used while importing AOI data.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Ogama.Modules.ImportExport.AOIData
{
  using System;

  using Ogama.Modules.ImportExport.Common;

  /// <summary>
  ///   List of settings that will be used while importing AOI data.
  /// </summary>
  [Serializable]
  public class AOIDataSettings
  {
    #region Fields

    /// <summary>
    ///   Saves the <see cref="AOIImportModes" /> choosen during import.
    /// </summary>
    private AOIImportModes importMode;

    #endregion

    #region Constructors and Destructors

    /// <summary>
    ///   Initializes a new instance of the AOIDataSettings class.
    /// </summary>
    public AOIDataSettings()
    {
      this.importMode = AOIImportModes.UseOgamaColumns;
    }

    #endregion

    #region Public Properties

    /// <summary>
    ///   Gets or sets the <see cref="AOIImportModes" /> choosen during import.
    /// </summary>
    /// <value>A <see cref="AOIImportModes" /> with the current mode.</value>
    public AOIImportModes ImportMode
    {
      get
      {
        return this.importMode;
      }

      set
      {
        this.importMode = value;
      }
    }

    #endregion
  }
}