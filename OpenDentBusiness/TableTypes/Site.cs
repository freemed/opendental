using System;
using System.Collections;
using OpenDental.DataAccess;

namespace OpenDentBusiness{

	///<summary>Generally used by mobile clinics to track the temporary locations where treatment is performed, such as schools, nursing homes, and community centers.  Replaces the old school table.</summary>
	[DataObject("site")]
	public class Site : DataObjectBase {
		[DataField("SiteNum", PrimaryKey=true, AutoNumber=true)]
		private int siteNum;
		bool siteNumChanged;
		///<summary>Primary key.</summary>
		public int SiteNum {
			get { return siteNum; }
			set { if(siteNum!=value){siteNum = value; MarkDirty(); siteNumChanged = true; }}
		}
		public bool SiteNumChanged {
			get { return siteNumChanged; }
		}

		[DataField("Description")]
		private string description;
		bool descriptionChanged;
		///<summary></summary>
		public string Description {
			get { return description; }
			set { if(description!=value){description = value; MarkDirty(); descriptionChanged = true;} }
		}
		public bool DescriptionChanged {
			get { return descriptionChanged; }
		}

		[DataField("Note")]
		private string note;
		bool noteChanged;
		/// <summary>Notes could include phone, address, contacts, etc.</summary>
		public string Note {
			get { return note; }
			set { if(note!=value){note = value; MarkDirty(); noteChanged = true; }}
		}
		public bool NoteChanged {
			get { return noteChanged; }
		}

		public Site Copy(){
			return (Site)Clone();
		}	
	}
}
