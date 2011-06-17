using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental {
	///<summary>Cannot return OK without a proper date.</summary>
	public partial class FormOrthoChartAddDate:Form {
		public DateTime SelectedDate;

		public FormOrthoChartAddDate() {
			InitializeComponent();
			SelectedDate=new DateTime();
			Lan.F(this);
		}

		private void FormOrthoChartAddDate_Load(object sender,EventArgs e) {

		}

		private void butOK_Click(object sender,EventArgs e) {
			if(!DateTime.TryParse(textNewDate.Text,out SelectedDate)) {
				MsgBox.Show(this,"Please fix date entry.");
				return;
			}
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

	}
}