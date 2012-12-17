using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using OpenDental.UI;
using OpenDentBusiness;

namespace OpenDental {
	public partial class FormEmailAddresses:Form {
		private List<EmailAddress> ListEmailAddress;

		public FormEmailAddresses() {
			InitializeComponent();
			Lan.F(this);
		}

		private void butOK_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		private void FormEmailAddresses_Load(object sender,EventArgs e) {
			ListEmailAddress=EmailAddresses.GetAll();
			gridMain.BeginUpdate();
			gridMain.Columns.Clear();
			ODGridColumn col;
			col=new ODGridColumn(Lan.g(this,"Email Address"),300);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g(this,"Default"),20);
			gridMain.Columns.Add(col);
			gridMain.Rows.Clear();
			ODGridRow row;
			for(int i=0;i<ListEmailAddress.Count;i++) {
				row=new ODGridRow();
				row.Cells.Add(ListEmailAddress[i].SenderAddress);
			}
		}
	}
}