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
using System.Windows;
using System.Windows.Media;
#endif
using PdfSharp.Internal;

namespace PdfSharp.Drawing
{
  /// <summary>
  /// Defines an object used to draw lines and curves.
  /// </summary>
  public sealed class XPen
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="XPen"/> class.
    /// </summary>
    public XPen(XColor color)
      : this(color, 1, false)
    { }

    /// <summary>
    /// Initializes a new instance of the <see cref="XPen"/> class.
    /// </summary>
    public XPen(XColor color, double width)
      : this(color, width, false)
    { }

    internal XPen(XColor color, double width, bool immutable)
    {
      this.color = color;
      this.width = width;
      this.lineJoin = XLineJoin.Miter;
      this.lineCap = XLineCap.Flat;
      this.dashStyle = XDashStyle.Solid;
      this.dashOffset = 0f;
      this.immutable = immutable;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="XPen"/> class.
    /// </summary>
    public XPen(XPen pen)
    {
      this.color = pen.color;
      this.width = pen.width;
      this.lineJoin = pen.lineJoin;
      this.lineCap = pen.lineCap;
      this.dashStyle = pen.dashStyle;
      this.dashOffset = pen.dashOffset;
      this.dashPattern = pen.dashPattern;
      if (this.dashPattern != null)
        this.dashPattern = (double[])this.dashPattern.Clone();
    }

    /// <summary>
    /// Clones this instance.
    /// </summary>
    public XPen Clone()
    {
      return new XPen(this);
    }

    /// <summary>
    /// Gets or sets the color.
    /// </summary>
    public XColor Color
    {
      get { return this.color; }
      set
      {
        if (this.immutable)
          throw new ArgumentException(PSSR.CannotChangeImmutableObject("XPen"));
        this.dirty = this.dirty || this.color != value;
        this.color = value;
      }
    }
    internal XColor color;

    /// <summary>
    /// Gets or sets the width.
    /// </summary>
    public double Width
    {
      get { return this.width; }
      set
      {
        if (this.immutable)
          throw new ArgumentException(PSSR.CannotChangeImmutableObject("XPen"));
        this.dirty = this.dirty || this.width != value;
        this.width = value;
      }
    }
    internal double width;

    /// <summary>
    /// Gets or sets the line join.
    /// </summary>
    public XLineJoin LineJoin
    {
      get { return this.lineJoin; }
      set
      {
        if (this.immutable)
          throw new ArgumentException(PSSR.CannotChangeImmutableObject("XPen"));
        this.dirty = this.dirty || this.lineJoin != value;
        this.lineJoin = value;
      }
    }
    internal XLineJoin lineJoin;

    /// <summary>
    /// Gets or sets the line cap.
    /// </summary>
    public XLineCap LineCap
    {
      get { return this.lineCap; }
      set
      {
        if (this.immutable)
          throw new ArgumentException(PSSR.CannotChangeImmutableObject("XPen"));
        this.dirty = this.dirty || this.lineCap != value;
        this.lineCap = value;
      }
    }
    internal XLineCap lineCap;

    /// <summary>
    /// Gets or sets the miter limit.
    /// </summary>
    public double MiterLimit
    {
      get { return this.miterLimit; }
      set
      {
        if (this.immutable)
          throw new ArgumentException(PSSR.CannotChangeImmutableObject("XPen"));
        this.dirty = this.dirty || this.miterLimit != value;
        this.miterLimit = value;
      }
    }
    internal double miterLimit;

    /// <summary>
    /// Gets or sets the dash style.
    /// </summary>
    public XDashStyle DashStyle
    {
      get { return this.dashStyle; }
      set
      {
        if (this.immutable)
          throw new ArgumentException(PSSR.CannotChangeImmutableObject("XPen"));
        this.dirty = this.dirty || this.dashStyle != value;
        this.dashStyle = value;
      }
    }
    internal XDashStyle dashStyle;

    /// <summary>
    /// Gets or sets the dash offset.
    /// </summary>
    public double DashOffset
    {
      get { return this.dashOffset; }
      set
      {
        if (this.immutable)
          throw new ArgumentException(PSSR.CannotChangeImmutableObject("XPen"));
        this.dirty = this.dirty || this.dashOffset != value;
        this.dashOffset = value;
      }
    }
    internal double dashOffset;

    /// <summary>
    /// Gets or sets the dash pattern.
    /// </summary>
    public double[] DashPattern
    {
      get
      {
        if (this.dashPattern == null)
          this.dashPattern = new double[0];
        return this.dashPattern;
      }
      set
      {
        if (this.immutable)
          throw new ArgumentException(PSSR.CannotChangeImmutableObject("XPen"));

        int length = value.Length;
        //if (length == 0)
        //  throw new ArgumentException("Dash pattern array must not be empty.");

        for (int idx = 0; idx < length; idx++)
        {
          if (value[idx] <= 0)
            throw new ArgumentException("Dash pattern value must greater than zero.");
        }

        this.dirty = true;
        this.dashStyle = XDashStyle.Custom;
        this.dashPattern = (double[])value.Clone();
      }
    }
    internal double[] dashPattern;

#if Gdip
#if GdipUseGdiObjects
    /// <summary>
    /// Implicit convertion from Pen to XPen
    /// </summary>
    public static implicit operator XPen(Pen pen)
    {
      XPen xpen;
      switch (pen.PenType)
      {
        case PenType.SolidColor:
          xpen = new XPen(pen.Color, pen.Width);
          xpen.LineJoin = (XLineJoin)pen.LineJoin;
          xpen.DashStyle = (XDashStyle)pen.DashStyle;
          xpen.miterLimit = pen.MiterLimit;
          break;

        default:
          throw new NotImplementedException("Pen type not supported by PDFsharp.");
      }
      // Bug fixed by drice2@ageone.de
      if (pen.DashStyle == System.Drawing.Drawing2D.DashStyle.Custom)
      {
        int length = pen.DashPattern.Length;
        double[] pattern = new double[length];
        for (int idx = 0; idx < length; idx++)
          pattern[idx] = pen.DashPattern[idx];
        xpen.DashPattern = pattern;
        xpen.dashOffset = pen.DashOffset;
      }
      return xpen;
    }
#endif

    internal Pen RealizeGdiPen()
    {
      if (this.dirty)
      {
        if (this.pen == null)
          this.pen = new Pen(this.color.ToGdiColor(), (float)this.width);
        else
        {
          this.pen.Color = this.color.ToGdiColor();
          this.pen.Width = (float)this.width;
        }
        LineCap lineCap = XConvert.ToLineCap(this.lineCap);
        this.pen.StartCap = lineCap;
        this.pen.EndCap = lineCap;
        this.pen.LineJoin = XConvert.ToLineJoin(this.lineJoin);
        this.pen.DashOffset = (float)this.dashOffset;
        if (this.dashStyle == XDashStyle.Custom)
        {
          int len = this.dashPattern == null ? 0 : this.dashPattern.Length;
          float[] pattern = new float[len];
          for (int idx = 0; idx < len; idx++)
            pattern[idx] = (float)this.dashPattern[idx];
          this.pen.DashPattern = pattern;
        }
        else
          this.pen.DashStyle = (DashStyle)this.dashStyle;
      }
      return this.pen;
    }
#endif

#if Wpf
    internal Pen RealizeWpfPen()
    {
      if (this.dirty)
      {
        if (this.pen == null)
          this.pen = new Pen(new SolidColorBrush(this.color.ToWpfColor()), this.width);
        else
        {
          this.pen.Brush = new SolidColorBrush(this.color.ToWpfColor());
          this.pen.Thickness = this.width;
        }
        // TODOFX
        //LineCap lineCap = XConvert.ToLineCap(this.lineCap);
        //this.pen.StartCap = lineCap;
        //this.pen.EndCap = lineCap;
        //this.pen.LineJoin = XConvert.ToLineJoin(this.lineJoin);
        //this.pen.DashOffset = (float)this.dashOffset;
        //if (this.dashStyle == XDashStyle.Custom)
        //{
        //  int len = this.dashPattern == null ? 0 : this.dashPattern.Length;
        //  float[] pattern = new float[len];
        //  for (int idx = 0; idx < len; idx++)
        //    pattern[idx] = (float)this.dashPattern[idx];
        //  this.pen.DashPattern = pattern;
        //}
        //else
        //  this.pen.DashStyle = (DashStyle)this.dashStyle;
      }
      return this.pen;
    }
#endif

    bool dirty = true;
    bool immutable;
    Pen pen;
  }
}
