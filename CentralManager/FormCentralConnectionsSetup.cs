using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using OpenDentBusiness;
using OpenDental.UI;

namespace CentralManager {
	public partial class FormAggPathSetup:Form {
		private List<CentralConnection> AggPathList;

		public FormAggPathSetup() {
			InitializeComponent();
		}

		private void FormAggPathSetup_Load(object sender,EventArgs e) {
			FillAggPaths();
		}

		private void FillAggPaths() {
			//AggPathList=AggPaths.Refresh();
			gridMain.BeginUpdate();
			gridMain.Columns.Clear();
			ODGridColumn col=new ODGridColumn("Remote URI",320);
			gridMain.Columns.Add(col);
			col=new ODGridColumn("User Name",120);
			gridMain.Columns.Add(col);
			col=new ODGridColumn("Password",120);
			gridMain.Columns.Add(col);
			gridMain.Rows.Clear();
			ODGridRow row;
			for(int i=0;i<AggPathList.Count;i++) {
				row=new ODGridRow();
				//row.Cells.Add(AggPathList[i].RemoteURI);
				//row.Cells.Add(AggPathList[i].RemoteUserName);
				//row.Cells.Add(AggPathList[i].RemotePassword);
				gridMain.Rows.Add(row);
			}
			gridMain.EndUpdate();
		}

		private void gridMain_CellDoubleClick(object sender,ODGridClickEventArgs e) {
			FormCentralConnectionEdit formC = new FormCentralConnectionEdit();
			formC.CentralConnectionCur=AggPathList[e.Row];
			formC.ShowDialog();
			FillAggPaths();
		}

		private void butAdd_Click(object sender,EventArgs e) {
			//FormAggPathEdit formAPE = new FormAggPathEdit();
			//formAPE.IsNew=true;
			//formAPE.CentralConnectionCur=new CentralConnection();
			//formAPE.ShowDialog();
			//FillAggPaths();
		}

		private void butCancel_Click(object sender,EventArgs e) {
			Close();
		}



	}
}