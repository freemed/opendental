using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using OpenDentBusiness;
using OpenDental.UI;

namespace OpenDental {
	public partial class FormSupplyInventory:Form {
		private List<SupplyNeeded> listNeeded;
		private List<Supply> listSupply;
		private List<Supplier> listSupplier;
		private List<SupplyOrder> listOrder;
		private DataTable tableOrderItem;
		///<Summary>Will be true if the data displayed in gridSupplyMain was obtained with the find text empty.</Summary>
		private bool IsCleanRefresh;
		//public int displayH

		public FormSupplyInventory() {
			InitializeComponent();
			Lan.F(this);
		}

		private void FormInventory_Load(object sender,EventArgs e) {
			//Width=tabControl.Width+12;
			//Height=DesktopBounds.Height;
			//DesktopLocation=new Point(DesktopBounds.Width/2-Width/2,0);
			FillGridNeeded();
			FillSuppliers();
			if(comboSupplier.Items.Count>0){
				comboSupplier.SelectedIndex=0;
			}
			FillGridOrder();
			FillGridOrderItem();
			FillGridSupplyMain();
			IsCleanRefresh=true;
		}

		private void FillSuppliers(){
			listSupplier=Suppliers.CreateObjects();
			comboSupplier.Items.Clear();
			//comboSupplier.Items.Add(Lan.g(this,"All"));//just too complicated
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

		private void FillGridOrder() {
			int supplier=0;
			if(comboSupplier.SelectedIndex!=-1){
				supplier=listSupplier[comboSupplier.SelectedIndex].SupplierNum;
			}
			listOrder=SupplyOrders.CreateObjects(supplier);
			gridOrder.BeginUpdate();
			gridOrder.Columns.Clear();
			ODGridColumn col=new ODGridColumn(Lan.g(this,"Date Placed"),90);
			gridOrder.Columns.Add(col);
			col=new ODGridColumn(Lan.g(this,"Supplier"),120);
			gridOrder.Columns.Add(col);
			col=new ODGridColumn(Lan.g(this,"Note"),180);
			gridOrder.Columns.Add(col);
			gridOrder.Rows.Clear();
			ODGridRow row;
			for(int i=0;i<listOrder.Count;i++) {
				row=new ODGridRow();
				if(listOrder[i].DatePlaced.Year>2200){
					row.Cells.Add(Lan.g(this,"pending"));
				}
				else{
					row.Cells.Add(listOrder[i].DatePlaced.ToShortDateString());
				}
				row.Cells.Add(Suppliers.GetName(listSupplier,listOrder[i].SupplierNum));
				row.Cells.Add(listOrder[i].Note);
				gridOrder.Rows.Add(row);
			}
			gridOrder.EndUpdate();
		}

		private void gridOrder_CellClick(object sender,ODGridClickEventArgs e) {
			FillGridOrderItem();
			/*
			if(comboSupplier.SelectedIndex==0){//All orders from all suppliers are showing.
				for(int i=0;i<listSupplier.Count;i++) {
					if(listSupplier[i].SupplierNum==listOrder[gridOrder.GetSelectedIndex()].SupplierNum) {
						comboSupplier.SelectedIndex=i;
						break;
					}
				}
			}*/
			FillGridSupplyMain();
		}

		private void gridOrder_CellDoubleClick(object sender,ODGridClickEventArgs e) {
			FormSupplyOrderEdit FormS=new FormSupplyOrderEdit();
			FormS.Order=listOrder[e.Row];
			int selectedOrderNum=listOrder[e.Row].SupplyOrderNum;
			FormS.ListSupplier=listSupplier;
			FormS.ShowDialog();
			if(FormS.DialogResult==DialogResult.OK) {
				FillGridOrder();
			}
			for(int i=0;i<listOrder.Count;i++){
				if(listOrder[i].SupplyOrderNum==selectedOrderNum){
					gridOrder.SetSelected(i,true);
				}
			}
		}

		private void butNewOrder_Click(object sender,EventArgs e) {
			if(listSupplier.Count==0) {
				MsgBox.Show(this,"Please add suppliers first.  Use the menu at the top of this window.");
				return;
			}
			if(comboSupplier.SelectedIndex==-1) {
				MsgBox.Show(this,"Please select a supplier first.");
				return;
			}
			for(int i=0;i<listOrder.Count;i++){
				if(listOrder[i].DatePlaced.Year>2200){
					MsgBox.Show(this,"Not allowed to add a new order when there is already one pending.  Please finish the other order instead.");
					return;
				}
			}
			SupplyOrder order=new SupplyOrder();
			order.SupplierNum=listSupplier[comboSupplier.SelectedIndex].SupplierNum;
			order.IsNew=true;
			order.DatePlaced=new DateTime(2500,1,1);
			order.Note="";
			SupplyOrders.WriteObject(order);
			FillGridOrder();
			gridOrder.SetSelected(listOrder.Count-1,true);
		}

		private void FillGridOrderItem(){
			int orderNum=0;
			if(gridOrder.GetSelectedIndex()!=-1){//an order is selected
				orderNum=listOrder[gridOrder.GetSelectedIndex()].SupplyOrderNum;
			}
			tableOrderItem=SupplyOrderItems.GetItemsForOrder(orderNum);
			gridOrderItem.BeginUpdate();
			gridOrderItem.Columns.Clear();
			ODGridColumn col=new ODGridColumn(Lan.g(this,"Catalog #"),80);
			gridOrderItem.Columns.Add(col);
			col=new ODGridColumn(Lan.g(this,"Description"),320);
			gridOrderItem.Columns.Add(col);
			col=new ODGridColumn(Lan.g(this,"Qty"),60,HorizontalAlignment.Center);
			gridOrderItem.Columns.Add(col);
			col=new ODGridColumn(Lan.g(this,"Price/Unit"),70,HorizontalAlignment.Right);
			gridOrderItem.Columns.Add(col);
			col=new ODGridColumn(Lan.g(this,"Subtotal"),70,HorizontalAlignment.Center);
			gridOrderItem.Columns.Add(col);
			gridOrderItem.Rows.Clear();
			ODGridRow row;
			double price;
			int qty;
			double subtotal;
			for(int i=0;i<tableOrderItem.Rows.Count;i++){
				row=new ODGridRow();
				row.Cells.Add(tableOrderItem.Rows[i]["CatalogNumber"].ToString());
				row.Cells.Add(tableOrderItem.Rows[i]["Descript"].ToString());
				qty=PIn.PInt(tableOrderItem.Rows[i]["Qty"].ToString());
				row.Cells.Add(qty.ToString());
				price=PIn.PDouble(tableOrderItem.Rows[i]["Price"].ToString());
				row.Cells.Add(price.ToString("n"));
				subtotal=((double)qty)*price;
				row.Cells.Add(subtotal.ToString("n"));
				gridOrderItem.Rows.Add(row);
			}
			gridOrderItem.EndUpdate();
		}

		private void gridOrderItem_CellDoubleClick(object sender,ODGridClickEventArgs e) {
			FormSupplyOrderItemEdit FormS=new FormSupplyOrderItemEdit();
			FormS.ItemCur=SupplyOrderItems.CreateObject(PIn.PInt(tableOrderItem.Rows[e.Row]["SupplyOrderItemNum"].ToString()));
			FormS.ListSupplier=listSupplier;
			FormS.ShowDialog();
			if(FormS.DialogResult==DialogResult.OK) {
				FillGridOrderItem();
			}
			//still need to reselect item
		}

		private void FillGridSupplyMain(){
			int supplier=0;
			if(comboSupplier.SelectedIndex!=-1) {
				supplier=listSupplier[comboSupplier.SelectedIndex].SupplierNum;
			}
			listSupply=Supplies.CreateObjects(checkShowHidden.Checked,supplier,textFind.Text);
			gridSupplyMain.BeginUpdate();
			gridSupplyMain.Columns.Clear();
			ODGridColumn col=new ODGridColumn(Lan.g(this,"Category"),130);
			gridSupplyMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g(this,"Catalog #"),80);
			gridSupplyMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g(this,"Description"),320);
			gridSupplyMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g(this,"Price"),60,HorizontalAlignment.Right);
			gridSupplyMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g(this,"StockQty"),60,HorizontalAlignment.Center);
			gridSupplyMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g(this,"IsHidden"),40,HorizontalAlignment.Center);
			gridSupplyMain.Columns.Add(col);
			gridSupplyMain.Rows.Clear();
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
				//row.Cells.Add(listSupply[i].ItemOrder.ToString());
				gridSupplyMain.Rows.Add(row);
			}
			gridSupplyMain.EndUpdate();
		}

		private void gridSupplyMain_CellDoubleClick(object sender,ODGridClickEventArgs e) {
			FormSupplyEdit FormS=new FormSupplyEdit();
			FormS.Supp=listSupply[e.Row];
			FormS.ListSupplier=listSupplier;
			FormS.ShowDialog();
			if(FormS.DialogResult!=DialogResult.OK) {
				return;
			}
			int selected=listSupply[e.Row].SupplyNum;
			int scroll=gridSupplyMain.ScrollValue;
			FillGridSupplyMain();
			gridSupplyMain.ScrollValue=scroll;
			for(int i=0;i<listSupply.Count;i++) {
				if(listSupply[i].SupplyNum==selected) {
					gridSupplyMain.SetSelected(i,true);
				}
			}
		}

		private void butNewSupply_Click(object sender,EventArgs e) {
			if(listSupplier.Count==0) {
				MsgBox.Show(this,"Please add suppliers first.  Use the menu at the top of this window.");
				return;
			}
			if(DefB.Short[(int)DefCat.SupplyCats].Length==0) {
				MsgBox.Show(this,"Please add supply categories first.  Use the menu at the top of this window.");
				return;
			}
			if(comboSupplier.SelectedIndex==-1) {
				MsgBox.Show(this,"Please select a supplier first.");
				return;
			}
			Supply supp=new Supply();
			supp.IsNew=true;
			supp.SupplierNum=listSupplier[comboSupplier.SelectedIndex].SupplierNum;
			FormSupplyEdit FormS=new FormSupplyEdit();
			FormS.Supp=supp;
			FormS.ListSupplier=listSupplier;
			FormS.ShowDialog();
			if(FormS.DialogResult!=DialogResult.OK) {
				return;
			}
			int selected=FormS.Supp.SupplyNum;
			int scroll=gridSupplyMain.ScrollValue;
			FillGridSupplyMain();
			gridSupplyMain.ScrollValue=scroll;
			for(int i=0;i<listSupply.Count;i++) {
				if(listSupply[i].SupplyNum==selected) {
					gridSupplyMain.SetSelected(i,true);
				}
			}
		}

		private void butAddToOrder_Click(object sender,EventArgs e) {
			if(gridOrder.GetSelectedIndex()==-1){
				MsgBox.Show(this,"Please select an order first.");
				return;
			}
			if(gridSupplyMain.SelectedIndices.Length==0){
				MsgBox.Show(this,"Please select one or more supplies first.");
				return;
			}
			SupplyOrderItem item;
			List<int> itemNums=new List<int>();
			List<Supply> skippedSupplies=new List<Supply>();
			bool isSkipped;
			for(int i=0;i<gridSupplyMain.SelectedIndices.Length;i++){
				isSkipped=false;
				for(int t=0;t<tableOrderItem.Rows.Count;t++){
					if(listSupply[gridSupplyMain.SelectedIndices[i]].SupplyNum.ToString()==tableOrderItem.Rows[t]["SupplyNum"].ToString()) {
						isSkipped=true;
						break;;
					}
				}
				if(isSkipped){
					skippedSupplies.Add(listSupply[gridSupplyMain.SelectedIndices[i]]);
					continue;
				}
				item=new SupplyOrderItem();
				item.SupplyOrderNum=listOrder[gridOrder.GetSelectedIndex()].SupplyOrderNum;
				item.SupplyNum=listSupply[gridSupplyMain.SelectedIndices[i]].SupplyNum;
				item.Qty=1;
				item.Price=listSupply[gridSupplyMain.SelectedIndices[i]].Price;
				SupplyOrderItems.WriteObject(item);
				itemNums.Add(item.SupplyOrderItemNum);
			}
			if(gridSupplyMain.SelectedIndices.Length==1 && skippedSupplies.Count==1){
				MsgBox.Show(this,"Selected supply is already on the order.");
				return;
			}
			else if(skippedSupplies.Count==gridSupplyMain.SelectedIndices.Length){
				MsgBox.Show(this,"Selected supplies are already on the order.");
				return;
			}
			else if(skippedSupplies.Count>0){
				MessageBox.Show(skippedSupplies.Count.ToString()+" "+Lan.g(this,"supplies were skipped because they are already on the order."));
			}
			FillGridOrderItem();
			tabControl.SelectedIndex=1;
			for(int i=0;i<tableOrderItem.Rows.Count;i++){
				if(itemNums.Contains(PIn.PInt(tableOrderItem.Rows[i]["SupplyOrderItemNum"].ToString()))){
					gridOrderItem.SetSelected(i,true);
				}
			}
		}

		private void butUp_Click(object sender,EventArgs e) {
			if(!IsCleanRefresh) {
				MsgBox.Show(this,"Please perform a clean refresh first without any find text.");
				return;
			}
			if(gridSupplyMain.SelectedIndices.Length==0) {
				MsgBox.Show(this,"You must first select at least one row.");
				return;
			}
			textFind.Text="";
			if(Supplies.CleanupItemOrders(listSupply)) {
				MsgBox.Show(this,"There was a problem with sorting, but it has been fixed.  You may now try again.");
				FillGridSupplyMain();
				return;
			}
			for(int i=0;i<gridSupplyMain.SelectedIndices.Length;i++) {
				if(listSupply[gridSupplyMain.SelectedIndices[0]].Category!=listSupply[gridSupplyMain.SelectedIndices[i]].Category) {
					MsgBox.Show(this,"You may only move items that are in the same category.");
					return;
				}
			}
			if(gridSupplyMain.SelectedIndices[0]==0
				|| listSupply[gridSupplyMain.SelectedIndices[0]].Category!=listSupply[gridSupplyMain.SelectedIndices[0]-1].Category) {
				return;//already at the top
			}
			//remember the selected SupplyNums for rehighlighting later.
			List<int> selectedSupplyNums=new List<int>();
			for(int i=0;i<gridSupplyMain.SelectedIndices.Length;i++) {
				selectedSupplyNums.Add(listSupply[gridSupplyMain.SelectedIndices[i]].SupplyNum);
			}
			int scrollVal=gridSupplyMain.ScrollValue;
			//change all the appropriate itemorders
			for(int i=0;i<gridSupplyMain.SelectedIndices.Length;i++) {//loop from the top down
				listSupply[gridSupplyMain.SelectedIndices[i]-1].ItemOrder++;//move the one above it down
				listSupply[gridSupplyMain.SelectedIndices[i]].ItemOrder--;//move this one up
				listSupply.Reverse(gridSupplyMain.SelectedIndices[i]-1,2);
			}
			//persist changes
			for(int i=0;i<listSupply.Count;i++) {
				if(listSupply[i].ItemOrderChanged) {
					Supplies.WriteObject(listSupply[i]);
				}
			}
			FillGridSupplyMain();
			//reselect the original supplyNums
			for(int i=0;i<listSupply.Count;i++) {
				if(selectedSupplyNums.Contains(listSupply[i].SupplyNum)) {
					gridSupplyMain.SetSelected(i,true);
				}
			}
			gridSupplyMain.ScrollValue=scrollVal;
		}

		private void butDown_Click(object sender,EventArgs e) {
			if(!IsCleanRefresh) {
				MsgBox.Show(this,"Please perform a clean refresh first without any find text.");
				return;
			}
			if(gridSupplyMain.SelectedIndices.Length==0) {
				MsgBox.Show(this,"You must first select at least one row.");
				return;
			}
			textFind.Text="";
			if(Supplies.CleanupItemOrders(listSupply)) {
				MsgBox.Show(this,"There was a problem with sorting, but it has been fixed.  You may now try again.");
				FillGridSupplyMain();
				return;
			}
			for(int i=0;i<gridSupplyMain.SelectedIndices.Length;i++) {
				if(listSupply[gridSupplyMain.SelectedIndices[0]].Category!=listSupply[gridSupplyMain.SelectedIndices[i]].Category) {
					MsgBox.Show(this,"You may only move items that are in the same category.");
					return;
				}
			}
			if(gridSupplyMain.SelectedIndices[gridSupplyMain.SelectedIndices.Length-1]==listSupply.Count-1
				|| listSupply[gridSupplyMain.SelectedIndices[gridSupplyMain.SelectedIndices.Length-1]].Category
				!=listSupply[gridSupplyMain.SelectedIndices[gridSupplyMain.SelectedIndices.Length-1]+1].Category) {
				return;//already at the bottom
			}
			//remember the selected SupplyNums for rehighlighting later.
			List<int> selectedSupplyNums=new List<int>();
			for(int i=0;i<gridSupplyMain.SelectedIndices.Length;i++) {
				selectedSupplyNums.Add(listSupply[gridSupplyMain.SelectedIndices[i]].SupplyNum);
			}
			int scrollVal=gridSupplyMain.ScrollValue;
			//change all the appropriate itemorders in the main list
			for(int i=gridSupplyMain.SelectedIndices.Length-1;i>=0;i--) {//loop from the bottom up
				listSupply[gridSupplyMain.SelectedIndices[i]+1].ItemOrder--;//move the one below it up
				listSupply[gridSupplyMain.SelectedIndices[i]].ItemOrder++;//move this one down
				listSupply.Reverse(gridSupplyMain.SelectedIndices[i],2);
			}
			//Persist changes
			for(int i=0;i<listSupply.Count;i++) {
				if(listSupply[i].ItemOrderChanged) {
					Supplies.WriteObject(listSupply[i]);
				}
			}
			FillGridSupplyMain();
			//reselect the original supplyNums
			for(int i=0;i<listSupply.Count;i++) {
				if(selectedSupplyNums.Contains(listSupply[i].SupplyNum)) {
					gridSupplyMain.SetSelected(i,true);
				}
			}
			gridSupplyMain.ScrollValue=scrollVal;
		}

		private void textFind_KeyDown(object sender,KeyEventArgs e) {
			if(e.KeyCode==Keys.Enter) {
				butRefresh_Click(this,new EventArgs());
			}
		}

		private void butRefresh_Click(object sender,EventArgs e) {
			if(!Regex.IsMatch(textFind.Text,@"^[0-9a-zA-Z]*$")) {
				MsgBox.Show(this,"No special characters are allowed in the find text.");
				return;
			}
			IsCleanRefresh=textFind.Text=="";
			FillGridSupplyMain();
		}

		private void comboSupplier_SelectionChangeCommitted(object sender,EventArgs e) {
			//gridOrder.SetSelected(false);
			FillGridOrder();
			FillGridOrderItem();
			FillGridSupplyMain();
		}

		private void checkShowHidden_Click(object sender,EventArgs e) {
			FillGridSupplyMain();
		}

		private void menuItemSuppliers_Click(object sender,EventArgs e) {
			FormSuppliers FormS=new FormSuppliers();
			FormS.ShowDialog();
			FillSuppliers();
			FillGridOrderItem();//clears the grid.
		}

		private void menuItemCategories_Click(object sender,EventArgs e) {
			FormDefinitions FormD=new FormDefinitions(DefCat.SupplyCats);
			FormD.ShowDialog();
			FillGridOrderItem();
		}

		private void butClose_Click(object sender,EventArgs e) {
			Close();
		}

		

		

		

		

		
		

		

	

		

		

		

		

		

		

		

		
	}
}