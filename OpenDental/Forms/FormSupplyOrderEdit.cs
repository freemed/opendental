using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental {
	public partial class FormSupplyOrderEdit:Form {
		public SupplyOrder Order;
		public List<Supplier> ListSupplier;

		///<Summary>This form is only going to be used to edit existing supplyOrders, not to add new ones.</Summary>
		public FormSupplyOrderEdit() {
			InitializeComponent();
			Lan.F(this);
		}

		private void FormSupplyOrderEdit_Load(object sender,EventArgs e) {
			textSupplier.Text=Suppliers.GetName(ListSupplier,Order.SupplierNum);
			if(Order.DatePlaced.Year>2200){
				textDatePlaced.Text="";
			}
			else{
				textDatePlaced.Text=Order.DatePlaced.ToShortDateString();
			}
			textNote.Text=Order.Note;
		}

		private void butToday_Click(object sender,EventArgs e) {
			textDatePlaced.Text=DateTime.Today.ToShortDateString();
		}

		private void butDelete_Click(object sender,EventArgs e) {
			//if(Order.IsNew){//never
			//	DialogResult=DialogResult.Cancel;
			//}
			if(!MsgBox.Show(this,true,"Delete entire order?")){
				return;
			}
			try{
				SupplyOrders.DeleteObject(Order);
			}
			catch(ApplicationException ex){
				MessageBox.Show(ex.Message);
				return;
			}
			DialogResult=DialogResult.OK;
		}

		private void butOK_Click(object sender,EventArgs e) {
			if(textDatePlaced.errorProvider1.GetError(textDatePlaced)!="")
			{
				MsgBox.Show(this,"Please fix data entry errors first.");
				return;
			}
			if(textDatePlaced.Text==""){
				Order.DatePlaced=new DateTime(2500,1,1);
			}
			else{
				Order.DatePlaced=PIn.PDate(textDatePlaced.Text);
			}
			Order.Note=textNote.Text;
			SupplyOrders.WriteObject(Order);
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

	

		
	}
}