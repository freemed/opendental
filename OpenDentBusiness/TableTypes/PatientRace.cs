using System;
using System.Collections;
using System.Drawing;

namespace OpenDentBusiness {
	///<summary>Each patient may have multiple races.  Used to represent a race or an ethnicity for a patient.</summary>
	[Serializable]
	public class PatientRace:TableBase {
		///<summary>Primary key.</summary>
		[CrudColumn(IsPriKey=true)]
		public long PatientRaceNum;
		///<summary>FK to patient.PatNum.</summary>
		public long PatNum;
		///<summary>Enum:PatRace </summary>
		public PatRace Race;
		///<summary>FK to cdcrec.CdcrecCode.  This code is mainly for Ehr reporting, but may also be used for other HL7 messages.  Will be blank if they choose a race, like Aboriginal, that is not in the cdcrec code list.  We will initially only use 8 of the cdcrec race codes, see enum below.</summary>
		public string CdcrecCode;

		///<summary></summary>
		public PatientRace Clone() {
			return (PatientRace)this.MemberwiseClone();
		}

	}

	/// <summary>This enum was not able to completely replace the old enum because we keep string representations of the old enums in certain places like sheets and HL7.</summary>
	public enum PatRace {
		///<summary>0 - Hidden for EHR.</summary>
		Aboriginal,
		///<summary>1 - CDCREC:2054-5 Race</summary>
		AfricanAmerican,
		///<summary>2 - CDCREC:1002-5 Race</summary>
		AmericanIndian,
		///<summary>3 - CDCREC:2028-9 Race</summary>
		Asian,
		///<summary>4 - Our hard-coded option for EHR reporting.  One entry represents Declined for both race and ethnicity.</summary>
		DeclinedToSpecify,
		///<summary>5 - CDCREC:2076-8 Race</summary>
		HawaiiOrPacIsland,
		///<summary>6 - CDCREC:2135-2 Ethnicicty.  If EHR is turned on, our UI will force this to be supplemental to a base 'race'.</summary>
		Hispanic,//should be renamed to HispanicOrLatino
		///<summary>7 - We had to keep this for backward compatibility.  Hidden for EHR because it's explicitly not allowed.</summary>
		Multiracial,
		///<summary>8 - CDCREC:2131-1 Race.</summary>
		Other,
		///<summary>9 - CDCREC:2106-3 Race</summary>
		White,
		///<summary>10 - CDCREC:2186-5 Ethnicity.  We originally used the lack of Hispanic to indicate NonHispanic.  Now we are going to explicitly store NonHispanic to make queries for ClinicalQualityMeasures easier.</summary>
		NotHispanic
	}
}