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
	public partial class FormAggPathSetup:Form {
		private List<AggPath> AggPathList;

		public FormAggPathSetup() {
			InitializeComponent();
			Lan.F(this);
		}

		private void FormAggPathSetup_Load(object sender,EventArgs e) {
			FillAggPaths();
		}

		private void FillAggPaths() {
			AggPathList=AggPaths.Refresh();
			gridMain.BeginUpdate();
			gridMain.Columns.Clear();
			ODGridColumn col=new ODGridColumn("Remote URI",120);
			gridMain.Columns.Add(col);
			col=new ODGridColumn("User Name",120);
			gridMain.Columns.Add(col);
			col=new ODGridColumn("Password",120);
			gridMain.Columns.Add(col);
			gridMain.Rows.Clear();
			ODGridRow row;
			for(int i=0;i<AggPathList.Count;i++) {
				row=new ODGridRow();
				row.Cells.Add(AggPathList[i].RemoteURI);
				row.Cells.Add(AggPathList[i].RemoteUserName);
				row.Cells.Add(AggPathList[i].RemotePassword);
				gridMain.Rows.Add(row);
			}
			gridMain.EndUpdate();
		}

		private void gridMain_CellDoubleClick(object sender,ODGridClickEventArgs e) {
			FormAggPathEdit formAPE = new FormAggPathEdit();
			formAPE.AggPathCur=AggPathList[e.Row];
			formAPE.ShowDialog();
			FillAggPaths();
			if(formAPE.DialogResult!=DialogResult.OK) {
				return;
			}
			//AggPaths.Update(formAPE.AggPathCur);
			FillAggPaths();
		}

		private void butAdd_Click(object sender,EventArgs e) {
			FormAggPathEdit formAPE = new FormAggPathEdit();
			formAPE.IsNew=true;
			formAPE.AggPathCur=new AggPath();
			formAPE.ShowDialog();
			FillAggPaths();//this runs twice to handle deletions from FormAggPathEdit
			if(formAPE.DialogResult!=DialogResult.OK) {
				return;
			}
			//AggPaths.Insert(formAPE.AggPathCur);
			FillAggPaths();
		}

		private void butCancel_Click(object sender,EventArgs e) {
			Close();
		}

		private void butDelete_Click(object sender,EventArgs e) {
			for(int i=0;i<gridMain.SelectedIndices.Length;i++) {
				AggPaths.Delete(AggPathList[gridMain.SelectedIndices[i]].AggPathNum);
			}
			FillAggPaths();
		}


	}
}