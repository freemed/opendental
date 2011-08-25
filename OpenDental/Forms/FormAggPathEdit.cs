using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental {
	public partial class FormAggPathEdit:Form {
		public AggPath AggPathCur;
		public bool IsNew;

		public FormAggPathEdit() {
			InitializeComponent();
			Lan.F(this);
		}

		private void FormAggPathEdit_Load(object sender,EventArgs e) {
			textURI.Text=AggPathCur.RemoteURI;
			textUserName.Text=AggPathCur.RemoteUserName;
			textPassword.Text=AggPathCur.RemotePassword;
		}

		private void butDelete_Click(object sender,EventArgs e) {
			if(IsNew) {
				DialogResult=DialogResult.Cancel;
				return;
			}
			AggPaths.Delete(AggPathCur.AggPathNum);
			DialogResult=DialogResult.OK;
		}

		private void butOK_Click(object sender,EventArgs e) {
			if(textURI.Text=="") {
				MsgBox.Show(this,"Please enter a remote URI.");
				return;
			}
			if(textUserName.Text=="") {
				MsgBox.Show(this,"Please enter a username.");
				return;
			}
			if(textPassword.Text=="") {
				if(!MsgBox.Show(this,MsgBoxButtons.OKCancel,"Do you want to save with no password?")) {
					return;
				}
			}
			AggPathCur.RemoteURI=textURI.Text;
			AggPathCur.RemoteUserName=textUserName.Text;
			AggPathCur.RemotePassword=textPassword.Text;
			//here is where you do insert or update
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		


	}
}