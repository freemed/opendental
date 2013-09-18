using System;

namespace OpenDentBusiness {
	///<summary>An intervention ordered or performed.  Examples: smoking cessation and weightloss counseling.  Links to a definition in the ehrcode table using the CodeValue and CodeSystem.</summary>
	[Serializable]
	public class Intervention:TableBase {
		///<summary>Primary key.</summary>
		[CrudColumn(IsPriKey=true)]
		public long InterventionNum;
		///<summary>FK to patient.PatNum.</summary>
		public long PatNum;
		///<summary>FK to provider.ProvNum. </summary>
		public long ProvNum;
		///<summary>FK to ehrcode.CodeValue.  This code may not exist in the ehrcode table, it may have been chosen from a bigger list of available codes.  In that case, this will be a FK to a specific code system table identified by the CodeSystem column.  The code for this item from one of the code systems supported.  Examples: V65.3 or 418995006.</summary>
		public string CodeValue;
		///<summary>FK to codesystem.CodeSystemName. The code system name for this code.  Possible values are: CPT, HCPCS, ICD9CM, ICD10CM, and SNOMEDCT.</summary>
		public string CodeSystem;
		///<summary>User-entered details about the intervention for this patient.  Max length 4000.</summary>
		public string Note;
		///<summary>The date and time of the intervention.  Automatically set when the intervention is created, but this can be adjusted later.</summary>
		[CrudColumn(SpecialType=CrudSpecialColType.DateTEntryEditable)]
		public DateTime DateTimeEntry;

		///<summary></summary>
		public Intervention Copy() {
			return (Intervention)MemberwiseClone();
		}

	}

}
