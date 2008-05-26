using System;
using System.Collections;

namespace OpenDentBusiness{
	
	///<summary>A clinic is usually a separate physical office location.  If multiple clinics are sharing one database, then this is used.  Patients, Operatories, Claims, and many other types of objects can be assigned to a clinic.</summary>
	public class Clinic{
		///<summary>Primary key.  Used in patient,payment,claimpayment,appointment,procedurelog</summary>
		public int ClinicNum;
		///<summary>.</summary>
		public string Description;
		///<summary>.</summary>
		public string Address;
		///<summary>Second line of address.</summary>
		public string Address2;
		///<summary>.</summary>
		public string City;
		///<summary>2 char in the US.</summary>
		public string State;
		///<summary></summary>
		public string Zip;
		///<summary>Does not include any punctuation.  Should be exactly 10 digits</summary>
		public string Phone;
		///<summary>The account number for deposits.</summary>
		public string BankNumber;
		///<summary>Enum:PlaceOfService Usually 0 unless a mobile clinic for instance.</summary>
		public PlaceOfService DefaultPlaceService;

		///<summary>Returns a copy of this Clinic.</summary>
		public Clinic Copy(){
			Clinic c=new Clinic();
			c.ClinicNum=ClinicNum;
			c.Description=Description;
			c.Address=Address;
			c.Address2=Address2;
			c.City=City;
			c.State=State;
			c.Zip=Zip;
			c.Phone=Phone;
			c.BankNumber=BankNumber;
			c.DefaultPlaceService=DefaultPlaceService;
			return c;
		}

	}
	


}













