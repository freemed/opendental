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
using System.Windows.Media;
#endif

namespace PdfSharp.Drawing
{
  /// <summary>
  /// Stores a set of four floating-point numbers that represent the location and size of a rectangle.
  /// </summary>
  [DebuggerDisplay("({X}, {Y}, {Width}, {Height})")]
  public struct XRect
  {
    // Called XRect and not XRectangle because XRectangle will get the name of a shape object
    // in a forthcoming extension.

    /// <summary>
    /// Initializes a new instance of the XRect class.
    /// </summary>
    public XRect(double x, double y, double width, double height)
    {
      this.x = x;
      this.y = y;
      this.width = width;
      this.height = height;
    }

    /// <summary>
    /// Initializes a new instance of the XRect class.
    /// </summary>
    public XRect(XPoint location, XSize size)
    {
      this.x = location.X;
      this.y = location.Y;
      this.width = size.Width;
      this.height = size.Height;
    }

#if Gdip
    /// <summary>
    /// Initializes a new instance of the XRect class.
    /// </summary>
    public XRect(PointF location, SizeF size)
    {
      this.x = location.X;
      this.y = location.Y;
      this.width = size.Width;
      this.height = size.Height;
    }
#endif

#if Gdip
    /// <summary>
    /// Initializes a new instance of the XRect class.
    /// </summary>
    public XRect(RectangleF rect)
    {
      this.x = rect.X;
      this.y = rect.Y;
      this.width = rect.Width;
      this.height = rect.Height;
    }
#endif

#if Wpf
    /// <summary>
    /// Initializes a new instance of the XRect class.
    /// </summary>
    public XRect(Rect rect)
    {
      this.x = rect.X;
      this.y = rect.Y;
      this.width = rect.Width;
      this.height = rect.Height;
    }
#endif

    /// <summary>
    /// Creates a rectangle from for straight lines.
    /// </summary>
    public static XRect FromLTRB(double left, double top, double right, double bottom)
    {
      return new XRect(left, top, right - left, bottom - top);
    }

    /// <summary>
    /// Returns the hash code for this instance.
    /// </summary>
    public override int GetHashCode()
    {
      // Lutz Roeder's .NET Reflector proudly presents:
      //   »THE ART OF HASH CODE PROGRAMMING«
      //
      // .NET 1.1:
      //   return (int) (((((uint) this.X) ^ ((((uint) this.Y) << 13) | (((uint) this.Y) >> 0x13))) ^ ((((uint) this.Width) << 0x1a) | (((uint) this.Width) >> 6))) ^ ((((uint) this.Height) << 7) | (((uint) this.Height) >> 0x19)));
      // Mono:
      //   return (int) (x + y + width + height);
      return (int)(x + y + width + height);
    }

    /// <summary>
    /// Indicates whether this instance and a specified object are equal.
    /// </summary>
    public override bool Equals(object obj)
    {
      if (obj is XRect)
      {
        XRect rect = (XRect)obj;
        return rect.x == this.x && rect.y == this.y && rect.width == this.width && rect.height == this.height;
      }
      return false;
    }

    /// <summary>
    /// Returns a string with the values of this rectangle.
    /// </summary>
    public override string ToString()
    {
      return String.Format("{{X={0},Y={1},Width={2},Height={3}}}", this.x, this.y, this.width, this.height);
    }

#if Gdip
    /// <summary>
    /// Converts this instance to a System.Drawing.RectangleF.
    /// </summary>
    public RectangleF ToRectangleF()
    {
      return new RectangleF((float)this.x, (float)this.y, (float)this.width, (float)this.height);
    }
#endif

    /// <summary>
    /// Gets a value indicating whether this rectangle is empty.
    /// </summary>
    [Browsable(false)]
    public bool IsEmpty
    {
      // The .NET documentation differs from the actual implemention, which differs from the Mono 
      // implementation. This is my recommendation what an empty rectangle means:
      get { return this.width <= 0.0 || this.height <= 0.0; }
    }

    /// <summary>
    /// Gets or sets the location of the rectangle.
    /// </summary>
    [Browsable(false)]
    public XPoint Location
    {
      get { return new XPoint(this.x, this.y); }
      set { this.x = value.X; this.y = value.Y; }
    }

    /// <summary>
    /// Gets or sets the size of the rectangle.
    /// </summary>
    [Browsable(false)]
    public XSize Size
    {
      get { return new XSize(this.width, this.height); }
      set { this.width = value.Width; this.height = value.Height; }
    }

    /// <summary>
    /// Gets or sets the X value.
    /// </summary>
    public double X
    {
      get { return this.x; }
      set { this.x = value; }
    }

    /// <summary>
    /// Gets or sets the Y value.
    /// </summary>
    public double Y
    {
      get { return this.y; }
      set { this.y = value; }
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
    /// Gets the left.
    /// </summary>
    [Browsable(false)]
    public double Left
    {
      get { return this.x; }
    }

    /// <summary>
    /// Gets the top.
    /// </summary>
    [Browsable(false)]
    public double Top
    {
      get { return this.y; }
    }

    /// <summary>
    /// Gets the right.
    /// </summary>
    [Browsable(false)]
    public double Right
    {
      get { return this.x + this.width; }
    }

    /// <summary>
    /// Gets the bottom.
    /// </summary>
    [Browsable(false)]
    public double Bottom
    {
      get { return this.y + this.height; }
    }

    /// <summary>
    /// Gets the center of the rectangle.
    /// </summary>
    [Browsable(false)]
    public XPoint Center
    {
      get { return new XPoint(this.x + this.width / 2, this.y + this.height / 2); }
    }

    /// <summary>
    /// Determines whether the rectangle contains the specified point.
    /// </summary>
    public bool Contains(XPoint pt)
    {
      return Contains(pt.X, pt.Y);
    }

    /// <summary>
    /// Determines whether the rectangle contains the specified point.
    /// </summary>
    public bool Contains(double x, double y)
    {
      return this.x <= x && x < this.x + this.width && this.y <= y && y < this.y + this.height;
    }

    /// <summary>
    /// Determines whether the rectangle completely contains the specified rectangle.
    /// </summary>
    public bool Contains(XRect rect)
    {
      return this.x <= rect.x && rect.x + rect.width <= this.x + this.width &&
             this.y <= rect.y && rect.y + rect.height <= this.y + this.height;
    }

    /// <summary>
    /// Inflates the rectangle by the specified size.
    /// </summary>
    public void Inflate(XSize size)
    {
      Inflate(size.Width, size.Height);
    }

    /// <summary>
    /// Inflates the rectangle by the specified size.
    /// </summary>
    public void Inflate(double x, double y)
    {
      this.x -= x;
      this.y -= y;
      this.width += x * 2;
      this.height += y * 2;
    }

    /// <summary>
    /// Inflates the rectangle by the specified size.
    /// </summary>
    public static XRect Inflate(XRect rect, double x, double y)
    {
      rect.Inflate(x, y);
      return rect;
    }

    /// <summary>
    /// Intersects the rectangle with the specified rectangle.
    /// </summary>
    public void Intersect(XRect rect)
    {
      rect = XRect.Intersect(rect, this);
      this.x = rect.x;
      this.y = rect.y;
      this.width = rect.width;
      this.height = rect.height;
    }

    /// <summary>
    /// Intersects the specified rectangles.
    /// </summary>
    public static XRect Intersect(XRect left, XRect right)
    {
      double l = Math.Max(left.x, right.x);
      double r = Math.Min(left.x + left.width, right.x + right.width);
      double t = Math.Max(left.y, right.y);
      double b = Math.Min(left.y + left.height, right.y + right.height);
      if ((r >= l) && (b >= t))
        return new XRect(l, t, r - l, b - t);
      return XRect.Empty;
    }

    /// <summary>
    /// Determines whether the rectangle intersects with the specified rectangle.
    /// </summary>
    public bool IntersectsWith(XRect rect)
    {
      return rect.x < this.x + this.width && this.x < rect.x + rect.width &&
        rect.y < this.y + this.height && this.y < rect.y + rect.height;
    }

    /// <summary>
    /// Unites the specified rectangles.
    /// </summary>
    public static XRect Union(XRect left, XRect right)
    {
      double l = Math.Min(left.X, right.X);
      double r = Math.Max(left.X + left.Width, right.X + right.Width);
      double t = Math.Min(left.Y, right.Y);
      double b = Math.Max(left.Y + left.Height, right.Y + right.Height);
      return new XRect(l, t, r - l, b - t);
    }

    /// <summary>
    /// Translates the rectangle by the specifed offset.
    /// </summary>
    public void Offset(XPoint pt)
    {
      Offset(pt.X, pt.Y);
    }

    /// <summary>
    /// Translates the rectangle by the specifed offset.
    /// </summary>
    public void Offset(double x, double y)
    {
      this.x += x;
      this.y += y;
    }

    /// <summary>
    /// Translates the rectangle by adding the specifed point.
    /// </summary>
    public static XRect operator +(XRect rect, XPoint point)
    {
      return new XRect(rect.x + point.x, rect.Y + point.y, rect.width, rect.height);
    }

    /// <summary>
    /// Translates the rectangle by subtracting the specifed point.
    /// </summary>
    public static XRect operator -(XRect rect, XPoint point)
    {
      return new XRect(rect.x - point.x, rect.Y - point.y, rect.width, rect.height);
    }

#if Gdip
    /// <summary>
    /// Implicit conversion from a System.Drawing.Rectangle to an XRect.
    /// </summary>
    public static implicit operator XRect(Rectangle rect)
    {
      return new XRect(rect.X, rect.Y, rect.Width, rect.Height);
    }

    /// <summary>
    /// Implicit conversion from a System.Drawing.RectangleF to an XRect.
    /// </summary>
    public static implicit operator XRect(RectangleF rect)
    {
      return new XRect(rect.X, rect.Y, rect.Width, rect.Height);
    }
#endif

#if Wpf
    public static implicit operator XRect(Rect rect)
    {
      return new XRect(rect.X, rect.Y, rect.Width, rect.Height);
    }
#endif

    /// <summary>
    /// Determines whether the two rectangles are equal.
    /// </summary>
    public static bool operator ==(XRect left, XRect right)
    {
      return left.x == right.x && left.y == right.y && left.width == right.width && left.height == right.height;
    }

    /// <summary>
    /// Determines whether the two rectangles not are equal.
    /// </summary>
    public static bool operator !=(XRect left, XRect right)
    {
      return !(left == right);
    }

    /// <summary>
    /// Represents the empty rectangle.
    /// </summary>
    public static readonly XRect Empty = new XRect();

    internal double x;
    internal double y;
    internal double width;
    internal double height;
  }
}
