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
using System.IO;
#if Gdip
using System.Drawing;
#endif
#if Wpf
using System.Windows;
using System.Windows.Media;
#endif
using PdfSharp.Internal;

namespace PdfSharp.Drawing
{
  /// <summary>
  /// Defines a single color object used to fill shapes and draw text.
  /// </summary>
  public class XSolidBrush : XBrush
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="XSolidBrush"/> class.
    /// </summary>
    public XSolidBrush()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="XSolidBrush"/> class.
    /// </summary>
    public XSolidBrush(XColor color)
      : this(color, false)
    {
    }

    internal XSolidBrush(XColor color, bool immutable)
    {
      this.color = color;
      this.immutable = immutable;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="XSolidBrush"/> class.
    /// </summary>
    public XSolidBrush(XSolidBrush brush)
    {
      this.color = brush.Color;
    }

    /// <summary>
    /// Gets or sets the color of this brush.
    /// </summary>
    public XColor Color
    {
      get { return this.color; }
      set
      {
        if (this.immutable)
          throw new ArgumentException(PSSR.CannotChangeImmutableObject("XSolidBrush"));
        this.dirty = this.dirty || this.color != value;
        this.color = value;
      }
    }
    internal XColor color;

#if Gdip
    internal override Brush RealizeGdiBrush()
    {
      if (this.dirty)
      {
        if (this.brush == null)
          this.brush = new SolidBrush(this.color.ToGdiColor());
        else
        {
          this.brush.Color = this.color.ToGdiColor();
        }
        this.dirty = false;
      }

      System.Drawing.Color clr = this.color.ToGdiColor();
      SolidBrush brush1 = new SolidBrush(clr); //System.Drawing.Color.FromArgb(128, 128, 0, 0));
      Debug.Assert(this.brush.Color == brush1.Color);
      return brush1;//this.brush;
    }
#endif

#if Wpf
    internal override Brush RealizeWpfBrush()
    {
      if (this.dirty)
      {
        if (this.brush == null)
          this.brush = new SolidColorBrush(this.color.ToWpfColor());
        else
        {
          this.brush.Color = this.color.ToWpfColor();
        }
        this.dirty = false;
      }

#if DEBUG
      Color clr = this.color.ToWpfColor();
      SolidColorBrush brush1 = new SolidColorBrush(clr); //System.Drawing.Color.FromArgb(128, 128, 0, 0));
      Debug.Assert(this.brush.Color == brush1.Color);
#endif
      return this.brush;
    }
#endif

    bool dirty = true;
    bool immutable;
#if Gdip
    SolidBrush brush;
#endif
#if Wpf
    SolidColorBrush brush;
#endif
  }
}
