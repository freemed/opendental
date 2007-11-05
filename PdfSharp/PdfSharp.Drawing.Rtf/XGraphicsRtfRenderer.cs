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
using System.Windows.Media;
#endif
using PdfSharp.Internal;

namespace PdfSharp.Drawing.Rtf
{
  /// <summary>
  /// Represents a drawing surface for PdfPages.
  /// </summary>
  class XGraphicsRtfRenderer
  {
    public XGraphicsRtfRenderer()
    {
    }
#if false
    // ----- DrawLine -----------------------------------------------------------------------------

    public void DrawLine(XPen pen, float x1, float y1, float x2, float y2)
    {
      if (this.gfx != null)
        this.gfx.DrawLine(pen.RealizeGdiPen(), x1, y1, x2, y2);

      if (this.pdfPage != null)
        this.pdfPage.PageContent.DrawLines(pen, new PointF[2]{new PointF(x1, y1), new PointF(x2, y2)});
    }

    // ----- DrawLines ----------------------------------------------------------------------------

    public void DrawLines(XPen pen, PointF[] points)
    {
      if (this.gfx != null)
        this.gfx.DrawLines(pen.RealizeGdiPen(), points);

      if (this.pdfPage != null)
        this.pdfPage.PageContent.DrawLines(pen, points);
    }

    // ----- DrawRectangle ------------------------------------------------------------------------

    public void DrawRectangle(XPen pen, XSolidBrush brush, float x, float y, float width, float height)
    {
      if (this.gfx != null)
      {
        this.gfx.FillRectangle(brush.RealizeGdiBrush(), x, y, width, height);
        this.gfx.DrawRectangle(pen.RealizeGdiPen(), x, y, width, height);
      }
      if (this.pdfPage != null)
        this.pdfPage.PageContent.DrawRectangle(pen, brush, x, y, width, height);
    }

    // ----- DrawRectangles -----------------------------------------------------------------------

    //public void DrawRectangles(XPen pen, XSolidBrush brush, Rectangle[] rects)
    //{
    //  if (this.gfx != null)
    //  {
    //    this.gfx.FillRectangles(brush.RealizeGdiBrush(), rects);
    //    this.gfx.DrawRectangles(pen.RealizeGdiPen(), rects);
    //  }
    //  if (this.pdfPage != null)
    //  {
    //    int count = rects.Length;
    //    for (int idx = 0; idx < count; idx++)
    //    {
    //      Rectangle rect = rects[idx];
    //      this.pdfPage.PageContent.DrawRectangle(pen, brush, rect.X, rect.Y, rect.Width, rect.Height);
    //    }
    //  }
    //}

    public void DrawRectangles(XPen pen, XSolidBrush brush, RectangleF[] rects)
    {
      if (this.gfx != null)
      {
        this.gfx.FillRectangles(brush.RealizeGdiBrush(), rects);
        this.gfx.DrawRectangles(pen.RealizeGdiPen(), rects);
      }
      if (this.pdfPage != null)
      {
        int count = rects.Length;
        for (int idx = 0; idx < count; idx++)
        {
          RectangleF rect = rects[idx];
          this.pdfPage.PageContent.DrawRectangle(pen, brush, rect.X, rect.Y, rect.Width, rect.Height);
        }
      }
    }

    // ----- DrawEllipse --------------------------------------------------------------------------

    //public void DrawEllipse(XPen pen, Rectangle rect);
    //public void DrawEllipse(XPen pen, RectangleF rect);
    //public void DrawEllipse(XPen pen, int x, int y, int width, int height);
    //public void DrawEllipse(XPen pen, float x, float y, float width, float height);

    // ----- DrawPolygon --------------------------------------------------------------------------

    //public void DrawPolygon(XPen pen, Point[] points);
    //public void DrawPolygon(XPen pen, PointF[] points);

    // ----- DrawPath -----------------------------------------------------------------------------

    public void DrawPath(XPen pen, XSolidBrush brush, XGraphicsPath path)
    {
      if (this.gfx != null)
      {
        this.gfx.FillPath(brush.RealizeGdiBrush(), path.RealizeGdiPath());
        this.gfx.DrawPath(pen.RealizeGdiPen(), path.RealizeGdiPath());
      }
      if (this.pdfPage != null)
        this.pdfPage.PageContent.DrawPath(pen, brush, path);
    }

    // ----- DrawImage ----------------------------------------------------------------------------

    //public void DrawImage(Image image, Point point);
    //public void DrawImage(Image image, PointF point);
    //public void DrawImage(Image image, Point[] destPoints);
    //public void DrawImage(Image image, PointF[] destPoints);
    //public void DrawImage(Image image, Rectangle rect);
    //public void DrawImage(Image image, RectangleF rect);
    //public void DrawImage(Image image, int x, int y);
    //public void DrawImage(Image image, float x, float y);
    //public void DrawImage(Image image, Point[] destPoints, Rectangle srcRect, GraphicsUnit srcUnit);
    //public void DrawImage(Image image, Rectangle destRect, Rectangle srcRect, GraphicsUnit srcUnit);
    //public void DrawImage(Image image, RectangleF destRect, RectangleF srcRect, GraphicsUnit srcUnit);
    //public void DrawImage(Image image, PointF[] destPoints, RectangleF srcRect, GraphicsUnit srcUnit);
    //public void DrawImage(Image image, int x, int y, Rectangle srcRect, GraphicsUnit srcUnit);
    //public void DrawImage(Image image, float x, float y, RectangleF srcRect, GraphicsUnit srcUnit);
    //public void DrawImage(Image image, Point[] destPoints, Rectangle srcRect, GraphicsUnit srcUnit, ImageAttributes imageAttr);
    //public void DrawImage(Image image, PointF[] destPoints, RectangleF srcRect, GraphicsUnit srcUnit, ImageAttributes imageAttr);
    //public void DrawImage(Image image, int x, int y, int width, int height);
    //public void DrawImage(Image image, float x, float y, float width, float height);
    //public void DrawImage(Image image, Point[] destPoints, Rectangle srcRect, GraphicsUnit srcUnit, ImageAttributes imageAttr, DrawImageAbort callback);
    //public void DrawImage(Image image, PointF[] destPoints, RectangleF srcRect, GraphicsUnit srcUnit, ImageAttributes imageAttr, DrawImageAbort callback);
    //public void DrawImage(Image image, Rectangle destRect, int srcX, int srcY, int srcWidth, int srcHeight, GraphicsUnit srcUnit);
    //public void DrawImage(Image image, Rectangle destRect, float srcX, float srcY, float srcWidth, float srcHeight, GraphicsUnit srcUnit);
    //public void DrawImage(Image image, Point[] destPoints, Rectangle srcRect, GraphicsUnit srcUnit, ImageAttributes imageAttr, DrawImageAbort callback, int callbackData);
    //public void DrawImage(Image image, PointF[] destPoints, RectangleF srcRect, GraphicsUnit srcUnit, ImageAttributes imageAttr, DrawImageAbort callback, int callbackData);
    //public void DrawImage(Image image, Rectangle destRect, int srcX, int srcY, int srcWidth, int srcHeight, GraphicsUnit srcUnit, ImageAttributes imageAttr);
    //public void DrawImage(Image image, Rectangle destRect, float srcX, float srcY, float srcWidth, float srcHeight, GraphicsUnit srcUnit, ImageAttributes imageAttrs);
    //public void DrawImage(Image image, Rectangle destRect, int srcX, int srcY, int srcWidth, int srcHeight, GraphicsUnit srcUnit, ImageAttributes imageAttr, DrawImageAbort callback);
    //public void DrawImage(Image image, Rectangle destRect, float srcX, float srcY, float srcWidth, float srcHeight, GraphicsUnit srcUnit, ImageAttributes imageAttrs, DrawImageAbort callback);
    //public void DrawImage(Image image, Rectangle destRect, int srcX, int srcY, int srcWidth, int srcHeight, GraphicsUnit srcUnit, ImageAttributes imageAttrs, DrawImageAbort callback, IntPtr callbackData);
    //public void DrawImage(Image image, Rectangle destRect, float srcX, float srcY, float srcWidth, float srcHeight, GraphicsUnit srcUnit, ImageAttributes

    // --------------------------------------------------------------------------------------------
#endif
  }
}
