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
#if Gdip
using System.Drawing;
using System.Drawing.Drawing2D;
#endif
#if Wpf
using System.Windows.Media;
#endif

namespace PdfSharp.Drawing
{
  /// <summary>
  /// Represents a stack of XGraphicsState and XGraphicsContainer objects.
  /// </summary>
  internal class GraphicsStateStack
  {
    public GraphicsStateStack()
    {
    }

    public int Count
    {
      get { return this.stack.Count; }
    }

    public void Push(InternalGraphicsState state)
    {
      this.stack.Push(state);
    }

    public int Restore(InternalGraphicsState state)
    {
      if (!this.stack.Contains(state))
        throw new ArgumentException("State not on stack.", "state");
      if (state.invalid)
        throw new ArgumentException("State already restored.", "state");

      int count = 1;
      InternalGraphicsState top = (InternalGraphicsState)this.stack.Pop();
      while (top != state)
      {
        count++;
        state.invalid = true;
        top = (InternalGraphicsState)this.stack.Pop();
      }
      state.invalid = true;
      return count;
    }

    Stack stack = new Stack();
  }
}
