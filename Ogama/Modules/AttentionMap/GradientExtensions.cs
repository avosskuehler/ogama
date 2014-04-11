// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GradientExtensions.cs" company="Freie Universität Berlin">
//   OGAMA - open gaze and mouse analyzer 
//   Copyright (C) 2014 Dr. Adrian Voßkühler  
//   Licensed under GPL V3
// </copyright>
// <author>Adrian Voßkühler</author>
// <email>adrian@ogama.net</email>
// <summary>
//   Defines the GradientExtensions type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Ogama.Modules.AttentionMap
{
  using System.Linq;
  using System.Windows.Media;

  using Color = System.Drawing.Color;

  /// <summary>
  /// </summary>
  public static class GradientExtensions
  {
    #region Public Methods and Operators

    /// <summary>
    /// Gets the color of the relative.
    /// </summary>
    /// <param name="gsc">
    /// The GSC.
    /// </param>
    /// <param name="offset">
    /// The offset.
    /// </param>
    /// <returns>
    /// The <see cref="Color"/>.
    /// </returns>
    public static Color GetRelativeColor(this GradientStopCollection gsc, double offset)
    {
      GradientStop before = gsc.First(w => w.Offset == gsc.Min(m => m.Offset));
      GradientStop after = gsc.First(w => w.Offset == gsc.Max(m => m.Offset));

      foreach (GradientStop gs in gsc)
      {
        if (gs.Offset < offset && gs.Offset > before.Offset)
        {
          before = gs;
        }

        if (gs.Offset > offset && gs.Offset < after.Offset)
        {
          after = gs;
        }
      }

      var a =
        (float)
        ((offset - before.Offset) * (after.Color.ScA - before.Color.ScA) / (after.Offset - before.Offset)
         + before.Color.ScA);
      var r =
        (float)
        ((offset - before.Offset) * (after.Color.ScR - before.Color.ScR) / (after.Offset - before.Offset)
         + before.Color.ScR);
      var g =
        (float)
        ((offset - before.Offset) * (after.Color.ScG - before.Color.ScG) / (after.Offset - before.Offset)
         + before.Color.ScG);
      var b =
        (float)
        ((offset - before.Offset) * (after.Color.ScB - before.Color.ScB) / (after.Offset - before.Offset)
         + before.Color.ScB);
      Color color = Color.FromArgb((int)(a * 255), (int)(r * 255), (int)(g * 255), (int)(b * 255));

      return color;
    }

    #endregion
  }
}