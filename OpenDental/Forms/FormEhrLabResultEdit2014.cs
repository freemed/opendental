using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using OpenDentBusiness;
using System.Drawing.Printing;
using OpenDental.UI;

namespace OpenDental {
	public partial class FormEhrLabResultEdit2014:Form {

		public FormEhrLabResultEdit2014() {
			InitializeComponent();
		}

		private void FormLabResultEdit_Load(object sender,EventArgs e) {
		}

		private void butCancel_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		
	}
}
