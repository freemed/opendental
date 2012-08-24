using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using Microsoft.Win32;
using System.IO;


namespace ServiceManager {
	public partial class FormServiceManage:Form {
		public ServiceController ServControllerCur;
		public List<ServiceController> allOpenDentServices;
		public string pathToExe;

		public FormServiceManage() {
			InitializeComponent();
		}

		private void FormServiceManager_Load(object sender,EventArgs e) {
			allOpenDentServices=new List<ServiceController>();
			ServiceController[] serviceControllersAll=ServiceController.GetServices();
			for(int i=0;i<serviceControllersAll.Length;i++) {
				if(serviceControllersAll[i].ServiceName.StartsWith("OpenDent")) {
					allOpenDentServices.Add(serviceControllersAll[i]);
				}
			}
			if(ServControllerCur!=null) {//installed
				textName.Text=ServControllerCur.ServiceName;
				RegistryKey hklm=Registry.LocalMachine;
				hklm=hklm.OpenSubKey(@"System\CurrentControlSet\Services\"+ServControllerCur.ServiceName);
				pathToExe=hklm.GetValue("ImagePath").ToString();
				pathToExe=pathToExe.Replace("\"","");
				pathToExe=Path.GetDirectoryName(pathToExe);
				textPathToExe.Text=pathToExe;
				textStatus.Text="Installed";
				butInstall.Enabled=false;
				butUninstall.Enabled=true;
				butBrowse.Enabled=false;
				textPathToExe.ReadOnly=true;
				textName.ReadOnly=true;
				if(ServControllerCur.Status==ServiceControllerStatus.Running) {
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
				textPathToExe.Text=Directory.GetCurrentDirectory();
				textName.Text="";
				textName.ReadOnly=false;
				textPathToExe.ReadOnly=false;
				butInstall.Enabled=true;
				butUninstall.Enabled=false;
				butStart.Enabled=false;
				butStop.Enabled=false;
			}
		}

		private void butInstall_Click(object sender,EventArgs e) {
			for(int i=0;i<allOpenDentServices.Count;i++) {//create list of all OpenDent service install paths to ensure only one service can be installed from each directory
				if(textName.Text==allOpenDentServices[i].ServiceName) {
					MessageBox.Show("Error.  An OpenDentHL7 service with this name is already installed.  Names must be unique.");
					return;
				}
				RegistryKey hklm=Registry.LocalMachine;
				hklm=hklm.OpenSubKey(@"System\CurrentControlSet\Services\"+allOpenDentServices[i].ServiceName);
				string installedServicePath=hklm.GetValue("ImagePath").ToString();
				installedServicePath=installedServicePath.Replace("\"","");
				installedServicePath=Path.GetDirectoryName(installedServicePath);
				if(installedServicePath==textPathToExe.Text) {
					MessageBox.Show("Error.  Cannot install an HL7 service from this directory.  Another OpenDentHL7 service is already installed from this directory.");
					return;
				}
			}
			Process process=new Process();
			process.StartInfo.WorkingDirectory=textPathToExe.Text;
			process.StartInfo.FileName="installutil.exe";
			//new strategy for having control over servicename
			//InstallUtil /ServiceName=OpenDentHL7_abc OpenDentHL7.exe
			process.StartInfo.Arguments="/ServiceName="+textName.Text+" OpenDentHL7.exe";
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
			ServiceController[] serviceControllersAll=ServiceController.GetServices();
			for(int i=0;i<serviceControllersAll.Length;i++) {
				if(serviceControllersAll[i].ServiceName==textName.Text) {
					ServControllerCur=serviceControllersAll[i];
				}
			}
			butRefresh_Click(this,e);
		}

		private void butUninstall_Click(object sender,EventArgs e) {
			RegistryKey hklm=Registry.LocalMachine;
			Process process=new Process();
			process.StartInfo.WorkingDirectory=textPathToExe.Text;
			process.StartInfo.FileName="installutil.exe";
			process.StartInfo.Arguments="/u /ServiceName="+textName.Text+" OpenDentHL7.exe";
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
			ServControllerCur=null;
			DialogResult=DialogResult.OK;
		}

		private void butStart_Click(object sender,EventArgs e) {
			Cursor=Cursors.WaitCursor;
			try {
				ServiceController service=new ServiceController(textName.Text);
				service.Start();
				service.WaitForStatus(ServiceControllerStatus.Running,new TimeSpan(0,0,7));
				ServControllerCur=service;
			}
			catch(Exception ex) {
				Cursor=Cursors.Default;
				MessageBox.Show(ex.Message);
			}
			Cursor=Cursors.Default;
			butRefresh_Click(this,e);
		}

		private void butStop_Click(object sender,EventArgs e) {
			Cursor=Cursors.WaitCursor;
			try {
				ServiceController service=new ServiceController(textName.Text);
				service.Stop();
				service.WaitForStatus(ServiceControllerStatus.Stopped,new TimeSpan(0,0,7));
				ServControllerCur=service;
			}
			catch(Exception ex) {
				Cursor=Cursors.Default;
				MessageBox.Show(ex.Message);
			}
			Cursor=Cursors.Default;
			butRefresh_Click(this,e);
		}

		private void butRefresh_Click(object sender,EventArgs e) {
			FormServiceManager_Load(this,e);
		}

		private void butBrowse_Click(object sender,EventArgs e) {
			FolderBrowserDialog dlg=new FolderBrowserDialog();
			dlg.SelectedPath=textPathToExe.Text;
			if(dlg.ShowDialog()==DialogResult.OK) {
				textPathToExe.Text=dlg.SelectedPath;
			}
		}
	}
}
