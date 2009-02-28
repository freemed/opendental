using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.ServiceProcess;
using System.Text;
using System.Windows.Forms;

namespace ServiceManager {
	public partial class FormMain:Form {
		//ServiceController service=null;

		public FormMain() {
			InitializeComponent();
		}

		private void FormMain_Load(object sender,EventArgs e) {
			SetStatus();
		}

		private void SetStatus() {
			ServiceController service=null;
			ServiceController[] services=ServiceController.GetServices();
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
					textStatus.Text+=", Stopped";
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
			butRefresh.Select();
		}

		private void butInstall_Click(object sender,EventArgs e) {
			Process process=new Process();
			process.StartInfo.FileName="installutil.exe";
			process.StartInfo.Arguments="OpenDentHL7.exe";
			process.Start();
			try {
				process.WaitForExit(10000);
				if(process.ExitCode!=0) {
					MessageBox.Show("Error. Exit code:"+process.ExitCode.ToString());
				}
			}
			catch {
				MessageBox.Show("Error. Did not exit after 10 seconds.");
			}
			SetStatus();
		}

		private void butUninstall_Click(object sender,EventArgs e) {
			Process process=new Process();
			process.StartInfo.FileName="installutil.exe";
			process.StartInfo.Arguments="/u OpenDentHL7.exe";
			process.Start();
			try {
				process.WaitForExit(10000);
				if(process.ExitCode!=0) {
					MessageBox.Show("Error. Exit code:"+process.ExitCode.ToString());
				}
			}
			catch {
				MessageBox.Show("Error. Did not exit after 5 seconds.");
				return;
			}
			SetStatus();
		}

		private void butStart_Click(object sender,EventArgs e) {
			Cursor=Cursors.WaitCursor;
			try {
				ServiceController service=new ServiceController("OpenDentalHL7");
				service.Start();
				service.WaitForStatus(ServiceControllerStatus.Running,new TimeSpan(0,0,7));
			}
			catch(Exception ex) {
				Cursor=Cursors.Default;
				MessageBox.Show(ex.Message);
			}
			Cursor=Cursors.Default;
			SetStatus();
		}

		private void butStop_Click(object sender,EventArgs e) {
			Cursor=Cursors.WaitCursor;
			try {
				ServiceController service=new ServiceController("OpenDentalHL7");
				service.Stop();
				service.WaitForStatus(ServiceControllerStatus.Stopped,new TimeSpan(0,0,7));
			}
			catch(Exception ex) {
				Cursor=Cursors.Default;
				MessageBox.Show(ex.Message);
			}
			Cursor=Cursors.Default;
			SetStatus();
		}

		private void butRefresh_Click(object sender,EventArgs e) {
			SetStatus();
		}
	}
}
