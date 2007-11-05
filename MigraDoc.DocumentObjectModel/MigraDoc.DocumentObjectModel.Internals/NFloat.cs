#region MigraDoc - Creating Documents on the Fly
//
// Authors:
//   Stefan Lange (mailto:Stefan.Lange@pdfsharp.com)
//   Klaus Potzesny (mailto:Klaus.Potzesny@pdfsharp.com)
//   David Stephensen (mailto:David.Stephensen@pdfsharp.com)
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

using System;

namespace MigraDoc.DocumentObjectModel.Internals
{
  /* Not in use anymore - delete
 
    /// <summary>
    /// Represents a nullable float value.
    /// </summary>
    internal struct NFloat : IDomValue
    {
      public NFloat(float value)
      {
        this.val = value;
      }

      /// <summary>
      /// Gets or sets the value of the instance.
      /// </summary>
      public float Value
      {
        get {return float.IsNaN(this.val) ? 0 : this.val;}
        set {this.val = value;}
      }

      /// <summary>
      /// Gets the value of the instance.
      /// </summary>
      object IDomValue.GetValue()
      {
        return this.Value;
      }

      /// <summary>
      /// Sets the value of the instance.
      /// </summary>
      void IDomValue.SetValue(object value)
      {
        // API uses NFloat, internal double is used.
        if (value is double)
          this.val = (float)(double)value;
        else
          this.val = (float)value;
      }

      /// <summary>
      /// Resets this instance,
      /// i.e. IsNull() will return true afterwards.
      /// </summary>
      public void SetNull()
      {
        this.val = float.NaN;
      }

      /// <summary>
      /// Determines whether this instance is null (not set).
      /// </summary>
      public bool IsNull
      {
        get {return float.IsNaN(this.val);}
      }
  todo == !=
      public static readonly NFloat NullValue = new NFloat(float.NaN);

      float val;
    }

  */
}
