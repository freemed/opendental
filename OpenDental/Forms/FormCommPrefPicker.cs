using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using OpenDentBusiness;
using OpenDental.UI;

namespace OpenDental {
	public partial class FormCommPrefPicker:Form {
		public ContactMethod ContMethCur;

		public FormCommPrefPicker() {
			InitializeComponent();
		}

		private void FormCommPrefPicker_Load(object sender,EventArgs e) {
			if(ContMethCur==null) {
				ContMethCur=new ContactMethod();
			}
			fillGrid();
		}

		private void fillGrid() {
			gridMain.BeginUpdate();
			gridMain.Columns.Clear();
			ODGridColumn col;
			col=new ODGridColumn("Contact Preference",120,HorizontalAlignment.Center);
			gridMain.Columns.Add(col);
			//col=new ODGridColumn("ICD9 Code",80);
			//gridMain.Columns.Add(col);
			//col=new ODGridColumn("SNOMED Code",80);
			//gridMain.Columns.Add(col);
			gridMain.Rows.Clear();
			ODGridRow row;
			for(int i=0;i<Enum.GetNames(typeof(ContactMethod)).Length;i++) {
				row=new ODGridRow();
				row.Cells.Add(Enum.GetNames(typeof(ContactMethod))[i]);
				//row.Cells.Add(DiseaseDefs.ListLong[i].ICD9Code);
				//row.Cells.Add(DiseaseDefs.ListLong[i].SnomedCode);
				gridMain.Rows.Add(row);
			}
			gridMain.EndUpdate();
		}

		private void gridMain_CellDoubleClick(object sender,ODGridClickEventArgs e) {
			ContMethCur=(ContactMethod)e.Row;
			DialogResult=DialogResult.OK;
		}

		private void butOK_Click(object sender,EventArgs e) {
			if(gridMain.GetSelectedIndex()==-1) {
				MsgBox.Show(this,"Please select a communication preference first.");
				return;
			}
			ContMethCur=(ContactMethod)gridMain.GetSelectedIndex();
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}
	}
}
