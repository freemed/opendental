using System;
using System.Collections;
using System.Text.RegularExpressions;

namespace OpenDentBusiness{

	///<summary>Every InsPlan has a Carrier.  The carrier stores the name and address.</summary>
	[Serializable()]
	public class Carrier:TableBase{
		///<summary>Primary key.</summary>
		[CrudColumn(IsPriKey=true)]
		public long CarrierNum;
		///<summary>Name of the carrier.</summary>
		public string CarrierName;
		///<summary>.</summary>
		public string Address;
		///<summary>Second line of address.</summary>
		public string Address2;
		///<summary>.</summary>
		public string City;
		///<summary>2 char in the US.</summary>
		public string State;
		///<summary>Postal code.</summary>
		public string Zip;
		///<summary>Includes any punctuation.</summary>
		public string Phone;
		///<summary>E-claims electronic payer id.  5 char in USA.  6 digits in Canada.  I've seen an ID this long before: "LA-DHH-MEDICAID".  The user interface currently limits length to 20, although db limits length to 255.  X12 requires length between 2 and 80.</summary>
		public string ElectID;
		///<summary>Do not send electronically.  It's just a default; you can still send electronically.</summary>
		public bool NoSendElect;
		///<summary>Canada: True if a CDAnet carrier.  This has significant implications:  1. It can be filtered for in the list of carriers.  2. An ElectID is required.  3. The ElectID can never be used by another carrier.  4. If the carrier is attached to any etrans, then the ElectID cannot be changed (and, of course, the carrier cannot be deleted or combined).</summary>
		public bool IsCDA;
		///<summary>The version of CDAnet supported.  Either 02 or 04.</summary>
		public string CDAnetVersion;
		///<summary>FK to canadiannetwork.CanadianNetworkNum.  Only used in Canada.  Right now, there is no UI to the canadiannetwork table in our db.</summary>
		public long CanadianNetworkNum;
		///<summary></summary>
		public bool IsHidden;
		///<summary>1=No Encryption, 2=CDAnet standard #1, 3=CDAnet standard #2.  Field A10.</summary>
		public byte CanadianEncryptionMethod;
		///<summary>A01.  Up to 12 char.</summary>
		public string CanadianTransactionPrefix;
		///<summary>Bit flags.</summary>
		public CanSupTransTypes CanadianSupportedTypes; 


		public Carrier Copy(){
			return (Carrier)this.MemberwiseClone();
		}

		public Carrier() {

		}


	}

	

	///<summary>Type 23, Predetermination EOB (regular and embedded) are not included because they are not part of the testing scripts.  The three required types are not included: ClaimTransaction_01, ClaimAcknowledgement_11, and ClaimEOB_21.  Can't find specs for PredeterminationEobEmbedded.</summary>
	[Flags]
	public enum CanSupTransTypes {
		None=0,
		EligibilityTransaction_08=1,
		EligibilityResponse_18=2,
		CobClaimTransaction_07=4,
		///<summary>ClaimAck_11 is not here because it's required by all carriers.</summary>
		ClaimAckEmbedded_11e=8,
		///<summary>ClaimEob_21 is not here because it's required by all carriers.</summary>
		ClaimEobEmbedded_21e=16,
		ClaimReversal_02=32,
		ClaimReversalResponse_12=64,
		PredeterminationSinglePage_03=128,
		PredeterminationMultiPage_03=256,
		PredeterminationAck_13=512,
		PredeterminationAckEmbedded_13e=1024,
		RequestForOutstandingTrans_04=2048,
		OutstandingTransAck_14=4096,
		///<summary>Response</summary>
		EmailTransaction_24=8192,
		RequestForSummaryReconciliation_05=16384,
		SummaryReconciliation_15=32768,
		RequestForPaymentReconciliation_06=65536,
		PaymentReconciliation_16=131072
	}
	
	

}













