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
			//display Date
			textDate.Text = MiscData.GetNowDateTime().ToString("mm/dd/yyyy");
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


	}
}
