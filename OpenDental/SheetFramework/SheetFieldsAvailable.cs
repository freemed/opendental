using System;
using System.Collections.Generic;
using System.Text;

namespace OpenDental{
	class SheetFieldsAvailable {
		///<Summary>This isn't actually used yet.  It's just a handy place to store the lists.  Later, when there is a UI for customizing sheets, this will be the list of fields they pick from.</Summary>
		public static List<SheetField> GetList(SheetTypeEnum sheetType){
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
			return new List<SheetField>();
		}

		private static List<SheetField> GetLabelPatient(){
			List<SheetField> list=new List<SheetField>();
			list.Add(new SheetField(SheetFieldType.OutputText,"nameFL"));
			list.Add(new SheetField(SheetFieldType.OutputText,"nameLF"));
			list.Add(new SheetField(SheetFieldType.OutputText,"address"));//includes address2
			list.Add(new SheetField(SheetFieldType.OutputText,"cityStateZip"));
			list.Add(new SheetField(SheetFieldType.OutputText,"ChartNumber"));
			list.Add(new SheetField(SheetFieldType.OutputText,"PatNum"));
			list.Add(new SheetField(SheetFieldType.OutputText,"dateTime.Today"));
			list.Add(new SheetField(SheetFieldType.OutputText,"birthdate"));
			list.Add(new SheetField(SheetFieldType.OutputText,"priProvName"));
			return list;
		}

		private static List<SheetField> GetLabelCarrier() {
			List<SheetField> list=new List<SheetField>();
			list.Add(new SheetField(SheetFieldType.OutputText,"CarrierName"));
			list.Add(new SheetField(SheetFieldType.OutputText,"address"));//includes address2
			list.Add(new SheetField(SheetFieldType.OutputText,"cityStateZip"));
			return list;
		}

		private static List<SheetField> GetLabelReferral() {
			List<SheetField> list=new List<SheetField>();
			list.Add(new SheetField(SheetFieldType.OutputText,"nameFL"));//includes Title
			list.Add(new SheetField(SheetFieldType.OutputText,"address"));//includes address2
			list.Add(new SheetField(SheetFieldType.OutputText,"cityStateZip"));
			return list;
		}

		private static List<SheetField> GetReferralSlip() {
			List<SheetField> list=new List<SheetField>();
			
			return list;
		}





	}

}
