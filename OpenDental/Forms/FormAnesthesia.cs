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
	public partial class FormAnesthesia : Form
	{
		public Patient PatCur;
		public bool isNew;

		public FormAnesthesia()
		{
			InitializeComponent();
			
			Lan.F(this);
		}

		private void butOK_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}



		private void butAnestheticRecord_Click(object sender, EventArgs e)
		{
			FormAnestheticRecord FormAR = new FormAnestheticRecord(PatCur);
			FormAR.ShowDialog();
		}

		private void butAnestheticInventory_Click(object sender, EventArgs e)
		{
			FormAnestheticMedsInventory FormMI = new FormAnestheticMedsInventory();
			FormMI.ShowDialog();
		}

		private void butReports_Click(object sender, EventArgs e)
		{	//Reports are not implemented yet. Hide this button before releases
			//FormAnesthesiaReports FormAR = new FormAnesthesiaReports();
			//FormAR.ShowDialog();
		}
	}
}