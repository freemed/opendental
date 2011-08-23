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
		public bool isNew;

		public FormAggPathEdit() {
			AggPathCur = new AggPath();
			InitializeComponent();
			Lan.F(this);
		}

		private void FormAggPathEdit_Load(object sender,EventArgs e) {
			textURI.Text=AggPathCur.RemoteURI;
			textUserName.Text=AggPathCur.RemoteUserName;
			textPassword.Text=AggPathCur.RemotePassword;
		}

		private void butOK_Click(object sender,EventArgs e) {
			if(textURI.Text=="") {
				MsgBox.Show(this,"Aggrigation paths cannot have a blank URI.");
				return;
			}
			if(textUserName.Text=="") {
				MsgBox.Show(this,"Please enter a username.");
				return;
			}
			if(textPassword.Text=="") {
				if(!MsgBox.Show(this,MsgBoxButtons.OKCancel,"Do you want to save this aggregation path with no password?")) {
					return;
				}
			}
			AggPathCur.RemoteURI=textURI.Text;
			AggPathCur.RemoteUserName=textUserName.Text;
			AggPathCur.RemotePassword=textPassword.Text;
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		private void butDelete_Click(object sender,EventArgs e) {
			if(isNew) {
				DialogResult=DialogResult.Cancel;
			}
			else {
				AggPaths.Delete(AggPathCur.AggPathNum);
			}
			DialogResult=DialogResult.Cancel;
		}


	}
}