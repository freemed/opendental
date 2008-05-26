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
  /// Represents a 3-by-3 matrix that represents an affine 2D transformation.
  /// </summary>
  [DebuggerDisplay("({M11}, {M12}, {M21}, {M22}, {OffsetX}, {OffsetY})")]
  public struct XMatrix
  {
    // TODO: In Windows 6.0 the type System.Windows.Media.Matrix is a much more
    // sophisticated implementation of a matrix -> enhance this implementation

    // is struct now and must be initializes with Matrix.Identity
    //    /// <summary>
    //    /// Initializes a new instance of the Matrix class as the identity matrix.
    //    /// </summary>
    //    public XMatrix()
    //    {
    //      Reset();
    //    }

    static XMatrix()
    {
      XMatrix.identity = new XMatrix(1, 0, 0, 1, 0, 0);
    }

    ///// <summary>
    ///// Initializes a new instance of the Matrix class with the specified matrix.
    ///// </summary>
    //public XMatrix(Matrix matrix)
    //{
    //  float[] elements = matrix.Elements;
    //  this.m11 = elements[0];
    //  this.m12 = elements[1];
    //  this.m21 = elements[2];
    //  this.m22 = elements[3];
    //  this.mdx = elements[4];
    //  this.mdy = elements[5];
    //}

#if Gdip
    /// <summary>
    /// Initializes a new instance of the Matrix class to the transform defined by the specified rectangle and 
    /// array of points.
    /// </summary>
    public XMatrix(Rectangle rect, Point[] plgpts)
      : this(new XRect(rect.X, rect.Y, rect.Width, rect.Height),
      new XPoint[3] { new XPoint(plgpts[0]), new XPoint(plgpts[1]), new XPoint(plgpts[2]) })
    {
    }
#endif

#if Gdip
    /// <summary>
    /// Initializes a new instance of the Matrix class to the transform defined by the specified rectangle and 
    /// array of points.
    /// </summary>
    public XMatrix(RectangleF rect, PointF[] plgpts)
      : this(new XRect(rect.X, rect.Y, rect.Width, rect.Height),
      new XPoint[3] { new XPoint(plgpts[0]), new XPoint(plgpts[1]), new XPoint(plgpts[2]) })
    {
    }
#endif

#if Gdip
    /// <summary>
    /// Initializes a new instance of the <see cref="XMatrix"/> class.
    /// </summary>
    public XMatrix(XRect rect, XPoint[] plgpts)
    {
      // TODO
#if true
      // Lazy solution... left as an exercise :-)
      Matrix matrix = new Matrix(
        new RectangleF((float)rect.X, (float)rect.Y, (float)rect.Width, (float)rect.Height),
        new PointF[3]{new PointF((float)plgpts[0].X, (float)plgpts[0].Y),
                      new PointF((float)plgpts[1].X, (float)plgpts[1].Y), 
                      new PointF((float)plgpts[2].X, (float)plgpts[2].Y)});
      float[] elements = matrix.Elements;
      this.m11 = elements[0];
      this.m12 = elements[1];
      this.m21 = elements[2];
      this.m22 = elements[3];
      this.mdx = elements[4];
      this.mdy = elements[5];
#else
      // TODO work out the formulas for each value...
      this.m11 = 0;
      this.m12 = 0;
      this.m21 = 0;
      this.m22 = 0;
      this.mdx = 0;
      this.mdy = 0;
      throw new NotImplementedException("TODO");
#endif
    }
#endif

    /// <summary>
    /// Initializes a new instance of the Matrix class with the specified points.
    /// </summary>
    public XMatrix(double m11, double m12, double m21, double m22, double offsetX, double offsetY)
    {
      this.m11 = m11;
      this.m12 = m12;
      this.m21 = m21;
      this.m22 = m22;
      this.mdx = offsetX;
      this.mdy = offsetY;
    }

    /// <summary>
    /// Returns the hash code for this instance.
    /// </summary>
    public override int GetHashCode()
    {
      return base.GetHashCode();
    }

    /// <summary>
    /// Indicates whether this instance and a specified object are equal.
    /// </summary>
    public override bool Equals(object obj)
    {
      if (obj is XMatrix)
      {
        XMatrix matrix = (XMatrix)obj;
        return this.m11 == matrix.m11 && this.m12 == matrix.m12 && this.m21 == matrix.m21 &&
          this.m22 == matrix.m22 && this.mdx == matrix.mdx && this.mdy == matrix.mdy;
      }
      return false;
    }

    /// <summary>
    /// Inverts this XMatrix object. Throws an exception if the matrix is not invertible.
    /// </summary>
    public void Invert()
    {
      double det = this.m11 * this.m22 - this.m12 * this.m21;
      if (det == 0.0)
        throw new InvalidOperationException("Matrix is singular and cannot be inverted.");

      double i11 = this.m22 / det;
      double i12 = -this.m12 / det;
      double i21 = -this.m21 / det;
      double i22 = this.m11 / det;
      double idx = (this.m21 * this.mdy - this.m22 * this.mdx) / det;
      double idy = (this.m12 * this.mdx - this.m11 * this.mdy) / det;

      this.m11 = i11;
      this.m12 = i12;
      this.m21 = i21;
      this.m22 = i22;
      this.mdx = idx;
      this.mdy = idy;
    }

    /// <summary>
    /// Multiplies this matrix with the specified matrix.
    /// </summary>
    public void Multiply(XMatrix matrix)
    {
      Multiply(matrix, XMatrixOrder.Prepend);
    }

    /// <summary>
    /// Multiplies this matrix with the specified matrix.
    /// </summary>
    public void Multiply(XMatrix matrix, XMatrixOrder order)
    {
      double t11 = this.m11;
      double t12 = this.m12;
      double t21 = this.m21;
      double t22 = this.m22;
      double tdx = this.mdx;
      double tdy = this.mdy;

      if (order == XMatrixOrder.Append)
      {
        m11 = t11 * matrix.m11 + t12 * matrix.m21;
        m12 = t11 * matrix.m12 + t12 * matrix.m22;
        m21 = t21 * matrix.m11 + t22 * matrix.m21;
        m22 = t21 * matrix.m12 + t22 * matrix.m22;
        mdx = tdx * matrix.m11 + tdy * matrix.m21 + matrix.mdx;
        mdy = tdx * matrix.m12 + tdy * matrix.m22 + matrix.mdy;
      }
      else
      {
        m11 = t11 * matrix.m11 + t21 * matrix.m12;
        m12 = t12 * matrix.m11 + t22 * matrix.m12;
        m21 = t11 * matrix.m21 + t21 * matrix.m22;
        m22 = t12 * matrix.m21 + t22 * matrix.m22;
        mdx = t11 * matrix.mdx + t21 * matrix.mdy + tdx;
        mdy = t12 * matrix.mdx + t22 * matrix.mdy + tdy;
      }
    }

    /// <summary>
    /// Translates the matrix with the specified offsets.
    /// </summary>
    public void Translate(double offsetX, double offsetY)
    {
      // TODO: In Avalon the default is Append. What does that mean?
      Translate(offsetX, offsetY, XMatrixOrder.Prepend);
    }

    /// <summary>
    /// Translates the matrix with the specified offsets.
    /// </summary>
    public void Translate(double offsetX, double offsetY, XMatrixOrder order)
    {
      if (order == XMatrixOrder.Append)
      {
        this.mdx += offsetX;
        this.mdy += offsetY;
      }
      else
      {
        this.mdx += offsetX * this.m11 + offsetY * this.m21;
        this.mdy += offsetX * this.m12 + offsetY * this.m22;
      }
    }

    /// <summary>
    /// Scales the matrix with the specified scalars.
    /// </summary>
    public void Scale(double scaleX, double scaleY)
    {
      // TODO: In Avalon the default is Append
      Scale(scaleX, scaleY, XMatrixOrder.Prepend);
    }

    /// <summary>
    /// Scales the matrix with the specified scalars.
    /// </summary>
    public void Scale(double scaleX, double scaleY, XMatrixOrder order)
    {
      if (order == XMatrixOrder.Append)
      {
        this.m11 *= scaleX;
        this.m12 *= scaleY;
        this.m21 *= scaleX;
        this.m22 *= scaleY;
        this.mdx *= scaleX;
        this.mdy *= scaleY;
      }
      else
      {
        this.m11 *= scaleX;
        this.m12 *= scaleX;
        this.m21 *= scaleY;
        this.m22 *= scaleY;
      }
    }

    /// <summary>
    /// Scales the matrix with the specified scalar.
    /// </summary>
    public void Scale(double scaleXY)
    {
      // TODO: In Avalon the default is Append
      Scale(scaleXY, scaleXY, XMatrixOrder.Prepend);
    }

    /// <summary>
    /// Scales the matrix with the specified scalar.
    /// </summary>
    public void Scale(double scaleXY, XMatrixOrder order)
    {
      Scale(scaleXY, scaleXY, order);
    }

    /// <summary>
    /// Rotates the matrix with the specified angle.
    /// </summary>
    public void Rotate(double angle)
    {
      // TODO: In Avalon the default is Append
      Rotate(angle, XMatrixOrder.Prepend);
    }

    /// <summary>
    /// Rotates the matrix with the specified angle.
    /// </summary>
    public void Rotate(double angle, XMatrixOrder order)
    {
      angle = angle * Calc.Deg2Rad;
      double cos = Math.Cos(angle);
      double sin = Math.Sin(angle);
      if (order == XMatrixOrder.Append)
      {
        double t11 = this.m11;
        double t12 = this.m12;
        double t21 = this.m21;
        double t22 = this.m22;
        double tdx = this.mdx;
        double tdy = this.mdy;
        this.m11 = t11 * cos - t12 * sin;
        this.m12 = t11 * sin + t12 * cos;
        this.m21 = t21 * cos - t22 * sin;
        this.m22 = t21 * sin + t22 * cos;
        this.mdx = tdx * cos - tdy * sin;
        this.mdy = tdx * sin + tdy * cos;
      }
      else
      {
        double t11 = this.m11;
        double t12 = this.m12;
        double t21 = this.m21;
        double t22 = this.m22;
        this.m11 = t11 * cos + t21 * sin;
        this.m12 = t12 * cos + t22 * sin;
        this.m21 = -t11 * sin + t21 * cos;
        this.m22 = -t12 * sin + t22 * cos;
      }
    }

    /// <summary>
    /// Rotates the matrix with the specified angle at the specified point.
    /// </summary>
    public void RotateAt(double angle, XPoint point)
    {
      // TODO: In Avalon the default is Append
      RotateAt(angle, point, XMatrixOrder.Prepend);
    }

    /// <summary>
    /// Rotates the matrix with the specified angle at the specified point.
    /// </summary>
    public void RotateAt(double angle, XPoint point, XMatrixOrder order)
    {
      // TODO: check code
      if (order == XMatrixOrder.Prepend)
      {
        this.Translate(point.X, point.Y, order);
        this.Rotate(angle, order);
        this.Translate(-point.X, -point.Y, order);
      }
      else
      {
        throw new NotImplementedException("RotateAt with XMatrixOrder.Append");
      }
    }

    /// <summary>
    /// Shears the matrix with the specified scalars.
    /// </summary>
    public void Shear(double shearX, double shearY)
    {
      // TODO: In Avalon the default is Append
      Shear(shearX, shearY, XMatrixOrder.Prepend);
    }

    /// <summary>
    /// Shears the matrix with the specified scalars.
    /// </summary>
    public void Shear(double shearX, double shearY, XMatrixOrder order)
    {
      double t11 = this.m11;
      double t12 = this.m12;
      double t21 = this.m21;
      double t22 = this.m22;
      double tdx = this.mdx;
      double tdy = this.mdy;
      if (order == XMatrixOrder.Append)
      {
        this.m11 += shearX * t12;
        this.m12 += shearY * t11;
        this.m21 += shearX * t22;
        this.m22 += shearY * t21;
        this.mdx += shearX * tdy;
        this.mdy += shearY * tdx;
      }
      else
      {
        this.m11 += shearY * t21;
        this.m12 += shearY * t22;
        this.m21 += shearX * t11;
        this.m22 += shearX * t12;
      }
    }

    /// <summary>
    /// Multiplies all points of the specified array with the this matrix.
    /// </summary>
    public void TransformPoints(Point[] points)
    {
      if (points == null)
        throw new ArgumentNullException("points");

      if (IsIdentity)
        return;

      int count = points.Length;
      for (int idx = 0; idx < count; idx++)
      {
        double x = points[idx].X;
        double y = points[idx].Y;
        points[idx].X = (int)(x * this.m11 + y * this.m21 + this.mdx);
        points[idx].Y = (int)(x * this.m12 + y * this.m22 + this.mdy);
      }
    }

    /// <summary>
    /// Multiplies all points of the specified array with the this matrix.
    /// </summary>
    public void TransformPoints(XPoint[] points)
    {
      if (points == null)
        throw new ArgumentNullException("points");

      int count = points.Length;
      for (int idx = 0; idx < count; idx++)
      {
        double x = points[idx].X;
        double y = points[idx].Y;
        points[idx].X = x * this.m11 + y * this.m21 + this.mdx;
        points[idx].Y = x * this.m12 + y * this.m22 + this.mdy;
      }
    }

    /// <summary>
    /// Multiplies all vectors of the specified array with the this matrix. The translation elements 
    /// of this matrix (third row) are ignored.
    /// </summary>
    public void TransformVectors(XPoint[] points)
    {
      if (points == null)
        throw new ArgumentNullException("points");

      int count = points.Length;
      for (int idx = 0; idx < count; idx++)
      {
        double x = points[idx].X;
        double y = points[idx].Y;
        points[idx].X = x * this.m11 + y * this.m21;
        points[idx].Y = x * this.m12 + y * this.m22;
      }
    }

#if Gdip
    /// <summary>
    /// Multiplies all vectors of the specified array with the this matrix. The translation elements 
    /// of this matrix (third row) are ignored.
    /// </summary>
    public void TransformVectors(PointF[] points)
    {
      if (points == null)
        throw new ArgumentNullException("points");

      int count = points.Length;
      for (int idx = 0; idx < count; idx++)
      {
        double x = points[idx].X;
        double y = points[idx].Y;
        points[idx].X = (float)(x * this.m11 + y * this.m21 + this.mdx);
        points[idx].Y = (float)(x * this.m12 + y * this.m22 + this.mdy);
      }
    }
#endif

    /// <summary>
    /// Gets an array of double values that represents the elements of this matrix.
    /// </summary>
    public double[] Elements
    {
      get
      {
        double[] elements = new double[6];
        elements[0] = this.m11;
        elements[1] = this.m12;
        elements[2] = this.m21;
        elements[3] = this.m22;
        elements[4] = this.mdx;
        elements[5] = this.mdy;
        return elements;
      }
    }

    /// <summary>
    /// Gets a value from the matrix.
    /// </summary>
    public double M11
    {
      get { return this.m11; }
      set { this.m11 = value; }
    }

    /// <summary>
    /// Gets a value from the matrix.
    /// </summary>
    public double M12
    {
      get { return this.m12; }
      set { this.m12 = value; }
    }

    /// <summary>
    /// Gets a value from the matrix.
    /// </summary>
    public double M21
    {
      get { return this.m21; }
      set { this.m21 = value; }
    }

    /// <summary>
    /// Gets a value from the matrix.
    /// </summary>
    public double M22
    {
      get { return this.m22; }
      set { this.m22 = value; }
    }

    /// <summary>
    /// Gets the x translation value.
    /// </summary>
    public double OffsetX
    {
      get { return this.mdx; }
      set { this.mdx = value; }
    }

    /// <summary>
    /// Gets the y translation value.
    /// </summary>
    public double OffsetY
    {
      get { return this.mdy; }
      set { this.mdy = value; }
    }

    /// <summary>
    /// Converts this matrix to a System.Drawing.Drawing2D.Matrix object.
    /// </summary>
    public Matrix ToMatrix()
    {
      return new Matrix((float)this.m11, (float)this.m12, (float)this.m21, (float)this.m22,
        (float)this.mdx, (float)this.mdy);
    }

    /// <summary>
    /// Indicates whether this matrix is the identity matrix.
    /// </summary>
    public bool IsIdentity
    {
      get { return this.m11 == 1 && this.m12 == 0 && this.m21 == 0 && this.m22 == 1 && this.mdx == 0 && this.mdy == 0; }
    }

    /// <summary>
    /// Indicates whether this matrix is invertible, i. e. its determinant is not zero.
    /// </summary>
    public bool IsInvertible
    {
      get { return this.m11 * this.m22 - this.m12 * this.m21 != 0; }
    }

    /// <summary>
    /// Explicitly converts a XMatrix to an Matrix.
    /// </summary>
    public static explicit operator Matrix(XMatrix matrix)
    {
      return new Matrix(
        (float)matrix.m11, (float)matrix.m12,
        (float)matrix.m21, (float)matrix.m22,
        (float)matrix.mdx, (float)matrix.mdy);
    }

#if GdipUseGdiObjects
    /// <summary>
    /// Explicitly converts a Matrix to an XMatrix.
    /// </summary>
    public static implicit operator XMatrix(Matrix matrix)
    {
      float[] elements = matrix.Elements;
      return new XMatrix(elements[0], elements[1], elements[2], elements[3], elements[4], elements[5]);
    }
#endif

#if Wpf
    public static implicit operator XMatrix(Matrix matrix)
    {
      return new XMatrix(matrix.M11, matrix.M12, matrix.M21, matrix.M22, matrix.OffsetX, matrix.OffsetY);
    }
#endif

    /// <summary>
    /// Gets an identity matrix.
    /// </summary>
    public static XMatrix Identity
    {
      get { return XMatrix.identity; }
    }

    /// <summary>
    /// Determines whether to matrices are equal.
    /// </summary>
    public static bool operator ==(XMatrix matrix1, XMatrix matrix2)
    {
      return
        matrix1.m11 == matrix2.m11 &&
        matrix1.m12 == matrix2.m12 &&
        matrix1.m21 == matrix2.m21 &&
        matrix1.m22 == matrix2.m22 &&
        matrix1.mdx == matrix2.mdx &&
        matrix1.mdy == matrix2.mdy;
    }

    /// <summary>
    /// Determines whether to matrices are not equal.
    /// </summary>
    public static bool operator !=(XMatrix matrix1, XMatrix matrix2)
    {
      return !(matrix1 == matrix2);
    }

    double m11, m12, m21, m22, mdx, mdy;

    private static XMatrix identity;

#if DEBUG_
    /// <summary>
    /// Some test code to check that there are no typing errors in the formulars.
    /// </summary>
    public static void Test()
    {
      XMatrix xm1 = new XMatrix(23, -35, 837, 332, -3, 12);
      Matrix  m1 = new Matrix(23, -35, 837, 332, -3, 12);
      DumpMatrix(xm1, m1);
      XMatrix xm2 = new XMatrix(12, 235, 245, 42, 33, -56);
      Matrix  m2 = xm2.ToMatrix();
      DumpMatrix(xm2, m2);

//      xm1.Multiply(xm2, XMatrixOrder.Prepend);
//      m1.Multiply(m2, MatrixOrder.Append);
      xm1.Multiply(xm2, XMatrixOrder.Append);
      m1.Multiply(m2, MatrixOrder.Append);
      DumpMatrix(xm1, m1);

      xm1.Translate(-243, 342, XMatrixOrder.Append);
      m1.Translate(-243, 342, MatrixOrder.Append);
      DumpMatrix(xm1, m1);

      xm1.Scale(-5.66, 7.87);
      m1.Scale(-5.66f, 7.87f);
//      xm1.Scale(-5.66, 7.87, XMatrixOrder.Prepend);
//      m1.Scale(-5.66f, 7.87f, MatrixOrder.Prepend);
      DumpMatrix(xm1, m1);


      xm1.Rotate(135, XMatrixOrder.Append);
      m1.Rotate(135, MatrixOrder.Append);
      //      xm1.Scale(-5.66, 7.87, XMatrixOrder.Prepend);
      //      m1.Scale(-5.66f, 7.87f, MatrixOrder.Prepend);
      DumpMatrix(xm1, m1);

      xm1.RotateAt(177, new XPoint(-3456, 654), XMatrixOrder.Append);
      m1.RotateAt(177, new PointF(-3456, 654), MatrixOrder.Append);
      DumpMatrix(xm1, m1);

      xm1.Shear(0.76, -0.87, XMatrixOrder.Prepend);
      m1.Shear(0.76f, -0.87f, MatrixOrder.Prepend);
      DumpMatrix(xm1, m1);

      xm1 = new XMatrix(23, -35, 837, 332, -3, 12);
      m1 = new Matrix(23, -35, 837, 332, -3, 12);

      XPoint[] xpoints = new XPoint[3]{new XPoint(23, 10), new XPoint(-27, 120), new XPoint(-87, -55)};
      PointF[] points = new PointF[3]{new PointF(23, 10), new PointF(-27, 120), new PointF(-87, -55)};

      xm1.TransformPoints(xpoints);
      m1.TransformPoints(points);

      xm1.Invert();
      m1.Invert();
      DumpMatrix(xm1, m1);

    }

    static void DumpMatrix(XMatrix xm, Matrix m)
    {
      double[] xmv = xm.Elements;
      float[] mv = m.Elements;
      string message = String.Format("{0:0.###} {1:0.###} {2:0.###} {3:0.###} {4:0.###} {5:0.###}",
        xmv[0], xmv[1], xmv[2], xmv[3], xmv[4], xmv[5]);
      Console.WriteLine(message);
      message = String.Format("{0:0.###} {1:0.###} {2:0.###} {3:0.###} {4:0.###} {5:0.###}",
        mv[0], mv[1], mv[2], mv[3], mv[4], mv[5]);
      Console.WriteLine(message);
      Console.WriteLine();
    }
#endif
  }
}
