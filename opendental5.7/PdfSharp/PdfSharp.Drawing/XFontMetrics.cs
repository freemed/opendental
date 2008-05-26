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

namespace PdfSharp.Drawing
{
  /// <summary>
  /// Collects information of a font.
  /// </summary>
  public sealed class XFontMetrics
  {
    internal XFontMetrics(string name, double ascent, double descent, double leading,
      double capHeight, double xHeight, double stemV, double stemH, double averageWidth, double maxWidth)
    {
      this.name = name;
      this.ascent = ascent;
      this.descent = descent;
      this.leading = leading;
      this.capHeight = capHeight;
      this.xHeight = xHeight;
      this.stemV = stemV;
      this.stemH = stemH;
      this.averageWidth = averageWidth;
      this.maxWidth = maxWidth;
    }

    /// <summary>
    /// Gets the font name.
    /// </summary>
    public string Name
    {
      get { return this.name; }
    }
    string name;

    /// <summary>
    /// Gets the ascent value.
    /// </summary>
    public double Ascent
    {
      get { return this.ascent; }
    }
    double ascent;

    /// <summary>
    /// Gets the descent value.
    /// </summary>
    public double Descent
    {
      get { return this.descent; }
    }
    double descent;

    /// <summary>
    /// Gets the average width.
    /// </summary>
    /// <value>The average width.</value>
    public double AverageWidth
    {
      get { return this.averageWidth; }
    }
    double averageWidth;

    /// <summary>
    /// Gets the height of capital letters.
    /// </summary>
    public double CapHeight
    {
      get { return this.capHeight; }
    }
    double capHeight;

    /// <summary>
    /// Gets the leading value.
    /// </summary>
    public double Leading
    {
      get { return this.leading; }
    }
    double leading;

    /// <summary>
    /// Gets the maximum width of a character.
    /// </summary>
    public double MaxWidth
    {
      get { return this.maxWidth; }
    }
    double maxWidth;

    /// <summary>
    /// Gets an internal value.
    /// </summary>
    public double StemH
    {
      get { return this.stemH; }
    }
    double stemH;

    /// <summary>
    /// Gets an internal value.
    /// </summary>
    public double StemV
    {
      get { return this.stemV; }
    }
    double stemV;

    /// <summary>
    /// Gets the height of a character.
    /// </summary>
    public double XHeight
    {
      get { return this.xHeight; }
    }
    double xHeight;
  }
}
