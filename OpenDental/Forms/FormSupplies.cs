using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using OpenDental.UI;
using OpenDentBusiness;

namespace OpenDental {
	public partial class FormSupplies:Form {
		private List<Supply> listSupply;
		public List<Supplier> ListSupplier;
		///<Summary>Sets the supplier that will first show when opening this window.</Summary>
		public int SupplierNum;
		///<Summary>Will be true if the displayed data was obtained with the find text empty.</Summary>
		private bool IsCleanRefresh;

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
			IsCleanRefresh=true;
			FillGrid();
		}

		private void FillGrid(){
			long supplier=0;
			if(comboSupplier.SelectedIndex!=-1) {
				supplier=ListSupplier[comboSupplier.SelectedIndex].SupplierNum;
			}
			listSupply=Supplies.CreateObjects(checkShowHidden.Checked,supplier,textFind.Text);
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
			col=new ODGridColumn(Lan.g(this,"IsHidden"),40,HorizontalAlignment.Center);
			gridMain.Columns.Add(col);
			gridMain.Rows.Clear();
			ODGridRow row;
			for(int i=0;i<listSupply.Count;i++) {
				row=new ODGridRow();
				if(i==0 || listSupply[i].Category!=listSupply[i-1].Category) {
					row.Cells.Add(DefC.GetName(DefCat.SupplyCats,listSupply[i].Category));
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
				//row.Cells.Add(listSupply[i].ItemOrder.ToString());
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
			if(FormS.DialogResult!=DialogResult.OK) {
				return;
			}
			long selected=FormS.Supp.SupplyNum;
			int scroll=gridMain.ScrollValue;
			FillGrid();
			gridMain.ScrollValue=scroll;
			for(int i=0;i<listSupply.Count;i++) {
				if(listSupply[i].SupplyNum==selected) {
					gridMain.SetSelected(i,true);
				}
			}
		}

		private void gridMain_CellDoubleClick(object sender,ODGridClickEventArgs e) {
			FormSupplyEdit FormS=new FormSupplyEdit();
			FormS.Supp=listSupply[e.Row];
			FormS.ListSupplier=ListSupplier;
			FormS.ShowDialog();
			if(FormS.DialogResult!=DialogResult.OK) {
				return;	
			}
			long selected=listSupply[e.Row].SupplyNum;
			int scroll=gridMain.ScrollValue;
			FillGrid();
			gridMain.ScrollValue=scroll;
			for(int i=0;i<listSupply.Count;i++){
				if(listSupply[i].SupplyNum==selected){
					gridMain.SetSelected(i,true);
				}
			}
		}

		private void comboSupplier_SelectionChangeCommitted(object sender,EventArgs e) {
			FillGrid();
		}

		private void checkShowHidden_Click(object sender,EventArgs e) {
			FillGrid();
		}

		private void butUp_Click(object sender,EventArgs e) {
			if(!IsCleanRefresh) {
				MsgBox.Show(this,"Please perform a clean refresh first without any find text.");
				return;
			}
			if(gridMain.SelectedIndices.Length==0) {
				MsgBox.Show(this,"You must first select at least one row.");
				return;
			}
			textFind.Text="";
			if(Supplies.CleanupItemOrders(listSupply)) {
				MsgBox.Show(this,"There was a problem with sorting, but it has been fixed.  You may now try again.");
				FillGrid();
				return;
			}
			for(int i=0;i<gridMain.SelectedIndices.Length;i++){
				if(listSupply[gridMain.SelectedIndices[0]].Category!=listSupply[gridMain.SelectedIndices[i]].Category){
					MsgBox.Show(this,"You may only move items that are in the same category.");
					return;
				}
			}
			if(gridMain.SelectedIndices[0]==0
				|| listSupply[gridMain.SelectedIndices[0]].Category!=listSupply[gridMain.SelectedIndices[0]-1].Category)
			{
				return;//already at the top
			}
			//remember the selected SupplyNums for rehighlighting later.
			List<long> selectedSupplyNums=new List<long>();
			for(int i=0;i<gridMain.SelectedIndices.Length;i++){
				selectedSupplyNums.Add(listSupply[gridMain.SelectedIndices[i]].SupplyNum);
			}
			int scrollVal=gridMain.ScrollValue;
			//change all the appropriate itemorders
			for(int i=0;i<gridMain.SelectedIndices.Length;i++){//loop from the top down
				//move the one above it down
				listSupply[gridMain.SelectedIndices[i]-1].ItemOrder++;
				Supplies.Update(listSupply[gridMain.SelectedIndices[i]-1]);
				//move this one up
				listSupply[gridMain.SelectedIndices[i]].ItemOrder--;
				Supplies.Update(listSupply[gridMain.SelectedIndices[i]]);
				//keep the list uptodate
				listSupply.Reverse(gridMain.SelectedIndices[i]-1,2);
			}
			FillGrid();
			//reselect the original supplyNums
			for(int i=0;i<listSupply.Count;i++){
				if(selectedSupplyNums.Contains(listSupply[i].SupplyNum)){
					gridMain.SetSelected(i,true);
				}
			}
			gridMain.ScrollValue=scrollVal;
		}

		private void butDown_Click(object sender,EventArgs e) {
			if(!IsCleanRefresh){
				MsgBox.Show(this,"Please perform a clean refresh first without any find text.");
				return;
			}
			if(gridMain.SelectedIndices.Length==0) {
				MsgBox.Show(this,"You must first select at least one row.");
				return;
			}
			textFind.Text="";
			if(Supplies.CleanupItemOrders(listSupply)){
				MsgBox.Show(this,"There was a problem with sorting, but it has been fixed.  You may now try again.");
				FillGrid();
				return;
			}
			for(int i=0;i<gridMain.SelectedIndices.Length;i++) {
				if(listSupply[gridMain.SelectedIndices[0]].Category!=listSupply[gridMain.SelectedIndices[i]].Category) {
					MsgBox.Show(this,"You may only move items that are in the same category.");
					return;
				}
			}
			if(gridMain.SelectedIndices[gridMain.SelectedIndices.Length-1]==listSupply.Count-1
				|| listSupply[gridMain.SelectedIndices[gridMain.SelectedIndices.Length-1]].Category
				!=listSupply[gridMain.SelectedIndices[gridMain.SelectedIndices.Length-1]+1].Category) 
			{
				return;//already at the bottom
			}
			//remember the selected SupplyNums for rehighlighting later.
			List<long> selectedSupplyNums=new List<long>();
			for(int i=0;i<gridMain.SelectedIndices.Length;i++) {
				selectedSupplyNums.Add(listSupply[gridMain.SelectedIndices[i]].SupplyNum);
			}
			int scrollVal=gridMain.ScrollValue;
			//change all the appropriate itemorders in the main list
			for(int i=gridMain.SelectedIndices.Length-1;i>=0;i--) {//loop from the bottom up
				//move the one below it up
				listSupply[gridMain.SelectedIndices[i]+1].ItemOrder--;
				Supplies.Update(listSupply[gridMain.SelectedIndices[i]+1]);
				//move this one down
				listSupply[gridMain.SelectedIndices[i]].ItemOrder++;
				Supplies.Update(listSupply[gridMain.SelectedIndices[i]]);
				//keep the list uptodate
				listSupply.Reverse(gridMain.SelectedIndices[i],2);
			}
			FillGrid();
			//reselect the original supplyNums
			for(int i=0;i<listSupply.Count;i++) {
				if(selectedSupplyNums.Contains(listSupply[i].SupplyNum)) {
					gridMain.SetSelected(i,true);
				}
			}
			gridMain.ScrollValue=scrollVal;
		}

		private void textFind_KeyDown(object sender,KeyEventArgs e) {
			if(e.KeyCode==Keys.Enter){
				butRefresh_Click(this,new EventArgs());
			}
		}

		private void butRefresh_Click(object sender,EventArgs e) {
			if(!Regex.IsMatch(textFind.Text,@"^[0-9a-zA-Z]*$")){
				MsgBox.Show(this,"No special characters are allowed in the find text.");
				return;
			}
			IsCleanRefresh=textFind.Text=="";
			FillGrid();
		}

		private void butClose_Click(object sender,EventArgs e) {
			Close();
		}

		
	

		

		

		

		

		

		
	}
}