using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using OpenDental.DataAccess;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using OpenDental.UI;
using OpenDentBusiness;

namespace OpenDental {
	public partial class FormAnestheticMedsAdjQtys : Form
	{

        //private ODGrid gridAnesthMedsAdjQty;
        private List<AnesthMedsInventory> listAnestheticMeds;


		public FormAnestheticMedsAdjQtys()

       
		{
			InitializeComponent();
			Lan.F(this);
		}


        private void FormAnestheticMedsAdjQtys_Load(object sender, EventArgs e)
        {
            FillGrid();
        }
       
        
        
        private void FillGrid(){

            listAnestheticMeds = AnestheticMeds.CreateObjects();
            gridAnesthMedsAdjQty.BeginUpdate();
            gridAnesthMedsAdjQty.Columns.Clear();
            ODGridColumn col = new ODGridColumn(Lan.g(this, "Anesthetic Medication"), 200);
            gridAnesthMedsAdjQty.Columns.Add(col);
            col = new ODGridColumn(Lan.g(this, "How Supplied"), 200);
            gridAnesthMedsAdjQty.Columns.Add(col);
            col = new ODGridColumn(Lan.g(this, "Quantity on Hand (mL)"), 180);
            gridAnesthMedsAdjQty.Columns.Add(col);
            col = new ODGridColumn(Lan.g(this, "Adjustment (mL)"), 100);
            gridAnesthMedsAdjQty.Columns.Add(col);
            gridAnesthMedsAdjQty.Rows.Clear();
            ODGridRow row;
            for (int i = 0; i < listAnestheticMeds.Count; i++)
            {
                row = new ODGridRow();
                row.Cells.Add(listAnestheticMeds[i].AnesthMedName);
                row.Cells.Add(listAnestheticMeds[i].AnesthHowSupplied);
                row.Cells.Add(listAnestheticMeds[i].QtyOnHand);
                gridAnesthMedsAdjQty.Rows.Add(row);
            }
            gridAnesthMedsAdjQty.EndUpdate();
        
        }


		private void butOK_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.OK;
		}

		private void gridAnesthMeds_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{

		}

		private void butClose_Click(object sender, EventArgs e)
		{

		}

		private void button_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.Cancel;
		}

        private void gridAnesthMedsAdjQty_CellDoubleClick(object sender, OpenDental.UI.ODGridClickEventArgs e)
        {

        }

        private void groupBoxAdjQtys_Enter(object sender, EventArgs e)
        {

        }


	}
}