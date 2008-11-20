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
		/*...RH...DataTable PtDataTable;
		string notes, anesthmedname, howsupplied, qty, adj, num;
		int i, newQty, oldQty, newAdj, num2 = 0, qtyOnHand = 0;
		Boolean flag = false;*/

		private List<AnesthMedsInventory> listAnestheticMeds;
		public List<AnesthMedInvC> ListAnestheticMeds;
		//...RH...private List<DisplayField> fields;
		//...RH...public AnesthMedsInventoryAdj MedInvAdj;
		//...RH...public int rowindex;

		public FormAnestheticMedsAdjQtys()
		{
			InitializeComponent();
			Lan.F(this);
		}

		private void FormAnestheticMedsAdjQtys_Load(object sender, EventArgs e){
			
			FillGrid();

		}

		private void FillGrid() {

			listAnestheticMeds = AnestheticMeds.CreateObjects();
			gridAnesthMedsAdjQty.BeginUpdate();
			gridAnesthMedsAdjQty.Columns.Clear();
			ODGridColumn col = new ODGridColumn(Lan.g(this, "Anesthetic Medication"), 200);
			gridAnesthMedsAdjQty.Columns.Add(col);
			col = new ODGridColumn(Lan.g(this, "How Supplied"), 200);
			gridAnesthMedsAdjQty.Columns.Add(col);
			col = new ODGridColumn(Lan.g(this, "Quantity on Hand (mL)"), 180);
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

			/*
			PtDataTable = AMedications.GetdataForGridADJ();
			gridAnesthMedsAdjQty.BeginUpdate();
			gridAnesthMedsAdjQty.Rows.Clear();
			ODGridRow row;
			for (int i = 0; i < PtDataTable.Rows.Count; i++)
			{
				row = new ODGridRow();
				for (int f = 0; f < fields.Count; f++)
				{
					switch (fields[f].InternalName)
					{
						case "Anesthetic Medication":
							row.Cells.Add(PtDataTable.Rows[i]["Anesthetic Medication"].ToString());
							break;
						case "How Supplied":
							row.Cells.Add(PtDataTable.Rows[i]["How Supplied"].ToString());
							break;
						case "Quantity on hand(mLs)":
							row.Cells.Add(PtDataTable.Rows[i]["Quantity on hand(mLs)"].ToString());
							break;
						case "Quantity Adjustment(mLs)":
							row.Cells.Add(PtDataTable.Rows[i]["Quantity Adjustment(mLs)"].ToString());
							break;
						case "Notes":
							row.Cells.Add(PtDataTable.Rows[i]["Notes"].ToString());
							break;

					}
				}
				gridAnesthMedsAdjQty.Rows.Add(row);
			}
			gridAnesthMedsAdjQty.EndUpdate();
			gridAnesthMedsAdjQty.SetSelected(0, true);*/
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
			/*Userod curUser = Security.CurUser;
			if (GroupPermissions.HasPermission(curUser.UserGroupNum, Permissions.AnesthesiaControlMeds))
			{
				FormAnesthMedAdjQty FormMA = new FormAnesthMedAdjQty();
				FormMA.Med = listAnestheticMeds[e.Row];
				FormMA.ShowDialog();
				if (FormMA.DialogResult == DialogResult.OK)
				{
					FillGrid();
				}
				return;
			}
			else
			{
				MessageBox.Show(this, "You must be an administrator with rights to control anesthetic medication inventory levels to unlock this action");
				return;
			}
			FormAnesthMedAdjQty FormMAQ = new FormAnesthMedAdjQty();

			
			if (FormMAQ.DialogResult == DialogResult.OK)
			{
				FillGrid();
            }*/

			FillGrid();
        }


	}
}