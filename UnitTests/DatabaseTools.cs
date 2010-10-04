using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using OpenDental;
using OpenDentBusiness;
using System.Windows.Forms;

namespace UnitTests {
	///<summary>Contains the queries, scripts, and tools to clear the database of data from previous unitTest runs.</summary>
	class DatabaseTools {
		//public static string DbName;

		//public static bool DbExists(){
		//	string command="";
		//}

		///<summary>This is analogous to FormChooseDatabase.TryToConnect.  Empty string is allowed.</summary>
		public static bool SetDbConnection(string dbName){
			OpenDentBusiness.DataConnection dcon;
			//Try to connect to the database directly
			try {
				DataConnection.DBtype=DatabaseType.MySql;
				dcon=new OpenDentBusiness.DataConnection(DataConnection.DBtype);
				dcon.SetDb("Server=localhost;Database="+dbName+";User ID=root;Password=;CharSet=utf8;Treat Tiny As Boolean=false","",DataConnection.DBtype,true);
				RemotingClient.RemotingRole=RemotingRole.ClientDirect;
				return true;
			}
			catch{//(Exception ex){
				//throw new Exception(ex.Message);
				//MessageBox.Show(ex.Message);
				//textResults.Text="Make a copy of any OD db and rename it to unittest.";
				return false;
			}
		}

		public static string FreshFromDump(){
			string command="DROP DATABASE IF EXISTS unittest";
			try{
				DataCore.NonQ(command);
			}
			catch{
				throw new Exception("Database could not be dropped.  Please remove any remaining text files and try again.");
			}
			command="CREATE DATABASE unittest";
			DataCore.NonQ(command);
			SetDbConnection("unittest");
			command=Properties.Resources.dump;
			DataCore.NonQ(command);
			string toVersion=Assembly.GetAssembly(typeof(OpenDental.PrefL)).GetName().Version.ToString();
			//MessageBox.Show(Application.ProductVersion+" - "+
			if(!PrefL.ConvertDB(true,toVersion)) {
				throw new Exception("Wrong version.");
			}
			ProcedureCodes.TcodesClear();
			//FormProcCodes.ImportProcCodes("",null,OpenDental.Properties.Resources.NoFeeProcCodes);
			FormProcCodes.ImportProcCodes("",CDT.Class1.GetADAcodes(),"");//IF THIS LINE CRASHES:
			//Go to Solution, Configuration Manager.  Exclude UnitTest project from build.
			AutoCodes.SetToDefault();
			ProcButtons.SetToDefault();
			ProcedureCodes.ResetApptProcsQuickAdd();
			//RefreshCache (might be missing a few)  Or, it might make more sense to do this as an entirely separate method when running.
			ProcedureCodes.RefreshCache();
			return "Fresh database loaded from sql dump.\r\n";
		}

		public static string ClearDb() {
			string command=@"
				DELETE FROM carrier;
				DELETE FROM claim;
				DELETE FROM claimproc;
				DELETE FROM fee;
				DELETE FROM feesched WHERE FeeSchedNum !=53; /*because this is the default fee schedule for providers*/
				DELETE FROM insplan;
				DELETE FROM patient;
				DELETE FROM patplan;
				DELETE FROM procedurelog;
				";
			DataCore.NonQ(command);
			return "Database cleared of old data.\r\n";
		}
	}
}
