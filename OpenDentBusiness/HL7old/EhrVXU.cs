using System;
using System.Collections.Generic;
using System.Text;
using OpenDentBusiness;

namespace OpenDentBusiness.HL7 {
	///<summary>A VXU message is an Unsolicited Vaccination Record Update.  It is a message sent out from Open Dental detailing vaccinations that were given.
	///Implementation based on HL7 version 2.5.1 Immunization Messaging Release 1.4 08/01/2012.  Data types defined on page 52.
	///To view specific HL7 table definitions, see http://hl7.org/implement/standards/fhir/terminologies-v2.html. </summary>
	public class EhrVXU {

		private MessageHL7 _msg;
		private SegmentHL7 _seg;

		///<summary></summary>
		public EhrVXU() {
			
		}
		
		///<summary>Creates the Message object and fills it with data.  Vaccines must all be for the same patient.</summary>
		public void Initialize(Patient pat,List<VaccinePat> vaccines){
			if(vaccines.Count==0) {
				throw new ApplicationException("Must be at least one vaccine.");
			}
			_msg=new MessageHL7(MessageTypeHL7.VXU);//Message format for VXU is on guide page 160.
			MSH();//MSH segment. Required.  Cardinality [1..1].
			//SFT segment. Optional. Cardinality [0..*].  Undefined and may be locally specified.
			PID(pat);//PID segment.  Required.  Cardinality [1..1].
			PD1();//PD1 segment.  Required if known.  Cardinality [0..1].
			NK1();//NK1 segment.  Required if known.  Cardinality [0..1].
			//PV1 segment.  Optional.  Cardinality [0..1].  Undefined and may be locally specified.
			//PV2 segment.  Optional.  Cardinality [0..1].  Undefined and may be locally specified.
			//GT1 segment.  Optional.  Cardinality [0..*].  Undefined and may be locally specified.
			//Begin Insurance group.  Optional.  Cardinality [0..*].
			//IN1 segment.  Optional.  Cardinality [0..1].  Undefined and may be locally specified.  Guide page 102.
			//IN2 segment.  Optional.  Cardinality [0..1].  Undefined and may be locally specified.  Guide page 102.
			//IN3 segment.  Optional.  Cardinality [0..1].  Undefined and may be locally specified.  Guide page 102.
			//End Insurance group.
			//Begin Order group.
			for(int i=0;i<vaccines.Count;i++) {
				ORC();//ORC segment.  Required.  Cardinality [1..1].
				//TQ1 segment.  Optional.  Cardinality [0..1]. Undefined and may be locally specified.
				//TQ2 segment.  Optional.  Cardinality [0..1]. Undefined and may be locally specified.
				RXA(vaccines[i]);//RXA segment.  Required.  Cardinality [1..1].
				RXR();//RXR segment.  Required if known.  Cardinality [0..1].
				OBX();//OBX segment.  Required if known.  Cardinality [0..*].
				NTE();//NTE segment.  Required if known.  Cardinality [0..1].
			}
			//End Order group.
		}

		///<summary>Event Type segment.  Guide page 99.</summary>
		private void EVN() {
		}
		
		///<summary>Message Segment Header segment.  Defines intent, source, destination and syntax of the message.  Guide page 104.</summary>
		private void MSH() {
			_seg=new SegmentHL7("MSH"
				+"|"//MSH-1 Field Separator (|).  Required (length 1..1).
				+@"^~\&"//MSH-2 Encoding Characters.  Required (length 4..4).  Component separator (^), then field repetition separator (~), then escape character (\), then Sub-component separator (&).
				+"|Open Dental"//MSH-3 Sending Application.  Required if known (length unspecified).  Value set HL70361.
				+"|"//MSH-4 Sending Facility.  Required if known (length unspecified).  Value set HL70362.
				+"|"//MSH-5 Receiving Application.  Required if known (length unspecified).  Value set HL70361.
				+"|"//MSH-6 Receiving Facility.  Required if known (length unspecified).  Value set HL70362.
				+"|"+DateTime.Now.ToString("yyyyMMddHHmmss")//MSH-7 Date/Time of Message.  Required (length 12..19).
				+"|"//MSH-8 Security.  Optional (length unspecified).
				+"|VXU^VO4^VXU_V04"//MSH-9 Message Type. Required (length unspecified).
				+"|OD-"+DateTime.Now.ToString("yyyyMMddHHmmss")+"-"+CodeBase.MiscUtils.CreateRandomAlphaNumericString(14)//MSH-10 Message Control ID.  Required (length 1..199).  Our IDs are 32 characters.
				+"|P"//MSH-11 Processing ID.  Required (length unspecified).  P=production.
				+"|2.5.1"//MSH-12 Version ID.  Required (length unspecified).  Must be exactly "2.5.1".
				+"|"//MSH-13 Sequence Number.  Optional (length unspecified).
				+"|"//MSH-14 Continuation Pointer.  Optional (length unspecified).
				+"|NE"//MSH-15 Accept Acknowledgement Type.  Required if known (length unspecified).  Value set HL70155 (AL=Always, ER=Error/rejection conditions only, NE=Never, SU=Successful completion only).
				+"|NE"//MSH-16 Application Acknowledgement Type.  Required if known (length unspecified).  Value set HL70155 (AL=Always, ER=Error/rejection conditions only, NE=Never, SU=Successful completion only).
				//MSH-17 Country Code.  Optional (length unspecified).  Value set HL70399.  Default value is USA.
				//MSH-18 Character Set.  Optional (length unspecified).
				//MSH-19 Principal Language Of Message.  Optional (length unspecified).
				//MSH-20 Alternate Character Set Handling Scheme.  Optional (length unspecified).
				//MSH-21 Message Profile Identifier.  Required if MSH9 component 1 is set to "QBP" or "RSP.  In our case, this field is not required.
			);
			_msg.Segments.Add(_seg);
		}

		///<summary>Next of Kin segment.  Guide page 111.</summary>
		private void NK1() {
		}

		///<summary>Note segment.  Guide page 116.</summary>
		private void NTE() {
		}

		///<summary>Observation Result segment.  The basic format is question and answer.  Guide page 116.</summary>
		private void OBX() {
		}
		
		///<summary>Order Request segment.  Optional and rarely included.  Guide page 126.</summary>
		private void ORC() {
			_seg=new SegmentHL7(SegmentNameHL7.ORC);
			_seg.SetField(0,"ORC");
			_seg.SetField(1,"RE");//fixed
			_msg.Segments.Add(_seg);
		}

		///<summary>Patient Demographic segment.  Additional demographics.  Guide page 132.</summary>
		private void PD1() {
		}

		///<summary>Patient Identifier segment.  Guide page 137.</summary>
		private void PID(Patient pat){
			_seg=new SegmentHL7(SegmentNameHL7.PID);
			_seg.SetField(0,"PID");
			//PID-1 Set ID - PID.  Required if known.  Cardinality [0..1].
			//PID-2 Patient ID.  No longer used.
			_seg.SetField(3,//PID-3 Patient Identifier List.  Required.  Cardinality [1..*].  Type CX (see guide page 58 for type definition).
				pat.PatNum.ToString(),//PID-3.1 ID Number.  Required (length 1..15).  
				"",//PID-3.2 Check Digit.  Optional (length 1..1).
				"",//PID-3.3 Check Digit Scheme.  Required if PID-3.2 is specified.  Not required for our purposes.  Value set HL70061.
				"Open Dental",//PID-3.4 Assigning Authority.  Required.  Value set HL70363.
				"MR"//PID-3.5 Identifier Type Code.  Required (length 2..5).  Value set HL70203.  MR=medical record number.
				//PID-3.6 Assigning Facility.  Optional (length undefined).
				//PID-3.7 Effective Date.  Optional (length 4..8).
				//PID-3.8 Expiration Date.  Optional (length 4..8).
				//PID-3.9 Assigning Jurisdiction.  Optional (length undefined).
				//PID-3.10 Assigning Agency or Department.  Optional (length undefined).
			);
			//PID-4 Alternate Patient ID - 00106.  No longer used.
			_seg.SetField(5,pat.LName,pat.FName);//PID-5 Patient Name.  Required (length unspecified).  Cardinality [1..*].  The first repetition must contain the legal name.
			//PID-6 Mother's Maiden Name.  Required if known (length unspecified).  Cardinality [0..1].  TODO: This will be required for us to pass EHR testing.
			//PID-7 Date/Time of Birth.  Required.  Cardinality [1..1].  We must specify "UNK" if unknown.
			if(pat.Birthdate.Year<1880) {
				_seg.SetField(7,"UNK");
			}
			else {
				_seg.SetField(7,pat.Birthdate.ToString("yyyyMMdd"));
			}
			_seg.SetField(8,ConvertGender(pat.Gender));//PID-8 Administrative Sex.  Required if known.  Cardinality [0..1].  Value set HL70001.
			//PID-9 Patient Alias.  No longer used.
			//PID-10 Race.  Required if known.  Cardinality [0..*].  Value set HL70005 (guide page 194).  Each race definition must be type CE.  Type CE is defined on guide page 53.
			List<PatientRace> listPatientRaces=PatientRaces.GetForPatient(pat.PatNum);
			List<PatRace> listPatRacesFiltered=new List<PatRace>();
			bool isHispanicOrLatino=false;
			for(int i=0;i<listPatientRaces.Count;i++) {
				PatRace patRace=listPatientRaces[i].Race;
				if(patRace==PatRace.Hispanic) {
					isHispanicOrLatino=true;
				}
				else if(patRace==PatRace.NotHispanic) {
					//Nothing to do. Flag is set to false by default.
				}
				else if(patRace==PatRace.DeclinedToSpecify) {
					listPatRacesFiltered.Clear();
					break;
				}
				listPatRacesFiltered.Add(patRace);
			}
			string hl7Race="";
			if(listPatRacesFiltered.Count==0) {//No selection or declined to specify.
				hl7Race+=""//PID-10.1 Identifier.  Required (length 1..50).  Blank for unknown.
					+"^Unknown/undetermined"//PID-10.2  Text.  Required if known (length 1..999). Human readable text that is not further used.
					+"^Race and Ethnicity"//PID-10.3 Name of Coding System.  Required (length 1..20).  The full name is actually "Race &amp; Ethnicity - CDC", but it is more than 20 characters.
					//PID-10.4 Alternate Identifier.  Required if known (length 1..50).
					//PID-10.5 Alternate Text.  Required if known (length 1..999).
					//PID-10.6 Name of Alternate Coding system.  Required if PID-10.4 is not blank.
				;
			}
			else {
				for(int i=0;i<listPatRacesFiltered.Count;i++) {
					if(i>0) {
						hl7Race+="~";//field repetition separator
					}
					PatRace patRace=listPatRacesFiltered[i];
					string strRaceCode="";
					string strRaceName="";
					if(patRace==PatRace.AfricanAmerican) {
						strRaceCode="2054-5";
						strRaceName="Black or African American";
					}
					else if(patRace==PatRace.AmericanIndian) {
						strRaceCode="1002-5";
						strRaceName="American Indian or Alaska Native";
					}
					else if(patRace==PatRace.Asian) {
						strRaceCode="2028-9";
						strRaceName="Asian";
					}
					else if(patRace==PatRace.HawaiiOrPacIsland) {
						strRaceCode="2076-8";
						strRaceName="Native Hawaiian or Other Pacific Islander";
					}
					else if(patRace==PatRace.White) {
						strRaceCode="2106-3";
						strRaceName="White";
					}
					else {//Aboriginal, Other, Multiracial
						strRaceCode="2131-1";
						strRaceName="Other Race";
					}
					hl7Race+=strRaceCode//PID-10.1 Identifier.  Required (length 1..50).
						+"^"+strRaceName//PID-10.2  Text.  Required if known (length 1..999). Human readable text that is not further used.
						+"^Race and Ethnicity"//PID-10.3 Name of Coding System.  Required (length 1..20).  The full name is actually "Race &amp; Ethnicity - CDC", but it is more than 20 characters.
						//PID-10.4 Alternate Identifier.  Required if known (length 1..50).
						//PID-10.5 Alternate Text.  Required if known (length 1..999).
						//PID-10.6 Name of Alternate Coding system.  Required if PID-10.4 is not blank.
					;
				}
			}
			_seg.SetField(10,hl7Race);
			_seg.SetField(11,//PID-11 Patient Address.  Required if known (length unspecified).  Cardinality [0..*].  Type XAD (guide page 74).  First repetition must be the primary address.
				pat.Address,//PID-11.1 Street Address.  Required if known (length unspecified).  Data type SAD (guide page 72).  The SAD type only requires the first sub-component.
				pat.Address2,//PID-11.2 Other Designation.  Required if known (length 1..120).
				pat.City,//PID-11.3 City.  Required if known (length 1..50).
				pat.State,//PID-11.4 State or Province.  Required if known (length 1..50).
				pat.Zip,//PID-11.5 Zip or Postal Code.  Required if known (length 1..12).
				"USA",//PID-11.6 Country.  Required if known.  Value set HL70399.  Defaults to USA.
				"M"//PID-11.7 Address Type.  Required (length 1..3).  Value set HL70190 (guide page 202).  M is for mailing.
				//PID-11.8 Other Geographic Designation.  Optional.
				//PID-11.9 County/Parish Code.  Optional.
				//PID-11.10 Census Tract.  Optional.
				//PID-11.11 Address Representation Code.  Optional.
				//PID-11.12 Address Validity Range.  No longer used.
				//PID-11.13 Effective Date.  Optional.
				//PID-11.14 Expiration Date.  Optional.
			);
			//PID-12 County Code.  No longer used.
			_seg.SetField(13,ConvertPhone(pat.HmPhone));//PID-13 Phone Number - Home.  Required if known (length unspecified).  Cardinality [0..*].  Type XTN (guide page 84).
			//PID-14 Phone Number - Business.  Optional.
			//PID-15 Primary Language.  Optional.
			//PID-16 Marital Status.  Optional.
			//PID-17 Religion.  Optional.
			//PID-18 Patient Account Number.  Optional.
			//PID-19 SSN Number - Patient.  No longer used.
			//PID-20 Driver's License Number - Patient.  No longer used.
			//PID-21 Mother's Identifier.  No longer used.
			//PID-22 Ethnic Group.  Required if known (length unspecified).  Cardinality [0..1].  Value set HL70189 (guide page 201).
			if(listPatRacesFiltered.Count==0) {
				_seg.SetField(22,"U");//Unknown
			}
			else if(isHispanicOrLatino) {
				_seg.SetField(22,"H");
			}
			else {
				_seg.SetField(22,"N");//Not hispanic or latino.
			}
			//if(isHispanicOrLatino) {
			//	StartAndEnd("ethnicGroupCode","code","2135-2","displayName","Hispanic or Latino","codeSystem","2.16.840.1.113883.6.238","codeSystemName","Race &amp; Ethnicity - CDC");
			//}
			//else {//Not hispanic
			//	StartAndEnd("ethnicGroupCode","code","2186-5","displayName","Not Hispanic or Latino","codeSystem","2.16.840.1.113883.6.238","codeSystemName","Race &amp; Ethnicity - CDC");
			//}
			_msg.Segments.Add(_seg);
		}

		///<summary>Pharmacy/Treatment Administration segment.  Guide page 149.</summary>
		private void RXA(VaccinePat vaccine) {
			VaccineDef vaccineDef=VaccineDefs.GetOne(vaccine.VaccineDefNum);
			_seg=new SegmentHL7(SegmentNameHL7.RXA);
			_seg.SetField(0,"RXA");
			_seg.SetField(1,"0");//fixed
			_seg.SetField(2,"1");//fixed
			_seg.SetField(3,vaccine.DateTimeStart.ToString("yyyyMMddHHmm"));
			_seg.SetField(4,vaccine.DateTimeEnd.ToString("yyyyMMddHHmm"));
			_seg.SetField(5,vaccineDef.CVXCode,vaccineDef.VaccineName,"HL70292");
			if(vaccine.AdministeredAmt==0){
				_seg.SetField(6,"999");
			}
			else{
				_seg.SetField(6,vaccine.AdministeredAmt.ToString());
			}
			if(vaccine.DrugUnitNum!=0){
				DrugUnit drugUnit=DrugUnits.GetOne(vaccine.DrugUnitNum);
				_seg.SetField(7,drugUnit.UnitIdentifier,drugUnit.UnitText,"ISO+");
			}
			_seg.SetField(15,vaccine.LotNumber);//optional.
			//17-Manufacturer.  Is this really optional?
			if(vaccineDef.DrugManufacturerNum!=0) {//always?
				DrugManufacturer manufacturer=DrugManufacturers.GetOne(vaccineDef.DrugManufacturerNum);
				_seg.SetField(17,manufacturer.ManufacturerCode,manufacturer.ManufacturerName,"HL70227");
			}
			_seg.SetField(21,"A");//21-Action code, A=Add
			_msg.Segments.Add(_seg);
		}

		///<summary>Pharmacy/Treatment Route segment.  Guide page 158.</summary>
		private void RXR() {
		}

		public string GenerateMessage() {
			return _msg.ToString();
		}

		///<summary>For the given patient gender, returns a string corresponding to table HL70001 (guide page 193).</summary>
		private string ConvertGender(PatientGender gender) {
			if(gender==PatientGender.Female) {
				return "F";
			}
			if(gender==PatientGender.Male) {
				return "M";
			}
			return "U";
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
			retVal+="PRN^";//2:table201. PRN=primary residence number.  (Guide page 203)
			retVal+="^^^";//3-5:
			retVal+=digits.Substring(0,3)+"^";//6:area code
			retVal+=digits.Substring(3);
			return retVal;
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

	}
}
