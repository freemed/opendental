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

// Comment out next line in Visual Studio 7.1
#pragma warning disable 1591

using System;
using System.ComponentModel;
#if Gdip
using System.Drawing;
#endif
#if Wpf
using System.Windows.Media;
#endif

namespace PdfSharp.Drawing
{
  /// <summary>
  /// Represents a two-dimensional vector specified by x- and y-coordinates.
  /// </summary>
  public struct XVector //TODO: IFormattable
  {
    /// <summary>
    /// Initializes a new instance of the XVector class with the specified coordinates.
    /// </summary>
    public XVector(double x, double y)
    {
      this.x = x;
      this.y = y;
    }

    public override bool Equals(object obj)
    {
      if (obj is XVector)
        return XVector.Equals(this, (XVector)obj);
      return false;
    }

    public static bool Equals(XVector vector1, XVector vector2)
    {
      return vector1.x == vector2.x && vector1.y == vector2.y;
    }

    public override int GetHashCode()
    {
      return this.x.GetHashCode() ^ this.y.GetHashCode();
    }

    /// <summary>
    /// Gets the x-coordinate of this XVector.
    /// </summary>
    public double X
    {
      get { return this.x; }
      set { this.x = value; }
    }

    /// <summary>
    /// Gets the y-coordinate of this XVector.
    /// </summary>
    public double Y
    {
      get { return this.y; }
      set { this.y = value; }
    }

    public double Length
    {
      get { return Math.Sqrt(this.x * this.x + this.y * this.y); }
    }

    public double LengthSquared
    {
      get { return this.x * this.x + this.y * this.y; }
    }

    public void Normalize()
    {
      this = this / Math.Max(Math.Abs(this.x), Math.Abs(this.y));
      this = this / Length;
    }

    public void Negate()
    {
      this.x = -this.x;
      this.y = -this.y;
    }

    //public static double Determinant(XVector vector1, XVector vector2);
    //public static XVector Parse(string source);
    //public static XVector Parse(string source, IFormatProvider formatProvider);
    //public override string ToString();
    //public string ToString(IFormatProvider provider);
    //string IFormattable.ToString(string format, IFormatProvider provider);
    //internal string ConvertToString(string format, IFormatProvider provider);
    //public static double CrossProduct(XVector vector1, XVector vector2);
    //public static double AngleBetween(XVector vector1, XVector vector2);

    public static XPoint Add(XVector vector, XPoint point)
    {
      return new XPoint(point.x + vector.x, point.y + vector.y);
    }

    public static XVector Add(XVector vector1, XVector vector2)
    {
      return new XVector(vector1.x + vector2.x, vector1.y + vector2.y);
    }

    public static XVector operator +(XVector vector1, XVector vector2)
    {
      return new XVector(vector1.x + vector2.x, vector1.y + vector2.y);
    }

    public static XPoint operator +(XVector vector, XPoint point)
    {
      return new XPoint(point.x + vector.x, point.y + vector.y);
    }

    public static XVector Subtract(XVector vector1, XVector vector2)
    {
      return new XVector(vector1.x - vector2.x, vector1.y - vector2.y);
    }

    public static XVector operator -(XVector vector)
    {
      return new XVector(-vector.x, -vector.y);
    }

    public static XVector operator -(XVector vector1, XVector vector2)
    {
      return new XVector(vector1.x - vector2.x, vector1.y - vector2.y);
    }

    public static double Multiply(XVector vector1, XVector vector2)
    {
      return vector1.x * vector2.x + vector1.y * vector2.y;
    }

    public static XVector Multiply(XVector vector, double scalar)
    {
      return new XVector(vector.x * scalar, vector.y * scalar);
    }

    public static XVector Multiply(double scalar, XVector vector)
    {
      return new XVector(vector.x * scalar, vector.y * scalar);
    }

    //public static XVector Multiply(XVector vector, XMatrix matrix)
    //{
    //  return matrix.TransformVector(vector);
    //}


    public static XVector operator *(XVector vector, double scalar)
    {
      return new XVector(vector.x * scalar, vector.y * scalar);
    }

    public static XVector operator *(double scalar, XVector vector)
    {
      return new XVector(vector.x * scalar, vector.y * scalar);
    }

    //public static XVector operator *(XVector vector, XMatrix matrix)
    //{
    //  return matrix.TransformVector(vector);
    //
    //}

    public static double operator *(XVector vector1, XVector vector2)
    {
      return vector1.x * vector2.x + vector1.y * vector2.y;
    }

    public static XVector Divide(XVector vector, double scalar)
    {
      return vector * (1 / scalar);
    }

    public static XVector operator /(XVector vector, double scalar)
    {
      return vector * (1 / scalar);
    }

    public static explicit operator XSize(XVector vector)
    {
      return new XSize(Math.Abs(vector.x), Math.Abs(vector.y));
    }

    public static explicit operator XPoint(XVector vector)
    {
      return new XPoint(vector.x, vector.y);
    }

    /// <summary>
    /// Converts this XVector to a human readable string.
    /// </summary>
    public override string ToString()
    {
      return string.Format("{{X={0}, Y={1}}}", this.x, this.y);
    }

    ///// <summary>
    ///// Converts this XVector to a PoinF.
    ///// </summary>
    //public PointF ToPointF()
    //{
    //  return new PointF((float)this.x, (float)this.y);
    //}

    public static bool operator ==(XVector left, XVector right)
    {
      return left.x == right.x && left.y == right.y;
    }

    public static bool operator !=(XVector left, XVector right)
    {
      return !(left == right);
    }

    internal double x;
    internal double y;
  }
}
