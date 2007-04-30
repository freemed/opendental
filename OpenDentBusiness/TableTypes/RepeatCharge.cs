using System;
using System.Collections;

namespace OpenDentBusiness{
	
	/// <summary>Each row represents one charge that will be added monthly.</summary>
	public class RepeatCharge{
		/// <summary>Primary key</summary>
		public int RepeatChargeNum;
		/// <summary>FK to patient.PatNum.</summary>
		public int PatNum;
		///<summary>FK to procedurecode.ProcCode.  The code that will be added to the account as a completed procedure.</summary>
		public string ProcCode;
		///<summary>The amount that will be charged.  The amount from the procedurecode will not be used.  This way, a repeating charge cannot be accidentally altered.</summary>
		public double ChargeAmt;
		///<summary>The date of the first charge.  Charges will always be added on the same day of the month as the start date.  If more than one month goes by, then multiple charges will be added.</summary>
		public DateTime DateStart;
		///<summary>The last date on which a charge is allowed.  So if you want 12 charges, and the start date is 8/1/05, then the stop date should be 7/1/05, not 8/1/05.  Can be blank (0001-01-01) to represent a perpetual repeating charge.</summary>
		public DateTime DateStop;
		///<summary>Any note for internal use.</summary>
		public string Note;	

		///<summary></summary>
		public RepeatCharge Copy(){
			RepeatCharge r=new RepeatCharge();
			r.RepeatChargeNum=RepeatChargeNum;
			r.PatNum=PatNum;
			r.ProcCode=ProcCode;
			r.ChargeAmt=ChargeAmt;
			r.DateStart=DateStart;
			r.DateStop=DateStop;
			r.Note=Note;
			return r;
		}

		

		

		


	}

	

	


}










