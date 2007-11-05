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
using System.Globalization;
#if Gdip
using System.Drawing;
#endif
#if Wpf
using System.Windows;
#endif

namespace PdfSharp.Drawing
{
  /// <summary>
  /// Represents a pair of floating point x- and y-coordinates that defines a point
  /// in a two-dimensional plane.
  /// </summary>
  [DebuggerDisplay("({X}, {Y})")]
  public struct XPoint
  {
    /// <summary>
    /// Initializes a new instance of the XPoint class with the specified coordinates.
    /// </summary>
    public XPoint(double x, double y)
    {
      this.x = x;
      this.y = y;
    }

    /// <summary>
    /// Initializes a new instance of the XPoint class with the specified point.
    /// </summary>
    public XPoint(Point point)
    {
      this.x = point.X;
      this.y = point.Y;
    }

#if Gdip
    /// <summary>
    /// Initializes a new instance of the XPoint class with the specified point.
    /// </summary>
    public XPoint(PointF point)
    {
      this.x = point.X;
      this.y = point.Y;
    }
#endif

    /// <summary>
    /// Indicates whether this instance and a specified object are equal.
    /// </summary>
    public override bool Equals(object obj)
    {
      XPoint point = (XPoint)obj;
      if (obj != null)
        return point.x == this.x && point.y == this.y;
      return false;
    }

    /// <summary>
    /// Returns the hash code for this instance.
    /// </summary>
    public override int GetHashCode()
    {
      return this.x.GetHashCode() ^ this.y.GetHashCode();
    }

    /// <summary>
    /// Converts this XPoint to a human readable string.
    /// </summary>
    public override string ToString()
    {
      return String.Format("{{X={0}, Y={1}}}", this.x, this.y);
    }

#if Gdip
    /// <summary>
    /// Converts this XPoint to a PoinF.
    /// </summary>
    public PointF ToPointF()
    {
      return new PointF((float)this.x, (float)this.y);
    }
#endif

    /// <summary>
    /// Gets the x-coordinate of this XPoint.
    /// </summary>
    public double X
    {
      get { return this.x; }
      set { this.x = value; }
    }

    /// <summary>
    /// Gets the y-coordinate of this XPoint.
    /// </summary>
    public double Y
    {
      get { return this.y; }
      set { this.y = value; }
    }

    /// <summary>
    /// Indicates whether this XPoint is empty.
    /// </summary>
    [Browsable(false)]
    public bool IsEmpty
    {
      get { return this.x == 0 && this.y == 0; }
    }

    /// <summary>
    /// Add a point and a size.
    /// </summary>
    public static XPoint operator +(XPoint point, XSize sz)
    {
      return new XPoint(point.x + sz.width, point.y + sz.height);
    }

    /// <summary>
    /// Add a size from a point.
    /// </summary>
    public static XPoint operator -(XPoint point, XSize sz)
    {
      return new XPoint(point.x - sz.width, point.y - sz.height);
    }

    /// <summary>
    /// Multiplies a point with a scalar value.
    /// </summary>
    public static XPoint operator *(XPoint point, double f)
    {
      return new XPoint(point.x * f, point.y * f);
    }

    /// <summary>
    /// Multiplies a point with a scalar value.
    /// </summary>
    public static XPoint operator *(double f, XPoint point)
    {
      return new XPoint(f * point.x, f * point.y);
    }

    /// <summary>
    /// Divides a point by a scalar value.
    /// </summary>
    public static XPoint operator /(XPoint point, double f)
    {
      if (f == 0)
        throw new DivideByZeroException("Divisor is zero.");

      return new XPoint(point.x / f, point.y / f);
    }

    /// <summary>
    /// Determines whether two points are equal.
    /// </summary>
    public static bool operator ==(XPoint left, XPoint right)
    {
      return left.x == right.x && left.y == right.y;
    }

    /// <summary>
    /// Determines whether two points are not equal.
    /// </summary>
    public static bool operator !=(XPoint left, XPoint right)
    {
      return !(left == right);
    }

    /// <summary>
    /// Offsets the x and y value of this point.
    /// </summary>
    internal void Offset(double dx, double dy)
    {
      this.X += dx;
      this.Y += dy;
    }

    internal double x;
    internal double y;

    /// <summary>
    /// Parses the point from a string.
    /// </summary>
    public static XPoint ParsePoint(string value)
    {
      if (value == null)
        throw new ArgumentNullException("value");

      // TODO: Reflect reliabel implementation from Avalon
      int ich = value.IndexOf(',');
      if (ich == -1)
        throw new ArgumentException("Invalid value.", "value");

      double x = double.Parse(value.Substring(0, ich), CultureInfo.InvariantCulture);
      double y = double.Parse(value.Substring(ich + 1), CultureInfo.InvariantCulture);
      return new XPoint(x, y);
    }

    /// <summary>
    /// Parses an array of points from a string.
    /// </summary>
    public static XPoint[] ParsePoints(string value)
    {
      if (value == null)
        throw new ArgumentNullException("value");

      // TODO: Reflect reliabel implementation from Avalon
      string[] values = value.Split(' ');
      int count = values.Length;
      XPoint[] points = new XPoint[count];
      for (int idx = 0; idx < count; idx++)
        points[idx] = ParsePoint(values[idx]);
      return points;
    }

    /// <summary>
    /// Represents a new instance of the XPoint class with member data left uninitialized.
    /// </summary>
    public static readonly XPoint Empty = new XPoint();
  }
}
