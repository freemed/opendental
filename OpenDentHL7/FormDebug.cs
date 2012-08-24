using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace OpenDentHL7 {
	//WARNING!!  When run in debug from VS, must be run as admin.

	public partial class FormDebug:Form {
		string ServiceName;//just for debug

		public FormDebug(string serviceName) {
			ServiceName=serviceName;
			InitializeComponent();
		}

		private void FormDebug_Load(object sender,EventArgs e) {
			ServiceHL7 service=new ServiceHL7();
			service.ServiceName=ServiceName;
			try {
				service.StartManually();
			}
			catch(Exception ex) {
				MessageBox.Show(ex.Message);
			}
		}

	
	}
}
