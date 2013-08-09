using System;
using System.Collections;
using System.Drawing;

namespace OpenDentBusiness{
	///<summary>(Will be) Used in various forms for importing codes and meeting EHR CQMs.</summary>
	[Serializable()]
	public class CQMCode:TableBase{
		///<summary>Primary key..</summary>
		[CrudColumn(IsPriKey=true)]
		public long CQMCodeNum;


		///<summary>.</summary>
		public string CMSID;
		///<summary>.</summary>
		public string ValueSetName;
		///<summary>.</summary>
		public string ValueSetOID;
		///<summary>.</summary>
		public string QDMCategory;
		///<summary>.</summary>
		public string Code;
		///<summary>.</summary>
		[CrudColumn(SpecialType=CrudSpecialColType.TextIsClob)]
		public string Description;
		///<summary>Name of code system. i.e. SNOMEDCT, ICD9CM, CDT, etc...</summary>
		public string CodeSystem;
		///<summary>Official unique identifier assigned to code system. i.e. SNOMEDCT=2.16.840.1.113883.6.96, CDT=2.16.840.1.113883.6.13</summary>
		public string CodeSystemOID;
		///<summary>.</summary>
		public string CodeSystemVersion;

		///<summary></summary>
		public CQMCode Clone() {
			return (CQMCode)this.MemberwiseClone();
		}

	}

	
}




