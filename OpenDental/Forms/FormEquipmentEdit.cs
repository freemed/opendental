using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental {
	public partial class FormEquipmentEdit:Form {
		public Equipment Equip;
		public bool IsNew;

		public FormEquipmentEdit() {
			InitializeComponent();
			Lan.F(this);
		}

		private void FormEquipmentEdit_Load(object sender,EventArgs e) {
			textDateEntry.Text=Equip.DateEntry.ToShortDateString();
			textDescription.Text=Equip.Description;
			textSerialNumber.Text=Equip.SerialNumber;
			textModelYear.Text=Equip.ModelYear;
			if(Equip.DatePurchased.Year>1880) {
				textDatePurchased.Text=Equip.DatePurchased.ToShortDateString();
			}
			if(Equip.DateSold.Year>1880) {
				textDateSold.Text=Equip.DateSold.ToShortDateString();
			}
			if(Equip.PurchaseCost>0) {
				textPurchaseCost.Text=Equip.PurchaseCost.ToString("f");
			}
			if(Equip.MarketValue>0) {
				textMarketValue.Text=Equip.MarketValue.ToString("f");
			}
			textLocation.Text=Equip.Location;
		}

		private void butDelete_Click(object sender,EventArgs e) {
			if(IsNew){
				DialogResult=DialogResult.Cancel;
			}
			if(!Security.IsAuthorized(Permissions.EquipmentDelete,Equip.DateEntry)) {
				return;
			}
			if(!MsgBox.Show(this,true,"Delete?")){
				return;
			}
			try{
				Equipments.Delete(Equip);
			}
			catch(ApplicationException ex){
				MessageBox.Show(ex.Message);
				return;
			}
			DialogResult=DialogResult.OK;
		}

		private void butOK_Click(object sender,EventArgs e) {
			if(textDatePurchased.errorProvider1.GetError(textDatePurchased)!=""
				|| textDateSold.errorProvider1.GetError(textDateSold)!=""
				|| textPurchaseCost.errorProvider1.GetError(textPurchaseCost)!=""
				|| textMarketValue.errorProvider1.GetError(textMarketValue)!="")
			{
				MsgBox.Show(this,"Please fix data entry errors first.");
				return;
			}
			if(textDescription.Text==""){
				MsgBox.Show(this,"Please enter a description.");
				return;
			}
			if(textDatePurchased.Text=="") {
				MsgBox.Show(this,"Please enter date purchased.");
				return;
			}
			if(PIn.Date(textDatePurchased.Text) > DateTime.Today) {
				if(!MsgBox.Show(this,MsgBoxButtons.OKCancel,"Date is in the future.  Continue anyway?")) {
					return;
				}
			}
			Equip.Description=textDescription.Text;
			Equip.SerialNumber=textSerialNumber.Text;
			Equip.ModelYear=textModelYear.Text;
			Equip.DatePurchased=PIn.Date(textDatePurchased.Text);
			Equip.DateSold=PIn.Date(textDateSold.Text);
			Equip.PurchaseCost=PIn.Double(textPurchaseCost.Text);
			Equip.MarketValue=PIn.Double(textMarketValue.Text);
			Equip.Location=textLocation.Text;
			if(IsNew) {
				Equipments.Insert(Equip);
			}
			else {
				Equipments.Update(Equip);
			}
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

      

		
	}
}