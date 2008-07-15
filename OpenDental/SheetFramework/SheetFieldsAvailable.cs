using System;
using System.Collections.Generic;
using System.Text;
using OpenDentBusiness;

namespace OpenDental{
	class SheetFieldsAvailable {
		///<Summary>This isn't actually used yet.  It's just a handy place to store the lists.  Later, when there is a UI for customizing sheets, this will be the list of fields they pick from.</Summary>
		public static List<SheetFieldDef> GetList(SheetTypeEnum sheetType){
			switch(sheetType){
				case SheetTypeEnum.LabelPatient:
					return GetLabelPatient();
				case SheetTypeEnum.LabelCarrier:
					return GetLabelCarrier();
				case SheetTypeEnum.LabelReferral:
					return GetLabelReferral();
				case SheetTypeEnum.ReferralSlip:
					return GetReferralSlip();
			}
			return new List<SheetFieldDef>();
		}

		private static SheetFieldDef NewOutput(string fieldName){
			return new SheetFieldDef(SheetFieldType.OutputText,fieldName,"",0,0,0,0,null,GrowthBehaviorEnum.None);
		}

		private static SheetFieldDef NewInput(string fieldName){
			return new SheetFieldDef(SheetFieldType.InputField,fieldName,"",0,0,0,0,null,GrowthBehaviorEnum.None);
		}

		private static List<SheetFieldDef> GetLabelPatient(){
			List<SheetFieldDef> list=new List<SheetFieldDef>();
			list.Add(NewOutput("nameFL"));
			list.Add(NewOutput("nameLF"));
			list.Add(NewOutput("address"));//includes address2
			list.Add(NewOutput("cityStateZip"));
			list.Add(NewOutput("ChartNumber"));
			list.Add(NewOutput("PatNum"));
			list.Add(NewOutput("dateTime.Today"));
			list.Add(NewOutput("birthdate"));
			list.Add(NewOutput("priProvName"));
			return list;
		}

		private static List<SheetFieldDef> GetLabelCarrier() {
			List<SheetFieldDef> list=new List<SheetFieldDef>();
			list.Add(NewOutput("CarrierName"));
			list.Add(NewOutput("address"));//includes address2
			list.Add(NewOutput("cityStateZip"));
			return list;
		}

		private static List<SheetFieldDef> GetLabelReferral() {
			List<SheetFieldDef> list=new List<SheetFieldDef>();
			list.Add(NewOutput("nameFL"));//includes Title
			list.Add(NewOutput("address"));//includes address2
			list.Add(NewOutput("cityStateZip"));
			return list;
		}

		private static List<SheetFieldDef> GetReferralSlip() {
			List<SheetFieldDef> list=new List<SheetFieldDef>();
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
			list.Add(NewInput("notes"));
			return list;
		}





	}

}
