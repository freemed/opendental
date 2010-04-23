using System;
using System.Collections;
using OpenDentBusiness.DataAccess;

namespace OpenDentBusiness{

	///<summary>Generally used by mobile clinics to track the temporary locations where treatment is performed, such as schools, nursing homes, and community centers.  Replaces the old school table.</summary>
	[Serializable()]
	public class Site : TableBase {
		///<summary>Primary key.</summary>
		[CrudColumn(IsPriKey=true)]
		public long SiteNum;
		///<summary></summary>
		public string Description;
		/// <summary>Notes could include phone, address, contacts, etc.</summary>
		public string Note;

		public Site Copy(){
			return (Site)this.MemberwiseClone();
		}	
	}
}
