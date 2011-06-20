using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenDentBusiness {
	///<summary>Used by ehr "Generate Patient Lists".  This object represents one row in the grid when building the report.  Multiple such elements will be ANDed together to automatically generate a query.</summary>
	public class EhrPatListElement {
		///<summary>Birthdate, Disease, Medication, or LabResult</summary>
		public EhrRestrictionType Restriction;
		///<summary>For all 4 types, what to compare against.  Examples:  Birthdate: '50', Disease: '4140' (icd9 code will be followed by wildcard), Medication: 'Lisinopril' (not case sensitive, surrounded by wildcards), LabResult: 'HDL-cholesterol'.</summary>
		public string CompareString;
		///<summary>gt, lt, or equal.  Only used for lab and birthdate.</summary>
		public EhrOperand Operand;
		///<summary>Only used for Lab.  Usually a number.</summary>
		public string LabValue;
		///<summary></summary>
		public bool OrderBy;
	}

	public enum EhrRestrictionType {
		Birthdate,
		Disease,
		Medication,
		LabResult
	}

	public enum EhrOperand {
		GreaterThan,
		LessThan,
		Equals
	}
}
