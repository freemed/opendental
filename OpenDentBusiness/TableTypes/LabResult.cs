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
		///<summary>From OBX-14.</summary>
		public DateTime DateTest;
		///<summary>From OBX-3.</summary>
		public string TestPerformed;
		///<summary>To be used for synch with web server.</summary>
		public DateTime DateTStamp;

		///<summary></summary>
		public LabResult Copy() {
			return (LabResult)this.MemberwiseClone();
		}

	}
}