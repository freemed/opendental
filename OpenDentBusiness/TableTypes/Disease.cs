using System;
using System.Collections;

namespace OpenDentBusiness {

	/// <summary>Each row is one disease that one patient has.  Now called a problem in the UI.  Must have either a DiseaseDefNum or an ICD9Num.</summary>
	[Serializable]
	public class Disease:TableBase {
		///<summary>Primary key.</summary>
		[CrudColumn(IsPriKey=true)]
		public long DiseaseNum;
		///<summary>FK to patient.PatNum</summary>
		public long PatNum;
		///<summary>FK to diseasedef.DiseaseDefNum.  The disease description is in that table.  Will be zero if ICD9Num has a value.</summary>
		public long DiseaseDefNum;
		///<summary>Any note about this disease that is specific to this patient.</summary>
		public string PatNote;///<summary>The last date and time this row was altered.  Not user editable.  Will be set to NOW by OD if this patient gets an OnlinePassword assigned.</summary>
		[CrudColumn(SpecialType=CrudSpecialColType.TimeStamp)]
		public DateTime DateTStamp;
		///<summary>FK to icd9.ICD9Num.  Will be zero if DiseaseDefNum has a value.</summary>
		public long ICD9Num;
		///<summary>Enum: ProblemStatus: Active=0, Resolved=1, Inactive=2.</summary>
		public ProblemStatus ProbStatus;


		///<summary></summary>
		public Disease Copy() {
			return (Disease)this.MemberwiseClone();
		}

		
		
		
		
	}

		



		
	

	

	


}










