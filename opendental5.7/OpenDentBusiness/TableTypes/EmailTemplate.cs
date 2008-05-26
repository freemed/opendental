using System;
using System.Collections;

namespace OpenDentBusiness{

	///<summary>A template email which can be used as the basis for a new email.</summary>
	public class EmailTemplate{
		///<summary>Primary key.</summary>
		public int EmailTemplateNum;
		///<summary>Default subject line.</summary>
		public string Subject;
		///<summary>Body of the email</summary>
		public string BodyText;

		///<summary>Returns a copy of this EmailTemplate.</summary>
		public EmailTemplate Copy(){
			EmailTemplate t=new EmailTemplate();
			t.EmailTemplateNum=EmailTemplateNum;
			t.Subject=Subject;
			t.BodyText=BodyText;
			return t;
		}

		

		
		
	}

	
	

}













