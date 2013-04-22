using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using OpenDentBusiness;
using OpenDental.UI;

namespace OpenDental {
	public partial class FormResellerEdit:Form {
		//TODO: Uncomment this after the table Reseller has been added to the db.
		//private Reseller ResellerCur;

		public FormResellerEdit() {
			//TODO: Add a Reseller parameter to the constructor and set ResellerCur to the parameter passed in.
			InitializeComponent();
			Lan.F(this);
		}

		private void FormResellerEdit_Load(object sender,EventArgs e) {
			//Load up the reseller's credentials and all of their customers that are paying for the Patient Portal.
		}

		private void FillGrid() {
			//TODO: Get the list of customers for the reseller.
			gridMain.BeginUpdate();
			gridMain.Rows.Clear();
			ODGridRow row;
			//TODO: Loop through the list customers of the reseller and set each column text accordingly.
			row=new ODGridRow();
			gridMain.Rows.Add(row);
			gridMain.EndUpdate();
		}

		private void butGenerateNew_Click(object sender,EventArgs e) {
			if(textPassword.Text!="" && !MsgBox.Show(this,MsgBoxButtons.YesNo,"This will reset the reseller's online password immediately.\r\n Are you sure you want to reset their password?")) {
				return;
			}
			//TODO: Generate a new password and possibly a new user name for the reseller.
			//TODO: Instantly update the db.
		}

		private void butDelete_Click(object sender,EventArgs e) {
			//TODO: Do not let the reseller be deleted if they have customers in their list.
		}

		private void butClose_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}


	}
}