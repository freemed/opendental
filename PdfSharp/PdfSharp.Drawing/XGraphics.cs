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
using System.Globalization;
using System.IO;
#if Gdip
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
#endif
#if Wpf
using System.Windows;
using System.Windows.Media;
#endif
using PdfSharp.Internal;
using PdfSharp.Pdf;
using PdfSharp.Drawing.Pdf;
using PdfSharp.Pdf.Advanced;

namespace PdfSharp.Drawing
{
  /// <summary>
  /// Represents a drawing surface (or canvas) for a fixed size page.
  /// </summary>
  public class XGraphics : IDisposable
  {
#if Gdip
    /// <summary>
    /// Initializes a new instance of the XGraphics class.
    /// </summary>
    /// <param name="gfx">The GFX.</param>
    /// <param name="size">The size.</param>
    /// <param name="pageUnit">The page unit.</param>
    /// <param name="pageDirection">The page direction.</param>
    XGraphics(Graphics gfx, XSize size, XGraphicsUnit pageUnit, XPageDirection pageDirection)
    {
      if (gfx == null)
        throw new ArgumentNullException("gfx");

      this.gfx = gfx;
      this.drawGraphics = true;
      this.pageSize = new XSize(size.width, size.height);
      this.pageUnit = pageUnit;
      switch (pageUnit)
      {
        case XGraphicsUnit.Point:
          this.pageSizePoints = new XSize(size.width, size.height);
          break;

        case XGraphicsUnit.Inch:
          this.pageSizePoints = new XSize(XUnit.FromInch(size.width), XUnit.FromInch(size.height));
          break;

        case XGraphicsUnit.Millimeter:
          this.pageSizePoints = new XSize(XUnit.FromMillimeter(size.width), XUnit.FromMillimeter(size.height));
          break;

        case XGraphicsUnit.Centimeter:
          this.pageSizePoints = new XSize(XUnit.FromCentimeter(size.width), XUnit.FromCentimeter(size.height));
          break;

        default:
          throw new NotImplementedException("unit");
      }

      this.pageDirection = pageDirection;
      Initialize();
    }
#endif

    /// <summary>
    /// Initializes a new instance of the XGraphics class for drawing on a PDF page.
    /// </summary>
    XGraphics(PdfPage page, XGraphicsPdfPageOptions options, XGraphicsUnit pageUnit, XPageDirection pageDirection)
    {
      if (page == null)
        throw new ArgumentNullException("page");

      if (page.Owner == null)
        throw new ArgumentException("You cannot draw on a page that is not owned by a PdfDocument object.", "page");

      if (page.RenderContent != null)
        throw new InvalidOperationException("An XGraphics object already exists for this page and must be disposed before a new one can be created.");

      if (page.Owner.IsReadOnly)
        throw new InvalidOperationException("Cannot create XGraphics for a page of a document that cannot be modiefied");

      PdfContent content = null;
      switch (options)
      {
        case XGraphicsPdfPageOptions.Replace:
          page.Contents.Elements.Clear();
          goto case XGraphicsPdfPageOptions.Append;

        case XGraphicsPdfPageOptions.Prepend:
          content = page.Contents.PrependContent();
          break;

        case XGraphicsPdfPageOptions.Append:
          content = page.Contents.AppendContent();
          break;
      }
      page.RenderContent = content;

      this.gfx = Graphics.FromHwnd(IntPtr.Zero);
      this.renderer = new PdfSharp.Drawing.Pdf.XGraphicsPdfRenderer(page, this, options);
      this.pageSizePoints = new XSize(page.Width, page.Height);
      switch (pageUnit)
      {
        case XGraphicsUnit.Point:
          this.pageSize = new XSize(page.Width, page.Height);
          break;

        case XGraphicsUnit.Inch:
          this.pageSize = new XSize(XUnit.FromPoint(page.Width).Inch, XUnit.FromPoint(page.Height).Inch);
          break;

        case XGraphicsUnit.Millimeter:
          this.pageSize = new XSize(XUnit.FromPoint(page.Width).Millimeter, XUnit.FromPoint(page.Height).Millimeter);
          break;

        case XGraphicsUnit.Centimeter:
          this.pageSize = new XSize(XUnit.FromPoint(page.Width).Centimeter, XUnit.FromPoint(page.Height).Centimeter);
          break;

        default:
          throw new NotImplementedException("unit");
      }
      this.pageUnit = pageUnit;
      this.pageDirection = pageDirection;

      Initialize();
    }

    /// <summary>
    /// Initializes a new instance of the XGraphics class used for drawing on a form.
    /// </summary>
    XGraphics(XForm form)
    {
      if (form == null)
        throw new ArgumentNullException("form");

      this.form = form;
      form.AssociateGraphics(this);

      // If form.Owner is null create a meta file.
      if (form.Owner == null)
      {
        MemoryStream stream = new MemoryStream();
        Graphics refgfx = Graphics.FromHwnd(IntPtr.Zero);
        IntPtr hdc = refgfx.GetHdc();

#if true_
        // This code comes from my C++ RenderContext and checks some confusing details in connection 
        // with metafiles.
        //                                                                                    Display                 | LaserJet
        //                                                                               DPI   96 : 120               | 300
        // physical device size in MM                                                    ---------------------------------------------
        int horzSizeMM    = NativeMethods.GetDeviceCaps(hdc, NativeMethods.HORZSIZE);    // = 360 : 360               | 198 (not 210)
        int vertSizeMM    = NativeMethods.GetDeviceCaps(hdc, NativeMethods.VERTSIZE);    // = 290 : 290               | 288 (hot 297)
        // Cool:
        // My monitor is a Sony SDM-N80 and it's size is EXACTLY 360mm x 290mm!!
        // It is an 18.1" Flat Panel LCD display from 2002 and these are the values
        // an older display drivers reports in about 2003:
        //        Display  
        //  DPI   96 : 120
        //  --------------
        //       330 : 254
        //       254 : 203
        // Obviously my ATI driver reports the exact size of the monitor.


        // device size in pixel
        int horzSizePixel = NativeMethods.GetDeviceCaps(hdc, NativeMethods.HORZRES);     // = 1280 : 1280             | 4676
        int vertSizePixel = NativeMethods.GetDeviceCaps(hdc, NativeMethods.VERTRES);     // = 1024 : 1024             | 6814

        // 'logical' device resolution in DPI
        int logResX       = NativeMethods.GetDeviceCaps(hdc, NativeMethods.LOGPIXELSX);  // = 96 : 120                | 600
        int logResY       = NativeMethods.GetDeviceCaps(hdc, NativeMethods.LOGPIXELSY);  // = 96 : 120                | 600

        // now we can get the 'physical' device resolution...
        float phyResX = horzSizePixel / (horzSizeMM / 25.4f);  // = 98.521210 : 128.00000   | 599.85052
        float phyResY = vertSizePixel / (vertSizeMM / 25.4f);  // = 102.40000 : 128.12611   | 600.95691

        // ...and rescale the size of the meta rectangle.
        float magicX = logResX / phyResX;                      // = 0.97440946 : 0.93750000 | 1.0002491
        float magicY = logResY / phyResY;                      // = 0.93750000 : 0.93657720 | 0.99840766

        // use A4 page in point
        // adjust size of A4 page so that meta file fits with DrawImage...
        //RectangleF rcMagic = new RectangleF(0, 0, magicX * form.Width, magicY * form.Height);
        //m_PreviewMetafile = new Metafile(hdc, rcMagic, MetafileFrameUnitPoint,
        //  EmfTypeEmfPlusOnly, L"some description");
#endif

        RectangleF rect = new RectangleF(0, 0, form.Width, form.Height);
        this.metafile = new Metafile(stream, hdc, rect, MetafileFrameUnit.Pixel); //, EmfType.EmfPlusOnly);

        // Petzold disposes the refgfx object, although the hdc is in use of the metafile
        refgfx.ReleaseHdc(hdc);
        refgfx.Dispose();

        this.gfx = Graphics.FromImage(this.metafile);
        this.gfx.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
        this.drawGraphics = true;
      }
      else
      {
        this.metafile = null;
        this.gfx = Graphics.FromHwnd(IntPtr.Zero);
      }
      if (form.Owner != null)
        this.renderer = new PdfSharp.Drawing.Pdf.XGraphicsPdfRenderer(form, this);
      this.pageSize = form.Size;
      Initialize();
    }

#if Gdip
    /// <summary>
    /// Creates a new instance of the XGraphics class from a System.Drawing.Graphics object.
    /// </summary>
    public static XGraphics FromGraphics(Graphics graphics, XSize size)
    {
      // TODO: Get object from cache...
      return new XGraphics(graphics, size, XGraphicsUnit.Point, XPageDirection.Downwards);
    }

    /// <summary>
    /// Creates a new instance of the XGraphics class from a System.Drawing.Graphics object.
    /// </summary>
    public static XGraphics FromGraphics(Graphics graphics, XSize size, XGraphicsUnit unit)
    {
      // TODO: Get object from cache...
      return new XGraphics(graphics, size, unit, XPageDirection.Downwards);
    }

    ///// <summary>
    ///// Creates a new instance of the XGraphics class from a System.Drawing.Graphics object.
    ///// </summary>
    //public static XGraphics FromGraphics(Graphics graphics, XSize size, XPageDirection pageDirection)
    //{
    //  // TODO: Get object from cache...
    //  return new XGraphics(graphics, size, XGraphicsUnit.Point, pageDirection);
    //}

    ///// <summary>
    ///// Creates a new instance of the XGraphics class from a System.Drawing.Graphics object.
    ///// </summary>
    //public static XGraphics FromGraphics(Graphics graphics, XSize size, XGraphicsUnit unit, XPageDirection pageDirection)
    //{
    //  // TODO: Get object from cache...
    //  return new XGraphics(graphics, size, XGraphicsUnit.Point, pageDirection);
    //}
#endif

    /// <summary>
    /// Creates a new instance of the XGraphics class from a PdfSharp.Pdf.PdfPage object.
    /// </summary>
    public static XGraphics FromPdfPage(PdfPage page)
    {
      return new XGraphics(page, XGraphicsPdfPageOptions.Append, XGraphicsUnit.Point, XPageDirection.Downwards);
    }

    /// <summary>
    /// Creates a new instance of the XGraphics class from a PdfSharp.Pdf.PdfPage object.
    /// </summary>
    public static XGraphics FromPdfPage(PdfPage page, XGraphicsUnit unit)
    {
      return new XGraphics(page, XGraphicsPdfPageOptions.Append, unit, XPageDirection.Downwards);
    }

    /// <summary>
    /// Creates a new instance of the XGraphics class from a PdfSharp.Pdf.PdfPage object.
    /// </summary>
    public static XGraphics FromPdfPage(PdfPage page, XPageDirection pageDirection)
    {
      return new XGraphics(page, XGraphicsPdfPageOptions.Append, XGraphicsUnit.Point, pageDirection);
    }

    /// <summary>
    /// Creates a new instance of the XGraphics class from a PdfSharp.Pdf.PdfPage object.
    /// </summary>
    public static XGraphics FromPdfPage(PdfPage page, XGraphicsPdfPageOptions options)
    {
      return new XGraphics(page, options, XGraphicsUnit.Point, XPageDirection.Downwards);
    }

    /// <summary>
    /// Creates a new instance of the XGraphics class from a PdfSharp.Pdf.PdfPage object.
    /// </summary>
    public static XGraphics FromPdfPage(PdfPage page, XGraphicsPdfPageOptions options, XPageDirection pageDirection)
    {
      return new XGraphics(page, options, XGraphicsUnit.Point, pageDirection);
    }

    /// <summary>
    /// Creates a new instance of the XGraphics class from a PdfSharp.Pdf.PdfPage object.
    /// </summary>
    public static XGraphics FromPdfPage(PdfPage page, XGraphicsPdfPageOptions options, XGraphicsUnit unit)
    {
      return new XGraphics(page, options, unit, XPageDirection.Downwards);
    }

    /// <summary>
    /// Creates a new instance of the XGraphics class from a PdfSharp.Pdf.PdfPage object.
    /// </summary>
    public static XGraphics FromPdfPage(PdfPage page, XGraphicsPdfPageOptions options, XGraphicsUnit unit, XPageDirection pageDirection)
    {
      return new XGraphics(page, options, unit, pageDirection);
    }

    /// <summary>
    /// Creates a new instance of the XGraphics class from a PdfSharp.Drawing.XPdfForm object.
    /// </summary>
    public static XGraphics FromPdfForm(XPdfForm form)
    {
      if (form.gfx != null)
        return form.gfx;

      return new XGraphics(form);
    }

    /// <summary>
    /// Creates a new instance of the XGraphics class from a PdfSharp.Drawing.XForm object.
    /// </summary>
    public static XGraphics FromForm(XForm form)
    {
      if (form.gfx != null)
        return form.gfx;

      return new XGraphics(form);
    }

    /// <summary>
    /// Internal setup.
    /// </summary>
    void Initialize()
    {
      this.pageOrigin = XPoint.Empty;
      XMatrix matrix = (XMatrix)gfx.Transform;

      if (this.pageUnit != XGraphicsUnit.Point)
      {
        switch (this.pageUnit)
        {
          case XGraphicsUnit.Inch:
            matrix.Scale(XUnit.InchFactor);
            break;

          case XGraphicsUnit.Millimeter:
            matrix.Scale(XUnit.MillimeterFactor);
            break;

          case XGraphicsUnit.Centimeter:
            matrix.Scale(XUnit.CentimeterFactor);
            break;
        }
      }
      if (this.pageDirection == XPageDirection.Upwards)
        matrix.Multiply(new XMatrix(1, 0, 0, -1, 0, this.pageSize.height));

      this.defaultViewMatrix = matrix;
      this.transform = XMatrix.Identity;
    }

    /// <summary>
    /// Releases all resources used by this object.
    /// </summary>
    public void Dispose()
    {
      Dispose(true);
    }

    void Dispose(bool disposing)
    {
      if (!this.disposed)
      {
        this.disposed = true;
        if (disposing)
        {
          // Dispose managed resources.
        }

        if (this.form != null)
          this.form.Finish();

        // GDI+ requires this to disassociate it from metafiles
        this.gfx.Dispose();
        this.gfx = null;
        this.metafile = null;
        this.drawGraphics = false;

        if (this.renderer != null)
        {
          this.renderer.Close();
          this.renderer = null;
        }
      }
    }
    bool disposed;

    /// <summary>
    /// Internal hack for MigraDoc. Will be removed in further releases.
    /// Unicode support requires a global refactoring of MigraDoc and will be done in further releases.
    /// </summary>
    public PdfFontEncoding MUH  // MigraDoc Unicode Hack...
    {
      get { return this.muh; }
      set { this.muh = value; }
    }
    PdfFontEncoding muh;

    /// <summary>
    /// Internal hack for MigraDoc. Will be removed in further releases.
    /// Font embedding support requires a global refactoring of MigraDoc and will be done in further releases.
    /// </summary>
    public PdfFontEmbedding MFEH  // MigraDoc Font Embedding Hack...
    {
      get { return this.mfeh; }
      set { this.mfeh = value; }
    }
    PdfFontEmbedding mfeh;

    /// <summary>
    /// Gets or sets the unit of measure used for page coordinates.
    /// CURRENTLY ONLY POINT IS IMPLEMENTED.
    /// </summary>
    public XGraphicsUnit PageUnit
    {
      get { return this.pageUnit; }
      //set
      //{
      //  // TODO: other page units
      //  if (value != XGraphicsUnit.Point)
      //    throw new NotImplementedException("PageUnit must be XGraphicsUnit.Point in current implementation.");
      //}
    }
    XGraphicsUnit pageUnit;

    /// <summary>
    /// Gets or sets the a value indicating in which direction y-value grow.
    /// </summary>
    public XPageDirection PageDirection
    {
      get { return this.pageDirection; }
      set
      {
        //TODO
        if (value != XPageDirection.Downwards)
          throw new NotImplementedException("PageDirection must be XPageDirection.Downwards in current implementation.");
      }
    }
    XPageDirection pageDirection;

    /// <summary>
    /// Gets the current page origin. Setting the origin is not yet implemented.
    /// </summary>
    public XPoint PageOrigin
    {
      get { return this.pageOrigin; }
      set
      {
        //TODO
        if (value != XPoint.Empty)
          throw new NotImplementedException("PageOrigin cannot be modified in current implementation.");
      }
    }
    XPoint pageOrigin;

    /// <summary>
    /// Gets the current size of the page.
    /// </summary>
    public XSize PageSize
    {
      get { return this.pageSize; }
      //set
      //{
      //  //TODO
      //  throw new NotImplementedException("PageSize cannot be modified in current implementation.");
      //}
    }
    XSize pageSize;
    XSize pageSizePoints;

    //public void Flush();
    //public void Flush(FlushIntention intention);

    #region Drawing

    // ----- Clear --------------------------------------------------------------------------------

    /// <summary>
    /// Fills the entire drawing surface with the specified color. The functions works only if
    /// the current transformation is identity, i.e. the function should be called only immediately
    /// after the XGraphics object was created.
    /// </summary>
    public void Clear(XColor color)
    {
      if (this.drawGraphics)
        this.gfx.Clear(color.ToGdiColor());

      if (this.renderer != null)
        this.renderer.Clear(color);
    }

    // ----- DrawLine -----------------------------------------------------------------------------

    /// <summary>
    /// Draws a line connecting two Point structures.
    /// </summary>
    public void DrawLine(XPen pen, Point pt1, Point pt2)
    {
      DrawLine(pen, (double)pt1.X, (double)pt1.Y, (double)pt2.X, (double)pt2.Y);
    }

#if Gdip
    /// <summary>
    /// Draws a line connecting two PointF structures.
    /// </summary>
    public void DrawLine(XPen pen, PointF pt1, PointF pt2)
    {
      DrawLine(pen, pt1.X, pt1.Y, pt2.X, pt2.Y);
    }
#endif

    /// <summary>
    /// Draws a line connecting two XPoint structures.
    /// </summary>
    public void DrawLine(XPen pen, XPoint pt1, XPoint pt2)
    {
      DrawLine(pen, pt1.X, pt1.Y, pt2.X, pt2.Y);
    }

    /// <summary>
    /// Draws a line connecting the two points specified by coordinate pairs.
    /// </summary>
    public void DrawLine(XPen pen, int x1, int y1, int x2, int y2)
    {
      DrawLine(pen, (double)x1, (double)y1, (double)x2, (double)y2);
    }

    /// <summary>
    /// Draws a line connecting the two points specified by coordinate pairs.
    /// </summary>
    public void DrawLine(XPen pen, double x1, double y1, double x2, double y2)
    {
      if (pen == null)
        throw new ArgumentNullException("pen");

      if (this.drawGraphics)
        this.gfx.DrawLine(pen.RealizeGdiPen(), (float)x1, (float)y1, (float)x2, (float)y2);

      if (this.renderer != null)
        this.renderer.DrawLines(pen, new XPoint[2] { new XPoint(x1, y1), new XPoint(x2, y2) });
    }

    // ----- DrawLines ----------------------------------------------------------------------------

    /// <summary>
    /// Draws a series of line segments that connect an array of points.
    /// </summary>
    public void DrawLines(XPen pen, Point[] points)
    {
      DrawLines(pen, MakePointFArray(points));
    }

#if Gdip
    /// <summary>
    /// Draws a series of line segments that connect an array of points.
    /// </summary>
    public void DrawLines(XPen pen, PointF[] points)
    {
      if (pen == null)
        throw new ArgumentNullException("pen");
      if (points == null)
        throw new ArgumentNullException("points");
      if (points.Length < 2)
        throw new ArgumentException("points", PSSR.PointArrayAtLeast(2));

      if (this.drawGraphics)
        this.gfx.DrawLines(pen.RealizeGdiPen(), points);

      if (this.renderer != null)
        this.renderer.DrawLines(pen, MakeXPointArray(points));
    }
#endif

    /// <summary>
    /// Draws a series of line segments that connect an array of points.
    /// </summary>
    public void DrawLines(XPen pen, XPoint[] points)
    {
      if (pen == null)
        throw new ArgumentNullException("pen");
      if (points == null)
        throw new ArgumentNullException("points");
      if (points.Length < 2)
        throw new ArgumentException("points", PSSR.PointArrayAtLeast(2));

      if (this.drawGraphics)
        this.gfx.DrawLines(pen.RealizeGdiPen(), XGraphics.MakePointFArray(points));

      if (this.renderer != null)
        this.renderer.DrawLines(pen, points);
    }

    /// <summary>
    /// Draws a series of line segments that connect an array of x and y pairs.
    /// </summary>
    public void DrawLines(XPen pen, double x, double y, params double[] value)
    {
      if (pen == null)
        throw new ArgumentNullException("pen");
      if (value == null)
        throw new ArgumentNullException("value");

      int length = value.Length;
      XPoint[] points = new XPoint[length / 2 + 1];
      points[0].X = x;
      points[0].Y = y;
      for (int idx = 0; idx < length / 2; idx++)
      {
        points[idx + 1].X = value[2 * idx];
        points[idx + 1].Y = value[2 * idx + 1];
      }
      DrawLines(pen, points);
    }

    // ----- DrawBezier ---------------------------------------------------------------------------

    /// <summary>
    /// Draws a Bézier spline defined by four points.
    /// </summary>
    public void DrawBezier(XPen pen, Point pt1, Point pt2, Point pt3, Point pt4)
    {
      DrawBezier(pen, (double)pt1.X, (double)pt1.Y, (double)pt2.X, (double)pt2.Y,
        (double)pt3.X, (double)pt3.Y, (double)pt4.X, (double)pt4.Y);
    }

#if Gdip
    /// <summary>
    /// Draws a Bézier spline defined by four points.
    /// </summary>
    public void DrawBezier(XPen pen, PointF pt1, PointF pt2, PointF pt3, PointF pt4)
    {
      DrawBezier(pen, pt1.X, pt1.Y, pt2.X, pt2.Y, pt3.X, pt3.Y, pt4.X, pt4.Y);
    }
#endif

    /// <summary>
    /// Draws a Bézier spline defined by four points.
    /// </summary>
    public void DrawBezier(XPen pen, XPoint pt1, XPoint pt2, XPoint pt3, XPoint pt4)
    {
      DrawBezier(pen, pt1.X, pt1.Y, pt2.X, pt2.Y, pt3.X, pt3.Y, pt4.X, pt4.Y);
    }

    /// <summary>
    /// Draws a Bézier spline defined by four points.
    /// </summary>
    public void DrawBezier(XPen pen, double x1, double y1, double x2, double y2,
      double x3, double y3, double x4, double y4)
    {
      if (pen == null)
        throw new ArgumentNullException("pen");

      if (this.drawGraphics)
        this.gfx.DrawBezier(pen.RealizeGdiPen(), (float)x1, (float)y1, (float)x2, (float)y2, (float)x3, (float)y3, (float)x4, (float)y4);

      if (this.renderer != null)
        this.renderer.DrawBeziers(pen, new XPoint[4]{new XPoint(x1, y1), new XPoint(x2, y2), 
                                                     new XPoint(x3, y3), new XPoint(x4, y4)});
    }

    // ----- DrawBeziers --------------------------------------------------------------------------

    /// <summary>
    /// Draws a series of Bézier splines from an array of points.
    /// </summary>
    public void DrawBeziers(XPen pen, Point[] points)
    {
      DrawBeziers(pen, MakeXPointArray(points));
    }

#if Gdip
    /// <summary>
    /// Draws a series of Bézier splines from an array of points.
    /// </summary>
    public void DrawBeziers(XPen pen, PointF[] points)
    {
      DrawBeziers(pen, MakeXPointArray(points));
    }
#endif

    /// <summary>
    /// Draws a series of Bézier splines from an array of points.
    /// </summary>
    public void DrawBeziers(XPen pen, XPoint[] points)
    {
      if (pen == null)
        throw new ArgumentNullException("pen");

      int count = points.Length;
      if (count == 0)
        return;

      if ((count - 1) % 3 != 0)
        throw new ArgumentException("Invalid number of points for bezier curves. Number must fulfil 4+3n.", "points");

      if (this.drawGraphics)
        this.gfx.DrawBeziers(pen.RealizeGdiPen(), MakePointFArray(points));

      if (this.renderer != null)
        this.renderer.DrawBeziers(pen, points);
    }

    // ----- DrawCurve ----------------------------------------------------------------------------

    /// <summary>
    /// Draws a cardinal spline through a specified array of points.
    /// </summary>
    public void DrawCurve(XPen pen, Point[] points)
    {
      DrawCurve(pen, MakePointFArray(points), 0.5);
    }

#if Gdip
    /// <summary>
    /// Draws a cardinal spline through a specified array of points.
    /// </summary>
    public void DrawCurve(XPen pen, PointF[] points)
    {
      DrawCurve(pen, MakeXPointArray(points), 0.5);
    }
#endif

    /// <summary>
    /// Draws a cardinal spline through a specified array of points.
    /// </summary>
    public void DrawCurve(XPen pen, XPoint[] points)
    {
      DrawCurve(pen, points, 0.5);
    }

    /// <summary>
    /// Draws a cardinal spline through a specified array of points using a specified tension. 
    /// </summary>
    public void DrawCurve(XPen pen, Point[] points, double tension)
    {
      DrawCurve(pen, MakeXPointArray(points), tension);
    }

#if Gdip
    /// <summary>
    /// Draws a cardinal spline through a specified array of points using a specified tension. 
    /// </summary>
    public void DrawCurve(XPen pen, PointF[] points, double tension)
    {
      DrawCurve(pen, MakeXPointArray(points), tension);
    }
#endif

    /// <summary>
    /// Draws a cardinal spline through a specified array of points using a specified tension. 
    /// </summary>
    public void DrawCurve(XPen pen, XPoint[] points, double tension)
    {
      if (pen == null)
        throw new ArgumentNullException("pen");
      if (points == null)
        throw new ArgumentNullException("points");
      if (points.Length < 2)
        throw new ArgumentException("DrawCurve requires two or more points.", "points");

      if (this.drawGraphics)
        this.gfx.DrawCurve(pen.RealizeGdiPen(), MakePointFArray(points), (float)tension);

      if (this.renderer != null)
        this.renderer.DrawCurve(pen, points, tension);
    }

    // TODO:
    //public void DrawCurve(XPen pen, PointF[] points, int offset, int numberOfSegments);
    //public void DrawCurve(XPen pen, Point[] points, int offset, int numberOfSegments, double tension);
    //public void DrawCurve(XPen pen, PointF[] points, int offset, int numberOfSegments, double tension);

    // ----- DrawArc ------------------------------------------------------------------------------

#if Gdip
    /// <summary>
    /// Draws an arc representing a portion of an ellipse.
    /// </summary>
    public void DrawArc(XPen pen, Rectangle rect, double startAngle, double sweepAngle)
    {
      DrawArc(pen, (double)rect.X, (double)rect.Y, (double)rect.Width, (double)rect.Height, startAngle, sweepAngle);
    }
#endif

#if Gdip
    /// <summary>
    /// Draws an arc representing a portion of an ellipse.
    /// </summary>
    public void DrawArc(XPen pen, RectangleF rect, double startAngle, double sweepAngle)
    {
      DrawArc(pen, rect.X, rect.Y, rect.Width, rect.Height, startAngle, sweepAngle);
    }
#endif

    /// <summary>
    /// Draws an arc representing a portion of an ellipse.
    /// </summary>
    public void DrawArc(XPen pen, XRect rect, double startAngle, double sweepAngle)
    {
      DrawArc(pen, rect.X, rect.Y, rect.Width, rect.Height, startAngle, sweepAngle);
    }

    /// <summary>
    /// Draws an arc representing a portion of an ellipse.
    /// </summary>
    public void DrawArc(XPen pen, int x, int y, int width, int height, int startAngle, int sweepAngle)
    {
      DrawArc(pen, (double)x, (double)y, (double)width, (double)height, startAngle, sweepAngle);
    }

    /// <summary>
    /// Draws an arc representing a portion of an ellipse.
    /// </summary>
    public void DrawArc(XPen pen, double x, double y, double width, double height, double startAngle, double sweepAngle)
    {
      if (pen == null)
        throw new ArgumentNullException("pen");

      if (Math.Abs(sweepAngle) >= 360)
      {
        DrawEllipse(pen, x, y, width, height);
      }
      else
      {
        if (this.drawGraphics)
          this.gfx.DrawArc(pen.RealizeGdiPen(), (float)x, (float)y, (float)width, (float)height, (float)startAngle, (float)sweepAngle);

        if (this.renderer != null)
          this.renderer.DrawArc(pen, x, y, width, height, startAngle, sweepAngle);
      }
    }

    // ----- DrawRectangle ------------------------------------------------------------------------

    // ----- stroke -----

#if Gdip
    /// <summary>
    /// Draws a rectangle.
    /// </summary>
    public void DrawRectangle(XPen pen, Rectangle rect)
    {
      DrawRectangle(pen, (double)rect.X, (double)rect.Y, (double)rect.Width, (double)rect.Height);
    }
#endif

#if Gdip
    /// <summary>
    /// Draws a rectangle.
    /// </summary>
    public void DrawRectangle(XPen pen, RectangleF rect)
    {
      DrawRectangle(pen, rect.X, rect.Y, rect.Width, rect.Height);
    }
#endif

    /// <summary>
    /// Draws a rectangle.
    /// </summary>
    public void DrawRectangle(XPen pen, XRect rect)
    {
      DrawRectangle(pen, rect.X, rect.Y, rect.Width, rect.Height);
    }

    /// <summary>
    /// Draws a rectangle.
    /// </summary>
    public void DrawRectangle(XPen pen, int x, int y, int width, int height)
    {
      DrawRectangle(pen, (double)x, (double)y, (double)width, (double)height);
    }

    /// <summary>
    /// Draws a rectangle.
    /// </summary>
    public void DrawRectangle(XPen pen, double x, double y, double width, double height)
    {
      if (pen == null)
        throw new ArgumentNullException("pen");

      if (this.drawGraphics)
        this.gfx.DrawRectangle(pen.RealizeGdiPen(), (float)x, (float)y, (float)width, (float)height);

      if (this.renderer != null)
        this.renderer.DrawRectangle(pen, null, x, y, width, height);
    }

    // ----- fill -----

#if Gdip
    /// <summary>
    /// Draws a rectangle.
    /// </summary>
    public void DrawRectangle(XBrush brush, Rectangle rect)
    {
      DrawRectangle(brush, (double)rect.X, (double)rect.Y, (double)rect.Width, (double)rect.Height);
    }
#endif

#if Gdip
    /// <summary>
    /// Draws a rectangle.
    /// </summary>
    public void DrawRectangle(XBrush brush, RectangleF rect)
    {
      DrawRectangle(brush, rect.X, rect.Y, rect.Width, rect.Height);
    }
#endif

    /// <summary>
    /// Draws a rectangle.
    /// </summary>
    public void DrawRectangle(XBrush brush, XRect rect)
    {
      DrawRectangle(brush, rect.X, rect.Y, rect.Width, rect.Height);
    }

    /// <summary>
    /// Draws a rectangle.
    /// </summary>
    public void DrawRectangle(XBrush brush, int x, int y, int width, int height)
    {
      DrawRectangle(brush, (double)x, (double)y, (double)width, (double)height);
    }

    /// <summary>
    /// Draws a rectangle.
    /// </summary>
    public void DrawRectangle(XBrush brush, double x, double y, double width, double height)
    {
      if (brush == null)
        throw new ArgumentNullException("brush");

      if (this.drawGraphics)
        this.gfx.FillRectangle(brush.RealizeGdiBrush(), (float)x, (float)y, (float)width, (float)height);

      if (this.renderer != null)
        this.renderer.DrawRectangle(null, brush, x, y, width, height);
    }

    // ----- stroke and fill -----

#if Gdip
    /// <summary>
    /// Draws a rectangle.
    /// </summary>
    public void DrawRectangle(XPen pen, XBrush brush, Rectangle rect)
    {
      DrawRectangle(pen, brush, (double)rect.X, (double)rect.Y, (double)rect.Width, (double)rect.Height);
    }
#endif

#if Gdip
    /// <summary>
    /// Draws a rectangle.
    /// </summary>
    public void DrawRectangle(XPen pen, XBrush brush, RectangleF rect)
    {
      DrawRectangle(pen, brush, rect.X, rect.Y, rect.Width, rect.Height);
    }
#endif

    /// <summary>
    /// Draws a rectangle.
    /// </summary>
    public void DrawRectangle(XPen pen, XBrush brush, XRect rect)
    {
      DrawRectangle(pen, brush, rect.X, rect.Y, rect.Width, rect.Height);
    }

    /// <summary>
    /// Draws a rectangle.
    /// </summary>
    public void DrawRectangle(XPen pen, XBrush brush, int x, int y, int width, int height)
    {
      DrawRectangle(pen, brush, (double)x, (double)y, (double)width, (double)height);
    }

    /// <summary>
    /// Draws a rectangle.
    /// </summary>
    public void DrawRectangle(XPen pen, XBrush brush, double x, double y, double width, double height)
    {
      if (pen == null && brush == null)
        throw new ArgumentNullException("pen and brush", PSSR.NeedPenOrBrush);

      if (this.drawGraphics)
      {
        this.gfx.FillRectangle(brush.RealizeGdiBrush(), (float)x, (float)y, (float)width, (float)height);
        this.gfx.DrawRectangle(pen.RealizeGdiPen(), (float)x, (float)y, (float)width, (float)height);
      }
      if (this.renderer != null)
        this.renderer.DrawRectangle(pen, brush, x, y, width, height);
    }

    // ----- DrawRectangles -----------------------------------------------------------------------

    // ----- stroke -----

#if Gdip
    /// <summary>
    /// Draws a series of rectangles.
    /// </summary>
    public void DrawRectangles(XPen pen, Rectangle[] rectangles)
    {
      if (pen == null)
        throw new ArgumentNullException("pen");
      if (rectangles == null)
        throw new ArgumentNullException("rectangles");

      DrawRectangles(pen, null, rectangles);
    }
#endif

#if Gdip
    /// <summary>
    /// Draws a series of rectangles.
    /// </summary>
    public void DrawRectangles(XPen pen, RectangleF[] rectangles)
    {
      if (pen == null)
        throw new ArgumentNullException("pen");
      if (rectangles == null)
        throw new ArgumentNullException("rectangles");

      DrawRectangles(pen, null, rectangles);
    }
#endif

    /// <summary>
    /// Draws a series of rectangles.
    /// </summary>
    public void DrawRectangles(XPen pen, XRect[] rectangles)
    {
      if (pen == null)
        throw new ArgumentNullException("pen");
      if (rectangles == null)
        throw new ArgumentNullException("rectangles");

      DrawRectangles(pen, null, rectangles);
    }

    // ----- fill -----

#if Gdip
    /// <summary>
    /// Draws a series of rectangles.
    /// </summary>
    public void DrawRectangles(XBrush brush, Rectangle[] rectangles)
    {
      if (brush == null)
        throw new ArgumentNullException("brush");
      if (rectangles == null)
        throw new ArgumentNullException("rectangles");

      DrawRectangles(null, brush, rectangles);
    }
#endif

#if Gdip
    /// <summary>
    /// Draws a series of rectangles.
    /// </summary>
    public void DrawRectangles(XBrush brush, RectangleF[] rectangles)
    {
      if (brush == null)
        throw new ArgumentNullException("brush");
      if (rectangles == null)
        throw new ArgumentNullException("rectangles");

      DrawRectangles(null, brush, rectangles);
    }
#endif

    /// <summary>
    /// Draws a series of rectangles.
    /// </summary>
    public void DrawRectangles(XBrush brush, XRect[] rectangles)
    {
      if (brush == null)
        throw new ArgumentNullException("brush");
      if (rectangles == null)
        throw new ArgumentNullException("rectangles");

      DrawRectangles(null, brush, rectangles);
    }

    // ----- stroke and fill -----

#if Gdip
    /// <summary>
    /// Draws a series of rectangles.
    /// </summary>
    public void DrawRectangles(XPen pen, XBrush brush, Rectangle[] rectangles)
    {
      if (pen == null && brush == null)
        throw new ArgumentNullException("pen and brush", PSSR.NeedPenOrBrush);
      if (rectangles == null)
        throw new ArgumentNullException("rectangles");

      if (this.drawGraphics)
      {
        this.gfx.FillRectangles(brush.RealizeGdiBrush(), rectangles);
        this.gfx.DrawRectangles(pen.RealizeGdiPen(), rectangles);
      }
      if (this.renderer != null)
      {
        int count = rectangles.Length;
        for (int idx = 0; idx < count; idx++)
        {
          Rectangle rect = rectangles[idx];
          this.renderer.DrawRectangle(pen, brush, rect.X, rect.Y, rect.Width, rect.Height);
        }
      }
    }
#endif

#if Gdip
    /// <summary>
    /// Draws a series of rectangles.
    /// </summary>
    public void DrawRectangles(XPen pen, XBrush brush, RectangleF[] rectangles)
    {
      if (pen == null && brush == null)
        throw new ArgumentNullException("pen and brush", PSSR.NeedPenOrBrush);
      if (rectangles == null)
        throw new ArgumentNullException("rectangles");

      if (this.drawGraphics)
      {
        this.gfx.FillRectangles(brush.RealizeGdiBrush(), rectangles);
        this.gfx.DrawRectangles(pen.RealizeGdiPen(), rectangles);
      }
      if (this.renderer != null)
      {
        int count = rectangles.Length;
        for (int idx = 0; idx < count; idx++)
        {
          RectangleF rect = rectangles[idx];
          this.renderer.DrawRectangle(pen, brush, rect.X, rect.Y, rect.Width, rect.Height);
        }
      }
    }
#endif

    /// <summary>
    /// Draws a series of rectangles.
    /// </summary>
    public void DrawRectangles(XPen pen, XBrush brush, XRect[] rectangles)
    {
      if (pen == null && brush == null)
        throw new ArgumentNullException("pen and brush", PSSR.NeedPenOrBrush);
      if (rectangles == null)
        throw new ArgumentNullException("rectangles");

      int count = rectangles.Length;
      if (this.drawGraphics)
      {
        RectangleF[] rects = new RectangleF[count];
        for (int idx = 0; idx < count; idx++)
        {
          XRect rect = rectangles[idx];
          rects[idx] = new RectangleF((float)rect.X, (float)rect.Y, (float)rect.Width, (float)rect.Height);
        }
        this.gfx.FillRectangles(brush.RealizeGdiBrush(), rects);
        this.gfx.DrawRectangles(pen.RealizeGdiPen(), rects);
      }
      if (this.renderer != null)
      {
        for (int idx = 0; idx < count; idx++)
        {
          XRect rect = rectangles[idx];
          this.renderer.DrawRectangle(pen, brush, rect.X, rect.Y, rect.Width, rect.Height);
        }
      }
    }

    // ----- DrawRoundedRectangle -----------------------------------------------------------------

    // ----- stroke -----

#if Gdip
    /// <summary>
    /// Draws a rectangles with round corners.
    /// </summary>
    public void DrawRoundedRectangle(XPen pen, Rectangle rect, Size ellipseSize)
    {
      DrawRoundedRectangle(pen, (double)rect.X, (double)rect.Y, (double)rect.Width, (double)rect.Height,
        (double)ellipseSize.Width, (double)ellipseSize.Height);
    }
#endif

#if Gdip
    /// <summary>
    /// Draws a rectangles with round corners.
    /// </summary>
    public void DrawRoundedRectangle(XPen pen, RectangleF rect, SizeF ellipseSize)
    {
      DrawRoundedRectangle(pen, rect.X, rect.Y, rect.Width, rect.Height, ellipseSize.Width, ellipseSize.Height);
    }
#endif

    /// <summary>
    /// Draws a rectangles with round corners.
    /// </summary>
    public void DrawRoundedRectangle(XPen pen, XRect rect, XSize ellipseSize)
    {
      DrawRoundedRectangle(pen, rect.X, rect.Y, rect.Width, rect.Height, ellipseSize.Width, ellipseSize.Height);
    }

    /// <summary>
    /// Draws a rectangles with round corners.
    /// </summary>
    public void DrawRoundedRectangle(XPen pen, int x, int y, int width, int height, int ellipseWidth, int ellipseHeight)
    {
      DrawRoundedRectangle(pen, (double)x, (double)y, (double)width, (double)height, (double)ellipseWidth, (double)ellipseHeight);
    }

    /// <summary>
    /// Draws a rectangles with round corners.
    /// </summary>
    public void DrawRoundedRectangle(XPen pen, double x, double y, double width, double height, double ellipseWidth, double ellipseHeight)
    {
      if (pen == null)
        throw new ArgumentNullException("pen");

      DrawRoundedRectangle(pen, null, x, y, width, height, ellipseWidth, ellipseHeight);
    }

    // ----- fill -----

#if Gdip
    /// <summary>
    /// Draws a rectangles with round corners.
    /// </summary>
    public void DrawRoundedRectangle(XBrush brush, Rectangle rect, Size ellipseSize)
    {
      DrawRoundedRectangle(brush, (double)rect.X, (double)rect.Y, (double)rect.Width, (double)rect.Height,
        (double)ellipseSize.Width, (double)ellipseSize.Height);
    }
#endif

#if Gdip
    /// <summary>
    /// Draws a rectangles with round corners.
    /// </summary>
    public void DrawRoundedRectangle(XBrush brush, RectangleF rect, SizeF ellipseSize)
    {
      DrawRoundedRectangle(brush, rect.X, rect.Y, rect.Width, rect.Height, ellipseSize.Width, ellipseSize.Height);
    }
#endif

    /// <summary>
    /// Draws a rectangles with round corners.
    /// </summary>
    public void DrawRoundedRectangle(XBrush brush, XRect rect, XSize ellipseSize)
    {
      DrawRoundedRectangle(brush, rect.X, rect.Y, rect.Width, rect.Height, ellipseSize.Width, ellipseSize.Height);
    }

    /// <summary>
    /// Draws a rectangles with round corners.
    /// </summary>
    public void DrawRoundedRectangle(XBrush brush, int x, int y, int width, int height, int ellipseWidth, int ellipseHeight)
    {
      DrawRoundedRectangle(brush, (double)x, (double)y, (double)width, (double)height, (double)ellipseWidth, (double)ellipseHeight);
    }

    /// <summary>
    /// Draws a rectangles with round corners.
    /// </summary>
    public void DrawRoundedRectangle(XBrush brush, double x, double y, double width, double height, double ellipseWidth, double ellipseHeight)
    {
      if (brush == null)
        throw new ArgumentNullException("brush");

      DrawRoundedRectangle(null, brush, x, y, width, height, ellipseWidth, ellipseHeight);
    }

    // ----- stroke and fill -----

#if Gdip
    /// <summary>
    /// Draws a rectangles with round corners.
    /// </summary>
    public void DrawRoundedRectangle(XPen pen, XBrush brush, Rectangle rect, Size ellipseSize)
    {
      DrawRoundedRectangle(pen, brush, (double)rect.X, (double)rect.Y, (double)rect.Width, (double)rect.Height,
        (double)ellipseSize.Width, (double)ellipseSize.Height);
    }
#endif

#if Gdip
    /// <summary>
    /// Draws a rectangles with round corners.
    /// </summary>
    public void DrawRoundedRectangle(XPen pen, XBrush brush, RectangleF rect, SizeF ellipseSize)
    {
      DrawRoundedRectangle(pen, brush, rect.X, rect.Y, rect.Width, rect.Height, ellipseSize.Width, ellipseSize.Height);
    }
#endif

    /// <summary>
    /// Draws a rectangles with round corners.
    /// </summary>
    public void DrawRoundedRectangle(XPen pen, XBrush brush, XRect rect, XSize ellipseSize)
    {
      DrawRoundedRectangle(pen, brush, rect.X, rect.Y, rect.Width, rect.Height, ellipseSize.Width, ellipseSize.Height);
    }

    /// <summary>
    /// Draws a rectangles with round corners.
    /// </summary>
    public void DrawRoundedRectangle(XPen pen, XBrush brush, int x, int y, int width, int height, int ellipseWidth, int ellipseHeight)
    {
      DrawRoundedRectangle(pen, brush, (double)x, (double)y, (double)width, (double)height, (double)ellipseWidth, (double)ellipseHeight);
    }

    /// <summary>
    /// Draws a rectangles with round corners.
    /// </summary>
    public void DrawRoundedRectangle(XPen pen, XBrush brush, double x, double y, double width, double height,
      double ellipseWidth, double ellipseHeight)
    {
      if (pen == null && brush == null)
        throw new ArgumentNullException("pen and brush", PSSR.NeedPenOrBrush);


      if (this.drawGraphics)
      {
        XGraphicsPath path = new XGraphicsPath();
        path.AddRoundedRectangle(x, y, width, height, ellipseWidth, ellipseHeight);
        DrawPath(pen, brush, path);

        //GraphicsPath gdiPath = path.RealizeGdiPath();
        //if (brush != null)
        //  this.gfx.FillPath(brush.RealizeGdiBrush(), gdiPath);
        //if (pen != null)
        //  this.gfx.DrawPath(pen.RealizeGdiPen(), gdiPath);
      }
      if (this.renderer != null)
        this.renderer.DrawRoundedRectangle(pen, brush, x, y, width, height, ellipseWidth, ellipseHeight);
    }

    // ----- DrawEllipse --------------------------------------------------------------------------

    // ----- stroke -----

#if Gdip
    /// <summary>
    /// Draws an ellipse defined by a bounding rectangle.
    /// </summary>
    public void DrawEllipse(XPen pen, Rectangle rect)
    {
      DrawEllipse(pen, (double)rect.X, (double)rect.Y, (double)rect.Width, (double)rect.Height);
    }
#endif

#if Gdip
    /// <summary>
    /// Draws an ellipse defined by a bounding rectangle.
    /// </summary>
    public void DrawEllipse(XPen pen, RectangleF rect)
    {
      DrawEllipse(pen, rect.X, rect.Y, rect.Width, rect.Height);
    }
#endif

    /// <summary>
    /// Draws an ellipse defined by a bounding rectangle.
    /// </summary>
    public void DrawEllipse(XPen pen, XRect rect)
    {
      DrawEllipse(pen, rect.X, rect.Y, rect.Width, rect.Height);
    }

    /// <summary>
    /// Draws an ellipse defined by a bounding rectangle.
    /// </summary>
    public void DrawEllipse(XPen pen, int x, int y, int width, int height)
    {
      DrawEllipse(pen, (double)x, (double)y, (double)width, (double)height);
    }

    /// <summary>
    /// Draws an ellipse defined by a bounding rectangle.
    /// </summary>
    public void DrawEllipse(XPen pen, double x, double y, double width, double height)
    {
      if (pen == null)
        throw new ArgumentNullException("pen");

      // No DrawArc defined?
      if (this.drawGraphics)
        this.gfx.DrawArc(pen.RealizeGdiPen(), (float)x, (float)y, (float)width, (float)height, 0, 360);

      if (this.renderer != null)
        this.renderer.DrawEllipse(pen, null, x, y, width, height);
    }

    // ----- fill -----

#if Gdip
    /// <summary>
    /// Draws an ellipse defined by a bounding rectangle.
    /// </summary>
    public void DrawEllipse(XBrush brush, Rectangle rect)
    {
      DrawEllipse(brush, (double)rect.X, (double)rect.Y, (double)rect.Width, (double)rect.Height);
    }
#endif

#if Gdip
    /// <summary>
    /// Draws an ellipse defined by a bounding rectangle.
    /// </summary>
    public void DrawEllipse(XBrush brush, RectangleF rect)
    {
      DrawEllipse(brush, rect.X, rect.Y, rect.Width, rect.Height);
    }
#endif

    /// <summary>
    /// Draws an ellipse defined by a bounding rectangle.
    /// </summary>
    public void DrawEllipse(XBrush brush, XRect rect)
    {
      DrawEllipse(brush, rect.X, rect.Y, rect.Width, rect.Height);
    }

    /// <summary>
    /// Draws an ellipse defined by a bounding rectangle.
    /// </summary>
    public void DrawEllipse(XBrush brush, int x, int y, int width, int height)
    {
      DrawEllipse(brush, (double)x, (double)y, (double)width, (double)height);
    }

    /// <summary>
    /// Draws an ellipse defined by a bounding rectangle.
    /// </summary>
    public void DrawEllipse(XBrush brush, double x, double y, double width, double height)
    {
      if (brush == null)
        throw new ArgumentNullException("brush");

      if (this.drawGraphics)
        this.gfx.FillEllipse(brush.RealizeGdiBrush(), (float)x, (float)y, (float)width, (float)height);

      if (this.renderer != null)
        this.renderer.DrawEllipse(null, brush, x, y, width, height);
    }

    // ----- stroke and fill -----

#if Gdip
    /// <summary>
    /// Draws an ellipse defined by a bounding rectangle.
    /// </summary>
    public void DrawEllipse(XPen pen, XBrush brush, Rectangle rect)
    {
      DrawEllipse(pen, brush, (double)rect.X, (double)rect.Y, (double)rect.Width, (double)rect.Height);
    }
#endif

#if Gdip
    /// <summary>
    /// Draws an ellipse defined by a bounding rectangle.
    /// </summary>
    public void DrawEllipse(XPen pen, XBrush brush, RectangleF rect)
    {
      DrawEllipse(pen, brush, rect.X, rect.Y, rect.Width, rect.Height);
    }
#endif

    /// <summary>
    /// Draws an ellipse defined by a bounding rectangle.
    /// </summary>
    public void DrawEllipse(XPen pen, XBrush brush, XRect rect)
    {
      DrawEllipse(pen, brush, rect.X, rect.Y, rect.Width, rect.Height);
    }

    /// <summary>
    /// Draws an ellipse defined by a bounding rectangle.
    /// </summary>
    public void DrawEllipse(XPen pen, XBrush brush, int x, int y, int width, int height)
    {
      DrawEllipse(pen, brush, (double)x, (double)y, (double)width, (double)height);
    }

    /// <summary>
    /// Draws an ellipse defined by a bounding rectangle.
    /// </summary>
    public void DrawEllipse(XPen pen, XBrush brush, double x, double y, double width, double height)
    {
      if (pen == null && brush == null)
        throw new ArgumentNullException("pen and brush", PSSR.NeedPenOrBrush);

      if (this.drawGraphics)
      {
        this.gfx.FillEllipse(brush.RealizeGdiBrush(), (float)x, (float)y, (float)width, (float)height);
        this.gfx.DrawArc(pen.RealizeGdiPen(), (float)x, (float)y, (float)width, (float)height, 0, 360);
      }
      if (this.renderer != null)
        this.renderer.DrawEllipse(pen, brush, x, y, width, height);
    }

    // ----- DrawPolygon --------------------------------------------------------------------------

    // ----- stroke -----

    /// <summary>
    /// Draws a polygon defined by an array of points.
    /// </summary>
    public void DrawPolygon(XPen pen, Point[] points)
    {
      DrawPolygon(pen, MakePointFArray(points));
    }

#if Gdip
    /// <summary>
    /// Draws a polygon defined by an array of points.
    /// </summary>
    public void DrawPolygon(XPen pen, PointF[] points)
    {
      if (pen == null)
        throw new ArgumentNullException("pen");
      if (points == null)
        throw new ArgumentNullException("points");
      if (points.Length < 2)
        throw new ArgumentException("points", PSSR.PointArrayAtLeast(2));

      if (this.drawGraphics)
        this.gfx.DrawPolygon(pen.RealizeGdiPen(), points);

      if (this.renderer != null)
        this.renderer.DrawPolygon(pen, null, MakeXPointArray(points), XFillMode.Alternate);  // XFillMode is ignored
    }
#endif

    /// <summary>
    /// Draws a polygon defined by an array of points.
    /// </summary>
    public void DrawPolygon(XPen pen, XPoint[] points)
    {
      if (pen == null)
        throw new ArgumentNullException("pen");
      if (points == null)
        throw new ArgumentNullException("points");
      if (points.Length < 2)
        throw new ArgumentException("points", PSSR.PointArrayAtLeast(2));

      if (this.drawGraphics)
        this.gfx.DrawPolygon(pen.RealizeGdiPen(), MakePointFArray(points));

      if (this.renderer != null)
        this.renderer.DrawPolygon(pen, null, points, XFillMode.Alternate);  // XFillMode is ignored
    }

    // ----- fill -----

    /// <summary>
    /// Draws a polygon defined by an array of points.
    /// </summary>
    public void DrawPolygon(XBrush brush, Point[] points, XFillMode fillmode)
    {
      DrawPolygon(brush, MakePointFArray(points), fillmode);
    }

#if Gdip
    /// <summary>
    /// Draws a polygon defined by an array of points.
    /// </summary>
    public void DrawPolygon(XBrush brush, PointF[] points, XFillMode fillmode)
    {
      if (brush == null)
        throw new ArgumentNullException("brush");
      if (points == null)
        throw new ArgumentNullException("points");
      if (points.Length < 2)
        throw new ArgumentException("points", PSSR.PointArrayAtLeast(2));

      if (this.drawGraphics)
        this.gfx.FillPolygon(brush.RealizeGdiBrush(), points, (FillMode)fillmode);

      if (this.renderer != null)
        this.renderer.DrawPolygon(null, brush, MakeXPointArray(points), fillmode);
    }
#endif

    /// <summary>
    /// Draws a polygon defined by an array of points.
    /// </summary>
    public void DrawPolygon(XBrush brush, XPoint[] points, XFillMode fillmode)
    {
      if (brush == null)
        throw new ArgumentNullException("brush");
      if (points == null)
        throw new ArgumentNullException("points");
      if (points.Length < 2)
        throw new ArgumentException("points", PSSR.PointArrayAtLeast(2));

      if (this.drawGraphics)
        this.gfx.FillPolygon(brush.RealizeGdiBrush(), MakePointFArray(points), (FillMode)fillmode);

      if (this.renderer != null)
        this.renderer.DrawPolygon(null, brush, points, fillmode);
    }

    // ----- stroke and fill -----

    /// <summary>
    /// Draws a polygon defined by an array of points.
    /// </summary>
    public void DrawPolygon(XPen pen, XBrush brush, Point[] points, XFillMode fillmode)
    {
      DrawPolygon(pen, brush, MakePointFArray(points), fillmode);
    }

#if Gdip
    /// <summary>
    /// Draws a polygon defined by an array of points.
    /// </summary>
    public void DrawPolygon(XPen pen, XBrush brush, PointF[] points, XFillMode fillmode)
    {
      if (pen == null && brush == null)
        throw new ArgumentNullException("pen and brush", PSSR.NeedPenOrBrush);
      if (points == null)
        throw new ArgumentNullException("points");
      if (points.Length < 2)
        throw new ArgumentException("points", PSSR.PointArrayAtLeast(2));

      if (this.drawGraphics)
      {
        this.gfx.FillPolygon(brush.RealizeGdiBrush(), points, (FillMode)fillmode);
        this.gfx.DrawPolygon(pen.RealizeGdiPen(), points);
      }
      if (this.renderer != null)
        this.renderer.DrawPolygon(pen, brush, MakeXPointArray(points), fillmode);
    }
#endif

    /// <summary>
    /// Draws a polygon defined by an array of points.
    /// </summary>
    public void DrawPolygon(XPen pen, XBrush brush, XPoint[] points, XFillMode fillmode)
    {
      if (pen == null && brush == null)
        throw new ArgumentNullException("pen and brush", PSSR.NeedPenOrBrush);
      if (points == null)
        throw new ArgumentNullException("points");
      if (points.Length < 2)
        throw new ArgumentException("points", PSSR.PointArrayAtLeast(2));

      if (this.drawGraphics)
      {
        PointF[] pts = MakePointFArray(points);
        this.gfx.FillPolygon(brush.RealizeGdiBrush(), pts, (FillMode)fillmode);
        this.gfx.DrawPolygon(pen.RealizeGdiPen(), pts);
      }
      if (this.renderer != null)
        this.renderer.DrawPolygon(pen, brush, points, fillmode);
    }

    // ----- DrawPie ------------------------------------------------------------------------------

    // ----- stroke -----

#if Gdip
    /// <summary>
    /// Draws a pie defined by an ellipse.
    /// </summary>
    public void DrawPie(XPen pen, Rectangle rect, double startAngle, double sweepAngle)
    {
      DrawPie(pen, (double)rect.X, (double)rect.Y, (double)rect.Width, (double)rect.Height, startAngle, sweepAngle);
    }
#endif

#if Gdip
    /// <summary>
    /// Draws a pie defined by an ellipse.
    /// </summary>
    public void DrawPie(XPen pen, RectangleF rect, double startAngle, double sweepAngle)
    {
      DrawPie(pen, rect.X, rect.Y, rect.Width, rect.Height, startAngle, sweepAngle);
    }
#endif

    /// <summary>
    /// Draws a pie defined by an ellipse.
    /// </summary>
    public void DrawPie(XPen pen, XRect rect, double startAngle, double sweepAngle)
    {
      DrawPie(pen, rect.X, rect.Y, rect.Width, rect.Height, startAngle, sweepAngle);
    }

    /// <summary>
    /// Draws a pie defined by an ellipse.
    /// </summary>
    public void DrawPie(XPen pen, int x, int y, int width, int height, int startAngle, int sweepAngle)
    {
      DrawPie(pen, (double)x, (double)y, (double)width, (double)height, (double)startAngle, (double)sweepAngle);
    }

    /// <summary>
    /// Draws a pie defined by an ellipse.
    /// </summary>
    public void DrawPie(XPen pen, double x, double y, double width, double height, double startAngle, double sweepAngle)
    {
      if (pen == null)
        throw new ArgumentNullException("pen", PSSR.NeedPenOrBrush);

      if (this.drawGraphics)
        this.gfx.DrawPie(pen.RealizeGdiPen(), (float)x, (float)y, (float)width, (float)height, (float)startAngle, (float)sweepAngle);

      if (this.renderer != null)
        this.renderer.DrawPie(pen, null, x, y, width, height, startAngle, sweepAngle);
    }

    // ----- fill -----

#if Gdip
    /// <summary>
    /// Draws a pie defined by an ellipse.
    /// </summary>
    public void DrawPie(XBrush brush, Rectangle rect, double startAngle, double sweepAngle)
    {
      DrawPie(brush, (double)rect.X, (double)rect.Y, (double)rect.Width, (double)rect.Height, startAngle, sweepAngle);
    }
#endif

#if Gdip
    /// <summary>
    /// Draws a pie defined by an ellipse.
    /// </summary>
    public void DrawPie(XBrush brush, RectangleF rect, double startAngle, double sweepAngle)
    {
      DrawPie(brush, rect.X, rect.Y, rect.Width, rect.Height, startAngle, sweepAngle);
    }
#endif

    /// <summary>
    /// Draws a pie defined by an ellipse.
    /// </summary>
    public void DrawPie(XBrush brush, XRect rect, double startAngle, double sweepAngle)
    {
      DrawPie(brush, rect.X, rect.Y, rect.Width, rect.Height, startAngle, sweepAngle);
    }

    /// <summary>
    /// Draws a pie defined by an ellipse.
    /// </summary>
    public void DrawPie(XBrush brush, int x, int y, int width, int height, int startAngle, int sweepAngle)
    {
      DrawPie(brush, (double)x, (double)y, (double)width, (double)height, (double)startAngle, (double)sweepAngle);
    }

    /// <summary>
    /// Draws a pie defined by an ellipse.
    /// </summary>
    public void DrawPie(XBrush brush, double x, double y, double width, double height, double startAngle, double sweepAngle)
    {
      if (brush == null)
        throw new ArgumentNullException("brush", PSSR.NeedPenOrBrush);

      if (this.drawGraphics)
        this.gfx.FillPie(brush.RealizeGdiBrush(), (float)x, (float)y, (float)width, (float)height, (float)startAngle, (float)sweepAngle);

      if (this.renderer != null)
        this.renderer.DrawPie(null, brush, x, y, width, height, startAngle, sweepAngle);
    }

    // ----- stroke and fill -----

#if Gdip
    /// <summary>
    /// Draws a pie defined by an ellipse.
    /// </summary>
    public void DrawPie(XPen pen, XBrush brush, Rectangle rect, double startAngle, double sweepAngle)
    {
      DrawPie(pen, brush, (double)rect.X, (double)rect.Y, (double)rect.Width, (double)rect.Height, startAngle, sweepAngle);
    }
#endif

#if Gdip
    /// <summary>
    /// Draws a pie defined by an ellipse.
    /// </summary>
    public void DrawPie(XPen pen, XBrush brush, RectangleF rect, double startAngle, double sweepAngle)
    {
      DrawPie(pen, brush, rect.X, rect.Y, rect.Width, rect.Height, startAngle, sweepAngle);
    }
#endif

    /// <summary>
    /// Draws a pie defined by an ellipse.
    /// </summary>
    public void DrawPie(XPen pen, XBrush brush, XRect rect, double startAngle, double sweepAngle)
    {
      DrawPie(pen, brush, rect.X, rect.Y, rect.Width, rect.Height, startAngle, sweepAngle);
    }

    /// <summary>
    /// Draws a pie defined by an ellipse.
    /// </summary>
    public void DrawPie(XPen pen, XBrush brush, int x, int y, int width, int height, int startAngle, int sweepAngle)
    {
      DrawPie(pen, brush, (double)x, (double)y, (double)width, (double)height, (double)startAngle, (double)sweepAngle);
    }

    /// <summary>
    /// Draws a pie defined by an ellipse.
    /// </summary>
    public void DrawPie(XPen pen, XBrush brush, double x, double y, double width, double height, double startAngle, double sweepAngle)
    {
      if (pen == null)
        throw new ArgumentNullException("pen", PSSR.NeedPenOrBrush);
      if (brush == null)
        throw new ArgumentNullException("brush", PSSR.NeedPenOrBrush);

      if (this.drawGraphics)
      {
        this.gfx.FillPie(brush.RealizeGdiBrush(), (float)x, (float)y, (float)width, (float)height, (float)startAngle, (float)sweepAngle);
        this.gfx.DrawPie(pen.RealizeGdiPen(), (float)x, (float)y, (float)width, (float)height, (float)startAngle, (float)sweepAngle);
      }
      if (this.renderer != null)
        this.renderer.DrawPie(pen, brush, x, y, width, height, startAngle, sweepAngle);
    }

    // ----- DrawClosedCurve ----------------------------------------------------------------------

    // ----- stroke -----

    /// <summary>
    /// Draws a closed cardinal spline defined by an array of points.
    /// </summary>
    public void DrawClosedCurve(XPen pen, Point[] points)
    {
      DrawClosedCurve(pen, null, MakeXPointArray(points), XFillMode.Alternate, 0.5);
    }

#if Gdip
    /// <summary>
    /// Draws a closed cardinal spline defined by an array of points.
    /// </summary>
    public void DrawClosedCurve(XPen pen, PointF[] points)
    {
      DrawClosedCurve(pen, null, MakeXPointArray(points), XFillMode.Alternate, 0.5);
    }
#endif

    /// <summary>
    /// Draws a closed cardinal spline defined by an array of points.
    /// </summary>
    public void DrawClosedCurve(XPen pen, XPoint[] points)
    {
      DrawClosedCurve(pen, null, points, XFillMode.Alternate, 0.5);
    }

    /// <summary>
    /// Draws a closed cardinal spline defined by an array of points.
    /// </summary>
    public void DrawClosedCurve(XPen pen, Point[] points, double tension)
    {
      DrawClosedCurve(pen, null, MakeXPointArray(points), XFillMode.Alternate, tension);
    }

#if Gdip
    /// <summary>
    /// Draws a closed cardinal spline defined by an array of points.
    /// </summary>
    public void DrawClosedCurve(XPen pen, PointF[] points, double tension)
    {
      DrawClosedCurve(pen, null, MakeXPointArray(points), XFillMode.Alternate, tension);
    }
#endif

    /// <summary>
    /// Draws a closed cardinal spline defined by an array of points.
    /// </summary>
    public void DrawClosedCurve(XPen pen, XPoint[] points, double tension)
    {
      if (pen == null)
        throw new ArgumentNullException("pen");

      if (this.drawGraphics)
        this.gfx.DrawClosedCurve(pen.RealizeGdiPen(), MakePointFArray(points), (float)tension, FillMode.Alternate);

      if (this.renderer != null)
        this.renderer.DrawClosedCurve(pen, null, points, tension, XFillMode.Alternate);
    }

    // ----- fill -----

    /// <summary>
    /// Draws a closed cardinal spline defined by an array of points.
    /// </summary>
    public void DrawClosedCurve(XBrush brush, Point[] points)
    {
      DrawClosedCurve(null, brush, MakeXPointArray(points), XFillMode.Alternate, 0.5);
    }

#if Gdip
    /// <summary>
    /// Draws a closed cardinal spline defined by an array of points.
    /// </summary>
    public void DrawClosedCurve(XBrush brush, PointF[] points)
    {
      DrawClosedCurve(null, brush, MakeXPointArray(points), XFillMode.Alternate, 0.5);
    }
#endif

    /// <summary>
    /// Draws a closed cardinal spline defined by an array of points.
    /// </summary>
    public void DrawClosedCurve(XBrush brush, XPoint[] points)
    {
      DrawClosedCurve(null, brush, points, XFillMode.Alternate, 0.5);
    }

    /// <summary>
    /// Draws a closed cardinal spline defined by an array of points.
    /// </summary>
    public void DrawClosedCurve(XBrush brush, Point[] points, XFillMode fillmode)
    {
      DrawClosedCurve(null, brush, MakeXPointArray(points), fillmode, 0.5);
    }

#if Gdip
    /// <summary>
    /// Draws a closed cardinal spline defined by an array of points.
    /// </summary>
    public void DrawClosedCurve(XBrush brush, PointF[] points, XFillMode fillmode)
    {
      DrawClosedCurve(null, brush, MakeXPointArray(points), fillmode, 0.5);
    }
#endif

    /// <summary>
    /// Draws a closed cardinal spline defined by an array of points.
    /// </summary>
    public void DrawClosedCurve(XBrush brush, XPoint[] points, XFillMode fillmode)
    {
      DrawClosedCurve(null, brush, points, fillmode, 0.5);
    }

    /// <summary>
    /// Draws a closed cardinal spline defined by an array of points.
    /// </summary>
    public void DrawClosedCurve(XBrush brush, Point[] points, XFillMode fillmode, double tension)
    {
      DrawClosedCurve(null, brush, MakeXPointArray(points), fillmode, tension);
    }

#if Gdip
    /// <summary>
    /// Draws a closed cardinal spline defined by an array of points.
    /// </summary>
    public void DrawClosedCurve(XBrush brush, PointF[] points, XFillMode fillmode, double tension)
    {
      DrawClosedCurve(null, brush, MakeXPointArray(points), fillmode, tension);
    }
#endif

    /// <summary>
    /// Draws a closed cardinal spline defined by an array of points.
    /// </summary>
    public void DrawClosedCurve(XBrush brush, XPoint[] points, XFillMode fillmode, double tension)
    {
      if (brush == null)
        throw new ArgumentNullException("brush");

      if (this.drawGraphics)
        this.gfx.FillClosedCurve(brush.RealizeGdiBrush(), MakePointFArray(points), (FillMode)fillmode, (float)tension);

      if (this.renderer != null)
        this.renderer.DrawClosedCurve(null, brush, points, tension, fillmode);
    }

    // ----- stroke and fill -----

    /// <summary>
    /// Draws a closed cardinal spline defined by an array of points.
    /// </summary>
    public void DrawClosedCurve(XPen pen, XBrush brush, Point[] points)
    {
      DrawClosedCurve(pen, brush, MakeXPointArray(points), XFillMode.Alternate, 0.5);
    }

#if Gdip
    /// <summary>
    /// Draws a closed cardinal spline defined by an array of points.
    /// </summary>
    public void DrawClosedCurve(XPen pen, XBrush brush, PointF[] points)
    {
      DrawClosedCurve(pen, brush, MakeXPointArray(points), XFillMode.Alternate, 0.5);
    }
#endif

    /// <summary>
    /// Draws a closed cardinal spline defined by an array of points.
    /// </summary>
    public void DrawClosedCurve(XPen pen, XBrush brush, XPoint[] points)
    {
      DrawClosedCurve(pen, brush, points, XFillMode.Alternate, 0.5);
    }

    /// <summary>
    /// Draws a closed cardinal spline defined by an array of points.
    /// </summary>
    public void DrawClosedCurve(XPen pen, XBrush brush, Point[] points, XFillMode fillmode)
    {
      DrawClosedCurve(pen, brush, MakeXPointArray(points), fillmode, 0.5);
    }

#if Gdip
    /// <summary>
    /// Draws a closed cardinal spline defined by an array of points.
    /// </summary>
    public void DrawClosedCurve(XPen pen, XBrush brush, PointF[] points, XFillMode fillmode)
    {
      DrawClosedCurve(pen, brush, MakeXPointArray(points), fillmode, 0.5);
    }
#endif

    /// <summary>
    /// Draws a closed cardinal spline defined by an array of points.
    /// </summary>
    public void DrawClosedCurve(XPen pen, XBrush brush, XPoint[] points, XFillMode fillmode)
    {
      DrawClosedCurve(pen, brush, points, fillmode, 0.5);
    }

    /// <summary>
    /// Draws a closed cardinal spline defined by an array of points.
    /// </summary>
    public void DrawClosedCurve(XPen pen, XBrush brush, Point[] points, XFillMode fillmode, double tension)
    {
      DrawClosedCurve(pen, brush, MakeXPointArray(points), fillmode, tension);
    }

#if Gdip
    /// <summary>
    /// Draws a closed cardinal spline defined by an array of points.
    /// </summary>
    public void DrawClosedCurve(XPen pen, XBrush brush, PointF[] points, XFillMode fillmode, double tension)
    {
      DrawClosedCurve(pen, brush, MakeXPointArray(points), fillmode, tension);
    }
#endif

    /// <summary>
    /// Draws a closed cardinal spline defined by an array of points.
    /// </summary>
    public void DrawClosedCurve(XPen pen, XBrush brush, XPoint[] points, XFillMode fillmode, double tension)
    {
      if (pen == null && brush == null)
        throw new ArgumentNullException("pen and brush", PSSR.NeedPenOrBrush);

      if (this.drawGraphics)
      {
        this.gfx.FillClosedCurve(brush.RealizeGdiBrush(), MakePointFArray(points), (FillMode)fillmode, (float)tension);
        // The fillmode is not used by DrawClosedCurve
        this.gfx.DrawClosedCurve(pen.RealizeGdiPen(), MakePointFArray(points), (float)tension, (FillMode)fillmode);
      }

      if (this.renderer != null)
        this.renderer.DrawClosedCurve(pen, brush, points, tension, fillmode);
    }

    // ----- DrawPath -----------------------------------------------------------------------------

    // ----- stroke -----

    /// <summary>
    /// Draws a graphical path.
    /// </summary>
    public void DrawPath(XPen pen, XGraphicsPath path)
    {
      if (pen == null)
        throw new ArgumentNullException("pen");
      if (path == null)
        throw new ArgumentNullException("path");

      if (this.drawGraphics)
        this.gfx.DrawPath(pen.RealizeGdiPen(), path.RealizeGdiPath());

      if (this.renderer != null)
        this.renderer.DrawPath(pen, null, path);
    }

    // ----- fill -----

    /// <summary>
    /// Draws a graphical path.
    /// </summary>
    public void DrawPath(XBrush brush, XGraphicsPath path)
    {
      if (brush == null)
        throw new ArgumentNullException("brush");
      if (path == null)
        throw new ArgumentNullException("path");

      if (this.drawGraphics)
        this.gfx.FillPath(brush.RealizeGdiBrush(), path.RealizeGdiPath());

      if (this.renderer != null)
        this.renderer.DrawPath(null, brush, path);
    }

    // ----- stroke and fill -----

    /// <summary>
    /// Draws a graphical path.
    /// </summary>
    public void DrawPath(XPen pen, XBrush brush, XGraphicsPath path)
    {
      if (pen == null && brush == null)
        throw new ArgumentNullException("pen and brush", PSSR.NeedPenOrBrush);
      if (path == null)
        throw new ArgumentNullException("path");

      if (this.drawGraphics)
      {
        if (brush != null)
          this.gfx.FillPath(brush.RealizeGdiBrush(), path.RealizeGdiPath());
        if (pen != null)
          this.gfx.DrawPath(pen.RealizeGdiPen(), path.RealizeGdiPath());
      }
      if (this.renderer != null)
        this.renderer.DrawPath(pen, brush, path);
    }

    // ----- DrawString ---------------------------------------------------------------------------

#if Gdip
    /// <summary>
    /// Draws the specified text string.
    /// </summary>
    public void DrawString(string s, XFont font, XBrush brush, PointF point)
    {
      DrawString(s, font, brush, new XRect(point.X, point.Y, 0, 0), XStringFormat.Default);
    }
#endif

    /// <summary>
    /// Draws the specified text string.
    /// </summary>
    public void DrawString(string s, XFont font, XBrush brush, XPoint point)
    {
      DrawString(s, font, brush, new XRect(point.X, point.Y, 0, 0), XStringFormat.Default);
    }

#if Gdip
    /// <summary>
    /// Draws the specified text string.
    /// </summary>
    public void DrawString(string s, XFont font, XBrush brush, PointF point, XStringFormat format)
    {
      DrawString(s, font, brush, new XRect(point.X, point.Y, 0, 0), format);
    }
#endif

    /// <summary>
    /// Draws the specified text string.
    /// </summary>
    public void DrawString(string s, XFont font, XBrush brush, XPoint point, XStringFormat format)
    {
      DrawString(s, font, brush, new XRect(point.X, point.Y, 0, 0), format);
    }

    /// <summary>
    /// Draws the specified text string.
    /// </summary>
    public void DrawString(string s, XFont font, XBrush brush, double x, double y)
    {
      DrawString(s, font, brush, new XRect(x, y, 0, 0), XStringFormat.Default);
    }

    /// <summary>
    /// Draws the specified text string.
    /// </summary>
    public void DrawString(string s, XFont font, XBrush brush, double x, double y, XStringFormat format)
    {
      DrawString(s, font, brush, new XRect(x, y, 0, 0), format);
    }

#if Gdip
    /// <summary>
    /// Draws the specified text string.
    /// </summary>
    public void DrawString(string s, XFont font, XBrush brush, RectangleF layoutRectangle)
    {
      DrawString(s, font, brush, new XRect(layoutRectangle), XStringFormat.Default);
    }
#endif

    /// <summary>
    /// Draws the specified text string.
    /// </summary>
    public void DrawString(string s, XFont font, XBrush brush, XRect layoutRectangle)
    {
      DrawString(s, font, brush, layoutRectangle, XStringFormat.Default);
    }

#if Gdip
    /// <summary>
    /// Draws the specified text string.
    /// </summary>
    public void DrawString(string s, XFont font, XBrush brush, RectangleF layoutRectangle, XStringFormat format)
    {
      DrawString(s, font, brush, new XRect(layoutRectangle), format);
    }
#endif

    /// <summary>
    /// Draws the specified text string.
    /// </summary>
    public void DrawString(string s, XFont font, XBrush brush, XRect layoutRectangle, XStringFormat format)
    {
      if (s == null)
        throw new ArgumentNullException("s");
      if (font == null)
        throw new ArgumentNullException("font");
      if (brush == null)
        throw new ArgumentNullException("brush");

      if (format.LineAlignment == XLineAlignment.BaseLine && layoutRectangle.Height != 0)
        throw new InvalidOperationException("DrawString: With XLineAlignment.BaseLine the height of the layout rectangle must be 0.");

      if (s.Length == 0)
        return;

      if (format == null)
        format = XStringFormat.Default;

      if (this.drawGraphics)
      {
        RectangleF rect = layoutRectangle.ToRectangleF();
        if (format.LineAlignment == XLineAlignment.BaseLine)
        {
          // TODO optimze
          double lineSpace = font.GetHeight(this);
          int cellSpace = font.FontFamily.GetLineSpacing(font.Style);
          int cellAscent = font.FontFamily.GetCellAscent(font.Style);
          int cellDescent = font.FontFamily.GetCellDescent(font.Style);
          double cyAscent = lineSpace * cellAscent / cellSpace;
          cyAscent = lineSpace * font.cellAscent / font.cellSpace;
          rect.Offset(0, (float)-cyAscent);
        }
        this.gfx.DrawString(s, font.RealizeGdiFont(), brush.RealizeGdiBrush(), rect,
          format != null ? format.RealizeGdiStringFormat() : null);
      }

      if (this.renderer != null)
        this.renderer.DrawString(s, font, brush, layoutRectangle, format);
    }

    // ----- MeasureString ------------------------------------------------------------------------

    /// <summary>
    /// Measures the specified string when drawn with the specified font.
    /// </summary>
    public XSize MeasureString(string text, XFont font, XStringFormat stringFormat)
    {
      // TODO: Here comes a lot of code in the future: kerning etc...
      if (text == null)
        throw new ArgumentNullException("text");
      if (font == null)
        throw new ArgumentNullException("font");
      if (stringFormat == null)
        throw new ArgumentNullException("stringFormat");

      return XSize.FromSizeF(this.gfx.MeasureString(text, font.RealizeGdiFont(), new PointF(0, 0), stringFormat.RealizeGdiStringFormat()));
    }

    /// <summary>
    /// Measures the specified string when drawn with the specified font.
    /// </summary>
    public XSize MeasureString(string text, XFont font)
    {
      return MeasureString(text, font, XStringFormat.Default);
    }

    //public SizeF MeasureString(string text, XFont font, SizeF layoutArea);
    //public SizeF MeasureString(string text, XFont font, int width);
    //public SizeF MeasureString(string text, XFont font, PointF origin, XStringFormat stringFormat);
    //public SizeF MeasureString(string text, XFont font, SizeF layoutArea, XStringFormat stringFormat);
    //public SizeF MeasureString(string text, XFont font, int width, XStringFormat format);
    //public SizeF MeasureString(string text, XFont font, SizeF layoutArea, XStringFormat stringFormat, out int charactersFitted, out int linesFilled);

    // ----- DrawImage ----------------------------------------------------------------------------

    /// <summary>
    /// Draws the specified image.
    /// </summary>
    public void DrawImage(XImage image, Point point)
    {
      DrawImage(image, (double)point.X, (double)point.Y);
    }

#if Gdip
    /// <summary>
    /// Draws the specified image.
    /// </summary>
    public void DrawImage(XImage image, PointF point)
    {
      DrawImage(image, point.X, point.Y);
    }
#endif

    /// <summary>
    /// Draws the specified image.
    /// </summary>
    public void DrawImage(XImage image, XPoint point)
    {
      DrawImage(image, point.X, point.Y);
    }

    //TODO trapezoid transformation
    ////public void DrawImage(XImage image, Point[] destPoints);
    ////public void DrawImage(XImage image, PointF[] destPoints);
    ////public void DrawImage(XImage image, XPoint[] destPoints);

    /// <summary>
    /// Draws the specified image.
    /// </summary>
    public void DrawImage(XImage image, int x, int y)
    {
      DrawImage(image, (double)x, (double)y);
    }

    /// <summary>
    /// Draws the specified image.
    /// </summary>
    public void DrawImage(XImage image, double x, double y)
    {
      if (image == null)
        throw new ArgumentNullException("image");

      CheckXPdfFormConsistence(image);

      if (this.drawGraphics)
      {
        if (image.gdiImage != null)
        {
          InterpolationMode interpolationMode = InterpolationMode.Invalid;
          if (!image.Interpolate)
          {
            interpolationMode = gfx.InterpolationMode;
            gfx.InterpolationMode = InterpolationMode.NearestNeighbor;
          }

          this.gfx.DrawImage(image.gdiImage, (float)x, (float)y);

          if (!image.Interpolate)
            gfx.InterpolationMode = interpolationMode;
        }
        else
        {
          RectangleF rect = new RectangleF((float)x, (float)y, image.Width, image.Height);
          this.gfx.DrawRectangle(Pens.Red, (float)x, (float)y, image.Width, image.Height);
          this.gfx.DrawLine(Pens.Red, (float)x, (float)y, (float)(x + image.Width), (float)(y + image.Height));
          this.gfx.DrawLine(Pens.Red, (float)(x + image.Width), (float)y, (float)x, (float)(y + image.Height));
        }
      }

      if (this.renderer != null)
        this.renderer.DrawImage(image, x, y,
          image.Width * 72 / image.HorizontalResolution,
          image.Height * 72 / image.HorizontalResolution);
    }

#if Gdip
    /// <summary>
    /// Draws the specified image.
    /// </summary>
    public void DrawImage(XImage image, Rectangle rect)
    {
      DrawImage(image, (double)rect.X, (double)rect.Y, (double)rect.Width, (double)rect.Height);
    }
#endif

#if Gdip
    /// <summary>
    /// Draws the specified image.
    /// </summary>
    public void DrawImage(XImage image, RectangleF rect)
    {
      DrawImage(image, rect.X, rect.Y, rect.Width, rect.Height);
    }
#endif

    /// <summary>
    /// Draws the specified image.
    /// </summary>
    public void DrawImage(XImage image, XRect rect)
    {
      DrawImage(image, rect.X, rect.Y, rect.Width, rect.Height);
    }

    /// <summary>
    /// Draws the specified image.
    /// </summary>
    public void DrawImage(XImage image, int x, int y, int width, int height)
    {
      DrawImage(image, (double)x, (double)y, (double)width, (double)height);
    }

    /// <summary>
    /// Draws the specified image.
    /// </summary>
    public void DrawImage(XImage image, double x, double y, double width, double height)
    {
      if (image == null)
        throw new ArgumentNullException("image");

      CheckXPdfFormConsistence(image);

      if (this.drawGraphics)
      {
        if (image.gdiImage != null)
        {
          InterpolationMode interpolationMode = InterpolationMode.Invalid;
          if (!image.Interpolate)
          {
            interpolationMode = gfx.InterpolationMode;
            gfx.InterpolationMode = InterpolationMode.NearestNeighbor;
          }

          this.gfx.DrawImage(image.gdiImage, (float)x, (float)y, (float)width, (float)height);

          if (!image.Interpolate)
            gfx.InterpolationMode = interpolationMode;
        }
        else
        {
#if true
          XImage Placeholder = null;
          if (image is XPdfForm)
          {
            XPdfForm pf = image as XPdfForm;
            if (pf.PlaceHolder != null)
              Placeholder = pf.PlaceHolder;
          }
          if (Placeholder != null)
            this.gfx.DrawImage(Placeholder.gdiImage, (float)x, (float)y, (float)width, (float)height);
          else
          {
            RectangleF rect = new RectangleF((float)x, (float)y, (float)width, (float)height);
            this.gfx.DrawRectangle(Pens.Red, (float)x, (float)y, (float)width, (float)height);
            this.gfx.DrawLine(Pens.Red, (float)x, (float)y, (float)(x + rect.Width), (float)(y + rect.Height));
            this.gfx.DrawLine(Pens.Red, (float)(x + rect.Width), (float)y, (float)x, (float)(y + rect.Height));
          }
#else
          RectangleF rect = new RectangleF((float)x, (float)y, (float)width, (float)height);
          this.gfx.DrawRectangle(Pens.Red, (float)x, (float)y, (float)width, (float)height);
          this.gfx.DrawRectangle(Pens.Red, (float)x, (float)y, image.Width, image.Height);
          this.gfx.DrawLine(Pens.Red, (float)x, (float)y, (float)(x + image.Width), (float)(y + image.Height));
          this.gfx.DrawLine(Pens.Red, (float)(x + image.Width), (float)y, (float)x, (float)(y + image.Height));
#endif
        }
      }

      if (this.renderer != null)
        this.renderer.DrawImage(image, x, y, width, height);
    }

    //TODO trapezoid transformation
    //public void DrawImage(XImage image, Point[] destPoints, Rectangle srcRect, GraphicsUnit srcUnit);
    //public void DrawImage(XImage image, PointF[] destPoints, RectangleF srcRect, GraphicsUnit srcUnit);
    //public void DrawImage(XImage image, XPoint[] destPoints, XRect srcRect, GraphicsUnit srcUnit);

    // TODO: calculate destination size
    //public void DrawImage(XImage image, int x, int y, Rectangle srcRect, XGraphicsUnit srcUnit)
    //public void DrawImage(XImage image, double x, double y, RectangleF srcRect, XGraphicsUnit srcUnit)
    //public void DrawImage(XImage image, double x, double y, XRect srcRect, XGraphicsUnit srcUnit)

#if Gdip
    /// <summary>
    /// Draws the specified image.
    /// </summary>
    public void DrawImage(XImage image, Rectangle destRect, Rectangle srcRect, XGraphicsUnit srcUnit)
    {
      XRect destRectX = new XRect(destRect.X, destRect.Y, destRect.Width, destRect.Height);
      XRect srcRectX = new XRect(srcRect.X, srcRect.Y, srcRect.Width, srcRect.Height);
      DrawImage(image, destRectX, srcRectX, srcUnit);
    }
#endif

#if Gdip
    /// <summary>
    /// Draws the specified image.
    /// </summary>
    public void DrawImage(XImage image, RectangleF destRect, RectangleF srcRect, XGraphicsUnit srcUnit)
    {
      XRect destRectX = new XRect(destRect.X, destRect.Y, destRect.Width, destRect.Height);
      XRect srcRectX = new XRect(srcRect.X, srcRect.Y, srcRect.Width, srcRect.Height);
      DrawImage(image, destRectX, srcRectX, srcUnit);
    }
#endif

    /// <summary>
    /// Draws the specified image.
    /// </summary>
    public void DrawImage(XImage image, XRect destRect, XRect srcRect, XGraphicsUnit srcUnit)
    {
      if (image == null)
        throw new ArgumentNullException("image");

      CheckXPdfFormConsistence(image);

      if (this.drawGraphics)
      {
        if (image.gdiImage != null)
        {
          InterpolationMode interpolationMode = InterpolationMode.Invalid;
          if (!image.Interpolate)
          {
            interpolationMode = gfx.InterpolationMode;
            gfx.InterpolationMode = InterpolationMode.NearestNeighbor;
          }

          RectangleF destRectF = new RectangleF((float)destRect.X, (float)destRect.Y, (float)destRect.Width, (float)destRect.Height);
          RectangleF srcRectF = new RectangleF((float)srcRect.X, (float)srcRect.Y, (float)srcRect.Width, (float)srcRect.Height);
          this.gfx.DrawImage(image.gdiImage, destRectF, srcRectF, GraphicsUnit.Pixel);

          if (!image.Interpolate)
            gfx.InterpolationMode = interpolationMode;
        }
        else
        {
          this.gfx.DrawRectangle(Pens.Red, (float)destRect.X, (float)destRect.Y,
            (float)destRect.Width, (float)destRect.Height);
          this.gfx.DrawLine(Pens.Red, (float)destRect.X, (float)destRect.Y, (float)(destRect.X + destRect.Width), (float)(destRect.Y + destRect.Height));
          this.gfx.DrawLine(Pens.Red, (float)(destRect.X + destRect.Width), (float)destRect.Y, (float)destRect.X, (float)(destRect.Y + destRect.Height));
        }
      }

      if (this.renderer != null)
        this.renderer.DrawImage(image, destRect, srcRect, srcUnit);
    }

    //TODO?
    //public void DrawImage(XImage image, Rectangle destRect, int srcX, int srcY, int srcWidth, int srcHeight, GraphicsUnit srcUnit);
    //public void DrawImage(XImage image, Rectangle destRect, double srcX, double srcY, double srcWidth, double srcHeight, GraphicsUnit srcUnit);
    //public void DrawImage(XImage image, Rectangle destRect, double srcX, double srcY, double srcWidth, double srcHeight, GraphicsUnit srcUnit);

    /// <summary>
    /// Checks whether drawing is allowed and disposes the XGraphics object, if neccessary.
    /// </summary>
    void CheckXPdfFormConsistence(XImage image)
    {
      XForm form = image as XForm;
      XGraphicsPdfRenderer renderer;
      if (form != null)
      {
        // Force disposing of XGraphics that draws the content
        form.Finish();

        if (this.renderer != null && (renderer = this.renderer as XGraphicsPdfRenderer) != null)
        {
          if (form.Owner != null && form.Owner != ((XGraphicsPdfRenderer)this.renderer).Owner)
            throw new InvalidOperationException(
              "A XPdfForm objects is bound to the document it was created for and cannot be drawn in the context of another document.");

          if (form == ((XGraphicsPdfRenderer)this.renderer).form)
            throw new InvalidOperationException(
              "A XPdfForm cannot be drawn on itself.");
        }
      }
    }

    // ----- DrawBarCode --------------------------------------------------------------------------

    /// <summary>
    /// Draws the specified bar code.
    /// </summary>
    public void DrawBarCode(BarCodes.BarCode barcode, XPoint position)
    {
      barcode.Render(this, XBrushes.Black, null, position);
    }

    /// <summary>
    /// Draws the specified bar code.
    /// </summary>
    public void DrawBarCode(BarCodes.BarCode barcode, XBrush brush, XPoint position)
    {
      barcode.Render(this, brush, null, position);
    }

    /// <summary>
    /// Draws the specified bar code.
    /// </summary>
    public void DrawBarCode(BarCodes.BarCode barcode, XBrush brush, XFont font, XPoint position)
    {
      barcode.Render(this, brush, font, position);
    }

    // ----- DrawMatrixCode -----------------------------------------------------------------------

    /// <summary>
    /// Draws the specified data matrix code.
    /// </summary>
    public void DrawMatrixCode(BarCodes.MatrixCode matrixcode, XPoint position)
    {
      matrixcode.Render(this, XBrushes.Black, position);
    }

    /// <summary>
    /// Draws the specified data matrix code.
    /// </summary>
    public void DrawMatrixCode(BarCodes.MatrixCode matrixcode, XBrush brush, XPoint position)
    {
      matrixcode.Render(this, brush, position);
    }

    // ----- DrawGrit -----------------------------------------------------------------------------

    //[Conditional("DEBUG")]
    //public void DrawGridlines(XPoint origin, XPen majorpen, double majordelta, XPen minorpen, double minordelta)
    //{
    //  RectangleF box = new RectangleF(0, 0, 600, 850);
    //  DrawGridline(origin, minorpen, minordelta, box);
    //  DrawGridline(origin, majorpen, majordelta, box);
    //  /*
    //        float xmin = -10000f, ymin = -10000f, xmax = 10000f, ymax = 10000f;
    //        float x, y;
    //        x = origin.X;
    //        while (x < xmax)
    //        {
    //          DrawLine(majorpen, x, ymin, x, ymax);
    //          x += majordelta;
    //        }
    //        x = origin.X - majordelta;
    //        while (x > xmin)
    //        {
    //          DrawLine(majorpen, x, ymin, x, ymax);
    //          x -= majordelta;
    //        }
    //        y = origin.Y;
    //        while (y < ymax)
    //        {
    //          DrawLine(majorpen, xmin, y, xmax, y);
    //          y += majordelta;
    //        }
    //        y = origin.Y - majordelta;
    //        while (y > ymin)
    //        {
    //          DrawLine(majorpen, xmin, y, xmax, y);
    //          y -= majordelta;
    //        }
    //   */
    //}

    //[Conditional("DEBUG")]
    //void DrawGridline(XPoint origin, XPen pen, double delta, XRect box)
    //{
    //  double xmin = box.X, ymin = box.Y, xmax = box.X + box.Width, ymax = box.Y + box.Height;
    //  double x, y;
    //  y = origin.Y;
    //  while (y < ymax)
    //  {
    //    DrawLine(pen, xmin, y, xmax, y);
    //    y += delta;
    //  }
    //  y = origin.Y - delta;
    //  while (y > ymin)
    //  {
    //    DrawLine(pen, xmin, y, xmax, y);
    //    y -= delta;
    //  }
    //  x = origin.X;
    //  while (x < xmax)
    //  {
    //    DrawLine(pen, x, ymin, x, ymax);
    //    x += delta;
    //  }
    //  x = origin.X - delta;
    //  while (x > xmin)
    //  {
    //    DrawLine(pen, x, ymin, x, ymax);
    //    x -= delta;
    //  }
    //}
    #endregion

    // --------------------------------------------------------------------------------------------

    #region Save and Restore

    /// <summary>
    /// Saves the current state of this XGraphics object and identifies the saved state with the
    /// returned XGraphicsState object.
    /// </summary>
    public XGraphicsState Save()
    {
      XGraphicsState xState = new XGraphicsState(this.gfx.Save());
      InternalGraphicsState iState = new InternalGraphicsState(xState);
      iState.Transform = this.transform;

      this.gsStack.Push(iState);

      if (this.renderer != null)
        this.renderer.Save(xState);

      return xState;
    }

    /// <summary>
    /// Restores the state of this XGraphics object to the state represented by the specified 
    /// XGraphicsState object.
    /// </summary>
    public void Restore(XGraphicsState state)
    {
      if (state == null)
        throw new ArgumentNullException("state");

      this.gsStack.Restore(state.InternalState);

      this.gfx.Restore(state.GdipState);
      this.transform = state.InternalState.Transform;

      if (this.renderer != null)
        this.renderer.Restore(state);
    }

    /// <summary>
    /// Saves a graphics container with the current state of this XGraphics and 
    /// opens and uses a new graphics container.
    /// </summary>
    public XGraphicsContainer BeginContainer()
    {
      return BeginContainer(new RectangleF(0, 0, 1, 1), new RectangleF(0, 0, 1, 1), XGraphicsUnit.Point);
    }

#if Gdip
    /// <summary>
    /// Saves a graphics container with the current state of this XGraphics and 
    /// opens and uses a new graphics container.
    /// </summary>
    public XGraphicsContainer BeginContainer(Rectangle dstrect, Rectangle srcrect, XGraphicsUnit unit)
    {
      return BeginContainer(new XRect(dstrect), new XRect(dstrect), unit);
    }
#endif

#if Gdip
    /// <summary>
    /// Saves a graphics container with the current state of this XGraphics and 
    /// opens and uses a new graphics container.
    /// </summary>
    public XGraphicsContainer BeginContainer(RectangleF dstrect, RectangleF srcrect, XGraphicsUnit unit)
    {
      return BeginContainer(new XRect(dstrect), new XRect(dstrect), unit);
    }
#endif

    /// <summary>
    /// Saves a graphics container with the current state of this XGraphics and 
    /// opens and uses a new graphics container.
    /// </summary>
    public XGraphicsContainer BeginContainer(XRect dstrect, XRect srcrect, XGraphicsUnit unit)
    {
      // TODO: unit
      if (unit != XGraphicsUnit.Point)
        throw new ArgumentException("The current implementation supports XGraphicsUnit.Point only.", "unit");
#if true
      XGraphicsContainer xContainer = new XGraphicsContainer(this.gfx.Save());
      InternalGraphicsState iState = new InternalGraphicsState(xContainer);
      iState.Transform = this.transform;

      this.gsStack.Push(iState);

      if (this.renderer != null)
        this.renderer.BeginContainer(xContainer, dstrect, srcrect, unit);

      XMatrix matrix = XMatrix.Identity;
#if true
      double scaleX = dstrect.Width / srcrect.Width;
      double scaleY = dstrect.Height / srcrect.Height;
      matrix.Translate(-srcrect.X, -srcrect.Y);
      matrix.Scale(scaleX, scaleY);
      matrix.Translate(dstrect.X / scaleX, dstrect.Y / scaleY);
#else
      matrix.Translate(-dstrect.X, -dstrect.Y);
      matrix.Scale(dstrect.Width / srcrect.Width, dstrect.Height / srcrect.Height);
      matrix.Translate(srcrect.X, srcrect.Y);
#endif
      Transform = matrix;

      return xContainer;
#else
      XGraphicsContainer container = new XGraphicsContainer(this.gfx.BeginContainer(dstrect, srcrect, this.gfx.PageUnit));
      container.Transform = this.transform;

      if (this.renderer != null)
        this.renderer.BeginContainer(container, dstrect, srcrect, unit);

      return container;
#endif
    }

    /// <summary>
    /// Closes the current graphics container and restores the state of this XGraphics 
    /// to the state saved by a call to the BeginContainer method.
    /// </summary>
    public void EndContainer(XGraphicsContainer container)
    {
      if (container == null)
        throw new ArgumentNullException("container");

      this.gsStack.Restore(container.InternalState);

      //this.gfx.EndContainer(container.GdipState);
      this.gfx.Restore(container.GdipState);
      this.transform = container.InternalState.Transform;

      if (this.renderer != null)
        this.renderer.EndContainer(container);
    }

    /// <summary>
    /// Gets the current graphics state level. The default value is 0. Each call of Save or BeginContainer
    /// increased and each call of Restore or EndContainer decreased the value by 1.
    /// </summary>
    public int GraphicsStateLevel
    {
      get { return this.gsStack.Count; }
    }

    #endregion

    // --------------------------------------------------------------------------------------------

    #region Properties

    /// <summary>
    /// Gets or sets the smoothing mode.
    /// </summary>
    /// <value>The smoothing mode.</value>
    public XSmoothingMode SmoothingMode
    {
      get { return (XSmoothingMode)this.gfx.SmoothingMode; }
      set { this.gfx.SmoothingMode = (SmoothingMode)value; }
    }

    //public Region Clip { get; set; }
    //public RectangleF ClipBounds { get; }
    //public CompositingMode CompositingMode { get; set; }
    //public CompositingQuality CompositingQuality { get; set; }
    //public float DpiX { get; }
    //public float DpiY { get; }
    //public InterpolationMode InterpolationMode { get; set; }
    //public bool IsClipEmpty { get; }
    //public bool IsVisibleClipEmpty { get; }
    //public float PageScale { get; set; }
    //public GraphicsUnit PageUnit { get; set; }
    //public PixelOffsetMode PixelOffsetMode { get; set; }
    //public Point RenderingOrigin { get; set; }
    //public SmoothingMode SmoothingMode { get; set; }
    //public int TextContrast { get; set; }
    //public TextRenderingHint TextRenderingHint { get; set; }
    //public Matrix Transform { get; set; }
    //public RectangleF VisibleClipBounds { get; }

    #endregion

    // --------------------------------------------------------------------------------------------

    #region Transformation

    /// <summary>
    /// Applies the specified translation operation to the transformation matrix of this object by 
    /// prepending it to the object's transformation matrix.
    /// </summary>
    public void TranslateTransform(double dx, double dy)
    {
      TranslateTransform(dx, dy, XMatrixOrder.Prepend);
    }

    /// <summary>
    /// Applies the specified translation operation to the transformation matrix of this object
    /// in the specified order.
    /// </summary>
    public void TranslateTransform(double dx, double dy, XMatrixOrder order)
    {
      XMatrix matrix = this.transform;
      matrix.Translate(dx, dy, order);
      Transform = matrix;
    }

    /// <summary>
    /// Applies the specified scaling operation to the transformation matrix of this object by 
    /// prepending it to the object's transformation matrix.
    /// </summary>
    public void ScaleTransform(double scaleX, double scaleY)
    {
      ScaleTransform(scaleX, scaleY, XMatrixOrder.Prepend);
    }

    /// <summary>
    /// Applies the specified scaling operation to the transformation matrix of this object
    /// in the specified order.
    /// </summary>
    public void ScaleTransform(double scaleX, double scaleY, XMatrixOrder order)
    {
      XMatrix matrix = this.transform;
      matrix.Scale(scaleX, scaleY, order);
      Transform = matrix;
    }

    /// <summary>
    /// Applies the specified scaling operation to the transformation matrix of this object by 
    /// prepending it to the object's transformation matrix.
    /// </summary>
    public void ScaleTransform(double scaleXY)
    {
      ScaleTransform(scaleXY, scaleXY, XMatrixOrder.Prepend);
    }

    /// <summary>
    /// Applies the specified scaling operation to the transformation matrix of this object
    /// in the specified order.
    /// </summary>
    public void ScaleTransform(double scaleXY, XMatrixOrder order)
    {
      XMatrix matrix = this.transform;
      matrix.Scale(scaleXY, scaleXY, order);
      Transform = matrix;
    }

    /// <summary>
    /// Applies the specified rotation operation to the transformation matrix of this object by 
    /// prepending it to the object's transformation matrix.
    /// </summary>
    public void RotateTransform(double angle)
    {
      RotateTransform(angle, XMatrixOrder.Prepend);
    }

    /// <summary>
    /// Applies the specified rotation operation to the transformation matrix of this object
    /// in the specified order. The angle unit of measure is degree.
    /// </summary>
    public void RotateTransform(double angle, XMatrixOrder order)
    {
      XMatrix matrix = this.transform;
      matrix.Rotate(angle, order);
      Transform = matrix;
    }

    /// <summary>
    /// Applies the specified rotation operation to the transformation matrix of this object by 
    /// prepending it to the object's transformation matrix.
    /// </summary>
    public void RotateAtTransform(double angle, XPoint point)
    {
      RotateAtTransform(angle, point, XMatrixOrder.Prepend);
    }

    /// <summary>
    /// Applies the specified rotation operation to the transformation matrix of this object by 
    /// prepending it to the object's transformation matrix.
    /// </summary>
    public void RotateAtTransform(double angle, XPoint point, XMatrixOrder order)
    {
      XMatrix matrix = this.transform;
      matrix.RotateAt(angle, point, order);
      Transform = matrix;
    }

    /// <summary>
    /// Multiplies the transformation matrix of this object and specified matrix.
    /// </summary>
    public void MultiplyTransform(XMatrix matrix)
    {
      MultiplyTransform(matrix, XMatrixOrder.Prepend);
    }

    /// <summary>
    /// Multiplies the transformation matrix of this object and specified matrix in the specified order.
    /// </summary>
    public void MultiplyTransform(XMatrix matrix, XMatrixOrder order)
    {
      XMatrix matrix2 = this.transform;
      matrix2.Multiply(matrix, order);
      Transform = matrix2;
    }

    /// <summary>
    /// Gets or sets the transformation matrix.
    /// </summary>
    public XMatrix Transform
    {
      get { return this.transform; }
      set
      {
        if (!this.transform.Equals(value))
        {
          this.transform = value;
          Matrix matrix = (Matrix)this.defaultViewMatrix;
          matrix.Multiply(value.ToMatrix());
          this.gfx.Transform = matrix;

          if (this.renderer != null)
            this.renderer.Transform = value;
        }
      }
    }

    /// <summary>
    /// Resets the transformation matrix of this object to the identity matrix.
    /// </summary>
    public void ResetTransform()
    {
      if (!this.transform.IsIdentity)
      {
        this.transform = XMatrix.Identity;
        this.gfx.Transform = (Matrix)this.defaultViewMatrix;
        if (this.renderer != null)
          this.renderer.Transform = this.transform;
      }
    }

    //public void TransformPoints(CoordinateSpace destSpace, CoordinateSpace srcSpace, Point[] points)
    //{
    //}
    //
    //public void TransformPoints(CoordinateSpace destSpace, CoordinateSpace srcSpace, PointF[] points)
    //{
    //}

    #endregion

    // --------------------------------------------------------------------------------------------

    #region Clipping

#if Gdip
    /// <summary>
    /// Sets the clipping region to the specified rectangle.
    /// </summary>
    public void SetClip(Rectangle rect)
    {
      XGraphicsPath path = new XGraphicsPath();
      path.AddRectangle(rect);
      SetClip(path, XCombineMode.Replace);
    }
#endif

#if Gdip
    /// <summary>
    /// Sets the clipping region to the specified rectangle.
    /// </summary>
    public void SetClip(RectangleF rect)
    {
      XGraphicsPath path = new XGraphicsPath();
      path.AddRectangle(rect);
      SetClip(path, XCombineMode.Replace);
    }
#endif

    /// <summary>
    /// Sets the clipping region to the specified rectangle.
    /// </summary>
    public void SetClip(XRect rect)
    {
      XGraphicsPath path = new XGraphicsPath();
      path.AddRectangle(rect);
      SetClip(path, XCombineMode.Replace);
    }

    /// <summary>
    /// Sets the clipping region to the specified graphical path.
    /// </summary>
    public void SetClip(XGraphicsPath path)
    {
      SetClip(path, XCombineMode.Replace);
    }

    /// <summary>
    /// Sets the clipping region to the specified graphical path.
    /// </summary>
    public void SetClip(XRect rect, XCombineMode combineMode)
    {
      XGraphicsPath path = new XGraphicsPath();
      path.AddRectangle(rect);
      SetClip(path, combineMode);
    }

    /// <summary>
    /// Sets the clipping region to the specified graphical path.
    /// </summary>
    public void SetClip(XGraphicsPath path, XCombineMode combineMode)
    {
      if (path == null)
        throw new ArgumentNullException("path");

      if (combineMode != XCombineMode.Replace && combineMode != XCombineMode.Intersect)
        throw new ArgumentException("Only XCombineMode.Replace and XCombineMode.Intersect are currently supported by PDFsharp.", "combineMode");

      if (this.drawGraphics)
        this.gfx.SetClip(path.RealizeGdiPath(), (CombineMode)combineMode);

      if (this.renderer != null)
        this.renderer.SetClip(path, combineMode);
    }

#if nyi
    public void ExcludeClip(Rectangle rect)
    {
      throw new NotImplementedException("ExcludeClip");
    }

    public void ExcludeClip(RectangleF rect)
    {
      throw new NotImplementedException("ExcludeClip");
    }

    public void ExcludeClip(XRect rect)
    {
      throw new NotImplementedException("ExcludeClip");
    }
#endif

#if Gdip
    /// <summary>
    /// Updates the clip region of this XGraphics to the intersection of the 
    /// current clip region and the specified rectangle.
    /// </summary>
    public void IntersectClip(Rectangle rect)
    {
      XGraphicsPath path = new XGraphicsPath();
      path.AddRectangle(rect);
      SetClip(path, XCombineMode.Intersect);
    }
#endif

#if Gdip
    /// <summary>
    /// Updates the clip region of this XGraphics to the intersection of the 
    /// current clip region and the specified rectangle.
    /// </summary>
    public void IntersectClip(RectangleF rect)
    {
      XGraphicsPath path = new XGraphicsPath();
      path.AddRectangle(rect);
      SetClip(path, XCombineMode.Intersect);
    }
#endif

    /// <summary>
    /// Updates the clip region of this XGraphics to the intersection of the 
    /// current clip region and the specified rectangle.
    /// </summary>
    public void IntersectClip(XRect rect)
    {
      XGraphicsPath path = new XGraphicsPath();
      path.AddRectangle(rect);
      SetClip(path, XCombineMode.Intersect);
    }

    /// <summary>
    /// Updates the clip region of this XGraphics to the intersection of the 
    /// current clip region and the specified graphical path.
    /// </summary>
    public void IntersectClip(XGraphicsPath path)
    {
      SetClip(path, XCombineMode.Intersect);
    }

    /// <summary>
    /// Resets the clip region of this XGraphics to an infinite region, 
    /// i.e. no clipping takes place.
    /// </summary>
    public void ResetClip()
    {
      if (this.drawGraphics)
        this.gfx.ResetClip();

      if (this.renderer != null)
        this.renderer.ResetClip();
    }

    //public void SetClip(Graphics g);
    //public void SetClip(Graphics g, CombineMode combineMode);
    //public void SetClip(GraphicsPath path, CombineMode combineMode);
    //public void SetClip(Rectangle rect, CombineMode combineMode);
    //public void SetClip(RectangleF rect, CombineMode combineMode);
    //public void SetClip(Region region, CombineMode combineMode);
    //public void IntersectClip(Region region);
    //public void ExcludeClip(Region region);

    #endregion

    // --------------------------------------------------------------------------------------------

    #region Miscellaneous

    /// <summary>
    /// Writes a comment to the output stream. Comments have no effect on the rendering of the output.
    /// They may be useful to mark a position in a content stream of a PDF document.
    /// </summary>
    public void WriteComment(string comment)
    {
      if (comment == null)
        throw new ArgumentNullException("comment");

      if (this.drawGraphics)
      {
        // TODO: Do something if metafile?
      }

      if (this.renderer != null)
        this.renderer.WriteComment(comment);
    }

    /// <summary>
    /// Permits acces to internal data.
    /// </summary>
    public XGraphicsInternals Internals
    {
      get
      {
        if (this.internals == null)
          this.internals = new XGraphicsInternals(this);
        return this.internals;
      }
    }
    XGraphicsInternals internals;

    /// <summary>
    /// (Under construction. May change in future versions.)
    /// </summary>
    public SpaceTransformer Transformer
    {
      get
      {
        if (this.transformer == null)
          this.transformer = new SpaceTransformer(this);
        return this.transformer;
      }
    }
    SpaceTransformer transformer;

    #endregion

    // --------------------------------------------------------------------------------------------

    #region Internal Helper Functions

#if Gdip
    /// <summary>
    /// Converts a Point[] into a PointF[].
    /// </summary>
    internal static PointF[] MakePointFArray(Point[] points)
    {
      if (points == null)
        return null;

      int count = points.Length;
      PointF[] result = new PointF[count];
      for (int idx = 0; idx < count; idx++)
      {
        result[idx].X = points[idx].X;
        result[idx].Y = points[idx].Y;
      }
      return result;
    }
#endif

#if Gdip
    /// <summary>
    /// Converts a XPoint[] into a PointF[].
    /// </summary>
    internal static PointF[] MakePointFArray(XPoint[] points)
    {
      if (points == null)
        return null;

      int count = points.Length;
      PointF[] result = new PointF[count];
      for (int idx = 0; idx < count; idx++)
      {
        result[idx].X = (float)points[idx].x;
        result[idx].Y = (float)points[idx].y;
      }
      return result;
    }
#endif

    /// <summary>
    /// Converts a Point[] into a XPoint[].
    /// </summary>
    internal static XPoint[] MakeXPointArray(Point[] points)
    {
      if (points == null)
        return null;

      int count = points.Length;
      XPoint[] result = new XPoint[count];
      for (int idx = 0; idx < count; idx++)
      {
        result[idx].x = points[idx].X;
        result[idx].y = points[idx].Y;
      }
      return result;
    }

#if Gdip
    /// <summary>
    /// Converts a PointF[] into a XPoint[].
    /// </summary>
    internal static XPoint[] MakeXPointArray(PointF[] points)
    {
      if (points == null)
        return null;

      int count = points.Length;
      XPoint[] result = new XPoint[count];
      for (int idx = 0; idx < count; idx++)
      {
        result[idx].x = points[idx].X;
        result[idx].y = points[idx].Y;
      }
      return result;
    }
#endif

    #endregion

    ////    /// <summary>
    ////    /// Testcode
    ////    /// </summary>
    ////    public void TestXObject(PdfDocument thisDoc, PdfPage thisPage, int page, 
    ////      PdfDocument externalDoc, ImportedObjectTable impDoc)
    ////    {
    ////      PdfPage impPage = externalDoc.Pages[page];
    ////      //      impDoc.ImportPage(impPage);
    ////      PdfFormXObject form = new PdfFormXObject(thisDoc, impDoc, impPage);
    ////      thisDoc.xrefTable.Add(form);
    ////
    ////      PdfDictionary xobjects = new PdfDictionary();
    ////      xobjects.Elements["/X42"] = form.XRef;
    ////      thisPage.Resources.Elements[PdfResources.Keys.XObject] = xobjects;
    ////      ((XGraphicsPdfRenderer)this.renderer).DrawXObject("/X42");
    ////    }

#if Gdip
    /// <summary>
    /// Always defined System.Drawing.Graphics object. Used as 'query context' for PDF pages.
    /// </summary>
    internal Graphics gfx;
#endif

    /// <summary>
    /// The transformation matrix from the XGraphics page space to the Graphics world space.
    /// (The name 'default view matrix' comes from Microsoft OS/2 Presentation Manager. I choose
    /// this name because I have no better one.)
    /// </summary>
    internal XMatrix defaultViewMatrix = XMatrix.Identity;

    /// <summary>
    /// Indicates whether to send drawing operations to this.gfx.
    /// </summary>
    bool drawGraphics;

    XForm form;

#if Gdip
    internal Metafile metafile;
#endif

    /// <summary>
    /// Interface to an (optional) renderer. Currently it is the XGraphicsPdfRenderer, if defined.
    /// </summary>
    IXGraphicsRenderer renderer;

    /// <summary>
    /// The transformation matrix from XGraphics world space to page unit space.
    /// </summary>
    XMatrix transform = XMatrix.Identity;

    /// <summary>
    /// The graphics state stack.
    /// </summary>
    GraphicsStateStack gsStack = new GraphicsStateStack();

    /// <summary>
    /// Gets the PDF page that serves as drawing surface if PDF is rendered, otherwise null.
    /// </summary>
    public PdfPage PdfPage
    {
      get
      {
        XGraphicsPdfRenderer renderer = this.renderer as PdfSharp.Drawing.Pdf.XGraphicsPdfRenderer;
        return renderer != null ? renderer.page : null;
      }
    }

#if Gdip
    /// <summary>
    /// Gets the System.Drawing.Graphics objects that serves as drawing surface if no PDF is rendered,
    /// or null, if no such object exists.
    /// </summary>
    public Graphics Graphics
    {
      get { return this.gfx; }
    }
#endif

    /// <summary>
    /// Privides access to internal data structures of the XGraphics class.
    /// </summary>
    public class XGraphicsInternals
    {
      internal XGraphicsInternals(XGraphics gfx)
      {
        this.gfx = gfx;
      }
      XGraphics gfx;

#if Gdip
      /// <summary>
      /// Gets the underlying Graphics object.
      /// </summary>
      public Graphics Graphics
      {
        get { return this.gfx.gfx; }
      }
#endif

      /// <summary>
      /// If PDF is rendered, sets the tz value.
      /// </summary>
      public void SetPdfTz(double value)
      {
        XGraphicsPdfRenderer renderer = this.gfx.renderer as XGraphicsPdfRenderer;
        if (renderer != null)
          renderer.AppendFormat(String.Format(CultureInfo.InvariantCulture, "{0:0.###} Tz\n", value));
      }
    }

    /// <summary>
    /// (This class is under construction.)
    /// </summary>
    public class SpaceTransformer
    {
      internal SpaceTransformer(XGraphics gfx)
      {
        this.gfx = gfx;
      }
      XGraphics gfx;

      /// <summary>
      /// Gets the smalles rectangle in default page space units that completely encloses the specified rect
      /// in world space units.
      /// </summary>
      public XRect WorldToDefaultPage(XRect rect)
      {
        XPoint[] points = new XPoint[4];
        points[0] = new XPoint(rect.x, rect.y);
        points[1] = new XPoint(rect.x + rect.width, rect.y);
        points[2] = new XPoint(rect.x, rect.y + rect.height);
        points[3] = new XPoint(rect.x + rect.width, rect.y + rect.height);

        XMatrix matrix = this.gfx.transform;
        matrix.TransformPoints(points);

        double height = this.gfx.PageSize.height;
        points[0].y = height - points[0].y;
        points[1].y = height - points[1].y;
        points[2].y = height - points[2].y;
        points[3].y = height - points[3].y;

        double xmin = Math.Min(Math.Min(points[0].x, points[1].x), Math.Min(points[2].x, points[3].x));
        double xmax = Math.Max(Math.Max(points[0].x, points[1].x), Math.Max(points[2].x, points[3].x));
        double ymin = Math.Min(Math.Min(points[0].y, points[1].y), Math.Min(points[2].y, points[3].y));
        double ymax = Math.Max(Math.Max(points[0].y, points[1].y), Math.Max(points[2].y, points[3].y));

        return new XRect(xmin, ymin, xmax - xmin, ymax - ymin);
      }
    }
  }
}
