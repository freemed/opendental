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
		///<summary>Both name and address in a single field.  OBR-20</summary>
		public string LabNameAddress;
		///<summary>OBR-13.  Usually blank.  Example: hemolyzed.</summary>
		public string SpecimenCondition;
		///<summary>OBR-15-1.  Usually blank.  Example: LNA&Arterial Catheter&HL70070.</summary>
		public string SpecimenSource;


		///<summary></summary>
		public LabPanelm Copy() {
			return (LabPanelm)this.MemberwiseClone();
		}



	}
}
