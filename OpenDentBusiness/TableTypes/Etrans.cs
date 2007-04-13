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
		///<summary>Whether outgoing or incoming, this field contains the actual text of the message.  We don't yet know how we will handle multiple transactions in a single batch message if that becomes necessary.</summary>
		public string MessageText;


		///<summary></summary>
		public Etrans Copy(){
			Etrans e=new Etrans();
			e.EtransNum=EtransNum;
			e.DateTimeTrans=DateTimeTrans;
			e.ClearinghouseNum=ClearinghouseNum;
			e.Etype=Etype;
			e.ClaimNum=ClaimNum;
			e.OfficeSequenceNumber=OfficeSequenceNumber;
			e.CarrierTransCounter=CarrierTransCounter;
			e.CarrierTransCounter2=CarrierTransCounter2;
			e.CarrierNum=CarrierNum;
			e.CarrierNum2=CarrierNum2;
			e.PatNum=PatNum;
			e.MessageText=MessageText;
			return e;
		}

	}

	




}

















