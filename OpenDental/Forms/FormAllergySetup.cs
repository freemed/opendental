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
		public bool IsSelectionMode;
		public long SelectedAllergyDefNum;

		public FormAllergySetup() {
			InitializeComponent();
			Lan.F(this);
		}

		private void FormAllergySetup_Load(object sender,EventArgs e) {
			if(IsSelectionMode) {
				butOK.Visible=true;
				butClose.Text="Cancel";
			}
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
			if(IsSelectionMode) {
				SelectedAllergyDefNum=listAllergyDefs[e.Row].AllergyDefNum;
				DialogResult=DialogResult.OK;
			}
			else {
				FormAllergyDefEdit formA=new FormAllergyDefEdit();
				formA.AllergyDefCur=listAllergyDefs[gridMain.GetSelectedIndex()];
				formA.ShowDialog();
				FillGrid();
			}
		}

		private void butAdd_Click(object sender,EventArgs e) {
			FormAllergyDefEdit formA=new FormAllergyDefEdit();
			formA.AllergyDefCur=new AllergyDef();
			formA.AllergyDefCur.IsNew=true;
			formA.ShowDialog();
			FillGrid();
		}

		private void butOK_Click(object sender,EventArgs e) {
			//Only visible in IsSelectionMode.
			if(gridMain.GetSelectedIndex()==-1) {
				MsgBox.Show(this,"Select at least one allergy.");
				return;
			}
			SelectedAllergyDefNum=listAllergyDefs[gridMain.GetSelectedIndex()].AllergyDefNum;
			DialogResult=DialogResult.OK;
		}

		private void butClose_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}
	}
}