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
		///<summary>PER*BL segment of loop 1000A (pg. 97). Payer technical contact information. Required.</summary>
		private X12Segment segPER_BL;
		///<summary>N1*PE segment of loop 1000B (pg. 102). Payee identification. Required. We include this information because it could be helpful for those customers who are using clinics.</summary>
		private X12Segment segN1_PE;
		///<summary>CLP of loop 2100 (pg. 123). Claim payment information.</summary>
		private List<int> segNumsCLP;
		///<summary>SVC of loop 2110 (pg. 186). Service (procedure) payment information.</summary>
		private List<int> segNumsSVC;

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
			for(int i=0;i<segments.Count;i++) {
				X12Segment seg=segments[i];
				if(seg.SegmentID=="N1" && seg.Get(1)=="PR") {
					segN1_PR=seg;
					segN3_PR=segments[i+1];
					segN4_PR=segments[i+2];
				}
				else if(seg.SegmentID=="PER" && seg.Get(1)=="BL") {
					segPER_BL=seg;
				}
				else if(seg.SegmentID=="N1" && seg.Get(1)=="PE") {
					segN1_PE=seg;
				}
			}
			segNumsCLP=new List<int>();
			segNumsSVC=new List<int>();
			for(int i=0;i<segments.Count;i++) { //All segments which have unique names within the 835 format can go inside this loop.
				if(segments[i].SegmentID=="CLP") { //The only place CLP segments exist is within the 2100 loop.
					segNumsCLP.Add(i);
				}
				else if(segments[i].SegmentID=="SVC") { //The only place SVC segments exist is within the 2110 loop.
					segNumsSVC.Add(i);
				}
			}
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

		///<summary>Result will contain strings in the following order: Claim Status Code (CLP02), Monetary Amount of submitted charges for this claim (CLP03), Monetary Amount paid on this claim (CLP04), Monetary Amount of patient responsibility (CLP05), Payer Claim Control Number (CLP07).</summary>
    public string[] GetClaimInfo(string trackingNumber) {
      string[] result=new string[5];
      for(int i=0;i<result.Length;i++) {
        result[i]="";
      }
      for(int i=0;i<segNumsCLP.Count;i++) {
        int segNum=segNumsCLP[i];
				X12Segment seg=segments[segNum];//CLP segment.
				if(seg.Get(1)!=trackingNumber) {//CLP01
					continue;
				}
				result[0]=seg.Get(2);//CLP02
				result[1]=seg.Get(3);//CLP03
				result[2]=seg.Get(4);//CLP04
				result[3]=seg.Get(5);//CLP05
				result[4]=seg.Get(7);//CLP07
				break;
      }
      return result;
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