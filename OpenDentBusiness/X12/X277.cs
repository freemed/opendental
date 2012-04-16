using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace OpenDentBusiness
{
	///<summary>X12 277 Unsolicited Claim Status Notification. There is only one type of 277, but a 277 can be sent out unsolicited (without sending a request) or as a response to a 276 request.</summary>
	public class X277:X12object{

		private List<X12Segment> segments;
		///<summary>NM1 of loop 2100A.</summary>
		private int segNumInfoSourceNM101;
		///<summary>NM1 of loop 2100B.</summary>
		private int segNumInfoReceiverNM101;
		///<summary>NM1 of loop 2100C.</summary>
		private List<int> segNumsBillingProviderNM101;
		///<summary>NM1 of loop 2100D.</summary>
		private List<int> segNumsPatientDetailNM101;

		public static bool Is277(X12object xobj) {
			if(xobj.FunctGroups.Count!=1) {
				return false;
			}
			if(xobj.FunctGroups[0].Header.Get(1)=="HN") {
				return true;
			}
			return false;
		}

		public X277(string messageText):base(messageText) {
			segments=FunctGroups[0].Transactions[0].Segments;
			segNumInfoSourceNM101=-1;
			segNumInfoReceiverNM101=-1;
			segNumsBillingProviderNM101=new List<int>();
			segNumsPatientDetailNM101=new List<int>();
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
						segNumsBillingProviderNM101.Add(i);
					}
					else if(entityIdentifierCode=="QC") {
						segNumsPatientDetailNM101.Add(i);
					}
				}
			}
		}

		///<summary>NM101 of loop 2100A.</summary>
		public string GetInformationSourceType() {
			if(segNumInfoSourceNM101!=-1) {
				return segments[segNumInfoSourceNM101].Get(1);
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

		///<summary>Returns -1 on error.</summary>
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
		public long GetAcceptedQuantity() {
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
		public long GetRejectedQuantity() {
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
		public double GetAcceptedAmount() {
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
						return long.Parse(seg.Get(2));
					}
				}
			}
			return 0;
		}

		///<summary>AMT02 of loop 2200B.</summary>
		public double GetRejectedAmount() {
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
							return long.Parse(seg.Get(2));
						}
						else {
							segNum++;
							if(segNum<segments.Count) {
								seg=segments[segNum];
								if(seg.SegmentID=="AMT" && seg.Get(1)=="YY") {
									return long.Parse(seg.Get(2));
								}
							}
						}
					}
				}
			}
			return 0;
		}

		///<summary>Do this first to get a list of all claim tracking numbers that are contained within this 277.  Then, for each claim tracking number, we can later retrieve the AckCode for that single claim. The claim tracking numbers correspond to CLM01 exactly as submitted in the 837. We allow more than just digits in our tracking numbers so we must return a list of strings.</summary>
		public List<string> GetClaimTrackingNumbers() {
			List<string> retVal=new List<string>();
			for(int i=0;i<segNumsPatientDetailNM101.Count;i++) {
				//The specification says that there could be more than one tracking number per claim, but we only use the first one for each claim.
				X12Segment seg=segments[segNumsPatientDetailNM101[i]+1];//TRN segment.
				retVal.Add(seg.Get(2));
			}
			return retVal;
		}

		///<summary>Use after GetClaimTrackingNumbers(). Will return A=Accepted, R=Rejected, or "" if can't determine.</summary>
		public string GetAckForTrans(string trackingNumber) {
			for(int i=0;i<segNumsPatientDetailNM101.Count;i++) {
				//The specification says that there could be more than one tracking number per claim, but we only use the first one for each claim.
				X12Segment seg=segments[segNumsPatientDetailNM101[i]+1];//TRN segment.
				if(seg.Get(2)==trackingNumber) { //TRN02
					//The X12 specification says that there can be more than one STC segment. I'm not sure why there would ever be more than one, but we will simply use the first one for now.
					int segNum=segNumsPatientDetailNM101[i]+2;
					seg=segments[segNum];
					while(seg.SegmentID=="TRN") { //Skip any additional TRN segments to locate the first STC segment.
						segNum++;
						seg=segments[segNum];
					}
					if(seg.Get(3)=="WQ") { //STC03 = WQ
						return "A";//accepted
					}
					else { //STC03 = U
						return "R";//rejected
					}
				}
			}
			return "";//cannot determine
		}

		public string GetHumanReadable() {
			return "";
		}

	}
}



















