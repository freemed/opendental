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
	public partial class FormCanadaSummaryReconciliation:Form {

		List<Carrier> carriers=new List<Carrier>();

		public FormCanadaSummaryReconciliation() {
			InitializeComponent();
			Lan.F(this);
		}

		private void FormCanadaPaymentReconciliation_Load(object sender,EventArgs e) {
			for(int i=0;i<CanadianNetworks.Listt.Count;i++) {
				listNetworks.Items.Add(CanadianNetworks.Listt[i].Abbrev+" - "+CanadianNetworks.Listt[i].Descript);
			}
			for(int i=0;i<Carriers.Listt.Length;i++) {
				if(Carriers.Listt[i].CDAnetVersion!="02" &&//This transaction does not exist in version 02.
					(Carriers.Listt[i].CanadianSupportedTypes&CanSupTransTypes.RequestForSummaryReconciliation_05)==CanSupTransTypes.RequestForSummaryReconciliation_05) {
					carriers.Add(Carriers.Listt[i]);
					listCarriers.Items.Add(Carriers.Listt[i].CarrierName);
				}
			}
			long defaultProvNum=PrefC.GetLong(PrefName.PracticeDefaultProv);
			for(int i=0;i<ProviderC.List.Length;i++) {
				listTreatingProvider.Items.Add(ProviderC.List[i].Abbr);
				if(ProviderC.List[i].ProvNum==defaultProvNum) {
					listTreatingProvider.SelectedIndex=i;
				}
			}
			textDateReconciliation.Text=DateTime.Today.ToShortDateString();
		}

		private void checkGetForAllCarriers_Click(object sender,EventArgs e) {
			groupCarrierOrNetwork.Enabled=!checkGetForAllCarriers.Checked;
		}

		private void listCarriers_Click(object sender,EventArgs e) {
			listNetworks.SelectedIndex=-1;
		}

		private void listNetwork_Click(object sender,EventArgs e) {
			listCarriers.SelectedIndex=-1;
		}

		private void butOK_Click(object sender,EventArgs e) {
			if(!checkGetForAllCarriers.Checked) {
				if(listCarriers.SelectedIndex<0 && listNetworks.SelectedIndex<0) {
					MsgBox.Show(this,"You must first choose one carrier or one network.");
					return;
				}
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
				if(checkGetForAllCarriers.Checked) {
					Carrier carrier=new Carrier();
					carrier.CDAnetVersion="04";
					carrier.ElectID="999999";//The whole ITRANS network.
					carrier.CanadianEncryptionMethod=1;//No encryption.
					CanadianOutput.GetSummaryReconciliation(carrier,null,ProviderC.List[listTreatingProvider.SelectedIndex],reconciliationDate);
				}
				else {
					if(listCarriers.SelectedIndex>=0) {
						CanadianOutput.GetSummaryReconciliation(carriers[listCarriers.SelectedIndex],null,ProviderC.List[listTreatingProvider.SelectedIndex],reconciliationDate);
					}
					else {
						CanadianOutput.GetSummaryReconciliation(null,CanadianNetworks.Listt[listNetworks.SelectedIndex],ProviderC.List[listTreatingProvider.SelectedIndex],reconciliationDate);
					}
				}
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