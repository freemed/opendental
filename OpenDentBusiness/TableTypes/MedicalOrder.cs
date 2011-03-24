using System;
using System.Collections;
using System.Drawing;

namespace OpenDentBusiness {
	///<summary></summary>
	[Serializable]
	public class MedicalOrder:TableBase {
		///<summary>Primary key.</summary>
		[CrudColumn(IsPriKey=true)]
		public long MedicalOrderNum;
		///<summary>Enum:MedicalOrderType Laboratory=0,Radiology=1.</summary>
		public MedicalOrderType MedOrderType;
		///<summary>FK to patient.PatNum</summary>
		public long PatNum;
		///<summary></summary>
		[CrudColumn(SpecialType=CrudSpecialColType.DateT)]
		public DateTime DateTimeOrder;

		///<summary></summary>
		public MedicalOrder Copy() {
			return (MedicalOrder)this.MemberwiseClone();
		}
	}

	public enum MedicalOrderType {
		///<summary>0- Laboratory</summary>
		Laboratory=0,
		///<summary>1- Radiology</summary>
		Radiology=1
	}
}