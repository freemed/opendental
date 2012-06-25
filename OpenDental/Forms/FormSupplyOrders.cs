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
		private List<Supplier> listSupplier = Suppliers.CreateObjects();
		private List<SupplyOrder> listOrder = new List<SupplyOrder>();
		private List<SupplyOrder> listOrderAll = SupplyOrders.GetAll();
		private DataTable tableOrderItem;
		public FormSupplyOrders() {
			InitializeComponent();
			Lan.F(this);
		}

		private void FormSupplyOrders_Load(object sender,EventArgs e) {
			fillComboSupplier();
			FillGridOrder();
		}

		private void fillComboSupplier() {
			listSupplier=Suppliers.CreateObjects();
			comboSupplier.Items.Clear();
			comboSupplier.Items.Add("All");//add all to begining of list for composite listings.
			comboSupplier.SelectedIndex=0;
			for(int i=0;i<listSupplier.Count;i++) {
				comboSupplier.Items.Add(listSupplier[i].Name);
				//if(ListSupplier[i].SupplierNum==SupplierNum) {
				//  comboSupplier.SelectedIndex=i;
				//}
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
			FormSOE.ListSupplier = listSupplier;
			FormSOE.Order = listOrder[e.Row];
			FormSOE.ShowDialog();
			if(FormSOE.DialogResult!=DialogResult.OK) {
				return;
			}
			listOrderAll = SupplyOrders.GetAll();
			FillGridOrder();
			FillGridOrderItem();
		}

		private void butAddSupply_Click(object sender,EventArgs e) {
			if(gridOrder.GetSelectedIndex() < 1) {//Nothing selected or ALL selected
				MsgBox.Show(this,"Please select a supply order to add items to first.");
				return;
			}
			FormSupplies FormSup = new FormSupplies();
			FormSup.IsSelectMode = true;
			FormSup.SupplierNum = listOrder[gridOrder.GetSelectedIndex()].SupplierNum;
			FormSup.ShowDialog();
			if(FormSup.DialogResult!=DialogResult.OK) {
				return;
			}
			for(int i=0;i<tableOrderItem.Rows.Count;i++) {
				if((long)tableOrderItem.Rows[i]["SupplyNum"]==FormSup.SelectedSupply.SupplyNum) {
					MsgBox.Show(this,"Selected item already exists in currently selected order. Please edit quantity instead.");
					return;
				}
			}
			SupplyOrderItem soi = new SupplyOrderItem();
			soi.SupplyNum = FormSup.SelectedSupply.SupplyNum;
			soi.Qty = 0;
			soi.Price = FormSup.SelectedSupply.Price;
			soi.SupplyOrderNum = listOrder[gridOrder.GetSelectedIndex()].SupplyOrderNum;
			//soi.SupplyOrderItemNum
			SupplyOrderItems.Insert(soi);
			FillGridOrderItem();
		}

		private void FillGridOrder() {
			filterListOrder();
			gridOrder.BeginUpdate();
			gridOrder.Columns.Clear();
			ODGridColumn col=new ODGridColumn(Lan.g(this,"Date Placed"),80);
			gridOrder.Columns.Add(col);
			col=new ODGridColumn(Lan.g(this,"Amount"),70,HorizontalAlignment.Right);
			gridOrder.Columns.Add(col);
			col=new ODGridColumn(Lan.g(this,"Note"),200);
			gridOrder.Columns.Add(col);
			gridOrder.Rows.Clear();
			ODGridRow row;
			for(int i=0;i<listOrder.Count;i++) {
				row=new ODGridRow();
				if(listOrder[i].DatePlaced.Year>2200) {
					row.Cells.Add(Lan.g(this,"pending"));
				}
				else {
					row.Cells.Add(listOrder[i].DatePlaced.ToShortDateString());
				}
				row.Cells.Add(listOrder[i].AmountTotal.ToString("c"));
				row.Cells.Add(listOrder[i].Note);
				gridOrder.Rows.Add(row);
			}
			gridOrder.EndUpdate();
		}

		private void FillGridOrderItem() {
			long orderNum=0;
			if(gridOrder.GetSelectedIndex()!=-1) {//an order is selected
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
			double total=0;
			bool autocalcTotal=true;
			for(int i=0;i<tableOrderItem.Rows.Count;i++) {
				row=new ODGridRow();
				row.Cells.Add(tableOrderItem.Rows[i]["CatalogNumber"].ToString());
				row.Cells.Add(tableOrderItem.Rows[i]["Descript"].ToString());
				qty=PIn.Int(tableOrderItem.Rows[i]["Qty"].ToString());
				row.Cells.Add(qty.ToString());
				price=PIn.Double(tableOrderItem.Rows[i]["Price"].ToString());
				row.Cells.Add(price.ToString("n"));
				subtotal=((double)qty)*price;
				row.Cells.Add(subtotal.ToString("n"));
				gridOrderItem.Rows.Add(row);
				if(subtotal==0) {
					autocalcTotal=false;
				}
				total+=subtotal;
			}
			gridOrderItem.EndUpdate();
			if(gridOrder.GetSelectedIndex()!=-1 
				&& autocalcTotal
				&& total!=listOrder[gridOrder.GetSelectedIndex()].AmountTotal) {
				SupplyOrder order=listOrder[gridOrder.GetSelectedIndex()].Copy();
				order.AmountTotal=total;
				SupplyOrders.Update(order);
				FillGridOrder();
				for(int i=0;i<listOrder.Count;i++) {
					if(listOrder[i].SupplyOrderNum==order.SupplyOrderNum) {
						gridOrder.SetSelected(i,true);
					}
				}
			}
		}

		private void filterListOrder() {
			listOrder.Clear();
			long supplier=0;
			if(comboSupplier.SelectedIndex < 1) {//this includes selecting All or not having anything selected.
				supplier = 0;
			}
			else {
				supplier=listSupplier[comboSupplier.SelectedIndex-1].SupplierNum;//SelectedIndex-1 because All is added before all the other items in the list.
			}
			foreach(SupplyOrder order in listOrderAll) {
				if(supplier==0) {//Either the ALL supplier is selected or no supplier is selected.
					listOrder.Add(order);
				}
				else if(order.SupplierNum == supplier) {
					listOrder.Add(order);
					continue;
				}
			}
		}

		private void gridOrderItem_CellDoubleClick(object sender,ODGridClickEventArgs e) {
			FormSupplyOrderItemEdit FormSOIE = new FormSupplyOrderItemEdit();
			FormSOIE.ItemCur = SupplyOrderItems.CreateObject((long)tableOrderItem.Rows[e.Row]["SupplyOrderItemNum"]);
			FormSOIE.ListSupplier = Suppliers.CreateObjects();
			FormSOIE.ShowDialog();
			if(FormSOIE.DialogResult!=DialogResult.OK) {
				return;
			}
			SupplyOrderItems.Update(FormSOIE.ItemCur);
			listOrderAll = SupplyOrders.GetAll();//force refresh because total might have changed.
			int gridSelect = gridOrder.SelectedIndices[0];
			FillGridOrder();
			gridOrder.SetSelected(gridSelect,true);
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