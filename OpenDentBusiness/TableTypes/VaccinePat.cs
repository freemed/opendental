using System;
using System.Collections;
using System.Drawing;

namespace OpenDentBusiness {
	///<summary>A vaccine given to a patient on a date.</summary>
	[Serializable]
	public class VaccinePat:TableBase {
		///<summary>Primary key.</summary>
		[CrudColumn(IsPriKey=true)]
		public long VaccinePatNum;
		///<summary>FK to vaccinedef.VaccineDefNum.</summary>
		public long VaccineDefNum;
		///<summary>The datetime that the vaccine was administered.</summary>
		[CrudColumn(SpecialType=CrudSpecialColType.DateT)]
		public DateTime DateTimeStart;
		///<summary>Typically set to the same as DateTimeStart.  User can change.</summary>
		[CrudColumn(SpecialType=CrudSpecialColType.DateT)]
		public DateTime DateTimeEnd;
		///<summary>Size of the dose of the vaccine.  0 indicates unknown and gets converted to 999 on HL7 output.</summary>
		public float AdministeredAmt;
		///<summary>FK to drugunit.DrugUnitNum. Unit of measurement of the AdministeredAmt.  0 represents null.</summary>
		public long DrugUnitNum;
		///<summary>Optional</summary>
		public string LotNumber;
		///<summary>FK to patient.PatNum.</summary>
		public long PatNum;
		///<summary>Set to true if no vaccine given.  Documentation required in the Note.</summary>
		public bool NotGiven;
		///<summary>Documentation sometimes required.</summary>
		public string Note;

		///<summary></summary>
		public VaccinePat Copy() {
			return (VaccinePat)this.MemberwiseClone();
		}

	}
}