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
using System.ComponentModel;
#if Gdip
using System.Drawing;
#endif
#if Wpf
using System.Windows;
#endif

namespace PdfSharp.Drawing
{
  /// <summary>
  /// Represents a pair of floating-point numbers, typically the width and height of a
  /// graphical object.
  /// </summary>
  [DebuggerDisplay("({Width}, {Height})")]
  public struct XSize
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="XSize"/> class.
    /// </summary>
    public XSize(XSize size)
    {
      this.width = size.width;
      this.height = size.height;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="XSize"/> class.
    /// </summary>
    /// <param name="pt">The pt.</param>
    public XSize(XPoint pt)
    {
      this.width = pt.X;
      this.height = pt.Y;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="XSize"/> class.
    /// </summary>
    /// <param name="width">The width.</param>
    /// <param name="height">The height.</param>
    public XSize(double width, double height)
    {
      this.width = width;
      this.height = height;
    }

    /// <summary>
    /// Returns the hash code for this instance.
    /// </summary>
    public override int GetHashCode()
    {
      return this.width.GetHashCode() ^ this.height.GetHashCode();
    }

    /// <summary>
    /// Indicates whether this instance and a specified object are equal.
    /// </summary>
    public override bool Equals(object obj)
    {
      if (obj is XSize)
      {
        XSize size = (XSize)obj;
        return size.width == this.width && size.height == this.height;
      }
      return false;
    }

#if Gdip
    /// <summary>
    /// Creates an XSize from a System.Drawing.Size.
    /// </summary>
    public static XSize FromSize(Size size)
    {
      return new XSize(size.Width, size.Height);
    }

    /// <summary>
    /// Creates an XSize from a System.Drawing.Size.
    /// </summary>
    public static XSize FromSizeF(SizeF size)
    {
      return new XSize(size.Width, size.Height);
    }
#endif

    /// <summary>
    /// Returns a string with the values of this instance.
    /// </summary>
    public override string ToString()
    {
      return string.Format("{{Width={0}, Height={1}}}", this.width, this.height);
    }

#if Gdip
    /// <summary>
    /// Converts this XSize to a PointF.
    /// </summary>
    public PointF ToPointF()
    {
      return new PointF((float)this.width, (float)this.height);
    }
#endif

    /// <summary>
    /// Converts this XSize to an XPoint.
    /// </summary>
    public XPoint ToXPoint()
    {
      return new XPoint(this.width, this.height);
    }

#if Gdip
    /// <summary>
    /// Converts this XSize to a SizeF.
    /// </summary>
    public SizeF ToSizeF()
    {
      return new SizeF((float)this.width, (float)this.height);
    }
#endif

    /// <summary>
    /// Gets a value indicating whether this instance is empty.
    /// </summary>
    [Browsable(false)]
    public bool IsEmpty
    {
      get { return this.width == 0 && this.height == 0; }
    }

    /// <summary>
    /// Gets or sets the width.
    /// </summary>
    public double Width
    {
      get { return this.width; }
      set { this.width = value; }
    }

    /// <summary>
    /// Gets or sets the height.
    /// </summary>
    public double Height
    {
      get { return this.height; }
      set { this.height = value; }
    }

    /// <summary>
    /// Adds two size objects.
    /// </summary>
    public static XSize operator +(XSize size1, XSize size2)
    {
      return new XSize(size1.width + size2.width, size1.height + size2.height);
    }

    /// <summary>
    /// Subtracts two size objects.
    /// </summary>
    public static XSize operator -(XSize size1, XSize size2)
    {
      return new XSize(size1.width - size2.width, size1.height - size2.height);
    }

    /// <summary>
    /// Multiplies a size with a scalar.
    /// </summary>
    public static XSize operator *(XSize size, double f)
    {
      return new XSize(size.width * f, size.height * f);
    }

    /// <summary>
    /// Multiplies a scalar with a size.
    /// </summary>
    public static XSize operator *(double f, XSize size)
    {
      return new XSize(f * size.width, f * size.height);
    }

    /// <summary>
    /// Divides a size by a scalar.
    /// </summary>
    public static XSize operator /(XSize size, double f)
    {
      if (f == 0)
        throw new DivideByZeroException("Divisor is zero.");

      return new XSize(size.width / f, size.height / f);
    }

    /// <summary>
    /// Determines whether two size objects are equal.
    /// </summary>
    public static bool operator ==(XSize left, XSize right)
    {
      return left.width == right.width && left.height == right.height;
    }

    /// <summary>
    /// Determines whether two size objects are not equal.
    /// </summary>
    public static bool operator !=(XSize left, XSize right)
    {
      return !(left == right);
    }

    /// <summary>
    /// Explicit conversion from XSize to XPoint.
    /// </summary>
    public static explicit operator XPoint(XSize size)
    {
      return new XPoint(size.width, size.height);
    }

    /// <summary>
    /// Represents the empty size.
    /// </summary>
    public static readonly XSize Empty = new XSize();

    internal double width;
    internal double height;
  }
}
