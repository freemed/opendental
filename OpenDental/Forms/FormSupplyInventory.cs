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
	public partial class FormSupplyInventory:Form {
		private List<SupplyNeeded> listNeeded;

		public FormSupplyInventory() {
			InitializeComponent();
			Lan.F(this);
		}

		private void FormInventory_Load(object sender,EventArgs e) {
			FillGrid();
		}

		private void FillGrid(){
			listNeeded=SupplyNeededs.CreateObjects();
			gridNeeded.BeginUpdate();
			gridNeeded.Columns.Clear();
			ODGridColumn col=new ODGridColumn(Lan.g(this,"Date Added"),90);
			gridNeeded.Columns.Add(col);
			col=new ODGridColumn(Lan.g(this,"Description"),300);
			gridNeeded.Columns.Add(col);
			gridNeeded.Rows.Clear();
			ODGridRow row;
			for(int i=0;i<listNeeded.Count;i++){
				row=new ODGridRow();
				row.Cells.Add(listNeeded[i].DateAdded.ToShortDateString());
				row.Cells.Add(listNeeded[i].Description);
				gridNeeded.Rows.Add(row);
			}
			gridNeeded.EndUpdate();
		}

		private void gridNeeded_CellDoubleClick(object sender,ODGridClickEventArgs e) {
			FormSupplyNeededEdit FormS=new FormSupplyNeededEdit();
			FormS.Supp=listNeeded[e.Row];
			FormS.ShowDialog();
			if(FormS.DialogResult==DialogResult.OK) {
				FillGrid();
			}
		}

		private void butAdd_Click(object sender,EventArgs e) {
			SupplyNeeded supp=new SupplyNeeded();
			supp.IsNew=true;
			supp.DateAdded=DateTime.Today;
			FormSupplyNeededEdit FormS=new FormSupplyNeededEdit();
			FormS.Supp=supp;
			FormS.ShowDialog();
			if(FormS.DialogResult==DialogResult.OK){
				FillGrid();
			}
		}

		private void menuItemSetupSuppliers_Click(object sender,EventArgs e) {
			FormSuppliers FormS=new FormSuppliers();
			FormS.ShowDialog();
		}

		private void butClose_Click(object sender,EventArgs e) {
			Close();
		}

		

		

		

		
	}
}