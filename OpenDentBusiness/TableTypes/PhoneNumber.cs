using System;
using System.Collections;
using OpenDental.DataAccess;

namespace OpenDentBusiness{
	///<summary>Only used internally by OpenDental, Inc.  Not used by anyone else.</summary>
	[DataObject("phonenumber")]
	public class PhoneNumber : DataObjectBase{
		[DataField("PhoneNumberNum",PrimaryKey=true,AutoNumber=true)]
		private int phoneNumberNum;
		private bool phoneNumberNumChanged;
		///<summary>Primary key.</summary>
		public int PhoneNumberNum{
			get{return phoneNumberNum;}
			set{if(phoneNumberNum!=value){phoneNumberNum=value;MarkDirty();phoneNumberNumChanged=true;}}
		}
		public bool PhoneNumberNumChanged{
			get{return phoneNumberNumChanged;}
		}

		[DataField("PatNum")]
		private int patNum;
		private bool patNumChanged;
		///<summary>FK to patient.PatNum.</summary>
		public int PatNum{
			get{return patNum;}
			set{if(patNum!=value){patNum=value;MarkDirty();patNumChanged=true;}}
		}
		public bool PatNumChanged{
			get{return patNumChanged;}
		}

		[DataField("PhoneNumberVal")]
		private string phoneNumberVal;
		private bool phoneNumberValChanged;
		///<summary>The actual phone number for the patient.</summary>
		public string PhoneNumberVal{
			get{return phoneNumberVal;}
			set{if(phoneNumberVal!=value){phoneNumberVal=value;MarkDirty();phoneNumberValChanged=true;}}
		}
		public bool PhoneNumberValChanged{
			get{return phoneNumberValChanged;}
		}

	
	}
}















