using System;
using System.Collections;

namespace OpenDentBusiness{

	///<summary>A clinic is usually a separate physical office location.  If multiple clinics are sharing one database, then this is used.  Patients, Operatories, Claims, and many other types of objects can be assigned to a clinic.</summary>
	[Serializable()]
	public class Clinic:TableBase {
		///<summary>Primary key.  Used in patient,payment,claimpayment,appointment,procedurelog, etc.</summary>
		[CrudColumn(IsPriKey=true)]
		public long ClinicNum;
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
		///<summary>FK to provider.ProvNum.  0=Default practice provider, -1=Treating provider.</summary>
		public long InsBillingProv;

		///<summary>Returns a copy of this Clinic.</summary>
		public Clinic Copy(){
			return (Clinic)this.MemberwiseClone();
		}

	}
	


}













