using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace OpenDental {
	public partial class FormBackupReminder:Form {
		public FormBackupReminder() {
			InitializeComponent();
			Lan.F(this);
		}

		private void butOK_Click(object sender,EventArgs e) {
			if(!checkA1.Checked
				&& !checkA2.Checked
				&& !checkA3.Checked
				&& !checkA4.Checked)
			{
				MsgBox.Show(this,"You are not allowed to continue using this program unless you are making daily backups.");
				return;
			}
			if(!checkB1.Checked
				&& !checkB2.Checked)
			{
				MsgBox.Show(this,"You are not allowed to continue using this program unless you have proof that your backups are good.");
				return;
			}
			if(!checkC1.Checked
				&& !checkC2.Checked)
			{
				MsgBox.Show(this,"You are not allowed to continue using this program unless you have a long-term strategy.");
				return;
			}
			DialogResult=DialogResult.OK;
		}

		private void FormBackupReminder_FormClosing(object sender,FormClosingEventArgs e) {
			if(DialogResult!=DialogResult.OK){
				if(!MsgBox.Show(this,true,"Program will now close.")){
					e.Cancel=true;
				}
			}
		}



	}
}