using System;
using System.Collections;
using System.Collections.Generic;

namespace OpenDentBusiness{

	///<summary>One form or questionnaire filled out by a patient.  Each patient can have multiple forms.</summary>
	public class FormPat{
		///<summary>Primary key.</summary>
		public int FormPatNum;
		///<summary>FK to patient.PatNum.</summary>
		public int PatNum;
		///<summary>The date and time that this questionnaire was filled out.</summary>
		public DateTime FormDateTime;
		///<summary>Not a database field.</summary>
		public List<Question> QuestionList;

		///<summary>Constructor</summary>
		public FormPat(){
			QuestionList=new List<Question>();
		}
		
		///<summary></summary>
		public FormPat Copy(){
			FormPat f=new FormPat();
			f.FormPatNum=FormPatNum;
			f.PatNum=PatNum;
			f.FormDateTime=FormDateTime;
			f.QuestionList=new List<Question>(QuestionList);
			return f;
		}
	}

	
	

}




















