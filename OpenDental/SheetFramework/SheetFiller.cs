using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
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
			Patient pat=null;
			Referral refer=null;
			switch(sheet.SheetType) {
				case SheetTypeEnum.LabelPatient:
					pat=Patients.GetPat((long)GetParamByName(sheet,"PatNum").ParamValue);
					FillFieldsForLabelPatient(sheet,pat);
					break;
				case SheetTypeEnum.LabelCarrier:
					Carrier carrier=Carriers.GetCarrier((long)GetParamByName(sheet,"CarrierNum").ParamValue);
					FillFieldsForLabelCarrier(sheet,carrier);
					break;
				case SheetTypeEnum.LabelReferral:
					refer=Referrals.GetReferral((long)GetParamByName(sheet,"ReferralNum").ParamValue);
					FillFieldsForLabelReferral(sheet,refer);
					break;
				case SheetTypeEnum.ReferralSlip:
					pat=Patients.GetPat((long)GetParamByName(sheet,"PatNum").ParamValue);
					refer=Referrals.GetReferral((long)GetParamByName(sheet,"ReferralNum").ParamValue);
					FillFieldsForReferralSlip(sheet,pat,refer);
					break;
				case SheetTypeEnum.LabelAppointment:
					Appointment appt=Appointments.GetOneApt((long)GetParamByName(sheet,"AptNum").ParamValue);
					pat=Patients.GetPat(appt.PatNum);
					FillFieldsForLabelAppointment(sheet,appt,pat);
					break;
				case SheetTypeEnum.Rx:
					RxPat rx=RxPats.GetRx((long)GetParamByName(sheet,"RxNum").ParamValue);
					pat=Patients.GetPat(rx.PatNum);
					Provider prov=Providers.GetProv(rx.ProvNum);
					FillFieldsForRx(sheet,rx,pat,prov);
					break;
				case SheetTypeEnum.Consent:
					pat=Patients.GetPat((long)GetParamByName(sheet,"PatNum").ParamValue);
					FillFieldsForConsent(sheet,pat);
					break;
				case SheetTypeEnum.PatientLetter:
					pat=Patients.GetPat((long)GetParamByName(sheet,"PatNum").ParamValue);
					FillFieldsForPatientLetter(sheet,pat);
					break;
				case SheetTypeEnum.ReferralLetter:
					pat=Patients.GetPat((long)GetParamByName(sheet,"PatNum").ParamValue);
					refer=Referrals.GetReferral((long)GetParamByName(sheet,"ReferralNum").ParamValue);
					FillFieldsForReferralLetter(sheet,pat,refer);
					break;
				case SheetTypeEnum.PatientForm:
					pat=Patients.GetPat((long)GetParamByName(sheet,"PatNum").ParamValue);
					FillFieldsForPatientForm(sheet,pat);
					break;
				case SheetTypeEnum.RoutingSlip:
					Appointment apt=Appointments.GetOneApt((long)GetParamByName(sheet,"AptNum").ParamValue);
					pat=Patients.GetPat(apt.PatNum);
					FillFieldsForRoutingSlip(sheet,pat,apt);
					break;
				case SheetTypeEnum.MedicalHistory:
					pat=Patients.GetPat((long)GetParamByName(sheet,"PatNum").ParamValue);
					FillFieldsForMedicalHistory(sheet,pat);
					break;
			}
			FillFieldsInStaticText(sheet,pat);
			FillPatientImages(sheet,pat);
		}

		private static SheetParameter GetParamByName(Sheet sheet,string paramName){
			foreach(SheetParameter param in sheet.Parameters){
				if(param.ParamName==paramName){
					return param;
				}
			}
			return null;
		}

		private static void FillFieldsInStaticText(Sheet sheet,Patient pat) {
			if(pat==null){
				return;
			}
			string fldval;
			string address=pat.Address;
			if(pat.Address2!=""){
				address+=", "+pat.Address2;
			}
			string birthdate=pat.Birthdate.ToShortDateString();
			if(pat.Birthdate.Year<1880){
				birthdate="";
			}
			string dateFirstVisit=pat.DateFirstVisit.ToShortDateString();
			if(pat.DateFirstVisit.Year<1880) {
				dateFirstVisit="";
			}
			Family fam=Patients.GetFamily(pat.PatNum);
			string treatmentPlanProcs="";
			if(Sheets.ContainsStaticField(sheet,"treatmentPlanProcs")) {
				List<Procedure> procsList=Procedures.Refresh(pat.PatNum);
				for(int i=0;i<procsList.Count;i++) {
					if(procsList[i].ProcStatus!=ProcStat.TP) {
						continue;
					}
					if(treatmentPlanProcs!="") {
						treatmentPlanProcs+="\r\n";
					}
					treatmentPlanProcs+=ProcedureCodes.GetStringProcCode(procsList[i].CodeNum)+", "
						+Procedures.GetDescription(procsList[i])+", "
						+procsList[i].ProcFee.ToString("c");
				}
			}
			//Insurance------------------------
			List <PatPlan> patPlanList=PatPlans.Refresh(pat.PatNum);
			long planNum=PatPlans.GetPlanNum(patPlanList,1);
			long patPlanNum=PatPlans.GetPatPlanNum(patPlanList,planNum);
			List<InsPlan> planList=InsPlans.Refresh(fam);
			InsPlan plan=InsPlans.GetPlan(planNum,planList);
			Carrier carrier=null;
			List<Benefit> benefitList=Benefits.Refresh(patPlanList);
			List<ClaimProcHist> histList=ClaimProcs.GetHistList(pat.PatNum,benefitList,patPlanList,planList,DateTime.Today);
			string carrierName="";
			string carrierAddress="";
			string carrierCityStZip="";
			string subscriberId="";
			string subscriberNameFL="";
			string insAnnualMax="";
			string insDeductible="";
			string insDeductibleUsed="";
			string insPending="";
			string insPercentages="";
			string insUsed="";
			double doubAnnualMax;
			double doubDeductible;
			double doubDeductibleUsed;
			double doubPending;
			double doubUsed;
			if(plan!=null){
				carrier=Carriers.GetCarrier(plan.CarrierNum);
				carrierName=carrier.CarrierName;
				carrierAddress=carrier.Address;
				if(carrier.Address2!=""){
					carrierAddress+=", "+carrier.Address2;
				}
				carrierCityStZip=carrier.City+", "+carrier.State+"  "+carrier.Zip;
				subscriberId=plan.SubscriberID;
				subscriberNameFL=Patients.GetLim(plan.Subscriber).GetNameFL();
				doubAnnualMax=Benefits.GetAnnualMaxDisplay(benefitList,plan.PlanNum,patPlanNum);
				if(doubAnnualMax!=-1){
					insAnnualMax=doubAnnualMax.ToString("c");
				}
				doubDeductible=Benefits.GetDeductGeneralDisplay(benefitList,plan.PlanNum,patPlanNum,BenefitCoverageLevel.Individual);
				if(doubDeductible!=-1){
					insDeductible=doubDeductible.ToString("c");
				}
				doubDeductibleUsed=InsPlans.GetDedUsedDisplay(histList,DateTime.Today,plan.PlanNum,patPlanNum,-1,planList,BenefitCoverageLevel.Individual,pat.PatNum);
				if(doubDeductibleUsed!=-1){
					insDeductibleUsed=doubDeductibleUsed.ToString("c");
				}
				doubPending=InsPlans.GetPendingDisplay(histList,DateTime.Today,plan,patPlanNum,-1,pat.PatNum);
				if(doubPending!=-1) {
					insPending=doubPending.ToString("c");
				}
				doubUsed=InsPlans.GetInsUsedDisplay(histList,DateTime.Today,plan.PlanNum,patPlanNum,-1,planList);
				if(doubUsed!=-1) {
					insUsed=doubUsed.ToString("c");
				}
				for(int j=0;j<benefitList.Count;j++) {
					if(benefitList[j].PlanNum != plan.PlanNum) {
						continue;
					}
					if(benefitList[j].BenefitType != InsBenefitType.CoInsurance) {
						continue;
					}
					if(insPercentages!="") {
						insPercentages+=",  ";
					}
					insPercentages+=CovCats.GetDesc(benefitList[j].CovCatNum)+" "+benefitList[j].Percent.ToString()+"%";
				}
			}
			planNum=PatPlans.GetPlanNum(patPlanList,2);
			patPlanNum=PatPlans.GetPatPlanNum(patPlanList,planNum);
			plan=InsPlans.GetPlan(planNum,planList);
			string carrier2Name="";
			string subscriber2NameFL="";
			string ins2AnnualMax="";
			string ins2Deductible="";
			string ins2DeductibleUsed="";
			string ins2Pending="";
			string ins2Percentages="";
			string ins2Used="";
			if(plan!=null) {
				carrier=Carriers.GetCarrier(plan.CarrierNum);
				carrier2Name=carrier.CarrierName;
				//carrierAddress=carrier.Address;
				//if(carrier.Address2!="") {
				//	carrierAddress+=", "+carrier.Address2;
				//}
				//carrierCityStZip=carrier.City+", "+carrier.State+"  "+carrier.Zip;
				//subscriberId=plan.SubscriberID;
				subscriber2NameFL=Patients.GetLim(plan.Subscriber).GetNameFL();
				doubAnnualMax=Benefits.GetAnnualMaxDisplay(benefitList,plan.PlanNum,patPlanNum);
				if(doubAnnualMax!=-1) {
					ins2AnnualMax=doubAnnualMax.ToString("c");
				}
				doubDeductible=Benefits.GetDeductGeneralDisplay(benefitList,plan.PlanNum,patPlanNum,BenefitCoverageLevel.Individual);
				if(doubDeductible!=-1) {
					ins2Deductible=doubDeductible.ToString("c");
				}
				doubDeductibleUsed=InsPlans.GetDedUsedDisplay(histList,DateTime.Today,plan.PlanNum,patPlanNum,-1,planList,BenefitCoverageLevel.Individual,pat.PatNum);
				if(doubDeductibleUsed!=-1) {
					ins2DeductibleUsed=doubDeductibleUsed.ToString("c");
				}
				doubPending=InsPlans.GetPendingDisplay(histList,DateTime.Today,plan,patPlanNum,-1,pat.PatNum);
				if(doubPending!=-1) {
					ins2Pending=doubPending.ToString("c");
				}
				doubUsed=InsPlans.GetInsUsedDisplay(histList,DateTime.Today,plan.PlanNum,patPlanNum,-1,planList);
				if(doubUsed!=-1) {
					ins2Used=doubUsed.ToString("c");
				}
				for(int j=0;j<benefitList.Count;j++) {
					if(benefitList[j].PlanNum != plan.PlanNum) {
						continue;
					}
					if(benefitList[j].BenefitType != InsBenefitType.CoInsurance) {
						continue;
					}
					if(ins2Percentages!="") {
						ins2Percentages+=",  ";
					}
					ins2Percentages+=CovCats.GetDesc(benefitList[j].CovCatNum)+" "+benefitList[j].Percent.ToString()+"%";
				}
			}
			TreatPlan[] treatPlanList=TreatPlans.Refresh(pat.PatNum);
			TreatPlan treatPlan=null;
			string dateOfLastSavedTP="";
			string tpResponsPartyAddress="";
			string tpResponsPartyCityStZip="";
			string tpResponsPartyNameFL="";
			if(treatPlanList.Length>0){
				treatPlan=treatPlanList[treatPlanList.Length-1].Copy();
				dateOfLastSavedTP=treatPlan.DateTP.ToShortDateString();
				Patient patRespParty=Patients.GetPat(treatPlan.ResponsParty);
				if(patRespParty!=null){
					tpResponsPartyAddress=patRespParty.Address;
					if(patRespParty.Address2!=""){
						tpResponsPartyAddress+=", "+patRespParty.Address2;
					}
					tpResponsPartyCityStZip=patRespParty.City+", "+patRespParty.State+"  "+patRespParty.Zip;
					tpResponsPartyNameFL=patRespParty.GetNameFL();
				}
			}
			Recall recall=Recalls.GetRecallProphyOrPerio(pat.PatNum);
			string dateRecallDue="";
			string recallInterval="";
			if(recall!=null){
				if(recall.DateDue.Year>1880){
					dateRecallDue=recall.DateDue.ToShortDateString();
				}
				recallInterval=recall.RecallInterval.ToString();
			}
			List<Appointment> apptList=Appointments.GetListForPat(pat.PatNum);
			string nextSchedApptDateT="";
			string dateTimeLastAppt="";
			for(int i=0;i<apptList.Count;i++) {
				if(apptList[i].AptStatus != ApptStatus.Scheduled
					&& apptList[i].AptStatus != ApptStatus.Complete
					&& apptList[i].AptStatus != ApptStatus.None
					&& apptList[i].AptStatus != ApptStatus.ASAP) 
				{
					continue;
				}
				if(apptList[i].AptDateTime < DateTime.Now) {
					//this will happen repeatedly up until the most recent.
					dateTimeLastAppt=apptList[i].AptDateTime.ToShortDateString()+"  "+apptList[i].AptDateTime.ToShortTimeString();
				}
				else {//after now
					if(nextSchedApptDateT=="") {//only the first one found
						nextSchedApptDateT=apptList[i].AptDateTime.ToShortDateString()+"  "+apptList[i].AptDateTime.ToShortTimeString();
						break;//we're done with the list now.
					}
				}
			}
			Provider priProv=Providers.GetProv(Patients.GetProvNum(pat));//guaranteed to work
			Clinic clinic=new Clinic();
			string clinicAddress="";
			string clinicPhone="";
			if(pat.ClinicNum != 0){
				clinic=Clinics.GetClinic(pat.ClinicNum);
				clinicAddress=clinic.Address;
				if(clinic.Address2!=""){
					clinicAddress+=", "+clinic.Address2;
				}
				if(clinic.Phone.Length==10){
					clinicPhone="("+clinic.Phone.Substring(0,3)+")"
						+clinic.Phone.Substring(3,3)+"-"
						+clinic.Phone.Substring(6);
				}
			}
			foreach(SheetField field in sheet.SheetFields) {
				if(field.FieldType!=SheetFieldType.StaticText) {
					continue;
				}
				fldval=field.FieldValue;
				fldval=fldval.Replace("[address]",address);
				fldval=fldval.Replace("[age]",Patients.AgeToString(pat.Age));
				fldval=fldval.Replace("[balTotal]",fam.ListPats[0].BalTotal.ToString("c"));
				fldval=fldval.Replace("[bal_0_30]",fam.ListPats[0].Bal_0_30.ToString("c"));
				fldval=fldval.Replace("[bal_31_60]",fam.ListPats[0].Bal_31_60.ToString("c"));
				fldval=fldval.Replace("[bal_61_90]",fam.ListPats[0].Bal_61_90.ToString("c"));
				fldval=fldval.Replace("[balOver90]",fam.ListPats[0].BalOver90.ToString("c"));
				fldval=fldval.Replace("[balInsEst]",fam.ListPats[0].InsEst.ToString("c"));
				fldval=fldval.Replace("[balTotalMinusInsEst]",(fam.ListPats[0].BalTotal-fam.ListPats[0].InsEst).ToString("c"));
				fldval=fldval.Replace("[BillingType]",DefC.GetName(DefCat.BillingTypes,pat.BillingType));
				fldval=fldval.Replace("[Birthdate]",birthdate);
				fldval=fldval.Replace("[carrierName]",carrierName);
				fldval=fldval.Replace("[carrier2Name]",carrier2Name);
				fldval=fldval.Replace("[ChartNumber]",pat.ChartNumber);
				fldval=fldval.Replace("[carrierAddress]",carrierAddress);
				fldval=fldval.Replace("[carrierCityStZip]",carrierCityStZip);
				fldval=fldval.Replace("[cityStateZip]",pat.City+", "+pat.State+"  "+pat.Zip);
				fldval=fldval.Replace("[clinicDescription]",clinic.Description);
				fldval=fldval.Replace("[clinicAddress]",clinicAddress);
				fldval=fldval.Replace("[clinicCityStZip]",clinic.City+", "+clinic.State+"  "+clinic.Zip);
				fldval=fldval.Replace("[clinicPhone]",clinicPhone);
				fldval=fldval.Replace("[DateFirstVisit]",dateFirstVisit);
				fldval=fldval.Replace("[dateOfLastSavedTP]",dateOfLastSavedTP);
				fldval=fldval.Replace("[dateRecallDue]",dateRecallDue);
				fldval=fldval.Replace("[dateTimeLastAppt]",dateTimeLastAppt);
				fldval=fldval.Replace("[dateToday]",DateTime.Today.ToShortDateString());
				fldval=fldval.Replace("[Email]",pat.Email);
				fldval=fldval.Replace("[famFinUrgNote]",fam.ListPats[0].FamFinUrgNote);
				fldval=fldval.Replace("[guarantorNameFL]",fam.ListPats[0].GetNameFL());
				fldval=fldval.Replace("[HmPhone]",StripPhoneBeyondSpace(pat.HmPhone));
				fldval=fldval.Replace("[insAnnualMax]",insAnnualMax);
				fldval=fldval.Replace("[insDeductible]",insDeductible);
				fldval=fldval.Replace("[insDeductibleUsed]",insDeductibleUsed);
				fldval=fldval.Replace("[insPending]",insPending);
				fldval=fldval.Replace("[insPercentages]",insPercentages);
				fldval=fldval.Replace("[insUsed]",insUsed);
				fldval=fldval.Replace("[ins2AnnualMax]",ins2AnnualMax);
				fldval=fldval.Replace("[ins2Deductible]",ins2Deductible);
				fldval=fldval.Replace("[ins2DeductibleUsed]",ins2DeductibleUsed);
				fldval=fldval.Replace("[ins2Pending]",ins2Pending);
				fldval=fldval.Replace("[ins2Percentages]",ins2Percentages);
				fldval=fldval.Replace("[ins2Used]",ins2Used);
				fldval=fldval.Replace("[MedUrgNote]",pat.MedUrgNote);
				fldval=fldval.Replace("[nameF]",pat.FName);
				fldval=fldval.Replace("[nameFL]",pat.GetNameFL());
				fldval=fldval.Replace("[nameFLFormal]",pat.GetNameFLFormal());
				fldval=fldval.Replace("[nameL]",pat.LName);
				fldval=fldval.Replace("[nameLF]",pat.GetNameLF());
				fldval=fldval.Replace("[nextSchedApptDateT]",nextSchedApptDateT);
				fldval=fldval.Replace("[PatNum]",pat.PatNum.ToString());
				fldval=fldval.Replace("[priProvNameFormal]",priProv.GetFormalName());
				fldval=fldval.Replace("[recallInterval]",recallInterval);
				fldval=fldval.Replace("[salutation]",pat.GetSalutation());
				fldval=fldval.Replace("[siteDescription]",Sites.GetDescription(pat.SiteNum));
				fldval=fldval.Replace("[subscriberID]",subscriberId);
				fldval=fldval.Replace("[subscriberNameFL]",subscriberNameFL);
				fldval=fldval.Replace("[subscriber2NameFL]",subscriber2NameFL);
				fldval=fldval.Replace("[tpResponsPartyAddress]",tpResponsPartyAddress);
				fldval=fldval.Replace("[tpResponsPartyCityStZip]",tpResponsPartyCityStZip);
				fldval=fldval.Replace("[tpResponsPartyNameFL]",tpResponsPartyNameFL);
				fldval=fldval.Replace("[treatmentPlanProcs]",treatmentPlanProcs);
				fldval=fldval.Replace("[WirelessPhone]",StripPhoneBeyondSpace(pat.WirelessPhone));
				fldval=fldval.Replace("[WkPhone]",StripPhoneBeyondSpace(pat.WkPhone));
				field.FieldValue=fldval;
			}
		}

		private static string StripPhoneBeyondSpace(string phone) {
			if(!phone.Contains(" ")) {
				return phone;
			}
			int idx=phone.IndexOf(" ");
			return phone.Substring(0,idx);
		}

		private static void FillPatientImages(Sheet sheet,Patient pat){
			if(pat==null){
				return;
			}
			Document[] docList=Documents.GetAllWithPat(pat.PatNum);
			long category;
			//string fieldVal;//zoom and pan
			//int x;
			//int y;
			//int w;
			//int h;
			float ratioObject;
			//float ratioImage;
			Image img;
			foreach(SheetField field in sheet.SheetFields){
				if(field.FieldType!=SheetFieldType.PatImage){
					continue;
				}
				category=PIn.Long(field.FieldName);
				field.FieldName="0";//in case we can't find an image, this will be 0.
				field.FieldValue="";
				//go backwards to find the latest date
				for(int i=docList.Length-1;i>=0;i--){
					if(docList[i].DocCategory!=category){
						continue;
					}
					field.FieldName=docList[i].DocNum.ToString();
					ratioObject=(float)field.Width/(float)field.Height;
					img=Image.FromFile(  docList[i].FileName);
					//ratioImage=(float)docList[i].wid  field.Width/(float)field.Height;
//incomplete

					field.FieldValue="";
					break;
				}
			}
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
					case "text":
						field.FieldValue=GetParamByName(sheet,"text").ParamValue.ToString();
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

		private static void FillFieldsForReferralSlip(Sheet sheet,Patient pat,Referral refer) {
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
					case "referral.phone2":
						field.FieldValue=refer.Phone2;
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

		private static void FillFieldsForLabelAppointment(Sheet sheet,Appointment appt,Patient pat) {
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

		private static void FillFieldsForRx(Sheet sheet,RxPat rx,Patient pat,Provider prov) {
			Clinic clinic=null;
			if(pat.ClinicNum != 0) {
				clinic=Clinics.GetClinic(pat.ClinicNum);
			}
			string text;
			foreach(SheetField field in sheet.SheetFields) {
				switch(field.FieldName) {
					case "prov.nameFL":
						field.FieldValue=prov.GetFormalName();
						break;
					case "prov.address":
						if(clinic==null) {
							field.FieldValue=PrefC.GetString(PrefName.PracticeAddress);
							if(PrefC.GetString(PrefName.PracticeAddress2)!="") {
								field.FieldValue+="\r\n"+PrefC.GetString(PrefName.PracticeAddress2);
							}
						}
						else{
							field.FieldValue=clinic.Address;
							if(clinic.Address2!="") {
								field.FieldValue+="\r\n"+clinic.Address2;
							}
						}
						break;
					case "prov.cityStateZip":
						if(clinic==null) {
							field.FieldValue=PrefC.GetString(PrefName.PracticeCity)+", "+PrefC.GetString(PrefName.PracticeST)+" "+PrefC.GetString(PrefName.PracticeZip);
						}
						else {
							field.FieldValue=clinic.City+", "+clinic.State+" "+clinic.Zip;
						}
						break;
					case "prov.phone":
						if(clinic==null) {
							text=PrefC.GetString(PrefName.PracticePhone);
						}
						else {
							text=clinic.Phone;
						}
						field.FieldValue=text;
						if(text.Length==10) {
							field.FieldValue="("+text.Substring(0,3)+")"+text.Substring(3,3)+"-"+text.Substring(6);
						}
						break;
					case "RxDate":
						field.FieldValue=rx.RxDate.ToShortDateString();
						break;
					case "RxDateMonthSpelled":
						field.FieldValue=rx.RxDate.ToString("MMM dd,yyyy");
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

		private static void FillFieldsForConsent(Sheet sheet,Patient pat) {
			foreach(SheetField field in sheet.SheetFields) {
				switch(field.FieldName) {
					case "patient.nameFL":
						field.FieldValue=pat.GetNameFL();
						break;
					case "dateTime.Today":
						field.FieldValue=DateTime.Today.ToShortDateString();
						break;
				}
			}
		}

		private static void FillFieldsForPatientLetter(Sheet sheet,Patient pat) {
			foreach(SheetField field in sheet.SheetFields) {
				switch(field.FieldName) {
					case "PracticeTitle":
						field.FieldValue=PrefC.GetString(PrefName.PracticeTitle);
						break;
					case "PracticeAddress":
						field.FieldValue=PrefC.GetString(PrefName.PracticeAddress);
						if(PrefC.GetString(PrefName.PracticeAddress2) != ""){
							field.FieldValue+="\r\n"+PrefC.GetString(PrefName.PracticeAddress2);
						}
						break;
					case "practiceCityStateZip":
						field.FieldValue=PrefC.GetString(PrefName.PracticeCity)+", "
							+PrefC.GetString(PrefName.PracticeST)+"  "
							+PrefC.GetString(PrefName.PracticeZip);
						break;
					case "patient.nameFL":
						field.FieldValue=pat.GetNameFLFormal();
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
					case "today.DayDate":
						field.FieldValue=DateTime.Today.ToString("dddd")+", "+DateTime.Today.ToShortDateString();
						break;
					case "patient.salutation":
						field.FieldValue="Dear "+pat.GetSalutation()+":";
						break;
					case "patient.priProvNameFL":
						field.FieldValue=Providers.GetFormalName(pat.PriProv);
						break;
				}
			}
		}

		private static void FillFieldsForReferralLetter(Sheet sheet,Patient pat,Referral refer) {
			foreach(SheetField field in sheet.SheetFields) {
				switch(field.FieldName) {
					case "PracticeTitle":
						field.FieldValue=PrefC.GetString(PrefName.PracticeTitle);
						break;
					case "PracticeAddress":
						field.FieldValue=PrefC.GetString(PrefName.PracticeAddress);
						if(PrefC.GetString(PrefName.PracticeAddress2) != ""){
							field.FieldValue+="\r\n"+PrefC.GetString(PrefName.PracticeAddress2);
						}
						break;
					case "practiceCityStateZip":
						field.FieldValue=PrefC.GetString(PrefName.PracticeCity)+", "
							+PrefC.GetString(PrefName.PracticeST)+"  "
							+PrefC.GetString(PrefName.PracticeZip);
						break;
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
					case "today.DayDate":
						field.FieldValue=DateTime.Today.ToString("dddd")+", "+DateTime.Today.ToShortDateString();
						break;
					case "patient.nameFL":
						field.FieldValue=pat.GetNameFL();
						break;
					case "referral.salutation":
						field.FieldValue="Dear "+refer.FName+":";
						break;
					case "patient.priProvNameFL":
						field.FieldValue=Providers.GetFormalName(pat.PriProv);
						break;
				}
			}
		}

		private static void FillFieldsForPatientForm(Sheet sheet,Patient pat) {
			Family fam=Patients.GetFamily(pat.PatNum);
			List<PatPlan> patPlanList=PatPlans.Refresh(pat.PatNum);
			List<InsPlan> planList=InsPlans.Refresh(fam);
			InsPlan insplan1=null;
			Carrier carrier1=null;
			if(patPlanList.Count>0){
				insplan1=InsPlans.GetPlan(patPlanList[0].PlanNum,planList);
				carrier1=Carriers.GetCarrier(insplan1.CarrierNum);
			}
			InsPlan insplan2=null;
			Carrier carrier2=null;
			if(patPlanList.Count>1) {
				insplan2=InsPlans.GetPlan(patPlanList[1].PlanNum,planList);
				carrier2=Carriers.GetCarrier(insplan2.CarrierNum);
			}
			foreach(SheetField field in sheet.SheetFields) {
				switch(field.FieldName) {
					case "Address":
						field.FieldValue=pat.Address;
						break;
					case "Address2":
						field.FieldValue=pat.Address2;
						break;
					case "addressAndHmPhoneIsSameEntireFamily":
						bool isSame=true;
						for(int i=0;i<fam.ListPats.Length;i++){
							if(pat.HmPhone!=fam.ListPats[i].HmPhone
								|| pat.Address!=fam.ListPats[i].Address
								|| pat.Address2!=fam.ListPats[i].Address2
								|| pat.City!=fam.ListPats[i].City
								|| pat.State!=fam.ListPats[i].State
								|| pat.Zip!=fam.ListPats[i].Zip)
							{
								isSame=false;
								break;
							}
						}
						if(isSame) {
							field.FieldValue="X";
						}
						break;
					case "Birthdate":
						field.FieldValue=pat.Birthdate.ToShortDateString();
						break;
					case "City":
						field.FieldValue=pat.City;
						break;
					case "Email":
						field.FieldValue=pat.Email;
						break;
					case "FName":
						field.FieldValue=pat.FName;
						break;
					case "Gender":
						if(field.RadioButtonValue==pat.Gender.ToString()) {
							field.FieldValue="X";
						}
						break;
					case "HmPhone":
						field.FieldValue=pat.HmPhone;
						break;
					case "ins1CarrierName":
						if(carrier1!=null){
							field.FieldValue=carrier1.CarrierName;
						}
						break;
					case "ins1CarrierPhone":
						if(carrier1!=null) {
							field.FieldValue=carrier1.Phone;
						}
						break;
					case "ins1EmployerName":
						if(insplan1!=null) {
							field.FieldValue=Employers.GetName(insplan1.EmployerNum);
						}
						break;
					case "ins1GroupName":
						if(insplan1!=null) {
							field.FieldValue=insplan1.GroupName;
						}
						break;
					case "ins1GroupNum":
						if(insplan1!=null) {
							field.FieldValue=insplan1.GroupNum;
						}
						break;
					case "ins1Relat":
						if(patPlanList.Count>0 && patPlanList[0].Relationship.ToString()==field.RadioButtonValue) {
							field.FieldValue="X";
						}
						break;
					case "ins1SubscriberID":
						if(insplan1!=null) {
							field.FieldValue=insplan1.SubscriberID;
						}
						break;
					case "ins1SubscriberNameF":
						if(insplan1!=null) {
							field.FieldValue=fam.GetNameInFamFirst(insplan1.Subscriber);
						}
						break;
					case "ins2CarrierName":
						if(carrier2!=null) {
							field.FieldValue=carrier2.CarrierName;
						}
						break;
					case "ins2CarrierPhone":
						if(carrier2!=null) {
							field.FieldValue=carrier2.Phone;
						}
						break;
					case "ins2EmployerName":
						if(insplan2!=null) {
							field.FieldValue=Employers.GetName(insplan2.EmployerNum);
						}
						break;
					case "ins2GroupName":
						if(insplan2!=null) {
							field.FieldValue=insplan2.GroupName;
						}
						break;
					case "ins2GroupNum":
						if(insplan2!=null) {
							field.FieldValue=insplan2.GroupNum;
						}
						break;
					case "ins2Relat":
						if(patPlanList.Count>1 && patPlanList[1].Relationship.ToString()==field.RadioButtonValue) {
							field.FieldValue="X";
						}
						break;
					case "ins2SubscriberID":
						if(insplan2!=null) {
							field.FieldValue=insplan2.SubscriberID;
						}
						break;
					case "ins2SubscriberNameF":
						if(insplan2!=null) {
							field.FieldValue=fam.GetNameInFamFirst(insplan2.Subscriber);
						}
						break;
					case "LName":
						field.FieldValue=pat.LName;
						break;
					case "MiddleI":
						field.FieldValue=pat.MiddleI;
						break;
					case "Position":
						if(pat.Position.ToString()==field.RadioButtonValue) {
							field.FieldValue="X";
						}
						break;
					case "PreferConfirmMethod":
						if(pat.PreferConfirmMethod.ToString()==field.RadioButtonValue) {
							field.FieldValue="X";
						}
						break;
					case "PreferContactMethod":
						if(pat.PreferContactMethod.ToString()==field.RadioButtonValue) {
							field.FieldValue="X";
						}
						break;
					case "PreferRecallMethod":
						if(pat.PreferRecallMethod.ToString()==field.RadioButtonValue) {
							field.FieldValue="X";
						}
						break;
					case "Preferred":
						field.FieldValue=pat.Preferred;
						break;
					case "referredFrom":
						Referral referral=Referrals.GetReferralForPat(pat.PatNum);
						if(referral!=null){
							field.FieldValue=Referrals.GetNameFL(referral.ReferralNum);
						}
						break;
					case "SSN":
						if(CultureInfo.CurrentCulture.Name=="en-US" && pat.SSN.Length==9){//and length exactly 9 (no data gets lost in formatting)
							field.FieldValue=pat.SSN.Substring(0,3)+"-"+pat.SSN.Substring(3,2)+"-"+pat.SSN.Substring(5,4);
						}
						else {
							field.FieldValue=pat.SSN;
						}
						break;
					case "State":
						field.FieldValue=pat.State;
						break;
					case "StudentStatus":
						if(pat.StudentStatus=="F" && field.RadioButtonValue=="Fulltime") {
							field.FieldValue="X";
						}
						if(pat.StudentStatus=="N" && field.RadioButtonValue=="Nonstudent") {
							field.FieldValue="X";
						}
						if(pat.StudentStatus=="P" && field.RadioButtonValue=="Parttime") {
							field.FieldValue="X";
						}

						break;
					case "WirelessPhone":
						field.FieldValue=pat.WirelessPhone;
						break;
					case "wirelessCarrier":
						field.FieldValue="";//not implemented
						break;
					case "WkPhone":
						field.FieldValue=pat.WkPhone;
						break;
					case "Zip":
						field.FieldValue=pat.Zip;
						break;
				}
			}
		}

		private static void FillFieldsForRoutingSlip(Sheet sheet,Patient pat,Appointment apt) {
			Family fam=Patients.GetFamily(apt.PatNum);
			string str;
			foreach(SheetField field in sheet.SheetFields) {
				switch(field.FieldName) {
					case "appt.timeDate":
						field.FieldValue=apt.AptDateTime.ToShortTimeString()+"  "+apt.AptDateTime.ToShortDateString();
						break;
					case "appt.length":
						field.FieldValue=(apt.Pattern.Length*5).ToString()+" "+Lan.g("SheetRoutingSlip","minutes");
						break;
					case "appt.providers":
						str=Providers.GetLongDesc(apt.ProvNum);
						if(apt.ProvHyg!=0){
							str+="\r\n"+Providers.GetLongDesc(apt.ProvHyg);
						}
						field.FieldValue=str;
						break;
					case "appt.procedures":
						str="";
						List<Procedure> procs=Procedures.GetProcsForSingle(apt.AptNum,false);
						for(int i=0;i<procs.Count;i++) {
							if(i>0){
								str+="\r\n";
							}
							str+=Procedures.GetDescription(procs[i]);
						}
						field.FieldValue=str;
						break;
					case "appt.Note":
						field.FieldValue=apt.Note;
						break;
					case "otherFamilyMembers":
						str="";
						for(int i=0;i<fam.ListPats.Length;i++) {
							if(fam.ListPats[i].PatNum==pat.PatNum) {
								continue;
							}
							if(fam.ListPats[i].PatStatus==PatientStatus.Archived
								|| fam.ListPats[i].PatStatus==PatientStatus.Deceased) {
								continue;
							}
							if(str!="") {
								str+="\r\n";
							}
							str+=fam.ListPats[i].GetNameFL();
							if(fam.ListPats[i].Age>0){
								str+=",   "+fam.ListPats[i].Age.ToString();
							}
						}
						field.FieldValue=str;
						break;
				}
			}
		}

		private static void FillFieldsForMedicalHistory(Sheet sheet,Patient pat) {
			/*There aren't any fields for medical history yet
			foreach(SheetField field in sheet.SheetFields) {
				switch(field.FieldName) {
					case "appt.timeDate":
						field.FieldValue=apt.AptDateTime.ToShortTimeString()+"  "+apt.AptDateTime.ToShortDateString();
						break;
				}
			}*/
		}




	}
}
