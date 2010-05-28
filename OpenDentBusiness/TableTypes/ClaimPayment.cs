using System;
using System.Collections;

namespace OpenDentBusiness{

	///<summary>Each row represents a single check from the insurance company.  The amount may be split between patients using claimprocs.  The amount of the check must always exactly equal the sum of all the claimprocs attached to it.  There might be only one claimproc.</summary>
	[Serializable()]
	public class ClaimPayment:TableBase {
		///<summary>Primary key.</summary>
		[CrudColumn(IsPriKey=true)]
		public long ClaimPaymentNum;
		///<summary>Date the check was entered into this system, not the date on the check.</summary>
		public DateTime CheckDate;
		///<summary>The amount of the check.</summary>
		public Double CheckAmt;
		///<summary>The check number.</summary>
		public string CheckNum;
		///<summary>Bank and branch.</summary>
		public string BankBranch;
		///<summary>Note for this check if needed.</summary>
		public string Note;
		///<summary>FK to clinic.ClinicNum.  0 if no clinic.</summary>
		public long ClinicNum;
		///<summary>FK to deposit.DepositNum.  0 if not attached to any deposits.</summary>
		public long DepositNum;
		///<summary>Descriptive name of the carrier just for reporting purposes.</summary>
		public string CarrierName;

		///<summary>Returns a copy of this ClaimPayment.</summary>
		public ClaimPayment Copy(){
			return (ClaimPayment)this.MemberwiseClone();
		}

		
	}

	

	


}









