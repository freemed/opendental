using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenDentBusiness.Mobile {
	[Serializable]
	[CrudTable(IsMobile=true)]
	public class LabPanelm:TableBase {
		///<summary>Primary key 1.</summary>
		[CrudColumn(IsPriKeyMobile1=true)]
		public long CustomerNum;
		///<summary>Primary key 2.</summary>
		[CrudColumn(IsPriKeyMobile2=true)]
		public long LabPanelNum;
		///<summary>FK to patient.PatNum</summary>
		public long PatNum;
		///<summary>FK to medicalorder.MedicalOrderNum.</summary>
		public long MedicalOrderNum;
		///<summary>Both name and address in a single field.  OBR-20</summary>
		public string LabNameAddress;
		///<summary>Snomed code for the type of specimen.  SPM-4.</summary>
		public string SpecimenCode;
		///<summary>Description of the specimen.  Example, Blood venous. SPM-4.</summary>
		public string SpecimenDesc;


		///<summary></summary>
		public LabPanelm Copy() {
			return (LabPanelm)this.MemberwiseClone();
		}



	}
}
