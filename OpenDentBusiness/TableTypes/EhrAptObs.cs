using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenDentBusiness {
	///<summary>An EHR appointment observation.  Needed for syndromic surveillance messaging.  Each syndromic message requires at least one observation.</summary>
	[Serializable]
	public class EhrAptObs:TableBase {

		///<summary>Primary key.</summary>
		[CrudColumn(IsPriKey=true)]
		public long EhrAptObsNum;
		///<summary>FK to appointment.AptNum.  There can be an unlimited number of observations per appointment.</summary>
		public long AptNum;
		///<summary>Enum:EhrAptObsType .  Used in HL7 OBX-2 for syndromic surveillance.  Identifies the data type for the observation value in ValReported.</summary>
		public EhrAptObsType ValType;
		///<summary>A LOINC code which identifies the type of observation.  Used in HL7 OBX-3 for syndromic surveillance.</summary>
		public string LoincCode;
		///<summary>The value of the observation. The value format must match the ValType.  This field could be text, a datetime, a code, etc..  Used in HL7 OBX-5 for syndromic surveillance.</summary>
		public string ValReported;
		///<summary>UCUM code.  Used in HL7 OBX-6 for syndromic surveillance when ValType is Numeric (otherwise left blank).</summary>
		public string ValUnit;
		///<summary>When ValType is Coded, then this contains the code system corresponding to the code in ValReported.  When ValType is not Coded, then this field should be blank.
		///Allowed values are LOINC,SNOMEDCT,ICD9,ICD10.</summary>
		public string ValCodeSystem;

		public EhrAptObs Clone() {
			return (EhrAptObs)this.MemberwiseClone();
		}
	}

	public enum EhrAptObsType {
		//Address,//This is also an allowed type in syndromic messaging, but we don't know why we would ever need it.
		Coded,
		DateAndTime,		
		Numeric,
		Text,
	}

}
