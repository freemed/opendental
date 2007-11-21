using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental {
	public partial class FormSupplyOrderItemEdit:Form {
		private Supply Supp;
		public SupplyOrderItem ItemCur;
		public List<Supplier> ListSupplier;

		public FormSupplyOrderItemEdit() {
			InitializeComponent();
			Lan.F(this);
		}

		private void FormSupplyOrderItemEdit_Load(object sender,EventArgs e) {
			Supp=Supplies.CreateObject(ItemCur.SupplyNum);
			textSupplier.Text=Suppliers.GetName(ListSupplier,Supp.SupplierNum);
			textCategory.Text=DefB.GetName(DefCat.SupplyCats,Supp.Category);
			textCatalogNumber.Text=Supp.CatalogNumber;
			textDescript.Text=Supp.Descript;
			textQty.Text=ItemCur.Qty.ToString();
			textPrice.Text=ItemCur.Price.ToString("n");
		}

		private void butDelete_Click(object sender,EventArgs e) {
			if(!MsgBox.Show(this,true,"Delete?")){
				return;
			}
			//try{
			SupplyOrderItems.DeleteObject(ItemCur);
			//}
			//catch(ApplicationException ex){
			//	MessageBox.Show(ex.Message);
			//	return;
			//}
			DialogResult=DialogResult.OK;
		}

		private void textPrice_TextChanged(object sender,EventArgs e) {
			FillSubtotal();
		}

		private void textQty_TextChanged(object sender,EventArgs e) {
			FillSubtotal();
		}

		private void FillSubtotal(){
			if(textQty.errorProvider1.GetError(textQty)!=""
				|| textPrice.errorProvider1.GetError(textPrice)!="") 
			{	
				return;
			}
			if(textQty.Text=="" || textPrice.Text==""){
				return;
			}
			int qty=PIn.PInt(textQty.Text);
			double price=PIn.PDouble(textPrice.Text);
			double subtotal=qty*price;
			textSubtotal.Text=subtotal.ToString("n");
		}

		private void butOK_Click(object sender,EventArgs e) {
			if(textQty.errorProvider1.GetError(textQty)!=""
				|| textPrice.errorProvider1.GetError(textPrice)!="")
			{
				MsgBox.Show(this,"Please fix data entry errors first.");
				return;
			}
			ItemCur.Qty=PIn.PInt(textQty.Text);
			ItemCur.Price=PIn.PDouble(textPrice.Text);
			SupplyOrderItems.WriteObject(ItemCur);
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		

		
	}
}