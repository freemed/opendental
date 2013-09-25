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

namespace EHR {
	public partial class FormVitalsignEdit2014:Form {
		public Vitalsign VitalsignCur;
		private Patient patCur;
		public bool IsBPOnly;
		public bool IsBMIOnly;
		public int ageBeforeJanFirst;
		private List<Loinc> listHeightCodes;
		private List<Loinc> listWeightCodes;
		private List<Loinc> listBMICodes;
		private TabPage childPage;
		private TabPage overUnderPage;

		public FormVitalsignEdit2014() {
			InitializeComponent();
		}

		private void FormVitalsignEdit2014_Load(object sender,EventArgs e) {
			childPage=tabControl1.TabPages[0];
			overUnderPage=tabControl1.TabPages[1];
			tabControl1.TabPages.Remove(overUnderPage);
			tabControl1.TabPages.Remove(childPage);
			tabControl1.Visible=false;
			patCur=Patients.GetPat(VitalsignCur.PatNum);
			if(IsBPOnly) {
				labelBMI.Visible=false;
				labelHeight.Visible=false;
				labelWeight.Visible=false;
				textBMI.Visible=false;
				textHeight.Visible=false;
				textWeight.Visible=false;
				labelWeightCode.Visible=false;
				textBMIExamCode.Visible=false;
				labelBMIExamCode.Visible=false;
				labelHeightExamCode.Visible=false;
				labelWeightExamCode.Visible=false;
				comboHeightExamCode.Visible=false;
				comboWeightExamCode.Visible=false;
				comboBMIPercentileCode.Visible=false;
				labelBMIPercentCode.Visible=false;
			}
			if(IsBMIOnly) {
				labelBPs.Visible=false;
				labelBPd.Visible=false;
				textBPd.Visible=false;
				textBPs.Visible=false;
				labelBPsExamCode.Visible=false;
				labelBPdExamCode.Visible=false;
				textBPsExamCode.Visible=false;
				textBPdExamCode.Visible=false;
			}
			textDateTaken.Text=VitalsignCur.DateTaken.ToShortDateString();
			if(!IsBPOnly) {
				textHeight.Text=VitalsignCur.Height.ToString();
				textWeight.Text=VitalsignCur.Weight.ToString();
				CalcBMI();
			}
			if(!IsBMIOnly) {
				textBPd.Text=VitalsignCur.BpDiastolic.ToString();
				textBPs.Text=VitalsignCur.BpSystolic.ToString();
			}
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
			ageBeforeJanFirst=(PIn.Date(textDateTaken.Text)).Year-patCur.Birthdate.Year-1;//This is how old this patient was before any birthday in the year the vital sign was taken, can be negative if patient born the year taken or if value in textDateTaken is empty or not a valid date
			CalcBMI();//This will use new year taken to determine age at start of that year to show over/underweight if applicable using age specific criteria
			textDateTaken.Focus();
		}

		private void CalcBMI() {
			labelWeightCode.Text="";
			//BMI = (lbs*703)/(in^2)
			float height;
			float weight;
			try{
				height = float.Parse(textHeight.Text);
				weight = float.Parse(textWeight.Text);
			}
			catch{
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
			float bmi=Vitalsigns.CalcBMI(weight, height);// ((float)(weight*703)/(height*height));
			textBMI.Text=bmi.ToString("n1");
			labelWeightCode.Text=calcOverUnderBMIHelper(bmi);
			tabControl1.Visible=false;
			if(ageBeforeJanFirst<17) {
				tabControl1.Visible=true;
				if(!tabControl1.Contains(childPage)) {
					tabControl1.TabPages.Add(childPage);
				}
				if(tabControl1.Contains(overUnderPage)) {
					tabControl1.TabPages.Remove(overUnderPage);
				}
			}
			else if(ageBeforeJanFirst>=18) {
				if(tabControl1.Contains(childPage)) {
					tabControl1.TabPages.Remove(childPage);
				}
				if(labelWeightCode.Text!="") {//if over 18 and given an over/underweight code due to BMI
					tabControl1.Visible=true;
					if(!tabControl1.Contains(overUnderPage)) {
						tabControl1.TabPages.Add(overUnderPage);
					}
				}
			}
			textBMIExamCode.Text="LOINC 39156-5";//This is the only code allowed for the BMI procedure
			return;
		}

		private string calcOverUnderBMIHelper(float bmi) {
			if(ageBeforeJanFirst<18) {//Do not clasify children as over/underweight
				return "";
			}
			else if(ageBeforeJanFirst<65) {
				if(bmi<18.5) {
					return "Underweight";
				}
				else if(bmi<25) {
					return "";
				}
				else {
					return "Overweight";
				}
			}
			else {
				if(bmi<23) {
					return "Underweight";
				}
				else if(bmi<30) {
					return "";
				}
				else {
					return "Overweight";
				}
			}
			//do not save to DB until butOK_Click
		}

		private void checkPregnant_Click(object sender,EventArgs e) {
			if(checkPregnant.Checked) {
				SetPregCodeAndDescript();
			}
			else {
				textPregCode.Clear();
				textPregCodeDescript.Clear();
				labelPregNotice.Visible=false;
			}
		}

		///<summary>Sets the pregnancy code and description based on the defaults set in the preference table.</summary>
		private void SetPregCodeAndDescript() {
			string pregCode=PrefC.GetString(PrefName.PregnancyDefaultCodeValue);
			textPregCode.Text=pregCode;
			if(pregCode!="" && pregCode!="none") {
				labelPregNotice.Visible=true;
				switch(PrefC.GetString(PrefName.PregnancyDefaultCodeSystem)) {
					case "ICD9CM":
						ICD9 i9Preg=ICD9s.GetByCode(pregCode);
						if(i9Preg!=null) {
							textPregCodeDescript.Text=i9Preg.Description;
						}
						break;
					case "ICD10CM":
						Icd10 i10Preg=Icd10s.GetByCode(pregCode);
						if(i10Preg!=null) {
							textPregCodeDescript.Text=i10Preg.Description;
						}
						break;
					case "SNOMEDCT":
						Snomed sPreg=Snomeds.GetByCode(pregCode);
						if(sPreg!=null) {
							textPregCodeDescript.Text=sPreg.Description;
						}
						break;
				}
			}
			else {
				labelPregNotice.Visible=false;
			}
		}

		private void butChangeDefault_Click(object sender,EventArgs e) {
			if(Security.IsAuthorized(Permissions.SecurityAdmin,false)) {
				FormEhrSettings formE=new FormEhrSettings();
				formE.ShowDialog();
				if(formE.DialogResult==DialogResult.OK) {
					SetPregCodeAndDescript();
				}
			}
		}

		private void checkNotPerf_Click(object sender,EventArgs e) {
			FormEhrNotPerformedEdit formNP=new FormEhrNotPerformedEdit();
			formNP.EhrNotPerfCur=new EhrNotPerformed();
			formNP.EhrNotPerfCur.PatNum=patCur.PatNum;
			formNP.EhrNotPerfCur.ProvNum=patCur.PriProv;
			formNP.EhrNotPerfCur.CodeValue="39156-5";//This is the only code allowed for the BMI procedure
			formNP.EhrNotPerfCur.CodeSystem="LOINC";
			formNP.EhrNotPerfCur.DateTimeEntry=PIn.Date(textDateTaken.Text);
			formNP.ShowDialog();
			if(formNP.DialogResult==DialogResult.OK) {
				VitalsignCur.EhrNotPerformedNum=formNP.EhrNotPerfCur.EhrNotPerformedNum;
				textReasonCode.Text=formNP.EhrNotPerfCur.CodeValueReason;
				Snomed sCur=Snomeds.GetByCode(formNP.EhrNotPerfCur.CodeValueReason);
				if(sCur!=null) {
					textReasonDescript.Text=sCur.Description;
				}
			}
		}

		private void butIntervention_Click(object sender,EventArgs e) {

		}

		private void butMedication_Click_1(object sender,EventArgs e) {

		}

		private void butNutrition_Click(object sender,EventArgs e) {

		}

		private void butPhysActivity_Click(object sender,EventArgs e) {

		}

		private void butDelete_Click(object sender,EventArgs e) {
			if(VitalsignCur.IsNew) {
				DialogResult=DialogResult.Cancel;
				return;
			}
			if(MessageBox.Show("Delete?","",MessageBoxButtons.OKCancel)!=DialogResult.OK){
				return;
			}
			Vitalsigns.Delete(VitalsignCur.VitalsignNum);
			DialogResult=DialogResult.Cancel;
		}

		private void butOK_Click(object sender,EventArgs e) {
			//validate
			DateTime date;
			if(textDateTaken.Text=="") {
				MessageBox.Show("Please enter a date.");
				return;
			}
			try {
				date=DateTime.Parse(textDateTaken.Text);
			}
			catch {
				MessageBox.Show("Please fix date first.");
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
				MessageBox.Show("Please fix height first.");
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
				MessageBox.Show("Please fix weight first.");
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
				MessageBox.Show("Please fix BP first.");
				return;
			}
			//save------------------------------------------------------------------
			VitalsignCur.DateTaken=date;
			VitalsignCur.Height=height;
			VitalsignCur.Weight=weight;
			VitalsignCur.BpDiastolic=BPdia;
			VitalsignCur.BpSystolic=BPsys;
			VitalsignCur.BMIExamCode=listBMICodes[comboBMIPercentileCode.SelectedIndex].LoincCode;
			VitalsignCur.HeightExamCode=listHeightCodes[comboHeightExamCode.SelectedIndex].LoincCode;
			VitalsignCur.WeightCode=listWeightCodes[comboWeightExamCode.SelectedIndex].LoincCode;
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
			if(VitalsignCur.IsNew) {
				if(checkPregnant.Checked) {//if new and checked the pregnant check box, use default preg code and system to create a new preg Dx for this patient with start date equal to the exam date
					string pregCode=PrefC.GetString(PrefName.PregnancyDefaultCodeValue);
					if(pregCode!="" && pregCode!="none") {
						//long disease
						Disease pregDisCur=new Disease();
						pregDisCur.PatNum=VitalsignCur.PatNum;
						pregDisCur.DiseaseDefNum=DiseaseDefs.GetNumFromCode(pregCode);
					}
				}
			  Vitalsigns.Insert(VitalsignCur);
			}
			else {
				Vitalsigns.Update(VitalsignCur);
			}
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		

	

	


	}
}
