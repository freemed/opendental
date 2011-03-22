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

		private void FormCanadaPaymentReconciliation_Load(object sender,EventArgs e) {
			for(int i=0;i<Carriers.Listt.Length;i++) {
				if((Carriers.Listt[i].CanadianSupportedTypes&CanSupTransTypes.RequestForOutstandingTrans_04)==CanSupTransTypes.RequestForOutstandingTrans_04) {
					carriers.Add(Carriers.Listt[i]);
					listCarriers.Items.Add(Carriers.Listt[i].CarrierName);
				}
			}
		}

		private void butOK_Click(object sender,EventArgs e) {
			if(listCarriers.SelectedIndex<0) {
				MsgBox.Show(this,"You must first choose a carrier.");
				return;
			}
			Cursor=Cursors.WaitCursor;
			try {
				CanadianOutput.GetOutstandingTransactions(carriers[listCarriers.SelectedIndex]);
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