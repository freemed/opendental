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
		///<summary>Set to true if part of the update process.  Makes it behave more discretely to avoid worrying people.</summary>
		public bool IsUpdate;

		public FormShutdown() {
			InitializeComponent();
			Lan.F(this);
		}

		private void FormShutdown_Load(object sender,EventArgs e) {
			List<string> runningComps=Computers.GetRunningComputers();
			for(int i=0;i<runningComps.Count;i++) {
				listMain.Items.Add(runningComps[i]);
			}
			if(IsUpdate) {
				butShutdown.Text=Lan.g(this,"Continue");
			}
		}

		private void butShutdown_Click(object sender,EventArgs e) {
			if(IsUpdate) {
				DialogResult=DialogResult.OK;
				return;
			}
			if(!MsgBox.Show(this,MsgBoxButtons.OKCancel,"Shutdown this program on all workstations except this one?  Users will be given a 15 second warning to save data.")) {
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