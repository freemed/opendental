using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental {
	public partial class FormAllergyDefEdit:Form {
		public AllergyDef AllergyDefCur;

		public FormAllergyDefEdit() {
			InitializeComponent();
			Lan.F(this);
		}

		private void FormAllergyEdit_Load(object sender,EventArgs e) {
			if(!AllergyDefCur.IsNew) { 
				textDescription.Text=AllergyDefCur.Description;
				checkHidden.Checked=AllergyDefCur.IsHidden;
			}
			for(int i=0;i<Enum.GetNames(typeof(SnomedAllergy)).Length;i++) {
				comboSnomedAllergyType.Items.Add(Enum.GetNames(typeof(SnomedAllergy))[i]);
			}
		}

		private void butOK_Click(object sender,EventArgs e) {
			if(textDescription.Text.Trim()=="") {
				MsgBox.Show(this,"Description cannot be blank.");
				return;
			}
			AllergyDefCur.Description=textDescription.Text;
			AllergyDefCur.IsHidden=checkHidden.Checked;
			AllergyDefCur.Snomed=(SnomedAllergy)comboSnomedAllergyType.SelectedIndex;
			if(textRxCui.errorProvider1.GetError(textRxCui)!=""){
				MsgBox.Show(this,"You may only enter a number for RxNorm CUI.");
			}
			AllergyDefCur.RxCui=PIn.Long(textRxCui.Text);	
			if(AllergyDefCur.IsNew) {
				AllergyDefs.Insert(AllergyDefCur);
			}
			else {
				AllergyDefs.Update(AllergyDefCur);
			}
			DialogResult=DialogResult.OK;
		}

		private void butDelete_Click(object sender,EventArgs e) {
			if(!AllergyDefCur.IsNew) {
				if(!AllergyDefs.DefIsInUse(AllergyDefCur.AllergyDefNum)) {
					AllergyDefs.Delete(AllergyDefCur.AllergyDefNum);
				}
				else {
					MsgBox.Show(this,"Cannot delete allergies in use.");
					return;
				}
			}
			DialogResult=DialogResult.Cancel;
		}

		private void butCancel_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}
	}
}