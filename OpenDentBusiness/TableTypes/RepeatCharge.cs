using System;
using System.Collections;

namespace OpenDentBusiness{

	/// <summary>Each row represents one charge that will be added monthly.</summary>
	[Serializable]
	public class RepeatCharge:TableBase {
		/// <summary>Primary key</summary>
		[CrudColumn(IsPriKey=true)]
		public long RepeatChargeNum;
		/// <summary>FK to patient.PatNum.</summary>
		public long PatNum;
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
		///<summary>Indicates that the note should be copied to the corresponding procedure billing note.</summary>
		public bool CopyNoteToProc;
		///<summary>Set to true to have a claim automatically created for the patient with the procedure that is attached to this repeating charge.</summary>
		public bool CreatesClaim;
		///<summary>Defaulted to true.  Set to false to disable the repeating charge.  This allows patients to have repeating charges in their history that are not active.  Used mainly for repeating charges with notes that should not be deleted.</summary>
		public bool IsEnabled;

		///<summary></summary>
		public RepeatCharge Copy(){
			return (RepeatCharge)this.MemberwiseClone();
		}

		

		

		


	}

	

	


}










