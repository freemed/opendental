using System;
using System.Collections;
using OpenDentBusiness.DataAccess;

namespace OpenDentBusiness{
	///<summary>Only used internally by OpenDental, Inc.  Not used by anyone else.</summary>
	[Serializable()]
	public class PhoneNumber : TableBase{
		///<summary>Primary key.</summary>
		[CrudColumn(IsPriKey=true)]
		public long PhoneNumberNum;
		///<summary>FK to patient.PatNum.</summary>
		public long PatNum;
		///<summary>The actual phone number for the patient.  Includes any punctuation.  No leading 1 or plus, so almost always 10 digits.</summary>
		public string PhoneNumberVal;

	
	}
}















