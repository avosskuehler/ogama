// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StimuliTypes.cs" company="Freie Universität Berlin">
//   OGAMA - open gaze and mouse analyzer 
//   Copyright (C) 2015 Dr. Adrian Voßkühler  
//   Licensed under GPL V3
// </copyright>
// <author>Adrian Voßkühler</author>
// <email>adrian@ogama.net</email>
// <summary>
//   Type of stimulus objects that can be created or displayed.
//   Can be None, Shape, Instruction, Image, Flash.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Ogama.Modules.SlideshowDesign
{
  /// <summary>
  ///   Type of stimulus objects that can be created or displayed.
  ///   Can be None, Shape, Instruction, Image, Flash.
  /// </summary>
  public enum StimuliTypes
  {
    /// <summary>
    ///   Undefined stimulus.
    /// </summary>
    None, 

    /// <summary>
    ///   An empty stimulus.
    /// </summary>
    Blank, 

    /// <summary>
    ///   Shape stimuli like <see cref="VectorGraphics.Elements.VGPolyline" />,
    ///   <see cref="VectorGraphics.Elements.VGRectangle" />
    /// </summary>
    Shape, 

    /// <summary>
    ///   Instructional stimuli (textual stimuli).
    /// </summary>
    Instruction, 

    /// <summary>
    ///   Textual stimuli based on RTF format
    /// </summary>
    RTFInstruction, 

    /// <summary>
    ///   Image stimuli, like pictures.
    /// </summary>
    Image, 

    /// <summary>
    ///   Macromedia Flash stimuli.
    /// </summary>
    Flash, 

    /// <summary>
    ///   Browser based stimuli.
    /// </summary>
    Browser, 

    /// <summary>
    ///   The stimulus is the desktop.
    /// </summary>
    Desktop, 

    /// <summary>
    ///   Different types of stimuli
    /// </summary>
    Mixed, 
  }
}