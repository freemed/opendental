using System;
using System.Collections.Generic;
using System.Text;
using OpenDentBusiness;

namespace OpenDentBusiness.HL7 {
	///<summary>An ORU message is an Unsolicited Observation Message.  It is a message sent out from Open Dental containing immunization status in order to satisfy ehr public health requirements.</summary>
	public class ORU {
		private MessageHL7 msg;
		private SegmentHL7 seg;

		///<summary></summary>
		public ORU() {
			
		}
		
		///<summary>Creates the Message object and fills it with data.  Just for one patient.</summary>
		public void Initialize(Patient pat,List<VaccinePat> vaccines){
			if(vaccines.Count==0) {
				throw new ApplicationException("Must be at least one vaccine.");
			}
			msg=new MessageHL7(MessageType.ORU);
			MSH();
			PID(pat);
			//for(int i=0;i<vaccines.Count;i++) {
				OBR();
				OBX(vaccines[0]);
			//}
		}

		///<summary>Message Header Segment</summary>
		private void MSH(){
			seg=new SegmentHL7(@"MSH|^~\&|Open Dental|"//MSH-3: Sending application
				+"|||"+DateTime.Now.ToString("yyyyMMddHHmmss")+"||"
				+"ORU^R01^ORU_R01|"//MSH-9: fixed
				+"OD-110316102457113|"//MSH-10: Control Id, str20. Fixed is ok for testing.
				+"P|"//MSH-11: P=production
				+"2.5.1");
			msg.Segments.Add(seg);
		}

		///<summary>Patient identification.</summary>
		private void PID(Patient pat){
			seg=new SegmentHL7(SegmentName.PID);
			seg.SetField(0,"PID");
			seg.SetField(3,pat.PatNum.ToString());
			seg.SetField(5,pat.LName,pat.FName);
			if(pat.Birthdate.Year>1880) {//7: dob optional
				seg.SetField(7,pat.Birthdate.ToString("yyyyMMdd"));
			}
			seg.SetField(8,ConvertGender(pat.Gender));
			seg.SetField(10,ConvertRace(pat.Race));
			seg.SetField(11,pat.Address,pat.Address2,pat.City,pat.State,pat.Zip,"","M");//M is for mailing.
			seg.SetField(13,ConvertPhone(pat.HmPhone));
			seg.SetField(22,ConvertEthnicGroup(pat.Race));
			msg.Segments.Add(seg);
		}

		private void OBR() {
			seg=new SegmentHL7(SegmentName.OBR);
			//seg.SetField(1,"1");
			//seg.SetField(,"");
			//seg.SetField(,"");
			//seg.SetField(,"");
			//seg.SetField(,"");
			//seg.SetField(,"");




			msg.Segments.Add(seg);
		}

		private void OBX(VaccinePat vaccine) {
			seg=new SegmentHL7(SegmentName.OBX);
			//seg.SetField(1,"0");//fixed
			//seg.SetField(2,"1");//fixed
			//seg.SetField(3,vaccine.DateTimeStart.ToString("yyyyMMddHHmm"));
			//seg.SetField(4,vaccine.DateTimeEnd.ToString("yyyyMMddHHmm"));
			//etc.  (see the patterns above.  Skip fields as necessary)

			msg.Segments.Add(seg);
		}

		public string GenerateMessage() {
			return msg.ToString();
		}

		private string ConvertGender(PatientGender gender) {
			if(gender==PatientGender.Female) {
				return "F";
			}
			if(gender==PatientGender.Male) {
				return "M";
			}
			return "U";
		}

		private string ConvertRace(PatientRace race) {
			switch(race) {
				case PatientRace.AmericanIndian:
					return "1002-5^American Indian Or Alaska Native^HL70005";
				case PatientRace.Asian:
					return "2028-9^Asian^HL70005";
				case PatientRace.AfricanAmerican:
					return "2054-5^Black or African American^HL70005";
				case PatientRace.HawaiiOrPacIsland:
					return "2076-8^Native Hawaiian or Other Pacific Islander^HL70005";
				case PatientRace.White:
					return "2106-3^White^HL70005";
				case PatientRace.Other:
					return "2131-1^Other Race^HL70005";
				default://including hispanic
					return "2131-1^Other Race^HL70005";
			}
		}

		private string ConvertEthnicGroup(PatientRace race) {
			switch(race) {
				case PatientRace.HispanicLatino:
					return "H^Hispanic or Latino^HL70189";
				default:
					return "N^Not Hispanic or Latino^HL70189";
			}
		}

		private string ConvertPhone(string phone) {
			string digits="";
			for(int i=0;i<phone.Length;i++) {
				if(!Char.IsNumber(phone,i)) {
					continue;
				}
				if(digits=="" && phone.Substring(i,1)=="1") {
					continue;//skip leading 1.
				}
				digits+=phone.Substring(i,1);
			}
			if(digits.Length!=10) {
				return "";
			}
			string retVal="";
			retVal+="^";//1:deprecated
			retVal+="PRN^";//2:table201. PRN=primary residence number.  
			retVal+="^^^";//3-5:
			retVal+=digits.Substring(0,3)+"^";//6:area code
			retVal+=digits.Substring(3);
			return retVal;
		}




	}
}
