using System;
using System.Collections.Generic;
using System.Text;
using OpenDentBusiness;

namespace OpenDental{
	public class SheetFiller {
		///<summary>Gets the data from the database and fills the fields.</summary>
		public static void FillFields(Sheet sheet){
			foreach(SheetParameter param in sheet.Parameters){
				if(param.IsRequired && param.ParamValue==null){
					throw new ApplicationException(Lan.g("Sheet","Parameter not specified for sheet: ")+param.ParamName);
				}
			}
			switch(sheet.SheetType) {
				case SheetTypeEnum.LabelPatient:
					Patient pat=Patients.GetPat((int)GetParamByName(sheet,"PatNum").ParamValue);
					FillFieldsForLabelPatient(sheet,pat);
					break;
				case SheetTypeEnum.LabelCarrier:
					Carrier carrier=Carriers.GetCarrier((int)GetParamByName(sheet,"CarrierNum").ParamValue);
					FillFieldsForLabelCarrier(sheet,carrier);
					break;
				case SheetTypeEnum.LabelReferral:
					Referral refer=Referrals.GetReferral((int)GetParamByName(sheet,"ReferralNum").ParamValue);
					FillFieldsForLabelReferral(sheet,refer);
					break;
				case SheetTypeEnum.ReferralSlip:
					FillFieldsForReferralSlip(sheet);
					break;
				case SheetTypeEnum.LabelAppointment:
					FillFieldsForLabelAppointment(sheet);
					break;
				case SheetTypeEnum.Rx:
					FillFieldsForRx(sheet);
					break;
			}
		}

		private static SheetParameter GetParamByName(Sheet sheet,string paramName){
			foreach(SheetParameter param in sheet.Parameters){
				if(param.ParamName==paramName){
					return param;
				}
			}
			return null;
		}

		private static void FillFieldsForLabelPatient(Sheet sheet,Patient pat){
			foreach(SheetField field in sheet.SheetFields){
				switch(field.FieldName){
					case "nameFL":
						field.FieldValue=pat.GetNameFLFormal();
						break;
					case "nameLF":
						field.FieldValue=pat.GetNameLF();
						break;
					case "address":
						field.FieldValue=pat.Address;
						if(pat.Address2!=""){
							field.FieldValue+="\r\n"+pat.Address2;
						}
						break;
					case "cityStateZip":
						field.FieldValue=pat.City+", "+pat.State+" "+pat.Zip;
						break;
					case "ChartNumber":
						field.FieldValue=pat.ChartNumber;
						break;
					case "PatNum":
						field.FieldValue=pat.PatNum.ToString();
						break;
					case "dateTime.Today":
						field.FieldValue=DateTime.Today.ToShortDateString();
						break;
					case "birthdate":
						//only a temporary workaround:
						field.FieldValue="BD: "+pat.Birthdate.ToShortDateString();
						break;
					case "priProvName":
						field.FieldValue=Providers.GetLongDesc(pat.PriProv);
						break;
				}
			}
		}

		private static void FillFieldsForLabelCarrier(Sheet sheet,Carrier carrier) {
			foreach(SheetField field in sheet.SheetFields) {
				switch(field.FieldName) {
					case "CarrierName":
						field.FieldValue=carrier.CarrierName;
						break;
					case "address":
						field.FieldValue=carrier.Address;
						if(carrier.Address2!="") {
							field.FieldValue+="\r\n"+carrier.Address2;
						}
						break;
					case "cityStateZip":
						field.FieldValue=carrier.City+", "+carrier.State+" "+carrier.Zip;
						break;
				}
			}
		}

		private static void FillFieldsForLabelReferral(Sheet sheet,Referral refer) {
			foreach(SheetField field in sheet.SheetFields) {
				switch(field.FieldName) {
					case "nameFL":
						field.FieldValue=Referrals.GetNameFL(refer.ReferralNum);
						break;
					case "address":
						field.FieldValue=refer.Address;
						if(refer.Address2!="") {
							field.FieldValue+="\r\n"+refer.Address2;
						}
						break;
					case "cityStateZip":
						field.FieldValue=refer.City+", "+refer.ST+" "+refer.Zip;
						break;
				}
			}
		}

		private static void FillFieldsForReferralSlip(Sheet sheet) {
			Patient pat=Patients.GetPat((int)GetParamByName(sheet,"PatNum").ParamValue);
			Referral refer=Referrals.GetReferral((int)GetParamByName(sheet,"ReferralNum").ParamValue);
			foreach(SheetField field in sheet.SheetFields) {
				switch(field.FieldName) {
					case "referral.nameFL":
						field.FieldValue=Referrals.GetNameFL(refer.ReferralNum);
						break;
					case "referral.address":
						field.FieldValue=refer.Address;
						if(refer.Address2!="") {
							field.FieldValue+="\r\n"+refer.Address2;
						}
						break;
					case "referral.cityStateZip":
						field.FieldValue=refer.City+", "+refer.ST+" "+refer.Zip;
						break;
					case "referral.phone":
						field.FieldValue="";
						if(refer.Telephone.Length==10){
							field.FieldValue="("+refer.Telephone.Substring(0,3)+")"
								+refer.Telephone.Substring(3,3)+"-"
								+refer.Telephone.Substring(6);
						}
						break;
					case "patient.nameFL":
						field.FieldValue=pat.GetNameFL();
						break;
					case "dateTime.Today":
						field.FieldValue=DateTime.Today.ToShortDateString();
						break;
					case "patient.WkPhone":
						field.FieldValue=pat.WkPhone;
						break;
					case "patient.HmPhone":
						field.FieldValue=pat.HmPhone;
						break;
					case "patient.WirelessPhone":
						field.FieldValue=pat.WirelessPhone;
						break;
					case "patient.address":
						field.FieldValue=pat.Address;
						if(pat.Address2!="") {
							field.FieldValue+="\r\n"+pat.Address2;
						}
						break;
					case "patient.cityStateZip":
						field.FieldValue=pat.City+", "+pat.State+" "+pat.Zip;
						break;
					case "patient.provider":
						field.FieldValue=Providers.GetProv(Patients.GetProvNum(pat)).GetFormalName();
						break;
					//case "notes"://an input field
				}
			}
		}

		private static void FillFieldsForLabelAppointment(Sheet sheet) {
			Appointment appt=Appointments.GetOneApt((int)GetParamByName(sheet,"AptNum").ParamValue);
			Patient pat=Patients.GetPat(appt.PatNum);
			foreach(SheetField field in sheet.SheetFields) {
				switch(field.FieldName) {
					case "nameFL":
						field.FieldValue=pat.GetNameFirstOrPrefL();
						break;
					case "nameLF":
						field.FieldValue=pat.GetNameLF();
						break;
					case "weekdayDateTime":
						field.FieldValue=appt.AptDateTime.ToString("ddd")+"   "
							+appt.AptDateTime.ToShortDateString()+"  "
							+appt.AptDateTime.ToShortTimeString();//  h:mm tt");
						break;
					case "length":
						int minutesTotal=appt.Pattern.Length*5;
						int hours=minutesTotal/60;//automatically rounds down
						int minutes=minutesTotal-hours*60;
						field.FieldValue="";
						if(hours>0){
							field.FieldValue=hours.ToString()+" hours, ";
						}
						field.FieldValue+=minutes.ToString()+" min";
						break;
				}
			}
		}

		private static void FillFieldsForRx(Sheet sheet) {
			RxPat rx=RxPats.GetRx((int)GetParamByName(sheet,"RxNum").ParamValue);
			Patient pat=Patients.GetPat(rx.PatNum);
			Provider prov=Providers.GetProv(rx.ProvNum);
			string text;
			foreach(SheetField field in sheet.SheetFields) {
				switch(field.FieldName) {
					case "prov.nameFL":
						field.FieldValue=prov.GetFormalName();
						break;
					case "prov.address":
						field.FieldValue=PrefC.GetString("PracticeAddress");
						if(PrefC.GetString("PracticeAddress2")!=""){
							field.FieldValue+="\r\n"+PrefC.GetString("PracticeAddress2");
						}
						break;
					case "prov.cityStateZip":
						field.FieldValue=PrefC.GetString("PracticeCity")+", "+PrefC.GetString("PracticeST")+" "+PrefC.GetString("PracticeZip");;
						break;
					case "prov.phone":
						text=PrefC.GetString("PracticePhone");
						field.FieldValue=text;
						if(text.Length==10) {
							field.FieldValue="("+text.Substring(0,3)+")"+text.Substring(3,3)+"-"+text.Substring(6);
						}
						break;
					case "RxDate":
						field.FieldValue=rx.RxDate.ToShortDateString();
						break;
					case "prov.dEANum":
						if(rx.IsControlled){
							field.FieldValue=prov.DEANum;
						}
						else{
							field.FieldValue="";
						}
						break;
					case "pat.nameFL":
						//Can't include preferred, so:
						field.FieldValue=pat.FName+" "+pat.MiddleI+"  "+pat.LName;
						break;
					case "pat.Birthdate":
						if(pat.Birthdate.Year<1880){
							field.FieldValue="";
						}
						else{
							field.FieldValue=pat.Birthdate.ToShortDateString();
						}
						break;
					case "pat.HmPhone":
						field.FieldValue=pat.HmPhone;
						break;
					case "pat.address":
						field.FieldValue=pat.Address;
						if(pat.Address2!=""){
							field.FieldValue+="\r\n"+pat.Address2;
						}
						break;
					case "pat.cityStateZip":
						field.FieldValue=pat.City+", "+pat.State+" "+pat.Zip;
						break;
					case "Drug":
						field.FieldValue=rx.Drug;
						break;
					case "Disp":
						field.FieldValue=rx.Disp;
						break;
					case "Sig":
						field.FieldValue=rx.Sig;
						break;
					case "Refills":
						field.FieldValue=rx.Refills;
						break;					
				}
			}
		}



	}
}
