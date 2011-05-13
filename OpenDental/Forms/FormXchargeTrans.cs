using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental {
	public partial class FormXchargeTrans:Form {
		public int TransactionType;
		public decimal CashBackAmount;
		public bool SaveToken;

		public FormXchargeTrans() {
			InitializeComponent();
			Lan.F(this);
		}

		private void FormXchargeTrans_Load(object sender,EventArgs e) {
			CashBackAmount=0;
			textCashBackAmt.Text=CashBackAmount.ToString("F2");
			listTransType.Items.Clear();
			listTransType.Items.Add("Purchase");
			listTransType.Items.Add("Return");
			listTransType.Items.Add("Debit Purchase");
			listTransType.Items.Add("Debit Return");
			listTransType.Items.Add("Force");
			listTransType.Items.Add("Pre-Authorization");
			listTransType.Items.Add("Adjustment");
			listTransType.Items.Add("Void");
			listTransType.SelectedIndex=0;
			checkSaveToken.Checked=PrefC.GetBool(PrefName.StoreCCtokens);
		}

		private void listTransType_MouseClick(object sender,MouseEventArgs e) {
			if(listTransType.IndexFromPoint(e.Location)!=-1) {
				textCashBackAmt.Visible=false;
				labelCashBackAmt.Visible=false;
				if(listTransType.SelectedIndex==2) { //Debit Purchase
					textCashBackAmt.Visible=true;
					labelCashBackAmt.Visible=true;
				}
			}
		}

		private void butOK_Click(object sender,EventArgs e) {
			if(listTransType.SelectedIndex==2) { //Debit Purchase
				if(textCashBackAmt.errorProvider1.GetError(textCashBackAmt)!="") {
					MsgBox.Show(this,"Please fix data entry errors first.");
					return;
				}
				CashBackAmount=PIn.Decimal(textCashBackAmt.Text);
			}
			TransactionType=listTransType.SelectedIndex;
			SaveToken=checkSaveToken.Checked;
			DialogResult=DialogResult.OK;
		}

	}
}