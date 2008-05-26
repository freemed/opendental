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
		///<summary>Notes specific to this Rx.</summary>
		public string Notes;

		///<summary></summary>
		public RxPat Copy() {
			RxPat r=new RxPat();
			r.RxNum=RxNum;
			r.PatNum=PatNum;
			r.RxDate=RxDate;
			r.Drug=Drug;
			r.Sig=Sig;
			r.Disp=Disp;
			r.Refills=Refills;
			r.ProvNum=ProvNum;
			r.Notes=Notes;
			return r;
		}


		
	}

	


}













