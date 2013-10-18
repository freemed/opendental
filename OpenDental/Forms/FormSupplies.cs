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
using System.Drawing.Printing;

namespace OpenDental {
	public partial class FormSupplies:Form {
		/////<summary>Used to cache all supply items from DB, this list is compaired to ListSupplyAll to determine which elements have changed and need to be updated.</summary>
		//private List<Supply> ListSupplyAllCache = new List<Supply>();
		///<summary>Used to cache all supply items from DB and then altered.</summary>
		private List<Supply> ListSupplyAll = new List<Supply>();
		///<summary>Used to populate the grid. Filtered version of ListSupplyAll.</summary>
		private List<Supply> ListSupply = new List<Supply>();
		///<summary>Used to cach list of all suppliers.</summary>
		public List<Supplier> ListSupplier;
		///<Summary>Sets the supplier that will first show when opening this window.</Summary>
		public long SelectedSupplierNum;
		///<summary>Used for selecting supply items to add to orders.</summary>
		public bool IsSelectMode;
		///<summary>Possible deprecated with the addition of ListSelectedSupplies.  Selected supply item. Intended to be used to add to an order.</summary>
		public Supply SelectedSupply;
		///<summary>Selected supply items. Intended to be used to add to an order.</summary>
		public List<Supply> ListSelectedSupplies = new List<Supply>();
		///<summary>Used to cache the selected SupplyNums of the items in the main grid, to reselect them after a refresh.</summary>
		private List<long> SelectedGridItems=new List<long>();
		//Variables used for printing are copied and pasted here
		PrintDocument pd2;
		private int pagesPrinted;
		private bool headingPrinted;
		private int headingPrintH;

		public FormSupplies() {
			InitializeComponent();
			Lan.F(this);
		}

		private void FormSupplies_Load(object sender,EventArgs e) {
			Height=SystemInformation.WorkingArea.Height;//max height
			Location=new Point(Location.X,0);//move to top of screen
			ListSupplier=Suppliers.CreateObjects();
			ListSupplyAll=Supplies.GetAll();//Seperate GetAll() function call so that we do not copy by reference.
			fillComboSupplier();
			FillGrid();
			if(IsSelectMode) {
				comboSupplier.Enabled=false;
				//gridMain.SelectionMode=GridSelectionMode.One;//we now support multi select to add
			}
		}

		private void fillComboSupplier() {
			comboSupplier.Items.Clear();
			comboSupplier.Items.Add(Lan.g(this,"All"));
			comboSupplier.SelectedIndex=0;//default to "All" otherwise selected index will be selected below.
			for(int i=0;i<ListSupplier.Count;i++) {
				comboSupplier.Items.Add(ListSupplier[i].Name);
				if(ListSupplier[i].SupplierNum==SelectedSupplierNum) {
					comboSupplier.SelectedIndex=i+1;//+1 to account for the ALL item.
				}
			}
		}

		private void FillGrid(){
			//We don't refresh ListSupplyAll here because we are frequently using FillGrid with a search filter.
			filterListSupply();
			ListSupply.Sort(sortSupplyListByCategoryOrderThenItemOrder);
			gridMain.BeginUpdate();
			gridMain.Columns.Clear();
			ODGridColumn col=new ODGridColumn(Lan.g(this,"Category"),130);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g(this,"Catalog #"),80);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g(this,"Supplier"),100);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g(this,"Description"),240);
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
				row.Cells.Add(Suppliers.GetName(ListSupplier,ListSupply[i].SupplierNum));
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
			if(DefC.Short[(int)DefCat.SupplyCats].Length==0) {//No supply categories have been entered, not allowed to enter supply
				MsgBox.Show(this,"No supply categories have been created.  Go to the supply inventory window, select categories, and enter at least one supply category first.");
				return;
			}
			if(comboSupplier.SelectedIndex < 1) {//Includes no items or the ALL item being selected
				MsgBox.Show(this,"Please select a supplier first.");
				return;
			}
			Supply supp=new Supply();
			supp.IsNew=true;
			supp.SupplierNum=ListSupplier[comboSupplier.SelectedIndex-1].SupplierNum;//Selected index -1 to account for ALL being at the top of the list.
			if(gridMain.GetSelectedIndex()>-1){
				supp.Category=ListSupply[gridMain.GetSelectedIndex()].Category;
				supp.ItemOrder=ListSupply[gridMain.GetSelectedIndex()].ItemOrder;
			}
			FormSupplyEdit FormS=new FormSupplyEdit();
			FormS.Supp=supp;
			FormS.ListSupplier=ListSupplier;
			FormS.ShowDialog();//inserts supply in DB if needed.  Item order will be at selected index or end of category.
			if(FormS.DialogResult!=DialogResult.OK) {
				return;
			}
			//update listSupplyAll to reflect changes made to DB.
			ListSupplyAll=Supplies.GetAll();
			//int scroll=gridMain.ScrollValue;
			SelectedGridItems.Clear();
			SelectedGridItems.Add(FormS.Supp.SupplyNum);
			FillGrid();
			//gridMain.ScrollValue=scroll;
		}

		private void gridMain_CellDoubleClick(object sender,ODGridClickEventArgs e) {
			SelectedGridItems.Clear();
			foreach(int index in gridMain.SelectedIndices) {
				SelectedGridItems.Add(ListSupply[index].SupplyNum);
			}
			if(IsSelectMode) {
				SelectedSupply = Supplies.GetSupply((long)gridMain.Rows[e.Row].Tag);
				DialogResult = DialogResult.OK;
				return;
			}
			//Supply supplyCached=Supplies.GetSupply((long)gridMain.Rows[e.Row].Tag);
			FormSupplyEdit FormS=new FormSupplyEdit();
			FormS.Supp=Supplies.GetSupply((long)gridMain.Rows[e.Row].Tag);//works with sorting
			FormS.ListSupplier=ListSupplier;
			FormS.ShowDialog();
			if(FormS.DialogResult!=DialogResult.OK) {
				return;	
			}
			ListSupplyAll=Supplies.GetAll();
			int scroll=gridMain.ScrollValue;
			FillGrid();
			gridMain.ScrollValue=scroll;
		}

		private void comboSupplier_SelectionChangeCommitted(object sender,EventArgs e) {
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
			//Nothing Selected
			if(gridMain.SelectedIndices.Length==0) {
				return;
			}
			//remember selected SupplyNums for later
			SelectedGridItems.Clear();
			foreach(int index in gridMain.SelectedIndices) {
				SelectedGridItems.Add(ListSupply[index].SupplyNum);
			}
			if(Supplies.CleanupItemOrders(ListSupplyAll)) {//should only have to run once more... after code updates to supply window.
				MsgBox.Show(this,"There was a problem with sorting, but it has been fixed.  You may now try again.");
				FillGrid();
				return;
			}
			//loop through selected indicies, moving each one as needed, no saves to the database until after all items are moved.
			for(int i=0;i<gridMain.SelectedIndices.Length;i++) {
				int index=gridMain.SelectedIndices[i];//to reduce confusion
				int itemOrder=ListSupply[index].ItemOrder;//to reduce confusion
				//Top of category---------------------------------------------------------------------------------------
				if(itemOrder==0) {
					continue;//already top of category
				}
				//Top of visible category but not actually top of category----------------------------------------------
				if(index==0 //topmost item in visible list
					|| ListSupply[index].Category!=ListSupply[index-1].Category) //topmost item in category in visible list
				{
					moveItemOrderHelper(ListSupply[index],0);//move item to top of respective category.
					continue;
				}
				//Item is directly below a selected item in same category-----------------------------------------------
				if(indexIsSelectedHelper(index-1)) {//check for same category is performed above
					moveItemOrderHelper(ListSupply[index],ListSupply[index-1].ItemOrder+1);//move this item after the item above it, because both are selected.
					continue;
				}
				//Item is directly below a non-selected item in same category-------------------------------------------
				//check for same category is performed above.
				moveItemOrderHelper(ListSupply[index],ListSupply[index-1].ItemOrder);
			}
			saveChangesToDBHelper();
			ListSupplyAll.Sort(sortSupplyListByCategoryOrderThenItemOrder);
			int scrollVal=gridMain.ScrollValue;
			FillGrid();
			gridMain.ScrollValue=scrollVal;
		}

		private void butDown_Click(object sender,EventArgs e) {
			//Nothing Selected
			if(gridMain.SelectedIndices.Length==0) {
				return;
			}
			//remember selected SupplyNums for later
			SelectedGridItems.Clear();
			foreach(int index in gridMain.SelectedIndices) {
				SelectedGridItems.Add(ListSupply[index].SupplyNum);
			}
			if(Supplies.CleanupItemOrders(ListSupplyAll)) {//should only have to run once more... after code updates to supply window.
				MsgBox.Show(this,"There was a problem with sorting, but it has been fixed.  You may now try again.");
				FillGrid();
				return;
			}
			for(int i=gridMain.SelectedIndices.Length-1;i>=0;i--) {
				int index=gridMain.SelectedIndices[i];//to reduce confusion
				int itemOrder=ListSupply[index].ItemOrder;//to reduce confusion
				//Bottom----------------------------------------------
				if(index==ListSupply.Count-1 //bottommost item in visible list
					|| ListSupply[index].Category!=ListSupply[index+1].Category) //bottommost item in category in visible list
				{
					//end of list or category already.
					continue;
				}
				//Item is directly above a selected item in same category-----------------------------------------------
				if(indexIsSelectedHelper(index+1)) {//check for same category is performed above
					moveItemOrderHelper(ListSupply[index],ListSupply[index+1].ItemOrder-1);//move this item after the item above it, because both are selected.
					continue;
				}
				//Item is directly below a non-selected item in same category-------------------------------------------
				//check for same category is performed above.
				moveItemOrderHelper(ListSupply[index],ListSupply[index+1].ItemOrder);
			}
			saveChangesToDBHelper();
			ListSupplyAll.Sort(sortSupplyListByCategoryOrderThenItemOrder);
			int scrollVal=gridMain.ScrollValue;
			FillGrid();
			gridMain.ScrollValue=scrollVal;
		}

		///<summary>Updates database based on the values in ListSupplyAll.</summary>
		private void saveChangesToDBHelper() {
			//Get all supplies from DB to check for changes.-----------------------------------------------------------------
			List<Supply> ListSupplyAllDB=Supplies.GetAll();
			//Itterate through current supply list in memory-----------------------------------------------------------------
			for(int i=0;i<ListSupplyAll.Count;i++) {
				//find DB version of supply to pass to UpdateOrInsertIfNeeded
				Supply supplyOriginal=null;
				for(int j=0;j<ListSupplyAllDB.Count;j++) {
					if(ListSupplyAll[i].SupplyNum!=ListSupplyAllDB[j].SupplyNum) {
						continue;//not the correct supply
					}
					supplyOriginal=ListSupplyAllDB[j];
					break;//found match
				}
				Supplies.UpdateOrInsertIfNeeded(supplyOriginal,ListSupplyAll[i]);//accepts null for original.
			}
			//Update inmemory list to reflect changes.
			ListSupplyAll=Supplies.GetAll();
		}

		private bool indexIsSelectedHelper(int index) {
			for(int i=0;i<gridMain.SelectedIndices.Length;i++) {
				if(gridMain.SelectedIndices[i]==index) {
					return true;
				}
			}
			return false;
		}

		///<summary>Sets item orders appropriately. Does not reorder list, and does not repaint/refill grid.</summary>
		private void moveItemOrderHelper(Supply supply,int newItemOrder) {
			if(supply.ItemOrder>newItemOrder) {//moving item up, itterate down through list
				for(int i=0;i<ListSupplyAll.Count;i++) {
					if(ListSupplyAll[i].Category!=supply.Category) {
						continue;//wrong category
					}
					if(ListSupplyAll[i].SupplyNum==supply.SupplyNum) {
						ListSupplyAll[i].ItemOrder=newItemOrder;//set item order of this supply.
						continue;
					}
					if(ListSupplyAll[i].ItemOrder>=newItemOrder && ListSupplyAll[i].ItemOrder<supply.ItemOrder) {//all items between newItemOrder and oldItemOrder
						ListSupplyAll[i].ItemOrder++;
					}
				}
			}
			else {//moving item down, itterate up through list
				for(int i=ListSupplyAll.Count-1;i>=0;i--) {
					if(ListSupplyAll[i].Category!=supply.Category) {
						continue;//wrong category
					}
					if(ListSupplyAll[i].SupplyNum==supply.SupplyNum) {
						ListSupplyAll[i].ItemOrder=newItemOrder;//set item order of this supply.
						continue;
					}
					if(ListSupplyAll[i].ItemOrder<=newItemOrder && ListSupplyAll[i].ItemOrder>supply.ItemOrder) {//all items between newItemOrder and oldItemOrder
						ListSupplyAll[i].ItemOrder--;
					}
				}
			}
			//ListSupplyAll has correct itemOrder values, which we need to copy to listSupply without changing the actual order of ListSupply.
			for(int i=0;i<ListSupply.Count;i++){
				for(int j=0;j<ListSupplyAll.Count;j++) {
					if(ListSupply[i].SupplyNum!=ListSupplyAll[j].SupplyNum) {
						continue;
					}
					ListSupply[i]=ListSupplyAll[j];//update order and category.
				}
			}
		}
		
		private void textFind_TextChanged(object sender,EventArgs e) {
			//TODO: standardize this validation. i.e. use validation provider?
			if(!Regex.IsMatch(textFind.Text,@"^[0-9a-zA-Z]*$")) {
				textFind.BackColor=Color.LightPink;
				return;
			}
			textFind.BackColor=SystemColors.Window;
			//if(textFind.Text!="" || IsSelectMode) {
			//	butUp.Enabled=false;
			//	butDown.Enabled=false;
			//}
			//else {
			//	butUp.Enabled=true;
			//	butDown.Enabled=true;
			//}
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
			if(SelectedSupplierNum!=0) {//Use supplier num if it is provided, usually when IsSelectMode is also true
				supplier = SelectedSupplierNum;
			}
			else if(comboSupplier.SelectedIndex < 1) {//this includes selecting All or not having anything selected.
				supplier = 0;
			}
			else {
				supplier=ListSupplier[comboSupplier.SelectedIndex-1].SupplierNum;//SelectedIndex-1 because All is added before all the other items in the list.
			}
			foreach(Supply supply in ListSupplyAll) {
				if(!checkShowHidden.Checked && supply.IsHidden) {
					continue;//skip hidden supplies if show hidden is not checked
				}
				if(supplier!=0 && supply.SupplierNum!=supplier) {
					continue;//skip supplies that do not match selected supplier
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

		///<summary>Used to sort supply list. Returns -1 if sup1 should come before sup2, 0 is they are the same, and 1 is sup2 should come before sup1.</summary>
		private int sortSupplyListByCategoryOrderThenItemOrder(Supply sup1,Supply sup2) {
			int sup1Cat=DefC.GetOrder(DefCat.SupplyCats,sup1.Category);
			int sup2Cat=DefC.GetOrder(DefCat.SupplyCats,sup2.Category);
			if(sup1Cat==sup2Cat) {//Items in same category
				if(sup1.ItemOrder==sup2.ItemOrder) {//same item
					return 0;
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
			if(ListSupply.Count<1) {
				MsgBox.Show(this,"Supply list is Empty.");
				return;
			}
			pagesPrinted=0;
			headingPrinted=false;
			pd2=new PrintDocument();
			pd2.DefaultPageSettings.Margins=new Margins(50,50,40,30);
			pd2.PrintPage+=new PrintPageEventHandler(pd2_PrintPage);
			if(pd2.DefaultPageSettings.PrintableArea.Height==0) {
				pd2.DefaultPageSettings.PaperSize=new PaperSize("default",850,1100);
			}
#if DEBUG
			FormRpPrintPreview pView=new FormRpPrintPreview();
			pView.printPreviewControl2.Document=pd2;
			pView.ShowDialog();
#else
				if(PrinterL.SetPrinter(pd2,PrintSituation.Default,0,"Supplies list printed")) {
					try{
						pd2.Print();
					}
					catch{
						MsgBox.Show(this,"Printer not available");
					}
				}
#endif
		}

		private void pd2_PrintPage(object sender,System.Drawing.Printing.PrintPageEventArgs e) {
			Rectangle bounds=e.MarginBounds;
			Graphics g=e.Graphics;
			string text;
			Font headingFont=new Font("Arial",13,FontStyle.Bold);
			Font subHeadingFont=new Font("Arial",10,FontStyle.Bold);
			Font mainFont=new Font("Arial",9);
			int yPos=bounds.Top;
			#region printHeading
			//TODO: Decide what information goes in the heading.
			if(!headingPrinted) {
				text=Lan.g(this,"Supply List");
				g.DrawString(text,headingFont,Brushes.Black,425-g.MeasureString(text,headingFont).Width/2,yPos);
				yPos+=(int)g.MeasureString(text,headingFont).Height;
				if(checkShowHidden.Checked) {
					text=Lan.g(this,"Showing Hidden Items");
					g.DrawString(text,subHeadingFont,Brushes.Black,425-g.MeasureString(text,subHeadingFont).Width/2,yPos);
					yPos+=(int)g.MeasureString(text,subHeadingFont).Height;
				}
				else{
					text=Lan.g(this,"Not Showing Hidden Items");
					g.DrawString(text,subHeadingFont,Brushes.Black,425-g.MeasureString(text,subHeadingFont).Width/2,yPos);
					yPos+=(int)g.MeasureString(text,subHeadingFont).Height;
				}
				if(textFind.Text!="") {
					text=Lan.g(this,"Search Filter")+": "+textFind.Text;
					g.DrawString(text,subHeadingFont,Brushes.Black,425-g.MeasureString(text,subHeadingFont).Width/2,yPos);
					yPos+=(int)g.MeasureString(text,subHeadingFont).Height;
				}
				else {
					text=Lan.g(this,"No Search Filter");
					g.DrawString(text,subHeadingFont,Brushes.Black,425-g.MeasureString(text,subHeadingFont).Width/2,yPos);
					yPos+=(int)g.MeasureString(text,subHeadingFont).Height;
				}
				if(comboSupplier.SelectedIndex<1) {
					text=Lan.g(this,"All Suppliers");
					g.DrawString(text,subHeadingFont,Brushes.Black,425-g.MeasureString(text,subHeadingFont).Width/2,yPos);
					yPos+=(int)g.MeasureString(text,subHeadingFont).Height;
				}
				else {
					text=Lan.g(this,"Supplier")+": "+ListSupplier[comboSupplier.SelectedIndex-1].Name;
					g.DrawString(text,subHeadingFont,Brushes.Black,425-g.MeasureString(text,subHeadingFont).Width/2,yPos);
					yPos+=(int)g.MeasureString(text,subHeadingFont).Height;
					if(ListSupplier[comboSupplier.SelectedIndex-1].Phone!="") {
						text=Lan.g(this,"Phone")+": "+ListSupplier[comboSupplier.SelectedIndex-1].Phone;
						g.DrawString(text,subHeadingFont,Brushes.Black,425-g.MeasureString(text,subHeadingFont).Width/2,yPos);
						yPos+=(int)g.MeasureString(text,subHeadingFont).Height;
					}
					if(ListSupplier[comboSupplier.SelectedIndex-1].Name!="") {
						text=Lan.g(this,"Note")+": "+ListSupplier[comboSupplier.SelectedIndex-1].Name;
						g.DrawString(text,subHeadingFont,Brushes.Black,425-g.MeasureString(text,subHeadingFont).Width/2,yPos);
						yPos+=(int)g.MeasureString(text,subHeadingFont).Height;
					}
				}
				yPos+=15;
				headingPrinted=true;
				headingPrintH=yPos;
			}
			#endregion
			yPos=gridMain.PrintPage(g,pagesPrinted,bounds,headingPrintH);
			pagesPrinted++;
			if(yPos==-1) {
				e.HasMorePages=true;
			}
			else {
				e.HasMorePages=false;
			}
			g.Dispose();
		}

		private void butOK_Click(object sender,EventArgs e) {
			if(IsSelectMode) {
				if(gridMain.SelectedIndices.Length<1) {
					MsgBox.Show(this,"Please select a supply from the list first.");
					return;
				}
				ListSelectedSupplies.Clear();//just in case
				for(int i=0;i<gridMain.SelectedIndices.Length;i++) {
					ListSelectedSupplies.Add(ListSupply[gridMain.SelectedIndices[i]]);
				}
				SelectedSupply = Supplies.GetSupply((long)gridMain.Rows[gridMain.SelectedIndices[0]].Tag);
				DialogResult=DialogResult.OK;
			}
			//saveChangesToDBHelper();//All changes should already be saved to the database
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		
	

		

		

		

		

		

		
	}
}