using System;
using System.Collections;

namespace OpenDentBusiness{
	
	///<summary>Always attached to a payment.  Always affects exactly one patient account and one provider.</summary>
	public class PaySplit{
		///<summary>Primary key.</summary>
		public int SplitNum;
		///<summary>Amount of split.</summary>
		public double SplitAmt;
		///<summary>FK to patient.PatNum.</summary>
		public int PatNum;
		///<summary>Procedure date.  This is the date that shows on the account.  Frequently the same as the date of the payment, but not necessarily.  Not when the payment was made.  This is what the aging will be based on in a future version.</summary>
		public DateTime ProcDate;
		///<summary>FK to payment.PayNum.  Every paysplit must be linked to a payment.</summary>
		public int PayNum;
		///<summary>No longer used.</summary>
		public bool IsDiscount;
		///<summary>No longer used</summary>
		public int DiscountType;
		///<summary>FK to provider.ProvNum.</summary>
		public int ProvNum;
		///<summary>FK to payplan.PayPlanNum.  0 if not attached to a payplan.</summary>
		public int PayPlanNum;
		///<summary>Date always in perfect synch with Payment date.</summary>
		public DateTime DatePay;
		/// <summary></summary>
		public int ProcNum;
		///<summary>Date this paysplit was created.  User not allowed to edit.</summary>
		public DateTime DateEntry;

		///<summary>Returns a copy of this PaySplit.</summary>
		public PaySplit Copy(){
			PaySplit p=new PaySplit();
			p.SplitNum=SplitNum;
			p.SplitAmt=SplitAmt;
			p.PatNum=PatNum;
			p.ProcDate=ProcDate;
			p.PayNum=PayNum;
			p.ProvNum=ProvNum;
			p.PayPlanNum=PayPlanNum;
			p.DatePay=DatePay;
			p.ProcNum=ProcNum;
			p.DateEntry=DateEntry;
			return p;
		}

		

	}

	

	


}










