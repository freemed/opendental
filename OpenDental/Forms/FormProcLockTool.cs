using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental {
	public partial class FormProcLockTool:Form {
		public FormProcLockTool() {
			InitializeComponent();
			Lan.F(this);
		}

		private void FormProcLockTool_Load(object sender,EventArgs e) {
			textDate1.Text=DateTime.Today.AddDays(-3).ToShortDateString();
			textDate2.Text=DateTime.Today.AddDays(-1).ToShortDateString();
		}

		private void butOK_Click(object sender,EventArgs e) {
			if(textDate1.errorProvider1.GetError(textDate1)!=""
				|| textDate2.errorProvider1.GetError(textDate2)!="")
			{
				MsgBox.Show(this,"Please fix data entry errors first.");
				return;
			}
			DateTime date1=PIn.Date(textDate1.Text);
			DateTime date2=PIn.Date(textDate2.Text);


			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

	
	}
}