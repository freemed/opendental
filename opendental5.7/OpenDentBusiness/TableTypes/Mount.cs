using System;
using System.Collections.Generic;
using System.Text;
using OpenDental.DataAccess;

namespace OpenDentBusiness {
	/// <summary>A mount shows in the images module just like other images in the tree.  But it is just a container for images within it rather than an actual image itself.</summary>
	[DataObject("mount")]
	public class Mount : DataObjectBase {

		///<summary>Primary key.</summary>
		[DataField("MountNum", PrimaryKey=true, AutoNumber=true)]
		private int mountNum;
		///<summary>Primary key.</summary>
		public int MountNum{get{return mountNum;}set{mountNum=value;MarkDirty();mountNumChanged=true;}}
		bool mountNumChanged;
		public bool MountNumChanged{get{return mountNumChanged;}}

		///<summary>FK to patient.PatNum</summary>
		[DataField("PatNum")]
		private int patNum;
		///<summary>FK to patient.PatNum</summary>
		public int PatNum{get{return patNum;}set{patNum=value;MarkDirty();patNumChanged=true;}}
		bool patNumChanged;
		public bool PatNumChanged{get{return patNumChanged;}}

		///<summary>FK to definition.DefNum. Categories for documents.</summary>
		[DataField("DocCategory")]
		private int docCategory;
		///<summary>FK to definition.DefNum. Categories for documents.</summary>
		public int DocCategory{get{return docCategory;}set{docCategory=value;MarkDirty();docCategoryChanged=true;}}
		bool docCategoryChanged;
		public bool DocCategoryChanged{get{return docCategoryChanged;}}

		///<summary>The date at which the mount itself was created. Has no bearing on the creation date of the images the mount houses.</summary>
		[DataField("DateCreated")]
		private DateTime dateCreated;
		/// <summary>The date at which the mount itself was created. Has no bearing on the creation date of the images the mount houses.</summary>
		public DateTime DateCreated {
			get { return dateCreated; }
			set { dateCreated = value; MarkDirty(); dateCreatedChanged = true; }
		}
		bool dateCreatedChanged;
		public bool DateCreatedChanged {
			get { return dateCreatedChanged; }
		}

		///<summary>Used to provide a document description in the image module tree-view.</summary>
		[DataField("Description")]
		private string description;
		/// <summary>Used to provide a document description in the image module tree-view.</summary>
		public string Description {
			get { return description; }
			set { description = value; MarkDirty(); descriptionChanged = true; }
		}
		bool descriptionChanged;
		public bool DescriptionChanged {
			get { return descriptionChanged; }
		}

		///<summary>To allow the user to enter specific information regarding the exam and tooth numbers, as well as points on interest in the xray images.</summary>
		[DataField("Note")]
		private string note;
		/// <summary>To allow the user to enter specific information regarding the exam and tooth numbers, as well as points on interest in the xray images.</summary>
		public string Note {
			get { return note; }
			set { note = value; MarkDirty(); noteChanged = true; }
		}
		bool noteChanged;
		public bool NoteChanged {
			get { return noteChanged; }
		}

		///<summary>Enum:ImageType This is so that an image can be properly associated with the mount in the image module tree-view.</summary>
		[DataField("ImgType")]
		private ImageType imgType;
		/// <summary>Enum:ImageType This is so that an image can be properly associated with the mount in the image module tree-view.</summary>
		public ImageType ImgType {
			get { return imgType; }
			set { imgType = value; MarkDirty(); imgTypeChanged = true; }
		}
		bool imgTypeChanged;
		public bool ImgTypeChanged {
			get { return imgTypeChanged; }
		}

		///<summary>The static width of the mount, in pixels.</summary>
		[DataField("Width")]
		private int width;
		/// <summary>The static width of the mount, in pixels.</summary>
		public int Width {
			get { return width; }
			set { width = value; MarkDirty(); widthChanged = true; }
		}
		bool widthChanged;
		public bool WidthChanged {
			get { return widthChanged; }
		}

		///<summary>The static height of the mount, in pixels.</summary>
		[DataField("Height")]
		private int height;
		/// <summary>The static height of the mount, in pixels.</summary>
		public int Height {
			get { return height; }
			set { height = value; MarkDirty(); heightChanged = true; }
		}
		bool heightChanged;
		public bool HeightChanged {
			get { return heightChanged; }
		}

		///<summary></summary>
		public Mount Copy() {
			return (Mount)this.MemberwiseClone();
		}

	}
}
