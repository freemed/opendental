using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental {
	public partial class FormBillingDefaults:Form {
		public FormBillingDefaults() {
			InitializeComponent();
			Lan.F(this);
		}

		private void FormBillingDefaults_Load(object sender,EventArgs e) {
			textDays.Text=PrefC.GetInt("BillingDefaultsLastDays").ToString();
			checkIntermingled.Checked=PrefC.GetBool("BillingDefaultsIntermingle");
			textNote.Text=PrefC.GetString("BillingDefaultsNote");
		}

		private void butOK_Click(object sender,EventArgs e) {
			if(textDays.errorProvider1.GetError(textDays)!=""){
				MsgBox.Show(this,"Please fix data entry errors first.");
				return;
			}
			if(Prefs.UpdateInt("BillingDefaultsLastDays",PIn.PInt(textDays.Text))
				| Prefs.UpdateBool("BillingDefaultsIntermingle",checkIntermingled.Checked)
				| Prefs.UpdateString("BillingDefaultsNote",textNote.Text))
			{
				DataValid.SetInvalid(InvalidTypes.Prefs);
			}
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

	
	}
}