using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental {
	public partial class FormAnesthMedsEdit:Form {
		public AnesthMedsInventory Med;
        

		public FormAnesthMedsEdit() {
			InitializeComponent();
			Lan.F(this);
		}

		private void FormAnesthMedsEdit_Load(object sender,EventArgs e) {

           
			textAnesthMedName.Text= Med.AnesthMedName;
			textAnesthHowSupplied.Text = Med.AnesthHowSupplied;
            
            
		}

		private void butDelete_Click(object sender,EventArgs e) {
			if(Med.IsNew){
				DialogResult=DialogResult.Cancel;
			}
			if(!MsgBox.Show(this,true,"Delete?")){
				return;
			}
            AnestheticMeds.DeleteObject(Med);

            try{
            AnestheticMeds.DeleteObject(Med);
            }
            catch(ApplicationException ex){
                MessageBox.Show(ex.Message);
                return;
            }
            DialogResult =DialogResult.OK;
		}

		private void butOK_Click(object sender,EventArgs e) {

			if(textAnesthMedName.Text==""){
				MsgBox.Show(this,"Please enter a name.");
				return;
			}
            Med.AnesthMedName=textAnesthMedName.Text;
		    Med.AnesthHowSupplied=textAnesthHowSupplied.Text;
          
            if (Med.QtyOnHand == null)
            {
                Med.QtyOnHand = "0";
            }

			AnestheticMeds.WriteObject(Med);
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

        private void textAnesthMedName_TextChanged(object sender, EventArgs e)
        {

        }

        private void textQtyOnHand_TextChanged(object sender, EventArgs e)
        {

        }


	}
}