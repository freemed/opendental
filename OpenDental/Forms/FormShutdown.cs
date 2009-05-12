using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental {
	public partial class FormShutdown:Form {
		public FormShutdown() {
			InitializeComponent();
			Lan.F(this);
		}

		private void FormShutdown_Load(object sender,EventArgs e) {
			List<string> runningComps=Computers.GetRunningComputers();
			for(int i=0;i<runningComps.Count;i++) {
				listMain.Items.Add(runningComps[i]);
			}
		}

		private void butShutdown_Click(object sender,EventArgs e) {
			if(!MsgBox.Show(this,MsgBoxButtons.OKCancel,"Shutdown this program on all workstations except this one?  Users will be given a 10 second warning to save data.")) {
				return;
			}
			//happens outside this form
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		

		
	}
}