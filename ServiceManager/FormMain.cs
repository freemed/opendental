using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.ServiceProcess;
using System.Text;
using System.Windows.Forms;

namespace ServiceManager {
	public partial class FormMain:Form {
		public FormMain() {
			InitializeComponent();
		}

		private void FormMain_Load(object sender,EventArgs e) {
			SetStatus();
		}

		private void SetStatus() {
			ServiceController service=null;
			ServiceController[] services=ServiceController.GetServices();
			//bool installed=false;
			//ServiceController
			for(int i=0;i<services.Length;i++) {
				if(services[i].ServiceName=="OpenDentalHL7") {
					service=services[i];
					break;
				}
			}
			if(service != null) {//installed) {
				textStatus.Text="Installed";
				butInstall.Enabled=false;
				butUninstall.Enabled=true;
				if(service.Status==ServiceControllerStatus.Running) {
					textStatus.Text+=", Running";
					butStart.Enabled=false;
					butStop.Enabled=true;
				}
				else {
					textStatus.Text=", Stopped";
					butStart.Enabled=true;
					butStop.Enabled=false;
				}
			}
			else {
				textStatus.Text="Not installed";
				butInstall.Enabled=true;
				butUninstall.Enabled=false;
				butStart.Enabled=false;
				butStop.Enabled=false;
			}
		}

		private void butInstall_Click(object sender,EventArgs e) {

		}

		private void butUninstall_Click(object sender,EventArgs e) {

		}

		private void butStart_Click(object sender,EventArgs e) {

		}

		private void butStop_Click(object sender,EventArgs e) {

		}
	}
}
