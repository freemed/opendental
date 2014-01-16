using System;

namespace OpenDentBusiness {
	///<summary>Patient information needed for EHR.  1:1 relation to patient table.  They are stored here because we want to try to keep the size of the patient table a bit smaller.</summary>
	[Serializable]
	public class EhrPatient:TableBase {
		///<summary>FK to patient.PatNum.  Also the primary key for this table. Always one to one relationship with patient table.  A new patient might not have an entry here until needed.</summary>
		[CrudColumn(IsPriKey=true)]
		public long PatNum;
		///<summary>Mother's maiden first name.  Exported in HL7 PID-6 for immunization messages.</summary>
		public string MotherMaidenFname;
		///<summary>Mother's maiden last name.  Exported in HL7 PID-6 for immunization messages.</summary>
		public string MotherMaidenLname;
		///<summary>Enum:YN. Indicates whether or not the patient wants to share their vaccination information with other EHRs.  Used in immunization export.</summary>
		public YN VacShareOk;

		///<summary></summary>
		public EhrPatient Clone() {
			return (EhrPatient)this.MemberwiseClone();
		}

	}
}