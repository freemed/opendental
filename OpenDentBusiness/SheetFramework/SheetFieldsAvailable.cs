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
				//case SheetTypeEnum.PatientRegistration:
				//	return GetPatientRegistration(outInCheck);
			}
			return new List<SheetFieldDef>();
		}

		private static SheetFieldDef NewOutput(string fieldName){
			return new SheetFieldDef(SheetFieldType.OutputText,fieldName,"",0,"",false,0,0,0,0,GrowthBehaviorEnum.None);
		}

		private static SheetFieldDef NewInput(string fieldName){
			return new SheetFieldDef(SheetFieldType.InputField,fieldName,"",0,"",false,0,0,0,0,GrowthBehaviorEnum.None);
		}

		private static SheetFieldDef NewCheck(string fieldName){
			return new SheetFieldDef(SheetFieldType.CheckBox,fieldName,"",0,"",false,0,0,0,0,GrowthBehaviorEnum.None);
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
			return list;
		}

		/*
		private static List<SheetFieldDef> GetPatientRegistration(OutInCheck outInCheck) {
			List<SheetFieldDef> list=new List<SheetFieldDef>();
			if(outInCheck==OutInCheck.Out){
				
				
			}
			else if(outInCheck==OutInCheck.In){
				list.Add(NewOutput("FName"));
				list.Add(NewOutput("LName"));
				list.Add(NewOutput("MiddleI"));
				list.Add(NewOutput("Preferred"));
				list.Add(NewOutput("Birthdate"));
				list.Add(NewOutput("SSN"));
				list.Add(NewOutput("Address"));
				list.Add(NewOutput("Address2"));
				list.Add(NewOutput("City"));
				list.Add(NewOutput("State"));
				list.Add(NewOutput("Zip"));
				list.Add(NewOutput("HmPhone"));
				list.Add(NewOutput("WkPhone"));
				list.Add(NewOutput("WirelessPhone"));
				/*list.Add(NewOutput(""));
				list.Add(NewOutput(""));
				list.Add(NewOutput(""));
				list.Add(NewOutput(""));
				list.Add(NewOutput(""));
				list.Add(NewOutput(""));
				list.Add(NewOutput(""));

				list.Add(NewInput("notes"));
			}
			else if(outInCheck==OutInCheck.Check){
				list.Add(NewCheck("GenderIsMale"));
				list.Add(NewCheck("GenderIsFemale"));
				list.Add(NewCheck("PositionIsMarried"));
				list.Add(NewCheck("PositionIsSingle"));
				//list.Add(NewCheck(""));
				


				list.Add(NewCheck("misc"));
			}
			return list;
		}*/


	}

}
