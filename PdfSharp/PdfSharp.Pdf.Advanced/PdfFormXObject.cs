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
#if Gdip
using System.Drawing;
using System.Drawing.Imaging;
#endif
#if Wpf
using System.Windows.Media;
#endif
using PdfSharp.Drawing;
using PdfSharp.Fonts.TrueType;
using PdfSharp.Internal;
using PdfSharp.Pdf.Internal;

namespace PdfSharp.Pdf.Advanced
{
  /// <summary>
  /// Represents an external form object (an imported page i.g).
  /// </summary>
  public sealed class PdfFormXObject : PdfXObject
  {
    internal PdfFormXObject(PdfDocument thisDocument, XForm form)
      : base(thisDocument)
    {
      Elements.SetName(Keys.Type, "/XObject");
      Elements.SetName(Keys.Subtype, "/Form");

      //if (form.IsTemplate)
      //{ }
    }

    internal PdfFormXObject(PdfDocument thisDocument, PdfImportedObjectTable importedObjectTable, XPdfForm form)
      : base(thisDocument)
    {
      Debug.Assert(Object.ReferenceEquals(thisDocument, importedObjectTable.Owner));
      Elements.SetName(Keys.Type, "/XObject");
      Elements.SetName(Keys.Subtype, "/Form");

      if (form.IsTemplate)
      {
        Debug.Assert(importedObjectTable == null);
        // TODO more initialization here???
        return;
      }
      Debug.Assert(importedObjectTable != null);

      XPdfForm pdfForm = (XPdfForm)form;
      // Get import page
      PdfPages importPages = importedObjectTable.ExternalDocument.Pages;
      if (pdfForm.PageNumber < 1 || pdfForm.PageNumber > importPages.Count)
        PSSR.ImportPageNumberOutOfRange(pdfForm.PageNumber, importPages.Count, form.path);
      PdfPage importPage = importPages[pdfForm.PageNumber - 1];

      // Import resources
      PdfItem res = importPage.Elements["/Resources"];
      if (res != null) // unlikely but possible
      {
#if true
        // Get root object
        PdfObject root;
        if (res is PdfReference)
          root = ((PdfReference)res).Value;
        else
          root = (PdfDictionary)res;

        root = ImportClosure(importedObjectTable, thisDocument, root);
        // If the root was a direct object, make it indirect.
        if (root.Reference == null)
          thisDocument.irefTable.Add(root);

        Debug.Assert(root.Reference != null);
        Elements["/Resources"] = root.Reference;
#else
        // Get transitive closure
        PdfObject[] resources = importPage.Owner.Internals.GetClosure(resourcesRoot);
        int count = resources.Length;
#if DEBUG_
        for (int idx = 0; idx < count; idx++)
        {
          Debug.Assert(resources[idx].XRef != null);
          Debug.Assert(resources[idx].XRef.Document != null);
          Debug.Assert(resources[idx].Document != null);
          if (resources[idx].ObjectID.ObjectNumber == 12)
            GetType();
        }
#endif
        // 1st step. Already imported objects are reused and new ones are cloned.
        for (int idx = 0; idx < count; idx++)
        {
          PdfObject obj = resources[idx];
          if (importedObjectTable.Contains(obj.ObjectID))
          {
            // external object was already imported
            PdfReference iref = importedObjectTable[obj.ObjectID];
            Debug.Assert(iref != null);
            Debug.Assert(iref.Value != null);
            Debug.Assert(iref.Document == this.Owner);
            // replace external object by the already clone counterpart
            resources[idx] = iref.Value;
          }
          else
          {
            // External object was not imported ealier and must be cloned
            PdfObject clone = obj.Clone();
            Debug.Assert(clone.Reference == null);
            clone.Document = this.Owner;
            if (obj.Reference != null)
            {
              // add it to this (the importer) document
              this.Owner.irefTable.Add(clone);
              Debug.Assert(clone.Reference != null);
              // save old object identifier
              importedObjectTable.Add(obj.ObjectID, clone.Reference);
              //Debug.WriteLine("Cloned: " + obj.ObjectID.ToString());
            }
            else
            {
              // The root object (the /Resources value) is not an indirect object
              Debug.Assert(idx == 0);
              // add it to this (the importer) document
              this.Owner.irefTable.Add(clone);
              Debug.Assert(clone.Reference != null);
            }
            // replace external object by its clone
            resources[idx] = clone;
          }
        }
#if DEBUG_
        for (int idx = 0; idx < count; idx++)
        {
          Debug.Assert(resources[idx].XRef != null);
          Debug.Assert(resources[idx].XRef.Document != null);
          Debug.Assert(resources[idx].Document != null);
          if (resources[idx].ObjectID.ObjectNumber == 12)
            GetType();
        }
#endif

        // 2nd step. Fix up indirect references that still refers to the import document.
        for (int idx = 0; idx < count; idx++)
        {
          PdfObject obj = resources[idx];
          Debug.Assert(obj.Owner != null);
          FixUpObject(importedObjectTable, importedObjectTable.Owner, obj);
        }

        // Set resources key to the root of the clones
        Elements["/Resources"] = resources[0].Reference;
#endif
      }

      // Take /Rotate into account
      PdfRectangle rect = importPage.Elements.GetRectangle(PdfPage.Keys.MediaBox);
      int rotate = importPage.Elements.GetInteger(PdfPage.Keys.Rotate);
      //rotate = 0;
      if (rotate == 0)
      {
        // Set bounding box to media box
        this.Elements["/BBox"] = rect;
      }
      else
      {
        // TODO: Have to adjust bounding box? (I think not, but I'm not sure -> wait for problem)
        this.Elements["/BBox"] = rect;

        // Rotate the image such that it is upright
        XMatrix matrix = XMatrix.Identity;
        double width = rect.Width;
        double height = rect.Height;
        matrix.RotateAt(-rotate, new XPoint(width / 2, height / 2));

        // Translate the image such that its center lies on the center of the rotated bounding box
        double offset = (height - width) / 2;
        if (height > width)
          matrix.Translate(offset, offset);
        else
          matrix.Translate(-offset, -offset);

        string item = "[" + PdfEncoders.ToString(matrix) + "]";
        this.Elements[Keys.Matrix] = new PdfLiteral(item);
      }

      // Preserve filter because the content keeps unmodified
      PdfContent content = importPage.Contents.CreateSingleContent();
#if !DEBUG
      content.Compressed = true;
#endif
      PdfItem filter = content.Elements["/Filter"];
      if (filter != null)
        this.Elements["/Filter"] = filter.Clone();

      // (no cloning needed because the bytes keep untouched)
      this.Stream = content.Stream; // new PdfStream(bytes, this);
      Elements.SetInteger("/Length", content.Stream.Value.Length);
    }

#if keep_code_some_time_as_reference
    /// <summary>
    /// Replace all indirect references to external objects by their cloned counterparts
    /// owned by the importer document.
    /// </summary>
    void FixUpObject_old(PdfImportedObjectTable iot, PdfObject value)
    {
      // TODO: merge with PdfXObject.FixUpObject
      PdfDictionary dict;
      PdfArray array;
      if ((dict = value as PdfDictionary) != null)
      {
        // Set document for cloned direct objects
        if (dict.Owner == null)
          dict.Document = this.Owner;
        else
          Debug.Assert(dict.Owner == this.Owner);

        // Search for indirect references in all keys
        PdfName[] names = dict.Elements.KeyNames;
        foreach (PdfName name in names)
        {
          PdfItem item = dict.Elements[name];
          // Is item an iref?
          PdfReference iref = item as PdfReference;
          if (iref != null)
          {
            // Does the iref already belong to this document?
            if (iref.Document == this.Owner)
            {
              // Yes: fine
              continue;
            }
            else
            {
              Debug.Assert(iref.Document == iot.ExternalDocument);
              // No: replace with iref of cloned object
              PdfReference newXRef = iot[iref.ObjectID];
              Debug.Assert(newXRef != null);
              Debug.Assert(newXRef.Document == this.Owner);
              dict.Elements[name] = newXRef;
            }
          }
          else if (item is PdfObject)
          {
            // Fix up inner objects
            FixUpObject_old(iot, (PdfObject)item);
          }
        }
      }
      else if ((array = value as PdfArray) != null)
      {
        // Set document for cloned direct objects
        if (array.Owner == null)
          array.Document = this.Owner;
        else
          Debug.Assert(array.Owner == this.Owner);

        // Search for indirect references in all array elements
        int count = array.Elements.Count;
        for (int idx = 0; idx < count; idx++)
        {
          PdfItem item = array.Elements[idx];
          // Is item an iref?
          PdfReference iref = item as PdfReference;
          if (iref != null)
          {
            // Does the iref belongs to this document?
            if (iref.Document == this.Owner)
            {
              // Yes: fine
              continue;
            }
            else
            {
              Debug.Assert(iref.Document == iot.ExternalDocument);
              // No: replace with iref of cloned object
              PdfReference newXRef = iot[iref.ObjectID];
              Debug.Assert(newXRef != null);
              Debug.Assert(newXRef.Document == this.Owner);
              array.Elements[idx] = newXRef;
            }
          }
          else if (item is PdfObject)
          {
            // Fix up inner objects
            FixUpObject_old(iot, (PdfObject)item);
          }
        }
      }
    }
#endif

    //    /// <summary>
    //    /// Returns ???
    //    /// </summary>
    //    public override string ToString()
    //    {
    //      return "Form";
    //    }

    /// <summary>
    /// Predefined keys of this dictionary.
    /// </summary>
    public sealed new class Keys : PdfXObject.Keys
    {
      /// <summary>
      /// (Optional) The type of PDF object that this dictionary describes; if present,
      /// must be XObject for a form XObject.
      /// </summary>
      [KeyInfo(KeyType.Name | KeyType.Optional)]
      public const string Type = "/Type";

      /// <summary>
      /// (Required) The type of XObject that this dictionary describes; must be Form
      /// for a form XObject.
      /// </summary>
      [KeyInfo(KeyType.Name | KeyType.Required)]
      public const string Subtype = "/Subtype";

      /// <summary>
      /// (Optional) A code identifying the type of form XObject that this dictionary
      /// describes. The only valid value defined at the time of publication is 1.
      /// Default value: 1.
      /// </summary>
      [KeyInfo(KeyType.Integer | KeyType.Optional)]
      public const string FormType = "/FormType";

      /// <summary>
      /// (Required) An array of four numbers in the form coordinate system, giving the 
      /// coordinates of the left, bottom, right, and top edges, respectively, of the 
      /// form XObject’s bounding box. These boundaries are used to clip the form XObject
      /// and to determine its size for caching.
      /// </summary>
      [KeyInfo(KeyType.Rectangle | KeyType.Required)]
      public const string BBox = "/BBox";

      /// <summary>
      /// (Optional) An array of six numbers specifying the form matrix, which maps
      /// form space into user space.
      /// Default value: the identity matrix [1 0 0 1 0 0].
      /// </summary>
      [KeyInfo(KeyType.Array | KeyType.Optional)]
      public const string Matrix = "/Matrix";

      /// <summary>
      /// (Optional but strongly recommended; PDF 1.2) A dictionary specifying any
      /// resources (such as fonts and images) required by the form XObject.
      /// </summary>
      [KeyInfo(KeyType.Dictionary | KeyType.Optional, typeof(PdfResources))]
      public const string Resources = "/Resources";

      /// <summary>
      /// (Optional; PDF 1.4) A group attributes dictionary indicating that the contents
      /// of the form XObject are to be treated as a group and specifying the attributes
      /// of that group (see Section 4.9.2, “Group XObjects”).
      /// Note: If a Ref entry (see below) is present, the group attributes also apply to the
      /// external page imported by that entry, which allows such an imported page to be
      /// treated as a group without further modification.
      /// </summary>
      [KeyInfo(KeyType.Dictionary | KeyType.Optional)]
      public const string Group = "/Group";

      // further keys:
      //Ref
      //Metadata
      //PieceInfo
      //LastModified
      //StructParent
      //StructParents
      //OPI
      //OC
      //Name

      /// <summary>
      /// Gets the KeysMeta for these keys.
      /// </summary>
      internal static DictionaryMeta Meta
      {
        get
        {
          if (Keys.meta == null)
            Keys.meta = CreateMeta(typeof(Keys));
          return Keys.meta;
        }
      }
      static DictionaryMeta meta;
    }

    /// <summary>
    /// Gets the KeysMeta of this dictionary type.
    /// </summary>
    internal override DictionaryMeta Meta
    {
      get { return Keys.Meta; }
    }


#if delete
    /// <summary>
    /// Common keys for all streams.
    /// </summary>
    public class Keys____ : PdfDictionary.PdfStream.Keys  // TODO: check!
    {
      /// <summary>
      /// (Optional) The type of PDF object that this dictionary describes;
      /// if present, must be XObject for an image XObject.
      /// </summary>
      [KeyInfo(KeyType.Name | KeyType.Optional)]
      public const string Type = "/Type";

      /// <summary>
      /// (Required) The type of XObject that this dictionary describes;
      /// must be Image for an image XObject.
      /// </summary>
      [KeyInfo(KeyType.Name | KeyType.Required)]
      public const string Subtype = "/Subtype";

      /// <summary>
      /// (Required) The width of the image, in samples.
      /// </summary>
      [KeyInfo(KeyType.Integer | KeyType.Required)]
      public const string Width = "/Width";

      /// <summary>
      /// (Required) The height of the image, in samples.
      /// </summary>
      [KeyInfo(KeyType.Integer | KeyType.Required)]
      public const string Height = "/Height";

      /// <summary>
      /// (Required for images, except those that use the JPXDecode filter; not allowed for image masks)
      /// The color space in which image samples are specified; it can be any type of color space except
      /// Pattern. If the image uses the JPXDecode filter, this entry is optional:
      /// • If ColorSpace is present, any color space specifications in the JPEG2000 data are ignored.
      /// • If ColorSpace is absent, the color space specifications in the JPEG2000 data are used.
      ///   The Decode array is also ignored unless ImageMask is true.
      /// </summary>
      [KeyInfo(KeyType.NameOrArray | KeyType.Required)]
      public const string ColorSpace = "/ColorSpace";

      /// <summary>
      /// (Required except for image masks and images that use the JPXDecode filter)
      /// The number of bits used to represent each color component. Only a single value may be specified;
      /// the number of bits is the same for all color components. Valid values are 1, 2, 4, 8, and 
      /// (in PDF 1.5) 16. If ImageMask is true, this entry is optional, and if specified, its value 
      /// must be 1.
      /// If the image stream uses a filter, the value of BitsPerComponent must be consistent with the 
      /// size of the data samples that the filter delivers. In particular, a CCITTFaxDecode or JBIG2Decode 
      /// filter always delivers 1-bit samples, a RunLengthDecode or DCTDecode filter delivers 8-bit samples,
      /// and an LZWDecode or FlateDecode filter delivers samples of a specified size if a predictor function
      /// is used.
      /// If the image stream uses the JPXDecode filter, this entry is optional and ignored if present.
      /// The bit depth is determined in the process of decoding the JPEG2000 image.
      /// </summary>
      [KeyInfo(KeyType.Integer | KeyType.Required)]
      public const string BitsPerComponent = "/BitsPerComponent";

      /// <summary>
      /// (Optional; PDF 1.1) The name of a color rendering intent to be used in rendering the image.
      /// Default value: the current rendering intent in the graphics state.
      /// </summary>
      [KeyInfo(KeyType.Name | KeyType.Optional)]
      public const string Intent = "/Intent";

      /// <summary>
      /// (Optional) A flag indicating whether the image is to be treated as an image mask.
      /// If this flag is true, the value of BitsPerComponent must be 1 and Mask and ColorSpace should
      /// not be specified; unmasked areas are painted using the current nonstroking color.
      /// Default value: false.
      /// </summary>
      [KeyInfo(KeyType.Boolean | KeyType.Optional)]
      public const string ImageMask = "/ImageMask";

      /// <summary>
      /// (Optional except for image masks; not allowed for image masks; PDF 1.3)
      /// An image XObject defining an image mask to be applied to this image, or an array specifying 
      /// a range of colors to be applied to it as a color key mask. If ImageMask is true, this entry
      /// must not be present.
      /// </summary>
      [KeyInfo(KeyType.StreamOrArray | KeyType.Optional)]
      public const string Mask = "/Mask";

      /// <summary>
      /// (Optional) An array of numbers describing how to map image samples into the range of values
      /// appropriate for the image’s color space. If ImageMask is true, the array must be either
      /// [0 1] or [1 0]; otherwise, its length must be twice the number of color components required 
      /// by ColorSpace. If the image uses the JPXDecode filter and ImageMask is false, Decode is ignored.
      /// Default value: see “Decode Arrays”.
      /// </summary>
      [KeyInfo(KeyType.Array | KeyType.Optional)]
      public const string Decode = "/Decode";

      /// <summary>
      /// (Optional) A flag indicating whether image interpolation is to be performed. 
      /// Default value: false.
      /// </summary>
      [KeyInfo(KeyType.Boolean | KeyType.Optional)]
      public const string Interpolate = "/Interpolate";

      /// <summary>
      /// (Optional; PDF 1.3) An array of alternate image dictionaries for this image. The order of 
      /// elements within the array has no significance. This entry may not be present in an image 
      /// XObject that is itself an alternate image.
      /// </summary>
      [KeyInfo(KeyType.Array | KeyType.Optional)]
      public const string Alternates = "/Alternates";

      /// <summary>
      /// (Optional; PDF 1.4) A subsidiary image XObject defining a soft-mask image to be used as a 
      /// source of mask shape or mask opacity values in the transparent imaging model. The alpha 
      /// source parameter in the graphics state determines whether the mask values are interpreted as
      /// shape or opacity. If present, this entry overrides the current soft mask in the graphics state,
      /// as well as the image’s Mask entry, if any. (However, the other transparencyrelated graphics 
      /// state parameters—blend mode and alpha constant—remain in effect.) If SMask is absent, the 
      /// image has no associated soft mask (although the current soft mask in the graphics state may
      /// still apply).
      /// </summary>
      [KeyInfo(KeyType.Integer | KeyType.Required)]
      public const string SMask = "/SMask";

      /// <summary>
      /// (Optional for images that use the JPXDecode filter, meaningless otherwise; PDF 1.5)
      /// A code specifying how soft-mask information encoded with image samples should be used:
      /// 0 If present, encoded soft-mask image information should be ignored.
      /// 1 The image’s data stream includes encoded soft-mask values. An application can create
      ///   a soft-mask image from the information to be used as a source of mask shape or mask 
      ///   opacity in the transparency imaging model.
      /// 2 The image’s data stream includes color channels that have been preblended with a 
      ///   background; the image data also includes an opacity channel. An application can create
      ///   a soft-mask image with a Matte entry from the opacity channel information to be used as
      ///   a source of mask shape or mask opacity in the transparency model. If this entry has a 
      ///   nonzero value, SMask should not be specified.
      /// Default value: 0.
      /// </summary>
      [KeyInfo(KeyType.Integer | KeyType.Optional)]
      public const string SMaskInData = "/SMaskInData";

      /// <summary>
      /// (Required in PDF 1.0; optional otherwise) The name by which this image XObject is 
      /// referenced in the XObject subdictionary of the current resource dictionary.
      /// </summary>
      [KeyInfo(KeyType.Name | KeyType.Optional)]
      public const string Name = "/Name";

      /// <summary>
      /// (Required if the image is a structural content item; PDF 1.3) The integer key of the 
      /// image’s entry in the structural parent tree.
      /// </summary>
      [KeyInfo(KeyType.Integer | KeyType.Required)]
      public const string StructParent = "/StructParent";

      /// <summary>
      /// (Optional; PDF 1.3; indirect reference preferred) The digital identifier of the image’s
      /// parent Web Capture content set.
      /// </summary>
      [KeyInfo(KeyType.String | KeyType.Optional)]
      public const string ID = "/ID";

      /// <summary>
      /// (Optional; PDF 1.2) An OPI version dictionary for the image. If ImageMask is true, 
      /// this entry is ignored.
      /// </summary>
      [KeyInfo(KeyType.Dictionary | KeyType.Optional)]
      public const string OPI = "/OPI";

      /// <summary>
      /// (Optional; PDF 1.4) A metadata stream containing metadata for the image.
      /// </summary>
      [KeyInfo(KeyType.Stream | KeyType.Optional)]
      public const string Metadata = "/Metadata";

      /// <summary>
      /// (Optional; PDF 1.5) An optional content group or optional content membership dictionary,
      /// specifying the optional content properties for this image XObject. Before the image is
      /// processed, its visibility is determined based on this entry. If it is determined to be 
      /// invisible, the entire image is skipped, as if there were no Do operator to invoke it.
      /// </summary>
      [KeyInfo(KeyType.Dictionary | KeyType.Optional)]
      public const string OC = "/OC";
    }
#endif
  }
}
