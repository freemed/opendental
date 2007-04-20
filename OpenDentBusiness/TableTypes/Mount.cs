using System;
using System.Collections.Generic;
using System.Text;

namespace OpenDentBusiness {
	/// <summary>These can be freely deleted, renamed, moved, etc. without affecting any patient info.  mountitem</summary>
	public class Mount{
		///<summary>Primary key.</summary>
		public int MountNum;
		///<summary>FK to patient.PatNum</summary>
		public int PatNum;
		///<summary>The category enumeration/folder that this mount belongs to in the image module tree-view.</summary>
		public int DocCategory;
		///<summary>The date at which the mount itself was created. Has no bearing on the creation date of the images the mount houses.</summary>
		public DateTime DateCreated;
		///<summary>Used to provide a document description in the image module tree-view.</summary>
		public string Description;
		///<summary>Enum:ImageType This is so that an image can be properly associated with the mount in the image module tree-view.</summary>
		public ImageType ImgType;

		///<summary></summary>
		public Mount Copy() {
			Mount m=new Mount();
			m.MountNum=MountNum;
			m.PatNum=PatNum;
			m.DocCategory=DocCategory;
			m.DateCreated=DateCreated;
			m.Description=Description;
			return m;
		}

	}
}
