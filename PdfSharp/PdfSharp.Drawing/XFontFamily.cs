#region PDFsharp - A .NET library for processing PDF
//
// Authors:
//   Stefan Lange (mailto:Stefan.Lange@pdfsharp.com)
//
// Copyright (c) 2005-2007 empira Software GmbH, Cologne (Germany)
//
// http://www.pdfsharp.com
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
using System.IO;
#if Gdip
using System.Drawing;
using System.Drawing.Drawing2D;
#endif
#if Wpf
using System.Windows.Media;
#endif
using PdfSharp.Pdf;

namespace PdfSharp.Drawing
{
  /// <summary>
  /// Defines a group of type faces having a similar basic design and certain variations in styles.
  /// </summary>
  public sealed class XFontFamily
  {
    internal XFontFamily() { }

    internal XFontFamily(FontFamily family)
    {
      this.family = family;
    }

    //internal FontFamily();
    //public FontFamily(GenericFontFamilies genericFamily);
    //internal FontFamily(IntPtr family);

    /// <summary>
    /// Initializes a new instance of the <see cref="XFontFamily"/> class.
    /// </summary>
    /// <param name="name">The family name of a font.</param>
    public XFontFamily(string name)
    {
      this.family = new FontFamily(name);
    }

    //public FontFamily(string name, FontCollection fontCollection);

    //public override bool Equals(object obj);

    /// <summary>
    /// Returns the cell ascent, in design units, of the XFontFamily object of the specified style.
    /// </summary>
    public int GetCellAscent(XFontStyle style)
    {
      return this.family.GetCellAscent((FontStyle)style);
    }

    /// <summary>
    /// Returns the cell descent, in design units, of the XFontFamily object of the specified style.
    /// </summary>
    public int GetCellDescent(XFontStyle style)
    {
      return this.family.GetCellDescent((FontStyle)style);
    }

    /// <summary>
    /// Gets the height, in font design units, of the em square for the specified style.
    /// </summary>
    public int GetEmHeight(XFontStyle style)
    {
      return this.family.GetEmHeight((FontStyle)style);
    }

    //public override int GetHashCode();

    /// <summary>
    /// Returns the line spacing, in design units, of the FontFamily object of the specified style.
    /// The line spacing is the vertical distance between the base lines of two consecutive lines of text.
    /// </summary>
    public int GetLineSpacing(XFontStyle style)
    {
      return this.family.GetLineSpacing((FontStyle)style);
    }

    //public string GetName(int language);

    /// <summary>
    /// Indicates whether the specified FontStyle enumeration is available.
    /// </summary>
    public bool IsStyleAvailable(XFontStyle style)
    {
      return this.family.IsStyleAvailable((FontStyle)style);
    }

    //internal void SetNative(IntPtr native);
    //public override string ToString();
    //
    //// Properties
    //private static int CurrentLanguage { get; }

    /// <summary>
    /// Returns an array that contains all the FontFamily objects associated with the current graphics context.
    /// </summary>
    public static XFontFamily[] Families
    {
      get
      {
        FontFamily[] families = FontFamily.Families;
        int count = families.Length;
        XFontFamily[] result = new XFontFamily[count];
        for (int idx = 0; idx < count; idx++)
          result[idx] = new XFontFamily(families[idx]);
        return result;
      }
    }

    /// <summary>
    /// Returns an array that contains all the FontFamily objects available for the specified 
    /// graphics context.
    /// </summary>
    public static XFontFamily[] GetFamilies(XGraphics graphics)
    {
      FontFamily[] families = FontFamily.GetFamilies(graphics.gfx);
      int count = families.Length;
      XFontFamily[] result = new XFontFamily[count];
      for (int idx = 0; idx < count; idx++)
        result[idx] = new XFontFamily(families[idx]);
      return result;
    }

    //public static FontFamily GenericMonospace { get; }
    //public static FontFamily GenericSansSerif { get; }
    //public static FontFamily GenericSerif { get; }
    //public string Name { get; }

    /// <summary>
    /// GDI+ object.
    /// </summary>
    internal FontFamily family;
  }
}
