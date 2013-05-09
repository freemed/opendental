using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace OpenDentBusiness
{
	///<summary>X12 835 Health Care Claim Payment/Advice. This transaction type is a response to an 837 claim submission. The 835 will always come after a 277 is received and a 277 will always come after a 999. Neither the 277 nor the 999 are required, so it is possible that an 835 will be received directly after the 837. The 835 is not required either, so it is possible that none of the 997, 999, 277 or 835 reports will be returned from the carrier.</summary>
	public class X835:X12object{

		///<summary>All segments within the transaction.</summary>
    private List<X12Segment> segments;
		///<summary>BPR segment (pg. 69). Financial Information segment. Required.</summary>
		private X12Segment segBPR;
		///<summary>TRN segment (pg. 77). Reassociation Trace Number segment. Required.</summary>
		private X12Segment segTRN;
		///<summary>N1*PR segment of loop 1000A (pg. 87). Payer Identification segment. Required.</summary>
		private X12Segment segN1_PR;
		///<summary>N3 segment of loop 1000A (pg. 89). Payer Address. Required.</summary>
		private X12Segment segN3_PR;
		///<summary>N4 segment of loop 1000A (pg. 90). Payer City, Sate, Zip code. Required.</summary>
		private X12Segment segN4_PR;
		///<summary>PER*BL segment of loop 1000A (pg. 97). Payer technical contact information. Required. Can repeat more than once, but we only are about the first occurrence.</summary>
		private X12Segment segPER_BL;
		///<summary>N1*PE segment of loop 1000B (pg. 102). Payee identification. Required. We include this information because it could be helpful for those customers who are using clinics.</summary>
		private X12Segment segN1_PE;
		///<summary>CLP of loop 2100 (pg. 123). Claim payment information.</summary>
		private List<int> segNumsCLP;
		///<summary>SVC of loop 2110 (pg. 186). Service (procedure) payment information. One sub-list for each claim (CLP segment).</summary>
		private List<List<int>> segNumsSVC;
		///<summary>PLB segments (pg.217). Provider Adjustment. Situational. This is the footer and table 3 if pesent.</summary>
		private List<int> segNumsPLB;

    public static bool Is835(X12object xobj) {
      if(xobj.FunctGroups.Count!=1) {//Exactly 1 GS segment in each 835.
        return false;
      }
      if(xobj.FunctGroups[0].Header.Get(1)=="HP") {//GS01 (pg. 279)
        return true;
      }
      return false;
    }

    public X835(string messageText):base(messageText) {
      segments=FunctGroups[0].Transactions[0].Segments;//The GS segment contains exactly one ST segment below it.
			segBPR=segments[0];//Always present, because required.
			segTRN=segments[1];//Always present, because required.
			segNumsCLP=new List<int>();
			segNumsSVC=new List<List<int>>();
			List<int> segNumsSVC_cur=new List<int>();
			for(int i=0;i<segments.Count;i++) {
				X12Segment seg=segments[i];
				if(seg.SegmentID=="N1" && seg.Get(1)=="PR") {
					segN1_PR=seg;
					segN3_PR=segments[i+1];
					segN4_PR=segments[i+2];
				}
				else if(seg.SegmentID=="PER" && seg.Get(1)=="BL") {
					if(segPER_BL==null) {//This segment can repeat. We only care about the first occurrence.
						segPER_BL=seg;
					}
				}
				else if(seg.SegmentID=="N1" && seg.Get(1)=="PE") {
					segN1_PE=seg;
				}
				else if(seg.SegmentID=="CLP") { //The CLP segment only exists is within the 2100 loop.
					segNumsCLP.Add(i);
					segNumsSVC.Add(segNumsSVC_cur);
					segNumsSVC_cur=new List<int>();//Start a new list of procedures.
				}
				else if(seg.SegmentID=="SVC") { //The SVC segment only exists is within the 2100 loop.
					segNumsSVC_cur.Add(i);
				}
				else if(seg.SegmentID=="PLB") {
					segNumsPLB.Add(i);
				}
			}
			segNumsSVC.Add(segNumsSVC_cur);
    }

		///<summary>Gets the description for the transaction handling code in Table 1 (Header) BPR01. Required.</summary>
		public string GetTransactionHandlingCodeDescription() {
			string transactionHandlingCode=segBPR.Get(1);
			if(transactionHandlingCode=="C") {
				return "Payment Accompanies Remittance Advice";
			}
			else if(transactionHandlingCode=="D") {
				return "Make Payment Only";
			}
			else if(transactionHandlingCode=="H") {
				return "Notification Only";
			}
			else if(transactionHandlingCode=="I") {
				return "Remittance Information Only";
			}
			else if(transactionHandlingCode=="P") {
				return "Prenotification of Future Transfers";
			}
			else if(transactionHandlingCode=="U") {
				return "Split Payment and Remittance";
			}
			else if(transactionHandlingCode=="X") {
				return "Handling Party's Option to Split Payment and Remittance";
			}
			return "UNKNOWN";
		}

		///<summary>Gets the payment credit or debit amount in Table 1 (Header) BPR02. Required.</summary>
		public string GetPaymentAmount() {
			return segBPR.Get(2);
		}

		///<summary>Gets the payment credit or debit flag in Table 1 (Header) BPR03. Returns "Credit" or "Debit". Required.</summary>
		public string GetCreditDebit() {
			string creditDebitFlag=segBPR.Get(3);
			if(creditDebitFlag=="C") {
				return "Credit";
			}
			else if(creditDebitFlag=="D") {
				return "Debit";
			}
			return "";
		}

		///<summary>Gets the description for the payment method in Table 1 (Header) BPR04. Required.</summary>
		public string GetPaymentMethodDescription() {
			string paymentMethodCode=segBPR.Get(4);
			if(paymentMethodCode=="ACH") {
				return "Automated Clearing House (ACH)";
			}
			else if(paymentMethodCode=="BOP") {
				return "Financial Institution Option";
			}
			else if(paymentMethodCode=="CHK") {
				return "Check";
			}
			else if(paymentMethodCode=="FWT") {
				return "Federal Reserve Funds/Wire Transfer - Nonrepetitive";
			}
			else if(paymentMethodCode=="NON") {
				return "Non-payment Data";
			}
			return "UNKNOWN";
		}

		///<summary>Gets the last 4 digits of the account number for the receiving company (the provider office) in Table 1 (Header) BPR15. Situational. If not present, then returns empty string.</summary>
		public string GetAccountNumReceivingShort() {
			string accountNumber=segBPR.Get(15);
			if(accountNumber.Length<=4) {
				return accountNumber;
			}
			return accountNumber.Substring(accountNumber.Length-4);
		}

		///<summary>Gets the effective payment date in Table 1 (Header) BPR16. Required.</summary>
		public DateTime GetDateEffective() {
			string dateEffectiveStr=segBPR.Get(16);//BPR16 will be blank if the payment is a check.
			if(dateEffectiveStr.Length<8) {
				return DateTime.MinValue;
			}
			int dateEffectiveYear=int.Parse(dateEffectiveStr.Substring(0,4));
			int dateEffectiveMonth=int.Parse(dateEffectiveStr.Substring(4,2));
			int dateEffectiveDay=int.Parse(dateEffectiveStr.Substring(6,2));
			return new DateTime(dateEffectiveYear,dateEffectiveMonth,dateEffectiveDay);
		}

		///<summary>Gets the check number or transaction reference number in Table 1 (Header) TRN02. Required.</summary>
		public string GetTransactionReferenceNumber() {
			return segTRN.Get(2);
		}

		///<summary>Gets the payer name in Table 1 (Header) N102. Required.</summary>
		public string GetPayerName() {
			return segN1_PR.Get(2);
		}

		///<summary>Gets the payer electronic ID in Table 1 (Header) N104 of segment N1*PR. Situational. If not present, then returns empty string.</summary>
		public string GetPayerID() {
			if(segN1_PR.Elements.Length>=4) {
				return segN1_PR.Get(4);
			}
			return "";
		}

		///<summary>Gets the payer address line 1 in Table 1 (Header) N301 of segment N1*PR. Required.</summary>
		public string GetPayerAddress1() {
			return segN3_PR.Get(1);
		}

		///<summary>Gets the payer city name in Table 1 (Header) N401 of segment N1*PR. Required.</summary>
		public string GetPayerCityName() {
			return segN4_PR.Get(1);
		}

		///<summary>Gets the payer state in Table 1 (Header) N402 of segment N1*PR. Required when in USA or Canada.</summary>
		public string GetPayerState() {
			return segN4_PR.Get(2);
		}

		///<summary>Gets the payer zip code in Table 1 (Header) N403 of segment N1*PR. Required when in USA or Canada.</summary>
		public string GetPayerZip() {
			return segN4_PR.Get(3);
		}

		///<summary>Gets the contact information from segment PER*BL. Phone/email in PER04 or the contact phone/email in PER06 or both. If neither PER04 nor PER06 are present, then returns empty string.</summary>
		public string GetPayerContactInfo() {
			string contact_info="";
			if(segPER_BL.Elements.Length>=4 && segPER_BL.Get(4)!="") {//Contact number 1.
				contact_info=segPER_BL.Get(4);
			}
			if(segPER_BL.Elements.Length>=6 && segPER_BL.Get(6)!="") {//Contact number 2.
				if(contact_info!="") {
					contact_info+=" or ";
				}
				contact_info+=segPER_BL.Get(6);
			}
			if(segPER_BL.Elements.Length>=8 && segPER_BL.Get(8)!="") {//Telephone extension for contact number 2.
				if(contact_info!="") {
					contact_info+=" x";
				}
				contact_info+=segPER_BL.Get(8);
			}
			return contact_info;
		}

		///<summary>Gets the payee name in Table 1 (Header) N102 of segment N1*PE. Required.</summary>
		public string GetPayeeName() {
			return segN1_PE.Get(2);
		}

		///<summary>Gets a human readable description for the identifiation code qualifier found in N103 of segment N1*PE. Required.</summary>
		public string GetPayeeIdType() {
			string qualifier=segN1_PE.Get(3);
			if(qualifier=="FI") {
				return "TIN";
			}
			else if(qualifier=="XV") {
				return "Medicaid ID";
			}
			else if(qualifier=="XX") {
				return "NPI";
			}
			return "";
		}

		///<summary>Gets the payee identification number found in N104 of segment N1*PE. Required. Usually the NPI number.</summary>
		public string GetPayeeId() {
			return segN1_PE.Get(4);
		}

		///<summary>CLP01 in loop 2100. Referred to in this format as a Patient Control Number. Do this first to get a list of all claim tracking numbers that are contained within this 835.  Then, for each claim tracking number, we can later retrieve specific information for that single claim. The claim tracking numbers correspond to CLM01 exactly as submitted in the 837. We refer to CLM01 as the claim identifier on our end. We allow more than just digits in our claim identifiers, so we must return a list of strings.</summary>
		public List<string> GetClaimTrackingNumbers() {
			List<string> retVal=new List<string>();
			for(int i=0;i<segNumsCLP.Count;i++) {
				X12Segment seg=segments[segNumsCLP[i]];//CLP segment.
				retVal.Add(seg.Get(1));//CLP01
			}
			return retVal;
		}

		///<summary>Result will contain strings in the following order: Claim Status Code Description (CLP02), Total Claim Charge Amount (CLP03), Claim Payment Amount (CLP04), Patient Responsibility Amount (CLP05), Payer Claim Control Number (CLP07).</summary>
    public string[] GetClaimInfo(string trackingNumber) {
      string[] result=new string[5];
      for(int i=0;i<result.Length;i++) {
        result[i]="";
      }
      for(int i=0;i<segNumsCLP.Count;i++) {
        int segNum=segNumsCLP[i];
				X12Segment segCLP=segments[segNum];
				if(segCLP.Get(1)!=trackingNumber) {//CLP01 Patient Control Number
					continue;
				}
				result[0]=GetClaimStatusDescriptionForCode(segCLP.Get(2));//CLP02 Claim Status Code Description
				result[1]=segCLP.Get(3);//CLP03 Total Claim Charge Amount
				result[2]=segCLP.Get(4);//CLP04 Claim Payment Amount
				result[3]=segCLP.Get(5);//CLP05 Patient Responsibility Amount
				result[4]=segCLP.Get(7);//CLP07 Payer Claim Control Number
				break;
      }
      return result;
    }

		private string GetClaimStatusDescriptionForCode(string claimStatusCode) {
			string claimStatusCodeDescript="";
			if(claimStatusCode=="1") {
				claimStatusCodeDescript="Processed as Primary";
			}
			else if(claimStatusCode=="2") {
				claimStatusCodeDescript="Processed as Secondary";
			}
			else if(claimStatusCode=="3") {
				claimStatusCodeDescript="Processed as Tertiary";
			}
			else if(claimStatusCode=="4") {
				claimStatusCodeDescript="Denied";
			}
			else if(claimStatusCode=="19") {
				claimStatusCodeDescript="Processed as Primary, Forwarded to Additional Payer(s)";
			}
			else if(claimStatusCode=="20") {
				claimStatusCodeDescript="Processed as Secondary, Forwarded to Additional Payer(s)";
			}
			else if(claimStatusCode=="21") {
				claimStatusCodeDescript="Processed as Tertiary, Forwarded to Additional Payer(s)";
			}
			else if(claimStatusCode=="22") {
				claimStatusCodeDescript="Reversal of Previous Payment";
			}
			else if(claimStatusCode=="23") {
				claimStatusCodeDescript="Not Our Claim, Forwarded to Additional Payer(s)";
			}
			else if(claimStatusCode=="25") {
				claimStatusCodeDescript="Predetermination Pricing Only - No Payment";
			}
			return claimStatusCodeDescript;
		}

		///<summary>Each item returned contains a string[] with values:
		///00 Provider NPI (PLB01), 
		///01 Fiscal Period Date (PLB02), 
		///02 ReasonCodeDescription, 
		///03 ReasonCode (PLB03-1 or PLB05-1 or PLB07-1 or PLB09-1 or PLB11-1 or PLB13-1 2/2), 
		///04 ReferenceIdentification (PLB03-2 or PLB05-2 or PLB07-2 or PLB09-2 or PLB11-2 or PLB13-2 1/50),
		///05 Amount (PLB04 1/18).</summary>
		public List<string[]> GetProviderLevelAdjustments() {
			List<string[]> result=new List<string[]>();
			for(int i=0;i<segNumsPLB.Count;i++) {
				X12Segment segPLB=segments[segNumsPLB[i]];
				string provNPI=segPLB.Get(1);//PLB01 is required.
				string dateFiscalPeriodStr=segPLB.Get(2);//PLB02 is required.
				DateTime dateFiscalPeriod=DateTime.MinValue;
				try {
					int dateEffectiveYear=int.Parse(dateFiscalPeriodStr.Substring(0,4));
					int dateEffectiveMonth=int.Parse(dateFiscalPeriodStr.Substring(4,2));
					int dateEffectiveDay=int.Parse(dateFiscalPeriodStr.Substring(6,2));
					dateFiscalPeriod=new DateTime(dateEffectiveYear,dateEffectiveMonth,dateEffectiveDay);
				}
				catch {
					//Oh well, not very important infomration anyway.
				}
				int segNumAdjCode=3;//PLB03 is required.
				while(segNumAdjCode<segPLB.Elements.Length) {
					string reasonCode=segPLB.Get(segNumAdjCode,1);
					//For each adjustment reason code, the reference identification is optional.
					string referenceIdentification="";
					if(segPLB.Get(3).Length>reasonCode.Length) {
						referenceIdentification=segPLB.Get(3,2);
					}
					//For each adjustment reason code, an amount is required.
					string amount=segPLB.Get(segNumAdjCode+1);
					result.Add(new string[] { provNPI,dateFiscalPeriod.ToShortDateString(),GetDescriptForProvAdjCode(reasonCode),reasonCode,referenceIdentification,amount });
				}
			}
			return result;
		}

		///<summary>Used for the reason codes in the PLB segment.</summary>
		private string GetDescriptForProvAdjCode(string reasonCode) {
			if(reasonCode=="50") {
				return "Late Charge";
			}
			if(reasonCode=="51") {
				return "Interest Penalty Charge";
			}
			if(reasonCode=="72") {
				return "Authorized Return";
			}
			if(reasonCode=="90") {
				return "Early Payment Allowance";
			}
			if(reasonCode=="AH") {
				return "Origination Fee";
			}
			if(reasonCode=="AM") {
				return "Applied to Borrower's Account";
			}
			if(reasonCode=="AP") {
				return "Acceleration of Benefits";
			}
			if(reasonCode=="B2") {
				return "Rebate";
			}
			if(reasonCode=="B3") {
				return "Recovery Allowance";
			}
			if(reasonCode=="BD") {
				return "Bad Debt Adjustment";
			}
			if(reasonCode=="BN") {
				return "Bonus";
			}
			if(reasonCode=="C5") {
				return "Temporary Allowance";
			}
			if(reasonCode=="CR") {
				return "Capitation Interest";
			}
			if(reasonCode=="CS") {
				return "Adjustment";
			}
			if(reasonCode=="CT") {
				return "Capitation Payment";
			}
			if(reasonCode=="CV") {
				return "Capital Passthru";
			}
			if(reasonCode=="CW") {
				return "Certified Registered Nurse Anesthetist Passthru";
			}
			if(reasonCode=="DM") {
				return "Direct Medical Education Passthru";
			}
			if(reasonCode=="E3") {
				return "Withholding";
			}
			if(reasonCode=="FB") {
				return "Forwarding Balance";
			}
			if(reasonCode=="FC") {
				return "Fund Allocation";
			}
			if(reasonCode=="GO") {
				return "Graduate Medical Education Passthru";
			}
			if(reasonCode=="HM") {
				return "Hemophilia Clotting Factor Supplement";
			}
			if(reasonCode=="IP") {
				return "Incentive Premium Payment";
			}
			if(reasonCode=="IR") {
				return "Internal Revenue Service Withholding";
			}
			if(reasonCode=="IS") {
				return "Interim Settlement";
			}
			if(reasonCode=="J1") {
				return "Nonreimbursable";
			}
			if(reasonCode=="L3") {
				return "Penalty";
			}
			if(reasonCode=="L6") {
				return "Interest Owed";
			}
			if(reasonCode=="LE") {
				return "Levy";
			}
			if(reasonCode=="LS") {
				return "Lump Sum";
			}
			if(reasonCode=="OA") {
				return "Organ Acquisition Passthru";
			}
			if(reasonCode=="OB") {
				return "Offset for Affiliated Providers";
			}
			if(reasonCode=="PI") {
				return "Periodic Interim Payment";
			}
			if(reasonCode=="PL") {
				return "Payment Final";
			}
			if(reasonCode=="RA") {
				return "Retro-activity Adjustment";
			}
			if(reasonCode=="RE") {
				return "Return on Equity";
			}
			if(reasonCode=="SL") {
				return "Student Loan Repayment";
			}
			if(reasonCode=="TL") {
				return "Third Party Liability";
			}
			if(reasonCode=="WO") {
				return "Overpayment Recovery";
			}
			if(reasonCode=="WU") {
				return "Unspecified Recovery";
			}
			return "Reason "+reasonCode;
		}

		///<summary>Code Source 139. The complete list can be found at: http://www.wpc-edi.com/reference/codelists/healthcare/claim-adjustment-reason-codes/ .
		///Used for claim and procedure reason codes.</summary>
		private string GetDescriptForReasonCode(string reasonCode) {
			if(reasonCode=="1") {
				return "Deductible Amount";
			}
			if(reasonCode=="2") {
				return "Coinsurance Amount";
			}
			if(reasonCode=="3") {
				return "Co-payment Amount";
			}
			if(reasonCode=="4") {
				return "The procedure code is inconsistent with the modifier used or a required modifier is missing.";
			}
			if(reasonCode=="5") {
				return "The procedure code/bill type is inconsistent with the place of service.";
			}
			if(reasonCode=="6") {
				return "	The procedure/revenue code is inconsistent with the patient's age.";
			}
			if(reasonCode=="7") {
				return "The procedure/revenue code is inconsistent with the patient's gender.";
			}
			if(reasonCode=="8") {
				return "The procedure code is inconsistent with the provider type/specialty (taxonomy).";
			}
			if(reasonCode=="9") {
				return "The diagnosis is inconsistent with the patient's age.";
			}
			if(reasonCode=="10") {
				return "The diagnosis is inconsistent with the patient's gender.";
			}
			if(reasonCode=="11") {
				return "The diagnosis is inconsistent with the procedure.";
			}
			if(reasonCode=="12") {
				return "The diagnosis is inconsistent with the provider type.";
			}
			if(reasonCode=="13") {
				return "The date of death precedes the date of service.";
			}
			if(reasonCode=="14") {
				return "The date of birth follows the date of service.";
			}
			if(reasonCode=="15") {
				return "The authorization number is missing, invalid, or does not apply to the billed services or provider.";
			}
			if(reasonCode=="16") {
				return "Claim/service lacks information which is needed for adjudication.";
			}
			if(reasonCode=="18") {
				return "Exact duplicate claim/service";
			}
			if(reasonCode=="19") {
				return "This is a work-related injury/illness and thus the liability of the Worker's Compensation Carrier.";
			}
			if(reasonCode=="20") {
				return "This injury/illness is covered by the liability carrier.";
			}
			if(reasonCode=="21") {
				return "This injury/illness is the liability of the no-fault carrier.";
			}
			if(reasonCode=="22") {
				return "This care may be covered by another payer per coordination of benefits.";
			}
			if(reasonCode=="23") {
				return "The impact of prior payer(s) adjudication including payments and/or adjustments.";
			}
			if(reasonCode=="24") {
				return "Charges are covered under a capitation agreement/managed care plan.";
			}
			if(reasonCode=="26") {
				return "Expenses incurred prior to coverage.";
			}
			if(reasonCode=="27") {
				return "Expenses incurred after coverage terminated.";
			}
			if(reasonCode=="29") {
				return "The time limit for filing has expired.";
			}	
			if(reasonCode=="Patient cannot be identified as our insured.") {
				return "";
			}	
			if(reasonCode=="32") {
				return "Our records indicate that this dependent is not an eligible dependent as defined.";
			}	
			if(reasonCode=="33") {
				return "Insured has no dependent coverage.";
			}
			if(reasonCode=="34") {
				return "Insured has no coverage for newborns.";
			}
			if(reasonCode=="35") {
				return "Lifetime benefit maximum has been reached.";
			}
			if(reasonCode=="39") {
				return "Services denied at the time authorization/pre-certification was requested.";
			}
			if(reasonCode=="40") {
				return "Charges do not meet qualifications for emergent/urgent care. Note: Refer to the 835 Healthcare Policy Identification Segment (loop 2110 Service Payment Information REF), if present.";
			}
			if(reasonCode=="44") {
				return "Prompt-pay discount.";
			}
			if(reasonCode=="45") {
				return "Charge exceeds fee schedule/maximum allowable or contracted/legislated fee arrangement.";
			}
			if(reasonCode=="49") {
				return "These are non-covered services because this is a routine exam or screening procedure done in conjunction with a routine exam.";
			}
			if(reasonCode=="50") {
				return "These are non-covered services because this is not deemed a 'medical necessity' by the payer.";
			}
			if(reasonCode=="51") {
				return "These are non-covered services because this is a pre-existing condition.";
			}
			if(reasonCode=="53") {
				return "Services by an immediate relative or a member of the same household are not covered.";
			}
			if(reasonCode=="54") {
				return "Multiple physicians/assistants are not covered in this case.";
			}
			if(reasonCode=="55") {
				return "Procedure/treatment is deemed experimental/investigational by the payer.";
			}
			if(reasonCode=="56") {
				return "Procedure/treatment has not been deemed 'proven to be effective' by the payer.";
			}
			if(reasonCode=="58") {
				return "Treatment was deemed by the payer to have been rendered in an inappropriate or invalid place of service.";
			}
			if(reasonCode=="59") {
				return "Processed based on multiple or concurrent procedure rules.";
			}
			if(reasonCode=="60") {
				return "Charges for outpatient services are not covered when performed within a period of time prior to or after inpatient services.";
			}
			if(reasonCode=="61") {
				return "Penalty for failure to obtain second surgical opinion.";
			}
			if(reasonCode=="66") {
				return "Blood Deductible.";
			}
			if(reasonCode=="69") {
				return "Day outlier amount.";
			}
			if(reasonCode=="70") {
				return "Cost outlier - Adjustment to compensate for additional costs.";
			}
			if(reasonCode=="74") {
				return "Indirect Medical Education Adjustment.";
			}
			if(reasonCode=="75") {
				return "Direct Medical Education Adjustment.";
			}
			if(reasonCode=="76") {
				return "Disproportionate Share Adjustment.";
			}
			if(reasonCode=="78") {
				return "Non-Covered days/Room charge adjustment.";
			}
			if(reasonCode=="85") {//Use only Group Code PR
				return "Patient Interest Adjustment";
			}
			if(reasonCode=="89") {
				return "Professional fees removed from charges.";
			}
			if(reasonCode=="90") {
				return "Ingredient cost adjustment.";
			}
			if(reasonCode=="91") {
				return "Dispensing fee adjustment.";
			}
			if(reasonCode=="94") {
				return "Processed in Excess of charges.";
			}
			if(reasonCode=="95") {
				return "Plan procedures not followed.";
			}
			if(reasonCode=="96") {
				return "Non-covered charge(s).";
			}
			if(reasonCode=="97") {
				return "The benefit for this service is included in the payment/allowance for another service/procedure that has already been adjudicated.";
			}
			if(reasonCode=="100") {
				return "Payment made to patient/insured/responsible party/employer.";
			}
			if(reasonCode=="101") {
				return "Predetermination: anticipated payment upon completion of services or claim adjudication.";
			}
			if(reasonCode=="102") {
				return "Major Medical Adjustment.";
			}
			if(reasonCode=="103") {
				return "Provider promotional discount";
			}
			if(reasonCode=="104") {
				return "Managed care withholding.";
			}
			if(reasonCode=="105") {
				return "Tax withholding.";
			}
			if(reasonCode=="106") {
				return "Patient payment option/election not in effect.";
			}
			if(reasonCode=="107") {
				return "The related or qualifying claim/service was not identified on this claim.";
			}
			if(reasonCode=="108") {
				return "Rent/purchase guidelines were not met.";
			}
			if(reasonCode=="109") {
				return "Claim/service not covered by this payer/contractor.";
			}
			if(reasonCode=="110") {
				return "Billing date predates service date.";
			}
			if(reasonCode=="111") {
				return "Not covered unless the provider accepts assignment.";
			}
			if(reasonCode=="112") {
				return "Service not furnished directly to the patient and/or not documented.";
			}
			if(reasonCode=="114") {
				return "Procedure/product not approved by the Food and Drug Administration.";
			}
			if(reasonCode=="115") {
				return "Procedure postponed, canceled, or delayed.";
			}
			if(reasonCode=="116") {
				return "The advance indemnification notice signed by the patient did not comply with requirements.";
			}
			if(reasonCode=="117") {
				return "Transportation is only covered to the closest facility that can provide the necessary care.";
			}
			if(reasonCode=="118") {
				return "ESRD network support adjustment.";
			}
			if(reasonCode=="119") {
				return "Benefit maximum for this time period or occurrence has been reached.";
			}
			if(reasonCode=="121") {
				return "Indemnification adjustment - compensation for outstanding member responsibility.";
			}
			if(reasonCode=="122") {
				return "Psychiatric reduction.";
			}
			if(reasonCode=="125") {
				return "Submission/billing error(s).";
			}
			if(reasonCode=="128") {
				return "Newborn's services are covered in the mother's Allowance.";
			}
			if(reasonCode=="129") {
				return "Prior processing information appears incorrect.";
			}
			if(reasonCode=="130") {
				return "Claim submission fee.";
			}
			if(reasonCode=="131") {
				return "Claim specific negotiated discount.";
			}
			if(reasonCode=="132") {
				return "Prearranged demonstration project adjustment.";
			}
			if(reasonCode=="133") { //Use only with Group Code OA
				return "The disposition of the claim/service is pending further review.";
			}
			if(reasonCode=="134") {
				return "Technical fees removed from charges.";
			}
			if(reasonCode=="135") {
				return "Interim bills cannot be processed.";
			}
			if(reasonCode=="136") { //Use Group Code OA
				return "Failure to follow prior payer's coverage rules.";
			}
			if(reasonCode=="137") {
				return "Regulatory Surcharges, Assessments, Allowances or Health Related Taxes.";
			}
			if(reasonCode=="138") {
				return "Appeal procedures not followed or time limits not met.";
			}
			if(reasonCode=="139") {
				return "Contracted funding agreement - Subscriber is employed by the provider of services.";
			}
			if(reasonCode=="140") {
				return "Patient/Insured health identification number and name do not match.";
			}
			if(reasonCode=="142") {
				return "Monthly Medicaid patient liability amount.";
			}
			if(reasonCode=="143") {
				return "Portion of payment deferred.";
			}
			if(reasonCode=="144") {
				return "Incentive adjustment, e.g. preferred product/service.";
			}
			if(reasonCode=="146") {
				return "Diagnosis was invalid for the date(s) of service reported.";
			}
			if(reasonCode=="147") {
				return "Provider contracted/negotiated rate expired or not on file.";
			}
			if(reasonCode=="148") {
				return "Information from another provider was not provided or was insufficient/incomplete.";
			}
			if(reasonCode=="149") {
				return "Lifetime benefit maximum has been reached for this service/benefit category.";
			}
			if(reasonCode=="150") {
				return "Payer deems the information submitted does not support this level of service.";
			}
			if(reasonCode=="151") {
				return "Payment adjusted because the payer deems the information submitted does not support this many/frequency of services.";
			}
			if(reasonCode=="152") {
				return "Payer deems the information submitted does not support this length of service.";
			}
			if(reasonCode=="153") {
				return "Payer deems the information submitted does not support this dosage.";
			}
			if(reasonCode=="154") {
				return "Payer deems the information submitted does not support this day's supply.";
			}
			if(reasonCode=="155") {
				return "Patient refused the service/procedure.";
			}
			if(reasonCode=="157") {
				return "Service/procedure was provided as a result of an act of war.";
			}
			if(reasonCode=="158") {
				return "Service/procedure was provided outside of the United States.";
			}
			if(reasonCode=="159") {
				return "Service/procedure was provided as a result of terrorism.";
			}
			if(reasonCode=="160") {
				return "Injury/illness was the result of an activity that is a benefit exclusion.";
			}
			if(reasonCode=="161") {
				return "Provider performance bonus";
			}
			if(reasonCode=="162") {
				return "State-mandated Requirement for Property and Casualty, see Claim Payment Remarks Code for specific explanation.";
			}
			if(reasonCode=="163") {
				return "Attachment referenced on the claim was not received.";
			}
			if(reasonCode=="164") {
				return "Attachment referenced on the claim was not received in a timely fashion.";
			}
			if(reasonCode=="165") {
				return "Referral absent or exceeded.";
			}
			if(reasonCode=="166") {
				return "These services were submitted after this payers responsibility for processing claims under this plan ended.";
			}
			if(reasonCode=="167") {
				return "This (these) diagnosis(es) is (are) not covered.";
			}
			if(reasonCode=="168") {
				return "Service(s) have been considered under the patient's medical plan. Benefits are not available under this dental plan.";
			}
			if(reasonCode=="169") {
				return "Alternate benefit has been provided.";
			}
			if(reasonCode=="170") {
				return "Payment is denied when performed/billed by this type of provider.";
			}
			if(reasonCode=="171") {
				return "Payment is denied when performed/billed by this type of provider in this type of facility.";
			}
			if(reasonCode=="172") {
				return "Payment is adjusted when performed/billed by a provider of this specialty.";
			}
			if(reasonCode=="173") {
				return "Service was not prescribed by a physician.";
			}
			if(reasonCode=="174") {
				return "Service was not prescribed prior to delivery.";
			}
			if(reasonCode=="175") {
				return "Prescription is incomplete.";
			}
			if(reasonCode=="176") {
				return "Prescription is not current.";
			}
			if(reasonCode=="177") {
				return "Patient has not met the required eligibility requirements.";
			}
			if(reasonCode=="178") {
				return "Patient has not met the required spend down requirements.";
			}
			if(reasonCode=="179") {
				return "Patient has not met the required waiting requirements.";
			}
			if(reasonCode=="180") {
				return "Patient has not met the required residency requirements.";
			}
			if(reasonCode=="181") {
				return "Procedure code was invalid on the date of service.";
			}
			if(reasonCode=="182") {
				return "Procedure modifier was invalid on the date of service.";
			}
			if(reasonCode=="183") {
				return "The referring provider is not eligible to refer the service billed.";
			}
			if(reasonCode=="184") {
				return "The prescribing/ordering provider is not eligible to prescribe/order the service billed.";
			}
			if(reasonCode=="185") {
				return "The rendering provider is not eligible to perform the service billed.";
			}
			if(reasonCode=="186") {
				return "Level of care change adjustment.";
			}
			if(reasonCode=="187") {
				return "Consumer Spending Account payments.";
			}
			if(reasonCode=="188") {
				return "This product/procedure is only covered when used according to FDA recommendations.";
			}
			if(reasonCode=="189") {
				return "'Not otherwise classified' or 'unlisted' procedure code (CPT/HCPCS) was billed when there is a specific procedure code for this procedure/service.";
			}
			if(reasonCode=="190") {
				return "Payment is included in the allowance for a Skilled Nursing Facility (SNF) qualified stay.";
			}
			if(reasonCode=="191") {
				return "Not a work related injury/illness and thus not the liability of the workers' compensation carrier";
			}
			if(reasonCode=="192") {
				return "Non standard adjustment code from paper remittance.";
			}
			if(reasonCode=="193") {
				return "Original payment decision is being maintained. Upon review, it was determined that this claim was processed properly.";
			}
			if(reasonCode=="194") {
				return "Anesthesia performed by the operating physician, the assistant surgeon or the attending physician.";
			}
			if(reasonCode=="195") {
				return "Refund issued to an erroneous priority payer for this claim/service.";
			}
			if(reasonCode=="197") {
				return "Precertification/authorization/notification absent.";
			}
			if(reasonCode=="198") {
				return "Precertification/authorization exceeded.";
			}
			if(reasonCode=="199") {
				return "Revenue code and Procedure code do not match.";
			}
			if(reasonCode=="200") {
				return "Expenses incurred during lapse in coverage";
			}
			if(reasonCode=="201") { //Use group code PR
				return "Workers' Compensation case settled. Patient is responsible for amount of this claim/service through WC 'Medicare set aside arrangement' or other agreement.";
			}
			if(reasonCode=="202") {
				return "Non-covered personal comfort or convenience services.";
			}
			if(reasonCode=="203") {
				return "Discontinued or reduced service.";
			}
			if(reasonCode=="204") {
				return "This service/equipment/drug is not covered under the patient’s current benefit plan.";
			}
			if(reasonCode=="205") {
				return "Pharmacy discount card processing fee";
			}
			if(reasonCode=="206") {
				return "National Provider Identifier - missing.";
			}
			if(reasonCode=="207") {
				return "National Provider identifier - Invalid format";
			}
			if(reasonCode=="208") {
				return "National Provider Identifier - Not matched.";
			}
			if(reasonCode=="209") { //Use Group code OA
				return "Per regulatory or other agreement. The provider cannot collect this amount from the patient. However, this amount may be billed to subsequent payer. Refund to patient if collected.";
			}
			if(reasonCode=="210") {
				return "Payment adjusted because pre-certification/authorization not received in a timely fashion";
			}
			if(reasonCode=="211") {
				return "National Drug Codes (NDC) not eligible for rebate, are not covered.";
			}
			if(reasonCode=="212") {
				return "Administrative surcharges are not covered";
			}
			if(reasonCode=="213") {
				return "Non-compliance with the physician self referral prohibition legislation or payer policy.";
			}
			if(reasonCode=="214") {
				return "Workers' Compensation claim adjudicated as non-compensable. This Payer not liable for claim or service/treatment.";
			}
			if(reasonCode=="215") {
				return "Based on subrogation of a third party settlement";
			}
			if(reasonCode=="216") {
				return "Based on the findings of a review organization";
			}
			if(reasonCode=="217") {
				return "Based on payer reasonable and customary fees. No maximum allowable defined by legislated fee arrangement.";
			}
			if(reasonCode=="218") {
				return "Based on entitlement to benefits.";
			}
			if(reasonCode=="219") {
				return "Based on extent of injury.";
			}
			if(reasonCode=="220") {
				return "The applicable fee schedule/fee database does not contain the billed code. Please resubmit a bill with the appropriate fee schedule/fee database code(s) that best describe the service(s) provided and supporting documentation if required.";
			}
			if(reasonCode=="221") {
				return "Workers' Compensation claim is under investigation.";
			}
			if(reasonCode=="222") {
				return "Exceeds the contracted maximum number of hours/days/units by this provider for this period. This is not patient specific.";
			}
			if(reasonCode=="223") {
				return "Adjustment code for mandated federal, state or local law/regulation that is not already covered by another code and is mandated before a new code can be created.";
			}
			if(reasonCode=="224") {
				return "Patient identification compromised by identity theft. Identity verification required for processing this and future claims.";
			}
			if(reasonCode=="225") {
				return "Penalty or Interest Payment by Payer";
			}
			if(reasonCode=="226") {
				return "Information requested from the Billing/Rendering Provider was not provided or was insufficient/incomplete.";
			}
			if(reasonCode=="227") {
				return "Information requested from the patient/insured/responsible party was not provided or was insufficient/incomplete.";
			}
			if(reasonCode=="228") {
				return "Denied for failure of this provider, another provider or the subscriber to supply requested information to a previous payer for their adjudication.";
			}
			if(reasonCode=="229") { //Use only with Group Code PR
				return "Partial charge amount not considered by Medicare due to the initial claim Type of Bill being 12X.";
			}
			if(reasonCode=="230") {
				return "No available or correlating CPT/HCPCS code to describe this service.";
			}
			if(reasonCode=="231") {
				return "Mutually exclusive procedures cannot be done in the same day/setting.";
			}
			if(reasonCode=="232") {
				return "Institutional Transfer Amount.";
			}
			if(reasonCode=="233") {
				return "Services/charges related to the treatment of a hospital-acquired condition or preventable medical error.";
			}
			if(reasonCode=="234") {
				return "This procedure is not paid separately.";
			}
			if(reasonCode=="235") {
				return "Sales Tax";
			}
			if(reasonCode=="236") {
				return "This procedure or procedure/modifier combination is not compatible with another procedure or procedure/modifier combination provided on the same day according to the National Correct Coding Initiative.";
			}
			if(reasonCode=="237") {
				return "Legislated/Regulatory Penalty.";
			}
			if(reasonCode=="238") { //Use Group Code PR
				return "Claim spans eligible and ineligible periods of coverage, this is the reduction for the ineligible period.";
			}
			if(reasonCode=="239") {
				return "Claim spans eligible and ineligible periods of coverage. Rebill separate claims.";
			}
			if(reasonCode=="240") {
				return "The diagnosis is inconsistent with the patient's birth weight.";
			}
			if(reasonCode=="241") {
				return "Low Income Subsidy (LIS) Co-payment Amount";
			}
			if(reasonCode=="242") {
				return "Services not provided by network/primary care providers.";
			}
			if(reasonCode=="243") {
				return "Services not authorized by network/primary care providers.";
			}
			if(reasonCode=="244") {
				return "Payment reduced to zero due to litigation. Additional information will be sent following the conclusion of litigation.";
			}
			if(reasonCode=="245") {
				return "Provider performance program withhold.";
			}
			if(reasonCode=="246") {
				return "This non-payable code is for required reporting only.";
			}
			if(reasonCode=="247") {
				return "Deductible for Professional service rendered in an Institutional setting and billed on an Institutional claim.";
			}
			if(reasonCode=="248") {
				return "Coinsurance for Professional service rendered in an Institutional setting and billed on an Institutional claim.";
			}
			if(reasonCode=="249") { //Use only with Group Code CO
				return "This claim has been identified as a readmission.";
			}
			if(reasonCode=="250") {
				return "The attachment content received is inconsistent with the expected content.";
			}
			if(reasonCode=="251") {
				return "The attachment content received did not contain the content required to process this claim or service.";
			}
			if(reasonCode=="252") {
				return "An attachment is required to adjudicate this claim/service.";
			}
			if(reasonCode=="A0") {
				return "Patient refund amount.";
			}
			if(reasonCode=="A1") {
				return "Claim/Service denied.";
			}
			if(reasonCode=="A5") {
				return "Medicare Claim PPS Capital Cost Outlier Amount.";
			}
			if(reasonCode=="A6") {
				return "Prior hospitalization or 30 day transfer requirement not met.";
			}
			if(reasonCode=="A7") {
				return "Presumptive Payment Adjustment";
			}
			if(reasonCode=="A8") {
				return "Ungroupable DRG.";
			}
			if(reasonCode=="B1") {
				return "Non-covered visits.";
			}
			if(reasonCode=="B4") {
				return "Late filing penalty.";
			}
			if(reasonCode=="B5") {
				return "Coverage/program guidelines were not met or were exceeded.";
			}
			if(reasonCode=="B7") {
				return "This provider was not certified/eligible to be paid for this procedure/service on this date of service.";
			}
			if(reasonCode=="B8") {
				return "Alternative services were available, and should have been utilized.";
			}
			if(reasonCode=="B9") {
				return "Patient is enrolled in a Hospice.";
			}
			if(reasonCode=="B10") {
				return "Allowed amount has been reduced because a component of the basic procedure/test was paid. The beneficiary is not liable for more than the charge limit for the basic procedure/test.";
			}
			if(reasonCode=="B11") {
				return "The claim/service has been transferred to the proper payer/processor for processing. Claim/service not covered by this payer/processor.";
			}
			if(reasonCode=="B12") {
				return "Services not documented in patients' medical records.";
			}
			if(reasonCode=="B13") {
				return "Previously paid. Payment for this claim/service may have been provided in a previous payment.";
			}
			if(reasonCode=="B14") {
				return "Only one visit or consultation per physician per day is covered.";
			}
			if(reasonCode=="B15") {
				return "This service/procedure requires that a qualifying service/procedure be received and covered. The qualifying other service/procedure has not been received/adjudicated.";
			}
			if(reasonCode=="B16") {
				return "'New Patient' qualifications were not met.";
			}
			if(reasonCode=="B20") {
				return "Procedure/service was partially or fully furnished by another provider.";
			}
			if(reasonCode=="B22") {
				return "This payment is adjusted based on the diagnosis.";
			}
			if(reasonCode=="B23") {
				return "Procedure billed is not authorized per your Clinical Laboratory Improvement Amendment (CLIA) proficiency test.";
			}
			if(reasonCode=="W1") {
				return "Workers' compensation jurisdictional fee schedule adjustment.";
			}
			if(reasonCode=="W2") {
				return "Payment reduced or denied based on workers' compensation jurisdictional regulations or payment policies, use only if no other code is applicable.";
			}
			if(reasonCode=="W3") {
				return "The Benefit for this Service is included in the payment/allowance for another service/procedure that has been performed on the same day.";
			}
			if(reasonCode=="W4") {
				return "Workers' Compensation Medical Treatment Guideline Adjustment.";
			}
			if(reasonCode=="Y1") {
				return "Payment denied based on Medical Payments Coverage (MPC) or Personal Injury Protection (PIP) Benefits jurisdictional regulations or payment policies, use only if no other code is applicable.";
			}
			if(reasonCode=="Y2") {
				return "Payment adjusted based on Medical Payments Coverage (MPC) or Personal Injury Protection (PIP) Benefits jurisdictional regulations or payment policies, use only if no other code is applicable.";
			}
			if(reasonCode=="Y3") {
				return "Medical Payments Coverage (MPC) or Personal Injury Protection (PIP) Benefits jurisdictional fee schedule adjustment.";
			}
			return "Reason "+reasonCode+".";//Worst case, if we do not recognize the code, display it verbatim so the user can look it up.
		}
			

  }
}

//Example 1 From 835 Specification:
//ST*835*1234~
//BPR*C*150000*C*ACH*CTX*01*999999992*DA*123456*1512345678*01*999988880*DA*98765*20020913~
//TRN*1*12345*1512345678~
//DTM*405*20020916~
//N1*PR*INSURANCE COMPANY OF TIMBUCKTU~
//N3*1 MAIN STREET~
//N4*TIMBUCKTU*AK*89111~
//REF*2U*999~
//N1*PE*REGIONAL HOPE HOSPITAL*XX*6543210903~
//LX*110212~
//TS3*6543210903*11*20021231*1*211366.97****138018.4**73348.57~
//TS2*2178.45*1919.71**56.82*197.69*4.23~
//CLP*666123*1*211366.97*138018.4**MA*1999999444444*11*1~
//CAS*CO*45*73348.57~
//NM1*QC*1*JONES*SAM*O***HN*666666666A~
//MIA*0***138018.4~
//DTM*232*20020816~
//DTM*233*20020824~
//QTY*CA*8~
//LX*130212~
//TS3*6543210909*13*19961231*1*15000****11980.33**3019.67~
//CLP*777777*1*150000*11980.33**MB*1999999444445*13*1~
//CAS*CO*35*3019.67~
//NM1*QC*1*BORDER*LIZ*E***HN*996669999B~
//MOA***MA02~
//DTM*232*20020512~
//PLB*6543210903*20021231*CV:CP*-1.27~
//SE*28*1234~

//Example 2 From 835 Specification:
//ST*835*12233~
//BPR*I*945*C*ACH*CCP*01*888999777*DA*24681012*1935665544*01*111333555*DA*144444*20020316~
//TRN*1*71700666555*1935665544~
//DTM*405*20020314~
//N1*PR*RUSHMORE LIFE~
//N3*10 SOUTH AVENUE~
//N4*RAPID CITY*SD*55111~
//N1*PE*ACME MEDICAL CENTER*XX*5544667733~
//REF*TJ*777667755~
//LX*1~
//CLP*55545554444*1*800*450*300*12*94060555410000~
//CAS*CO*A2*50~
//NM1*QC*1*BUDD*WILLIAM****MI*33344555510~
//SVC*HC:99211*800*500~
//DTM*150*20020301~
//DTM*151*20020304~
//CAS*PR*1*300~
//CLP*8765432112*1*1200*495*600*12*9407779923000~
//CAS*CO*A2*55~
//NM1*QC*1*SETTLE*SUSAN****MI*44455666610~
//SVC*HC:93555*1200*550~
//DTM*150*20020310~
//DTM*151*20020312~
//CAS*PR*1*600~
//CAS*CO*45*50~
//SE*25*112233~

//Example 3 From 835 Specification:
//ST*835*0001~
//BPR*I*1222*C*CHK************20050412~
//TRN*1*0012524965*1559123456~
//REF*EV*030240928~
//DTM*405*20050412~
//N1*PR*YOUR TAX DOLLARS AT WORK~
//N3*481A00 DEER RUN ROAD~
//N4*WEST PALM BCH*FL*11114~
//N1*PE*ACME MEDICAL CENTER*FI*5999944521~
//N3*PO BOX 863382~
//N4*ORLANDO*FL*55115~
//REF*PQ*10488~
//LX*1~
//CLP*L0004828311*2*10323.64*912**12*05090256390*11*1~
//CAS*OA*23*9411.64~
//NM1*QC*1*TOWNSEND*WILLIAM*P***MI*XXX123456789~
//NM1*82*2*ACME MEDICAL CENTER*****BD*987~
//DTM*232*20050303~
//DTM*233*20050304~
//AMT*AU*912~
//LX*2~
//CLP*0001000053*2*751.50*310*220*12*50630626430~
//NM1*QC*1*BAKI*ANGI****MI*456789123~
//NM1*82*2*SMITH JONES PA*****BS*34426~
//DTM*232*20050106~
//DTM*233*20050106~
//SVC*HC>12345>26*166.5*30**1~
//DTM*472*20050106~
//CAS*OA*23*136.50~
//REF*1B*43285~
//AMT*AU*150~
//SVC*HC>66543>26*585*280*220*1~
//DTM*472*20050106~
//CAS*PR*1*150**2*70~
//CAS*CO*42*85~
//REF*1B*43285~
//AMT*AU*500~
//SE*38*0001~

//Example 4 From 835 Specification:
//ST*835*0001~
//BPR*I*187.50*C*CHK************20050412~
//TRN*1*0012524879*1559123456~
//REF*EV*030240928~
//DTM*405*20050412~
//N1*PR*YOUR TAX DOLLARS AT WORK~
//N3*481A00 DEER RUN ROAD~
//N4*WEST PALM BCH*FL*11114~
//N1*PE*ACME MEDICAL CENTER*FI*599944521~
//N3*PO BOX 863382~
//N4*ORLANDO*FL*55115~
//REF*PQ*10488~
//LX*1~
//CLP*0001000054*3*1766.5*187.50**12*50580155533~
//NM1*QC*1*ISLAND*ELLIS*E****MI*789123456~
//NM1*82*2*JONES JONES ASSOCIATES*****BS*AB34U~
//DTM*232*20050120~
//SVC*HC*24599*1766.5*187.50**1~
//DTM*472*20050120~
//CAS*OA*23*1579~
//REF*1B*44280~
//AMT*AU*1700~
//SE*38*0001~

//Example 5 From 835 Specification:
//ST*835*0001~
//BPR*I*34.00*C*CHK************20050318~
//TRN*1*0063158ABC*1566339911~
//REF*EV*030240928~
//DTM*405*20050318~
//N1*PR*YOUR TAX DOLLARS AT WORK~
//N3*481A00 DEER RUN ROAD~
//N4*WEST PALM BCH*FL*11114~
//N1*PE*ATONEWITHHEALTH*FI*3UR334563~
//N3*3501 JOHNSON STREET~
//N4*SUNSHINE*FL*12345~
//REF*PQ*11861~
//LX*1~
//CLP*0001000055*2*541*34**12*50650619501~
//NM1*QC*1*BRUCK*RAYMOND*W***MI*987654321~
//NM1*82*2*PROFESSIONAL TEST 1*****BS*34426~
//DTM*232*20050202~
//DTM*233*20050202~
//SVC*HC>55669*541*34**1~
//DTM*472*20050202~
//CAS*OA*23*516~
//CAS*OA*94*-9~
//REF*1B*44280~
//AMT*AU*550~
//SE*38*0001~