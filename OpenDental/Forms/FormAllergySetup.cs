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
		private List<AllergyDef> listAllergyDefs;

		public FormAllergySetup() {
			InitializeComponent();
			Lan.F(this);
		}

		private void FormAllergySetup_Load(object sender,EventArgs e) {
			FillGrid();
		}

		private void FillGrid() {
			listAllergyDefs=AllergyDefs.GetAll(checkShowHidden.Checked);
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
				if(listAllergyDefs[i].IsHidden) {
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

		private void gridMain_CellDoubleClick(object sender,ODGridClickEventArgs e) {
			FormAllergyDefEdit FADE=new FormAllergyDefEdit();
			FADE.AllergyDefCur=listAllergyDefs[gridMain.GetSelectedIndex()];
			FADE.ShowDialog();
			FillGrid();
		}

		private void butAdd_Click(object sender,EventArgs e) {
			FormAllergyDefEdit FADE=new FormAllergyDefEdit();
			FADE.AllergyDefCur=new AllergyDef();
			FADE.AllergyDefCur.IsNew=true;
			FADE.ShowDialog();
			FillGrid();
		}

		private void butClose_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}
	}
}