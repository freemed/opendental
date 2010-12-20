using System;
using System.Collections;

namespace OpenDentBusiness{

	///<summary>One of the dated charges attached to a payment plan.  This has nothing to do with payments, but rather just causes the amount due to increase on the date of the charge.  The amount of the charge is the sum of the principal and the interest.</summary>
	[Serializable]
	public class PayPlanCharge:TableBase {
		///<summary>Primary key.</summary>
		[CrudColumn(IsPriKey=true)]
		public long PayPlanChargeNum;
		///<summary>FK to payplan.PayPlanNum.</summary>
		public long PayPlanNum;
		///<summary>FK to patient.PatNum.  The guarantor account that each charge will affect.</summary>
		public long Guarantor;
		///<summary>FK to patient.PatNum.  The patient account that the principal gets removed from.</summary>
		public long PatNum;
		///<summary>The date that the charge will show on the patient account.  Any charge with a future date will not show on the account yet and will not affect the balance.</summary>
		public DateTime ChargeDate;
		///<summary>The principal portion of this payment.</summary>
		public double Principal;
		///<summary>The interest portion of this payment.</summary>
		public double Interest;
		///<summary>Any note about this particular payment plan charge</summary>
		public string Note;
		///<summary>FK to provider.ProvNum.  Since there is no ProvNum field at the payplan level, the provider must be the same for all payplancharges.  It's initially assigned as the patient priProv.  Payments applied should be to this provnum, although the current user interface does not help with this.</summary>
		public long ProvNum;
		///<summary>FK to clinic.ClinicNum.  Since there is no ClincNum field at the payplan level, the clinic must be the same for all payplancharges.  It's initially assigned using the patient clinic.  Payments applied should be to this clinic, although the current user interface does not help with this.</summary>
		public long ClinicNum;
		
		///<summary></summary>
		public PayPlanCharge Copy(){
			return (PayPlanCharge)this.MemberwiseClone();
		}

	
	}

	

	


}




















