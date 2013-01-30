/*
using System;
using System.Collections;
using System.Drawing;

namespace OpenDentBusiness{
	///<summary>.</summary>
	[Serializable()]
	public class GradingScaleItem:TableBase{
		///<summary>Primary key..</summary>
		[CrudColumn(IsPriKey=true)]
		public long GradingScaleItemNum;
		///<summary>FK to gradingscale.GradingScaleNum</summary>
		public long GradingScaleNum;
		///<summary>Grade. A, B, C, D, F, or 1-10, etc.</summary>
		public string GradeVal;
		///<summary>Description</summary>
		public string Description;

		///<summary></summary>
		public GradingScaleItem Clone() {
			return (GradingScaleItem)this.MemberwiseClone();
		}

	}

	
}




*/