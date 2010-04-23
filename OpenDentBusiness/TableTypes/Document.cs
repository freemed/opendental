using System;
using System.Collections.Generic;
using System.Text;

namespace OpenDentBusiness{
	///<summary>It should be called image, but the name is for historical reasons.  Represents a single document in the images module.</summary>
	[Serializable()]
	public class Document:TableBase {
		///<summary>Primary key.</summary>
		[CrudColumn(IsPriKey=true)]
		public long DocNum;
		/// <summary>Description of the document.</summary>
		public string Description;
		/// <summary>Date created.</summary>
		public DateTime DateCreated;
		/// <summary>FK to definition.DefNum. Categories for documents.</summary>
		public long DocCategory;
		/// <summary>FK to patient.PatNum.  Patient folder that document is in.(for sharing situations later)</summary>
		public long PatNum;
		/// <summary>The name of the file. Does not include any directory info.</summary>
		public string FileName;
		/// <summary>Enum:ImageType eg. document, radiograph, photo, file</summary>
		public ImageType ImgType;
		/// <summary>True if flipped horizontally. A vertical flip would be stored as a horizontal flip plus a 180 rotation.</summary>
		public bool IsFlipped;
		/// <summary>Only allowed 0,90,180, and 270.</summary>
		public int DegreesRotated;
		/// <summary>Incomplete.  An optional list of tooth numbers separated by commas.  The tooth numbers will be in American format and must be processed for display.  When displayed, dashes will be used for sequences of 3 or more tooth numbers.</summary>
		public string ToothNumbers;
		/// <summary></summary>
		public string Note;
		/// <summary>True if the signature is in Topaz format rather than OD format.</summary>
		public bool SigIsTopaz;
		/// <summary>The encrypted and bound signature in base64 format.  The signature is bound to the byte sequence of the original image.</summary>
		public string Signature;
		/// <summary>Crop rectangle X in original image pixel coordinates.  May be negative.</summary>
		public int CropX;
		/// <summary>Crop rectangle Y in original image pixel coordinates.  May be negative.</summary>
		public int CropY;
		/// <summary>Crop rectangle Width in original image pixel coordinates.  May be zero if no cropping.  May be greater than original image width.</summary>
		public int CropW;
		/// <summary>Crop rectangle Height in original image pixel coordinates.  May be zero if no cropping.  May be greater than original image height.</summary>
		public int CropH;
		/// <summary>The lower value of the "windowing" (contrast/brightness) for radiographs.  Default is 0.  Max is 255.</summary>
		public int WindowingMin;
		/// <summary>The upper value of the "windowing" (contrast/brightness) for radiographs.  Default is 0(no windowing).  Max is 255.</summary>
		public int WindowingMax;
		/// <summary>FK to mountitem.MountItemNum. If set to 0, then no mount item is associated with this document.</summary>
		public long MountItemNum;
		/// <summary>Date/time last altered.</summary>
		[CrudColumn(SpecialType=EnumCrudSpecialColType.TimeStamp)]
		public DateTime DateTStamp;

		///<summary>Returns a copy of this Document.</summary>
		public Document Copy() {
			return (Document)this.MemberwiseClone();
		}
	}

	

}
