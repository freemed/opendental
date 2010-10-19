using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace OpenDental {
	public partial class FormRpOutIns:Form {
		public FormRpOutIns() {
			InitializeComponent();
			Lan.F(this);

		}

		private void butOK_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		private void InitializeComponent() {
			this.SuspendLayout();
			// 
			// FormRpOutIns
			// 
			this.ClientSize = new System.Drawing.Size(284,262);
			this.Name = "FormRpOutIns";
			this.Load += new System.EventHandler(this.FormRpOutIns_Load);
			this.ResumeLayout(false);

		}

		private void FormRpOutIns_Load(object sender,EventArgs e) {

		}
	}
}