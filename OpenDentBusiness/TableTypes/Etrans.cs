using System;
using System.Collections;

namespace OpenDentBusiness{
	
	/// <summary>One electronic transaction.  Typically, one claim or response.  Or one benefit request or response.  Is constantly being expanded to include more types of transactions with clearinghouses.  Also stores printing of paper claims.  Sometimes stores a copy of what was sent.</summary>
	public class Etrans{
		///<summary>Primary key.</summary>
		public long EtransNum;
		///<summary>The date and time of the transaction.</summary>
		public DateTime DateTimeTrans;
		///<summary>FK to clearinghouse.ClearinghouseNum .  Can be 0 if no clearinghouse was involved.</summary>
		public long ClearinghouseNum;
		///<summary>Enum:EtransType</summary>
		public EtransType Etype;
		///<summary>FK to claim.ClaimNum if a claim. Otherwise 0.  Warning.  Original claim might have been deleted.  But if Canadian claim was successfully sent, then deletion will be blocked.</summary>
		public long ClaimNum;
		///<summary>For Canada.  Unique for every transaction sent.  Increments by one until 999999, then resets to 1.</summary>
		public int OfficeSequenceNumber;
		///<summary>For Canada.  Separate counter for each carrier.  Increments by one until 99999, then resets to 1.</summary>
		public int CarrierTransCounter;
		///<summary>For Canada.  If this claim includes secondary, then this is the counter for the secondary carrier.</summary>
		public int CarrierTransCounter2;
		///<summary>FK to carrier.CarrierNum.</summary>
		public long CarrierNum;
		///<summary>FK to carrier.CarrierNum Only used if secondary insurance info is provided on a claim.  Necessary for Canada.</summary>
		public long CarrierNum2;
		///<summary>This is useful in case the original claim has been deleted.  Now, we can still tell who the patient was.</summary>
		public long PatNum;
		///<summary>Maxes out at 999, then loops back to 1.  This is not a good key, but is a restriction of (canadian?).  So dates must also be used to isolate the correct BatchNumber key.  Specific to one clearinghouse.  Only used with e-claims.  Claim will have BatchNumber, and 997 will have matching BatchNumber. (In X12 lingo, it's a functional group number)</summary>
		public int BatchNumber;
		///<summary>A=Accepted, R=Rejected, blank if not able to parse.  More options will be added later.  The incoming 997 sets this flag automatically.  To find the 997, look for a matching BatchNumber with a similar date, since both the claims and the 997 will both have the same batch number.  The 997 does not have this flag set on itself.</summary>
		public string AckCode;
		///<summary>For sent e-claims, within each batch (functional group), each carrier gets it's own transaction set.  Since 997s acknowledge transaction sets rather than batches, we need to keep track of which transaction set each claim is part of as well as which batch it's part of.  This field can't be set as part of 997, because one 997 refers to multiple trans sets.</summary>
		public int TransSetNum;
		///<summary>Typical uses include indicating that the report was printed, the claim was resent, reason for rejection, etc.  For a 270, this contains the automatically generated short summary of the response.  The response could include the reason for failure, or it could be a short summary of the 271.</summary>
		public string Note;
		///<summary>FK to etransmessagetext.EtransMessageTextNum.  Can be 0 if there is no message text.  Multiple Etrans objects can refer to the same message text, very common in a batch.</summary>
		public long EtransMessageTextNum;
		///<summary>FK to etrans.EtransNum.  Only has a non-zero value if there exists an ack etrans, like a 997 or a 271.  There can be only one ack for any given etrans, but one ack can apply to multiple etran's that were sent as one batch.</summary>
		public long AckEtransNum;
		///<summary>FK to insplan.PlanNum.  Used if EtransType.BenefitInquiry270 and BenefitResponse271.</summary>
		public long PlanNum;


		///<summary></summary>
		public Etrans Copy(){
			return (Etrans)this.MemberwiseClone();
		}

	}

	




}

















