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
		///<summary>Enum:PatRace.</summary>
		public PatRace Race;

		///<summary></summary>
		public PatientRace Clone() {
			return (PatientRace)this.MemberwiseClone();
		}

	}

	public enum PatRace {
		///<summary>0</summary>
		Aboriginal,
		///<summary>1</summary>
		AfricanAmerican,
		///<summary>2</summary>
		AmericanIndian,
		///<summary>3</summary>
		Asian,
		///<summary>4</summary>
		DeclinedToSpecify,
		///<summary>5</summary>
		HawaiiOrPacIsland,
		///<summary>6</summary>
		Hispanic,
		///<summary>7</summary>
		Multiracial,
		///<summary>8</summary>
		NotHispanic,
		///<summary>9</summary>
		Other,
		///<summary>10</summary>
		Unknown,
		///<summary>11</summary>
		White
	}
}