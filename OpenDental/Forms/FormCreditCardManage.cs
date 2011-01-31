using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental {
	public partial class FormCreditCardManage:Form {
		public FormCreditCardManage() {
			InitializeComponent();
			Lan.F(this);
		}

		private void listCreditCards_MouseDoubleClick(object sender,MouseEventArgs e) {
			if(listCreditCards.SelectedIndex==-1) {
				return;
			}
		}

		private void butAdd_Click(object sender,EventArgs e) {

		}

		private void butUp_Click(object sender,EventArgs e) {

		}

		private void butDown_Click(object sender,EventArgs e) {

		}

		private void butClose_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}
	}
}