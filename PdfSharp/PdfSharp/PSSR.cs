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
using System.Resources;
using System.Reflection;
using System.Text.RegularExpressions;

namespace PdfSharp
{
  /// <summary>
  /// The Pdf-Sharp-String-Resources.
  /// </summary>
  internal sealed class PSSR
  {
    // How to use:
    // Create a function or property for each message text, depending on how much parameters are
    // part of the message. For the time beginning, type plain english text in the function or property. 
    // The use of functions is save when a parameter must be changed. The compiler tells you all
    // places in your code that must be modified.
    // For localization, create a enum value for each function or property with the same name. Then
    // create localized message files with the enum values as messages identifiers. In the properties
    // and functions all text is replaced by Format or GetString functions with the according enum value
    // as first parameter. The use of enums ensures that typing errors in message resource names are 
    // simply impossible. Use the TestResourceMessages function to ensure that each enum value has an 
    // appropriate message text.

    PSSR() { }

    #region Helper functions
    ////    /// <summary>
    ////    /// Loads the message from the resource associated with the enum type and formats it
    ////    /// using 'String.Format'. Because this function is intended to be used during error
    ////    /// handling it never raises an exception.
    ////    /// </summary>
    ////    /// <param name="id">The type of the parameter identifies the resource
    ////    /// and the name of the enum identifies the message in the resource.</param>
    ////    /// <param name="args">Parameters passed through 'String.Format'.</param>
    ////    /// <returns>The formatted message.</returns>
    public static string Format(PSMsgID id, params object[] args)
    {
      string message;
      try
      {
        message = PSSR.GetString(id);
        if (message != null)
          message = Format(message, args);
        else
          message = "INTERNAL ERROR: Message not found in resources.";
        return message;
      }
      catch (Exception ex)
      {
        message = "UNEXPECTED ERROR while formatting message: " + ex.ToString();
      }
      return message;
    }

    public static string Format(string format, params object[] args)
    {
      if (format == null)
        throw new ArgumentNullException("format");

      string message;
      try
      {
        message = String.Format(format, args);
      }
      catch (Exception ex)
      {
        message = "UNEXPECTED ERROR while formatting message: " + ex.ToString();
      }
      return message;
    }

    /// <summary>
    /// Gets the localized message identified by the specified DomMsgID.
    /// </summary>
    public static string GetString(PSMsgID id)
    {
      return PSSR.ResMngr.GetString(id.ToString());
    }

    #endregion

    #region General messages

    public static string IndexOutOfRange
    {
      get { return "The index is out of range."; }
    }

    public static string ListEnumCurrentOutOfRange
    {
      get { return "Enumeration out of range."; }
    }

    public static string PageIndexOutOfRange
    {
      get { return "The index of a page is out of range."; }
    }

    public static string OutlineIndexOutOfRange
    {
      get { return "The index of an outline is out of range."; }
    }

    public static string InvalidValue(int val, string name, int min, int max)
    {
      return Format("{0} is not a valid value for {1}. {1} should be greater than or equal to {2} and less than or equal to {3}.",
        val, name, min, max);
    }

    public static string OwningDocumentRequired
    {
      get { return "The PDF object must belong to a PdfDocument, but property Document is null."; }
    }

    public static string FileNotFound(string path)
    {
      return Format("The file '{0}' does not exist.", path);
    }

    public static string FontImageReadOnly
    {
      get { return "Font image is read-only"; }
    }

    #endregion

    #region XGraphics specific messages

    // ----- XGraphics ----------------------------------------------------------------------------

    public static string PointArrayEmpty
    {
      get { return "The PointF array must not be empty."; }
    }

    public static string PointArrayAtLeast(int count)
    {
      return Format("The point array must contain {0} or more points.", count);
    }

    public static string NeedPenOrBrush
    {
      get { return "XPen or XBrush or both must not be null."; }
    }

    public static string CannotChangeImmutableObject(string typename)
    {
      return String.Format("You cannot change this immutable {0} object.", typename);
    }

    #endregion

    #region PDF specific messages

    // ----- PDF ----------------------------------------------------------------------------------

    public static string InvalidPdf
    {
      get { return "The file is not a valid PDF document."; }
    }

    public static string InvalidVersionNumber
    {
      get { return "Invalid version number. Valid values are 12, 13, and 14."; }
    }

    public static string CannotHandleXRefStreams
    {
      get { return "Cannot handle iref streams. The current implementation of PDFsharp cannot handle this PDF feature introduced with Acrobat 6."; }
    }

    public static string PasswordRequired
    {
      get { return "A password is required to open the PDF document."; }
    }

    public static string InvalidPassword
    {
      get { return "The specified password is invalid."; }
    }

    public static string OwnerPasswordRequired
    {
      get { return "To modify the document the owner password is required"; }
    }

    public static string UserOrOwnerPasswordRequired
    {
      get { return GetString(PSMsgID.UserOrOwnerPasswordRequired); }
    }

    public static string CannotModify
    {
      get { return "The document cannot be modified."; }
    }

    public static string NameMustStartWithSlash
    {
      get { return GetString(PSMsgID.NameMustStartWithSlash); }
    }

    public static string ImportPageNumberOutOfRange(int pageNumber, int maxPage, string path)
    {
      return String.Format("The page cannot be imported from document '{2}', because the page number is out of range. " +
        "The specified page number is {0}, but it must be in the range from 1 to {1}.", pageNumber, maxPage, path);
    }

    public static string MultiplePageInsert
    {
      get { return "The page cannot be added to this document because the document already owned this page."; }
    }

    public static string UnexpectedTokenInPdfFile
    {
      get { return "Unexpected token in PDF file. The PDF file may be currupt. If it is not, please send us the file for serivce."; }
    }




    // ----- PdfParser ----------------------------------------------------------------------------

    public static string UnexpectedToken(string token)
    {
      return Format(PSMsgID.UnexpectedToken, token);
    }

    public static string UnknownEncryption
    {
      get { return GetString(PSMsgID.UnknownEncryption); }
    }


    //    public static string NoClassType(Type type)
    //    {
    //      return String.Format(FrameWork.GetString(MsgID.Error_NoClassType), type);
    //    }
    //
    //    public static string NoMeta(string type)
    //    {
    //      return String.Format(FrameWork.GetString(MsgID.Error_NoMeta), type);
    //    }
    //
    //    public static string NoDataSource
    //    {
    //      get {return FrameWork.GetString(MsgID.Error_NoDataSource);}
    //    }
    //
    //    public static string InvalidValueName(string name)
    //    {
    //      return String.Format(FrameWork.GetString(MsgID.Error_InvalidValueName), name);
    //    }
    //
    //    public static string InvalidValuePath(string path)
    //    {
    //      return String.Format(FrameWork.GetString(MsgID.Error_InvalidValuePath), path);
    //    }
    //
    //    /// <summary>
    //    /// Assigning of domain objects fails.
    //    /// </summary>
    //    public static string InappropriateType(object type, object typeAssign)
    //    {
    //      return String.Format(FrameWork.GetString(MsgID.Error_InappropriateType), type, typeAssign);
    //    }
    //
    //    public static string DuplicateClassType(string type)
    //    {
    //      return String.Format(FrameWork.GetString(MsgID.Error_DuplicateClassType), type);
    //    }
    //
    //    public static string InappropriateMeta
    //    {
    //      get {return "Inappropriate meta for this domain object.";}
    //      //return String.Format(FrameWork.GetString(MsgID.Error_DuplicateClassType), type);
    //    }
    //
    //    public static string InitializeBadType(Type givenType, Type thisType)
    //    {
    //      //return String.Format(FrameWork.GetString(MsgID.Error_DuplicateClassType), type);
    //      return String.Format("'{0}' is not assignable form '{1}'. You probably call 'Initialize(typeof(YourDomainObject))' with the wrong type.", givenType.FullName, thisType.FullName);
    //    }
    //
    //    public static string ValueNotAField(string name)
    //    {
    //      return String.Format("Value '{0}' is not a field.", name);
    //    }
    //    
    //    public static string ValueNotAProperty(string name)
    //    {
    //      return String.Format("Value '{0}' is not a property.", name);
    //    }
    //    
    //    public static string PropertyHasNoGetter(string name)
    //    {
    //      return String.Format("Property '{0}' has no getter.", name);
    //    }
    //
    //    public static string PropertyHasNoSetter(string name)
    //    {
    //      return String.Format("Property '{0}' has no setter.", name);
    //    }
    //
    //    //
    //    // XML
    //    //
    //
    //    public static string CyclicDataButNoRefId
    //    {
    //      get {return "The pdf have cyclic references but XML was written with no RefId. Set property WriteRefId of XmlDomainObjectWriter to true.";}
    //    }

    #endregion

    #region Resource manager

    /// <summary>
    /// Gets the resource manager for this module.
    /// </summary>
    public static ResourceManager ResMngr
    {
      get
      {
        if (PSSR.resmngr == null)
        {
#if DEBUG_
          // Force the english language, even on a German PC.
          System.Threading.Thread.CurrentThread.CurrentUICulture = System.Globalization.CultureInfo.InvariantCulture;
#endif
          PSSR.resmngr = new ResourceManager("PdfSharp.Resources.Messages", Assembly.GetExecutingAssembly());
#if DEBUG_
          string test = PSSR.resmngr.GetString("SampleMessage1");
          test.GetType();
#endif
        }
        return PSSR.resmngr;
      }
    }
    static ResourceManager resmngr;

    /// <summary>
    /// Writes all messages defined by PSMsgID.
    /// </summary>
    [Conditional("DEBUG")]
    public static void TestResourceMessages()
    {
      string[] names = Enum.GetNames(typeof(PSMsgID));
      foreach (string name in names)
      {
        string message = String.Format("{0}: '{1}'", name, ResMngr.GetString(name));
        Debug.WriteLine(message);
      }
    }

    #endregion
  }
}
