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
#if Gdip
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
#endif
#if Wpf
using System.Windows.Media;
#endif
using PdfSharp.Pdf.Internal;
using PdfSharp.Drawing;

namespace PdfSharp.Fonts.TrueType
{
  /// <summary>
  /// Base class for all font descriptors.
  /// </summary>
  internal class FontDescriptor
  {
    //    protected:
    //    static const int  sm_FixedPitch;
    //    static const int  sm_Serif;
    //    static const int  sm_Symbolic;
    //    static const int  sm_Script;
    //    static const int  sm_Nonsymbolic;
    //    static const int  sm_Italic;
    //    static const int  sm_AllCap;
    //    static const int  sm_SmallCap;
    //    static const int  sm_ForceBold;
    //
    //    public:
    //    static const int  NoKerning;
    //    static const int  LightKerning;
    //    static const int  MediumKerning;
    //    static const int  RightKerning;

    protected FontDescriptor()
    {
    }

    /// <summary>
    /// 
    /// </summary>
    public string FontFile
    {
      get { return this.fontFile; }
    }
    protected string fontFile;

    /// <summary>
    /// 
    /// </summary>
    public string FontType
    {
      get { return this.fontType; }
    }
    protected string fontType;

    /// <summary>
    /// 
    /// </summary>
    public string FontName
    {
      get { return this.fontName; }
    }
    protected string fontName;

    /// <summary>
    /// 
    /// </summary>
    public string FullName
    {
      get { return this.fullName; }
    }
    protected string fullName;

    /// <summary>
    /// 
    /// </summary>
    public string FamilyName
    {
      get { return this.familyName; }
    }
    protected string familyName;

    /// <summary>
    /// 
    /// </summary>
    public string Weight
    {
      get { return this.weight; }
    }
    protected string weight;

    /// <summary>
    /// Gets a value indicating whether this instance belongs to a bold font.
    /// </summary>
    public virtual bool IsBoldFace
    {
      get { return false; }
    }

    /// <summary>
    /// 
    /// </summary>
    public float ItalicAngle
    {
      get { return this.italicAngle; }
    }
    protected float italicAngle;

    /// <summary>
    /// Gets a value indicating whether this instance belongs to an italic font.
    /// </summary>
    public virtual bool IsItalicFace
    {
      get { return false; }
    }

    /// <summary>
    /// 
    /// </summary>
    public int XMin
    {
      get { return this.xMin; }
    }
    protected int xMin;

    /// <summary>
    /// 
    /// </summary>
    public int YMin
    {
      get { return this.yMin; }
    }
    protected int yMin;

    /// <summary>
    /// 
    /// </summary>
    public int XMax
    {
      get { return this.xMax; }
    }
    protected int xMax;

    /// <summary>
    /// 
    /// </summary>
    public int YMax
    {
      get { return this.yMax; }
    }
    protected int yMax;

    /// <summary>
    /// 
    /// </summary>
    public bool IsFixedPitch
    {
      get { return this.isFixedPitch; }
    }
    protected bool isFixedPitch;

    //Rect FontBBox;

    /// <summary>
    /// 
    /// </summary>
    public int UnderlinePosition
    {
      get { return this.underlinePosition; }
    }
    protected int underlinePosition;

    /// <summary>
    /// 
    /// </summary>
    public int UnderlineThickness
    {
      get { return this.underlineThickness; }
    }
    protected int underlineThickness;

    /// <summary>
    /// 
    /// </summary>
    public int StrikeoutPosition
    {
      get { return this.strikeoutPosition; }
    }
    protected int strikeoutPosition;

    /// <summary>
    /// 
    /// </summary>
    public int StrikeoutSize
    {
      get { return this.strikeoutSize; }
    }
    protected int strikeoutSize;

    /// <summary>
    /// 
    /// </summary>
    public string Version
    {
      get { return this.version; }
    }
    protected string version;

    /// <summary>
    /// 
    /// </summary>
    public string Notice
    {
      get { return this.Notice; }
    }
    protected string notice;

    /// <summary>
    /// 
    /// </summary>
    public string EncodingScheme
    {
      get { return this.encodingScheme; }
    }
    protected string encodingScheme;

    /// <summary>
    /// 
    /// </summary>
    public int CapHeight
    {
      get { return this.capHeight; }
    }
    protected int capHeight;

    /// <summary>
    /// 
    /// </summary>
    public int XHeight
    {
      get { return this.xHeight; }
    }
    protected int xHeight;

    /// <summary>
    /// 
    /// </summary>
    public int Ascender
    {
      get { return this.ascender; }
    }
    protected int ascender;

    /// <summary>
    /// 
    /// </summary>
    public int Descender
    {
      get { return this.descender; }
    }
    protected int descender;

    /// <summary>
    /// 
    /// </summary>
    public int Flags
    {
      get { return this.flags; }
    }
    protected int flags;

    /// <summary>
    /// 
    /// </summary>
    public int StemV
    {
      get { return this.stemV; }
    }
    protected int stemV;

    /// <summary>
    /// Under Construction
    /// </summary>
    public XFontMetrics FontMetrics
    {
      get
      {
        if (this.fontMetrics == null)
        {
          this.fontMetrics = new XFontMetrics(this.fontName, this.ascender, this.descender, 0, this.capHeight,
            this.xHeight, this.stemV, 0, 0, 0);
        }
        return this.fontMetrics;
      }
    }
    XFontMetrics fontMetrics;
  }

  /// <summary>
  /// The TrueType font desriptor.
  /// </summary>
  internal sealed class TrueTypeDescriptor : FontDescriptor
  {
#if Gdip
    public TrueTypeDescriptor(System.Drawing.Font font, XPdfFontOptions options)
    {
      try
      {
        this.fontImage = new FontImage(font, options);
        this.fontName = font.Name;
        Initialize();
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }
#endif

    //internal TrueTypeDescriptor(FontSelector selector)
    //{
    //  throw new NotImplementedException("TrueTypeDescriptor(FontSelector selector)");
    //}

    internal TrueTypeDescriptor(XFont font)
      : this(font.RealizeGdiFont(), font.PdfOptions)
    { }

    internal FontImage fontImage;

    void Initialize()
    {
      bool embeddingRestricted = this.fontImage.os2.fsType == 0x0002;

      //this.fontName = image.n
      this.italicAngle = this.fontImage.post.italicAngle;

      this.xMin = this.fontImage.head.xMin;
      this.yMin = this.fontImage.head.yMin;
      this.xMax = this.fontImage.head.xMax;
      this.yMax = this.fontImage.head.yMax;

      this.underlinePosition = this.fontImage.post.underlinePosition;
      this.underlineThickness = this.fontImage.post.underlineThickness;
      this.strikeoutPosition = this.fontImage.os2.yStrikeoutPosition;
      this.strikeoutSize = this.fontImage.os2.yStrikeoutSize;

      // No documetation found how to get the set vertical stems width from the
      // TrueType tables.
      // The following formula comes from PDFlib Lite source code. Acrobat 5.0 sets
      // /StemV to 0 always. I think the value doesn't matter.
      //float weight = (float)(this.image.os2.usWeightClass / 65.0f);
      //this.stemV = (int)(50 + weight * weight);  // MAGIC
      this.stemV = 0;

      // PDFlib states that some Apple fonts miss the OS/2 table.
      Debug.Assert(fontImage.os2 != null, "TrueType font has no OS/2 table.");

      // PDFlib takes ascender and descender from OS/2 tabe, but GDI+ uses other values!
      //if (image.os2.sTypoAscender != 0)
      //  this.ascender = image.os2.sTypoAscender;
      //else
      this.ascender = fontImage.hhea.ascender;

      //if (image.os2.sTypoDescender != 0)
      //  this.descender = image.os2.sTypoDescender;
      //else
      this.descender = fontImage.hhea.descender;

      // sCapHeight and sxHeight are only valid if version >= 2
      if (fontImage.os2.version >= 2 && fontImage.os2.sCapHeight != 0)
        this.capHeight = fontImage.os2.sCapHeight;
      else
        this.capHeight = fontImage.hhea.ascender;

      if (fontImage.os2.version >= 2 && fontImage.os2.sxHeight != 0)
        this.xHeight = fontImage.os2.sxHeight;
      else
        this.xHeight = (int)(0.66f * this.ascender);

      //this.flags = this.image.

      Encoding ansi = PdfEncoders.WinAnsiEncoding; // System.Text.Encoding.Default;
      Encoding unicode = System.Text.Encoding.Unicode;
      byte[] bytes = new byte[256];

      bool symbol = this.fontImage.cmap.symbol;
      this.widths = new int[256];
      for (int idx = 0; idx < 256; idx++)
      {
        bytes[idx] = (byte)idx;
        // PDFlib handles some font flaws here...
        // We wait for bug reports.

        char ch = (char)idx;
        string s = ansi.GetString(bytes, idx, 1);
        if (s.Length != 0)
        {
          if (s[0] != ch)
            ch = s[0];
        }
#if DEBUG
        if (idx == (int)'S')
          GetType();
#endif
        int glyphIndex;
        if (symbol)
        {
          glyphIndex = idx + (this.fontImage.os2.usFirstCharIndex & 0xFF00);
          glyphIndex = CharCodeToGlyphIndex((char)glyphIndex);
        }
        else
        {
          Debug.Assert(idx + (this.fontImage.os2.usFirstCharIndex & 0xFF00) == idx);
          //glyphIndex = CharCodeToGlyphIndex((char)idx);
          glyphIndex = CharCodeToGlyphIndex(ch);
        }
        this.widths[idx] = GlyphIndexToPdfWidth(glyphIndex);
      }
    }
    public int[] widths;

    public override bool IsBoldFace
    {
      get
      {
        return base.IsBoldFace;
      }
    }

    public override bool IsItalicFace
    {
      get
      {
        return base.IsItalicFace;
      }
    }

    internal int DesignUnitsToPdf(double value)
    {
      return (int)Math.Round(value * 1000.0 / this.fontImage.head.unitsPerEm);
    }

    /// <summary>
    /// Maps a unicode to the index of the corresponding glyph.
    /// See OpenType spec "cmap - Character To Glyph Index Mapping Table / Format 4: Segment mapping to delta values"
    /// for details about this a little bit strange looking algorythm.
    /// </summary>
    public int CharCodeToGlyphIndex(char value)
    {
      try
      {
        CMap4 cmap = this.fontImage.cmap.cmap4;
        int segCount = cmap.segCountX2 / 2;
        int seg;
        for (seg = 0; seg < segCount; seg++)
        {
          if (value <= cmap.endCount[seg])
            break;
        }
        Debug.Assert(seg < segCount);

        if (value < cmap.startCount[seg])
          return 0;

        if (cmap.idRangeOffs[seg] == 0)
          return (value + cmap.idDelta[seg]) & 0xFFFF;

        int idx = cmap.idRangeOffs[seg] / 2 + (value - cmap.startCount[seg]) - (segCount - seg);
        Debug.Assert(idx >= 0 && idx < cmap.glyphCount);

        if (cmap.glyphIdArray[idx] == 0)
          return 0;
        else
          return (cmap.glyphIdArray[idx] + cmap.idDelta[seg]) & 0xFFFF;
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    /// <summary>
    /// Converts the width of a glyph identified by its index to PDF design units.
    /// </summary>
    public int GlyphIndexToPdfWidth(int glyphIndex)
    {
      try
      {
        int numberOfHMetrics = this.fontImage.hhea.numberOfHMetrics;
        int unitsPerEm = this.fontImage.head.unitsPerEm;

        // glyphIndex >= numberOfHMetrics means the font is mono-spaced and all glyphs have the same width
        if (glyphIndex >= numberOfHMetrics)
          glyphIndex = numberOfHMetrics - 1;

        int width = this.fontImage.hmtx.metrics[glyphIndex].advanceWidth;

        // Sometimes the unitsPerEm is 1000, sometimes a power of 2.
        if (unitsPerEm == 1000)
          return width;
        return width * 1000 / unitsPerEm;
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public int PdfWidthFromCharCode(char ch)
    {
      int idx = CharCodeToGlyphIndex(ch);
      int width = GlyphIndexToPdfWidth(idx);
      return width;
    }

#if DEBUTG
    public static void Test()
    {
      Font font = new Font("Times", 10);
      FontImage image = new FontImage(font);

//      Font font = new Font("Isabelle", 12);
//      LOGFONT logFont = new LOGFONT();
//      font.ToLogFont(logFont);
//
//      IntPtr hfont = CreateFontIndirect(logFont);
////      IntPtr hfont2 = font.ToHfont();
////      System.Windows.Forms.MessageBox.Show(hfont2.ToString());
//
//      Graphics gfx = Graphics.FromHwnd(IntPtr.Zero);
//      IntPtr hdc = gfx.GetHdc();
//      IntPtr oldFont =  SelectObject(hdc, hfont);
//      int size = GetFontData(hdc, 0, 0, null, 0);
//
//      byte[] fontbits = new byte[size];
//      int xx = GetFontData(hdc, 0, 0, fontbits, size);
//      SelectObject(hdc, oldFont);
//      DeleteObject(hfont);
//      gfx.ReleaseHdc(hdc);
//
//      FontImage image = new FontImage(fontbits);
//      //image.Read();
//
//
//      //HandleRef
//
//      font.GetType();
    }
#endif
  }
}
