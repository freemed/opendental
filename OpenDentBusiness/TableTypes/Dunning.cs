using System;
using System.Collections;

namespace OpenDentBusiness{
	
	///<summary>A message that will show on certain patient statements when printing bills.  Criteria must be met in order for the dunning message to show.</summary>
	public class Dunning{
		///<summary>Primary key.</summary>
		public int DunningNum;
		///<summary>The actual dunning message that will go on the patient bill.</summary>
		public string DunMessage;
		///<summary>FK to definition.DefNum.</summary>
		public int BillingType;
		///<summary>This is an int field, but program forces only 0,30,60,or 90.</summary>
		public int AgeAccount;
		///<summary>Enum:YN Set Y to only show if insurance is pending.</summary>
		public YN InsIsPending;

		///<summary>Returns a copy of this Dunning.</summary>
		public Dunning Copy(){
			Dunning d=new Dunning();
			d.DunningNum=DunningNum;
			d.DunMessage=DunMessage;
			d.BillingType=BillingType;
			d.AgeAccount=AgeAccount;
			d.InsIsPending=InsIsPending;
			return d;
		}


	}
	


}













