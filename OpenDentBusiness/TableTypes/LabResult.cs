using System;
using System.Collections;
using System.Drawing;

namespace OpenDentBusiness{
	///<summary>Medical labs, not dental labs.  Multiple labresults are attached to a labpanel.  Loosely corresponds to the OBX segment in HL7.</summary>
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
		///<summary>OBX-3, id portion, LOINC.  For example, 10676-5.</summary>
		public string TestID;
		//<summary>Enum:LabObsValueType as defined in HL7 docs. OBX-2. NM=numeric.</summary>
		//public LabObsValueType ValueType;
		///<summary>OBX-5. Value always stored as a string because the type might vary in the future.</summary>
		public string ObsValue;
		///<summary>FK to drugunit.DrugUnitNum.  OBX-6  For example, mL.</summary>
		public long DrugUnitNum;

		///<summary></summary>
		public LabResult Copy() {
			return (LabResult)this.MemberwiseClone();
		}

	}

	
}