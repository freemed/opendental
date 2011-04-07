using System;
using System.Collections;
using System.Drawing;

namespace OpenDentBusiness {
	///<summary>A medication on a formulary.</summary>
	[Serializable]
	public class FormularyMed:TableBase {
		///<summary>Primary key.</summary>
		[CrudColumn(IsPriKey=true)]
		public long FormularyMedNum;
		///<summary>FK to Formulary.</summary>
		public long FormularyNum;
		///<summary>FK to Medication.</summary>
		public long MedicationNum;

		///<summary></summary>
		public FormularyMed Copy() {
			return (FormularyMed)this.MemberwiseClone();
		}


	}
}
