using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using OpenDental.DataAccess;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using OpenDentBusiness;
using OpenDental.UI;

namespace OpenDental
{
	public partial class FormAnesthMedDelDose : Form{

		public AnestheticMedsGiven Med;

		public FormAnesthMedDelDose(){
			InitializeComponent();
			Lan.F(this);
		}

		private void FormAnesthMedDelDose_Load(object sender, EventArgs e){

			textAnesthMedName.Text = Med.AnesthMedName;
			textDose.Text = Med.QtyGiven; 
			textDoseTimeStamp.Text = Med.DoseTimeStamp;
		}

		private void textDate_TextChanged(object sender, EventArgs e){

		}

		private void textAnesthMed_TextChanged(object sender, EventArgs e){

		}

		private void butCancel_Click(object sender, EventArgs e){

			DialogResult = DialogResult.Cancel;
		}

		private void butClose_Click(object sender, EventArgs e){

			Close(); 
				
		}

		private void textDose_TextChanged(object sender, EventArgs e){

		}

		private void butDelAnesthMeds_Click(object sender, EventArgs e){

			DialogResult = DialogResult.OK;
		}

		private void groupBoxAnesthMedDelete_Enter(object sender, EventArgs e){

		}




	}
}
