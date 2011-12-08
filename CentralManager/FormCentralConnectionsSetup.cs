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
	public partial class FormCentralConnectionsSetup:Form {
		private List<CentralConnection> ConnList;

		public FormCentralConnectionsSetup() {
			InitializeComponent();
		}

		private void FormAggPathSetup_Load(object sender,EventArgs e) {
			FillGrid();
		}

		private void FillGrid() {
			ConnList=CentralConnections.Refresh();
			gridMain.BeginUpdate();
			gridMain.Columns.Clear();
			ODGridColumn col;
			col=new ODGridColumn("#",40);
			gridMain.Columns.Add(col);
			col=new ODGridColumn("Database",320);
			gridMain.Columns.Add(col);
			col=new ODGridColumn("Note",300);
			gridMain.Columns.Add(col);
			gridMain.Rows.Clear();
			ODGridRow row;
			for(int i=0;i<ConnList.Count;i++) {
				row=new ODGridRow();
				row.Cells.Add(ConnList[i].ItemOrder.ToString());
				if(ConnList[i].DatabaseName=="") {//uri
					row.Cells.Add(ConnList[i].ServiceURI);
				}
				else {
					row.Cells.Add(ConnList[i].ServerName+", "+ConnList[i].DatabaseName);
				}
				row.Cells.Add(ConnList[i].Note);
				gridMain.Rows.Add(row);
			}
			gridMain.EndUpdate();
		}

		private void gridMain_CellDoubleClick(object sender,ODGridClickEventArgs e) {
			FormCentralConnectionEdit formC = new FormCentralConnectionEdit();
			formC.CentralConnectionCur=ConnList[e.Row];
			formC.ShowDialog();
			FillGrid();
		}

		private void butAdd_Click(object sender,EventArgs e) {
			FormCentralConnectionEdit formC = new FormCentralConnectionEdit();
			CentralConnection cc=new CentralConnection();
			cc.IsNew=true;;
			formC.CentralConnectionCur=cc;
			formC.ShowDialog();
			FillGrid();
		}

		private void butCancel_Click(object sender,EventArgs e) {
			Close();
		}



	}
}