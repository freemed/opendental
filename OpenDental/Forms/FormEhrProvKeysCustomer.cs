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
	public partial class FormEhrProvKeysCustomer:Form {
		private List<EhrProvKey> listKeys;
		public long Guarantor;

		public FormEhrProvKeysCustomer() {
			InitializeComponent();
			Lan.F(this);
		}

		private void FormEhrProvKeysCustomer_Load(object sender,EventArgs e) {
			FillGrid();
		}

		private void FillGrid(){
			gridMain.BeginUpdate();
			gridMain.Columns.Clear();
			ODGridColumn col=new ODGridColumn(Lan.g(this,"LName"),120);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g(this,"FName"),120);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g(this,"Key"),120);
			gridMain.Columns.Add(col);
			//todo: add more columns	
			listKeys=EhrProvKeys.RefreshForFam(Guarantor);
			gridMain.Rows.Clear();
			ODGridRow row;
			for(int i=0;i<listKeys.Count;i++) {
				row=new ODGridRow();
				row.Cells.Add(listKeys[i].LName);
				row.Cells.Add(listKeys[i].FName);
				row.Cells.Add(listKeys[i].ProvKey);
				gridMain.Rows.Add(row);
			}
			gridMain.EndUpdate();
		}

		private void gridMain_CellDoubleClick(object sender,UI.ODGridClickEventArgs e) {
			FormEhrProvKeyEditCust formK=new FormEhrProvKeyEditCust();
			formK.KeyCur=listKeys[e.Row];
			formK.ShowDialog();
			FillGrid();
		}

		private void butAdd_Click(object sender,EventArgs e) {
			FormEhrProvKeyEditCust formK=new FormEhrProvKeyEditCust();
			formK.KeyCur=new EhrProvKey();
			formK.KeyCur.PatNum=Guarantor;
			formK.KeyCur.IsNew=true;
			formK.ShowDialog();
			FillGrid();
		}

		private void butClose_Click(object sender,EventArgs e) {
			Close();
		}

	

		

		
	}
}