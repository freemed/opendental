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
	public partial class FormSupplyOrders:Form {
		private List<Supplier> ListSuppliers;
		private List<SupplyOrder> ListOrdersAll;
		private List<SupplyOrder> ListOrders;
		private DataTable tableOrderItems;

		public FormSupplyOrders() {
			InitializeComponent();
			Lan.F(this);
		}

		private void FormSupplyOrders_Load(object sender,EventArgs e) {
			ListSuppliers = Suppliers.CreateObjects();
			ListOrdersAll = SupplyOrders.GetAll();
			ListOrders = new List<SupplyOrder>();
			fillComboSupplier();
			FillGridOrder();
		}

		private void fillComboSupplier() {
			ListSuppliers=Suppliers.CreateObjects();
			comboSupplier.Items.Clear();
			comboSupplier.Items.Add(Lan.g(this,"All"));//add all to begining of list for composite listings.
			comboSupplier.SelectedIndex=0;
			for(int i=0;i<ListSuppliers.Count;i++) {
				comboSupplier.Items.Add(ListSuppliers[i].Name);
			}
		}

		private void comboSupplier_SelectedIndexChanged(object sender,EventArgs e) {
			FillGridOrder();
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
			FillGridOrder();
			FillGridOrderItem();
		}

		private void butAddSupply_Click(object sender,EventArgs e) {
			if(gridOrders.GetSelectedIndex() < 1) {//Nothing selected or ALL selected
				MsgBox.Show(this,"Please select a supply order to add items to first.");
				return;
			}
			FormSupplies FormSup = new FormSupplies();
			FormSup.IsSelectMode = true;
			FormSup.SupplierNum = ListOrders[gridOrders.GetSelectedIndex()].SupplierNum;
			FormSup.ShowDialog();
			if(FormSup.DialogResult!=DialogResult.OK) {
				return;
			}
			for(int i=0;i<tableOrderItems.Rows.Count;i++) {
				if((long)tableOrderItems.Rows[i]["SupplyNum"]==FormSup.SelectedSupply.SupplyNum) {
					MsgBox.Show(this,"Selected item already exists in currently selected order. Please edit quantity instead.");
					return;
				}
			}
			SupplyOrderItem orderitem = new SupplyOrderItem();
			orderitem.SupplyNum = FormSup.SelectedSupply.SupplyNum;
			orderitem.Qty=0;
			orderitem.Price = FormSup.SelectedSupply.Price;
			orderitem.SupplyOrderNum = ListOrders[gridOrders.GetSelectedIndex()].SupplyOrderNum;
			//soi.SupplyOrderItemNum
			SupplyOrderItems.Insert(orderitem);
			FillGridOrderItem();
		}

		private void FillGridOrder() {
			filterListOrder();
			gridOrders.BeginUpdate();
			gridOrders.Columns.Clear();
			ODGridColumn col=new ODGridColumn(Lan.g(this,"Date Placed"),80);
			gridOrders.Columns.Add(col);
			col=new ODGridColumn(Lan.g(this,"Amount"),70,HorizontalAlignment.Right);
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
				row.Cells.Add(ListOrders[i].Note);
				gridOrders.Rows.Add(row);
			}
			gridOrders.EndUpdate();
		}

		private void filterListOrder() {
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
			ODGridColumn col=new ODGridColumn(Lan.g(this,"Catalog #"),80);
			gridItems.Columns.Add(col);
			col=new ODGridColumn(Lan.g(this,"Description"),320);
			gridItems.Columns.Add(col);
			col=new ODGridColumn(Lan.g(this,"Qty"),60,HorizontalAlignment.Center);
			gridItems.Columns.Add(col);
			col=new ODGridColumn(Lan.g(this,"Price/Unit"),70,HorizontalAlignment.Right);
			gridItems.Columns.Add(col);
			col=new ODGridColumn(Lan.g(this,"Subtotal"),70,HorizontalAlignment.Center);
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
				FillGridOrder();
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
			FillGridOrder();
			gridOrders.SetSelected(gridSelect,true);
			FillGridOrderItem();
		}



		private void butOK_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

	}
}