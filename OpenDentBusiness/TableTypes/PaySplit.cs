using System;
using System.Collections;

namespace OpenDentBusiness{

	///<summary>Always attached to a payment.  Always affects exactly one patient account and one provider.</summary>
	[Serializable]
	public class PaySplit:TableBase {
		///<summary>Primary key.</summary>
		[CrudColumn(IsPriKey=true)]
		public long SplitNum;
		///<summary>Amount of split.</summary>
		public double SplitAmt;
		///<summary>FK to patient.PatNum.</summary>
		public long PatNum;
		///<summary>Procedure date.  Typically only used if tied to a procedure.  In older versions (before 7.0), this was the date that showed on the account.  Frequently the same as the date of the payment, but not necessarily.  Not when the payment was made.  This is what the aging will be based on in a future version.</summary>
		public DateTime ProcDate;
		///<summary>FK to payment.PayNum.  Every paysplit must be linked to a payment.</summary>
		public long PayNum;
		///<summary>No longer used.</summary>
		public bool IsDiscount;
		///<summary>No longer used</summary>
		public byte DiscountType;
		///<summary>FK to provider.ProvNum.</summary>
		public long ProvNum;
		///<summary>FK to payplan.PayPlanNum.  0 if not attached to a payplan.</summary>
		public long PayPlanNum;
		///<summary>Date always in perfect synch with Payment date.</summary>
		public DateTime DatePay;
		///<summary>FK to procedurelog.ProcNum.  0 if not attached to a procedure.</summary>
		public long ProcNum;
		///<summary>Date this paysplit was created.  User not allowed to edit.</summary>
		public DateTime DateEntry;
		///<summary>FK to definition.DefNum.  Usually 0 unless this is a special unearned split.</summary>
		public long UnearnedType;
		///<summary>FK to clinic.ClinicNum.  Can be 0.  Need not match the ClinicNum of the Payment, because a payment can be split between clinics.</summary>
		public long ClinicNum;

		///<summary>Returns a copy of this PaySplit.</summary>
		public PaySplit Copy(){
			return (PaySplit)this.MemberwiseClone();
		}

		

	}

	

	


}










