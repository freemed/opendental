using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ServiceManager {
	public partial class FormServiceManage:Form {
		public FormServiceManage() {
			InitializeComponent();
		}

		private void butInstall_Click(object sender,EventArgs e) {
			/*
			Process process=new Process();
			process.StartInfo.FileName="installutil.exe";
			//new strategy for having control over servicename
			//InstallUtil /ServiceName=OpenDentHL7_abc OpenDentHL7.exe
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
			SetStatus();*/
		}

		private void butUninstall_Click(object sender,EventArgs e) {
			/*
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
			SetStatus();*/
		}

		private void butStart_Click(object sender,EventArgs e) {
			/*
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
			SetStatus();*/
		}

		private void butStop_Click(object sender,EventArgs e) {
			/*
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
			SetStatus();*/
		}

		private void butRefresh_Click(object sender,EventArgs e) {
			//SetStatus();
		}



	}
}
