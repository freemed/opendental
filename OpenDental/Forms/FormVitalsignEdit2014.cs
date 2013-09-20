using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using OpenDentBusiness;

namespace EHR {
	public partial class FormVitalsignEdit2014:Form {
		public Vitalsign VitalsignCur;
		private Patient patCur;
		public bool IsBPOnly;
		public bool IsBMIOnly;

		public FormVitalsignEdit2014() {
			InitializeComponent();
		}

		private void FormVitalsignEdit2014_Load(object sender,EventArgs e) {
			patCur=Patients.GetPat(VitalsignCur.PatNum);
			textBMIExamCode.Text="LOINC 39156-5";
			if(IsBPOnly) {
				labelBMI.Visible=false;
				labelHeight.Visible=false;
				labelWeight.Visible=false;
				groupBox1.Text="";
				textBMI.Visible=false;
				textHeight.Visible=false;
				textWeight.Visible=false;
			}
			if(IsBMIOnly) {
				labelBP.Visible=false;
				labelDiv.Visible=false;
				textBPd.Visible=false;
				textBPs.Visible=false;
			}
			if(patCur.Age>0 && patCur.Age<17) {//child
				groupBox1.Text="";
			}
			else {

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
		}

		private void textWeight_TextChanged(object sender,EventArgs e) {
			CalcBMI();
		}

		private void textHeight_TextChanged(object sender,EventArgs e) {
			CalcBMI();
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
				return;
			}
			if(height==0) {
				return;
			}
			if(weight==0) {
				return;
			}
			float bmi=Vitalsigns.CalcBMI(weight, height);// ((float)(weight*703)/(height*height));
			textBMI.Text=bmi.ToString("n1");
			labelWeightCode.Text=calcOverUnderBMIHelper(bmi);
			return;
		}

		private string calcOverUnderBMIHelper(float bmi) {
			int ageBeforeJanFirst=DateTime.Now.Year-patCur.Birthdate.Year;
			if(ageBeforeJanFirst<10) {
				if(bmi<15) {
					return "Underweight";
				}
				else if(bmi<25) {
					return "";
				}
				else {
					return "Overweight";
				}
			}
			else if(ageBeforeJanFirst<50) {
				if(bmi<15) {
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
				if(bmi<15) {
					return "Underweight";
				}
				else if(bmi<25) {
					return "";
				}
				else {
					return "Overweight";
				}
			}
			//do not save to DB until butOK_Click
		}

		private void butSnomed_Click(object sender,EventArgs e) {
			
		}

		private void butIcd9_Click(object sender,EventArgs e) {

		}

		private void butIcd10_Click(object sender,EventArgs e) {

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
