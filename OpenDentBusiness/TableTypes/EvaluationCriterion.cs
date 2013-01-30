/*
using System;
using System.Collections;
using System.Drawing;

namespace OpenDentBusiness{
	///<summary></summary>
	[Serializable()]
	public class EvaluationCriterion:TableBase{
		///<summary>Primary key..</summary>
		[CrudColumn(IsPriKey=true)]
		public long EvaluationCriterionNum;
		///<summary>FK to evaluation.EvaluationNum</summary>
		public long EvaluationNum;
		///<summary>Category (is this a MySQL reserved word?). Evaluation criteria with the same category are listed together.</summary>
		public string Category;
		///<summary>FK to gradingscaleitem.GradingScaleItemNum</summary>
		public long GradingScaleItemNum;

		///<summary></summary>
		public EvaluationCriterion Clone() {
			return (EvaluationCriterion)this.MemberwiseClone();
		}

	}

	
}




*/