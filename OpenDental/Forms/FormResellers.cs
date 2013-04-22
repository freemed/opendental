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
	public partial class FormResellers:Form {
		//TODO: Uncomment this list after the table Reseller has been added to the db.
		//private List<Reseller> ListResellers;

		public FormResellers() {
			InitializeComponent();
			Lan.F(this);
		}

		private void FormResellers_Load(object sender,EventArgs e) {
			FillGrid();
		}

		private void FillGrid() {
			//TODO: Fill the list of resellers using the values in the text boxes.
			gridMain.BeginUpdate();
			gridMain.Rows.Clear();
			ODGridRow row;
			//TODO: Loop through the list of resellers and set each column accordingly.
			row=new ODGridRow();
			gridMain.Rows.Add(row);
			gridMain.EndUpdate();
			gridMain.SetSelected(0,true);
		}

		#region TextChanged

		private void textLName_TextChanged(object sender,EventArgs e) {
			FillGrid();
		}

		private void textFName_TextChanged(object sender,EventArgs e) {
			FillGrid();
		}

		private void textHmPhone_TextChanged(object sender,EventArgs e) {
			FillGrid();
		}

		private void textAddress_TextChanged(object sender,EventArgs e) {
			FillGrid();
		}

		private void textCity_TextChanged(object sender,EventArgs e) {
			FillGrid();
		}

		private void textState_TextChanged(object sender,EventArgs e) {
			FillGrid();
		}

		private void textPatNum_TextChanged(object sender,EventArgs e) {
			FillGrid();
		}

		private void textEmail_TextChanged(object sender,EventArgs e) {
			FillGrid();
		}

		#endregion TextChanged

		private void gridMain_CellDoubleClick(object sender,ODGridClickEventArgs e) {
			//TODO: Create a reseller object and set it to the reseller with the matching index in the reseller list.  Reminder: [e.Row]
		}

		private void butAdd_Click(object sender,EventArgs e) {
			FormPatientSelect formPS=new FormPatientSelect();
			formPS.ShowDialog();
			if(formPS.DialogResult==DialogResult.OK) {
				long selectedPatNum=formPS.SelectedPatNum;
				//TODO: Create a new entry in the resellers table linked to this patient.
				FillGrid();
			}
		}

		private void butOK_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}
	}
}