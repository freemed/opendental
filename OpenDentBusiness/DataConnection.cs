/*=============================================================================================================
Open Dental GPL license Copyright (C) 2003  Jordan Sparks, DMD.  http://www.open-dent.com,  www.docsparks.com
See header in FormOpenDental.cs for complete text.  Redistributions must retain this text.
===============================================================================================================*/
using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Threading;
//#if !LINUX
//using Oracle.DataAccess.Client;
//using Oracle.DataAccess.Types;
//#else
//using System.Data.OracleClient;
//#endif
using CodeBase;

namespace OpenDentBusiness{
	///<summary></summary>
	public enum DatabaseType{
		MySql,
		Oracle
		//MS_Sql
	}

	///<summary></summary>
	public class DataConnection{//
		///<summary>The value here is now reliable for public use.  FormChooseDatabase.DBtype, which used to be used for the client is now gone.</summary>
		public static DatabaseType DBtype;
		///<summary>This data adapter is used for all queries to the database.</summary>
		private MySqlDataAdapter da;
		///<summary>This is the connection that is used by the data adapter for all queries.</summary>
		private static MySqlConnection con;
		///<summary>Used to get very small bits of data from the db when the data adapter would be overkill.  For instance retrieving the response after a command is sent.</summary>
		private MySqlDataReader dr;
		///<summary>Stores the string of the command that will be sent to the database.</summary>
		public MySqlCommand cmd;
		///<summary>After inserting a row, this variable will contain the primary key for the newly inserted row.  This can frequently save an additional query to the database.</summary>
		public long InsertID;
		private static string Database;
		private static string ServerName;
		private static string MysqlUser;
		private static string MysqlPass;
		//User with lower privileges:
		private static string MysqlUserLow;
		private static string MysqlPassLow;
		///<summary>If this is used, then none of the fields above will be set.</summary>
		private static string ConnectionString="";
		///<summary>milliseconds.</summary>
		private static int delayForTesting=400;
		private static bool logDebugQueries=true;

		//For queries that do not use this flag, all queries are split into single commands. For those SQL commands which
		//are a single command but contain multiple semi-colons, then this string should be set to false before the 
		//command is executed, then set back to true immediately thereafter.
		public static bool splitStrings=true;

		public static int DefaultPortNum(){
			switch(DBtype){
				case DatabaseType.Oracle:
					return 1521;
				case DatabaseType.MySql:
					return 3306;
				default:
					return 3306;//Guess (same as MySQL, to target larger customer crowd).
			}
		}

		/*
		///<Summary>This is only meaningful if local connection instead of through server.  This might be added to be able to access from ODR when users start customizing their RDL reports.  But for now, we should build the connection string programmatically</Summary>
		public static string GetCurrentConnectionString(){
			//ONLY TEMPORARY
			return BuildSimpleConnectionString(DatabaseType.MySql,ServerName,Database,DefaultPortNum().ToString(),MysqlUser,MysqlPass);
		}*/

		public static string BuildSimpleConnectionString(DatabaseType pDbType,string pServer,string pDatabase,string pUserID,string pPassword){
			string serverName=pServer;
			string port=DefaultPortNum().ToString();
			if(pServer.Contains(":")) {
				string[] serverNamePort=pServer.Split(new char[] { ':' },StringSplitOptions.RemoveEmptyEntries);
				serverName=serverNamePort[0];
				port=serverNamePort[1];
			}
			string connectStr="";
			/*
			if(DBtype==DatabaseType.Oracle){
				connectStr=
					"Data Source=(DESCRIPTION=(ADDRESS_LIST="
				+ "(ADDRESS=(PROTOCOL=TCP)(HOST="+serverName+")(PORT="+port+")))"
				+ "(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME="+pDatabase+")));"
				+ "User Id="+pUserID+";Password="+pPassword+";";
			}
			else if(DBtype==DatabaseType.MySql){*/
			connectStr=
				"Server="+serverName
				+";Port="+port//although this does seem to cause a bug in Mono.  We will revisit this bug if needed to exclude the port option only for Mono.
				+";Database="+pDatabase
				//+";Connect Timeout=20"
				+";User ID="+pUserID
				+";Password="+pPassword
				+";CharSet=utf8";
				//+";Pooling=false";
			//}
			return connectStr;
		}

		private string BuildSimpleConnectionString(string pServer,string pDatabase,string pUserID,string pPassword){
			return BuildSimpleConnectionString(DBtype,pServer,pDatabase,pUserID,pPassword);
		}

		///<summary>This needs to be run every time we switch databases, especially on startup.  Will throw an exception if fails.  Calling class should catch exception.</summary>
		public void SetDb(string server,string db,string user, string password, string userLow, string passLow, DatabaseType dbtype){
			DBtype=dbtype;
			string connectStr=BuildSimpleConnectionString(server,db,user,password);
			string connectStrLow="";
			if(userLow!=""){
				connectStrLow=BuildSimpleConnectionString(server,db,userLow,passLow);
			}
			TestConnection(connectStr,connectStrLow,dbtype,false);
			//connection strings must be valid, so OK to set permanently
			Database=db;
			ServerName=server;//yes, it includes the port
			MysqlUser=user;
			MysqlPass=password;
			MysqlUserLow=userLow;
			MysqlPassLow=passLow;			
		}

		///<summary>This needs to be run every time we switch databases, especially on startup.  Will throw an exception if fails.  Calling class should catch exception.</summary>
		public void SetDb(string connectStr,string connectStrLow,DatabaseType dbtype,bool skipValidation){
			TestConnection(connectStr,connectStrLow,dbtype,skipValidation);
			//connection string must be valid, so OK to set permanently
			ConnectionString=connectStr;
		}

		///<summary></summary>
		public void SetDb(string connectStr,string connectStrLow,DatabaseType dbtype){
			SetDb(connectStr,connectStrLow,dbtype,false);
		}

		private void TestConnection(string connectStr,string connectStrLow,DatabaseType dbtype,bool skipValidation) {
			DBtype=dbtype;
			con=new MySqlConnection(connectStr);
			cmd = new MySqlCommand();
			//cmd.CommandTimeout=30;
			cmd.Connection=con;
			con.Open();
			if(!skipValidation){
				cmd.CommandText="UPDATE preference SET ValueString = '0' WHERE ValueString = '0'";
				cmd.ExecuteNonQuery();
			}
			con.Close();
			if(connectStrLow!=""){
				con=new MySqlConnection(connectStrLow);
				cmd = new MySqlCommand();
				cmd.Connection=con;
				con.Open();
				cmd.CommandText="SELECT * FROM preference WHERE ValueString = 'DataBaseVersion'";
				DataTable table=new DataTable();
				da=new MySqlDataAdapter(cmd);
				da.Fill(table);
				con.Close();
			}
		}

		//<summary>Constructor sets the connection values.</summary>
		//private static string 

		public DataConnection(bool isLow){
			string connectStr=ConnectionString;
			if(connectStr.Length<1 && ServerName!=null) {
				connectStr=BuildSimpleConnectionString(ServerName,Database,MysqlUserLow,MysqlPassLow);
			}
			con=new MySqlConnection(connectStr);
			//dr = null;
			cmd = new MySqlCommand();
			cmd.Connection=con;
		}

		///<summary></summary>
		public DataConnection(){
			string connectStr=ConnectionString;
			if(connectStr.Length<1 && ServerName!=null) {
				connectStr=BuildSimpleConnectionString(ServerName,Database,MysqlUser,MysqlPass);
			}
			con=new MySqlConnection(connectStr);
			//dr = null;
			cmd = new MySqlCommand();
			cmd.Connection=con;
			//table=new DataTable();
		}

		///<summary>Only used from FormChooseDatabase to attempt connection with database.</summary>
		public DataConnection(DatabaseType dbtype) {
			//SetDb will be run immediately, so no need to do anything here.
		}

		///<summary></summary>
		public DataConnection(string database) {
			string connectStr=ConnectionString;//this doesn't really set it to the new db as intended. Deal with later.
			if(connectStr.Length<1) {
				connectStr=BuildSimpleConnectionString(ServerName,database,MysqlUser,MysqlPass);
			}
			con=new MySqlConnection(connectStr);
			//dr = null;
			cmd = new MySqlCommand();
			cmd.Connection=con;
			//table=new DataTable();
		}

		///<summary>Only used to fill the list of databases in the ChooseDatabase window and from Employees.GetAsteriskMissedCalls.</summary>
		public DataConnection(string serverName, string database, string mysqlUser, string mysqlPass,DatabaseType dtype){
			string connectStr=ConnectionString;
			if(connectStr.Length<1){
				connectStr=BuildSimpleConnectionString(dtype,serverName,database,mysqlUser,mysqlPass);
			}
			con=new MySqlConnection(connectStr);
			//dr = null;
			cmd = new MySqlCommand();
			cmd.Connection=con;
		}

		///<summary>Fills table with data from the database.</summary>
		public DataTable GetTable(string command){
			#if DEBUG
				if(logDebugQueries){
					Debug.WriteLine(command);
				}
				Thread.Sleep(delayForTesting);
			#endif
			DataTable table=new DataTable();
			cmd.CommandText=command;
			da=new MySqlDataAdapter(cmd);
			da.Fill(table);
			con.Close();
 			return table;
		}
		
		///<summary>Fills dataset with data from the database.</summary>
		public DataSet GetDs(string commands) {
			#if DEBUG
				if(logDebugQueries){
					Debug.WriteLine(commands);
				}
				Thread.Sleep(delayForTesting);
			#endif
			DataSet ds=new DataSet();
			cmd.CommandText=commands;
			da=new MySqlDataAdapter(cmd);
			da.Fill(ds);
			con.Close();
			return ds;
		}

		///<summary>Sends a non query command to the database and returns the number of rows affected. If true, then InsertID will be set to the value of the primary key of the newly inserted row.</summary>
		public long NonQ(string commands,bool getInsertID){
			#if DEBUG
				if(logDebugQueries){
					Debug.WriteLine(commands);
				}
				Thread.Sleep(delayForTesting);
			#endif
			long rowsChanged=0;
			cmd.CommandText=commands;
			con.Open();
			rowsChanged=cmd.ExecuteNonQuery();
			if(getInsertID) {
				cmd.CommandText="SELECT LAST_INSERT_ID()";
				dr=(MySqlDataReader)cmd.ExecuteReader();
				if(dr.Read()) {
					InsertID=Convert.ToInt64(dr[0].ToString());
				}
			}
			con.Close();
			return rowsChanged;
		}
		
		///<summary>Sends a non query command to the database and returns the number of rows affected. If true, then InsertID will be set to the value of the primary key of the newly inserted row.</summary>
		public long NonQ(string command) {
			return NonQ(command,false);
		}

		///<summary>Use this for count(*) queries.  They are always guaranteed to return one and only one value.  Uses datareader instead of datatable, so faster.  Can also be used when retrieving prefs manually, since they will also return exactly one value</summary>
		public string GetCount(string command){
			#if DEBUG
				if(logDebugQueries){
					Debug.WriteLine(command);
				}
				Thread.Sleep(delayForTesting);
			#endif
			string retVal="";
			cmd.CommandText=command;
			con.Open();
			dr=(MySqlDataReader)cmd.ExecuteReader();
			dr.Read();
			retVal=dr[0].ToString();
			con.Close();
			return retVal;
		}

		///<summary>Get one value.</summary>
		public string GetScalar(string command) {
			#if DEBUG
				if(logDebugQueries){
					Debug.WriteLine(command);
				}
				Thread.Sleep(delayForTesting);
			#endif
			string retVal="";
			cmd.CommandText=command;
			con.Open();
			object scalar=cmd.ExecuteScalar();
			if(scalar==null) {
				retVal="";
			}
			else {
				retVal=scalar.ToString();
			}
			con.Close();
			return retVal;
		}

	}
}
