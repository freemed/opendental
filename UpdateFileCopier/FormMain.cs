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
			//kill all processes named OpenDental.
			//If the software has been rebranded, this won't do anything, but the original exe will still be correctly closed.
			Process[] processes=Process.GetProcessesByName("OpenDental");
			for(int i=0;i<processes.Length;i++) {
				processes[i].Kill();//CloseMainWindow and Close were ineffective if a dialog was open.
			}
			/* Don't bother with this anymore.  It always happens very quickly anyway.
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
			}*/
			//wait for a moment to make sure they have really exited.
			DateTime now=DateTime.Now;
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
			//Not sure what to do about this line in rebranding situations:
			//I guess it will have to be hard coded.
			Process.Start(Path.Combine(DestDirectory,"OpenDental.exe"));
			Cursor=Cursors.Default;
			Application.Exit();
		}
	}
}
