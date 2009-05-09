using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Text;
using OpenDentBusiness;
using System.Windows.Forms;
using CodeBase;

namespace OpenDental {
	public class PrefL{

		///<summary>This ONLY runs when first opening the program</summary>
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
				Prefs.UpdateString("ProgramVersion",currentVersion.ToString());
				Cache.Refresh(InvalidType.Prefs);
			}
			if(storedVersion>currentVersion) {
				if(PrefC.UsingAtoZfolder){
					string setupBinPath=ODFileUtils.CombinePaths(FormPath.GetPreferredImagePath(),"Setup.exe");
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
					}
				}
				else{//Not using image path.
					//this does not bypass checking the RegistrationKey because that's the only way to get the UpdateCode.
					//perform program update automatically.
					string patchName="Setup.exe";
					string updateUri=PrefC.GetString("UpdateWebsitePath");
					string updateCode=PrefC.GetString("UpdateCode");
					string updateInfoMajor="";
					string updateInfoMinor="";
					if(FormUpdate.ShouldDownloadUpdate(updateUri,updateCode,out updateInfoMajor,out updateInfoMinor)){
						if(MessageBox.Show(updateInfoMajor+Lan.g("Prefs","Perform program update now?"),"",
							MessageBoxButtons.YesNo)==DialogResult.Yes)
						{
							string tempFile=ODFileUtils.CombinePaths(Path.GetTempPath(),patchName);//Resort to a more common temp file name.
							FormUpdate.DownloadInstallPatchFromURI(updateUri+updateCode+"/"+patchName,//Source URI
								tempFile);//Local destination file.
							File.Delete(tempFile);//Cleanup install file.
						}
					}
				}
				Application.Exit();//always exits, whether launch of setup worked or not
				return false;
			}
			return true;
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
