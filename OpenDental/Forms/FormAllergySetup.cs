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
	public partial class FormAllergySetup:Form {
		public FormAllergySetup() {
			InitializeComponent();
			Lan.F(this);
		}

		private void FormAllergySetup_Load(object sender,EventArgs e) {
			FillGrid();
		}

		private void FillGrid() {
			List<AllergyDef> listAllergyDefs=AllergyDefs.GetAll(checkShowHidden.Checked);
			gridMain.BeginUpdate();
			gridMain.Columns.Clear();
			ODGridColumn col=new ODGridColumn(Lan.g("FormAllergySetup","Desciption"),160);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g("FormAllergySetup","Hidden"),60);
			gridMain.Columns.Add(col);
			gridMain.Rows.Clear();
			ODGridRow row;
			for(int i=0;i<listAllergyDefs.Count;i++) {
				row=new ODGridRow();
				row.Cells.Add(listAllergyDefs[i].Description);
				if(PIn.Bool(listAllergyDefs[i].IsHidden.ToString())) {
					row.Cells.Add("X");
				}
				else {
					row.Cells.Add("");
				}
				gridMain.Rows.Add(row);
			}
			gridMain.EndUpdate();
		}

		private void checkShowHidden_CheckedChanged(object sender,EventArgs e) {
			FillGrid();
		}

		private void butAdd_Click(object sender,EventArgs e) {
			//Show a FormDefSetup window?
		}

		private void butClose_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}
	}
}