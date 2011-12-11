using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using OpenDentBusiness;

namespace CentralManager {
	public partial class FormCentralPasswordCheck:Form {

		public FormCentralPasswordCheck() {
			InitializeComponent();
		}

		private void FormCentralPasswordCheck_Load(object sender,EventArgs e) {
			
		}

		private void butOK_Click(object sender,EventArgs e) {
			if(PrefC.GetString(PrefName.CentralManagerPassHash)!=Userods.EncryptPassword(textPassword.Text)){
				MessageBox.Show("Bad password.");
				return;
			}
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		


	}
}