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

		private void butDelete_Click(object sender,EventArgs e) {
			//TODO: Do not let the reseller be deleted if they have customers in their list.
		}

		private void butOK_Click(object sender,EventArgs e) {
			//TODO: Check if the username is already in use.
			//TODO: Update the username and password to the database.  Keep as plain text.
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}


	}
}