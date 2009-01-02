using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlServerCe;
using System.Text;

namespace OpenDentMobile {
	///<summary>DataConnection</summary>
	public class Dcon {
		///<summary>This data adapter is used for all queries to the database.</summary>
		private static SqlCeDataAdapter da;
		///<summary>This is the connection that is used by the data adapter for all queries.</summary>
		private static SqlCeConnection con;
		///<summary>Stores the string of the command that will be sent to the database.</summary>
		public static SqlCeCommand cmd;
		public static string ConnectionString;

		///<summary>This needs to be run on startup.  Will throw an exception if fails.  Calling class should catch exception.</summary>
		public static void SetDb(string db){
			ConnectionString="Data Source='"+db+".sdf'";
			TestConnection(ConnectionString);
			//connection strings must be valid, so OK to set permanently
			//ConnectionString=connectStr;	
		}

		private static void TestConnection(string connectStr) {
			con=new SqlCeConnection(connectStr);
			cmd=new SqlCeCommand();
			cmd.Connection=con;
			con.Open();//this is where it will fail if no db yet exists.	
			//con.Close();
			//connection will always remain open.
		}

		/*
		///<summary></summary>
		public DataConnection(){
			con=new SqlCeConnection(ConnectionString);
			//dr = null;
			cmd = new SqlCeCommand();
			cmd.Connection=con;
			//table=new DataTable();
		}*/

		///<summary>Sends a non query command to the database and returns the number of rows affected. If true, then InsertID will be set to the value of the primary key of the newly inserted row.</summary>
		public static void NonQ(string command){//,bool getInsertID){
			//int rowsChanged=0;
			cmd.CommandText=command;
			//con.Open();
			//rowsChanged=
			cmd.ExecuteNonQuery();
			/*if(getInsertID) {
				cmd.CommandText="SELECT LAST_INSERT_ID()";
				dr=(MySqlDataReader)cmd.ExecuteReader();
				if(dr.Read())
					InsertID=Convert.ToInt32(dr[0].ToString());
			}*/
			//con.Close();
			//return rowsChanged;
		}

		///<summary>Sends a set of non query commands to the database and returns the number of rows affected. If true, then InsertID will be set to the value of the primary key of the newly inserted row.</summary>
		public static void NonQs(string commands){//,bool getInsertID){
			//int rowsChanged=0;
			//cmd.CommandText=commands;
			//con.Open();
//this will be a problem if a semicolon is within a text block somewhere:
			string[] commandArray=commands.Split(';');//,StringSplitOptions.RemoveEmptyEntries);
			//Can't do batch queries, so we have to split them up and run them individually.
			//if(getInsertID){
			//	cmdOr.CommandText="LOCK TABLE preference IN EXCLUSIVE MODE";
			//	cmdOr.ExecuteNonQuery();//Lock the preference table, because we need exclusive access to the OracleInsertId.
			//}
			for(int i=0;i<commandArray.Length;i++){
				if(commandArray[i]==""){
					continue;
				}
				cmd.CommandText=commandArray[i];
				//rowsChanged=
				cmd.ExecuteNonQuery();
			}
			//rowsChanged=
			//cmd.ExecuteNonQuery();
			/*if(getInsertID) {
				cmd.CommandText="SELECT LAST_INSERT_ID()";
				dr=(MySqlDataReader)cmd.ExecuteReader();
				if(dr.Read())
					InsertID=Convert.ToInt32(dr[0].ToString());
			}*/
			//con.Close();
			//return rowsChanged;
		}

		///<summary>Fills table with data from the database.</summary>
		public static DataTable GetTable(string command){
			DataTable table=new DataTable();
			cmd.CommandText=command;
			da=new SqlCeDataAdapter(cmd);
			da.Fill(table);
			//con.Close();
 			return table;
		}



	}
}
