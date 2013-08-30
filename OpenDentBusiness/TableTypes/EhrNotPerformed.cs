using System;

namespace OpenDentBusiness {
	///<summary>For EHR module, these are all the items 'not performed' on patients.  Each row will link to the ehrcode table to retrieve relevant data.  To join this table to the ehrcode table you must join on ValueSetOID, CodeValue, and CodeSystem.  Some items will have associated reasons attached to specify why it was not performed.  Those reasons will also be defined in the ehrcode table, so it may be necessary to join with that table again for the data relevant to the reason.</summary>
	[Serializable]
	public class EhrNotPerformed:TableBase {
		///<summary>Primary key.</summary>
		[CrudColumn(IsPriKey=true)]
		public long EhrNotPerformedNum;
		///<summary>FK to patient.PatNum.</summary>
		public long PatNum;
		///<summary>FK to ehrcode.ValueSetOID.  This ID links to a specific value set and QDMCategory in the ehrcode table.  To get the exact item referenced you will have to join on the CodeValue and CodeSystem as well.  The QDMCategory identifies the type of this item, e.g. Encounter, Procedure, or Medication.  Example: 2.16.840.1.113883.3.526.3.402.</summary>
		public string ValueSetOID;
		///<summary>FK to ehrcode.CodeValue.  The code for this item from one of the code systems supported.  Examples: 666.22 or 10197000.</summary>
		public string CodeValue;
		///<summary>FK to ehrcode.CodeSystem. The code system name for this code.  Possible values are: CDCREC, CDT, CPT, CVX, HCPCS, ICD9CM, ICD10CM, LOINC, RXNORM, SNOMEDCT, SOP, and AdministrativeSex.</summary>
		public string CodeSystem;
		///<summary>FK to ehrcode.ValueSetOID.  This ID links to a specific value set and QDMCategory in the ehrcode table.  To get the exact item referenced you will have to join on the CodeValueReason and CodeSystemReason as well.  The QDMCategory identifies the type of this item, e.g. Attribute.  Example: 2.16.840.1.113883.3.526.3.1007.</summary>
		public string ValueSetOIDReason;
		///<summary>FK to ehrcode.CodeValue.  The code for this item from one of the code systems supported.  Examples: 666.22 or 10197000.</summary>
		public string CodeValueReason;
		///<summary>FK to ehrcode.CodeSystem. The code system name for this code.  Possible values are: CDCREC, CDT, CPT, CVX, HCPCS, ICD9CM, ICD10CM, LOINC, RXNORM, SNOMEDCT, SOP, and AdministrativeSex.</summary>
		public string CodeSystemReason;
		///<summary>The date and time this item was created.  Can be edited to the date and time the item actually occurred.</summary>
		[CrudColumn(SpecialType=CrudSpecialColType.DateTEntryEditable)]
		public DateTime DateTimeEntry;

		///<summary></summary>
		public EhrNotPerformed Copy() {
			return (EhrNotPerformed)MemberwiseClone();
		}

	}

}
