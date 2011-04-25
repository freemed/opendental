using System;
using System.Collections;
using System.Drawing;

namespace OpenDentBusiness{
	///<summary></summary>
	[Serializable]
	public class LabResult:TableBase{
		///<summary>Primary key.</summary>
		[CrudColumn(IsPriKey=true)]
		public long LabResultNum;
		///<summary>FK to labpanel.LabPanelNum.</summary>
		public long LabPanelNum;
		///<summary>OBX-14.</summary>
		[CrudColumn(SpecialType=CrudSpecialColType.DateT)]
		public DateTime DateTimeTest;
		///<summary>OBX-3, text portion.</summary>
		public string TestName;
		///<summary>To be used for synch with web server.</summary>
		[CrudColumn(SpecialType=CrudSpecialColType.TimeStamp)]
		public DateTime DateTStamp;
		///<summary>OBX-3 id portion, LOINC.  For example, 10676-5.</summary>
		public string TestID;
		///<summary>Enum:LabObsValueType as defined in HL7 docs. NM=numeric.</summary>
		public LabObsValueType ValueType;
		///<summary>Value always stored as a string because the type can vary.</summary>
		public string ObsValue;
		///<summary>FK to drugunit.DrugUnitNum.  For example, mL.</summary>
		public long DrugUnitNum;

		///<summary></summary>
		public LabResult Copy() {
			return (LabResult)this.MemberwiseClone();
		}

	}

	///<summary>Defined in HL7 table 0125.</summary>
	public enum LabObsValueType {
		///<summary>0</summary>
		None,
		///<summary>1-Numeric</summary>
		NM
	}
}