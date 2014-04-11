// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HaythamSetting.cs" company="Freie Universität Berlin">
//   OGAMA - open gaze and mouse analyzer 
//   Copyright (C) 2014 Dr. Adrian Voßkühler  
//   Licensed under GPL V3
// </copyright>
// <author>Adrian Voßkühler</author>
// <email>adrian@ogama.net</email>
// <summary>
//   Class to save settings for the gazegroup gazetracker ip client tracking system.
//   It is XML serializable and can be stored in a file via
//   the <see cref="XmlSerializer" /> class.
//   <example>XmlSerializer serializer = new XmlSerializer(typeof(HaythamSetting));</example>
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Ogama.Modules.Recording.HaythamInterface
{
  using System;
  using System.Xml.Serialization;

  /// <summary>
  ///   Class to save settings for the gazegroup gazetracker ip client tracking system.
  ///   It is XML serializable and can be stored in a file via
  ///   the <see cref="XmlSerializer" /> class.
  ///   <example>XmlSerializer serializer = new XmlSerializer(typeof(HaythamSetting));</example>
  /// </summary>
  [Serializable]
  public class HaythamSetting
  {
    #region Constructors and Destructors

    /// <summary>
    ///   Initializes a new instance of the HaythamSetting class.
    /// </summary>
    public HaythamSetting()
    {
      this.HaythamServerIPAddress = "127.0.0.1";
    }

    #endregion

    #region Public Properties

    /// <summary>
    ///   Gets or sets the data server IP address.
    /// </summary>
    /// <value>A <see cref="string" /> with the server address.</value>
    public string HaythamServerIPAddress { get; set; }

    #endregion
  }
}