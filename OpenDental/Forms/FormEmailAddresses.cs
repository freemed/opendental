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
		private List<EmailAddress> ListEmailAddresses;

		public FormEmailAddresses() {
			InitializeComponent();
			Lan.F(this);
		}

		private void FormEmailAddresses_Load(object sender,EventArgs e) {
			FillGrid();
		}

		private void gridMain_CellDoubleClick(object sender,ODGridClickEventArgs e) {
			FormEmailAddressEdit FormEAE=new FormEmailAddressEdit();
			FormEAE.EmailAddressCur=ListEmailAddresses[e.Row];
			FormEAE.ShowDialog();
			if(FormEAE.DialogResult==DialogResult.OK) {
				FillGrid();
			}
		}

		private void FillGrid() {
			ListEmailAddresses=EmailAddresses.GetAll();
			gridMain.BeginUpdate();
			gridMain.Columns.Clear();
			ODGridColumn col;
			col=new ODGridColumn(Lan.g(this,"User Name"),300);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g(this,"Default"),20);
			gridMain.Columns.Add(col);
			gridMain.Rows.Clear();
			ODGridRow row;
			for(int i=0;i<ListEmailAddresses.Count;i++) {
				row=new ODGridRow();
				row.Cells.Add(ListEmailAddresses[i].EmailUsername);
				row.Cells.Add((ListEmailAddresses[i].EmailAddressNum==PrefC.GetLong(PrefName.EmailDefaultAddressNum))?"X":"");
				gridMain.Rows.Add(row);
			}
			gridMain.EndUpdate();
		}

		private void butSetDefault_Click(object sender,EventArgs e) {
			if(gridMain.GetSelectedIndex()==-1) {
				MsgBox.Show(this,"Please select a row first.");
				return;
			}
			if(Prefs.UpdateLong(PrefName.EmailDefaultAddressNum,ListEmailAddresses[gridMain.GetSelectedIndex()].EmailAddressNum)) {
				DataValid.SetInvalid(InvalidType.Prefs);
			}
			FillGrid();
		}

		private void butAdd_Click(object sender,EventArgs e) {
			FormEmailAddressEdit FormEAE=new FormEmailAddressEdit();
			FormEAE.EmailAddressCur=new EmailAddress();
			FormEAE.IsNew=true;
			FormEAE.ShowDialog();
			if(FormEAE.DialogResult==DialogResult.OK) {
				FillGrid();
			}
		}

		private void butClose_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		private void FormEmailAddresses_FormClosing(object sender,FormClosingEventArgs e) {
			//if changed
			//DataValid.SetInvalid(InvalidType.Email);
		}
	}
}