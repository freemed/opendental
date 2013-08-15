using System;
using System.Collections;
using System.Drawing;

namespace OpenDentBusiness {
	///<summary>For EHR module, clinical quality measure code set.</summary>
	[Serializable]
	public class EhrCode:TableBase {
		///<summary>Primary key.</summary>
		[CrudColumn(IsPriKey=true)]
		public long EhrCodeNum;
		///<summary>Clinical quality measure test ID's that utilize this code.  Comma delimited list.</summary>
		public string MeasureIds;
		///<summary>The National Library of Medicine Value Set Authority Center assigned value set name.</summary>
		public string ValueSetName;
		///<summary>The value set object identifier for reporting CQM.</summary>
		public string ValueSetOID;
		///<summary>The Quality Data Model category for this code.</summary>
		public string QDMCategory;
		///<summary>The code from the specified code system.</summary>
		public string CodeValue;
		///<summary>The description for this code.</summary>
		public string Description;
		///<summary>The code system name for this code.  Possible values are: CDCREC, CDT, CPT, CVX, HCPCS, ICD9CM, ICD10CM, LOINC, RXNORM, SNOMEDCT, SOP, and AdministrativeSex.</summary>
		public string CodeSystem;
		///<summary>The code system object identifier for reporting CQM.</summary>
		public string CodeSystemOID;

		///<summary></summary>
		public EhrCode Copy() {
			return (EhrCode)MemberwiseClone();
		}

	}

}
