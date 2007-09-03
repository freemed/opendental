using System;
using System.Collections;

namespace OpenDentBusiness{
	
	/// <summary>One electronic transaction.  Typically, one claim or response.  Will later be expanded to include many other types of transactions with clearinghouses, such as eligibility, reports, etc.  Also stores printing of paper claims.  Sometimes stores a copy of what was sent.</summary>
	public class Etrans{
		///<summary>Primary key.</summary>
		public int EtransNum;
		///<summary>The date and time of the transaction.</summary>
		public DateTime DateTimeTrans;
		///<summary>FK to clearinghouse.ClearinghouseNum .  Can be 0 if no clearinghouse was involved.</summary>
		public int ClearinghouseNum;
		///<summary></summary>
		public EtransType Etype;
		///<summary>FK to claim.ClaimNum if a claim. Otherwise 0.  Warning.  Original claim might have been deleted.  But if Canadian claim was successfully sent, then deletion will be blocked.</summary>
		public int ClaimNum;
		///<summary>For Canada.  Unique for every transaction sent.  Increments by one until 999999, then resets to 1.</summary>
		public int OfficeSequenceNumber;
		///<summary>For Canada.  Separate counter for each carrier.  Increments by one until 99999, then resets to 1.</summary>
		public int CarrierTransCounter;
		///<summary>For Canada.  If this claim includes secondary, then this is the counter for the secondary carrier.</summary>
		public int CarrierTransCounter2;
		///<summary>FK to carrier.CarrierNum.</summary>
		public int CarrierNum;
		///<summary>FK to carrier.CarrierNum Only used if secondary insurance info is provided on a claim.  Necessary for Canada.</summary>
		public int CarrierNum2;
		///<summary>This is useful in case the original claim has been deleted.  Now, we can still tell who the patient was.</summary>
		public int PatNum;
		///<summary>Whether outgoing or incoming, this field contains the actual text of the message.  When there is a batch, this field will contain the text of the entire batch.  Other claims will be mixed in.  The same text will be duplicated in the MessageText fields on the other claims.</summary>
		public string MessageText;
		///<summary>Maxes out at 999, then loops back to 1.  This is not a good key, but is a restriction of (canadian?).  So dates must also be used to isolate the correct BatchNumber key.  Specific to one clearinghouse.  Only used with e-claims.  Claim will have BatchNumber, and 997 will have matching BatchNumber. (In X12 lingo, it's a functional group number)</summary>
		public int BatchNumber;
		///<summary>A=Accepted, R=Rejected, blank if not able to parse.  More options will be added later.  The incoming 997 sets this flag automatically.  To find the 997, look for a matching BatchNumber with a similar date, since both the claims and the 997 will both have the same batch number.  The 997 does not have this flag set on itself.</summary>
		public string AckCode;
		///<summary>For sent e-claims, within each batch (functional group), each carrier gets it's own transaction set.  Since 997s acknowledge transaction sets rather than batches, we need to keep track of which transaction set each claim is part of as well as which batch it's part of.  This field can't be set as part of 997, because one 997 refers to multiple trans sets.</summary>
		public int TransSetNum;
		///<summary>Typical uses include indicating that report was printed, claim was resent, reason for rejection, etc.</summary>
		public string Note;


		///<summary></summary>
		public Etrans Copy(){
			return (Etrans)this.MemberwiseClone();
		}

	}

	




}

















