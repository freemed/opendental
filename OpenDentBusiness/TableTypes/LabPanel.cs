using System;
using System.Collections;
using System.Drawing;

namespace OpenDentBusiness{
	///<summary></summary>
	[Serializable]
	public class LabPanel:TableBase{
		///<summary>Primary key.</summary>
		[CrudColumn(IsPriKey=true)]
		public long LabPanelNum;
		///<summary>FK to patient.PatNum</summary>
		public long PatNum;
		///<summary>FK to medicalorder.MedicalOrderNum.  Every lab panel can be attached to one medical order.  If a medical order of type lab does not have an attached lab panel, then it is still pending.</summary>
		public long MedicalOrderNum;
		///<summary>The entire raw HL7 message</summary>
		[CrudColumn(SpecialType=CrudSpecialColType.TextIsClob)]
		public string RawMessage;
		///<summary>Both name and address in a single field.  OBR-20. OBX-26? ORC?</summary>
		public string LabNameAddress;
		///<summary>To be used for synch with web server.</summary>
		[CrudColumn(SpecialType=CrudSpecialColType.TimeStamp)]
		public DateTime DateTStamp;
		///<summary>Snomed code for the type of specimen.  SPM-4.</summary>
		public string SpecimenCode;
		///<summary>Description of the specimen.  Example, Blood venous. SPM-4.</summary>
		public string SpecimenDesc;

		///<summary></summary>
		public LabPanel Copy() {
			return (LabPanel)this.MemberwiseClone();
		}

	}
}