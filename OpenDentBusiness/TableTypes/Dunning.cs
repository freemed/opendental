using System;
using System.Collections;

namespace OpenDentBusiness{

	///<summary>A message that will show on certain patient statements when printing bills.  Criteria must be met in order for the dunning message to show.</summary>
	[Serializable]
	public class Dunning:TableBase {
		///<summary>Primary key.</summary>
		[CrudColumn(IsPriKey=true)]
		public long DunningNum;
		///<summary>The actual dunning message that will go on the patient bill.</summary>
		public string DunMessage;
		///<summary>FK to definition.DefNum.</summary>
		public long BillingType;
		///<summary>Program forces only 0,30,60,or 90.</summary>
		public byte AgeAccount;
		///<summary>Enum:YN Set Y to only show if insurance is pending.</summary>
		public YN InsIsPending;
		///<summary>A message that will be copied to the NoteBold field of the Statement.</summary>
		public string MessageBold;

		///<summary>Returns a copy of this Dunning.</summary>
		public Dunning Copy(){
			return (Dunning)this.MemberwiseClone();
		}


	}
	


}













