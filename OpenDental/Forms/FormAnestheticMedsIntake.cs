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
        

		public FormAnestheticMedsIntake()

        {
            InitializeComponent();
            //Binds date to the textDate textbox.
            textDate.Text = MiscData.GetNowDateTime().ToString("yyyy-MM-dd");
            //Binds the combobox comboBoxAnesthMed with Medication names from the database.
            this.comboAnesthMedName.Items.Clear();
            this.comboAnesthMedName.Items.Insert(0, "--Select--");
            int noOfRows = bindComboQueries.bindAMedName().Tables[0].Rows.Count;
            for (int i = 0; i <= noOfRows - 1; i++)
            {
                this.comboAnesthMedName.Items.Add(bindComboQueries.bindAMedName().Tables[0].Rows[i][0].ToString());
                this.comboAnesthMedName.SelectedIndex = 0;
            }
            //Binds the combobox comboBoxSupplier with Medication names from the database.
            this.comboSupplierName.Items.Clear();
            this.comboSupplierName.Items.Insert(0, "--Select--");
            int noOfRows2 = bindComboQueries.bindSuppliers().Tables[0].Rows.Count;
            for (int i = 0; i <= noOfRows2 - 1; i++)
            {
                this.comboSupplierName.Items.Add(bindComboQueries.bindSuppliers().Tables[0].Rows[i][0].ToString());
                this.comboSupplierName.SelectedIndex = 0;
            }
            Lan.F(this);
        }

        private void FormAnestheticMedsIntake_Load(object sender, EventArgs e)
        {


        }

        
	
		private void textDate_TextChanged(object sender, EventArgs e)
		{

		}

		private void textAnesthMed_TextChanged(object sender, EventArgs e)
		{

		}



		private void butCancel_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.Cancel;
		}


		private void butAddSupplier_Click(object sender, EventArgs e)

		{

            FormAnesthMedSuppliers FormMS = new FormAnesthMedSuppliers();
            FormMS.ShowDialog();

		}

        private void comboAnesthMed_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void butClose_Click(object sender, EventArgs e)
        {
            if (comboAnesthMedName.SelectedIndex == 0 || textQty.Text == "" || comboSupplierName.SelectedIndex == 0 || textInvoiceNum.Text == "" )
            {
                MessageBox.Show("All fields are mandatory.");
            }
            else
            {
                Regex regex = new Regex("^\\d{1,3}?$");
                if (!(regex.IsMatch(textQty.Text)) && textQty.Text != "")
                {
                    MessageBox.Show("The Quantity field should be a 1-3 digit integer.");
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
                        else
                        {
                            int supplierID = DataConnection.getSupplierID(comboSupplierName.SelectedItem.ToString());
                            AMedication.InsertMed_Intake(comboAnesthMedName.SelectedItem.ToString(), Convert.ToInt32(textQty.Text), supplierID.ToString(), textInvoiceNum.Text);
                            this.Hide();
                            FormAnestheticMedsInventory FAMI = new FormAnestheticMedsInventory();
                            FAMI.ShowDialog();
                        }
                    }
                }
            }
        }
    }
}
