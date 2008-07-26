using System;

namespace OpenDentBusiness{

	///<summary>One Rx for one patient. Copied from rxdef rather than linked to it.</summary>
	public class RxPat{
		///<summary>Primary key.</summary>
		public int RxNum;
		///<summary>FK to patient.PatNum.</summary>
		public int PatNum;
		///<summary>Date of Rx.</summary>
		public DateTime RxDate;
		///<summary>Drug name.</summary>
		public string Drug;
		///<summary>Directions.</summary>
		public string Sig;
		///<summary>Amount to dispense.</summary>
		public string Disp;
		///<summary>Number of refills.</summary>
		public string Refills;
		///<summary>FK to provider.ProvNum.</summary>
		public int ProvNum;
		///<summary>Notes specific to this Rx.  Will not show on the printout.  For staff use only.</summary>
		public string Notes;
		///<summary>FK to pharmacy.PharmacyNum.</summary>
		public int PharmacyNum;
		///<summary>Is a controlled substance.  This will affect the way it prints.</summary>
		public bool IsControlled;

		///<summary></summary>
		public RxPat Copy() {
			return (RxPat)this.MemberwiseClone();
		}


		
	}

	


}













