using System;
using System.Collections;
using System.Drawing;

namespace OpenDentBusiness{
	///<summary></summary>
	[Serializable()]
	public class Evaluation:TableBase{
		///<summary>Primary key..</summary>
		[CrudColumn(IsPriKey=true)]
		public long EvaluationNum;
		///<summary>FK to provider.ProvNum</summary>
		public long InstructNum;
		///<summary>FK to schoolcourse.SchoolCourseNum</summary>
		public long SchoolCourseNum;
		///<summary>Description</summary>
		public string Description;
		///<summary>Date of the evaluation.</summary>
		public DateTime DateEval;

		///<summary></summary>
		public Evaluation Clone() {
			return (Evaluation)this.MemberwiseClone();
		}

	}

	
}




