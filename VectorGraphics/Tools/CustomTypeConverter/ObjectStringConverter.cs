// <copyright file="ObjectStringConverter.cs" company="FU Berlin">
// ******************************************************
// OGAMA - open gaze and mouse analyzer 
// Copyright (C) 2012 Adrian Voßkühler  
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
  using System.Collections.Generic;
  using System.ComponentModel;
  using System.Drawing;
  using System.Drawing.Drawing2D;
  using System.Globalization;
  using System.Text;
  using System.Windows.Forms;

  /// <summary>
  /// A class with a set of custom object to string (and back) converters.
  /// These are not implemented by the NET Framework.
  /// For example a <see cref="PointF"/> can be converted into a <see cref="string"/> and back.
  /// </summary>
  public static class ObjectStringConverter
  {
    /// <summary>
    /// Returns <see cref="List{PointF}"/> from the given formatted string point list.
    /// </summary>
    /// <param name="strPtList">A string representation of the point list.
    /// In the form "P1:(XXX.XX;YYY.YY) P2:(XXX.XX;YYY.YY) ...".</param>
    /// <returns>A <see cref="List{PointF}"/> with the point list.</returns>
    public static List<PointF> StringToPointFList(string strPtList)
    {
      List<PointF> pointList = new List<PointF>();
      string[] items = strPtList.Trim().Split(' ');

      // Use the decimal separator of the en-US culture.
      NumberFormatInfo nfi = CultureInfo.GetCultureInfo("en-US").NumberFormat;

      foreach (string point in items)
      {
        string newPoint = point.Substring(point.IndexOf('(', 0) + 1);
        newPoint = newPoint.Replace(")", string.Empty);
        string[] numbers = newPoint.Trim().Split(';');

        // Old versions used current culture to convert to
        // string which could be en or de or other
        if (numbers.Length > 0 && numbers[0].Contains(",") && numbers[1].Contains(","))
        {
          nfi = CultureInfo.GetCultureInfo("de-DE").NumberFormat;
        }

        PointF newPointF = new PointF();
        newPointF.X = Convert.ToSingle(numbers[0], nfi);
        newPointF.Y = Convert.ToSingle(numbers[1], nfi);
        pointList.Add(newPointF);
      }

      return pointList;
    }

    /// <summary>
    /// Returns a unique string representation for a <see cref="List{PointF}"/>.
    /// </summary>
    /// <param name="lstPoints">A <see cref="List{PointF}"/> to convert.</param>
    /// <returns>The <see cref="string"/> in the form "P1:(XX.XX;YY.YY) P2:(..."</returns>
    public static string PointFListToString(List<PointF> lstPoints)
    {
      StringBuilder sb = new StringBuilder();

      // Use the decimal separator of the en-US culture.
      NumberFormatInfo nfi = CultureInfo.GetCultureInfo("en-US").NumberFormat;

      for (int i = 0; i < lstPoints.Count; i++)
      {
        PointF pt = lstPoints[i];
        sb.Append("P");
        sb.Append(i);
        sb.Append(":(");
        sb.Append(string.Format(nfi, "{0:F1}", pt.X));
        sb.Append(";");
        sb.Append(string.Format(nfi, "{0:F1}", pt.Y));
        sb.Append(") ");
      }

      return sb.ToString();
    }

    /// <summary>
    /// Returns a <see cref="GraphicsPath"/> from the given formatted string.
    /// </summary>
    /// <param name="strGraphicsPath">A string representation of the graphics path.
    /// In the form "GraphicsPath-SPLIT-PointFList-SPLIT-PathTypes".</param>
    /// <returns>A <see cref="GraphicsPath"/> with the graphics path.</returns>
    public static GraphicsPath StringToGraphicsPath(string strGraphicsPath)
    {
      string[] items = strGraphicsPath.Trim().Split(new string[] { "<SPLIT>" }, StringSplitOptions.None);
      List<PointF> pts = StringToPointFList(items[1]);
      byte[] types = Convert.FromBase64String(items[2]);
      GraphicsPath path = new GraphicsPath(pts.ToArray(), types);
      return path;
    }

    /// <summary>
    /// Returns a unique string representation for a <see cref="GraphicsPath"/>.
    /// </summary>
    /// <param name="path">A <see cref="GraphicsPath"/> to convert.</param>
    /// <returns>The <see cref="string"/> in the form "GraphicsPath-SPLIT-PointFList-SPLIT-PathTypes"</returns>
    public static string GraphicsPathToString(GraphicsPath path)
    {
      StringBuilder output = new StringBuilder();
      output.Append("GraphicsPath");
      output.Append("<SPLIT>");
      output.Append(PointFListToString(new List<PointF>(path.PathPoints)));
      output.Append("<SPLIT>");
      output.Append(Convert.ToBase64String(path.PathTypes));

      return output.ToString();
    }

    /// <summary>
    /// Returns a unique string representation for an ARGB Color value.
    /// </summary>
    /// <param name="c">The <see cref="Color"/> to transform.</param>
    /// <returns>The <see cref="string"/> in the form "AARRGGBB"</returns>
    public static string ColorToHtmlAlpha(Color c)
    {
      return string.Concat(
        c.A.ToString("X2", null),
        c.R.ToString("X2", null),
        c.G.ToString("X2", null),
        c.B.ToString("X2", null));
    }

    /// <summary>
    /// Returns ARGB <see cref="Color"/> structure from given string.
    /// </summary>
    /// <param name="str">A <see cref="String"/> in the form "AARRGGBB"</param>
    /// <returns>A <see cref="Color"/> structur.</returns>
    public static Color HtmlAlphaToColor(string str)
    {
      Color color = Color.Black;
      try
      {
        int a = Convert.ToInt32(str.Substring(0, 2), 16);
        int r = Convert.ToInt32(str.Substring(2, 2), 16);
        int g = Convert.ToInt32(str.Substring(4, 2), 16);
        int b = Convert.ToInt32(str.Substring(6, 2), 16);

        color = Color.FromArgb(a, r, g, b);
      }
      catch (Exception)
      {
        MessageBox.Show("String could not be converted to color. Standard value black will be used.");
      }

      return color;
    }

    /// <summary>
    /// Returns unique string representation of a Brush structure.
    /// </summary>
    /// <remarks>Currently only <see cref="SolidBrush"/>, <see cref="TextureBrush"/>,
    /// or <see cref="HatchBrush"/> are supported.</remarks>
    /// <param name="brush">Brush to convert.</param>
    /// <returns>A <see cref="string"/> representation of the given
    /// <see cref="SolidBrush"/>, <see cref="TextureBrush"/>,
    /// or <see cref="HatchBrush"/>.</returns>
    public static string BrushToString(Brush brush)
    {
      StringBuilder output = new StringBuilder();
      if (brush is SolidBrush)
      {
        SolidBrush solid = (SolidBrush)brush;
        output.Append("SolidBrush");
        output.Append(";");
        output.Append(ColorToHtmlAlpha(solid.Color));
      }
      else if (brush is TextureBrush)
      {
        TextureBrush texture = (TextureBrush)brush;
        output.Append("TextureBrush");
        output.Append(";");
        output.Append(texture.WrapMode.ToString());
        output.Append(";<bmpbytes>");
        TypeConverter bitmapConverter = TypeDescriptor.GetConverter(texture.Image.GetType());
        byte[] bytes = (byte[])bitmapConverter.ConvertTo(texture.Image, typeof(byte[]));
        output.Append(Convert.ToBase64String(bytes));
      }
      else if (brush is HatchBrush)
      {
        HatchBrush hatch = (HatchBrush)brush;
        output.Append("HatchBrush");
        output.Append(";");
        output.Append(hatch.HatchStyle.ToString());
        output.Append(";");
        output.Append(ColorToHtmlAlpha(hatch.ForegroundColor));
        output.Append(";");
        output.Append(ColorToHtmlAlpha(hatch.BackgroundColor));
      }

      return output.ToString();
    }

    /// <summary>
    /// Returns Brush representation of given Brush string,
    /// if the brush was a <see cref="SolidBrush"/>, <see cref="TextureBrush"/>,
    /// or <see cref="HatchBrush"/>.
    /// </summary>
    /// <remarks>All other Brushtypes are currently not serializable.</remarks>
    /// <param name="str">string with brush description.</param>
    /// <returns>A <see cref="SolidBrush"/>, <see cref="TextureBrush"/>,
    /// or <see cref="HatchBrush"/> or null if string is empty.</returns>
    public static Brush StringToBrush(string str)
    {
      if (str == string.Empty)
      {
        return null;
      }

      string delim = ";";
      string[] parts = str.Trim().Split(delim.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
      Brush newValue;
      if (parts[0] == "SolidBrush")
      {
        newValue = new SolidBrush(HtmlAlphaToColor(parts[1]));
      }
      else if (parts[0] == "TextureBrush")
      {
        string[] majorParts = str.Split(new string[] { "<bmpbytes>" }, StringSplitOptions.RemoveEmptyEntries);
        parts = majorParts[0].Trim().Split(delim.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
        WrapMode mode = (WrapMode)Enum.Parse(typeof(WrapMode), parts[1]);
        byte[] btmBytes = Convert.FromBase64String(majorParts[1]);
        Image bitmap = new Bitmap(new System.IO.MemoryStream(btmBytes));
        newValue = new TextureBrush(bitmap, mode);
      }
      else if (parts[0] == "HatchBrush")
      {
        HatchStyle style = (HatchStyle)Enum.Parse(typeof(HatchStyle), parts[1]);
        Color foreground = HtmlAlphaToColor(parts[2]);
        Color background = HtmlAlphaToColor(parts[3]);
        newValue = new HatchBrush(style, foreground, background);
      }
      else
      {
        newValue = SystemBrushes.ButtonHighlight;
      }

      return newValue;
    }

    /// <summary>
    /// Obsolete. Returns Brush representation of given Brush string,
    /// if the brush was a SolidBrush.
    /// </summary>
    /// <remarks>This method is only for compatibility reasons.</remarks>
    /// <param name="str">A <see cref="String"/> with the brush representation
    /// in OGAMA 1.0 format.</param>
    /// <returns>The converted <see cref="Brush"/></returns>
    public static Brush StringToBrushOld(string str)
    {
      string delim = "####";
      string[] parts = str.Trim().Split(delim.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
      Brush newValue;
      if (parts[0] == "SolidBrush")
      {
        newValue = new SolidBrush(HtmlAlphaToColor(parts[1]));
      }
      else
      {
        newValue = SystemBrushes.AppWorkspace;
      }

      return newValue;
    }

    /// <summary>
    /// Returns unique string representation of a Pen structure.
    /// </summary>
    /// <param name="newPen">The <see cref="Pen"/> to convert.</param>
    /// <returns>A <see cref="String"/> with Color, Width, DashStyle and arrows.</returns>
    public static string PenToString(Pen newPen)
    {
      StringBuilder output = new StringBuilder();
      output.Append("Color: ");
      output.Append(ObjectStringConverter.ColorToHtmlAlpha(newPen.Color));
      output.Append(";");
      output.Append(newPen.Width.ToString());
      output.Append(" px");
      output.Append(";");
      output.Append(newPen.DashStyle.ToString());
      output.Append(";");
      output.Append("StartCap :");
      output.Append(newPen.StartCap.ToString());
      output.Append(";");
      output.Append("EndCap :");
      output.Append(newPen.EndCap.ToString());
      output.Append(";");

      output.Append("CustomStartCap AdjustableArrow:");
      if (newPen.StartCap == LineCap.Custom)
      {
        output.Append(((AdjustableArrowCap)newPen.CustomStartCap).Width.ToString());
        output.Append(":");
        output.Append(((AdjustableArrowCap)newPen.CustomStartCap).Height.ToString());
      }

      output.Append(";");
      output.Append("CustomEndCap AdjustableArrow:");
      if (newPen.EndCap == LineCap.Custom)
      {
        output.Append(((AdjustableArrowCap)newPen.CustomEndCap).Width.ToString());
        output.Append(":");
        output.Append(((AdjustableArrowCap)newPen.CustomEndCap).Height.ToString());
      }

      return output.ToString();
    }

    /// <summary>
    /// Returns Pen representation of given pen string .
    /// </summary>
    /// <param name="str">A <see cref="String"/> with Color, Width, DashStyle and arrows.</param>
    /// <returns>The converted <see cref="Pen"/></returns>
    public static Pen StringToPen(string str)
    {
      Pen newValue = Pens.Black;
      try
      {
        string[] parts = str.Trim().Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
        newValue = new Pen(
          ObjectStringConverter.HtmlAlphaToColor(parts[0].Remove(0, 7)),
          float.Parse(parts[1].Replace(" px", string.Empty), System.Globalization.NumberStyles.Number));

        newValue.DashStyle = (DashStyle)Enum.Parse(typeof(DashStyle), parts[2]);
        if (parts.Length > 3)
        {
          string[] parts3 = parts[3].Trim().Split(new string[] { ":" }, StringSplitOptions.RemoveEmptyEntries);
          string[] parts4 = parts[4].Trim().Split(new string[] { ":" }, StringSplitOptions.RemoveEmptyEntries);
          LineCap startCap = (LineCap)Enum.Parse(typeof(LineCap), parts3[1]);
          LineCap endCap = (LineCap)Enum.Parse(typeof(LineCap), parts4[1]);
          newValue.StartCap = startCap;
          newValue.EndCap = endCap;
          if (startCap == LineCap.Custom)
          {
            string[] parts5 = parts[5].Trim().Split(new string[] { ":" }, StringSplitOptions.RemoveEmptyEntries);
            AdjustableArrowCap customStartCap = new AdjustableArrowCap(float.Parse(parts5[1]), float.Parse(parts5[2]));
            newValue.CustomStartCap = customStartCap;
          }

          if (endCap == LineCap.Custom)
          {
            string[] parts6 = parts[6].Trim().Split(new string[] { ":" }, StringSplitOptions.RemoveEmptyEntries);
            AdjustableArrowCap customEndCap = new AdjustableArrowCap(float.Parse(parts6[1]), float.Parse(parts6[2]));
            newValue.CustomEndCap = customEndCap;
          }
        }
      }
      catch (Exception)
      {
        MessageBox.Show("String could not be converted to pen. Standard black pen will be used.");
      }

      return newValue;
    }

    /// <summary>
    /// Obsolete. Returns Pen representation of given pen string 
    /// </summary>
    /// <remarks>This method is only for compatibility reasons.</remarks>
    /// <param name="str">A <see cref="String"/> representing the pen in Ogama 1.0 versions.</param>
    /// <returns>The converted <see cref="Pen"/>.</returns>
    public static Pen StringToPenOld(string str)
    {
      string[] parts = str.Trim().Split(new string[] { "####" }, StringSplitOptions.RemoveEmptyEntries);
      Pen newValue = new Pen(
        ObjectStringConverter.HtmlAlphaToColor(parts[0]),
        float.Parse(parts[1], System.Globalization.NumberStyles.Number));
      newValue.DashStyle = (DashStyle)Enum.Parse(typeof(DashStyle), parts[2]);
      return newValue;
    }

    /// <summary>
    /// Returns unique string representation of a point structure,
    /// of type "(PosX,PosY)"
    /// </summary>
    /// <param name="pt">The <see cref="Point"/> to convert.</param>
    /// <returns>A <see cref="String"/> in the format "(PosX,PosY)"</returns>
    public static string PointToString(Point pt)
    {
      StringBuilder output = new StringBuilder();

      NumberFormatInfo nfi = new NumberFormatInfo();
      nfi.CurrencyDecimalSeparator = ".";
      nfi.CurrencyGroupSeparator = string.Empty;
      nfi.NumberDecimalSeparator = ".";
      nfi.NumberGroupSeparator = string.Empty;

      output.Append("(");
      output.Append(pt.X.ToString("N0", nfi));
      output.Append(",");
      output.Append(pt.Y.ToString("N0", nfi));
      output.Append(")");

      return output.ToString();
    }

    /// <summary>
    /// Returns point representation of given point string of
    /// the form "(PosX,PosY)"
    /// </summary>
    /// <param name="str">A <see cref="String"/> in the format "(PosX,PosY)"</param>
    /// <returns>The converted <see cref="Point"/></returns>
    public static Point StringToPoint(string str)
    {
      string delim = "()";
      string str2 = str.Trim(delim.ToCharArray());
      string[] numbers = str2.Trim().Split(',');
      Point newValue = new Point(
        int.Parse(numbers[0], System.Globalization.NumberStyles.Number),
        int.Parse(numbers[1], System.Globalization.NumberStyles.Number));

      return newValue;
    }

    /// <summary>
    /// Obsolete. Returns a <see cref="Font"/> from the given string format.
    /// </summary>
    /// <remarks>This method is only for compatibility reasons.</remarks>
    /// <param name="formattedFont">A string representation of a font to retrieve.</param>
    /// <returns>A <see cref="Font"/> which represents the given string.</returns>
    public static Font StringToFontOld(string formattedFont)
    {
      if (formattedFont == null)
      {
        return SystemFonts.MenuFont;
      }

      string[] properties = formattedFont.Split('#');

      if (properties.Length == 6)
      {
        FontStyle style = FontStyle.Regular;

        if (properties[2].Contains("Bold"))
        {
          style |= FontStyle.Bold;
        }

        if (properties[2].Contains("Italic"))
        {
          style |= FontStyle.Italic;
        }

        if (properties[2].Contains("Strikeout"))
        {
          style |= FontStyle.Strikeout;
        }

        if (properties[2].Contains("Underline"))
        {
          style |= FontStyle.Underline;
        }

        GraphicsUnit unit;
        switch (properties[3])
        {
          case "Display":
            unit = GraphicsUnit.Display;
            break;
          case "Document":
            unit = GraphicsUnit.Document;
            break;
          case "Inch":
            unit = GraphicsUnit.Inch;
            break;
          case "Millimeter":
            unit = GraphicsUnit.Millimeter;
            break;
          case "Pixel":
            unit = GraphicsUnit.Pixel;
            break;
          case "Point":
            unit = GraphicsUnit.Point;
            break;
          case "World":
            unit = GraphicsUnit.World;
            break;
          default:
            unit = GraphicsUnit.Display;
            break;
        }

        Font newFont = new Font(
          properties[0],
          float.Parse(properties[1]),
          style,
          unit,
          byte.Parse(properties[4]),
          bool.Parse(properties[5]));

        return newFont;
      }

      return SystemFonts.MenuFont;
    }

    /// <summary>
    /// Returns <see cref="Color"/> value from given RGB string.
    /// </summary>
    /// <param name="formattedColor">A <see cref="string"/> with the string formatted color
    /// using <see cref="Color.ToString()"/> method. </param>
    /// <returns>A <see cref="Color"/> representing the string.</returns>
    public static Color StringToColor(string formattedColor)
    {
      int test;
      if (int.TryParse(formattedColor, System.Globalization.NumberStyles.HexNumber, null, out test))
      {
        return Color.FromArgb(int.Parse(formattedColor, System.Globalization.NumberStyles.HexNumber));
      }
      else
      {
        return Color.FromName(formattedColor);
      }
    }
  }
}
