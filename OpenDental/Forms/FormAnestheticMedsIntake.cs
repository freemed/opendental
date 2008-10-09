using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using OpenDentBusiness;
using OpenDental.UI;

namespace OpenDental
{
	public partial class FormAnestheticMedsIntake : Form{

		public FormAnestheticMedsIntake()
		{
			InitializeComponent();
			Lan.F(this);
		}

		private void FormAnestheticMedsIntake_Load(object sender, EventArgs e)
		{
			if (!Security.IsAuthorized(Permissions.AnesthesiaIntakeMeds))
			{

				DialogResult = DialogResult.Cancel;
				return;
			}
		
		}

		private void textDate_TextChanged(object sender, EventArgs e)
		{

		}

		private void textAnesthMed_TextChanged(object sender, EventArgs e)
		{

		}

		private void textDate_TextChanged_1(object sender, EventArgs e)
		{

		}

		private void butCancel_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.Cancel;
		}

		//this will ultimately be moved to main OD setup menu, here for convenience now
		private void butSetupAnesthMeds_Click(object sender, EventArgs e)
		{
			FormAnestheticMedsInventory FormI = new FormAnestheticMedsInventory();
			FormI.ShowDialog();
			
		}

		private void butAddSupplier_Click(object sender, EventArgs e)

		{
			FormAnesthMedSuppliers FormMS = new FormAnesthMedSuppliers();
			FormMS.ShowDialog();
		}


	}
}
