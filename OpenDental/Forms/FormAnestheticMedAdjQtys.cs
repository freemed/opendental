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

		private void butOK_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.OK;
		}

		private void gridAnesthMeds_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{

		}

		private void BasicTemplate_Load(object sender, EventArgs e)
		{

		}

		private void butClose_Click(object sender, EventArgs e)
		{

		}

		private void button_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.Cancel;
		}
	}
}