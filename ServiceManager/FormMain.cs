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
		List<ServiceController> serviceControllerList;

		public FormMain() {
			InitializeComponent();
		}

		private void FormMain_Load(object sender,EventArgs e) {
			FillList();
		}

		private void FillList() {
			listMain.Items.Clear();
			serviceControllerList=new List<ServiceController>();
			ServiceController[] serviceControllersAll=ServiceController.GetServices();
			for(int i=0;i<serviceControllersAll.Length;i++) {
				if(serviceControllersAll[i].ServiceName.StartsWith("OpenDent")) {
					serviceControllerList.Add(serviceControllersAll[i]);
					listMain.Items.Add(serviceControllersAll[i].ServiceName);
				}
			}
		}

		private void listMain_DoubleClick(object sender,EventArgs e) {
			if(listMain.SelectedIndex==-1) {
				return;
			}
			FormServiceManage FormS=new FormServiceManage();
			FormS.allOpenDentServices=serviceControllerList;
			FormS.ServControllerCur=serviceControllerList[listMain.SelectedIndex];
			FormS.ShowDialog();
			FillList();
		}

		private void butAdd_Click(object sender,EventArgs e) {
			FormServiceManage FormS=new FormServiceManage();
			FormS.allOpenDentServices=serviceControllerList;
			FormS.ShowDialog();
			FillList();
		}
	}
}
