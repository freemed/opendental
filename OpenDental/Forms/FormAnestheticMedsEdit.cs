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
	public partial class FormAnestheticMedsEdit:Form {
        public AnesthMedsInventory Med;

		public FormAnestheticMedsEdit() {
			InitializeComponent();
			Lan.F(this);
		}

		
		private void AnestheticMedsEdit_Load(object sender, EventArgs e)
		{
            textAnesthMedName.Text = Med.AnesthMedName;
            textAnesthHowSupplied.Text = Med.AnesthHowSupplied;
            
		}

		private void textAnesthMedsInventory_TextChanged(object sender, EventArgs e)
		{

		}

		private void butCancel_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.Cancel;
		}

        private void butOK_Click(object sender, EventArgs e)
        {

            //if(textDate.errorProvider1.GetError(textDate)!=""){
            //	MsgBox.Show(this,"Please fix data entry errors first.");
            //	return;
            //}
            if (textAnesthMedName.Text == "")
            {
                MsgBox.Show(this, "Please enter a name.");
                return;
            }
            Med.AnesthMedName = textAnesthMedName.Text;
            Med.AnesthHowSupplied = textAnesthHowSupplied.Text;
            AnestheticMeds.WriteObject(Med);
            DialogResult = DialogResult.OK;
        }

        private void textAnestheticMedNum_TextChanged(object sender, EventArgs e)
        {

        }

        private void textAnesthMedName_TextChanged(object sender, EventArgs e)
        {

        }
	}
}