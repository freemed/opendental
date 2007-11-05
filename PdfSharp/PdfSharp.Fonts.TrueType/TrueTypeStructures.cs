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

#define VERBOSE_

using System;
using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.IO;
using PdfSharp.Drawing;
using PdfSharp.Internal;

using Fixed = System.Int32;
using FWord = System.Int16;
using UFWord = System.UInt16;

namespace PdfSharp.Fonts.TrueType
{
  internal enum PlatformId
  {
    Apple, Mac, Iso, Win
  }

  /// <summary>
  /// Only Symbol and Unicode is used by PDFsharp.
  /// </summary>
  internal enum WinEncodingId
  {
    Symbol, Unicode
  }

  /// <summary>
  /// CMap format 4: Segment mapping to delta values.
  /// The Windows standard format.
  /// </summary>
  internal class CMap4 : TrueTypeFontTable
  {
    public WinEncodingId encodingId; // Windows encoding ID.
    public ushort format; // Format number is set to 4.
    public ushort length; // This is the length in bytes of the subtable. 
    public ushort language; // This field must be set to zero for all cmap subtables whose platform IDs are other than Macintosh (platform ID 1). 
    public ushort segCountX2; // 2 x segCount.
    public ushort searchRange; // 2 x (2**floor(log2(segCount)))
    public ushort entrySelector; // log2(searchRange/2)
    public ushort rangeShift;
    public ushort[] endCount; // [segCount] / End characterCode for each segment, last=0xFFFF.
    public ushort[] startCount; // [segCount] / Start character code for each segment.
    public short[] idDelta; // [segCount] / Delta for all character codes in segment.
    public ushort[] idRangeOffs; // [segCount] / Offsets into glyphIdArray or 0
    public int glyphCount; // = (length - (16 + 4 * 2 * segCount)) / 2;
    public ushort[] glyphIdArray;     // Glyph index array (arbitrary length)

    public CMap4(FontImage fontImage, WinEncodingId encodingId)
      : base(fontImage, "----")
    {
      this.encodingId = encodingId;
      Read();
    }

    internal void Read()
    {
      try
      {
        // m_EncodingID = encID;
        this.format = this.fontImage.ReadUShort();
        Debug.Assert(this.format == 4, "Only format 4 expected.");
        this.length = this.fontImage.ReadUShort();
        this.language = this.fontImage.ReadUShort();  // Always null in Windows
        this.segCountX2 = this.fontImage.ReadUShort();
        this.searchRange = this.fontImage.ReadUShort();
        this.entrySelector = this.fontImage.ReadUShort();
        this.rangeShift = this.fontImage.ReadUShort();

        int segCount = this.segCountX2 / 2;
        this.glyphCount = (this.length - (16 + 8 * segCount)) / 2;

        //ASSERT_CONDITION(0 <= m_NumGlyphIds && m_NumGlyphIds < m_Length, "Invalid Index");

        this.endCount = new ushort[segCount];
        this.startCount = new ushort[segCount];
        this.idDelta = new short[segCount];
        this.idRangeOffs = new ushort[segCount];

        this.glyphIdArray = new ushort[this.glyphCount];

        for (int idx = 0; idx < segCount; idx++)
          this.endCount[idx] = this.fontImage.ReadUShort();

        //ASSERT_CONDITION(m_EndCount[segs - 1] == 0xFFFF, "Out of Index");

        // Read reserved pad.
        this.fontImage.ReadUShort();

        for (int idx = 0; idx < segCount; idx++)
          this.startCount[idx] = this.fontImage.ReadUShort();

        for (int idx = 0; idx < segCount; idx++)
          this.idDelta[idx] = this.fontImage.ReadShort();

        for (int idx = 0; idx < segCount; idx++)
          this.idRangeOffs[idx] = this.fontImage.ReadUShort();

        for (int idx = 0; idx < this.glyphCount; idx++)
          this.glyphIdArray[idx] = this.fontImage.ReadUShort();
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }
  }

  /// <summary>
  /// This table defines the mapping of character codes to the glyph index values used in the font.
  /// It may contain more than one subtable, in order to support more than one character encoding scheme.
  /// </summary>
  internal class CMapTable : TrueTypeFontTable
  {
    public const string Tag = TableTagNames.CMap;

    public ushort version;
    public ushort numTables;

    /// <summary>
    /// Is true for symbol font encoding.
    /// </summary>
    public bool symbol;

    public CMap4 cmap4;

    /// <summary>
    /// Initializes a new instance of the <see cref="CMapTable"/> class.
    /// </summary>
    public CMapTable(FontImage fontImage)
      : base(fontImage, Tag)
    {
      Read();
    }

    internal void Read()
    {
      try
      {
        int tableOffset = this.fontImage.Position;

        this.version = this.fontImage.ReadUShort();
        this.numTables = this.fontImage.ReadUShort();

        bool success = false;
        for (int idx = 0; idx < this.numTables; idx++)
        {
          PlatformId platformId = (PlatformId)this.fontImage.ReadUShort();
          WinEncodingId encodingId = (WinEncodingId)this.fontImage.ReadUShort();
          int offset = this.fontImage.ReadLong();

          int currentPosition = this.fontImage.Position;

          // Just read Windows stuff
          if (platformId == PlatformId.Win && (encodingId == WinEncodingId.Symbol || encodingId == WinEncodingId.Unicode))
          {
            this.symbol = encodingId == WinEncodingId.Symbol;

            this.fontImage.Position = tableOffset + offset;
            this.cmap4 = new CMap4(this.fontImage, encodingId);
            this.fontImage.Position = currentPosition;
            // We have found what we are looking for, so break.
            success = true;
            break;
          }
        }
        if (!success)
          throw new InvalidOperationException("Font has no usable platform or encoding ID. It cannot be used with PDFsharp.");
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }
  }

  /// <summary>
  /// This table gives global information about the font. The bounding box values should be computed using 
  /// only glyphs that have contours. Glyphs with no contours should be ignored for the purposes of these calculations.
  /// </summary>
  internal class FontHeaderTable : TrueTypeFontTable
  {
    public const string Tag = TableTagNames.Head;

    public Fixed version; // 0x00010000 for version 1.0.
    public Fixed fontRevision;
    public uint checkSumAdjustment;
    public uint magicNumber; // Set to 0x5F0F3CF5
    public ushort flags;
    public ushort unitsPerEm; // Valid range is from 16 to 16384. This value should be a power of 2 for fonts that have TrueType outlines.
    public long created;
    public long modified;
    public short xMin, yMin; // For all glyph bounding boxes.
    public short xMax, yMax; // For all glyph bounding boxes.
    public ushort macStyle;
    public ushort lowestRecPPEM;
    public short fontDirectionHint;
    public short indexToLocFormat; // 0 for short offsets, 1 for long
    public short glyphDataFormat; // 0 for current format

    public FontHeaderTable(FontImage fontImage)
      : base(fontImage, Tag)
    {
      Read();
    }

    public void Read()
    {
      try
      {
        this.version = this.fontImage.ReadFixed();
        this.fontRevision = this.fontImage.ReadFixed();
        this.checkSumAdjustment = this.fontImage.ReadULong();
        this.magicNumber = this.fontImage.ReadULong();
        this.flags = this.fontImage.ReadUShort();
        this.unitsPerEm = this.fontImage.ReadUShort();
        this.created = this.fontImage.ReadLongDate();
        this.modified = this.fontImage.ReadLongDate();
        this.xMin = this.fontImage.ReadShort();
        this.yMin = this.fontImage.ReadShort();
        this.xMax = this.fontImage.ReadShort();
        this.yMax = this.fontImage.ReadShort();
        this.macStyle = this.fontImage.ReadUShort();
        this.lowestRecPPEM = this.fontImage.ReadUShort();
        this.fontDirectionHint = this.fontImage.ReadShort();
        this.indexToLocFormat = this.fontImage.ReadShort();
        this.glyphDataFormat = this.fontImage.ReadShort();
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }
  }
  
  /// <summary>
  /// This table contains information for horizontal layout. The values in the minRightSidebearing, 
  /// minLeftSideBearing and xMaxExtent should be computed using only glyphs that have contours.
  /// Glyphs with no contours should be ignored for the purposes of these calculations.
  /// All reserved areas must be set to 0. 
  /// </summary>
  internal class HorizontalHeaderTable : TrueTypeFontTable
  {
    public const string Tag = TableTagNames.HHea;

    public Fixed version; // 0x00010000 for version 1.0.
    public FWord ascender; // Typographic ascent. (Distance from baseline of highest ascender) 
    public FWord descender; // Typographic descent. (Distance from baseline of lowest descender) 
    public FWord lineGap; // Typographic line gap. Negative LineGap values are treated as zero in Windows 3.1, System 6, and System 7.
    public UFWord advanceWidthMax;
    public FWord minLeftSideBearing;
    public FWord minRightSideBearing;
    public FWord xMaxExtent;
    public short caretSlopeRise;
    public short caretSlopeRun;
    public short reserved1;
    public short reserved2;
    public short reserved3;
    public short reserved4;
    public short reserved5;
    public short metricDataFormat;
    public ushort numberOfHMetrics;

    public HorizontalHeaderTable(FontImage fontImage)
      : base(fontImage, Tag)
    {
      Read();
    }

    public void Read()
    {
      try
      {
        this.version = this.fontImage.ReadFixed();
        this.ascender = this.fontImage.ReadFWord();
        this.descender = this.fontImage.ReadFWord();
        this.lineGap = this.fontImage.ReadFWord();
        this.advanceWidthMax = this.fontImage.ReadUFWord();
        this.minLeftSideBearing = this.fontImage.ReadFWord();
        this.minRightSideBearing = this.fontImage.ReadFWord();
        this.xMaxExtent = this.fontImage.ReadFWord();
        this.caretSlopeRise = this.fontImage.ReadShort();
        this.caretSlopeRun = this.fontImage.ReadShort();
        this.reserved1 = this.fontImage.ReadShort();
        this.reserved2 = this.fontImage.ReadShort();
        this.reserved3 = this.fontImage.ReadShort();
        this.reserved4 = this.fontImage.ReadShort();
        this.reserved5 = this.fontImage.ReadShort();
        this.metricDataFormat = this.fontImage.ReadShort();
        this.numberOfHMetrics = this.fontImage.ReadUShort();
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }
  }

  internal class HorizontalMetrics : TrueTypeFontTable
  {
    public const string Tag = "----";

    public ushort advanceWidth;
    public short lsb;

    public HorizontalMetrics(FontImage fontImage)
      : base(fontImage, Tag)
    {
      Read();
    }
     
    public void Read()
    {
      try
      {
        this.advanceWidth = this.fontImage.ReadUFWord();
        this.lsb = this.fontImage.ReadFWord();
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }
  }

  /// <summary>
  /// The type longHorMetric is defined as an array where each element has two parts:
  /// the advance width, which is of type USHORT, and the left side bearing, which is of type SHORT.
  /// These fields are in font design units.
  /// </summary>
  internal class HorizontalMetricsTable : TrueTypeFontTable
  {
    public const string Tag = TableTagNames.HMtx;

    public HorizontalMetrics[] metrics;
    public FWord[] leftSideBearing;

    public HorizontalMetricsTable(FontImage fontImage)
      : base(fontImage, Tag)
    {
      Read();
    }

    public void Read()
    {
      try
      {
        HorizontalHeaderTable hhea = this.fontImage.hhea;
        MaximumProfileTable maxp = this.fontImage.maxp;
        if (hhea != null && maxp != null)
        {
          int numMetrics = hhea.numberOfHMetrics; //->NumberOfHMetrics();
          int numLsbs = maxp.numGlyphs - numMetrics;

          Debug.Assert(numMetrics != 0);
          Debug.Assert(numLsbs >= 0);

          this.metrics = new HorizontalMetrics[numMetrics];
          for (int idx = 0; idx < numMetrics; idx++)
            this.metrics[idx] = new HorizontalMetrics(this.fontImage);

          if (numLsbs > 0)
          {
            this.leftSideBearing = new FWord[numLsbs];
            for (int idx = 0; idx < numLsbs; idx++)
              this.leftSideBearing[idx] = this.fontImage.ReadFWord();
          }
        }
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }
  }

  /// <summary>
  /// This table establishes the memory requirements for this font.
  /// Fonts with CFF data must use Version 0.5 of this table, specifying only the numGlyphs field.
  /// Fonts with TrueType outlines must use Version 1.0 of this table, where all data is required.
  /// Both formats of OpenType require a 'maxp' table because a number of applications call the 
  /// Windows GetFontData() API on the 'maxp' table to determine the number of glyphs in the font.
  /// </summary>
  internal class MaximumProfileTable : TrueTypeFontTable
  {
    public const string Tag = TableTagNames.MaxP;

    public Fixed version;
    public ushort numGlyphs;
    public ushort maxPoints;
    public ushort maxContours;
    public ushort maxCompositePoints;
    public ushort maxCompositeContours;
    public ushort maxZones;
    public ushort maxTwilightPoints;
    public ushort maxStorage;
    public ushort maxFunctionDefs;
    public ushort maxInstructionDefs;
    public ushort maxStackElements;
    public ushort maxSizeOfInstructions;
    public ushort maxComponentElements;
    public ushort maxComponentDepth;

    public MaximumProfileTable(FontImage fontImage)
      : base(fontImage, Tag)
    {
      Read();
    }

    public void Read()
    {
      try
      {
        this.version = this.fontImage.ReadFixed();
        this.numGlyphs = this.fontImage.ReadUShort();
        this.maxPoints = this.fontImage.ReadUShort();
        this.maxContours = this.fontImage.ReadUShort();
        this.maxCompositePoints = this.fontImage.ReadUShort();
        this.maxCompositeContours = this.fontImage.ReadUShort();
        this.maxZones = this.fontImage.ReadUShort();
        this.maxTwilightPoints = this.fontImage.ReadUShort();
        this.maxStorage = this.fontImage.ReadUShort();
        this.maxFunctionDefs = this.fontImage.ReadUShort();
        this.maxInstructionDefs = this.fontImage.ReadUShort();
        this.maxStackElements = this.fontImage.ReadUShort();
        this.maxSizeOfInstructions = this.fontImage.ReadUShort();
        this.maxComponentElements = this.fontImage.ReadUShort();
        this.maxComponentDepth = this.fontImage.ReadUShort();
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }
  }

  /// <summary>
  /// The naming table allows multilingual strings to be associated with the OpenTypeTM font file.
  /// These strings can represent copyright notices, font names, family names, style names, and so on.
  /// To keep this table short, the font manufacturer may wish to make a limited set of entries in some
  /// small set of languages; later, the font can be "localized" and the strings translated or added.
  /// Other parts of the OpenType font file that require these strings can then refer to them simply by
  /// their index number. Clients that need a particular string can look it up by its platform ID, character
  /// encoding ID, language ID and name ID. Note that some platforms may require single byte character
  /// strings, while others may require double byte strings. 
  ///
  /// For historical reasons, some applications which install fonts perform version control using Macintosh
  /// platform (platform ID 1) strings from the 'name' table. Because of this, we strongly recommend that
  /// the 'name' table of all fonts include Macintosh platform strings and that the syntax of the version
  /// number (name id 5) follows the guidelines given in this document.
  /// </summary>
  internal class NameTable : TrueTypeFontTable
  {
    public const string Tag = TableTagNames.Name;

    byte[] bytes;

    public NameTable(FontImage fontImage)
      : base(fontImage, Tag)
    {
      Read();
    }

    public void Read()
    {
      try
      {
        this.bytes = new byte[DirectoryEntry.PaddedLength];
        Buffer.BlockCopy(this.fontImage.Bytes, DirectoryEntry.Offset, bytes, 0, DirectoryEntry.Length);
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }
  }

  /// <summary>
  /// The OS/2 table consists of a set of metrics that are required in OpenType fonts. 
  /// </summary>
  internal class OS2Table : TrueTypeFontTable
  {
    public const string Tag = TableTagNames.OS2;

    public ushort version;
    public short xAvgCharWidth;
    public ushort usWeightClass;
    public ushort usWidthClass;
    public ushort fsType;
    public short ySubscriptXSize;
    public short ySubscriptYSize;
    public short ySubscriptXOffset;
    public short ySubscriptYOffset;
    public short ySuperscriptXSize;
    public short ySuperscriptYSize;
    public short ySuperscriptXOffset;
    public short ySuperscriptYOffset;
    public short yStrikeoutSize;
    public short yStrikeoutPosition;
    public short sFamilyClass;
    public byte[] panose; // = new byte[10];
    public uint ulUnicodeRange1; // Bits 0-31
    public uint ulUnicodeRange2; // Bits 32-63
    public uint ulUnicodeRange3; // Bits 64-95
    public uint ulUnicodeRange4; // Bits 96-127
    public string achVendID; // = "";
    public ushort fsSelection;
    public ushort usFirstCharIndex;
    public ushort usLastCharIndex;
    public short sTypoAscender;
    public short sTypoDescender;
    public short sTypoLineGap;
    public ushort usWinAscent;
    public ushort usWinDescent;
    // version >= 1
    public uint ulCodePageRange1; // Bits 0-31
    public uint ulCodePageRange2; // Bits 32-63
    // version >= 2
    public short sxHeight;
    public short sCapHeight;
    public ushort usDefaultChar;
    public ushort usBreakChar;
    public ushort usMaxContext;

    public OS2Table(FontImage fontImage)
      : base(fontImage, Tag)
    {
      Read();
    }

    public void Read()
    {
      try
      {
        this.version = this.fontImage.ReadUShort();
        this.xAvgCharWidth = this.fontImage.ReadShort();
        this.usWeightClass = this.fontImage.ReadUShort();
        this.usWidthClass = this.fontImage.ReadUShort();
        this.fsType = this.fontImage.ReadUShort();
        this.ySubscriptXSize = this.fontImage.ReadShort();
        this.ySubscriptYSize = this.fontImage.ReadShort();
        this.ySubscriptXOffset = this.fontImage.ReadShort();
        this.ySubscriptYOffset = this.fontImage.ReadShort();
        this.ySuperscriptXSize = this.fontImage.ReadShort();
        this.ySuperscriptYSize = this.fontImage.ReadShort();
        this.ySuperscriptXOffset = this.fontImage.ReadShort();
        this.ySuperscriptYOffset = this.fontImage.ReadShort();
        this.yStrikeoutSize = this.fontImage.ReadShort();
        this.yStrikeoutPosition = this.fontImage.ReadShort();
        this.sFamilyClass = this.fontImage.ReadShort();
        this.panose = this.fontImage.ReadBytes(10);
        this.ulUnicodeRange1 = this.fontImage.ReadULong();
        this.ulUnicodeRange2 = this.fontImage.ReadULong();
        this.ulUnicodeRange3 = this.fontImage.ReadULong();
        this.ulUnicodeRange4 = this.fontImage.ReadULong();
        this.achVendID = this.fontImage.ReadString(4);
        this.fsSelection = this.fontImage.ReadUShort();
        this.usFirstCharIndex = this.fontImage.ReadUShort();
        this.usLastCharIndex = this.fontImage.ReadUShort();
        this.sTypoAscender = this.fontImage.ReadShort();
        this.sTypoDescender = this.fontImage.ReadShort();
        this.sTypoLineGap = this.fontImage.ReadShort();
        this.usWinAscent = this.fontImage.ReadUShort();
        this.usWinDescent = this.fontImage.ReadUShort();

        if (this.version >= 1)
        {
          this.ulCodePageRange1 = this.fontImage.ReadULong();
          this.ulCodePageRange2 = this.fontImage.ReadULong();

          if (this.version >= 2)
          {
            this.sxHeight = this.fontImage.ReadShort();
            this.sCapHeight = this.fontImage.ReadShort();
            this.usDefaultChar = this.fontImage.ReadUShort();
            this.usBreakChar = this.fontImage.ReadUShort();
            this.usMaxContext = this.fontImage.ReadUShort();
          }
        }
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }
  }

  /// <summary>
  /// This table contains additional information needed to use TrueType or OpenTypeTM fonts
  /// on PostScript printers. 
  /// </summary>
  internal class PostScriptTable : TrueTypeFontTable
  {
    public const string Tag = TableTagNames.Post;

    public Fixed formatType;
    public float italicAngle;
    public FWord underlinePosition;
    public FWord underlineThickness;
    public ulong isFixedPitch;
    public ulong minMemType42;
    public ulong maxMemType42;
    public ulong minMemType1;
    public ulong maxMemType1;

    public PostScriptTable(FontImage fontImage)
      : base(fontImage, Tag)
    {
      Read();
    }

    public void Read()
    {
      try
      {
        this.formatType = this.fontImage.ReadFixed();
        this.italicAngle = this.fontImage.ReadFixed() / 65536f;
        this.underlinePosition = this.fontImage.ReadFWord();
        this.underlineThickness = this.fontImage.ReadFWord();
        this.isFixedPitch = this.fontImage.ReadULong();
        this.minMemType42 = this.fontImage.ReadULong();
        this.maxMemType42 = this.fontImage.ReadULong();
        this.minMemType1 = this.fontImage.ReadULong();
        this.maxMemType1 = this.fontImage.ReadULong();
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }
  }

  /// <summary>
  /// This table contains a list of values that can be referenced by instructions.
  /// They can be used, among other things, to control characteristics for different glyphs.
  /// The length of the table must be an integral number of FWORD units. 
  /// </summary>
  internal class ControlValueTable : TrueTypeFontTable
  {
    public const string Tag = TableTagNames.Cvt;

    FWord[] array; // List of n values referenceable by instructions. n is the number of FWORD items that fit in the size of the table.

    public ControlValueTable(FontImage fontImage)
      : base(fontImage, Tag)
    {
      DirectoryEntry.Tag = TableTagNames.Cvt;
      DirectoryEntry = fontImage.tableDictionary[TableTagNames.Cvt];
      Read();
    }

    public void Read()
    {
      try
      {
        int length = DirectoryEntry.Length / 2;
        this.array = new FWord[length];
        for (int idx = 0; idx < length; idx++)
          this.array[idx] = this.fontImage.ReadFWord();
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }
  }

  /// <summary>
  /// This table is similar to the CVT Program, except that it is only run once, when the font is first used.
  /// It is used only for FDEFs and IDEFs. Thus the CVT Program need not contain function definitions.
  /// However, the CVT Program may redefine existing FDEFs or IDEFs. 
  /// </summary>
  internal class FontProgram  : TrueTypeFontTable
  {
    public const string Tag = TableTagNames.Fpgm;

    byte[] bytes; // Instructions. n is the number of BYTE items that fit in the size of the table.

    public FontProgram(FontImage fontImage)
      : base(fontImage, Tag)
    {
      DirectoryEntry.Tag = TableTagNames.Fpgm;
      DirectoryEntry = fontImage.tableDictionary[TableTagNames.Fpgm];
      Read();
    }

    public void Read()
    {
      try
      {
        int length = DirectoryEntry.Length;
        this.bytes = new byte[length];
        for (int idx = 0; idx < length; idx++)
          this.bytes[idx] = this.fontImage.ReadByte();
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }
  }

  /// <summary>
  /// The Control Value Program consists of a set of TrueType instructions that will be executed whenever the font or 
  /// point size or transformation matrix change and before each glyph is interpreted. Any instruction is legal in the
  /// CVT Program but since no glyph is associated with it, instructions intended to move points within a particula
  /// glyph outline cannot be used in the CVT Program. The name 'prep' is anachronistic. 
  /// </summary>
  internal class ControlValueProgram : TrueTypeFontTable
  {
    public const string Tag = TableTagNames.Prep;

    byte[] bytes; // Set of instructions executed whenever point size or font or transformation change. n is the number of BYTE items that fit in the size of the table.

    public ControlValueProgram(FontImage fontImage)
      : base(fontImage, Tag)
    {
      DirectoryEntry.Tag = TableTagNames.Prep;
      DirectoryEntry = fontImage.tableDictionary[TableTagNames.Prep];
      Read();
    }

    public void Read()
    {
      try
      {
        int length = DirectoryEntry.Length;
        this.bytes = new byte[length];
        for (int idx = 0; idx < length; idx++)
          this.bytes[idx] = this.fontImage.ReadByte();
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }
  }

  /// <summary>
  /// This table contains information that describes the glyphs in the font in the TrueType outline format.
  /// Information regarding the rasterizer (scaler) refers to the TrueType rasterizer. 
  /// </summary>
  internal class GlyphSubstitutionTable : TrueTypeFontTable
  {
    public const string Tag = TableTagNames.GSUB;

    public GlyphSubstitutionTable(FontImage fontImage)
      : base(fontImage, Tag)
    {
      DirectoryEntry.Tag = TableTagNames.GSUB;
      DirectoryEntry = fontImage.tableDictionary[TableTagNames.GSUB];
      Read();
    }

    public void Read()
    {
      try
      {
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }
  }

  // StL: Comes from PDFlib or so. Delete it.
  //  /// <summary>
  //  /// Windows code page IDs.
  //  /// </summary>
  //  internal enum WindowsCodePage
  //  {
  //    CP1252 =  0,  // Latin 1
  //    CP1250 =  1,  // Latin 2: Eastern Europe
  //    CP1251 =  2,  // Cyrillic
  //    CP1253 =  3,  // Greek
  //    CP1254 =  4,  // Turkish
  //    CP1255 =  5,  // Hebrew
  //    CP1256 =  6,  // Arabic
  //    CP1257 =  7,  // Windows Baltic
  //    CP1258 =  8,  // Vietnamese
  //    CP874  = 16,  // Thai
  //    CP932  = 17,  // JIS/Japan
  //    CP936  = 18,  // Chinese: Simplified chars--PRC and Singapore
  //    CP949  = 19,  // Korean Wansung
  //    CP950  = 20,  // Chinese: Traditional chars--Taiwan and Hong Kong
  //    CP1361 = 21,  // Korean Johab
  //    CP869  = 48,  // IBM Greek
  //    CP866  = 49,  // MS-DOS Russian
  //    CP865  = 50,  // MS-DOS Nordic
  //    CP864  = 51,  // Arabic
  //    CP863  = 52,  // MS-DOS Canadian French
  //    CP862  = 53,  // Hebrew
  //    CP861  = 54,  // MS-DOS Icelandic
  //    CP860  = 55,  // MS-DOS Portuguese
  //    CP857  = 56,  // IBM Turkish
  //    CP855  = 57,  // IBM Cyrillic; primarily Russian
  //    CP852  = 58,  // Latin 2
  //    CP775  = 59,  // MS-DOS Baltic
  //    CP737  = 60,  // Greek; former 437 G
  //    CP708  = 61,  // Arabic; ASMO 708
  //    CP850  = 62,  // WE/Latin 1
  //    CP437  = 63,  // US
  //  }
}
