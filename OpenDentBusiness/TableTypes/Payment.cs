using System;
using System.Collections;

namespace OpenDentBusiness{

	///<summary>A patient payment.  Always has at least one split.</summary>
	[Serializable]
	public class Payment:TableBase {
		///<summary>Primary key.</summary>
		[CrudColumn(IsPriKey=true)]
		public long PayNum;
		///<summary>FK to definition.DefNum.  This will be 0 if this is an income transfer to another provider.</summary>
		public long PayType;
		///<summary>The date that the payment displays on the patient account.</summary>
		public DateTime PayDate;
		///<summary>Amount of the payment.  Must equal the sum of the splits.</summary>
		public double PayAmt;
		///<summary>Check number is optional.</summary>
		public string CheckNum;
		///<summary>Bank-branch for checks.</summary>
		public string BankBranch;
		///<summary>Any admin note.  Not for patient to see.</summary>
		public string PayNote;
		///<summary>Set to true to indicate that a payment is split.  Just makes a few functions easier.  Might be eliminated.</summary>
		public bool IsSplit;
		///<summary>FK to patient.PatNum.  The patient where the payment entry will show.  But only the splits affect account balances.  This has a value even if the 'payment' is actually an income transfer to another provider.</summary>
		public long PatNum;
		///<summary>FK to clinic.ClinicNum.  Can be 0. Copied from patient.ClinicNum when creating payment, but user can override.  Not used in provider income transfers.  Cannot be used in financial reporting when grouping by clinic, because payments may be split between clinics.</summary>
		public long ClinicNum;
		///<summary>The date that this payment was entered.  Not user editable.</summary>
		public DateTime DateEntry;
		///<summary>FK to deposit.DepositNum.  0 if not attached to any deposits.  Cash does not usually get attached to a deposit; only checks.</summary>
		[CrudColumn(SpecialType=CrudSpecialColType.ExcludeFromUpdate)]
		public long DepositNum;
		///<summary>Text of printed receipt if the payment was done electronically. Allows reprinting if needed. Only used for PayConnect at the moment, but plans to use for XCharge as well.</summary>
		public string Receipt;


		

	}

	

	

}










