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
	public partial class FormReference:Form {
		private DataTable RefTable;
		public List<CustReference> SelectedCustRefs;

		public FormReference() {
			InitializeComponent();
			Lan.F(this);
		}

		private void FormReference_Load(object sender,EventArgs e) {
			FillMain(true);
		}

		private void FillMain(bool limit) {
			int age=0;
			try {
				age=PIn.Int(textAge.Text);
			}
			catch { } 
			int superFam=0;
			try {
				superFam=PIn.Int(textSuperFamily.Text);
			}
			catch { }
			RefTable=CustReferences.GetReferenceTable(limit,checkBadRefs.Checked,checkUsedRefs.Checked,textCity.Text,textState.Text,
				textZip.Text,textAreaCode.Text,textSpecialty.Text,superFam,textLName.Text,textFName.Text,textPatNum.Text,age);
			gridMain.BeginUpdate();
			gridMain.Columns.Clear();
			ODGridColumn col=new ODGridColumn("PatNum",50);
			gridMain.Columns.Add(col);
			col=new ODGridColumn("First Name",75);
			gridMain.Columns.Add(col);
			col=new ODGridColumn("Last Name",75);
			gridMain.Columns.Add(col);
			col=new ODGridColumn("HmPhone",90);
			gridMain.Columns.Add(col);
			col=new ODGridColumn("State",45);
			gridMain.Columns.Add(col);
			col=new ODGridColumn("City",80);
			gridMain.Columns.Add(col);
			col=new ODGridColumn("Zip Code",60);
			gridMain.Columns.Add(col);
			col=new ODGridColumn("Specialty",90);
			gridMain.Columns.Add(col);
			col=new ODGridColumn("Age",40);
			gridMain.Columns.Add(col);
			col=new ODGridColumn("Super",50);
			col.TextAlign=HorizontalAlignment.Center;
			gridMain.Columns.Add(col);
			col=new ODGridColumn("Last Used",70);
			col.TextAlign=HorizontalAlignment.Center;
			gridMain.Columns.Add(col);
			col=new ODGridColumn("Times Used",70);
			col.TextAlign=HorizontalAlignment.Center;
			gridMain.Columns.Add(col);
			if(checkBadRefs.Checked) {
				col=new ODGridColumn("Bad",50);
				col.TextAlign=HorizontalAlignment.Center;
				gridMain.Columns.Add(col);
			}
			gridMain.Rows.Clear();
			ODGridRow row;
			for(int i=0;i<RefTable.Rows.Count;i++) {
				row=new ODGridRow();
				row.Cells.Add(RefTable.Rows[i]["PatNum"].ToString());
				row.Cells.Add(RefTable.Rows[i]["FName"].ToString());
				row.Cells.Add(RefTable.Rows[i]["LName"].ToString());
				row.Cells.Add(RefTable.Rows[i]["HmPhone"].ToString());
				row.Cells.Add(RefTable.Rows[i]["State"].ToString());
				row.Cells.Add(RefTable.Rows[i]["City"].ToString());
				row.Cells.Add(RefTable.Rows[i]["Zip"].ToString());
				row.Cells.Add(RefTable.Rows[i]["Specialty"].ToString());
				row.Cells.Add(RefTable.Rows[i]["age"].ToString());
				row.Cells.Add(RefTable.Rows[i]["SuperFamily"].ToString());
				row.Cells.Add(RefTable.Rows[i]["DateMostRecent"].ToString());
				row.Cells.Add(RefTable.Rows[i]["TimesUsed"].ToString());
				if(checkBadRefs.Checked) {
					row.Cells.Add(RefTable.Rows[i]["IsBadRef"].ToString());
				}
				gridMain.Rows.Add(row);
			}
			gridMain.EndUpdate();
			gridMain.SetSelected(0,true);
		}

		private void OnDataEntered() {
			if(checkRefresh.Checked) {
				FillMain(true);
			}
		}

		private void gridMain_CellDoubleClick(object sender,UI.ODGridClickEventArgs e) {
			CustReference refCur=CustReferences.GetOne(PIn.Long(RefTable.Rows[e.Row]["CustReferenceNum"].ToString()));
			FormReferenceEdit FormRE=new FormReferenceEdit(refCur);
			FormRE.ShowDialog();
		}

		private void checkUsedRefs_Click(object sender,EventArgs e) {
			OnDataEntered();
		}

		private void checkBadRefs_Click(object sender,EventArgs e) {
			OnDataEntered();
		}

		private void butSearch_Click(object sender,EventArgs e) {
			FillMain(true);
		}

		private void butGetAll_Click(object sender,EventArgs e) {
			FillMain(false);
		}

		#region TextChanged
		private void textCity_TextChanged(object sender,EventArgs e) {
			OnDataEntered();
		}

		private void textState_TextChanged(object sender,EventArgs e) {
			OnDataEntered();
		}

		private void textZip_TextChanged(object sender,EventArgs e) {
			OnDataEntered();
		}

		private void textAreaCode_TextChanged(object sender,EventArgs e) {
			OnDataEntered();
		}

		private void textSpecialty_TextChanged(object sender,EventArgs e) {
			OnDataEntered();
		}

		private void textSuperFamily_TextChanged(object sender,EventArgs e) {
			OnDataEntered();
		}

		private void textLName_TextChanged(object sender,EventArgs e) {
			OnDataEntered();
		}

		private void textFName_TextChanged(object sender,EventArgs e) {
			OnDataEntered();
		}

		private void textPatNum_TextChanged(object sender,EventArgs e) {
			OnDataEntered();
		}

		private void textAge_TextChanged(object sender,EventArgs e) {
			OnDataEntered();
		}
		#endregion

		#region KeyDown
		private void textCity_KeyDown(object sender,KeyEventArgs e) {
			if(e.KeyCode==Keys.Up || e.KeyCode==Keys.Down){
				gridMain_KeyDown(sender,e);
				gridMain.Invalidate();
				e.Handled=true;
			}
		}

		private void textState_KeyDown(object sender,KeyEventArgs e) {
			if(e.KeyCode==Keys.Up || e.KeyCode==Keys.Down){
				gridMain_KeyDown(sender,e);
				gridMain.Invalidate();
				e.Handled=true;
			}
		}

		private void textZip_KeyDown(object sender,KeyEventArgs e) {
			if(e.KeyCode==Keys.Up || e.KeyCode==Keys.Down){
				gridMain_KeyDown(sender,e);
				gridMain.Invalidate();
				e.Handled=true;
			}
		}

		private void textAreaCode_KeyDown(object sender,KeyEventArgs e) {
			if(e.KeyCode==Keys.Up || e.KeyCode==Keys.Down){
				gridMain_KeyDown(sender,e);
				gridMain.Invalidate();
				e.Handled=true;
			}
		}

		private void textSpecialty_KeyDown(object sender,KeyEventArgs e) {
			if(e.KeyCode==Keys.Up || e.KeyCode==Keys.Down){
				gridMain_KeyDown(sender,e);
				gridMain.Invalidate();
				e.Handled=true;
			}
		}

		private void textSuperFamily_KeyDown(object sender,KeyEventArgs e) {
			if(e.KeyCode==Keys.Up || e.KeyCode==Keys.Down){
				gridMain_KeyDown(sender,e);
				gridMain.Invalidate();
				e.Handled=true;
			}
		}

		private void textLName_KeyDown(object sender,KeyEventArgs e) {
			if(e.KeyCode==Keys.Up || e.KeyCode==Keys.Down){
				gridMain_KeyDown(sender,e);
				gridMain.Invalidate();
				e.Handled=true;
			}
		}

		private void textFName_KeyDown(object sender,KeyEventArgs e) {
			if(e.KeyCode==Keys.Up || e.KeyCode==Keys.Down){
				gridMain_KeyDown(sender,e);
				gridMain.Invalidate();
				e.Handled=true;
			}
		}

		private void textPatNum_KeyDown(object sender,KeyEventArgs e) {
			if(e.KeyCode==Keys.Up || e.KeyCode==Keys.Down){
				gridMain_KeyDown(sender,e);
				gridMain.Invalidate();
				e.Handled=true;
			}
		}

		private void textAge_KeyDown(object sender,KeyEventArgs e) {
			if(e.KeyCode==Keys.Up || e.KeyCode==Keys.Down){
				gridMain_KeyDown(sender,e);
				gridMain.Invalidate();
				e.Handled=true;
			}
		}

		private void gridMain_KeyDown(object sender,KeyEventArgs e) {

		}
		#endregion

		private void butOK_Click(object sender,EventArgs e) {
			if(gridMain.SelectedIndices.Length<1) {
				MsgBox.Show(this,"Select at least one reference.");
				return;
			}
			SelectedCustRefs=new List<CustReference>();
			for(int i=0;i<gridMain.SelectedIndices.Length;i++) {
				CustReference custRef=CustReferences.GetOne(PIn.Long(RefTable.Rows[gridMain.SelectedIndices[i]]["CustReferenceNum"].ToString()));
				custRef.DateMostRecent=DateTime.Now;
				CustReferences.Update(custRef);
				SelectedCustRefs.Add(custRef);
			}
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}
	}
}