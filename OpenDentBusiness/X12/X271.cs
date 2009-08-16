using System;
using System.Collections.Generic;

namespace OpenDentBusiness
{
	///<summary></summary>
	public class X271:X12object{

		public X271(string messageText):base(messageText){
		
		}

		///<summary>In realtime mode, X12 limits the request to one patient.  We will always use the subscriber.  So all EB segments are for the subscriber.</summary>
		public List<EB271> GetListEB() {
			List<EB271> retVal=new List<EB271>();
			EB271 eb;
			for(int i=0;i<Segments.Count;i++) {
				if(Segments[i].SegmentID != "EB") {
					continue;
				}
				eb=new EB271(Segments[i]);
				retVal.Add(eb);
			}
			return retVal;
		}

		///<summary>Only the DTP segments that come before the EB segments.  X12 loop 2100C.</summary>
		public List<DTP271> GetListDtpSubscriber() {
			List<DTP271> retVal=new List<DTP271>();
			DTP271 dtp;
			for(int i=0;i<Segments.Count;i++) {
				if(Segments[i].SegmentID!="DTP") {
					continue;
				}
				if(Segments[i].SegmentID=="EB") {
					break;
				}
				dtp=new DTP271(Segments[i]);
				retVal.Add(dtp);
			}
			return retVal;
		}

		///<summary>If there was no processing error (AAA segment), then this will return empty string.</summary>
		public string GetProcessingError() {
			string retVal="";
			for(int i=0;i<Segments.Count;i++) {
				if(Segments[i].SegmentID!="AAA") {
					continue;
				}
				if(retVal != "") {//if multiple errors
					retVal+=", ";
				}
				retVal+=GetRejectReason(Segments[i].Get(3))+", "
					+GetFollowupAction(Segments[i].Get(4));
			}
			return retVal;
		}

		private string GetRejectReason(string code) {
			switch(code) {
				case "15": return "Required application data missing"; 
				case "42": return "Unable to Respond at Current Time"; 
				case "43": return "Invalid/Missing Provider Identification"; 
				case "45": return "Invalid/Missing Provider Specialty"; 
				case "47": return "Invalid/Missing Provider State"; 
				case "48": return "Invalid/Missing Referring Provider Identification Number"; 
				case "49": return "Provider is Not Primary Care Physician"; 
				case "51": return "Provider Not on File"; 
				case "52": return "Service Dates Not Within Provider Plan Enrollment"; 
				case "56": return "Inappropriate Date"; 
				case "57": return "Invalid/Missing Date(s) of Service"; 
				case "58": return "Invalid/Missing Date-of-Birth"; 
				case "60": return "Date of Birth Follows Date(s) of Service"; 
				case "61": return "Date of Death Precedes Date(s) of Service"; 
				case "62": return "Date of Service Not Within Allowable Inquiry Period"; 
				case "63": return "Date of Service in Future"; 
				case "64": return "Invalid/Missing Patient ID"; 
				case "65": return "Invalid/Missing Patient Name"; 
				case "66": return "Invalid/Missing Patient Gender Code"; 
				case "67": return "Patient Not Found"; 
				case "68": return "Duplicate Patient ID Number"; 
				case "71": return "Patient Birth Date Does Not Match That for the Patient on the Database"; 
				case "72": return "Invalid/Missing Subscriber/Insured ID"; 
				case "73": return "Invalid/Missing Subscriber/Insured Name"; 
				case "74": return "Invalid/Missing Subscriber/Insured Gender Code"; 
				case "75": return "Subscriber/Insured Not Found"; 
				case "76": return "Duplicate Subscriber/Insured ID Number"; 
				case "77": return "Subscriber Found, Patient Not Found"; 
				case "78": return "Subscriber/Insured Not in Group/Plan Identified"; 
				default: return "Error code '"+code+"' not valid.";
			} 
		}

		private string GetFollowupAction(string code) {
			switch(code) {
				case "C": return "Please Correct and Resubmit";
				case "N": return "Resubmission Not Allowed";
				case "R": return "Resubmission Allowed";
				case "S": return "Do Not Resubmit; Inquiry Initiated to a Third Party";
				case "W": return "Please Wait 30 Days and Resubmit";
				case "X": return "Please Wait 10 Days and Resubmit";
				case "Y": return "Do Not Resubmit; We Will Hold Your Request and Respond Again Shortly";
				default: return "Error code '"+code+"' not valid.";
			}
		}

	}
}
