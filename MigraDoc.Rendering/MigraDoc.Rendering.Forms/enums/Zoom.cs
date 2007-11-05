#region MigraDoc - Creating Documents on the Fly
//
// Authors:
//   Stefan Lange (mailto:Stefan.Lange@pdfsharp.com)
//
// Copyright (c) 2001-2007 empira Software GmbH, Cologne (Germany)
//
// http://www.pdfsharp.com
// http://www.migradoc.com
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

namespace MigraDoc.Rendering.Forms
{
  /// <summary>
  /// Defines a zoom factor used in the preview control.
  /// </summary>
  public enum Zoom
  {
    Percent800 = PdfSharp.Forms.Zoom.Percent800,
    Percent600 = PdfSharp.Forms.Zoom.Percent600,
    Percent400 = PdfSharp.Forms.Zoom.Percent400,
    Percent200 = PdfSharp.Forms.Zoom.Percent200,
    Percent150 = PdfSharp.Forms.Zoom.Percent150,
    Percent100 = PdfSharp.Forms.Zoom.Percent100,
    Percent75 = PdfSharp.Forms.Zoom.Percent75,
    Percent50 = PdfSharp.Forms.Zoom.Percent50,
    Percent25 = PdfSharp.Forms.Zoom.Percent25,
    Percent10 = PdfSharp.Forms.Zoom.Percent10,
    BestFit = PdfSharp.Forms.Zoom.BestFit,
    TextFit = PdfSharp.Forms.Zoom.TextFit,
    FullPage = PdfSharp.Forms.Zoom.FullPage,
    OriginalSize = PdfSharp.Forms.Zoom.OriginalSize,
    Mininum = PdfSharp.Forms.Zoom.Mininum,
    Maximum = PdfSharp.Forms.Zoom.Maximum,
  }
}
