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
		private List<Supply> ListSupply = new List<Supply>();
		/// <summary>Used to cache all supply information to reduce load on DB server.</summary>
		private List<Supply> ListSupplyAll;
		public List<Supplier> ListSupplier;
		///<Summary>Sets the supplier that will first show when opening this window.</Summary>
		public long SupplierNum;
		public bool IsSelectMode;
		public Supply SelectedSupply;
		///<summary>This is is only used locally to cache the selected indicies of the main grid.</summary>
		private List<long> SelectedGridItems = new List<long>();

		public FormSupplies() {
			InitializeComponent();
			Lan.F(this);
			ListSupplyAll=Supplies.GetAll();
			FillGrid();
		}

		private void FormSupplies_Load(object sender,EventArgs e) {
			fillComboSupplier();
			FillGrid();
			butUp.Enabled=false;
			butUp.Visible=false;
			butDown.Enabled=false;
			butDown.Visible=false;
			if(IsSelectMode) {
				comboSupplier.Enabled=false;
				checkSort.Enabled=false;
				checkSort.Visible=false;
				gridMain.SelectionMode=GridSelectionMode.One;
			}
		}

		private void fillComboSupplier() {
			ListSupplier=Suppliers.CreateObjects();
			comboSupplier.Items.Clear();
			comboSupplier.Items.Add(Lan.g(this,"All"));
			comboSupplier.SelectedIndex=0;//default to "All" otherwise selected index will be selected below.
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
			for(int i=0;i<ListSupply.Count;i++) {
				row=new ODGridRow();
				if(i==0 || ListSupply[i].Category!=ListSupply[i-1].Category) {
					row.Cells.Add(DefC.GetName(DefCat.SupplyCats,ListSupply[i].Category));
				}
				else {
					row.Cells.Add("");
				}
				row.Cells.Add(ListSupply[i].CatalogNumber);
				row.Cells.Add(ListSupply[i].SupplierNum.ToString());
				row.Cells.Add(ListSupply[i].Descript);
				if(ListSupply[i].Price==0) {
					row.Cells.Add("");
				}
				else {
					row.Cells.Add(ListSupply[i].Price.ToString("n"));
				}
				if(ListSupply[i].LevelDesired==0) {
					row.Cells.Add("");
				}
				else {
					row.Cells.Add(ListSupply[i].LevelDesired.ToString());
				}
				if(ListSupply[i].IsHidden) {
					row.Cells.Add("X");
				}
				else {
					row.Cells.Add("");
				}
				row.Tag=ListSupply[i].SupplyNum;
				//row.Cells.Add(listSupply[i].ItemOrder.ToString());
				gridMain.Rows.Add(row);
			}
			gridMain.EndUpdate();
			for(int i=0;i<ListSupply.Count;i++) {
				if(SelectedGridItems.Contains(ListSupply[i].SupplyNum)) {
					gridMain.SetSelected(i,true);
				}
			}
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
			ListSupplyAll=Supplies.GetAll();//refresh supply list because something changed.
			long selected=FormS.Supp.SupplyNum;
			int scroll=gridMain.ScrollValue;
			SelectedGridItems.Clear();
			SelectedGridItems.Add(FormS.Supp.SupplyNum);
			FillGrid();
			gridMain.ScrollValue=scroll;
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
			ListSupplyAll=Supplies.GetAll();//refresh the master list because the supplies changed.
			long selected=(long)gridMain.Rows[e.Row].Tag;
			int scroll=gridMain.ScrollValue;
			FillGrid();
			gridMain.ScrollValue=scroll;
			for(int i=0;i<ListSupply.Count;i++){
				if(ListSupply[i].SupplyNum==selected){
					gridMain.SetSelected(i,true);
				}
			}
		}

		private void comboSupplier_SelectionChangeCommitted(object sender,EventArgs e) {
			if(comboSupplier.SelectedIndex>0) {//supplier selected
				butUp.Enabled=false;
				butDown.Enabled=false;
			}
			else {//either no supplier or "All" supplier selected
				butUp.Enabled=true;
				butDown.Enabled=true;
			}
			SelectedGridItems.Clear();
			foreach(int index in gridMain.SelectedIndices) {
				SelectedGridItems.Add(ListSupply[index].SupplyNum);
			}
			FillGrid();
		}

		private void checkShowHidden_Click(object sender,EventArgs e) {
			SelectedGridItems.Clear();
			foreach(int index in gridMain.SelectedIndices) {
				SelectedGridItems.Add(ListSupply[index].SupplyNum);
			}
			FillGrid();
		}

		private void butUp_Click(object sender,EventArgs e) {
			//Validation is not optimized for speed. It is broken into sections so that more important error messages are provided first.
			//Selected items are in different categories
			foreach(int i in gridMain.SelectedIndices) {
				if(ListSupply[i].Category!=ListSupply[gridMain.SelectedIndices[0]].Category) {
					MsgBox.Show(this,"You may only move items that are in the same category.");
					return;
				}
			}
			//Nothing Selected
			if(gridMain.SelectedIndices.Length==0) {
				return;
			}
			//Selected items already at top of list or category
			foreach(int i in gridMain.SelectedIndices) {
				if(i==0 || ListSupply[i].ItemOrder==0) {
					return;
				}
			}
			//remember selected SupplyNums for later
			SelectedGridItems.Clear();
			foreach(int index in gridMain.SelectedIndices) {
				SelectedGridItems.Add(ListSupply[index].SupplyNum);
			}
			if(Supplies.CleanupItemOrders(ListSupply)) {
				MsgBox.Show(this,"There was a problem with sorting, but it has been fixed.  You may now try again.");
				FillGrid();
				return;
			}
			//Begin shifting elements
			int scrollVal=gridMain.ScrollValue;
			//change all the appropriate itemorders
			for(int i=0;i<gridMain.SelectedIndices.Length;i++) {//loop from the top down
				ListSupply[gridMain.SelectedIndices[i]].ItemOrder-=1;//move selected item up in the list
				Supplies.Update(ListSupply[gridMain.SelectedIndices[i]]);//update item order
				ListSupply[gridMain.SelectedIndices[i]-1].ItemOrder+=1;//selected item moves up
				Supplies.Update(ListSupply[gridMain.SelectedIndices[i]-1]);//update item order
			}
			//keep the list uptodate
			ListSupplyAll=Supplies.GetAll();
			FillGrid();
			gridMain.ScrollValue=scrollVal;
		}

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
			//TODO: standardize this validation. i.e. use validation provider?
			if(!Regex.IsMatch(textFind.Text,@"^[0-9a-zA-Z]*$")) {
				textFind.BackColor=Color.LightPink;
				return;
			}
			textFind.BackColor=SystemColors.Window;
			if(textFind.Text!="" || IsSelectMode) {
				butUp.Enabled=false;
				butDown.Enabled=false;
			}
			else {
				butUp.Enabled=true;
				butDown.Enabled=true;
			}
			SelectedGridItems.Clear();
			foreach(int index in gridMain.SelectedIndices) {
				SelectedGridItems.Add(ListSupply[index].SupplyNum);
			}
			FillGrid();
		}

		/// <summary> Empties listSupply and adds to it all elements that contain items in the search field. Matches on all columns simultaneously.</summary>
		private void filterListSupply() {
			ListSupply.Clear();
			ListSupplyAll.Sort(sortSupplyListByCategoryOrderThenItemOrder);
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
			foreach(Supply supply in ListSupplyAll) {
				if(checkSort.Checked) {//all items are added in sort mode
					ListSupply.Add(supply);
					continue;
				}
				if(!checkShowHidden.Checked && supply.IsHidden) {//skip hidden supplies if show hidden is not checked
					continue;
				}
				if(supplier!=0 && supply.SupplierNum!=supplier) {//skip supplies that do not match selected supplier
					continue;
				}
				if(textFind.Text=="") {//Start filtering based on findText
					ListSupply.Add(supply);
					continue;
				}
				else if(supply.CatalogNumber.ToString().ToUpper().Contains(textFind.Text.ToUpper())) {//Check each field to see if it matches the search text field. If it does then add it and move on.
					ListSupply.Add(supply);
					continue;
				}
				else if(DefC.GetName(DefCat.SupplyCats,supply.Category).ToUpper().Contains(textFind.Text.ToUpper())) {
					ListSupply.Add(supply);
					continue;
				}
				else if(supply.Descript.ToString().ToUpper().Contains(textFind.Text.ToUpper())) {
					ListSupply.Add(supply);
					continue;
				}
				//else if(supply.ItemOrder.ToString().Contains(textFind.Text)) {
				//  listSupply.Add(supply);
				//  continue;
				//}
				else if(supply.LevelDesired.ToString().Contains(textFind.Text)) {
					ListSupply.Add(supply);
					continue;
				}
				else if(supply.Price.ToString().ToUpper().Contains(textFind.Text.ToUpper())) {
					ListSupply.Add(supply);
					continue;
				}
				else if(supply.SupplierNum.ToString().Contains(textFind.Text)) {
					ListSupply.Add(supply);
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

		private void checkSort_CheckedChanged(object sender,EventArgs e) {
			SelectedGridItems.Clear();
			foreach(int index in gridMain.SelectedIndices) {
				SelectedGridItems.Add(ListSupply[index].SupplyNum);
			}
			if(checkSort.Checked) {//Sort Mode
				butAdd.Enabled=false;
				butAdd.Visible=false;
				butUp.Enabled=true;
				butUp.Visible=true;
				butDown.Enabled=true;
				butDown.Visible=true;
				checkShowHidden.Enabled=false;
				textFind.Enabled=false;
				comboSupplier.Enabled=false;
				butOK.Enabled=false;
				butPrint.Enabled=false;
			}
			else {//Normal Mode
				butAdd.Enabled=true;
				butAdd.Visible=true;
				butUp.Enabled=false;
				butUp.Visible=false;
				butDown.Enabled=false;
				butDown.Visible=false;
				checkShowHidden.Enabled=true;
				textFind.Enabled=true;
				comboSupplier.Enabled=true;
				butOK.Enabled=true;
				butPrint.Enabled=true;
			}
			FillGrid();
		}

		///<summary>Used to sort supply list. Returns -1 if sup1 should come before sup2, 0 is they are the same, and 1 is sup2 should come before sup1.</summary>
		private int sortSupplyListByCategoryOrderThenItemOrder(Supply sup1,Supply sup2) {
			int sup1Cat=DefC.GetOrder(DefCat.SupplyCats,sup1.Category);
			int sup2Cat=DefC.GetOrder(DefCat.SupplyCats,sup2.Category);
			if(sup1Cat==sup2Cat) {//Items in same category
				if(sup1.ItemOrder==sup2.ItemOrder) {//same item
					return sup1.Descript.CompareTo(sup2.Descript);//return alphabetical order of items (0 if same item)
				}
				else if(sup1.ItemOrder<sup2.ItemOrder) {
					return -1;
				}
				else {
					return 1;
				}
			}
			else if(sup1Cat<sup2Cat) {
				return -1;
			}
			else {
				return 1;
			}
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
			}
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		
	

		

		

		

		

		

		
	}
}