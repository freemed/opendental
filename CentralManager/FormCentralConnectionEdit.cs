using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using OpenDentBusiness;

namespace CentralManager {
	public partial class FormAggPathEdit:Form {
		public CentralConnection CentralConnectionCur;
		public bool IsNew;

		public FormAggPathEdit() {
			InitializeComponent();
		}

		private void FormAggPathEdit_Load(object sender,EventArgs e) {
			textURI.Text=CentralConnectionCur.RemoteURI;
			textUserName.Text=CentralConnectionCur.RemoteUserName;
			textPassword.Text=CentralConnectionCur.RemotePassword;
		}

		private void butDelete_Click(object sender,EventArgs e) {
			if(IsNew) {
				DialogResult=DialogResult.Cancel;
				return;
			}
			//no prompt
			//AggPaths.Delete(AggPathCur.AggPathNum);
			DialogResult=DialogResult.OK;
		}

		private void butOK_Click(object sender,EventArgs e) {
			if(textURI.Text=="") {
				MessageBox.Show("Please enter a remote URI.");
				return;
			}
			if(textUserName.Text=="") {
				MessageBox.Show("Please enter a username.");
				return;
			}
			if(textPassword.Text=="") {
				//if(!MessageBox.Show(this,MsgBoxButtons.OKCancel,"Do you want to save with no password?")) {
				//	return;
				//}
			}
			CentralConnectionCur.RemoteURI=textURI.Text;
			CentralConnectionCur.RemoteUserName=textUserName.Text;
			CentralConnectionCur.RemotePassword=textPassword.Text;
			if(IsNew) {
				//AggPaths.Insert(DbConnectionCur);
			}
			else {
				//AggPaths.Update(DbConnectionCur);
			}
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		


	}
}