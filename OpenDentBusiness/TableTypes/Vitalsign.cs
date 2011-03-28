using System;

namespace OpenDentBusiness {
	///<summary>For EHR module, one dated vital sign entry.</summary>
	[Serializable]
	public class Vitalsign:TableBase {
		///<summary>Primary key.</summary>
		[CrudColumn(IsPriKey=true)]
		public long VitalsignNum;
		///<summary>FK to patient.PatNum.</summary>
		public long PatNum;
		///<summary>Height of patient in inches. Fractions might be needed some day.  Allowed to be 0.</summary>
		public float Height;
		///<summary>Lbs.  Allowed to be 0.</summary>
		public float Weight;
		///<summary>Allowed to be 0.</summary>
		public int BpSystolic;
		///<summary>Allowed to be 0.</summary>
		public int BpDiastolic;
		///<summary>The date that the vitalsigns were taken.</summary>
		public DateTime DateTaken;

		///<summary></summary>
		public Vitalsign Copy() {
			return (Vitalsign)MemberwiseClone();
		}

	}
}
