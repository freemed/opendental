using System;

using System.Collections.Generic;
using System.Text;

namespace OpenDentMobile {
	public class Patient {
		public int PatNum;
		public string LName;
		public string FName;
		public string Preferred;
		public PatientStatus PatStatus;
		public PatientGender Gender;
		public PatientPosition Position;
		public DateTime Birthdate;
		public string Address;
		public string Address2;
		public string City;
		public string State;
		public string HmPhone;
		public string WkPhone;
		public string WirelessPhone;
		public int Guarantor;
		public string CreditType;
		public string FamFinUrgNote;
		public string MedUrgNote;
		///<summary>This is a generated field that is not present in the full version.  CarrierName.</summary>
		public string PrimaryInsurance;

		///<summary>LName, 'Preferred' FName M</summary>
		public string GetNameLF(){
			string retVal="";
			retVal+=LName+", ";
			if(Preferred!=""){
				retVal+="'"+Preferred+"' ";
			}
			retVal+=FName;
			//if(MiddleI!=""){
			//	retVal+=" "+MiddleI;
			//}
			return retVal;
		}


	}
}
