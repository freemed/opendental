using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using OpenDentBusiness;

namespace CentralManager {
	public partial class FormCentralConnectionEdit:Form {
		public CentralConnection CentralConnectionCur;

		public FormCentralConnectionEdit() {
			InitializeComponent();
		}

		private void FormCentralConnectionEdit_Load(object sender,EventArgs e) {
			textServerName.Text=CentralConnectionCur.ServerName;
			textDatabaseName.Text=CentralConnectionCur.DatabaseName;
			textMySqlUser.Text=CentralConnectionCur.MySqlUser;
			textMySqlPassword.Text=CentralConnectionCur.MySqlPassword;
			textServiceURI.Text=CentralConnectionCur.ServiceURI;
			textOdUser.Text=CentralConnectionCur.OdUser;
			textOdPassword.Text=CentralConnectionCur.OdPassword;
			textItemOrder.Text=CentralConnectionCur.ItemOrder.ToString();
			textNote.Text=CentralConnectionCur.Note;
		}

		private void butDelete_Click(object sender,EventArgs e) {
			if(CentralConnectionCur.IsNew) {
				DialogResult=DialogResult.Cancel;
				return;
			}
			//no prompt
			CentralConnections.Delete(CentralConnectionCur.CentralConnectionNum);
			DialogResult=DialogResult.OK;
		}

		private void butOK_Click(object sender,EventArgs e) {
			try {
				int.Parse(textItemOrder.Text);
			}
			catch {
				MessageBox.Show("Item Order invalid");
			}
			//if(textServiceURI.Text=="") {
			//	MessageBox.Show("Please enter a remote URI.");
			//	return;
			//}
			//if(textUserName.Text=="") {
			//	MessageBox.Show("Please enter a username.");
			//	return;
			//}
			//if(textPassword.Text=="") {
				//if(!MessageBox.Show(this,MsgBoxButtons.OKCancel,"Do you want to save with no password?")) {
				//	return;
				//}
			//}
			CentralConnectionCur.ServerName=textServerName.Text;
			CentralConnectionCur.DatabaseName=textDatabaseName.Text;
			CentralConnectionCur.MySqlUser=textMySqlUser.Text;
			CentralConnectionCur.MySqlPassword=textMySqlPassword.Text;
			CentralConnectionCur.ServiceURI=textServiceURI.Text;
			CentralConnectionCur.OdUser=textOdUser.Text;
			CentralConnectionCur.OdPassword=textOdPassword.Text;
			CentralConnectionCur.ItemOrder=int.Parse(textItemOrder.Text);
			CentralConnectionCur.Note=textNote.Text;
			if(CentralConnectionCur.IsNew) {
				CentralConnections.Insert(CentralConnectionCur);
			}
			else {
				CentralConnections.Update(CentralConnectionCur);
			}
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		


	}
}