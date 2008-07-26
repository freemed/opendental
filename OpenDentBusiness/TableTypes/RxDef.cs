using System;
using System.Collections;

namespace OpenDentBusiness{

	///<summary>Rx definitions.  Can safely delete or alter, because they get copied to the rxPat table, not referenced.</summary>
	public class RxDef{
		///<summary>Primary key.</summary>
		public int RxDefNum;
		///<summary>The name of the drug.</summary>
		public string Drug;
		///<summary>Directions.</summary>
		public string Sig;
		///<summary>Amount to dispense.</summary>
		public string Disp;
		///<summary>Number of refills.</summary>
		public string Refills;
		///<summary>Notes about this drug. Will not be copied to the rxpat.</summary>
		public string Notes;
		///<summary>Is a controlled substance.  This will affect the way it prints.</summary>
		public bool IsControlled;

		///<summary></summary>
		public RxDef Copy() {
			return (RxDef)this.MemberwiseClone();
		}

	
	}

	

	


}













