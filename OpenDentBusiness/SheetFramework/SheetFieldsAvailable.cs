using System;
using System.Collections.Generic;
using System.Text;

namespace OpenDentBusiness{
	public class SheetFieldsAvailable {
		/*public static List<SheetFieldDef> GetListInput(SheetTypeEnum sheetType){
			return GetList(sheetType,OutInCheck.In);
		}

		public static List<SheetFieldDef> GetListOutput(SheetTypeEnum sheetType){
			return GetList(sheetType,OutInCheck.Out);
		}

		public static List<SheetFieldDef> GetListCheckBox(SheetTypeEnum sheetType){
			return GetList(sheetType,OutInCheck.Check);
		}*/

		///<Summary>This is the list of input, output, or checkbox fieldnames for user to pick from.</Summary>
		public static List<SheetFieldDef> GetList(SheetTypeEnum sheetType,OutInCheck outInCheck){
			switch(sheetType){
				case SheetTypeEnum.LabelPatient:
					return GetLabelPatient(outInCheck);
				case SheetTypeEnum.LabelCarrier:
					return GetLabelCarrier(outInCheck);
				case SheetTypeEnum.LabelReferral:
					return GetLabelReferral(outInCheck);
				case SheetTypeEnum.ReferralSlip:
					return GetReferralSlip(outInCheck);
				case SheetTypeEnum.LabelAppointment:
					return GetLabelAppointment(outInCheck);
				case SheetTypeEnum.Rx:
					return GetRx(outInCheck);
				case SheetTypeEnum.Consent:
					return GetConsent(outInCheck);
				case SheetTypeEnum.PatientLetter:
					return GetPatientLetter(outInCheck);
				case SheetTypeEnum.ReferralLetter:
					return GetReferralLetter(outInCheck);
				case SheetTypeEnum.PatientForm:
					return GetPatientForm(outInCheck);
				case SheetTypeEnum.RoutingSlip:
					return GetRoutingSlip(outInCheck);
				case SheetTypeEnum.MedicalHistory:
					return GetMedicalHistory(outInCheck);
				case SheetTypeEnum.LabSlip:
					return GetLabSlip(outInCheck);
				case SheetTypeEnum.ExamSheet:
					return GetExamSheet(outInCheck);
				case SheetTypeEnum.DepositSlip:
					return GetDepositSlip(outInCheck);
			}
			return new List<SheetFieldDef>();
		}

		///<summary>For a given fieldName, return all the allowed radioButtonValues.  Will frequently be an empty list if a checkbox with this fieldname is not allowed to act as a radiobutton.</summary>
		public static List<string> GetRadio(string fieldName) {
			List<string> retVal=new List<string>();
			string[] stringAr=null;
			switch(fieldName) {
				default:
					return retVal;
				case "Gender":
					stringAr=Enum.GetNames(typeof(PatientGender));
					break;
				case "ins1Relat":
				case "ins2Relat":
					stringAr=Enum.GetNames(typeof(Relat));
					break;
				case "Position":
					stringAr=Enum.GetNames(typeof(PatientPosition));
					break;
				case "PreferContactMethod":
				case "PreferConfirmMethod":
				case "PreferRecallMethod":
					stringAr=Enum.GetNames(typeof(ContactMethod));
					break;
				case "StudentStatus":
					retVal.Add("Nonstudent");
					retVal.Add("Parttime");
					retVal.Add("Fulltime");
					return retVal;
			}
			for(int i=0;i<stringAr.Length;i++) {
				retVal.Add(stringAr[i]);
			}
			return retVal;
		}

		private static SheetFieldDef NewOutput(string fieldName) {
			return new SheetFieldDef(SheetFieldType.OutputText,fieldName,"",0,"",false,0,0,0,0,GrowthBehaviorEnum.None,"");
		}

		private static SheetFieldDef NewInput(string fieldName){
			return new SheetFieldDef(SheetFieldType.InputField,fieldName,"",0,"",false,0,0,0,0,GrowthBehaviorEnum.None,"");
		}

		private static SheetFieldDef NewCheck(string fieldName){
			return new SheetFieldDef(SheetFieldType.CheckBox,fieldName,"",0,"",false,0,0,0,0,GrowthBehaviorEnum.None,"");
		}

		private static SheetFieldDef NewRadio(string fieldName,string radioButtonValue){
			return new SheetFieldDef(SheetFieldType.CheckBox,fieldName,"",0,"",false,0,0,0,0,GrowthBehaviorEnum.None,radioButtonValue);
		}

		private static List<SheetFieldDef> GetLabelPatient(OutInCheck outInCheck){
			List<SheetFieldDef> list=new List<SheetFieldDef>();
			if(outInCheck==OutInCheck.Out){
				list.Add(NewOutput("nameFL"));
				list.Add(NewOutput("nameLF"));
				list.Add(NewOutput("address"));//includes address2
				list.Add(NewOutput("cityStateZip"));
				list.Add(NewOutput("ChartNumber"));
				list.Add(NewOutput("PatNum"));
				list.Add(NewOutput("dateTime.Today"));
				list.Add(NewOutput("birthdate"));
				list.Add(NewOutput("priProvName"));
			}
			return list;
		}

		private static List<SheetFieldDef> GetLabelCarrier(OutInCheck outInCheck) {
			List<SheetFieldDef> list=new List<SheetFieldDef>();
			if(outInCheck==OutInCheck.Out){
				list.Add(NewOutput("CarrierName"));
				list.Add(NewOutput("address"));//includes address2
				list.Add(NewOutput("cityStateZip"));
			}
			return list;
		}

		private static List<SheetFieldDef> GetLabelReferral(OutInCheck outInCheck) {
			List<SheetFieldDef> list=new List<SheetFieldDef>();
			if(outInCheck==OutInCheck.Out){
				list.Add(NewOutput("nameFL"));//includes Title
				list.Add(NewOutput("address"));//includes address2
				list.Add(NewOutput("cityStateZip"));
			}
			return list;
		}

		private static List<SheetFieldDef> GetReferralSlip(OutInCheck outInCheck) {
			List<SheetFieldDef> list=new List<SheetFieldDef>();
			if(outInCheck==OutInCheck.Out){
				list.Add(NewOutput("referral.nameFL"));
				list.Add(NewOutput("referral.address"));
				list.Add(NewOutput("referral.cityStateZip"));
				list.Add(NewOutput("referral.phone"));
				list.Add(NewOutput("referral.phone2"));
				list.Add(NewOutput("patient.nameFL"));
				list.Add(NewOutput("dateTime.Today"));
				list.Add(NewOutput("patient.WkPhone"));
				list.Add(NewOutput("patient.HmPhone"));
				list.Add(NewOutput("patient.WirelessPhone"));
				list.Add(NewOutput("patient.address"));
				list.Add(NewOutput("patient.cityStateZip"));
				list.Add(NewOutput("patient.provider"));
			}
			else if(outInCheck==OutInCheck.In){
				list.Add(NewInput("notes"));
			}
			else if(outInCheck==OutInCheck.Check){
				list.Add(NewCheck("misc"));
			}
			return list;
		}

		private static List<SheetFieldDef> GetLabelAppointment(OutInCheck outInCheck){
			List<SheetFieldDef> list=new List<SheetFieldDef>();
			if(outInCheck==OutInCheck.Out){
				list.Add(NewOutput("nameFL"));
				list.Add(NewOutput("nameLF"));
				list.Add(NewOutput("weekdayDateTime"));
				list.Add(NewOutput("length"));
			}
			return list;
		}

		private static List<SheetFieldDef> GetRx(OutInCheck outInCheck){
			List<SheetFieldDef> list=new List<SheetFieldDef>();
			if(outInCheck==OutInCheck.Out){
				list.Add(NewOutput("prov.nameFL"));
				list.Add(NewOutput("prov.address"));
				list.Add(NewOutput("prov.cityStateZip"));
				list.Add(NewOutput("prov.phone"));
				list.Add(NewOutput("RxDate"));
				list.Add(NewOutput("RxDateMonthSpelled"));
				list.Add(NewOutput("prov.dEANum"));
				list.Add(NewOutput("pat.nameFL"));
				list.Add(NewOutput("pat.Birthdate"));
				list.Add(NewOutput("pat.HmPhone"));
				list.Add(NewOutput("pat.address"));
				list.Add(NewOutput("pat.cityStateZip"));
				list.Add(NewOutput("Drug"));
				list.Add(NewOutput("Disp"));
				list.Add(NewOutput("Sig"));
				list.Add(NewOutput("Refills"));			
			}
			else if(outInCheck==OutInCheck.In){
				list.Add(NewInput("notes"));
			}
			return list;
		}

		private static List<SheetFieldDef> GetConsent(OutInCheck outInCheck){
			List<SheetFieldDef> list=new List<SheetFieldDef>();
			if(outInCheck==OutInCheck.Out){
				list.Add(NewOutput("dateTime.Today"));
				list.Add(NewOutput("patient.nameFL"));
			}
			else if(outInCheck==OutInCheck.In){
				list.Add(NewInput("toothNum"));
			}
			else if(outInCheck==OutInCheck.Check) {
				list.Add(NewCheck("misc"));
			}
			return list;
		}

		private static List<SheetFieldDef> GetPatientLetter(OutInCheck outInCheck){
			List<SheetFieldDef> list=new List<SheetFieldDef>();
			if(outInCheck==OutInCheck.Out){
				list.Add(NewOutput("PracticeTitle"));
				list.Add(NewOutput("PracticeAddress"));
				list.Add(NewOutput("practiceCityStateZip"));
				list.Add(NewOutput("patient.nameFL"));
				list.Add(NewOutput("patient.address"));
				list.Add(NewOutput("patient.cityStateZip"));
				list.Add(NewOutput("today.DayDate"));
				list.Add(NewOutput("patient.salutation"));
				list.Add(NewOutput("patient.priProvNameFL"));
			}
			else if(outInCheck==OutInCheck.In){
				//none
			}
			return list;
		}

		private static List<SheetFieldDef> GetReferralLetter(OutInCheck outInCheck){
			List<SheetFieldDef> list=new List<SheetFieldDef>();
			if(outInCheck==OutInCheck.Out){
				list.Add(NewOutput("PracticeTitle"));
				list.Add(NewOutput("PracticeAddress"));
				list.Add(NewOutput("practiceCityStateZip"));
				list.Add(NewOutput("referral.phone"));
				list.Add(NewOutput("referral.phone2"));
				list.Add(NewOutput("referral.nameFL"));
				list.Add(NewOutput("referral.address"));
				list.Add(NewOutput("referral.cityStateZip"));
				list.Add(NewOutput("today.DayDate"));
				list.Add(NewOutput("patient.nameFL"));
				list.Add(NewOutput("referral.salutation"));
				list.Add(NewOutput("patient.priProvNameFL"));
			}
			else if(outInCheck==OutInCheck.In){
				//none
			}
			else if(outInCheck==OutInCheck.Check) {
				list.Add(NewCheck("misc"));
			}
			return list;
		}

		private static List<SheetFieldDef> GetPatientForm(OutInCheck outInCheck) {
			List<SheetFieldDef> list=new List<SheetFieldDef>();
			if(outInCheck==OutInCheck.Out){
				//I can't really think of any for this kind				
			}
			else if(outInCheck==OutInCheck.In){
				list.Add(NewInput("Address"));
				list.Add(NewInput("Address2"));
				list.Add(NewInput("Birthdate"));
				list.Add(NewInput("City"));
				list.Add(NewInput("Email"));
				list.Add(NewInput("FName"));
				list.Add(NewInput("HmPhone"));
				list.Add(NewInput("ins1CarrierName"));
				list.Add(NewInput("ins1CarrierPhone"));
				list.Add(NewInput("ins1EmployerName"));
				list.Add(NewInput("ins1GroupName"));
				list.Add(NewInput("ins1GroupNum"));
				list.Add(NewInput("ins1SubscriberID"));
				list.Add(NewInput("ins1SubscriberNameF"));
				list.Add(NewInput("ins2CarrierName"));
				list.Add(NewInput("ins2CarrierPhone"));
				list.Add(NewInput("ins2EmployerName"));
				list.Add(NewInput("ins2GroupName"));
				list.Add(NewInput("ins2GroupNum"));
				list.Add(NewInput("ins2SubscriberID"));
				list.Add(NewInput("ins2SubscriberNameF"));
				list.Add(NewInput("LName"));
				list.Add(NewInput("MiddleI"));
				list.Add(NewInput("misc"));
				list.Add(NewInput("Preferred"));
				list.Add(NewInput("referredFrom"));
				list.Add(NewInput("SSN"));
				list.Add(NewInput("State"));
				list.Add(NewInput("WkPhone"));
				list.Add(NewInput("WirelessPhone"));
				list.Add(NewInput("wirelessCarrier"));
				list.Add(NewInput("Zip"));
			}
			else if(outInCheck==OutInCheck.Check){
				list.Add(NewCheck("addressAndHmPhoneIsSameEntireFamily"));
				list.Add(NewCheck("Gender"));
				list.Add(NewCheck("ins1Relat"));
				list.Add(NewCheck("ins2Relat"));
				list.Add(NewCheck("misc"));
				list.Add(NewCheck("Position"));
				list.Add(NewCheck("PreferConfirmMethod"));
				list.Add(NewCheck("PreferContactMethod"));
				list.Add(NewCheck("PreferRecallMethod"));
				list.Add(NewCheck("StudentStatus"));
			}
			return list;
		}

		private static List<SheetFieldDef> GetRoutingSlip(OutInCheck outInCheck) {
			List<SheetFieldDef> list=new List<SheetFieldDef>();
			if(outInCheck==OutInCheck.Out) {
				list.Add(NewOutput("appt.timeDate"));
				list.Add(NewOutput("appt.length"));
				list.Add(NewOutput("appt.providers"));
				list.Add(NewOutput("appt.procedures"));
				list.Add(NewOutput("appt.Note"));
				list.Add(NewOutput("otherFamilyMembers"));
				//most fields turned out to work best as static text.
			}
			else if(outInCheck==OutInCheck.In) {
				//Not applicable
			}
			else if(outInCheck==OutInCheck.Check) {
				//Not applicable
			}
			return list;
		}

		private static List<SheetFieldDef> GetMedicalHistory(OutInCheck outInCheck) {
			List<SheetFieldDef> list=new List<SheetFieldDef>();
			if(outInCheck==OutInCheck.Out) {
				//none
			}
			else if(outInCheck==OutInCheck.In) {
				list.Add(NewInput("Birthdate"));
				list.Add(NewInput("FName"));
				list.Add(NewInput("LName"));
				list.Add(NewInput("misc"));
			}
			else if(outInCheck==OutInCheck.Check) {
				list.Add(NewCheck("misc"));
			}
			return list;
		}

		private static List<SheetFieldDef> GetLabSlip(OutInCheck outInCheck) {
			List<SheetFieldDef> list=new List<SheetFieldDef>();
			if(outInCheck==OutInCheck.Out) {
				list.Add(NewOutput("lab.Description"));
				list.Add(NewOutput("lab.Phone"));
				list.Add(NewOutput("lab.Notes"));
				list.Add(NewOutput("lab.WirelessPhone"));
				list.Add(NewOutput("lab.Address"));
				list.Add(NewOutput("lab.CityStZip"));
				list.Add(NewOutput("lab.Email"));
				list.Add(NewOutput("appt.DateTime"));
				list.Add(NewOutput("labcase.DateTimeDue"));
				list.Add(NewOutput("labcase.DateTimeCreated"));
				list.Add(NewOutput("prov.nameFormal"));
				list.Add(NewOutput("prov.stateLicence"));
				//patient fields already handled with static text: name,age,gender.
				//other fields already handled: dateToday, practice address and phone.
			}
			else if(outInCheck==OutInCheck.In) {
				list.Add(NewInput("notes"));
				list.Add(NewInput("labcase.Instructions"));
			}
			else if(outInCheck==OutInCheck.Check) {
				list.Add(NewCheck("misc"));
			}
			return list;
		}

		public static List<SheetFieldDef> GetExamSheet(OutInCheck outInCheck){
			List<SheetFieldDef> list=new List<SheetFieldDef>();
			if(outInCheck==OutInCheck.Out) {
			}
			else if(outInCheck==OutInCheck.In){
				list.Add(NewInput("misc"));
			}
			else if(outInCheck==OutInCheck.Check){
				list.Add(NewCheck("misc"));
			}
			return list;
		}

		public static List<SheetFieldDef> GetDepositSlip(OutInCheck outInCheck){
			List<SheetFieldDef> list=new List<SheetFieldDef>();
			if(outInCheck==OutInCheck.Out) {
				list.Add(NewOutput("deposit.BankAccountInfo"));
				list.Add(NewOutput("deposit.DateDeposit"));
				list.Add(NewOutput("depositList"));
				list.Add(NewOutput("depositTotal"));
				list.Add(NewOutput("depositItemCount"));
				list.Add(NewOutput("depositItem01"));
				list.Add(NewOutput("depositItem02"));
				list.Add(NewOutput("depositItem03"));
				list.Add(NewOutput("depositItem04"));
				list.Add(NewOutput("depositItem05"));
				list.Add(NewOutput("depositItem06"));
				list.Add(NewOutput("depositItem07"));
				list.Add(NewOutput("depositItem08"));
				list.Add(NewOutput("depositItem09"));
				list.Add(NewOutput("depositItem10"));
				list.Add(NewOutput("depositItem11"));
				list.Add(NewOutput("depositItem12"));
				list.Add(NewOutput("depositItem13"));
				list.Add(NewOutput("depositItem14"));
				list.Add(NewOutput("depositItem15"));
				list.Add(NewOutput("depositItem16"));
				list.Add(NewOutput("depositItem17"));
				list.Add(NewOutput("depositItem18"));
			}
			else if(outInCheck==OutInCheck.In){				
			}
			else if(outInCheck==OutInCheck.Check){
			}
			return list;
		}




	}

}
