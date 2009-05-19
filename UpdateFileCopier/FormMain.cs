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
		private string DestDirectory;
		private int OpenDentProcessId;

		public FormMain(string sourceDirectory,string openDentProcessId,string destDirectory) {
			InitializeComponent();
			SourceDirectory=sourceDirectory;
			DestDirectory=destDirectory;
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
			DirectoryInfo dirInfoSource=new DirectoryInfo(SourceDirectory);
			DirectoryInfo dirInfoDest=new DirectoryInfo(DestDirectory);
			FileInfo[] appfiles=dirInfoSource.GetFiles();
			for(int i=0;i<appfiles.Length;i++) {
				//if(appfiles[i].Name=="UpdateFileCopier.exe") {
				//	continue;//skip this one.
				//}
				//any file exclusions will have happened when originally copying files into the AtoZ folder.
				File.Copy(appfiles[i].FullName,Path.Combine(DestDirectory,appfiles[i].Name),true);
			}
			label1.Text="brief pause";
			//I wonder how long we should wait here for the files to be copied over.
			//I wonder if there's any way to test the last altered time of a variety of files.  A lot of work.
			now=DateTime.Now;
			while(DateTime.Now < now.AddSeconds(3)) {
				Application.DoEvents();
			}
			Process.Start(Path.Combine(DestDirectory,"OpenDental.exe"));
			Cursor=Cursors.Default;
			Application.Exit();
		}
	}
}
