using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Design;
using System.Drawing.Text;
using System.Drawing.Drawing2D;
using System.Drawing.Printing;
using System.Globalization;
using System.IO;
using System.Net;
using System.Resources;
using System.Text; 
using System.Windows.Forms;
using OpenDentBusiness;
using CodeBase;

namespace OpenDental{

	///<summary></summary>
	public partial class ClassConvertDatabase{
		private System.Version FromVersion;
		private System.Version ToVersion;

		///<summary>Return false to indicate exit app.  Only called when program first starts up at the beginning of FormOpenDental.PrefsStartup.</summary>
		public bool Convert(string fromVersion,string toVersion,bool silent) {
			FromVersion=new Version(fromVersion);
			ToVersion=new Version(toVersion);//Application.ProductVersion);
			if(FromVersion>=new Version("3.4.0") && PrefC.GetBool(PrefName.CorruptedDatabase)){
				MsgBox.Show(this,"Your database is corrupted because a conversion failed.  Please contact us.  This database is unusable and you will need to restore from a backup.");
				return false;//shuts program down.
			}
			if(FromVersion==ToVersion) {
				return true;//no conversion necessary
			}
			if(FromVersion.CompareTo(ToVersion)>0){//"Cannot convert database to an older version."
				//no longer necessary to catch it here.  It will be handled soon enough in CheckProgramVersion
				return true;
			}
			if(FromVersion < new Version("2.8.0")){
				MsgBox.Show(this,"This database is too old to easily convert in one step. Please upgrade to 2.1 if necessary, then to 2.8.  Then you will be able to upgrade to this version. We apologize for the inconvenience.");
				return false;
			}
			if(FromVersion < new Version("6.6.2")) {
				MsgBox.Show(this,"This database is too old to easily convert in one step. Please upgrade to 11.1 first.  Then you will be able to upgrade to this version. We apologize for the inconvenience.");
				return false;
			}
			if(FromVersion < new Version("3.0.1")) {
				MsgBox.Show(this,"This is an old database.  The conversion must be done using MySQL 4.1 (not MySQL 5.0) or it will fail.");
			}
			if(FromVersion.ToString()=="2.9.0.0" || FromVersion.ToString()=="3.0.0.0" || FromVersion.ToString()=="4.7.0.0"){
				MsgBox.Show(this,"Cannot convert this database version which was only for development purposes.");
				return false;
			}
			if(FromVersion > new Version("4.7.0") && FromVersion.Build==0){
				MsgBox.Show(this,"Cannot convert this database version which was only for development purposes.");
				return false;
			}
			if(FromVersion >= ConvertDatabases.LatestVersion){
				return true;//no conversion necessary
			}
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				MsgBox.Show(this,"Web client cannot convert database.  Must be using a direct connection.");
				return false;
			}
			if(ReplicationServers.ServerIsBlocked()) {
				MsgBox.Show(this,"This replication server is blocked from performing updates.");
				return false;
			}
			if(PrefC.GetString(PrefName.WebServiceServerName)!="" //using web service
				&& !ODEnvironment.IdIsThisComputer(PrefC.GetString(PrefName.WebServiceServerName).ToLower()))//and not on web server 
			{
				MessageBox.Show(Lan.g(this,"Updates are only allowed from the web server: ")+PrefC.GetString(PrefName.WebServiceServerName));
				return false;
			}
			//If MyISAM and InnoDb mix, then try to fix
			if(DataConnection.DBtype==DatabaseType.MySql) {//not for Oracle
				string namesInnodb=DatabaseMaintenance.GetInnodbTableNames();//Or possibly some other format.
				int numMyisam=DatabaseMaintenance.GetMyisamTableCount();
				if(namesInnodb!="" && numMyisam>0) {
					MessageBox.Show(Lan.g(this,"A mixture of database tables in InnoDB and MyISAM format were found.  A database backup will now be made, and then the following InnoDB tables will be converted to MyISAM format: ")+namesInnodb);
					try {
						MiscData.MakeABackup();//Does not work for Oracle, due to some MySQL specific commands inside.
					}
					catch(Exception e) {
						Cursor.Current=Cursors.Default;
						if(e.Message!="") {
							MessageBox.Show(e.Message);
						}
						MsgBox.Show(this,"Backup failed. Your database has not been altered.");
						return false;
					}
					if(!DatabaseMaintenance.ConvertTablesToMyisam()) {
						MessageBox.Show(Lan.g(this,"Failed to convert InnoDB tables to MyISAM format. Please contact support."));
						return false;
					}
					MessageBox.Show(Lan.g(this,"All tables converted to MyISAM format successfully."));
					namesInnodb="";
				}
				if(namesInnodb=="" && numMyisam>0) {//if all tables are myisam
					//but default storage engine is innodb, then kick them out.
					if(DatabaseMaintenance.GetStorageEngineDefaultName().ToUpper()!="MYISAM") { //Probably InnoDB but could be another format.
						MessageBox.Show(Lan.g(this,"The database tables are in MyISAM format, but the default database engine format is InnoDB. You must change the default storage engine within the my.ini (or my.cnf) file on the database server and restart MySQL in order to fix this problem. Exiting."));
						return false;
					}
				}
			}
#if DEBUG
			if(!silent && MessageBox.Show("You are in Debug mode.  Your database can now be converted"+"\r"
				+"from version"+" "+FromVersion.ToString()+"\r"
				+"to version"+" "+ToVersion.ToString()+"\r"
				+"You can click Cancel to skip conversion and attempt to the newer code against the older database."
				,"",MessageBoxButtons.OKCancel)!=DialogResult.OK)
			{
				return true;//If user clicks cancel, then do nothing
			}
#else
			if(!silent && MessageBox.Show(Lan.g(this,"Your database will now be converted")+"\r"
				+Lan.g(this,"from version")+" "+FromVersion.ToString()+"\r"
				+Lan.g(this,"to version")+" "+ToVersion.ToString()+"\r"
				+Lan.g(this,"The conversion works best if you are on the server.  Depending on the speed of your computer, it can be as fast as a few seconds, or it can take as long as 10 minutes.")
				,"",MessageBoxButtons.OKCancel)!=DialogResult.OK)
			{
				return false;//If user clicks cancel, then close the program
			}
#endif
			Cursor.Current=Cursors.WaitCursor;
#if !DEBUG
			if(DataConnection.DBtype!=DatabaseType.MySql
				&& !MsgBox.Show(this,true,"If you have not made a backup, please Cancel and backup before continuing.  Continue?"))
			{
				return false;
			}
			try{
				if(DataConnection.DBtype==DatabaseType.MySql) {
					MiscData.MakeABackup();//Does not work for Oracle, due to some MySQL specific commands inside.
				}
			}
			catch(Exception e){
				Cursor.Current=Cursors.Default;
				if(e.Message!=""){
					MessageBox.Show(e.Message);
				}
				MsgBox.Show(this,"Backup failed. Your database has not been altered.");
				return false;
			}
			try{
#endif
			if(FromVersion < new Version("7.5.17")) {//Insurance Plan schema conversion
				Cursor.Current=Cursors.Default;
				YN InsPlanConverstion_7_5_17_AutoMergeYN=YN.Unknown;
				if(FromVersion < new Version("7.5.1")) {
					FormInsPlanConvert_7_5_17 form=new FormInsPlanConvert_7_5_17();
					if(PrefC.GetBoolSilent(PrefName.InsurancePlansShared,true)) {
						form.InsPlanConverstion_7_5_17_AutoMergeYN=YN.Yes;
					}
					else {
						form.InsPlanConverstion_7_5_17_AutoMergeYN=YN.No;
					}
					form.ShowDialog();
					if(form.DialogResult==DialogResult.Cancel) {
						MessageBox.Show("Your database has not been altered.");
						return false;
					}
					InsPlanConverstion_7_5_17_AutoMergeYN=form.InsPlanConverstion_7_5_17_AutoMergeYN;
				}
				ConvertDatabases.Set_7_5_17_AutoMerge(InsPlanConverstion_7_5_17_AutoMergeYN);//does nothing if this pref is already present for some reason.
				Cursor.Current=Cursors.WaitCursor;
			}
			if(FromVersion>=new Version("3.4.0")){
				Prefs.UpdateBool(PrefName.CorruptedDatabase,true);
			}
			ConvertDatabases.FromVersion=FromVersion;
			ConvertDatabases.To2_8_2();//begins going through the chain of conversion steps
			Cursor.Current=Cursors.Default;
			if(FromVersion>=new Version("3.4.0")){
				//CacheL.Refresh(InvalidType.Prefs);//or it won't know it has to update in the next line.
				Prefs.UpdateBool(PrefName.CorruptedDatabase,false,true);//more forceful refresh in order to properly change flag
			}
			if(!silent) {
				MsgBox.Show(this,"Conversion successful");
			}
			Cache.Refresh(InvalidType.Prefs);
			return true;
#if !DEBUG
			}
			catch(System.IO.FileNotFoundException e){
				MessageBox.Show(e.FileName+" "+Lan.g(this,"could not be found. Your database has not been altered and is still usable if you uninstall this version, then reinstall the previous version."));
				if(FromVersion>=new Version("3.4.0")){
					Prefs.UpdateBool(PrefName.CorruptedDatabase,false);
				}
				//Prefs.Refresh();
				return false;
			}
			catch(System.IO.DirectoryNotFoundException){
				MessageBox.Show(Lan.g(this,"ConversionFiles folder could not be found. Your database has not been altered and is still usable if you uninstall this version, then reinstall the previous version."));
				if(FromVersion>=new Version("3.4.0")){
					Prefs.UpdateBool(PrefName.CorruptedDatabase,false);
				}
				//Prefs.Refresh();
				return false;
			}
			catch(Exception ex){
			//	MessageBox.Show();
				MessageBox.Show(ex.Message+"\r\n\r\n"
					+Lan.g(this,"Conversion unsuccessful. Your database is now corrupted and you cannot use it.  Please contact us."));
				//Then, application will exit, and database will remain tagged as corrupted.
				return false;
			}
#endif
		}

	}

}