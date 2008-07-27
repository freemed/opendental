using System;
using System.Collections.Generic;
using System.Text;

namespace OpenDentBusiness{
	public class SheetFieldsAvailable {
		public static List<SheetFieldDef> GetListInput(SheetTypeEnum sheetType){
			return GetList(sheetType,false);
		}

		public static List<SheetFieldDef> GetListOutput(SheetTypeEnum sheetType){
			return GetList(sheetType,true);
		}

		///<Summary>This is the list of input or output fieldnames for user to pick from.  The only two options are input or output.</Summary>
		private static List<SheetFieldDef> GetList(SheetTypeEnum sheetType,bool isOutput){
			switch(sheetType){
				case SheetTypeEnum.LabelPatient:
					return GetLabelPatient(isOutput);
				case SheetTypeEnum.LabelCarrier:
					return GetLabelCarrier(isOutput);
				case SheetTypeEnum.LabelReferral:
					return GetLabelReferral(isOutput);
				case SheetTypeEnum.ReferralSlip:
					return GetReferralSlip(isOutput);
				case SheetTypeEnum.LabelAppointment:
					return GetLabelAppointment(isOutput);
				case SheetTypeEnum.Rx:
					return GetRx(isOutput);
			}
			return new List<SheetFieldDef>();
		}

		private static SheetFieldDef NewOutput(string fieldName){
			return new SheetFieldDef(SheetFieldType.OutputText,fieldName,"",0,"",false,0,0,0,0,GrowthBehaviorEnum.None);
		}

		private static SheetFieldDef NewInput(string fieldName){
			return new SheetFieldDef(SheetFieldType.InputField,fieldName,"",0,"",false,0,0,0,0,GrowthBehaviorEnum.None);
		}

		private static List<SheetFieldDef> GetLabelPatient(bool isOutput){
			List<SheetFieldDef> list=new List<SheetFieldDef>();
			if(isOutput){
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
			else{

			}
			return list;
		}

		private static List<SheetFieldDef> GetLabelCarrier(bool isOutput) {
			List<SheetFieldDef> list=new List<SheetFieldDef>();
			if(isOutput){
				list.Add(NewOutput("CarrierName"));
				list.Add(NewOutput("address"));//includes address2
				list.Add(NewOutput("cityStateZip"));
			}
			else{

			}
			return list;
		}

		private static List<SheetFieldDef> GetLabelReferral(bool isOutput) {
			List<SheetFieldDef> list=new List<SheetFieldDef>();
			if(isOutput){
				list.Add(NewOutput("nameFL"));//includes Title
				list.Add(NewOutput("address"));//includes address2
				list.Add(NewOutput("cityStateZip"));
			}
			else{

			}
			return list;
		}

		private static List<SheetFieldDef> GetReferralSlip(bool isOutput) {
			List<SheetFieldDef> list=new List<SheetFieldDef>();
			if(isOutput){
				list.Add(NewOutput("referral.nameFL"));
				list.Add(NewOutput("referral.address"));
				list.Add(NewOutput("referral.cityStateZip"));
				list.Add(NewOutput("patient.nameFL"));
				list.Add(NewOutput("dateTime.Today"));
				list.Add(NewOutput("patient.WkPhone"));
				list.Add(NewOutput("patient.HmPhone"));
				list.Add(NewOutput("patient.WirelessPhone"));
				list.Add(NewOutput("patient.address"));
				list.Add(NewOutput("patient.cityStateZip"));
				list.Add(NewOutput("patient.provider"));
			}
			else{
				list.Add(NewInput("notes"));
			}
			return list;
		}

		private static List<SheetFieldDef> GetLabelAppointment(bool isOutput){
			List<SheetFieldDef> list=new List<SheetFieldDef>();
			if(isOutput){
				list.Add(NewOutput("nameFL"));
				list.Add(NewOutput("nameLF"));
				list.Add(NewOutput("weekdayDateTime"));
				list.Add(NewOutput("length"));
			}
			else{
				//list.Add(NewInput("notes"));
			}
			return list;
		}

		private static List<SheetFieldDef> GetRx(bool isOutput){
			List<SheetFieldDef> list=new List<SheetFieldDef>();
			if(isOutput){
				list.Add(NewOutput("prov.nameFL"));
				list.Add(NewOutput("prov.address"));
				list.Add(NewOutput("prov.cityStateZip"));
				list.Add(NewOutput("prov.phone"));
				list.Add(NewOutput("RxDate"));
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
			else{
				list.Add(NewInput("notes"));
			}
			return list;
		}


	}

}
