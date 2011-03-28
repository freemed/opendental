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

		public FormAllergyEdit(Allergy allergy) {
			InitializeComponent();
			Lan.F(this);
			AllergyCur=allergy;
		}

		private void FormAllergyEdit_Load(object sender,EventArgs e) {
			AllergyDef aDef=AllergyDefs.GetOne(AllergyCur.AllergyDefNum);
			textAllergy.Text=aDef.Description;
			textReaction.Text=AllergyCur.Reaction;
			checkActive.Checked=AllergyCur.StatusIsActive;
		}

		private void butOK_Click(object sender,EventArgs e) {
			AllergyCur.Reaction=textReaction.Text;
			AllergyCur.StatusIsActive=checkActive.Checked;
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}
	}
}