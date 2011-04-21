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
		///<summary>FK to medicalorder.MedicalOrderNum.</summary>
		public long MedicalOrderNum;
		///<summary>The entire raw HL7 message</summary>
		public string RawMessage;
		///<summary>Both name and address in a single field.  From OBR-20</summary>
		public string Length;
		///<summary>To be used for synch with web server.</summary>
		public DateTime DateTStamp;

		///<summary></summary>
		public LabPanel Copy() {
			return (LabPanel)this.MemberwiseClone();
		}

	}
}