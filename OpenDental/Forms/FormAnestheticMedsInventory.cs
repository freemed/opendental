using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using OpenDental.UI;
using OpenDentBusiness;

namespace OpenDental {
	public partial class AnestheticMedsInventory:Form {

        private List<AnesthMed> listAnestheticMeds;

		public AnestheticMedsInventory() {
			InitializeComponent();
			Lan.F(this);
		}
        private void FormAnestheticMedsInventory_Load(object sender, System.EventArgs e)
        {

            FillGrid();
        }

        private void FillGrid()
        {

            listAnestheticMeds = AnestheticMeds.CreateObjects();
            gridAnesthMedsInventory.BeginUpdate();
            gridAnesthMedsInventory.Columns.Clear();
            ODGridColumn col = new ODGridColumn(Lan.g("TableAnesthMedsInventory", "Anesthetic Medication"), 200);
            gridAnesthMedsInventory.Columns.Add(col);
            col = new ODGridColumn(Lan.g("TableAnesthMedsInventory", "How Supplied"), 200);
            gridAnesthMedsInventory.Columns.Add(col);
            col = new ODGridColumn(Lan.g("TableAnesthMedsInventory", "Quantity on Hand"), 180);
            gridAnesthMedsInventory.Columns.Add(col);
            gridAnesthMedsInventory.Rows.Clear();
            ODGridRow row;
            for (int i = 0; i < listAnestheticMeds.Count; i++)
            {
                row = new ODGridRow();
                row.Cells.Add(listAnestheticMeds[i].AnesthMedName);
                row.Cells.Add(listAnestheticMeds[i].AnesthHowSupplied);
                row.Cells.Add(listAnestheticMeds[i].QtyOnHand);
                gridAnesthMedsInventory.Rows.Add(row);
            }
            gridAnesthMedsInventory.EndUpdate();

        }

        private void butAddAnesthMeds_Click(object sender, EventArgs e)
        {
            AnesthMed med = new AnesthMed();
            med.IsNew = true;
            FormAnestheticMedsEdit FormME = new FormAnestheticMedsEdit();
            FormME.Med = med;
            FormME.ShowDialog();
            if (FormME.DialogResult == DialogResult.OK)
            {
                FillGrid();
            }
        }
        private void gridAnesthMedsInventory_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void butAnesthMedIntake_Click(object sender, EventArgs e)
        {

            if (!Security.IsAuthorized(Permissions.AnesthesiaIntakeMeds))
            {

                butAnesthMedIntake.Enabled = false;
                return;
            }

            else
            {
                FormAnestheticMedsIntake FormI = new FormAnestheticMedsIntake();
                FormI.ShowDialog();



            }

        }

        private void labelIntakeNewMeds_Click(object sender, EventArgs e)
        {

        }

        private void butAdjustQtys_Click(object sender, EventArgs e)
        {

            Userod curUser = Security.CurUser;

            if (GroupPermissions.HasPermission(curUser.UserGroupNum, Permissions.AnesthesiaControlMeds))
            {

                FormAnestheticMedsAdjQtys FormA = new FormAnestheticMedsAdjQtys();
                FormA.ShowDialog();
                return;
            }

            else
            {

                MessageBox.Show(this, "You must be an administrator to unlock this action");
                return;

            }

        }

        private void butClose_Click(object sender, EventArgs e)
        {

        }

        private void butEditAnesthMeds_Click(object sender, EventArgs e)
        {

        }

        private void butDelAnesthMeds_Click(object sender, EventArgs e)
        {

        }

        private void gridAnesthMedsInventory_CellDoubleClick(object sender, ODGridClickEventArgs e)
        {
            FormAnestheticMedsEdit FormME = new FormAnestheticMedsEdit();
            FormME.Med = listAnestheticMeds[e.Row];
            FormME.ShowDialog();
            if (FormME.DialogResult == DialogResult.OK)
            {
                FillGrid();
            }
        }

		private void butOK_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}
	}
}