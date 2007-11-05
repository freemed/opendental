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
using System.Text;
using System.IO;
using PdfSharp.Internal;
using PdfSharp.Pdf.IO;
using PdfSharp.Pdf.Internal;

namespace PdfSharp.Pdf
{
  /// <summary>
  /// Determines the encoding of a PdfString or PdfStringObject.
  /// </summary>
  [Flags]
  public enum PdfStringEncoding
  {
    /// <summary>
    /// The characters of the string are actually bytes with an unknown or context specific meaning or encoding.
    /// With this encoding the 8 high bits of each character is zero.
    /// </summary>
    RawEncoding       = 0x00,

    /// <summary>
    /// Not yet used by PDFsharp.
    /// </summary>
    StandardEncoding = 0x01,

    /// <summary>
    /// The characters of the string are actually bytes with PDF document encoding.
    /// With this encoding the 8 high bits of each character is zero.
    /// </summary>
    PDFDocEncoding = 0x02,

    /// <summary>
    /// The characters of the string are actually bytes with Windows ANSI encoding.
    /// With this encoding the 8 high bits of each character is zero.
    /// </summary>
    WinAnsiEncoding = 0x03,

    /// <summary>
    /// Not yet used by PDFsharp.
    /// </summary>
    MacRomanEncoding = 0x04,  // not used by PDFsharp

    /// <summary>
    /// Not yet used by PDFsharp.
    /// </summary>
    MacExpertEncoding = 0x05,  // not used by PDFsharp

    /// <summary>
    /// The characters of the string are Unicode characters.
    /// </summary>
    Unicode = 0x06,
  }

  /// <summary>
  /// Internal wrapper for PdfStringEncoding.
  /// </summary>
  [Flags]
  enum PdfStringFlags
  {
    RawEncoding       = 0x00,
    StandardEncoding  = 0x01,  // not used by PDFsharp
    PDFDocEncoding    = 0x02,
    WinAnsiEncoding   = 0x03,
    MacRomanEncoding  = 0x04,  // not used by PDFsharp
    MacExpertEncoding = 0x05,  // not used by PDFsharp
    Unicode           = 0x06,
    EncodingMask      = 0x0F,

    HexLiteral        = 0x80,
  }

  /// <summary>
  /// Represents a direct text string value.
  /// </summary>
  [DebuggerDisplay("({Value})")]
  public sealed class PdfString : PdfItem
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="PdfString"/> class.
    /// </summary>
    public PdfString()
    {
      this.flags = PdfStringFlags.RawEncoding;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="PdfString"/> class.
    /// </summary>
    /// <param name="value">The value.</param>
    public PdfString(string value)
    {
      this.value = value;
      this.flags = PdfStringFlags.RawEncoding;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="PdfString"/> class.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <param name="encoding">The encoding.</param>
    public PdfString(string value, PdfStringEncoding encoding)
    {
      this.value = value;
      //if ((flags & PdfStringFlags.EncodingMask) == 0)
      //  flags |= PdfStringFlags.PDFDocEncoding;
      this.flags = (PdfStringFlags)encoding;
    }

    internal PdfString(string value, PdfStringFlags flags)
    {
      this.value = value;
      //if ((flags & PdfStringFlags.EncodingMask) == 0)
      //  flags |= PdfStringFlags.PDFDocEncoding;
      this.flags = flags;
    }

    /// <summary>
    /// Gets the number of characters in this string.
    /// </summary>
    public int Length
    {
      get { return this.value == null ? 0 : this.value.Length; }
    }

    /// <summary>
    /// Gets the encoding.
    /// </summary>
    public PdfStringEncoding Encoding
    {
      get { return (PdfStringEncoding)(this.flags & PdfStringFlags.EncodingMask); }
      //set { this.flags = (this.flags & ~PdfStringFlags.EncodingMask) | ((PdfStringFlags)value & PdfStringFlags.EncodingMask);}
    }

    /// <summary>
    /// Gets a value indicating whether the string is a hexadecimal literal.
    /// </summary>
    public bool HexLiteral
    {
      get { return (this.flags & PdfStringFlags.HexLiteral) != 0; }
      //set { this.flags = value ? this.flags | PdfStringFlags.HexLiteral : this.flags & ~PdfStringFlags.HexLiteral;}
    }

    internal PdfStringFlags Flags
    {
      get { return this.flags; }
      //set { this.flags = value; }
    }
    PdfStringFlags flags;

    /// <summary>
    /// Gets the string value.
    /// </summary>
    public string Value
    {
      // This class must behave like a value type. Therefore it cannot be changed (like System.String).
      get { return this.value == null ? "" : this.value; }
    }
    string value;

    /// <summary>
    /// Gets or sets the string value for encryption purposes.
    /// </summary>
    internal byte[] EncryptionValue
    {
      // TODO: Unicode case is not handled!
      get {return this.value == null ? new byte[0] : PdfEncoders.RawEncoding.GetBytes(this.value);}
      // BUG: May lead to trouble with the value semantics of PdfString
      set {this.value = PdfEncoders.RawEncoding.GetString(value);}
    }

    /// <summary>
    /// Returns the string.
    /// </summary>
    public override string ToString()
    {
      return this.value;
    }

    /// <summary>
    /// Writes the string DocEncoded.
    /// </summary>
    internal override void WriteObject(PdfWriter writer)
    {
      writer.Write(this);
    }
  }
}
