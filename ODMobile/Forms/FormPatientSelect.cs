using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlServerCe;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using OpenDentMobile.UI;

namespace OpenDentMobile {
	public partial class FormPatientSelect:Form {
		private DataTable PtDataTable;
		///<summary>After this form closes, if this value is anything other than 0, then the user has picked a patient.</summary>
		public int SelectedPatNum;

		public FormPatientSelect() {
			InitializeComponent();
		}

		private void Form1_KeyDown(object sender,KeyEventArgs e) {
			if((e.KeyCode == System.Windows.Forms.Keys.Up)) {
				// Up
			}
			if((e.KeyCode == System.Windows.Forms.Keys.Down)) {
				// Down
			}
			if((e.KeyCode == System.Windows.Forms.Keys.Left)) {
				// Left
			}
			if((e.KeyCode == System.Windows.Forms.Keys.Right)) {
				// Right
			}
			if((e.KeyCode == System.Windows.Forms.Keys.Enter)) {
				// Enter
			}

		}

		private void Form1_Load(object sender,EventArgs e) {
			//LoadIni();
			//LoadPatients();
			FillColumns();
		}

		private void FillColumns(){
			gridMain.BeginUpdate();
			gridMain.Columns.Clear();
			ODGridColumn col=new ODGridColumn("LName",70);
			gridMain.Columns.Add(col);
			col=new ODGridColumn("FName",70);
			gridMain.Columns.Add(col);
			col=new ODGridColumn("HmPhone",80);
			gridMain.Columns.Add(col);
			col=new ODGridColumn("Wireless",80);
			gridMain.Columns.Add(col);
			gridMain.EndUpdate();
		}

		private void LoadPatients(){
			if(textLName.Text.Length<2){
				MessageBox.Show("Please enter at least two letters of the last name.");
				return;
			}
			PtDataTable=Patients.GetPtDataTable(textLName.Text);
			gridMain.BeginUpdate();
			gridMain.Rows.Clear();
			ODGridRow row;
			for(int i=0;i<PtDataTable.Rows.Count;i++){
				row=new ODGridRow();
				row.Cells.Add(PtDataTable.Rows[i]["LName"].ToString());
				row.Cells.Add(PtDataTable.Rows[i]["FName"].ToString());
				row.Cells.Add(PtDataTable.Rows[i]["HmPhone"].ToString());
				row.Cells.Add(PtDataTable.Rows[i]["WirelessPhone"].ToString());
				gridMain.Rows.Add(row);
			}
			gridMain.EndUpdate();
			if(PtDataTable.Rows.Count==0){
				MessageBox.Show("No results");
			}
		}

		private void butSearch_Click(object sender,EventArgs e) {
			LoadPatients();
		}

		private void gridMain_CellClick(object sender,ODGridClickEventArgs e) {
			SelectedPatNum=PIn.PInt(PtDataTable.Rows[e.Row]["PatNum"].ToString());
			DialogResult=DialogResult.OK;
		}

		




	}
}