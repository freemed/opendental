using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace OpenDental {
	public partial class FormAnestheticMedsAdjQtys : Form
	{
		public FormAnestheticMedsAdjQtys()
		{
			InitializeComponent();
			Lan.F(this);
		}

       /* private void FillGrid();
   
	
            DataSetAnesthMedAdjQty=null;
			gridAnesthMedAdjQty.BeginUpdate();
			gridAnesthMedAdjQty.Columns.Clear();
			ODGridColumn col;
			List<DisplayField> fields=DisplayFields.GetForCategory(DisplayFieldCategory.AnesthMedAdjQty);
            DataTable table=DataSetAnesthMedAdjQty.Tables["AnesthMedAdjQty"];
			ProcList=new List<DataRow>();
            row=new ODGridRow();
		    row.ColorLborder=Color.Black;
			//remember that columns that start with lowercase are already altered for display rather than being raw data.
			
            for(int f=0;f<fields.Count;f++) 
    
                {
				switch(fields[f].InternalName)
                    {
					case "Date":
					    row.Cells.Add(table.Rows[i]["procDate"].ToString());
					break;
					case "Time":
						row.Cells.Add(table.Rows[i]["procTime"].ToString());
					break;
					case "Th":
						row.Cells.Add(table.Rows[i]["toothNum"].ToString());
					break;
					case "Surf":
					    row.Cells.Add(table.Rows[i]["Surf"].ToString());
					break;
					case "Dx":
						row.Cells.Add(table.Rows[i]["dx"].ToString());
					break;
					case "Description":
						row.Cells.Add(table.Rows[i]["description"].ToString());
					break;
					case "Stat":
						row.Cells.Add(table.Rows[i]["procStatus"].ToString());
					break;
					case "Prov":
						row.Cells.Add(table.Rows[i]["prov"].ToString());
					break;
					case "Amount":
						row.Cells.Add(table.Rows[i]["procFee"].ToString());
					break;
					case "ADA Code":
						row.Cells.Add(table.Rows[i]["ProcCode"].ToString());
					break;
					case "User":
						row.Cells.Add(table.Rows[i]["user"].ToString());
					break;
					case "Signed":
						row.Cells.Add(table.Rows[i]["signature"].ToString());
					break;
                    }
                 }*/
       

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
	}
}