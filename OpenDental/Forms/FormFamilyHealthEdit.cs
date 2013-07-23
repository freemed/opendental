using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental {
	public partial class FormFamilyHealthEdit:Form {
		public FamilyHealth FamilyHealthCur;
		private DiseaseDef DisDefCur;

		public FormFamilyHealthEdit() {
			InitializeComponent();
			Lan.F(this);
		}

		private void FormFamilyHealthEdit_Load(object sender,EventArgs e) {
			string[] familyRelationships=Enum.GetNames(typeof(FamilyRelationship));
			for(int i=0;i<familyRelationships.Length;i++) {
				listRelationship.Items.Add(Lan.g("enumFamilyRelationship",familyRelationships[i]));
			}
			listRelationship.SelectedIndex=(int)FamilyHealthCur.Relationship;
			if(FamilyHealthCur.IsNew) {
				return; //Don't need to set any of the info below.  All null.
			}
			DisDefCur=DiseaseDefs.GetItem(FamilyHealthCur.DiseaseDefNum);
			textProblem.Text=DisDefCur.DiseaseName;
			textSnomed.Text=DisDefCur.SnomedCode;
			textName.Text=FamilyHealthCur.PersonName;
		}

		private void butPick_Click(object sender,EventArgs e) {
			FormDiseaseDefs FormD=new FormDiseaseDefs();
			FormD.IsSelectionMode=true;
			FormD.ShowDialog();
			if(FormD.DialogResult!=DialogResult.OK) {
				return;
			}
			DiseaseDef disDef=DiseaseDefs.GetItem(FormD.SelectedDiseaseDefNum);
			if(disDef.SnomedCode=="") {
				MsgBox.Show(this,"Selection must have a Snomed code associated");
				return;
			}
			textProblem.Text=disDef.DiseaseName;
			textSnomed.Text=disDef.SnomedCode;
			DisDefCur=disDef;
		}

		private void butDelete_Click(object sender,EventArgs e) {
			if(FamilyHealthCur.IsNew) {
				DialogResult=DialogResult.Cancel;
				return;
			}
			if(!MsgBox.Show(this,MsgBoxButtons.OKCancel,"Delete?")) {
				return;
			}
			FamilyHealths.Delete(FamilyHealthCur.FamilyHealthNum);
			DialogResult=DialogResult.OK;
		}

		private void butOK_Click(object sender,EventArgs e) {
			if(listRelationship.SelectedIndex<0) {
				MsgBox.Show(this,"Relationship required.");
				return;
			}
			if(textName.Text.Trim()=="") {
				MsgBox.Show(this,"Name required.");
				return;
			}
			if(DisDefCur==null) {
				MsgBox.Show(this,"Problem required.");
				return;
			}
			FamilyHealthCur.DiseaseDefNum=DisDefCur.DiseaseDefNum;
			FamilyHealthCur.Relationship=(FamilyRelationship)listRelationship.SelectedIndex;
			FamilyHealthCur.PersonName=textName.Text;
			if(FamilyHealthCur.IsNew) {
				FamilyHealths.Insert(FamilyHealthCur);
			}
			else {
				FamilyHealths.Update(FamilyHealthCur);
			}
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}
	}
}