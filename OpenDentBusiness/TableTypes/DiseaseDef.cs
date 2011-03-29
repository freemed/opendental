using System;
using System.Collections;


namespace OpenDentBusiness{

	/// <summary>A list of diseases that can be assigned to patients.  Cannot be deleted if in use by any patients.</summary>
	[Serializable]
	public class DiseaseDef:TableBase {
		///<summary>Primary key.</summary>
		[CrudColumn(IsPriKey=true)]
		public long DiseaseDefNum;
		///<summary>.</summary>
		public string DiseaseName;
		///<summary>The order that the diseases will show in various lists.</summary>
		public int ItemOrder;
		///<summary>If hidden, the disease will still show on any patient that it was previously attached to, but it will not be available for future patients.</summary>
		public bool IsHidden;
		///<summary>The last date and time this row was altered.  Not user editable.</summary>
		[CrudColumn(SpecialType=CrudSpecialColType.TimeStamp)]
		public DateTime DateTStamp;

		///<summary></summary>
		public DiseaseDef Copy() {
			return (DiseaseDef)this.MemberwiseClone();
		}

		
	}

		



		
	

	

	


}










