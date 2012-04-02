using System;
using System.Collections;

namespace OpenDentBusiness{
	
	/// <summary>One electronic transaction.  Typically, one claim or response.  Or one benefit request or response.  Is constantly being expanded to include more types of transactions with clearinghouses.  Also stores printing of paper claims.  Sometimes stores a copy of what was sent.</summary>
	[Serializable]
	public class Etrans:TableBase{
		///<summary>Primary key.</summary>
		[CrudColumn(IsPriKey=true)]
		public long EtransNum;
		///<summary>The date and time of the transaction.</summary>
		[CrudColumn(SpecialType=CrudSpecialColType.DateTEntryEditable)]
		public DateTime DateTimeTrans;
		///<summary>FK to clearinghouse.ClearinghouseNum .  Can be 0 if no clearinghouse was involved.</summary>
		public long ClearingHouseNum;
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
		///<summary>FK to patient.PatNum This is useful in case the original claim has been deleted.  Now, we can still tell who the patient was.</summary>
		public long PatNum;
		///<summary>Maxes out at 999, then loops back to 1.  This is not a good key, but is a restriction of (canadian?).  So dates must also be used to isolate the correct BatchNumber key.  Specific to one clearinghouse.  Only used with e-claims.  Claim will have BatchNumber, and 997 will have matching BatchNumber. (In X12 lingo, it's a functional group number)</summary>
		public int BatchNumber;
		///<summary>A=Accepted, R=Rejected, blank if not able to parse.  More options will be added later.  The incoming 997 or 999 sets this flag automatically.  To find the 997 or 999, look for a matching BatchNumber with a similar date, since both the claims and the 997 or 999 will both have the same batch number.  The 997 or 999 does not have this flag set on itself.</summary>
		public string AckCode;
		///<summary>For sent e-claims, within each batch (functional group), each carrier gets it's own transaction set.  Since 997s and 999s acknowledge transaction sets rather than batches, we need to keep track of which transaction set each claim is part of as well as which batch it's part of.  This field can't be set as part of 997 or 999, because one 997 or 999 refers to multiple trans sets.</summary>
		public int TransSetNum;
		///<summary>Typical uses include indicating that the report was printed, the claim was resent, reason for rejection, etc.  For a 270, this contains the automatically generated short summary of the response.  The response could include the reason for failure, or it could be a short summary of the 271.</summary>
		public string Note;
		///<summary>FK to etransmessagetext.EtransMessageTextNum.  Can be 0 if there is no message text.  Multiple Etrans objects can refer to the same message text, very common in a batch.</summary>
		public long EtransMessageTextNum;
		///<summary>FK to etrans.EtransNum.  Only has a non-zero value if there exists an ack etrans, like a 997, 999, 271, or ackError.  There can be only one ack for any given etrans, but one ack can apply to multiple etran's that were sent as one batch.</summary>
		public long AckEtransNum;
		///<summary>FK to insplan.PlanNum.  Used if EtransType.BenefitInquiry270 and BenefitResponse271 and Eligibility_CA.</summary>
		public long PlanNum;
		///<summary>FK to inssub.InsSubNum.  Used if EtransType.BenefitInquiry270 and BenefitResponse271 and Eligibility_CA.</summary>
		public long InsSubNum;

		///<summary></summary>
		public Etrans Copy(){
			return (Etrans)this.MemberwiseClone();
		}

	}

	///<summary>The _CA of some types should get stripped off when displaying to users.</summary>
	public enum EtransType {
		///<summary>0 X12-837.  Should we differenitate between different kinds of 837s and 4010 vs 5010?</summary>
		ClaimSent,
		///<summary>1 claim</summary>
		ClaimPrinted,
		///<summary>2 Canada. Type 01</summary>
		Claim_CA,
		///<summary>3 Renaissance</summary>
		Claim_Ren,
		///<summary>4 Canada. Type 11</summary>
		ClaimAck_CA,
		///<summary>5 Canada. Type 21</summary>
		ClaimEOB_CA,
		///<summary>6 Canada. Type 08</summary>
		Eligibility_CA,
		///<summary>7 Canada. Type 18. V02 type 10.</summary>
		EligResponse_CA,
		///<summary>8 Canada. Type 02</summary>
		ClaimReversal_CA,
		///<summary>9 Canada. Type 03</summary>
		Predeterm_CA,
		///<summary>10 Canada. Type 04</summary>
		RequestOutstand_CA,
		///<summary>11 Canada. Type 05</summary>
		RequestSumm_CA,
		///<summary>12 Canada. Type 06</summary>
		RequestPay_CA,
		///<summary>13 Canada. Type 07</summary>
		ClaimCOB_CA,
		///<summary>14 Canada. Type 12</summary>
		ReverseResponse_CA,
		///<summary>15 Canada. Type 13</summary>
		PredetermAck_CA,
		///<summary>16 Canada. Type 23</summary>
		PredetermEOB_CA,
		///<summary>17 Canada. Type 14</summary>
		OutstandingAck_CA,
		///<summary>18 Canada. Type 24</summary>
		EmailResponse_CA,
		///<summary>19 Canada. Type 16</summary>
		PaymentResponse_CA,
		///<summary>20 Canada. Type 15</summary>
		SummaryResponse_CA,
		///<summary>21 Ack from clearinghouse. X12-997.</summary>
		Acknowledge_997,
		///<summary>22 X12-277. Unsolicited claim status notification.</summary>
		StatusNotify_277,
		///<summary>23 Text report from clearinghouse in human readable format.</summary>
		TextReport,
		///<summary>24 X12-270.</summary>
		BenefitInquiry270,
		///<summary>25 X12-271</summary>
		BenefitResponse271,
		///<summary>26 When a Canadian message is sent, and an error comes back instead of a message.  This stores information about the error.  The etrans with this type is attached it to the original etrans as an ack.</summary>
		AckError,
		///<summary>27 X12-835. Not used yet.</summary>
		ERA_835,
		///<summary>28 Ack from clearinghouse. X12-999.</summary>
		Acknowledge_999
	}




}

















