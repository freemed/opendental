using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental {
	public partial class FormAllergyEdit:Form {
		public Allergy AllergyCur;
		private Patient patCur;
		private List<AllergyDef> allergyList;

		public FormAllergyEdit(Patient PatCur) {
			InitializeComponent();
			Lan.F(this);
			patCur=PatCur;
		}

		private void FormAllergyEdit_Load(object sender,EventArgs e) {
			int allergyIndex=0;
			allergyList=AllergyDefs.GetAll(false);
			if(allergyList.Count<1) {
				MsgBox.Show(this,"Need to set up at least one Allergy from EHR setup window.");
				DialogResult=DialogResult.Cancel;
				return;
			}
			for(int i=0;i<allergyList.Count;i++) {
				comboAllergies.Items.Add(allergyList[i].Description);
				if(!AllergyCur.IsNew && allergyList[i].AllergyDefNum==AllergyCur.AllergyDefNum) {
					allergyIndex=i;
				}
			}
			if(!AllergyCur.IsNew) {
				comboAllergies.SelectedIndex=allergyIndex;
				textReaction.Text=AllergyCur.Reaction;
				checkActive.Checked=AllergyCur.StatusIsActive;
			}
			else {
				comboAllergies.SelectedIndex=0;
			}
		}

		private void butOK_Click(object sender,EventArgs e) {
			AllergyCur.AllergyDefNum=allergyList[comboAllergies.SelectedIndex].AllergyDefNum;
			AllergyCur.Reaction=textReaction.Text;
			AllergyCur.StatusIsActive=checkActive.Checked;
			if(AllergyCur.IsNew) {
				AllergyCur.PatNum=patCur.PatNum;
				Allergies.Insert(AllergyCur);
			}
			else {
				Allergies.Update(AllergyCur);
			}
			DialogResult=DialogResult.OK;
		}

		private void butDelete_Click(object sender,EventArgs e) {
			if(!AllergyCur.IsNew) {
				Allergies.Delete(AllergyCur.AllergyNum);
			}
			DialogResult=DialogResult.Cancel;
		}

		private void butCancel_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}
	}
}