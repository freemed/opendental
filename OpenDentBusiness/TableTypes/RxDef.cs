using System;
using System.Collections;

namespace OpenDentBusiness{

	///<summary>Rx definitions.  Can safely delete or alter, because they get copied to the rxPat table, not referenced.</summary>
	[Serializable]
	public class RxDef:TableBase {
		///<summary>Primary key.</summary>
		[CrudColumn(IsPriKey=true)]
		public long RxDefNum;
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
		///<summary>RxNorm Code identifier.  This is used to enhance the RxAlert functionality.  Usually, RxAlerts are triggered when an Rx is entered from an RxDef.  But if an alert needs to be triggered when entering a medication through the ehr CPOE, then this RxCui matching the RxCui of the medication is the trigger.  This is clearly not practical because there are so many RxCuis that an exact match would be extremely rare.  So the only reason this field is here is to pass ehr certification.  We should have used a string type.</summary>
		public long RxCui;

		///<summary></summary>
		public RxDef Copy() {
			return (RxDef)this.MemberwiseClone();
		}

	
	}

	

	


}













