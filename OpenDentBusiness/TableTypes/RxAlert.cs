using System;
using System.Collections;

namespace OpenDentBusiness {

	/// <summary>Many-to-many relationship connecting RxDef with AllergyDef.</summary>
	[Serializable]
	public class RxAlert:TableBase {
		///<summary>Primary key.</summary>
		[CrudColumn(IsPriKey=true)]
		public long RxAlertNum;
		///<summary>FK to rxdef.RxDefNum.  This alert is to be shown when user attempts to write an Rx for this RxDef.</summary>
		public long RxDefNum;
		///<summary>Deprecated, but we left these rows in the db.</summary>
		public long DiseaseDefNum;
		///<summary>FK to allergydef.AllergyDefNum.  Compared against allergy.AllergyDefNum using PatNum.  Drug-Allergy checking is also perfomed in NewCrop.</summary>
		public long AllergyDefNum;
		///<summary>Deprecated, but we left these rows in the db.</summary>
		public long MedicationNum;
		///<summary>This is typically blank, so a default message will be displayed by OD.  But if this contains a message, then this message will be used instead.</summary>
		public string NotificationMsg;
		///<summary>Deprecated</summary>
		public bool IsHighSignificance;

		///<summary></summary>
		public RxAlert Copy() {
			return (RxAlert)this.MemberwiseClone();
		}

		
		
	}

		



		
	

	

	


}










