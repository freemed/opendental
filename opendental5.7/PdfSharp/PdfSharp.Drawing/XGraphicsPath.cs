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
using System.Globalization;
using System.Text;
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
  /// Represents a series of connected lines and curves.
  /// </summary>
  public sealed class XGraphicsPath
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="XGraphicsPath"/> class.
    /// </summary>
    public XGraphicsPath()
    {
      this.dirty.GetType();
      this.items = new ArrayList();
      this.gdipPath = new GraphicsPath();
    }

#if Gdip
    /// <summary>
    /// Initializes a new instance of the <see cref="XGraphicsPath"/> class.
    /// </summary>
    public XGraphicsPath(PointF[] points, byte[] types, XFillMode fillMode)
    {
      this.gdipPath = new GraphicsPath(points, types, (FillMode)fillMode);
    }
#endif

#if Gdip
    /// <summary>
    /// Gets access to underlying GDI+ path.
    /// </summary>
    internal GraphicsPath gdipPath;
#endif

    /// <summary>
    /// Clones this instance.
    /// </summary>
    public XGraphicsPath Clone()
    {
      XGraphicsPath path = (XGraphicsPath)MemberwiseClone();
      if (path.items != null)
      {
        int count = path.items.Count;
        for (int idx = 0; idx < count; idx++)
        {
          path.items[idx] = ((XGraphicsPathItem)path.items[idx]).Clone();
        }
      }
      path.gdipPath = this.gdipPath.Clone() as GraphicsPath;
      return path;
    }

    /// <summary>
    /// For internal use only.
    /// </summary>
    internal XGraphicsPathItem[] GetPathData()
    {
      int count = this.items.Count;
      XGraphicsPathItem[] data = new XGraphicsPathItem[count];
      for (int idx = 0; idx < count; idx++)
        data[idx] = ((XGraphicsPathItem)this.items[idx]).Clone() as XGraphicsPathItem;
      return data;
    }

    // ----- AddLine ------------------------------------------------------------------------------

    /// <summary>
    /// Adds a line segment to current figure.
    /// </summary>
    public void AddLine(Point pt1, Point pt2)
    {
      this.items.Add(new XGraphicsPathItem(XGraphicsPathItemType.Lines, new XPoint(pt1.X, pt1.Y), new XPoint(pt2.X, pt2.Y)));
      this.dirty = true;
      this.gdipPath.AddLine(pt1, pt2);
    }

#if Gdip
    /// <summary>
    /// Adds  a line segment to current figure.
    /// </summary>
    public void AddLine(PointF pt1, PointF pt2)
    {
      this.items.Add(new XGraphicsPathItem(XGraphicsPathItemType.Lines, pt1, pt2));
      this.dirty = true;
      this.gdipPath.AddLine(pt1, pt2);
    }
#endif

    /// <summary>
    /// Adds  a line segment to current figure.
    /// </summary>
    public void AddLine(XPoint pt1, XPoint pt2)
    {
      this.items.Add(new XGraphicsPathItem(XGraphicsPathItemType.Lines, pt1, pt2));
      this.dirty = true;
      this.gdipPath.AddLine(pt1.ToPointF(), pt2.ToPointF());
    }

    /// <summary>
    /// Adds  a line segment to current figure.
    /// </summary>
    public void AddLine(int x1, int y1, int x2, int y2)
    {
      this.items.Add(new XGraphicsPathItem(XGraphicsPathItemType.Lines, new XPoint(x1, y1), new XPoint(x1, y1)));
      this.dirty = true;
      this.gdipPath.AddLine(x1, y1, x2, y2);
    }

    /// <summary>
    /// Adds  a line segment to current figure.
    /// </summary>
    public void AddLine(double x1, double y1, double x2, double y2)
    {
      this.items.Add(new XGraphicsPathItem(XGraphicsPathItemType.Lines, new XPoint(x1, y1), new XPoint(x1, y1)));
      this.dirty = true;
      this.gdipPath.AddLine((float)x1, (float)y1, (float)x2, (float)y2);
    }

    // ----- AddLines -----------------------------------------------------------------------------

    /// <summary>
    /// Adds a series of connected line segments to current figure.
    /// </summary>
    public void AddLines(Point[] points)
    {
      this.items.Add(new XGraphicsPathItem(XGraphicsPathItemType.Lines, XGraphics.MakeXPointArray(points)));
      this.dirty = true;
      this.gdipPath.AddLines(points);
    }

#if Gdip
    /// <summary>
    /// Adds a series of connected line segments to current figure.
    /// </summary>
    public void AddLines(PointF[] points)
    {
      this.items.Add(new XGraphicsPathItem(XGraphicsPathItemType.Lines, XGraphics.MakeXPointArray(points)));
      this.dirty = true;
      this.gdipPath.AddLines(points);
    }
#endif

    /// <summary>
    /// Adds a series of connected line segments to current figure.
    /// </summary>
    public void AddLines(XPoint[] points)
    {
      this.items.Add(new XGraphicsPathItem(XGraphicsPathItemType.Lines, points.Clone() as XPoint[]));
      this.dirty = true;
      this.gdipPath.AddLines(XGraphics.MakePointFArray(points));
    }

    // ----- AddBezier ----------------------------------------------------------------------------

    /// <summary>
    /// Adds a cubic Bézier curve to the current figure.
    /// </summary>
    public void AddBezier(Point pt1, Point pt2, Point pt3, Point pt4)
    {
      this.items.Add(new XGraphicsPathItem(XGraphicsPathItemType.Beziers,
        new XPoint(pt1.X, pt1.Y), new XPoint(pt2.X, pt2.Y), new XPoint(pt3.X, pt3.Y), new XPoint(pt4.X, pt4.Y)));
      this.dirty = true;
      this.gdipPath.AddBezier(pt1, pt2, pt3, pt4);
    }

#if Gdip
    /// <summary>
    /// Adds a cubic Bézier curve to the current figure.
    /// </summary>
    public void AddBezier(PointF pt1, PointF pt2, PointF pt3, PointF pt4)
    {
      this.items.Add(new XGraphicsPathItem(XGraphicsPathItemType.Beziers,
        new XPoint(pt1.X, pt1.Y), new XPoint(pt2.X, pt2.Y), new XPoint(pt3.X, pt3.Y), new XPoint(pt4.X, pt4.Y)));
      this.dirty = true;
      this.gdipPath.AddBezier(pt1, pt2, pt3, pt4);
    }
#endif

    /// <summary>
    /// Adds a cubic Bézier curve to the current figure.
    /// </summary>
    public void AddBezier(XPoint pt1, XPoint pt2, XPoint pt3, XPoint pt4)
    {
      this.items.Add(new XGraphicsPathItem(XGraphicsPathItemType.Beziers,
        new XPoint(pt1.X, pt1.Y), new XPoint(pt2.X, pt2.Y), new XPoint(pt3.X, pt3.Y), new XPoint(pt4.X, pt4.Y)));
      this.dirty = true;
      this.gdipPath.AddBezier(pt1.ToPointF(), pt2.ToPointF(), pt3.ToPointF(), pt4.ToPointF());
    }

    /// <summary>
    /// Adds a cubic Bézier curve to the current figure.
    /// </summary>
    public void AddBezier(int x1, int y1, int x2, int y2, int x3, int y3, int x4, int y4)
    {
      this.items.Add(new XGraphicsPathItem(XGraphicsPathItemType.Beziers,
        new XPoint(x1, y1), new XPoint(x2, y2), new XPoint(x3, y3), new XPoint(x4, y4)));
      this.dirty = true;
      this.gdipPath.AddBezier(x1, y1, x2, y2, x3, y3, x4, y4);
    }

    /// <summary>
    /// Adds a cubic Bézier curve to the current figure.
    /// </summary>
    public void AddBezier(double x1, double y1, double x2, double y2, double x3, double y3, double x4, double y4)
    {
      this.items.Add(new XGraphicsPathItem(XGraphicsPathItemType.Beziers,
        new XPoint(x1, y1), new XPoint(x2, y2), new XPoint(x3, y3), new XPoint(x4, y4)));
      this.dirty = true;
      this.gdipPath.AddBezier((float)x1, (float)y1, (float)x2, (float)y2, (float)x3, (float)y3, (float)x4, (float)y4);
    }

    // ----- AddBeziers ---------------------------------------------------------------------------

    /// <summary>
    /// Adds a sequence of connected cubic Bézier curves to the current figure.
    /// </summary>
    public void AddBeziers(Point[] points)
    {
      AddBeziers(XGraphics.MakePointFArray(points));
    }

#if Gdip
    /// <summary>
    /// Adds a sequence of connected cubic Bézier curves to the current figure.
    /// </summary>
    public void AddBeziers(PointF[] points)
    {
      if (points.Length < 4)
        throw new ArgumentException("At least four points required for bezier curve.", "points");

      if ((points.Length - 1) % 3 != 0)
        throw new ArgumentException("Invalid number of points for bezier curve. Number must fulfil 4+3n.", "points");

      this.items.Add(new XGraphicsPathItem(XGraphicsPathItemType.Beziers, points.Clone() as XPoint[]));
      this.dirty = true;
      this.gdipPath.AddBeziers(points);
    }
#endif

    /// <summary>
    /// Adds a sequence of connected cubic Bézier curves to the current figure.
    /// </summary>
    public void AddBeziers(XPoint[] points)
    {
      if (points.Length < 4)
        throw new ArgumentException("At least four points required for bezier curve.", "points");

      if ((points.Length - 1) % 3 != 0)
        throw new ArgumentException("Invalid number of points for bezier curve. Number must fulfil 4+3n.", "points");

      this.items.Add(new XGraphicsPathItem(XGraphicsPathItemType.Beziers, points.Clone() as XPoint[]));
      this.dirty = true;
      this.gdipPath.AddBeziers(XGraphics.MakePointFArray(points));
    }

    // ----- AddCurve -----------------------------------------------------------------------

    /// <summary>
    /// Adds a spline curve to the current figure.
    /// </summary>
    public void AddCurve(Point[] points)
    {
      this.gdipPath.AddCurve(points);
    }

#if Gdip
    /// <summary>
    /// Adds a spline curve to the current figure.
    /// </summary>
    public void AddCurve(PointF[] points)
    {
      this.gdipPath.AddCurve(points);
    }
#endif

    /// <summary>
    /// Adds a spline curve to the current figure.
    /// </summary>
    public void AddCurve(XPoint[] points)
    {
      this.gdipPath.AddCurve(XGraphics.MakePointFArray(points));
    }

    /// <summary>
    /// Adds a spline curve to the current figure.
    /// </summary>
    public void AddCurve(Point[] points, double tension)
    {
      this.gdipPath.AddCurve(points, (float)tension);
    }

#if Gdip
    /// <summary>
    /// Adds a spline curve to the current figure.
    /// </summary>
    public void AddCurve(PointF[] points, double tension)
    {
      this.gdipPath.AddCurve(points, (float)tension);
    }
#endif

    /// <summary>
    /// Adds a spline curve to the current figure.
    /// </summary>
    public void AddCurve(XPoint[] points, double tension)
    {
      this.gdipPath.AddCurve(XGraphics.MakePointFArray(points), (float)tension);
    }

    /// <summary>
    /// Adds a spline curve to the current figure.
    /// </summary>
    public void AddCurve(Point[] points, int offset, int numberOfSegments, float tension)
    {
      this.gdipPath.AddCurve(points, offset, numberOfSegments, tension);
    }

    /// <summary>
    /// Adds a spline curve to the current figure.
    /// </summary>
    public void AddCurve(PointF[] points, int offset, int numberOfSegments, float tension)
    {
      this.gdipPath.AddCurve(points, offset, numberOfSegments, tension);
    }

    // ----- AddArc -------------------------------------------------------------------------------

#if Gdip
    /// <summary>
    /// Adds an elliptical arc to the current figure.
    /// </summary>
    public void AddArc(Rectangle rect, double startAngle, double sweepAngle)
    {
      this.items.Add(new XGraphicsPathItem(XGraphicsPathItemType.Arc, new XPoint(rect.X, rect.Y), new XPoint(rect.Width, rect.Height),
        new XPoint(startAngle, sweepAngle)));
      this.dirty = true;
      this.gdipPath.AddArc(rect, (float)startAngle, (float)sweepAngle);
    }
#endif

#if Gdip
    /// <summary>
    /// Adds an elliptical arc to the current figure.
    /// </summary>
    public void AddArc(RectangleF rect, double startAngle, double sweepAngle)
    {
      this.items.Add(new XGraphicsPathItem(XGraphicsPathItemType.Arc, new XPoint(rect.X, rect.Y), new XPoint(rect.Width, rect.Height),
        new XPoint(startAngle, sweepAngle)));
      this.dirty = true;
      this.gdipPath.AddArc(rect, (float)startAngle, (float)sweepAngle);
    }
#endif

    /// <summary>
    /// Adds an elliptical arc to the current figure.
    /// </summary>
    public void AddArc(XRect rect, double startAngle, double sweepAngle)
    {
      this.items.Add(new XGraphicsPathItem(XGraphicsPathItemType.Arc, new XPoint(rect.X, rect.Y), new XPoint(rect.Width, rect.Height),
        new XPoint(startAngle, sweepAngle)));
      this.dirty = true;
      this.gdipPath.AddArc(rect.ToRectangleF(), (float)startAngle, (float)sweepAngle);
    }

    /// <summary>
    /// Adds an elliptical arc to the current figure.
    /// </summary>
    public void AddArc(int x, int y, int width, int height, int startAngle, int sweepAngle)
    {
      this.items.Add(new XGraphicsPathItem(XGraphicsPathItemType.Arc, new XPoint(x, y), new XPoint(width, height),
        new XPoint(startAngle, sweepAngle)));
      this.dirty = true;
      this.gdipPath.AddArc(x, y, width, height, startAngle, sweepAngle);
    }

    /// <summary>
    /// Adds an elliptical arc to the current figure.
    /// </summary>
    public void AddArc(double x, double y, double width, double height, double startAngle, double sweepAngle)
    {
      this.items.Add(new XGraphicsPathItem(XGraphicsPathItemType.Arc, new XPoint(x, y), new XPoint(width, height),
        new XPoint(startAngle, sweepAngle)));
      this.dirty = true;
      this.gdipPath.AddArc((float)x, (float)y, (float)width, (float)height, (float)startAngle, (float)sweepAngle);
    }

    // ----- AddRectangle -------------------------------------------------------------------------

#if Gdip
    /// <summary>
    /// Adds a rectangle to this path.
    /// </summary>
    public void AddRectangle(Rectangle rect)
    {
      this.items.Add(new XGraphicsPathItem(XGraphicsPathItemType.Rectangle, new XPoint(rect.X, rect.Y), new XPoint(rect.Width, rect.Height)));
      this.dirty = true;
      this.gdipPath.AddRectangle(rect);
    }
#endif

#if Gdip
    /// <summary>
    /// Adds a rectangle to this path.
    /// </summary>
    public void AddRectangle(RectangleF rect)
    {
      this.items.Add(new XGraphicsPathItem(XGraphicsPathItemType.Rectangle, new XPoint(rect.X, rect.Y), new XPoint(rect.Width, rect.Height)));
      this.dirty = true;
      this.gdipPath.AddRectangle(rect);
    }
#endif

    /// <summary>
    /// Adds a rectangle to this path.
    /// </summary>
    public void AddRectangle(XRect rect)
    {
      this.items.Add(new XGraphicsPathItem(XGraphicsPathItemType.Rectangle, new XPoint(rect.X, rect.Y), new XPoint(rect.Width, rect.Height)));
      this.dirty = true;
      this.gdipPath.AddRectangle(rect.ToRectangleF());
    }

    /// <summary>
    /// Adds a rectangle to this path.
    /// </summary>
    public void AddRectangle(int x, int y, int width, int height)
    {
      this.items.Add(new XGraphicsPathItem(XGraphicsPathItemType.Rectangle, new XPoint(x, y), new XPoint(width, height)));
      this.dirty = true;
      this.gdipPath.AddRectangle(new Rectangle(x, y, width, height));
    }

    /// <summary>
    /// Adds a rectangle to this path.
    /// </summary>
    public void AddRectangle(double x, double y, double width, double height)
    {
      this.items.Add(new XGraphicsPathItem(XGraphicsPathItemType.Rectangle, new XPoint(x, y), new XPoint(width, height)));
      this.dirty = true;
      this.gdipPath.AddRectangle(new RectangleF((float)x, (float)y, (float)width, (float)height));
    }

    // ----- AddRectangles ------------------------------------------------------------------------

#if Gdip
    /// <summary>
    /// Adds a series of rectangles to this path.
    /// </summary>
    public void AddRectangles(Rectangle[] rects)
    {
      int count = rects.Length;
      for (int idx = 0; idx < count; idx++)
        AddRectangle(rects[idx]);
      this.gdipPath.AddRectangles(rects);
    }
#endif

#if Gdip
    /// <summary>
    /// Adds a series of rectangles to this path.
    /// </summary>
    public void AddRectangles(RectangleF[] rects)
    {
      int count = rects.Length;
      for (int idx = 0; idx < count; idx++)
        AddRectangle(rects[idx]);
      this.gdipPath.AddRectangles(rects);
    }
#endif

    /// <summary>
    /// Adds a series of rectangles to this path.
    /// </summary>
    public void AddRectangles(XRect[] rects)
    {
      int count = rects.Length;
      for (int idx = 0; idx < count; idx++)
      {
        AddRectangle(rects[idx]);
        this.gdipPath.AddRectangle(rects[idx].ToRectangleF());
      }
    }

    // ----- AddRoundedRectangle ------------------------------------------------------------------

#if Gdip
    /// <summary>
    /// Adds a rectangle with rounded cornes to this path.
    /// </summary>
    public void AddRoundedRectangle(Rectangle rect, Size ellipseSize)
    {
      this.items.Add(new XGraphicsPathItem(XGraphicsPathItemType.RoundedRectangle, new XPoint(rect.X, rect.Y), new XPoint(rect.Width, rect.Height),
        new XPoint(ellipseSize.Width, ellipseSize.Height)));
      this.dirty = true;
      AddRoundedRectangle((double)rect.X, (double)rect.Y, (double)rect.Width, (double)rect.Height,
        (double)ellipseSize.Width, (double)ellipseSize.Height);
    }
#endif

#if Gdip
    /// <summary>
    /// Adds a rectangle with rounded cornes to this path.
    /// </summary>
    public void AddRoundedRectangle(RectangleF rect, SizeF ellipseSize)
    {
      this.items.Add(new XGraphicsPathItem(XGraphicsPathItemType.RoundedRectangle, new XPoint(rect.X, rect.Y), new XPoint(rect.Width, rect.Height),
        new XPoint(ellipseSize.Width, ellipseSize.Height)));
      this.dirty = true;
      AddRoundedRectangle((double)rect.X, (double)rect.Y, (double)rect.Width, (double)rect.Height,
        (double)ellipseSize.Width, (double)ellipseSize.Height);
    }
#endif

#if Gdip
    /// <summary>
    /// Adds a rectangle with rounded cornes to this path.
    /// </summary>
    public void AddRoundedRectangle(XRect rect, SizeF ellipseSize)
    {
      this.items.Add(new XGraphicsPathItem(XGraphicsPathItemType.RoundedRectangle, new XPoint(rect.X, rect.Y), new XPoint(rect.Width, rect.Height),
        new XPoint(ellipseSize.Width, ellipseSize.Height)));
      this.dirty = true;
      AddRoundedRectangle((double)rect.X, (double)rect.Y, (double)rect.Width, (double)rect.Height,
        (double)ellipseSize.Width, (double)ellipseSize.Height);
    }
#endif

    /// <summary>
    /// Adds a rectangle with rounded cornes to this path.
    /// </summary>
    public void AddRoundedRectangle(int x, int y, int width, int height, int ellipseWidth, int ellipseHeight)
    {
      this.items.Add(new XGraphicsPathItem(XGraphicsPathItemType.RoundedRectangle, new XPoint(x, y), new XPoint(width, height),
        new XPoint(ellipseWidth, ellipseHeight)));
      this.dirty = true;
      AddRoundedRectangle((double)x, (double)y, (double)width, (double)height, (double)ellipseWidth, (double)ellipseHeight);
    }

    /// <summary>
    /// Adds a rectangle with rounded cornes to this path.
    /// </summary>
    public void AddRoundedRectangle(double x, double y, double width, double height, double ellipseWidth, double ellipseHeight)
    {
      this.items.Add(new XGraphicsPathItem(XGraphicsPathItemType.RoundedRectangle, new XPoint(x, y), new XPoint(width, height),
        new XPoint(ellipseWidth, ellipseHeight)));
      this.dirty = true;

      //double x = item.points[0].X;
      //double y = item.points[0].Y;
      //double width = item.points[1].X;
      //double height = item.points[1].Y;
      //double ellipseWidth = item.points[2].X;
      //double ellipseHeight = item.points[2].Y;
      //PathXyz xyz = inPath ? PathXyz.LineTo1st : PathXyz.MoveTo1st;
      AddArc(x + width - ellipseWidth, y, ellipseWidth, ellipseHeight, -90, 90);
      AddArc(x + width - ellipseWidth, y + height - ellipseHeight, ellipseWidth, ellipseHeight, 0, 90);
      AddArc(x, y + height - ellipseHeight, ellipseWidth, ellipseHeight, 90, 90);
      AddArc(x, y, ellipseWidth, ellipseHeight, 180, 90);
      CloseFigure();
    }

    // ----- AddEllipse ---------------------------------------------------------------------------

#if Gdip
    /// <summary>
    /// Adds an ellipse to the current path.
    /// </summary>
    public void AddEllipse(Rectangle rect)
    {
      this.items.Add(new XGraphicsPathItem(XGraphicsPathItemType.Ellipse, new XPoint(rect.X, rect.Y), new XPoint(rect.Width, rect.Height)));
      this.dirty = true;
      this.gdipPath.AddEllipse(rect);
    }
#endif

#if Gdip
    /// <summary>
    /// Adds an ellipse to the current path.
    /// </summary>
    public void AddEllipse(RectangleF rect)
    {
      this.items.Add(new XGraphicsPathItem(XGraphicsPathItemType.Ellipse, new XPoint(rect.X, rect.Y), new XPoint(rect.Width, rect.Height)));
      this.dirty = true;
      this.gdipPath.AddEllipse(rect);
    }
#endif

    /// <summary>
    /// Adds an ellipse to the current path.
    /// </summary>
    public void AddEllipse(XRect rect)
    {
      this.items.Add(new XGraphicsPathItem(XGraphicsPathItemType.Ellipse, new XPoint(rect.X, rect.Y), new XPoint(rect.Width, rect.Height)));
      this.dirty = true;
      this.gdipPath.AddEllipse(rect.ToRectangleF());
    }

    /// <summary>
    /// Adds an ellipse to the current path.
    /// </summary>
    public void AddEllipse(int x, int y, int width, int height)
    {
      this.items.Add(new XGraphicsPathItem(XGraphicsPathItemType.Ellipse, new XPoint(x, y), new XPoint(width, height)));
      this.dirty = true;
      this.gdipPath.AddEllipse(x, y, width, height);
    }

    /// <summary>
    /// Adds an ellipse to the current path.
    /// </summary>
    public void AddEllipse(double x, double y, double width, double height)
    {
      this.items.Add(new XGraphicsPathItem(XGraphicsPathItemType.Ellipse, new XPoint(x, y), new XPoint(width, height)));
      this.dirty = true;
      this.gdipPath.AddEllipse((float)x, (float)y, (float)width, (float)height);
    }

    // ----- AddPolygon ---------------------------------------------------------------------------

    /// <summary>
    /// Adds a polygon to this path.
    /// </summary>
    public void AddPolygon(Point[] points)
    {
      this.items.Add(new XGraphicsPathItem(XGraphicsPathItemType.Polygon, XGraphics.MakeXPointArray(points)));
      this.dirty = true;
      this.gdipPath.AddPolygon(points);
    }

#if Gdip
    /// <summary>
    /// Adds a polygon to this path.
    /// </summary>
    public void AddPolygon(PointF[] points)
    {
      this.items.Add(new XGraphicsPathItem(XGraphicsPathItemType.Polygon, XGraphics.MakeXPointArray(points)));
      this.dirty = true;
      this.gdipPath.AddPolygon(points);
    }
#endif

    /// <summary>
    /// Adds a polygon to this path.
    /// </summary>
    public void AddPolygon(XPoint[] points)
    {
      this.items.Add(new XGraphicsPathItem(XGraphicsPathItemType.Polygon, points.Clone() as XPoint[]));
      this.dirty = true;
      this.gdipPath.AddPolygon(XGraphics.MakePointFArray(points));
    }

    // ----- AddPie -------------------------------------------------------------------------------

#if Gdip
    /// <summary>
    /// Adds the outline of a pie shape to this path.
    /// </summary>
    public void AddPie(Rectangle rect, double startAngle, double sweepAngle)
    {
      this.gdipPath.AddPie(rect, (float)startAngle, (float)sweepAngle);
    }
#endif

#if Gdip
    /// <summary>
    /// Adds the outline of a pie shape to this path.
    /// </summary>
    public void AddPie(RectangleF rect, double startAngle, double sweepAngle)
    {
      AddPie((double)rect.X, (double)rect.Y, (double)rect.Width, (double)rect.Height, startAngle, sweepAngle);
    }
#endif

    /// <summary>
    /// Adds the outline of a pie shape to this path.
    /// </summary>
    public void AddPie(XRect rect, double startAngle, double sweepAngle)
    {
      AddPie(rect.X, rect.Y, rect.Width, rect.Height, startAngle, sweepAngle);
    }

    /// <summary>
    /// Adds the outline of a pie shape to this path.
    /// </summary>
    public void AddPie(int x, int y, int width, int height, double startAngle, double sweepAngle)
    {
      AddPie((double)x, (double)y, (double)width, (double)height, startAngle, sweepAngle);
    }

    /// <summary>
    /// Adds the outline of a pie shape to this path.
    /// </summary>
    public void AddPie(double x, double y, double width, double height, double startAngle, double sweepAngle)
    {
      this.gdipPath.AddPie((float)x, (float)y, (float)width, (float)height, (float)startAngle, (float)sweepAngle);
    }

    // ----- AddClosedCurve ------------------------------------------------------------------------

    /// <summary>
    /// Adds a closed curve to this path.
    /// </summary>
    public void AddClosedCurve(Point[] points)
    {
      this.gdipPath.AddClosedCurve(points);
    }

#if Gdip
    /// <summary>
    /// Adds a closed curve to this path.
    /// </summary>
    public void AddClosedCurve(PointF[] points)
    {
      this.gdipPath.AddClosedCurve(points);
    }
#endif

    /// <summary>
    /// Adds a closed curve to this path.
    /// </summary>
    public void AddClosedCurve(XPoint[] points)
    {
      this.gdipPath.AddClosedCurve(XGraphics.MakePointFArray(points));
    }

    /// <summary>
    /// Adds a closed curve to this path.
    /// </summary>
    public void AddClosedCurve(Point[] points, double tension)
    {
      this.gdipPath.AddClosedCurve(points, (float)tension);
    }

#if Gdip
    /// <summary>
    /// Adds a closed curve to this path.
    /// </summary>
    public void AddClosedCurve(PointF[] points, double tension)
    {
      this.gdipPath.AddClosedCurve(points, (float)tension);
    }
#endif

    /// <summary>
    /// Adds a closed curve to this path.
    /// </summary>
    public void AddClosedCurve(XPoint[] points, double tension)
    {
      this.gdipPath.AddClosedCurve(XGraphics.MakePointFArray(points), (float)tension);
    }

    // ----- AddPath ------------------------------------------------------------------------------

    /// <summary>
    /// Adds the specified path to this path.
    /// </summary>
    public void AddPath(XGraphicsPath path, bool connect)
    {
      if (!connect)
      {
        if (this.items.Count > 0 && ((XGraphicsPathItem)this.items[0]).type != XGraphicsPathItemType.CloseFigure)
          this.items.Add(new XGraphicsPathItem(XGraphicsPathItemType.CloseFigure));
      }
      this.items.AddRange(path.items);
      this.dirty = true;
      this.gdipPath.AddPath(path.gdipPath, connect);
    }

    // ----- AddString ----------------------------------------------------------------------------

    /// <summary>
    /// Adds a text string to this path.
    /// </summary>
    public void AddString(string s, XFontFamily family, XFontStyle style, double emSize, Point origin, XStringFormat format)
    {
      this.gdipPath.AddString(s, family.family, (int)style, (float)emSize, origin, format.RealizeGdiStringFormat());
    }

#if Gdip
    /// <summary>
    /// Adds a text string to this path.
    /// </summary>
    public void AddString(string s, XFontFamily family, XFontStyle style, double emSize, PointF origin, XStringFormat format)
    {
      this.gdipPath.AddString(s, family.family, (int)style, (float)emSize, origin, format.RealizeGdiStringFormat());
    }
#endif

    /// <summary>
    /// Adds a text string to this path.
    /// </summary>
    public void AddString(string s, XFontFamily family, XFontStyle style, double emSize, XPoint origin, XStringFormat format)
    {
      this.gdipPath.AddString(s, family.family, (int)style, (float)emSize, origin.ToPointF(), format.RealizeGdiStringFormat());
    }

#if Gdip
    /// <summary>
    /// Adds a text string to this path.
    /// </summary>
    public void AddString(string s, XFontFamily family, XFontStyle style, double emSize, Rectangle layoutRect, XStringFormat format)
    {
      this.gdipPath.AddString(s, family.family, (int)style, (float)emSize, layoutRect, format.RealizeGdiStringFormat());
    }
#endif

#if Gdip
    /// <summary>
    /// Adds a text string to this path.
    /// </summary>
    public void AddString(string s, XFontFamily family, XFontStyle style, double emSize, RectangleF layoutRect, XStringFormat format)
    {
      this.gdipPath.AddString(s, family.family, (int)style, (float)emSize, layoutRect, format.RealizeGdiStringFormat());
    }
#endif

    /// <summary>
    /// Adds a text string to this path.
    /// </summary>
    public void AddString(string s, XFontFamily family, XFontStyle style, double emSize, XRect layoutRect, XStringFormat format)
    {
      this.gdipPath.AddString(s, family.family, (int)style, (float)emSize, layoutRect.ToRectangleF(), format.RealizeGdiStringFormat());
    }

    // ----- CloseAllFigures ----------------------------------------------------------------------

    // TODO? CloseAllFigures
    //public void CloseAllFigures();

    // --------------------------------------------------------------------------------------------

    /// <summary>
    /// Closes the current figure and starts a new figure.
    /// </summary>
    public void CloseFigure()
    {
      this.items.Add(new XGraphicsPathItem(XGraphicsPathItemType.CloseFigure));
      this.gdipPath.CloseAllFigures();
    }

    /// <summary>
    /// Starts a new figure without closing the current figure.
    /// </summary>
    public void StartFigure()
    {
      this.items.Add(new XGraphicsPathItem(XGraphicsPathItemType.StartFigure));
      this.gdipPath.StartFigure();
    }

    // --------------------------------------------------------------------------------------------

    /// <summary>
    /// Gets or sets an XFillMode that determines how the interiors of shapes are filled.
    /// </summary>
    public XFillMode FillMode
    {
      get { return this.fillMode; }
      set 
      { 
        this.fillMode = value; 
        this.gdipPath.FillMode = (FillMode)value;
      }
    }
    XFillMode fillMode;

    // --------------------------------------------------------------------------------------------

    /// <summary>
    /// Converts each curve in this XGraphicsPath into a sequence of connected line segments. 
    /// </summary>
    public void Flatten()
    {
      this.gdipPath.Flatten();
    }

    /// <summary>
    /// Converts each curve in this XGraphicsPath into a sequence of connected line segments. 
    /// </summary>
    public void Flatten(XMatrix matrix)
    {
      this.gdipPath.Flatten(matrix.ToMatrix());
    }

    /// <summary>
    /// Converts each curve in this XGraphicsPath into a sequence of connected line segments. 
    /// </summary>
    public void Flatten(XMatrix matrix, double flatness)
    {
      this.gdipPath.Flatten(matrix.ToMatrix(), (float)flatness);
    }

    // --------------------------------------------------------------------------------------------

    /// <summary>
    /// Replaces this path with curves that enclose the area that is filled when this path is drawn 
    /// by the specified pen.
    /// </summary>
    public void Widen(XPen pen)
    {
      this.gdipPath.Widen(pen.RealizeGdiPen());
    }

    /// <summary>
    /// Replaces this path with curves that enclose the area that is filled when this path is drawn 
    /// by the specified pen.
    /// </summary>
    public void Widen(XPen pen, XMatrix matrix)
    {
      this.gdipPath.Widen(pen.RealizeGdiPen(), matrix.ToMatrix());
    }

    /// <summary>
    /// Replaces this path with curves that enclose the area that is filled when this path is drawn 
    /// by the specified pen.
    /// </summary>
    public void Widen(XPen pen, XMatrix matrix, double flatness)
    {
      this.gdipPath.Widen(pen.RealizeGdiPen(), matrix.ToMatrix(), (float)flatness);
    }

    // --------------------------------------------------------------------------------------------

    ArrayList items;

#if Gdip
    /// <summary>
    /// Converts the XGraphicsPath into a GDI+ path.
    /// </summary>
    internal GraphicsPath RealizeGdiPath()
    {
#if true
      return this.gdipPath;
#else
      if (this.dirty)
      {
        if (this.path != null)
          this.path.Dispose();
        this.path = new GraphicsPath();

        int count = this.items.Count;
        for (int idx = 0; idx < count; idx++)
        {
          XGraphicsPathItem item = (XGraphicsPathItem)this.items[idx];
          switch (item.type)
          {
            case XGraphicsPathItemType.Lines:
              this.path.AddLines(XGraphics.MakePointFArray(item.points));
              break;

            case XGraphicsPathItemType.Beziers:
              this.path.AddBeziers(XGraphics.MakePointFArray(item.points));
              break;

            case XGraphicsPathItemType.Curve:
              throw new NotImplementedException("XGraphicsPathItemType.Curve");

            case XGraphicsPathItemType.Arc:
              this.path.AddArc((float)item.points[0].X, (float)item.points[0].Y, 
                (float)item.points[1].X, (float)item.points[1].Y, 
                (float)item.points[2].X, (float)item.points[2].Y);
              break;

            case XGraphicsPathItemType.Rectangle:
              this.path.AddRectangle(new RectangleF((float)item.points[0].X, (float)item.points[0].Y, 
                (float)item.points[1].X, (float)item.points[1].Y));
              break;

            case XGraphicsPathItemType.RoundedRectangle:
            {
              float x = (float)item.points[0].X;
              float y = (float)item.points[0].Y;
              float width = (float)item.points[1].X;
              float height = (float)item.points[1].Y;
              float ellipseWidth = (float)item.points[2].X;
              float ellipseHeight = (float)item.points[2].Y;
              this.path.AddArc(x + width - ellipseWidth, y, ellipseWidth, ellipseHeight, -90, 90);
              this.path.AddArc(x + width - ellipseWidth, y + height - ellipseHeight, ellipseWidth, ellipseHeight, 0, 90);
              this.path.AddArc(x, y + height - ellipseHeight, ellipseWidth, ellipseHeight, 90, 90);
              this.path.AddArc(x, y, ellipseWidth, ellipseHeight, 180, 90);
              this.path.CloseFigure();
            }
              break;

            case XGraphicsPathItemType.Ellipse:
              this.path.AddEllipse(new RectangleF((float)item.points[0].X, (float)item.points[0].Y, (float)item.points[1].X, (float)item.points[1].Y));
              break;

            case XGraphicsPathItemType.Polygon:
              this.path.AddPolygon(XGraphics.MakePointFArray(item.points));
              break;

            case XGraphicsPathItemType.CloseFigure:
              this.path.CloseFigure();
              break;
          }
        }
        this.dirty = false;
      }
      return this.path;
#endif
    }
#endif
    bool dirty = true;
  }
  //enum PathPointType
  //
  //  Start = 0,
  //  Line = 1,
  //  Bezier = 3,
  //  Bezier3 = 3,
  //  PathTypeMask = 7,
  //  DashMode = 0x10,
  //  CloseSubpath = 0x80,
  //  PathMarker = 0x20,
  //}
}
