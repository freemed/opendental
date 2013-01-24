using System;
using System.Collections;
using System.Drawing;

namespace OpenDentBusiness{
	///<summary></summary>
	[Serializable()]
	public class EvaluationDef:TableBase{
		///<summary>Primary key..</summary>
		[CrudColumn(IsPriKey=true)]
		public long EvaluationDefNum;
		///<summary>FK to schoolcourse.SchoolCourseNum</summary>
		public long SchoolCourseNum;
		///<summary>Description</summary>
		public long Description;

		///<summary></summary>
		public EvaluationDef Clone() {
			return (EvaluationDef)this.MemberwiseClone();
		}

	}

	
}




