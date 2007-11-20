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
	public partial class FormSupplies:Form {
		private List<Supply> listSupply;
		public List<Supplier> ListSupplier;
		///<Summary>Sets the supplier that will first show when opening this window.</Summary>
		public int SupplierNum;

		public FormSupplies() {
			InitializeComponent();
			Lan.F(this);
		}

		private void FormSupplies_Load(object sender,EventArgs e) {
			comboSupplier.Items.Clear();
			for(int i=0;i<ListSupplier.Count;i++) {
				comboSupplier.Items.Add(ListSupplier[i].Name);
				if(ListSupplier[i].SupplierNum==SupplierNum){
					comboSupplier.SelectedIndex=i;
				}
			}
			FillGrid();
		}

		private void FillGrid(){
			int supplier=0;
			if(comboSupplier.SelectedIndex!=-1) {
				supplier=ListSupplier[comboSupplier.SelectedIndex].SupplierNum;
			}
			listSupply=Supplies.CreateObjects(checkShowHidden.Checked,supplier);
			gridMain.BeginUpdate();
			gridMain.Columns.Clear();
			ODGridColumn col=new ODGridColumn(Lan.g(this,"Category"),130);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g(this,"Catalog #"),80);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g(this,"Description"),320);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g(this,"Price"),60,HorizontalAlignment.Right);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g(this,"StockQty"),60,HorizontalAlignment.Center);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g(this,"Hidden"),40,HorizontalAlignment.Center);
			gridMain.Columns.Add(col);
			gridMain.Rows.Clear();
			ODGridRow row;
			for(int i=0;i<listSupply.Count;i++) {
				row=new ODGridRow();
				if(i==0 || listSupply[i].Category!=listSupply[i-1].Category) {
					row.Cells.Add(DefB.GetName(DefCat.SupplyCats,listSupply[i].Category));
				}
				else {
					row.Cells.Add("");
				}
				row.Cells.Add(listSupply[i].CatalogNumber);
				row.Cells.Add(listSupply[i].Descript);
				if(listSupply[i].Price==0) {
					row.Cells.Add("");
				}
				else {
					row.Cells.Add(listSupply[i].Price.ToString("n"));
				}
				if(listSupply[i].LevelDesired==0) {
					row.Cells.Add("");
				}
				else {
					row.Cells.Add(listSupply[i].LevelDesired.ToString());
				}
				if(listSupply[i].IsHidden) {
					row.Cells.Add("X");
				}
				else {
					row.Cells.Add("");
				}
				gridMain.Rows.Add(row);
			}
			gridMain.EndUpdate();
		}

		private void butAdd_Click(object sender,EventArgs e) {
			if(comboSupplier.SelectedIndex==-1) {
				MsgBox.Show(this,"Please select a supplier first.");
				return;
			}
			Supply supp=new Supply();
			supp.IsNew=true;
			supp.SupplierNum=ListSupplier[comboSupplier.SelectedIndex].SupplierNum;
			FormSupplyEdit FormS=new FormSupplyEdit();
			FormS.Supp=supp;
			FormS.ListSupplier=ListSupplier;
			FormS.ShowDialog();
			if(FormS.DialogResult==DialogResult.OK) {
				FillGrid();
			}
		}

		private void gridMain_CellDoubleClick(object sender,ODGridClickEventArgs e) {
			FormSupplyEdit FormS=new FormSupplyEdit();
			FormS.Supp=listSupply[e.Row];
			FormS.ListSupplier=ListSupplier;
			FormS.ShowDialog();
			if(FormS.DialogResult==DialogResult.OK) {
				FillGrid();
			}
		}

		private void comboSupplier_SelectionChangeCommitted(object sender,EventArgs e) {
			FillGrid();
		}

		private void checkShowHidden_Click(object sender,EventArgs e) {
			FillGrid();
		}

		private void butClose_Click(object sender,EventArgs e) {
			Close();
		}

		

		

		

		

		
	}
}