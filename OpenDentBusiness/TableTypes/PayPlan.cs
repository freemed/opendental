using System;
using System.Collections;

namespace OpenDentBusiness{
	
	/// <summary>Each row represents one signed agreement to make payments. </summary>
	public class PayPlan{
		/// <summary>Primary key</summary>
		public int PayPlanNum;
		/// <summary>FK to patient.PatNum.  The patient who had the treatment done.</summary>
		public int PatNum;
		/// <summary>FK to patient.PatNum.  The person responsible for the payments.  Does not need to be in the same family as the patient.  Will be 0 if planNum has a value.</summary>
		public int Guarantor;
		/// <summary>Date that the payment plan will display in the account.</summary>
		public DateTime PayPlanDate;
		/// <summary>Annual percentage rate.  eg 18.  This does not take into consideration any late payments, but only the percentage used to calculate the amortization schedule.</summary>
		public double APR;
		///<summary>Generally used to archive the terms when the amortization schedule is created.</summary>
		public string Note;
		///<summary>Will be 0 if standard payment plan.  But if this is being used to track expected insurance payments, then this will be the foreign key to insplan.PlanNum and Guarantor will be 0.</summary>
		public int PlanNum;

		///<summary></summary>
		public PayPlan Copy(){
			PayPlan p=new PayPlan();
			p.PayPlanNum=PayPlanNum;
			p.PatNum=PatNum;
			p.Guarantor=Guarantor;
			p.PayPlanDate=PayPlanDate;
			p.APR=APR;
			p.Note=Note;
			p.PlanNum=PlanNum;
			return p;
		}

	}

	

	


}










