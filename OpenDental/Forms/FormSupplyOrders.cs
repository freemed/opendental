using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using OpenDentBusiness;
using OpenDental.UI;
using System.Drawing.Printing;

namespace OpenDental {
	public partial class FormSupplyOrders:Form {
		private List<Supplier> ListSuppliers;
		private List<SupplyOrder> ListOrdersAll;
		private List<SupplyOrder> ListOrders;
		private DataTable tableOrderItems;
		//Variables used for printing are copied and pasted here
		PrintDocument pd2;
		private int pagesPrinted;
		private bool headingPrinted;
		private int headingPrintH;

		public FormSupplyOrders() {
			InitializeComponent();
			Lan.F(this);
		}

		private void FormSupplyOrders_Load(object sender,EventArgs e) {
			Height=SystemInformation.WorkingArea.Height;//max height
			Location=new Point(Location.X,0);//move to top of screen
			ListSuppliers = Suppliers.CreateObjects();
			ListOrdersAll = SupplyOrders.GetAll();
			ListOrders = new List<SupplyOrder>();
			FillComboSupplier();
			FillGridOrders();
		}

		private void FillComboSupplier() {
			ListSuppliers=Suppliers.CreateObjects();
			comboSupplier.Items.Clear();
			comboSupplier.Items.Add(Lan.g(this,"All"));//add all to begining of list for composite listings.
			comboSupplier.SelectedIndex=0;
			for(int i=0;i<ListSuppliers.Count;i++) {
				comboSupplier.Items.Add(ListSuppliers[i].Name);
			}
		}

		private void comboSupplier_SelectedIndexChanged(object sender,EventArgs e) {
			FillGridOrders();
			FillGridOrderItem();
		}

		private void gridOrder_CellClick(object sender,ODGridClickEventArgs e) {
			FillGridOrderItem();
		}

		private void gridOrder_CellDoubleClick(object sender,ODGridClickEventArgs e) {
			FormSupplyOrderEdit FormSOE = new FormSupplyOrderEdit();
			FormSOE.ListSupplier = ListSuppliers;
			FormSOE.Order = ListOrders[e.Row];
			FormSOE.ShowDialog();
			if(FormSOE.DialogResult!=DialogResult.OK) {
				return;
			}
			ListOrdersAll = SupplyOrders.GetAll();
			FillGridOrders();
			FillGridOrderItem();
		}

		private void butAddSupply_Click(object sender,EventArgs e) {
			if(gridOrders.GetSelectedIndex()==-1) {
				MsgBox.Show(this,"Please select a supply order to add items to first.");
				return;
			}
			FormSupplies FormSup = new FormSupplies();
			FormSup.IsSelectMode = true;
			FormSup.SelectedSupplierNum = ListOrders[gridOrders.GetSelectedIndex()].SupplierNum;
			FormSup.ShowDialog();
			if(FormSup.DialogResult!=DialogResult.OK) {
				return;
			}
			
			for(int i=0;i<FormSup.ListSelectedSupplies.Count;i++) {
				//check for existing----			
				if(itemExistsHelper(FormSup.ListSelectedSupplies[i])) {
					//MsgBox.Show(this,"Selected item already exists in currently selected order. Please edit quantity instead.");
					continue;
				}
				SupplyOrderItem orderitem = new SupplyOrderItem();
				orderitem.SupplyNum = FormSup.ListSelectedSupplies[i].SupplyNum;
				orderitem.Qty=1;
				orderitem.Price = FormSup.ListSelectedSupplies[i].Price;
				orderitem.SupplyOrderNum = ListOrders[gridOrders.GetSelectedIndex()].SupplyOrderNum;
				//soi.SupplyOrderItemNum
				SupplyOrderItems.Insert(orderitem);
			}
			FillGridOrderItem();
		}

		///<summary>Returns true if item exists in supply order.</summary>
		private bool itemExistsHelper(Supply supply) {
			for(int i=0;i<tableOrderItems.Rows.Count;i++) {
				if((long)tableOrderItems.Rows[i]["SupplyNum"]==supply.SupplyNum) {
					return true;
				}
			}
			return false;
		}

		private void FillGridOrders() {
			FilterListOrder();
			gridOrders.BeginUpdate();
			gridOrders.Columns.Clear();
			ODGridColumn col=new ODGridColumn(Lan.g(this,"Date Placed"),80);
			gridOrders.Columns.Add(col);
			col=new ODGridColumn(Lan.g(this,"Amount"),70,HorizontalAlignment.Right);
			gridOrders.Columns.Add(col);
			col=new ODGridColumn(Lan.g(this,"Supplier"),120);
			gridOrders.Columns.Add(col);
			col=new ODGridColumn(Lan.g(this,"Note"),200);
			gridOrders.Columns.Add(col);
			gridOrders.Rows.Clear();
			ODGridRow row;
			for(int i=0;i<ListOrders.Count;i++) {
				row=new ODGridRow();
				if(ListOrders[i].DatePlaced.Year>2200) {
					row.Cells.Add(Lan.g(this,"pending"));
				}
				else {
					row.Cells.Add(ListOrders[i].DatePlaced.ToShortDateString());
				}
				row.Cells.Add(ListOrders[i].AmountTotal.ToString("c"));
				row.Cells.Add(Suppliers.GetName(ListSuppliers,ListOrders[i].SupplierNum));
				row.Cells.Add(ListOrders[i].Note);
				gridOrders.Rows.Add(row);
			}
			gridOrders.EndUpdate();
		}

		private void FilterListOrder() {
			ListOrders.Clear();
			long supplier=0;
			if(comboSupplier.SelectedIndex < 1) {//this includes selecting All or not having anything selected.
				supplier = 0;
			}
			else {
				supplier=ListSuppliers[comboSupplier.SelectedIndex-1].SupplierNum;//SelectedIndex-1 because All is added before all the other items in the list.
			}
			foreach(SupplyOrder order in ListOrdersAll) {
				if(supplier==0) {//Either the ALL supplier is selected or no supplier is selected.
					ListOrders.Add(order);
				}
				else if(order.SupplierNum == supplier) {
					ListOrders.Add(order);
					continue;
				}
			}
		}

		private void FillGridOrderItem() {
			long orderNum=0;
			if(gridOrders.GetSelectedIndex()!=-1) {//an order is selected
				orderNum=ListOrders[gridOrders.GetSelectedIndex()].SupplyOrderNum;
			}
			tableOrderItems=SupplyOrderItems.GetItemsForOrder(orderNum);
			gridItems.BeginUpdate();
			gridItems.Columns.Clear();
			//ODGridColumn col=new ODGridColumn(Lan.g(this,"Supplier"),120);
			//gridItems.Columns.Add(col);
			ODGridColumn col=new ODGridColumn(Lan.g(this,"Catalog #"),80);
			gridItems.Columns.Add(col);
			col=new ODGridColumn(Lan.g(this,"Description"),320);
			gridItems.Columns.Add(col);
			col=new ODGridColumn(Lan.g(this,"Qty"),60,HorizontalAlignment.Center);
			col.IsEditable=true;
			gridItems.Columns.Add(col);
			col=new ODGridColumn(Lan.g(this,"Price/Unit"),70,HorizontalAlignment.Right);
			col.IsEditable=true;
			gridItems.Columns.Add(col);
			col=new ODGridColumn(Lan.g(this,"Subtotal"),70,HorizontalAlignment.Right);
			gridItems.Columns.Add(col);
			gridItems.Rows.Clear();
			ODGridRow row;
			double price;
			int qty;
			double subtotal;
			double total=0;
			bool autocalcTotal=true;
			for(int i=0;i<tableOrderItems.Rows.Count;i++) {
				row=new ODGridRow();
				//if(gridOrders.GetSelectedIndex()==-1){
				//	row.Cells.Add("");
				//}
				//else{
				//	row.Cells.Add(Suppliers.GetName(ListSuppliers,ListOrders[gridOrders.GetSelectedIndex()].SupplierNum));
				//}
				row.Cells.Add(tableOrderItems.Rows[i]["CatalogNumber"].ToString());
				row.Cells.Add(tableOrderItems.Rows[i]["Descript"].ToString());
				qty=PIn.Int(tableOrderItems.Rows[i]["Qty"].ToString());
				row.Cells.Add(qty.ToString());
				price=PIn.Double(tableOrderItems.Rows[i]["Price"].ToString());
				row.Cells.Add(price.ToString("n"));
				subtotal=((double)qty)*price;
				row.Cells.Add(subtotal.ToString("n"));
				gridItems.Rows.Add(row);
				if(subtotal==0) {
					autocalcTotal=false;
				}
				total+=subtotal;
			}
			gridItems.EndUpdate();
			if(gridOrders.GetSelectedIndex()!=-1 
				&& autocalcTotal
				&& total!=ListOrders[gridOrders.GetSelectedIndex()].AmountTotal) 
			{
				SupplyOrder order=ListOrders[gridOrders.GetSelectedIndex()].Copy();
				order.AmountTotal=total;
				SupplyOrders.Update(order);
				FillGridOrders();
				for(int i=0;i<ListOrders.Count;i++) {
					if(ListOrders[i].SupplyOrderNum==order.SupplyOrderNum) {
						gridOrders.SetSelected(i,true);
					}
				}
			}
		}

		private void gridOrderItem_CellDoubleClick(object sender,ODGridClickEventArgs e) {
			FormSupplyOrderItemEdit FormSOIE = new FormSupplyOrderItemEdit();
			FormSOIE.ItemCur = SupplyOrderItems.CreateObject((long)tableOrderItems.Rows[e.Row]["SupplyOrderItemNum"]);
			FormSOIE.ListSupplier = Suppliers.CreateObjects();
			FormSOIE.ShowDialog();
			if(FormSOIE.DialogResult!=DialogResult.OK) {
				return;
			}
			SupplyOrderItems.Update(FormSOIE.ItemCur);
			ListOrdersAll = SupplyOrders.GetAll();//force refresh because total might have changed.
			int gridSelect = gridOrders.SelectedIndices[0];
			FillGridOrders();
			gridOrders.SetSelected(gridSelect,true);
			FillGridOrderItem();
		}

		///<summary>Used to update subtotal when qty or price are edited.</summary>
		private void calculateSubtotalHelper() {
			try {
				gridItems.Rows[gridItems.SelectedCell.Y].ColorBackG=Color.White;
				if(gridItems.SelectedCell.X==2) {//Qty
					int qty=Int32.Parse(gridItems.Rows[gridItems.SelectedCell.Y].Cells[gridItems.SelectedCell.X].Text);
					gridItems.Rows[gridItems.SelectedCell.Y].Cells[4].Text=(qty*PIn.Double(gridItems.Rows[gridItems.SelectedCell.Y].Cells[3].Text)).ToString("n");
				}
				if(gridItems.SelectedCell.X==3) {//Price
					double price=Double.Parse(gridItems.Rows[gridItems.SelectedCell.Y].Cells[gridItems.SelectedCell.X].Text);
					gridItems.Rows[gridItems.SelectedCell.Y].Cells[4].Text=(price*PIn.Int(gridItems.Rows[gridItems.SelectedCell.Y].Cells[2].Text)).ToString("n");
				}
				Application.DoEvents();
				//save changes to order item on cell leave
			}
			catch(Exception ex) {
				//problem calculating or parsing amount.
				gridItems.Rows[gridItems.SelectedCell.Y].ColorBackG=Color.LightPink;
				gridItems.Rows[gridItems.SelectedCell.Y].Cells[4].Text=0.ToString("n");
			}
		}

		private void butNewOrder_Click(object sender,EventArgs e) {
			if(comboSupplier.SelectedIndex < 1) {//Includes no items or the ALL supplier being selected.
				MsgBox.Show(this,"Please select a supplier first.");
				return;
			}
			for(int i=0;i<ListOrders.Count;i++) {
				if(ListOrders[i].DatePlaced.Year>2200) {
					MsgBox.Show(this,"Not allowed to add a new order when there is already one pending.  Please finish the other order instead.");
					return;
				}
			}
			SupplyOrder order=new SupplyOrder();
			if(comboSupplier.SelectedIndex==0) {//Supplier "All".
				order.SupplierNum=0;
			}
			else {//Specific supplier selected.
				order.SupplierNum=ListSuppliers[comboSupplier.SelectedIndex-1].SupplierNum;//SelectedIndex-1 because "All" is first option.
			}
			order.IsNew=true;
			order.DatePlaced=new DateTime(2500,1,1);
			order.Note="";
			SupplyOrders.Insert(order);
			ListOrdersAll=SupplyOrders.GetAll();//Refresh the list all.
			FillGridOrders();
			gridOrders.SetSelected(ListOrders.Count-1,true);
		}

		private void butPrint_Click(object sender,EventArgs e) {
			if(tableOrderItems.Rows.Count<1) {
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
				if(PrinterL.SetPrinter(pd2,PrintSituation.Default,0,"Supplies order from "+ListOrders[gridOrders.GetSelectedIndex()].DatePlaced.ToShortDateString()+" printed")) {
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
				text=Lan.g(this,"Order Number")+": "+ListOrders[gridOrders.SelectedIndices[0]].SupplyOrderNum;
				g.DrawString(text,subHeadingFont,Brushes.Black,425-g.MeasureString(text,subHeadingFont).Width/2,yPos);
				yPos+=(int)g.MeasureString(text,subHeadingFont).Height;
				text=Lan.g(this,"Date")+": "+ListOrders[gridOrders.SelectedIndices[0]].DatePlaced.ToShortDateString();
				g.DrawString(text,subHeadingFont,Brushes.Black,425-g.MeasureString(text,subHeadingFont).Width/2,yPos);
				yPos+=(int)g.MeasureString(text,subHeadingFont).Height;
				Supplier supCur=Suppliers.GetOne(ListOrders[gridOrders.SelectedIndices[0]].SupplierNum);
				text=supCur.Name;
				g.DrawString(text,subHeadingFont,Brushes.Black,425-g.MeasureString(text,subHeadingFont).Width/2,yPos);
				yPos+=(int)g.MeasureString(text,subHeadingFont).Height;
				text=supCur.Phone;
				g.DrawString(text,subHeadingFont,Brushes.Black,425-g.MeasureString(text,subHeadingFont).Width/2,yPos);
				yPos+=(int)g.MeasureString(text,subHeadingFont).Height;
				text=supCur.Note;
				g.DrawString(text,subHeadingFont,Brushes.Black,425-g.MeasureString(text,subHeadingFont).Width/2,yPos);
				yPos+=(int)g.MeasureString(text,subHeadingFont).Height;
				yPos+=15;
				headingPrinted=true;
				headingPrintH=yPos;
			}
			#endregion
			yPos=gridItems.PrintPage(g,pagesPrinted,bounds,headingPrintH);
			pagesPrinted++;
			if(yPos==-1) {
				e.HasMorePages=true;
			}
			else {
				e.HasMorePages=false;
			}
			g.Dispose();
		}

		///<summary>Save changes to orderItems based on input in grid.</summary>
		//private bool saveChangesHelper() {
		//	if(gridItems.Rows.Count==0) {
		//		return true;
		//	}
		//	//validate ------------------------------------------------------------------------
		//	for(int i=0;i<gridItems.Rows.Count;i++) {
		//		int qtyThisRow=0;
		//		double priceThisRow=0;
		//		if(gridItems.Rows[i].Cells[2].Text!=""){
		//			try{
		//					qtyThisRow=Int32.Parse(gridItems.Rows[i].Cells[2].Text);
		//			}
		//			catch{
		//				MsgBox.Show(this,"Please fix errors in Qty column first.");
		//				return false;
		//			}
		//		}
		//		if(gridItems.Rows[i].Cells[3].Text!=""){
		//			try{
		//					priceThisRow=double.Parse(gridItems.Rows[i].Cells[3].Text);
		//			}
		//			catch{
		//				MsgBox.Show(this,"Please fix errors in Price column first.");
		//				return false;
		//			}
		//		}
		//	}
		//	//Save changes---------------------------------------------------------------------------
		//	//List<SupplyOrderItem> listOrderItems=OpenDentBusiness.Crud.SupplyOrderItemCrud.TableToList(tableOrderItems);//turn table into list of supplyOrderItem objects
		//	for(int i=0;i<gridItems.Rows.Count;i++) {
		//		int qtyThisRow=PIn.Int(gridItems.Rows[i].Cells[2].Text);//already validated
		//		double priceThisRow=PIn.Double(gridItems.Rows[i].Cells[3].Text);//already validated
		//		if(qtyThisRow==PIn.Int(tableOrderItems.Rows[i]["Qty"].ToString())
		//			&& priceThisRow==PIn.Double(tableOrderItems.Rows[i]["Price"].ToString()))
		//		{
		//			continue;//no changes to order item.
		//		}
		//		SupplyOrderItem soi=new SupplyOrderItem();
		//		soi.SupplyNum=PIn.Long(tableOrderItems.Rows[i]["SupplyNum"].ToString());
		//		soi.SupplyOrderItemNum=PIn.Long(tableOrderItems.Rows[i]["SupplyOrderItemNum"].ToString());
		//		soi.SupplyOrderNum=ListOrders[gridOrders.GetSelectedIndex()].SupplyOrderNum;
		//		soi.Qty=qtyThisRow;
		//		soi.Price=priceThisRow;
		//		SupplyOrderItems.Update(soi);
		//	}//end gridItems
		//	SupplyOrders.UpdateOrderPrice(ListOrders[gridOrders.GetSelectedIndex()].SupplyOrderNum);
		//	int selectedIndex=gridOrders.GetSelectedIndex();
		//	ListOrdersAll = SupplyOrders.GetAll();//update new totals
		//	FillGridOrders();
		//	if(selectedIndex!=-1) {
		//		gridOrders.SetSelected(selectedIndex,true);
		//	}
		//	return true;
		//}

		private void gridItems_CellLeave(object sender,ODGridClickEventArgs e) {
			//no need to check which cell was edited, just reprocess both cells
			int qtyNew=PIn.Int(gridItems.Rows[e.Row].Cells[2].Text);//0 if not valid input
			double priceNew=PIn.Double(gridItems.Rows[e.Row].Cells[3].Text);//0 if not valid input
			SupplyOrderItem suppOI=SupplyOrderItems.CreateObject(PIn.Long(tableOrderItems.Rows[e.Row]["SupplyOrderItemNum"].ToString()));
			suppOI.Qty=qtyNew;
			suppOI.Price=priceNew;
			SupplyOrderItems.Update(suppOI);
			SupplyOrders.UpdateOrderPrice(suppOI.SupplyOrderNum);
			gridItems.Rows[e.Row].Cells[2].Text=qtyNew.ToString();//to standardize formatting.  They probably didn't type .00
			gridItems.Rows[e.Row].Cells[3].Text=priceNew.ToString("n");//to standardize formatting.  They probably didn't type .00
			gridItems.Rows[e.Row].Cells[4].Text=(qtyNew*priceNew).ToString("n");//to standardize formatting.  They probably didn't type .00
			gridItems.Invalidate();
			int si=gridOrders.GetSelectedIndex();
			ListOrdersAll=SupplyOrders.GetAll();
			FillGridOrders();
			gridOrders.SetSelected(si,true);
		}

		private void butOK_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender,EventArgs e) {
			//maybe rename to close, since most saving happens automatically.
			DialogResult=DialogResult.Cancel;
		}


	}
}