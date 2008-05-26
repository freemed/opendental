using System;
using System.Collections;

namespace OpenDentBusiness {

	/// <summary>Each row is one Question for one patient.  If a patient has never filled out a questionnaire, then they will have no rows in this table.</summary>
	public class Question{
		///<summary>Primary key.</summary>
		public int QuestionNum;
		///<summary>FK to patient.PatNum</summary>
		public int PatNum;
		///<summary>The order that this question shows in the list.</summary>
		public int ItemOrder;
		///<summary>The original question.</summary>
		public string Description;
		///<summary>The answer to the question in text form.</summary>
		public string Answer;
		///<summary>FK to formpat.FormPatNum</summary>
		public int FormPatNum;

		///<summary></summary>
		public Question Copy() {
			Question q=new Question();
			q.QuestionNum=QuestionNum;
			q.PatNum=PatNum;
			q.ItemOrder=ItemOrder;
			q.Description=Description;
			q.Answer=Answer;
			q.FormPatNum=FormPatNum;
			return q;
		}

	
		
		
	}

		



		
	

	

	


}










