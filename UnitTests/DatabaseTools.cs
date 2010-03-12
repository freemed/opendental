using System;
using System.Collections.Generic;
using System.Text;
using OpenDentBusiness;
using OpenDentBusiness.DataAccess;
using System.Windows.Forms;

namespace UnitTests {
	///<summary>Contains the queries, scripts, and tools to clear the database of data from previous unitTest runs.</summary>
	class DatabaseTools {
		//public static string DbName;

		//public static bool DbExists(){

		//}

		///<summary>This is analogous to clicking OK from the ChooseDatabase window.</summary>
		public static string SetDbConnection(string dbName){
			OpenDentBusiness.DataConnection dcon;
			//Try to connect to the database directly
			try {
				DataConnection.DBtype=DatabaseType.MySql;
				DataSettings.DbType = DataConnection.DBtype;
				dcon=new OpenDentBusiness.DataConnection(DataConnection.DBtype);
				DataSettings.ConnectionString = "Server=localhost;Database="+dbName+";User ID=root;Password=;CharSet=utf8";
				dcon.SetDb("Server=localhost;Database="+dbName+";User ID=root;Password=;CharSet=utf8","",DataConnection.DBtype);
			}
			catch(Exception ex){
				throw new Exception(ex.Message);
				//MessageBox.Show(ex.Message);
				//textResults.Text="Make a copy of any OD db and rename it to unittest.";
				//return;
			}
			RemotingClient.RemotingRole=RemotingRole.ClientDirect;
			return "Connected.";
		}

		public static void FreshFromDump(){
			string command="DROP DATABASE unittest";
			try{
				DataCore.NonQ(command);
			}
			catch{
				throw new Exception("Database could not be dropped.  Please remove any remaining text files and try again.");
			}
			command="CREATE DATABASE unittest";
			DataCore.NonQ(command);

		}
	}
}
