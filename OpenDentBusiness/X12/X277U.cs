using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace OpenDentBusiness
{
	///<summary>X12 277 Unsolicited Claim Status Notification. There is only one type of 277, but a 277 can be sent out unsolicited (without sending a request) or as a response to a 276 request.</summary>
	public class X277U:X12object{

		public X277U(string messageText):base(messageText) {
		}

		public static bool Is277U(X12object xobj) {
			if(xobj.FunctGroups.Count!=1) {//Assume only one funct group allowed.  Is this too strict?
				return false;
			}
			if(xobj.FunctGroups[0].Header.Get(1)=="HN") {
				return true;
			}
			return false;
		}

		///<summary>In X12 lingo, the batchNumber is known as the functional group.</summary>
		public int GetBatchNumber() {
			if(FunctGroups.Count!=1) {
				return 0;
			}
			string batchNumStr=FunctGroups[0].Header.Get(6);
			try {
				return PIn.Int(batchNumStr);
			}
			catch {
				return 0;
			}
		}

		///<summary>Do this first to get a list of all trans nums that are contained within this 277.  Then, for each trans num, we can later retrieve the AckCode for that single trans num. The transaction numbers correspond to BHT03 from the 837.</summary>
		public List<int> GetTransNums() {
			List<int> retVal=new List<int>();
			X12Segment seg;
			int transNum=0;
			for(int i=0;i<FunctGroups[0].Transactions[0].Segments.Count;i++) {
				seg=FunctGroups[0].Transactions[0].Segments[i];
				//Only one type of NM1 within the entire 277 has an Entity Identifier Code in NM101 of value 41=Submitter. The transaction numbers we are looking for are in the TRN segments right below these NM1 segments. Each NM1 segment with value 41 in NM101 is guaranteed to have exactly one TRN segment following immediately afterwards.
				if(seg.SegmentID=="NM1" && seg.Get(1)=="41") {
					i++;
					seg=FunctGroups[0].Transactions[0].Segments[i];//a TRN segment.
					transNum=0;
					try {
						transNum=PIn.Int(seg.Get(2));
					}
					catch {
						transNum=0;
					}
					if(transNum!=0) {
						retVal.Add(transNum);
					}
				}
			}
			return retVal;
		}

		///<summary>Use after GetTransNums.  Will return A=Accepted, R=Rejected, or "" if can't determine.</summary>
		public string GetAckForTrans(int transNum) {
			for(int i=0;i<FunctGroups[0].Transactions[0].Segments.Count;i++) {
				X12Segment seg=FunctGroups[0].Transactions[0].Segments[i];
				//Only one type of NM1 within the entire 277 has an Entity Identifier Code in NM101 of value 41=Submitter. The transaction numbers we are looking for are in the TRN segments right below these NM1 segments. Each NM1 segment with value 41 in NM101 is guaranteed to have exactly one TRN segment following immediately afterwards.
				if(seg.SegmentID=="NM1" && seg.Get(1)=="41") {
					i++;
					seg=FunctGroups[0].Transactions[0].Segments[i];//a TRN segment.
					int thisTransNum=0;
					try {
						thisTransNum=PIn.Int(seg.Get(2));
					}
					catch {
						thisTransNum=0;
					}
					if(thisTransNum==transNum) {
						//TODO:
					}
				}
			}
			return "";
		}

		///<summary>Will return "" if unable to determine.  But would normally return A=Accepted or R=Rejected or P=Partially accepted if only some of the transactions were accepted.</summary>
		public string GetBatchAckCode() {
			if(this.FunctGroups[0].Transactions.Count!=1) {
				return "";
			}
			int totalClaimsAccepted=0;
			int totalClaimsRejected=0;
			for(int i=0;i<FunctGroups[0].Transactions[0].Segments.Count;i++) {
				X12Segment seg=FunctGroups[0].Transactions[0].Segments[i];
				if(seg.SegmentID!="QTY") {
					continue;
				}
				string segQualifier=seg.Get(1);
				if(segQualifier=="90") {
					totalClaimsAccepted=PIn.Int(seg.Get(2));
				}
				else if(segQualifier=="AA") {
					totalClaimsRejected=PIn.Int(seg.Get(2));
				}
			}
			if(totalClaimsAccepted>0 && totalClaimsRejected>0) {
				return "P";
			}
			if(totalClaimsAccepted>0) {
				return "A";
			}
			if(totalClaimsRejected>0) {
				return "R";
			}
			return "";
		}

		///<summary>This can take a 277 file of any length and convert it to a human readable format.  It can then be saved to a text file in the same folder as the original. The original file should be immediately archived if this is successful.  Actually includes extra functionality to make an 837 readable.</summary>
		public string GetHumanReadable() {
			//List<string> messageLines=new List<string>();
			//X12object xObj=new X12object(File.ReadAllText(fileName));
			string rn="\r\n";
			StringBuilder ret=new StringBuilder();
			foreach(X12FunctionalGroup functGroup in FunctGroups){
				ret.Append("Functional Group");
				if(functGroup.Header.Get(1)=="HN"){
					ret.Append(" 277-Claim Status Notification");
				}
				ret.Append(rn);	
				foreach(X12Transaction trans in functGroup.Transactions){
					ret.Append("Transaction Set 277-Claim Status Notification"+rn);
					foreach(X12Segment segment in trans.Segments){
						ret.Append(SegmentToString(segment));
					}
				}
			}
			return ret.ToString();
		}

		private static string SegmentToString(X12Segment segment){
			string retVal="";
			string rn="\r\n";
			//HL
			if(segment.SegmentID=="HL"){
				retVal+=rn;//insert a blank line before each loop
				switch(segment.Get(3)){
					case "20":
						retVal+="Information Source:";
						break;
					case "21":
						retVal+="Information Receiver:";
						break;
					case "19":
						retVal+="Service Provider:";
						break;
					case "22":
						retVal+="Subscriber:";
						break;
					case "23":
						retVal+="Dependent:";
						break;
				}
				retVal+=rn;
			}
			//NM1
			else if(segment.SegmentID=="NM1"){
				switch(segment.Get(1)){//payer ID code
					case "PR":
						retVal="Payer: ";
						break;
					case "41":
						retVal="Submitter: ";
						break;
					case "1P":
						retVal="Provider: ";
						break;
					case "IL":
						retVal="Insured or Subscriber: ";
						break;
					case "QC":
						retVal="Patient: ";
						break;
					case "85":
						retVal="Billing Provider: ";
						break;
				}
				retVal+=segment.Get(3)+", "//LName
					+segment.Get(4)+" "//Fname
					+segment.Get(5)+" "//MiddleName
					+segment.Get(7)+", "//Suffix
					+"ID code: "+segment.Get(9)+rn;//ID code
			}
			//PER
			else if(segment.SegmentID=="PER"){
				retVal="Information Contact: "
					+segment.Get(2)+" "//Name
					+GetPERqualifier(segment.Get(3))+" "
					+segment.Get(4)+" "
					+GetPERqualifier(segment.Get(5))+" "
					+segment.Get(6)+" "
					+GetPERqualifier(segment.Get(7))+" "
					+segment.Get(8)+rn;
			}
			//TRN
			else if(segment.SegmentID=="TRN"){
				retVal="Trace Number: "
					+segment.Get(2)+rn;
			}
			//STC-Status information
			else if(segment.SegmentID=="STC"){
				retVal="ClaimStatus :"
					//+segment.Get(1,1)+" "//Industry R code
					+"LOINC:"+segment.Get(1,2)+", "//LOINC code
					+"DATE: "+ConvertDate(segment.Get(2))+" "
					+ConvertCurrency(segment.Get(4))+" "//monetary amount
					+segment.Get(10,2)+" "//LOINC code 2
					+segment.Get(11,2)+rn;//LOINC code 3
			}
			//REF
			else if(segment.SegmentID=="REF"){
				switch(segment.Get(1)){
					case "EJ":
						retVal="Patient Account Number: ";
						break;
					case "BLT":
						retVal="Billing Type: ";
						break;
					case "EA":
						retVal="Medical Record ID Number: ";
						break;
					case "D9":
						retVal="Claim Number: ";
						break;
					case "FJ":
						retVal="Line Item Control Number: ";
						break;
					case "87":
						//functional category irrelevant
						return "";
					case "9F":
						retVal="Referral Number: ";
						break;
					case "6R":
						retVal="Provider Control Number: ";
						break;
					case "G5":
						retVal="Provider Site Number: ";
						break;
					case "1B":
						retVal="Blue Shield Provider Number: ";
						break;
				}
				retVal+=segment.Get(2)+rn;
			}
			//DTP(x2)-Date or Time period
			else if(segment.SegmentID=="DTP"){
				switch(segment.Get(1)){
					case "434":
						retVal="Statement date: ";
						break;
					case "106":
						retVal="Required by: ";
						break;	
					case "472":
						retVal="Service date: ";
						break;	
				}
				if(segment.Get(2)=="D8"){//Date eight char
					retVal+=ConvertDate(segment.Get(3))+rn;
				}
				else if(segment.Get(2)=="RD8"){//Range Date eight char
					retVal+=ConvertDateRange(segment.Get(3))+rn;
				}
			}
			//PWK-not very useful here
			//N1
			else if(segment.SegmentID=="N1"){
				switch(segment.Get(1)){//
					case "PR":
						retVal="Payer: ";
						break;
				}
				retVal+=segment.Get(2)+rn;
			}
			//N3
			else if(segment.SegmentID=="N3"){
				retVal=segment.Get(1)+" "//address
					+segment.Get(2)+rn;//address2
			}
			//N4-CityStateZip
			else if(segment.SegmentID=="N4"){
				retVal=segment.Get(1)+", "//City
					+segment.Get(2)+"  "//State
					+segment.Get(3)+" ";//Zip
				if(segment.Get(5)=="B1"){//branch
					retVal+="Branch "+segment.Get(6);
				}
				else if(segment.Get(5)=="DP"){//department
					retVal+="Department "+segment.Get(6);
				}
				retVal+=rn;
			}
			//SVC-Service Line Information
			else if(segment.SegmentID=="SVC"){
				retVal=segment.Get(1,2)+" "//procedure code
					+ConvertCurrency(segment.Get(2))+" "//amount
					+segment.Get(4)+rn;//national Uniform Billing Code??? on institutional claims.
			}
			//SBR-subscriber on 837
			//DMG-birthdate,gender on 837
			//CLM-claim on 837
			//DN2-tooth status on 837
			//LX-line counter not needed
			//SV3-Dental service on 837
			//TOO-Tooth info on 837
			return retVal;
		}

		private static string GetPERqualifier(string type){
			switch(type){
				case "ED":
					return "Electronic Data Number ";
				case "EM":
					return "E-mail ";
				case "EX":
					return "Telephone Extension ";
				case "FX":
					return "Fax ";
				case "TE":
					return "Telephone ";
				default:
					return "";
			}
		}

		private static string ConvertDate(string aDate){
			if(aDate.Length!=8){
				return "";
			}
			try{
				DateTime aValue=new DateTime(Convert.ToInt32(aDate.Substring(0,4))
					,Convert.ToInt32(aDate.Substring(4,2))
					,Convert.ToInt32(aDate.Substring(6,2)));
				return aValue.ToShortDateString();
			}
			catch{
				return "";
			}
		}

		private static string ConvertDateRange(string aRange){
			if(aRange.Length!=17){
				return "";
			}
			return ConvertDate(aRange.Substring(0,8))+"-"
				+ConvertDate(aRange.Substring(9,8));
		}

		private static string ConvertCurrency(string amount){
			if(amount==""){
				return "";
			}
			try{
				double dAmount=Double.Parse(amount);
				return dAmount.ToString("c");
			}
			catch{
				return "";
			}
		}



		
	}
}



















