using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Net;
using System.Reflection;

namespace OpenDentBusiness {

	///<summary>Miscellaneous database functions.</summary>
	public class MiscData {

		///<summary>Gets the current date/Time direcly from the server.  Mostly used to prevent uesr from altering the workstation date to bypass security.</summary>
		public static DateTime GetNowDateTime() {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<DateTime>(MethodBase.GetCurrentMethod());
			}
			string command="SELECT NOW()";
			if(DataConnection.DBtype==DatabaseType.Oracle) {
				command="SELECT CURRENT_TIMESTAMP FROM DUAL";
			}
			DataTable table=Db.GetTable(command);
			return PIn.DateT(table.Rows[0][0].ToString());
		}

		///<summary>Gets the current date/Time with milliseconds directly from server.  In Mysql we must query the server until the second rolls over, which may take up to one second.  Used to confirm synchronization in time for EHR.</summary>
		public static DateTime GetNowDateTimeWithMilli() {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<DateTime>(MethodBase.GetCurrentMethod());
			}
			string command;
			string dbtime;
			if(DataConnection.DBtype==DatabaseType.MySql) {
				command="SELECT NOW()"; //Only up to 1 second precision pre-Mysql 5.6.4.  Does not round milliseconds.
				dbtime=Db.GetScalar(command);
				int secondInit=PIn.DateT(dbtime).Second;
				int secondCur;
				//Continue querying server for current time until second changes (milliseconds will be close to 0)
				do {
					dbtime=Db.GetScalar(command);
					secondCur=PIn.DateT(dbtime).Second;
				}
				while(secondInit==secondCur);
			}
			else {
				command="SELECT CURRENT_TIMESTAMP(3) FROM DUAL"; //Timestamp with milliseconds
				dbtime=Db.GetScalar(command);
			}
			return PIn.DateT(dbtime);
		}


		///<summary>Used in MakeABackup to ensure a unique backup database name.</summary>
		private static bool Contains(string[] arrayToSearch,string valueToTest) {
			//No need to check RemotingRole; no call to db.
			string compare;
			for(int i=0;i<arrayToSearch.Length;i++) {
				compare=arrayToSearch[i];
				if(arrayToSearch[i]==valueToTest) {
					return true;
				}
			}
			return false;
		}

		///<summary>Backs up the database to the same directory as the original just in case the user did not have sense enough to do a backup first.</summary>
		public static long MakeABackup() {
			//This function should always make the backup on the server itself, and since no directories are
			//referred to (all handled with MySQL), this function will always be referred to the server from
			//client machines.
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetLong(MethodBase.GetCurrentMethod());
			}
			//only used in two places: upgrading version, and upgrading mysql version.
			//Both places check first to make sure user is using mysql.
			//we have to be careful to throw an exception if the backup is failing.
			DataConnection dcon=new DataConnection();
			string command="SELECT database()";
			DataTable table=dcon.GetTable(command);
			string oldDb=PIn.String(table.Rows[0][0].ToString());
			string newDb=oldDb+"backup_"+DateTime.Today.ToString("MM_dd_yyyy");
			command="SHOW DATABASES";
			table=dcon.GetTable(command);
			string[] databases=new string[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++) {
				databases[i]=table.Rows[i][0].ToString();
			}
			if(Contains(databases,newDb)) {//if the new database name already exists
				//find a unique one
				int uniqueID=1;
				string originalNewDb=newDb;
				do {
					newDb=originalNewDb+"_"+uniqueID.ToString();
					uniqueID++;
				}
				while(Contains(databases,newDb));
			}
			command="CREATE DATABASE "+newDb+" CHARACTER SET utf8";
			dcon.NonQ(command);
			command="SHOW TABLES";
			table=dcon.GetTable(command);
			string[] tableName=new string[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++) {
				tableName[i]=table.Rows[i][0].ToString();
			}
			//switch to using the new database
			DataConnection newDcon=new DataConnection(newDb);
			for(int i=0;i<tableName.Length;i++) {
				command="SHOW CREATE TABLE "+oldDb+"."+tableName[i];
				table=newDcon.GetTable(command);
				command=PIn.ByteArray(table.Rows[0][1]);
				newDcon.NonQ(command);//this has to be run using connection with new database
				command="INSERT INTO "+newDb+"."+tableName[i]
					+" SELECT * FROM "+oldDb+"."+tableName[i];
				newDcon.NonQ(command);
			}
			return 0;
		}

		public static string GetCurrentDatabase() {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetString(MethodBase.GetCurrentMethod());
			}
			string command="SELECT database()";
			DataTable table=Db.GetTable(command);
			return PIn.String(table.Rows[0][0].ToString());
		}

		public static string GetMySqlVersion() {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetString(MethodBase.GetCurrentMethod());
			}
			string command="SELECT @@version";
			DataTable table=Db.GetTable(command);
			return PIn.String(table.Rows[0][0].ToString());
		}

		///<summary>Gets the human readable host name of the database server, even when using the middle-tier.  This will return an empty string if Dns lookup fails.</summary>
		public static string GetODServer() {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetString(MethodBase.GetCurrentMethod());
			}
			//string command="SELECT @@hostname";//This command fails in MySQL 5.0.22 (the version of MySQL 5.0 we used to use), because the hostname variable was added in MySQL 5.0.38.
			//string rawHostName=DataConnection.GetServerName();//This could be a human readable name, or it might be "localhost" or "127.0.0.1" or another IP address.
			//return Dns.GetHostEntry(rawHostName).HostName;//Return the human readable name (full domain name) corresponding to the rawHostName.
			//Had to strip off the port, caused Dns.GetHostEntry to fail and is not needed to get the hostname
			string rawHostName=DataConnection.GetServerName().Split(':')[0];//This could be a human readable name, or it might be "localhost" or "127.0.0.1" or another IP address.
			string retval="";
			try {
				retval=Dns.GetHostEntry(rawHostName).HostName;//Return the human readable name (full domain name) corresponding to the rawHostName.
			}
			catch(Exception ex) {
			}
			return retval;
		}

		///<summary>Returns a collection of unique AtoZ folders for the array of dbnames passed in.  It will not include the current AtoZ folder for this database, even if shared by another db.  This is used for the feature that updates multiple databases simultaneously.</summary>
		public static List<string> GetAtoZforDb(string[] dbNames) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				throw new ApplicationException("MiscData.GetAtoZforDb failed.  Updates not allowed from ClientWeb.");
			}
			List<string> retval=new List<string>();
			DataConnection dcon=null;
			string atozName;
			string atozThisDb=PrefC.GetString(PrefName.DocPath);
			for(int i=0;i<dbNames.Length;i++) {
				try {
					dcon=new DataConnection(dbNames[i]);
					string command="SELECT ValueString FROM preference WHERE PrefName='DocPath'";
					atozName=dcon.GetScalar(command);
					if(retval.Contains(atozName)) {
						continue;
					}
					if(atozName==atozThisDb) {
						continue;
					}
					retval.Add(atozName);
				}
				catch {
					//don't add it to the list
				}
			}
			return retval;
		}

		public static void LockWorkstationsForDbs(string[] dbNames) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				throw new ApplicationException("MiscData.LockWorkstationsForDbs failed.  Updates not allowed from ClientWeb.");
			}
			DataConnection dcon=null;
			for(int i=0;i<dbNames.Length;i++) {
				try {
					dcon=new DataConnection(dbNames[i]);
					string command="UPDATE preference SET ValueString ='"+POut.String(Environment.MachineName)
						+"' WHERE PrefName='UpdateInProgressOnComputerName'";
					dcon.NonQ(command);
				}
				catch { }
			}
		}

		public static void UnlockWorkstationsForDbs(string[] dbNames) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				throw new ApplicationException("MiscData.UnlockWorkstationsForDbs failed.  Updates not allowed from ClientWeb.");
			}
			DataConnection dcon=null;
			for(int i=0;i<dbNames.Length;i++) {
				try {
					dcon=new DataConnection(dbNames[i]);
					string command="UPDATE preference SET ValueString =''"
						+" WHERE PrefName='UpdateInProgressOnComputerName'";
					dcon.NonQ(command);
				}
				catch { }
			}
		}

		public static void SetSqlMode() {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod());
				return;
			}
			string command="SET GLOBAL sql_mode=''";//in case user did not use our my.ini file.
			//A slightly more elegant approach could require less user permissions.  It could first check SELECT @@GLOBAL.sql_mode, 
			//and then only attempt to set if it was not blank already.
			Db.NonQ(command);
		}

	}

	
}































