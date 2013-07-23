using System;
using System.Collections;
using System.Drawing;

namespace OpenDentBusiness {
	///<summary></summary>
	[Serializable]
	public class PatientRace:TableBase {
		///<summary>Primary key.</summary>
		[CrudColumn(IsPriKey=true)]
		public long PatientRaceNum;
		///<summary>FK to patient.PatNum.</summary>
		public long PatNum;
		///<summary>Enum:PatRace </summary>
		public PatRace Race;

		///<summary></summary>
		public PatientRace Clone() {
			return (PatientRace)this.MemberwiseClone();
		}

	}

	public enum PatRace {
		///<summary>0 - Hidden for EHR.</summary>
		Aboriginal,
		///<summary>1</summary>
		AfricanAmerican,
		///<summary>2</summary>
		AmericanIndian,
		///<summary>3</summary>
		Asian,
		///<summary>4 - Our hard-coded option for EHR reporting.</summary>
		DeclinedToSpecify,
		///<summary>5</summary>
		HawaiiOrPacIsland,
		///<summary>6 - If EHR is turned on, our UI will force this to be supplemental to a base 'race'.</summary>
		Hispanic,
		///<summary>7 - We had to keep this for backward compatibility.  Hidden for EHR.</summary>
		Multiracial,
		///<summary>8 - Hidden for EHR.</summary>
		Other,
		///<summary>9</summary>
		White
	}
}