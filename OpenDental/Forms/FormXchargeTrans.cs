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

		public FormXchargeTrans() {
			InitializeComponent();
			Lan.F(this);
		}

		private void FormXchargeTrans_Load(object sender,EventArgs e) {
			CashBackAmount=0;
			textCashBackAmt.Text=CashBackAmount.ToString("F2");
			comboTransType.Items.Clear();
			comboTransType.Items.Add("Purchase");
			comboTransType.Items.Add("Return");
			comboTransType.Items.Add("Debit Purchase");
			comboTransType.Items.Add("Debit Return");
			comboTransType.Items.Add("Force");
			comboTransType.Items.Add("Pre-Authorization");
			comboTransType.Items.Add("Adjustment");
			comboTransType.Items.Add("Void");
			comboTransType.SelectedIndex=0;
		}

		private void comboTransType_SelectionChangeCommitted(object sender,EventArgs e) {
			textCashBackAmt.Visible=false;
			labelCashBackAmt.Visible=false;
			if(comboTransType.SelectedIndex==2) { //Debit Purchase
				textCashBackAmt.Visible=true;
				labelCashBackAmt.Visible=true;
			}
		}

		private void butOK_Click(object sender,EventArgs e) {
			if(comboTransType.SelectedIndex==2) { //Debit Purchase
				if(textCashBackAmt.errorProvider1.GetError(textCashBackAmt)!="") {
					MsgBox.Show(this,"Please fix data entry errors first.");
					return;
				}
				CashBackAmount=PIn.Decimal(textCashBackAmt.Text);
			}
			TransactionType=comboTransType.SelectedIndex;
			DialogResult=DialogResult.OK;
		}

	}
}