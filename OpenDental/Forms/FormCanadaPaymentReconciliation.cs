using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using OpenDentBusiness;
using OpenDental.Eclaims;

namespace OpenDental {
	public partial class FormCanadaPaymentReconciliation:Form {

		List<Carrier> carriers=new List<Carrier>();

		public FormCanadaPaymentReconciliation() {
			InitializeComponent();
			Lan.F(this);
		}

		private void FormCanadaPaymentReconciliation_Load(object sender,EventArgs e) {
			for(int i=0;i<Carriers.Listt.Length;i++) {
				if(Carriers.Listt[i].CDAnetVersion!="02" &&//This transaction does not exist in version 02.
					(Carriers.Listt[i].CanadianSupportedTypes&CanSupTransTypes.RequestForPaymentReconciliation_06)==CanSupTransTypes.RequestForPaymentReconciliation_06) {
					carriers.Add(Carriers.Listt[i]);
					listCarriers.Items.Add(Carriers.Listt[i].CarrierName);
				}
			}
			long defaultProvNum=PrefC.GetLong(PrefName.PracticeDefaultProv);
			for(int i=0;i<ProviderC.ListShort.Length;i++) {
				if(ProviderC.ListShort[i].IsCDAnet) {
					listBillingProvider.Items.Add(ProviderC.ListShort[i].Abbr);
					listTreatingProvider.Items.Add(ProviderC.ListShort[i].Abbr);
					if(ProviderC.ListShort[i].ProvNum==defaultProvNum) {
						listBillingProvider.SelectedIndex=i;
						textBillingOfficeNumber.Text=ProviderC.ListShort[i].CanadianOfficeNum;
						listTreatingProvider.SelectedIndex=i;
						textTreatingOfficeNumber.Text=ProviderC.ListShort[i].CanadianOfficeNum;
					}
				}
			}
			textDateReconciliation.Text=DateTime.Today.ToShortDateString();
		}

		private void listBillingProvider_Click(object sender,EventArgs e) {
			textBillingOfficeNumber.Text=ProviderC.ListShort[listBillingProvider.SelectedIndex].CanadianOfficeNum;
		}

		private void listTreatingProvider_Click(object sender,EventArgs e) {
			textTreatingOfficeNumber.Text=ProviderC.ListShort[listTreatingProvider.SelectedIndex].CanadianOfficeNum;
		}

		private void butOK_Click(object sender,EventArgs e) {
			if(listCarriers.SelectedIndex<0) {
				MsgBox.Show(this,"You must first choose a carrier.");
				return;
			}
			if(listBillingProvider.SelectedIndex<0) {
				MsgBox.Show(this,"You must first choose a billing provider.");
				return;
			}
			if(listTreatingProvider.SelectedIndex<0) {
				MsgBox.Show(this,"You must first choose a treating provider.");
				return;
			}
			DateTime reconciliationDate;
			try {
				reconciliationDate=DateTime.Parse(textDateReconciliation.Text).Date;
			}
			catch {
				MsgBox.Show(this,"Reconciliation date invalid.");
				return;
			}
			Cursor=Cursors.WaitCursor;
			try {
				CanadianOutput.GetPaymentReconciliations(carriers[listCarriers.SelectedIndex],ProviderC.ListShort[listTreatingProvider.SelectedIndex],
					ProviderC.ListShort[listBillingProvider.SelectedIndex],reconciliationDate);
				Cursor=Cursors.Default;
				MsgBox.Show(this,"Done.");
			}
			catch(Exception ex) {
				Cursor=Cursors.Default;
				MessageBox.Show(Lan.g(this,"Request failed: ")+ex.Message);
			}
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

	}
}