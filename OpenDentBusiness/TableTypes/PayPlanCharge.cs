using System;
using System.Collections;

namespace OpenDentBusiness{

	///<summary>One of the dated charges attached to a payment plan.  This has nothing to do with payments, but rather just causes the amount due to increase on the date of the charge.  The amount of the charge is the sum of the principal and the interest.</summary>
	public class PayPlanCharge{
		///<summary>Primary key.</summary>
		public int PayPlanChargeNum;
		///<summary>FK to payplan.PayPlanNum.</summary>
		public int PayPlanNum;
		///<summary>FK to patient.PatNum.  The guarantor account that each charge will affect.</summary>
		public int Guarantor;
		///<summary>FK to patient.PatNum.  The patient account that the principal gets removed from.</summary>
		public int PatNum;
		///<summary>The date that the charge will show on the patient account.  Any charge with a future date will not show on the account yet and will not affect the balance.</summary>
		public DateTime ChargeDate;
		///<summary>The principal portion of this payment.</summary>
		public double Principal;
		///<summary>The interest portion of this payment.</summary>
		public double Interest;
		///<summary>Any note about this particular payment plan charge</summary>
		public string Note;
		
		///<summary></summary>
		public PayPlanCharge Copy(){
			PayPlanCharge p=new PayPlanCharge();
			p.PayPlanChargeNum=PayPlanChargeNum;
			p.PayPlanNum=PayPlanNum;
			p.Guarantor=Guarantor;
			p.PatNum=PatNum;
			p.ChargeDate=ChargeDate;
			p.Principal=Principal;
			p.Interest=Interest;
			p.Note=Note;
			return p;
		}

	
	}

	

	


}




















