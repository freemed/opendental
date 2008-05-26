using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental {
	public partial class FormSupplyEdit:Form {
		public Supply Supp;
		public List<Supplier> ListSupplier;

		public FormSupplyEdit() {
			InitializeComponent();
			Lan.F(this);
		}

		private void FormSupplyEdit_Load(object sender,EventArgs e) {
			textSupplier.Text=Suppliers.GetName(ListSupplier,Supp.SupplierNum);
			for(int i=0;i<DefC.Short[(int)DefCat.SupplyCats].Length;i++){
				comboCategory.Items.Add(DefC.Short[(int)DefCat.SupplyCats][i].ItemName);
				if(Supp.Category==DefC.Short[(int)DefCat.SupplyCats][i].DefNum){
					comboCategory.SelectedIndex=i;
				}
			}
			if(comboCategory.SelectedIndex==-1){
				comboCategory.SelectedIndex=0;//There are no hidden cats, and presence of cats is checked before allowing user to add new.
			}
			textCatalogNumber.Text=Supp.CatalogNumber;
			textDescript.Text=Supp.Descript;
			if(Supp.LevelDesired!=0){
				textLevelDesired.Text=Supp.LevelDesired.ToString();
			}
			if(Supp.Price!=0){
				textPrice.Text=Supp.Price.ToString("n");
			}
			checkIsHidden.Checked=Supp.IsHidden;
		}

		private void butDelete_Click(object sender,EventArgs e) {
			if(Supp.IsNew){
				DialogResult=DialogResult.Cancel;
			}
			if(!MsgBox.Show(this,true,"Delete?")){
				return;
			}
			try{
				Supplies.DeleteObject(Supp);
			}
			catch(ApplicationException ex){
				MessageBox.Show(ex.Message);
				return;
			}
			DialogResult=DialogResult.OK;
		}

		private void butOK_Click(object sender,EventArgs e) {
			if(textLevelDesired.errorProvider1.GetError(textLevelDesired)!=""
				|| textPrice.errorProvider1.GetError(textPrice)!="")
			{
				MsgBox.Show(this,"Please fix data entry errors first.");
				return;
			}
			if(textDescript.Text==""){
				MsgBox.Show(this,"Please enter a description.");
				return;
			}
			Supp.Category=DefC.Short[(int)DefCat.SupplyCats][comboCategory.SelectedIndex].DefNum;
			Supp.CatalogNumber=textCatalogNumber.Text;
			Supp.Descript=textDescript.Text;
			Supp.LevelDesired=PIn.PFloat(textLevelDesired.Text);
			Supp.Price=PIn.PDouble(textPrice.Text);
			Supp.IsHidden=checkIsHidden.Checked;
			if(Supp.IsHiddenChanged) {
				if(Supp.IsHidden) {
					Supp.ItemOrder=0;
				}
				else {
					Supp.ItemOrder=Supplies.GetLastItemOrder(Supp.SupplierNum,Supp.Category)+1;
				}
			}
			Supplies.WriteObject(Supp);
			if(Supp.IsHiddenChanged || Supp.CategoryChanged){
				List<Supply> listSupply=Supplies.CreateObjects(false,Supp.SupplierNum,"");
				Supplies.CleanupItemOrders(listSupply);
			}
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		
	}
}