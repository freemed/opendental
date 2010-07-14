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

		private Patient PatCur;
		private List <Patient> FamilyMembersToShow;

		public FormGuardianEdit(Patient dependant,List <Patient> familyMembersToShow) {
			InitializeComponent();
			PatCur=dependant;
			FamilyMembersToShow=familyMembersToShow;
			textDependant.Text=dependant.GetNameFLFormal();
			for(int i=0;i<FamilyMembersToShow.Count;i++){
				if(FamilyMembersToShow[i].Position==PatientPosition.Child){
					continue;
				}
				listFamilyMembers.Items.Add(FamilyMembersToShow[i].GetNameFLFormal());
			}
			string[] relationshipNames=Enum.GetNames(typeof(GuardianRelationship));
			for(int i=0;i<relationshipNames.Length;i++){
				listRelationships.Items.Add(relationshipNames[i]);
			}
			Lan.F(this);
		}

		private void butOK_Click(object sender,EventArgs e) {
			if(listFamilyMembers.SelectedIndex<0){
				MsgBox.Show(this,"You must first select a family member");
				return;
			}
			if(listRelationships.SelectedIndex<0){
				MsgBox.Show(this,"You must first select a relationship");
				return;
			}
			Guardian relat=new Guardian();
			relat.PatNumChild=PatCur.PatNum;
			relat.PatNumGuardian=FamilyMembersToShow[listFamilyMembers.SelectedIndex].PatNum;
			relat.Relationship=(GuardianRelationship)(listRelationships.SelectedIndex+1);
			Guardians.Insert(relat);
			DialogResult=DialogResult.OK;
			Close();
		}

		private void butCancel_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}
	}
}