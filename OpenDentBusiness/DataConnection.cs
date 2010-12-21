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
#if !LINUX
using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;
#else
using System.Data.OracleClient;
#endif
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
		///<summary>Data adapter when 'isOracle' is set to true.</summary>
		private OracleDataAdapter daOr;
		///<summary>This is the connection that is used by the data adapter for all queries.  8/30/2010 js Changed this to be not static so that we can use it with multiple threads.  Has potential to cause bugs.</summary>
		private MySqlConnection con;
		///<summary>Connection that is being used when 'isOracle' is set to true.</summary>
		private OracleConnection conOr;
		///<summary>Used to get very small bits of data from the db when the data adapter would be overkill.  For instance retrieving the response after a command is sent.</summary>
		private MySqlDataReader dr;
		///<summary>The data reader being used when 'isOracle' is set to true.</summary>
		private OracleDataReader drOr;
		///<summary>Stores the string of the command that will be sent to the database.</summary>
		private MySqlCommand cmd;
		///<summary>The command to set when 'isOracle' is set to true?</summary>
		public OracleCommand cmdOr;
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
		private static ArrayList parameters=new ArrayList();
#if DEBUG
		///<summary>milliseconds.</summary>
		private static int delayForTesting=0;
		private static bool logDebugQueries=false;
#endif

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

		public static string GetDatabaseName(){
			return Database;
		}

		#region Constructors
		public DataConnection(bool isLow) {
			string connectStr=ConnectionString;
			if(connectStr.Length<1 && ServerName!=null) {
				connectStr=BuildSimpleConnectionString(ServerName,Database,MysqlUserLow,MysqlPassLow);
			}
			if(DBtype==DatabaseType.Oracle) {
				conOr=new OracleConnection(connectStr);
				//drOr = null;
				cmdOr=new OracleCommand();
				cmdOr.Connection=conOr;
			}
			else if(DBtype==DatabaseType.MySql) {
				con=new MySqlConnection(connectStr);
				//dr = null;
				cmd = new MySqlCommand();
				cmd.Connection=con;
			}
		}

		///<summary></summary>
		public DataConnection() {
			string connectStr=ConnectionString;
			if(connectStr.Length<1 && ServerName!=null) {
				connectStr=BuildSimpleConnectionString(ServerName,Database,MysqlUser,MysqlPass);
			}
			if(DBtype==DatabaseType.Oracle) {
				conOr=new OracleConnection(connectStr);
				//drOr = null;
				cmdOr=new OracleCommand();
				cmdOr.Connection=conOr;
				//table=new DataTable();
			}
			else if(DBtype==DatabaseType.MySql) {
				con=new MySqlConnection(connectStr);
				//dr = null;
				cmd = new MySqlCommand();
				cmd.Connection=con;
				//table=new DataTable();
			}
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
			if(DBtype==DatabaseType.Oracle) {
				conOr=new OracleConnection(connectStr);
				//drOr=null;
				cmdOr=new OracleCommand();
				cmdOr.Connection=conOr;
				//table=new DataTable();
			}
			else if(DBtype==DatabaseType.MySql) {
				con=new MySqlConnection(connectStr);
				//dr = null;
				cmd = new MySqlCommand();
				cmd.Connection=con;
				//table=new DataTable();
			}
		}

		///<summary>Only used to fill the list of databases in the ChooseDatabase window and from Employees.GetAsteriskMissedCalls.</summary>
		public DataConnection(string serverName,string database,string mysqlUser,string mysqlPass,DatabaseType dtype) {
			string connectStr=ConnectionString;
			if(connectStr.Length<1) {
				connectStr=BuildSimpleConnectionString(dtype,serverName,database,mysqlUser,mysqlPass);
			}
			if(dtype==DatabaseType.Oracle) {
				conOr=new OracleConnection(connectStr);
				//drOr=null;
				cmdOr=new OracleCommand();
				cmdOr.Connection=conOr;
			}
			else if(DBtype==DatabaseType.MySql) {
				con=new MySqlConnection(connectStr);
				//dr = null;
				cmd = new MySqlCommand();
				cmd.Connection=con;
			}
		}

		///<summary>Used by the mobile server because it does not need to worry about 3-tier scenarios.  Only supports MySQL.</summary>
		public DataConnection(string connectStr,bool isMobile) {//isMobile is ignored.  Just needed to make it different than the other constructor.
			con=new MySqlConnection(connectStr);
			cmd = new MySqlCommand();
			cmd.Connection=con;
		}
		#endregion Constructors

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
				+";CharSet=utf8"
				+";Treat Tiny As Boolean=false"
				+";Allow User Variables=true"
				+";Default Command Timeout=3600";//one hour timeout on commands.  Prevents crash during conversions, etc.
				//+";Pooling=false";
			//}
			return connectStr;
		}

		private string BuildSimpleConnectionString(string pServer,string pDatabase,string pUserID,string pPassword){
			return BuildSimpleConnectionString(DBtype,pServer,pDatabase,pUserID,pPassword);
		}

		private void PrepOracleConnection(){
			if(parameters.Count>0) {//Getting parameters for statement.
				for(int p=0;p<parameters.Count;p++) {
					cmdOr.Parameters.Add(":param"+(p+1),parameters[p]);
				}
				cmdOr.Prepare();
				parameters.Clear();
			}
			cmdOr.CommandText="ALTER SESSION SET NLS_DATE_FORMAT = 'YYYY-MM-DD HH24:MI:SS'";
			try{
				cmdOr.ExecuteNonQuery();	//Change the date-time format for this oracle connection to match our
																	//MySQL date-time format.
			}
			catch(Exception e) {
				Logger.openlog.LogMB("Oracle SQL Error: "+cmdOr.CommandText+"\r\n"+"Exception: "+e.ToString(),Logger.Severity.ERROR);
				throw;//continue to pass the exception one level up.
			}
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
			if(DBtype==DatabaseType.Oracle) {
				conOr=new OracleConnection(connectStr);
				cmdOr=new OracleCommand();
				conOr.Open();
				cmdOr.Connection=conOr;
				//UPDATE preference SET ValueString = '0' WHERE ValueString = '0'
				cmdOr.CommandText="UPDATE preference SET PrefName = '0' WHERE PrefName = '0'";
				cmdOr.ExecuteNonQuery();
				conOr.Close();
				if(connectStrLow!="") {
					conOr=new OracleConnection(connectStrLow);
					cmdOr=new OracleCommand();
					conOr.Open();
					cmdOr.Connection=conOr;
					cmdOr.CommandText="SELECT * FROM preference WHERE ValueString = 'DataBaseVersion'";
					DataTable table=new DataTable();
					daOr=new OracleDataAdapter(cmdOr);
					daOr.Fill(table);
					conOr.Close();
				}
			}
			else {
				con=new MySqlConnection(connectStr);
				cmd = new MySqlCommand();
				//cmd.CommandTimeout=30;
				cmd.Connection=con;
				con.Open();
				if(!skipValidation) {
					cmd.CommandText="UPDATE preference SET ValueString = '0' WHERE ValueString = '0'";
					cmd.ExecuteNonQuery();
				}
				con.Close();
				if(connectStrLow!="") {
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
			if(DBtype==DatabaseType.Oracle){
				conOr.Open();
				PrepOracleConnection();
				cmdOr.CommandText=command;
				daOr=new OracleDataAdapter(cmdOr);
				try{
					daOr.Fill(table);
				}
				catch (System.Exception e){
					Logger.openlog.LogMB("Oracle SQL Error: "+cmdOr.CommandText+"\r\n"+"Exception: "+e.ToString(),Logger.Severity.ERROR);
					throw e;//continue to pass the exception one level up.
				}
				conOr.Close();
			}
			else if(DBtype==DatabaseType.MySql) {
				cmd.CommandText=command;
				da=new MySqlDataAdapter(cmd);
				da.Fill(table);
				con.Close();
			}
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
			if(DBtype==DatabaseType.Oracle){
				conOr.Open();
				PrepOracleConnection();
				string[] commandArray=new string[] { commands };
				if(splitStrings) {
					commandArray=commands.Split(new char[] { ';' },StringSplitOptions.RemoveEmptyEntries);
				}
				//Can't do batch queries in Oracle, so we have to split them up and run them individually.
				foreach(string com in commandArray){
					cmdOr.CommandText=com;
					daOr=new OracleDataAdapter(cmdOr);
					DataTable dsTab=new DataTable();
					try{
						daOr.Fill(dsTab);
					}
					catch(System.Exception e) {
						Logger.openlog.LogMB("Oracle SQL Error: "+cmdOr.CommandText+"\r\n"+"Exception: "+e.ToString(),Logger.Severity.ERROR);
						throw e;//continue to pass the exception one level up.
					}
					ds.Tables.Add(dsTab);
				}
				conOr.Close();
			}
			else if(DBtype==DatabaseType.MySql){
				cmd.CommandText=commands;
				da=new MySqlDataAdapter(cmd);
				da.Fill(ds);
				con.Close();
			}
			return ds;
		}

		///<summary>Sends a non query command to the database and returns the number of rows affected. If true, then InsertID will be set to the value of the primary key of the newly inserted row.</summary>
		public long NonQ(string commands,bool getInsertID,params OdSqlParameter[] parameters){
			#if DEBUG
				if(logDebugQueries){
					Debug.WriteLine(commands);
				}
				Thread.Sleep(delayForTesting);
			#endif
			long rowsChanged=0;
			if(DBtype==DatabaseType.Oracle){
				conOr.Open();
				PrepOracleConnection();
				//string[] commandArray=new string[] {commands};
				//if(splitStrings){
				//  commandArray=commands.Split(new char[] {';'},StringSplitOptions.RemoveEmptyEntries);
				//}
				//Can't do batch queries in Oracle, so we have to split them up and run them individually.
				try{
					if(getInsertID){
						cmdOr.CommandText="LOCK TABLE preference IN EXCLUSIVE MODE";
						cmdOr.ExecuteNonQuery();//Lock the preference table, because we need exclusive access to the OracleInsertId.
					}
					//for(int i=0;i<commandArray.Length;i++){
						cmdOr.CommandText=commands; //Array[i];
						rowsChanged=cmdOr.ExecuteNonQuery();
					//}
				}
				catch(System.Exception e){
					Logger.openlog.LogMB("Oracle SQL Error: "+cmdOr.CommandText+"\r\n"+"Exception: "+e.ToString(),Logger.Severity.ERROR);
					throw;//continue to pass the exception one level up.
				}
				finally{
					if(getInsertID){
						try{
							cmdOr.CommandText="SELECT ValueString FROM preference WHERE PrefName='OracleInsertId'";
							daOr=new OracleDataAdapter(cmdOr);
							DataTable table=new DataTable();
							daOr.Fill(table);//Will always return a result, unless a critical error, in which case we will catch.
							this.InsertID=Convert.ToInt32((table.Rows[0][0]).ToString());
							cmdOr.CommandText="commit";
							cmdOr.ExecuteNonQuery();//Release the exlusive lock we attaned above.
						}catch(Exception e){
							Logger.openlog.LogMB("Oracle SQL Error: "+cmdOr.CommandText+"\r\n"+"Exception: "+e.ToString(),
								Logger.Severity.ERROR);
							throw e;//continue to pass the exception one level up.
						}
					}
				}
				conOr.Close();
			}
			else if(DBtype==DatabaseType.MySql) {
				cmd.CommandText=commands;
				for(int p=0;p<parameters.Length;p++) {
					cmd.Parameters.Add(parameters[p].GetMySqlParameter());
				}
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
			}
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
			if(DBtype==DatabaseType.Oracle){
				conOr.Open();
				PrepOracleConnection();
				cmdOr.CommandText=command;
				try{
					drOr=(OracleDataReader)cmdOr.ExecuteReader();
				}
				catch(System.Exception e) {
					Logger.openlog.LogMB("Oracle SQL Error: "+cmdOr.CommandText+"\r\n"+"Exception: "+e.ToString(),Logger.Severity.ERROR);
					throw;//continue to pass the exception one level up.
				}
				drOr.Read();
				retVal=drOr[0].ToString();
				conOr.Close();
			}
			else if(DBtype==DatabaseType.MySql) {
				cmd.CommandText=command;
				con.Open();
				dr=(MySqlDataReader)cmd.ExecuteReader();
				dr.Read();
				retVal=dr[0].ToString();
				con.Close();
			}
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
//todo: Oracle.
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

		public static void AddParam(object param) {
			parameters.Add(param);
		}

	}
}
