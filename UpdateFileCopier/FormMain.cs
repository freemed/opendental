using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace UpdateFileCopier {
	public partial class FormMain:Form {
		private string SourceDirectory;
		private int OpenDentProcessId;

		public FormMain(string sourceDirectory,string openDentProcessId) {
			InitializeComponent();
			SourceDirectory=sourceDirectory;
			OpenDentProcessId=Int32.Parse(openDentProcessId);
		}

		private void FormMain_Load(object sender,EventArgs e) {
			Cursor=Cursors.WaitCursor;
			//label1.Text="SourceDir: "+SourceDirectory;
			DateTime now;
			if(OpenDentProcessId!=0){//it could be zero for debugging
				try {
					Process process=Process.GetProcessById(OpenDentProcessId);
					now=DateTime.Now;
					while(!process.HasExited) {
						Application.DoEvents();
					}
					//TimeSpan span=DateTime.Now-now;
					//MessageBox.Show("Time waited to exit: "+span.ToString());//~.07 seconds
				}
				catch { }//sometimes, it happens so fast that it would fail to get the processById.
			}
			//wait for a moment to make sure it has really exited.
			now=DateTime.Now;
			while(DateTime.Now < now.AddSeconds(1)) {
				Application.DoEvents();
			}
			DirectoryInfo dirInfo=new DirectoryInfo(SourceDirectory);
			string startupFolder=Application.StartupPath;
			FileInfo[] appfiles=dirInfo.GetFiles();
			for(int i=0;i<appfiles.Length;i++) {
				if(appfiles[i].Name=="UpdateFileCopier.exe") {
					continue;//skip this one.
				}
				if(appfiles[i].Name=="OpenDentalServerConfig.xml") {
					continue;//skip also
				}
				File.Copy(appfiles[i].FullName,Path.Combine(startupFolder,appfiles[i].Name),true);
			}
			label1.Text="brief pause";
			Application.DoEvents();
			Thread.Sleep(500);
			Process.Start("OpenDental.exe");
			Cursor=Cursors.Default;
			Application.Exit();
		}
	}
}
