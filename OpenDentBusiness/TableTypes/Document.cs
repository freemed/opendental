using System;
using System.Collections.Generic;
using System.Text;
using OpenDentBusiness.DataAccess;

namespace OpenDentBusiness{
	///<summary>It should be called image, but the name is for historical reasons.  Represents a single document in the images module.</summary>
	[DataObject("document")]
	public class Document : DataObjectBase {

		///<summary>Primary key.</summary>
		[DataField("DocNum", PrimaryKey=true, AutoNumber=true)]
		private long docNum;
		/// <summary>Primary key.</summary>
		public long DocNum {
			get { return docNum; }
			set { docNum = value; MarkDirty(); docNumChanged = true; }
		}
		bool docNumChanged;
		public bool DocNumChanged {
			get { return docNumChanged; }
		}

		///<summary>Description of the document.</summary>
		[DataField("Description")]
		private string description;
		/// <summary>Description of the document.</summary>
		public string Description {
			get { return description; }
			set { description = value; MarkDirty(); descriptionChanged = true; }
		}
		bool descriptionChanged;
		public bool DescriptionChanged {
			get { return descriptionChanged; }
		}

		///<summary>Date created.</summary>
		[DataField("DateCreated")]
		private DateTime dateCreated;
		/// <summary>Date created.</summary>
		public DateTime DateCreated {
			get { return dateCreated; }
			set { dateCreated = value; MarkDirty(); dateCreatedChanged = true; }
		}
		bool dateCreatedChanged;
		public bool DateCreatedChanged {
			get { return dateCreatedChanged; }
		}

		///<summary>FK to definition.DefNum. Categories for documents.</summary>
		[DataField("DocCategory")]
		private long docCategory;
		/// <summary>FK to definition.DefNum. Categories for documents.</summary>
		public long DocCategory {
			get { return docCategory; }
			set { docCategory = value; MarkDirty(); docCategoryChanged = true; }
		}
		bool docCategoryChanged;
		public bool DocCategoryChanged {
			get { return docCategoryChanged; }
		}

		///<summary>FK to patient.PatNum.  Patient folder that document is in.(for sharing situations later)</summary>
		[DataField("PatNum")]
		private long patNum;
		/// <summary>FK to patient.PatNum.  Patient folder that document is in.(for sharing situations later)</summary>
		public long PatNum {
			get { return patNum; }
			set { patNum = value; MarkDirty(); patNumChanged = true; }
		}
		bool patNumChanged;
		public bool PatNumChanged {
			get { return patNumChanged; }
		}

		///<summary>The name of the file. Does not include any directory info.</summary>
		[DataField("FileName")]
		private string fileName;
		/// <summary>The name of the file. Does not include any directory info.</summary>
		public string FileName {
			get { return fileName; }
			set { fileName = value; MarkDirty(); fileNameChanged = true; }
		}
		bool fileNameChanged;
		public bool FileNameChanged {
			get { return fileNameChanged; }
		}

		///<summary>Enum:ImageType eg. document, radiograph, photo, file</summary>
		[DataField("ImgType")]
		private ImageType imgType;
		/// <summary>Enum:ImageType eg. document, radiograph, photo, file</summary>
		public ImageType ImgType {
			get { return imgType; }
			set { imgType = value; MarkDirty(); imgTypeChanged = true; }
		}
		bool imgTypeChanged;
		public bool ImgTypeChanged {
			get { return imgTypeChanged; }
		}

		///<summary>True if flipped horizontally. A vertical flip would be stored as a horizontal flip plus a 180 rotation.</summary>
		[DataField("IsFlipped")]
		private bool isFlipped;
		/// <summary>True if flipped horizontally. A vertical flip would be stored as a horizontal flip plus a 180 rotation.</summary>
		public bool IsFlipped {
			get { return isFlipped; }
			set { isFlipped = value; MarkDirty(); isFlippedChanged = true; }
		}
		bool isFlippedChanged;
		public bool IsFlippedChanged {
			get { return isFlippedChanged; }
		}

		///<summary>Only allowed 0,90,180, and 270.</summary>
		[DataField("DegreesRotated")]
		private long degreesRotated;
		/// <summary>Only allowed 0,90,180, and 270.</summary>
		public long DegreesRotated {
			get { return degreesRotated; }
			set { degreesRotated = value; MarkDirty(); degreesRotatedChanged = true; }
		}
		bool degreesRotatedChanged;
		public bool DegreesRotatedChanged {
			get { return degreesRotatedChanged; }
		}

		///<summary>Incomplete.  An optional list of tooth numbers separated by commas.  The tooth numbers will be in American format and must be processed for display.  When displayed, dashes will be used for sequences of 3 or more tooth numbers.</summary>
		[DataField("ToothNumbers")]
		private string toothNumbers;
		/// <summary>Incomplete.  An optional list of tooth numbers separated by commas.  The tooth numbers will be in American format and must be processed for display.  When displayed, dashes will be used for sequences of 3 or more tooth numbers.</summary>
		public string ToothNumbers {
			get { return toothNumbers; }
			set { toothNumbers = value; MarkDirty(); toothNumbersChanged = true; }
		}
		bool toothNumbersChanged;
		public bool ToothNumbersChanged {
			get { return toothNumbersChanged; }
		}

		///<summary></summary>
		[DataField("Note")]
		private string note;
		/// <summary></summary>
		public string Note {
			get { return note; }
			set { note = value; MarkDirty(); noteChanged = true; }
		}
		bool noteChanged;
		public bool NoteChanged {
			get { return noteChanged; }
		}

		///<summary>True if the signature is in Topaz format rather than OD format.</summary>
		[DataField("SigIsTopaz")]
		private bool sigIsTopaz;
		/// <summary>True if the signature is in Topaz format rather than OD format.</summary>
		public bool SigIsTopaz {
			get { return sigIsTopaz; }
			set { sigIsTopaz = value; MarkDirty(); sigIsTopazChanged = true; }
		}
		bool sigIsTopazChanged;
		public bool SigIsTopazChanged {
			get { return sigIsTopazChanged; }
		}


		///<summary>The encrypted and bound signature in base64 format.  The signature is bound to the byte sequence of the original image.</summary>
		[DataField("Signature")]
		private string signature;
		/// <summary>The encrypted and bound signature in base64 format.  The signature is bound to the byte sequence of the original image.</summary>
		public string Signature {
			get { return signature; }
			set { signature = value; MarkDirty(); signatureChanged = true; }
		}
		bool signatureChanged;
		public bool SignatureChanged {
			get { return signatureChanged; }
		}


		///<summary>Crop rectangle X in original image pixel coordinates.  May be negative.</summary>
		[DataField("CropX")]
		private long cropX;
		/// <summary>Crop rectangle X in original image pixel coordinates.  May be negative.</summary>
		public long CropX {
			get { return cropX; }
			set { cropX = value; MarkDirty(); cropXChanged = true; }
		}
		bool cropXChanged;
		public bool CropXChanged {
			get { return cropXChanged; }
		}


		///<summary>Crop rectangle Y in original image pixel coordinates.  May be negative.</summary>
		[DataField("CropY")]
		private long cropY;
		/// <summary>Crop rectangle Y in original image pixel coordinates.  May be negative.</summary>
		public long CropY {
			get { return cropY; }
			set { cropY = value; MarkDirty(); cropYChanged = true; }
		}
		bool cropYChanged;
		public bool CropYChanged {
			get { return cropYChanged; }
		}


		///<summary>Crop rectangle Width in original image pixel coordinates.  May be zero if no cropping.  May be greater than original image width.</summary>
		[DataField("CropW")]
		private long cropW;
		/// <summary>Crop rectangle Width in original image pixel coordinates.  May be zero if no cropping.  May be greater than original image width.</summary>
		public long CropW {
			get { return cropW; }
			set { cropW = value; MarkDirty(); cropWChanged = true; }
		}
		bool cropWChanged;
		public bool CropWChanged {
			get { return cropWChanged; }
		}


		///<summary>Crop rectangle Height in original image pixel coordinates.  May be zero if no cropping.  May be greater than original image height.</summary>
		[DataField("CropH")]
		private long cropH;
		/// <summary>Crop rectangle Height in original image pixel coordinates.  May be zero if no cropping.  May be greater than original image height.</summary>
		public long CropH {
			get { return cropH; }
			set { cropH = value; MarkDirty(); cropHChanged = true; }
		}
		bool cropHChanged;
		public bool CropHChanged {
			get { return cropHChanged; }
		}


		///<summary>The lower value of the "windowing" (contrast/brightness) for radiographs.  Default is 0.  Max is 255.</summary>
		[DataField("WindowingMin")]
		private long windowingMin;
		/// <summary>The lower value of the "windowing" (contrast/brightness) for radiographs.  Default is 0.  Max is 255.</summary>
		public long WindowingMin {
			get { return windowingMin; }
			set { windowingMin = value; MarkDirty(); windowingMinChanged = true; }
		}
		bool windowingMinChanged;
		public bool WindowingMinChanged {
			get { return windowingMinChanged; }
		}


		///<summary>The upper value of the "windowing" (contrast/brightness) for radiographs.  Default is 0(no windowing).  Max is 255.</summary>
		[DataField("WindowingMax")]
		private long windowingMax;
		/// <summary>The upper value of the "windowing" (contrast/brightness) for radiographs.  Default is 0(no windowing).  Max is 255.</summary>
		public long WindowingMax {
			get { return windowingMax; }
			set { windowingMax = value; MarkDirty(); windowingMaxChanged = true; }
		}
		bool windowingMaxChanged;
		public bool WindowingMaxChanged {
			get { return windowingMaxChanged; }
		}


		///<summary>FK to mountitem.MountItemNum. If set to 0, then no mount item is associated with this document.</summary>
		[DataField("MountItemNum")]
		private long mountItemNum;
		/// <summary>FK to mountitem.MountItemNum. If set to 0, then no mount item is associated with this document.</summary>
		public long MountItemNum {
			get { return mountItemNum; }
			set { mountItemNum = value; MarkDirty(); mountItemNumChanged = true; }
		}
		bool mountItemNumChanged;
		public bool MountItemNumChanged {
			get { return mountItemNumChanged; }
		}

		///<summary>Date/time last altered.  User not allowed to alter.</summary>
		[DataField("DateTStamp")]
		private DateTime dateTStamp;
		/// <summary>Date/time last altered.</summary>
		public DateTime DateTStamp {
			get { return dateTStamp; }
			set { dateTStamp=value; MarkDirty(); dateTStampChanged=true; }
		}
		bool dateTStampChanged;
		public bool DateTStampChanged {
			get { return dateTStampChanged; }
		}


		///<summary>Returns a copy of this Document.</summary>
		public Document Copy() {
			return (Document)this.MemberwiseClone();
		}
	}

	

}
