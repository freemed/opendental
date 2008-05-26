#region MigraDoc - Creating Documents on the Fly
//
// Authors:
//   Stefan Lange (mailto:Stefan.Lange@pdfsharp.com)
//   Klaus Potzesny (mailto:Klaus.Potzesny@pdfsharp.com)
//   David Stephensen (mailto:David.Stephensen@pdfsharp.com)
//
// Copyright (c) 2001-2007 empira Software GmbH, Cologne (Germany)
//
// http://www.pdfsharp.com
// http://www.migradoc.com
// http://sourceforge.net/projects/pdfsharp
//
// Permission is hereby granted, free of charge, to any person obtaining a
// copy of this software and associated documentation files (the "Software"),
// to deal in the Software without restriction, including without limitation
// the rights to use, copy, modify, merge, publish, distribute, sublicense,
// and/or sell copies of the Software, and to permit persons to whom the
// Software is furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included
// in all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL
// THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
// FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER 
// DEALINGS IN THE SOFTWARE.
#endregion

using System;
using System.Collections;
using System.Diagnostics;
using System.Reflection;
using MigraDoc.DocumentObjectModel.Internals;

namespace MigraDoc.DocumentObjectModel
{
  /// <summary>
  /// The Color class represents an ARGB color value.
  /// </summary>
  public struct Color : INullableValue
  {
    /// <summary>
    /// Initializes a new instance of the Color class.
    /// </summary>
    public Color(uint argb)
    {
      this.argb = argb;
    }

    /// <summary>
    /// Initializes a new instance of the Color class.
    /// </summary>
    public Color(byte r, byte g, byte b)
    {
      this.argb = 0xFF000000 | ((uint)r << 16) | ((uint)g << 8) | (uint)b;
    }

    /// <summary>
    /// Initializes a new instance of the Color class.
    /// </summary>
    public Color(byte a, byte r, byte g, byte b)
    {
      this.argb = ((uint)a << 24) | ((uint)r << 16) | ((uint)g << 8) | (uint)b;
    }

    /// <summary>
    /// Determines whether this color is empty.
    /// </summary>
    public bool IsEmpty
    {
      get { return this == Color.Empty; }
    }

    /// <summary>
    /// Returns the value.
    /// </summary>
    object INullableValue.GetValue()
    {
      return this;
    }

    /// <summary>
    /// Sets the given value.
    /// </summary>
    void INullableValue.SetValue(object value)
    {
      if (value is uint)
        this.argb = (uint)value;
      else
        this = Color.Parse(value.ToString());
    }

    /// <summary>
    /// Resets this instance, i.e. IsNull() will return true afterwards.
    /// </summary>
    void INullableValue.SetNull()
    {
      this = Color.Empty;
    }

    /// <summary>
    /// Determines whether this instance is null (not set).
    /// </summary>
    bool INullableValue.IsNull
    {
      get { return this == Color.Empty; }
    }

    /// <summary>
    /// Determines whether this instance is null (not set).
    /// </summary>
    internal bool IsNull
    {
      get { return this == Color.Empty; }
    }

    /// <summary>
    /// Gets or sets the ARGB value.
    /// </summary>
    public uint Argb
    {
      get { return argb; }
      set { argb = value; }
    }
    uint argb;

    /// <summary>
    /// Gets or sets the RGB value.
    /// </summary>
    public uint RGB
    {
      get { return argb; }
      set { argb = value; }
    }

    /// <summary>
    /// Calls base class Equals.
    /// </summary>
    public override bool Equals(Object obj)
    {
      if (obj is Color)
        return this.argb == ((Color)obj).argb;
      return false;
    }

    /// <summary>
    /// Gets the ARGB value that this Color instance represents.
    /// </summary>
    public override int GetHashCode()
    {
      return (int)this.argb;
    }

    /// <summary>
    /// Compares two color objects. True if both argb values are equal, false otherwise.
    /// </summary>
    public static bool operator ==(Color clr1, Color clr2)
    {
      return clr1.argb == clr2.argb;
    }

    /// <summary>
    /// Compares two color objects. True if both argb values are not equal, false otherwise.
    /// </summary>
    public static bool operator !=(Color clr1, Color clr2)
    {
      return clr1.argb != clr2.argb;
    }

    /// <summary>
    /// Parses the string and returns a color object.
    /// Throws ArgumentException if color is invalid.
    /// </summary>
    /// <param name="color">integer, hex or color name.</param>
    public static Color Parse(string color)
    {
      if (color == null)
        throw new ArgumentNullException("color");
      if (color == "")
        throw new ArgumentException("color");

      try
      {
        uint clr = 0;
        // Must use Enum.Parse because Enum.IsDefined is case sensitive
        try
        {
          object obj = Enum.Parse(typeof(ColorName), color, true);
          clr = (uint)obj;
          return new Color(clr);
        }
        catch
        {
          //ignore exception cause it's not a ColorName.
        }

        System.Globalization.NumberStyles numberStyle = System.Globalization.NumberStyles.Integer;
        string number = color.ToLower();
        if (number.StartsWith("0x"))
        {
          numberStyle = System.Globalization.NumberStyles.HexNumber;
          number = color.Substring(2);
        }
        clr = uint.Parse(number, numberStyle);
        return new Color(clr);
      }
      catch (FormatException ex)
      {
        throw new ArgumentException(DomSR.InvalidColorString(color), ex);
      }
    }

    /// <summary>
    /// Gets the alpha (transparency) part of the Color.
    /// </summary>
    public uint A
    {
      get { return (this.argb & 0xFF000000) >> 24; }
    }

    /// <summary>
    /// Gets the red part of the Color.
    /// </summary>
    public uint R
    {
      get { return (this.argb & 0xFF0000) >> 16; }
    }

    /// <summary>
    /// Gets the green part of the Color.
    /// </summary>
    public uint G
    {
      get { return (this.argb & 0x00FF00) >> 8; }
    }

    /// <summary>
    /// Gets the blue part of the Color.
    /// </summary>
    public uint B
    {
      get { return this.argb & 0x0000FF; }
    }

    /// <summary>
    /// Gets a non transparent color brightened in terms of transparency if any is given(A &lt; 255),
    /// otherwise this instance itself.
    /// </summary>
    public Color GetMixedTransparencyColor()
    {
      int alpha = (int)A;
      if (alpha == 0xFF)
        return this;

      int red = (int)R;
      int green = (int)G;
      int blue = (int)B;

      double whiteFactor = 1 - alpha / 255.0;

      red = (int)(red + (255 - red) * whiteFactor);
      green = (int)(green + (255 - green) * whiteFactor);
      blue = (int)(blue + (255 - blue) * whiteFactor);
      return new Color((uint)(0xFF << 24 | (red << 16) | (green << 8) | blue));
    }

    /// <summary>
    /// Writes the Color object in its hexadecimal value.
    /// </summary>
    public override string ToString()
    {
      if (stdColors == null)
      {
        Array colorNames = Enum.GetNames(typeof(ColorName));
        Array colorValues = Enum.GetValues(typeof(ColorName));
        int count = colorNames.GetLength(0);
        stdColors = new Hashtable(count);
        for (int index = 0; index < count; index++)
        {
          string c = (string)colorNames.GetValue(index);
          uint d = (uint)colorValues.GetValue(index);
          // Some color are double named...
          // Aqua == Cyan
          // Fuchsia == Magenta
          if (!stdColors.ContainsKey(d))
            stdColors.Add(d, c);
        }
      }
      if (stdColors.ContainsKey(argb))
        return (string)stdColors[argb];
      else
      {
        if ((this.argb & 0xFF000000) == 0xFF000000)
          return "RGB(" +
            ((this.argb & 0xFF0000) >> 16).ToString() + "," +
            ((this.argb & 0x00FF00) >> 8).ToString() + "," +
            (this.argb & 0x0000FF).ToString() + ")";
        else
          return "0x" + argb.ToString("X");
      }
    }
    static Hashtable stdColors;

    /// <summary>
    /// Represents a null color.
    /// </summary>
    public static readonly Color Empty = new Color(0);

    // TODO: delete 07-12-31
    // Predefined colors in Color are obsolete. Use Colors class.
    [Obsolete("Use class Colors for predefined colors.")]
    public static readonly Color AliceBlue = new Color(0xFFF0F8FF);
    [Obsolete("Use class Colors for predefined colors.")]
    public static readonly Color AntiqueWhite = new Color(0xFFFAEBD7);
    [Obsolete("Use class Colors for predefined colors.")]
    public static readonly Color Aqua = new Color(0xFF00FFFF);
    [Obsolete("Use class Colors for predefined colors.")]
    public static readonly Color Aquamarine = new Color(0xFF7FFFD4);
    [Obsolete("Use class Colors for predefined colors.")]
    public static readonly Color Azure = new Color(0xFFF0FFFF);
    [Obsolete("Use class Colors for predefined colors.")]
    public static readonly Color Beige = new Color(0xFFF5F5DC);
    [Obsolete("Use class Colors for predefined colors.")]
    public static readonly Color Bisque = new Color(0xFFFFE4C4);
    [Obsolete("Use class Colors for predefined colors.")]
    public static readonly Color Black = new Color(0xFF000000);
    [Obsolete("Use class Colors for predefined colors.")]
    public static readonly Color BlanchedAlmond = new Color(0xFFFFEBCD);
    [Obsolete("Use class Colors for predefined colors.")]
    public static readonly Color Blue = new Color(0xFF0000FF);
    [Obsolete("Use class Colors for predefined colors.")]
    public static readonly Color BlueViolet = new Color(0xFF8A2BE2);
    [Obsolete("Use class Colors for predefined colors.")]
    public static readonly Color Brown = new Color(0xFFA52A2A);
    [Obsolete("Use class Colors for predefined colors.")]
    public static readonly Color BurlyWood = new Color(0xFFDEB887);
    [Obsolete("Use class Colors for predefined colors.")]
    public static readonly Color CadetBlue = new Color(0xFF5F9EA0);
    [Obsolete("Use class Colors for predefined colors.")]
    public static readonly Color Chartreuse = new Color(0xFF7FFF00);
    [Obsolete("Use class Colors for predefined colors.")]
    public static readonly Color Chocolate = new Color(0xFFD2691E);
    [Obsolete("Use class Colors for predefined colors.")]
    public static readonly Color Coral = new Color(0xFFFF7F50);
    [Obsolete("Use class Colors for predefined colors.")]
    public static readonly Color CornflowerBlue = new Color(0xFF6495ED);
    [Obsolete("Use class Colors for predefined colors.")]
    public static readonly Color Cornsilk = new Color(0xFFFFF8DC);
    [Obsolete("Use class Colors for predefined colors.")]
    public static readonly Color Crimson = new Color(0xFFDC143C);
    [Obsolete("Use class Colors for predefined colors.")]
    public static readonly Color Cyan = new Color(0xFF00FFFF);
    [Obsolete("Use class Colors for predefined colors.")]
    public static readonly Color DarkBlue = new Color(0xFF00008B);
    [Obsolete("Use class Colors for predefined colors.")]
    public static readonly Color DarkCyan = new Color(0xFF008B8B);
    [Obsolete("Use class Colors for predefined colors.")]
    public static readonly Color DarkGoldenrod = new Color(0xFFB8860B);
    [Obsolete("Use class Colors for predefined colors.")]
    public static readonly Color DarkGray = new Color(0xFFA9A9A9);
    [Obsolete("Use class Colors for predefined colors.")]
    public static readonly Color DarkGreen = new Color(0xFF006400);
    [Obsolete("Use class Colors for predefined colors.")]
    public static readonly Color DarkKhaki = new Color(0xFFBDB76B);
    [Obsolete("Use class Colors for predefined colors.")]
    public static readonly Color DarkMagenta = new Color(0xFF8B008B);
    [Obsolete("Use class Colors for predefined colors.")]
    public static readonly Color DarkOliveGreen = new Color(0xFF556B2F);
    [Obsolete("Use class Colors for predefined colors.")]
    public static readonly Color DarkOrange = new Color(0xFFFF8C00);
    [Obsolete("Use class Colors for predefined colors.")]
    public static readonly Color DarkOrchid = new Color(0xFF9932CC);
    [Obsolete("Use class Colors for predefined colors.")]
    public static readonly Color DarkRed = new Color(0xFF8B0000);
    [Obsolete("Use class Colors for predefined colors.")]
    public static readonly Color DarkSalmon = new Color(0xFFE9967A);
    [Obsolete("Use class Colors for predefined colors.")]
    public static readonly Color DarkSeaGreen = new Color(0xFF8FBC8B);
    [Obsolete("Use class Colors for predefined colors.")]
    public static readonly Color DarkSlateBlue = new Color(0xFF483D8B);
    [Obsolete("Use class Colors for predefined colors.")]
    public static readonly Color DarkSlateGray = new Color(0xFF2F4F4F);
    [Obsolete("Use class Colors for predefined colors.")]
    public static readonly Color DarkTurquoise = new Color(0xFF00CED1);
    [Obsolete("Use class Colors for predefined colors.")]
    public static readonly Color DarkViolet = new Color(0xFF9400D3);
    [Obsolete("Use class Colors for predefined colors.")]
    public static readonly Color DeepPink = new Color(0xFFFF1493);
    [Obsolete("Use class Colors for predefined colors.")]
    public static readonly Color DeepSkyBlue = new Color(0xFF00BFFF);
    [Obsolete("Use class Colors for predefined colors.")]
    public static readonly Color DimGray = new Color(0xFF696969);
    [Obsolete("Use class Colors for predefined colors.")]
    public static readonly Color DodgerBlue = new Color(0xFF1E90FF);
    [Obsolete("Use class Colors for predefined colors.")]
    public static readonly Color Firebrick = new Color(0xFFB22222);
    [Obsolete("Use class Colors for predefined colors.")]
    public static readonly Color FloralWhite = new Color(0xFFFFFAF0);
    [Obsolete("Use class Colors for predefined colors.")]
    public static readonly Color ForestGreen = new Color(0xFF228B22);
    [Obsolete("Use class Colors for predefined colors.")]
    public static readonly Color Fuchsia = new Color(0xFFFF00FF);
    [Obsolete("Use class Colors for predefined colors.")]
    public static readonly Color Gainsboro = new Color(0xFFDCDCDC);
    [Obsolete("Use class Colors for predefined colors.")]
    public static readonly Color GhostWhite = new Color(0xFFF8F8FF);
    [Obsolete("Use class Colors for predefined colors.")]
    public static readonly Color Gold = new Color(0xFFFFD700);
    [Obsolete("Use class Colors for predefined colors.")]
    public static readonly Color Goldenrod = new Color(0xFFDAA520);
    [Obsolete("Use class Colors for predefined colors.")]
    public static readonly Color Gray = new Color(0xFF808080);
    [Obsolete("Use class Colors for predefined colors.")]
    public static readonly Color Green = new Color(0xFF008000);
    [Obsolete("Use class Colors for predefined colors.")]
    public static readonly Color GreenYellow = new Color(0xFFADFF2F);
    [Obsolete("Use class Colors for predefined colors.")]
    public static readonly Color Honeydew = new Color(0xFFF0FFF0);
    [Obsolete("Use class Colors for predefined colors.")]
    public static readonly Color HotPink = new Color(0xFFFF69B4);
    [Obsolete("Use class Colors for predefined colors.")]
    public static readonly Color IndianRed = new Color(0xFFCD5C5C);
    [Obsolete("Use class Colors for predefined colors.")]
    public static readonly Color Indigo = new Color(0xFF4B0082);
    [Obsolete("Use class Colors for predefined colors.")]
    public static readonly Color Ivory = new Color(0xFFFFFFF0);
    [Obsolete("Use class Colors for predefined colors.")]
    public static readonly Color Khaki = new Color(0xFFF0E68C);
    [Obsolete("Use class Colors for predefined colors.")]
    public static readonly Color Lavender = new Color(0xFFE6E6FA);
    [Obsolete("Use class Colors for predefined colors.")]
    public static readonly Color LavenderBlush = new Color(0xFFFFF0F5);
    [Obsolete("Use class Colors for predefined colors.")]
    public static readonly Color LawnGreen = new Color(0xFF7CFC00);
    [Obsolete("Use class Colors for predefined colors.")]
    public static readonly Color LemonChiffon = new Color(0xFFFFFACD);
    [Obsolete("Use class Colors for predefined colors.")]
    public static readonly Color LightBlue = new Color(0xFFADD8E6);
    [Obsolete("Use class Colors for predefined colors.")]
    public static readonly Color LightCoral = new Color(0xFFF08080);
    [Obsolete("Use class Colors for predefined colors.")]
    public static readonly Color LightCyan = new Color(0xFFE0FFFF);
    [Obsolete("Use class Colors for predefined colors.")]
    public static readonly Color LightGoldenrodYellow = new Color(0xFFFAFAD2);
    [Obsolete("Use class Colors for predefined colors.")]
    public static readonly Color LightGray = new Color(0xFFD3D3D3);
    [Obsolete("Use class Colors for predefined colors.")]
    public static readonly Color LightGreen = new Color(0xFF90EE90);
    [Obsolete("Use class Colors for predefined colors.")]
    public static readonly Color LightPink = new Color(0xFFFFB6C1);
    [Obsolete("Use class Colors for predefined colors.")]
    public static readonly Color LightSalmon = new Color(0xFFFFA07A);
    [Obsolete("Use class Colors for predefined colors.")]
    public static readonly Color LightSeaGreen = new Color(0xFF20B2AA);
    [Obsolete("Use class Colors for predefined colors.")]
    public static readonly Color LightSkyBlue = new Color(0xFF87CEFA);
    [Obsolete("Use class Colors for predefined colors.")]
    public static readonly Color LightSlateGray = new Color(0xFF778899);
    [Obsolete("Use class Colors for predefined colors.")]
    public static readonly Color LightSteelBlue = new Color(0xFFB0C4DE);
    [Obsolete("Use class Colors for predefined colors.")]
    public static readonly Color LightYellow = new Color(0xFFFFFFE0);
    [Obsolete("Use class Colors for predefined colors.")]
    public static readonly Color Lime = new Color(0xFF00FF00);
    [Obsolete("Use class Colors for predefined colors.")]
    public static readonly Color LimeGreen = new Color(0xFF32CD32);
    [Obsolete("Use class Colors for predefined colors.")]
    public static readonly Color Linen = new Color(0xFFFAF0E6);
    [Obsolete("Use class Colors for predefined colors.")]
    public static readonly Color Magenta = new Color(0xFFFF00FF);
    [Obsolete("Use class Colors for predefined colors.")]
    public static readonly Color Maroon = new Color(0xFF800000);
    [Obsolete("Use class Colors for predefined colors.")]
    public static readonly Color MediumAquamarine = new Color(0xFF66CDAA);
    [Obsolete("Use class Colors for predefined colors.")]
    public static readonly Color MediumBlue = new Color(0xFF0000CD);
    [Obsolete("Use class Colors for predefined colors.")]
    public static readonly Color MediumOrchid = new Color(0xFFBA55D3);
    [Obsolete("Use class Colors for predefined colors.")]
    public static readonly Color MediumPurple = new Color(0xFF9370DB);
    [Obsolete("Use class Colors for predefined colors.")]
    public static readonly Color MediumSeaGreen = new Color(0xFF3CB371);
    [Obsolete("Use class Colors for predefined colors.")]
    public static readonly Color MediumSlateBlue = new Color(0xFF7B68EE);
    [Obsolete("Use class Colors for predefined colors.")]
    public static readonly Color MediumSpringGreen = new Color(0xFF00FA9A);
    [Obsolete("Use class Colors for predefined colors.")]
    public static readonly Color MediumTurquoise = new Color(0xFF48D1CC);
    [Obsolete("Use class Colors for predefined colors.")]
    public static readonly Color MediumVioletRed = new Color(0xFFC71585);
    [Obsolete("Use class Colors for predefined colors.")]
    public static readonly Color MidnightBlue = new Color(0xFF191970);
    [Obsolete("Use class Colors for predefined colors.")]
    public static readonly Color MintCream = new Color(0xFFF5FFFA);
    [Obsolete("Use class Colors for predefined colors.")]
    public static readonly Color MistyRose = new Color(0xFFFFE4E1);
    [Obsolete("Use class Colors for predefined colors.")]
    public static readonly Color Moccasin = new Color(0xFFFFE4B5);
    [Obsolete("Use class Colors for predefined colors.")]
    public static readonly Color NavajoWhite = new Color(0xFFFFDEAD);
    [Obsolete("Use class Colors for predefined colors.")]
    public static readonly Color Navy = new Color(0xFF000080);
    [Obsolete("Use class Colors for predefined colors.")]
    public static readonly Color OldLace = new Color(0xFFFDF5E6);
    [Obsolete("Use class Colors for predefined colors.")]
    public static readonly Color Olive = new Color(0xFF808000);
    [Obsolete("Use class Colors for predefined colors.")]
    public static readonly Color OliveDrab = new Color(0xFF6B8E23);
    [Obsolete("Use class Colors for predefined colors.")]
    public static readonly Color Orange = new Color(0xFFFFA500);
    [Obsolete("Use class Colors for predefined colors.")]
    public static readonly Color OrangeRed = new Color(0xFFFF4500);
    [Obsolete("Use class Colors for predefined colors.")]
    public static readonly Color Orchid = new Color(0xFFDA70D6);
    [Obsolete("Use class Colors for predefined colors.")]
    public static readonly Color PaleGoldenrod = new Color(0xFFEEE8AA);
    [Obsolete("Use class Colors for predefined colors.")]
    public static readonly Color PaleGreen = new Color(0xFF98FB98);
    [Obsolete("Use class Colors for predefined colors.")]
    public static readonly Color PaleTurquoise = new Color(0xFFAFEEEE);
    [Obsolete("Use class Colors for predefined colors.")]
    public static readonly Color PaleVioletRed = new Color(0xFFDB7093);
    [Obsolete("Use class Colors for predefined colors.")]
    public static readonly Color PapayaWhip = new Color(0xFFFFEFD5);
    [Obsolete("Use class Colors for predefined colors.")]
    public static readonly Color PeachPuff = new Color(0xFFFFDAB9);
    [Obsolete("Use class Colors for predefined colors.")]
    public static readonly Color Peru = new Color(0xFFCD853F);
    [Obsolete("Use class Colors for predefined colors.")]
    public static readonly Color Pink = new Color(0xFFFFC0CB);
    [Obsolete("Use class Colors for predefined colors.")]
    public static readonly Color Plum = new Color(0xFFDDA0DD);
    [Obsolete("Use class Colors for predefined colors.")]
    public static readonly Color PowderBlue = new Color(0xFFB0E0E6);
    [Obsolete("Use class Colors for predefined colors.")]
    public static readonly Color Purple = new Color(0xFF800080);
    [Obsolete("Use class Colors for predefined colors.")]
    public static readonly Color Red = new Color(0xFFFF0000);
    [Obsolete("Use class Colors for predefined colors.")]
    public static readonly Color RosyBrown = new Color(0xFFBC8F8F);
    [Obsolete("Use class Colors for predefined colors.")]
    public static readonly Color RoyalBlue = new Color(0xFF4169E1);
    [Obsolete("Use class Colors for predefined colors.")]
    public static readonly Color SaddleBrown = new Color(0xFF8B4513);
    [Obsolete("Use class Colors for predefined colors.")]
    public static readonly Color Salmon = new Color(0xFFFA8072);
    [Obsolete("Use class Colors for predefined colors.")]
    public static readonly Color SandyBrown = new Color(0xFFF4A460);
    [Obsolete("Use class Colors for predefined colors.")]
    public static readonly Color SeaGreen = new Color(0xFF2E8B57);
    [Obsolete("Use class Colors for predefined colors.")]
    public static readonly Color SeaShell = new Color(0xFFFFF5EE);
    [Obsolete("Use class Colors for predefined colors.")]
    public static readonly Color Sienna = new Color(0xFFA0522D);
    [Obsolete("Use class Colors for predefined colors.")]
    public static readonly Color Silver = new Color(0xFFC0C0C0);
    [Obsolete("Use class Colors for predefined colors.")]
    public static readonly Color SkyBlue = new Color(0xFF87CEEB);
    [Obsolete("Use class Colors for predefined colors.")]
    public static readonly Color SlateBlue = new Color(0xFF6A5ACD);
    [Obsolete("Use class Colors for predefined colors.")]
    public static readonly Color SlateGray = new Color(0xFF708090);
    [Obsolete("Use class Colors for predefined colors.")]
    public static readonly Color Snow = new Color(0xFFFFFAFA);
    [Obsolete("Use class Colors for predefined colors.")]
    public static readonly Color SpringGreen = new Color(0xFF00FF7F);
    [Obsolete("Use class Colors for predefined colors.")]
    public static readonly Color SteelBlue = new Color(0xFF4682B4);
    [Obsolete("Use class Colors for predefined colors.")]
    public static readonly Color Tan = new Color(0xFFD2B48C);
    [Obsolete("Use class Colors for predefined colors.")]
    public static readonly Color Teal = new Color(0xFF008080);
    [Obsolete("Use class Colors for predefined colors.")]
    public static readonly Color Thistle = new Color(0xFFD8BFD8);
    [Obsolete("Use class Colors for predefined colors.")]
    public static readonly Color Tomato = new Color(0xFFFF6347);
    [Obsolete("Use class Colors for predefined colors.")]
    public static readonly Color Transparent = new Color(0x00FFFFFF);
    [Obsolete("Use class Colors for predefined colors.")]
    public static readonly Color Turquoise = new Color(0xFF40E0D0);
    [Obsolete("Use class Colors for predefined colors.")]
    public static readonly Color Violet = new Color(0xFFEE82EE);
    [Obsolete("Use class Colors for predefined colors.")]
    public static readonly Color Wheat = new Color(0xFFF5DEB3);
    [Obsolete("Use class Colors for predefined colors.")]
    public static readonly Color White = new Color(0xFFFFFFFF);
    [Obsolete("Use class Colors for predefined colors.")]
    public static readonly Color WhiteSmoke = new Color(0xFFF5F5F5);
    [Obsolete("Use class Colors for predefined colors.")]
    public static readonly Color Yellow = new Color(0xFFFFFF00);
    [Obsolete("Use class Colors for predefined colors.")]
    public static readonly Color YellowGreen = new Color(0xFF9ACD32);
  }
}
