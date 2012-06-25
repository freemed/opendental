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
		private List<Supply> listSupply = new List<Supply>();
		/// <summary>Used to cache all supply information to reduce load on DB server.</summary>
		private List<Supply> listSupplyAll;
		public List<Supplier> ListSupplier;
		///<Summary>Sets the supplier that will first show when opening this window.</Summary>
		public long SupplierNum;
		public bool IsSelectMode = false;
		public Supply SelectedSupply;

		public FormSupplies() {
			InitializeComponent();
			Lan.F(this);
			listSupplyAll=Supplies.GetAll();
			FillGrid();
		}

		private void FormSupplies_Load(object sender,EventArgs e) {
			fillComboSupplier();
			FillGrid();
			if(IsSelectMode) {
				comboSupplier.Enabled=false;
				gridMain.SelectionMode=GridSelectionMode.One;
			}
		}

		private void fillComboSupplier() {
			ListSupplier=Suppliers.CreateObjects();
			comboSupplier.Items.Clear();
			comboSupplier.Items.Add("All");
			for(int i=0;i<ListSupplier.Count;i++) {
				comboSupplier.Items.Add("#"+ListSupplier[i].SupplierNum+" "+ListSupplier[i].Name);
				if(ListSupplier[i].SupplierNum==SupplierNum) {
					comboSupplier.SelectedIndex=i+1;
				}
			}
		}

		private void FillGrid(){
			filterListSupply();
			gridMain.BeginUpdate();
			gridMain.Columns.Clear();
			ODGridColumn col=new ODGridColumn(Lan.g(this,"Category"),130);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g(this,"Catalog #"),80);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g(this,"Supplier #"),60);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g(this,"Description"),260);
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
				row.Cells.Add(listSupply[i].SupplierNum.ToString());
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
				row.Tag=listSupply[i].SupplyNum;
				//row.Cells.Add(listSupply[i].ItemOrder.ToString());
				gridMain.Rows.Add(row);
			}
			gridMain.EndUpdate();
		}

		private void butAdd_Click(object sender,EventArgs e) {
			if(comboSupplier.SelectedIndex < 1) {//Includes no items or the ALL item being selected
				MsgBox.Show(this,"Please select a supplier first.");
				return;
			}
			Supply supp=new Supply();
			supp.IsNew=true;
			supp.SupplierNum=ListSupplier[comboSupplier.SelectedIndex-1].SupplierNum;//Selected index -1 to account for ALL being at the top of the list.
			FormSupplyEdit FormS=new FormSupplyEdit();
			FormS.Supp=supp;
			FormS.ListSupplier=ListSupplier;
			FormS.ShowDialog();
			if(FormS.DialogResult!=DialogResult.OK) {
				return;
			}
			listSupplyAll = Supplies.GetAll();//refresh supply list because something changed.
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
			if(IsSelectMode) {
				SelectedSupply = Supplies.GetSupply((long)gridMain.Rows[e.Row].Tag);
				DialogResult = DialogResult.OK;
				return;
			}
			FormSupplyEdit FormS=new FormSupplyEdit();
			FormS.Supp=Supplies.GetSupply((long)gridMain.Rows[e.Row].Tag);//works with sorting
			FormS.ListSupplier=ListSupplier;
			FormS.ShowDialog();
			if(FormS.DialogResult!=DialogResult.OK) {
				return;	
			}
			listSupplyAll = Supplies.GetAll();//refresh the master list because the supplies changed.
			long selected=(long)gridMain.Rows[e.Row].Tag;
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

		/*private void butUp_Click(object sender,EventArgs e) {
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
			for(int i=0;i<gridMain.SelectedIndices.Length;i++) {
				if(listSupply[gridMain.SelectedIndices[0]].Category!=listSupply[gridMain.SelectedIndices[i]].Category) {
					MsgBox.Show(this,"You may only move items that are in the same category.");
					return;
				}
			}
			if(gridMain.SelectedIndices[0]==0
		    || listSupply[gridMain.SelectedIndices[0]].Category!=listSupply[gridMain.SelectedIndices[0]-1].Category) {
				return;//already at the top
			}
			//remember the selected SupplyNums for rehighlighting later.
			List<long> selectedSupplyNums=new List<long>();
			for(int i=0;i<gridMain.SelectedIndices.Length;i++) {
				selectedSupplyNums.Add(listSupply[gridMain.SelectedIndices[i]].SupplyNum);
			}
			int scrollVal=gridMain.ScrollValue;
			//change all the appropriate itemorders
			for(int i=0;i<gridMain.SelectedIndices.Length;i++) {//loop from the top down
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
			for(int i=0;i<listSupply.Count;i++) {
				if(selectedSupplyNums.Contains(listSupply[i].SupplyNum)) {
					gridMain.SetSelected(i,true);
				}
			}
			gridMain.ScrollValue=scrollVal;
		}*/

		/*private void butDown_Click(object sender,EventArgs e) {
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
			for(int i=0;i<gridMain.SelectedIndices.Length;i++) {
				if(listSupply[gridMain.SelectedIndices[0]].Category!=listSupply[gridMain.SelectedIndices[i]].Category) {
					MsgBox.Show(this,"You may only move items that are in the same category.");
					return;
				}
			}
			if(gridMain.SelectedIndices[gridMain.SelectedIndices.Length-1]==listSupply.Count-1
		    || listSupply[gridMain.SelectedIndices[gridMain.SelectedIndices.Length-1]].Category
		    !=listSupply[gridMain.SelectedIndices[gridMain.SelectedIndices.Length-1]+1].Category) {
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
		}*/
		
		private void textFind_TextChanged(object sender,EventArgs e) {
			if(!Regex.IsMatch(textFind.Text,@"^[0-9a-zA-Z]*$")) {
				textFind.BackColor=Color.LightPink;
				return;
			}
			textFind.BackColor=SystemColors.Window;
			FillGrid();
		}

		/// <summary> Empties listSupply and adds to it all elements that contain items in the search field. Matches on all columns simultaneously.</summary>
		private void filterListSupply() {
			listSupply.Clear();
			long supplier=0;
			if(SupplierNum!=0) {//Use supplier nume if it is provided, usually when IsSelectMode is also true
				supplier = SupplierNum;
			}
			else if(comboSupplier.SelectedIndex < 1) {//this includes selecting All or not having anything selected.
				supplier = 0;
			}
			else {
				supplier=ListSupplier[comboSupplier.SelectedIndex-1].SupplierNum;//SelectedIndex-1 because All is added before all the other items in the list.
			}
			foreach(Supply supply in listSupplyAll) {
				if(!checkShowHidden.Checked && supply.IsHidden) {//skip hidden supplies if show hidden is not 
					continue;
				}
				if(supplier!=0 && supply.SupplierNum!=supplier) {//skip supplies that do not match selected supplier
					continue;
				}
				if(textFind.Text=="") {//Start filtering based on findText
					listSupply.Add(supply);
					continue;
				}
				else if(supply.CatalogNumber.ToString().ToUpper().Contains(textFind.Text.ToUpper())) {//Check each field to see if it matches the search text field. If it does then add it and move on.
					listSupply.Add(supply);
					continue;
				}
				else if(DefC.GetName(DefCat.SupplyCats,supply.Category).ToUpper().Contains(textFind.Text.ToUpper())) {
					listSupply.Add(supply);
					continue;
				}
				else if(supply.Descript.ToString().ToUpper().Contains(textFind.Text.ToUpper())) {
					listSupply.Add(supply);
					continue;
				}
				//else if(supply.ItemOrder.ToString().Contains(textFind.Text)) {
				//  listSupply.Add(supply);
				//  continue;
				//}
				else if(supply.LevelDesired.ToString().Contains(textFind.Text)) {
					listSupply.Add(supply);
					continue;
				}
				else if(supply.Price.ToString().ToUpper().Contains(textFind.Text.ToUpper())) {
					listSupply.Add(supply);
					continue;
				}
				else if(supply.SupplierNum.ToString().Contains(textFind.Text)) {
					listSupply.Add(supply);
					continue;
				}
				//else if(supply.SupplyNum.ToString().Contains(textFind.Text)) {
				//  listSupply.Add(supply);
				//  continue;
				//}
				//end of filter, item not added, move to next supply item.
			}
			return;
		}

		private void butPrint_Click(object sender,EventArgs e) {
		}

		private void butOK_Click(object sender,EventArgs e) {
			if(IsSelectMode) {
				if(gridMain.SelectedIndices.Length<1) {
					MsgBox.Show(this,"Please select a supply from the list first.");
					return;
				}
				SelectedSupply = Supplies.GetSupply((long)gridMain.Rows[gridMain.SelectedIndices[0]].Tag);
				//TODO:I don't know what type of object needs to be passed to where.
			}
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		
	

		

		

		

		

		

		
	}
}