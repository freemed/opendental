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
			checkUseElectronic.Checked=PrefC.GetBool("BillingUseElectronic");
			textVendorId.Text=PrefC.GetString("BillingElectVendorId");
			textVendorPMScode.Text=PrefC.GetString("BillingElectVendorPMSCode");
			string cc=PrefC.GetString("BillingElectCreditCardChoices");
			if(cc.Contains("MC")) {
				checkMC.Checked=true;
			}
			if(cc.Contains("V")) {
				checkV.Checked=true;
			}
			if(cc.Contains("D")) {
				checkD.Checked=true;
			}
			if(cc.Contains("A")) {
				checkAmEx.Checked=true;
			}
			textClientAcctNumber.Text=PrefC.GetString("BillingElectClientAcctNumber");
			textUserName.Text=PrefC.GetString("BillingElectUserName");
			textPassword.Text=PrefC.GetString("BillingElectPassword");
		}

		private void butOK_Click(object sender,EventArgs e) {
			if(textDays.errorProvider1.GetError(textDays)!=""){
				MsgBox.Show(this,"Please fix data entry errors first.");
				return;
			}
			string cc="";
			if(checkMC.Checked) {
				cc="MC";
			}
			if(checkV.Checked) {
				if(cc!="") {
					cc+=",";
				}
				cc+="V";
			}
			if(checkD.Checked) {
				if(cc!="") {
					cc+=",";
				}
				cc+="D";
			}
			if(checkAmEx.Checked) {
				if(cc!="") {
					cc+=",";
				}
				cc+="A";
			}
			if(Prefs.UpdateInt("BillingDefaultsLastDays",PIn.PLong(textDays.Text))
				| Prefs.UpdateBool("BillingDefaultsIntermingle",checkIntermingled.Checked)
				| Prefs.UpdateString("BillingDefaultsNote",textNote.Text)
				| Prefs.UpdateBool("BillingUseElectronic",checkUseElectronic.Checked)
				| Prefs.UpdateString("BillingElectVendorId",textVendorId.Text)
				| Prefs.UpdateString("BillingElectVendorPMSCode",textVendorPMScode.Text)
				| Prefs.UpdateString("BillingElectCreditCardChoices",cc)
				| Prefs.UpdateString("BillingElectClientAcctNumber",textClientAcctNumber.Text)
				| Prefs.UpdateString("BillingElectUserName",textUserName.Text)
				| Prefs.UpdateString("BillingElectPassword",textPassword.Text))
			{
				DataValid.SetInvalid(InvalidType.Prefs);
			}
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

	
	}
}