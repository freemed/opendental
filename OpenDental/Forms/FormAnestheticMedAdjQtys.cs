using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using OpenDentBusiness;
using OpenDental.UI;
using System.Text.RegularExpressions;
using OpenDental.DataAccess;

namespace OpenDental {

	public partial class FormAnestheticMedsAdjQtys : Form{

		/*...RH Code...
		DataTable PtDataTable;
		string notes, anesthmedname, howsupplied, qty, adj, num;
		int i, newQty, oldQty, newAdj, num2 = 0, qtyOnHand = 0;
		Boolean flag = false;*/

		private List<AnesthMedsInventory> listAnestheticMeds;

		public FormAnestheticMedsAdjQtys(){

			InitializeComponent();
			Lan.F(this);
		}

		private void FormAnestheticMedsAdjQtys_Load(object sender, EventArgs e){

			FillGrid();
			/*...RH Code ...
			//Makes only the Adjustment,Notes columns editable..
			gridAnesthMedsAdjQty.Columns[0].ReadOnly = true;
			gridAnesthMedsAdjQty.Columns[1].ReadOnly = true;
			gridAnesthMedsAdjQty.Columns[2].ReadOnly = true;
			//gridAnesthMedsAdjQtys.Columns[2].MinimumWidth = 4;
			gridAnesthMedsAdjQty.Columns[3].ReadOnly = false;
			//gridAnesthMedsAdjQtys.Columns[3].MinimumWidth = 4;
			gridAnesthMedsAdjQty.Columns[4].ReadOnly = false;*/
		}

		private void FillGrid(){
			
			/*...RH Code...
			PtDataTable = AMedication.GetdataForGridADJ();
			gridAnesthMedsAdjQtys.DataSource = PtDataTable;*/
			listAnestheticMeds = AnestheticMeds.CreateObjects();
			gridAnesthMedsAdjQty.BeginUpdate();
			gridAnesthMedsAdjQty.Columns.Clear();

			ODGridColumn col = new ODGridColumn(Lan.g(this, "Anesthetic Medication"), 200);
			gridAnesthMedsAdjQty.Columns.Add(col);
			col = new ODGridColumn(Lan.g(this, "How Supplied"), 200);
			gridAnesthMedsAdjQty.Columns.Add(col);
			col = new ODGridColumn(Lan.g(this, "Quantity on Hand (mL)"), 180);
			gridAnesthMedsAdjQty.Columns.Add(col);
			col = new ODGridColumn(Lan.g(this, "Qty Adjustment (mL)"), 100);
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

		private void butClose_Click(object sender, EventArgs e){

			/*...RH Code...
			i = gridAnesthMedsAdjQtys.CurrentRow.Index;
			anesthmedname = gridAnesthMedsAdjQtys.Rows[i].Cells[0].Value.ToString();
			//aMed = gridAnesthMedsAdjQtys.Rows[i].Cells[0].Value.ToString();
			howsupplied = gridAnesthMedsAdjQtys.Rows[i].Cells[1].Value.ToString();
			qty = gridAnesthMedsAdjQtys.Rows[i].Cells[2].Value.ToString();
			int qty2 = 0;
			if (qty != "" && qty != null)
				{
					qty2 = Convert.ToInt32(qty);
				}
			if (qty != "" && qty != null)
				{
					qtyOnHand = 0;
					if (qty != "" && qty != null)
						qtyOnHand = Convert.ToInt32(qty);
						adj = gridAnesthMedsAdjQtys.Rows[i].Cells[3].Value.ToString();
					if (adj != "")
					{
						Regex regex = new Regex("^-[0-9]+$|^[0-9]+$");
						if (!regex.IsMatch(adj))
						{
							MessageBox.Show("Enter Valid adjustment quantity on hand");
						}
						else
							{
							if (adj.Contains("-"))
							{
								num = adj.Substring(1);
								num2 = Convert.ToInt32(num);
							}
							if (adj.Length <= 4)
							{
								newQty = Convert.ToInt32(adj);
								oldQty = Convert.ToInt32(qty);
								newAdj = Convert.ToInt32(adj);
							}
							else
							{
								oldQty = Convert.ToInt32(qty);
								gridAnesthMedsAdjQtys.Focus();
								MessageBox.Show("Adjustment field accepts 3 digit integer");
								flag = true;
							}

							if (adj.Contains("-") && (num2 > oldQty))
							{
								MessageBox.Show("Adjustment field should be less than the Quantity on Hand.");
							}
							else
							{
								notes = gridAnesthMedsAdjQtys.Rows[i].Cells[4].Value.ToString();
								newQty = oldQty + (newAdj);
								AMedication.updateMed_adj(anesthmedname, howsupplied, qtyOnHand, adj, notes, oldQty);
								AMedication.updateMed_adj_qty(anesthmedname, howsupplied, qtyOnHand, newQty);
								if (flag != true)
								{
									this.Hide();
									FormAnestheticMedsInventory FAMI = new FormAnestheticMedsInventory();
									FAMI.ShowDialog();
								}
							}
						}
					}
					else
					{
						notes = gridAnesthMedsAdjQtys.Rows[i].Cells[4].Value.ToString();
						AMedication.update(anesthmedname, howsupplied, qtyOnHand, notes, qty2);
						this.Hide();
						FormAnestheticMedsInventory fAMI = new FormAnestheticMedsInventory();
						fAMI.ShowDialog();
					}
				}
				else
				{
					MessageBox.Show("You cannot adjust the quantity as Quantity on hand is empty.");
				}
			}*/
		}

		private void button_Click(object sender, EventArgs e){

			DialogResult = DialogResult.Cancel;
		}

		private void gridAnesthMedsAdjQty_CellContentClick(object sender, OpenDental.UI.ODGridClickEventArgs e){

		}

		private void gridAnesthMedsAdjQty_CellDoubleClick(object sender, OpenDental.UI.ODGridClickEventArgs e){

		}

		private void groupBoxAdjQtys_Enter(object sender, EventArgs e){

		}

		/* private void gridAnesthMedsAdjQtys_CellContentClick(object sender, DataGridViewCellEventArgs e){

		}*/

	}
}