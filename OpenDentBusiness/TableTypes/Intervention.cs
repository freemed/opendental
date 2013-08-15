using System;

namespace OpenDentBusiness {
	///<summary>An intervention ordered or performed.  Links to a definition in the ehrcode table using the ValueSetID, CodeValue, and CodeSystem.</summary>
	[Serializable]
	public class Intervention:TableBase {
		///<summary>Primary key.</summary>
		[CrudColumn(IsPriKey=true)]
		public long InterventionNum;
		///<summary>FK to patient.PatNum.</summary>
		public long PatNum;
		///<summary>FK to ehrcode.ValueSetOID.  This ID links to a specific value set and QDMCategory in the ehrcode table.  To get the exact item referenced you will have to join on the CodeValue and CodeSystem as well.  The QDMCategory identifies this item as an Intervention.  Example: 2.16.840.1.113883.3.600.1.1525.</summary>
		public string ValueSetOID;
		///<summary>FK to ehrcode.CodeValue.  The code for this item from one of the code systems supported, e.g. ICD9CM or SNOMEDCT.  Example: V65.3 or 418995006.</summary>
		public string CodeValue;
		///<summary>FK to ehrcode.CodeSystem. The code system name for this code.  Possible values are: CPT, HCPCS, ICD9CM, ICD10CM, and SNOMEDCT.</summary>
		public string CodeSystem;
		///<summary>User entered details about the intervention.</summary>
		public string MoreInfo;
		///<summary>The date and time of the intervention.  Automatically set when the intervention is created, but this can be adjusted later.</summary>
		[CrudColumn(SpecialType=CrudSpecialColType.DateTEntryEditable)]
		public DateTime DateTimeEntry;

		///<summary></summary>
		public Intervention Copy() {
			return (Intervention)MemberwiseClone();
		}

	}

}
