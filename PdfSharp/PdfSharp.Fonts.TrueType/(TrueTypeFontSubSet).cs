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

#if DEBUG

using System;
using System.Collections;
using System.Collections.Generic;
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
using PdfSharp.Drawing;
using PdfSharp.Pdf.Internal;

// Stuff from iTextSharp to double-check implementation of font sub-setting
namespace PdfSharp.Fonts.TrueType
{
  /** Subsets a True Type font by removing the unneeded glyphs from
   * the font.
   */
  internal class TrueTypeFontSubSet
  {
    internal static string[] tableNamesSimple = {"cvt ", "fpgm", "glyf", "head", "hhea", "hmtx", "loca", "maxp", "prep"};
    internal static string[] tableNamesCmap = {"cmap", "cvt ", "fpgm", "glyf", "head", "hhea", "hmtx", "loca", "maxp", "prep"};
    internal static string[] tableNamesExtra = {"OS/2", "cmap", "cvt ", "fpgm", "glyf", "head", "hhea", "hmtx", "loca", "maxp", "name", "prep"};
    internal static int[] entrySelectors = { 0, 0, 1, 1, 2, 2, 2, 2, 3, 3, 3, 3, 3, 3, 3, 3, 4, 4, 4, 4, 4 };
    internal static int TABLE_CHECKSUM = 0;
    internal static int TABLE_OFFSET = 1;
    internal static int TABLE_LENGTH = 2;
    internal static int HEAD_LOCA_FORMAT_OFFSET = 51;

    internal static int ARG_1_AND_2_ARE_WORDS = 1;
    internal static int WE_HAVE_A_SCALE = 8;
    internal static int MORE_COMPONENTS = 32;
    internal static int WE_HAVE_AN_X_AND_Y_SCALE = 64;
    internal static int WE_HAVE_A_TWO_BY_TWO = 128;

    [Conditional("DEBUG")]
    internal void CompareBytes(byte[] buffer1, byte[] buffer2)
    {
      int length1 = buffer1.Length;
      int length2 = buffer2.Length;
      Debug.Assert(length1 == length2);
      for (int idx = 0; idx < length1; idx++)
        Debug.Assert(buffer1[idx] == buffer2[idx]);
    }

    /** Contains the location of the several tables. The key is the name of
     * the table and the value is an <CODE>int[3]</CODE> where position 0
     * is the checksum, position 1 is the offset from the start of the file
     * and position 2 is the length of the table.
     */
    protected Hashtable tableDirectory;
    /** The file in use.
     */
    protected FontImage rf;
    /** The file name.
     */
    protected string fileName;
    protected bool includeCmap;
    protected bool includeExtras;
    protected bool locaShortTable;
    protected int[] locaTable;
    protected Dictionary<int, object> glyphsUsed;
    protected ArrayList glyphsInList;
    protected int tableGlyphOffset;
    protected int[] newLocaTable;
    protected byte[] newLocaTableOut;
    protected byte[] newGlyfTable;
    protected int glyfTableRealSize;
    protected int locaTableRealSize;
    protected byte[] outFont;
    protected int fontPtr;
    protected int directoryOffset;

    /** Creates a new TrueTypeFontSubSet
     * @param directoryOffset The offset from the start of the file to the table directory
     * @param fileName the file name of the font
     * @param glyphsUsed the glyphs used
     * @param includeCmap <CODE>true</CODE> if the table cmap is to be included in the generated font
     */
    internal TrueTypeFontSubSet(string fileName, FontImage rf, Dictionary<int, object> glyphsUsed, int directoryOffset, bool includeCmap, bool includeExtras)
    {
      this.fileName = fileName;
      this.rf = rf;
      this.glyphsUsed = glyphsUsed;
      this.includeCmap = includeCmap;
      this.includeExtras = includeExtras;
      this.directoryOffset = directoryOffset;
      glyphsInList = new ArrayList(glyphsUsed.Keys);
    }

    /** Does the actual work of subsetting the font.
     * @throws IOException on error
     * @throws Exception on error
     * @return the subset font
     */
    internal byte[] Process()
    {
      try
      {
        //rf.ReOpen();
        CreateTableDirectory();
        ReadLoca();
        FlatGlyphs();
        CreateNewGlyphTables();
        LocaTobytes();
        AssembleFont();
        return outFont;
      }
      finally
      {
        try
        {
          //rf.Close();
        }
        catch
        {
          // empty on purpose
        }
      }
    }

    protected void AssembleFont()
    {
      int[] tableLocation;
      int fullFontSize = 0;
      string[] tableNames;
      if (includeExtras)
        tableNames = tableNamesExtra;
      else
      {
        if (includeCmap)
          tableNames = tableNamesCmap;
        else
          tableNames = tableNamesSimple;
      }
      int tablesUsed = 2;
      int len = 0;
      for (int k = 0; k < tableNames.Length; ++k)
      {
        string name = tableNames[k];
        if (name.Equals("glyf") || name.Equals("loca"))
          continue;
        tableLocation = (int[])tableDirectory[name];
        if (tableLocation == null)
          continue;
        ++tablesUsed;
        fullFontSize += (tableLocation[TABLE_LENGTH] + 3) & (~3);
      }
      fullFontSize += newLocaTableOut.Length;
      fullFontSize += newGlyfTable.Length;
      int iref = 16 * tablesUsed + 12;
      fullFontSize += iref;
      outFont = new byte[fullFontSize];
      fontPtr = 0;
      WriteFontInt(0x00010000);
      WriteFontShort(tablesUsed);
      int selector = entrySelectors[tablesUsed];
      WriteFontShort((1 << selector) * 16);
      WriteFontShort(selector);
      WriteFontShort((tablesUsed - (1 << selector)) * 16);
      for (int k = 0; k < tableNames.Length; ++k)
      {
        string name = tableNames[k];
        tableLocation = (int[])tableDirectory[name];
        if (tableLocation == null)
          continue;
        WriteFontString(name);
        if (name.Equals("glyf"))
        {
          WriteFontInt((int)TrueTypeFontTable.CalcChecksum(newGlyfTable));
          len = glyfTableRealSize;
        }
        else if (name.Equals("loca"))
        {
          WriteFontInt((int)TrueTypeFontTable.CalcChecksum(newLocaTableOut));
          len = locaTableRealSize;
        }
        else
        {
          WriteFontInt(tableLocation[TABLE_CHECKSUM]);
          len = tableLocation[TABLE_LENGTH];
        }
        WriteFontInt(iref);
        WriteFontInt(len);
        iref += (len + 3) & (~3);
      }
#if VERBOSE
      Debug.WriteLine("Start TrueTypeFontSubSet");
#endif
      for (int k = 0; k < tableNames.Length; ++k)
      {
        string name = tableNames[k];
        tableLocation = (int[])tableDirectory[name];
        if (tableLocation == null)
          continue;
        if (name.Equals("glyf"))
        {
          Array.Copy(newGlyfTable, 0, outFont, fontPtr, newGlyfTable.Length);
#if VERBOSE
          Debug.WriteLine(String.Format("  Write Table '{0}', offset={1}, length={2}, checksum={3}, ", name, fontPtr, glyfTableRealSize, TrueTypeFontTable.CalcChecksum(newGlyfTable)));
#endif
          fontPtr += newGlyfTable.Length;
          newGlyfTable = null;
        }
        else if (name.Equals("loca"))
        {
          Array.Copy(newLocaTableOut, 0, outFont, fontPtr, newLocaTableOut.Length);
#if VERBOSE
          Debug.WriteLine(String.Format("  Write Table '{0}', offset={1}, length={2}, checksum={3}, ", name, fontPtr, locaTableRealSize, TrueTypeFontTable.CalcChecksum(newLocaTableOut)));
#endif
          fontPtr += newLocaTableOut.Length;
          newLocaTableOut = null;
        }
        else
        {
          rf.Position = tableLocation[TABLE_OFFSET];
          rf.Read(outFont, fontPtr, tableLocation[TABLE_LENGTH]);
#if VERBOSE
          Debug.WriteLine(String.Format("  Write Table '{0}', offset={1}, length={2}, checksum={3}, ", name, fontPtr, tableLocation[TABLE_LENGTH], (uint)tableLocation[TABLE_CHECKSUM]));
#endif
          fontPtr += (tableLocation[TABLE_LENGTH] + 3) & (~3);
        }
      }
#if VERBOSE
      Debug.WriteLine("End TrueTypeFontSubSet");
#endif
    }

    protected void CreateTableDirectory()
    {
      tableDirectory = new Hashtable();
      rf.Position = directoryOffset;
      int id = rf.ReadLong();
      if (id != 0x00010000)
        throw new Exception(fileName + " is not a true type file.");
      int num_tables = rf.ReadUFWord();
      rf.SeekOffset(6);
      for (int k = 0; k < num_tables; ++k)
      {
        string tag = ReadStandardString(4);
        int[] tableLocation = new int[3];
        tableLocation[TABLE_CHECKSUM] = rf.ReadLong();
        tableLocation[TABLE_OFFSET] = rf.ReadLong();
        tableLocation[TABLE_LENGTH] = rf.ReadLong();
        tableDirectory[tag] = tableLocation;
      }
    }

    protected void ReadLoca()
    {
      int[] tableLocation;
      tableLocation = (int[])tableDirectory["head"];
      if (tableLocation == null)
        throw new Exception("Table 'head' does not exist in " + fileName);
      rf.Position = tableLocation[TABLE_OFFSET] + HEAD_LOCA_FORMAT_OFFSET;
      locaShortTable = (rf.ReadUFWord() == 0);
      tableLocation = (int[])tableDirectory["loca"];
      if (tableLocation == null)
        throw new Exception("Table 'loca' does not exist in " + fileName);
      rf.Position = tableLocation[TABLE_OFFSET];
      if (locaShortTable)
      {
        int entries = tableLocation[TABLE_LENGTH] / 2;
        locaTable = new int[entries];
        for (int k = 0; k < entries; ++k)
          locaTable[k] = rf.ReadUFWord() * 2;
      }
      else
      {
        int entries = tableLocation[TABLE_LENGTH] / 4;
        locaTable = new int[entries];
        for (int k = 0; k < entries; ++k)
          locaTable[k] = rf.ReadLong();
      }
    }

    protected void CreateNewGlyphTables()
    {
      newLocaTable = new int[locaTable.Length];
      int[] activeGlyphs = new int[glyphsInList.Count];
      for (int k = 0; k < activeGlyphs.Length; ++k)
        activeGlyphs[k] = (int)glyphsInList[k];
      Array.Sort(activeGlyphs);
      int glyfSize = 0;
      for (int k = 0; k < activeGlyphs.Length; ++k)
      {
        int glyph = activeGlyphs[k];
        glyfSize += locaTable[glyph + 1] - locaTable[glyph];
      }
      glyfTableRealSize = glyfSize;
      glyfSize = (glyfSize + 3) & (~3);
      newGlyfTable = new byte[glyfSize];
      int glyfPtr = 0;
      int listGlyf = 0;
      for (int k = 0; k < newLocaTable.Length; ++k)
      {
        newLocaTable[k] = glyfPtr;
        if (listGlyf < activeGlyphs.Length && activeGlyphs[listGlyf] == k)
        {
          ++listGlyf;
          newLocaTable[k] = glyfPtr;
          int start = locaTable[k];
          int len = locaTable[k + 1] - start;
          if (len > 0)
          {
            rf.Position = tableGlyphOffset + start;
            rf.Read(newGlyfTable, glyfPtr, len);
            glyfPtr += len;
          }
        }
      }
    }

    protected void LocaTobytes()
    {
      if (locaShortTable)
        locaTableRealSize = newLocaTable.Length * 2;
      else
        locaTableRealSize = newLocaTable.Length * 4;
      newLocaTableOut = new byte[(locaTableRealSize + 3) & (~3)];
      outFont = newLocaTableOut;
      fontPtr = 0;
      for (int k = 0; k < newLocaTable.Length; ++k)
      {
        if (locaShortTable)
          WriteFontShort(newLocaTable[k] / 2);
        else
          WriteFontInt(newLocaTable[k]);
      }
    }

    protected void FlatGlyphs()
    {
      int[] tableLocation;
      tableLocation = (int[])tableDirectory["glyf"];
      if (tableLocation == null)
        throw new Exception("Table 'glyf' does not exist in " + fileName);
      int glyph0 = 0;
      if (!glyphsUsed.ContainsKey(glyph0))
      {
        glyphsUsed[glyph0] = null;
        glyphsInList.Add(glyph0);
      }
      tableGlyphOffset = tableLocation[TABLE_OFFSET];
      for (int k = 0; k < glyphsInList.Count; ++k)
      {
        int glyph = (int)glyphsInList[k];
        CheckGlyphComposite(glyph);
      }
    }

    protected void CheckGlyphComposite(int glyph)
    {
      int start = locaTable[glyph];
      if (start == locaTable[glyph + 1]) // no contour
        return;
      rf.Position = tableGlyphOffset + start;
      int numContours = rf.ReadShort();
      if (numContours >= 0)
        return;
      rf.SeekOffset(8);
      for (; ; )
      {
        int flags = rf.ReadUFWord();
        int cGlyph = rf.ReadUFWord();
        if (!glyphsUsed.ContainsKey(cGlyph))
        {
          glyphsUsed[cGlyph] = null;
          glyphsInList.Add(cGlyph);
        }
        if ((flags & MORE_COMPONENTS) == 0)
          return;
        int skip;
        if ((flags & ARG_1_AND_2_ARE_WORDS) != 0)
          skip = 4;
        else
          skip = 2;
        if ((flags & WE_HAVE_A_SCALE) != 0)
          skip += 2;
        else if ((flags & WE_HAVE_AN_X_AND_Y_SCALE) != 0)
          skip += 4;
        if ((flags & WE_HAVE_A_TWO_BY_TWO) != 0)
          skip += 8;
        rf.SeekOffset(skip);
      }
    }

    /** Reads a <CODE>string</CODE> from the font file as bytes using the Cp1252
     *  encoding.
     * @param length the length of bytes to read
     * @return the <CODE>string</CODE> read
     * @throws IOException the font file could not be read
     */
    protected string ReadStandardString(int length)
    {
      byte[] buf = new byte[length];
      rf.Read(buf);
      return PdfEncoders.WinAnsiEncoding.GetString(buf);
    }

    protected void WriteFontShort(int n)
    {
      outFont[fontPtr++] = (byte)(n >> 8);
      outFont[fontPtr++] = (byte)(n);
    }

    protected void WriteFontInt(int n)
    {
      outFont[fontPtr++] = (byte)(n >> 24);
      outFont[fontPtr++] = (byte)(n >> 16);
      outFont[fontPtr++] = (byte)(n >> 8);
      outFont[fontPtr++] = (byte)(n);
    }

    protected void WriteFontString(string s)
    {
      //byte[] b = PdfEncodings.ConvertToBytes(s, BaseFont.WINANSI);
      byte[] b = PdfEncoders.WinAnsiEncoding.GetBytes(s);
      Array.Copy(b, 0, outFont, fontPtr, b.Length);
      fontPtr += b.Length;
    }

  }
}
#endif
