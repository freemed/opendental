using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental {
	public partial class FormDrugUnitEdit:Form {
		public DrugUnit DrugUnitCur;
		public bool IsNew;

		public FormDrugUnitEdit() {
			InitializeComponent();
			Lan.F(this);
		}

		private void FormDrugUnitEdit_Load(object sender,EventArgs e) {
			textUnitIdentifier.Text=DrugUnitCur.UnitIdentifier;
			textUnitText.Text=DrugUnitCur.UnitText;
		}

		private void butDelete_Click(object sender,EventArgs e) {
			if(IsNew) {
				DialogResult=DialogResult.Cancel;
				return;
			}
			if(!MsgBox.Show(this,MsgBoxButtons.OKCancel,"Delete?")){
				return;
			}
			else{
				DrugUnits.Delete(DrugUnitCur.DrugUnitNum);
				DialogResult=DialogResult.Cancel;
			}
		}

		private void butOK_Click(object sender,EventArgs e) {
			if(textUnitIdentifier.Text=="" || textUnitText.Text=="") {
				MsgBox.Show(this,"Bank fields are not allowed.");
				return;
			}
			DrugUnitCur.UnitIdentifier=textUnitIdentifier.Text;
			DrugUnitCur.UnitText=textUnitText.Text;
			if(IsNew) {
				for(int i=0;i<DrugUnits.Listt.Count;i++) {
					if(DrugUnits.Listt[i].UnitIdentifier==textUnitIdentifier.Text) {
						MsgBox.Show(this,"Unit with this identifier already exists.");
						return;
					}
				}
				DrugUnits.Insert(DrugUnitCur);
			}
			else {
				DrugUnits.Update(DrugUnitCur);
			}
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}
	}
}