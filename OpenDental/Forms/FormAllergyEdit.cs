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
		private List<AllergyDef> allergyDefList;

		public FormAllergyEdit() {
			InitializeComponent();
			Lan.F(this);
		}

		private void FormAllergyEdit_Load(object sender,EventArgs e) {
			int allergyIndex=0;
			allergyDefList=AllergyDefs.GetAll(false);
			if(allergyDefList.Count<1) {
				MsgBox.Show(this,"Need to set up at least one Allergy from EHR setup window.");
				DialogResult=DialogResult.Cancel;
				return;
			}
			for(int i=0;i<allergyDefList.Count;i++) {
				comboAllergies.Items.Add(allergyDefList[i].Description);
				if(!AllergyCur.IsNew && allergyDefList[i].AllergyDefNum==AllergyCur.AllergyDefNum) {
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

		private void butDelete_Click(object sender,EventArgs e) {
			if(AllergyCur.IsNew) {
				DialogResult=DialogResult.Cancel;
				return;
			}
			if(!MsgBox.Show(this,MsgBoxButtons.OKCancel,"Delete?")){
				return;
			}
			Allergies.Delete(AllergyCur.AllergyNum);
			DialogResult=DialogResult.OK;
		}

		private void butOK_Click(object sender,EventArgs e) {
			AllergyCur.AllergyDefNum=allergyDefList[comboAllergies.SelectedIndex].AllergyDefNum;
			AllergyCur.Reaction=textReaction.Text;
			AllergyCur.StatusIsActive=checkActive.Checked;
			if(AllergyCur.IsNew) {
				Allergies.Insert(AllergyCur);
			}
			else {
				Allergies.Update(AllergyCur);
			}
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}
	}
}