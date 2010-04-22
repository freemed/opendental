using System;
using System.Collections.Generic;
using System.Text;
using OpenDentBusiness.DataAccess;

namespace OpenDentBusiness {
	/// <summary>A mount shows in the images module just like other images in the tree.  But it is just a container for images within it rather than an actual image itself.</summary>
	[Serializable()]
	public class Mount : TableBase {
		///<summary>Primary key.</summary>
		[CrudColumn(IsPriKey=true)]
		public long MountNum;
		///<summary>FK to patient.PatNum</summary>
		public long PatNum;
		///<summary>FK to definition.DefNum. Categories for documents.</summary>
		public long DocCategory;
		/// <summary>The date at which the mount itself was created. Has no bearing on the creation date of the images the mount houses.</summary>
		public DateTime DateCreated;
		/// <summary>Used to provide a document description in the image module tree-view.</summary>
		public string Description;
		/// <summary>To allow the user to enter specific information regarding the exam and tooth numbers, as well as points on interest in the xray images.</summary>
		public string Note;
		/// <summary>Enum:ImageType This is so that an image can be properly associated with the mount in the image module tree-view.</summary>
		public ImageType ImgType;
		/// <summary>The static width of the mount, in pixels.</summary>
		public int Width;
		/// <summary>The static height of the mount, in pixels.</summary>
		public int Height;

		///<summary></summary>
		public Mount Copy() {
			return (Mount)this.MemberwiseClone();
		}

	}
}
