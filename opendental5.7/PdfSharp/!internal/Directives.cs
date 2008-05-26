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

//
// Documentatation of defines used in PDFsharp
//
#if NET_2_0
// Compile code for .NET 2.0
#else
#error NET_2_0 must be defined
#endif

#if MIGRADOC
// empira internal only: Some hacks that make PDFsharp behave like PDFlib when used with Asc.RenderContext.
// Applies to MigraDoc 1.2 only. The Open Source MigraDoc lite does not need this define.
#endif

#if NET_ZIP
// In .NET 2.0 GZipStream is used instead of SharpZipLib
// This does not work.
#endif
#if Gdip
// PDFsharp based on System.Drawing classes

#if GdipUseGdiObjects
// PDFsharp X graphics classes have implicit cast operators for GDI+ objects.
// Define this to make it easier to use older code with PDFsharp.
// Undefine this to prevent dependencies to GDI+
#endif
#elif Wpf
// PDFsharp based on Windows Presentation Foundation in a future release...
// (Current version will not compile with Wpf defined)
#else
#error Either GdipUseGdiObjects or Wpf must be defined
#endif
