using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using OpenDentBusiness;
using OpenDental.UI;
using OpenDental.DataAccess;
using System.Text.RegularExpressions;

namespace OpenDental{

	public partial class FormAnestheticMedsIntake : Form{

		public bool IsNew;
		public AnesthMedsInventory Med;
		public double qtyOnHand;
		public FormAnestheticMedsIntake()
		{
			
			InitializeComponent();
			//Binds date to the textDate textbox.
			textDate.Text = MiscData.GetNowDateTime().ToString("yyyy-MM-dd");
			//Binds the combobox comboBoxAnesthMed with Medication names from the database.
			this.comboAnesthMedName.Items.Clear();
			this.comboAnesthMedName.Items.Insert(0, "");
			int noOfRows = bindComboQueries.bindAMedName().Tables[0].Rows.Count;
			for (int i = 0; i <= noOfRows - 1; i++)
			{
				this.comboAnesthMedName.Items.Add(bindComboQueries.bindAMedName().Tables[0].Rows[i][0].ToString());
				this.comboAnesthMedName.SelectedIndex = 0;
			}
			//Binds the combobox comboBoxSupplier with Medication names from the database.
			this.comboSupplierName.Items.Clear();
			this.comboSupplierName.Items.Insert(0, "");
			int noOfRows2 = bindComboQueries.bindSuppliers().Tables[0].Rows.Count;
			for (int i = 0; i <= noOfRows2 - 1; i++)
			{
				this.comboSupplierName.Items.Add(bindComboQueries.bindSuppliers().Tables[0].Rows[i][0].ToString());
				this.comboSupplierName.SelectedIndex = 0;
			}
			Lan.F(this);
		}

		private void FormAnestheticMedsIntake_Load(object sender, EventArgs e){
			if (!Security.IsAuthorized(Permissions.AnesthesiaIntakeMeds))
			{
				DialogResult = DialogResult.Cancel;
				return;
			}

		}

		private void textDate_TextChanged(object sender, EventArgs e){

		}

		private void textAnesthMed_TextChanged(object sender, EventArgs e){

		}

		private void butCancel_Click(object sender, EventArgs e){
			DialogResult = DialogResult.Cancel;
		}

		private void butAddSupplier_Click(object sender, EventArgs e){

			FormAnesthMedSuppliers FormMS = new FormAnesthMedSuppliers();
			FormMS.ShowDialog();

		    //re-binds the Supplier name list to the Combo box in case a supplier is added while adding a new med
			if (FormMS.DialogResult == DialogResult.OK)
			{
				this.comboSupplierName.Items.Clear();
				this.comboSupplierName.Items.Insert(0, "");
				int noOfRows2 = bindComboQueries.bindSuppliers().Tables[0].Rows.Count;
				for (int i = 0; i <= noOfRows2 - 1; i++)
				{
					this.comboSupplierName.Items.Add(bindComboQueries.bindSuppliers().Tables[0].Rows[i][0].ToString());
					this.comboSupplierName.SelectedIndex = 0;
				}
				Lan.F(this);
			}

		}

		private void comboAnesthMed_SelectedIndexChanged(object sender, EventArgs e){


			}

		private void butClose_Click(object sender, EventArgs e){

			//the current QtyOnHand of a scheduled anesthetic medication
			double qtyOnHand = Convert.ToDouble(AMedications.GetQtyOnHand(comboAnesthMedName.SelectedItem.ToString()));
			
			
				if (comboAnesthMedName.SelectedIndex == 0 || textQty.Text == "" || comboSupplierName.SelectedIndex == 0 || textInvoiceNum.Text == "" )
				{
					MessageBox.Show("All fields are mandatory.");
				}
				else
				{
					Regex regex = new Regex("^\\d{1,6}?$");
					if (!(regex.IsMatch(textQty.Text)) && textQty.Text != "")
					{
						MessageBox.Show("The Quantity field should be a 1-6 digit integer.");
						textQty.Focus();
					}
					else
					{
						if (comboAnesthMedName.SelectedIndex != 0 && comboSupplierName.SelectedIndex != 0)
						{



							if (textInvoiceNum.Text.Trim() == "")
							{
								MessageBox.Show("Invoice # does not accept spaces");
								textInvoiceNum.Focus();
							}

						}

					}
					//records transaction into tableanesthmedsintake which tracks intake of scheduled anesthetic medications into inventory
					int supplierIDNum = AnesthMedSuppliers.GetSupplierIDNum(comboSupplierName.SelectedIndex);
					AMedications.InsertMed_Intake(comboAnesthMedName.SelectedItem.ToString(), Convert.ToInt32(textQty.Text), supplierIDNum.ToString(), textInvoiceNum.Text);
					
					//updates QtyOnHand in tableanesthmedsinventory when a new order of scheduled anesthetic medications is received into inventory
					AMedications.updateMed_adj(comboAnesthMedName.SelectedItem.ToString(), Convert.ToDouble(textQty.Text), qtyOnHand);
							
					DialogResult = DialogResult.OK;
					Close();
					}
				}
		

		private void textQty_TextChanged(object sender, EventArgs e)
		{

		}
	}
}
