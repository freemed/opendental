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
		private List<Supply> listSupply;
		private List<Supplier> listSupplier;

		public FormSupplyInventory() {
			InitializeComponent();
			Lan.F(this);
		}

		private void FormInventory_Load(object sender,EventArgs e) {
			FillSuppliers();
			FillGridNeeded();
			//FillGridSupply();User will have to pick a supplier to fill this grid
		}

		private void FillSuppliers(){
			listSupplier=Suppliers.CreateObjects();
			comboSupplier.Items.Clear();
			for(int i=0;i<listSupplier.Count;i++){
				comboSupplier.Items.Add(listSupplier[i].Name);
			}
		}

		private void FillGridNeeded(){
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
				FillGridNeeded();
			}
		}

		private void butAddNeeded_Click(object sender,EventArgs e) {
			SupplyNeeded supp=new SupplyNeeded();
			supp.IsNew=true;
			supp.DateAdded=DateTime.Today;
			FormSupplyNeededEdit FormS=new FormSupplyNeededEdit();
			FormS.Supp=supp;
			FormS.ShowDialog();
			if(FormS.DialogResult==DialogResult.OK){
				FillGridNeeded();
			}
		}

		private void FillGridSupply(){
			int supplier=0;
			if(comboSupplier.SelectedIndex!=-1){
				supplier=listSupplier[comboSupplier.SelectedIndex].SupplierNum;
			}
			listSupply=Supplies.CreateObjects(checkShowHidden.Checked,supplier);
			gridSupply.BeginUpdate();
			gridSupply.Columns.Clear();
			//ODGridColumn col=new ODGridColumn(Lan.g(this,"Date Added"),90);
			//gridSupply.Columns.Add(col);
			//col=new ODGridColumn(Lan.g(this,"Description"),300);
			//gridSupply.Columns.Add(col);
			gridSupply.Rows.Clear();
			/*ODGridRow row;
			for(int i=0;i<listNeeded.Count;i++){
				row=new ODGridRow();
				row.Cells.Add(listNeeded[i].DateAdded.ToShortDateString());
				row.Cells.Add(listNeeded[i].Description);
				gridSupply.Rows.Add(row);
			}*/
			gridSupply.EndUpdate();
		}

		private void gridSupply_CellDoubleClick(object sender,ODGridClickEventArgs e) {
			/*FormSupplyNeededEdit FormS=new FormSupplyNeededEdit();
			FormS.Supp=listNeeded[e.Row];
			FormS.ShowDialog();
			if(FormS.DialogResult==DialogResult.OK) {
				FillGridNeeded();
			}*/
		}

		private void butAddSupply_Click(object sender,EventArgs e) {
			if(listSupplier.Count==0){
				MsgBox.Show(this,"Please add suppliers first.  Use the menu at the top of this window.");
				return;
			}
			if(comboSupplier.SelectedIndex==-1) {
				MsgBox.Show(this,"Please select a supplier first.");
				return;
			}
			/*SupplyNeeded supp=new SupplyNeeded();
			supp.IsNew=true;
			supp.DateAdded=DateTime.Today;
			FormSupplyNeededEdit FormS=new FormSupplyNeededEdit();
			FormS.Supp=supp;
			FormS.ShowDialog();
			if(FormS.DialogResult==DialogResult.OK) {
				FillGridNeeded();
			}*/
		}

		private void checkShowHidden_Click(object sender,EventArgs e) {
			FillGridSupply();
		}

		private void comboSupplier_SelectionChangeCommitted(object sender,EventArgs e) {
			FillGridSupply();
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