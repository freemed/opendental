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
		}

		private void LoadPatients(){
			if(textLName.Text.Length<2){
				MessageBox.Show("Please enter at least two letters of the last name.");
				return;
			}
			listView.Items.Clear();
			PtDataTable=Patients.GetPtDataTable(textLName.Text);
			if(PtDataTable.Rows.Count==0){
				MessageBox.Show("No results");
			}
			ListViewItem row;
			for(int i=0;i<PtDataTable.Rows.Count;i++){
				row=new ListViewItem();
				row.Text=PtDataTable.Rows[i]["LName"].ToString();
				row.SubItems.Add(PtDataTable.Rows[i]["FName"].ToString());
				row.SubItems.Add(PtDataTable.Rows[i]["HmPhone"].ToString());
				listView.Items.Add(row);
			}
		}

		private void butSearch_Click(object sender,EventArgs e) {
			LoadPatients();
		}

		private void listView_ItemActivate(object sender,EventArgs e) {
			//MessageBox.Show(listView.SelectedIndices[0].ToString());
			SelectedPatNum=PIn.PInt(PtDataTable.Rows[listView.SelectedIndices[0]]["PatNum"].ToString());
			DialogResult=DialogResult.OK;
		}

		




	}
}