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
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.ComponentModel;
using System.IO;
#if Gdip
using System.Drawing;
using System.Drawing.Drawing2D;
#endif
#if Wpf
using System.Windows.Media;
#endif
using PdfSharp.Internal;
using PdfSharp.Fonts.TrueType;
using PdfSharp.Pdf;
using PdfSharp.Pdf.Advanced;

namespace PdfSharp.Drawing
{
  /// <summary>
  /// Defines an object used to draw glyphs.
  /// </summary>
  [DebuggerDisplay("({Name}, {Size})")]
  public class XFont
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="XFont"/> class.
    /// </summary>
    /// <param name="familyName">Name of the font family.</param>
    /// <param name="emSize">The em size.</param>
    public XFont(string familyName, double emSize)
    {
      this.familyName = familyName;
      this.size = emSize;
      this.style = XFontStyle.Regular;
      this.pdfOptions = new XPdfFontOptions();
      Initialize();
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="XFont"/> class.
    /// </summary>
    /// <param name="familyName">Name of the font family.</param>
    /// <param name="emSize">The em size.</param>
    /// <param name="style">The font style.</param>
    public XFont(string familyName, double emSize, XFontStyle style)
    {
      this.familyName = familyName;
      this.size = emSize;
      this.style = style;
      this.pdfOptions = new XPdfFontOptions();
      Initialize();
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="XFont"/> class.
    /// </summary>
    /// <param name="familyName">Name of the font family.</param>
    /// <param name="emSize">The em size.</param>
    /// <param name="style">The font style.</param>
    /// <param name="pdfOptions">Additional PDF options.</param>
    public XFont(string familyName, double emSize, XFontStyle style, XPdfFontOptions pdfOptions)
    {
      this.familyName = familyName;
      this.size = emSize;
      this.style = style;
      this.pdfOptions = pdfOptions;
      Initialize();
    }

#if Gdip
#if GdipUseGdiObjects
    /// <summary>
    /// Initializes a new instance of the <see cref="XFont"/> class from a System.Drawing.Font.
    /// </summary>
    /// <param name="font">A System.Drawing.Font.</param>
    /// <param name="pdfOptions">Additional PDF options.</param>
    public XFont(Font font, XPdfFontOptions pdfOptions)
    {
      if (font.Unit != GraphicsUnit.World)
        throw new ArgumentException("Font must use GraphicsUnit.World.");
      this.font = font;
      this.familyName = font.Name;
      this.size = font.Size;
      this.style = FontStyleFrom(font);
      this.pdfOptions = pdfOptions;
      Initialize();
    }
#endif
#endif

    void Initialize()
    {
      if (this.font == null)
        this.font = new Font(this.familyName, (float)this.size, (FontStyle)this.style, GraphicsUnit.World);

      FontFamily fontFamily = this.font.FontFamily;
      this.cellSpace = fontFamily.GetLineSpacing(font.Style);
      this.cellAscent = fontFamily.GetCellAscent(font.Style);
      this.cellDescent = fontFamily.GetCellDescent(font.Style);
    }
    string familyName;

    internal static XFontStyle FontStyleFrom(Font font)
    {
      return
        (font.Bold ? XFontStyle.Bold : 0) |
        (font.Italic ? XFontStyle.Italic : 0) |
        (font.Strikeout ? XFontStyle.Strikeout : 0) |
        (font.Underline ? XFontStyle.Underline : 0);
    }

    //// Methods
    //public Font(Font prototype, FontStyle newStyle);
    //public Font(FontFamily family, float emSize);
    //public Font(string familyName, float emSize);
    //public Font(FontFamily family, float emSize, FontStyle style);
    //public Font(FontFamily family, float emSize, GraphicsUnit unit);
    //public Font(string familyName, float emSize, FontStyle style);
    //public Font(string familyName, float emSize, GraphicsUnit unit);
    //public Font(FontFamily family, float emSize, FontStyle style, GraphicsUnit unit);
    //public Font(string familyName, float emSize, FontStyle style, GraphicsUnit unit);
    ////public Font(FontFamily family, float emSize, FontStyle style, GraphicsUnit unit, byte gdiCharSet);
    ////public Font(string familyName, float emSize, FontStyle style, GraphicsUnit unit, byte gdiCharSet);
    ////public Font(FontFamily family, float emSize, FontStyle style, GraphicsUnit unit, byte gdiCharSet, bool gdiVerticalFont);
    ////public Font(string familyName, float emSize, FontStyle style, GraphicsUnit unit, byte gdiCharSet, bool gdiVerticalFont);


    //public object Clone();
    //private static FontFamily CreateFontFamilyWithFallback(string familyName);

    //private void Dispose(bool disposing);
    //public override bool Equals(object obj);
    //protected override void Finalize();
    //public static Font FromHdc(IntPtr hdc);
    //public static Font FromHfont(IntPtr hfont);
    //public static Font FromLogFont(object lf);
    //public static Font FromLogFont(object lf, IntPtr hdc);
    //public override int GetHashCode();

    /// <summary>
    /// Returns the line spacing, in pixels, of this font. The line spacing is the vertical distance
    /// between the base lines of two consecutive lines of text. Thus, the line spacing includes the
    /// blank space between lines along with the height of the character itself.
    /// </summary>
    public double GetHeight()
    {
      RealizeGdiFont();
      return this.font.GetHeight();
    }

    /// <summary>
    /// Returns the line spacing, in the current unit of a specified Graphics object, of this font.
    /// The line spacing is the vertical distance between the base lines of two consecutive lines of
    /// text. Thus, the line spacing includes the blank space between lines along with the height of
    /// </summary>
    public double GetHeight(XGraphics graphics)
    {
      RealizeGdiFont();
      return this.font.GetHeight(graphics.gfx);
    }

    //public float GetHeight(float dpi);
    //public IntPtr ToHfont();
    //public void ToLogFont(object logFont);
    //public void ToLogFont(object logFont, Graphics graphics);
    //public override string ToString();

    // Properties

    /// <summary>
    /// Gets the XFontFamily object associated with this XFont object.
    /// </summary>
    [Browsable(false)]
    public XFontFamily FontFamily
    {
      get
      {
        if (this.fontFamily == null)
        {
          RealizeGdiFont();
          this.fontFamily = new XFontFamily(this.font.FontFamily);
        }
        return this.fontFamily;
      }
    }
    XFontFamily fontFamily;

    /// <summary>
    /// Gets the face name of this Font object.
    /// </summary>
    public string Name
    {
      get
      {
        RealizeGdiFont();
        return this.font.Name;
      }
    }

    /// <summary>
    /// Gets the em-size of this Font object measured in the unit of this Font object.
    /// </summary>
    public double Size
    {
      get { return this.size; }
    }
    double size;


    /// <summary>
    /// Gets the line spacing of this font.
    /// </summary>
    [Browsable(false)]
    public int Height
    {
      get
      {
        RealizeGdiFont();
        return this.font.Height;
      }
    }

    /// <summary>
    /// Gets style information for this Font object.
    /// </summary>
    [Browsable(false)]
    public XFontStyle Style
    {
      get { return this.style; }
    }
    XFontStyle style;

    /// <summary>
    /// Indicates whether this XFont object is bold.
    /// </summary>
    public bool Bold
    {
      get { return ((this.style & XFontStyle.Bold) == XFontStyle.Bold); }
    }

    /// <summary>
    /// Indicates whether this XFont object is italic.
    /// </summary>
    public bool Italic
    {
      get { return ((this.style & XFontStyle.Italic) == XFontStyle.Italic); }
    }

    /// <summary>
    /// Indicates whether this XFont object is stroke out.
    /// </summary>
    public bool Strikeout
    {
      get { return ((this.style & XFontStyle.Strikeout) == XFontStyle.Strikeout); }
    }

    /// <summary>
    /// Indicates whether this XFont object is underlined.
    /// </summary>
    public bool Underline
    {
      get { return ((this.style & XFontStyle.Underline) == XFontStyle.Underline); }
    }

    /// <summary>
    /// Gets the PDF options of the font.
    /// </summary>
    public XPdfFontOptions PdfOptions
    {
      get
      {
        if (this.pdfOptions == null)
          this.pdfOptions = new XPdfFontOptions();
        return this.pdfOptions;
      }
    }
    XPdfFontOptions pdfOptions;

    /// <summary>
    /// Indicates whether this XFont is encoded as Unicode.
    /// </summary>
    internal bool Unicode
    {
      get { return this.pdfOptions != null ? this.pdfOptions.FontEncoding == PdfFontEncoding.Unicode : false; }
    }

    /// <summary>
    /// Gets the metrics.
    /// </summary>
    /// <value>The metrics.</value>
    public XFontMetrics Metrics
    {
      get
      {
        if (this.fontMetrics == null)
        {
          FontDescriptor descriptor = FontDescriptorStock.Global.CreateDescriptor(this);
          this.fontMetrics = descriptor.FontMetrics;
        }
        return this.fontMetrics;
      }
    }
    XFontMetrics fontMetrics;

#if Gdip
#if GdipUseGdiObjects
    /// <summary>
    /// Implicit conversion form Font to XFont
    /// </summary>
    public static implicit operator XFont(Font font)
    {
      //XFont xfont = new XFont(font.Name, font.Size, FontStyleFrom(font));
      XFont xfont = new XFont(font, null);
      return xfont;
    }
#endif

    internal Font RealizeGdiFont()
    {
      //if (this.font == null)
      //  this.font = new Font(this.familyName, this.size, (FontStyle)this.style);
      return this.font;
    }
    Font font;
#endif

#if WinFX_
    internal Font RealizeGdiFont()
    {
      //if (this.font == null)
      //  this.font = new Font(this.familyName, this.size, (FontStyle)this.style);
      return this.font;
    }
    Font font;
#endif

    internal int cellSpace;
    internal int cellAscent;
    internal int cellDescent;

    /// <summary>
    /// Cache PdfFontTable.FontSelector to speed up finding the right PdfFont
    /// if this font is used more than once.
    /// </summary>
    internal PdfFontTable.FontSelector selector;
  }
}
