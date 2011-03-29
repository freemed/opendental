using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using OpenDental;
using OpenDentBusiness;
using System.Windows.Forms;

namespace TestCanada {
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
				//textResults.Text="Make a copy of any OD db and rename it to canadatest.";
				return false;
			}
		}

		public static string FreshFromDump(){
			string command="DROP DATABASE IF EXISTS canadatest";
			try{
				DataCore.NonQ(command);
			}
			catch{
				throw new Exception("Database could not be dropped.  Please remove any remaining text files and try again.");
			}
			command="CREATE DATABASE canadatest";
			DataCore.NonQ(command);
			SetDbConnection("canadatest");
			command=Properties.Resources.dumpcanada;
			DataCore.NonQ(command);
			string toVersion=Assembly.GetAssembly(typeof(OpenDental.PrefL)).GetName().Version.ToString();
			//MessageBox.Show(Application.ProductVersion+" - "+
			if(!PrefL.ConvertDB(true,toVersion)) {
				throw new Exception("Wrong version.");
			}
	/*
			ProcedureCodes.TcodesClear();
			//FormProcCodes.ImportProcCodes("",null,OpenDental.Properties.Resources.NoFeeProcCodes);
			FormProcCodes.ImportProcCodes("",CDT.Class1.GetADAcodes(),"");//Yes, this will be broken if not on a specially configured development machine.
			AutoCodes.SetToDefault();
			ProcButtons.SetToDefault();
			ProcedureCodes.ResetApptProcsQuickAdd();
			//RefreshCache (might be missing a few)  Or, it might make more sense to do this as an entirely separate method when running.
			ProcedureCodes.RefreshCache();
*/
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
				DELETE FROM etrans;
				";
			DataCore.NonQ(command);
			ProcedureCodes.RefreshCache();
			ProcedureCode procCode;
			if(!ProcedureCodes.IsValidCode("99222")) {
				procCode=new ProcedureCode();
				procCode.ProcCode="99222";
				procCode.Descript="Lab2";
				procCode.AbbrDesc="Lab2";
				procCode.IsCanadianLab=true;
				procCode.ProcCat=256;
				procCode.ProcTime="/X/";
				procCode.TreatArea=TreatmentArea.Mouth;
				ProcedureCodes.Insert(procCode);
				ProcedureCodes.RefreshCache();
			}
			procCode=ProcedureCodes.GetProcCode("99111");
			procCode.IsCanadianLab=true;
			ProcedureCodes.Update(procCode);
			ProcedureCodes.RefreshCache();
			if(!ProcedureCodes.IsValidCode("27213")) {
				procCode=new ProcedureCode();
				procCode.ProcCode="27213";
				procCode.Descript="Crown";
				procCode.AbbrDesc="Crn";
				procCode.ProcCat=250;
				procCode.ProcTime="/X/";
				procCode.TreatArea=TreatmentArea.Tooth;
				procCode.PaintType=ToothPaintingType.CrownLight;
				ProcedureCodes.Insert(procCode);
				ProcedureCodes.RefreshCache();
			}
			procCode=ProcedureCodes.GetProcCode("67211");
			procCode.TreatArea=TreatmentArea.Quad;
			ProcedureCodes.Update(procCode);
			ProcedureCodes.RefreshCache();



			return "Database cleared of old data.\r\n";
		}
	}
}
