using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using OpenDentBusiness;
using OpenDental;
using OpenDental.UI;
using System.Diagnostics;

namespace EHR {
	public partial class FormVitalsignEdit2014:Form {
		public Vitalsign VitalsignCur;
		private Patient patCur;
		public int ageBeforeJanFirst;
		private List<Loinc> listHeightCodes;
		private List<Loinc> listWeightCodes;
		private List<Loinc> listBMICodes;
		private long disdefNumCur;//used to keep track of the def we will update VitalsignCur.PregDiseaseNum with
		private string pregDefaultText;
		private string pregManualText;
		private InterventionCodeSet intervCodeSet;

		public FormVitalsignEdit2014() {
			InitializeComponent();
		}

		private void FormVitalsignEdit2014_Load(object sender,EventArgs e) {
			pregDefaultText="A diagnosis of pregnancy with this code will be added to the patient's medical history with a start date equal to the date of this exam.";
			pregManualText="Selecting a code that is not in the recommended list of pregnancy codes may not exclude this patient from certain CQM calculations.";
			labelPregNotice.Text=pregDefaultText;
			#region SetHWBPAndVisibility
			disdefNumCur=0;
			groupInterventions.Visible=false;
			patCur=Patients.GetPat(VitalsignCur.PatNum);
			textDateTaken.Text=VitalsignCur.DateTaken.ToShortDateString();
			ageBeforeJanFirst=PIn.Date(textDateTaken.Text).Year-patCur.Birthdate.Year-1;
			textHeight.Text=VitalsignCur.Height.ToString();
			textWeight.Text=VitalsignCur.Weight.ToString();
			CalcBMI();
			textBPd.Text=VitalsignCur.BpDiastolic.ToString();
			textBPs.Text=VitalsignCur.BpSystolic.ToString();
			#endregion
			#region SetBMIHeightWeightExamCodes
			listHeightCodes=new List<Loinc>();
			listWeightCodes=new List<Loinc>();
			listBMICodes=new List<Loinc>();
			FillBMICodeLists();
			for(int i=0;i<listBMICodes.Count;i++) {
				if(listBMICodes[i].NameShort=="" || listBMICodes[i].NameLongCommon.Length<30) {//30 is roughly the number of characters that will fit in the combo box
					comboBMIPercentileCode.Items.Add(listBMICodes[i].NameLongCommon);
				}
				else {
					comboBMIPercentileCode.Items.Add(listBMICodes[i].NameShort);
				}
				if(VitalsignCur.BMIExamCode==listBMICodes[i].LoincCode) {
					comboBMIPercentileCode.SelectedIndex=i;
				}
			}
			for(int i=0;i<listHeightCodes.Count;i++) {
				if(listHeightCodes[i].NameShort=="" || listHeightCodes[i].NameLongCommon.Length<30) {//30 is roughly the number of characters that will fit in the combo box
					comboHeightExamCode.Items.Add(listHeightCodes[i].NameLongCommon);
				}
				else {
					comboHeightExamCode.Items.Add(listHeightCodes[i].NameShort);
				}
				if(VitalsignCur.HeightExamCode==listHeightCodes[i].LoincCode) {
					comboHeightExamCode.SelectedIndex=i;
				}
			}
			for(int i=0;i<listWeightCodes.Count;i++) {
				if(listWeightCodes[i].NameShort=="" || listWeightCodes[i].NameLongCommon.Length<30) {//30 is roughly the number of characters that will fit in the combo box
					comboWeightExamCode.Items.Add(listWeightCodes[i].NameLongCommon);
				}
				else {
					comboWeightExamCode.Items.Add(listWeightCodes[i].NameShort);
				}
				if(VitalsignCur.WeightExamCode==listWeightCodes[i].LoincCode) {
					comboWeightExamCode.SelectedIndex=i;
				}
			}
			if(comboBMIPercentileCode.SelectedIndex==-1) {
				comboBMIPercentileCode.SelectedIndex=0;
			}
			if(comboHeightExamCode.SelectedIndex==-1) {
				comboHeightExamCode.SelectedIndex=0;
			}
			if(comboWeightExamCode.SelectedIndex==-1) {
				comboWeightExamCode.SelectedIndex=0;
			}
			#endregion
			#region SetPregCodeAndDescript
			if(VitalsignCur.PregDiseaseNum>0) {
				checkPregnant.Checked=true;
				SetPregCodeAndDescript();//if after this function disdefNumCur=0, the PregDiseaseNum is pointing to an invalid disease or diseasedef, or the default is set to 'none'
				if(disdefNumCur>0) {
					labelPregNotice.Visible=true;
					butChangeDefault.Text="Go to Problem";
				}
			}
			else {
				disdefNumCur=0;
			}
			#endregion
			#region SetEhrNotPerformedReasonCodeAndDescript
			if(VitalsignCur.EhrNotPerformedNum>0) {
				checkNotPerf.Checked=true;
				EhrNotPerformed enpCur=EhrNotPerformeds.GetOne(VitalsignCur.EhrNotPerformedNum);
				if(enpCur==null) {
					VitalsignCur.EhrNotPerformedNum=0;//if this vital sign is pointing to an EhrNotPerformed item that no longer exists, we will just remove the pointer
					checkNotPerf.Checked=false;
				}
				else {
					textReasonCode.Text=enpCur.CodeValueReason;
					//all reasons not performed are snomed codes
					Snomed sCur=Snomeds.GetByCode(enpCur.CodeValueReason);
					if(sCur!=null) {
						textReasonDescript.Text=sCur.Description;
					}
				}
			}
			#endregion
			#region SetInterventionCodeAndDescript
			//if(VitalsignCur.OverUnderInterventionNum>0) {
			//	Intervention iCur=Interventions.GetOne(VitalsignCur.OverUnderInterventionNum);
			//	if(iCur==null) {
			//		VitalsignCur.OverUnderInterventionNum=0;
			//	}
			//	else {
			//		textIntervenCode.Text=iCur.CodeValue;
			//		string descript="";
			//		switch(iCur.CodeSystem) {//interventions can be SNOMEDCT, ICD9CM, ICD10CM, HCPCS, or CPT codes
			//			case "SNOMEDCT":
			//				Snomed sCur=Snomeds.GetByCode(iCur.CodeValue);
			//				if(sCur!=null) {
			//					descript=sCur.Description;
			//				}
			//				break;
			//			case "ICD9CM":
			//				ICD9 i9Cur=ICD9s.GetByCode(iCur.CodeValue);
			//				if(i9Cur!=null) {
			//					descript=i9Cur.Description;
			//				}
			//				break;
			//			case "ICD10CM":
			//				Icd10 i10Cur=Icd10s.GetByCode(iCur.CodeValue);
			//				if(i10Cur!=null) {
			//					descript=i10Cur.Description;
			//				}
			//				break;
			//			case "HCPCS":
			//				Hcpcs hCur=Hcpcses.GetByCode(iCur.CodeValue);
			//				if(hCur!=null) {
			//					descript=hCur.DescriptionShort;
			//				}
			//				break;
			//			case "CPT":
			//				//no need to check for null, return new ProcedureCode object if not found, Descript will be blank
			//				descript=ProcedureCodes.GetProcCode(iCur.CodeValue).Descript;
			//				break;
			//		}
			//		textIntervenDescript.Text=descript;
			//	}
			//}
			#endregion
		}

		///<summary>Sets the pregnancy code and description text box with either the attached pregnancy dx if exists or the default preg dx set in FormEhrSettings or a manually selected def.  If the pregnancy diseasedef with the default pregnancy code and code system does not exist, it will be inserted.  The pregnancy problem will be inserted when closing if necessary.</summary>
		private void SetPregCodeAndDescript() {
			labelPregNotice.Text=pregDefaultText;
			disdefNumCur=0;//this will be set to the correct problem def at the end of this function and will be the def of the problem we will insert/attach this exam to
			string pregCode="";
			string descript="";
			Disease disCur=null;
			DiseaseDef disdefCur=null;
			DateTime examDate=PIn.Date(textDateTaken.Text);//this may be different than the saved Vitalsign.DateTaken if user edited
			#region Get DiseaseDefNum from attached pregnancy problem
			if(VitalsignCur.PregDiseaseNum>0) {//already pointing to a disease, get that one
				disCur=Diseases.GetOne(VitalsignCur.PregDiseaseNum);//get disease this vital sign is pointing to, see if it exists
				if(disCur==null) {
					VitalsignCur.PregDiseaseNum=0;
				}
				else {
					if(examDate.Year<1880 || disCur.DateStart>examDate.Date || (disCur.DateStop.Year>1880 && disCur.DateStop<examDate.Date)) {
						VitalsignCur.PregDiseaseNum=0;
						disCur=null;
					}
					else {
						disdefCur=DiseaseDefs.GetItem(disCur.DiseaseDefNum);
						if(disdefCur==null) {
							VitalsignCur.PregDiseaseNum=0;
							disCur=null;
						}
						else {//disease points to valid def
							disdefNumCur=disdefCur.DiseaseDefNum;
						}
					}
				}
			}
			#endregion
			if(VitalsignCur.PregDiseaseNum==0) {//not currently attached to a disease
				#region Get DiseaseDefNum from existing pregnancy problem
				if(examDate.Year>1880) {//only try to find existing problem if a valid exam date is entered before checking the box, otherwise we do not know what date to compare to the active dates of the pregnancy dx
					List<DiseaseDef> listPregDisDefs=DiseaseDefs.GetAllPregDiseaseDefs();
					List<Disease> listPatDiseases=Diseases.Refresh(VitalsignCur.PatNum,true);
					for(int i=0;i<listPatDiseases.Count;i++) {//loop through all diseases for this patient, shouldn't be very many
						if(listPatDiseases[i].DateStart>examDate.Date //startdate for current disease is after the exam date set in form
							|| (listPatDiseases[i].DateStop.Year>1880 && listPatDiseases[i].DateStop<examDate.Date))//or current disease has a stop date and stop date before exam date
						{
							continue;
						}
						for(int j=0;j<listPregDisDefs.Count;j++) {//loop through preg disease defs in the db, shouldn't be very many
							if(listPatDiseases[i].DiseaseDefNum!=listPregDisDefs[j].DiseaseDefNum) {//see if this problem is a pregnancy problem
								continue;
							}
							if(disCur==null || listPatDiseases[i].DateStart>disCur.DateStart) {//if we haven't found a disease match yet or this match is more recent (later start date)
								disCur=listPatDiseases[i];
								break;
							}
						}
					}
				}
				if(disCur!=null) {
					disdefNumCur=disCur.DiseaseDefNum;
					VitalsignCur.PregDiseaseNum=disCur.DiseaseNum;
				}
				#endregion
				else {//we are going to insert either the default pregnancy problem or a manually selected problem
					#region Get DiseaseDefNum from global default pregnancy problem
					//if preg dx doesn't exist, use the default pregnancy code if set to something other than blank or 'none'
					pregCode=PrefC.GetString(PrefName.PregnancyDefaultCodeValue);//could be 'none' which disables the automatic dx insertion
					string pregCodeSys=PrefC.GetString(PrefName.PregnancyDefaultCodeSystem);//if 'none' for code, code system will default to 'SNOMEDCT', display will be ""
					if(pregCode!="" && pregCode!="none") {//default pregnancy code set to a code other than 'none', should never be blank, we set in ConvertDB and don't allow blank
						disdefNumCur=DiseaseDefs.GetNumFromCode(pregCode);//see if the code is attached to a valid diseasedef
						if(disdefNumCur==0) {//no diseasedef in db for the default code, create and insert def
							disdefCur=new DiseaseDef();
							disdefCur.DiseaseName="Pregnant";
							switch(pregCodeSys) {
								case "ICD9CM":
									disdefCur.ICD9Code=pregCode;
									break;
								case "ICD10CM":
									disdefCur.Icd10Code=pregCode;
									break;
								case "SNOMEDCT":
									disdefCur.SnomedCode=pregCode;
									break;
							}
							disdefNumCur=DiseaseDefs.Insert(disdefCur);
							DiseaseDefs.RefreshCache();
							DataValid.SetInvalid(InvalidType.Diseases);
							SecurityLogs.MakeLogEntry(Permissions.ProblemEdit,0,disdefCur.DiseaseName+" added.");
						}
					}
					#endregion
					#region Get DiseaseDefNum from manually selected pregnancy problem
					else if(pregCode=="none") {//if pref for default preg dx is 'none', make user choose a problem from list
						FormDiseaseDefs FormDD=new FormDiseaseDefs();
						FormDD.IsSelectionMode=true;
						FormDD.IsMultiSelect=false;
						FormDD.ShowDialog();
						if(FormDD.DialogResult!=DialogResult.OK) {
							checkPregnant.Checked=false;
							textPregCode.Clear();
							textPregCodeDescript.Clear();
							labelPregNotice.Visible=false;
							butChangeDefault.Text="Change Default";
							return;
						}
						labelPregNotice.Text=pregManualText;
						disdefNumCur=FormDD.SelectedDiseaseDefNum;
					}
					#endregion
				}
			}
			#region Set description and code from DiseaseDefNum
			if(disdefNumCur==0) {
				textPregCode.Clear();
				textPregCodeDescript.Clear();
				labelPregNotice.Visible=false;
				return;
			}
			disdefCur=DiseaseDefs.GetItem(disdefNumCur);
			if(disdefCur.ICD9Code!="") {
				ICD9 i9Preg=ICD9s.GetByCode(disdefCur.ICD9Code);
				if(i9Preg!=null) {
					pregCode=i9Preg.ICD9Code;
					descript=i9Preg.Description;
				}
			}
			else if(disdefCur.Icd10Code!="") {
				Icd10 i10Preg=Icd10s.GetByCode(disdefCur.Icd10Code);
				if(i10Preg!=null) {
					pregCode=i10Preg.Icd10Code;
					descript=i10Preg.Description;
				}
			}
			else if(disdefCur.SnomedCode!="") {
				Snomed sPreg=Snomeds.GetByCode(disdefCur.SnomedCode);
				if(sPreg!=null) {
					pregCode=sPreg.SnomedCode;
					descript=sPreg.Description;
				}
			}
			if(pregCode=="none" || pregCode=="") {
				descript=disdefCur.DiseaseName;
			}
			#endregion
			textPregCode.Text=pregCode;
			textPregCodeDescript.Text=descript;
		}

		private void FillBMICodeLists() {
			listBMICodes.Add(Loincs.GetByCode("59574-4"));//Body mass index (BMI) [Percentile]
			listBMICodes.Add(Loincs.GetByCode("59575-1"));//Body mass index (BMI) [Percentile] Per age
			listBMICodes.Add(Loincs.GetByCode("59576-9"));//Body mass index (BMI) [Percentile] Per age and gender
			listHeightCodes.Add(Loincs.GetByCode("8302-2"));//Body height
			listHeightCodes.Add(Loincs.GetByCode("3137-7"));//Body height Measured
			listHeightCodes.Add(Loincs.GetByCode("3138-5"));//Body height Stated
			listHeightCodes.Add(Loincs.GetByCode("8306-3"));//Body height --lying
			listHeightCodes.Add(Loincs.GetByCode("8307-1"));//Body height --pre surgery
			listHeightCodes.Add(Loincs.GetByCode("8308-9"));//Body height --standing
			listWeightCodes.Add(Loincs.GetByCode("29463-7"));//Body weight
			listWeightCodes.Add(Loincs.GetByCode("18833-4"));//First Body weight
			listWeightCodes.Add(Loincs.GetByCode("3141-9"));//Body weight Measured
			listWeightCodes.Add(Loincs.GetByCode("3142-7"));//Body weight Stated
			listWeightCodes.Add(Loincs.GetByCode("8350-1"));//Body weight Measured --with clothes
			listWeightCodes.Add(Loincs.GetByCode("8351-9"));//Body weight Measured --without clothes
		}

		private void CalcBMI() {
			labelWeightCode.Text="";
			//BMI = (lbs*703)/(in^2)
			float height;
			float weight;
			try {
				height = float.Parse(textHeight.Text);
				weight = float.Parse(textWeight.Text);
			}
			catch {
				textBMIExamCode.Text="";
				return;
			}
			if(height==0) {
				textBMIExamCode.Text="";
				return;
			}
			if(weight==0) {
				textBMIExamCode.Text="";
				return;
			}
			float bmi=Vitalsigns.CalcBMI(weight,height);// ((float)(weight*703)/(height*height));
			textBMI.Text=bmi.ToString("n1");
			labelWeightCode.Text=calcOverUnderBMIHelper(bmi);
			groupInterventions.Visible=false;
			if(ageBeforeJanFirst<17) {
				groupInterventions.Visible=true;
				groupInterventions.Text="Child Counseling for Nutrition and Physical Activity";
				FillGridInterventions(true);
			}
			else if(ageBeforeJanFirst>=18) {
				if(labelWeightCode.Text!="") {//if over 18 and given an over/underweight code due to BMI
					groupInterventions.Visible=true;
					groupInterventions.Text="Intervention for BMI Above or Below Normal";
					FillGridInterventions(false);
				}
			}
			textBMIExamCode.Text="LOINC 39156-5";//This is the only code allowed for the BMI procedure
			return;
		}

		private void FillGridInterventions(bool isChild) {
			DateTime examDate=PIn.Date(textDateTaken.Text);//this may be different than the saved VitalsignCur.DateTaken if user edited and has not hit ok to save
			#region GetInterventionsThatApply
			List<Intervention> listIntervention=Interventions.Refresh(VitalsignCur.PatNum);
			if(listIntervention.Count>0) {
				List<InterventionCodeSet> listCodeSets;
				if(isChild) {
					listCodeSets=new List<InterventionCodeSet> { InterventionCodeSet.Nutrition,InterventionCodeSet.PhysicalActivity };//Counseling for Nutrition or Physical Activity
				}
				else {
					listCodeSets=new List<InterventionCodeSet> { InterventionCodeSet.AboveNormalWeight,InterventionCodeSet.BelowNormalWeight };//Above Normal, Referral, Below Normal Interventions
				}
				for(int i=listIntervention.Count-1;i>-1;i--) {
					if(listCodeSets.Contains(listIntervention[i].CodeSet)) {
						if(listIntervention[i].DateTimeEntry.Date>examDate.AddMonths(-6).Date && listIntervention[i].DateTimeEntry.Date<=examDate.Date) {
							continue;
						}
					}
					listIntervention.RemoveAt(i);
				}
			}
			#endregion
			#region GetMedicationOrdersThatApply
			List<MedicationPat> listMedPats=new List<MedicationPat>();
			if(!isChild) {
				listMedPats=MedicationPats.Refresh(VitalsignCur.PatNum,true);
				for(int i=listMedPats.Count-1;i>-1;i--) {
					if(listMedPats[i].DateStart.Date>examDate.AddMonths(-6).Date && listMedPats[i].DateStart.Date<=examDate.Date) {
						continue;
					}
					listMedPats.RemoveAt(i);
				}
				//if still meds that have start date within exam date and exam date -6 months, check the rxnorm against valid ehr meds
				if(listMedPats.Count>0) {
					List<string> listOIDs=new List<string> { "2.16.840.1.113883.3.600.1.1498","2.16.840.1.113883.3.600.1.1499" };//Above Normal Medications RxNorm Value Set, Below Normal Medications RxNorm Value Set
					List<EhrCode> listEhrMeds=EhrCodes.GetForValueSetOIDs(listOIDs);//currently only 7 medications for above/below normal weight
					for(int i=listMedPats.Count-1;i>-1;i--) {
						bool found=false;
						for(int j=0;j<listEhrMeds.Count;j++) {
							if(listMedPats[i].RxCui.ToString()==listEhrMeds[j].CodeValue) {
								found=true;
								break;
							}
						}
						if(found) {
							continue;
						}
						listMedPats.RemoveAt(i);
					}
				}
			}
			#endregion
			gridInterventions.BeginUpdate();
			gridInterventions.Columns.Clear();
			ODGridColumn col=new ODGridColumn("Date",70);
			gridInterventions.Columns.Add(col);
			col=new ODGridColumn("Intervention Type",115);
			gridInterventions.Columns.Add(col);
			col=new ODGridColumn("Code Description",200);
			gridInterventions.Columns.Add(col);
			gridInterventions.Rows.Clear();
			ODGridRow row;
#region AddInterventionRows
			for(int i=0;i<listIntervention.Count;i++) {
				row=new ODGridRow();
				row.Cells.Add(listIntervention[i].DateTimeEntry.ToShortDateString());
				row.Cells.Add(listIntervention[i].CodeSet.ToString());
				//Description of Intervention---------------------------------------------
				//to get description, first determine which table the code is from.  Interventions are allowed to be SNOMEDCT, ICD9, ICD10, HCPCS, or CPT.
				string descript="";
				switch(listIntervention[i].CodeSystem) {
					case "SNOMEDCT":
						Snomed sCur=Snomeds.GetByCode(listIntervention[i].CodeValue);
						if(sCur!=null) {
							descript=sCur.Description;
						}
						break;
					case "ICD9CM":
						ICD9 i9Cur=ICD9s.GetByCode(listIntervention[i].CodeValue);
						if(i9Cur!=null) {
							descript=i9Cur.Description;
						}
						break;
					case "ICD10CM":
						Icd10 i10Cur=Icd10s.GetByCode(listIntervention[i].CodeValue);
						if(i10Cur!=null) {
							descript=i10Cur.Description;
						}
						break;
					case "HCPCS":
						Hcpcs hCur=Hcpcses.GetByCode(listIntervention[i].CodeValue);
						if(hCur!=null) {
							descript=hCur.DescriptionShort;
						}
						break;
					case "CPT":
						//no need to check for null, return new ProcedureCode object if not found, Descript will be blank
						descript=ProcedureCodes.GetProcCode(listIntervention[i].CodeValue).Descript;
						break;
				}
				row.Cells.Add(descript);
				gridInterventions.Rows.Add(row);
			}
#endregion
#region AddMedicationRows
			for(int i=0;i<listMedPats.Count;i++) {
				row=new ODGridRow();
				row.Cells.Add(listMedPats[i].DateStart.ToShortDateString());
				if(listMedPats[i].RxCui==314153 || listMedPats[i].RxCui==692876) {
					row.Cells.Add("AboveNormalWeightMedication");
				}
				else {
					row.Cells.Add("BelowNormalWeightMedication");
				}
				//Description of Medication----------------------------------------------
				//only meds in EHR table are from RxNorm table
				string descript=RxNorms.GetDescByRxCui(listMedPats[i].RxCui.ToString());
				row.Cells.Add(descript);
				gridInterventions.Rows.Add(row);
			}
#endregion
			gridInterventions.EndUpdate();
		}

		private string calcOverUnderBMIHelper(float bmi) {
			if(ageBeforeJanFirst<18) {//Do not clasify children as over/underweight
				intervCodeSet=InterventionCodeSet.Nutrition;
				return "";
			}
			else if(ageBeforeJanFirst<65) {
				if(bmi<18.5) {
					intervCodeSet=InterventionCodeSet.BelowNormalWeight;
					return "Underweight";
				}
				else if(bmi<25) {
					return "";
				}
				else {
					intervCodeSet=InterventionCodeSet.AboveNormalWeight;
					return "Overweight";
				}
			}
			else {
				if(bmi<23) {
					intervCodeSet=InterventionCodeSet.BelowNormalWeight;
					return "Underweight";
				}
				else if(bmi<30) {
					return "";
				}
				else {
					intervCodeSet=InterventionCodeSet.AboveNormalWeight;
					return "Overweight";
				}
			}
			//do not save to DB until butOK_Click
		}

		private void textWeight_TextChanged(object sender,EventArgs e) {
			CalcBMI();
		}

		private void textHeight_TextChanged(object sender,EventArgs e) {
			CalcBMI();
		}

		private void textBPs_TextChanged(object sender,EventArgs e) {
			if(textBPs.Text=="0") {
				textBPsExamCode.Text="";
				return;
			}
			try {
				int.Parse(textBPs.Text);
			}
			catch {
				textBPsExamCode.Text="";
				return;
			}
			textBPsExamCode.Text="LOINC 8480-6";
		}

		private void textBPd_TextChanged(object sender,EventArgs e) {
			if(textBPd.Text=="0") {
				textBPdExamCode.Text="";
				return;
			}
			try {
				int.Parse(textBPd.Text);
			}
			catch {
				textBPdExamCode.Text="";
				return;
			}
			textBPdExamCode.Text="LOINC 8462-4";
		}

		private void textDateTaken_TextChanged(object sender,EventArgs e) {
			
		}

		///<summary>If they change the date of the exam and it is attached to a pregnancy problem and the date is now outside the active dates of the problem, tell them you are removing the problem and unchecking the pregnancy box.</summary>
		private void textDateTaken_Leave(object sender,EventArgs e) {
			DateTime examDate=PIn.Date(textDateTaken.Text);
			ageBeforeJanFirst=examDate.Year-patCur.Birthdate.Year-1;//This is how old this patient was before any birthday in the year the vital sign was taken, can be negative if patient born the year taken or if value in textDateTaken is empty or not a valid date
			if(!checkPregnant.Checked || VitalsignCur.PregDiseaseNum==0) {
				CalcBMI();//This will use new year taken to determine age at start of that year to show over/underweight if applicable using age specific criteria
				return;
			}
			Disease disCur=Diseases.GetOne(VitalsignCur.PregDiseaseNum);
			if(disCur!=null) {//the currently attached preg disease is valid, will be null if they checked the box on new exam but haven't hit ok to save
				if(examDate.Date<disCur.DateStart || (disCur.DateStop.Year>1880 && disCur.DateStop<examDate.Date)) {//if this exam is not in the active date range of the problem
					if(!MsgBox.Show(this,MsgBoxButtons.YesNo,@"The exam date is no longer within the active dates of the attached pregnancy diagnosis.
Do you want to remove the pregnancy diagnosis?")) 
					{
					 textDateTaken.Focus();
					 return;
					}
				}
				else {
					CalcBMI();
					return;
				}
			}
			//if we get here, either the pregnant check box is not checked, there is not a currently attached preg disease, there is an attached disease but this exam is no longer in the active dates and the user said to remove it, or the current PregDiseaseNum is invalid
			VitalsignCur.PregDiseaseNum=0;
			checkPregnant.Checked=false;
			labelPregNotice.Visible=false;
			textPregCode.Clear();
			textPregCodeDescript.Clear();
			butChangeDefault.Text="Change Default";
			CalcBMI();
		}

		private void checkPregnant_Click(object sender,EventArgs e) {
			labelPregNotice.Visible=false;
			butChangeDefault.Text="Change Default";
			textPregCode.Clear();
			textPregCodeDescript.Clear();
			if(!checkPregnant.Checked) {
				return;
			}
			SetPregCodeAndDescript();
			if(disdefNumCur==0) {
				checkPregnant.Checked=false;
				return;
			}
			if(VitalsignCur.PregDiseaseNum>0) {//if the current vital sign exam linked to a pregnancy dx set the Change Default button to "To Problem" so they can modify the existing problem.
				butChangeDefault.Text="Go to Problem";
			}
			else {//only show warning if we are now attached to a preg dx, add it is not a previously existing problem so we have to insert it.
				labelPregNotice.Visible=true;
			}
		}

		private void butChangeDefault_Click(object sender,EventArgs e) {
			if(butChangeDefault.Text=="Go to Problem") {//text is "To Problem" only when vital sign has a valid PregDiseaseNum and the preg box is checked
				Disease disCur=Diseases.GetOne(VitalsignCur.PregDiseaseNum);
				if(disCur==null) {//should never happen, the only way the button will say "To Problem" is if this exam is pointing to a valid problem
					butChangeDefault.Text="Change Default";
					labelPregNotice.Visible=false;
					textPregCode.Clear();
					textPregCodeDescript.Clear();
					VitalsignCur.PregDiseaseNum=0;
					checkPregnant.Checked=false;
					return;
				}
				FormDiseaseEdit FormDis=new FormDiseaseEdit(disCur);
				FormDis.IsNew=false;
				FormDis.ShowDialog();
				if(FormDis.DialogResult==DialogResult.OK) {
					VitalsignCur.PregDiseaseNum=Vitalsigns.GetOne(VitalsignCur.VitalsignNum).PregDiseaseNum;//if unlinked in FormDiseaseEdit, refresh PregDiseaseNum from db
					if(VitalsignCur.PregDiseaseNum==0) {
						butChangeDefault.Text="Change Default";
						labelPregNotice.Visible=false;
						textPregCode.Clear();
						textPregCodeDescript.Clear();
						checkPregnant.Checked=false;
						return;
					}
					SetPregCodeAndDescript();
					if(disdefNumCur==0) {
						labelPregNotice.Visible=false;
						butChangeDefault.Text="Change Default";
					}
				}
			}
			else {
				if(!Security.IsAuthorized(Permissions.SecurityAdmin,false)) {
					return;
				}
				FormEhrSettings FormEhr=new FormEhrSettings();
				FormEhr.ShowDialog();
				if(FormEhr.DialogResult!=DialogResult.OK || checkPregnant.Checked==false) {
					return;
				}
				labelPregNotice.Visible=false;
				SetPregCodeAndDescript();
				if(disdefNumCur>0) {
					labelPregNotice.Visible=true;
				}
			}
		}

		private void checkNotPerf_Click(object sender,EventArgs e) {
			if(checkNotPerf.Checked) {
				FormEhrNotPerformedEdit FormNP=new FormEhrNotPerformedEdit();
				if(VitalsignCur.EhrNotPerformedNum==0) {
					FormNP.EhrNotPerfCur=new EhrNotPerformed();
					FormNP.EhrNotPerfCur.IsNew=true;
					FormNP.EhrNotPerfCur.PatNum=patCur.PatNum;
					FormNP.EhrNotPerfCur.ProvNum=patCur.PriProv;
					FormNP.SelectedItemIndex=(int)EhrNotPerformedItem.BMIExam;//The code and code value will be set in FormEhrNotPerformedEdit, set the selected index to the EhrNotPerformedItem enum index for BMIExam
					FormNP.EhrNotPerfCur.DateEntry=PIn.Date(textDateTaken.Text);
				}
				else {
					FormNP.EhrNotPerfCur=EhrNotPerformeds.GetOne(VitalsignCur.EhrNotPerformedNum);
					FormNP.EhrNotPerfCur.IsNew=false;
					FormNP.SelectedItemIndex=(int)EhrNotPerformedItem.BMIExam;
				}
				FormNP.ShowDialog();
				if(FormNP.DialogResult==DialogResult.OK) {
					VitalsignCur.EhrNotPerformedNum=FormNP.EhrNotPerfCur.EhrNotPerformedNum;
					textReasonCode.Text=FormNP.EhrNotPerfCur.CodeValueReason;
					Snomed sCur=Snomeds.GetByCode(FormNP.EhrNotPerfCur.CodeValueReason);
					if(sCur!=null) {
						textReasonDescript.Text=sCur.Description;
					}
				}
				else {
					checkNotPerf.Checked=false;
					textReasonCode.Clear();
					textReasonDescript.Clear();
				}
			}
			else {
				textReasonCode.Clear();
				textReasonDescript.Clear();
			}
		}

		private void butAdd_Click(object sender,EventArgs e) {
			FormInterventionEdit FormInt=new FormInterventionEdit();
			FormInt.InterventionCur=new Intervention();
			FormInt.InterventionCur.IsNew=true;
			FormInt.InterventionCur.PatNum=patCur.PatNum;
			FormInt.InterventionCur.ProvNum=patCur.PriProv;
			FormInt.InterventionCur.DateTimeEntry=VitalsignCur.DateTaken;
			FormInt.InterventionCur.CodeSet=intervCodeSet;
			FormInt.IsAllTypes=true;
			FormInt.ShowDialog();
			if(FormInt.DialogResult==DialogResult.OK) {
				bool child=ageBeforeJanFirst<17;
				FillGridInterventions(child);
			}
		}

		private void butIntervention_Click(object sender,EventArgs e) {
			//FormInterventionEdit FormInt=new FormInterventionEdit();
			//FormInt.InterventionCur=new Intervention();
			//FormInt.InterventionCur.IsNew=true;
			//FormInt.InterventionCur.PatNum=patCur.PatNum;
			//FormInt.InterventionCur.ProvNum=patCur.PriProv;
			//FormInt.InterventionCur.DateTimeEntry=VitalsignCur.DateTaken;
			//if(labelWeightCode.Text=="Overweight") {
			//	FormInt.InterventionCur.CodeSet=InterventionCodeSet.AboveNormalWeight;
			//}
			//else {
			//	FormInt.InterventionCur.CodeSet=InterventionCodeSet.BelowNormalWeight;
			//}
			//FormInt.IsCodeSetLocked=true;
			//FormInt.ShowDialog();
		}

		private void butMedication_Click(object sender,EventArgs e) {
			////select medication from list.  Additional meds can be added to the list from within that dlg
			//FormMedications FormM=new FormMedications();
			//FormM.IsSelectionMode=true;
			//FormM.ShowDialog();
			//if(FormM.DialogResult!=DialogResult.OK) {
			//	return;
			//}
			//MedicationPat MedicationPatCur=new MedicationPat();
			//MedicationPatCur.PatNum=patCur.PatNum;
			//MedicationPatCur.MedicationNum=FormM.SelectedMedicationNum;
			//MedicationPatCur.RxCui=Medications.GetMedication(FormM.SelectedMedicationNum).RxCui;
			//MedicationPatCur.ProvNum=patCur.PriProv;
			//FormMedPat FormMP=new FormMedPat();
			//FormMP.MedicationPatCur=MedicationPatCur;
			//FormMP.IsNew=true;
			//FormMP.ShowDialog();
			//if(FormMP.DialogResult!=DialogResult.OK) {
			//	return;
			//}
			////if medication button visible, not child
			//FillGridInterventions(false);
		}

		private void butDelete_Click(object sender,EventArgs e) {
			if(VitalsignCur.IsNew) {
				DialogResult=DialogResult.Cancel;
				return;
			}
			if(!MsgBox.Show(this,MsgBoxButtons.OKCancel,"Delete?")) {
				return;
			}
			if(VitalsignCur.EhrNotPerformedNum!=0) {
				EhrNotPerformeds.Delete(VitalsignCur.EhrNotPerformedNum);
			}
			Vitalsigns.Delete(VitalsignCur.VitalsignNum);
			DialogResult=DialogResult.Cancel;
		}

		private void butOK_Click(object sender,EventArgs e) {
			#region Validate
			DateTime date;
			if(textDateTaken.Text=="") {
				MsgBox.Show(this,"Please enter a date.");
				return;
			}
			try {
				date=DateTime.Parse(textDateTaken.Text);
			}
			catch {
				MsgBox.Show(this,"Please fix date first.");
				return;
			}
			//validate height
			float height=0;
			try {
				if(textHeight.Text!="") {
					height = float.Parse(textHeight.Text);
				}
			}
			catch {
				MsgBox.Show(this,"Please fix height first.");
				return;
			}
			//validate weight
			float weight=0;
			try {
				if(textWeight.Text!="") {
					weight = float.Parse(textWeight.Text);
				}
			}
			catch {
				MsgBox.Show(this,"Please fix weight first.");
				return;
			}
			//validate bp
			int BPsys=0;
			int BPdia=0;
			try {
				if(textBPs.Text!="") {
					BPsys = int.Parse(textBPs.Text);
				}
				if(textBPd.Text!="") {
					BPdia = int.Parse(textBPd.Text);
				}
			}
			catch {
				MsgBox.Show(this,"Please fix BP first.");
				return;
			}
			#endregion
			#region Save
			VitalsignCur.DateTaken=date;
			VitalsignCur.Height=height;
			VitalsignCur.Weight=weight;
			VitalsignCur.BpDiastolic=BPdia;
			VitalsignCur.BpSystolic=BPsys;
			VitalsignCur.BMIExamCode=listBMICodes[comboBMIPercentileCode.SelectedIndex].LoincCode;
			VitalsignCur.HeightExamCode=listHeightCodes[comboHeightExamCode.SelectedIndex].LoincCode;
			VitalsignCur.WeightExamCode=listWeightCodes[comboWeightExamCode.SelectedIndex].LoincCode;
			switch(labelWeightCode.Text) {
				case "Overweight":
					VitalsignCur.WeightCode="238131007";
					break;
				case "Underweight":
					VitalsignCur.WeightCode="248342006";
					break;
				case "":
				default:
					VitalsignCur.WeightCode="";
					break;
			}
			#region PregnancyDx
			if(checkPregnant.Checked) {//pregnant, add pregnant dx if necessary
				if(disdefNumCur==0) {
					//shouldn't happen, if checked this must be set to either an existing problem def or a new problem that requires inserting, return to form with checkPregnant unchecked
					MsgBox.Show(this,"This exam must point to a valid pregnancy diagnosis.");
					checkPregnant.Checked=false;
					labelPregNotice.Visible=false;
					return;
				}
				if(VitalsignCur.PregDiseaseNum==0) {//insert new preg disease and update vitalsign to point to it
					Disease pregDisNew=new Disease();
					pregDisNew.PatNum=VitalsignCur.PatNum;
					pregDisNew.DiseaseDefNum=disdefNumCur;
					pregDisNew.DateStart=VitalsignCur.DateTaken;
					pregDisNew.ProbStatus=ProblemStatus.Active;
					VitalsignCur.PregDiseaseNum=Diseases.Insert(pregDisNew);
				}
				else {
					Disease disCur=Diseases.GetOne(VitalsignCur.PregDiseaseNum);
					if(VitalsignCur.DateTaken<disCur.DateStart
						|| (disCur.DateStop.Year>1880 && VitalsignCur.DateTaken>disCur.DateStop))
					{//the current exam is no longer within dates of preg problem, uncheck the pregnancy box and remove the pointer to the disease
						MsgBox.Show(this,"This exam is not within the active dates of the attached pregnancy problem.  To exclude this patient from BMI measurement due to pregnancy, reselect the pregnant check box.");
						checkPregnant.Checked=false;
						textPregCode.Clear();
						textPregCodeDescript.Clear();
						labelPregNotice.Visible=false;
						VitalsignCur.PregDiseaseNum=0;
						butChangeDefault.Text="Change Default";
						return;
					}
				}
			}
			else {//checkPregnant not checked
				VitalsignCur.PregDiseaseNum=0;
			}
			#endregion
			#region EhrNotPerformed
			if(!checkNotPerf.Checked) {
				if(VitalsignCur.EhrNotPerformedNum!=0) {
					EhrNotPerformeds.Delete(VitalsignCur.EhrNotPerformedNum);
					VitalsignCur.EhrNotPerformedNum=0;
				}
			}
			if(VitalsignCur.IsNew) {
			  Vitalsigns.Insert(VitalsignCur);
			}
			else {
				Vitalsigns.Update(VitalsignCur);
			}
			#endregion
			#endregion
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}
		

	

	


	}
}
