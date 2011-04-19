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
			textUnitIdentifier.Text=DrugUnitCur.UnitIdentifier.ToString();
			textUnitText.Text=DrugUnitCur.UnitText.ToString();
		}

		private void butDelete_Click(object sender,EventArgs e) {
			if(!MsgBox.Show(this,MsgBoxButtons.OKCancel,"Delete?")){
				return;
			}
			if(IsNew){
				DialogResult=DialogResult.Cancel;
			}
			else{
				DrugUnits.Delete(DrugUnitCur.DrugUnitNum);
				DialogResult=DialogResult.OK;
			}
		}

		private void butOK_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}
	}
}