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
			for(int i=0;i<DefB.Short[(int)DefCat.SupplyCats].Length;i++){
				comboCategory.Items.Add(DefB.Short[(int)DefCat.SupplyCats][i].ItemName);
				if(Supp.Category==DefB.Short[(int)DefCat.SupplyCats][i].DefNum){
					comboCategory.SelectedIndex=i;
				}
			}
			if(comboCategory.SelectedIndex==-1){
				comboCategory.SelectedIndex=0;//There are no hidden cats, and presence of cats is checked before allowing user to add new.
			}
			textCatalogNumber.Text=Supp.CatalogNumber;
			textCatalogDescript.Text=Supp.CatalogDescript;
			textCommonName.Text=Supp.CommonName;
			textUnitType.Text=Supp.UnitType;
			if(Supp.LevelDesired!=0){
				textLevelDesired.Text=Supp.LevelDesired.ToString();
			}
			if(Supp.Price!=0){
				textPrice.Text=Supp.Price.ToString("c");
			}
			textNote.Text=Supp.Note;
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
			if(textCatalogDescript.Text=="" && textCommonName.Text==""){
				MsgBox.Show(this,"Please enter a catalog description or a common name.");
				return;
			}
			Supp.Category=DefB.Short[(int)DefCat.SupplyCats][comboCategory.SelectedIndex].DefNum;
			Supp.CatalogNumber=textCatalogNumber.Text;
			Supp.CatalogDescript=textCatalogDescript.Text;
			Supp.CommonName=textCommonName.Text;
			Supp.UnitType=textUnitType.Text;
			Supp.LevelDesired=PIn.PFloat(textLevelDesired.Text);
			Supp.Price=PIn.PDouble(textPrice.Text);
			Supp.Note=textNote.Text;
			Supp.IsHidden=checkIsHidden.Checked;
			Supplies.WriteObject(Supp);
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		
	}
}