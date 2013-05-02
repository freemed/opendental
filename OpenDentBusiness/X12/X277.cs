using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace OpenDentBusiness
{
	///<summary>X12 277 Unsolicited Claim Status Notification. There is only one type of 277, but a 277 can be sent out unsolicited (without sending a request) or as a response to a 276 request.</summary>
	public class X277:X12object {

		private List<X12Segment> segments;
		///<summary>NM1 of loop 2100A.</summary>
		private int segNumInfoSourceNM101;
		///<summary>NM1 of loop 2100B.</summary>
		private int segNumInfoReceiverNM101;
		///<summary>NM1 of loop 2100C.</summary>
		private List<int> segNumsBillingProviderNM1;
		///<summary>NM1 of loop 2100D.</summary>
		private List<int> segNumsPatientDetailNM1;
		///<summary>TRN of loop 2200D.</summary>
		private List<int> segNumsClaimTrackingNumberTRN;

		public static bool Is277(X12object xobj) {
			if(xobj.FunctGroups.Count!=1) {//Exactly 1 GS segment in each 277.
				return false;
			}
			if(xobj.FunctGroups[0].Header.Get(1)=="HN") {//GS01 (pgs. 139 & 7)
				return true;
			}
			return false;
		}

		public X277(string messageText)
			: base(messageText) {
			segments=FunctGroups[0].Transactions[0].Segments;//The GS segment contains exactly one ST segment below it.
			segNumInfoSourceNM101=-1;
			segNumInfoReceiverNM101=-1;
			segNumsBillingProviderNM1=new List<int>();
			segNumsPatientDetailNM1=new List<int>();
			segNumsClaimTrackingNumberTRN=new List<int>();
			for(int i=0;i<segments.Count;i++) {
				X12Segment seg=segments[i];
				if(seg.SegmentID=="NM1") {
					string entityIdentifierCode=seg.Get(1);
					if(entityIdentifierCode=="AY" || entityIdentifierCode=="PR") {
						segNumInfoSourceNM101=i;
						i+=4;
					}
					else if(entityIdentifierCode=="41") {
						segNumInfoReceiverNM101=i;
						i+=3;
						seg=segments[i];
						while(seg.SegmentID=="STC") {
							i++;
							seg=segments[i];
						}
						i+=4;
					}
					else if(entityIdentifierCode=="85") {
						segNumsBillingProviderNM1.Add(i);
					}
					else if(entityIdentifierCode=="QC") {
						segNumsPatientDetailNM1.Add(i);
						//Loop 2200D: There can be multiple TRN segments for each NM1*QC.
						do {
							i++;
							segNumsClaimTrackingNumberTRN.Add(i);//a TRN segment is required at this location.
							i++;
							seg=segments[i];//at least one STC segment is required at this location.
							while(seg.SegmentID=="STC") {//there may be multiple STC segments.
								i++;
								seg=segments[i];
							}
							//Followed by 0 to 3 situational REF segments.
							for(int j=0;j<3 && (seg.SegmentID=="REF");j++) {
								i++;
								seg=segments[i];
							}
							//Followed by 0 or 1 DTP segments. 
							if(seg.SegmentID=="DTP") {
								i++;
								seg=segments[i];
							}
							//An entire iteration of loop 2200D is now finished. If another iteration is present, it will begin with a TRN segment.
						} while(seg.SegmentID=="TRN");
					}
				}
			}
		}

		///<summary>NM101 of loop 2100A.</summary>
		public string GetInformationSourceType() {
			if(segNumInfoSourceNM101!=-1) {
				if(segments[segNumInfoSourceNM101].Get(1)=="AY") {
					return "Clearinghouse";
				}
				return "Payor";
			}
			return "";
		}

		///<summary>NM103 of loop 2100A.</summary>
		public string GetInformationSourceName() {
			if(segNumInfoSourceNM101!=-1) {
				return segments[segNumInfoSourceNM101].Get(3);
			}
			return "";
		}

		///<summary>DTP03 of loop 2200A.</summary>
		public DateTime GetInformationSourceReceiptDate() {
			if(segNumInfoSourceNM101!=-1) {
				try {
					string dateStr=segments[segNumInfoSourceNM101+2].Get(3);
					int dateYear=PIn.Int(dateStr.Substring(0,4));
					int dateMonth=PIn.Int(dateStr.Substring(4,2));
					int dateDay=PIn.Int(dateStr.Substring(6,2));
					return new DateTime(dateYear,dateMonth,dateDay);
				}
				catch {
				}
			}
			return DateTime.MinValue;
		}

		///<summary>DTP03 of loop 2200A.</summary>
		public DateTime GetInformationSourceProcessDate() {
			if(segNumInfoSourceNM101!=-1) {
				try {
					string dateStr=segments[segNumInfoSourceNM101+3].Get(3);
					int dateYear=PIn.Int(dateStr.Substring(0,4));
					int dateMonth=PIn.Int(dateStr.Substring(4,2));
					int dateDay=PIn.Int(dateStr.Substring(6,2));
					return new DateTime(dateYear,dateMonth,dateDay);
				}
				catch {
				}
			}
			return DateTime.MinValue;
		}

		///<summary>Last STC segment in loop 2200B. Returns -1 on error.</summary>
		private int GetSegNumLastSTC2200B() {
			if(segNumInfoReceiverNM101!=-1) {
				int segNum=segNumInfoReceiverNM101+2;
				X12Segment seg=segments[segNum];
				while(seg.SegmentID=="STC") {
					segNum++;
					//End of message can happen because the QTY and AMT segments are situational, and so are the two HL segments after this.
					if(segNum>=segments.Count) {
						return segNum-1;
					}
					seg=segments[segNum];
				}
				return segNum-1;
			}
			return -1;
		}

		///<summary>QTY02 of loop 2200B.</summary>
		public long GetQuantityAccepted() {
			int segNum=GetSegNumLastSTC2200B();
			if(segNum!=-1) {
				segNum++;
				if(segNum<segments.Count) {
					X12Segment seg=segments[segNum];
					if(seg.SegmentID=="QTY" && seg.Get(1)=="90") {
						return long.Parse(seg.Get(2));
					}
				}
			}
			return 0;
		}

		///<summary>QTY02 of loop 2200B.</summary>
		public long GetQuantityRejected() {
			int segNum=GetSegNumLastSTC2200B();
			if(segNum!=-1) {
				segNum++;
				if(segNum<segments.Count) {
					X12Segment seg=segments[segNum];
					if(seg.SegmentID=="QTY") {
						try {
							if(seg.Get(1)=="AA") {
								return long.Parse(seg.Get(2));
							}
							else {
								segNum++;
								if(segNum<segments.Count) {
									seg=segments[segNum];
									if(seg.SegmentID=="QTY" && seg.Get(1)=="AA") {
										return long.Parse(seg.Get(2));
									}
								}
							}
						}
						catch {
						}
					}
				}
			}
			return 0;
		}

		///<summary>AMT02 of loop 2200B.</summary>
		public double GetAmountAccepted() {
			int segNum=GetSegNumLastSTC2200B();
			if(segNum!=-1) {
				segNum++;
				if(segNum<segments.Count) {
					X12Segment seg=segments[segNum];
					while(seg.SegmentID=="QTY") {
						segNum++;
						if(segNum>=segments.Count) {
							return 0;
						}
						seg=segments[segNum];
					}
					if(seg.SegmentID=="AMT" && seg.Get(1)=="YU") {
						return double.Parse(seg.Get(2));
					}
				}
			}
			return 0;
		}

		///<summary>AMT02 of loop 2200B.</summary>
		public double GetAmountRejected() {
			int segNum=GetSegNumLastSTC2200B();
			if(segNum!=-1) {
				segNum++;
				if(segNum<segments.Count) {
					X12Segment seg=segments[segNum];
					while(seg.SegmentID=="QTY") {
						segNum++;
						if(segNum>=segments.Count) {
							return 0;
						}
						seg=segments[segNum];
					}
					if(seg.SegmentID=="AMT") {
						if(seg.Get(1)=="YY") {
							return double.Parse(seg.Get(2));
						}
						else {
							segNum++;
							if(segNum<segments.Count) {
								seg=segments[segNum];
								if(seg.SegmentID=="AMT" && seg.Get(1)=="YY") {
									return double.Parse(seg.Get(2));
								}
							}
						}
					}
				}
			}
			return 0;
		}

		///<summary>TRN02 in loop 2200D. Do this first to get a list of all claim tracking numbers that are contained within this 277.  Then, for each claim tracking number, we can later retrieve the AckCode for that single claim. The claim tracking numbers correspond to CLM01 exactly as submitted in the 837. We refer to CLM01 as the claim identifier on our end. We allow more than just digits in our claim identifiers, so we must return a list of strings.</summary>
		public List<string> GetClaimTrackingNumbers() {
			List<string> retVal=new List<string>();
			for(int i=0;i<segNumsClaimTrackingNumberTRN.Count;i++) {
				X12Segment seg=segments[segNumsClaimTrackingNumberTRN[i]];//TRN segment.
				retVal.Add(seg.Get(2));
			}
			return retVal;
		}

		///<summary>Result will contain strings in the following order: 0 Patient Last Name (NM103), 1 Patient First Name (NM104), 2 Patient Middle Name (NM105), 
		///3 Claim Status (STC03), 4 Payor's Claim Control Number (REF02), 5 Institutional Type of Bill (REF02), 6 Claim Date Service Start (DTP03), 
		///7 Claim Date Service End (DTP03), 8 Reason (STC01-2), 9 Amount (STC04)</summary>
		public string[] GetClaimInfo(string trackingNumber) {
			string[] result=new string[10];
			for(int i=0;i<result.Length;i++) {
				result[i]="";
			}
			for(int i=0;i<segNumsClaimTrackingNumberTRN.Count;i++) {
				int segNum=segNumsClaimTrackingNumberTRN[i];
				X12Segment seg=segments[segNum];//TRN segment.
				if(seg.Get(2)==trackingNumber) { //TRN02
					//Locate the NM1 segment corresponding to the claim tracking number. One NM1 segment can be shared with multiple TRN segments.
					//The strategy is to locate the NM1 segment furthest down in the message that is above the TRN segment for the tracking number.
					int segNumNM1=segNumsPatientDetailNM1[segNumsPatientDetailNM1.Count-1];//very last NM1 segment
					for(int j=0;j<segNumsPatientDetailNM1.Count-1;j++) {
						if(segNum>segNumsPatientDetailNM1[j] && segNum<segNumsPatientDetailNM1[j+1]) {
							segNumNM1=segNumsPatientDetailNM1[j];
							break;
						}
					}
					seg=segments[segNumNM1];//NM1 segment.
					result[0]=seg.Get(3);//NM103 Last Name
					result[1]=seg.Get(4);//NM104 First Name
					result[2]=seg.Get(5);//NM105 Middle Name
					segNum++;
					seg=segments[segNum];//STC segment. At least one, maybe multiple, but we only care about the first one.
					string[] stc01=seg.Get(1).Split(new string[] { Separators.Subelement },StringSplitOptions.None);
					//The codes are pulled from the Washington Publishing Company website (the makers of X12).
					//http://www.wpc-edi.com/reference/codelists/healthcare/claim-status-category-codes/
					//Code source 507. Since we only send batches, we can only get status codes starting with "A" returned to us.
					switch(stc01[0]) {
						case "A0"://Acknowledgement/Forwarded-The claim/encounter has been forwarded to another entity.
							result[3]="A";
							break;
						case "A1"://Acknowledgement/Receipt-The claim/encounter has been received. This does not mean that the claim has been accepted for adjudication.
							result[3]="A";//We say accepted, because if rejected later, then there should hopefully be another 277 which overwrites this one.
							break;
						case "A2"://Acknowledgement/Acceptance into adjudication system-The claim/encounter has been accepted into the adjudication system.
							result[3]="A";
							break;
						case "A3"://Acknowledgement/Returned as unprocessable claim-The claim/encounter has been rejected and has not been entered into the adjudication system.
							result[3]="R";
							break;
						case "A4"://Acknowledgement/Not Found-The claim/encounter can not be found in the adjudication system.
							result[3]="R";//Not really rejected, but no way it could be accepted.
							break;
						case "A5"://Acknowledgement/Split Claim-The claim/encounter has been split upon acceptance into the adjudication system.
							result[3]="A";
							break;
						case "A6"://Acknowledgement/Rejected for Missing Information - The claim/encounter is missing the information specified in the Status details and has been rejected.
							result[3]="R";
							break;
						case "A7"://Acknowledgement/Rejected for Invalid Information - The claim/encounter has invalid information as specified in the Status details and has been rejected.
							result[3]="R";
							break;
						case "A8"://cknowledgement / Rejected for relational field in error.
							result[3]="R";
							break;
						default:
							result[3]="";//Unknown.
							break;
					}
					result[8]=GetStringForExternalSourceCode508(PIn.Int(stc01[1]));//STC01-2
					result[9]=seg.Get(4);
					//Skip the remaining STC segments (if any).
					segNum++;
					seg=segments[segNum];
					while(seg.SegmentID=="STC") {
						segNum++;
						seg=segments[segNum];
					}
					while(seg.SegmentID=="REF") {
						string refIdQualifier=seg.Get(1);
						if(refIdQualifier=="1K") {
							result[4]=seg.Get(2);//REF02 Payor's Claim Control Number.
						}
						else if(refIdQualifier=="D9") {
							//REF02 Claim Identifier Number for Clearinghouse and Other Transmission Intermediary from the 837.
							//When we send this it is the same as the claim identifier/claim tracking number, so we don't use this for now.
						}
						else if(refIdQualifier=="BLT") {
							//REF02 Institutional Type of Bill that was sent in the 837.
							result[5]=seg.Get(2);
						}
						segNum++;
						seg=segments[segNum];
					}
					//The DTP segment for the date of service will not be present when an invalid date was originally sent to the carrier (even though the specifications have it marked as a required segment).
					if(seg.SegmentID=="DTP") {
						string dateServiceStr=seg.Get(3);
						int dateServiceStartYear=PIn.Int(dateServiceStr.Substring(0,4));
						int dateServiceStartMonth=PIn.Int(dateServiceStr.Substring(4,2));
						int dateServiceStartDay=PIn.Int(dateServiceStr.Substring(6,2));
						result[6]=(new DateTime(dateServiceStartYear,dateServiceStartMonth,dateServiceStartDay)).ToShortDateString();
						if(dateServiceStr.Length==17) { //Date range.
							int dateServiceEndYear=PIn.Int(dateServiceStr.Substring(9,4));
							int dateServiceEndMonth=PIn.Int(dateServiceStr.Substring(13,2));
							int dateServiceEndDay=PIn.Int(dateServiceStr.Substring(15,2));
							result[7]=(new DateTime(dateServiceEndYear,dateServiceEndMonth,dateServiceEndDay)).ToShortDateString();
						}
					}
				}
			}
			return result;
		}

		public string GetHumanReadable() {
			string result=
				"Claim Status Reponse From "+GetInformationSourceType()+" "+GetInformationSourceName()+Environment.NewLine
				+"Receipt Date: "+GetInformationSourceReceiptDate().ToShortDateString()+Environment.NewLine
				+"Process Date: "+GetInformationSourceProcessDate().ToShortDateString()+Environment.NewLine
				+"Quantity Accepted: "+GetQuantityAccepted()+Environment.NewLine
				+"Quantity Rejected: "+GetQuantityRejected()+Environment.NewLine
				+"Amount Accepted: "+GetAmountAccepted()+Environment.NewLine
				+"Amount Rejected: "+GetAmountRejected()+Environment.NewLine
				+"Individual Claim Status List: "+Environment.NewLine
				+"Tracking Num  LName  FName  MName  Status  PayorControlNum  InstBillType DateServiceStart  DateServiceEnd";
			List<string> claimTrackingNumbers=GetClaimTrackingNumbers();
			for(int i=0;i<claimTrackingNumbers.Count;i++) {
				string[] claimInfo=GetClaimInfo(claimTrackingNumbers[i]);
				for(int j=0;j<claimInfo.Length;j++) {
					result+=claimInfo[j]+"\\t";
				}
				result+=Environment.NewLine;
			}
			return result;
		}

		///<summary>Convert the given source code to a string. Only 0 to 56 included so far. All codes listed at http://www.wpc-edi.com/reference/codelists/healthcare/claim-status-codes. </summary>
		public static string GetStringForExternalSourceCode508(int sourceCode) {
			switch(sourceCode) {
				case 0:
					return "Cannot provide further status electronically.";
				case 1:
					return "For more detailed information, see remittance advice.";
				case 2:
					return "More detailed information in letter.";
				case 3:
					return "Claim has been adjudicated and is awaiting payment cycle.";
				case 6:
					return "Balance due from the subscriber.";
				case 12:
					return "One or more originally submitted procedure codes have been combined.";
				case 15:
					return "One or more originally submitted procedure code have been modified.";
				case 16:
					return "Claim/encounter has been forwarded to entity. Note: This code requires use of an Entity Code.";
				case 17:
					return "Claim/encounter has been forwarded by third party entity to entity. Note: This code requires use of an Entity Code.";
				case 18:
					return "Entity received claim/encounter, but returned invalid status. Note: This code requires use of an Entity Code.";
				case 19:
					return "Entity acknowledges receipt of claim/encounter. Note: This code requires use of an Entity Code.";
				case 20:
					return "Accepted for processing.";
				case 21:
					return "Missing or invalid information. Note: At least one other status code is required to identify the missing or invalid information.";
				case 23:
					return "Returned to Entity. Note: This code requires use of an Entity Code.";
				case 24:
					return "Entity not approved as an electronic submitter. Note: This code requires use of an Entity Code.";
				case 25:
					return "Entity not approved. Note: This code requires use of an Entity Code.";
				case 26:
					return "Entity not found. Note: This code requires use of an Entity Code.";
				case 27:
					return "Policy canceled.";
				case 29:
					return "Subscriber and policy number/contract number mismatched.";
				case 30:
					return "Subscriber and subscriber id mismatched.";
				case 31:
					return "Subscriber and policyholder name mismatched.";
				case 32:
					return "Subscriber and policy number/contract number not found.";
				case 33:
					return "Subscriber and subscriber id not found.";
				case 34:
					return "Subscriber and policyholder name not found.";
				case 35:
					return "Claim/encounter not found.";
				case 37:
					return "Predetermination is on file, awaiting completion of services.";
				case 38:
					return "Awaiting next periodic adjudication cycle.";
				case 39:
					return "Charges for pregnancy deferred until delivery.";
				case 40:
					return "Waiting for final approval.";
				case 41:
					return "Special handling required at payer site.";
				case 42:
					return "Awaiting related charges.";
				case 44:
					return "Charges pending provider audit.";
				case 45:
					return "Awaiting benefit determination.";
				case 46:
					return "Internal review/audit.";
				case 47:
					return "Internal review/audit - partial payment made.";
				case 49:
					return "Pending provider accreditation review.";
				case 50:
					return "Claim waiting for internal provider verification.";
				case 51:
					return "Investigating occupational illness/accident.";
				case 52:
					return "Investigating existence of other insurance coverage.";
				case 53:
					return "Claim being researched for Insured ID/Group Policy Number error.";
				case 54:
					return "Duplicate of a previously processed claim/line.";
				case 55:
					return "Claim assigned to an approver/analyst.";
				case 56:
					return "Awaiting eligibility determination.";
			}
			return sourceCode.ToString();//There are over 700 total, but we chose to display the most common codes. This will at least display the code number for the user so they can look it up.
		}

	}
}

//EXAMPLE 1 - From X12 Specification
//ISA*00*          *00*          *ZZ*810624427      *ZZ*133052274      *060131*0756*^*00501*000000017*0*T*:~
//GS*HN*810624427*133052274*20060131*0756*17*X*005010X214~
//ST*277*0001*005010X214~
//BHT*0085*08*277X2140001*20060205*1635*TH~
//HL*1**20*1~
//NM1*AY*2*FIRST CLEARINGHOUSE*****46*CLHR00~
//TRN*1*200102051635S00001ABCDEF~
//DTP*050*D8*20060205~
//DTP*009*D8*20060207~
//HL*2*1*21*1~
//NM1*41*2*BEST BILLING SERVICE*****46*S00001~
//TRN*2*2002020542857~
//STC*A0:16:PR*20060205*WQ*1000~
//QTY*90*1~
//QTY*AA*2~
//AMT*YU*200~
//AMT*YY*800~
//HL*3*2*19*1~
//NM1*85*2*SMITH CLINIC*****FI*123456789~
//HL*4*3*PT~
//NM1*QC*1*DOE*JOHN****MI*00ABCD1234~
//TRN*2*4001/1339~
//STC*A0:16:PR*20060205*WQ*200~
//REF*1K*22029500123407X~
//DTP*472*RD8*20060128-20060131~
//HL*5*3*PT~
//NM1*QC*1*DOE*JANE****MI*45613027602~
//TRN*2*2890/4~
//STC*A3:21:82*20060205*U*500~
//DTP*472*D8*20060115~
//SVC*HC:22305:22*350*****1~
//STC*A3:122**U*******A3:153:82~
//REF*FJ*11~
//HL*6*3*PT~
//NM1*QC*1*VEST*HELEN****MI*45602708901~
//TRN*2*00000000000000000000~
//STC*A3:401*20060205*U*300~
//DTP*472*RD8*20060120-20060120~
//SE*37*0001~
//GE*1*17~
//IEA*1*000000017~

//EXAMPLE 2 - From X12 Specification
//ISA*00*          *00*          *ZZ*810624427      *ZZ*133052274      *060131*0756*^*00501*000000017*0*T*:~
//GS*HN*810624427*133052274*20060131*0756*17*X*005010X214~
//ST*277*0002*005010X214~
//BHT*0085*08*277X2140002*20060201*0405*TH~
//HL*1**20*1~
//NM1*AY*2*FIRST CLEARINGHOUSE*****46*CLHR00~
//TRN*1*200201312005S00002XYZABC~
//DTP*050*D8*20060131~
//DTP*009*D8*20060201~
//HL*2*1*21*0~
//NM1*41*2*LAST BILLING SERVICE*****46*S00002~
//TRN*2*20020131052389~
//STC*A3:24:41**U~
//QTY*AA*3~
//AMT*YY*800~
//SE*14*00002~
//GE*1*17~
//IEA*1*000000017~

//EXAMPLE 3 - From X12 Specification
//ISA*00*          *00*          *ZZ*810624427      *ZZ*133052274      *060131*0756*^*00501*000000017*0*T*:~
//GS*HN*810624427*133052274*20060131*0756*17*X*005010X214~
//ST*277*0003*005010X214~
//BHT*0085*08*277X2140003*20060221*1025*TH~
//HL*1**20*1~
//NM1*PR*2*YOUR INSURANCE COMPANY*****PI*YIC01~
//TRN*1*0091182~
//DTP*050*D8*20060220~
//DTP*009*D8*20060221~
//HL*2*1*21*1~
//NM1*41*1*JONES*HARRY*B**MD*46*S00003~
//TRN*2*2002022045678~
//STC*A1:19:PR*20060221*WQ*365.5~
//QTY*90*3~
//QTY*AA*2~
//AMT*YU*200.5~
//AMT*YY*165~
//HL*3*2*19*1~
//NM1*85*1*JONES*HARRY*B**MD*FI*234567894~
//HL*4*3*PT~
//NM1*QC*1*PATIENT*FEMALE****MI*2222222222~
//TRN*2*PATIENT22222~
//STC*A2:20:PR*20060221*WQ*100~
//REF*1K*220216359803X~
//DTP*472*D8*20060214~
//HL*5*3*PT~
//NM1*QC*1*PATIENT*MALE****MI*3333333333~
//TRN*2*PATIENT33333~
//STC*A3:187:PR*20060221*U*65~
//DTP*472*D8*20090221~
//HL*6*3*PT~
//NM1*QC*1*JONES*LARRY****MI*4444444444~
//TRN*2*JONES44444~
//STC*A3:21:77*20060221*U*100~
//DTP*472*D8*20060211~
//HL*7*3*PT~
//NM1*QC*1*JOHNSON*MARY****MI*5555555555~
//TRN*2*JONHSON55555~
//STC*A2:20:PR*20060221*WQ*50.5~
//REF*1K*220216359806X~
//DTP*472*D8*20060210~
//HL*8*3*PT~
//NM1*QC*1*MILLS*HARRIETT****MI*6666666666~
//TRN*2*MILLS66666~
//STC*A2:20:PR*20060221*WQ*50~
//REF*1K*220216359807X~
//DTP*472*D8*20060205~
//SE*46*0003~
//GE*1*17~
//IEA*1*000000017~

//EXAMPLE 4 - From X12 Specification
//ISA*00*          *00*          *ZZ*810624427      *ZZ*133052274      *060131*0756*^*00501*000000017*0*T*:~
//GS*HN*810624427*133052274*20060131*0756*17*X*005010X214~
//ST*277*0004*005010X214~
//BHT*0085*08*277X2140004*20060321*1025*TH~
//HL*1**20*1~
//NM1*PR*2*OUR INSURANCE COMPANY*****PI*OIC02~
//TRN*1*00911232~
//DTP*050*D8*20060320~
//DTP*009*D8*20060321~
//HL*2*1*21*1~
//NM1*41*1*KING*EWELL*B**MD*46*S00005~
//TRN*2*200203207890~
//STC*A1:19:PR*20060321*WQ*455~
//QTY*90*3~
//QTY*AA*5~
//AMT*YU*155~
//AMT*YY*300~
//HL*3*2*19*1~
//NM1*85*1*KING*EWELL*B**MD*XX*5365432101~
//TRN*2*00098765432~
//STC*A1:19:PR**WQ*305~
//HL*4*3*PT~
//NM1*QC*1*PATIENT*FEMALE****MI*2222222222~
//TRN*2*PATIENT22222~
//STC*A2:20:PR*20060321*WQ*55~
//REF*1K*220216359803X~
//DTP*472*D8*20060314~
//HL*5*3*PT~
//NM1*QC*1*PATIENT*MALE****MI*3333333333~
//TRN*2*PATIENT33333~
//STC*A3:187:PR*20060321*U*50~
//HL*6*3*PT~
//NM1*QC*1*JONES*MARY****MI*4444444444~
//TRN*2*JONES44444~
//STC*A3:116*20060321*U*100~
//DTP*472*D8*20060311~
//HL*7*3*PT~
//NM1*QC*1*JOHNSON*JIMMY****MI*5555555555~
//TRN*2*JOHNSON55555~
//STC*A2:20:PR*20060321*WQ*50~
//REF*1K*220216359806X~
//DTP*472*D8*20060310~
//HL*8*3*PT~
//NM1*QC*1*MILLS*HALEY****MI*6666666666~
//TRN*2*MILLS66666~
//STC*A2:20:PR*20060321*WQ*50~
//REF*1K*200216359807X~
//DTP*472*D8*20060305~
//HL*9*2*19*0~
//NM1*85*1*REED*I*B**MD*FI*567012345~
//TRN*2*00023456789~
//STC*A3:24:85*20060321*U*150~
//QTY*QC*3~
//AMT*YY*150~
//SE*53*0004~
//GE*1*17~
//IEA*1*000000017~