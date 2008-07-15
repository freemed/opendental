using System;
using System.Collections.Generic;
using System.Text;
using OpenDentBusiness;

namespace OpenDental {
	public class SheetFiller {
		///<summary>Gets the data from the database and fills the fields.</summary>
		public static void FillFields(SheetDef sheetDef){
			switch(sheetDef.SheetType) {
				case SheetTypeEnum.LabelPatient:
					Patient pat=Patients.GetPat((int)GetParamByName(sheetDef,"PatNum").ParamValue);
					FillFieldsForLabelPatient(sheetDef,pat);
					break;
				case SheetTypeEnum.LabelCarrier:
					Carrier carrier=Carriers.GetCarrier((int)GetParamByName(sheetDef,"CarrierNum").ParamValue);
					FillFieldsForLabelCarrier(sheetDef,carrier);
					break;
				case SheetTypeEnum.LabelReferral:
					Referral refer=Referrals.GetReferral((int)GetParamByName(sheetDef,"ReferralNum").ParamValue);
					FillFieldsForLabelReferral(sheetDef,refer);
					break;
				case SheetTypeEnum.ReferralSlip:
					FillFieldsForReferralSlip(sheetDef);
					break;
			}
		}

		private static SheetParameter GetParamByName(SheetDef sheetDef,string paramName){
			foreach(SheetParameter param in sheetDef.Parameters){
				if(param.ParamName==paramName){
					return param;
				}
			}
			return null;
		}

		private static void FillFieldsForLabelPatient(SheetDef sheetDef,Patient pat){
			foreach(SheetFieldDef fieldDef in sheetDef.SheetFieldDefs){
				switch(fieldDef.FieldName){
					case "nameFL":
						fieldDef.FieldValue=pat.GetNameFLFormal();
						break;
					case "nameLF":
						fieldDef.FieldValue=pat.GetNameLF();
						break;
					case "address":
						fieldDef.FieldValue=pat.Address;
						if(pat.Address2!=""){
							fieldDef.FieldValue+="\r\n"+pat.Address2;
						}
						break;
					case "cityStateZip":
						fieldDef.FieldValue=pat.City+", "+pat.State+" "+pat.Zip;
						break;
					case "ChartNumber":
						fieldDef.FieldValue=pat.ChartNumber;
						break;
					case "PatNum":
						fieldDef.FieldValue=pat.PatNum.ToString();
						break;
					case "dateTime.Today":
						fieldDef.FieldValue=DateTime.Today.ToShortDateString();
						break;
					case "birthdate":
						//only a temporary workaround:
						fieldDef.FieldValue="BD: "+pat.Birthdate.ToShortDateString();
						break;
					case "priProvName":
						fieldDef.FieldValue=Providers.GetLongDesc(pat.PriProv);
						break;
				}
			}
		}

		private static void FillFieldsForLabelCarrier(SheetDef sheetDef,Carrier carrier) {
			foreach(SheetFieldDef fieldDef in sheetDef.SheetFieldDefs) {
				switch(fieldDef.FieldName) {
					case "CarrierName":
						fieldDef.FieldValue=carrier.CarrierName;
						break;
					case "address":
						fieldDef.FieldValue=carrier.Address;
						if(carrier.Address2!="") {
							fieldDef.FieldValue+="\r\n"+carrier.Address2;
						}
						break;
					case "cityStateZip":
						fieldDef.FieldValue=carrier.City+", "+carrier.State+" "+carrier.Zip;
						break;
				}
			}
		}

		private static void FillFieldsForLabelReferral(SheetDef sheetDef,Referral refer) {
			foreach(SheetFieldDef fieldDef in sheetDef.SheetFieldDefs) {
				switch(fieldDef.FieldName) {
					case "nameFL":
						fieldDef.FieldValue=Referrals.GetNameFL(refer.ReferralNum);
						break;
					case "address":
						fieldDef.FieldValue=refer.Address;
						if(refer.Address2!="") {
							fieldDef.FieldValue+="\r\n"+refer.Address2;
						}
						break;
					case "cityStateZip":
						fieldDef.FieldValue=refer.City+", "+refer.ST+" "+refer.Zip;
						break;
				}
			}
		}

		private static void FillFieldsForReferralSlip(SheetDef sheetDef) {
			Patient pat=Patients.GetPat((int)GetParamByName(sheetDef,"PatNum").ParamValue);
			Referral refer=Referrals.GetReferral((int)GetParamByName(sheetDef,"ReferralNum").ParamValue);
			foreach(SheetFieldDef fieldDef in sheetDef.SheetFieldDefs) {
				switch(fieldDef.FieldName) {
					case "referral.nameFL":
						fieldDef.FieldValue=Referrals.GetNameFL(refer.ReferralNum);
						break;
					case "referral.address":
						fieldDef.FieldValue=refer.Address;
						if(refer.Address2!="") {
							fieldDef.FieldValue+="\r\n"+refer.Address2;
						}
						break;
					case "referral.cityStateZip":
						fieldDef.FieldValue=refer.City+", "+refer.ST+" "+refer.Zip;
						break;
					case "patient.nameFL":
						fieldDef.FieldValue=pat.GetNameFL();
						break;
					case "dateTime.Today":
						fieldDef.FieldValue=DateTime.Today.ToShortDateString();
						break;
					case "patient.WkPhone":
						fieldDef.FieldValue=pat.WkPhone;
						break;
					case "patient.HmPhone":
						fieldDef.FieldValue=pat.HmPhone;
						break;
					case "patient.WirelessPhone":
						fieldDef.FieldValue=pat.WirelessPhone;
						break;
					case "patient.address":
						fieldDef.FieldValue=pat.Address;
						if(pat.Address2!="") {
							fieldDef.FieldValue+="\r\n"+pat.Address2;
						}
						break;
					case "patient.cityStateZip":
						fieldDef.FieldValue=pat.City+", "+pat.State+" "+pat.Zip;
						break;
					case "patient.provider":
						fieldDef.FieldValue=Providers.GetProv(Patients.GetProvNum(pat)).GetFormalName();
						break;
					//case "notes"://an input field
				}
			}
		}



	}
}
