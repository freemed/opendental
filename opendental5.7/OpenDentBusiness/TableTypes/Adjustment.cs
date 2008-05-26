using System;
using System.Collections;

namespace OpenDentBusiness{
	
	///<summary>An adjustment in the patient account.  Usually, adjustments are very simple, just being assigned to one patient and provider.  But they can also be attached to a procedure to represent a discount on that procedure.  Attaching adjustments to procedures is not automated, so it is not very common.</summary>
	public class Adjustment{
		///<summary>Primary key.</summary>
		public int AdjNum;
		///<summary>The date that the adjustment shows in the patient account.</summary>
		public DateTime AdjDate;
		///<summary>Amount of adjustment.  Can be pos or neg.</summary>
		public double AdjAmt;
		///<summary>FK to patient.PatNum.</summary>
		public int PatNum;
		///<summary>FK to definition.DefNum.</summary>
		public int AdjType;
		///<summary>FK to provider.ProvNum.</summary>
		public int ProvNum;
		///<summary>Note for this adjustment.</summary>
		public string AdjNote;
		///<summary>Procedure date.  Not when the adjustment was entered.  This is what the aging will be based on in a future version.</summary>
		public DateTime ProcDate;
		///<summary>FK to procedurelog.ProcNum.  Only used if attached to a procedure.  Otherwise, 0.</summary>
		public int ProcNum;
		///<summary>Timestamp automatically generated and user not allowed to change.  The actual date of entry.</summary>
		public DateTime DateEntry;

		/*///<summary>Returns a copy of this Adjustment.</summary>
		public Adjustment Copy(){
			Adjustment a=new Adjustment();
			a.AdjNum=AdjNum;
			//etc
			return a;
		}*/

		
	}

	


	


}









