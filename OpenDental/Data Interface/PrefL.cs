using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;
using OpenDentBusiness;
using System.Windows.Forms;
using CodeBase;

namespace OpenDental {
	public class PrefL{

		///<summary>This ONLY runs when first opening the program.  It returns true if either no conversion is necessary, or if conversion was successful.  False for other situations like corrupt db, trying to convert to older version, etc.</summary>
		public static bool ConvertDB() {
			ClassConvertDatabase ClassConvertDatabase2=new ClassConvertDatabase();
			string pref=PrefC.GetString("DataBaseVersion");
				//(Pref)PrefC.HList["DataBaseVersion"];
			//Debug.WriteLine(pref.PrefName+","+pref.ValueString);
			if(ClassConvertDatabase2.Convert(pref)){
				//((Pref)PrefC.HList["DataBaseVersion"]).ValueString)) {
				return true;
			}
			else {
				Application.Exit();
				return false;
			}
		}

		///<summary>Called in two places.  Once from RefreshLocalData, and also from FormBackups after a restore.</summary>
		public static bool CheckProgramVersion() {
			Version storedVersion=new Version(PrefC.GetString("ProgramVersion"));
			Version currentVersion=new Version(Application.ProductVersion);
			string database="";
			//string command="";
			if(DataConnection.DBtype==DatabaseType.MySql){
				database=MiscData.GetCurrentDatabase();
			}
			if(storedVersion<currentVersion) {
				//There are two different situations where this might happen.
				if(PrefC.GetString("UpdateInProgressOnComputerName")==""){//1. Just performed an update from this workstation on another database.
					//setup file needs to be downloaded again because it's in a different AtoZ folder.
					if(PrefC.UsingAtoZfolder) {
						string destDir=FormPath.GetPreferredImagePath();
						string updateCode="";
						try {
							updateCode=FormUpdate.GetUpdateCodeForThisVersion();
						}
						catch(Exception ex) {
							MessageBox.Show(ex.Message);
							//but keep going.
						}
						Prefs.UpdateString("UpdateInProgressOnComputerName",Environment.MachineName);
						if(updateCode != "") {
							Prefs.UpdateString("UpdateCode",updateCode);
							FormUpdate.DownloadInstallPatchFromURI(PrefC.GetString("UpdateWebsitePath")+updateCode+"/"+"Setup.exe",//Source URI
								ODFileUtils.CombinePaths(destDir,"Setup.exe"),false);//download, but don't run
							FormShutdown FormSD=new FormShutdown();
							FormSD.ShowDialog();
							if(FormSD.DialogResult==DialogResult.OK) {
								//can't really change the timer from here.  So we have a small risk of the program on this workstation shutting down.
								//FormOpenDental.timerSignals.Enabled=false;//so that this workstation doesn't shut down
								Signal sig=new Signal();
								sig.ITypes=((int)InvalidType.ShutDownNow).ToString();
								sig.SigType=SignalType.Invalid;
								Signals.Insert(sig);
								FormOpenDental.signalLastRefreshed=MiscData.GetNowDateTime();
								Computers.ClearAllHeartBeats();//always assume success
								//FormOpenDental.timerSignals.Enabled=true;
								SecurityLogs.MakeLogEntry(Permissions.Setup,0,"Shutdown all workstations.");
							}
						}
						//and don't exit.
					}
				}
				//and 2. Just performed an update from this workstation on this database.  We already downloaded Setup file.
				//if this computer just performed an update, and none of the other computers has updated yet.
				//then attempt to stash all files that are in the Application directory.
				if(PrefC.UsingAtoZfolder) {
					string folderUpdate=ODFileUtils.CombinePaths(FormPath.GetPreferredImagePath(),"UpdateFiles");
					if(Directory.Exists(folderUpdate)) {
						Directory.Delete(folderUpdate,true);
						//wait a bit so that CreateDirectory won't malfunction.
						DateTime now=DateTime.Now;
						while(Directory.Exists(folderUpdate) && DateTime.Now < now.AddSeconds(10)) {//up to 10 seconds
							Application.DoEvents();
						}
					}
					Directory.CreateDirectory(folderUpdate);
					DirectoryInfo dirInfo=new DirectoryInfo(Application.StartupPath);
					FileInfo[] appfiles=dirInfo.GetFiles();
					for(int i=0;i<appfiles.Length;i++) {
						if(appfiles[i].Name=="FreeDentalConfig.xml") {
							continue;//skip this one.
						}
						File.Copy(appfiles[i].FullName,ODFileUtils.CombinePaths(folderUpdate,appfiles[i].Name));
					}
					//Create a simple manifest file so that we know what version the files are for.
					File.WriteAllText(ODFileUtils.CombinePaths(folderUpdate,"Manifest.txt"),currentVersion.ToString(3));
				}//else if not used AtoZ, then no place to stash the files.
				Prefs.UpdateString("ProgramVersion",currentVersion.ToString());
				Prefs.UpdateString("UpdateInProgressOnComputerName","");//now, other workstations will be allowed to update.
				Cache.Refresh(InvalidType.Prefs);
			}
			if(storedVersion>currentVersion) {
				if(PrefC.UsingAtoZfolder){
					//string setupBinPath=ODFileUtils.CombinePaths(FormPath.GetPreferredImagePath(),"Setup.exe");
					string folderUpdate=ODFileUtils.CombinePaths(FormPath.GetPreferredImagePath(),"UpdateFiles");
					//look at the manifest to see if it's the version we need
					string manifestVersion=File.ReadAllText(ODFileUtils.CombinePaths(folderUpdate,"Manifest.txt"));
					if(manifestVersion==storedVersion.ToString(3)) {
						//manifest version matches
						if(MessageBox.Show(Lan.g("Prefs","Files will now be copied.")+"\r\n"
							+Lan.g("Prefs","Workstation version will be updated from ")+currentVersion.ToString(3)
							+Lan.g("Prefs"," to ")+storedVersion.ToString(3),
							"",MessageBoxButtons.OKCancel)
							!=DialogResult.OK)//they don't want to update for some reason.
						{
							Application.Exit();
							return false;
						}
						//copy UpdateFileCopier.exe to this directory
						File.Copy(ODFileUtils.CombinePaths(folderUpdate,"UpdateFileCopier.exe"),//source
							ODFileUtils.CombinePaths(Application.StartupPath,"UpdateFileCopier.exe"),//dest
							true);//overwrite
						//wait a moment to make sure the file was copied
						//FileInfo fi=new FileInfo("");
						//fi.Length
						Thread.Sleep(1000);
						//launch UpdateFileCopier to copy the remaining files to here.
						int processId=Process.GetCurrentProcess().Id;
						//MessageBox.Show("processId: "+processId.ToString(),"",MessageBoxButtons.OK,MessageBoxIcon.None);
						//bool hasExited=Process.GetProcessById(processId).HasExited;
						//MessageBox.Show("hasExited: "+hasExited.ToString(),"",MessageBoxButtons.OK,MessageBoxIcon.None);
						Process.Start("UpdateFileCopier.exe","\""+folderUpdate+"\""//pass the source directory to the file copier.
							+" "+processId.ToString());//and the processId of Open Dental.
					}
					else {//manifest version is wrong
						//No point trying the Setup.exe because that's probably wrong too.
						//Just go straight to downloading and running the Setup.exe.
						DownloadAndRunSetup(storedVersion,currentVersion);
					}
					/*
					if(File.Exists(setupBinPath)) {
						if(MessageBox.Show("You are attempting to run version "+currentVersion.ToString(3)+",\r\n"
							+"But the database "+database+"\r\n"
							+"is already using version "+storedVersion.ToString(3)+".\r\n"
							+"A newer version must have already been installed on at least one computer.\r\n"  
							+"The setup program stored in your A to Z folder will now be launched.\r\n"
							+"Or, if you hit Cancel, then you will have the option to download again."
							,"",MessageBoxButtons.OKCancel)==DialogResult.Cancel) {
							if(MessageBox.Show("Download again?","",MessageBoxButtons.OKCancel)
								==DialogResult.OK) {
								FormUpdate FormU=new FormUpdate();
								FormU.ShowDialog();
							}
							Application.Exit();
							return false;
						}
						try {
							Process.Start(setupBinPath);
						}
						catch {
							MessageBox.Show("Could not launch Setup.exe");
						}
					}
					else if(MessageBox.Show("A newer version has been installed on at least one computer,"+
							"but Setup.exe could not be found in any of the following paths: "+
							FormPath.GetPreferredImagePath()+".  Download again?","",MessageBoxButtons.OKCancel)==DialogResult.OK) {
						FormUpdate FormU=new FormUpdate();
						FormU.ShowDialog();
					}*/
				}
				else{//Not using image path.
					//this does not bypass checking the RegistrationKey because that's the only way to get the UpdateCode.
					//perform program update automatically.
					DownloadAndRunSetup(storedVersion,currentVersion);
				}
				Application.Exit();//always exits, whether launch of setup worked or not
				return false;
			}
			return true;
		}

		///<summary>If AtoZ.manifest was wrong, or if user is not using AtoZ, then just download again.  Will use temp dir.  If an appropriate download is not available, it will fail and inform user.</summary>
		private static void DownloadAndRunSetup(Version storedVersion,Version currentVersion) {
			string patchName="Setup.exe";
			string updateUri=PrefC.GetString("UpdateWebsitePath");
			string updateCode=PrefC.GetString("UpdateCode");
			string updateInfoMajor="";
			string updateInfoMinor="";
			if(!FormUpdate.ShouldDownloadUpdate(updateUri,updateCode,out updateInfoMajor,out updateInfoMinor)){
				return;
			}
			if(MessageBox.Show(
				Lan.g("Prefs","Setup file will now be downloaded.")+"\r\n"
				+Lan.g("Prefs","Workstation version will be updated from ")+currentVersion.ToString(3)
				+Lan.g("Prefs"," to ")+storedVersion.ToString(3),
				"",MessageBoxButtons.OKCancel)
				!=DialogResult.OK)//they don't want to update for some reason.
			{
				return;
			}
			string tempFile=ODFileUtils.CombinePaths(Path.GetTempPath(),patchName);
			FormUpdate.DownloadInstallPatchFromURI(updateUri+updateCode+"/"+patchName,//Source URI
				tempFile,true);//Local destination file.
			File.Delete(tempFile);//Cleanup install file.
		}

		///<summary>This ONLY runs when first opening the program.  Gets run early in the sequence. Returns false if the program should exit.</summary>
		public static bool CheckMySqlVersion41() {
			if(DataConnection.DBtype!=DatabaseType.MySql){
				return true;
			}
			string thisVersion=MiscData.GetMySqlVersion();
			if(thisVersion.Substring(0,3)=="4.1"
				|| thisVersion.Substring(0,3)=="5.0"
				|| thisVersion.Substring(0,3)=="5.1")
			{
				if(PrefC.HList.ContainsKey("DatabaseConvertedForMySql41"))
				//&& GetBool("DatabaseConvertedForMySql41"))
				{
					return true;//already converted
				}
				if(!MsgBox.Show("Prefs",true,"Your database will now be converted for use with MySQL 4.1.")) {
					Application.Exit();
					return false;
				}
				//ClassConvertDatabase CCD=new ClassConvertDatabase();
				try {
					MiscData.MakeABackup();
				}
				catch(Exception e) {
					if(e.Message!="") {
						MessageBox.Show(e.Message);
					}
					MsgBox.Show("Prefs","Backup failed. Your database has not been altered.");
					Application.Exit();
					return false;//but this should never happen
				}
				MessageBox.Show("Backup performed");
				Prefs.ConvertToMySqlVersion41();
				MessageBox.Show("converted");
				//Refresh();
			}
			else {
				MessageBox.Show(Lan.g("Prefs","Your version of MySQL won't work with this program")+": "+thisVersion
					+".  "+Lan.g("Prefs","You should upgrade to MySQL 4.1"));
				Application.Exit();
				return false;
			}
			return true;
		}

	}
}
