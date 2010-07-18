using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental {
	public partial class FormGuardianEdit:Form {
		private Guardian GuardianCur;
		private Family Fam;

		public FormGuardianEdit(Guardian guardianCur,Family fam){
			InitializeComponent();
			GuardianCur=guardianCur;
			Fam=fam;
			Lan.F(this);
		}

		private void FormGuardianEdit_Load(object sender,EventArgs e) {
			textDependant.Text=Fam.GetNameInFamFL(GuardianCur.PatNumChild);
			textGuardian.Text=Fam.GetNameInFamFL(GuardianCur.PatNumGuardian);
			string[] relationshipNames=Enum.GetNames(typeof(GuardianRelationship));
			for(int i=0;i<relationshipNames.Length;i++){
				listRelationship.Items.Add(relationshipNames[i]);
			}
			listRelationship.SelectedIndex=(int)GuardianCur.Relationship;
		}

		private void butPick_Click(object sender,EventArgs e) {
			FormFamilyMemberSelect FormF=new FormFamilyMemberSelect(Fam);
			FormF.ShowDialog();
			if(FormF.DialogResult!=DialogResult.OK) {
				return;
			}
			GuardianCur.PatNumGuardian=FormF.SelectedPatNum;
			textGuardian.Text=Fam.GetNameInFamFL(GuardianCur.PatNumGuardian);
		}

		private void butDelete_Click(object sender,EventArgs e) {
			if(GuardianCur.IsNew) {
				DialogResult=DialogResult.Cancel;
			}
			else {
				Guardians.Delete(GuardianCur.GuardianNum);
				DialogResult=DialogResult.OK;
			}
		}

		private void butOK_Click(object sender,EventArgs e) {
			if(GuardianCur.PatNumGuardian==0) {
				MsgBox.Show(this,"Please set a guardian first.");
				return;
			}
			//PatNumChild already set
			//PatNumGuardian already set
			GuardianCur.Relationship=(GuardianRelationship)listRelationship.SelectedIndex;
			if(GuardianCur.IsNew) {
				Guardians.Insert(GuardianCur);
			}
			else {
				Guardians.Update(GuardianCur);
			}
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		
		

		

		
	}
}