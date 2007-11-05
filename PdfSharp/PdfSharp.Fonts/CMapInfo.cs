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
using System.Collections;
using System.Collections.Generic;
using PdfSharp.Fonts.TrueType;

namespace PdfSharp.Fonts
{
  /// <summary>
  /// Helper class that determines the characters used in a particular font.
  /// </summary>
  internal class CMapInfo
  {
    public CMapInfo(TrueTypeDescriptor descriptor)
    {
      Debug.Assert(descriptor != null);
      this.descriptor = descriptor;
    }
    internal TrueTypeDescriptor descriptor;

    /// <summary>
    /// Adds the characters of the specifed string the the hashtable
    /// </summary>
    public void AddChars(string text)
    {
      if (text != null)
      {
        bool symbol = this.descriptor.fontImage.cmap.symbol;
        int length = text.Length;
        for (int idx = 0; idx < length; idx++)
        {
          char ch = text[idx];
          if (!CharacterToGlyphIndex.ContainsKey(ch))
          {
            int glyphIndex = 0;
            if (this.descriptor != null)
            {
              if (symbol)
              {
                glyphIndex = (int)ch + (descriptor.fontImage.os2.usFirstCharIndex & 0xFF00); // @@@
                glyphIndex = descriptor.CharCodeToGlyphIndex((char)glyphIndex);
              }
              else
                glyphIndex = descriptor.CharCodeToGlyphIndex(ch);
            }

            CharacterToGlyphIndex.Add(ch, glyphIndex);
            //GlyphIndices.Add(glyphIndex, null);
            GlyphIndices[glyphIndex] = null;
            this.MinChar = (char)Math.Min(this.MinChar, ch);
            this.MaxChar = (char)Math.Max(this.MaxChar, ch);
          }
        }
      }
    }

    internal bool Contains(char ch)
    {
      return CharacterToGlyphIndex.ContainsKey(ch);
    }

    public char[] Chars
    {
      get
      {
        char[] chars = new char[CharacterToGlyphIndex.Count];
        CharacterToGlyphIndex.Keys.CopyTo(chars, 0);
        Array.Sort(chars);
        return chars;
      }
    }

    public char MinChar = Char.MaxValue;
    public char MaxChar = Char.MinValue;
    public Dictionary<char, int> CharacterToGlyphIndex = new Dictionary<char, int>();
    public Dictionary<int, object> GlyphIndices = new Dictionary<int, object>();
  }
}
