using System;
using System.Collections;

namespace OpenDentBusiness{

	/// <summary>Each row represents one signed agreement to make payments. </summary>
	[Serializable]
	public class PayPlan:TableBase {
		/// <summary>Primary key</summary>
		[CrudColumn(IsPriKey=true)]
		public long PayPlanNum;
		/// <summary>FK to patient.PatNum.  The patient who had the treatment done.</summary>
		public long PatNum;
		/// <summary>FK to patient.PatNum.  The person responsible for the payments.  Does not need to be in the same family as the patient.  Will be 0 if planNum has a value.</summary>
		public long Guarantor;
		/// <summary>Date that the payment plan will display in the account.</summary>
		public DateTime PayPlanDate;
		/// <summary>Annual percentage rate.  eg 18.  This does not take into consideration any late payments, but only the percentage used to calculate the amortization schedule.</summary>
		public double APR;
		///<summary>Generally used to archive the terms when the amortization schedule is created.</summary>
		public string Note;
		///<summary>FK to insplan.PlanNum.  Will be 0 if standard payment plan.  But if this is being used to track expected insurance payments, then this will be the foreign key to insplan.PlanNum and Guarantor will be 0.</summary>
		public long PlanNum;
		///<summary>The amount of the treatment that has already been completed.  This should match the sum of the principal amounts for most situations.  But if the procedures have not yet been completed, and the payment plan is to make any sense, then this number must be changed.</summary>
		public double CompletedAmt;
		///<summary>FK to inssub.InsSubNum.</summary>
		public long InsSubNum;

		///<summary></summary>
		public PayPlan Copy(){
			return (PayPlan)this.MemberwiseClone();
		}

	}

	

	


}










