using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using OpenDental.UI;

namespace OpenDental {
	public partial class FormLicenseMissing:Form {
		public FormLicenseMissing() {
			InitializeComponent();
			RefreshGrid();
		}

		private void RefreshGrid() {
			codeGrid.BeginUpdate();
			codeGrid.Columns.Clear();
			ODGridColumn col=new ODGridColumn("Patient",80);
			codeGrid.Columns.Add(col);
			col=new ODGridColumn("Date Used",80);
			codeGrid.Columns.Add(col);
			codeGrid.Rows.Clear();
			/*for(int i=0;i<;i++) {
				ODGridRow row=new ODGridRow();
				row.Cells.Add();//patient
				row.Cells.Add();//date used
				codeGrid.Rows.Add(row);
			}*/
			codeGrid.EndUpdate();
		}

		private void okbutton_Click(object sender,EventArgs e) {
			Close();
		}

		private void printbutton_Click(object sender,EventArgs e) {

		}
	}
}