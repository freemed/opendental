using System;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using OpenDentBusiness;
using CodeBase;

namespace OpenDental{
	///<summary></summary>
	public class Prefs{
		///<summary></summary>
		public static void Refresh() {
			DataSet ds=null;
			try {
				if(RemotingClient.OpenDentBusinessIsLocal) {
					ds=PrefB.Refresh();
				}
				else {
					DtoPrefRefresh dto=new DtoPrefRefresh();
					ds=RemotingClient.ProcessQuery(dto);
				}
			}
			catch(Exception e) {
				MessageBox.Show(e.Message);
				return;
			}
			PrefB.FillHList(ds.Tables[0]);//now, we have an HList on both the client and the server.
		}

		///<summary></summary>
		public static void Update(Pref pref) {
			string command= "UPDATE preference SET "
				+"valuestring = '"  +POut.PString(pref.ValueString)+"'"
				+" WHERE prefname = '"+POut.PString(pref.PrefName)+"'";
			General.NonQ(command);
		}

		///<summary>Updates a pref of type int.  Returns true if a change was required, or false if no change needed.</summary>
		public static bool UpdateInt(string prefName,int newValue) {
			if(!PrefB.HList.ContainsKey(prefName)) {
				MessageBox.Show(prefName+" is an invalid pref name.");
				return false;
			}
			if(PrefB.GetInt(prefName)==newValue) {
				return false;//no change needed
			}
			string command= "UPDATE preference SET "
				+"ValueString = '"+POut.PInt(newValue)+"' "
				+"WHERE PrefName = '"+POut.PString(prefName)+"'";
			General.NonQ(command);
			return true;
		}

		///<summary>Updates a pref of type double.  Returns true if a change was required, or false if no change needed.</summary>
		public static bool UpdateDouble(string prefName,double newValue) {
			if(!PrefB.HList.ContainsKey(prefName)) {
				MessageBox.Show(prefName+" is an invalid pref name.");
				return false;
			}
			if(PrefB.GetDouble(prefName)==newValue) {
				return false;//no change needed
			}
			string command = "UPDATE preference SET "
				+"ValueString = '"+POut.PDouble(newValue)+"' "
				+"WHERE PrefName = '"+POut.PString(prefName)+"'";
			General.NonQ(command);
			return true;
		}

		///<summary>Returns true if a change was required, or false if no change needed.</summary>
		public static bool UpdateBool(string prefName,bool newValue) {
			if(!PrefB.HList.ContainsKey(prefName)) {
				MessageBox.Show(prefName+" is an invalid pref name.");
				return false;
			}
			if(PrefB.GetBool(prefName)==newValue) {
				return false;//no change needed
			}
			string command = "UPDATE preference SET "
				+"ValueString = '"+POut.PBool(newValue)+"' "
				+"WHERE PrefName = '"+POut.PString(prefName)+"'";
			General.NonQ(command);
			return true;
		}

		///<summary>Returns true if a change was required, or false if no change needed.</summary>
		public static bool UpdateString(string prefName,string newValue) {
			if(!PrefB.HList.ContainsKey(prefName)) {
				MessageBox.Show(prefName+" is an invalid pref name.");
				return false;
			}
			if(PrefB.GetString(prefName)==newValue) {
				return false;//no change needed
			}
			string command = "UPDATE preference SET "
				+"ValueString = '"+POut.PString(newValue)+"' "
				+"WHERE PrefName = '"+POut.PString(prefName)+"'";
			General.NonQ(command);
			return true;
		}

		///<summary>Called in two places.  Once from RefreshLocalData, and also from FormBackups after a restore.</summary>
		public static bool CheckProgramVersion() {
			Version storedVersion=new Version(PrefB.GetString("ProgramVersion"));
			Version currentVersion=new Version(Application.ProductVersion);
			string database="";
			string command="";
			if(FormChooseDatabase.DBtype==DatabaseType.MySql){
				command="SELECT database()";
				DataTable table=General.GetTable(command);
				database=PIn.PString(table.Rows[0][0].ToString());
			}
			if(storedVersion<currentVersion) {
				UpdateString("ProgramVersion",currentVersion.ToString());
				Prefs.Refresh();
			}
			if(storedVersion>currentVersion) {
				if(PrefB.UsingAtoZfolder){
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
				}else{//Not using image path.
					//perform program update automatically.
					string patchName="Setup.exe";
					string updateUri=PrefB.GetString("UpdateWebsitePath");
					string registrationCode=PrefB.GetString("RegistrationNumber");
					string updateInfoMajor="";
					string updateInfoMinor="";
					if(FormUpdate.ShouldDownloadUpdate(updateUri,registrationCode,out updateInfoMajor,out updateInfoMinor)){
						if(MessageBox.Show(updateInfoMajor+Lan.g("Prefs","Perform program update now?"),"",
							MessageBoxButtons.YesNo)==DialogResult.Yes)
						{
							string tempFile=ODFileUtils.CombinePaths(Path.GetTempPath(),patchName);//Resort to a more common temp file name.
							FormUpdate.DownloadInstallPatchFromURI(updateUri+registrationCode+"/"+patchName,//Source URI
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

		///<summary>This ONLY runs when first opening the program</summary>
		public static bool ConvertDB() {
			ClassConvertDatabase ClassConvertDatabase2=new ClassConvertDatabase();
			string pref=PrefB.GetString("DataBaseVersion");
				//(Pref)PrefB.HList["DataBaseVersion"];
			//Debug.WriteLine(pref.PrefName+","+pref.ValueString);
			if(ClassConvertDatabase2.Convert(pref)){
				//((Pref)PrefB.HList["DataBaseVersion"]).ValueString)) {
				return true;
			}
			else {
				Application.Exit();
				return false;
			}
		}

		///<summary>This ONLY runs when first opening the program.  Gets run early in the sequence. Returns false if the program should exit.</summary>
		public static bool CheckMySqlVersion41() {
			if(FormChooseDatabase.DBtype!=DatabaseType.MySql){
				return true;
			}
			string command="SELECT @@version";
			DataTable table=General.GetTable(command);
			string thisVersion=PIn.PString(table.Rows[0][0].ToString());
			//if(thisVersion.Substring(0,3)=="4.0"){
			//do nothing
			//}
			if(thisVersion.Substring(0,3)=="4.1"
				|| thisVersion.Substring(0,3)=="5.0"
				|| thisVersion.Substring(0,3)=="5.1")
			{
				if(PrefB.HList.ContainsKey("DatabaseConvertedForMySql41"))
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
				command="SHOW TABLES";
				table=General.GetTable(command);
				string[] tableNames=new string[table.Rows.Count];
				for(int i=0;i<table.Rows.Count;i++) {
					tableNames[i]=table.Rows[i][0].ToString();
				}
				for(int i=0;i<tableNames.Length;i++) {
					if(tableNames[i]!="procedurecode") {
						command="ALTER TABLE "+tableNames[i]+" CONVERT TO CHARACTER SET utf8";
						General.NonQ(command);
					}
				}
				string[] commands=new string[]
				{
					//"ALTER TABLE procedurecode CHANGE OldCode OldCode varchar(15) character set utf8 collate utf8_bin NOT NULL"
					//,"ALTER TABLE procedurecode DEFAULT character set utf8"
					"ALTER TABLE procedurecode MODIFY Descript varchar(255) character set utf8 NOT NULL"
					,"ALTER TABLE procedurecode MODIFY AbbrDesc varchar(50) character set utf8 NOT NULL"
					,"ALTER TABLE procedurecode MODIFY ProcTime varchar(24) character set utf8 NOT NULL"
					,"ALTER TABLE procedurecode MODIFY DefaultNote text character set utf8 NOT NULL"
					,"ALTER TABLE procedurecode MODIFY AlternateCode1 varchar(15) character set utf8 NOT NULL"
					//,"ALTER TABLE procedurelog MODIFY OldCode varchar(15) character set utf8 collate utf8_bin NOT NULL"
					//,"ALTER TABLE autocodeitem MODIFY OldCode varchar(15) character set utf8 collate utf8_bin NOT NULL"
					//,"ALTER TABLE procbuttonitem MODIFY OldCode varchar(15) character set utf8 collate utf8_bin NOT NULL"
					//,"ALTER TABLE covspan MODIFY FromCode varchar(15) character set utf8 collate utf8_bin NOT NULL"
					//,"ALTER TABLE covspan MODIFY ToCode varchar(15) character set utf8 collate utf8_bin NOT NULL"
					//,"ALTER TABLE fee MODIFY OldCode varchar(15) character set utf8 collate utf8_bin NOT NULL"
				};
				General.NonQ(commands);
				//and set the default too
				command="ALTER DATABASE CHARACTER SET utf8";
				General.NonQ(command);
				command="INSERT INTO preference VALUES('DatabaseConvertedForMySql41','1')";
				General.NonQ(command);
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

		/*
		///<summary>Attempts to write a simple value to the database.  If it fails, then we know we don't have a good connection with write permissions.</summary>
		public static bool TryToConnect() {
			DataConnection dcon=new DataConnection();
			return dcon.IsValid();
		}

		///<summary></summary>
		public static bool DBExists() {
			DataConnection dcon=new DataConnection();
			return dcon.DbExists();
		}*/


	}

	


	


}










