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
	public partial class FormEquipment:Form {
		//private List<Supplier> listSuppliers;

		public FormEquipment() {
			InitializeComponent();
			Lan.F(this);
		}

		private void FormEquipment_Load(object sender,EventArgs e) {
			FillGrid();
		}

		private void FillGrid(){
			/*
			listSuppliers=Suppliers.CreateObjects();
			gridMain.BeginUpdate();
			gridMain.Columns.Clear();
			ODGridColumn col=new ODGridColumn(Lan.g(this,"Name"),110);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g(this,"Phone"),90);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g(this,"CustomerID"),80);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g(this,"Website"),180);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g(this,"UserName"),80);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g(this,"Password"),80);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g(this,"Note"),150);
			gridMain.Columns.Add(col);
			gridMain.Rows.Clear();
			ODGridRow row;
			for(int i=0;i<listSuppliers.Count;i++){
				row=new ODGridRow();
				row.Cells.Add(listSuppliers[i].Name);
				row.Cells.Add(listSuppliers[i].Phone);
				row.Cells.Add(listSuppliers[i].CustomerId);
				row.Cells.Add(listSuppliers[i].Website);
				row.Cells.Add(listSuppliers[i].UserName);
				row.Cells.Add(listSuppliers[i].Password);
				row.Cells.Add(listSuppliers[i].Note);
				gridMain.Rows.Add(row);
			}
			gridMain.EndUpdate();*/
		}

		private void butAdd_Click(object sender,EventArgs e) {
			/*
			Supplier supp=new Supplier();
			supp.IsNew=true;
			FormSupplierEdit FormS=new FormSupplierEdit();
			FormS.Supp=supp;
			FormS.ShowDialog();
			if(FormS.DialogResult==DialogResult.OK) {
				FillGrid();
			}*/
		}

		private void gridMain_CellDoubleClick(object sender,ODGridClickEventArgs e) {
			/*
			FormSupplierEdit FormS=new FormSupplierEdit();
			FormS.Supp=listSuppliers[e.Row];
			FormS.ShowDialog();
			if(FormS.DialogResult==DialogResult.OK) {
				FillGrid();
			}*/
		}

		private void butClose_Click(object sender,EventArgs e) {
			Close();
		}

		

		

		
	}
}