/*
using System;
using System.Collections;
using System.Drawing;

namespace OpenDentBusiness{
	///<summary></summary>
	[Serializable()]
	public class EvaluationCriterionDef:TableBase{
		///<summary>Primary key..</summary>
		[CrudColumn(IsPriKey=true)]
		public long EvaluationCriterionDefNum;
		///<summary>FK to evaluationdef.EvaluationDefNum</summary>
		public long EvaluationDefNum;
		///<summary>Category (is this a MySQL reserved word?). Evaluation criteria with the same category are listed together.</summary>
		public string Category;

		///<summary></summary>
		public EvaluationCriterionDef Clone() {
			return (EvaluationCriterionDef)this.MemberwiseClone();
		}

	}

	
}




*/