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
			OpenDentProcessId=Int32.Parse(openDentProcessId);//deprecated, but we'll just leave it here.
		}

		private void FormMain_Load(object sender,EventArgs e) {

		}

		private void FormMain_Shown(object sender,EventArgs e) {
			Cursor=Cursors.WaitCursor;
			//kill all processes named OpenDental.
			//If the software has been rebranded, the original exe will NOT be correctly closed.
			KillProcess("OpenDental");
			//kill all processes named WebCamOD.
			//web cam does not always close properly when updater kills OpenDental
			//web cam relies on shared library, OpenDentBusiness.dll (shared ref with OpenDental). 
			//if this lib can't be updated then the opendental update/install fails
			KillProcess("WebCamOD");
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
			Thread.Sleep(300);
			DirectoryInfo dirInfoSource=new DirectoryInfo(SourceDirectory);
			//DirectoryInfo dirInfoDest=new DirectoryInfo(DestDirectory);
			FileInfo[] appfiles=dirInfoSource.GetFiles();
			//create install log file directory
			/*Maybe later
			string logDir = Path.Combine(DestDirectory,"InstallLogs");
			if(!Directory.Exists(logDir)) {
				Directory.CreateDirectory(logDir);
			}
			//create the log file so we can document the status of each file
			using(StreamWriter sw = new StreamWriter(Path.Combine(logDir,string.Format(@"OD_Install {0}.txt",DateTime.Now.ToString("yyyy-MMM-dd hh-mm-tt"))))) {
				sw.WriteLine(string.Format(@"Copying {0} files...",appfiles.Length));*/
				for(int i=0;i<appfiles.Length;i++) {
					//Any file exclusions will have happened when originally copying files into the AtoZ folder.
					//And that happens in OpenDental.PrefL.CheckProgramVersion().
					string source = appfiles[i].FullName;
					string dest = Path.Combine(DestDirectory,appfiles[i].Name);
					try {
						//sw.WriteLine(string.Format(@"Copying file: {0} to {1}...",source,dest));
						File.Copy(source,dest,true);
					}
					catch{//Exception ex) {
						//silently fail.  This can prevent all kinds of problems if there are extra files sitting around.
						/*sw.WriteLine(
							string.Format(@"***** File copy failed *****{0}Source: {1}{0}Dest: {2}{0}Error: {3}",
								Environment.NewLine + "   ",
								source,
								dest,
								ex.Message));*/
					}
				}
			//}
			//DirectoryInfo dirInfoDest=new DirectoryInfo(DestDirectory);
			//MessageBox.Show(dirInfoDest.GetFiles().Length.ToString());
			//The above test shows that by the time it gets to this point,
			//the files have already been copied over, so short wait.
			Thread.Sleep(300);
			//If Open Dental has been rebranded, then change this value:
			Process.Start(Path.Combine(DestDirectory,"OpenDental.exe"));
			Cursor=Cursors.Default;
			Application.Exit();
		}

		private static void KillProcess(string name) {
			Process[] processes=Process.GetProcessesByName(name);
			for(int i=0;i<processes.Length;i++) {
				try {
					processes[i].Kill();//CloseMainWindow and Close were ineffective if a dialog was open.
				}
				catch {
					//Kill() could fail if the process is closed between the time that the process list is read and the time that the Kill() function is called.
					//Since each Kill() call could take a few seconds, this exception could easily be caused by user interaction. In this case, the instance
					//is already closed so we don't need to take any further action.
				}
			}
		}
	}	
}
