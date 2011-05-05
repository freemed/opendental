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
	public partial class FormCanadaOutstandingTransactions:Form {

		List<Carrier> carriers=new List<Carrier>();

		public FormCanadaOutstandingTransactions() {
			InitializeComponent();
			Lan.F(this);
		}

		private void FormCanadaOutstandingTransactions_Load(object sender,EventArgs e) {
			for(int i=0;i<Carriers.Listt.Length;i++) {
				if((Carriers.Listt[i].CanadianSupportedTypes&CanSupTransTypes.RequestForOutstandingTrans_04)==CanSupTransTypes.RequestForOutstandingTrans_04) {
					carriers.Add(Carriers.Listt[i]);
					listCarriers.Items.Add(Carriers.Listt[i].CarrierName);
				}
			}
			for(int i=0;i<ProviderC.List.Length;i++) {
				if(ProviderC.List[i].NationalProvID=="" || ProviderC.List[i].CanadianOfficeNum=="") {
					continue;
				}
				if(!listOfficeNumbers.Items.Contains(ProviderC.List[i].CanadianOfficeNum)) {
					listOfficeNumbers.Items.Add(ProviderC.List[i].CanadianOfficeNum);
				}
			}
			if(listOfficeNumbers.Items.Count<1) {
				MsgBox.Show(this,"At least one unhidden provider must have a CDA Number and an Office Number set before running a Request for Outstanding Transactions.");
				Close();
			}
		}

		private void radioVersion2_Click(object sender,EventArgs e) {
			radioVersion2.Checked=true;
			radioVersion4Itrans.Checked=false;
			radioVersion4ToCarrier.Checked=false;
			groupCarrier.Enabled=false;
		}

		private void radioVersion4Itrans_Click(object sender,EventArgs e) {
			radioVersion2.Checked=false;
			radioVersion4Itrans.Checked=true;
			radioVersion4ToCarrier.Checked=false;
			groupCarrier.Enabled=false;
		}

		private void radioVersion4ToCarrier_Click(object sender,EventArgs e) {
			radioVersion2.Checked=false;
			radioVersion4Itrans.Checked=false;
			radioVersion4ToCarrier.Checked=true;
			groupCarrier.Enabled=true;
		}

		private void butOK_Click(object sender,EventArgs e) {
			if(radioVersion4ToCarrier.Checked) {
				if(listCarriers.SelectedIndex<0) {
					MsgBox.Show(this,"You must first select a carrier to use.");
					return;
				}
			}
			if(listOfficeNumbers.SelectedIndex<0) {
				MsgBox.Show(this,"You must first select an Office Number to use.");
				return;
			}
			Cursor=Cursors.WaitCursor;
			Provider prov=null;
			for(int i=0;i<ProviderC.List.Length;i++) {
				if(ProviderC.List[i].CanadianOfficeNum==listOfficeNumbers.Items[listOfficeNumbers.SelectedIndex].ToString() 
					&& ProviderC.List[i].NationalProvID!="") {
					prov=ProviderC.List[i];
					break;
				}
			}
			try {
				if(radioVersion2.Checked) {
					CanadianOutput.GetOutstandingTransactions(true,false,null,prov);
				}
				else if(radioVersion4Itrans.Checked) {
					CanadianOutput.GetOutstandingTransactions(false,true,null,prov);
				}
				else if(radioVersion4ToCarrier.Checked) {
					CanadianOutput.GetOutstandingTransactions(false,false,carriers[listCarriers.SelectedIndex],prov);
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