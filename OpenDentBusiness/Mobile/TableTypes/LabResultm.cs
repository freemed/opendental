using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenDentBusiness.Mobile {
	[Serializable]
	[CrudTable(IsMobile=true)]
	public class LabResultm:TableBase {
		///<summary>Primary key 1.</summary>
		[CrudColumn(IsPriKeyMobile1=true)]
		public long CustomerNum;
		///<summary>Primary key 2.</summary>
		[CrudColumn(IsPriKeyMobile2=true)]
		public long LabResultNum;
		///<summary>FK to labpanel.LabPanelNum.</summary>
		public long LabPanelNum;
		///<summary>OBX-14.</summary>
		[CrudColumn(SpecialType=CrudSpecialColType.DateT)]
		public DateTime DateTimeTest;
		///<summary>OBX-3, text portion.</summary>
		public string TestName;
		///<summary>OBX-3 id portion, LOINC.  For example, 10676-5.</summary>
		public string TestID;
		///<summary>Enum:LabObsValueType as defined in HL7 docs. NM=numeric.</summary>
		public LabObsValueType ValueType;
		///<summary>Value always stored as a string because the type can vary.</summary>
		public string ObsValue;
		///<summary>FK to drugunit.DrugUnitNum.  For example, mL.</summary>
		public long DrugUnitNum;

		///<summary></summary>
		public LabResultm Copy() {
			return (LabResultm)this.MemberwiseClone();
		}



	}
}
