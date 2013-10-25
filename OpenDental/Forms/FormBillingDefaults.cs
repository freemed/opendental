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
			textDays.Text=PrefC.GetLong(PrefName.BillingDefaultsLastDays).ToString();
			checkIntermingled.Checked=PrefC.GetBool(PrefName.BillingDefaultsIntermingle);
			textNote.Text=PrefC.GetString(PrefName.BillingDefaultsNote);
			listElectBilling.SelectedIndex=0;
			if(PrefC.GetString(PrefName.BillingUseElectronic)=="1") {
				listElectBilling.SelectedIndex=1;
			}
			if(PrefC.GetString(PrefName.BillingUseElectronic)=="2") {
				listElectBilling.SelectedIndex=2;
			}
			if(PrefC.GetString(PrefName.BillingUseElectronic)=="3") {
				listElectBilling.SelectedIndex=3;
			}
			textVendorId.Text=PrefC.GetString(PrefName.BillingElectVendorId);
			textVendorPMScode.Text=PrefC.GetString(PrefName.BillingElectVendorPMSCode);
			string cc=PrefC.GetString(PrefName.BillingElectCreditCardChoices);
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
			textClientAcctNumber.Text=PrefC.GetString(PrefName.BillingElectClientAcctNumber);
			textUserName.Text=PrefC.GetString(PrefName.BillingElectUserName);
			textPassword.Text=PrefC.GetString(PrefName.BillingElectPassword);
			//email
			textBillingEmailSubject.Text=PrefC.GetString(PrefName.BillingEmailSubject);
			textBillingEmailBody.Text=PrefC.GetString(PrefName.BillingEmailBodyText);
			textInvoiceNote.Text=PrefC.GetString(PrefName.BillingDefaultsInvoiceNote);
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
			string billingUseElectronic=listElectBilling.SelectedIndex.ToString();
			if(Prefs.UpdateLong(PrefName.BillingDefaultsLastDays,PIn.Long(textDays.Text))
				| Prefs.UpdateBool(PrefName.BillingDefaultsIntermingle,checkIntermingled.Checked)
				| Prefs.UpdateString(PrefName.BillingDefaultsNote,textNote.Text)
				| Prefs.UpdateString(PrefName.BillingUseElectronic,billingUseElectronic)
				| Prefs.UpdateString(PrefName.BillingEmailSubject,textBillingEmailSubject.Text)
				| Prefs.UpdateString(PrefName.BillingEmailBodyText,textBillingEmailBody.Text)
				| Prefs.UpdateString(PrefName.BillingElectVendorId,textVendorId.Text)
				| Prefs.UpdateString(PrefName.BillingElectVendorPMSCode,textVendorPMScode.Text)
				| Prefs.UpdateString(PrefName.BillingElectCreditCardChoices,cc)
				| Prefs.UpdateString(PrefName.BillingElectClientAcctNumber,textClientAcctNumber.Text)
				| Prefs.UpdateString(PrefName.BillingElectUserName,textUserName.Text)
				| Prefs.UpdateString(PrefName.BillingElectPassword,textPassword.Text)
				| Prefs.UpdateString(PrefName.BillingDefaultsInvoiceNote,textInvoiceNote.Text))
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