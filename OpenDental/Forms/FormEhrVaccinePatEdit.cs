using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental {
	public partial class FormEhrVaccinePatEdit:Form {
		public VaccinePat VaccinePatCur;
		public bool IsNew;

		public FormEhrVaccinePatEdit() {
			InitializeComponent();
		}

		private void FormVaccinePatEdit_Load(object sender,EventArgs e) {
			Patient pat=Patients.GetLim(VaccinePatCur.PatNum);
			if(pat.Age!=0 && pat.Age<3) {
				labelDocument.Text="Document reason not given below.  Reason can include a contraindication due to a specific allergy, adverse effect, intollerance, or specific disease.";//less leeway with reasons for kids.
			}
			for(int i=0;i<VaccineDefs.Listt.Count;i++) {
				comboVaccine.Items.Add(VaccineDefs.Listt[i].CVXCode + " - " + VaccineDefs.Listt[i].VaccineName);
				if(VaccineDefs.Listt[i].VaccineDefNum==VaccinePatCur.VaccineDefNum) {
					comboVaccine.SelectedIndex=i;
				}
			}
			if(VaccinePatCur.DateTimeStart.Year>1880) {
				textDateTimeStart.Text=VaccinePatCur.DateTimeStart.ToString();
			}
			if(VaccinePatCur.DateTimeEnd.Year>1880) {
				textDateTimeStop.Text=VaccinePatCur.DateTimeEnd.ToString();
			}
			if(!IsNew) {
				VaccineDef vaccineDef=VaccineDefs.GetOne(VaccinePatCur.VaccineDefNum);//Need vaccine to get manufacturer.
				DrugManufacturer manufacturer=DrugManufacturers.GetOne(vaccineDef.DrugManufacturerNum);
				textManufacturer.Text=manufacturer.ManufacturerCode + " - " + manufacturer.ManufacturerName;
			}
			if(VaccinePatCur.AdministeredAmt!=0){
				textAmount.Text=VaccinePatCur.AdministeredAmt.ToString();
			}
			textLotNum.Text=VaccinePatCur.LotNumber;
			comboUnits.Items.Add("none");
			comboUnits.SelectedIndex=0;
			for(int i=0;i<DrugUnits.Listt.Count;i++) {
				comboUnits.Items.Add(DrugUnits.Listt[i].UnitIdentifier);
				if(DrugUnits.Listt[i].DrugUnitNum==VaccinePatCur.DrugUnitNum) {
					comboUnits.SelectedIndex=i+1;
				}
			}
			checkNotGiven.Checked=VaccinePatCur.NotGiven;
			textNote.Text=VaccinePatCur.Note;
		}

		private void comboVaccine_SelectedIndexChanged(object sender,EventArgs e) {
			DrugManufacturer manufacturer=DrugManufacturers.GetOne(VaccineDefs.Listt[comboVaccine.SelectedIndex].DrugManufacturerNum);
			textManufacturer.Text=manufacturer.ManufacturerCode + " - " + manufacturer.ManufacturerName;
		}

		private void butDelete_Click(object sender,EventArgs e) {
			if(IsNew) {
				DialogResult=DialogResult.Cancel;
				return;
			}
			if(MessageBox.Show("Delete?","Delete?",MessageBoxButtons.OKCancel)==DialogResult.Cancel) {
				return;
			}
			VaccinePats.Delete(VaccinePatCur.VaccinePatNum);
			DialogResult=DialogResult.OK;
		}

		private void butOK_Click(object sender,EventArgs e) {
			if(comboVaccine.SelectedIndex==-1) {
				MessageBox.Show(this,"Please select a vaccine.");
				return;
			}
			if(checkNotGiven.Checked) {
				if(textNote.Text=="") {
					MessageBox.Show(this,"Please enter documentation in the note.");
					return;
				}
			}
			VaccinePatCur.VaccineDefNum=VaccineDefs.Listt[comboVaccine.SelectedIndex].VaccineDefNum;
			try {
				VaccinePatCur.DateTimeStart=PIn.DateT(textDateTimeStart.Text);
				VaccinePatCur.DateTimeEnd=PIn.DateT(textDateTimeStop.Text);
			}
			catch {
				MessageBox.Show(this,"Please enter start and end times in format DD/MM/YYYY HH:mm AM/PM");
			}
			if(textAmount.Text==""){
				VaccinePatCur.AdministeredAmt=0;
			}
			else{
				try {
					VaccinePatCur.AdministeredAmt=PIn.Float(textAmount.Text);
				}
				catch {
					MessageBox.Show(this,"Please enter a valid amount.");
				}
			}
			if(comboUnits.SelectedIndex==0) {//'none'
				VaccinePatCur.DrugUnitNum=0;
			}
			else{
				VaccinePatCur.DrugUnitNum=DrugUnits.Listt[comboUnits.SelectedIndex-1].DrugUnitNum;
			}
			VaccinePatCur.LotNumber=textLotNum.Text;
			VaccinePatCur.NotGiven=checkNotGiven.Checked;
			VaccinePatCur.Note=textNote.Text;
			if(IsNew) {
				VaccinePats.Insert(VaccinePatCur);
			}
			else {
				VaccinePats.Update(VaccinePatCur);
			}
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}
		

	

	


	}
}
