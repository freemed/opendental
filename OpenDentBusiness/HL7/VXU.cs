using System;
using System.Collections.Generic;
using System.Text;
using OpenDentBusiness;

namespace OpenDentBusiness.HL7 {
	///<summary>A VXU message is an Unsolicited Vaccination Record Update.  It is a message sent out from Open Dental detailing vaccinations that were given.</summary>
	public class VXU {
		private MessageHL7 msg;
		private SegmentHL7 seg;

		///<summary></summary>
		public VXU() {
			
		}
		
		///<summary>Creates the Message object and fills it with data.  Vaccines must all be for the same patient.</summary>
		public void Initialize(Patient pat,List<VaccinePat> vaccines){
			if(vaccines.Count==0) {
				throw new ApplicationException("Must be at least one vaccine.");
			}
			msg=new MessageHL7(MessageType.VXU);
			MSH();
			PID(pat);
			for(int i=0;i<vaccines.Count;i++) {
				ORC();
				RXA(vaccines[i]);
			}
		}

		//The 2 examples below have been edited slightly for our purposes.  They still pass validation.

		//example 1:
		/*
MSH|^~\&|Open Dental||||20110316102457||VXU^V04^VXU_V04|OD-110316102457117|P|2.5.1
PID|||9817566735^^^MPI&2.16.840.1.113883.19.3.2.1&ISO^MR||Johnson^Philip||20070526|M||2106-3^White^HL70005|3345 Elm Street^^Aurora^CO^80011^^M||^PRN^^^^303^5548889|||||||||N^Not Hispanic or Latino^HL70189
ORC|RE
RXA|0|1|201004051600|201004051600|33^Pneumococcal Polysaccharide^CVX|0.5|ml^milliliter^ISO+||||||||1039A||MSD^Merck^HL70227||||A
		 */

		//example7 has two vaccines:
		/*
MSH|^~\&|EHR Application|EHR Facility|PH Application|PH Facility|20110316102838||VXU^V04^VXU_V04|NIST-110316102838387|P|2.5.1
PID|||787478017^^^MPI&2.16.840.1.113883.19.3.2.1&ISO^MR||James^Wanda||19810430|F||2106-3^White^HL70005|574 Wilkins Road^^Shawville^Pennsylvania^16873^^M||^PRN^^^^814^5752819|||||||||N^Not Hispanic or Latino^HL70189
ORC|RE
RXA|0|1|201004051600|201004051600|52^Hepatitis A^HL70292|1|ml^milliliter^ISO+||||||||HAB9678V1||SKB^GLAXOSMITHKLINE^HL70227||||A
ORC|RE
RXA|0|1|201007011330|201007011330|03^Measles Mumps Rubella^HL70292|999|||||||||||||||A
		 */
		
		///<summary>Message Header Segment</summary>
		private void MSH(){
			seg=new SegmentHL7(@"MSH|^~\&|Open Dental|"//MSH-3: Sending application
				+"|||"+DateTime.Now.ToString("yyyyMMddHHmmss")+"||"
				+"VXU^V04^VXU_V04|"//MSH-9: fixed
				+"OD-110316102457117|"//MSH-10: Control Id, str20. Fixed is ok for testing.
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

		private void ORC() {
			seg=new SegmentHL7(SegmentName.ORC);
			seg.SetField(1,"RE");//fixed
			msg.Segments.Add(seg);
		}

		private void RXA(VaccinePat vaccine) {
			seg=new SegmentHL7(SegmentName.RXA);
			seg.SetField(1,"0");//fixed
			seg.SetField(2,"1");//fixed
			seg.SetField(3,vaccine.DateTimeStart.ToString("yyyyMMddHHmm"));
			seg.SetField(4,vaccine.DateTimeEnd.ToString("yyyyMMddHHmm"));
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

		private string ConvertRace(PatientRaceOld race) {
			switch(race) {
				case PatientRaceOld.AmericanIndian:
					return "1002-5^American Indian Or Alaska Native^HL70005";
				case PatientRaceOld.Asian:
					return "2028-9^Asian^HL70005";
				case PatientRaceOld.AfricanAmerican:
					return "2054-5^Black or African American^HL70005";
				case PatientRaceOld.HawaiiOrPacIsland:
					return "2076-8^Native Hawaiian or Other Pacific Islander^HL70005";
				case PatientRaceOld.White:
					return "2106-3^White^HL70005";
				case PatientRaceOld.Other:
					return "2131-1^Other Race^HL70005";
				default://including hispanic
					return "2131-1^Other Race^HL70005";
			}
		}

		private string ConvertEthnicGroup(PatientRaceOld race) {
			switch(race) {
				case PatientRaceOld.HispanicLatino:
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
